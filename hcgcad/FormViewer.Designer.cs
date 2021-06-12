
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
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCGX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCOL)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSCR)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Load COL/CGX/SCR...";
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
            this.pictureBoxCOL.Location = new System.Drawing.Point(12, 202);
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
            this.labelCOL.Location = new System.Drawing.Point(12, 183);
            this.labelCOL.Name = "labelCOL";
            this.labelCOL.Size = new System.Drawing.Size(31, 13);
            this.labelCOL.TabIndex = 9;
            this.labelCOL.Text = "COL:";
            // 
            // labelSCR
            // 
            this.labelSCR.AutoSize = true;
            this.labelSCR.Location = new System.Drawing.Point(570, 38);
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
            this.pictureBoxSCR.Location = new System.Drawing.Point(573, 55);
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
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 14;
            this.button4.Text = "Unload SCR";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1092, 24);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.exportCGXAsPNGToolStripMenuItem,
            this.exportSCRAsPNGToolStripMenuItem,
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
            this.loadToolStripMenuItem.Text = "Load File...";
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
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(29, 143);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 17;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // FormViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 575);
            this.Controls.Add(this.numericUpDown1);
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
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
        private System.Windows.Forms.NumericUpDown numericUpDown1;
    }
}

