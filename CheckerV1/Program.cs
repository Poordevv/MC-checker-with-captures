using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckerV1
{
    class Program
    {

        static void Main(string[] args)
        {

            CheckerV1.Loading.LoadFiles();
            Console.Title = "ASCII Art";
            Console.ForegroundColor = ConsoleColor.Green;
            string title = @"
 
                          ¦¦¦+   ¦¦¦+¦¦+¦¦¦+   ¦¦+¦¦¦¦¦¦¦+ ¦¦¦¦¦¦+¦¦¦¦¦¦+  ¦¦¦¦¦+ ¦¦¦¦¦¦¦+¦¦¦¦¦¦¦¦+
                          ¦¦¦¦+ ¦¦¦¦¦¦¦¦¦¦¦¦+  ¦¦¦¦¦+----+¦¦+----+¦¦+--¦¦+¦¦+--¦¦+¦¦+----++--¦¦+--+
                          ¦¦+¦¦¦¦+¦¦¦¦¦¦¦¦+¦¦+ ¦¦¦¦¦¦¦¦+  ¦¦¦     ¦¦¦¦¦¦++¦¦¦¦¦¦¦¦¦¦¦¦¦+     ¦¦¦   
                          ¦¦¦+¦¦++¦¦¦¦¦¦¦¦¦+¦¦+¦¦¦¦¦+--+  ¦¦¦     ¦¦+--¦¦+¦¦+--¦¦¦¦¦+--+     ¦¦¦   
                          ¦¦¦ +-+ ¦¦¦¦¦¦¦¦¦ +¦¦¦¦¦¦¦¦¦¦¦¦++¦¦¦¦¦¦+¦¦¦  ¦¦¦¦¦¦  ¦¦¦¦¦¦        ¦¦¦   
                          +-+     +-++-++-+  +---++------+ +-----++-+  +-++-+  +-++-+        +-+  
                                                                                            
                                                                         [Created by Poordev]";

            Console.WriteLine(title);


            Thread menuUpdater;
            menuUpdater = new Thread(new ThreadStart(CPM));
            menuUpdater.Start();

            string value;
            int count;


            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("-> Input Thread Counts [Recommended Amount: 100+]: ");
            Console.ResetColor();

            value = Console.ReadLine();
            count = Convert.ToInt32(value);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("-> Enter '1' to proceed cracking accounts. Enter '2' to quit the application: ");
            Console.ResetColor();

            Threading.Initialize(count, Request.Start);

            var test = Console.ReadKey();
            if (test.Key == ConsoleKey.D2)
            {
                Environment.Exit(1);
            }
        }


        static void CPM()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            while (true)
            {
                if (sw.ElapsedMilliseconds > 10000 && sw.ElapsedMilliseconds < 10500)
                {
                    Request.CPM = Request.CPMTimer * 6;
                    Request.CPMTimer = 0;
                    sw.Reset();
                    sw.Start();
                }

                Console.Title = "Poordev's Minecraft Cracker ¬ Hits: " + Request.Hits + " || Progression: " + Request.Checked + "/" + Loading.comboList.Count() + " || [CPM: " + Request.CPM + " ]";
            }


        }
    }
}
