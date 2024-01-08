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
using COMExcel = Microsoft.Office.Interop.Excel;
using QLBV.DungChung;
using OpenXmlPackaging;
using System.IO;

namespace QLBV.FormThamSo
{
    public partial class Frm_TongHopChiPhi_NT_NgT_VP_XaThang : DevExpress.XtraEditors.XtraForm
    {
        public Frm_TongHopChiPhi_NT_NgT_VP_XaThang()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private class KPhongc
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

        List<KPhong> _lkpall = new List<KPhong>();
        List<KPhongc> _Kphong = new List<KPhongc>();

        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void Frm_TongHopChiPhi_NT_NgT_VP_XaThang_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = System.DateTime.Now.Date;
            lupNgayden.DateTime = System.DateTime.Now.Date;

            _lkpall = data.KPhongs.Where(p => p.Status == 1).ToList();
            var kphong = (from kp in _lkpall
                          where (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham)
                          where (kp.Status == 1)
                          select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                KPhongc themmoi1 = new KPhongc();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = true;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhongc themmoi = new KPhongc();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }
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
        public class BaoCao578
        {
            public int soluotnoitru { get; set; }
            public int soluotngoaitru { get; set; }
            public int soluotvienphi { get; set; }
            public double songaydtngoaitru { get; set; }
            public double songaydtnoitru { get; set; }
            public double songaydtvienphi { get; set; }
            public double ttThuocnoitru { get; set; }
            public double ttThuocngoaitru { get; set; }
            public double ttThuocvienphi { get; set; }
            public double ttOXYnoitru { get; set; }
            public double ttOXYngoaitru { get; set; }
            public double ttOXYvienphi { get; set; }
            public double ttVatTunoitru { get; set; }
            public double ttVatTungoaitru { get; set; }
            public double ttVatTuvienphi { get; set; }
            public double ttMaunoitru { get; set; }
            public double ttMaungoaitru { get; set; }
            public double ttMauvienphi { get; set; }
            public double ttDichnoitru { get; set; }
            public double ttDichngoaitru { get; set; }
            public double ttDichvienphi { get; set; }

            //tổng 1
            public double ttTongngoaitru { get; set; }
            public double ttTongnoitru { get; set; }
            public double ttTongvienphi { get; set; }


            public double ttGiuongngoaitru { get; set; }
            public double ttGiuongnoitru { get; set; }
            public double ttGiuongvienphi { get; set; }
            public double ttCongKhamngoaitru { get; set; }
            public double ttCongKhamnoitru { get; set; }
            public double ttCongKhamvienphi { get; set; }
            public double KSKngoaitru { get; set; }
            public double KSKnoitru { get; set; }
            public double KSKvienphi { get; set; }
            public double ttVanChuyenngoaitru { get; set; }
            public double ttVanChuyennoitru { get; set; }
            public double ttVanChuyenvienphi { get; set; }
            public double ttPTTTngoaitru { get; set; }
            public double ttPTTTnoitru { get; set; }
            public double ttPTTTvienphi { get; set; }
            public double ttHoHapngoaitru { get; set; }
            public double ttHoHapnoitru { get; set; }
            public double ttHoHapvienphi { get; set; }
            public double ttDaDayngoaitru { get; set; }
            public double ttDaDaynoitru { get; set; }
            public double ttDaDayvienphi { get; set; }
            public double ttCTCngoaitru { get; set; }
            public double ttCTCnoitru { get; set; }
            public double ttCTCvienphi { get; set; }

            public double ttSieuAmngoaitru { get; set; }
            public double ttSieuAmnoitru { get; set; }
            public double ttSieuAmvienphi { get; set; }
            public double ttXQuangngoaitru { get; set; }
            public double ttXQuangnoitru { get; set; }
            public double ttXQuangvienphi { get; set; }
            public double ttChupCTngoaitru { get; set; }
            public double ttChupCTnoitru { get; set; }
            public double ttChupCTvienphi { get; set; }
            public double ttXNngoaitru { get; set; }
            public double ttXNnoitru { get; set; }
            public double ttXNvienphi { get; set; }
            public double ttKTSngoaitru { get; set; }
            public double ttKTSnoitru { get; set; }
            public double ttKTSvienphi { get; set; }
            public double ttLoangXuongngoaitru { get; set; }
            public double ttLoangXuongnoitru { get; set; }
            public double ttLoangXuongvienphi { get; set; }

