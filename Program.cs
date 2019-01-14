using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Postevanka
{
    class Program
    {
        // ******************************************* //

        private static readonly bool showImmediateSuccess = true;
        private static readonly bool showImmediateTime = false;

        private static readonly int iterations = 20;  // število računov
        
        private static int[] ss = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; // poštevanka števil X,...        
        //private static int[] os = { 0 }; // 0 = množenje, 1 = deljenje, 2 = seštevanje, 3 = odštevanje
        private static int[] os = { 0 }; // 

        // ******************************************* //

        static void Main(string[] args)
        {
            FullScreen();

            Console.WriteLine("Pritisni ENTER za začetek!");

            Console.ReadLine();           

            var rand = new Random();

            // množenje
            var pravilno = 0;
            var w = new Stopwatch();
            w.Start();

            for (int i = 0; i < iterations; i++)
            {
                int x = ss[rand.Next(ss.Length)];
                int y = ss[rand.Next(ss.Length)];
                
                int o = os[rand.Next(os.Length)];

                var w2 = new Stopwatch();
                w2.Start();

                if (o == 0)
                {
                    if (rand.Next(2) == 0)
                    {
                        var t = x;
                        x = y;
                        y = t;
                    }                    
                    Console.Write(x.ToString().PadLeft(2, ' ') + " × " + y.ToString().PadRight(2, ' ') + " = ");                    
                } else if (o == 1)
                {
                    Console.Write((x * y).ToString().PadLeft(2, ' ') + " : " + y.ToString().PadRight(2, ' ') + " = ");

                } else if (o == 2)
                {
                    Console.Write(x.ToString().PadLeft(2, ' ') + " + " + y.ToString().PadRight(2, ' ') + " = ");
                } else if (o == 3)
                {
                    Console.Write((x + y).ToString().PadLeft(2, ' ') + " - " + y.ToString().PadRight(2, ' ') + " = ");
                }
                var zs = Console.ReadLine();

                w2.Stop();

                Console.SetCursorPosition(15, Console.CursorTop - 1);

                if (!int.TryParse(zs, out int z))
                    Console.Write("  Nisi vpisal številke!");
                else
                {
                    if (o == 0)
                    {
                        if (x * y == z)
                        {
                            if (showImmediateSuccess) Console.Write("  " + GetBravo());
                            pravilno++;
                        }
                        else
                        {
                            if (showImmediateSuccess) Console.Write("  Buuuuu! Pravilno je " + (x * y) + ".");
                        }
                    }
                    else if (o == 1)
                    {
                        if (x == z)
                        {
                            if (showImmediateSuccess) Console.Write("  " + GetBravo());
                            pravilno++;
                        }
                        else
                        {
                            if (showImmediateSuccess) Console.Write("  Buuuuu! Pravilno je " + x + ".");

                        }
                    }
                    else if (o == 2)
                    {
                        if (x + y == z)
                        {
                            if (showImmediateSuccess) Console.Write("  " + GetBravo());
                            pravilno++;
                        }
                        else
                        {
                            if (showImmediateSuccess) Console.Write("  Buuuuu! Pravilno je " + (x + y) + ".");
                        }
                    }
                    else if (o == 3)
                    {
                        if (x == z)
                        {
                            if (showImmediateSuccess) Console.Write("  " + GetBravo());
                            pravilno++;
                        }
                        else
                        {
                            if (showImmediateSuccess) Console.Write("  Buuuuu! Pravilno je " + x + ".");
                        }
                    }
                }
                Console.WriteLine();

                if (showImmediateTime)
                    Console.WriteLine(" (porabil si " + Math.Round(w2.Elapsed.TotalSeconds, 1) + " sek.)");
            }
            w.Stop();

            Console.WriteLine();
            Console.WriteLine("Pravilnih odgovorov: " + pravilno);
            Console.ReadLine();

            Console.WriteLine("Porabljen čas: " + Math.Round(w.Elapsed.TotalSeconds, 0) + " sekund");

            Console.ReadLine();
        }

        private static string GetBravo()
        {
            string[] bravo = {
                "Bravo!", "Seveda, stari!", "Prav imaš, model.", "Pa ti razturaš!",
                "Jup!", "Pravilno!", "Uau, bejbika!", "Kaj si ja nor, prav imaš!", "Odlično, bela rit!",
                "Slaveeeeeeeeeeeeen siiiii!", "Mislil sem, da je narobe!", "Norc si!",
                "S tabo grem na tekmovanje!", "Pravilno je res to kar si napisow, ne 48!", "Wat, ti maš prow?!","Mhm!","Wooooooow"
            };
            var r = new Random();
            return bravo[r.Next(bravo.Length)];
        }

        public static object DllImports { get; private set; }

        [StructLayout(LayoutKind.Sequential)]
        public struct COORD
        {

            public short X;
            public short Y;
            public COORD(short x, short y)
            {
                this.X = x;
                this.Y = y;
            }

        }
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetStdHandle(int handle);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleDisplayMode(
            IntPtr ConsoleOutput
            , uint Flags
            , out COORD NewScreenBufferDimensions
            );

        private static void FullScreen()
        {
            IntPtr hConsole = GetStdHandle(-11);   // get console handle
            COORD xy = new COORD(100, 100);
            SetConsoleDisplayMode(hConsole, 1, out xy); // set the console to fullscreen
        }
    }
}