namespace PCKViewer
{
    /// <summary>
    /// 全局变量常量
    /// </summary>
    internal class GlobalVar
    {
        // 被操作的PCK文件路径
        public static string targetPCK;

        public class PCKViewer
        {          
            // PCK包内文件格式代码
            public static int formatID = -1;
           
            // 支持浏览的格式
            public static string[] supportedformat = { ".txt", ".gfx", ".mod", ".sdr", ".res", ".bmp", ".tga", ".psd", ".wav", ".mp3" };
        }
    }
}
