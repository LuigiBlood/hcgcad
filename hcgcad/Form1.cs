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
        static byte[] cgx;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "*.col|*.COL|*.col.bak|*.col.bak|*.*|All files";
            o.Title = "Load COL file...";
            if (o.ShowDialog() == DialogResult.OK)
            {
                Stream file = o.OpenFile();
                byte[] paldat = new byte[512];
                file.Read(paldat, 0, 512);
                file.Close();

                pal = GraphicsRender.Nintendo.PaletteFromByteArray(paldat);
                pictureBox2.Image = GraphicsRender.Nintendo.RenderPalette(pal);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "*.cgx|*.cgx|*.cgx.bak|*.cgx.bak|*.*|All files";
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


                int fmt = 0;
                if (cgx.Length == 0x8500)
                    fmt = 1;
                else if (cgx.Length == 0x10100)
                    fmt = 2;

                pictureBox1.Image = GraphicsRender.Nintendo.RenderCGX(fmt, cgx, pal, 2);
                pictureBox1.Size = pictureBox1.Image.Size;
            }
        }
    }
}
