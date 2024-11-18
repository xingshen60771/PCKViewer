using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

/*

此部分代码用于声明图像浏览插件FreeImage.dll的API，并保证本项目能顺利的将带有图像的字节集数组数据转为Bitmap数据，
综合官方文档、现有开源样例、AI解答结果编写。请不要随意修改！

*/


namespace PCKViewer
{
    internal class FreeImage
    {
        // 在FreeImage.dll中引入必须的API
        [DllImport("FreeImage.dll", EntryPoint = "FreeImage_OpenMemory")]
        public static extern FIMEMORY OpenMemory(IntPtr data, uint size_in_bytes);

        [DllImport("FreeImage.dll", EntryPoint = "FreeImage_LoadFromMemory")]
        public static extern FIBITMAP LoadFromMemory(FREE_IMAGE_FORMAT fif, FIMEMORY stream, FREE_IMAGE_LOAD_FLAGS flags);

        [DllImport("FreeImage.dll", EntryPoint = "FreeImage_GetImageType")]
        public static extern FREE_IMAGE_TYPE GetImageType(FIBITMAP dib);

        [DllImport("FreeImage.dll", EntryPoint = "FreeImage_GetBPP")]
        public static extern uint GetBPP(FIBITMAP dib);

        [DllImport("FreeImage.dll", EntryPoint = "FreeImage_GetWidth")]
        public static extern uint GetWidth(FIBITMAP dib);

        [DllImport("FreeImage.dll", EntryPoint = "FreeImage_GetHeight")]
        public static extern uint GetHeight(FIBITMAP dib);

        [DllImport("FreeImage.dll", EntryPoint = "FreeImage_GetPitch")]
        public static extern uint GetPitch(FIBITMAP dib);

        [DllImport("FreeImage.dll", EntryPoint = "FreeImage_ConvertToRawBits")]
        public static extern void ConvertToRawBits(IntPtr bits, FIBITMAP dib, int pitch, uint bpp, uint red_mask, uint green_mask, uint blue_mask, bool topdown);

        [DllImport("FreeImage.dll", EntryPoint = "FreeImage_GetRedMask")]
        public static extern uint GetRedMask(FIBITMAP dib);

        [DllImport("FreeImage.dll", EntryPoint = "FreeImage_GetGreenMask")]
        public static extern uint GetGreenMask(FIBITMAP dib);

        [DllImport("FreeImage.dll", EntryPoint = "FreeImage_GetBlueMask")]
        public static extern uint GetBlueMask(FIBITMAP dib);

        [DllImport("FreeImage.dll", EntryPoint = "FreeImage_GetPalette")]
        public static extern IntPtr GetPalette(FIBITMAP dib);

        [DllImport("FreeImage.dll", EntryPoint = "FreeImage_IsTransparent")]
        public static extern bool IsTransparent(FIBITMAP dib);

        [DllImport("FreeImage.dll", EntryPoint = "FreeImage_FindFirstMetadata")]
        public static extern FIMETADATA FindFirstMetadata(FREE_IMAGE_MDMODEL model, FIBITMAP dib, out FITAG tag);

        [DllImport("FreeImage.dll", EntryPoint = "FreeImage_GetTagLength")]
        public static extern uint GetTagLength(FITAG tag);

        [DllImport("FreeImage.dll", EntryPoint = "FreeImage_GetTagID")]
        public static extern ushort GetTagID(FITAG tag);

        [DllImport("FreeImage.dll", EntryPoint = "FreeImage_GetTagType")]
        public static extern FREE_IMAGE_MDTYPE GetTagType(FITAG tag);

        [DllImport("FreeImage.dll", EntryPoint = "FreeImage_GetTagValue")]
        public static extern IntPtr GetTagValue(FITAG tag);

        [DllImport("FreeImage.dll", EntryPoint = "FreeImage_FindNextMetadata")]
        public static extern bool FindNextMetadata(FIMETADATA mdhandle, out FITAG tag);

        [DllImport("FreeImage.dll", EntryPoint = "FreeImage_FindCloseMetadata")]
        private static extern void FindCloseMetadata_(FIMETADATA mdhandle);

        [DllImport("FreeImage.dll", EntryPoint = "FreeImage_GetDotsPerMeterX")]
        public static extern uint GetDotsPerMeterX(FIBITMAP dib);

        [DllImport("FreeImage.dll", EntryPoint = "FreeImage_GetDotsPerMeterY")]
        public static extern uint GetDotsPerMeterY(FIBITMAP dib);

        [DllImport("FreeImage.dll", EntryPoint = "FreeImage_GetTransparencyCount")]
        public static extern uint GetTransparencyCount(FIBITMAP dib);

        [DllImport("FreeImage.dll", EntryPoint = "FreeImage_GetTransparencyTable")]
        public static extern IntPtr GetTransparencyTable(FIBITMAP dib);

        [DllImport("FreeImage.dll", EntryPoint = "FreeImage_SetDotsPerMeterX")]
        public static extern void SetDotsPerMeterX(FIBITMAP dib, uint res);

        [DllImport("FreeImage.dll", EntryPoint = "FreeImage_SetDotsPerMeterY")]
        public static extern void SetDotsPerMeterY(FIBITMAP dib, uint res);

        [DllImport("FreeImage.dll", EntryPoint = "FreeImage_GetTagCount")]
        public static extern uint GetTagCount(FITAG tag);

        [DllImport("FreeImage.dll", EntryPoint = "FreeImage_GetColorsUsed")]
        public static extern uint GetColorsUsed(FIBITMAP dib);

        [DllImport("ntdll.dll")]
        private unsafe static extern uint RtlCompareMemory(void* buf1, void* buf2, uint count);

        public static readonly FREE_IMAGE_MDMODEL[] FREE_IMAGE_MDMODELS = (FREE_IMAGE_MDMODEL[])Enum.GetValues(typeof(FREE_IMAGE_MDMODEL));
        private static Dictionary<FIMETADATA, FREE_IMAGE_MDMODEL> metaDataSearchHandler = new Dictionary<FIMETADATA, FREE_IMAGE_MDMODEL>(1);


