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
    public partial class FormViewer : Form
    {
        static Color[] pal;
        static Color[] pal_inv;
        static byte[] cgx;
        static byte[] scr;
        static int fmt;

        static int selectedPal = 0;

        public FormViewer()
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
            o.Multiselect = true;
            o.Filter = "All Supported Files|*.col;*.col.bak;*.cgx;*.cgx.bak;*.scr;*.scr.bak|COL (CGRAM) Files|*.col;*.col.bak|CGX (Graphics) Files|*.cgx;*.cgx.bak|SCR (Screen) Files|*.scr;*.scr.bak|All files|*.*";
            o.Title = "Load SCAD files...";
            if (o.ShowDialog() == DialogResult.OK)
            {
                bool loadedCOL = false;
                bool loadedCGX = false;
                bool loadedSCR = false;

                foreach (string p in o.FileNames)
                {
                    FileStream file = File.Open(p, FileMode.Open, FileAccess.Read);

                    if (LoadCOL(file))
                    {
                        labelCOL.Text = "COL (" + Path.GetFileName(p) + "):";
                        loadedCOL = true;
                    }
                    else if (LoadCGX(file))
                    {
                        labelCGX.Text = "CGX (" + Path.GetFileName(p) + "):";
                        loadedCGX = true;
                    }
                    else if (LoadSCR(file))
                    {
                        labelSCR.Text = "SCR (" + Path.GetFileName(p) + "):";
                        loadedSCR = true;
                    }

                    file.Close();
                }

                if (loadedCOL)
                {
                    RenderCOL();
                    RenderCGX();
                    RenderSCR();
                }
                else if (loadedCGX)
                {
                    RenderCGX();
                    RenderSCR();
                }
                else if (loadedSCR)
                {
                    RenderSCR();
                }
            }
        }

        private bool LoadCOL(FileStream file)
        {
            //COL
            file.Seek(0, SeekOrigin.Begin);

            //Check File Size
            if (file.Length != 0x400)
                return false;

            byte[] paldat = new byte[512];
            file.Read(paldat, 0, 512);
            byte[] palftr = new byte[512];
            file.Read(palftr, 0, 512);

            //Check Footer Info
            string footer_string = System.Text.Encoding.ASCII.GetString(Program.Subarray(palftr, 0, 0x10));
            if (!footer_string.Equals("NAK1989 S-CG-CAD"))
                return false;

            pal = GraphicsRender.Nintendo.PaletteFromByteArray(paldat);
            pal_inv = new Color[pal.Length];
            for (int i = 0; i < pal.Length; i++)
            {
                if (i + 128 < pal.Length)
                    pal_inv[i] = pal[128 + i];
                else
                    pal_inv[i] = pal[i - 128];
            }
            return true;
        }

        private bool LoadCGX(FileStream file)
        {
            //CGX
            file.Seek(0, SeekOrigin.Begin);

            //Check File Size
            if (file.Length != 0x4500 && file.Length != 0x8500 && file.Length != 0x10100)
                return false;

            int off_hdr = 0x4000;
            if (file.Length == 0x8500)
                off_hdr = 0x8000;
            else if (file.Length == 0x10100)
                off_hdr = 0x10000;

            byte[] cgx_t = new byte[file.Length];
            file.Read(cgx_t, 0, (int)file.Length);

            //Check Footer Info
            string footer_string = System.Text.Encoding.ASCII.GetString(Program.Subarray(cgx_t, off_hdr, 0x10));
            if (!footer_string.Equals("NAK1989 S-CG-CAD"))
                return false;

            //if (cgx_t.Length == 0x4500)
            fmt = 0;
            if (cgx_t.Length == 0x8500)
                fmt = 1;
            else if (cgx_t.Length == 0x10100)
                fmt = 2;

            cgx = cgx_t;
            return true;
        }

        private bool LoadSCR(FileStream file)
        {
            //SCR
            file.Seek(0, SeekOrigin.Begin);

            //Check File Size
            if (file.Length != 0x2300)
                return false;

            byte[] scr_t = new byte[file.Length];
            file.Read(scr_t, 0, (int)file.Length);

            //Check Footer Info
            string footer_string = System.Text.Encoding.ASCII.GetString(Program.Subarray(scr_t, 0x2000, 0x10));
            if (!footer_string.Equals("NAK1989 S-CG-CAD"))
                return false;

            scr = scr_t;
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
