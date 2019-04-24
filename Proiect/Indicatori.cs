using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPaw
{
    public class Indicatori
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

        public override string ToString()
        {
            return string.Format("TerenArabil: {0} ha, Vii: {1} ha, Livezi: {2} ha, Pasuni: {3} ha",
                TerenArabil, Vii, Livezi, Pasuni);
        }
    }
}
