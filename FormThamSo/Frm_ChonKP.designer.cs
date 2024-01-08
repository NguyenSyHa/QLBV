namespace QLBV.FormThamSo
{
    partial class Frm_ChonKP
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.radBN = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.grcKP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radBN.Properties)).BeginInit();
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
            this.grcKP.Location = new System.Drawing.Point(2, 36);
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
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.sbtHuy);
            this.panelControl1.Controls.Add(this.sbtTBC);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 203);
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
            // radBN
            // 
            this.radBN.Location = new System.Drawing.Point(2, 177);
            this.radBN.Name = "radBN";
            this.radBN.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Ngoài DM"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Trong DM"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Không Thanh Toán"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Tất Cả")});
            this.radBN.Size = new System.Drawing.Size(458, 25);
            this.radBN.TabIndex = 16;
            this.radBN.Visible = false;
            // 
            // Frm_ChonKP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 252);
            this.Controls.Add(this.radBN);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.grcKP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_ChonKP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tạo phiếu điều trị";
            this.Load += new System.EventHandler(this.Frm_ChonKP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grcKP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radBN.Properties)).EndInit();
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
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.RadioGroup radBN;
    }
}