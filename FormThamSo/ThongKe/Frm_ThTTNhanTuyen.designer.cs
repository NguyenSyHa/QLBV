﻿namespace QLBV.FormThamSo
{
    partial class Frm_ThTTNhanTuyen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_ThTTNhanTuyen));
            this.lupDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.lupTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.grcKhoaphong = new DevExpress.XtraGrid.GridControl();
            this.grvKhoaphong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Chọn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.TenKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MaKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.RadBN = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.radIn = new DevExpress.XtraEditors.RadioGroup();
            this.radMauBC = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaphong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaphong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RadBN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radIn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMauBC.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lupDenNgay
            // 
            this.lupDenNgay.EditValue = null;
            this.lupDenNgay.Location = new System.Drawing.Point(312, 12);
            this.lupDenNgay.Name = "lupDenNgay";
            this.lupDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDenNgay.Size = new System.Drawing.Size(117, 20);
            this.lupDenNgay.TabIndex = 1;
            // 
            // lupTuNgay
            // 
            this.lupTuNgay.EditValue = null;
            this.lupTuNgay.Location = new System.Drawing.Point(110, 12);
            this.lupTuNgay.Name = "lupTuNgay";
            this.lupTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTuNgay.Size = new System.Drawing.Size(116, 20);
            this.lupTuNgay.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(237, 15);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(62, 14);
            this.labelControl2.TabIndex = 51;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(17, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(54, 14);
            this.labelControl1.TabIndex = 50;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.Image = ((System.Drawing.Image)(resources.GetObject("btnThoat.Image")));
            this.btnThoat.Location = new System.Drawing.Point(335, 352);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(89, 27);
            this.btnThoat.TabIndex = 4;
            this.btnThoat.Text = "&Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(223, 352);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(87, 27);
            this.btnOK.TabIndex = 3;
            this.btnOK.TabStop = false;
            this.btnOK.Text = "In báo &cáo";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.grcKhoaphong);
            this.groupControl1.Location = new System.Drawing.Point(108, 38);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(321, 160);
            this.groupControl1.TabIndex = 52;
            // 
            // grcKhoaphong
            // 
            this.grcKhoaphong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcKhoaphong.Location = new System.Drawing.Point(2, 2);
            this.grcKhoaphong.MainView = this.grvKhoaphong;
            this.grcKhoaphong.Margin = new System.Windows.Forms.Padding(4);
            this.grcKhoaphong.Name = "grcKhoaphong";
            this.grcKhoaphong.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.grcKhoaphong.Size = new System.Drawing.Size(317, 156);
            this.grcKhoaphong.TabIndex = 0;
            this.grcKhoaphong.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvKhoaphong});
            // 
            // grvKhoaphong
            // 
            this.grvKhoaphong.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Chọn,
            this.TenKP,
            this.MaKP});
            this.grvKhoaphong.GridControl = this.grcKhoaphong;
            this.grvKhoaphong.Name = "grvKhoaphong";
            this.grvKhoaphong.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.grvKhoaphong.OptionsView.ShowGroupPanel = false;
            this.grvKhoaphong.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvKhoaphong_CellValueChanging);
            // 
            // Chọn
            // 
            this.Chọn.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chọn.AppearanceHeader.Options.UseFont = true;
            this.Chọn.AppearanceHeader.Options.UseTextOptions = true;
            this.Chọn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Chọn.Caption = "Chọn";
            this.Chọn.ColumnEdit = this.repositoryItemCheckEdit1;
            this.Chọn.FieldName = "chon";
            this.Chọn.MinWidth = 10;
            this.Chọn.Name = "Chọn";
            this.Chọn.Visible = true;
            this.Chọn.VisibleIndex = 0;
            this.Chọn.Width = 47;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Caption = "Check";
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // TenKP
            // 
            this.TenKP.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TenKP.AppearanceCell.Options.UseFont = true;
            this.TenKP.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TenKP.AppearanceHeader.Options.UseFont = true;
            this.TenKP.AppearanceHeader.Options.UseTextOptions = true;
            this.TenKP.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TenKP.Caption = "Khoa Phòng";
            this.TenKP.FieldName = "tenkp";
            this.TenKP.Name = "TenKP";
            this.TenKP.OptionsColumn.AllowEdit = false;
            this.TenKP.Visible = true;
            this.TenKP.VisibleIndex = 1;
            this.TenKP.Width = 252;
            // 
            // MaKP
            // 
            this.MaKP.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaKP.AppearanceHeader.Options.UseFont = true;
            this.MaKP.Caption = "MaKP";
            this.MaKP.FieldName = "makp";
            this.MaKP.Name = "MaKP";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(17, 44);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(79, 14);
            this.labelControl4.TabIndex = 53;
            this.labelControl4.Text = "Khoa Phòng:";
            // 
            // RadBN
            // 
            this.RadBN.Location = new System.Drawing.Point(110, 204);
            this.RadBN.Name = "RadBN";
            this.RadBN.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.RadBN.Properties.Appearance.Options.UseFont = true;
            this.RadBN.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Tất cả BN"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "BN Nội trú"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "BN Ngoại trú")});
            this.RadBN.Size = new System.Drawing.Size(317, 24);
            this.RadBN.TabIndex = 2;
            this.RadBN.SelectedIndexChanged += new System.EventHandler(this.RadBN_SelectedIndexChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(17, 209);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(88, 14);
            this.labelControl3.TabIndex = 54;
            this.labelControl3.Text = "Nội|Ngoại Trú:";
            // 
            // radIn
            // 
            this.radIn.Location = new System.Drawing.Point(108, 235);
            this.radIn.Name = "radIn";
            this.radIn.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.radIn.Properties.Appearance.Options.UseFont = true;
            this.radIn.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Thống kê BN chuyển đến và đã ra viện"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Thống kê tất cả các BN chuyển đến ")});
            this.radIn.Size = new System.Drawing.Size(316, 47);
            this.radIn.TabIndex = 55;
            // 
            // radMauBC
            // 
            this.radMauBC.Location = new System.Drawing.Point(108, 288);
            this.radMauBC.Name = "radMauBC";
            this.radMauBC.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.radMauBC.Properties.Appearance.Options.UseFont = true;
            this.radMauBC.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Dùng chung"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Trung tâm y tế Tp. Bắc Ninh")});
            this.radMauBC.Size = new System.Drawing.Size(316, 47);
            this.radMauBC.TabIndex = 55;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Location = new System.Drawing.Point(17, 295);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(50, 14);
            this.labelControl5.TabIndex = 54;
            this.labelControl5.Text = "Mẫu BC:";
            // 
            // Frm_ThTTNhanTuyen
            // 
            this.AcceptButton = this.btnOK;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 391);
            this.Controls.Add(this.radMauBC);
            this.Controls.Add(this.radIn);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.RadBN);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.lupDenNgay);
            this.Controls.Add(this.lupTuNgay);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnOK);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Margin = new System.Windows.Forms.Padding(9);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_ThTTNhanTuyen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tổng hợp thông tin nhận người bệnh từ các tuyến chuyển đến";
            this.Load += new System.EventHandler(this.Frm_ThTTNhanTuyen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaphong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaphong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RadBN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radIn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMauBC.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit lupDenNgay;
        private DevExpress.XtraEditors.DateEdit lupTuNgay;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl grcKhoaphong;
        private DevExpress.XtraGrid.Views.Grid.GridView grvKhoaphong;
        private DevExpress.XtraGrid.Columns.GridColumn Chọn;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn TenKP;
        private DevExpress.XtraGrid.Columns.GridColumn MaKP;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.RadioGroup RadBN;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.RadioGroup radIn;
        private DevExpress.XtraEditors.RadioGroup radMauBC;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}