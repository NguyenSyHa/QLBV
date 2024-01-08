namespace QLBV.ChucNang.FormDanhMuc
{
    partial class frm_MachineMap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_MachineMap));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtGT = new System.Windows.Forms.TextBox();
            this.btnXoa = new DevExpress.XtraEditors.SimpleButton();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnSua = new DevExpress.XtraEditors.SimpleButton();
            this.btnMoi = new DevExpress.XtraEditors.SimpleButton();
            this.txtMachineID = new DevExpress.XtraEditors.TextEdit();
            this.grcMachine = new DevExpress.XtraGrid.GridControl();
            this.grvMachine = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.IDMachine = new DevExpress.XtraGrid.Columns.GridColumn();
            this.IDTaiSan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.IDDVct = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MAP_VALUE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lupMaDVct = new DevExpress.XtraEditors.LookUpEdit();
            this.lupTSID = new DevExpress.XtraEditors.LookUpEdit();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMachineID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcMachine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMachine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMaDVct.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTSID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtGT);
            this.layoutControl1.Controls.Add(this.btnXoa);
            this.layoutControl1.Controls.Add(this.btnLuu);
            this.layoutControl1.Controls.Add(this.btnHuy);
            this.layoutControl1.Controls.Add(this.btnSua);
            this.layoutControl1.Controls.Add(this.btnMoi);
            this.layoutControl1.Controls.Add(this.txtMachineID);
            this.layoutControl1.Controls.Add(this.grcMachine);
            this.layoutControl1.Controls.Add(this.lupMaDVct);
            this.layoutControl1.Controls.Add(this.lupTSID);
            this.layoutControl1.Controls.Add(this.textEdit1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(876, 490);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtGT
            // 
            this.txtGT.Location = new System.Drawing.Point(693, 12);
            this.txtGT.Name = "txtGT";
            this.txtGT.Size = new System.Drawing.Size(171, 20);
            this.txtGT.TabIndex = 15;
            // 
            // btnXoa
            // 
            this.btnXoa.Image = ((System.Drawing.Image)(resources.GetObject("btnXoa.Image")));
            this.btnXoa.Location = new System.Drawing.Point(339, 456);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(110, 22);
            this.btnXoa.StyleController = this.layoutControl1;
            this.btnXoa.TabIndex = 13;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Image = ((System.Drawing.Image)(resources.GetObject("btnLuu.Image")));
            this.btnLuu.Location = new System.Drawing.Point(110, 456);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(101, 22);
            this.btnLuu.StyleController = this.layoutControl1;
            this.btnLuu.TabIndex = 12;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Image = ((System.Drawing.Image)(resources.GetObject("btnHuy.Image")));
            this.btnHuy.Location = new System.Drawing.Point(453, 456);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(105, 22);
            this.btnHuy.StyleController = this.layoutControl1;
            this.btnHuy.TabIndex = 11;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnSua
            // 
            this.btnSua.Image = ((System.Drawing.Image)(resources.GetObject("btnSua.Image")));
            this.btnSua.Location = new System.Drawing.Point(215, 456);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(120, 22);
            this.btnSua.StyleController = this.layoutControl1;
            this.btnSua.TabIndex = 10;
            this.btnSua.Text = "Sửa";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnMoi
            // 
            this.btnMoi.Image = ((System.Drawing.Image)(resources.GetObject("btnMoi.Image")));
            this.btnMoi.Location = new System.Drawing.Point(12, 456);
            this.btnMoi.Name = "btnMoi";
            this.btnMoi.Size = new System.Drawing.Size(94, 22);
            this.btnMoi.StyleController = this.layoutControl1;
            this.btnMoi.TabIndex = 9;
            this.btnMoi.Text = "Mới";
            this.btnMoi.Click += new System.EventHandler(this.btnMoi_Click);
            // 
            // txtMachineID
            // 
            this.txtMachineID.Location = new System.Drawing.Point(58, 12);
            this.txtMachineID.Name = "txtMachineID";
            this.txtMachineID.Size = new System.Drawing.Size(178, 20);
            this.txtMachineID.StyleController = this.layoutControl1;
            this.txtMachineID.TabIndex = 7;
            // 
            // grcMachine
            // 
            this.grcMachine.Location = new System.Drawing.Point(12, 36);
            this.grcMachine.MainView = this.grvMachine;
            this.grcMachine.Name = "grcMachine";
            this.grcMachine.Size = new System.Drawing.Size(852, 416);
            this.grcMachine.TabIndex = 4;
            this.grcMachine.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvMachine});
            // 
            // grvMachine
            // 
            this.grvMachine.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.IDMachine,
            this.IDTaiSan,
            this.IDDVct,
            this.MAP_VALUE});
            this.grvMachine.GridControl = this.grcMachine;
            this.grvMachine.Name = "grvMachine";
            this.grvMachine.OptionsView.ShowGroupPanel = false;
            this.grvMachine.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvMachine_FocusedRowChanged);
            // 
            // IDMachine
            // 
            this.IDMachine.AppearanceCell.Options.UseTextOptions = true;
            this.IDMachine.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.IDMachine.AppearanceHeader.Options.UseTextOptions = true;
            this.IDMachine.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.IDMachine.Caption = "Mã máy";
            this.IDMachine.FieldName = "LIS_MACHINE_MAP_ID";
            this.IDMachine.Name = "IDMachine";
            this.IDMachine.OptionsColumn.AllowEdit = false;
            this.IDMachine.Visible = true;
            this.IDMachine.VisibleIndex = 0;
            // 
            // IDTaiSan
            // 
            this.IDTaiSan.AppearanceCell.Options.UseTextOptions = true;
            this.IDTaiSan.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.IDTaiSan.AppearanceHeader.Options.UseTextOptions = true;
            this.IDTaiSan.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.IDTaiSan.Caption = "Mã tài sản";
            this.IDTaiSan.FieldName = "TAISAN_ID";
            this.IDTaiSan.Name = "IDTaiSan";
            this.IDTaiSan.OptionsColumn.AllowEdit = false;
            this.IDTaiSan.Visible = true;
            this.IDTaiSan.VisibleIndex = 1;
            // 
            // IDDVct
            // 
            this.IDDVct.AppearanceCell.Options.UseTextOptions = true;
            this.IDDVct.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.IDDVct.AppearanceHeader.Options.UseTextOptions = true;
            this.IDDVct.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.IDDVct.Caption = "Mã dịch vụ chi tiết";
            this.IDDVct.FieldName = "MaDVct";
            this.IDDVct.Name = "IDDVct";
            this.IDDVct.OptionsColumn.AllowEdit = false;
            this.IDDVct.Visible = true;
            this.IDDVct.VisibleIndex = 2;
            // 
            // MAP_VALUE
            // 
            this.MAP_VALUE.AppearanceHeader.Options.UseTextOptions = true;
            this.MAP_VALUE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MAP_VALUE.Caption = "Giá trị";
            this.MAP_VALUE.FieldName = "MAP_VALUE";
            this.MAP_VALUE.Name = "MAP_VALUE";
            this.MAP_VALUE.OptionsColumn.AllowEdit = false;
            this.MAP_VALUE.Visible = true;
            this.MAP_VALUE.VisibleIndex = 3;
            // 
            // lupMaDVct
            // 
            this.lupMaDVct.Location = new System.Drawing.Point(549, 12);
            this.lupMaDVct.Name = "lupMaDVct";
            this.lupMaDVct.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupMaDVct.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaDVct", "Mã dịch vụ chi tiết"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenDVct", "Tên dịch vụ chi tiết")});
            this.lupMaDVct.Properties.DisplayMember = "MaDVct";
            this.lupMaDVct.Properties.NullText = "";
            this.lupMaDVct.Properties.PopupSizeable = false;
            this.lupMaDVct.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupMaDVct.Properties.ValueMember = "MaDVct";
            this.lupMaDVct.Size = new System.Drawing.Size(103, 20);
            this.lupMaDVct.StyleController = this.layoutControl1;
            this.lupMaDVct.TabIndex = 8;
            // 
            // lupTSID
            // 
            this.lupTSID.Location = new System.Drawing.Point(304, 12);
            this.lupTSID.Name = "lupTSID";
            this.lupTSID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTSID.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("IDTS", "Mã tài sản"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GhiChu", "Tên")});
            this.lupTSID.Properties.DisplayMember = "IDTS";
            this.lupTSID.Properties.NullText = "";
            this.lupTSID.Properties.PopupSizeable = false;
            this.lupTSID.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupTSID.Properties.ValueMember = "IDTS";
            this.lupTSID.Size = new System.Drawing.Size(140, 20);
            this.lupTSID.StyleController = this.layoutControl1;
            this.lupTSID.TabIndex = 6;
            // 
            // textEdit1
            // 
            this.textEdit1.EditValue = "";
            this.textEdit1.Location = new System.Drawing.Point(664, 456);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(200, 20);
            this.textEdit1.StyleController = this.layoutControl1;
            this.textEdit1.TabIndex = 14;
            this.textEdit1.EditValueChanged += new System.EventHandler(this.textEdit1_EditValueChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.layoutControlItem2,
            this.layoutControlItem11});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(876, 490);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grcMachine;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(856, 420);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.lupTSID;
            this.layoutControlItem3.Location = new System.Drawing.Point(228, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(208, 24);
            this.layoutControlItem3.Text = "Mã tài sản*:";
            this.layoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(59, 13);
            this.layoutControlItem3.TextToControlDistance = 5;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtMachineID;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(228, 24);
            this.layoutControlItem4.Text = "Mã máy:";
            this.layoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(41, 13);
            this.layoutControlItem4.TextToControlDistance = 5;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.lupMaDVct;
            this.layoutControlItem5.Location = new System.Drawing.Point(436, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(208, 24);
            this.layoutControlItem5.Text = "Mã dịch vụ chi tiết*:";
            this.layoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(96, 13);
            this.layoutControlItem5.TextToControlDistance = 5;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnMoi;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 444);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(98, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnSua;
            this.layoutControlItem7.Location = new System.Drawing.Point(203, 444);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(124, 26);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btnHuy;
            this.layoutControlItem8.Location = new System.Drawing.Point(441, 444);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(109, 26);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnLuu;
            this.layoutControlItem9.Location = new System.Drawing.Point(98, 444);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(105, 26);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.btnXoa;
            this.layoutControlItem10.Location = new System.Drawing.Point(327, 444);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(114, 26);
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.textEdit1;
            this.layoutControlItem2.Location = new System.Drawing.Point(550, 444);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(306, 26);
            this.layoutControlItem2.Text = "Tìm kiếm theo giá trị:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(99, 13);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.txtGT;
            this.layoutControlItem11.Location = new System.Drawing.Point(644, 0);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(212, 24);
            this.layoutControlItem11.Text = "Giá trị:";
            this.layoutControlItem11.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem11.TextSize = new System.Drawing.Size(32, 13);
            this.layoutControlItem11.TextToControlDistance = 5;
            // 
            // frm_MachineMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 490);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frm_MachineMap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frm_MachineMap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtMachineID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcMachine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMachine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMaDVct.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTSID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnXoa;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnSua;
        private DevExpress.XtraEditors.TextEdit txtMachineID;
        private DevExpress.XtraGrid.GridControl grcMachine;
        private DevExpress.XtraGrid.Views.Grid.GridView grvMachine;
        private DevExpress.XtraGrid.Columns.GridColumn IDMachine;
        private DevExpress.XtraGrid.Columns.GridColumn IDTaiSan;
        private DevExpress.XtraGrid.Columns.GridColumn IDDVct;
        private DevExpress.XtraGrid.Columns.GridColumn MAP_VALUE;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.LookUpEdit lupMaDVct;
        private DevExpress.XtraEditors.LookUpEdit lupTSID;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private System.Windows.Forms.TextBox txtGT;
        private DevExpress.XtraEditors.SimpleButton btnMoi;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
    }
}