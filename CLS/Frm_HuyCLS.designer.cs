namespace QLBV.FormThamSo
{
    partial class Frm_HuyCLS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_HuyCLS));
            this.grcCLS = new DevExpress.XtraGrid.GridControl();
            this.grvCLS = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Huy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLydo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SbtLuu = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraDichVu = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.xtraDichVuct = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.grcChiTiet = new DevExpress.XtraGrid.GridControl();
            this.grvChiTiet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenDVct = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHuyct = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grcCLS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCLS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraDichVu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.xtraDichVuct.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcChiTiet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvChiTiet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // grcCLS
            // 
            this.grcCLS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcCLS.Location = new System.Drawing.Point(0, 0);
            this.grcCLS.MainView = this.grvCLS;
            this.grcCLS.Name = "grcCLS";
            this.grcCLS.Size = new System.Drawing.Size(490, 374);
            this.grcCLS.TabIndex = 0;
            this.grcCLS.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvCLS});
            // 
            // grvCLS
            // 
            this.grvCLS.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaDV,
            this.colTenDV,
            this.Huy,
            this.colLydo});
            this.grvCLS.GridControl = this.grcCLS;
            this.grvCLS.Name = "grvCLS";
            this.grvCLS.OptionsView.ShowGroupPanel = false;
            // 
            // colMaDV
            // 
            this.colMaDV.Caption = "MaDv";
            this.colMaDV.FieldName = "MaDV";
            this.colMaDV.Name = "colMaDV";
            // 
            // colTenDV
            // 
            this.colTenDV.AppearanceCell.Options.UseTextOptions = true;
            this.colTenDV.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.colTenDV.AppearanceHeader.Options.UseFont = true;
            this.colTenDV.AppearanceHeader.Options.UseTextOptions = true;
            this.colTenDV.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTenDV.Caption = "Tên dịch vụ";
            this.colTenDV.FieldName = "TenDV";
            this.colTenDV.Name = "colTenDV";
            this.colTenDV.OptionsColumn.AllowEdit = false;
            this.colTenDV.OptionsColumn.AllowFocus = false;
            this.colTenDV.Visible = true;
            this.colTenDV.VisibleIndex = 0;
            this.colTenDV.Width = 240;
            // 
            // Huy
            // 
            this.Huy.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Huy.AppearanceHeader.Options.UseFont = true;
            this.Huy.AppearanceHeader.Options.UseTextOptions = true;
            this.Huy.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Huy.Caption = "Huỷ";
            this.Huy.FieldName = "Huy";
            this.Huy.Name = "Huy";
            this.Huy.Visible = true;
            this.Huy.VisibleIndex = 1;
            this.Huy.Width = 45;
            // 
            // colLydo
            // 
            this.colLydo.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.colLydo.AppearanceHeader.Options.UseFont = true;
            this.colLydo.AppearanceHeader.Options.UseTextOptions = true;
            this.colLydo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLydo.Caption = "Lý do huỷ";
            this.colLydo.FieldName = "LyDo";
            this.colLydo.Name = "colLydo";
            this.colLydo.Visible = true;
            this.colLydo.VisibleIndex = 2;
            this.colLydo.Width = 160;
            // 
            // SbtLuu
            // 
            this.SbtLuu.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.SbtLuu.Appearance.Options.UseFont = true;
            this.SbtLuu.Image = ((System.Drawing.Image)(resources.GetObject("SbtLuu.Image")));
            this.SbtLuu.Location = new System.Drawing.Point(304, 6);
            this.SbtLuu.Name = "SbtLuu";
            this.SbtLuu.Size = new System.Drawing.Size(87, 31);
            this.SbtLuu.TabIndex = 1;
            this.SbtLuu.Text = "Lưu";
            this.SbtLuu.Click += new System.EventHandler(this.SbtLuu_Click);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraDichVu;
            this.xtraTabControl1.Size = new System.Drawing.Size(496, 444);
            this.xtraTabControl1.TabIndex = 3;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraDichVu,
            this.xtraDichVuct});
            // 
            // xtraDichVu
            // 
            this.xtraDichVu.Controls.Add(this.panelControl2);
            this.xtraDichVu.Controls.Add(this.panelControl1);
            this.xtraDichVu.Name = "xtraDichVu";
            this.xtraDichVu.Size = new System.Drawing.Size(490, 416);
            this.xtraDichVu.Text = "Dịch vụ";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.grcCLS);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(490, 374);
            this.panelControl2.TabIndex = 4;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnThoat);
            this.panelControl1.Controls.Add(this.SbtLuu);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 374);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(490, 42);
            this.panelControl1.TabIndex = 3;
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.Image = ((System.Drawing.Image)(resources.GetObject("btnThoat.Image")));
            this.btnThoat.Location = new System.Drawing.Point(396, 6);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(87, 31);
            this.btnThoat.TabIndex = 2;
            this.btnThoat.Text = "Thoát";
            // 
            // xtraDichVuct
            // 
            this.xtraDichVuct.Controls.Add(this.panelControl4);
            this.xtraDichVuct.Controls.Add(this.panelControl3);
            this.xtraDichVuct.Name = "xtraDichVuct";
            this.xtraDichVuct.Size = new System.Drawing.Size(490, 416);
            this.xtraDichVuct.Text = "Chi tiết dịch vụ";
            // 
            // panelControl4
            // 
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Controls.Add(this.grcChiTiet);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl4.Location = new System.Drawing.Point(0, 0);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(490, 374);
            this.panelControl4.TabIndex = 5;
            // 
            // grcChiTiet
            // 
            this.grcChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcChiTiet.Location = new System.Drawing.Point(0, 0);
            this.grcChiTiet.MainView = this.grvChiTiet;
            this.grcChiTiet.Name = "grcChiTiet";
            this.grcChiTiet.Size = new System.Drawing.Size(490, 374);
            this.grcChiTiet.TabIndex = 0;
            this.grcChiTiet.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvChiTiet});
            // 
            // grvChiTiet
            // 
            this.grvChiTiet.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.colTenDVct,
            this.colHuyct,
            this.gridColumn4});
            this.grvChiTiet.GridControl = this.grcChiTiet;
            this.grvChiTiet.Name = "grvChiTiet";
            this.grvChiTiet.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "MaDv";
            this.gridColumn1.FieldName = "MaDV";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // colTenDVct
            // 
            this.colTenDVct.AppearanceCell.Options.UseTextOptions = true;
            this.colTenDVct.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.colTenDVct.AppearanceHeader.Options.UseFont = true;
            this.colTenDVct.AppearanceHeader.Options.UseTextOptions = true;
            this.colTenDVct.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTenDVct.Caption = "Tên xét nghiệm";
            this.colTenDVct.FieldName = "TenDV";
            this.colTenDVct.Name = "colTenDVct";
            this.colTenDVct.OptionsColumn.AllowEdit = false;
            this.colTenDVct.OptionsColumn.AllowFocus = false;
            this.colTenDVct.Visible = true;
            this.colTenDVct.VisibleIndex = 0;
            this.colTenDVct.Width = 240;
            // 
            // colHuyct
            // 
            this.colHuyct.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.colHuyct.AppearanceHeader.Options.UseFont = true;
            this.colHuyct.AppearanceHeader.Options.UseTextOptions = true;
            this.colHuyct.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHuyct.Caption = "Hủy";
            this.colHuyct.FieldName = "Huy";
            this.colHuyct.Name = "colHuyct";
            this.colHuyct.Visible = true;
            this.colHuyct.VisibleIndex = 1;
            this.colHuyct.Width = 45;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.gridColumn4.AppearanceHeader.Options.UseFont = true;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "Lý do huỷ";
            this.gridColumn4.FieldName = "LyDo";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 160;
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.simpleButton1);
            this.panelControl3.Controls.Add(this.simpleButton2);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(0, 374);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(490, 42);
            this.panelControl3.TabIndex = 4;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(304, 6);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(87, 31);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "Lưu";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.Image")));
            this.simpleButton2.Location = new System.Drawing.Point(397, 6);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(86, 31);
            this.simpleButton2.TabIndex = 2;
            this.simpleButton2.Text = "Thoát";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // Frm_HuyCLS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 444);
            this.Controls.Add(this.xtraTabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_HuyCLS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Huỷ thực hiện CLS";
            this.Load += new System.EventHandler(this.Frm_HuyCLS_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grcCLS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCLS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraDichVu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.xtraDichVuct.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcChiTiet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvChiTiet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grcCLS;
        private DevExpress.XtraGrid.Views.Grid.GridView grvCLS;
        private DevExpress.XtraGrid.Columns.GridColumn colMaDV;
        private DevExpress.XtraGrid.Columns.GridColumn colTenDV;
        private DevExpress.XtraGrid.Columns.GridColumn Huy;
        private DevExpress.XtraGrid.Columns.GridColumn colLydo;
        private DevExpress.XtraEditors.SimpleButton SbtLuu;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraDichVu;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraTab.XtraTabPage xtraDichVuct;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraGrid.GridControl grcChiTiet;
        private DevExpress.XtraGrid.Views.Grid.GridView grvChiTiet;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn colTenDVct;
        private DevExpress.XtraGrid.Columns.GridColumn colHuyct;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.SimpleButton btnThoat;

    }
}