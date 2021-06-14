
namespace hcgcad
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
            this.button4 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportCGXAsPNGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportSCRAsPNGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportOBJAsGIFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.numericUpDownFrame = new System.Windows.Forms.NumericUpDown();
            this.comboBoxOBJSize = new System.Windows.Forms.ComboBox();
            this.comboBoxCHRBANK = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxOBJ = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownOBJSeq = new System.Windows.Forms.NumericUpDown();
            this.radioButtonOBJSeq = new System.Windows.Forms.RadioButton();
            this.radioButtonOBJRaw = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCGX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCOL)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSCR)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrame)).BeginInit();
            this.groupBoxOBJ.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOBJSeq)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 23);
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
            this.checkBoxPalForce.Location = new System.Drawing.Point(15, 66);
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
            this.checkBoxVisibleTiles.Location = new System.Drawing.Point(15, 89);
            this.checkBoxVisibleTiles.Name = "checkBoxVisibleTiles";
            this.checkBoxVisibleTiles.Size = new System.Drawing.Size(156, 17);
            this.checkBoxVisibleTiles.TabIndex = 12;
            this.checkBoxVisibleTiles.Text = "(SCR) Make All Tiles Visible";
            this.checkBoxVisibleTiles.UseVisualStyleBackColor = true;
            this.checkBoxVisibleTiles.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBoxCGRAMSwap
            // 
            this.checkBoxCGRAMSwap.AutoSize = true;
            this.checkBoxCGRAMSwap.Location = new System.Drawing.Point(15, 113);
            this.checkBoxCGRAMSwap.Name = "checkBoxCGRAMSwap";
            this.checkBoxCGRAMSwap.Size = new System.Drawing.Size(134, 17);
            this.checkBoxCGRAMSwap.TabIndex = 13;
            this.checkBoxCGRAMSwap.Text = "Swap CG-RAM (Hi/Lo)";
            this.checkBoxCGRAMSwap.UseVisualStyleBackColor = true;
            this.checkBoxCGRAMSwap.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(181, 33);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 38);
            this.button4.TabIndex = 14;
            this.button4.Text = "Unload SCR/OBJ";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
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
            this.exportCGXAsPNGToolStripMenuItem,
            this.exportSCRAsPNGToolStripMenuItem,
            this.exportOBJAsGIFToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.loadToolStripMenuItem.Text = "Load Assets...";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // exportCGXAsPNGToolStripMenuItem
            // 
            this.exportCGXAsPNGToolStripMenuItem.Name = "exportCGXAsPNGToolStripMenuItem";
            this.exportCGXAsPNGToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.exportCGXAsPNGToolStripMenuItem.Text = "Export CGX as PNG...";
            this.exportCGXAsPNGToolStripMenuItem.Click += new System.EventHandler(this.exportCGXAsPNGToolStripMenuItem_Click);
            // 
            // exportSCRAsPNGToolStripMenuItem
            // 
            this.exportSCRAsPNGToolStripMenuItem.Name = "exportSCRAsPNGToolStripMenuItem";
            this.exportSCRAsPNGToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.exportSCRAsPNGToolStripMenuItem.Text = "Export SCR as PNG...";
            this.exportSCRAsPNGToolStripMenuItem.Click += new System.EventHandler(this.exportSCRAsPNGToolStripMenuItem_Click);
            // 
            // exportOBJAsGIFToolStripMenuItem
            // 
            this.exportOBJAsGIFToolStripMenuItem.Name = "exportOBJAsGIFToolStripMenuItem";
            this.exportOBJAsGIFToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.exportOBJAsGIFToolStripMenuItem.Text = "Export OBJ as GIF...";
            this.exportOBJAsGIFToolStripMenuItem.Click += new System.EventHandler(this.exportOBJAsGIFToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(181, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // numericUpDownFrame
            // 
            this.numericUpDownFrame.Location = new System.Drawing.Point(209, 55);
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
            "8x8 16x16",
            "8x8 32x32",
            "8x8 64x64",
            "16x16 32x32",
            "16x16 64x64",
            "32x32 64x64"});
            this.comboBoxOBJSize.Location = new System.Drawing.Point(129, 81);
            this.comboBoxOBJSize.Name = "comboBoxOBJSize";
            this.comboBoxOBJSize.Size = new System.Drawing.Size(121, 21);
            this.comboBoxOBJSize.TabIndex = 19;
            this.comboBoxOBJSize.SelectedIndexChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // comboBoxCHRBANK
            // 
            this.comboBoxCHRBANK.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCHRBANK.FormattingEnabled = true;
            this.comboBoxCHRBANK.Items.AddRange(new object[] {
            "CGX Bank 0",
            "CGX Bank 1",
            "CGX Bank 2",
            "CGX Bank 3"});
            this.comboBoxCHRBANK.Location = new System.Drawing.Point(129, 108);
            this.comboBoxCHRBANK.Name = "comboBoxCHRBANK";
            this.comboBoxCHRBANK.Size = new System.Drawing.Size(121, 21);
            this.comboBoxCHRBANK.TabIndex = 20;
            this.comboBoxCHRBANK.SelectedIndexChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(141, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "OBJ Frame:";
            // 
            // groupBoxOBJ
            // 
            this.groupBoxOBJ.Controls.Add(this.label4);
            this.groupBoxOBJ.Controls.Add(this.numericUpDownOBJSeq);
            this.groupBoxOBJ.Controls.Add(this.radioButtonOBJSeq);
            this.groupBoxOBJ.Controls.Add(this.radioButtonOBJRaw);
            this.groupBoxOBJ.Controls.Add(this.label3);
            this.groupBoxOBJ.Controls.Add(this.label2);
            this.groupBoxOBJ.Controls.Add(this.numericUpDownFrame);
            this.groupBoxOBJ.Controls.Add(this.label1);
            this.groupBoxOBJ.Controls.Add(this.comboBoxOBJSize);
            this.groupBoxOBJ.Controls.Add(this.comboBoxCHRBANK);
            this.groupBoxOBJ.Location = new System.Drawing.Point(12, 141);
            this.groupBoxOBJ.Name = "groupBoxOBJ";
            this.groupBoxOBJ.Size = new System.Drawing.Size(256, 135);
            this.groupBoxOBJ.TabIndex = 22;
            this.groupBoxOBJ.TabStop = false;
            this.groupBoxOBJ.Text = "OBJ Control:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "OBJ Sequence:";
            // 
            // numericUpDownOBJSeq
            // 
            this.numericUpDownOBJSeq.Location = new System.Drawing.Point(97, 55);
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
            this.radioButtonOBJSeq.Location = new System.Drawing.Point(129, 19);
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
            this.radioButtonOBJRaw.Location = new System.Drawing.Point(6, 19);
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
            this.label3.Location = new System.Drawing.Point(64, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "CGX Base:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Object Sizes:";
            // 
            // FormViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 575);
            this.Controls.Add(this.groupBoxOBJ);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.checkBoxCGRAMSwap);
            this.Controls.Add(this.checkBoxVisibleTiles);
            this.Controls.Add(this.pictureBoxSCR);
            this.Controls.Add(this.labelSCR);
            this.Controls.Add(this.labelCOL);
            this.Controls.Add(this.labelCGX);
            this.Controls.Add(this.checkBoxPalForce);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBoxCOL);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "FormViewer";
            this.Text = "Hyper CG-CAD - Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCGX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCOL)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSCR)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrame)).EndInit();
            this.groupBoxOBJ.ResumeLayout(false);
            this.groupBoxOBJ.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOBJSeq)).EndInit();
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
        private System.Windows.Forms.Button button4;
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
        private System.Windows.Forms.GroupBox groupBoxOBJ;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem exportOBJAsGIFToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownOBJSeq;
        private System.Windows.Forms.RadioButton radioButtonOBJSeq;
        private System.Windows.Forms.RadioButton radioButtonOBJRaw;
    }
}

