namespace QLBV.FormNhap
{
    partial class frm_nhappersion
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
            this.components = new System.ComponentModel.Container();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.grdpersion = new DevExpress.XtraGrid.GridControl();
            this.grpersion = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.col_IDPerson = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_SThe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_MaDTuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_TenBNhan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_GTinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_DChi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_HanBHTu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_HanBHDen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_NgayCap = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Status = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_MaCS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_MaTinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_MaHuyen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_MaXa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_NSinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_NgaySinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_ThangSinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_NgayHM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_KhuVuc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnhuy = new DevExpress.XtraEditors.SimpleButton();
            this.bdpersion = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdpersion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpersion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdpersion)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.grdpersion);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(924, 478);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Danh sách thẻ bảo hiểm y tế hết hạn sử dụng";
            // 
            // grdpersion
            // 
            this.grdpersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdpersion.Location = new System.Drawing.Point(2, 20);
            this.grdpersion.MainView = this.grpersion;
            this.grdpersion.Name = "grdpersion";
            this.grdpersion.Size = new System.Drawing.Size(920, 456);
            this.grdpersion.TabIndex = 0;
            this.grdpersion.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grpersion});
            // 
            // grpersion
            // 
            this.grpersion.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col_IDPerson,
            this.col_SThe,
            this.col_MaDTuong,
            this.col_TenBNhan,
            this.col_GTinh,
            this.col_DChi,
            this.col_HanBHTu,
            this.col_HanBHDen,
            this.col_NgayCap,
            this.col_Status,
            this.col_MaCS,
            this.col_MaTinh,
            this.col_MaHuyen,
            this.col_MaXa,
            this.col_NSinh,
            this.col_NgaySinh,
            this.col_ThangSinh,
            this.col_NgayHM,
            this.col_KhuVuc});
            this.grpersion.GridControl = this.grdpersion;
            this.grpersion.Name = "grpersion";
            this.grpersion.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.grpersion.OptionsNavigation.AutoFocusNewRow = true;
            this.grpersion.OptionsNavigation.EnterMoveNextColumn = true;
            this.grpersion.OptionsView.ColumnAutoWidth = false;
            this.grpersion.OptionsView.EnableAppearanceEvenRow = true;
            this.grpersion.OptionsView.EnableAppearanceOddRow = true;
            this.grpersion.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.grpersion.OptionsView.ShowGroupPanel = false;
            this.grpersion.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grpersion_CellValueChanged);
            this.grpersion.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.grpersion_ValidateRow);
            // 
            // col_IDPerson
            // 
            this.col_IDPerson.Caption = "IDPerson";
            this.col_IDPerson.FieldName = "IDPerson";
            this.col_IDPerson.Name = "col_IDPerson";
            // 
            // col_SThe
            // 
            this.col_SThe.Caption = "Số thẻ";
            this.col_SThe.FieldName = "SThe";
            this.col_SThe.Name = "col_SThe";
            this.col_SThe.Visible = true;
            this.col_SThe.VisibleIndex = 0;
            this.col_SThe.Width = 139;
            // 
            // col_MaDTuong
            // 
            this.col_MaDTuong.Caption = "Mã ĐTượng";
            this.col_MaDTuong.FieldName = "MaDTuong";
            this.col_MaDTuong.Name = "col_MaDTuong";
            this.col_MaDTuong.Visible = true;
            this.col_MaDTuong.VisibleIndex = 1;
            this.col_MaDTuong.Width = 56;
            // 
            // col_TenBNhan
            // 
            this.col_TenBNhan.Caption = "Tên BNhân";
            this.col_TenBNhan.FieldName = "TenBNhan";
            this.col_TenBNhan.Name = "col_TenBNhan";
            this.col_TenBNhan.Visible = true;
            this.col_TenBNhan.VisibleIndex = 2;
            this.col_TenBNhan.Width = 140;
            // 
            // col_GTinh
            // 
            this.col_GTinh.Caption = "GTính";
            this.col_GTinh.FieldName = "GTinh";
            this.col_GTinh.Name = "col_GTinh";
            this.col_GTinh.Visible = true;
            this.col_GTinh.VisibleIndex = 3;
            this.col_GTinh.Width = 42;
            // 
            // col_DChi
            // 
            this.col_DChi.Caption = "Địa chỉ";
            this.col_DChi.FieldName = "DChi";
            this.col_DChi.Name = "col_DChi";
            this.col_DChi.Visible = true;
            this.col_DChi.VisibleIndex = 4;
            this.col_DChi.Width = 110;
            // 
            // col_HanBHTu
            // 
            this.col_HanBHTu.Caption = "Hạn BH từ";
            this.col_HanBHTu.FieldName = "HanBHTu";
            this.col_HanBHTu.Name = "col_HanBHTu";
            this.col_HanBHTu.Visible = true;
            this.col_HanBHTu.VisibleIndex = 5;
            this.col_HanBHTu.Width = 108;
            // 
            // col_HanBHDen
            // 
            this.col_HanBHDen.Caption = "Hạn BH đến";
            this.col_HanBHDen.FieldName = "HanBHDen";
            this.col_HanBHDen.Name = "col_HanBHDen";
            this.col_HanBHDen.Visible = true;
            this.col_HanBHDen.VisibleIndex = 6;
            this.col_HanBHDen.Width = 105;
            // 
            // col_NgayCap
            // 
            this.col_NgayCap.Caption = "Ngày cấp";
            this.col_NgayCap.FieldName = "NgayCap";
            this.col_NgayCap.Name = "col_NgayCap";
            this.col_NgayCap.Visible = true;
            this.col_NgayCap.VisibleIndex = 7;
            this.col_NgayCap.Width = 88;
            // 
            // col_Status
            // 
            this.col_Status.Caption = "Status";
            this.col_Status.FieldName = "Status";
            this.col_Status.Name = "col_Status";
            // 
            // col_MaCS
            // 
            this.col_MaCS.Caption = "Mã CS";
            this.col_MaCS.FieldName = "MaCS";
            this.col_MaCS.Name = "col_MaCS";
            this.col_MaCS.Visible = true;
            this.col_MaCS.VisibleIndex = 8;
            this.col_MaCS.Width = 56;
            // 
            // col_MaTinh
            // 
            this.col_MaTinh.Caption = "Mã tỉnh";
            this.col_MaTinh.FieldName = "MaTinh";
            this.col_MaTinh.Name = "col_MaTinh";
            this.col_MaTinh.Visible = true;
            this.col_MaTinh.VisibleIndex = 9;
            this.col_MaTinh.Width = 60;
            // 
            // col_MaHuyen
            // 
            this.col_MaHuyen.Caption = "Mã huyện";
            this.col_MaHuyen.FieldName = "MaHuyen";
            this.col_MaHuyen.Name = "col_MaHuyen";
            this.col_MaHuyen.Visible = true;
            this.col_MaHuyen.VisibleIndex = 10;
            this.col_MaHuyen.Width = 66;
            // 
            // col_MaXa
            // 
            this.col_MaXa.Caption = "Mã xã";
            this.col_MaXa.FieldName = "MaXa";
            this.col_MaXa.Name = "col_MaXa";
            this.col_MaXa.Visible = true;
            this.col_MaXa.VisibleIndex = 11;
            this.col_MaXa.Width = 59;
            // 
            // col_NSinh
            // 
            this.col_NSinh.Caption = "Năm sinh";
            this.col_NSinh.FieldName = "NSinh";
            this.col_NSinh.Name = "col_NSinh";
            this.col_NSinh.Visible = true;
            this.col_NSinh.VisibleIndex = 12;
            this.col_NSinh.Width = 63;
            // 
            // col_NgaySinh
            // 
            this.col_NgaySinh.Caption = "Ngày sinh";
            this.col_NgaySinh.FieldName = "NgaySinh";
            this.col_NgaySinh.Name = "col_NgaySinh";
            this.col_NgaySinh.Visible = true;
            this.col_NgaySinh.VisibleIndex = 13;
            this.col_NgaySinh.Width = 58;
            // 
            // col_ThangSinh
            // 
            this.col_ThangSinh.Caption = "Tháng sinh";
            this.col_ThangSinh.FieldName = "ThangSinh";
            this.col_ThangSinh.Name = "col_ThangSinh";
            this.col_ThangSinh.Visible = true;
            this.col_ThangSinh.VisibleIndex = 14;
            this.col_ThangSinh.Width = 60;
            // 
            // col_NgayHM
            // 
            this.col_NgayHM.Caption = "Ngày HM";
            this.col_NgayHM.FieldName = "NgayHM";
            this.col_NgayHM.Name = "col_NgayHM";
            this.col_NgayHM.Visible = true;
            this.col_NgayHM.VisibleIndex = 15;
            this.col_NgayHM.Width = 96;
            // 
            // col_KhuVuc
            // 
            this.col_KhuVuc.Caption = "Khu vực";
            this.col_KhuVuc.FieldName = "KhuVuc";
            this.col_KhuVuc.Name = "col_KhuVuc";
            this.col_KhuVuc.Visible = true;
            this.col_KhuVuc.VisibleIndex = 16;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnhuy);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 485);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(924, 36);
            this.panelControl1.TabIndex = 1;
            // 
            // btnhuy
            // 
            this.btnhuy.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnhuy.Appearance.Options.UseFont = true;
            this.btnhuy.Location = new System.Drawing.Point(796, 5);
            this.btnhuy.Name = "btnhuy";
            this.btnhuy.Size = new System.Drawing.Size(83, 26);
            this.btnhuy.TabIndex = 1;
            this.btnhuy.Text = "Hủy";
            this.btnhuy.Click += new System.EventHandler(this.btnhuy_Click);
            // 
            // frm_nhappersion
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 521);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.groupControl1);
            this.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frm_nhappersion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nhập thẻ hết hạn sử dụng";
            this.Load += new System.EventHandler(this.frm_nhappersion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdpersion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpersion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bdpersion)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl grdpersion;
        private DevExpress.XtraGrid.Views.Grid.GridView grpersion;
        private DevExpress.XtraGrid.Columns.GridColumn col_IDPerson;
        private DevExpress.XtraGrid.Columns.GridColumn col_SThe;
        private DevExpress.XtraGrid.Columns.GridColumn col_MaDTuong;
        private DevExpress.XtraGrid.Columns.GridColumn col_TenBNhan;
        private DevExpress.XtraGrid.Columns.GridColumn col_GTinh;
        private DevExpress.XtraGrid.Columns.GridColumn col_DChi;
        private DevExpress.XtraGrid.Columns.GridColumn col_HanBHTu;
        private DevExpress.XtraGrid.Columns.GridColumn col_HanBHDen;
        private DevExpress.XtraGrid.Columns.GridColumn col_NgayCap;
        private DevExpress.XtraGrid.Columns.GridColumn col_Status;
        private DevExpress.XtraGrid.Columns.GridColumn col_MaCS;
        private DevExpress.XtraGrid.Columns.GridColumn col_MaTinh;
        private DevExpress.XtraGrid.Columns.GridColumn col_MaHuyen;
        private DevExpress.XtraGrid.Columns.GridColumn col_MaXa;
        private DevExpress.XtraGrid.Columns.GridColumn col_NSinh;
        private DevExpress.XtraGrid.Columns.GridColumn col_NgaySinh;
        private DevExpress.XtraGrid.Columns.GridColumn col_ThangSinh;
        private DevExpress.XtraGrid.Columns.GridColumn col_NgayHM;
        private DevExpress.XtraGrid.Columns.GridColumn col_KhuVuc;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.BindingSource bdpersion;
        private DevExpress.XtraEditors.SimpleButton btnhuy;
    }
}