            //tổng 2
            public double ttTong2ngoaitru { get; set; }
            public double ttTong2noitru { get; set; }
            public double ttTong2vienphi { get; set; }


            public double ttHBA1Cngoaitru { get; set; }
            public double ttHBA1Cnoitru { get; set; }
            public double ttHBA1Cvienphi { get; set; }
            public double ttNoiSoingoaitru { get; set; }
            public double ttNoiSoinoitru { get; set; }
            public double ttNoiSoivienphi { get; set; }
            public double ttLuuHuyetNaongoaitru { get; set; }
            public double ttLuuHuyetNaonoitru { get; set; }
            public double ttLuuHuyetNaovienphi { get; set; }

            public int ttTong1{ get; set; }
            public double ttTong2 { get; set; }
            public double ttTong3 { get; set; }
            public double ttTong4 { get; set; }
            public double ttTong5 { get; set; }
            public double ttTong6 { get; set; }
            public double ttTong7 { get; set; }
            public double ttTong8 { get; set; }
            public double ttTong9 { get; set; }
            public double ttTong10 { get; set; }
            public double ttTong11 { get; set; }
            public double ttTong12 { get; set; }
            public double ttTong13 { get; set; }
            public double ttTong14 { get; set; }
            public double ttTong145 { get; set; }
            public double ttTong15 { get; set; }
            public double ttTong16 { get; set; }
            public double ttTong17 { get; set; }
            public double ttTong18 { get; set; }
            public double ttTong19 { get; set; }
            public double ttTong20 { get; set; }
            public double ttTong21 { get; set; }
            public double ttTong22 { get; set; }
            public double ttTong23 { get; set; }
            public double ttTong24 { get; set; }
            public double ttTong25 { get; set; }

        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            List<BaoCao578> bc = new List<BaoCao578>();
            DateTime ngaytu = System.DateTime.Now.Date;
            DateTime ngayden = System.DateTime.Now.Date;
            ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            ngayden = DungChung.Ham.NgayDen(lupNgayden.DateTime);
            List<int> lkp = _Kphong.Where(p => p.chon == true).Select(p => p.makp).ToList();
            var tonghop = (from dv in data.DichVus
                           join vpct in data.VienPhicts on dv.MaDV equals vpct.MaDV
                           join vp in data.VienPhis.Where(p => p.NgayTT >= ngaytu && p.NgayTT <= ngayden) on vpct.idVPhi equals vp.idVPhi
                           join bn in data.BenhNhans on vp.MaBNhan equals bn.MaBNhan
                           join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                           select new { vpct.TrongBH, vpct.ThanhTien, bn.NoiTru, bn.DTuong, rv.SoNgaydt, bn.MaKP, dv.IdTieuNhom, dv.IDNhom, dv.ThuocDTruyen, dv.MinNhom, bn.MaBNhan}).ToList();

