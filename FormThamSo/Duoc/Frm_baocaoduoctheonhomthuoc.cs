using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormThamSo
{
    public partial class Frm_baocaoduoctheonhomthuoc : DevExpress.XtraEditors.XtraForm
    {
        public Frm_baocaoduoctheonhomthuoc()
        {
            InitializeComponent();
        }

        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private class KPhong
        {
            private string TenKP;
            private int MaKP;
            private bool Chon;
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
            public bool chon
            { set { Chon = value; } get { return Chon; } }
        }
        List<KPhong> _Kphong = new List<KPhong>();
        private void Frm_TKthuocnhap_Load(object sender, EventArgs e)
        {
            Tungay.DateTime = DateTime.Now;
            Denngay.DateTime = DateTime.Now;
            var kphong1 = (from kp in _data.KPhongs.Where(p => DungChung.Bien.MaBV == "20001" ? (p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc || p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc) : p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc) select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong1.Count > 0)
            {
                KPhong themmoi1 = new KPhong();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = true;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong1)
                {
                    KPhong themmoi = new KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DateTime tu = DungChung.Ham.NgayTu(Tungay.DateTime);
            DateTime den = DungChung.Ham.NgayDen(Denngay.DateTime);
            List<KPhong> kphong = new List<KPhong>();
            List<int> listKP = new List<int>();
            kphong = _Kphong.Where(p => p.chon == true).Where(p => p.makp > 0).ToList();
            listKP = kphong.Select(p => p.makp).ToList();
            var ndd = (from a in _data.NhapDs.Where(p => p.NgayNhap >= tu && p.NgayNhap <= den)
                       join b in _data.NhapDcts on a.IDNhap equals b.IDNhap
                       select new
                       {
                           a.IDNhap,
                           a.MaKP,
                           a.PLoai,
                           a.KieuDon,
                           a.NgayNhap,
                           b,
                           b.MaDV,
                           a.XuatTD,

                           SoLuongN = a.PLoai == 1 ? b.SoLuongN : 0,

                           SoLuongX = (a.PLoai == 2 || a.PLoai == 3) ? b.SoLuongX : 0,

                           ThanhTienN = a.PLoai == 1 ? b.ThanhTienN : 0,

                           ThanhTienX = (a.PLoai == 2 || a.PLoai == 3) ? b.ThanhTienX : 0,
                           b.IDDTBN
                       });
            var dichvu = (from h in _data.DichVus.Where(p => p.PLoai == 1)
                          join h1 in _data.TieuNhomDVs on h.IdTieuNhom equals h1.IdTieuNhom
                          join h2 in _data.NhomDVs on h.IDNhom equals h2.IDNhom
                          select new { h.MaDV, h1.TenRG, h2.TenNhom, h.NuocSX, h2.TenNhomCT, h1.TenTN, h.IDNhom, h.NguonGoc, h.DongY, h.TinhTNhap }).ToList();
            if (rad_MauIn.SelectedIndex == 0)
            {
                BaoCao.rep_bcduoctheonhomthuoc repbaocao = new BaoCao.rep_bcduoctheonhomthuoc();
                frmIn inn = new frmIn();

                repbaocao.TUngay.Value = Tungay.DateTime.ToShortDateString();
                repbaocao.Denngay.Value = Denngay.DateTime.ToShortDateString();
                if (kphong.Count > 0)
                {

                    var nhapd = (from kp in kphong
                                 join nd in ndd.Where(p => p.PLoai == 2 && p.KieuDon != 2) on kp.makp equals nd.MaKP
                                 select new { nd.b.MaDV, nd.b.SoLuongX, nd.b.ThanhTienX, nd.SoLuongN, nd.ThanhTienN }).ToList();
                    if (DungChung.Bien.MaBV == "27023")
                    {
                        nhapd = (from kp in kphong
                                 join nd in ndd.Where(p => p.PLoai == 2 || (p.PLoai == 1 && p.KieuDon == 2)) on kp.makp equals nd.MaKP
                                 select new { nd.b.MaDV, nd.b.SoLuongX, nd.b.ThanhTienX, nd.b.SoLuongN, nd.b.ThanhTienN }).ToList();
                    }
                    var ketquad = from h in nhapd
                                  join h1 in dichvu on h.MaDV equals h1.MaDV
                                  select new { id = 1, h.SoLuongX, h1.TenRG, h1.NuocSX, h1.TenNhomCT, h1.TenTN, h.ThanhTienX, h.SoLuongN, h.ThanhTienN };
                    // ket qua thuoc khanh sinh vitamin 
                    var ketqua = ketquad.GroupBy(p => new { p.TenRG }).Select(p => new { p.Key.TenRG, xuat = DungChung.Bien.MaBV == "27023" ? ((p.Sum(k => k.ThanhTienX) - p.Sum(k => k.SoLuongN)) / 1000) : (p.Sum(k => k.ThanhTienX) / 1000) }).ToList();
                    if (ketqua.Where(p => p.TenRG == "Thuốc thường (kháng sinh)").Count() > 0)
                    {
                        repbaocao.ketquas[0] = ketqua.Where(p => p.TenRG == "Thuốc thường (kháng sinh)").First().xuat;
                    }
                    if (ketqua.Where(p => p.TenRG == "Thuốc thường (Vitamin)").Count() > 0)
                    {
                        repbaocao.ketquas[1] = ketqua.Where(p => p.TenRG == "Thuốc thường (Vitamin)").First().xuat;
                    }

                    // ketqua thuoc coticod thuoc me 
                    var ketqua1 = ketquad.Where(p => p.TenTN == "Corticod" || p.TenTN == "Thuocme").GroupBy(p => new { p.TenTN }).Select(p => new { p.Key.TenTN, xuat = p.Sum(k => k.ThanhTienX) / 1000 });
                    if (ketqua1.Where(p => p.TenTN == "Corticod").Count() > 0)
                    {
                        repbaocao.ketquas[2] = ketqua1.Where(p => p.TenTN == "Corticod").First().xuat;
                    }
                    if (ketqua1.Where(p => p.TenTN == "Thuocme").Count() > 0)
                    {
                        repbaocao.ketquas[3] = ketqua1.Where(p => p.TenTN == "Thuocme").First().xuat;
                    }

                    // hoachat
                    var ketqua2 = ketquad.Where(p => p.TenNhomCT == "Nhóm hóa chất").GroupBy(p => new { p.TenNhomCT }).Select(p => new { p.Key.TenNhomCT, xuat = p.Sum(k => k.ThanhTienX) / 1000 });
                    if (ketqua2.Count() > 0)
                    {
                        repbaocao.ketquas[4] = ketqua2.First().xuat;
                    }
                    // thuocs dich chuyeen
                    var ketqua3 = ketquad.Where(p => p.TenNhomCT == "Thuốc trong danh mục BHYT").GroupBy(p => new { p.TenNhomCT }).Select(p => new { p.Key.TenNhomCT, Xuat = p.Sum(k => k.ThanhTienX) / 1000 });
                    if (ketqua3.Count() > 0)
                    {
                        repbaocao.ketquas[5] = ketqua3.First().Xuat;
                    }

                    // thuoc san xuat trong trong nuowc
                    var ketqua4 = ketquad.Where(p => p.NuocSX == "Vietnam" || p.NuocSX == "vietnam" || p.NuocSX == "Việt nam" || p.NuocSX == "Việt Nam").GroupBy(p => new { p.id }).Select(p => new { p.Key.id, Xuat = p.Sum(k => k.ThanhTienX) / 1000 });
                    if (ketqua4.Count() > 0)
                    {
                        repbaocao.ketquas[12] = ketqua4.First().Xuat;
                    }

                    // thuốc san xuát ngoài nước
                    var ketqua5 = ketquad.Where(p => p.NuocSX != "Vietnam" || p.NuocSX != "vietnam" || p.NuocSX != "Việt nam" || p.NuocSX == "Việt Nam").GroupBy(p => new { p.id }).Select(p => new { p.Key.id, Xuat = p.Sum(k => k.ThanhTienX) / 1000 });
                    if (ketqua5.Count() > 0)
                    {
                        repbaocao.ketquas[13] = ketqua5.First().Xuat;
                    }

                }
                else
                {

                    XtraMessageBox.Show("Chưa chọn kho", "Thông báo");
                    return;

                }

                repbaocao.loat();
                repbaocao.CreateDocument();
                inn.prcIN.PrintingSystem = repbaocao.PrintingSystem;
                inn.ShowDialog();


            } // in mau bao cao cong tac khoa duoc
            else if (rad_MauIn.SelectedIndex == 1)
            {
                #region 01071
                if (DungChung.Bien.MaBV == "01071")
                {
                    BaoCao.rep_BcCTKhoaDuoc_01071 repbaocao = new BaoCao.rep_BcCTKhoaDuoc_01071();
                    frmIn inn = new frmIn();
                    repbaocao.ngaybatdau = Tungay.DateTime;
                    repbaocao.ngayketthuc = Denngay.DateTime;
                    if (kphong.Count > 0)
                    {

                        //nhập dược
                        var nhapduoc = (from kp in kphong
                                        join nd in ndd.Where(p => p.PLoai == 1 && p.KieuDon == 1) on kp.makp equals nd.MaKP
                                        join dv in dichvu on nd.b.MaDV equals dv.MaDV
                                        select new { nd.b.MaDV, nd.b.SoLuongN, nd.b.ThanhTienN, dv.TenRG, TrongNuoc = (dv.NuocSX.ToLower().Contains("vietnam") || dv.NuocSX.ToLower().Contains("việt nam") || dv.NuocSX.ToLower().Contains("vn")) ? "Trong nước" : "Nước ngoài", dv.IDNhom, dv.TenNhomCT }).ToList();
                        // xuất dược
                        var nhapd = (from kp in kphong
                                     join nd in ndd.Where(p => p.PLoai == 2 && p.KieuDon != 2) on kp.makp equals nd.MaKP
                                     select new { nd.MaDV, nd.SoLuongX, nd.ThanhTienX, nd.b.MaBNhan, nd.IDDTBN, nd.SoLuongN, nd.ThanhTienN }).ToList();
                        
                        var ketquad = (from h in nhapd
                                       join h1 in dichvu on h.MaDV equals h1.MaDV
                                       select new { id = 1, h.SoLuongX, h.MaBNhan, h1.TenRG, h1.NuocSX, h1.TenNhomCT, h1.TenTN, h.ThanhTienX, h1.IDNhom, h.IDDTBN, h.SoLuongN, h.ThanhTienN }).ToList();
                        var vp = (from nd in _data.VienPhis.Where(p => p.NgayTT >= tu && p.NgayTT <= den)/*.Where(p => listKP.Contains(p.MaKP ?? 0))*/
                                  join ndct in _data.VienPhicts on nd.idVPhi equals ndct.idVPhi
                                  select new { nd.MaBNhan, ndct.ThanhTien, ndct.MaDV }).ToList();

                        var dvv = (from vpp in vp
                                   join dv in dichvu on vpp.MaDV equals dv.MaDV
                                   select new { dv.MaDV, dv.TenRG, TrongNuoc = (dv.NuocSX.ToLower().Contains("vietnam") || dv.NuocSX.ToLower().Contains("việt nam") || dv.NuocSX.ToLower().Contains("vn")) ? "Trong nước" : "Nước ngoài", dv.IDNhom, dv.TenNhomCT, vpp.ThanhTien, vpp.MaBNhan }).ToList();
                        double tienvp = vp.Sum(p => p.ThanhTien);
                        //Tổng số tiền mua thuốc
                        //var kqThuocNhap = nhapduoc.Where(p => p.TenRG != "Hóa chất" && p.TenRG != "Vắc xin, sinh phẩm").Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6).ToList();
                        //if (kqThuocNhap.Count > 0)
                        //{
                        //    repbaocao.tongtienthuocnt.Text = (kqThuocNhap.Sum(p => p.ThanhTienN) / 1000).ToString("##,###");
                        //}

                        //tổng tiền thuốc trong và ngoài nước
                        var Kq1 = (from a in nhapduoc.Where(p => p.TenRG != "Hóa chất").Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6)
                                   group a by new { a.TrongNuoc, a.MaDV } into kq
                                   select new
                                   {
                                       kq.Key.TrongNuoc,
                                       kq.Key.MaDV,
                                       ThanhTien = kq.Sum(p => p.ThanhTienN)
                                   }).ToList();
                        if (Kq1.Count > 0)
                        {
                            double TrongN = 0, NgoaiN = 0 , tongmuat = 0;
                            TrongN = Kq1.Where(p => p.TrongNuoc == "Trong nước").Sum(p => p.ThanhTien);
                            NgoaiN = Kq1.Where(p => p.TrongNuoc == "Nước ngoài").Sum(p => p.ThanhTien);
                            tongmuat = TrongN + NgoaiN;
                            repbaocao.tienthuocnn.Text = (NgoaiN / 1000).ToString("##,###");
                            repbaocao.tienthuoctn.Text = (TrongN / 1000).ToString("##,###");
                            repbaocao.tongtienthuocnt.Text = (tongmuat / 1000).ToString("##,###");
                        }

                        //Tổng số tiền mua hóa chất
                        var kqHoaChat = nhapduoc.Where(p => p.TenRG == "Hóa chất").ToList();
                        if (kqHoaChat.Count > 0)
                        {
                            repbaocao.hoachat.Text = (kqHoaChat.Sum(p => p.ThanhTienN) / 1000).ToString("##,###");
                            if (DungChung.Bien.MaBV == "27022")
                            {
                                repbaocao.xrTableCell30.Text = (kqHoaChat.Sum(p => p.ThanhTienN) * 100 / tienvp).ToString("##,###.##");
                                repbaocao.xrTableCell30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                            }
                        }

                        //Vật tư y tế tiêu hao

                        double vtyt_tieuhao_tong = 0;
                        double vtyt_vtth = 0;
                        double vtyt_vacxin = 0;


                        var kqvtyt = nhapduoc.Where(p => p.TenRG == "Vật tư y tế tiêu hao").ToList();
                        if (kqvtyt.Count > 0)
                        {
                            string rs = (kqvtyt.Sum(p => p.ThanhTienN) / 1000).ToString("##,###");
                            repbaocao.vtytth.Text = rs;
                            vtyt_vtth = Convert.ToDouble(rs);
                        }

                        //kq vacxin 
                        var vacxin_sinhpham = nhapduoc.Where(p => p.TenRG == "Vắc xin, sinh phẩm").ToList();
                        if (vacxin_sinhpham.Count > 0)
                        {
                            string rs = (vacxin_sinhpham.Sum(p => p.ThanhTienN) / 1000).ToString("##,###");
                            repbaocao.xrTableCell44.Text = rs;
                            if (!string.IsNullOrEmpty(rs))
                                vtyt_vacxin = Convert.ToDouble(rs);
                        }

                        vtyt_tieuhao_tong = vtyt_vtth + vtyt_vacxin;
                        repbaocao.vattu.Text = vtyt_tieuhao_tong.ToString("##,###");


                        //Tổng số tiền  thuốc đã sử dụng
                        var kq4 = ketquad.Where(p => p.TenRG != "Hóa chất").Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6).ToList();
                        if (kq4.Count > 0)
                        {
                            //repbaocao.Tongtiensd.Text = ((kq4.Sum(p => p.ThanhTienX) - kq4.Sum(p => p.ThanhTienN)) / 1000).ToString("##,###");
                            // ket qua thuoc khanh sinh 
                            double tienthuocsd = 0;//Tổng tiền thuốc đã sử dụng
                            double tt = 0;// Tiền thuốc của từng loại
                            double ttt = 0, tongtiensd_01071 = 0; ;

                            var kq41 = ketquad.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_khangsinh).ToList();
                            if (kq41.Count > 0)
                            {
                                tt = kq41.Sum(p => p.ThanhTienX) / 1000;
                                tienthuocsd += tt;
                                repbaocao.tienthuocks.Text = tt.ToString("##,###");
                                tongtiensd_01071 += tt;
                                ttt = (tt * 100 / (kq4.Sum(p => p.ThanhTienX) / 1000));
                            }

                            
                            var kq42_01071 = ketquad.Where(p => p.TenTN == "Nhóm thuốc vitamin").ToList();
                           

                            if (kq42_01071.Count > 0 && DungChung.Bien.MaBV == "01071")
                            {
                                tt = kq42_01071.Sum(p => p.ThanhTienX) / 1000;
                                tienthuocsd += tt;
                                repbaocao.tienthuocvitamin.Text = tt.ToString("##,###");
                                tongtiensd_01071 += tt;
                                ttt = (tt * 100 / (kq4.Sum(p => p.ThanhTienX) / 1000));
                            }

                            // ketqua thuoc dịch truyền
                            var kq43 = ketquad.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_DT).ToList();
                            
                            if (kq43.Count > 0)
                            {
                                tt = kq43.Sum(p => p.ThanhTienX) / 1000;
                                tienthuocsd += tt;
                                repbaocao.tienthuocdichchuyen.Text = tt.ToString("##,###");
                                tongtiensd_01071 += tt;
                                ttt = (tt * 100 / (kq4.Sum(p => p.ThanhTienX) / 1000));
                            }


                            // ketqua thuoc coticod thuoc me 
                            //var kq44 = ketquad.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_Corticoid).ToList();
                            var kq44_01071 = ketquad.Where(p => p.TenTN == "Nhóm thuốc corticoid").ToList();
                          
                            if (kq44_01071.Count > 0)
                            {
                                tt = kq44_01071.Sum(p => p.ThanhTienX) / 1000;
                                tienthuocsd += tt;
                                repbaocao.tienthuoccorticoid.Text = tt.ToString("##,###");
                                tongtiensd_01071 += tt;
                                ttt = (tt * 100 / (kq4.Sum(p => p.ThanhTienX) / 1000));
                                
                            }

                           
                            //
                            tt = ((DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001") ? ((kq4.Sum(p => p.ThanhTienX) - kq4.Sum(p => p.ThanhTienN)) / 1000) : (kq4.Sum(p => p.ThanhTienX) / 1000)) - tienthuocsd;
                            //tt = (kq4.Sum(p => p.ThanhTienX) / 1000) - tienthuocsd;
                            repbaocao.tienthuockhac.Text = tt.ToString("##,###");
                            tongtiensd_01071 += tt;
                            ttt = (tt * 100 / (kq4.Sum(p => p.ThanhTienX) / 1000));
                            repbaocao.Tongtiensd.Text = tongtiensd_01071.ToString("##,###");
                        }
                        
                        //Tiền máu
                        if (DungChung.Bien.MaBV == "01071")
                        {
                            var kq5 = dvv.Where(p => p.IDNhom == 7).ToList();
                            repbaocao.celTienMau.Text = ((kq5.Sum(p => p.ThanhTien) / 1000)).ToString("##,###");
                        }
                        else
                        {
                            var kq5 = ketquad.Where(p => p.IDNhom == 7).ToList();
                            repbaocao.celTienMau.Text = ((DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001") ? ((kq5.Sum(p => p.ThanhTienX) - kq5.Sum(p => p.ThanhTienN)) / 1000) : (kq5.Sum(p => p.ThanhTienX) / 1000)).ToString("##,###");
                        }

                        

                        //Các nguồn tiền thuốc đã sử dụng -- không cần lên
                        var nguonTThuoc = (from nd in ketquad.Where(p => p.TenRG != "Hóa chất").Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6)
                                           join bn in _data.BenhNhans on nd.MaBNhan equals bn.MaBNhan
                                           select new { nd.MaBNhan, nd.ThanhTienX, nd.ThanhTienN, nd.TenRG, nd.IDNhom, SThe = bn.SThe, MaDTuong = bn.MaDTuong, Tuoi = bn.Tuoi, DTuong = bn.DTuong }).ToList();

                        //tong
                        double tong = ((kq4.Sum(p => p.ThanhTienX) - kq4.Sum(p => p.ThanhTienN)) / 1000);
                        repbaocao.cel6.Text = tong.ToString("##,###");
                        //tiền thuốc BHYT
                        double tt_bh = 0;
                        var TT_BHYT = nguonTThuoc.Where(p => (p.MaDTuong != "TE" && p.Tuoi >= 6) && p.MaDTuong != "HN" && p.DTuong == "BHYT").ToList();
                        if (TT_BHYT.Count > 0)
                        {
                            tt_bh = ((TT_BHYT.Sum(p => p.ThanhTienX) - TT_BHYT.Sum(p => p.ThanhTienN)) / 1000);
                            repbaocao.cel61.Text = tt_bh.ToString("##,###");
                        }
                        //tiền thuốc TE > 6 BHYT

                        var kq62 = nguonTThuoc.Where(p => p.MaDTuong == "TE" && p.DTuong == "BHYT" && p.Tuoi < 6).ToList();
                        double treem = 0;
                        if (kq62.Count > 0)
                        {
                            treem = ((kq62.Sum(p => p.ThanhTienX) - kq62.Sum(p => p.ThanhTienN)) / 1000);
                            repbaocao.cel62.Text = treem.ToString("##,###");
                        }
                        //tiền thuốc HN
                        var kq63 = nguonTThuoc.Where(p => p.MaDTuong == "HN" && p.DTuong == "BHYT").ToList();
                        double hongheo = 0;
                        if (kq63.Count > 0)
                        {
                            hongheo = ((kq63.Sum(p => p.ThanhTienX) - kq63.Sum(p => p.ThanhTienN)) / 1000);
                            repbaocao.cel63.Text = hongheo.ToString("##,###");
                        }
                        //tiền thuốc dịch vụ
                        var kq64 = nguonTThuoc.Where(p => p.DTuong == "Dịch vụ").ToList();
                        double tongDV = 0;
                        if (kq64.Count > 0)
                        {
                            //tongDV = ((kq64.Sum(p => p.ThanhTienX) - kq64.Sum(p => p.ThanhTienN)) / 1000);

                            tongDV = tong - tt_bh;
                            repbaocao.cel64.Text = tongDV.ToString("##,###");
                        }
                    }
                    else
                    {

                        XtraMessageBox.Show("Chưa chọn kho", "Thông báo");
                        return;

                    }
                    repbaocao.ks[0] = memoTuDG.Text;
                    repbaocao.ks[1] = memoTuNX.Text;
                    repbaocao.ks[2] = memoKienNghi.Text;
                    repbaocao.hamin();
                    repbaocao.CreateDocument();
                    inn.prcIN.PrintingSystem = repbaocao.PrintingSystem;
                    inn.ShowDialog();
                }
                #endregion

                #region 01071 bv khác
                else
                {
                    BaoCao.rep_BcCTKhoaDuoc repbaocao = new BaoCao.rep_BcCTKhoaDuoc();
                    frmIn inn = new frmIn();
                    repbaocao.ngaybatdau = Tungay.DateTime;
                    repbaocao.ngayketthuc = Denngay.DateTime;
                    //var _lThuoc = _data.DichVus.Where(p => p.PLoai == 1).ToList();
                    if (kphong.Count > 0)
                    {

                        //nhập dược
                        var nhapduoc = (from kp in kphong
                                        join nd in ndd.Where(p => p.PLoai == 1 && p.KieuDon == 1) on kp.makp equals nd.MaKP
                                        join dv in dichvu on nd.b.MaDV equals dv.MaDV
                                        select new { nd.b.MaDV, nd.b.SoLuongN, nd.b.ThanhTienN, dv.TenRG, TrongNuoc = (dv.NuocSX.ToLower().Contains("vietnam") || dv.NuocSX.ToLower().Contains("việt nam") || dv.NuocSX.ToLower().Contains("vn")) ? "Trong nước" : "Nước ngoài", dv.IDNhom, dv.TenNhomCT }).ToList();
                        // xuất dược
                        var nhapd = (from kp in kphong
                                     join nd in ndd.Where(p => p.PLoai == 2 && p.KieuDon != 2) on kp.makp equals nd.MaKP
                                     select new { nd.MaDV, nd.SoLuongX, nd.ThanhTienX, nd.b.MaBNhan, nd.IDDTBN, nd.SoLuongN, nd.ThanhTienN }).ToList();
                        if (DungChung.Bien.MaBV == "27023")
                        {
                            nhapd = (from kp in kphong
                                     join nd in ndd.Where(p => p.PLoai == 2 || (p.PLoai == 1 && p.KieuDon == 2)) on kp.makp equals nd.MaKP
                                     select new { nd.MaDV, nd.SoLuongX, nd.ThanhTienX, nd.b.MaBNhan, nd.IDDTBN, nd.SoLuongN, nd.ThanhTienN }).ToList();
                        }
                        if (DungChung.Bien.MaBV == "20001")
                        {
                            nhapd = (from kp in kphong
                                     join nd in ndd.Where(p => (p.PLoai == 2 && p.KieuDon != 2) || (p.PLoai == 1 && p.KieuDon == 2)) on kp.makp equals nd.MaKP
                                     select new { nd.MaDV, nd.SoLuongX, nd.ThanhTienX, nd.b.MaBNhan, nd.IDDTBN, nd.SoLuongN, nd.ThanhTienN }).ToList();
                        }
                        var ketquad = (from h in nhapd
                                       join h1 in dichvu on h.MaDV equals h1.MaDV
                                       select new { id = 1, h.SoLuongX, h.MaBNhan, h1.TenRG, h1.NuocSX, h1.TenNhomCT, h1.TenTN, h.ThanhTienX, h1.IDNhom, h.IDDTBN, h.SoLuongN, h.ThanhTienN }).ToList();
                        var vp = (from nd in _data.VienPhis.Where(p => p.NgayTT >= tu && p.NgayTT <= den)/*.Where(p => listKP.Contains(p.MaKP ?? 0))*/
                                  join ndct in _data.VienPhicts on nd.idVPhi equals ndct.idVPhi
                                  select new { nd.MaBNhan, ndct.ThanhTien, ndct.MaDV }).ToList();

                        var dvv = (from vpp in vp
                                   join dv in dichvu on vpp.MaDV equals dv.MaDV
                                   select new { dv.MaDV, dv.TenRG, TrongNuoc = (dv.NuocSX.ToLower().Contains("vietnam") || dv.NuocSX.ToLower().Contains("việt nam") || dv.NuocSX.ToLower().Contains("vn")) ? "Trong nước" : "Nước ngoài", dv.IDNhom, dv.TenNhomCT, vpp.ThanhTien, vpp.MaBNhan }).ToList();
                        double tienvp = vp.Sum(p => p.ThanhTien);
                        //Tổng số tiền mua thuốc
                        var kqThuocNhap = nhapduoc.Where(p => p.TenRG != "Hóa chất").Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6).ToList();
                        if (kqThuocNhap.Count > 0)
                        {
                            repbaocao.tongtienthuocnt.Text = (kqThuocNhap.Sum(p => p.ThanhTienN) / 1000).ToString("##,###");
                            if (DungChung.Bien.MaBV == "27022")
                            {
                                repbaocao.xrTableCell10.Text = (kqThuocNhap.Sum(p => p.ThanhTienN) * 100 / tienvp).ToString("##,###.##");
                                repbaocao.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                            }
                        }

                        //tổng tiền thuốc trong và ngoài nước
                        var Kq1 = (from a in nhapduoc.Where(p => p.TenRG != "Hóa chất").Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6)
                                   group a by new { a.TrongNuoc, a.MaDV } into kq
                                   select new
                                   {
                                       kq.Key.TrongNuoc,
                                       kq.Key.MaDV,
                                       ThanhTien = kq.Sum(p => p.ThanhTienN)
                                   }).ToList();
                        if (Kq1.Count > 0)
                        {
                            double TrongN = 0, NgoaiN = 0;
                            TrongN = Kq1.Where(p => p.TrongNuoc == "Trong nước").Sum(p => p.ThanhTien);
                            NgoaiN = Kq1.Where(p => p.TrongNuoc == "Nước ngoài").Sum(p => p.ThanhTien);
                            repbaocao.tienthuocnn.Text = (NgoaiN / 1000).ToString("##,###");
                            repbaocao.tienthuoctn.Text = (TrongN / 1000).ToString("##,###");
                            //if (DungChung.Bien.MaBV == "27022")
                            //{
                            //    repbaocao.xrTableCell25.Text = (TrongN / tienvp ).ToString("##,###");
                            //    repbaocao.xrTableCell20.Text = (NgoaiN / tienvp ).ToString("##,###");
                            //}
                        }

                        //Tổng số tiền mua hóa chất
                        var kqHoaChat = nhapduoc.Where(p => p.TenRG == "Hóa chất").ToList();
                        if (kqHoaChat.Count > 0)
                        {
                            repbaocao.hoachat.Text = (kqHoaChat.Sum(p => p.ThanhTienN) / 1000).ToString("##,###");
                            if (DungChung.Bien.MaBV == "27022")
                            {
                                repbaocao.xrTableCell30.Text = (kqHoaChat.Sum(p => p.ThanhTienN) * 100 / tienvp).ToString("##,###.##");
                                repbaocao.xrTableCell30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                            }
                        }

                        //Vật tư y tế tiêu hao
                        var kqvtyt = nhapduoc.Where(p => p.TenRG == "Vật tư y tế tiêu hao").ToList();
                        if (kqvtyt.Count > 0)
                        {
                            repbaocao.vattu.Text = (kqvtyt.Sum(p => p.ThanhTienN) / 1000).ToString("##,###");
                            if (DungChung.Bien.MaBV == "27022")
                            {
                                repbaocao.xrTableCell35.Text = (kqvtyt.Sum(p => p.ThanhTienN) * 100 / tienvp).ToString("##,###.##");
                                repbaocao.xrTableCell35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                            }
                        }

                        //Tổng số tiền  thuốc đã sử dụng
                        var kq4 = ketquad.Where(p => p.TenRG != "Hóa chất").Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6).ToList();
                        if (kq4.Count > 0)
                        {
                            repbaocao.Tongtiensd.Text = ((kq4.Sum(p => p.ThanhTienX) - kq4.Sum(p => p.ThanhTienN)) / 1000).ToString("##,###");
                            if (DungChung.Bien.MaBV == "27022")
                            {
                                repbaocao.xrTableCell50.Text = (kq4.Sum(p => p.ThanhTienX) * 100 / tienvp).ToString("##,###.##");
                                repbaocao.xrTableCell50.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                            }
                            // ket qua thuoc khanh sinh 
                            double tienthuocsd = 0;//Tổng tiền thuốc đã sử dụng
                            double tt = 0;// Tiền thuốc của từng loại
                            double ttt = 0;
                            var kq41 = ketquad.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_khangsinh).ToList();
                            if (kq41.Count > 0)
                            {
                                tt = (DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001") ? ((kq41.Sum(p => p.ThanhTienX) - kq41.Sum(p => p.ThanhTienN)) / 1000) : (kq41.Sum(p => p.ThanhTienX) / 1000);
                                tienthuocsd += tt;
                                repbaocao.tienthuocks.Text = tt.ToString("##,###");
                                ttt = (tt * 100 / (kq4.Sum(p => p.ThanhTienX) / 1000));
                                if (DungChung.Bien.MaBV == "27022")
                                {
                                    repbaocao.xrTableCell60.Text = ttt.ToString("##,###.##");
                                    repbaocao.xrTableCell60.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                }
                            }


                            var kq42 = ketquad.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_vitamin).ToList();
                            var kq42_01071 = ketquad.Where(p => p.TenTN == "Nhóm thuốc vitamin").ToList();
                            if (kq42.Count > 0)
                            {
                                tt = (DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001") ? ((kq42.Sum(p => p.ThanhTienX) - kq42.Sum(p => p.ThanhTienN)) / 1000) : (kq42.Sum(p => p.ThanhTienX) / 1000);
                                tienthuocsd += tt;
                                repbaocao.tienthuocvitamin.Text = tt.ToString("##,###");
                                ttt = (tt * 100 / (kq4.Sum(p => p.ThanhTienX) / 1000));
                                if (DungChung.Bien.MaBV == "27022")
                                {
                                    repbaocao.xrTableCell65.Text = ttt.ToString("##,###.##");
                                    repbaocao.xrTableCell65.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                }
                            }

                            if (kq42_01071.Count > 0 && DungChung.Bien.MaBV == "01071")
                            {
                                tt = (DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001") ? ((kq42_01071.Sum(p => p.ThanhTienX) - kq42_01071.Sum(p => p.ThanhTienN)) / 1000) : (kq42_01071.Sum(p => p.ThanhTienX) / 1000);
                                tienthuocsd += tt;
                                repbaocao.tienthuocvitamin.Text = tt.ToString("##,###");
                                ttt = (tt * 100 / (kq4.Sum(p => p.ThanhTienX) / 1000));
                            }

                            // ketqua thuoc dịch truyền
                            var kq43 = ketquad.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_DT).ToList();
                            if (DungChung.Bien.MaBV == "20001")
                            {
                                kq43 = ketquad.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_DT && p.TenTN.Contains("tiêm truyền")).ToList();
                            }
                            if (kq43.Count > 0)
                            {
                                tt = (DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001") ? ((kq43.Sum(p => p.ThanhTienX) - kq43.Sum(p => p.ThanhTienN)) / 1000) : (kq43.Sum(p => p.ThanhTienX) / 1000);
                                tienthuocsd += tt;
                                repbaocao.tienthuocdichchuyen.Text = tt.ToString("##,###");
                                ttt = (tt * 100 / (kq4.Sum(p => p.ThanhTienX) / 1000));
                                if (DungChung.Bien.MaBV == "27022")
                                {
                                    repbaocao.xrTableCell70.Text = ttt.ToString("##,###.##");
                                    repbaocao.xrTableCell70.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                }
                            }

                            // ketqua thuoc coticod thuoc me 
                            var kq44 = ketquad.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_Corticoid).ToList();
                            
                            if (kq44.Count > 0)
                            {
                                tt = (DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001") ? ((kq44.Sum(p => p.ThanhTienX) - kq44.Sum(p => p.ThanhTienN)) / 1000) : (kq44.Sum(p => p.ThanhTienX) / 1000);
                                tienthuocsd += tt;
                                repbaocao.tienthuoccorticoid.Text = tt.ToString("##,###");
                                ttt = (tt * 100 / (kq4.Sum(p => p.ThanhTienX) / 1000));
                                if (DungChung.Bien.MaBV == "27022")
                                {
                                    repbaocao.xrTableCell75.Text = ttt.ToString("##,###.##");
                                    repbaocao.xrTableCell75.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                }
                            }

                            

                            tt = ((DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001") ? ((kq4.Sum(p => p.ThanhTienX) - kq4.Sum(p => p.ThanhTienN)) / 1000) : (kq4.Sum(p => p.ThanhTienX) / 1000)) - tienthuocsd;
                            //tt = (kq4.Sum(p => p.ThanhTienX) / 1000) - tienthuocsd;
                            repbaocao.tienthuockhac.Text = tt.ToString("##,###");
                            ttt = (tt * 100 / (kq4.Sum(p => p.ThanhTienX) / 1000));
                            if (DungChung.Bien.MaBV == "27022")
                            {
                                repbaocao.xrTableCell80.Text = ttt.ToString("##,###.##");
                                repbaocao.xrTableCell80.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                            }
                        }

                        //Tiền máu
                        if (DungChung.Bien.MaBV == "01071")
                        {
                            var kq5 = dvv.Where(p => p.IDNhom == 7).ToList();
                            repbaocao.celTienMau.Text = ((kq5.Sum(p => p.ThanhTien) / 1000)).ToString("##,###");
                        }
                        else
                        {
                            var kq5 = ketquad.Where(p => p.IDNhom == 7).ToList();
                            repbaocao.celTienMau.Text = ((DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001") ? ((kq5.Sum(p => p.ThanhTienX) - kq5.Sum(p => p.ThanhTienN)) / 1000) : (kq5.Sum(p => p.ThanhTienX) / 1000)).ToString("##,###");
                        }



                        //Các nguồn tiền thuốc đã sử dụng -- không cần lên

                        var benhnhan = (from bn in _data.BenhNhans join vpp in _data.VienPhis on bn.MaBNhan equals vpp.MaBNhan select bn).ToList();
                        var nguonTThuoc = (from nd in dvv.Where(p => p.TenRG != "Hóa chất").Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6)
                                           join bn in benhnhan on nd.MaBNhan equals bn.MaBNhan
                                           select new { nd.MaBNhan, nd.ThanhTien, nd.TenRG, nd.IDNhom, SThe = bn.SThe, MaDTuong = bn.MaDTuong, Tuoi = bn.Tuoi, DTuong = bn.DTuong }).ToList();
                        repbaocao.cel6.Text = (nguonTThuoc.Sum(p => p.ThanhTien) / 1000).ToString("##,###");
                        //tiền thuốc BHYT
                        var TT_BHYT = nguonTThuoc.Where(p => (p.MaDTuong != "TE" && p.Tuoi >= 6) && p.MaDTuong != "HN" && p.DTuong == "BHYT").ToList();
                        if (TT_BHYT.Count > 0)
                        {
                            repbaocao.cel61.Text = (TT_BHYT.Sum(p => p.ThanhTien) / 1000).ToString("##,###");
                            if (DungChung.Bien.MaBV == "27022")
                            {
                                repbaocao.xrTableCell100.Text = (TT_BHYT.Sum(p => p.ThanhTien) / tienvp * 100).ToString("##,###.##");
                                repbaocao.xrTableCell100.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                            }
                        }
                        //tiền thuốc TE > 6 BHYT

                        var kq62 = nguonTThuoc.Where(p => p.MaDTuong == "TE" && p.DTuong == "BHYT" && p.Tuoi < 6).ToList();
                        if (kq62.Count > 0)
                        {
                            repbaocao.cel62.Text = (kq62.Sum(p => p.ThanhTien) / 1000).ToString("##,###");
                            if (DungChung.Bien.MaBV == "27022")
                            {
                                repbaocao.xrTableCell105.Text = (kq62.Sum(p => p.ThanhTien) / tienvp * 100).ToString("##,###.##");
                                repbaocao.xrTableCell105.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                            }
                        }
                        //tiền thuốc HN
                        var kq63 = nguonTThuoc.Where(p => p.MaDTuong == "HN" && p.DTuong == "BHYT").ToList();
                        if (kq63.Count > 0)
                        {
                            repbaocao.cel63.Text = (kq63.Sum(p => p.ThanhTien) / 1000).ToString("##,###");
                            if (DungChung.Bien.MaBV == "27022")
                            {
                                repbaocao.xrTableCell110.Text = (kq63.Sum(p => p.ThanhTien) / tienvp * 100).ToString("##,###.##");
                                repbaocao.xrTableCell110.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                            }
                        }
                        //tiền thuốc dịch vụ
                        var kq64 = nguonTThuoc.Where(p => p.DTuong == "Dịch vụ").ToList();
                        if (kq64.Count > 0)
                        {
                            repbaocao.cel64.Text = (kq64.Sum(p => p.ThanhTien) / 1000).ToString("##,###");
                            if (DungChung.Bien.MaBV == "27022")
                            {
                                repbaocao.xrTableCell115.Text = (kq64.Sum(p => p.ThanhTien) / tienvp * 100).ToString("##,###.##");
                                repbaocao.xrTableCell115.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                            }
                        }
                    }
                    else
                    {

                        XtraMessageBox.Show("Chưa chọn kho", "Thông báo");
                        return;

                    }
                    repbaocao.ks[0] = memoTuDG.Text;
                    repbaocao.ks[1] = memoTuNX.Text;
                    repbaocao.ks[2] = memoKienNghi.Text;
                    repbaocao.hamin();
                    repbaocao.CreateDocument();
                    inn.prcIN.PrintingSystem = repbaocao.PrintingSystem;
                    inn.ShowDialog();
                }
                #endregion
            }
            else
            { // mẫu y học cổ truyền
                BaoCao.rep_ctduoccotruyen_24009 repbaocao = new BaoCao.rep_ctduoccotruyen_24009();
                frmIn inn = new frmIn();
                repbaocao.lab_tungaytoingay.Text = "Từ: " + Tungay.DateTime.ToShortDateString() + " đến: " + Denngay.DateTime.ToShortDateString();
                int DTBHYT = -1;
                var dtbn = _data.DTBNs.Where(p => p.DTBN1 == "BHYT").Select(p => p.IDDTBN).ToList();
                if (dtbn.Count > 0)
                    DTBHYT = dtbn.First();
                if (kphong.Count > 0)
                {
                    var nhapd = (from nd in ndd
                                 select new { nd.PLoai, nd.KieuDon, nd.b.IDDTBN, nd.b.MaDV, nd.XuatTD, nd.MaKP, nd.b.SoLuongX, nd.b.ThanhTienX, nd.b.SoLuongN, nd.b.ThanhTienN }).ToList();
                    var ketquad = (from h in nhapd join h1 in dichvu on h.MaDV equals h1.MaDV select new { h.MaKP, h.IDDTBN, h.XuatTD, h.KieuDon, h1.NguonGoc, h1.DongY, h1.TinhTNhap, h.PLoai, h.SoLuongN, h.ThanhTienN, id = 1, h.SoLuongX, h1.TenRG, h1.NuocSX, h1.TenNhomCT, h1.TenTN, h.ThanhTienX }).ToList();
                    // ket qua thuoc khanh sinh vitamin 
                    var vp = (from nd in _data.VienPhis.Where(p => p.NgayTT >= tu && p.NgayTT <= den)
                              join ndct in _data.VienPhicts on nd.idVPhi equals ndct.idVPhi
                              select new { ndct.ThanhTien }).ToList();
                    double tienvp = vp.Sum(p => p.ThanhTien);
                    var ketqua = (from a in ketquad.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocDongY || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_CPYHCT)
                                  join kp in kphong on a.MaKP equals kp.makp
                                  select a).ToList();
                    //1.
                    double T_muaThuoc = ketqua.Where(p => p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.ThanhTienN);
                    repbaocao.cel_TSmuathuoc.Text = (T_muaThuoc / 1000).ToString("##,###");
                    if (tienvp > 0)
                        repbaocao.TL_TSTienMua.Text = (T_muaThuoc * 100 / tienvp).ToString("##,###.###");
                    T_muaThuoc = 0;
                    T_muaThuoc = ketqua.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_CPYHCT).Where(p => p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.ThanhTienN);
                    repbaocao.cel_chepham.Text = (T_muaThuoc / 1000).ToString("##,###");
                    if (tienvp > 0)
                        repbaocao.TL_MuaChePham.Text = (T_muaThuoc * 100 / tienvp).ToString("##,###.###");

                    T_muaThuoc = 0;
                    T_muaThuoc = ketqua.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocDongY).Where(p => p.PLoai == 1 && p.KieuDon == 1 && p.DongY == 1 && p.TinhTNhap == "C").Sum(p => p.ThanhTienN);
                    repbaocao.cel_duoclieu.Text = (T_muaThuoc / 1000).ToString("##,###");
                    if (tienvp > 0)
                        repbaocao.TL_DuocLieu.Text = (T_muaThuoc * 100 / tienvp).ToString("##,###.###");

                    T_muaThuoc = 0;
                    T_muaThuoc = ketqua.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocDongY).Where(p => p.PLoai == 1 && p.KieuDon == 1 && p.DongY == 1 && p.TinhTNhap != "C").Sum(p => p.ThanhTienN);
                    repbaocao.cel_vithuocYHCT.Text = (T_muaThuoc / 1000).ToString("##,###");
                    if (tienvp > 0)
                        repbaocao.Tl_muaViYHCT.Text = (T_muaThuoc * 100 / tienvp).ToString("##,###.###");
                    //2
                    T_muaThuoc = 0;
                    T_muaThuoc = ketqua.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocDongY).Where(p => p.PLoai == 1 && p.KieuDon == 1 && p.DongY == 1).Sum(p => p.ThanhTienN);
                    repbaocao.cel_TSmuaduoclieu.Text = (T_muaThuoc / 1000).ToString("##,###");
                    if (tienvp > 0)
                        repbaocao.TLTS_muaNB.Text = (T_muaThuoc * 100 / tienvp).ToString("##,###.###");

                    T_muaThuoc = 0;
                    T_muaThuoc = ketqua.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocDongY).Where(p => p.PLoai == 1 && p.KieuDon == 1 && p.DongY == 1 && p.NguonGoc == "N").Sum(p => p.ThanhTienN);
                    repbaocao.cel_thuocnam.Text = (T_muaThuoc / 1000).ToString("##,###");
                    if (tienvp > 0)
                        repbaocao.TL_Nam.Text = (T_muaThuoc * 100 / tienvp).ToString("##,###.###");

                    T_muaThuoc = 0;
                    T_muaThuoc = ketqua.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocDongY).Where(p => p.PLoai == 1 && p.KieuDon == 1 && p.DongY == 1 && p.NguonGoc == "B").Sum(p => p.ThanhTienN);
                    repbaocao.cel_thuocbac.Text = (T_muaThuoc / 1000).ToString("##,###");
                    if (tienvp > 0)
                        repbaocao.TL_Bac.Text = (T_muaThuoc * 100 / tienvp).ToString("##,###.###");
                    //3
                    T_muaThuoc = 0;
                    T_muaThuoc = ketqua.Where(p => p.PLoai == 2 && p.KieuDon != 2).Sum(p => p.ThanhTienX);
                    repbaocao.cel_tienthuocsd.Text = (T_muaThuoc / 1000).ToString("##,###");
                    if (tienvp > 0)
                        repbaocao.TLSDTS.Text = (T_muaThuoc * 100 / tienvp).ToString("##,###.###");

                    T_muaThuoc = 0;
                    T_muaThuoc = ketqua.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocDongY).Where(p => p.PLoai == 2 && p.DongY == 1 && p.KieuDon != 2).Sum(p => p.ThanhTienX);
                    repbaocao.cel_thuocsd_YHCT.Text = (T_muaThuoc / 1000).ToString("##,###");
                    if (tienvp > 0)
                        repbaocao.TLSD_YHCT.Text = (T_muaThuoc * 100 / tienvp).ToString("##,###.###");
                    T_muaThuoc = 0;
                    T_muaThuoc = ketqua.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocDongY).Where(p => p.PLoai == 2 && p.DongY == 1 && p.TinhTNhap == "C" && p.KieuDon != 2).Sum(p => p.ThanhTienX);
                    repbaocao.cel_thuocsd_thuocsx.Text = (T_muaThuoc / 1000).ToString("##,###");
                    if (tienvp > 0)
                        repbaocao.TLSD_CSKCB.Text = (T_muaThuoc * 100 / tienvp).ToString("##,###.###");
                    //T_muaThuoc = 0;
                    //T_muaThuoc = ketquad.Where(p => p.PLoai == 2 && p.KieuDon != 2 && p.IDDTBN==DTBHYT).Sum(p => p.ThanhTienX);
                    //repbaocao.cel_thuocsd_thuocBHYT.Text = (T_muaThuoc / 1000).ToString("##,###");
                    //if (tienvp > 0)
                    //    repbaocao.TLSD_BHYT.Text = (T_muaThuoc * 100 / tienvp).ToString("##,###.###");
                    //T_muaThuoc = 0;
                    //T_muaThuoc = ketquad.Where(p => p.PLoai == 2 && p.KieuDon != 2 && p.IDDTBN != DTBHYT).Sum(p => p.ThanhTienX);
                    //repbaocao.cel_thuocsd_thuockhac.Text = (T_muaThuoc / 1000).ToString("##,###");
                    //if (tienvp > 0)
                    //    repbaocao.TLSDThuocKhac.Text = (T_muaThuoc * 100 / tienvp).ToString("##,###.###");
                    //repbaocao.cel_thuocsd_thuocBHYT.Text = (ketqua.Where(p => p.PLoai == 2 && p.DongY == 1 && p.TinhTNhap == "C").Sum(p => p.ThanhTienX) / 1000).ToString();

                }
                else
                {

                    XtraMessageBox.Show("Chưa chọn kho", "Thông báo");
                    return;

                }


                repbaocao.CreateDocument();
                inn.prcIN.PrintingSystem = repbaocao.PrintingSystem;
                inn.ShowDialog();
            }
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            BaoCao.rep_BcCTKhoaDuoc baocao = new BaoCao.rep_BcCTKhoaDuoc();
        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chọn")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("tenkp") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("tenkp").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_Kphong.First().chon == true)
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = true;
                            }
                        }
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _Kphong.ToList();
                    }
                }
            }
        }


    }
}