namespace QLBV.FormThamSo
{
    partial class Frm_NangcapSQL
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
            this.txtPass = new DevExpress.XtraEditors.TextEdit();
            this.sbtCheck = new DevExpress.XtraEditors.SimpleButton();
            this.GrcDS = new DevExpress.XtraGrid.GridControl();
            this.GrvDS = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNoidung = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsql = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sbtUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.mmcommand = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrcDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrvDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmcommand.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 21);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(117, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Mật khẩu tài khoản SQL:";
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(135, 18);
            this.txtPass.Name = "txtPass";
            this.txtPass.Properties.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(161, 20);
            this.txtPass.TabIndex = 1;
            this.txtPass.EditValueChanged += new System.EventHandler(this.txtPass_EditValueChanged);
            // 
            // sbtCheck
            // 
            this.sbtCheck.Location = new System.Drawing.Point(302, 16);
            this.sbtCheck.Name = "sbtCheck";
            this.sbtCheck.Size = new System.Drawing.Size(118, 23);
            this.sbtCheck.TabIndex = 2;
            this.sbtCheck.Text = "Kiểm tra kết nối";
            this.sbtCheck.Click += new System.EventHandler(this.sbtCheck_Click);
            // 
            // GrcDS
            // 
            this.GrcDS.Location = new System.Drawing.Point(4, 45);
            this.GrcDS.MainView = this.GrvDS;
            this.GrcDS.Name = "GrcDS";
            this.GrcDS.Size = new System.Drawing.Size(552, 293);
            this.GrcDS.TabIndex = 3;
            this.GrcDS.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrvDS});
            // 
            // GrvDS
            // 
            this.GrvDS.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSTT,
            this.colNoidung,
            this.colsql,
            this.colChon});
            this.GrvDS.GridControl = this.GrcDS;
            this.GrvDS.Name = "GrvDS";
            this.GrvDS.OptionsView.ShowGroupPanel = false;
            this.GrvDS.OptionsView.ShowViewCaption = true;
            this.GrvDS.ViewCaption = "Chọn câu lệnh Update";
            // 
            // colSTT
            // 
            this.colSTT.Caption = "STT";
            this.colSTT.FieldName = "STT";
            this.colSTT.Name = "colSTT";
            this.colSTT.Visible = true;
            this.colSTT.VisibleIndex = 0;
            this.colSTT.Width = 51;
            // 
            // colNoidung
            // 
            this.colNoidung.AppearanceCell.Options.UseTextOptions = true;
            this.colNoidung.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNoidung.Caption = "Nội dung Update";
            this.colNoidung.FieldName = "Mota";
            this.colNoidung.Name = "colNoidung";
            this.colNoidung.Visible = true;
            this.colNoidung.VisibleIndex = 1;
            this.colNoidung.Width = 291;
            // 
            // colsql
            // 
            this.colsql.Caption = "sql";
            this.colsql.FieldName = "sql";
            this.colsql.Name = "colsql";
            // 
            // colChon
            // 
            this.colChon.Caption = "Chọn";
            this.colChon.FieldName = "Chon";
            this.colChon.Name = "colChon";
            this.colChon.Visible = true;
            this.colChon.VisibleIndex = 2;
            this.colChon.Width = 40;
            // 
            // sbtUpdate
            // 
            this.sbtUpdate.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.sbtUpdate.Appearance.Options.UseFont = true;
            this.sbtUpdate.Location = new System.Drawing.Point(4, 346);
            this.sbtUpdate.Name = "sbtUpdate";
            this.sbtUpdate.Size = new System.Drawing.Size(93, 94);
            this.sbtUpdate.TabIndex = 4;
            this.sbtUpdate.Text = "Update";
            this.sbtUpdate.Click += new System.EventHandler(this.sbtUpdate_Click);
            // 
            // mmcommand
            // 
            this.mmcommand.Location = new System.Drawing.Point(103, 344);
            this.mmcommand.Name = "mmcommand";
            this.mmcommand.Properties.ReadOnly = true;
            this.mmcommand.Size = new System.Drawing.Size(453, 96);
            this.mmcommand.TabIndex = 7;
            // 
            // Frm_NangcapSQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 460);
            this.Controls.Add(this.mmcommand);
            this.Controls.Add(this.sbtUpdate);
            this.Controls.Add(this.GrcDS);
            this.Controls.Add(this.sbtCheck);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.labelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_NangcapSQL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nâng cấp SQL";
            this.Load += new System.EventHandler(this.Frm_NangcapSQL_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtPass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrcDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrvDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmcommand.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtPass;
        private DevExpress.XtraEditors.SimpleButton sbtCheck;
        private DevExpress.XtraGrid.GridControl GrcDS;
        private DevExpress.XtraGrid.Views.Grid.GridView GrvDS;
        private DevExpress.XtraGrid.Columns.GridColumn colSTT;
        private DevExpress.XtraGrid.Columns.GridColumn colNoidung;
        private DevExpress.XtraGrid.Columns.GridColumn colsql;
        private DevExpress.XtraGrid.Columns.GridColumn colChon;
        private DevExpress.XtraEditors.SimpleButton sbtUpdate;
        private DevExpress.XtraEditors.MemoEdit mmcommand;
    }
}