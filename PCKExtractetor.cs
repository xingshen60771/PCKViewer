using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PCKViewer
{
    /// <summary>
    /// PCK解压操作类
    /// </summary>
    internal class PCKExtractetor
    {
        /// <summary>
        /// &lt;List&gt; 取PCK内部文件列表
        /// <param name="pckfilePath">(文本型 欲获取内部文件列表的PCK文件) </param>
        /// <returns><para>成功返回PCK文件的单个文件的目录长度、完整文件路径、文件偏移、实际大小、压缩大小，并封装在 &lt;List&gt;中</para></returns>
        /// </summary>
        public static List<Tuple<int, string, long, long, long>> GetPCKInformation(string pckfilePath)
        {
            // 定义一个五元组的<List>，用于返回文件信息，Tuple元素内容依次为：文件路径长度，文件路径，文件偏移，文件真实大小，文件物理大小
            List<Tuple<int, string, long, long, long>> returnList = new List<Tuple<int, string, long, long, long>>();

            // 定义PCK文件特征区大小
            int pckSignatureSize = 272;

            // 取PACK文件大小
            FileInfo fileInfo = new FileInfo(pckfilePath);
            long pckfileSize = fileInfo.Length;

            // 调试输出PCK文件大小
            Console.WriteLine("PCK文件大小：" + fileInfo.Length + " 字节（十六进制：" + fileInfo.Length.ToString("X") + "）");

            // 开始解析文件
            using (FileStream fs = new FileStream(pckfilePath, FileMode.Open))
            {
                // 取PCK特征区数据
                byte[] pckSignatureByte = new byte[pckSignatureSize];
                fs.Seek(pckSignatureSize * -1, SeekOrigin.End);
                fs.Read(pckSignatureByte, 0, pckSignatureSize);

                // 定义文件魔数
                byte[] magNum = { 0x41, 0x6E, 0x67, 0x65, 0x6C, 0x69, 0x63, 0x61, 0x20, 0x46, 0x69, 0x6C, 0x65, 0x20, 0x50, 0x61, 0x63, 0x6B, 0x61, 0x67, 0x65 };

                // 取目标PCK文件魔数
                byte[] existMagNum = new byte[magNum.Length];
                Array.Copy(pckSignatureByte, 8, existMagNum, 0, existMagNum.Length);

                // 检查是否为PCK文件
                if (!existMagNum.SequenceEqual(magNum))
                {
                    throw new Exception("The file\"" + pckfilePath + "\" is not a valid PCK format file!");
                }

                // 处理数据区物理大小
                byte[] dataSizeBit = new byte[4];      // 字节集状态下的数据区物理大小数值
                long dataSize;                         // 长整型的数据区物理大小数值               
                                                       // 定位到文件长度减去PCK文件特征区大小再减去4个字节的位置

                // 取数据区物理大小数值信息              
                Array.Copy(pckSignatureByte, 4, dataSizeBit, 0, dataSizeBit.Length);
                // 将取到的物理大小数值信息补满八字节，然后转换成长整型数值
                dataSize = BitConverter.ToInt64(PublicFunction.EightByteConverter(dataSizeBit), 0);
                // 调试输出数据区物理大小
                Console.WriteLine("该文件的数据区物理大小为 " + dataSize.ToString() + " 字节（十六进制:" + dataSize.ToString("X") + "）");

                // 处理PCK内部文件文件数量
                byte[] fileCountBit = new byte[4];      // 字节集状态下的数据区物理大小数值
                long fileCount;                         // 长整型的数据区物理大小数值
                // 取PCK内部文件数量信息
                Array.Copy(pckSignatureByte, 264, fileCountBit, 0, fileCountBit.Length);
                // 将取到的PCK内部文件信息补满八字节，然后转换成长整型数值
                fileCount = BitConverter.ToInt64(PublicFunction.EightByteConverter(fileCountBit), 0);
                // 调试输出包内文件数量
                Console.WriteLine("该PCK文件包应有 " + fileCount.ToString() + " 个文件（十六进制:" + fileCount.ToString("X") + "）");

                // 开始处理目录区             
                byte[] fileIdxByte = new byte[pckfileSize - dataSize - pckSignatureSize];        // 文件索引数据
                // 定位到文件索引区的首个字节，即数据区物理大小的值
                fs.Seek(dataSize, SeekOrigin.Begin);
                // 开始填充文件索引数据
                fs.Read(fileIdxByte, 0, fileIdxByte.Length);
                // 调试输出文件索引数据大小
                Console.WriteLine("该PCK文件包的文件索引大小为 " + fileIdxByte.Length.ToString() + " 字节（十六进制:" + fileIdxByte.Length.ToString("X") + "）");

                // 开始创建文件列表
                int byteOffset = 0;        //字节偏移记录

                // 开始for循环，循环次数为文件数量
                for (int i = 0; i < fileCount; i++)
                {
                    // 取文件目录文本长度
                    byte[] filePathLengthBit = new byte[4];
                    // 从当前偏移位置开始拷贝四个字节
                    Array.Copy(fileIdxByte, byteOffset, filePathLengthBit, 0, filePathLengthBit.Length);
                    // 四字节补到八字节，再转换成整型数值
                    int filePathLength = BitConverter.ToInt32(PublicFunction.EightByteConverter(filePathLengthBit), 0);
                    //字节偏移移到目录的第一个字符
                    byteOffset += 4;

                    // 取文件目录，因为所有目录文本字节后面都填充了一个00，因此要减1
                    byte[] filePathStringBit = new byte[filePathLength - 1];
                    // 从当前的字节偏移位置拷贝文件目录文本字节数据，文本长度变量filePathLength来决定
                    Array.Copy(fileIdxByte, byteOffset, filePathStringBit, 0, filePathStringBit.Length);
                    // 将文件目录文本字节数据转换为文本型数据，包内文件名可能含有中文，需使用.Net框架提供的默认编码，否则中文部分会乱码
                    string filePathString = Encoding.Default.GetString(filePathStringBit);
                    //字节偏移移到当前文件偏移的字节
                    byteOffset += filePathLength;

                    // 取文件偏移
                    byte[] fileOffsetBit = new byte[4];
                    // 从当前偏移位置开始拷贝四个字节
                    Array.Copy(fileIdxByte, byteOffset, fileOffsetBit, 0, fileOffsetBit.Length);
                    // 四字节补到八字节，再转换成长整型数值，
                    long fileOffset = BitConverter.ToInt64(PublicFunction.EightByteConverter(fileOffsetBit), 0);
                    //字节偏移到文件实际大小的字节
                    byteOffset += 4;

                    // 取文件实际大小
                    byte[] fileActualsizeBit = new byte[4];
                    // 从当前偏移位置开始拷贝四个字节
                    Array.Copy(fileIdxByte, byteOffset, fileActualsizeBit, 0, fileActualsizeBit.Length);
                    // 四字节补到八字节，再转换成长整型数值，
                    long fileActualsize = BitConverter.ToInt64(PublicFunction.EightByteConverter(fileActualsizeBit), 0);
                    //字节偏移到文件压缩后大小的字节                 
                    byteOffset += 4;

                    // 取文件压缩大小
                    byte[] filecompressionsizeBit = new byte[4];
                    // 从当前偏移位置开始拷贝四个字节
                    Array.Copy(fileIdxByte, byteOffset, filecompressionsizeBit, 0, filecompressionsizeBit.Length);
                    // 四字节补到八字节，再转换成长整型数值，                    
                    long filecompressionsize = BitConverter.ToInt64(PublicFunction.EightByteConverter(filecompressionsizeBit), 0);
                    //字节偏移到下一个文件长度信息字节，为下一次循环做准备                      
                    byteOffset += 4;

                    // 将获取到的信息添加到五元组的<List>returnList
                    returnList.Add(Tuple.Create(filePathLength, filePathString, fileOffset, fileActualsize, filecompressionsize));
                }
                // 关掉文件流以节约内存
                fs.Close();
            }

            // 循环结束将五元组的<List>returnList返回
            return returnList;
        }


        /// <summary>
        /// 解压单独文件
        /// <param name="pckfilePath">(文本型 欲单独解压的PCK文件, </param>
        /// <param name="fileList">List 已处理好的文件列表, </param>
        /// <param name="targetNum">(欲解压的文件顺序号, </param>
        /// <param name="savePath">欲保存的路径)</param>
        /// </summary>
        public static void ExtractFileSingle(string pckfilePath, List<Tuple<int, string, long, long, long>> fileList, int targetNum, string savePath)
        {
            // 取单独的文件名
            string fileName = Path.GetFileName(fileList[targetNum].Item2);

            // 开始获取指定文件的物理数据
            long pckOffset = fileList[targetNum].Item3;               //PCK文件偏移
            long actualDataSize = fileList[targetNum].Item4;          // 指定文件的真实大小
            long compressionDataSize = fileList[targetNum].Item5;     // 指定文件的物理大小
            byte[] actualData = new byte[actualDataSize];                                          // 指定文件的物理数据字节集数组
            byte[] compressionData = new byte[compressionDataSize];     // 指定文件的物理数据字节集数组

            // 将物理数据拷贝到文件的物理数据字节集数组中

            using (FileStream fs = new FileStream(pckfilePath, FileMode.Open))
            {
                fs.Seek(pckOffset, SeekOrigin.Begin);
                fs.Read(compressionData, 0, (int)compressionDataSize);
                // 由于极少数文件未压缩，将导致ZLib抛出数据无法识别的异常，因此需要加判断，判断依据为检查压缩前后大小
                if (actualDataSize == compressionDataSize)
                {
                    actualData = compressionData;
                }
                else
                {
                    try
                    {
                        actualData = ZLibHelper.DeZlibcompress(compressionData, actualDataSize);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Zlib decompression error:" + ex.Message);

                    }
                }
                File.WriteAllBytes(savePath, actualData);
                // 关掉文件流以节约内存
                fs.Close();
            }




        }

        /// <summary>
        /// &lt;List&gt; 解压所有文件(非异步)
        /// <param name="pckfilePath">(文本型 欲单解压的PCK文件, </param>
        /// <param name="fileList">List 已处理好的文件列表, </param>
        /// <param name="savePath">欲保存的路径)</param>
        /// </summary>
        public static void ExtractAllFile(string pckfilePath, List<Tuple<int, string, long, long, long>> fileList, string savePath)
        {
            // 取文件数量，即List成员数
            int fileCount = fileList.Count;

            // 设置解压文件夹，保存到文件名+_PCKUnpacked中
            string extractFolder = Path.GetFileNameWithoutExtension(pckfilePath) + "_PCKUnpacked";
            Directory.CreateDirectory(extractFolder);

            //开始解压操作
            using (FileStream fs = new FileStream(pckfilePath, FileMode.Open))
            {
                // 进入for循环
                for (int i = 0; i < fileCount; i++)
                {
                    // 取文件名和文件路径
                    string filePath = fileList[i].Item2;
                    string fileDir = Path.GetDirectoryName(filePath);
                    string fileName = Path.GetFileName(filePath);

                    // 置该文件最终绝对路径
                    string finalPath = savePath + "\\" + extractFolder + "\\" + filePath;
                    long pckOffset = fileList[i].Item3;                     //PCK文件偏移
                    long actualDataSize = fileList[i].Item4;                // 指定文件的真实大小
                    long compressionDataSize = fileList[i].Item5;           // 指定文件的物理大小
                    byte[] actualData = new byte[actualDataSize];                      // 指定文件的物理数据字节集数组
                    byte[] compressionData = new byte[compressionDataSize]; // 指定文件的物理数据字节集数组
                    fs.Seek(pckOffset, SeekOrigin.Begin);
                    fs.Read(compressionData, 0, (int)compressionDataSize);
                    // 由于极少数文件未压缩，将导致ZLib抛出数据无法识别的异常，因此需要加判断，判断依据为检查压缩前后大小
                    if (actualDataSize == compressionDataSize)
                    {
                        actualData = compressionData;
                    }
                    else
                    {
                        try
                        {

                            actualData = ZLibHelper.DeZlibcompress(compressionData, actualDataSize);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Zlib解压错误！错误代码:\n" + ex.Message, "解压错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }

                    // 检查目录是否存在，不存在则创建
                    if (!Directory.Exists(Path.GetDirectoryName(finalPath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(finalPath));
                    }

                    // 此轮工作已结束，写到文件
                    File.WriteAllBytes(finalPath, actualData);
                }
                // 关掉文件流以节约内存
                fs.Close();
            }
        }


        /// <summary>
        /// 文件解压状态输出
        /// </summary>
        public class FileExtractProgress
        {
            public int Percentage { get; set; }
            public string extracting { get; set; }
        }

        /// <summary>
        /// &lt;List&gt; 解压所有文件(非异步)
        /// <param name="pckfilePath">(文本型 欲单解压的PCK文件, </param>
        /// <param name="fileList">List 已处理好的文件列表, </param>
        /// <param name="savePath">欲保存的路径)</param>
        /// </summary>
        public static async Task ExtractAllFileAsync(string pckfilePath, List<Tuple<int, string, long, long, long>> targetFile, string savePath, Action<FileExtractProgress> progressCallback)
        {
            // 取文件数量，即List成员数
            int fileCount = targetFile.Count;
            string extractFolder = Path.GetFileNameWithoutExtension(pckfilePath) + "_PCKUnpacked";
            //Directory.CreateDirectory(extractFolder);
            using (FileStream fs = new FileStream(pckfilePath, FileMode.Open))
            {
                for (int i = 0; i < fileCount; i++)
                {
                    // 取文件名和文件路径
                    string filePath = targetFile[i].Item2;
                    string fileDir = Path.GetDirectoryName(filePath);
                    string fileName = Path.GetFileName(filePath);
                    string finalPath = savePath + "\\" + extractFolder + "\\" + filePath;
                    long pckOffset = targetFile[i].Item3; // PCK文件偏移
                    long actualDataSize = targetFile[i].Item4; // 指定文件的真实大小
                    long compressionDataSize = targetFile[i].Item5; // 指定文件的物理大小
                    byte[] actualData = new byte[actualDataSize]; // 指定文件的物理数据字节集数组
                    byte[] compressionData = new byte[compressionDataSize]; // 指定文件的物理数据字节集数组

                    fs.Seek(pckOffset, SeekOrigin.Begin);
                    await fs.ReadAsync(compressionData, 0, (int)compressionDataSize);
                    // 由于极少数文件未压缩，将导致ZLib抛出数据无法识别的异常，因此需要加判断，判断依据为检查压缩前后大小
                    if (actualDataSize == compressionDataSize)
                    {
                        actualData = compressionData;
                    }
                    else
                    {
                        try
                        {
                            actualData = ZLibHelper.DeZlibcompress(compressionData, actualDataSize);
                        }
                        catch (Exception ex)
                        {
                            ErrorRecorder.WriteErrLog(savePath + "\\" + extractFolder, filePath, ex.Message);
                            MessageBox.Show("Zlib解压错误！\n无法解压 \"" + filePath + " \"\n错误代码:" + ex.Message + "\n错误以记载到日志文件", "解压错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    // 检查目录是否存在，不存在则创建
                    if (!Directory.Exists(Path.GetDirectoryName(finalPath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(finalPath));
                    }

                    File.WriteAllBytes(finalPath, actualData);

                    // 计算并报告进度
                    int percent = (int)(((i + 1) / (double)fileCount) * 100);
                    progressCallback(new FileExtractProgress { Percentage = percent, extracting = filePath });

                }

                // 关掉文件流以节约内存
                fs.Close();
            }
        }
    }
}

