namespace QLBV.FormThamSo
{
    partial class Frm_SuaBC
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.chkE = new DevExpress.XtraEditors.CheckEdit();
            this.chkD = new DevExpress.XtraEditors.CheckEdit();
            this.chkB = new DevExpress.XtraEditors.CheckEdit();
            this.chkC = new DevExpress.XtraEditors.CheckEdit();
            this.chkA = new DevExpress.XtraEditors.CheckEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkB.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkA.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.chkE);
            this.groupControl1.Controls.Add(this.chkD);
            this.groupControl1.Controls.Add(this.chkB);
            this.groupControl1.Controls.Add(this.chkC);
            this.groupControl1.Controls.Add(this.chkA);
            this.groupControl1.Controls.Add(this.simpleButton1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(259, 163);
            this.groupControl1.TabIndex = 0;
            // 
            // chkE
            // 
            this.chkE.Location = new System.Drawing.Point(166, 61);
            this.chkE.Name = "chkE";
            this.chkE.Properties.Caption = "CLS";
            this.chkE.Size = new System.Drawing.Size(75, 19);
            this.chkE.TabIndex = 5;
            // 
            // chkD
            // 
            this.chkD.Location = new System.Drawing.Point(166, 36);
            this.chkD.Name = "chkD";
            this.chkD.Properties.Caption = "Tổng hợp";
            this.chkD.Size = new System.Drawing.Size(75, 19);
            this.chkD.TabIndex = 4;
            // 
            // chkB
            // 
            this.chkB.Location = new System.Drawing.Point(56, 61);
            this.chkB.Name = "chkB";
            this.chkB.Properties.Caption = "Viện phí ";
            this.chkB.Size = new System.Drawing.Size(75, 19);
            this.chkB.TabIndex = 3;
            // 
            // chkC
            // 
            this.chkC.Location = new System.Drawing.Point(56, 86);
            this.chkC.Name = "chkC";
            this.chkC.Properties.Caption = "BHYT";
            this.chkC.Size = new System.Drawing.Size(75, 19);
            this.chkC.TabIndex = 2;
            // 
            // chkA
            // 
            this.chkA.Location = new System.Drawing.Point(56, 36);
            this.chkA.Name = "chkA";
            this.chkA.Properties.Caption = "Khoa dược";
            this.chkA.Size = new System.Drawing.Size(75, 19);
            this.chkA.TabIndex = 1;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(86, 111);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(97, 27);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Lưu và thoát";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl1.Location = new System.Drawing.Point(50, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(179, 16);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "Chọn các nhóm của báo cáo";
            // 
            // Frm_SuaBC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 163);
            this.Controls.Add(this.groupControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_SuaBC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sửa loại báo cáo";
            this.Load += new System.EventHandler(this.Frm_SuaBC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkA.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.CheckEdit chkE;
        private DevExpress.XtraEditors.CheckEdit chkD;
        private DevExpress.XtraEditors.CheckEdit chkB;
        private DevExpress.XtraEditors.CheckEdit chkC;
        private DevExpress.XtraEditors.CheckEdit chkA;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}