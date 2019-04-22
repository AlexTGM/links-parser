namespace WebSiteParser.Controllers.Requests
{
    public class ParseRequest
    {
        public string Url { get; set; }

        public string ContentValidationRules { get; set; }
        public string ResponseValidationRules { get; set; }
        public string ParsingRules { get; set; }

        public int MaxDepth { get; set; }
    }
}