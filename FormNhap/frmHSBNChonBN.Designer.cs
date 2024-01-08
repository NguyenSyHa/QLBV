namespace QLBV.FormNhap
{
    partial class frmHSBNChonBN
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
            this.gridControlChonBN = new DevExpress.XtraGrid.GridControl();
            this.gridViewChonBN = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlChonBN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewChonBN)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlChonBN
            // 
            this.gridControlChonBN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlChonBN.Location = new System.Drawing.Point(0, 0);
            this.gridControlChonBN.MainView = this.gridViewChonBN;
            this.gridControlChonBN.Name = "gridControlChonBN";
            this.gridControlChonBN.Size = new System.Drawing.Size(720, 300);
            this.gridControlChonBN.TabIndex = 0;
            this.gridControlChonBN.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewChonBN});
            // 
            // gridViewChonBN
            // 
            this.gridViewChonBN.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.gridViewChonBN.GridControl = this.gridControlChonBN;
            this.gridViewChonBN.Name = "gridViewChonBN";
            this.gridViewChonBN.OptionsView.ShowGroupPanel = false;
            this.gridViewChonBN.OptionsView.ShowIndicator = false;
            this.gridViewChonBN.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.gridViewChonBN_CustomUnboundColumnData);
            this.gridViewChonBN.DoubleClick += new System.EventHandler(this.gridViewChonBN_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Họ tên";
            this.gridColumn1.FieldName = "TenBNhan";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 154;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Ngày sinh";
            this.gridColumn2.FieldName = "NgayThangNamSinh";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 84;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Địa chỉ";
            this.gridColumn3.FieldName = "DChi";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 333;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Ngày nhập";
            this.gridColumn4.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn4.FieldName = "NNhap";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 147;
            // 
            // frmHSBNChonBN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 300);
            this.Controls.Add(this.gridControlChonBN);
            this.Name = "frmHSBNChonBN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn bệnh nhân";
            this.Load += new System.EventHandler(this.frmHSBNChonBN_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlChonBN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewChonBN)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlChonBN;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewChonBN;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
    }
}