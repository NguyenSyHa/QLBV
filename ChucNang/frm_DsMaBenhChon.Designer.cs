namespace QLBV.ChucNang
{
    partial class frm_DsMaBenhChon
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
            this.txtTenICD = new DevExpress.XtraEditors.TextEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.grcICD = new DevExpress.XtraGrid.GridControl();
            this.grvICD = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colChon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenICD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaICD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnXem = new DevExpress.XtraEditors.SimpleButton();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenICD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcICD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvICD)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(17, 32);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(47, 16);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Tên DV:";
            // 
            // txtTenICD
            // 
            this.txtTenICD.Location = new System.Drawing.Point(82, 29);
            this.txtTenICD.Name = "txtTenICD";
            this.txtTenICD.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenICD.Properties.Appearance.Options.UseFont = true;
            this.txtTenICD.Size = new System.Drawing.Size(259, 22);
            this.txtTenICD.TabIndex = 1;
            this.txtTenICD.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txtTenICD_EditValueChanging);
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.Appearance.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtTenICD);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(498, 72);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Tìm kiếm";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.grcICD);
            this.groupControl2.Location = new System.Drawing.Point(0, 73);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.ShowCaption = false;
            this.groupControl2.Size = new System.Drawing.Size(495, 267);
            this.groupControl2.TabIndex = 128;
            // 
            // grcICD
            // 
            this.grcICD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcICD.EmbeddedNavigator.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grcICD.EmbeddedNavigator.Appearance.Options.UseFont = true;
            this.grcICD.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grcICD.Location = new System.Drawing.Point(2, 2);
            this.grcICD.MainView = this.grvICD;
            this.grcICD.Name = "grcICD";
            this.grcICD.Size = new System.Drawing.Size(491, 263);
            this.grcICD.TabIndex = 0;
            this.grcICD.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvICD});
            // 
            // grvICD
            // 
            this.grvICD.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colChon,
            this.colTenICD,
            this.colMaICD});
            this.grvICD.GridControl = this.grcICD;
            this.grvICD.Name = "grvICD";
            this.grvICD.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.grvICD.OptionsView.ShowGroupPanel = false;
            this.grvICD.OptionsView.ShowViewCaption = true;
            this.grvICD.ViewCaption = "Danh sách dịch vụ";
            this.grvICD.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvDV_CellValueChanging);
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
            // colTenICD
            // 
            this.colTenICD.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTenICD.AppearanceHeader.Options.UseFont = true;
            this.colTenICD.Caption = "Tên dịch vụ";
            this.colTenICD.FieldName = "TenICD";
            this.colTenICD.Name = "colTenICD";
            this.colTenICD.OptionsColumn.AllowEdit = false;
            this.colTenICD.Visible = true;
            this.colTenICD.VisibleIndex = 1;
            this.colTenICD.Width = 357;
            // 
            // colMaICD
            // 
            this.colMaICD.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colMaICD.AppearanceHeader.Options.UseFont = true;
            this.colMaICD.Caption = "Mã ICD";
            this.colMaICD.FieldName = "MaICD";
            this.colMaICD.Name = "colMaICD";
            this.colMaICD.Visible = true;
            this.colMaICD.VisibleIndex = 2;
            this.colMaICD.Width = 76;
            // 
            // btnXem
            // 
            this.btnXem.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXem.Appearance.Options.UseFont = true;
            this.btnXem.Image = global::QLBV.Properties.Resources.preview_16x16;
            this.btnXem.Location = new System.Drawing.Point(316, 346);
            this.btnXem.Name = "btnXem";
            this.btnXem.Size = new System.Drawing.Size(86, 30);
            this.btnXem.TabIndex = 126;
            this.btnXem.Text = "Đồng ý";
            this.btnXem.Click += new System.EventHandler(this.btnXem_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.Image = global::QLBV.Properties.Resources.delete_16x16;
            this.btnThoat.Location = new System.Drawing.Point(408, 346);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(75, 30);
            this.btnThoat.TabIndex = 127;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // frm_DsMaBenhChon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 381);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.btnXem);
            this.Controls.Add(this.btnThoat);
            this.Name = "frm_DsMaBenhChon";
            this.Text = "frm_DsMaBenhChon";
            this.Load += new System.EventHandler(this.frm_DsMaBenhChon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtTenICD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcICD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvICD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtTenICD;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl grcICD;
        private DevExpress.XtraGrid.Views.Grid.GridView grvICD;
        private DevExpress.XtraGrid.Columns.GridColumn colChon;
        private DevExpress.XtraGrid.Columns.GridColumn colTenICD;
        private DevExpress.XtraGrid.Columns.GridColumn colMaICD;
        private DevExpress.XtraEditors.SimpleButton btnXem;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
    }
}