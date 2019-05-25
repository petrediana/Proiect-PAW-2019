using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProiectPaw
{
    static class Program
    {
        const string CaleFisier = "date.txt";
        //‪C:\Users\Peanutt\Desktop\VS\C#\ProiectPaw\Unitati.mdb

        const string ProviderName = "System.Data.OleDb";
        const string ConnString =
             @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=
               C:\Users\Peanutt\Desktop\VS\C#\ProiectPaw\Unitati.mdb";

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //List<UnitateAgricola> unitati = new List<UnitateAgricola>();

            //string[] linii = File.ReadAllLines(CaleFisier);
            //int nrUnitati = int.Parse(linii[0]);

            //for(int i = 0; i < nrUnitati; i++)
            //{
            //    string[] cuvinte = linii[i + 1].Split(',');

            //    unitati.Add(new UnitateAgricola(cuvinte[0], new Adresa(cuvinte[1], cuvinte[2],
            //        int.Parse(cuvinte[3])), new Indicatori(double.Parse(cuvinte[4]), double.Parse(cuvinte[5]),
            //        double.Parse(cuvinte[6]), double.Parse(cuvinte[7])), cuvinte[8]));
            //}

            ////foreach(var unitate in unitati)
            ////{
            ////    Console.WriteLine(unitate);
            ////}

            //Application.Run(new PaginaStartForm(unitati));

            //string txt = "RO222222222C";
            ////Console.WriteLine(txt.Substring(2, 10).All(char.IsDigit));
            //Console.WriteLine(txt.Substring(2, 9));

            List<UnitateAgricola> Unitati = new List<UnitateAgricola>();

            try
            {
                DbProviderFactory factory = DbProviderFactories.GetFactory(ProviderName);

                DbConnection connection = factory.CreateConnection();
                connection.ConnectionString = ConnString;
                connection.Open();

                DbCommand SelectCommand = connection.CreateCommand();
                SelectCommand.CommandText = "SELECT * FROM UNITATI";

                DbDataReader reader = SelectCommand.ExecuteReader();
                while (reader.Read())
                {
                    string denumire = (string)reader["Denumire"];
                    string judet = (string)reader["Judet"];
                    string strada = (string)reader["Strada"];
                    int numar = (int)reader["Numar"];
                    double TerenArabil = (double)reader["TerenArabil"];
                    double Vii = (double)reader["Vii"];
                    double Livezi = (double)reader["Livezi"];
                    double Pasune = (double)reader["Pasune"];
                    string CUI = (string)reader["CUI"];

                    Console.WriteLine("Denumire: {0}, Judet: {1}, Strada: {2}, Numar: {3}, TerenA: {4}, Vii: {5}," +
                        "Livezi: {6}, Pasune: {7}, CUI: {8}"
                        , denumire, judet, strada, numar, TerenArabil, Vii, Livezi, Pasune, CUI);

                    Adresa adresa = new Adresa(judet, strada, numar);
                    Indicatori indicatori = new Indicatori(TerenArabil, Vii, Livezi, Pasune);

                    Unitati.Add(new UnitateAgricola(denumire, adresa, indicatori, CUI));
                }
                Console.WriteLine("\n---Elemente bagate in lista---\n");
                foreach(var u in Unitati)
                {
                    Console.WriteLine(u + "\n");
                }

                Application.Run(new PaginaStartForm(Unitati));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

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