        // 定义图像格式、类型
        public enum FREE_IMAGE_FORMAT
        {
            FIF_UNKNOWN = -1,
            FIF_BMP = 0,
            FIF_ICO = 1,
            FIF_JPEG = 2,
            FIF_JNG = 3,
            FIF_KOALA = 4,
            FIF_LBM = 5,
            FIF_IFF = 5,
            FIF_MNG = 6,
            FIF_PBM = 7,
            FIF_PBMRAW = 8,
            FIF_PCD = 9,
            FIF_PCX = 10,
            FIF_PGM = 11,
            FIF_PGMRAW = 12,
            FIF_PNG = 13,
            FIF_PPM = 14,
            FIF_PPMRAW = 15,
            FIF_RAS = 16,
            FIF_TARGA = 17,
            FIF_TIFF = 18,
            FIF_WBMP = 19,
            FIF_PSD = 20,
            FIF_CUT = 21,
            FIF_XBM = 22,
            FIF_XPM = 23,
            FIF_DDS = 24,
            FIF_GIF = 25,
            FIF_HDR = 26,
            FIF_FAXG3 = 27,
            FIF_SGI = 28,
            FIF_EXR = 29,
            FIF_J2K = 30,
            FIF_JP2 = 31,
            FIF_PFM = 32,
            FIF_PICT = 33,
            FIF_RAW = 34

        }

        public enum FREE_IMAGE_TYPE
        {
            FIT_UNKNOWN,
            FIT_BITMAP,
            FIT_UINT16,
            FIT_INT16,
            FIT_UINT32,
            FIT_INT32,
            FIT_FLOAT,
            FIT_DOUBLE,
            FIT_COMPLEX,
            FIT_RGB16,
            FIT_RGBA16,
            FIT_RGBF,
            FIT_RGBAF
        }

        public enum FREE_IMAGE_MDTYPE
        {
            FIDT_NOTYPE,
            FIDT_BYTE,
            FIDT_ASCII,
            FIDT_SHORT,
            FIDT_LONG,
            FIDT_RATIONAL,
            FIDT_SBYTE,
            FIDT_UNDEFINED,
            FIDT_SSHORT,
            FIDT_SLONG,
            FIDT_SRATIONAL,
            FIDT_FLOAT,
            FIDT_DOUBLE,
            FIDT_IFD,
            FIDT_PALETTE
        }

        public enum FREE_IMAGE_MDMODEL
        {
            FIMD_NODATA = -1,
            FIMD_COMMENTS,
            FIMD_EXIF_MAIN,
            FIMD_EXIF_EXIF,
            FIMD_EXIF_GPS,
            FIMD_EXIF_MAKERNOTE,
            FIMD_EXIF_INTEROP,
            FIMD_IPTC,
            FIMD_XMP,
            FIMD_GEOTIFF,
            FIMD_ANIMATION,
            FIMD_CUSTOM
        }

        // 各种句柄
        [Serializable]
        [DebuggerDisplay("{value}")]
        public struct FI1BIT
        {
            public const byte MaxValue = 1;

            public const byte MinValue = 0;

            private byte value;

            private FI1BIT(byte value)
            {
                this.value = (byte)(value & 1u);
            }

            public static implicit operator byte(FI1BIT value)
            {
                return value.value;
            }

            public static implicit operator FI1BIT(byte value)
            {
                return new FI1BIT(value);
            }

            public override string ToString()
            {
                return value.ToString();
            }
        }

        [DebuggerDisplay("{value}")]
        public struct FI4BIT
        {
            public const byte MaxValue = 15;

            public const byte MinValue = 0;

            private byte value;

            private FI4BIT(byte value)
            {
                this.value = (byte)(value & 0xFu);
            }

            public static implicit operator byte(FI4BIT value)
            {
                return value.value;
            }

            public static implicit operator FI4BIT(byte value)
            {
                return new FI4BIT(value);
            }

