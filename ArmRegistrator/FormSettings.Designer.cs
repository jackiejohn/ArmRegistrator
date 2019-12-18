namespace ArmRegistrator
{
    partial class FormSettings
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
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnSerial = new System.Windows.Forms.Button();
            this.BtnDbConnect = new System.Windows.Forms.Button();
            this.BtnAbout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnCancel.Location = new System.Drawing.Point(0, 221);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(117, 68);
            this.BtnCancel.TabIndex = 16;
            this.BtnCancel.Text = "Закрыть";
            this.BtnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSerial
            // 
            this.BtnSerial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSerial.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnSerial.Location = new System.Drawing.Point(0, 73);
            this.BtnSerial.Name = "BtnSerial";
            this.BtnSerial.Size = new System.Drawing.Size(117, 68);
            this.BtnSerial.TabIndex = 14;
            this.BtnSerial.Text = "Коммуникации";
            this.BtnSerial.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnSerial.UseVisualStyleBackColor = true;
            this.BtnSerial.Click += new System.EventHandler(this.BtnSerial_Click);
            // 
            // BtnDbConnect
            // 
            this.BtnDbConnect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnDbConnect.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnDbConnect.Location = new System.Drawing.Point(0, -1);
            this.BtnDbConnect.Name = "BtnDbConnect";
            this.BtnDbConnect.Size = new System.Drawing.Size(117, 68);
            this.BtnDbConnect.TabIndex = 13;
            this.BtnDbConnect.Text = "Подключение к БД";
            this.BtnDbConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnDbConnect.UseVisualStyleBackColor = true;
            this.BtnDbConnect.Click += new System.EventHandler(this.BtnDbConnect_Click);
            // 
            // BtnAbout
            // 
            this.BtnAbout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAbout.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnAbout.Image = global::ArmRegistrator.Properties.Resources.LOGO2;
            this.BtnAbout.Location = new System.Drawing.Point(0, 147);
            this.BtnAbout.Name = "BtnAbout";
            this.BtnAbout.Size = new System.Drawing.Size(117, 68);
            this.BtnAbout.TabIndex = 15;
            this.BtnAbout.Text = "О программе";
            this.BtnAbout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnAbout.UseVisualStyleBackColor = true;
            this.BtnAbout.Click += new System.EventHandler(this.BtnAbout_Click);
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(119, 290);
            this.ControlBox = false;
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnAbout);
            this.Controls.Add(this.BtnSerial);
            this.Controls.Add(this.BtnDbConnect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Выбор настроек";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnSerial;
        private System.Windows.Forms.Button BtnDbConnect;
        private System.Windows.Forms.Button BtnAbout;
        private System.Windows.Forms.Button BtnCancel;
    }
}