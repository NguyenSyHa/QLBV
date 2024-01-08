namespace QLBV.FormThamSo
{
    partial class frm_DsDichVuChon
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtTenDV = new DevExpress.XtraEditors.TextEdit();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.btnXem = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.grcDV = new DevExpress.XtraGrid.GridControl();
            this.grvDV = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colChon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaDV = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenDV.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcDV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDV)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.Appearance.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtTenDV);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(495, 72);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Tìm kiếm";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(29, 32);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(47, 16);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Tên DV:";
            // 
            // txtTenDV
            // 
            this.txtTenDV.Location = new System.Drawing.Point(82, 29);
            this.txtTenDV.Name = "txtTenDV";
            this.txtTenDV.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenDV.Properties.Appearance.Options.UseFont = true;
            this.txtTenDV.Size = new System.Drawing.Size(259, 22);
            this.txtTenDV.TabIndex = 1;
            this.txtTenDV.EditValueChanged += new System.EventHandler(this.txtTenDV_EditValueChanging);
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.Image = global::QLBV.Properties.Resources.delete_16x16;
            this.btnThoat.Location = new System.Drawing.Point(408, 349);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(75, 30);
            this.btnThoat.TabIndex = 2;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnXem
            // 
            this.btnXem.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXem.Appearance.Options.UseFont = true;
            this.btnXem.Image = global::QLBV.Properties.Resources.preview_16x16;
            this.btnXem.Location = new System.Drawing.Point(316, 349);
            this.btnXem.Name = "btnXem";
            this.btnXem.Size = new System.Drawing.Size(86, 30);
            this.btnXem.TabIndex = 1;
            this.btnXem.Text = "Đồng ý";
            this.btnXem.Click += new System.EventHandler(this.btnXem_Click);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.grcDV);
            this.groupControl2.Location = new System.Drawing.Point(0, 76);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.ShowCaption = false;
            this.groupControl2.Size = new System.Drawing.Size(495, 267);
            this.groupControl2.TabIndex = 125;
            // 
            // grcDV
            // 
            this.grcDV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcDV.EmbeddedNavigator.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grcDV.EmbeddedNavigator.Appearance.Options.UseFont = true;
            this.grcDV.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grcDV.Location = new System.Drawing.Point(2, 2);
            this.grcDV.MainView = this.grvDV;
            this.grcDV.Name = "grcDV";
            this.grcDV.Size = new System.Drawing.Size(491, 263);
            this.grcDV.TabIndex = 0;
            this.grcDV.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDV});
            // 
            // grvDV
            // 
            this.grvDV.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colChon,
            this.colTenDV,
            this.colMaDV});
            this.grvDV.GridControl = this.grcDV;
            this.grvDV.Name = "grvDV";
            this.grvDV.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.grvDV.OptionsView.ShowGroupPanel = false;
            this.grvDV.OptionsView.ShowViewCaption = true;
            this.grvDV.ViewCaption = "Danh sách dịch vụ";
            this.grvDV.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvDV_CellValueChanging);
            // 
            // colChon
            // 
            this.colChon.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colChon.AppearanceHeader.Options.UseFont = true;
            this.colChon.Caption = "Chọn";
            this.colChon.FieldName = "Chon";
            this.colChon.MinWidth = 10;
            this.colChon.Name = "colChon";
            this.colChon.Visible = true;
            this.colChon.VisibleIndex = 0;
            this.colChon.Width = 40;
            // 
            // colTenDV
            // 
            this.colTenDV.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTenDV.AppearanceHeader.Options.UseFont = true;
            this.colTenDV.Caption = "Tên dịch vụ";
            this.colTenDV.FieldName = "TenDV";
            this.colTenDV.Name = "colTenDV";
            this.colTenDV.OptionsColumn.AllowEdit = false;
            this.colTenDV.Visible = true;
            this.colTenDV.VisibleIndex = 1;
            this.colTenDV.Width = 357;
            // 
            // colMaDV
            // 
            this.colMaDV.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colMaDV.AppearanceHeader.Options.UseFont = true;
            this.colMaDV.Caption = "Mã DV";
            this.colMaDV.FieldName = "MaDV";
            this.colMaDV.Name = "colMaDV";
            this.colMaDV.Visible = true;
            this.colMaDV.VisibleIndex = 2;
            this.colMaDV.Width = 76;
            // 
            // frm_DsDichVuChon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 384);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.btnXem);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.groupControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_DsDichVuChon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách dịch vụ";
            this.Load += new System.EventHandler(this.frm_DsDichVuChon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenDV.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcDV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtTenDV;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.SimpleButton btnXem;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl grcDV;
        private DevExpress.XtraGrid.Views.Grid.GridView grvDV;
        private DevExpress.XtraGrid.Columns.GridColumn colChon;
        private DevExpress.XtraGrid.Columns.GridColumn colTenDV;
        private DevExpress.XtraGrid.Columns.GridColumn colMaDV;
    }
}