namespace QLBV.FormThamSo
{
    partial class Frm_BCSKSCanbo
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
            this.sbtTaoBC = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.Ngaytu = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.Ngayden = new DevExpress.XtraEditors.DateEdit();
            this.sbtHuy = new DevExpress.XtraEditors.SimpleButton();
            this.lupDTuong = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.grcCapBac = new DevExpress.XtraGrid.GridControl();
            this.grvCapBac = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCapBac = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkChon = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Ngaytu.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ngaytu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ngayden.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ngayden.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDTuong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcCapBac)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCapBac)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkChon)).BeginInit();
            this.SuspendLayout();
            // 
            // sbtTaoBC
            // 
            this.sbtTaoBC.Location = new System.Drawing.Point(70, 227);
            this.sbtTaoBC.Name = "sbtTaoBC";
            this.sbtTaoBC.Size = new System.Drawing.Size(75, 23);
            this.sbtTaoBC.TabIndex = 0;
            this.sbtTaoBC.Text = "Tạo báo cáo";
            this.sbtTaoBC.Click += new System.EventHandler(this.sbtTaoBC_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 27);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(44, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // Ngaytu
            // 
            this.Ngaytu.EditValue = null;
            this.Ngaytu.Location = new System.Drawing.Point(70, 24);
            this.Ngaytu.Name = "Ngaytu";
            this.Ngaytu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Ngaytu.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Ngaytu.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.Ngaytu.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.Ngaytu.Size = new System.Drawing.Size(100, 20);
            this.Ngaytu.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(176, 27);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(49, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "đến ngày:";
            // 
            // Ngayden
            // 
            this.Ngayden.EditValue = null;
            this.Ngayden.Location = new System.Drawing.Point(231, 24);
            this.Ngayden.Name = "Ngayden";
            this.Ngayden.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Ngayden.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Ngayden.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.Ngayden.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.Ngayden.Size = new System.Drawing.Size(100, 20);
            this.Ngayden.TabIndex = 4;
            // 
            // sbtHuy
            // 
            this.sbtHuy.Location = new System.Drawing.Point(231, 227);
            this.sbtHuy.Name = "sbtHuy";
            this.sbtHuy.Size = new System.Drawing.Size(75, 23);
            this.sbtHuy.TabIndex = 5;
            this.sbtHuy.Text = "Huỷ";
            this.sbtHuy.Click += new System.EventHandler(this.sbtHuy_Click);
            // 
            // lupDTuong
            // 
            this.lupDTuong.Location = new System.Drawing.Point(70, 50);
            this.lupDTuong.Name = "lupDTuong";
            this.lupDTuong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupDTuong.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DTBN1", 200, "Đối tượng"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("IDDTBN", "ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lupDTuong.Properties.DisplayMember = "DTBN1";
            this.lupDTuong.Properties.NullText = "";
            this.lupDTuong.Properties.ValueMember = "IDDTBN";
            this.lupDTuong.Size = new System.Drawing.Size(261, 20);
            this.lupDTuong.TabIndex = 6;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 53);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(52, 13);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "Đối tượng:";
            // 
            // grcCapBac
            // 
            this.grcCapBac.Location = new System.Drawing.Point(70, 76);
            this.grcCapBac.MainView = this.grvCapBac;
            this.grcCapBac.Name = "grcCapBac";
            this.grcCapBac.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.chkChon});
            this.grcCapBac.Size = new System.Drawing.Size(261, 133);
            this.grcCapBac.TabIndex = 8;
            this.grcCapBac.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvCapBac});
            // 
            // grvCapBac
            // 
            this.grvCapBac.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCapBac,
            this.colChon,
            this.colID});
            this.grvCapBac.GridControl = this.grcCapBac;
            this.grvCapBac.Name = "grvCapBac";
            this.grvCapBac.OptionsView.ShowGroupPanel = false;
            this.grvCapBac.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvCapBac_CellValueChanging);
            // 
            // colCapBac
            // 
            this.colCapBac.Caption = "Cấp bậc";
            this.colCapBac.FieldName = "_CapBac";
            this.colCapBac.Name = "colCapBac";
            this.colCapBac.Visible = true;
            this.colCapBac.VisibleIndex = 0;
            this.colCapBac.Width = 162;
            // 
            // colChon
            // 
            this.colChon.Caption = "Chọn";
            this.colChon.ColumnEdit = this.chkChon;
            this.colChon.FieldName = "_Chon";
            this.colChon.Name = "colChon";
            this.colChon.Visible = true;
            this.colChon.VisibleIndex = 1;
            this.colChon.Width = 81;
            // 
            // chkChon
            // 
            this.chkChon.AutoHeight = false;
            this.chkChon.Caption = "Check";
            this.chkChon.Name = "chkChon";
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "_ID";
            this.colID.Name = "colID";
            // 
            // Frm_BCSKSCanbo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 262);
            this.Controls.Add(this.grcCapBac);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.lupDTuong);
            this.Controls.Add(this.sbtHuy);
            this.Controls.Add(this.Ngayden);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.Ngaytu);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.sbtTaoBC);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_BCSKSCanbo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo tình hình sức khoẻ cán bộ cao cấp";
            this.Load += new System.EventHandler(this.Frm_BCSKSCanbo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Ngaytu.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ngaytu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ngayden.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ngayden.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupDTuong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcCapBac)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCapBac)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkChon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton sbtTaoBC;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit Ngaytu;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit Ngayden;
        private DevExpress.XtraEditors.SimpleButton sbtHuy;
        private DevExpress.XtraEditors.LookUpEdit lupDTuong;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraGrid.GridControl grcCapBac;
        private DevExpress.XtraGrid.Views.Grid.GridView grvCapBac;
        private DevExpress.XtraGrid.Columns.GridColumn colCapBac;
        private DevExpress.XtraGrid.Columns.GridColumn colChon;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkChon;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
    }
}