            BaoCao578 a = new BaoCao578();
            foreach (var _makp in lkp)
            {
                if (_makp != 0)
                {
                    var ngoaitru = tonghop.Where(p => p.NoiTru == 0 && p.TrongBH == 1 && p.MaKP == _makp).ToList();
                    var noitru = tonghop.Where(p => p.NoiTru == 1 && p.TrongBH == 1 && p.MaKP == _makp).ToList();
                    var vienphi = tonghop.Where(p => p.TrongBH == 0 && p.MaKP == _makp).ToList();

                    var kskngoaitru = (from bn in data.BenhNhans.Where(p => p.NNhap >= ngaytu && p.NNhap <= ngayden && p.DTuong == "KSK" && p.NoiTru == 0 && p.MaKP == _makp)
                                       join tu in data.TamUngs on bn.MaBNhan equals tu.MaBNhan
                                       select new { tu.SoTien}).ToList();
                    var ksknoitru = (from bn in data.BenhNhans.Where(p => p.NNhap >= ngaytu && p.NNhap <= ngayden && p.DTuong == "KSK" && p.NoiTru == 1 && p.MaKP == _makp)
                                       join tu in data.TamUngs on bn.MaBNhan equals tu.MaBNhan
                                       select new { tu.SoTien }).ToList();
                    var kskvienphi = (from bn in data.BenhNhans.Where(p => p.NNhap >= ngaytu && p.NNhap <= ngayden && p.DTuong == "KSK" && p.MaKP == _makp)
                                       join tu in data.TamUngs on bn.MaBNhan equals tu.MaBNhan
                                       select new { tu.SoTien }).ToList();
                    //Ngoại Trú
                    a.soluotngoaitru += ngoaitru.Select(p => p.MaBNhan).Distinct().Count();
                    //a.songaydtngoaitru += (double)ngoaitru.Sum(p => p.SoNgaydt);
                    a.ttOXYngoaitru += (double)ngoaitru.Where(p => (DungChung.Bien.MaBV == "30303" ? p.IdTieuNhom == 71 : p.IdTieuNhom == 31) && p.DTuong == "BHYT").Sum(p => p.ThanhTien);
                    a.ttVatTungoaitru += (double)ngoaitru.Where(p => p.IDNhom == 10 && p.DTuong == "BHYT").Sum(p => p.ThanhTien);
                    a.ttMaungoaitru += (double)ngoaitru.Where(p => p.IDNhom == 7 && p.DTuong == "BHYT").Sum(p => p.ThanhTien);
                    a.ttDichngoaitru += (double)ngoaitru.Where(p => p.ThuocDTruyen == 1).Sum(p => p.ThanhTien);
                    a.ttTongngoaitru += (double)ngoaitru.Where(p => (p.IDNhom == 0 || p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7 || p.IDNhom == 10 || p.IDNhom == 11 || p.IDNhom == 31 || p.IDNhom == 33) && p.DTuong == "BHYT").Sum(p => p.ThanhTien);

                    a.ttGiuongngoaitru += (double)ngoaitru.Where(p => p.IDNhom == 15 && p.DTuong == "BHYT").Sum(p => p.ThanhTien);
                    a.ttCongKhamngoaitru += (double)ngoaitru.Where(p => (DungChung.Bien.MaBV == "30303" ? p.IdTieuNhom == 54 : p.IdTieuNhom == 44) && p.DTuong == "BHYT").Sum(p => p.ThanhTien);

                    //a.KSKngoaitru += data.BenhNhans.Where(p => p.NNhap >= ngaytu && p.NNhap <= ngayden && p.DTuong == "KSK" && p.NoiTru == 0 && p.MaKP == _makp).Count();
                    a.KSKngoaitru += (double)kskngoaitru.Sum(p => p.SoTien);

                    a.ttVanChuyenngoaitru += (double)ngoaitru.Where(p => p.IDNhom == 12 && p.DTuong == "BHYT").Sum(p => p.ThanhTien);
                    a.ttPTTTngoaitru += (double)ngoaitru.Where(p => p.IdTieuNhom == 22 || p.IdTieuNhom == 23).Sum(p => p.ThanhTien);
                    a.ttHoHapngoaitru += (double)ngoaitru.Where(p => p.IDNhom == 3 && p.IdTieuNhom != 67).Sum(p => p.ThanhTien);

                    a.ttDaDayngoaitru += (double)ngoaitru.Where(p => p.IDNhom == 8 && p.MinNhom == 3).Sum(p => p.ThanhTien);

                    a.ttCTCngoaitru += (double)ngoaitru.Where(p => p.IDNhom == 8 && p.MinNhom == 1).Sum(p => p.ThanhTien);
                    
                    a.ttSieuAmngoaitru += (double)ngoaitru.Where(p => p.IDNhom == 2 && p.IdTieuNhom == 4).Sum(p => p.ThanhTien);
                    a.ttXQuangngoaitru += (double)ngoaitru.Where(p => p.IDNhom == 2 && p.MinNhom == 4).Sum(p => p.ThanhTien);
                    a.ttChupCTngoaitru += (double)ngoaitru.Where(p => p.IDNhom == 2 && p.IdTieuNhom == 1005).Sum(p => p.ThanhTien);
                    a.ttXNngoaitru += (double)ngoaitru.Where(p => p.IDNhom == 1 || p.MinNhom == 5).Sum(p => p.ThanhTien);
                    a.ttKTSngoaitru += (double)ngoaitru.Where(p => p.IDNhom == 2 && p.MinNhom == 6).Sum(p => p.ThanhTien);
                    a.ttLoangXuongngoaitru += (double)ngoaitru.Where(p => p.IDNhom == 2 && (DungChung.Bien.MaBV == "30303" ? p.MinNhom == 7 : p.IdTieuNhom == 68)).Sum(p => p.ThanhTien);

                    a.ttHBA1Cngoaitru += (double)ngoaitru.Where(p => p.IDNhom == 1 && p.MinNhom == 2).Sum(p => p.ThanhTien);
                    a.ttNoiSoingoaitru += (double)ngoaitru.Where(p => p.IDNhom == 8 && (DungChung.Bien.MaBV == "30303" ? p.IdTieuNhom == 101 : p.IdTieuNhom == 42)).Sum(p => p.ThanhTien);
                    a.ttLuuHuyetNaongoaitru += (double)ngoaitru.Where(p => p.IDNhom == 3 && p.IdTieuNhom == 67).Sum(p => p.ThanhTien);
                   
                    //Nội Trú
                    var noitru1 = tonghop.Where(p => p.NoiTru == 1 && p.TrongBH == 1 && p.MaKP == _makp).ToList();
                    var noitru2 = (from th in tonghop.Where(p => p.NoiTru == 1 && p.TrongBH == 1 && p.MaKP == _makp)
                                    select new { th.MaBNhan, th.SoNgaydt }).ToList();
                    a.soluotnoitru += noitru1.Select(p => p.MaBNhan).Distinct().Count();
                    a.songaydtnoitru += (double)((noitru2.Distinct()).Sum(p => p.SoNgaydt));
                    a.ttOXYnoitru += (double)noitru.Where(p => (DungChung.Bien.MaBV == "30303" ? p.IdTieuNhom == 71 : p.IdTieuNhom == 31) && p.DTuong == "BHYT").Sum(p => p.ThanhTien);
                    a.ttVatTunoitru += (double)noitru.Where(p => p.IDNhom == 10 && p.DTuong == "BHYT").Sum(p => p.ThanhTien);
                    a.ttMaunoitru += (double)noitru.Where(p => p.IDNhom == 7 && p.DTuong == "BHYT").Sum(p => p.ThanhTien);
                    a.ttDichnoitru += (double)noitru.Where(p => p.ThuocDTruyen == 1).Sum(p => p.ThanhTien);
                    a.ttTongnoitru += (double)noitru.Where(p => (p.IDNhom == 0 || p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7 || p.IDNhom == 10 || p.IDNhom == 11 || p.IDNhom == 31 || p.IDNhom == 33) && p.DTuong == "BHYT").Sum(p => p.ThanhTien);

                    a.ttGiuongnoitru += (double)noitru.Where(p => p.IDNhom == 15 && p.DTuong == "BHYT").Sum(p => p.ThanhTien);
                    a.ttCongKhamnoitru += (double)noitru.Where(p => (DungChung.Bien.MaBV == "30303" ? p.IdTieuNhom == 54 : p.IdTieuNhom == 44) && p.DTuong == "BHYT").Sum(p => p.ThanhTien);

                    //a.KSKnoitru += data.BenhNhans.Where(p => p.NNhap >= ngaytu && p.NNhap <= ngayden && p.DTuong == "KSK" && p.NoiTru == 1 && p.MaKP == _makp).Count();
                    a.KSKnoitru += (double)ksknoitru.Sum(p => p.SoTien);

                    a.ttVanChuyennoitru += (double)noitru.Where(p => p.IDNhom == 12 && p.DTuong == "BHYT").Sum(p => p.ThanhTien);
                    a.ttPTTTnoitru += (double)noitru.Where(p => p.IdTieuNhom == 22 || p.IdTieuNhom == 23).Sum(p => p.ThanhTien);
                    a.ttHoHapnoitru += (double)noitru.Where(p => p.IDNhom == 3 && p.IdTieuNhom != 67).Sum(p => p.ThanhTien);

                    a.ttDaDaynoitru += (double)noitru.Where(p => p.IDNhom == 8 && p.MinNhom == 3).Sum(p => p.ThanhTien);

                    a.ttCTCnoitru += (double)noitru.Where(p => p.IDNhom == 8 && p.MinNhom == 1).Sum(p => p.ThanhTien);

                    a.ttSieuAmnoitru += (double)noitru.Where(p => p.IDNhom == 2 && p.IdTieuNhom == 4).Sum(p => p.ThanhTien);
                    a.ttXQuangnoitru += (double)noitru.Where(p => p.IDNhom == 2 && p.MinNhom == 4).Sum(p => p.ThanhTien);
                    a.ttChupCTnoitru += (double)noitru.Where(p => p.IDNhom == 2 && p.IdTieuNhom == 1005).Sum(p => p.ThanhTien);
                    a.ttXNnoitru += (double)noitru.Where(p => p.IDNhom == 1 || p.MinNhom == 5).Sum(p => p.ThanhTien);
                    a.ttKTSnoitru += (double)noitru.Where(p => p.IDNhom == 2 && p.MinNhom == 6).Sum(p => p.ThanhTien);
                    a.ttLoangXuongnoitru += (double)noitru.Where(p => p.IDNhom == 2 && (DungChung.Bien.MaBV == "30303" ? p.MinNhom == 7 : p.IdTieuNhom == 68)).Sum(p => p.ThanhTien);

                    a.ttHBA1Cnoitru += (double)noitru.Where(p => p.IDNhom == 1 && p.MinNhom == 2).Sum(p => p.ThanhTien);
                    a.ttNoiSoinoitru += (double)noitru.Where(p => p.IDNhom == 8 && (DungChung.Bien.MaBV == "30303" ? p.IdTieuNhom == 101 : p.IdTieuNhom == 42)).Sum(p => p.ThanhTien);
                    a.ttLuuHuyetNaonoitru += (double)noitru.Where(p => p.IDNhom == 3 && p.IdTieuNhom == 67).Sum(p => p.ThanhTien);

                    //Viện Phí
                    var vienphi1 = (from th in tonghop.Where(p => p.DTuong == "Dịch vụ" && p.MaKP == _makp && p.NoiTru == 1 && p.TrongBH == 0)
                                   select new { th.MaBNhan, th.SoNgaydt}).ToList();
                    a.soluotvienphi += vienphi1.Select(p => p.MaBNhan).Distinct().Count();
                    a.songaydtvienphi += (double)((vienphi1.Distinct()).Sum(p => p.SoNgaydt));
                    a.ttOXYvienphi += (double)vienphi.Where(p => (DungChung.Bien.MaBV == "30303" ? p.IdTieuNhom == 71 : p.IdTieuNhom == 31) && p.DTuong == "BHYT").Sum(p => p.ThanhTien);
                    a.ttVatTuvienphi += (double)vienphi.Where(p => p.IDNhom == 10 && (p.DTuong == "BHYT" || p.DTuong == "Dịch vụ")).Sum(p => p.ThanhTien);
                    a.ttMauvienphi += (double)vienphi.Where(p => p.IDNhom == 7 && p.DTuong == "BHYT").Sum(p => p.ThanhTien);
                    a.ttDichvienphi += (double)vienphi.Where(p => p.ThuocDTruyen == 1).Sum(p => p.ThanhTien);
                    a.ttTongvienphi += (double)vienphi.Where(p => (p.IDNhom == 0 || p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7 || p.IDNhom == 10 || p.IDNhom == 11 || p.IDNhom == 31 || p.IDNhom == 33) && (p.DTuong == "BHYT" || p.DTuong == "Dịch vụ")).Sum(p => p.ThanhTien);

                    a.ttGiuongvienphi += (double)vienphi.Where(p => (p.IDNhom == 15) && (p.DTuong == "BHYT" || p.DTuong == "Dịch vụ")).Sum(p => p.ThanhTien);
                    a.ttCongKhamvienphi += (double)vienphi.Where(p => (DungChung.Bien.MaBV == "30303" ? p.IdTieuNhom == 54 : p.IdTieuNhom == 44) && (p.DTuong == "BHYT" || p.DTuong == "Dịch vụ")).Sum(p => p.ThanhTien);

                    //a.KSKvienphi += data.BenhNhans.Where(p => p.NNhap >= ngaytu && p.NNhap <= ngayden && p.DTuong == "KSK" && p.MaKP == _makp).Count();
                    //a.KSKvienphi += (double)kskvienphi.Sum(p => p.SoTien);
                    var vienphi11 = tonghop.Where(p => p.MaKP == _makp).ToList();
                    a.ttVanChuyenvienphi += (double)vienphi11.Where(p => p.IDNhom == 12 && p.DTuong == "Dịch vụ" && p.TrongBH==0).Sum(p => p.ThanhTien);
                    a.ttPTTTvienphi += (double)vienphi.Where(p => p.IdTieuNhom == 22 || p.IdTieuNhom == 23).Sum(p => p.ThanhTien);
                    a.ttHoHapvienphi += (double)vienphi.Where(p => p.IDNhom == 3 && p.IdTieuNhom != 67).Sum(p => p.ThanhTien);
                    a.ttDaDayvienphi += (double)vienphi.Where(p => p.IDNhom == 8 && p.MinNhom == 3).Sum(p => p.ThanhTien);
                    a.ttCTCvienphi += (double)vienphi.Where(p => p.IDNhom == 8 && p.MinNhom == 1).Sum(p => p.ThanhTien);

                    a.ttSieuAmvienphi += (double)vienphi.Where(p => p.IDNhom == 2 && p.IdTieuNhom == 4).Sum(p => p.ThanhTien);
                    a.ttXQuangvienphi += (double)vienphi.Where(p => p.IDNhom == 2 && p.MinNhom == 4).Sum(p => p.ThanhTien);
                    a.ttChupCTvienphi += (double)vienphi.Where(p => p.IDNhom == 2 && p.IdTieuNhom == 1005).Sum(p => p.ThanhTien);
                    a.ttXNvienphi += (double)vienphi.Where(p => p.IDNhom == 1 || p.MinNhom == 5).Sum(p => p.ThanhTien);
                    a.ttKTSvienphi += (double)vienphi.Where(p => p.IDNhom == 2 && p.MinNhom == 6).Sum(p => p.ThanhTien);
                    a.ttLoangXuongvienphi += (double)vienphi.Where(p => (DungChung.Bien.MaBV == "30303" ? p.MinNhom == 7 : p.IdTieuNhom == 68)).Sum(p => p.ThanhTien);

                    a.ttHBA1Cvienphi += (double)vienphi.Where(p => p.IDNhom == 1 && p.MinNhom == 2).Sum(p => p.ThanhTien);
                    a.ttNoiSoivienphi += (double)vienphi.Where(p => p.IDNhom == 8 && (DungChung.Bien.MaBV == "30303" ? p.IdTieuNhom == 101 : p.IdTieuNhom == 42)).Sum(p => p.ThanhTien);
                    a.ttLuuHuyetNaovienphi += (double)vienphi.Where(p => p.IDNhom == 3 && p.IdTieuNhom == 67).Sum(p => p.ThanhTien);
                }
            }
            a.ttXNngoaitru -= a.ttHBA1Cngoaitru;
            a.ttXNnoitru -= a.ttHBA1Cnoitru;
            a.ttXNvienphi -= a.ttHBA1Cvienphi;
            a.ttTong1 = a.soluotngoaitru + a.soluotnoitru + a.soluotvienphi;
            a.ttTong2 = a.songaydtngoaitru + a.songaydtnoitru + a.songaydtvienphi;
            a.ttTong4 = a.ttOXYngoaitru + a.ttOXYnoitru + a.ttOXYvienphi;
            a.ttTong5 = a.ttVatTungoaitru + a.ttVatTunoitru + a.ttVatTuvienphi;
            a.ttTong6 = a.ttMaungoaitru + a.ttMaunoitru + a.ttMauvienphi;
            a.ttTong7 = a.ttDichngoaitru + a.ttDichnoitru + a.ttDichvienphi;
            a.ttTong8 = a.ttTongngoaitru + a.ttTongnoitru + a.ttTongvienphi;

