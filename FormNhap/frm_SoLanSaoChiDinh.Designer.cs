namespace QLBV.FormNhap
{
    partial class frm_SoLanSaoChiDinh
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_SoLanSaoChiDinh));
            this.spSoLan = new DevExpress.XtraEditors.SpinEdit();
            this.lblSoLan = new DevExpress.XtraEditors.LabelControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.spSoLan.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // spSoLan
            // 
            this.spSoLan.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spSoLan.Location = new System.Drawing.Point(63, 13);
            this.spSoLan.Name = "spSoLan";
            this.spSoLan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spSoLan.Properties.Mask.EditMask = "d";
            this.spSoLan.Properties.MaxValue = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.spSoLan.Size = new System.Drawing.Size(141, 20);
            this.spSoLan.TabIndex = 0;
            // 
            // lblSoLan
            // 
            this.lblSoLan.Location = new System.Drawing.Point(12, 16);
            this.lblSoLan.Name = "lblSoLan";
            this.lblSoLan.Size = new System.Drawing.Size(33, 13);
            this.lblSoLan.TabIndex = 1;
            this.lblSoLan.Text = "Số lần:";
            // 
            // btnOK
            // 
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(12, 39);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(95, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "Đồng ý";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(113, 39);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frm_SoLanSaoChiDinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(216, 68);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblSoLan);
            this.Controls.Add(this.spSoLan);
            this.Name = "frm_SoLanSaoChiDinh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nhập số lần sao";
            this.Load += new System.EventHandler(this.frm_SoLanSaoChiDinh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.spSoLan.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SpinEdit spSoLan;
        private DevExpress.XtraEditors.LabelControl lblSoLan;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}