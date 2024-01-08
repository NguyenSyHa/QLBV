namespace QLBV.FormNhap
{
    partial class frm_ChiTietPL
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
            this.grcChiTietDt = new DevExpress.XtraGrid.GridControl();
            this.grvChiTietDt = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.COLTENDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.COLDONVI = new DevExpress.XtraGrid.Columns.GridColumn();
            this.COLSOLUONG = new DevExpress.XtraGrid.Columns.GridColumn();
            this.COLDONGIA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.COLTHANHTIEN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.COLMADV = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grcChiTietDt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvChiTietDt)).BeginInit();
            this.SuspendLayout();
            // 
            // grcChiTietDt
            // 
            this.grcChiTietDt.Dock = System.Windows.Forms.DockStyle.Top;
            this.grcChiTietDt.Location = new System.Drawing.Point(0, 0);
            this.grcChiTietDt.MainView = this.grvChiTietDt;
            this.grcChiTietDt.Name = "grcChiTietDt";
            this.grcChiTietDt.Size = new System.Drawing.Size(676, 390);
            this.grcChiTietDt.TabIndex = 0;
            this.grcChiTietDt.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvChiTietDt});
            this.grcChiTietDt.Click += new System.EventHandler(this.grcChiTietDt_Click);
            // 
            // grvChiTietDt
            // 
            this.grvChiTietDt.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.COLTENDV,
            this.COLDONVI,
            this.COLSOLUONG,
            this.COLDONGIA,
            this.COLTHANHTIEN,
            this.COLMADV});
            this.grvChiTietDt.GridControl = this.grcChiTietDt;
            this.grvChiTietDt.Name = "grvChiTietDt";
            this.grvChiTietDt.OptionsBehavior.AutoSelectAllInEditor = false;
            this.grvChiTietDt.OptionsBehavior.Editable = false;
            this.grvChiTietDt.OptionsBehavior.ReadOnly = true;
            this.grvChiTietDt.OptionsView.ShowGroupPanel = false;
            this.grvChiTietDt.OptionsView.ShowViewCaption = true;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.simpleButton1.Location = new System.Drawing.Point(0, 390);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(676, 27);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "THOÁT";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // COLTENDV
            // 
            this.COLTENDV.AppearanceCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.COLTENDV.AppearanceCell.Options.UseFont = true;
            this.COLTENDV.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.COLTENDV.AppearanceHeader.Options.UseFont = true;
            this.COLTENDV.Caption = "Tên Thuốc";
            this.COLTENDV.FieldName = "TenDV";
            this.COLTENDV.Name = "COLTENDV";
            this.COLTENDV.Visible = true;
            this.COLTENDV.VisibleIndex = 1;
            this.COLTENDV.Width = 470;
            // 
            // COLDONVI
            // 
            this.COLDONVI.AppearanceCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.COLDONVI.AppearanceCell.Options.UseFont = true;
            this.COLDONVI.AppearanceCell.Options.UseTextOptions = true;
            this.COLDONVI.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.COLDONVI.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.COLDONVI.AppearanceHeader.Options.UseFont = true;
            this.COLDONVI.AppearanceHeader.Options.UseTextOptions = true;
            this.COLDONVI.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.COLDONVI.Caption = "Đơn Vị";
            this.COLDONVI.FieldName = "DonVi";
            this.COLDONVI.Name = "COLDONVI";
            this.COLDONVI.Visible = true;
            this.COLDONVI.VisibleIndex = 2;
            this.COLDONVI.Width = 114;
            // 
            // COLSOLUONG
            // 
            this.COLSOLUONG.AppearanceCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.COLSOLUONG.AppearanceCell.Options.UseFont = true;
            this.COLSOLUONG.AppearanceCell.Options.UseTextOptions = true;
            this.COLSOLUONG.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.COLSOLUONG.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.COLSOLUONG.AppearanceHeader.Options.UseFont = true;
            this.COLSOLUONG.AppearanceHeader.Options.UseTextOptions = true;
            this.COLSOLUONG.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.COLSOLUONG.Caption = "Số Lượng";
            this.COLSOLUONG.FieldName = "SoLuong";
            this.COLSOLUONG.Name = "COLSOLUONG";
            this.COLSOLUONG.Visible = true;
            this.COLSOLUONG.VisibleIndex = 3;
            this.COLSOLUONG.Width = 106;
            // 
            // COLDONGIA
            // 
            this.COLDONGIA.AppearanceCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.COLDONGIA.AppearanceCell.Options.UseFont = true;
            this.COLDONGIA.AppearanceCell.Options.UseTextOptions = true;
            this.COLDONGIA.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.COLDONGIA.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.COLDONGIA.AppearanceHeader.Options.UseFont = true;
            this.COLDONGIA.AppearanceHeader.Options.UseTextOptions = true;
            this.COLDONGIA.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.COLDONGIA.Caption = "Đơn Giá";
            this.COLDONGIA.FieldName = "DonGia";
            this.COLDONGIA.Name = "COLDONGIA";
            this.COLDONGIA.Visible = true;
            this.COLDONGIA.VisibleIndex = 4;
            this.COLDONGIA.Width = 153;
            // 
            // COLTHANHTIEN
            // 
            this.COLTHANHTIEN.AppearanceCell.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.COLTHANHTIEN.AppearanceCell.Options.UseFont = true;
            this.COLTHANHTIEN.AppearanceCell.Options.UseTextOptions = true;
            this.COLTHANHTIEN.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.COLTHANHTIEN.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.COLTHANHTIEN.AppearanceHeader.Options.UseFont = true;
            this.COLTHANHTIEN.AppearanceHeader.Options.UseTextOptions = true;
            this.COLTHANHTIEN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.COLTHANHTIEN.Caption = "Thành Tiền";
            this.COLTHANHTIEN.FieldName = "ThanhTien";
            this.COLTHANHTIEN.Name = "COLTHANHTIEN";
            this.COLTHANHTIEN.Visible = true;
            this.COLTHANHTIEN.VisibleIndex = 5;
            this.COLTHANHTIEN.Width = 171;
            // 
            // COLMADV
            // 
            this.COLMADV.Caption = "gridColumn1";
            this.COLMADV.FieldName = "MaDV";
            this.COLMADV.Name = "COLMADV";
            // 
            // frm_ChiTietPL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 417);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.grcChiTietDt);
            this.Name = "frm_ChiTietPL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chi tiết phiếu lĩnh";
            this.Load += new System.EventHandler(this.frm_ChiTietPL_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grcChiTietDt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvChiTietDt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grcChiTietDt;
        private DevExpress.XtraGrid.Views.Grid.GridView grvChiTietDt;
        private DevExpress.XtraGrid.Columns.GridColumn COLTENDV;
        private DevExpress.XtraGrid.Columns.GridColumn COLDONVI;
        private DevExpress.XtraGrid.Columns.GridColumn COLSOLUONG;
        private DevExpress.XtraGrid.Columns.GridColumn COLDONGIA;
        private DevExpress.XtraGrid.Columns.GridColumn COLTHANHTIEN;
        private DevExpress.XtraGrid.Columns.GridColumn COLMADV;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;

    }
}