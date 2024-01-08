using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormThamSo
{
    public partial class frm_BC_ChiTieuChuyenMon_BVDKKimThanh : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_ChiTieuChuyenMon_BVDKKimThanh()
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            #region Ngày tháng
            DateTime tungay = new DateTime(Convert.ToInt32(cbbNam.SelectedValue), 1, 1, 00, 00, 00);
            DateTime denngay = new DateTime(Convert.ToInt32(cbbNam.SelectedValue), 12, 1, 23, 59, 59, 999);
            DateTime tungayquy1 = new DateTime(Convert.ToInt32(cbbNam.SelectedValue), 1, 1, 00, 00, 00);
            DateTime denngayquy1 = new DateTime(Convert.ToInt32(cbbNam.SelectedValue), 3, 31, 23, 59, 59, 999);
            DateTime tungayquy2 = new DateTime(Convert.ToInt32(cbbNam.SelectedValue), 4, 1, 00, 00, 00);
            DateTime denngayquy2 = new DateTime(Convert.ToInt32(cbbNam.SelectedValue), 6, 30, 23, 59, 59, 999);
            DateTime tungayquy3 = new DateTime(Convert.ToInt32(cbbNam.SelectedValue), 7, 1, 00, 00, 00);
            DateTime denngayquy3 = new DateTime(Convert.ToInt32(cbbNam.SelectedValue), 9, 30, 23, 59, 59, 999);
            DateTime tungayquy4 = new DateTime(Convert.ToInt32(cbbNam.SelectedValue), 10, 1, 00, 00, 00);
            DateTime denngayquy4 = new DateTime(Convert.ToInt32(cbbNam.SelectedValue), 12, 31, 23, 59, 59, 999);
            #endregion
            string _tenkhoa = this.cbbKhoa.GetItemText(this.cbbKhoa.SelectedItem);
            int _maK = Convert.ToInt32(cbbKhoa.SelectedValue);
            List<Content> _lContent = new List<Content>();
            Content moi = new Content();
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

                var qCLS = (from cls in data.CLS
                            join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                            join kp in data.KPhongs on cls.MaKP equals kp.MaKP
                            select new { cls.IdCLS, cls.NgayTH, cls.MaBNhan, cls.MaKP, kp.TenKP, bn.NoiTru, bn.DTNT }).ToList();
                var qCLS1 = (from cd in data.ChiDinhs
                             join dv in data.DichVus on cd.MaDV equals dv.MaDV
                             join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                             join dtct in data.DThuoccts on cd.IDCD equals dtct.IDCD
                             select new { cd.Status, cd.IdCLS, tn.TenRG, dv.Loai, dtct.IDCD, dv.TenDV })
                                        .Where(p => p.Status == 1 && p.IDCD != null).ToList();
                var qClsKhamSan = (from a in qCLS.Where(p => _maK == 0 || p.MaKP == _maK)
                                   join b in qCLS1 on a.IdCLS equals b.IdCLS
                                   select new { a.IdCLS, a.NgayTH, a.MaBNhan, a.MaKP, a.TenKP, a.NoiTru, a.DTNT, b.Status, b.TenRG, b.Loai, b.IDCD, b.TenDV }).Where(p => p.NoiTru == 1).ToList();

                var _luotKham = (from bnkb in data.BNKBs.Where(p => _maK == 0 || p.MaKP == _maK)
                                 join kp in data.KPhongs on bnkb.MaKP equals kp.MaKP
                                 group new { bnkb, kp } by new { bnkb.MaBNhan, kp.TenKP, bnkb.NgayKham } into kq
                                 select new { kq.Key.TenKP, kq.Key.NgayKham }).Where(p => p.TenKP.ToUpper().Contains(_tenkhoa.ToUpper())).ToList();
                #endregion

                #region 1.Tổng số CBCNV
                moi = new Content();
                moi.Stt = 1;
                moi.DanhMuc = "Tổng số CBCNV";
                moi.ChiTietDanhMuc = "";
                moi.DVTinh = "Người";
                moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 0 : 0;
                moi.ThucHienCaNam = canbo.Count;
                _lContent.Add(moi);
                #endregion
                #region 2. Giường bệnh
                moi = new Content();
                moi.Stt = 2;
                moi.DanhMuc = "Giường bệnh";
                moi.ChiTietDanhMuc = "";
                moi.DVTinh = "Người";
                moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 0 : 0;
                moi.ThucHienCaNam = (_maK != 0) ? SoGiuong(data.KPhongs.Where(p => p.MaKP == _maK).FirstOrDefault().BuongGiuong) : 0;
                _lContent.Add(moi);
                #endregion
                #region 3. Ngày điều trị trung bình
                moi = new Content();
                moi.Stt = 3;
                moi.DanhMuc = "Ngày điều trị trung bình";
                moi.ChiTietDanhMuc = "";
                moi.DVTinh = "Ngày";
                //moi.ChiTieuCaNam = 10;
                moi.ThucHienCaNam = (q3.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Count() != 0) ? (int)q3.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Sum(p => p.rv.SoNgaydt) / q3.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Count() : 0;
                //moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                moi.ThucHienQuyI = (q3.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Count() != 0) ? (int)q3.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Sum(p => p.rv.SoNgaydt) / q3.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Count() : 0;
                // moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                moi.ThucHienQuyII = (q3.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Count() != 0) ? (int)q3.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Sum(p => p.rv.SoNgaydt) / q3.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Count() : 0;
                // moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                moi.ThucHienQuyIII = (q3.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Count() != 0) ? (int)q3.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Sum(p => p.rv.SoNgaydt) / q3.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Count() : 0;
                //moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                moi.ThucHienQuyIV = (q3.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Count() != 0) ? (int)q3.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Sum(p => p.rv.SoNgaydt) / q3.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Count() : 0;
                //moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                //moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                //moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                //moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                //moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                //moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                _lContent.Add(moi);
                #endregion
                #region 4.Tổng số ngày điều trị
                moi = new Content();
                moi.Stt = 4;
                moi.DanhMuc = "Tổng số ngày điều trị";
                moi.ChiTietDanhMuc = "";
                moi.DVTinh = "Ngày";
                //moi.ChiTieuCaNam = 6200;
                moi.ThucHienCaNam = q3.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Sum(p => p.rv.SoNgaydt);
                //moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                moi.ThucHienQuyI = q3.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Sum(p => p.rv.SoNgaydt);
                //moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                moi.ThucHienQuyII = q3.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Sum(p => p.rv.SoNgaydt);
                //moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                moi.ThucHienQuyIII = q3.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Sum(p => p.rv.SoNgaydt);
                //moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                moi.ThucHienQuyIV = q3.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Sum(p => p.rv.SoNgaydt);
                //moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                //moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                //moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                //moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                //moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                //moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                _lContent.Add(moi);
                #endregion
                #region 5. Ngày sử dụng giường/tháng
                moi = new Content();
                moi.Stt = 5;
                moi.DanhMuc = "Ngày sử dụng giường/tháng";
                moi.ChiTietDanhMuc = "";
                moi.DVTinh = "Ngày";
                //moi.ChiTieuCaNam = 30;
                _lContent.Add(moi);
                #endregion
                #region 6. Công suất sử dụng giường bệnh
                moi = new Content();
                moi.Stt = 6;
                moi.DanhMuc = "Công suất sử dụng giường bệnh";
                moi.ChiTietDanhMuc = "";
                moi.DVTinh = "%";
                moi.ChiTieuCaNam = 100;
                _lContent.Add(moi);
                #endregion
                #region 7. Tai biến trong điều trị
                moi = new Content();
                moi.Stt = 7;
                moi.DanhMuc = "Tai biến trong điều trị";
                moi.ChiTietDanhMuc = "";
                moi.DVTinh = "Ca";
                moi.ChiTieuCaNam = 0;
                _lContent.Add(moi);
                #endregion

                #region Khoa sản
                if (_tenkhoa.ToUpper().Contains("KHOA SẢN"))
                {
                    #region 9. Tổng số khám
                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Tổng số khám";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 6500 : 0;
                    moi.ThucHienCaNam = _luotKham.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = _luotKham.Where(p => p.NgayKham >= tungayquy1 && p.NgayKham <= denngayquy1).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = _luotKham.Where(p => p.NgayKham >= tungayquy2 && p.NgayKham <= denngayquy2).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = _luotKham.Where(p => p.NgayKham >= tungayquy3 && p.NgayKham <= denngayquy3).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = _luotKham.Where(p => p.NgayKham >= tungayquy4 && p.NgayKham <= denngayquy4).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 10.Trong đó khám phụ khoa
                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Trong đó khám phụ khoa";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1800 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 11. Cận lâm sàng tại phòng khám sản
                    moi = new Content();
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám sản";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám sản";
                    moi.ChiTietDanhMuc = "Huyết học";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 10 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám sản";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 650 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám sản";
                    moi.ChiTietDanhMuc = "Sinh hóa nước tiểu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 100 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám sản";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 6500 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám sản";
                    moi.ChiTietDanhMuc = "Điện tim";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 6500 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám sản";
                    moi.ChiTietDanhMuc = "- Tỷ lệ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 15.5 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám sản";
                    moi.ChiTietDanhMuc = "Siêu âm";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 6500 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM SẢN")).Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám sản";
                    moi.ChiTietDanhMuc = "- Tỷ lệ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 15.5 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 12. Bệnh nhân điều trị nội trú
                    var q8 = (from rv in data.RaViens.Where(p => _maK == 0 || p.MaKP == _maK)
                              join a in data.BenhNhans.Where(p => p.NoiTru == 1 && p.DTNT == false) on rv.MaBNhan equals a.MaBNhan
                              //group new { a, rv } by new { a.MaBNhan } into kq
                              select new { a.MaBNhan, rv.NgayRa, rv.KetQua, rv.MaBVC, a.Tuoi }).ToList();
                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3139 : 0; ;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Khỏi";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 95 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2982 : 0;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Đỡ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3.9 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 125 : 0;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Chuyển viện";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1.1 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 32 : 0;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.MaBVC != null).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.MaBVC != null).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.MaBVC != null).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.MaBVC != null).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.MaBVC != null).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân nặng hơn xin về";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 32 : 0;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân tử vong sơ sinh (đủ tháng)";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 13. Cận lâm sàng (BN nội trú)
                    moi = new Content();
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Huyết học";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 100 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3139 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Sinh hóa máu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 796 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 25000 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Sinh hóa nước tiểu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 100 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3139 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Điện tim";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 30 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 942 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Siêu âm";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 100 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3139 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                  || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "X-quang";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 94 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Test HIV";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 80 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2511 : 0;

                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Test khác";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 80 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 13;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2511 : 0;

                    _lContent.Add(moi);
                    #endregion
                    #region 14.Mổ
                    moi = new Content();
                    moi.Stt = 14;
                    moi.DanhMuc = "Mổ";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Ca";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 700 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 14;
                    moi.DanhMuc = "Mổ";
                    moi.ChiTietDanhMuc = "+ Loại I";
                    moi.DVTinh = "Ca";
                    moi.ChiTieuCaNam = null;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 1).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 1).Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 1).Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 1).Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 1).Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 14;
                    moi.DanhMuc = "Mổ";
                    moi.ChiTietDanhMuc = "+ Loại II";
                    moi.DVTinh = "Ca";
                    moi.ChiTieuCaNam = null;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 2).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 2).Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 2).Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 2).Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 2).Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 14;
                    moi.DanhMuc = "Mổ";
                    moi.ChiTietDanhMuc = "+ Loại III";
                    moi.DVTinh = "Ca";
                    moi.ChiTieuCaNam = null;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 3).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 3).Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 3).Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 3).Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 3).Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
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
                    moi = new Content();
                    moi.Stt = 15;
                    moi.DanhMuc = "Đẻ thường";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 900 : 0;
                    moi.ThucHienCaNam = bnDe.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Where(p => p.TenDV.Contains("Đỡ đẻ")).Count();
                    moi.ThucHienQuyI = bnDe.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Where(p => p.TenDV.Contains("Đỡ đẻ")).Count();
                    moi.ThucHienQuyII = bnDe.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Where(p => p.TenDV.Contains("Đỡ đẻ")).Count();
                    moi.ThucHienQuyIII = bnDe.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Where(p => p.TenDV.Contains("Đỡ đẻ")).Count();
                    moi.ThucHienQuyIV = bnDe.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Where(p => p.TenDV.Contains("Đỡ đẻ")).Count();
                    _lContent.Add(moi);
                    #endregion
                    #region 16. Đẻ khó
                    moi = new Content();
                    moi.Stt = 16;
                    moi.DanhMuc = "Đẻ khó";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1000 : 0;
                    moi.ThucHienCaNam = bnDe.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Where(p => p.TenDV.Contains("Phẫu thuật lấy thai")).Count();
                    moi.ThucHienQuyI = bnDe.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Where(p => p.TenDV.Contains("Phẫu thuật lấy thai")).Count();
                    moi.ThucHienQuyII = bnDe.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Where(p => p.TenDV.Contains("Phẫu thuật lấy thai")).Count();
                    moi.ThucHienQuyIII = bnDe.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Where(p => p.TenDV.Contains("Phẫu thuật lấy thai")).Count();
                    moi.ThucHienQuyIV = bnDe.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Where(p => p.TenDV.Contains("Phẫu thuật lấy thai")).Count();
                    _lContent.Add(moi);
                    #endregion
                    #region 17. Nạo phá thai
                    moi = new Content();
                    moi.Stt = 17;
                    moi.DanhMuc = "Nạo phá thai";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 60 : 0;
                    moi.ThucHienCaNam = bnDe.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Where(p => p.TenDV.Contains("Phá thai")).Count();
                    moi.ThucHienQuyI = bnDe.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Where(p => p.TenDV.Contains("Phá thai")).Count();
                    moi.ThucHienQuyII = bnDe.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Where(p => p.TenDV.Contains("Phá thai")).Count();
                    moi.ThucHienQuyIII = bnDe.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Where(p => p.TenDV.Contains("Phá thai")).Count();
                    moi.ThucHienQuyIV = bnDe.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Where(p => p.TenDV.Contains("Phá thai")).Count();
                    _lContent.Add(moi);
                    #endregion
                    #region 18. Thủ thuật khác
                    moi = new Content();
                    moi.Stt = 18;
                    moi.DanhMuc = "Thủ thuật khác";
                    moi.DVTinh = "Lần";
                    moi.ChiTietDanhMuc = "";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 2000 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG == "Thủ thuật").Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG == "Thủ thuật").Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG == "Thủ thuật").Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG == "Thủ thuật").Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA SẢN")).Where(p => p.TenRG == "Thủ thuật").Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    _lContent.Add(moi);
                    #endregion
                    #region 19. Kiểm tra khoa phòng
                    moi = new Content();
                    moi.Stt = 19;
                    moi.DanhMuc = "Kiểm tra khoa phòng";
                    moi.DVTinh = "Lần";
                    moi.ChiTietDanhMuc = "";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 12 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 20. Kiểm tra tuyến
                    moi = new Content();
                    moi.Stt = 20;
                    moi.DanhMuc = "Kiểm tra tuyến";
                    moi.DVTinh = "Lần";
                    moi.ChiTietDanhMuc = "";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 12 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 21. sinh hoạt bệnh nhân
                    moi = new Content();
                    moi.Stt = 21;
                    moi.DanhMuc = "Sinh hoạt bệnh nhân";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = 24;
                    _lContent.Add(moi);
                    #endregion
                    #region 22. Chiến sĩ thi đua cấp ngành
                    moi = new Content();
                    moi.Stt = 22;
                    moi.DanhMuc = "Chiến sĩ thi đua cấp ngành";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 24. Kỹ thuật mới
                    moi = new Content();
                    moi.Stt = 24;
                    moi.DanhMuc = "Kỹ thuật mới";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "KT";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 25. Thi đua cấp cơ sở
                    moi = new Content();
                    moi.Stt = 25;
                    moi.DanhMuc = "Thi đua cấp cơ sở";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 26. Đề tài NCKH+Sáng kiến
                    moi = new Content();
                    moi.Stt = 26;
                    moi.DanhMuc = "Đề tài NCKH + Sáng kiến";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "ĐT+SK";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 27. Tập huấn chuyên môn xã
                    moi = new Content();
                    moi.Stt = 27;
                    moi.DanhMuc = "Tập huấn chuyên môn xã";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = 2;
                    _lContent.Add(moi);
                    #endregion
                    #region 28. Đề tài NCKH+Sáng kiến
                    moi = new Content();
                    moi.Stt = 28;
                    moi.DanhMuc = "Đè tài NCKH + Sáng kiến";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "ĐT+SK";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 29. Bài tuyên truyền GDSK
                    moi = new Content();
                    moi.Stt = 29;
                    moi.DanhMuc = "Bài tuyên truyền GDSK";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Bài";
                    moi.ChiTieuCaNam = 10;
                    _lContent.Add(moi);
                    #endregion
                    #region 30. Danh hiệu khen thưởng
                    moi = new Content();
                    moi.Stt = 30;
                    moi.DanhMuc = "Danh hiệu khen thưởng";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    moi.ChiTieuCaNam = null;
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
                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3346 : 0; ;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Khỏi";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 78.7 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2633 : 0;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Đỡ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 19 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 636 : 0;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Chuyển viện";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1.9 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 63 : 0;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.MaBVC != null).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.MaBVC != null).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.MaBVC != null).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.MaBVC != null).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.MaBVC != null).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân nặng hơn xin về";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 0.4 : 0; ;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 14 : 0;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân tử vong";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = 0;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua == "Tử vong").Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.KetQua == "Tử vong").Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.KetQua == "Tử vong").Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.KetQua == "Tử vong").Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Tử vong").Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 9. Cận lâm sàng (BN nội trú)
                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    moi.ChiTieuCaNam = null;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI"))
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Huyết học";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 100 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3346 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Sinh hóa máu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 20000 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Sinh hóa nước tiểu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 100 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3346 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Đờm";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 100 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Siêu âm";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 54 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1800 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                  || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "X-quang";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 48 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1600 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Điện tâm đồ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 90 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3000 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Đo chức năng hô hấp";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 400 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                       .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap)).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap)).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap)).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap)).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NỘI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap)).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 10. Kiểm tra khoa phòng
                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Kiểm tra khoa phòng";
                    moi.DVTinh = "Lần";
                    moi.ChiTietDanhMuc = "";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 12 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 10. Kiểm tra tuyến
                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Kiểm tra tuyến";
                    moi.DVTinh = "Lần";
                    moi.ChiTietDanhMuc = "";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 12 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 11. sinh hoạt bệnh nhân
                    moi = new Content();
                    moi.Stt = 11;
                    moi.DanhMuc = "Sinh hoạt bệnh nhân";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 12 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 12. Kỹ thuật mới
                    moi = new Content();
                    moi.Stt = 12;
                    moi.DanhMuc = "Kỹ thuật mới";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "KT";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 13. Chiến sĩ thi đua cấp ngành
                    moi = new Content();
                    moi.Stt = 13;
                    moi.DanhMuc = "Chiến sĩ thi đua cấp ngành";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 14. Thi đua cấp cơ sở
                    moi = new Content();
                    moi.Stt = 14;
                    moi.DanhMuc = "Thi đua cấp cơ sở";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 15. Đề tài NCKH+Sáng kiến
                    moi = new Content();
                    moi.Stt = 15;
                    moi.DanhMuc = "Đề tài NCKH + Sáng kiến";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "ĐT+SK";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 16. Tập huấn chuyên môn xã
                    moi = new Content();
                    moi.Stt = 16;
                    moi.DanhMuc = "Tập huấn chuyên môn xã";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 17. Đề tài NCKH+Sáng kiến
                    moi = new Content();
                    moi.Stt = 17;
                    moi.DanhMuc = "Đè tài NCKH + Sáng kiến";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "ĐT+SK";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 18. Bài tuyên truyền GDSK
                    moi = new Content();
                    moi.Stt = 18;
                    moi.DanhMuc = "Bài tuyên truyền GDSK";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Bài";
                    moi.ChiTieuCaNam = 10;
                    _lContent.Add(moi);
                    #endregion
                    #region 19. Danh hiệu khen thưởng
                    moi = new Content();
                    moi.Stt = 19;
                    moi.DanhMuc = "Danh hiệu khen thưởng";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    moi.ChiTieuCaNam = null;
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
                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2075 : 0; ;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Khỏi";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 60 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1245 : 0;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Đỡ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 32 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 664 : 0;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Chuyển viện";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 5 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 104 : 0;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.MaBVC != null).Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.MaBVC != null).Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.MaBVC != null).Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.MaBVC != null).Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.MaBVC != null).Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân nặng hơn xin về";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3 : 0; ;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 62 : 0;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 9. tai nạn thương tích
                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Tổng tai nạn thương tích";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 50 : 0; ;
                    moi.ThucHienCaNam = qTaiNan.Where(p => p.NNhap >= tungay && p.NNhap <= denngay).Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = qTaiNan.Where(p => p.NNhap >= tungayquy1 && p.NNhap <= denngayquy1).Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = qTaiNan.Where(p => p.NNhap >= tungayquy2 && p.NNhap <= denngayquy2).Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = qTaiNan.Where(p => p.NNhap >= tungayquy3 && p.NNhap <= denngayquy3).Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = qTaiNan.Where(p => p.NNhap >= tungayquy4 && p.NNhap <= denngayquy4).Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Tổng tai nạn thương tích";
                    moi.ChiTietDanhMuc = "- Tự tử";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 18 : 0;
                    moi.ThucHienCaNam = qTaiNan.Where(p => p.NNhap >= tungay && p.NNhap <= denngay).Where(p => p.ChuyenKhoa == "Tự tử").Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = qTaiNan.Where(p => p.NNhap >= tungayquy1 && p.NNhap <= denngayquy1).Where(p => p.ChuyenKhoa == "Tự tử").Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = qTaiNan.Where(p => p.NNhap >= tungayquy2 && p.NNhap <= denngayquy2).Where(p => p.ChuyenKhoa == "Tự tử").Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = qTaiNan.Where(p => p.NNhap >= tungayquy3 && p.NNhap <= denngayquy3).Where(p => p.ChuyenKhoa == "Tự tử").Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = qTaiNan.Where(p => p.NNhap >= tungayquy4 && p.NNhap <= denngayquy4).Where(p => p.ChuyenKhoa == "Tự tử").Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Tổng tai nạn thương tích";
                    moi.ChiTietDanhMuc = "- Ngộ độc";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 664 : 0;
                    moi.ThucHienCaNam = qTaiNan.Where(p => p.NNhap >= tungay && p.NNhap <= denngay).Where(p => p.ChuyenKhoa == "Ngộ độc").Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = qTaiNan.Where(p => p.NNhap >= tungayquy1 && p.NNhap <= denngayquy1).Where(p => p.ChuyenKhoa == "Ngộ độc").Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = qTaiNan.Where(p => p.NNhap >= tungayquy2 && p.NNhap <= denngayquy2).Where(p => p.ChuyenKhoa == "Ngộ độc").Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = qTaiNan.Where(p => p.NNhap >= tungayquy3 && p.NNhap <= denngayquy3).Where(p => p.ChuyenKhoa == "Ngộ độc").Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = qTaiNan.Where(p => p.NNhap >= tungayquy4 && p.NNhap <= denngayquy4).Where(p => p.ChuyenKhoa == "Ngộ độc").Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Tổng tai nạn thương tích";
                    moi.ChiTietDanhMuc = "- Khác";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 104 : 0;
                    moi.ThucHienCaNam = TNKhac.Where(p => p.NNhap >= tungay && p.NNhap <= denngay).Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = TNKhac.Where(p => p.NNhap >= tungayquy1 && p.NNhap <= denngayquy1).Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = TNKhac.Where(p => p.NNhap >= tungayquy2 && p.NNhap <= denngayquy2).Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = TNKhac.Where(p => p.NNhap >= tungayquy3 && p.NNhap <= denngayquy3).Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = TNKhac.Where(p => p.NNhap >= tungayquy4 && p.NNhap <= denngayquy4).Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 10. Cận lâm sàng (BN nội trú)
                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Huyết học";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 106 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2200 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Sinh hóa máu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 13800 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Sinh hóa nước tiểu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 97 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2008 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Siêu âm";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 35 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 726 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                  || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "X-quang";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 40 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 830 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Điện tâm đồ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 60 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1238 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 11. Thủ thuật khác
                    moi = new Content();
                    moi.Stt = 11;
                    moi.DanhMuc = "Thủ thuật";
                    moi.DVTinh = "Ca";
                    moi.ChiTietDanhMuc = "";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 3118 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN"))
                                        .Where(p => p.TenRG == "Thủ thuật").Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN"))
                                        .Where(p => p.TenRG == "Thủ thuật").Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN"))
                                        .Where(p => p.TenRG == "Thủ thuật").Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN"))
                                        .Where(p => p.TenRG == "Thủ thuật").Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA HỒI SỨC CẤP CỨU") || p.TenKP.ToUpper().Contains("Khoa HSCC - TN"))
                                        .Where(p => p.TenRG == "Thủ thuật").Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    _lContent.Add(moi);
                    #endregion
                    #region 12. tỷ lệ dùng kháng sinh
                    moi = new Content();
                    moi.Stt = 12;
                    moi.DanhMuc = "Tỷ lệ dùng kháng sinh";
                    moi.DVTinh = "%";
                    moi.ChiTietDanhMuc = "";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 13. Kiểm tra khoa phòng
                    moi = new Content();
                    moi.Stt = 13;
                    moi.DanhMuc = "Kiểm tra khoa phòng";
                    moi.DVTinh = "Lần";
                    moi.ChiTietDanhMuc = "";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 24 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 14. Kiểm tra tuyến
                    moi = new Content();
                    moi.Stt = 14;
                    moi.DanhMuc = "Kiểm tra tuyến";
                    moi.DVTinh = "Lần";
                    moi.ChiTietDanhMuc = "";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 12 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 15. sinh hoạt bệnh nhân
                    moi = new Content();
                    moi.Stt = 15;
                    moi.DanhMuc = "Sinh hoạt bệnh nhân";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 24 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 16. Kỹ thuật mới
                    moi = new Content();
                    moi.Stt = 16;
                    moi.DanhMuc = "Kỹ thuật mới";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "KT";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 17. Thi đua cấp cơ sở
                    moi = new Content();
                    moi.Stt = 17;
                    moi.DanhMuc = "Thi đua cấp cơ sở";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 18. Đề tài NCKH+Sáng kiến
                    moi = new Content();
                    moi.Stt = 18;
                    moi.DanhMuc = "Đề tài NCKH + Sáng kiến";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "ĐT+SK";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 19. Tập huấn chuyên môn xã
                    moi = new Content();
                    moi.Stt = 19;
                    moi.DanhMuc = "Tập huấn chuyên môn xã";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 20. Bài tuyên truyền GDSK
                    moi = new Content();
                    moi.Stt = 20;
                    moi.DanhMuc = "Bài tuyên truyền GDSK";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Bài";
                    moi.ChiTieuCaNam = 2;
                    _lContent.Add(moi);
                    #endregion
                    #region 21. Danh hiệu khen thưởng
                    moi = new Content();
                    moi.Stt = 21;
                    moi.DanhMuc = "Danh hiệu khen thưởng";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    moi.ChiTieuCaNam = null;
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
                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1156 : 0; ;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Khỏi";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 80 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 925 : 0;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Đỡ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 17 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 197 : 0;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Chuyển viện";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 23 : 0;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.MaBVC != null).Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.MaBVC != null).Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.MaBVC != null).Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.MaBVC != null).Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.MaBVC != null).Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân nặng hơn xin về";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1 : 0; ;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 11 : 0;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân tử vong";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 0 : 0; ;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 0 : 0;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua == "Tử vong").Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.KetQua == "Tử vong").Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.KetQua == "Tử vong").Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.KetQua == "Tử vong").Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Tử vong").Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 9. Cận lâm sàng (BN nội trú)
                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Huyết học";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 100 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1156 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Sinh hóa máu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 578 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Sinh hóa nước tiểu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 20 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 231 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Siêu âm";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 20 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 231 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                  || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "X-quang";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 10 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 116 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Điện tâm đồ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 10 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 116 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.DVTinh = "Lần";
                    moi.ChiTietDanhMuc = "Thủ thuật";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 264 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI"))
                                        .Where(p => p.TenRG == "Thủ thuật").Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI"))
                                        .Where(p => p.TenRG == "Thủ thuật").Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI"))
                                        .Where(p => p.TenRG == "Thủ thuật").Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI"))
                                        .Where(p => p.TenRG == "Thủ thuật").Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NHI"))
                                        .Where(p => p.TenRG == "Thủ thuật").Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    _lContent.Add(moi);
                    #endregion
                    #region 10. Kiểm tra khoa phòng
                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Kiểm tra khoa phòng";
                    moi.DVTinh = "Lần";
                    moi.ChiTietDanhMuc = "";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 24 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 11. Kiểm tra tuyến
                    moi = new Content();
                    moi.Stt = 11;
                    moi.DanhMuc = "Kiểm tra tuyến";
                    moi.DVTinh = "Lần";
                    moi.ChiTietDanhMuc = "";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 12 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 12. sinh hoạt bệnh nhân
                    moi = new Content();
                    moi.Stt = 12;
                    moi.DanhMuc = "Sinh hoạt bệnh nhân";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 12 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 13. Kỹ thuật mới
                    moi = new Content();
                    moi.Stt = 13;
                    moi.DanhMuc = "Kỹ thuật mới";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "KT";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 14. Thi đua cấp ngành
                    moi = new Content();
                    moi.Stt = 14;
                    moi.DanhMuc = "Chiến sĩ thi đua cấp ngành";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 15. Thi đua cấp cơ sở
                    moi = new Content();
                    moi.Stt = 15;
                    moi.DanhMuc = "Thi đua cấp cơ sở";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 16. Đề tài NCKH+Sáng kiến
                    moi = new Content();
                    moi.Stt = 16;
                    moi.DanhMuc = "Đề tài NCKH + Sáng kiến";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "ĐT+SK";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 17. Tập huấn chuyên môn xã
                    moi = new Content();
                    moi.Stt = 17;
                    moi.DanhMuc = "Tập huấn chuyên môn xã";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 18. Bài tuyên truyền GDSK
                    moi = new Content();
                    moi.Stt = 18;
                    moi.DanhMuc = "Bài tuyên truyền GDSK";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Bài";
                    moi.ChiTieuCaNam = 2;
                    _lContent.Add(moi);
                    #endregion
                    #region 19. Danh hiệu khen thưởng
                    moi = new Content();
                    moi.Stt = 19;
                    moi.DanhMuc = "Danh hiệu khen thưởng";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    moi.ChiTieuCaNam = null;
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
                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3212 : 0; ;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Khỏi";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 80 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2570 : 0;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Đỡ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 16.7 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 536 : 0;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Chuyển viện";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 96 : 0;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.MaBVC != null).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.MaBVC != null).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.MaBVC != null).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.MaBVC != null).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.MaBVC != null).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân nặng hơn xin về";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 0.3 : 0; ;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 10 : 0;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân tử vong";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 0 : 0; ;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 0 : 0;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua == "Tử vong").Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.KetQua == "Tử vong").Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.KetQua == "Tử vong").Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.KetQua == "Tử vong").Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Tử vong").Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 9. Tổng số khám
                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Tổng số khám";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 7000 : 0;
                    moi.ThucHienCaNam = _luotKham.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = _luotKham.Where(p => p.NgayKham >= tungayquy1 && p.NgayKham <= denngayquy1).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = _luotKham.Where(p => p.NgayKham >= tungayquy2 && p.NgayKham <= denngayquy2).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = _luotKham.Where(p => p.NgayKham >= tungayquy3 && p.NgayKham <= denngayquy3).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = _luotKham.Where(p => p.NgayKham >= tungayquy4 && p.NgayKham <= denngayquy4).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 10. Cận lâm sàng tại phòng khám ngoại CK
                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám ngoại CK";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám ngoại";
                    moi.ChiTietDanhMuc = "Huyết học";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 5000 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám ngoại";
                    moi.ChiTietDanhMuc = "- Tỷ lệ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 72 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám ngoại";
                    moi.ChiTietDanhMuc = "Sinh hóa nước tiểu";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2800 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám ngoại";
                    moi.ChiTietDanhMuc = "- Tỷ lệ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 40 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám ngoại";
                    moi.ChiTietDanhMuc = "Sinh hóa máu";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 18000 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám ngoại";
                    moi.ChiTietDanhMuc = "- Tỷ lệ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 258 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám ngoại";
                    moi.ChiTietDanhMuc = "Siêu âm";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2800 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám ngoại";
                    moi.ChiTietDanhMuc = "- Tỷ lệ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 40 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám ngoại";
                    moi.ChiTietDanhMuc = "X-quang";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3500 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("PHÒNG KHÁM NGOẠI")).Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Cận lâm sàng tại Phòng khám ngoại";
                    moi.ChiTietDanhMuc = "- Tỷ lệ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 50 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 11. Cận lâm sàng (BN nội trú)
                    moi = new Content();
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Truyền máu";
                    moi.DVTinh = "ui";
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Huyết học";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 100 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3212 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Sinh hóa máu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 19000 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Sinh hóa nước tiểu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 100 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3212 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Siêu âm";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 62 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2000 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                  || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "X-quang";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 62 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2000 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Điện tim";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 20 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 11;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 640 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 12. Phẫu thuật
                    moi = new Content();
                    moi.Stt = 12;
                    moi.DanhMuc = "Phẫu thuật";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Ca";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 400 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 12;
                    moi.DanhMuc = "Phẫu thuật";
                    moi.ChiTietDanhMuc = "+ Loại I";
                    moi.DVTinh = "Ca";
                    moi.ChiTieuCaNam = null;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 1).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 1).Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 1).Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 1).Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 1).Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 12;
                    moi.DanhMuc = "Phẫu thuật";
                    moi.ChiTietDanhMuc = "+ Loại II";
                    moi.DVTinh = "Ca";
                    moi.ChiTieuCaNam = null;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 2).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 2).Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 2).Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 2).Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 2).Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 12;
                    moi.DanhMuc = "Phẫu thuật";
                    moi.ChiTietDanhMuc = "+ Loại III";
                    moi.DVTinh = "Ca";
                    moi.ChiTieuCaNam = null;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 3).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 3).Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 3).Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 3).Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                        .Where(p => p.Loai == 3).Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    _lContent.Add(moi);
                    #endregion
                    #region 13. Tổng tiểu phẫu
                    moi = new Content();
                    moi.Stt = 13;
                    moi.DanhMuc = "Tổng tiểu phẫu";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Ca";
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 13;
                    moi.DanhMuc = "Tổng tiểu phẫu";
                    moi.ChiTietDanhMuc = "Bó bột";
                    moi.DVTinh = "Ca";
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenDV.Contains("bó bột"))
                                        .Where(p => p.Loai == 1).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenDV.Contains("bó bột"))
                                        .Where(p => p.Loai == 1).Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenDV.Contains("bó bột"))
                                        .Where(p => p.Loai == 1).Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenDV.Contains("bó bột"))
                                        .Where(p => p.Loai == 1).Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA NGOẠI")).Where(p => p.TenDV.Contains("bó bột"))
                                        .Where(p => p.Loai == 1).Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    _lContent.Add(moi);
                    #endregion
                    #region 14. tai nạn thương tích
                    moi = new Content();
                    moi.Stt = 14;
                    moi.DanhMuc = "Tổng tai nạn thương tích";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1000 : 0; ;
                    moi.ThucHienCaNam = qTaiNan.Where(p => p.NNhap >= tungay && p.NNhap <= denngay).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qTaiNan.Where(p => p.NNhap >= tungayquy1 && p.NNhap <= denngayquy1).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qTaiNan.Where(p => p.NNhap >= tungayquy2 && p.NNhap <= denngayquy2).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qTaiNan.Where(p => p.NNhap >= tungayquy3 && p.NNhap <= denngayquy3).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qTaiNan.Where(p => p.NNhap >= tungayquy4 && p.NNhap <= denngayquy4).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 14;
                    moi.DanhMuc = "Tổng tai nạn thương tích";
                    moi.ChiTietDanhMuc = "- TNGT";
                    moi.DVTinh = "Người";
                    moi.ThucHienCaNam = qTaiNan.Where(p => p.NNhap >= tungay && p.NNhap <= denngay).Where(p => p.ChuyenKhoa == "Tai nạn giao thông" || p.ChuyenKhoa == "Đường bộ"
                                                                                                        || p.ChuyenKhoa == "Đường sắt" || p.ChuyenKhoa == "Đường sông").Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = qTaiNan.Where(p => p.NNhap >= tungayquy1 && p.NNhap <= denngayquy1).Where(p => p.ChuyenKhoa == "Tai nạn giao thông" || p.ChuyenKhoa == "Đường bộ"
                                                                                                        || p.ChuyenKhoa == "Đường sắt" || p.ChuyenKhoa == "Đường sông").Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = qTaiNan.Where(p => p.NNhap >= tungayquy2 && p.NNhap <= denngayquy2).Where(p => p.ChuyenKhoa == "Tai nạn giao thông" || p.ChuyenKhoa == "Đường bộ"
                                                                                                        || p.ChuyenKhoa == "Đường sắt" || p.ChuyenKhoa == "Đường sông").Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = qTaiNan.Where(p => p.NNhap >= tungayquy3 && p.NNhap <= denngayquy3).Where(p => p.ChuyenKhoa == "Tai nạn giao thông" || p.ChuyenKhoa == "Đường bộ"
                                                                                                        || p.ChuyenKhoa == "Đường sắt" || p.ChuyenKhoa == "Đường sông").Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = qTaiNan.Where(p => p.NNhap >= tungayquy4 && p.NNhap <= denngayquy4).Where(p => p.ChuyenKhoa == "Tai nạn giao thông" || p.ChuyenKhoa == "Đường bộ"
                                                                                                        || p.ChuyenKhoa == "Đường sắt" || p.ChuyenKhoa == "Đường sông").Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 14;
                    moi.DanhMuc = "Tổng tai nạn thương tích";
                    moi.ChiTietDanhMuc = "- TNLĐ";
                    moi.DVTinh = "Người";
                    moi.ThucHienCaNam = qTaiNan.Where(p => p.NNhap >= tungay && p.NNhap <= denngay).Where(p => p.ChuyenKhoa == "Tai nạn lao động").Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = qTaiNan.Where(p => p.NNhap >= tungayquy1 && p.NNhap <= denngayquy1).Where(p => p.ChuyenKhoa == "Tai nạn lao động").Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = qTaiNan.Where(p => p.NNhap >= tungayquy2 && p.NNhap <= denngayquy2).Where(p => p.ChuyenKhoa == "Tai nạn lao động").Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = qTaiNan.Where(p => p.NNhap >= tungayquy3 && p.NNhap <= denngayquy3).Where(p => p.ChuyenKhoa == "Tai nạn lao động").Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = qTaiNan.Where(p => p.NNhap >= tungayquy4 && p.NNhap <= denngayquy4).Where(p => p.ChuyenKhoa == "Tai nạn lao động").Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 14;
                    moi.DanhMuc = "Tổng tai nạn thương tích";
                    moi.ChiTietDanhMuc = "- TNSH";
                    moi.DVTinh = "Người";
                    moi.ThucHienCaNam = qTaiNan.Where(p => p.NNhap >= tungay && p.NNhap <= denngay).Where(p => p.ChuyenKhoa == "Tai nạn sinh hoạt").Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = qTaiNan.Where(p => p.NNhap >= tungayquy1 && p.NNhap <= denngayquy1).Where(p => p.ChuyenKhoa == "Tai nạn sinh hoạt").Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = qTaiNan.Where(p => p.NNhap >= tungayquy2 && p.NNhap <= denngayquy2).Where(p => p.ChuyenKhoa == "Tai nạn sinh hoạt").Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = qTaiNan.Where(p => p.NNhap >= tungayquy3 && p.NNhap <= denngayquy3).Where(p => p.ChuyenKhoa == "Tai nạn sinh hoạt").Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = qTaiNan.Where(p => p.NNhap >= tungayquy4 && p.NNhap <= denngayquy4).Where(p => p.ChuyenKhoa == "Tai nạn sinh hoạt").Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 14;
                    moi.DanhMuc = "Tổng tai nạn thương tích";
                    moi.ChiTietDanhMuc = "- Đánh nhau";
                    moi.DVTinh = "Người";
                    moi.ThucHienCaNam = qTaiNan.Where(p => p.NNhap >= tungay && p.NNhap <= denngay).Where(p => p.ChuyenKhoa == "Đánh nhau").Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = qTaiNan.Where(p => p.NNhap >= tungayquy1 && p.NNhap <= denngayquy1).Where(p => p.ChuyenKhoa == "Đánh nhau").Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = qTaiNan.Where(p => p.NNhap >= tungayquy2 && p.NNhap <= denngayquy2).Where(p => p.ChuyenKhoa == "Đánh nhau").Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = qTaiNan.Where(p => p.NNhap >= tungayquy3 && p.NNhap <= denngayquy3).Where(p => p.ChuyenKhoa == "Đánh nhau").Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = qTaiNan.Where(p => p.NNhap >= tungayquy4 && p.NNhap <= denngayquy4).Where(p => p.ChuyenKhoa == "Đánh nhau").Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 14;
                    moi.DanhMuc = "Tổng tai nạn thương tích";
                    moi.ChiTietDanhMuc = "- Bỏng";
                    moi.DVTinh = "Người";
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 14;
                    moi.DanhMuc = "Tổng tai nạn thương tích";
                    moi.ChiTietDanhMuc = "- Khác";
                    moi.DVTinh = "Người";
                    moi.ThucHienCaNam = qTaiNan.Where(p => p.NNhap >= tungay && p.NNhap <= denngay).Where(p => p.ChuyenKhoa == "Khác").Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = qTaiNan.Where(p => p.NNhap >= tungayquy1 && p.NNhap <= denngayquy1).Where(p => p.ChuyenKhoa == "Khác").Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = qTaiNan.Where(p => p.NNhap >= tungayquy2 && p.NNhap <= denngayquy2).Where(p => p.ChuyenKhoa == "Khác").Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = qTaiNan.Where(p => p.NNhap >= tungayquy3 && p.NNhap <= denngayquy3).Where(p => p.ChuyenKhoa == "Khác").Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = qTaiNan.Where(p => p.NNhap >= tungayquy4 && p.NNhap <= denngayquy4).Where(p => p.ChuyenKhoa == "Khác").Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 15. Kiểm tra khoa phòng
                    moi = new Content();
                    moi.Stt = 15;
                    moi.DanhMuc = "Kiểm tra khoa phòng";
                    moi.DVTinh = "Lần";
                    moi.ChiTietDanhMuc = "";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 12 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 16. sinh hoạt bệnh nhân
                    moi = new Content();
                    moi.Stt = 16;
                    moi.DanhMuc = "Sinh hoạt bệnh nhân";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 12 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 17. Kỹ thuật mới
                    moi = new Content();
                    moi.Stt = 17;
                    moi.DanhMuc = "Kỹ thuật mới";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "KT";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 18. Chuyển giao kỹ thuật cho tuyến xã
                    moi = new Content();
                    moi.Stt = 18;
                    moi.DanhMuc = "Chuyển giao kỹ thuật cho tuyến xã";
                    moi.DVTinh = "KT";
                    moi.ChiTietDanhMuc = "";
                    _lContent.Add(moi);
                    #endregion
                    #region 19. Thi đua cấp ngành
                    moi = new Content();
                    moi.Stt = 19;
                    moi.DanhMuc = "Chiến sĩ thi đua cấp ngành";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 20. Thi đua cấp cơ sở
                    moi = new Content();
                    moi.Stt = 20;
                    moi.DanhMuc = "Thi đua cấp cơ sở";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 21. Đề tài NCKH+Sáng kiến
                    moi = new Content();
                    moi.Stt = 21;
                    moi.DanhMuc = "Đề tài NCKH + Sáng kiến";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "ĐT+SK";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 22. Bài tuyên truyền GDSK
                    moi = new Content();
                    moi.Stt = 22;
                    moi.DanhMuc = "Bài tuyên truyền GDSK";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Bài";
                    moi.ChiTieuCaNam = 2;
                    _lContent.Add(moi);
                    #endregion
                    #region 24. Danh hiệu khen thưởng
                    moi = new Content();
                    moi.Stt = 24;
                    moi.DanhMuc = "Danh hiệu khen thưởng";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    moi.ChiTieuCaNam = null;
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
                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 590 : 0; ;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Khỏi";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 80 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 472 : 0;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Khỏi").Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Đỡ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 13.5 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 80 : 0;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Chuyển viện";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 5 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 30 : 0;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.MaBVC != null).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.MaBVC != null).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.MaBVC != null).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.MaBVC != null).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.MaBVC != null).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân nặng hơn xin về";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1.5 : 0; ;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 10 : 0;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Nặng hơn").Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 9. Cận lâm sàng (BN nội trú)
                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Huyết học";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 100 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 590 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Sinh hóa máu";
                    moi.DVTinh = "%";
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1770 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Sinh hóa nước tiểu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 100 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 590 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Siêu âm";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 20 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 118 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                  || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm
                                                                                                                    || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "X-quang";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 40 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 236 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "Điện tim";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 20 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Cận lâm sàng (BN nội trú)";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 118 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM")).Where(p => p.NoiTru == 1)
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
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

                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Số người XN AFB";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 200 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Số người XN AFB";
                    moi.ChiTietDanhMuc = "Số tiêu bản";
                    moi.DVTinh = "T.bản";
                    moi.ChiTieuCaNam = null;
                    moi.ThucHienCaNam = q2.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM"))
                                        .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                    moi.ThucHienQuyI = q2.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM"))
                                        .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                    moi.ThucHienQuyII = q2.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM"))
                                        .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                    moi.ThucHienQuyIII = q2.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM"))
                                        .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                    moi.ThucHienQuyIV = q2.Where(p => p.TenKP.ToUpper().Contains("KHOA TRUYỀN NHIỄM"))
                                        .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
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
                    moi = new Content();
                    moi.Stt = 8;
                    moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 620 : 0; ;
                    moi.ThucHienCaNam = q8.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = q8.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = q8.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = q8.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = q8.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 9. Tỷ lệ điều trị khỏi
                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Tỷ lệ điều trị Khỏi";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = 80;
                    moi.ThucHienCaNam = (q3.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Count() != 0) ? q3.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Where(p => p.rv.KetQua == "Khỏi").Count() / q3.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Count() * 100 : 0;
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = (q3.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Count() != 0) ? q3.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Where(p => p.rv.KetQua == "Khỏi").Count() / q3.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Count() * 100 : 0;
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = (q3.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Count() != 0) ? q3.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Where(p => p.rv.KetQua == "Khỏi").Count() / q3.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Count() * 100 : 0;
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = (q3.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Count() != 0) ? q3.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Where(p => p.rv.KetQua == "Khỏi").Count() / q3.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Count() * 100 : 0;
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = (q3.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Count() != 0) ? q3.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Where(p => p.rv.KetQua == "Khỏi").Count() / q3.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Count() * 100 : 0;
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 9;
                    moi.DanhMuc = "Tỷ lệ điều trị Khỏi";
                    moi.ChiTietDanhMuc = "Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = 496;
                    moi.ThucHienCaNam = q3.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Where(p => p.rv.KetQua == "Khỏi").Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = q3.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Where(p => p.rv.KetQua == "Khỏi").Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = q3.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Where(p => p.rv.KetQua == "Khỏi").Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = q3.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Where(p => p.rv.KetQua == "Khỏi").Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = q3.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Where(p => p.rv.KetQua == "Khỏi").Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 10. Tỷ lệ điều trị đỡ
                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Tỷ lệ điều trị Đỡ";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = 18;
                    moi.ThucHienCaNam = (q3.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Count() != 0) ? q3.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Where(p => p.rv.KetQua == "Đỡ|Giảm").Count() / q3.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Count() * 100 : 0;
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = (q3.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Count() != 0) ? q3.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Where(p => p.rv.KetQua == "Đỡ|Giảm").Count() / q3.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Count() * 100 : 0;
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = (q3.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Count() != 0) ? q3.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Where(p => p.rv.KetQua == "Đỡ|Giảm").Count() / q3.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Count() * 100 : 0;
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = (q3.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Count() != 0) ? q3.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Where(p => p.rv.KetQua == "Đỡ|Giảm").Count() / q3.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Count() * 100 : 0;
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = (q3.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Count() != 0) ? q3.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Where(p => p.rv.KetQua == "Đỡ|Giảm").Count() / q3.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Count() * 100 : 0;
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 10;
                    moi.DanhMuc = "Tỷ lệ điều trị Đỡ";
                    moi.ChiTietDanhMuc = "Tổng số bệnh nhân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = 112;
                    moi.ThucHienCaNam = q3.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Where(p => p.rv.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = q3.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Where(p => p.rv.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = q3.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Where(p => p.rv.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = q3.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Where(p => p.rv.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = q3.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Where(p => p.rv.KetQua == "Đỡ|Giảm").Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 11. Tỷ lệ chuyển viện
                    moi = new Content();
                    moi.Stt = 11;
                    moi.DanhMuc = "Tỷ lệ chuyển viện";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = 2;
                    moi.ThucHienCaNam = (q3.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Count() != 0) ? q3.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Where(p => p.rv.MaBVC != null).Count() / q3.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Count() * 100 : 0;
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = (q3.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Count() != 0) ? q3.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Where(p => p.rv.MaBVC != null).Count() / q3.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Count() * 100 : 0;
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = (q3.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Count() != 0) ? q3.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Where(p => p.rv.MaBVC != null).Count() / q3.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Count() * 100 : 0;
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = (q3.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Count() != 0) ? q3.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Where(p => p.rv.MaBVC != null).Count() / q3.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Count() * 100 : 0;
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = (q3.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Count() != 0) ? q3.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Where(p => p.rv.MaBVC != null).Count() / q3.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Count() * 100 : 0;
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 11;
                    moi.DanhMuc = "Tỷ lệ chuyển viện";
                    moi.ChiTietDanhMuc = "Tổng số chuyển viện";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = 12;
                    moi.ThucHienCaNam = q3.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Where(p => p.rv.MaBVC != null).Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = q3.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Where(p => p.rv.MaBVC != null).Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = q3.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Where(p => p.rv.MaBVC != null).Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = q3.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Where(p => p.rv.MaBVC != null).Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = q3.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Where(p => p.rv.MaBVC != null).Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
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

                    moi = new Content();
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "";
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "Tỷ lệ huyết học";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = 100;
                    moi.ThucHienCaNam = (q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == "XN huyết học").Count() / q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count() * 100 : 0;
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = (q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == "XN huyết học").Count() / q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count() * 100 : 0;
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = (q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == "XN huyết học").Count() / q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count() * 100 : 0;
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = (q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == "XN huyết học").Count() / q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count() * 100 : 0;
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = (q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == "XN huyết học").Count() / q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count() * 100 : 0;
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "lần";
                    moi.ChiTieuCaNam = 620;
                    moi.ThucHienCaNam = q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == "XN huyết học").Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == "XN huyết học").Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == "XN huyết học").Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == "XN huyết học").Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == "XN huyết học").Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "Sinh hóa máu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = 200;
                    moi.ThucHienCaNam = (q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count() / q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count() * 100 : 0;
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = (q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count() / q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count() * 100 : 0;
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = (q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count() / q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count() * 100 : 0;
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = (q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count() / q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count() * 100 : 0;
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = (q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count() / q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count() * 100 : 0;
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "lần";
                    moi.ChiTieuCaNam = 1200;
                    moi.ThucHienCaNam = q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "Sinh hóa nước tiểu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = 100;
                    moi.ThucHienCaNam = (q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count() / q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count() * 100 : 0;
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = (q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count() / q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count() * 100 : 0;
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = (q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count() / q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count() * 100 : 0;
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = (q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count() / q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count() * 100 : 0;
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = (q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count() / q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count() * 100 : 0;
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "lần";
                    moi.ChiTieuCaNam = 620;
                    moi.ThucHienCaNam = q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "Điện tâm đồ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = 30;
                    moi.ThucHienCaNam = (q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count() / q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count() * 100 : 0;
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = (q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count() / q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count() * 100 : 0;
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = (q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count() / q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count() * 100 : 0;
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = (q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count() / q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count() * 100 : 0;
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = (q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count() / q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count() * 100 : 0;
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "lần";
                    moi.ChiTieuCaNam = 180;
                    moi.ThucHienCaNam = q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "Siêu âm";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = 20;
                    moi.ThucHienCaNam = (q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm ||
                                             p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count() / q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count() * 100 : 0;
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = (q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm ||
                                             p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count() / q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count() * 100 : 0;
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = (q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm ||
                                             p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count() / q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count() * 100 : 0;
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = (q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm ||
                                             p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count() / q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count() * 100 : 0;
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = (q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm ||
                                             p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count() / q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count() * 100 : 0;
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "lần";
                    moi.ChiTieuCaNam = 124;
                    moi.ThucHienCaNam = q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm ||
                                             p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm ||
                                             p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm ||
                                             p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm ||
                                             p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm ||
                                             p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)).Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "X-quang";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = 20;
                    moi.ThucHienCaNam = (q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count() / q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count() * 100 : 0;
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = (q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count() / q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count() * 100 : 0;
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = (q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count() / q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count() * 100 : 0;
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = (q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count() / q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count() * 100 : 0;
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = (q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count() / q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count() * 100 : 0;
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "lần";
                    moi.ChiTieuCaNam = 124;
                    moi.ThucHienCaNam = q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "Đo Chức năng hô hấp";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = 10;
                    moi.ThucHienCaNam = (q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count() / q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count() * 100 : 0;
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = (q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count() / q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count() * 100 : 0;
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = (q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count() / q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count() * 100 : 0;
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = (q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count() / q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count() * 100 : 0;
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = (q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count() / q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count() * 100 : 0;
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "lần";
                    moi.ChiTieuCaNam = 62;
                    moi.ThucHienCaNam = q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "Đo mật độ xương";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = 50;
                    moi.ThucHienCaNam = (q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count() / q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count() * 100 : 0;
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = (q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count() / q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count() * 100 : 0;
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = (q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count() / q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count() * 100 : 0;
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = (q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count() / q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count() * 100 : 0;
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = (q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count() != 0) ? q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count() / q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count() * 100 : 0;
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 12;
                    moi.DanhMuc = "Cận lâm sàm bệnh phòng";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "lần";
                    moi.ChiTieuCaNam = 310;
                    moi.ThucHienCaNam = q12_2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = q12_2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = q12_2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = q12_2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = q12_2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 13. Bệnh nhân khám YHCT
                    var kpdongy = (from kp in _khoaMoi.Where(p => _maK == 0 || p.makp == _maK) join ck in data.ChuyenKhoas.Where(p => p.TenCK == "Đông y") on kp.mack equals ck.MaCK select kp).ToList();
                    var q13 = (from bn in q3
                               join bnkb in data.BNKBs on bn.bn.MaBNhan equals bnkb.MaBNhan
                               join kp in kpdongy on bnkb.MaKP equals kp.makp
                               join dtbn in data.DTBNs on bn.bn.IDDTBN equals dtbn.IDDTBN
                               select new { bn, dtbn, bn.rv }).Distinct().ToList();

                    moi = new Content();
                    moi.Stt = 13;
                    moi.DanhMuc = "Bệnh nhân khám YHCT";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 13;
                    moi.DanhMuc = "Bệnh nhân khám YHCT";
                    moi.ChiTietDanhMuc = "Bệnh nhân khám BHYT";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = 1800;
                    moi.ThucHienCaNam = q13.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Where(p => p.dtbn.DTBN1.Contains("BHYT")).Count();
                    moi.ThucHienQuyI = q13.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Where(p => p.dtbn.DTBN1.Contains("BHYT")).Count();
                    moi.ThucHienQuyII = q13.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Where(p => p.dtbn.DTBN1.Contains("BHYT")).Count();
                    moi.ThucHienQuyIII = q13.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Where(p => p.dtbn.DTBN1.Contains("BHYT")).Count();
                    moi.ThucHienQuyIV = q13.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Where(p => p.dtbn.DTBN1.Contains("BHYT")).Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 13;
                    moi.DanhMuc = "Bệnh nhân khám YHCT";
                    moi.ChiTietDanhMuc = "Tỷ lệ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = 80;
                    moi.ThucHienCaNam = (q13.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Count() != 0) ? q13.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Where(p => p.dtbn.DTBN1.Contains("BHYT")).Count() / q13.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Count() * 100 : 0;
                    moi.ThucHienQuyI = (q13.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Count() != 0) ? q13.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Where(p => p.dtbn.DTBN1.Contains("BHYT")).Count() / q13.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Count() * 100 : 0;
                    moi.ThucHienQuyII = (q13.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Count() != 0) ? q13.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Where(p => p.dtbn.DTBN1.Contains("BHYT")).Count() / q13.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Count() * 100 : 0;
                    moi.ThucHienQuyIII = (q13.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Count() != 0) ? q13.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Where(p => p.dtbn.DTBN1.Contains("BHYT")).Count() / q13.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Count() * 100 : 0;
                    moi.ThucHienQuyIV = (q13.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Count() != 0) ? q13.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Where(p => p.dtbn.DTBN1.Contains("BHYT")).Count() / q13.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Count() * 100 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 13;
                    moi.DanhMuc = "Bệnh nhân khám YHCT";
                    moi.ChiTietDanhMuc = "Bệnh nhân khám nhân dân";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = 200;
                    moi.ThucHienCaNam = q13.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Where(p => p.dtbn.DTBN1.Contains("Dịch vụ")).Count();
                    moi.ThucHienQuyI = q13.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Where(p => p.dtbn.DTBN1.Contains("Dịch vụ")).Count();
                    moi.ThucHienQuyII = q13.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Where(p => p.dtbn.DTBN1.Contains("Dịch vụ")).Count();
                    moi.ThucHienQuyIII = q13.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Where(p => p.dtbn.DTBN1.Contains("Dịch vụ")).Count();
                    moi.ThucHienQuyIV = q13.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Where(p => p.dtbn.DTBN1.Contains("Dịch vụ")).Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 13;
                    moi.DanhMuc = "Bệnh nhân khám YHCT";
                    moi.ChiTietDanhMuc = "Tỷ lệ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = 20;
                    moi.ThucHienCaNam = (q13.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Count() != 0) ? q13.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Where(p => p.dtbn.DTBN1.Contains("Dịch vụ")).Count() / q13.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Count() * 100 : 0;
                    moi.ThucHienQuyI = (q13.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Count() != 0) ? q13.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Where(p => p.dtbn.DTBN1.Contains("Dịch vụ")).Count() / q13.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Count() * 100 : 0;
                    moi.ThucHienQuyII = (q13.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Count() != 0) ? q13.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Where(p => p.dtbn.DTBN1.Contains("Dịch vụ")).Count() / q13.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Count() * 100 : 0;
                    moi.ThucHienQuyIII = (q13.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Count() != 0) ? q13.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Where(p => p.dtbn.DTBN1.Contains("Dịch vụ")).Count() / q13.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Count() * 100 : 0;
                    moi.ThucHienQuyIV = (q13.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Count() != 0) ? q13.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Where(p => p.dtbn.DTBN1.Contains("Dịch vụ")).Count() / q13.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Count() * 100 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 14. Bệnh nhân điều trị ngoại trú
                    moi = new Content();
                    moi.Stt = 14;
                    moi.DanhMuc = "Bệnh nhân điều trị ngoại trú";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = 850;
                    moi.ThucHienCaNam = bnNgoaitru.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Count();
                    moi.PtramCaNam = (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyI = bnNgoaitru.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Count();
                    moi.PtramQuyI = (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyII = bnNgoaitru.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Count();
                    moi.PtramQuyII = (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIII = bnNgoaitru.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Count();
                    moi.PtramQuyIII = (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100;
                    moi.ThucHienQuyIV = bnNgoaitru.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Count();
                    moi.PtramQuyIV = (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    #endregion
                    #region 15. Tỷ lệ dùng thuốc thang
                    moi = new Content();
                    moi.Stt = 15;
                    moi.DanhMuc = "Tỷ lệ dùng thuốc thang";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = 95;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 15;
                    moi.DanhMuc = "Tỷ lệ dùng thuốc thang";
                    moi.ChiTietDanhMuc = "Tỷ lệ dùng thuốc thang sắc uống tại viện";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = 100;
                    _lContent.Add(moi);
                    #endregion
                    #region 16.Tỷ lệ điều trị kết hợp
                    moi = new Content();
                    moi.Stt = 16;
                    moi.DanhMuc = "Tỷ lệ điều trị kết hợp";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = 80;
                    _lContent.Add(moi);
                    #endregion
                    #region 17. Tỷ lệ bệnh nhân châm cứu
                    moi = new Content();
                    moi.Stt = 17;
                    moi.DanhMuc = "Tỷ lệ bệnh nhân châm cứu";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = 95;
                    _lContent.Add(moi);
                    #endregion
                    #region 18. Số bệnh nhân châm cứu
                    moi = new Content();
                    moi.Stt = 18;
                    moi.DanhMuc = "Số bệnh nhân châm cứu";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = 1000;
                    _lContent.Add(moi);
                    #endregion
                    #region 19. Thủ thuật khác
                    moi = new Content();
                    moi.Stt = 19;
                    moi.DanhMuc = "Thủ thuật khác";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "ca";
                    moi.ChiTieuCaNam = 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 20. Kiểm tra khoa phòng
                    moi = new Content();
                    moi.Stt = 20;
                    moi.DanhMuc = "Kiểm tra khoa phòng";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = 12;
                    _lContent.Add(moi);
                    #endregion
                    #region 21. Kiểm tra tuyến
                    moi = new Content();
                    moi.Stt = 21;
                    moi.DanhMuc = "Kiểm tra tuyến";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = 12;
                    _lContent.Add(moi);
                    #endregion
                    #region 22. sinh hoạt bệnh nhân
                    moi = new Content();
                    moi.Stt = 22;
                    moi.DanhMuc = "Sinh hoạt bệnh nhân";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = 24;
                    _lContent.Add(moi);
                    #endregion
                    #region 24. bài tuyên truyền
                    moi = new Content();
                    moi.Stt = 24;
                    moi.DanhMuc = "Bài tuyên truyền";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Bài";
                    moi.ChiTieuCaNam = 2;
                    _lContent.Add(moi);
                    #endregion
                    #region 25. Kỹ thuật mới
                    moi = new Content();
                    moi.Stt = 25;
                    moi.DanhMuc = "Kỹ thuật mới";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "KT";

                    _lContent.Add(moi);
                    #endregion
                    #region 26. Chiến sĩ thi đua cấp cơ sở
                    moi = new Content();
                    moi.Stt = 26;
                    moi.DanhMuc = "Chiến sĩ thi đua cấp cơ sở";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 27. Đề tài NCKH+Sáng kiến
                    moi = new Content();
                    moi.Stt = 27;
                    moi.DanhMuc = "Đề tài NCKH + Sáng kiến";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "ĐT+SK";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 28. Tập huấn chuyên môn xã
                    moi = new Content();
                    moi.Stt = 28;
                    moi.DanhMuc = "Tập huấn chuyên môn xã";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = 2;
                    _lContent.Add(moi);
                    #endregion
                    #region 29. Danh hiệu thi đua
                    moi = new Content();
                    moi.Stt = 29;
                    moi.DanhMuc = "Danh hiệu thi đua";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    _lContent.Add(moi);
                    #endregion
                }
                #endregion
                #region 23. Cá nhân đạt lao động tiên tiến
                moi = new Content();
                moi.Stt = 23;
                moi.DanhMuc = "Cá nhân đạt lao động tiên tiến";
                moi.ChiTietDanhMuc = "";
                moi.DVTinh = "%";
                moi.ChiTieuCaNam = 100;
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
                    var _qcls = (from cls in data.CLS.Where(o => o.NgayTH >= tungay && o.NgayTH <= denngay)
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
                    #endregion
                    #region 1. Tổng số cán bộ
                    moi = new Content();
                    moi.Stt = 1;
                    moi.DanhMuc = "Tổng số cán bộ";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 20 : 0;
                    moi.ThucHienCaNam = cbCDHA.Count;
                    _lContent.Add(moi);
                    #endregion
                    #region 2. Xét nghiệm
                    moi = new Content();
                    moi.Stt = 2;
                    moi.DanhMuc = "Xét nghiệm";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 344000 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 2;
                    moi.DanhMuc = "Xét nghiệm";
                    moi.ChiTietDanhMuc = "Huyết học";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 15.7 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 2;
                    moi.DanhMuc = "Xét nghiệm";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 54000 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 2;
                    moi.DanhMuc = "Xét nghiệm";
                    moi.ChiTietDanhMuc = "Sinh hóa máu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 58.2 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 2;
                    moi.DanhMuc = "Xét nghiệm";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 200000 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 2;
                    moi.DanhMuc = "Xét nghiệm";
                    moi.ChiTietDanhMuc = "Sinh hóa nước tiểu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 15.7 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 2;
                    moi.DanhMuc = "Xét nghiệm";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 54000 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 2;
                    moi.DanhMuc = "Xét nghiệm";
                    moi.ChiTietDanhMuc = "Vi sinh";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 5.8 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 2;
                    moi.DanhMuc = "Xét nghiệm";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 20000 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 2;
                    moi.DanhMuc = "Xét nghiệm";
                    moi.ChiTietDanhMuc = "NM, MC, MD";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 4.6 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 2;
                    moi.DanhMuc = "Xét nghiệm";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 16000 : 0;
                    //moi.ThucHienCaNam = qClsKhamSan.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Count();
                    //moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    //moi.ThucHienQuyI = qClsKhamSan.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Count();
                    //moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    //moi.ThucHienQuyII = qClsKhamSan.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Count();
                    //moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    //moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Count();
                    //moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    //moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Count();
                    //moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    //moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    //moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    //moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    //moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    //moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 3. siêu âm
                    moi = new Content();
                    moi.Stt = 3;
                    moi.DanhMuc = "Siêu âm";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 20000 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 4. Điện tâm đồ
                    moi = new Content();
                    moi.Stt = 4;
                    moi.DanhMuc = "Điện tâm đồ";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 10000 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 5. X-quang thường
                    moi = new Content();
                    moi.Stt = 5;
                    moi.DanhMuc = "X-quang thường";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 20000 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 6. Chụp CT - Scanner
                    moi = new Content();
                    moi.Stt = 6;
                    moi.DanhMuc = "Chụp CT - Scanner";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1200 : 0;
                    moi.ThucHienCaNam = qClsKhamSan.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == "X-Quang CT").Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == "X-Quang CT").Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == "X-Quang CT").Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == "X-Quang CT").Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == "X-Quang CT").Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 7. Xét nghiệm AFB
                    moi = new Content();
                    moi.Stt = 7;
                    moi.DanhMuc = "Xét nghiệm AFB";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 7;
                    moi.DanhMuc = "Xét nghiệm AFB";
                    moi.ChiTietDanhMuc = "Số người";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 400 : 0;
                    moi.ThucHienCaNam = q2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).GroupBy(p => p.MaBNhan).Count();
                    moi.ThucHienQuyI = q2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).GroupBy(p => p.MaBNhan).Count();
                    moi.ThucHienQuyII = q2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).GroupBy(p => p.MaBNhan).Count();
                    moi.ThucHienQuyIII = q2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).GroupBy(p => p.MaBNhan).Count();
                    moi.ThucHienQuyIV = q2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).GroupBy(p => p.MaBNhan).Count();
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 7;
                    moi.DanhMuc = "Xét nghiệm AFB";
                    moi.ChiTietDanhMuc = "Số tiêu bản XN";
                    moi.DVTinh = "T.bản";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 800 : 0;
                    moi.ThucHienCaNam = q2.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                    moi.ThucHienQuyI = q2.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                    moi.ThucHienQuyII = q2.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                    moi.ThucHienQuyIII = q2.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                    moi.ThucHienQuyIV = q2.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 7;
                    moi.DanhMuc = "Xét nghiệm AFB";
                    moi.ChiTietDanhMuc = "Số người có AFB (+)";
                    moi.DVTinh = "T.bản";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 11 : 0;
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
                    var _qcls = (from cls in data.CLS.Where(o => o.NgayTH >= tungay && o.NgayTH <= denngay)
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
                    moi = new Content();
                    moi.Stt = 1;
                    moi.DanhMuc = "Tổng số cán bộ";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 17 : 0;
                    moi.ThucHienCaNam = cbKKB.Count;
                    _lContent.Add(moi);
                    #endregion
                    #region 2. Giường khám bệnh
                    moi = new Content();
                    moi.Stt = 2;
                    moi.DanhMuc = "Giường khám bệnh";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Giường";
                    _lContent.Add(moi);
                    #endregion
                    #region 3. Tổng số BN khám tại PK
                    moi = new Content();
                    moi.Stt = 3;
                    moi.DanhMuc = "Tổng số BN khám tại PK";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 58000 : 0;
                    moi.ThucHienCaNam = _luotKham.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyI = _luotKham.Where(p => p.NgayKham >= tungayquy1 && p.NgayKham <= denngayquy1).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyII = _luotKham.Where(p => p.NgayKham >= tungayquy2 && p.NgayKham <= denngayquy2).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyIII = _luotKham.Where(p => p.NgayKham >= tungayquy3 && p.NgayKham <= denngayquy3).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyIV = _luotKham.Where(p => p.NgayKham >= tungayquy4 && p.NgayKham <= denngayquy4).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : null;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 3;
                    moi.DanhMuc = "Tổng số BN khám tại PK";
                    moi.ChiTietDanhMuc = "BN khám Nội và Nhi";
                    moi.DVTinh = "Người";
                    moi.ThucHienCaNam = _luotKham.Where(p => p.ChuyenKhoa.Contains("Nội") || p.ChuyenKhoa.Contains("Nhi"))
                                                 .Where(p => p.PLoai.Contains("Phòng khám")).Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyI = _luotKham.Where(p => p.ChuyenKhoa.Contains("Nội") || p.ChuyenKhoa.Contains("Nhi"))
                                                 .Where(p => p.PLoai.Contains("Phòng khám")).Where(p => p.NgayKham >= tungayquy1 && p.NgayKham <= denngayquy1).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyII = _luotKham.Where(p => p.ChuyenKhoa.Contains("Nội") || p.ChuyenKhoa.Contains("Nhi"))
                                                 .Where(p => p.PLoai.Contains("Phòng khám")).Where(p => p.NgayKham >= tungayquy2 && p.NgayKham <= denngayquy2).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyIII = _luotKham.Where(p => p.ChuyenKhoa.Contains("Nội") || p.ChuyenKhoa.Contains("Nhi"))
                                                 .Where(p => p.PLoai.Contains("Phòng khám")).Where(p => p.NgayKham >= tungayquy3 && p.NgayKham <= denngayquy3).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyIV = _luotKham.Where(p => p.ChuyenKhoa.Contains("Nội") || p.ChuyenKhoa.Contains("Nhi"))
                                                 .Where(p => p.PLoai.Contains("Phòng khám")).Where(p => p.NgayKham >= tungayquy4 && p.NgayKham <= denngayquy4).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : null;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 3;
                    moi.DanhMuc = "Tổng số BN khám tại PK";
                    moi.ChiTietDanhMuc = "BN khám sức khỏe";
                    moi.DVTinh = "Người";
                    moi.ThucHienCaNam = _luotKham.Where(p => p.DTBN1.Contains("KSK")).Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyI = _luotKham.Where(p => p.DTBN1.Contains("KSK")).Where(p => p.NgayKham >= tungayquy1 && p.NgayKham <= denngayquy1).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyII = _luotKham.Where(p => p.DTBN1.Contains("KSK")).Where(p => p.NgayKham >= tungayquy2 && p.NgayKham <= denngayquy2).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyIII = _luotKham.Where(p => p.DTBN1.Contains("KSK")).Where(p => p.NgayKham >= tungayquy3 && p.NgayKham <= denngayquy3).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyIV = _luotKham.Where(p => p.DTBN1.Contains("KSK")).Where(p => p.NgayKham >= tungayquy4 && p.NgayKham <= denngayquy4).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : null;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 4. Số BN vào điều trị nội trú
                    moi = new Content();
                    moi.Stt = 4;
                    moi.DanhMuc = "Tổng số BN điều trị nội trú";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 15638 : 0;
                    moi.ThucHienCaNam = bnNoiTru.Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyI = bnNoiTru.Where(p => p.NgayVao >= tungayquy1 && p.NgayVao <= denngayquy1).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyII = bnNoiTru.Where(p => p.NgayVao >= tungayquy2 && p.NgayVao <= denngayquy2).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyIII = bnNoiTru.Where(p => p.NgayVao >= tungayquy3 && p.NgayVao <= denngayquy3).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyIV = bnNoiTru.Where(p => p.NgayVao >= tungayquy4 && p.NgayVao <= denngayquy4).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : null;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 4;
                    moi.DanhMuc = "Tổng số BN điều trị nội trú";
                    moi.ChiTietDanhMuc = "Nội";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3346 : 0;
                    moi.ThucHienCaNam = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Nội")).Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyI = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Nội")).Where(p => p.NgayVao >= tungayquy1 && p.NgayVao <= denngayquy1).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyII = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Nội")).Where(p => p.NgayVao >= tungayquy2 && p.NgayVao <= denngayquy2).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyIII = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Nội")).Where(p => p.NgayVao >= tungayquy3 && p.NgayVao <= denngayquy3).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyIV = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Nội")).Where(p => p.NgayVao >= tungayquy4 && p.NgayVao <= denngayquy4).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : null;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 4;
                    moi.DanhMuc = "Tổng số BN điều trị nội trú";
                    moi.ChiTietDanhMuc = "Nhi";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1156 : 0;
                    moi.ThucHienCaNam = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Nhi")).Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyI = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Nhi")).Where(p => p.NgayVao >= tungayquy1 && p.NgayVao <= denngayquy1).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyII = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Nhi")).Where(p => p.NgayVao >= tungayquy2 && p.NgayVao <= denngayquy2).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyIII = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Nhi")).Where(p => p.NgayVao >= tungayquy3 && p.NgayVao <= denngayquy3).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyIV = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Nhi")).Where(p => p.NgayVao >= tungayquy4 && p.NgayVao <= denngayquy4).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : null;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 4;
                    moi.DanhMuc = "Tổng số BN điều trị nội trú";
                    moi.ChiTietDanhMuc = "Truyền nhiễm";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 590 : 0;
                    moi.ThucHienCaNam = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Truyền nhiễm")).Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyI = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Truyền nhiễm")).Where(p => p.NgayVao >= tungayquy1 && p.NgayVao <= denngayquy1).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyII = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Truyền nhiễm")).Where(p => p.NgayVao >= tungayquy2 && p.NgayVao <= denngayquy2).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyIII = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Truyền nhiễm")).Where(p => p.NgayVao >= tungayquy3 && p.NgayVao <= denngayquy3).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyIV = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Truyền nhiễm")).Where(p => p.NgayVao >= tungayquy4 && p.NgayVao <= denngayquy4).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : null;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 4;
                    moi.DanhMuc = "Tổng số BN điều trị nội trú";
                    moi.ChiTietDanhMuc = "Hồi sức cấp cứu";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2075 : 0;
                    moi.ThucHienCaNam = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Hồi sức cấp cứu")).Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyI = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Hồi sức cấp cứu")).Where(p => p.NgayVao >= tungayquy1 && p.NgayVao <= denngayquy1).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyII = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Hồi sức cấp cứu")).Where(p => p.NgayVao >= tungayquy2 && p.NgayVao <= denngayquy2).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyIII = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Hồi sức cấp cứu")).Where(p => p.NgayVao >= tungayquy3 && p.NgayVao <= denngayquy3).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyIV = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Hồi sức cấp cứu")).Where(p => p.NgayVao >= tungayquy4 && p.NgayVao <= denngayquy4).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : null;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 4;
                    moi.DanhMuc = "Tổng số BN điều trị nội trú";
                    moi.ChiTietDanhMuc = "Ngoại";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3212 : 0;
                    moi.ThucHienCaNam = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Ngoại")).Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyI = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Ngoại")).Where(p => p.NgayVao >= tungayquy1 && p.NgayVao <= denngayquy1).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyII = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Ngoại")).Where(p => p.NgayVao >= tungayquy2 && p.NgayVao <= denngayquy2).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyIII = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Ngoại")).Where(p => p.NgayVao >= tungayquy3 && p.NgayVao <= denngayquy3).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyIV = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Ngoại")).Where(p => p.NgayVao >= tungayquy4 && p.NgayVao <= denngayquy4).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : null;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 4;
                    moi.DanhMuc = "Tổng số BN điều trị nội trú";
                    moi.ChiTietDanhMuc = "Liên chuyên khoa";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1500 : 0;
                    moi.ThucHienCaNam = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt") || p.ChuyenKhoa.Contains("Tai Mũi Họng") || p.ChuyenKhoa.Contains("Mắt"))
                                                .Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyI = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt") || p.ChuyenKhoa.Contains("Tai Mũi Họng") || p.ChuyenKhoa.Contains("Mắt"))
                                               .Where(p => p.NgayVao >= tungayquy1 && p.NgayVao <= denngayquy1).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyII = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt") || p.ChuyenKhoa.Contains("Tai Mũi Họng") || p.ChuyenKhoa.Contains("Mắt"))
                                                .Where(p => p.NgayVao >= tungayquy2 && p.NgayVao <= denngayquy2).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyIII = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt") || p.ChuyenKhoa.Contains("Tai Mũi Họng") || p.ChuyenKhoa.Contains("Mắt"))
                                                .Where(p => p.NgayVao >= tungayquy3 && p.NgayVao <= denngayquy3).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyIV = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt") || p.ChuyenKhoa.Contains("Tai Mũi Họng") || p.ChuyenKhoa.Contains("Mắt"))
                                                .Where(p => p.NgayVao >= tungayquy4 && p.NgayVao <= denngayquy4).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : null;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 4;
                    moi.DanhMuc = "Tổng số BN điều trị nội trú";
                    moi.ChiTietDanhMuc = "Sản";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3139 : 0;
                    moi.ThucHienCaNam = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Sản")).Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyI = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Sản")).Where(p => p.NgayVao >= tungayquy1 && p.NgayVao <= denngayquy1).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyII = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Sản")).Where(p => p.NgayVao >= tungayquy2 && p.NgayVao <= denngayquy2).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyIII = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Sản")).Where(p => p.NgayVao >= tungayquy3 && p.NgayVao <= denngayquy3).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyIV = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Sản")).Where(p => p.NgayVao >= tungayquy4 && p.NgayVao <= denngayquy4).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : null;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 4;
                    moi.DanhMuc = "Tổng số BN điều trị nội trú";
                    moi.ChiTietDanhMuc = "YHCT-PHCN";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 620 : 0;
                    moi.ThucHienCaNam = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Đông y") || p.TenKP.Contains("Khoa YHCT"))
                                                .Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyI = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Đông y") || p.TenKP.Contains("Khoa YHCT"))
                                               .Where(p => p.NgayVao >= tungayquy1 && p.NgayVao <= denngayquy1).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyII = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Đông y") || p.TenKP.Contains("Khoa YHCT"))
                                                .Where(p => p.NgayVao >= tungayquy2 && p.NgayVao <= denngayquy2).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyIII = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Đông y") || p.TenKP.Contains("Khoa YHCT"))
                                                .Where(p => p.NgayVao >= tungayquy3 && p.NgayVao <= denngayquy3).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyIV = bnNoiTru.Where(p => p.TenKP.Contains("Khoa Đông y") || p.TenKP.Contains("Khoa YHCT"))
                                                .Where(p => p.NgayVao >= tungayquy4 && p.NgayVao <= denngayquy4).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : null;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 5. Bệnh nhân chuyển viện
                    var bnChuyenVien = (from k in _kkb
                                        join rv in data.RaViens on k.MaKP equals rv.MaKP
                                        join bn in data.BenhNhans on rv.MaBNhan equals bn.MaBNhan
                                        select new { bn, rv }).Where(p => p.rv.MaBVC != null).ToList();
                    moi = new Content();
                    moi.Stt = 5;
                    moi.DanhMuc = "BN chuyển viện PK";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2610 : 0;
                    moi.ThucHienCaNam = bnChuyenVien.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyI = bnChuyenVien.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyII = bnChuyenVien.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyIII = bnChuyenVien.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyIV = bnChuyenVien.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : null;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 6. Cận lâm sàng tại phòng khám
                    moi = new Content();
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "Huyết học";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 34.5 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 20000 : 0;
                    moi.ThucHienCaNam = qClsKhamSan_KB.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "Sinh hóa máu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 259 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 150000 : 0;
                    moi.ThucHienCaNam = qClsKhamSan_KB.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "Sinh hóa nước tiểu";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 34.5 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 20000 : 0;
                    moi.ThucHienCaNam = qClsKhamSan_KB.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "Siêu âm";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 20 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 11600 : 0;
                    moi.ThucHienCaNam = qClsKhamSan_KB.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "Điện tâm đồ";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 12 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 7000 : 0;
                    moi.ThucHienCaNam = qClsKhamSan_KB.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "X-quang";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 19 : 0;
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "- Tổng số";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 11000 : 0;
                    moi.ThucHienCaNam = qClsKhamSan_KB.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "Bệnh nhân điều trị ngoại trú";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1500 : 0;
                    moi.ThucHienCaNam = bnNgoaiTru.Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyI = bnNgoaiTru.Where(p => p.NgayVao >= tungayquy1 && p.NgayVao <= denngayquy1).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyII = bnNgoaiTru.Where(p => p.NgayVao >= tungayquy2 && p.NgayVao <= denngayquy2).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyIII = bnNgoaiTru.Where(p => p.NgayVao >= tungayquy3 && p.NgayVao <= denngayquy3).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : null;
                    moi.ThucHienQuyIV = bnNgoaiTru.Where(p => p.NgayVao >= tungayquy4 && p.NgayVao <= denngayquy4).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : null;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "Bệnh nhân đo loãng xương";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2900 : 0;
                    moi.ThucHienCaNam = qClsKhamSan_KB.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "Bệnh nhân đo chức năng hô hấp";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1740 : 0;
                    moi.ThucHienCaNam = qClsKhamSan_KB.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 6;
                    moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                    moi.ChiTietDanhMuc = "Bệnh nhân nội soi dạ dày";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1000 : 0;
                    moi.ThucHienCaNam = qClsKhamSan_KB.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi).Count();
                    moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyI = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi).Count();
                    moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyII = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi).Count();
                    moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIII = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi).Count();
                    moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                    moi.ThucHienQuyIV = qClsKhamSan_KB.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4 && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi).Count();
                    moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                    moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                    moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                    moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                    moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                    moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                    _lContent.Add(moi);
                    #endregion
                    #region 7. Xét nghiệm AFB
                    moi = new Content();
                    moi.Stt = 7;
                    moi.DanhMuc = "Xét nghiệm AFB";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 7;
                    moi.DanhMuc = "Xét nghiệm AFB";
                    moi.ChiTietDanhMuc = "Số người";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 200 : 0;
                    moi.ThucHienCaNam = q2_kb.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).GroupBy(p => p.MaBNhan).Count();
                    moi.ThucHienQuyI = q2_kb.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).GroupBy(p => p.MaBNhan).Count();
                    moi.ThucHienQuyII = q2_kb.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).GroupBy(p => p.MaBNhan).Count();
                    moi.ThucHienQuyIII = q2_kb.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).GroupBy(p => p.MaBNhan).Count();
                    moi.ThucHienQuyIV = q2_kb.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).GroupBy(p => p.MaBNhan).Count();
                    _lContent.Add(moi);

                    moi = new Content();
                    moi.Stt = 7;
                    moi.DanhMuc = "Xét nghiệm AFB";
                    moi.ChiTietDanhMuc = "Số tiêu bản XN";
                    moi.DVTinh = "T.bản";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 400 : 0;
                    moi.ThucHienCaNam = q2_kb.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                    moi.ThucHienQuyI = q2_kb.Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                    moi.ThucHienQuyII = q2_kb.Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                    moi.ThucHienQuyIII = q2_kb.Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                    moi.ThucHienQuyIV = q2_kb.Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
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
                    moi = new Content();
                    moi.Stt = 1;
                    moi.DanhMuc = "Tổng số cán bộ";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 10 : 0;
                    moi.ThucHienCaNam = cbKD.Count;
                    _lContent.Add(moi);
                    #endregion
                    #region 2. Kiểm tra dược tới các khoa
                    moi = new Content();
                    moi.Stt = 2;
                    moi.DanhMuc = "Kiểm tra dược tới các khoa";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 12 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 3. Thu vỏ lọ gây nghiện - HTT
                    moi = new Content();
                    moi.Stt = 3;
                    moi.DanhMuc = "Thu vỏ lọ gây nghiện - HTT";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 100 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 4. Thông báo thuốc
                    moi = new Content();
                    moi.Stt = 4;
                    moi.DanhMuc = "Thông báo thuốc";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 12 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 5. Cung ứng đầy đủ thuốc, VTTH, HC
                    moi = new Content();
                    moi.Stt = 5;
                    moi.DanhMuc = "Cung ứng đầy đủ thuốc, VTTH, HC";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 100 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 6. Cấp phát đảm bảo chế độ nguyên tắc
                    moi = new Content();
                    moi.Stt = 6;
                    moi.DanhMuc = "Cấp phát đảm bảo chế độ nguyên tắc";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 100 : 0;
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
                    var qCLS_KB = (from kp in _klck
                                   join cls in data.CLS on kp.MaKP equals cls.MaKP
                                   select new { cls.IdCLS, cls.NgayTH, cls.MaBNhan, cls.MaKP, kp.TenKP }).ToList();
                    var qCLS_KB1 = (from cd in data.ChiDinhs
                                    join dv in data.DichVus on cd.MaDV equals dv.MaDV
                                    join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                    join dtct in data.DThuoccts on cd.IDCD equals dtct.IDCD
                                    select new { cd.Status, cd.IdCLS, tn.TenRG, dv.Loai, dtct.IDCD, dv.TenDV })
                                            .Where(p => p.Status == 1 && p.IDCD != null).ToList();
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
                    if (!chkMauMoi.Checked)
                    {
                        #region 1. Tổng số CBCNV
                        moi = new Content();
                        moi.Stt = 1;
                        moi.DanhMuc = "Tổng số cán bộ";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "Người";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 14 : 0;
                        moi.ThucHienCaNam = cbKLCK.Count;
                        _lContent.Add(moi);
                        #endregion
                        #region 2. Giường khám bệnh
                        moi = new Content();
                        moi.Stt = 2;
                        moi.DanhMuc = "Giường khám bệnh";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "Giường";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 15 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 2;
                        moi.DanhMuc = "Giường khám bệnh";
                        moi.ChiTietDanhMuc = "Tai mũi họng";
                        moi.DVTinh = "Giường";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 5 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 2;
                        moi.DanhMuc = "Giường khám bệnh";
                        moi.ChiTietDanhMuc = "Mắt";
                        moi.DVTinh = "Giường";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 5 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 2;
                        moi.DanhMuc = "Giường khám bệnh";
                        moi.ChiTietDanhMuc = "Răng hàm mặt";
                        moi.DVTinh = "Giường";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 5 : 0;
                        _lContent.Add(moi);
                        #endregion
                        #region 3. Tổng số BN khám tại PK
                        moi = new Content();
                        moi.Stt = 3;
                        moi.DanhMuc = "Tổng số BN khám tại PK";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "Người";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3750 : 0;
                        _lContent.Add(moi);
                        //if (ck == 3 || ck == 1)
                        //{
                        moi = new Content();
                        moi.Stt = 3;
                        moi.DanhMuc = "Tổng số BN khám tại PK";
                        moi.ChiTietDanhMuc = "Tai mũi họng";
                        moi.DVTinh = "Người";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2000 : 0;
                        moi.ThucHienCaNam = _luotKham.Where(p => p.ChuyenKhoa.Contains("Tai Mũi Họng")).Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay).Count();
                        moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyI = _luotKham.Where(p => p.ChuyenKhoa.Contains("Tai Mũi Họng")).Where(p => p.NgayKham >= tungayquy1 && p.NgayKham <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyII = _luotKham.Where(p => p.ChuyenKhoa.Contains("Tai Mũi Họng")).Where(p => p.NgayKham >= tungayquy2 && p.NgayKham <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIII = _luotKham.Where(p => p.ChuyenKhoa.Contains("Tai Mũi Họng")).Where(p => p.NgayKham >= tungayquy3 && p.NgayKham <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIV = _luotKham.Where(p => p.ChuyenKhoa.Contains("Tai Mũi Họng")).Where(p => p.NgayKham >= tungayquy4 && p.NgayKham <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : null;
                        moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);
                        //}
                        //if (ck == 3 || ck == 2)
                        //{
                        moi = new Content();
                        moi.Stt = 3;
                        moi.DanhMuc = "Tổng số BN khám tại PK";
                        moi.ChiTietDanhMuc = "Mắt";
                        moi.DVTinh = "Người";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1000 : 0;
                        moi.ThucHienCaNam = _luotKham.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay).Count();
                        moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyI = _luotKham.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.NgayKham >= tungayquy1 && p.NgayKham <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyII = _luotKham.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.NgayKham >= tungayquy2 && p.NgayKham <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIII = _luotKham.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.NgayKham >= tungayquy3 && p.NgayKham <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIV = _luotKham.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.NgayKham >= tungayquy4 && p.NgayKham <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : null;
                        moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);
                        //}
                        //if (ck == 3 || ck == 0)
                        //{
                        moi = new Content();
                        moi.Stt = 3;
                        moi.DanhMuc = "Tổng số BN khám tại PK";
                        moi.ChiTietDanhMuc = "Răng hàm mặt";
                        moi.DVTinh = "Người";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 750 : 0;
                        moi.ThucHienCaNam = _luotKham.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay).Count();
                        moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyI = _luotKham.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.NgayKham >= tungayquy1 && p.NgayKham <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyII = _luotKham.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.NgayKham >= tungayquy2 && p.NgayKham <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIII = _luotKham.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.NgayKham >= tungayquy3 && p.NgayKham <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIV = _luotKham.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.NgayKham >= tungayquy4 && p.NgayKham <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : null;
                        moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);
                        //}

                        moi = new Content();
                        moi.Stt = 3;
                        moi.DanhMuc = "Tổng số BN khám tại PK";
                        moi.ChiTietDanhMuc = "- Các thủ thuật về răng";
                        moi.DVTinh = "Người";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 150 : 0;
                        moi.ThucHienCaNam = qClsKhamSan_PK.Where(p => p.TenRG == "Thủ thuật").Where(p => p.TenDV.Contains("răng"))
                                                          .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyI = qClsKhamSan_PK.Where(p => p.TenRG == "Thủ thuật").Where(p => p.TenDV.Contains("răng"))
                                                         .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyII = qClsKhamSan_PK.Where(p => p.TenRG == "Thủ thuật").Where(p => p.TenDV.Contains("răng"))
                                                          .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIII = qClsKhamSan_PK.Where(p => p.TenRG == "Thủ thuật").Where(p => p.TenDV.Contains("răng"))
                                                           .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenRG == "Thủ thuật").Where(p => p.TenDV.Contains("răng"))
                                                          .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : null;
                        moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);
                        #endregion
                        #region 4. Cận lâm sàng tại phòng khám
                        moi = new Content();
                        moi.Stt = 4;
                        moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "Người";
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 4;
                        moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                        moi.ChiTietDanhMuc = "Huyết học";
                        moi.DVTinh = "Lần";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 375 : 0;
                        moi.ThucHienCaNam = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                                                          .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyI = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                                                          .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyII = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                                                          .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIII = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                                                          .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                                                          .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : null;
                        moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 4;
                        moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                        moi.ChiTietDanhMuc = "- Tỷ lệ";
                        moi.DVTinh = "%";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 10 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 4;
                        moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                        moi.ChiTietDanhMuc = "Sinh hóa nước tiểu";
                        moi.DVTinh = "Lần";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 250 : 0;
                        moi.ThucHienCaNam = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)
                                                          .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyI = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)
                                                          .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyII = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)
                                                          .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIII = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)
                                                          .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)
                                                          .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : null;
                        moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 4;
                        moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                        moi.ChiTietDanhMuc = "- Tỷ lệ";
                        moi.DVTinh = "%";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 6.67 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 4;
                        moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                        moi.ChiTietDanhMuc = "Sinh hóa máu";
                        moi.DVTinh = "Lần";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 475 : 0;
                        moi.ThucHienCaNam = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                                                          .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyI = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                                                          .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyII = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                                                          .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIII = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                                                          .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                                                          .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : null;
                        moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 4;
                        moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                        moi.ChiTietDanhMuc = "- Tỷ lệ";
                        moi.DVTinh = "%";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 13 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 4;
                        moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                        moi.ChiTietDanhMuc = "X-quang";
                        moi.DVTinh = "Lần";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 238 : 0;
                        moi.ThucHienCaNam = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)
                                                          .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyI = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)
                                                          .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyII = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)
                                                          .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIII = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)
                                                          .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)
                                                          .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : null;
                        moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 4;
                        moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                        moi.ChiTietDanhMuc = "- Tỷ lệ";
                        moi.DVTinh = "%";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 6 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 4;
                        moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                        moi.ChiTietDanhMuc = "Siêu âm";
                        moi.DVTinh = "Lần";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 238 : 0;
                        moi.ThucHienCaNam = qClsKhamSan_PK.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm))
                                                          .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyI = qClsKhamSan_PK.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm))
                                                          .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyII = qClsKhamSan_PK.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm))
                                                          .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIII = qClsKhamSan_PK.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm))
                                                          .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm))
                                                          .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : null;
                        moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 4;
                        moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                        moi.ChiTietDanhMuc = "- Tỷ lệ";
                        moi.DVTinh = "%";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 6 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 4;
                        moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                        moi.ChiTietDanhMuc = "Nội soi T-M-H";
                        moi.DVTinh = "Lần";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1750 : 0;
                        moi.ThucHienCaNam = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong)
                                                          .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyI = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong)
                                                          .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyII = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong)
                                                          .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIII = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong)
                                                          .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong)
                                                          .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : null;
                        moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 4;
                        moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                        moi.ChiTietDanhMuc = "- Tỷ lệ";
                        moi.DVTinh = "%";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 47 : 0;
                        _lContent.Add(moi);
                        #endregion
                        #region 5. Ngày điều trị trung bình
                        moi = new Content();
                        moi.Stt = 5;
                        moi.DanhMuc = "Ngày điều trị trung bình";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "Ngày";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 4 : 0;
                        moi.ThucHienCaNam = (bnRV.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Count() != 0) ? (int)bnRV.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Sum(p => p.rv.SoNgaydt) / bnRV.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Count() : 0;
                        moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyI = (bnRV.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Count() != 0) ? (int)bnRV.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Sum(p => p.rv.SoNgaydt) / bnRV.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Count() : 0;
                        moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyII = (bnRV.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Count() != 0) ? (int)bnRV.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Sum(p => p.rv.SoNgaydt) / bnRV.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Count() : 0;
                        moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyIII = (bnRV.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Count() != 0) ? (int)bnRV.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Sum(p => p.rv.SoNgaydt) / bnRV.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Count() : 0;
                        moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyIV = (bnRV.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Count() != 0) ? (int)bnRV.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Sum(p => p.rv.SoNgaydt) / bnRV.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Count() : 0;
                        moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                        moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);
                        #endregion
                        #region 6.Tổng số ngày điều trị
                        moi = new Content();
                        moi.Stt = 6;
                        moi.DanhMuc = "Tổng số ngày điều trị";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "Ngày";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 5475 : 0;
                        moi.ThucHienCaNam = bnRV.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Sum(p => p.rv.SoNgaydt);
                        moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyI = bnRV.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Sum(p => p.rv.SoNgaydt);
                        moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyII = bnRV.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Sum(p => p.rv.SoNgaydt);
                        moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyIII = bnRV.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Sum(p => p.rv.SoNgaydt);
                        moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyIV = bnRV.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Sum(p => p.rv.SoNgaydt);
                        moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                        moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);
                        #endregion
                        #region 7. Ngày sử dụng giường/tháng
                        moi = new Content();
                        moi.Stt = 7;
                        moi.DanhMuc = "Ngày sử dụng giường/tháng";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "Ngày";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 30 : 0;
                        _lContent.Add(moi);
                        #endregion
                        #region 8. Công suất sử dụng giường bệnh
                        moi = new Content();
                        moi.Stt = 8;
                        moi.DanhMuc = "Công suất sử dụng giường bệnh";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "%";
                        moi.ChiTieuCaNam = 100;
                        _lContent.Add(moi);
                        #endregion
                        #region 9. Tai biến trong điều trị
                        moi = new Content();
                        moi.Stt = 9;
                        moi.DanhMuc = "Tai biến trong điều trị";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "Ca";
                        moi.ChiTieuCaNam = 0;
                        _lContent.Add(moi);
                        #endregion
                        #region 10. Bệnh nhân điều trị nội trú
                        moi = new Content();
                        moi.Stt = 10;
                        moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "Người";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 375 : 0; ;
                        moi.ThucHienCaNam = bnNoiTru.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Count();
                        moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyI = bnNoiTru.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyII = bnNoiTru.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyIII = bnNoiTru.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyIV = bnNoiTru.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                        moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);
                        //if (ck == 3 || ck == 1)
                        //{
                        moi = new Content();
                        moi.Stt = 10;
                        moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                        moi.ChiTietDanhMuc = "Tai mũi họng";
                        moi.DVTinh = "Người";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 175 : 0; ;
                        moi.ThucHienCaNam = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Tai Mũi Họng")).Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Count();
                        moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyI = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Tai Mũi Họng")).Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyII = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Tai Mũi Họng")).Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyIII = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Tai Mũi Họng")).Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyIV = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Tai Mũi Họng")).Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                        moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);
                        //}
                        //if (ck == 3 || ck == 0)
                        //{
                        moi = new Content();
                        moi.Stt = 10;
                        moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                        moi.ChiTietDanhMuc = "Răng hàm mặt";
                        moi.DVTinh = "Người";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 175 : 0; ;
                        moi.ThucHienCaNam = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Count();
                        moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyI = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyII = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyIII = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyIV = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                        moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);
                        //}
                        //if (ck == 3 || ck == 2)
                        //{
                        moi = new Content();
                        moi.Stt = 10;
                        moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                        moi.ChiTietDanhMuc = "Mắt";
                        moi.DVTinh = "Người";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 175 : 0; ;
                        moi.ThucHienCaNam = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Count();
                        moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyI = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyII = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyIII = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyIV = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                        moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);
                        //}
                        #endregion
                        #region 10. BN khỏi
                        moi = new Content();
                        moi.Stt = 10;
                        moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                        moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Khỏi";
                        moi.DVTinh = "%";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 80 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 10;
                        moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                        moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                        moi.DVTinh = "Người";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 300 : 0;
                        moi.ThucHienCaNam = bnNoiTru.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua == "Khỏi").Count();
                        moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyI = bnNoiTru.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.KetQua == "Khỏi").Count();
                        moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyII = bnNoiTru.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.KetQua == "Khỏi").Count();
                        moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyIII = bnNoiTru.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.KetQua == "Khỏi").Count();
                        moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyIV = bnNoiTru.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Khỏi").Count();
                        moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                        moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);
                        #endregion
                        #region 10. BN đỡ
                        moi = new Content();
                        moi.Stt = 10;
                        moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                        moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Đỡ";
                        moi.DVTinh = "%";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 17 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 10;
                        moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                        moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                        moi.DVTinh = "Người";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 64 : 0;
                        moi.ThucHienCaNam = bnNoiTru.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                        moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyI = bnNoiTru.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                        moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyII = bnNoiTru.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                        moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyIII = bnNoiTru.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                        moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyIV = bnNoiTru.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                        moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                        moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);
                        #endregion
                        #region 10. BN chuyển viện
                        moi = new Content();
                        moi.Stt = 10;
                        moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                        moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân Chuyển viện";
                        moi.DVTinh = "%";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 10;
                        moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                        moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                        moi.DVTinh = "Người";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 8 : 0;
                        moi.ThucHienCaNam = bnNoiTru.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.MaBVC != null).Count();
                        moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyI = bnNoiTru.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.MaBVC != null).Count();
                        moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyII = bnNoiTru.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.MaBVC != null).Count();
                        moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyIII = bnNoiTru.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.MaBVC != null).Count();
                        moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyIV = bnNoiTru.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.MaBVC != null).Count();
                        moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                        moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);
                        #endregion
                        #region 10. BN xin về
                        moi = new Content();
                        moi.Stt = 10;
                        moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                        moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân nặng hơn xin về";
                        moi.DVTinh = "%";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 10;
                        moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                        moi.ChiTietDanhMuc = "- Tổng số bệnh nhân";
                        moi.DVTinh = "Người";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 3 : 0;
                        moi.ThucHienCaNam = bnNoiTru.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua == "Nặng hơn").Count();
                        moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyI = bnNoiTru.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.KetQua == "Nặng hơn").Count();
                        moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyII = bnNoiTru.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.KetQua == "Nặng hơn").Count();
                        moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyIII = bnNoiTru.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.KetQua == "Nặng hơn").Count();
                        moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyIV = bnNoiTru.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Nặng hơn").Count();
                        moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                        moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);
                        #endregion
                        #region 10. BN tử vong
                        moi = new Content();
                        moi.Stt = 10;
                        moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                        moi.ChiTietDanhMuc = "Tỷ lệ bệnh nhân tử vong";
                        moi.DVTinh = "%";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 0 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 10;
                        moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                        moi.ChiTietDanhMuc = "- Tổng số";
                        moi.DVTinh = "Người";
                        moi.ChiTieuCaNam = 0;
                        moi.ThucHienCaNam = bnNoiTru.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua == "Tử vong").Count();
                        moi.PtramCaNam = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyI = bnNoiTru.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.KetQua == "Tử vong").Count();
                        moi.PtramQuyI = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyII = bnNoiTru.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.KetQua == "Tử vong").Count();
                        moi.PtramQuyII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyIII = bnNoiTru.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.KetQua == "Tử vong").Count();
                        moi.PtramQuyIII = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : 0;
                        moi.ThucHienQuyIV = bnNoiTru.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Tử vong").Count();
                        moi.PtramQuyIV = (moi.ChiTieuCaNam != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : 0;
                        moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);
                        #endregion
                        #region 11. Cận lâm sàng tại bệnh phòng
                        var qCls_Noitru = (from a in bnNoiTru
                                           join b in qCLS_KB on a.MaBNhan equals b.MaBNhan
                                           join c in qCLS_KB1 on b.IdCLS equals c.IdCLS
                                           select new { b.IdCLS, b.NgayTH, b.MaBNhan, b.MaKP, b.TenKP, c.Status, c.TenRG, c.Loai, c.IDCD, c.TenDV }).ToList();
                        moi = new Content();
                        moi.Stt = 11;
                        moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "Người";
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 11;
                        moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                        moi.ChiTietDanhMuc = "Truyền máu";
                        moi.DVTinh = "ui";
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 11;
                        moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                        moi.ChiTietDanhMuc = "Huyết học";
                        moi.DVTinh = "Lần";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 375 : 0;
                        moi.ThucHienCaNam = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                                                          .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyI = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                                                          .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyII = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                                                          .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIII = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                                                          .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIV = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                                                          .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : null;
                        moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 11;
                        moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                        moi.ChiTietDanhMuc = "- Tỷ lệ";
                        moi.DVTinh = "%";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 100 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 11;
                        moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                        moi.ChiTietDanhMuc = "Sinh hóa nước tiểu";
                        moi.DVTinh = "Lần";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2500 : 0;
                        moi.ThucHienCaNam = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)
                                                          .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyI = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)
                                                          .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyII = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)
                                                          .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIII = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)
                                                          .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIV = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)
                                                          .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : null;
                        moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 11;
                        moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                        moi.ChiTietDanhMuc = "- Tỷ lệ";
                        moi.DVTinh = "%";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 667 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 11;
                        moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                        moi.ChiTietDanhMuc = "Sinh hóa máu";
                        moi.DVTinh = "Lần";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 2500 : 0;
                        moi.ThucHienCaNam = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                                                          .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyI = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                                                          .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyII = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                                                          .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIII = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                                                          .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIV = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                                                          .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : null;
                        moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 11;
                        moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                        moi.ChiTietDanhMuc = "- Tỷ lệ";
                        moi.DVTinh = "%";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 667 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 11;
                        moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                        moi.ChiTietDanhMuc = "X-quang";
                        moi.DVTinh = "Lần";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 55 : 0;
                        moi.ThucHienCaNam = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)
                                                          .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyI = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)
                                                          .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyII = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)
                                                          .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIII = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)
                                                          .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIV = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)
                                                          .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : null;
                        moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 11;
                        moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                        moi.ChiTietDanhMuc = "- Tỷ lệ";
                        moi.DVTinh = "%";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 15 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 11;
                        moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                        moi.ChiTietDanhMuc = "Siêu âm";
                        moi.DVTinh = "Lần";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 112 : 0;
                        moi.ThucHienCaNam = qCls_Noitru.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm))
                                                          .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyI = qCls_Noitru.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm))
                                                          .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyII = qCls_Noitru.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm))
                                                          .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIII = qCls_Noitru.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm))
                                                          .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIV = qCls_Noitru.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm))
                                                          .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : null;
                        moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 11;
                        moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                        moi.ChiTietDanhMuc = "- Tỷ lệ";
                        moi.DVTinh = "%";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 30 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 11;
                        moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                        moi.ChiTietDanhMuc = "Điện tim";
                        moi.DVTinh = "Lần";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1750 : 0;
                        moi.ThucHienCaNam = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)
                                                          .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.PtramCaNam = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienCaNam / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyI = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)
                                                          .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyII = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)
                                                          .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIII = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)
                                                          .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuCaNam * 100 : null;
                        moi.ThucHienQuyIV = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)
                                                          .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuCaNam != 0 || moi.ChiTieuCaNam != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuCaNam * 100 : null;
                        moi.PtramCaNam = Math.Round(moi.PtramCaNam.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 11;
                        moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                        moi.ChiTietDanhMuc = "- Tỷ lệ";
                        moi.DVTinh = "%";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 35 : 0;
                        _lContent.Add(moi);
                        #endregion
                        #region 12. Phẫu thuật
                        moi = new Content();
                        moi.Stt = 12;
                        moi.DanhMuc = "Phẫu thuật Mắt";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "Ca";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 113 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 12;
                        moi.DanhMuc = "Phẫu thuật Mắt";
                        moi.ChiTietDanhMuc = "Mổ Phaco";
                        moi.DVTinh = "Ca";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 88 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 12;
                        moi.DanhMuc = "Phẫu thuật Mắt";
                        moi.ChiTietDanhMuc = "Mổ mắt khác";
                        moi.DVTinh = "Ca";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 25 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 12;
                        moi.DanhMuc = "Phẫu thuật";
                        moi.ChiTietDanhMuc = "+ Loại I";
                        moi.DVTinh = "Ca";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 5 : 0;
                        moi.ThucHienCaNam = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.Loai == 1).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.ThucHienQuyI = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.Loai == 1).Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.ThucHienQuyII = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.Loai == 1).Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.ThucHienQuyIII = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.Loai == 1).Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.Loai == 1).Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 12;
                        moi.DanhMuc = "Phẫu thuật";
                        moi.ChiTietDanhMuc = "+ Loại II";
                        moi.DVTinh = "Ca";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 19 : 0;
                        moi.ThucHienCaNam = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.Loai == 2).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.ThucHienQuyI = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.Loai == 2).Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.ThucHienQuyII = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.Loai == 2).Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.ThucHienQuyIII = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.Loai == 2).Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.Loai == 2).Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 12;
                        moi.DanhMuc = "Phẫu thuật";
                        moi.ChiTietDanhMuc = "+ Loại III";
                        moi.DVTinh = "Ca";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 1 : 0;
                        moi.ThucHienCaNam = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.Loai == 3).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.ThucHienQuyI = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.Loai == 3).Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.ThucHienQuyII = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.Loai == 3).Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.ThucHienQuyIII = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.Loai == 3).Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.Loai == 3).Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        _lContent.Add(moi);
                        #endregion
                        #region 13. Tổng tiểu phẫu
                        moi = new Content();
                        moi.Stt = 13;
                        moi.DanhMuc = "Phẫu thuật Răng hàm mặt";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "Ca";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 8 : 0;
                        moi.ThucHienCaNam = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.ThucHienQuyI = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.ThucHienQuyII = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.ThucHienQuyIII = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 13;
                        moi.DanhMuc = "Phẫu thuật Răng hàm mặt";
                        moi.ChiTietDanhMuc = "Tổng tiểu phẫu";
                        moi.DVTinh = "Ca";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 25 : 0;
                        moi.ThucHienCaNam = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.TenRG.Contains("Thủ thuật"))
                                            .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.ThucHienQuyI = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.TenRG.Contains("Thủ thuật"))
                                            .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.ThucHienQuyII = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.TenRG.Contains("Thủ thuật"))
                                            .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.ThucHienQuyIII = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.TenRG.Contains("Thủ thuật"))
                                            .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.TenRG.Contains("Thủ thuật"))
                                            .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 13;
                        moi.DanhMuc = "Phẫu thuật Răng hàm mặt";
                        moi.ChiTietDanhMuc = "Cắt Amidal";
                        moi.DVTinh = "Ca";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 25 : 0;
                        moi.ThucHienCaNam = qClsKhamSan_PK.Where(p => p.TenDV.ToLower().Contains("cắt a"))
                                            .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.ThucHienQuyI = qClsKhamSan_PK.Where(p => p.TenDV.ToLower().Contains("cắt a"))
                                            .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.ThucHienQuyII = qClsKhamSan_PK.Where(p => p.TenDV.ToLower().Contains("cắt a"))
                                            .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.ThucHienQuyIII = qClsKhamSan_PK.Where(p => p.TenDV.ToLower().Contains("cắt a"))
                                            .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenDV.ToLower().Contains("cắt a"))
                                            .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 13;
                        moi.DanhMuc = "Phẫu thuật Răng hàm mặt";
                        moi.ChiTietDanhMuc = "Thủ thuật TMH khác";
                        moi.DVTinh = "Ca";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 25 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 13;
                        moi.DanhMuc = "Phẫu thuật Răng hàm mặt";
                        moi.ChiTietDanhMuc = "Làm răng giả";
                        moi.DVTinh = "Ca";
                        moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 25 : 0;
                        _lContent.Add(moi);
                        #endregion
                    }
                    else
                    {
                        #region 1. Tổng số CBVC
                        moi = new Content();
                        moi.Stt = 1;
                        moi.DanhMuc = "Tổng số CBVC";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "Người";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 15 : 0;
                        moi.ThucHienNam1 = 0;
                        _lContent.Add(moi);
                        #endregion
                        #region 2. Giường bệnh
                        moi = new Content();
                        moi.Stt = 2;
                        moi.DanhMuc = "Giường khám bệnh";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "Giường";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 24 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 24 : 0;
                        _lContent.Add(moi);

                        //moi = new Content();
                        //moi.Stt = 2;
                        //moi.DanhMuc = "Giường khám bệnh";
                        //moi.ChiTietDanhMuc = "Tai mũi họng";
                        //moi.DVTinh = "Giường";
                        //moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 5 : 0;
                        //_lContent.Add(moi);

                        //moi = new Content();
                        //moi.Stt = 2;
                        //moi.DanhMuc = "Giường khám bệnh";
                        //moi.ChiTietDanhMuc = "Mắt";
                        //moi.DVTinh = "Giường";
                        //moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 5 : 0;
                        //_lContent.Add(moi);

                        //moi = new Content();
                        //moi.Stt = 2;
                        //moi.DanhMuc = "Giường khám bệnh";
                        //moi.ChiTietDanhMuc = "Răng hàm mặt";
                        //moi.DVTinh = "Giường";
                        //moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 5 : 0;
                        //_lContent.Add(moi);
                        #endregion
                        #region 3. Tổng số khám bệnh
                        moi = new Content();
                        moi.Stt = 3;
                        moi.DanhMuc = "Tổng số khám bệnh";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "Người";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 18000 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 18000 : 0;
                        _lContent.Add(moi);
                        //if (ck == 3 || ck == 1)
                        //{
                        moi = new Content();
                        moi.Stt = 3;
                        moi.DanhMuc = "Tổng số khám bệnh";
                        moi.ChiTietDanhMuc = "-Tai mũi họng";
                        moi.DVTinh = "Người";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 9000 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 9000 : 0;
                        moi.ThucHienNam2 = _luotKham.Where(p => p.ChuyenKhoa.Contains("Tai Mũi Họng")).Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay).Count();
                        moi.PtramNam2 = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienNam2 / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyI = _luotKham.Where(p => p.ChuyenKhoa.Contains("Tai Mũi Họng")).Where(p => p.NgayKham >= tungayquy1 && p.NgayKham <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyII = _luotKham.Where(p => p.ChuyenKhoa.Contains("Tai Mũi Họng")).Where(p => p.NgayKham >= tungayquy2 && p.NgayKham <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIII = _luotKham.Where(p => p.ChuyenKhoa.Contains("Tai Mũi Họng")).Where(p => p.NgayKham >= tungayquy3 && p.NgayKham <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIV = _luotKham.Where(p => p.ChuyenKhoa.Contains("Tai Mũi Họng")).Where(p => p.NgayKham >= tungayquy4 && p.NgayKham <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuNam2 * 100 : null;
                        moi.PtramNam2 = Math.Round(moi.PtramNam2.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);
                        //}
                        //if (ck == 3 || ck == 2)
                        //{
                        moi = new Content();
                        moi.Stt = 3;
                        moi.DanhMuc = "Tổng số khám bệnh";
                        moi.ChiTietDanhMuc = "-Răng hàm mặt";
                        moi.DVTinh = "Người";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 3000 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 3000 : 0;
                        moi.ThucHienNam2 = _luotKham.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay).Count();
                        moi.PtramNam2 = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienNam2 / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyI = _luotKham.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.NgayKham >= tungayquy1 && p.NgayKham <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyII = _luotKham.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.NgayKham >= tungayquy2 && p.NgayKham <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIII = _luotKham.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.NgayKham >= tungayquy3 && p.NgayKham <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIV = _luotKham.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.NgayKham >= tungayquy4 && p.NgayKham <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuNam2 * 100 : null;
                        moi.PtramNam2 = Math.Round(moi.PtramNam2.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 3;
                        moi.DanhMuc = "Tổng số khám bệnh";
                        moi.ChiTietDanhMuc = "-Mắt";
                        moi.DVTinh = "";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 6000 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 6000 : 0;
                        moi.ThucHienNam2 = _luotKham.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay).Count();
                        moi.PtramNam2 = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienNam2 / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyI = _luotKham.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.NgayKham >= tungayquy1 && p.NgayKham <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyII = _luotKham.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.NgayKham >= tungayquy2 && p.NgayKham <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIII = _luotKham.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.NgayKham >= tungayquy3 && p.NgayKham <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIV = _luotKham.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.NgayKham >= tungayquy4 && p.NgayKham <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuNam2 * 100 : null;
                        moi.PtramNam2 = Math.Round(moi.PtramNam2.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);
                        //}
                        //if (ck == 3 || ck == 0)
                        //{
                        //}

                        moi = new Content();
                        moi.Stt = 3;
                        moi.DanhMuc = "Tổng số khám bệnh";
                        moi.ChiTietDanhMuc = "Bệnh nhân điều trị ngoại trú RHM";
                        moi.DVTinh = "Ca";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 600 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 600 : 0;
                        moi.ThucHienNam2 = qClsKhamSan_PK.Where(p => p.TenRG == "Thủ thuật").Where(p => p.TenDV.Contains("răng"))
                                                          .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.PtramNam2 = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienNam2 / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyI = qClsKhamSan_PK.Where(p => p.TenRG == "Thủ thuật").Where(p => p.TenDV.Contains("răng"))
                                                         .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyII = qClsKhamSan_PK.Where(p => p.TenRG == "Thủ thuật").Where(p => p.TenDV.Contains("răng"))
                                                          .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIII = qClsKhamSan_PK.Where(p => p.TenRG == "Thủ thuật").Where(p => p.TenDV.Contains("răng"))
                                                           .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenRG == "Thủ thuật").Where(p => p.TenDV.Contains("răng"))
                                                          .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuNam2 * 100 : null;
                        moi.PtramNam2 = Math.Round(moi.PtramNam2.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);
                        #endregion
                        #region 4. Cận lâm sàng tại phòng khám
                        moi = new Content();
                        moi.Stt = 4;
                        moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "Người";
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 4;
                        moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                        moi.ChiTietDanhMuc = "- Huyết học";
                        moi.DVTinh = "Lần";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 3060 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 3060 : 0;
                        moi.ThucHienNam2 = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                                                          .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.PtramNam2 = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienNam2 / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyI = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                                                          .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyII = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                                                          .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIII = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                                                          .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                                                          .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuNam2 * 100 : null;
                        moi.PtramNam2 = Math.Round(moi.PtramNam2.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 4;
                        moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                        moi.ChiTietDanhMuc = "      Tỷ lệ";
                        moi.DVTinh = "%";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 17 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 17 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 4;
                        moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                        moi.ChiTietDanhMuc = "- Sinh hóa nước tiểu";
                        moi.DVTinh = "Lần";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 1440 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 1440 : 0;
                        moi.ThucHienNam2 = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)
                                                          .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.PtramNam2 = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienNam2 / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyI = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)
                                                          .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyII = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)
                                                          .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIII = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)
                                                          .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)
                                                          .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuNam2 * 100 : null;
                        moi.PtramNam2 = Math.Round(moi.PtramNam2.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 4;
                        moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                        moi.ChiTietDanhMuc = "      Tỷ lệ";
                        moi.DVTinh = "%";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 8 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 8 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 4;
                        moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                        moi.ChiTietDanhMuc = "- Sinh hóa máu";
                        moi.DVTinh = "Lần";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 9 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 9 : 0;
                        moi.ThucHienNam2 = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                                                          .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.PtramNam2 = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienNam2 / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyI = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                                                          .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyII = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                                                          .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIII = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                                                          .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                                                          .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuNam2 * 100 : null;
                        moi.PtramNam2 = Math.Round(moi.PtramNam2.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 4;
                        moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                        moi.ChiTietDanhMuc = "      Tỷ lệ";
                        moi.DVTinh = "%";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 10 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 10 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 4;
                        moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                        moi.ChiTietDanhMuc = "- X-quang";
                        moi.DVTinh = "Lần";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 2160 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 2160 : 0;
                        moi.ThucHienNam2 = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)
                                                          .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.PtramNam2 = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienNam2 / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyI = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)
                                                          .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyII = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)
                                                          .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIII = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)
                                                          .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)
                                                          .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuNam2 * 100 : null;
                        moi.PtramNam2 = Math.Round(moi.PtramNam2.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 4;
                        moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                        moi.ChiTietDanhMuc = "      Tỷ lệ";
                        moi.DVTinh = "%";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 12 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 12 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 4;
                        moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                        moi.ChiTietDanhMuc = "- Siêu âm";
                        moi.DVTinh = "Lần";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 1080 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 1080 : 0;
                        moi.ThucHienNam2 = qClsKhamSan_PK.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm))
                                                          .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.PtramNam2 = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienNam2 / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyI = qClsKhamSan_PK.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm))
                                                          .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyII = qClsKhamSan_PK.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm))
                                                          .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIII = qClsKhamSan_PK.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm))
                                                          .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm))
                                                          .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuNam2 * 100 : null;
                        moi.PtramNam2 = Math.Round(moi.PtramNam2.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 4;
                        moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                        moi.ChiTietDanhMuc = "      Tỷ lệ";
                        moi.DVTinh = "%";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 6 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 6 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 4;
                        moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                        moi.ChiTietDanhMuc = "      Nội soi T-M-H";
                        moi.DVTinh = "Lần";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 6870 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 6870 : 0;
                        moi.ThucHienNam2 = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong)
                                                          .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.PtramNam2 = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienNam2 / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyI = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong)
                                                          .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyII = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong)
                                                          .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIII = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong)
                                                          .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong)
                                                          .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuNam2 * 100 : null;
                        moi.PtramNam2 = Math.Round(moi.PtramNam2.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 4;
                        moi.DanhMuc = "Cận lâm sàng tại phòng khám";
                        moi.ChiTietDanhMuc = "- Tỷ lệ";
                        moi.DVTinh = "%";
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 47 : 0;
                        _lContent.Add(moi);
                        #endregion
                        #region 5. Ngày điều trị trung bình
                        moi = new Content();
                        moi.Stt = 5;
                        moi.DanhMuc = "Ngày điều trị trung bình";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "Ngày";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 5 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 5 : 0;
                        moi.ThucHienNam2 = (bnRV.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Count() != 0) ? (int)bnRV.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Sum(p => p.rv.SoNgaydt) / bnRV.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Count() : 0;
                        moi.PtramNam2 = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienNam2 / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyI = (bnRV.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Count() != 0) ? (int)bnRV.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Sum(p => p.rv.SoNgaydt) / bnRV.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Count() : 0;
                        moi.PtramQuyI = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyII = (bnRV.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Count() != 0) ? (int)bnRV.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Sum(p => p.rv.SoNgaydt) / bnRV.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Count() : 0;
                        moi.PtramQuyII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyIII = (bnRV.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Count() != 0) ? (int)bnRV.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Sum(p => p.rv.SoNgaydt) / bnRV.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Count() : 0;
                        moi.PtramQuyIII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyIV = (bnRV.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Count() != 0) ? (int)bnRV.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Sum(p => p.rv.SoNgaydt) / bnRV.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Count() : 0;
                        moi.PtramQuyIV = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuNam2 * 100 : 0;
                        moi.PtramNam2 = Math.Round(moi.PtramNam2.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);
                        #endregion
                        #region 6.Tổng số ngày điều trị
                        moi = new Content();
                        moi.Stt = 6;
                        moi.DanhMuc = "Tổng số ngày điều trị";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "Ngày";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 7600 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 7600 : 0;
                        moi.ThucHienNam2 = bnRV.Where(p => p.rv.NgayRa >= tungay && p.rv.NgayRa <= denngay).Sum(p => p.rv.SoNgaydt);
                        moi.PtramNam2 = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienNam2 / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyI = bnRV.Where(p => p.rv.NgayRa >= tungayquy1 && p.rv.NgayRa <= denngayquy1).Sum(p => p.rv.SoNgaydt);
                        moi.PtramQuyI = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyII = bnRV.Where(p => p.rv.NgayRa >= tungayquy2 && p.rv.NgayRa <= denngayquy2).Sum(p => p.rv.SoNgaydt);
                        moi.PtramQuyII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyIII = bnRV.Where(p => p.rv.NgayRa >= tungayquy3 && p.rv.NgayRa <= denngayquy3).Sum(p => p.rv.SoNgaydt);
                        moi.PtramQuyIII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyIV = bnRV.Where(p => p.rv.NgayRa >= tungayquy4 && p.rv.NgayRa <= denngayquy4).Sum(p => p.rv.SoNgaydt);
                        moi.PtramQuyIV = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuNam2 * 100 : 0;
                        moi.PtramNam2 = Math.Round(moi.PtramNam2.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);
                        #endregion
                        #region 7. Ngày sử dụng giường/tháng
                        moi = new Content();
                        moi.Stt = 7;
                        moi.DanhMuc = "Ngày sử dụng giường/tháng";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "Ngày";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 30 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 30 : 0;
                        _lContent.Add(moi);
                        #endregion
                        #region 8. Công suất sử dụng giường bệnh
                        moi = new Content();
                        moi.Stt = 8;
                        moi.DanhMuc = "Công suất sử dụng giường bệnh";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "%";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 100 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 100 : 0;
                        _lContent.Add(moi);
                        #endregion
                        #region 9. Tai biến trong điều trị
                        moi = new Content();
                        moi.Stt = 9;
                        moi.DanhMuc = "Tai biến trong điều trị";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "Ca";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 0 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 0 : 0;
                        _lContent.Add(moi);
                        #endregion
                        #region 10. Bệnh nhân điều trị nội trú
                        moi = new Content();
                        moi.Stt = 10;
                        moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "Người";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 1520 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 1520 : 0;
                        moi.ThucHienNam2 = bnNoiTru.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Count();
                        moi.PtramNam2 = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienNam2 / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyI = bnNoiTru.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyII = bnNoiTru.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyIII = bnNoiTru.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyIV = bnNoiTru.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuNam2 * 100 : 0;
                        moi.PtramNam2 = Math.Round(moi.PtramNam2.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);
                        //if (ck == 3 || ck == 1)
                        //{
                        moi = new Content();
                        moi.Stt = 10;
                        moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                        moi.ChiTietDanhMuc = "- Tai mũi họng";
                        moi.DVTinh = "Người";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 700 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 700 : 0;
                        moi.ThucHienNam2 = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Tai Mũi Họng")).Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Count();
                        moi.PtramNam2 = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienNam2 / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyI = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Tai Mũi Họng")).Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyII = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Tai Mũi Họng")).Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyIII = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Tai Mũi Họng")).Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyIV = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Tai Mũi Họng")).Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuNam2 * 100 : 0;
                        moi.PtramNam2 = Math.Round(moi.PtramNam2.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);
                        //}
                        //if (ck == 3 || ck == 0)
                        //{
                        moi = new Content();
                        moi.Stt = 10;
                        moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                        moi.ChiTietDanhMuc = "- Răng hàm mặt";
                        moi.DVTinh = "Người";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 330 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 330 : 0;
                        moi.ThucHienNam2 = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Count();
                        moi.PtramNam2 = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienNam2 / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyI = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyII = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyIII = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyIV = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuNam2 * 100 : 0;
                        moi.PtramNam2 = Math.Round(moi.PtramNam2.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);
                        //}
                        //if (ck == 3 || ck == 2)
                        //{
                        moi = new Content();
                        moi.Stt = 10;
                        moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                        moi.ChiTietDanhMuc = "- Mắt";
                        moi.DVTinh = "Người";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 490 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 490 : 0;
                        moi.ThucHienNam2 = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Count();
                        moi.PtramNam2 = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienNam2 / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyI = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyII = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyIII = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyIV = bnNoiTru.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuNam2 * 100 : 0;
                        moi.PtramNam2 = Math.Round(moi.PtramNam2.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);
                        //}
                        #endregion
                        #region 11. Tỷ lệ điều trị khỏi
                        moi = new Content();
                        moi.Stt = 10;
                        moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                        moi.ChiTietDanhMuc = "Tỷ lệ điều trị khỏi";
                        moi.DVTinh = "%";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 80 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 80 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 10;
                        moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                        moi.ChiTietDanhMuc = "Tổng BN điều trị khỏi";
                        moi.DVTinh = "Người";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 1216 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 1216 : 0;
                        moi.ThucHienNam2 = bnNoiTru.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua == "Khỏi").Count();
                        moi.PtramNam2 = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienNam2 / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyI = bnNoiTru.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.KetQua == "Khỏi").Count();
                        moi.PtramQuyI = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyII = bnNoiTru.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.KetQua == "Khỏi").Count();
                        moi.PtramQuyII = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyIII = bnNoiTru.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.KetQua == "Khỏi").Count();
                        moi.PtramQuyIII = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyIV = bnNoiTru.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Khỏi").Count();
                        moi.PtramQuyIV = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuNam2 * 100 : 0;
                        moi.PtramNam2 = Math.Round(moi.PtramNam2.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);
                        #endregion
                        #region 12. Tỷ lệ đỡ
                        moi = new Content();
                        moi.Stt = 10;
                        moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                        moi.ChiTietDanhMuc = "Tỷ lệ đỡ";
                        moi.DVTinh = "%";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 17 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 17 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 10;
                        moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                        moi.ChiTietDanhMuc = "Tổng BN điều trị đỡ";
                        moi.DVTinh = "Người";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 258 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 258 : 0;
                        moi.ThucHienNam2 = bnNoiTru.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                        moi.PtramNam2 = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienNam2 / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyI = bnNoiTru.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                        moi.PtramQuyI = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyII = bnNoiTru.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                        moi.PtramQuyII = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyIII = bnNoiTru.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                        moi.PtramQuyIII = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyIV = bnNoiTru.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Đỡ|Giảm").Count();
                        moi.PtramQuyIV = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuNam2 * 100 : 0;
                        moi.PtramNam2 = Math.Round(moi.PtramNam2.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);
                        #endregion
                        #region 13. Tỷ lệ BN chuyển viện
                        moi = new Content();
                        moi.Stt = 10;
                        moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                        moi.ChiTietDanhMuc = "Tỷ lệ BN chuyển viện";
                        moi.DVTinh = "%";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 1 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 1 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 10;
                        moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                        moi.ChiTietDanhMuc = "Tổng BN chuyển viện";
                        moi.DVTinh = "Người";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 14 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 14 : 0;
                        moi.ThucHienNam2 = bnNoiTru.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.MaBVC != null).Count();
                        moi.PtramNam2 = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienNam2 / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyI = bnNoiTru.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.MaBVC != null).Count();
                        moi.PtramQuyI = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyII = bnNoiTru.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.MaBVC != null).Count();
                        moi.PtramQuyII = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyIII = bnNoiTru.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.MaBVC != null).Count();
                        moi.PtramQuyIII = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyIV = bnNoiTru.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.MaBVC != null).Count();
                        moi.PtramQuyIV = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuNam2 * 100 : 0;
                        moi.PtramNam2 = Math.Round(moi.PtramNam2.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);
                        #endregion
                        #region 14. Tỷ lệ xin về
                        moi = new Content();
                        moi.Stt = 10;
                        moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                        moi.ChiTietDanhMuc = "Tỷ lệ xin về";
                        moi.DVTinh = "%";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 2 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 2 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 10;
                        moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                        moi.ChiTietDanhMuc = "Tổng số BN nặng xin về";
                        moi.DVTinh = "Người";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 0 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 0 : 0;
                        moi.ThucHienNam2 = bnNoiTru.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua == "Nặng hơn").Count();
                        moi.PtramNam2 = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienNam2 / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyI = bnNoiTru.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.KetQua == "Nặng hơn").Count();
                        moi.PtramQuyI = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyII = bnNoiTru.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.KetQua == "Nặng hơn").Count();
                        moi.PtramQuyII = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyIII = bnNoiTru.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.KetQua == "Nặng hơn").Count();
                        moi.PtramQuyIII = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyIV = bnNoiTru.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Nặng hơn").Count();
                        moi.PtramQuyIV = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuNam2 * 100 : 0;
                        moi.PtramNam2 = Math.Round(moi.PtramNam2.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);
                        #endregion
                        #region 15. Tỷ lệ tử vong
                        moi = new Content();
                        moi.Stt = 10;
                        moi.DanhMuc = "Tổng số bệnh nhân điều trị nội trú";
                        moi.ChiTietDanhMuc = "Tỷ lệ tử vong";
                        moi.DVTinh = "%";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 0 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 0 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 10;
                        moi.DanhMuc = "Tổng số bênh nhân điều trị nội trú";
                        moi.ChiTietDanhMuc = "Tổng số BN tử vong";
                        moi.DVTinh = "Người";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 0 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 0 : 0;
                        moi.ThucHienNam2 = bnNoiTru.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua == "Tử vong").Count();
                        moi.PtramNam2 = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienNam2 / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyI = bnNoiTru.Where(p => p.NgayRa >= tungayquy1 && p.NgayRa <= denngayquy1).Where(p => p.KetQua == "Tử vong").Count();
                        moi.PtramQuyI = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyI / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyII = bnNoiTru.Where(p => p.NgayRa >= tungayquy2 && p.NgayRa <= denngayquy2).Where(p => p.KetQua == "Tử vong").Count();
                        moi.PtramQuyII = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyII / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyIII = bnNoiTru.Where(p => p.NgayRa >= tungayquy3 && p.NgayRa <= denngayquy3).Where(p => p.KetQua == "Tử vong").Count();
                        moi.PtramQuyIII = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyIII / moi.ChiTieuNam2 * 100 : 0;
                        moi.ThucHienQuyIV = bnNoiTru.Where(p => p.NgayRa >= tungayquy4 && p.NgayRa <= denngayquy4).Where(p => p.KetQua == "Tử vong").Count();
                        moi.PtramQuyIV = (moi.ChiTieuNam2 != 0) ? (double)moi.ThucHienQuyIV / moi.ChiTieuNam2 * 100 : 0;
                        moi.PtramNam2 = Math.Round(moi.PtramNam2.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);
                        #endregion
                        #region 16. Cận lâm sàng tại bệnh phòng
                        var qCls_Noitru = (from a in bnNoiTru
                                           join b in qCLS_KB on a.MaBNhan equals b.MaBNhan
                                           join c in qCLS_KB1 on b.IdCLS equals c.IdCLS
                                           select new { b.IdCLS, b.NgayTH, b.MaBNhan, b.MaKP, b.TenKP, c.Status, c.TenRG, c.Loai, c.IDCD, c.TenDV }).ToList();
                        moi = new Content();
                        moi.Stt = 11;
                        moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "Người";
                        _lContent.Add(moi);

                        //moi = new Content();
                        //moi.Stt = 11;
                        //moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                        //moi.ChiTietDanhMuc = "Truyền máu";
                        //moi.DVTinh = "ui";
                        //_lContent.Add(moi);
                        #region Huyết học
                        moi = new Content();
                        moi.Stt = 11;
                        moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                        moi.ChiTietDanhMuc = "- Tỷ lệ huyết học";
                        moi.DVTinh = "%";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 100 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 100 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 11;
                        moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                        moi.ChiTietDanhMuc = "Tổng số";
                        moi.DVTinh = "Lần";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 1520 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 1520 : 0;
                        moi.ThucHienNam2 = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                                                          .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.PtramNam2 = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienNam2 / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyI = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                                                          .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyII = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                                                          .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIII = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                                                          .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIV = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
                                                          .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuNam2 * 100 : null;
                        moi.PtramNam2 = Math.Round(moi.PtramNam2.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);
                        #endregion

                        #region Sinh hóa máu
                        moi = new Content();
                        moi.Stt = 11;
                        moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                        moi.ChiTietDanhMuc = "- Sinh hóa máu";
                        moi.DVTinh = "%";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 100 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 100 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 11;
                        moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                        moi.ChiTietDanhMuc = "Tổng số";
                        moi.DVTinh = "Lần";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 7600 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 7600 : 0;
                        moi.ThucHienNam2 = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                                                          .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.PtramNam2 = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienNam2 / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyI = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                                                          .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyII = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                                                          .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIII = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                                                          .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIV = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)
                                                          .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuNam2 * 100 : null;
                        moi.PtramNam2 = Math.Round(moi.PtramNam2.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);

                        #endregion

                        #region Sinh hóa nước tiểu
                        //moi = new Content();
                        moi.Stt = 11;
                        moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                        moi.ChiTietDanhMuc = "- Sinh hóa nước tiểu";
                        moi.DVTinh = "%";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 100 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 100 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 11;
                        moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                        moi.ChiTietDanhMuc = "Tổng số";
                        moi.DVTinh = "Lần";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 1520 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 1520 : 0;
                        moi.ThucHienNam2 = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)
                                                          .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.PtramNam2 = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienNam2 / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyI = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)
                                                          .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyII = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)
                                                          .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIII = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)
                                                          .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIV = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu)
                                                          .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuNam2 * 100 : null;
                        moi.PtramNam2 = Math.Round(moi.PtramNam2.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);

                        #endregion

                        #region Điện tim

                        moi = new Content();
                        moi.Stt = 11;
                        moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                        moi.ChiTietDanhMuc = "- Điện tim";
                        moi.DVTinh = "Lần";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 532 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 532 : 0;
                        moi.ThucHienNam2 = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)
                                                          .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.PtramNam2 = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienNam2 / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyI = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)
                                                          .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyII = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)
                                                          .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIII = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)
                                                          .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIV = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)
                                                          .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuNam2 * 100 : null;
                        moi.PtramNam2 = Math.Round(moi.PtramNam2.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 11;
                        moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                        moi.ChiTietDanhMuc = "Tỷ lệ";
                        moi.DVTinh = "%";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 35 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 35 : 0;
                        _lContent.Add(moi);

                        #endregion

                        #region Siêu âm

                        moi = new Content();
                        moi.Stt = 11;
                        moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                        moi.ChiTietDanhMuc = "- Siêu âm";
                        moi.DVTinh = "Lần";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 304 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 304 : 0;
                        moi.ThucHienNam2 = qCls_Noitru.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm))
                                                          .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.PtramNam2 = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienNam2 / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyI = qCls_Noitru.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm))
                                                          .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyII = qCls_Noitru.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm))
                                                          .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIII = qCls_Noitru.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm))
                                                          .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIV = qCls_Noitru.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm))
                                                          .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuNam2 * 100 : null;
                        moi.PtramNam2 = Math.Round(moi.PtramNam2.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 11;
                        moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                        moi.ChiTietDanhMuc = "Tỷ lệ";
                        moi.DVTinh = "%";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 20 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 20 : 0;
                        _lContent.Add(moi);

                        #endregion

                        #region X-quang
                        moi = new Content();
                        moi.Stt = 11;
                        moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                        moi.ChiTietDanhMuc = "- X-quang";
                        moi.DVTinh = "Lần";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 304 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 304 : 0;
                        moi.ThucHienNam2 = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)
                                                          .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.PtramNam2 = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienNam2 / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyI = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)
                                                          .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyII = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)
                                                          .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIII = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)
                                                          .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIV = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)
                                                          .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuNam2 * 100 : null;
                        moi.PtramNam2 = Math.Round(moi.PtramNam2.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 11;
                        moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                        moi.ChiTietDanhMuc = "Tỷ lệ";
                        moi.DVTinh = "%";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 30 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 30 : 0;
                        _lContent.Add(moi);
                        #endregion

                        #region Nội soi TMH
                        moi = new Content();
                        moi.Stt = 11;
                        moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                        moi.ChiTietDanhMuc = "- Nội soi T- M- H";
                        moi.DVTinh = "Lần";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 456 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 456 : 0;
                        moi.ThucHienNam2 = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong)
                                                          .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.PtramNam2 = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienNam2 / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyI = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong)
                                                          .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.PtramQuyI = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyI / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyII = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong)
                                                          .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.PtramQuyII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIII = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong)
                                                          .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.PtramQuyIII = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIII / moi.ChiTieuNam2 * 100 : null;
                        moi.ThucHienQuyIV = qCls_Noitru.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong)
                                                          .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        moi.PtramQuyIV = (moi.ChiTieuNam2 != 0 || moi.ChiTieuNam2 != null) ? (double)moi.ThucHienQuyIV / moi.ChiTieuNam2 * 100 : null;
                        moi.PtramNam2 = Math.Round(moi.PtramNam2.GetValueOrDefault(), 2);
                        moi.PtramQuyI = Math.Round(moi.PtramQuyI.GetValueOrDefault(), 2);
                        moi.PtramQuyII = Math.Round(moi.PtramQuyII.GetValueOrDefault(), 2);
                        moi.PtramQuyIII = Math.Round(moi.PtramQuyIII.GetValueOrDefault(), 2);
                        moi.PtramQuyIV = Math.Round(moi.PtramQuyIV.GetValueOrDefault(), 2);
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 11;
                        moi.DanhMuc = "Cận lâm sàng tại bệnh phòng";
                        moi.ChiTietDanhMuc = "Tỷ lệ";
                        moi.DVTinh = "%";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 30 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 30 : 0;
                        _lContent.Add(moi);
                        #endregion

                        #endregion
                        #region 17. Tổng Phẫu thuật

                        moi = new Content();
                        moi.Stt = 12;
                        moi.DanhMuc = "Tổng phẫu thuật";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "Ca";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 550 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 600 : 0;
                        moi.ThucHienNam2 = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.ThucHienQuyI = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.ThucHienQuyII = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.ThucHienQuyIII = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 12;
                        moi.DanhMuc = "Tổng phẫu thuật";
                        moi.ChiTietDanhMuc = "      + Loại I";
                        moi.DVTinh = "Ca";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 400 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 450 : 0;
                        moi.ThucHienNam2 = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.Loai == 1).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.ThucHienQuyI = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.Loai == 1).Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.ThucHienQuyII = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.Loai == 1).Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.ThucHienQuyIII = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.Loai == 1).Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.Loai == 1).Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 12;
                        moi.DanhMuc = "Tổng phẫu thuật";
                        moi.ChiTietDanhMuc = "      + Loại II";
                        moi.DVTinh = "Ca";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 100 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 100 : 0;
                        moi.ThucHienNam2 = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.Loai == 2).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.ThucHienQuyI = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.Loai == 2).Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.ThucHienQuyII = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.Loai == 2).Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.ThucHienQuyIII = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.Loai == 2).Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.Loai == 2).Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 12;
                        moi.DanhMuc = "Tổng phẫu thuật";
                        moi.ChiTietDanhMuc = "      + Loại III";
                        moi.DVTinh = "Ca";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 50 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 50 : 0;
                        moi.ThucHienNam2 = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.Loai == 3).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.ThucHienQuyI = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.Loai == 3).Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.ThucHienQuyII = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.Loai == 3).Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.ThucHienQuyIII = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Mắt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.Loai == 3).Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.Loai == 3).Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 12;
                        moi.DanhMuc = "Tổng phẫu thuật";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "Ca";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 600 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 600 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 12;
                        moi.DanhMuc = "Tổng phẫu thuật";
                        moi.ChiTietDanhMuc = "      Mổ Phaco";
                        moi.DVTinh = "Ca";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 300 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 500 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 12;
                        moi.DanhMuc = "Tổng phẫu thuật";
                        moi.ChiTietDanhMuc = "      Mổ khác";
                        moi.DVTinh = "Ca";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 300 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 100 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 12;
                        moi.DanhMuc = "Tổng phẫu thuật";
                        moi.ChiTietDanhMuc = "- Phẫu thuật răng hàm mặt";
                        moi.DVTinh = "Ca";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 50 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 50 : 0;
                        moi.ThucHienNam2 = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.ThucHienQuyI = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.ThucHienQuyII = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.ThucHienQuyIII = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.TenRG.Contains("Phẫu thuật"))
                                            .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 12;
                        moi.DanhMuc = "Tổng phẫu thuật";
                        moi.ChiTietDanhMuc = "- PT Cắt Amidal";
                        moi.DVTinh = "Ca";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 40 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 40 : 0;
                        moi.ThucHienNam2 = qClsKhamSan_PK.Where(p => p.TenDV.ToLower().Contains("cắt a"))
                                            .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.ThucHienQuyI = qClsKhamSan_PK.Where(p => p.TenDV.ToLower().Contains("cắt a"))
                                            .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.ThucHienQuyII = qClsKhamSan_PK.Where(p => p.TenDV.ToLower().Contains("cắt a"))
                                            .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.ThucHienQuyIII = qClsKhamSan_PK.Where(p => p.TenDV.ToLower().Contains("cắt a"))
                                            .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenDV.ToLower().Contains("cắt a"))
                                            .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 12;
                        moi.DanhMuc = "Tổng phẫu thuật";
                        moi.ChiTietDanhMuc = "- Thủ thuật TMH";
                        moi.DVTinh = "Ca";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 10 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 15 : 0;
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 12;
                        moi.DanhMuc = "Tổng phẫu thuật";
                        moi.ChiTietDanhMuc = "- Làm răng giả";
                        moi.DVTinh = "Ca";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 12 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 12 : 0;
                        _lContent.Add(moi);

                        #endregion
                        #region 18. Tổng ca thủ thuật chung
                        moi = new Content();
                        moi.Stt = 13;
                        moi.DanhMuc = "Tổng ca thủ thuật chung";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "Ca";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 2500 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 3500 : 0;
                        moi.ThucHienNam2 = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Thủ thuật"))
                                            .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        moi.ThucHienQuyI = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Thủ thuật"))
                                            .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        moi.ThucHienQuyII = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Thủ thuật"))
                                            .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        moi.ThucHienQuyIII = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Thủ thuật"))
                                            .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenRG.Contains("Thủ thuật"))
                                            .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        _lContent.Add(moi);

                        moi = new Content();
                        moi.Stt = 13;
                        moi.DanhMuc = "Tổng ca thủ thuật chung";
                        moi.ChiTietDanhMuc = "Tỷ lệ";
                        moi.DVTinh = "%";

                        moi = new Content();
                        moi.Stt = 13;
                        moi.DanhMuc = "Tổng ca thủ thuật chung";
                        moi.ChiTietDanhMuc = "      + Loại I";
                        moi.DVTinh = "Ca";

                        moi = new Content();
                        moi.Stt = 13;
                        moi.DanhMuc = "Tổng ca thủ thuật chung";
                        moi.ChiTietDanhMuc = "      + Loại II";
                        moi.DVTinh = "Ca";

                        moi = new Content();
                        moi.Stt = 13;
                        moi.DanhMuc = "Tổng ca thủ thuật chung";
                        moi.ChiTietDanhMuc = "      + Loại III";
                        moi.DVTinh = "Ca";

                        //moi = new Content();
                        //moi.Stt = 13;
                        //moi.DanhMuc = "Phẫu thuật Răng hàm mặt";
                        //moi.ChiTietDanhMuc = "Tổng tiểu phẫu";
                        //moi.DVTinh = "Ca";
                        //moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 25 : 0;
                        //moi.ThucHienNam2 = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.TenRG.Contains("Thủ thuật"))
                        //                    .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        //moi.ThucHienQuyI = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.TenRG.Contains("Thủ thuật"))
                        //                    .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        //moi.ThucHienQuyII = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.TenRG.Contains("Thủ thuật"))
                        //                    .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        //moi.ThucHienQuyIII = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.TenRG.Contains("Thủ thuật"))
                        //                    .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        //moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).Where(p => p.TenRG.Contains("Thủ thuật"))
                        //                    .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        //_lContent.Add(moi);

                        //moi = new Content();
                        //moi.Stt = 13;
                        //moi.DanhMuc = "Phẫu thuật Răng hàm mặt";
                        //moi.ChiTietDanhMuc = "Cắt Amidal";
                        //moi.DVTinh = "Ca";
                        //moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 25 : 0;
                        //moi.ThucHienNam2 = qClsKhamSan_PK.Where(p => p.TenDV.ToLower().Contains("cắt a"))
                        //                    .Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Count();
                        //moi.ThucHienQuyI = qClsKhamSan_PK.Where(p => p.TenDV.ToLower().Contains("cắt a"))
                        //                    .Where(p => p.NgayTH >= tungayquy1 && p.NgayTH <= denngayquy1).Count();
                        //moi.ThucHienQuyII = qClsKhamSan_PK.Where(p => p.TenDV.ToLower().Contains("cắt a"))
                        //                    .Where(p => p.NgayTH >= tungayquy2 && p.NgayTH <= denngayquy2).Count();
                        //moi.ThucHienQuyIII = qClsKhamSan_PK.Where(p => p.TenDV.ToLower().Contains("cắt a"))
                        //                    .Where(p => p.NgayTH >= tungayquy3 && p.NgayTH <= denngayquy3).Count();
                        //moi.ThucHienQuyIV = qClsKhamSan_PK.Where(p => p.TenDV.ToLower().Contains("cắt a"))
                        //                    .Where(p => p.NgayTH >= tungayquy4 && p.NgayTH <= denngayquy4).Count();
                        //_lContent.Add(moi);

                        //moi = new Content();
                        //moi.Stt = 13;
                        //moi.DanhMuc = "Phẫu thuật Răng hàm mặt";
                        //moi.ChiTietDanhMuc = "Thủ thuật TMH khác";
                        //moi.DVTinh = "Ca";
                        //moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 25 : 0;
                        //_lContent.Add(moi);

                        //moi = new Content();
                        //moi.Stt = 13;
                        //moi.DanhMuc = "Phẫu thuật Răng hàm mặt";
                        //moi.ChiTietDanhMuc = "Làm răng giả";
                        //moi.DVTinh = "Ca";
                        //moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2016) ? 25 : 0;
                        //_lContent.Add(moi);
                        #endregion
                        #region 19. Kiểm tra khoa phòng
                        moi = new Content();
                        moi.Stt = 15;
                        moi.DanhMuc = "Kiểm tra khoa phòng";
                        moi.DVTinh = "Lần";
                        moi.ChiTietDanhMuc = "";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 12 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 12 : 0;
                        _lContent.Add(moi);
                        #endregion
                        #region 20. sinh hoạt bệnh nhân
                        moi = new Content();
                        moi.Stt = 16;
                        moi.DanhMuc = "Sinh hoạt bệnh nhân";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "Lần";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 12 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 12 : 0;
                        _lContent.Add(moi);
                        #endregion
                        #region 21. Chuyển giao kỹ thuật cho tuyến xã
                        moi = new Content();
                        moi.Stt = 17;
                        moi.DanhMuc = "Chuyển giao kỹ thuật cho tuyến xã";
                        moi.DVTinh = "KT";
                        moi.ChiTietDanhMuc = "";
                        _lContent.Add(moi);
                        #endregion
                        #region 22. Kỹ thuật mới
                        moi = new Content();
                        moi.Stt = 18;
                        moi.DanhMuc = "Kỹ thuật mới";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "KT";
                        _lContent.Add(moi);
                        #endregion
                        #region 23. Cá nhân đạt lao động tiên tiến
                        moi = new Content();
                        moi.Stt = 19;
                        moi.DanhMuc = "Cá nhân đạt lao động tiên tiến";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "%";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 100 : 0;
                        _lContent.Add(moi);
                        #endregion
                        #region 24. Thi đua cấp cơ sở
                        moi = new Content();
                        moi.Stt = 20;
                        moi.DanhMuc = "Thi đua cấp cơ sở";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "Người";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 1 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 0 : 0;
                        _lContent.Add(moi);
                        #endregion
                        #region 25. Đề tài NCKH+Sáng kiến
                        moi = new Content();
                        moi.Stt = 21;
                        moi.DanhMuc = "Đề tài NCKH + Sáng kiến";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "ĐT+SK";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 1 : 0;
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 0 : 0;
                        _lContent.Add(moi);
                        #endregion
                        #region 26. Sở y tế khen
                        moi = new Content();
                        moi.Stt = 22;
                        moi.DanhMuc = "Sở Y tế khen";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "Người";
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 1 : 0;
                        _lContent.Add(moi);
                        #endregion
                        #region 27. UBND Huyện khen
                        moi = new Content();
                        moi.Stt = 23;
                        moi.DanhMuc = "UBND Huyện khen";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "Người";
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 1 : 0;
                        _lContent.Add(moi);
                        #endregion
                        #region 28. Giám đốc khen
                        moi = new Content();
                        moi.Stt = 24;
                        moi.DanhMuc = "Giám đốc khen";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "Người";
                        moi.ChiTieuNam2 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 1 : 0;
                        _lContent.Add(moi);
                        #endregion
                        #region 29. Bài tuyên truyền GDSK
                        moi = new Content();
                        moi.Stt = 25;
                        moi.DanhMuc = "Bài tuyên truyền GDSK";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "Bài";
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 2 : 0;
                        moi.ChiTieuNam1 = (Convert.ToInt32(cbbNam.SelectedValue) == 2019) ? 2 : 0;
                        _lContent.Add(moi);
                        #endregion
                        #region 30. Danh hiệu thi đua tập thể
                        moi = new Content();
                        moi.Stt = 26;
                        moi.DanhMuc = "Danh hiệu thi đua tập thể";
                        moi.ChiTietDanhMuc = "";
                        moi.DVTinh = "";
                        moi.ChiTieuNam2 = null;
                        #endregion

                    }
                }
                #endregion

                if (!chkMauMoi.Checked)
                {
                    #region 15. Kiểm tra khoa phòng
                    moi = new Content();
                    moi.Stt = 15;
                    moi.DanhMuc = "Kiểm tra khoa phòng";
                    moi.DVTinh = "Lần";
                    moi.ChiTietDanhMuc = "";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 12 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 16. sinh hoạt bệnh nhân
                    moi = new Content();
                    moi.Stt = 16;
                    moi.DanhMuc = "Sinh hoạt bệnh nhân";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Lần";
                    moi.ChiTieuCaNam = (Convert.ToInt32(cbbNam.SelectedValue) != 2016) ? 12 : 0;
                    _lContent.Add(moi);
                    #endregion
                    #region 17. Kỹ thuật mới
                    moi = new Content();
                    moi.Stt = 17;
                    moi.DanhMuc = "Kỹ thuật mới";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "KT";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 18. Chuyển giao kỹ thuật cho tuyến xã
                    moi = new Content();
                    moi.Stt = 18;
                    moi.DanhMuc = "Chuyển giao kỹ thuật cho tuyến xã";
                    moi.DVTinh = "KT";
                    moi.ChiTietDanhMuc = "";
                    _lContent.Add(moi);
                    #endregion
                    #region 19. Thi đua cấp ngành
                    moi = new Content();
                    moi.Stt = 19;
                    moi.DanhMuc = "Chiến sĩ thi đua cấp ngành";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 20. Thi đua cấp cơ sở
                    moi = new Content();
                    moi.Stt = 20;
                    moi.DanhMuc = "Thi đua cấp cơ sở";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Người";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 21. Đề tài NCKH+Sáng kiến
                    moi = new Content();
                    moi.Stt = 21;
                    moi.DanhMuc = "Đề tài NCKH + Sáng kiến";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "ĐT+SK";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion
                    #region 22. Bài tuyên truyền GDSK
                    moi = new Content();
                    moi.Stt = 22;
                    moi.DanhMuc = "Bài tuyên truyền GDSK";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "Bài";
                    moi.ChiTieuCaNam = 2;
                    _lContent.Add(moi);
                    #endregion
                    #region 23. Cá nhân đạt lao động tiên tiến
                    moi = new Content();
                    moi.Stt = 23;
                    moi.DanhMuc = "Cá nhân đạt lao động tiên tiến";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "%";
                    moi.ChiTieuCaNam = 100;
                    _lContent.Add(moi);
                    #endregion
                    #region 24. Danh hiệu khen thưởng
                    moi = new Content();
                    moi.Stt = 24;
                    moi.DanhMuc = "Danh hiệu khen thưởng";
                    moi.ChiTietDanhMuc = "";
                    moi.DVTinh = "";
                    moi.ChiTieuCaNam = null;
                    _lContent.Add(moi);
                    #endregion\
                }
            }

            if (_tenkhoa.ToUpper().Contains("KHOA LIÊN CHUYÊN KHOA") && chkMauMoi.Checked)
            {
                BaoCao.Rep_BC_ChiTieuChuyenMon_BVDKKimThanh_LCK_New rep = new BaoCao.Rep_BC_ChiTieuChuyenMon_BVDKKimThanh_LCK_New();
                frmIn frm = new frmIn();
                rep.celChiTieuCM.Text = "GIAO CHỈ TIÊU CHUYÊN MÔN NĂM " + cbbNam.SelectedValue;
                rep.lblTieuDe.Text = "GIAO CHỈ TIÊU CHUYÊN MÔN NĂM " + cbbNam.SelectedValue;
                rep.celNam1.Text = "Năm " + (Convert.ToInt32(cbbNam.SelectedValue) - 1).ToString();
                rep.celNam2.Text = "Năm " + cbbNam.SelectedValue;
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
            else
            {
                BaoCao.Rep_BC_ChiTieuChuyenMon_BVDKKimThanh rep = new BaoCao.Rep_BC_ChiTieuChuyenMon_BVDKKimThanh();
                frmIn frm = new frmIn();
                rep.celChiTieuCM.Text = "Chỉ tiêu chuyên môn năm " + cbbNam.SelectedValue;
                rep.celCaNam.Text = "Cả năm " + cbbNam.SelectedValue;
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
        #endregion
        #region Class Content
        public class Content
        {
            public int Stt { get; set; }
            public string DanhMuc { get; set; }
            public string ChiTietDanhMuc { get; set; }
            public string DVTinh { get; set; }
            public double? ChiTieuCaNam { get; set; }
            public double? ThucHienCaNam { get; set; }
            public double? PtramCaNam { get; set; }
            public double? ThucHienQuyI { get; set; }
            public double? PtramQuyI { get; set; }
            public double? ThucHienQuyII { get; set; }
            public double? PtramQuyII { get; set; }
            public double? ThucHienQuyIII { get; set; }
            public double? PtramQuyIII { get; set; }
            public double? ThucHienQuyIV { get; set; }
            public double? PtramQuyIV { get; set; }
            public double? ChiTieuNam1 { get; set; }
            public double? ThucHienNam1 { get; set; }
            public double? PtramNam2 { get; set; }
            public double? ChiTieuNam2 { get; set; }
            public double? ThucHienNam2 { get; set; }
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