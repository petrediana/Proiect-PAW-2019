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
    public partial class ModificaDateForm : Form
    {
        string CUI;
        string Denumire;
        Adresa _Adresa = new Adresa();
        Indicatori _Indicatori = new Indicatori();
        public UnitateAgricola Rezultat = new UnitateAgricola();

        public ModificaDateForm(string denumire, Adresa adresa, Indicatori indicatori, string cui)
        {
            InitializeComponent();
            Denumire = denumire;
            _Adresa = adresa;
            _Indicatori = indicatori;
            CUI = cui;

            AfisareDateCurente();

            this.KeyPreview = true;
            this.KeyDown += ModificaDateForm_KeyDown;            
        }

        private void ModificaDateForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode.ToString() == "H")
                textBox8.Focus();

            if (e.Alt && e.KeyCode.ToString() == "G")
                textBox7.Focus();

            if (e.Alt && e.KeyCode.ToString() == "F")
                textBox6.Focus();

            if (e.Alt && e.KeyCode.ToString() == "E")
                textBox5.Focus();

            if (e.Alt && e.KeyCode.ToString() == "D")
                textBox4.Focus();

            if (e.Alt && e.KeyCode.ToString() == "C")
                textBox3.Focus();

            if (e.Alt && e.KeyCode.ToString() == "A")
                textBox1.Focus();
        }

        void AfisareDateCurente()
        {
            textBox1.Text = Denumire;
            //textBox2.Text = _Adresa.Judet;
            listaJudete1.theComboBox.Text = _Adresa.Judet;
            textBox3.Text = _Adresa.Strada;
            textBox4.Text = _Adresa.Numar.ToString();
            textBox5.Text = _Indicatori.TerenArabil.ToString();
            textBox6.Text = _Indicatori.Vii.ToString();
            textBox7.Text = _Indicatori.Livezi.ToString();
            textBox8.Text = _Indicatori.Pasuni.ToString();
        }

        //vezi modificari - buton
        private void button2_Click(object sender, EventArgs e)
        {
            int count = 0;
            string modificari = "";

            if (textBox1.Text != Denumire)
            {
                count++;
                modificari = modificari + "Denumire: " + textBox1.Text;
            }
            if (listaJudete1.theComboBox.Text != _Adresa.Judet)
            {
                count++;
                modificari += "\nJudet: " + listaJudete1.theComboBox.Text;
            }
            if (textBox3.Text != _Adresa.Strada)
            {
                count++;
                modificari += "\nStrada: " + textBox3.Text;
            }
            if (textBox4.Text != _Adresa.Numar.ToString())
            {
                count++;
                modificari += "\nNumarul: " + textBox4.Text;
            }
            if (textBox5.Text != _Indicatori.TerenArabil.ToString())
            {
                count++;
                modificari += "\nTeren arabil (ha): " + textBox5.Text;
            }
            if (textBox6.Text != _Indicatori.Vii.ToString())
            {
                count++;
                modificari += "\nVii (ha): " + textBox6.Text;
            }
            if (textBox7.Text != _Indicatori.Livezi.ToString())
            {
                count++;
                modificari += "\nLivezi (ha): " + textBox7.Text;
            }
            if (textBox8.Text != _Indicatori.Pasuni.ToString())
            {
                count++;
                modificari += "\nPasuni (ha): " + textBox8.Text;
            }

            if(count > 0) {
                MessageBox.Show(string.Format(
                    "S-au produs {0} modificari acestea sunt: \n {1}", count.ToString(), modificari));               
            }
            else
            {
                MessageBox.Show("Nu s-au produs modificari!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Rezultat.Denumire = Denumire;
            Rezultat._Adresa.Judet = listaJudete1.ReturnJudetSelectat();
            Rezultat._Adresa.Strada = _Adresa.Strada;
            Rezultat._Adresa.Numar = _Adresa.Numar;
            Rezultat._Indicatori.TerenArabil = _Indicatori.TerenArabil;
            Rezultat._Indicatori.Vii = _Indicatori.Vii;
            Rezultat._Indicatori.Livezi = _Indicatori.Livezi;
            Rezultat._Indicatori.Pasuni = _Indicatori.Pasuni;

            Close();
        }

       private bool DateValide()
        {
            //denumire
            string den = textBox1.Text;
            if (string.IsNullOrEmpty(den))
                return false;

            ////judet
            //string jud = textBox.Text;
            //if (string.IsNullOrEmpty(jud) || jud.Any(char.IsDigit))
            //    return false;

            //strada
            string str = textBox3.Text;
            if (string.IsNullOrEmpty(str) || str.Any(char.IsDigit))
                return false;

            //numarul
            string nr = textBox4.Text;
            if (string.IsNullOrEmpty(nr) || nr.Any(char.IsLetter))
                return false;

            //teren arabil
            string ta = textBox5.Text;
            if (string.IsNullOrEmpty(ta) || ta.Any(char.IsLetter) || double.Parse(ta) < 0)
                return false;

            //vii
            string v = textBox6.Text;
            if (string.IsNullOrEmpty(v) || v.Any(char.IsLetter) || double.Parse(v) < 0)
                return false;

            //livezi
            string l = textBox7.Text;
            if (string.IsNullOrEmpty(l) || l.Any(char.IsLetter) || double.Parse(l) < 0)
                return false;

            //pasuni
            string p = textBox8.Text;
            if (string.IsNullOrEmpty(p) || p.Any(char.IsLetter) || double.Parse(p) < 0)
                return false;
            else
                return true;
        }

        //salveaza modificari - buton
        private void button1_Click(object sender, EventArgs e)
        {
            bool legal = DateValide();
            if (!legal)
                MessageBox.Show(string.Format("Datele nu au fost introduse corect!\n"
                    + "Verificati daca exista: \n 1.Campuri goale \n 2. Numere negative pentru " +
                    "indicatorii numerici\n 3. Judetul nu poate contine numere"));
            else
            {
                DialogResult dialog = MessageBox.Show("Datele se vor modifica permanent! Sunteti sigur?", "Atentie!",
                    MessageBoxButtons.OKCancel);
                
                if(dialog == DialogResult.OK)
                {
                    MessageBox.Show(string.Format("Datele au fost modificate!"));
                    Rezultat.Denumire = textBox1.Text;
                    //Rezultat._Adresa.Judet = textBox2.Text;
                    Rezultat._Adresa.Judet = listaJudete1.ReturnJudetSelectat();
                    Rezultat._Adresa.Strada = textBox3.Text;
                    Rezultat._Adresa.Numar = int.Parse(textBox4.Text);
                    Rezultat._Indicatori.TerenArabil = double.Parse(textBox5.Text);
                    Rezultat._Indicatori.Vii = double.Parse(textBox6.Text);
                    Rezultat._Indicatori.Livezi = double.Parse(textBox7.Text);
                    Rezultat._Indicatori.Pasuni = double.Parse(textBox8.Text);

                    const string ProviderName = "System.Data.OleDb";
                    const string ConnString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=
                                        C:\Users\Peanutt\Desktop\VS\C#\ProiectPaw\Unitati.mdb";

                    //UPDATE TABLE
                    try
                    {
                        MessageBox.Show(CUI);
                        DbProviderFactory factory = DbProviderFactories.GetFactory(ProviderName);
                        DbConnection connection = factory.CreateConnection();
                        connection.ConnectionString = ConnString;
                        connection.Open();

                        DbCommand UpdateCommand = connection.CreateCommand();
                        UpdateCommand.CommandText = "UPDATE UNITATI SET Denumire = ?, Judet = ?, Strada = ?," +
                            "Numar = ?, TerenArabil = ?, Vii = ?, Livezi = ?, Pasune = ? WHERE CUI = ?";

                        //parametru pentru denumire unitate
                        DbParameter DenumireParameter = UpdateCommand.CreateParameter();
                        DenumireParameter.DbType = System.Data.DbType.String;
                        UpdateCommand.Parameters.Add(DenumireParameter);

                        //parametru pentru judet unitate
                        DbParameter JudetParameter = UpdateCommand.CreateParameter();
                        JudetParameter.DbType = System.Data.DbType.String;
                        UpdateCommand.Parameters.Add(JudetParameter);

                        //parametru pentru strada unitate
                        DbParameter StradaParameter = UpdateCommand.CreateParameter();
                        StradaParameter.DbType = System.Data.DbType.String;
                        UpdateCommand.Parameters.Add(StradaParameter);

                        //parametru pentru numar
                        DbParameter NumarParameter = UpdateCommand.CreateParameter();
                        NumarParameter.DbType = System.Data.DbType.Int32;
                        UpdateCommand.Parameters.Add(NumarParameter);

                        //parametru pentru TerenArabil
                        DbParameter TerenAParameter = UpdateCommand.CreateParameter();
                        TerenAParameter.DbType = System.Data.DbType.Double;
                        UpdateCommand.Parameters.Add(TerenAParameter);

                        //parametru pentru Vii
                        DbParameter ViiParameter = UpdateCommand.CreateParameter();
                        ViiParameter.DbType = System.Data.DbType.Double;
                        UpdateCommand.Parameters.Add(ViiParameter);

                        //parametru pentru livezi
                        DbParameter LiveziParameter = UpdateCommand.CreateParameter();
                        LiveziParameter.DbType = System.Data.DbType.Double;
                        UpdateCommand.Parameters.Add(LiveziParameter);

                        //parametru pentru Pasune
                        DbParameter PasuneParameter = UpdateCommand.CreateParameter();
                        PasuneParameter.DbType = System.Data.DbType.Double;
                        UpdateCommand.Parameters.Add(PasuneParameter);

                        //parametru pentru CUI unitate
                        DbParameter CUIParameter = UpdateCommand.CreateParameter();
                        CUIParameter.DbType = System.Data.DbType.String;
                        UpdateCommand.Parameters.Add(CUIParameter);

                        //valori ->
                        DenumireParameter.Value = Rezultat.Denumire;
                        JudetParameter.Value = Rezultat._Adresa.Judet;
                        StradaParameter.Value = Rezultat._Adresa.Strada;
                        NumarParameter.Value = Rezultat._Adresa.Numar;
                        TerenAParameter.Value = Rezultat._Indicatori.TerenArabil;
                        ViiParameter.Value = Rezultat._Indicatori.Vii;
                        LiveziParameter.Value = Rezultat._Indicatori.Livezi;
                        PasuneParameter.Value = Rezultat._Indicatori.Pasuni;
                        CUIParameter.Value = CUI;

                        UpdateCommand.ExecuteNonQuery();
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        private void listaJudete1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(listaJudete1.ReturnJudetSelectat());
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
                        Rezultat._Adresa.Judet = listaJudete1.ReturnJudetSelectat();
                        MessageBox.Show(Rezultat._Adresa.Judet);
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
    }
}
