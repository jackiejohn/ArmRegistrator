namespace ArmRegistrator.Photo
{
    partial class FormPhoto
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
            this.PhotoOut = new System.Windows.Forms.PictureBox();
            this.PhotoIn = new System.Windows.Forms.PictureBox();
            this.BackLabelOut = new System.Windows.Forms.Label();
            this.BackLabelIn = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblOutFio = new System.Windows.Forms.Label();
            this.lblOutDolj = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblOutId = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblInId = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblInDolj = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblInFio = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PhotoOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhotoIn)).BeginInit();
            this.SuspendLayout();
            // 
            // PhotoOut
            // 
            this.PhotoOut.Location = new System.Drawing.Point(5, 42);
            this.PhotoOut.Name = "PhotoOut";
            this.PhotoOut.Size = new System.Drawing.Size(373, 249);
            this.PhotoOut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PhotoOut.TabIndex = 14;
            this.PhotoOut.TabStop = false;
            // 
            // PhotoIn
            // 
            this.PhotoIn.Location = new System.Drawing.Point(382, 42);
            this.PhotoIn.Name = "PhotoIn";
            this.PhotoIn.Size = new System.Drawing.Size(373, 249);
            this.PhotoIn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PhotoIn.TabIndex = 15;
            this.PhotoIn.TabStop = false;
            // 
            // BackLabelOut
            // 
            this.BackLabelOut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BackLabelOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BackLabelOut.Location = new System.Drawing.Point(4, 12);
            this.BackLabelOut.Name = "BackLabelOut";
            this.BackLabelOut.Size = new System.Drawing.Size(375, 375);
            this.BackLabelOut.TabIndex = 16;
            this.BackLabelOut.Text = "Выдача";
            this.BackLabelOut.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // BackLabelIn
            // 
            this.BackLabelIn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BackLabelIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BackLabelIn.Location = new System.Drawing.Point(381, 12);
            this.BackLabelIn.Name = "BackLabelIn";
            this.BackLabelIn.Size = new System.Drawing.Size(375, 375);
            this.BackLabelIn.TabIndex = 17;
            this.BackLabelIn.Text = "Сдача";
            this.BackLabelIn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(12, 296);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 16);
            this.label3.TabIndex = 18;
            this.label3.Text = "ФИО:";
            // 
            // lblOutFio
            // 
            this.lblOutFio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblOutFio.Location = new System.Drawing.Point(95, 292);
            this.lblOutFio.Name = "lblOutFio";
            this.lblOutFio.Size = new System.Drawing.Size(263, 20);
            this.lblOutFio.TabIndex = 19;
            this.lblOutFio.Text = "фамилия имя отчество";
            // 
            // lblOutDolj
            // 
            this.lblOutDolj.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblOutDolj.Location = new System.Drawing.Point(95, 324);
            this.lblOutDolj.Name = "lblOutDolj";
            this.lblOutDolj.Size = new System.Drawing.Size(263, 20);
            this.lblOutDolj.TabIndex = 21;
            this.lblOutDolj.Text = "должность";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 328);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 16);
            this.label2.TabIndex = 20;
            this.label2.Text = "Должность:";
            // 
            // lblOutId
            // 
            this.lblOutId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblOutId.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblOutId.Location = new System.Drawing.Point(95, 357);
            this.lblOutId.Name = "lblOutId";
            this.lblOutId.Size = new System.Drawing.Size(263, 20);
            this.lblOutId.TabIndex = 23;
            this.lblOutId.Text = "ID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label5.Location = new System.Drawing.Point(12, 357);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 20);
            this.label5.TabIndex = 22;
            this.label5.Text = "ID:";
            // 
            // lblInId
            // 
            this.lblInId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblInId.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblInId.Location = new System.Drawing.Point(480, 357);
            this.lblInId.Name = "lblInId";
            this.lblInId.Size = new System.Drawing.Size(263, 20);
            this.lblInId.TabIndex = 29;
            this.lblInId.Text = "ID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label4.Location = new System.Drawing.Point(397, 357);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 20);
            this.label4.TabIndex = 28;
            this.label4.Text = "ID:";
            // 
            // lblInDolj
            // 
            this.lblInDolj.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblInDolj.Location = new System.Drawing.Point(480, 324);
            this.lblInDolj.Name = "lblInDolj";
            this.lblInDolj.Size = new System.Drawing.Size(263, 20);
            this.lblInDolj.TabIndex = 27;
            this.lblInDolj.Text = "должность";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(397, 328);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 16);
            this.label7.TabIndex = 26;
            this.label7.Text = "Должность:";
            // 
            // lblInFio
            // 
            this.lblInFio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblInFio.Location = new System.Drawing.Point(480, 292);
            this.lblInFio.Name = "lblInFio";
            this.lblInFio.Size = new System.Drawing.Size(263, 20);
            this.lblInFio.TabIndex = 25;
            this.lblInFio.Text = "фамилия имя отчество";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(397, 296);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 16);
            this.label9.TabIndex = 24;
            this.label9.Text = "ФИО:";
            // 
            // FormPhoto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 392);
            this.ControlBox = false;
            this.Controls.Add(this.lblInId);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblInDolj);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblInFio);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblOutId);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblOutDolj);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblOutFio);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PhotoOut);
            this.Controls.Add(this.PhotoIn);
            this.Controls.Add(this.BackLabelOut);
            this.Controls.Add(this.BackLabelIn);
            this.Name = "FormPhoto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Сотрудники";
            ((System.ComponentModel.ISupportInitialize)(this.PhotoOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhotoIn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PhotoOut;
        private System.Windows.Forms.PictureBox PhotoIn;
        private System.Windows.Forms.Label BackLabelOut;
        private System.Windows.Forms.Label BackLabelIn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblOutFio;
        private System.Windows.Forms.Label lblOutDolj;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblOutId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblInId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblInDolj;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblInFio;
        private System.Windows.Forms.Label label9;
    }
}