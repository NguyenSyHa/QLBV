namespace QLBV.FormThamSo
{
    partial class Frm_BcTonDuoc_BG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_BcTonDuoc_BG));
            this.label5 = new System.Windows.Forms.Label();
            this.lupKho = new DevExpress.XtraEditors.LookUpEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbPL = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnInBC = new DevExpress.XtraEditors.SimpleButton();
            this.dateTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.grcKhoaphong = new DevExpress.XtraGrid.GridControl();
            this.grvKhoaphong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Chọn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.TenKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MaKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ckcTieuNhomDV = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.cklNhomDV = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.chkAllTieuNhom = new DevExpress.XtraEditors.CheckEdit();
            this.chkAllNhom = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.lupKho.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaphong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaphong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckcTieuNhomDV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cklNhomDV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllTieuNhom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllNhom.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(438, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 17);
            this.label5.TabIndex = 116;
            this.label5.Text = "Kho:";
            // 
            // lupKho
            // 
            this.lupKho.Location = new System.Drawing.Point(534, 66);
            this.lupKho.Name = "lupKho";
            this.lupKho.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupKho.Properties.Appearance.Options.UseFont = true;
            this.lupKho.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupKho.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", 200, "Tên khoa phòng"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaKP", "Mã khoa phòng", 200, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lupKho.Properties.DisplayMember = "TenKP";
            this.lupKho.Properties.NullText = "";
            this.lupKho.Properties.PopupSizeable = false;
            this.lupKho.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupKho.Properties.ValueMember = "MaKP";
            this.lupKho.Size = new System.Drawing.Size(305, 24);
            this.lupKho.TabIndex = 109;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 17);
            this.label4.TabIndex = 115;
            this.label4.Text = "Phân loại DV:";
            // 
            // cmbPL
            // 
            this.cmbPL.EditValue = "Tất cả";
            this.cmbPL.Location = new System.Drawing.Point(115, 99);
            this.cmbPL.Name = "cmbPL";
            this.cmbPL.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPL.Properties.Appearance.Options.UseFont = true;
            this.cmbPL.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPL.Properties.Items.AddRange(new object[] {
            "Tất cả",
            "Thuốc",
            "Hóa chất",
            "Vật tư "});
            this.cmbPL.Size = new System.Drawing.Size(291, 24);
            this.cmbPL.TabIndex = 110;
            this.cmbPL.SelectedIndexChanged += new System.EventHandler(this.cmbPL_SelectedIndexChanged);
            this.cmbPL.EditValueChanged += new System.EventHandler(this.cmbPL_EditValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 113;
            this.label2.Text = "Ngày tháng";
            // 
            // btnHuy
            // 
            this.btnHuy.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuy.Appearance.Options.UseFont = true;
            this.btnHuy.Image = ((System.Drawing.Image)(resources.GetObject("btnHuy.Image")));
            this.btnHuy.Location = new System.Drawing.Point(747, 336);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(92, 23);
            this.btnHuy.TabIndex = 112;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnInBC
            // 
            this.btnInBC.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInBC.Appearance.Options.UseFont = true;
            this.btnInBC.Image = ((System.Drawing.Image)(resources.GetObject("btnInBC.Image")));
            this.btnInBC.Location = new System.Drawing.Point(639, 336);
            this.btnInBC.Name = "btnInBC";
            this.btnInBC.Size = new System.Drawing.Size(92, 23);
            this.btnInBC.TabIndex = 111;
            this.btnInBC.Text = "&In Báo cáo";
            this.btnInBC.Click += new System.EventHandler(this.btnInBC_Click);
            // 
            // dateTuNgay
            // 
            this.dateTuNgay.EditValue = null;
            this.dateTuNgay.Location = new System.Drawing.Point(115, 66);
            this.dateTuNgay.Name = "dateTuNgay";
            this.dateTuNgay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTuNgay.Properties.Appearance.Options.UseFont = true;
            this.dateTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTuNgay.Size = new System.Drawing.Size(289, 24);
            this.dateTuNgay.TabIndex = 107;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.grcKhoaphong);
            this.groupControl1.Location = new System.Drawing.Point(115, 133);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(291, 198);
            this.groupControl1.TabIndex = 118;
            // 
            // grcKhoaphong
            // 
            this.grcKhoaphong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcKhoaphong.Location = new System.Drawing.Point(2, 2);
            this.grcKhoaphong.MainView = this.grvKhoaphong;
            this.grcKhoaphong.Name = "grcKhoaphong";
            this.grcKhoaphong.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.grcKhoaphong.Size = new System.Drawing.Size(287, 194);
            this.grcKhoaphong.TabIndex = 2;
            this.grcKhoaphong.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvKhoaphong});
            // 
            // grvKhoaphong
            // 
            this.grvKhoaphong.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvKhoaphong.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvKhoaphong.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Chọn,
            this.TenKP,
            this.MaKP});
            this.grvKhoaphong.GridControl = this.grcKhoaphong;
            this.grvKhoaphong.Name = "grvKhoaphong";
            this.grvKhoaphong.OptionsView.EnableAppearanceEvenRow = true;
            this.grvKhoaphong.OptionsView.EnableAppearanceOddRow = true;
            this.grvKhoaphong.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.grvKhoaphong.OptionsView.ShowGroupPanel = false;
            this.grvKhoaphong.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvKhoaphong_CellValueChanging);
            // 
            // Chọn
            // 
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
            this.TenKP.Caption = "Tên K.phòng";
            this.TenKP.FieldName = "tenkp";
            this.TenKP.Name = "TenKP";
            this.TenKP.OptionsColumn.AllowEdit = false;
            this.TenKP.Visible = true;
            this.TenKP.VisibleIndex = 1;
            this.TenKP.Width = 222;
            // 
            // MaKP
            // 
            this.MaKP.Caption = "MaKP";
            this.MaKP.FieldName = "makp";
            this.MaKP.Name = "MaKP";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 17);
            this.label1.TabIndex = 117;
            this.label1.Text = "Khoa|Phòng:";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.richTextBox1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.richTextBox1.Location = new System.Drawing.Point(32, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(807, 48);
            this.richTextBox1.TabIndex = 119;
            this.richTextBox1.Text = "Báo cáo lấy số lượng tồn của các xã phường đến một ngày tháng xác định được lựa c" +
    "họn trên tham số";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(438, 217);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 17);
            this.label3.TabIndex = 545;
            this.label3.Text = "Tiểu Nhóm:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label6.Location = new System.Drawing.Point(438, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 17);
            this.label6.TabIndex = 546;
            this.label6.Text = "Nhóm:";
            // 
            // ckcTieuNhomDV
            // 
            this.ckcTieuNhomDV.CheckOnClick = true;
            this.ckcTieuNhomDV.Location = new System.Drawing.Point(534, 236);
            this.ckcTieuNhomDV.Name = "ckcTieuNhomDV";
            this.ckcTieuNhomDV.Size = new System.Drawing.Size(305, 95);
            this.ckcTieuNhomDV.TabIndex = 543;
            // 
            // cklNhomDV
            // 
            this.cklNhomDV.CheckOnClick = true;
            this.cklNhomDV.Location = new System.Drawing.Point(534, 112);
            this.cklNhomDV.Name = "cklNhomDV";
            this.cklNhomDV.Size = new System.Drawing.Size(305, 103);
            this.cklNhomDV.TabIndex = 544;
            this.cklNhomDV.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.cklNhomDV_ItemCheck);
            // 
            // chkAllTieuNhom
            // 
            this.chkAllTieuNhom.Location = new System.Drawing.Point(537, 216);
            this.chkAllTieuNhom.Name = "chkAllTieuNhom";
            this.chkAllTieuNhom.Properties.Caption = "Chọn tất cả";
            this.chkAllTieuNhom.Size = new System.Drawing.Size(75, 19);
            this.chkAllTieuNhom.TabIndex = 547;
            this.chkAllTieuNhom.CheckedChanged += new System.EventHandler(this.chkAllTieuNhom_CheckedChanged);
            // 
            // chkAllNhom
            // 
            this.chkAllNhom.Location = new System.Drawing.Point(537, 92);
            this.chkAllNhom.Name = "chkAllNhom";
            this.chkAllNhom.Properties.Caption = "Chọn tất cả";
            this.chkAllNhom.Size = new System.Drawing.Size(75, 19);
            this.chkAllNhom.TabIndex = 547;
            this.chkAllNhom.CheckedChanged += new System.EventHandler(this.chkAllNhom_CheckedChanged);
            // 
            // Frm_BcTonDuoc_BG
            // 
            this.AcceptButton = this.btnInBC;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 366);
            this.Controls.Add(this.chkAllNhom);
            this.Controls.Add(this.chkAllTieuNhom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ckcTieuNhomDV);
            this.Controls.Add(this.cklNhomDV);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lupKho);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbPL);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnInBC);
            this.Controls.Add(this.dateTuNgay);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_BcTonDuoc_BG";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo tồn xã";
            this.Load += new System.EventHandler(this.Frm_BcSuDungDuoc_BG_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupKho.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaphong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaphong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckcTieuNhomDV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cklNhomDV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllTieuNhom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllNhom.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.LookUpEdit lupKho;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.ComboBoxEdit cmbPL;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnInBC;
        private DevExpress.XtraEditors.DateEdit dateTuNgay;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl grcKhoaphong;
        private DevExpress.XtraGrid.Views.Grid.GridView grvKhoaphong;
        private DevExpress.XtraGrid.Columns.GridColumn Chọn;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn TenKP;
        private DevExpress.XtraGrid.Columns.GridColumn MaKP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.CheckedListBoxControl ckcTieuNhomDV;
        private DevExpress.XtraEditors.CheckedListBoxControl cklNhomDV;
        private DevExpress.XtraEditors.CheckEdit chkAllTieuNhom;
        private DevExpress.XtraEditors.CheckEdit chkAllNhom;
    }
}