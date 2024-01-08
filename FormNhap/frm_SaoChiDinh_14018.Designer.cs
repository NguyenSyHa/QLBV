namespace QLBV.FormNhap
{
    partial class frm_SaoChiDinh_14018
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_SaoChiDinh_14018));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.dtGioSao = new DevExpress.XtraEditors.DateEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlSoLan = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroupSoLan = new DevExpress.XtraLayout.LayoutControlGroup();
            this.btnSaoChiDinh = new DevExpress.XtraEditors.SimpleButton();
            this.spSoNgaySao = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGioSao.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGioSao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlSoLan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupSoLan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spSoNgaySao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.dtGioSao);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.layoutControlSoLan);
            this.layoutControl1.Controls.Add(this.btnSaoChiDinh);
            this.layoutControl1.Controls.Add(this.spSoNgaySao);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(335, 78);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // dtGioSao
            // 
            this.dtGioSao.EditValue = null;
            this.dtGioSao.Location = new System.Drawing.Point(97, 2);
            this.dtGioSao.Name = "dtGioSao";
            this.dtGioSao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.dtGioSao.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtGioSao.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None);
            this.dtGioSao.Properties.DisplayFormat.FormatString = "HH:mm:ss";
            this.dtGioSao.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtGioSao.Properties.EditFormat.FormatString = "dHH:mm:ss";
            this.dtGioSao.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtGioSao.Properties.Mask.EditMask = "HH:mm:ss";
            this.dtGioSao.Properties.NullValuePrompt = "Nhập giờ sao Giờ:Phút:Giây";
            this.dtGioSao.Properties.NullValuePromptShowForEmptyValue = true;
            this.dtGioSao.Properties.ShowNullValuePromptWhenFocused = true;
            this.dtGioSao.Size = new System.Drawing.Size(68, 20);
            this.dtGioSao.StyleController = this.layoutControl1;
            this.dtGioSao.TabIndex = 9;
            this.dtGioSao.EditValueChanged += new System.EventHandler(this.dtGioSao_EditValueChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(169, 54);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(164, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // layoutControlSoLan
            // 
            this.layoutControlSoLan.Location = new System.Drawing.Point(0, 24);
            this.layoutControlSoLan.Name = "layoutControlSoLan";
            this.layoutControlSoLan.Root = this.layoutControlGroupSoLan;
            this.layoutControlSoLan.Size = new System.Drawing.Size(335, 28);
            this.layoutControlSoLan.TabIndex = 7;
            this.layoutControlSoLan.Text = "layoutControl2";
            // 
            // layoutControlGroupSoLan
            // 
            this.layoutControlGroupSoLan.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupSoLan.GroupBordersVisible = false;
            this.layoutControlGroupSoLan.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupSoLan.Name = "layoutControlGroupSoLan";
            this.layoutControlGroupSoLan.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroupSoLan.Size = new System.Drawing.Size(335, 28);
            this.layoutControlGroupSoLan.TextVisible = false;
            // 
            // btnSaoChiDinh
            // 
            this.btnSaoChiDinh.Image = ((System.Drawing.Image)(resources.GetObject("btnSaoChiDinh.Image")));
            this.btnSaoChiDinh.Location = new System.Drawing.Point(2, 54);
            this.btnSaoChiDinh.Name = "btnSaoChiDinh";
            this.btnSaoChiDinh.Size = new System.Drawing.Size(163, 22);
            this.btnSaoChiDinh.StyleController = this.layoutControl1;
            this.btnSaoChiDinh.TabIndex = 4;
            this.btnSaoChiDinh.Text = "Sao chỉ định";
            this.btnSaoChiDinh.Click += new System.EventHandler(this.btnSaoChiDinh_Click);
            // 
            // spSoNgaySao
            // 
            this.spSoNgaySao.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spSoNgaySao.Location = new System.Drawing.Point(264, 2);
            this.spSoNgaySao.Name = "spSoNgaySao";
            this.spSoNgaySao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spSoNgaySao.Properties.Mask.EditMask = "d";
            this.spSoNgaySao.Properties.MaxValue = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.spSoNgaySao.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spSoNgaySao.Size = new System.Drawing.Size(69, 20);
            this.spSoNgaySao.StyleController = this.layoutControl1;
            this.spSoNgaySao.TabIndex = 6;
            this.spSoNgaySao.EditValueChanged += new System.EventHandler(this.spSoNgaySao_EditValueChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(335, 78);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnSaoChiDinh;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(167, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem3.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItem3.Control = this.spSoNgaySao;
            this.layoutControlItem3.Location = new System.Drawing.Point(167, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(168, 24);
            this.layoutControlItem3.Text = "Số ngày sao:";
            this.layoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(90, 20);
            this.layoutControlItem3.TextToControlDistance = 5;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.layoutControlSoLan;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem2.Size = new System.Drawing.Size(335, 28);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnCancel;
            this.layoutControlItem4.Location = new System.Drawing.Point(167, 52);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(168, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem5.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItem5.Control = this.dtGioSao;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(167, 24);
            this.layoutControlItem5.Text = "Thời gian sao:";
            this.layoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(90, 20);
            this.layoutControlItem5.TextToControlDistance = 5;
            // 
            // frm_SaoChiDinh_14018
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 78);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frm_SaoChiDinh_14018";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sao chỉ định";
            this.Load += new System.EventHandler(this.frm_SaoChiDinh_14018_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtGioSao.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGioSao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlSoLan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupSoLan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spSoNgaySao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnSaoChiDinh;
        private DevExpress.XtraEditors.SpinEdit spSoNgaySao;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControl layoutControlSoLan;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupSoLan;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.DateEdit dtGioSao;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;

    }
}