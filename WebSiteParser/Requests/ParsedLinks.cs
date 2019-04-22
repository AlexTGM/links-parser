using System;
using System.Collections.Generic;

namespace WebSiteParser.Requests
{
    public class ParsedLinks
    {
        public ParsedLinks(List<string> links, IEnumerable<Uri> absoluteLinks, IEnumerable<Uri> relativeLinks)
        {
            Links = links;
            AbsoluteLinks = absoluteLinks;
            RelativeLinks = relativeLinks;
        }

        public List<string> Links { get; set; }
        public IEnumerable<Uri> AbsoluteLinks { get; set; }
        public IEnumerable<Uri> RelativeLinks { get; set; }
    }
}