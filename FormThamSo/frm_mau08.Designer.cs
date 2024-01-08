namespace QLBV.FormThamSo
{
    partial class frm_mau08
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.checkKDT = new DevExpress.XtraEditors.CheckEdit();
            this.checkPK = new DevExpress.XtraEditors.CheckEdit();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.btnIn = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.grcThuocSD = new DevExpress.XtraGrid.GridControl();
            this.grvThuocSD = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTenDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lup_MaDV = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.Chon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTen = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkKDT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkPK.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcThuocSD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvThuocSD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_MaDV)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.checkKDT);
            this.panelControl1.Controls.Add(this.checkPK);
            this.panelControl1.Controls.Add(this.btnThoat);
            this.panelControl1.Controls.Add(this.btnIn);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 395);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(438, 57);
            this.panelControl1.TabIndex = 0;
            // 
            // checkKDT
            // 
            this.checkKDT.EditValue = true;
            this.checkKDT.Location = new System.Drawing.Point(5, 31);
            this.checkKDT.Name = "checkKDT";
            this.checkKDT.Properties.Caption = "Khoa phòng điều trị";
            this.checkKDT.Size = new System.Drawing.Size(126, 19);
            this.checkKDT.TabIndex = 3;
            this.checkKDT.Visible = false;
            this.checkKDT.CheckedChanged += new System.EventHandler(this.checkKDT_CheckedChanged);
            // 
            // checkPK
            // 
            this.checkPK.EditValue = true;
            this.checkPK.Location = new System.Drawing.Point(5, 6);
            this.checkPK.Name = "checkPK";
            this.checkPK.Properties.Caption = "Phòng khám";
            this.checkPK.Size = new System.Drawing.Size(126, 19);
            this.checkPK.TabIndex = 2;
            this.checkPK.Visible = false;
            this.checkPK.CheckedChanged += new System.EventHandler(this.checkPK_CheckedChanged);
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.Location = new System.Drawing.Point(351, 16);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(75, 29);
            this.btnThoat.TabIndex = 1;
            this.btnThoat.Text = "&Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnIn
            // 
            this.btnIn.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnIn.Appearance.Options.UseFont = true;
            this.btnIn.Location = new System.Drawing.Point(270, 16);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(75, 29);
            this.btnIn.TabIndex = 0;
            this.btnIn.Text = "&In phiếu";
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.grcThuocSD);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(438, 395);
            this.panelControl2.TabIndex = 1;
            // 
            // grcThuocSD
            // 
            this.grcThuocSD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcThuocSD.Location = new System.Drawing.Point(2, 2);
            this.grcThuocSD.MainView = this.grvThuocSD;
            this.grcThuocSD.Name = "grcThuocSD";
            this.grcThuocSD.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lup_MaDV});
            this.grcThuocSD.Size = new System.Drawing.Size(434, 391);
            this.grcThuocSD.TabIndex = 1;
            this.grcThuocSD.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvThuocSD});
            // 
            // grvThuocSD
            // 
            this.grvThuocSD.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTenDV,
            this.Chon,
            this.colTen});
            this.grvThuocSD.GridControl = this.grcThuocSD;
            this.grvThuocSD.Name = "grvThuocSD";
            this.grvThuocSD.OptionsView.EnableAppearanceEvenRow = true;
            this.grvThuocSD.OptionsView.EnableAppearanceOddRow = true;
            this.grvThuocSD.OptionsView.ShowGroupPanel = false;
            this.grvThuocSD.OptionsView.ShowViewCaption = true;
            // 
            // colTenDV
            // 
            this.colTenDV.AppearanceCell.Options.UseTextOptions = true;
            this.colTenDV.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.colTenDV.AppearanceHeader.Options.UseFont = true;
            this.colTenDV.AppearanceHeader.Options.UseTextOptions = true;
            this.colTenDV.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTenDV.Caption = "Tên dịch vụ";
            this.colTenDV.ColumnEdit = this.lup_MaDV;
            this.colTenDV.FieldName = "MaDV";
            this.colTenDV.Name = "colTenDV";
            this.colTenDV.OptionsColumn.AllowEdit = false;
            this.colTenDV.OptionsColumn.AllowFocus = false;
            this.colTenDV.OptionsColumn.ReadOnly = true;
            this.colTenDV.Width = 240;
            // 
            // lup_MaDV
            // 
            this.lup_MaDV.AllowFocused = false;
            this.lup_MaDV.AutoHeight = false;
            this.lup_MaDV.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_MaDV.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenDV", "Tên DV")});
            this.lup_MaDV.DisplayMember = "TenDV";
            this.lup_MaDV.Name = "lup_MaDV";
            this.lup_MaDV.NullText = "";
            this.lup_MaDV.ReadOnly = true;
            this.lup_MaDV.ValueMember = "MaDV";
            // 
            // Chon
            // 
            this.Chon.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.Chon.AppearanceHeader.Options.UseFont = true;
            this.Chon.AppearanceHeader.Options.UseTextOptions = true;
            this.Chon.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Chon.Caption = "Chọn";
            this.Chon.FieldName = "Chon";
            this.Chon.Name = "Chon";
            this.Chon.Visible = true;
            this.Chon.VisibleIndex = 1;
            this.Chon.Width = 93;
            // 
            // colTen
            // 
            this.colTen.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.colTen.AppearanceCell.Options.UseFont = true;
            this.colTen.AppearanceCell.Options.UseTextOptions = true;
            this.colTen.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colTen.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.colTen.AppearanceHeader.Options.UseFont = true;
            this.colTen.AppearanceHeader.Options.UseTextOptions = true;
            this.colTen.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTen.Caption = "Tên dịch vụ";
            this.colTen.FieldName = "TenDV";
            this.colTen.Name = "colTen";
            this.colTen.Visible = true;
            this.colTen.VisibleIndex = 0;
            this.colTen.Width = 323;
            // 
            // frm_mau08
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 452);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_mau08";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phiếu khám chữa bệnh(MS 08)";
            this.Load += new System.EventHandler(this.frm_mau08_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkKDT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkPK.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcThuocSD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvThuocSD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lup_MaDV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.SimpleButton btnIn;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl grcThuocSD;
        private DevExpress.XtraGrid.Views.Grid.GridView grvThuocSD;
        private DevExpress.XtraGrid.Columns.GridColumn colTenDV;
        private DevExpress.XtraGrid.Columns.GridColumn Chon;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lup_MaDV;
        private DevExpress.XtraGrid.Columns.GridColumn colTen;
        private DevExpress.XtraEditors.CheckEdit checkKDT;
        private DevExpress.XtraEditors.CheckEdit checkPK;
    }
}