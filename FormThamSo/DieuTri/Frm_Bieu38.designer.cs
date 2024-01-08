namespace QLBV.FormThamSo
{
    partial class Frm_Bieu38
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
            this.radDM = new DevExpress.XtraEditors.RadioGroup();
            this.grcKP = new DevExpress.XtraGrid.GridControl();
            this.grvKP = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaKP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tenkp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Chọn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.sbtTaoBC = new DevExpress.XtraEditors.SimpleButton();
            this.sbtThoat = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.radDM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcKP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // radDM
            // 
            this.radDM.Location = new System.Drawing.Point(12, 12);
            this.radDM.Name = "radDM";
            this.radDM.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Thuốc trong danh mục"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Thuốc ngoài danh mục"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Cả hai")});
            this.radDM.Size = new System.Drawing.Size(437, 27);
            this.radDM.TabIndex = 0;
            // 
            // grcKP
            // 
            this.grcKP.Location = new System.Drawing.Point(12, 45);
            this.grcKP.MainView = this.grvKP;
            this.grcKP.Name = "grcKP";
            this.grcKP.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.grcKP.Size = new System.Drawing.Size(437, 158);
            this.grcKP.TabIndex = 1;
            this.grcKP.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvKP});
            // 
            // grvKP
            // 
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
            // sbtTaoBC
            // 
            this.sbtTaoBC.Location = new System.Drawing.Point(79, 228);
            this.sbtTaoBC.Name = "sbtTaoBC";
            this.sbtTaoBC.Size = new System.Drawing.Size(75, 23);
            this.sbtTaoBC.TabIndex = 2;
            this.sbtTaoBC.Text = "Tạo báo cáo";
            this.sbtTaoBC.Click += new System.EventHandler(this.sbtTaoBC_Click);
            // 
            // sbtThoat
            // 
            this.sbtThoat.Location = new System.Drawing.Point(293, 228);
            this.sbtThoat.Name = "sbtThoat";
            this.sbtThoat.Size = new System.Drawing.Size(75, 23);
            this.sbtThoat.TabIndex = 3;
            this.sbtThoat.Text = "Thoát";
            this.sbtThoat.Click += new System.EventHandler(this.sbtThoat_Click);
            // 
            // Frm_Bieu38
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 277);
            this.Controls.Add(this.sbtThoat);
            this.Controls.Add(this.sbtTaoBC);
            this.Controls.Add(this.grcKP);
            this.Controls.Add(this.radDM);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Bieu38";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tạo biểu 38";
            this.Load += new System.EventHandler(this.Frm_Bieu38_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radDM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grcKP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.RadioGroup radDM;
        private DevExpress.XtraGrid.GridControl grcKP;
        private DevExpress.XtraGrid.Views.Grid.GridView grvKP;
        private DevExpress.XtraGrid.Columns.GridColumn colMaKP;
        private DevExpress.XtraGrid.Columns.GridColumn tenkp;
        private DevExpress.XtraGrid.Columns.GridColumn Chọn;
        private DevExpress.XtraEditors.SimpleButton sbtTaoBC;
        private DevExpress.XtraEditors.SimpleButton sbtThoat;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;

    }
}