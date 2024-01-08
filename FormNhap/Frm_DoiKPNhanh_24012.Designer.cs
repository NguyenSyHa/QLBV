
namespace QLBV.FormNhap
{
    partial class Frm_DoiKPNhanh_24012
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_DoiKPNhanh_24012));
            this.btnLuuKb = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lupMaKP = new DevExpress.XtraEditors.LookUpEdit();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.lupMaKP.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLuuKb
            // 
            this.btnLuuKb.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuuKb.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.btnLuuKb.Appearance.Options.UseFont = true;
            this.btnLuuKb.Appearance.Options.UseForeColor = true;
            this.btnLuuKb.ImageOptions.Image = global::QLBV.Properties.Resources.save_16x16;
            this.btnLuuKb.Location = new System.Drawing.Point(12, 83);
            this.btnLuuKb.Name = "btnLuuKb";
            this.btnLuuKb.Size = new System.Drawing.Size(111, 25);
            this.btnLuuKb.TabIndex = 4;
            this.btnLuuKb.Text = "&Lưu";
            this.btnLuuKb.Click += new System.EventHandler(this.btnLuuKb_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(12, 33);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 13);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "Bộ phận:";
            // 
            // lupMaKP
            // 
            this.lupMaKP.Location = new System.Drawing.Point(76, 30);
            this.lupMaKP.Name = "lupMaKP";
            this.lupMaKP.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lupMaKP.Properties.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.lupMaKP.Properties.Appearance.Options.UseFont = true;
            this.lupMaKP.Properties.Appearance.Options.UseForeColor = true;
            this.lupMaKP.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupMaKP.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKP", "Tên bộ phận"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaKP", "MaKP", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lupMaKP.Properties.DisplayMember = "TenKP";
            this.lupMaKP.Properties.NullText = "";
            this.lupMaKP.Properties.ValueMember = "MaKP";
            this.lupMaKP.Size = new System.Drawing.Size(180, 20);
            this.lupMaKP.TabIndex = 7;
            this.lupMaKP.Tag = "zzz";
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.Appearance.Options.UseForeColor = true;
            this.btnThoat.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnThoat.ImageOptions.Image")));
            this.btnThoat.Location = new System.Drawing.Point(140, 83);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(116, 25);
            this.btnThoat.TabIndex = 9;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // Frm_DoiKPNhanh_24012
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 127);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.lupMaKP);
            this.Controls.Add(this.btnLuuKb);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_DoiKPNhanh_24012";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đổi bộ phận nhanh";
            this.Load += new System.EventHandler(this.Frm_DoiKPNhanh_24012_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lupMaKP.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnLuuKb;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LookUpEdit lupMaKP;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
    }
}