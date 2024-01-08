namespace QLBV.FormNhap
{
    partial class frm_xoadulieugiac
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_xoadulieugiac));
            this.radiodonthuoc = new System.Windows.Forms.RadioButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.denngay = new DevExpress.XtraEditors.DateEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.benhnhan = new DevExpress.XtraGrid.GridControl();
            this.Xem = new DevExpress.XtraEditors.SimpleButton();
            this.sobenhnhan = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.denngay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.denngay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.benhnhan)).BeginInit();
            this.SuspendLayout();
            // 
            // radiodonthuoc
            // 
            this.radiodonthuoc.AutoSize = true;
            this.radiodonthuoc.Location = new System.Drawing.Point(2, 6);
            this.radiodonthuoc.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.radiodonthuoc.Name = "radiodonthuoc";
            this.radiodonthuoc.Size = new System.Drawing.Size(173, 17);
            this.radiodonthuoc.TabIndex = 1;
            this.radiodonthuoc.TabStop = true;
            this.radiodonthuoc.Text = "Đơn thuốc và đơnthuốc chi tiết";
            this.radiodonthuoc.UseVisualStyleBackColor = true;
            this.radiodonthuoc.Click += new System.EventHandler(this.radioButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(288, 6);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "Xóa";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(3, 29);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(112, 17);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Bệnh nhân diều trị";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // denngay
            // 
            this.denngay.EditValue = null;
            this.denngay.Enabled = false;
            this.denngay.Location = new System.Drawing.Point(45, 53);
            this.denngay.Name = "denngay";
            this.denngay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.denngay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.denngay.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.denngay.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.denngay.Size = new System.Drawing.Size(130, 20);
            this.denngay.TabIndex = 4;
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.benhnhan;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // benhnhan
            // 
            this.benhnhan.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.benhnhan.Enabled = false;
            this.benhnhan.Location = new System.Drawing.Point(0, 79);
            this.benhnhan.MainView = this.gridView1;
            this.benhnhan.Name = "benhnhan";
            this.benhnhan.Size = new System.Drawing.Size(363, 210);
            this.benhnhan.TabIndex = 6;
            this.benhnhan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // Xem
            // 
            this.Xem.Enabled = false;
            this.Xem.Image = ((System.Drawing.Image)(resources.GetObject("Xem.Image")));
            this.Xem.Location = new System.Drawing.Point(288, 49);
            this.Xem.Name = "Xem";
            this.Xem.Size = new System.Drawing.Size(75, 23);
            this.Xem.TabIndex = 7;
            this.Xem.Text = "Xem";
            this.Xem.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // sobenhnhan
            // 
            this.sobenhnhan.Location = new System.Drawing.Point(193, 55);
            this.sobenhnhan.Name = "sobenhnhan";
            this.sobenhnhan.Size = new System.Drawing.Size(6, 13);
            this.sobenhnhan.TabIndex = 8;
            this.sobenhnhan.Text = "0";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(3, 55);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(39, 13);
            this.labelControl1.TabIndex = 9;
            this.labelControl1.Text = "Đ ngày:";
            // 
            // frm_xoadulieugia
            // 
            this.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 289);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.sobenhnhan);
            this.Controls.Add(this.Xem);
            this.Controls.Add(this.benhnhan);
            this.Controls.Add(this.denngay);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.radiodonthuoc);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "frm_xoadulieugia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xoa dữ liệu giác ";
            this.Load += new System.EventHandler(this.frm_xoadulieugia_Load);
            ((System.ComponentModel.ISupportInitialize)(this.denngay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.denngay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.benhnhan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radiodonthuoc;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.RadioButton radioButton1;
        private DevExpress.XtraEditors.DateEdit denngay;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.GridControl benhnhan;
        private DevExpress.XtraEditors.SimpleButton Xem;
        private DevExpress.XtraEditors.LabelControl sobenhnhan;
        private DevExpress.XtraEditors.LabelControl labelControl1;

    }
}