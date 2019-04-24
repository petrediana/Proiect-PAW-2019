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
    public partial class PaginaStart : Form
    {
        public List<UnitateAgricola> Unitati = new List<UnitateAgricola>();
        public PaginaStart(List<UnitateAgricola> unitati)
        {
            InitializeComponent();
            Unitati = unitati;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            MeniuAplicatie meniu = new MeniuAplicatie(Unitati);
            meniu.ShowDialog();
            Close();
        }
    }
}
