namespace QLBV.FormThamSo
{
    partial class Frm_KTNB
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lupNgayVao = new DevExpress.XtraEditors.DateEdit();
            this.LupNgayRa = new DevExpress.XtraEditors.DateEdit();
            this.sbtKiemtra = new DevExpress.XtraEditors.SimpleButton();
            this.GrcDSBN = new DevExpress.XtraGrid.GridControl();
            this.GrvDSBN = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaBNhan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenBN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSothe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDiaChi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayNhap = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgayVao.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgayVao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayRa.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayRa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrcDSBN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrvDSBN)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(379, 19);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Chức năng này lọc ra những bệnh nhân đi khám ngoại trú";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl2.Location = new System.Drawing.Point(12, 37);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(320, 19);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = " trong thời gian chưa kết thúc đợt điều trị nội trú";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 84);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(44, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Từ ngày:";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 110);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(51, 13);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "Đến ngày:";
            // 
            // lupNgayVao
            // 
            this.lupNgayVao.EditValue = null;
            this.lupNgayVao.Location = new System.Drawing.Point(69, 81);
            this.lupNgayVao.Name = "lupNgayVao";
            this.lupNgayVao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgayVao.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgayVao.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.lupNgayVao.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.lupNgayVao.Size = new System.Drawing.Size(100, 20);
            this.lupNgayVao.TabIndex = 4;
            // 
            // LupNgayRa
            // 
            this.LupNgayRa.EditValue = null;
            this.LupNgayRa.Location = new System.Drawing.Point(69, 107);
            this.LupNgayRa.Name = "LupNgayRa";
            this.LupNgayRa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgayRa.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupNgayRa.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.LupNgayRa.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.LupNgayRa.Size = new System.Drawing.Size(100, 20);
            this.LupNgayRa.TabIndex = 5;
            // 
            // sbtKiemtra
            // 
            this.sbtKiemtra.Location = new System.Drawing.Point(190, 84);
            this.sbtKiemtra.Name = "sbtKiemtra";
            this.sbtKiemtra.Size = new System.Drawing.Size(90, 39);
            this.sbtKiemtra.TabIndex = 6;
            this.sbtKiemtra.Text = "Kiểm tra";
            this.sbtKiemtra.Click += new System.EventHandler(this.sbtKiemtra_Click);
            // 
            // GrcDSBN
            // 
            this.GrcDSBN.Location = new System.Drawing.Point(12, 143);
            this.GrcDSBN.MainView = this.GrvDSBN;
            this.GrcDSBN.Name = "GrcDSBN";
            this.GrcDSBN.Size = new System.Drawing.Size(578, 215);
            this.GrcDSBN.TabIndex = 7;
            this.GrcDSBN.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrvDSBN});
            // 
            // GrvDSBN
            // 
            this.GrvDSBN.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaBNhan,
            this.colTenBN,
            this.colSothe,
            this.colDiaChi,
            this.colNgayNhap});
            this.GrvDSBN.GridControl = this.GrcDSBN;
            this.GrvDSBN.Name = "GrvDSBN";
            this.GrvDSBN.OptionsView.ShowFooter = true;
            this.GrvDSBN.OptionsView.ShowGroupPanel = false;
            // 
            // colMaBNhan
            // 
            this.colMaBNhan.Caption = "Mã bệnh nhân";
            this.colMaBNhan.FieldName = "MaBNhan";
            this.colMaBNhan.Name = "colMaBNhan";
            this.colMaBNhan.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.colMaBNhan.Visible = true;
            this.colMaBNhan.VisibleIndex = 0;
            this.colMaBNhan.Width = 76;
            // 
            // colTenBN
            // 
            this.colTenBN.Caption = "Tên bệnh nhân";
            this.colTenBN.FieldName = "TenBNhan";
            this.colTenBN.Name = "colTenBN";
            this.colTenBN.Visible = true;
            this.colTenBN.VisibleIndex = 1;
            this.colTenBN.Width = 240;
            // 
            // colSothe
            // 
            this.colSothe.Caption = "Số thẻ";
            this.colSothe.FieldName = "SThe";
            this.colSothe.Name = "colSothe";
            this.colSothe.Visible = true;
            this.colSothe.VisibleIndex = 2;
            this.colSothe.Width = 158;
            // 
            // colDiaChi
            // 
            this.colDiaChi.Caption = "Địa chỉ";
            this.colDiaChi.FieldName = "DChi";
            this.colDiaChi.Name = "colDiaChi";
            this.colDiaChi.Visible = true;
            this.colDiaChi.VisibleIndex = 3;
            this.colDiaChi.Width = 445;
            // 
            // colNgayNhap
            // 
            this.colNgayNhap.Caption = "Ngày nhập";
            this.colNgayNhap.FieldName = "NNhap";
            this.colNgayNhap.Name = "colNgayNhap";
            this.colNgayNhap.Visible = true;
            this.colNgayNhap.VisibleIndex = 4;
            this.colNgayNhap.Width = 125;
            // 
            // Frm_KTNB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 431);
            this.Controls.Add(this.GrcDSBN);
            this.Controls.Add(this.sbtKiemtra);
            this.Controls.Add(this.LupNgayRa);
            this.Controls.Add(this.lupNgayVao);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_KTNB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kiểm tra người bệnh";
            this.Load += new System.EventHandler(this.Frm_KTNB_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupNgayVao.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgayVao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayRa.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LupNgayRa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrcDSBN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrvDSBN)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.DateEdit lupNgayVao;
        private DevExpress.XtraEditors.DateEdit LupNgayRa;
        private DevExpress.XtraEditors.SimpleButton sbtKiemtra;
        private DevExpress.XtraGrid.GridControl GrcDSBN;
        private DevExpress.XtraGrid.Views.Grid.GridView GrvDSBN;
        private DevExpress.XtraGrid.Columns.GridColumn colMaBNhan;
        private DevExpress.XtraGrid.Columns.GridColumn colTenBN;
        private DevExpress.XtraGrid.Columns.GridColumn colSothe;
        private DevExpress.XtraGrid.Columns.GridColumn colDiaChi;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayNhap;
    }
}