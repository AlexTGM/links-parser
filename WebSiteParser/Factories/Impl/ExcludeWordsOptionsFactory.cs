using System.Collections.Generic;
using WebSiteParser.Requests;

namespace WebSiteParser.Factories.Impl
{
    public class ExcludeWordsOptionsFactory 
        : IParsingOptionsFactory
    {
        public string Name => "exclude";

        public ParsingOptions Patch(ParsingOptions options, IEnumerable<string> values)
        {
            options.ExcludeWords = values;

            return options;
        }
    }
}