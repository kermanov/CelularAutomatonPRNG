using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace CellularAutomatonPRNG.Program
{
    public class Options
    {
        [Option('k', "kilobytes", Required = true, HelpText = "Set output size in kilobytes.")]
        public int OutputSizeKilobytes { get; set; }

        [Option('r', "rule", Required = true, HelpText = "Set which automaton rule to use.")]
        public int Rule { get; set; }

        [Option('s', "seed", Required = true, HelpText = "Set seed.")]
        public int Seed { get; set; }

        [Option('f', "file", Required = true, HelpText = "Set output file.")]
        public string FileName { get; set; }

        [Option('d', "default", Required = false, HelpText = "Use default random.")]
        public bool UseDefaultRandom { get; set; }
    }
}