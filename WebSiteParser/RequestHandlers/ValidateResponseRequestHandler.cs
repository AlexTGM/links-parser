using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebSiteParser.Requests;
using WebSiteParser.Rules.Infrastructure;

namespace WebSiteParser.RequestHandlers
{
    public class ValidateResponseRequestHandler : IRequestHandler<ValidateResponseRequest, bool>
    {
        private readonly IMediator _mediator;
        private readonly ISiteRulesValidator _siteRulesValidator;

        public ValidateResponseRequestHandler(ISiteRulesValidator siteRulesValidator, IMediator mediator)
        {
            _siteRulesValidator = siteRulesValidator;
            _mediator = mediator;
        }

        public async Task<bool> Handle(ValidateResponseRequest request, CancellationToken cancellationToken)
        {
            var options = request.Options;
            var getRules = new GetValidationRulesListRequest(options.ContentValidationRules,
                options.ResponseValidationRules);
            var rules = await _mediator.Send(getRules, cancellationToken);

            return _siteRulesValidator.Validate(request.Response, rules);
        }
    }
}