            a.ttThuocngoaitru = (a.ttTongngoaitru - a.ttDichngoaitru - a.ttMaungoaitru - a.ttVatTungoaitru - a.ttOXYngoaitru);
            a.ttThuocnoitru = (a.ttTongnoitru - a.ttDichnoitru - a.ttMaunoitru - a.ttVatTunoitru - a.ttOXYnoitru);
            a.ttThuocvienphi = (a.ttTongvienphi - a.ttDichvienphi - a.ttMauvienphi - a.ttVatTuvienphi - a.ttOXYvienphi);
            a.ttTong3 = a.ttThuocngoaitru + a.ttThuocnoitru + a.ttThuocvienphi;

            a.ttTong9 = a.ttGiuongngoaitru + a.ttGiuongnoitru + a.ttGiuongvienphi;
            a.ttTong10 = a.ttCongKhamngoaitru + a.ttCongKhamnoitru + a.ttCongKhamvienphi;
            a.ttTong11 = a.KSKngoaitru + a.KSKnoitru + a.KSKvienphi;
            a.ttTong12 = a.ttVanChuyenngoaitru + a.ttVanChuyennoitru + a.ttVanChuyenvienphi;
            a.ttTong13 = a.ttPTTTngoaitru + a.ttPTTTnoitru + a.ttPTTTvienphi;
            a.ttTong14 = a.ttHoHapngoaitru + a.ttHoHapnoitru + a.ttHoHapvienphi;

