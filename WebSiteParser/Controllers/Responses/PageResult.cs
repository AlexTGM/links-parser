using System.Collections.Generic;

namespace WebSiteParser.Controllers.Responses
{
    public class PageResult
    {
        public PageResult(string page,
            IEnumerable<string> links = null,
            IEnumerable<PageResult> pages = null)
        {
            Page = page;
            Links = links;
            Pages = pages;
        }

        public string Page { get; set; }

        public IEnumerable<string> Links { get; set; }

        public IEnumerable<PageResult> Pages { get; set; }
    }
}