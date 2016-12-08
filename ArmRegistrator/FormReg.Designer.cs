namespace ArmRegistrator
{
    partial class FormReg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReg));
            this.PanelGrids = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ToolStripTextBoxSearch = new System.Windows.Forms.ToolStripTextBox();
            this.ToolStripButtonSearch = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripLabelHoursPre = new System.Windows.Forms.ToolStripLabel();
            this.ToolStripTextBoxHours = new System.Windows.Forms.ToolStripTextBox();
            this.ToolStripLabelHoursPost = new System.Windows.Forms.ToolStripLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.TrackerView = new System.Windows.Forms.DataGridView();
            this.button8 = new System.Windows.Forms.Button();
            this.BtnNotInField = new System.Windows.Forms.Button();
            this.BtnInField = new System.Windows.Forms.Button();
            this.CardView = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnDbConfig = new System.Windows.Forms.Button();
            this.BtnDbConnect = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.FlagTechnics = new System.Windows.Forms.CheckBox();
            this.BtnAllObjectType = new System.Windows.Forms.Button();
            this.FlagTransport = new System.Windows.Forms.CheckBox();
            this.FlagPersonal = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.FlagLongTimeInField = new System.Windows.Forms.CheckBox();
            this.FlagIsNotInField = new System.Windows.Forms.CheckBox();
            this.FlagIsInField = new System.Windows.Forms.CheckBox();
            this.BtnAllField = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.BtnRModuleConnect = new System.Windows.Forms.Button();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.PanelGrids.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackerView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CardView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelGrids
            // 
            this.PanelGrids.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelGrids.Controls.Add(this.toolStrip1);
            this.PanelGrids.Controls.Add(this.splitContainer1);
            this.PanelGrids.Location = new System.Drawing.Point(3, 119);
            this.PanelGrids.Name = "PanelGrids";
            this.PanelGrids.Size = new System.Drawing.Size(943, 444);
            this.PanelGrids.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripTextBoxSearch,
            this.ToolStripButtonSearch,
            this.toolStripSeparator2,
            this.ToolStripLabelHoursPre,
            this.ToolStripTextBoxHours,
            this.ToolStripLabelHoursPost});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(943, 50);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ToolStripTextBoxSearch
            // 
            this.ToolStripTextBoxSearch.AutoSize = false;
            this.ToolStripTextBoxSearch.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ToolStripTextBoxSearch.Name = "ToolStripTextBoxSearch";
            this.ToolStripTextBoxSearch.Size = new System.Drawing.Size(250, 27);
            this.ToolStripTextBoxSearch.ToolTipText = "Фильтр объекта";
            this.ToolStripTextBoxSearch.TextChanged += new System.EventHandler(this.ToolStripTextBoxSearch_TextChanged);
            // 
            // ToolStripButtonSearch
            // 
            this.ToolStripButtonSearch.AutoSize = false;
            this.ToolStripButtonSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButtonSearch.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButtonSearch.Image")));
            this.ToolStripButtonSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonSearch.Name = "ToolStripButtonSearch";
            this.ToolStripButtonSearch.Size = new System.Drawing.Size(28, 28);
            this.ToolStripButtonSearch.Text = "toolStripButton1";
            this.ToolStripButtonSearch.ToolTipText = "Очистить фильтр объектов";
            this.ToolStripButtonSearch.Click += new System.EventHandler(this.ToolStripButtonSearch_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 50);
            // 
            // ToolStripLabelHoursPre
            // 
            this.ToolStripLabelHoursPre.Name = "ToolStripLabelHoursPre";
            this.ToolStripLabelHoursPre.Size = new System.Drawing.Size(95, 47);
            this.ToolStripLabelHoursPre.Text = "На смене более";
            this.ToolStripLabelHoursPre.Visible = false;
            // 
            // ToolStripTextBoxHours
            // 
            this.ToolStripTextBoxHours.Font = new System.Drawing.Font("Tahoma", 12F);
            this.ToolStripTextBoxHours.MaxLength = 2;
            this.ToolStripTextBoxHours.Name = "ToolStripTextBoxHours";
            this.ToolStripTextBoxHours.Size = new System.Drawing.Size(25, 50);
            this.ToolStripTextBoxHours.Text = "00";
            this.ToolStripTextBoxHours.Visible = false;
            this.ToolStripTextBoxHours.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ToolStripTextBoxHours_KeyDown);
            this.ToolStripTextBoxHours.Validating += new System.ComponentModel.CancelEventHandler(this.ToolStripTextBoxHours_Validating);
            this.ToolStripTextBoxHours.Validated += new System.EventHandler(this.ToolStripTextBoxHours_Validated);
            // 
            // ToolStripLabelHoursPost
            // 
            this.ToolStripLabelHoursPost.Name = "ToolStripLabelHoursPost";
            this.ToolStripLabelHoursPost.Size = new System.Drawing.Size(53, 47);
            this.ToolStripLabelHoursPost.Text = "часа(ов)";
            this.ToolStripLabelHoursPost.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.TrackerView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.button8);
            this.splitContainer1.Panel2.Controls.Add(this.BtnNotInField);
            this.splitContainer1.Panel2.Controls.Add(this.BtnInField);
            this.splitContainer1.Panel2.Controls.Add(this.CardView);
            this.splitContainer1.Panel2MinSize = 100;
            this.splitContainer1.Size = new System.Drawing.Size(940, 416);
            this.splitContainer1.SplitterDistance = 645;
            this.splitContainer1.TabIndex = 0;
            // 
            // TrackerView
            // 
            this.TrackerView.AllowUserToAddRows = false;
            this.TrackerView.AllowUserToDeleteRows = false;
            this.TrackerView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TrackerView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TrackerView.Location = new System.Drawing.Point(0, 28);
            this.TrackerView.Name = "TrackerView";
            this.TrackerView.ReadOnly = true;
            this.TrackerView.Size = new System.Drawing.Size(645, 388);
            this.TrackerView.TabIndex = 0;
            this.TrackerView.SelectionChanged += new System.EventHandler(this.TrackerView_SelectionChanged);
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button8.Enabled = false;
            this.button8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button8.Location = new System.Drawing.Point(217, 342);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(74, 68);
            this.button8.TabIndex = 10;
            this.button8.Text = "Замена устройства";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // BtnNotInField
            // 
            this.BtnNotInField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnNotInField.Enabled = false;
            this.BtnNotInField.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnNotInField.Location = new System.Drawing.Point(89, 342);
            this.BtnNotInField.Name = "BtnNotInField";
            this.BtnNotInField.Size = new System.Drawing.Size(74, 68);
            this.BtnNotInField.TabIndex = 9;
            this.BtnNotInField.Text = "Закончить смену";
            this.BtnNotInField.UseVisualStyleBackColor = true;
            this.BtnNotInField.Click += new System.EventHandler(this.BtnNotInField_Click);
            // 
            // BtnInField
            // 
            this.BtnInField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnInField.Enabled = false;
            this.BtnInField.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnInField.Location = new System.Drawing.Point(3, 342);
            this.BtnInField.Name = "BtnInField";
            this.BtnInField.Size = new System.Drawing.Size(74, 68);
            this.BtnInField.TabIndex = 8;
            this.BtnInField.Text = "Начать смену";
            this.BtnInField.UseVisualStyleBackColor = true;
            this.BtnInField.Click += new System.EventHandler(this.BtnInField_Click);
            // 
            // CardView
            // 
            this.CardView.AllowUserToAddRows = false;
            this.CardView.AllowUserToDeleteRows = false;
            this.CardView.AllowUserToResizeRows = false;
            this.CardView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.CardView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CardView.ColumnHeadersVisible = false;
            this.CardView.Location = new System.Drawing.Point(3, 28);
            this.CardView.Name = "CardView";
            this.CardView.ReadOnly = true;
            this.CardView.RowHeadersVisible = false;
            this.CardView.Size = new System.Drawing.Size(286, 305);
            this.CardView.TabIndex = 0;
            this.CardView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.CardView_CellPainting);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.BtnDbConfig);
            this.groupBox1.Controls.Add(this.BtnDbConnect);
            this.groupBox1.Location = new System.Drawing.Point(731, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(206, 98);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "База данных";
            // 
            // BtnDbConfig
            // 
            this.BtnDbConfig.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnDbConfig.Image = ((System.Drawing.Image)(resources.GetObject("BtnDbConfig.Image")));
            this.BtnDbConfig.Location = new System.Drawing.Point(102, 21);
            this.BtnDbConfig.Name = "BtnDbConfig";
            this.BtnDbConfig.Size = new System.Drawing.Size(96, 68);
            this.BtnDbConfig.TabIndex = 12;
            this.BtnDbConfig.Text = "Настройка";
            this.BtnDbConfig.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnDbConfig.UseVisualStyleBackColor = true;
            this.BtnDbConfig.Click += new System.EventHandler(this.BtnDbConfig_Click);
            // 
            // BtnDbConnect
            // 
            this.BtnDbConnect.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnDbConnect.Image = global::ArmRegistrator.Properties.Resources.ImageConnectionDeactive;
            this.BtnDbConnect.Location = new System.Drawing.Point(6, 21);
            this.BtnDbConnect.Name = "BtnDbConnect";
            this.BtnDbConnect.Size = new System.Drawing.Size(96, 68);
            this.BtnDbConnect.TabIndex = 11;
            this.BtnDbConnect.Text = "Подключение";
            this.BtnDbConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnDbConnect.UseVisualStyleBackColor = true;
            this.BtnDbConnect.Click += new System.EventHandler(this.BtnDbConnect_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.FlagTechnics);
            this.groupBox2.Controls.Add(this.BtnAllObjectType);
            this.groupBox2.Controls.Add(this.FlagTransport);
            this.groupBox2.Controls.Add(this.FlagPersonal);
            this.groupBox2.Location = new System.Drawing.Point(3, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(285, 98);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Управление списком";
            // 
            // FlagTechnics
            // 
            this.FlagTechnics.Appearance = System.Windows.Forms.Appearance.Button;
            this.FlagTechnics.Checked = true;
            this.FlagTechnics.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FlagTechnics.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FlagTechnics.Image = ((System.Drawing.Image)(resources.GetObject("FlagTechnics.Image")));
            this.FlagTechnics.Location = new System.Drawing.Point(210, 21);
            this.FlagTechnics.Name = "FlagTechnics";
            this.FlagTechnics.Size = new System.Drawing.Size(68, 68);
            this.FlagTechnics.TabIndex = 13;
            this.FlagTechnics.Text = "Техника";
            this.FlagTechnics.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.FlagTechnics.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.FlagTechnics.UseVisualStyleBackColor = true;
            this.FlagTechnics.CheckedChanged += new System.EventHandler(this.FlagTechnics_CheckedChanged);
            // 
            // BtnAllObjectType
            // 
            this.BtnAllObjectType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BtnAllObjectType.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnAllObjectType.Image = ((System.Drawing.Image)(resources.GetObject("BtnAllObjectType.Image")));
            this.BtnAllObjectType.Location = new System.Drawing.Point(6, 21);
            this.BtnAllObjectType.Name = "BtnAllObjectType";
            this.BtnAllObjectType.Size = new System.Drawing.Size(68, 68);
            this.BtnAllObjectType.TabIndex = 1;
            this.BtnAllObjectType.Text = "Все";
            this.BtnAllObjectType.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnAllObjectType.UseVisualStyleBackColor = true;
            this.BtnAllObjectType.Click += new System.EventHandler(this.BtnAllObjectType_Click);
            // 
            // FlagTransport
            // 
            this.FlagTransport.Appearance = System.Windows.Forms.Appearance.Button;
            this.FlagTransport.Checked = true;
            this.FlagTransport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FlagTransport.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FlagTransport.Image = ((System.Drawing.Image)(resources.GetObject("FlagTransport.Image")));
            this.FlagTransport.Location = new System.Drawing.Point(142, 21);
            this.FlagTransport.Name = "FlagTransport";
            this.FlagTransport.Size = new System.Drawing.Size(68, 68);
            this.FlagTransport.TabIndex = 12;
            this.FlagTransport.Text = "Транспорт";
            this.FlagTransport.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.FlagTransport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.FlagTransport.UseVisualStyleBackColor = true;
            this.FlagTransport.CheckedChanged += new System.EventHandler(this.FlagTransport_CheckedChanged);
            // 
            // FlagPersonal
            // 
            this.FlagPersonal.Appearance = System.Windows.Forms.Appearance.Button;
            this.FlagPersonal.Checked = true;
            this.FlagPersonal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FlagPersonal.Image = ((System.Drawing.Image)(resources.GetObject("FlagPersonal.Image")));
            this.FlagPersonal.Location = new System.Drawing.Point(74, 21);
            this.FlagPersonal.Name = "FlagPersonal";
            this.FlagPersonal.Size = new System.Drawing.Size(68, 68);
            this.FlagPersonal.TabIndex = 11;
            this.FlagPersonal.Text = "Персонал";
            this.FlagPersonal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.FlagPersonal.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.FlagPersonal.UseVisualStyleBackColor = true;
            this.FlagPersonal.Click += new System.EventHandler(this.FlagPersonal_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.FlagLongTimeInField);
            this.groupBox3.Controls.Add(this.FlagIsNotInField);
            this.groupBox3.Controls.Add(this.FlagIsInField);
            this.groupBox3.Controls.Add(this.BtnAllField);
            this.groupBox3.Location = new System.Drawing.Point(294, 15);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(284, 98);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Фильтр";
            // 
            // FlagLongTimeInField
            // 
            this.FlagLongTimeInField.Appearance = System.Windows.Forms.Appearance.Button;
            this.FlagLongTimeInField.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FlagLongTimeInField.Image = ((System.Drawing.Image)(resources.GetObject("FlagLongTimeInField.Image")));
            this.FlagLongTimeInField.Location = new System.Drawing.Point(210, 21);
            this.FlagLongTimeInField.Name = "FlagLongTimeInField";
            this.FlagLongTimeInField.Size = new System.Drawing.Size(68, 68);
            this.FlagLongTimeInField.TabIndex = 16;
            this.FlagLongTimeInField.Text = "Более";
            this.FlagLongTimeInField.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.FlagLongTimeInField.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.FlagLongTimeInField.UseVisualStyleBackColor = true;
            this.FlagLongTimeInField.Click += new System.EventHandler(this.FlagLongTimeInField_Click);
            // 
            // FlagIsNotInField
            // 
            this.FlagIsNotInField.Appearance = System.Windows.Forms.Appearance.Button;
            this.FlagIsNotInField.Checked = true;
            this.FlagIsNotInField.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FlagIsNotInField.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FlagIsNotInField.Image = ((System.Drawing.Image)(resources.GetObject("FlagIsNotInField.Image")));
            this.FlagIsNotInField.Location = new System.Drawing.Point(142, 21);
            this.FlagIsNotInField.Name = "FlagIsNotInField";
            this.FlagIsNotInField.Size = new System.Drawing.Size(68, 68);
            this.FlagIsNotInField.TabIndex = 15;
            this.FlagIsNotInField.Text = "На складе";
            this.FlagIsNotInField.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.FlagIsNotInField.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.FlagIsNotInField.UseVisualStyleBackColor = true;
            this.FlagIsNotInField.Click += new System.EventHandler(this.FlagIsNotInField_Click);
            // 
            // FlagIsInField
            // 
            this.FlagIsInField.Appearance = System.Windows.Forms.Appearance.Button;
            this.FlagIsInField.Checked = true;
            this.FlagIsInField.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FlagIsInField.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FlagIsInField.Image = ((System.Drawing.Image)(resources.GetObject("FlagIsInField.Image")));
            this.FlagIsInField.Location = new System.Drawing.Point(74, 21);
            this.FlagIsInField.Name = "FlagIsInField";
            this.FlagIsInField.Size = new System.Drawing.Size(68, 68);
            this.FlagIsInField.TabIndex = 14;
            this.FlagIsInField.Text = "Выданы";
            this.FlagIsInField.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.FlagIsInField.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.FlagIsInField.UseVisualStyleBackColor = true;
            this.FlagIsInField.Click += new System.EventHandler(this.FlagIsInField_Click);
            // 
            // BtnAllField
            // 
            this.BtnAllField.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnAllField.Image = ((System.Drawing.Image)(resources.GetObject("BtnAllField.Image")));
            this.BtnAllField.Location = new System.Drawing.Point(6, 21);
            this.BtnAllField.Name = "BtnAllField";
            this.BtnAllField.Size = new System.Drawing.Size(68, 68);
            this.BtnAllField.TabIndex = 5;
            this.BtnAllField.Text = "Все";
            this.BtnAllField.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnAllField.UseVisualStyleBackColor = true;
            this.BtnAllField.Click += new System.EventHandler(this.BtnAllField_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.BtnRModuleConnect);
            this.groupBox4.Location = new System.Drawing.Point(584, 15);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(89, 98);
            this.groupBox4.TabIndex = 18;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Радиомодуль";
            // 
            // BtnRModuleConnect
            // 
            this.BtnRModuleConnect.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnRModuleConnect.Image = global::ArmRegistrator.Properties.Resources.RModuleNotConnected;
            this.BtnRModuleConnect.Location = new System.Drawing.Point(10, 21);
            this.BtnRModuleConnect.Name = "BtnRModuleConnect";
            this.BtnRModuleConnect.Size = new System.Drawing.Size(68, 68);
            this.BtnRModuleConnect.TabIndex = 17;
            this.BtnRModuleConnect.Text = "Связь";
            this.BtnRModuleConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnRModuleConnect.UseVisualStyleBackColor = true;
            this.BtnRModuleConnect.Click += new System.EventHandler(this.BtnRModuleConnect_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(86, 47);
            this.toolStripLabel1.Text = "toolStripLabel1";
            // 
            // FormReg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 566);
            this.Controls.Add(this.PanelGrids);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(965, 604);
            this.Name = "FormReg";
            this.Text = "АРМ регистрации";
            this.Load += new System.EventHandler(this.FormReg_Load);
            this.SizeChanged += new System.EventHandler(this.FormReg_SizeChanged);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormReg_FormClosed);
            this.ResizeEnd += new System.EventHandler(this.FormReg_ResizeEnd);
            this.PanelGrids.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TrackerView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CardView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelGrids;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView TrackerView;
        private System.Windows.Forms.DataGridView CardView;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button BtnNotInField;
        private System.Windows.Forms.Button BtnInField;
        private System.Windows.Forms.Button BtnAllObjectType;
        private System.Windows.Forms.Button BtnAllField;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button BtnDbConnect;
        private System.Windows.Forms.Button BtnDbConfig;
        private System.Windows.Forms.ToolStripTextBox ToolStripTextBoxSearch;
        private System.Windows.Forms.ToolStripButton ToolStripButtonSearch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.CheckBox FlagPersonal;
        private System.Windows.Forms.CheckBox FlagTechnics;
        private System.Windows.Forms.CheckBox FlagTransport;
        private System.Windows.Forms.CheckBox FlagIsInField;
        private System.Windows.Forms.CheckBox FlagIsNotInField;
        private System.Windows.Forms.CheckBox FlagLongTimeInField;
        private System.Windows.Forms.ToolStripTextBox ToolStripTextBoxHours;
        private System.Windows.Forms.ToolStripLabel ToolStripLabelHoursPost;
        private System.Windows.Forms.ToolStripLabel ToolStripLabelHoursPre;
        private System.Windows.Forms.Button BtnRModuleConnect;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    }
}

