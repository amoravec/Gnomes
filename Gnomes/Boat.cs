using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Gnomes
{
    class Boat : IEquatable<Boat>, IComparable<Boat>
    {

        public static int MaxBoatWeight = 100;

        private readonly List<Gnome> seatedGnomes;

        public Boat()
        {
            seatedGnomes = new List<Gnome>();
        }

        public Boat(List<Gnome> gnomes) : this()
        {
            AddGnomes(gnomes);
        }

        private bool CanAddGnome(Gnome gnome)
        {
            if (IsFull)
            {
                return false;
            }

            if ((TotalWeight + gnome.Weight) > MaxBoatWeight)
            {
                return false;
            }

            return true;
        }

        public bool AddGnome(Gnome gnome)
        {
            if (CanAddGnome(gnome))
            {
                seatedGnomes.Add(gnome);
                return true;
            }
            return false;
        }

        public bool AddGnomes(List<Gnome> gnomes)
        {
            if(gnomes.Sum(g => g.Weight) <= MaxBoatWeight)
            {
                gnomes.ForEach(g => AddGnome(g));
                return true;
            }

            return false;
        }

        public bool IsFull
        {
            get
            {
                return TotalWeight == MaxBoatWeight;
            }
        }

        private int TotalWeight { 
            get
            {
                int totalWeight = 0;
                foreach(Gnome g in seatedGnomes)
                {
                    totalWeight = totalWeight + g.Weight;
                }

                return totalWeight;
            }
        }

        public override int GetHashCode()
        {
            return ToString(CultureInfo.InvariantCulture).GetHashCode(StringComparison.Ordinal);
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, "{0,7} | {1,7} | {2,7} | {3,7} | {4,7}", seatedGnomes.OfType<SeniorCitizen>().Count().ToString("N0", formatProvider),
                seatedGnomes.OfType<Adult>().Count().ToString("N0", formatProvider),
                seatedGnomes.OfType<Teenager>().Count().ToString("N0", formatProvider),
                seatedGnomes.OfType<Child>().Count().ToString("N0", formatProvider),
                seatedGnomes.OfType<Infant>().Count().ToString("N0", formatProvider));
        }

        public override string ToString()
        {
            return ToString(CultureInfo.InvariantCulture);
        }

        public int GetNumberOf<T>() where T : Gnome
        {
            return seatedGnomes.OfType<T>().Count();
        }

        public bool Equals(Boat other)
        {
            if(null == other)
            {
                return false;
            }
            return ToString(CultureInfo.InvariantCulture).Equals(other.ToString(CultureInfo.InvariantCulture), StringComparison.Ordinal);
        }

        public int CompareTo(Boat other)
        {
            int result = other.GetNumberOf<SeniorCitizen>().CompareTo(GetNumberOf<SeniorCitizen>());
            if(result != 0)
            {
                return result;
            }

            result = other.GetNumberOf<Adult>().CompareTo(GetNumberOf<Adult>());
            if (result != 0)
            {
                return result;
            }

            result = other.GetNumberOf<Teenager>().CompareTo(GetNumberOf<Teenager>());
            if (result != 0)
            {
                return result;
            }

            result = other.GetNumberOf<Child>().CompareTo(GetNumberOf<Child>());
            if (result != 0)
            {
                return result;
            }

            result = other.GetNumberOf<Infant>().CompareTo(GetNumberOf<Infant>());
            if (result != 0)
            {
                return result;
            }

            return 0;

        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Boat);
        }
    }
}
