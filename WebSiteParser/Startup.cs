using AngleSharp.Html.Parser;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebSiteParser.Controllers;
using WebSiteParser.Factories;
using WebSiteParser.Factories.Impl;
using WebSiteParser.Parsers;
using WebSiteParser.Parsers.Options;
using WebSiteParser.Parsers.Tags;
using WebSiteParser.RequestHandlers;
using WebSiteParser.Rules.ContentValidationLengthRule;
using WebSiteParser.Rules.Infrastructure;
using WebSiteParser.Rules.ServerValidationRule;

namespace WebSiteParser
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMediatR();

            services.AddHttpClient<WebSiteDownloadRequestHandler>();
            services.AddSingleton<IHtmlParser, HtmlParser>();
            services.AddSingleton<ISiteRulesValidator, SiteRulesValidator>();
            services.AddSingleton<IOptionsParser, OptionsParser>();

            services.AddSingleton<ITagParser, ImageTagParser>();
            services.AddSingleton<ITagParser, AnchorTagParser>();

            services.AddSingleton<ITagParserFactory, TagParserFactory>();

            services.AddSingleton<IParsingOptionsFactory, TagParsingOptionsFactory>();
            services.AddSingleton<IParsingOptionsFactory, ExcludeWordsOptionsFactory>();
            services.AddSingleton<IParsingOptionsFactory, IncludeWordsOptionsFactory>();

            services.AddSingleton<ILinksListFilter, IncludeLinksFilter>();
            services.AddSingleton<ILinksListFilter, ExcludeLinksFilter>();

            AddValidation(services);
        }

        private void AddValidation(IServiceCollection services)
        {
            services.AddSingleton<IValidationRuleFactory, ValidationRuleFactory>();

            services.AddSingleton<IValidationRule, ServerValidationRule>();
            services
                .AddSingleton<IValidationRuleArgsParser<ServerValidationRuleArgs>, ServerValidationRuleArgsParser>();

            services.AddSingleton<IValidationRule, ContentValidationLengthRule>();
            services
                .AddSingleton<IValidationRuleArgsParser<ContentValidationLengthRuleArgs>,
                    ContentValidationLengthRuleArgsParser>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseMvc();
        }
    }
}