            public override string ToString()
            {
                return value.ToString();
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct RGBQUAD : IComparable, IComparable<RGBQUAD>, IEquatable<RGBQUAD>
        {
            [FieldOffset(0)]
            public byte rgbBlue;

            [FieldOffset(1)]
            public byte rgbGreen;

            [FieldOffset(2)]
            public byte rgbRed;

            [FieldOffset(3)]
            public byte rgbReserved;

            [FieldOffset(0)]
            public uint uintValue;

            public Color Color
            {
                get
                {
                    return Color.FromArgb(rgbReserved, rgbRed, rgbGreen, rgbBlue);
                }
                set
                {
                    rgbRed = value.R;
                    rgbGreen = value.G;
                    rgbBlue = value.B;
                    rgbReserved = value.A;
                }
            }

            public RGBQUAD(Color color)
            {
                uintValue = 0u;
                rgbBlue = color.B;
                rgbGreen = color.G;
                rgbRed = color.R;
                rgbReserved = color.A;
            }

            public static bool operator ==(RGBQUAD left, RGBQUAD right)
            {
                return left.uintValue == right.uintValue;
            }

            public static bool operator !=(RGBQUAD left, RGBQUAD right)
            {
                return left.uintValue != right.uintValue;
            }

            public static implicit operator RGBQUAD(Color value)
            {
                return new RGBQUAD(value);
            }

            public static implicit operator Color(RGBQUAD value)
            {
                return value.Color;
            }

            public static implicit operator RGBQUAD(uint value)
            {
                RGBQUAD result = default(RGBQUAD);
                result.uintValue = value;
                return result;
            }

            public static implicit operator uint(RGBQUAD value)
            {
                return value.uintValue;
            }

            public static RGBQUAD[] ToRGBQUAD(Color[] array)
            {
                if (array == null)
                {
                    return null;
                }

                RGBQUAD[] array2 = new RGBQUAD[array.Length];
                for (int i = 0; i < array.Length; i++)
                {
                    array2[i] = array[i];
                }

                return array2;
            }

            public static Color[] ToColor(RGBQUAD[] array)
            {
                if (array == null)
                {
                    return null;
                }

                Color[] array2 = new Color[array.Length];
                for (int i = 0; i < array.Length; i++)
                {
                    array2[i] = array[i].Color;
                }

                return array2;
            }

            public int CompareTo(object obj)
            {
                if (obj == null)
                {
                    return 1;
                }

                if (!(obj is RGBQUAD))
                {
                    throw new ArgumentException("obj");
                }

                return CompareTo((RGBQUAD)obj);
            }

            public int CompareTo(RGBQUAD other)
            {
                return Color.ToArgb().CompareTo(other.Color.ToArgb());
            }

            public override bool Equals(object obj)
            {
                if (obj is RGBQUAD)
                {
                    return this == (RGBQUAD)obj;
                }

                return false;
            }

            public bool Equals(RGBQUAD other)
            {
                return this == other;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public override string ToString()
            {
                return ColorToString(Color);
            }
        }


        internal static string ColorToString(Color color)
        {
            return string.Format(CultureInfo.CurrentCulture, "{{Name={0}, ARGB=({1}, {2}, {3}, {4})}}", color.Name, color.A, color.R, color.G, color.B);
        }

        private unsafe static uint PlatformCompareMemory(void* buf1, void* buf2, uint count)
        {
            return RtlCompareMemory(buf1, buf2, count);
        }

        public struct FIBITMAP : IComparable, IComparable<FIBITMAP>, IEquatable<FIBITMAP>
        {
            private IntPtr data;

            public static readonly FIBITMAP Zero;

            public bool IsNull => data == IntPtr.Zero;

            public static bool operator ==(FIBITMAP left, FIBITMAP right)
            {
                return left.data == right.data;
            }

            public static bool operator !=(FIBITMAP left, FIBITMAP right)
            {
                return left.data != right.data;
            }

            public void SetNull()
            {
                data = IntPtr.Zero;
            }

            public override string ToString()
            {
                return data.ToString();
            }

            public override int GetHashCode()
            {
                return data.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                if (obj is FIBITMAP)
                {
                    return this == (FIBITMAP)obj;
                }

                return false;
            }

            public bool Equals(FIBITMAP other)
            {
                return this == other;
            }

            public int CompareTo(object obj)
            {
                if (obj == null)
                {
                    return 1;
                }

                if (!(obj is FIBITMAP))
                {
                    throw new ArgumentException("obj");
                }

                return CompareTo((FIBITMAP)obj);
            }

            public int CompareTo(FIBITMAP other)
            {
                return data.ToInt64().CompareTo(other.data.ToInt64());
            }
        }

        public struct FIMEMORY : IComparable, IComparable<FIMEMORY>, IEquatable<FIMEMORY>
        {
            private IntPtr data;

            public static readonly FIMEMORY Zero;

            public bool IsNull => data == IntPtr.Zero;

            public static bool operator ==(FIMEMORY left, FIMEMORY right)
            {
                return left.data == right.data;
            }

            public static bool operator !=(FIMEMORY left, FIMEMORY right)
            {
                return left.data != right.data;
            }

            public void SetNull()
            {
                data = IntPtr.Zero;
            }

            public override string ToString()
            {
                return data.ToString();
            }

            public override int GetHashCode()
            {
                return data.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                if (obj is FIMEMORY)
                {
                    return this == (FIMEMORY)obj;
                }

                return false;
            }

            public bool Equals(FIMEMORY other)
            {
                return this == other;
            }

            public int CompareTo(object obj)
            {
                if (obj == null)
                {
                    return 1;
                }

                if (!(obj is FIMEMORY))
                {
                    throw new ArgumentException("obj");
                }

                return CompareTo((FIMEMORY)obj);
            }

            public int CompareTo(FIMEMORY other)
            {
                return data.ToInt64().CompareTo(other.data.ToInt64());
            }
        }

        public struct FIMETADATA : IComparable, IComparable<FIMETADATA>, IEquatable<FIMETADATA>
        {
            private IntPtr data;

            public static readonly FIMETADATA Zero;

            public bool IsNull => data == IntPtr.Zero;

            public static bool operator ==(FIMETADATA left, FIMETADATA right)
            {
                return left.data == right.data;
            }

            public static bool operator !=(FIMETADATA left, FIMETADATA right)
            {
                return left.data != right.data;
            }

            public void SetNull()
            {
                data = IntPtr.Zero;
            }

            public override string ToString()
            {
                return data.ToString();
            }

            public override int GetHashCode()
            {
                return data.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                if (obj is FIMETADATA)
                {
                    return this == (FIMETADATA)obj;
                }

                return false;
            }

            public bool Equals(FIMETADATA other)
            {
                return this == other;
            }

            public int CompareTo(object obj)
            {
                if (obj == null)
                {
                    return 1;
                }

                if (!(obj is FIMETADATA))
                {
                    throw new ArgumentException("obj");
                }

                return CompareTo((FIMETADATA)obj);
            }

            public int CompareTo(FIMETADATA other)
            {
                return data.ToInt64().CompareTo(other.data.ToInt64());
            }
        }

        public struct FITAG : IComparable, IComparable<FITAG>, IEquatable<FITAG>
        {
            private IntPtr data;

            public static readonly FITAG Zero;

            public bool IsNull => data == IntPtr.Zero;

            public static bool operator ==(FITAG left, FITAG right)
            {
                return left.data == right.data;
            }

            public static bool operator !=(FITAG left, FITAG right)
            {
                return left.data != right.data;
            }

            public void SetNull()
            {
                data = IntPtr.Zero;
            }

            public override string ToString()
            {
                return data.ToString();
            }

            public override int GetHashCode()
            {
                return data.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                if (obj is FITAG)
                {
                    return this == (FITAG)obj;
                }

                return false;
            }

            public bool Equals(FITAG other)
            {
                return this == other;
            }

            public int CompareTo(object obj)
            {
                if (obj == null)
                {
                    return 1;
                }

                if (!(obj is FITAG))
                {
                    throw new ArgumentException("obj");
                }

                return CompareTo((FITAG)obj);
            }

            public int CompareTo(FITAG other)
            {
                return data.ToInt64().CompareTo(other.data.ToInt64());
            }
        }

        [Flags]
        public enum FREE_IMAGE_LOAD_FLAGS
        {
            DEFAULT = 0,
            GIF_LOAD256 = 1,
            GIF_PLAYBACK = 2,
            ICO_MAKEALPHA = 1,
            JPEG_FAST = 1,
            JPEG_ACCURATE = 2,
            JPEG_CMYK = 4,
            JPEG_EXIFROTATE = 8,
            PCD_BASE = 1,
            PCD_BASEDIV4 = 2,
            PCD_BASEDIV16 = 3,
            PNG_IGNOREGAMMA = 1,
            TARGA_LOAD_RGB888 = 1,
            TIFF_CMYK = 1,
            RAW_PREVIEW = 1,
            RAW_DISPLAY = 2
        }


        /// <summary>
        /// &lt;Bitmap&gt; 到位图
        /// <param name="dib">(FIBITMAP 欲转换的位图)</param>
        /// <returns><para>(成功返回Bitmap数据，失败则抛出异常)</para></returns>
        /// </summary>
        // 程序主体部分
        public static Bitmap GetBitmap(FIBITMAP dib)
        {
            return GetBitmap(dib, copyMetadata: true);
        }

        internal unsafe static Bitmap GetBitmap(FIBITMAP dib, bool copyMetadata)
        {
            if (dib.IsNull)
            {
                throw new ArgumentNullException("dib");
            }

            if (GetImageType(dib) != FREE_IMAGE_TYPE.FIT_BITMAP)
            {
                throw new ArgumentException("Only bitmaps with type of FIT_BITMAP can be converted.");
            }

            PixelFormat pixelFormat = GetPixelFormat(dib);
            if (pixelFormat == PixelFormat.Undefined && GetBPP(dib) == 16)
            {
                throw new ArgumentException("Only 16bit 555 and 565 are supported.");
            }

            int height = (int)GetHeight(dib);
            int width = (int)GetWidth(dib);
            int pitch = (int)GetPitch(dib);
            Bitmap bitmap = new Bitmap(width, height, pixelFormat);
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, pixelFormat);
            ConvertToRawBits(bitmapData.Scan0, dib, pitch, GetBPP(dib), GetRedMask(dib), GetGreenMask(dib), GetBlueMask(dib), topdown: true);
            bitmap.UnlockBits(bitmapData);
            if (GetResolutionX(dib) != 0 && GetResolutionY(dib) != 0)
            {
                bitmap.SetResolution(GetResolutionX(dib), GetResolutionY(dib));
            }

            if (GetPalette(dib) != IntPtr.Zero)
            {
                ColorPalette palette = bitmap.Palette;
                Color[] colorData = new Palette(dib).ColorData;
                int num = Math.Min(colorData.Length, palette.Entries.Length);
                if (IsTransparent(dib))
                {
                    byte[] transparencyTableEx = GetTransparencyTableEx(dib);
                    int i = 0;
                    for (int num2 = Math.Min(num, transparencyTableEx.Length); i < num2; i++)
                    {
                        palette.Entries[i] = Color.FromArgb(transparencyTableEx[i], colorData[i]);
                    }

                    for (; i < num; i++)
                    {
                        palette.Entries[i] = Color.FromArgb(255, colorData[i]);
                    }
                }
                else
                {
                    for (int j = 0; j < num; j++)
                    {
                        palette.Entries[j] = colorData[j];
                    }
                }

                bitmap.Palette = palette;
            }

            if (copyMetadata)
            {
                try
                {
                    List<PropertyItem> list = new List<PropertyItem>();
                    FREE_IMAGE_MDMODEL[] fREE_IMAGE_MDMODELS = FREE_IMAGE_MDMODELS;
                    for (int k = 0; k < fREE_IMAGE_MDMODELS.Length; k++)
                    {
                        FITAG tag;
                        FIMETADATA mdhandle = FindFirstMetadata(fREE_IMAGE_MDMODELS[k], dib, out tag);
                        if (mdhandle.IsNull)
                        {
                            continue;
                        }

                        do
                        {
                            PropertyItem propertyItem = CreatePropertyItem();
                            propertyItem.Len = (int)GetTagLength(tag);
                            propertyItem.Id = GetTagID(tag);
                            propertyItem.Type = (short)GetTagType(tag);
                            byte[] array = new byte[propertyItem.Len];
                            byte* src = (byte*)(void*)GetTagValue(tag);
                            fixed (byte* dest = array)
                            {
                                CopyMemory(dest, src, (uint)propertyItem.Len);
                            }

                            propertyItem.Value = array;
                            list.Add(propertyItem);
                        }
                        while (FindNextMetadata(mdhandle, out tag));
                        FindCloseMetadata(mdhandle);
                    }

                    foreach (PropertyItem item in list)
                    {
                        bitmap.SetPropertyItem(item);
                    }
                }
                catch
                {
                }
            }

            return bitmap;
        }

        public static PixelFormat GetPixelFormat(FIBITMAP dib)
        {
            if (dib.IsNull)
            {
                throw new ArgumentNullException("dib");
            }

            PixelFormat result = PixelFormat.Undefined;
            if (GetImageType(dib) == FREE_IMAGE_TYPE.FIT_BITMAP)
            {
                switch (GetBPP(dib))
                {
                    case 1u:
                        result = PixelFormat.Format1bppIndexed;
                        break;
                    case 4u:
                        result = PixelFormat.Format4bppIndexed;
                        break;
                    case 8u:
                        result = PixelFormat.Format8bppIndexed;
                        break;
                    case 16u:
                        if (GetBlueMask(dib) == 31 && GetGreenMask(dib) == 2016 && GetRedMask(dib) == 63488)
                        {
                            result = PixelFormat.Format16bppRgb565;
                        }

                        if (GetBlueMask(dib) == 31 && GetGreenMask(dib) == 992 && GetRedMask(dib) == 31744)
                        {
                            result = PixelFormat.Format16bppRgb555;
                        }

                        break;
                    case 24u:
                        result = PixelFormat.Format24bppRgb;
                        break;
                    case 32u:
                        result = PixelFormat.Format32bppArgb;
                        break;
                }
            }

            return result;
        }


        public static uint GetResolutionX(FIBITMAP dib)
        {
            if (dib.IsNull)
            {
                throw new ArgumentNullException("dib");
            }

            return (uint)(0.5 + 0.0254 * (double)GetDotsPerMeterX(dib));
        }

        public static uint GetResolutionY(FIBITMAP dib)
        {
            if (dib.IsNull)
            {
                throw new ArgumentNullException("dib");
            }

            return (uint)(0.5 + 0.0254 * (double)GetDotsPerMeterY(dib));
        }

        public static void SetResolutionX(FIBITMAP dib, uint res)
        {
            if (dib.IsNull)
            {
                throw new ArgumentNullException("dib");
            }

            SetDotsPerMeterX(dib, (uint)((double)res / 0.0254 + 0.5));
        }

        public static void SetResolutionY(FIBITMAP dib, uint res)
        {
            if (dib.IsNull)
            {
                throw new ArgumentNullException("dib");
            }

            SetDotsPerMeterY(dib, (uint)((double)res / 0.0254 + 0.5));
        }

        public unsafe static byte[] GetTransparencyTableEx(FIBITMAP dib)
        {
            if (dib.IsNull)
            {
                throw new ArgumentNullException("dib");
            }

            uint transparencyCount = GetTransparencyCount(dib);
            byte[] array = new byte[transparencyCount];
            byte* src = (byte*)(void*)GetTransparencyTable(dib);
            fixed (byte* dest = array)
            {
                CopyMemory(dest, src, transparencyCount);
            }

            return array;
        }

        internal static PropertyItem CreatePropertyItem()
        {
            return (PropertyItem)Activator.CreateInstance(typeof(PropertyItem), nonPublic: true);
        }



        public unsafe static void CopyMemory(byte* dest, byte* src, int len)
        {
            if (len >= 16)
            {
                do
                {
                    *(int*)dest = *(int*)src;
                    *(int*)(dest + 4) = *(int*)(src + 4);
                    *(int*)(dest + 8) = *(int*)(src + 8);
                    *(int*)(dest + 12) = *(int*)(src + 12);
                    dest += 16;
                    src += 16;
                }
                while ((len -= 16) >= 16);
            }

            if (len > 0)
            {
                if (((uint)len & 8u) != 0)
                {
                    *(int*)dest = *(int*)src;
                    *(int*)(dest + 4) = *(int*)(src + 4);
                    dest += 8;
                    src += 8;
                }

                if (((uint)len & 4u) != 0)
                {
                    *(int*)dest = *(int*)src;
                    dest += 4;
                    src += 4;
                }

                if (((uint)len & 2u) != 0)
                {
                    *(short*)dest = *(short*)src;
                    dest += 2;
                    src += 2;
                }

                if (((uint)len & (true ? 1u : 0u)) != 0)
                {
                    *dest = *src;
                }
            }
        }

        public unsafe static void CopyMemory(byte* dest, byte* src, long len)
        {
            CopyMemory(dest, src, checked((int)len));
        }

        public unsafe static void CopyMemory(void* dest, void* src, long len)
        {
            CopyMemory((byte*)dest, (byte*)src, checked((int)len));
        }

        public unsafe static void CopyMemory(void* dest, void* src, int len)
        {
            CopyMemory((byte*)dest, (byte*)src, len);
        }

        public unsafe static void CopyMemory(IntPtr dest, IntPtr src, int len)
        {
            CopyMemory((byte*)(void*)dest, (byte*)(void*)src, len);
        }

        public unsafe static void CopyMemory(IntPtr dest, IntPtr src, long len)
        {
            CopyMemory((byte*)(void*)dest, (byte*)(void*)src, checked((int)len));
        }

        public unsafe static void CopyMemory(Array dest, void* src, int len)
        {
            GCHandle gCHandle = GCHandle.Alloc(dest, GCHandleType.Pinned);
            try
            {
                CopyMemory((byte*)(void*)gCHandle.AddrOfPinnedObject(), (byte*)src, len);
            }
            finally
            {
                gCHandle.Free();
            }
        }

        public unsafe static void CopyMemory(Array dest, void* src, long len)
        {
            CopyMemory(dest, src, checked((int)len));
        }

        public unsafe static void CopyMemory(Array dest, IntPtr src, int len)
        {
            CopyMemory(dest, (void*)src, len);
        }

        public unsafe static void CopyMemory(Array dest, IntPtr src, long len)
        {
            CopyMemory(dest, (void*)src, checked((int)len));
        }

        public unsafe static void CopyMemory(void* dest, Array src, int len)
        {
            GCHandle gCHandle = GCHandle.Alloc(src, GCHandleType.Pinned);
            try
            {
                CopyMemory((byte*)dest, (byte*)(void*)gCHandle.AddrOfPinnedObject(), len);
            }
            finally
            {
                gCHandle.Free();
            }
        }

        public unsafe static void CopyMemory(void* dest, Array src, long len)
        {
            CopyMemory(dest, src, checked((int)len));
        }

        public unsafe static void CopyMemory(IntPtr dest, Array src, int len)
        {
            CopyMemory((void*)dest, src, len);
        }

        public unsafe static void CopyMemory(IntPtr dest, Array src, long len)
        {
            CopyMemory((void*)dest, src, checked((int)len));
        }

        public unsafe static void CopyMemory(Array dest, Array src, int len)
        {
            GCHandle gCHandle = GCHandle.Alloc(dest, GCHandleType.Pinned);
            try
            {
                GCHandle gCHandle2 = GCHandle.Alloc(src, GCHandleType.Pinned);
                try
                {
                    CopyMemory((byte*)(void*)gCHandle.AddrOfPinnedObject(), (byte*)(void*)gCHandle2.AddrOfPinnedObject(), len);
                }
                finally
                {
                    gCHandle2.Free();
                }
            }
            finally
            {
                gCHandle.Free();
            }
        }

        public static void CopyMemory(Array dest, Array src, long len)
        {
            CopyMemory(dest, src, checked((int)len));
        }

        public unsafe static bool CompareMemory(void* buf1, void* buf2, uint length)
        {
            return length == PlatformCompareMemory(buf1, buf2, length);
        }

        public unsafe static bool CompareMemory(void* buf1, void* buf2, long length)
        {
            return length == PlatformCompareMemory(buf1, buf2, checked((uint)length));
        }

        public unsafe static bool CompareMemory(IntPtr buf1, IntPtr buf2, uint length)
        {
            return length == PlatformCompareMemory(buf1.ToPointer(), buf2.ToPointer(), length);
        }

        public unsafe static bool CompareMemory(IntPtr buf1, IntPtr buf2, long length)
        {
            return length == PlatformCompareMemory(buf1.ToPointer(), buf2.ToPointer(), checked((uint)length));
        }



        public static void FindCloseMetadata(FIMETADATA mdhandle)
        {
            if (metaDataSearchHandler.ContainsKey(mdhandle))
            {
                metaDataSearchHandler.Remove(mdhandle);
            }

            FindCloseMetadata_(mdhandle);
        }


        public sealed class Palette : MemoryArray<RGBQUAD>
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private GCHandle paletteHandle;

            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private RGBQUAD[] array;

            public RGBQUAD[] AsArray
            {
                get
                {
                    return base.Data;
                }
                set
                {
                    base.Data = value;
                }
            }

            internal unsafe Color[] ColorData
            {
                get
                {
                    EnsureNotDisposed();
                    Color[] array = new Color[length];
                    for (int i = 0; i < length; i++)
                    {
                        array[i] = Color.FromArgb(*(int*)(baseAddress + (nint)i * (nint)4) | -16777216);
                    }

                    return array;
                }
            }

            public Palette(FIBITMAP dib)
                : base(GetPalette(dib), (int)GetColorsUsed(dib))
            {
                if (dib.IsNull)
                {
                    throw new ArgumentNullException("dib");
                }

                if (GetImageType(dib) != FREE_IMAGE_TYPE.FIT_BITMAP)
                {
                    throw new ArgumentException("dib");
                }

                if (GetBPP(dib) > 8)
                {
                    throw new ArgumentException("dib");
                }
            }

            public Palette(FITAG tag)
                : base(GetTagValue(tag), (int)GetTagCount(tag))
            {
                if (GetTagType(tag) != FREE_IMAGE_MDTYPE.FIDT_PALETTE)
                {
                    throw new ArgumentException("tag");
                }
            }


            public unsafe Palette(RGBQUAD[] palette)
            {
                array = (RGBQUAD[])palette.Clone();
                paletteHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
                baseAddress = (byte*)(void*)paletteHandle.AddrOfPinnedObject();
                length = array.Length;
                buffer = new RGBQUAD[1];
                handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                ptr = (byte*)(void*)handle.AddrOfPinnedObject();
            }

            public Palette(Color[] palette)
                : this(RGBQUAD.ToRGBQUAD(palette))
            {
            }

            public Palette(int size)
                : this(new RGBQUAD[size])
            {
            }

            public RGBQUAD[] ToArray()
            {
                return base.Data;
            }

            public void Colorize(Color color)
            {
                Colorize(color, 0.5);
            }

            public void Colorize(Color color, double splitSize)
            {
                Colorize(color, (int)((double)length * splitSize));
            }

            public void Colorize(Color color, int splitSize)
            {
                EnsureNotDisposed();
                if (splitSize < 1 || splitSize >= length)
                {
                    throw new ArgumentOutOfRangeException("splitSize");
                }

                RGBQUAD[] array = new RGBQUAD[length];
                double num = (int)color.R;
                double num2 = (int)color.G;
                double num3 = (int)color.B;
                int i = 0;
                double num4 = num / (double)splitSize;
                double num5 = num2 / (double)splitSize;
                double num6 = num3 / (double)splitSize;
                for (; i <= splitSize; i++)
                {
                    array[i].rgbRed = (byte)((double)i * num4);
                    array[i].rgbGreen = (byte)((double)i * num5);
                    array[i].rgbBlue = (byte)((double)i * num6);
                }

                num4 = (255.0 - num) / (double)(length - splitSize);
                num5 = (255.0 - num2) / (double)(length - splitSize);
                num6 = (255.0 - num3) / (double)(length - splitSize);
                for (; i < length; i++)
                {
                    array[i].rgbRed = (byte)(num + (double)(i - splitSize) * num4);
                    array[i].rgbGreen = (byte)(num2 + (double)(i - splitSize) * num5);
                    array[i].rgbBlue = (byte)(num3 + (double)(i - splitSize) * num6);
                }

                base.Data = array;
            }

            public void CreateGrayscalePalette()
            {
                Colorize(Color.White, length - 1);
            }

            public void CreateGrayscalePalette(bool inverse)
            {
                Colorize(Color.White, (!inverse) ? (length - 1) : 0);
            }

            public void CreateGrayscalePalette(Color color, bool inverse)
            {
                Colorize(color, (!inverse) ? (length - 1) : 0);
            }

            public void Reverse()
            {
                EnsureNotDisposed();
                if (array != null)
                {
                    Array.Reverse(array);
                    return;
                }

                RGBQUAD[] data = base.Data;
                Array.Reverse(data);
                base.Data = data;
            }

            public void CopyFrom(Palette palette)
            {
                EnsureNotDisposed();
                if (palette == null)
                {
                    throw new ArgumentNullException("palette");
                }

                CopyFrom(palette.Data, 0, 0, Math.Min(palette.Length, base.Length));
            }

            public void CopyFrom(Palette palette, int offset)
            {
                EnsureNotDisposed();
                CopyFrom(palette.Data, 0, offset, Math.Min(palette.Length, base.Length - offset));
            }

            public void Save(string filename)
            {
                using Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write);
                Save(stream);
            }

