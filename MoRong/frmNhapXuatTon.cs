using DevExpress.Data;
using DevExpress.XtraEditors.Controls;
using QLBV.Class;
using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.MoRong
{
    public partial class frmNhapXuatTon : Form
    {
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public frmNhapXuatTon()
        {
            InitializeComponent();
        }

        private void frmNhapXuatTon_Load(object sender, EventArgs e)
        {
            //Load kho
            LoadComboKho();
            btnUpdateThuocTon.Enabled = DungChung.Bien.MaBV == "30372";
            if (DungChung.Bien.MaBV == "24272")
            {
                gridColumn16.Visible = false;
                gridColumn15.Visible = false;
                gridColumn14.Visible = false;
                gridColumn13.Visible = false;
                gridColumn12.Visible = false;
                gridColumn11.Visible = false;
                gridColumn10.Visible = false;
                gridColumn9.Visible = false;
                gridColumn8.Visible = false;
                gridColumn7.Visible = false;
                gridColumn6.Visible = false;
                gridColumn2.Visible = true;
                gridColumn4.Caption = "Số lượng tồn";
                gridViewNXT.OptionsView.ColumnAutoWidth = true;
                //gridColumn1.OptionsColumn.FixedWidth = true;
                //gridColumn3.OptionsColumn.FixedWidth = true;
                //gridColumn5.OptionsColumn.FixedWidth = true;
                //gridColumn12.OptionsColumn.FixedWidth = true;
                //gridColumn13.OptionsColumn.FixedWidth = true;
            }
        }

        private void LoadComboKho()
        {
            var kd = (from kp in dataContext.KPhongs.Where(p => p.Status == 1)
                      where (kp.PLoai == ("Khoa dược") || kp.PLoai == ("Tủ trực")) && kp.MaBVsd == DungChung.Bien.MaBV
                      select kp).OrderBy(o => o.TenKP).ToList();
            cboKho.Properties.DataSource = kd;
        }

        List<NXT> listNXT = new List<NXT>();

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            int? thang = 0;
            bool hetHan = false;
            switch (radioGroup1.SelectedIndex)
            {
                case 0:
                    thang = null;
                    break;
                case 1:
                    hetHan = true;
                    break;
                case 2:
                    thang = 1;
                    break;
                case 3:
                    thang = 3;
                    break;
                case 4:
                    thang = 6;
                    break;
                case 5:
                    thang = 9;
                    break;
            }
            TimKiem(thang, hetHan);
        }

        private void TimKiem(int? thang, bool hetHan)
        {
            listNXT = new List<NXT>();
            if (cboKho.EditValue != null)
            {
                var maKp = int.Parse(cboKho.EditValue.ToString());
                var qnxt2 = (from nhapd in dataContext.NhapDs.Where(o => o.MaKP == maKp && (o.PLoai == 1 || o.PLoai == 2) && (o.Status != -3 || o.Status == null))
                             join nhapdct in dataContext.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                             select new { nhapd.MaKP, nhapd.MaKPnx, nhapdct.MaDV, nhapd.PLoai, nhapd.NgayNhap, nhapdct.DonGia, SoLuongN = nhapd.PLoai == 1 ? nhapdct.SoLuongN : 0, SoLuongX = (nhapd.PLoai == 2 || nhapd.PLoai == 3) ? nhapdct.SoLuongX : 0, ThanhTienN = nhapd.PLoai == 1 ? nhapdct.ThanhTienN : 0, ThanhTienX = (nhapd.PLoai == 2 || nhapd.PLoai == 3) ? nhapdct.ThanhTienX : 0, nhapdct.SoLo, nhapdct.HanDung }).ToList();
                var dichvu = (from dv in dataContext.DichVus.Where(o => o.PLoai == 1)
                              join tn in dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                              join nhomdv in dataContext.NhomDVs on tn.IDNhom equals nhomdv.IDNhom
                              join ncc in dataContext.NhaCCs on dv.MaCC equals ncc.MaCC into kq
                              from ncc1 in kq.DefaultIfEmpty()
                              select new { dv.NhaSX, dv.MaDV, dv.TenDV, dv.MaCC, dv.DonVi, dv.HamLuong, dv.TenHC, dv.MaTam, dv.SoDK, dv.NuocSX, dv.DuongD, dv.NhomThau, dv.MaQD, nhomdv.TenNhom, dv.DonGia, dv.SoQD, NhaThau = (ncc1 != null ? ncc1.TenCC : "") }).ToList();

                listNXT = (from dv in dichvu
                           join nxt in qnxt2 on dv.MaDV equals nxt.MaDV
                           select new NXT
                           {
                               NuocSX = dv.NuocSX,
                               NhaSX = dv.NhaSX,
                               DuongD = dv.DuongD,
                               HamLuong = dv.HamLuong,
                               MaDV = dv.MaDV,
                               MaTam = dv.MaTam,
                               DonVi = dv.DonVi,
                               DonGia = nxt.DonGia,
                               TenHC = dv.TenHC,
                               TenDV = dv.TenDV,
                               NhomThau = dv.NhomThau,
                               SoDK = dv.SoDK,
                               MaQD = dv.MaQD,
                               SoQD = dv.SoQD,
                               NhaThau = dv.NhaThau,
                               SoLo = nxt.SoLo,
                               HanDung = nxt.HanDung,
                               ThuocVatTu = dv.TenNhom,
                               SoLuongN = nxt.SoLuongN,
                               SoLuongX = nxt.SoLuongX,
                               ThanhTienN = nxt.ThanhTienN,
                               ThanhTienX = nxt.ThanhTienX,
                               NgayNhap = nxt.NgayNhap,
                               MaKP = nxt.MaKP,
                               IsNhap = nxt.PLoai == 1,
                           }).ToList();

                var queryNNhap = listNXT.Where(o => o.IsNhap && (dtFromTime.EditValue != null ? (o.NgayNhap.Value >= DungChung.Ham.NgayTu(dtFromTime.DateTime)) : true) && (dtToTime.EditValue != null ? (o.NgayNhap.Value <= DungChung.Ham.NgayDen(dtToTime.DateTime)) : true)).Select(o => new { o.DonGia, o.MaDV }).Distinct().ToList();

                var tonTreo = (from dt in dataContext.DThuocs.Where(p => p.MaKXuat == maKp).Where(p => p.KieuDon != 2 && p.KieuDon != 4)
                               join dtct in dataContext.DThuoccts.Where(o => o.Status == 0 || o.Status == null) on dt.IDDon equals dtct.IDDon
                               group dtct by new { dtct.DonGia, dtct.MaDV } into kq
                               select new { kq.Key.DonGia, kq.Key.MaDV, SoLuong = kq.Sum(o => o.SoLuong) }).ToList();

                var qnxt4 = (from dv in listNXT
                             group dv by new {dv.SoLo, dv.HanDung, dv.HamLuong, dv.DuongD, dv.NhaSX, dv.NuocSX, dv.TenDV, dv.DonVi, dv.DonGia, dv.MaDV, dv.MaTam, dv.SoDK, dv.NhomThau, dv.MaQD, dv.TenHC, dv.ThuocVatTu, dv.NhaThau, dv.SoQD } into kq
                             select new NXT
                             {
                                 NuocSX = kq.Key.NuocSX,
                                 NhaSX = kq.Key.NhaSX,
                                 DuongD = kq.Key.DuongD,
                                 HamLuong = kq.Key.HamLuong,
                                 MaDV = kq.Key.MaDV,
                                 MaTam = kq.Key.MaTam,
                                 DonVi = kq.Key.DonVi,
                                 DonGia = kq.Key.DonGia,
                                 TenHC = kq.Key.TenHC,
                                 TenDV = kq.Key.TenDV,
                                 NhomThau = kq.Key.NhomThau,
                                 SoDK = kq.Key.SoDK,
                                 SoQD = kq.Key.SoQD,
                                 NhaThau = kq.Key.NhaThau,
                                 SoLo = kq.OrderByDescending(o => o.NgayNhap).FirstOrDefault(o => o.IsNhap && o.SoLo != null && o.SoLo != "") != null ? kq.FirstOrDefault(o => o.IsNhap && o.SoLo != null && o.SoLo != "").SoLo : "",
                                 HanDung = kq.FirstOrDefault(o => o.IsNhap && o.HanDung != null) != null ? kq.FirstOrDefault(o => o.IsNhap && o.HanDung != null).HanDung : null,
                                 MaQD = kq.Key.MaQD,
                                 TonSL = kq.Where(p => p.NgayNhap <= DateTime.Now).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap <= DateTime.Now).Sum(p => p.SoLuongX),
                                 ThuocVatTu = kq.Key.ThuocVatTu,
                             }).Where(o => (thang == null) ? true : (o.HanDung == null ? false : CheckMonth(o.HanDung.Value, thang ?? 0, hetHan))).Where(o => queryNNhap.Contains(new { o.DonGia, o.MaDV })).OrderBy(p => p.TenDV).ToList();

                if (DungChung.Bien.MaBV == "24012")
                {
                    qnxt4 = (from dv in listNXT
                             group dv by new {dv.HamLuong, dv.DuongD, dv.NhaSX, dv.NuocSX, dv.TenDV, dv.DonVi, dv.DonGia, dv.MaDV, dv.MaTam, dv.SoDK, dv.NhomThau, dv.MaQD, dv.TenHC, dv.ThuocVatTu, dv.NhaThau, dv.SoQD } into kq
                             select new NXT
                             {
                                 NuocSX = kq.Key.NuocSX,
                                 NhaSX = kq.Key.NhaSX,
                                 DuongD = kq.Key.DuongD,
                                 HamLuong = kq.Key.HamLuong,
                                 MaDV = kq.Key.MaDV,
                                 MaTam = kq.Key.MaTam,
                                 DonVi = kq.Key.DonVi,
                                 DonGia = kq.Key.DonGia,
                                 TenHC = kq.Key.TenHC,
                                 TenDV = kq.Key.TenDV,
                                 NhomThau = kq.Key.NhomThau,
                                 SoDK = kq.Key.SoDK,
                                 SoQD = kq.Key.SoQD,
                                 NhaThau = kq.Key.NhaThau,
                                 SoLo = kq.OrderByDescending(o => o.NgayNhap).FirstOrDefault(o => o.IsNhap && o.SoLo != null && o.SoLo != "") != null ? kq.FirstOrDefault(o => o.IsNhap && o.SoLo != null && o.SoLo != "").SoLo : "",
                                 HanDung = kq.FirstOrDefault(o => o.IsNhap && o.HanDung != null) != null ? kq.FirstOrDefault(o => o.IsNhap && o.HanDung != null).HanDung : null,
                                 MaQD = kq.Key.MaQD,
                                 TonSL = kq.Where(p => p.NgayNhap <= DateTime.Now).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap <= DateTime.Now).Sum(p => p.SoLuongX),
                                 ThuocVatTu = kq.Key.ThuocVatTu,
                             }).Where(o => (thang == null) ? true : (o.HanDung == null ? false : CheckMonth(o.HanDung.Value, thang ?? 0, hetHan))).Where(o => queryNNhap.Contains(new { o.DonGia, o.MaDV })).OrderBy(p => p.TenDV).ToList();
                }
                else if (DungChung.Bien.MaBV == "24272")
                {
                    qnxt4 = (from dv in listNXT
                             group dv by new { dv.TenDV, dv.DonVi, dv.DonGia, dv.MaDV, dv.ThuocVatTu} into kq
                             select new NXT
                             {
                                 MaDV = kq.Key.MaDV,
                                 DonVi = kq.Key.DonVi,
                                 DonGia = kq.Key.DonGia,
                                 TenDV = kq.Key.TenDV,
                                 TonSL = kq.Where(p => p.NgayNhap <= DateTime.Now).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap <= DateTime.Now).Sum(p => p.SoLuongX),
                                 ThuocVatTu = kq.Key.ThuocVatTu,
                             }).Where(o => (thang == null) ? true : (o.HanDung == null ? false : CheckMonth(o.HanDung.Value, thang ?? 0, hetHan))).Where(o => queryNNhap.Contains(new { o.DonGia, o.MaDV })).OrderBy(p => p.TenDV).ToList();
                }

                qnxt4.ForEach(o => { var tt = tonTreo.FirstOrDefault(p => p.MaDV == o.MaDV && p.DonGia == o.DonGia); if (tt != null) o.ChuaXuatSL = tt.SoLuong; });

                gridControlNXT.DataSource = qnxt4;
            }
            else
                gridControlNXT.DataSource = null;
        }

        private bool CheckMonth(DateTime dateTime, int month, bool hetHan)
        {
            bool rs = false;
            var addMonth = DateTime.Now.Date.AddMonths(month);
            if (hetHan ? (DateTime.Now.Date > dateTime.Date) : (addMonth > dateTime.Date && DateTime.Now.Date <= dateTime.Date))
                rs = true;
            return rs;
        }

        private void gridViewNXT_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData && e.Column.UnboundType != UnboundColumnType.Bound)
            {
                if (e.Column.FieldName == "STT")
                {
                    e.Value = e.ListSourceRowIndex + 1;
                }
            }
        }

        private void gridViewNXT_DoubleClick(object sender, EventArgs e)
        {
            var row = (NXT)gridViewNXT.GetFocusedRow();
            if (row != null)
            {
                frmNhapXuatTon_ChiTiet frm = new frmNhapXuatTon_ChiTiet(row, listNXT, dtFromTime.EditValue, dtToTime.EditValue);
                frm.ShowDialog();
            }
        }

        public class DVKP
        {
            public int MaKP { get; set; }
            public int MaDV { get; set; }
            public double SoLuong { get; set; }
            public bool IsUpdate { get; set; }
        }

        private void btnUpdateThuocTon_Click(object sender, EventArgs e)
        {
            List<DVKP> listDVKPs = new List<DVKP>();
            var dichVu = dataContext.DichVus.Where(o => o.Status == 1 && o.PLoai == 1).ToList();
            foreach (var dv in dichVu)
            {
                var makp = cboKho.EditValue != null ? Convert.ToInt32(cboKho.EditValue) : 0;
                var khoaPhong = dataContext.KPhongs.Where(p => (p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc) && p.MaBVsd == DungChung.Bien.MaBV).Where(p => p.Status == 1).Where(o => makp > 0 ? o.MaKP == makp : true).ToList();
                foreach (var kp in khoaPhong)
                {
                    DungChung.Ham._getDSGia(dataContext, dv.MaDV, kp.MaKP);
                    listDVKPs.Add(new DVKP { MaDV = dv.MaDV, MaKP = kp.MaKP, SoLuong = DungChung.Bien.SoLuongTon });
                }
            }

            var dvKP = (from up in listDVKPs
                        group up by new { up.MaDV, up.MaKP } into kq
                        select new DVKP { MaDV = kq.Key.MaDV, MaKP = kq.Key.MaKP, IsUpdate = kq.Where(o => o.SoLuong > 0).Count() <= 0 }).Where(o => o.IsUpdate).ToList();

            if (dvKP.Count > 0)
            {
                foreach (var update in dvKP)
                {
                    var dvUpdate = dataContext.DichVus.FirstOrDefault(o => o.MaDV == update.MaDV);
                    if (dvUpdate != null && !string.IsNullOrWhiteSpace(dvUpdate.MaKPsd))
                    {
                        List<string> kpSDUpdate = new List<string>();
                        var kpSp = dvUpdate.MaKPsd.Split(';');
                        if (kpSp != null && kpSp.Count() > 0)
                        {
                            foreach (var k in kpSp)
                            {
                                if (!string.IsNullOrWhiteSpace(k) && k != update.MaKP.ToString())
                                {
                                    kpSDUpdate.Add(k);
                                }
                            }
                        }

                        if (kpSDUpdate.Count > 0)
                        {
                            dvUpdate.MaKPsd = string.Join(";", kpSDUpdate);
                            dataContext.SaveChanges();
                        }
                    }
                }
                MessageBox.Show("Cập nhật thành công!");
            }
        }

        private void dtFromTime_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                dtFromTime.EditValue = null;
            }
        }

        private void dtToTime_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                dtToTime.EditValue = null;
            }
        }

        private void gridViewNXT_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            var row = (NXT)gridViewNXT.GetRow(e.RowHandle);
            if (row != null)
            {
                if (DungChung.Bien.MaBV == "14017")
                {
                    if (row.HanDung == null)
                        return;
                    if (CheckMonth(row.HanDung.Value, 0, true))
                        e.Appearance.ForeColor = Color.Red;
                    else if (CheckMonth(row.HanDung.Value, 1, false))
                    {
                        e.Appearance.ForeColor = Color.Orange;
                    }
                    else if (CheckMonth(row.HanDung.Value, 3, false))
                    {
                        e.Appearance.ForeColor = Color.Green;
                    }
                }
            }
        }

        private void btnThuocKD_Click(object sender, EventArgs e)
        {
            if (gridViewNXT.SelectedRowsCount > 0)
            {
                int makho = cboKho.EditValue != null ? Convert.ToInt32(cboKho.EditValue) : 0;
                int madv = gridViewNXT.GetFocusedRowCellValue(gridColumn2) != null ? Convert.ToInt32(gridViewNXT.GetFocusedRowCellValue(gridColumn2)) : 0;
                double dongia = gridViewNXT.GetFocusedRowCellValue(gridColumn5) != null ? Convert.ToDouble(gridViewNXT.GetFocusedRowCellValue(gridColumn5)) : 0;

                TraCuu.us_TCThuocKD userControl = new TraCuu.us_TCThuocKD(makho, madv, dongia);
                userControl.Dock = DockStyle.Fill;
                Form frm = new Form();
                frm.Size = new Size(1100, 600);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.Controls.Add(userControl);
                frm.ShowDialog();
            }
            
        }
    }
}
