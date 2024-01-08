namespace QLBV.BaoCao
{
    partial class frm_BaocaoKQKhamChuaBenh
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
            this.dateEdit1 = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnBaocao = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.popupContainerControl1 = new DevExpress.XtraEditors.PopupContainerControl();
            this.grcICD = new DevExpress.XtraGrid.GridControl();
            this.grvICD = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaICD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenBenh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.popICD = new DevExpress.XtraEditors.PopupContainerEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).BeginInit();
            this.popupContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcICD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvICD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popICD.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dateEdit1
            // 
            this.dateEdit1.EditValue = null;
            this.dateEdit1.Location = new System.Drawing.Point(102, 50);
            this.dateEdit1.Name = "dateEdit1";
            this.dateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Properties.DisplayFormat.FormatString = "MM/yyyy";
            this.dateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit1.Properties.Mask.EditMask = "MM/yyyy";
            this.dateEdit1.Size = new System.Drawing.Size(137, 20);
            this.dateEdit1.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(22, 53);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Tháng/năm:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(49, 12);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(290, 16);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Báo cáo kết quả khám chữa bệnh theo tháng";
            // 
            // btnBaocao
            // 
            this.btnBaocao.Location = new System.Drawing.Point(259, 53);
            this.btnBaocao.Name = "btnBaocao";
            this.btnBaocao.Size = new System.Drawing.Size(99, 45);
            this.btnBaocao.TabIndex = 3;
            this.btnBaocao.Text = "Lấy báo cáo";
            this.btnBaocao.Click += new System.EventHandler(this.btnBaocao_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Mã ICD";
            this.label1.Visible = false;
            // 
            // popupContainerControl1
            // 
            this.popupContainerControl1.Controls.Add(this.grcICD);
            this.popupContainerControl1.Location = new System.Drawing.Point(235, 87);
            this.popupContainerControl1.Name = "popupContainerControl1";
            this.popupContainerControl1.Size = new System.Drawing.Size(289, 155);
            this.popupContainerControl1.TabIndex = 6;
            // 
            // grcICD
            // 
            this.grcICD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcICD.Location = new System.Drawing.Point(0, 0);
            this.grcICD.MainView = this.grvICD;
            this.grcICD.Name = "grcICD";
            this.grcICD.Size = new System.Drawing.Size(289, 155);
            this.grcICD.TabIndex = 0;
            this.grcICD.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvICD});
            // 
            // grvICD
            // 
            this.grvICD.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaICD,
            this.colTenBenh});
            this.grvICD.GridControl = this.grcICD;
            this.grvICD.Name = "grvICD";
            this.grvICD.OptionsSelection.MultiSelect = true;
            this.grvICD.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.grvICD.OptionsView.ShowGroupPanel = false;
            // 
            // colMaICD
            // 
            this.colMaICD.Caption = "MaICD";
            this.colMaICD.FieldName = "MaICD";
            this.colMaICD.Name = "colMaICD";
            this.colMaICD.Visible = true;
            this.colMaICD.VisibleIndex = 1;
            // 
            // colTenBenh
            // 
            this.colTenBenh.Caption = "Tên bệnh";
            this.colTenBenh.FieldName = "TenICD";
            this.colTenBenh.Name = "colTenBenh";
            this.colTenBenh.Visible = true;
            this.colTenBenh.VisibleIndex = 2;
            // 
            // popICD
            // 
            this.popICD.Location = new System.Drawing.Point(102, 82);
            this.popICD.Name = "popICD";
            this.popICD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.popICD.Properties.PopupControl = this.popupContainerControl1;
            this.popICD.Size = new System.Drawing.Size(137, 20);
            this.popICD.TabIndex = 7;
            this.popICD.Visible = false;
            this.popICD.Popup += new System.EventHandler(this.popICD_Popup);
            this.popICD.CloseUp += new DevExpress.XtraEditors.Controls.CloseUpEventHandler(this.popICD_CloseUp);
            this.popICD.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.popICD_Closed);
            // 
            // frm_BaocaoKQKhamChuaBenh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 127);
            this.Controls.Add(this.popICD);
            this.Controls.Add(this.popupContainerControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBaocao);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.dateEdit1);
            this.Name = "frm_BaocaoKQKhamChuaBenh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frm_BaocaoKQKhamChuaBenh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).EndInit();
            this.popupContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcICD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvICD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popICD.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit dateEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnBaocao;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerControl1;
        private DevExpress.XtraGrid.GridControl grcICD;
        private DevExpress.XtraGrid.Views.Grid.GridView grvICD;
        private DevExpress.XtraEditors.PopupContainerEdit popICD;
        private DevExpress.XtraGrid.Columns.GridColumn colMaICD;
        private DevExpress.XtraGrid.Columns.GridColumn colTenBenh;
    }
}