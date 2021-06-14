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
        static int fmt;     //for CGX
        static byte[] obj;

        static int selectedPal = 0;

        public FormViewer()
        {
            InitializeComponent();
            comboBoxCHRBANK.SelectedIndex = 0;
            comboBoxOBJSize.SelectedIndex = 0;
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

            pictureBoxCGX.Image = GraphicsRender.ScaleBitmap(GraphicsRender.Nintendo.RenderCGX(cgx, (!checkBoxCGRAMSwap.Checked) ? pal : pal_inv, checkBoxPalForce.Checked ? selectedPal : -1), 2);
            pictureBoxCGX.Size = pictureBoxCGX.Image.Size;
        }

        private void RenderSCR()
        {
            if (cgx == null || pal == null || scr == null)
                return;

            pictureBoxSCR.Image = GraphicsRender.Nintendo.RenderSCR(scr, cgx, (!checkBoxCGRAMSwap.Checked) ? pal : pal_inv, checkBoxVisibleTiles.Checked);
        }

        private void RenderOBJ()
        {
            if (cgx == null || pal == null || obj == null)
                return;

            pictureBoxSCR.Image = GraphicsRender.ScaleBitmap(GraphicsRender.Nintendo.RenderOBJ((int)numericUpDownFrame.Value, obj, cgx, (!checkBoxCGRAMSwap.Checked) ? pal : pal_inv, (byte)comboBoxOBJSize.SelectedIndex, (byte)comboBoxCHRBANK.SelectedIndex), 2);
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
            obj = null;
            pictureBoxSCR.Image = null;
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Multiselect = true;
            o.Filter = "All Supported Files|*.col;*.col.bak;*.cgx;*.cgx.bak;*.scr;*.scr.bak;*.obj;*.obj.bak|Color Files (*.col)|*.col;*.col.bak|Character Graphics Files (*.cgx)|*.cgx;*.cgx.bak|Screen Files (*.scr)|*.scr;*.scr.bak|Object Files (*.obj)|*.obj;*.obj.bak|All files|*.*";
            o.Title = "Load SCAD files...";
            if (o.ShowDialog() == DialogResult.OK)
            {
                bool loadedCOL = false;
                bool loadedCGX = false;
                bool loadedSCR = false;
                bool loadedOBJ = false;

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
                        obj = null;
                    }
                    else if (LoadOBJ(file))
                    {
                        labelSCR.Text = "OBJ (" + Path.GetFileName(p) + "):";
                        loadedOBJ = true;
                        scr = null;
                    }

                    file.Close();
                }

                if (loadedCOL)
                {
                    RenderCOL();
                    RenderCGX();
                    RenderSCR();
                    RenderOBJ();
                }
                else if (loadedCGX)
                {
                    RenderCGX();
                    RenderSCR();
                    RenderOBJ();
                }
                else if (loadedSCR)
                {
                    RenderSCR();
                }
                else if (loadedOBJ)
                {
                    RenderOBJ();
                }
            }
        }

        private bool LoadCOL(FileStream file)
        {
            //COL
            file.Seek(0, SeekOrigin.Begin);

            //Check File Size
            if (file.Length != 0x200 && file.Length != 0x400)
                return false;

            byte[] paldat = new byte[512];
            file.Read(paldat, 0, 512);
            if (file.Length == 0x400)
            {
                byte[] palftr = new byte[512];
                file.Read(palftr, 0, 512);

                //Check Footer Info
                string footer_string = System.Text.Encoding.ASCII.GetString(Program.Subarray(palftr, 0, 0x10));
                if (!footer_string.Equals("NAK1989 S-CG-CAD"))
                    return false;
            }

            pal = GraphicsRender.Nintendo.PaletteFromByteArray(paldat);
            pal_inv = Program.Subarray(pal, 128, 256);
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

        private bool LoadOBJ(FileStream file)
        {
            //OBJ
            file.Seek(0, SeekOrigin.Begin);

            //Check File Size
            if (file.Length != 0x3500)
                return false;

            byte[] obj_t = new byte[file.Length];
            file.Read(obj_t, 0, (int)file.Length);

            //Check Footer Info
            string footer_string = System.Text.Encoding.ASCII.GetString(Program.Subarray(obj_t, 0x3000, 0x10));
            if (!footer_string.Equals("NAK1989 S-CG-CAD"))
                return false;

            obj = obj_t;
            return true;
        }

        private void exportCGXAsPNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pal == null && cgx == null)
                return;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PNG Image|*.png";
            sfd.Title = "Save CGX Output...";
            ImageFormat format = ImageFormat.Png;
            sfd.FileName = labelCGX.Text.Substring(5, labelCGX.Text.Length - 5 - 2);
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                GraphicsRender.Nintendo.RenderCGX(cgx, (!checkBoxCGRAMSwap.Checked) ? pal : pal_inv, checkBoxPalForce.Checked ? selectedPal : -1).Save(sfd.FileName, format);
            }
        }

        private void exportSCRAsPNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pal == null || cgx == null || scr == null)
                return;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PNG Image|*.png";
            sfd.Title = "Save SCR Output...";
            ImageFormat format = ImageFormat.Png;
            sfd.FileName = labelSCR.Text.Substring(5, labelCGX.Text.Length - 5 - 2);
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                GraphicsRender.Nintendo.RenderSCR(scr, cgx, (!checkBoxCGRAMSwap.Checked) ? pal : pal_inv, checkBoxVisibleTiles.Checked).Save(sfd.FileName, format);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            RenderOBJ();
        }

        private void exportOBJAsGIFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pal == null || cgx == null || obj == null)
                return;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "GIF Animation|*.gif";
            sfd.Title = "Save OBJ Output...";
            sfd.FileName = labelSCR.Text.Substring(5, labelCGX.Text.Length - 5 - 2);

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Bitmap[] frames = new Bitmap[32];
                for (int i = 0; i < 32; i++)
                {
                    frames[i] = GraphicsRender.Nintendo.RenderOBJ(i, obj, cgx, (!checkBoxCGRAMSwap.Checked) ? pal : pal_inv, (byte)comboBoxOBJSize.SelectedIndex, (byte)comboBoxCHRBANK.SelectedIndex);
                }

                Rectangle rect = Program.GetBoundingRect(frames);
                for (int i = 0; i < 32; i++)
                {
                    frames[i] = frames[i].Clone(rect, PixelFormat.Format32bppArgb);
                }

                Program.SaveGIF(sfd.FileName, frames, pal);

                MessageBox.Show("Done");
            }
        }
    }
}
