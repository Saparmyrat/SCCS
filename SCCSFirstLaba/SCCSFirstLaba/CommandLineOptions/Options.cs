using CommandLine;

namespace SCCSFirstLaba.CommandLineOptions
{
    public class Options
    {
        [Option('i', "input", Required = true, HelpText = "Input file name")]
        public string InputFile { get; set; }
    
        [Option('o', "output", Required = true, HelpText = "Output file name")]
        public string OutputFile { get; set; }
        
        [Option('f', "fileType", Required = true, HelpText = "Type of file")]
        public string FileType { get; set; }
    }
}
