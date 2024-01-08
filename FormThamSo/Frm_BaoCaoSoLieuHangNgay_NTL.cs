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
    public partial class Frm_BaoCaoSoLieuHangNgay_NTL : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BaoCaoSoLieuHangNgay_NTL()
        {
            InitializeComponent();
        }
        public class BCSoLieu_NTL
        {
            private string _phongkham;

            public string Phongkham
            {
                get { return _phongkham; }
                set { _phongkham = value; }
            }

            private int _tong;

            public int Tong
            {
                get { return _tong; }
                set { _tong = value; }
            }

            private int _bhyt;

            public int Bhyt
            {
                get { return _bhyt; }
                set { _bhyt = value; }
            }
            private int _dv;

            public int Dv
            {
                get { return _dv; }
                set { _dv = value; }
            }
            private int _dieutri;

            public int Dieutri
            {
                get { return _dieutri; }
                set { _dieutri = value; }
            }
            private int _xNSinhhoa;

            public int XNSinhhoa
            {
                get { return _xNSinhhoa; }
                set { _xNSinhhoa = value; }
            }
            private int _xNHuyethoc;

            public int XNHuyethoc
            {
                get { return _xNHuyethoc; }
                set { _xNHuyethoc = value; }
            }
            private int _xNNuoctieu;

            public int XNNuoctieu
            {
                get { return _xNNuoctieu; }
                set { _xNNuoctieu = value; }
            }
            private int _xNMiendich;

            public int XNMiendich
            {
                get { return _xNMiendich; }
                set { _xNMiendich = value; }
            }
            private int _xNDongcammau;

            public int XNDongcammau
            {
                get { return _xNDongcammau; }
                set { _xNDongcammau = value; }
            }

            private int _xNVisinh;

            public int XNVisinh
            {
                get { return _xNVisinh; }
                set { _xNVisinh = value; }
            }
            private int _xNKhac;

            public int XNKhac
            {
                get { return _xNKhac; }
                set { _xNKhac = value; }
            }
            private int _sieuam;

            public int Sieuam
            {
                get { return _sieuam; }
                set { _sieuam = value; }
            }
            private int _xQuang;

            public int XQuang
            {
                get { return _xQuang; }
                set { _xQuang = value; }
            }
            private int _cTScanner;

            public int CTScanner
            {
                get { return _cTScanner; }
                set { _cTScanner = value; }
            }

            private int _noiSoitieuhoa;

            public int NoiSoitieuhoa
            {
                get { return _noiSoitieuhoa; }
                set { _noiSoitieuhoa = value; }
            }
            private int _noiSoiTMH;

            public int NoiSoiTMH
            {
                get { return _noiSoiTMH; }
                set { _noiSoiTMH = value; }
            }
            private int _dienTim;

            public int DienTim
            {
                get { return _dienTim; }
                set { _dienTim = value; }
            }
            private int _dienNao;

            public int DienNao
            {
                get { return _dienNao; }
                set { _dienNao = value; }
            }
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void btnReport_Click(object sender, EventArgs e)
        {
            

            DateTime tungay = Convert.ToDateTime(dateTimePicker1.Text);
            DateTime denngay = Convert.ToDateTime(dateTimePicker2.Text);
            if(tungay > denngay)
            {
                MessageBox.Show("Bạn đã chọn sai ngày, vui lòng chọn lại !");
            }
            else
            {
                List<BCSoLieu_NTL> _listbcsl = new List<BCSoLieu_NTL>();
                Dictionary<string, object> _dic = new Dictionary<string, object>();
                _dic.Add("TenCQCQ", DungChung.Bien.TenCQCQ);
                _dic.Add("TenCQ", DungChung.Bien.TenCQ);

                string s = "Từ " + tungay.Hour + " giờ " + tungay.Minute + " phút, ngày " + tungay.Day + " tháng " + tungay.Month + " năm " + tungay.Year + " đến " + denngay.Hour + " giờ  " + denngay.Minute + " phút, ngày " + denngay.Day + " tháng " + denngay.Month + " năm " + denngay.Year;
                _dic.Add("Tungay", s);
                //Danh sách phòng khám
                var _kphong = (from kp in _data.KPhongs.Where(p => p.PLoai == "Phòng khám") select new { kp.TenKP }).ToList();

                //            Cột BHYT:
                //SELECT KPhong.TenKP, COUNT(BNKB.MaBNhan) AS Expr1
                //FROM BNKB INNER JOIN
                //BenhNhan ON BNKB.MaBNhan = BenhNhan.MaBNhan INNER JOIN
                //KPhong ON BNKB.MaKP = KPhong.MaKP
                //WHERE (BNKB.NgayKham BETWEEN CONVERT(DATETIME, '2019-10-01 00:00:00', 102) AND CONVERT(DATETIME, '2019-10-31 23:59:59', 102)) AND (KPhong.PLoai = N'phòng khám') AND
                //(BenhNhan.DTuong = N'bhyt')
                //GROUP BY KPhong.TenKP
                var _tongDTbn = (from bn in _data.BenhNhans join bnkb in _data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay) on bn.MaBNhan equals bnkb.MaBNhan join kp in _data.KPhongs.Where(p => p.PLoai == "Phòng khám") on bnkb.MaKP equals kp.MaKP select new { bn.MaBNhan, bn.TenBNhan, bn.DTuong, kp.TenKP, bnkb.NgayKham }).ToList();
                //Bệnh nhân điều trị
                var _bndieutri = (from bn in _data.BenhNhans
                                  join bnkb in _data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay).Where(x => x.PhuongAn == 1) on bn.MaBNhan equals bnkb.MaBNhan
                                  join kp in _data.KPhongs.Where(p => p.PLoai == "Phòng khám") on bnkb.MaKP equals kp.MaKP
                                  select new
                                  {
                                      kp.TenKP,
                                      bnkb.MaBNhan
                                  }).ToList();

                //            SELECT KPhong.TenKP, COUNT(DichVu.TenDV) AS Expr1
                //FROM CLS INNER JOIN
                //BNKB ON CLS.MaBNhan = BNKB.MaBNhan INNER JOIN
                //KPhong ON CLS.MaKP = KPhong.MaKP INNER JOIN
                //ChiDinh ON CLS.IdCLS = ChiDinh.IdCLS INNER JOIN
                //DichVu ON ChiDinh.MaDV = DichVu.MaDV INNER JOIN
                //TieuNhomDV ON DichVu.IdTieuNhom = TieuNhomDV.IdTieuNhom
                //WHERE (BNKB.NgayKham BETWEEN CONVERT(DATETIME, '2019-10-01 00:00:00', 102) AND CONVERT(DATETIME, '2019-10-31 23:59:59', 102)) AND (KPhong.PLoai = N'phòng khám') AND
                //(TieuNhomDV.TenRG = N'XN hóa sinh máu')
                //GROUP BY KPhong.TenKP
                var _phieuxn = (from bn in _data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay)
                                join cls in _data.CLS on bn.MaBNhan equals cls.MaBNhan
                                join kp in _data.KPhongs.Where(p => p.PLoai == "Phòng khám") on cls.MaKP equals kp.MaKP
                                join chidinh in _data.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                                join dichvu in _data.DichVus on chidinh.MaDV equals dichvu.MaDV
                                join tieunhom in _data.TieuNhomDVs on dichvu.IdTieuNhom equals tieunhom.IdTieuNhom
                                select new
                                {
                                    tieunhom.TenRG,
                                    dichvu.MaDV,
                                    kp.TenKP
                                }).ToList();

                var _kp1 = _kphong.Select(p => p.TenKP).ToList();
                if(_tongDTbn.Count > 0 && _phieuxn.Count >0 && _bndieutri.Count >0)
                {
                    foreach (var item in _kp1)
                    {
                        BCSoLieu_NTL _bcsl = new BCSoLieu_NTL();
                        _bcsl.Phongkham = item;
                        int _tong = _tongDTbn.Where(p => p.TenKP == item).Count();
                        _bcsl.Tong = _tong;
                        int bnBHYT = _tongDTbn.Where(p => p.TenKP == item).Where(p => p.DTuong == "BHYT").Count(); _bcsl.Bhyt = bnBHYT;
                        int bnDV = _tongDTbn.Where(p => p.TenKP == item).Where(p => p.DTuong == "Dịch vụ").Count(); _bcsl.Dv = bnDV;
                        //Bệnh nhân điều trị
                        //                SELECT KPhong.TenKP, COUNT(BNKB.MaBNhan) AS Expr1
                        //FROM BNKB INNER JOIN
                        //BenhNhan ON BNKB.MaBNhan = BenhNhan.MaBNhan INNER JOIN
                        //KPhong ON BNKB.MaKP = KPhong.MaKP
                        //WHERE (BNKB.NgayKham BETWEEN CONVERT(DATETIME, '2019-10-01 00:00:00', 102) AND CONVERT(DATETIME, '2019-10-31 23:59:59', 102)) AND (KPhong.PLoai = N'phòng khám') AND
                        //(BNKB.PhuongAn = 1)
                        //GROUP BY KPhong.TenKP
                        int bnDtri = _bndieutri.Where(p => p.TenKP == item).Count(); _bcsl.Dieutri = bnDtri;

                        int xnSinhhoa = _phieuxn.Where(p => p.TenKP == item).Where(x => x.TenRG.Contains("XN hóa sinh máu")).Count();
                        _bcsl.XNSinhhoa = xnSinhhoa;

                        int xnHuyethoc = _phieuxn.Where(p => p.TenKP == item).Where(x => x.TenRG.Contains("XN huyết học")).Count();
                        _bcsl.XNHuyethoc = xnHuyethoc;

                        int xnNuoctieu = _phieuxn.Where(p => p.TenKP == item).Where(x => x.TenRG.Contains("XN nước tiểu")).Count();
                        _bcsl.XNNuoctieu = xnNuoctieu;

                        int xnmiendich = _phieuxn.Where(p => p.TenKP == item).Where(x => x.TenRG.Contains("miễn dịch")).Count();
                        _bcsl.XNMiendich = xnmiendich;

                        int xnDongcammau = _phieuxn.Where(p => p.TenKP == item).Where(x => x.TenRG.Contains("XN Đông cầm máu")).Count();
                        _bcsl.XNDongcammau = xnDongcammau;

                        int xnVisinh = _phieuxn.Where(p => p.TenKP == item).Where(x => x.TenRG.Contains("XN vi sinh")).Count();
                        _bcsl.XNVisinh = xnVisinh;

                        int XNkhac = _phieuxn.Where(p => p.TenKP == item).Where(x => x.TenRG.Contains("XN khác")).Count();
                        _bcsl.XNKhac = XNkhac;

                        int sieuam = _phieuxn.Where(p => p.TenKP == item).Where(x => x.TenRG.Contains("Siêu âm")).Count();
                        _bcsl.Sieuam = sieuam;

                        int Xquang = _phieuxn.Where(p => p.TenKP == item).Where(x => x.TenRG=="X-Quang").Count();
                        _bcsl.XQuang = Xquang;

                        int ctscanner = _phieuxn.Where(p => p.TenKP == item).Where(x => x.TenRG=="X-Quang CT").Count();
                        _bcsl.CTScanner = ctscanner;

                        int noisoitieuhoa = _phieuxn.Where(p => p.TenKP == item).Where(x => x.TenRG.Contains("Nội soi Dạ Dày")).Count();
                        _bcsl.NoiSoitieuhoa = noisoitieuhoa;

                        int noisoiTMH = _phieuxn.Where(p => p.TenKP == item).Where(x => x.TenRG.Contains("Nội soi Tai-Mũi-Họng")).Count();
                        _bcsl.NoiSoiTMH = noisoiTMH;

                        int dientim = _phieuxn.Where(p => p.TenKP == item).Where(x => x.TenRG.Contains("Điện tim")).Count();
                        _bcsl.DienTim = dientim;

                        int diennao = _phieuxn.Where(p => p.TenKP == item).Where(x => x.TenRG.Contains("Điện não đồ")).Count();
                        _bcsl.DienNao = diennao;

                        _listbcsl.Add(_bcsl);
                    }
                    DungChung.Ham.Print(DungChung.PrintConfig.Rp_Solieutheongay_NTLcs1, _listbcsl, _dic, false);
                }
                else { MessageBox.Show("Không có dữ liệu !"); }
                
            }
           
        }

        private void Frm_BaoCaoSoLieuHangNgay_NTL_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Text = DateTime.Now.ToString("dd/MM/yyyy 00:00:00");
            dateTimePicker2.Text = DateTime.Now.ToString("dd/MM/yyyy 23:59:59");
        }
    }
}