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
    public partial class MeniuAplicatieForm : Form
    {
        public List<UnitateAgricola> Unitati = new List<UnitateAgricola>();
        public MeniuAplicatieForm(List<UnitateAgricola> unitati)
        {
            InitializeComponent();
            Unitati = unitati;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void inapoiButon_Click(object sender, EventArgs e)
        {
            Hide();
            PaginaStartForm paginaStart = new PaginaStartForm(Unitati);
            paginaStart.ShowDialog();
            Close();
        }

        private void adaugaUnitate_button_Click(object sender, EventArgs e)
        {
            AdaugaUnitateAgricola adaugaUnitateAgricola = new AdaugaUnitateAgricola(Unitati);
            adaugaUnitateAgricola.ShowDialog();
        }

        private void database_button_Click(object sender, EventArgs e)
        {
            VizualizareUnitatiForm vizualizareUnitati = new VizualizareUnitatiForm(Unitati);
            vizualizareUnitati.ShowDialog();

        }

        private void MatObs_button_Click(object sender, EventArgs e)
        {
            ObservatiiForm observatiiForm = new ObservatiiForm(Unitati);
            observatiiForm.ShowDialog();
        }
    }
}
