using System.Collections.Generic;

namespace FactivaHtmlParser.Core
{
    public class Article
    {
        public string Id { get; set; }
        public List<string> Headings { get; set; }= new List<string>();
        public string SystemInfo { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public string By { get; set; }
    }
}