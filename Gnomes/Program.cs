using System;
using System.Collections.Generic;
using System.Globalization;

namespace Gnomes
{
    class Program
    {
        static void Main()
        {
            foreach(var g in new Gnome[] {new SeniorCitizen(), new Adult(), new Teenager(), new Child(), new Infant() }) 
            {
                Console.WriteLine("A {0}", g, CultureInfo.InvariantCulture);
            }
            Console.WriteLine();
            foreach(int i in new [] {100, 200, 300, 400, 500, 600, 700, 800, 900, 1000})
            {
                var s = new SeatingStrategy(); 
                Boat.MaxBoatWeight = i;
                Console.WriteLine("What if the boat could hold {0} grams?", Boat.MaxBoatWeight, CultureInfo.InvariantCulture);
                List<Boat> boats =  s.Execute();
                PrintResults(boats);
            }          
        }

        static void PrintResults(List<Boat> boats)
        {
            Console.WriteLine(" {0,7} | {1,7} | {2,7} | {3,7} | {4,7} | {5,7}", "#", "Senior", "Adult", "Teen", "Child", "Infant");
            Console.WriteLine(new string('-', 59));
            int comboNumber = 1;
            boats.ForEach(b => Console.WriteLine(" {0,7} | {1}", comboNumber++.ToString("N0", CultureInfo.InvariantCulture), b));

            Console.WriteLine();
        }
    }
}
