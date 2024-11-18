namespace PCKViewer
{
    partial class Form_Debug
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
            this.btn_Open = new System.Windows.Forms.Button();
            this.textBox_filePath = new System.Windows.Forms.TextBox();
            this.btn_DeZlib = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Open
            // 
            this.btn_Open.Location = new System.Drawing.Point(437, 130);
            this.btn_Open.Name = "btn_Open";
            this.btn_Open.Size = new System.Drawing.Size(75, 23);
            this.btn_Open.TabIndex = 0;
            this.btn_Open.Text = "&Open";
            this.btn_Open.UseVisualStyleBackColor = true;
            this.btn_Open.Click += new System.EventHandler(this.btn_Open_Click);
            // 
            // textBox_filePath
            // 
            this.textBox_filePath.Location = new System.Drawing.Point(179, 128);
            this.textBox_filePath.Name = "textBox_filePath";
            this.textBox_filePath.Size = new System.Drawing.Size(241, 25);
            this.textBox_filePath.TabIndex = 1;
            // 
            // btn_DeZlib
            // 
            this.btn_DeZlib.Location = new System.Drawing.Point(518, 128);
            this.btn_DeZlib.Name = "btn_DeZlib";
            this.btn_DeZlib.Size = new System.Drawing.Size(75, 23);
            this.btn_DeZlib.TabIndex = 2;
            this.btn_DeZlib.Text = "&DeZlib";
            this.btn_DeZlib.UseVisualStyleBackColor = true;
            this.btn_DeZlib.Click += new System.EventHandler(this.btn_DeZlib_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.Filter = "All File|*.*";
            this.openFileDialog.Title = "Please select zlib file";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(44, 183);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(298, 207);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(453, 269);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form_Debug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_DeZlib);
            this.Controls.Add(this.textBox_filePath);
            this.Controls.Add(this.btn_Open);
            this.Name = "Form_Debug";
            this.Text = "Form_Debug";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Open;
        private System.Windows.Forms.TextBox textBox_filePath;
        private System.Windows.Forms.Button btn_DeZlib;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
    }
}