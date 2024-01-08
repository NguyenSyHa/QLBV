namespace QLBV.FormThamSo
{
    partial class Frm_PhieuDieuTri
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
            this.sbtTBC = new DevExpress.XtraEditors.SimpleButton();
            this.sbtHuy = new DevExpress.XtraEditors.SimpleButton();
            this.grcKP = new DevExpress.XtraGrid.GridControl();
            this.grvKP = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tenkp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Chọn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.ChkChon = new DevExpress.XtraEditors.CheckEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.chk_Ngay = new DevExpress.XtraEditors.CheckEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.chk_CPKem = new DevExpress.XtraEditors.CheckEdit();
            this.chk_NgoaiDM = new DevExpress.XtraEditors.CheckEdit();
            this.chk_TrongDMBH = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.grcKP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkChon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_Ngay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chk_CPKem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_NgoaiDM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_TrongDMBH.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // sbtTBC
            // 
            this.sbtTBC.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.sbtTBC.Appearance.Options.UseFont = true;
            this.sbtTBC.Dock = System.Windows.Forms.DockStyle.Left;
            this.sbtTBC.Location = new System.Drawing.Point(2, 2);
            this.sbtTBC.Name = "sbtTBC";
            this.sbtTBC.Size = new System.Drawing.Size(223, 45);
            this.sbtTBC.TabIndex = 0;
            this.sbtTBC.Text = "&Tạo báo cáo";
            this.sbtTBC.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // sbtHuy
            // 
            this.sbtHuy.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.sbtHuy.Appearance.Options.UseFont = true;
            this.sbtHuy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sbtHuy.Location = new System.Drawing.Point(225, 2);
            this.sbtHuy.Name = "sbtHuy";
            this.sbtHuy.Size = new System.Drawing.Size(235, 45);
            this.sbtHuy.TabIndex = 3;
            this.sbtHuy.Text = "&Huỷ";
            this.sbtHuy.Click += new System.EventHandler(this.sbtHuy_Click);
            // 
            // grcKP
            // 
            this.grcKP.Location = new System.Drawing.Point(2, 94);
            this.grcKP.MainView = this.grvKP;
            this.grcKP.Name = "grcKP";
            this.grcKP.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.grcKP.Size = new System.Drawing.Size(458, 142);
            this.grcKP.TabIndex = 4;
            this.grcKP.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvKP});
            // 
            // grvKP
            // 
            this.grvKP.Appearance.GroupPanel.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grvKP.Appearance.GroupPanel.Options.UseFont = true;
            this.grvKP.Appearance.HeaderPanel.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grvKP.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvKP.Appearance.Row.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grvKP.Appearance.Row.Options.UseFont = true;
            this.grvKP.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaKP,
            this.tenkp,
            this.Chọn});
            this.grvKP.GridControl = this.grcKP;
            this.grvKP.Name = "grvKP";
            this.grvKP.OptionsView.ShowGroupPanel = false;
            this.grvKP.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvKP_CellValueChanging);
            // 
            // colMaKP
            // 
            this.colMaKP.Caption = "MaKP";
            this.colMaKP.FieldName = "MaKP";
            this.colMaKP.Name = "colMaKP";
            this.colMaKP.Visible = true;
            this.colMaKP.VisibleIndex = 0;
            this.colMaKP.Width = 51;
            // 
            // tenkp
            // 
            this.tenkp.Caption = "Tên khoa phòng";
            this.tenkp.FieldName = "tenkp";
            this.tenkp.Name = "tenkp";
            this.tenkp.Visible = true;
            this.tenkp.VisibleIndex = 1;
            this.tenkp.Width = 312;
            // 
            // Chọn
            // 
            this.Chọn.Caption = "Chọn";
            this.Chọn.ColumnEdit = this.repositoryItemCheckEdit1;
            this.Chọn.FieldName = "chon";
            this.Chọn.Name = "Chọn";
            this.Chọn.Visible = true;
            this.Chọn.VisibleIndex = 2;
            this.Chọn.Width = 56;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Caption = "Check";
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // ChkChon
            // 
            this.ChkChon.Location = new System.Drawing.Point(12, 238);
            this.ChkChon.Name = "ChkChon";
            this.ChkChon.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.ChkChon.Properties.Appearance.Options.UseFont = true;
            this.ChkChon.Properties.Caption = "Hiển thị thuốc và các dịch vụ trên cùng một tờ";
            this.ChkChon.Size = new System.Drawing.Size(438, 20);
            this.ChkChon.TabIndex = 6;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.sbtHuy);
            this.panelControl1.Controls.Add(this.sbtTBC);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 284);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(462, 49);
            this.panelControl1.TabIndex = 7;
            // 
            // textEdit1
            // 
            this.textEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textEdit1.EditValue = "Phiếu điều trị";
            this.textEdit1.Location = new System.Drawing.Point(0, 0);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.textEdit1.Properties.Appearance.ForeColor = System.Drawing.Color.SteelBlue;
            this.textEdit1.Properties.Appearance.Options.UseFont = true;
            this.textEdit1.Properties.Appearance.Options.UseForeColor = true;
            this.textEdit1.Properties.Appearance.Options.UseTextOptions = true;
            this.textEdit1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.textEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.textEdit1.Properties.ReadOnly = true;
            this.textEdit1.Size = new System.Drawing.Size(462, 28);
            this.textEdit1.TabIndex = 8;
            // 
            // chk_Ngay
            // 
            this.chk_Ngay.Location = new System.Drawing.Point(12, 260);
            this.chk_Ngay.Name = "chk_Ngay";
            this.chk_Ngay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.chk_Ngay.Properties.Appearance.Options.UseFont = true;
            this.chk_Ngay.Properties.Caption = "Hiển ngày liên tiếp";
            this.chk_Ngay.Size = new System.Drawing.Size(438, 20);
            this.chk_Ngay.TabIndex = 9;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.chk_CPKem);
            this.groupControl1.Controls.Add(this.chk_NgoaiDM);
            this.groupControl1.Controls.Add(this.chk_TrongDMBH);
            this.groupControl1.Location = new System.Drawing.Point(2, 35);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(458, 53);
            this.groupControl1.TabIndex = 10;
            this.groupControl1.Text = "Chi phí trong DM";
            // 
            // chk_CPKem
            // 
            this.chk_CPKem.EditValue = true;
            this.chk_CPKem.Location = new System.Drawing.Point(335, 25);
            this.chk_CPKem.Name = "chk_CPKem";
            this.chk_CPKem.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.chk_CPKem.Properties.Appearance.Options.UseFont = true;
            this.chk_CPKem.Properties.Caption = "Chi phí kèm";
            this.chk_CPKem.Size = new System.Drawing.Size(113, 20);
            this.chk_CPKem.TabIndex = 2;
            // 
            // chk_NgoaiDM
            // 
            this.chk_NgoaiDM.EditValue = true;
            this.chk_NgoaiDM.Location = new System.Drawing.Point(175, 25);
            this.chk_NgoaiDM.Name = "chk_NgoaiDM";
            this.chk_NgoaiDM.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.chk_NgoaiDM.Properties.Appearance.Options.UseFont = true;
            this.chk_NgoaiDM.Properties.Caption = "Ngoài danh mục";
            this.chk_NgoaiDM.Size = new System.Drawing.Size(121, 20);
            this.chk_NgoaiDM.TabIndex = 1;
            // 
            // chk_TrongDMBH
            // 
            this.chk_TrongDMBH.EditValue = true;
            this.chk_TrongDMBH.Location = new System.Drawing.Point(11, 25);
            this.chk_TrongDMBH.Name = "chk_TrongDMBH";
            this.chk_TrongDMBH.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.chk_TrongDMBH.Properties.Appearance.Options.UseFont = true;
            this.chk_TrongDMBH.Properties.Caption = "Trong DMBHYT";
            this.chk_TrongDMBH.Size = new System.Drawing.Size(130, 20);
            this.chk_TrongDMBH.TabIndex = 0;
            // 
            // Frm_PhieuDieuTri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 333);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.chk_Ngay);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.ChkChon);
            this.Controls.Add(this.grcKP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_PhieuDieuTri";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tạo phiếu điều trị";
            this.Load += new System.EventHandler(this.Frm_PhieuDieuTri_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grcKP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkChon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_Ngay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chk_CPKem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_NgoaiDM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_TrongDMBH.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton sbtTBC;
        private DevExpress.XtraEditors.SimpleButton sbtHuy;
        private DevExpress.XtraGrid.GridControl grcKP;
        private DevExpress.XtraGrid.Views.Grid.GridView grvKP;
        private DevExpress.XtraGrid.Columns.GridColumn colMaKP;
        private DevExpress.XtraGrid.Columns.GridColumn tenkp;
        private DevExpress.XtraGrid.Columns.GridColumn Chọn;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.CheckEdit ChkChon;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.CheckEdit chk_Ngay;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.CheckEdit chk_CPKem;
        private DevExpress.XtraEditors.CheckEdit chk_NgoaiDM;
        private DevExpress.XtraEditors.CheckEdit chk_TrongDMBH;
    }
}