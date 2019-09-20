using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ArmRegistrator.DataBase;
using DataGridViewExtendedControls.Utils;

namespace ArmRegistrator
{
    public partial class FormReplacement : Form
    {
        public FormReplacement(DbWrapper wrapper)
        {
            InitializeComponent();
            _dbWrapper = wrapper;
        }

        private void ConfigDataComponents()
        {
            const string objTableName = "Object";
            var bsObj = new BindingSource(_dbWrapper.Data, objTableName) { Filter = "ServiceId>0" };
            ObjectView.DataSource = bsObj;
            var bsMay = new BindingSource(_dbWrapper.Data, objTableName) { Filter = "ServiceId<0" };
            ObjectMayView.DataSource = bsMay;
            return;
        }

        

        private void FormReplacement_Load(object sender, EventArgs e)
        {
            CreateViewsColumns();
            DataGridViewAddCheckBoxColumns();
            ConfigDataComponents();
            DataGridViewsSetColumnsWidth();
            _defaultStyle = ObjectView.Rows[0].InheritedStyle.Clone();
            SetStyleForActiveRows();
        }
        private void DataGridViewAddCheckBoxColumns()
        {
            var cols = new[] {"InField"};
            StaticMethods.DataGridViewChangeColumnsToCheckBox(ObjectView, cols);
            StaticMethods.DataGridViewChangeColumnsToCheckBox(ObjectMayView, cols);
        }
        private void DataGridViewsSetColumnsWidth()
        {
            StaticMethods.DataGridViewSetColumnWidth(ObjectView);
            StaticMethods.DataGridViewSetColumnWidth(ObjectMayView);
        }
        
        private void CreateViewsColumns()
        {
            var objectCols = FormReplacementHelper.GetDefaultObjectColumnTitles();
            StaticMethods.CreateDataGridViewColumn(ObjectView, objectCols);
            var mayReplCols = FormReplacementHelper.GetDefaultMayReplacementColumnTitles();
            StaticMethods.CreateDataGridViewColumn(ObjectMayView, mayReplCols);
        }
        
        private void SetStyleForActiveRows()
        {
            var activedStyle = ObjectView.Rows[0].InheritedStyle.Clone();
            activedStyle.BackColor = Color.LightGray;

            var checkColumns = FormReplacementHelper.GetCheckBoxColumnNames();
            SetRowsStyle(ObjectView, activedStyle, "ISNULL(_ActiveObjectId,0)<>0", checkColumns);
            SetRowsStyle(ObjectMayView, activedStyle, "ISNULL(_PassiveObjectId,0)<>0", checkColumns);
        }
        private static void SetRowsStyle(DataGridView dgv, DataGridViewCellStyle style, string filtr, IEnumerable<string> checkColumns )
        {
            var bs = (BindingSource)dgv.DataSource;
            var dt = ((DataSet)bs.DataSource).Tables[bs.DataMember];
            var drs = dt.Select(filtr);

            foreach (DataRow row in drs)
            {
                var indx = bs.Find("ObjectId", row["ObjectId"]);
                if (indx > -1) StaticMethods.SetDefaultCellStyleForRow(dgv.Rows[indx], style, checkColumns);
            }
        }

        private void SetDefaultCellStyleForPair(DataGridViewRow dgvRow)
        {
            var bs = (BindingSource)ObjectMayView.DataSource;
            var row = ((DataRowView) dgvRow.DataBoundItem).Row;
            var indx = bs.Find("ObjectId", row["_ActiveObjectId"]);
            var checkColumns = FormReplacementHelper.GetCheckBoxColumnNames();
            if (indx > -1) StaticMethods.SetDefaultCellStyleForRow(ObjectMayView.Rows[indx], _defaultStyle, checkColumns);
            StaticMethods.SetDefaultCellStyleForRow(dgvRow, _defaultStyle, checkColumns);
        }

        private void BtnReplace_Click(object sender, EventArgs e)
        {
            var objRow = StaticMethods.GetCurrentDataRow(ObjectView);
            var replRow = StaticMethods.GetCurrentDataRow(ObjectMayView);
            if (objRow==null || replRow==null) return;
            var objId = Convert.ToInt32(objRow["ObjectId"]);
            var activObjId = Convert.ToInt32(replRow["ObjectId"]);
            _dbWrapper.WriteAddedReplacePair(objId, activObjId);
            _dbWrapper.RefreshObjectTable();
            EnableDisableButton();
            SetStyleForActiveRows();

        }
        private void BtnUnReplace_Click(object sender, EventArgs e)
        {
            var objRow = StaticMethods.GetCurrentDataRow(ObjectView);
            if (objRow == null) return;
            var objId = Convert.ToInt32(objRow["ObjectId"]);
            SetDefaultCellStyleForPair(ObjectView.CurrentRow);
            _dbWrapper.WriteDeletedReplacePair(objId);
            _dbWrapper.RefreshObjectTable();
            EnableDisableButton();
            SetStyleForActiveRows();
        }

        private void ObjectView_SelectionChanged(object sender, EventArgs e)
        {
            EnableDisableButton();
        }

        private void ObjectMayView_SelectionChanged(object sender, EventArgs e)
        {
            EnableDisableButton();
        }
        private void EnableDisableButton()
        {
            var objRow = StaticMethods.GetCurrentDataRow(ObjectView);
            var enableRepl = true;
            if (objRow == null) enableRepl = false;
            
            var objMayRow = StaticMethods.GetCurrentDataRow(ObjectMayView);
            var enableUnRepl = true;
            if (objMayRow == null) enableUnRepl = false;
            
            if (enableRepl)
                if (!Convert.IsDBNull(objRow["_ActiveObjectId"])) enableRepl = false;
            
            if(enableUnRepl)
                if (Convert.IsDBNull(objMayRow["_PassiveObjectId"])) enableUnRepl = false;

            BtnReplace.Enabled = enableRepl && !enableUnRepl;
            BtnUnReplace.Enabled = !enableRepl;//enableUnRepl || !enableRepl;
        }
        
        private readonly DbWrapper _dbWrapper;
        private DataGridViewCellStyle _defaultStyle;
    }
}
