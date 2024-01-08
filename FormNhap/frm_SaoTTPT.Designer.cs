namespace QLBV.FormNhap
{
    partial class frm_SaoTTPT
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.dtNgayCD = new DevExpress.XtraEditors.DateEdit();
            this.chkSaoKQ = new DevExpress.XtraEditors.CheckEdit();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSao = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboTyLe = new DevExpress.XtraEditors.ComboBoxEdit();
            this.dtNgayKQ = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayCD.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayCD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSaoKQ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTyLe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayKQ.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayKQ.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.dtNgayCD);
            this.panelControl1.Controls.Add(this.chkSaoKQ);
            this.panelControl1.Controls.Add(this.btnThoat);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.btnSao);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.cboTyLe);
            this.panelControl1.Controls.Add(this.dtNgayKQ);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(339, 181);
            this.panelControl1.TabIndex = 0;
            // 
            // dtNgayCD
            // 
            this.dtNgayCD.EditValue = null;
            this.dtNgayCD.Location = new System.Drawing.Point(119, 12);
            this.dtNgayCD.Name = "dtNgayCD";
            this.dtNgayCD.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtNgayCD.Properties.Appearance.Options.UseFont = true;
            this.dtNgayCD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtNgayCD.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtNgayCD.Properties.DisplayFormat.FormatString = "g";
            this.dtNgayCD.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtNgayCD.Properties.EditFormat.FormatString = "g";
            this.dtNgayCD.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtNgayCD.Properties.Mask.EditMask = "g";
            this.dtNgayCD.Size = new System.Drawing.Size(204, 22);
            this.dtNgayCD.TabIndex = 3;
            // 
            // chkSaoKQ
            // 
            this.chkSaoKQ.Location = new System.Drawing.Point(15, 34);
            this.chkSaoKQ.Name = "chkSaoKQ";
            this.chkSaoKQ.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSaoKQ.Properties.Appearance.Options.UseFont = true;
            this.chkSaoKQ.Properties.Caption = "Sao kết quả";
            this.chkSaoKQ.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.chkSaoKQ.Size = new System.Drawing.Size(121, 20);
            this.chkSaoKQ.TabIndex = 2;
            this.chkSaoKQ.CheckedChanged += new System.EventHandler(this.chkSaoKQ_CheckedChanged);
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.Location = new System.Drawing.Point(248, 123);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(75, 34);
            this.btnThoat.TabIndex = 1;
            this.btnThoat.Text = "&Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Tỷ lệ";
            // 
            // btnSao
            // 
            this.btnSao.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSao.Appearance.Options.UseFont = true;
            this.btnSao.Location = new System.Drawing.Point(173, 123);
            this.btnSao.Name = "btnSao";
            this.btnSao.Size = new System.Drawing.Size(75, 34);
            this.btnSao.TabIndex = 0;
            this.btnSao.Text = "&Sao";
            this.btnSao.Click += new System.EventHandler(this.btnSao_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Ngày chỉ định";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Ngày thực hiện";
            // 
            // cboTyLe
            // 
            this.cboTyLe.EditValue = "100";
            this.cboTyLe.Location = new System.Drawing.Point(119, 82);
            this.cboTyLe.Name = "cboTyLe";
            this.cboTyLe.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTyLe.Properties.MaxLength = 3;
            this.cboTyLe.Size = new System.Drawing.Size(204, 20);
            this.cboTyLe.TabIndex = 7;
            this.cboTyLe.SelectedValueChanged += new System.EventHandler(this.cboTyLe_SelectedValueChanged);
            // 
            // dtNgayKQ
            // 
            this.dtNgayKQ.EditValue = null;
            this.dtNgayKQ.Location = new System.Drawing.Point(119, 54);
            this.dtNgayKQ.Name = "dtNgayKQ";
            this.dtNgayKQ.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtNgayKQ.Properties.Appearance.Options.UseFont = true;
            this.dtNgayKQ.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtNgayKQ.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtNgayKQ.Properties.DisplayFormat.FormatString = "g";
            this.dtNgayKQ.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtNgayKQ.Properties.EditFormat.FormatString = "g";
            this.dtNgayKQ.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtNgayKQ.Properties.Mask.EditMask = "g";
            this.dtNgayKQ.Size = new System.Drawing.Size(204, 22);
            this.dtNgayKQ.TabIndex = 5;
            // 
            // frm_SaoTTPT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 181);
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_SaoTTPT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sao thủ thuật - phẫu thuật";
            this.Load += new System.EventHandler(this.frm_SaoTTPT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayCD.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayCD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSaoKQ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTyLe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayKQ.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayKQ.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.SimpleButton btnSao;
        private DevExpress.XtraEditors.CheckEdit chkSaoKQ;
        private DevExpress.XtraEditors.DateEdit dtNgayCD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.DateEdit dtNgayKQ;
        private DevExpress.XtraEditors.ComboBoxEdit cboTyLe;
        private System.Windows.Forms.Label label3;
    }
}