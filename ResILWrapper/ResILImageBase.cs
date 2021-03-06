using ResIL.Unmanaged;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using UsefulThings;

namespace ResILWrapper
{
    public abstract class ResILImageBase : IDisposable
    {
        public enum MipMapMode
        {
            BuildAll, RemoveAllButOne, Rebuild, ForceRemove, None
        }

        public static List<string> DDSFormats = new List<string>() { "None", "DXT1", "DXT2", "DXT3", "DXT4", "DXT5", "3Dc/ATI2", "RXGB", "ATI1N/BC4", "DXT1A", "G8", "ARGB", "V8U8" }; // KFreon: DDS Surface formats
        public static List<string> ValidFormats = null;

        public string Path { get; protected set; }
        public CompressedDataFormat SurfaceFormat { get; protected set; }
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        public int Mips { get; protected set; }
        public ImageType ImageType { get; set; }

        public abstract BitmapImage ToImage(ImageType type = ImageType.Jpg, int quality = 80, int width = 0, int height = 0);
        public abstract byte[] ToArray();
        
        public abstract bool BuildMipMaps(bool rebuild = false);
        public abstract bool RemoveMipMaps(bool force = false);
        public abstract bool ConvertAndSave(ImageType type, string savePath, MipMapMode MipsMode = MipMapMode.BuildAll, CompressedDataFormat surface = CompressedDataFormat.None, int quality = 80, bool SetJPGQuality = true);
        public abstract bool ConvertAndSave(ImageType type, Stream stream, MipMapMode MipsMode = MipMapMode.BuildAll, CompressedDataFormat surface = CompressedDataFormat.None, int quality = 80, bool SetJPGQuality = true);
        public abstract bool Resize(int width, int height);

        public static int EstimateNumMips(int Width, int Height)
        {
            int determiningDimension = Height > Width ? Width : Height;   // KFreon: Get smallest dimension

            return (int)Math.Log(determiningDimension, 2) + 1;
        }

        //Heff: Helper to get error info
        public static ErrorType GetLastError()
        {
            return IL2.GetError();
        }

        static ResILImageBase()
        {
            string[] types = Enum.GetNames(typeof(ResIL.Unmanaged.ImageType));
            ValidFormats = new List<string>(types.Where(t => t != "Dds" && t != "Unknown")).Concat(DDSFormats).ToList(30);  // KFreon: Remove DDS from types list
        }


        #region Creation
        public static ResILImageBase Create(string filepath)
        {
            using (FileStream stream = new FileStream(filepath, FileMode.Open))
                return Create(stream);
        }

        public static ResILImageBase Create(Stream stream)
        {
            bool? isv8u8 = IsV8U8(stream);
            if (isv8u8 == true)
                return new V8U8Image(stream);
            else if (isv8u8 == false)
                return new ResILImage(stream);

            throw new InvalidDataException("Not a valid image");
        }

        public static ResILImageBase Create(byte[] imageData)
        {
            using (MemoryTributary stream = new MemoryTributary(imageData))
                return Create(stream);
        }

        static bool? IsV8U8(Stream stream)
	    {
		    stream.Seek(0, SeekOrigin.Begin);
		    using (BinaryReader r = new BinaryReader(stream, Encoding.Default, true))
		    {
			    int dwMagic = r.ReadInt32();
	            if (dwMagic != 0x20534444)
	                return null;
	
	            DDS_HEADER header = new DDS_HEADER();
	            Read_DDS_HEADER(header, r);
	
	            if (((header.ddspf.dwFlags & 0x00000004) != 0) && (header.ddspf.dwFourCC == 0x30315844 /*DX10*/))
	            {
	                throw new Exception("DX10 not supported yet!");
	            }
                return header.ddspf.dwRGBBitCount == 0x10 &&
                           header.ddspf.dwRBitMask == 0xFF &&
                           header.ddspf.dwGBitMask == 0xFF00 &&
                           header.ddspf.dwBBitMask == 0x00 &&
                           header.ddspf.dwABitMask == 0x00;
		    }
	    }
        #endregion Creation

        public System.Drawing.Bitmap ToWinFormsBitmap(int width = 0, int height = 0)
        {
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            BitmapImage bmp = ToImage(width: width, height: height);
            BitmapFrame frame = BitmapFrame.Create(bmp);
            encoder.Frames.Add(frame);

            using (MemoryTributary ms = new MemoryTributary())
            {
                encoder.Save(ms);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(ms);
                return new System.Drawing.Bitmap(bitmap);
            }

        }

        #region DDS Header stuff
        /// <summary>
        /// Reads DDS header from file.
        /// </summary>
        /// <param name="h">Header struct.</param>
        /// <param name="r">File reader.</param>
        public static void Read_DDS_HEADER(DDS_HEADER h, BinaryReader r)
        {
            h.dwSize = r.ReadInt32();
            h.dwFlags = r.ReadInt32();
            h.dwHeight = r.ReadInt32();
            h.dwWidth = r.ReadInt32();
            h.dwPitchOrLinearSize = r.ReadInt32();
            h.dwDepth = r.ReadInt32();
            h.dwMipMapCount = r.ReadInt32();
            for (int i = 0; i < 11; ++i)
            {
                h.dwReserved1[i] = r.ReadInt32();
            }
            Read_DDS_PIXELFORMAT(h.ddspf, r);
            h.dwCaps = r.ReadInt32();
            h.dwCaps2 = r.ReadInt32();
            h.dwCaps3 = r.ReadInt32();
            h.dwCaps4 = r.ReadInt32();
            h.dwReserved2 = r.ReadInt32();
        }

        /// <summary>
        /// Reads DDS pixel format.
        /// </summary>
        /// <param name="p">Pixel format struct.</param>
        /// <param name="r">File reader.</param>
        private static void Read_DDS_PIXELFORMAT(DDS_PIXELFORMAT p, BinaryReader r)
        {
            p.dwSize = r.ReadInt32();
            p.dwFlags = r.ReadInt32();
            p.dwFourCC = r.ReadInt32();
            p.dwRGBBitCount = r.ReadInt32();
            p.dwRBitMask = r.ReadInt32();
            p.dwGBitMask = r.ReadInt32();
            p.dwBBitMask = r.ReadInt32();
            p.dwABitMask = r.ReadInt32();
        }


        public class DDS_HEADER
        {
            public int dwSize;
            public int dwFlags;
            /*	DDPF_ALPHAPIXELS   0x00000001 
                DDPF_ALPHA   0x00000002 
                DDPF_FOURCC   0x00000004 
                DDPF_RGB   0x00000040 
                DDPF_YUV   0x00000200 
                DDPF_LUMINANCE   0x00020000 
             */
            public int dwHeight;
            public int dwWidth;
            public int dwPitchOrLinearSize;
            public int dwDepth;
            public int dwMipMapCount;
            public int[] dwReserved1 = new int[11];
            public DDS_PIXELFORMAT ddspf = new DDS_PIXELFORMAT();
            public int dwCaps;
            public int dwCaps2;
            public int dwCaps3;
            public int dwCaps4;
            public int dwReserved2;
        }

        public class DDS_PIXELFORMAT
        {
            public int dwSize;
            public int dwFlags;
            public int dwFourCC;
            public int dwRGBBitCount;
            public int dwRBitMask;
            public int dwGBitMask;
            public int dwBBitMask;
            public int dwABitMask;

            public DDS_PIXELFORMAT()
            {
            }
        }
        #endregion DDS Header stuff

        public void Dispose()
        {
            // IGNORE
        }
    }
}
