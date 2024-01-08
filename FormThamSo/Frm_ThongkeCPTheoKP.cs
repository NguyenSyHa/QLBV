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
    public partial class Frm_ThongkeCPTheoKP : DevExpress.XtraEditors.XtraForm
    {
        public Frm_ThongkeCPTheoKP()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
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
        List<KPhong> _Kho = new List<KPhong>();
        private void Frm_ThongkeCPTheoKP_Load(object sender, EventArgs e)
        {
            doituong = rdBHYT.SelectedIndex == 0 ? "BHYT" : "Dịch vụ";
            _Kphong = new List<KPhong>();
            lupNgaytu.DateTime = System.DateTime.Now;
            lupNgayden.DateTime = System.DateTime.Now;
            var kphong = (from kp in _Data.KPhongs
                          where (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham)
                          select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                KPhong themmoi1 = new KPhong();
                themmoi1.tenkp = " Tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = false;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhong themmoi = new KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = false;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }
            var kho = (from kp in _Data.KPhongs
                       where (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc)
                       select new { kp.TenKP, kp.MaKP }).ToList();
            if (kho.Count > 0)
            {
                KPhong themmoi2 = new KPhong();
                themmoi2.tenkp = " Tất cả";
                themmoi2.makp = 0;
                themmoi2.chon = false;
                _Kho.Add(themmoi2);
                foreach (var a in kho)
                {
                    KPhong themmoi = new KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = false;
                    _Kho.Add(themmoi);
                }
                grcKho.DataSource = _Kho.ToList();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        string doituong = "", tenDieuTri = "";
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //bệnh viện thường tín  bị trùng do danh mục bệnh viện trùng mã rất nhiều
            doituong = rdBHYT.SelectedIndex == 0 ? "BHYT" : "Dịch vụ";
            DateTime _Ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime _Ngayden = DungChung.Ham.NgayDen(lupNgayden.DateTime);
            frmIn frm = new frmIn();
            BaoCao.Rep_ThongkeCPTheoKP rep = new BaoCao.Rep_ThongkeCPTheoKP(rdMau.SelectedIndex);
            List<int> lkp = _Kphong.Where(p => p.chon == true).Select(p => p.makp).ToList();
            List<int> lkho = _Kho.Where(p => p.chon == true).Select(p => p.makp).ToList();
            var qdv = (from dv in _Data.DichVus
                       join tn in _Data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                       join nhom in _Data.NhomDVs on tn.IDNhom equals nhom.IDNhom
                       select new { dv.MaDV, dv.TenDV, nhom.TenNhomCT, nhom.IDNhom }).ToList();
            if (rdBHYT.SelectedIndex == 0)
            {
                #region BHYT
                if (DungChung.Bien.MaBV == "26007")
                {
                    #region 27006
                    var qbn = (from vp in _Data.VienPhis.Where(p => rdLoaiNgay.SelectedIndex == 0 ? (p.NgayTT >= _Ngaytu && p.NgayTT <= _Ngayden) : (p.NgayDuyet >= _Ngaytu && p.NgayDuyet <= _Ngayden))
                               join bn in _Data.BenhNhans.Where(p => (rdNoiNgoaiTru.SelectedIndex == 1 && p.NoiTru == 1) || (rdNoiNgoaiTru.SelectedIndex == 0 && p.NoiTru == 0 && ((chkDTNgoaiTru.Checked && p.DTNT == true) || (chkKBNgoaiTru.Checked && p.DTNT == false)))).Where(p => p.DTuong == (doituong)) on vp.MaBNhan equals bn.MaBNhan
                               join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                               join kp in _Data.KPhongs on bn.MaKP equals kp.MaKP
                               // join bv in _Data.BenhViens on bn.MaCS equals bv.MaBV
                               select new
                               {
                                   Mabn = bn.MaBNhan,
                                   bn.TenBNhan,
                                   bn.SThe,
                                   rv.MaICD,
                                   bn.NNhap,
                                   kp.TenKP,
                                   bn.MaKP
                                   //TenBV = bv.TenBV.Trim(),
                               }).ToList();

                    var qvp = (from vp in _Data.VienPhis.Where(p => rdLoaiNgay.SelectedIndex == 0 ? (p.NgayTT >= _Ngaytu && p.NgayTT <= _Ngayden) : (p.NgayDuyet >= _Ngaytu && p.NgayDuyet <= _Ngayden))
                               join vpct in _Data.VienPhicts.Where(p => p.TrongBH == 1 || (ckHienThiCP.Checked && p.TrongBH == 2)) on vp.idVPhi equals vpct.idVPhi
                               select new { vpct.ThanhTien, vpct.TienBN, vpct.MaDV, vp.MaBNhan }).ToList();

                    var q = (from bn in qbn
                             join vp in qvp on bn.Mabn equals vp.MaBNhan
                             join kp in lkp on bn.MaKP equals kp
                             join dv in qdv on vp.MaDV equals dv.MaDV
                             group new { bn, vp, kp, dv } by new
                             {
                                 vp.MaBNhan,
                                 bn.TenBNhan,
                                 bn.Mabn,
                                 bn.SThe,
                                 bn.NNhap,
                                 bn.MaKP,
                                 bn.MaICD,
                                 // bn.TenBV,
                                 bn.TenKP,
                             } into kq
                             select new
                             {
                                 Makp = kq.Key.MaKP,
                                 Mabn = kq.Key.MaBNhan,
                                 TenBN = kq.Key.TenBNhan,
                                 //  DKKCB = kq.Key.TenBV,
                                 Sothe = kq.Key.SThe,
                                 MaBenh = kq.Key.MaICD,
                                 Ngaykham = kq.Key.NNhap,
                                 kq.Key.TenKP,
                                 Thuoc = kq.Where(p => p.dv.IDNhom == 4).Sum(p => p.vp.ThanhTien),
                                 CDHA = kq.Where(p => p.dv.TenNhomCT == "Chẩn đoán hình ảnh").Sum(p => p.vp.ThanhTien),
                                 TDCN = kq.Where(p => p.dv.IDNhom == 3).Sum(p => p.vp.ThanhTien),
                                 Congkham = kq.Where(p => p.dv.TenNhomCT == "Khám bệnh").Sum(p => p.vp.ThanhTien),
                                 Xetnghiem = kq.Where(p => p.dv.TenNhomCT == "Xét nghiệm").Sum(p => p.vp.ThanhTien),
                                 Mau = kq.Where(p => p.dv.TenNhomCT == "Máu và chế phẩm của máu").Sum(p => p.vp.ThanhTien),
                                 TTPT = kq.Where(p => p.dv.TenNhomCT == "Thủ thuật, phẫu thuật").Sum(p => p.vp.ThanhTien),
                                 VTTH = kq.Where(p => p.dv.TenNhomCT == "Vật tư y tế trong danh mục BHYT").Sum(p => p.vp.ThanhTien),
                                 VTTT = kq.Where(p => p.dv.TenNhomCT == "VTYT thanh toán theo tỷ lệ").Sum(p => p.vp.ThanhTien),
                                 DVKTC = kq.Where(p => p.dv.TenNhomCT == "DVKT thanh toán theo tỷ lệ").Sum(p => p.vp.ThanhTien),
                                 TTG = kq.Where(p => p.dv.TenNhomCT == "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục").Sum(p => p.vp.ThanhTien),
                                 Vanchuyen = kq.Where(p => p.dv.TenNhomCT == "Vận chuyển").Sum(p => p.vp.ThanhTien),
                                 TongCP = kq.Sum(p => p.vp.ThanhTien),
                                 BNchitra = kq.Sum(p => p.vp.TienBN),
                                 TienGiuong = kq.Where(p => rdNoiNgoaiTru.SelectedIndex == 0 ? (p.dv.TenNhomCT == "Giường điều trị ngoại trú") : (p.dv.TenNhomCT == "Giường điều trị nội trú")).Sum(p => p.vp.ThanhTien),
                             }).OrderBy(p => p.Mabn).ToList();
                    if (lkho.Count() > 0)
                    {
                        var bns = (from b in _Data.DThuocs
                                   join c in _Data.DThuoccts on b.IDDon equals c.IDDon
                                   join d in lkho on c.MaKXuat equals d
                                   select b.MaBNhan).Distinct().ToList();
                        if (bns.Count() > 0)
                        {
                            q = (from a in q
                                 join b in bns on a.Mabn equals b
                                 select a).ToList();
                            rep.DataSource = q.OrderBy(p => p.TenBN).ToList();
                        }
                        else
                        {
                            rep.DataSource = null;
                        }
                    }
                    else
                        rep.DataSource = q.OrderBy(p => p.TenBN).ToList();
                    #endregion
                }
                else
                {
                    var qbn = (from vp in _Data.VienPhis.Where(p => rdLoaiNgay.SelectedIndex == 0 ? (p.NgayTT >= _Ngaytu && p.NgayTT <= _Ngayden) : (p.NgayDuyet >= _Ngaytu && p.NgayDuyet <= _Ngayden))
                               join bn in _Data.BenhNhans.Where(p => (rdNoiNgoaiTru.SelectedIndex == 1 && p.NoiTru == 1) || (rdNoiNgoaiTru.SelectedIndex == 0 && p.NoiTru == 0 && ((chkDTNgoaiTru.Checked && p.DTNT == true) || (chkKBNgoaiTru.Checked && p.DTNT == false)))).Where(p => p.DTuong == (doituong)) on vp.MaBNhan equals bn.MaBNhan
                               join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                               // join bv in _Data.BenhViens on bn.MaCS equals bv.MaBV
                               select new
                               {
                                   Mabn = bn.MaBNhan,
                                   bn.TenBNhan,
                                   bn.SThe,
                                   rv.MaICD,
                                   bn.NNhap,
                                   //TenBV = bv.TenBV.Trim(),
                               }).ToList();
                    var qvp1 = (from vp in _Data.VienPhis.Where(p => rdLoaiNgay.SelectedIndex == 0 ? (p.NgayTT >= _Ngaytu && p.NgayTT <= _Ngayden) : (p.NgayDuyet >= _Ngaytu && p.NgayDuyet <= _Ngayden))
                                join vpct in _Data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                                join kp in _Data.KPhongs on vpct.MaKP equals kp.MaKP
                                select new { vpct.ThanhTien, vpct.TienBN, vpct.MaDV, vpct.MaKP, kp.TenKP, vp.MaBNhan, vpct.TrongBH }).ToList();
                    var qvp = qvp1;
                    if (DungChung.Bien.MaBV != "27183")
                        qvp = qvp1.Where(p => p.TrongBH == 1 || (ckHienThiCP.Checked && p.TrongBH == 2)).ToList();
                    var q = (from bn in qbn
                             join vp in qvp on bn.Mabn equals vp.MaBNhan
                             join kp in lkp on vp.MaKP equals kp
                             join dv in qdv on vp.MaDV equals dv.MaDV
                             group new { bn, vp, kp, dv } by new
                             {
                                 vp.MaBNhan,
                                 bn.TenBNhan,
                                 bn.Mabn,
                                 bn.SThe,
                                 bn.NNhap,
                                 vp.MaKP,
                                 bn.MaICD,
                                 // bn.TenBV,
                                 vp.TenKP,
                             } into kq
                             select new
                             {
                                 Makp = kq.Key.MaKP,
                                 Mabn = kq.Key.MaBNhan,
                                 TenBN = kq.Key.TenBNhan,
                                 //  DKKCB = kq.Key.TenBV,
                                 Sothe = kq.Key.SThe,
                                 MaBenh = kq.Key.MaICD,
                                 Ngaykham = kq.Key.NNhap,
                                 kq.Key.TenKP,
                                 Thuoc = kq.Where(p => p.dv.IDNhom == 4).Sum(p => p.vp.ThanhTien),
                                 CDHA = kq.Where(p => p.dv.TenNhomCT == "Chẩn đoán hình ảnh").Sum(p => p.vp.ThanhTien),
                                 TDCN = kq.Where(p => p.dv.IDNhom == 3).Sum(p => p.vp.ThanhTien),
                                 Congkham = kq.Where(p => p.dv.TenNhomCT == "Khám bệnh").Sum(p => p.vp.ThanhTien),
                                 Xetnghiem = kq.Where(p => p.dv.TenNhomCT == "Xét nghiệm").Sum(p => p.vp.ThanhTien),
                                 Mau = kq.Where(p => p.dv.TenNhomCT == "Máu và chế phẩm của máu").Sum(p => p.vp.ThanhTien),
                                 TTPT = kq.Where(p => p.dv.TenNhomCT == "Thủ thuật, phẫu thuật").Sum(p => p.vp.ThanhTien),
                                 VTTH = kq.Where(p => p.dv.TenNhomCT == "Vật tư y tế trong danh mục BHYT").Sum(p => p.vp.ThanhTien),
                                 VTTT = kq.Where(p => p.dv.TenNhomCT == "VTYT thanh toán theo tỷ lệ").Sum(p => p.vp.ThanhTien),
                                 DVKTC = kq.Where(p => p.dv.TenNhomCT == "DVKT thanh toán theo tỷ lệ").Sum(p => p.vp.ThanhTien),
                                 TTG = kq.Where(p => p.dv.TenNhomCT == "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục").Sum(p => p.vp.ThanhTien),
                                 Vanchuyen = kq.Where(p => p.dv.TenNhomCT == "Vận chuyển").Sum(p => p.vp.ThanhTien),
                                 TongCP = kq.Sum(p => p.vp.ThanhTien),
                                 BNchitra = kq.Sum(p => p.vp.TienBN),
                                 TienGiuong = kq.Where(p => rdNoiNgoaiTru.SelectedIndex == 0 ? (p.dv.TenNhomCT == "Giường điều trị ngoại trú") : (p.dv.TenNhomCT == "Giường điều trị nội trú")).Sum(p => p.vp.ThanhTien),
                             }).OrderBy(p => p.Mabn).ToList();
                    if(lkho.Count() > 0)
                    {
                        var bns = (from b in _Data.DThuocs
                                   join c in _Data.DThuoccts on b.IDDon equals c.IDDon
                                   join d in lkho on c.MaKXuat equals d
                                   select b.MaBNhan).Distinct().ToList();
                        if (bns.Count() > 0)
                        {
                            q = (from a in q
                                 join b in bns on a.Mabn equals b
                                 select a).ToList();
                            rep.DataSource = q.OrderBy(p => p.TenBN).ToList();
                        }
                        else
                        {
                            rep.DataSource = null;
                        }
                    }
                    else
                        rep.DataSource = q.OrderBy(p => p.TenBN).ToList();
                }
                #endregion
            }
            else
            {
                #region dịch vụ

                if (DungChung.Bien.MaBV == "26007")
                {
                    #region 26007
                    var qbn = (from vp in _Data.VienPhis.Where(p => rdLoaiNgay.SelectedIndex == 0 ? (p.NgayTT >= _Ngaytu && p.NgayTT <= _Ngayden) : (p.NgayDuyet >= _Ngaytu && p.NgayDuyet <= _Ngayden))
                               join bn in _Data.BenhNhans.Where(p => (rdNoiNgoaiTru.SelectedIndex == 1 && p.NoiTru == 1) || (rdNoiNgoaiTru.SelectedIndex == 0 && p.NoiTru == 0 && ((chkDTNgoaiTru.Checked && p.DTNT == true) || (chkKBNgoaiTru.Checked && p.DTNT == false)))).Where(p => p.DTuong == (doituong)) on vp.MaBNhan equals bn.MaBNhan
                               join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                               join kp in _Data.KPhongs on bn.MaKP equals kp.MaKP
                               select new
                               {
                                   Mabn = bn.MaBNhan,
                                   bn.TenBNhan,
                                   bn.SThe,
                                   rv.MaICD,
                                   bn.NNhap,
                                   bn.MaKP,
                                   kp.TenKP
                               }).ToList();

                    var qvp = (from vp in _Data.VienPhis.Where(p => rdLoaiNgay.SelectedIndex == 0 ? (p.NgayTT >= _Ngaytu && p.NgayTT <= _Ngayden) : (p.NgayDuyet >= _Ngaytu && p.NgayDuyet <= _Ngayden))
                               join vpct in _Data.VienPhicts.Where(p => p.TrongBH == 0 || (ckHienThiCP.Checked && p.TrongBH == 2)) on vp.idVPhi equals vpct.idVPhi

                               select new { vpct.ThanhTien, vpct.TienBN, vpct.MaDV, vp.MaBNhan }).ToList();


                    var q = (from bn in qbn
                             join vp in qvp on bn.Mabn equals vp.MaBNhan
                             join kp in lkp on bn.MaKP equals kp
                             join dv in qdv on vp.MaDV equals dv.MaDV
                             group new
                             {
                                 bn,
                                 vp,
                                 kp,
                                 dv
                             } by new
                             {
                                 vp.MaBNhan,
                                 bn.TenBNhan,
                                 bn.Mabn,
                                 bn.SThe,
                                 bn.NNhap,
                                 bn.MaKP,
                                 bn.MaICD,
                                 bn.TenKP,
                             } into kq
                             select new
                             {
                                 Makp = kq.Key.MaKP,
                                 Mabn = kq.Key.MaBNhan,
                                 TenBN = kq.Key.TenBNhan,
                                 Sothe = kq.Key.SThe,
                                 MaBenh = kq.Key.MaICD,
                                 Ngaykham = kq.Key.NNhap,
                                 kq.Key.TenKP,
                                 Thuoc = kq.Where(p => p.dv.IDNhom == 4).Sum(p => p.vp.ThanhTien),
                                 CDHA = kq.Where(p => p.dv.TenNhomCT == "Chẩn đoán hình ảnh").Sum(p => p.vp.ThanhTien),
                                 TDCN = kq.Where(p => p.dv.IDNhom == 3).Sum(p => p.vp.ThanhTien),
                                 Congkham = kq.Where(p => p.dv.TenNhomCT == "Khám bệnh").Sum(p => p.vp.ThanhTien),
                                 Xetnghiem = kq.Where(p => p.dv.TenNhomCT == "Xét nghiệm").Sum(p => p.vp.ThanhTien),
                                 Mau = kq.Where(p => p.dv.TenNhomCT == "Máu và chế phẩm của máu").Sum(p => p.vp.ThanhTien),
                                 TTPT = kq.Where(p => p.dv.TenNhomCT == "Thủ thuật, phẫu thuật").Sum(p => p.vp.ThanhTien),
                                 VTTH = kq.Where(p => p.dv.TenNhomCT == "Vật tư y tế trong danh mục BHYT").Sum(p => p.vp.ThanhTien),
                                 VTTT = kq.Where(p => p.dv.TenNhomCT == "VTYT thanh toán theo tỷ lệ").Sum(p => p.vp.ThanhTien),
                                 DVKTC = kq.Where(p => p.dv.TenNhomCT == "DVKT thanh toán theo tỷ lệ").Sum(p => p.vp.ThanhTien),
                                 TTG = kq.Where(p => p.dv.TenNhomCT == "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục").Sum(p => p.vp.ThanhTien),
                                 Vanchuyen = kq.Where(p => p.dv.TenNhomCT == "Vận chuyển").Sum(p => p.vp.ThanhTien),
                                 TongCP = kq.Sum(p => p.vp.ThanhTien),
                                 BNchitra = kq.Sum(p => p.vp.TienBN),
                                 TienGiuong = kq.Where(p => rdNoiNgoaiTru.SelectedIndex == 0 ? (p.dv.TenNhomCT == "Giường điều trị ngoại trú") : (p.dv.TenNhomCT == "Giường điều trị nội trú")).Sum(p => p.vp.ThanhTien),
                             }).OrderBy(p => p.Mabn).ToList();
                    if (lkho.Count() > 0)
                    {
                        var bns = (from b in _Data.DThuocs
                                   join c in _Data.DThuoccts on b.IDDon equals c.IDDon
                                   join d in lkho on c.MaKXuat equals d
                                   select b.MaBNhan).Distinct().ToList();
                        if (bns.Count() > 0)
                        {
                            q = (from a in q
                                 join b in bns on a.Mabn equals b
                                 select a).ToList();
                            rep.DataSource = q.OrderBy(p => p.TenBN).ToList();
                        }
                        else
                        {
                            rep.DataSource = null;
                        }
                    }
                    else
                        rep.DataSource = q.OrderBy(p => p.TenBN).ToList();
                    #endregion
                }

                else
                {
                    var qbn = (from vp in _Data.VienPhis.Where(p => rdLoaiNgay.SelectedIndex == 0 ? (p.NgayTT >= _Ngaytu && p.NgayTT <= _Ngayden) : (p.NgayDuyet >= _Ngaytu && p.NgayDuyet <= _Ngayden))
                               join bn in _Data.BenhNhans.Where(p => (rdNoiNgoaiTru.SelectedIndex == 1 && p.NoiTru == 1) || (rdNoiNgoaiTru.SelectedIndex == 0 && p.NoiTru == 0 && ((chkDTNgoaiTru.Checked && p.DTNT == true) || (chkKBNgoaiTru.Checked && p.DTNT == false)))).Where(p => p.DTuong == (doituong)) on vp.MaBNhan equals bn.MaBNhan
                               join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                               select new
                               {
                                   Mabn = bn.MaBNhan,
                                   bn.TenBNhan,
                                   bn.SThe,
                                   rv.MaICD,
                                   bn.NNhap,
                               }).ToList();

                    var qvp1 = (from vp in _Data.VienPhis.Where(p => rdLoaiNgay.SelectedIndex == 0 ? (p.NgayTT >= _Ngaytu && p.NgayTT <= _Ngayden) : (p.NgayDuyet >= _Ngaytu && p.NgayDuyet <= _Ngayden))
                                join vpct in _Data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                                join kp in _Data.KPhongs on vpct.MaKP equals kp.MaKP
                                select new { vpct.ThanhTien, vpct.TienBN, vpct.MaDV, vpct.MaKP, kp.TenKP, vp.MaBNhan, vpct.TrongBH }).ToList();
                    var qvp = qvp1;
                    if (DungChung.Bien.MaBV != "27183")
                        qvp = qvp1.Where(p => p.TrongBH == 1 || (ckHienThiCP.Checked && p.TrongBH == 2)).ToList();

                    var q = (from bn in qbn
                             join vp in qvp on bn.Mabn equals vp.MaBNhan
                             join kp in lkp on vp.MaKP equals kp
                             join dv in qdv on vp.MaDV equals dv.MaDV
                             group new
                             {
                                 bn,
                                 vp,
                                 kp,
                                 dv
                             } by new
                             {
                                 vp.MaBNhan,
                                 bn.TenBNhan,
                                 bn.Mabn,
                                 bn.SThe,
                                 bn.NNhap,
                                 vp.MaKP,
                                 bn.MaICD,
                                 vp.TenKP,
                             } into kq
                             select new
                             {
                                 Makp = kq.Key.MaKP,
                                 Mabn = kq.Key.MaBNhan,
                                 TenBN = kq.Key.TenBNhan,
                                 Sothe = kq.Key.SThe,
                                 MaBenh = kq.Key.MaICD,
                                 Ngaykham = kq.Key.NNhap,
                                 kq.Key.TenKP,
                                 Thuoc = kq.Where(p => p.dv.IDNhom == 4).Sum(p => p.vp.ThanhTien),
                                 CDHA = kq.Where(p => p.dv.TenNhomCT == "Chẩn đoán hình ảnh").Sum(p => p.vp.ThanhTien),
                                 TDCN = kq.Where(p => p.dv.IDNhom == 3).Sum(p => p.vp.ThanhTien),
                                 Congkham = kq.Where(p => p.dv.TenNhomCT == "Khám bệnh").Sum(p => p.vp.ThanhTien),
                                 Xetnghiem = kq.Where(p => p.dv.TenNhomCT == "Xét nghiệm").Sum(p => p.vp.ThanhTien),
                                 Mau = kq.Where(p => p.dv.TenNhomCT == "Máu và chế phẩm của máu").Sum(p => p.vp.ThanhTien),
                                 TTPT = kq.Where(p => p.dv.TenNhomCT == "Thủ thuật, phẫu thuật").Sum(p => p.vp.ThanhTien),
                                 VTTH = kq.Where(p => p.dv.TenNhomCT == "Vật tư y tế trong danh mục BHYT").Sum(p => p.vp.ThanhTien),
                                 VTTT = kq.Where(p => p.dv.TenNhomCT == "VTYT thanh toán theo tỷ lệ").Sum(p => p.vp.ThanhTien),
                                 DVKTC = kq.Where(p => p.dv.TenNhomCT == "DVKT thanh toán theo tỷ lệ").Sum(p => p.vp.ThanhTien),
                                 TTG = kq.Where(p => p.dv.TenNhomCT == "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục").Sum(p => p.vp.ThanhTien),
                                 Vanchuyen = kq.Where(p => p.dv.TenNhomCT == "Vận chuyển").Sum(p => p.vp.ThanhTien),
                                 TongCP = kq.Sum(p => p.vp.ThanhTien),
                                 BNchitra = kq.Sum(p => p.vp.TienBN),
                                 TienGiuong = kq.Where(p => rdNoiNgoaiTru.SelectedIndex == 0 ? (p.dv.TenNhomCT == "Giường điều trị ngoại trú") : (p.dv.TenNhomCT == "Giường điều trị nội trú")).Sum(p => p.vp.ThanhTien),
                             }).OrderBy(p => p.Mabn).ToList();
                    if (lkho.Count() > 0)
                    {
                        var bns = (from b in _Data.DThuocs
                                   join c in _Data.DThuoccts on b.IDDon equals c.IDDon
                                   join d in lkho on c.MaKXuat equals d
                                   select b.MaBNhan).Distinct().ToList();
                        if (bns.Count() > 0)
                        {
                            q = (from a in q
                                 join b in bns on a.Mabn equals b
                                 select a).ToList();
                            rep.DataSource = q.OrderBy(p => p.TenBN).ToList();
                        }
                        else
                        {
                            rep.DataSource = null;
                        }
                    }
                    else
                        rep.DataSource = q.OrderBy(p => p.TenBN).ToList();
                }
                #endregion
            }

            if (rdNoiNgoaiTru.SelectedIndex == 0)
            {
                if (chkDTNgoaiTru.Checked && chkKBNgoaiTru.Checked)
                {
                    tenDieuTri = "NGOẠI TRÚ " + doituong.ToUpper();
                }

                else if (chkDTNgoaiTru.Checked)
                {
                    tenDieuTri = "ĐIỀU TRỊ NGOẠI TRÚ " + doituong.ToUpper();
                }

                else if (chkKBNgoaiTru.Checked)
                {
                    tenDieuTri = "KHÁM BỆNH NGOẠI TRÚ " + doituong.ToUpper();
                }
            }

            else
            {
                tenDieuTri = "NỘI TRÚ " + doituong.ToUpper();
            }

            rep.paramTungayDenngay.Value = "Từ ngày " + lupNgaytu.DateTime.ToShortDateString() + " đến ngày " + lupNgayden.DateTime.ToShortDateString();
            string paramKP = "";
            int j = 1;
            if (_Kphong[0].chon == true)
            {
                rep.paramtenBC.Value = "BẢNG THỐNG KÊ CHI PHÍ KHÁM CHỮA BỆNH " + tenDieuTri + " TẤT CẢ CÁC KHOA PHÒNG";
            }
            else
            {
                int sokp = _Kphong.Where(k => k.chon == true).Count();
                for (int i = 0; i < _Kphong.Count(); i++)
                {
                    if (_Kphong[i].chon == true)
                    {
                        if (j == sokp)
                        {
                            paramKP += _Kphong[i].tenkp.ToUpper();
                            break;
                        }
                        paramKP += _Kphong[i].tenkp.ToUpper() + " - ";
                        j++;
                    }
                }
                rep.paramtenBC.Value = "BẢNG THỐNG KÊ CHI PHÍ KHÁM CHỮA BỆNH " + tenDieuTri + " THEO KHOA PHÒNG";
            }

            rep.Congkham.Value = "Công khám";
            rep.paramKhoaPhong.Value = paramKP;
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void rdNoiNgoaiTru_EditValueChanged(object sender, EventArgs e)
        {
            if (rdNoiNgoaiTru.SelectedIndex == 0)
            {
                pnNgoaiTru.Enabled = true;
            }
            else
            {
                pnNgoaiTru.Enabled = false;
            }
        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chọn")
            {
                if (e.RowHandle == 0)
                {
                    if (grvKhoaphong.GetFocusedRowCellValue(Chọn) != null)
                    {
                        if (grvKhoaphong.GetRowCellValue(0, Chọn).ToString() == "False")
                        {
                            for (int i = 0; i < grvKhoaphong.RowCount; i++)
                            {
                                grvKhoaphong.SetRowCellValue(i, "chon", true);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < grvKhoaphong.RowCount; i++)
                            {
                                grvKhoaphong.SetRowCellValue(i, "chon", false);
                            }
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < grvKhoaphong.RowCount; i++)
                    {
                        if (grvKhoaphong.GetRowCellValue(i, Chọn) != null && grvKhoaphong.GetRowCellValue(i, Chọn).ToString() == "True")
                        {

                        }
                        else
                        {
                            grvKhoaphong.SetRowCellValue(0, Chọn, false);
                            break;
                        }
                    }

                }
            }
        }

        private void Frm_ThongkeCPTheoKP_KeyDown(object sender, KeyEventArgs e)
        {
            //if (sender. .KeyCode == Keys.Enter)
            //{
            //    MessageBox.Show("Test");
            //}
            MessageBox.Show(sender.ToString());
        }

        private void grvKho_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Check")
            {
                if (e.RowHandle == 0)
                {
                    if (grvKho.GetFocusedRowCellValue(Check) != null)
                    {
                        if (grvKho.GetRowCellValue(0, Check).ToString() == "False")
                        {
                            for (int i = 0; i < grvKho.RowCount; i++)
                            {
                                grvKho.SetRowCellValue(i, "chon", true);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < grvKho.RowCount; i++)
                            {
                                grvKho.SetRowCellValue(i, "chon", false);
                            }
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < grvKho.RowCount; i++)
                    {
                        if (grvKho.GetRowCellValue(i, Check) != null && grvKho.GetRowCellValue(i, Check).ToString() == "True")
                        {

                        }
                        else
                        {
                            grvKho.SetRowCellValue(0, Check, false);
                            break;
                        }
                    }

                }
            }
        }

        private void rdNoiNgoaiTru_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}