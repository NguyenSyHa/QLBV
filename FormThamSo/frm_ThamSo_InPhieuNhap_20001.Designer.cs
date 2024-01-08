namespace QLBV.FormThamSo
{
    partial class frm_ThamSo_InPhieuNhap_20001
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ThamSo_InPhieuNhap_20001));
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.lupTV4 = new DevExpress.XtraEditors.LookUpEdit();
            this.txtCanBo = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.lupTV4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCanBo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.Image = ((System.Drawing.Image)(resources.GetObject("btnThoat.Image")));
            this.btnThoat.Location = new System.Drawing.Point(149, 84);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(92, 26);
            this.btnThoat.TabIndex = 2;
            this.btnThoat.Text = "&Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(43, 84);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 26);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "Đồng ý";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lupTV4
            // 
            this.lupTV4.Location = new System.Drawing.Point(43, 28);
            this.lupTV4.Name = "lupTV4";
            this.lupTV4.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupTV4.Properties.Appearance.Options.UseFont = true;
            this.lupTV4.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupTV4.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenCB", 100, "Tên cán bộ"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaCB", 50, "Mã cán bộ")});
            this.lupTV4.Properties.DisplayMember = "TenCB";
            this.lupTV4.Properties.NullText = "";
            this.lupTV4.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lupTV4.Properties.ValueMember = "MaCB";
            this.lupTV4.Size = new System.Drawing.Size(198, 22);
            this.lupTV4.TabIndex = 0;
            // 
            // txtCanBo
            // 
            this.txtCanBo.Location = new System.Drawing.Point(43, 28);
            this.txtCanBo.Name = "txtCanBo";
            this.txtCanBo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCanBo.Properties.Appearance.Options.UseFont = true;
            this.txtCanBo.Size = new System.Drawing.Size(198, 20);
            this.txtCanBo.TabIndex = 3;
            // 
            // frm_ThamSo_InPhieuNhap_20001
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 122);
            this.Controls.Add(this.txtCanBo);
            this.Controls.Add(this.lupTV4);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_ThamSo_InPhieuNhap_20001";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cán bộ thống kê dược";
            this.Load += new System.EventHandler(this.frm_ThamSo_InPhieuNhap_20001_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupTV4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCanBo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.LookUpEdit lupTV4;
        private DevExpress.XtraEditors.TextEdit txtCanBo;
    }
}