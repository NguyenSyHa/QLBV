namespace QLBV.FormDanhMuc
{
    partial class dmDoiTuongcs
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.MaDTuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TenDTuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Nhom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.VanChuyen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Status = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnOK);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 316);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(747, 44);
            this.panelControl1.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Location = new System.Drawing.Point(5, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&Lưu";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.gridControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(747, 316);
            this.panelControl2.TabIndex = 2;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 2);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(743, 312);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.MaDTuong,
            this.TenDTuong,
            this.Nhom,
            this.VanChuyen,
            this.Status});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowViewCaption = true;
            this.gridView1.ViewCaption = "Danh mục đối tượng Bảo Hiểm Y Tế";
            // 
            // MaDTuong
            // 
            this.MaDTuong.Caption = "Mã ĐT";
            this.MaDTuong.FieldName = "MaDTuong";
            this.MaDTuong.Name = "MaDTuong";
            this.MaDTuong.OptionsColumn.AllowEdit = false;
            this.MaDTuong.OptionsColumn.AllowFocus = false;
            this.MaDTuong.OptionsColumn.ReadOnly = true;
            this.MaDTuong.Visible = true;
            this.MaDTuong.VisibleIndex = 0;
            this.MaDTuong.Width = 71;
            // 
            // TenDTuong
            // 
            this.TenDTuong.Caption = "Đối tượng";
            this.TenDTuong.FieldName = "TenDTuong";
            this.TenDTuong.Name = "TenDTuong";
            this.TenDTuong.Visible = true;
            this.TenDTuong.VisibleIndex = 1;
            this.TenDTuong.Width = 208;
            // 
            // Nhom
            // 
            this.Nhom.Caption = "Nhóm";
            this.Nhom.FieldName = "Nhom";
            this.Nhom.Name = "Nhom";
            this.Nhom.Visible = true;
            this.Nhom.VisibleIndex = 2;
            this.Nhom.Width = 188;
            // 
            // VanChuyen
            // 
            this.VanChuyen.Caption = "Hưởng vận chuyện";
            this.VanChuyen.FieldName = "VanChuyen";
            this.VanChuyen.Name = "VanChuyen";
            this.VanChuyen.Visible = true;
            this.VanChuyen.VisibleIndex = 3;
            this.VanChuyen.Width = 130;
            // 
            // Status
            // 
            this.Status.Caption = "Trạng thái";
            this.Status.FieldName = "Status";
            this.Status.Name = "Status";
            this.Status.Visible = true;
            this.Status.VisibleIndex = 4;
            this.Status.Width = 171;
            // 
            // dmDoiTuongcs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "dmDoiTuongcs";
            this.Size = new System.Drawing.Size(747, 360);
            this.Load += new System.EventHandler(this.dmDoiTuongcs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn MaDTuong;
        private DevExpress.XtraGrid.Columns.GridColumn TenDTuong;
        private DevExpress.XtraGrid.Columns.GridColumn Nhom;
        private DevExpress.XtraGrid.Columns.GridColumn VanChuyen;
        private DevExpress.XtraGrid.Columns.GridColumn Status;
    }
}
