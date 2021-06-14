using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hcgcad
{
    public static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormViewer());
        }

        //Handy stuff
        public static T[] Subarray<T>(T[] obj, int i, int len)
        {
            T[] output = new T[len];

            for (int j = 0; j < len; j++)
            {
                if ((i + j) < obj.Length)
                    output[j] = obj[i + j];
                else
                    output[j] = obj[j - i];
            }

            return output;
        }

        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }

            return null;
        }

        public static byte[] LZWHacky(byte[] t)
        {
            //kinda uncompressed
            List<byte[]> blocks = new List<byte[]>();
            List<bool> bitStream = new List<bool>();
            foreach (byte b in t)
            {
                if (bitStream.Count >= (2022 - 9))
                {
                    //Too much data
                    blocks.Add(ToByteStream(bitStream.ToArray()));
                    bitStream.Clear();
                }
                if (bitStream.Count == 0)
                {
                    //Clear Code
                    byte[] start = { 0, 1 };
                    bitStream.AddRange(ToBitStream(start, 9));
                }
                //Add 9-bit Color Data
                byte[] stuff = { b, 0 };
                bitStream.AddRange(ToBitStream(stuff, 9));
            }
            if (bitStream.Count > 9)
            {
                //Stop Code
                blocks.Add(ToByteStream(bitStream.ToArray()));
                bitStream.Clear();
            }
            byte[] end = { 1, 1 };
            bitStream.AddRange(ToBitStream(end, 9));
            blocks.Add(ToByteStream(bitStream.ToArray()));
            bitStream.Clear();

            List<byte> lzw = new List<byte>();
            foreach (var block in blocks)
            {
                lzw.Add((byte)block.Length);
                lzw.AddRange(block);
            }
            lzw.Add(0);

            return lzw.ToArray();
        }

        public static bool[] ToBitStream(byte[] t, int bits)
        {
            bool[] bitstr = new bool[bits];
            for (int i = 0; i < bits; i++)
                bitstr[i] = (((t[i / 8] >> (i % 8)) & 1) != 0) ? true : false;
            return bitstr;
        }

        public static byte[] ToByteStream(bool[] bits)
        {
            byte[] bytes = new byte[Convert.ToInt32(Math.Ceiling((decimal)(bits.Length / 8.0)))];
            for (int i = 0; i < bits.Length; i++)
            {
                byte test = (byte)((bits[i]) ? (1 << (i % 8)) : 0);
                bytes[i / 8] |= test;
            }
            return bytes;
        }

        public static bool SaveGIF(string filename, Bitmap[] images, Color[] pal)
        {
            if (images.Length == 0 || pal.Length == 0 || filename == "")
                return false;

            int width = images[0].Width;
            int height = images[0].Height;

            List<byte> gif = new List<byte>();
            //Header and Version
            gif.AddRange(System.Text.Encoding.ASCII.GetBytes("GIF89a"));
            //Width
            gif.Add((byte)(width & 0xFF));
            gif.Add((byte)((width >> 8) & 0xFF));
            //Height
            gif.Add((byte)(height & 0xFF));
            gif.Add((byte)((height >> 8) & 0xFF));
            //Packed Fields - GCT Flag, 8-bit Prim Color, no Sort Flag, 256 colors
            gif.Add(0xF7);
            //Background Color Index = 00
            gif.Add(0);
            //No Aspect Ratio Given
            gif.Add(0);

            //Global Color Table
            foreach (Color c in pal)
            {
                gif.Add(c.R);
                gif.Add(c.G);
                gif.Add(c.B);
            }

            //Bitmap
            foreach (Bitmap b in images)
            {
                //-Graphic Control Extension Block
                gif.Add(0x21);      //EXT
                gif.Add(0xF9);      //GCE
                gif.Add(4);         //Size

                gif.Add(0b00001001);    //Packed - Restore to background color, transparent color flag

                //Delay Time
                gif.Add(10);
                gif.Add(0);

                //Transparent Color Index = 00
                gif.Add(0);

                //End Block
                gif.Add(0);

                //-Image Descriptor
                gif.Add(0x2C);      //Image Seperator
                //Left Pos
                gif.Add(0);
                gif.Add(0);
                //Top Pos
                gif.Add(0);
                gif.Add(0);
                //Width
                gif.Add((byte)(b.Width & 0xFF));
                gif.Add((byte)((b.Width >> 8) & 0xFF));
                //Height
                gif.Add((byte)(b.Height & 0xFF));
                gif.Add((byte)((b.Height >> 8) & 0xFF));

                //Packed = 00
                gif.Add(0);

                gif.Add(8);

                List<byte> indices = new List<byte>();
                for (int y = 0; y < b.Height; y++)
                {
                    for (int x = 0; x < b.Width; x++)
                    {
                        Color col = b.GetPixel(x, y);
                        byte index = 0;
                        if (col.A != 0)
                        {
                            for (byte i = 1; i < pal.Length; i++)
                            {
                                if (pal[i] == col)
                                {
                                    index = i;
                                    break;
                                }
                            }
                        }
                        indices.Add(index);
                    }
                }

                gif.AddRange(LZWHacky(indices.ToArray()));
            }
            //Termination
            gif.Add(0x3B);

            File.WriteAllBytes(filename, gif.ToArray());

            return true;
        }

        public static Rectangle GetBounding(Bitmap[] imgs)
        {
            Rectangle rect = new Rectangle(imgs[0].Width, imgs[0].Height, 0, 0);

            //Find Base
            foreach (Bitmap b in imgs)
            {
                for (int y = 0; y < b.Height; y++)
                {
                    for (int x = 0; x < b.Width; x++)
                    {
                        Color pixel = b.GetPixel(x, y);
                        if (pixel.A != 0)
                        {
                            rect.X = (x < rect.X) ? x : rect.X;
                            rect.Y = (y < rect.Y) ? y : rect.Y;
                        }
                    }
                }
            }

            //Width & Height
            foreach (Bitmap b in imgs)
            {
                for (int y = b.Height - 1; y >= 0; y--)
                {
                    for (int x = b.Width - 1; x >= 0; x--)
                    {
                        Color pixel = b.GetPixel(x, y);
                        if (pixel.A != 0)
                        {
                            rect.Width = ((x - rect.X) > rect.Width) ? (x - rect.X + 1) : rect.Width;
                            rect.Height = ((y - rect.Y) > rect.Height) ? (y - rect.Y + 1) : rect.Height;
                        }
                    }
                }
            }

            return rect;
        }
    }
}
