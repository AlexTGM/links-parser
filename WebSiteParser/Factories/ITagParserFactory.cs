using WebSiteParser.Parsers.Tags;

namespace WebSiteParser.Factories
{
    public interface ITagParserFactory
    {
        ITagParser Create(string name);
    }
}