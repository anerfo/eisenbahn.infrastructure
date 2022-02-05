using CommandLine;

namespace Browser
{
    public class Options
    {
        [Option('u', "uri", Required=true, HelpText="The uri to navigate to.")]
        public string Uri { get; set; }

        [Option('t', "title", Required = false, HelpText = "The browser window title.")]
        public string Title { get; set; }

        [Option('e', "events", Required = false, HelpText = "Address to send browser events to.")]
        public string EventAddress { get; set; }
    }
}
