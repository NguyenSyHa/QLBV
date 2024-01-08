namespace QLBV.FormThamSo.SoTiepDon
{
    partial class frm_BC_SoTiepDonBenhNhan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_BC_SoTiepDonBenhNhan));
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.cboDTuong = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.dtTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.dtDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.lupMaKP = new DevExpress.XtraEditors.LookUpEdit();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnInBC = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.cboDTuong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMaKP.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl11
            // 
            this.labelControl11.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl11.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl11.Appearance.Options.UseFont = true;
            this.labelControl11.Appearance.Options.UseForeColor = true;
            this.labelControl11.Location = new System.Drawing.Point(12, 63);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(59, 13);
            this.labelControl11.TabIndex = 24;
            this.labelControl11.Text = "Đối tượng:";
            // 
            // cboDTuong
            // 
            this.cboDTuong.EditValue = "99";
            this.cboDTuong.Location = new System.Drawing.Point(100, 54);
            this.cboDTuong.Name = "cboDTuong";
            this.cboDTuong.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cboDTuong.Properties.Appearance.ForeColor = System.Drawing.Color.DarkCyan;
            this.cboDTuong.Properties.Appearance.Options.UseFont = true;
            this.cboDTuong.Properties.Appearance.Options.UseForeColor = true;
            this.cboDTuong.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.GhostWhite;
            this.cboDTuong.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray;
            this.cboDTuong.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.cboDTuong.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.cboDTuong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDTuong.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DTBN1", "Đối tượng")});
            this.cboDTuong.Properties.DisplayMember = "DTBN1";
            this.cboDTuong.Properties.NullText = "";
            this.cboDTuong.Properties.PopupSizeable = false;
            this.cboDTuong.Properties.ValueMember = "IDDTBN";
            this.cboDTuong.Size = new System.Drawing.Size(172, 22);
            this.cboDTuong.TabIndex = 22;
            this.cboDTuong.EditValueChanged += new System.EventHandler(this.cboDTuong_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(12, 12);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(69, 13);
            this.labelControl2.TabIndex = 23;
            this.labelControl2.Text = "Khoa phòng:";
            // 
            // labelControl19
            // 
            this.labelControl19.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl19.Appearance.Options.UseFont = true;
            this.labelControl19.Location = new System.Drawing.Point(12, 147);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(57, 13);
            this.labelControl19.TabIndex = 21;
            this.labelControl19.Text = "Đến ngày:";
            // 
            // labelControl18
            // 
            this.labelControl18.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl18.Appearance.Options.UseFont = true;
            this.labelControl18.Location = new System.Drawing.Point(12, 104);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(49, 13);
            this.labelControl18.TabIndex = 18;
            this.labelControl18.Text = "Từ ngày:";
            // 
            // dtTuNgay
            // 
            this.dtTuNgay.EditValue = null;
            this.dtTuNgay.EnterMoveNextControl = true;
            this.dtTuNgay.Location = new System.Drawing.Point(100, 101);
            this.dtTuNgay.Name = "dtTuNgay";
            this.dtTuNgay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtTuNgay.Properties.Appearance.ForeColor = System.Drawing.Color.DarkCyan;
            this.dtTuNgay.Properties.Appearance.Options.UseFont = true;
            this.dtTuNgay.Properties.Appearance.Options.UseForeColor = true;
            this.dtTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTuNgay.Properties.MaskSettings.Set("mask", "dd/MM/yyyy");
            this.dtTuNgay.Size = new System.Drawing.Size(172, 20);
            this.dtTuNgay.TabIndex = 17;
            this.dtTuNgay.EditValueChanged += new System.EventHandler(this.dtTuNgay_EditValueChanged);
            // 
            // dtDenNgay
            // 
            this.dtDenNgay.EditValue = null;
            this.dtDenNgay.EnterMoveNextControl = true;
            this.dtDenNgay.Location = new System.Drawing.Point(100, 144);
            this.dtDenNgay.Name = "dtDenNgay";
            this.dtDenNgay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtDenNgay.Properties.Appearance.ForeColor = System.Drawing.Color.DarkCyan;
            this.dtDenNgay.Properties.Appearance.Options.UseFont = true;
            this.dtDenNgay.Properties.Appearance.Options.UseForeColor = true;
            this.dtDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDenNgay.Properties.MaskSettings.Set("mask", "dd/MM/yyyy");
            this.dtDenNgay.Size = new System.Drawing.Size(172, 20);
            this.dtDenNgay.TabIndex = 19;
            this.dtDenNgay.EditValueChanged += new System.EventHandler(this.dtDenNgay_EditValueChanged);
            // 
            // lupMaKP
            // 
            this.lupMaKP.Location = new System.Drawing.Point(100, 12);
            this.lupMaKP.Name = "lupMaKP";
            this.lupMaKP.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupMaKP.Properties.Appearance.ForeColor = System.Drawing.Color.DarkCyan;
            this.lupMaKP.Properties.Appearance.Options.UseFont = true;
            this.lupMaKP.Properties.Appearance.Options.UseForeColor = true;
            this.lupMaKP.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupMaKP.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", "Tên phòng khám"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaKP", "Name2", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lupMaKP.Properties.DisplayMember = "TenKP";
            this.lupMaKP.Properties.NullText = "";
            this.lupMaKP.Properties.ValueMember = "MaKP";
            this.lupMaKP.Size = new System.Drawing.Size(172, 20);
            this.lupMaKP.TabIndex = 20;
            this.lupMaKP.EditValueChanged += new System.EventHandler(this.lupMaKP_EditValueChanged);
            // 
            // btnHuy
            // 
            this.btnHuy.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnHuy.Appearance.Options.UseFont = true;
            this.btnHuy.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnHuy.ImageOptions.Image")));
            this.btnHuy.Location = new System.Drawing.Point(231, 232);
            this.btnHuy.Margin = new System.Windows.Forms.Padding(4);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(68, 27);
            this.btnHuy.TabIndex = 82;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnInBC
            // 
            this.btnInBC.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnInBC.Appearance.Options.UseFont = true;
            this.btnInBC.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnInBC.ImageOptions.Image")));
            this.btnInBC.Location = new System.Drawing.Point(126, 232);
            this.btnInBC.Margin = new System.Windows.Forms.Padding(4);
            this.btnInBC.Name = "btnInBC";
            this.btnInBC.Size = new System.Drawing.Size(97, 27);
            this.btnInBC.TabIndex = 81;
            this.btnInBC.Text = "&In báo cáo";
            this.btnInBC.Click += new System.EventHandler(this.btnInBC_Click);
            // 
            // frm_BC_SoTiepDonBenhNhan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 272);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnInBC);
            this.Controls.Add(this.labelControl11);
            this.Controls.Add(this.cboDTuong);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl19);
            this.Controls.Add(this.labelControl18);
            this.Controls.Add(this.dtTuNgay);
            this.Controls.Add(this.dtDenNgay);
            this.Controls.Add(this.lupMaKP);
            this.Name = "frm_BC_SoTiepDonBenhNhan";
            this.Text = "Sổ tiếp đón bệnh nhân";
            this.Load += new System.EventHandler(this.frm_BC_SoTiepDonBenhNhan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cboDTuong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupMaKP.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LookUpEdit cboDTuong;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl19;
        private DevExpress.XtraEditors.LabelControl labelControl18;
        private DevExpress.XtraEditors.DateEdit dtTuNgay;
        private DevExpress.XtraEditors.DateEdit dtDenNgay;
        private DevExpress.XtraEditors.LookUpEdit lupMaKP;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnInBC;
    }
}