using System;

namespace PCKViewer
{
    partial class Form_ResView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ResView));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_FileType = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_Parameter = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_FileSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel_View = new System.Windows.Forms.Panel();
            this.groupBox_SoundPlay = new System.Windows.Forms.GroupBox();
            this.label_SoundName = new System.Windows.Forms.Label();
            this.label_PlayDuration = new System.Windows.Forms.Label();
            this.label_PlayCurrent = new System.Windows.Forms.Label();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.btn_PlayPause = new System.Windows.Forms.Button();
            this.trackBar_PlayPosition = new System.Windows.Forms.TrackBar();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.timer_Play = new System.Windows.Forms.Timer(this.components);
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItem_File = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_SpeLine01 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_View = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_FontSize = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_FontSize_Small = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_FontSize_Middle = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_FontSize_Large = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_FontSize_XLarge = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_EPmode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Background = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Background_Fushia = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Background_Blue = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Background_Red = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Background_Green = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Background_None = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_SpeLine02 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_Background_Custom = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_ZoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_ZoomOut = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_ReadMe = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_SpeLine03 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_About = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.textBox = new System.Windows.Forms.TextBox();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.statusStrip.SuspendLayout();
            this.panel_View.SuspendLayout();
            this.groupBox_SoundPlay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_PlayPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.SystemColors.Control;
            this.statusStrip.Font = new System.Drawing.Font("宋体", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_FileType,
            this.toolStripStatusLabel_Parameter,
            this.toolStripStatusLabel_FileSize});
            this.statusStrip.Location = new System.Drawing.Point(0, 420);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(800, 30);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "文件属性";
            // 
            // toolStripStatusLabel_FileType
            // 
            this.toolStripStatusLabel_FileType.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabel_FileType.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.toolStripStatusLabel_FileType.Name = "toolStripStatusLabel_FileType";
            this.toolStripStatusLabel_FileType.Size = new System.Drawing.Size(230, 24);
            this.toolStripStatusLabel_FileType.Text = "toolStripStatusLabel_FileType";
            // 
            // toolStripStatusLabel_Parameter
            // 
            this.toolStripStatusLabel_Parameter.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabel_Parameter.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.toolStripStatusLabel_Parameter.Name = "toolStripStatusLabel_Parameter";
            this.toolStripStatusLabel_Parameter.Size = new System.Drawing.Size(244, 24);
            this.toolStripStatusLabel_Parameter.Text = "toolStripStatusLabel_Parameter";
            // 
            // toolStripStatusLabel_FileSize
            // 
            this.toolStripStatusLabel_FileSize.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabel_FileSize.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.toolStripStatusLabel_FileSize.Name = "toolStripStatusLabel_FileSize";
            this.toolStripStatusLabel_FileSize.Size = new System.Drawing.Size(223, 24);
            this.toolStripStatusLabel_FileSize.Text = "toolStripStatusLabel_FileSize";
            // 
            // panel_View
            // 
            this.panel_View.AutoScroll = true;
            this.panel_View.BackColor = System.Drawing.Color.Transparent;
            this.panel_View.Controls.Add(this.groupBox_SoundPlay);
            this.panel_View.Controls.Add(this.pictureBox);
            this.panel_View.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_View.Location = new System.Drawing.Point(0, 0);
            this.panel_View.Name = "panel_View";
            this.panel_View.Size = new System.Drawing.Size(800, 450);
            this.panel_View.TabIndex = 1;
            this.panel_View.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Panel_View_MouseWheel);
            // 
            // groupBox_SoundPlay
            // 
            this.groupBox_SoundPlay.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox_SoundPlay.Controls.Add(this.label_SoundName);
            this.groupBox_SoundPlay.Controls.Add(this.label_PlayDuration);
            this.groupBox_SoundPlay.Controls.Add(this.label_PlayCurrent);
            this.groupBox_SoundPlay.Controls.Add(this.btn_Stop);
            this.groupBox_SoundPlay.Controls.Add(this.btn_PlayPause);
            this.groupBox_SoundPlay.Controls.Add(this.trackBar_PlayPosition);
            this.groupBox_SoundPlay.Location = new System.Drawing.Point(0, 59);
            this.groupBox_SoundPlay.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox_SoundPlay.Name = "groupBox_SoundPlay";
            this.groupBox_SoundPlay.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox_SoundPlay.Size = new System.Drawing.Size(657, 220);
            this.groupBox_SoundPlay.TabIndex = 19;
            this.groupBox_SoundPlay.TabStop = false;
            this.groupBox_SoundPlay.Visible = false;
            // 
            // label_SoundName
            // 
            this.label_SoundName.Location = new System.Drawing.Point(32, 33);
            this.label_SoundName.Name = "label_SoundName";
            this.label_SoundName.Size = new System.Drawing.Size(592, 33);
            this.label_SoundName.TabIndex = 11;
            this.label_SoundName.Text = "label1";
            // 
            // label_PlayDuration
            // 
            this.label_PlayDuration.Location = new System.Drawing.Point(448, 78);
            this.label_PlayDuration.Name = "label_PlayDuration";
            this.label_PlayDuration.Size = new System.Drawing.Size(125, 15);
            this.label_PlayDuration.TabIndex = 10;
            this.label_PlayDuration.Text = "label3";
            this.label_PlayDuration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_PlayCurrent
            // 
            this.label_PlayCurrent.Location = new System.Drawing.Point(12, 78);
            this.label_PlayCurrent.Name = "label_PlayCurrent";
            this.label_PlayCurrent.Size = new System.Drawing.Size(125, 15);
            this.label_PlayCurrent.TabIndex = 9;
            this.label_PlayCurrent.Text = "label2";
            this.label_PlayCurrent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_Stop
            // 
            this.btn_Stop.Font = new System.Drawing.Font("宋体", 18F);
            this.btn_Stop.Location = new System.Drawing.Point(158, 125);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(50, 50);
            this.btn_Stop.TabIndex = 8;
            this.btn_Stop.Text = "btn_Stop";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.Btn_Stop_Click);
            // 
            // btn_PlayPause
            // 
            this.btn_PlayPause.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.btn_PlayPause.Location = new System.Drawing.Point(102, 125);
            this.btn_PlayPause.Name = "btn_PlayPause";
            this.btn_PlayPause.Size = new System.Drawing.Size(50, 50);
            this.btn_PlayPause.TabIndex = 7;
            this.btn_PlayPause.Text = "btn_PlayPause";
            this.btn_PlayPause.UseVisualStyleBackColor = true;
            this.btn_PlayPause.Click += new System.EventHandler(this.Btn_PlayPause_Click);
            // 
            // trackBar_PlayPosition
            // 
            this.trackBar_PlayPosition.AutoSize = false;
            this.trackBar_PlayPosition.Location = new System.Drawing.Point(142, 78);
            this.trackBar_PlayPosition.Maximum = 100000000;
            this.trackBar_PlayPosition.Name = "trackBar_PlayPosition";
            this.trackBar_PlayPosition.Size = new System.Drawing.Size(300, 41);
            this.trackBar_PlayPosition.TabIndex = 6;
            this.trackBar_PlayPosition.TickFrequency = 0;
            this.trackBar_PlayPosition.ValueChanged += new System.EventHandler(this.TrackBar_PlayPosition_ValueChanged);
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(100, 50);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 10;
            this.pictureBox.TabStop = false;
            this.pictureBox.Visible = false;
            // 
            // timer_Play
            // 
            this.timer_Play.Tick += new System.EventHandler(this.Timer_Play_Tick);
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_File,
            this.toolStripMenuItem_View,
            this.ToolStripMenuItem_Help});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 28);
            this.menuStrip.TabIndex = 15;
            this.menuStrip.Text = "menuStrip1";
            // 
            // ToolStripMenuItem_File
            // 
            this.ToolStripMenuItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_SaveAs,
            this.toolStripMenuItem_SpeLine01,
            this.toolStripMenuItem_Exit});
            this.ToolStripMenuItem_File.Name = "ToolStripMenuItem_File";
            this.ToolStripMenuItem_File.Size = new System.Drawing.Size(71, 24);
            this.ToolStripMenuItem_File.Text = "文件(&F)";
            // 
            // toolStripMenuItem_SaveAs
            // 
            this.toolStripMenuItem_SaveAs.Name = "toolStripMenuItem_SaveAs";
            this.toolStripMenuItem_SaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.toolStripMenuItem_SaveAs.Size = new System.Drawing.Size(230, 26);
            this.toolStripMenuItem_SaveAs.Text = "另存为(&S)";
            this.toolStripMenuItem_SaveAs.Click += new System.EventHandler(this.ToolStripMenuItem_SaveAs_Click);
            // 
            // toolStripMenuItem_SpeLine01
            // 
            this.toolStripMenuItem_SpeLine01.Name = "toolStripMenuItem_SpeLine01";
            this.toolStripMenuItem_SpeLine01.Size = new System.Drawing.Size(227, 6);
            // 
            // toolStripMenuItem_Exit
            // 
            this.toolStripMenuItem_Exit.Name = "toolStripMenuItem_Exit";
            this.toolStripMenuItem_Exit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.toolStripMenuItem_Exit.Size = new System.Drawing.Size(230, 26);
            this.toolStripMenuItem_Exit.Text = "退出浏览(&X)";
            this.toolStripMenuItem_Exit.Click += new System.EventHandler(this.ToolStripMenuItem_Exit_Click);
            // 
            // toolStripMenuItem_View
            // 
            this.toolStripMenuItem_View.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_FontSize,
            this.toolStripMenuItem_EPmode,
            this.toolStripMenuItem_Background,
            this.toolStripMenuItem_ZoomIn,
            this.toolStripMenuItem_ZoomOut});
            this.toolStripMenuItem_View.Name = "toolStripMenuItem_View";
            this.toolStripMenuItem_View.Size = new System.Drawing.Size(73, 24);
            this.toolStripMenuItem_View.Text = "视图(&V)";
            // 
            // toolStripMenuItem_FontSize
            // 
            this.toolStripMenuItem_FontSize.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_FontSize_Small,
            this.ToolStripMenuItem_FontSize_Middle,
            this.ToolStripMenuItem_FontSize_Large,
            this.ToolStripMenuItem_FontSize_XLarge});
            this.toolStripMenuItem_FontSize.Name = "toolStripMenuItem_FontSize";
            this.toolStripMenuItem_FontSize.Size = new System.Drawing.Size(224, 26);
            this.toolStripMenuItem_FontSize.Text = "字号(&I)";
            this.toolStripMenuItem_FontSize.Visible = false;
            // 
            // ToolStripMenuItem_FontSize_Small
            // 
            this.ToolStripMenuItem_FontSize_Small.Name = "ToolStripMenuItem_FontSize_Small";
            this.ToolStripMenuItem_FontSize_Small.Size = new System.Drawing.Size(224, 26);
            this.ToolStripMenuItem_FontSize_Small.Text = "小(&S)";
            this.ToolStripMenuItem_FontSize_Small.Click += new System.EventHandler(this.ToolStripMenuItem_FontSize_Small_Click);
            // 
            // ToolStripMenuItem_FontSize_Middle
            // 
            this.ToolStripMenuItem_FontSize_Middle.Name = "ToolStripMenuItem_FontSize_Middle";
            this.ToolStripMenuItem_FontSize_Middle.Size = new System.Drawing.Size(224, 26);
            this.ToolStripMenuItem_FontSize_Middle.Text = "常规(&M)";
            this.ToolStripMenuItem_FontSize_Middle.Click += new System.EventHandler(this.ToolStripMenuItem_FontSize_Middle_Click);
            // 
            // ToolStripMenuItem_FontSize_Large
            // 
            this.ToolStripMenuItem_FontSize_Large.Name = "ToolStripMenuItem_FontSize_Large";
            this.ToolStripMenuItem_FontSize_Large.Size = new System.Drawing.Size(224, 26);
            this.ToolStripMenuItem_FontSize_Large.Text = "大(&L)";
            this.ToolStripMenuItem_FontSize_Large.Click += new System.EventHandler(this.ToolStripMenuItem_FontSize_Large_Click);
            // 
            // ToolStripMenuItem_FontSize_XLarge
            // 
            this.ToolStripMenuItem_FontSize_XLarge.Name = "ToolStripMenuItem_FontSize_XLarge";
            this.ToolStripMenuItem_FontSize_XLarge.Size = new System.Drawing.Size(224, 26);
            this.ToolStripMenuItem_FontSize_XLarge.Text = "特大(&X)";
            this.ToolStripMenuItem_FontSize_XLarge.Click += new System.EventHandler(this.ToolStripMenuItem_FontSize_XLarge_Click);
            // 
            // toolStripMenuItem_EPmode
            // 
            this.toolStripMenuItem_EPmode.Checked = true;
            this.toolStripMenuItem_EPmode.CheckOnClick = true;
            this.toolStripMenuItem_EPmode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem_EPmode.Name = "toolStripMenuItem_EPmode";
            this.toolStripMenuItem_EPmode.Size = new System.Drawing.Size(224, 26);
            this.toolStripMenuItem_EPmode.Text = "护眼模式(&E)";
            this.toolStripMenuItem_EPmode.Visible = false;
            this.toolStripMenuItem_EPmode.Click += new System.EventHandler(this.ToolStripMenuItem_EPmode_Click);
            // 
            // toolStripMenuItem_Background
            // 
            this.toolStripMenuItem_Background.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Background_Fushia,
            this.toolStripMenuItem_Background_Blue,
            this.toolStripMenuItem_Background_Red,
            this.toolStripMenuItem_Background_Green,
            this.toolStripMenuItem_Background_None,
            this.toolStripMenuItem_SpeLine02,
            this.toolStripMenuItem_Background_Custom});
            this.toolStripMenuItem_Background.Name = "toolStripMenuItem_Background";
            this.toolStripMenuItem_Background.Size = new System.Drawing.Size(224, 26);
            this.toolStripMenuItem_Background.Text = "背景颜色(&B)";
            this.toolStripMenuItem_Background.Visible = false;
            // 
            // toolStripMenuItem_Background_Fushia
            // 
            this.toolStripMenuItem_Background_Fushia.BackColor = System.Drawing.Color.Fuchsia;
            this.toolStripMenuItem_Background_Fushia.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem_Background_Fushia.ImageTransparentColor = System.Drawing.SystemColors.Control;
            this.toolStripMenuItem_Background_Fushia.Name = "toolStripMenuItem_Background_Fushia";
            this.toolStripMenuItem_Background_Fushia.Size = new System.Drawing.Size(224, 26);
            this.toolStripMenuItem_Background_Fushia.Text = "紫红色(&F)";
            this.toolStripMenuItem_Background_Fushia.Click += new System.EventHandler(this.ToolStripMenuItem_Background_Fushia_Click);
            // 
            // toolStripMenuItem_Background_Blue
            // 
            this.toolStripMenuItem_Background_Blue.BackColor = System.Drawing.Color.Blue;
            this.toolStripMenuItem_Background_Blue.CheckOnClick = true;
            this.toolStripMenuItem_Background_Blue.ImageTransparentColor = System.Drawing.SystemColors.Control;
            this.toolStripMenuItem_Background_Blue.Name = "toolStripMenuItem_Background_Blue";
            this.toolStripMenuItem_Background_Blue.Size = new System.Drawing.Size(224, 26);
            this.toolStripMenuItem_Background_Blue.Text = "蓝色(&B)";
            this.toolStripMenuItem_Background_Blue.Click += new System.EventHandler(this.ToolStripMenuItem_Background_Blue_Click);
            // 
            // toolStripMenuItem_Background_Red
            // 
            this.toolStripMenuItem_Background_Red.BackColor = System.Drawing.Color.Red;
            this.toolStripMenuItem_Background_Red.CheckOnClick = true;
            this.toolStripMenuItem_Background_Red.ImageTransparentColor = System.Drawing.SystemColors.Control;
            this.toolStripMenuItem_Background_Red.Name = "toolStripMenuItem_Background_Red";
            this.toolStripMenuItem_Background_Red.Size = new System.Drawing.Size(224, 26);
            this.toolStripMenuItem_Background_Red.Text = "红色(&R)";
            this.toolStripMenuItem_Background_Red.Click += new System.EventHandler(this.ToolStripMenuItem_Background_Red_Click);
            // 
            // toolStripMenuItem_Background_Green
            // 
            this.toolStripMenuItem_Background_Green.BackColor = System.Drawing.Color.Lime;
            this.toolStripMenuItem_Background_Green.Name = "toolStripMenuItem_Background_Green";
            this.toolStripMenuItem_Background_Green.Size = new System.Drawing.Size(224, 26);
            this.toolStripMenuItem_Background_Green.Text = "绿色(&G)";
            this.toolStripMenuItem_Background_Green.Click += new System.EventHandler(this.ToolStripMenuItem_Background_Green_Click);
            // 
            // toolStripMenuItem_Background_None
            // 
            this.toolStripMenuItem_Background_None.BackColor = System.Drawing.SystemColors.Window;
            this.toolStripMenuItem_Background_None.CheckOnClick = true;
            this.toolStripMenuItem_Background_None.ImageTransparentColor = System.Drawing.SystemColors.Control;
            this.toolStripMenuItem_Background_None.Name = "toolStripMenuItem_Background_None";
            this.toolStripMenuItem_Background_None.Size = new System.Drawing.Size(224, 26);
            this.toolStripMenuItem_Background_None.Text = "无色(&N)";
            this.toolStripMenuItem_Background_None.Click += new System.EventHandler(this.ToolStripMenuItem_Background_None_Click);
            // 
            // toolStripMenuItem_SpeLine02
            // 
            this.toolStripMenuItem_SpeLine02.Name = "toolStripMenuItem_SpeLine02";
            this.toolStripMenuItem_SpeLine02.Size = new System.Drawing.Size(221, 6);
            // 
            // toolStripMenuItem_Background_Custom
            // 
            this.toolStripMenuItem_Background_Custom.Name = "toolStripMenuItem_Background_Custom";
            this.toolStripMenuItem_Background_Custom.Size = new System.Drawing.Size(224, 26);
            this.toolStripMenuItem_Background_Custom.Text = "自定义颜色(&C)";
            this.toolStripMenuItem_Background_Custom.Click += new System.EventHandler(this.toolStripMenuItem_Background_Custom_Click);
            // 
            // toolStripMenuItem_ZoomIn
            // 
            this.toolStripMenuItem_ZoomIn.Name = "toolStripMenuItem_ZoomIn";
            this.toolStripMenuItem_ZoomIn.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.toolStripMenuItem_ZoomIn.Size = new System.Drawing.Size(224, 26);
            this.toolStripMenuItem_ZoomIn.Text = "放大";
            this.toolStripMenuItem_ZoomIn.Visible = false;
            this.toolStripMenuItem_ZoomIn.Click += new System.EventHandler(this.ToolStripMenuItem_ZoomIn_Click);
            // 
            // toolStripMenuItem_ZoomOut
            // 
            this.toolStripMenuItem_ZoomOut.Name = "toolStripMenuItem_ZoomOut";
            this.toolStripMenuItem_ZoomOut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.toolStripMenuItem_ZoomOut.Size = new System.Drawing.Size(224, 26);
            this.toolStripMenuItem_ZoomOut.Text = "缩小";
            this.toolStripMenuItem_ZoomOut.Visible = false;
            this.toolStripMenuItem_ZoomOut.Click += new System.EventHandler(this.ToolStripMenuItem_ZoomOut_Click);
            // 
            // ToolStripMenuItem_Help
            // 
            this.ToolStripMenuItem_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_ReadMe,
            this.toolStripMenuItem_SpeLine03,
            this.ToolStripMenuItem_About});
            this.ToolStripMenuItem_Help.Name = "ToolStripMenuItem_Help";
            this.ToolStripMenuItem_Help.Size = new System.Drawing.Size(75, 24);
            this.ToolStripMenuItem_Help.Text = "帮助(&H)";
            // 
            // ToolStripMenuItem_ReadMe
            // 
            this.ToolStripMenuItem_ReadMe.Name = "ToolStripMenuItem_ReadMe";
            this.ToolStripMenuItem_ReadMe.Size = new System.Drawing.Size(224, 26);
            this.ToolStripMenuItem_ReadMe.Text = "使用说明(&R)";
            this.ToolStripMenuItem_ReadMe.Click += new System.EventHandler(this.ToolStripMenuItem_ReadMe_Click);
            // 
            // toolStripMenuItem_SpeLine03
            // 
            this.toolStripMenuItem_SpeLine03.Name = "toolStripMenuItem_SpeLine03";
            this.toolStripMenuItem_SpeLine03.Size = new System.Drawing.Size(221, 6);
            // 
            // ToolStripMenuItem_About
            // 
            this.ToolStripMenuItem_About.Name = "ToolStripMenuItem_About";
            this.ToolStripMenuItem_About.Size = new System.Drawing.Size(224, 26);
            this.ToolStripMenuItem_About.Text = "版本信息(&A)";
            this.ToolStripMenuItem_About.Click += new System.EventHandler(this.ToolStripMenuItem_About_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Title = "另存为...";
            // 
            // textBox
            // 
            this.textBox.Font = new System.Drawing.Font("宋体", 12F);
            this.textBox.Location = new System.Drawing.Point(0, 31);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox.Size = new System.Drawing.Size(100, 25);
            this.textBox.TabIndex = 18;
            this.textBox.Visible = false;
            this.textBox.WordWrap = false;
            // 
            // Form_ResView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.panel_View);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "Form_ResView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form_ResView";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_ResView_FormClosed);
            this.Load += new System.EventHandler(this.Form_ResView_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panel_View.ResumeLayout(false);
            this.groupBox_SoundPlay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_PlayPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_FileType;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Parameter;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_FileSize;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Timer timer_Play;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_File;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_SaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem_SpeLine01;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Exit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_View;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_FontSize;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_FontSize_Small;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_FontSize_Middle;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_FontSize_Large;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_FontSize_XLarge;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_EPmode;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Background;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Background_None;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Background_Blue;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Background_Fushia;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Background_Red;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ZoomIn;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ZoomOut;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Label label_SoundName;
        private System.Windows.Forms.Label label_PlayDuration;
        private System.Windows.Forms.Label label_PlayCurrent;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.Button btn_PlayPause;
        private System.Windows.Forms.TrackBar trackBar_PlayPosition;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Background_Green;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Help;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ReadMe;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem_SpeLine03;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_About;
        public System.Windows.Forms.Panel panel_View;
        public System.Windows.Forms.TextBox textBox;
        public System.Windows.Forms.GroupBox groupBox_SoundPlay;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem_SpeLine02;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Background_Custom;
    }
}