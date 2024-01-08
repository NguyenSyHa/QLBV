namespace QLBV.FormThamSo
{
    partial class Frm_repTonghopXNKhac
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
            this.LupNgaytu = new DevExpress.XtraEditors.DateEdit();
            this.LupNgayden = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.butTaoBC = new DevExpress.XtraEditors.SimpleButton();
            this.ButHuy = new DevExpress.XtraEditors.SimpleButton();
            this.cboTT = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.grcKhoaphong = new DevExpress.XtraGrid.GridControl();
            this.grvKhoaphong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Chọn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.TenKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MaKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgaytu.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgaytu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayden.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayden.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaphong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaphong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // LupNgaytu
            // 
            this.LupNgaytu.EditValue = null;
            this.LupNgaytu.Location = new System.Drawing.Point(100, 16);
            this.LupNgaytu.Name = "LupNgaytu";
            this.LupNgaytu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgaytu.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgaytu.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.LupNgaytu.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.LupNgaytu.Size = new System.Drawing.Size(187, 20);
            this.LupNgaytu.TabIndex = 0;
            // 
            // LupNgayden
            // 
            this.LupNgayden.EditValue = null;
            this.LupNgayden.Location = new System.Drawing.Point(100, 42);
            this.LupNgayden.Name = "LupNgayden";
            this.LupNgayden.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgayden.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgayden.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.LupNgayden.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.LupNgayden.Size = new System.Drawing.Size(187, 20);
            this.LupNgayden.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(10, 19);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(44, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(10, 45);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(51, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // butTaoBC
            // 
            this.butTaoBC.Location = new System.Drawing.Point(58, 261);
            this.butTaoBC.Name = "butTaoBC";
            this.butTaoBC.Size = new System.Drawing.Size(75, 23);
            this.butTaoBC.TabIndex = 4;
            this.butTaoBC.Text = "Tạo báo cáo";
            this.butTaoBC.Click += new System.EventHandler(this.butTaoBC_Click);
            // 
            // ButHuy
            // 
            this.ButHuy.Location = new System.Drawing.Point(212, 261);
            this.ButHuy.Name = "ButHuy";
            this.ButHuy.Size = new System.Drawing.Size(75, 23);
            this.ButHuy.TabIndex = 5;
            this.ButHuy.Text = "Hủy";
            this.ButHuy.Click += new System.EventHandler(this.ButHuy_Click);
            // 
            // cboTT
            // 
            this.cboTT.Location = new System.Drawing.Point(100, 68);
            this.cboTT.Name = "cboTT";
            this.cboTT.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTT.Properties.Items.AddRange(new object[] {
            "Đã thanh toán",
            "Chưa thanh toán",
            "Cả hải"});
            this.cboTT.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboTT.Size = new System.Drawing.Size(187, 20);
            this.cboTT.TabIndex = 9;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(10, 71);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(59, 13);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "Thanh toán:";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.grcKhoaphong);
            this.groupControl1.Location = new System.Drawing.Point(100, 94);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(189, 136);
            this.groupControl1.TabIndex = 10;
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
            this.Chọn.Caption = "Chọn";
            this.Chọn.ColumnEdit = this.repositoryItemCheckEdit1;
            this.Chọn.FieldName = "chon";
            this.Chọn.MinWidth = 10;
            this.Chọn.Name = "Chọn";
            this.Chọn.Visible = true;
            this.Chọn.VisibleIndex = 0;
            this.Chọn.Width = 45;
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
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 94);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(72, 13);
            this.labelControl4.TabIndex = 11;
            this.labelControl4.Text = "Chọn K.Phòng:";
            // 
            // Frm_repTonghopXNKhac
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 312);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.cboTT);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.ButHuy);
            this.Controls.Add(this.butTaoBC);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.LupNgayden);
            this.Controls.Add(this.LupNgaytu);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_repTonghopXNKhac";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tổng hợp hóa sinh máu";
            this.Load += new System.EventHandler(this.Frm_repTonghopHoaSinh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LupNgaytu.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgaytu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayden.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayden.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcKhoaphong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhoaphong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit LupNgaytu;
        private DevExpress.XtraEditors.DateEdit LupNgayden;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton butTaoBC;
        private DevExpress.XtraEditors.SimpleButton ButHuy;
        private DevExpress.XtraEditors.ComboBoxEdit cboTT;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl grcKhoaphong;
        private DevExpress.XtraGrid.Views.Grid.GridView grvKhoaphong;
        private DevExpress.XtraGrid.Columns.GridColumn Chọn;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn TenKP;
        private DevExpress.XtraGrid.Columns.GridColumn MaKP;
        private DevExpress.XtraEditors.LabelControl labelControl4;
    }
}