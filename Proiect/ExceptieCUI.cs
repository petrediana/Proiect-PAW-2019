using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPaw
{
    class ExceptieCUI : Exception
    {
        public string message_format()
        {
            return string.Format("Format gresit pentru codul de identificare!\nTrebuie" +
                "respectat urmatorul format: RO#########X" +
                "\nRO, 9 cifre, o litera");
        }
    }
}
