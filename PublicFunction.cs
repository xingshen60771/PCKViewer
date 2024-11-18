using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;


namespace PCKViewer
{
    /// <summary>
    /// 公共函数
    /// </summary>
    internal class PublicFunction
    {
        ///<summary>
        /// &lt;文本型&gt; 取软件基本信息 
        /// <param name="paramcode">(整数型 要获取的信息代码)<para>参数代码含义:</para>1：取软件名称；2:取软件版本；3:取软件开发者；4、取软件产品名称。<para></para></param>
        /// <returns><para></para>返回文本型基本信息结果，失败使用了除1-4以外的参数则返回"InvalidRequest"。</returns> 
        /// </summary>
        public static string GetAPPInformation(int paramcode)
        {
            //取程序名称、版本、公司、产品名称、版权信息
            Assembly asm = Assembly.GetExecutingAssembly();           
            AssemblyTitleAttribute asmTitle = (AssemblyTitleAttribute)Attribute.GetCustomAttribute(asm, typeof(AssemblyTitleAttribute));
            Version asmVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            AssemblyCompanyAttribute asmCompany = (AssemblyCompanyAttribute)Attribute.GetCustomAttribute(asm, typeof(AssemblyCompanyAttribute));
            AssemblyProductAttribute asmProduct = (AssemblyProductAttribute)Attribute.GetCustomAttribute(asm, typeof(AssemblyProductAttribute));
            AssemblyCopyrightAttribute asmCopyRight = (AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(asm, typeof(AssemblyCopyrightAttribute));
            
            // 将获取接管转为文本型
            string version = "Ver " + asmVersion.ToString();
            string title = asmTitle.Title;
            string company = asmCompany.Company;
            string appEnglishNane = asmProduct.Product;
            string copyRight = asmCopyRight.Copyright.ToString();

            // 参数接到不同代码返回不同的信息
            if (paramcode == 1)                 // 返回软件名称
            {
                return title;
            }
            else if (paramcode == 2)            // 返回软件版本
            {
                return version;
            }
            else if (paramcode == 3)            // 返回软件公司
            {
                return company;
            }
            else if (paramcode == 4)            // 返回软件产品名称
            {
                return appEnglishNane;
            }
            else if (paramcode == 5)            // 返回版权信息
            {
                return copyRight;
            }
            else                                // 输入其他字符则返回"InvalidRequest"
            {
                return "InvalidRequest";
            }
        }

        /// <summary>
        ///  &lt;字节集&gt; 8字节补零
        /// <param name="bytes">(字节集 欲填充0x00的8字节字节集数组)</param>
        /// <returns><para>返回处理后的8字节数组</para></returns>
        /// </summary>
        public static byte[] EightByteConverter(byte[] bytes)
        {
            // 补到8字节用于Bit转换
            byte[] newByees = new byte[8];
            Array.Copy(bytes, 0, newByees, 0, bytes.Length);
            for (int i = bytes.Length; i < 8; i++)
            {
                newByees[i] = 0x00; // 补充0x00
            }
            // 将处理完的字节集数组交还给bytes变量
            return newByees;
        }

        /// <summary>
        /// &lt;文本型&gt; 字节大小数值转换
        /// <param name="bytes">(长整型 欲转换的字节大小数组)</param>
        /// <returns><para>成功返回字节大小</para></returns>
        /// </summary>
        public static string BytesToSize(long size)
        {
            var num = 1024.00; //byte
            if (size < num)
                return size + " Byte";
            if (size < Math.Pow(num, 2))
                return (size / num).ToString("f2") + " KB";
            if (size < Math.Pow(num, 3))
                return (size / Math.Pow(num, 2)).ToString("f2") + " MB";
            if (size < Math.Pow(num, 4))
                return (size / Math.Pow(num, 3)).ToString("f2") + " GB";
            if (size < Math.Pow(num, 5))
                return (size / Math.Pow(num, 4)).ToString("f2") + " TB";
            if (size < Math.Pow(num, 6))
                return (size / Math.Pow(num, 5)).ToString("f2") + " PB";
            if (size < Math.Pow(num, 7))
                return (size / Math.Pow(num, 6)).ToString("f2") + " EB";
            if (size < Math.Pow(num, 8))
                return (size / Math.Pow(num, 7)).ToString("f2") + " ZB";
            if (size < Math.Pow(num, 9))
                return (size / Math.Pow(num, 8)).ToString("f2") + " YB";
            if (size < Math.Pow(num, 10))
                return (size / Math.Pow(num, 9)).ToString("f2") + "DB";
            return (size / Math.Pow(num, 10)).ToString("f2") + "NB";
        }

        /// <summary>
        /// 定位输出文件夹(文本型 欲定位的路径)
        /// </summary>
        /// <param name="path"></param>
        public static void GetOutputFolder(string path)
        {
            try
            {
                // 创建一个ProcessStartInfo对象来配置进程启动的信息
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    // 要定位和打开的文件夹路径
                    FileName = "explorer.exe",
                    Arguments = $"/e,/select, \"{path}\""
                };
                startInfo.UseShellExecute = true;
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                // 处理可能出现的异常
                Console.WriteLine($"无法打开文件夹: {ex.Message}");
            }
        }

        /// <summary>
        /// &lt;Font&gt; 修改字号
        /// <param name="font">(Font 欲修改的Font, </param>
        /// <param name="newSize">整数型 欲修改的大小)</param>
        /// <returns>成功返回修改后的Font</returns>
        /// </summary>
        public static Font ChangeFontSize(Font font,int newSize)
        {
            // 获取当前的字体信息
            Font currentFont = font;
            // 创建一个新的字体，修改大小，保持其他属性不变
            Font newFont = new Font(currentFont.FontFamily, newSize, currentFont.Style);
            // 将新字体赋给TextBox的Font属性
            return newFont;
        }

        /// <summary>
        /// &lt;逻辑型&gt; 窗体是否存在
        /// <param name="formType">(Type 欲查找的窗体名称)</param>
        /// <returns><para>存在返回真。否则返回假</para></returns>
        /// </summary>
        public static bool CheckFormExists(Type formType)
        {
            // 遍历已打开窗体
            foreach (Form openForm in Application.OpenForms)
            {
                // 找到指定窗体，返回真
                if (openForm.GetType() == formType)
                {
                    return true;
                }
            }

            // 未找到，返回假
            return false;
        }
    }
}

