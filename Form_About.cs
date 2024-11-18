using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace PCKViewer
{
    public partial class Form_About : Form
    {
        /// <summary>
        /// 初始化窗体
        /// </summary>
        public Form_About()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 点击“OK”按钮响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 窗口载入完毕响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_About_Load(object sender, EventArgs e)
        {
            // 显示软件信息
            Assembly assembly = Assembly.GetExecutingAssembly();
            Icon icon = Icon.ExtractAssociatedIcon(assembly.Location);
            pictureBox_Icon.Image = icon.ToBitmap();           
            label_Title.Text = PublicFunction.GetAPPInformation(1);
            label_Info.Text = PublicFunction.GetAPPInformation(2) + "\n" + PublicFunction.GetAPPInformation(5) + "\n" + "\n" + "开源软件，禁止贩卖！";
        }

        /// <summary>
        /// 点击“软件源代码”标签响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_GitHub_Click(object sender, EventArgs e)
        { 
            // 显示GitHub链接
            System.Diagnostics.Process.Start("https://github.com/xingshen60771/PCKViewer");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form_Debug  Form_Debug =new Form_Debug();
            Form_Debug.ShowDialog();
        }
    }
}
