using System.IO;
using System.Collections.Generic;
using System;
using System.Diagnostics;

namespace Binary
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch st = new Stopwatch();
            st.Start();
            List <byte []> data = new List<byte []>();
            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open("input.dat", FileMode.Open)))
                {
                    Console.Write("Чтение файла ...");
                    var progress = new ProgressBar();
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        data.Add(reader.ReadBytes(1024));
                        progress.Report((double)reader.BaseStream.Position / (double)reader.BaseStream.Length);
                    }
                    progress.Dispose();
                }
                data.Reverse();
                using (BinaryWriter write = new BinaryWriter(File.Open("output.dat", FileMode.OpenOrCreate)))
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        var temp = data[i];
                        Array.Reverse(temp);
                        write.Write(temp);
                    }
                }

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            st.Stop();
            Console.WriteLine("\n\rВремя выполнения программы: {0:0.000} секунд", st.Elapsed.TotalSeconds);
            Console.ReadKey();
            
        }
    }
}
