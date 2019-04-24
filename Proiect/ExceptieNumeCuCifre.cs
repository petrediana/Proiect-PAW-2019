using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPaw
{
    class ExceptieNumeCuCifre : Exception
    {
        public string mesaj()
        {
            return string.Format("Numele nu poate contine cifre!");
        }
    }
}
