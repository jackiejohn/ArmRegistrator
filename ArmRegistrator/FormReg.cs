using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ArmRegistrator.Automation;
using ArmRegistrator.DataBase;
using ArmRegistrator.Photo;
using ArmRegistrator.Radio;
using DataGridViewExtendedControls.DataGridViewProgress;
using DataGridViewExtendedControls.Utils;
using SharedTypes.Paks;

namespace ArmRegistrator
{
    public partial class FormReg : Form
    {
        public FormReg()
        {
            InitializeComponent();
            _dbWrapper = new DbWrapper(Properties.Settings.Default.RefreshTime, Properties.Settings.Default.ConnectionString);
            _rModuleWrapper=new RModuleWrapper();
            _logic =CreateNewLogicAndSubscribeEvents();
        }

        public Logic CreateNewLogicAndSubscribeEvents()
        {
            var logic = Properties.Settings.Default.RfidTwoReaders
                ? new Logic(Properties.Settings.Default.RfidPortIn, Properties.Settings.Default.RfidPortOut, _dbWrapper, _rModuleWrapper)
                : new Logic(Properties.Settings.Default.RfidPortIn, _dbWrapper, _rModuleWrapper);
            logic.OnObjectToLamp += Logic_OnObjectChangeStateToLamp;
            logic.OnObjectToWork += Logic_OnObjectChangeStateToWork;
            return logic;
        }

        

        #region Form Events
        private void FormReg_Load(object sender, EventArgs e)
        {
            GetDefaultParamValues();
            BtnDbConnectSetImage(false);
            CreateTrackerViewColumns();
            CreateCardViewColumns();
            HaveModemChanged();
            if (!Properties.Settings.Default.RfidUse) BtnSerialConnect.Enabled = false;
            FormPhotoOpen();
        }
        
        private void FormReg_FormClosed(object sender, FormClosedEventArgs e)
        {
            _rModuleWrapper.StopCommunication();
        }
        private void FormReg_ResizeEnd(object sender, EventArgs e)
        {
            Properties.Settings.Default.FormWidth = Width;
            Properties.Settings.Default.FormHeight = Height;
            Properties.Settings.Default.Save();
        }
        private void FormReg_SizeChanged(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.IsMaximized != (WindowState == FormWindowState.Maximized))
            {
                Properties.Settings.Default.IsMaximized = WindowState == FormWindowState.Maximized;
                Properties.Settings.Default.Save();
            }
        }

