using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Reflection;

namespace QLBV.FormThamSo
{
    public partial class frm_BC_ChiTieuChuyenMon_BVDKKimThanh_Quy : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_ChiTieuChuyenMon_BVDKKimThanh_Quy()
        {
            InitializeComponent();
        }

        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        public class namBC
        {
            public int Value { set; get; }
        }

        public class Khoa
        {
            public int makp { get; set; }
            public int mack { get; set; }
            public string tenkp { get; set; }
            public string pLoai { get; set; }
            public string chuyenKhoa { get; set; }
        }
        List<Khoa> _khoaMoi = new List<Khoa>();
        private void frm_BC_ChiTieuChuyenMon_BVDKKimThanh_Load(object sender, EventArgs e)
        {
            int namHT = DateTime.Now.Year;
            List<namBC> _list = new List<namBC>();
            for (int i = namHT - 10; i <= namHT + 2; i++)
            {
                namBC obj = new namBC();
                obj.Value = i;
                _list.Add(obj);
            }
            cbbNam.DisplayMember = "Value";
            cbbNam.ValueMember = "Value";
            cbbNam.DataSource = _list;
            cbbNam.SelectedValue = namHT;

            cbbQuyBC.SelectedItem = "1";

            #region List Khoa Combobox
            var listKhoa = (from n in data.KPhongs.Where(p => p.PLoai == "Lâm sàng")
                            select new { n.MaKP, n.TenKP, n.PLoai, n.ChuyenKhoa, n.MaCK }).ToList();
            Khoa moi1 = new Khoa();
            moi1.tenkp = "Chọn tất cả";
            moi1.makp = 0;
            moi1.mack = 0;
            moi1.pLoai = "";
            moi1.chuyenKhoa = "";
            _khoaMoi.Add(moi1);
            Khoa moi2 = new Khoa();
            moi2.tenkp = "Khoa chẩn đoán hình ảnh";
            moi2.makp = 1000;
            moi2.mack = 0;
            moi2.pLoai = "";
            moi2.chuyenKhoa = "";
            _khoaMoi.Add(moi2);
            Khoa moi4 = new Khoa();
            moi4.tenkp = "Khoa Dược";
            moi4.makp = 1002;
            moi4.mack = 0;
            moi4.pLoai = "";
            moi4.chuyenKhoa = "";
            _khoaMoi.Add(moi4);
            Khoa moi5 = new Khoa();
            moi5.tenkp = "Khoa Khám Bệnh";
            moi5.makp = 1002;
            moi5.mack = 0;
            moi5.pLoai = "";
            moi5.chuyenKhoa = "";
            _khoaMoi.Add(moi5);

            foreach (var a in listKhoa)
            {
                Khoa themmoi = new Khoa();
                themmoi.tenkp = a.TenKP;
                themmoi.makp = a.MaKP;
                themmoi.mack = Convert.ToInt32(a.MaCK);
                themmoi.pLoai = a.PLoai;
                themmoi.chuyenKhoa = a.ChuyenKhoa;
                _khoaMoi.Add(themmoi);
            }

            cbbKhoa.DisplayMember = "tenkp";
            cbbKhoa.ValueMember = "makp";
            cbbKhoa.DataSource = _khoaMoi.ToList();
            cbbKhoa.SelectedValue = 0;
            #endregion
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static DateTime GetFirstDayOfMonth(int iYear, int iMonth)
        {
            DateTime dtResult = new DateTime(iYear, iMonth, 1, 00, 00, 00, 000);
            dtResult = dtResult.AddDays((-dtResult.Day) + 1);
            return dtResult;
        }

        public static DateTime GetLastDayOfMonth(int iYear, int iMonth)
        {
            DateTime dtResult = new DateTime(iYear, iMonth, 1, 23, 59, 59, 999);
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult;
        }

        public class QuerryField
        {
            public QuerryField() { }
            public QuerryField(DateTime? ngayTH, string tenRG, int? soTT, string tenDV, int? maBNhan, string tenKP)
            {
                this.NgayTH = ngayTH;
                this.TenRG = tenRG;
                this.SoTT = soTT;
                this.TenDV = tenDV;
                this.MaBNhan = maBNhan;
                this.TenKP = tenKP;
            }

            public DateTime? NgayTH { get; set; }
            public string TenRG { get; set; }
            public int? SoTT { get; set; }
            public string TenDV { get; set; }
            public int? MaBNhan { get; set; }
            public string TenKP { get; set; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            #region Ngày tháng
            //DateTime moi.TuNgayQuy = GetFirstDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 1);
            //DateTime moi.DenNgayQuy = GetLastDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 12);

            //DateTime moi.TuNgayThang1 = GetFirstDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 1);
            //DateTime moi.DenNgayThang1 = GetLastDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 3);

            //DateTime tuNgayThang1 = GetFirstDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 1);
            //DateTime denNgayThang1 = GetLastDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 1);
            //DateTime tuNgayThang2 = GetFirstDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 2);
            //DateTime denNgayThang2 = GetLastDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 2);
            //DateTime tuNgayThang3 = GetFirstDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 3);
            //DateTime denNgayThang3 = GetLastDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 3);

            //DateTime moi.TuNgayThang2 = GetFirstDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 4);
            //DateTime moi.DenNgayThang2 = GetLastDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 6);

            //DateTime tuNgayThang4 = GetFirstDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 4);
            //DateTime denNgayThang4 = GetLastDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 4);
            //DateTime tuNgayThang5 = GetFirstDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 5);
            //DateTime denNgayThang5 = GetLastDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 5);
            //DateTime tuNgayThang6 = GetFirstDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 6);
            //DateTime denNgayThang6 = GetLastDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 6);

            //DateTime moi.TuNgayThang3 = GetFirstDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 7);
            //DateTime moi.DenNgayThang3 = GetLastDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 9);

            //DateTime tuNgayThang7 = GetFirstDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 7);
            //DateTime denNgayThang7 = GetLastDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 7);
            //DateTime tuNgayThang8 = GetFirstDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 8);
            //DateTime denNgayThang8 = GetLastDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 8);
            //DateTime tuNgayThang9 = GetFirstDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 9);
            //DateTime denNgayThang9 = GetLastDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 9);

            //DateTime tungayquy4 = GetFirstDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 10);
            //DateTime denngayquy4 = GetLastDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 12);

            //DateTime tuNgayThang10 = GetFirstDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 10);
            //DateTime denNgayThang10 = GetLastDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 10);
            //DateTime tuNgayThang11 = GetFirstDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 11);
            //DateTime denNgayThang11 = GetLastDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 11);
            //DateTime tuNgayThang12 = GetFirstDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 12);
            //DateTime denNgayThang12 = GetLastDayOfMonth(Convert.ToInt32(cbbNam.SelectedValue), 12);

            #endregion
            string _tenkhoa = this.cbbKhoa.GetItemText(this.cbbKhoa.SelectedItem);
            int _maK = Convert.ToInt32(cbbKhoa.SelectedValue);
            List<Content> _lContent = new List<Content>();
            Content moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
            if (_tenkhoa != "Khoa chẩn đoán hình ảnh" && _tenkhoa != "Khoa Dược" && _tenkhoa != "Khoa Khám Bệnh" && !_tenkhoa.ToLower().Contains("khoa liên chuyên khoa"))
            {
                #region query
                var canbo = (from cb in data.CanBoes.Where(p => _maK == 0 || p.MaKP == _maK)
                             select new
                             {
                                 cb.MaCB,
                                 cb.TenCB
                             }).ToList();
                //Bệnh nhân ra viện
                var q3 = (from rv in data.RaViens.Where(p => _maK == 0 || p.MaKP == _maK)
                          join bn in data.BenhNhans on rv.MaBNhan equals bn.MaBNhan
                          select new { bn, rv }).ToList();
                //bệnh nhân ngoại trú
                var bnNgoaitru = (from rv in data.RaViens.Where(p => _maK == 0 || p.MaKP == _maK)
                                  join a in data.BenhNhans.Where(p => p.NoiTru == 0 && p.DTNT == true) on rv.MaBNhan equals a.MaBNhan
                                  group new { a, rv } by new { a.MaBNhan, rv.NgayRa } into kq
                                  select new { kq.Key.MaBNhan, kq.Key.NgayRa }).ToList();
                var qTaiNan = (from bn in data.BenhNhans
                               join ttbx in data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                               join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                               from kq1 in kq.DefaultIfEmpty()
                               select new
                               {
                                   bn.MaBNhan,
                                   bn.NNhap,
                                   bn.ChuyenKhoa
                               }).Where(p => p.ChuyenKhoa == "Tai nạn giao thông" || p.ChuyenKhoa == "Đường bộ" || p.ChuyenKhoa == "Đường sắt"
                                           || p.ChuyenKhoa == "Đường sông" || p.ChuyenKhoa == "Tai nạn lao động" || p.ChuyenKhoa == "Tai nạn sinh hoạt"
                                           || p.ChuyenKhoa == "Đánh nhau" || p.ChuyenKhoa == "Tự tử" || p.ChuyenKhoa == "Ngộ độc" || p.ChuyenKhoa == "Đuối nước"
                                           || p.ChuyenKhoa == "Bạo lực gia đình" || p.ChuyenKhoa == "Khác").ToList();

                var TNKhac = (from n in qTaiNan
                              select n).Where(p => p.ChuyenKhoa == "Tai nạn giao thông" || p.ChuyenKhoa == "Đường bộ" || p.ChuyenKhoa == "Đường sắt"
                                       || p.ChuyenKhoa == "Đường sông" || p.ChuyenKhoa == "Tai nạn lao động" || p.ChuyenKhoa == "Tai nạn sinh hoạt"
                                       || p.ChuyenKhoa == "Đánh nhau" || p.ChuyenKhoa != "Tự tử" || p.ChuyenKhoa != "Ngộ độc" || p.ChuyenKhoa == "Đuối nước"
                                       || p.ChuyenKhoa == "Bạo lực gia đình" || p.ChuyenKhoa == "Khác").ToList();

                DateTime tuQuy = DateTime.MinValue;
                DateTime denQuy = DateTime.MinValue;

                GetNgayCuaQuy(Convert.ToInt32(cbbNam.SelectedValue), Convert.ToInt32(cbbQuyBC.SelectedItem), ref tuQuy, ref denQuy);

                var qCLS = (from cls in data.CLS.Where(o => o.NgayTH >= tuQuy && o.NgayTH <= denQuy)
                            join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                            join kp in data.KPhongs on cls.MaKP equals kp.MaKP
                            select new { cls.IdCLS, cls.NgayTH, cls.MaBNhan, cls.MaKP, kp.TenKP, bn.NoiTru, bn.DTNT }).ToList();
                var qCLS1 = (from cd in data.ChiDinhs.Where(o => o.Status == 1)
                             join dv in data.DichVus on cd.MaDV equals dv.MaDV
                             join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                             join dtct in data.DThuoccts.Where(o => o.IDCD != null) on cd.IDCD equals dtct.IDCD
                             select new { cd.Status, cd.IdCLS, tn.TenRG, dv.Loai, dtct.IDCD, dv.TenDV }).ToList();

                //var qClsKhamSan = (from cls in data.CLS
                //                   join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                //                   join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                //                   join kp in data.KPhongs on cls.MaKP equals kp.MaKP
                //                   join dv in data.DichVus on cd.MaDV equals dv.MaDV
                //                   join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                //                   join dtct in data.DThuoccts on cd.IDCD equals dtct.IDCD
                //                   where (_maK == 0 || cls.MaKP == _maK) && bn.NoiTru == 1
                //                   select new { cls.IdCLS, cls.NgayTH, cls.MaBNhan, cls.MaKP, kp.TenKP, bn.NoiTru, bn.DTNT, cd.Status, tn.TenRG, dv.Loai, dtct.IDCD, dv.TenDV }).ToList();

                var qClsKhamSan = (from a in qCLS.Where(p => _maK == 0 || p.MaKP == _maK)
                                   join b in qCLS1 on a.IdCLS equals b.IdCLS
                                   select new { a.IdCLS, a.NgayTH, a.MaBNhan, a.MaKP, a.TenKP, a.NoiTru, a.DTNT, b.Status, b.TenRG, b.Loai, b.IDCD, b.TenDV }).Where(p => p.NoiTru == 1).ToList();

                var _luotKham = (from bnkb in data.BNKBs.Where(p => _maK == 0 || p.MaKP == _maK)
                                 join kp in data.KPhongs on bnkb.MaKP equals kp.MaKP
                                 group new { bnkb, kp } by new { bnkb.MaBNhan, kp.TenKP, bnkb.NgayKham } into kq
                                 select new { kq.Key.TenKP, kq.Key.NgayKham }).Where(p => p.TenKP.ToUpper().Contains(_tenkhoa.ToUpper())).ToList();
                #endregion

                #region 1.Tổng số CBCNV
                moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                moi.Stt = 1;
                moi.DanhMuc = "Tổng số CBCNV";
                moi.ChiTietDanhMuc = "";
                moi.DVTinh = "Người";
                moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 0 : 0;
                moi.ThucHienCaQuy = canbo.Count;
                _lContent.Add(moi);
                #endregion
                #region 2. Giường bệnh
                moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                moi.Stt = 2;
                moi.DanhMuc = "Giường bệnh";
                moi.ChiTietDanhMuc = "";
                moi.DVTinh = "Người";
                moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 0 : 0;
                moi.ThucHienCaQuy = (_maK != 0) ? SoGiuong(data.KPhongs.Where(p => p.MaKP == _maK).FirstOrDefault().BuongGiuong) : 0;
                _lContent.Add(moi);
                #endregion
                #region 3. Ngày điều trị trung bình
                moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                moi.Stt = 3;
                moi.DanhMuc = "Ngày điều trị trung bình";
                moi.ChiTietDanhMuc = "";
                moi.DVTinh = "Ngày";
                //moi.ChiTieuCaQuy = 10;
                moi.ChiTieuCaQuy = (q3.Where(p => p.rv.NgayRa >= moi.TuNgayQuy && p.rv.NgayRa <= moi.DenNgayQuy).Count() != 0) ? (int)q3.Where(p => p.rv.NgayRa >= moi.TuNgayQuy && p.rv.NgayRa <= moi.DenNgayQuy).Sum(p => p.rv.SoNgaydt) / q3.Where(p => p.rv.NgayRa >= moi.TuNgayQuy && p.rv.NgayRa <= moi.DenNgayQuy).Count() : 0;
                //moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                moi.ThucHienThang1 = (q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang1 && p.rv.NgayRa <= moi.DenNgayThang1).Count() != 0) ? (int)q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang1 && p.rv.NgayRa <= moi.DenNgayThang1).Sum(p => p.rv.SoNgaydt) / q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang1 && p.rv.NgayRa <= moi.DenNgayThang1).Count() : 0;
                // moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                moi.ThucHienThang2 = (q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang2 && p.rv.NgayRa <= moi.DenNgayThang2).Count() != 0) ? (int)q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang2 && p.rv.NgayRa <= moi.DenNgayThang2).Sum(p => p.rv.SoNgaydt) / q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang2 && p.rv.NgayRa <= moi.DenNgayThang2).Count() : 0;
                // moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                moi.ThucHienThang3 = (q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang3 && p.rv.NgayRa <= moi.DenNgayThang3).Count() != 0) ? (int)q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang3 && p.rv.NgayRa <= moi.DenNgayThang3).Sum(p => p.rv.SoNgaydt) / q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang3 && p.rv.NgayRa <= moi.DenNgayThang3).Count() : 0;
                _lContent.Add(moi);
                #endregion
                #region 4.Tổng số ngày điều trị
                moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                moi.Stt = 4;
                moi.DanhMuc = "Tổng số ngày điều trị";
                moi.ChiTietDanhMuc = "";
                moi.DVTinh = "Ngày";
                //moi.ChiTieuCaQuy = 6200;
                moi.ThucHienCaQuy = q3.Where(p => p.rv.NgayRa >= moi.TuNgayQuy && p.rv.NgayRa <= moi.DenNgayQuy).Sum(p => p.rv.SoNgaydt);
                //moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                moi.ThucHienThang1 = q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang1 && p.rv.NgayRa <= moi.DenNgayThang1).Sum(p => p.rv.SoNgaydt);
                //moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                moi.ThucHienThang2 = q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang2 && p.rv.NgayRa <= moi.DenNgayThang2).Sum(p => p.rv.SoNgaydt);
                //moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                moi.ThucHienThang3 = q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang3 && p.rv.NgayRa <= moi.DenNgayThang3).Sum(p => p.rv.SoNgaydt);
                //moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                //moi.ThucHienThang1V = q3.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Sum(p => p.rv.SoNgaydt);
                ////moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                //moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                //moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                //moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                //moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                //////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                _lContent.Add(moi);
                #endregion
                #region 5. Ngày sử dụng giường/tháng
                moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                moi.Stt = 5;
                moi.DanhMuc = "Ngày sử dụng giường/tháng";
                moi.ChiTietDanhMuc = "";
                moi.DVTinh = "Ngày";
                //moi.ChiTieuCaQuy = 30;
                _lContent.Add(moi);
                #endregion
                #region 6. Công suất sử dụng giường bệnh
                moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                moi.Stt = 6;
                moi.DanhMuc = "Công suất sử dụng giường bệnh";
                moi.ChiTietDanhMuc = "";
                moi.DVTinh = "%";
                moi.ChiTieuCaQuy = 100;
                _lContent.Add(moi);
                #endregion
                #region 7. Tai biến trong điều trị
                moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                moi.Stt = 7;
                moi.DanhMuc = "Tai biến trong điều trị";
                moi.ChiTietDanhMuc = "";
                moi.DVTinh = "Ca";
                moi.ChiTieuCaQuy = 0;
                _lContent.Add(moi);
                #endregion

                #region Khoa sản
                if (_tenkhoa.ToUpper().Contains("KHOA SẢN"))
                {
                    #region 9. Tổng số khám
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Tổng số khám";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 6500 : 0;
                    moi.ThucHienCaQuy = _luotKham.Where(p => p.NgayKham >= moi.TuNgayQuy && p.NgayKham <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = _luotKham.Where(p => p.NgayKham >= moi.TuNgayThang1 && p.NgayKham <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = _luotKham.Where(p => p.NgayKham >= moi.TuNgayThang2 && p.NgayKham <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = _luotKham.Where(p => p.NgayKham >= moi.TuNgayThang3 && p.NgayKham <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = _luotKham.Where(p => p.NgayKham >= tungayquy4 && p.NgayKham <= denngayquy4).Count();
                    //////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 10.Trong đó khám phụ khoa
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Trong đó khám phụ khoa";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1800 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 11. Cận lâm sàng tại phòng khám sản
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám sản";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám sản";
                    moi.ChiTietDanhMuc = "Huyết học";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 10 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám sản";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 650 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN"))////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    //////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám sản";
                    moi.ChiTietDanhMuc = "Sinh hóa nước tiểu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 100 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám sản";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 6500 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN"))////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    //////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám sản";
                    moi.ChiTietDanhMuc = "Điện tim";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 6500 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN"))////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám sản";
                    moi.ChiTietDanhMuc = "- Tỷ lệ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 15.5 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám sản";
                    moi.ChiTietDanhMuc = "Siêu âm";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 6500 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN"))////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám sản";
                    moi.ChiTietDanhMuc = "- Tỷ lệ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 15.5 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 12. Bệnh nhân điều trị nội trú
                    var q8 = (from rv in data.RaViens.Where(p => _maK == 0 || p.MaKP == _maK)
                              join a in data.BenhNhans.Where(p => p.NoiTru == 1 && p.DTNT == false) on rv.MaBNhan equals a.MaBNhan
                              //group new { a, rv } by new { a.MaBNhan } into kq
                              select new { a.MaBNhan, rv.NgayRa, rv.KetQua, rv.MaBVC, a.Tuoi }).ToList();
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3139 : 0; ;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Khỏi";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 95 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2982 : 0;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Khỏi").Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Đỡ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3.9 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 125 : 0;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Chuyển viện";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1.1 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 32 : 0;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.MaBVC != null).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.MaBVC != null).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.MaBVC != null).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.MaBVC != null).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.MaBVC != null).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân nặng hơn xin về";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 32 : 0;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Nặng hơn").Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân tử vong sơ sinh (đủ tháng)";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 13. Cận lâm sàng (BN nội trú)
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Huyết học";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 100 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3139 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Sinh hóa máu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 796 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 25000 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Sinh hóa nước tiểu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 100 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3139 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Điện tim";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 30 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 942 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Siêu âm";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 100 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3139 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                  || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "X-quang";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 94 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Test HIV";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 80 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2511 : 0;

                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Test khác";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 80 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2511 : 0;

                    _lContent.Add(moi);
                    #endregion
                    #region 14.Mổ
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 14;
                    moi.DanhMuc = "Mổ";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Ca";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 700 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 14;
                    moi.DanhMuc = "Mổ";
                    moi.ChiTietDanhMuc = "+ Loại I";
                    moi.DVTinh = "Ca";
                    moi.ChiTieuCaQuy = null;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 1).Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 1).Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 1).Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 1).Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 14;
                    moi.DanhMuc = "Mổ";
                    moi.ChiTietDanhMuc = "+ Loại II";
                    moi.DVTinh = "Ca";
                    moi.ChiTieuCaQuy = null;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 2).Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 2).Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 2).Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 2).Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 14;
                    moi.DanhMuc = "Mổ";
                    moi.ChiTietDanhMuc = "+ Loại III";
                    moi.DVTinh = "Ca";
                    moi.ChiTieuCaQuy = null;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 3).Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 3).Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 3).Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 3).Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    _lContent.Add(moi);
                    #endregion
                    #region 15. Đẻ thường
                    var bnDe = (from bn in data.BenhNhans
                                join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                                join cls in data.CLS on vv.MaBNhan equals cls.MaBNhan
                                join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                join dv in data.DichVus on cd.MaDV equals dv.MaDV
                                select new
                                {
                                    cls.NgayTH,
                                    dv.TenDV
                                }).Where(p => p.TenDV.Contains("Đỡ đẻ") || p.TenDV.Contains("Phẫu thuật lấy thai") || p.TenDV.Contains("Phá thai")).ToList();
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 15;
                    moi.DanhMuc = "Đẻ thường";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 900 : 0;
                    moi.ThucHienCaQuy = bnDe.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Where(p => p.TenDV.Contains("Đỡ đẻ")).Count();
                    moi.ThucHienThang1 = bnDe.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Where(p => p.TenDV.Contains("Đỡ đẻ")).Count();
                    moi.ThucHienThang2 = bnDe.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Where(p => p.TenDV.Contains("Đỡ đẻ")).Count();
                    moi.ThucHienThang3 = bnDe.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Where(p => p.TenDV.Contains("Đỡ đẻ")).Count();
                    //moi.ThucHienThang1V = bnDe//.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Where(p => p.TenDV.Contains("Đỡ đẻ")).Count();
                    _lContent.Add(moi);
                    #endregion
                    #region 16. Đẻ khó
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 16;
                    moi.DanhMuc = "Đẻ khó";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1000 : 0;
                    moi.ThucHienCaQuy = bnDe.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Where(p => p.TenDV.Contains("Phẫu thuật lấy thai")).Count();
                    moi.ThucHienThang1 = bnDe.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Where(p => p.TenDV.Contains("Phẫu thuật lấy thai")).Count();
                    moi.ThucHienThang2 = bnDe.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Where(p => p.TenDV.Contains("Phẫu thuật lấy thai")).Count();
                    moi.ThucHienThang3 = bnDe.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Where(p => p.TenDV.Contains("Phẫu thuật lấy thai")).Count();
                    //moi.ThucHienThang1V = bnDe//.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Where(p => p.TenDV.Contains("Phẫu thuật lấy thai")).Count();
                    _lContent.Add(moi);
                    #endregion
                    #region 17. Nạo phá thai
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 17;
                    moi.DanhMuc = "Nạo phá thai";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 60 : 0;
                    moi.ThucHienCaQuy = bnDe.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Where(p => p.TenDV.Contains("Phá thai")).Count();
                    moi.ThucHienThang1 = bnDe.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Where(p => p.TenDV.Contains("Phá thai")).Count();
                    moi.ThucHienThang2 = bnDe.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Where(p => p.TenDV.Contains("Phá thai")).Count();
                    moi.ThucHienThang3 = bnDe.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Where(p => p.TenDV.Contains("Phá thai")).Count();
                    //moi.ThucHienThang1V = bnDe//.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Where(p => p.TenDV.Contains("Phá thai")).Count();
                    _lContent.Add(moi);
                    #endregion
                    #region 18. Thủ thuật khác
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 18;
                    moi.DanhMuc = "Thủ thuật khác";
                    moi.DVTinh = "Lần";
                    moi.ChiTietDanhMuc = "";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 2000 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG == "Thủ thuật").Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG == "Thủ thuật").Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG == "Thủ thuật").Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG == "Thủ thuật").Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG == "Thủ thuật")//.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    _lContent.Add(moi);
                    #endregion
                    #region 19. Kiểm tra khoa phòng
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 19;
                    moi.DanhMuc = "Kiểm tra khoa phòng";
                    moi.DVTinh = "Lần";
                    moi.ChiTietDanhMuc = "";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 12 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 20. Kiểm tra tuyến
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 20;
                    moi.DanhMuc = "Kiểm tra tuyến";
                    moi.DVTinh = "Lần";
                    moi.ChiTietDanhMuc = "";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 12 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 21. sinh hoạt bệnh nhân
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 21;
                    moi.DanhMuc = "Sinh hoạt bệnh nhân";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = 24;
                    _lContent.Add(moi);
                    #endregion
                    #region 22. Chiến sĩ thi đua cấp ngành
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 22;
                    moi.DanhMuc = "Chiến sĩ thi đua cấp ngành";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 24. Kỹ thuật mới
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 24;
                    moi.DanhMuc = "Kỹ thuật mới";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "KT";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 25. Thi đua cấp cơ sở
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 25;
                    moi.DanhMuc = "Thi đua cấp cơ sở";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 26. Đề tài NCKH+Sáng kiến
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 26;
                    moi.DanhMuc = "Đề tài NCKH + Sáng kiến";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "ĐT+SK";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 27. Tập huấn chuyên môn xã
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 27;
                    moi.DanhMuc = "Tập huấn chuyên môn xã";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = 2;
                    _lContent.Add(moi);
                    #endregion
                    #region 28. Đề tài NCKH+Sáng kiến
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 28;
                    moi.DanhMuc = "Đè tài NCKH + Sáng kiến";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "ĐT+SK";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 29. Bài tuyên truyền GDSK
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 29;
                    moi.DanhMuc = "Bài tuyên truyền GDSK";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Bài";
                    moi.ChiTieuCaQuy = 10;
                    _lContent.Add(moi);
                    #endregion
                    #region 30. Danh hiệu khen thưởng
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 30;
                    moi.DanhMuc = "Danh hiệu khen thưởng";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                }
                #endregion
                #region Khoa nội
                if (_tenkhoa.ToUpper().Contains("KHOA NỘI"))
                {
                    #region 8. Bệnh nhân điều trị nội trú
                    var q8 = (from rv in data.RaViens.Where(p => _maK == 0 || p.MaKP == _maK)
                              join a in data.BenhNhans.Where(p => p.NoiTru == 1 && p.DTNT == false) on rv.MaBNhan equals a.MaBNhan
                              select new { a.MaBNhan, rv.NgayRa, rv.KetQua, rv.MaBVC, a.Tuoi }).ToList();
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3346 : 0; ;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Khỏi";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 78.7 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2633 : 0;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Khỏi").Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Đỡ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 19 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 636 : 0;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Chuyển viện";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1.9 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 63 : 0;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.MaBVC != null).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.MaBVC != null).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.MaBVC != null).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.MaBVC != null).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.MaBVC != null).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân nặng hơn xin về";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 0.4 : 0; ;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 14 : 0;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Nặng hơn").Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân tử vong";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = 0;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.KetQua == "Tử vong").Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.KetQua == "Tử vong").Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.KetQua == "Tử vong").Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.KetQua == "Tử vong").Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Tử vong").Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 9. Cận lâm sàng (BN nội trú)
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    moi.ChiTieuCaQuy = null;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI"))
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Huyết học";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 100 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3346 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Sinh hóa máu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 20000 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Sinh hóa nước tiểu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 100 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3346 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Đờm";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 100 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Siêu âm";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 54 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1800 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                  || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "X-quang";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 48 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1600 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                    ////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Điện tâm đồ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 90 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3000 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                    ////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Đo chức năng hô hấp";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 400 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                       .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap)).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap)).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap)).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap)).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                    ////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap)).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 10. Kiểm tra khoa phòng
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Kiểm tra khoa phòng";
                    moi.DVTinh = "Lần";
                    moi.ChiTietDanhMuc = "";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 12 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 10. Kiểm tra tuyến
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Kiểm tra tuyến";
                    moi.DVTinh = "Lần";
                    moi.ChiTietDanhMuc = "";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 12 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 11. sinh hoạt bệnh nhân
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Sinh hoạt bệnh nhân";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 12 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 12. Kỹ thuật mới
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Kỹ thuật mới";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "KT";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 13. Chiến sĩ thi đua cấp ngành
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Chiến sĩ thi đua cấp ngành";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 14. Thi đua cấp cơ sở
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 14;
                    moi.DanhMuc = "Thi đua cấp cơ sở";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 15. Đề tài NCKH+Sáng kiến
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 15;
                    moi.DanhMuc = "Đề tài NCKH + Sáng kiến";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "ĐT+SK";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 16. Tập huấn chuyên môn xã
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 16;
                    moi.DanhMuc = "Tập huấn chuyên môn xã";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 17. Đề tài NCKH+Sáng kiến
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 17;
                    moi.DanhMuc = "Đè tài NCKH + Sáng kiến";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "ĐT+SK";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 18. Bài tuyên truyền GDSK
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 18;
                    moi.DanhMuc = "Bài tuyên truyền GDSK";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Bài";
                    moi.ChiTieuCaQuy = 10;
                    _lContent.Add(moi);
                    #endregion
                    #region 19. Danh hiệu khen thưởng
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 19;
                    moi.DanhMuc = "Danh hiệu khen thưởng";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                }
                #endregion
                #region Khoa Hồi sức cấp cứu
                if (_tenkhoa.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || _tenkhoa.ToUpper().Contains("KHOA HSCC - TN"))
                {
                    #region 8. Bệnh nhân điều trị nội trú
                    var q8 = (from rv in data.RaViens.Where(p => _maK == 0 || p.MaKP == _maK)
                              join a in data.BenhNhans.Where(p => p.NoiTru == 1 && p.DTNT == false) on rv.MaBNhan equals a.MaBNhan
                              //group new { a, rv } by new { a.MaBNhan } into kq
                              select new { a.MaBNhan, rv.NgayRa, rv.KetQua, rv.MaBVC, a.Tuoi }).ToList();
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2075 : 0; ;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Count();
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Khỏi";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 60 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1245 : 0;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Khỏi").Count();
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Đỡ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 32 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 664 : 0;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Chuyển viện";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 5 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 104 : 0;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.MaBVC != null).Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.MaBVC != null).Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.MaBVC != null).Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.MaBVC != null).Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.MaBVC != null).Count();
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân nặng hơn xin về";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3 : 0; ;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 62 : 0;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Nặng hơn").Count();
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 9. tai nạn thương tích
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Tổng tai nạn thương tích";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 50 : 0; ;
                    moi.ThucHienCaQuy = qTaiNan.Where(p => p.NNhap >= moi.TuNgayQuy && p.NNhap <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = qTaiNan.Where(p => p.NNhap >= moi.TuNgayThang1 && p.NNhap <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = qTaiNan.Where(p => p.NNhap >= moi.TuNgayThang2 && p.NNhap <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = qTaiNan.Where(p => p.NNhap >= moi.TuNgayThang3 && p.NNhap <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = qTaiNan.Where(p => p.NNhap >= tungayquy4 && p.NNhap <= denngayquy4).Count();
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Tổng tai nạn thương tích";
                    moi.ChiTietDanhMuc = "- Tự tử";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 18 : 0;
                    moi.ThucHienCaQuy = qTaiNan.Where(p => p.NNhap >= moi.TuNgayQuy && p.NNhap <= moi.DenNgayQuy).Where(p => p.ChuyenKhoa == "Tự tử").Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = qTaiNan.Where(p => p.NNhap >= moi.TuNgayThang1 && p.NNhap <= moi.DenNgayThang1).Where(p => p.ChuyenKhoa == "Tự tử").Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = qTaiNan.Where(p => p.NNhap >= moi.TuNgayThang2 && p.NNhap <= moi.DenNgayThang2).Where(p => p.ChuyenKhoa == "Tự tử").Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = qTaiNan.Where(p => p.NNhap >= moi.TuNgayThang3 && p.NNhap <= moi.DenNgayThang3).Where(p => p.ChuyenKhoa == "Tự tử").Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = qTaiNan.Where(p => p.NNhap >= tungayquy4 && p.NNhap <= denngayquy4).Where(p => p.ChuyenKhoa == "Tự tử").Count();
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Tổng tai nạn thương tích";
                    moi.ChiTietDanhMuc = "- Ngộ độc";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 664 : 0;
                    moi.ThucHienCaQuy = qTaiNan.Where(p => p.NNhap >= moi.TuNgayQuy && p.NNhap <= moi.DenNgayQuy).Where(p => p.ChuyenKhoa == "Ngộ độc").Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = qTaiNan.Where(p => p.NNhap >= moi.TuNgayThang1 && p.NNhap <= moi.DenNgayThang1).Where(p => p.ChuyenKhoa == "Ngộ độc").Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = qTaiNan.Where(p => p.NNhap >= moi.TuNgayThang2 && p.NNhap <= moi.DenNgayThang2).Where(p => p.ChuyenKhoa == "Ngộ độc").Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = qTaiNan.Where(p => p.NNhap >= moi.TuNgayThang3 && p.NNhap <= moi.DenNgayThang3).Where(p => p.ChuyenKhoa == "Ngộ độc").Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = qTaiNan.Where(p => p.NNhap >= tungayquy4 && p.NNhap <= denngayquy4).Where(p => p.ChuyenKhoa == "Ngộ độc").Count();
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Tổng tai nạn thương tích";
                    moi.ChiTietDanhMuc = "- Khác";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 104 : 0;
                    moi.ThucHienCaQuy = TNKhac.Where(p => p.NNhap >= moi.TuNgayQuy && p.NNhap <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = TNKhac.Where(p => p.NNhap >= moi.TuNgayThang1 && p.NNhap <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = TNKhac.Where(p => p.NNhap >= moi.TuNgayThang2 && p.NNhap <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = TNKhac.Where(p => p.NNhap >= moi.TuNgayThang3 && p.NNhap <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = TNKhac.Where(p => p.NNhap >= tungayquy4 && p.NNhap <= denngayquy4).Count();
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 10. Cận lâm sàng (BN nội trú)
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Huyết học";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 106 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2200 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                    ////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Sinh hóa máu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 13800 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                    ////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Sinh hóa nước tiểu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 97 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2008 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                    ////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Siêu âm";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 35 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 726 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                  || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                    ////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                    //|| p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "X-quang";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 40 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 830 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                    ////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Điện tâm đồ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 60 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1238 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                    ////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 11. Thủ thuật khác
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Thủ thuật";
                    moi.DVTinh = "Ca";
                    moi.ChiTietDanhMuc = "";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 3118 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN"))
                                        .Where(p => p.TenRG == "Thủ thuật").Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN"))
                                        .Where(p => p.TenRG == "Thủ thuật").Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN"))
                                        .Where(p => p.TenRG == "Thủ thuật").Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN"))
                                        .Where(p => p.TenRG == "Thủ thuật").Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    _lContent.Add(moi);
                    #endregion
                    #region 12. tỷ lệ dùng kháng sinh
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Tỷ lệ dùng kháng sinh";
                    moi.DVTinh = "%";
                    moi.ChiTietDanhMuc = "";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 13. Kiểm tra khoa phòng
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Kiểm tra khoa phòng";
                    moi.DVTinh = "Lần";
                    moi.ChiTietDanhMuc = "";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 24 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 14. Kiểm tra tuyến
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 14;
                    moi.DanhMuc = "Kiểm tra tuyến";
                    moi.DVTinh = "Lần";
                    moi.ChiTietDanhMuc = "";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 12 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 15. sinh hoạt bệnh nhân
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 15;
                    moi.DanhMuc = "Sinh hoạt bệnh nhân";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 24 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 16. Kỹ thuật mới
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 16;
                    moi.DanhMuc = "Kỹ thuật mới";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "KT";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 17. Thi đua cấp cơ sở
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 17;
                    moi.DanhMuc = "Thi đua cấp cơ sở";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 18. Đề tài NCKH+Sáng kiến
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 18;
                    moi.DanhMuc = "Đề tài NCKH + Sáng kiến";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "ĐT+SK";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 19. Tập huấn chuyên môn xã
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 19;
                    moi.DanhMuc = "Tập huấn chuyên môn xã";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 20. Bài tuyên truyền GDSK
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 20;
                    moi.DanhMuc = "Bài tuyên truyền GDSK";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Bài";
                    moi.ChiTieuCaQuy = 2;
                    _lContent.Add(moi);
                    #endregion
                    #region 21. Danh hiệu khen thưởng
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 21;
                    moi.DanhMuc = "Danh hiệu khen thưởng";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                }
                #endregion
                #region Khoa nhi
                if (_tenkhoa.ToUpper().Contains("KHOA NHI"))
                {
                    #region 8. Bệnh nhân điều trị nội trú
                    var q8 = (from rv in data.RaViens.Where(p => _maK == 0 || p.MaKP == _maK)
                              join a in data.BenhNhans.Where(p => p.NoiTru == 1 && p.DTNT == false) on rv.MaBNhan equals a.MaBNhan
                              //group new { a, rv } by new { a.MaBNhan } into kq
                              select new { a.MaBNhan, rv.NgayRa, rv.KetQua, rv.MaBVC, a.Tuoi }).ToList();
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1156 : 0; ;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Count();
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Khỏi";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 80 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 925 : 0;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Khỏi").Count();
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Đỡ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 17 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 197 : 0;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Chuyển viện";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 23 : 0;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.MaBVC != null).Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.MaBVC != null).Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.MaBVC != null).Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.MaBVC != null).Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.MaBVC != null).Count();
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân nặng hơn xin về";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1 : 0; ;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 11 : 0;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Nặng hơn").Count();
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân tử vong";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 0 : 0; ;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 0 : 0;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.KetQua == "Tử vong").Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.KetQua == "Tử vong").Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.KetQua == "Tử vong").Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.KetQua == "Tử vong").Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Tử vong").Count();
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 9. Cận lâm sàng (BN nội trú)
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Huyết học";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 100 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1156 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                    ////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Sinh hóa máu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 578 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                    ////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Sinh hóa nước tiểu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 20 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 231 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                    ////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Siêu âm";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 20 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 231 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                  || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "X-quang";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 10 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 116 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                    ////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Điện tâm đồ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 10 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 116 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                    ////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.DVTinh = "Lần";
                    moi.ChiTietDanhMuc = "Thủ thuật";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 264 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI"))
                                        .Where(p => p.TenRG == "Thủ thuật").Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI"))
                                        .Where(p => p.TenRG == "Thủ thuật").Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI"))
                                        .Where(p => p.TenRG == "Thủ thuật").Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI"))
                                        .Where(p => p.TenRG == "Thủ thuật").Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    _lContent.Add(moi);
                    #endregion
                    #region 10. Kiểm tra khoa phòng
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Kiểm tra khoa phòng";
                    moi.DVTinh = "Lần";
                    moi.ChiTietDanhMuc = "";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 24 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 11. Kiểm tra tuyến
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Kiểm tra tuyến";
                    moi.DVTinh = "Lần";
                    moi.ChiTietDanhMuc = "";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 12 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 12. sinh hoạt bệnh nhân
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Sinh hoạt bệnh nhân";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 12 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 13. Kỹ thuật mới
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Kỹ thuật mới";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "KT";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 14. Thi đua cấp ngành
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 14;
                    moi.DanhMuc = "Chiến sĩ thi đua cấp ngành";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 15. Thi đua cấp cơ sở
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 15;
                    moi.DanhMuc = "Thi đua cấp cơ sở";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 16. Đề tài NCKH+Sáng kiến
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 16;
                    moi.DanhMuc = "Đề tài NCKH + Sáng kiến";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "ĐT+SK";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 17. Tập huấn chuyên môn xã
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 17;
                    moi.DanhMuc = "Tập huấn chuyên môn xã";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 18. Bài tuyên truyền GDSK
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 18;
                    moi.DanhMuc = "Bài tuyên truyền GDSK";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Bài";
                    moi.ChiTieuCaQuy = 2;
                    _lContent.Add(moi);
                    #endregion
                    #region 19. Danh hiệu khen thưởng
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 19;
                    moi.DanhMuc = "Danh hiệu khen thưởng";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                }
                #endregion
                #region Khoa ngoại
                if (_tenkhoa.ToUpper().Contains("KHOA NGOẠI"))
                {
                    #region 8. Bệnh nhân điều trị nội trú
                    var q8 = (from rv in data.RaViens.Where(p => _maK == 0 || p.MaKP == _maK)
                              join a in data.BenhNhans.Where(p => p.NoiTru == 1 && p.DTNT == false) on rv.MaBNhan equals a.MaBNhan
                              //group new { a, rv } by new { a.MaBNhan } into kq
                              select new { a.MaBNhan, rv.NgayRa, rv.KetQua, rv.MaBVC, a.Tuoi }).ToList();
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3212 : 0; ;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Khỏi";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 80 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2570 : 0;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Khỏi").Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Đỡ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 16.7 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 536 : 0;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Chuyển viện";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 96 : 0;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.MaBVC != null).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.MaBVC != null).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.MaBVC != null).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.MaBVC != null).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.MaBVC != null).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân nặng hơn xin về";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 0.3 : 0; ;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 10 : 0;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Nặng hơn").Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân tử vong";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 0 : 0; ;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 0 : 0;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.KetQua == "Tử vong").Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.KetQua == "Tử vong").Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.KetQua == "Tử vong").Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.KetQua == "Tử vong").Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Tử vong").Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 9. Tổng số khám
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Tổng số khám";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 7000 : 0;
                    moi.ThucHienCaQuy = _luotKham.Where(p => p.NgayKham >= moi.TuNgayQuy && p.NgayKham <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = _luotKham.Where(p => p.NgayKham >= moi.TuNgayThang1 && p.NgayKham <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = _luotKham.Where(p => p.NgayKham >= moi.TuNgayThang2 && p.NgayKham <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = _luotKham.Where(p => p.NgayKham >= moi.TuNgayThang3 && p.NgayKham <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = _luotKham.Where(p => p.NgayKham >= tungayquy4 && p.NgayKham <= denngayquy4).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 10. Cận lâm sàng tại phòng khám ngoại CK
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám ngoại CK";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám ngoại";
                    moi.ChiTietDanhMuc = "Huyết học";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 5000 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI"))////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám ngoại";
                    moi.ChiTietDanhMuc = "- Tỷ lệ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 72 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám ngoại";
                    moi.ChiTietDanhMuc = "Sinh hóa nước tiểu";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2800 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI"))////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám ngoại";
                    moi.ChiTietDanhMuc = "- Tỷ lệ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 40 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám ngoại";
                    moi.ChiTietDanhMuc = "Sinh hóa máu";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 18000 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI"))////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám ngoại";
                    moi.ChiTietDanhMuc = "- Tỷ lệ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 258 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám ngoại";
                    moi.ChiTietDanhMuc = "Siêu âm";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2800 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám ngoại";
                    moi.ChiTietDanhMuc = "- Tỷ lệ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 40 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám ngoại";
                    moi.ChiTietDanhMuc = "X-quang";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3500 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI"))////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám ngoại";
                    moi.ChiTietDanhMuc = "- Tỷ lệ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 50 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 11. Cận lâm sàng (BN nội trú)
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Truyền máu";
                    moi.DVTinh = "ui";
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Huyết học";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 100 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3212 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                    ////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Sinh hóa máu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 19000 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                    ////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Sinh hóa nước tiểu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 100 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3212 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                    ////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Siêu âm";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 62 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2000 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                  || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "X-quang";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 62 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2000 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                    ////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Điện tim";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 20 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 640 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                    ////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 12. Phẫu thuật
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Phẫu thuật";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Ca";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 400 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Phẫu thuật";
                    moi.ChiTietDanhMuc = "+ Loại I";
                    moi.DVTinh = "Ca";
                    moi.ChiTieuCaQuy = null;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 1).Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 1).Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 1).Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 1).Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Phẫu thuật";
                    moi.ChiTietDanhMuc = "+ Loại II";
                    moi.DVTinh = "Ca";
                    moi.ChiTieuCaQuy = null;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 2).Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 2).Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 2).Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 2).Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Phẫu thuật";
                    moi.ChiTietDanhMuc = "+ Loại III";
                    moi.DVTinh = "Ca";
                    moi.ChiTieuCaQuy = null;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 3).Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 3).Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 3).Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 3).Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    _lContent.Add(moi);
                    #endregion
                    #region 13. Tổng tiểu phẫu
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Tổng tiểu phẫu";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Ca";
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Tổng tiểu phẫu";
                    moi.ChiTietDanhMuc = "Bó bột";
                    moi.DVTinh = "Ca";
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenDV.Contains("bó bột"))
                                        .Where(p => p.Loai == 1).Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenDV.Contains("bó bột"))
                                        .Where(p => p.Loai == 1).Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenDV.Contains("bó bột"))
                                        .Where(p => p.Loai == 1).Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenDV.Contains("bó bột"))
                                        .Where(p => p.Loai == 1).Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    _lContent.Add(moi);
                    #endregion
                    #region 14. tai nạn thương tích
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 14;
                    moi.DanhMuc = "Tổng tai nạn thương tích";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1000 : 0; ;
                    moi.ThucHienCaQuy = qTaiNan.Where(p => p.NNhap >= moi.TuNgayQuy && p.NNhap <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qTaiNan.Where(p => p.NNhap >= moi.TuNgayThang1 && p.NNhap <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qTaiNan.Where(p => p.NNhap >= moi.TuNgayThang2 && p.NNhap <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qTaiNan.Where(p => p.NNhap >= moi.TuNgayThang3 && p.NNhap <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qTaiNan.Where(p => p.NNhap >= tungayquy4 && p.NNhap <= denngayquy4).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 14;
                    moi.DanhMuc = "Tổng tai nạn thương tích";
                    moi.ChiTietDanhMuc = "- TNGT";
                    moi.DVTinh = "Người";
                    moi.ThucHienCaQuy = qTaiNan.Where(p => p.NNhap >= moi.TuNgayQuy && p.NNhap <= moi.DenNgayQuy).Where(p => p.ChuyenKhoa == "Tai nạn giao thông" || p.ChuyenKhoa == "Đường bộ"
                                                                                                        || p.ChuyenKhoa == "Đường sắt" || p.ChuyenKhoa == "Đường sông").Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = qTaiNan.Where(p => p.NNhap >= moi.TuNgayThang1 && p.NNhap <= moi.DenNgayThang1).Where(p => p.ChuyenKhoa == "Tai nạn giao thông" || p.ChuyenKhoa == "Đường bộ"
                                                                                                        || p.ChuyenKhoa == "Đường sắt" || p.ChuyenKhoa == "Đường sông").Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = qTaiNan.Where(p => p.NNhap >= moi.TuNgayThang2 && p.NNhap <= moi.DenNgayThang2).Where(p => p.ChuyenKhoa == "Tai nạn giao thông" || p.ChuyenKhoa == "Đường bộ"
                                                                                                        || p.ChuyenKhoa == "Đường sắt" || p.ChuyenKhoa == "Đường sông").Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = qTaiNan.Where(p => p.NNhap >= moi.TuNgayThang3 && p.NNhap <= moi.DenNgayThang3).Where(p => p.ChuyenKhoa == "Tai nạn giao thông" || p.ChuyenKhoa == "Đường bộ"
                                                                                                        || p.ChuyenKhoa == "Đường sắt" || p.ChuyenKhoa == "Đường sông").Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 14;
                    moi.DanhMuc = "Tổng tai nạn thương tích";
                    moi.ChiTietDanhMuc = "- TNLĐ";
                    moi.DVTinh = "Người";
                    moi.ThucHienCaQuy = qTaiNan.Where(p => p.NNhap >= moi.TuNgayQuy && p.NNhap <= moi.DenNgayQuy).Where(p => p.ChuyenKhoa == "Tai nạn lao động").Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = qTaiNan.Where(p => p.NNhap >= moi.TuNgayThang1 && p.NNhap <= moi.DenNgayThang1).Where(p => p.ChuyenKhoa == "Tai nạn lao động").Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = qTaiNan.Where(p => p.NNhap >= moi.TuNgayThang2 && p.NNhap <= moi.DenNgayThang2).Where(p => p.ChuyenKhoa == "Tai nạn lao động").Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = qTaiNan.Where(p => p.NNhap >= moi.TuNgayThang3 && p.NNhap <= moi.DenNgayThang3).Where(p => p.ChuyenKhoa == "Tai nạn lao động").Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = qTaiNan.Where(p => p.NNhap >= tungayquy4 && p.NNhap <= denngayquy4).Where(p => p.ChuyenKhoa == "Tai nạn lao động").Count();
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 14;
                    moi.DanhMuc = "Tổng tai nạn thương tích";
                    moi.ChiTietDanhMuc = "- TNSH";
                    moi.DVTinh = "Người";
                    moi.ThucHienCaQuy = qTaiNan.Where(p => p.NNhap >= moi.TuNgayQuy && p.NNhap <= moi.DenNgayQuy).Where(p => p.ChuyenKhoa == "Tai nạn sinh hoạt").Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = qTaiNan.Where(p => p.NNhap >= moi.TuNgayThang1 && p.NNhap <= moi.DenNgayThang1).Where(p => p.ChuyenKhoa == "Tai nạn sinh hoạt").Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = qTaiNan.Where(p => p.NNhap >= moi.TuNgayThang2 && p.NNhap <= moi.DenNgayThang2).Where(p => p.ChuyenKhoa == "Tai nạn sinh hoạt").Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = qTaiNan.Where(p => p.NNhap >= moi.TuNgayThang3 && p.NNhap <= moi.DenNgayThang3).Where(p => p.ChuyenKhoa == "Tai nạn sinh hoạt").Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = qTaiNan.Where(p => p.NNhap >= tungayquy4 && p.NNhap <= denngayquy4).Where(p => p.ChuyenKhoa == "Tai nạn sinh hoạt").Count();
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 14;
                    moi.DanhMuc = "Tổng tai nạn thương tích";
                    moi.ChiTietDanhMuc = "- Đánh nhau";
                    moi.DVTinh = "Người";
                    moi.ThucHienCaQuy = qTaiNan.Where(p => p.NNhap >= moi.TuNgayQuy && p.NNhap <= moi.DenNgayQuy).Where(p => p.ChuyenKhoa == "Đánh nhau").Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = qTaiNan.Where(p => p.NNhap >= moi.TuNgayThang1 && p.NNhap <= moi.DenNgayThang1).Where(p => p.ChuyenKhoa == "Đánh nhau").Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = qTaiNan.Where(p => p.NNhap >= moi.TuNgayThang2 && p.NNhap <= moi.DenNgayThang2).Where(p => p.ChuyenKhoa == "Đánh nhau").Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = qTaiNan.Where(p => p.NNhap >= moi.TuNgayThang3 && p.NNhap <= moi.DenNgayThang3).Where(p => p.ChuyenKhoa == "Đánh nhau").Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = qTaiNan.Where(p => p.NNhap >= tungayquy4 && p.NNhap <= denngayquy4).Where(p => p.ChuyenKhoa == "Đánh nhau").Count();
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 14;
                    moi.DanhMuc = "Tổng tai nạn thương tích";
                    moi.ChiTietDanhMuc = "- Bỏng";
                    moi.DVTinh = "Người";
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 14;
                    moi.DanhMuc = "Tổng tai nạn thương tích";
                    moi.ChiTietDanhMuc = "- Khác";
                    moi.DVTinh = "Người";
                    moi.ThucHienCaQuy = qTaiNan.Where(p => p.NNhap >= moi.TuNgayQuy && p.NNhap <= moi.DenNgayQuy).Where(p => p.ChuyenKhoa == "Khác").Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = qTaiNan.Where(p => p.NNhap >= moi.TuNgayThang1 && p.NNhap <= moi.DenNgayThang1).Where(p => p.ChuyenKhoa == "Khác").Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = qTaiNan.Where(p => p.NNhap >= moi.TuNgayThang2 && p.NNhap <= moi.DenNgayThang2).Where(p => p.ChuyenKhoa == "Khác").Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = qTaiNan.Where(p => p.NNhap >= moi.TuNgayThang3 && p.NNhap <= moi.DenNgayThang3).Where(p => p.ChuyenKhoa == "Khác").Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = qTaiNan.Where(p => p.NNhap >= tungayquy4 && p.NNhap <= denngayquy4).Where(p => p.ChuyenKhoa == "Khác").Count();
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 15. Kiểm tra khoa phòng
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 15;
                    moi.DanhMuc = "Kiểm tra khoa phòng";
                    moi.DVTinh = "Lần";
                    moi.ChiTietDanhMuc = "";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 12 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 16. sinh hoạt bệnh nhân
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 16;
                    moi.DanhMuc = "Sinh hoạt bệnh nhân";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 12 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 17. Kỹ thuật mới
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 17;
                    moi.DanhMuc = "Kỹ thuật mới";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "KT";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 18. Chuyển giao kỹ thuật cho tuyến xã
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 18;
                    moi.DanhMuc = "Chuyển giao kỹ thuật cho tuyến xã";
                    moi.DVTinh = "KT";
                    moi.ChiTietDanhMuc = "";
                    _lContent.Add(moi);
                    #endregion
                    #region 19. Thi đua cấp ngành
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 19;
                    moi.DanhMuc = "Chiến sĩ thi đua cấp ngành";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 20. Thi đua cấp cơ sở
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 20;
                    moi.DanhMuc = "Thi đua cấp cơ sở";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 21. Đề tài NCKH+Sáng kiến
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 21;
                    moi.DanhMuc = "Đề tài NCKH + Sáng kiến";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "ĐT+SK";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 22. Bài tuyên truyền GDSK
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 22;
                    moi.DanhMuc = "Bài tuyên truyền GDSK";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Bài";
                    moi.ChiTieuCaQuy = 2;
                    _lContent.Add(moi);
                    #endregion
                    #region 24. Danh hiệu khen thưởng
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 24;
                    moi.DanhMuc = "Danh hiệu khen thưởng";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                }
                #endregion
                #region Khoa truyền nhiễm
                if (_tenkhoa.ToUpper().Contains("KHOA TRUYỀN NHIỄM"))
                {
                    #region 8. Bệnh nhân điều trị nội trú
                    var q8 = (from rv in data.RaViens.Where(p => _maK == 0 || p.MaKP == _maK)
                              join a in data.BenhNhans.Where(p => p.NoiTru == 1 && p.DTNT == false) on rv.MaBNhan equals a.MaBNhan
                              //group new { a, rv } by new { a.MaBNhan } into kq
                              select new { a.MaBNhan, rv.NgayRa, rv.KetQua, rv.MaBVC, a.Tuoi }).ToList();
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 590 : 0; ;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Khỏi";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 80 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 472 : 0;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Khỏi").Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Đỡ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 13.5 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 80 : 0;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Chuyển viện";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 5 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 30 : 0;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.MaBVC != null).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.MaBVC != null).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.MaBVC != null).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.MaBVC != null).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.MaBVC != null).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân nặng hơn xin về";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1.5 : 0; ;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 10 : 0;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Nặng hơn").Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 9. Cận lâm sàng (BN nội trú)
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Huyết học";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 100 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 590 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                    ////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Sinh hóa máu";
                    moi.DVTinh = "%";
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1770 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                    ////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Sinh hóa nước tiểu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 100 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 590 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                    ////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Siêu âm";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 20 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 118 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                  || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "X-quang";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 40 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 236 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                    ////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Điện tim";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 20 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 118 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                    ////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 10. Số người XN AFB
                    var q1 = (from cls in data.CLS
                              join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                              join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                              join kp in data.KPhongs on cls.MaKPth equals kp.MaKP
                              select new
                              {
                                  clsct.MaDVct,
                                  cls.NgayTH,
                                  cls.MaBNhan,
                                  kp.TenKP
                              }).ToList();

                    var q2 = (from a in q1
                              join dvct in data.DichVucts on a.MaDVct equals dvct.MaDVct
                              join dv in data.DichVus on dvct.MaDV equals dv.MaDV
                              join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                              select new
                              {
                                  a.NgayTH,
                                  tn.TenRG,
                                  dv.SoTT,
                                  dv.TenDV,
                                  a.MaBNhan,
                                  a.TenKP
                              }).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom)
                                  .Where(p => p.SoTT == 1 || p.SoTT == 2)
                                  .Where(p => p.TenDV.Contains("AFB")).ToList();

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Số người XN AFB";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 200 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Số người XN AFB";
                    moi.ChiTietDanhMuc = "Số tiêu bản";
                    moi.DVTinh = "T.bản";
                    moi.ChiTieuCaQuy = null;
                    moi.ThucHienCaQuy = q2.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM"))
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.ThucHienThang1 = q2.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM"))
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.ThucHienThang2 = q2.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM"))
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.ThucHienThang3 = q2.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM"))
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    _lContent.Add(moi);
                    #endregion
                }
                #endregion
                #region Khoa YHCT-PHCN
                if (_tenkhoa.ToUpper().Contains("KHOA ĐÔNG Y") || _tenkhoa.ToUpper().Contains("KHOA YHCT _ PHCN"))
                {
                    #region 8. Tổng số bệnh nhân điều trị nội trú
                    var q8 = (from rv in data.RaViens.Where(p => _maK == 0 || p.MaKP == _maK)
                              join a in data.BenhNhans.Where(p => p.NoiTru == 1 && p.DTNT == false) on rv.MaBNhan equals a.MaBNhan
                              //group new { a, rv } by new { a.MaBNhan } into kq
                              select new { a.MaBNhan, rv.NgayRa, rv.KetQua, rv.MaBVC, a.Tuoi }).ToList();
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 620 : 0; ;
                    moi.ThucHienCaQuy = q8.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = q8.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = q8.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = q8.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Count();
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 9. Tỷ lệ điều trị khỏi
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Tỷ lệ điều trị Khỏi";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = 80;
                    moi.ThucHienCaQuy = (q3.Where(p => p.rv.NgayRa >= moi.TuNgayQuy && p.rv.NgayRa <= moi.DenNgayQuy).Count() != 0) ? q3.Where(p => p.rv.NgayRa >= moi.TuNgayQuy && p.rv.NgayRa <= moi.DenNgayQuy).Where(p => p.rv.KetQua == "Khỏi").Count() / q3.Where(p => p.rv.NgayRa >= moi.TuNgayQuy && p.rv.NgayRa <= moi.DenNgayQuy).Count() * 100 : 0;
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = (q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang1 && p.rv.NgayRa <= moi.DenNgayThang1).Count() != 0) ? q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang1 && p.rv.NgayRa <= moi.DenNgayThang1).Where(p => p.rv.KetQua == "Khỏi").Count() / q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang1 && p.rv.NgayRa <= moi.DenNgayThang1).Count() * 100 : 0;
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = (q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang2 && p.rv.NgayRa <= moi.DenNgayThang2).Count() != 0) ? q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang2 && p.rv.NgayRa <= moi.DenNgayThang2).Where(p => p.rv.KetQua == "Khỏi").Count() / q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang2 && p.rv.NgayRa <= moi.DenNgayThang2).Count() * 100 : 0;
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = (q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang3 && p.rv.NgayRa <= moi.DenNgayThang3).Count() != 0) ? q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang3 && p.rv.NgayRa <= moi.DenNgayThang3).Where(p => p.rv.KetQua == "Khỏi").Count() / q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang3 && p.rv.NgayRa <= moi.DenNgayThang3).Count() * 100 : 0;
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = (q3.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Count() != 0) ? q3.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Where(p => p.rv.KetQua == "Khỏi").Count() / q3.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Count() * 100 : 0;
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Tỷ lệ điều trị Khỏi";
                    moi.ChiTietDanhMuc = "Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = 496;
                    moi.ThucHienCaQuy = q3.Where(p => p.rv.NgayRa >= moi.TuNgayQuy && p.rv.NgayRa <= moi.DenNgayQuy).Where(p => p.rv.KetQua == "Khỏi").Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang1 && p.rv.NgayRa <= moi.DenNgayThang1).Where(p => p.rv.KetQua == "Khỏi").Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang2 && p.rv.NgayRa <= moi.DenNgayThang2).Where(p => p.rv.KetQua == "Khỏi").Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang3 && p.rv.NgayRa <= moi.DenNgayThang3).Where(p => p.rv.KetQua == "Khỏi").Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = q3.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Where(p => p.rv.KetQua == "Khỏi").Count();
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 10. Tỷ lệ điều trị đỡ
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Tỷ lệ điều trị Đỡ";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = 18;
                    moi.ThucHienCaQuy = (q3.Where(p => p.rv.NgayRa >= moi.TuNgayQuy && p.rv.NgayRa <= moi.DenNgayQuy).Count() != 0) ? q3.Where(p => p.rv.NgayRa >= moi.TuNgayQuy && p.rv.NgayRa <= moi.DenNgayQuy).Where(p => p.rv.KetQua == "Đỡ|Giảm").Count() / q3.Where(p => p.rv.NgayRa >= moi.TuNgayQuy && p.rv.NgayRa <= moi.DenNgayQuy).Count() * 100 : 0;
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = (q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang1 && p.rv.NgayRa <= moi.DenNgayThang1).Count() != 0) ? q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang1 && p.rv.NgayRa <= moi.DenNgayThang1).Where(p => p.rv.KetQua == "Đỡ|Giảm").Count() / q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang1 && p.rv.NgayRa <= moi.DenNgayThang1).Count() * 100 : 0;
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = (q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang2 && p.rv.NgayRa <= moi.DenNgayThang2).Count() != 0) ? q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang2 && p.rv.NgayRa <= moi.DenNgayThang2).Where(p => p.rv.KetQua == "Đỡ|Giảm").Count() / q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang2 && p.rv.NgayRa <= moi.DenNgayThang2).Count() * 100 : 0;
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = (q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang3 && p.rv.NgayRa <= moi.DenNgayThang3).Count() != 0) ? q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang3 && p.rv.NgayRa <= moi.DenNgayThang3).Where(p => p.rv.KetQua == "Đỡ|Giảm").Count() / q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang3 && p.rv.NgayRa <= moi.DenNgayThang3).Count() * 100 : 0;
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = (q3.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Count() != 0) ? q3.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Where(p => p.rv.KetQua == "Đỡ|Giảm").Count() / q3.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Count() * 100 : 0;
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Tỷ lệ điều trị Đỡ";
                    moi.ChiTietDanhMuc = "Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = 112;
                    moi.ThucHienCaQuy = q3.Where(p => p.rv.NgayRa >= moi.TuNgayQuy && p.rv.NgayRa <= moi.DenNgayQuy).Where(p => p.rv.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang1 && p.rv.NgayRa <= moi.DenNgayThang1).Where(p => p.rv.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang2 && p.rv.NgayRa <= moi.DenNgayThang2).Where(p => p.rv.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang3 && p.rv.NgayRa <= moi.DenNgayThang3).Where(p => p.rv.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = q3.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Where(p => p.rv.KetQua == "Đỡ|Giảm").Count();
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 11. Tỷ lệ chuyển viện
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Tỷ lệ chuyển viện";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = 2;
                    moi.ThucHienCaQuy = (q3.Where(p => p.rv.NgayRa >= moi.TuNgayQuy && p.rv.NgayRa <= moi.DenNgayQuy).Count() != 0) ? q3.Where(p => p.rv.NgayRa >= moi.TuNgayQuy && p.rv.NgayRa <= moi.DenNgayQuy).Where(p => p.rv.MaBVC != null).Count() / q3.Where(p => p.rv.NgayRa >= moi.TuNgayQuy && p.rv.NgayRa <= moi.DenNgayQuy).Count() * 100 : 0;
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = (q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang1 && p.rv.NgayRa <= moi.DenNgayThang1).Count() != 0) ? q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang1 && p.rv.NgayRa <= moi.DenNgayThang1).Where(p => p.rv.MaBVC != null).Count() / q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang1 && p.rv.NgayRa <= moi.DenNgayThang1).Count() * 100 : 0;
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = (q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang2 && p.rv.NgayRa <= moi.DenNgayThang2).Count() != 0) ? q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang2 && p.rv.NgayRa <= moi.DenNgayThang2).Where(p => p.rv.MaBVC != null).Count() / q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang2 && p.rv.NgayRa <= moi.DenNgayThang2).Count() * 100 : 0;
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = (q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang3 && p.rv.NgayRa <= moi.DenNgayThang3).Count() != 0) ? q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang3 && p.rv.NgayRa <= moi.DenNgayThang3).Where(p => p.rv.MaBVC != null).Count() / q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang3 && p.rv.NgayRa <= moi.DenNgayThang3).Count() * 100 : 0;
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = (q3.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Count() != 0) ? q3.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Where(p => p.rv.MaBVC != null).Count() / q3.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Count() * 100 : 0;
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Tỷ lệ chuyển viện";
                    moi.ChiTietDanhMuc = "Tổng số chuyển viện";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = 12;
                    moi.ThucHienCaQuy = q3.Where(p => p.rv.NgayRa >= moi.TuNgayQuy && p.rv.NgayRa <= moi.DenNgayQuy).Where(p => p.rv.MaBVC != null).Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang1 && p.rv.NgayRa <= moi.DenNgayThang1).Where(p => p.rv.MaBVC != null).Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang2 && p.rv.NgayRa <= moi.DenNgayThang2).Where(p => p.rv.MaBVC != null).Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = q3.Where(p => p.rv.NgayRa >= moi.TuNgayThang3 && p.rv.NgayRa <= moi.DenNgayThang3).Where(p => p.rv.MaBVC != null).Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = q3.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Where(p => p.rv.MaBVC != null).Count();
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 12. Cận lâm sàn bệnh phòng
                    var q12_1 = (from cd in data.ChiDinhs
                                 join dv in data.DichVus on cd.MaDV equals dv.MaDV
                                 join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                 join dtct in data.DThuoccts on cd.IDCD equals dtct.IDCD
                                 join cls in data.CLS on cd.IdCLS equals cls.IdCLS
                                 select new { cd.Status, tn.TenRG, dv.Loai, dtct.IDCD, cls.NgayTH, cls.MaBNhan, cls.MaKP }).Where(p => p.Status == 1 && p.IDCD != null).ToList();

                    var q12_2 = (from n in q12_1.Where(p => _maK == 0 || p.MaKP == _maK)
                                 join bn in data.BenhNhans on n.MaBNhan equals bn.MaBNhan
                                 select new
                                 {
                                     n.Status,
                                     n.TenRG,
                                     n.Loai,
                                     n.IDCD,
                                     n.NgayTH,
                                     n.MaBNhan,
                                     bn.NoiTru,
                                     bn.DTNT
                                 }).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc ||
                                               p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau ||
                                               p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu ||
                                               p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim ||
                                               p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm ||
                                               p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler ||
                                               p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang ||
                                               p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap ||
                                               p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).ToList();

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "";
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "Tỷ lệ huyết học";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = 100;
                    moi.ThucHienCaQuy = (q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == "XN huyết học").Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count() * 100 : 0;
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = (q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == "XN huyết học").Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count() * 100 : 0;
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = (q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == "XN huyết học").Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count() * 100 : 0;
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = (q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == "XN huyết học").Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count() * 100 : 0;
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = (q12_2//.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count() != 0) ? q12_2////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == "XN huyết học").Count() / q12_2//.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count() * 100 : 0;
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "lần";
                    moi.ChiTieuCaQuy = 620;
                    moi.ThucHienCaQuy = q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == "XN huyết học").Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == "XN huyết học").Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == "XN huyết học").Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == "XN huyết học").Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = q12_2////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == "XN huyết học").Count();
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "Sinh hóa máu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = 200;
                    moi.ThucHienCaQuy = (q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count() * 100 : 0;
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = (q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count() * 100 : 0;
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = (q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count() * 100 : 0;
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = (q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count() * 100 : 0;
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = (q12_2//.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count() != 0) ? q12_2////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count() / q12_2//.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count() * 100 : 0;
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "lần";
                    moi.ChiTieuCaQuy = 1200;
                    moi.ThucHienCaQuy = q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = q12_2////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "Sinh hóa nước tiểu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = 100;
                    moi.ThucHienCaQuy = (q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count() * 100 : 0;
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = (q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count() * 100 : 0;
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = (q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count() * 100 : 0;
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = (q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count() * 100 : 0;
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = (q12_2//.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count() != 0) ? q12_2////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count() / q12_2//.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count() * 100 : 0;
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "lần";
                    moi.ChiTieuCaQuy = 620;
                    moi.ThucHienCaQuy = q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = q12_2////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "Điện tâm đồ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = 30;
                    moi.ThucHienCaQuy = (q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count() * 100 : 0;
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = (q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count() * 100 : 0;
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = (q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count() * 100 : 0;
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = (q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count() * 100 : 0;
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = (q12_2//.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count() != 0) ? q12_2////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count() / q12_2//.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count() * 100 : 0;
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "lần";
                    moi.ChiTieuCaQuy = 180;
                    moi.ThucHienCaQuy = q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = q12_2////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "Siêu âm";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = 20;
                    moi.ThucHienCaQuy = (q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm ||
                                             p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count() * 100 : 0;
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = (q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm ||
                                             p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count() * 100 : 0;
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = (q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm ||
                                             p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count() * 100 : 0;
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = (q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm ||
                                             p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count() * 100 : 0;
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "lần";
                    moi.ChiTieuCaQuy = 124;
                    moi.ThucHienCaQuy = q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm ||
                                             p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm ||
                                             p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm ||
                                             p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm ||
                                             p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "X-quang";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = 20;
                    moi.ThucHienCaQuy = (q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count() * 100 : 0;
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = (q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count() * 100 : 0;
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = (q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count() * 100 : 0;
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = (q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count() * 100 : 0;
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = (q12_2//.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count() != 0) ? q12_2////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count() / q12_2//.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count() * 100 : 0;
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "lần";
                    moi.ChiTieuCaQuy = 124;
                    moi.ThucHienCaQuy = q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = q12_2////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "Đo Chức năng hô hấp";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = 10;
                    moi.ThucHienCaQuy = (q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count() * 100 : 0;
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = (q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count() * 100 : 0;
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = (q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count() * 100 : 0;
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = (q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count() * 100 : 0;
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = (q12_2//.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count() != 0) ? q12_2////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count() / q12_2//.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count() * 100 : 0;
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "lần";
                    moi.ChiTieuCaQuy = 62;
                    moi.ThucHienCaQuy = q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = q12_2////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count();
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "Đo mật độ xương";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = 50;
                    moi.ThucHienCaQuy = (q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count() * 100 : 0;
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = (q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count() * 100 : 0;
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = (q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count() * 100 : 0;
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = (q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count() != 0) ? q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count() / q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count() * 100 : 0;
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = (q12_2//.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count() != 0) ? q12_2////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count() / q12_2//.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count() * 100 : 0;
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "lần";
                    moi.ChiTieuCaQuy = 310;
                    moi.ThucHienCaQuy = q12_2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = q12_2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = q12_2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = q12_2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = q12_2////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count();
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 13. Bệnh nhân khám YHCT
                    var kpdongy = (from kp in _khoaMoi.Where(p => _maK == 0 || p.makp == _maK) join ck in data.ChuyenKhoas.Where(p => p.TenCK == "Đông y") on kp.mack equals ck.MaCK select kp).ToList();
                    var q13 = (from bn in q3
                               join bnkb in data.BNKBs on bn.bn.MaBNhan equals bnkb.MaBNhan
                               join kp in kpdongy on bnkb.MaKP equals kp.makp
                               join dtbn in data.DTBNs on bn.bn.IDDTBN equals dtbn.IDDTBN
                               select new { bn, dtbn, bn.rv }).Distinct().ToList();

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Bệnh nhân khám YHCT";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Bệnh nhân khám YHCT";
                    moi.ChiTietDanhMuc = "Bệnh nhân khám BHYT";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = 1800;
                    moi.ThucHienCaQuy = q13.Where(p => p.rv.NgayRa >= moi.TuNgayQuy && p.rv.NgayRa <= moi.DenNgayQuy).Where(p => p.dtbn.DTBN1.Contains("BHYT")).Count();
                    moi.ThucHienThang1 = q13.Where(p => p.rv.NgayRa >= moi.TuNgayThang1 && p.rv.NgayRa <= moi.DenNgayThang1).Where(p => p.dtbn.DTBN1.Contains("BHYT")).Count();
                    moi.ThucHienThang2 = q13.Where(p => p.rv.NgayRa >= moi.TuNgayThang2 && p.rv.NgayRa <= moi.DenNgayThang2).Where(p => p.dtbn.DTBN1.Contains("BHYT")).Count();
                    moi.ThucHienThang3 = q13.Where(p => p.rv.NgayRa >= moi.TuNgayThang3 && p.rv.NgayRa <= moi.DenNgayThang3).Where(p => p.dtbn.DTBN1.Contains("BHYT")).Count();
                    //moi.ThucHienThang1V = q13.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Where(p => p.dtbn.DTBN1.Contains("BHYT")).Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Bệnh nhân khám YHCT";
                    moi.ChiTietDanhMuc = "Tỷ lệ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = 80;
                    moi.ThucHienCaQuy = (q13.Where(p => p.rv.NgayRa >= moi.TuNgayQuy && p.rv.NgayRa <= moi.DenNgayQuy).Count() != 0) ? q13.Where(p => p.rv.NgayRa >= moi.TuNgayQuy && p.rv.NgayRa <= moi.DenNgayQuy).Where(p => p.dtbn.DTBN1.Contains("BHYT")).Count() / q13.Where(p => p.rv.NgayRa >= moi.TuNgayQuy && p.rv.NgayRa <= moi.DenNgayQuy).Count() * 100 : 0;
                    moi.ThucHienThang1 = (q13.Where(p => p.rv.NgayRa >= moi.TuNgayThang1 && p.rv.NgayRa <= moi.DenNgayThang1).Count() != 0) ? q13.Where(p => p.rv.NgayRa >= moi.TuNgayThang1 && p.rv.NgayRa <= moi.DenNgayThang1).Where(p => p.dtbn.DTBN1.Contains("BHYT")).Count() / q13.Where(p => p.rv.NgayRa >= moi.TuNgayThang1 && p.rv.NgayRa <= moi.DenNgayThang1).Count() * 100 : 0;
                    moi.ThucHienThang2 = (q13.Where(p => p.rv.NgayRa >= moi.TuNgayThang2 && p.rv.NgayRa <= moi.DenNgayThang2).Count() != 0) ? q13.Where(p => p.rv.NgayRa >= moi.TuNgayThang2 && p.rv.NgayRa <= moi.DenNgayThang2).Where(p => p.dtbn.DTBN1.Contains("BHYT")).Count() / q13.Where(p => p.rv.NgayRa >= moi.TuNgayThang2 && p.rv.NgayRa <= moi.DenNgayThang2).Count() * 100 : 0;
                    moi.ThucHienThang3 = (q13.Where(p => p.rv.NgayRa >= moi.TuNgayThang3 && p.rv.NgayRa <= moi.DenNgayThang3).Count() != 0) ? q13.Where(p => p.rv.NgayRa >= moi.TuNgayThang3 && p.rv.NgayRa <= moi.DenNgayThang3).Where(p => p.dtbn.DTBN1.Contains("BHYT")).Count() / q13.Where(p => p.rv.NgayRa >= moi.TuNgayThang3 && p.rv.NgayRa <= moi.DenNgayThang3).Count() * 100 : 0;
                    //moi.ThucHienThang1V = (q13.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Count() != 0) ? q13.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Where(p => p.dtbn.DTBN1.Contains("BHYT")).Count() / q13.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Count() * 100 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Bệnh nhân khám YHCT";
                    moi.ChiTietDanhMuc = "Bệnh nhân khám nhân dân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = 200;
                    moi.ThucHienCaQuy = q13.Where(p => p.rv.NgayRa >= moi.TuNgayQuy && p.rv.NgayRa <= moi.DenNgayQuy).Where(p => p.dtbn.DTBN1.Contains("Dịch vụ")).Count();
                    moi.ThucHienThang1 = q13.Where(p => p.rv.NgayRa >= moi.TuNgayThang1 && p.rv.NgayRa <= moi.DenNgayThang1).Where(p => p.dtbn.DTBN1.Contains("Dịch vụ")).Count();
                    moi.ThucHienThang2 = q13.Where(p => p.rv.NgayRa >= moi.TuNgayThang2 && p.rv.NgayRa <= moi.DenNgayThang2).Where(p => p.dtbn.DTBN1.Contains("Dịch vụ")).Count();
                    moi.ThucHienThang3 = q13.Where(p => p.rv.NgayRa >= moi.TuNgayThang3 && p.rv.NgayRa <= moi.DenNgayThang3).Where(p => p.dtbn.DTBN1.Contains("Dịch vụ")).Count();
                    //moi.ThucHienThang1V = q13.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Where(p => p.dtbn.DTBN1.Contains("Dịch vụ")).Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Bệnh nhân khám YHCT";
                    moi.ChiTietDanhMuc = "Tỷ lệ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = 20;
                    moi.ThucHienCaQuy = (q13.Where(p => p.rv.NgayRa >= moi.TuNgayQuy && p.rv.NgayRa <= moi.DenNgayQuy).Count() != 0) ? q13.Where(p => p.rv.NgayRa >= moi.TuNgayQuy && p.rv.NgayRa <= moi.DenNgayQuy).Where(p => p.dtbn.DTBN1.Contains("Dịch vụ")).Count() / q13.Where(p => p.rv.NgayRa >= moi.TuNgayQuy && p.rv.NgayRa <= moi.DenNgayQuy).Count() * 100 : 0;
                    moi.ThucHienThang1 = (q13.Where(p => p.rv.NgayRa >= moi.TuNgayThang1 && p.rv.NgayRa <= moi.DenNgayThang1).Count() != 0) ? q13.Where(p => p.rv.NgayRa >= moi.TuNgayThang1 && p.rv.NgayRa <= moi.DenNgayThang1).Where(p => p.dtbn.DTBN1.Contains("Dịch vụ")).Count() / q13.Where(p => p.rv.NgayRa >= moi.TuNgayThang1 && p.rv.NgayRa <= moi.DenNgayThang1).Count() * 100 : 0;
                    moi.ThucHienThang2 = (q13.Where(p => p.rv.NgayRa >= moi.TuNgayThang2 && p.rv.NgayRa <= moi.DenNgayThang2).Count() != 0) ? q13.Where(p => p.rv.NgayRa >= moi.TuNgayThang2 && p.rv.NgayRa <= moi.DenNgayThang2).Where(p => p.dtbn.DTBN1.Contains("Dịch vụ")).Count() / q13.Where(p => p.rv.NgayRa >= moi.TuNgayThang2 && p.rv.NgayRa <= moi.DenNgayThang2).Count() * 100 : 0;
                    moi.ThucHienThang3 = (q13.Where(p => p.rv.NgayRa >= moi.TuNgayThang3 && p.rv.NgayRa <= moi.DenNgayThang3).Count() != 0) ? q13.Where(p => p.rv.NgayRa >= moi.TuNgayThang3 && p.rv.NgayRa <= moi.DenNgayThang3).Where(p => p.dtbn.DTBN1.Contains("Dịch vụ")).Count() / q13.Where(p => p.rv.NgayRa >= moi.TuNgayThang3 && p.rv.NgayRa <= moi.DenNgayThang3).Count() * 100 : 0;
                    //moi.ThucHienThang1V = (q13.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Count() != 0) ? q13.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Where(p => p.dtbn.DTBN1.Contains("Dịch vụ")).Count() / q13.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Count() * 100 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 14. Bệnh nhân điều trị ngoại trú
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 14;
                    moi.DanhMuc = "Bệnh nhân điều trị ngoại trú";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = 850;
                    moi.ThucHienCaQuy = bnNgoaitru.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang1 = bnNgoaitru.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang2 = bnNgoaitru.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100;
                    moi.ThucHienThang3 = bnNgoaitru.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100;
                    //moi.ThucHienThang1V = bnNgoaitru.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Count();
                    //moi.PtramQuyIV = (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    #endregion
                    #region 15. Tỷ lệ dùng thuốc thang
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 15;
                    moi.DanhMuc = "Tỷ lệ dùng thuốc thang";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = 95;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 15;
                    moi.DanhMuc = "Tỷ lệ dùng thuốc thang";
                    moi.ChiTietDanhMuc = "Tỷ lệ dùng thuốc thang sắc uống tại viện";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = 100;
                    _lContent.Add(moi);
                    #endregion
                    #region 16.Tỷ lệ điều trị kết hợp
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 16;
                    moi.DanhMuc = "Tỷ lệ điều trị kết hợp";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = 80;
                    _lContent.Add(moi);
                    #endregion
                    #region 17. Tỷ lệ bệnh nhân châm cứu
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 17;
                    moi.DanhMuc = "Tỷ lệ bệnh nhân châm cứu";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = 95;
                    _lContent.Add(moi);
                    #endregion
                    #region 18. Số bệnh nhân châm cứu
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 18;
                    moi.DanhMuc = "Số bệnh nhân châm cứu";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = 1000;
                    _lContent.Add(moi);
                    #endregion
                    #region 19. Thủ thuật khác
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 19;
                    moi.DanhMuc = "Thủ thuật khác";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "ca";
                    moi.ChiTieuCaQuy = 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 20. Kiểm tra khoa phòng
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 20;
                    moi.DanhMuc = "Kiểm tra khoa phòng";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = 12;
                    _lContent.Add(moi);
                    #endregion
                    #region 21. Kiểm tra tuyến
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 21;
                    moi.DanhMuc = "Kiểm tra tuyến";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = 12;
                    _lContent.Add(moi);
                    #endregion
                    #region 22. sinh hoạt bệnh nhân
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 22;
                    moi.DanhMuc = "Sinh hoạt bệnh nhân";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = 24;
                    _lContent.Add(moi);
                    #endregion
                    #region 24. bài tuyên truyền
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 24;
                    moi.DanhMuc = "Bài tuyên truyền";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Bài";
                    moi.ChiTieuCaQuy = 2;
                    _lContent.Add(moi);
                    #endregion
                    #region 25. Kỹ thuật mới
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 25;
                    moi.DanhMuc = "Kỹ thuật mới";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "KT";

                    _lContent.Add(moi);
                    #endregion
                    #region 26. Chiến sĩ thi đua cấp cơ sở
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 26;
                    moi.DanhMuc = "Chiến sĩ thi đua cấp cơ sở";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 27. Đề tài NCKH+Sáng kiến
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 27;
                    moi.DanhMuc = "Đề tài NCKH + Sáng kiến";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "ĐT+SK";
                    moi.ChiTieuCaQuy = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 28. Tập huấn chuyên môn xã
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 28;
                    moi.DanhMuc = "Tập huấn chuyên môn xã";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = 2;
                    _lContent.Add(moi);
                    #endregion
                    #region 29. Danh hiệu thi đua
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 29;
                    moi.DanhMuc = "Danh hiệu thi đua";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    _lContent.Add(moi);
                    #endregion
                }
                #endregion
                #region 23. Cá nhân đạt lao động tiên tiến
                moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                moi.Stt = 23;
                moi.DanhMuc = "Cá nhân đạt lao động tiên tiến";
                moi.ChiTietDanhMuc = "";
                moi.DVTinh = "%";
                moi.ChiTieuCaQuy = 100;
                _lContent.Add(moi);
                #endregion
            }
            else
            {
                #region Khoa chẩn đoán hình ảnh
                if (_tenkhoa.ToUpper().Contains("KHOA CHẨN ĐOÁN HÌNH ẢNH"))
                {
                    #region query
                    var _kphong = (from k in data.KPhongs.Where(p => p.ChuyenKhoa.Contains("Siêu âm") || p.ChuyenKhoa.Contains("X-Quang") || p.ChuyenKhoa.Contains("Nội soi")
                                                                     || p.ChuyenKhoa.Contains("Xét nghiệm") || p.ChuyenKhoa.Contains("Nội soi Tai-Mũi-Họng") || p.ChuyenKhoa.Contains("Điện tim"))
                                   select k).Where(p => p.PLoai == "Cận lâm sàng").ToList();
                    var cbCDHA = (from k in _kphong
                                  join cb in data.CanBoes on k.MaKP equals cb.MaKP
                                  select cb).ToList();
                    var qCLS = (from cls in data.CLS
                                join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                                select new { cls.IdCLS, cls.NgayTH, cls.MaBNhan, cls.MaKP, cls.MaKPth, bn.NoiTru, bn.DTNT }).ToList();
                    var qCLS1 = (from cd in data.ChiDinhs
                                 join dv in data.DichVus on cd.MaDV equals dv.MaDV
                                 join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                 join dtct in data.DThuoccts on cd.IDCD equals dtct.IDCD
                                 select new { cd.Status, cd.IdCLS, tn.TenRG, dv.Loai, dtct.IDCD, dv.TenDV })
                                            .Where(p => p.Status == 1 && p.IDCD != null).ToList();
                    var qClsKhamSan = (from a in _kphong
                                       join b in qCLS on a.MaKP equals b.MaKPth
                                       join c in qCLS1 on b.IdCLS equals c.IdCLS
                                       select new { b.IdCLS, b.NgayTH, b.MaBNhan, a.MaKP, a.TenKP, b.NoiTru, b.DTNT, c.Status, c.TenRG, c.Loai, c.IDCD, c.TenDV }).ToList();

                    //var q1 = (from k in _kphong
                    //          join cls in data.CLS on k.MaKP equals cls.MaKPth
                    //          join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                    //          join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                    //          select new
                    //          {
                    //              clsct.MaDVct,
                    //              cls.NgayTH,
                    //              cls.MaBNhan,
                    //              k.TenKP
                    //          }).ToList();
                    DateTime tuQuy = DateTime.MinValue;
                    DateTime denQuy = DateTime.MinValue;

                    GetNgayCuaQuy(Convert.ToInt32(cbbNam.SelectedValue), Convert.ToInt32(cbbQuyBC.SelectedItem), ref tuQuy, ref denQuy);

                    //List<QuerryField> q2 = new List<QuerryField>();
                    //var q2 = (from k in _kphong
                    //          join cls in data.CLS.Where(o => o.NgayTH >= tuQuy && o.NgayTH <= denQuy) on k.MaKP equals cls.MaKPth
                    //          join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                    //          join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                    //          join dvct in data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                    //          join dv in data.DichVus.Where(o => (o.SoTT == 1 || o.SoTT == 2) && o.TenDV.Contains("AFB")) on dvct.MaDV equals dv.MaDV
                    //          join tn in data.TieuNhomDVs.Where(o => o.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom) on dv.IdTieuNhom equals tn.IdTieuNhom
                    //          select new
                    //          {
                    //              cls.NgayTH,
                    //              cls.MaBNhan,
                    //              k.TenKP,
                    //              tn.TenRG,
                    //              dv.SoTT,
                    //              dv.TenDV
                    //}).ToList();

                    var _qcls = (from cls in data.CLS.Where(o => o.NgayTH >= tuQuy && o.NgayTH <= denQuy)
                                 join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                 join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                 select new { cls.IdCLS, cls.MaKP, cls.MaBNhan, cd.IDCD, cd.Status, cd.MaDV, cd.MaCBth, cd.NgayTH, clsct.MaDVct, clsct.KetQua }).ToList();

                    var q1 = (from kp in _kphong
                              join cls in _qcls on kp.MaKP equals cls.MaKP
                              select new
                              {
                                  cls.MaDVct,
                                  cls.NgayTH,
                                  cls.MaBNhan,
                                  kp.TenKP
                              }).ToList();

                    var q2 = (from a in q1
                              join dvct in data.DichVucts on a.MaDVct equals dvct.MaDVct
                              join dv in data.DichVus on dvct.MaDV equals dv.MaDV
                              join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                              select new
                              {
                                  a.NgayTH,
                                  tn.TenRG,
                                  dv.SoTT,
                                  dv.TenDV,
                                  a.MaBNhan,
                                  a.TenKP
                              }).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom)
                                  .Where(p => p.SoTT == 1 || p.SoTT == 2)
                                  .Where(p => p.TenDV.Contains("AFB")).ToList();

                    //var currentFetched = 0;
                    //var totalFetched = 0;

                    //do
                    //{
                    //    var cht = (from k in _kphong
                    //               join cls in data.CLS.Where(o => o.NgayTH >= tuQuy && o.NgayTH <= denQuy) on k.MaKP equals cls.MaKPth
                    //               join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                    //               join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                    //               join dvct in data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                    //               join dv in data.DichVus on dvct.MaDV equals dv.MaDV
                    //               join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                    //               where tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom && (dv.SoTT == 1 || dv.SoTT == 2) && dv.TenDV.Contains("AFB")
                    //               select new QuerryField(cls.NgayTH, dv.TenRG, dv.SoTT, dv.TenDV, cls.MaBNhan, k.TenKP)
                    //               ).Skip(totalFetched)
                    //                        .Take(1000)
                    //                        .ToList();

                    //    currentFetched = cht.Count();
                    //    totalFetched += currentFetched;
                    //    q2.AddRange(cht);
                    //}
                    //while (currentFetched > 0);

                    //var q2 = (from k in _kphong
                    //          join cls in data.CLS on k.MaKP equals cls.MaKPth
                    //          join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                    //          join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                    //          join dvct in data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                    //          join dv in data.DichVus on dvct.MaDV equals dv.MaDV
                    //          join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                    //          where tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom && (dv.SoTT == 1 || dv.SoTT == 2) && dv.TenDV.Contains("AFB") && cls.NgayTH >= tuQuy && cls.NgayTH <= denQuy
                    //          select new
                    //          {
                    //              clsct.MaDVct,
                    //              cls.NgayTH,
                    //              cls.MaBNhan,
                    //              k.TenKP
                    //          }).ToList();

                    //var q2 = (from a in q1
                    //          join dvct in data.DichVucts on a.MaDVct equals dvct.MaDVct
                    //          join dv in data.DichVus on dvct.MaDV equals dv.MaDV
                    //          join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                    //          select new
                    //          {
                    //              a.NgayTH,
                    //              tn.TenRG,
                    //              dv.SoTT,
                    //              dv.TenDV,
                    //              a.MaBNhan,
                    //              a.TenKP
                    //          }).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom)
                    //              .Where(p => p.SoTT == 1 || p.SoTT == 2)
                    //              .Where(p => p.TenDV.Contains("AFB")).ToList();

                    #endregion
                    #region 1. Tổng số cán bộ
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 1;
                    moi.DanhMuc = "Tổng số cán bộ";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 20 : 0;
                    moi.ThucHienCaQuy = cbCDHA.Count;
                    _lContent.Add(moi);
                    #endregion
                    #region 2. Xét nghiệm
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 2;
                    moi.DanhMuc = "Xét nghiệm";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 344000 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan//.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 2;
                    moi.DanhMuc = "Xét nghiệm";
                    moi.ChiTietDanhMuc = "Huyết học";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 15.7 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 2;
                    moi.DanhMuc = "Xét nghiệm";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 54000 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 2;
                    moi.DanhMuc = "Xét nghiệm";
                    moi.ChiTietDanhMuc = "Sinh hóa máu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 58.2 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 2;
                    moi.DanhMuc = "Xét nghiệm";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 200000 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 2;
                    moi.DanhMuc = "Xét nghiệm";
                    moi.ChiTietDanhMuc = "Sinh hóa nước tiểu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 15.7 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 2;
                    moi.DanhMuc = "Xét nghiệm";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 54000 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 2;
                    moi.DanhMuc = "Xét nghiệm";
                    moi.ChiTietDanhMuc = "Vi sinh";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 5.8 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 2;
                    moi.DanhMuc = "Xét nghiệm";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 20000 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 2;
                    moi.DanhMuc = "Xét nghiệm";
                    moi.ChiTietDanhMuc = "NM, MC, MD";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 4.6 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 2;
                    moi.DanhMuc = "Xét nghiệm";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 16000 : 0;
                    //moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Count();
                    //moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1 = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Count();
                    //moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang2 = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Count();
                    //moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang3 = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Count();
                    //moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    ////moi.ThucHienThang1V = qClsKhamSan////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Count();
                    //////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    //moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    //moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    //moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 3. siêu âm
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 3;
                    moi.DanhMuc = "Siêu âm";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 20000 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan//.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 4. Điện tâm đồ
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 4;
                    moi.DanhMuc = "Điện tâm đồ";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 10000 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 5. X-quang thường
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 5;
                    moi.DanhMuc = "X-quang thường";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 20000 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 6. Chụp CT - Scanner
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 6;
                    moi.DanhMuc = "Chụp CT - Scanner";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1200 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == "X-Quang CT").Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == "X-Quang CT").Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == "X-Quang CT").Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == "X-Quang CT").Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == "X-Quang CT").Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 7. Xét nghiệm AFB
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 7;
                    moi.DanhMuc = "Xét nghiệm AFB";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 7;
                    moi.DanhMuc = "Xét nghiệm AFB";
                    moi.ChiTietDanhMuc = "Số người";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 400 : 0;
                    moi.ThucHienCaQuy = q2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).GroupBy(p => p.MaBNhan).Count();
                    moi.ThucHienThang1 = q2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).GroupBy(p => p.MaBNhan).Count();
                    moi.ThucHienThang2 = q2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).GroupBy(p => p.MaBNhan).Count();
                    moi.ThucHienThang3 = q2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).GroupBy(p => p.MaBNhan).Count();
                    //moi.ThucHienThang1V = q2//.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).GroupBy(p => p.MaBNhan).Count();
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 7;
                    moi.DanhMuc = "Xét nghiệm AFB";
                    moi.ChiTietDanhMuc = "Số tiêu bản XN";
                    moi.DVTinh = "T.bản";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 800 : 0;
                    moi.ThucHienCaQuy = q2.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.ThucHienThang1 = q2.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.ThucHienThang2 = q2.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.ThucHienThang3 = q2.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    //moi.ThucHienThang1V = q2//.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 7;
                    moi.DanhMuc = "Xét nghiệm AFB";
                    moi.ChiTietDanhMuc = "Số người có AFB (+)";
                    moi.DVTinh = "T.bản";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 11 : 0;
                    _lContent.Add(moi);
                    #endregion
                }
                #endregion
                #region Khoa khám bệnh
                if (_tenkhoa.ToUpper().Contains("KHOA KHÁM BỆNH"))
                {
                    #region query
                    var _kkb = (from k in data.KPhongs.Where(p => p.PLoai.Contains("Phòng khám"))
                                select k).ToList();
                    var cbKKB = (from k in _kkb
                                 join cb in data.CanBoes on k.MaKP equals cb.MaKP
                                 select cb).ToList();
                    var qCLS_KB = (from cls in data.CLS
                                   join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                                   join kp in data.KPhongs on cls.MaKP equals kp.MaKP
                                   select new { cls.IdCLS, cls.NgayTH, cls.MaBNhan, cls.MaKP, kp.TenKP, bn.NoiTru, bn.DTNT }).ToList();
                    var qCLS_KB1 = (from cd in data.ChiDinhs
                                    join dv in data.DichVus on cd.MaDV equals dv.MaDV
                                    join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                    join dtct in data.DThuoccts on cd.IDCD equals dtct.IDCD
                                    select new { cd.Status, cd.IdCLS, tn.TenRG, dv.Loai, dtct.IDCD, dv.TenDV })
                                            .Where(p => p.Status == 1 && p.IDCD != null).ToList();
                    var qClsKhamSan_KB = (from a in _kkb
                                          join b in qCLS_KB on a.MaKP equals b.MaKP
                                          join c in qCLS_KB1 on b.IdCLS equals c.IdCLS
                                          select new { b.IdCLS, b.NgayTH, b.MaBNhan, a.MaKP, a.TenKP, b.NoiTru, b.DTNT, c.Status, c.TenRG, c.Loai, c.IDCD, c.TenDV }).ToList();

                    DateTime tuQuy = DateTime.MinValue;
                    DateTime denQuy = DateTime.MinValue;

                    GetNgayCuaQuy(Convert.ToInt32(cbbNam.SelectedValue), Convert.ToInt32(cbbQuyBC.SelectedItem), ref tuQuy, ref denQuy);

                    var _qcls = (from cls in data.CLS.Where(o => o.NgayTH >= tuQuy && o.NgayTH <= denQuy)
                                 join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                 join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                 select new { cls.IdCLS, cls.MaKP, cls.MaBNhan, cd.IDCD, cd.Status, cd.MaDV, cd.MaCBth, cd.NgayTH, clsct.MaDVct, clsct.KetQua }).ToList();

                    var q1_kb = (from kp in _kkb
                                 join cls in _qcls on kp.MaKP equals cls.MaKP
                                 select new
                                 {
                                     cls.MaDVct,
                                     cls.NgayTH,
                                     cls.MaBNhan,
                                     kp.TenKP
                                 }).ToList();

                    var q2_kb = (from a in q1_kb
                                 join dvct in data.DichVucts on a.MaDVct equals dvct.MaDVct
                                 join dv in data.DichVus on dvct.MaDV equals dv.MaDV
                                 join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                 select new
                                 {
                                     a.NgayTH,
                                     tn.TenRG,
                                     dv.SoTT,
                                     dv.TenDV,
                                     a.MaBNhan,
                                     a.TenKP
                                 }).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom)
                                  .Where(p => p.SoTT == 1 || p.SoTT == 2)
                                  .Where(p => p.TenDV.Contains("AFB")).ToList();

                    //var q1_kb = (from k in _kkb
                    //             join cls in data.CLS on k.MaKP equals cls.MaKPth
                    //             join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                    //             join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                    //             select new
                    //             {
                    //                 clsct.MaDVct,
                    //                 cls.NgayTH,
                    //                 cls.MaBNhan,
                    //                 k.TenKP
                    //             }).ToList();

                    //var q2_kb = (from a in q1_kb
                    //             join dvct in data.DichVucts on a.MaDVct equals dvct.MaDVct
                    //             join dv in data.DichVus on dvct.MaDV equals dv.MaDV
                    //             join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                    //             select new
                    //             {
                    //                 a.NgayTH,
                    //                 tn.TenRG,
                    //                 dv.SoTT,
                    //                 dv.TenDV,
                    //                 a.MaBNhan,
                    //                 a.TenKP
                    //             }).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom)
                    //              .Where(p => p.SoTT == 1 || p.SoTT == 2)
                    //              .Where(p => p.TenDV.Contains("AFB")).ToList();
                    var _luotKham = (from kp in _kkb
                                     join bnkb in data.BNKBs on kp.MaKP equals bnkb.MaKP
                                     join bn in data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                                     join dtbn in data.DTBNs on bn.IDDTBN equals dtbn.IDDTBN
                                     group new { bnkb, kp } by new { bnkb.MaBNhan, kp.TenKP, kp.ChuyenKhoa, kp.PLoai, bnkb.NgayKham, dtbn.DTBN1 } into kq
                                     select new { kq.Key.TenKP, kq.Key.NgayKham, kq.Key.ChuyenKhoa, kq.Key.PLoai, kq.Key.DTBN1 }).ToList();
                    var bnNoiTru = (from k in data.KPhongs
                                    join vv in data.VaoViens on k.MaKP equals vv.MaKP
                                    join bn in data.BenhNhans on vv.MaBNhan equals bn.MaBNhan
                                    select new
                                    {
                                        vv.NgayVao,
                                        bn.NoiTru,
                                        bn.DTNT,
                                        vv.MaKP,
                                        k.ChuyenKhoa,
                                        k.TenKP
                                    }).Where(p => p.NoiTru == 1 && p.DTNT == false).ToList();
                    var bnNgoaiTru = (from k in _kkb
                                      join vv in data.VaoViens on k.MaKP equals vv.MaKP
                                      join bn in data.BenhNhans on vv.MaBNhan equals bn.MaBNhan
                                      join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                                      select new { vv.NgayVao, bn.NoiTru, bn.DTNT, rv.NgayRa }).Where(p => p.NoiTru == 0 && p.DTNT == true).ToList();
                    #endregion
                    #region 1. Tổng số CBCNV
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 1;
                    moi.DanhMuc = "Tổng số cán bộ";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 17 : 0;
                    moi.ThucHienCaQuy = cbKKB.Count;
                    _lContent.Add(moi);
                    #endregion
                    #region 2. Giường khám bệnh
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 2;
                    moi.DanhMuc = "Giường khám bệnh";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Giường";
                    _lContent.Add(moi);
                    #endregion
                    #region 3. Tổng số BN khám tại PK
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 3;
                    moi.DanhMuc = "Tổng số BN khám tại PK";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 58000 : 0;
                    moi.ThucHienCaQuy = _luotKham.Where(p => p.NgayKham >= moi.TuNgayQuy && p.NgayKham <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang1 = _luotKham.Where(p => p.NgayKham >= moi.TuNgayThang1 && p.NgayKham <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang2 = _luotKham.Where(p => p.NgayKham >= moi.TuNgayThang2 && p.NgayKham <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang3 = _luotKham.Where(p => p.NgayKham >= moi.TuNgayThang3 && p.NgayKham <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : null;
                    //moi.ThucHienThang1V = _luotKham.Where(p => p.NgayKham >= tungayquy4 && p.NgayKham <= denngayquy4).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : null;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 3;
                    moi.DanhMuc = "Tổng số BN khám tại PK";
                    moi.ChiTietDanhMuc = "BN khám Nội và Nhi";
                    moi.DVTinh = "Người";
                    moi.ThucHienCaQuy = _luotKham.Where(p => p.ChuyenKhoa.Contains("Nội") || p.ChuyenKhoa.Contains("Nhi"))
                                                 .Where(p => p.PLoai.Contains("Phòng khám")).Where(p => p.NgayKham >= moi.TuNgayQuy && p.NgayKham <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang1 = _luotKham.Where(p => p.ChuyenKhoa.Contains("Nội") || p.ChuyenKhoa.Contains("Nhi"))
                                                 .Where(p => p.PLoai.Contains("Phòng khám")).Where(p => p.NgayKham >= moi.TuNgayThang1 && p.NgayKham <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang2 = _luotKham.Where(p => p.ChuyenKhoa.Contains("Nội") || p.ChuyenKhoa.Contains("Nhi"))
                                                 .Where(p => p.PLoai.Contains("Phòng khám")).Where(p => p.NgayKham >= moi.TuNgayThang2 && p.NgayKham <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang3 = _luotKham.Where(p => p.ChuyenKhoa.Contains("Nội") || p.ChuyenKhoa.Contains("Nhi"))
                                                 .Where(p => p.PLoai.Contains("Phòng khám")).Where(p => p.NgayKham >= moi.TuNgayThang3 && p.NgayKham <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : null;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 3;
                    moi.DanhMuc = "Tổng số BN khám tại PK";
                    moi.ChiTietDanhMuc = "BN khám sức khỏe";
                    moi.DVTinh = "Người";
                    moi.ThucHienCaQuy = _luotKham.Where(p => p.DTBN1.Contains("KSK")).Where(p => p.NgayKham >= moi.TuNgayQuy && p.NgayKham <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang1 = _luotKham.Where(p => p.DTBN1.Contains("KSK")).Where(p => p.NgayKham >= moi.TuNgayThang1 && p.NgayKham <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang2 = _luotKham.Where(p => p.DTBN1.Contains("KSK")).Where(p => p.NgayKham >= moi.TuNgayThang2 && p.NgayKham <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang3 = _luotKham.Where(p => p.DTBN1.Contains("KSK")).Where(p => p.NgayKham >= moi.TuNgayThang3 && p.NgayKham <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : null;
                    //moi.ThucHienThang1V = _luotKham.Where(p => p.DTBN1.Contains("KSK")).Where(p => p.NgayKham >= tungayquy4 && p.NgayKham <= denngayquy4).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : null;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 4. Số BN vào điều trị nội trú
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 4;
                    moi.DanhMuc = "Tổng số BN điều trị nội trú";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 15638 : 0;
                    moi.ThucHienCaQuy = bnNoiTru.Where(p => p.NgayVao >= moi.TuNgayQuy && p.NgayVao <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang1 = bnNoiTru.Where(p => p.NgayVao >= moi.TuNgayThang1 && p.NgayVao <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang2 = bnNoiTru.Where(p => p.NgayVao >= moi.TuNgayThang2 && p.NgayVao <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang3 = bnNoiTru.Where(p => p.NgayVao >= moi.TuNgayThang3 && p.NgayVao <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : null;
                    //moi.ThucHienThang1V = bnNoiTru.Where(p => p.NgayVao >= tungayquy4 && p.NgayVao <= denngayquy4).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : null;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 4;
                    moi.DanhMuc = "Tổng số BN điều trị nội trú";
                    moi.ChiTietDanhMuc = "Nội";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3346 : 0;
                    moi.ThucHienCaQuy = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Nội")).Where(p => p.NgayVao >= moi.TuNgayQuy && p.NgayVao <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang1 = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Nội")).Where(p => p.NgayVao >= moi.TuNgayThang1 && p.NgayVao <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang2 = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Nội")).Where(p => p.NgayVao >= moi.TuNgayThang2 && p.NgayVao <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang3 = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Nội")).Where(p => p.NgayVao >= moi.TuNgayThang3 && p.NgayVao <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : null;
                    //moi.ThucHienThang1V = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Nội")).Where(p => p.NgayVao >= tungayquy4 && p.NgayVao <= denngayquy4).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : null;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 4;
                    moi.DanhMuc = "Tổng số BN điều trị nội trú";
                    moi.ChiTietDanhMuc = "Nhi";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1156 : 0;
                    moi.ThucHienCaQuy = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Nhi")).Where(p => p.NgayVao >= moi.TuNgayQuy && p.NgayVao <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang1 = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Nhi")).Where(p => p.NgayVao >= moi.TuNgayThang1 && p.NgayVao <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang2 = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Nhi")).Where(p => p.NgayVao >= moi.TuNgayThang2 && p.NgayVao <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang3 = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Nhi")).Where(p => p.NgayVao >= moi.TuNgayThang3 && p.NgayVao <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : null;
                    //moi.ThucHienThang1V = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Nhi")).Where(p => p.NgayVao >= tungayquy4 && p.NgayVao <= denngayquy4).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : null;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 4;
                    moi.DanhMuc = "Tổng số BN điều trị nội trú";
                    moi.ChiTietDanhMuc = "Truyền nhiễm";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 590 : 0;
                    moi.ThucHienCaQuy = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Truyền nhiễm")).Where(p => p.NgayVao >= moi.TuNgayQuy && p.NgayVao <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang1 = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Truyền nhiễm")).Where(p => p.NgayVao >= moi.TuNgayThang1 && p.NgayVao <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang2 = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Truyền nhiễm")).Where(p => p.NgayVao >= moi.TuNgayThang2 && p.NgayVao <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang3 = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Truyền nhiễm")).Where(p => p.NgayVao >= moi.TuNgayThang3 && p.NgayVao <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : null;
                    //moi.ThucHienThang1V = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Truyền nhiễm")).Where(p => p.NgayVao >= tungayquy4 && p.NgayVao <= denngayquy4).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : null;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 4;
                    moi.DanhMuc = "Tổng số BN điều trị nội trú";
                    moi.ChiTietDanhMuc = "Hồi sức cấp cứu";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2075 : 0;
                    moi.ThucHienCaQuy = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Hồi sức cấp cứu")).Where(p => p.NgayVao >= moi.TuNgayQuy && p.NgayVao <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang1 = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Hồi sức cấp cứu")).Where(p => p.NgayVao >= moi.TuNgayThang1 && p.NgayVao <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang2 = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Hồi sức cấp cứu")).Where(p => p.NgayVao >= moi.TuNgayThang2 && p.NgayVao <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang3 = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Hồi sức cấp cứu")).Where(p => p.NgayVao >= moi.TuNgayThang3 && p.NgayVao <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : null;
                    //moi.ThucHienThang1V = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Hồi sức cấp cứu")).Where(p => p.NgayVao >= tungayquy4 && p.NgayVao <= denngayquy4).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : null;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 4;
                    moi.DanhMuc = "Tổng số BN điều trị nội trú";
                    moi.ChiTietDanhMuc = "Ngoại";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3212 : 0;
                    moi.ThucHienCaQuy = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Ngoại")).Where(p => p.NgayVao >= moi.TuNgayQuy && p.NgayVao <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang1 = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Ngoại")).Where(p => p.NgayVao >= moi.TuNgayThang1 && p.NgayVao <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang2 = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Ngoại")).Where(p => p.NgayVao >= moi.TuNgayThang2 && p.NgayVao <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang3 = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Ngoại")).Where(p => p.NgayVao >= moi.TuNgayThang3 && p.NgayVao <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : null;
                    //moi.ThucHienThang1V = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Ngoại")).Where(p => p.NgayVao >= tungayquy4 && p.NgayVao <= denngayquy4).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : null;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 4;
                    moi.DanhMuc = "Tổng số BN điều trị nội trú";
                    moi.ChiTietDanhMuc = "Liên chuyên khoa";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1500 : 0;
                    moi.ThucHienCaQuy = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt") || p.ChuyenKhoa.Contains("Tai Mũi Họng") || p.ChuyenKhoa.Contains("Mắt"))
                                                .Where(p => p.NgayVao >= moi.TuNgayQuy && p.NgayVao <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang1 = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt") || p.ChuyenKhoa.Contains("Tai Mũi Họng") || p.ChuyenKhoa.Contains("Mắt"))
                                               .Where(p => p.NgayVao >= moi.TuNgayThang1 && p.NgayVao <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang2 = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt") || p.ChuyenKhoa.Contains("Tai Mũi Họng") || p.ChuyenKhoa.Contains("Mắt"))
                                                .Where(p => p.NgayVao >= moi.TuNgayThang2 && p.NgayVao <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang3 = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt") || p.ChuyenKhoa.Contains("Tai Mũi Họng") || p.ChuyenKhoa.Contains("Mắt"))
                                                .Where(p => p.NgayVao >= moi.TuNgayThang3 && p.NgayVao <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : null;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 4;
                    moi.DanhMuc = "Tổng số BN điều trị nội trú";
                    moi.ChiTietDanhMuc = "Sản";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3139 : 0;
                    moi.ThucHienCaQuy = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Sản")).Where(p => p.NgayVao >= moi.TuNgayQuy && p.NgayVao <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang1 = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Sản")).Where(p => p.NgayVao >= moi.TuNgayThang1 && p.NgayVao <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang2 = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Sản")).Where(p => p.NgayVao >= moi.TuNgayThang2 && p.NgayVao <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang3 = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Sản")).Where(p => p.NgayVao >= moi.TuNgayThang3 && p.NgayVao <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : null;
                    //moi.ThucHienThang1V = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Sản")).Where(p => p.NgayVao >= tungayquy4 && p.NgayVao <= denngayquy4).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : null;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 4;
                    moi.DanhMuc = "Tổng số BN điều trị nội trú";
                    moi.ChiTietDanhMuc = "YHCT-PHCN";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 620 : 0;
                    moi.ThucHienCaQuy = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Đông y") || p.TenKP.Contains("Khoa YHCT"))
                                                .Where(p => p.NgayVao >= moi.TuNgayQuy && p.NgayVao <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang1 = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Đông y") || p.TenKP.Contains("Khoa YHCT"))
                                               .Where(p => p.NgayVao >= moi.TuNgayThang1 && p.NgayVao <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang2 = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Đông y") || p.TenKP.Contains("Khoa YHCT"))
                                                .Where(p => p.NgayVao >= moi.TuNgayThang2 && p.NgayVao <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang3 = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Đông y") || p.TenKP.Contains("Khoa YHCT"))
                                                .Where(p => p.NgayVao >= moi.TuNgayThang3 && p.NgayVao <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : null;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 5. Bệnh nhân chuyển viện
                    var bnChuyenVien = (from k in _kkb
                                        join rv in data.RaViens on k.MaKP equals rv.MaKP
                                        join bn in data.BenhNhans on rv.MaBNhan equals bn.MaBNhan
                                        select new { bn, rv }).Where(p => p.rv.MaBVC != null).ToList();
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 5;
                    moi.DanhMuc = "BN chuyển viện PK";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2610 : 0;
                    moi.ThucHienCaQuy = bnChuyenVien.Where(p => p.rv.NgayRa >= moi.TuNgayQuy && p.rv.NgayRa <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang1 = bnChuyenVien.Where(p => p.rv.NgayRa >= moi.TuNgayThang1 && p.rv.NgayRa <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang2 = bnChuyenVien.Where(p => p.rv.NgayRa >= moi.TuNgayThang2 && p.rv.NgayRa <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang3 = bnChuyenVien.Where(p => p.rv.NgayRa >= moi.TuNgayThang3 && p.rv.NgayRa <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : null;
                    //moi.ThucHienThang1V = bnChuyenVien.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : null;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 6. Cận lâm sàng tại phòng khám
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "Huyết học";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 34.5 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 20000 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan_KB////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "Sinh hóa máu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 259 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 150000 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan_KB////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "Sinh hóa nước tiểu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 34.5 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 20000 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan_KB////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "Siêu âm";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 20 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 11600 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan_KB//.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "Điện tâm đồ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 12 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 7000 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan_KB////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "X-quang";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 19 : 0;
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 11000 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan_KB////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "Bệnh nhân điều trị ngoại trú";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1500 : 0;
                    moi.ThucHienCaQuy = bnNgoaiTru.Where(p => p.NgayVao >= moi.TuNgayQuy && p.NgayVao <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang1 = bnNgoaiTru.Where(p => p.NgayVao >= moi.TuNgayThang1 && p.NgayVao <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang2 = bnNgoaiTru.Where(p => p.NgayVao >= moi.TuNgayThang2 && p.NgayVao <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang3 = bnNgoaiTru.Where(p => p.NgayVao >= moi.TuNgayThang3 && p.NgayVao <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : null;
                    //moi.ThucHienThang1V = bnNgoaiTru.Where(p => p.NgayVao >= tungayquy4 && p.NgayVao <= denngayquy4).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : null;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "Bệnh nhân đo loãng xương";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2900 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan_KB////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "Bệnh nhân đo chức năng hô hấp";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1740 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan_KB////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "Bệnh nhân nội soi dạ dày";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1000 : 0;
                    moi.ThucHienCaQuy = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = qClsKhamSan_KB.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienThang1V = qClsKhamSan_KB////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi).Count();
                    ////moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)//moi.ThucHienThang1V / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    ////moi.PtramQuyIV = Math.Round(//moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 7. Xét nghiệm AFB
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 7;
                    moi.DanhMuc = "Xét nghiệm AFB";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 7;
                    moi.DanhMuc = "Xét nghiệm AFB";
                    moi.ChiTietDanhMuc = "Số người";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 200 : 0;
                    moi.ThucHienCaQuy = q2_kb.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).GroupBy(p => p.MaBNhan).Count();
                    moi.ThucHienThang1 = q2_kb.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).GroupBy(p => p.MaBNhan).Count();
                    moi.ThucHienThang2 = q2_kb.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).GroupBy(p => p.MaBNhan).Count();
                    moi.ThucHienThang3 = q2_kb.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).GroupBy(p => p.MaBNhan).Count();
                    //moi.ThucHienThang1V = q2_kb//.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).GroupBy(p => p.MaBNhan).Count();
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 7;
                    moi.DanhMuc = "Xét nghiệm AFB";
                    moi.ChiTietDanhMuc = "Số tiêu bản XN";
                    moi.DVTinh = "T.bản";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 400 : 0;
                    moi.ThucHienCaQuy = q2_kb.Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.ThucHienThang1 = q2_kb.Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.ThucHienThang2 = q2_kb.Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.ThucHienThang3 = q2_kb.Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    //moi.ThucHienThang1V = q2_kb//.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    _lContent.Add(moi);
                    #endregion
                }
                #endregion
                #region Khoa dược
                if (_tenkhoa.ToUpper().Contains("KHOA DƯỢC"))
                {
                    #region query
                    var _kd = (from k in data.KPhongs.Where(p => p.PLoai.Contains("Khoa dược"))
                               select k).ToList();
                    var cbKD = (from k in _kd
                                join cb in data.CanBoes on k.MaKP equals cb.MaKP
                                select cb).ToList();
                    #endregion
                    #region 1. Tổng số CBCNV
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 1;
                    moi.DanhMuc = "Tổng số cán bộ";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 10 : 0;
                    moi.ThucHienCaQuy = cbKD.Count;
                    _lContent.Add(moi);
                    #endregion
                    #region 2. Kiểm tra dược tới các khoa
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 2;
                    moi.DanhMuc = "Kiểm tra dược tới các khoa";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 12 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 3. Thu vỏ lọ gây nghiện - HTT
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 3;
                    moi.DanhMuc = "Thu vỏ lọ gây nghiện - HTT";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 100 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 4. Thông báo thuốc
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 4;
                    moi.DanhMuc = "Thông báo thuốc";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 12 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 5. Cung ứng đầy đủ thuốc, VTTH, HC
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 5;
                    moi.DanhMuc = "Cung ứng đầy đủ thuốc, VTTH, HC";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 100 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 6. Cấp phát đảm bảo chế độ nguyên tắc
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 6;
                    moi.DanhMuc = "Cấp phát đảm bảo chế độ nguyên tắc";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 100 : 0;
                    _lContent.Add(moi);
                    #endregion
                }
                #endregion
                #region Khoa liên chuyên khoa
                if (_tenkhoa.ToUpper().Contains("KHOA LIÊN CHUYÊN KHOA"))
                {
                    #region query
                    //int ck = boChuyenKhoa.SelectedIndex;// ck == 3 ? p.ChuyenKhoa.Contains("Răng Hàm Mặt") || p.ChuyenKhoa.Contains("Tai Mũi Họng") || p.ChuyenKhoa.Contains("Mắt") : (ck == 0 ? p.ChuyenKhoa.Contains("Răng Hàm Mặt") : (ck == 1 ? p.ChuyenKhoa.Contains("Răng Hàm Mặt") : p.ChuyenKhoa.Contains("Mắt")))
                    var _klck = (from k in data.KPhongs.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt") || p.ChuyenKhoa.Contains("Tai Mũi Họng") || p.ChuyenKhoa.Contains("Mắt"))
                                 select k).ToList();
                    var cbKLCK = (from k in _klck
                                  join cb in data.CanBoes on k.MaKP equals cb.MaKP
                                  select cb).ToList();
                    var _luotKham = (from kp in _klck
                                     join bnkb in data.BNKBs on kp.MaKP equals bnkb.MaKP
                                     join bn in data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                                     join dtbn in data.DTBNs on bn.IDDTBN equals dtbn.IDDTBN
                                     group new { bnkb, kp } by new { bnkb.MaBNhan, kp.TenKP, kp.ChuyenKhoa, kp.PLoai, bnkb.NgayKham, dtbn.DTBN1 } into kq
                                     select new { kq.Key.TenKP, kq.Key.NgayKham, kq.Key.ChuyenKhoa, kq.Key.PLoai, kq.Key.DTBN1, kq.Key.MaBNhan }).ToList();

                    DateTime tuQuy = DateTime.MinValue;
                    DateTime denQuy = DateTime.MinValue;

                    GetNgayCuaQuy(Convert.ToInt32(cbbNam.SelectedValue), Convert.ToInt32(cbbQuyBC.SelectedItem), ref tuQuy, ref denQuy);

                    var qCLS_KB = (from kp in _klck
                                   join cls in data.CLS.Where(o => o.NgayTH >= tuQuy && o.NgayTH <= denQuy) on kp.MaKP equals cls.MaKP
                                   select new { cls.IdCLS, cls.NgayTH, cls.MaBNhan, cls.MaKP, kp.TenKP }).ToList();
                    var qCLS_KB1 = (from cd in data.ChiDinhs.Where(p => p.Status == 1)
                                    join dv in data.DichVus on cd.MaDV equals dv.MaDV
                                    join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                    join dtct in data.DThuoccts.Where(o => o.IDCD != null) on cd.IDCD equals dtct.IDCD
                                    select new { cd.Status, cd.IdCLS, tn.TenRG, dv.Loai, dtct.IDCD, dv.TenDV }).ToList();
                    var qClsKhamSan_PK = (from a in _luotKham
                                          join b in qCLS_KB on a.MaBNhan equals b.MaBNhan
                                          join c in qCLS_KB1 on b.IdCLS equals c.IdCLS
                                          select new { b.IdCLS, b.NgayTH, b.MaBNhan, b.MaKP, b.TenKP, c.Status, c.TenRG, c.Loai, c.IDCD, c.TenDV, a.ChuyenKhoa }).ToList();

                    var bnNoiTru = (from k in _klck
                                    join rv in data.RaViens on k.MaKP equals rv.MaKP
                                    join bn in data.BenhNhans on rv.MaBNhan equals bn.MaBNhan
                                    select new
                                    {
                                        bn.NoiTru,
                                        bn.DTNT,
                                        rv.MaKP,
                                        rv.NgayRa,
                                        rv.KetQua,
                                        rv.MaBVC,
                                        k.ChuyenKhoa,
                                        k.TenKP,
                                        bn.MaBNhan
                                    }).Where(p => p.NoiTru == 1 && p.DTNT == false).ToList();
                    var bnRV = (from kp in _klck
                                join rv in data.RaViens on kp.MaKP equals rv.MaKP
                                join bn in data.BenhNhans on rv.MaBNhan equals bn.MaBNhan
                                select new { bn, rv }).ToList();
                    #endregion
                    #region 1. Tổng số CBVC
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 1;
                    moi.DanhMuc = "Tổng số CBVC";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 15;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 18;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 15;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 15;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    _lContent.Add(moi);
                    #endregion
                    #region 2. Giường bệnh
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 2;
                    moi.DanhMuc = "Giường khám bệnh";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Giường";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 24;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 30;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 24;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 24;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    _lContent.Add(moi);

                    //moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    //moi.Stt = 2;
                    //moi.DanhMuc = "Giường khám bệnh";
                    //moi.ChiTietDanhMuc = "Tai mũi họng";
                    //moi.DVTinh = "Giường";
                    //moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 5 : 0;
                    //_lContent.Add(moi);

                    //moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    //moi.Stt = 2;
                    //moi.DanhMuc = "Giường khám bệnh";
                    //moi.ChiTietDanhMuc = "Mắt";
                    //moi.DVTinh = "Giường";
                    //moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 5 : 0;
                    //_lContent.Add(moi);

                    //moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    //moi.Stt = 2;
                    //moi.DanhMuc = "Giường khám bệnh";
                    //moi.ChiTietDanhMuc = "Răng hàm mặt";
                    //moi.DVTinh = "Giường";
                    //moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 5 : 0;
                    //_lContent.Add(moi);
                    #endregion
                    #region 3. Tổng số khám bệnh
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 3;
                    moi.DanhMuc = "Tổng số khám bệnh";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 4500;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 4500;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 4500;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 4500;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    _lContent.Add(moi);
                    //if (ck == 3 || ck == 1)
                    //{
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 3;
                    moi.DanhMuc = "Tổng số khám bệnh";
                    moi.ChiTietDanhMuc = "-Tai mũi họng";
                    moi.DVTinh = "Người";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 2250;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 2250;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 2250;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 2250;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = _luotKham.Where(p => p.ChuyenKhoa.Contains("Tai Mũi Họng")).Where(p => p.NgayKham >= moi.TuNgayQuy && p.NgayKham <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang1 = _luotKham.Where(p => p.ChuyenKhoa.Contains("Tai Mũi Họng")).Where(p => p.NgayKham >= moi.TuNgayThang1 && p.NgayKham <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang2 = _luotKham.Where(p => p.ChuyenKhoa.Contains("Tai Mũi Họng")).Where(p => p.NgayKham >= moi.TuNgayThang2 && p.NgayKham <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang3 = _luotKham.Where(p => p.ChuyenKhoa.Contains("Tai Mũi Họng")).Where(p => p.NgayKham >= moi.TuNgayThang3 && p.NgayKham <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : null;
                    //moi.ThucHienQuyIV = _luotKham.Where(p => p.ChuyenKhoa.Contains("Tai Mũi Họng")).Where(p => p.NgayKham >= tungayquy4 && p.NgayKham <= denngayquy4).Count();
                    //moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaQuy * 100 : null;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    //}
                    //if (ck == 3 || ck == 2)
                    //{
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 3;
                    moi.DanhMuc = "Tổng số khám bệnh";
                    moi.ChiTietDanhMuc = "-Răng hàm mặt";
                    moi.DVTinh = "Người";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 750;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 750;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 750;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 750;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = _luotKham.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.NgayKham >= moi.TuNgayQuy && p.NgayKham <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang1 = _luotKham.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.NgayKham >= moi.TuNgayThang1 && p.NgayKham <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang2 = _luotKham.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.NgayKham >= moi.TuNgayThang2 && p.NgayKham <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang3 = _luotKham.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.NgayKham >= moi.TuNgayThang3 && p.NgayKham <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : null;
                    //moi.ThucHienQuyIV = _luotKham.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.NgayKham >= tungayquy4 && p.NgayKham <= denngayquy4).Count();
                    //moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaQuy * 100 : null;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 3;
                    moi.DanhMuc = "Tổng số khám bệnh";
                    moi.ChiTietDanhMuc = "-Mắt";
                    moi.DVTinh = "";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 1500;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 1500;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 1500;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 1500;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = _luotKham.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.NgayKham >= moi.TuNgayQuy && p.NgayKham <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang1 = _luotKham.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.NgayKham >= moi.TuNgayThang1 && p.NgayKham <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang2 = _luotKham.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.NgayKham >= moi.TuNgayThang2 && p.NgayKham <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang3 = _luotKham.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.NgayKham >= moi.TuNgayThang3 && p.NgayKham <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : null;
                    //moi.ThucHienQuyIV = _luotKham.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.NgayKham >= tungayquy4 && p.NgayKham <= denngayquy4).Count();
                    //moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaQuy * 100 : null;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    //}
                    //if (ck == 3 || ck == 0)
                    //{
                    //}

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 3;
                    moi.DanhMuc = "Tổng số khám bệnh";
                    moi.ChiTietDanhMuc = "Bệnh nhân điều trị ngoại trú RHM";
                    moi.DVTinh = "Ca";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 150;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 150;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 150;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 150;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = qClsKhamSan_PK.Where(p => p.TenRG == "Thủ thuật").Where(p => p.TenDV.Contains("răng"))
                                                      .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang1 = qClsKhamSan_PK.Where(p => p.TenRG == "Thủ thuật").Where(p => p.TenDV.Contains("răng"))
                                                     .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang2 = qClsKhamSan_PK.Where(p => p.TenRG == "Thủ thuật").Where(p => p.TenDV.Contains("răng"))
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang3 = qClsKhamSan_PK.Where(p => p.TenRG == "Thủ thuật").Where(p => p.TenDV.Contains("răng"))
                                                       .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : null;
                    //moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenRG == "Thủ thuật").Where(p => p.TenDV.Contains("răng"))
                                                      ////.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    //moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaQuy * 100 : null;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 4. Cận lâm sàng tại phòng khám
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 4;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 4;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "- Huyết học";
                    moi.DVTinh = "Lần";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 765;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 765;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 765;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 765;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                                                      .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang1 = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang2 = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang3 = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : null;
                    //moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                                                      //.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    //moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaQuy * 100 : null;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 4;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "      Tỷ lệ";
                    moi.DVTinh = "%";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 0.0425;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 0.0425;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 0.0425;
                                break;
                            case "4":
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 4;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "- Sinh hóa nước tiểu";
                    moi.DVTinh = "Lần";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 360;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 360;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 360;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 360;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)
                                                      .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang1 = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang2 = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang3 = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : null;
                    //moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)
                                                      //.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    //moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaQuy * 100 : null;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 4;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "      Tỷ lệ";
                    moi.DVTinh = "%";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 0;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 0.02;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 0.02;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 0;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 4;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "- Sinh hóa máu";
                    moi.DVTinh = "Lần";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 2250;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 2250;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 2250;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 2250;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                                                      .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang1 = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang2 = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang3 = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : null;
                    //moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                                                      //.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    //moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaQuy * 100 : null;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 4;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "      Tỷ lệ";
                    moi.DVTinh = "%";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = null;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = null;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = null;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = null;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 4;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "- X-quang";
                    moi.DVTinh = "Lần";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 540;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 540;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 540;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 540;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)
                                                      .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang1 = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang2 = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang3 = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : null;
                    //moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)
                                                      //.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    //moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaQuy * 100 : null;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 4;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "      Tỷ lệ";
                    moi.DVTinh = "%";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 0.03;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 0.03;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 0.03;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = null;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 4;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "- Siêu âm";
                    moi.DVTinh = "Lần";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 270;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 270;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 270;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 270;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = qClsKhamSan_PK.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm))
                                                      .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang1 = qClsKhamSan_PK.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm))
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang2 = qClsKhamSan_PK.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm))
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang3 = qClsKhamSan_PK.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm))
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : null;
                    //moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm))
                                                      //.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    //moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaQuy * 100 : null;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 4;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "      Tỷ lệ";
                    moi.DVTinh = "%";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 0.015;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 0.015;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 0.015;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = null;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 4;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "      Nội soi T-M-H";
                    moi.DVTinh = "Lần";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 1717.5;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 1717.5;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 1717.5;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 1717.5;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong)
                                                      .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang1 = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang2 = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang3 = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : null;
                    //moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong)
                                                      //.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    //moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaQuy * 100 : null;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 4;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "- Tỷ lệ";
                    moi.DVTinh = "%";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 0.095;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 0.095;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 0.095;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = null;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    _lContent.Add(moi);
                    #endregion
                    #region 5. Ngày điều trị trung bình
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 5;
                    moi.DanhMuc = "Ngày điều trị trung bình";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Ngày";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 5;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 5;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 5;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 5;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = (bnRV.Where(p => p.rv.NgayRa >= moi.TuNgayQuy && p.rv.NgayRa <= moi.DenNgayQuy).Count() != 0) ? (int)bnRV.Where(p => p.rv.NgayRa >= moi.TuNgayQuy && p.rv.NgayRa <= moi.DenNgayQuy).Sum(p => p.rv.SoNgaydt) / bnRV.Where(p => p.rv.NgayRa >= moi.TuNgayQuy && p.rv.NgayRa <= moi.DenNgayQuy).Count() : 0;
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = (bnRV.Where(p => p.rv.NgayRa >= moi.TuNgayThang1 && p.rv.NgayRa <= moi.DenNgayThang1).Count() != 0) ? (int)bnRV.Where(p => p.rv.NgayRa >= moi.TuNgayThang1 && p.rv.NgayRa <= moi.DenNgayThang1).Sum(p => p.rv.SoNgaydt) / bnRV.Where(p => p.rv.NgayRa >= moi.TuNgayThang1 && p.rv.NgayRa <= moi.DenNgayThang1).Count() : 0;
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = (bnRV.Where(p => p.rv.NgayRa >= moi.TuNgayThang2 && p.rv.NgayRa <= moi.DenNgayThang2).Count() != 0) ? (int)bnRV.Where(p => p.rv.NgayRa >= moi.TuNgayThang2 && p.rv.NgayRa <= moi.DenNgayThang2).Sum(p => p.rv.SoNgaydt) / bnRV.Where(p => p.rv.NgayRa >= moi.TuNgayThang2 && p.rv.NgayRa <= moi.DenNgayThang2).Count() : 0;
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = (bnRV.Where(p => p.rv.NgayRa >= moi.TuNgayThang3 && p.rv.NgayRa <= moi.DenNgayThang3).Count() != 0) ? (int)bnRV.Where(p => p.rv.NgayRa >= moi.TuNgayThang3 && p.rv.NgayRa <= moi.DenNgayThang3).Sum(p => p.rv.SoNgaydt) / bnRV.Where(p => p.rv.NgayRa >= moi.TuNgayThang3 && p.rv.NgayRa <= moi.DenNgayThang3).Count() : 0;
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienQuyIV = (bnRV.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Count() != 0) ? (int)bnRV.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Sum(p => p.rv.SoNgaydt) / bnRV.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Count() : 0;
                    //moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 6.Tổng số ngày điều trị
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 6;
                    moi.DanhMuc = "Tổng số ngày điều trị";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Ngày";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 1900;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 1900;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 1900;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 1900;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = bnRV.Where(p => p.rv.NgayRa >= moi.TuNgayQuy && p.rv.NgayRa <= moi.DenNgayQuy).Sum(p => p.rv.SoNgaydt);
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = bnRV.Where(p => p.rv.NgayRa >= moi.TuNgayThang1 && p.rv.NgayRa <= moi.DenNgayThang1).Sum(p => p.rv.SoNgaydt);
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = bnRV.Where(p => p.rv.NgayRa >= moi.TuNgayThang2 && p.rv.NgayRa <= moi.DenNgayThang2).Sum(p => p.rv.SoNgaydt);
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = bnRV.Where(p => p.rv.NgayRa >= moi.TuNgayThang3 && p.rv.NgayRa <= moi.DenNgayThang3).Sum(p => p.rv.SoNgaydt);
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienQuyIV = bnRV.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Sum(p => p.rv.SoNgaydt);
                    //moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 7. Ngày sử dụng giường/tháng
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 7;
                    moi.DanhMuc = "Ngày sử dụng giường/tháng";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Ngày";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 30;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 30;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 30;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 30;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    _lContent.Add(moi);
                    #endregion
                    #region 8. Công suất sử dụng giường bệnh
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 8;
                    moi.DanhMuc = "Công suất sử dụng giường bệnh";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "%";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 100;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 100;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 100;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 100;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    _lContent.Add(moi);
                    #endregion
                    #region 9. Tai biến trong điều trị
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 9;
                    moi.DanhMuc = "Tai biến trong điều trị";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Ca";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 0;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 0;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 0;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 0;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    _lContent.Add(moi);
                    #endregion
                    #region 10. Bệnh nhân điều trị nội trú
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 380;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 380;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 380;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 380;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = bnNoiTru.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = bnNoiTru.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = bnNoiTru.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = bnNoiTru.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienQuyIV = bnNoiTru.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Count();
                    //moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    //if (ck == 3 || ck == 1)
                    //{
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tai mũi họng";
                    moi.DVTinh = "Người";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 175;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 175;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 175;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 175;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Tai Mũi Họng")).Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Tai Mũi Họng")).Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Tai Mũi Họng")).Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Tai Mũi Họng")).Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                   // moi.ThucHienQuyIV = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Tai Mũi Họng")).Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Count();
                    //moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    //}
                    //if (ck == 3 || ck == 0)
                    //{
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Răng hàm mặt";
                    moi.DVTinh = "Người";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 82.5;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 82.5;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 82.5;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 82.5;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienQuyIV = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Count();
                    //moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    //}
                    //if (ck == 3 || ck == 2)
                    //{
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Mắt";
                    moi.DVTinh = "Người";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 122.5;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 122.5;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 122.5;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 122.5;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienQuyIV = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Count();
                    //moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    //}
                    #endregion
                    #region 11. Tỷ lệ điều trị khỏi
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ điều trị khỏi";
                    moi.DVTinh = "%";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 0.2;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 0.2;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 0.2;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 0.2;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tổng BN điều trị khỏi";
                    moi.DVTinh = "Người";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 304;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 304;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 304;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 304;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = bnNoiTru.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = bnNoiTru.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = bnNoiTru.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = bnNoiTru.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienQuyIV = bnNoiTru.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Khỏi").Count();
                    //moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 12. Tỷ lệ đỡ
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ đỡ";
                    moi.DVTinh = "%";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 0.0425;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 0.0425;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 0.0425;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = null;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tổng BN điều trị đỡ";
                    moi.DVTinh = "Người";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 64.5;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 64.5;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 64.5;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = null;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = bnNoiTru.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = bnNoiTru.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = bnNoiTru.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = bnNoiTru.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienQuyIV = bnNoiTru.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    //moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 13. Tỷ lệ BN chuyển viện
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ BN chuyển viện";
                    moi.DVTinh = "%";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 0.0025;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 0.0025;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = null;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = null;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tổng BN chuyển viện";
                    moi.DVTinh = "Người";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 3.5;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 3.5;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 3.5;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = null;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = bnNoiTru.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.MaBVC != null).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = bnNoiTru.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.MaBVC != null).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = bnNoiTru.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.MaBVC != null).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = bnNoiTru.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.MaBVC != null).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienQuyIV = bnNoiTru.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.MaBVC != null).Count();
                    //moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 14. Tỷ lệ xin về
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ xin về";
                    moi.DVTinh = "%";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 0.005;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 0.005;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 0.005;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = null;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tổng số BN nặng xin về";
                    moi.DVTinh = "Người";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 0;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 0;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 0;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 0;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = bnNoiTru.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = bnNoiTru.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = bnNoiTru.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = bnNoiTru.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienQuyIV = bnNoiTru.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Nặng hơn").Count();
                    //moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 15. Tỷ lệ tử vong
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ tử vong";
                    moi.DVTinh = "%";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 0;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 0;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 0;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 0;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 10;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tổng số BN tử vong";
                    moi.DVTinh = "Người";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 0;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 0;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 0;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 0;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = bnNoiTru.Where(p => p.NgayRa >= moi.TuNgayQuy && p.NgayRa <= moi.DenNgayQuy).Where(p => p.KetQua == "Tử vong").Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang1 = bnNoiTru.Where(p => p.NgayRa >= moi.TuNgayThang1 && p.NgayRa <= moi.DenNgayThang1).Where(p => p.KetQua == "Tử vong").Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang2 = bnNoiTru.Where(p => p.NgayRa >= moi.TuNgayThang2 && p.NgayRa <= moi.DenNgayThang2).Where(p => p.KetQua == "Tử vong").Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : 0;
                    moi.ThucHienThang3 = bnNoiTru.Where(p => p.NgayRa >= moi.TuNgayThang3 && p.NgayRa <= moi.DenNgayThang3).Where(p => p.KetQua == "Tử vong").Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : 0;
                    //moi.ThucHienQuyIV = bnNoiTru.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Tử vong").Count();
                    //moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaQuy * 100 : 0;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 16. Cận lâm sàng tại bệnh phòng
                    var qCls_Noitru = (from a in bnNoiTru
                                       join b in qCLS_KB on a.MaBNhan equals b.MaBNhan
                                       join c in qCLS_KB1 on b.IdCLS equals c.IdCLS
                                       select new { b.IdCLS, b.NgayTH, b.MaBNhan, b.MaKP, b.TenKP, c.Status, c.TenRG, c.Loai, c.IDCD, c.TenDV }).ToList();
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    _lContent.Add(moi);

                    //moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    //moi.Stt = 11;
                    //moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                    //moi.ChiTietDanhMuc = "Truyền máu";
                    //moi.DVTinh = "ui";
                    //_lContent.Add(moi);
                    #region Huyết học
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                    moi.ChiTietDanhMuc = "- Tỷ lệ huyết học";
                    moi.DVTinh = "%";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 100;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 100;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 100;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 100;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                    moi.ChiTietDanhMuc = "Tổng số";
                    moi.DVTinh = "Lần";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 380;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 380;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 380;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 380;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                                                      .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang1 = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang2 = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang3 = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : null;
                    //moi.ThucHienQuyIV = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                                                      //.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    //moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaQuy * 100 : null;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion

                    #region Sinh hóa máu
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                    moi.ChiTietDanhMuc = "- Sinh hóa máu";
                    moi.DVTinh = "%";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = null;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = null;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = null;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = null;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                    moi.ChiTietDanhMuc = "Tổng số";
                    moi.DVTinh = "Lần";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 1900;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 1900;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 1900;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 1900;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                                                      .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang1 = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang2 = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang3 = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : null;
                    //moi.ThucHienQuyIV = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                                                      //.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    //moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaQuy * 100 : null;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    #endregion

                    #region Sinh hóa nước tiểu
                    //moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                    moi.ChiTietDanhMuc = "- Sinh hóa nước tiểu";
                    moi.DVTinh = "%";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = null;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = null;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = null;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = null;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                    moi.ChiTietDanhMuc = "Tổng số";
                    moi.DVTinh = "Lần";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 380;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 380;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 380;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 380;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)
                                                      .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang1 = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang2 = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang3 = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : null;
                    //moi.ThucHienQuyIV = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)
                                                      //.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    //moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaQuy * 100 : null;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    #endregion

                    #region Điện tim

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                    moi.ChiTietDanhMuc = "- Điện tim";
                    moi.DVTinh = "Lần";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 133;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 133;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 133;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 133;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)
                                                      .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang1 = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang2 = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang3 = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : null;
                    //moi.ThucHienQuyIV = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)
                                                      //.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    //moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaQuy * 100 : null;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                    moi.ChiTietDanhMuc = "Tỷ lệ";
                    moi.DVTinh = "%";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 0;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 0;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 0;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 0;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    _lContent.Add(moi);

                    #endregion

                    #region Siêu âm

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                    moi.ChiTietDanhMuc = "- Siêu âm";
                    moi.DVTinh = "Lần";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 76;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 76;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 76;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 76;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = qCls_Noitru.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm))
                                                      .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang1 = qCls_Noitru.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm))
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang2 = qCls_Noitru.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm))
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang3 = qCls_Noitru.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm))
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : null;
                    //moi.ThucHienQuyIV = qCls_Noitru.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm))
                                                      //.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    //moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaQuy * 100 : null;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                    moi.ChiTietDanhMuc = "Tỷ lệ";
                    moi.DVTinh = "%";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 0;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 0;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 0;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 0;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    _lContent.Add(moi);

                    #endregion

                    #region X-quang
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                    moi.ChiTietDanhMuc = "- X-quang";
                    moi.DVTinh = "Lần";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 76;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 76;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 76;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 76;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)
                                                      .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang1 = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang2 = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang3 = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : null;
                    //moi.ThucHienQuyIV = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)
                                                      //.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    //moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaQuy * 100 : null;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                    moi.ChiTietDanhMuc = "Tỷ lệ";
                    moi.DVTinh = "%";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 0;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 0;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 0;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 0;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    _lContent.Add(moi);
                    #endregion

                    #region Nội soi TMH
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                    moi.ChiTietDanhMuc = "- Nội soi T- M- H";
                    moi.DVTinh = "Lần";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 114;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 114;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 114;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 114;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong)
                                                      .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.PtramCaQuy = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienCaQuy / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang1 = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.PtramThang1 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang1 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang2 = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.PtramThang2 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang2 / moi.ChiTieuCaQuy * 100 : null;
                    moi.ThucHienThang3 = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong)
                                                      .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    moi.PtramThang3 = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienThang3 / moi.ChiTieuCaQuy * 100 : null;
                    //moi.ThucHienQuyIV = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong)
                                                      //.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    //moi.PtramQuyIV = (moi.ChiTieuCaQuy != 0 || moi.ChiTieuCaQuy != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaQuy * 100 : null;
                    moi.PtramCaQuy = Math.Round(moi.PtramCaQuy.GetValueOrDefault(), 2);
                    moi.PtramThang1 = Math.Round(moi.PtramThang1.GetValueOrDefault(), 2);
                    moi.PtramThang2 = Math.Round(moi.PtramThang2.GetValueOrDefault(), 2);
                    moi.PtramThang3 = Math.Round(moi.PtramThang3.GetValueOrDefault(), 2);
                    //moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                    moi.ChiTietDanhMuc = "Tỷ lệ";
                    moi.DVTinh = "%";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 0;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 0;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 0;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 0;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    _lContent.Add(moi);
                    #endregion

                    #endregion
                    #region 17. Tổng Phẫu thuật

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Tổng phẫu thuật";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Ca";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 138;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 138;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 138;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 138;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.ThucHienThang1 = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.ThucHienThang2 = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.ThucHienThang3 = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    //moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        //.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Tổng phẫu thuật";
                    moi.ChiTietDanhMuc = "      + Loại I";
                    moi.DVTinh = "Ca";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 100;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 100;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 100;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 100;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 1).Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.ThucHienThang1 = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 1).Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.ThucHienThang2 = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 1).Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.ThucHienThang3 = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 1).Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    //moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        //.Where(p => p.Loai == 1)//.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Tổng phẫu thuật";
                    moi.ChiTietDanhMuc = "      + Loại II";
                    moi.DVTinh = "Ca";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 25;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 25;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 25;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 25;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 2).Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.ThucHienThang1 = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 2).Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.ThucHienThang2 = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 2).Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.ThucHienThang3 = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 2).Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    //moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        //.Where(p => p.Loai == 2)//.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Tổng phẫu thuật";
                    moi.ChiTietDanhMuc = "      + Loại III";
                    moi.DVTinh = "Ca";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 13;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 13;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 13;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 13;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 3).Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.ThucHienThang1 = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 3).Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.ThucHienThang2 = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 3).Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.ThucHienThang3 = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 3).Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    //moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        //.Where(p => p.Loai == 3)//.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Tổng phẫu thuật";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Ca";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 150;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 150;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 150;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 150;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Tổng phẫu thuật";
                    moi.ChiTietDanhMuc = "      Mổ Phaco";
                    moi.DVTinh = "Ca";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 75;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 75;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 75;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 75;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Tổng phẫu thuật";
                    moi.ChiTietDanhMuc = "      Mổ khác";
                    moi.DVTinh = "Ca";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 75;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 75;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 75;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 75;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Tổng phẫu thuật";
                    moi.ChiTietDanhMuc = "- Phẫu thuật răng hàm mặt";
                    moi.DVTinh = "Ca";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 13;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 13;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 13;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 13;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.ThucHienThang1 = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.ThucHienThang2 = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.ThucHienThang3 = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    //moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        //.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Tổng phẫu thuật";
                    moi.ChiTietDanhMuc = "- PT Cắt Amidal";
                    moi.DVTinh = "Ca";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 10;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 10;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 10;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 10;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = qClsKhamSan_PK.Where(p => p.TenDV.ToLower().Contains("cắt a"))
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.ThucHienThang1 = qClsKhamSan_PK.Where(p => p.TenDV.ToLower().Contains("cắt a"))
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.ThucHienThang2 = qClsKhamSan_PK.Where(p => p.TenDV.ToLower().Contains("cắt a"))
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.ThucHienThang3 = qClsKhamSan_PK.Where(p => p.TenDV.ToLower().Contains("cắt a"))
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    //moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenDV.ToLower().Contains("cắt a"))
                                        //.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Tổng phẫu thuật";
                    moi.ChiTietDanhMuc = "- Thủ thuật TMH";
                    moi.DVTinh = "Ca";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = null;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = null;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 3;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 3;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 12;
                    moi.DanhMuc = "Tổng phẫu thuật";
                    moi.ChiTietDanhMuc = "- Làm răng giả";
                    moi.DVTinh = "Ca";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 3;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 3;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 3;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 3;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    _lContent.Add(moi);

                    #endregion
                    #region 18. Tổng ca thủ thuật chung
                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Tổng ca thủ thuật chung";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Ca";
                    if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                    {
                        switch (cbbQuyBC.SelectedItem.ToString())
                        {
                            case "1":
                                moi.ChiTieuCaQuy = 625;
                                break;
                            case "2":
                                moi.ChiTieuCaQuy = 625;
                                break;
                            case "3":
                                moi.ChiTieuCaQuy = 625;
                                break;
                            case "4":
                                moi.ChiTieuCaQuy = 625;
                                break;
                        }
                    }
                    else
                    {
                        moi.ChiTieuCaQuy = 0;
                    }
                    moi.ThucHienCaQuy = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Thủ thuật"))
                                        .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    moi.ThucHienThang1 = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Thủ thuật"))
                                        .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    moi.ThucHienThang2 = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Thủ thuật"))
                                        .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    moi.ThucHienThang3 = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Thủ thuật"))
                                        .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    //moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Thủ thuật"))
                                        //.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    _lContent.Add(moi);

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Tổng ca thủ thuật chung";
                    moi.ChiTietDanhMuc = "Tỷ lệ";
                    moi.DVTinh = "%";

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Tổng ca thủ thuật chung";
                    moi.ChiTietDanhMuc = "      + Loại I";
                    moi.DVTinh = "Ca";

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Tổng ca thủ thuật chung";
                    moi.ChiTietDanhMuc = "      + Loại II";
                    moi.DVTinh = "Ca";

                    moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    moi.Stt = 13;
                    moi.DanhMuc = "Tổng ca thủ thuật chung";
                    moi.ChiTietDanhMuc = "      + Loại III";
                    moi.DVTinh = "Ca";

                    //moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    //moi.Stt = 13;
                    //moi.DanhMuc = "Phẫu thuật Răng hàm mặt";
                    //moi.ChiTietDanhMuc = "Tổng tiểu phẫu";
                    //moi.DVTinh = "Ca";
                    //moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 25 : 0;
                    //moi.ThucHienCaQuy = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.TenRG.Contains("Thủ thuật"))
                    //                    .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    //moi.ThucHienThang1 = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.TenRG.Contains("Thủ thuật"))
                    //                    .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    //moi.ThucHienThang2 = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.TenRG.Contains("Thủ thuật"))
                    //                    .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    //moi.ThucHienThang3 = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.TenRG.Contains("Thủ thuật"))
                    //                    .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    ////moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.TenRG.Contains("Thủ thuật"))
                    //                    //.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    //_lContent.Add(moi);

                    //moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    //moi.Stt = 13;
                    //moi.DanhMuc = "Phẫu thuật Răng hàm mặt";
                    //moi.ChiTietDanhMuc = "Cắt Amidal";
                    //moi.DVTinh = "Ca";
                    //moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 25 : 0;
                    //moi.ThucHienCaQuy = qClsKhamSan_PK.Where(p => p.TenDV.ToLower().Contains("cắt a"))
                    //                    .Where(p => p.NgayTH >= moi.TuNgayQuy && p.NgayTH <= moi.DenNgayQuy).Count();
                    //moi.ThucHienThang1 = qClsKhamSan_PK.Where(p => p.TenDV.ToLower().Contains("cắt a"))
                    //                    .Where(p => p.NgayTH >= moi.TuNgayThang1 && p.NgayTH <= moi.DenNgayThang1).Count();
                    //moi.ThucHienThang2 = qClsKhamSan_PK.Where(p => p.TenDV.ToLower().Contains("cắt a"))
                    //                    .Where(p => p.NgayTH >= moi.TuNgayThang2 && p.NgayTH <= moi.DenNgayThang2).Count();
                    //moi.ThucHienThang3 = qClsKhamSan_PK.Where(p => p.TenDV.ToLower().Contains("cắt a"))
                    //                    .Where(p => p.NgayTH >= moi.TuNgayThang3 && p.NgayTH <= moi.DenNgayThang3).Count();
                    ////moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenDV.ToLower().Contains("cắt a"))
                    //                    //.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    //_lContent.Add(moi);

                    //moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    //moi.Stt = 13;
                    //moi.DanhMuc = "Phẫu thuật Răng hàm mặt";
                    //moi.ChiTietDanhMuc = "Thủ thuật TMH khác";
                    //moi.DVTinh = "Ca";
                    //moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 25 : 0;
                    //_lContent.Add(moi);

                    //moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                    //moi.Stt = 13;
                    //moi.DanhMuc = "Phẫu thuật Răng hàm mặt";
                    //moi.ChiTietDanhMuc = "Làm răng giả";
                    //moi.DVTinh = "Ca";
                    //moi.ChiTieuCaQuy = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 25 : 0;
                    //_lContent.Add(moi);
                    #endregion
                }
                #endregion
                #region 19. Kiểm tra khoa phòng
                moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                moi.Stt = 15;
                moi.DanhMuc = "Kiểm tra khoa phòng";
                moi.DVTinh = "Lần";
                moi.ChiTietDanhMuc = "";
                if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                {
                    switch (cbbQuyBC.SelectedItem.ToString())
                    {
                        case "1":
                            moi.ChiTieuCaQuy = 3;
                            break;
                        case "2":
                            moi.ChiTieuCaQuy = 3;
                            break;
                        case "3":
                            moi.ChiTieuCaQuy = 3;
                            break;
                        case "4":
                            moi.ChiTieuCaQuy = 3;
                            break;
                    }
                }
                else
                {
                    moi.ChiTieuCaQuy = 0;
                }
                _lContent.Add(moi);
                #endregion
                #region 20. sinh hoạt bệnh nhân
                moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                moi.Stt = 16;
                moi.DanhMuc = "Sinh hoạt bệnh nhân";
                moi.ChiTietDanhMuc = "";
                moi.DVTinh = "Lần";
                if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                {
                    switch (cbbQuyBC.SelectedItem.ToString())
                    {
                        case "1":
                            moi.ChiTieuCaQuy = 3;
                            break;
                        case "2":
                            moi.ChiTieuCaQuy = 3;
                            break;
                        case "3":
                            moi.ChiTieuCaQuy = 3;
                            break;
                        case "4":
                            moi.ChiTieuCaQuy = 3;
                            break;
                    }
                }
                else
                {
                    moi.ChiTieuCaQuy = 0;
                }
                _lContent.Add(moi);
                #endregion
                #region 21. Chuyển giao kỹ thuật cho tuyến xã
                moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                moi.Stt = 17;
                moi.DanhMuc = "Chuyển giao kỹ thuật cho tuyến xã";
                moi.DVTinh = "KT";
                moi.ChiTietDanhMuc = "";
                _lContent.Add(moi);
                #endregion
                #region 22. Kỹ thuật mới
                moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                moi.Stt = 18;
                moi.DanhMuc = "Kỹ thuật mới";
                moi.ChiTietDanhMuc = "";
                moi.DVTinh = "KT";
                _lContent.Add(moi);
                #endregion
                #region 23. Cá nhân đạt lao động tiên tiến
                moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                moi.Stt = 19;
                moi.DanhMuc = "Cá nhân đạt lao động tiên tiến";
                moi.ChiTietDanhMuc = "";
                moi.DVTinh = "%";
                if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                {
                    switch (cbbQuyBC.SelectedItem.ToString())
                    {
                        case "1":
                            moi.ChiTieuCaQuy = 100;
                            break;
                        case "2":
                            moi.ChiTieuCaQuy = 100;
                            break;
                        case "3":
                            moi.ChiTieuCaQuy = 100;
                            break;
                        case "4":
                            moi.ChiTieuCaQuy = 100;
                            break;
                    }
                }
                else
                {
                    moi.ChiTieuCaQuy = 0;
                }
                _lContent.Add(moi);
                #endregion
                #region 24. Thi đua cấp cơ sở
                moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                moi.Stt = 20;
                moi.DanhMuc = "Thi đua cấp cơ sở";
                moi.ChiTietDanhMuc = "";
                moi.DVTinh = "Người";
                if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                {
                    switch (cbbQuyBC.SelectedItem.ToString())
                    {
                        case "1":
                            moi.ChiTieuCaQuy = 1;
                            break;
                        case "2":
                            moi.ChiTieuCaQuy = 1;
                            break;
                        case "3":
                            moi.ChiTieuCaQuy = 1;
                            break;
                        case "4":
                            moi.ChiTieuCaQuy = 1;
                            break;
                    }
                }
                else
                {
                    moi.ChiTieuCaQuy = 0;
                }
                _lContent.Add(moi);
                #endregion
                #region 25. Đề tài NCKH+Sáng kiến
                moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                moi.Stt = 21;
                moi.DanhMuc = "Đề tài NCKH + Sáng kiến";
                moi.ChiTietDanhMuc = "";
                moi.DVTinh = "ĐT+SK";
                if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                {
                    switch (cbbQuyBC.SelectedItem.ToString())
                    {
                        case "1":
                            moi.ChiTieuCaQuy = 1;
                            break;
                        case "2":
                            moi.ChiTieuCaQuy = 1;
                            break;
                        case "3":
                            moi.ChiTieuCaQuy = 1;
                            break;
                        case "4":
                            moi.ChiTieuCaQuy = 1;
                            break;
                    }
                }
                else
                {
                    moi.ChiTieuCaQuy = 0;
                }
                _lContent.Add(moi);
                #endregion
                #region 26. Sở y tế khen
                moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                moi.Stt = 22;
                moi.DanhMuc = "Sở Y tế khen";
                moi.ChiTietDanhMuc = "";
                moi.DVTinh = "Người";
                if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                {
                    switch (cbbQuyBC.SelectedItem.ToString())
                    {
                        case "1":
                            moi.ChiTieuCaQuy = 1;
                            break;
                        case "2":
                            moi.ChiTieuCaQuy = 1;
                            break;
                        case "3":
                            moi.ChiTieuCaQuy = 1;
                            break;
                        case "4":
                            moi.ChiTieuCaQuy = 1;
                            break;
                    }
                }
                else
                {
                    moi.ChiTieuCaQuy = 0;
                }
                _lContent.Add(moi);
                #endregion
                #region 27. UBND Huyện khen
                moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                moi.Stt = 23;
                moi.DanhMuc = "UBND Huyện khen";
                moi.ChiTietDanhMuc = "";
                moi.DVTinh = "Người";
                if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                {
                    switch (cbbQuyBC.SelectedItem.ToString())
                    {
                        case "1":
                            moi.ChiTieuCaQuy = 1;
                            break;
                        case "2":
                            moi.ChiTieuCaQuy = 1;
                            break;
                        case "3":
                            moi.ChiTieuCaQuy = 1;
                            break;
                        case "4":
                            moi.ChiTieuCaQuy = 1;
                            break;
                    }
                }
                else
                {
                    moi.ChiTieuCaQuy = 0;
                }
                _lContent.Add(moi);
                #endregion
                #region 28. Giám đốc khen
                moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                moi.Stt = 24;
                moi.DanhMuc = "Giám đốc khen";
                moi.ChiTietDanhMuc = "";
                moi.DVTinh = "Người";
                if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                {
                    switch (cbbQuyBC.SelectedItem.ToString())
                    {
                        case "1":
                            moi.ChiTieuCaQuy = 1;
                            break;
                        case "2":
                            moi.ChiTieuCaQuy = 1;
                            break;
                        case "3":
                            moi.ChiTieuCaQuy = 1;
                            break;
                        case "4":
                            moi.ChiTieuCaQuy = 1;
                            break;
                    }
                }
                else
                {
                    moi.ChiTieuCaQuy = 0;
                }
                _lContent.Add(moi);
                #endregion
                #region 29. Bài tuyên truyền GDSK
                moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                moi.Stt = 25;
                moi.DanhMuc = "Bài tuyên truyền GDSK";
                moi.ChiTietDanhMuc = "";
                moi.DVTinh = "Bài";
                if (Convert.ToInt32(cbbNam.SelectedValue) == 2019)
                {
                    switch (cbbQuyBC.SelectedItem.ToString())
                    {
                        case "1":
                            moi.ChiTieuCaQuy = 2;
                            break;
                        case "2":
                            moi.ChiTieuCaQuy = 2;
                            break;
                        case "3":
                            moi.ChiTieuCaQuy = 2;
                            break;
                        case "4":
                            moi.ChiTieuCaQuy = 2;
                            break;
                    }
                }
                else
                {
                    moi.ChiTieuCaQuy = 0;
                }
                _lContent.Add(moi);
                #endregion
                #region 30. Danh hiệu thi đua
                moi = new Content(Convert.ToInt32(cbbQuyBC.SelectedItem), Convert.ToInt32(cbbNam.SelectedValue));
                moi.Stt = 26;
                moi.DanhMuc = "Danh hiệu thi đua";
                moi.ChiTietDanhMuc = "";
                moi.DVTinh = "";
                #endregion
            }
            BaoCao.Rep_BC_ChiTieuChuyenMon_BVDKKimThanh_Quy rep = new BaoCao.Rep_BC_ChiTieuChuyenMon_BVDKKimThanh_Quy();
            frmIn frm = new frmIn();
            rep.celChiTieuCM.Text = "Chỉ tiêu chuyên môn quý " + cbbQuyBC.SelectedValue;
            rep.celCaNam.Text = "Quý " + cbbQuyBC.SelectedItem;
            switch (Convert.ToInt32(cbbQuyBC.SelectedItem))
            {
                case 1:
                    rep.colThang1.Text = "Tháng 1";
                    rep.colThang2.Text = "Tháng 2";
                    rep.colThang3.Text = "Tháng 3";
                    break;
                case 2:
                    rep.colThang1.Text = "Tháng 4";
                    rep.colThang2.Text = "Tháng 5";
                    rep.colThang3.Text = "Tháng 6";
                    break;
                case 3:
                    rep.colThang1.Text = "Tháng 7";
                    rep.colThang2.Text = "Tháng 8";
                    rep.colThang3.Text = "Tháng 9";
                    break;
                case 4:
                    rep.colThang1.Text = "Tháng 10";
                    rep.colThang2.Text = "Tháng 11";
                    rep.colThang3.Text = "Tháng 12";
                    break;
            }
            if (_tenkhoa.Contains("Chọn tất cả"))
                rep.lblKhoa.Text = "Toàn viện";
            else
            {
                rep.lblKhoa.Text = _tenkhoa;
            }
            rep.DataSource = _lContent;
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
        #region số giường bệnh
        public int SoGiuong(string str)
        {
            int _sogiuong = 0;
            if (!string.IsNullOrEmpty(str))
            {
                List<string> buong = new List<string>();
                List<string> giuong = new List<string>();
                List<string> tam = new List<string>();
                buong = str.Split(';').ToList();
                foreach (var _buong in buong)
                {
                    tam = _buong.Split(',').ToList();
                    foreach (var _tam in tam)
                    {
                        giuong.Add(_tam);
                    }
                }
                _sogiuong = giuong.Count;
            }
            return _sogiuong;
        }

        public void GetNgayCuaQuy(int nam, int quy, ref DateTime ngayDau, ref DateTime ngayCuoi)
        {
            switch (quy)
            {
                case 1:
                    ngayDau = GetFirstDayOfMonth(nam, 1);
                    ngayCuoi = GetLastDayOfMonth(nam, 3);
                    break;
                case 2:
                    ngayDau = GetFirstDayOfMonth(nam, 4);
                    ngayCuoi = GetLastDayOfMonth(nam, 6);
                    break;
                case 3:
                    ngayDau = GetFirstDayOfMonth(nam, 7);
                    ngayCuoi = GetLastDayOfMonth(nam, 9);
                    break;
                case 4:
                    ngayDau = GetFirstDayOfMonth(nam, 10);
                    ngayCuoi = GetLastDayOfMonth(nam, 12);
                    break;
            }
        }
        #endregion
        #region Class Content
        public class Content
        {
            public Content(int quy, int nam)
            {
                this.Quy = quy;
                this.Nam = nam;

                switch (quy)
                {
                    case 1:
                        this.TuNgayQuy = GetFirstDayOfMonth(nam, 1);
                        this.DenNgayQuy = GetLastDayOfMonth(nam, 3);
                        this.TuNgayThang1 = GetFirstDayOfMonth(nam, 1);
                        this.DenNgayThang1 = GetLastDayOfMonth(nam, 1);
                        this.TuNgayThang2 = GetFirstDayOfMonth(nam, 2);
                        this.DenNgayThang2 = GetLastDayOfMonth(nam, 2);
                        this.TuNgayThang3 = GetFirstDayOfMonth(nam, 3);
                        this.DenNgayThang3 = GetLastDayOfMonth(nam, 3);
                        break;
                    case 2:
                        this.TuNgayQuy = GetFirstDayOfMonth(nam, 4);
                        this.DenNgayQuy = GetLastDayOfMonth(nam, 6);
                        this.TuNgayThang1 = GetFirstDayOfMonth(nam, 4);
                        this.DenNgayThang1 = GetLastDayOfMonth(nam, 4);
                        this.TuNgayThang2 = GetFirstDayOfMonth(nam, 5);
                        this.DenNgayThang2 = GetLastDayOfMonth(nam, 5);
                        this.TuNgayThang3 = GetFirstDayOfMonth(nam, 6);
                        this.DenNgayThang3 = GetLastDayOfMonth(nam, 6);
                        break;
                    case 3:
                        this.TuNgayQuy = GetFirstDayOfMonth(nam, 7);
                        this.DenNgayQuy = GetLastDayOfMonth(nam, 9);
                        this.TuNgayThang1 = GetFirstDayOfMonth(nam, 7);
                        this.DenNgayThang1 = GetLastDayOfMonth(nam, 7);
                        this.TuNgayThang2 = GetFirstDayOfMonth(nam, 8);
                        this.DenNgayThang2 = GetLastDayOfMonth(nam, 8);
                        this.TuNgayThang3 = GetFirstDayOfMonth(nam, 9);
                        this.DenNgayThang3 = GetLastDayOfMonth(nam, 9);
                        break;
                    case 4:
                        this.TuNgayQuy = GetFirstDayOfMonth(nam, 10);
                        this.DenNgayQuy = GetLastDayOfMonth(nam, 12);
                        this.TuNgayThang1 = GetFirstDayOfMonth(nam, 10);
                        this.DenNgayThang1 = GetLastDayOfMonth(nam, 10);
                        this.TuNgayThang2 = GetFirstDayOfMonth(nam, 11);
                        this.DenNgayThang2 = GetLastDayOfMonth(nam, 11);
                        this.TuNgayThang3 = GetFirstDayOfMonth(nam, 12);
                        this.DenNgayThang3 = GetLastDayOfMonth(nam, 12);
                        break;
                }
            }
            public int? Nam { get; set; }
            public int Quy { get; set; }
            public DateTime TuNgayQuy { get; set; }
            public DateTime DenNgayQuy { get; set; }
            public DateTime TuNgayThang1 { get; set; }
            public DateTime DenNgayThang1 { get; set; }
            public DateTime TuNgayThang2 { get; set; }
            public DateTime DenNgayThang2 { get; set; }
            public DateTime TuNgayThang3 { get; set; }
            public DateTime DenNgayThang3 { get; set; }
            public int Stt { get; set; }
            public string DanhMuc { get; set; }
            public string ChiTietDanhMuc { get; set; }
            public string DVTinh { get; set; }
            public double? ChiTieuCaQuy { get; set; }
            public double? ThucHienCaQuy { get; set; }
            public double? PtramCaQuy { get; set; }
            public double? ThucHienThang1 { get; set; }
            public double? PtramThang1 { get; set; }
            public double? ThucHienThang2 { get; set; }
            public double? PtramThang2 { get; set; }
            public double? ThucHienThang3 { get; set; }
            public double? PtramThang3 { get; set; }
        }
        #endregion

        private void cbbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(this.cbbKhoa.GetItemText(this.cbbKhoa.SelectedItem).ToUpper().Contains("KHOA LIÊN CHUYÊN KHOA"))
            //{
            //    labelControl3.Visible = true;
            //    boChuyenKhoa.Visible = true;
            //}
            //else
            //{
            //    labelControl3.Visible = false;
            //    boChuyenKhoa.Visible = false;
            //}
        }
    }
}