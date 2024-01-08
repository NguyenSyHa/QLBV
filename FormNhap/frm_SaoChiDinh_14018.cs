using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Customization;
using DevExpress.XtraLayout.Utils;
using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QLBV.FormThamSo;

namespace QLBV.FormNhap
{
    public partial class frm_SaoChiDinh_14018 : Form
    {
        List<int> idCLSs;
        public delegate void ReloadForm();
        ReloadForm dlgReload;
        int idCLSClick;

        public frm_SaoChiDinh_14018(List<int> _idCLSs, ReloadForm _dlgReload, int _idCLSClick)
        {
            InitializeComponent();
            this.idCLSs = _idCLSs;
            dlgReload = _dlgReload;
            idCLSClick = _idCLSClick;
        }

        DateTime ngaySao;
        int heightForm;
        private void frm_SaoChiDinh_14018_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var cls = dataContext.CLS.FirstOrDefault(o => o.IdCLS == idCLSClick);
            if (cls != null)
            {
                ngaySao = cls.NgayThang ?? DateTime.Now;
                dtGioSao.DateTime = cls.NgayThang ?? DateTime.Now;
            }
            heightForm = this.Height;
            spSoNgaySao.EditValue = 1;
        }

        private void spSoNgaySao_EditValueChanged(object sender, EventArgs e)
        {
            var soNgay = spSoNgaySao.Value;
            if (soNgay > 0)
            {
                layoutControlSoLan.BeginUpdate();
                layoutControlGroupSoLan.BeginUpdate();
                layoutControlSoLan.Clear();
                var ngayThang = ngaySao;
                this.Height = heightForm;
                for (int i = 0; i < soNgay; i++)
                {
                    ngayThang = ngayThang.AddDays(1);

                    DateEdit dt = new DateEdit();
                    dt.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    dt.Properties.EditFormat.FormatType = FormatType.DateTime;
                    dt.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    dt.Properties.DisplayFormat.FormatType = FormatType.DateTime;
                    dt.Properties.Mask.EditMask = "dd/MM/yyyy HH:mm:ss";
                    dt.Name = "dtSoLan" + (i + 1);
                    dt.DateTime = ngayThang;
                    dt.Tag = i;
                    dt.Properties.AllowNullInput = DefaultBoolean.False;

                    SpinEdit sp = new SpinEdit();
                    sp.Properties.Mask.EditMask = "d";
                    sp.Properties.Mask.MaskType = MaskType.Numeric;
                    sp.Properties.MinValue = 0;
                    sp.Properties.MaxValue = 999;
                    sp.Name = "spSoLan" + (i + 1);
                    sp.Value = 1;
                    sp.Tag = i;
                    if (i == 0)
                    {
                        LayoutControlItem item = layoutControlGroupSoLan.AddItem();
                        item.AppearanceItemCaption.Options.UseTextOptions = true;
                        item.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
                        item.TextAlignMode = TextAlignModeItem.CustomSize;
                        item.TextSize = new Size(90, 20);
                        item.Text = ngayThang.ToString("dd/MM/yyyy");
                        item.Control = dt;
                        item.TextVisible = false;

                        LayoutControlItem item1 = new LayoutControlItem(layoutControlSoLan, sp);
                        item1.TextVisible = false;
                        item1.Move(item, InsertType.Right);
                    }
                    else
                    {
                        LayoutControlItem item = new LayoutControlItem(layoutControlSoLan, dt);
                        item.AppearanceItemCaption.Options.UseTextOptions = true;
                        item.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
                        item.TextAlignMode = TextAlignModeItem.CustomSize;
                        item.TextSize = new Size(90, 20);
                        item.Text = ngayThang.ToString("dd/MM/yyyy");
                        item.TextVisible = false;
                        LayoutItemDragController dc = new LayoutItemDragController(layoutControlGroupSoLan.Items[2 * i - 1], layoutControlGroupSoLan, InsertLocation.After, LayoutType.Vertical);
                        item.Move(dc);

                        LayoutControlItem item1 = new LayoutControlItem(layoutControlSoLan, sp);
                        item1.TextVisible = false;
                        item1.Move(item, InsertType.Right);

                        this.Height += item.Height;
                    }
                }
                this.Height += 2;
                layoutControlSoLan.EndUpdate();
                layoutControlGroupSoLan.EndUpdate();
                ReallyCenterToScreen();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ReallyCenterToScreen()
        {
            Screen screen = Screen.FromControl(this);

            Rectangle workingArea = screen.WorkingArea;
            this.Location = new Point()
            {
                X = Math.Max(workingArea.X, workingArea.X + (workingArea.Width - this.Width) / 2),
                Y = Math.Max(workingArea.Y, workingArea.Y + (workingArea.Height - this.Height) / 2)
            };
        }

        private void btnSaoChiDinh_Click(object sender, EventArgs e)
        {
            var dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            foreach (var idCLS in idCLSs)
            {
                var cls = dataContext.CLS.FirstOrDefault(o => o.IdCLS == idCLS);
                cls.IS_COPY = true;
                var listControls = layoutControlSoLan.Controls.Cast<Control>().ToList();
                var dateEdits = listControls.Where(o => o is DateEdit).ToList();
                foreach (Control ctrl in listControls)
                {
                    if (ctrl is SpinEdit)
                    {
                        var spin = (SpinEdit)ctrl;
                        for (int i = 0; i < spin.Value; i++)
                        {
                            if (spin.Value == 0)
                                continue;
                            int IdCLS = cls.IdCLS;
                            CL moicls = new CL();
                            moicls.MaBNhan = cls.MaBNhan;
                            moicls.MaCB = cls.MaCB;
                            moicls.MaKP = cls.MaKP;
                            moicls.MaKPth = cls.MaKPth;
                            var dt = (DateEdit)dateEdits.FirstOrDefault(o => o.Tag.ToString() == spin.Tag.ToString());
                            moicls.NgayThang = dt.DateTime;
                            moicls.CapCuu = cls.CapCuu;
                            moicls.Status = 0;
                            moicls.ChanDoan = cls.ChanDoan;
                            moicls.MaICD = cls.MaICD;
                            moicls.STT = 0;
                            dataContext.CLS.Add(moicls);
                            dataContext.SaveChanges();
                            int IdCLSNew = moicls.IdCLS;

                            var chidinh = dataContext.ChiDinhs.Where(p => p.IdCLS == IdCLS).ToList();
                            foreach (var items in chidinh)
                            {
                                int idcd = items.IDCD;
                                ChiDinh themmoiCD = new ChiDinh();
                                themmoiCD.IdCLS = IdCLSNew;
                                themmoiCD.MaDV = items.MaDV;
                                themmoiCD.Status = 0;
                                themmoiCD.DonGia = items.DonGia;
                                themmoiCD.TrongBH = items.TrongBH;
                                themmoiCD.XHH = 0;
                                themmoiCD.YLenh2 = items.YLenh2;
                                themmoiCD.ChiDinh1 = items.ChiDinh1;
                                themmoiCD.IDCD_Copy = items.IDCD;
                                dataContext.ChiDinhs.Add(themmoiCD);
                                if (dataContext.SaveChanges() >= 0)
                                {
                                    int idCDnew = themmoiCD.IDCD;
                                    var clsct = dataContext.CLScts.Where(p => p.IDCD == idcd).ToList();
                                    foreach (var item2 in clsct)
                                    {
                                        CLSct themmoiCL = new CLSct();
                                        themmoiCL.IDCD = idCDnew;
                                        themmoiCL.STTHT = 0;
                                        themmoiCL.MaDVct = item2.MaDVct;
                                        themmoiCL.Status = 0;
                                        dataContext.CLScts.Add(themmoiCL);
                                        dataContext.SaveChanges();
                                    }
                                }
                                var dv = dataContext.DichVus.FirstOrDefault(o => o.MaDV == items.MaDV);
                                if (DungChung.Bien.MaBV == "14017" && dv != null && dv.IDNhom == 8)
                                    FRM_chidinh_Moi.ThucHienTTPT(dataContext, cls.MaBNhan ?? 0, themmoiCD.IDCD, moicls.NgayThang.Value.AddMinutes(5), moicls.MaKP ?? 0, moicls.MaCB);

                            }

                            if (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017")
                            {
                                var ngayDB = moicls.NgayThang ?? DateTime.Now;
                                DungChung.Ham.Update_CLS_DienBienct(cls.MaBNhan ?? 0, ngayDB.Date, cls.MaKP);
                                dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                var dienBien = dataContext.DienBiens.FirstOrDefault(o => o.MaBNhan == (cls.MaBNhan ?? 0) && o.NgayNhap.Value.Day == ngayDB.Day && o.NgayNhap.Value.Month == ngayDB.Month && o.NgayNhap.Value.Year == ngayDB.Year && o.Loai == 1 && o.MaKP == cls.MaKP);
                                if (dienBien != null)
                                {
                                    dienBien.YLenh = "";
                                    dienBien.MaKP = cls.MaKP;
                                    dataContext.SaveChanges();
                                    DungChung.Ham.Update_DienBien_All(dienBien.ID, cls.MaKP);
                                }
                                else
                                {
                                    DienBien dienBienNew = new DienBien();
                                    dienBienNew.NgayNhap = ngayDB;
                                    dienBienNew.YLenh = "";
                                    dienBienNew.DienBien1 = "";
                                    dienBienNew.MaCB = DungChung.Bien.MaCB;
                                    dienBienNew.Loai = 1;
                                    dienBienNew.MaKP = cls.MaKP;
                                    dienBienNew.Ploai = 0;
                                    dienBienNew.MaBNhan = cls.MaBNhan;
                                    dataContext.DienBiens.Add(dienBienNew);
                                    dataContext.SaveChanges();

                                    DungChung.Ham.Update_DienBien_All(dienBienNew.ID, cls.MaKP);
                                }
                            }
                        }
                    }
                }
            }
            MessageBox.Show("Sao thành công");
            if (this.dlgReload != null)
                this.dlgReload();
            this.Close();
        }

        private void dtGioSao_EditValueChanged(object sender, EventArgs e)
        {
            if (dtGioSao.EditValue != null)
            {
                ngaySao = dtGioSao.DateTime;
            }
            spSoNgaySao_EditValueChanged(null, null);
        }
    }
}
