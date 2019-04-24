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
    public partial class VizualizareUnitati : Form
    {
        public List<UnitateAgricola> Unitati = new List<UnitateAgricola>();

        public VizualizareUnitati(List<UnitateAgricola> unitati)
        {
            InitializeComponent();
            Unitati = unitati;

            listView1.Dock = DockStyle.Fill;
            listView1.View = View.Details;
            listView1.Columns.Add("Denumire Unitate: ", 120);
            listView1.Columns.Add("CUI: ", 100);
            listView1.Columns.Add("Judet: ", 80);
            listView1.Columns.Add("Strada ", 120);
            listView1.Columns.Add("Numarul: ", 80);
            listView1.Columns.Add("Teren arabil (ha): ", 100);
            listView1.Columns.Add("Vie (ha): ", 100);
            listView1.Columns.Add("Livada (ha): ", 100);
            listView1.Columns.Add("Pasune (ha): ", 100);

            Afisare();
        }

        void Afisare()
        {
            listView1.Items.Clear();
            foreach(var unitate in Unitati.OrderByDescending(m => m._Adresa.Judet))
            {
                ListViewItem rand_nou = new ListViewItem();
                rand_nou.Text = unitate.Denumire;
                rand_nou.SubItems.Add(unitate.CUI);
                rand_nou.SubItems.Add(unitate._Adresa.Judet);
                rand_nou.SubItems.Add(unitate._Adresa.Strada);
                rand_nou.SubItems.Add(unitate._Adresa.Numar.ToString());
                rand_nou.SubItems.Add(unitate._Indicatori.TerenArabil.ToString("0.00"));
                rand_nou.SubItems.Add(unitate._Indicatori.Vii.ToString("0.00"));
                rand_nou.SubItems.Add(unitate._Indicatori.Livezi.ToString("0.00"));
                rand_nou.SubItems.Add(unitate._Indicatori.Pasuni.ToString("0.00"));

                listView1.Items.Add(rand_nou);
            }
        }
    }
}
