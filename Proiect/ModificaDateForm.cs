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
    public partial class ModificaDateForm : Form
    {
        string Denumire;
        Adresa _Adresa = new Adresa();
        Indicatori _Indicatori = new Indicatori();

        public ModificaDateForm(string denumire, Adresa adresa, Indicatori indicatori)
        {
            InitializeComponent();
            Denumire = denumire;
            _Adresa = adresa;
            _Indicatori = indicatori;

            AfisareDateCurente();
        }

        void AfisareDateCurente()
        {
            textBox1.Text = Denumire;
            textBox2.Text = _Adresa.Judet;
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
            if (textBox2.Text != _Adresa.Judet)
            {
                count++;
                modificari += "\nJudet: " + textBox2.Text;
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
    }
}
