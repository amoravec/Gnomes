using System;
using System.Globalization;

namespace Gnomes
{
    abstract class Gnome
    {
        public abstract int Weight { get; }

        string Name => GetType().Name;

        public override string ToString() => string.Format(CultureInfo.InvariantCulture, "{0} weighing {1} grams", this.Name, this.Weight);
    }

    class SeniorCitizen : Gnome
    {
        public override int Weight => 60;
    }

    class Adult : Gnome
    {
        public override int Weight => 40;
    }

    class Teenager : Gnome
    {
        public override int Weight => 20;
    }

    class Child : Gnome
    {
        public override int Weight => 10;
    }

    class Infant : Gnome
    {
        public override int Weight => 5;
    }
}
