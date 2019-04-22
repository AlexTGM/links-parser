namespace WebSiteParser.Requests
{
    public class Options
    {
        public Options(string parsingRules, string contentValidationRules, string responseValidationRules)
        {
            ParsingRules = parsingRules;
            ContentValidationRules = contentValidationRules;
            ResponseValidationRules = responseValidationRules;
        }

        public string ParsingRules { get; set; }
        public string ContentValidationRules { get; set; }
        public string ResponseValidationRules { get; set; }
    }
}