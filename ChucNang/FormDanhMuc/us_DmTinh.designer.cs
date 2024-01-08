namespace QLBV.FormDanhMuc
{
    partial class us_DmTinh
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(us_DmTinh));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtTenTinh = new DevExpress.XtraEditors.TextEdit();
            this.cboStatus = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtMaTinh = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.grcDmTinh = new DevExpress.XtraGrid.GridControl();
            this.grvDmTinh = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaTinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenTinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.txtTimKiem = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.btnXoa = new DevExpress.XtraEditors.SimpleButton();
            this.btnSua = new DevExpress.XtraEditors.SimpleButton();
            this.btnMoi = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenTinh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaTinh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcDmTinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDmTinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimKiem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.AppearanceCaption.ForeColor = System.Drawing.Color.Teal;
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl1.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.txtTenTinh);
            this.groupControl1.Controls.Add(this.cboStatus);
            this.groupControl1.Controls.Add(this.txtMaTinh);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1024, 103);
            this.groupControl1.TabIndex = 20;
            this.groupControl1.Text = "DANH MỤC TỈNH";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Location = new System.Drawing.Point(48, 49);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(52, 13);
            this.labelControl6.TabIndex = 18;
            this.labelControl6.Text = "Mã tỉnh*:";
            // 
            // txtTenTinh
            // 
            this.txtTenTinh.Location = new System.Drawing.Point(272, 47);
            this.txtTenTinh.Name = "txtTenTinh";
            this.txtTenTinh.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTenTinh.Properties.Appearance.Options.UseFont = true;
            this.txtTenTinh.Size = new System.Drawing.Size(197, 20);
            this.txtTenTinh.TabIndex = 11;
            // 
            // cboStatus
            // 
            this.cboStatus.EditValue = "Hoạt động";
            this.cboStatus.Location = new System.Drawing.Point(541, 45);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cboStatus.Properties.Appearance.Options.UseFont = true;
            this.cboStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStatus.Properties.Items.AddRange(new object[] {
            "Hoạt động",
            "Không hoạt động"});
            this.cboStatus.Properties.PopupSizeable = true;
            this.cboStatus.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboStatus.Size = new System.Drawing.Size(118, 20);
            this.cboStatus.TabIndex = 14;
            // 
            // txtMaTinh
            // 
            this.txtMaTinh.Location = new System.Drawing.Point(122, 47);
            this.txtMaTinh.Name = "txtMaTinh";
            this.txtMaTinh.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtMaTinh.Properties.Appearance.Options.UseFont = true;
            this.txtMaTinh.Properties.MaxLength = 2;
            this.txtMaTinh.Size = new System.Drawing.Size(63, 20);
            this.txtMaTinh.TabIndex = 10;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(194, 50);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(56, 13);
            this.labelControl2.TabIndex = 15;
            this.labelControl2.Text = "Tên tỉnh*:";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Location = new System.Drawing.Point(474, 48);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(61, 13);
            this.labelControl5.TabIndex = 17;
            this.labelControl5.Text = "Trạng thái:";
            // 
            // grcDmTinh
            // 
            this.grcDmTinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcDmTinh.Location = new System.Drawing.Point(0, 103);
            this.grcDmTinh.MainView = this.grvDmTinh;
            this.grcDmTinh.Name = "grcDmTinh";
            this.grcDmTinh.Size = new System.Drawing.Size(1024, 497);
            this.grcDmTinh.TabIndex = 21;
            this.grcDmTinh.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDmTinh});
            // 
            // grvDmTinh
            // 
            this.grvDmTinh.Appearance.GroupRow.Options.UseTextOptions = true;
            this.grvDmTinh.Appearance.GroupRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvDmTinh.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvDmTinh.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvDmTinh.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvDmTinh.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvDmTinh.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvDmTinh.Appearance.Row.Options.UseFont = true;
            this.grvDmTinh.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaTinh,
            this.colTenTinh,
            this.colStatus});
            this.grvDmTinh.GridControl = this.grcDmTinh;
            this.grvDmTinh.Name = "grvDmTinh";
            this.grvDmTinh.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.grvDmTinh.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.grvDmTinh.OptionsBehavior.Editable = false;
            this.grvDmTinh.OptionsNavigation.EnterMoveNextColumn = true;
            this.grvDmTinh.OptionsView.ColumnAutoWidth = false;
            this.grvDmTinh.OptionsView.EnableAppearanceEvenRow = true;
            this.grvDmTinh.OptionsView.EnableAppearanceOddRow = true;
            this.grvDmTinh.OptionsView.ShowGroupPanel = false;
            this.grvDmTinh.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvDanToc_FocusedRowChanged);
            // 
            // colMaTinh
            // 
            this.colMaTinh.Caption = "Mã tỉnh";
            this.colMaTinh.FieldName = "MaTinh";
            this.colMaTinh.Name = "colMaTinh";
            this.colMaTinh.Visible = true;
            this.colMaTinh.VisibleIndex = 0;
            this.colMaTinh.Width = 113;
            // 
            // colTenTinh
            // 
            this.colTenTinh.Caption = "Tên tỉnh";
            this.colTenTinh.FieldName = "TenTinh";
            this.colTenTinh.Name = "colTenTinh";
            this.colTenTinh.Visible = true;
            this.colTenTinh.VisibleIndex = 1;
            this.colTenTinh.Width = 289;
            // 
            // colStatus
            // 
            this.colStatus.Caption = "Trạng thái";
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 2;
            this.colStatus.Width = 107;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.txtTimKiem);
            this.panelControl2.Controls.Add(this.labelControl4);
            this.panelControl2.Controls.Add(this.panelControl4);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 561);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1024, 39);
            this.panelControl2.TabIndex = 22;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.EditValue = "Tìm kiếm (theo tên tỉnh)";
            this.txtTimKiem.Location = new System.Drawing.Point(341, 9);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTimKiem.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.txtTimKiem.Properties.Appearance.Options.UseFont = true;
            this.txtTimKiem.Properties.Appearance.Options.UseForeColor = true;
            this.txtTimKiem.Properties.NullText = "Tìm kiếm (theo tên DT):";
            this.txtTimKiem.Size = new System.Drawing.Size(306, 20);
            this.txtTimKiem.TabIndex = 34;
            this.txtTimKiem.EditValueChanged += new System.EventHandler(this.txtTimKiem_EditValueChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.labelControl4.Location = new System.Drawing.Point(315, 15);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(0, 13);
            this.labelControl4.TabIndex = 35;
            // 
            // panelControl4
            // 
            this.panelControl4.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControl4.Appearance.Options.UseBackColor = true;
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Controls.Add(this.btnLuu);
            this.panelControl4.Controls.Add(this.btnXoa);
            this.panelControl4.Controls.Add(this.btnSua);
            this.panelControl4.Controls.Add(this.btnMoi);
            this.panelControl4.Location = new System.Drawing.Point(70, 5);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(230, 29);
            this.panelControl4.TabIndex = 33;
            // 
            // btnLuu
            // 
            this.btnLuu.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.btnLuu.Appearance.Options.UseFont = true;
            this.btnLuu.Appearance.Options.UseForeColor = true;
            this.btnLuu.Image = global::QLBV.Properties.Resources.saveall_16x16;
            this.btnLuu.Location = new System.Drawing.Point(177, 3);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(48, 23);
            this.btnLuu.TabIndex = 3;
            this.btnLuu.Text = "&Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.btnXoa.Appearance.Options.UseFont = true;
            this.btnXoa.Appearance.Options.UseForeColor = true;
            this.btnXoa.Image = ((System.Drawing.Image)(resources.GetObject("btnXoa.Image")));
            this.btnXoa.Location = new System.Drawing.Point(120, 3);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(51, 23);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "&Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.btnSua.Appearance.Options.UseFont = true;
            this.btnSua.Appearance.Options.UseForeColor = true;
            this.btnSua.Image = ((System.Drawing.Image)(resources.GetObject("btnSua.Image")));
            this.btnSua.Location = new System.Drawing.Point(60, 3);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(54, 23);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "&Sửa";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnMoi
            // 
            this.btnMoi.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoi.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.btnMoi.Appearance.Options.UseFont = true;
            this.btnMoi.Appearance.Options.UseForeColor = true;
            this.btnMoi.Image = ((System.Drawing.Image)(resources.GetObject("btnMoi.Image")));
            this.btnMoi.Location = new System.Drawing.Point(4, 3);
            this.btnMoi.Name = "btnMoi";
            this.btnMoi.Size = new System.Drawing.Size(50, 23);
            this.btnMoi.TabIndex = 0;
            this.btnMoi.Text = "&Mới";
            this.btnMoi.Click += new System.EventHandler(this.btnMoi_Click);
            // 
            // us_DmTinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.grcDmTinh);
            this.Controls.Add(this.groupControl1);
            this.Name = "us_DmTinh";
            this.Size = new System.Drawing.Size(1024, 600);
            this.Load += new System.EventHandler(this.us_DmTinh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenTinh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaTinh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcDmTinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDmTinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimKiem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtTenTinh;
        private DevExpress.XtraEditors.ComboBoxEdit cboStatus;
        private DevExpress.XtraEditors.TextEdit txtMaTinh;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraGrid.GridControl grcDmTinh;
        private DevExpress.XtraGrid.Views.Grid.GridView grvDmTinh;
        private DevExpress.XtraGrid.Columns.GridColumn colMaTinh;
        private DevExpress.XtraGrid.Columns.GridColumn colTenTinh;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.TextEdit txtTimKiem;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraEditors.SimpleButton btnXoa;
        private DevExpress.XtraEditors.SimpleButton btnSua;
        private DevExpress.XtraEditors.SimpleButton btnMoi;
    }
}
