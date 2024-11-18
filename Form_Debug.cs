
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PCKViewer
{
    public partial class Form_Debug : Form
    {
        public Form_Debug()
        {
            InitializeComponent();
        }

        private void btn_Open_Click(object sender, EventArgs e)
        {

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox_filePath.Text = openFileDialog.FileName;
            }
        }

        private void btn_DeZlib_Click(object sender, EventArgs e)
        {
            byte[] compressionData = File.ReadAllBytes(textBox_filePath.Text);

            byte[] actualData = new byte[81704 * 10];

            try
            {
                actualData = ZLibHelper.DeZlibcompress(compressionData, actualData.Length);

                File.WriteAllBytes(textBox_filePath.Text + ".decompressed", actualData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }







        }

        private void button1_Click(object sender, EventArgs e)
        {










        }
    }
}
