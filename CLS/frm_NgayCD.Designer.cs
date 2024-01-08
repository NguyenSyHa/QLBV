namespace QLBV.CLS
{
    partial class frm_NgayCD
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
            this.btn_cdNgay = new System.Windows.Forms.Button();
            this.dtCD = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dtCD.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtCD.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 44);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ngày chỉ định";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_cdNgay
            // 
            this.btn_cdNgay.Location = new System.Drawing.Point(111, 94);
            this.btn_cdNgay.Name = "btn_cdNgay";
            this.btn_cdNgay.Size = new System.Drawing.Size(75, 23);
            this.btn_cdNgay.TabIndex = 2;
            this.btn_cdNgay.Text = "In phiếu";
            this.btn_cdNgay.UseVisualStyleBackColor = true;
            this.btn_cdNgay.Click += new System.EventHandler(this.btn_cdNgay_Click);
            // 
            // dtCD
            // 
            this.dtCD.EditValue = null;
            this.dtCD.EnterMoveNextControl = true;
            this.dtCD.Location = new System.Drawing.Point(93, 42);
            this.dtCD.Name = "dtCD";
            this.dtCD.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtCD.Properties.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.dtCD.Properties.Appearance.Options.UseFont = true;
            this.dtCD.Properties.Appearance.Options.UseForeColor = true;
            this.dtCD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtCD.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtCD.Properties.NullText = "Từ ngày";
            this.dtCD.Size = new System.Drawing.Size(188, 22);
            this.dtCD.TabIndex = 3;
            // 
            // frm_NgayCD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 129);
            this.Controls.Add(this.dtCD);
            this.Controls.Add(this.btn_cdNgay);
            this.Controls.Add(this.label1);
            this.Name = "frm_NgayCD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn ngày in chỉ định";
            this.Load += new System.EventHandler(this.frm_NgayCD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtCD.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtCD.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_cdNgay;
        private DevExpress.XtraEditors.DateEdit dtCD;
    }
}