            public void Save(Stream stream)
            {
                Save(new BinaryWriter(stream));
            }

            public void Save(BinaryWriter writer)
            {
                EnsureNotDisposed();
                writer.Write(ToByteArray());
            }

            public void Load(string filename)
            {
                using Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                Load(stream);
            }

            public void Load(Stream stream)
            {
                Load(new BinaryReader(stream));
            }

            public unsafe void Load(BinaryReader reader)
            {
                EnsureNotDisposed();
                int count = length * sizeof(RGBQUAD);
                byte[] array = reader.ReadBytes(count);
                fixed (byte* src = array)
                {
                    MemoryArray<RGBQUAD>.CopyMemory(baseAddress, src, array.Length);
                }
            }

            protected override void Dispose(bool disposing)
            {
                if (paletteHandle.IsAllocated)
                {
                    paletteHandle.Free();
                }

                array = null;
                base.Dispose(disposing);
            }
        }


        public class MemoryArray<T> : IDisposable, ICloneable, ICollection, IEnumerable, IEnumerable<T>, IEquatable<MemoryArray<T>> where T : struct
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            protected unsafe byte* baseAddress;

            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            protected int length;

            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private static readonly int size;

            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            protected T[] buffer;

            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            protected unsafe byte* ptr;

            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            protected GCHandle handle;

            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            protected readonly bool isOneBit;

            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            protected readonly bool isFourBit;

            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            protected object syncRoot;

