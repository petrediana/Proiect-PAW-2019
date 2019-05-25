using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ProiectPaw
{
    public partial class ListaJudete : UserControl
    {
        string CaleFisier = @"C:\Users\Peanutt\Desktop\VS\C#\ProiectPaw\ProiectPaw\ListaJudete.txt";
        public ComboBox theComboBox = new ComboBox();        

        public ListaJudete()
        {
            InitializeComponent();
            PuneDateInComboBox();
            theComboBox = comboBox1;            
        }
        
        public string ReturnJudetSelectat()
        {
            string aa = string.Empty;
            if (comboBox1.SelectedItem == null)
                return string.Empty;
            else
            {
                aa = comboBox1.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(aa))
                    return string.Format(aa);
                else
                    return string.Empty;
            }
        }

        public List<string> InstantiazaListaJudete()
        {
            List<string> judete = new List<string>();

            string[] linii = File.ReadAllLines(CaleFisier);
            for (int i = 0; i < 42; i++)
            {
                judete.Add(linii[i]);
            }

            return judete;
        }

        private void PuneDateInComboBox()
        {
            List<string> judete = InstantiazaListaJudete();
            foreach (var judet in judete)
                comboBox1.Items.Add(judet);
        }
    }
}
