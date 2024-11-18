using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace PCKViewer
{
    public partial class MainForm : Form
    {
        // PCK包内文件列表（文件路径长度，文件路径，文件偏移，文件真实大小，文件物理大小）
        List<Tuple<int, string, long, long, long>> pckList = new List<Tuple<int, string, long, long, long>>();

        /// <summary>
        /// 复位图形界面
        /// </summary>
        private void GUIReset()
        {
            label_ExtractState.Visible = false;
            label_progress.Visible = false;
            btn_ExtractSingle.Enabled = false;
            btn_ExtractAll.Enabled = false;
            btn_Browse.Enabled = false;
            GlobalVar.targetPCK = string.Empty;
            pckList.Clear();
            listView_PCKList.Items.Clear();
            progressBar_Extracted.Value = 0;
            this.Text = string.Empty;
            this.Text = PublicFunction.GetAPPInformation(1) + Convert.ToChar(32) + PublicFunction.GetAPPInformation(2);
        }

        /// <summary>
        /// 打开PCK文件
        /// </summary>
        /// <param name="filePath"></param>
        private void OpenFile(string filePath)
        {
            GUIReset();                                                 // 复位图形界面
            GlobalVar.targetPCK = filePath;                            // 欲获取列表的PCK文件

            // 尝试获取PCK文件列表
            try
            {
                LoadPCK(GlobalVar.targetPCK);                          //载入PCK

                // 载入成功，继续执行                 
                textBox_ExtractAllOutDir.Text = Path.GetDirectoryName(GlobalVar.targetPCK);    // 解压路径输入框解压路径为PCK文件所在路径
                textBox_ExtractAllOutDir.Enabled = true;

                // 配置图形界面
                btn_ExtractSingle.Enabled = true;
                btn_ExtractAll.Enabled = true;
                btn_Browse.Enabled = true;
                label_ExtractState.Visible = true;
                label_ExtractState.Font = new System.Drawing.Font(label_ExtractState.Font, label_ExtractState.Font.Style | System.Drawing.FontStyle.Bold);
                label_ExtractState.ForeColor = System.Drawing.Color.Blue;
                label_ExtractState.Text = "支持的格式，可以解压！(共有" + pckList.Count + "个文件)";
                this.Text += "—— (已打开\"" + GlobalVar.targetPCK + "\")";

                // 播放系统提示音
                SystemSounds.Asterisk.Play();
            }
            catch (Exception ex)                                                     // 捕获LoadPCK抛出的异常，并配置读取失败时候图形界面
            {
                // 配置图形界面
                textBox_ExtractAllOutDir.Text = string.Empty;
                textBox_ExtractAllOutDir.Enabled = false;
                label_ExtractState.Visible = true;
                label_ExtractState.Font = new System.Drawing.Font(label_ExtractState.Font, label_ExtractState.Font.Style | System.Drawing.FontStyle.Bold);
                label_ExtractState.ForeColor = System.Drawing.Color.Red;
                btn_ExtractSingle.Enabled = false;
                btn_ExtractAll.Enabled = false;
                btn_Browse.Enabled = false;

                // 把抛出异常的消息放到列表框显示
                ListViewItem pckItemList = new ListViewItem();
                pckItemList.SubItems.Add(ex.Message);
                this.listView_PCKList.Items.Add(pckItemList);

                //调试输出异常
                Console.WriteLine(ex.Message);
                label_ExtractState.Text = "未知的支持的格式，不可以解压！";

                // 播放系统提示音
                SystemSounds.Hand.Play();

                return;
            }
        }

        /// <summary>
        /// 载入PCK文件
        /// <param name="targetPCK"></param>
        /// </summary>
        private void LoadPCK(string targetPCK)
        {
            try
            {
                // 取PCK文件列表
                pckList = PCKExtractetor.GetPCKInformation(GlobalVar.targetPCK);

                // 开始更新列表框
                this.listView_PCKList.BeginUpdate();
                for (int i = 0; i < pckList.Count; i++)
                {
                    ListViewItem pckItemList = new ListViewItem();
                    pckItemList.Text = (i + 1).ToString();                                          // 序号列
                    pckItemList.SubItems.Add(pckList[i].Item2);                                     // 文件名列
                    pckItemList.SubItems.Add("0x" + pckList[i].Item3.ToString("X"));                // 文件偏移列
                    pckItemList.SubItems.Add(PublicFunction.BytesToSize(pckList[i].Item4));         // 文件实际大小列
                    pckItemList.SubItems.Add(PublicFunction.BytesToSize(pckList[i].Item5));         // 文件压缩后大小列
                    this.listView_PCKList.Items.Add(pckItemList);
                }
                this.listView_PCKList.EndUpdate();
            }
            catch (Exception ex)                                                                    // 当GetPCKInformation抛出异常后要抛给图形界面进行显示
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// 解压全部
        /// <param name="extPath">(文本型 解压缩目标目录)</param>
        /// </summary>
        private async void ExtractetorAll(string extPath)
        {
            // 配置图形界面状态
            label_ExtractState.ResetFont();
            btn_ExtractSingle.Enabled = false;
            btn_ExtractAll.Enabled = false;
            btn_Browse.Enabled = false;
            btn_OpenPCK.Enabled = false;
            textBox_ExtractAllOutDir.Enabled = false;
            listView_PCKList.Enabled = false;

            // 执行解压操作
            await PCKExtractetor.ExtractAllFileAsync(GlobalVar.targetPCK, pckList, extPath, progress =>
            {
                // 在UI线程上更新解压状态
                progressBar_Extracted.Value = progress.Percentage;
                label_ExtractState.ResetForeColor();
                label_ExtractState.Text = "正在解压:\"" + progress.extracting + "\"";
                label_progress.Visible = true;
                label_progress.Text = progress.Percentage.ToString() + "%";
                if (progress.Percentage == 100)
                {
                    label_ExtractState.Text = "解压完成！";
                    label_progress.Visible = false;
                    btn_OpenPCK.Enabled = true;
                    btn_ExtractSingle.Enabled = true;
                    btn_ExtractAll.Enabled = true;
                    btn_Browse.Enabled = true;
                    textBox_ExtractAllOutDir.Enabled = true;
                    listView_PCKList.Enabled = true;

                    // 解压完成后操作，询问是否打开解压文件夹
                    string folderPath = extPath + "\\" + Path.GetFileNameWithoutExtension(GlobalVar.targetPCK) + "_PCKUnpacked";
                    DialogResult r = MessageBox.Show("文件 \"" + Path.GetFileName(GlobalVar.targetPCK) + "\" 已解压到:\n\"" + folderPath + "\" \n是否打开解压的文件夹？", "解压完成", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (r == DialogResult.Yes)
                    {
                        PublicFunction.GetOutputFolder(folderPath);
                    }
                }
            });
        }

        /// <summary>
        /// 初始化窗体
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗口加载完毕响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Form_Load(object sender, EventArgs e)
        {
            // 显示软件名称及版本号
            this.Text = PublicFunction.GetAPPInformation(1) + Convert.ToChar(32) + PublicFunction.GetAPPInformation(2);
        }

        /// <summary>
        /// “打开”按钮单击响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_OpenPCK_Click(object sender, EventArgs e)
        {
            // 当选中文件并点击“打开”按钮的时候执行
            if (openFileDialog_OpenPCK.ShowDialog() == DialogResult.OK)
            {
                OpenFile(openFileDialog_OpenPCK.FileName);
            }
        }

        /// <summary>
        ///  点击“解压选中”按钮的响应函数
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// </summary>
        private void Btn_ExtractSingle_Click(object sender, EventArgs e)
        {
            try
            {
                // 取列表框索引，索引是多少就是文件列表List相应的索引
                int fileNum = listView_PCKList.SelectedIndices[0];

                // 取文件名称作为默认文件名称
                string defaultFileName = Path.GetFileName(pckList[fileNum].Item2);
                saveFileDialog_ExtractSingle.FileName = defaultFileName;

                // 调起对话框
                if (saveFileDialog_ExtractSingle.ShowDialog() == DialogResult.OK)
                {
                    // 开始解压
                    PCKExtractetor.ExtractFileSingle(GlobalVar.targetPCK, pckList, fileNum, saveFileDialog_ExtractSingle.FileName);
                    DialogResult r = MessageBox.Show("解压完成，是否打开解压的文件夹？", "解压完成", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (r == DialogResult.Yes)
                    {
                        string targetPCK = saveFileDialog_ExtractSingle.FileName;
                        PublicFunction.GetOutputFolder(targetPCK);
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("请选择要单独解压的文件！", "未选择文件", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("发生错误，解压失败！\n错误原因:" + ex.Message, "解压错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 点击“解压全部”按钮的响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ExtractAll_Click(object sender, EventArgs e)
        {
            // 当解压路径输入框为空时，不能进行解压操作，并弹出对话框，然后点击一下“浏览”   按钮  
            if (textBox_ExtractAllOutDir.Text != string.Empty)
            {
                ExtractetorAll(textBox_ExtractAllOutDir.Text);
            }
            else
            {
                MessageBox.Show("请选择解压路径！", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Btn_Browse_Click(sender, e);
            }
        }

        /// <summary>
        /// 点击“浏览”按钮的响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Browse_Click(object sender, EventArgs e)
        {
            // 目录选择对话框选中目录为当前打开的PCK所在路径
            folderBrowserDialog_ExtractAll.SelectedPath = Path.GetDirectoryName(GlobalVar.targetPCK);

            // 选中后将选择的路径返回给解压路径输入框
            if (folderBrowserDialog_ExtractAll.ShowDialog() == DialogResult.OK)
            {
                textBox_ExtractAllOutDir.Text = folderBrowserDialog_ExtractAll.SelectedPath;
            }
        }

        /// <summary>
        /// 鼠标单击PCK文件列表框的响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_PCKList_MouseClick(object sender, MouseEventArgs e)
        {
            // 必须在PCK文件列表变量有数据的情况下才能触发
            if (pckList.Count >= 0)
            {
                // 执行右键菜单操作
                if (e.Button == MouseButtons.Right)
                {
                    // 检查文件是否支持浏览
                    string fileType = Path.GetExtension(pckList[listView_PCKList.SelectedIndices[0]].Item2);
                    if (!GlobalVar.PCKViewer.supportedformat.Contains(fileType))
                    {
                        // 不支持浏览，菜单项变灰
                        ToolStripMenuItem_View.Text = "不支持浏览";
                        ToolStripMenuItem_View.Enabled = false;
                    }
                    else
                    {
                        // 支持浏览，菜单项变为可用状态
                        ToolStripMenuItem_View.Text = "浏览选定文件(&V)";
                        ToolStripMenuItem_View.Enabled = true;
                    }

                    // 弹出右键菜单
                    contextMenuStrip_PCKList.Show(listView_PCKList, e.Location);
                }
            }
        }

        /// <summary>
        /// 将文件拖拽并进入到PCK文件列表框的响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_PCKList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy; // 显示复制效果
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// 将文件拖拽并松开鼠标到PCK文件列表框的响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_PCKList_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop); // 获取拖放的文件路径数组
            if (files.Length > 0)
            {
                OpenFile(files[0]);
            }
        }

        /// <summary>
        /// PCK文件列表框双击后响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_PCKList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // 双击列表项可单独解压文件，但必须在PCK文件列表变量有数据的情况下才能触发
            if (pckList.Count >= 0)
            {
                if (e.Button == MouseButtons.Left)
                {
                    try
                    {
                        int fileNum = listView_PCKList.SelectedIndices[0];
                        int load = PCKViewer.ViewResource(GlobalVar.targetPCK, pckList[fileNum].Item2, pckList[fileNum].Item3, pckList[fileNum].Item4, pckList[fileNum].Item5);
                        // 尝试浏览文件，如果返回为0则表示选中的文件不支持浏览，此时询问是否单独解压文件
                        if (load == 0)
                        {
                            DialogResult r = MessageBox.Show("抱歉，该文件不支持浏览！是否解压该文件？", "不支持浏览的文件", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (r == DialogResult.Yes)
                            {
                                Btn_ExtractSingle_Click(sender, e);
                            }
                        }
                    }
                    catch
                    {
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 点击“关闭工具”按钮的响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Exit_Click(object sender, EventArgs e)
        {
            // 执行关闭操作
            this.Close();
        }

        /// <summary>
        /// 点击“版本信息”按钮的响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_About_Click(object sender, EventArgs e)
        {
            Form_About form_About = new Form_About();
            form_About.ShowDialog();
        }

        /// <summary>
        /// 点击“使用说明”按钮的响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Help_Click(object sender, EventArgs e)
        {
            Form_Help form_Help = new Form_Help();
            form_Help.StartPosition = FormStartPosition.CenterScreen;
            form_Help.ShowDialog();
        }

        /// <summary>
        /// 窗口关闭时的响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 询问是否退出
            DialogResult r = MessageBox.Show("确定要退出吗", "退出确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (r == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }
            else
            {
                e.Cancel = false;
            }
        }
    }
}
