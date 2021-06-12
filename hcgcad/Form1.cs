using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
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

            Color[] selPal = (!checkBoxCGRAMSwap.Checked) ? pal : pal_inv;
            Bitmap output = GraphicsRender.Nintendo.RenderPalette(selPal);

            if (checkBoxPalForce.Checked)
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

            pictureBoxCOL.Image = output;
        }

        private void RenderCGX()
        {
            if (cgx == null || pal == null)
                return;

            pictureBoxCGX.Image = GraphicsRender.Nintendo.RenderCGX(cgx, (!checkBoxCGRAMSwap.Checked) ? pal : pal_inv, 2, checkBoxPalForce.Checked ? selectedPal : -1);
            pictureBoxCGX.Size = pictureBoxCGX.Image.Size;
        }

        private void RenderSCR()
        {
            if (cgx == null || pal == null || scr == null)
                return;

            pictureBoxSCR.Image = GraphicsRender.Nintendo.RenderSCR(scr, cgx, (!checkBoxCGRAMSwap.Checked) ? pal : pal_inv, 1, checkBoxVisibleTiles.Checked);
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
            pictureBoxSCR.Image = null;
            labelSCR.Text = "SCR:";
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "All Supported Files|*.col;*.col.bak;*.cgx;*.cgx.bak;*.scr;*.scr.bak|COL (CGRAM) Files|*.col;*.col.bak|CGX (Graphics) Files|*.cgx;*.cgx.bak|SCR (Screen) Files|*.scr;*.scr.bak|All files|*.*";
            o.Title = "Load SCAD file...";
            if (o.ShowDialog() == DialogResult.OK)
            {
                if (LoadCOL(o.OpenFile()))
                    labelCOL.Text = "COL (" + Path.GetFileName(o.FileName) + "):";
                else if (LoadCGX(o.OpenFile()))
                    labelCGX.Text = "CGX (" + Path.GetFileName(o.FileName) + "):";
                else if (LoadSCR(o.OpenFile()))
                    labelSCR.Text = "SCR (" + Path.GetFileName(o.FileName) + "):";
            }
        }

        private bool LoadCOL(Stream file)
        {
            //COL
            if (file.Length != 0x400)
            {
                file.Close();
                return false;
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
            return true;
        }

        private bool LoadCGX(Stream file)
        {
            //CGX
            if (file.Length != 0x4500 && file.Length != 0x8500 && file.Length != 0x10100)
            {
                file.Close();
                return false;
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
            return true;
        }

        private bool LoadSCR(Stream file)
        {
            //SCR
            if (file.Length != 0x2300)
            {
                file.Close();
                return false;
            }

            scr = new byte[file.Length];
            file.Read(scr, 0, (int)file.Length);
            file.Close();

            RenderSCR();
            return true;
        }

        private void exportCGXAsPNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PNG Image|*.png";
            sfd.Title = "Save CGX Output...";
            ImageFormat format = ImageFormat.Png;
            sfd.FileName = labelCGX.Text.Substring(5, labelCGX.Text.Length - 5 - 2);
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                GraphicsRender.Nintendo.RenderCGX(cgx, (!checkBoxCGRAMSwap.Checked) ? pal : pal_inv, 1, checkBoxPalForce.Checked ? selectedPal : -1).Save(sfd.FileName, format);
            }
        }

        private void exportSCRAsPNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PNG Image|*.png";
            sfd.Title = "Save SCR Output...";
            ImageFormat format = ImageFormat.Png;
            sfd.FileName = labelSCR.Text.Substring(5, labelCGX.Text.Length - 5 - 2);
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                GraphicsRender.Nintendo.RenderSCR(scr, cgx, (!checkBoxCGRAMSwap.Checked) ? pal : pal_inv, 1, checkBoxVisibleTiles.Checked).Save(sfd.FileName, format);
            }
        }
    }
}
