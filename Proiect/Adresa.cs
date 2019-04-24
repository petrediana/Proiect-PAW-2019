using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPaw
{
    public class Adresa
    {
        const string Tara = "Romania";
        private string _judet;
        private string _strada;
        private int _numar;

        public string Judet
        {
            get { return _judet; }
            set { _judet = value; }                 
        }

        public string Strada
        {
            get { return _strada; }
            set { _strada = value; }
        }

        public int Numar
        {
            get { return _numar; }
            set { _numar = value; }
        }

        public Adresa() { }
        public Adresa(string judet, string strada, int numar)
        {
            _judet = judet;
            _strada = strada;
            _numar = numar;
        }

        public override string ToString()
        {
            return string.Format("Tara: {0}, Judet: {1}, Strada: {2}, Numar: {3}",
                Tara, _judet, _strada, _numar);
        }

    }
}
