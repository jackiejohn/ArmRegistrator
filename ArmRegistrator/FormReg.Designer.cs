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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReg));
            this.PanelGrids = new System.Windows.Forms.Panel();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.ToolStripTextBoxSearch = new System.Windows.Forms.ToolStripTextBox();
            this.ToolStripButtonSearch = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.TrackerView = new System.Windows.Forms.DataGridView();
            this.button8 = new System.Windows.Forms.Button();
            this.BtnNotInField = new System.Windows.Forms.Button();
            this.BtnInField = new System.Windows.Forms.Button();
            this.CardView = new System.Windows.Forms.DataGridView();
            this.BtnAllField = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnConnectDbConfig = new System.Windows.Forms.Button();
            this.BtnConnect = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.FlagTechnics = new System.Windows.Forms.CheckBox();
            this.BtnAllObjectType = new System.Windows.Forms.Button();
            this.FlagTransport = new System.Windows.Forms.CheckBox();
            this.FlagPersonal = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.FlagLongTimeInField = new System.Windows.Forms.CheckBox();
            this.FlagIsNotInField = new System.Windows.Forms.CheckBox();
            this.FlagIsInField = new System.Windows.Forms.CheckBox();
            this.PanelGrids.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackerView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CardView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelGrids
            // 
            this.PanelGrids.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelGrids.Controls.Add(this.bindingNavigator1);
            this.PanelGrids.Controls.Add(this.splitContainer1);
            this.PanelGrids.Location = new System.Drawing.Point(3, 119);
            this.PanelGrids.Name = "PanelGrids";
            this.PanelGrids.Size = new System.Drawing.Size(943, 444);
            this.PanelGrids.TabIndex = 0;
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = this.bindingNavigatorAddNewItem;
            this.bindingNavigator1.AutoSize = false;
            this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator1.DeleteItem = this.bindingNavigatorDeleteItem;
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripTextBoxSearch,
            this.ToolStripButtonSearch,
            this.toolStripSeparator2,
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem});
            this.bindingNavigator1.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator1.Size = new System.Drawing.Size(943, 50);
            this.bindingNavigator1.Stretch = true;
            this.bindingNavigator1.TabIndex = 1;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 47);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            this.bindingNavigatorAddNewItem.Visible = false;
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 47);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            this.bindingNavigatorCountItem.Visible = false;
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 47);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            this.bindingNavigatorDeleteItem.Visible = false;
            // 
            // ToolStripTextBoxSearch
            // 
            this.ToolStripTextBoxSearch.AutoSize = false;
            this.ToolStripTextBoxSearch.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ToolStripTextBoxSearch.Name = "ToolStripTextBoxSearch";
            this.ToolStripTextBoxSearch.Size = new System.Drawing.Size(250, 27);
            this.ToolStripTextBoxSearch.ToolTipText = "Фильтр объекта";
            // 
            // ToolStripButtonSearch
            // 
            this.ToolStripButtonSearch.AutoSize = false;
            this.ToolStripButtonSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButtonSearch.Image = global::ArmRegistrator.Properties.Resources.SearchIco;
            this.ToolStripButtonSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonSearch.Name = "ToolStripButtonSearch";
            this.ToolStripButtonSearch.Size = new System.Drawing.Size(28, 28);
            this.ToolStripButtonSearch.Text = "toolStripButton1";
            this.ToolStripButtonSearch.ToolTipText = "Очистить фильтр объектов";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 50);
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 47);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            this.bindingNavigatorMoveFirstItem.Visible = false;
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 47);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            this.bindingNavigatorMovePreviousItem.Visible = false;
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 50);
            this.bindingNavigatorSeparator.Visible = false;
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            this.bindingNavigatorPositionItem.Visible = false;
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 50);
            this.bindingNavigatorSeparator1.Visible = false;
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 47);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            this.bindingNavigatorMoveNextItem.Visible = false;
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 47);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            this.bindingNavigatorMoveLastItem.Visible = false;
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 50);
            this.bindingNavigatorSeparator2.Visible = false;
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
            // 
            // BtnAllField
            // 
            this.BtnAllField.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnAllField.Location = new System.Drawing.Point(6, 21);
            this.BtnAllField.Name = "BtnAllField";
            this.BtnAllField.Size = new System.Drawing.Size(68, 68);
            this.BtnAllField.TabIndex = 5;
            this.BtnAllField.Text = "Все";
            this.BtnAllField.UseVisualStyleBackColor = true;
            this.BtnAllField.Click += new System.EventHandler(this.BtnAllField_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.BtnConnectDbConfig);
            this.groupBox1.Controls.Add(this.BtnConnect);
            this.groupBox1.Location = new System.Drawing.Point(731, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(206, 98);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "База данных";
            // 
            // BtnConnectDbConfig
            // 
            this.BtnConnectDbConfig.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnConnectDbConfig.Image = ((System.Drawing.Image)(resources.GetObject("BtnConnectDbConfig.Image")));
            this.BtnConnectDbConfig.Location = new System.Drawing.Point(102, 21);
            this.BtnConnectDbConfig.Name = "BtnConnectDbConfig";
            this.BtnConnectDbConfig.Size = new System.Drawing.Size(96, 68);
            this.BtnConnectDbConfig.TabIndex = 12;
            this.BtnConnectDbConfig.Text = "Настройка";
            this.BtnConnectDbConfig.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnConnectDbConfig.UseVisualStyleBackColor = true;
            this.BtnConnectDbConfig.Click += new System.EventHandler(this.BtnConnectDbConfig_Click);
            // 
            // BtnConnect
            // 
            this.BtnConnect.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnConnect.Image = global::ArmRegistrator.Properties.Resources.ImageConnectionDeactive;
            this.BtnConnect.Location = new System.Drawing.Point(6, 21);
            this.BtnConnect.Name = "BtnConnect";
            this.BtnConnect.Size = new System.Drawing.Size(96, 68);
            this.BtnConnect.TabIndex = 11;
            this.BtnConnect.Text = "Подключение";
            this.BtnConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnConnect.UseVisualStyleBackColor = true;
            this.BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
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
            this.FlagLongTimeInField.Location = new System.Drawing.Point(210, 21);
            this.FlagLongTimeInField.Name = "FlagLongTimeInField";
            this.FlagLongTimeInField.Size = new System.Drawing.Size(68, 68);
            this.FlagLongTimeInField.TabIndex = 16;
            this.FlagLongTimeInField.Text = "На смене более 12 часов";
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
            // FormReg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 566);
            this.Controls.Add(this.PanelGrids);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.MinimumSize = new System.Drawing.Size(965, 604);
            this.Name = "FormReg";
            this.Text = "АРМ регистрации";
            this.Load += new System.EventHandler(this.FormReg_Load);
            this.PanelGrids.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TrackerView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CardView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelGrids;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
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
        private System.Windows.Forms.Button BtnConnect;
        private System.Windows.Forms.Button BtnConnectDbConfig;
        private System.Windows.Forms.ToolStripTextBox ToolStripTextBoxSearch;
        private System.Windows.Forms.ToolStripButton ToolStripButtonSearch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.CheckBox FlagPersonal;
        private System.Windows.Forms.CheckBox FlagTechnics;
        private System.Windows.Forms.CheckBox FlagTransport;
        private System.Windows.Forms.CheckBox FlagIsInField;
        private System.Windows.Forms.CheckBox FlagIsNotInField;
        private System.Windows.Forms.CheckBox FlagLongTimeInField;

    }
}

