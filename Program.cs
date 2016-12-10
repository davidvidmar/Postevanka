using System;
using System.Diagnostics;

namespace Postevanka
{
    class Program
    {

        // ******************************* //
        int[] s = { 2, 3, 4, 5 };
        // ******************************* //

        static void Main(string[] args)
        {            
            Console.WriteLine("Pritisni ENTER za začetek!");
            Console.ReadLine();           

            var rand = new Random();

            // množenje
            var pravilno = 0;
            var w = new Stopwatch();
            w.Start();

            for (int i = 0; i < 20; i++)
            {
                int x = s[rand.Next(s.Length)];
                int y = rand.Next(7) + 3;

                if (rand.Next(1) == 0)
                {

                    var t = x;
                    x = y;
                    y = t;
                }

                var w2 = new Stopwatch();
                w2.Start();
                Console.Write(x.ToString().PadLeft(2, ' ') + " * " + y.ToString().PadRight(2, ' ') + " = ");
                var zs = Console.ReadLine();
                w2.Stop();

                Console.SetCursorPosition(15, Console.CursorTop - 1);

                int z;
                if (!int.TryParse(zs, out z))
                    Console.Write("  Nisi vpisal številke!");
                else 
                    if (x * y == z)
                    {
                        
                        Console.Write("  Bravo!");
                        pravilno++;
                    }
                    else
                    {
                        Console.Write("  Buuuuu! Pravilno je " + x * y + ".");
                    }
                Console.WriteLine(" (porabil si " + Math.Round(w2.Elapsed.TotalSeconds, 1) + " sek.)");
            }
            w.Stop();

            Console.WriteLine();
            Console.WriteLine("Pravilnih odgovorov: " + pravilno);
            Console.WriteLine("Porabljen čas: " + Math.Round(w.Elapsed.TotalSeconds, 0) + " sekund");

            Console.ReadLine();
        }
    }
}
