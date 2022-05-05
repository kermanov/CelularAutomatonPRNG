using CelularAutomatonPRNG;
using CommandLine;
using System;
using System.IO;

namespace CellularAutomatonPRNG.Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o =>
                {
                    if (o.Rule < 0 || o.Rule > 255)
                    {
                        Console.WriteLine("Invalid rule value.");
                        return;
                    }

                    try
                    {
                        var random = new CellularAutomatonRandom((byte)o.Rule, o.Seed);
                        var bytes = random.GetBytes(o.OutputSizeKilobytes * 1024);
                        File.WriteAllBytes(o.FileName, bytes);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                });
        }
    }
}
