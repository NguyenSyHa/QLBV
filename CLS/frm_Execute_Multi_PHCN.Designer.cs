namespace QLBV.CLS
{
    partial class frm_Execute_Multi_PHCN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Execute_Multi_PHCN));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.dtChonNgayChiDinh = new DevExpress.XtraEditors.DateEdit();
            this.btnExecute = new DevExpress.XtraEditors.SimpleButton();
            this.cboCBExecute = new DevExpress.XtraEditors.LookUpEdit();
            this.dtExecuteTime = new DevExpress.XtraEditors.DateEdit();
            this.gridControlBenhNhan = new DevExpress.XtraGrid.GridControl();
            this.gridViewBenhNhan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControlDichVu = new DevExpress.XtraGrid.GridControl();
            this.gridViewDichVu = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtChonNgayChiDinh.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtChonNgayChiDinh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCBExecute.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtExecuteTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtExecuteTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlBenhNhan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBenhNhan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDichVu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDichVu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.dtChonNgayChiDinh);
            this.layoutControl1.Controls.Add(this.btnExecute);
            this.layoutControl1.Controls.Add(this.cboCBExecute);
            this.layoutControl1.Controls.Add(this.dtExecuteTime);
            this.layoutControl1.Controls.Add(this.gridControlBenhNhan);
            this.layoutControl1.Controls.Add(this.gridControlDichVu);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(950, 369);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // dtChonNgayChiDinh
            // 
            this.dtChonNgayChiDinh.EditValue = null;
            this.dtChonNgayChiDinh.Location = new System.Drawing.Point(117, 2);
            this.dtChonNgayChiDinh.Name = "dtChonNgayChiDinh";
            this.dtChonNgayChiDinh.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dtChonNgayChiDinh.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtChonNgayChiDinh.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtChonNgayChiDinh.Size = new System.Drawing.Size(170, 20);
            this.dtChonNgayChiDinh.StyleController = this.layoutControl1;
            this.dtChonNgayChiDinh.TabIndex = 10;
            this.dtChonNgayChiDinh.EditValueChanged += new System.EventHandler(this.dtChonNgayChiDinh_EditValueChanged);
            // 
            // btnExecute
            // 
            this.btnExecute.Image = ((System.Drawing.Image)(resources.GetObject("btnExecute.Image")));
            this.btnExecute.Location = new System.Drawing.Point(793, 345);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(155, 22);
            this.btnExecute.StyleController = this.layoutControl1;
            this.btnExecute.TabIndex = 9;
            this.btnExecute.Text = "Thực hiện";
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // cboCBExecute
            // 
            this.cboCBExecute.Location = new System.Drawing.Point(406, 345);
            this.cboCBExecute.Name = "cboCBExecute";
            this.cboCBExecute.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCBExecute.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenCB", "Tên CB")});
            this.cboCBExecute.Properties.DisplayMember = "TenCB";
            this.cboCBExecute.Properties.NullText = "";
            this.cboCBExecute.Properties.ShowHeader = false;
            this.cboCBExecute.Properties.ValueMember = "MaCB";
            this.cboCBExecute.Size = new System.Drawing.Size(383, 20);
            this.cboCBExecute.StyleController = this.layoutControl1;
            this.cboCBExecute.TabIndex = 8;
            // 
            // dtExecuteTime
            // 
            this.dtExecuteTime.EditValue = null;
            this.dtExecuteTime.Location = new System.Drawing.Point(117, 345);
            this.dtExecuteTime.Name = "dtExecuteTime";
            this.dtExecuteTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtExecuteTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtExecuteTime.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
            this.dtExecuteTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtExecuteTime.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
            this.dtExecuteTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtExecuteTime.Properties.Mask.EditMask = "dd/MM/yyyy HH:mm:ss";
            this.dtExecuteTime.Size = new System.Drawing.Size(170, 20);
            this.dtExecuteTime.StyleController = this.layoutControl1;
            this.dtExecuteTime.TabIndex = 7;
            // 
            // gridControlBenhNhan
            // 
            this.gridControlBenhNhan.Location = new System.Drawing.Point(291, 26);
            this.gridControlBenhNhan.MainView = this.gridViewBenhNhan;
            this.gridControlBenhNhan.Name = "gridControlBenhNhan";
            this.gridControlBenhNhan.Size = new System.Drawing.Size(657, 315);
            this.gridControlBenhNhan.TabIndex = 5;
            this.gridControlBenhNhan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewBenhNhan});
            // 
            // gridViewBenhNhan
            // 
            this.gridViewBenhNhan.Appearance.SelectedRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridViewBenhNhan.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gridViewBenhNhan.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn8,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6});
            this.gridViewBenhNhan.GridControl = this.gridControlBenhNhan;
            this.gridViewBenhNhan.Name = "gridViewBenhNhan";
            this.gridViewBenhNhan.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
            this.gridViewBenhNhan.OptionsSelection.MultiSelect = true;
            this.gridViewBenhNhan.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridViewBenhNhan.OptionsView.ShowGroupPanel = false;
            this.gridViewBenhNhan.OptionsView.ShowIndicator = false;
            this.gridViewBenhNhan.OptionsView.ShowViewCaption = true;
            this.gridViewBenhNhan.ViewCaption = "Chọn bệnh nhân";
            this.gridViewBenhNhan.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.gridViewBenhNhan_CustomUnboundColumnData);
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "STT";
            this.gridColumn8.FieldName = "STT";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 1;
            this.gridColumn8.Width = 31;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Mã bệnh nhân";
            this.gridColumn1.FieldName = "MaBNhan";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            this.gridColumn1.Width = 76;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Tên bệnh nhân";
            this.gridColumn2.FieldName = "TenBNhan";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 3;
            this.gridColumn2.Width = 149;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Tuổi";
            this.gridColumn3.FieldName = "Tuoi";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 4;
            this.gridColumn3.Width = 33;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Giới tính";
            this.gridColumn4.FieldName = "GioiTinh";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 5;
            this.gridColumn4.Width = 47;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Đối tượng";
            this.gridColumn5.FieldName = "DTuong";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 6;
            this.gridColumn5.Width = 64;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Địa chỉ";
            this.gridColumn6.FieldName = "DChi";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 7;
            this.gridColumn6.Width = 225;
            // 
            // gridControlDichVu
            // 
            this.gridControlDichVu.Location = new System.Drawing.Point(2, 26);
            this.gridControlDichVu.MainView = this.gridViewDichVu;
            this.gridControlDichVu.Name = "gridControlDichVu";
            this.gridControlDichVu.Size = new System.Drawing.Size(285, 315);
            this.gridControlDichVu.TabIndex = 4;
            this.gridControlDichVu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDichVu});
            // 
            // gridViewDichVu
            // 
            this.gridViewDichVu.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Red;
            this.gridViewDichVu.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gridViewDichVu.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Red;
            this.gridViewDichVu.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gridViewDichVu.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Red;
            this.gridViewDichVu.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gridViewDichVu.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7});
            this.gridViewDichVu.GridControl = this.gridControlDichVu;
            this.gridViewDichVu.Name = "gridViewDichVu";
            this.gridViewDichVu.OptionsView.ShowColumnHeaders = false;
            this.gridViewDichVu.OptionsView.ShowGroupPanel = false;
            this.gridViewDichVu.OptionsView.ShowViewCaption = true;
            this.gridViewDichVu.ViewCaption = "Danh sách dịch vụ";
            this.gridViewDichVu.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewDichVu_FocusedRowChanged);
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Tên dịch vụ";
            this.gridColumn7.FieldName = "TenDV";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem3,
            this.layoutControlItem6,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(950, 369);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControlDichVu;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(289, 319);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridControlBenhNhan;
            this.layoutControlItem2.Location = new System.Drawing.Point(289, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(661, 319);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.AppearanceItemCaption.ForeColor = System.Drawing.Color.Maroon;
            this.layoutControlItem4.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem4.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem4.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItem4.Control = this.dtExecuteTime;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 343);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(289, 26);
            this.layoutControlItem4.Text = "Thời gian thực hiện:";
            this.layoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(110, 20);
            this.layoutControlItem4.TextToControlDistance = 5;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.AppearanceItemCaption.ForeColor = System.Drawing.Color.Maroon;
            this.layoutControlItem5.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem5.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem5.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItem5.Control = this.cboCBExecute;
            this.layoutControlItem5.Location = new System.Drawing.Point(289, 343);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(502, 26);
            this.layoutControlItem5.Text = "Bác sỹ thực hiện:";
            this.layoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(110, 20);
            this.layoutControlItem5.TextToControlDistance = 5;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnExecute;
            this.layoutControlItem3.Location = new System.Drawing.Point(791, 343);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(159, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem6.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItem6.Control = this.dtChonNgayChiDinh;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(289, 24);
            this.layoutControlItem6.Text = "Chọn ngày chỉ định:";
            this.layoutControlItem6.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(110, 13);
            this.layoutControlItem6.TextToControlDistance = 5;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(289, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(661, 24);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frm_Execute_Multi_PHCN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 369);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frm_Execute_Multi_PHCN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thực hiện PHCN";
            this.Load += new System.EventHandler(this.frm_Execute_Multi_PHCN_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtChonNgayChiDinh.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtChonNgayChiDinh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCBExecute.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtExecuteTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtExecuteTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlBenhNhan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBenhNhan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDichVu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDichVu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton btnExecute;
        private DevExpress.XtraEditors.LookUpEdit cboCBExecute;
        private DevExpress.XtraEditors.DateEdit dtExecuteTime;
        private DevExpress.XtraGrid.GridControl gridControlBenhNhan;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewBenhNhan;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.GridControl gridControlDichVu;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDichVu;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.DateEdit dtChonNgayChiDinh;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
    }
}