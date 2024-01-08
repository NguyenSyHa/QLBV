namespace QLBV.FormThamSo
{
    partial class Frm_BkChiTietThuVP_TY02
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_BkChiTietThuVP_TY02));
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.grcKhoaphong = new DevExpress.XtraGrid.GridControl();
            this.grvKhoaphong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Chon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.TenKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MaKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lupNgayden = new DevExpress.XtraEditors.DateEdit();
            this.lupNgaytu = new DevExpress.XtraEditors.DateEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.cmbPLBN = new System.Windows.Forms.ComboBox();
            this.bntOK = new DevExpress.XtraEditors.SimpleButton();
            this.bntHuy = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaphong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaphong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgayden.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgayden.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(49, 120);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(72, 13);
            this.labelControl3.TabIndex = 15;
            this.labelControl3.Text = "Chọn K.Phòng:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(49, 65);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(50, 13);
            this.labelControl2.TabIndex = 14;
            this.labelControl2.Text = "Ngày đến:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(49, 39);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(43, 13);
            this.labelControl1.TabIndex = 13;
            this.labelControl1.Text = "Ngày từ:";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.grcKhoaphong);
            this.groupControl1.Location = new System.Drawing.Point(141, 123);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(189, 136);
            this.groupControl1.TabIndex = 12;
            // 
            // grcKhoaphong
            // 
            this.grcKhoaphong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcKhoaphong.Location = new System.Drawing.Point(2, 2);
            this.grcKhoaphong.MainView = this.grvKhoaphong;
            this.grcKhoaphong.Name = "grcKhoaphong";
            this.grcKhoaphong.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.grcKhoaphong.Size = new System.Drawing.Size(185, 132);
            this.grcKhoaphong.TabIndex = 2;
            this.grcKhoaphong.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvKhoaphong});
            // 
            // grvKhoaphong
            // 
            this.grvKhoaphong.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Chon,
            this.TenKP,
            this.MaKP});
            this.grvKhoaphong.GridControl = this.grcKhoaphong;
            this.grvKhoaphong.Name = "grvKhoaphong";
            this.grvKhoaphong.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.grvKhoaphong.OptionsView.ShowGroupPanel = false;
            // 
            // Chon
            // 
            this.Chon.Caption = "Chọn";
            this.Chon.ColumnEdit = this.repositoryItemCheckEdit1;
            this.Chon.FieldName = "chon";
            this.Chon.MinWidth = 10;
            this.Chon.Name = "Chon";
            this.Chon.Visible = true;
            this.Chon.VisibleIndex = 0;
            this.Chon.Width = 45;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Caption = "Check";
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // TenKP
            // 
            this.TenKP.Caption = "Tên K.phòng";
            this.TenKP.FieldName = "tenkp";
            this.TenKP.Name = "TenKP";
            this.TenKP.OptionsColumn.AllowEdit = false;
            this.TenKP.Visible = true;
            this.TenKP.VisibleIndex = 1;
            this.TenKP.Width = 122;
            // 
            // MaKP
            // 
            this.MaKP.Caption = "MaKP";
            this.MaKP.FieldName = "makp";
            this.MaKP.Name = "MaKP";
            // 
            // lupNgayden
            // 
            this.lupNgayden.EditValue = null;
            this.lupNgayden.Location = new System.Drawing.Point(143, 62);
            this.lupNgayden.Name = "lupNgayden";
            this.lupNgayden.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgayden.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgayden.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.lupNgayden.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.lupNgayden.Size = new System.Drawing.Size(189, 20);
            this.lupNgayden.TabIndex = 11;
            // 
            // lupNgaytu
            // 
            this.lupNgaytu.EditValue = null;
            this.lupNgaytu.Location = new System.Drawing.Point(143, 36);
            this.lupNgaytu.Name = "lupNgaytu";
            this.lupNgaytu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgaytu.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgaytu.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.lupNgaytu.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.lupNgaytu.Size = new System.Drawing.Size(189, 20);
            this.lupNgaytu.TabIndex = 10;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(49, 93);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(32, 13);
            this.labelControl4.TabIndex = 14;
            this.labelControl4.Text = "Kê BN:";
            this.labelControl4.ToolTip = "Kê bệnh nhân:";
            // 
            // cmbPLBN
            // 
            this.cmbPLBN.FormattingEnabled = true;
            this.cmbPLBN.Items.AddRange(new object[] {
            "Tất cả",
            "Nội trú",
            "Ngoại trú"});
            this.cmbPLBN.Location = new System.Drawing.Point(143, 89);
            this.cmbPLBN.Name = "cmbPLBN";
            this.cmbPLBN.Size = new System.Drawing.Size(189, 21);
            this.cmbPLBN.TabIndex = 18;
            this.cmbPLBN.Text = "Tất cả";
            // 
            // bntOK
            // 
            this.bntOK.Appearance.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.bntOK.Appearance.Options.UseFont = true;
            this.bntOK.Image = ((System.Drawing.Image)(resources.GetObject("bntOK.Image")));
            this.bntOK.Location = new System.Drawing.Point(72, 288);
            this.bntOK.Name = "bntOK";
            this.bntOK.Size = new System.Drawing.Size(99, 23);
            this.bntOK.TabIndex = 16;
            this.bntOK.Text = "&In Báo cáo";
            this.bntOK.Click += new System.EventHandler(this.bntOK_Click);
            // 
            // bntHuy
            // 
            this.bntHuy.Appearance.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.bntHuy.Appearance.Options.UseFont = true;
            this.bntHuy.Image = ((System.Drawing.Image)(resources.GetObject("bntHuy.Image")));
            this.bntHuy.Location = new System.Drawing.Point(229, 288);
            this.bntHuy.Name = "bntHuy";
            this.bntHuy.Size = new System.Drawing.Size(84, 23);
            this.bntHuy.TabIndex = 17;
            this.bntHuy.Text = "Hủy";
            this.bntHuy.Click += new System.EventHandler(this.bntHuy_Click);
            // 
            // Frm_BkChiTietThuVP_TY02
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 333);
            this.Controls.Add(this.cmbPLBN);
            this.Controls.Add(this.bntHuy);
            this.Controls.Add(this.bntOK);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.lupNgayden);
            this.Controls.Add(this.lupNgaytu);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_BkChiTietThuVP_TY02";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "In Bảng kê chi tiết thu viện phí";
            this.Load += new System.EventHandler(this.Frm_BkChiTietThuVP_TY02_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaphong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaphong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgayden.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgayden.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl grcKhoaphong;
        private DevExpress.XtraGrid.Views.Grid.GridView grvKhoaphong;
        private DevExpress.XtraGrid.Columns.GridColumn Chon;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn TenKP;
        private DevExpress.XtraGrid.Columns.GridColumn MaKP;
        private DevExpress.XtraEditors.DateEdit lupNgayden;
        private DevExpress.XtraEditors.DateEdit lupNgaytu;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private System.Windows.Forms.ComboBox cmbPLBN;
        private DevExpress.XtraEditors.SimpleButton bntOK;
        private DevExpress.XtraEditors.SimpleButton bntHuy;
    }
}