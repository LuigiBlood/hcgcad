using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hcgcad
{
    public class GraphicsRender
    {
        public class Nintendo
        {
            //SNES Tile Render Functions
            public static Bitmap Tile2BPP(byte[] dat, Color[] pal, bool xflip = false, bool yflip = false)
            {
                Bitmap tile = new Bitmap(8, 8);

                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        int colorID = (((dat[(y * 2)] & (1 << (7 - x))) << x) >> 7)
                            | (((dat[(y * 2) + 1] & (1 << (7 - x))) << x) >> 6);

                        int xt = x;
                        if (xflip) xt = 7 - x;
                        int yt = y;
                        if (yflip) yt = 7 - y;

                        tile.SetPixel(xt, yt, pal[colorID]);
                    }
                }

                return tile;
            }

            public static Bitmap Tile4BPP(byte[] dat, Color[] pal, bool xflip = false, bool yflip = false)
            {
                Bitmap tile = new Bitmap(8, 8);

                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        int colorID = (((dat[(y * 2)] & (1 << (7 - x))) << x) >> 7)
                            | (((dat[(y * 2) + 1] & (1 << (7 - x))) << x) >> 6)
                            | (((dat[(y * 2) + 16] & (1 << (7 - x))) << x) >> 5)
                            | (((dat[(y * 2) + 17] & (1 << (7 - x))) << x) >> 4);

                        int xt = x;
                        if (xflip) xt = 7 - x;
                        int yt = y;
                        if (yflip) yt = 7 - y;

                        tile.SetPixel(xt, yt, pal[colorID]);
                    }
                }

                return tile;
            }

            public static Bitmap Tile8BPP(byte[] dat, Color[] pal, bool xflip = false, bool yflip = false)
            {
                Bitmap tile = new Bitmap(8, 8);

                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        int colorID = (((dat[(y * 2)] & (1 << (7 - x))) << x) >> 7)
                            | (((dat[(y * 2) + 1] & (1 << (7 - x))) << x) >> 6)
                            | (((dat[(y * 2) + 16] & (1 << (7 - x))) << x) >> 5)
                            | (((dat[(y * 2) + 17] & (1 << (7 - x))) << x) >> 4)
                            | (((dat[(y * 2) + 32] & (1 << (7 - x))) << x) >> 3)
                            | (((dat[(y * 2) + 33] & (1 << (7 - x))) << x) >> 2)
                            | (((dat[(y * 2) + 48] & (1 << (7 - x))) << x) >> 1)
                            | (((dat[(y * 2) + 49] & (1 << (7 - x))) << x) >> 0);

                        int xt = x;
                        if (xflip) xt = 7 - x;
                        int yt = y;
                        if (yflip) yt = 7 - y;

                        tile.SetPixel(xt, yt, pal[colorID]);
                    }
                }

                return tile;
            }

            //SNES Palette
            public static Color[] PaletteFromByteArray(byte[] dat)
            {
                Color[] pal = new Color[dat.Length / 2];

                for (int i = 0; (i * 2) < dat.Length; i++)
                {
                    ushort colordata = (ushort)(dat[i * 2] | (dat[(i * 2) + 1] << 8));

                    int r = (int)((float)((colordata & 0x1F) / 31.0f) * 255);
                    int g = (int)((float)(((colordata >> 5) & 0x1F) / 31.0f) * 255);
                    int b = (int)((float)(((colordata >> 10) & 0x1F) / 31.0f) * 255);

                    pal[i] = Color.FromArgb(r, g, b);
                }

                return pal;
            }

            public static Bitmap RenderPalette(Color[] pal)
            {
                Bitmap output = new Bitmap(16 * 16, 16 * 16);

                for (int i = 0; i < Math.Min(pal.Length, 256); i++)
                {
                    using (Graphics g = Graphics.FromImage(output))
                    {
                        g.FillRectangle(new SolidBrush(pal[i]), (i % 16) * 16, (i / 16) * 16, 16, 16);
                    }
                }

                return output;
            }

            //SNES SuperCG-CAD Renderer
            //CGX
            public static Bitmap RenderCGX(byte[] cgx, Color[] pal, int palForce = -1)
            {
                //dat: CGX file
                //pal: color palette (imported from *.COL)
                //scale: scale int
                //palForce: Force palette index (-1 = use *.CGX)

                //Get CGX Format
                int fmt = 0;
                if (cgx.Length == 0x8500)
                    fmt = 1;
                else if (cgx.Length == 0x10100)
                    fmt = 2;

                //Get Footer Address
                int off_hdr = 0x4000;
                for (int i = 0; i < fmt; i++)
                    off_hdr *= 2;

                //Get Version
                float ver = float.Parse(System.Text.Encoding.ASCII.GetString(cgx, off_hdr + 0x13, 4), System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                int rev = int.Parse(System.Text.Encoding.ASCII.GetString(cgx, off_hdr + 0x18, 6));

                Bitmap output = new Bitmap(128, (128) * 4);

                for (int i = 0; i < (256 * 4); i++)
                {
                    int x = ((i % 16) * 8);
                    int y = ((i / 16) * 8);
                    int s = 8;
                    int p_b = 0;
                    int p = 0;
                    if (palForce == -1)
                    {
                        p_b = cgx[off_hdr + 0x22];
                        if (fmt < 2)
                            p = cgx[off_hdr + 0x100 + i];
                    }
                    else
                    {
                        p = palForce;
                    }

                    Bitmap tile;
                    switch (fmt)
                    {
                        case 0: //2bit
                            tile = Tile2BPP(Program.Subarray<byte>(cgx, i * 16, 16), Program.Subarray<Color>(pal, (p_b * 128) + (p * 4), 4));
                            break;
                        case 1: //4bit
                            tile = Tile4BPP(Program.Subarray<byte>(cgx, i * 32, 32), Program.Subarray<Color>(pal, (p_b * 128) + (p * 16), 16));
                            break;
                        default:
                        case 2: //8bit
                            tile = Tile8BPP(Program.Subarray<byte>(cgx, i * 64, 64), Program.Subarray<Color>(pal, (p_b * 128) + (p * 128), 256));
                            break;
                    }

                    using (Graphics g = Graphics.FromImage(output))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                        g.DrawImage(tile, x, y, s, s);
                    }
                }

                return output;
            }

            public static Bitmap RenderCGXTile(int tile, int size, byte[] cgx, Color[] pal, bool xflip = false, bool yflip = false)
            {
                Bitmap output = new Bitmap(size, size);

                int fmt = 0;
                if (cgx.Length == 0x8500)
                    fmt = 1;
                else if (cgx.Length == 0x10100)
                    fmt = 2;

                for (int y = 0; y < (size / 8); y++)
                {
                    for (int x = 0; x < (size / 8); x++)
                    {
                        using (Graphics g = Graphics.FromImage(output))
                        {
                            int tilecalc = (tile + (!xflip ? x : ((size / 8) - x - 1)) + (!yflip ? y * 0x10 : ((size / 8) - y - 1) * 0x10)) % 0x400;
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                            switch (fmt)
                            {
                                case 0:
                                    g.DrawImage(Tile2BPP(Program.Subarray<byte>(cgx, tilecalc * 16, 16), pal, xflip, yflip), x * 8, y * 8, 8, 8);
                                    break;
                                case 1:
                                    g.DrawImage(Tile4BPP(Program.Subarray<byte>(cgx, tilecalc * 32, 32), pal, xflip, yflip), x * 8, y * 8, 8, 8);
                                    break;
                                default:
                                case 2:
                                    g.DrawImage(Tile8BPP(Program.Subarray<byte>(cgx, tilecalc * 64, 64), pal, xflip, yflip), x * 8, y * 8, 8, 8);
                                    break;
                            }
                        }
                    }
                }

                return output;
            }

            //SCR
            public static Bitmap RenderSCR(byte[] scr, byte[] cgx, Color[] pal, bool allvisible = false)
            {
                //Get CGX Format
                int fmt = 0;
                if (cgx.Length == 0x8500)
                    fmt = 1;
                else if (cgx.Length == 0x10100)
                    fmt = 2;

                //Get Offset to Footer
                int off_hdr = 0x2000;

                //Get Version
                float ver = float.Parse(System.Text.Encoding.ASCII.GetString(scr, off_hdr + 0x13, 4), System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                int rev = int.Parse(System.Text.Encoding.ASCII.GetString(scr, off_hdr + 0x18, 6));

                //Tile Size
                int t = 8 * (scr[off_hdr + 0x42] + 1);

                Bitmap output = new Bitmap(512 * (t / 8), 512 * (t / 8));

                //Screen ID
                for (int s = 0; s < 4; s++)
                {
                    //Screen Data
                    for (int i = 0; i < 0x800; i += 2)
                    {
                        //X Pos
                        int x = (((s % 2) * (t * 32)) + (((i / 2) % 32) * t));
                        //Y Pos
                        int y = (((s / 2) * (t * 32)) + (((i / 2) / 32) * t));
                        //Scale
                        int z = t;

                        int p_b = scr[off_hdr + 0x45];

                        //Map
                        ushort dat = (ushort)(scr[(s * 0x800) + i] | (scr[(s * 0x800) + i + 1] << 8));
                        int tile = dat & 0x3FF;
                        int color = (dat & 0x1C00) >> 10;
                        bool xflip = ((dat & 0x4000) != 0);
                        bool yflip = ((dat & 0x8000) != 0);
                        bool visible = ((scr[off_hdr + 0x100 + (s * 0x80) + (((i / 2) % 32) / 8)] >> (7 - (((i / 2) % 32) % 8))) & 1) != 0;
                        if (allvisible)
                            visible = true;
                        if (!visible)
                            continue;

                        Bitmap chr;
                        switch (fmt)
                        {
                            case 0: //2bit
                                chr = RenderCGXTile(tile, z, cgx, Program.Subarray<Color>(pal, (p_b * 128) + (color * 4), 4), xflip, yflip);
                                break;
                            case 1: //4bit
                                chr = RenderCGXTile(tile, z, cgx, Program.Subarray<Color>(pal, (p_b * 128) + (color * 16), 16), xflip, yflip);
                                break;
                            default:
                            case 2: //8bit
                                chr = RenderCGXTile(tile, z, cgx, Program.Subarray<Color>(pal, (p_b * 128) + ((color & 1) * 128), 256), xflip, yflip);
                                break;
                        }

                        using (Graphics g = Graphics.FromImage(output))
                        {
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                            g.DrawImage(chr, x, y, z, z);
                        }
                    }
                }

                return output;
            }

            public static Bitmap RenderOBJ(int seq, int frame, byte[] obj, byte[] cgx, Color[] pal, byte obj_size, byte cgx_bank)
            {
                //Get Offset to Footer
                int off_hdr = 0x3000;

                int entry = obj[off_hdr + 0x100 + (seq * 0x40) + (frame * 2) + 1];

                return RenderOBJ(entry, obj, cgx, pal, obj_size, cgx_bank);
            }

            public static Bitmap RenderOBJ(int entry, byte[] obj, byte[] cgx, Color[] pal, byte obj_size, byte cgx_bank)
            {
                //Tile Sizes
                int[] tilesizes = { 8, 16, 8, 32, 8, 64, 16, 32, 16, 64, 32, 64 };

                //Get CGX Format
                int fmt = 0;
                if (cgx.Length == 0x8500)
                    fmt = 1;
                else if (cgx.Length == 0x10100)
                    fmt = 2;

                //Get Offset to Footer
                int off_hdr = 0x3000;

                //Get Version
                float ver = float.Parse(System.Text.Encoding.ASCII.GetString(obj, off_hdr + 0x13, 4), System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                int rev = int.Parse(System.Text.Encoding.ASCII.GetString(obj, off_hdr + 0x18, 6));

                //Get Data
                //byte obj_size = obj[off_hdr + 0x50];
                byte pal_half = obj[off_hdr + 0x53];
                //byte cgx_bank = obj[off_hdr + 0x56];

                Bitmap output = new Bitmap(256, 256);

                using (Graphics g = Graphics.FromImage(output))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    g.FillRectangle(new SolidBrush(Color.FromArgb(0, 254, 1, 254)), 0, 0, 256, 256);
                }

                //Get All Frame Data at once
                for (int i = 63; i >= 0; i--)
                {
                    bool visible = (obj[(entry * 0x180) + (i * 6) + 0] & 0x80) != 0;
                    if (!visible)
                        continue;
                    bool sizetype = (obj[(entry * 0x180) + (i * 6) + 0] & 0x01) != 0;
                    int size = tilesizes[(obj_size * 2) + (sizetype ? 1 : 0)];
                    byte group = obj[(entry * 0x180) + (i * 6) + 1];
                    sbyte y = (sbyte)obj[(entry * 0x180) + (i * 6) + 2];
                    sbyte x = (sbyte)obj[(entry * 0x180) + (i * 6) + 3];

                    //Assumes Big Endian for now
                    bool yflip = (obj[(entry * 0x180) + (i * 6) + 4] & 0x80) != 0;
                    bool xflip = (obj[(entry * 0x180) + (i * 6) + 4] & 0x40) != 0;
                    byte priority = (byte)((obj[(entry * 0x180) + (i * 6) + 4] & 0x30) >> 8);
                    byte color = (byte)((obj[(entry * 0x180) + (i * 6) + 4] & 0x0E) >> 1);
                    int tile = ((obj[(entry * 0x180) + (i * 6) + 4] & 0x01) << 8) | obj[(entry * 0x180) + (i * 6) + 5];

                    //Get 16-color Palette
                    Color[] sprpal = Program.Subarray(pal, (pal_half * 128) + (color * 16), 16);
                    sprpal[0] = Color.FromArgb(0, sprpal[0].R, sprpal[0].G, sprpal[0].B); //Must be transparent
                    Bitmap chr = RenderCGXTile((cgx_bank * 256) + tile, size, cgx, sprpal, xflip, yflip);

                    using (Graphics g = Graphics.FromImage(output))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                        g.DrawImage(chr, (128 + x), (128 + y), size, size);
                    }
                }
                return output;
            }

            public static bool RenderOBJAnim(int seq, byte[] obj, byte[] cgx, Color[] pal, byte obj_size, byte cgx_bank, out Bitmap[] frames, out int[] durations)
            {
                int amountframe = 0;
                for (int i = 0; i < 0x40; i += 2)
                {
                    if ((obj[0x3100 + (seq * 0x40) + i] == 0) && (obj[0x3100 + (seq * 0x40) + i + 1] == 0))
                    {
                        amountframe = (i / 2) - 1;
                        break;
                    }
                }

                frames = new Bitmap[amountframe];
                durations = new int[amountframe];

                if (amountframe == 0)
                    return false;

                for (int i = 0; i < amountframe; i++)
                {
                    durations[i] = (obj[0x3100 + (seq * 0x40) + i * 2] * 16) / 10;
                    frames[i] = RenderOBJ(seq, i, obj, cgx, pal, obj_size, cgx_bank);
                }

                return true;
            }
        }

        public static Bitmap ScaleBitmap(Bitmap input, int scale)
        {
            Bitmap output = new Bitmap(input.Width * scale, input.Height * scale);

            using (Graphics g = Graphics.FromImage(output))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.DrawImage(input, 0, 0, input.Width * scale, input.Height * scale);
            }

            return output;
        }
    }
}
