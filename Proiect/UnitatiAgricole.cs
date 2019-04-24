using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPaw
{
    public class UnitateAgricola
    {
        public string Denumire { get; set; }
        public Adresa _Adresa = new Adresa();
        public Indicatori _Indicatori = new Indicatori();
        public  string CUI { get; set; }

        public UnitateAgricola() {  }

        public UnitateAgricola(string denumire, Adresa adresa, Indicatori ind, string cui)
        {
            Denumire = denumire;
            _Adresa = adresa;
            CUI = cui;
            _Indicatori = ind;
        }

        public override string ToString()
        {
            return string.Format("Denumire Unitate: {0}| Adresa -> {1}| CUI: {2}|\nIndicatorii colectati -> {3}",
                Denumire, _Adresa, CUI, _Indicatori);
        }
    }
}