            a.ttTong145 = a.ttDaDayngoaitru + a.ttDaDaynoitru + a.ttDaDayvienphi;

            a.ttTong15 = a.ttCTCngoaitru + a.ttCTCnoitru + a.ttCTCvienphi;

            a.ttTong16 = a.ttSieuAmngoaitru + a.ttSieuAmnoitru + a.ttSieuAmvienphi;
            a.ttTong17 = a.ttXQuangngoaitru + a.ttXQuangnoitru + a.ttXQuangvienphi;
            a.ttTong18 = a.ttChupCTngoaitru + a.ttChupCTnoitru + a.ttChupCTvienphi;
            a.ttTong19 = a.ttXNngoaitru + a.ttXNnoitru + a.ttXNvienphi;
            a.ttTong20 = a.ttKTSngoaitru + a.ttKTSnoitru + a.ttKTSvienphi;
            a.ttTong21 = a.ttLoangXuongngoaitru + a.ttLoangXuongnoitru + a.ttLoangXuongvienphi;

            a.ttTong23 = a.ttHBA1Cngoaitru + a.ttHBA1Cnoitru + a.ttHBA1Cvienphi;
            a.ttTong24 = a.ttNoiSoingoaitru + a.ttNoiSoinoitru + a.ttNoiSoivienphi;
            a.ttTong25 = a.ttLuuHuyetNaongoaitru + a.ttLuuHuyetNaonoitru + a.ttLuuHuyetNaovienphi;
            
