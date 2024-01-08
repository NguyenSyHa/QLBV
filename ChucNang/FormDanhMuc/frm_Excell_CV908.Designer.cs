namespace QLBV.FormDanhMuc
{
    partial class frm_Excell_CV908
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
            this.cklNhom = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.rad_mau = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.cklNhom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rad_mau.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cklNhom
            // 
            this.cklNhom.CheckOnClick = true;
            this.cklNhom.DisplayMember = "TenNhomCT";
            this.cklNhom.Location = new System.Drawing.Point(33, 25);
            this.cklNhom.MultiColumn = true;
            this.cklNhom.Name = "cklNhom";
            this.cklNhom.Size = new System.Drawing.Size(379, 105);
            this.cklNhom.TabIndex = 2;
            this.cklNhom.ValueMember = "IDNhom";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(333, 172);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(78, 23);
            this.simpleButton1.TabIndex = 7;
            this.simpleButton1.Text = "&Thoát";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.Location = new System.Drawing.Point(206, 172);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(121, 23);
            this.simpleButton2.TabIndex = 8;
            this.simpleButton2.Text = "Xuất Excell CV908";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // rad_mau
            // 
            this.rad_mau.Location = new System.Drawing.Point(33, 136);
            this.rad_mau.Name = "rad_mau";
            this.rad_mau.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Mẫu Bảng 1(Thuốc)"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Mẫu Bảng 2(VTYT)")});
            this.rad_mau.Size = new System.Drawing.Size(378, 23);
            this.rad_mau.TabIndex = 9;
            // 
            // frm_Excell_CV908
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 224);
            this.Controls.Add(this.rad_mau);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.cklNhom);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Excell_CV908";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_Excell_CV908";
            this.Load += new System.EventHandler(this.frm_Excell_CV908_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cklNhom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rad_mau.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.CheckedListBoxControl cklNhom;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.RadioGroup rad_mau;
    }
}