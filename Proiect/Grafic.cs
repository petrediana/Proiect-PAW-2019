using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;

namespace ProiectPaw
{
    class Grafic : Control
    {
        double[] valori = new double[0];        

        public Grafic()
        {
            Paint += Grafic_Paint;
            //KeyDown += Grafic_KeyDown;
        }

        //private void Grafic_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Control && e.KeyCode == Keys.P)
        //    {
        //        MessageBox.Show(string.Format("Control + P"));

        //        PrintDocument document = new PrintDocument();
        //        document.PrintPage += (s, ea) =>
        //        {
        //            Desenare(ea.Graphics, ea.MarginBounds);
        //            ea.HasMorePages = false;
        //        };

        //        PrintPreviewDialog ppd = new PrintPreviewDialog();
        //        ppd.Document = document;
        //        ppd.ShowDialog(this);
        //    }
        //}

        public PrintDocument Print_Graf()
        {
            PrintDocument printdoc = new PrintDocument();
            printdoc.PrintPage += (s, ea) =>
            {
                Desenare(ea.Graphics, ea.MarginBounds);
                ea.HasMorePages = false;
            };

            return printdoc;
        }

        public double[] Valori
        {
            get { return valori; }
            set
            {
                try
                {
                    if (value != null)
                    {
                        foreach(var vals in value)
                        {
                            if (vals < 0)
                                throw new ExceptieNrNegativ();
                        }

                        valori = value;
                        Invalidate();
                    }
                }
                catch (ExceptieNrNegativ ex)
                {
                    MessageBox.Show(string.Format(ex.mesaj()));
                }
            }
        }

        private void Grafic_Paint(object sender, PaintEventArgs e)
        {
            Desenare(e.Graphics, DisplayRectangle);
        }

        private void Desenare(Graphics g, Rectangle r)
        {
            g.FillRectangle(Brushes.White, r);

            if (valori.Length == 0)
            {
                return;
            }

            double W = r.Width, H = r.Height;
            int n = valori.Length;
            double w = W / n, f = ((double)H / valori.Max());

            for (var i = 0; i < n; i++)
            {
                double h = valori[i] * f;
                RectangleF rElem = new RectangleF((float)(r.Left + i * w + 0.1f * w), (float)(r.Top + H - h),
                                    50, (float)h);
                // (float)w * 0.8f -> width;

                g.FillRectangle(Brushes.Red, rElem);
            }

            //RectangleF rElem = new RectangleF(100, 100, 100, 10);
            //g.FillRectangle(Brushes.Red, rElem);
        }
    }
}
