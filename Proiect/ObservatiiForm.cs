using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProiectPaw
{
    public partial class ObservatiiForm : Form
    {
        public List<UnitateAgricola> Unitati = new List<UnitateAgricola>();
        string SalveazaJudeteSelectate = "S-a selectat:\n";

        public ObservatiiForm(List<UnitateAgricola> unitati)
        {
            InitializeComponent();
            Unitati = unitati;

            judetDraggedTxtBox.AllowDrop = true;
            judetDraggedTxtBox.ReadOnly = true;
            listView1.FullRowSelect = true;

            listView1.View = View.Details;
            listView1.Columns.Add("Județ", 80);
            listView1.Columns.Add("Teren arabil (total ha): ", 120);
            listView1.Columns.Add("Vie (total ha): ", 90);
            listView1.Columns.Add("Livadă (total ha): ", 100);
            listView1.Columns.Add("Pășune (total ha): ", 100);

            AfisareMapa();
            InitPieChart();

            KeyDown += ObservatiiForm_KeyDown;
            listView1.MouseDown += ListView1_MouseDown;
            judetDraggedTxtBox.DragEnter += JudetDraggedTxtBox_DragEnter;
            judetDraggedTxtBox.DragDrop += JudetDraggedTxtBox_DragDrop;            
        }

        public string GetValoriDinListView()
        {
            if (!string.IsNullOrEmpty(judetDraggedTxtBox.Text))
            {
                return judetDraggedTxtBox.Text;
            }

            else
                return string.Format("0,0,0,0");
        }

        private void SeteazaValoriPentruGrafic()
        {
            string rezultat = GetValoriDinListView();
            string[] t = rezultat.Split(',');

            double[] valori = new double[4];
            for (int i = 0; i < 4; i++)
            {
                valori[i] = double.Parse(t[i]);
            }

            grafic1.Valori = valori;
        }

        //TEXT DRAG DROP EVENT
        private void JudetDraggedTxtBox_DragDrop(object sender, DragEventArgs e)
        {
            string result = e.Data.GetData(DataFormats.StringFormat) as string;
            judetDraggedTxtBox.Text = result;
            JudeteSelectateLabel.Text = SalveazaJudeteSelectate;
        }

        //TEXT DRAG ENTER EVENT
        private void JudetDraggedTxtBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        //LIST VIEW MOUSE DOWN EVENT
        private void ListView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Indicatori indicatori = new Indicatori();
                SalveazaJudeteSelectate = "S-a selectat:\n";

                foreach(ListViewItem rand in listView1.SelectedItems)
                {
                    SalveazaJudeteSelectate += rand.SubItems[0].Text + "\n";
                    indicatori.TerenArabil += double.Parse(rand.SubItems[1].Text);
                    indicatori.Vii += double.Parse(rand.SubItems[2].Text);
                    indicatori.Livezi += double.Parse(rand.SubItems[3].Text);
                    indicatori.Pasuni += double.Parse(rand.SubItems[4].Text);
                }
                string result = indicatori.AfiseazaValori();
                listView1.DoDragDrop(result, DragDropEffects.Move);
            }
        }

        private void ObservatiiForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                PrintDocument document = grafic1.Print_Graf();
                PrintPreviewDialog ppd = new PrintPreviewDialog();
                ppd.Document = document;
                ppd.ShowDialog(grafic1);
            }

            if ((Control.ModifierKeys & Keys.Control) != 0)
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    //continue;
                }
            }
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

        private void GetTotalHa(out double terenA, out double vii, out double livezi, out double pasuni)
        {
            terenA = 0;
            vii = 0;
            livezi = 0;
            pasuni = 0;

            foreach(var unitate in Unitati)
            {
                terenA += unitate._Indicatori.TerenArabil;
                vii += unitate._Indicatori.Vii;
                livezi += unitate._Indicatori.Livezi;
                pasuni += unitate._Indicatori.Pasuni;
            }
        }

        void InitPieChart()
        {
            chart1.Titles.Add("Repartizare");
            chart1.Series["totalhaSeries"].IsValueShownAsLabel = true;
            chart1.Series["totalhaSeries"].Font = new Font("Times", 12f);

            double ta = 0, v = 0, l = 0, p = 0;
            GetTotalHa(out ta, out v, out l, out p);
            chart1.Series["totalhaSeries"].Points.AddXY("TerenArabil", ta);
            chart1.Series["totalhaSeries"].Points.AddXY("Vii", v);
            chart1.Series["totalhaSeries"].Points.AddXY("Livezi", l);
            chart1.Series["totalhaSeries"].Points.AddXY("Pășuni: ", p);

        }

        private void inchideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }       

        private void ListViewSelected_Mouse_Click(object sender, MouseEventArgs e)
        {
            listView1.Focus();
            listView1.FullRowSelect = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Printing.PrintPreview();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SeteazaValoriPentruGrafic();

            //foreach (var x in grafic1.Valori)
            //    MessageBox.Show(x + " ");

        }
    }
}
