namespace PCKViewer
{
    partial class Form_About
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
            this.btn_OK = new System.Windows.Forms.Button();
            this.pictureBox_Icon = new System.Windows.Forms.PictureBox();
            this.label_Title = new System.Windows.Forms.Label();
            this.label_Info = new System.Windows.Forms.Label();
            this.label_GitHub = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Icon)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(209, 200);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(82, 30);
            this.btn_OK.TabIndex = 2;
            this.btn_OK.Text = "&OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.Btn_OK_Click);
            // 
            // pictureBox_Icon
            // 
            this.pictureBox_Icon.Location = new System.Drawing.Point(35, 18);
            this.pictureBox_Icon.Name = "pictureBox_Icon";
            this.pictureBox_Icon.Size = new System.Drawing.Size(48, 48);
            this.pictureBox_Icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Icon.TabIndex = 3;
            this.pictureBox_Icon.TabStop = false;
            // 
            // label_Title
            // 
            this.label_Title.Font = new System.Drawing.Font("宋体", 12F);
            this.label_Title.Location = new System.Drawing.Point(92, 32);
            this.label_Title.Name = "label_Title";
            this.label_Title.Size = new System.Drawing.Size(316, 20);
            this.label_Title.TabIndex = 5;
            this.label_Title.Text = "label1";
            this.label_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_Info
            // 
            this.label_Info.AutoSize = true;
            this.label_Info.Font = new System.Drawing.Font("宋体", 9F);
            this.label_Info.Location = new System.Drawing.Point(32, 77);
            this.label_Info.Name = "label_Info";
            this.label_Info.Size = new System.Drawing.Size(55, 15);
            this.label_Info.TabIndex = 6;
            this.label_Info.Text = "label2";
            // 
            // label_GitHub
            // 
            this.label_GitHub.AutoSize = true;
            this.label_GitHub.Font = new System.Drawing.Font("宋体", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_GitHub.ForeColor = System.Drawing.Color.Blue;
            this.label_GitHub.Location = new System.Drawing.Point(32, 179);
            this.label_GitHub.Name = "label_GitHub";
            this.label_GitHub.Size = new System.Drawing.Size(87, 15);
            this.label_GitHub.TabIndex = 7;
            this.label_GitHub.Text = "软件源代码";
            this.label_GitHub.Click += new System.EventHandler(this.label_GitHub_Click);
            // 
            // Form_About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 253);
            this.Controls.Add(this.label_GitHub);
            this.Controls.Add(this.label_Info);
            this.Controls.Add(this.label_Title);
            this.Controls.Add(this.pictureBox_Icon);
            this.Controls.Add(this.btn_OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_About";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "版本信息";
            this.Load += new System.EventHandler(this.Form_About_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Icon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.PictureBox pictureBox_Icon;
        private System.Windows.Forms.Label label_Title;
        private System.Windows.Forms.Label label_Info;
        private System.Windows.Forms.Label label_GitHub;
    }
}