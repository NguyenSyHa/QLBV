namespace QLBV.FormDanhMuc
{
    partial class frm_DM5084
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_DM5084));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.btn_Chon = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtTimKiem = new DevExpress.XtraEditors.TextEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.grc_CV = new DevExpress.XtraGrid.GridControl();
            this.grv_CV = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.col_CV_IDDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_CV_MaDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_CV_TenDV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_CV_HC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_CV_SDK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.STT_ThongTu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DongGoi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TrongDM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BaoChe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TieuChuan = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimKiem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grc_CV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grv_CV)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.groupControl2);
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(986, 61);
            this.panelControl1.TabIndex = 0;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.btn_Chon);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(448, 2);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(536, 57);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Chức năng";
            // 
            // btn_Chon
            // 
            this.btn_Chon.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_Chon.Appearance.Options.UseFont = true;
            this.btn_Chon.Image = ((System.Drawing.Image)(resources.GetObject("btn_Chon.Image")));
            this.btn_Chon.Location = new System.Drawing.Point(6, 17);
            this.btn_Chon.Name = "btn_Chon";
            this.btn_Chon.Size = new System.Drawing.Size(92, 33);
            this.btn_Chon.TabIndex = 0;
            this.btn_Chon.Text = "Chọn";
            this.btn_Chon.Click += new System.EventHandler(this.btn_Chon_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtTimKiem);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupControl1.Location = new System.Drawing.Point(2, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(446, 57);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Tìm kiếm";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(10, 18);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTimKiem.Properties.Appearance.Options.UseFont = true;
            this.txtTimKiem.Properties.NullText = "Nhập tên thuốc|hoạt chất|Số đăng ký";
            this.txtTimKiem.Size = new System.Drawing.Size(417, 22);
            this.txtTimKiem.TabIndex = 0;
            this.txtTimKiem.EditValueChanged += new System.EventHandler(this.txtTimKiem_EditValueChanged);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.grc_CV);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 61);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(986, 298);
            this.panelControl2.TabIndex = 1;
            // 
            // grc_CV
            // 
            this.grc_CV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grc_CV.Location = new System.Drawing.Point(2, 2);
            this.grc_CV.MainView = this.grv_CV;
            this.grc_CV.Name = "grc_CV";
            this.grc_CV.Size = new System.Drawing.Size(982, 294);
            this.grc_CV.TabIndex = 2;
            this.grc_CV.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grv_CV});
            // 
            // grv_CV
            // 
            this.grv_CV.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grv_CV.Appearance.HeaderPanel.Options.UseFont = true;
            this.grv_CV.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grv_CV.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grv_CV.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grv_CV.Appearance.SelectedRow.BackColor = System.Drawing.Color.RoyalBlue;
            this.grv_CV.Appearance.SelectedRow.BackColor2 = System.Drawing.Color.RoyalBlue;
            this.grv_CV.Appearance.SelectedRow.Options.UseBackColor = true;
            this.grv_CV.Appearance.SelectedRow.Options.UseTextOptions = true;
            this.grv_CV.Appearance.SelectedRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grv_CV.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col_CV_IDDV,
            this.col_CV_MaDV,
            this.col_CV_TenDV,
            this.col_CV_HC,
            this.col_CV_SDK,
            this.STT_ThongTu,
            this.DongGoi,
            this.TrongDM,
            this.BaoChe,
            this.TieuChuan});
            this.grv_CV.GridControl = this.grc_CV;
            this.grv_CV.Name = "grv_CV";
            this.grv_CV.OptionsBehavior.Editable = false;
            this.grv_CV.OptionsBehavior.ReadOnly = true;
            this.grv_CV.OptionsView.ColumnAutoWidth = false;
            this.grv_CV.OptionsView.EnableAppearanceEvenRow = true;
            this.grv_CV.OptionsView.EnableAppearanceOddRow = true;
            this.grv_CV.OptionsView.ShowGroupPanel = false;
            // 
            // col_CV_IDDV
            // 
            this.col_CV_IDDV.Caption = "ID dịch vụ";
            this.col_CV_IDDV.FieldName = "idDichVu";
            this.col_CV_IDDV.Name = "col_CV_IDDV";
            // 
            // col_CV_MaDV
            // 
            this.col_CV_MaDV.Caption = "Mã DVQĐ";
            this.col_CV_MaDV.FieldName = "MaDV";
            this.col_CV_MaDV.Name = "col_CV_MaDV";
            this.col_CV_MaDV.Visible = true;
            this.col_CV_MaDV.VisibleIndex = 0;
            this.col_CV_MaDV.Width = 70;
            // 
            // col_CV_TenDV
            // 
            this.col_CV_TenDV.Caption = "Tên dịch vụ";
            this.col_CV_TenDV.FieldName = "TenDV";
            this.col_CV_TenDV.Name = "col_CV_TenDV";
            this.col_CV_TenDV.Visible = true;
            this.col_CV_TenDV.VisibleIndex = 1;
            this.col_CV_TenDV.Width = 200;
            // 
            // col_CV_HC
            // 
            this.col_CV_HC.Caption = "Hoạt chất";
            this.col_CV_HC.FieldName = "HoatChat";
            this.col_CV_HC.Name = "col_CV_HC";
            this.col_CV_HC.Visible = true;
            this.col_CV_HC.VisibleIndex = 2;
            this.col_CV_HC.Width = 200;
            // 
            // col_CV_SDK
            // 
            this.col_CV_SDK.Caption = "Số đăng ký";
            this.col_CV_SDK.FieldName = "SoDangKy";
            this.col_CV_SDK.Name = "col_CV_SDK";
            this.col_CV_SDK.Visible = true;
            this.col_CV_SDK.VisibleIndex = 3;
            // 
            // STT_ThongTu
            // 
            this.STT_ThongTu.Caption = "Số TT Thông tư";
            this.STT_ThongTu.FieldName = "STT_ThongTu";
            this.STT_ThongTu.Name = "STT_ThongTu";
            this.STT_ThongTu.Visible = true;
            this.STT_ThongTu.VisibleIndex = 4;
            // 
            // DongGoi
            // 
            this.DongGoi.Caption = "DongGoi";
            this.DongGoi.FieldName = "DongGoi";
            this.DongGoi.Name = "DongGoi";
            this.DongGoi.Visible = true;
            this.DongGoi.VisibleIndex = 5;
            // 
            // TrongDM
            // 
            this.TrongDM.Caption = "TrongDM";
            this.TrongDM.FieldName = "TrongDM";
            this.TrongDM.Name = "TrongDM";
            this.TrongDM.Visible = true;
            this.TrongDM.VisibleIndex = 8;
            // 
            // BaoChe
            // 
            this.BaoChe.Caption = "BaoChe";
            this.BaoChe.FieldName = "BaoChe";
            this.BaoChe.Name = "BaoChe";
            this.BaoChe.Visible = true;
            this.BaoChe.VisibleIndex = 6;
            this.BaoChe.Width = 88;
            // 
            // TieuChuan
            // 
            this.TieuChuan.Caption = "TieuChuan";
            this.TieuChuan.FieldName = "TieuChuan";
            this.TieuChuan.Name = "TieuChuan";
            this.TieuChuan.Visible = true;
            this.TieuChuan.VisibleIndex = 7;
            // 
            // frm_DM5084
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 359);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "frm_DM5084";
            this.Text = "frm_DM5084";
            this.Load += new System.EventHandler(this.frm_DM5084_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTimKiem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grc_CV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grv_CV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btn_Chon;
        private DevExpress.XtraEditors.TextEdit txtTimKiem;
        private DevExpress.XtraGrid.GridControl grc_CV;
        private DevExpress.XtraGrid.Views.Grid.GridView grv_CV;
        private DevExpress.XtraGrid.Columns.GridColumn col_CV_IDDV;
        private DevExpress.XtraGrid.Columns.GridColumn col_CV_MaDV;
        private DevExpress.XtraGrid.Columns.GridColumn col_CV_TenDV;
        private DevExpress.XtraGrid.Columns.GridColumn col_CV_HC;
        private DevExpress.XtraGrid.Columns.GridColumn col_CV_SDK;
        private DevExpress.XtraGrid.Columns.GridColumn STT_ThongTu;
        private DevExpress.XtraGrid.Columns.GridColumn DongGoi;
        private DevExpress.XtraGrid.Columns.GridColumn TrongDM;
        private DevExpress.XtraGrid.Columns.GridColumn BaoChe;
        private DevExpress.XtraGrid.Columns.GridColumn TieuChuan;
    }
}