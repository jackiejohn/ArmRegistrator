namespace ArmRegistrator
{
    partial class FormBar
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
            this.picWebCam = new System.Windows.Forms.PictureBox();
            this.BtnDecode = new System.Windows.Forms.Button();
            this.txtTypeWebCam = new System.Windows.Forms.TextBox();
            this.txtContentWebCam = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picWebCam)).BeginInit();
            this.SuspendLayout();
            // 
            // picWebCam
            // 
            this.picWebCam.Location = new System.Drawing.Point(20, 22);
            this.picWebCam.Name = "picWebCam";
            this.picWebCam.Size = new System.Drawing.Size(251, 156);
            this.picWebCam.TabIndex = 0;
            this.picWebCam.TabStop = false;
            // 
            // BtnDecode
            // 
            this.BtnDecode.Location = new System.Drawing.Point(191, 204);
            this.BtnDecode.Name = "BtnDecode";
            this.BtnDecode.Size = new System.Drawing.Size(80, 31);
            this.BtnDecode.TabIndex = 1;
            this.BtnDecode.Text = "Decode";
            this.BtnDecode.UseVisualStyleBackColor = true;
            this.BtnDecode.Click += new System.EventHandler(this.BtnDecode_Click);
            // 
            // txtTypeWebCam
            // 
            this.txtTypeWebCam.Location = new System.Drawing.Point(27, 195);
            this.txtTypeWebCam.Name = "txtTypeWebCam";
            this.txtTypeWebCam.Size = new System.Drawing.Size(122, 20);
            this.txtTypeWebCam.TabIndex = 2;
            // 
            // txtContentWebCam
            // 
            this.txtContentWebCam.Location = new System.Drawing.Point(27, 221);
            this.txtContentWebCam.Name = "txtContentWebCam";
            this.txtContentWebCam.Size = new System.Drawing.Size(122, 20);
            this.txtContentWebCam.TabIndex = 3;
            // 
            // FormBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.txtContentWebCam);
            this.Controls.Add(this.txtTypeWebCam);
            this.Controls.Add(this.BtnDecode);
            this.Controls.Add(this.picWebCam);
            this.Name = "FormBar";
            this.Text = "FormBar";
            ((System.ComponentModel.ISupportInitialize)(this.picWebCam)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picWebCam;
        private System.Windows.Forms.Button BtnDecode;
        private System.Windows.Forms.TextBox txtTypeWebCam;
        private System.Windows.Forms.TextBox txtContentWebCam;
    }
}