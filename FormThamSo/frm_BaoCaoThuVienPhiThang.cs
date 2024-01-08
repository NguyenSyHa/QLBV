using System;using QLBV_Database;
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

    public partial class frm_BaoCaoThuVienPhiThang : DevExpress.XtraEditors.XtraForm
    {
        public frm_BaoCaoThuVienPhiThang()
        {
            InitializeComponent();
        }


        private void btnOK_Click(object sender, EventArgs e)
        {

            if (kt())
            {
                DateTime ngaytu = System.DateTime.Now.Date;
                DateTime ngayden = System.DateTime.Now.Date;
                ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupngayden.DateTime);
                int _ngaytt = radTimKiem.SelectedIndex;

                bool noitru = ckNoiTru.Checked;
                bool ngoaitru = ckNgoaiTru.Checked;
                bool dtnt = ckDTNgoaiTru.Checked;
                bool BHYT = ckBH.Checked;
                bool bnDichvu = ckDichVu.Checked;
                bool bnKSK = ckKSK.Checked;
                int trongBH = 2;
                trongBH = rdTrongBH.SelectedIndex;
                string macb = "";
                if (lupCanBoTT.EditValue != null)
                    macb = lupCanBoTT.EditValue.ToString();
                int makp = 0, maKhoaA = 0, maKhoaB = 0, maKhoaC = 0;
                if (lupKhoaphong.EditValue != null)
                    makp = Convert.ToInt32(lupKhoaphong.EditValue);
                if (lupKhoaA.EditValue != null)
                    maKhoaA = Convert.ToInt32(lupKhoaA.EditValue);
                if (lupKhoaB.EditValue != null)
                    maKhoaB = Convert.ToInt32(lupKhoaB.EditValue);
                if (lupKhoaC.EditValue != null)
                    maKhoaC = Convert.ToInt32(lupKhoaC.EditValue);



                QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

                var qkp = (from kp in _data.KPhongs select new { kp.MaKP, kp.TenKP, KB = (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham || kp.PLoai == DungChung.Bien.st_PhanLoaiKP.CanLamSang) ? true : false, KhoaA = kp.MaKP == maKhoaA ? true : false, KhoaB = kp.MaKP == maKhoaB ? true : false, KhoaC = kp.MaKP == maKhoaC ? true : false }).ToList();
                var qdv0 = (from dv in _data.DichVus join tn in _data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom select new { dv.MaDV, dv.TenDV, tn.TenTN, tn.TenRG, tn.IdTieuNhom, tn.IDNhom}).ToList();
                var qdv = (from dv in qdv0 select new { dv.MaDV, dv.TenDV, dv.TenRG, TenTN = dv.TenRG == "X-Quang" ? ((dv.TenDV.Contains("cắt lớp vi tính") || dv.TenDV.Contains("CLVT") || dv.TenDV.Contains("CT Scanner")) ? "Chụp cắt lớp vi tính" : "X-Quang") : dv.TenRG, IdTieuNhom = dv.TenRG == "X-Quang" ? ((dv.TenDV.Contains("cắt lớp vi tính") || dv.TenDV.Contains("CLVT") || dv.TenDV.Contains("CT Scanner")) ? -1 : -2) : dv.IdTieuNhom, dv.IDNhom }).ToList();


                var qvp = (from vp in _data.VienPhis.Where(p => _ngaytt == 0 ? true : (_ngaytt == 1 ? (p.NgayTT >= ngaytu && p.NgayTT <= ngayden) : (_ngaytt == 2 ? (p.NgayDuyet >= ngaytu && p.NgayDuyet <= ngayden) : (p.NgayDuyetCP >= ngaytu && p.NgayDuyetCP <= ngayden))))
                           join rv in _data.RaViens.Where(p => _ngaytt == 0 ? (p.NgayRa >= ngaytu && p.NgayRa <= ngayden) : true) on vp.MaBNhan equals rv.MaBNhan
                           join bn in _data.BenhNhans.Where(p => ((BHYT && p.DTuong == "BHYT") || (bnDichvu && p.DTuong == "Dịch vụ") || (bnKSK && p.DTuong == "KSK")) && ((p.NoiTru == 1 && noitru) || (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (dtnt && p.NoiTru == 0 && p.DTNT == true))) on vp.MaBNhan equals bn.MaBNhan
                           join vpct in _data.VienPhicts.Where(p => ((trongBH == 2 && (p.TrongBH == 0 || p.TrongBH == 1)) || (p.TrongBH == trongBH && trongBH != 2)) && (macb == "" || p.MaCB == macb) && (makp == 0 || p.MaKP == makp) && p.ThanhToan == 0)
                           on vp.idVPhi equals vpct.idVPhi
                           select new { vp, rv, bn, vpct }).ToList();
                var qvp2 = (from vp in qvp
                            join kp in qkp on vp.vpct.MaKP equals kp.MaKP
                            join dv in qdv on vp.vpct.MaDV equals dv.MaDV
                            select new { vp.bn.MaBNhan, vp.bn.NoiTru, vp.vpct.TrongBH, vp.vpct.TienBH, vp.vpct.TienBN,  ThanhTien = vp.vpct.ThanhTien, vp.vpct.SoLuong, dv.MaDV, dv.TenDV, dv.TenRG, dv.IdTieuNhom, dv.TenTN, dv.IDNhom, kp.MaKP, kp.TenKP, kp.KB, kp.KhoaA, kp.KhoaB, kp.KhoaC, SoNgayDT = (vp.rv.SoNgaydt == null || vp.rv.SoNgaydt == 0) ? 1 : vp.rv.SoNgaydt.Value }).ToList();
                var qCPdichvu = qvp2.Where(p => p.TrongBH == 0).ToList();
                var qCPTyLe = qvp2.Where(p => p.TrongBH == 1 && p.TienBN != 0).ToList();
                var qCPBH = qvp2.Where(p => p.TrongBH == 1 && p.TienBH != 0).ToList();

                List<BC> _list = new List<BC>();

                #region số lượt
                BC soluot = new BC();
                soluot.TenTieuChi = "Số lượt";
                soluot.Row = 1;
                soluot.DichVuKhamBenh = qCPdichvu.Where(p => p.KB == true && p.NoiTru == 0).Select(p => p.MaBNhan).Distinct().Count();
                soluot.DichVuNoiTruKB = qCPdichvu.Where(p => p.KB == true && p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count();
                soluot.DichVuKhoaA = qCPdichvu.Where(p => p.KhoaA == true).Select(p => p.MaBNhan).Distinct().Count();
                soluot.DichVuKhoaB = qCPdichvu.Where(p => p.KhoaB == true).Select(p => p.MaBNhan).Distinct().Count();
                soluot.DichVuKhoaC = qCPdichvu.Where(p => p.KhoaC == true).Select(p => p.MaBNhan).Distinct().Count();


                soluot.TyLeKhamBenh = qCPTyLe.Where(p => p.KB == true && p.NoiTru == 0).Select(p => p.MaBNhan).Distinct().Count();
                soluot.TyLeNoiTruKB = qCPTyLe.Where(p => p.KB == true && p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count();
                soluot.TyLeKhoaA = qCPTyLe.Where(p => p.KhoaA == true).Select(p => p.MaBNhan).Distinct().Count();
                soluot.TyLeKhoaB = qCPTyLe.Where(p => p.KhoaB == true).Select(p => p.MaBNhan).Distinct().Count();
                soluot.TyLeKhoaC = qCPTyLe.Where(p => p.KhoaC == true).Select(p => p.MaBNhan).Distinct().Count();


                soluot.BHKhamBenh = qCPBH.Where(p => p.KB == true && p.NoiTru == 0).Select(p => p.MaBNhan).Distinct().Count();
                soluot.BHNoiTruKB = qCPBH.Where(p => p.KB == true && p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count();
                soluot.BHKhoaA = qCPBH.Where(p => p.KhoaA == true).Select(p => p.MaBNhan).Distinct().Count();
                soluot.BHKhoaB = qCPBH.Where(p => p.KhoaB == true).Select(p => p.MaBNhan).Distinct().Count();
                soluot.BHKhoaC = qCPBH.Where(p => p.KhoaC == true).Select(p => p.MaBNhan).Distinct().Count();

                //soluot.TTKhamBenh = soluot.DichVuKhamBenh + soluot.BHKhamBenh; //qvp2.Where(p => p.KB == true).Select(p => p.MaBNhan).Distinct().Count();
                //soluot.TTKhoaA = soluot.DichVuKhoaA + soluot.BHKhoaA; //qvp2.Where(p => p.KhoaA == true).Select(p => p.MaBNhan).Distinct().Count();
                //soluot.TTKhoaB = soluot.DichVuKhoaB + soluot.BHKhoaB; //qvp2.Where(p => p.KhoaB == true).Select(p => p.MaBNhan).Distinct().Count();
                //soluot.TTKhoaC = soluot.DichVuKhoaC + soluot.BHKhoaC; // qvp2.Where(p => p.KhoaC == true).Select(p => p.MaBNhan).Distinct().Count();

                soluot.TTKhamBenh = qvp2.Where(p => p.KB == true && p.NoiTru == 0).Select(p => p.MaBNhan).Distinct().Count();
                soluot.TTNoiTruKB = qvp2.Where(p => p.KB == true && p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count();
                soluot.TTKhoaA = qvp2.Where(p => p.KhoaA == true).Select(p => p.MaBNhan).Distinct().Count();
                soluot.TTKhoaB = qvp2.Where(p => p.KhoaB == true).Select(p => p.MaBNhan).Distinct().Count();
                soluot.TTKhoaC = qvp2.Where(p => p.KhoaC == true).Select(p => p.MaBNhan).Distinct().Count();

                //_list.Add(soluot);
                #endregion

                #region số ngày điều trị
                BC songaydt = new BC();
                songaydt.TenTieuChi = "Số ngày điều trị";
                songaydt.Row = 1;
                var qdv_SongayDT = (from vp in qCPdichvu group vp by new { vp.MaBNhan, vp.KB, vp.KhoaA, vp.KhoaB, vp.KhoaC, vp.SoNgayDT } into kq select new { kq.Key.MaBNhan, kq.Key.KB, kq.Key.KhoaA, kq.Key.KhoaB, kq.Key.KhoaC, kq.Key.SoNgayDT }).ToList();
                var qTyle_SongayDT = (from vp in qCPTyLe group vp by new { vp.MaBNhan, vp.KB, vp.KhoaA, vp.KhoaB, vp.KhoaC, vp.SoNgayDT } into kq select new { kq.Key.MaBNhan, kq.Key.KB, kq.Key.KhoaA, kq.Key.KhoaB, kq.Key.KhoaC, kq.Key.SoNgayDT }).ToList();
                var qbh_SongayDT = (from vp in qCPBH group vp by new { vp.MaBNhan, vp.KB, vp.KhoaA, vp.KhoaB, vp.KhoaC, vp.SoNgayDT } into kq select new { kq.Key.MaBNhan, kq.Key.KB, kq.Key.KhoaA, kq.Key.KhoaB, kq.Key.KhoaC, kq.Key.SoNgayDT }).ToList();
                var qtt_SongayDT = (from vp in qvp2 group vp by new { vp.MaBNhan, vp.KB, vp.KhoaA, vp.KhoaB, vp.KhoaC, vp.SoNgayDT } into kq select new { kq.Key.MaBNhan, kq.Key.KB, kq.Key.KhoaA, kq.Key.KhoaB, kq.Key.KhoaC, kq.Key.SoNgayDT }).ToList();

                songaydt.DichVuKhamBenh = 0;// qdv_SongayDT.Where(p => p.KB == true).Sum(p => p.SoNgayDT);
                songaydt.DichVuKhoaA = qdv_SongayDT.Where(p => p.KhoaA == true).Sum(p => p.SoNgayDT);
                songaydt.DichVuKhoaB = qdv_SongayDT.Where(p => p.KhoaB == true).Sum(p => p.SoNgayDT);
                songaydt.DichVuKhoaC = qdv_SongayDT.Where(p => p.KhoaC == true).Sum(p => p.SoNgayDT);


                songaydt.TyLeKhamBenh = 0;//qTyle_SongayDT.Where(p => p.KB == true).Sum(p => p.SoNgayDT);
                songaydt.TyLeKhoaA = qTyle_SongayDT.Where(p => p.KhoaA == true).Sum(p => p.SoNgayDT);
                songaydt.TyLeKhoaB = qTyle_SongayDT.Where(p => p.KhoaB == true).Sum(p => p.SoNgayDT);
                songaydt.TyLeKhoaC = qTyle_SongayDT.Where(p => p.KhoaC == true).Sum(p => p.SoNgayDT);


                songaydt.BHKhamBenh = 0;// qbh_SongayDT.Where(p => p.KB == true).Sum(p => p.SoNgayDT);
                songaydt.BHKhoaA = qbh_SongayDT.Where(p => p.KhoaA == true).Sum(p => p.SoNgayDT);
                songaydt.BHKhoaB = qbh_SongayDT.Where(p => p.KhoaB == true).Sum(p => p.SoNgayDT);
                songaydt.BHKhoaC = qbh_SongayDT.Where(p => p.KhoaC == true).Sum(p => p.SoNgayDT);

                songaydt.TTKhamBenh = 0;// qtt_SongayDT.Where(p => p.KB == true).Sum(p => p.SoNgayDT);
                songaydt.TTKhoaA = qtt_SongayDT.Where(p => p.KhoaA == true).Sum(p => p.SoNgayDT);
                songaydt.TTKhoaB = qtt_SongayDT.Where(p => p.KhoaB == true).Sum(p => p.SoNgayDT);
                songaydt.TTKhoaC = qtt_SongayDT.Where(p => p.KhoaC == true).Sum(p => p.SoNgayDT);


                //  _list.Add(songaydt);
                #endregion
                #region xét nghiệm
                BC xn = new BC();
                xn.TenNhom = "XN";
                xn.TenTieuChi = "Xét nghiệm";
                xn.Row = 2;
                xn.DichVuKhamBenh = qCPdichvu.Where(p => p.KB == true && p.NoiTru == 0 && p.IDNhom == 1).Sum(p => p.SoLuong);
                xn.DichVuNoiTruKB = qCPdichvu.Where(p => p.KB == true && p.NoiTru == 1 && p.IDNhom == 1).Sum(p => p.SoLuong);
                xn.DichVuKhoaA = qCPdichvu.Where(p => p.KhoaA == true && p.IDNhom == 1).Sum(p => p.SoLuong);
                xn.DichVuKhoaB = qCPdichvu.Where(p => p.KhoaB == true && p.IDNhom == 1).Sum(p => p.SoLuong);
                xn.DichVuKhoaC = qCPdichvu.Where(p => p.KhoaC == true && p.IDNhom == 1).Sum(p => p.SoLuong);


                xn.TyLeKhamBenh = qCPTyLe.Where(p => p.KB == true && p.NoiTru == 0 && p.IDNhom == 1).Sum(p => p.SoLuong);
                xn.TyLeNoiTruKB = qCPTyLe.Where(p => p.KB == true && p.NoiTru == 1 && p.IDNhom == 1).Sum(p => p.SoLuong);
                xn.TyLeKhoaA = qCPTyLe.Where(p => p.KhoaA == true && p.IDNhom == 1).Sum(p => p.SoLuong);
                xn.TyLeKhoaB = qCPTyLe.Where(p => p.KhoaB == true && p.IDNhom == 1).Sum(p => p.SoLuong);
                xn.TyLeKhoaC = qCPTyLe.Where(p => p.KhoaC == true && p.IDNhom == 1).Sum(p => p.SoLuong);


                xn.BHKhamBenh = qCPBH.Where(p => p.KB == true && p.NoiTru == 0 && p.IDNhom == 1).Sum(p => p.SoLuong);
                xn.BHNoiTruKB = qCPBH.Where(p => p.KB == true && p.NoiTru == 1 && p.IDNhom == 1).Sum(p => p.SoLuong);
                xn.BHKhoaA = qCPBH.Where(p => p.KhoaA == true && p.IDNhom == 1).Sum(p => p.SoLuong);
                xn.BHKhoaB = qCPBH.Where(p => p.KhoaB == true && p.IDNhom == 1).Sum(p => p.SoLuong);
                xn.BHKhoaC = qCPBH.Where(p => p.KhoaC == true && p.IDNhom == 1).Sum(p => p.SoLuong);

                xn.TTKhamBenh = xn.DichVuKhamBenh + xn.BHKhamBenh;
                xn.TTNoiTruKB = xn.DichVuNoiTruKB + xn.BHNoiTruKB;
                xn.TTKhoaA = xn.DichVuKhoaA + xn.BHKhoaA;
                xn.TTKhoaB = xn.DichVuKhoaB + xn.BHKhoaB;
                xn.TTKhoaC = xn.DichVuKhoaC + xn.BHKhoaC;

                //xn.TTKhamBenh = qvp2.Where(p => p.KB == true && p.IDNhom == 1).Sum(p => p.SoLuong);
                //xn.TTKhoaA = qvp2.Where(p => p.KhoaA == true && p.IDNhom == 1).Sum(p => p.SoLuong);
                //xn.TTKhoaB = qvp2.Where(p => p.KhoaB == true && p.IDNhom == 1).Sum(p => p.SoLuong);
                //xn.TTKhoaC = qvp2.Where(p => p.KhoaC == true && p.IDNhom == 1).Sum(p => p.SoLuong);

                //thành tiền

                xn.DichVuKhamBenhTT = qCPdichvu.Where(p => p.KB == true && p.NoiTru == 0 && p.IDNhom == 1).Sum(p => p.ThanhTien);
                xn.DichVuNoiTruKBTT = qCPdichvu.Where(p => p.KB == true && p.NoiTru == 1 && p.IDNhom == 1).Sum(p => p.ThanhTien);
                xn.DichVuKhoaATT = qCPdichvu.Where(p => p.KhoaA == true && p.IDNhom == 1).Sum(p => p.ThanhTien);
                xn.DichVuKhoaBTT = qCPdichvu.Where(p => p.KhoaB == true && p.IDNhom == 1).Sum(p => p.ThanhTien);
                xn.DichVuKhoaCTT = qCPdichvu.Where(p => p.KhoaC == true && p.IDNhom == 1).Sum(p => p.ThanhTien);


                xn.TyLeKhamBenhTT = qCPTyLe.Where(p => p.KB == true && p.NoiTru == 0 && p.IDNhom == 1).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KB == true && p.NoiTru == 0 && p.IDNhom == 1).Sum(p => p.TienBH);
                xn.TyLeNoiTruKBTT = qCPTyLe.Where(p => p.KB == true && p.NoiTru == 1 && p.IDNhom == 1).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KB == true && p.NoiTru == 1 && p.IDNhom == 1).Sum(p => p.TienBH);
                xn.TyLeKhoaATT = qCPTyLe.Where(p => p.KhoaA == true && p.IDNhom == 1).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KhoaA == true && p.IDNhom == 1).Sum(p => p.TienBH);
                xn.TyLeKhoaBTT = qCPTyLe.Where(p => p.KhoaB == true && p.IDNhom == 1).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KhoaB == true && p.IDNhom == 1).Sum(p => p.TienBH);
                xn.TyLeKhoaCTT = qCPTyLe.Where(p => p.KhoaC == true && p.IDNhom == 1).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KhoaC == true && p.IDNhom == 1).Sum(p => p.TienBH);


                xn.BHKhamBenhTT = qCPBH.Where(p => p.KB == true && p.NoiTru == 0 && p.IDNhom == 1).Sum(p => p.TienBH);
                xn.BHNoiTruKBTT = qCPBH.Where(p => p.KB == true && p.NoiTru == 1 && p.IDNhom == 1).Sum(p => p.TienBH);
                xn.BHKhoaATT = qCPBH.Where(p => p.KhoaA == true && p.IDNhom == 1).Sum(p => p.TienBH);
                xn.BHKhoaBTT = qCPBH.Where(p => p.KhoaB == true && p.IDNhom == 1).Sum(p => p.TienBH);
                xn.BHKhoaCTT = qCPBH.Where(p => p.KhoaC == true && p.IDNhom == 1).Sum(p => p.TienBH);

                xn.TTKhamBenhTT = qvp2.Where(p => p.KB == true && p.NoiTru == 0 && p.IDNhom == 1).Sum(p => p.ThanhTien);
                xn.TTNoiTruKBTT = qvp2.Where(p => p.KB == true && p.NoiTru == 1 && p.IDNhom == 1).Sum(p => p.ThanhTien);
                xn.TTKhoaATT = qvp2.Where(p => p.KhoaA == true && p.IDNhom == 1).Sum(p => p.ThanhTien);
                xn.TTKhoaBTT = qvp2.Where(p => p.KhoaB == true && p.IDNhom == 1).Sum(p => p.ThanhTien);
                xn.TTKhoaCTT = qvp2.Where(p => p.KhoaC == true && p.IDNhom == 1).Sum(p => p.ThanhTien);
                _list.Add(xn);

                BC xn2 = new BC();
                xn2.TenNhom = "";
                xn2.TenTieuChi = "Xét nghiệm PCR";
                xn2.Row = 2;
                _list.Add(xn2);

                #endregion

                #region chẩn đoán hình ảnh
                var qCDHA = qvp2.Where(p => p.IDNhom == 2 || p.IDNhom == 3).ToList();
                var qdvCDHA = (from dv in qCDHA group dv by new { dv.TenTN, dv.IdTieuNhom, dv.IDNhom, dv.TenRG } into kq select new { kq.Key.IdTieuNhom, kq.Key.TenTN, kq.Key.IDNhom, kq.Key.TenRG}).OrderBy(p => p.IDNhom).ToList();
                int count = 0;
                foreach (var a in qdvCDHA)
                {
                    count++;

                    BC cdha = new BC();

                    cdha.TenTieuChi = a.TenTN;
                    cdha.Row = qdvCDHA.Count;
                    if (count == 1)
                    {
                        cdha.TenNhom = "CĐHA";

                    }
                    else
                        cdha.TenNhom = "";
                    cdha.DichVuKhamBenh = qCPdichvu.Where(p => p.KB == true && p.NoiTru == 0 && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.SoLuong);
                    cdha.DichVuNoiTruKB= qCPdichvu.Where(p => p.KB == true && p.NoiTru == 1 && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.SoLuong);
                    cdha.DichVuKhoaA = qCPdichvu.Where(p => p.KhoaA == true && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.SoLuong);
                    cdha.DichVuKhoaB = qCPdichvu.Where(p => p.KhoaB == true && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.SoLuong);
                    cdha.DichVuKhoaC = qCPdichvu.Where(p => p.KhoaC == true && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.SoLuong);


                    cdha.TyLeKhamBenh = qCPTyLe.Where(p => p.KB == true && p.NoiTru == 0 && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.SoLuong);
                    cdha.TyLeNoiTruKB = qCPTyLe.Where(p => p.KB == true && p.NoiTru == 1 && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.SoLuong);
                    cdha.TyLeKhoaA = qCPTyLe.Where(p => p.KhoaA == true && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.SoLuong);
                    cdha.TyLeKhoaB = qCPTyLe.Where(p => p.KhoaB == true && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.SoLuong);
                    cdha.TyLeKhoaC = qCPTyLe.Where(p => p.KhoaC == true && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.SoLuong);


                    cdha.BHKhamBenh = qCPBH.Where(p => p.KB == true && p.NoiTru == 0 && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.SoLuong);
                    cdha.BHNoiTruKB = qCPBH.Where(p => p.KB == true && p.NoiTru == 1 && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.SoLuong);
                    cdha.BHKhoaA = qCPBH.Where(p => p.KhoaA == true && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.SoLuong);
                    cdha.BHKhoaB = qCPBH.Where(p => p.KhoaB == true && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.SoLuong);
                    cdha.BHKhoaC = qCPBH.Where(p => p.KhoaC == true && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.SoLuong);

                    cdha.TTKhamBenh = cdha.DichVuKhamBenh + cdha.BHKhamBenh;
                    cdha.TTNoiTruKB = cdha.DichVuNoiTruKB + cdha.BHNoiTruKB;
                    cdha.TTKhoaA = cdha.DichVuKhoaA + cdha.BHKhoaA;
                    cdha.TTKhoaB = cdha.DichVuKhoaB + cdha.BHKhoaB;
                    cdha.TTKhoaC = cdha.DichVuKhoaC + cdha.BHKhoaC;


                    //thành tiền

                    cdha.DichVuKhamBenhTT = qCPdichvu.Where(p => p.KB == true && p.NoiTru == 0 && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.ThanhTien);
                    cdha.DichVuNoiTruKBTT = qCPdichvu.Where(p => p.KB == true && p.NoiTru == 1 && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.ThanhTien);
                    cdha.DichVuKhoaATT = qCPdichvu.Where(p => p.KhoaA == true && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.ThanhTien);
                    cdha.DichVuKhoaBTT = qCPdichvu.Where(p => p.KhoaB == true && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.ThanhTien);
                    cdha.DichVuKhoaCTT = qCPdichvu.Where(p => p.KhoaC == true && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.ThanhTien);


                    cdha.TyLeKhamBenhTT = qCPTyLe.Where(p => p.KB == true && p.NoiTru == 0 && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KB == true && p.NoiTru == 0 && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.TienBH);
                    cdha.TyLeNoiTruKBTT = qCPTyLe.Where(p => p.KB == true && p.NoiTru == 1 && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KB == true && p.NoiTru == 1 && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.TienBH);
                    cdha.TyLeKhoaATT = qCPTyLe.Where(p => p.KhoaA == true && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KhoaA == true && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.TienBH);
                    cdha.TyLeKhoaBTT = qCPTyLe.Where(p => p.KhoaB == true && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KhoaB == true && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.TienBH); ;
                    cdha.TyLeKhoaCTT = qCPTyLe.Where(p => p.KhoaC == true && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KhoaC == true && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.TienBH); ;



                    cdha.BHKhamBenhTT = qCPBH.Where(p => p.KB == true && p.NoiTru == 0 && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.TienBH);
                    cdha.BHNoiTruKBTT = qCPBH.Where(p => p.KB == true && p.NoiTru == 1 && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.TienBH);
                    cdha.BHKhoaATT = qCPBH.Where(p => p.KhoaA == true && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.TienBH);
                    cdha.BHKhoaBTT = qCPBH.Where(p => p.KhoaB == true && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.TienBH);
                    cdha.BHKhoaCTT = qCPBH.Where(p => p.KhoaC == true && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.TienBH);

                    cdha.TTKhamBenhTT = qvp2.Where(p => p.KB == true && p.NoiTru == 0 && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.ThanhTien);
                    cdha.TTNoiTruKBTT = qvp2.Where(p => p.KB == true && p.NoiTru == 1 && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.ThanhTien);
                    cdha.TTKhoaATT = qvp2.Where(p => p.KhoaA == true && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.ThanhTien);
                    cdha.TTKhoaBTT = qvp2.Where(p => p.KhoaB == true && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.ThanhTien);
                    cdha.TTKhoaCTT = qvp2.Where(p => p.KhoaC == true && p.IdTieuNhom == a.IdTieuNhom).Sum(p => p.ThanhTien);
                    _list.Add(cdha);


                }
                #endregion

                #region chẩn phẫu thuật thủ thuật

                var qPTTT = (from dv in qvp2.Where(p => p.IDNhom == 8) group dv by new { dv.MaDV, dv.TenDV, dv.TenTN, dv.IdTieuNhom, dv.IDNhom } into kq select new { kq.Key.MaDV, kq.Key.TenDV, kq.Key.IdTieuNhom, kq.Key.TenTN, kq.Key.IDNhom }).OrderBy(p => p.IDNhom).ToList();
                count = 0;
                foreach (var a in qPTTT)
                {
                    count++;
                    BC pttt = new BC();

                    pttt.TenTieuChi = a.TenDV;
                    pttt.Row = qPTTT.Count;
                    if (count == 1)
                    {
                        pttt.TenNhom = "PTTT";
                    }
                    else
                        pttt.TenNhom = "";
                    pttt.DichVuKhamBenh = qCPdichvu.Where(p => p.MaDV == a.MaDV && p.KB == true && p.NoiTru == 0).Sum(p => p.SoLuong);
                    pttt.DichVuNoiTruKB = qCPdichvu.Where(p => p.MaDV == a.MaDV && p.KB == true && p.NoiTru == 1).Sum(p => p.SoLuong);
                    pttt.DichVuKhoaA = qCPdichvu.Where(p => p.MaDV == a.MaDV && p.KhoaA == true).Sum(p => p.SoLuong);
                    pttt.DichVuKhoaB = qCPdichvu.Where(p => p.MaDV == a.MaDV && p.KhoaB == true).Sum(p => p.SoLuong);
                    pttt.DichVuKhoaC = qCPdichvu.Where(p => p.MaDV == a.MaDV && p.KhoaC == true).Sum(p => p.SoLuong);

                    pttt.TyLeKhamBenh = qCPTyLe.Where(p => p.MaDV == a.MaDV && p.KB == true && p.NoiTru == 0).Sum(p => p.SoLuong);
                    pttt.TyLeNoiTruKB = qCPTyLe.Where(p => p.MaDV == a.MaDV && p.KB == true && p.NoiTru == 1).Sum(p => p.SoLuong);
                    pttt.TyLeKhoaA = qCPTyLe.Where(p => p.MaDV == a.MaDV && p.KhoaA == true).Sum(p => p.SoLuong);
                    pttt.TyLeKhoaB = qCPTyLe.Where(p => p.MaDV == a.MaDV && p.KhoaB == true).Sum(p => p.SoLuong);
                    pttt.TyLeKhoaC = qCPTyLe.Where(p => p.MaDV == a.MaDV && p.KhoaC == true).Sum(p => p.SoLuong);

                    pttt.BHKhamBenh = qCPBH.Where(p => p.MaDV == a.MaDV && p.KB == true && p.NoiTru == 0).Sum(p => p.SoLuong);
                    pttt.BHNoiTruKB = qCPBH.Where(p => p.MaDV == a.MaDV && p.KB == true && p.NoiTru == 1).Sum(p => p.SoLuong);
                    pttt.BHKhoaA = qCPBH.Where(p => p.MaDV == a.MaDV && p.KhoaA == true).Sum(p => p.SoLuong);
                    pttt.BHKhoaB = qCPBH.Where(p => p.MaDV == a.MaDV && p.KhoaB == true).Sum(p => p.SoLuong);
                    pttt.BHKhoaC = qCPBH.Where(p => p.MaDV == a.MaDV && p.KhoaC == true).Sum(p => p.SoLuong);

                    //pttt.TTKhamBenh = qvp2.Where(p => p.KB == true && p.MaDV == a.MaDV).Sum(p=>p.SoLuong);
                    //pttt.TTKhoaA = qvp2.Where(p => p.KhoaA == true && p.MaDV == a.MaDV).Sum(p=>p.SoLuong);
                    //pttt.TTKhoaB = qvp2.Where(p => p.KhoaB == true && p.MaDV == a.MaDV).Sum(p=>p.SoLuong);
                    //pttt.TTKhoaC = qvp2.Where(p => p.KhoaC == true && p.MaDV == a.MaDV).Sum(p=>p.SoLuong);
                    pttt.TTKhamBenh = pttt.DichVuKhamBenh + pttt.BHKhamBenh;
                    pttt.TTNoiTruKB = pttt.DichVuNoiTruKB + pttt.BHNoiTruKB;
                    pttt.TTKhoaA = pttt.DichVuKhoaA + pttt.BHKhoaA;
                    pttt.TTKhoaB = pttt.DichVuKhoaB + pttt.BHKhoaB;
                    pttt.TTKhoaC = pttt.DichVuKhoaC + pttt.BHKhoaC;


                    //thành tiền

                    pttt.DichVuKhamBenhTT = qCPdichvu.Where(p => p.KB == true && p.NoiTru == 0 && p.MaDV == a.MaDV).Sum(p => p.ThanhTien);
                    pttt.DichVuNoiTruKBTT = qCPdichvu.Where(p => p.KB == true && p.NoiTru == 1 && p.MaDV == a.MaDV).Sum(p => p.ThanhTien);
                    pttt.DichVuKhoaATT = qCPdichvu.Where(p => p.KhoaA == true && p.MaDV == a.MaDV).Sum(p => p.ThanhTien);
                    pttt.DichVuKhoaBTT = qCPdichvu.Where(p => p.KhoaB == true && p.MaDV == a.MaDV).Sum(p => p.ThanhTien);
                    pttt.DichVuKhoaCTT = qCPdichvu.Where(p => p.KhoaC == true && p.MaDV == a.MaDV).Sum(p => p.ThanhTien);

                    pttt.TyLeKhamBenhTT = qCPTyLe.Where(p => p.KB == true && p.NoiTru == 0 && p.MaDV == a.MaDV).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KB == true && p.NoiTru == 0 && p.MaDV == a.MaDV).Sum(p => p.TienBH);
                    pttt.TyLeNoiTruKBTT = qCPTyLe.Where(p => p.KB == true && p.NoiTru == 1 && p.MaDV == a.MaDV).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KB == true && p.NoiTru == 1 && p.MaDV == a.MaDV).Sum(p => p.TienBH);
                    pttt.TyLeKhoaATT = qCPTyLe.Where(p => p.KhoaA == true && p.MaDV == a.MaDV).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KhoaA == true && p.MaDV == a.MaDV).Sum(p => p.TienBH);
                    pttt.TyLeKhoaBTT = qCPTyLe.Where(p => p.KhoaB == true && p.MaDV == a.MaDV).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KhoaB == true && p.MaDV == a.MaDV).Sum(p => p.TienBH);
                    pttt.TyLeKhoaCTT = qCPTyLe.Where(p => p.KhoaC == true && p.MaDV == a.MaDV).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KhoaC == true && p.MaDV == a.MaDV).Sum(p => p.TienBH);

                    pttt.BHKhamBenhTT = qCPBH.Where(p => p.KB == true && p.NoiTru == 0 && p.MaDV == a.MaDV).Sum(p => p.TienBH);
                    pttt.BHNoiTruKBTT = qCPBH.Where(p => p.KB == true && p.NoiTru == 1 && p.MaDV == a.MaDV).Sum(p => p.TienBH);
                    pttt.BHKhoaATT = qCPBH.Where(p => p.KhoaA == true && p.MaDV == a.MaDV).Sum(p => p.TienBH);
                    pttt.BHKhoaBTT = qCPBH.Where(p => p.KhoaB == true && p.MaDV == a.MaDV).Sum(p => p.TienBH);
                    pttt.BHKhoaCTT = qCPBH.Where(p => p.KhoaC == true && p.MaDV == a.MaDV).Sum(p => p.TienBH);

                    pttt.TTKhamBenhTT = qvp2.Where(p => p.KB == true && p.NoiTru == 0 && p.MaDV == a.MaDV).Sum(p => p.ThanhTien);
                    pttt.TTNoiTruKBTT = qvp2.Where(p => p.KB == true && p.NoiTru == 1 && p.MaDV == a.MaDV).Sum(p => p.ThanhTien);
                    pttt.TTKhoaATT = qvp2.Where(p => p.KhoaA == true && p.MaDV == a.MaDV).Sum(p => p.ThanhTien);
                    pttt.TTKhoaBTT = qvp2.Where(p => p.KhoaB == true && p.MaDV == a.MaDV).Sum(p => p.ThanhTien);
                    pttt.TTKhoaCTT = qvp2.Where(p => p.KhoaC == true && p.MaDV == a.MaDV).Sum(p => p.ThanhTien);
                    _list.Add(pttt);
                }
                #endregion
                count = 1;
                #region thuốc
                BC thuoc = new BC();
                thuoc.TenNhom = "Thuốc";
                thuoc.TenTieuChi = "";
                thuoc.Row = count;
                //thuoc.DichVuKhamBenh = qCPdichvu.Where(p => p.KB == true && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Count();
                //thuoc.DichVuKhoaA = qCPdichvu.Where(p => p.KhoaA == true && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Count();
                //thuoc.DichVuKhoaB = qCPdichvu.Where(p => p.KhoaB == true && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Count();
                //thuoc.DichVuKhoaC = qCPdichvu.Where(p => p.KhoaC == true && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Count();


                //thuoc.TyLeKhamBenh = qCPTyLe.Where(p => p.KB == true && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Count();
                //thuoc.TyLeKhoaA = qCPTyLe.Where(p => p.KhoaA == true && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Count();
                //thuoc.TyLeKhoaB = qCPTyLe.Where(p => p.KhoaB == true && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Count();
                //thuoc.TyLeKhoaC = qCPTyLe.Where(p => p.KhoaC == true && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Count();


                //thuoc.BHKhamBenh = qCPBH.Where(p => p.KB == true && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Count();
                //thuoc.BHKhoaA = qCPBH.Where(p => p.KhoaA == true && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Count();
                //thuoc.BHKhoaB = qCPBH.Where(p => p.KhoaB == true && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Count();
                //thuoc.BHKhoaC = qCPBH.Where(p => p.KhoaC == true && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Count();

                //thuoc.TTKhamBenh = qvp2.Where(p => p.KB == true && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Count();
                //thuoc.TTKhoaA = qvp2.Where(p => p.KhoaA == true && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Count();
                //thuoc.TTKhoaB = qvp2.Where(p => p.KhoaB == true && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Count();
                //thuoc.TTKhoaC = qvp2.Where(p => p.KhoaC == true && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Count();

                //thành tiền

                thuoc.DichVuKhamBenhTT = qCPdichvu.Where(p => p.KB == true && p.NoiTru == 0 && p.NoiTru == 0 && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Sum(p => p.ThanhTien);
                thuoc.DichVuNoiTruKBTT = qCPdichvu.Where(p => p.KB == true && p.NoiTru == 1 && p.NoiTru == 1 && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Sum(p => p.ThanhTien);
                thuoc.DichVuKhoaATT = qCPdichvu.Where(p => p.KhoaA == true && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Sum(p => p.ThanhTien);
                thuoc.DichVuKhoaBTT = qCPdichvu.Where(p => p.KhoaB == true && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Sum(p => p.ThanhTien);
                thuoc.DichVuKhoaCTT = qCPdichvu.Where(p => p.KhoaC == true && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Sum(p => p.ThanhTien);


                thuoc.TyLeKhamBenhTT = qCPTyLe.Where(p => p.KB == true && p.NoiTru == 0 && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KB == true && p.NoiTru == 0 && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Sum(p => p.TienBH);
                thuoc.TyLeNoiTruKBTT = qCPTyLe.Where(p => p.KB == true && p.NoiTru == 1 && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KB == true && p.NoiTru == 1 && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Sum(p => p.TienBH);
                thuoc.TyLeKhoaATT = qCPTyLe.Where(p => p.KhoaA == true && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KhoaA == true && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Sum(p => p.TienBH);
                thuoc.TyLeKhoaBTT = qCPTyLe.Where(p => p.KhoaB == true && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KhoaB == true && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Sum(p => p.TienBH);
                thuoc.TyLeKhoaCTT = qCPTyLe.Where(p => p.KhoaC == true && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KhoaC == true && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Sum(p => p.TienBH);


                thuoc.BHKhamBenhTT = qCPBH.Where(p => p.KB == true && p.NoiTru == 0 && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Sum(p => p.TienBH);
                thuoc.BHNoiTruKBTT = qCPBH.Where(p => p.KB == true && p.NoiTru == 1 && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Sum(p => p.TienBH);
                thuoc.BHKhoaATT = qCPBH.Where(p => p.KhoaA == true && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Sum(p => p.TienBH);
                thuoc.BHKhoaBTT = qCPBH.Where(p => p.KhoaB == true && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Sum(p => p.TienBH);
                thuoc.BHKhoaCTT = qCPBH.Where(p => p.KhoaC == true && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Sum(p => p.TienBH);

                thuoc.TTKhamBenhTT = qvp2.Where(p => p.KB == true && p.NoiTru == 0 && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Sum(p => p.ThanhTien);
                thuoc.TTNoiTruKBTT = qvp2.Where(p => p.KB == true && p.NoiTru == 1 && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Sum(p => p.ThanhTien);
                thuoc.TTKhoaATT = qvp2.Where(p => p.KhoaA == true && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Sum(p => p.ThanhTien);
                thuoc.TTKhoaBTT = qvp2.Where(p => p.KhoaB == true && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Sum(p => p.ThanhTien);
                thuoc.TTKhoaCTT = qvp2.Where(p => p.KhoaC == true && (p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7)).Sum(p => p.ThanhTien);
                _list.Add(thuoc);
                #endregion

                #region vtyt
                BC vtyt = new BC();
                vtyt.TenNhom = "VTYT";
                vtyt.TenTieuChi = "";
                vtyt.Row = count;
                vtyt.DichVuKhamBenh = qCPdichvu.Where(p => p.KB == true && p.NoiTru == 0 && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.SoLuong);
                vtyt.DichVuNoiTruKB = qCPdichvu.Where(p => p.KB == true && p.NoiTru == 1 && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.SoLuong);
                vtyt.DichVuKhoaA = qCPdichvu.Where(p => p.KhoaA == true && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.SoLuong);
                vtyt.DichVuKhoaB = qCPdichvu.Where(p => p.KhoaB == true && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.SoLuong);
                vtyt.DichVuKhoaC = qCPdichvu.Where(p => p.KhoaC == true && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.SoLuong);


                vtyt.TyLeKhamBenh = qCPTyLe.Where(p => p.KB == true && p.NoiTru == 0 && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.SoLuong);
                vtyt.TyLeNoiTruKB = qCPTyLe.Where(p => p.KB == true && p.NoiTru == 1 && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.SoLuong);
                vtyt.TyLeKhoaA = qCPTyLe.Where(p => p.KhoaA == true && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.SoLuong);
                vtyt.TyLeKhoaB = qCPTyLe.Where(p => p.KhoaB == true && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.SoLuong);
                vtyt.TyLeKhoaC = qCPTyLe.Where(p => p.KhoaC == true && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.SoLuong);


                vtyt.BHKhamBenh = qCPBH.Where(p => p.KB == true && p.NoiTru == 0 && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.SoLuong);
                vtyt.BHNoiTruKB = qCPBH.Where(p => p.KB == true && p.NoiTru == 1 && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.SoLuong);
                vtyt.BHKhoaA = qCPBH.Where(p => p.KhoaA == true && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.SoLuong);
                vtyt.BHKhoaB = qCPBH.Where(p => p.KhoaB == true && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.SoLuong);
                vtyt.BHKhoaC = qCPBH.Where(p => p.KhoaC == true && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.SoLuong);

                //vtyt.TTKhamBenh = qvp2.Where(p => p.KB == true && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p=>p.SoLuong);
                //vtyt.TTKhoaA = qvp2.Where(p => p.KhoaA == true && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p=>p.SoLuong);
                //vtyt.TTKhoaB = qvp2.Where(p => p.KhoaB == true && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p=>p.SoLuong);
                //vtyt.TTKhoaC = qvp2.Where(p => p.KhoaC == true && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p=>p.SoLuong);
                vtyt.TTKhamBenh = vtyt.DichVuKhamBenh + vtyt.BHKhamBenh;
                vtyt.TTNoiTruKB = vtyt.DichVuNoiTruKB + vtyt.BHNoiTruKB;
                vtyt.TTKhoaA = vtyt.DichVuKhoaA + vtyt.BHKhoaA;
                vtyt.TTKhoaB = vtyt.DichVuKhoaB + vtyt.BHKhoaB;
                vtyt.TTKhoaC = vtyt.DichVuKhoaC + vtyt.BHKhoaC;


                //thành tiền

                vtyt.DichVuKhamBenhTT = qCPdichvu.Where(p => p.KB == true && p.NoiTru == 0 && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.ThanhTien);
                vtyt.DichVuNoiTruKBTT = qCPdichvu.Where(p => p.KB == true && p.NoiTru == 1 && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.ThanhTien);
                vtyt.DichVuKhoaATT = qCPdichvu.Where(p => p.KhoaA == true && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.ThanhTien);
                vtyt.DichVuKhoaBTT = qCPdichvu.Where(p => p.KhoaB == true && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.ThanhTien);
                vtyt.DichVuKhoaCTT = qCPdichvu.Where(p => p.KhoaC == true && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.ThanhTien);


                vtyt.TyLeKhamBenhTT = qCPTyLe.Where(p => p.KB == true && p.NoiTru == 0 && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KB == true && p.NoiTru == 0 && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.TienBH);
                vtyt.TyLeNoiTruKBTT = qCPTyLe.Where(p => p.KB == true && p.NoiTru == 1 && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KB == true && p.NoiTru == 1 && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.TienBH);
                vtyt.TyLeKhoaATT = qCPTyLe.Where(p => p.KhoaA == true && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KhoaA == true && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.TienBH);
                vtyt.TyLeKhoaBTT = qCPTyLe.Where(p => p.KhoaB == true && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KhoaB == true && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.TienBH);
                vtyt.TyLeKhoaCTT = qCPTyLe.Where(p => p.KhoaC == true && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KhoaC == true && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.TienBH);


                vtyt.BHKhamBenhTT = qCPBH.Where(p => p.KB == true && p.NoiTru == 0 && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.TienBH);
                vtyt.BHNoiTruKBTT = qCPBH.Where(p => p.KB == true && p.NoiTru == 1 && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.TienBH);
                vtyt.BHKhoaATT = qCPBH.Where(p => p.KhoaA == true && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.TienBH);
                vtyt.BHKhoaBTT = qCPBH.Where(p => p.KhoaB == true && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.TienBH);
                vtyt.BHKhoaCTT = qCPBH.Where(p => p.KhoaC == true && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.TienBH);

                vtyt.TTKhamBenhTT = qvp2.Where(p => p.KB == true && p.NoiTru == 0 && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.ThanhTien);
                vtyt.TTNoiTruKBTT = qvp2.Where(p => p.KB == true && p.NoiTru == 1 && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.ThanhTien);
                vtyt.TTKhoaATT = qvp2.Where(p => p.KhoaA == true && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.ThanhTien);
                vtyt.TTKhoaBTT = qvp2.Where(p => p.KhoaB == true && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.ThanhTien);
                vtyt.TTKhoaCTT = qvp2.Where(p => p.KhoaC == true && (p.IDNhom == 10 || p.IDNhom == 11)).Sum(p => p.ThanhTien);
                _list.Add(vtyt);
                #endregion


                #region công khám
                BC ck = new BC();
                ck.TenNhom = "Công khám";
                ck.TenTieuChi = "";
                ck.Row = count;
                ck.DichVuKhamBenh = qCPdichvu.Where(p => p.KB == true && p.NoiTru == 0 && p.IDNhom == 13).Sum(p => p.SoLuong);
                ck.DichVuNoiTruKB = qCPdichvu.Where(p => p.KB == true && p.NoiTru == 1 && p.IDNhom == 13).Sum(p => p.SoLuong);               
                ck.DichVuKhoaA = qCPdichvu.Where(p => p.KhoaA == true && p.IDNhom == 13).Sum(p => p.SoLuong);
                ck.DichVuKhoaB = qCPdichvu.Where(p => p.KhoaB == true && p.IDNhom == 13).Sum(p => p.SoLuong);
                ck.DichVuKhoaC = qCPdichvu.Where(p => p.KhoaC == true && p.IDNhom == 13).Sum(p => p.SoLuong);


                ck.TyLeKhamBenh = qCPTyLe.Where(p => p.KB == true && p.NoiTru == 0 && p.IDNhom == 13).Sum(p => p.SoLuong);
                ck.TyLeNoiTruKB = qCPTyLe.Where(p => p.KB == true && p.NoiTru == 1 && p.IDNhom == 13).Sum(p => p.SoLuong);
                ck.TyLeKhoaA = qCPTyLe.Where(p => p.KhoaA == true && p.IDNhom == 13).Sum(p => p.SoLuong);
                ck.TyLeKhoaB = qCPTyLe.Where(p => p.KhoaB == true && p.IDNhom == 13).Sum(p => p.SoLuong);
                ck.TyLeKhoaC = qCPTyLe.Where(p => p.KhoaC == true && p.IDNhom == 13).Sum(p => p.SoLuong);


                ck.BHKhamBenh = qCPBH.Where(p => p.KB == true && p.NoiTru == 0 && p.IDNhom == 13).Sum(p => p.SoLuong);
                ck.BHNoiTruKB = qCPBH.Where(p => p.KB == true && p.NoiTru == 1 && p.IDNhom == 13).Sum(p => p.SoLuong);
                ck.BHKhoaA = qCPBH.Where(p => p.KhoaA == true && p.IDNhom == 13).Sum(p => p.SoLuong);
                ck.BHKhoaB = qCPBH.Where(p => p.KhoaB == true && p.IDNhom == 13).Sum(p => p.SoLuong);
                ck.BHKhoaC = qCPBH.Where(p => p.KhoaC == true && p.IDNhom == 13).Sum(p => p.SoLuong);

                //ck.TTKhamBenh = qvp2.Where(p => p.KB == true && p.IDNhom == 13).Sum(p=>p.SoLuong);
                //ck.TTKhoaA = qvp2.Where(p => p.KhoaA == true && p.IDNhom == 13).Sum(p=>p.SoLuong);
                //ck.TTKhoaB = qvp2.Where(p => p.KhoaB == true && p.IDNhom == 13).Sum(p=>p.SoLuong);
                //ck.TTKhoaC = qvp2.Where(p => p.KhoaC == true && p.IDNhom == 13).Sum(p=>p.SoLuong);
                ck.TTKhamBenh = ck.DichVuKhamBenh + ck.BHKhamBenh;
                ck.TTNoiTruKB = ck.DichVuNoiTruKB + ck.BHNoiTruKB;
                ck.TTKhoaA = ck.DichVuKhoaA + ck.BHKhoaA;
                ck.TTKhoaB = ck.DichVuKhoaB + ck.BHKhoaB;
                ck.TTKhoaC = ck.DichVuKhoaC + ck.BHKhoaC;

                //thành tiền

                ck.DichVuKhamBenhTT = qCPdichvu.Where(p => p.KB == true && p.NoiTru == 0 && p.IDNhom == 13).Sum(p => p.ThanhTien);
                ck.DichVuNoiTruKBTT = qCPdichvu.Where(p => p.KB == true && p.NoiTru == 1 && p.IDNhom == 13).Sum(p => p.ThanhTien);
                ck.DichVuKhoaATT = qCPdichvu.Where(p => p.KhoaA == true && p.IDNhom == 13).Sum(p => p.ThanhTien);
                ck.DichVuKhoaBTT = qCPdichvu.Where(p => p.KhoaB == true && p.IDNhom == 13).Sum(p => p.ThanhTien);
                ck.DichVuKhoaCTT = qCPdichvu.Where(p => p.KhoaC == true && p.IDNhom == 13).Sum(p => p.ThanhTien);


                ck.TyLeKhamBenhTT = qCPTyLe.Where(p => p.KB == true && p.NoiTru == 0 && p.IDNhom == 13).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KB == true && p.NoiTru == 0 && p.IDNhom == 13).Sum(p => p.TienBH);
                ck.TyLeNoiTruKBTT = qCPTyLe.Where(p => p.KB == true && p.NoiTru == 1 && p.IDNhom == 13).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KB == true && p.NoiTru == 1 && p.IDNhom == 13).Sum(p => p.TienBH);
                ck.TyLeKhoaATT = qCPTyLe.Where(p => p.KhoaA == true && p.IDNhom == 13).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KhoaA == true && p.IDNhom == 13).Sum(p => p.TienBH);
                ck.TyLeKhoaBTT = qCPTyLe.Where(p => p.KhoaB == true && p.IDNhom == 13).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KhoaB == true && p.IDNhom == 13).Sum(p => p.TienBH);
                ck.TyLeKhoaCTT = qCPTyLe.Where(p => p.KhoaC == true && p.IDNhom == 13).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KhoaC == true && p.IDNhom == 13).Sum(p => p.TienBH);


                ck.BHKhamBenhTT = qCPBH.Where(p => p.KB == true && p.NoiTru == 0 && p.IDNhom == 13).Sum(p => p.TienBH);
                ck.BHNoiTruKBTT = qCPBH.Where(p => p.KB == true && p.NoiTru == 1 && p.IDNhom == 13).Sum(p => p.TienBH);
                ck.BHKhoaATT = qCPBH.Where(p => p.KhoaA == true && p.IDNhom == 13).Sum(p => p.TienBH);
                ck.BHKhoaBTT = qCPBH.Where(p => p.KhoaB == true && p.IDNhom == 13).Sum(p => p.TienBH);
                ck.BHKhoaCTT = qCPBH.Where(p => p.KhoaC == true && p.IDNhom == 13).Sum(p => p.TienBH);

                ck.TTKhamBenhTT = qvp2.Where(p => p.KB == true && p.NoiTru == 0 && p.IDNhom == 13).Sum(p => p.ThanhTien);
                ck.TTNoiTruKBTT = qvp2.Where(p => p.KB == true && p.NoiTru == 1 && p.IDNhom == 13).Sum(p => p.ThanhTien);
                ck.TTKhoaATT = qvp2.Where(p => p.KhoaA == true && p.IDNhom == 13).Sum(p => p.ThanhTien);
                ck.TTKhoaBTT = qvp2.Where(p => p.KhoaB == true && p.IDNhom == 13).Sum(p => p.ThanhTien);
                ck.TTKhoaCTT = qvp2.Where(p => p.KhoaC == true && p.IDNhom == 13).Sum(p => p.ThanhTien);
                _list.Add(ck);
                #endregion

                #region tiền giường
                BC giuong = new BC();
                giuong.TenNhom = "Giường";
                giuong.TenTieuChi = "";
                giuong.Row = count;
                giuong.DichVuKhamBenh = qCPdichvu.Where(p => p.KB == true && p.NoiTru == 0 && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.SoLuong);
                giuong.DichVuNoiTruKB = qCPdichvu.Where(p => p.KB == true && p.NoiTru == 1 && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.SoLuong);
                giuong.DichVuKhoaA = qCPdichvu.Where(p => p.KhoaA == true && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.SoLuong);
                giuong.DichVuKhoaB = qCPdichvu.Where(p => p.KhoaB == true && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.SoLuong);
                giuong.DichVuKhoaC = qCPdichvu.Where(p => p.KhoaC == true && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.SoLuong);


                giuong.TyLeKhamBenh = qCPTyLe.Where(p => p.KB == true && p.NoiTru == 0 && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.SoLuong);
                giuong.TyLeNoiTruKB = qCPTyLe.Where(p => p.KB == true && p.NoiTru == 1 && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.SoLuong);
                giuong.TyLeKhoaA = qCPTyLe.Where(p => p.KhoaA == true && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.SoLuong);
                giuong.TyLeKhoaB = qCPTyLe.Where(p => p.KhoaB == true && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.SoLuong);
                giuong.TyLeKhoaC = qCPTyLe.Where(p => p.KhoaC == true && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.SoLuong);


                giuong.BHKhamBenh = qCPBH.Where(p => p.KB == true && p.NoiTru == 0 && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.SoLuong);
                giuong.BHNoiTruKB = qCPBH.Where(p => p.KB == true && p.NoiTru == 1 && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.SoLuong);
                giuong.BHKhoaA = qCPBH.Where(p => p.KhoaA == true && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.SoLuong);
                giuong.BHKhoaB = qCPBH.Where(p => p.KhoaB == true && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.SoLuong);
                giuong.BHKhoaC = qCPBH.Where(p => p.KhoaC == true && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.SoLuong);

                //giuong.TTKhamBenh = qvp2.Where(p => p.KB == true && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p=>p.SoLuong);
                //giuong.TTKhoaA = qvp2.Where(p => p.KhoaA == true && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p=>p.SoLuong);
                //giuong.TTKhoaB = qvp2.Where(p => p.KhoaB == true && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p=>p.SoLuong);
                //giuong.TTKhoaC = qvp2.Where(p => p.KhoaC == true && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p=>p.SoLuong);
                giuong.TTKhamBenh = giuong.DichVuKhamBenh + giuong.BHKhamBenh;
                giuong.TTNoiTruKB = giuong.DichVuNoiTruKB + giuong.BHNoiTruKB;
                giuong.TTKhoaA = giuong.DichVuKhoaA + giuong.BHKhoaA;
                giuong.TTKhoaB = giuong.DichVuKhoaB + giuong.BHKhoaB;
                giuong.TTKhoaC = giuong.DichVuKhoaC + giuong.BHKhoaC;

                //thành tiền

                giuong.DichVuKhamBenhTT = qCPdichvu.Where(p => p.KB == true && p.NoiTru == 0 && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.ThanhTien);
                giuong.DichVuNoiTruKBTT = qCPdichvu.Where(p => p.KB == true && p.NoiTru == 1 && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.ThanhTien);
                giuong.DichVuKhoaATT = qCPdichvu.Where(p => p.KhoaA == true && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.ThanhTien);
                giuong.DichVuKhoaBTT = qCPdichvu.Where(p => p.KhoaB == true && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.ThanhTien);
                giuong.DichVuKhoaCTT = qCPdichvu.Where(p => p.KhoaC == true && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.ThanhTien);


                giuong.TyLeKhamBenhTT = qCPTyLe.Where(p => p.KB == true && p.NoiTru == 0 && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KB == true && p.NoiTru == 0 && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.TienBH);
                giuong.TyLeNoiTruKBTT = qCPTyLe.Where(p => p.KB == true && p.NoiTru == 1 && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KB == true && p.NoiTru == 1 && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.TienBH);
                giuong.TyLeKhoaATT = qCPTyLe.Where(p => p.KhoaA == true && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KhoaA == true && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.TienBH);
                giuong.TyLeKhoaBTT = qCPTyLe.Where(p => p.KhoaB == true && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KhoaB == true && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.TienBH);
                giuong.TyLeKhoaCTT = qCPTyLe.Where(p => p.KhoaC == true && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KhoaC == true && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.TienBH);


                giuong.BHKhamBenhTT = qCPBH.Where(p => p.KB == true && p.NoiTru == 0 && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.TienBH);
                giuong.BHNoiTruKBTT = qCPBH.Where(p => p.KB == true && p.NoiTru == 1 && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.TienBH);
                giuong.BHKhoaATT = qCPBH.Where(p => p.KhoaA == true && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.TienBH);
                giuong.BHKhoaBTT = qCPBH.Where(p => p.KhoaB == true && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.TienBH);
                giuong.BHKhoaCTT = qCPBH.Where(p => p.KhoaC == true && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.TienBH);

                giuong.TTKhamBenhTT = qvp2.Where(p => p.KB == true && p.NoiTru == 0 && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.ThanhTien);
                giuong.TTNoiTruKBTT = qvp2.Where(p => p.KB == true && p.NoiTru == 1 && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.ThanhTien);
                giuong.TTKhoaATT = qvp2.Where(p => p.KhoaA == true && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.ThanhTien);
                giuong.TTKhoaBTT = qvp2.Where(p => p.KhoaB == true && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.ThanhTien);
                giuong.TTKhoaCTT = qvp2.Where(p => p.KhoaC == true && (p.IDNhom == 14 || p.IDNhom == 15)).Sum(p => p.ThanhTien);
                _list.Add(giuong);
                #endregion

                #region cpvc
                BC cpvc = new BC();
                cpvc.TenNhom = "CP V.Chuyển";
                cpvc.TenTieuChi = "";
                cpvc.Row = count;
                cpvc.DichVuKhamBenh = qCPdichvu.Where(p => p.KB == true && p.NoiTru == 0 && p.IDNhom == 12).Sum(p => p.SoLuong);
                cpvc.DichVuNoiTruKB = qCPdichvu.Where(p => p.KB == true && p.NoiTru == 1 && p.IDNhom == 12).Sum(p => p.SoLuong);
                cpvc.DichVuKhoaA = qCPdichvu.Where(p => p.KhoaA == true && p.IDNhom == 12).Sum(p => p.SoLuong);
                cpvc.DichVuKhoaB = qCPdichvu.Where(p => p.KhoaB == true && p.IDNhom == 12).Sum(p => p.SoLuong);
                cpvc.DichVuKhoaC = qCPdichvu.Where(p => p.KhoaC == true && p.IDNhom == 12).Sum(p => p.SoLuong);


                cpvc.TyLeKhamBenh = qCPTyLe.Where(p => p.KB == true && p.NoiTru == 0 && p.IDNhom == 12).Sum(p => p.SoLuong);
                cpvc.TyLeNoiTruKB = qCPTyLe.Where(p => p.KB == true && p.NoiTru == 1 && p.IDNhom == 12).Sum(p => p.SoLuong);
                cpvc.TyLeKhoaA = qCPTyLe.Where(p => p.KhoaA == true && p.IDNhom == 12).Sum(p => p.SoLuong);
                cpvc.TyLeKhoaB = qCPTyLe.Where(p => p.KhoaB == true && p.IDNhom == 12).Sum(p => p.SoLuong);
                cpvc.TyLeKhoaC = qCPTyLe.Where(p => p.KhoaC == true && p.IDNhom == 12).Sum(p => p.SoLuong);


                cpvc.BHKhamBenh = qCPBH.Where(p => p.KB == true && p.NoiTru == 0 && p.IDNhom == 12).Sum(p => p.SoLuong);
                cpvc.BHNoiTruKB = qCPBH.Where(p => p.KB == true && p.NoiTru == 1 && p.IDNhom == 12).Sum(p => p.SoLuong);
                cpvc.BHKhoaA = qCPBH.Where(p => p.KhoaA == true && p.IDNhom == 12).Sum(p => p.SoLuong);
                cpvc.BHKhoaB = qCPBH.Where(p => p.KhoaB == true && p.IDNhom == 12).Sum(p => p.SoLuong);
                cpvc.BHKhoaC = qCPBH.Where(p => p.KhoaC == true && p.IDNhom == 12).Sum(p => p.SoLuong);

                //cpvc.TTKhamBenh = qvp2.Where(p => p.KB == true && p.IDNhom == 12).Sum(p=>p.SoLuong);
                //cpvc.TTKhoaA = qvp2.Where(p => p.KhoaA == true && p.IDNhom == 12).Sum(p=>p.SoLuong);
                //cpvc.TTKhoaB = qvp2.Where(p => p.KhoaB == true && p.IDNhom == 12).Sum(p=>p.SoLuong);
                //cpvc.TTKhoaC = qvp2.Where(p => p.KhoaC == true && p.IDNhom == 12).Sum(p=>p.SoLuong);
                cpvc.TTKhamBenh = cpvc.DichVuKhamBenh + cpvc.BHKhamBenh;
                cpvc.TTNoiTruKB = cpvc.DichVuNoiTruKB + cpvc.BHNoiTruKB;
                cpvc.TTKhoaA = cpvc.DichVuKhoaA + cpvc.BHKhoaA;
                cpvc.TTKhoaB = cpvc.DichVuKhoaB + cpvc.BHKhoaB;
                cpvc.TTKhoaC = cpvc.DichVuKhoaC + cpvc.BHKhoaC;

                //thành tiền

                cpvc.DichVuKhamBenhTT = qCPdichvu.Where(p => p.KB == true && p.NoiTru == 0 && p.IDNhom == 12).Sum(p => p.ThanhTien);
                cpvc.DichVuNoiTruKBTT = qCPdichvu.Where(p => p.KB == true && p.NoiTru == 1 && p.IDNhom == 12).Sum(p => p.ThanhTien);
                cpvc.DichVuKhoaATT = qCPdichvu.Where(p => p.KhoaA == true && p.IDNhom == 12).Sum(p => p.ThanhTien);
                cpvc.DichVuKhoaBTT = qCPdichvu.Where(p => p.KhoaB == true && p.IDNhom == 12).Sum(p => p.ThanhTien);
                cpvc.DichVuKhoaCTT = qCPdichvu.Where(p => p.KhoaC == true && p.IDNhom == 12).Sum(p => p.ThanhTien);


                cpvc.TyLeKhamBenhTT = qCPTyLe.Where(p => p.KB == true && p.NoiTru == 0 && p.IDNhom == 12).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KB == true && p.NoiTru ==0 && p.IDNhom == 12).Sum(p => p.TienBH);
                cpvc.TyLeNoiTruKBTT = qCPTyLe.Where(p => p.KB == true && p.NoiTru == 1 && p.IDNhom == 12).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KB == true && p.NoiTru == 1 && p.IDNhom == 12).Sum(p => p.TienBH);
                cpvc.TyLeKhoaATT = qCPTyLe.Where(p => p.KhoaA == true && p.IDNhom == 12).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KhoaA == true && p.IDNhom == 12).Sum(p => p.TienBH);
                cpvc.TyLeKhoaBTT = qCPTyLe.Where(p => p.KhoaB == true && p.IDNhom == 12).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KhoaB == true && p.IDNhom == 12).Sum(p => p.TienBH);
                cpvc.TyLeKhoaCTT = qCPTyLe.Where(p => p.KhoaC == true && p.IDNhom == 12).Sum(p => p.ThanhTien) - qCPTyLe.Where(p => p.KhoaC == true && p.IDNhom == 12).Sum(p => p.TienBH);


                cpvc.BHKhamBenhTT = qCPBH.Where(p => p.KB == true && p.NoiTru == 0 && p.IDNhom == 12).Sum(p => p.TienBH);
                cpvc.BHNoiTruKBTT = qCPBH.Where(p => p.KB == true && p.NoiTru == 1 && p.IDNhom == 12).Sum(p => p.TienBH);
                cpvc.BHKhoaATT = qCPBH.Where(p => p.KhoaA == true && p.IDNhom == 12).Sum(p => p.TienBH);
                cpvc.BHKhoaBTT = qCPBH.Where(p => p.KhoaB == true && p.IDNhom == 12).Sum(p => p.TienBH);
                cpvc.BHKhoaCTT = qCPBH.Where(p => p.KhoaC == true && p.IDNhom == 12).Sum(p => p.TienBH);

                cpvc.TTKhamBenhTT = qvp2.Where(p => p.KB == true && p.NoiTru == 0 && p.IDNhom == 12).Sum(p => p.ThanhTien);
                cpvc.TTNoiTruKBTT = qvp2.Where(p => p.KB == true && p.NoiTru == 1 && p.IDNhom == 12).Sum(p => p.ThanhTien);
                cpvc.TTKhoaATT = qvp2.Where(p => p.KhoaA == true && p.IDNhom == 12).Sum(p => p.ThanhTien);
                cpvc.TTKhoaBTT = qvp2.Where(p => p.KhoaB == true && p.IDNhom == 12).Sum(p => p.ThanhTien);
                cpvc.TTKhoaCTT = qvp2.Where(p => p.KhoaC == true && p.IDNhom == 12).Sum(p => p.ThanhTien);
                _list.Add(cpvc);
                #endregion

                #region tsba
                BC tsba = new BC();
                tsba.TenNhom = "TSBA";
                tsba.TenTieuChi = "";
                tsba.Row = count;
                _list.Add(tsba);
                #endregion

                for (int i = 1; i < 3; i++)
                {
                    BaoCao.rep_BaoCaoThuVienPhiThang rep = new BaoCao.rep_BaoCaoThuVienPhiThang(i);
                    frmIn frm = new frmIn();
                    rep.TenKhoa1.Value = lupKhoaA.Text;
                    rep.TenKhoa2.Value = lupKhoaB.Text;
                    rep.TenKhoa3.Value = lupKhoaC.Text;
                    if (i == 1)
                    {
                        if (soluot.DichVuKhamBenh != null)
                            rep.cel11.Text = soluot.DichVuKhamBenh.Value.ToString("###.##");
                        if (soluot.DichVuKhoaA != null)
                            rep.cel12.Text = soluot.DichVuKhoaA.Value.ToString("###.##");
                        if (soluot.DichVuKhoaB != null)
                            rep.cel13.Text = soluot.DichVuKhoaB.Value.ToString("###.##");
                        if (soluot.DichVuKhoaC != null)
                            rep.cel14.Text = soluot.DichVuKhoaC.Value.ToString("###.##");
                        if (soluot.TyLeKhamBenh != null)
                            rep.cel15.Text = soluot.TyLeKhamBenh.Value.ToString("###.##");
                        if (soluot.TyLeKhoaA != null)
                            rep.cel16.Text = soluot.TyLeKhoaA.Value.ToString("###.##");
                        if (soluot.TyLeKhoaB != null)
                            rep.cel17.Text = soluot.TyLeKhoaB.Value.ToString("###.##");
                        if (soluot.TyLeKhoaC != null)
                            rep.cel18.Text = soluot.TyLeKhoaC.Value.ToString("###.##");
                        if (soluot.DichVuNoiTruKB != null)
                            rep.cel19.Text = soluot.DichVuNoiTruKB.Value.ToString("###.##");
                        if (soluot.TyLeNoiTruKB != null)
                            rep.cel110.Text = soluot.TyLeNoiTruKB.Value.ToString("###.##");


                        if (songaydt.DichVuKhamBenh != null)
                            rep.cel21.Text = songaydt.DichVuKhamBenh.Value.ToString("###.##");
                        if (songaydt.DichVuKhoaA != null)
                            rep.cel22.Text = songaydt.DichVuKhoaA.Value.ToString("###.##");
                        if (songaydt.DichVuKhoaB != null)
                            rep.cel23.Text = songaydt.DichVuKhoaB.Value.ToString("###.##");
                        if (songaydt.DichVuKhoaC != null)
                            rep.cel24.Text = songaydt.DichVuKhoaC.Value.ToString("###.##");
                        if (songaydt.TyLeKhamBenh != null)
                            rep.cel25.Text = songaydt.TyLeKhamBenh.Value.ToString("###.##");
                        if (songaydt.TyLeKhoaA != null)
                            rep.cel26.Text = songaydt.TyLeKhoaA.Value.ToString("###.##");
                        if (songaydt.TyLeKhoaB != null)
                            rep.cel27.Text = songaydt.TyLeKhoaB.Value.ToString("###.##");
                        if (songaydt.TyLeKhoaC != null)
                            rep.cel28.Text = songaydt.TyLeKhoaC.Value.ToString("###.##");
                       
                    }
                    else
                    {
                        if (soluot.BHKhamBenh != null)
                            rep.cel11.Text = soluot.BHKhamBenh.Value.ToString("###.##");
                        if (soluot.BHKhoaA != null)
                            rep.cel12.Text = soluot.BHKhoaA.Value.ToString("###.##");
                        if (soluot.BHKhoaB != null)
                            rep.cel13.Text = soluot.BHKhoaB.Value.ToString("###.##");
                        if (soluot.BHKhoaC != null)
                            rep.cel14.Text = soluot.BHKhoaC.Value.ToString("###.##");
                        if (soluot.TTKhamBenh != null)
                            rep.cel15.Text = soluot.TTKhamBenh.Value.ToString("###.##");
                        if (soluot.TTKhoaA != null)
                            rep.cel16.Text = soluot.TTKhoaA.Value.ToString("###.##");
                        if (soluot.TTKhoaB != null)
                            rep.cel17.Text = soluot.TTKhoaB.Value.ToString("###.##");
                        if (soluot.TTKhoaC != null)
                            rep.cel18.Text = soluot.TTKhoaC.Value.ToString("###.##");
                        if (soluot.BHNoiTruKB != null)
                            rep.cel19.Text = soluot.BHNoiTruKB.Value.ToString("###.##");
                        if (soluot.TTNoiTruKB != null)
                            rep.cel110.Text = soluot.TTNoiTruKB.Value.ToString("###.##");

                        if (songaydt.BHKhamBenh != null)
                            rep.cel21.Text = songaydt.BHKhamBenh.Value.ToString("###.##");
                        if (songaydt.BHKhoaA != null)
                            rep.cel22.Text = songaydt.BHKhoaA.Value.ToString("###.##");
                        if (songaydt.BHKhoaB != null)
                            rep.cel23.Text = songaydt.BHKhoaB.Value.ToString("###.##");
                        if (songaydt.BHKhoaC != null)
                            rep.cel24.Text = songaydt.BHKhoaC.Value.ToString("###.##");
                        if (songaydt.TTKhamBenh != null)
                            rep.cel25.Text = songaydt.TTKhamBenh.Value.ToString("###.##");
                        if (songaydt.TTKhoaA != null)
                            rep.cel26.Text = songaydt.TTKhoaA.Value.ToString("###.##");
                        if (songaydt.TTKhoaB != null)
                            rep.cel27.Text = songaydt.TTKhoaB.Value.ToString("###.##");
                        if (songaydt.TTKhoaC != null)
                            rep.cel28.Text = songaydt.TTKhoaC.Value.ToString("###.##");
                    }



                    rep.mau.Value = i.ToString();
                    rep.DataSource = _list;
                    //rep.celNgayThang.Text = "Từ ngày " + lupTuNgay.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupDenNgay.DateTime.ToString("dd/MM/yyyy");
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }

                BaoCao.rep_BaoCaoThuVienPhiThang_ChuKy rep2 = new BaoCao.rep_BaoCaoThuVienPhiThang_ChuKy();
                frmIn frm2 = new frmIn();
                rep2.lblKhoaA.Text = lupKhoaA.Text;
                rep2.lblKhoaB.Text = lupKhoaB.Text;
                rep2.lblKhoaC.Text = lupKhoaC.Text;
                rep2.celThuKB.Text = string.Format(DungChung.Bien.FormatString[1], _list.Sum(p => p.TTKhamBenhTT) + _list.Sum(p=>p.TTNoiTruKBTT));
                rep2.celThuKhoaA.Text = string.Format(DungChung.Bien.FormatString[1], _list.Sum(p => p.TTKhoaATT));
                rep2.celThuKhoaB.Text = string.Format(DungChung.Bien.FormatString[1], _list.Sum(p => p.TTKhoaBTT));
                rep2.celThuKhoaC.Text = string.Format(DungChung.Bien.FormatString[1], _list.Sum(p => p.TTKhoaCTT));
                rep2.celTongThu.Text = string.Format(DungChung.Bien.FormatString[1], _list.Sum(p => (p.TTKhamBenhTT + p.TTNoiTruKBTT + p.TTKhoaATT + p.TTKhoaBTT + p.TTKhoaCTT)));

                rep2.celThuTrucTiep.Text = string.Format(DungChung.Bien.FormatString[1], _list.Sum(p => p.DichVuKhamBenhTT + p.DichVuNoiTruKBTT + p.DichVuKhoaATT + p.DichVuKhoaBTT + p.DichVuKhoaCTT + p.TyLeKhamBenhTT + p.TyLeKhoaATT + p.TyLeKhoaBTT + p.TyLeKhoaCTT + p.TyLeNoiTruKBTT));
                rep2.celThuNoiTru.Text = string.Format(DungChung.Bien.FormatString[1], _list.Sum(p => p.DichVuKhoaATT + p.DichVuKhoaBTT + p.DichVuKhoaCTT + p.DichVuNoiTruKBTT + p.TyLeKhoaATT + p.TyLeKhoaBTT + p.TyLeKhoaCTT + p.TyLeNoiTruKBTT));
                rep2.celThuNgoaiTru.Text = string.Format(DungChung.Bien.FormatString[1], _list.Sum(p => p.DichVuKhamBenhTT + p.TyLeKhamBenhTT));
                rep2.celThuBHYT.Text = string.Format(DungChung.Bien.FormatString[1], _list.Sum(p => p.BHKhamBenhTT + p.BHNoiTruKBTT  + p.BHKhoaATT + p.BHKhoaBTT + p.BHKhoaCTT));
                rep2.celThuBHYTNoiTru.Text = string.Format(DungChung.Bien.FormatString[1], _list.Sum(p => p.BHNoiTruKBTT + p.BHKhoaATT + p.BHKhoaBTT + p.BHKhoaCTT));
                rep2.celThuBHYTNgoaiTru.Text = string.Format(DungChung.Bien.FormatString[1], _list.Sum(p => p.BHKhamBenhTT));

                //rep2.celSoLuotNoiTru.Text = string.Format(DungChung.Bien.FormatString[0], soluot.DichVuKhoaA + soluot.DichVuKhoaB + soluot.DichVuKhoaC + soluot.BHKhoaA + soluot.BHKhoaB + soluot.BHKhoaC);
                //rep2.celSoLuotNgoaiTru.Text = soluot.BHKhamBenh == null ? "" : soluot.BHKhamBenh.ToString();

                rep2.celSoLuotNoiTru.Text = string.Format(DungChung.Bien.FormatString[0],  (soluot.TTKhoaA ??0) + (soluot.TTKhoaB ??0)+ (soluot.TTKhoaC ?? 0));
                rep2.celSoLuotNgoaiTru.Text = soluot.TTKhamBenh == null ? "" : soluot.TTKhamBenh.ToString();

                //rep2.celSoNgayDTNoiTru.Text = (songaydt.DichVuKhoaA + songaydt.DichVuKhoaB + songaydt.DichVuKhoaC + songaydt.BHKhoaA + songaydt.BHKhoaB + songaydt.BHKhoaC).ToString();
                //rep2.celSNDTNgoaitTru.Text = songaydt.BHKhamBenh == null ? "" : songaydt.BHKhamBenh.ToString();

                rep2.celSoNgayDTNoiTru.Text = ( (songaydt.TTKhoaA ??0)+ (songaydt.TTKhoaB )+ (songaydt.TTKhoaC ?? 0)).ToString();
                rep2.celSNDTNgoaitTru.Text = "0"; //songaydt.TTKhamBenh == null ? "" : songaydt.TTKhamBenh.ToString();

                rep2.celSoThuNoiTru.Text = string.Format(DungChung.Bien.FormatString[1], _list.Sum(p =>p.DichVuNoiTruKBTT +  p.DichVuKhoaATT + p.DichVuKhoaBTT + p.DichVuKhoaCTT + p.TyLeNoiTruKBTT + p.TyLeKhoaATT + p.TyLeKhoaBTT + p.TyLeKhoaCTT + p.BHNoiTruKBTT+ p.BHKhoaATT + p.BHKhoaBTT + p.BHKhoaCTT));
                rep2.celSoThuNgoaiTru.Text = string.Format(DungChung.Bien.FormatString[1], _list.Sum(p => p.DichVuKhamBenhTT + p.TyLeKhamBenhTT + p.BHKhamBenhTT));


                rep2.CreateDocument();
                frm2.prcIN.PrintingSystem = rep2.PrintingSystem;
                frm2.ShowDialog();
            }

        }

        private class BC
        {
            public string TenNhom { set; get; }
            public string TenTieuChi { set; get; }

            #region số lần
            // chi phí ngoài danh mục
            public double? DichVuKhamBenh { set; get; }
            public double? DichVuNoiTruKB { set; get; }
            public double? DichVuKhoaA { set; get; }
            public double? DichVuKhoaB { set; get; }
            public double? DichVuKhoaC { set; get; }

            // chi phí trong danh mục mà bệnh nhân đồng chi trả
            public double? TyLeKhamBenh { set; get; }
            public double? TyLeNoiTruKB { set; get; }
            public double? TyLeKhoaA { set; get; }
            public double? TyLeKhoaB { set; get; }
            public double? TyLeKhoaC { set; get; }

            //chi phí trong danh mục bảo hiểm chi trả
            public double? BHKhamBenh { set; get; }
            public double? BHNoiTruKB { set; get; }
            public double? BHKhoaA { set; get; }
            public double? BHKhoaB { set; get; }
            public double? BHKhoaC { set; get; }

            //tổng chi phí
            public double? TTKhamBenh { set; get; }
            public double? TTNoiTruKB { set; get; }
            public double? TTKhoaA { set; get; }
            public double? TTKhoaB { set; get; }
            public double? TTKhoaC { set; get; }
            #endregion

            #region thành tiền
            // chi phí ngoài danh mục thành tiền
            public double? DichVuKhamBenhTT { set; get; }
            public double? DichVuNoiTruKBTT { set; get; }
            public double? DichVuKhoaATT { set; get; }
            public double? DichVuKhoaBTT { set; get; }
            public double? DichVuKhoaCTT { set; get; }

            // chi phí trong danh mục mà bệnh nhân đồng chi trả
            public double? TyLeKhamBenhTT { set; get; }
            public double? TyLeNoiTruKBTT { set; get; }
            public double? TyLeKhoaATT { set; get; }
            public double? TyLeKhoaBTT { set; get; }
            public double? TyLeKhoaCTT { set; get; }

            //chi phí trong danh mục bảo hiểm chi trả
            public double? BHKhamBenhTT { set; get; }
            public double? BHNoiTruKBTT { set; get; }
            public double? BHKhoaATT { set; get; }
            public double? BHKhoaBTT { set; get; }
            public double? BHKhoaCTT { set; get; }

            //tổng chi phí
            public double? TTKhamBenhTT { set; get; }
            public double? TTNoiTruKBTT { set; get; }
            public double? TTKhoaATT { set; get; }
            public double? TTKhoaBTT { set; get; }
            public double? TTKhoaCTT { set; get; }
            #endregion

            public int Row { get; set; }

          
        }


        List<DichVu> _ldv = new List<DichVu>();
        List<KPhong> _lKphong = new List<KPhong>();

        private void frm_80ct_Load(object sender, EventArgs e)
        {

            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _ldv = _dataContext.DichVus.ToList();
            _lKphong = _dataContext.KPhongs
            .Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám" || p.PLoai == DungChung.Bien.st_PhanLoaiKP.CanLamSang)
            .ToList();
            _lKphong.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả", });
            lupKhoaphong.Properties.DataSource = _lKphong;
            lupKhoaphong.EditValue = 0;
            lupNgaytu.EditValue = System.DateTime.Now.Date;
            lupngayden.EditValue = System.DateTime.Now.Date;
            List<CanBo> _lcanbo = new List<CanBo>();
            _lcanbo = (from cb in _dataContext.CanBoes
                       join kp in _dataContext.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KeToan) on cb.MaKP equals kp.MaKP
                       select cb).ToList();
            _lcanbo.Insert(0, new CanBo { MaCB = "", TenCB = "Tất cả" });
            lupCanBoTT.Properties.DataSource = _lcanbo;
            lupCanBoTT.EditValue = lupCanBoTT.Properties.GetKeyValueByDisplayText("Tất cả");
            rdTrongBH.SelectedIndex = 2;

            var qkp = _dataContext.KPhongs
             .Where(p => p.PLoai == "Lâm sàng")
             .ToList();
            lupKhoaA.Properties.DataSource = qkp;
            lupKhoaB.Properties.DataSource = qkp;
            lupKhoaC.Properties.DataSource = qkp;

            ckBH.Checked = true;
            ckDichVu.Checked = true;
            ckKSK.Checked = true;
            ckNgoaiTru.Checked = true;
            ckNoiTru.Checked = true;
            ckDTNgoaiTru.Checked = true;

        }
        class MyObject
        {
            public string value { set; get; }
            public string Text { set; get; }
        }
        private bool kt()
        {
            if (string.IsNullOrEmpty(lupNgaytu.Text))
            {
                MessageBox.Show("Bạn hãy chọn ngày tháng");
                lupNgaytu.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(lupngayden.Text))
            {
                MessageBox.Show("Bạn hãy chọn ngày tháng");
                lupngayden.Focus();
                return false;
            }
            else if ((lupngayden.DateTime - lupNgaytu.DateTime).Days < 0)
            {
                MessageBox.Show("Ngày đến phải lớn hơn hoặc bằng ngày từ");
                lupngayden.Focus();
                return false;
            }
            if (lupKhoaA.EditValue == null)
            {
                MessageBox.Show("Bạn hãy chọn khoa A");
                lupKhoaA.Focus();
                return false;
            }
            if (lupKhoaB.EditValue == null)
            {
                MessageBox.Show("Bạn hãy chọn khoa B");
                lupKhoaB.Focus();
                return false;
            }
            if (lupKhoaC.EditValue == null)
            {
                MessageBox.Show("Bạn hãy chọn khoa C");
                lupKhoaC.Focus();
                return false;
            }
            return true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
