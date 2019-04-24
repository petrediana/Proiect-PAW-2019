using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(deAgaugat.ToString());
            Unitati.Add(deAgaugat);
            //TODO: fa ceva si la buton + fisier
            //Close();
        }

        private void enter_denumire(object sender, KeyEventArgs e)
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

        private void enter_cui(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                string txt = CUItxt.Text;
                try
                {
                    if (txt.Length != 11 || txt.Substring(0, 2) != "RO" || txt.Substring(10, 10) != "C"
                        || !txt.Substring(2, 10).All(char.IsDigit))
                        throw new ExceptieCUI();
                    else
                    {
                        deAgaugat.CUI = txt;
                        MessageBox.Show(deAgaugat.CUI);
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

        private void enter_judet(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string txt = judetTxt.Text;
                try
                {
                    if (string.IsNullOrEmpty(txt))
                        throw new ExceptieDenumire();
                    else
                    if (txt.Any(char.IsDigit))
                        throw new ExceptieNumeCuCifre();
                    else
                    {
                        deAgaugat._Adresa.Judet = txt;
                        MessageBox.Show(deAgaugat._Adresa.Judet);
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
    }
}
