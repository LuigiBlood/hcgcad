using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace hcgcad
{
    public class Utility
    {
        //Handy stuff
        public static T[] Subarray<T>(T[] obj, int i, int len)
        {
            T[] output = new T[len];

            for (int j = 0; j < len; j++)
            {
                output[j] = obj[(i + j) % obj.Length];
            }

            return output;
        }

        public static bool[] ToBitStream(byte[] t, int bits)
        {
            bool[] bitstr = new bool[bits];
            for (int i = 0; i < bits; i++)
                bitstr[i] = (((t[i / 8] >> (i % 8)) & 1) != 0) ? true : false;
            return bitstr;
        }

        public static bool[] ToBitStreamReverse(byte[] t, int bits)
        {
            bool[] bitstr = new bool[bits];
            for (int i = 0; i < bits; i++)
                bitstr[i] = (((t[i / 8] << (i % 8)) & 0x80) != 0) ? true : false;
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

        //Get Rectangle for Cropping
        public static Rectangle GetBoundingRect(Bitmap[] imgs)
        {
            if (imgs.Length == 0)
                return new Rectangle(0, 0, 0, 0);

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
                            rect.Width = ((x - rect.X + 1) > rect.Width) ? (x - rect.X + 1) : rect.Width;
                            rect.Height = ((y - rect.Y + 1) > rect.Height) ? (y - rect.Y + 1) : rect.Height;
                        }
                    }
                }
            }

            return rect;
        }
    }
}
