using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPaw
{
    class ExceptieDenumire : Exception
    {
           public string mesaj()
        {
            return string.Format("Campul nu poate fi null!");
        }
    }
}
