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
        static CAD.PNL cad_pnl;
        static CAD.MAP cad_map;

        static int selectedPal = 0;

        string col_filename;
        string cgx_filename;
        string scr_filename;
        string obj_filename;
        string pnl_filename;
        string map_filename;

        bool allowRender;

        public FormViewer()
        {
            InitializeComponent();
            comboBoxCHRBANK.SelectedIndex = 0;
            comboBoxOBJSize.SelectedIndex = 0;
            radioButtonOBJRaw.Checked = true;
            comboBoxLeftDisplay.SelectedIndex = 0;
            comboBoxRightDisplay.SelectedIndex = 0;
            allowRender = true;
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
            if (!allowRender)
                return;
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

            labelCOL.Text = "COL (" + col_filename + "):";
            pictureBoxCOL.Image = output;
        }

        private void RenderCGX()
        {
            if (!allowRender)
                return;
            if (comboBoxLeftDisplay.SelectedIndex != 0)
                return;
            if (cad_cgx == null || cad_col == null)
            {
                pictureBoxCGX.Image = null;
                pictureBoxCGX.Size = new Size(1, 1);
                return;
            }

            pictureBoxCGX.Image = Render.ScaleBitmap(cad_cgx.Render(cad_col, checkBoxPalForce.Checked ? selectedPal : -1), 2);
            pictureBoxCGX.Size = pictureBoxCGX.Image.Size;
        }

        private void RenderSCR()
        {
            if (!allowRender)
                return;
            if (comboBoxRightDisplay.SelectedIndex != 0)
                return;
            if (cad_cgx == null || cad_col == null || cad_scr == null)
            {
                pictureBoxSCR.Image = null;
                return;
            }

            pictureBoxSCR.Image = cad_scr.Render(cad_cgx, cad_col, checkBoxVisibleTiles.Checked, checkBoxDispBGColor.Checked);
            pictureBoxSCR.Size = pictureBoxSCR.Image.Size;
        }

        private void RenderOBJ()
        {
            if (!allowRender)
                return;
            if (comboBoxRightDisplay.SelectedIndex != 1)
                return;
            if (cad_cgx == null || cad_col == null || cad_obj == null)
            {
                pictureBoxSCR.Image = null;
                return;
            }

            if (radioButtonOBJRaw.Checked)
                pictureBoxSCR.Image = Render.ScaleBitmap(cad_obj.Render((int)numericUpDownFrame.Value, cad_cgx, cad_col, (byte)comboBoxOBJSize.SelectedIndex, (byte)comboBoxCHRBANK.SelectedIndex), 2);
            else
                pictureBoxSCR.Image = Render.ScaleBitmap(cad_obj.Render((int)numericUpDownOBJSeq.Value, (int)numericUpDownFrame.Value, cad_cgx, cad_col, (byte)comboBoxOBJSize.SelectedIndex, (byte)comboBoxCHRBANK.SelectedIndex), 2);
            pictureBoxSCR.Size = pictureBoxSCR.Image.Size;
        }

        private void RenderPNL()
        {
            if (!allowRender)
                return;
            if (comboBoxLeftDisplay.SelectedIndex != 1)
                return;
            if (cad_cgx == null || cad_col == null || cad_pnl == null)
            {
                pictureBoxCGX.Image = null;
                pictureBoxCGX.Size = new Size(1, 1);
                return;
            }

            pictureBoxCGX.Image = cad_pnl.Render(cad_cgx, cad_col, checkBoxVisibleTiles.Checked, checkBoxDispBGColor.Checked);
            pictureBoxCGX.Size = pictureBoxCGX.Image.Size;
        }

        private void RenderMAP()
        {
            if (!allowRender)
                return;
            if (comboBoxRightDisplay.SelectedIndex != 2)
                return;
            if (cad_cgx == null || cad_col == null || cad_pnl == null || cad_map == null)
            {
                //labelSCR.Text = "MAP (Error):";
                pictureBoxSCR.Image = null;
                return;
            }

            pictureBoxSCR.Image = cad_map.Render(cad_pnl, cad_cgx, cad_col, checkBoxVisibleTiles.Checked, checkBoxDispBGColor.Checked);
            pictureBoxSCR.Size = pictureBoxSCR.Image.Size;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            RenderSCR();
            RenderPNL();
            RenderMAP();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            cad_col.SetPaletteSwap(checkBoxCGRAMSwap.Checked);
            RenderCOL();
            RenderCGX();
            RenderSCR();
            RenderOBJ();
            RenderPNL();
            RenderMAP();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Multiselect = true;
            o.Filter = "All Supported Files|*.col;*.col.bak;*.cgx;*.cgx.bak;*.scr;*.scr.bak;*.obj;*.obj.bak;*.pnl;*.pnl.bak;*.map;*.map.bak|Color Files (*.col)|*.col;*.col.bak|Character Graphics Files (*.cgx)|*.cgx;*.cgx.bak|Screen Files (*.scr)|*.scr;*.scr.bak|Object Files (*.obj)|*.obj;*.obj.bak|Panel Files (*.pnl)|*.pnl;*.pnl.bak|Map Files (*.map)|*.map;*.map.bak|All files|*.*";
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

        private bool LoadPNL(FileStream file)
        {
            CAD.PNL test = CAD.PNL.Load(file);
            if (test != null)
                cad_pnl = test;
            return (test != null);
        }

        private bool LoadMAP(FileStream file)
        {
            CAD.MAP test = CAD.MAP.Load(file);
            if (test != null)
                cad_map = test;
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
            if (checkBoxExportFilenames.Checked)
                sfd.FileName = Path.GetFileName(col_filename) + "+" + Path.GetFileName(cgx_filename) + "_cgx";
            else
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
            if (checkBoxExportFilenames.Checked)
                sfd.FileName = Path.GetFileName(col_filename) + "+" + Path.GetFileName(cgx_filename) + "+" + Path.GetFileName(scr_filename) + "_scr";
            else
                sfd.FileName = Path.GetFileNameWithoutExtension(scr_filename) + "_scr";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                cad_scr.Render(cad_cgx, cad_col, checkBoxVisibleTiles.Checked, checkBoxDispBGColor.Checked).Save(sfd.FileName, format);
            }
        }

        private void exportPNLAsPNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cad_col == null || cad_cgx == null || cad_pnl == null)
                return;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PNG Image|*.png";
            sfd.Title = "Save PNL Output...";
            ImageFormat format = ImageFormat.Png;
            if (checkBoxExportFilenames.Checked)
                sfd.FileName = Path.GetFileName(col_filename) + "+" + Path.GetFileName(cgx_filename) + "+" + Path.GetFileName(pnl_filename) + "_pnl";
            else
                sfd.FileName = Path.GetFileNameWithoutExtension(pnl_filename) + "_pnl";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                cad_pnl.Render(cad_cgx, cad_col, checkBoxVisibleTiles.Checked, checkBoxDispBGColor.Checked).Save(sfd.FileName, format);
            }
        }

        private void exportMAPAsPNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cad_col == null || cad_cgx == null || cad_pnl == null || cad_map == null)
                return;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PNG Image|*.png";
            sfd.Title = "Save MAP Output...";
            ImageFormat format = ImageFormat.Png;
            if (checkBoxExportFilenames.Checked)
                sfd.FileName = Path.GetFileName(col_filename) + "+" + Path.GetFileName(cgx_filename) + "+" + Path.GetFileName(pnl_filename) + "+" + Path.GetFileName(map_filename) + "_map";
            else
                sfd.FileName = Path.GetFileNameWithoutExtension(map_filename) + "_map";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                cad_map.Render(cad_pnl, cad_cgx, cad_col, checkBoxVisibleTiles.Checked, checkBoxDispBGColor.Checked).Save(sfd.FileName, format);
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
            if (checkBoxExportFilenames.Checked)
                sfd.FileName = Path.GetFileName(col_filename) + "+" + Path.GetFileName(cgx_filename) + "+" + Path.GetFileName(obj_filename) + "_obj";
            else
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
                    RenderPNL();
                    RenderMAP();
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
                RenderPNL();
                RenderMAP();
            }
        }

        private void importReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Multiselect = false;
            o.Filter = "All Supported Files|*.col;*.col.bak;*.cgx;*.cgx.bak|Color Files (*.col)|*.col;*.col.bak|Character Graphics Files (*.cgx)|*.cgx;*.cgx.bak";
            o.Title = "Import / Replace SCAD files...";
            if (o.ShowDialog() == DialogResult.OK)
            {
                FileStream file = File.Open(o.FileName, FileMode.Open, FileAccess.Read);
                CAD.COL new_col = null;
                CAD.CGX new_cgx = null;

                new_col = CAD.COL.Load(file);
                if (new_col == null) new_cgx = CAD.CGX.Load(file);

                file.Close();

                if (new_col != null)
                {
                    //Replace color bank
                    if (cad_col == null)
                    {
                        MessageBox.Show("Please load a COL file first.", "COL Replace", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (new_cgx != null)
                {
                    //Replace chr bank
                    if (cad_cgx == null)
                    {
                        MessageBox.Show("Please load a CGX file first.", "CGX Replace", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (cad_cgx.GetFormat() != new_cgx.GetFormat())
                    {
                        MessageBox.Show("Error: CGX Format Mismatch", "CGX Replace", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Form importCGXform = new Form();
                    importCGXform.AutoSize = false;
                    importCGXform.ClientSize = new Size(200, 150);
                    importCGXform.Text = "Replace Graphics Bank...";

                    Label label1 = new Label();
                    label1.Text = "From Bank (@" + Path.GetFileName(o.FileName) + "):";
                    label1.Location = new Point(10, 10);
                    label1.AutoSize = true;

                    ComboBox newcgxbank = new ComboBox();
                    newcgxbank.Location = new Point(10, 25);
                    newcgxbank.Size = new Size(150, 21);
                    newcgxbank.DropDownStyle = ComboBoxStyle.DropDownList;
                    newcgxbank.Items.Add("CGX Bank 0 (Tile 0x000)");
                    newcgxbank.Items.Add("CGX Bank 1 (Tile 0x080)");
                    newcgxbank.Items.Add("CGX Bank 2 (Tile 0x100)");
                    newcgxbank.Items.Add("CGX Bank 3 (Tile 0x180)");
                    newcgxbank.Items.Add("CGX Bank 4 (Tile 0x200)");
                    newcgxbank.Items.Add("CGX Bank 5 (Tile 0x280)");
                    newcgxbank.Items.Add("CGX Bank 6 (Tile 0x300)");
                    newcgxbank.Items.Add("CGX Bank 7 (Tile 0x380)");
                    newcgxbank.SelectedIndex = 0;

                    Label label2 = new Label();
                    label2.Text = "To Loaded Bank:";
                    label2.Location = new Point(10, 55);
                    label2.AutoSize = true;

                    ComboBox oldcgxbank = new ComboBox();
                    oldcgxbank.Location = new Point(10, 70);
                    oldcgxbank.Size = new Size(150, 21);
                    oldcgxbank.DropDownStyle = ComboBoxStyle.DropDownList;
                    oldcgxbank.Items.Add("CGX Bank 0 (Tile 0x000)");
                    oldcgxbank.Items.Add("CGX Bank 1 (Tile 0x080)");
                    oldcgxbank.Items.Add("CGX Bank 2 (Tile 0x100)");
                    oldcgxbank.Items.Add("CGX Bank 3 (Tile 0x180)");
                    oldcgxbank.Items.Add("CGX Bank 4 (Tile 0x200)");
                    oldcgxbank.Items.Add("CGX Bank 5 (Tile 0x280)");
                    oldcgxbank.Items.Add("CGX Bank 6 (Tile 0x300)");
                    oldcgxbank.Items.Add("CGX Bank 7 (Tile 0x380)");
                    oldcgxbank.SelectedIndex = 0;

                    Button acceptBTN = new Button();
                    acceptBTN.Text = "OK";
                    acceptBTN.Location = new Point(10, importCGXform.ClientSize.Height - acceptBTN.ClientSize.Height - 10);
                    acceptBTN.DialogResult = DialogResult.OK;

                    Button cancelBTN = new Button();
                    cancelBTN.Text = "Cancel";
                    cancelBTN.Location = new Point(importCGXform.ClientSize.Width - cancelBTN.ClientSize.Width - 10, acceptBTN.Location.Y);

                    importCGXform.FormBorderStyle = FormBorderStyle.FixedDialog;
                    importCGXform.MaximizeBox = false;
                    importCGXform.MinimizeBox = false;
                    importCGXform.AcceptButton = acceptBTN;
                    importCGXform.CancelButton = cancelBTN;

                    importCGXform.Controls.Add(newcgxbank);
                    importCGXform.Controls.Add(oldcgxbank);
                    importCGXform.Controls.Add(label1);
                    importCGXform.Controls.Add(label2);
                    importCGXform.Controls.Add(acceptBTN);
                    importCGXform.Controls.Add(cancelBTN);

                    acceptBTN.Focus();

                    if (importCGXform.ShowDialog() == DialogResult.OK)
                    {
                        cad_cgx.CopyBank(new_cgx, newcgxbank.SelectedIndex, oldcgxbank.SelectedIndex);
                        RenderCGX();
                        RenderSCR();
                        RenderOBJ();
                        RenderPNL();
                        RenderMAP();
                    }
                }
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
            bool loadedPNL = false;
            bool loadedMAP = false;

            allowRender = false;

            foreach (string p in filenames)
            {
                FileStream file = File.Open(p, FileMode.Open, FileAccess.Read);

                if (LoadCOL(file))
                {
                    col_filename = Path.GetFileName(p);
                    loadedCOL = true;
                }
                else if (LoadCGX(file))
                {
                    cgx_filename = Path.GetFileName(p);
                    loadedCGX = true;
                    comboBoxLeftDisplay.Items[0] = "CGX (Graphics) - " + cgx_filename;
                    comboBoxLeftDisplay.SelectedIndex = 0;
                }
                else if (LoadSCR(file))
                {
                    scr_filename = Path.GetFileName(p);
                    loadedSCR = true;
                    comboBoxRightDisplay.Items[0] = "SCR (Screen) - " + scr_filename;
                    comboBoxRightDisplay.SelectedIndex = 0;
                }
                else if (LoadOBJ(file))
                {
                    obj_filename = Path.GetFileName(p);
                    loadedOBJ = true;
                    comboBoxRightDisplay.Items[1] = "OBJ (Object) - " + obj_filename;
                    comboBoxRightDisplay.SelectedIndex = 1;
                }
                else if (LoadPNL(file))
                {
                    pnl_filename = Path.GetFileName(p);
                    loadedPNL = true;
                    comboBoxLeftDisplay.Items[1] = "PNL (Panel) - " + pnl_filename;
                    comboBoxLeftDisplay.SelectedIndex = 1;
                }
                else if (LoadMAP(file))
                {
                    map_filename = Path.GetFileName(p);
                    loadedMAP = true;
                    comboBoxRightDisplay.Items[2] = "MAP (Map) - " + map_filename;
                    comboBoxRightDisplay.SelectedIndex = 2;
                }

                file.Close();
            }

            allowRender = true;

            UpdateOBJGroupBox();

            if (loadedCOL)
            {
                RenderCOL();
                RenderCGX();
                RenderSCR();
                RenderOBJ();
                RenderPNL();
                RenderMAP();
            }
            else if (loadedCGX)
            {
                RenderCGX();
                RenderSCR();
                RenderOBJ();
                RenderPNL();
                RenderMAP();
            }
            else if (loadedSCR)
            {
                RenderSCR();
            }
            else if (loadedOBJ)
            {
                RenderOBJ();
            }
            else if (loadedPNL)
            {
                RenderPNL();
                RenderMAP();
            }
            else if (loadedMAP)
            {
                RenderMAP();
            }
        }

        private void comboBoxLeftDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            RenderCGX();
            RenderPNL();
        }

        private void comboBoxRightDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            RenderSCR();
            RenderOBJ();
            RenderMAP();
        }

        private void checkBoxDispBGColor_CheckedChanged(object sender, EventArgs e)
        {
            RenderSCR();
            RenderPNL();
            RenderMAP();
        }
    }
}
