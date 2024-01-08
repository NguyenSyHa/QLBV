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
    public partial class frm_BC_ChiTieu : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_ChiTieu()
        {
            InitializeComponent();
        }

        public class thangBC
        {
            public int Value { get; set; }
            public string Text { get; set; }
        }

        public class Quy
        {
            public int Value { get; set; }
            public string Text { get; set; }
        }

        public class namBC
        {
            public int Value { set; get; }
        }

        private void frm_BC_ChiTieu_Load(object sender, EventArgs e)
        {
            MinimizeBox = false;
            MaximizeBox = false;
            if (rdTheoNgay.Checked)
            {
                cbbQuy.Enabled = false;
                cbbNam_2.Enabled = false;
                lupDenNgay.Enabled = true;
                lupTuNgay.Enabled = true;
                cbbThang.Enabled = false;
                cbbNam.Enabled = false;
            }

            List<thangBC> _listThang = new List<thangBC>();
            _listThang.Add(new thangBC { Value = 3, Text = "3 tháng" });
            _listThang.Add(new thangBC { Value = 6, Text = "6 tháng" });
            _listThang.Add(new thangBC { Value = 9, Text = "9 tháng" });
            _listThang.Add(new thangBC { Value = 12, Text = "12 tháng" });

            cbbThang.DataSource = _listThang;
            cbbThang.DisplayMember = "Text";
            cbbThang.ValueMember = "Value";

            lupTuNgay.DateTime = DateTime.Now;
            lupDenNgay.DateTime = DateTime.Now;
            int namHT = DateTime.Now.Year;
            List<namBC> _list = new List<namBC>();
            for (int i = namHT - 10; i <= namHT; i++)
            {
                namBC obj = new namBC();
                obj.Value = i;
                _list.Add(obj);
            }
            cbbNam.DisplayMember = "Value";
            cbbNam.ValueMember = "Value";
            cbbNam.DataSource = _list;
            cbbNam.SelectedValue = namHT;

            cbbNam_2.DisplayMember = "Value";
            cbbNam_2.ValueMember = "Value";
            cbbNam_2.DataSource = _list;
            cbbNam_2.SelectedValue = namHT;

            List<Quy> _listQuy = new List<Quy>();
            _listQuy.Add(new Quy { Value = 1, Text = "Quý 1" });
            _listQuy.Add(new Quy { Value = 2, Text = "Quý 2" });
            _listQuy.Add(new Quy { Value = 3, Text = "Quý 3" });
            _listQuy.Add(new Quy { Value = 4, Text = "Quý 4" });

            cbbQuy.DataSource = _listQuy;
            cbbQuy.DisplayMember = "Text";
            cbbQuy.ValueMember = "Value";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        class cravien
        {
            public int maBN { set; get; }
            public int SoNgayDT { set; get; }
        }
        class vpctP 
        {
            public int? MaBNhan { get; set; }
            public int Mien { get; set; }
        }
        List<vpctP> _listVPCT = new List<vpctP>();
        List<Content> _listContent = new List<Content>();
        List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong> _da = new List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong>();
        List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong> _da1 = new List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong>();
        private void btnOK_Click(object sender, EventArgs e)
        {

            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<BenhVien> _lbenhvien = data.BenhViens.ToList();
            List<KPhong> _lkp = data.KPhongs.ToList();
            List<cravien> _lclRavien = (from a in data.RaViens select new cravien { maBN = a.MaBNhan, SoNgayDT = a.SoNgaydt ?? 0 }).ToList();
            _listContent.Clear();
            string tenhuyen = "";
            if (!string.IsNullOrEmpty(DungChung.Bien.MaHuyen))
            {
                var qhuyen = (from h in data.DmHuyens.Where(p => p.MaHuyen.Equals(DungChung.Bien.MaHuyen)) select h).ToList();
                if (qhuyen.Count > 0)
                    tenhuyen = qhuyen.FirstOrDefault().TenHuyen;
            }
            Content moi = new Content();
            DateTime tungayHT = new DateTime();
            DateTime denngayHT = new DateTime();
            DateTime _tungayNamTruoc = new DateTime();
            DateTime _denngayNamTruoc = new DateTime();
            string title = string.Empty;
            string kehoach = string.Empty;
            string _kq6thang2015 = string.Empty;
            string _kq6thang2016 = string.Empty;
            string _tile1 = string.Empty;
            string _tile2 = string.Empty;
            int nam1 = 0, namtruoc = 0;
            int ngay = 0;
            #region tìm theo ngày
            if (rdTheoNgay.Checked)
            {
                DateTime tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                DateTime denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                title = "(Kết quả thực hiện một số chỉ tiêu từ ngày " + tungay.ToShortDateString() + " đến ngày " + denngay.ToShortDateString();
                kehoach = "KH năm " + denngay.Year;
                _kq6thang2015 = "Kết quả cùng kỳ năm trước";
                _kq6thang2016 = "Kết quả kỳ này";
                _tile1 = "Tỉ lệ %  so với KH kỳ này";
                _tile2 = "Tỉ lệ % so với KH kỳ trước";
                tungayHT = tungay;
                denngayHT = denngay;
                ngay = (denngayHT - tungayHT).Days + 1;
                _tungayNamTruoc = tungayHT.AddYears(-1);
                _denngayNamTruoc = denngayHT.AddYears(-1);
                nam1 = tungayHT.Year;
                namtruoc = _tungayNamTruoc.Year;
            }
            #endregion
            #region tìm theo tháng
            if (radThang.Checked)
            {
                int _thang = (int)cbbThang.SelectedValue;
                int _nam = (int)cbbNam.SelectedValue;
                int _namTruoc = (int)cbbNam.SelectedValue - 1;
                title = "(Kết quả thực hiện một số chỉ tiêu " + (Convert.ToInt32(cbbThang.SelectedValue)).ToString() + " tháng đầu năm " + _nam + ")";
                kehoach = "KH năm " + cbbNam.Text;
                _kq6thang2015 = "Kết quả " + (Convert.ToInt32(cbbThang.SelectedValue)).ToString() + " tháng năm" + (Convert.ToInt32(cbbNam.SelectedValue) - 1).ToString();
                _kq6thang2016 = "Kết quả " + (Convert.ToInt32(cbbThang.SelectedValue)).ToString() + " tháng năm" + (Convert.ToInt32(cbbNam.SelectedValue)).ToString();
                _tile1 = "Tỉ lệ % " + (Convert.ToInt32(cbbThang.SelectedValue)).ToString() + " tháng " + (Convert.ToInt32(cbbNam.SelectedValue)).ToString() + " so với KH năm" + (Convert.ToInt32(cbbNam.SelectedValue)).ToString();
                _tile2 = "Tỉ lệ % " + (Convert.ToInt32(cbbThang.SelectedValue)).ToString() + " tháng " + (Convert.ToInt32(cbbNam.SelectedValue)).ToString() + " so với cùng kỳ năm" + (Convert.ToInt32(cbbNam.SelectedValue) - 1).ToString();

                switch (_thang)
                {
                    case 3:
                        tungayHT = new DateTime(_nam, 1, 1);
                        denngayHT = new DateTime(_nam, 4, 1);
                        tungayHT = DungChung.Ham.NgayTu(tungayHT);
                        denngayHT = DungChung.Ham.NgayDen(denngayHT.AddDays(-1));
                        _tungayNamTruoc = tungayHT.AddYears(-1);
                        _denngayNamTruoc = denngayHT.AddYears(-1);
                        break;
                    case 6:
                        tungayHT = new DateTime(_nam, 1, 1);
                        denngayHT = new DateTime(_nam, 7, 1);
                        tungayHT = DungChung.Ham.NgayTu(tungayHT);
                        denngayHT = DungChung.Ham.NgayDen(denngayHT.AddDays(-1));
                        _tungayNamTruoc = tungayHT.AddYears(-1);
                        _denngayNamTruoc = denngayHT.AddYears(-1);
                        break;
                    case 9:
                        tungayHT = new DateTime(_nam, 1, 1);
                        denngayHT = new DateTime(_nam, 10, 1);
                        tungayHT = DungChung.Ham.NgayTu(tungayHT);
                        denngayHT = DungChung.Ham.NgayDen(denngayHT.AddDays(-1));
                        _tungayNamTruoc = tungayHT.AddYears(-1);
                        _denngayNamTruoc = denngayHT.AddYears(-1);
                        break;
                    case 12:
                        tungayHT = new DateTime(_nam, 1, 1);
                        denngayHT = new DateTime(_nam + 1, 1, 1);
                        tungayHT = DungChung.Ham.NgayTu(tungayHT);
                        denngayHT = DungChung.Ham.NgayDen(denngayHT.AddDays(-1));
                        _tungayNamTruoc = tungayHT.AddYears(-1);
                        _denngayNamTruoc = denngayHT.AddYears(-1);
                        break;
                }
                nam1 = tungayHT.Year;
                namtruoc = _tungayNamTruoc.Year;
                ngay = (denngayHT - tungayHT).Days + 1;
            }
            #endregion
            #region tìm theo quý
            if (radQuy.Checked)
            {
                int _thang = (int)cbbQuy.SelectedValue;
                int _nam = (int)cbbNam_2.SelectedValue;
                int _namTruoc = (int)cbbNam_2.SelectedValue - 1;
                title = "(Kết quả thực hiện một số chỉ tiêu Quý " + (Convert.ToInt32(cbbQuy.SelectedValue)).ToString() + " năm " + _nam + ")";
                kehoach = "KH năm " + cbbNam_2.Text;
                _kq6thang2015 = "Kết quả Quý " + (Convert.ToInt32(cbbQuy.SelectedValue)).ToString() + " năm" + (Convert.ToInt32(cbbNam_2.SelectedValue) - 1).ToString();
                _kq6thang2016 = "Kết quả Quý " + (Convert.ToInt32(cbbQuy.SelectedValue)).ToString() + " năm" + (Convert.ToInt32(cbbNam_2.SelectedValue)).ToString();
                _tile1 = "Tỉ lệ % Quý " + (Convert.ToInt32(cbbQuy.SelectedValue)).ToString() + " năm " + (Convert.ToInt32(cbbNam_2.SelectedValue)).ToString() + " so với KH năm" + (Convert.ToInt32(cbbNam_2.SelectedValue)).ToString();
                _tile2 = "Tỉ lệ % Quý " + (Convert.ToInt32(cbbQuy.SelectedValue)).ToString() + " năm " + (Convert.ToInt32(cbbNam_2.SelectedValue)).ToString() + " so với cùng kỳ năm" + (Convert.ToInt32(cbbNam_2.SelectedValue) - 1).ToString();

                switch (_thang)
                {
                    case 1:
                        tungayHT = new DateTime(_nam, 1, 1);
                        denngayHT = new DateTime(_nam, 4, 1);
                        tungayHT = DungChung.Ham.NgayTu(tungayHT);
                        denngayHT = DungChung.Ham.NgayDen(denngayHT.AddDays(-1));
                        _tungayNamTruoc = tungayHT.AddYears(-1);
                        _denngayNamTruoc = denngayHT.AddYears(-1);
                        break;
                    case 2:
                        tungayHT = new DateTime(_nam, 3, 1);
                        denngayHT = new DateTime(_nam, 7, 1);
                        tungayHT = DungChung.Ham.NgayTu(tungayHT);
                        denngayHT = DungChung.Ham.NgayDen(denngayHT.AddDays(-1));
                        _tungayNamTruoc = tungayHT.AddYears(-1);
                        _denngayNamTruoc = denngayHT.AddYears(-1);
                        break;
                    case 3:
                        tungayHT = new DateTime(_nam, 6, 1);
                        denngayHT = new DateTime(_nam, 10, 1);
                        tungayHT = DungChung.Ham.NgayTu(tungayHT);
                        denngayHT = DungChung.Ham.NgayDen(denngayHT.AddDays(-1));
                        _tungayNamTruoc = tungayHT.AddYears(-1);
                        _denngayNamTruoc = denngayHT.AddYears(-1);
                        break;
                    case 4:
                        tungayHT = new DateTime(_nam, 9, 1);
                        denngayHT = new DateTime(_nam + 1, 1, 1);
                        tungayHT = DungChung.Ham.NgayTu(tungayHT);
                        denngayHT = DungChung.Ham.NgayDen(denngayHT.AddDays(-1));
                        _tungayNamTruoc = tungayHT.AddYears(-1);
                        _denngayNamTruoc = denngayHT.AddYears(-1);
                        break;
                }
                nam1 = tungayHT.Year;
                namtruoc = _tungayNamTruoc.Year;
                ngay = (denngayHT - tungayHT).Days + 1;
            }
            #endregion

            #region query
            var qTong1 = (from bnkb in data.BNKBs.Where(p => p.NgayKham >= _tungayNamTruoc && p.NgayKham <= denngayHT) 
                         select new {bnkb.MaBNhan, bnkb.NgayKham, bnkb.MaKP }
                            );
            var qTong = (from bn in data.BenhNhans
                         join bnkb in qTong1.Where(p => p.NgayKham >= _tungayNamTruoc && p.NgayKham <= denngayHT) on bn.MaBNhan equals bnkb.MaBNhan
                          select new { bn, bnkb.NgayKham, bnkb.MaKP }
                            ).ToList();
            var benhnhan = (from bn in qTong
                            where (bn.NgayKham >= tungayHT && bn.NgayKham <= denngayHT)
                            join kp in _lkp on bn.MaKP equals kp.MaKP
                            where (DungChung.Bien.MaBV.Equals("30012") ? (kp.PLoai == "Phòng khám") : kp.PLoai == "Phòng khám")
                            select bn.bn).ToList();
            var benhNhanNamNgoai = (from bn in qTong
                                    where (bn.NgayKham >= _tungayNamTruoc && bn.NgayKham <= _denngayNamTruoc)
                                    join kp in _lkp on bn.MaKP equals kp.MaKP
                                    where (DungChung.Bien.MaBV.Equals("30012") ? (kp.PLoai == "Phòng khám") : kp.PLoai == "Phòng khám")
                                    select bn.bn).ToList();
            var qtongRV = (from bn in data.BenhNhans
                           join rv in data.RaViens.Where(p => p.NgayRa >= _tungayNamTruoc && p.NgayRa <= denngayHT) on bn.MaBNhan equals rv.MaBNhan
                           select new { bn, rv });
            var benhnhanRV = (from bn in qtongRV
                              where (bn.rv.NgayRa >= tungayHT && bn.rv.NgayRa <= denngayHT)
                              select new { bn.bn, bn.rv }).ToList();
            var benhnhanRVnamNgoai = (from bn in qtongRV
                                      where (bn.rv.NgayRa >= _tungayNamTruoc && bn.rv.NgayRa <= _denngayNamTruoc)
                                      select new { bn.bn, bn.rv }).ToList();
            var bnCoThe = benhnhan.Where(p => p.SThe != null && p.SThe != "").ToList();
            var bnCoTheNoiTru = benhnhanRV.Where(p => p.bn.SThe != null && p.bn.SThe != "").Where(p => p.bn.NoiTru == 1).ToList();
            var bnThanhToanTT = benhnhan.Where(p => p.SThe == null || p.SThe == "").ToList();
            var bnThanhToanTTNoiTru = benhnhanRV.Where(p => p.bn.SThe == null || p.bn.SThe == "").ToList();

            List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong> _da = new List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong>();
            List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong> _da1 = new List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong>();
            List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong> _da2 = new List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong>();
            List<KPhong> _lKhoaP = new List<KPhong>();
            _lKhoaP = data.KPhongs.ToList();
            foreach (var item in data.KPhongs.ToList())
            {
                _da = frm_NhapBuongGiuongKeKoach.getBuongGiuong(data, item.MaKP, Convert.ToString(nam1));
                foreach (var item1 in _da)
                {
                    _da1.Add(item1);
                }
            }
            foreach (var item in data.KPhongs.ToList())
            {
                _da = frm_NhapBuongGiuongKeKoach.getBuongGiuong(data, item.MaKP, Convert.ToString(namtruoc));
                foreach (var item1 in _da)
                {
                    _da2.Add(item1);
                }
            }
            var n2 = (from a in _da2 group a by new { a.giuongKH, a.makp } into kq select new { kq.Key.makp, kq.Key.giuongKH }).ToList();
            var n1 = (from a in _da1 group a by new { a.giuongKH, a.makp } into kq select new { kq.Key.makp, kq.Key.giuongKH }).ToList();


            #endregion

            #region Hạng BV
            var hangBV = _lbenhvien.Where(p => p.MaBV == DungChung.Bien.MaBV).FirstOrDefault();
            int tuyenBV = -1;
            if (hangBV.TuyenBV.Contains("A"))
                tuyenBV = 1;
            if (hangBV.TuyenBV.Contains("B"))
                tuyenBV = 2;
            if (hangBV.TuyenBV.Contains("C"))
                tuyenBV = 3;
            if (hangBV.TuyenBV.Contains("D"))
                tuyenBV = 4;
            #endregion


            #region 1.Số giường kế hoạch
            moi = new Content();
            moi.Stt = 1;
            moi.TenNhom = "1. Số giường kế hoạch";
            moi.TenChiTiet = "";
            moi.KH2016 = n1.Sum(p => Convert.ToInt32(p.giuongKH));
            //moi.KQ6Thang2015 = n2.Sum(p => Convert.ToInt32(p.giuongKH));
            //moi.KQ6Thang2016 = n1.Sum(p => Convert.ToInt32(p.giuongKH));
            //moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            //moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);
            #endregion

            #region 2. Số giường thực kê
            moi = new Content();
            moi.Stt = 2;
            moi.TenNhom = "2. Giường thực kê";
            moi.TenChiTiet = "";
            moi.KQ6Thang2015 = _da2.Count();
            moi.KQ6Thang2016 = _da1.Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            moi.Tile6Thang2016SoVoiKH2016 = (n1.Sum(p => Convert.ToInt32(p.giuongKH)) != 0) ? moi.KQ6Thang2016 / n1.Sum(p => Convert.ToInt32(p.giuongKH)) * 100 : null;
            moi.Tile6Thang2016SoVoiKH2016 = Math.Round(moi.Tile6Thang2016SoVoiKH2016.GetValueOrDefault(), 2);
            _listContent.Add(moi);
            #endregion

            #region 3.Công suất sử dụng giường
            moi = new Content();
            moi.Stt = 3;
            moi.TenNhom = "3. Công suất sử dụng giường bệnh";
            moi.TenChiTiet = "";
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 3;
            moi.TenNhom = "3. Công suất sử dụng giường bệnh";
            moi.TenChiTiet = "3a. Tính theo giường bệnh kế hoạch";
            moi.KH2016 = 100;
            moi.KQ6Thang2015 = (n2.Sum(p => Convert.ToInt32(p.giuongKH)) * ngay != 0) ? (double)(benhnhanRVnamNgoai.Where(p => p.bn.NoiTru == 1).Sum(p => p.rv.SoNgaydt) * 100 / (n2.Sum(p => Convert.ToInt32(p.giuongKH)) * ngay)) : 0.0;
            moi.KQ6Thang2016 = (n1.Sum(p => Convert.ToInt32(p.giuongKH)) * ngay != 0) ? (double)(benhnhanRV.Where(p => p.bn.NoiTru == 1).Sum(p => p.rv.SoNgaydt) * 100 / (n1.Sum(p => Convert.ToInt32(p.giuongKH)) * ngay)) : 0.0;
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            //moi.Tile6Thang2016SoVoiKH2016 = (n1.Sum(p => Convert.ToInt32(p.giuongKH)) != 0) ? moi.KQ6Thang2016 / n1.Sum(p => Convert.ToInt32(p.giuongKH)) * 100 : null;
            //moi.Tile6Thang2016SoVoiKH2016 = Math.Round(moi.Tile6Thang2016SoVoiKH2016.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 3;
            moi.TenNhom = "3. Công suất sử dụng giường bệnh";
            moi.TenChiTiet = "3a. Tính theo giường bệnh thực kê";
            moi.KH2016 = 100;
            moi.KQ6Thang2015 = (_da2.Count() * ngay != 0) ? (double)(benhnhanRVnamNgoai.Where(p => p.bn.NoiTru == 1).Sum(p => p.rv.SoNgaydt) * 100 / (_da2.Count() * ngay)) : 0.0;
            moi.KQ6Thang2016 = (_da1.Count() * ngay != 0) ? (double)(benhnhanRV.Where(p => p.bn.NoiTru == 1).Sum(p => p.rv.SoNgaydt) * 100 / (_da1.Count() * ngay)) : 0.0;
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);
            #endregion

            #region 4.Tổng số lượt khám bệnh
            moi = new Content();
            moi.Stt = 4;
            moi.TenNhom = "4. Tổng số lượt khám bệnh(Tất cả các đối tượng = 4a + 4b + 4c + 4d + 4đ):";
            moi.TenChiTiet = "";
            //moi.KH2016 = bnCoThe.Count;//????
            moi.KQ6Thang2015 = benhNhanNamNgoai.Where(p => p.DTuong != "KSK").Count();
            moi.KQ6Thang2016 = benhnhan.Where(p => p.DTuong != "KSK").Count();
            //moi.Tile6Thang2016SoVoiKH2016 = (double)moi.KQ6Thang2016 / moi.KH2016 * 100;
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 4;
            moi.TenNhom = "4. Tổng số lượt khám bệnh(4 = 4a + 4b + 4c + 4d): ";
            moi.TenChiTiet = "4a. Tổng số lượt khám bệnh thu phí trực tiếp";
            moi.KQ6Thang2015 = benhNhanNamNgoai.Where(p => p.DTuong != "KSK").Where(p => p.SThe == "").Count();
            moi.KQ6Thang2016 = benhnhan.Where(p => p.DTuong != "KSK").Where(p => p.SThe == "").Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 4;
            moi.TenNhom = "4. Tổng số lượt khám bệnh(Tất cả các đối tượng = 4a + 4b + 4c + 4d): ";
            moi.TenChiTiet = "4b. Tổng số lượt khám bệnh cho người được BHYT chi trả (tất cả các đối tượng có thẻ BHYT)";
            //moi.KH2016 = bnCoThe.Count;//????
            moi.KQ6Thang2015 = benhNhanNamNgoai.Where(p => p.DTuong != "KSK").Where(p => p.SThe != "" && p.MaDTuong != "HN" && p.MaDTuong != "CN").Count();
            moi.KQ6Thang2016 = benhnhan.Where(p => p.DTuong != "KSK").Where(p => p.SThe != "" && p.MaDTuong != "HN" && p.MaDTuong != "CN").Count();
            //moi.Tile6Thang2016SoVoiKH2016 = (moi.KH2016 != 0) ? (double)moi.KQ6Thang2016 / moi.KH2016 * 100 : null;
            //moi.Tile6Thang2016SoVoiKH2016 = Math.Round(moi.Tile6Thang2016SoVoiKH2016.GetValueOrDefault(), 2);
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 4;
            moi.TenNhom = "4. Tổng số lượt khám bệnh(Tất cả các đối tượng = 4a + 4b + 4c + 4d): ";
            moi.TenChiTiet = "4c. Tổng số lượt khám bệnh cho người nghèo (Không sử dụng thẻ BHYT nhưng vẫn được quyết toán theo thực thanh thực chi)";
            moi.KQ6Thang2015 = benhNhanNamNgoai.Where(p => p.DTuong != "KSK").Where(p => p.MaDTuong == "HN").Count();
            moi.KQ6Thang2016 = benhnhan.Where(p => p.DTuong != "KSK").Where(p => p.MaDTuong == "HN").Count();
            //moi.Tile6Thang2016SoVoi2015 = (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100;
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 4;
            moi.TenNhom = "4. Tổng số lượt khám bệnh(Tất cả các đối tượng = 4a + 4b + 4c + 4d): ";
            moi.TenChiTiet = "4d. Tổng số lượt khám viện phí cho các đối tượng (cận nghèo, khó khăn...) do BV quyết định";
            //moi.KH2016 = bnCoThe.Where(p => p.MaDTuong == "CN").Count();//????
            moi.KQ6Thang2015 = benhNhanNamNgoai.Where(p => p.DTuong != "KSK").Where(p => p.MaDTuong == "CN").Count();
            moi.KQ6Thang2016 = benhnhan.Where(p => p.DTuong != "KSK").Where(p => p.MaDTuong == "CN").Count();
            moi.Tile6Thang2016SoVoiKH2016 = (moi.KH2016 != 0) ? (double)moi.KQ6Thang2016 / moi.KH2016 * 100 : null;
            moi.Tile6Thang2016SoVoiKH2016 = Math.Round(moi.Tile6Thang2016SoVoiKH2016.GetValueOrDefault(), 2);
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            _listContent.Add(new Content { Stt = 4, TenNhom = "4. Tổng số lượt khám bệnh(Tất cả các đối tượng = 4a + 4b + 4c + 4d): " + Environment.NewLine + " Trong đó:", TenChiTiet = "4e. Tổng số lượt khám giảm viện phí do Bv quyết định" });
            #endregion

            #region 5.Tổng số lượt khám bệnh cho trẻ em dưới 6 tuổi
            var q5_2 = bnThanhToanTT.Where(p => p.Tuoi == null || p.Tuoi <= 6).ToList();
            moi = new Content();
            moi.Stt = 5;
            moi.TenNhom = "5. Tổng số khám chữa bệnh trẻ em dưới 6 tuổi";
            moi.TenChiTiet = "";
            //moi.KH2016 = q5_2.Count;//?????
            moi.KQ6Thang2015 = benhNhanNamNgoai.Where(p => p.Tuoi == null || p.Tuoi <= 6).Count();
            moi.KQ6Thang2016 = benhnhan.Where(p => p.Tuoi == null || p.Tuoi <= 6).Count();
            //moi.Tile6Thang2016SoVoiKH2016 = (double)moi.KQ6Thang2016 / moi.KH2016 * 100;
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 5;
            moi.TenNhom = "5. Tổng số khám chữa bệnh trẻ em dưới 6 tuổi";
            moi.TenChiTiet = "5a. Số trẻ dưới 6 tuổi thu phí trực tiếp";
            //moi.KH2016 = q5_2.Count;//?????
            moi.KQ6Thang2015 = benhNhanNamNgoai.Where(p => p.SThe == "").Where(p => p.Tuoi == null || p.Tuoi <= 6).Count();
            moi.KQ6Thang2016 = benhnhan.Where(p => p.SThe == "").Where(p => p.Tuoi == null || p.Tuoi <= 6).Count();
            //moi.Tile6Thang2016SoVoiKH2016 = (moi.KH2016 != 0) ? (double)moi.KQ6Thang2016 / moi.KH2016 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 5;
            moi.TenNhom = "5. Tổng số khám chữa bệnh trẻ em dưới 6 tuổi";
            moi.TenChiTiet = "5b. Số trẻ dưới 6 tuổi có thẻ BHYT, hoặc thẻ khám chữa bệnh cho trẻ em dưới 6 tuổi:";
            moi.KQ6Thang2015 = benhNhanNamNgoai.Where(p => p.SThe.Contains("TE")).Where(p => p.Tuoi == null || p.Tuoi <= 6).Count();
            moi.KQ6Thang2016 = benhnhan.Where(p => p.SThe.Contains("TE")).Where(p => p.Tuoi == null || p.Tuoi <= 6).Count();
            moi.Tile6Thang2016SoVoiKH2016 = (moi.KH2016 != 0) ? (double)moi.KQ6Thang2016 / moi.KH2016 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            moi.Tile6Thang2016SoVoiKH2016 = Math.Round(moi.Tile6Thang2016SoVoiKH2016.GetValueOrDefault(), 2);
            _listContent.Add(moi);
            #endregion

            #region 6.Tổng số lượt khám chữa bệnh cho người bệnh cao tuổi >=60
            moi = new Content();
            moi.Stt = 6;
            moi.TenNhom = "6. Tổng số khám cho người bệnh cao tuổi >= 60 tuổi (tất cả các đối tượng): ";
            moi.TenChiTiet = "";
            moi.KQ6Thang2015 = benhNhanNamNgoai.Where(p => p.Tuoi == null || p.Tuoi >= 60).Count();
            moi.KQ6Thang2016 = benhnhan.Where(p => p.Tuoi == null || p.Tuoi >= 60).Count();
            //moi.Tile6Thang2016SoVoiKH2016 = (double)moi.KQ6Thang2016 / moi.KH2016 * 100;
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 6;
            moi.TenNhom = "6. Tổng số khám cho người bệnh cao tuổi >= 60 tuổi (tất cả các đối tượng): ";
            moi.TenChiTiet = "6a. Số lượt khám bệnh cho người cao tuổi có thẻ BHYT, hoặc đối tượng chính sách khác được miễn viện phí";
            moi.KQ6Thang2015 = benhNhanNamNgoai.Where(p => p.SThe != "").Where(p => p.Tuoi == null || p.Tuoi >= 60).Count();
            moi.KQ6Thang2016 = benhnhan.Where(p => p.SThe != "").Where(p => p.Tuoi == null || p.Tuoi >= 60).Count();
            moi.Tile6Thang2016SoVoiKH2016 = (moi.KH2016 != 0) ? (double)moi.KQ6Thang2016 / moi.KH2016 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            var q6_2 = bnThanhToanTT.Where(p => p.Tuoi >= 60).ToList();
            moi = new Content();
            moi.Stt = 6;
            moi.TenNhom = "6. Tổng số khám cho người bệnh cao tuổi >= 60 tuổi (tất cả các đối tượng): ";
            moi.TenChiTiet = "6b. Số lượt khám bệnh cho người cao tuổi thu phí trực tiếp";
            moi.KQ6Thang2015 = benhNhanNamNgoai.Where(p => p.SThe == "").Where(p => p.Tuoi == null || p.Tuoi >= 60).Count();
            moi.KQ6Thang2016 = benhnhan.Where(p => p.SThe == "").Where(p => p.Tuoi == null || p.Tuoi >= 60).Count();
            moi.Tile6Thang2016SoVoiKH2016 = (moi.KH2016 != 0) ? (double)moi.KQ6Thang2016 / moi.KH2016 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);
            #endregion

            #region 7.Tổng số lượt chuyển khám
            var q7_1 = (from a in benhnhanRV
                        join bv in _lbenhvien on a.rv.MaBVC equals bv.MaBV
                        select new
                        {
                            a.rv.Status,
                            a.rv.MaBVC,
                            bv.TuyenBV,
                            a.bn.NoiTru,
                            HangBV = (bv.TuyenBV.Contains("A")) ? bv.HangBV = 1 : (bv.TuyenBV.Contains("B")) ? bv.HangBV = 2 : (bv.TuyenBV.Contains("C")) ? bv.HangBV = 3 : (bv.TuyenBV.Contains("D")) ? bv.HangBV = 4 : null
                        }).Where(p => p.Status == 1).ToList();
            var q7_2 = (from b in benhnhanRVnamNgoai
                        join bv in _lbenhvien on b.rv.MaBVC equals bv.MaBV
                        select new
                        {
                            b.rv.Status,
                            b.rv.MaBVC,
                            bv.TuyenBV,
                            b.bn.NoiTru,
                            HangBV = (bv.TuyenBV.Contains("A")) ? bv.HangBV = 1 : (bv.TuyenBV.Contains("B")) ? bv.HangBV = 2 : (bv.TuyenBV.Contains("C")) ? bv.HangBV = 3 : (bv.TuyenBV.Contains("D")) ? bv.HangBV = 4 : null
                        }).Where(p => p.Status == 1).ToList();
            moi = new Content();
            moi.Stt = 7;
            moi.TenNhom = "7. Tổng số lượt chuyển khám ";
            moi.TenChiTiet = "";
            moi.KQ6Thang2016 = q7_1.Count;
            moi.KQ6Thang2015 = q7_2.Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 7;
            moi.TenNhom = "7. Tổng số lượt chuyển khám ";
            moi.TenChiTiet = "7a. Chuyển khám BV tuyến trên";
            moi.KQ6Thang2016 = q7_1.Where(p => p.HangBV < tuyenBV).Count();
            moi.KQ6Thang2015 = q7_2.Where(p => p.HangBV < tuyenBV).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 7;
            moi.TenNhom = "7. Tổng số lượt chuyển khám ";
            moi.TenChiTiet = "7b. Chuyển khám BV chuyên khoa ( do không thuộc chức năng nhiệm vụ của BV)";
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 7;
            moi.TenNhom = "7. Tổng số lượt chuyển khám ";
            moi.TenChiTiet = "7c. Chuyển khám vì lý do khác (không thuộc 2 TH trên)";
            moi.KQ6Thang2016 = q7_1.Count - q7_1.Where(p => p.HangBV < tuyenBV).Count();
            moi.KQ6Thang2015 = q7_2.Count - q7_2.Where(p => p.HangBV < tuyenBV).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);
            #endregion

            List<int> lMabnvaovien = data.VaoViens.Where(p => p.MaBNhan != null).Select(p => p.MaBNhan).ToList();

            #region 8. Tổng số lượt người điều trị ngoại trú
            var q8_1 = (from a in benhnhan.Where(p => p.NoiTru == 0 && p.DTNT == true)
                        join vv in lMabnvaovien on a.MaBNhan equals vv
                        join rv in _lclRavien on a.MaBNhan equals rv.maBN
                        group new { a, rv } by new { a.MaBNhan, rv.SoNgayDT } into kq
                        select new { kq.Key.MaBNhan, SoNgaydt = kq.Key.SoNgayDT }).ToList();

            var q8_2 = (from a in benhNhanNamNgoai.Where(p => p.NoiTru == 0 && p.DTNT == true)
                        join vv in lMabnvaovien on a.MaBNhan equals vv
                        join rv in _lclRavien on a.MaBNhan equals rv.maBN
                        group new { a, rv } by new { a.MaBNhan, rv.SoNgayDT } into kq
                        select new { kq.Key.MaBNhan, SoNgaydt = kq.Key.SoNgayDT }).ToList();

            var BNMantinhBH1 = (from a in benhnhan.Where(p => p.NoiTru == 0 && p.DTNT == false).Where(p => p.DTuong == "BHYT")
                                join b in data.BNManTinhs on a.SThe equals b.STheSoCMT
                                select new { a.MaBNhan }).Distinct().ToList();
            var BNManTinhDV3 = (from a in benhnhan.Where(p => p.NoiTru == 0 && p.DTNT == false).Where(p => p.DTuong == "Dịch vụ")
                                join ttbx in data.TTboXungs on a.MaBNhan equals ttbx.MaBNhan
                                select new { a.MaBNhan,ttbx.CMT }).Distinct();

            var BNManTinhDV1 = (from a in BNManTinhDV3
                                join b in data.BNManTinhs on a.CMT equals b.STheSoCMT
                                select new { a.MaBNhan }).Distinct().ToList();

            var BNMantinhBH2 = (from a in benhNhanNamNgoai.Where(p => p.NoiTru == 0 && p.DTNT == false).Where(p => p.DTuong == "BHYT")
                                join b in data.BNManTinhs on a.SThe equals b.STheSoCMT
                                select new { a.MaBNhan }).Distinct().ToList();
            var BNManTinhDV2 = (from a in benhNhanNamNgoai.Where(p => p.NoiTru == 0 && p.DTNT == false).Where(p => p.DTuong == "Dịch vụ")
                                join ttbx in data.TTboXungs on a.MaBNhan equals ttbx.MaBNhan
                                join b in data.BNManTinhs on ttbx.CMT equals b.STheSoCMT
                                select new { a.MaBNhan }).Distinct().ToList();
            moi = new Content();
            moi.Stt = 8;
            moi.TenNhom = "8. Tổng số lượt người bệnh điều trị ngoại trú";
            moi.TenChiTiet = "";
            //moi.KH2016
            moi.KQ6Thang2015 = q8_2.Count() + BNMantinhBH2.Count() + BNManTinhDV2.Count();
            moi.KQ6Thang2016 = q8_1.Count() + BNMantinhBH1.Count() + BNManTinhDV1.Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);
            #endregion

            #region 9. Tổng số ngày người bệnh điều trị ngoại trú
            moi = new Content();
            moi.Stt = 9;
            moi.TenNhom = "9. Tổng số ngày người bệnh điều trị ngoại trú";
            moi.TenChiTiet = "";
            moi.KQ6Thang2015 = q8_2.Sum(p => p.SoNgaydt);
            moi.KQ6Thang2016 = q8_1.Sum(p => p.SoNgaydt);
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);
            #endregion

            #region 10. Tổng số lượt người bệnh nội trú
            var qtongvienphi = (from vp in data.VienPhis
                                join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                                select new vpctP
                                {
                                    MaBNhan = vp.MaBNhan,
                                    Mien = vpct.Mien
                                }).Distinct().ToList();
            //var qtongvienphi = (from vp in data.VienPhis
            //                    join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
            //                    select new 
            //                    {
            //                        vp.MaBNhan,
            //                        vpct.Mien
            //                    }).ToList();
            
            var q10_a = (from n in benhnhanRV.Where(o => o.bn.NoiTru == 1 && o.bn.DTNT == false)
                         join vp in qtongvienphi on n.bn.MaBNhan equals vp.MaBNhan
                         select new
                         {
                             n.bn.MaBNhan,
                             n.bn.NoiTru,
                             n.rv.KetQua,
                             n.rv.Status,
                             vp.Mien,
                             n.bn.DTNT,
                             SThe = n.bn.SThe == null ? "" : n.bn.SThe,
                             n.bn.MaDTuong
                         }).ToList();
            var q10_b = (from n in benhnhanRVnamNgoai.Where(o => o.bn.NoiTru == 1 && o.bn.DTNT == false)
                         join vp in qtongvienphi on n.bn.MaBNhan equals vp.MaBNhan
                         select new
                         {
                             n.bn.MaBNhan,
                             n.bn.NoiTru,
                             n.rv.KetQua,
                             n.rv.Status,
                             vp.Mien,
                             n.bn.DTNT,
                             SThe = n.bn.SThe == null ? "" : n.bn.SThe,
                             n.bn.MaDTuong
                         }).ToList();

            moi = new Content();
            moi.Stt = 10;
            moi.TenNhom = "10. Tổng số lượt người bệnh nội trú, tất cả các đối tượng (10 = 10a + 10b + 10c + 10d):";
            moi.TenChiTiet = "";
            moi.KQ6Thang2015 = q10_b.Select(p => p.MaBNhan).Distinct().Count();
            moi.KQ6Thang2016 = q10_a.Select(p => p.MaBNhan).Distinct().Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 10;
            moi.TenNhom = "10. Tổng số lượt người bệnh nội trú, tất cả các đối tượng (10 = 10a + 10b + 10c + 10d):";
            moi.TenChiTiet = "10a. Tổng số lượt điều trị nội trú thu viện phí trực tiếp.";
            moi.KQ6Thang2015 = q10_b.Where(p => p.SThe == "").Select(p => p.MaBNhan).Distinct().Count();
            moi.KQ6Thang2016 = q10_a.Where(p => p.SThe == "").Select(p => p.MaBNhan).Distinct().Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 10;
            moi.TenNhom = "10. Tổng số lượt người bệnh nội trú, tất cả các đối tượng (10 = 10a + 10b + 10c + 10d):";
            moi.TenChiTiet = "10b. Tổng số lượt người điều trị nội trú được BHYT chi trả ( các đối tượng có thẻ BHYT)";
            moi.KQ6Thang2015 = q10_b.Where(p => p.SThe != "").Where(p => !p.MaDTuong.Equals("HN")).Select(p => p.MaBNhan).Distinct().Count();
            moi.KQ6Thang2016 = q10_a.Where(p => p.SThe != "").Where(p => !p.MaDTuong.Equals("HN")).Select(p => p.MaBNhan).Distinct().Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 10;
            moi.TenNhom = "10. Tổng số lượt người bệnh nội trú, tất cả các đối tượng (10 = 10a + 10b + 10c + 10d):";
            moi.TenChiTiet = "10c. Tổng số lượt người điều trị cho người nghèo (không có thẻ BHYT, hoặc có thẻ khám chữa bệnh cho người nghèo được quyết toán theo thực thanh thực chi)";
            moi.KQ6Thang2015 = q10_b.Where(p => p.MaDTuong == "HN").Select(p => p.MaBNhan).Distinct().Count();
            moi.KQ6Thang2016 = q10_a.Where(p => p.MaDTuong == "HN").Select(p => p.MaBNhan).Distinct().Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 10;
            moi.TenNhom = "10. Tổng số lượt người bệnh nội trú, tất cả các đối tượng (10 = 10a + 10b + 10c + 10d):";
            moi.TenChiTiet = "10d. Tổng số lượt người bệnh điều trị nội trú được miễn viện phí do BV quyết định";
            moi.KQ6Thang2015 = q10_b.Where(p => p.Mien == 100).Select(p => p.MaBNhan).Distinct().Count();
            moi.KQ6Thang2016 = q10_a.Where(p => p.Mien == 100).Select(p => p.MaBNhan).Distinct().Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 10;
            moi.TenNhom = "10. Tổng số lượt người bệnh nội trú, tất cả các đối tượng (10 = 10a + 10b + 10c + 10d):";
            moi.TenChiTiet = "10đ. Tổng số lượt người điều trị nội trú được giảm do BV quyết định";
            moi.KQ6Thang2015 = q10_b.Where(p => p.Mien > 0 && p.Mien < 100).Select(p => p.MaBNhan).Distinct().Count();
            moi.KQ6Thang2016 = q10_a.Where(p => p.Mien > 0 && p.Mien < 100).Select(p => p.MaBNhan).Distinct().Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);
            #endregion

            #region 11. Tổng số lượt điều trị nội trú bằng YHCT, hoặc có kết hợp YHCT
            var kpdongy = (from kp in data.KPhongs join ck in data.ChuyenKhoas.Where(p => p.TenCK == "Đông y") on kp.MaCK equals ck.MaCK select kp).ToList();
            var q11 = (from bn in benhnhanRV.Where(p => p.bn.NoiTru == 1)
                       join bnkb in qTong on bn.bn.MaBNhan equals bnkb.bn.MaBNhan
                       join kp in kpdongy on bnkb.MaKP equals kp.MaKP
                       select bn).Distinct().ToList();
            var q11_1 = (from bn in benhnhanRVnamNgoai.Where(p => p.bn.NoiTru == 1) join bnkb in qTong on bn.bn.MaBNhan equals bnkb.bn.MaBNhan join kp in kpdongy on bnkb.MaKP equals kp.MaKP select bn).Distinct().ToList();
            moi = new Content();
            moi.Stt = 11;
            moi.TenNhom = "11. Tổng số lượt người bệnh điều trị nội trú bằng YHCT, hoặc có kết hợp YHCT";
            moi.TenChiTiet = "";
            moi.KQ6Thang2015 = q11_1.Count();
            moi.KQ6Thang2016 = q11.Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);
            #endregion

            #region 12. Tổng số lượt trẻ em dưới 6 tuổi điều trị nội trú
            //var q12_1 = (from a in benhnhan.Where(p => p.NoiTru == 1)
            //             join vv in data.VaoViens on a.MaBNhan equals vv.MaBNhan
            //             join rv in data.RaViens on a.MaBNhan equals rv.MaBNhan
            //             select new { a.MaBNhan, rv.SoNgaydt, a.SThe, a.Tuoi, a.DTuong, rv.KetQua }).ToList();

            //var q12_2 = (from a in benhNhanNamNgoai.Where(p => p.NoiTru == 1)
            //             join vv in data.VaoViens on a.MaBNhan equals vv.MaBNhan
            //             join rv in data.RaViens on a.MaBNhan equals rv.MaBNhan
            //             select new { a.MaBNhan, rv.SoNgaydt, a.SThe, a.Tuoi, a.DTuong, rv.KetQua }).ToList();
            moi = new Content();
            moi.Stt = 12;
            moi.TenNhom = "12. Tổng số lượt trẻ em dưới 6 tuổi điều trị nội trú";
            moi.TenChiTiet = "";
            moi.KQ6Thang2015 = benhnhanRVnamNgoai.Where(p => p.bn.NoiTru == 1).Where(p => p.bn.Tuoi < 6).Count();
            moi.KQ6Thang2016 = benhnhanRV.Where(p => p.bn.NoiTru == 1).Where(p => p.bn.Tuoi < 6).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            //var q12_2 = bnThanhToanTTNoiTru.Where(p => p.bn.Tuoi == null || p.bn.Tuoi < 6).ToList();
            moi = new Content();
            moi.Stt = 12;
            moi.TenNhom = "12. Tổng số lượt trẻ em dưới 6 tuổi điều trị nội trú";
            moi.TenChiTiet = "12a. Số lượt điều trị cho trẻ em dưới 6 tuổi thu phí trực tiếp";
            moi.KQ6Thang2015 = benhnhanRVnamNgoai.Where(p => p.bn.NoiTru == 1).Where(p => p.bn.DTuong.ToLower().Contains("dịch vụ")).Where(p => p.bn.Tuoi < 6).Count();
            moi.KQ6Thang2016 = benhnhanRV.Where(p => p.bn.NoiTru == 1).Where(p => p.bn.DTuong.ToLower().Contains("dịch vụ")).Where(p => p.bn.Tuoi < 6).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            //var q12_1 = bnCoTheNoiTru.Where(p => p.bn.Tuoi == null || p.bn.Tuoi < 6).ToList();
            moi = new Content();
            moi.Stt = 12;
            moi.TenNhom = "12. Tổng số lượt trẻ em dưới 6 tuổi điều trị nội trú";
            moi.TenChiTiet = "12b. Số lượt điều trị cho trẻ em dưới 6 tuổi có thẻ BHYT, hoặc thẻ khám chữa bệnh cho trẻ em dưới 6 tuổi";
            moi.KQ6Thang2015 = benhnhanRVnamNgoai.Where(p => p.bn.NoiTru == 1).Where(p => p.bn.DTuong.ToUpper().Contains("BHYT")).Where(p => p.bn.Tuoi < 6).Count();
            moi.KQ6Thang2016 = benhnhanRV.Where(p => p.bn.NoiTru == 1).Where(p => p.bn.DTuong.ToUpper().Contains("BHYT")).Where(p => p.bn.Tuoi < 6).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);
            #endregion

            #region 13. Tổng số lượt điều trị nội trú cho người bệnh cao tuổi >=60 tuổi
            var q13_1 = bnCoTheNoiTru.Where(p => p.bn.Tuoi >= 60).ToList();
            moi = new Content();
            moi.Stt = 13;
            moi.TenNhom = "13. Tổng số lượt điều trị nội trú cho người bệnh cao tuổi >=60 tuổi";
            moi.TenChiTiet = "";
            moi.KQ6Thang2015 = benhnhanRVnamNgoai.Where(p => p.bn.NoiTru == 1).Where(p => p.bn.Tuoi >= 60).Count();
            moi.KQ6Thang2016 = benhnhanRV.Where(p => p.bn.NoiTru == 1).Where(p => p.bn.Tuoi >= 60).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 13;
            moi.TenNhom = "13. Tổng số lượt điều trị nội trú cho người bệnh cao tuổi >=60 tuổi";
            moi.TenChiTiet = "13a. Số lượt điều trị cho người cao tuổi có thẻ BHYT, hoặc thẻ chính sách khác được miễn giảm viện phí";
            moi.KQ6Thang2015 = benhnhanRVnamNgoai.Where(p => p.bn.NoiTru == 1).Where(p => p.bn.DTuong.ToUpper().Equals("BHYT")).Where(p => p.bn.Tuoi >= 60).Count();
            moi.KQ6Thang2016 = benhnhanRV.Where(p => p.bn.NoiTru == 1).Where(p => p.bn.DTuong.ToUpper().Equals("BHYT")).Where(p => p.bn.Tuoi >= 60).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            var q13_2 = bnThanhToanTTNoiTru.Where(p => p.bn.Tuoi >= 60).ToList();
            moi = new Content();
            moi.Stt = 13;
            moi.TenNhom = "13. Tổng số lượt điều trị nội trú cho người bệnh cao tuổi >=60 tuổi";
            moi.TenChiTiet = "13b. Số lượt điều trị người tuổi thu phí trực tiếp";
            moi.KQ6Thang2015 = benhnhanRVnamNgoai.Where(p => p.bn.NoiTru == 1).Where(p => p.bn.DTuong.ToLower().Contains("dịch vụ")).Where(p => p.bn.Tuoi >= 60).Count();
            moi.KQ6Thang2016 = benhnhanRV.Where(p => p.bn.NoiTru == 1).Where(p => p.bn.DTuong.ToLower().Contains("dịch vụ")).Where(p => p.bn.Tuoi >= 60).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);
            #endregion

            #region 14. Kết quả điều trị nội trú
            //var q14 = benhnhanRV.Where(p => p.bn.NoiTru == 1 && (p.rv.KetQua != null || p.rv.KetQua != "")).ToList();
            moi = new Content();
            moi.Stt = 14;
            moi.TenNhom = "14. Kết quả điều trị nội trú";
            moi.TenChiTiet = "";
            moi.KQ6Thang2015 = q10_b.Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count();
            moi.KQ6Thang2016 = q10_a.Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 14;
            moi.TenNhom = "14. Kết quả điều trị nội trú";
            moi.TenChiTiet = "14a. Số lượt người bệnh được điều trị khỏi";
            moi.KQ6Thang2015 = q10_b.Where(p => p.NoiTru == 1).Where(p => p.KetQua == "Khỏi").Select(p => p.MaBNhan).Distinct().Count();
            moi.KQ6Thang2016 = q10_a.Where(p => p.NoiTru == 1).Where(p => p.KetQua == "Khỏi").Select(p => p.MaBNhan).Distinct().Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 14;
            moi.TenNhom = "14. Kết quả điều trị nội trú";
            moi.TenChiTiet = "14b. Số lượt người bệnh đỡ/giảm";
            moi.KQ6Thang2015 = q10_b.Where(p => p.NoiTru == 1).Where(p => p.KetQua == "Đỡ|Giảm").Select(p => p.MaBNhan).Distinct().Count();
            moi.KQ6Thang2016 = q10_a.Where(p => p.NoiTru == 1).Where(p => p.KetQua == "Đỡ|Giảm").Select(p => p.MaBNhan).Distinct().Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 14;
            moi.TenNhom = "14. Kết quả điều trị nội trú";
            moi.TenChiTiet = "14c. Số lượt người bệnh kết quả điều trị không thay đổi";
            moi.KQ6Thang2015 = q10_b.Where(p => p.NoiTru == 1).Where(p => p.KetQua == "Không T.đổi").Select(p => p.MaBNhan).Distinct().Count();
            moi.KQ6Thang2016 = q10_a.Where(p => p.NoiTru == 1).Where(p => p.KetQua == "Không T.đổi").Select(p => p.MaBNhan).Distinct().Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 14;
            moi.TenNhom = "14. Kết quả điều trị nội trú";
            moi.TenChiTiet = "14d. Số lượt người bệnh nặng hơn";
            moi.KQ6Thang2015 = q10_b.Where(p => p.NoiTru == 1).Where(p => p.KetQua == "Nặng hơn" || (p.KetQua != "Nặng hơn" && p.KetQua != "Không T.đổi" && p.KetQua != "Tử vong" && p.KetQua != "Đỡ|Giảm" && p.KetQua != "Khỏi")).Select(p => p.MaBNhan).Distinct().Count();
            moi.KQ6Thang2016 = q10_a.Where(p => p.NoiTru == 1).Where(p => p.KetQua == "Nặng hơn" || (p.KetQua != "Nặng hơn" && p.KetQua != "Không T.đổi" && p.KetQua != "Tử vong" && p.KetQua != "Đỡ|Giảm" && p.KetQua != "Khỏi")).Select(p => p.MaBNhan).Distinct().Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 14;
            moi.TenNhom = "14. Kết quả điều trị nội trú";
            moi.TenChiTiet = "14e. Số lượt người bệnh tiên lượng tử vong gia đình xin về";
            moi.KQ6Thang2015 = q10_b.Where(p => p.NoiTru == 1).Where(p => p.KetQua == "Tử vong").Select(p => p.MaBNhan).Distinct().Count();
            moi.KQ6Thang2016 = q10_a.Where(p => p.NoiTru == 1).Where(p => p.KetQua == "Tử vong").Select(p => p.MaBNhan).Distinct().Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);
            #endregion

            #region 15.Tổng số điều trị nội trú chuyển viện
            //var q15_a = benhnhanRV.Where(p => p.rv.Status == 1).ToList();
            moi = new Content();
            moi.Stt = 15;
            moi.TenNhom = "15. Tổng số điều trị nội trú chuyển viện";
            moi.TenChiTiet = "";
            moi.KQ6Thang2016 = q7_1.Where(p => p.NoiTru == 1).Count();
            moi.KQ6Thang2015 = q7_2.Where(p => p.NoiTru == 1).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 15;
            moi.TenNhom = "15. Tổng số điều trị nội trú chuyển viện";
            moi.TenChiTiet = "15a. Chuyển bệnh viện tuyến trên";
            moi.KQ6Thang2016 = q7_1.Where(p => p.NoiTru == 1).Where(p => p.HangBV < tuyenBV).Count();
            moi.KQ6Thang2015 = q7_2.Where(p => p.NoiTru == 1).Where(p => p.HangBV < tuyenBV).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 15;
            moi.TenNhom = "15. Tổng số điều trị nội trú chuyển viện";
            moi.TenChiTiet = "15b. Chuyển bệnh viện chuyên khoa";
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 15;
            moi.TenNhom = "15. Tổng số điều trị nội trú chuyển viện";
            moi.TenChiTiet = "15c. Chuyển bệnh viện tuyến dưới";
            moi.KQ6Thang2016 = q7_1.Where(p => p.NoiTru == 1).Where(p => p.HangBV > tuyenBV).Count();
            moi.KQ6Thang2015 = q7_2.Where(p => p.NoiTru == 1).Where(p => p.HangBV > tuyenBV).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 15;
            moi.TenNhom = "15. Tổng số điều trị nội trú chuyển viện";
            moi.TenChiTiet = "15e. Chuyển viện khác (không thuộc 3 trường hợp trên";
            moi.KQ6Thang2016 = q7_1.Where(p => p.NoiTru == 1).Count() - q7_1.Where(p => p.NoiTru == 1).Where(p => p.HangBV < tuyenBV).Count() - q7_1.Where(p => p.NoiTru == 1).Where(p => p.HangBV > tuyenBV).Count();
            moi.KQ6Thang2015 = q7_2.Where(p => p.NoiTru == 1).Count() - q7_2.Where(p => p.NoiTru == 1).Where(p => p.HangBV < tuyenBV).Count() - q7_2.Where(p => p.NoiTru == 1).Where(p => p.HangBV > tuyenBV).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);
            #endregion

            #region 16. Tổng số ngày điều trị của người bệnh nội trú
            moi = new Content();
            moi.Stt = 16;
            moi.TenNhom = "16. Tổng số ngày điều trị của người bệnh nội trú";
            moi.TenChiTiet = "";
            //moi.KH2016
            moi.KQ6Thang2015 = benhnhanRVnamNgoai.Where(p => p.bn.NoiTru == 1).Sum(p => p.rv.SoNgaydt);
            moi.KQ6Thang2016 = benhnhanRV.Where(p => p.bn.NoiTru == 1).Sum(p => p.rv.SoNgaydt);
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);
            #endregion

            #region 17. Số ngày điều trị trung bình của người bệnh nội trú
            var q17_1 = benhnhanRV.Where(p => p.bn.NoiTru == 1).ToList();
            var q17_2 = benhnhanRVnamNgoai.Where(p => p.bn.NoiTru == 1).ToList();
            moi = new Content();
            moi.Stt = 17;
            moi.TenNhom = "17. Số ngày điều trị trung bình của người bệnh nội trú";
            moi.TenChiTiet = "";
            //moi.KH2016
            moi.KQ6Thang2015 = (q17_2.Count != 0) ? Math.Round((double)q17_2.Sum(p => p.rv.SoNgaydt) / q17_2.Count, 2) : 0;
            moi.KQ6Thang2016 = (q17_1.Count != 0) ? Math.Round((double)q17_1.Sum(p => p.rv.SoNgaydt) / q17_1.Count, 2) : 0;
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);
            #endregion

            #region 18. Tổng số người bệnh tử vong tại BV (18 = 18a + 18b)
            //var q18 = benhnhanRV.Where(p => p.rv.KetQua == "Tử vong").ToList();
            //var q18_1 = benhnhanRVnamNgoai.Where(p => p.rv.KetQua == "Tử vong").ToList();
            moi = new Content();
            moi.Stt = 18;
            moi.TenNhom = "18. Tổng số người bệnh tử vong tại BV (18 = 18a + 18b)";
            moi.TenChiTiet = "";
            moi.KQ6Thang2015 = benhnhanRVnamNgoai.Where(p => p.rv.KetQua == "Tử vong").Count();
            moi.KQ6Thang2016 = benhnhanRV.Where(p => p.rv.KetQua == "Tử vong").Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            _listContent.Add(new Content { Stt = 18, TenNhom = "18. Tổng số người bệnh tử vong tại BV (18 = 18a + 18b)", TenChiTiet = "18a. Số tử vong trong vòng 24 giờ đầu nhập viện" });
            _listContent.Add(new Content { Stt = 18, TenNhom = "18. Tổng số người bệnh tử vong tại BV (18 = 18a + 18b)", TenChiTiet = "18a. Số tử vong sau 24 giờ đầu nhập viện" });
            #endregion

            #region 19. Tổng số phẫu thuật thực hiện tại bệnh viện
            var q19_1 = (from bn in data.BenhNhans
                         join dt in data.DThuocs on bn.MaBNhan equals dt.MaBNhan
                         join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
                         select new { NgayTH = dtct.NgayNhap, bn.MaBNhan, bn.NoiTru, bn.DTNT, bn.DTuong, dtct.MaDV });
            var q19_2 = (from dv in data.DichVus
                         join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                         select new { tn.TenRG, dv.Loai, dv.TenDV, dv.MaDV });
            var q19 = (from q1 in q19_1
                       join q2 in q19_2 on q1.MaDV equals q2.MaDV
                       select new { q2.TenRG, q2.Loai, q1.NgayTH, q1.MaBNhan, q2.TenDV, q1.NoiTru, q1.DTNT, q1.DTuong });

            moi = new Content();
            moi.Stt = 19;
            moi.TenNhom = "19. Tổng số phẫu thuật thực hiện tại BV ( loại 3 trở lên = 19a + 19b + 19c + 19d)";
            moi.TenChiTiet = "";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "Phẫu Thuật" && (p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3)).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "Phẫu Thuật" && (p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3)).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 19;
            moi.TenNhom = "19. Tổng số phẫu thuật thực hiện tại BV ( loại 3 trở lên = 19a + 19b + 19c + 19d)";
            moi.TenChiTiet = "19a. Số phẫu thuật loại đặc biệt";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "Phẫu Thuật" && p.Loai == 0).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "Phẫu Thuật" && p.Loai == 0).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 19;
            moi.TenNhom = "19. Tổng số phẫu thuật thực hiện tại BV ( loại 3 trở lên = 19a + 19b + 19c + 19d)";
            moi.TenChiTiet = "19b. Số phẫu thuật loại 1";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "Phẫu Thuật" && p.Loai == 1).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "Phẫu Thuật" && p.Loai == 1).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 19;
            moi.TenNhom = "19. Tổng số phẫu thuật thực hiện tại BV ( loại 3 trở lên = 19a + 19b + 19c + 19d)";
            moi.TenChiTiet = "19c. Số phẫu thuật loại 2";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "Phẫu Thuật" && p.Loai == 2).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "Phẫu Thuật" && p.Loai == 2).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 19;
            moi.TenNhom = "19. Tổng số phẫu thuật thực hiện tại BV ( loại 3 trở lên = 19a + 19b + 19c + 19d)";
            moi.TenChiTiet = "19d. Số phẫu thuật loại 3";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "Phẫu Thuật" && p.Loai == 3).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "Phẫu Thuật" && p.Loai == 3).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);
            #endregion

            #region 20. Tổng số thủ thuật thực hiện tại BV
            moi = new Content();
            moi.Stt = 20;
            moi.TenNhom = "20. Tổng số thủ thuật thực hiện tại BV";
            moi.TenChiTiet = "";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "Thủ thuật" && (p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3)).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "Thủ thuật" && (p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3)).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 20;
            moi.TenNhom = "20. Tổng số thủ thuật thực hiện tại BV";
            moi.TenChiTiet = "20a. Số thủ thuật loại đặc biệt";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "Thủ thuật" && p.Loai == 0).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "Thủ thuật" && p.Loai == 0).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 20;
            moi.TenNhom = "20. Tổng số thủ thuật thực hiện tại BV";
            moi.TenChiTiet = "20b. Số thủ thuật loại 1";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "Thủ thuật" && p.Loai == 1).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "Thủ thuật" && p.Loai == 1).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 20;
            moi.TenNhom = "20. Tổng số thủ thuật thực hiện tại BV";
            moi.TenChiTiet = "20c. Số thủ thuật loại 2";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "Thủ thuật" && p.Loai == 2).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "Thủ thuật" && p.Loai == 2).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 20;
            moi.TenNhom = "20. Tổng số thủ thuật thực hiện tại BV";
            moi.TenChiTiet = "20d. Số thủ thuật loại 3";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "Thủ thuật" && p.Loai == 3).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "Thủ thuật" && p.Loai == 3).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);
            #endregion

            #region 21. Tổng số ca đẻ tại BV
            var bnDe = (from bn in data.BenhNhans
                        //join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                        join cls in data.CLS on bn.MaBNhan equals cls.MaBNhan
                        join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                        join dv in data.DichVus on cd.MaDV equals dv.MaDV
                        join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                        from kq1 in kq.DefaultIfEmpty()
                        select new
                        {
                            cls.NgayTH,
                            dv.TenDV,
                            KetQua = kq1 == null ? "" : kq1.KetQua
                        }).Where(p => p.TenDV.ToLower().Contains("đỡ đẻ") || p.TenDV.ToLower().Contains("phẫu thuật lấy thai")).ToList();
            moi = new Content();
            moi.Stt = 21;
            moi.TenNhom = "21. Tổng số ca đẻ BV";
            moi.TenChiTiet = "";
            moi.KQ6Thang2015 = bnDe.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc).Count();
            moi.KQ6Thang2016 = bnDe.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 21;
            moi.TenNhom = "21. Tổng số ca đẻ BV";
            moi.TenChiTiet = "21a. Số ca phẫu thuật lấy thai";
            moi.KQ6Thang2015 = bnDe.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenDV.ToLower().Contains("phẫu thuật lấy thai")).Count();
            moi.KQ6Thang2016 = bnDe.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenDV.ToLower().Contains("phẫu thuật lấy thai")).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 21;
            moi.TenNhom = "21. Tổng số ca đẻ tại BV";
            moi.TenChiTiet = "21b. Số ca tử vong mẹ";
            moi.KQ6Thang2015 = bnDe.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.KetQua == "Tử vong").Count();
            moi.KQ6Thang2016 = bnDe.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.KetQua == "Tử vong").Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 21;
            moi.TenNhom = "21. Tổng số ca đẻ tại BV";
            moi.TenChiTiet = "21c. Số ca tử vong trẻ sơ sinh";
            _listContent.Add(moi);
            #endregion

            #region 22. Tổng số lượng máu đã sử dụng tại BV (đơn vị tính = lít)
            var q22 = (from dt in data.DThuocs
                       join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
                       join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                       join tn in data.TieuNhomDVs.Where(p => p.TenRG == "Máu") on dv.IdTieuNhom equals tn.IdTieuNhom
                       group new { dt, dtct, dv, tn } by new { dt.IDDon, dt.NgayKe, dtct.DonVi, dtct.SoLuong, dv.TenDV } into kq
                       select new
                       {
                           kq.Key.TenDV,
                           kq.Key.DonVi,
                           kq.Key.SoLuong,
                           kq.Key.NgayKe,
                       }).Where(p => p.DonVi.ToLower().Contains("lit")).ToList();

            moi = new Content();
            moi.Stt = 22;
            moi.TenNhom = "22.Tổng số lượng máu đã sử dụng tại Bv (đơn vị tính = lít)";
            moi.TenChiTiet = "";
            moi.KQ6Thang2015 = q22.Where(p => p.NgayKe >= _tungayNamTruoc && p.NgayKe <= _denngayNamTruoc).Sum(p => p.SoLuong);
            moi.KQ6Thang2016 = q22.Where(p => p.NgayKe >= tungayHT && p.NgayKe <= denngayHT).Sum(p => p.SoLuong);
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 22;
            moi.TenNhom = "22.Tổng số lượng máu đã sử dụng tại Bv (đơn vị tính = lít)";
            moi.TenChiTiet = "22a. Số lượng máu tiếp nhận từ người hiến máu tình nguyện (đơn vị tính = lít)";
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 22;
            moi.TenNhom = "22.Tổng số lượng máu đã sử dụng tại Bv (đơn vị tính = lít)";
            moi.TenChiTiet = "22b. Số lượng máu tiếp nhận từ các trung tâm Huyết học truyền máu (đơn vị tính = lít)";
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 22;
            moi.TenNhom = "22.Tổng số lượng máu đã sử dụng tại Bv (đơn vị tính = lít)";
            moi.TenChiTiet = "22c. Số lượng máu tiếp nhận từ các nguồn khác (người nhà, tự thân, người cho máu .v.v.) (đơn vị tính = lít)";
            _listContent.Add(moi);
            #endregion

            #region 23. Tổng số xét nghiệm về Sinh hóa
            //var q23 = (from n in q19
            //           select new { n.Status, n.TenRG, n.Loai, n.NgayTH, n.MaBNhan, n.NoiTru, n.DTNT, n.TenDV, n.DTuong }).ToList();

            moi = new Content();
            moi.Stt = 23;
            moi.TenNhom = "23. Tổng số xét nghiệm về sinh hóa thực hiện tại Bv (23 = 23a + 23b + 23c)";
            moi.TenChiTiet = "";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "XN hóa sinh máu").Select(p => p.MaBNhan).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "XN hóa sinh máu").Select(p => p.MaBNhan).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 23;
            moi.TenNhom = "23. Tổng số xét nghiệm về sinh hóa thực hiện tại Bv (23 = 23a + 23b + 23c)";
            moi.TenChiTiet = "23a. Số XN sinh hóa cho người bệnh nội trú";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "XN hóa sinh máu" && p.NoiTru == 1 && p.DTNT == false).Select(p => p.MaBNhan).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "XN hóa sinh máu" && p.NoiTru == 1 && p.DTNT == false).Select(p => p.MaBNhan).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 23;
            moi.TenNhom = "23. Tổng số xét nghiệm về sinh hóa thực hiện tại Bv (23 = 23a + 23b + 23c)";
            moi.TenChiTiet = "23b. Số XN sinh hóa cho NB khám và điều trị ngoại trú";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "XN hóa sinh máu" && p.NoiTru == 0 && !p.DTuong.ToUpper().Contains("KSK")).Select(p => p.MaBNhan).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "XN hóa sinh máu" && p.NoiTru == 0 && !p.DTuong.ToUpper().Contains("KSK")).Select(p => p.MaBNhan).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 23;
            moi.TenNhom = "23. Tổng số xét nghiệm về sinh hóa thực hiện tại Bv (23 = 23a + 23b + 23c)";
            moi.TenChiTiet = "23c. Số XN sinh hóa phục vụ những đối tượng không khám, chữa bệnh tại Bv, Khám sức khỏe, NCKH.";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "XN hóa sinh máu" && p.DTuong.ToUpper().Contains("KSK")).Select(p => p.MaBNhan).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "XN hóa sinh máu" && p.DTuong.ToUpper().Contains("KSK")).Select(p => p.MaBNhan).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);
            #endregion

            #region 24. Tổng số xét nghiệm huyết học thực hiện tại BV
            moi = new Content();
            moi.Stt = 24;
            moi.TenNhom = "24. Tổng số xét nghiệm về Huyết học thực hiện tại BV (24 = 24a + 24b + 24c)";
            moi.TenChiTiet = "";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "XN huyết học").Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "XN huyết học").Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 24;
            moi.TenNhom = "24. Tổng số xét nghiệm về Huyết học thực hiện tại BV (24 = 24a + 24b + 24c)";
            moi.TenChiTiet = "24a. Số XN huyết học cho người bệnh nội trú";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "XN huyết học" && p.NoiTru == 1 && p.DTNT == false).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "XN huyết học" && p.NoiTru == 1 && p.DTNT == false).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 24;
            moi.TenNhom = "24. Tổng số xét nghiệm về Huyết học thực hiện tại BV (24 = 24a + 24b + 24c)";
            moi.TenChiTiet = "24b. Số XN huyết học cho NB khám và điều trị ngoại trú";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "XN huyết học" && p.NoiTru == 0 && !p.DTuong.ToUpper().Contains("KSK")).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "XN huyết học" && p.NoiTru == 0 && !p.DTuong.ToUpper().Contains("KSK")).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 24;
            moi.TenNhom = "24. Tổng số xét nghiệm về Huyết học thực hiện tại BV (24 = 24a + 24b + 24c)";
            moi.TenChiTiet = "24c. Số XN Huyết học phục vụ những đối tượng không khám, chữa bệnh tại BV, Khám sức khỏe, NCKH";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "XN huyết học" && p.DTuong.ToUpper().Contains("KSK")).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "XN huyết học" && p.DTuong.ToUpper().Contains("KSK")).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            #endregion

            #region 25. Tổng số xét nghiệm về Vi sinh thực hiện tại BV (25 = 25a + 25b + 25c)
            moi = new Content();
            moi.Stt = 25;
            moi.TenNhom = "25. Tổng số xét nghiệm về Vi sinh thực hiện tại BV (25 = 25a + 25b + 25c)";
            moi.TenChiTiet = "";
            // moi.SLNamSau = q21.Where(p => p.TenRG == "XN vi sinh").Count();// chưa có
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 25;
            moi.TenNhom = "25. Tổng số xét nghiệm về Vi sinh thực hiện tại BV (25 = 25a + 25b + 25c)";
            moi.TenChiTiet = "25a. Số XN về Vi sinh cho người bệnh nội trú";
            // moi.SLNamSau = q21.Where(p => p.TenRG == "XN vi sinh").Count();// chưa có
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh &&
                                         p.NoiTru == 1 && p.DTNT == false).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh &&
                                         p.NoiTru == 1 && p.DTNT == false).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 25;
            moi.TenNhom = "25. Tổng số xét nghiệm về Vi sinh thực hiện tại BV (25 = 25a + 25b + 25c)";
            moi.TenChiTiet = "25b. Số XN về Vi sinh cho người bệnh khám và điều trị ngoại trú";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh &&
                                         p.NoiTru == 0 && !p.DTuong.ToUpper().Contains("KSK")).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh &&
                                         p.NoiTru == 0 && !p.DTuong.ToUpper().Contains("KSK")).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 25;
            moi.TenNhom = "25. Tổng số xét nghiệm về Vi sinh thực hiện tại BV (25 = 25a + 25b + 25c)";
            moi.TenChiTiet = "25b. Số XN về Vi sinh cho những đối tượng không khám, chữa bệnh tại BV, Khám sức khỏe; NCKH.";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh && p.DTuong.ToUpper().Contains("KSK")).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh && p.DTuong.ToUpper().Contains("KSK")).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);
            #endregion

            #region 26. Tổng số xét nghiệm về Giải phẫu bệnh lý thực hiện tại BV (26 = 26a + 26b + 26c)
            moi = new Content();
            moi.Stt = 26;
            moi.TenNhom = "26. Tổng số xét nghiệm về Giải phẫu thực hiện tại BV (26 = 26a + 26b + 26c)";
            moi.TenChiTiet = "";
            // moi.SLNamSau = q21.Where(p => p.TenRG == "XN vi sinh").Count();// chưa có
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 26;
            moi.TenNhom = "26. Tổng số xét nghiệm về Giải phẫu thực hiện tại BV (26 = 26a + 26b + 26c)";
            moi.TenChiTiet = "26a. Số Giải phẫu bệnh lý cho người bệnh nội trú";
            // moi.SLNamSau = q21.Where(p => p.TenRG == "XN vi sinh").Count();// chưa có
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 26;
            moi.TenNhom = "26. Tổng số xét nghiệm về Giải phẫu thực hiện tại BV (26 = 26a + 26b + 26c)";
            moi.TenChiTiet = "26b. Số Giải phẫu bệnh lý cho người bệnh khám và điều trị ngoại trú";
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 26;
            moi.TenNhom = "26. Tổng số xét nghiệm về Giải phẫu thực hiện tại BV (26 = 26a + 26b + 26c)";
            moi.TenChiTiet = "26b. Số Giải phẫu bệnh lý cho những đối tượng không khám, chữa bệnh tại BV, Khám sức khỏe; NCKH.";
            _listContent.Add(moi);
            #endregion

            #region 27. Tổng số chụp X Quang (27 = 27a + 27b + 27c) mỗi vị trí được tính 1 film
            moi = new Content();
            moi.Stt = 27;
            moi.TenNhom = "27. Tổng số chụp X Quang (27 = 27a + 27b + 27c) mỗi vị trí được tính 1 film";
            moi.TenChiTiet = "";
            //moi.SLNamSau = q19.Where(p => p.TenRG == "X-Quang").Count();
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "X-Quang").Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "X-Quang").Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 27;
            moi.TenNhom = "27. Tổng số chụp X Quang (27 = 27a + 27b + 27c) mỗi vị trí được tính 1 film";
            moi.TenChiTiet = "27a. Số chụp XQ cho người bệnh nội trú";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "X-Quang" && p.NoiTru == 1 && p.DTNT == false).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "X-Quang" && p.NoiTru == 1 && p.DTNT == false).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 27;
            moi.TenNhom = "27. Tổng số chụp X Quang (27 = 27a + 27b + 27c) mỗi vị trí được tính 1 film";
            moi.TenChiTiet = "27b. Số chụp XQ cho người bệnh khám và ĐT ngoại trú";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "X-Quang" && p.NoiTru == 0 && !p.DTuong.ToUpper().Contains("KSK")).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "X-Quang" && p.NoiTru == 0 && !p.DTuong.ToUpper().Contains("KSK")).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 27;
            moi.TenNhom = "27. Tổng số chụp X Quang (27 = 27a + 27b + 27c) mỗi vị trí được tính 1 film";
            moi.TenChiTiet = "27c. Số chụp XQ phục vụ những đối tượng không khám, chữa bệnh tại BV, Khám sức khỏe; NCKH";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "X-Quang" && p.DTuong.ToUpper().Contains("KSK")).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "X-Quang" && p.DTuong.ToUpper().Contains("KSK")).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);
            #endregion

            #region 28. Tổng số chụp CT Scan (28 = 28a + 28b +28c)
            moi = new Content();
            moi.Stt = 28;
            moi.TenNhom = "28. Tổng số chụp CT Scan (28 = 28a + 28b +28c)";
            moi.TenChiTiet = "";
            //moi.SLNamSau = q19.Where(p => p.TenRG == "X-Quang CT").Count();
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "X-Quang" && (p.TenDV.ToLower().Contains("chụp clvt") || p.TenDV.ToLower().Contains("chụp cắt lớp vi tính"))).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "X-Quang" && (p.TenDV.ToLower().Contains("chụp clvt") || p.TenDV.ToLower().Contains("chụp cắt lớp vi tính"))).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 28;
            moi.TenNhom = "28. Tổng số chụp CT Scan (28 = 28a + 28b +28c)";
            moi.TenChiTiet = "28a. Số chụp CT Scan cho người bệnh nội trú";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "X-Quang" && p.NoiTru == 1 && p.DTNT == false && (p.TenDV.ToLower().Contains("chụp clvt") || p.TenDV.ToLower().Contains("chụp cắt lớp vi tính"))).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "X-Quang" && p.NoiTru == 1 && p.DTNT == false && (p.TenDV.ToLower().Contains("chụp clvt") || p.TenDV.ToLower().Contains("chụp cắt lớp vi tính"))).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 28;
            moi.TenNhom = "28. Tổng số chụp CT Scan (28 = 28a + 28b +28c)";
            moi.TenChiTiet = "28b. Số chụp CT Scan cho người bệnh khám và ĐT ngoại trú";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "X-Quang" && p.NoiTru == 0 && !p.DTuong.ToUpper().Contains("KSK") && (p.TenDV.ToLower().Contains("chụp clvt") || p.TenDV.ToLower().Contains("chụp cắt lớp vi tính"))).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "X-Quang" && p.NoiTru == 0 && !p.DTuong.ToUpper().Contains("KSK") && (p.TenDV.ToLower().Contains("chụp clvt") || p.TenDV.ToLower().Contains("chụp cắt lớp vi tính"))).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 28;
            moi.TenNhom = "28. Tổng số chụp CT Scan (28 = 28a + 28b +28c)";
            moi.TenChiTiet = "28c. Số chụp CT Scan phục vụ những đối tượng không khám, chữa bệnh tại BV, Khám sức khỏe; NCKH";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "X-Quang" && p.DTuong.ToUpper().Contains("KSK") && (p.TenDV.ToLower().Contains("chụp clvt") || p.TenDV.ToLower().Contains("chụp cắt lớp vi tính"))).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "X-Quang" && p.DTuong.ToUpper().Contains("KSK") && (p.TenDV.ToLower().Contains("chụp clvt") || p.TenDV.ToLower().Contains("chụp cắt lớp vi tính"))).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);
            #endregion

            #region 29. Tổng số chụp MRI (29 = 29a + 29b +29c)
            moi = new Content();
            moi.Stt = 29;
            moi.TenNhom = "29. Tổng số chụp MRI (29 = 29a + 29b +29c)";
            moi.TenChiTiet = "";
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 29;
            moi.TenNhom = "29. Tổng số chụp MRI (29 = 29a + 29b +29c)";
            moi.TenChiTiet = "29a. Số chụp MRI cho người bệnh nội trú";
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 29;
            moi.TenNhom = "29. Tổng số chụp MRI (29 = 29a + 29b +29c)";
            moi.TenChiTiet = "29b. Số chụp MRI cho người bệnh khám và ĐT ngoại trú";
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 29;
            moi.TenNhom = "29. Tổng số chụp MRI (29 = 29a + 29b +29c)";
            moi.TenChiTiet = "29c. Số chụp MRI phục vụ những đối tượng không khám, chữa bệnh tại BV, Khám sức khỏe; NCKH";
            _listContent.Add(moi);
            #endregion

            #region 30. Tổng số siêu âm (30 = 30a + 30b +30c)
            moi = new Content();
            moi.Stt = 30;
            moi.TenNhom = "30. Tổng số siêu âm chẩn đoán và điều trị (30 = 30a + 30b +30c)";
            moi.TenChiTiet = "";
            //moi.SLNamSau = q19.Where(p => p.TenRG.Contains("Siêu âm")).Count();
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG.Contains("Siêu âm")).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG.Contains("Siêu âm")).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 30;
            moi.TenNhom = "30. Tổng số siêu âm chẩn đoán và điều trị (30 = 30a + 30b +30c)";
            moi.TenChiTiet = "30a. Số siêu âm cho người bệnh nội trú";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG.Contains("Siêu âm") && p.NoiTru == 1 && p.DTNT == false).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG.Contains("Siêu âm") && p.NoiTru == 1 && p.DTNT == false).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 30;
            moi.TenNhom = "30. Tổng số siêu âm chẩn đoán và điều trị (30 = 30a + 30b +30c)";
            moi.TenChiTiet = "30b. Số siêu âm cho người bệnh khám và ĐT ngoại trú";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG.Contains("Siêu âm") && p.NoiTru == 0 && !p.DTuong.ToUpper().Contains("KSK")).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG.Contains("Siêu âm") && p.NoiTru == 0 && !p.DTuong.ToUpper().Contains("KSK")).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 30;
            moi.TenNhom = "30. Tổng số siêu âm chẩn đoán và điều trị (30 = 30a + 30b +30c)";
            moi.TenChiTiet = "30c. Số siêu âm phục vụ những đối tượng không khám, chữa bệnh tại BV, Khám sức khỏe; NCKH";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG.Contains("Siêu âm") && p.DTuong.ToUpper().Contains("KSK")).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG.Contains("Siêu âm") && p.DTuong.ToUpper().Contains("KSK")).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);
            #endregion

            #region 31. Tổng số nội soi (31 = 31a + 31b)
            moi = new Content();
            moi.Stt = 31;
            moi.TenNhom = "31. Tổng số nội soi chẩn đoán và can thiệp (31 = 31a + 31b)";
            moi.TenChiTiet = "";
            //moi.SLNamSau = q19.Where(p => p.TenRG == "Nội soi").Count();
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "Nội soi").Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "Nội soi").Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 31;
            moi.TenNhom = "31. Tổng số nội soi chẩn đoán và can thiệp (31 = 31a + 31b)";
            moi.TenChiTiet = "31a. Số nội soi cho người bệnh nội trú";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "Nội soi" && p.NoiTru == 1 && p.DTNT == false).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "Nội soi" && p.NoiTru == 1 && p.DTNT == false).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 31;
            moi.TenNhom = "31. Tổng số nội soi chẩn đoán và can thiệp (31 = 31a + 31b)";
            moi.TenChiTiet = "31b. Số nội soi cho người bệnh khám và ĐT ngoại trú";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "Nội soi" && p.NoiTru == 0).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "Nội soi" && p.NoiTru == 0).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);
            #endregion

            #region 32. Tổng số lần đo CNHH( 32 = 32a+ 32b)
            moi = new Content();
            moi.Stt = 32;
            moi.TenNhom = "32. Tổng số lần đo CNHH( 32 = 32a+ 32b)";
            moi.TenChiTiet = "";
            //moi.SLNamSau = q19.Where(p => p.TenRG == "Nội soi").Count();
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 32;
            moi.TenNhom = "32. Tổng số lần đo CNHH( 32 = 32a+ 32b)";
            moi.TenChiTiet = "32a. Đo CNHH cho NB nội trú";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap && p.NoiTru == 1 && p.DTNT == false).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap && p.NoiTru == 1 && p.DTNT == false).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 32;
            moi.TenNhom = "32. Tổng số lần đo CNHH( 32 = 32a+ 32b)";
            moi.TenChiTiet = "32b. Đo CNHH cho NB khám và điều trị ngoại trú";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap && p.NoiTru == 0).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap && p.NoiTru == 0).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);
            #endregion

            #region 33. Tổng số bệnh nhân điện tim (32 = 32a +32b)
            moi = new Content();
            moi.Stt = 33;
            moi.TenNhom = "33. Tổng số bệnh nhân điện tim (32 = 32a +32b)";
            moi.TenChiTiet = "";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "Điện tim").Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "Điện tim").Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 33;
            moi.TenNhom = "33. Tổng số bệnh nhân điện tim (32 = 32a +32b)";
            moi.TenChiTiet = "33a. Số điện tim cho người bệnh nội trú";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "Điện tim" && p.NoiTru == 1).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "Điện tim" && p.NoiTru == 1).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 33;
            moi.TenNhom = "33. Tổng số bệnh nhân điện tim (32 = 32a +32b +32c)";
            moi.TenChiTiet = "33b. Số điện tim cho người bệnh khám và ĐT ngoại trú";
            moi.KQ6Thang2015 = q19.Where(p => p.NgayTH >= _tungayNamTruoc && p.NgayTH <= _denngayNamTruoc && p.TenRG == "Điện tim" && p.NoiTru == 0).Count();
            moi.KQ6Thang2016 = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "Điện tim" && p.NoiTru == 0).Count();
            moi.Tile6Thang2016SoVoi2015 = (moi.KQ6Thang2015 != 0) ? (double)moi.KQ6Thang2016 / moi.KQ6Thang2015 * 100 : null;
            moi.Tile6Thang2016SoVoi2015 = Math.Round(moi.Tile6Thang2016SoVoi2015.GetValueOrDefault(), 2);
            _listContent.Add(moi);
            #endregion

            #region 34. Tổng số tai biến trong điều trị phát hiện được
            moi = new Content();
            moi.Stt = 34;
            moi.TenNhom = "34. Tổng số tai biến trong điều trị phát hiện được (33 = 33a + 33b + 33c + 33d +33đ)";
            moi.TenChiTiet = "";
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 34;
            moi.TenNhom = "34. Tổng số tai biến trong điều trị phát hiện được (33 = 33a + 33b + 33c + 33d +33đ)";
            moi.TenChiTiet = "34a. Số tai biến do sử dụng thuốc";
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 34;
            moi.TenNhom = "34. Tổng số tai biến trong điều trị phát hiện được (33 = 33a + 33b + 33c + 33d +33đ)";
            moi.TenChiTiet = "- Số tai biến do phản ứng có hại của thuốc (ADR)";
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 34;
            moi.TenNhom = "34. Tổng số tai biến trong điều trị phát hiện được (33 = 33a + 33b + 33c + 33d +33đ)";
            moi.TenChiTiet = "34b. Số tai biến do truyền máu";
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 34;
            moi.TenNhom = "34. Tổng số tai biến trong điều trị phát hiện được (33 = 33a + 33b + 33c + 33d +33đ)";
            moi.TenChiTiet = "34c. Số tai biến do phẫu thuật";
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 34;
            moi.TenNhom = "34. Tổng số tai biến tỏng điều trị phát hiện được (33 = 33a + 33b + 33c + 33d +33đ)";
            moi.TenChiTiet = "34d. Số tai biến do thủ thuật";
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 34;
            moi.TenNhom = "34. Tổng số tai biến tỏng điều trị phát hiện được (33 = 33a + 33b + 33c + 33d +33đ)";
            moi.TenChiTiet = "34đ. Số tai biến khác (ghi cụ thể)";
            _listContent.Add(moi);
            #endregion

            #region 35. Tổng số tai biến sản, phụ khoa
            moi = new Content();
            moi.Stt = 35;
            moi.TenNhom = "35. Tổng số tai biến sản, phụ khoa";
            moi.TenChiTiet = "";
            _listContent.Add(moi);
            #endregion

            #region 35. Số kỹ thuật lâm sàng mới (lần đầu tiên thực hiện tại BV) ( tạm bỏ )
            //moi = new Content();
            //moi.Stt = 35;
            //moi.TenNhom = "35. Số kỹ thuật lâm sàng mới (lần đầu tiên thực hiện tại BV)";
            //moi.TenChiTiet = "35a. Số kỹ thuật lâm sàng mới được BV tuyến trên về chuyển giao tại BV";
            //_listContent.Add(moi);

            //moi = new Content();
            //moi.Stt = 35;
            //moi.TenNhom = "35. Số kỹ thuật lâm sàng mới (lần đầu tiên thực hiện tại BV)";
            //moi.TenChiTiet = "35b. Số kỹ thuật lâm sàng mới do BV cử cán bộ đi học về triển khai";
            //_listContent.Add(moi);

            //moi = new Content();
            //moi.Stt = 35;
            //moi.TenNhom = "35. Số kỹ thuật lâm sàng mới (lần đầu tiên thực hiện tại BV)";
            //moi.TenChiTiet = "35c. Kể tên những kỹ thuật lâm sàng MỚI được triển khai trong năm " + cbbNam.Text;
            //_listContent.Add(moi);
            #endregion // tạm bỏ

            #region 36. Số kỹ thuật cận lâm sàng mới thực hiện trong 6 tháng đầu năm 2017
            moi = new Content();
            moi.Stt = 36;
            moi.TenNhom = "36. Số kỹ thuật cận lâm sàng mới thực hiện trong 6 tháng đầu năm 2017 (kể tên)";
            moi.TenChiTiet = "";
            _listContent.Add(moi);
            #endregion

            #region 37. Số kỹ thuật cận lâm sàng mới thực hiện trong 6 tháng năm 2017
            moi = new Content();
            moi.Stt = 37;
            moi.TenNhom = "37. Số kỹ thuật cận lâm sàng mới thực hiện trong 6 tháng năm 2017 ( kể tên)";
            moi.TenChiTiet = "";
            _listContent.Add(moi);
            #endregion

            #region  Xuat excel
            string col1_BC = "Chỉ số hoạt động";
            string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@" };
            string[] _tieude = { "Stt", col1_BC, kehoach, _kq6thang2015, _kq6thang2016, _tile1, _tile2 };
            string _filePath = "C:\\" + "ChiTieuKhamBenh.xls";
            int[] _arrWidth = new int[] { };
            var qexcel = _listContent;
            DungChung.Bien.MangHaiChieu = new Object[qexcel.Count() + 1, 7];
            for (int i = 0; i < 7; i++)
            {
                DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
            }
            int num = 1;
            foreach (var r in qexcel)
            {
                DungChung.Bien.MangHaiChieu[num, 0] = num;
                DungChung.Bien.MangHaiChieu[num, 1] = r.TenChiTiet;
                DungChung.Bien.MangHaiChieu[num, 2] = r.KH2016;
                DungChung.Bien.MangHaiChieu[num, 3] = r.KQ6Thang2015;
                DungChung.Bien.MangHaiChieu[num, 4] = r.KQ6Thang2016;
                DungChung.Bien.MangHaiChieu[num, 5] = r.Tile6Thang2016SoVoiKH2016;
                DungChung.Bien.MangHaiChieu[num, 6] = r.Tile6Thang2016SoVoi2015;
                num++;
            }
            //QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "123", _filePath, true);
            #endregion

            BaoCao.Rep_BC_ChiTieu rep = new BaoCao.Rep_BC_ChiTieu();
            frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "sheet1", _filePath, true, this.Name);
            rep.lblTitle.Text = title;
            rep.cel_Title_KH2016.Text = kehoach;
            rep.cel_Title_KQ6Thang2015.Text = _kq6thang2015;
            rep.cel_Title_KQ6Thang2016.Text = _kq6thang2016;
            rep.cel_Title_TiLe6Thang2016VS2016.Text = _tile1;
            rep.cel_Title_TiLe6Thang2016VS2015.Text = _tile2;
            rep.lblNgayThang.Text = tenhuyen + ", ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            rep.DataSource = _listContent;
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        #region class Content
        public class Content
        {
            public string TenNhom { get; set; }
            public string TenChiTiet { get; set; }
            public int Stt { get; set; }
            public double? KH2016 { get; set; }
            public double? KQ6Thang2015 { get; set; }
            public double? KQ6Thang2016 { get; set; }
            public double? Tile6Thang2016SoVoiKH2016 { get; set; }
            public double? Tile6Thang2016SoVoi2015 { get; set; }
        }
        #endregion

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

        private void radQuy_CheckedChanged(object sender, EventArgs e)
        {
            if (radQuy.Checked)
            {
                cbbThang.Enabled = false;
                cbbNam.Enabled = false;
                cbbQuy.Enabled = true;
                cbbNam_2.Enabled = true;
                lupTuNgay.Enabled = false;
                lupDenNgay.Enabled = false;
            }
        }

        private void radThang_CheckedChanged(object sender, EventArgs e)
        {
            if (radThang.Checked)
            {
                cbbQuy.Enabled = false;
                cbbNam_2.Enabled = false;
                cbbThang.Enabled = true;
                cbbNam.Enabled = true;
                lupTuNgay.Enabled = false;
                lupDenNgay.Enabled = false;
            }
        }

        private void rdTheoNgay_CheckedChanged(object sender, EventArgs e)
        {

            if (rdTheoNgay.Checked)
            {
                cbbQuy.Enabled = false;
                cbbNam_2.Enabled = false;
                cbbThang.Enabled = false;
                cbbNam.Enabled = false;
                lupTuNgay.Enabled = true;
                lupDenNgay.Enabled = true;
            }
        }

    }
}