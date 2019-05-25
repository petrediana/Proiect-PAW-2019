using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPaw
{
    public class UnitateAgricola :IComparable<UnitateAgricola>, ICloneable
    {
        public string Denumire { get; set; }
        public Adresa _Adresa = new Adresa();
        public Indicatori _Indicatori = new Indicatori();
        public string CUI { get; set; }

        public UnitateAgricola() {
            Denumire = "Anonim";
            _Adresa.Judet = "Anonim";
            _Adresa.Strada = "Anonim";
        }

        public UnitateAgricola(string denumire, Adresa adresa, Indicatori ind, string cui)
        {
            Denumire = denumire;
            _Adresa = adresa;
            CUI = cui;
            _Indicatori = ind;
        }

        public override string ToString()
        {
            return string.Format("Denumire Unitate: {0} CUI: {1} Adresa:\n{2}\nIndicatorii colectati:\n{3}",
                Denumire, CUI, _Adresa, _Indicatori);
        }

        public int CompareTo(UnitateAgricola other)
        {
            return _Indicatori.CompareTo(other._Indicatori);
        }

        public object Clone()
        {
            UnitateAgricola clona = (UnitateAgricola)this.MemberwiseClone();

            Adresa adresa = new Adresa();
            adresa = (Adresa)_Adresa.Clone();

            Indicatori indicatori = new Indicatori();
            indicatori = (Indicatori)_Indicatori.Clone();

            clona._Adresa = adresa;
            clona._Indicatori = indicatori;

            return clona;
        }
    }
}
