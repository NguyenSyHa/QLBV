namespace QLBV.FormThamSo
{
    partial class frm_BC_SolanThucHienPTTT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_BC_SolanThucHienPTTT));
            this.label1 = new System.Windows.Forms.Label();
            this.date_tungay = new DevExpress.XtraEditors.DateEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.date_ngayden = new DevExpress.XtraEditors.DateEdit();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.grvkhoaphong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colChon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tenkp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.makp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grckhoaphong = new DevExpress.XtraGrid.GridControl();
            ((System.ComponentModel.ISupportInitialize)(this.date_tungay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_tungay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_ngayden.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_ngayden.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvkhoaphong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grckhoaphong)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 19);
            this.label1.TabIndex = 11;
            this.label1.Text = "Từ ngày:";
            // 
            // date_tungay
            // 
            this.date_tungay.EditValue = null;
            this.date_tungay.Location = new System.Drawing.Point(80, 6);
            this.date_tungay.Name = "date_tungay";
            this.date_tungay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.date_tungay.Properties.Appearance.Options.UseFont = true;
            this.date_tungay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.date_tungay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.date_tungay.Size = new System.Drawing.Size(137, 26);
            this.date_tungay.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(236, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 19);
            this.label2.TabIndex = 14;
            this.label2.Text = "Đến ngày:";
            // 
            // date_ngayden
            // 
            this.date_ngayden.EditValue = null;
            this.date_ngayden.Location = new System.Drawing.Point(304, 6);
            this.date_ngayden.Name = "date_ngayden";
            this.date_ngayden.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.date_ngayden.Properties.Appearance.Options.UseFont = true;
            this.date_ngayden.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.date_ngayden.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.date_ngayden.Size = new System.Drawing.Size(137, 26);
            this.date_ngayden.TabIndex = 13;
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.Image = ((System.Drawing.Image)(resources.GetObject("btnThoat.Image")));
            this.btnThoat.Location = new System.Drawing.Point(376, 263);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(65, 25);
            this.btnThoat.TabIndex = 16;
            this.btnThoat.Text = "&Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(245, 263);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 25);
            this.btnOK.TabIndex = 15;
            this.btnOK.Text = "Tạo báo &cáo";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 19);
            this.label3.TabIndex = 17;
            this.label3.Text = "Khoa:";
            // 
            // grvkhoaphong
            // 
            this.grvkhoaphong.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colChon,
            this.tenkp,
            this.makp});
            this.grvkhoaphong.GridControl = this.grckhoaphong;
            this.grvkhoaphong.Name = "grvkhoaphong";
            this.grvkhoaphong.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.makp, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.grvkhoaphong.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvKho_CellValueChanging);
            // 
            // colChon
            // 
            this.colChon.Caption = "Chọn";
            this.colChon.FieldName = "Check";
            this.colChon.Name = "colChon";
            this.colChon.Visible = true;
            this.colChon.VisibleIndex = 0;
            this.colChon.Width = 41;
            // 
            // tenkp
            // 
            this.tenkp.Caption = "Tên khoa phòng";
            this.tenkp.FieldName = "TenKP";
            this.tenkp.Name = "tenkp";
            this.tenkp.Visible = true;
            this.tenkp.VisibleIndex = 1;
            this.tenkp.Width = 150;
            // 
            // makp
            // 
            this.makp.Caption = "Mã";
            this.makp.FieldName = "MaKP";
            this.makp.Name = "makp";
            this.makp.Visible = true;
            this.makp.VisibleIndex = 2;
            this.makp.Width = 25;
            // 
            // grckhoaphong
            // 
            this.grckhoaphong.Location = new System.Drawing.Point(80, 43);
            this.grckhoaphong.MainView = this.grvkhoaphong;
            this.grckhoaphong.Name = "grckhoaphong";
            this.grckhoaphong.Size = new System.Drawing.Size(361, 201);
            this.grckhoaphong.TabIndex = 18;
            this.grckhoaphong.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvkhoaphong});
            // 
            // frm_BC_SolanThucHienPTTT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 304);
            this.Controls.Add(this.grckhoaphong);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.date_ngayden);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.date_tungay);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_BC_SolanThucHienPTTT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo số lần thực hiện PTTT";
            this.Load += new System.EventHandler(this.frm_BC_SolanThucHienPTTT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.date_tungay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_tungay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_ngayden.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_ngayden.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvkhoaphong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grckhoaphong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.DateEdit date_tungay;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.DateEdit date_ngayden;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraGrid.Views.Grid.GridView grvkhoaphong;
        public DevExpress.XtraGrid.Columns.GridColumn colChon;
        public DevExpress.XtraGrid.Columns.GridColumn tenkp;
        private DevExpress.XtraGrid.Columns.GridColumn makp;
        private DevExpress.XtraGrid.GridControl grckhoaphong;

    }
}