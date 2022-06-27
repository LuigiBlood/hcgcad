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

namespace hcgcadviewer
{
    public partial class FormViewer : Form
    {
        static CAD.COL cad_col;
        static CAD.CGX cad_cgx;
        static CAD.SCR cad_scr;
        static CAD.OBJ cad_obj;

        static int selectedPal = 0;

        string col_filename;
        string cgx_filename;
        string scr_filename;
        string obj_filename;

        public FormViewer()
        {
            InitializeComponent();
            comboBoxCHRBANK.SelectedIndex = 0;
            comboBoxOBJSize.SelectedIndex = 0;
            radioButtonOBJRaw.Checked = true;
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (cad_cgx.GetFormat() == 0)
                selectedPal = (e.X / 64) + ((e.Y / 16) * 4);
            else if (cad_cgx.GetFormat() == 1)
                selectedPal = (e.Y / 16);
            else //if (cad_cgx.GetFormat() == 2)
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
            if (cad_col == null)
                return;

            Bitmap output = cad_col.RenderPalette();

            if (checkBoxPalForce.Checked)
            {
                if (cad_cgx.GetFormat() == 0)
                    using (Graphics g = Graphics.FromImage(output))
                        g.DrawRectangle(new Pen(Brushes.Red), (selectedPal % 4) * 64, (selectedPal / 4) * 16, (16 * 4) - 1, 16 - 1);
                else if (cad_cgx.GetFormat() == 1)
                    using (Graphics g = Graphics.FromImage(output))
                        g.DrawRectangle(new Pen(Brushes.Red), 0, (selectedPal % 16) * 16, (16 * 16) - 1, 16 - 1);
                else //if (cad_cgx.GetFormat() == 2)
                    using (Graphics g = Graphics.FromImage(output))
                        g.DrawRectangle(new Pen(Brushes.Red), 0, (selectedPal % 2) * 128, (16 * 16) - 1, 128 - 1);
            }

            pictureBoxCOL.Image = output;
        }

        private void RenderCGX()
        {
            if (cad_cgx == null || cad_col == null)
                return;

            pictureBoxCGX.Image = Render.ScaleBitmap(cad_cgx.Render(cad_col, checkBoxPalForce.Checked ? selectedPal : -1), 2);
            pictureBoxCGX.Size = pictureBoxCGX.Image.Size;
        }

        private void RenderSCR()
        {
            if (cad_cgx == null || cad_col == null || cad_scr == null)
                return;

            pictureBoxSCR.Image = cad_scr.Render(cad_cgx, cad_col, checkBoxVisibleTiles.Checked);
        }

        private void RenderOBJ()
        {
            if (cad_cgx == null || cad_col == null || cad_obj == null)
                return;
            if (radioButtonOBJRaw.Checked)
                pictureBoxSCR.Image = Render.ScaleBitmap(cad_obj.Render((int)numericUpDownFrame.Value, cad_cgx, cad_col, (byte)comboBoxOBJSize.SelectedIndex, (byte)comboBoxCHRBANK.SelectedIndex), 2);
            else
                pictureBoxSCR.Image = Render.ScaleBitmap(cad_obj.Render((int)numericUpDownOBJSeq.Value, (int)numericUpDownFrame.Value, cad_cgx, cad_col, (byte)comboBoxOBJSize.SelectedIndex, (byte)comboBoxCHRBANK.SelectedIndex), 2);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            RenderSCR();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            cad_col.SetPaletteSwap(checkBoxCGRAMSwap.Checked);
            RenderCOL();
            RenderCGX();
            RenderSCR();
            RenderOBJ();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cad_scr = null;
            cad_obj = null;
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
                loadAssets(o.FileNames);
            }
        }

        private bool LoadCOL(FileStream file)
        {
            //COL
            CAD.COL test = CAD.COL.Load(file);
            if (test != null)
                cad_col = test;
            return (test != null);
        }

        private bool LoadCGX(FileStream file)
        {
            //CGX
            CAD.CGX test = CAD.CGX.Load(file);
            if (test != null)
                cad_cgx = test;
            return (test != null);
        }

        private bool LoadSCR(FileStream file)
        {
            CAD.SCR test = CAD.SCR.Load(file);
            if (test != null)
                cad_scr = test;
            return (test != null);
        }

        private bool LoadOBJ(FileStream file)
        {
            CAD.OBJ test = CAD.OBJ.Load(file);
            if (test != null)
                cad_obj = test;
            return (test != null);
        }

