
namespace QLBV.FormThamSo
{
    partial class Frm_ChonKQXN
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
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule2 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue2 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            this.colTSTu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKetQua = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTSDen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnCreate = new DevExpress.XtraEditors.SimpleButton();
            this.grcCLS = new DevExpress.XtraGrid.GridControl();
            this.grvCLS = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenRG = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenXN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTSBT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTrangThai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lupStatus = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colNgayThang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayTH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDonVi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcCLS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCLS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // colTSTu
            // 
            this.colTSTu.FieldName = "TSTu";
            this.colTSTu.Name = "colTSTu";
            // 
            // colKetQua
            // 
            this.colKetQua.AppearanceCell.Options.UseTextOptions = true;
            this.colKetQua.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colKetQua.AppearanceHeader.Options.UseTextOptions = true;
            this.colKetQua.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colKetQua.Caption = "Kết quả";
            this.colKetQua.FieldName = "KetQua";
            this.colKetQua.Name = "colKetQua";
            this.colKetQua.OptionsColumn.AllowEdit = false;
            this.colKetQua.Visible = true;
            this.colKetQua.VisibleIndex = 3;
            this.colKetQua.Width = 114;
            // 
            // colTSDen
            // 
            this.colTSDen.FieldName = "TSDen";
            this.colTSDen.Name = "colTSDen";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.layoutControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
            this.splitContainerControl1.Size = new System.Drawing.Size(689, 509);
            this.splitContainerControl1.SplitterPosition = 481;
            this.splitContainerControl1.TabIndex = 0;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnCreate);
            this.layoutControl1.Controls.Add(this.grcCLS);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(689, 509);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(12, 475);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(665, 22);
            this.btnCreate.StyleController = this.layoutControl1;
            this.btnCreate.TabIndex = 5;
            this.btnCreate.Text = "OK";
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // grcCLS
            // 
            this.grcCLS.Location = new System.Drawing.Point(12, 12);
            this.grcCLS.MainView = this.grvCLS;
            this.grcCLS.Name = "grcCLS";
            this.grcCLS.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lupStatus});
            this.grcCLS.Size = new System.Drawing.Size(665, 459);
            this.grcCLS.TabIndex = 4;
            this.grcCLS.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvCLS});
            // 
            // grvCLS
            // 
            this.grvCLS.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grvCLS.Appearance.SelectedRow.BorderColor = System.Drawing.Color.Gray;
            this.grvCLS.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.grvCLS.Appearance.SelectedRow.Options.UseBackColor = true;
            this.grvCLS.Appearance.SelectedRow.Options.UseBorderColor = true;
            this.grvCLS.Appearance.SelectedRow.Options.UseForeColor = true;
            this.grvCLS.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colTenRG,
            this.colTenXN,
            this.colTSBT,
            this.colKetQua,
            this.colTrangThai,
            this.colNgayThang,
            this.colNgayTH,
            this.colTSTu,
            this.colTSDen,
            this.colDonVi});
            gridFormatRule1.Column = this.colTSTu;
            gridFormatRule1.ColumnApplyTo = this.colKetQua;
            gridFormatRule1.Name = "Format0";
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Expression;
            formatConditionRuleValue1.Expression = "[KetQua] < [TSTu]";
            formatConditionRuleValue1.PredefinedName = "Red Bold Text";
            gridFormatRule1.Rule = formatConditionRuleValue1;
            gridFormatRule2.Column = this.colTSDen;
            gridFormatRule2.ColumnApplyTo = this.colKetQua;
            gridFormatRule2.Name = "Format1";
            formatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.Expression;
            formatConditionRuleValue2.Expression = "[KetQua] > [TSDen]";
            formatConditionRuleValue2.PredefinedName = "Red Bold Text";
            gridFormatRule2.Rule = formatConditionRuleValue2;
            this.grvCLS.FormatRules.Add(gridFormatRule1);
            this.grvCLS.FormatRules.Add(gridFormatRule2);
            this.grvCLS.GridControl = this.grcCLS;
            this.grvCLS.GroupCount = 2;
            this.grvCLS.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.None, "", null, "")});
            this.grvCLS.Name = "grvCLS";
            this.grvCLS.OptionsBehavior.AutoExpandAllGroups = true;
            this.grvCLS.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            this.grvCLS.OptionsSelection.MultiSelect = true;
            this.grvCLS.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.grvCLS.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            this.grvCLS.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            this.grvCLS.OptionsView.GroupDrawMode = DevExpress.XtraGrid.Views.Grid.GroupDrawMode.Standard;
            this.grvCLS.OptionsView.ShowGroupPanel = false;
            this.grvCLS.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colNgayTH, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colTenRG, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colID
            // 
            this.colID.Caption = "IDclsct";
            this.colID.FieldName = "Id";
            this.colID.Name = "colID";
            this.colID.OptionsColumn.AllowEdit = false;
            // 
            // colTenRG
            // 
            this.colTenRG.Caption = "Tên RG";
            this.colTenRG.FieldName = "TenRG";
            this.colTenRG.GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.Value;
            this.colTenRG.Name = "colTenRG";
            this.colTenRG.Visible = true;
            this.colTenRG.VisibleIndex = 1;
            // 
            // colTenXN
            // 
            this.colTenXN.AppearanceCell.Options.UseTextOptions = true;
            this.colTenXN.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colTenXN.AppearanceHeader.Options.UseTextOptions = true;
            this.colTenXN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTenXN.Caption = "Tên xét nghiệm";
            this.colTenXN.FieldName = "TenXN";
            this.colTenXN.Name = "colTenXN";
            this.colTenXN.OptionsColumn.AllowEdit = false;
            this.colTenXN.Visible = true;
            this.colTenXN.VisibleIndex = 1;
            this.colTenXN.Width = 165;
            // 
            // colTSBT
            // 
            this.colTSBT.AppearanceCell.Options.UseTextOptions = true;
            this.colTSBT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTSBT.AppearanceHeader.Options.UseTextOptions = true;
            this.colTSBT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTSBT.Caption = "Trị số bình thường";
            this.colTSBT.FieldName = "TSBT";
            this.colTSBT.Name = "colTSBT";
            this.colTSBT.OptionsColumn.AllowEdit = false;
            this.colTSBT.Visible = true;
            this.colTSBT.VisibleIndex = 2;
            this.colTSBT.Width = 99;
            // 
            // colTrangThai
            // 
            this.colTrangThai.AppearanceCell.Options.UseTextOptions = true;
            this.colTrangThai.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTrangThai.AppearanceHeader.Options.UseTextOptions = true;
            this.colTrangThai.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTrangThai.Caption = "Trạng thái";
            this.colTrangThai.ColumnEdit = this.lupStatus;
            this.colTrangThai.FieldName = "Status";
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.OptionsColumn.AllowEdit = false;
            this.colTrangThai.Visible = true;
            this.colTrangThai.VisibleIndex = 4;
            this.colTrangThai.Width = 96;
            // 
            // lupStatus
            // 
            this.lupStatus.AutoHeight = false;
            this.lupStatus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupStatus.DisplayMember = "Ten";
            this.lupStatus.Name = "lupStatus";
            this.lupStatus.ValueMember = "Status";
            // 
            // colNgayThang
            // 
            this.colNgayThang.AppearanceCell.Options.UseTextOptions = true;
            this.colNgayThang.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNgayThang.AppearanceHeader.Options.UseTextOptions = true;
            this.colNgayThang.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNgayThang.Caption = "Ngày chỉ định";
            this.colNgayThang.FieldName = "NgayThang";
            this.colNgayThang.GroupFormat.FormatString = "dd-MM-yyyy HH:mm";
            this.colNgayThang.GroupFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colNgayThang.GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.Value;
            this.colNgayThang.Name = "colNgayThang";
            this.colNgayThang.Width = 137;
            // 
            // colNgayTH
            // 
            this.colNgayTH.Caption = "Ngày thực hiện";
            this.colNgayTH.FieldName = "NgayTH";
            this.colNgayTH.GroupFormat.FormatString = "dd-MM-yyyy HH:mm";
            this.colNgayTH.GroupFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colNgayTH.Name = "colNgayTH";
            this.colNgayTH.Visible = true;
            this.colNgayTH.VisibleIndex = 6;
            // 
            // colDonVi
            // 
            this.colDonVi.Caption = "Đơn vị";
            this.colDonVi.FieldName = "DonVi";
            this.colDonVi.Name = "colDonVi";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(689, 509);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grcCLS;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(669, 463);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnCreate;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 463);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(669, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // Frm_ChonKQXN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 509);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "Frm_ChonKQXN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_ChonKQXN";
            this.Load += new System.EventHandler(this.Frm_ChonKQXN_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcCLS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCLS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl grcCLS;
        private DevExpress.XtraGrid.Views.Grid.GridView grvCLS;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colTenRG;
        private DevExpress.XtraGrid.Columns.GridColumn colTenXN;
        private DevExpress.XtraGrid.Columns.GridColumn colTSBT;
        private DevExpress.XtraGrid.Columns.GridColumn colTrangThai;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lupStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colKetQua;
        private DevExpress.XtraEditors.SimpleButton btnCreate;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayThang;
        private DevExpress.XtraGrid.Columns.GridColumn colTSDen;
        private DevExpress.XtraGrid.Columns.GridColumn colTSTu;
        private DevExpress.XtraGrid.Columns.GridColumn colDonVi;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayTH;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}