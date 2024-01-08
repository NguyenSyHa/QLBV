namespace QLBV.FormThamSo
{
    partial class Frm_Download
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Download));
            this.txtMatkhau = new DevExpress.XtraEditors.TextEdit();
            this.txtTaikhoan = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtDuongdan = new DevExpress.XtraEditors.TextEdit();
            this.sbtBR2 = new DevExpress.XtraEditors.SimpleButton();
            this.sbtDownload = new DevExpress.XtraEditors.SimpleButton();
            this.GrcDS = new DevExpress.XtraGrid.GridControl();
            this.GrvDS = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.STT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenFile = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chk = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.txtMatkhau.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaikhoan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDuongdan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrcDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrvDS)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMatkhau
            // 
            this.txtMatkhau.Location = new System.Drawing.Point(93, 47);
            this.txtMatkhau.Name = "txtMatkhau";
            this.txtMatkhau.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtMatkhau.Properties.Appearance.Options.UseFont = true;
            this.txtMatkhau.Properties.PasswordChar = '*';
            this.txtMatkhau.Size = new System.Drawing.Size(151, 26);
            this.txtMatkhau.TabIndex = 1;
            this.txtMatkhau.EditValueChanged += new System.EventHandler(this.txtMatkhau_EditValueChanged);
            this.txtMatkhau.Leave += new System.EventHandler(this.txtMatkhau_Leave);
            // 
            // txtTaikhoan
            // 
            this.txtTaikhoan.Location = new System.Drawing.Point(93, 21);
            this.txtTaikhoan.Name = "txtTaikhoan";
            this.txtTaikhoan.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTaikhoan.Properties.Appearance.Options.UseFont = true;
            this.txtTaikhoan.Size = new System.Drawing.Size(151, 26);
            this.txtTaikhoan.TabIndex = 0;
            this.txtTaikhoan.EditValueChanged += new System.EventHandler(this.txtTaikhoan_EditValueChanged);
            this.txtTaikhoan.Leave += new System.EventHandler(this.txtTaikhoan_Leave);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl3.Location = new System.Drawing.Point(14, 49);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(71, 19);
            this.labelControl3.TabIndex = 10;
            this.labelControl3.Text = "Mật khẩu:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl2.Location = new System.Drawing.Point(14, 24);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(72, 19);
            this.labelControl2.TabIndex = 9;
            this.labelControl2.Text = "Tài khoản:";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(39, 74);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(19, 23);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "Kiểm tra kết nối";
            this.simpleButton1.Visible = false;
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl4.Location = new System.Drawing.Point(7, 236);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(80, 19);
            this.labelControl4.TabIndex = 16;
            this.labelControl4.Text = "ĐD lưu file:";
            // 
            // txtDuongdan
            // 
            this.txtDuongdan.Location = new System.Drawing.Point(93, 233);
            this.txtDuongdan.Name = "txtDuongdan";
            this.txtDuongdan.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtDuongdan.Properties.Appearance.Options.UseFont = true;
            this.txtDuongdan.Size = new System.Drawing.Size(571, 26);
            this.txtDuongdan.TabIndex = 17;
            // 
            // sbtBR2
            // 
            this.sbtBR2.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.sbtBR2.Appearance.Options.UseFont = true;
            this.sbtBR2.Image = global::QLBV.Properties.Resources.save_16x16;
            this.sbtBR2.Location = new System.Drawing.Point(664, 232);
            this.sbtBR2.Name = "sbtBR2";
            this.sbtBR2.Size = new System.Drawing.Size(25, 27);
            this.sbtBR2.TabIndex = 4;
            this.sbtBR2.Click += new System.EventHandler(this.sbtBR2_Click);
            // 
            // sbtDownload
            // 
            this.sbtDownload.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.sbtDownload.Appearance.Options.UseFont = true;
            this.sbtDownload.Image = ((System.Drawing.Image)(resources.GetObject("sbtDownload.Image")));
            this.sbtDownload.Location = new System.Drawing.Point(93, 73);
            this.sbtDownload.Name = "sbtDownload";
            this.sbtDownload.Size = new System.Drawing.Size(151, 151);
            this.sbtDownload.TabIndex = 5;
            this.sbtDownload.Text = "Download";
            this.sbtDownload.Click += new System.EventHandler(this.sbtDownload_Click);
            // 
            // GrcDS
            // 
            this.GrcDS.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.GrcDS.Location = new System.Drawing.Point(250, 24);
            this.GrcDS.MainView = this.GrvDS;
            this.GrcDS.Name = "GrcDS";
            this.GrcDS.Size = new System.Drawing.Size(437, 200);
            this.GrcDS.TabIndex = 3;
            this.GrcDS.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrvDS});
            // 
            // GrvDS
            // 
            this.GrvDS.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.STT,
            this.colTenFile,
            this.chk});
            this.GrvDS.GridControl = this.GrcDS;
            this.GrvDS.Name = "GrvDS";
            this.GrvDS.OptionsView.ShowGroupPanel = false;
            this.GrvDS.OptionsView.ShowViewCaption = true;
            this.GrvDS.ViewCaption = "Chọn File tải về";
            this.GrvDS.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.GrvDS_CellValueChanging);
            // 
            // STT
            // 
            this.STT.Caption = "STT";
            this.STT.FieldName = "SoTT";
            this.STT.Name = "STT";
            this.STT.OptionsColumn.AllowEdit = false;
            this.STT.OptionsColumn.AllowFocus = false;
            this.STT.Visible = true;
            this.STT.VisibleIndex = 0;
            this.STT.Width = 43;
            // 
            // colTenFile
            // 
            this.colTenFile.Caption = "Tên File";
            this.colTenFile.FieldName = "Tenfile";
            this.colTenFile.Name = "colTenFile";
            this.colTenFile.OptionsColumn.AllowEdit = false;
            this.colTenFile.Visible = true;
            this.colTenFile.VisibleIndex = 1;
            this.colTenFile.Width = 160;
            // 
            // chk
            // 
            this.chk.Caption = "Chọn";
            this.chk.FieldName = "Chon";
            this.chk.Name = "chk";
            this.chk.Visible = true;
            this.chk.VisibleIndex = 2;
            this.chk.Width = 64;
            // 
            // Frm_Download
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 271);
            this.Controls.Add(this.GrcDS);
            this.Controls.Add(this.sbtDownload);
            this.Controls.Add(this.sbtBR2);
            this.Controls.Add(this.txtDuongdan);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.txtMatkhau);
            this.Controls.Add(this.txtTaikhoan);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.simpleButton1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Download";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DownLoad";
            this.Load += new System.EventHandler(this.Frm_Download_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtMatkhau.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaikhoan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDuongdan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrcDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrvDS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtMatkhau;
        private DevExpress.XtraEditors.TextEdit txtTaikhoan;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtDuongdan;
        private DevExpress.XtraEditors.SimpleButton sbtBR2;
        private DevExpress.XtraEditors.SimpleButton sbtDownload;
        private DevExpress.XtraGrid.GridControl GrcDS;
        private DevExpress.XtraGrid.Views.Grid.GridView GrvDS;
        private DevExpress.XtraGrid.Columns.GridColumn STT;
        private DevExpress.XtraGrid.Columns.GridColumn colTenFile;
        private DevExpress.XtraGrid.Columns.GridColumn chk;
    }
}