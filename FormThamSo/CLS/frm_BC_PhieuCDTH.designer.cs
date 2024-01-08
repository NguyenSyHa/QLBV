namespace QLBV.TraCuu
{
    partial class frm_BC_PhieuCDTH
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
            this.btnIn = new System.Windows.Forms.Button();
            this.grcTHDV = new DevExpress.XtraGrid.GridControl();
            this.grvCDTH = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colChon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDonGia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoluong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colThanhTien = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grcTHDV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCDTH)).BeginInit();
            this.SuspendLayout();
            // 
            // btnIn
            // 
            this.btnIn.Location = new System.Drawing.Point(279, 269);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(129, 41);
            this.btnIn.TabIndex = 0;
            this.btnIn.Text = "In";
            this.btnIn.UseVisualStyleBackColor = true;
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // grcTHDV
            // 
            this.grcTHDV.Location = new System.Drawing.Point(0, 0);
            this.grcTHDV.MainView = this.grvCDTH;
            this.grcTHDV.Name = "grcTHDV";
            this.grcTHDV.Size = new System.Drawing.Size(662, 252);
            this.grcTHDV.TabIndex = 1;
            this.grcTHDV.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvCDTH});
            // 
            // grvCDTH
            // 
            this.grvCDTH.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colChon,
            this.colTenDV,
            this.colDonGia,
            this.colSoluong,
            this.colThanhTien});
            this.grvCDTH.GridControl = this.grcTHDV;
            this.grvCDTH.Name = "grvCDTH";
            this.grvCDTH.OptionsView.ShowGroupPanel = false;
            this.grvCDTH.OptionsView.ShowViewCaption = true;
            this.grvCDTH.ViewCaption = "Bảng chỉ định tổng hợp";
            this.grvCDTH.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvCDTH_CellValueChanging);
            // 
            // colChon
            // 
            this.colChon.Caption = "Chọn";
            this.colChon.FieldName = "Chon";
            this.colChon.Name = "colChon";
            this.colChon.Visible = true;
            this.colChon.VisibleIndex = 0;
            this.colChon.Width = 61;
            // 
            // colTenDV
            // 
            this.colTenDV.Caption = "Tên dịch vụ";
            this.colTenDV.FieldName = "TenDV";
            this.colTenDV.Name = "colTenDV";
            this.colTenDV.Visible = true;
            this.colTenDV.VisibleIndex = 1;
            this.colTenDV.Width = 515;
            // 
            // colDonGia
            // 
            this.colDonGia.Caption = "Đơn giá";
            this.colDonGia.FieldName = "DonGia";
            this.colDonGia.Name = "colDonGia";
            this.colDonGia.Visible = true;
            this.colDonGia.VisibleIndex = 2;
            this.colDonGia.Width = 168;
            // 
            // colSoluong
            // 
            this.colSoluong.Caption = "Số lượng";
            this.colSoluong.FieldName = "SoLuong";
            this.colSoluong.Name = "colSoluong";
            this.colSoluong.Visible = true;
            this.colSoluong.VisibleIndex = 3;
            this.colSoluong.Width = 124;
            // 
            // colThanhTien
            // 
            this.colThanhTien.Caption = "Thành tiền";
            this.colThanhTien.FieldName = "ThanhTien";
            this.colThanhTien.Name = "colThanhTien";
            this.colThanhTien.Visible = true;
            this.colThanhTien.VisibleIndex = 4;
            this.colThanhTien.Width = 210;
            // 
            // frm_BC_PhieuCDTH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 322);
            this.Controls.Add(this.grcTHDV);
            this.Controls.Add(this.btnIn);
            this.Name = "frm_BC_PhieuCDTH";
            this.Text = "frm_BC_PhieuCDTH";
            this.Load += new System.EventHandler(this.frm_BC_PhieuCDTH_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grcTHDV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCDTH)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnIn;
        private DevExpress.XtraGrid.GridControl grcTHDV;
        private DevExpress.XtraGrid.Views.Grid.GridView grvCDTH;
        private DevExpress.XtraGrid.Columns.GridColumn colChon;
        private DevExpress.XtraGrid.Columns.GridColumn colTenDV;
        private DevExpress.XtraGrid.Columns.GridColumn colDonGia;
        private DevExpress.XtraGrid.Columns.GridColumn colSoluong;
        private DevExpress.XtraGrid.Columns.GridColumn colThanhTien;
    }
}