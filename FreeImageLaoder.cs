using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace PCKViewer
{
    /// <summary>
    /// FreeImage图像加载类
    /// </summary>
    internal class FreeImageLaoder
    {
        /// <summary>
        /// &lt;Bitmap&gt; 载入图像
        /// <param name="imgByte">(字节集 欲载入的图像, </param>
        /// <param name="imgFormat">整数型 图像格式代码)</param>
        /// <returns><para>成功返回Bitmap数据</para></returns>
        /// <exception cref="Exception"><para>失败则抛出异常</para></exception>
        /// <para>格式代码：</para>
        /// <para>1：Unknown；0：Windows位图 BMP；1：Windows图标 ICO；2：JPEG图像；3：JPEG网络图像；4：KOALA图像；5：LBM图像；5：IFF图像；6：MNG图像；7：PBM图像；8：PBMEAW图像；9：PCD图像；10：PCX图像；11：PGM图像；12：PGMRAW图像；13：可移植图像PNG；14：PPM图像；15：PPMRAW图像；16：RAS图像；17：TGA图像；18：标签图像TIFF；19：Wireless位图WBMP；20：Photoshop文档PSD；21：CUT图像；22：XBM图像；23：XPM图像；24：DirectX纹理图像DDS；25：GIF动态图；26：高动态范围图像HDR；27：FAXG3图像；28：SGI图像；29：EXR图像；30：JPEG2000图像J2K；31：JPEG2000图像JP2；32：PFM图像；33：PICT图像；34：RAW图像。</para>
        /// <param><para>注意：切勿使用参数-1！即便不知道图像格式也不准使用！</para>
        /// </param>
        /// </summary>
        public static Bitmap LaodImage(byte[] imgByte, int imgFormat)
        {
            try
            {
                // 转为FreeImage数据流
                IntPtr unmanagedBuffer = Marshal.AllocHGlobal(imgByte.Length);
                Marshal.Copy(imgByte, 0, unmanagedBuffer, imgByte.Length);

                // 载入图像
                FreeImage.FIMEMORY memory = FreeImage.OpenMemory(unmanagedBuffer, (uint)imgByte.Length);
                FreeImage.FIBITMAP dib = FreeImage.LoadFromMemory((FreeImage.FREE_IMAGE_FORMAT)imgFormat, memory, 0);

                // 转为.Net位图
                Bitmap bitmap = FreeImage.GetBitmap(dib);
                return bitmap;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // 以下为专门针对某种特定格式的图像转换的方法，实际未使用，仅用于项目调试使用


        /// <summary>
        /// &lt;Bitmap&gt; 载入TGA图像
        /// <param name="tgaByte">(字节集 欲载入的图像)</param>
        /// <returns><para>成功返回Bitmap数据</para></returns>
        /// <exception cref="Exception"><para>失败则抛出异常</para></exception>
        /// </summary>
        public static Bitmap LaodTARGA(byte[] tgaByte)
        {
            try
            {
                IntPtr unmanagedBuffer = Marshal.AllocHGlobal(tgaByte.Length);
                Marshal.Copy(tgaByte, 0, unmanagedBuffer, tgaByte.Length);
                FreeImage.FIMEMORY memory = FreeImage.OpenMemory(unmanagedBuffer, (uint)tgaByte.Length);
                FreeImage.FIBITMAP dib = FreeImage.LoadFromMemory(FreeImage.FREE_IMAGE_FORMAT.FIF_TARGA, memory, 0);
                Bitmap bitmap = FreeImage.GetBitmap(dib);
                return bitmap;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// &lt;Bitmap&gt; 载入BMP图像
        /// <param name="bmpByte">(字节集 欲载入的图像)</param>
        /// <returns><para>成功返回Bitmap数据</para></returns>
        /// <exception cref="Exception"><para>失败则抛出异常</para></exception>
        /// </summary>
        public static Bitmap LaodBITMAP(byte[] bmpByte)
        {
            try
            {
                IntPtr unmanagedBuffer = Marshal.AllocHGlobal(bmpByte.Length);
                Marshal.Copy(bmpByte, 0, unmanagedBuffer, bmpByte.Length);
                FreeImage.FIMEMORY memory = FreeImage.OpenMemory(unmanagedBuffer, (uint)bmpByte.Length);
                FreeImage.FIBITMAP dib = FreeImage.LoadFromMemory(FreeImage.FREE_IMAGE_FORMAT.FIF_BMP, memory, 0);
                Bitmap bitmap = FreeImage.GetBitmap(dib);
                return bitmap;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// &lt;Bitmap&gt; 载入PSD图像
        /// <param name="psdByte">(字节集 欲载入的图像)</param>
        /// <returns><para>成功返回Bitmap数据</para></returns>
        /// <exception cref="Exception"><para>失败则抛出异常</para></exception>
        /// </summary>
        public static Bitmap LaodPSD(byte[] psdByte)
        {
            try
            {

                IntPtr unmanagedBuffer = Marshal.AllocHGlobal(psdByte.Length);
                Marshal.Copy(psdByte, 0, unmanagedBuffer, psdByte.Length);
                FreeImage.FIMEMORY memory = FreeImage.OpenMemory(unmanagedBuffer, (uint)psdByte.Length);
                FreeImage.FIBITMAP dib = FreeImage.LoadFromMemory(FreeImage.FREE_IMAGE_FORMAT.FIF_PSD, memory, 0);
                Bitmap bitmap = FreeImage.GetBitmap(dib);
                return bitmap;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}








