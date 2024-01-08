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
    public partial class frm_BC_THThuVP_27023 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_THThuVP_27023()
        {
            InitializeComponent();
        }

        private void frm_BC_THThuVP_27023_Load(object sender, EventArgs e)
        {
            dtNgay.DateTime = DateTime.Now;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        List<VPhi> _lVPhi = new List<VPhi>();
        private void btnInBC_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(dtNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dtNgay.DateTime);
            _lVPhi.Clear();
            VPhi moi = new VPhi();
            var qdv = (from dv in data.DichVus
                       join ndv in data.NhomDVs on dv.IDNhom equals ndv.IDNhom
                       join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                       select new { dv.MaDV, dv.TenDV, ndv.TenNhom, ndv.TenNhomCT, tn.TenRG, dv.IDNhom }).ToList();

            var qtamung = (from tu in data.TamUngs.Where(p => p.PhanLoai == 0 || p.PhanLoai == 1 || p.PhanLoai == 2)
                           group tu by new { tu.PhanLoai, tu.MaBNhan, tu.NgayThu } into kq
                           select new
                           {
                               kq.Key.PhanLoai,
                               kq.Key.MaBNhan,
                               //tu.IDTamUng,
                               TienUng = kq.Key.PhanLoai == 0 ? kq.Sum(p => p.SoTien) : 0,
                               ThuThem = kq.Key.PhanLoai == 1 ? kq.Sum(p => p.TienChenh) : 0,
                               TraLai = kq.Key.PhanLoai == 2 ? kq.Sum(p => p.TienChenh) : 0,
                               kq.Key.NgayThu
                           }).ToList();

            var qbn = (from bn in data.BenhNhans select bn).ToList();

            var qvp = (from vp in data.VienPhis
                       join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                       select new { vp.MaBNhan, vpct.MaDV, vp.NgayTT, vp.NgayDuyet, vpct.TienBN }).ToList();

            var qbnNgTru = (from a in qbn.Where(p => p.NoiTru == 0 && p.DTNT == false)
                            join b in qvp.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay) on a.MaBNhan equals b.MaBNhan
                            join c in qdv on b.MaDV equals c.MaDV
                            group new { a, b, c } by new { b.NgayTT } into kq
                            select new
                            {
                                Ngay = kq.Key.NgayTT.Value.Date,
                                KhamBenh = kq.Where(p => p.c.TenNhomCT.ToLower().Contains("khám bệnh")).Sum(p => p.b.TienBN),
                                SieuAm = kq.Where(p => p.c.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Sum(p => p.b.TienBN),
                                XQ = kq.Where(p => p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Sum(p => p.b.TienBN),
                                XQ_CT = kq.Where(p => p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT ).Sum(p => p.b.TienBN),
                                XetNghiem = kq.Where(p => p.c.TenNhomCT.ToLower().Contains("xét nghiệm")).Sum(p => p.b.TienBN),
                                DienTim = kq.Where(p => p.c.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Sum(p => p.b.TienBN),
                                DoCNHH = kq.Where(p => p.c.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap)).Sum(p => p.b.TienBN),
                                ThuThuat = kq.Where(p => p.c.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat)).Sum(p => p.b.TienBN),
                                Thuoc = kq.Where(p => p.c.IDNhom == 4 || p.c.IDNhom == 5 || p.c.IDNhom == 6).Sum(p => p.b.TienBN)
                            }).ToList();

            foreach (var item in qbnNgTru)
            {
                moi = new VPhi();
                moi.Ngay = item.Ngay;
                moi.KhamBenh = item.KhamBenh;
                moi.SieuAm = item.SieuAm;
                moi.XQ = item.XQ;
                moi.XQ_CT = item.XQ_CT;
                moi.XetNghiem = item.XetNghiem;
                moi.DienTim = item.DienTim;
                moi.DoCNHH = item.DoCNHH;
                moi.ThuThuat = item.ThuThuat;
                moi.Thuoc = item.Thuoc;
                if(DungChung.Bien.MaBV == "27023")
                    moi.CongI = item.KhamBenh + item.SieuAm + item.XQ + item.XetNghiem + item.DienTim + item.ThuThuat + item.Thuoc;
                else
                    moi.CongI = item.KhamBenh + item.SieuAm + item.XQ + item.XetNghiem + item.DienTim + item.ThuThuat;
                _lVPhi.Add(moi);
            }

            var qbnNTru = (from a in qbn.Where(p => p.NoiTru == 1)
                           join b in qvp.Where(p => p.NgayDuyet >= tungay && p.NgayDuyet <= denngay) on a.MaBNhan equals b.MaBNhan
                           join d in qtamung on a.MaBNhan equals d.MaBNhan into q
                           from q1 in q.DefaultIfEmpty()
                           //join c in qdv on b.MaDV equals c.MaDV
                           group new { a, b, q1 } by new { b.NgayDuyet, a.MaBNhan, ThuThem = q1.ThuThem, TraLai = q1.TraLai } into kq
                           select new
                           {
                               kq.Key.MaBNhan,
                               Ngay = kq.Key.NgayDuyet.Value.Date,
                               kq.Key.ThuThem ,
                               kq.Key.TraLai
                           }).ToList();

            var tu1 = (from a in qbn.Where(p => p.NoiTru == 1)
                       join d in qtamung.Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay) on a.MaBNhan equals d.MaBNhan
                       group new { a, d } by new { d.NgayThu, a.MaBNhan, d.TienUng } into kq
                       select new
                       {
                           kq.Key.MaBNhan,
                           Ngay = kq.Key.NgayThu.Value.Date,
                           TienUng = kq.Key.TienUng
                       }).ToList();


            foreach (var item in tu1)
            {
                moi = new VPhi();
                moi.Ngay = item.Ngay;
                moi.TienUng = item.TienUng;
                _lVPhi.Add(moi);
            }

            foreach (var item in qbnNTru)
            {
                moi = new VPhi();
                moi.Ngay = item.Ngay;
                moi.ThuThem = item.ThuThem;
                moi.TraLai = item.TraLai;
                _lVPhi.Add(moi);
            }

            var qbnRaVien = (from a in qbn.Where(p => p.NoiTru == 1)
                             join b in qvp.Where(p => p.NgayDuyet >= tungay && p.NgayDuyet <= denngay) on a.MaBNhan equals b.MaBNhan
                             join c in qdv on b.MaDV equals c.MaDV
                             join d in data.RaViens on b.MaBNhan equals d.MaBNhan
                             group new { a, b, c, d } by new { b.NgayDuyet } into kq
                             select new
                             {
                                 Ngay = kq.Key.NgayDuyet.Value.Date,
                                 TTRaVien = kq.Sum(p => p.b.TienBN)
                             }).ToList();

            foreach (var item in qbnRaVien)
            {
                moi = new VPhi();
                moi.Ngay = item.Ngay;
                moi.TTRaVien = item.TTRaVien;
                _lVPhi.Add(moi);
            }

            var query = (from n in _lVPhi
                         group n by new { n.Ngay } into kq
                         select new
                         {
                             KhamBenh = kq.Sum(p => p.KhamBenh),
                             SieuAm = kq.Sum(p => p.SieuAm),
                             XQ = kq.Sum(p => p.XQ),
                             XQ_CT = kq.Sum(p => p.XQ_CT),
                             XetNghiem = kq.Sum(p => p.XetNghiem),
                             DienTim = kq.Sum(p => p.DienTim),
                             DoCNHH = kq.Sum(p => p.DoCNHH),
                             ThuThuat = kq.Sum(p => p.ThuThuat),
                             Thuoc = kq.Sum(p => p.Thuoc),
                             CongI = kq.Sum(p => p.CongI),
                             TienUng = kq.Sum(p => p.TienUng),
                             ThuThem = kq.Sum(p => p.ThuThem),
                             TraLai = kq.Sum(p => p.TraLai),
                             CongII = kq.Sum(p => p.TienUng) + kq.Sum(p => p.ThuThem) - kq.Sum(p => p.TraLai),
                             TongCong = kq.Sum(p => p.CongI) + (kq.Sum(p => p.TienUng) + kq.Sum(p => p.ThuThem) - kq.Sum(p => p.TraLai)),
                             TTRaVien = kq.Sum(p => p.TTRaVien),
                             NTruTruThuThem = kq.Sum(p => p.TTRaVien) - kq.Sum(p => p.ThuThem)
                         }).ToList();

            if (query.Count > 0)
            {
                BaoCao.Rep_BC_THThuVP_27023 rep = new BaoCao.Rep_BC_THThuVP_27023();
                frmIn frm = new frmIn();
                rep.celNgay.Text = "Ngày " + tungay.Day + " tháng " + tungay.Month + " năm " + tungay.Year;
                rep.DataSource = query.ToList();
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
                MessageBox.Show("Không có dữ liệu", "THÔNG BÁO");
        }

        #region class VPhi
        private class VPhi
        {
            public DateTime? Ngay { get; set; }
            public double? KhamBenh { get; set; }
            public double? SieuAm { get; set; }
            public double? XQ { get; set; }
            public double? XetNghiem { get; set; }
            public double? DienTim { get; set; }
            public double? ThuThuat { get; set; }
            public double? Thuoc { get; set; }
            public double? CongI { get; set; }
            public double? TienUng { get; set; }
            public double? ThuThem { get; set; }
            public double? TraLai { get; set; }
            public double? CongII { get; set; }
            public double? TongCong { get; set; }
            public double? TTRaVien { get; set; }

            public double XQ_CT { get; set; }

            public double DoCNHH { get; set; }
        }
        #endregion
    }
}