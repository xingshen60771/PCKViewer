using NAudio.Wave;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace PCKViewer
{


    partial class Form_ResView : Form
    {
        // 另存为字节集
        byte[] saveData;
        // 默认图片保存过滤器
        string defaultImgFilter = "JPEG 图像|*.jpg|可移植网络图像|*.png|GIF 格式图片|*.gif|";

        // 图像大小(用于控制图片放大缩小)
        int imgorginWidth;
        int imgorginHeight;

        // 定义音频播放流
        WaveStream waveStream;
        WaveOutEvent outputDevice;

        // 播放、暂停、停止按钮字符
        char sndPlay = '\u25B6';
        char sndPause = '\u23F8';
        char sndStop = '\u25A0';

        /// <summary>
        /// 初始化窗体
        /// </summary>
        public Form_ResView()
        {
            InitializeComponent();

        }

        /// <summary>
        /// 载入资源
        /// <param name="byteData">(字节集 欲载入的资源, </param>
        /// <param name="filePath">文本型 资源在PCK文件内的路径, </param>
        /// <param name="formatID">整数型 资源类型)</param>
        /// </summary>
        public void LaodRes(byte[] byteData, string filePath, int formatID)
        {
            // 置窗体标题名称为当前PCK文件名、被浏览的文件名
            this.Text = "正在浏览:  " + Path.GetFileName(GlobalVar.targetPCK) + " -> " + filePath;

            // 置文件大小信息
            string fileSize = "文件大小: " + PublicFunction.BytesToSize(byteData.Length) + "（" + byteData.Length.ToString("N0", CultureInfo.InvariantCulture) + "字节)";

            // 文本类数据处理
            if (formatID == 0)
            {
                // 列出五种文本到文本型数组
                string[] textType = { "TXT文本文件", "特效配置文件", "模型配置文件", "着色器文件", "资源配置文件" };

                // 置窗体大小为800*600
                this.Size = new Size(800, 600);

                // 文本显示框控件并填满整个窗体
                textBox.Visible = true;
                textBox.Dock = DockStyle.Fill;

                // 将字节集数据载入到文本框
                textBox.Text = Encoding.Default.GetString(byteData);

                // 置文本框光标到文本尾部以防默认全选
                textBox.SelectionStart = textBox.Text.Length;

                // 置状态栏第一栏位为文本类型和另存为对话框的过滤器
                switch (Path.GetExtension(filePath))
                {
                    case ".txt":
                        toolStripStatusLabel_FileType.Text = textType[0];
                        saveFileDialog.DefaultExt = "txt";
                        saveFileDialog.Filter = textType[0] + "|*.txt";
                        break;
                    case ".gfx":
                        toolStripStatusLabel_FileType.Text = textType[1];
                        saveFileDialog.Filter = textType[0] + "|*.txt|" + textType[1] + "|*.gfx";       //非TXT文本均可保存为TXT，其他文件格式的判断也是如此，以此类推
                        saveFileDialog.DefaultExt = "gfx";                                              //默认保存格式为原格式，以此类推
                        break;
                    case ".mod":
                        toolStripStatusLabel_FileType.Text = textType[2];
                        saveFileDialog.Filter = textType[0] + "|*.txt|" + textType[2] + "|*.mod";
                        saveFileDialog.DefaultExt = "mod";
                        break;
                    case ".sdr":
                        toolStripStatusLabel_FileType.Text = textType[3];
                        saveFileDialog.Filter = textType[0] + "|*.txt|" + textType[3] + "|*.sdr";
                        saveFileDialog.DefaultExt = "sdr";
                        break;
                    case ".res":
                        toolStripStatusLabel_FileType.Text = textType[4];
                        saveFileDialog.Filter = textType[0] + "|*.txt|" + textType[4] + "|*.res";
                        saveFileDialog.DefaultExt = "res";
                        break;
                }

                // 置默认文件名
                saveFileDialog.FileName = filePath;

                // 另存为字节集就是原始文件的字节集
                saveData = byteData;

                // 置状态栏第二栏位为文本行数
                toolStripStatusLabel_Parameter.Text = "共有 " + textBox.Lines.Length.ToString() + " 行";

                // 置状态栏第三栏为文件大小
                toolStripStatusLabel_FileSize.Text = fileSize;

                // 启用默认字号的字体和护眼模式
                FontSizeDefault();
                EPmode();

                // 显示菜单项“护眼模式”和“字号”
                toolStripMenuItem_FontSize.Visible = true;
                toolStripMenuItem_EPmode.Visible = true;
            }

            // 处理图像数据
            if (formatID == 1)
            {
                // 位图数据默认为空
                Bitmap img = null;

                // 列出三种图像到文本型数组
                string[] imageType = { "Windows位图", "TGA图像", "Photoshop文档" };

                // 按图像格式载入图像并转为Bitmap数据，然后置状态栏第一栏位为文本类型
                switch (Path.GetExtension(filePath))
                {
                    case ".bmp":
                        img = FreeImageLaoder.LaodImage(byteData, 0);            // 在FreeImage中，0表示BMP格式
                        toolStripStatusLabel_FileType.Text = imageType[0];
                        // BMP格式不涉及透明像素，因此“背景图像”菜单项没有意义，故隐藏
                        toolStripMenuItem_Background.Visible = false;
                        break;
                    case ".tga":
                        img = FreeImageLaoder.LaodImage(byteData, 17);           // 在FreeImage中，17表示TGA格式
                        toolStripStatusLabel_FileType.Text = imageType[1];
                        break;
                    case ".psd":
                        img = FreeImageLaoder.LaodImage(byteData, 20);           // 在FreeImage中，20表示PSD格式
                        toolStripStatusLabel_FileType.Text = imageType[2];
                        break;
                }

                // 启用背景颜色默认颜色
                PictureBGColorDefault();

                // 取图像宽高
                imgorginWidth = img.Width;
                imgorginHeight = img.Height;

                // 置窗体大小，默认为800*600，如果图像宽或高满足条件则再进行调整
                this.Size = new Size(600, 600);
                if (img.Width >= 600)
                {
                    this.Width = img.Width + 100;
                }
                if (img.Height >= 600)
                {
                    this.Height = img.Height + 100;
                }

                // 显示图像控件，置大小为图像大小
                pictureBox.Visible = true;
                pictureBox.Size = new Size(img.Width, img.Height);
                pictureBox.Location = new Point(0, menuStrip.Height);

                // 载入BMP图像
                pictureBox.Image = img;

                // 置状态栏第二栏位为图像尺寸
                toolStripStatusLabel_Parameter.Text = "图像尺寸: " + img.Width + "px" + "×" + img.Height + "px";
                // 置状态栏第三栏为文件大小
                toolStripStatusLabel_FileSize.Text = fileSize;

                //设置另存为
                saveFileDialog.FileName = filePath;
                saveData = byteData;
                saveFileDialog.Filter = defaultImgFilter + toolStripStatusLabel_FileType.Text + "|*" + Path.GetExtension(filePath);

                // 显示菜单项“背景颜色”、“放大”和“缩小”
                toolStripMenuItem_Background.Visible = true;
                toolStripMenuItem_ZoomIn.Visible = true;
                toolStripMenuItem_ZoomOut.Visible = true;
            }


            // 处理音频数据
            if (formatID == 2)
            {
                // 置音频播放器容器标题为音频文件名称
                this.Text = "正在试听: " + Path.GetFileName(filePath);
                label_SoundName.Text = Path.GetFileName(GlobalVar.targetPCK) + " -> " + filePath;

                // 置播放暂停按钮文本为播放暂停的UNcode字符
                btn_PlayPause.Text = sndPlay.ToString();
                btn_Stop.Text = sndStop.ToString();

                // 置窗体大小为450*250
                this.Size = new Size(450, 250);
                this.MaximizeBox = false;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                groupBox_SoundPlay.Dock = DockStyle.Fill;

                // 显示音频播放器容器，定位到窗体左上角
                groupBox_SoundPlay.Visible = true;
                groupBox_SoundPlay.Location = new Point(0, 25);

                // 置状态栏第一栏位为音频类型，并初始化音频流
                string[] soundType = { "WAV音频", "MP3音频" };
                switch (Path.GetExtension(filePath))
                {
                    case ".wav":
                        toolStripStatusLabel_FileType.Text = soundType[0];
                        waveStream = new WaveFileReader(new System.IO.MemoryStream(byteData));
                        saveFileDialog.Filter = soundType[0] + "|*" + Path.GetExtension(filePath);
                        break;
                    case ".mp3":
                        toolStripStatusLabel_FileType.Text = soundType[1];
                        waveStream = new Mp3FileReader(new System.IO.MemoryStream(byteData));
                        saveFileDialog.Filter = soundType[1] + "|*" + Path.GetExtension(filePath);
                        break;
                }

                // 置当前时长标签和总时长标签为音频当前时长和总时长时间格式为hh:mm:ss.ff
                label_PlayCurrent.Text = waveStream.CurrentTime.ToString("hh\\:mm\\:ss\\.ff");
                label_PlayDuration.Text = waveStream.TotalTime.ToString("hh\\:mm\\:ss\\.ff");

                // 置状态栏第二栏位为音频时长
                toolStripStatusLabel_Parameter.Text = "音频时长: " + label_PlayDuration.Text;

                // 置状态栏第三栏为文件大小
                toolStripStatusLabel_FileSize.Text = fileSize;

                // 初始化音频输出流
                outputDevice = new WaveOutEvent();
                outputDevice.Init(waveStream);

                //设置另存为
                saveFileDialog.FileName = filePath;
                saveData = byteData;

                // 隐藏菜单项
                toolStripMenuItem_View.Visible = false;
            }

        }

        /// <summary>
        /// 图像缩小响应函数
        /// </summary>
        private void ZoomIn()
        {
            // 缩放因子为10，图像最多放大三倍，超出的直接return
            if (pictureBox.Size.Width > imgorginWidth * 3 | pictureBox.Size.Height > imgorginHeight * 3)
            {
                return;
            }
            else
            {
                pictureBox.Size = new Size(pictureBox.Size.Width + imgorginWidth / 10, pictureBox.Size.Height + imgorginHeight / 10);
            }
        }
        /// <summary>
        /// 图像放大响应函数
        /// </summary>
        private void ZoomOut()
        {
            // 缩小因子为10，图像缩放不得小于原尺寸的一般，超出的直接return
            if (pictureBox.Size.Width < imgorginWidth * 0.5 | pictureBox.Size.Height < imgorginHeight * 0.5)
            {
                return;
            }
            else
            {
                pictureBox.Size = new Size(pictureBox.Size.Width - imgorginWidth / 10, pictureBox.Size.Height - imgorginHeight / 10);
            }
        }

        /// <summary>
        /// 面板容器的鼠标滚轮响应函数
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// </summary>
        private void Panel_View_MouseWheel(object sender, MouseEventArgs e)
        {
            // 当图片框显示的时候才会触发
            if (pictureBox.Visible)
            {
                if (e.Delta < 0)        //鼠标滚轮向上执行缩小操作
                {
                    ZoomOut();
                }
                if (e.Delta > 0)        //鼠标滚轮向下执行放大操作
                {
                    ZoomIn();
                }
            }
        }
        /// <summary>
        /// “播放/暂停”按钮响应函数
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// </summary>
        private void Btn_PlayPause_Click(object sender, EventArgs e)
        {
            // 音频只有播放器容器显示状态下才能触发下列操作
            if (groupBox_SoundPlay.Visible)
            {
                // 只有音频播放的时候才可进行暂停操作
                if (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    outputDevice.Pause();                                         // 音频输出流暂停
                    btn_PlayPause.Text = sndPlay.ToString();                      // 按钮字符变为倒三角播放
                }
                else
                {
                    // 只有音频播放暂停的时候才可进行播放操作，
                    if (outputDevice.PlaybackState == PlaybackState.Paused)
                    {
                        outputDevice.Play();                                      // 音频输出流播放
                        btn_PlayPause.Text = sndPause.ToString();                 // 按钮字符变为双竖线暂停
                    }
                    else
                    {
                        // 否则就是刚打开播放窗口未播放
                        outputDevice.Play();                                      // 输出流开始播放
                        btn_PlayPause.Text = sndPause.ToString();                 // 按钮字符变为双竖线暂停
                        trackBar_PlayPosition.Value = 0;                          // 播放进度滑块归零
                        timer_Play.Start();                                       // 时钟启动

                    }
                }
            }
        }

        /// <summary>
        /// “停止播放”按钮响应函数
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// </summary>
        private void Btn_Stop_Click(object sender, EventArgs e)
        {
            // 同上，音频只有播放器容器显示状态下才能触发下列操作
            if (groupBox_SoundPlay.Visible)
            {
                // 输出流状态既没有播放也没有暂停时候执行停止播放操作
                if (outputDevice.PlaybackState != PlaybackState.Stopped)
                {
                    outputDevice.Stop();                                        // 音频输出流停止
                    btn_PlayPause.Text = sndPlay.ToString();                    // 按钮字符变为倒三角播放
                    trackBar_PlayPosition.Value = 0;                            // 播放进度滑块归零
                    waveStream.Position = 0;                                    // Wave流归零
                    timer_Play.Stop();                                          // 时钟停止
                    // 当前播放时间标签归零
                    label_PlayCurrent.Text = waveStream.CurrentTime.ToString("hh\\:mm\\:ss\\.ff");
                }
            }
        }

        /// <summary>
        /// 音频播放期间时钟周期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Timer_Play_Tick(object sender, EventArgs e)
        {
            // 当音频输出流为播放状态时才会触发
            if (outputDevice.PlaybackState == PlaybackState.Playing)
            {
                // 更新当前播放时间
                label_PlayCurrent.Text = waveStream.CurrentTime.ToString("hh\\:mm\\:ss\\.ff");

                // 取当前播放时长和音频总时长
                long totalTicks = waveStream.TotalTime.Ticks;
                long currentTicks = waveStream.CurrentTime.Ticks;

                // 置播放进度滑块
                int value = (int)(currentTicks * (double)trackBar_PlayPosition.Maximum / totalTicks);
                trackBar_PlayPosition.Value = value;

                // 音频播放完毕时实施
                if (totalTicks == currentTicks)
                {
                    // 异步延迟100毫秒，防止音频没播完时钟就停止了
                    await Task.Delay(100);

                    // 音频输出流停止并清理
                    outputDevice.Stop();
                    outputDevice.Dispose();

                    // Wave流归零
                    waveStream.Position = 0;

                    // 当前播放时间标签和播放进度滑块归零，复位播放暂停按钮，
                    label_PlayCurrent.Text = waveStream.CurrentTime.ToString("hh\\:mm\\:ss\\.ff");
                    trackBar_PlayPosition.Value = (int)waveStream.Position;
                    btn_PlayPause.Text = sndPlay.ToString();

                    // 再初始化一遍音频输出流，为重播做准备
                    outputDevice = new WaveOutEvent();
                    outputDevice.Init(waveStream);
                }
            }
        }

        /// <summary>
        /// 播放进度滑块被拖动时响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrackBar_PlayPosition_ValueChanged(object sender, EventArgs e)
        {
            // 只有在非停止状态下才会执行
            if (outputDevice.PlaybackState != PlaybackState.Stopped)
            {
                // 置音频总时长，并按照播放进度滑块当前进度计算跳转位置
                long totalBytes = waveStream.Length;
                long newPosition = trackBar_PlayPosition.Value * totalBytes / trackBar_PlayPosition.Maximum;
                waveStream.Position = newPosition;
            }
        }

        /// <summary>
        /// 窗口关闭后响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_ResView_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 只有在音频输出流非空的情况下才会执行
            if (outputDevice != null)
            {
                outputDevice.Stop();                    //关闭窗口后，音频输出流停止
            }

        }

        /// <summary>
        /// 窗口载入完毕响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_ResView_Load(object sender, EventArgs e)
        {
            // 只有在Wave流非空的情况下才会执行
            if (waveStream != null)
            {
                Btn_PlayPause_Click(sender, e);         // 默认打开窗口就播放音频
            }
        }

        /// <summary>
        /// 点击“另存为”菜单项响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_SaveAs_Click(object sender, EventArgs e)
        {
            // 只保留子文件名
            saveFileDialog.FileName = Path.GetFileName(saveFileDialog.FileName);

            //取过滤器数量，并定位到最后一个
            int filterCount = saveFileDialog.Filter.Split('*').Length - 1;
            saveFileDialog.FilterIndex = filterCount;

            // 执行保存时的条件判断
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 浏览文本的情况下
                if (textBox.Visible || groupBox_SoundPlay.Visible)
                {
                    try
                    {
                        // 无论什么情况都将字节集写入到文本
                        System.IO.File.WriteAllBytes(saveFileDialog.FileName, saveData);

                        // 保存完成询问是否打开保存的文件夹
                        DialogResult r = MessageBox.Show("保存成功，是否打开保存的文件夹？", "保存完毕", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (r == DialogResult.Yes)
                        {
                            // 定位到目标文件夹
                            PublicFunction.GetOutputFolder(saveFileDialog.FileName);
                        }
                        else
                        {
                            return;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("发生未知错误，保存失败！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // 浏览图像的情况下
                if (pictureBox.Visible)
                {
                    try
                    {
                        // 根据文件过滤器来确定是否转换其他格式
                        switch (saveFileDialog.FilterIndex)
                        {
                            case 1:
                                pictureBox.Image.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                                break;
                            case 2:
                                pictureBox.Image.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                                break;
                            case 3:
                                pictureBox.Image.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Gif);
                                break;
                            case 4:
                                System.IO.File.WriteAllBytes(saveFileDialog.FileName, saveData);
                                break;
                        }

                        // 保存完成询问是否打开保存的文件夹
                        DialogResult r = MessageBox.Show("保存成功，是否打开保存的文件夹？", "保存完毕", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (r == DialogResult.Yes)
                        {
                            // 定位到目标文件夹
                            PublicFunction.GetOutputFolder(saveFileDialog.FileName);
                        }
                        else
                        {
                            return;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("发生未知错误，保存失败！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }



        /// <summary>
        /// 点击“退出浏览”菜单项响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        /// <summary>
        /// 文本显示框常规字号
        /// </summary>
        private void FontSizeDefault()
        {
            // 常规字号为12，菜单项“默认字号”选中，其余不选中
            textBox.Font = PublicFunction.ChangeFontSize(textBox.Font, 12);
            ToolStripMenuItem_FontSize_Small.Checked = !true;
            ToolStripMenuItem_FontSize_Middle.Checked = true;
            ToolStripMenuItem_FontSize_Large.Checked = !true;
            ToolStripMenuItem_FontSize_XLarge.Checked = !true;
        }

        /// <summary>
        /// 点击菜单项“小字号”响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_FontSize_Small_Click(object sender, EventArgs e)
        {
            // 小字号为9，菜单项“小字号”选中，其余不选中
            textBox.Font = PublicFunction.ChangeFontSize(textBox.Font, 9);
            ToolStripMenuItem_FontSize_Small.Checked = true;
            ToolStripMenuItem_FontSize_Middle.Checked = !true;
            ToolStripMenuItem_FontSize_Large.Checked = !true;
            ToolStripMenuItem_FontSize_XLarge.Checked = !true;
        }

        /// <summary>
        /// 点击菜单项“常规字号”响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_FontSize_Middle_Click(object sender, EventArgs e)
        {
            // 执行文本显示框常规字号操作
            FontSizeDefault();
        }

        /// <summary>
        /// 点击菜单项“大字号”响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_FontSize_Large_Click(object sender, EventArgs e)
        {
            // 大字号为18，菜单项“大字号”选中，其余不选中
            textBox.Font = PublicFunction.ChangeFontSize(textBox.Font, 18);
            ToolStripMenuItem_FontSize_Small.Checked = !true;
            ToolStripMenuItem_FontSize_Middle.Checked = !true;
            ToolStripMenuItem_FontSize_Large.Checked = true;
            ToolStripMenuItem_FontSize_XLarge.Checked = !true;
        }

        /// <summary>
        /// 点击菜单项“特大字号”响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_FontSize_XLarge_Click(object sender, EventArgs e)
        {
            // 特大字号为24，菜单项“特大字号”选中，其余不选中
            textBox.Font = PublicFunction.ChangeFontSize(textBox.Font, 24);
            ToolStripMenuItem_FontSize_Small.Checked = !true;
            ToolStripMenuItem_FontSize_Middle.Checked = !true;
            ToolStripMenuItem_FontSize_Large.Checked = !true;
            ToolStripMenuItem_FontSize_XLarge.Checked = true;
        }

        /// <summary>
        /// 开启关闭护眼模式操作
        /// </summary>
        private void EPmode()
        {
            // 当菜单项“护眼模式”选中时
            if (toolStripMenuItem_EPmode.Checked)
            {
                textBox.BackColor = Color.FromArgb(199, 237, 204);
                toolStripMenuItem_EPmode.Checked = true;
            }
            else
            {
                textBox.BackColor = SystemColors.Window;
                toolStripMenuItem_EPmode.Checked = false;
            }
        }

        /// <summary>
        /// 点击菜单项“护眼模式”响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_EPmode_Click(object sender, EventArgs e)
        {
            // 执行开启关闭护眼模式的操作
            EPmode();
        }


        /// <summary>
        /// 图片框默认背景色操作
        /// </summary>
        private void PictureBGColorDefault()
        {
            // 置图片框默认背景色为紫红色，菜单项“紫红色”选中，其余不选中
            pictureBox.BackColor = toolStripMenuItem_Background_Fushia.BackColor;
            toolStripMenuItem_Background_Fushia.Checked = true;
            toolStripMenuItem_Background_Blue.Checked = !true;
            toolStripMenuItem_Background_Red.Checked = !true;
            toolStripMenuItem_Background_Green.Checked = !true;
            toolStripMenuItem_Background_None.Checked = !true;
            toolStripMenuItem_Background_Custom.Checked = !true;
        }

        /// <summary>
        /// 点击菜单项“紫红色”响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Background_Fushia_Click(object sender, EventArgs e)
        {
            // 执行图片框默认背景色操作
            PictureBGColorDefault();
        }

        /// <summary>
        /// 点击菜单项“蓝色”响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Background_Blue_Click(object sender, EventArgs e)
        {
            // 置图片框背景颜色为蓝色，菜单项“蓝色”选中，其余不选中
            pictureBox.BackColor = toolStripMenuItem_Background_Blue.BackColor;
            toolStripMenuItem_Background_Fushia.Checked = !true;
            toolStripMenuItem_Background_Blue.Checked = true;
            toolStripMenuItem_Background_Red.Checked = !true;
            toolStripMenuItem_Background_Green.Checked = !true;
            toolStripMenuItem_Background_None.Checked = !true;
            toolStripMenuItem_Background_Custom.Checked = !true;
        }

        /// <summary>
        /// 点击菜单项“红色”响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Background_Red_Click(object sender, EventArgs e)
        {
            // 置图片框背景颜色为红色，菜单项“红色”选中，其余不选中
            pictureBox.BackColor = toolStripMenuItem_Background_Red.BackColor;
            toolStripMenuItem_Background_Fushia.Checked = !true;
            toolStripMenuItem_Background_Blue.Checked = !true;
            toolStripMenuItem_Background_Red.Checked = true;
            toolStripMenuItem_Background_Green.Checked = !true;
            toolStripMenuItem_Background_None.Checked = !true;
            toolStripMenuItem_Background_Custom.Checked = !true;
        }

        /// <summary>
        /// 点击菜单项“绿色”响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Background_Green_Click(object sender, EventArgs e)
        {
            // 置图片框背景颜色为绿色，菜单项“绿色”选中，其余不选中
            pictureBox.BackColor = toolStripMenuItem_Background_Green.BackColor;
            toolStripMenuItem_Background_Fushia.Checked = !true;
            toolStripMenuItem_Background_Blue.Checked = !true;
            toolStripMenuItem_Background_Red.Checked = !true;
            toolStripMenuItem_Background_Green.Checked = true;
            toolStripMenuItem_Background_None.Checked = !true;
            toolStripMenuItem_Background_Custom.Checked = !true;
        }

        /// <summary>
        /// 点击菜单项“无色”响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Background_None_Click(object sender, EventArgs e)
        {
            // 置图片框背景颜色为无色，菜单项“无色”选中，其余不选中
            pictureBox.BackColor = statusStrip.BackColor;
            toolStripMenuItem_Background_Fushia.Checked = !true;
            toolStripMenuItem_Background_Blue.Checked = !true;
            toolStripMenuItem_Background_Red.Checked = !true;
            toolStripMenuItem_Background_Green.Checked = !true;
            toolStripMenuItem_Background_None.Checked = true;
            toolStripMenuItem_Background_Custom.Checked = !true;
        }


        /// <summary>
        /// 点击菜单项“自定义颜色”响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem_Background_Custom_Click(object sender, EventArgs e)
        {
            // 颜色选择器默认为当前图片框背景颜色
            colorDialog.Color = pictureBox.BackColor;

            // 当选定颜色后执行操作
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // 置图片框背景颜色为颜色选择器颜色，菜单项“自定义”选中，其余不选中
                pictureBox.BackColor = colorDialog.Color;
                toolStripMenuItem_Background_Fushia.Checked = !true;
                toolStripMenuItem_Background_Blue.Checked = !true;
                toolStripMenuItem_Background_Red.Checked = !true;
                toolStripMenuItem_Background_Green.Checked = !true;
                toolStripMenuItem_Background_None.Checked = !true;
                toolStripMenuItem_Background_Custom.Checked = true;
            }
        }

        /// <summary>
        /// 点击菜单项“放大”响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_ZoomIn_Click(object sender, EventArgs e)
        {
            ZoomIn();       // 执行放大操作
        }

        /// <summary>
        /// 点击菜单项“缩小”响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_ZoomOut_Click(object sender, EventArgs e)
        {
            ZoomOut();      // 执行缩小操作
        }

        /// <summary>
        /// 点击菜单项“使用说明”响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_ReadMe_Click(object sender, EventArgs e)
        {

            // 创建，置所有者为本窗体
            Form_Help Form_Help = new Form_Help();
            Form_Help.Owner = this;

            // 文本显示框显示时向使用说明窗体传送文本浏览器使用说明
            if (textBox.Visible)
            {
                Form_Help.resViewHelpText = Properties.Resources.help_TextView;
                Form_Help.Text = "文本浏览器使用说明";
                Form_Help.StartPosition = FormStartPosition.CenterScreen;
            }
            // 图片浏览框显示时向使用说明窗体传送图形图像浏览器使用说明
            if (pictureBox.Visible)
            {
                Form_Help.resViewHelpText = Properties.Resources.help_ImageView;
                Form_Help.Text = "图形图像浏览器使用说明";
                Form_Help.StartPosition = FormStartPosition.CenterScreen;
            }

            // 音频播放容器显示时向使用说明窗体传送声音播放器使用说明"
            if (groupBox_SoundPlay.Visible)
            {
                Form_Help.resViewHelpText = Properties.Resources.help_SoundView;
                Form_Help.Text = "声音播放器使用说明";

                // 由于音频播放窗口比较小，使用说明窗口比较大，会遮住音频播放窗口，因此要将使用说明窗口移到旁边
                Form_Help.StartPosition = FormStartPosition.Manual;
                Form_Help.Location = new Point(0, 0);
            }

            // 以对话框方式显示使用说明窗体
            Form_Help.ShowDialog();
        }

        /// <summary>
        /// 点击菜单项“版本信息”响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_About_Click(object sender, EventArgs e)
        {
            Form_About form_About = new Form_About();
            form_About.Owner = this;
            form_About.ShowDialog();
        }
    }
}