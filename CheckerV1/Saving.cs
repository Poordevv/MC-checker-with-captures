using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckerV1
{
    class Saving
    {

        static object LOCKER = new object();

        public static void Save(string Combo, string capturedUsername, int file, string message)
        {
            if (file == 1)
            {
                lock (LOCKER)
                {
                    using (FileStream textDocument = new FileStream("MinecraftHits.txt", FileMode.Append, FileAccess.Write, FileShare.None))
                    {
                        using (StreamWriter writer = new StreamWriter(textDocument))
                        {
                            writer.WriteLine($"[NFA] - {Combo} | Name: {capturedUsername}");

                            textDocument.Flush();

                        }
                    }
                }
            }
            else if (file == 2)
            {

            }
            else if (file == 3)
            {

            }
        }
    }
}
