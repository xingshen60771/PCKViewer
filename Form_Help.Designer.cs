namespace PCKViewer
{
    partial class Form_Help
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
            this.textBox_Help = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(688, 511);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(82, 30);
            this.btn_OK.TabIndex = 1;
            this.btn_OK.Text = "&OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.Btn_OK_Click);
            // 
            // textBox_Help
            // 
            this.textBox_Help.Font = new System.Drawing.Font("宋体", 12F);
            this.textBox_Help.Location = new System.Drawing.Point(12, 12);
            this.textBox_Help.Multiline = true;
            this.textBox_Help.Name = "textBox_Help";
            this.textBox_Help.ReadOnly = true;
            this.textBox_Help.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Help.Size = new System.Drawing.Size(758, 479);
            this.textBox_Help.TabIndex = 2;
            this.textBox_Help.Text = "\r\n\r\n";
            // 
            // Form_Help
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.Controls.Add(this.textBox_Help);
            this.Controls.Add(this.btn_OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_Help";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "使用说明";
            this.Load += new System.EventHandler(this.Form_Help_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.TextBox textBox_Help;
    }
}