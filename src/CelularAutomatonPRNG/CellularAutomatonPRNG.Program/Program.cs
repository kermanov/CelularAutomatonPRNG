using CelularAutomatonPRNG;
using CommandLine;
using System;
using System.IO;
using System.Linq;

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
                        byte[] bytes;
                        if (!o.UseDefaultRandom)
                        {
                            var random = new CellularAutomatonRandom((byte)o.Rule, o.Seed);
                            bytes = new byte[o.OutputSizeKilobytes * 1024];
                            random.GetBytes(bytes);
                        }
                        else
                        {
                            bytes = GetStandardRandomBytes(o.OutputSizeKilobytes * 1024, o.Seed);
                        }

                        WriteToFile(bytes, o.FileName);

                        var mode = o.UseDefaultRandom ? "default" : "celular_automaton";
                        Console.WriteLine($"Done. File '{o.FileName}' created, mode: {mode}, size: {o.OutputSizeKilobytes} KB.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                });
        }

        static void WriteToFile(byte[] bytes, string fileName)
        {
            var fileType = fileName.Split('.').Last();
            if (fileType == "bin")
            {
                File.WriteAllBytes(fileName, bytes);
            }
            else if (fileType == "txt")
            {
                File.WriteAllText(fileName, BitConverter.ToString(bytes).Replace("-", "").ToLower());
            }
            else
            {
                Console.WriteLine($"Unknown file type: '{fileType}'.");
            }
        }

        static byte[] GetStandardRandomBytes(int count, int seed)
        {
            var random = new Random(seed);
            var bytes = new byte[count];
            random.NextBytes(bytes);
            return bytes;
        }
    }
}
