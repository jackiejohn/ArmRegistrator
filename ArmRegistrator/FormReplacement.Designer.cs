namespace ArmRegistrator
{
    partial class FormReplacement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReplacement));
            this.ObjectView = new System.Windows.Forms.DataGridView();
            this.ObjectMayView = new System.Windows.Forms.DataGridView();
            this.BtnUnReplace = new System.Windows.Forms.Button();
            this.BtnReplace = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectMayView)).BeginInit();
            this.SuspendLayout();
            // 
            // ObjectView
            // 
            this.ObjectView.AllowUserToAddRows = false;
            this.ObjectView.AllowUserToDeleteRows = false;
            this.ObjectView.AllowUserToResizeColumns = false;
            this.ObjectView.AllowUserToResizeRows = false;
            this.ObjectView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ObjectView.Location = new System.Drawing.Point(12, 26);
            this.ObjectView.Name = "ObjectView";
            this.ObjectView.ReadOnly = true;
            this.ObjectView.Size = new System.Drawing.Size(752, 203);
            this.ObjectView.TabIndex = 0;
            this.ObjectView.SelectionChanged += new System.EventHandler(this.ObjectView_SelectionChanged);
            // 
            // ObjectMayView
            // 
            this.ObjectMayView.AllowUserToAddRows = false;
            this.ObjectMayView.AllowUserToDeleteRows = false;
            this.ObjectMayView.AllowUserToResizeRows = false;
            this.ObjectMayView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ObjectMayView.Location = new System.Drawing.Point(12, 251);
            this.ObjectMayView.Name = "ObjectMayView";
            this.ObjectMayView.ReadOnly = true;
            this.ObjectMayView.Size = new System.Drawing.Size(752, 203);
            this.ObjectMayView.TabIndex = 1;
            this.ObjectMayView.SelectionChanged += new System.EventHandler(this.ObjectMayView_SelectionChanged);
            // 
            // BtnUnReplace
            // 
            this.BtnUnReplace.Image = ((System.Drawing.Image)(resources.GetObject("BtnUnReplace.Image")));
            this.BtnUnReplace.Location = new System.Drawing.Point(770, 251);
            this.BtnUnReplace.Name = "BtnUnReplace";
            this.BtnUnReplace.Size = new System.Drawing.Size(68, 68);
            this.BtnUnReplace.TabIndex = 3;
            this.BtnUnReplace.Text = "Отмена";
            this.BtnUnReplace.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnUnReplace.UseVisualStyleBackColor = true;
            this.BtnUnReplace.Click += new System.EventHandler(this.BtnUnReplace_Click);
            // 
            // BtnReplace
            // 
            this.BtnReplace.Image = ((System.Drawing.Image)(resources.GetObject("BtnReplace.Image")));
            this.BtnReplace.Location = new System.Drawing.Point(770, 161);
            this.BtnReplace.Name = "BtnReplace";
            this.BtnReplace.Size = new System.Drawing.Size(68, 68);
            this.BtnReplace.TabIndex = 4;
            this.BtnReplace.Text = "Подмена";
            this.BtnReplace.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnReplace.UseVisualStyleBackColor = true;
            this.BtnReplace.Click += new System.EventHandler(this.BtnReplace_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Объекты системы";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 235);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Подменные устройства";
            // 
            // FormReplacement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 459);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnReplace);
            this.Controls.Add(this.BtnUnReplace);
            this.Controls.Add(this.ObjectMayView);
            this.Controls.Add(this.ObjectView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormReplacement";
            this.ShowInTaskbar = false;
            this.Text = "Замена объектов системы подменными устройствами";
            this.Load += new System.EventHandler(this.FormReplacement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ObjectView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectMayView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ObjectView;
        private System.Windows.Forms.DataGridView ObjectMayView;
        private System.Windows.Forms.Button BtnUnReplace;
        private System.Windows.Forms.Button BtnReplace;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}