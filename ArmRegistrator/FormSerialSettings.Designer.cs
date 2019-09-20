namespace ArmRegistrator
{
    partial class FormSerialSettings
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
            this.label1 = new System.Windows.Forms.Label();
            this.grpOut = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbPortOut = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbPortIn = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbBaudRateRModem = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbPortRModem = new System.Windows.Forms.ComboBox();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnOk = new System.Windows.Forms.Button();
            this.FlgTwoReaders = new System.Windows.Forms.CheckBox();
            this.grpOut.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Скорость";
            // 
            // grpOut
            // 
            this.grpOut.Controls.Add(this.label3);
            this.grpOut.Controls.Add(this.cbPortOut);
            this.grpOut.Location = new System.Drawing.Point(12, 91);
            this.grpOut.Name = "grpOut";
            this.grpOut.Size = new System.Drawing.Size(185, 51);
            this.grpOut.TabIndex = 2;
            this.grpOut.TabStop = false;
            this.grpOut.Text = "Выдача";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Порт";
            // 
            // cbPortOut
            // 
            this.cbPortOut.FormattingEnabled = true;
            this.cbPortOut.Location = new System.Drawing.Point(76, 15);
            this.cbPortOut.Name = "cbPortOut";
            this.cbPortOut.Size = new System.Drawing.Size(100, 21);
            this.cbPortOut.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cbPortIn);
            this.groupBox2.Location = new System.Drawing.Point(12, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(185, 51);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Сдача";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Порт";
            // 
            // cbPortIn
            // 
            this.cbPortIn.FormattingEnabled = true;
            this.cbPortIn.Location = new System.Drawing.Point(76, 15);
            this.cbPortIn.Name = "cbPortIn";
            this.cbPortIn.Size = new System.Drawing.Size(100, 21);
            this.cbPortIn.TabIndex = 4;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbBaudRateRModem);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.cbPortRModem);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(12, 142);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(185, 84);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Радиомодем";
            // 
            // cbBaudRateRModem
            // 
            this.cbBaudRateRModem.FormattingEnabled = true;
            this.cbBaudRateRModem.Location = new System.Drawing.Point(76, 53);
            this.cbBaudRateRModem.Name = "cbBaudRateRModem";
            this.cbBaudRateRModem.Size = new System.Drawing.Size(100, 21);
            this.cbBaudRateRModem.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Порт";
            // 
            // cbPortRModem
            // 
            this.cbPortRModem.FormattingEnabled = true;
            this.cbPortRModem.Location = new System.Drawing.Point(76, 22);
            this.cbPortRModem.Name = "cbPortRModem";
            this.cbPortRModem.Size = new System.Drawing.Size(100, 21);
            this.cbPortRModem.TabIndex = 4;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.BtnCancel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnCancel.Location = new System.Drawing.Point(123, 233);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(74, 68);
            this.BtnCancel.TabIndex = 11;
            this.BtnCancel.Text = "Отмена";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnOk
            // 
            this.BtnOk.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.BtnOk.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnOk.Location = new System.Drawing.Point(12, 233);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(74, 68);
            this.BtnOk.TabIndex = 10;
            this.BtnOk.Text = "Ок";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // FlgTwoReaders
            // 
            this.FlgTwoReaders.AutoSize = true;
            this.FlgTwoReaders.Location = new System.Drawing.Point(15, 67);
            this.FlgTwoReaders.Name = "FlgTwoReaders";
            this.FlgTwoReaders.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.FlgTwoReaders.Size = new System.Drawing.Size(115, 17);
            this.FlgTwoReaders.TabIndex = 6;
            this.FlgTwoReaders.Text = "Два считывателя";
            this.FlgTwoReaders.UseVisualStyleBackColor = true;
            this.FlgTwoReaders.CheckedChanged += new System.EventHandler(this.FlgTwoReaders_CheckedChanged);
            // 
            // FormSerialSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 306);
            this.ControlBox = false;
            this.Controls.Add(this.FlgTwoReaders);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grpOut);
            this.Name = "FormSerialSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройка последовательных портов";
            this.Load += new System.EventHandler(this.FormSerialSettings_Load);
            this.grpOut.ResumeLayout(false);
            this.grpOut.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpOut;
        private System.Windows.Forms.ComboBox cbPortOut;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbPortIn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbPortRModem;
        private System.Windows.Forms.ComboBox cbBaudRateRModem;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.CheckBox FlgTwoReaders;
    }
}