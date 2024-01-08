using System;
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
    public partial class frm_BaoCao : DevExpress.XtraEditors.XtraForm
    {
        public frm_BaoCao()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void btnIn_Click(object sender, EventArgs e)
        {
            //Khoa Kham Benh
            if (radioGroup1.SelectedIndex == 0)
            {

                DateTime _tungay = System.DateTime.Now.Date;
                DateTime _denngay = System.DateTime.Now.Date;
                _tungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                _denngay = DungChung.Ham.NgayDen(lupNgayden.DateTime);
                //khambenh
                var b = (from bn in _Data.BenhNhans.Where(p => p.NNhap >= _tungay && p.NNhap <= _denngay)
                             //join bnrv in _Data.RaViens.Where(p => p.NgayRa.Value.Month == thang && p.NgayRa.Value.Year == nam) on bn.MaBNhan equals bnrv.MaBNhan
                         select new
                         {
                             bn.MaBNhan,
                             bn.Status,
                             bn.NoiTru,
                             bn.DTuong,
                             bn.Tuoi,
                             bn.SThe,
                             bn.DTNT,
                             bn.NNhap,
                             bn.MaKP,
                         });

                //dieutri
                var bb = (from bn in _Data.BenhNhans
                          join bnrv in _Data.VaoViens.Where(p => p.NgayVao >= _tungay && p.NgayVao <= _denngay) on bn.MaBNhan equals bnrv.MaBNhan
                          select new
                          {
                              bn.NoiTru,
                              bn.DTuong,
                              bn.Tuoi,
                              bn.SThe,
                              bn.DTNT,
                          });

                var b1 = b.Count(p => p.DTNT == false && p.NoiTru == 0);

                var c = b.Count(p => p.DTuong == "BHYT");
                var d = b.Count(p => p.Tuoi < 6);
                var f = (from ng in b.Where(p => p.Tuoi >= 60)
                         select new
                         {

                         }).Count();
                var g = b.Count(p => p.DTuong == "Dịch vụ");
                var h = b.Count(p => p.DTuong == "HN");
                var i = b.Count(p => p.NoiTru == 1);
                var k = b.Count(p => p.DTNT == true);
                var l = b.Count(p => p.DTNT == true && p.Tuoi >= 60);
                var m = (from ng in b.Where(p => p.NoiTru == 0)
                         join rv in _Data.RaViens.Where(p => p.Status == 1) on ng.MaBNhan equals rv.MaBNhan
                         select new { }).Count();
                var ooo = (from cd in _Data.ChiDinhs.Where(p => p.Status == 1)
                           join dv in _Data.DichVus.Where(p => p.IdTieuNhom == 1) on cd.MaDV equals dv.MaDV
                           join cls in _Data.CLS on cd.IdCLS equals cls.IdCLS
                           join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                           where rv.NgayRa >= _tungay && rv.NgayRa <= _denngay
                           select new
                           {
                               cd.IdCLS,
                               rv.MaBNhan,
                           });
                var o = ooo.GroupBy(p => p.IdCLS).Count();
                var o0 = ooo.GroupBy(p => p.MaBNhan).Count();

                var o1 = (from a in ooo
                          join bn in _Data.BenhNhans.Where(p => p.NoiTru == 1) on a.MaBNhan equals bn.MaBNhan
                          select new
                          {
                              a.IdCLS,
                          }).GroupBy(p => p.IdCLS).Count();
                var o2 = (from a in ooo
                          join bn in _Data.BenhNhans.Where(p => p.NoiTru == 0 && p.DTNT == false) on a.MaBNhan equals bn.MaBNhan
                          select new
                          {
                              a.IdCLS,
                          }).GroupBy(p => p.IdCLS).Count();

                var o3 = (from a in ooo
                          join bn in _Data.BenhNhans.Where(p => p.NoiTru == 0 && p.DTNT == true) on a.MaBNhan equals bn.MaBNhan
                          select new
                          {
                              a.IdCLS,
                          }).GroupBy(p => p.IdCLS).Count();
                var ttt = (from cd in _Data.ChiDinhs.Where(p => p.Status == 1)
                           join dv in _Data.DichVus.Where(p => p.IdTieuNhom == 2) on cd.MaDV equals dv.MaDV
                           join cls in _Data.CLS on cd.IdCLS equals cls.IdCLS
                           join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                           where rv.NgayRa >= _tungay && rv.NgayRa <= _denngay
                           select new
                           {
                               cd.IdCLS,
                               rv.MaBNhan,
                           });
                var t = ttt.GroupBy(p => p.IdCLS).Count();
                var t0 = ttt.GroupBy(p => p.MaBNhan).Count();

                var t1 = (from a in ttt
                          join bn in _Data.BenhNhans.Where(p => p.NoiTru == 1) on a.MaBNhan equals bn.MaBNhan
                          select new
                          {
                              a.IdCLS,
                          }).GroupBy(p => p.IdCLS).Count();
                var t2 = (from a in ttt
                          join bn in _Data.BenhNhans.Where(p => p.NoiTru == 0 && p.DTNT == false) on a.MaBNhan equals bn.MaBNhan
                          select new
                          {
                              a.MaBNhan,
                              a.IdCLS,
                          }).GroupBy(p => p.MaBNhan).Count();
                var t3 = (from a in ttt
                          join bn in _Data.BenhNhans.Where(p => p.NoiTru == 0 && p.DTNT == true) on a.MaBNhan equals bn.MaBNhan
                          select new
                          {
                              a.MaBNhan,
                              a.IdCLS,
                          }).GroupBy(p => p.MaBNhan).Count();
                var yyy = (from cd in _Data.ChiDinhs.Where(p => p.Status == 1)
                           join dv in _Data.DichVus.Where(p => p.IdTieuNhom == 6) on cd.MaDV equals dv.MaDV
                           join cls in _Data.CLS on cd.IdCLS equals cls.IdCLS
                           join rv in _Data.RaViens on cls.MaBNhan equals rv.MaBNhan
                           where rv.NgayRa >= _tungay && rv.NgayRa <= _denngay
                           select new
                           {
                               cd.IdCLS,
                               rv.MaBNhan
                           });
                var y = yyy.GroupBy(p => p.IdCLS).Count();
                var y0 = yyy.GroupBy(p => p.MaBNhan).Count();
                var y1 = (from a in yyy
                          join bn in _Data.BenhNhans.Where(p => p.NoiTru == 1) on a.MaBNhan equals bn.MaBNhan
                          select new
                          {
                              a.IdCLS,
                          }).GroupBy(p => p.IdCLS).Count();
                var y2 = (from a in yyy
                          join bn in _Data.BenhNhans.Where(p => p.NoiTru == 0 && p.DTNT == false) on a.MaBNhan equals bn.MaBNhan
                          select new
                          {
                              a.MaBNhan,
                              a.IdCLS,
                          }).GroupBy(p => p.MaBNhan).Count();
                var y3 = (from a in yyy
                          join bn in _Data.BenhNhans.Where(p => p.NoiTru == 0 && p.DTNT == true) on a.MaBNhan equals bn.MaBNhan
                          select new
                          {
                              a.IdCLS,
                          }).GroupBy(p => p.IdCLS).Count();
                var uuu = (from cd in _Data.ChiDinhs.Where(p => p.NgayTH >= _tungay && p.NgayTH <= _denngay)
                           join dv in _Data.DichVus.Where(p => p.IdTieuNhom == 5) on cd.MaDV equals dv.MaDV
                           join cls in _Data.CLS on cd.IdCLS equals cls.IdCLS
                           select new
                           {
                               cls.MaBNhan,
                               cd.IdCLS,
                           });

                var u = (from cd in _Data.ChiDinhs.Where(p => p.NgayTH >= _tungay && p.NgayTH <= _denngay)
                         join dv in _Data.DichVus.Where(p => p.IdTieuNhom == 5) on cd.MaDV equals dv.MaDV
                         join tn in _Data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                         select new
                         {
                             cd.IdCLS,
                         }).GroupBy(p => p.IdCLS).Count();
                var u0 = uuu.GroupBy(p => p.MaBNhan).Count();
                var u1 = (from a in uuu
                          join bn in _Data.BenhNhans.Where(p => p.NoiTru == 1) on a.MaBNhan equals bn.MaBNhan
                          select new
                          {
                              a.IdCLS,
                          }).GroupBy(p => p.IdCLS).Count();
                var u2 = (from a in uuu
                          join bn in _Data.BenhNhans.Where(p => p.NoiTru == 0 && p.DTNT == false) on a.MaBNhan equals bn.MaBNhan
                          select new
                          {
                              a.IdCLS,
                          }).GroupBy(p => p.IdCLS).Count();
                var u3 = (from a in uuu
                          join bn in _Data.BenhNhans.Where(p => p.NoiTru == 0 && p.DTNT == true) on a.MaBNhan equals bn.MaBNhan
                          select new
                          {
                              a.IdCLS,
                          }).GroupBy(p => p.IdCLS).Count();

                var jjj = (from cd in _Data.ChiDinhs.Where(p => p.NgayTH >= _tungay && p.NgayTH <= _denngay)
                           join dv in _Data.DichVus.Where(p => p.IdTieuNhom == 4) on cd.MaDV equals dv.MaDV
                           join cls in _Data.CLS on cd.IdCLS equals cls.IdCLS
                           select new
                           {
                               cls.MaBNhan,
                               cd.IdCLS,
                           });
                var j = (from cd in _Data.ChiDinhs.Where(p => p.NgayTH >= _tungay && p.NgayTH <= _denngay)
                         join dv in _Data.DichVus.Where(p => p.IdTieuNhom == 4) on cd.MaDV equals dv.MaDV
                         select new
                         {
                             cd.IdCLS,
                         }).GroupBy(p => p.IdCLS).Count();
                var j0 = jjj.GroupBy(p => p.MaBNhan).Count();
                var j1 = (from a in jjj
                          join bn in _Data.BenhNhans.Where(p => p.NoiTru == 1) on a.MaBNhan equals bn.MaBNhan
                          select new
                          {
                              a.IdCLS,
                          }).GroupBy(p => p.IdCLS).Count();
                var j2 = (from a in jjj
                          join bn in _Data.BenhNhans.Where(p => p.NoiTru == 0 && p.DTNT == false) on a.MaBNhan equals bn.MaBNhan
                          select new
                          {
                              a.IdCLS,
                          }).GroupBy(p => p.IdCLS).Count();
                var j3 = (from a in jjj
                          join bn in _Data.BenhNhans.Where(p => p.NoiTru == 0 && p.DTNT == true) on a.MaBNhan equals bn.MaBNhan
                          select new
                          {
                              a.IdCLS,
                          }).GroupBy(p => p.IdCLS).Count();


                var dientimmm = (from cd in _Data.ChiDinhs.Where(p => p.NgayTH >= _tungay && p.NgayTH <= _denngay)
                                 join dv in _Data.DichVus.Where(p => p.IdTieuNhom == 15) on cd.MaDV equals dv.MaDV
                                 join cls in _Data.CLS on cd.IdCLS equals cls.IdCLS
                                 select new
                                 {
                                     cls.MaBNhan,
                                     cd.IdCLS,
                                 });
                var dientim = (from cd in _Data.ChiDinhs.Where(p => p.NgayTH >= _tungay && p.NgayTH <= _denngay)
                               join dv in _Data.DichVus.Where(p => p.IdTieuNhom == 15) on cd.MaDV equals dv.MaDV
                               select new
                               {
                                   cd.IdCLS,
                               }).GroupBy(p => p.IdCLS).Count();
                var dientim0 = dientimmm.GroupBy(p => p.MaBNhan).Count();
                var dientim1 = (from a in dientimmm
                                join bn in _Data.BenhNhans.Where(p => p.NoiTru == 1) on a.MaBNhan equals bn.MaBNhan
                                select new
                                {
                                    a.IdCLS,
                                }).GroupBy(p => p.IdCLS).Count();
                var dientim2 = (from a in dientimmm
                                join bn in _Data.BenhNhans.Where(p => p.NoiTru == 0 && p.DTNT == false) on a.MaBNhan equals bn.MaBNhan
                                select new
                                {
                                    a.IdCLS,
                                }).GroupBy(p => p.IdCLS).Count();
                var dientim3 = (from a in dientimmm
                                join bn in _Data.BenhNhans.Where(p => p.NoiTru == 0 && p.DTNT == true) on a.MaBNhan equals bn.MaBNhan
                                select new
                                {
                                    a.IdCLS,
                                }).GroupBy(p => p.IdCLS).Count();

                var naooo = (from cd in _Data.ChiDinhs.Where(p => p.NgayTH >= _tungay && p.NgayTH <= _denngay)
                             join dv in _Data.DichVus.Where(p => p.IdTieuNhom == 133) on cd.MaDV equals dv.MaDV
                             join cls in _Data.CLS on cd.IdCLS equals cls.IdCLS
                             select new
                             {
                                 cls.MaBNhan,
                                 cd.IdCLS,
                             });
                var luuhuyetnao = (from cd in _Data.ChiDinhs.Where(p => p.NgayTH >= _tungay && p.NgayTH <= _denngay)
                                   join dv in _Data.DichVus.Where(p => p.IdTieuNhom == 133) on cd.MaDV equals dv.MaDV
                                   select new
                                   {
                                       cd.IdCLS,
                                   }).GroupBy(p => p.IdCLS).Count();
                var luuhuyetnao0 = naooo.GroupBy(p => p.MaBNhan).Count();
                var luuhuyetnao1 = (from a in naooo
                                    join bn in _Data.BenhNhans.Where(p => p.NoiTru == 1) on a.MaBNhan equals bn.MaBNhan
                                    select new
                                    {
                                        a.IdCLS,
                                    }).GroupBy(p => p.IdCLS).Count();
                var luuhuyetnao2 = (from a in naooo
                                    join bn in _Data.BenhNhans.Where(p => p.NoiTru == 0 && p.DTNT == false) on a.MaBNhan equals bn.MaBNhan
                                    select new
                                    {
                                        a.IdCLS,
                                    }).GroupBy(p => p.IdCLS).Count();
                var luuhuyetnao3 = (from a in naooo
                                    join bn in _Data.BenhNhans.Where(p => p.NoiTru == 0 && p.DTNT == true) on a.MaBNhan equals bn.MaBNhan
                                    select new
                                    {
                                        a.IdCLS,
                                    }).GroupBy(p => p.IdCLS).Count();
                var TongSoThuThuat = (from cd in _Data.ChiDinhs.Where(p => p.NgayTH >= _tungay && p.NgayTH <= _denngay)
                                      join dv in _Data.DichVus.Where(p => p.IdTieuNhom == 22) on cd.MaDV equals dv.MaDV
                                      select new
                                      {
                                          cd.IdCLS,
                                      }).GroupBy(p => p.IdCLS).Count();

                var NB = (from p1 in _Data.BNKBs.Where(p => p.NgayKham >= _tungay && p.NgayKham <= _denngay)
                          join kp in _Data.KPhongs.Where(p => p.PLoai.Contains("Phòng khám")) on p1.MaKP equals kp.MaKP
                          select p1.MaCK);
                var NBDaLieu = NB.Count(p => p == 9);
                var NBKhamMat = NB.Count(p => p == 5);
                var NBKhamTMH = NB.Count(p => p == 4);
                var NBKhamSan = NB.Count(p => p == 8);
                var NBKhamRHM = NB.Count(p => p == 3);
                var NBKhamYHCT = NB.Count(p => p == 7);
                var NBKhamNhi = NB.Count(p => p == 2);
                var NBKhamKSK = b.Count(p => p.DTuong == "KSK");
                var Muc20NK = (from m20 in _Data.BNKBs.Where(p => p.MaICD == "T00" ||p.MaICD == "T00.1" || p.MaICD == "T00.2" || p.MaICD == "T00.3" || p.MaICD == "T00.6" || p.MaICD == "T00.8" || p.MaICD == "T00.9")
                               join rv in _Data.RaViens on m20.MaBNhan equals rv.MaBNhan
                               where rv.NgayRa >= _tungay && rv.NgayRa <= _denngay
                               select new
                               {
                                   m20.MaBNhan
                               }).Distinct().Count();
                var Muc20NoiK = (from m20 in _Data.BNKBs.Where(p => p.MaICD != "T00" && p.MaICD != "T00.1" && p.MaICD != "T00.2" && p.MaICD != "T00.3" && p.MaICD != "T00.6" && p.MaICD != "T00.8" && p.MaICD != "T00.9")
                                 join rv in _Data.RaViens on m20.MaBNhan equals rv.MaBNhan
                                 where rv.NgayRa >= _tungay && rv.NgayRa <= _denngay
                                 select new
                                 {
                                     m20.MaBNhan
                                 }).Distinct().Count();
                Dictionary<string, object> _dic = new Dictionary<string, object>();
                _dic.Add("TenCQCQ", DungChung.Bien.TenCQCQ);
                _dic.Add("TenCQ", DungChung.Bien.TenCQ);
                string s = "Từ Ngày" + lupNgaytu.Text + " Đến Ngày " + lupNgayden.Text;
                _dic.Add("Ngaythang", s.ToUpper());
                _dic.Add("LuotKham", b.Count());
                _dic.Add("LuotKhamBHYT", c);
                _dic.Add("LuotKhamTE", d);
                _dic.Add("LuotKhamNG", f);
                _dic.Add("LuotKhamDV", g);
                _dic.Add("LuotKhamNGBHYT", h);

                _dic.Add("LuotDTNoiTru", i);

                _dic.Add("LuotKhamDTNgoaiTru", k);
                _dic.Add("LuotKhamDTNgoaiTru60", l);
                _dic.Add("LuotChuyenVienNgoaiTru", m);

                _dic.Add("XetNghiemSinhHoa", o);
                _dic.Add("SoNguoiSinhHoaMau", o0);
                _dic.Add("XetNghiemSinhHoaNBNoiTru", o1);
                _dic.Add("XetNghiemSinhHoaNBNgoaiTru", o2);
                _dic.Add("XetNghiemSinhHoaNBDieuTriNgoaiTru", o3);

                _dic.Add("XetNghiemHuyetHoc", t);
                _dic.Add("SoNguoiXetNghiemHuyetHoc", t0);
                _dic.Add("XetNghiemHuyetHocNBNoiTru", t1);
                _dic.Add("XetNghiemHuyetHocNBNgoaiTru", t2);
                _dic.Add("XetNghiemHuyetHocNBDieuTriNgoaiTru", t3);

                _dic.Add("XetNghiemNuocTieu", y);
                _dic.Add("SoNguoiXetNghiemNuocTieu", y0);
                _dic.Add("XetNghiemNuocTieuNBNoiTru", y1);
                _dic.Add("XetNghiemNuocTieuNBNgoaiTru", y2);
                _dic.Add("XetNghiemNuocTieuNBDieuTriNgoaiTru", y3);

                _dic.Add("XQuang", u);
                _dic.Add("SoNguoiXQuang", u0);
                _dic.Add("XQuangNBNoiTru", u1);
                _dic.Add("XQuangNBNgoaiTru", u2);
                _dic.Add("XQuangNBKSK", u3);

                _dic.Add("SieuAm", j);
                _dic.Add("SoNguoiSieuAm", j0);
                _dic.Add("SieuAmNBNoiTru", j1);
                _dic.Add("SieuAmNBNgoaiTru", j2);
                _dic.Add("SieuAmNBDieuTriNgoaiTru", j3);

                _dic.Add("DienTim", dientim);
                _dic.Add("SoNguoiDienTim", dientim0);
                _dic.Add("DienTimNBNoiTru", dientim1);
                _dic.Add("DienTimNBNgoaiTru", dientim2);
                _dic.Add("DienTimNBDieuTriNgoaiTru", dientim3);

                _dic.Add("LuuHuyetNao", luuhuyetnao);
                _dic.Add("SoNguoiLuuHuyetNao", luuhuyetnao0);
                _dic.Add("LuuHuyetNaoNBNoiTru", luuhuyetnao1);
                _dic.Add("LuuHuyetNaoNBNgoaiTru", luuhuyetnao2);
                _dic.Add("LuuHuyetNaoNBDieuTriNgoaiTru", luuhuyetnao3);

                _dic.Add("TongSoThuThuat", TongSoThuThuat);
                _dic.Add("NBDaLieu", NBDaLieu);
                _dic.Add("NBKhamMat", NBKhamMat);
                _dic.Add("NBKhamTMH", NBKhamTMH);
                _dic.Add("NBKhamSan", NBKhamSan);
                _dic.Add("NBKhamRHM", NBKhamRHM);
                _dic.Add("NBKhamYHCT", NBKhamYHCT);
                _dic.Add("NBKhamNhi", NBKhamNhi);
                _dic.Add("NBKhamKSK", NBKhamKSK);
                _dic.Add("TongSoCCNgoaiKhoa", Muc20NK);
                _dic.Add("TongSoCCNoiKhoa", Muc20NoiK);
                DungChung.Ham.Print(DungChung.PrintConfig.Rep_BCKhoaKhamBenh24012, b.ToList(), _dic, false);
            }
            //KhoaCLS
            else if (radioGroup1.SelectedIndex == 1)
            {
                //CanLamSang
                if (cbKP.Text == "")
                {
                    MessageBox.Show("Bạn chưa chọn thông tin khoa.", "Thông Báo!!!");
                    return;
                }
                string aa = "";
                aa = cbKP.Text;
                var khoaphong = _Data.KPhongs.Single(p => p.TenKP == aa);
                DateTime _tungay = System.DateTime.Now.Date;
                DateTime _denngay = System.DateTime.Now.Date;
                _tungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                _denngay = DungChung.Ham.NgayDen(lupNgayden.DateTime);
                var GiuongKH = (from kp in _Data.KPhongs.Where(p => p.SoGiuongKH != null) select new { kp.MaKP, kp.TenKP, kp.SoGiuongKH }).ToList();
                int TongGiuong = 0;
                var b = (from bn in _Data.BenhNhans.Where(p => p.NNhap >= _tungay && p.NNhap <= _denngay)
                             //join bnrv in _Data.RaViens.Where(p => p.NgayRa.Value.Month == thang && p.NgayRa.Value.Year == nam) on bn.MaBNhan equals bnrv.MaBNhan
                         select new
                         {
                             bn.MaBNhan,
                             bn.Status,
                             bn.NoiTru,
                             bn.DTuong,
                             bn.Tuoi,
                             bn.SThe,
                             bn.DTNT,
                             bn.NNhap,
                             bn.MaKP,
                         });

                //dieutri
                //var bb = (from bn in _Data.BenhNhans
                //          join bnrv in _Data.VaoViens.Where(p => p.NgayVao >= _tungay && p.NgayVao <= _denngay) on bn.MaBNhan equals bnrv.MaBNhan
                //          select new
                //          {
                //              bn.NoiTru,
                //              bn.DTuong,
                //              bn.Tuoi,
                //              bn.SThe,
                //              bn.DTNT,
                //          });
                for (int ii = 0; ii < GiuongKH.Count; ii++)
                {
                    int SoGiuongTungKhoa = tachchuoi(GiuongKH[ii].SoGiuongKH);
                    TongGiuong += SoGiuongTungKhoa;
                }
                var SoBanhNhanTon1 = (from bn in _Data.BenhNhans.Where(p => p.Status == 0 || p.Status == 1 || p.Status == 4 || p.Status == 5)
                                      join bnvv in _Data.VaoViens.Where(p => p.NgayVao < _tungay && p.MaKP == khoaphong.MaKP) on bn.MaBNhan equals bnvv.MaBNhan
                                      select new
                                      {

                                      }).Count();
                var SoBanhNhanTon2 = (from bn in _Data.BenhNhans.Where(p => p.Status == 2 || p.Status == 3)
                                      join bnvv in _Data.VaoViens.Where(p => p.MaKP == khoaphong.MaKP) on bn.MaBNhan equals bnvv.MaBNhan
                                      join rv in _Data.RaViens.Where(p => p.NgayVao < _tungay && p.NgayRa > _denngay) on bn.MaBNhan equals rv.MaBNhan
                                      select new
                                      {

                                      }).Count();
                var SoBanhNhanTonCuoiThang1 = (from bn in _Data.BenhNhans.Where(p => p.Status == 0 || p.Status == 1 || p.Status == 4 || p.Status == 5)
                                               join bnvv in _Data.VaoViens.Where(p => (p.NgayVao >= _tungay && p.NgayVao <= _denngay) && p.MaKP == khoaphong.MaKP) on bn.MaBNhan equals bnvv.MaBNhan
                                               select new
                                               {

                                               }).Count();
                var SoBanhNhanTonCuoiThang2 = (from bn in _Data.BenhNhans.Where(p => p.Status == 2 || p.Status == 3)
                                               join bnvv in _Data.VaoViens.Where(p => (p.NgayVao >= _tungay && p.NgayVao <= _denngay) && p.MaKP == khoaphong.MaKP) on bn.MaBNhan equals bnvv.MaBNhan
                                               join rv in _Data.RaViens.Where(p => p.NgayRa > _denngay) on bn.MaBNhan equals rv.MaBNhan
                                               select new
                                               {

                                               }).Count();


                    var TongSoNgayDieuTriNoiTru = (from bnkb in _Data.BNKBs.Where(p => p.NgayKham < _denngay && p.NgayNghi >= _tungay && p.MaKP == 9)
                                                   select new
                                                   {
                                                   }).Count();
                  

                var TongSoLuotDieuTriNoiTru = (from vv in _Data.VaoViens.Where(p => p.MaKP == khoaphong.MaKP && p.NgayVao >= _tungay && p.NgayVao <= _denngay)
                                               join bn in _Data.BenhNhans.Where(p => p.NoiTru == 1) on vv.MaBNhan equals bn.MaBNhan
                                               select new
                                               {

                                               }).Count();

                var TongSoLuotDieuTriNoiTruBHYT = (from vv in _Data.VaoViens.Where(p => p.MaKP == khoaphong.MaKP && p.NgayVao >= _tungay && p.NgayVao <= _denngay)
                                                   join bn in _Data.BenhNhans.Where(p => p.NoiTru == 1 && p.DTuong == "BHYT") on vv.MaBNhan equals bn.MaBNhan
                                                   select new
                                                   {

                                                   }).Count();

                var TongSoLuotDieuTriNoiTru6 = (from vv in _Data.VaoViens.Where(p => p.MaKP == khoaphong.MaKP && p.NgayVao >= _tungay && p.NgayVao <= _denngay)
                                                join bn in _Data.BenhNhans.Where(p => p.NoiTru == 1 && p.Tuoi < 6) on vv.MaBNhan equals bn.MaBNhan
                                                select new
                                                {

                                                }).Count();
                var TongSoLuotDieuTriNoiTru60 = (from vv in _Data.VaoViens.Where(p => p.MaKP == khoaphong.MaKP && p.NgayVao >= _tungay && p.NgayVao <= _denngay)
                                                 join bn in _Data.BenhNhans.Where(p => p.NoiTru == 1 && p.Tuoi >= 60) on vv.MaBNhan equals bn.MaBNhan
                                                 select new
                                                 {

                                                 }).Count();

                var TongSoLuotDieuTriNoiTruHoNgheo = (from vv in _Data.VaoViens.Where(p => p.MaKP == khoaphong.MaKP && p.NgayVao >= _tungay && p.NgayVao <= _denngay)
                                                      join bn in _Data.BenhNhans.Where(p => p.NoiTru == 1 && p.SThe.Contains("HN")) on vv.MaBNhan equals bn.MaBNhan
                                                      select new
                                                      {

                                                      }).Count();
                var SoNguoiBenhVaoKhoa = (from vv in _Data.VaoViens.Where(p => p.MaKP == khoaphong.MaKP && p.NgayVao >= _tungay && p.NgayVao <= _denngay)
                                          select new
                                          {

                                          }).Count();

                var RaVienKhoi = (from bnrv in _Data.RaViens.Where(p => p.MaKP == khoaphong.MaKP && p.KetQua.Contains("Khỏi") && p.NgayRa >= _tungay && p.NgayRa <= _denngay)
                                  join bn in _Data.BenhNhans.Where(p => p.NoiTru == 1)on bnrv.MaBNhan equals bn.MaBNhan
                                  select new
                                  {

                                  }).Count();
                var RaVienDoGiam = (from bnrv in _Data.RaViens.Where(p => p.MaKP == khoaphong.MaKP && p.KetQua.Contains("Đỡ|Giảm") && p.NgayRa >= _tungay && p.NgayRa <= _denngay)
                                    join bn in _Data.BenhNhans.Where(p => p.NoiTru == 1) on bnrv.MaBNhan equals bn.MaBNhan
                                    select new
                                    {

                                    }).Count();
                var RaVienKhongDoi = (from bnrv in _Data.RaViens.Where(p => p.MaKP == khoaphong.MaKP && p.KetQua.Contains("Không T.đổi") && p.NgayRa >= _tungay && p.NgayRa <= _denngay)
                                      join bn in _Data.BenhNhans.Where(p => p.NoiTru == 1) on bnrv.MaBNhan equals bn.MaBNhan
                                      select new
                                      {

                                      }).Count();
                var RaVienNangHon = (from bnrv in _Data.RaViens.Where(p => p.MaKP == khoaphong.MaKP && p.KetQua.Contains("Nặng hơn") && p.NgayRa >= _tungay && p.NgayRa <= _denngay)
                                     join bn in _Data.BenhNhans.Where(p => p.NoiTru == 1) on bnrv.MaBNhan equals bn.MaBNhan
                                     select new
                                     {

                                     }).Count();
                var RaVienTuVong = (from bnrv in _Data.RaViens.Where(p => p.MaKP == khoaphong.MaKP && p.KetQua.Contains("Tử vong") && p.NgayRa >= _tungay && p.NgayRa <= _denngay)
                                    join bn in _Data.BenhNhans.Where(p => p.NoiTru == 1) on bnrv.MaBNhan equals bn.MaBNhan
                                    select new
                                    {

                                    }).Count();

                var SoNBRaVien = (from rv in _Data.BenhNhans.Where(p => p.NoiTru == 1)
                                  join bnrv in _Data.RaViens.Where(p => p.MaKP == khoaphong.MaKP && p.NgayRa >= _tungay && p.NgayRa <= _denngay) on rv.MaBNhan equals bnrv.MaBNhan
                                  select new
                                  {
                                      bnrv.SoNgaydt,
                                  });

                var SoNBChuyenVien = (from rv in b.Where(p => p.NoiTru == 1)
                                      join bnrv in _Data.RaViens.Where(p => p.Status == 1 && p.MaKP == khoaphong.MaKP && p.NgayVao >= _tungay && p.NgayVao <= _denngay) on rv.MaBNhan equals bnrv.MaBNhan
                                      select new
                                      {
                                          bnrv.SoNgaydt,
                                      }).Count();
                var TongSoThuThuatCLS = (from cd in _Data.ChiDinhs.Where(p => p.NgayTH >= _tungay && p.NgayTH <= _denngay)
                                         join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                                         join tn in _Data.TieuNhomDVs.Where(p => p.TenRG == "Thủ thuật") on dv.IdTieuNhom equals tn.IdTieuNhom
                                         select new
                                         {

                                         }).Count();

                int songaydieutribenhnhanravien = 0;
                foreach (var item in SoNBRaVien)
                {
                    songaydieutribenhnhanravien += Convert.ToInt32(item.SoNgaydt);
                }

                double songaydtTB = 0;
                if (SoNBRaVien.Count() > 0)
                {
                    songaydtTB = songaydieutribenhnhanravien / SoNBRaVien.Count();
                }

                var TongSoNBTuVongTaiVien = (from bnrv in _Data.RaViens.Where(p => p.MaKP == khoaphong.MaKP && p.Status == 2 && p.KetQua.Contains("Tử vong") && p.NgayRa >= _tungay && p.NgayRa <= _denngay)
                                             select new
                                             {

                                             }).Count();
                var TongSoThuThuat1 = (from bn in _Data.BenhNhans.Where(p => p.NoiTru == 1)
                                       join cls in _Data.CLS.Where(p => p.MaKP == khoaphong.MaKP) on bn.MaBNhan equals cls.MaBNhan
                                       join cd in _Data.ChiDinhs.Where(p => p.NgayTH >= _tungay && p.NgayTH <= _denngay) on cls.IdCLS equals cd.IdCLS
                                       join dv in _Data.DichVus.Where(p => p.IdTieuNhom == 22) on cd.MaDV equals dv.MaDV
                                       join tn in _Data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                       select new
                                       {
                                           cls.MaBNhan,
                                           cd.IdCLS,
                                       }).GroupBy(p => p.IdCLS).Count();
                var TongSoNBDieuTriYHCT = (from bnkb in _Data.BNKBs.Where(p => p.NgayKham >= _tungay && p.NgayKham <= _denngay && p.MaKP == 21 && p.MaKPdt == 9 && p.PhuongAn == 1)
                                           select new
                                           {

                                           }).Count();

                var TongSoDieuTriNguoiTanTat = (from bnkb in _Data.BNKBs.Where(p => p.NgayKham >= _tungay && p.NgayKham <= _denngay && p.MaKP == khoaphong.MaKP)
                                                select new
                                                {

                                                }).Count();

                
                Dictionary<string, object> _dic = new Dictionary<string, object>();
                string TenBaoCao = "BÁO CÁO " + cbKP.Text;
                string s = "Từ Ngày" + lupNgaytu.Text + " Đến Ngày " + lupNgayden.Text;
                _dic.Add("TenBaoCao", TenBaoCao.ToUpper());
                _dic.Add("Ngaythang1", s.ToUpper());
                _dic.Add("TongGiuong", TongGiuong);
                _dic.Add("SoBenhNhanTon", SoBanhNhanTon1 + SoBanhNhanTon2);
                _dic.Add("SoBenhNhanTonCuoiThang", SoBanhNhanTonCuoiThang1 + SoBanhNhanTonCuoiThang2);
                
                _dic.Add("TongSoLuotDieuTriNoiTru", TongSoLuotDieuTriNoiTru);
                _dic.Add("TongSoLuotDieuTriNoiTruBHYT", TongSoLuotDieuTriNoiTruBHYT);
                _dic.Add("TongSoLuotDieuTriNoiTru6", TongSoLuotDieuTriNoiTru6);
                _dic.Add("TongSoLuotDieuTriNoiTru60", TongSoLuotDieuTriNoiTru60);
                _dic.Add("TongSoLuotDieuTriNoiTruHoNgheo", TongSoLuotDieuTriNoiTruHoNgheo);
                _dic.Add("SoNguoiBenhVaoKhoa", SoNguoiBenhVaoKhoa);

                _dic.Add("RaVienKhoi", RaVienKhoi);
                _dic.Add("RaVienDoGiam", RaVienDoGiam);
                _dic.Add("RaVienKhongDoi", RaVienKhongDoi);
                _dic.Add("RaVienNangHon", RaVienNangHon);
                _dic.Add("RaVienTuVong", RaVienTuVong);

                _dic.Add("SoNBChuyenVien", SoNBChuyenVien);

                _dic.Add("SoNgayDieuTri", songaydtTB);
                _dic.Add("SoNBRaVien", SoNBRaVien.Count());
                _dic.Add("SoNgayDieuTriNBRaVien", songaydieutribenhnhanravien);
                _dic.Add("SoNgayDieuTriTB", Math.Round(songaydtTB, 0));
                _dic.Add("TongSoNBTuVong", RaVienTuVong);
                _dic.Add("TongSoThuThuat1", TongSoThuThuat1);

                _dic.Add("TongSoNBDieuTriYHCT", TongSoNBDieuTriYHCT);
                _dic.Add("TongSoThuThuatCLS", TongSoThuThuatCLS);
                _dic.Add("TongSoNBTuVongTaiVien", TongSoNBTuVongTaiVien);
                _dic.Add("TongSoDieuTriNguoiTanTat", TongSoDieuTriNguoiTanTat);
                DungChung.Ham.Print(DungChung.PrintConfig.Rep_BCKhoaLamSang24012, b.ToList(), _dic, false);
            }
                
        }
        public int tachchuoi(string s)
        {
            int year = DateTime.Now.Year;
            //int sogiuong = 0;
            string sogiuong = "";
            string[] arr = s.Split('|');

            for (int i = 0; i < arr.Count(); i++)
            {
                if (arr[i] != "")
                {
                    string[] arrGi = arr[i].Split(':');
                    if (year == Convert.ToInt32(arrGi[0].ToString()))
                    {
                        //sogiuong = Convert.ToInt32(arrGi[1].ToString());
                        sogiuong = Convert.ToString(arrGi[1].ToString());
                    }
                }

            }
            //return sogiuong;
            return 0;
        }
        private void frm_BaoCao_Load(object sender, EventArgs e)
        {
            if(DungChung.Bien.MaBV == "24012")
            {
                panel2.Visible = true;
                lupNgaytu.DateTime = System.DateTime.Now;
                lupNgayden.DateTime = System.DateTime.Now;
            }
           
            var ba = _Data.KPhongs.Where(p => p.PLoai == "Lâm sàng").ToList();
            foreach (var item in ba)
            {
                cbKP.Items.Add(item.TenKP);
            }

            if (DungChung.Bien.check == 1)
            {
                radioGroup1.SelectedIndex = 0;
            }
            else if (DungChung.Bien.check == 2)
            {
                radioGroup1.SelectedIndex = 1;
            }
            int thisYear = DateTime.Now.Year;
            var b = (from kp in _Data.KPhongs.Where(p => p.PLoai.Contains("Lâm sàng"))
                     select new
                     {
                         kp.MaKP,
                         kp.TenKP
                     }).ToList();
            //LupKhoaphong.Properties.DataSource = b;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex == 1)
            {
                label1.Visible = true;
                cbKP.Visible = true;
            }
            else
            {
                label1.Visible = false;
                cbKP.Visible = false;
            }
            
        }
    }
}