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
    public partial class frm_BaoCaoNopTienQuy_01071_new : DevExpress.XtraEditors.XtraForm
    {
        public frm_BaoCaoNopTienQuy_01071_new()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_BaoCaoNopTienQuy_01071_new_Load(object sender, EventArgs e)
        {
            dttungay.EditValue = DungChung.Ham.NgayTu(System.DateTime.Now);
            dtdenngay.EditValue = DungChung.Ham.NgayDen(System.DateTime.Now);
            gdoituong.Properties.DataSource = DSDTBN().ToList();
            gnhanvienbc.Properties.DataSource = DSCBT().ToList();

        }
        class DSTBN
        {
            public int ID { get; set; }
            public string TenDoiTuong { get; set; }
        }
        List<DSTBN> DSDTBN()
        {
            List<DSTBN> Dt = new List<DSTBN>();
            DSTBN d = new DSTBN();
            d.ID = 0;
            d.TenDoiTuong = "Tất cả";
            Dt.Add(d);
            var q1 = (from a in data.DTBNs.Where(p => p.Status == 1)
                      select new
                                  {
                                      ID = a.IDDTBN,
                                      TenDoiTuong = a.DTBN1,
                                  }).ToList();

            foreach (var item in q1)
            {
                DSTBN q = new DSTBN();
                q.ID = item.ID;
                q.TenDoiTuong = item.TenDoiTuong;
                Dt.Add(q);
            }
            return Dt;
        }
        class CBTaoBc
        {
            public string Macb { get; set; }
            public string TenCB { get; set; }
        }
        List<CBTaoBc> DSCBT()
        {
            List<CBTaoBc> cb = new List<CBTaoBc>();
            CBTaoBc n = new CBTaoBc();
            n.Macb = "";
            n.TenCB = "Tất cả";
            cb.Add(n);
            var q1 = (from cbs in data.CanBoes
                      join kp in data.KPhongs on cbs.MaKP equals kp.MaKP
                      where (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.KeToan)
                      select new
                      {
                          Macb = cbs.MaCB,
                          TenCB = cbs.TenCB,
                      }).Distinct().OrderBy(p => p.TenCB).ToList();
            foreach (var item in q1)
            {
                CBTaoBc q = new CBTaoBc();
                q.Macb = item.Macb;
                q.TenCB = item.TenCB;
                cb.Add(q);
            }

            return cb;
        }
        public class VPHangNgay
        {
            public string TenBNhan { set; get; }
            public int Tuoi { set; get; }
            public string DChi { set; get; }
            public int MaBNhan { set; get; }
            public string DTuong { set; get; }
            public int TrongDM { set; get; }
            public DateTime NgayTT { set; get; }
            public double Giuong { set; get; }
            public double XN { set; get; }
            public double TDCN { set; get; }
            public double CDHA { set; get; }
            public double SieuAm { set; get; }
            public double DienTim { set; get; }
            public double ThuThuatPT { set; get; }
            public double NoiSoi { set; get; }
            public double KSK { set; get; }
            public double Congkham { set; get; }
            public double TienVC { set; get; }
            public double XQ { set; get; }
            public double DienNaoDo { get; set; }
            public double NgoaiTruBH { set; get; }
            public double NoiTruBH { set; get; }
            public double NgoaiTruDV { set; get; }
            public double NoiTruDV { set; get; }
            public double TienBN { set; get; }
            public double TienNGDM { set; get; }
            public double TienBNDV { set; get; }
            public string SoHD { set; get; }
            public int IDTamUng { set; get; }
            public int XuatHD { get; set; }
            public double Tong { set; get; }

            public double DoLoangXuong { get; set; }

            public double LuuHuyetNao { get; set; }

            public double TienVTYT { get; set; }

            public double TienThuoc { get; set; }

            public double ChupCT { get; set; }

            public double DoCNHH { get; set; }

            public double SieuAmDL { get; set; }

            public double TienBH { get; set; }

            public double ThanhTien { get; set; }

            public double TienKTT { get; set; }
        }
        private void btnTaoBaoCao_Click(object sender, EventArgs e)
        {

            List<VPHangNgay> all = new List<VPHangNgay>();
              var qtn = (from tn in data.TieuNhomDVs
                       join n in data.NhomDVs on tn.IDNhom equals n.IDNhom
                       select new { tn.TenRG, tn.IdTieuNhom, n.IDNhom, n.TenNhomCT }).ToList();
              List<int> idNhomXN = new List<int>(); List<int> idThuThuat = new List<int>(); List<int> idtienvc = new List<int>(); List<int> idNhomTDCN = new List<int>(); List<int> idNhomCDHA = new List<int>();
            List<int> idSieuam = new List<int>(), idSieuamDL = new List<int>(), idDientim = new List<int>(), idNoiSoi = new List<int>(), idXQ = new List<int>(), idCongkham = new List<int>(), idKSK = new List<int>(), idChupCT = new List<int>(), idDoCNHH = new List<int>(); //id tiểu nhóm
            List<int> idDoLoangXuong = new List<int>(), idLuuHuyetNao = new List<int>(), idThuoc = new List<int>(), idVTYT = new List<int>(); //id tiểu nhóm
            List<int> idTienGiuong = new List<int>(), idDienNaoDo = new List<int>();

            idNhomXN = qtn.Where(p => p.TenNhomCT == "Xét nghiệm").Select(p => p.IdTieuNhom).ToList();
            idNhomTDCN = qtn.Where(p => p.IDNhom == 3).Where(p => p.TenRG != DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Select(p => p.IdTieuNhom).ToList();
            idNhomCDHA = qtn.Where(p => p.IDNhom == 2).Select(p => p.IdTieuNhom).ToList();
            idThuThuat = qtn.Where(p => p.TenNhomCT == "Thủ thuật, phẫu thuật").Select(p => p.IdTieuNhom).ToList();
            idtienvc = qtn.Where(p => p.TenNhomCT == "Vận chuyển").Select(p => p.IdTieuNhom).ToList();
            // idCongkham = qtn.Where(p => p.TenNhomCT == "Khám bệnh").Where(p => p.TenRG != "KSK").Select(p => p.IdTieuNhom).ToList();
            idCongkham = qtn.Where(p => p.TenNhomCT == "Khám bệnh").Select(p => p.IdTieuNhom).ToList();
            //  idKSK = qtn.Where(p => p.TenRG == "KSK").Select(p => p.IdTieuNhom).ToList();
            idSieuam = qtn.Where(p => p.TenRG == "Siêu âm").Select(p => p.IdTieuNhom).ToList();
            idSieuamDL = qtn.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Select(p => p.IdTieuNhom).ToList();
            idDientim = qtn.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Select(p => p.IdTieuNhom).ToList();
            idDienNaoDo = qtn.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKDienNaoDo).Select(p => p.IdTieuNhom).ToList();
            idNoiSoi = qtn.Where(p => p.TenRG.Contains("Nội soi")).Select(p => p.IdTieuNhom).ToList();
            idChupCT = qtn.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Select(p => p.IdTieuNhom).ToList();
            idDoCNHH = qtn.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Select(p => p.IdTieuNhom).ToList();
            idXQ = qtn.Where(p => p.TenRG == "X-Quang").Select(p => p.IdTieuNhom).ToList();
            idDoLoangXuong = qtn.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Select(p => p.IdTieuNhom).ToList();
            idLuuHuyetNao = qtn.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.LuuHuyetNao).Select(p => p.IdTieuNhom).ToList();
            idThuoc = qtn.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6).Select(p => p.IdTieuNhom).ToList();
            idVTYT = qtn.Where(p => p.IDNhom == 10 || p.IDNhom == 11).Select(p => p.IdTieuNhom).ToList();
            idTienGiuong = qtn.Where(p => p.IDNhom == 14 || p.IDNhom == 15).Select(p => p.IdTieuNhom).ToList();


            int dtbn = Convert.ToInt32(gdoituong.EditValue);
            string macbthu = Convert.ToString(gnhanvienbc.EditValue);
             var qdtbn = data.DTBNs.Where(p =>( dtbn == 0 || dtbn == null) ? true : p.IDDTBN == dtbn).ToList();
            var q0 = (from bn in data.BenhNhans.Where(p => p.TuyenDuoi == 0).Where(p=>(dtbn == 0 || dtbn == null)? true : p.IDDTBN ==  dtbn)
                      join rv in data.RaViens
                      on bn.MaBNhan equals rv.MaBNhan into kq
                      from kq1 in kq.DefaultIfEmpty()
                      select new { bn.DTuong, bn.MaBNhan, bn.TenBNhan, bn.Tuoi, bn.DChi, bn.NoiTru, kq1, bn.DTNT, bn.IDDTBN }).ToList();
            var q1 = (from bn in q0
                      join dt in DSDTBN() on bn.IDDTBN equals dt.ID
                      select new { bn.DTuong, bn.MaBNhan, bn.TenBNhan, bn.Tuoi, bn.DChi, bn.NoiTru, dt.TenDoiTuong, bn.kq1, bn.DTNT }).ToList();
              var qdv = (from dv in data.DichVus
                       join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                       join n in data.NhomDVs on tn.IDNhom equals n.IDNhom
                       select new
                       {
                           dv.MaDV,
                           tn.IdTieuNhom,
                           tn.TenRG,//tên tiểu nhóm
                           n.IDNhom,
                           n.TenNhomCT
                       }).ToList();

                List<VPHangNgay> q6 = new List<VPHangNgay>();
                    //chi phí thu khi thanh toán//.Where(p => XuatHD == 2 ? true : (XuatHD == 0 ? (p.FkeyVNPT == null && p.MaHD == null) : (p.FkeyVNPT != null || p.MaHD != null)))
                    var q2 = (from vp in data.VienPhis.Where( p=> p.NgayDuyet >= dttungay.DateTime  && p.NgayDuyet <= dtdenngay.DateTime)
                              join vpct in data.VienPhicts.Where(p => p.ThanhToan == 0) on vp.idVPhi equals vpct.idVPhi
                              join tu in data.TamUngs.Where(p => p.PhanLoai == 1 && (macbthu == "" ? true : p.MaCB == macbthu)) on vp.MaBNhan equals tu.MaBNhan
                              select new
                              {
                                  tu.IDTamUng,
                                  tu.SoHD,
                                  vp.MaBNhan,
                                  vp.NgayTT,
                                  vp.NgoaiGio,
                                  vpct.Mien,
                                  vpct.MaKP,
                                  vpct.TrongBH,
                                  vpct.MaDV,
                                  vpct.DonGia,
                                  vpct.SoLuong,
                                  vpct.ThanhTien,
                                  vpct.ThanhToan,
                                  vpct.TienBH,
                                  TienBN = vpct.TienBN,
                                  TienNGDM = vpct.TrongBH == 1 ? 0 : vpct.TienBN,
                                  vp.NgayDuyet
                              }).ToList();
                    var q3 = (from bn in q1.Where(p => p.kq1 != null)
                              join vp in q2 on bn.MaBNhan equals vp.MaBNhan
                              join dv in qdv on vp.MaDV equals dv.MaDV
                              select new
                              {
                                  bn.MaBNhan,
                                  bn.TenBNhan,
                                  bn.Tuoi,
                                  bn.DChi,
                                  bn.NoiTru,
                                  bn.DTNT,
                                  vp.IDTamUng,
                                  vp.SoHD,
                                  DTBN1 = bn.TenDoiTuong.ToUpper().Trim(),
                                  vp.TrongBH,
                                  vp.MaDV,
                                  vp.DonGia,
                                  vp.SoLuong,
                                  vp.ThanhTien,
                                  vp.ThanhToan,
                                  vp.TienBH,
                                  dv.IdTieuNhom,
                                  vp.TienBN,
                                  dv.TenRG,
                                  dv.IDNhom,
                                  dv.TenNhomCT,
                                  NgayTT = vp.NgayDuyet.Value.Date,
                              }).ToList();
                    var q4 = (from bn in q3
                              group bn by new { bn.TenBNhan, bn.Tuoi, bn.DChi, bn.MaBNhan, bn.NoiTru, bn.DTNT, bn.NgayTT, bn.DTBN1, bn.IDTamUng, bn.SoHD } into kq
                              select new
                              {
                                  kq.Key.TenBNhan,
                                  kq.Key.Tuoi,
                                  kq.Key.DChi,
                                  kq.Key.MaBNhan,
                                  kq.Key.NgayTT,
                                  kq.Key.DTBN1,
                                  kq.Key.IDTamUng,
                                  kq.Key.SoHD,
                                  #region bv 27183
                                  Giuong = (kq.Where(p => idTienGiuong.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)),
                                  XN = (kq.Where(p => idNhomXN.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)),
                                  TDCN = (kq.Where(p => idNhomTDCN.Contains(p.IdTieuNhom)).Where(p => !idDientim.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)),
                                  SieuAm = (kq.Where(p => idSieuam.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)),
                                  SieuAmDL = (kq.Where(p => idSieuamDL.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)),
                                  DienTim = (kq.Where(p => idDientim.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)),
                                  ChupCT = (kq.Where(p => idChupCT.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)),
                                  DoCNHH = (kq.Where(p => idDoCNHH.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)),
                                  ThuThuatPT = (kq.Where(p => idThuThuat.Contains(p.IdTieuNhom)).Where(p => !idNoiSoi.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)),
                                  NoiSoi = (kq.Where(p => idNoiSoi.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)),
                                  Congkham = (kq.Where(p => idCongkham.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)),
                                  TienVC = (kq.Where(p => idtienvc.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)),
                                  XQ = (kq.Where(p => idXQ.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)),
                                  DoLoangXuong = (kq.Where(p => idDoLoangXuong.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)),
                                  LuuHuyetNao = (kq.Where(p => idLuuHuyetNao.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)),
                                    TienBN = 0,
                                  #endregion
                                  TienNGDM = kq.Sum(p => p.TienBN),
                                  NgoaiTruBH = kq.Where(p => p.DTBN1 == "BHYT").Where(p => p.TrongBH == 1).Sum(p => p.TienBN),// -( DungChung.Bien.MaBV == "30005" ? (((ngoaitru && kq.Key.NoiTru == 0 &&kq.Key.DTNT == false) || (DTNT && kq.Key.NoiTru == 0 && kq.Key.DTNT == true) || (noitru && kq.Key.NoiTru == 1)) ? qtamungBNDV.Where(p => p.MaBNhan == kq.Key.MaBNhan).Sum(p => p.SoTien ?? 0) : 0) : 0), // phải trừ tiền đã tạm ứng
                                  TienThuoc = kq.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7).Sum(p => p.TienBN),
                                  TienVTYT = kq.Where(p => p.IDNhom == 10 || p.IDNhom == 11).Sum(p => p.TienBN),
                                  TamUng = 0
                              }).ToList();

                    List<VPHangNgay> q5 = (from bn in q4
                                           group bn by new { bn.TenBNhan, bn.Tuoi, bn.DChi, bn.MaBNhan, bn.NgayTT, bn.DTBN1, bn.IDTamUng, bn.SoHD } into kq
                                           select new VPHangNgay
                                           {
                                               TenBNhan = kq.Key.TenBNhan,
                                               Tuoi = kq.Key.Tuoi ?? 0,
                                               DChi = kq.Key.DChi,
                                               DTuong = kq.Key.DTBN1,
                                               MaBNhan = kq.Key.MaBNhan,
                                               NgayTT = kq.Key.NgayTT,
                                               IDTamUng = kq.Key.IDTamUng,
                                               SoHD = kq.Key.SoHD,
                                               NgoaiTruBH = kq.Sum(p => p.NgoaiTruBH),// thu thêm bệnh nhân mới tính (bvì là bc thu vp)
                                               TienThuoc = kq.Sum(p => p.TienThuoc),
                                               TienVTYT = kq.Sum(p => p.TienVTYT),
                                               TienBN = kq.Sum(p => p.TienBN),
                                               TienNGDM = kq.Sum(p => p.TienNGDM),
                                               Giuong = kq.Sum(p => p.Giuong),
                                               XN = kq.Sum(p => p.XN),
                                               TDCN = kq.Sum(p => p.TDCN),
                                               SieuAm = kq.Sum(p => p.SieuAm),
                                               SieuAmDL = kq.Sum(p => p.SieuAmDL),
                                               ChupCT = kq.Sum(p => p.ChupCT),
                                               DoCNHH = kq.Sum(p => p.DoCNHH),
                                               DienTim = kq.Sum(p => p.DienTim),
                                               ThuThuatPT = kq.Sum(p => p.ThuThuatPT),
                                               NoiSoi = kq.Sum(p => p.NoiSoi),
                                               Congkham = kq.Key.DTBN1 == "KSK" ? 0 : kq.Sum(p => p.Congkham),
                                               KSK = kq.Key.DTBN1 == "KSK" ? kq.Sum(p => p.Congkham) : 0,
                                               TienVC = kq.Sum(p => p.TienVC),
                                               XQ = kq.Sum(p => p.XQ),
                                               DoLoangXuong = kq.Sum(p => p.DoLoangXuong),
                                               LuuHuyetNao = kq.Sum(p => p.LuuHuyetNao),
                                               Tong = kq.Sum(p => p.TienNGDM) + kq.Sum(p => p.NgoaiTruBH)
                                           }).OrderBy(p => p.NgayTT).ToList();
                    q6.AddRange(q5);
                    //List<int> _lMabn = q5.Select(p => p.MaBNhan).ToList();//.Where(p => _lMabn.Contains(p.MaBNhan))
                    //Chi phí thu trực tiếp
                    //join dt in qdtbn on bn.IDDTBN equals dt.IDDTBN
                    var qTamung0 = (from bn in data.BenhNhans.Where(p => p.TuyenDuoi == 0).Where(p => dtbn == 0 || p.IDDTBN == dtbn)
                                    join tu in data.TamUngs.Where(p => p.NgayThu >= dttungay.DateTime && p.NgayThu <= dtdenngay.DateTime).Where(p => p.PhanLoai == 3).Where(p => (macbthu == "" || p.MaCB == macbthu)? true : p.MaCB == macbthu  ) on bn.MaBNhan equals tu.MaBNhan
                                    join tuct in data.TamUngcts on tu.IDTamUng equals tuct.IDTamUng
                                    select new { bn.MaBNhan, bn.IDDTBN, tu.IDTamUng, tu.SoHD, bn.TenBNhan, bn.Tuoi, bn.DChi, bn.NoiTru, bn.DTNT, tu.PhanLoai, tu.NgayThu, tuct.MaDV, tuct.Mien, tu.MaKP, tuct.SoLuong, tuct.TienBN, tuct.TrongBH, tuct.ThanhTien, tuct.DonGia, tuct.SoTien }).ToList();

                    var qTamung1 = (from bn in qTamung0
                                    join dt in qdtbn on bn.IDDTBN equals dt.IDDTBN
                                    select new { bn.MaBNhan, bn.IDTamUng, bn.SoHD, bn.TenBNhan, bn.Tuoi, bn.DChi, bn.NoiTru, bn.DTNT, dt.DTBN1, bn.PhanLoai, bn.NgayThu, bn.MaDV, bn.Mien, bn.MaKP, bn.SoLuong, bn.TienBN, bn.TrongBH, bn.ThanhTien, bn.DonGia, bn.SoTien }).ToList();
                    var qTamung2 = (from bn in qTamung1
                                    join dv in qdv on bn.MaDV equals dv.MaDV
                                    select new
                                    {
                                        bn.MaBNhan,
                                        bn.TenBNhan,
                                        bn.Tuoi,
                                        bn.DChi,
                                        bn.NoiTru,
                                        bn.DTNT,
                                        bn.IDTamUng,
                                        bn.SoHD,
                                        DTBN1 = bn.DTBN1.ToUpper().Trim(),
                                        bn.MaDV,
                                        bn.DonGia,
                                        bn.SoLuong,
                                        bn.ThanhTien,
                                        bn.SoTien,
                                        bn.TrongBH,
                                        TienBN = bn.SoTien,//hiển thị số tiền bệnh nhân thực trả
                                        dv.IdTieuNhom,
                                        dv.TenRG,
                                        dv.IDNhom,
                                        dv.TenNhomCT,
                                        NgayTT = bn.NgayThu.Value.Date
                                    }).ToList();

                    var qTamung3 = (from bn in qTamung2
                                    group bn by new { bn.TenBNhan, bn.Tuoi, bn.DChi, bn.MaBNhan, bn.NoiTru, bn.NgayTT, bn.DTBN1, bn.DTNT, bn.IDTamUng, bn.SoHD } into kq
                                    select new
                                    {
                                        kq.Key.TenBNhan,
                                        kq.Key.Tuoi,
                                        kq.Key.DChi,
                                        kq.Key.MaBNhan,
                                        kq.Key.NgayTT,
                                        kq.Key.DTBN1,
                                        kq.Key.SoHD,
                                        kq.Key.IDTamUng,
                                        #region mẫu chi tiết 30005
                                        CDHA = kq.Where(p => idNhomCDHA.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),                   
                                        #endregion
                                        Giuong = kq.Where(p => idTienGiuong.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        XN = kq.Where(p => idNhomXN.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        TDCN = kq.Where(p => idNhomTDCN.Contains(p.IdTieuNhom)).Where(p => !idDientim.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        SieuAm = kq.Where(p => idSieuam.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        SieuAmDL = kq.Where(p => idSieuamDL.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        DienTim = kq.Where(p => idDientim.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        ChupCT = kq.Where(p => idChupCT.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        DoCNHH = kq.Where(p => idDoCNHH.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        ThuThuatPT = kq.Where(p => idThuThuat.Contains(p.IdTieuNhom)).Where(p => !idNoiSoi.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        NoiSoi = kq.Where(p => idNoiSoi.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        Congkham =  (kq.Key.DTBN1 == "KSK") ? 0 : kq.Where(p => idCongkham.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        KSK = ( kq.Key.DTBN1 == "KSK") ? kq.Where(p => idCongkham.Contains(p.IdTieuNhom)).Sum(p => p.TienBN) : 0,
                                        TienVC = kq.Where(p => idtienvc.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        XQ = kq.Where(p => idXQ.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        DoLoangXuong = kq.Where(p => idDoLoangXuong.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        LuuHuyetNao = kq.Where(p => idLuuHuyetNao.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        TienBN = kq.Sum(p => p.TienBN)
                                    }).OrderBy(p => p.NgayTT).ToList();
                    List<VPHangNgay> qTamung4 = (from bn in qTamung3
                                                 group bn by new { bn.TenBNhan, bn.Tuoi, bn.DChi, bn.MaBNhan, bn.NgayTT, bn.DTBN1, bn.IDTamUng, bn.SoHD } into kq
                                                 select new VPHangNgay
                                                 {
                                                     TenBNhan = kq.Key.TenBNhan,
                                                     Tuoi = kq.Key.Tuoi ?? 0,
                                                     DChi = kq.Key.DChi,
                                                     MaBNhan = kq.Key.MaBNhan,
                                                     DTuong = kq.Key.DTBN1,
                                                     NgayTT = kq.Key.NgayTT,
                                                     XN = kq.Sum(p => p.XN),
                                                     TDCN = kq.Sum(p => p.TDCN),
                                                     IDTamUng = kq.Key.IDTamUng,
                                                     SoHD = kq.Key.SoHD,
                                              
                                                     CDHA = kq.Sum(p => p.CDHA),
                                                  
                                                     SieuAm = kq.Sum(p => p.SieuAm),
                                                     SieuAmDL = kq.Sum(p => p.SieuAmDL),
                                                     ChupCT = kq.Sum(p => p.ChupCT),
                                                     DoCNHH = kq.Sum(p => p.DoCNHH),
                                                     DienTim = kq.Sum(p => p.DienTim),
                                                     ThuThuatPT = kq.Sum(p => p.ThuThuatPT),
                                                     NoiSoi = kq.Sum(p => p.NoiSoi),
                                                     Congkham = kq.Sum(p => p.Congkham),
                                                     KSK = kq.Sum(p => p.KSK),
                                                     TienVC = kq.Sum(p => p.TienVC),
                                                     XQ = kq.Sum(p => p.XQ),
                                                     Giuong = kq.Sum(p => p.Giuong),
                                                     DoLoangXuong = kq.Sum(p => p.DoLoangXuong),
                                                     LuuHuyetNao = kq.Sum(p => p.LuuHuyetNao),
                                                     NgoaiTruBH = 0,
                                                     TienNGDM = 0,
                                                     TienBN = kq.Sum(p => p.TienBN),
                                                     Tong = kq.Sum(p => p.TienBN)
                                                 }).OrderBy(p => p.NgayTT).ToList();

                    q6.AddRange(qTamung4);

                    var qTamungksk = (from bn in data.BenhNhans.Where(p => p.DTuong == "KSK")//.Where(p => p.TuyenDuoi == 0).Where(p => dtbn == 100 || p.IDDTBN == dtbn).Where(p => (_ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (_DTNT && p.NoiTru == 0 && p.DTNT == true) || (_noitru && p.NoiTru == 1))
                                      join tu in data.TamUngs.Where(p => p.NgayThu >= dttungay.DateTime&& p.NgayThu <= dtdenngay.DateTime).Where(p => p.PhanLoai == 1).Where(p => (macbthu == "") ? true: p.MaCB == macbthu) on bn.MaBNhan equals tu.MaBNhan
                                      select new { bn.MaBNhan, bn.IDDTBN, tu.SoTien, tu.IDTamUng, tu.SoHD, bn.TenBNhan, bn.Tuoi, bn.DChi, bn.NoiTru, bn.DTNT, tu.PhanLoai, tu.NgayThu, tu.MaKP, bn.DTuong }).ToList();
                    List<VPHangNgay> qksk = (from bn in qTamungksk
                                             group bn by new { bn.TenBNhan, bn.Tuoi, bn.DChi, bn.MaBNhan, bn.NgayThu, bn.DTuong, bn.IDTamUng, bn.SoHD } into kq
                                             select new VPHangNgay
                                             {
                                                 TenBNhan = kq.Key.TenBNhan,
                                                 Tuoi = kq.Key.Tuoi ?? 0,
                                                 DChi = kq.Key.DChi,
                                                 DTuong = kq.Key.DTuong,
                                                 MaBNhan = kq.Key.MaBNhan,
                                                 NgayTT = kq.Key.NgayThu.Value.Date,
                                                 IDTamUng = kq.Key.IDTamUng,
                                                 SoHD = kq.Key.SoHD,
                                                 KSK = kq.Sum(p => p.SoTien ?? 0),
                                                 Tong = kq.Sum(p => p.SoTien ?? 0)
                                             }).OrderBy(p => p.NgayTT).ToList();
                    q6.AddRange(qksk);
                    all.AddRange(q6);

                    all = (from a in all
                           group a by new { a.TenBNhan, a.MaBNhan, a.DChi, a.Tuoi, a.NgayTT, a.DTuong, a.IDTamUng, a.SoHD } into kq
                           select new VPHangNgay
                           {
                               TenBNhan = kq.Key.TenBNhan,
                               IDTamUng = kq.Key.IDTamUng,
                               SoHD = kq.Key.SoHD,
                               Tuoi = kq.Key.Tuoi,
                               DChi = kq.Key.DChi,
                               MaBNhan = kq.Key.MaBNhan,
                               DTuong = kq.Key.DTuong,
                               NgayTT = kq.Key.NgayTT,
                               Giuong = kq.Sum(p => p.Giuong),
                               XN = kq.Sum(p => p.XN),
                               TDCN = kq.Sum(p => p.TDCN),
                               SieuAm = kq.Sum(p => p.SieuAm),
                               SieuAmDL = kq.Sum(p => p.SieuAmDL),
                               DienTim = kq.Sum(p => p.DienTim),
                               ChupCT = kq.Sum(p => p.ChupCT),
                               DoCNHH = kq.Sum(p => p.DoCNHH),
                               ThuThuatPT = kq.Sum(p => p.ThuThuatPT),
                               NoiSoi = kq.Sum(p => p.NoiSoi),
                               Congkham = kq.Sum(p => p.Congkham),
                               TienVC = kq.Sum(p => p.TienVC),
                               XQ = kq.Sum(p => p.XQ),
                               NgoaiTruBH = kq.Sum(p => p.NgoaiTruBH),
                               KSK = kq.Sum(p => p.KSK),
                               DoLoangXuong = kq.Sum(p => p.DoLoangXuong),
                               LuuHuyetNao = kq.Sum(p => p.LuuHuyetNao),
                               TienThuoc = kq.Sum(p => p.TienThuoc),
                               TienVTYT = kq.Sum(p => p.TienVTYT),
                               TienBN = kq.Sum(p => p.TienBN),
                               TienNGDM = kq.Sum(p => p.TienNGDM),
                               Tong = kq.Sum(p => p.Tong) // kq.Sum(p => p.XN + p.TDCN + p.SieuAm + p.DienTim + p.ThuThuatPT + p.NoiSoi + p.Congkham + p.TienVC + p.XQ + p.NgoaiTruBH + p.KSK + p.DoLoangXuong + p.LuuHuyetNao + p.TienThuoc + p.TienVTYT)
                           }).Where(p => p.Tong != 0).OrderBy(p => p.SoHD).ThenBy(p => p.NgayTT).ToList();
                    BaoCao.rep_VienPhiHangNgay_30003_moi rep = new BaoCao.rep_VienPhiHangNgay_30003_moi();
                    frmIn frm = new frmIn();
                    rep.celTitBH.Text = "% BHYT";
                    rep.DataSource = all;
                    rep.colCQ.Text = DungChung.Bien.TenCQ.ToUpper();
                    rep.cel_diadanh.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                    rep.celNgayThang.Text = "Từ ngày " + dttungay.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + dtdenngay.DateTime.ToString("dd/MM/yyyy");
                  
                        rep.CelTieuDe.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN VIỆN PHÍ HẰNG NGÀY";

                        rep.celNgayThang.Text = dttungay.DateTime.ToString();
                    rep.cel_NguoiLap.Text = DungChung.Bien.NguoiLapBieu;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
        }

    }
}