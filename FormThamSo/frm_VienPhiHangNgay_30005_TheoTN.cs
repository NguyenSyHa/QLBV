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
    public partial class frm_VienPhiHangNgay_30005_TheoTN : DevExpress.XtraEditors.XtraForm
    {
        public frm_VienPhiHangNgay_30005_TheoTN()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data;
        private void frm_VienPhiHangNgay_30005_TheoTN_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = DateTime.Now.Date;
            lupngayden.DateTime = DateTime.Now.Date.AddDays(1).AddMilliseconds(-1);

            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
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
            List<MyObject> listTieuNhomDV = new List<MyObject>();
            listTieuNhomDV = (from n in data.NhomDVs.Where(p => p.Status == 2)
                              join tn in data.TieuNhomDVs on n.IDNhom equals tn.IDNhom
                              select new MyObject { Text = tn.TenTN, Value = tn.IdTieuNhom }).ToList();
            listTieuNhomDV.Insert(0, new MyObject { Value = -1, Text = "Tất cả" });
            lupNhomDV.Properties.DataSource = listTieuNhomDV;
            lupNhomDV.EditValue = lupNhomDV.Properties.GetKeyValueByDisplayText("Tất cả");

            cklTieuNhomDV.DataSource = listTieuNhomDV;
            cklTieuNhomDV.DisplayMember = "Text";
            cklTieuNhomDV.ValueMember = "Value";

        }
        #region class VPHangNgay
        public class VPHangNgay
        {
            public string TenBNhan { set; get; }
            public int Tuoi { set; get; }
            public string DChi { set; get; }
            public int MaBNhan { set; get; }
            public DateTime NgayTT { set; get; }
            public string SoHD { set; get; }
            public int IDTamUng { set; get; }

            #region dùng cho mẫu chi tiết
            public double TienTN1 { get; set; }
            public double TienTN2 { get; set; }
            public double TienTN3 { get; set; }
            public double TienTN4 { get; set; }
            public double TienTN5 { get; set; }
            public double TienTN6 { get; set; }
            public double TienTN7 { get; set; }
            public double TienTN8 { get; set; }
            public double TienTN9 { get; set; }
            public double TienTN10 { get; set; }
            public double TienTN11 { get; set; }
            public double TienTN12 { get; set; }
            public double TienTN13 { get; set; }
            public double TienTN14 { get; set; }
            public double TienTN15 { get; set; }
            public double TienTN16 { get; set; }
            public double TienTN17 { get; set; }
            public double TienTN18 { get; set; }
            public double TienTN19 { get; set; }
            public double TienTN20 { get; set; }
            public double TienTN21 { get; set; }
            public double TienTN22 { get; set; }
            public double TienTN23 { get; set; }
            public double TienTN24 { get; set; }
            public double TienTN25 { get; set; }
            public double TienTN26 { get; set; }
            public double TienTN27 { get; set; }
            public double TienTN28 { get; set; }
            public double TienTN29 { get; set; }
            public double TienTN30 { get; set; }
            public double TienTN31 { get; set; }
            public double TienTN32 { get; set; }
            #endregion

            public double Tong { set; get; }
        }
        #endregion

        private class MyObject
        {
            public string Text { set; get; }
            public int Value { set; get; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
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
            List<int> LidTieuNhom = new List<int>();
            //if(lupNhomDV.EditValue != null)
            //{
            //    idnhom = Convert.ToInt32(lupNhomDV.EditValue);
            //}
            for (int i = 1; i < cklTieuNhomDV.ItemCount; i++)
            {
                if (cklTieuNhomDV.GetItemChecked(i) == true)
                    LidTieuNhom.Add(Convert.ToInt32(cklTieuNhomDV.GetItemValue(i)));
            }
            int cp = rdTrongBH.SelectedIndex;

            var qtn = (from tn in data.TieuNhomDVs
                       join n in data.NhomDVs on tn.IDNhom equals n.IDNhom
                       join dv in data.DichVus on tn.IdTieuNhom equals dv.IdTieuNhom
                       where (LidTieuNhom.Contains(tn.IdTieuNhom))
                       //where (rdgNhomDV.SelectedIndex == 3 ? (tn.IDNhom == 1 || tn.IDNhom == 3 || tn.IDNhom == 2) : (rdgNhomDV.SelectedIndex == 0 ? tn.IDNhom == 1 :
                       //          (rdgNhomDV.SelectedIndex == 1 ? tn.IDNhom == 2 ://tn.TenNhomCT.ToLower().Contains("xét nghiệm")tn.TenNhomCT.ToLower().Contains("chẩn đoán hình ảnh")
                       //           tn.IDNhom == 3)))
                       select new { tn.TenRG, tn.IdTieuNhom, n.IDNhom, n.TenNhomCT, tn.TenTN, dv.MaDV }).ToList();
            var qtn30005 = (from tn in qtn
                            select new { tn.TenRG, tn.TenTN, tn.IdTieuNhom }).OrderBy(p => p.IdTieuNhom).Distinct().ToList();

            //BaoCao.rep_VienPhiHangNgay_MauCT_30005_A3 rep30005A31 = new BaoCao.rep_VienPhiHangNgay_MauCT_30005_A3(1);
            //BaoCao.rep_VienPhiHangNgay_MauCT_30005_A3_20 rep20 = new BaoCao.rep_VienPhiHangNgay_MauCT_30005_A3_20(1);
            //BaoCao.rep_VienPhiHangNgay_MauCT_30005_A3_Mid rep30005A32 = new BaoCao.rep_VienPhiHangNgay_MauCT_30005_A3_Mid();
            BaoCao.rep_VienPhiHangNgay_MauCT_30005_InDoc rep30005_indoc = new BaoCao.rep_VienPhiHangNgay_MauCT_30005_InDoc();
            if (qtn30005.Count > 0)
            {
                #region gán TenRG lên từng cột
                //if (qtn30005.Count <= 9)
                //{

                //}
                //else
                //{

                #region rep30005A31
                //for (int i = 0; i < qtn30005.Count; i++)
                //{
                //    switch (i)
                //    {
                //        case 0:
                //            rep30005A31.TieuNhom1.Value = qtn30005[0].TenTN == null ? "" : qtn30005[0].TenTN;
                //            break;
                //        case 1:
                //            rep30005A31.TieuNhom2.Value = qtn30005[1].TenTN == null ? "" : qtn30005[1].TenTN;
                //            break;
                //        case 2:
                //            rep30005A31.TieuNhom3.Value = qtn30005[2].TenTN == null ? "" : qtn30005[2].TenTN;
                //            break;
                //        case 3:
                //            rep30005A31.TieuNhom4.Value = qtn30005[3].TenTN == null ? "" : qtn30005[3].TenTN;
                //            break;
                //        case 4:
                //            rep30005A31.TieuNhom5.Value = qtn30005[4].TenTN == null ? "" : qtn30005[4].TenTN;
                //            break;
                //        case 5:
                //            rep30005A31.TieuNhom6.Value = qtn30005[5].TenTN == null ? "" : qtn30005[5].TenTN;
                //            break;
                //        case 6:
                //            rep30005A31.TieuNhom7.Value = qtn30005[6].TenTN == null ? "" : qtn30005[6].TenTN;
                //            break;
                //        case 7:
                //            rep30005A31.TieuNhom8.Value = qtn30005[7].TenTN == null ? "" : qtn30005[7].TenTN;
                //            break;
                //        case 8:
                //            rep30005A31.TieuNhom9.Value = qtn30005[8].TenTN == null ? "" : qtn30005[8].TenTN;
                //            break;
                //        case 9:
                //            rep30005A31.TieuNhom10.Value = qtn30005[9].TenTN == null ? "" : qtn30005[9].TenTN;
                //            break;
                //        case 10:
                //            rep30005A31.TieuNhom11.Value = qtn30005[10].TenTN == null ? "" : qtn30005[10].TenTN;
                //            break;
                //        case 11:
                //            rep30005A31.TieuNhom12.Value = qtn30005[11].TenTN == null ? "" : qtn30005[11].TenTN;
                //            break;
                //        case 12:
                //            rep30005A31.TieuNhom13.Value = qtn30005[12].TenTN == null ? "" : qtn30005[12].TenTN;
                //            break;
                //        case 13:
                //            rep30005A31.TieuNhom14.Value = qtn30005[13].TenTN == null ? "" : qtn30005[13].TenTN;
                //            break;
                //        case 14:
                //            rep30005A31.TieuNhom15.Value = qtn30005[14].TenTN == null ? "" : qtn30005[14].TenTN;
                //            break;
                //        case 15:
                //            rep30005A31.TieuNhom16.Value = qtn30005[15].TenTN == null ? "" : qtn30005[15].TenTN;
                //            break;
                //    }
                //}
                #endregion
                #region rep20
                //if (qtn30005.Count > 16 )
                //{

                //    for (int i = 0; i < qtn30005.Count; i++)
                //    {
                //        if (i < 25)
                //        {
                //            switch (i)
                //            {
                //                case 0:
                //                    rep20.TieuNhom1.Value = qtn30005[i].TenTN == null ? "" : qtn30005[i].TenTN;
                //                    break;
                //                case 1:
                //                    rep20.TieuNhom2.Value = qtn30005[i].TenTN == null ? "" : qtn30005[i].TenTN;
                //                    break;
                //                case 2:
                //                    rep20.TieuNhom3.Value = qtn30005[i].TenTN == null ? "" : qtn30005[i].TenTN;
                //                    break;
                //                case 3:
                //                    rep20.TieuNhom4.Value = qtn30005[i].TenTN == null ? "" : qtn30005[i].TenTN;
                //                    break;
                //                case 4:
                //                    rep20.TieuNhom5.Value = qtn30005[i].TenTN == null ? "" : qtn30005[i].TenTN;
                //                    break;
                //                case 5:
                //                    rep20.TieuNhom6.Value = qtn30005[i].TenTN == null ? "" : qtn30005[i].TenTN;
                //                    break;
                //                case 6:
                //                    rep20.TieuNhom7.Value = qtn30005[i].TenTN == null ? "" : qtn30005[i].TenTN;
                //                    break;
                //                case 7:
                //                    rep20.TieuNhom8.Value = qtn30005[i].TenTN == null ? "" : qtn30005[i].TenTN;
                //                    break;
                //                case 8:
                //                    rep20.TieuNhom9.Value = qtn30005[i].TenTN == null ? "" : qtn30005[i].TenTN;
                //                    break;
                //                case 9:
                //                    rep20.TieuNhom10.Value = qtn30005[i].TenTN == null ? "" : qtn30005[i].TenTN;
                //                    break;
                //                case 10:
                //                    rep20.TieuNhom11.Value = qtn30005[i].TenTN == null ? "" : qtn30005[i].TenTN;
                //                    break;
                //                case 11:
                //                    rep20.TieuNhom12.Value = qtn30005[i].TenTN == null ? "" : qtn30005[i].TenTN;
                //                    break;
                //                case 12:
                //                    rep20.TieuNhom13.Value = qtn30005[i].TenTN == null ? "" : qtn30005[i].TenTN;
                //                    break;
                //                case 13:
                //                    rep20.TieuNhom14.Value = qtn30005[i].TenTN == null ? "" : qtn30005[i].TenTN;
                //                    break;
                //                case 14:
                //                    rep20.TieuNhom15.Value = qtn30005[i].TenTN == null ? "" : qtn30005[i].TenTN;
                //                    break;
                //                case 15:
                //                    rep20.TieuNhom16.Value = qtn30005[i].TenTN == null ? "" : qtn30005[i].TenTN;
                //                    break;
                //                case 16:
                //                    rep20.TieuNhom17.Value = qtn30005[i].TenTN == null ? "" : qtn30005[i].TenTN;
                //                    break;
                //                case 17:
                //                    rep20.TieuNhom18.Value = qtn30005[i].TenTN == null ? "" : qtn30005[i].TenTN;
                //                    break;
                //                case 18:
                //                    rep20.TieuNhom19.Value = qtn30005[i].TenTN == null ? "" : qtn30005[i].TenTN;
                //                    break;
                //                case 19:
                //                    rep20.TieuNhom20.Value = qtn30005[i].TenTN == null ? "" : qtn30005[i].TenTN;
                //                    break;
                //                case 20:
                //                    rep20.TieuNhom21.Value = qtn30005[i].TenTN == null ? "" : qtn30005[i].TenTN;
                //                    break;
                //                case 21:
                //                    rep20.TieuNhom22.Value = qtn30005[i].TenTN == null ? "" : qtn30005[i].TenTN;
                //                    break;
                //                case 22:
                //                    rep20.TieuNhom23.Value = qtn30005[i].TenTN == null ? "" : qtn30005[i].TenTN;
                //                    break;
                //                case 23:
                //                    rep20.TieuNhom24.Value = qtn30005[i].TenTN == null ? "" : qtn30005[i].TenTN;
                //                    break;
                //                case 24:
                //                    rep20.TieuNhom25.Value = qtn30005[i].TenTN == null ? "" : qtn30005[i].TenTN;
                //                    break;
                //            }
                //        }
                //    }
                //    //}
                //    //}
                //    //else if (qtn30005.Count <= 32)
                //    //{

                //    //}
                //    //else
                //    //{
                //    //    MessageBox.Show("Quá nhiều tiểu nhóm");

                //    //}

                //}
                #endregion

                for (int i = 0; i < qtn30005.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            rep30005_indoc.TieuNhom1.Value = qtn30005[0].TenTN == null ? "" : qtn30005[0].TenTN;
                            break;
                        case 1:
                            rep30005_indoc.TieuNhom2.Value = qtn30005[1].TenTN == null ? "" : qtn30005[1].TenTN;
                            break;
                        case 2:
                            rep30005_indoc.TieuNhom3.Value = qtn30005[2].TenTN == null ? "" : qtn30005[2].TenTN;
                            break;
                        case 3:
                            rep30005_indoc.TieuNhom4.Value = qtn30005[3].TenTN == null ? "" : qtn30005[3].TenTN;
                            break;
                        case 4:
                            rep30005_indoc.TieuNhom5.Value = qtn30005[4].TenTN == null ? "" : qtn30005[4].TenTN;
                            break;
                        case 5:
                            rep30005_indoc.TieuNhom6.Value = qtn30005[5].TenTN == null ? "" : qtn30005[5].TenTN;
                            break;
                        case 6:
                            rep30005_indoc.TieuNhom7.Value = qtn30005[6].TenTN == null ? "" : qtn30005[6].TenTN;
                            break;
                        case 7:
                            rep30005_indoc.TieuNhom8.Value = qtn30005[7].TenTN == null ? "" : qtn30005[7].TenTN;
                            break;
                        case 8:
                            rep30005_indoc.TieuNhom9.Value = qtn30005[8].TenTN == null ? "" : qtn30005[8].TenTN;
                            break;
                    }
                }
                #endregion
            }
            bool _noitru = cklNoiNgoaiTru.GetItemChecked(2);
            bool _DTNT = cklNoiNgoaiTru.GetItemChecked(1);
            bool _ngoaitru = cklNoiNgoaiTru.GetItemChecked(0);
            //%bảo hiểm
            bool noitru = cklHThi.GetItemChecked(2);
            bool DTNT = cklHThi.GetItemChecked(1);
            bool ngoaitru = cklHThi.GetItemChecked(0);
            List<VPHangNgay> all = new List<VPHangNgay>();
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            var qdtbn = data.DTBNs.Where(p => dtbn == 100 || p.IDDTBN == dtbn).ToList();
            var qdv = qtn.ToList();

            var qTamung0 = (from bn in data.BenhNhans.Where(p => p.TuyenDuoi == 0).Where(p => dtbn == 100 || p.IDDTBN == dtbn).Where(p => (_ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (_DTNT && p.NoiTru == 0 && p.DTNT == true) || (_noitru && p.NoiTru == 1))
                            join tu in data.TamUngs.Where(p => p.IDGoiDV <= 0 || p.IDGoiDV == null).Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay).Where(p => p.PhanLoai == 3).Where(p => makp == 0 || p.MaKP == makp).Where(p => gioHC == 2 || (gioHC == 1 && p.NgoaiGio == 1) || (gioHC == 0 && (p.NgoaiGio == null || p.NgoaiGio != 1))) on bn.MaBNhan equals tu.MaBNhan
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
                                //bn.TienBN,
                                TienBN = (bn.DTBN1 == "KSK" && dv.IDNhom == 13) ? 0 : bn.TienBN,
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
                                #region mẫu chi tiết 30005
                                TN1 = kq.Where(p => qtn30005.Count > 0 ? p.IdTieuNhom == qtn30005.First().IdTieuNhom : p.IdTieuNhom == -1).Sum(p => p.TienBN),
                                TN2 = kq.Where(p => qtn30005.Count > 1 ? p.IdTieuNhom == qtn30005.Skip(1).Select(c => c.IdTieuNhom).FirstOrDefault() : p.IdTieuNhom == -1).Sum(p => p.TienBN),
                                TN3 = kq.Where(p => qtn30005.Count > 2 ? p.IdTieuNhom == qtn30005.Skip(2).Select(c => c.IdTieuNhom).FirstOrDefault() : p.IdTieuNhom == -1).Sum(p => p.TienBN),
                                TN4 = kq.Where(p => qtn30005.Count > 3 ? p.IdTieuNhom == qtn30005.Skip(3).Select(c => c.IdTieuNhom).FirstOrDefault() : p.IdTieuNhom == -1).Sum(p => p.TienBN),
                                TN5 = kq.Where(p => qtn30005.Count > 4 ? p.IdTieuNhom == qtn30005.Skip(4).Select(c => c.IdTieuNhom).FirstOrDefault() : p.IdTieuNhom == -1).Sum(p => p.TienBN),
                                TN6 = kq.Where(p => qtn30005.Count > 5 ? p.IdTieuNhom == qtn30005.Skip(5).Select(c => c.IdTieuNhom).FirstOrDefault() : p.IdTieuNhom == -1).Sum(p => p.TienBN),
                                TN7 = kq.Where(p => qtn30005.Count > 6 ? p.IdTieuNhom == qtn30005.Skip(6).Select(c => c.IdTieuNhom).FirstOrDefault() : p.IdTieuNhom == -1).Sum(p => p.TienBN),
                                TN8 = kq.Where(p => qtn30005.Count > 7 ? p.IdTieuNhom == qtn30005.Skip(7).Select(c => c.IdTieuNhom).FirstOrDefault() : p.IdTieuNhom == -1).Sum(p => p.TienBN),
                                TN9 = kq.Where(p => qtn30005.Count > 8 ? p.IdTieuNhom == qtn30005.Skip(8).Select(c => c.IdTieuNhom).FirstOrDefault() : p.IdTieuNhom == -1).Sum(p => p.TienBN),
                                TN10 = kq.Where(p => qtn30005.Count > 9 ? p.IdTieuNhom == qtn30005.Skip(9).Select(c => c.IdTieuNhom).FirstOrDefault() : p.IdTieuNhom == -1).Sum(p => p.TienBN),
                                TN11 = kq.Where(p => qtn30005.Count > 10 ? p.IdTieuNhom == qtn30005.Skip(10).Select(c => c.IdTieuNhom).FirstOrDefault() : p.IdTieuNhom == -1).Sum(p => p.TienBN),
                                TN12 = kq.Where(p => qtn30005.Count > 11 ? p.IdTieuNhom == qtn30005.Skip(11).Select(c => c.IdTieuNhom).FirstOrDefault() : p.IdTieuNhom == -1).Sum(p => p.TienBN),
                                TN13 = kq.Where(p => qtn30005.Count > 12 ? p.IdTieuNhom == qtn30005.Skip(12).Select(c => c.IdTieuNhom).FirstOrDefault() : p.IdTieuNhom == -1).Sum(p => p.TienBN),
                                TN14 = kq.Where(p => qtn30005.Count > 13 ? p.IdTieuNhom == qtn30005.Skip(13).Select(c => c.IdTieuNhom).FirstOrDefault() : p.IdTieuNhom == -1).Sum(p => p.TienBN),
                                TN15 = kq.Where(p => qtn30005.Count > 14 ? p.IdTieuNhom == qtn30005.Skip(14).Select(c => c.IdTieuNhom).FirstOrDefault() : p.IdTieuNhom == -1).Sum(p => p.TienBN),
                                TN16 = kq.Where(p => qtn30005.Count > 15 ? p.IdTieuNhom == qtn30005.Skip(15).Select(c => c.IdTieuNhom).FirstOrDefault() : p.IdTieuNhom == -1).Sum(p => p.TienBN),

                                TN17 = kq.Where(p => qtn30005.Count > 16 ? p.IdTieuNhom == qtn30005.Skip(16).Select(c => c.IdTieuNhom).FirstOrDefault() : p.IdTieuNhom == -1).Sum(p => p.TienBN),
                                TN18 = kq.Where(p => qtn30005.Count > 17 ? p.IdTieuNhom == qtn30005.Skip(17).Select(c => c.IdTieuNhom).FirstOrDefault() : p.IdTieuNhom == -1).Sum(p => p.TienBN),
                                TN19 = kq.Where(p => qtn30005.Count > 18 ? p.IdTieuNhom == qtn30005.Skip(18).Select(c => c.IdTieuNhom).FirstOrDefault() : p.IdTieuNhom == -1).Sum(p => p.TienBN),
                                TN20 = kq.Where(p => qtn30005.Count > 19 ? p.IdTieuNhom == qtn30005.Skip(19).Select(c => c.IdTieuNhom).FirstOrDefault() : p.IdTieuNhom == -1).Sum(p => p.TienBN),
                                TN21 = kq.Where(p => qtn30005.Count > 20 ? p.IdTieuNhom == qtn30005.Skip(20).Select(c => c.IdTieuNhom).FirstOrDefault() : p.IdTieuNhom == -1).Sum(p => p.TienBN),
                                TN22 = kq.Where(p => qtn30005.Count > 21 ? p.IdTieuNhom == qtn30005.Skip(21).Select(c => c.IdTieuNhom).FirstOrDefault() : p.IdTieuNhom == -1).Sum(p => p.TienBN),
                                TN23 = kq.Where(p => qtn30005.Count > 22 ? p.IdTieuNhom == qtn30005.Skip(22).Select(c => c.IdTieuNhom).FirstOrDefault() : p.IdTieuNhom == -1).Sum(p => p.TienBN),
                                TN24 = kq.Where(p => qtn30005.Count > 23 ? p.IdTieuNhom == qtn30005.Skip(23).Select(c => c.IdTieuNhom).FirstOrDefault() : p.IdTieuNhom == -1).Sum(p => p.TienBN),
                                TN25 = kq.Where(p => qtn30005.Count > 24 ? p.IdTieuNhom == qtn30005.Skip(24).Select(c => c.IdTieuNhom).FirstOrDefault() : p.IdTieuNhom == -1).Sum(p => p.TienBN),
                                TN26 = kq.Where(p => qtn30005.Count > 25 ? p.IdTieuNhom == qtn30005.Skip(25).Select(c => c.IdTieuNhom).FirstOrDefault() : p.IdTieuNhom == -1).Sum(p => p.TienBN),
                                TN27 = kq.Where(p => qtn30005.Count > 26 ? p.IdTieuNhom == qtn30005.Skip(26).Select(c => c.IdTieuNhom).FirstOrDefault() : p.IdTieuNhom == -1).Sum(p => p.TienBN),
                                TN28 = kq.Where(p => qtn30005.Count > 27 ? p.IdTieuNhom == qtn30005.Skip(27).Select(c => c.IdTieuNhom).FirstOrDefault() : p.IdTieuNhom == -1).Sum(p => p.TienBN),
                                TN29 = kq.Where(p => qtn30005.Count > 28 ? p.IdTieuNhom == qtn30005.Skip(28).Select(c => c.IdTieuNhom).FirstOrDefault() : p.IdTieuNhom == -1).Sum(p => p.TienBN),
                                TN30 = kq.Where(p => qtn30005.Count > 29 ? p.IdTieuNhom == qtn30005.Skip(29).Select(c => c.IdTieuNhom).FirstOrDefault() : p.IdTieuNhom == -1).Sum(p => p.TienBN),
                                TN31 = kq.Where(p => qtn30005.Count > 30 ? p.IdTieuNhom == qtn30005.Skip(30).Select(c => c.IdTieuNhom).FirstOrDefault() : p.IdTieuNhom == -1).Sum(p => p.TienBN),
                                TN32 = kq.Where(p => qtn30005.Count > 31 ? p.IdTieuNhom == qtn30005.Skip(31).Select(c => c.IdTieuNhom).FirstOrDefault() : p.IdTieuNhom == -1).Sum(p => p.TienBN),
                                #endregion
                                Tong = kq.Sum(p => p.TienBN),
                            }).OrderBy(p => p.NgayTT).ToList();

            List<VPHangNgay> qTamung4 = (from bn in qTamung3
                                         group bn by new { bn.TenBNhan, bn.Tuoi, bn.DChi, bn.MaBNhan, bn.NgayTT, bn.DTBN1 } into kq
                                         select new VPHangNgay
                                         {
                                             TenBNhan = kq.Key.TenBNhan,
                                             Tuoi = kq.Key.Tuoi ?? 0,
                                             DChi = kq.Key.DChi,
                                             MaBNhan = kq.Key.MaBNhan,
                                             NgayTT = kq.Key.NgayTT,
                                             TienTN1 = kq.Sum(p => p.TN1),
                                             TienTN2 = kq.Sum(p => p.TN2),
                                             TienTN3 = kq.Sum(p => p.TN3),
                                             TienTN4 = kq.Sum(p => p.TN4),
                                             TienTN5 = kq.Sum(p => p.TN5),
                                             TienTN6 = kq.Sum(p => p.TN6),
                                             TienTN7 = kq.Sum(p => p.TN7),
                                             TienTN8 = kq.Sum(p => p.TN8),
                                             TienTN9 = kq.Sum(p => p.TN9),
                                             TienTN10 = kq.Sum(p => p.TN10),
                                             TienTN11 = kq.Sum(p => p.TN11),
                                             TienTN12 = kq.Sum(p => p.TN12),
                                             TienTN13 = kq.Sum(p => p.TN13),
                                             TienTN14 = kq.Sum(p => p.TN14),
                                             TienTN15 = kq.Sum(p => p.TN15),
                                             TienTN16 = kq.Sum(p => p.TN16),
                                             TienTN17 = kq.Sum(p => p.TN17),
                                             TienTN18 = kq.Sum(p => p.TN18),
                                             TienTN19 = kq.Sum(p => p.TN19),
                                             TienTN20 = kq.Sum(p => p.TN20),
                                             TienTN21 = kq.Sum(p => p.TN21),
                                             TienTN22 = kq.Sum(p => p.TN22),
                                             TienTN23 = kq.Sum(p => p.TN23),
                                             TienTN24 = kq.Sum(p => p.TN24),
                                             TienTN25 = kq.Sum(p => p.TN25),
                                             TienTN26 = kq.Sum(p => p.TN26),
                                             TienTN27 = kq.Sum(p => p.TN27),
                                             TienTN28 = kq.Sum(p => p.TN28),
                                             TienTN29 = kq.Sum(p => p.TN29),
                                             TienTN30 = kq.Sum(p => p.TN30),
                                             TienTN31 = kq.Sum(p => p.TN31),
                                             TienTN32 = kq.Sum(p => p.TN32),

                                             Tong = kq.Sum(p => p.Tong),
                                         }).OrderBy(p => p.NgayTT).ToList();
            all.AddRange(qTamung4);
            all = all.Where(p => p.Tong != 0).ToList();
            if (ckInDoc.Checked)
            {
                if (qtn30005.Count > 9)
                    MessageBox.Show("Dữ liệu vượt giới hạn");
                frmIn frm = new frmIn();
                if (gioHC == 0)
                {
                    rep30005_indoc.lblHanhChinh.Text = "(Trong giờ hành chính)";
                }
                else if (gioHC == 1)
                {
                    rep30005_indoc.lblHanhChinh.Text = "(Ngoài giờ hành chính)";
                }
                rep30005_indoc.DataSource = all;
                rep30005_indoc.cel_diadanh.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                rep30005_indoc.celNgayThang.Text = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy");
                rep30005_indoc.BindingData();
                rep30005_indoc.CreateDocument();
                frm.prcIN.PrintingSystem = rep30005_indoc.PrintingSystem;
                frm.ShowDialog();
            }

            else
            {
                for (int dem = 0; dem * 14 < qtn30005.Count; dem++)
                {

                    BaoCao.rep_VienPhiHangNgay_MauCT_30005 rep30005 = new BaoCao.rep_VienPhiHangNgay_MauCT_30005(dem);
                    for (int k = dem * 14; k < dem * 14 + 14; k++)
                    {
                        if (k < qtn30005.Count)
                        {
                            int j = 0;
                            j = k % 14;
                            switch (j)
                            {
                                case 0:
                                    rep30005.TieuNhom1.Value = qtn30005[k].TenTN == null ? "" : qtn30005[k].TenTN;
                                    break;
                                case 1:
                                    rep30005.TieuNhom2.Value = qtn30005[k].TenTN == null ? "" : qtn30005[k].TenTN;
                                    break;
                                case 2:
                                    rep30005.TieuNhom3.Value = qtn30005[k].TenTN == null ? "" : qtn30005[k].TenTN;
                                    break;
                                case 3:
                                    rep30005.TieuNhom4.Value = qtn30005[k].TenTN == null ? "" : qtn30005[k].TenTN;
                                    break;
                                case 4:
                                    rep30005.TieuNhom5.Value = qtn30005[k].TenTN == null ? "" : qtn30005[k].TenTN;
                                    break;
                                case 5:
                                    rep30005.TieuNhom6.Value = qtn30005[k].TenTN == null ? "" : qtn30005[k].TenTN;
                                    break;
                                case 6:
                                    rep30005.TieuNhom7.Value = qtn30005[k].TenTN == null ? "" : qtn30005[k].TenTN;
                                    break;
                                case 7:
                                    rep30005.TieuNhom8.Value = qtn30005[k].TenTN == null ? "" : qtn30005[k].TenTN;
                                    break;
                                case 8:
                                    rep30005.TieuNhom9.Value = qtn30005[k].TenTN == null ? "" : qtn30005[k].TenTN;
                                    break;
                                case 9:
                                    rep30005.TieuNhom10.Value = qtn30005[k].TenTN == null ? "" : qtn30005[k].TenTN;
                                    break;
                                case 10:
                                    rep30005.TieuNhom11.Value = qtn30005[k].TenTN == null ? "" : qtn30005[k].TenTN;
                                    break;
                                case 11:
                                    rep30005.TieuNhom12.Value = qtn30005[k].TenTN == null ? "" : qtn30005[k].TenTN;
                                    break;
                                case 12:
                                    rep30005.TieuNhom13.Value = qtn30005[k].TenTN == null ? "" : qtn30005[k].TenTN;
                                    break;
                                case 13:
                                    rep30005.TieuNhom14.Value = qtn30005[k].TenTN == null ? "" : qtn30005[k].TenTN;
                                    break;
                            }
                        }
                    }
                    frmIn frm = new frmIn();
                    if (gioHC == 0)
                    {
                        rep30005.lblHanhChinh.Text = "(Trong giờ hành chính)";
                    }
                    else if (gioHC == 1)
                    {
                        rep30005.lblHanhChinh.Text = "(Ngoài giờ hành chính)";
                    }
                    rep30005.DataSource = all;
                    rep30005.cel_diadanh.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                    rep30005.celNgayThang.Text = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy");
                    rep30005.BindingData();
                    rep30005.CreateDocument();
                    frm.prcIN.PrintingSystem = rep30005.PrintingSystem;
                    frm.ShowDialog();
                }
              
            }

            #region xuất excel
            if (ckXuatExcel.Checked && all.Count > 0)
            {
                string[] _arr = new string[37];
                for (int i = 0; i < 37; i++)
                {
                    _arr[i] = "@";
                }
                string[] _tieude = new string[37];
                _tieude[0] = "STT";
                _tieude[1] = "Họ tên";
                _tieude[2] = "Tuổi";
                _tieude[3] = "Địa chỉ";
                _tieude[4] = "Số phiếu";
                for (int i = 0; i < 32; i++)
                {
                    if (qtn30005.Count > i)
                    {
                        _tieude[i + 5] = qtn30005.Skip(i).Take(1).First().TenTN;
                    }
                    else
                        break;
                }

                string _filePath = "D:\\BCVienPhiHangNgay_MauCT.xls";
                int[] _arrWidth = new int[] { };
                DungChung.Bien.MangHaiChieu = new Object[all.Count + 1, 37];
                for (int i = 0; i < _tieude.Length; i++)
                {
                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i] == null ? "" : _tieude[i].ToUpper();
                }
                int num = 1;
                foreach (var r in all)
                {
                    DungChung.Bien.MangHaiChieu[num, 0] = num;
                    DungChung.Bien.MangHaiChieu[num, 1] = r.TenBNhan;
                    DungChung.Bien.MangHaiChieu[num, 2] = r.Tuoi;
                    DungChung.Bien.MangHaiChieu[num, 3] = r.DChi;
                    DungChung.Bien.MangHaiChieu[num, 4] = r.MaBNhan;
                    DungChung.Bien.MangHaiChieu[num, 5] = r.TienTN1;
                    DungChung.Bien.MangHaiChieu[num, 6] = r.TienTN2;
                    DungChung.Bien.MangHaiChieu[num, 7] = r.TienTN3;
                    DungChung.Bien.MangHaiChieu[num, 8] = r.TienTN4;
                    DungChung.Bien.MangHaiChieu[num, 9] = r.TienTN5;
                    DungChung.Bien.MangHaiChieu[num, 10] = r.TienTN6;
                    DungChung.Bien.MangHaiChieu[num, 11] = r.TienTN7;
                    DungChung.Bien.MangHaiChieu[num, 12] = r.TienTN8;
                    DungChung.Bien.MangHaiChieu[num, 13] = r.TienTN9;
                    DungChung.Bien.MangHaiChieu[num, 14] = r.TienTN10;
                    DungChung.Bien.MangHaiChieu[num, 15] = r.TienTN11;
                    DungChung.Bien.MangHaiChieu[num, 16] = r.TienTN12;
                    DungChung.Bien.MangHaiChieu[num, 17] = r.TienTN13;
                    DungChung.Bien.MangHaiChieu[num, 18] = r.TienTN14;
                    DungChung.Bien.MangHaiChieu[num, 19] = r.TienTN15;
                    DungChung.Bien.MangHaiChieu[num, 20] = r.TienTN16;
                    DungChung.Bien.MangHaiChieu[num, 21] = r.TienTN17;
                    DungChung.Bien.MangHaiChieu[num, 22] = r.TienTN18;
                    DungChung.Bien.MangHaiChieu[num, 23] = r.TienTN19;
                    DungChung.Bien.MangHaiChieu[num, 24] = r.TienTN20;
                    DungChung.Bien.MangHaiChieu[num, 25] = r.TienTN21;
                    DungChung.Bien.MangHaiChieu[num, 26] = r.TienTN22;
                    DungChung.Bien.MangHaiChieu[num, 27] = r.TienTN23;
                    DungChung.Bien.MangHaiChieu[num, 28] = r.TienTN24;
                    DungChung.Bien.MangHaiChieu[num, 29] = r.TienTN25;
                    DungChung.Bien.MangHaiChieu[num, 30] = r.TienTN26;
                    DungChung.Bien.MangHaiChieu[num, 31] = r.TienTN27;
                    DungChung.Bien.MangHaiChieu[num, 32] = r.TienTN28;
                    DungChung.Bien.MangHaiChieu[num, 33] = r.TienTN29;
                    DungChung.Bien.MangHaiChieu[num, 34] = r.TienTN30;
                    DungChung.Bien.MangHaiChieu[num, 35] = r.TienTN31;
                    DungChung.Bien.MangHaiChieu[num, 36] = r.TienTN32;
                    DungChung.Bien.MangHaiChieu[num, 14] = r.Tong;
                    num++;
                }

                // frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Sheet1", "C:\\VienPhiHangNgay_MauCT.xls", true, this.Name);
                if (QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Sheet1", "C:\\VienPhiHangNgay_MauCT.xls", true))

                    MessageBox.Show("Xuất thành công");
                else
                    MessageBox.Show("Lỗi!");
            }
            #endregion
            //#region Xuất Excel
            //string[] _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
            //string[] _tieude = { "Stt", "Họ tên", "Tuổi", "Địa chỉ", "Số phiếu", rep30005.TieuNhom1.Value.ToString(), rep30005.TieuNhom2.Value.ToString(), rep30005.TieuNhom3.Value.ToString(), rep30005.TieuNhom4.Value.ToString(), 
            //                             rep30005.TieuNhom5.Value.ToString(), rep30005.TieuNhom6.Value.ToString(), rep30005.TieuNhom7.Value.ToString(), rep30005.TieuNhom8.Value.ToString(), rep30005.TieuNhom9.Value.ToString(), "Tổng" };
            //string _filePath = "D:\\BCVienPhiHangNgay_MauCT.xls";
            //int[] _arrWidth = new int[] { };
            //DungChung.Bien.MangHaiChieu = new Object[all.Count + 1, 17];
            //for (int i = 0; i < _tieude.Length; i++)
            //{
            //    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i].ToUpper();
            //}
            //int num = 1;
            //foreach (var r in all)
            //{
            //    DungChung.Bien.MangHaiChieu[num, 0] = num;
            //    DungChung.Bien.MangHaiChieu[num, 1] = r.TenBNhan;
            //    DungChung.Bien.MangHaiChieu[num, 2] = r.Tuoi;
            //    DungChung.Bien.MangHaiChieu[num, 3] = r.DChi;
            //    DungChung.Bien.MangHaiChieu[num, 4] = r.MaBNhan;
            //    DungChung.Bien.MangHaiChieu[num, 5] = r.TienTN1;
            //    DungChung.Bien.MangHaiChieu[num, 6] = r.TienTN2;
            //    DungChung.Bien.MangHaiChieu[num, 7] = r.TienTN3;
            //    DungChung.Bien.MangHaiChieu[num, 8] = r.TienTN4;
            //    DungChung.Bien.MangHaiChieu[num, 9] = r.TienTN5;
            //    DungChung.Bien.MangHaiChieu[num, 10] = r.TienTN6;
            //    DungChung.Bien.MangHaiChieu[num, 11] = r.TienTN7;
            //    DungChung.Bien.MangHaiChieu[num, 12] = r.TienTN8;
            //    DungChung.Bien.MangHaiChieu[num, 13] = r.TienTN9;
            //    DungChung.Bien.MangHaiChieu[num, 14] = r.Tong;
            //    num++;
            //}
            //#endregion
            //frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Sheet1", "C:\\VienPhiHangNgay_MauCT.xls", true, this.Name);
            //rep30005.DataSource = all;
            //rep30005.cel_diadanh.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            //rep30005.celNgayThang.Text = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy");
            //rep30005.BindingData();
            //rep30005.CreateDocument();
            //frm.prcIN.PrintingSystem = rep30005.PrintingSystem;
            //frm.ShowDialog();
            //}
            //else if (qtn30005.Count <= 16)
            //{
            //    frmIn frm = new frmIn();
            //    rep30005A31.DataSource = all;
            //    rep30005A31.cel_diadanh.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            //    rep30005A31.celNgayThang.Text = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy");
            //    rep30005A31.BindingData();
            //    rep30005A31.CreateDocument();
            //    frm.prcIN.PrintingSystem = rep30005A31.PrintingSystem;
            //    frm.ShowDialog();
            //}
            //else if (qtn30005.Count > 16)
            //{
            //    if(qtn30005.Count > 25)
            //        MessageBox.Show("Dữ liệu vượt giới hạn");
            //    frmIn frm = new frmIn();
            //    rep20.DataSource = all;
            //    rep20.cel_diadanh.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            //    rep20.celNgayThang.Text = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy");
            //    rep20.BindingData();
            //    rep20.CreateDocument();
            //    frm.prcIN.PrintingSystem = rep20.PrintingSystem;
            //    frm.ShowDialog();
            //}
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cklNhomDV_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                if (e.State == CheckState.Checked)
                {
                    cklTieuNhomDV.CheckAll();
                }
                else
                    cklTieuNhomDV.UnCheckAll();
            }
        }
    }
}