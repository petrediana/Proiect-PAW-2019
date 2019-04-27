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
    public partial class VizualizareUnitatiForm : Form
    {
        public List<UnitateAgricola> Unitati = new List<UnitateAgricola>();

        public VizualizareUnitatiForm(List<UnitateAgricola> unitati)
        {
            InitializeComponent();
            Unitati = unitati;

            listView1.Dock = DockStyle.Fill;
            listView1.View = View.Details;
            listView1.Columns.Add("Denumire Unitate: ", 120);
            listView1.Columns.Add("CUI: ", 100);
            listView1.Columns.Add("Județ: ", 80);
            listView1.Columns.Add("Strada ", 120);
            listView1.Columns.Add("Numarul: ", 80);
            listView1.Columns.Add("Teren arabil (ha): ", 100);
            listView1.Columns.Add("Vie (ha): ", 100);
            listView1.Columns.Add("Livadă (ha): ", 100);
            listView1.Columns.Add("Pășune (ha): ", 100);

            Afisare();
        }

        void Afisare()
        {
            listView1.Items.Clear();
            //foreach(var unitate in Unitati.OrderByDescending(m => m._Adresa.Judet))
            foreach(var unitate in Unitati.OrderBy(m => m._Adresa.Judet))
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

        private void Enter_cautaCUI(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                bool ok = false;
                string txt = CUItextBox.Text;
                try
                {
                    if (string.IsNullOrEmpty(txt))
                        throw new ExceptieDenumire();
                    else
                        if (txt.Length != 12 || txt.Substring(0, 2) != "RO" || !txt.Substring(2, 9).All(char.IsDigit))
                        throw new ExceptieCUI();
                    else
                    {
                        //foreach (var unitate in Unitati)
                        //{
                        //    if (unitate.CUI == txt)
                        //    {
                        //        MessageBox.Show(string.Format(unitate.ToString()), "Codul unitatii cautat: "
                        //            + txt, MessageBoxButtons.OK);
                        //        ok = true;
                        //        break;
                        //    }
                        //}
                        //if (!ok)
                        //{
                        //    MessageBox.Show("Nu exista aceasta unitate!");
                        //}

                    foreach(var unitate in Unitati)
                        {
                            if (unitate.CUI == txt)
                            {
                                listView1.Items.Clear();

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

                                ok = true;
                                break;
                            }
                        }
                        if (!ok)
                        {
                            MessageBox.Show("Nu exista aceasta unitate!");
                        }
                    }
                }
                catch(ExceptieCUI ex)
                {
                    MessageBox.Show(ex.message_format());
                }
                catch(ExceptieDenumire ex)
                {
                    MessageBox.Show(ex.mesaj());
                }
                catch(Exception ex)
                {
                    MessageBox.Show(string.Format("Nu se poate! Alta exceptie ({0})", ex.Message));
                }
            }
        }

        private void toateUnitatiileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Afisare();
        }

        private void Select_unitatiLV(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
               DialogResult dialog = MessageBox.Show("Doriti sa modificati aceasta unitate?", "Denumire unitate: "
                    + listView1.SelectedItems[0].Text + ", CUI: " + listView1.SelectedItems[0].SubItems[1].Text,
                    MessageBoxButtons.OKCancel);

                if(dialog == DialogResult.OK)
                {
                    Adresa adr = new Adresa(listView1.SelectedItems[0].SubItems[2].Text,
                       listView1.SelectedItems[0].SubItems[3].Text, int.Parse(listView1.SelectedItems[0]
                       .SubItems[4].Text));

                    Indicatori ind = new Indicatori(double.Parse(listView1.SelectedItems[0].SubItems[5].Text),
                        double.Parse(listView1.SelectedItems[0].SubItems[6].Text),
                        double.Parse(listView1.SelectedItems[0].SubItems[7].Text),
                        double.Parse(listView1.SelectedItems[0].SubItems[8].Text));

                    ModificaDateForm modificaDate = new ModificaDateForm(listView1.SelectedItems[0].Text,
                        adr, ind);

                    modificaDate.ShowDialog();
                }
                
            }
        }
    }
}