            public T this[int index]
            {
                get
                {
                    return GetValue(index);
                }
                set
                {
                    SetValue(value, index);
                }
            }

            public T[] Data
            {
                get
                {
                    return GetValues(0, length);
                }
                set
                {
                    if (value == null)
                    {
                        throw new ArgumentNullException("value");
                    }

                    if (value.Length != length)
                    {
                        throw new ArgumentOutOfRangeException("value.Lengt");
                    }

                    SetValues(value, 0);
                }
            }

            public int Length
            {
                get
                {
                    EnsureNotDisposed();
                    return length;
                }
            }

            public unsafe IntPtr BaseAddress
            {
                get
                {
                    EnsureNotDisposed();
                    return new IntPtr(baseAddress);
                }
            }

            public int Count
            {
                get
                {
                    EnsureNotDisposed();
                    return length;
                }
            }

            public bool IsSynchronized
            {
                get
                {
                    EnsureNotDisposed();
                    return false;
                }
            }

            public object SyncRoot
            {
                get
                {
                    EnsureNotDisposed();
                    if (syncRoot == null)
                    {
                        Interlocked.CompareExchange(ref syncRoot, new object(), null);
                    }

                    return syncRoot;
                }
            }

            static MemoryArray()
            {
                T[] arr = new T[2];
                long num = Marshal.SizeOf(typeof(T));
                long num2 = Marshal.UnsafeAddrOfPinnedArrayElement(arr, 1).ToInt64() - Marshal.UnsafeAddrOfPinnedArrayElement(arr, 0).ToInt64();
                if (num != num2)
                {
                    throw new NotSupportedException("The desired type can not be handled, because its managed and unmanaged size in bytes are different.");
                }

                size = (int)num;
            }

