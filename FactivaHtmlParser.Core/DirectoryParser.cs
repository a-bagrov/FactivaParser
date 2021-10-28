using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FactivaHtmlParser.Core
{
    public static class DirectoryParser
    {
        public static IEnumerable<string> GetDirectories(string startPath, int depth, string[] extensions)
            => GetDirectories(startPath, 0, depth, extensions);

        private static IEnumerable<string> GetDirectories(string startPath, int currentDepth, int maxDepth,
            string[] extensions)
        {
            if (currentDepth > maxDepth)
                yield break;

            foreach (var path in Directory.EnumerateDirectories(startPath))
            {
                if (Directory.EnumerateFiles(path).Any(c => extensions.Any(ext => c.EndsWith(ext))))
                    yield return path;

                foreach (var childPath in GetDirectories(path, currentDepth + 1, maxDepth, extensions))
                    yield return childPath;
            }
        }
    }
}