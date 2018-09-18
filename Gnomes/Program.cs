using System;
using System.Collections.Generic;
using System.Globalization;

namespace Gnomes
{
    class Program
    {
        static void Main()
        {
            SeatingStrategy s = new SeatingStrategy();
            List<Boat> boats =  s.Execute();

            Console.WriteLine(" {0,7} | {1,7} | {2,7} | {3,7} | {4,7} | {5,7}", "#", "Senior", "Adult", "Teen", "Child", "Infant");
            Console.WriteLine(new string('-', 59));
            int comboNumber = 1;
            boats.ForEach(b => Console.WriteLine(" {0,7} | {1}", comboNumber++.ToString("N0", CultureInfo.InvariantCulture), b));

            Console.WriteLine();
            Console.WriteLine("Press <ENTER> to exit.");
            Console.ReadLine();
        }
    }
}