            protected MemoryArray()
            {
            }

            public unsafe MemoryArray(IntPtr baseAddress, int length)
                : this(baseAddress.ToPointer(), length)
            {
            }

            public unsafe MemoryArray(void* baseAddress, int length)
            {
                if (typeof(T) == typeof(FI1BIT))
                {
                    isOneBit = true;
                }
                else if (typeof(T) == typeof(FI4BIT))
                {
                    isFourBit = true;
                }

                if (baseAddress == null)
                {
                    throw new ArgumentNullException("baseAddress");
                }

                if (length < 1)
                {
                    throw new ArgumentOutOfRangeException("length");
                }

                this.baseAddress = (byte*)baseAddress;
                this.length = length;
                if (!isOneBit && !isFourBit)
                {
                    buffer = new T[1];
                    handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    ptr = (byte*)(void*)handle.AddrOfPinnedObject();
                }
            }

            ~MemoryArray()
            {
                Dispose(disposing: false);
            }

            public unsafe static bool operator ==(MemoryArray<T> left, MemoryArray<T> right)
            {
                if ((object)left == right)
                {
                    return true;
                }

                if ((object)right == null || (object)left == null || left.length != right.length)
                {
                    return false;
                }

                if (left.baseAddress == right.baseAddress)
                {
                    return true;
                }

                return CompareMemory(left.baseAddress, right.baseAddress, (uint)left.length);
            }

