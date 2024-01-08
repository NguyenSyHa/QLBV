namespace QLBV.FormThamSo
{
    partial class frm_DSCapGiayChungSinh
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_DSCapGiayChungSinh));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.linkLabelUnselectAll = new System.Windows.Forms.LinkLabel();
            this.linkLabelSelectAll = new System.Windows.Forms.LinkLabel();
            this.checkedListBoxControlDoiTuong = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.lupKhoaPhong = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkXuatexcel = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dtpDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.dtpTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.btnXuatFileExcell = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlDoiTuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKhoaPhong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkXuatexcel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTuNgay.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.linkLabelUnselectAll);
            this.groupControl1.Controls.Add(this.linkLabelSelectAll);
            this.groupControl1.Controls.Add(this.checkedListBoxControlDoiTuong);
            this.groupControl1.Controls.Add(this.lupKhoaPhong);
            this.groupControl1.Controls.Add(this.chkXuatexcel);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.dtpDenNgay);
            this.groupControl1.Controls.Add(this.dtpTuNgay);
            this.groupControl1.Location = new System.Drawing.Point(-2, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(430, 225);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Ngày cấp";
            this.groupControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupControl1_Paint);
            // 
            // linkLabelUnselectAll
            // 
            this.linkLabelUnselectAll.AutoSize = true;
            this.linkLabelUnselectAll.Location = new System.Drawing.Point(191, 202);
            this.linkLabelUnselectAll.Name = "linkLabelUnselectAll";
            this.linkLabelUnselectAll.Size = new System.Drawing.Size(76, 13);
            this.linkLabelUnselectAll.TabIndex = 10;
            this.linkLabelUnselectAll.TabStop = true;
            this.linkLabelUnselectAll.Text = "Bỏ chọn tất cả";
            this.linkLabelUnselectAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelUnselectAll_LinkClicked);
            // 
            // linkLabelSelectAll
            // 
            this.linkLabelSelectAll.AutoSize = true;
            this.linkLabelSelectAll.Location = new System.Drawing.Point(122, 202);
            this.linkLabelSelectAll.Name = "linkLabelSelectAll";
            this.linkLabelSelectAll.Size = new System.Drawing.Size(63, 13);
            this.linkLabelSelectAll.TabIndex = 10;
            this.linkLabelSelectAll.TabStop = true;
            this.linkLabelSelectAll.Text = "Chọn tất cả";
            this.linkLabelSelectAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSelectAll_LinkClicked);
            // 
            // checkedListBoxControlDoiTuong
            // 
            this.checkedListBoxControlDoiTuong.DisplayMember = "DTBN1";
            this.checkedListBoxControlDoiTuong.Location = new System.Drawing.Point(122, 133);
            this.checkedListBoxControlDoiTuong.Name = "checkedListBoxControlDoiTuong";
            this.checkedListBoxControlDoiTuong.Size = new System.Drawing.Size(290, 62);
            this.checkedListBoxControlDoiTuong.TabIndex = 9;
            this.checkedListBoxControlDoiTuong.ValueMember = "IDDTBN";
            // 
            // lupKhoaPhong
            // 
            this.lupKhoaPhong.Location = new System.Drawing.Point(122, 102);
            this.lupKhoaPhong.Name = "lupKhoaPhong";
            this.lupKhoaPhong.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupKhoaPhong.Properties.Appearance.Options.UseFont = true;
            this.lupKhoaPhong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupKhoaPhong.Properties.DisplayMember = "tenkp";
            this.lupKhoaPhong.Properties.NullText = "";
            this.lupKhoaPhong.Properties.ValueMember = "makp";
            this.lupKhoaPhong.Properties.View = this.gridLookUpEdit1View;
            this.lupKhoaPhong.Size = new System.Drawing.Size(290, 24);
            this.lupKhoaPhong.TabIndex = 7;
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Mã";
            this.gridColumn1.FieldName = "makp";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 140;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Khoa Phòng";
            this.gridColumn2.FieldName = "tenkp";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 556;
            // 
            // chkXuatexcel
            // 
            this.chkXuatexcel.Location = new System.Drawing.Point(337, 201);
            this.chkXuatexcel.Name = "chkXuatexcel";
            this.chkXuatexcel.Properties.Caption = "Xuất Excel";
            this.chkXuatexcel.Size = new System.Drawing.Size(75, 19);
            this.chkXuatexcel.TabIndex = 6;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(22, 132);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(68, 18);
            this.labelControl4.TabIndex = 5;
            this.labelControl4.Text = "Đối tượng:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(22, 105);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(83, 18);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Khoa phòng:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(22, 74);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(68, 18);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(22, 44);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(87, 18);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Sinh từ ngày:";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.EditValue = null;
            this.dtpDenNgay.Location = new System.Drawing.Point(122, 71);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDenNgay.Properties.Appearance.Options.UseFont = true;
            this.dtpDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDenNgay.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
            this.dtpDenNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtpDenNgay.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
            this.dtpDenNgay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtpDenNgay.Properties.Mask.EditMask = "dd/MM/yyyy HH:mm:ss";
            this.dtpDenNgay.Size = new System.Drawing.Size(290, 24);
            this.dtpDenNgay.TabIndex = 1;
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.EditValue = null;
            this.dtpTuNgay.Location = new System.Drawing.Point(122, 41);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTuNgay.Properties.Appearance.Options.UseFont = true;
            this.dtpTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpTuNgay.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
            this.dtpTuNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtpTuNgay.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
            this.dtpTuNgay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtpTuNgay.Properties.Mask.EditMask = "dd/MM/yyyy HH:mm:ss";
            this.dtpTuNgay.Size = new System.Drawing.Size(290, 24);
            this.dtpTuNgay.TabIndex = 0;
            // 
            // btnXuatFileExcell
            // 
            this.btnXuatFileExcell.Image = ((System.Drawing.Image)(resources.GetObject("btnXuatFileExcell.Image")));
            this.btnXuatFileExcell.Location = new System.Drawing.Point(219, 231);
            this.btnXuatFileExcell.Name = "btnXuatFileExcell";
            this.btnXuatFileExcell.Size = new System.Drawing.Size(97, 33);
            this.btnXuatFileExcell.TabIndex = 1;
            this.btnXuatFileExcell.Text = "Tạo báo cáo";
            this.btnXuatFileExcell.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Image = ((System.Drawing.Image)(resources.GetObject("btnHuy.Image")));
            this.btnHuy.Location = new System.Drawing.Point(324, 231);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(86, 33);
            this.btnHuy.TabIndex = 2;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // frm_DSCapGiayChungSinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 276);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnXuatFileExcell);
            this.Controls.Add(this.groupControl1);
            this.Name = "frm_DSCapGiayChungSinh";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xuất file Excell cấp giấy chứng sinh";
            this.Load += new System.EventHandler(this.frm_DSCapGiayChungSinh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlDoiTuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKhoaPhong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkXuatexcel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTuNgay.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dtpDenNgay;
        private DevExpress.XtraEditors.DateEdit dtpTuNgay;
        private DevExpress.XtraEditors.SimpleButton btnXuatFileExcell;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CheckEdit chkXuatexcel;
        private DevExpress.XtraEditors.GridLookUpEdit lupKhoaPhong;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControlDoiTuong;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private System.Windows.Forms.LinkLabel linkLabelUnselectAll;
        private System.Windows.Forms.LinkLabel linkLabelSelectAll;

    }
}