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
    public partial class ObservatiiForm : Form
    {
        public List<UnitateAgricola> Unitati = new List<UnitateAgricola>();

        public ObservatiiForm(List<UnitateAgricola> unitati)
        {
            InitializeComponent();
            Unitati = unitati;

            listView1.View = View.Details;
            listView1.Columns.Add("Județ", 80);
            listView1.Columns.Add("Teren arabil (total ha): ", 120);
            listView1.Columns.Add("Vie (total ha): ", 90);
            listView1.Columns.Add("Livadă (total ha): ", 100);
            listView1.Columns.Add("Pășune (total ha): ", 100);

            AfisareMapa();
        }

        Dictionary<string, Indicatori> GetDate()
        {
            var myMap = new Dictionary<string, Indicatori>();

            foreach(var unitate in Unitati.OrderBy(u => u._Adresa.Judet))
            {
                if (!myMap.ContainsKey(unitate._Adresa.Judet))
                {
                    Indicatori indicatori = new Indicatori(unitate._Indicatori.TerenArabil,
                        unitate._Indicatori.Vii, unitate._Indicatori.Livezi, unitate._Indicatori.Pasuni);
                    myMap.Add(unitate._Adresa.Judet, indicatori);
                }
                else
                {
                    myMap[unitate._Adresa.Judet].TerenArabil += unitate._Indicatori.TerenArabil;
                    myMap[unitate._Adresa.Judet].Vii += unitate._Indicatori.Vii;
                    myMap[unitate._Adresa.Judet].Livezi += unitate._Indicatori.Livezi;
                    myMap[unitate._Adresa.Judet].Pasuni += unitate._Indicatori.Pasuni;
                }
            }

            return myMap;
        }

        void AfisareMapa()
        {
            listView1.Items.Clear();
            var DateMap = GetDate();

            foreach(var unitate in DateMap)
            {
                ListViewItem item = new ListViewItem();
                item.Text = unitate.Key;
                item.SubItems.Add(unitate.Value.TerenArabil.ToString("0.00"));
                item.SubItems.Add(unitate.Value.Vii.ToString("0.00"));
                item.SubItems.Add(unitate.Value.Livezi.ToString("0.00"));
                item.SubItems.Add(unitate.Value.Pasuni.ToString("0.00"));

                listView1.Items.Add(item);
            }
        }
    }
}
