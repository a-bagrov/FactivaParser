using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CommandLine;

namespace FactivaHtmlParser.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            var extensions = new[] {".html"};
            
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed(async o => await new Parser().ParseAsync(o.Path, o.Depth, extensions))
                .WithNotParsed(c =>
                {
                    Console.WriteLine(string.Join("\n", c));
                    Console.ReadLine();
                    Environment.Exit(1);
                });
        }
    }
}