        private void CardView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var dgv = (DataGridView)sender;
            var row = ((DataRowView)dgv.Rows[e.RowIndex].DataBoundItem).Row;
            if (row["Name"].ToString() == "InFieldTime" && dgv.Columns[e.ColumnIndex].Name == "Value")
            {
                var dgvTrack = TrackerView;
                if (dgvTrack.CurrentRow != null)
                {
                    var inField = (bool)((DataRowView)dgvTrack.CurrentRow.DataBoundItem).Row["InField"];
                    //var longTime = (bool)((DataRowView)dgvTrack.CurrentRow.DataBoundItem).Row["LongTimeInField"];
                    var style = e.CellStyle;
                    Color col = Color.LightGreen;
                    //if (longTime) col = Color.Orange;
                    if (!inField) col = Color.OrangeRed;
                    style.BackColor = col;
                    e.CellStyle.ApplyStyle(style);
                }
            }
        }
        private void TrackerView_SelectionChanged(object sender, EventArgs e)
        {
            TrackerViewRowChanged();
        }

        private void BtnReplace_Click(object sender, EventArgs e)
        {
            using (var frm = new FormReplacement( _dbWrapper))
            {
                frm.ShowDialog(this);
            }
        }
        private void BtnStartReader_Click(object sender, EventArgs e)
        {
            using (var frm = new FormBar())
            {
                frm.ShowDialog(this);
            }
        }
        private void FlagIsNotHaveModem_Click(object sender, EventArgs e)
        {
            bool state = ((CheckBox)sender).Checked;
            if (state)
            {
                if (!FormHelper.OperatorRiskAgreeModem())
                {
                    ((CheckBox)sender).Checked = false;
                    return;
                }
            }
            HaveModemChanged();
            TrackerViewRowChanged();
            SaveHaveModemProp(state);
        }
        private void BtnDbConnect_Click(object sender, EventArgs e)
        {
            if (!_dataConfigured)
            {
                if (!_dbWrapper.PrepareDataSetObjects(Properties.Settings.Default.RefreshTime, Properties.Settings.Default.ConnectionString)) return;
                TrackerViewPrepare();
                _dataConfigured = true;
                BtnDbConnectSetImage(true);
                _dbWrapper.TimerStart();
                BtnReplace.Enabled = true;
                BtnSerialConnect.Enabled = true;
                //if (Properties.Settings.Default.RfidUse) _logic.Start();
            }
            else
            {
                _dataConfigured = false;
                BtnDbConnectSetImage(false);
                _dbWrapper.TimerStop();
                _dbWrapper.ClearDataSet();
                BtnReplace.Enabled = false;
                BtnSerialConnect.Enabled = false;
                LogicStop();
                
            }
            StaticMethods.DataGridViewSetColumnWidth(TrackerView);
            TrackerViewSetFilter();
        }
        private void TrackerViewPrepare()
        {
            var bsP = new BindingSource(_dbWrapper.Data, DbWrapper.OBJECT_TABLE_NAME);
            TrackerView.DataSource = bsP;
        }
        private void BtnDbConfig_Click(object sender, EventArgs e)
        {
            Dirty deleg = SetLogicDirty;
            using (var dialog = new FormSettings(deleg))
            {
                dialog.ShowDialog(this);
            }
            return;
        }
        private void BtnAllObjectType_Click(object sender, EventArgs e)
        {
            FlagPersonal.Checked = true;
            FlagTransport.Checked = true;
            FlagTechnics.Checked = true;
            TrackerViewSetFilter();
        }
        private void BtnAllField_Click(object sender, EventArgs e)
        {
            FlagIsInField.Checked = true;
            FlagIsNotInField.Checked = true;
            FlagLongTimeInField.Checked = false;
            InputHoursVisibility();
            TrackerViewSetFilter();
        }
        private void BtnRModuleConnect_Click(object sender, EventArgs e)
        {
            RModuleWrapperChangeCommunicationState();
            VisualizeRModuleState();
        }
        private void BtnSerialConnect_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.RfidUse) 
            {
                if (_logic.IsStarted)
                {
                    LogicStop();
                }
                else
                {
                    if (_dirtyLogic)
                    {
                        //_logic.Stop();
                        _logic.OnObjectToLamp -= Logic_OnObjectChangeStateToLamp;
                        _logic.OnObjectToWork -= Logic_OnObjectChangeStateToWork;
                        _logic.Dispose();
                        
                        _logic = CreateNewLogicAndSubscribeEvents();
                        _dirtyLogic = false;
                    }
                    LogicStart();
                }
            }
        }

        private void BtnInField_Click(object sender, EventArgs e)
        {
            InFieldChangeState();
        }
        private void BtnNotInField_Click(object sender, EventArgs e)
        {
            InFieldChangeState();
        }

        private void FlagPersonal_Click(object sender, EventArgs e)
        {
            TrackerViewSetFilter();
        }
        private void FlagTransport_CheckedChanged(object sender, EventArgs e)
        {
            TrackerViewSetFilter();
        }
        private void FlagTechnics_CheckedChanged(object sender, EventArgs e)
        {
            TrackerViewSetFilter();
        }
        private void FlagIsInField_Click(object sender, EventArgs e)
        {
            FlagLongTimeInField.Checked = false;
            InputHoursVisibility();
            TrackerViewSetFilter();
        }
        private void FlagIsNotInField_Click(object sender, EventArgs e)
        {
            FlagLongTimeInField.Checked = false;
            InputHoursVisibility();
            TrackerViewSetFilter();
        }
        private void FlagLongTimeInField_Click(object sender, EventArgs e)
        {
            FlagIsInField.Checked = false;
            FlagIsNotInField.Checked = false;
            InputHoursVisibility();
            TrackerViewSetFilter();
        }

        private void ToolStripTextBoxHours_Validated(object sender, EventArgs e)
        {
            Properties.Settings.Default.LongTime = ((ToolStripTextBox)sender).Text;
            Properties.Settings.Default.Save();
            TrackerViewSetFilter();
        }
        private void ToolStripTextBoxHours_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string strVal = ((ToolStripTextBox)sender).Text;
            int val;
            if (int.TryParse(strVal, out val))
            {
                if (val >= 1 && val <= 99) return;
            }
            e.Cancel = true;
            MessageBox.Show("Вводимое значение должно быть целым числом от 1 до 99", "Неверные данные",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void ToolStripTextBoxHours_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                Validate();
                e.Handled = true;
                return;
            }
        }
        private void ToolStripTextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if ( _dbWrapper.Data== null) return;
            Image img = Properties.Resources.SearchIco;
            if (ToolStripTextBoxSearch.Text.Length > 0) img = Properties.Resources.DelCross;
            ToolStripButtonSearch.Image = img;

            SetFullFilter(ToolStripTextBoxSearch.Text);
        }
        private void ToolStripButtonSearch_Click(object sender, EventArgs e)
        {
            ToolStripTextBoxSearch.Clear();
        }
        
        private void RModuleWrapper_OnPortError(object sender, EventArgs e)
        {
            _rModuleWrapper.Close();
            BtnRModuleConnectSetImage(false);
            ButtonsFieldDisable();
        }

        private void Logic_OnObjectChangeStateToLamp(object sender, ObjectChangeStateEventArgs e)
        {
            TrackerViewRowChanged();
            var info = _dbWrapper.ReadEmployeeData(e.ObjectId);
            _formPhoto.UpdatePersonalData(info, true,true);
        }
        private void Logic_OnObjectChangeStateToWork(object sender, ObjectChangeStateEventArgs e)
        {
            TrackerViewRowChanged();
            var info = _dbWrapper.ReadEmployeeData(e.ObjectId);
            _formPhoto.UpdatePersonalData(info, false,true);
        }

        

        #endregion

        #region Config and initializing

        private void GetDefaultParamValues()
        {
            ToolStripTextBoxHours.Text = Properties.Settings.Default.LongTime;
            WindowState = Properties.Settings.Default.IsMaximized ? FormWindowState.Maximized : FormWindowState.Normal;
            Width = Properties.Settings.Default.FormWidth;
            Height = Properties.Settings.Default.FormHeight;
            FlagIsNotHaveModem.Checked = Properties.Settings.Default.HaveModemState;
        }

        private void CreateCardViewColumns()
        {
            var dgv = CardView;
            var columns = FormHelper.GetDefaultCardColumnTitles();
            StaticMethods.CreateDataGridViewColumn(dgv, columns);
            var colValue = dgv.Columns["Value"];
            if (colValue == null) throw new NullReferenceException("Столбец Value не найден");
            colValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        private void CreateTrackerViewColumns()
        {
            var dgv = TrackerView;
            var columns = FormHelper.GetVisibleTrackerColumnNames();
            StaticMethods.CreateDataGridViewColumn(dgv, columns);
            TrackerViewAddProgressColumns();
            TrackerViewAddCheckBoxColumns();
        }

        private static void SaveHaveModemProp(bool state)
        {
            Properties.Settings.Default.HaveModemState = state;
            Properties.Settings.Default.Save();
        }

        #endregion

        #region Form object methods

        private void TrackerViewAddCheckBoxColumns()
        {
            var colsName = new[] { "InField", "Error" };
            StaticMethods.DataGridViewChangeColumnsToCheckBox(TrackerView, colsName);
        }
        private void TrackerViewAddProgressColumns()
        {
            DataGridView dgv = TrackerView;
            if (dgv.Columns == null) return;

            const string colName = "Charge";
            var col = dgv.Columns[colName];
            if (col == null) return;
            int indxCharge = col.Index;

            DataGridViewProgressCell cell = FormHelper.GetDefaultProgressCell();
            cell.BarStyle = ProgressCellProgressStyle.Visible;
            var progressColumn = new DataGridViewProgressColumn(cell)
            {
                Name = colName,
                SortMode = DataGridViewColumnSortMode.Automatic,
                DataPropertyName = colName,
                CellTemplate = cell,
                HeaderText = col.HeaderText,
            };
            dgv.Columns.Remove(colName);
            dgv.Columns.Insert(indxCharge - 1, progressColumn);

        }
        private void TrackerViewSetFilter()
        {
            DataGridView dgv = TrackerView;
            var bs = (BindingSource)dgv.DataSource;
            if (bs == null) return;
            var sbType = new StringBuilder(" or 1<>1");
            if (FlagPersonal.Checked) sbType.Append(" or ObjectTypeId=1");
            if (FlagTransport.Checked) sbType.Append(" or ObjectTypeId=2");
            if (FlagTechnics.Checked) sbType.Append(" or ObjectTypeId=3");
            if (sbType.Length > 0)
            {
                sbType.Replace(" or ", "", 0, 5);
                sbType.Insert(0, "(");
                sbType.Append(")");
            }

            var sbField = new StringBuilder(" or 1<>1");
            if (FlagIsInField.Checked) sbField.Append(" or InField=1");
            if (FlagIsNotInField.Checked) sbField.Append(" or InField=0");
            if (FlagLongTimeInField.Checked) sbField.AppendFormat(" or LongTime>={0}", ToolStripTextBoxHours.Text);
            if (sbField.Length > 0)
            {
                sbField.Replace(" or ", "", 0, 5);
                // sbField.Insert(0, "(");
                //sbField.Append(")");
            }
            sbType.AppendFormat("and({0}) and (ServiceId>0)", sbField);
            _filter = sbType.ToString();
            SetFullFilter(ToolStripTextBoxSearch.Text);
            //bs.Filter = sbType.ToString();
        }
        private void TrackerViewRowChanged()
        {
            var dgv = TrackerView;
            var dgvCard = CardView;
            if (dgv.CurrentRow == null)
            {
                ButtonsFieldDisable();
                dgvCard.DataSource = null;
                return;
            }
            if (dgv.CurrentRow.Index < 0) return;

            var row = ((DataRowView)dgv.CurrentRow.DataBoundItem).Row;
            if (_rModuleWrapper.IsConnected || _isNoModem) ButtonsFieldEnabled((bool)row["InField"]);

            var objectType = (int)row["ObjectTypeId"];

            var table = _dbWrapper.DataTableForObjectType(objectType);
            if(table==null) return;

            if (dgvCard.DataSource == null) dgvCard.DataSource = table;
            else if (!dgvCard.DataSource.Equals(table)) dgvCard.DataSource = table;
            DataSetHelper.TransposedTableRefresh(row, table);
        }

        private void BtnDbConnectSetImage(bool isConnected)
        {
            var newImg = isConnected ? Properties.Resources.ImageConnectionActive : Properties.Resources.ImageConnectionDeactive;
            FormHelper.InvokeButtonSetImage(BtnDbConnect, newImg);
        }
        private void BtnRModuleConnectSetImage(bool isConnected)
        {
            var newImg = isConnected ? Properties.Resources.RModuleConnected : Properties.Resources.RModuleNotConnected;
            FormHelper.InvokeButtonSetImage(BtnRModuleConnect, newImg);
        }
        private void BtnSerialConnectSetImage(bool isConnected)
        {
            var newImg = isConnected ? Properties.Resources.SerialConnected : Properties.Resources.SerialNoConnected;
            FormHelper.InvokeButtonSetImage(BtnSerialConnect, newImg);
        }

        private void FlagIsNotHaveModemSetImage(bool haveModem)
        {
            var newImg = haveModem ? Properties.Resources.ModemHave : Properties.Resources.ModemNotHave;
            if (FlagIsNotHaveModem.InvokeRequired)
            {
                FlagIsNotHaveModem.BeginInvoke(new Action<Bitmap>(img => { FlagIsNotHaveModem.Image = img; }), newImg);
            }
            else
            {
                FlagIsNotHaveModem.Image = newImg;
            }

        }

        private void ButtonsFieldEnabled(bool isEnable)
        {
            BtnInField.Enabled = !isEnable;
            BtnNotInField.Enabled = isEnable;
        }
        private void ButtonsFieldDisable()
        {
            BtnInField.Enabled = false;
            BtnNotInField.Enabled = false;
        }

        private void InputHoursVisibility()
        {
            bool isVisible = FlagLongTimeInField.Checked;
            ToolStripTextBoxHours.Visible = isVisible;
            ToolStripLabelHoursPre.Visible = isVisible;
            ToolStripLabelHoursPost.Visible = isVisible;
        }

        private void SetFullFilter(string searchFilter)
        {
            var bs = (BindingSource)TrackerView.DataSource;
            if (bs == null) return;
            searchFilter = searchFilter.Replace("'", "");
            bs.Filter = String.Format("({0} like '%{1}%' or {2} like '%{1}%') and ({3}) ", "_Object", searchFilter, "Code", _filter);
        }

        private void VisualizeRModuleState()
        {
            var wrapperState = _rModuleWrapper.IsConnected;
            BtnRModuleConnectSetImage(wrapperState);
            if (wrapperState)
            {
                TrackerViewRowChanged();
            }
            else
            {
                ButtonsFieldDisable();
            }
        }

        //private void VisualizeRfidState()
        //{
        //    var wrapperState = _logic.IsStarted;
        //    BtnSerialConnectSetImage(wrapperState);
        //}

        private void SetLogicDirty()
        {
            _dirtyLogic = true;
            return;
        }

        private void LogicStop()
        {
            _logic.Stop();
            BtnSerialConnectSetImage(_logic.IsStarted);
        }
        private void LogicStart()
        {
            if(!_logic.Start())
            {
                MessageBox.Show(this, "Не удается открыть один из последовательных портов", "Ошибка связи",
                                MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            TtBtnSerial.SetToolTip(BtnSerialConnect,
                                   _logic.Error != LogicErrorEnum.None
                                       ? String.Format("Ошибка: {0}", _logic.Error)
                                       : null);
            BtnSerialConnectSetImage(_logic.IsStarted);
        }

        private void FormPhotoOpen()
        {
            //LinkedList<DeskScreen> screens = ScreenInformation.GetAllScreens();
            var screens = Screen.AllScreens;
            if (screens.Length > 1)
            {
                FormPhotoOpenMaximal(screens);
            }
            else
            {
                FormPhotoOpenMinimal();
            }
        }
        private void FormPhotoOpenMaximal(IEnumerable<Screen> screens)
        {
            var thisScreen = Screen.FromControl(this);
            foreach (var screen in screens)
            {
                if (thisScreen.Bounds.IntersectsWith(screen.Bounds)) continue;
                
                FormPhotoClose();
                _formPhoto = new FormPhoto
                                 {
                                     Top = screen.Bounds.Top, 
                                     Left = screen.Bounds.Left
                                 };
                
                //FormPhotoHelper.ScaleControls(_formPhoto.Controls,_formPhoto.Size);
                _formPhoto.Show();
                _formPhoto.Maximized();
                break;
            }
        }
        private void FormPhotoOpenMinimal()
        {
            FormPhotoClose();
            _formPhoto= new FormPhoto();
            _formPhoto.Show();
        }
        private void FormPhotoClose()
        {
            if (_formPhoto != null)
            {
                _formPhoto.Close();
                _formPhoto = null;
            }
        }

        #endregion

        #region RmoduleWrapper operate methods

        private bool RModuleWrapperConnect()
        {
            string portName = Properties.Settings.Default.RModemPort;
            string baudRate = Properties.Settings.Default.RModemBaudRate;

            bool tryConnect = true;
            while (tryConnect)
            {
                if (string.IsNullOrEmpty(portName))
                {
                    using (var frm = new FormSerialSettings())
                    {
                        if (frm.ShowDialog(this) != DialogResult.OK) return false;
                    }
                    portName = Properties.Settings.Default.RModemPort;
                    if (string.IsNullOrEmpty(portName)) return false;
                }
                tryConnect = !_rModuleWrapper.TryConnect(portName, baudRate);
                if (tryConnect)
                {
                    MessageBox.Show("Не удается инициализировать радиомодем на порту " + portName,
                                    "Ошибка инициализации радиомодуля (" + _rModuleWrapper.InitError + ")", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    portName = string.Empty;
                }
            }
            _rModuleWrapper.OnPortError += RModuleWrapper_OnPortError;
//            _rModuleWrapper.OnDataReceived += RModule_OnDataReceived;
            return true;
        }
        private void RModuleWrapperChangeCommunicationState()
        {
            if (!_rModuleWrapper.IsConnected)
            {
                if (!RModuleWrapperConnect()) return;
                _rModuleWrapper.StartCommunication();
            }
            else
            {
                _rModuleWrapper.StopCommunication();
            }
        }


        #endregion
        
        #region Model action methods
        
        private void HaveModemChanged()
        {
            var state = FlagIsNotHaveModem.Checked;

            BtnRModuleConnect.Enabled = !state;
            _isNoModem = state;
            FlagIsNotHaveModemSetImage(!state);
            if (state) { BtnRModuleConnectSetImage(true); }
            else
            {
                _rModuleWrapper.StopCommunication();
                VisualizeRModuleState();
            }
        }

        private void InFieldChangeState()
        {
            if (TrackerView.SelectedRows.Count > 1)
            {
                if (MessageBox.Show("Выделено несколько строк! Изменения коснутся только одиного"
                    + Environment.NewLine + "Продолжаем?", "Обратите внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }
            var row = StaticMethods.GetCurrentDataRow(TrackerView);
            if (row == null) return;

            int objectId = Convert.ToInt32(row["ObjectId"]);
            
            int activeObjectId = objectId;
            if (!Convert.IsDBNull(row["_ActiveObjectId"])) activeObjectId = Convert.ToInt32(row["_ActiveObjectId"]);
            
            bool inField = Convert.ToBoolean(row["InField"]);
            bool lampMode = true;
            bool workMode = true;
            bool sended = true;
            bool addEvent = true;
            if (!_isNoModem)
            {
                UInt16 status;
                Cursor = Cursors.WaitCursor;
                sended = _rModuleWrapper.ObjectGetStatus(out status, activeObjectId);
                Cursor = Cursors.Default;
                var statusWord = new PakStatusWord(status);
                lampMode = statusWord.LampMode;
                workMode = sended && !(statusWord.Error || statusWord.Charge < 9);

                if (!inField)
                {
                    var sb = new StringBuilder("Изменение режима НЕ ДОПУСТИМО по следующим причинам:");
                    sb.AppendLine();
                    if (!sended) sb.AppendLine(" - нет связи с трекером");
                    else
                    {
                        if (statusWord.Error) sb.AppendLine(" - ошибка оборудования");
                        if (statusWord.Charge < 9) sb.AppendLine(" - низкий заряд батареи");
                    }
                    MessageBox.Show(sb.ToString(), "Изменение режима", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                addEvent = false;
                if (!sended)
                {
                    if (!FormHelper.OperatorRiskAgree()) return;
                    addEvent = true;
                }
            }

            Cursor = Cursors.WaitCursor;

            bool isChanged;
            var stateEventArg = new ObjectChangeStateEventArgs(objectId);
            if(inField )
            {
                isChanged=InFieldUnSet(!(_isNoModem || lampMode || !sended), addEvent, objectId, activeObjectId);
                _logic.InvokeOnObjectToLamp(stateEventArg);
            }
            else
            {
                isChanged=InFieldSet(!(_isNoModem || workMode || !sended), addEvent, objectId, activeObjectId);
                _logic.InvokeOnObjectToWork(stateEventArg);
            }
                
            Cursor = Cursors.Default;

            if (isChanged)
            {
                _dbWrapper.RefreshDataSetAllTables();
                TrackerViewRowChanged();
                
            }
        }
        private bool InFieldUnSet(bool setLampMode, bool addEvent, int objectId, int trakObjId)
        {
            bool isChanged = true;
            UInt16 status;
            if (setLampMode) isChanged = _rModuleWrapper.ObjectSendPassiveMode(out status, trakObjId);
            if(isChanged) _dbWrapper.WriteObjectInFieldState(addEvent, objectId, true);
            return isChanged;
        }
        private bool InFieldSet(bool setWorkMode, bool addEvent, int objectId, int trakObjId)
        {
            bool isChanged = true;
            if (setWorkMode)
            {
                UInt16 status;
                isChanged = _rModuleWrapper.ObjectSendWorkMode(out status, trakObjId);
                isChanged = isChanged && !PakStatusWord.Instance(status).LampMode;
            }

            if (isChanged) _dbWrapper.WriteObjectInFieldState(addEvent, objectId, false);
            return isChanged;
        }

        #endregion


        #region Private Fields
        private bool _dataConfigured;
        private string _filter = string.Empty;
        private bool _isNoModem;

        private readonly DbWrapper _dbWrapper;
        private readonly RModuleWrapper _rModuleWrapper;

        private Logic _logic;
        private bool _dirtyLogic;

        private delegate void Dirty();

        private FormPhoto _formPhoto;
        #endregion
        
    }
}
