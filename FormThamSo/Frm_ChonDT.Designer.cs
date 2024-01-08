
namespace QLBV.FormThamSo
{
    partial class Frm_ChonDT
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
            this.rdb_DTThuong = new System.Windows.Forms.RadioButton();
            this.rdb_DTT04 = new System.Windows.Forms.RadioButton();
            this.rdb_DTN04 = new System.Windows.Forms.RadioButton();
            this.rdb_DTTT04 = new System.Windows.Forms.RadioButton();
            this.btn_In = new System.Windows.Forms.Button();
            this.btn_Thoat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rdb_DTThuong
            // 
            this.rdb_DTThuong.AutoSize = true;
            this.rdb_DTThuong.Location = new System.Drawing.Point(12, 12);
            this.rdb_DTThuong.Name = "rdb_DTThuong";
            this.rdb_DTThuong.Size = new System.Drawing.Size(111, 17);
            this.rdb_DTThuong.TabIndex = 0;
            this.rdb_DTThuong.TabStop = true;
            this.rdb_DTThuong.Text = "Đơn thuốc thường";
            this.rdb_DTThuong.UseVisualStyleBackColor = true;
            // 
            // rdb_DTT04
            // 
            this.rdb_DTT04.AutoSize = true;
            this.rdb_DTT04.Location = new System.Drawing.Point(12, 35);
            this.rdb_DTT04.Name = "rdb_DTT04";
            this.rdb_DTT04.Size = new System.Drawing.Size(143, 17);
            this.rdb_DTT04.TabIndex = 1;
            this.rdb_DTT04.TabStop = true;
            this.rdb_DTT04.Text = "Đơn thuốc thường(TT04)";
            this.rdb_DTT04.UseVisualStyleBackColor = true;
            // 
            // rdb_DTN04
            // 
            this.rdb_DTN04.AutoSize = true;
            this.rdb_DTN04.Location = new System.Drawing.Point(12, 58);
            this.rdb_DTN04.Name = "rdb_DTN04";
            this.rdb_DTN04.Size = new System.Drawing.Size(162, 17);
            this.rdb_DTN04.TabIndex = 2;
            this.rdb_DTN04.TabStop = true;
            this.rdb_DTN04.Text = "Đơn thuốc gây nghiện(TT04)";
            this.rdb_DTN04.UseVisualStyleBackColor = true;
            // 
            // rdb_DTTT04
            // 
            this.rdb_DTTT04.AutoSize = true;
            this.rdb_DTTT04.Location = new System.Drawing.Point(12, 81);
            this.rdb_DTTT04.Name = "rdb_DTTT04";
            this.rdb_DTTT04.Size = new System.Drawing.Size(184, 17);
            this.rdb_DTTT04.TabIndex = 3;
            this.rdb_DTTT04.TabStop = true;
            this.rdb_DTTT04.Text = "Đơn thuốc hướng tâm thần(TT04)";
            this.rdb_DTTT04.UseVisualStyleBackColor = true;
            // 
            // btn_In
            // 
            this.btn_In.Image = global::QLBV.Properties.Resources.preview_16x16;
            this.btn_In.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_In.Location = new System.Drawing.Point(216, 26);
            this.btn_In.Name = "btn_In";
            this.btn_In.Size = new System.Drawing.Size(75, 26);
            this.btn_In.TabIndex = 4;
            this.btn_In.Text = "IN";
            this.btn_In.UseVisualStyleBackColor = true;
            this.btn_In.Click += new System.EventHandler(this.btn_In_Click);
            // 
            // btn_Thoat
            // 
            this.btn_Thoat.Image = global::QLBV.Properties.Resources.cancel_16x16;
            this.btn_Thoat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Thoat.Location = new System.Drawing.Point(216, 54);
            this.btn_Thoat.Name = "btn_Thoat";
            this.btn_Thoat.Size = new System.Drawing.Size(75, 26);
            this.btn_Thoat.TabIndex = 5;
            this.btn_Thoat.Text = "Thoát";
            this.btn_Thoat.UseVisualStyleBackColor = true;
            this.btn_Thoat.Click += new System.EventHandler(this.btn_Thoat_Click);
            // 
            // Frm_ChonDT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 111);
            this.Controls.Add(this.btn_Thoat);
            this.Controls.Add(this.btn_In);
            this.Controls.Add(this.rdb_DTTT04);
            this.Controls.Add(this.rdb_DTN04);
            this.Controls.Add(this.rdb_DTT04);
            this.Controls.Add(this.rdb_DTThuong);
            this.Name = "Frm_ChonDT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn đơn thuốc";
            this.Load += new System.EventHandler(this.Frm_ChonDT_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdb_DTThuong;
        private System.Windows.Forms.RadioButton rdb_DTT04;
        private System.Windows.Forms.RadioButton rdb_DTN04;
        private System.Windows.Forms.RadioButton rdb_DTTT04;
        private System.Windows.Forms.Button btn_In;
        private System.Windows.Forms.Button btn_Thoat;
    }
}