            public static bool operator !=(MemoryArray<T> left, MemoryArray<T> right)
            {
                return !(left == right);
            }

            public T GetValue(int index)
            {
                if (index >= length || index < 0)
                {
                    throw new ArgumentOutOfRangeException("index");
                }

                return GetValueInternal(index);
            }

            private unsafe T GetValueInternal(int index)
            {
                EnsureNotDisposed();
                if (isOneBit)
                {
                    return (T)(object)(FI1BIT)(byte)(((baseAddress[index / 8] & (1 << 7 - index % 8)) != 0) ? 1u : 0u);
                }

                if (isFourBit)
                {
                    return (T)(object)(FI4BIT)(byte)((index % 2 == 0) ? ((uint)(baseAddress[index / 2] >> 4)) : (baseAddress[index / 2] & 0xFu));
                }

                CopyMemory(ptr, baseAddress + index * size, size);
                return buffer[0];
            }

            public void SetValue(T value, int index)
            {
                if (index >= length || index < 0)
                {
                    throw new ArgumentOutOfRangeException("index");
                }

                SetValueInternal(value, index);
            }

            private unsafe void SetValueInternal(T value, int index)
            {
                EnsureNotDisposed();
                if (isOneBit)
                {
                    if ((byte)(FI1BIT)(object)value != 0)
                    {
                        byte* num = baseAddress + index / 8;
                        *num |= (byte)(1 << 7 - index % 8);
                    }
                    else
                    {
                        byte* num2 = baseAddress + index / 8;
                        *num2 &= (byte)(~(1 << 7 - index % 8));
                    }
                }
                else if (isFourBit)
                {
                    if (index % 2 == 0)
                    {
                        baseAddress[index / 2] = (byte)((baseAddress[index / 2] & 0xFu) | (uint)((byte)(FI4BIT)(object)value << 4));
                    }
                    else
                    {
                        baseAddress[index / 2] = (byte)((baseAddress[index / 2] & 0xF0u) | ((byte)(FI4BIT)(object)value & 0xFu));
                    }
                }
                else
                {
                    buffer[0] = value;
                    CopyMemory(baseAddress + index * size, ptr, size);
                }
            }

            public unsafe T[] GetValues(int index, int length)
            {
                EnsureNotDisposed();
                if (index >= this.length || index < 0)
                {
                    throw new ArgumentOutOfRangeException("index");
                }

                if (index + length > this.length || length < 1)
                {
                    throw new ArgumentOutOfRangeException("length");
                }

                T[] array = new T[length];
                if (isOneBit || isFourBit)
                {
                    for (int i = 0; i < length; i++)
                    {
                        array[i] = GetValueInternal(i);
                    }
                }
                else
                {
                    GCHandle gCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
                    byte* dest = (byte*)(void*)Marshal.UnsafeAddrOfPinnedArrayElement(array, 0);
                    CopyMemory(dest, baseAddress + size * index, size * length);
                    gCHandle.Free();
                }

                return array;
            }

