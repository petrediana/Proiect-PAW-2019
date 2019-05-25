using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPaw
{
    public class Indicatori :IComparable<Indicatori>, ICloneable
    {
        public double TerenArabil { get; set; }
        public double Vii { get; set; }
        public double Livezi { get; set; }
        public double Pasuni { get; set; }

        public Indicatori() { }
        public Indicatori(double terenA, double vii, double livezi, double pasuni)
        {
            TerenArabil = terenA;
            Vii = vii;
            Livezi = livezi;
            Pasuni = pasuni;
        }

        public string AfiseazaValori()
        {
            return string.Format("{0},{1},{2},{3}", TerenArabil, Vii, Livezi, Pasuni);
        }

        public override string ToString()
        {
            return string.Format("TerenArabil: {0} ha, Vii: {1} ha, Livezi: {2} ha, Pasuni: {3} ha",
                TerenArabil, Vii, Livezi, Pasuni);
        }

        public int CompareTo(Indicatori other)
        {
            double sumaIndicatoriCurenti = TerenArabil + Vii + Livezi + Pasuni;
            double sumaAltiIndicatori = other.TerenArabil + other.Vii + other.Livezi + other.Pasuni;

            return sumaIndicatoriCurenti < sumaAltiIndicatori ? -1 : 1;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public static Indicatori operator+ (Indicatori indicatori1, Indicatori indicatori2)
        {
            Indicatori indNou = new Indicatori();
            indNou.TerenArabil = indicatori1.TerenArabil + indicatori2.TerenArabil;
            indNou.Vii = indicatori1.Vii + indicatori2.Vii;
            indNou.Livezi = indicatori1.Livezi + indicatori2.Livezi;
            indNou.Pasuni = indicatori1.Pasuni + indicatori2.Pasuni;

            return indNou;
        }

        public static Indicatori operator -(Indicatori indicatori1, Indicatori indicatori2)
        {
            Indicatori indNou = new Indicatori();
            indNou.TerenArabil = indicatori1.TerenArabil - indicatori2.TerenArabil;
            indNou.Vii = indicatori1.Vii - indicatori2.Vii;
            indNou.Livezi = indicatori1.Livezi - indicatori2.Livezi;
            indNou.Pasuni = indicatori1.Pasuni - indicatori2.Pasuni;

            return indNou;
        }
    }
}
