namespace QLBV.FormThamSo
{
    partial class Frm_ChonXuatThuoc
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
            this.sbtCapNatQD = new DevExpress.XtraEditors.SimpleButton();
            this.sbtTimkiem = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lupKho = new DevExpress.XtraEditors.LookUpEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.sbtThoat = new DevExpress.XtraEditors.SimpleButton();
            this.sbtLuu = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.grcDichVu = new DevExpress.XtraGrid.GridControl();
            this.grvDichVu = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.MaQD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MaDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TenDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DonGia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DonVi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NhaCC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SoLuongTon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Chon = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lupKho.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcDichVu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDichVu)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.sbtTimkiem);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.lupKho);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(742, 48);
            this.panelControl1.TabIndex = 0;
            // 
            // sbtCapNatQD
            // 
            this.sbtCapNatQD.Location = new System.Drawing.Point(174, 7);
            this.sbtCapNatQD.Name = "sbtCapNatQD";
            this.sbtCapNatQD.Size = new System.Drawing.Size(100, 23);
            this.sbtCapNatQD.TabIndex = 3;
            this.sbtCapNatQD.Text = "Cập nhật mã QĐ";
            this.sbtCapNatQD.Click += new System.EventHandler(this.sbtCapNatQD_Click);
            // 
            // sbtTimkiem
            // 
            this.sbtTimkiem.Location = new System.Drawing.Point(257, 10);
            this.sbtTimkiem.Name = "sbtTimkiem";
            this.sbtTimkiem.Size = new System.Drawing.Size(90, 19);
            this.sbtTimkiem.TabIndex = 2;
            this.sbtTimkiem.Text = "Tìm kiếm";
            this.sbtTimkiem.Click += new System.EventHandler(this.sbtTimkiem_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Kho thuốc:";
            // 
            // lupKho
            // 
            this.lupKho.Location = new System.Drawing.Point(70, 9);
            this.lupKho.Name = "lupKho";
            this.lupKho.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupKho.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", 200, "Tên kho"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaKP", "MaKP", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lupKho.Properties.DisplayMember = "TenKP";
            this.lupKho.Properties.NullText = "";
            this.lupKho.Properties.ValueMember = "MaKP";
            this.lupKho.Size = new System.Drawing.Size(181, 20);
            this.lupKho.TabIndex = 0;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.sbtCapNatQD);
            this.panelControl2.Controls.Add(this.sbtThoat);
            this.panelControl2.Controls.Add(this.sbtLuu);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 535);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(742, 38);
            this.panelControl2.TabIndex = 1;
            // 
            // sbtThoat
            // 
            this.sbtThoat.Location = new System.Drawing.Point(93, 7);
            this.sbtThoat.Name = "sbtThoat";
            this.sbtThoat.Size = new System.Drawing.Size(75, 23);
            this.sbtThoat.TabIndex = 1;
            this.sbtThoat.Text = "Thoát";
            this.sbtThoat.Click += new System.EventHandler(this.sbtThoat_Click);
            // 
            // sbtLuu
            // 
            this.sbtLuu.Location = new System.Drawing.Point(12, 7);
            this.sbtLuu.Name = "sbtLuu";
            this.sbtLuu.Size = new System.Drawing.Size(75, 23);
            this.sbtLuu.TabIndex = 0;
            this.sbtLuu.Text = "Lưu";
            this.sbtLuu.Click += new System.EventHandler(this.sbtLuu_Click);
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.grcDichVu);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 48);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(742, 487);
            this.panelControl3.TabIndex = 2;
            // 
            // grcDichVu
            // 
            this.grcDichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcDichVu.Location = new System.Drawing.Point(2, 2);
            this.grcDichVu.MainView = this.grvDichVu;
            this.grcDichVu.Name = "grcDichVu";
            this.grcDichVu.Size = new System.Drawing.Size(738, 483);
            this.grcDichVu.TabIndex = 0;
            this.grcDichVu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDichVu});
            // 
            // grvDichVu
            // 
            this.grvDichVu.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.MaQD,
            this.MaDV,
            this.TenDV,
            this.DonGia,
            this.DonVi,
            this.NhaCC,
            this.SoLuongTon,
            this.Chon});
            this.grvDichVu.GridControl = this.grcDichVu;
            this.grvDichVu.GroupCount = 1;
            this.grvDichVu.Name = "grvDichVu";
            this.grvDichVu.OptionsCustomization.AllowGroup = false;
            this.grvDichVu.OptionsView.ColumnAutoWidth = false;
            this.grvDichVu.OptionsView.ShowGroupPanel = false;
            this.grvDichVu.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.TenDV, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.MaQD, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.MaDV, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // MaQD
            // 
            this.MaQD.AppearanceHeader.BackColor = System.Drawing.Color.Red;
            this.MaQD.AppearanceHeader.BackColor2 = System.Drawing.Color.Red;
            this.MaQD.AppearanceHeader.Options.UseBackColor = true;
            this.MaQD.Caption = "MaQD";
            this.MaQD.FieldName = "MaQD";
            this.MaQD.Name = "MaQD";
            this.MaQD.OptionsColumn.AllowEdit = false;
            this.MaQD.OptionsColumn.AllowFocus = false;
            this.MaQD.Visible = true;
            this.MaQD.VisibleIndex = 0;
            // 
            // MaDV
            // 
            this.MaDV.Caption = "Mã dịch vụ";
            this.MaDV.FieldName = "MaDV";
            this.MaDV.Name = "MaDV";
            this.MaDV.OptionsColumn.AllowEdit = false;
            this.MaDV.OptionsColumn.AllowFocus = false;
            this.MaDV.Visible = true;
            this.MaDV.VisibleIndex = 1;
            this.MaDV.Width = 97;
            // 
            // TenDV
            // 
            this.TenDV.Caption = "Tên dịch vụ";
            this.TenDV.FieldName = "TenDV";
            this.TenDV.Name = "TenDV";
            this.TenDV.OptionsColumn.AllowEdit = false;
            this.TenDV.Visible = true;
            this.TenDV.VisibleIndex = 1;
            this.TenDV.Width = 229;
            // 
            // DonGia
            // 
            this.DonGia.Caption = "Đơn giá";
            this.DonGia.DisplayFormat.FormatString = "##,###";
            this.DonGia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.DonGia.FieldName = "DonGia";
            this.DonGia.Name = "DonGia";
            this.DonGia.OptionsColumn.AllowEdit = false;
            this.DonGia.Visible = true;
            this.DonGia.VisibleIndex = 2;
            this.DonGia.Width = 66;
            // 
            // DonVi
            // 
            this.DonVi.Caption = "Đơn vị";
            this.DonVi.FieldName = "DonVi";
            this.DonVi.Name = "DonVi";
            this.DonVi.OptionsColumn.AllowEdit = false;
            this.DonVi.Visible = true;
            this.DonVi.VisibleIndex = 3;
            this.DonVi.Width = 47;
            // 
            // NhaCC
            // 
            this.NhaCC.Caption = "Nhà cung cấp";
            this.NhaCC.FieldName = "TenNCC";
            this.NhaCC.Name = "NhaCC";
            this.NhaCC.OptionsColumn.AllowEdit = false;
            this.NhaCC.Visible = true;
            this.NhaCC.VisibleIndex = 4;
            this.NhaCC.Width = 272;
            // 
            // SoLuongTon
            // 
            this.SoLuongTon.Caption = "Số lượng tồn";
            this.SoLuongTon.FieldName = "SLT";
            this.SoLuongTon.Name = "SoLuongTon";
            this.SoLuongTon.OptionsColumn.AllowEdit = false;
            this.SoLuongTon.Visible = true;
            this.SoLuongTon.VisibleIndex = 5;
            this.SoLuongTon.Width = 82;
            // 
            // Chon
            // 
            this.Chon.Caption = "Sử dụng";
            this.Chon.FieldName = "Chon";
            this.Chon.Name = "Chon";
            this.Chon.Visible = true;
            this.Chon.VisibleIndex = 6;
            this.Chon.Width = 56;
            // 
            // Frm_ChonXuatThuoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 573);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_ChonXuatThuoc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn xuất thuốc";
            this.Load += new System.EventHandler(this.Frm_ChonXuatThuoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lupKho.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcDichVu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDichVu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton sbtTimkiem;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit lupKho;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton sbtThoat;
        private DevExpress.XtraEditors.SimpleButton sbtLuu;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraGrid.GridControl grcDichVu;
        private DevExpress.XtraGrid.Views.Grid.GridView grvDichVu;
        private DevExpress.XtraGrid.Columns.GridColumn MaQD;
        private DevExpress.XtraGrid.Columns.GridColumn MaDV;
        private DevExpress.XtraGrid.Columns.GridColumn TenDV;
        private DevExpress.XtraGrid.Columns.GridColumn DonGia;
        private DevExpress.XtraGrid.Columns.GridColumn NhaCC;
        private DevExpress.XtraGrid.Columns.GridColumn SoLuongTon;
        private DevExpress.XtraGrid.Columns.GridColumn Chon;
        private DevExpress.XtraGrid.Columns.GridColumn DonVi;
        private DevExpress.XtraEditors.SimpleButton sbtCapNatQD;
    }
}