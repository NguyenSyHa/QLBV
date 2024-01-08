namespace QLBV.FormThamSo
{
    partial class Frm_BCHoatDongPK_cs2_01049
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.BtnReport = new DevExpress.XtraEditors.SimpleButton();
            this.txttungay = new System.Windows.Forms.DateTimePicker();
            this.txtdenngay = new System.Windows.Forms.DateTimePicker();
            this.ckcInDoc = new System.Windows.Forms.CheckBox();
            this.ckcInNgang = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl1.Location = new System.Drawing.Point(27, 28);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(50, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl2.Location = new System.Drawing.Point(27, 60);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(56, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // BtnReport
            // 
            this.BtnReport.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.BtnReport.Appearance.Options.UseFont = true;
            this.BtnReport.Location = new System.Drawing.Point(27, 122);
            this.BtnReport.Name = "BtnReport";
            this.BtnReport.Size = new System.Drawing.Size(248, 32);
            this.BtnReport.TabIndex = 4;
            this.BtnReport.Text = "Lấy báo cáo";
            this.BtnReport.Click += new System.EventHandler(this.BtnReport_Click);
            // 
            // txttungay
            // 
            this.txttungay.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txttungay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txttungay.Location = new System.Drawing.Point(103, 21);
            this.txttungay.Name = "txttungay";
            this.txttungay.Size = new System.Drawing.Size(172, 24);
            this.txttungay.TabIndex = 0;
            this.txttungay.Value = new System.DateTime(2019, 10, 25, 0, 0, 0, 0);
            // 
            // txtdenngay
            // 
            this.txtdenngay.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtdenngay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtdenngay.Location = new System.Drawing.Point(103, 53);
            this.txtdenngay.Name = "txtdenngay";
            this.txtdenngay.Size = new System.Drawing.Size(172, 24);
            this.txtdenngay.TabIndex = 1;
            this.txtdenngay.Value = new System.DateTime(2019, 10, 25, 0, 0, 0, 0);
            // 
            // ckcInDoc
            // 
            this.ckcInDoc.AutoSize = true;
            this.ckcInDoc.Checked = true;
            this.ckcInDoc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckcInDoc.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ckcInDoc.Location = new System.Drawing.Point(27, 91);
            this.ckcInDoc.Name = "ckcInDoc";
            this.ckcInDoc.Size = new System.Drawing.Size(85, 18);
            this.ckcInDoc.TabIndex = 2;
            this.ckcInDoc.Text = "Mẫu in dọc";
            this.ckcInDoc.UseVisualStyleBackColor = true;
            this.ckcInDoc.Visible = false;
            this.ckcInDoc.CheckedChanged += new System.EventHandler(this.ckcInDoc_CheckedChanged);
            // 
            // ckcInNgang
            // 
            this.ckcInNgang.AutoSize = true;
            this.ckcInNgang.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ckcInNgang.Location = new System.Drawing.Point(176, 91);
            this.ckcInNgang.Name = "ckcInNgang";
            this.ckcInNgang.Size = new System.Drawing.Size(99, 18);
            this.ckcInNgang.TabIndex = 3;
            this.ckcInNgang.Text = "Mẫu in ngang";
            this.ckcInNgang.UseVisualStyleBackColor = true;
            this.ckcInNgang.Visible = false;
            this.ckcInNgang.CheckedChanged += new System.EventHandler(this.ckcInNgang_CheckedChanged);
            // 
            // Frm_BCHoatDongPK_cs2_01049
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 180);
            this.Controls.Add(this.ckcInNgang);
            this.Controls.Add(this.ckcInDoc);
            this.Controls.Add(this.txtdenngay);
            this.Controls.Add(this.txttungay);
            this.Controls.Add(this.BtnReport);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_BCHoatDongPK_cs2_01049";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo hoạt động phòng khám";
            this.Load += new System.EventHandler(this.Frm_BCHoatDongPK_cs2_01049_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton BtnReport;
        private System.Windows.Forms.DateTimePicker txttungay;
        private System.Windows.Forms.DateTimePicker txtdenngay;
        private System.Windows.Forms.CheckBox ckcInDoc;
        private System.Windows.Forms.CheckBox ckcInNgang;
    }
}