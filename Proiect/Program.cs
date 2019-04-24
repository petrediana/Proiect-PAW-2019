using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProiectPaw
{
    static class Program
    {
        const string CaleFisier = "date.txt";

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            List<UnitateAgricola> unitati = new List<UnitateAgricola>();

            string[] linii = File.ReadAllLines(CaleFisier);
            int nrUnitati = int.Parse(linii[0]);
                       
            for(int i = 0; i < nrUnitati; i++)
            {
                string[] cuvinte = linii[i + 1].Split(',');

                unitati.Add(new UnitateAgricola(cuvinte[0], new Adresa(cuvinte[1], cuvinte[2],
                    int.Parse(cuvinte[3])), new Indicatori(double.Parse(cuvinte[4]), double.Parse(cuvinte[5]),
                    double.Parse(cuvinte[6]), double.Parse(cuvinte[7])), cuvinte[8]));
            }

            foreach(var unitate in unitati)
            {
                Console.WriteLine(unitate);
            }

            Application.Run(new PaginaStart(unitati));

            string txt = "RO2222222222C";
            Console.WriteLine(txt.Substring(2, 10).All(char.IsDigit));

            //Console.WriteLine(linii[1]);
        }
    }
}
//string test = "aa3aa";
//bool nr = test.Any(char.IsDigit);
//if (nr)
//    Console.Write("da {0}", nr);
//else
//    Console.Write("nu {0}", nr);


//Adresa adresa = new Adresa("Iasi", "a4aa", 102);
////Console.WriteLine(adresa);
//Indicatori ind = new Indicatori(10, 15, 2, 4);
////Console.WriteLine(ind);
//UnitateAgricola unitate = new UnitateAgricola("aaa", adresa, 1234567, ind);
//Console.WriteLine(unitate._Adresa);