        private void exportCGXAsPNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cad_col == null && cad_cgx == null)
                return;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PNG Image|*.png";
            sfd.Title = "Save CGX Output...";
            ImageFormat format = ImageFormat.Png;
            sfd.FileName = Path.GetFileNameWithoutExtension(cgx_filename) + "_cgx";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                cad_cgx.Render(cad_col, checkBoxPalForce.Checked ? selectedPal : -1).Save(sfd.FileName, format);
            }
        }

        private void exportSCRAsPNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cad_col == null || cad_cgx == null || cad_scr == null)
                return;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PNG Image|*.png";
            sfd.Title = "Save SCR Output...";
            ImageFormat format = ImageFormat.Png;
            sfd.FileName = Path.GetFileNameWithoutExtension(scr_filename) + "_scr";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                cad_scr.Render(cad_cgx, cad_col).Save(sfd.FileName, format);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            UpdateOBJGroupBox();
            RenderOBJ();
        }

        private void exportOBJAsGIFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cad_col == null || cad_cgx == null || cad_obj == null)
                return;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "GIF Animation|*.gif";
            sfd.Title = "Save OBJ Output...";
            sfd.FileName = Path.GetFileNameWithoutExtension(obj_filename) + "_obj";
            if (radioButtonOBJSeq.Checked)
            {
                sfd.FileName += "_seq" + numericUpDownOBJSeq.Value;
            }

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Bitmap[] frames;
                int[] duration;
                bool render = true;
                if (radioButtonOBJRaw.Checked)
                {
                    frames = new Bitmap[32];
                    duration = new int[32];
                    for (int i = 0; i < 32; i++)
                    {
                        frames[i] = cad_obj.Render(i, cad_cgx, cad_col, (byte)comboBoxOBJSize.SelectedIndex, (byte)comboBoxCHRBANK.SelectedIndex);
                        duration[i] = 10;
                    }
                }
                else
                {
                    render = cad_obj.RenderOBJAnim((int)numericUpDownOBJSeq.Value, cad_cgx, cad_col, (byte)comboBoxOBJSize.SelectedIndex, (byte)comboBoxCHRBANK.SelectedIndex, out frames, out duration);
                }

                Rectangle rect = Utility.GetBoundingRect(frames);
                render = (rect.Width == 0 || rect.Height == 0) ? false : render;

                if (render)
                {
                    for (int i = 0; i < frames.Length; i++)
                    {
                        frames[i] = frames[i].Clone(rect, PixelFormat.Format32bppArgb);
                    }

                    GIF.SaveGIF(sfd.FileName, frames, cad_col.GetPalette(), duration);
                    MessageBox.Show("GIF file has been exported.", "GIF Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Couldn't export the GIF file.", "GIF Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void UpdateOBJGroupBox()
        {
            if (cad_obj == null)
                return;

            if (radioButtonOBJSeq.Checked)
            {
                //Sequence
                numericUpDownFrame.Maximum = 15;
                numericUpDownOBJSeq.Enabled = true;
                int frames = cad_obj.GetSequenceFrameAmount((int)numericUpDownOBJSeq.Value);
                if (frames == 0)
                {
                    numericUpDownFrame.Value = 0;
                    numericUpDownFrame.Enabled = false;
                }
                else
                {
                    numericUpDownFrame.Enabled = true;
                    numericUpDownFrame.Maximum = frames - 1;
                }
            }
            else
            {
                //Raw
                numericUpDownFrame.Maximum = 31;
                numericUpDownFrame.Enabled = true;
                numericUpDownOBJSeq.Enabled = false;
                numericUpDownOBJSeq.Value = 0;
            }
        }

        private void radioButtonOBJRaw_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonOBJSeq.Checked = !radioButtonOBJRaw.Checked;
            UpdateOBJGroupBox();
        }

        private void radioButtonOBJSeq_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonOBJRaw.Checked = !radioButtonOBJSeq.Checked;
            UpdateOBJGroupBox();
        }

        private void numericUpDownOBJSeq_ValueChanged(object sender, EventArgs e)
        {
            UpdateOBJGroupBox();
            numericUpDownFrame.Value = 0;
            RenderOBJ();
        }

        private void importRAWGraphicsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Multiselect = false;
            o.Filter = "Character Graphics Files (*.cgx;*.chr)|*.cgx;*.cgx.bak;*.chr;*.chr.bak|All files|*.*";
            o.Title = "Import RAW Graphics...";
            if (o.ShowDialog() == DialogResult.OK)
            {
                Form importCGXform = new Form();
                importCGXform.AutoSize = false;
                importCGXform.ClientSize = new Size(200, 100);

                Label label1 = new Label();
                label1.Text = "Bits Per Pixel:";
                label1.Location = new Point(10, 10);

                ComboBox cgxbpp = new ComboBox();
                cgxbpp.Location = new Point(10, 25);
                cgxbpp.DropDownStyle = ComboBoxStyle.DropDownList;
                cgxbpp.Items.Add("2BPP");
                cgxbpp.Items.Add("4BPP");
                cgxbpp.Items.Add("8BPP");
                cgxbpp.SelectedIndex = 0;

                Button acceptCGX = new Button();
                acceptCGX.Text = "OK";
                acceptCGX.Location = new Point(10, importCGXform.ClientSize.Height - acceptCGX.ClientSize.Height - 10);
                acceptCGX.DialogResult = DialogResult.OK;

                Button cancelCGX = new Button();
                cancelCGX.Text = "Cancel";
                cancelCGX.Location = new Point(importCGXform.ClientSize.Width - cancelCGX.ClientSize.Width - 10, acceptCGX.Location.Y);

                importCGXform.Text = "Import RAW Graphics Data...";
                importCGXform.FormBorderStyle = FormBorderStyle.FixedDialog;
                importCGXform.MaximizeBox = false;
                importCGXform.MinimizeBox = false;
                importCGXform.AcceptButton = acceptCGX;
                importCGXform.CancelButton = cancelCGX;

                importCGXform.Controls.Add(cgxbpp);
                importCGXform.Controls.Add(label1);
                importCGXform.Controls.Add(acceptCGX);
                importCGXform.Controls.Add(cancelCGX);
                acceptCGX.Focus();

                if (importCGXform.ShowDialog() == DialogResult.OK)
                {
                    FileStream file = File.OpenRead(o.FileName);
                    cad_cgx = CAD.CGX.Import(file, cgxbpp.SelectedIndex);
                    file.Close();
                    cgx_filename = o.FileName;
                    RenderCGX();
                    RenderSCR();
                    RenderOBJ();
                }
            }
        }

        private void importRAWScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Multiselect = false;
            o.Filter = "Screen Files (*.scr)|*.scr;*.scr.bak|All files|*.*";
            o.Title = "Import RAW Screen Data...";
            if (o.ShowDialog() == DialogResult.OK)
            {
                Form importSCRform = new Form();
                importSCRform.AutoSize = false;
                importSCRform.ClientSize = new Size(200, 100);

                Label label1 = new Label();
                label1.Text = "Individual Tile Size:";
                label1.Location = new Point(10, 10);

                ComboBox scrtile = new ComboBox();
                scrtile.Location = new Point(10, 25);
                scrtile.DropDownStyle = ComboBoxStyle.DropDownList;
                scrtile.Items.Add("8x8");
                scrtile.Items.Add("16x16");
                scrtile.SelectedIndex = 0;

                Button acceptSCR = new Button();
                acceptSCR.Text = "OK";
                acceptSCR.Location = new Point(10, importSCRform.ClientSize.Height - acceptSCR.ClientSize.Height - 10);
                acceptSCR.DialogResult = DialogResult.OK;

                Button cancelSCR = new Button();
                cancelSCR.Text = "Cancel";
                cancelSCR.Location = new Point(importSCRform.ClientSize.Width - cancelSCR.ClientSize.Width - 10, acceptSCR.Location.Y);

                importSCRform.Text = "Import as SCR...";
                importSCRform.FormBorderStyle = FormBorderStyle.FixedDialog;
                importSCRform.MaximizeBox = false;
                importSCRform.MinimizeBox = false;
                importSCRform.AcceptButton = acceptSCR;
                importSCRform.CancelButton = cancelSCR;

                importSCRform.Controls.Add(scrtile);
                importSCRform.Controls.Add(label1);
                importSCRform.Controls.Add(acceptSCR);
                importSCRform.Controls.Add(cancelSCR);
                acceptSCR.Focus();

                if (importSCRform.ShowDialog() == DialogResult.OK)
                {
                    FileStream file = File.OpenRead(o.FileName);
                    cad_scr = CAD.SCR.Import(file, (byte)scrtile.SelectedIndex);
                    file.Close();
                    scr_filename = o.FileName;
                    RenderSCR();
                }
            }
        }

        private void importRAWPaletteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Multiselect = false;
            o.Filter = "SNES Color Files (*.col;*.pal)|*.col;*.col.bak;*.pal;*.pal.bak|All files|*.*";
            o.Title = "Import RAW SNES Color Data...";
            if (o.ShowDialog() == DialogResult.OK)
            {
                FileStream file = File.OpenRead(o.FileName);
                cad_col = CAD.COL.Import(file);
                file.Close();
                col_filename = o.FileName;
                RenderCOL();
                RenderCGX();
                RenderSCR();
                RenderOBJ();
            }
        }

        private void FormViewer_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void FormViewer_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            loadAssets(files);
        }

        private void loadAssets(string[] filenames)
        {
            bool loadedCOL = false;
            bool loadedCGX = false;
            bool loadedSCR = false;
            bool loadedOBJ = false;

            foreach (string p in filenames)
            {
                FileStream file = File.Open(p, FileMode.Open, FileAccess.Read);

                if (LoadCOL(file))
                {
                    labelCOL.Text = "COL (" + Path.GetFileName(p) + "):";
                    col_filename = Path.GetFileName(p);
                    loadedCOL = true;
                }
                else if (LoadCGX(file))
                {
                    labelCGX.Text = "CGX (" + Path.GetFileName(p) + "):";
                    cgx_filename = Path.GetFileName(p);
                    loadedCGX = true;
                }
                else if (LoadSCR(file))
                {
                    labelSCR.Text = "SCR (" + Path.GetFileName(p) + "):";
                    scr_filename = Path.GetFileName(p);
                    loadedSCR = true;
                    cad_obj = null;
                    obj_filename = "";
                }
                else if (LoadOBJ(file))
                {
                    labelSCR.Text = "OBJ (" + Path.GetFileName(p) + "):";
                    obj_filename = Path.GetFileName(p);
                    loadedOBJ = true;
                    cad_scr = null;
                    scr_filename = "";
                }

                file.Close();
            }

            UpdateOBJGroupBox();

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
}