            bc.Add(a);

            #region In mới
            Dictionary<string, object> _dic = new Dictionary<string, object>();
            _dic.Add("TenCQCQ", DungChung.Bien.TenCQCQ);
            _dic.Add("TenCQ", DungChung.Bien.TenCQ);
            string tieude = "Tổng hợp khám chữa bệnh nội trú, ngoại trú, viện phí, xã, phường ".ToUpper() + " NĂM " + lupNgaytu.DateTime.Year + (lupNgaytu.DateTime.Year == lupNgayden.DateTime.Year ? "" : " - " +lupNgayden.DateTime.Year);
            _dic.Add("TieuDe", tieude);
            string ngaythang = "Từ ngày " + lupNgaytu.DateTime.ToShortDateString() + " đến ngày " + lupNgayden.DateTime.ToShortDateString();
            _dic.Add("Ngaythang", ngaythang);
            string ngayKy = DungChung.Ham.GetTenTinh(DungChung.Bien.MaBV) + ", " + DungChung.Ham.NgaySangChu(DateTime.Now, 1);
            _dic.Add("NgayKy", ngayKy);
            DungChung.Ham.Print(DungChung.PrintConfig.Rep_TongHopChiPhi_NT_NgT_VP_XaThang, bc, _dic, false);
            #endregion
        }
    }
}