using System.Collections.Generic;
using WebSiteParser.Requests;

namespace WebSiteParser.Factories
{
    public interface IParsingOptionsFactory
    {
        string Name { get; }

        ParsingOptions Patch(ParsingOptions options, IEnumerable<string> values);
    }
}