namespace QLBV.FormThamSo
{
    partial class frm_Baocaosolieutheongay_14018
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
            this.components = new System.ComponentModel.Container();
            this.BtnBaoCao = new DevExpress.XtraEditors.SimpleButton();
            this.chk = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.txtTuNgay = new System.Windows.Forms.DateTimePicker();
            this.txtdenngay = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnBaoCao
            // 
            this.BtnBaoCao.Location = new System.Drawing.Point(339, 114);
            this.BtnBaoCao.Name = "BtnBaoCao";
            this.BtnBaoCao.Size = new System.Drawing.Size(75, 34);
            this.BtnBaoCao.TabIndex = 1;
            this.BtnBaoCao.Text = "Báo cáo";
            this.BtnBaoCao.Click += new System.EventHandler(this.BtnBaoCao_Click);
            // 
            // chk
            // 
            this.chk.AutoSize = true;
            this.chk.Location = new System.Drawing.Point(166, 123);
            this.chk.Name = "chk";
            this.chk.Size = new System.Drawing.Size(150, 17);
            this.chk.TabIndex = 2;
            this.chk.Text = "Dịch vụ thực hiện tại khoa";
            this.chk.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(28, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(465, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "BÁO CÁO TỔNG HỢP SỐ LIỆU DỊCH VỤ THEO NGÀY/THEO KHOA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Từ ngày:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Đến ngày:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(418, 113);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(75, 35);
            this.btnHuy.TabIndex = 5;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // txtTuNgay
            // 
            this.txtTuNgay.CustomFormat = "dd/MM/yyyy";
            this.txtTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtTuNgay.Location = new System.Drawing.Point(102, 49);
            this.txtTuNgay.Name = "txtTuNgay";
            this.txtTuNgay.Size = new System.Drawing.Size(389, 21);
            this.txtTuNgay.TabIndex = 6;
            this.txtTuNgay.Validating += new System.ComponentModel.CancelEventHandler(this.txtTuNgay_Validating);
            // 
            // txtdenngay
            // 
            this.txtdenngay.CustomFormat = "dd/MM/yyyy";
            this.txtdenngay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtdenngay.Location = new System.Drawing.Point(102, 75);
            this.txtdenngay.Name = "txtdenngay";
            this.txtdenngay.Size = new System.Drawing.Size(389, 21);
            this.txtdenngay.TabIndex = 6;
            this.txtdenngay.Validating += new System.ComponentModel.CancelEventHandler(this.txtdenngay_Validating);
            // 
            // frm_Baocaosolieutheongay_14018
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 161);
            this.Controls.Add(this.txtdenngay);
            this.Controls.Add(this.txtTuNgay);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chk);
            this.Controls.Add(this.BtnBaoCao);
            this.Name = "frm_Baocaosolieutheongay_14018";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo số liệu";
            this.Load += new System.EventHandler(this.frm_Baocaosolieutheongay_14018_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton BtnBaoCao;
        private System.Windows.Forms.CheckBox chk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private System.Windows.Forms.DateTimePicker txtTuNgay;
        private System.Windows.Forms.DateTimePicker txtdenngay;
    }
}