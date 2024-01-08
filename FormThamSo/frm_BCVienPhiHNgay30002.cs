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
    public partial class frm_BCVienPhiHNgay30002 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCVienPhiHNgay30002()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data;
        private void frm_BCVienPhiHNgay30002_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            lupNgaytu.DateTime = DateTime.Now.Date;
            lupngayden.DateTime = DateTime.Now.Date.AddDays(1).AddMilliseconds(-1);

            List<CanBo> _lcb = new List<CanBo>();
            _lcb = (from kp in data.KPhongs.Where(p => p.PLoai.Contains("Kế toán"))
                    join cb in data.CanBoes on kp.MaKP equals cb.MaKP
                    select cb).ToList();
            _lcb.Insert(0, new CanBo { MaCB = "", TenCB = "Tất cả" });
            lupcbthu.Properties.DataSource = _lcb;
            lupcbthu.EditValue = lupcbthu.Properties.GetKeyValueByDisplayText("Tất cả");

            lupNgaytu.Focus();

            List<DTBN> _lDTBN = new List<DTBN>();
            _lDTBN = data.DTBNs.Where(p => p.Status == 1).ToList();
            _lDTBN.Insert(0, new DTBN { IDDTBN = 100, DTBN1 = "Tất cả" });
            lupDoituong.Properties.DataSource = _lDTBN;
            lupDoituong.EditValue = lupDoituong.Properties.GetKeyValueByDisplayText("Tất cả");

            List<KPhong> _lkp = new List<KPhong>();
            _lkp = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).ToList();
            _lkp.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
            lupKhoaphong.Properties.DataSource = _lkp;
            lupKhoaphong.EditValue = lupKhoaphong.Properties.GetKeyValueByDisplayText("Tất cả");

            cklHThi.SetItemChecked(0, true);
            cklNoiNgoaiTru.SetItemChecked(0, true);
        }

        #region class VPHangNgay
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
            public double SieuAmDL { get; set; }
            public double SieuAm { set; get; }
            public double ChupCT { get; set; }
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
            public double TienBNBH { set; get; }
            public double TienNGDM { set; get; }
            public double TienBNDV { set; get; }

            public string SoHD { set; get; }
            public int IDTamUng { set; get; }

            public double Tong { set; get; }

        }
        #endregion

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //đối tượng bệnh nhân
            int dtbn = -1;
            if (lupDoituong.EditValue != null)
                dtbn = Convert.ToInt32(lupDoituong.EditValue);

            //Thời gian
            DateTime tungay = lupNgaytu.DateTime;
            DateTime denngay = lupngayden.DateTime;

            //Trong ngoài giờ hành chính
            int gioHC = rdTrongGioHC.SelectedIndex;

            //khoa phòng thanh toán
            int makp = 0;
            if (lupKhoaphong.EditValue != null)
                makp = Convert.ToInt32(lupKhoaphong.EditValue);
            string macbthu = "";
            if (lupcbthu.EditValue != null)
                macbthu = Convert.ToString(lupcbthu.EditValue);
            //Chi phí trong ngoài danh mục
            int cp = rdTrongBH.SelectedIndex;

            bool _noitru = cklNoiNgoaiTru.GetItemChecked(2);
            bool _DTNT = cklNoiNgoaiTru.GetItemChecked(1);
            bool _ngoaitru = cklNoiNgoaiTru.GetItemChecked(0);
            //%bảo hiểm
            bool noitru = cklHThi.GetItemChecked(2);
            bool DTNT = cklHThi.GetItemChecked(1);
            bool ngoaitru = cklHThi.GetItemChecked(0);

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

            List<VPHangNgay> all = new List<VPHangNgay>();

            var qdtbn = data.DTBNs.Where(p => dtbn == 100 || p.IDDTBN == dtbn).ToList();
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

            #region cp thu thẳng

            var qTamung0 = (from bn in data.BenhNhans.Where(p => p.DTuong != "KSK").Where(p => p.TuyenDuoi == 0).Where(p => dtbn == 100 || p.IDDTBN == dtbn).Where(p => (_ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (_DTNT && p.NoiTru == 0 && p.DTNT == true) || (_noitru && p.NoiTru == 1))
                            join tu in data.TamUngs.Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay).Where(p => p.PhanLoai == 3).Where(p => makp == 0 || p.MaKP == makp).Where(p => macbthu == "" ? true : p.MaCB == macbthu).Where(p => gioHC == 2 || (gioHC == 1 && p.NgoaiGio == 1) || (gioHC == 0 && (p.NgoaiGio == null || p.NgoaiGio != 1))) on bn.MaBNhan equals tu.MaBNhan
                            join tuct in data.TamUngcts.Where(p => cp == 2 ? true : p.TrongBH == cp).Where(p => p.Status == 0) on tu.IDTamUng equals tuct.IDTamUng
                            select new { bn.MaBNhan, bn.IDDTBN, bn.TenBNhan, bn.Tuoi, bn.DChi, bn.NoiTru, bn.DTNT, tu.PhanLoai, tu.NgayThu, tuct.MaDV, tuct.Mien, tu.MaKP, tuct.SoLuong, tuct.TienBN, tuct.TrongBH, tuct.ThanhTien, tuct.DonGia, tuct.SoTien }).ToList();

            var qTamung1 = (from bn in qTamung0
                            join dt in qdtbn on bn.IDDTBN equals dt.IDDTBN
                            select new { bn.MaBNhan, bn.TenBNhan, bn.Tuoi, bn.DChi, bn.NoiTru, bn.DTNT, dt.DTBN1, bn.PhanLoai, bn.NgayThu, bn.MaDV, bn.Mien, bn.MaKP, bn.SoLuong, bn.TienBN, bn.TrongBH, bn.ThanhTien, bn.DonGia, bn.SoTien }).ToList();
          
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
                                DTBN1 = bn.DTBN1.ToUpper().Trim(),
                                bn.MaDV,
                                bn.DonGia,
                                bn.SoLuong,
                                bn.ThanhTien,
                                bn.TrongBH,
                                bn.SoTien,
                                dv.IdTieuNhom,
                                dv.TenRG,
                                dv.IDNhom,
                                dv.TenNhomCT,
                                NgayTT = bn.NgayThu.Value.Date
                            }).ToList();

            var qTamung3 = (from bn in qTamung2
                            group bn by new { bn.TenBNhan, bn.Tuoi, bn.DChi, bn.MaBNhan, bn.NoiTru, bn.NgayTT, bn.DTBN1, bn.DTNT } into kq
                            select new
                            {
                                kq.Key.TenBNhan,
                                kq.Key.Tuoi,
                                kq.Key.DChi,
                                kq.Key.MaBNhan,
                                kq.Key.NgayTT,
                                kq.Key.DTBN1,

                                XN = kq.Where(p => idNhomXN.Contains(p.IdTieuNhom)).Sum(p => p.SoTien),
                                TDCN = kq.Where(p => idNhomTDCN.Contains(p.IdTieuNhom)).Where(p => !idDientim.Contains(p.IdTieuNhom)).Sum(p => p.SoTien),
                                SieuAm = kq.Where(p => idSieuam.Contains(p.IdTieuNhom)).Sum(p => p.SoTien),
                                SieuAmDL = kq.Where(p => idSieuamDL.Contains(p.IdTieuNhom)).Sum(p => p.SoTien),
                                DienTim = kq.Where(p => idDientim.Contains(p.IdTieuNhom)).Sum(p => p.SoTien),
                                ChupCT = kq.Where(p => idChupCT.Contains(p.IdTieuNhom)).Sum(p => p.SoTien),
                                DoCNHH = kq.Where(p => idDoCNHH.Contains(p.IdTieuNhom)).Sum(p => p.SoTien),
                                ThuThuatPT = kq.Where(p => idThuThuat.Contains(p.IdTieuNhom)).Where(p => !idNoiSoi.Contains(p.IdTieuNhom)).Sum(p => p.SoTien),
                                NoiSoi = kq.Where(p => idNoiSoi.Contains(p.IdTieuNhom)).Sum(p => p.SoTien),
                                Congkham = kq.Where(p => idCongkham.Contains(p.IdTieuNhom)).Sum(p => p.SoTien),
                                TienVC = kq.Where(p => idtienvc.Contains(p.IdTieuNhom)).Sum(p => p.SoTien),
                                XQ = kq.Where(p => idXQ.Contains(p.IdTieuNhom)).Sum(p => p.SoTien),
                                DoLoangXuong = kq.Where(p => idDoLoangXuong.Contains(p.IdTieuNhom)).Sum(p => p.SoTien),
                                LuuHuyetNao = kq.Where(p => idLuuHuyetNao.Contains(p.IdTieuNhom)).Sum(p => p.SoTien),
                                DienNaoDo = kq.Where(p => idDienNaoDo.Contains(p.IdTieuNhom)).Sum(p => p.SoTien)
                                //KSK = kq.Where(p => p.DTBN1 == "KSK").Where(p => idCongkham.Contains(p.IdTieuNhom)).Sum(p => p.SoTien)
                            }).OrderBy(p => p.NgayTT).ToList();
            List<VPHangNgay> qTamung4 = (from bn in qTamung3
                                         group bn by new { bn.TenBNhan, bn.Tuoi, bn.DChi, bn.MaBNhan, bn.NgayTT, bn.DTBN1 } into kq
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
                                             SieuAm = kq.Sum(p => p.SieuAm),
                                             DienTim = kq.Sum(p => p.DienTim),
                                             ChupCT = kq.Sum(p => p.ChupCT),
                                             ThuThuatPT = kq.Sum(p => p.ThuThuatPT),
                                             NoiSoi = kq.Sum(p => p.NoiSoi),
                                             Congkham = kq.Sum(p => p.Congkham),
                                             TienVC = kq.Sum(p => p.TienVC),
                                             XQ = kq.Sum(p => p.XQ),
                                             TienNGDM = 0,
                                             TienBNBH = 0,
                                             TienBNDV = 0,
                                             Tong = kq.Sum(p => p.XN + p.TDCN + p.SieuAm + p.DienTim + p.ThuThuatPT + p.NoiSoi + p.Congkham + p.TienVC + p.XQ + p.ChupCT)
                                         }).OrderBy(p => p.NgayTT).ToList();
            #endregion

            #region cp thu vienphi
            var q2 = (from bn in data.BenhNhans.Where(p => p.DTuong != "KSK").Where(p => p.TuyenDuoi == 0).Where(p => dtbn == 100 || p.IDDTBN == dtbn).Where(p => (_ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (_DTNT && p.NoiTru == 0 && p.DTNT == true) || (_noitru && p.NoiTru == 1))
                      join vp in data.VienPhis.Where(p => macbthu == "" ? true : p.MaCB == macbthu).Where(p => rdNgay.SelectedIndex == 1 ? (p.NgayDuyet >= tungay && p.NgayDuyet <= denngay) : (p.NgayTT >= tungay && p.NgayTT <= denngay)).Where(p => gioHC == 2 || (gioHC == 1 && p.NgoaiGio == 1) || (gioHC == 0 && (p.NgoaiGio == null || p.NgoaiGio != 1))) on bn.MaBNhan equals vp.MaBNhan
                      join vpct in data.VienPhicts.Where(p => cp == 2 ? true : p.TrongBH == cp).Where(p => p.ThanhToan == 0).Where(p => makp == 0 || p.MaKP == makp)
                      on vp.idVPhi equals vpct.idVPhi
                      select new
                      {
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
                          vpct.TienBN,
                          vp.NgayDuyet,
                          bn.TenBNhan,
                          bn.Tuoi,
                          bn.DChi,
                          bn.NoiTru,
                          bn.DTNT,
                          bn.DTuong
                      }).ToList();

            var q3 = (from vp in q2
                      join dv in qdv on vp.MaDV equals dv.MaDV
                      select new
                      {
                          vp.MaBNhan,
                          vp.TenBNhan,
                          vp.Tuoi,
                          vp.DChi,
                          vp.NoiTru,
                          vp.DTNT,
                          DTBN1 = vp.DTuong.ToUpper().Trim(),
                          vp.TrongBH,
                          vp.MaDV,
                          vp.DonGia,
                          vp.SoLuong,
                          vp.ThanhTien,
                          vp.ThanhToan,
                          vp.TienBH,
                          vp.TienBN,
                          //TienNGDM = vp.DTuong == "BHYT" && vp.TrongBH == 0 ? vp.TienBN : 0,
                          //TienBNBH = vp.DTuong == "BHYT" && vp.TrongBH == 1 ? vp.TienBN : 0,
                          //TienBNDV = vp.DTuong == "Dịch vụ" ? vp.TienBN : 0,
                         
                          dv.IdTieuNhom,
                          dv.TenRG,
                          dv.IDNhom,
                          dv.TenNhomCT,
                          NgayTT = (rdNgay.SelectedIndex == 1) ? vp.NgayDuyet.Value.Date : vp.NgayTT.Value.Date
                      }).ToList();

            var q4 = (from bn in q3
                      group bn by new { bn.TenBNhan, bn.Tuoi, bn.DChi, bn.MaBNhan, bn.NoiTru, bn.DTNT, bn.NgayTT, bn.DTBN1 } into kq
                      select new
                      {
                          kq.Key.TenBNhan,
                          kq.Key.Tuoi,
                          kq.Key.DChi,
                          kq.Key.MaBNhan,
                          kq.Key.NgayTT,
                          kq.Key.DTBN1,
                          Congkham = (kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idCongkham.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)),
                          TienBNBH = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => p.DTBN1 == "BHYT" && p.TrongBH == 1).Sum(p => p.TienBN),
                          TienNGDM = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => p.DTBN1 == "BHYT" && p.TrongBH == 0).Sum(p => p.TienBN),
                          TienBNDV = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => p.DTBN1 == "DỊCH VỤ").Sum(p => p.TienBN),
                      }).ToList();

            List<VPHangNgay> q5 = (from bn in q4
                                   group bn by new { bn.TenBNhan, bn.Tuoi, bn.DChi, bn.MaBNhan, bn.NgayTT, bn.DTBN1 } into kq
                                   select new VPHangNgay
                                   {
                                       TenBNhan = kq.Key.TenBNhan,
                                       Tuoi = kq.Key.Tuoi ?? 0,
                                       DChi = kq.Key.DChi,
                                       DTuong = kq.Key.DTBN1,
                                       MaBNhan = kq.Key.MaBNhan ?? 0,
                                       NgayTT = kq.Key.NgayTT,
                                       XN = 0,
                                       TDCN = 0,
                                       SieuAm = 0,
                                       ThuThuatPT = 0,
                                       NoiSoi = 0,
                                       Congkham = 0,
                                       TienVC = 0,
                                       XQ = 0,
                                       NgoaiTruBH = 0,
                                       TienNGDM = kq.Sum(p => p.TienNGDM),
                                       TienBNBH = kq.Sum(p => p.TienBNBH),
                                       TienBNDV = kq.Sum(p => p.TienBNDV),
                                       KSK = kq.Where(p => p.DTBN1 == "KSK").Sum(p => p.Congkham),
                                       Tong = kq.Sum(p => p.TienNGDM + p.TienBNBH + p.TienBNDV) + kq.Where(p => p.DTBN1 == "KSK").Sum(p => p.Congkham)
                                   }).OrderBy(p => p.NgayTT).ToList();
            #endregion
            var qksk1 = (from a in data.BenhNhans.Where(p => p.DTuong == "KSK")
                         join tu in data.TamUngs.Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay).Where(p => makp == 0 || p.MaKP == makp).Where(p => macbthu == "" ? true : p.MaCB == macbthu).Where(p => gioHC == 2 || (gioHC == 1 && p.NgoaiGio == 1) || (gioHC == 0 && (p.NgoaiGio == null || p.NgoaiGio != 1))) on a.MaBNhan equals tu.MaBNhan
                         select new
                         {
                             tu.NgayThu,
                             a.MaBNhan,
                             a.TenBNhan,
                             a.Tuoi,
                             a.DChi,
                             a.NoiTru,
                             a.DTuong,
                             tu.PhanLoai,
                             tu.SoTien,
                             tu.TienChenh
                             //ThanhTien = (tu.PhanLoai == 0 || tu.PhanLoai == 1 || tu.PhanLoai == 3) ? tu.SoTien : (tu.PhanLoai == 4 ? -tu.SoTien : (tu.PhanLoai == 2 ? tu.TienChenh : 0))
                         }).ToList();
            var qksk12 = (from a in qksk1
                          select new
                          {
                              NgayThu = a.NgayThu.Value.Date,
                              a.MaBNhan,
                              a.TenBNhan,
                              a.Tuoi,
                              a.DChi,
                              a.NoiTru,
                              DTBN1 = a.DTuong,
                              a.PhanLoai,
                              SoTien = a.SoTien ?? 0.00,
                              a.TienChenh
                          }).ToList();


            List<VPHangNgay> qksk2 = (from a in qksk12
                                      group a by new
                                      {
                                          a.MaBNhan,
                                          a.TenBNhan,
                                          a.Tuoi,
                                          a.DChi,
                                          a.NoiTru,
                                          a.DTBN1,
                                          a.NgayThu
                                      } into kq
                                      select new VPHangNgay
                                      {
                                          MaBNhan = kq.Key.MaBNhan,
                                          TenBNhan = kq.Key.TenBNhan,
                                          DTuong = kq.Key.DTBN1,
                                          Tuoi = kq.Key.Tuoi ?? 0,
                                          DChi = kq.Key.DChi,
                                          KSK = kq.Sum(p => p.SoTien),
                                          NgayTT = kq.Key.NgayThu ,
                                          Tong = kq.Sum(p => p.SoTien)
                                      }).ToList();
            all.AddRange(q5);
            all.AddRange(qTamung4);
            all.AddRange(qksk2);
            all = (from a in all
                   group a by new { a.TenBNhan, a.MaBNhan, a.DChi, a.Tuoi, a.NgayTT, a.DTuong } into kq
                   select new VPHangNgay
                   {
                       TenBNhan = kq.Key.TenBNhan,
                       Tuoi = kq.Key.Tuoi,
                       DChi = kq.Key.DChi,
                       MaBNhan = kq.Key.MaBNhan,
                       DTuong = kq.Key.DTuong,
                       NgayTT = kq.Key.NgayTT,
                       XN = kq.Sum(p => p.XN),
                       TDCN = kq.Sum(p => p.TDCN),
                       SieuAm = kq.Sum(p => p.SieuAm),
                       SieuAmDL = kq.Sum(p => p.SieuAmDL),
                       DienTim = kq.Sum(p => p.DienTim),
                       ThuThuatPT = kq.Sum(p => p.ThuThuatPT),
                       NoiSoi = kq.Sum(p => p.NoiSoi),
                       Congkham = kq.Sum(p => p.Congkham),
                       TienVC = kq.Sum(p => p.TienVC),
                       XQ = kq.Sum(p => p.XQ),

                       NgoaiTruBH = kq.Sum(p => p.NgoaiTruBH),
                       KSK = kq.Sum(p => p.KSK),
                       TienNGDM = kq.Sum(p => p.TienNGDM),
                       TienBNBH = kq.Sum(p => p.TienBNBH),
                       TienBNDV = kq.Sum(p => p.TienBNDV),
                       Tong = kq.Sum(p => p.Tong) // kq.Sum(p => p.XN + p.TDCN + p.SieuAm + p.DienTim + p.ThuThuatPT + p.NoiSoi + p.Congkham + p.TienVC + p.XQ + p.NgoaiTruBH + p.KSK + p.DoLoangXuong + p.LuuHuyetNao + p.TienThuoc + p.TienVTYT)
                   }).Where(p => p.Tong != 0).OrderBy(p => p.NgayTT).ToList();
          
            if (ckHienThiNgay.Checked)
            {
                BaoCao.rep_VienPhiHangNgay_30002_Ngay rep = new BaoCao.rep_VienPhiHangNgay_30002_Ngay(ckHienThiNgay.Checked);
                frmIn frm = new frmIn();
                rep.celTitBH.Text = "% BHYT";
                rep.DataSource = all;
                rep.colCQ.Text = DungChung.Bien.TenCQ.ToUpper();
                rep.cel_diadanh.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                rep.celNgayThang.Text = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy");
                    rep.CelTieuDe.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN VIỆN PHÍ HẰNG NGÀY";
                rep.cel_NguoiLap.Text = DungChung.Bien.NguoiLapBieu;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                BaoCao.rep_VienPhiHangNgay_30002_moi rep = new BaoCao.rep_VienPhiHangNgay_30002_moi();
                frmIn frm = new frmIn();
                rep.celTitBH.Text = "% BHYT";
                rep.DataSource = all;
                rep.colCQ.Text = DungChung.Bien.TenCQ.ToUpper();
                rep.cel_diadanh.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                rep.celNgayThang.Text = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy");
                rep.CelTieuDe.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN VIỆN PHÍ HẰNG NGÀY";
                rep.cel_NguoiLap.Text = DungChung.Bien.NguoiLapBieu;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }
    }
}