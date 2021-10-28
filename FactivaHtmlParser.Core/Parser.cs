using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;

namespace FactivaHtmlParser.Core
{
    public class Parser
    {
        public async Task ParseAsync(string path, int depth, string[] extensions)
        {
            var directories = DirectoryParser.GetDirectories(path, depth, extensions);
            foreach (var directory in directories)
            {
                var files = Directory.EnumerateFiles(directory)
                    .Where(c => extensions.Any(ext => c.ToLower().EndsWith(ext)));

                var outPath = Path.Combine(directory, "out");
                Directory.CreateDirectory(outPath);

                Directory.CreateDirectory(Path.Combine(directory, "tmlda"));
                Directory.CreateDirectory(Path.Combine(directory, "concl"));

                var config = Configuration.Default;
                var context = BrowsingContext.New(config);

                foreach (var file in files)
                {
                    var text = await File.ReadAllTextAsync(file);
                    var document = await context.OpenAsync(req => req.Content(text));
                    var articlesElements = document.QuerySelectorAll(".article.enArticle");
                    var arts = new List<Article>();
                    foreach (IElement element in articlesElements)
                    {
                        var art = new Article
                        {
                            Id = element.ParentElement.Id,
                            Title = element.QuerySelector(".enHeadline").TextContent
                        };

                        if (art.Title.StartsWith("Summary of"))
                            continue;

                        art.Headings = element.QuerySelectorAll("div:not(#hd)")
                            .Select(c => c.TextContent)
                            .ToList();

                        var p = element.QuerySelectorAll("p")
                            .Select(c => c.Text())
                            .Where(c => c.Length > 1);

                        art.By = p.FirstOrDefault();
                        p = p.Skip(1);
                        art.Text = string.Join('\n', p.SkipLast(3));
                        art.SystemInfo = string.Join('\n', p.TakeLast(3));
                        arts.Add(art);

                        await File.WriteAllTextAsync(Path.Combine(outPath, $"{art.Id}.txt"), art.Text);
                    }
                }
            }
        }
    }
}