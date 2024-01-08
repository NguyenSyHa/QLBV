using System;using QLBV_Database;
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
    public partial class Frm_DsTonGoiThuoc : DevExpress.XtraEditors.XtraForm
    {
        public Frm_DsTonGoiThuoc()
        {
            InitializeComponent();
        }
        int x = 1;
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private bool KTtaoBc()
        {
            if (lupTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupTuNgay.Focus();
                return false;
            }
            if (lupDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
                return false;
            }

            return true;
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
        private class Thuoc
        {
            private string TenDV;
            public int sttDichVu { set; get; }
            public int MaBNhan { set; get; }

            public string TenBN { set; get; }
            private int MaDV;
            private string DVT;
            private double DonGia;
            private double DuocSL;
            private double DuocTT;
            private double VPDKSL;
            private double VPDKTT;
            private double VPTKSL;
            private double VPTKTT;
            private double? VPTKBHYTSL;
            private double? VPTKBHYTTT;
            private double? VPTKDVSL;
            private double? VPTKDVTT;
            private double? VPTKKhongTTSL;
            private double? VPTKKhongTTTT;
            private double VPCKSL;
            private double VPCKTT;
            private int trongBH;
            private double? VPTKBATT, VPTKBATT_1;

            public double? VPTKBATT_11
            {
                get { return VPTKBATT_1; }
                set { VPTKBATT_1 = value; }
            }

            public double? VPTKBATT1
            {
                get { return VPTKBATT; }
                set { VPTKBATT = value; }
            }
            private double? VPTKBASL, VPTKBASL_1;

            public double? VPTKBASL_11
            {
                get { return VPTKBASL_1; }
                set { VPTKBASL_1 = value; }
            }

            public double? VPTKBASL1
            {
                get { return VPTKBASL; }
                set { VPTKBASL = value; }
            }

            public int TrongBH
            {
                get { return trongBH; }
                set { trongBH = value; }
            }

            public double? VPTKKhongTTTT1
            {
                get { return VPTKKhongTTTT; }
                set { VPTKKhongTTTT = value; }
            }
            public double? VPTKKhongTTSL1
            {
                get { return VPTKKhongTTSL; }
                set { VPTKKhongTTSL = value; }
            }
            public double? VPTKDVTT1
            {
                get { return VPTKDVTT; }
                set { VPTKDVTT = value; }
            }
            public double? VPTKDVSL1
            {
                get { return VPTKDVSL; }
                set { VPTKDVSL = value; }
            }
            public double? VPTKBHYTTT1
            {
                get { return VPTKBHYTTT; }
                set { VPTKBHYTTT = value; }
            }
            public double? VPTKBHYTSL1
            {
                get { return VPTKBHYTSL; }
                set { VPTKBHYTSL = value; }
            }
            private int makp;
            public int MaKP
            {
                set { makp = value; }
                get { return makp; }
            }
            private int stt;
            public int STT
            {
                get { return stt; }
                set { stt = value; }
            }
            public string TenDV1
            {
                get { return TenDV; }
                set { TenDV = value; }
            }
            public int MaDV1
            {
                get { return MaDV; }
                set { MaDV = value; }
            }
            public string DVT1
            {
                get { return DVT; }
                set { DVT = value; }
            }

            public double DonGia1
            {
                get { return DonGia; }
                set { DonGia = value; }
            }

            public double DuocSL1
            {
                get { return DuocSL; }
                set { DuocSL = value; }
            }

            public double DuocTT1
            {
                get { return DuocTT; }
                set { DuocTT = value; }
            }

            public double VPDKSL1
            {
                get { return VPDKSL; }
                set { VPDKSL = value; }
            }

            public double VPDKTT1
            {
                get { return VPDKTT; }
                set { VPDKTT = value; }
            }

            public double VPTKSL1
            {
                get { return VPTKSL; }
                set { VPTKSL = value; }
            }

            public double VPTKTT1
            {
                get { return VPTKTT; }
                set { VPTKTT = value; }
            }

            public double VPCKSL1
            {
                get { return VPCKSL; }
                set { VPCKSL = value; }
            }

            public double VPCKTT1
            {
                get { return VPCKTT; }
                set { VPCKTT = value; }
            }
        }

        private class KhoDuoc
        {
            private string tenKP;

            public string TenKP
            {
                get { return tenKP; }
                set { tenKP = value; }
            }
            private int maKP;

            public int MaKP
            {
                get { return maKP; }
                set { maKP = value; }
            }
            private bool chon;
            private string pLoai;

            public string PLoai
            {
                get { return pLoai; }
                set { pLoai = value; }
            }
            public bool Chon
            {
                get { return chon; }
                set { chon = value; }
            }
        }

        List<KhoDuoc> _lKhoDuoc = new List<KhoDuoc>();
        List<KPhong> _Kphong = new List<KPhong>();
        List<KPhong> _Kphong1 = new List<KPhong>();
        List<Thuoc> _Thuoc1 = new List<Thuoc>();
        private void Frm_DsTonGoiThuoc_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            #region update MaKXuat trong DThuocct
            btnUpdate.Visible = false;
            //var dtct1 = (from dtct in data.DThuoccts
            //             join dv in data.DichVus.Where(p => p.PLoai == 1) on dtct.MaDV equals dv.MaDV
            //             select dtct).Where(p => p.MaKXuat == null).ToList();
            //var m = (from n in data.DThuocs
            //         select new { n.IDDon, n.MaKXuat }).Where(p => p.MaKXuat != null && p.MaKXuat != 0).ToList();
            //var kq = (from a in m
            //          join d in dtct1 on a.IDDon equals d.IDDon
            //          select d).ToList();

            //if (kq.Count() > 0)
            //{
            //    btnUpdate.Visible = true;
            //}
            //else
            //{
            //    btnUpdate.Visible = false;
            //}
            #endregion

            radNoiTru.SelectedIndex = 1;
            rdTrongBH.SelectedIndex = 3;
            _Kphong.Clear();
            _Kphong1.Clear();
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;

            List<DTBN> dtuong = data.DTBNs.ToList();
            dtuong.Insert(0, new DTBN { IDDTBN = 100, DTBN1 = "Tất cả" });
            lupDoituong.Properties.DataSource = dtuong;
            lupDoituong.EditValue = lupDoituong.Properties.GetKeyValueByDisplayText("Tất cả");

            var kphong = (from kp in data.KPhongs
                          where (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham)
                          select new { kp.TenKP, kp.MaKP }).ToList();
            var khoduoc = (from kp in data.KPhongs
                           where (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc)
                           select new { kp.TenKP, kp.MaKP, kp.PLoai }).ToList();
            if (DungChung.Bien.MaBV == "30007")
                khoduoc = (from kp in data.KPhongs
                           where (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc || kp.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc)
                           select new { kp.TenKP, kp.MaKP, kp.PLoai }).ToList();
            if (kphong.Count > 0)
            {
                KPhong themmoi1 = new KPhong();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = true;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhong themmoi = new KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }
            if (khoduoc.Count > 0)
            {
                KhoDuoc themmoi1 = new KhoDuoc();
                themmoi1.TenKP = "Chọn tất cả";
                themmoi1.MaKP = 0;
                themmoi1.Chon = true;
                themmoi1.PLoai = "";
                _lKhoDuoc.Add(themmoi1);
                foreach (var a in khoduoc)
                {
                    KhoDuoc themmoi = new KhoDuoc();
                    themmoi.TenKP = a.TenKP;
                    themmoi.MaKP = a.MaKP;
                    themmoi.Chon = true;
                    themmoi1.PLoai = a.PLoai;
                    _lKhoDuoc.Add(themmoi);
                }
                grcKhoDuoc.DataSource = _lKhoDuoc.ToList();
            }
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            DateTime tungay1 = System.DateTime.Now.Date;
            DateTime denngay1 = System.DateTime.Now.Date;
            _Thuoc1 = new List<Thuoc>();

            int dtuong = 100;

            List<KhoDuoc> khoChon = new List<KhoDuoc>();
            khoChon = _lKhoDuoc.Where(p => p.MaKP > 0).Where(p => p.Chon == true).ToList();

            if (lupDoituong.EditValue != null)
                dtuong = Convert.ToInt32(lupDoituong.EditValue);
            if (KTtaoBc())
            {

                _Kphong1.Clear();
                _Kphong1 = _Kphong.Where(p => p.makp > 0).Where(p => p.chon == true).ToList();
                //foreach (var a in _Kphong)
                //{
                //    if (a.chon == true)
                //    {
                //        KPhong themmoi = new KPhong();
                //        themmoi.makp = a.makp;
                //        themmoi.tenkp = a.tenkp;
                //        _Kphong1.Add(themmoi);
                //    }
                //}

                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                tungay1 = DungChung.Ham.NgayTu(lupTuNgay.DateTime).AddMonths(-3);
                denngay1 = DungChung.Ham.NgayDen(lupDenNgay.DateTime).AddMonths(1);
                bool htbn = false; // hiển thị bệnh nhân  
                if (ckTonDK.Checked || ckTTtrongky.Checked || ckTonCK.Checked)
                    htbn = true;
                bool Tyle = true;
                if (ckTyLe.Checked)// Thành tiền không tính theo tỷ lệ thanh toán
                    Tyle = false;
                var qdv = (from dv in data.DichVus join tn in data.TieuNhomDVs.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6) on dv.IdTieuNhom equals tn.IdTieuNhom select new { dv.MaDV, dv.TenDV }).ToList();
                if (radMauBC.SelectedIndex == 1)
                    qdv = (from dv in data.DichVus
                           join tn in data.TieuNhomDVs.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 10 || p.IDNhom == 11) on dv.IdTieuNhom equals tn.IdTieuNhom
                           select new { dv.MaDV, dv.TenDV }).ToList();
                var qdt1 = (from dt in data.DThuocs.Where(p => p.NgayKe >= tungay1 && p.NgayKe <= denngay1)
                            join dtct in data.DThuoccts.Where(p => rdTrongBH.SelectedIndex == 3 || p.TrongBH == rdTrongBH.SelectedIndex) on dt.IDDon equals dtct.IDDon
                            select new { dt.IDDon, dtct.Status, dt.MaBNhan, dt.MaKP, dt.NgayKe, dtct.MaDV, dtct.DonVi, dtct.SoLuong,ThanhTien = Tyle== true ? dtct.ThanhTien: (dtct.DonGia * dtct.SoLuong), dtct.TrongBH, dtct.DonGia, dtct.MaKXuat })
                    /*.Where(p => radNgay.SelectedIndex == 0 ? (p.NgayKe >= tungay && p.NgayKe <= denngay) : true).Where(p => p.Status == 1)*/.ToList();
                var qbn1 = (from a in data.BenhNhans.Where(p => p.NNhap >= tungay1 && p.NNhap <= denngay).Where(p => radNoiTru.SelectedIndex == 2 || p.NoiTru == radNoiTru.SelectedIndex).Where(p => dtuong == 100 || p.IDDTBN == dtuong) select new { a.MaBNhan, a.TenBNhan, a.NoiTru, a.IDDTBN, a.DTNT }).ToList();
                var qdt = (from dt in qdt1
                           join bn in qbn1 on dt.MaBNhan equals bn.MaBNhan
                           select new { dt.IDDon, dt.MaBNhan, dt.Status, dt.MaKP, dt.NgayKe, dt.MaDV, dt.DonVi, dt.SoLuong, dt.ThanhTien, dt.TrongBH, dt.DonGia, dt.MaKXuat, bn.NoiTru, bn.IDDTBN, bn.DTNT, bn.TenBNhan }).ToList();
                var q0 = (from dt in qdt
                          join dv in qdv on dt.MaDV equals dv.MaDV
                          select new { dt.IDDon, dt.MaBNhan, dt.TenBNhan, dt.Status, dt.MaKP, dt.NgayKe, dt.MaDV, dt.DonVi, dt.SoLuong, dt.ThanhTien, dt.TrongBH, dt.DonGia, dt.MaKXuat, dv.TenDV, dt.NoiTru, dt.IDDTBN, dt.DTNT }).ToList();
                var q1 = (from a in q0 
                          join kp in _Kphong1 on a.MaKP equals kp.makp select a).ToList();

                var qvp = data.VienPhis.ToList();
                var q3 = (from a in q1
                          join vp in qvp on a.MaBNhan equals vp.MaBNhan into kq
                          from kq1 in kq.DefaultIfEmpty()
                          select new
                          {
                              a.IDDon,
                              a.MaBNhan,
                              a.TenBNhan,
                              a.NgayKe,
                              a.MaDV,
                              a.DonVi,
                              a.DonGia,
                              a.SoLuong,
                              ThanhTien = a.ThanhTien,
                           
                              a.TenDV,
                              a.NoiTru,
                              a.IDDTBN,
                              a.DTNT,
                              a.TrongBH,
                              a.MaKXuat,
                              a.Status,
                              NgayTT = kq1 != null ? kq1.NgayTT : null,
                              vp = kq1 == null ? 0 : 1
                          }).ToList();

                DateTime ngayMin = tungay.AddMonths(-3);

                _Thuoc1 = (from a in q3
                           join kho in khoChon on a.MaKXuat equals kho.MaKP
                           group new { a, kho } by new { a.DonGia, a.MaDV, a.TenDV, a.DonVi, a.MaBNhan, a.TenBNhan } into kq
                           select new Thuoc
                           {
                               MaBNhan = kq.Key.MaBNhan ?? 0,
                               TenBN = kq.Key.TenBNhan,
                               MaDV1 = kq.Key.MaDV ?? 0,
                               TenDV1 = kq.Key.TenDV,
                               DonGia1 = kq.Key.DonGia,
                               DVT1 = kq.Key.DonVi,
                               //DuocSL1 = (radNgay.SelectedIndex == 0) ? kq.Where(p => p.NgayKe >= tungay && p.NgayKe <= denngay && p.Status == 1).Sum(p => p.SoLuong) : 0,
                               //DuocTT1 = (radNgay.SelectedIndex == 0) ? kq.Where(p => p.NgayKe >= tungay && p.NgayKe <= denngay && p.Status == 1).Sum(p => p.ThanhTien) : 0,
                               TrongBH = Convert.ToInt32(rdTrongBH.SelectedIndex),
                               VPDKSL1 = kq.Where(p => p.a.NgayKe < tungay && p.a.NgayKe >= ngayMin).Where(p => p.a.vp == 0 || (p.a.vp == 1 && p.a.NgayTT >= tungay)).Sum(p => p.a.SoLuong), // tồn số lượng kỳ trước chưa thanh toán
                               VPDKTT1 = kq.Where(p => p.a.NgayKe < tungay && p.a.NgayKe >= ngayMin).Where(p => p.a.vp == 0 || (p.a.vp == 1 && p.a.NgayTT >= tungay)).Sum(p => p.a.ThanhTien),

                               VPTKSL1 = kq.Where(p => p.a.NgayKe <= denngay).Where(p => p.a.vp == 1 && p.a.NgayTT <= denngay && p.a.NgayTT >= tungay).Sum(p => p.a.SoLuong),//  đã thanh toán trong kỳ (tính cả tồn kỳ trước)
                               VPTKTT1 = kq.Where(p => p.a.NgayKe <= denngay).Where(p => p.a.vp == 1 && p.a.NgayTT <= denngay && p.a.NgayTT >= tungay).Sum(p => p.a.ThanhTien),

                               //Thanh toán BHYT
                               VPTKBHYTSL1 = kq.Where(p => p.a.NgayKe <= denngay).Where(p => p.a.vp == 1 && p.a.NgayTT <= denngay && p.a.NgayTT >= tungay).Where(p => p.a.TrongBH == 1).Sum(p => p.a.SoLuong),
                               VPTKBHYTTT1 = kq.Where(p => p.a.NgayKe <= denngay).Where(p => p.a.vp == 1 && p.a.NgayTT <= denngay && p.a.NgayTT >= tungay).Where(p => p.a.TrongBH == 1).Sum(p => p.a.ThanhTien),

                               //Không thanh toán
                               VPTKKhongTTSL1 = kq.Where(p => p.a.NgayKe <= denngay).Where(p => p.a.vp == 1 && p.a.NgayTT <= denngay && p.a.NgayTT >= tungay).Where(p => p.a.TrongBH == 2).Sum(p => p.a.SoLuong),
                               VPTKKhongTTTT1 = kq.Where(p => p.a.NgayKe <= denngay).Where(p => p.a.vp == 1 && p.a.NgayTT <= denngay && p.a.NgayTT >= tungay).Where(p => p.a.TrongBH == 2).Sum(p => p.a.ThanhTien),

                               //Dịch vụ
                               VPTKDVSL1 = kq.Where(p => p.a.NgayKe <= denngay).Where(p => p.a.vp == 1 && p.a.NgayTT <= denngay && p.a.NgayTT >= tungay).Where(p => p.a.TrongBH == 0).Sum(p => p.a.SoLuong),
                               VPTKDVTT1 = kq.Where(p => p.a.NgayKe <= denngay).Where(p => p.a.vp == 1 && p.a.NgayTT <= denngay && p.a.NgayTT >= tungay).Where(p => p.a.TrongBH == 0).Sum(p => p.a.ThanhTien),

                               VPCKSL1 = kq.Where(p => p.a.NgayKe <= denngay && p.a.NgayKe >= ngayMin).Where(p => p.a.vp == 0 || (p.a.vp == 1 && p.a.NgayTT > denngay)).Sum(p => p.a.SoLuong), // tồn Bệnh án cuối kỳ
                               VPCKTT1 = kq.Where(p => p.a.NgayKe <= denngay && p.a.NgayKe >= ngayMin).Where(p => p.a.vp == 0 || (p.a.vp == 1 && p.a.NgayTT > denngay)).Sum(p => p.a.ThanhTien),

                               VPTKBASL1 = kq.Where(p => p.a.NgayKe >= tungay && p.a.NgayKe <= denngay).Sum(p => p.a.SoLuong),
                               VPTKBATT1 = kq.Where(p => p.a.NgayKe >= tungay && p.a.NgayKe <= denngay).Sum(p => p.a.ThanhTien),

                           }).OrderBy(p => p.sttDichVu).ThenBy(p => p.TenBN).ToList();


                if (radNgay.SelectedIndex == 0)//theo ngày kê
                {
                    var qduoc = (from b in q1.Where(p => p.Status == 1).Where(p => p.NgayKe >= tungay && p.NgayKe <= denngay)
                                 join k in khoChon on b.MaKXuat equals k.MaKP
                                 group b by new { b.MaDV, b.TenDV, b.DonGia, b.DonVi } into kq
                                 select new
                                 {
                                     kq.Key.MaDV,
                                     kq.Key.TenDV,
                                     kq.Key.DonGia,
                                     kq.Key.DonVi,
                                     SLuong = kq.Sum(p => p.SoLuong),
                                     ThanhTien = kq.Sum(p => p.ThanhTien),
                                 }).ToList();
                    if (DungChung.Bien.MaBV == "30007")
                    {
                        qduoc = (from b in q1.Where(p => p.NgayKe >= tungay && p.NgayKe <= denngay)
                                 join k in khoChon on b.MaKXuat equals k.MaKP
                                 join c in data.KPhongs on k.MaKP equals c.MaKP
                                 group new { b, c } by new { b.MaDV, b.TenDV, b.DonGia, b.DonVi } into kq
                                 select new
                                 {
                                     kq.Key.MaDV,
                                     kq.Key.TenDV,
                                     kq.Key.DonGia,
                                     kq.Key.DonVi,
                                     SLuong = kq.Where(p => p.b.Status == 1 || (p.b.Status == 0 && p.c.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc)).Sum(p => p.b.SoLuong),
                                     ThanhTien = kq.Where(p => p.b.Status == 1 || (p.b.Status == 0 && p.c.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc)).Sum(p => p.b.ThanhTien),
                                 }).ToList();
                    }
                    foreach (var d in qduoc)
                    {
                        Thuoc moi = new Thuoc();
                        moi.MaBNhan = -1;
                        moi.MaDV1 = d.MaDV ?? 0;
                        moi.TenDV1 = d.TenDV;
                        moi.DonGia1 = d.DonGia;
                        moi.DVT1 = d.DonVi;
                        moi.DuocSL1 = d.SLuong;
                        moi.DuocTT1 = d.ThanhTien;
                        _Thuoc1.Add(moi);
                    }
                }
                if (radNgay.SelectedIndex == 1)//theo ngày xuất
                {
                    var qnd1 = (from nd in data.NhapDs.Where(p => (p.PLoai == 1 && p.KieuDon == 2 && (p.TraDuoc_KieuDon == 0 || p.TraDuoc_KieuDon == 1 || p.TraDuoc_KieuDon == 4 ||  (DungChung.Bien.MaBV == "30007" ? p.TraDuoc_KieuDon == 6 : false) || ( DungChung.Bien.MaBV == "30009" ? true : false))) 
                        || (p.PLoai == 2 && (p.KieuDon == 0 || p.KieuDon == 1 || p.KieuDon == 4 || (DungChung.Bien.MaBV == "30007" ? p.KieuDon == 6 : false) || (DungChung.Bien.MaBV == "30009" ? true: false))))
                                //.Where(p => p.PLoai == 2).Where(p => radNoiTru.SelectedIndex == 2 ? (p.KieuDon == 0 || p.KieuDon == 1 || p.KieuDon == 4) : (radNoiTru.SelectedIndex == 0 ? (p.KieuDon == 0 || p.KieuDon == 4) : p.KieuDon == 1))
                                join ndct in data.NhapDcts.Where(p => dtuong == 100 || p.IDDTBN == dtuong) on nd.IDNhap equals ndct.IDNhap
                                join dv in data.DichVus on ndct.MaDV equals dv.MaDV
                                select new
                                {
                                    dv.MaDV,
                                    dv.TenDV,
                                    ndct.DonGia,
                                    ndct.DonVi,
                                    nd.PLoai,
                                    nd.MaKPnx,
                                    nd.MaKP,
                                    nd.KieuDon,
                                    nd.TraDuoc_KieuDon,
                                    nd.MaBNhan,
                                    nd.NgayNhap,
                                    ndct.SoLuongN,
                                    ndct.ThanhTienN,
                                    SLuongX = ndct.SoLuongX,
                                    ThanhTienX = ndct.ThanhTienX,
                                    nd.Status
                                }).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).ToList();

                    var nhapd = (from kp in _Kphong1
                                 join nd in qnd1 on kp.makp equals  nd.MaKPnx
                                 join kho in khoChon on nd.MaKP equals kho.MaKP
                                 group nd by new { nd.MaDV, nd.TenDV, nd.DonGia, nd.DonVi } into kq
                                 select new
                                 {
                                     kq.Key.MaDV,
                                     kq.Key.TenDV,
                                     kq.Key.DonGia,
                                     kq.Key.DonVi,
                                     SLNhapTDNoiTru = kq.Where(p => p.PLoai == 1 && p.KieuDon == 2 && p.TraDuoc_KieuDon == 1).Sum(p => p.SoLuongN),
                                     TTNhapTDNoiTru = kq.Where(p => p.PLoai == 1 && p.KieuDon == 2 && p.TraDuoc_KieuDon == 1).Sum(p => p.ThanhTienN),
                                     SLNhapTDNgTru = kq.Where(p => p.PLoai == 1 && p.KieuDon == 2 && p.TraDuoc_KieuDon == 0).Sum(p => p.SoLuongN),
                                     TTNhapTDNgTru = kq.Where(p => p.PLoai == 1 && p.KieuDon == 2 && p.TraDuoc_KieuDon == 0).Sum(p => p.ThanhTienN),
                                     SLNhapTDDTNgTru = kq.Where(p => p.PLoai == 1 && p.KieuDon == 2 && p.TraDuoc_KieuDon == 4).Sum(p => p.SoLuongN),
                                     TTNhapTDDTNgTru = kq.Where(p => p.PLoai == 1 && p.KieuDon == 2 && p.TraDuoc_KieuDon == 4).Sum(p => p.ThanhTienN),

                                     SLxuatNT = kq.Where(p => p.PLoai == 2 && p.KieuDon == 1).Sum(p => p.SLuongX),
                                     TTxuatNT = kq.Where(p => p.PLoai == 2 && p.KieuDon == 1).Sum(p => p.ThanhTienX),
                                     SLxuatNgT = kq.Where(p => p.PLoai == 2 && p.KieuDon == 0).Sum(p => p.SLuongX),
                                     TTxuatNgT = kq.Where(p => p.PLoai == 2 && p.KieuDon == 0).Sum(p => p.ThanhTienX),
                                     SLxuatDTNT = kq.Where(p => p.PLoai == 2 && p.KieuDon == 4).Sum(p => p.SLuongX),
                                     TTxuatDTNT = kq.Where(p => p.PLoai == 2 && p.KieuDon == 4).Sum(p => p.ThanhTienX),

                                     SLDuoc = kq.Where(p => p.PLoai == 2 && p.KieuDon == 6).Sum(p => p.SLuongX),
                                     TTDuoc = kq.Where(p => p.PLoai == 2 && p.KieuDon == 6).Sum(p => p.ThanhTienX),
                                     //30009
                                     SLNhapTD = kq.Where(p => p.PLoai == 1).Sum(p => p.SoLuongN) == null ? 0 : kq.Where(p => p.PLoai == 1).Sum(p => p.SoLuongN),
                                     TTNhapTD = kq.Where(p => p.PLoai == 1).Sum(p => p.ThanhTienN) == null ? 0 : kq.Where(p => p.PLoai == 1).Sum(p => p.ThanhTienN),
                                     SLXuat = kq.Where(p => p.PLoai == 2).Sum(p => p.SLuongX) == null ? 0 : kq.Where(p => p.PLoai == 2).Sum(p => p.SLuongX),
                                     TTXuat = kq.Where(p => p.PLoai == 2).Sum(p => p.ThanhTienX) == null ? 0 : kq.Where(p => p.PLoai == 2).Sum(p => p.ThanhTienX)


                                    
                                 }).ToList();
                    foreach (var a in nhapd)
                    {
                        Thuoc moi = new Thuoc();
                        moi.MaBNhan = -1;
                        moi.MaDV1 = a.MaDV;
                        moi.TenDV1 = a.TenDV;
                        moi.DonGia1 = a.DonGia;
                        moi.DVT1 = a.DonVi;
                        if (DungChung.Bien.MaBV == "30009")
                        {
                            moi.DuocSL1 = a.SLXuat - a.SLNhapTD ;
                            moi.DuocTT1 = a.TTXuat - a.TTNhapTD ;
                        }
                        else
                        {
                            if (radNoiTru.SelectedIndex == 2)
                            {
                                moi.DuocSL1 = a.SLxuatNT + a.SLxuatNgT + a.SLxuatDTNT - a.SLNhapTDDTNgTru - a.SLNhapTDNgTru - a.SLNhapTDNoiTru + a.SLDuoc;
                                moi.DuocTT1 = a.TTxuatDTNT + a.TTxuatNgT + a.TTxuatNT - a.TTNhapTDDTNgTru - a.TTNhapTDNgTru - a.TTNhapTDNoiTru + a.TTDuoc;
                            }
                            else if (radNoiTru.SelectedIndex == 0)
                            {
                                moi.DuocSL1 = a.SLxuatNgT + a.SLxuatDTNT - a.SLNhapTDDTNgTru - a.SLNhapTDNgTru + a.SLDuoc;
                                moi.DuocTT1 = a.TTxuatDTNT + a.TTxuatNgT - a.TTNhapTDDTNgTru - a.TTNhapTDNgTru + a.TTDuoc;

                            }
                            else if (radNoiTru.SelectedIndex == 1)
                            {
                                moi.DuocSL1 = a.SLxuatNT - a.SLNhapTDNoiTru + a.SLDuoc;
                                moi.DuocTT1 = a.TTxuatNT - a.TTNhapTDNoiTru + a.TTDuoc;
                            }
                        }
                        _Thuoc1.Add(moi);
                    }
                }
                int num = 1;
                var qdvudongia = (from t in _Thuoc1
                                  group t by new { t.MaDV1, t.TenDV1, t.DonGia1, t.DVT1 } into kq
                                  select new
                                  {
                                      kq.Key.MaDV1,
                                      kq.Key.TenDV1,
                                      kq.Key.DonGia1,
                                      kq.Key.DVT1
                                  }).OrderBy(p => p.TenDV1).ToList();
                var ldvu = (from dv in qdvudongia select new { sttDichVu = num++, TenDV1 = dv.TenDV1, MaDV1 = dv.MaDV1, DonGia1 = dv.DonGia1, dv.DVT1 }).ToList();

                foreach (Thuoc t in _Thuoc1)
                {
                    var stt = ldvu.Where(p => p.MaDV1 == t.MaDV1 && p.DonGia1 == t.DonGia1 && p.DVT1 == t.DVT1).FirstOrDefault();
                    if (stt != null)
                        t.sttDichVu = stt.sttDichVu;
                }

                if (!ckTonCK.Checked && !ckTonDK.Checked && !ckTTtrongky.Checked)
                    _Thuoc1 = _Thuoc1.Where(p => p.DuocSL1 != 0 || p.VPDKSL1 != 0 || p.VPTKSL1 != 0 || p.VPCKSL1 != 0).ToList();
                else
                {
                    if (rddkien.SelectedIndex == 0)// điều kiện và
                        _Thuoc1 = _Thuoc1.Where(p => p.DuocSL1 != 0 || ((ckTonDK.Checked ? p.VPDKSL1 != 0 : true)) && (ckTTtrongky.Checked ? p.VPTKSL1 != 0 : true) && (ckTonCK.Checked ? p.VPCKSL1 != 0 : true)).ToList();
                    else if (rddkien.SelectedIndex == 1)// điều kiện hoặc
                        _Thuoc1 = _Thuoc1.Where(p => p.DuocSL1 != 0 || (ckTonDK.Checked && p.VPDKSL1 != 0) || (p.VPTKSL1 != 0 && ckTTtrongky.Checked) || (p.VPCKSL1 != 0 && ckTonCK.Checked)).ToList();
                }

                if (radMauBC.SelectedIndex == 0)
                {
                    BaoCao.Rep_DsTonGoiThuoc rep = new BaoCao.Rep_DsTonGoiThuoc(htbn);
                    rep.TuNgayDenNgay.Value = "Từ ngày " + tungay.ToString().Substring(0, 10) + " đến ngày " + denngay.ToString().Substring(0, 10);
                    rep.BindingData();
                    rep.DataSource = _Thuoc1.OrderBy(p => p.sttDichVu).ThenBy(p => p.TenBN).ToList();
                    rep.CreateDocument();
                    frmIn frm = new frmIn();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    var dsThuoc = (from t in _Thuoc1
                                   group t by new { t.TenDV1, t.DVT1, t.DonGia1 } into n
                                   select new
                                   {
                                       n.Key.TenDV1,
                                       n.Key.DVT1,
                                       n.Key.DonGia1,
                                       DuocSL1 = n.Sum(p => p.DuocSL1),
                                       DuocTT1 = n.Sum(p => p.DuocTT1),
                                       VPDKSL1 = n.Sum(p => p.VPDKSL1),
                                       VPDKTT1 = n.Sum(p => p.VPDKTT1),
                                       VPTKBHYTSL1 = n.Sum(p => p.VPTKBHYTSL1),
                                       VPTKBHYTTT1 = n.Sum(p => p.VPTKBHYTTT1),
                                       VPTKKhongTTSL1 = n.Sum(p => p.VPTKKhongTTSL1),
                                       VPTKKhongTTTT1 = n.Sum(p => p.VPTKKhongTTTT1),
                                       VPTKDVSL1 = n.Sum(p => p.VPTKDVSL1),
                                       VPTKDVTT1 = n.Sum(p => p.VPTKDVTT1),
                                       VPCKSL1 = n.Sum(p => p.VPCKSL1),
                                       VPCKTT1 = n.Sum(p => p.VPCKTT1),
                                       VPTKBASL1 = n.Sum(p => p.VPTKBASL1),
                                       VPTKBATT1 = n.Sum(p => p.VPTKBATT1)
                                   }).ToList();
                    BaoCao.Rep_BC_THThuocVTYT rep1 = new BaoCao.Rep_BC_THThuocVTYT(htbn);
                    rep1.lblTuNgayDenNgay.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
                    rep1.DataSource = dsThuoc.OrderBy(p => p.TenDV1).ToList();
                    rep1.BindingData();
                    rep1.CreateDocument();
                    frmIn frm = new frmIn();
                    frm.prcIN.PrintingSystem = rep1.PrintingSystem;
                    frm.ShowDialog();
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void grvKhoDuoc_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChon")
            {
                if (grvKhoDuoc.GetFocusedRowCellValue("TenKP") != null)
                {
                    string Ten = grvKhoDuoc.GetFocusedRowCellValue("TenKP").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_lKhoDuoc.First().Chon == true)
                        {
                            foreach (var a in _lKhoDuoc)
                            {
                                a.Chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lKhoDuoc)
                            {
                                a.Chon = true;
                            }
                        }
                        grcKhoDuoc.DataSource = "";
                        grcKhoDuoc.DataSource = _lKhoDuoc.ToList();
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            #region Update MaKXuat
            var dtct1 = (from dtct in data.DThuoccts
                         join dv in data.DichVus.Where(p => p.PLoai == 1) on dtct.MaDV equals dv.MaDV
                         select dtct).Where(p => p.MaKXuat == null).ToList();
            var m = (from n in data.DThuocs
                     select new { n.IDDon, n.MaKXuat }).Where(p => p.MaKXuat != null && p.MaKXuat != 0).ToList();
            var kq = (from a in m
                      join d in dtct1 on a.IDDon equals d.IDDon
                      select d).ToList();

            foreach (var item in m)
            {
                var update = kq.Where(p => p.IDDon == item.IDDon).ToList();
                if (update.Count > 0)
                {
                    foreach (var _dtct in update)
                    {
                        _dtct.MaKXuat = item.MaKXuat;
                        data.SaveChanges();
                    }
                }
            }
            #endregion
        }

        private void rddkien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rddkien.SelectedIndex == 0)
            {
                MessageBox.Show("Điều kiện và khi thỏa mãn cả ba điều kiện dưới cùng có dữ liệu.\n- Chi tiết BN tồn B.A đầu kỳ.\n- Chi tiết BN thanh toán trong kỳ.\n- Chi tiết BN tồn B.A cuối kỳ.", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (rddkien.SelectedIndex == 1)
            {
                MessageBox.Show("Điều kiện hoặc khi một trong ba điều kiện dưới có dữ liệu.\n- Chi tiết BN tồn B.A đầu kỳ.\n- Chi tiết BN thanh toán trong kỳ.\n- Chi tiết BN tồn B.A cuối kỳ.", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}