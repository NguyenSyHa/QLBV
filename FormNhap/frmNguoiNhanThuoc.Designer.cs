namespace QLBV.FormNhap
{
    partial class frmNguoiNhanThuoc
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
            this.txtNguoiNhanThuoc = new DevExpress.XtraEditors.TextEdit();
            this.txtSoCMND = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtNguoiNhanThuoc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoCMND.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNguoiNhanThuoc
            // 
            this.txtNguoiNhanThuoc.Location = new System.Drawing.Point(120, 12);
            this.txtNguoiNhanThuoc.Name = "txtNguoiNhanThuoc";
            this.txtNguoiNhanThuoc.Properties.MaxLength = 50;
            this.txtNguoiNhanThuoc.Size = new System.Drawing.Size(152, 20);
            this.txtNguoiNhanThuoc.TabIndex = 0;
            // 
            // txtSoCMND
            // 
            this.txtSoCMND.Location = new System.Drawing.Point(120, 38);
            this.txtSoCMND.Name = "txtSoCMND";
            this.txtSoCMND.Properties.Mask.EditMask = "d";
            this.txtSoCMND.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtSoCMND.Properties.MaxLength = 12;
            this.txtSoCMND.Size = new System.Drawing.Size(152, 20);
            this.txtSoCMND.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Location = new System.Drawing.Point(12, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(101, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Người nhận thuốc:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Location = new System.Drawing.Point(12, 41);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(52, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Số CMND:";
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Image = global::QLBV.Properties.Resources.apply_16x16;
            this.btnSave.Location = new System.Drawing.Point(197, 64);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 32);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmNguoiNhanThuoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 101);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtSoCMND);
            this.Controls.Add(this.txtNguoiNhanThuoc);
            this.Name = "frmNguoiNhanThuoc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Người nhận thuốc";
            this.Load += new System.EventHandler(this.frmNguoiNhanThuoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtNguoiNhanThuoc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoCMND.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtNguoiNhanThuoc;
        private DevExpress.XtraEditors.TextEdit txtSoCMND;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnSave;
    }
}