            public unsafe void SetValues(T[] values, int index)
            {
                EnsureNotDisposed();
                if (values == null)
                {
                    throw new ArgumentNullException("values");
                }

                if (index >= length || index < 0)
                {
                    throw new ArgumentOutOfRangeException("index");
                }

                if (index + values.Length > length)
                {
                    throw new ArgumentOutOfRangeException("values.Length");
                }

                if (isOneBit || isFourBit)
                {
                    int num = 0;
                    while (num != values.Length)
                    {
                        SetValueInternal(values[num++], index++);
                    }
                }
                else
                {
                    GCHandle gCHandle = GCHandle.Alloc(values, GCHandleType.Pinned);
                    byte* src = (byte*)(void*)Marshal.UnsafeAddrOfPinnedArrayElement(values, 0);
                    CopyMemory(baseAddress + index * size, src, size * length);
                    gCHandle.Free();
                }
            }

            public void CopyTo(Array array, int index)
            {
                EnsureNotDisposed();
                if (!(array is T[]))
                {
                    throw new InvalidCastException("array");
                }

                try
                {
                    CopyTo((T[])array, 0, index, length);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    throw new ArgumentException(ex.Message, ex);
                }
            }

            public unsafe void CopyTo(T[] array, int sourceIndex, int destinationIndex, int length)
            {
                EnsureNotDisposed();
                if (array == null)
                {
                    throw new ArgumentNullException("array");
                }

                if (sourceIndex >= this.length || sourceIndex < 0)
                {
                    throw new ArgumentOutOfRangeException("sourceIndex");
                }

                if (destinationIndex >= array.Length || destinationIndex < 0)
                {
                    throw new ArgumentOutOfRangeException("destinationIndex");
                }

                if (sourceIndex + length > this.length || destinationIndex + length > array.Length || length < 1)
                {
                    throw new ArgumentOutOfRangeException("length");
                }

                if (isOneBit || isFourBit)
                {
                    for (int i = 0; i != length; i++)
                    {
                        array[destinationIndex++] = GetValueInternal(sourceIndex++);
                    }
                }
                else
                {
                    GCHandle gCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
                    byte* dest = (byte*)(void*)Marshal.UnsafeAddrOfPinnedArrayElement(array, destinationIndex);
                    CopyMemory(dest, baseAddress + size * sourceIndex, size * length);
                    gCHandle.Free();
                }
            }

            public unsafe void CopyFrom(T[] array, int sourceIndex, int destinationIndex, int length)
            {
                EnsureNotDisposed();
                if (array == null)
                {
                    throw new ArgumentNullException("array");
                }

                if (destinationIndex >= this.length || destinationIndex < 0)
                {
                    throw new ArgumentOutOfRangeException("destinationIndex");
                }

                if (sourceIndex >= array.Length || sourceIndex < 0)
                {
                    throw new ArgumentOutOfRangeException("sourceIndex");
                }

                if (destinationIndex + length > this.length || sourceIndex + length > array.Length || length < 1)
                {
                    throw new ArgumentOutOfRangeException("length");
                }

                if (isOneBit || isFourBit)
                {
                    for (int i = 0; i != length; i++)
                    {
                        SetValueInternal(array[sourceIndex++], destinationIndex++);
                    }
                }
                else
                {
                    GCHandle gCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
                    byte* src = (byte*)(void*)Marshal.UnsafeAddrOfPinnedArrayElement(array, sourceIndex);
                    CopyMemory(baseAddress + size * destinationIndex, src, size * length);
                    gCHandle.Free();
                }
            }

            public unsafe byte[] ToByteArray()
            {
                EnsureNotDisposed();
                byte[] array = (isOneBit ? new byte[(length + 7) / 8] : ((!isFourBit) ? new byte[size * length] : new byte[(length + 3) / 4]));
                fixed (byte* dest = array)
                {
                    CopyMemory(dest, baseAddress, array.Length);
                }

                return array;
            }

            public unsafe object Clone()
            {
                EnsureNotDisposed();
                return new MemoryArray<T>(baseAddress, length);
            }

            public IEnumerator GetEnumerator()
            {
                EnsureNotDisposed();
                T[] values = GetValues(0, length);
                for (int i = 0; i != values.Length; i++)
                {
                    yield return values[i];
                }
            }

            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                EnsureNotDisposed();
                T[] values = GetValues(0, length);
                for (int i = 0; i != values.Length; i++)
                {
                    yield return values[i];
                }
            }

            public void Dispose()
            {
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }

            protected unsafe virtual void Dispose(bool disposing)
            {
                if (baseAddress != null)
                {
                    if (handle.IsAllocated)
                    {
                        handle.Free();
                    }

                    baseAddress = null;
                    buffer = null;
                    length = 0;
                    syncRoot = null;
                }
            }

            protected unsafe virtual void EnsureNotDisposed()
            {
                if (baseAddress == null)
                {
                    throw new ObjectDisposedException("This instance is disposed.");
                }
            }

            public override bool Equals(object obj)
            {
                EnsureNotDisposed();
                if (obj is MemoryArray<T>)
                {
                    return Equals((MemoryArray<T>)obj);
                }

                return false;
            }

            public unsafe bool Equals(MemoryArray<T> other)
            {
                EnsureNotDisposed();
                if (baseAddress == other.baseAddress)
                {
                    return length == other.length;
                }

                return false;
            }

            public unsafe override int GetHashCode()
            {
                EnsureNotDisposed();
                return (int)baseAddress ^ length;
            }

            protected unsafe static void CopyMemory(byte* dest, byte* src, int len)
            {
                if (len >= 16)
                {
                    do
                    {
                        *(int*)dest = *(int*)src;
                        *(int*)(dest + 4) = *(int*)(src + 4);
                        *(int*)(dest + 8) = *(int*)(src + 8);
                        *(int*)(dest + 12) = *(int*)(src + 12);
                        dest += 16;
                        src += 16;
                    }
                    while ((len -= 16) >= 16);
                }

                if (len > 0)
                {
                    if (((uint)len & 8u) != 0)
                    {
                        *(int*)dest = *(int*)src;
                        *(int*)(dest + 4) = *(int*)(src + 4);
                        dest += 8;
                        src += 8;
                    }

                    if (((uint)len & 4u) != 0)
                    {
                        *(int*)dest = *(int*)src;
                        dest += 4;
                        src += 4;
                    }

                    if (((uint)len & 2u) != 0)
                    {
                        *(short*)dest = *(short*)src;
                        dest += 2;
                        src += 2;
                    }

                    if (((uint)len & (true ? 1u : 0u)) != 0)
                    {
                        *dest = *src;
                    }
                }
            }
        }
    }
}
