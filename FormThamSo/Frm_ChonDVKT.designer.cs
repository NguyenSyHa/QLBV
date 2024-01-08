namespace QLBV.FormThamSo
{
    partial class Frm_ChonDVKT
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
            this.grcDV = new DevExpress.XtraGrid.GridControl();
            this.grvDV = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TenKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Chọn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.IDNhom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.grcDV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
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
            // grcDV
            // 
            this.grcDV.Location = new System.Drawing.Point(2, 36);
            this.grcDV.MainView = this.grvDV;
            this.grcDV.Name = "grcDV";
            this.grcDV.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.grcDV.Size = new System.Drawing.Size(458, 142);
            this.grcDV.TabIndex = 4;
            this.grcDV.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDV});
            // 
            // grvDV
            // 
            this.grvDV.Appearance.GroupPanel.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grvDV.Appearance.GroupPanel.Options.UseFont = true;
            this.grvDV.Appearance.HeaderPanel.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grvDV.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvDV.Appearance.Row.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grvDV.Appearance.Row.Options.UseFont = true;
            this.grvDV.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaKP,
            this.TenKP,
            this.Chọn,
            this.IDNhom});
            this.grvDV.GridControl = this.grcDV;
            this.grvDV.Name = "grvDV";
            this.grvDV.OptionsView.ShowGroupPanel = false;
            this.grvDV.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvDV_CellValueChanging);
            // 
            // colMaKP
            // 
            this.colMaKP.Caption = "MaKP";
            this.colMaKP.FieldName = "MaKP";
            this.colMaKP.Name = "colMaKP";
            this.colMaKP.Width = 51;
            // 
            // TenKP
            // 
            this.TenKP.Caption = "Tên";
            this.TenKP.FieldName = "TenKP";
            this.TenKP.Name = "TenKP";
            this.TenKP.Visible = true;
            this.TenKP.VisibleIndex = 0;
            this.TenKP.Width = 312;
            // 
            // Chọn
            // 
            this.Chọn.Caption = "Chọn";
            this.Chọn.ColumnEdit = this.repositoryItemCheckEdit1;
            this.Chọn.FieldName = "Chon";
            this.Chọn.Name = "Chọn";
            this.Chọn.Visible = true;
            this.Chọn.VisibleIndex = 1;
            this.Chọn.Width = 56;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Caption = "Check";
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // IDNhom
            // 
            this.IDNhom.Caption = "IDNhom";
            this.IDNhom.FieldName = "IDNhom";
            this.IDNhom.Name = "IDNhom";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.sbtHuy);
            this.panelControl1.Controls.Add(this.sbtTBC);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 188);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(462, 49);
            this.panelControl1.TabIndex = 7;
            // 
            // textEdit1
            // 
            this.textEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textEdit1.EditValue = "Phiếu công khai dịch vụ";
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
            // Frm_ChonDVKT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 237);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.grcDV);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_ChonDVKT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tạo phiếu điều trị";
            this.Load += new System.EventHandler(this.Frm_ChonDVKT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grcDV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton sbtTBC;
        private DevExpress.XtraEditors.SimpleButton sbtHuy;
        private DevExpress.XtraGrid.GridControl grcDV;
        private DevExpress.XtraGrid.Views.Grid.GridView grvDV;
        private DevExpress.XtraGrid.Columns.GridColumn colMaKP;
        private DevExpress.XtraGrid.Columns.GridColumn TenKP;
        private DevExpress.XtraGrid.Columns.GridColumn Chọn;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn IDNhom;
    }
}