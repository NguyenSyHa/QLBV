namespace QLBV.FormThamSo
{
    partial class frm_BCTaiNanLaoDong_30010
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
            this.luptungay = new DevExpress.XtraEditors.DateEdit();
            this.lupdenngay = new DevExpress.XtraEditors.DateEdit();
            this.btntaobc = new DevExpress.XtraEditors.SimpleButton();
            this.btnhuy = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.luptungay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luptungay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupdenngay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupdenngay.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(12, 26);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(61, 19);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Từ ngày:";
            this.labelControl1.Click += new System.EventHandler(this.labelControl1_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(12, 57);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(69, 19);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // luptungay
            // 
            this.luptungay.EditValue = null;
            this.luptungay.Location = new System.Drawing.Point(91, 23);
            this.luptungay.Name = "luptungay";
            this.luptungay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.luptungay.Properties.Appearance.Options.UseFont = true;
            this.luptungay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luptungay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luptungay.Size = new System.Drawing.Size(181, 26);
            this.luptungay.TabIndex = 1;
            // 
            // lupdenngay
            // 
            this.lupdenngay.EditValue = null;
            this.lupdenngay.Location = new System.Drawing.Point(91, 57);
            this.lupdenngay.Name = "lupdenngay";
            this.lupdenngay.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lupdenngay.Properties.Appearance.Options.UseFont = true;
            this.lupdenngay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupdenngay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lupdenngay.Size = new System.Drawing.Size(181, 26);
            this.lupdenngay.TabIndex = 1;
            // 
            // btntaobc
            // 
            this.btntaobc.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btntaobc.Appearance.Options.UseFont = true;
            this.btntaobc.Image = global::QLBV.Properties.Resources.apply_16x16;
            this.btntaobc.Location = new System.Drawing.Point(91, 99);
            this.btntaobc.Name = "btntaobc";
            this.btntaobc.Size = new System.Drawing.Size(82, 23);
            this.btntaobc.TabIndex = 2;
            this.btntaobc.Text = "Tạo BC";
            this.btntaobc.Click += new System.EventHandler(this.btntaobc_Click);
            // 
            // btnhuy
            // 
            this.btnhuy.Appearance.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnhuy.Appearance.Options.UseFont = true;
            this.btnhuy.Image = global::QLBV.Properties.Resources.cancel_16x16;
            this.btnhuy.Location = new System.Drawing.Point(197, 99);
            this.btnhuy.Name = "btnhuy";
            this.btnhuy.Size = new System.Drawing.Size(75, 23);
            this.btnhuy.TabIndex = 2;
            this.btnhuy.Text = "Hủy";
            this.btnhuy.Click += new System.EventHandler(this.btnhuy_Click);
            // 
            // frm_BCTaiNanLaoDong_30010
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 133);
            this.Controls.Add(this.btnhuy);
            this.Controls.Add(this.btntaobc);
            this.Controls.Add(this.lupdenngay);
            this.Controls.Add(this.luptungay);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frm_BCTaiNanLaoDong_30010";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BC tai nạn lao động";
            this.Load += new System.EventHandler(this.frm_BCTaiNanLaoDong_30010_Load);
            ((System.ComponentModel.ISupportInitialize)(this.luptungay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luptungay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupdenngay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lupdenngay.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit luptungay;
        private DevExpress.XtraEditors.DateEdit lupdenngay;
        private DevExpress.XtraEditors.SimpleButton btntaobc;
        private DevExpress.XtraEditors.SimpleButton btnhuy;
    }
}