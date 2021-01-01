using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paradox
{
    public partial class Form1 : Form
    {
        Graphics grp1;
        Graphics grp2;

        Door d1, d2, d3;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            grp1 = panel1.CreateGraphics();
            grp2 = panel2.CreateGraphics();

            grp1.FillRectangle(Brushes.Black, 10, 10, 70, 100);
            grp1.FillRectangle(Brushes.Black, 100, 10, 70, 100);
            grp1.FillRectangle(Brushes.Black, 190, 10, 70, 100);

            grp2.FillRectangle(Brushes.Black, 10, 10, 70, 100);
            grp2.FillRectangle(Brushes.Black, 100, 10, 70, 100);
            grp2.FillRectangle(Brushes.Black, 190, 10, 70, 100);

            hScrollBar2.Value = Convert.ToInt32(textBox1.Text);
        }

        private void hScrollBar2_ValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = Convert.ToString(hScrollBar2.Value);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (Convert.ToInt32(textBox1.Text) <= 100)
                {
                    hScrollBar2.Value = Convert.ToInt32(textBox1.Text);
                }
            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int v = (100 - hScrollBar1.Value) * 10;

            bool change;

            double f1 = 0, f2 = 0;  // вероятность

            List<bool> changed = new List<bool>();
            List<bool> unchanged = new List<bool>();

            int iter = Convert.ToInt32(textBox1.Text);

            for (int i = 0; i < iter; i++)
            {
                grp1.FillRectangle(Brushes.Black, 10, 10, 70, 100);
                grp1.FillRectangle(Brushes.Black, 100, 10, 70, 100);
                grp1.FillRectangle(Brushes.Black, 190, 10, 70, 100);

                grp2.FillRectangle(Brushes.Black, 10, 10, 70, 100);
                grp2.FillRectangle(Brushes.Black, 100, 10, 70, 100);
                grp2.FillRectangle(Brushes.Black, 190, 10, 70, 100);

                change = false;

                Random r = new Random();
                double r1 = r.NextDouble();

                if (r1 < 0.3333)  // рисовка двери
                {
                    d1 = new Door(true, false);
                    d2 = new Door(false, false);
                    d3 = new Door(false, false);
                }
                else if (r1 >= 0.333 && r1 < 0.666)
                {
                    d1 = new Door(false, false);
                    d2 = new Door(true, false);
                    d3 = new Door(false, false);
                }
                else if (r1 >= 0.666)
                {
                    d1 = new Door(false, false);
                    d2 = new Door(false, false);
                    d3 = new Door(true, false);
                }

                r1 = r.NextDouble();

                if (r1 < 0.3333)  // выбор двери
                {
                    d1.isSelected = true;
                    grp1.FillRectangle(Brushes.Yellow, 10, 10, 70, 100);
                }
                else if (r1 >= 0.333 && r1 < 0.666)
                {
                    d2.isSelected = true;
                    grp1.FillRectangle(Brushes.Yellow, 100, 10, 70, 100);
                }
                else if (r1 >= 0.666)
                {
                    d3.isSelected = true;
                    grp1.FillRectangle(Brushes.Yellow, 190, 10, 70, 100);
                }

                Door[] doors = { d1, d2, d3 };

                foreach (Door d in doors)  // открыть дверь
                {
                    if (!d.isSelected && !d.isGifted)
                    {
                        d.isOpened = true;
                        break;
                    }
                }

                if (d1.isOpened)
                {
                    grp1.FillRectangle(Brushes.Magenta, 10, 10, 70, 100);
                    grp2.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240, 240)), 10, 10, 70, 100);
                }
                else if (d2.isOpened)
                {
                    grp1.FillRectangle(Brushes.Magenta, 100, 10, 70, 100);
                    grp2.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240, 240)), 100, 10, 70, 100);
                }
                else
                {
                    grp1.FillRectangle(Brushes.Magenta, 190, 10, 70, 100);
                    grp2.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240, 240)), 190, 10, 70, 100);
                }

                r1 = r.NextDouble();

                if (r1 < 0.5)   // менять ли дверь
                {
                    change = true;
                    foreach (Door d in doors)
                    {
                        if (!d.isSelected && !d.isOpened)
                        {
                            d.isSelected = true;
                        }
                        else
                        {
                            d.isSelected = false;
                        }
                    }
                }

                if (d1.isGifted && d1.isSelected)  // выиграл
                {
                    if (change)
                    {
                        changed.Add(true);
                    } else
                    {
                        unchanged.Add(true);
                    }
                    grp2.FillRectangle(Brushes.Green, 10, 10, 70, 100);
                }
                else if (d2.isGifted && d2.isSelected)
                {
                    if (change)
                    {
                        changed.Add(true);
                    }
                    else
                    {
                        unchanged.Add(true);
                    }
                    grp2.FillRectangle(Brushes.Green, 100, 10, 70, 100);
                }
                else if (d3.isGifted && d3.isSelected)
                {
                    if (change)
                    {
                        changed.Add(true);
                    }
                    else
                    {
                        unchanged.Add(true);
                    }
                    grp2.FillRectangle(Brushes.Green, 190, 10, 70, 100);
                }
                else if (d1.isGifted && !d1.isSelected)  // не выиграл
                {
                    if (change)
                    {
                        changed.Add(false);
                    }
                    else
                    {
                        unchanged.Add(false);
                    }
                    grp2.FillRectangle(Brushes.Red, 10, 10, 70, 100);
                }
                else if (d2.isGifted && !d2.isSelected)
                {
                    if (change)
                    {
                        changed.Add(false);
                    }
                    else
                    {
                        unchanged.Add(false);
                    }
                    grp2.FillRectangle(Brushes.Red, 100, 10, 70, 100);
                }
                else if (d3.isGifted && !d3.isSelected)
                {
                    if (change)
                    {
                        changed.Add(false);
                    }
                    else
                    {
                        unchanged.Add(false);
                    }
                    grp2.FillRectangle(Brushes.Red, 190, 10, 70, 100);
                }
                if (changed.Count != 0 && unchanged.Count != 0)
                {
                    int k1 = 0, k2 = 0;

                    foreach (bool b in changed)
                    {
                        if (b)
                        {
                            k1++;
                        }
                    }

                    foreach (bool b in unchanged)
                    {
                        if (b)
                        {
                            k2++;
                        }
                    }

                    f1 = (double)k1 / changed.Count;
                    f2 = (double)k2 / unchanged.Count;

                    chart1.Series[0].Points.AddXY(f1, changed.Count);
                    chart1.Series[1].Points.AddXY(f2, unchanged.Count);

                    chart1.Refresh();

                    label2.Text = $"Changed:{f1}   Unchanged:{f2}";

                    label2.Refresh();
                }

                System.Threading.Thread.Sleep(v);
            }
        }
    }
}
