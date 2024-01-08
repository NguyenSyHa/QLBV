namespace QLBV.FormDanhMuc
{
    partial class Frm_DmHuyen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_DmHuyen));
            this.lupMaTinh = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.BtnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.txtTimKiem = new DevExpress.XtraEditors.TextEdit();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.btnXoa = new DevExpress.XtraEditors.SimpleButton();
            this.btnSua = new DevExpress.XtraEditors.SimpleButton();
            this.grcDmHuyen = new DevExpress.XtraGrid.GridControl();
            this.grvDmHuyen = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaHuyen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenHuyen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnMoi = new DevExpress.XtraEditors.SimpleButton();
            this.txtTenHuyen = new DevExpress.XtraEditors.TextEdit();
            this.txtMaHuyen = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.lupMaTinh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimKiem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcDmHuyen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDmHuyen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenHuyen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaHuyen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            this.SuspendLayout();
            // 
            // lupMaTinh
            // 
            this.lupMaTinh.Location = new System.Drawing.Point(642, 12);
            this.lupMaTinh.Name = "lupMaTinh";
            this.lupMaTinh.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupMaTinh.Properties.Appearance.Options.UseFont = true;
            this.lupMaTinh.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupMaTinh.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTinh", 100, "Tên Tỉnh"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaTinh", "Mã Tỉnh", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lupMaTinh.Properties.DisplayMember = "TenTinh";
            this.lupMaTinh.Properties.NullText = "";
            this.lupMaTinh.Properties.ValueMember = "MaTinh";
            this.lupMaTinh.Size = new System.Drawing.Size(148, 20);
            this.lupMaTinh.StyleController = this.layoutControl1;
            this.lupMaTinh.TabIndex = 2;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.BtnHuy);
            this.layoutControl1.Controls.Add(this.txtTimKiem);
            this.layoutControl1.Controls.Add(this.btnLuu);
            this.layoutControl1.Controls.Add(this.btnXoa);
            this.layoutControl1.Controls.Add(this.btnSua);
            this.layoutControl1.Controls.Add(this.lupMaTinh);
            this.layoutControl1.Controls.Add(this.grcDmHuyen);
            this.layoutControl1.Controls.Add(this.btnMoi);
            this.layoutControl1.Controls.Add(this.txtTenHuyen);
            this.layoutControl1.Controls.Add(this.txtMaHuyen);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(802, 411);
            this.layoutControl1.TabIndex = 25;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // BtnHuy
            // 
            this.BtnHuy.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.BtnHuy.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.BtnHuy.Appearance.Options.UseFont = true;
            this.BtnHuy.Appearance.Options.UseForeColor = true;
            this.BtnHuy.Location = new System.Drawing.Point(405, 377);
            this.BtnHuy.Name = "BtnHuy";
            this.BtnHuy.Size = new System.Drawing.Size(71, 22);
            this.BtnHuy.StyleController = this.layoutControl1;
            this.BtnHuy.TabIndex = 4;
            this.BtnHuy.Text = "Hủy";
            this.BtnHuy.Click += new System.EventHandler(this.BtnHuy_Click);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.EditValue = "Tìm kiếm ( theo tên huyện )";
            this.txtTimKiem.Location = new System.Drawing.Point(480, 377);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTimKiem.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.txtTimKiem.Properties.Appearance.Options.UseFont = true;
            this.txtTimKiem.Properties.Appearance.Options.UseForeColor = true;
            this.txtTimKiem.Properties.NullText = "Tìm kiếm (theo tên DT):";
            this.txtTimKiem.Size = new System.Drawing.Size(310, 20);
            this.txtTimKiem.StyleController = this.layoutControl1;
            this.txtTimKiem.TabIndex = 0;
            this.txtTimKiem.EditValueChanged += new System.EventHandler(this.txtTimKiem_EditValueChanged);
            // 
            // btnLuu
            // 
            this.btnLuu.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.btnLuu.Appearance.Options.UseFont = true;
            this.btnLuu.Appearance.Options.UseForeColor = true;
            this.btnLuu.Image = ((System.Drawing.Image)(resources.GetObject("btnLuu.Image")));
            this.btnLuu.Location = new System.Drawing.Point(198, 377);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(96, 22);
            this.btnLuu.StyleController = this.layoutControl1;
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
            this.btnXoa.Location = new System.Drawing.Point(298, 377);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(103, 22);
            this.btnXoa.StyleController = this.layoutControl1;
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
            this.btnSua.Location = new System.Drawing.Point(104, 377);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(90, 22);
            this.btnSua.StyleController = this.layoutControl1;
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "&Sửa";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // grcDmHuyen
            // 
            this.grcDmHuyen.Location = new System.Drawing.Point(12, 36);
            this.grcDmHuyen.MainView = this.grvDmHuyen;
            this.grcDmHuyen.Name = "grcDmHuyen";
            this.grcDmHuyen.Size = new System.Drawing.Size(778, 337);
            this.grcDmHuyen.TabIndex = 0;
            this.grcDmHuyen.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDmHuyen});
            // 
            // grvDmHuyen
            // 
            this.grvDmHuyen.Appearance.GroupRow.Options.UseTextOptions = true;
            this.grvDmHuyen.Appearance.GroupRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvDmHuyen.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvDmHuyen.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvDmHuyen.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvDmHuyen.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvDmHuyen.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvDmHuyen.Appearance.Row.Options.UseFont = true;
            this.grvDmHuyen.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaHuyen,
            this.colTenHuyen,
            this.colTinh});
            this.grvDmHuyen.GridControl = this.grcDmHuyen;
            this.grvDmHuyen.Name = "grvDmHuyen";
            this.grvDmHuyen.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.grvDmHuyen.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.grvDmHuyen.OptionsBehavior.Editable = false;
            this.grvDmHuyen.OptionsNavigation.EnterMoveNextColumn = true;
            this.grvDmHuyen.OptionsView.EnableAppearanceEvenRow = true;
            this.grvDmHuyen.OptionsView.EnableAppearanceOddRow = true;
            this.grvDmHuyen.OptionsView.ShowGroupPanel = false;
            this.grvDmHuyen.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvDmHuyen_FocusedRowChanged);
            // 
            // colMaHuyen
            // 
            this.colMaHuyen.Caption = "Mã huyện";
            this.colMaHuyen.FieldName = "MaHuyen";
            this.colMaHuyen.Name = "colMaHuyen";
            this.colMaHuyen.Visible = true;
            this.colMaHuyen.VisibleIndex = 0;
            this.colMaHuyen.Width = 113;
            // 
            // colTenHuyen
            // 
            this.colTenHuyen.Caption = "Tên huyện";
            this.colTenHuyen.FieldName = "TenHuyen";
            this.colTenHuyen.Name = "colTenHuyen";
            this.colTenHuyen.Visible = true;
            this.colTenHuyen.VisibleIndex = 1;
            this.colTenHuyen.Width = 289;
            // 
            // colTinh
            // 
            this.colTinh.Caption = "Tỉnh";
            this.colTinh.FieldName = "MaTinh";
            this.colTinh.Name = "colTinh";
            this.colTinh.Visible = true;
            this.colTinh.VisibleIndex = 2;
            this.colTinh.Width = 107;
            // 
            // btnMoi
            // 
            this.btnMoi.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoi.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.btnMoi.Appearance.Options.UseFont = true;
            this.btnMoi.Appearance.Options.UseForeColor = true;
            this.btnMoi.Image = ((System.Drawing.Image)(resources.GetObject("btnMoi.Image")));
            this.btnMoi.Location = new System.Drawing.Point(12, 377);
            this.btnMoi.Name = "btnMoi";
            this.btnMoi.Size = new System.Drawing.Size(88, 22);
            this.btnMoi.StyleController = this.layoutControl1;
            this.btnMoi.TabIndex = 0;
            this.btnMoi.Text = "&Mới";
            this.btnMoi.Click += new System.EventHandler(this.btnMoi_Click);
            // 
            // txtTenHuyen
            // 
            this.txtTenHuyen.Location = new System.Drawing.Point(263, 12);
            this.txtTenHuyen.Name = "txtTenHuyen";
            this.txtTenHuyen.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTenHuyen.Properties.Appearance.Options.UseFont = true;
            this.txtTenHuyen.Size = new System.Drawing.Size(321, 20);
            this.txtTenHuyen.StyleController = this.layoutControl1;
            this.txtTenHuyen.TabIndex = 1;
            // 
            // txtMaHuyen
            // 
            this.txtMaHuyen.Location = new System.Drawing.Point(66, 12);
            this.txtMaHuyen.Name = "txtMaHuyen";
            this.txtMaHuyen.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtMaHuyen.Properties.Appearance.Options.UseFont = true;
            this.txtMaHuyen.Properties.MaxLength = 3;
            this.txtMaHuyen.Size = new System.Drawing.Size(139, 20);
            this.txtMaHuyen.StyleController = this.layoutControl1;
            this.txtMaHuyen.TabIndex = 0;
            this.txtMaHuyen.EditValueChanged += new System.EventHandler(this.txtMaHuyen_EditValueChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.layoutControlItem4,
            this.layoutControlItem3,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem10});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(802, 411);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnXoa;
            this.layoutControlItem7.Location = new System.Drawing.Point(286, 365);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(107, 26);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnMoi;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 365);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(92, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnSua;
            this.layoutControlItem6.Location = new System.Drawing.Point(92, 365);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(94, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btnLuu;
            this.layoutControlItem8.Location = new System.Drawing.Point(186, 365);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.txtTimKiem;
            this.layoutControlItem9.Location = new System.Drawing.Point(468, 365);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(314, 26);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.grcDmHuyen;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(782, 341);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.lupMaTinh;
            this.layoutControlItem3.Location = new System.Drawing.Point(576, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(206, 24);
            this.layoutControlItem3.Text = "Trạng thái";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(51, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtMaHuyen;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(197, 24);
            this.layoutControlItem1.Text = "Mã huyện";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(51, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtTenHuyen;
            this.layoutControlItem2.Location = new System.Drawing.Point(197, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(379, 24);
            this.layoutControlItem2.Text = "Tên huyện";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(51, 13);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.BtnHuy;
            this.layoutControlItem10.Location = new System.Drawing.Point(393, 365);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(75, 26);
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
            // 
            // Frm_DmHuyen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 411);
            this.Controls.Add(this.layoutControl1);
            this.Name = "Frm_DmHuyen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Frm_DmHuyen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupMaTinh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTimKiem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcDmHuyen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDmHuyen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenHuyen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaHuyen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtTenHuyen;
        private DevExpress.XtraGrid.GridControl grcDmHuyen;
        private DevExpress.XtraGrid.Views.Grid.GridView grvDmHuyen;
        private DevExpress.XtraGrid.Columns.GridColumn colMaHuyen;
        private DevExpress.XtraGrid.Columns.GridColumn colTenHuyen;
        private DevExpress.XtraGrid.Columns.GridColumn colTinh;
        private DevExpress.XtraEditors.TextEdit txtTimKiem;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraEditors.SimpleButton btnXoa;
        private DevExpress.XtraEditors.SimpleButton btnSua;
        private DevExpress.XtraEditors.SimpleButton btnMoi;
        private DevExpress.XtraEditors.LookUpEdit lupMaTinh;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit txtMaHuyen;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton BtnHuy;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
    }
}