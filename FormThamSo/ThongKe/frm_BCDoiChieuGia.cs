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
    public partial class frm_BCDoiChieuGia : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCDoiChieuGia()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            DateTime tungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupngayden.DateTime);
            DateTime ngayGiaMoi = DungChung.Ham.NgayTu(dtNgayGiaMoi.DateTime);
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<int> _lkp = new List<int>();
            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                if (cklKP.GetItemChecked(i))
                {
                    _lkp.Add(Convert.ToInt32(cklKP.GetItemValue(i)));
                }
            }
            List<string> _ldt = new List<string>();
            for (int i = 0; i < dsdt.ItemCount; i++)
            {
                if (dsdt.GetItemChecked(i))
                {
                    _ldt.Add(Convert.ToString(dsdt.GetItemValue(i)));
                }
            }

            string strthang = tungay.Month.ToString();
            DateTime ktra = tungay;
            while (ktra <= denngay.AddMonths(-1))
            {
                ktra = ktra.AddMonths(1);
                strthang = strthang + "," + ktra.Month.ToString();
            }
            string loai = "";
            if (ckDTnt.Checked && ckDTngt.Checked == false && ckKBNgt.Checked == false)
                loai = "nội";
            else if (!ckDTnt.Checked && (ckDTngt.Checked || ckKBNgt.Checked))
                loai = "ngoại";

            var qtn = (from dv in data.DichVus.Where(p => p.PLoai == 2)
                       join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                       join n in data.NhomDVs on tn.IDNhom equals n.IDNhom
                       select new { tn.IdTieuNhom, dv.MaQD, dv.MaDV, dv.TenDV, tn.TenRG, DonGia2 = dv.DonGia, DonGiaDV = dv.DonGia2, dv.DonGiaBHYT, n.TenNhomCT, tn.TenTN, n.IDNhom, dv.DonGiaTT15, dv.DonGiaTT39 }).ToList();


            var qvp = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                       join bn in data.BenhNhans.Where(parameters => parameters.DTuong == "BHYT").Where(p => (p.NoiTru == 1 && ckDTnt.Checked) || (p.NoiTru == 0 && p.DTNT == false && ckKBNgt.Checked) || (p.NoiTru == 0 && p.DTNT == true && ckDTngt.Checked)) on vp.MaBNhan equals bn.MaBNhan
                       join vpct in data.VienPhicts.Where(p => p.TrongBH == 1) on vp.idVPhi equals vpct.idVPhi
                       // join dv in data.DichVus.Where(p=>p.PLoai == 2) on vpct.MaDV equals dv.MaDV                       
                       select new { bn.MaBNhan, bn.NoiTru, bn.NoiTinh, bn.Tuyen, vpct.SoLuong, vpct.DonGia,vpct.ThanhTien, vpct.MaKP, vpct.TyLeTT, vp.NgayTT, vpct.MaDV, bn.MaDTuong }).ToList();


            var q1 = (from kp in _lkp
                      join vp in qvp on kp equals vp.MaKP
                      join dtuong in _ldt on vp.MaDTuong equals dtuong
                      join tn in qtn on vp.MaDV equals tn.MaDV
                      select new { tn.IDNhom, tn.TenTN, vp.MaBNhan, vp.NoiTru, vp.NoiTinh, vp.NgayTT, vp.TyLeTT, vp.Tuyen, MaDV = vp.MaDV ?? 0, tn.MaQD, tn.TenDV, vp.SoLuong, vp.DonGia,vp.ThanhTien, tn.DonGiaDV, vp.MaKP, tn.DonGia2, tn.DonGiaBHYT, tn.DonGiaTT15,tn.DonGiaTT39, tn.IdTieuNhom, tn.TenRG, tn.TenNhomCT }).ToList();

            List<BC> _lbc = new List<BC>();
            if (rdMau.SelectedIndex == 2 || rdMau.SelectedIndex == 3 || rdMau.SelectedIndex == 4 || rdMau.SelectedIndex == 5)
            {
                _lbc = (from a in q1.Where(p => p.TenNhomCT != "Khám bệnh")
                        group a by new { a.NoiTinh, a.MaDV, a.MaQD, a.TenDV, a.DonGia2,a.DonGia, a.DonGiaBHYT, a.DonGiaDV,a.ThanhTien, a.DonGiaTT15,a.DonGiaTT39, a.NoiTru, a.TenTN, a.IDNhom, TyLeTT = (double)a.TyLeTT / 100 } into kq
                        select new BC
                        {
                            TenTN = kq.Key.TenTN,
                            PLoai = (kq.Key.IDNhom == 15 || kq.Key.IDNhom == 14) ? 1 : (kq.Key.IDNhom == 1 ? 2 : (kq.Key.IDNhom == 2 ? 3 : (kq.Key.IDNhom == 8 ? 4 : 5))),
                            noitru = kq.Key.NoiTru,
                            MaDV = kq.Key.MaDV,
                            MaQD = kq.Key.MaQD,
                            TenDV = kq.Key.TenDV,
                            NoiTinh = kq.Key.NoiTinh,
                            dongia2712 = kq.Key.DonGia * kq.Key.TyLeTT,
                            thanhtien2712 = kq.Sum(p => p.SoLuong * (kq.Key.DonGia * kq.Key.TyLeTT)),
                            DonGia2 = kq.Key.TyLeTT == 1 ? kq.Key.DonGia2 : 0,
                            DonGiaDV = kq.Key.TyLeTT * kq.Key.DonGiaDV,
                            DonGiaBHYT = kq.Key.DonGiaBHYT * kq.Key.TyLeTT,
                            DonGiaTT15 = kq.Key.DonGiaTT15 * kq.Key.TyLeTT,
                            DonGiaTT39 = kq.Key.DonGiaTT39 * kq.Key.TyLeTT,
                            PhanLoai = (kq.Key.IDNhom == 15 || kq.Key.IDNhom == 14) ? "B" : (kq.Key.IDNhom == 1 ? "C" : (kq.Key.IDNhom == 2 ? "D" : (kq.Key.IDNhom == 8 ? "E" : (kq.Key.IDNhom == 12 ? "F" : (kq.Key.IDNhom == 9 ? "G" : "H"))))),
                            SoLuong = kq.Sum(p => p.SoLuong),
                            SoLuongNT = kq.Where(p => p.NoiTru == 1).Sum(p => p.SoLuong),
                            SoLuongNgT = kq.Where(p => p.NoiTru == 0).Sum(p => p.SoLuong),
                            ThanhTien2 = kq.Sum(p => p.SoLuong * (kq.Key.TyLeTT == 1 ? kq.Key.DonGia2 : 0)),
                            ThanhTien2NT = kq.Where(p => p.NoiTru == 1).Sum(p => p.SoLuong * (kq.Key.TyLeTT == 1 ? kq.Key.DonGia2 : 0)),
                            ThanhTien2NgT = kq.Where(p => p.NoiTru == 0).Sum(p => p.SoLuong * (kq.Key.TyLeTT == 1 ? kq.Key.DonGia2 : 0)),
                            ThanhTienDV = kq.Sum(p => p.SoLuong * (kq.Key.TyLeTT * kq.Key.DonGiaDV)),
                            ThanhTienDVNT = kq.Where(p => p.NoiTru == 1).Sum(p => p.SoLuong * (kq.Key.TyLeTT * kq.Key.DonGiaDV)),
                            ThanhTienDVNgT = kq.Where(p => p.NoiTru == 0).Sum(p => p.SoLuong * (kq.Key.TyLeTT * kq.Key.DonGiaDV)),
                            ThanhTienBHYT = kq.Sum(p => p.SoLuong * (kq.Key.DonGiaBHYT * kq.Key.TyLeTT)),
                            ThanhTienBHYTNT = kq.Where(p => p.NoiTru == 1).Sum(p => p.SoLuong * (kq.Key.DonGiaBHYT * kq.Key.TyLeTT)),
                            ThanhTienBHYTNgT = kq.Where(p => p.NoiTru == 0).Sum(p => p.SoLuong * (kq.Key.DonGiaBHYT * kq.Key.TyLeTT)),
                            ThanhTienTT15 = kq.Sum(p => p.SoLuong * (kq.Key.DonGiaTT15 * kq.Key.TyLeTT)),
                            ThanhTienTT15NT = kq.Where(p => p.NoiTru == 1).Sum(p => p.SoLuong * (kq.Key.DonGiaTT15 * kq.Key.TyLeTT)),
                            ThanhTienTT15NgT = kq.Where(p => p.NoiTru == 0).Sum(p => p.SoLuong * (kq.Key.DonGiaTT15 * kq.Key.TyLeTT)),
                            ThanhTienTT39 = kq.Sum(p => p.SoLuong * (kq.Key.DonGiaTT39 * kq.Key.TyLeTT)),
                            ThanhTienTT39NT = kq.Where(p => p.NoiTru == 1).Sum(p => p.SoLuong * (kq.Key.DonGiaTT39 * kq.Key.TyLeTT)),
                            ThanhTienTT39NgT = kq.Where(p => p.NoiTru == 0).Sum(p => p.SoLuong * (kq.Key.DonGiaTT39 * kq.Key.TyLeTT)),
                            
                            Thang = strthang,
                            Loai = kq.Key.NoiTru == 1 ? "Nội trú" : "Ngoại trú",
                            tuyen = "tuyến " + kq.Key.NoiTinh
                        }).OrderBy(p => p.PLoai).ThenBy(p => p.Loai).ThenByDescending(p => p.TenDV).ToList();


                var qCongKham = (from a in q1.Where(p => p.TenNhomCT == "Khám bệnh")
                                 select new { TenTN = a.TenTN,a.DonGia, NoiTru = a.NoiTru, MaDV = a.MaDV, MaQD = a.MaQD, TenDV = a.TenDV, NoiTinh = a.NoiTinh, DonGia2 = a.DonGia2, DonGiaDV = a.DonGiaDV, DonGiaBHYT = a.DonGiaBHYT, a.DonGiaTT15,a.DonGiaTT39, SoLuong = a.SoLuong, SoLuongNT = a.NoiTru == 1 ? a.SoLuong : 0, SoLuongNgT = a.NoiTru == 0 ? a.SoLuong : 0, TyLeTT = (double)a.TyLeTT / 100 }).ToList(); //( (a.NgayTT >= ngayGiaMoi) ? (a.DonGia == a.DonGiaBHYT ? 100 : (double)(a.DonGia/a.DonGiaBHYT) ): (a.DonGia == a.DonGia2 ? 100 : (double)(a.DonGia/a.DonGia2) ))}).ToList();

                var qCongKham2 = (from a in qCongKham select new { TenTN = a.TenTN,a.DonGia,a.TyLeTT, NoiTru = a.NoiTru, MaDV = a.MaDV, MaQD = a.MaQD, TenDV = a.TenDV, NoiTinh = a.NoiTinh, DonGia2 = a.TyLeTT == 1 ? a.DonGia2 : 0, DonGiaDV = a.TyLeTT * a.DonGiaDV, DonGiaBHYT = a.DonGiaBHYT * a.TyLeTT, DonGiaTT15 = a.DonGiaTT15 * a.TyLeTT, DonGiaTT39 = a.DonGiaTT39 * a.TyLeTT, SoLuong = a.SoLuong, SoLuongNT = a.SoLuongNT, SoLuongNgT = a.SoLuongNgT }).ToList();
                List<BC> _lCongKham = new List<BC>();
                _lCongKham = (from a in qCongKham2
                              group a by new { a.NoiTinh, a.MaDV, a.MaQD, a.TenDV,a.TyLeTT,a.DonGia, a.DonGia2, a.DonGiaDV, a.DonGiaBHYT, a.DonGiaTT15,a.DonGiaTT39, a.NoiTru, a.TenTN } into kq
                              select new BC
                              {
                                  TenTN = kq.Key.TenTN,
                                  PLoai = 0,
                                  noitru = kq.Key.NoiTru,
                                  MaDV = kq.Key.MaDV,
                                  MaQD = kq.Key.MaQD,
                                  TenDV = kq.Key.TenDV,
                                  dongia2712 = kq.Key.DonGia * kq.Key.TyLeTT,
                                  thanhtien2712 = kq.Sum(p => p.SoLuong * (kq.Key.DonGia * kq.Key.TyLeTT)),
                                  NoiTinh = kq.Key.NoiTinh,
                                  DonGia2 = kq.Key.DonGia2,
                                  DonGiaDV = kq.Key.DonGiaDV,
                                  DonGiaBHYT = kq.Key.DonGiaBHYT,
                                  DonGiaTT15 = kq.Key.DonGiaTT15,
                                  DonGiaTT39 = kq.Key.DonGiaTT39,
                                  PhanLoai = "A",
                                  SoLuong = kq.Sum(p => p.SoLuongNT) != 0 ? kq.Sum(p => p.SoLuongNT) : kq.Sum(p => p.SoLuongNgT),
                                  SoLuongNT = kq.Sum(p => p.SoLuongNT),
                                  SoLuongNgT = kq.Sum(p => p.SoLuongNgT),
                                  ThanhTien2 = kq.Sum(p => p.SoLuong * p.DonGia2),
                                  ThanhTien2NT = kq.Sum(p => p.SoLuongNT * p.DonGia2),
                                  ThanhTien2NgT = kq.Sum(p => p.SoLuongNgT * p.DonGia2),
                                  ThanhTienDV = kq.Sum(p => p.SoLuong * p.DonGiaDV),
                                  ThanhTienDVNT = kq.Sum(p => p.SoLuongNT * p.DonGiaDV),
                                  ThanhTienDVNgT = kq.Sum(p => p.SoLuongNgT * p.DonGiaDV),
                                  ThanhTienBHYT = kq.Sum(p => p.SoLuong * p.DonGiaBHYT),
                                  ThanhTienBHYTNT = kq.Sum(p => p.SoLuongNT * p.DonGiaBHYT),
                                  ThanhTienBHYTNgT = kq.Sum(p => p.SoLuongNgT * p.DonGiaBHYT),
                                  ThanhTienTT15 = kq.Sum(p => p.SoLuong * p.DonGiaTT15),
                                  ThanhTienTT15NT = kq.Sum(p => p.SoLuongNT * p.DonGiaTT15),
                                  ThanhTienTT15NgT = kq.Sum(p => p.SoLuongNgT * p.DonGiaTT15),
                                  ThanhTienTT39 = kq.Sum(p => p.SoLuong * p.DonGiaTT39),
                                  ThanhTienTT39NT = kq.Sum(p => p.SoLuongNT * p.DonGiaTT39),
                                  ThanhTienTT39NgT = kq.Sum(p => p.SoLuongNgT * p.DonGiaTT39),
                                  Thang = strthang,
                                  Loai = kq.Key.NoiTru == 1 ? "Nội trú" : "Ngoại trú",
                                  tuyen = "tuyến " + kq.Key.NoiTinh
                              }).OrderBy(p => p.MaQD).ToList();

                _lbc.AddRange(_lCongKham);
            }
            else
            {
                _lbc = (from a in q1.Where(p => p.TenNhomCT != "Khám bệnh")
                        group a by new { a.NoiTinh, a.MaDV, a.MaQD, a.TenDV, a.DonGia2, a.DonGiaBHYT, a.DonGiaDV, a.TenTN } into kq
                        select new BC
                        {
                            TenTN = kq.Key.TenTN,
                            MaDV = kq.Key.MaDV,
                            MaQD = kq.Key.MaQD,
                            TenDV = kq.Key.TenDV,
                            NoiTinh = kq.Key.NoiTinh,
                            DonGia2 = kq.Key.DonGia2,
                            DonGiaDV = kq.Key.DonGiaDV,
                            DonGiaBHYT = kq.Key.DonGiaBHYT,
                            PhanLoai = "B",
                            SoLuong = kq.Sum(p => p.SoLuong),
                            SoLuongNT = kq.Where(p => p.NoiTru == 1).Sum(p => p.SoLuong),
                            SoLuongNgT = kq.Where(p => p.NoiTru == 0).Sum(p => p.SoLuong),
                            ThanhTien2 = kq.Sum(p => p.SoLuong * p.DonGia2),
                            ThanhTien2NT = kq.Where(p => p.NoiTru == 1).Sum(p => p.SoLuong * p.DonGia2),
                            ThanhTien2NgT = kq.Where(p => p.NoiTru == 0).Sum(p => p.SoLuong * p.DonGia2),
                            ThanhTienDV = kq.Sum(p => p.SoLuong * p.DonGiaDV),
                            ThanhTienDVNT = kq.Where(p => p.NoiTru == 1).Sum(p => p.SoLuong * p.DonGiaDV),
                            ThanhTienDVNgT = kq.Where(p => p.NoiTru == 0).Sum(p => p.SoLuong * p.DonGiaDV),
                            ThanhTienBHYT = kq.Sum(p => p.SoLuong * p.DonGiaBHYT),
                            ThanhTienBHYTNT = kq.Where(p => p.NoiTru == 1).Sum(p => p.SoLuong * p.DonGiaBHYT),
                            ThanhTienBHYTNgT = kq.Where(p => p.NoiTru == 0).Sum(p => p.SoLuong * p.DonGiaBHYT),
                            Thang = strthang,
                            Loai = loai,
                            tuyen = "tuyến " + kq.Key.NoiTinh
                        }).OrderBy(p => p.TenDV).ToList();


                var qCongKham = (from a in q1.Where(p => p.TenNhomCT == "Khám bệnh")
                                 select new { TenTN = a.TenTN, MaDV = a.MaDV, MaQD = a.MaQD, TenDV = a.TenDV, NoiTinh = a.NoiTinh, DonGia2 = a.DonGia2, DonGiaDV = a.DonGiaDV, DonGiaBHYT = a.DonGiaBHYT, SoLuong = a.SoLuong, SoLuongNT = a.NoiTru == 1 ? a.SoLuong : 0, SoLuongNgT = a.NoiTru == 0 ? a.SoLuong : 0, TyLeTT = (double)a.TyLeTT / 100 }).ToList(); //( (a.NgayTT >= ngayGiaMoi) ? (a.DonGia == a.DonGiaBHYT ? 100 : (double)(a.DonGia/a.DonGiaBHYT) ): (a.DonGia == a.DonGia2 ? 100 : (double)(a.DonGia/a.DonGia2) ))}).ToList();

                var qCongKham2 = (from a in qCongKham select new { TenTN = a.TenTN, MaDV = a.MaDV, MaQD = a.MaQD, TenDV = a.TenDV, NoiTinh = a.NoiTinh, DonGia2 = a.TyLeTT == 1 ? a.DonGia2 : 0, DonGiaDV = a.TyLeTT * a.DonGiaDV, DonGiaBHYT = a.DonGiaBHYT * a.TyLeTT, SoLuong = a.SoLuong, SoLuongNT = a.SoLuongNT, SoLuongNgT = a.SoLuongNgT }).ToList();
                List<BC> _lCongKham = new List<BC>();
                _lCongKham = (from a in qCongKham2
                              group a by new { a.NoiTinh, a.MaDV, a.MaQD, a.TenDV, a.DonGia2, a.DonGiaDV, a.DonGiaBHYT, a.TenTN } into kq
                              select new BC
                              {
                                  TenTN = kq.Key.TenTN,
                                  MaDV = kq.Key.MaDV,
                                  MaQD = kq.Key.MaQD,
                                  TenDV = kq.Key.TenDV,
                                  NoiTinh = kq.Key.NoiTinh,
                                  DonGia2 = kq.Key.DonGia2,
                                  DonGiaDV = kq.Key.DonGiaDV,
                                  DonGiaBHYT = kq.Key.DonGiaBHYT,
                                  PhanLoai = "A",
                                  SoLuong = kq.Sum(p => p.SoLuong),
                                  SoLuongNT = kq.Sum(p => p.SoLuongNT),
                                  SoLuongNgT = kq.Sum(p => p.SoLuongNgT),
                                  ThanhTien2 = kq.Sum(p => p.SoLuong * p.DonGia2),
                                  ThanhTien2NT = kq.Sum(p => p.SoLuongNT * p.DonGia2),
                                  ThanhTien2NgT = kq.Sum(p => p.SoLuongNgT * p.DonGia2),
                                  ThanhTienDV = kq.Sum(p => p.SoLuong * p.DonGiaDV),
                                  ThanhTienDVNT = kq.Sum(p => p.SoLuongNT * p.DonGiaDV),
                                  ThanhTienDVNgT = kq.Sum(p => p.SoLuongNgT * p.DonGiaDV),
                                  ThanhTienBHYT = kq.Sum(p => p.SoLuong * p.DonGiaBHYT),
                                  ThanhTienBHYTNT = kq.Sum(p => p.SoLuongNT * p.DonGiaBHYT),
                                  ThanhTienBHYTNgT = kq.Sum(p => p.SoLuongNgT * p.DonGiaBHYT),
                                  Thang = strthang,
                                  Loai = loai,
                                  tuyen = "tuyến " + kq.Key.NoiTinh
                              }).OrderBy(p => p.MaQD).ToList();

                _lbc.AddRange(_lCongKham);
            }

            foreach (BC a in _lbc)
            {
                if (rdMau.SelectedIndex != 3 && rdMau.SelectedIndex != 4 && rdMau.SelectedIndex != 5)
                {
                    a.ChenhLech = (double)a.ThanhTienBHYT - (double)a.ThanhTien2; // bảo hiểm so với bh cũ
                    a.ChenhLechNT = (double)a.ThanhTienBHYTNT - (double)a.ThanhTien2NT;
                    a.ChenhLechNgT = (double)a.ThanhTienBHYTNgT - (double)a.ThanhTien2NgT;
                    a.ChenhLechDV = (double)a.ThanhTienBHYT - (double)a.ThanhTienDV;// bảo hiểm so với dịch vụ
                    a.ChenhLechDVNT = (double)a.ThanhTienBHYTNT - (double)a.ThanhTienDVNT;
                    a.ChenhLechDVNgT = (double)a.ThanhTienBHYTNgT - (double)a.ThanhTienDVNgT;
                }
                else
                {
                    a.ChenhLech = (double)a.ThanhTienTT15 - (double)a.ThanhTien2; // bảo hiểm so với bh cũ
                    a.ChenhLechNT = (double)a.ThanhTienTT15NT - (double)a.ThanhTien2NT;
                    a.ChenhLechNgT = (double)a.ThanhTienTT15NgT - (double)a.ThanhTien2NgT;
                    a.ChenhLechDV = (double)a.ThanhTienTT15 - (double)a.ThanhTienBHYT;// bảo hiểm so với dịch vụ
                    a.ChenhLechDVNT = (double)a.ThanhTienTT15NT - (double)a.ThanhTienBHYTNT;
                    a.ChenhLechDVNgT = (double)a.ThanhTienTT15NgT - (double)a.ThanhTienBHYTNgT;
                    a.ChenhLech39DV = (double)a.thanhtien2712 - (double)a.ThanhTien2; // chênh lệch giữa thông tư 39 và giá khi chưa áp dụng TT37
                    a.ChenhLech39_37 = (double)a.ThanhTienTT39 - (double)a.ThanhTienBHYT; // chênh lệch giữa thông tư 39 và giá TT37
                    a.ChenhLech39_15 = (double)a.ThanhTienTT39 - (double)a.ThanhTienTT15; // chênh lệch giữa thông tư 39 và giá TT37
                }
                //tuyến
            }
            if (rdMau.SelectedIndex == 0)
            {
                BaoCao.rep_BCDoiChieuGia rep = new BaoCao.rep_BCDoiChieuGia();
                frmIn frm2 = new frmIn();
                if (String.IsNullOrEmpty(txtNgayThangHT.Text))
                    rep.lbl_tungaydenngay.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
                else
                    rep.lbl_tungaydenngay.Text = txtNgayThangHT.Text;
                rep.DataSource = _lbc;
                rep.BindingData();
                rep.CreateDocument();
                frm2.prcIN.PrintingSystem = rep.PrintingSystem;
                frm2.ShowDialog();
            }
            else if (rdMau.SelectedIndex == 1)
            {
                BaoCao.rep_BCDoiChieuGia_NoiNgoaiTru rep = new BaoCao.rep_BCDoiChieuGia_NoiNgoaiTru();
                frmIn frm2 = new frmIn();
                if (String.IsNullOrEmpty(txtNgayThangHT.Text))
                    rep.lbl_tungaydenngay.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
                else
                    rep.lbl_tungaydenngay.Text = txtNgayThangHT.Text;
                rep.DataSource = _lbc;
                rep.BindingData();
                rep.CreateDocument();
                frm2.prcIN.PrintingSystem = rep.PrintingSystem;
                frm2.ShowDialog();
            }
            else if (rdMau.SelectedIndex == 2)
            {
                BaoCao.rep_BCDoiChieuGia_20001 rep = new BaoCao.rep_BCDoiChieuGia_20001();
                frmIn frm2 = new frmIn();
                if (String.IsNullOrEmpty(txtNgayThangHT.Text))
                    rep.lbl_tungaydenngay.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
                else
                    rep.lbl_tungaydenngay.Text = txtNgayThangHT.Text;
                _lbc = _lbc.OrderBy(p => p.PLoai).ThenBy(p => p.Loai).ThenByDescending(p => p.TenDV).ToList();
                rep.DataSource = _lbc;
                rep.BindingData();
                rep.CreateDocument();
                frm2.prcIN.PrintingSystem = rep.PrintingSystem;
                frm2.ShowDialog();
            }
            else if (rdMau.SelectedIndex == 3)
            {
                if (DungChung.Bien.MaBV != "20001")
                {
                    BaoCao.rep_BCDoiChieuGia_TT15 rep = new BaoCao.rep_BCDoiChieuGia_TT15();
                    frmIn frm2 = new frmIn();
                    if (String.IsNullOrEmpty(txtNgayThangHT.Text))
                        rep.lbl_tungaydenngay.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
                    else
                        rep.lbl_tungaydenngay.Text = txtNgayThangHT.Text;
                    _lbc = _lbc.OrderBy(p => p.PLoai).ThenBy(p => p.Loai).ThenByDescending(p => p.TenDV).ToList();
                    rep.DataSource = _lbc;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm2.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm2.ShowDialog();
                }
                else
                {
                    BaoCao.rep_BCDoiChieuGia_20001_TT15 rep = new BaoCao.rep_BCDoiChieuGia_20001_TT15();
                    frmIn frm2 = new frmIn();
                    if (String.IsNullOrEmpty(txtNgayThangHT.Text))
                        rep.lbl_tungaydenngay.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
                    else
                        rep.lbl_tungaydenngay.Text = txtNgayThangHT.Text;
                    _lbc = _lbc.OrderBy(p => p.PLoai).ThenBy(p => p.Loai).ThenByDescending(p => p.TenDV).ToList();
                    rep.DataSource = _lbc;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm2.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm2.ShowDialog();
                }
            }
            else if (rdMau.SelectedIndex == 4)
            {
                BaoCao.rep_BCDoiChieuGia_TT39_20001 rep = new BaoCao.rep_BCDoiChieuGia_TT39_20001();
                frmIn frm2 = new frmIn();
                if (String.IsNullOrEmpty(txtNgayThangHT.Text))
                    rep.lbl_tungaydenngay.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
                else
                    rep.lbl_tungaydenngay.Text = txtNgayThangHT.Text;
                _lbc = _lbc.OrderBy(p => p.PLoai).ThenBy(p => p.Loai).ThenByDescending(p => p.TenDV).ToList();
                rep.DataSource = _lbc;
                rep.BindingData();
                rep.CreateDocument();
                frm2.prcIN.PrintingSystem = rep.PrintingSystem;
                frm2.ShowDialog();
            }
            else if (rdMau.SelectedIndex == 5)
            {
                BaoCao.rep_BCDoiChieuGia_TT39_27001 rep = new BaoCao.rep_BCDoiChieuGia_TT39_27001();
                frmIn frm2 = new frmIn();
                if (String.IsNullOrEmpty(txtNgayThangHT.Text))
                    rep.lbl_tungaydenngay.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
                else
                    rep.lbl_tungaydenngay.Text = txtNgayThangHT.Text;
                _lbc = _lbc.OrderBy(p => p.PLoai).ThenBy(p => p.Loai).ThenByDescending(p => p.TenDV).ToList();
                rep.DataSource = _lbc;
                rep.BindingData();
                rep.CreateDocument();
                frm2.prcIN.PrintingSystem = rep.PrintingSystem;
                frm2.ShowDialog();
            }
        }

        private void frm_BCDoiChieuGia_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = DateTime.Now;
            lupngayden.DateTime = DateTime.Now;
            dtNgayGiaMoi.DateTime = new DateTime(2016, 3, 1);
            if (DungChung.Bien.ngayGiaMoi != null)
                dtNgayGiaMoi.DateTime = DungChung.Bien.ngayGiaMoi;
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var qkp = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).ToList();
            qkp.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
            cklKP.DataSource = qkp;
            cklKP.CheckAll();
            var ds = data.DTuongs.ToList();
            ds.Insert(0, new DTuong { MaDTuong = "Tất cả" });
            dsdt.DataSource = ds;
            dsdt.CheckAll();
        }
        public class BC
        {
            public int MaBNhan { set; get; }
            public int PLoai { set; get; }
            public int? NoiTinh { set; get; }
            public int MaDV { get; set; }
            public string TenDV { set; get; }
            public int MaKP { get; set; }
            public string TenRG { get; set; }
            public double SoLuong { set; get; }
            /// <summary>
            /// Đơn giá bảo hiểm cũ
            /// </summary>
            public double DonGia2 { set; get; }
            /// <summary>
            /// đơn giá BH mới
            /// </summary>
            public double DonGiaBHYT { set; get; }
            /// <summary>
            /// đơn giá TT15
            /// </summary>
            public double DonGiaTT15 { set; get; }

            public double ThanhTien2 { set; get; }
            public double ThanhTienBHYT { set; get; }
            public double ThanhTienTT15 { set; get; }
            public double ChenhLech { get; set; }
            public string PhanLoai { set; get; }//A: Công khám; B: dich vụ kỹ thuật
            public string Loai { set; get; }// nội hoặc ngoại trú
            public string Thang { set; get; }

            public string MaQD { get; set; }
            public int? TyLeTT { get; set; }// dùng cho tính công khám
            public string tuyen { get; set; }
            /// <summary>
            /// đơn giá dịch vụ
            /// </summary>
            public double DonGiaDV { get; set; }// dịch vụ

            public double ThanhTienDV { get; set; }// dịch vụ

            public double ChenhLechDV { get; set; }

            public double SoLuongNT { get; set; }

            public double SoLuongNgT { get; set; }

            public double ThanhTien2NT { get; set; }

            public double ThanhTien2NgT { get; set; }

            public double ThanhTienDVNT { get; set; }

            public double ThanhTienDVNgT { get; set; }

            public double ThanhTienBHYTNT { get; set; }

            public double ThanhTienBHYTNgT { get; set; }

            

            public double ThanhTienTT15NT { get; set; }

            public double ThanhTienTT15NgT { get; set; }

            public double ChenhLechNT { get; set; }

            public double ChenhLechNgT { get; set; }

            public double ChenhLechDVNT { get; set; }

            public double ChenhLechDVNgT { get; set; }
            public int? noitru { get; set; }
            public string TenTN { get; set; }

            public double DonGiaTT39 { get; set; }

            public double ThanhTienTT39 { get; set; }

            public double ThanhTienTT39NT { get; set; }

            public double ThanhTienTT39NgT { get; set; }

            public double ChenhLech39DV { get; set; }

            public double ChenhLech39_37 { get; set; }

            public double ChenhLech39_15 { get; set; }

            //hiss 5089
            public double dongia2712 { get; set; }
            public double thanhtien2712 { get; set; }
            
            //end hiss5089
        }

        private void cklKP_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                if (e.State == CheckState.Checked)
                    cklKP.CheckAll();
                else
                    cklKP.UnCheckAll();
            }
        }

        private void dsdt_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                if (e.State == CheckState.Checked)
                    dsdt.CheckAll();
                else
                    dsdt.UnCheckAll();
            }
        }
    }
}