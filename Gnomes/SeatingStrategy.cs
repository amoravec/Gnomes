using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Gnomes
{
    class SeatingStrategy
    {

        private ConcurrentBag<Boat> seatedBoats = new ConcurrentBag<Boat>();

        SeniorCitizen seniorCitizen = new SeniorCitizen();
        Adult adult = new Adult();
        Teenager teenager = new Teenager();
        Child child = new Child();
        Infant infant = new Infant();

        private IList<T> CreateANumberOfGnomes<T>(double number) where T : Gnome
        {
            IList<T> listOfNewGnomes = new List<T>();
            for (int i = 0; i < number; i++) {
                listOfNewGnomes.Add(Activator.CreateInstance<T>());
            }

            return listOfNewGnomes;
        }


        public List<Boat> Execute()
        {
            long possibleCombinations = 0;

            Stopwatch stopwatch = Stopwatch.StartNew();

            Parallel.For(0, Convert.ToInt32(Math.Ceiling(Boat.MaxBoatWeight / (double)seniorCitizen.Weight)),
                s =>
                {

                    for (int a = 0; a <= Math.Floor(Boat.MaxBoatWeight / (double)adult.Weight); a++)
                    {
                        for (int t = 0; t <= Math.Floor(Boat.MaxBoatWeight / (double)teenager.Weight); t++)
                        {
                            for (int c = 0; c <= Math.Floor(Boat.MaxBoatWeight / (double)child.Weight); c++)
                            {
                                for (int i = 0; i <= Math.Floor(Boat.MaxBoatWeight / (double)infant.Weight); i=i+2)
                                {
                                    possibleCombinations++;

                                    // if the numbers don't add up, don't create a model
                                    if (((s * seniorCitizen.Weight) +
                                         (a * adult.Weight) +
                                         (t * teenager.Weight) +
                                         (c * child.Weight) +
                                         (i * infant.Weight))
                                        != Boat.MaxBoatWeight)
                                    {
                                        continue;
                                    }

                                    List<Gnome> candidateList = new List<Gnome>();
                                    candidateList.AddRange(CreateANumberOfGnomes<SeniorCitizen>(s));
                                    candidateList.AddRange(CreateANumberOfGnomes<Adult>(a));
                                    candidateList.AddRange(CreateANumberOfGnomes<Teenager>(t));
                                    candidateList.AddRange(CreateANumberOfGnomes<Child>(c));
                                    candidateList.AddRange(CreateANumberOfGnomes<Infant>(i));

                                    seatedBoats.Add(new Boat(candidateList));
                                }
                            }
                        }
                    }
                });

            stopwatch.Stop();

            Console.WriteLine();
            Console.WriteLine("Candidates Evaluated: {0:n0} in {1}", possibleCombinations, stopwatch.Elapsed);
            Console.WriteLine("Unique Boat Combos Found: {0:n0}", seatedBoats.Count);

            stopwatch = Stopwatch.StartNew();
            List<Boat> sortedBoats = seatedBoats.ToList();
            sortedBoats.Sort();
            stopwatch.Stop();

            Console.WriteLine("Sorted List in {0}", stopwatch.Elapsed);
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine("Press <ENTER> to continue.");
            Console.ReadLine();

            return sortedBoats;
            
        }
    }
}
