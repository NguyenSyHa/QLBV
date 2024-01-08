namespace QLBV.FormThamSo
{
    partial class frm_TTNgThanTreEm
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
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtTenBo = new DevExpress.XtraEditors.TextEdit();
            this.txtCMT = new DevExpress.XtraEditors.TextEdit();
            this.txtSDT = new DevExpress.XtraEditors.TextEdit();
            this.txtDChi = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnhuy = new DevExpress.XtraEditors.SimpleButton();
            this.txtTenMe = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenBo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCMT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSDT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDChi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenMe.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(4, 9);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(51, 19);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Tên bố:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(4, 70);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(90, 19);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "CMT|CCCD:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(4, 100);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(36, 19);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "SĐT:";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(4, 130);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(96, 19);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "Nơi ở hiện tại:";
            // 
            // txtTenBo
            // 
            this.txtTenBo.Location = new System.Drawing.Point(106, 6);
            this.txtTenBo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTenBo.Name = "txtTenBo";
            this.txtTenBo.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenBo.Properties.Appearance.Options.UseFont = true;
            this.txtTenBo.Size = new System.Drawing.Size(353, 26);
            this.txtTenBo.TabIndex = 0;
            this.txtTenBo.Leave += new System.EventHandler(this.txtTen_Leave);
            // 
            // txtCMT
            // 
            this.txtCMT.Location = new System.Drawing.Point(106, 67);
            this.txtCMT.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCMT.Name = "txtCMT";
            this.txtCMT.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCMT.Properties.Appearance.Options.UseFont = true;
            this.txtCMT.Properties.Mask.EditMask = "d";
            this.txtCMT.Size = new System.Drawing.Size(353, 26);
            this.txtCMT.TabIndex = 1;
            // 
            // txtSDT
            // 
            this.txtSDT.Location = new System.Drawing.Point(106, 97);
            this.txtSDT.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSDT.Properties.Appearance.Options.UseFont = true;
            this.txtSDT.Properties.Mask.EditMask = "d";
            this.txtSDT.Size = new System.Drawing.Size(353, 26);
            this.txtSDT.TabIndex = 2;
            // 
            // txtDChi
            // 
            this.txtDChi.Location = new System.Drawing.Point(106, 127);
            this.txtDChi.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDChi.Name = "txtDChi";
            this.txtDChi.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDChi.Properties.Appearance.Options.UseFont = true;
            this.txtDChi.Size = new System.Drawing.Size(353, 26);
            this.txtDChi.TabIndex = 3;
            this.txtDChi.Leave += new System.EventHandler(this.txtDChi_Leave);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(301, 161);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(74, 28);
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "Lưu";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btnhuy
            // 
            this.btnhuy.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnhuy.Appearance.Options.UseFont = true;
            this.btnhuy.Location = new System.Drawing.Point(381, 161);
            this.btnhuy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnhuy.Name = "btnhuy";
            this.btnhuy.Size = new System.Drawing.Size(72, 28);
            this.btnhuy.TabIndex = 5;
            this.btnhuy.Text = "Thoát";
            this.btnhuy.Click += new System.EventHandler(this.btnhuy_Click);
            // 
            // txtTenMe
            // 
            this.txtTenMe.Location = new System.Drawing.Point(106, 36);
            this.txtTenMe.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTenMe.Name = "txtTenMe";
            this.txtTenMe.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenMe.Properties.Appearance.Options.UseFont = true;
            this.txtTenMe.Size = new System.Drawing.Size(353, 26);
            this.txtTenMe.TabIndex = 6;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Location = new System.Drawing.Point(4, 39);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(55, 19);
            this.labelControl5.TabIndex = 7;
            this.labelControl5.Text = "Tên mẹ:";
            // 
            // frm_TTNgThanTreEm
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 190);
            this.Controls.Add(this.txtTenMe);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.btnhuy);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.txtDChi);
            this.Controls.Add(this.txtSDT);
            this.Controls.Add(this.txtCMT);
            this.Controls.Add(this.txtTenBo);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frm_TTNgThanTreEm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin người thân bệnh nhân dưới 6 tuổi";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_TTNgThanTreEm_FormClosing);
            this.Load += new System.EventHandler(this.frm_TTNgThanTreEm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtTenBo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCMT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSDT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDChi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenMe.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtTenBo;
        private DevExpress.XtraEditors.TextEdit txtCMT;
        private DevExpress.XtraEditors.TextEdit txtSDT;
        private DevExpress.XtraEditors.TextEdit txtDChi;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton btnhuy;
        private DevExpress.XtraEditors.TextEdit txtTenMe;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}