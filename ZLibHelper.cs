using System;
using System.Runtime.InteropServices;

namespace PCKViewer
{

    /// <summary>
    /// Zlib类
    /// </summary>
    internal class ZLibHelper
    {
        // 将zlib1.dll的用于解压Zlib数据的API引入到本工程
        [DllImport("zlib1.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int uncompress(byte[] dest, ref int destLen, byte[] source, int sourceLen);

        /// <summary>
        /// &lt;字节集型&gt; 解压缩Zlib数据到字节集数组
        /// <param name="data">(字节集型 欲解压缩的数据 ,</param>
        /// <param name="destLen">长整型 应解压大小)</param>
        /// <returns>成功返回压缩后的字节集数组，失败返回NULL。</returns>
        /// <exception cref="Exception"><para>失败则抛出带错误码的异常</para></exception>
        ///<para>错误码描述：</para>
        ///<para>1、Z_STREAM_ERROR(错误码:-2)：表示文件流异常。</para>
        ///<para>2、Z_DATA_ERROR(错误码:-3)：表示被解压的数据有错误。所谓的数据错误可能是非Zlib数据、数据损坏、校验不正确等客观因素造成的。</para>
        ///<para>3、Z_MEM_ERROR(错误码:-4)：表示内存不足。</para>
        ///<para>5、Z_BUF_ERROR(错误码:5)：表示分配的缓存区大小不足。</para>
        ///<para>6、Z_VERSION_ERROR(错误码:-6）：表示被解压的数据与Zlib版本不兼容。</para>
        /// </summary>
        public static byte[] DeZlibcompress(byte[] data, long destLen)
        {
            // 获取压缩后大小
            int compressSize = data.Length;
            // 获取应解压大小
            int actualsize = (int)destLen;

            // 创建用于存放解压后数据的字节集数组
            byte[] returnData = new byte[destLen];

            // 创建一个记录解压结果状态码的整型变量并绑定解压操作
            int decState = uncompress(returnData, ref actualsize, data, compressSize);

            // 只有返回0才是解压成功，返回其他数值均为解压异常
            if (decState == 0)
            {
                return returnData;
            }
            else
            {
                throw new Exception(decState.ToString());           //将错误码抛出
            }
        }
    }
}

