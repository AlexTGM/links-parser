using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebSiteParser.Controllers;
using WebSiteParser.Controllers.Requests;
using WebSiteParser.Controllers.Responses;
using WebSiteParser.Parsers;
using WebSiteParser.Parsers.Options;
using WebSiteParser.RequestHandlers;
using WebSiteParser.Requests;
using WebSiteParser.Rules.ContentValidationLengthRule;
using WebSiteParser.Rules.Infrastructure;
using Xunit;
using Xunit.Sdk;

namespace WebSiteParser.Tests
{
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        public virtual Task<HttpResponseMessage> Send(HttpRequestMessage request) => throw new NetworkInformationException();

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) =>
            Send(request);
    }

    public class ParserControllerShould
    {
        private readonly ParserController _parserController;

        public ParserControllerShould()
        {
            var mediatorMock = new Mock<IMediator>();

            mediatorMock.Setup(m => m.Send(It.IsAny<ParseWebSiteRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PageResult("test", new List<string>(), new List<PageResult>()));

            _parserController = new ParserController(mediatorMock.Object);
        }

        [Fact]
        public async Task ThrowBadRequestErrorWhenUrlProvidedIsIncorrect()
        {
            var actual = await _parserController.ParseWebSite(new ParseRequest { Url = "" });

            actual.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task ParseWebSite()
        {
            var actual = await _parserController.ParseWebSite(new ParseRequest { Url = "http://test.test" });

            actual.Should().BeOfType<OkObjectResult>();
        }
    }

    public class WebSiteDownloadRequestHandlerShould
    {
        private readonly Mock<FakeHttpMessageHandler> _fakeHttpMessageHandler =
            new Mock<FakeHttpMessageHandler> { CallBase = true };

        private readonly WebSiteDownloadRequestHandler _handler;

        public WebSiteDownloadRequestHandlerShould()
        {
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(mock => mock.CreateClient(It.IsAny<string>()))
                .Returns(new HttpClient(_fakeHttpMessageHandler.Object));

            _handler = new WebSiteDownloadRequestHandler(httpClientFactoryMock.Object);
        }

        [Fact]
        public async Task DownloadWebSiteContent()
        {
            var uri = new Uri("http://test.test");

            _fakeHttpMessageHandler.Setup(mock => mock.Send(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(() => new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(string.Empty)
                });

            var actual = await _handler.Handle(new WebSiteDownloadRequest(uri), CancellationToken.None);

            actual.Should().NotBeNull();
        }
    }

    public class ParseHtmlRequestHandlerShould
    {
        private readonly Mock<IHtmlParser> _htmlParserMock = new Mock<IHtmlParser>();

        private readonly ParseHtmlRequestHandler _htmlRequestHandler;

        public ParseHtmlRequestHandlerShould()
        {
            _htmlRequestHandler = new ParseHtmlRequestHandler(_htmlParserMock.Object);
        }

        [Fact]
        public async Task ParseWebDocument()
        {
            _htmlParserMock.Setup(m => m.ParseDocumentAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Mock<IHtmlDocument>().Object);

            var actual = await _htmlRequestHandler.Handle(new ParseHtmlRequest(string.Empty), CancellationToken.None);

            actual.Should().NotBeNull();
        }
    }

    public class ContentValidationLengthRuleShould
    {
        [Fact]
        public void FailWhenContentLengthLessThanMinimum()
        {
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(string.Join("", Enumerable.Range(0, 1).Select(x => x)))
            };

            var argsParserMock = new Mock<IValidationRuleArgsParser<ContentValidationLengthRuleArgs>>();
            argsParserMock.Setup(m => m.Parse(It.IsAny<object[]>()))
                .Returns(new ContentValidationLengthRuleArgs(100, long.MaxValue));

            var rule = new ContentValidationLengthRule(argsParserMock.Object);

            var result = rule.IsApplicable(httpResponseMessage.Content.Headers);

            result.Should().BeFalse();
        }

        [Fact]
        public void SuccessWhenContentLengthMoreThanMinimum()
        {
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(string.Join("", Enumerable.Range(0, 1000).Select(x => x)))
            };

            var argsParserMock = new Mock<IValidationRuleArgsParser<ContentValidationLengthRuleArgs>>();
            argsParserMock.Setup(m => m.Parse(It.IsAny<object[]>()))
                .Returns(new ContentValidationLengthRuleArgs(100, long.MaxValue));

            var rule = new ContentValidationLengthRule(argsParserMock.Object);

            var result = rule.IsApplicable(httpResponseMessage.Content.Headers);

            result.Should().BeTrue();
        }

        [Fact]
        public void FailWhenContentLengthMoreThanMaximum()
        {
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(string.Join("", Enumerable.Range(0, 1000).Select(x => x)))
            };

            var argsParserMock = new Mock<IValidationRuleArgsParser<ContentValidationLengthRuleArgs>>();
            argsParserMock.Setup(m => m.Parse(It.IsAny<object[]>()))
                .Returns(new ContentValidationLengthRuleArgs(0, 100));

            var rule = new ContentValidationLengthRule(argsParserMock.Object);

            var result = rule.IsApplicable(httpResponseMessage.Content.Headers);

            result.Should().BeFalse();
        }

        [Fact]
        public void SuccessWhenContentLengthLessThanMaximum()
        {
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(string.Join("", Enumerable.Range(0, 1).Select(x => x)))
            };

            var argsParserMock = new Mock<IValidationRuleArgsParser<ContentValidationLengthRuleArgs>>();
            argsParserMock.Setup(m => m.Parse(It.IsAny<object[]>()))
                .Returns(new ContentValidationLengthRuleArgs(0, 100));

            var rule = new ContentValidationLengthRule(argsParserMock.Object);

            var result = rule.IsApplicable(httpResponseMessage.Content.Headers);

            result.Should().BeTrue();
        }
    }

    public class ValidationRuleFactoryShould
    {
        [Fact]
        public void ReturnValidationRule()
        {
            var validationRuleMock = new Mock<IContentHeaderValidationRule>();

            validationRuleMock.SetupGet(m => m.RuleName).Returns("rule");

            var validators = new[] { validationRuleMock.Object };

            var factory = new ValidationRuleFactory(validators);

            var rule = factory.Create("rule", new object[0]);

            rule.Should().NotBeNull();
        }

        [Fact]
        public void ReturnValidationRuleWithArguments()
        {
            var expected = new object[] { 1, 2, 3 };

            var validationRuleMock = new Mock<IContentHeaderValidationRule>();

            validationRuleMock.SetupGet(m => m.RuleName).Returns("rule");
            validationRuleMock.SetupGet(m => m.Args).Returns(expected);

            var validators = new[] { validationRuleMock.Object };

            var factory = new ValidationRuleFactory(validators);

            var rule = factory.Create("rule", expected);

            rule.Args.Should().NotBeEmpty();
        }

        [Fact]
        public void ThrowErrorWhenRuleWithNameSpecifiedDoesNotExist()
        {
            var validationRuleMock = new Mock<IContentHeaderValidationRule>();

            validationRuleMock.SetupGet(m => m.RuleName).Returns("rule");

            var validators = new[] { validationRuleMock.Object };

            var factory = new ValidationRuleFactory(validators);

            Action action = () => factory.Create("rule1111", new object[0]);

            action.Should().Throw<InvalidOperationException>();
        }
    }

    public class OptionsParserShould
    {
        [Fact]
        public void ParseOneSingleRuleWithNoArgs()
        {
            object[] expected = {
                new RuleOptions("rule1", new object[0])
            };

            var parser = new OptionsParser();

            var actual = parser.Parse("rule1:");

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ParseOneSingleRuleWithOneArg()
        {
            object[] expected = {
                new RuleOptions("rule1", new object[] {$"{1}"})
            };

            var parser = new OptionsParser();

            var actual = parser.Parse("rule1:1");

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ParseOneSingleRuleWithTwoArgs()
        {
            object[] expected = {
                new RuleOptions("rule1", new object[] {$"{1}", $"{2}"})
            };

            var parser = new OptionsParser();

            var actual = parser.Parse("rule1:1,2");

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ParseManyRulesWithDifferentArgs()
        {
            var expected = new[]
            {
                new RuleOptions("rule1", new object[] {$"{1}", $"{2}"}),
                new RuleOptions("rule2", new object[] {$"{0}"}),
                new RuleOptions("rule3", new object[0])
            };

            var parser = new OptionsParser();

            var actual = parser.Parse("rule1:1,2;rule2:0;rule3:");

            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("rule:1,2")]
        [InlineData("rule:")]
        [InlineData("rule:1,2;rule:2,3")]
        [InlineData("rule:1,2;rule:;rule:1,2;rule:2,3")]
        public void GetResultWhenInputIsValid(string rule)
        {
            var parser = new OptionsParser();

            Action action = () => parser.Parse(rule);

            action.Should().NotThrow<ArgumentException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData("rule")]
        [InlineData("rule,1,2")]
        [InlineData("rule;rule2")]
        public void ThrowExceptionWhenInputIsInvalid(string rule)
        {
            var parser = new OptionsParser();

            Action action = () => parser.Parse(rule);

            action.Should().Throw<ArgumentException>();
        }
    }

    public class ContentValidationLengthRuleArgsParserShould
    {
        [Fact]
        public void ParseWhenInputIsValid()
        {
            var parser = new ContentValidationLengthRuleArgsParser();

            var result = parser.Parse(new object[] { "1", "2" });

            result.MinimumContentLength.Should().Be(1L);
            result.MaximumContentLength.Should().Be(2L);
        }

        [Theory]
        [MemberData(nameof(InvalidMinimum))]
        public void ReturnMinimumAsZeroWhenInputIsInvalid(object[] args)
        {
            var parser = new ContentValidationLengthRuleArgsParser();

            var result = parser.Parse(args);

            result.MinimumContentLength.Should().Be(0L);
        }

        [Theory]
        [MemberData(nameof(InvalidMaximum))]
        public void ReturnMaximumLongWhenInputIsInvalid(object[] args)
        {
            var parser = new ContentValidationLengthRuleArgsParser();

            var result = parser.Parse(args);

            result.MaximumContentLength.Should().Be(long.MaxValue);
        }

        public static IEnumerable<object[]> InvalidMinimum()
        {
            return new List<object[]>
            {
                new object[0],
                new object[] { new [] {"-100"} },
                new object[] { new [] {"100", "10"} }
            };
        }

        public static IEnumerable<object[]> InvalidMaximum()
        {
            return new List<object[]>
            {
                new object[0],
                new object[] { new [] {"-100"} },
                new object[] { new [] {"100"} },
                new object[] { new [] {"100", "10"} },
                new object[] { new [] {"100", "-10"} }
            };
        }
    }

    public class FilterLinksByHostRequestHandlerShould
    {
        [Fact]
        public async Task SelectAbsoluteLinksWithTheSameHost()
        {
            var links = new[] {"http://thesamehost.link/page", "http://thedifferenthost.link"};

            var handler = new FilterLinksByHostRequestHandler();

            var result = await handler.Handle(new FilterLinksByHostRequest(links, new Uri("http://thesamehost.link")),
                CancellationToken.None);

            result.Should().BeEquivalentTo(links.Take(1).Select(link => new Uri(link)));
        }

        [Fact]
        public async Task SelectRelativesLinksWithTheHost()
        {
            var links = new[] { "about:///page", "http://thedifferenthost.link" };

            var handler = new FilterLinksByHostRequestHandler();

            var result = await handler.Handle(new FilterLinksByHostRequest(links, new Uri("http://thesamehost.link")),
                CancellationToken.None);

            result.Should().BeEquivalentTo(links.Take(1).Select(link => new Uri("http://thesamehost.link/page")));
        }
    }

    public class ExcludeLinksFilterShould
    {
        [Fact]
        public void ExcludeLinkWithBlackListedWords()
        {
            var links = new[] { "http://thesamehost.link/page", "http://thedifferenthost.link" };

            var filter = new ExcludeLinksFilter();

            var result = filter.ApplyFilter(links, new ParsingOptions(null, new [] {"different"}, null));

            result.Should().BeEquivalentTo(links.Take(1));

        }
    }

    public class IncludeLinksFilterShould
    {
        [Fact]
        public void IncludeLinkWithWhiteListedWords()
        {
            var links = new[] { "http://thesamehost.link/page", "http://thedifferenthost.link" };

            var filter = new IncludeLinksFilter();

            var result = filter.ApplyFilter(links, new ParsingOptions(null, null, new[] { "same" }));

            result.Should().BeEquivalentTo(links.Take(1));

        }
    }
}