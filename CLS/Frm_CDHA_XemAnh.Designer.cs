namespace QLBV.CLS
{
    partial class frm_CDHA_XemAnh
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
            this.ptAnh = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.ptAnh.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ptAnh
            // 
            this.ptAnh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ptAnh.Location = new System.Drawing.Point(0, 0);
            this.ptAnh.Name = "ptAnh";
            this.ptAnh.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.ptAnh.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.ptAnh.Size = new System.Drawing.Size(942, 568);
            this.ptAnh.TabIndex = 0;
            // 
            // frm_CDHA_XemAnh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 568);
            this.Controls.Add(this.ptAnh);
            this.Name = "frm_CDHA_XemAnh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ảnh";
            this.Load += new System.EventHandler(this.Ảnh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ptAnh.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit ptAnh;
    }
}