using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hcgcad
{
    public partial class Form1 : Form
    {
        static Color[] pal;
        static Color[] pal_inv;
        static byte[] cgx;
        static byte[] scr;
        static int fmt;

        static int selectedPal = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "*.col;*.col.bak|*.col;*.col.bak|All files|*.*";
            o.Title = "Load COL file...";
            if (o.ShowDialog() == DialogResult.OK)
            {
                Stream file = o.OpenFile();

                if (file.Length != 0x400)
                {
                    file.Close();
                    return;
                }

                byte[] paldat = new byte[512];
                file.Read(paldat, 0, 512);
                file.Close();

                pal = GraphicsRender.Nintendo.PaletteFromByteArray(paldat);
                pal_inv = new Color[pal.Length];
                for (int i = 0; i < pal.Length; i++)
                {
                    if (i + 128 < pal.Length)
                        pal_inv[i] = pal[128 + i];
                    else
                        pal_inv[i] = pal[i - 128];
                }

                RenderCOL();
                RenderCGX();
                RenderSCR();

                label2.Text = "COL (" + Path.GetFileName(o.FileName) + "):";

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "*.cgx;*.cgx.bak|*.cgx;*.cgx.bak|All files|*.*";
            o.Title = "Load CGX file...";
            if (o.ShowDialog() == DialogResult.OK)
            {
                Stream file = o.OpenFile();

                if (file.Length != 0x4500 && file.Length != 0x8500 && file.Length != 0x10100)
                {
                    file.Close();
                    return;
                }

                cgx = new byte[file.Length];
                file.Read(cgx, 0, (int)file.Length);
                file.Close();

                //if (cgx.Length == 0x4500)
                fmt = 0;
                if (cgx.Length == 0x8500)
                    fmt = 1;
                else if (cgx.Length == 0x10100)
                    fmt = 2;

                RenderCGX();
                RenderSCR();

                label1.Text = "CGX (" + Path.GetFileName(o.FileName) + "):";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "*.scr;*.scr.bak|*.scr;*.scr.bak|All files|*.*";
            o.Title = "Load SCR file...";
            if (o.ShowDialog() == DialogResult.OK)
            {
                Stream file = o.OpenFile();

                if (file.Length != 0x2300)
                {
                    file.Close();
                    return;
                }

                scr = new byte[file.Length];
                file.Read(scr, 0, (int)file.Length);
                file.Close();

                RenderSCR();

                label3.Text = "SCR (" + Path.GetFileName(o.FileName) + "):";
            }
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (fmt == 0)
                selectedPal = (e.X / 64) + ((e.Y / 16) * 4);
            else if (fmt == 1)
                selectedPal = (e.Y / 16);
            else //if (fmt == 2)
                selectedPal = (e.Y / 128);

            RenderCOL();
            RenderCGX();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            selectedPal = 0;
            RenderCOL();
            RenderCGX();
        }

        private void RenderCOL()
        {
            if (pal == null)
                return;

            Color[] selPal = (!checkBox3.Checked) ? pal : pal_inv;
            Bitmap output = GraphicsRender.Nintendo.RenderPalette(selPal);

            if (checkBox1.Checked)
            {
                if (fmt == 0)
                    using (Graphics g = Graphics.FromImage(output))
                        g.DrawRectangle(new Pen(Brushes.Red), (selectedPal % 4) * 64, (selectedPal / 4) * 16, (16 * 4) - 1, 16 - 1);
                else if (fmt == 1)
                    using (Graphics g = Graphics.FromImage(output))
                        g.DrawRectangle(new Pen(Brushes.Red), 0, (selectedPal % 16) * 16, (16 * 16) - 1, 16 - 1);
                else //if (fmt == 2)
                    using (Graphics g = Graphics.FromImage(output))
                        g.DrawRectangle(new Pen(Brushes.Red), 0, (selectedPal % 2) * 128, (16 * 16) - 1, 128 - 1);
            }

            pictureBox2.Image = output;
        }

        private void RenderCGX()
        {
            if (cgx == null || pal == null)
                return;

            Color[] selPal = (!checkBox3.Checked) ? pal : pal_inv;

            if (!checkBox1.Checked)
                pictureBox1.Image = GraphicsRender.Nintendo.RenderCGX(cgx, selPal, 2);
            else
                pictureBox1.Image = GraphicsRender.Nintendo.RenderCGX(cgx, selPal, 2, selectedPal);
            pictureBox1.Size = pictureBox1.Image.Size;
        }

        private void RenderSCR()
        {
            if (cgx == null || pal == null || scr == null)
                return;

            Color[] selPal = (!checkBox3.Checked) ? pal : pal_inv;
            pictureBox3.Image = GraphicsRender.Nintendo.RenderSCR(scr, cgx, selPal, 1, checkBox2.Checked);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            RenderSCR();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            RenderCOL();
            RenderCGX();
            RenderSCR();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            scr = null;
            pictureBox3.Image = null;
            label3.Text = "SCR:";
        }
    }
}
