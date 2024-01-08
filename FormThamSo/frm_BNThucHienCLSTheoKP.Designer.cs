namespace QLBV.FormThamSo
{
    partial class frm_BNThucHienCLSTheoKP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_BNThucHienCLSTheoKP));
            this.cboKhoaPhong = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dtpTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.dtpDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.cboThongKe = new DevExpress.XtraEditors.ComboBoxEdit();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.cboDoiTuong = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnInBaoCao = new DevExpress.XtraEditors.SimpleButton();
            this.chkHienThiGio = new DevExpress.XtraEditors.CheckEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.cboKhoaPhong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboThongKe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDoiTuong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHienThiGio.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cboKhoaPhong
            // 
            this.cboKhoaPhong.EditValue = "";
            this.cboKhoaPhong.Location = new System.Drawing.Point(93, 20);
            this.cboKhoaPhong.Name = "cboKhoaPhong";
            this.cboKhoaPhong.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboKhoaPhong.Properties.Appearance.Options.UseFont = true;
            this.cboKhoaPhong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboKhoaPhong.Properties.DisplayMember = "TenKP";
            this.cboKhoaPhong.Properties.PopupView = this.gridLookUpEdit1View;
            this.cboKhoaPhong.Properties.ValueMember = "MaKP";
            this.cboKhoaPhong.Size = new System.Drawing.Size(351, 26);
            this.cboKhoaPhong.TabIndex = 0;
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsEditForm.PopupEditFormWidth = 1200;
            this.gridLookUpEdit1View.OptionsImageLoad.DesiredThumbnailSize = new System.Drawing.Size(350, 0);
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.EditValue = null;
            this.dtpTuNgay.Location = new System.Drawing.Point(93, 52);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTuNgay.Properties.Appearance.Options.UseFont = true;
            this.dtpTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpTuNgay.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dtpTuNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpTuNgay.Size = new System.Drawing.Size(143, 26);
            this.dtpTuNgay.TabIndex = 1;
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.EditValue = null;
            this.dtpDenNgay.Location = new System.Drawing.Point(308, 52);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDenNgay.Properties.Appearance.Options.UseFont = true;
            this.dtpDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDenNgay.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dtpDenNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpDenNgay.Size = new System.Drawing.Size(136, 26);
            this.dtpDenNgay.TabIndex = 2;
            // 
            // cboThongKe
            // 
            this.cboThongKe.Location = new System.Drawing.Point(93, 84);
            this.cboThongKe.Name = "cboThongKe";
            this.cboThongKe.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboThongKe.Properties.Appearance.Options.UseFont = true;
            this.cboThongKe.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboThongKe.Properties.Items.AddRange(new object[] {
            "Tất cả bệnh nhân đã thực hiện(Lấy theo ngày thực hiện)",
            "Chỉ bệnh nhân đã thanh toán( Lấy theo ngày thanh toán)"});
            this.cboThongKe.Size = new System.Drawing.Size(351, 26);
            this.cboThongKe.TabIndex = 3;
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(93, 146);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioGroup1.Properties.Appearance.Options.UseFont = true;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Tất cả"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Nội trú"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Ngoại trú")});
            this.radioGroup1.Size = new System.Drawing.Size(351, 45);
            this.radioGroup1.TabIndex = 5;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(12, 25);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(74, 17);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "Khoa phòng:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(34, 57);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(52, 17);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "Từ ngày:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(242, 57);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 17);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "Đến ngày:";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(28, 87);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(58, 17);
            this.labelControl4.TabIndex = 9;
            this.labelControl4.Text = "Thống kê:";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(24, 119);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(62, 17);
            this.labelControl5.TabIndex = 10;
            this.labelControl5.Text = "Đối tượng:";
            // 
            // cboDoiTuong
            // 
            this.cboDoiTuong.Location = new System.Drawing.Point(93, 116);
            this.cboDoiTuong.Name = "cboDoiTuong";
            this.cboDoiTuong.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDoiTuong.Properties.Appearance.Options.UseFont = true;
            this.cboDoiTuong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDoiTuong.Properties.DisplayMember = "tendoituong";
            this.cboDoiTuong.Properties.NullText = "";
            this.cboDoiTuong.Properties.PopupSizeable = false;
            this.cboDoiTuong.Properties.PopupView = this.gridView1;
            this.cboDoiTuong.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cboDoiTuong.Properties.ValueMember = "tendoituong";
            this.cboDoiTuong.Size = new System.Drawing.Size(351, 24);
            this.cboDoiTuong.TabIndex = 12;
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // btnInBaoCao
            // 
            this.btnInBaoCao.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnInBaoCao.ImageOptions.Image")));
            this.btnInBaoCao.Location = new System.Drawing.Point(343, 197);
            this.btnInBaoCao.Name = "btnInBaoCao";
            this.btnInBaoCao.Size = new System.Drawing.Size(101, 35);
            this.btnInBaoCao.TabIndex = 11;
            this.btnInBaoCao.Text = "In báo cáo";
            this.btnInBaoCao.Click += new System.EventHandler(this.btnInBaoCao_Click);
            // 
            // chkHienThiGio
            // 
            this.chkHienThiGio.Location = new System.Drawing.Point(93, 197);
            this.chkHienThiGio.Name = "chkHienThiGio";
            this.chkHienThiGio.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.chkHienThiGio.Properties.Appearance.Options.UseFont = true;
            this.chkHienThiGio.Properties.AutoHeight = false;
            this.chkHienThiGio.Properties.Caption = "Hiển thị giờ";
            this.chkHienThiGio.Size = new System.Drawing.Size(118, 35);
            this.chkHienThiGio.TabIndex = 13;
            this.chkHienThiGio.Visible = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn1.Caption = "Mã khoa phòng";
            this.gridColumn1.FieldName = "MaKP";
            this.gridColumn1.MinWidth = 50;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 80;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Tên khoa phòng";
            this.gridColumn2.FieldName = "TenKP";
            this.gridColumn2.MinWidth = 50;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 250;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Đối Tượng";
            this.gridColumn3.FieldName = "tendoituong";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 250;
            // 
            // frm_BNThucHienCLSTheoKP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 244);
            this.Controls.Add(this.chkHienThiGio);
            this.Controls.Add(this.btnInBaoCao);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.radioGroup1);
            this.Controls.Add(this.cboThongKe);
            this.Controls.Add(this.dtpDenNgay);
            this.Controls.Add(this.dtpTuNgay);
            this.Controls.Add(this.cboKhoaPhong);
            this.Controls.Add(this.cboDoiTuong);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_BNThucHienCLSTheoKP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách bệnh nhân thực hiện cận lâm sàng theo khoa phòng.";
            this.Load += new System.EventHandler(this.frm_BNThucHienCLSTheoKP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cboKhoaPhong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboThongKe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDoiTuong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHienThiGio.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GridLookUpEdit cboKhoaPhong;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraEditors.DateEdit dtpTuNgay;
        private DevExpress.XtraEditors.DateEdit dtpDenNgay;
        private DevExpress.XtraEditors.ComboBoxEdit cboThongKe;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.SimpleButton btnInBaoCao;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.GridLookUpEdit cboDoiTuong;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.CheckEdit chkHienThiGio;
    }
}