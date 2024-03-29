﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace hcgcadviewer
{
    public class CAD
    {
        public class Extra
        {
            string magic;   //"NAK1989 S-CG-CAD"
            string ver;     //"VerX.XX "
            string date;    //"YYMMDD  " / "YYMMDD F"

            public Extra(string ext)
            {
                magic = ext.Substring(0, 0x10);
                ver = ext.Substring(0x10, 8);
                date = ext.Substring(0x18, 8);
            }
        }

        public class CGX
        {
            byte[] chr;     //Graphics Data
            Extra ext;
            byte bitmode;
            byte col_bank;  //Color Bank
            byte col_half;  //Color (high, low)
            byte col_cell;  //Color Cell
            byte[] attr;    //Attribute Data

            public CGX(byte[] dat)
            {
                //Get Format
                int fmt = 0;
                int off_hdr = 0x4000;
                if (dat.Length == 0x8500)
                {
                    off_hdr = 0x8000;
                    fmt = 1;
                }
                else if (dat.Length == 0x10100)
                {
                    off_hdr = 0x10000;
                    fmt = 2;
                }

                //Get Extra
                ext = new Extra(System.Text.Encoding.ASCII.GetString(Utility.Subarray(dat, off_hdr, 0x20)));
                bitmode = dat[off_hdr + 0x20];
                col_bank = dat[off_hdr + 0x21];
                col_half = dat[off_hdr + 0x22];
                col_cell = dat[off_hdr + 0x23];

                //Graphics
                chr = Utility.Subarray(dat, 0, off_hdr);
                //Attributes (2BPP & 4BPP only)
                if (fmt < 2)
                    attr = Utility.Subarray(dat, off_hdr + 0x100, 0x400);
            }

            public int GetFormat()
            {
                if (chr.Length == 0x8000)
                {
                    return 1;
                }
                else if (chr.Length == 0x10000)
                {
                    return 2;
                }
                return 0;
            }

            public int GetColorBase()
            {
                switch (GetFormat())
                {
                    case 0:
                        return ((col_half * 128) + (col_cell * 4)) % 256;
                    case 1:
                        return ((col_half * 128) + (col_cell * 16)) % 256;
                    default:
                    case 2:
                        return ((col_half * 128) + (col_cell * 128)) % 256;
                }
            }

            public int GetColorBase(int attr)
            {
                switch (GetFormat())
                {
                    case 0:
                        return ((col_half * 128) + (col_cell * 4) + (attr * 4)) % 256;
                    case 1:
                        return ((col_half * 128) + (col_cell * 16) + (attr * 16)) % 256;
                    default:
                    case 2:
                        return ((col_half * 128) + (col_cell * 128) + (attr * 128)) % 256;
                }
            }

            public Bitmap Render(COL pal, int palForce = -1)
            {
                Bitmap output = new Bitmap(128, 128 * 4);
                int fmt = GetFormat();
                for (int i = 0; i < (256 * 4); i++)
                {
                    int x = ((i % 16) * 8);
                    int y = ((i / 16) * 8);
                    int s = 8;
                    int p = GetColorBase();
                    if (palForce == -1)
                    {
                        //p_b = cgx[off_hdr + 0x22];
                        if (fmt < 2)
                            p = GetColorBase(attr[i]);
                    }
                    else
                    {
                        if (fmt == 0)
                            p = palForce * 4;
                        else if (fmt == 1)
                            p = palForce * 16;
                        else //if (fmt == 2)
                            p = palForce * 128;
                    }

                    Bitmap tile = RenderTile(i, 8, pal.GetPalette(fmt, p), false);

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

            public Bitmap RenderTile(int tile, int size, Color[] pal, bool xflip = false, bool yflip = false, bool bgcolor = false)
            {
                Bitmap output = new Bitmap(size, size);

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
                            switch (GetFormat())
                            {
                                case 0:
                                    g.DrawImage(hcgcadviewer.Render.SNES.Tile2BPP(Utility.Subarray(chr, tilecalc * 16, 16), pal, xflip, yflip, bgcolor), x * 8, y * 8, 8, 8);
                                    break;
                                case 1:
                                    g.DrawImage(hcgcadviewer.Render.SNES.Tile4BPP(Utility.Subarray(chr, tilecalc * 32, 32), pal, xflip, yflip, bgcolor), x * 8, y * 8, 8, 8);
                                    break;
                                default:
                                case 2:
                                    g.DrawImage(hcgcadviewer.Render.SNES.Tile8BPP(Utility.Subarray(chr, tilecalc * 64, 64), pal, xflip, yflip, bgcolor), x * 8, y * 8, 8, 8);
                                    break;
                            }
                        }
                    }
                }

                return output;
            }

            public int CopyBank(CGX source, int banksrc, int bankdst)
            {
                int fmt = this.GetFormat();
                if (fmt != source.GetFormat())
                {
                    return -1;
                }

                int offsetsrc_chr = banksrc * 0x80;
                int offsetdst_chr = bankdst * 0x80;
                int size_chr = 0x80;
                int offsetsrc_attr = banksrc * 0x80;
                int offsetdst_attr = bankdst * 0x80;
                int size_attr = 0x80;

                switch (fmt)
                {
                    case 0:
                        //2BPP
                        offsetsrc_chr *= 0x10;
                        offsetdst_chr *= 0x10;
                        size_chr *= 0x10;
                        break;
                    case 1:
                        //4BPP
                        offsetsrc_chr *= 0x20;
                        offsetdst_chr *= 0x20;
                        size_chr *= 0x20;
                        break;
                    case 2:
                        //8BPP
                        offsetsrc_chr *= 0x40;
                        offsetdst_chr *= 0x40;
                        size_chr *= 0x40;
                        break;
                }

                Array.Copy(source.chr, offsetsrc_chr, this.chr, offsetdst_chr, size_chr);
                if (fmt < 2) Array.Copy(source.attr, offsetsrc_attr, this.attr, offsetdst_attr, size_attr);
                return 0;
            }

            public static CGX Load(FileStream file)
            {
                //CGX
                file.Seek(0, SeekOrigin.Begin);

                //Check File Size
                if (file.Length != 0x4500 && file.Length != 0x8500 && file.Length != 0x10100)
                    return null;

                int off_hdr = 0x4000;
                if (file.Length == 0x8500)
                {
                    off_hdr = 0x8000;
                }
                else if (file.Length == 0x10100)
                {
                    off_hdr = 0x10000;
                }

                //Load File
                byte[] cgx_t = new byte[file.Length];
                file.Read(cgx_t, 0, (int)file.Length);

                //Check Footer Info
                string footer_string = System.Text.Encoding.ASCII.GetString(Utility.Subarray(cgx_t, off_hdr, 0x10));
                if (!footer_string.Equals("NAK1989 S-CG-CAD"))
                    return null;

                return new CGX(cgx_t);
            }

            public static CGX Import(FileStream file, int fmt)
            {
                byte[] dat;
                int maxlen;
                if (fmt == 0)
                {
                    dat = new byte[0x4500];
                    maxlen = 0x4000;
                }
                else if (fmt == 1)
                {
                    dat = new byte[0x8500];
                    maxlen = 0x8000;
                }
                else //if (fmt == 2)
                {
                    dat = new byte[0x10100];
                    maxlen = 0x10000;
                }

                //Generate File
                file.Read(dat, 0, Math.Min(maxlen, (int)file.Length));
                System.Text.Encoding.ASCII.GetBytes("NAK1989 S-CG-CAD").CopyTo(dat, maxlen);
                System.Text.Encoding.ASCII.GetBytes("Ver0.00 ").CopyTo(dat, maxlen + 0x10);
                System.Text.Encoding.ASCII.GetBytes("010101  ").CopyTo(dat, maxlen + 0x18);

                return new CGX(dat);
            }
        }

        public class COL
        {
            Color[] col;    //Color Palette
            Extra ext;
            byte bitmode;
            byte[] unk;

            bool swap;

            public COL(byte[] dat1)
            {
                col = Render.PaletteFromByteArray(dat1);
                swap = false;
            }

            public COL(byte[] dat1, byte[] dat2)
            {
                col = Render.PaletteFromByteArray(dat1);
                ext = new Extra(System.Text.Encoding.ASCII.GetString(Utility.Subarray(dat2, 0, 0x20)));
                bitmode = dat2[0x30];
                unk = Utility.Subarray(dat2, 0x100, 0x100);
                swap = false;
            }

            public void SetPaletteSwap(bool v)
            {
                swap = v;
            }

            public Color[] GetPalette()
            {
                return col;
            }

            public Color[] GetPalette(int fmt, int base_id)
            {
                if (fmt == 0)   //2BPP
                    return Utility.Subarray(col, base_id + (swap ? 128 : 0), 4);
                else if (fmt == 1)  //4BPP
                    return Utility.Subarray(col, base_id + (swap ? 128 : 0), 16);
                else    //if (fmt == 2)     //8BPP
                    return Utility.Subarray(col, base_id + (swap ? 128 : 0), 256);
            }

            public Bitmap RenderPalette()
            {
                Bitmap output = new Bitmap(16 * 16, 16 * 16);
                Color[] pal = Utility.Subarray(col, (swap ? 128 : 0), 256);
                for (int i = 0; i < Math.Min(pal.Length, 256); i++)
                {
                    using (Graphics g = Graphics.FromImage(output))
                        g.FillRectangle(new SolidBrush(pal[i]), (i % 16) * 16, (i / 16) * 16, 16, 16);
                }

                return output;
            }

            public static COL Load(FileStream file)
            {
                //COL
                file.Seek(0, SeekOrigin.Begin);

                //Check File Size
                if (file.Length != 0x200 && file.Length != 0x400)
                    return null;

                byte[] paldat = new byte[512];
                file.Read(paldat, 0, 512);
                if (file.Length == 0x400)
                {
                    byte[] palftr = new byte[512];
                    file.Read(palftr, 0, 512);

                    //Check Footer Info
                    string footer_string = System.Text.Encoding.ASCII.GetString(Utility.Subarray(palftr, 0, 0x10));
                    if (!footer_string.Equals("NAK1989 S-CG-CAD"))
                        return null;

                    return new COL(paldat, palftr);
                }
                else
                {
                    return new COL(paldat);
                }
                //return null;
            }

            public static COL Import(FileStream file)
            {
                file.Seek(0, SeekOrigin.Begin);

                byte[] col_t = new byte[0x200];
                file.Read(col_t, 0, Math.Min(0x200, (int)file.Length));

                return new COL(col_t);
            }
        }

        public class SCR
        {
            byte[][] cell;  //Screen Data (4 screens of 32x32)
            Extra ext;
            byte bitmode;
            bool mode7;     //Mode 7 Flag
            byte scr_mode;  //Screen Mode: 0 = 8x8 Tiles, 1 = 16x16 Tiles
            byte chr_bank;  //CHR BANK
            byte col_bank;  //Color Bank
            byte col_half;  //Color (high, low)
            byte col_cell;  //Color Cell
            ushort clr_chr_no;

            bool[][] clear; //Clear Code (4 screens of 32x32, false = invisible tile, true = visible tile)

            enum format
            {
                Normal,
                F,
                NoClearData,
            };

            format fmt;

            public SCR(byte[] dat)
            {
                fmt = format.Normal;
                if (dat.Length == 0x4100)
                    fmt = format.F;
                if (dat.Length == 0x2100)
                    fmt = format.NoClearData;

                cell = new byte[4][];
                clear = new bool[4][];
                for (int i = 0; i < 4; i++)
                {
                    cell[i] = Utility.Subarray(dat, i * 0x800, 0x800);

                    if (fmt == format.Normal)
                    {
                        byte[] tmp = new byte[0x80];
                        for (int j = 0; j < 0x80; j++)
                            tmp[j] = dat[0x2100 + ((i & 2) * 0x80) + ((i & 1) * 4) + (j % 4) + ((j / 4) * 8)];
                        clear[i] = Utility.ToBitStreamReverse(tmp, 0x400);
                    }
                    else if (fmt == format.F)
                    {
                        clear[i] = new bool[32 * 32];
                        for (int j = 0; (j < 32 * 32); j++)
                        {
                            clear[i][j] = (dat[0x2100 + (i * 0x800) + (j * 2) + 1] & 0x80) != 0;
                        }
                    }
                    else //if (fmt == format.NoClearData)
                    {
                        clear[i] = new bool[32 * 32];
                        for (int j = 0; (j < 32 * 32); j++)
                        {
                            clear[i][j] = true;
                        }
                    }
                }
                ext = new Extra(System.Text.Encoding.ASCII.GetString(Utility.Subarray(dat, 0x2000, 0x20)));
                bitmode = dat[0x2040];
                mode7 = dat[0x2041] != 0;
                scr_mode = dat[0x2042];
                chr_bank = dat[0x2043];
                col_bank = dat[0x2044];
                col_half = dat[0x2045];
                col_cell = dat[0x2046];
                clr_chr_no = (ushort)((dat[0x2047] << 8) | dat[0x2048]);
            }

            public int GetTileSize()
            {
                return 8 * (scr_mode + 1);
            }

            public Bitmap Render(CGX cgx, COL col, bool allvisible = false, bool bgcolor = false)
            {
                //Get CGX Format
                int fmt = cgx.GetFormat();

                //Tile Size
                int t = 8 * (scr_mode + 1);

                Bitmap output = new Bitmap(512 * (t / 8), 512 * (t / 8));

                //Fill BG Color
                int p_b = col_half;
                if (bgcolor)
                {
                    using (Graphics g = Graphics.FromImage(output))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                        g.FillRectangle(new SolidBrush(col.GetPalette()[(p_b * 128) + col_cell]), 0, 0, output.Width, output.Height);
                    }
                }

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

                        //Map
                        ushort dat = (ushort)(cell[s][i] | (cell[s][i + 1] << 8));
                        int tile = dat & 0x3FF;
                        int color = (dat & 0x1C00) >> 10;
                        bool xflip = ((dat & 0x4000) != 0);
                        bool yflip = ((dat & 0x8000) != 0);
                        bool visible = allvisible ? true : clear[s][i / 2];
                        if (!visible) continue;

                        Bitmap chr;
                        switch (fmt)
                        {
                            case 0: //2bit
                                chr = cgx.RenderTile(tile, z, col.GetPalette(fmt, (p_b * 128) + (color * 4)), xflip, yflip, bgcolor);
                                break;
                            case 1: //4bit
                                chr = cgx.RenderTile(tile, z, col.GetPalette(fmt, (p_b * 128) + (color * 16)), xflip, yflip, bgcolor);
                                break;
                            default:
                            case 2: //8bit
                                chr = cgx.RenderTile(tile, z, col.GetPalette(fmt, (p_b * 128) + (color * 128 * 0)), xflip, yflip, bgcolor);
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

            public static SCR Load(FileStream file)
            {
                file.Seek(0, SeekOrigin.Begin);

                //Check File Size
                if (file.Length != 0x2100 && file.Length != 0x2300 && file.Length != 0x4100)
                    return null;

                byte[] scr_t = new byte[file.Length];
                file.Read(scr_t, 0, (int)file.Length);

                //Check Footer Info
                string footer_string = System.Text.Encoding.ASCII.GetString(Utility.Subarray(scr_t, 0x2000, 0x10));
                if (!footer_string.Equals("NAK1989 S-CG-CAD"))
                    return null;

                return new SCR(scr_t);
            }

            public static SCR Import(FileStream file, byte scr_mode = 0)
            {
                byte[] dat = new byte[0x2300];
                int maxlen = 0x2000;

                //Generate File
                file.Read(dat, 0, Math.Min(maxlen, (int)file.Length));
                System.Text.Encoding.ASCII.GetBytes("NAK1989 S-CG-CAD").CopyTo(dat, maxlen);
                System.Text.Encoding.ASCII.GetBytes("Ver0.00 ").CopyTo(dat, maxlen + 0x10);
                System.Text.Encoding.ASCII.GetBytes("010101  ").CopyTo(dat, maxlen + 0x18);
                dat[0x2042] = scr_mode;
                Utility.Subarray(new byte[] { 0xFF }, 0, 0x200).CopyTo(dat, 0x2100);

                return new SCR(dat);
            }
        }

        public class OBJ
        {
            struct entry
            {
                public bool disp;      //Byte 0: D0000000
                public bool size;      //Byte 0: 0000000S
                public byte group;     //Byte 1
                public sbyte y;        //Byte 2
                public sbyte x;        //Byte 3
                public bool yflip;     //Byte 4: Y0000000 00000000
                public bool xflip;     //Byte 4: 0X000000 00000000
                public byte pri;       //Byte 4: 00PP0000 00000000
                public byte color;     //Byte 4: 0000CCC0 00000000
                public ushort tile;    //Byte 4: 0000000T TTTTTTTT

                public entry(byte[] dat, bool littleendian = false)
                {
                    disp = (dat[0] & 0x80) != 0;
                    size = (dat[0] & 0x01) != 0;
                    group = dat[1];
                    y = (sbyte)dat[2];
                    x = (sbyte)dat[3];

                    byte byte1;
                    byte byte2;

                    if (littleendian)
                    {
                        byte1 = dat[5];
                        byte2 = dat[4];
                    }
                    else
                    {
                        byte1 = dat[4];
                        byte2 = dat[5];
                    }

                    yflip = (byte1 & 0x80) != 0;
                    xflip = (byte1 & 0x40) != 0;
                    pri = (byte)((byte1 & 0x30) >> 4);
                    color = (byte)((byte1 & 0x0E) >> 1);
                    tile = (ushort)(((byte1 & 0x01) << 8) | byte2);
                }
            }

            struct sequence
            {
                public byte duration;
                public byte frame;

                public sequence(byte d, byte f)
                {
                    duration = d;
                    frame = f;
                }
            }

            struct sequence_pos
            {
                public sbyte x;
                public sbyte y;
            }

            entry[][] frames;   //32 frames, 64 entries each ([32][64])
            sequence[][] sequences; //16 sequences, 16 frames each ([16][32])
            sequence_pos[] sequences_pos; //16 sequences, one position each
            Extra ext;
            byte obj_mode;
            byte chr_bank;
            byte col_half;
            byte col_cell;
            byte vsize_flag;
            byte chr_s_bankl;
            byte chr_s_bankh;

            enum format {
                Normal,
                F,
            };

            format fmt;

            public OBJ(byte[] dat)
            {
                fmt = format.Normal;
                if (dat.Length == 0x3520)
                    fmt = format.F;
                ext = new Extra(System.Text.Encoding.ASCII.GetString(Utility.Subarray(dat, 0x3000, 0x20)));
                obj_mode = dat[0x3050];
                chr_bank = dat[0x3051];
                col_half = dat[0x3052];
                col_cell = dat[0x3053];
                vsize_flag = dat[0x3054];
                chr_s_bankl = dat[0x3055];
                chr_s_bankh = dat[0x3056];

                frames = new entry[32][];
                for (int f = 0; f < 32; f++)
                {
                    frames[f] = new entry[64];
                    for (int e = 0; e < 64; e++)
                        frames[f][e] = new entry(Utility.Subarray(dat, (0x180 * f) + (e * 6), 6), fmt == format.F);
                }

                sequences = new sequence[16][];
                sequences_pos = new sequence_pos[16];
                for (int f = 0; f < 16; f++)
                {
                    sequences[f] = new sequence[32];
                    for (int e = 0; e < 32; e++)
                        sequences[f][e] = new sequence(dat[0x3100 + (f * 0x40) + (e * 2) + 0], dat[0x3100 + (f * 0x40) + (e * 2) + 1]);

                    if (fmt == format.F)
                    {
                        sequences_pos[f].x = (sbyte)dat[0x3500 + (f * 2) + 0];
                        sequences_pos[f].y = (sbyte)dat[0x3500 + (f * 2) + 1];
                    }
                    else
                    {
                        sequences_pos[f].x = 0;
                        sequences_pos[f].y = 0;
                    }
                }
            }

            public int GetSequenceFrameAmount(int seq)
            {
                int i = 0;
                for (; i < sequences[seq].Length; i++)
                {
                    if (sequences[seq][i].duration == 0 && sequences[seq][i].frame == 0)
                        return i;
                }
                return i;
            }

            public Bitmap Render(int seq, int frame, CGX cgx, COL col, int obj_size, int cgx_bank)
            {
                int f = sequences[seq][frame].frame;

                return Render(f, cgx, col, obj_size, cgx_bank);
            }

            public Bitmap Render(int frame, CGX cgx, COL col, int obj_size, int cgx_bank)
            {
                //Tile Sizes
                int[] tilesizes = { 8, 16, 8, 32, 8, 64, 16, 32, 16, 64, 32, 64 };

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
                    if (!frames[frame][i].disp)
                        continue;
                    int size = tilesizes[(obj_size * 2) + (frames[frame][i].size ? 1 : 0)];
                    byte group = frames[frame][i].group;
                    sbyte y = frames[frame][i].y;
                    sbyte x = frames[frame][i].x;

                    //Assumes Big Endian for now
                    bool yflip = frames[frame][i].yflip;
                    bool xflip = frames[frame][i].xflip;
                    byte priority = frames[frame][i].pri;
                    byte color = frames[frame][i].color;
                    int tile = frames[frame][i].tile;

                    //Get 16-color Palette
                    Color[] sprpal = col.GetPalette(1, (col_cell * 128) + (color * 16));
                    //Color[] sprpal = Utility.Subarray(pal, (col_half * 128) + (color * 16), 16);
                    sprpal[0] = Color.FromArgb(0, sprpal[0].R, sprpal[0].G, sprpal[0].B); //Must be transparent
                    Bitmap chr = cgx.RenderTile((cgx_bank * 128) + tile, size, sprpal, xflip, yflip, false);
                    //Bitmap chr = RenderCGXTile((cgx_bank * 128) + tile, size, cgx, sprpal, xflip, yflip);

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

            public bool RenderOBJAnim(int seq, CGX cgx, COL pal, byte obj_size, byte cgx_bank, out Bitmap[] frames, out int[] durations)
            {
                int amountframe = GetSequenceFrameAmount(seq);

                frames = new Bitmap[amountframe];
                durations = new int[amountframe];

                if (amountframe == 0)
                    return false;

                for (int i = 0; i < amountframe; i++)
                {
                    durations[i] = sequences[seq][i].duration;
                    frames[i] = Render(seq, i, cgx, pal, obj_size, cgx_bank);
                    //durations[i] = (obj[0x3100 + (seq * 0x40) + i * 2] * 16) / 10;
                    //frames[i] = RenderOBJ(seq, i, obj, cgx, pal, obj_size, cgx_bank);
                }

                return true;
            }

            public static OBJ Load(FileStream file)
            {
                //OBJ
                file.Seek(0, SeekOrigin.Begin);

                //Check File Size
                if (file.Length != 0x3500 && file.Length != 0x3520)
                    return null;

                byte[] obj_t = new byte[file.Length];
                file.Read(obj_t, 0, (int)file.Length);

                //Check Footer Info
                string footer_string = System.Text.Encoding.ASCII.GetString(Utility.Subarray(obj_t, 0x3000, 0x10));
                if (!footer_string.Equals("NAK1989 S-CG-CAD"))
                    return null;

                return new OBJ(obj_t);
            }
        }

        public class PNL
        {
            byte[] cell;  //Panel Data (4 screens of 32x32)
            Extra ext;
            byte plbank;
            bool mode7;     //Mode 7 Flag
            byte scr_mode;  //Screen Mode: 0 = 8x8 Tiles, 1 = 16x16 Tiles
            byte chr_bank;  //CHR BANK
            byte col_bank;  //Color Bank
            public byte col_half;  //Color (high, low)
            public byte col_cell;  //Color Cell
            ushort unk1;
            byte unk2;
            byte unk3;

            bool[] clear; //Clear Code (false = invisible tile, true = visible tile)

            public PNL(byte[] dat)
            {
                cell = Utility.Subarray(dat, 0x100, 0x8000);
                clear = new bool[0x4000];
                for (int i = 0; i < 0x4000; i++) clear[i] = dat[0x8100 + (i * 2)] != 0;
                ext = new Extra(System.Text.Encoding.ASCII.GetString(Utility.Subarray(dat, 0x0000, 0x20)));
                plbank = dat[0x0060];
                //mode7 = dat[0x0061] != 0;
                //scr_mode = dat[0x0062];
                //chr_bank = dat[0x0063];
                col_bank = dat[0x0064];
                col_half = dat[0x0065];
                col_cell = dat[0x0066];
                unk1 = (ushort)((dat[0x0067] << 8) | dat[0x0068]);
                unk2 = dat[0x0069];
                unk3 = dat[0x006A];
            }

            public Bitmap RenderTile(int id, CGX cgx, COL col, bool allvisible = false, bool bgcolor = false)
            {
                //Get CGX Format
                int fmt = cgx.GetFormat();

                //Tile Size
                int t = 8 * (scr_mode + 1);

                Bitmap output = new Bitmap(t, t);

                //Panel Data
                int p_b = col_half;

                //Fill BG Color
                if (bgcolor)
                {
                    using (Graphics g = Graphics.FromImage(output))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                        g.FillRectangle(new SolidBrush(col.GetPalette()[(p_b * 128) + col_cell]), 0, 0, output.Width, output.Height);
                    }
                }

                //Map
                ushort dat = (ushort)(cell[(id * 2) + 1] | (cell[(id * 2)] << 8));
                int tile = dat & 0x3FF;
                int color = (dat & 0x1C00) >> 10;
                bool xflip = ((dat & 0x4000) != 0);
                bool yflip = ((dat & 0x8000) != 0);
                bool visible = allvisible ? true : clear[id];
                if (!visible) return output;

                Bitmap chr;
                switch (fmt)
                {
                    case 0: //2bit
                        chr = cgx.RenderTile(tile, t, col.GetPalette(fmt, (p_b * 128) + (color * 4)), xflip, yflip, bgcolor);
                        break;
                    case 1: //4bit
                        chr = cgx.RenderTile(tile, t, col.GetPalette(fmt, (p_b * 128) + (color * 16)), xflip, yflip, bgcolor);
                        break;
                    default:
                    case 2: //8bit
                        chr = cgx.RenderTile(tile, t, col.GetPalette(fmt, (p_b * 128) + (color * 128 * 0)), xflip, yflip, bgcolor);
                        break;
                }

                using (Graphics g = Graphics.FromImage(output))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    g.DrawImage(chr, 0, 0, t, t);
                }

                return output;
            }

            public Bitmap Render(CGX cgx, COL col, bool allvisible = false, bool bgcolor = false)
            {
                //Tile Size
                int t = 8 * (scr_mode + 1);

                Bitmap output = new Bitmap(256 * (t / 8), 4096 * (t / 8));

                //Panel Data
                for (int i = 0; i < 0x8000; i += 2)
                {
                    //X Pos
                    int x = (((i / 2) % 32) * t);
                    //Y Pos
                    int y = (((i / 2) / 32) * t);
                    //Scale
                    int z = t;

                    Bitmap chr = RenderTile(i / 2, cgx, col, allvisible, bgcolor);
                    using (Graphics g = Graphics.FromImage(output))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                        g.DrawImage(chr, x, y, z, z);
                    }
                }

                return output;
            }

            public static PNL Load(FileStream file)
            {
                file.Seek(0, SeekOrigin.Begin);

                //Check File Size
                if (file.Length != 0x10100)
                    return null;

                byte[] pnl_t = new byte[file.Length];
                file.Read(pnl_t, 0, (int)file.Length);

                //Check Footer Info
                string footer_string = System.Text.Encoding.ASCII.GetString(Utility.Subarray(pnl_t, 0x0000, 0x10));
                if (!footer_string.Equals("NAK1989 S-CG-CAD"))
                    return null;

                return new PNL(pnl_t);
            }
        }

        public class MAP
        {
            byte[] cell;    //Map Data
            Extra ext;
            byte map_pbank;

            public MAP(byte[] dat)
            {
                cell = Utility.Subarray(dat, 0x100, 0x2000);
                ext = new Extra(System.Text.Encoding.ASCII.GetString(Utility.Subarray(dat, 0x2000, 0x20)));
                map_pbank = dat[0x2070];
            }

            public Bitmap Render(PNL pnl, CGX cgx, COL col, bool allvisible = false, bool bgcolor = false)
            {
                //Get CGX Format
                int fmt = cgx.GetFormat();

                //Tile Size
                int t = 8;

                Bitmap output = new Bitmap(512 * (t / 8), 512 * (t / 8));

                //Fill BG Color
                int p_b = pnl.col_half;
                if (bgcolor)
                {
                    using (Graphics g = Graphics.FromImage(output))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                        g.FillRectangle(new SolidBrush(col.GetPalette()[(p_b * 128) + pnl.col_cell]), 0, 0, output.Width, output.Height);
                    }
                }

                //MAP Data
                for (int i = 0; i < 0x2000; i += 2)
                {
                    //X Pos
                    int x = (((i / 2) % 64) * t);
                    //Y Pos
                    int y = (((i / 2) / 64) * t);
                    //Scale
                    int z = t;

                    //Map
                    ushort dat = (ushort)(cell[i + 1] | (cell[i] << 8));
                    int tile = dat & 0x3FFF;
                    bool unk1 = ((dat & 0x4000) != 0);
                    bool unk2 = ((dat & 0x8000) != 0);
                    if (!unk2 && !allvisible) continue;

                    Bitmap chr = pnl.RenderTile(tile, cgx, col, allvisible, bgcolor);

                    using (Graphics g = Graphics.FromImage(output))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                        g.DrawImage(chr, x, y, z, z);
                    }
                }

                return output;
            }

            public static MAP Load(FileStream file)
            {
                file.Seek(0, SeekOrigin.Begin);

                //Check File Size
                if (file.Length != 0x2100)
                    return null;

                byte[] map_t = new byte[file.Length];
                file.Read(map_t, 0, (int)file.Length);

                //Check Footer Info
                string footer_string = System.Text.Encoding.ASCII.GetString(Utility.Subarray(map_t, 0x0000, 0x10));
                if (!footer_string.Equals("NAK1989 S-CG-CAD"))
                    return null;

                return new MAP(map_t);
            }
        }
    }
}
