using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;

namespace hcgcad
{
    public class GIF
    {
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
                    blocks.Add(Utility.ToByteStream(bitStream.ToArray()));
                    bitStream.Clear();
                }
                if (bitStream.Count == 0)
                {
                    //Clear Code
                    byte[] start = { 0, 1 };
                    bitStream.AddRange(Utility.ToBitStream(start, 9));
                }
                //Add 9-bit Color Data
                byte[] stuff = { b, 0 };
                bitStream.AddRange(Utility.ToBitStream(stuff, 9));
            }
            if (bitStream.Count > 9)
            {
                //Stop Code
                blocks.Add(Utility.ToByteStream(bitStream.ToArray()));
                bitStream.Clear();
            }
            byte[] end = { 1, 1 };
            bitStream.AddRange(Utility.ToBitStream(end, 9));
            blocks.Add(Utility.ToByteStream(bitStream.ToArray()));
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

        public static bool SaveGIF(string filename, Bitmap[] images, Color[] pal, int[] duration)
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

            //Application Extension
            gif.Add(0x21);      //EXT
            gif.Add(0xFF);      //App
            gif.Add(0x0B);      //Size
            gif.AddRange(System.Text.Encoding.ASCII.GetBytes("NETSCAPE2.0"));
            gif.Add(0x03);      //Size Sub Block
            gif.Add(0x01);      //Index Sub Block

            gif.Add(0x00);      //Repeat
            gif.Add(0x00);

            gif.Add(0x00);      //End Sub Block

            //Bitmap
            for (int j = 0; j < images.Length; j++)
            {
                Bitmap b = images[j];

                //-Graphic Control Extension Block
                gif.Add(0x21);      //EXT
                gif.Add(0xF9);      //GCE
                gif.Add(4);         //Size

                gif.Add(0x09);    //Packed - Restore to background color, transparent color flag

                //Delay Time
                gif.Add((byte)duration[j]);
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
    }
}
