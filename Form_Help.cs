using NAudio.Wave;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PCKViewer
{
    partial class Form_Help : Form
    {
        /// <summary>
        /// 初始化窗体
        /// </summary>
        public Form_Help()
        {
            InitializeComponent();
        }

        // 用于接收资源浏览器的帮助文本
        public string resViewHelpText;

        /// <summary>
        /// 窗口载入完毕响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Help_Load(object sender, EventArgs e)
        {
            // 是否为资源浏览器窗体打开的本窗体
            bool areView = PublicFunction.CheckFormExists(typeof(Form_ResView));        //枚举资源浏览器窗体Form_ResView
            if (!areView)
            {
                textBox_Help.Text = Properties.Resources.help_Main;
            }
            else
            {
                textBox_Help.Text = resViewHelpText;
            }
            btn_OK.Focus();
        }

        /// <summary>
        /// 点击“OK”按钮响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_OK_Click(object sender, EventArgs e)
        {
            this.Close();       //关闭窗体
        }

    }
}
