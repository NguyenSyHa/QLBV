namespace QLBV.FormDanhMuc
{
    partial class frm_DmGoiDV
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.grcDSGoi = new DevExpress.XtraGrid.GridControl();
            this.grvDSGoi = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIDGoi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenGoi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colstatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDSDTBN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDonGiaGoi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.btnXoa = new DevExpress.XtraEditors.SimpleButton();
            this.btnSua = new DevExpress.XtraEditors.SimpleButton();
            this.btnThemMoi = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.grcChiTiet = new DevExpress.XtraGrid.GridControl();
            this.grvChiTiet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTieuNhom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDonGia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIDGoict = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcDSGoi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDSGoi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcChiTiet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvChiTiet)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(795, 180);
            this.panelControl1.TabIndex = 0;
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.grcDSGoi);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(795, 180);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Danh sách gói dịch vụ";
            // 
            // grcDSGoi
            // 
            this.grcDSGoi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcDSGoi.Location = new System.Drawing.Point(2, 22);
            this.grcDSGoi.MainView = this.grvDSGoi;
            this.grcDSGoi.Name = "grcDSGoi";
            this.grcDSGoi.Size = new System.Drawing.Size(791, 156);
            this.grcDSGoi.TabIndex = 0;
            this.grcDSGoi.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDSGoi});
            // 
            // grvDSGoi
            // 
            this.grvDSGoi.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIDGoi,
            this.colTenGoi,
            this.colstatus,
            this.colDSDTBN,
            this.colDonGiaGoi});
            this.grvDSGoi.GridControl = this.grcDSGoi;
            this.grvDSGoi.Name = "grvDSGoi";
            this.grvDSGoi.OptionsBehavior.ReadOnly = true;
            this.grvDSGoi.OptionsView.ShowGroupPanel = false;
            this.grvDSGoi.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvDSGoi_FocusedRowChanged);
            // 
            // colIDGoi
            // 
            this.colIDGoi.AppearanceCell.Options.UseTextOptions = true;
            this.colIDGoi.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIDGoi.Caption = "ID";
            this.colIDGoi.FieldName = "ID";
            this.colIDGoi.Name = "colIDGoi";
            this.colIDGoi.Visible = true;
            this.colIDGoi.VisibleIndex = 0;
            this.colIDGoi.Width = 70;
            // 
            // colTenGoi
            // 
            this.colTenGoi.Caption = "Tên gói DV";
            this.colTenGoi.FieldName = "TenGoi";
            this.colTenGoi.Name = "colTenGoi";
            this.colTenGoi.Visible = true;
            this.colTenGoi.VisibleIndex = 1;
            this.colTenGoi.Width = 485;
            // 
            // colstatus
            // 
            this.colstatus.AppearanceCell.Options.UseTextOptions = true;
            this.colstatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colstatus.Caption = "Trạng thái";
            this.colstatus.FieldName = "TrangThai";
            this.colstatus.Name = "colstatus";
            this.colstatus.Visible = true;
            this.colstatus.VisibleIndex = 3;
            this.colstatus.Width = 189;
            // 
            // colDSDTBN
            // 
            this.colDSDTBN.AppearanceCell.Options.UseTextOptions = true;
            this.colDSDTBN.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDSDTBN.Caption = "Đối tượng sử dụng";
            this.colDSDTBN.FieldName = "DTBNSD";
            this.colDSDTBN.Name = "colDSDTBN";
            this.colDSDTBN.Visible = true;
            this.colDSDTBN.VisibleIndex = 4;
            this.colDSDTBN.Width = 157;
            // 
            // colDonGiaGoi
            // 
            this.colDonGiaGoi.Caption = "Giá trọn gói";
            this.colDonGiaGoi.DisplayFormat.FormatString = "###,###,00";
            this.colDonGiaGoi.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colDonGiaGoi.FieldName = "DonGia";
            this.colDonGiaGoi.Name = "colDonGiaGoi";
            this.colDonGiaGoi.Visible = true;
            this.colDonGiaGoi.VisibleIndex = 2;
            this.colDonGiaGoi.Width = 177;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.btnThoat);
            this.panelControl2.Controls.Add(this.btnXoa);
            this.panelControl2.Controls.Add(this.btnSua);
            this.panelControl2.Controls.Add(this.btnThemMoi);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 460);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(795, 39);
            this.panelControl2.TabIndex = 1;
            this.panelControl2.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControl2_Paint);
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.Location = new System.Drawing.Point(717, 8);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(75, 23);
            this.btnThoat.TabIndex = 0;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.Appearance.Options.UseFont = true;
            this.btnXoa.Location = new System.Drawing.Point(643, 8);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(75, 23);
            this.btnXoa.TabIndex = 0;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.Appearance.Options.UseFont = true;
            this.btnSua.Location = new System.Drawing.Point(569, 8);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(75, 23);
            this.btnSua.TabIndex = 0;
            this.btnSua.Text = "Sửa";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThemMoi
            // 
            this.btnThemMoi.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThemMoi.Appearance.Options.UseFont = true;
            this.btnThemMoi.Location = new System.Drawing.Point(495, 8);
            this.btnThemMoi.Name = "btnThemMoi";
            this.btnThemMoi.Size = new System.Drawing.Size(75, 23);
            this.btnThemMoi.TabIndex = 0;
            this.btnThemMoi.Text = "Mới";
            this.btnThemMoi.Click += new System.EventHandler(this.btnThemMoi_Click);
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.groupControl2);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 180);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(795, 280);
            this.panelControl3.TabIndex = 2;
            // 
            // groupControl2
            // 
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.Controls.Add(this.grcChiTiet);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(795, 280);
            this.groupControl2.TabIndex = 0;
            this.groupControl2.Text = "Chi tiết";
            // 
            // grcChiTiet
            // 
            this.grcChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcChiTiet.Location = new System.Drawing.Point(2, 22);
            this.grcChiTiet.MainView = this.grvChiTiet;
            this.grcChiTiet.Name = "grcChiTiet";
            this.grcChiTiet.Size = new System.Drawing.Size(791, 256);
            this.grcChiTiet.TabIndex = 0;
            this.grcChiTiet.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvChiTiet});
            // 
            // grvChiTiet
            // 
            this.grvChiTiet.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaDV,
            this.colTenDV,
            this.colTieuNhom,
            this.colDonGia,
            this.colIDGoict});
            this.grvChiTiet.GridControl = this.grcChiTiet;
            this.grvChiTiet.Name = "grvChiTiet";
            this.grvChiTiet.OptionsBehavior.ReadOnly = true;
            this.grvChiTiet.OptionsView.ShowGroupPanel = false;
            // 
            // colMaDV
            // 
            this.colMaDV.AppearanceCell.Options.UseTextOptions = true;
            this.colMaDV.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMaDV.Caption = "Mã DV";
            this.colMaDV.FieldName = "MaDV";
            this.colMaDV.Name = "colMaDV";
            this.colMaDV.Visible = true;
            this.colMaDV.VisibleIndex = 0;
            this.colMaDV.Width = 111;
            // 
            // colTenDV
            // 
            this.colTenDV.Caption = "Tên dịch vụ";
            this.colTenDV.FieldName = "TenDV";
            this.colTenDV.Name = "colTenDV";
            this.colTenDV.Visible = true;
            this.colTenDV.VisibleIndex = 1;
            this.colTenDV.Width = 493;
            // 
            // colTieuNhom
            // 
            this.colTieuNhom.AppearanceCell.Options.UseTextOptions = true;
            this.colTieuNhom.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTieuNhom.Caption = "Tiểu nhóm DV";
            this.colTieuNhom.FieldName = "TenTN";
            this.colTieuNhom.Name = "colTieuNhom";
            this.colTieuNhom.Visible = true;
            this.colTieuNhom.VisibleIndex = 3;
            this.colTieuNhom.Width = 357;
            // 
            // colDonGia
            // 
            this.colDonGia.AppearanceCell.Options.UseTextOptions = true;
            this.colDonGia.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDonGia.Caption = "Đơn giá";
            this.colDonGia.FieldName = "DonGia";
            this.colDonGia.Name = "colDonGia";
            this.colDonGia.Visible = true;
            this.colDonGia.VisibleIndex = 2;
            this.colDonGia.Width = 117;
            // 
            // colIDGoict
            // 
            this.colIDGoict.Caption = "gridColumn1";
            this.colIDGoict.FieldName = "IDGoi";
            this.colIDGoict.Name = "colIDGoict";
            // 
            // frm_DmGoiDV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 499);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "frm_DmGoiDV";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh mục gói dịch vụ";
            this.Load += new System.EventHandler(this.frm_DmGoiDV_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcDSGoi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDSGoi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcChiTiet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvChiTiet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl grcDSGoi;
        private DevExpress.XtraGrid.Views.Grid.GridView grvDSGoi;
        private DevExpress.XtraGrid.Columns.GridColumn colIDGoi;
        private DevExpress.XtraGrid.Columns.GridColumn colTenGoi;
        private DevExpress.XtraGrid.Columns.GridColumn colstatus;
        private DevExpress.XtraGrid.Columns.GridColumn colDSDTBN;
        private DevExpress.XtraGrid.GridControl grcChiTiet;
        private DevExpress.XtraGrid.Views.Grid.GridView grvChiTiet;
        private DevExpress.XtraGrid.Columns.GridColumn colMaDV;
        private DevExpress.XtraGrid.Columns.GridColumn colTenDV;
        private DevExpress.XtraGrid.Columns.GridColumn colTieuNhom;
        private DevExpress.XtraGrid.Columns.GridColumn colDonGia;
        private DevExpress.XtraGrid.Columns.GridColumn colIDGoict;
        private DevExpress.XtraEditors.SimpleButton btnXoa;
        private DevExpress.XtraEditors.SimpleButton btnSua;
        private DevExpress.XtraEditors.SimpleButton btnThemMoi;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraGrid.Columns.GridColumn colDonGiaGoi;
    }
}