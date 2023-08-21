using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMigrator
{
    public class CommandLineOptions
    {
        [Option("dryrun" , Required = false, HelpText = "Without migration")]
        public bool DryRun { get; set; }

        [Option('c', "connection", Required = false, HelpText = "Connection string")]
        public string? ConnectionString { get;set; }
    }
}
