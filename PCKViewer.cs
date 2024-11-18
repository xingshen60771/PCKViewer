using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
// 定义委托事件
public delegate void LaodRes(byte[] byteData, string filePath, int formatID);

namespace PCKViewer
{
    /// <summary>
    /// 资源浏览操作类
    /// </summary>
    internal class PCKViewer
    {
        /// <summary>
        /// &lt;整数型&gt; 载入资源文件
        /// <param name="pckfilePath">文本型 欲载入的PCK文件, </param>
        /// <param name="filePath">文本型 PCK包内字文件路径及子文件名, </param>
        /// <param name="pckOffset">长整型 欲载入的子文件偏移, </param>
        /// <param name="fileActualsize">长整型 欲载入的子文件压缩前大小, </param>
        /// <param name="fileCompressionsize">长整型 欲载入的子文件压缩后大小)</param>
        /// <returns><para>成功返回1，失败返回其他数值</para></returns>
        /// <param><para>0：格式不支持；-1：Zlib解压异常；-2：资源浏览窗口重复打开</para></param>
        /// </summary>
        public static int ViewResource(string pckfilePath, string filePath, long pckOffset, long fileActualsize, long fileCompressionsize)
        {
            try
            {
                // 判断是否为支持浏览的文件
                string fileFormat = Path.GetExtension(filePath);
                if (!GlobalVar.PCKViewer.supportedformat.Contains(fileFormat))
                {
                    //MessageBox.Show("抱歉，该文件不支持浏览！", "不支持浏览的文件", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return 0;       // 不支持返回0

                }

                byte[] actualData = new byte[fileActualsize];               // 指定文件的物理数据字节集数组
                byte[] compressionData = new byte[fileCompressionsize];     // 指定文件的物理数据字节集数组

                // 先解压缩数据，操作方法与PCK解压相同，解压失败弹出提示并返回-1                                                             
                try
                {
                    using (FileStream fs = new FileStream(pckfilePath, FileMode.Open))
                    {
                        fs.Seek(pckOffset, SeekOrigin.Begin);
                        fs.Read(compressionData, 0, (int)fileCompressionsize);

                        if (fileActualsize == fileCompressionsize)
                        {
                            actualData = compressionData;
                        }
                        else
                        {
                            actualData = ZLibHelper.DeZlibcompress(compressionData, fileActualsize);
                        }
                        fs.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("无法打开文件\"" + filePath + "\"！\n此文件可能为非标准Zlib格式压缩数据，或者文件被补丁过而无法识别！\n错误码:" + ex.Message, "打开文件遇到问题", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }



                // 开始处理文本
                string[] validTextFormat = { ".txt", ".gfx", ".mod", ".sdr", ".res" };
                if (validTextFormat.Contains(fileFormat))
                {
                    GlobalVar.PCKViewer.formatID = 0;
                }

                // 开始处理BMP
                string[] validImageFormat = { ".bmp", ".tga", ".psd" };
                if (validImageFormat.Contains(fileFormat))
                {
                    GlobalVar.PCKViewer.formatID = 1;
                }

                // 开始处理音频
                string[] validSoundFormat = { ".wav", ".mp3" };
                if (validSoundFormat.Contains(fileFormat))
                {
                    GlobalVar.PCKViewer.formatID = 2;
                }

                // 一次只能打开一个资源浏览窗体
                foreach (Form openForm in Application.OpenForms)
                {
                    if (openForm.GetType() == typeof(Form_ResView))
                    {
                        MessageBox.Show("抱歉！必须关闭资源文件浏览窗口才能浏览其他文件！", "请关闭浏览窗口", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        openForm.Activate();
                        return -2;
                    }
                }


                // 创建资源浏览窗体，并通过委托的方式传入要浏览的资源
                Form_ResView Form_ResView = new Form_ResView();
                LaodRes callLaodRes = new LaodRes(Form_ResView.LaodRes);
                callLaodRes(actualData, filePath, GlobalVar.PCKViewer.formatID);

                // 显示资源浏览窗体
                Form_ResView.Show();
                
                return 1;
            }
            catch (Exception ex)
            {
                // 发生错误时捕捉错误消息
                MessageBox.Show(ex.Message);
                return -1;
            }
        }
    }
}
