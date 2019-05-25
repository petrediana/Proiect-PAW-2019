using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProiectPaw
{
    public partial class AdaugaUnitateAgricola : Form
    {
        public List<UnitateAgricola> Unitati = new List<UnitateAgricola>();
        UnitateAgricola deAgaugat = new UnitateAgricola();

        public AdaugaUnitateAgricola(List<UnitateAgricola> unitati)
        {
            InitializeComponent();
            Unitati = unitati; 
        }
        
        private void AdaugaJudet()
        {
                MessageBox.Show(listaJudete1.ReturnJudetSelectat());
                try
                {
                    if (string.IsNullOrEmpty(listaJudete1.ReturnJudetSelectat()))
                        throw new ExceptieDenumire();
                    else
                    {
                        if (listaJudete1.ReturnJudetSelectat().Any(char.IsDigit))
                            throw new ExceptieNumeCuCifre();
                        else
                        {
                            deAgaugat._Adresa.Judet = listaJudete1.ReturnJudetSelectat();
                            //MessageBox.Show(deAgaugat._Adresa.Judet);
                        }
                    }
                }
                catch (ExceptieDenumire ex)
                {
                    MessageBox.Show(ex.mesaj());
                }
                catch (ExceptieNumeCuCifre ex)
                {
                    MessageBox.Show(ex.mesaj());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Nu se poate! Alta exceptie ({0})", ex.Message));
                }
            
        }

            //adauga unitate in lista + baza de date
        private void button1_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(deAgaugat.CUI))
            {
                MessageBox.Show("Nu pot sa introduc o unitate fara CUI!\n", "Important!!",
                    MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show(string.Format("Unitatea a fost adaugata cu succes!\n\nUnitatea se poate modifica" +
                    " de asemenea din aplicatie!\n\nUnitatea introdusa: " + deAgaugat.ToString()),
                    "Atentie!", MessageBoxButtons.OK);
                Unitati.Add(deAgaugat);

                const string ProviderName = "System.Data.OleDb";
                const string ConnString =
                     @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=
               C:\Users\Peanutt\Desktop\VS\C#\ProiectPaw\Unitati.mdb";

                //INSERT INTO TABLE
                try
                {
                    DbProviderFactory factory = DbProviderFactories.GetFactory(ProviderName);
                    DbConnection connection = factory.CreateConnection();
                    connection.ConnectionString = ConnString;
                    connection.Open();

                    DbCommand InsertCommand = connection.CreateCommand();
                    InsertCommand.CommandText = "INSERT INTO UNITATI(Denumire, Judet, Strada, Numar," +
                        "TerenArabil, Vii, Livezi, Pasune, CUI) VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?)";

                    //parametru pentru denumire unitate
                    DbParameter DenumireParameter = InsertCommand.CreateParameter();
                    DenumireParameter.DbType = System.Data.DbType.String;
                    InsertCommand.Parameters.Add(DenumireParameter);

                    //parametru pentru judet unitate
                    DbParameter JudetParameter = InsertCommand.CreateParameter();
                    JudetParameter.DbType = System.Data.DbType.String;
                    InsertCommand.Parameters.Add(JudetParameter);

                    //parametru pentru strada unitate
                    DbParameter StradaParameter = InsertCommand.CreateParameter();
                    StradaParameter.DbType = System.Data.DbType.String;
                    InsertCommand.Parameters.Add(StradaParameter);

                    //parametru pentru numar
                    DbParameter NumarParameter = InsertCommand.CreateParameter();
                    NumarParameter.DbType = System.Data.DbType.Int32;
                    InsertCommand.Parameters.Add(NumarParameter);

                    //parametru pentru TerenArabil
                    DbParameter TerenAParameter = InsertCommand.CreateParameter();
                    TerenAParameter.DbType = System.Data.DbType.Double;
                    InsertCommand.Parameters.Add(TerenAParameter);

                    //parametru pentru Vii
                    DbParameter ViiParameter = InsertCommand.CreateParameter();
                    ViiParameter.DbType = System.Data.DbType.Double;
                    InsertCommand.Parameters.Add(ViiParameter);

                    //parametru pentru livezi
                    DbParameter LiveziParameter = InsertCommand.CreateParameter();
                    LiveziParameter.DbType = System.Data.DbType.Double;
                    InsertCommand.Parameters.Add(LiveziParameter);

                    //parametru pentru Pasune
                    DbParameter PasuneParameter = InsertCommand.CreateParameter();
                    PasuneParameter.DbType = System.Data.DbType.Double;
                    InsertCommand.Parameters.Add(PasuneParameter);

                    //parametru pentru CUI unitate
                    DbParameter CUIParameter = InsertCommand.CreateParameter();
                    CUIParameter.DbType = System.Data.DbType.String;
                    InsertCommand.Parameters.Add(CUIParameter);

                    //valori ->
                    DenumireParameter.Value = deAgaugat.Denumire;
                    JudetParameter.Value = deAgaugat._Adresa.Judet;
                    StradaParameter.Value = deAgaugat._Adresa.Strada;
                    NumarParameter.Value = deAgaugat._Adresa.Numar;
                    TerenAParameter.Value = deAgaugat._Indicatori.TerenArabil;
                    ViiParameter.Value = deAgaugat._Indicatori.Vii;
                    LiveziParameter.Value = deAgaugat._Indicatori.Livezi;
                    PasuneParameter.Value = deAgaugat._Indicatori.Pasuni;
                    CUIParameter.Value = deAgaugat.CUI;

                    InsertCommand.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Console.WriteLine(ex.Message);
                }
                    Close();
            }
            
        }

        private void Enter_denumire(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string txt = denumireTxt.Text;
                try
                {
                    if (string.IsNullOrEmpty(txt))
                    {
                        throw new ExceptieDenumire();
                    }
                    else
                    {
                        deAgaugat.Denumire = txt;
                        MessageBox.Show(deAgaugat.Denumire);
                    }
                }
                catch (ExceptieDenumire ex)
                {
                    MessageBox.Show(ex.mesaj());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Nu se poate! Alta exceptie ({0})", ex.Message));
                }
            }
        }       

        private void Enter_cui(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                string txt = CUItxt.Text;
                try
                {
                    bool Ok = true;
                    foreach (var unit in Unitati)
                    {
                        if (unit.CUI == txt)
                            Ok = false;
                    }

                    if (!Ok)
                        MessageBox.Show("Exista deja o unitate cu acest CUI!");
                    else
                    {
                        if (txt.Length != 12 || txt.Substring(0, 2) != "RO"
                            || !txt.Substring(2, 9).All(char.IsDigit))
                            throw new ExceptieCUI();
                        else
                        {
                            deAgaugat.CUI = txt;
                            MessageBox.Show(deAgaugat.CUI);
                        }
                    }

                }
                catch (ExceptieCUI ex)
                {
                    MessageBox.Show(ex.message_format());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Nu se poate! Alta exceptie ({0})", ex.Message));
                }
            }

        }

        //private void enter_judet(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        string txt = judetTxt.Text;
        //        try
        //        {
        //            if (string.IsNullOrEmpty(txt))
        //                throw new ExceptieDenumire();
        //            else
        //            if (txt.Any(char.IsDigit))
        //                throw new ExceptieNumeCuCifre();
        //            else
        //            {
        //                deAgaugat._Adresa.Judet = txt;
        //                MessageBox.Show(deAgaugat._Adresa.Judet);
        //            }
        //        }
        //        catch (ExceptieDenumire ex)
        //        {
        //            MessageBox.Show(ex.mesaj());
        //        }
        //        catch (ExceptieNumeCuCifre ex)
        //        {
        //            MessageBox.Show(ex.mesaj());
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(string.Format("Nu se poate! Alta exceptie ({0})", ex.Message));
        //        }
        //    }
        //}

        private void enter_strada(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                string txtS = stradaTxt.Text;
                try
                {
                    if (txtS.Any(char.IsDigit))
                        throw new ExceptieNumeCuCifre();
                    else
                        if (string.IsNullOrEmpty(txtS))
                        throw new ExceptieDenumire();
                    else
                    {
                        deAgaugat._Adresa.Strada = txtS;
                        MessageBox.Show(deAgaugat._Adresa.Strada);
                    }
                }
                catch (ExceptieDenumire ex)
                {
                    MessageBox.Show(ex.mesaj());
                }
                catch (ExceptieNumeCuCifre ex)
                {
                    MessageBox.Show(ex.mesaj());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Nu se poate! Alta exceptie ({0})", ex.Message));
                }
            }
        }

        private void enter_numar(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                try
                {
                    string txtNr = nrTxt.Text;
                    if (string.IsNullOrEmpty(txtNr))
                        throw new ExceptieDenumire();
                    else
                        if (txtNr.Any(char.IsLetter))
                        MessageBox.Show(string.Format("Numarul strazii nu poate contine litere!"));
                    else
                    {
                        deAgaugat._Adresa.Numar = int.Parse(txtNr);
                        MessageBox.Show(deAgaugat._Adresa.Numar.ToString());
                    }
                }
                catch (ExceptieDenumire ex)
                {
                    MessageBox.Show(ex.mesaj());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Nu se poate! Alta exceptie ({0})", ex.Message));
                }
            }
        }

        private void enter_terenA(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                string txtTA = terenAtxt.Text;
                try
                {
                    if (txtTA.Any(char.IsLetter))
                        throw new ExceptieCifreCuLitere();
                    else
                        if (string.IsNullOrEmpty(txtTA))
                        throw new ExceptieDenumire();
                    else
                        if (double.Parse(txtTA) < 0)
                        throw new ExceptieNrNegativ();
                    else
                    {
                        deAgaugat._Indicatori.TerenArabil = double.Parse(txtTA);
                        MessageBox.Show(deAgaugat._Indicatori.TerenArabil.ToString());
                    }
                }
                catch (ExceptieNrNegativ ex)
                {
                    MessageBox.Show(ex.mesaj());
                }
                catch(ExceptieCifreCuLitere ex)
                {
                    MessageBox.Show(ex.mesaj());
                }
                catch (ExceptieDenumire ex)
                {
                    MessageBox.Show(ex.mesaj());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Nu se poate! Alta exceptie ({0})", ex.Message));
                }
            }
        }

        private void enter_vie(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string txtVie = vieTxt.Text;
                try
                {
                    if (txtVie.Any(char.IsLetter))
                        throw new ExceptieCifreCuLitere();
                    else
                        if (string.IsNullOrEmpty(txtVie))
                        throw new ExceptieDenumire();
                    else
                        if (double.Parse(txtVie) < 0)
                        throw new ExceptieNrNegativ();
                    else
                    {
                        deAgaugat._Indicatori.Vii = double.Parse(txtVie);
                        MessageBox.Show(deAgaugat._Indicatori.Vii.ToString());
                    }
                }
                catch (ExceptieNrNegativ ex)
                {
                    MessageBox.Show(ex.mesaj());
                }
                catch (ExceptieCifreCuLitere ex)
                {
                    MessageBox.Show(ex.mesaj());
                }
                catch (ExceptieDenumire ex)
                {
                    MessageBox.Show(ex.mesaj());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Nu se poate! Alta exceptie ({0})", ex.Message));
                }
            }
        }

        private void enter_livezi(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string txtLivada = livadaTxt.Text;
                try
                {
                    if (txtLivada.Any(char.IsLetter))
                        throw new ExceptieCifreCuLitere();
                    else
                        if (string.IsNullOrEmpty(txtLivada))
                        throw new ExceptieDenumire();
                    else
                        if (double.Parse(txtLivada) < 0)
                        throw new ExceptieNrNegativ();
                    else
                    {
                        deAgaugat._Indicatori.Livezi = double.Parse(txtLivada);
                        MessageBox.Show(deAgaugat._Indicatori.Livezi.ToString());
                    }
                }
                catch (ExceptieNrNegativ ex)
                {
                    MessageBox.Show(ex.mesaj());
                }
                catch (ExceptieCifreCuLitere ex)
                {
                    MessageBox.Show(ex.mesaj());
                }
                catch (ExceptieDenumire ex)
                {
                    MessageBox.Show(ex.mesaj());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Nu se poate! Alta exceptie ({0})", ex.Message));
                }
            }
        }

        private void enter_pasune(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string txtPasune = pasuneTxt.Text;
                try
                {
                    if (txtPasune.Any(char.IsLetter))
                        throw new ExceptieCifreCuLitere();
                    else
                        if (string.IsNullOrEmpty(txtPasune))
                        throw new ExceptieDenumire();
                    else
                        if (double.Parse(txtPasune) < 0)
                        throw new ExceptieNrNegativ();
                    else
                    {
                        deAgaugat._Indicatori.Pasuni = double.Parse(txtPasune);
                        MessageBox.Show(deAgaugat._Indicatori.Pasuni.ToString());
                    }
                }
                catch (ExceptieNrNegativ ex)
                {
                    MessageBox.Show(ex.mesaj());
                }
                catch (ExceptieCifreCuLitere ex)
                {
                    MessageBox.Show(ex.mesaj());
                }
                catch (ExceptieDenumire ex)
                {
                    MessageBox.Show(ex.mesaj());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Nu se poate! Alta exceptie ({0})", ex.Message));
                }
            }
        }

        //vezi unitate introdusa buton
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(string.Format(deAgaugat.ToString()));
        }

        private void listaJudete1_Click(object sender, EventArgs e)
        {
            AdaugaJudet();           
        }
    }
}
