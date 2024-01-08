namespace QLBV.FormNhap
{
    partial class Frm_NhapNgayDieuTri
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtSoNgayDT = new System.Windows.Forms.NumericUpDown();
            this.btnluu = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtSoNgayLT = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoNgayDT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoNgayLT)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Số ngày điều trị:";
            // 
            // txtSoNgayDT
            // 
            this.txtSoNgayDT.Location = new System.Drawing.Point(131, 11);
            this.txtSoNgayDT.Name = "txtSoNgayDT";
            this.txtSoNgayDT.Size = new System.Drawing.Size(182, 21);
            this.txtSoNgayDT.TabIndex = 2;
            this.txtSoNgayDT.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnluu
            // 
            this.btnluu.Location = new System.Drawing.Point(154, 64);
            this.btnluu.Name = "btnluu";
            this.btnluu.Size = new System.Drawing.Size(75, 23);
            this.btnluu.TabIndex = 3;
            this.btnluu.Text = "Lưu";
            this.btnluu.UseVisualStyleBackColor = true;
            this.btnluu.Click += new System.EventHandler(this.btnluu_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(238, 64);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Thoát";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtSoNgayLT
            // 
            this.txtSoNgayLT.Location = new System.Drawing.Point(131, 37);
            this.txtSoNgayLT.Name = "txtSoNgayLT";
            this.txtSoNgayLT.Size = new System.Drawing.Size(182, 21);
            this.txtSoNgayLT.TabIndex = 6;
            this.txtSoNgayLT.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtSoNgayLT.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Số ngày lấy thuốc:";
            this.label2.Visible = false;
            // 
            // Frm_NhapNgayDieuTri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 93);
            this.Controls.Add(this.txtSoNgayLT);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnluu);
            this.Controls.Add(this.txtSoNgayDT);
            this.Controls.Add(this.label1);
            this.Name = "Frm_NhapNgayDieuTri";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nhập ngày điều trị bệnh nhân ngoại trú";
            this.Load += new System.EventHandler(this.Frm_NhapNgayDieuTri_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtSoNgayDT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoNgayLT)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown txtSoNgayDT;
        private System.Windows.Forms.Button btnluu;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown txtSoNgayLT;
        private System.Windows.Forms.Label label2;
    }
}