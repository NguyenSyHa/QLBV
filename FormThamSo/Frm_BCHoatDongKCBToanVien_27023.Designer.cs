namespace QLBV.FormThamSo
{
    partial class Frm_BCHoatDongKCBToanVien_27023
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_BCHoatDongKCBToanVien_27023));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.Tha = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnInBC = new DevExpress.XtraEditors.SimpleButton();
            this.popKP = new DevExpress.XtraEditors.PopupContainerEdit();
            this.popupContainerControl1 = new DevExpress.XtraEditors.PopupContainerControl();
            this.grcKP = new DevExpress.XtraGrid.GridControl();
            this.grvKP = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTenKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.popdsKP = new DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit();
            this.lupngayden = new DevExpress.XtraEditors.DateEdit();
            this.lupNgaytu = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.popKP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).BeginInit();
            this.popupContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcKP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popdsKP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngayden.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngayden.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Location = new System.Drawing.Point(190, 14);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(66, 17);
            this.labelControl1.TabIndex = 64;
            this.labelControl1.Text = "Đến ngày:";
            // 
            // Tha
            // 
            this.Tha.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold);
            this.Tha.Location = new System.Drawing.Point(12, 14);
            this.Tha.Name = "Tha";
            this.Tha.Size = new System.Drawing.Size(58, 17);
            this.Tha.TabIndex = 63;
            this.Tha.Text = "Từ ngày:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Location = new System.Drawing.Point(12, 62);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(120, 17);
            this.labelControl2.TabIndex = 63;
            this.labelControl2.Text = "Chọn khoa phòng:";
            // 
            // btnHuy
            // 
            this.btnHuy.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnHuy.Appearance.Options.UseFont = true;
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Image = ((System.Drawing.Image)(resources.GetObject("btnHuy.Image")));
            this.btnHuy.Location = new System.Drawing.Point(226, 96);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(83, 23);
            this.btnHuy.TabIndex = 62;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnInBC
            // 
            this.btnInBC.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnInBC.Appearance.Options.UseFont = true;
            this.btnInBC.Image = ((System.Drawing.Image)(resources.GetObject("btnInBC.Image")));
            this.btnInBC.Location = new System.Drawing.Point(76, 96);
            this.btnInBC.Name = "btnInBC";
            this.btnInBC.Size = new System.Drawing.Size(88, 23);
            this.btnInBC.TabIndex = 61;
            this.btnInBC.Text = "&Tạo ";
            this.btnInBC.Click += new System.EventHandler(this.btnInBC_Click);
            // 
            // popKP
            // 
            this.popKP.EditValue = "Chọn Khoa | Phòng";
            this.popKP.Location = new System.Drawing.Point(156, 62);
            this.popKP.Name = "popKP";
            this.popKP.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.popKP.Properties.Appearance.Options.UseFont = true;
            this.popKP.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.popKP.Properties.PopupControl = this.popupContainerControl1;
            this.popKP.Size = new System.Drawing.Size(178, 24);
            this.popKP.TabIndex = 531;
            // 
            // popupContainerControl1
            // 
            this.popupContainerControl1.Controls.Add(this.grcKP);
            this.popupContainerControl1.Location = new System.Drawing.Point(340, -3);
            this.popupContainerControl1.Name = "popupContainerControl1";
            this.popupContainerControl1.Size = new System.Drawing.Size(336, 197);
            this.popupContainerControl1.TabIndex = 534;
            // 
            // grcKP
            // 
            this.grcKP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcKP.Location = new System.Drawing.Point(0, 0);
            this.grcKP.MainView = this.grvKP;
            this.grcKP.Name = "grcKP";
            this.grcKP.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.popdsKP});
            this.grcKP.Size = new System.Drawing.Size(336, 197);
            this.grcKP.TabIndex = 533;
            this.grcKP.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvKP});
            // 
            // grvKP
            // 
            this.grvKP.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTenKP,
            this.colMaKP});
            this.grvKP.GridControl = this.grcKP;
            this.grvKP.Name = "grvKP";
            this.grvKP.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.grvKP.OptionsBehavior.Editable = false;
            this.grvKP.OptionsBehavior.ReadOnly = true;
            this.grvKP.OptionsSelection.MultiSelect = true;
            this.grvKP.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.grvKP.OptionsView.ShowGroupPanel = false;
            // 
            // colTenKP
            // 
            this.colTenKP.Caption = "Tên khoa phòng";
            this.colTenKP.FieldName = "TenKP";
            this.colTenKP.Name = "colTenKP";
            this.colTenKP.Visible = true;
            this.colTenKP.VisibleIndex = 1;
            // 
            // colMaKP
            // 
            this.colMaKP.Caption = "gridColumn1";
            this.colMaKP.FieldName = "MaKP";
            this.colMaKP.Name = "colMaKP";
            // 
            // popdsKP
            // 
            this.popdsKP.AutoHeight = false;
            this.popdsKP.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.popdsKP.Name = "popdsKP";
            // 
            // lupngayden
            // 
            this.lupngayden.EditValue = new System.DateTime(2022, 10, 20, 12, 0, 0, 0);
            this.lupngayden.EnterMoveNextControl = true;
            this.lupngayden.Location = new System.Drawing.Point(262, 10);
            this.lupngayden.Name = "lupngayden";
            this.lupngayden.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupngayden.Properties.Appearance.Options.UseFont = true;
            this.lupngayden.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupngayden.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupngayden.Size = new System.Drawing.Size(94, 26);
            this.lupngayden.TabIndex = 536;
            // 
            // lupNgaytu
            // 
            this.lupNgaytu.EditValue = new System.DateTime(2022, 10, 20, 0, 0, 0, 0);
            this.lupNgaytu.EnterMoveNextControl = true;
            this.lupNgaytu.Location = new System.Drawing.Point(76, 10);
            this.lupNgaytu.Name = "lupNgaytu";
            this.lupNgaytu.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupNgaytu.Properties.Appearance.Options.UseFont = true;
            this.lupNgaytu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgaytu.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupNgaytu.Size = new System.Drawing.Size(94, 26);
            this.lupNgaytu.TabIndex = 535;
            // 
            // Frm_BCHoatDongKCBToanVien_27023
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 131);
            this.Controls.Add(this.lupngayden);
            this.Controls.Add(this.lupNgaytu);
            this.Controls.Add(this.popupContainerControl1);
            this.Controls.Add(this.popKP);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnInBC);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.Tha);
            this.Name = "Frm_BCHoatDongKCBToanVien_27023";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tổng hợp KCB toàn viện 27023";
            this.Load += new System.EventHandler(this.Frm_BCHoatDongKCBToanVien_27023_Load);
            ((System.ComponentModel.ISupportInitialize)(this.popKP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).EndInit();
            this.popupContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcKP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popdsKP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngayden.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupngayden.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupNgaytu.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnInBC;
        private DevExpress.XtraEditors.LabelControl Tha;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PopupContainerEdit popKP;
        private DevExpress.XtraGrid.GridControl grcKP;
        private DevExpress.XtraGrid.Views.Grid.GridView grvKP;
        private DevExpress.XtraGrid.Columns.GridColumn colTenKP;
        private DevExpress.XtraGrid.Columns.GridColumn colMaKP;
        private DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit popdsKP;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerControl1;
        private DevExpress.XtraEditors.DateEdit lupngayden;
        private DevExpress.XtraEditors.DateEdit lupNgaytu;
    }
}