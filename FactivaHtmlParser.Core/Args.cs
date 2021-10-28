using CommandLine;

namespace FactivaHtmlParser.Core
{
    public class Options
    {
        [Option('p', "path", Required = true, HelpText = "Set path where factiva html files would be searched.")]
        public string Path { get; set; }

        [Option('d', "depth", Required = false, Default = 2,
            HelpText = "Set depth of finding folders with factiva html data.")]
        public int Depth { get; set; }
    }
}