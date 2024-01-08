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
    public partial class frm_BCSoLieuBvHangNgay_08130 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCSoLieuBvHangNgay_08130()
        {   
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void btnInBC_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            List<BC> _listAll = new List<BC>();
            List<BC> _listKB = new List<BC>();

            var kph = (from kp in data.KPhongs
                       join ck in data.ChuyenKhoas on kp.MaCK equals ck.MaCK
                       where kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham
                       select new { kp.MaKP,kp.TenKP, ck.MaCK, ck.TenCK }).ToList();

            var qkb1 = (from bn in data.BenhNhans
                        join kb in data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay) on bn.MaBNhan equals kb.MaBNhan into kq
                        from kq1 in kq.DefaultIfEmpty()                
                       select new
                       {
                           bn.NoiTru,
                           bn.MaBNhan,
                           bn.DTNT,
                           bn.NNhap,
                           NgayKham = kq1 == null ? null : kq1.NgayKham,
                           MaKP = kq1 == null ? 0 :   kq1.MaKP,
                           MaCK = kq1 == null ? 0 : kq1.MaCK
                       }).ToList();

            //bệnh nhân khám bệnh
            var qkb = (from kb in qkb1.Where(p => p.NgayKham!= null)                      
                       join kp in data.ChuyenKhoas on kb.MaCK equals kp.MaCK into kq from kq1 in kq.DefaultIfEmpty()
                       join kp in data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham) on kb.MaKP equals kp.MaKP //into kq2 from kq3 in kq2.DefaultIfEmpty()
                       select new
                       {
                           kb.NoiTru,
                           kb.MaBNhan,
                           kb.NgayKham,
                           kb.NNhap,                          
                           kb.MaKP, 
                           kb.DTNT,
                           TenKP = kp.TenKP, 
                           TenCK = kq1 == null ? "" : kq1.TenCK,
                           MaCK = kq1 == null ? -1 : kq1.MaCK,
                       }).ToList();

            //lấy những bệnh nhân có cấp thuốc hoặc không
            var qxuatduoc = (from dt in data.NhapDs.Where(p => p.PLoai == 2 && p.KieuDon == 0).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                             select new { dt.MaBNhan, NgayNhap = dt.NgayNhap.Value }).ToList();

            //Lấy bệnh nhân thực hiện thủ thuật, phẫu thuật tại khoa
            var qdv = (from dv in data.DichVus
                       join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                       select new { dv.MaDV, tn.TenRG, TenDVRG = dv.TenRG.ToLower(), dv.TenDV }).ToList();

            var qdt = (from bn in qkb
                       join dt in data.DThuocs on new { MaBNhan = bn.MaBNhan, MaKP = bn.MaKP } equals new { MaBNhan = dt.MaBNhan??0, MaKP = dt.MaKP }
                       join dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay) on dt.IDDon equals dtct.IDDon
                       select new { MaBNhan = dt.MaBNhan ?? 0, dtct.SoLuong, bn.MaCK, bn.TenCK, NgayKe = dtct.NgayNhap.Value, dtct.MaDV }).ToList();
           
            var qdthuoc = (from dt in qdt
                           join dv in qdv.Where(p => p.TenRG == "Thủ thuật" || p.TenRG == "Phẫu Thuật") on dt.MaDV equals dv.MaDV
                           select new { dt.MaBNhan, dt.MaCK, dt.TenCK, NgayKe = dt.NgayKe.Date, dt.SoLuong }).ToList();
           
            // lấy thông tin chung và các bệnh nhân vào khám tại các khoa phòng
            List<BC> lbc = new List<BC>();
            foreach (var kb in qkb)
            {
                BC moi = new BC();
                moi.MaBNhan = kb.MaBNhan;
             
                moi.NoiTru = kb.NoiTru ?? 0;
                moi.DTNT = kb.DTNT;
                moi.MaKP = kb.MaKP??0;
                moi.MaCK = kb.MaCK;
                moi.TenCK = kb.TenCK;
                moi.col1 = kb.NgayKham.Value.Date;// kb.NgayKham.Value.Date;
                if (kb.NgayKham != null)
                    moi.col3 = 1;
                else
                    moi.col3 = 0;
              //  moi.col3 = (kb.NgayKham != null && kb.NgayKham.Value.Date == kb.NNhap.Value.Date) ? 1 : 0;// tổng số lượt khám
                if (kb.TenCK ==  DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKNoi && !kb.TenKP.ToLower().Contains("cấp cứu"))
                    moi.col7 = 1;
                moi.col9 = (kb.TenCK == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKNhi && !kb.TenKP.ToLower().Contains("cấp cứu")) ? 1 : 0;
                moi.col10 = (kb.TenCK == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKTruyenNhiem && !kb.TenKP.ToLower().Contains("cấp cứu")) ? 1 : 0;
                if (kb.TenKP.ToLower().Contains("cấp cứu"))
                    moi.col11 = 1;

                moi.col12 = (kb.TenCK == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKTaiMuiHong && !kb.TenKP.ToLower().Contains("cấp cứu"))? 1 : 0;
                moi.col14 = (kb.TenCK == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKMat && !kb.TenKP.ToLower().Contains("cấp cứu")) ? 1 : 0;
                moi.col16 = (kb.TenCK == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKRangHamMat && !kb.TenKP.ToLower().Contains("cấp cứu")) ? 1 : 0;
                moi.col18 = (kb.TenCK == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKDongY && !kb.TenKP.ToLower().Contains("cấp cứu")) ? 1 : 0;
                moi.col20 = (kb.TenCK ==  DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKSan && !kb.TenKP.ToLower().Contains("cấp cứu")) ? 1 : 0;
                moi.col22 = (kb.TenCK ==  DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKNgoai && !kb.TenKP.ToLower().Contains("cấp cứu")) ? 1 : 0;

               
                lbc.Add(moi);
            }

            _listKB = (from kb in lbc
                       join xd in qxuatduoc on new { kb.col1, kb.MaBNhan } equals new { col1 = xd.NgayNhap.Date, MaBNhan= xd.MaBNhan??0 } into kq from kq1 in kq.DefaultIfEmpty()
                       select new BC
                       {
                           MaBNhan = kb.MaBNhan,
                           PhuongAn = kb.PhuongAn,
                           NoiTru = kb.NoiTru,
                           MaKP = kb.MaKP,
                           MaCK = kb.MaCK,
                           TenCK = kb.TenCK,
                           col1 = kb.col1,
                           col3 = kb.col3,
                           col7 = kb.col7,
                           col9 = kb.col9,
                           col10 = kb.col10,
                           col11 = kb.col11,
                           col12 = kb.col12,
                           col14 = kb.col14,
                           col16 = kb.col16,
                           col18 = kb.col18,
                           col20 = kb.col20,
                           col22 = kb.col22,
                           col26 = (kb.NoiTru == 0 && kb.DTNT == false) ? 1 : 0,
                           col27 = (kq1 != null && kb.NoiTru == 0 && kb.DTNT == false) ? 1 : 0

                       }).ToList();
            //kiểm tra số bệnh nhân nội trú, ngoại trú, tổng số bệnh nhân
            var tsbn = (from kb in _listKB group kb by new { kb.MaBNhan, kb.NoiTru, kb.col1 } into kq2 select new { kq2.Key.col1, kq2.Key.NoiTru, kq2.Key.MaBNhan }
                ).ToList();
            var tsbn2 = (from bn in tsbn group bn by bn.col1 into kq1 select new { kq1.Key, Noitru = kq1.Where(p => p.NoiTru == 1).Count(), NgoaiTru = kq1.Where(p => p.NoiTru == 0).Count(), TS = kq1.Count() }).ToList();

            var _lcapthuoc = (from a in _listKB group a by new { a.col1, a.MaBNhan } into kq1 select new { kq1.Key.col1, kq1.Key.MaBNhan, col26 = (kq1.Max(p=>p.col27) ==0 && kq1.Max(p=>p.col26 == 1)) ? 1 : 0, col27 = kq1.Max(p=>p.col27)}).ToList();

            #region add các dịch vụ CLS
            var qcls1 = (from dt in data.DThuocs
                       join dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)on dt.IDDon equals dtct.IDDon
                         select new
                         {
                             dt.MaBNhan,
                             NgayTH = dtct.NgayNhap.Value,
                             dtct.MaDV,
                             dtct.MaKP
                         }).ToList();

            var qcls2 = (from dt in qcls1
                         join kp in data.KPhongs on dt.MaKP equals kp.MaKP
                         join dv in qdv.Where(p => p.TenRG != null) on dt.MaDV equals dv.MaDV
                         select new
                         {
                             MaBNhan = dt.MaBNhan.Value,
                             NgayTH = dt.NgayTH.Date,
                             dt.MaDV,
                             dv.TenRG,
                             dv.TenDVRG,
                             dv.TenDV

                         }).ToList();
       
            #endregion
            List<DateTime> _listNgay = _listKB.Select(p => p.col1).Distinct().OrderBy(p => p).ToList();
           
            List<BC> _listBC = new List<BC>();// danh sách sau khi đã nhóm theo ngày
            var dtuong = (from kb in _listKB join bn in data.BenhNhans on kb.MaBNhan equals bn.MaBNhan join dtbn in data.DTBNs on bn.IDDTBN equals dtbn.IDDTBN group new { bn, dtbn } by new { bn.MaBNhan, bn.NoiTru, dtbn.DTBN1, kb.col1 } into kq select new { kq.Key.MaBNhan, kq.Key.DTBN1, kq.Key.col1, kq.Key.NoiTru }).ToList();
            var phuongan = (from pa in _listKB group pa by new { pa.col1, pa.MaBNhan,pa.NoiTru, pa.PhuongAn } into kq1 select new { kq1.Key.col1,kq1.Key.NoiTru, kq1.Key.PhuongAn }).ToList();
            var ravien = (from bn in data.BenhNhans join rv in data.RaViens.Where(p=>p.Status==1) on bn.MaBNhan equals rv.MaBNhan select rv).ToList();// chuyển viện
            var vaovien = (from vv in data.VaoViens join bn in data.BenhNhans on vv.MaBNhan equals bn.MaBNhan select vv).ToList();
            foreach (DateTime dt in _listNgay)
            {
                var kbenh = _listKB.Where(p => p.col1 == dt).ToList();
                var dtuongBN = dtuong.Where(p => p.col1 == dt).ToList();
                var cls = qcls2.Where(p => p.NgayTH == dt).ToList();
                var xquang = cls.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).ToList();
                var qpttt = qdthuoc.Where(p => p.NgayKe == dt).ToList();
                var qphuongan = phuongan.Where(p => p.col1 == dt).ToList();
                var vvien = vaovien.Where(p => p.NgayVao.Value.Date == dt).ToList();
                var rvien = ravien.Where(p => p.NgayRa.Value.Date == dt).ToList();
                BC moi = new BC();
                moi.col1 = dt;
                moi.col2 = kbenh.Select(p => p.MaBNhan).Distinct().Count();// số bệnh nhân khám
                moi.col3 = kbenh.Where(p=>p.col3 == 1).Count();// lượt khám                
                moi.col4 = dtuongBN.Where(p => p.DTBN1 == "KSK").Count();
                moi.col5 = dtuongBN.Where(p => p.DTBN1 == "BHYT").Count();
                moi.col6 = dtuongBN.Where(p => p.DTBN1.ToLower() == "dịch vụ").Count();
                moi.col7 = kbenh.Sum(p => p.col7);
                moi.col9 = kbenh.Sum(p => p.col9);
                moi.col10 = kbenh.Sum(p => p.col10);
                moi.col11 = kbenh.Sum(p => p.col11);
                moi.col12 = kbenh.Sum(p => p.col12);
                moi.col14 = kbenh.Sum(p => p.col14);
                moi.col16 = kbenh.Sum(p => p.col16);
                moi.col18 = kbenh.Sum(p => p.col18);
                moi.col20 = kbenh.Sum(p => p.col20);
                moi.col22 = kbenh.Sum(p => p.col22);

                //thực hiện thủ thuật phẫu thuật tại phòng khám
                moi.col13 = qpttt.Where(p => p.TenCK == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKTaiMuiHong).Sum(p => p.SoLuong);
                moi.col15 = qpttt.Where(p => p.TenCK == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKMat).Sum(p => p.SoLuong);
                moi.col17 = qpttt.Where(p => p.TenCK == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKRangHamMat).Sum(p => p.SoLuong);
                moi.col19 = qpttt.Where(p => p.TenCK == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKDongY).Sum(p => p.SoLuong);
                moi.col21 = qpttt.Where(p => p.TenCK == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKSan).Sum(p => p.SoLuong);
                moi.col23 = qpttt.Where(p => p.TenCK == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKNgoai).Sum(p => p.SoLuong);
                moi.col24 = vvien.Count();
                moi.col25 = rvien.Count();

                //Số lượt cấp thuốc và không cấp thuốc
                moi.col26 = _lcapthuoc.Where(p=>p.col1 == dt).Sum(p => p.col26);
                moi.col27 = _lcapthuoc.Where(p => p.col1 == dt).Sum(p => p.col27);

                //số lần thực hiện cận lâm sàng: xét nghiệm, chẩn đoán hình ảnh
                if (cls != null)
                {
                    moi.col28 = cls.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
                    moi.col29 = cls.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
                    moi.col30 = cls.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDichChocDo).Count();
                    moi.col31 = cls.Where(p => p.TenRG == "XN đông máu").Count();
                    moi.col32 = cls.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Count();
                    moi.col33 = cls.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Count();
                    moi.col34 = cls.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Count();
                    moi.col35 = xquang.Count();
                    moi.col36 = xquang.Where(p => p.TenDV.Contains("(KTS)")).Count();
                    moi.col37 = xquang.Where(p => p.TenDV.StartsWith("Chụp Xquang răng")).Count();
                    moi.col38 = xquang.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Count();
                    moi.col39 = cls.Where(p => p.TenRG == "Nội soi" && p.TenDV.Contains("dạ dày")).Count();
                    moi.col40 = cls.Where(p => p.TenRG == "Nội soi" && p.TenDV.Contains("đại tràng")).Count();
                    moi.col41 = cls.Where(p => p.TenRG == "Điện tim").Count();
                }

                _listBC.Add(moi);
            }

            frmIn frm2 = new frmIn();
            BaoCao.rpt_BCSoLieuBvHangNgay_01830 rep2 = new BaoCao.rpt_BCSoLieuBvHangNgay_01830();
            rep2.lbl_thoigian.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
            rep2.DataSource = _listBC;
            rep2.BindingData();
            rep2.CreateDocument();
            frm2.prcIN.PrintingSystem = rep2.PrintingSystem;
            frm2.ShowDialog();
        }

        private void frm_BCBVHangNgay_08130_Load(object sender, EventArgs e)
        {
            lupTuNgay.DateTime = DateTime.Now;
            lupDenNgay.DateTime = DateTime.Now;
        }
        public class BC
        {
            public int MaKP { set; get; }
            public string TenKP { set; get; }
            public int MaCK { set; get; }
            public string TenCK { set; get; }
            public int MaBNhan { set; get; }
            public int NoiTru { set; get; }
            public int? PhuongAn { set; get; }
            public DateTime col1 { set; get; }
            public int col2 { set; get; }
            public int col3 { set; get; }
            public int col4 { set; get; }
            public int col5 { set; get; }
            public int col6 { set; get; }
            public int col7 { set; get; }
            public int col8 { set; get; }
            public int col9 { set; get; }
            public int col10 { set; get; }
            public int col11 { set; get; }
            public int col12 { set; get; }
            public double col13 { set; get; }
            public int col14 { set; get; }
            public double col15 { set; get; }
            public int col16 { set; get; }
            public double col17 { set; get; }
            public int col18 { set; get; }
            public double col19 { set; get; }
            public int col20 { set; get; }
            public double col21 { set; get; }
            public int col22 { set; get; }
            public double col23 { set; get; }
            public int col24 { set; get; }
            public int col25 { set; get; }
            public int col26 { set; get; }
            public int col27 { set; get; }
            public int col28 { set; get; }
            public int col29 { set; get; }
            public int col30 { set; get; }
            public int col31 { set; get; }
            public int col32 { set; get; }
            public int col33 { set; get; }
            public int col34 { set; get; }
            public int col35 { set; get; }
            public int col36 { set; get; }
            public int col37 { set; get; }
            public int col38 { set; get; }
            public int col39 { set; get; }
            public int col40 { set; get; }
            public int col41 { set; get; }

            public bool DTNT { get; set; }
        }
    }
}