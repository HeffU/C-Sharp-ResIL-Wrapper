﻿/*
* Copyright (c) 2012 Nicholas Woodfield
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResIL.Unmanaged
{
    /// <summary>
    /// Enumerates error types.
    /// </summary>
    public enum ErrorType
    {

        /// <summary>
        /// Everything's A-OK, no error reported.
        /// </summary>
        NoError = 0x0000,

        /// <summary>
        /// Invalid enum.
        /// </summary>
        InvalidEnum = 0x0501,

        /// <summary>
        /// DevIL ran out of memory.
        /// </summary>
        OutOfMemory = 0x0502,

        /// <summary>
        /// Format is not supported.
        /// </summary>
        FormatNotSupported = 0x0503,

        /// <summary>
        /// There was an internal error.
        /// </summary>
        InternalError = 0x0504,

        /// <summary>
        /// Value was invalid.
        /// </summary>
        InvalidValue = 0x0505,

        /// <summary>
        /// Operation was illegal.
        /// </summary>
        IllegalOperation = 0x0506,

        /// <summary>
        /// File value not valid.
        /// </summary>
        IllegalFileValue = 0x0507,

        /// <summary>
        /// File header not valid.
        /// </summary>
        IllegalFileHeader = 0x0508,

        /// <summary>
        /// Invalid parameter.
        /// </summary>
        InvalidParameter = 0x0509,

        /// <summary>
        /// Could not open the file.
        /// </summary>
        CouldNotOpenFile = 0x050A,

        /// <summary>
        /// Extension not supported.
        /// </summary>
        InvalidExtension = 0x050B,

        /// <summary>
        /// File already exists.
        /// </summary>
        FileAlreadyExists = 0x050C,

        /// <summary>
        /// Output format is the same as input.
        /// </summary>
        OutFormatSame = 0x050D,

        /// <summary>
        /// Stack overflow error.
        /// </summary>
        StackOverflow = 0x050E,

        /// <summary>
        /// Stack underflow error.
        /// </summary>
        StackUnderflow = 0x050F,

        /// <summary>
        /// There was an error in conversion.
        /// </summary>
        InvalidConversion = 0x0510,

        /// <summary>
        /// Error with image dimensions.
        /// </summary>
        BadDimensions = 0x0511,

        /// <summary>
        /// Error in reading file.
        /// </summary>
        FileReadError = 0x0512,

        /// <summary>
        /// Error in writing file.
        /// </summary>
        FileWriteError = 0x0512,

        /// <summary>
        /// GIF format specific error.
        /// </summary>
        Gif_Error = 0x05E1,

        /// <summary>
        /// JPG format specific error.
        /// </summary>
        Jpeg_Error = 0x05E2,

        /// <summary>
        /// PNG format specific error.
        /// </summary>
        Png_Error = 0x05E3,

        /// <summary>
        /// TIFF format specific error.
        /// </summary>
        Tiff_Error = 0x05E4,

        /// <summary>
        /// MNG format specific error.
        /// </summary>
        Mng_Error = 0x05E5,

        /// <summary>
        /// JP2 format specific error.
        /// </summary>
        Jp2_Error = 0x05E6,

        /// <summary>
        /// EXR format specific error.
        /// </summary>
        Exr_Error = 0x05E7,

        /// <summary>
        /// Unknown error.
        /// </summary>
        UnknownError = 0x05FF,

        /// <summary>
        /// DDS data not found.
        /// </summary>
        No_DDS_Data = 0x0513,
        
        /// <summary>
        /// Invalid or unexpected DDS compression.
        /// </summary>
        DDS_Compression_Incorrect = 0x0514
    }


    /// <summary>
    /// Enumerates attribute bit flags.
    /// </summary>
    [Flags]
    public enum AttributeBits
    {
        Origin = ILDefines.IL_ORIGIN_BIT,
        File = ILDefines.IL_FILE_BIT,
        Palette = ILDefines.IL_PAL_BIT,
        Format = ILDefines.IL_FORMAT_BIT,
        Type = ILDefines.IL_TYPE_BIT,
        Compress = ILDefines.IL_COMPRESS_BIT,
        LoadFail = ILDefines.IL_LOADFAIL_BIT,
        FormatSpecific = ILDefines.IL_FORMAT_SPECIFIC_BIT,
        All = ILDefines.IL_ALL_ATTRIB_BITS
    }


    /// <summary>
    /// Enumerates possible enable caps for DevIL's Enable/Disable function to enable/disable
    /// certain behaviors by the library.
    /// </summary>
    public enum ILEnable
    {
        /// <summary>
        /// If enabled, DevIL will convert (if need be) all images to the same data format (BGR, RGB, etc) that is specified by the user.
        /// </summary>
        AbsoluteFormat = ILDefines.IL_FORMAT_SET,

        /// <summary>
        /// If enabled, DevIL will convert (if need be) all images to the same data type (byte, float, etc) that is specified by the user.
        /// </summary>
        AbsoluteType = ILDefines.IL_TYPE_SET,

        /// <summary>
        /// If enabled, DevIL will use an absolute origin for all images.
        /// </summary>
        AbsoluteOrigin = ILDefines.IL_ORIGIN_SET,

        /// <summary>
        /// If enabled, DevIL will overwrite existing files when an image is saved.
        /// </summary>
        OverwriteExistingFile = ILDefines.IL_FILE_OVERWRITE,

        /// <summary>
        /// If enabled, DevIL will convert palettes to their base type (e.g. BGR palette to data format BGR).
        /// </summary>
        ConvertPalette = ILDefines.IL_CONV_PAL,

        /// <summary>
        /// If enabled, DevIL will use the nVidia Texture Tools (NVTT) library compression instead
        /// of the library default. If this is enabled with squish, NVTT takes higher priority and will be used
        /// instead.
        /// </summary>
        NvidiaCompression = ILDefines.IL_NVIDIA_COMPRESS,

        /// <summary>
        /// If enabled, DevIL will use the Squish library compression. Squish is slower than NVTT
        /// but generates images with the highest quality possible. If this is set with NvidiaCompression,
        /// the NVTT library is used instead. Note: Doesn't seem to be used anymore?
        /// </summary>
        SquishCompression = ILDefines.IL_SQUISH_COMPRESS,

        /// <summary>
        /// If enabled, when loading an image fails, DevIL will load the default image instead.
        /// </summary>
        DefaultOnFail = ILDefines.IL_DEFAULT_ON_FAIL,

        /// <summary>
        /// If enabled, DevIL will use a color key.
        /// </summary>
        UseColorKey = ILDefines.IL_USE_KEY_COLOR,

        /// <summary>
        /// If enabled, DevIL will blend the source image with the destination
        /// image if the source has an alpha channel present.
        /// </summary>
        BlitBlend = ILDefines.IL_BLIT_BLEND,

        /// <summary>
        /// If enabled, DevIL will do interlaced saving for PNG images.
        /// </summary>
        SaveInterlaced = ILDefines.IL_SAVE_INTERLACED,

        /// <summary>
        /// If enabled, DevIL will do "progressive" saving of JPG images.
        /// </summary>
        JpgProgressive = ILDefines.IL_JPG_PROGRESSIVE
    }

    public enum ILIntegerMode
    {
        CurrentImage = ILDefines.IL_CUR_IMAGE,
        MaxQuantizationIndices = ILDefines.IL_MAX_QUANT_INDICES,
        NeuQuantizationSample = ILDefines.IL_NEU_QUANT_SAMPLE,
        VersionNumber = ILDefines.IL_VERSION_NUM,
        VTFCompression = ILDefines.IL_VTF_COMP,
        JpgQuality = ILDefines.IL_JPG_QUALITY,
        PcdPicNumber = ILDefines.IL_PCD_PICNUM,
        PngAlphaIndex = ILDefines.IL_PNG_ALPHA_INDEX,
        ActiveImage = ILDefines.IL_ACTIVE_IMAGE,
        ActiveMipMap = ILDefines.IL_ACTIVE_MIPMAP,
        ActiveLayer = ILDefines.IL_ACTIVE_LAYER,

        ImageDuration = ILDefines.IL_IMAGE_DURATION,
        ImageOffsetX = ILDefines.IL_IMAGE_OFFX,
        ImageOffsetY = ILDefines.IL_IMAGE_OFFY,

        ImageWidth = ILDefines.IL_IMAGE_WIDTH,
        ImageHeight = ILDefines.IL_IMAGE_HEIGHT,
        ImageDepth = ILDefines.IL_IMAGE_DEPTH,
        ImageBitsPerPixel = ILDefines.IL_IMAGE_BITS_PER_PIXEL,
        ImageBytesPerPixel = ILDefines.IL_IMAGE_BYTES_PER_PIXEL,
        ImageChannels = ILDefines.IL_IMAGE_CHANNELS,
        ImageSizeOfData = ILDefines.IL_IMAGE_SIZE_OF_DATA,
        ImagePlaneSize = ILDefines.IL_IMAGE_PLANESIZE,
        ImageFaceCount = ILDefines.IL_NUM_FACES,
        ImageArrayCount = ILDefines.IL_NUM_IMAGES,
        ImageLayerCount = ILDefines.IL_NUM_LAYERS,
        ImageMipMapCount = ILDefines.IL_NUM_MIPMAPS,
        ImagePaletteBytesPerPixel = ILDefines.IL_PALETTE_BPP,
        ImagePaletteColumnCount = ILDefines.IL_PALETTE_NUM_COLS
    }

    public enum ILBooleanMode
    {
        KeepDxtcData = ILDefines.IL_KEEP_DXTC_DATA,
        BmpRLE = ILDefines.IL_BMP_RLE,
        PngInterlace = ILDefines.IL_PNG_INTERLACE,
        SgiRLE = ILDefines.IL_SGI_RLE,
        TgaCreateStamp = ILDefines.IL_TGA_CREATE_STAMP,
        TgaRLE = ILDefines.IL_TGA_RLE
    }

    public enum ILStringMode
    {
        TgaID = ILDefines.IL_TGA_ID_STRING,
        TgaAuthorName = ILDefines.IL_TGA_AUTHNAME_STRING,
        TgaAuthorComment = ILDefines.IL_TGA_AUTHCOMMENT_STRING,
        PngAuthorName = ILDefines.IL_PNG_AUTHNAME_STRING,
        PngTitle = ILDefines.IL_PNG_TITLE_STRING,
        PngDescription = ILDefines.IL_PNG_DESCRIPTION_STRING,
        TifDescription = ILDefines.IL_TIF_DESCRIPTION_STRING,
        TifHostComputer = ILDefines.IL_TIF_HOSTCOMPUTER_STRING,
        TifDocumentName = ILDefines.IL_TIF_DOCUMENTNAME_STRING,
        TifAuthorName = ILDefines.IL_TIF_AUTHNAME_STRING,
        CHeadHeader = ILDefines.IL_CHEAD_HEADER_STRING
    }

    /// <summary>
    /// Enumerates sub image types.
    /// </summary>
    public enum SubImageType
    {
        /// <summary>
        /// Denotes a "next" image in the chain, e.g. animation or face.
        /// </summary>
        Image = (int)ILDefines.IL_SUB_NEXT,

        /// <summary>
        /// Denotes a mipmap.
        /// </summary>
        MipMap = (int)ILDefines.IL_SUB_MIPMAP,

        /// <summary>
        /// Denotes a layer.
        /// </summary>
        Layer = (int)ILDefines.IL_SUB_LAYER
    }

    /// <summary>
    /// Enumerates data types.
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// 8-Bit signed byte.
        /// </summary>
        Byte = ILDefines.IL_BYTE,

        /// <summary>
        /// 8-Bit unsigned byte.
        /// </summary>
        UnsignedByte = ILDefines.IL_UNSIGNED_BYTE,

        /// <summary>
        /// 16-Bit signed integer.
        /// </summary>
        Short = ILDefines.IL_SHORT,

        /// <summary>
        /// 16-Bit unsigned integer.
        /// </summary>
        UnsignedShort = ILDefines.IL_UNSIGNED_SHORT,

        /// <summary>
        /// 32-Bit signed integer.
        /// </summary>
        Int = ILDefines.IL_INT,

        /// <summary>
        /// 32-Bit signed integer.
        /// </summary>
        UnsignedInt = ILDefines.IL_UNSIGNED_INT,

        /// <summary>
        /// 32-Bit floating point value.
        /// </summary>
        Float = ILDefines.IL_FLOAT,

        /// <summary>
        /// 64-Bit floating point value.
        /// </summary>
        Double = ILDefines.IL_DOUBLE,

        /// <summary>
        /// 16-Bit floating point value.
        /// </summary>
        Half = ILDefines.IL_HALF,
    }


    /// <summary>
    /// Enumerates uncompressed data formats.
    /// </summary>
    public enum DataFormat
    {
        ColorIndex = ILDefines.IL_COLOR_INDEX,
        Alpha = ILDefines.IL_ALPHA,
        RGB = ILDefines.IL_RGB,
        RGBA = ILDefines.IL_RGBA,
        BGR = ILDefines.IL_BGR,
        BGRA = ILDefines.IL_BGRA,
        Luminance = ILDefines.IL_LUMINANCE,
        LuminanceAlpha = ILDefines.IL_LUMINANCE_ALPHA
    }

    /// <summary>
    /// Enumerates compressed data formats.
    /// </summary>
    public enum CompressedDataFormat
    {
        /// <summary>
        /// No compression.
        /// </summary>
        None = ILDefines.IL_DXT_NO_COMP,

        /// <summary>
        /// RGB format with an alpha channel that is completely white. Format
        /// looks like this : 8:8:8:1 RGBA.
        /// </summary>
        DXT1 = ILDefines.IL_DXT1,

        /// <summary>
        /// Similar to DXT3 however, the color data is interpreted as being
        /// premultiplied by Alpha.
        /// </summary>
        DXT2 = ILDefines.IL_DXT2,

        /// <summary>
        /// RGB format with explicit alpha, useful for partially transparent textures.
        /// Format looks like this: 8:8:8:8: RGBA.
        /// </summary>
        DXT3 = ILDefines.IL_DXT3,

        /// <summary>
        /// Similar to DXT5 however, the color data is interpreted as being
        /// premultiplied by Alpha.
        /// </summary>
        DXT4 = ILDefines.IL_DXT4,

        /// <summary>
        /// RGB format with interpolated alpha. Similar to DXT3 but can do a better
        /// job with alpha (e.g. slow gradients). Format looks like this: 8:8:8:8 RGBA.
        /// </summary>
        DXT5 = ILDefines.IL_DXT5,

        /// <summary>
        /// 3Dc (FourCC) or Block Compression 5 (BC5) is a format developed by ATI
        /// that builds upon the DXT5 algorithm.
        /// </summary>
        ThreeDC = ILDefines.IL_3DC,

        /// <summary>
        /// Format for normal maps that is similar to DXT5, but with red and alpha
        /// components swapped, so the normal is given by the (a, g, b) components.
        /// </summary>
        RXGB = ILDefines.IL_RXGB,

        /// <summary>
        /// Also known as Block Compression 4, it is a format similar to DXT5.
        /// </summary>
        ATI1N = ILDefines.IL_ATI1N,

        /// <summary>
        /// Usually the same as DXT1, except for Nvidia Texture Tools. Is an RGB
        /// texture with a one-bit-depth alpha value. Format
        /// looks like this : 8:8:8:1 RGBA.
        /// </summary>
        DXT1A = ILDefines.IL_DXT1A,

        V8U8 = ILDefines.V8U8,  // KFreon: ADDED

        // Heff: Added this to handle 1805 format ID.
        /// <summary>
        /// G8/L8, luminance format based on ARGB.
        /// does seem to use red rather than green when looking in the files,
        /// but bioware has called it G8/L8 anyway, unsure of why.
        /// </summary>
        G8 = ILDefines.IL_DXTC_DATA_FORMAT
    }

    /// <summary>
    /// Enumerates the image origin.
    /// </summary>
    public enum OriginLocation
    {
        /// <summary>
        /// Origin is the lower left hand corner, ideal for OpenGL.
        /// </summary>
        LowerLeft = ILDefines.IL_ORIGIN_LOWER_LEFT,

        /// <summary>
        /// Origin is the upper left hand corner, ideal for Direct3D.
        /// </summary>
        UpperLeft = ILDefines.IL_ORIGIN_UPPER_LEFT
    }

    /// <summary>
    /// Enumerates palette types.
    /// </summary>
    public enum PaletteType
    {
        None = ILDefines.IL_PAL_NONE,
        RGB24 = ILDefines.IL_PAL_RGB24,
        RGB32 = ILDefines.IL_PAL_RGB32,
        RGBA32 = ILDefines.IL_PAL_RGBA32,
        BGR24 = ILDefines.IL_PAL_BGR24,
        BGR32 = ILDefines.IL_PAL_BGR32,
        BGRA32 = ILDefines.IL_PAL_BGRA32
    }

    /// <summary>
    /// Enumerates the compression algorithm DevIL uses.
    /// </summary>
    public enum CompressionAlgorithm
    {
        /// <summary>
        /// No compression
        /// </summary>
        None = ILDefines.IL_COMPRESS_NONE,

        /// <summary>
        /// Use run-length encoding (RLE).
        /// </summary>
        RLE = ILDefines.IL_COMPRESS_RLE,

        /// <summary>
        /// ZLib compression
        /// </summary>
        ZLib = ILDefines.IL_COMPRESS_ZLIB,
    }

    /// <summary>
    /// Enumerates DevIL compression library options.
    /// </summary>
    public enum CompressionLibrary
    {
        /// <summary>
        /// Use the default compression.
        /// </summary>
        Default,

        /// <summary>
        /// Use Nvidia compression.
        /// </summary>
        Nvidia,

        /// <summary>
        /// Use Squish compression.
        /// </summary>
        Squish
    }

    public enum Quantization
    {
        Wu = ILDefines.IL_WU_QUANT,
        Neu = ILDefines.IL_NEU_QUANT
    }

    /// <summary>
    /// Enumerates the faces of an environment map (cubemap) or denotes
    /// a sphere map.
    /// </summary>
    public enum CubeMapFace
    {
        /// <summary>
        /// No face defined.
        /// </summary>
        None = 0,

        /// <summary>
        /// Positive X.
        /// </summary>
        PositiveX = ILDefines.IL_CUBEMAP_POSITIVEX,

        /// <summary>
        /// Negative X.
        /// </summary>
        NegativeX = ILDefines.IL_CUBEMAP_NEGATIVEX,

        /// <summary>
        /// Positive Y.
        /// </summary>
        PositiveY = ILDefines.IL_CUBEMAP_POSITIVEY,

        /// <summary>
        /// Negative Y.
        /// </summary>
        NegativeY = ILDefines.IL_CUBEMAP_NEGATIVEY,

        /// <summary>
        /// Positive Z.
        /// </summary>
        PositiveZ = ILDefines.IL_CUBEMAP_POSITIVEZ,

        /// <summary>
        /// Negative Z.
        /// </summary>
        NegativeZ = ILDefines.IL_CUBEMAP_NEGATIVEZ,

        /// <summary>
        /// Not a cube map, but a sphere map. Seems to be unused.
        /// </summary>
        SphereMap = ILDefines.IL_SPHEREMAP,
    }

    /// <summary>
    /// Enumerates image types that can be loaded by DevIL.
    /// </summary>
    public enum ImageType
    {
        /// <summary>
        /// Unknown image type.
        /// </summary>
        Unknown = 0x0000,

        /// <summary>
        /// Microsoft Windows Bitmap - .bmp extension.
        /// </summary>
        Bmp = 0x0420,

        /// <summary>
        /// Dr. Halo - .cut extension.
        /// </summary>
        Cut = 0x0421,

        /// <summary>
        /// DOOM Walls - no specific extension.
        /// </summary>
        Doom = 0x0422,

        /// <summary>
        /// DOOM flats - no specific extension.
        /// </summary>
        DoomFlat = 0x0423,

        /// <summary>
        /// Microsoft Windows Icons and Cursors - .ico and .cur extensions.
        /// </summary>
        Ico = 0x0424,

        /// <summary>
        /// JPEG - .jpg, .jpe, and .jpeg extensions.
        /// </summary>
        Jpg = 0x0425,

        /// <summary>
        /// Handled as a Jpg.
        /// </summary>
        //Jfif = 0x0425,

        /// <summary>
        /// Amiga IFF - .iff, .ilbm, .lbm extensions.
        /// </summary>
        Ilbm = 0x0426,

        /// <summary>
        /// Kodak PhotoCD - .pcd extension.
        /// </summary>
        Pcd = 0x0427,

        /// <summary>
        /// ZSoft PCX - .pcx extension.
        /// </summary>
        Pcx = 0x0428,

        /// <summary>
        /// PIC - .pc extension.
        /// </summary>
        Pic = 0x0429,

        /// <summary>
        /// Portable Network Graphics - .png extension.
        /// </summary>
        Png = 0x042A,

        /// <summary>
        /// Portable Any Map - .pbm, .pgm, .ppm, and .pnm extensions.
        /// </summary>
        Pnm = 0x042B,

        /// <summary>
        /// Silicon Graphics - .sgi, .bw, .rgb, and .rgba extensions.
        /// </summary>
        Sgi = 0x042C,

        /// <summary>
        /// TrueVision Targa File - .tga, .vda, .icb, and .vst extensions.
        /// </summary>
        Tga = 0x042D,

        /// <summary>
        /// Tagged Image File Format - .tif and .tiff extensions.
        /// </summary>
        Tiff = 0x042E,

        /// <summary>
        /// C-Style header - .h extension.
        /// </summary>
        CHeader = 0x042F,

        /// <summary>
        /// Raw Image Data - any extension (e.g. .raw).
        /// </summary>
        Raw = 0x0430,

        /// <summary>
        /// Half-Life Model Texture - .mdl extension.
        /// </summary>
        Mdl = 0x0431,

        /// <summary>
        /// Quake 2 Texture - .wal extension.
        /// </summary>
        Wal = 0x0432,

        /// <summary>
        /// Homeworld Texture - .lif extension.
        /// </summary>
        Lif = 0x0434,

        /// <summary>
        /// Multiple-Image Network Graphics - .mng extension.
        /// </summary>
        Mng = 0x0435,

        /// <summary>
        /// Handled as a Mng.
        /// </summary>
        Jng = 0x0435,

        /// <summary>
        /// Graphics Interchange Format - .gif extension.
        /// </summary>
        Gif = 0x0436,

        /// <summary>
        /// DirectDraw Surface - .dds extension.
        /// </summary>
        Dds = 0x0437,

        /// <summary>
        /// ZSoft Multi-PCX - .dcx extension.
        /// </summary>
        Dcx = 0x0438,

        /// <summary>
        /// Adobe Photoshop - .psd extension.
        /// </summary>
        PSD = 0x0439,

        /// <summary>
        /// Exif format, no specified extension.
        /// </summary>
        Exif = 0x043A,

        /// <summary>
        /// Paintshop Pro - .psp extension.
        /// </summary>
        Psp = 0x043B,

        /// <summary>
        /// PIX - .pix extension.
        /// </summary>
        Pix = 0x043C,

        /// <summary>
        /// Pixar - .pxr extension.
        /// </summary>
        Pxr = 0x043D,

        /// <summary>
        /// X Pixel Map - .xpm extension.
        /// </summary>
        Xpm = 0x043E,

        /// <summary>
        /// Radiance High Dynamic Range - .hdr extension.
        /// </summary>
        Hdr = 0x043F,

        /// <summary>
        /// Macintosh Icon - .icns extension.
        /// </summary>
        Icns = 0x0440,

        /// <summary>
        /// Jpeg 2000 - .jp2 extension.
        /// </summary>
        Jp2 = 0x0441,

        /// <summary>
        /// OpenEXR - .exr extension.
        /// </summary>
        Exr = 0x0442,

        /// <summary>
        /// Microsoft HD Photo - .wdp and .hpd extension.
        /// </summary>
        Wdp = 0x0443,

        /// <summary>
        /// Valve Texture Format - .vtf extension.
        /// </summary>
        Vtf = 0x0444,

        /// <summary>
        /// Wireless Bitmap - .wbmp extension.
        /// </summary>
        Wbmp = 0x0445,

        /// <summary>
        /// Sun Raster - .sun, .ras, .rs, .im1, .im8, .im24, im.32 extensions. (wew!)
        /// </summary>
        Sun = 0x0446,

        /// <summary>
        /// Interchange File Format - .iff extension.
        /// </summary>
        Iff = 0x0447,

        /// <summary>
        /// GameCube Texture = .tpl extension.
        /// </summary>
        Tpl = 0x0448,

        /// <summary>
        /// Flexible Image Transport System - .fit and .fits extensions.
        /// </summary>
        Fits = 0x0449,

        /// <summary>
        /// Digital Imaging and Communications in Medicin (DICOM) - .dcm and .dicom extensions.
        /// </summary>
        Dicom = 0x044A,

        /// <summary>
        /// Call of Duty Infinity Ward Image = .iwi extension.
        /// </summary>
        Iwi = 0x044B,

        /// <summary>
        /// Blizzard Texture Format - .blp extension.
        /// </summary>
        Blp = 0x044C,

        /// <summary>
        /// Heavy Metal: FAKK2 Texture - .ftx extension.
        /// </summary>
        Ftx = 0x044D,

        /// <summary>
        /// Homeworld 2 - Relic texture - .rot extension.
        /// </summary>
        Rot = 0x044E,

        /// <summary>
        /// Medieval II : Total War Texture - .texture extension. (original aint it).
        /// </summary>
        Texture = 0x044F,

        /// <summary>
        /// Digital Picture Exchange - .dpx extension.
        /// </summary>
        Dpx = 0x0450,

        /// <summary>
        /// Unreal (and Unreal Tournament) Texture - .utx extension.
        /// </summary>
        Utx = 0x0451,

        /// <summary>
        /// MPEG-1 Audio Layer 3 - .mp3 extension.
        /// </summary>
        Mp3 = 0x0452
    }

    public enum JpgSaveFormat
    {
        Jpg = ILDefines.IL_JPG,
        Jfif = ILDefines.IL_JFIF
    }

    public enum MemoryHint
    {
        Fastest = ILDefines.IL_FASTEST,
        LessMemory = ILDefines.IL_LESS_MEM,
        DontCare = ILDefines.IL_DONT_CARE
    }

    public enum CompressionHint
    {
        UseCompression = ILDefines.IL_USE_COMPRESSION,
        NoCompression = ILDefines.IL_NO_COMPRESSION,
        DontCare = ILDefines.IL_DONT_CARE
    }
    /// <summary>
    /// Enumerates the available error string translations for
    /// <see cref="ILU.GetErrorString"/>.
    /// </summary>
    public enum Language
    {
        English = ILUDefines.ILU_ENGLISH,
        Arabic = ILUDefines.ILU_ARABIC,
        Dutch = ILUDefines.ILU_DUTCH,
        Japanese = ILUDefines.ILU_JAPANESE,
        Spanish = ILUDefines.ILU_SPANISH,
        German = ILUDefines.ILU_GERMAN,
        French = ILUDefines.ILU_FRENCH
    }

    /// <summary>
    /// Enumerates the filter used in <see cref="ILU.Scale"/>. The default
    /// is Nearest.
    /// </summary>
    public enum SamplingFilter
    {
        /// <summary>
        /// Uses a nearest filter to scale the image
        /// </summary>
        Nearest = ILUDefines.ILU_NEAREST,

        /// <summary>
        /// Uses a linear interpolation filter to scale the image
        /// </summary>
        Linear = ILUDefines.ILU_LINEAR,

        /// <summary>
        /// Uses a bilinear (or trilinear for 3D images) interpolation filter
        /// to scale the image (tends to be the best).
        /// </summary>
        Bilinear = ILUDefines.ILU_BILINEAR,

        /// <summary>
        /// Uses a box filter to scale the image.
        /// </summary>
        ScaleBox = ILUDefines.ILU_SCALE_BOX,

        /// <summary>
        /// Uses a triangle filter to scale the image.
        /// </summary>
        ScaleTriangle = ILUDefines.ILU_SCALE_TRIANGLE,

        /// <summary>
        /// Uses a bell filter to scale the image.
        /// </summary>
        ScaleBell = ILUDefines.ILU_SCALE_BELL,

        /// <summary>
        /// Uses a b-spline filter to scale the image.
        /// </summary>
        ScaleBSpline = ILUDefines.ILU_SCALE_BSPLINE,

        /// <summary>
        /// Uses a lanczos filter to scale the image.
        /// </summary>
        ScaleLanczos3 = ILUDefines.ILU_SCALE_LANCZOS3,

        /// <summary>
        /// Uses a mitchell filter to scale the image.
        /// </summary>
        ScaleMitchell = ILUDefines.ILU_SCALE_MITCHELL
    }

    /// <summary>
    /// Enumerates possible image placements for the <see cref="ILU.EnlargeCanvas"/> function.
    /// The default is Center.
    /// </summary>
    public enum Placement
    {
        /// <summary>
        /// Places the image in the lower left of the enlarged canvas.
        /// </summary>
        LowerLeft = ILUDefines.ILU_LOWER_LEFT,

        /// <summary>
        /// Places the image in the lower right of the enlarged canvas.
        /// </summary>
        LowerRight = ILUDefines.ILU_LOWER_RIGHT,

        /// <summary>
        /// Places the image in the upper left of the enlarged canvas.
        /// </summary>
        UpperLeft = ILUDefines.ILU_UPPER_LEFT,

        /// <summary>
        /// Places the image in the upper right of the enlarged canvas.
        /// </summary>
        UpperRight = ILUDefines.ILU_UPPER_RIGHT,

        /// <summary>
        /// Places the image in the center of the enlarged canvas.
        /// </summary>
        Center = ILUDefines.ILU_CENTER
    }
}
