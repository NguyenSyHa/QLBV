namespace QLBV.FormThamSo
{
    partial class frm_UpdateDVct
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
            this.btnUpdatePhauThuat = new DevExpress.XtraEditors.SimpleButton();
            this.cboTenRG = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.cboTenRG.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUpdatePhauThuat
            // 
            this.btnUpdatePhauThuat.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdatePhauThuat.Appearance.Options.UseFont = true;
            this.btnUpdatePhauThuat.Location = new System.Drawing.Point(55, 105);
            this.btnUpdatePhauThuat.Name = "btnUpdatePhauThuat";
            this.btnUpdatePhauThuat.Size = new System.Drawing.Size(96, 24);
            this.btnUpdatePhauThuat.TabIndex = 1;
            this.btnUpdatePhauThuat.Text = "Update";
            this.btnUpdatePhauThuat.Click += new System.EventHandler(this.btnUpdatePhauThuat_Click);
            // 
            // cboTenRG
            // 
            this.cboTenRG.EditValue = "Siêu âm";
            this.cboTenRG.Location = new System.Drawing.Point(55, 44);
            this.cboTenRG.Name = "cboTenRG";
            this.cboTenRG.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTenRG.Properties.Appearance.Options.UseFont = true;
            this.cboTenRG.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTenRG.Properties.Items.AddRange(new object[] {
            "Siêu âm",
            "X-Quang",
            "X-Quang CT",
            "Điện Tim",
            "Nội soi",
            "Nội soi Tai-Mũi-Họng",
            "Phẫu thuật",
            "Thủ thuật",
            "Chức năng hô hấp",
            "Đo khúc xạ máy"});
            this.cboTenRG.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboTenRG.Size = new System.Drawing.Size(259, 22);
            this.cboTenRG.TabIndex = 0;
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.Location = new System.Drawing.Point(218, 105);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(96, 24);
            this.btnThoat.TabIndex = 2;
            this.btnThoat.Text = "&Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // frm_UpdateDVct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 197);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.cboTenRG);
            this.Controls.Add(this.btnUpdatePhauThuat);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_UpdateDVct";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "update dịch vụ sang dịch vụ chi tiết";
            ((System.ComponentModel.ISupportInitialize)(this.cboTenRG.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnUpdatePhauThuat;
        private DevExpress.XtraEditors.ComboBoxEdit cboTenRG;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
    }
}