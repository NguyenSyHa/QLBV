namespace QLBV
{
    partial class frmBCKeThuocTheoBSi
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
            this.btnLayBC = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbChonTatCa = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gluCanBo = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dtpkDenNgay = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtpkTuNgay = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnHuy = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gluCanBo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLayBC
            // 
            this.btnLayBC.Location = new System.Drawing.Point(143, 172);
            this.btnLayBC.Name = "btnLayBC";
            this.btnLayBC.Size = new System.Drawing.Size(75, 23);
            this.btnLayBC.TabIndex = 0;
            this.btnLayBC.Text = "Lấy báo cáo";
            this.btnLayBC.UseVisualStyleBackColor = true;
            this.btnLayBC.Click += new System.EventHandler(this.btnLayBC_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbChonTatCa);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.gluCanBo);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(287, 154);
            this.panel1.TabIndex = 1;
            // 
            // cbChonTatCa
            // 
            this.cbChonTatCa.AutoSize = true;
            this.cbChonTatCa.Location = new System.Drawing.Point(193, 118);
            this.cbChonTatCa.Name = "cbChonTatCa";
            this.cbChonTatCa.Size = new System.Drawing.Size(57, 17);
            this.cbChonTatCa.TabIndex = 4;
            this.cbChonTatCa.Text = "Tất cả";
            this.cbChonTatCa.UseVisualStyleBackColor = true;
            this.cbChonTatCa.CheckedChanged += new System.EventHandler(this.cbChonTatCa_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Chọn CB:";
            // 
            // gluCanBo
            // 
            this.gluCanBo.EditValue = "[Chọn cán bộ]";
            this.gluCanBo.Location = new System.Drawing.Point(75, 90);
            this.gluCanBo.Name = "gluCanBo";
            this.gluCanBo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gluCanBo.Properties.Appearance.Options.UseFont = true;
            this.gluCanBo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.gluCanBo.Properties.View = this.gridLookUpEdit1View;
            this.gluCanBo.Size = new System.Drawing.Size(175, 22);
            this.gluCanBo.TabIndex = 2;
            this.gluCanBo.EditValueChanged += new System.EventHandler(this.gluCanBo_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsFind.AlwaysVisible = true;
            this.gridLookUpEdit1View.OptionsFind.SearchInPreview = true;
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridLookUpEdit1View.OptionsView.ShowAutoFilterRow = true;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dtpkDenNgay);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(3, 42);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(247, 42);
            this.panel3.TabIndex = 1;
            // 
            // dtpkDenNgay
            // 
            this.dtpkDenNgay.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpkDenNgay.CustomFormat = "dd/MM/YYYY";
            this.dtpkDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpkDenNgay.Location = new System.Drawing.Point(78, 13);
            this.dtpkDenNgay.Name = "dtpkDenNgay";
            this.dtpkDenNgay.Size = new System.Drawing.Size(97, 20);
            this.dtpkDenNgay.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Đến ngày:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dtpkTuNgay);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(247, 42);
            this.panel2.TabIndex = 0;
            // 
            // dtpkTuNgay
            // 
            this.dtpkTuNgay.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpkTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpkTuNgay.Location = new System.Drawing.Point(78, 13);
            this.dtpkTuNgay.Name = "dtpkTuNgay";
            this.dtpkTuNgay.Size = new System.Drawing.Size(97, 20);
            this.dtpkTuNgay.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Từ ngày:";
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(224, 172);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(75, 23);
            this.btnHuy.TabIndex = 2;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // frmBCKeThuocTheoBSi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 212);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnLayBC);
            this.Name = "frmBCKeThuocTheoBSi";
            this.Text = "Lấy Báo Cáo Kê Thuốc";
            this.Load += new System.EventHandler(this.frmBCKeThuocTheoBSi_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gluCanBo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLayBC;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DateTimePicker dtpkDenNgay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DateTimePicker dtpkTuNgay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.GridLookUpEdit gluCanBo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.CheckBox cbChonTatCa;
    }
}