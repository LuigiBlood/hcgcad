
namespace hcgcadviewer
{
    partial class FormViewer
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBoxCGX = new System.Windows.Forms.PictureBox();
            this.pictureBoxCOL = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxPalForce = new System.Windows.Forms.CheckBox();
            this.labelCGX = new System.Windows.Forms.Label();
            this.labelCOL = new System.Windows.Forms.Label();
            this.labelSCR = new System.Windows.Forms.Label();
            this.pictureBoxSCR = new System.Windows.Forms.PictureBox();
            this.checkBoxVisibleTiles = new System.Windows.Forms.CheckBox();
            this.checkBoxCGRAMSwap = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exportCGXAsPNGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportSCRAsPNGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportOBJAsGIFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportPNLAsPNGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMAPAsPNGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importRAWGraphicsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importRAWPaletteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importRAWScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.importReplaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.numericUpDownFrame = new System.Windows.Forms.NumericUpDown();
            this.comboBoxOBJSize = new System.Windows.Forms.ComboBox();
            this.comboBoxCHRBANK = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownOBJSeq = new System.Windows.Forms.NumericUpDown();
            this.radioButtonOBJSeq = new System.Windows.Forms.RadioButton();
            this.radioButtonOBJRaw = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxLeftDisplay = new System.Windows.Forms.ComboBox();
            this.comboBoxRightDisplay = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.checkBoxDispBGColor = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCGX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCOL)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSCR)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOBJSeq)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 36);
            this.button1.TabIndex = 0;
            this.button1.Text = "Load Assets...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // pictureBoxCGX
            // 
            this.pictureBoxCGX.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxCGX.Name = "pictureBoxCGX";
            this.pictureBoxCGX.Size = new System.Drawing.Size(117, 108);
            this.pictureBoxCGX.TabIndex = 3;
            this.pictureBoxCGX.TabStop = false;
            // 
            // pictureBoxCOL
            // 
            this.pictureBoxCOL.Location = new System.Drawing.Point(12, 311);
            this.pictureBoxCOL.Name = "pictureBoxCOL";
            this.pictureBoxCOL.Size = new System.Drawing.Size(256, 256);
            this.pictureBoxCOL.TabIndex = 4;
            this.pictureBoxCOL.TabStop = false;
            this.pictureBoxCOL.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseClick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pictureBoxCGX);
            this.panel1.Location = new System.Drawing.Point(274, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(273, 512);
            this.panel1.TabIndex = 5;
            // 
            // checkBoxPalForce
            // 
            this.checkBoxPalForce.AutoSize = true;
            this.checkBoxPalForce.Location = new System.Drawing.Point(7, 8);
            this.checkBoxPalForce.Name = "checkBoxPalForce";
            this.checkBoxPalForce.Size = new System.Drawing.Size(120, 17);
            this.checkBoxPalForce.TabIndex = 6;
            this.checkBoxPalForce.Text = "(CGX) Force Palette";
            this.checkBoxPalForce.UseVisualStyleBackColor = true;
            this.checkBoxPalForce.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // labelCGX
            // 
            this.labelCGX.AutoSize = true;
            this.labelCGX.Location = new System.Drawing.Point(274, 38);
            this.labelCGX.Name = "labelCGX";
            this.labelCGX.Size = new System.Drawing.Size(32, 13);
            this.labelCGX.TabIndex = 8;
            this.labelCGX.Text = "CGX:";
            // 
            // labelCOL
            // 
            this.labelCOL.AutoSize = true;
            this.labelCOL.Location = new System.Drawing.Point(12, 292);
            this.labelCOL.Name = "labelCOL";
            this.labelCOL.Size = new System.Drawing.Size(31, 13);
            this.labelCOL.TabIndex = 9;
            this.labelCOL.Text = "COL:";
            // 
            // labelSCR
            // 
            this.labelSCR.AutoSize = true;
            this.labelSCR.Location = new System.Drawing.Point(550, 38);
            this.labelSCR.Name = "labelSCR";
            this.labelSCR.Size = new System.Drawing.Size(32, 13);
            this.labelSCR.TabIndex = 10;
            this.labelSCR.Text = "SCR:";
            // 
            // pictureBoxSCR
            // 
            this.pictureBoxSCR.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxSCR.Location = new System.Drawing.Point(553, 55);
            this.pictureBoxSCR.MinimumSize = new System.Drawing.Size(512, 512);
            this.pictureBoxSCR.Name = "pictureBoxSCR";
            this.pictureBoxSCR.Size = new System.Drawing.Size(512, 512);
            this.pictureBoxSCR.TabIndex = 11;
            this.pictureBoxSCR.TabStop = false;
            // 
            // checkBoxVisibleTiles
            // 
            this.checkBoxVisibleTiles.AutoSize = true;
            this.checkBoxVisibleTiles.Location = new System.Drawing.Point(7, 51);
            this.checkBoxVisibleTiles.Name = "checkBoxVisibleTiles";
            this.checkBoxVisibleTiles.Size = new System.Drawing.Size(210, 17);
            this.checkBoxVisibleTiles.TabIndex = 12;
            this.checkBoxVisibleTiles.Text = "(SCR/PNL/MAP) Make All Tiles Visible";
            this.checkBoxVisibleTiles.UseVisualStyleBackColor = true;
            this.checkBoxVisibleTiles.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBoxCGRAMSwap
            // 
            this.checkBoxCGRAMSwap.AutoSize = true;
            this.checkBoxCGRAMSwap.Location = new System.Drawing.Point(7, 29);
            this.checkBoxCGRAMSwap.Name = "checkBoxCGRAMSwap";
            this.checkBoxCGRAMSwap.Size = new System.Drawing.Size(134, 17);
            this.checkBoxCGRAMSwap.TabIndex = 13;
            this.checkBoxCGRAMSwap.Text = "Swap CG-RAM (Hi/Lo)";
            this.checkBoxCGRAMSwap.UseVisualStyleBackColor = true;
            this.checkBoxCGRAMSwap.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.importToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1072, 24);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.toolStripSeparator2,
            this.exportCGXAsPNGToolStripMenuItem,
            this.exportSCRAsPNGToolStripMenuItem,
            this.exportOBJAsGIFToolStripMenuItem,
            this.exportPNLAsPNGToolStripMenuItem,
            this.exportMAPAsPNGToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.loadToolStripMenuItem.Text = "Load Assets...";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(184, 6);
            // 
            // exportCGXAsPNGToolStripMenuItem
            // 
            this.exportCGXAsPNGToolStripMenuItem.Name = "exportCGXAsPNGToolStripMenuItem";
            this.exportCGXAsPNGToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.exportCGXAsPNGToolStripMenuItem.Text = "Export CGX as PNG...";
            this.exportCGXAsPNGToolStripMenuItem.Click += new System.EventHandler(this.exportCGXAsPNGToolStripMenuItem_Click);
            // 
            // exportSCRAsPNGToolStripMenuItem
            // 
            this.exportSCRAsPNGToolStripMenuItem.Name = "exportSCRAsPNGToolStripMenuItem";
            this.exportSCRAsPNGToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.exportSCRAsPNGToolStripMenuItem.Text = "Export SCR as PNG...";
            this.exportSCRAsPNGToolStripMenuItem.Click += new System.EventHandler(this.exportSCRAsPNGToolStripMenuItem_Click);
            // 
            // exportOBJAsGIFToolStripMenuItem
            // 
            this.exportOBJAsGIFToolStripMenuItem.Name = "exportOBJAsGIFToolStripMenuItem";
            this.exportOBJAsGIFToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.exportOBJAsGIFToolStripMenuItem.Text = "Export OBJ as GIF...";
            this.exportOBJAsGIFToolStripMenuItem.Click += new System.EventHandler(this.exportOBJAsGIFToolStripMenuItem_Click);
            // 
            // exportPNLAsPNGToolStripMenuItem
            // 
            this.exportPNLAsPNGToolStripMenuItem.Name = "exportPNLAsPNGToolStripMenuItem";
            this.exportPNLAsPNGToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.exportPNLAsPNGToolStripMenuItem.Text = "Export PNL as PNG...";
            this.exportPNLAsPNGToolStripMenuItem.Click += new System.EventHandler(this.exportPNLAsPNGToolStripMenuItem_Click);
            // 
            // exportMAPAsPNGToolStripMenuItem
            // 
            this.exportMAPAsPNGToolStripMenuItem.Name = "exportMAPAsPNGToolStripMenuItem";
            this.exportMAPAsPNGToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.exportMAPAsPNGToolStripMenuItem.Text = "Export MAP as PNG...";
            this.exportMAPAsPNGToolStripMenuItem.Click += new System.EventHandler(this.exportMAPAsPNGToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(184, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importRAWGraphicsToolStripMenuItem,
            this.importRAWPaletteToolStripMenuItem,
            this.importRAWScreenToolStripMenuItem,
            this.toolStripSeparator3,
            this.importReplaceToolStripMenuItem});
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.importToolStripMenuItem.Text = "Import";
            // 
            // importRAWGraphicsToolStripMenuItem
            // 
            this.importRAWGraphicsToolStripMenuItem.Name = "importRAWGraphicsToolStripMenuItem";
            this.importRAWGraphicsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.importRAWGraphicsToolStripMenuItem.Text = "Load RAW Graphics...";
            this.importRAWGraphicsToolStripMenuItem.Click += new System.EventHandler(this.importRAWGraphicsToolStripMenuItem_Click);
            // 
            // importRAWPaletteToolStripMenuItem
            // 
            this.importRAWPaletteToolStripMenuItem.Name = "importRAWPaletteToolStripMenuItem";
            this.importRAWPaletteToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.importRAWPaletteToolStripMenuItem.Text = "Load RAW Palette...";
            this.importRAWPaletteToolStripMenuItem.Click += new System.EventHandler(this.importRAWPaletteToolStripMenuItem_Click);
            // 
            // importRAWScreenToolStripMenuItem
            // 
            this.importRAWScreenToolStripMenuItem.Name = "importRAWScreenToolStripMenuItem";
            this.importRAWScreenToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.importRAWScreenToolStripMenuItem.Text = "Load RAW Screen...";
            this.importRAWScreenToolStripMenuItem.Click += new System.EventHandler(this.importRAWScreenToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(183, 6);
            // 
            // importReplaceToolStripMenuItem
            // 
            this.importReplaceToolStripMenuItem.Name = "importReplaceToolStripMenuItem";
            this.importReplaceToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.importReplaceToolStripMenuItem.Text = "Import / Replace...";
            this.importReplaceToolStripMenuItem.Click += new System.EventHandler(this.importReplaceToolStripMenuItem_Click);
            // 
            // numericUpDownFrame
            // 
            this.numericUpDownFrame.Location = new System.Drawing.Point(201, 33);
            this.numericUpDownFrame.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.numericUpDownFrame.Name = "numericUpDownFrame";
            this.numericUpDownFrame.Size = new System.Drawing.Size(41, 20);
            this.numericUpDownFrame.TabIndex = 17;
            this.numericUpDownFrame.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // comboBoxOBJSize
            // 
            this.comboBoxOBJSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOBJSize.FormattingEnabled = true;
            this.comboBoxOBJSize.Items.AddRange(new object[] {
            "8x8, 16x16",
            "8x8, 32x32",
            "8x8, 64x64",
            "16x16, 32x32",
            "16x16, 64x64",
            "32x32, 64x64"});
            this.comboBoxOBJSize.Location = new System.Drawing.Point(89, 59);
            this.comboBoxOBJSize.Name = "comboBoxOBJSize";
            this.comboBoxOBJSize.Size = new System.Drawing.Size(152, 21);
            this.comboBoxOBJSize.TabIndex = 19;
            this.comboBoxOBJSize.SelectedIndexChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // comboBoxCHRBANK
            // 
            this.comboBoxCHRBANK.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCHRBANK.FormattingEnabled = true;
            this.comboBoxCHRBANK.Items.AddRange(new object[] {
            "CGX Bank 0 (Tile 0x000)",
            "CGX Bank 1 (Tile 0x080)",
            "CGX Bank 2 (Tile 0x100)",
            "CGX Bank 3 (Tile 0x180)",
            "CGX Bank 4 (Tile 0x200)",
            "CGX Bank 5 (Tile 0x280)",
            "CGX Bank 6 (Tile 0x300)",
            "CGX Bank 7 (Tile 0x380)"});
            this.comboBoxCHRBANK.Location = new System.Drawing.Point(89, 86);
            this.comboBoxCHRBANK.Name = "comboBoxCHRBANK";
            this.comboBoxCHRBANK.Size = new System.Drawing.Size(152, 21);
            this.comboBoxCHRBANK.TabIndex = 20;
            this.comboBoxCHRBANK.SelectedIndexChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(137, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "OBJ Frame:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "OBJ Sequence:";
            // 
            // numericUpDownOBJSeq
            // 
            this.numericUpDownOBJSeq.Location = new System.Drawing.Point(89, 33);
            this.numericUpDownOBJSeq.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDownOBJSeq.Name = "numericUpDownOBJSeq";
            this.numericUpDownOBJSeq.Size = new System.Drawing.Size(38, 20);
            this.numericUpDownOBJSeq.TabIndex = 26;
            this.numericUpDownOBJSeq.ValueChanged += new System.EventHandler(this.numericUpDownOBJSeq_ValueChanged);
            // 
            // radioButtonOBJSeq
            // 
            this.radioButtonOBJSeq.AutoSize = true;
            this.radioButtonOBJSeq.Location = new System.Drawing.Point(129, 6);
            this.radioButtonOBJSeq.Name = "radioButtonOBJSeq";
            this.radioButtonOBJSeq.Size = new System.Drawing.Size(79, 17);
            this.radioButtonOBJSeq.TabIndex = 25;
            this.radioButtonOBJSeq.TabStop = true;
            this.radioButtonOBJSeq.Text = "Sequences";
            this.radioButtonOBJSeq.UseVisualStyleBackColor = true;
            this.radioButtonOBJSeq.CheckedChanged += new System.EventHandler(this.radioButtonOBJSeq_CheckedChanged);
            // 
            // radioButtonOBJRaw
            // 
            this.radioButtonOBJRaw.AutoSize = true;
            this.radioButtonOBJRaw.Location = new System.Drawing.Point(6, 6);
            this.radioButtonOBJRaw.Name = "radioButtonOBJRaw";
            this.radioButtonOBJRaw.Size = new System.Drawing.Size(84, 17);
            this.radioButtonOBJRaw.TabIndex = 24;
            this.radioButtonOBJRaw.TabStop = true;
            this.radioButtonOBJRaw.Text = "Raw Frames";
            this.radioButtonOBJRaw.UseVisualStyleBackColor = true;
            this.radioButtonOBJRaw.CheckedChanged += new System.EventHandler(this.radioButtonOBJRaw_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "CGX Base:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Object Sizes:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 147);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(256, 140);
            this.tabControl1.TabIndex = 28;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.radioButtonOBJRaw);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.comboBoxCHRBANK);
            this.tabPage1.Controls.Add(this.numericUpDownOBJSeq);
            this.tabPage1.Controls.Add(this.comboBoxOBJSize);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.radioButtonOBJSeq);
            this.tabPage1.Controls.Add(this.numericUpDownFrame);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(248, 114);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "OBJ Control";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(167, 38);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 36);
            this.button2.TabIndex = 29;
            this.button2.Text = "Import / Replace";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.importReplaceToolStripMenuItem_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Left Side:";
            // 
            // comboBoxLeftDisplay
            // 
            this.comboBoxLeftDisplay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLeftDisplay.FormattingEnabled = true;
            this.comboBoxLeftDisplay.Items.AddRange(new object[] {
            "CGX (Graphics)",
            "PNL (Panel)"});
            this.comboBoxLeftDisplay.Location = new System.Drawing.Point(12, 108);
            this.comboBoxLeftDisplay.Name = "comboBoxLeftDisplay";
            this.comboBoxLeftDisplay.Size = new System.Drawing.Size(115, 21);
            this.comboBoxLeftDisplay.TabIndex = 20;
            this.comboBoxLeftDisplay.SelectedIndexChanged += new System.EventHandler(this.comboBoxLeftDisplay_SelectedIndexChanged);
            // 
            // comboBoxRightDisplay
            // 
            this.comboBoxRightDisplay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRightDisplay.FormattingEnabled = true;
            this.comboBoxRightDisplay.Items.AddRange(new object[] {
            "SCR (Screen)",
            "OBJ (Object)",
            "MAP (Map)"});
            this.comboBoxRightDisplay.Location = new System.Drawing.Point(141, 108);
            this.comboBoxRightDisplay.Name = "comboBoxRightDisplay";
            this.comboBoxRightDisplay.Size = new System.Drawing.Size(116, 21);
            this.comboBoxRightDisplay.TabIndex = 22;
            this.comboBoxRightDisplay.SelectedIndexChanged += new System.EventHandler(this.comboBoxRightDisplay_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(138, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Right Side:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.checkBoxDispBGColor);
            this.tabPage3.Controls.Add(this.checkBoxCGRAMSwap);
            this.tabPage3.Controls.Add(this.checkBoxPalForce);
            this.tabPage3.Controls.Add(this.checkBoxVisibleTiles);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(248, 114);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Display";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // checkBoxDispBGColor
            // 
            this.checkBoxDispBGColor.AutoSize = true;
            this.checkBoxDispBGColor.Location = new System.Drawing.Point(7, 73);
            this.checkBoxDispBGColor.Name = "checkBoxDispBGColor";
            this.checkBoxDispBGColor.Size = new System.Drawing.Size(233, 17);
            this.checkBoxDispBGColor.TabIndex = 14;
            this.checkBoxDispBGColor.Text = "(SCR/PNL/MAP) Display Background Color";
            this.checkBoxDispBGColor.UseVisualStyleBackColor = true;
            this.checkBoxDispBGColor.CheckedChanged += new System.EventHandler(this.checkBoxDispBGColor_CheckedChanged);
            // 
            // FormViewer
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 575);
            this.Controls.Add(this.comboBoxRightDisplay);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.comboBoxLeftDisplay);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pictureBoxSCR);
            this.Controls.Add(this.labelSCR);
            this.Controls.Add(this.labelCOL);
            this.Controls.Add(this.labelCGX);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBoxCOL);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "FormViewer";
            this.Text = "Hyper CG-CAD - Viewer";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormViewer_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormViewer_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCGX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCOL)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSCR)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOBJSeq)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBoxCGX;
        private System.Windows.Forms.PictureBox pictureBoxCOL;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBoxPalForce;
        private System.Windows.Forms.Label labelCGX;
        private System.Windows.Forms.Label labelCOL;
        private System.Windows.Forms.Label labelSCR;
        private System.Windows.Forms.PictureBox pictureBoxSCR;
        private System.Windows.Forms.CheckBox checkBoxVisibleTiles;
        private System.Windows.Forms.CheckBox checkBoxCGRAMSwap;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportCGXAsPNGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportSCRAsPNGToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown numericUpDownFrame;
        private System.Windows.Forms.ComboBox comboBoxOBJSize;
        private System.Windows.Forms.ComboBox comboBoxCHRBANK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem exportOBJAsGIFToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownOBJSeq;
        private System.Windows.Forms.RadioButton radioButtonOBJSeq;
        private System.Windows.Forms.RadioButton radioButtonOBJRaw;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importRAWGraphicsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importRAWPaletteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importRAWScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem importReplaceToolStripMenuItem;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem exportPNLAsPNGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportMAPAsPNGToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBoxRightDisplay;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxLeftDisplay;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox checkBoxDispBGColor;
    }
}

