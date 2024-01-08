using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.FormThamSo
{
    public partial class frm_BkeThuTienVP_BG03 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BkeThuTienVP_BG03()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data;
        List<KPhong> _lKPhong = new List<KPhong>();
        private void frm_BkeVienPhi_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            dtTungay.DateTime = System.DateTime.Now;
            dtDenNgay.DateTime = System.DateTime.Now;
            List<DTBN> _ldtuong = data.DTBNs.ToList();
            _ldtuong.Add(new DTBN { DTBN1 = "Tất cả", IDDTBN = 99 });
            lupDoiTuong.Properties.DataSource = _ldtuong.OrderByDescending(p => p.IDDTBN);
            _lKPhong = data.KPhongs.OrderBy(p => p.TenKP).ToList();
            _lKPhong.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
            lupKPhong.Properties.DataSource = _lKPhong;
            lupKPhong.EditValue = 0;
            radioGroup1.SelectedIndex = 0;
            rdIn.SelectedIndex = 0;
            rdNgayTK_SelectedIndexChanged(sender, e);


        }
        public DataTable tb;
        private void btnInBC_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(dtTungay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
            int doituong = 99;
            int khoaphong = 0;
            if (lupKPhong.EditValue != null)
            {
                khoaphong = Convert.ToInt32(lupKPhong.EditValue);
            }
            int noingoaitru = -1;
            if (lupDoiTuong.EditValue != null)
                doituong = Convert.ToInt32(lupDoiTuong.EditValue);
            if (radioGroup1.SelectedIndex == 0)// tất cả
                noingoaitru = -1;
            else if (radioGroup1.SelectedIndex == 1)// nội trú
                noingoaitru = 1;
            else if (radioGroup1.SelectedIndex == 2)// ngoại trú
                noingoaitru = 0;

            #region add para baocao
            List<NhomDV> _lnhomThuoc = new List<NhomDV>();
            int idNhomthuoc = 0;
            _lnhomThuoc = data.NhomDVs.Where(p => p.TenNhomCT == ("Thuốc trong danh mục BHYT")).ToList();
            if (_lnhomThuoc.Count > 0)
                idNhomthuoc = _lnhomThuoc.First().IDNhom;
            #endregion
            if (rdDuyetThanhToan.SelectedIndex == 0)
            {
                #region BN đã duyệt
                var _lTamUng = (from a in data.TamUngs.Where(p => p.PhanLoai == 1 || p.PhanLoai == 2) select new { a.IDTamUng, a.MaBNhan, a.PhanLoai, a.NgayThu }).ToList();
                var q13 = (from vp in data.VienPhis.Where(p => rdNgayTK.SelectedIndex == 0 ? (p.NgayDuyet >= tungay && p.NgayDuyet <= denngay) : (p.NgayTT >= tungay && p.NgayTT <= denngay))
                           join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                           select new { vpct.idVPhict, vpct.SoLuong, vpct.TienBH, vpct.TienBN, vpct.TrongBH, vpct.IDTamUng, vpct.MaDV, vpct.ThanhTien, vp.NgayTT, vpct.MaKP, vp.MaBNhan }).ToList();
                var q123 = (from idtu in _lTamUng
                            join vpct in q13 on idtu.IDTamUng equals vpct.IDTamUng
                            select new { idtu, vpct }).ToList();
                var _ldv = (from dv in data.DichVus
                            join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                            select new { dv.MaDV, dv.TenDV, tn.IdTieuNhom, tn.IDNhom, tn.TenTN }).ToList();
                var chiphi = (from a in q123 join dv in _ldv on a.vpct.MaDV equals dv.MaDV select new { a, dv }).ToList();
                var bnhan = (from idtu in _lTamUng
                             join bn in data.BenhNhans
                         .Where(p => p.IDDTBN == doituong || doituong == 99)
                         .Where(p => noingoaitru == -1 ? true : p.NoiTru == noingoaitru)
                         .Where(p => khoaphong == 0 ? true : p.MaKP == khoaphong) on idtu.MaBNhan equals bn.MaBNhan
                             join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             select new { idtu, bn, rv }).ToList();
                var all2 = (from bn in bnhan
                            join cp in chiphi on bn.bn.MaBNhan equals cp.a.vpct.MaBNhan
                            orderby bn.bn.MaBNhan, cp.dv.IDNhom
                            select new
                            {
                                cp.a.vpct.idVPhict,
                                cp.dv.TenTN,
                                bn.bn.MaBNhan,
                                bn.bn.TenBNhan,
                                bn.bn.GTinh,
                                bn.bn.Tuoi,
                                bn.bn.DChi,
                                cp.dv.IDNhom,
                                cp.dv.IdTieuNhom,
                                cp.a.vpct.SoLuong,
                                cp.a.vpct.ThanhTien,
                                cp.a.vpct.TienBN,
                                bn.rv.MaKP,
                                cp.a.vpct.NgayTT,
                                bn.rv.NgayVao,
                                bn.rv.SoNgaydt,
                                bn.bn.DTuong,
                                cp.a.vpct.TrongBH,
                                bn.bn.NoiTru,
                                // cp.a.idtu.PhanLoai,
                                //  cp.a.idtu.NgayThu
                            }).ToList();
                var all = (from a in all2
                           select new
                           {
                               a.idVPhict,
                               a.TenTN,
                               a.MaBNhan,
                               a.TenBNhan,
                               a.GTinh,
                               a.Tuoi,
                               a.DChi,
                               a.IDNhom,
                               a.IdTieuNhom,
                               a.SoLuong,
                               a.ThanhTien,
                               a.TienBN,
                               a.NgayTT,
                               a.NgayVao,
                               a.SoNgaydt,
                               a.DTuong,
                               a.TrongBH,
                               a.NoiTru,
                               // a.PhanLoai,
                               // a.NgayThu,
                               a.MaKP
                           }).ToList();
                var tnhom = (from a in all where a.IDNhom != idNhomthuoc select new { a.TenTN, a.IdTieuNhom, a.IDNhom }).OrderBy(p => p.IDNhom).Distinct().ToList();
                if (tnhom.Count > 16)
                {
                    MessageBox.Show("Dữ liệu vượt giới hạn");
                }
                int[] arrTn = new int[22];
                for (int i = 0; i < 22; i++)
                {
                    if (i < tnhom.Count)
                        arrTn[i] = tnhom.Skip(i).Take(1).First().IdTieuNhom;
                    else
                        arrTn[i] = 0;
                }
                int colcount = tnhom.Count + 1;
                if (tnhom.Count >= 17)
                    colcount = 23;
                int stt = 1;
                var b = (from a in all
                         group a by new
                         {
                             a.TenBNhan,
                             a.MaBNhan,
                             a.GTinh,
                             a.DChi,
                             a.Tuoi,
                             a.NgayVao,
                             a.MaKP,
                             a.NgayTT,
                             a.SoNgaydt
                         } into kq
                         select new
                         {
                             STT = stt++,
                             kq.Key.MaBNhan,
                             kq.Key.TenBNhan,
                             kq.Key.DChi,
                             kq.Key.NgayTT,
                             kq.Key.NgayVao,
                             kq.Key.SoNgaydt,
                             kq.Key.MaKP,
                             tn1 = kq.Where(p => p.IDNhom == idNhomthuoc).Sum(p => p.TienBN),
                             tn2 = kq.Where(p => p.IdTieuNhom == arrTn[0]).Sum(p => p.TienBN),
                             tn3 = kq.Where(p => p.IdTieuNhom == arrTn[1]).Sum(p => p.TienBN),
                             tn4 = kq.Where(p => p.IdTieuNhom == arrTn[2]).Sum(p => p.TienBN),
                             tn5 = kq.Where(p => p.IdTieuNhom == arrTn[3]).Sum(p => p.TienBN),
                             tn6 = kq.Where(p => p.IdTieuNhom == arrTn[4]).Sum(p => p.TienBN),
                             tn7 = kq.Where(p => p.IdTieuNhom == arrTn[5]).Sum(p => p.TienBN),
                             tn8 = kq.Where(p => p.IdTieuNhom == arrTn[6]).Sum(p => p.TienBN),
                             tn9 = kq.Where(p => p.IdTieuNhom == arrTn[7]).Sum(p => p.TienBN),
                             tn10 = kq.Where(p => p.IdTieuNhom == arrTn[8]).Sum(p => p.TienBN),
                             tn11 = kq.Where(p => p.IdTieuNhom == arrTn[9]).Sum(p => p.TienBN),
                             tn12 = kq.Where(p => p.IdTieuNhom == arrTn[10]).Sum(p => p.TienBN),
                             tn13 = kq.Where(p => p.IdTieuNhom == arrTn[11]).Sum(p => p.TienBN),
                             tn14 = kq.Where(p => p.IdTieuNhom == arrTn[12]).Sum(p => p.TienBN),
                             tn15 = kq.Where(p => p.IdTieuNhom == arrTn[13]).Sum(p => p.TienBN),
                             tn16 = kq.Where(p => p.IdTieuNhom == arrTn[14]).Sum(p => p.TienBN),
                             tn17 = kq.Where(p => p.IdTieuNhom == arrTn[15]).Sum(p => p.TienBN),
                             tn18 = kq.Where(p => p.IdTieuNhom == arrTn[16]).Sum(p => p.TienBN),
                             tn19 = kq.Where(p => p.IdTieuNhom == arrTn[17]).Sum(p => p.TienBN),
                             tn20 = kq.Where(p => p.IdTieuNhom == arrTn[18]).Sum(p => p.TienBN),
                             tn21 = kq.Where(p => p.IdTieuNhom == arrTn[19]).Sum(p => p.TienBN),
                             tn22 = kq.Where(p => p.IdTieuNhom == arrTn[20]).Sum(p => p.TienBN),
                             tn23 = kq.Where(p => p.IdTieuNhom == arrTn[21]).Sum(p => p.TienBN)
                         }).ToList();
                var c = (from bc in b
                         select new
                         {
                             bc.STT,
                             bc.MaBNhan,
                             bc.TenBNhan,
                             bc.DChi,
                             bc.MaKP,
                             NgayTT = (bc.NgayTT == null ? "" : bc.NgayTT.Value.ToString("dd/MM")),
                             NgayVao = (bc.NgayVao == null ? "" : bc.NgayVao.Value.ToString("dd/MM")),
                             bc.SoNgaydt,
                             tn1 = bc.tn1,
                             tn2 = bc.tn2,
                             tn3 = bc.tn3,
                             tn4 = bc.tn4,
                             tn5 = bc.tn5,
                             tn6 = bc.tn6,
                             tn7 = bc.tn7,
                             tn8 = bc.tn8,
                             tn9 = bc.tn9,
                             tn10 = bc.tn10,
                             tn11 = bc.tn11,
                             tn12 = bc.tn12,
                             tn13 = bc.tn13,
                             tn14 = bc.tn14,
                             tn15 = bc.tn15,
                             tn16 = bc.tn16,
                             tn17 = bc.tn17,
                             tn18 = bc.tn18,
                             tn19 = bc.tn19,
                             tn20 = bc.tn20,
                             tn21 = bc.tn21,
                             tn22 = bc.tn22,
                             tn23 = bc.tn23,
                             TongCong = bc.tn1 + bc.tn2 + bc.tn3 + bc.tn4 + bc.tn5 + bc.tn6 + bc.tn7 + bc.tn8 + bc.tn9 + bc.tn10 + bc.tn11 + bc.tn12
                             + bc.tn13 + bc.tn14 + bc.tn15 + bc.tn16 + bc.tn17 + bc.tn18 + bc.tn19 + bc.tn20 + bc.tn21 + bc.tn22 + bc.tn23
                         }).Where(p => p.TongCong != 0).ToList();
                #region in chi tiết
                if (rdIn.SelectedIndex == 0)
                {
                    frmIn frm = new frmIn();
                    BaoCao.rep_BkeThuTienVP_BG03 rep = new BaoCao.rep_BkeThuTienVP_BG03();
                    for (int i = 1; i <= colcount; i++)
                    {
                        switch (i)
                        {
                            case 1:
                                rep.n1.Value = "Thuốc";
                                break;
                            case 2:
                                rep.n2.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 3:
                                rep.n3.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 4:
                                rep.n4.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 5:
                                rep.n5.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 6:
                                rep.n6.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 7:
                                rep.n7.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 8:
                                rep.n8.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 9:
                                rep.n9.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 10:
                                rep.n10.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 11:
                                rep.n11.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 12:
                                rep.n12.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 13:
                                rep.n13.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 14:
                                rep.n14.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 15:
                                rep.n15.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 16:
                                rep.n16.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 17:
                                rep.n17.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 18:
                                rep.n18.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 19:
                                rep.n19.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 20:
                                rep.n20.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 21:
                                rep.n21.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 22:
                                rep.n22.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 23:
                                rep.n23.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                        }
                    }
                    rep.Ngay.Value = "Từ ngày: " + dtTungay.Text + "  đến ngày: " + dtDenNgay.Text;
                    rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                    rep.TieuDe.Value = ("Bảng kê chi tiết viện phí").ToUpper();
                    if (lupKPhong.EditValue != null && lupKPhong.EditValue.ToString() != "0")
                        rep.TenKhoa.Value = lupKPhong.Text;
                    rep.DataSource = c;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    this.Hide();
                    frm.ShowDialog();
                    this.Show();
                }
                #endregion in chi tiết
                #region in tổng hợp
                else
                {
                    var q = (from l in c
                             group l by new { l.MaKP } into kq
                             select new
                             {
                                 kq.Key.MaKP,
                                 soBN = kq.Count(),
                                 SoNgaydt = kq.Sum(p => p.SoNgaydt),
                                 tn1 = kq.Sum(p => p.tn1),
                                 tn2 = kq.Sum(p => p.tn2),
                                 tn3 = kq.Sum(p => p.tn3),
                                 tn4 = kq.Sum(p => p.tn4),
                                 tn5 = kq.Sum(p => p.tn5),
                                 tn6 = kq.Sum(p => p.tn6),
                                 tn7 = kq.Sum(p => p.tn7),
                                 tn8 = kq.Sum(p => p.tn8),
                                 tn9 = kq.Sum(p => p.tn9),
                                 tn10 = kq.Sum(p => p.tn10),
                                 tn11 = kq.Sum(p => p.tn11),
                                 tn12 = kq.Sum(p => p.tn12),
                                 tn13 = kq.Sum(p => p.tn13),
                                 tn14 = kq.Sum(p => p.tn14),
                                 tn15 = kq.Sum(p => p.tn15),
                                 tn16 = kq.Sum(p => p.tn16),
                                 tn17 = kq.Sum(p => p.tn17),
                                 //tn18 = kq.Sum(p => p.tn18),
                                 TongCong = kq.Sum(p => p.TongCong)
                             })
                             .Where(p => p.TongCong != null && p.TongCong != 0)
                             .ToList();
                    var q1 = (from qn in q
                              join kp in data.KPhongs on qn.MaKP equals kp.MaKP into ps
                              from p in ps.DefaultIfEmpty()
                              select new
                              {
                                  TenKP = p == null ? "" : p.TenKP,
                                  qn.soBN,
                                  qn.SoNgaydt,
                                  qn.tn1,
                                  qn.tn2,
                                  qn.tn3,
                                  qn.tn4,
                                  qn.tn5,
                                  qn.tn6,
                                  qn.tn7,
                                  qn.tn8,
                                  qn.tn9,
                                  qn.tn10,
                                  qn.tn11,
                                  qn.tn12,
                                  qn.tn13,
                                  qn.tn14,
                                  qn.tn15,
                                  qn.tn16,
                                  qn.tn17,
                                  //qn.tn18,
                                  qn.TongCong,
                              }).ToList();

                    frmIn frm = new frmIn();
                    BaoCao.rep_BkeThuTienVP_BG03_TH rep = new BaoCao.rep_BkeThuTienVP_BG03_TH();
                    for (int i = 1; i <= colcount; i++)
                    {
                        switch (i)
                        {
                            case 1:
                                rep.n1.Value = "Thuốc";
                                break;
                            case 2:
                                rep.n2.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            case 3:
                                rep.n3.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            case 4:
                                rep.n4.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            case 5:
                                rep.n5.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            case 6:
                                rep.n6.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            case 7:
                                rep.n7.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            case 8:
                                rep.n8.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            case 9:
                                rep.n9.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            case 10:
                                rep.n10.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            case 11:
                                rep.n11.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            case 12:
                                rep.n12.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            case 13:
                                rep.n13.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            case 14:
                                rep.n14.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            case 15:
                                rep.n15.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            case 16:
                                rep.n16.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            case 17:
                                rep.n17.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            //case 18:
                            //    rep.n18.Value = tnhom[i - 2].TenTN.ToString();
                            //    break;
                        }
                    }
                    rep.Ngay.Value = "Từ ngày: " + dtTungay.Text + "  đến ngày: " + dtDenNgay.Text;
                    rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                    rep.TieuDe.Value = ("Bảng tổng hợp viện phí các khoa").ToUpper();
                    rep.DataSource = q1;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    this.Hide();
                    frm.ShowDialog();
                    this.Show();
                }
                #endregion in tổng hợp
                #endregion
            }
            else
            {
                #region BN đã thanh toán
                //   var _lTamUng = (from a in data.TamUngs.Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay).Where(p => p.PhanLoai == 1 || p.PhanLoai == 2) select new { a.IDTamUng, a.MaBNhan, a.PhanLoai, a.NgayThu }).ToList();
                var q13 = (from vp in data.VienPhis.Where(p => rdNgayTK.SelectedIndex == 0 ? (p.NgayDuyet >= tungay && p.NgayDuyet <= denngay) : (rdNgayTK.SelectedIndex == 1 ? (p.NgayTT >= tungay && p.NgayTT <= denngay) : (p.NgayDuyetCP >= tungay && p.NgayDuyetCP <= denngay)))
                               .Where(p => ckTimTheoCB.Checked ? (p.MaCB == DungChung.Bien.MaCB) : true)
                           join vpct in data.VienPhicts.Where(p => p.ThanhToan == 0) on vp.idVPhi equals vpct.idVPhi
                           select new { vpct.idVPhict, vpct.SoLuong, vpct.TienBH, vpct.TienBN, vpct.TrongBH, vpct.IDTamUng, vpct.MaDV, vpct.ThanhTien, vp.NgayTT, vpct.MaKP, vp.MaBNhan }).ToList();
                //var q123 = (from idtu in _lTamUng
                //            join vpct in q13 on idtu.IDTamUng equals vpct.IDTamUng
                //            select new { idtu, vpct }).ToList();
                var _ldv = (from dv in data.DichVus
                            join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                            select new { dv.MaDV, dv.TenDV, tn.IdTieuNhom, tn.IDNhom, tn.TenTN }).ToList();
                var chiphi = (from a in q13 join dv in _ldv on a.MaDV equals dv.MaDV select new { a, dv }).ToList();
                var bnhan = (from bn in data.BenhNhans
                         .Where(p => p.IDDTBN == doituong || doituong == 99)
                         .Where(p => noingoaitru == -1 ? true : p.NoiTru == noingoaitru)
                         .Where(p => khoaphong == 0 ? true : p.MaKP == khoaphong)
                             join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                             select new { bn, rv }).ToList();
                var all2 = (from bn in bnhan
                            join cp in chiphi on bn.bn.MaBNhan equals cp.a.MaBNhan
                            orderby bn.bn.MaBNhan, cp.dv.IDNhom
                            select new
                            {
                                cp.a.idVPhict,
                                cp.dv.TenTN,
                                bn.bn.MaBNhan,
                                bn.bn.TenBNhan,
                                bn.bn.GTinh,
                                bn.bn.Tuoi,
                                bn.bn.DChi,
                                cp.dv.IDNhom,
                                cp.dv.IdTieuNhom,
                                cp.a.SoLuong,
                                cp.a.ThanhTien,
                                cp.a.TienBN,
                                bn.rv.MaKP,
                                cp.a.NgayTT,
                                bn.rv.NgayVao,
                                bn.rv.SoNgaydt,
                                bn.bn.DTuong,
                                cp.a.TrongBH,
                                bn.bn.NoiTru,
                                // cp.a.idtu.PhanLoai,
                                //  cp.a.idtu.NgayThu
                            }).ToList();
                var all = (from a in all2
                           select new
                           {
                               a.idVPhict,
                               a.TenTN,
                               a.MaBNhan,
                               a.TenBNhan,
                               a.GTinh,
                               a.Tuoi,
                               a.DChi,
                               a.IDNhom,
                               a.IdTieuNhom,
                               a.SoLuong,
                               a.ThanhTien,
                               a.TienBN,
                               a.NgayTT,
                               a.NgayVao,
                               a.SoNgaydt,
                               a.DTuong,
                               a.TrongBH,
                               a.NoiTru,
                               // a.PhanLoai,
                               // a.NgayThu,
                               a.MaKP
                           }).ToList();
                var tnhom = (from a in all where a.IDNhom != idNhomthuoc select new { a.TenTN, a.IdTieuNhom, a.IDNhom }).OrderBy(p => p.IDNhom).Distinct().ToList();
                if (tnhom.Count > 16)
                {
                    MessageBox.Show("Dữ liệu vượt giới hạn");
                }
                int[] arrTn = new int[21];
                for (int i = 0; i < 21; i++)
                {
                    if (i < tnhom.Count)
                        arrTn[i] = tnhom.Skip(i).Take(1).First().IdTieuNhom;
                    else
                        arrTn[i] = 0;
                }
                int colcount = tnhom.Count + 1;
                if (tnhom.Count >= 17)
                    colcount = 23;
                int stt = 1;
                var b = (from a in all
                         group a by new
                         {
                             a.TenBNhan,
                             a.MaBNhan,
                             a.GTinh,
                             a.DChi,
                             a.Tuoi,
                             a.NgayVao,
                             a.MaKP,
                             a.NgayTT,
                             a.SoNgaydt
                         } into kq
                         select new
                         {
                             STT = stt++,
                             kq.Key.MaBNhan,
                             kq.Key.TenBNhan,
                             kq.Key.DChi,
                             kq.Key.NgayTT,
                             kq.Key.NgayVao,
                             kq.Key.SoNgaydt,
                             kq.Key.MaKP,
                             tn1 = kq.Where(p => p.IDNhom == idNhomthuoc).Sum(p => p.TienBN),
                             tn2 = kq.Where(p => p.IdTieuNhom == arrTn[0]).Sum(p => p.TienBN),
                             tn3 = kq.Where(p => p.IdTieuNhom == arrTn[1]).Sum(p => p.TienBN),
                             tn4 = kq.Where(p => p.IdTieuNhom == arrTn[2]).Sum(p => p.TienBN),
                             tn5 = kq.Where(p => p.IdTieuNhom == arrTn[3]).Sum(p => p.TienBN),
                             tn6 = kq.Where(p => p.IdTieuNhom == arrTn[4]).Sum(p => p.TienBN),
                             tn7 = kq.Where(p => p.IdTieuNhom == arrTn[5]).Sum(p => p.TienBN),
                             tn8 = kq.Where(p => p.IdTieuNhom == arrTn[6]).Sum(p => p.TienBN),
                             tn9 = kq.Where(p => p.IdTieuNhom == arrTn[7]).Sum(p => p.TienBN),
                             tn10 = kq.Where(p => p.IdTieuNhom == arrTn[8]).Sum(p => p.TienBN),
                             tn11 = kq.Where(p => p.IdTieuNhom == arrTn[9]).Sum(p => p.TienBN),
                             tn12 = kq.Where(p => p.IdTieuNhom == arrTn[10]).Sum(p => p.TienBN),
                             tn13 = kq.Where(p => p.IdTieuNhom == arrTn[11]).Sum(p => p.TienBN),
                             tn14 = kq.Where(p => p.IdTieuNhom == arrTn[12]).Sum(p => p.TienBN),
                             tn15 = kq.Where(p => p.IdTieuNhom == arrTn[13]).Sum(p => p.TienBN),
                             tn16 = kq.Where(p => p.IdTieuNhom == arrTn[14]).Sum(p => p.TienBN),
                             tn17 = kq.Where(p => p.IdTieuNhom == arrTn[15]).Sum(p => p.TienBN),
                             tn18 = kq.Where(p => p.IdTieuNhom == arrTn[16]).Sum(p => p.TienBN),
                             tn19 = kq.Where(p => p.IdTieuNhom == arrTn[17]).Sum(p => p.TienBN),
                             tn20 = kq.Where(p => p.IdTieuNhom == arrTn[18]).Sum(p => p.TienBN),
                             tn21 = kq.Where(p => p.IdTieuNhom == arrTn[19]).Sum(p => p.TienBN),
                             tn22 = kq.Where(p => p.IdTieuNhom == arrTn[20]).Sum(p => p.TienBN),
                             //tn23 = kq.Where(p => p.IdTieuNhom == arrTn[21]).Sum(p => p.TienBN)
                         }).ToList();
                var c = (from bc in b
                         select new
                         {
                             bc.STT,
                             bc.MaBNhan,
                             bc.TenBNhan,
                             bc.DChi,
                             bc.MaKP,
                             NgayTT = (bc.NgayTT == null ? "" : bc.NgayTT.Value.ToString("dd/MM")),
                             NgayVao = (bc.NgayVao == null ? "" : bc.NgayVao.Value.ToString("dd/MM")),
                             bc.SoNgaydt,
                             tn1 = bc.tn1,
                             tn2 = bc.tn2,
                             tn3 = bc.tn3,
                             tn4 = bc.tn4,
                             tn5 = bc.tn5,
                             tn6 = bc.tn6,
                             tn7 = bc.tn7,
                             tn8 = bc.tn8,
                             tn9 = bc.tn9,
                             tn10 = bc.tn10,
                             tn11 = bc.tn11,
                             tn12 = bc.tn12,
                             tn13 = bc.tn13,
                             tn14 = bc.tn14,
                             tn15 = bc.tn15,
                             tn16 = bc.tn16,
                             tn17 = bc.tn17,
                             tn18 = bc.tn18,
                             tn19 = bc.tn19,
                             tn20 = bc.tn20,
                             tn21 = bc.tn21,
                             tn22 = bc.tn22,
                             //tn23 = bc.tn23,
                             TongCong = bc.tn1 + bc.tn2 + bc.tn3 + bc.tn4 + bc.tn5 + bc.tn6 + bc.tn7 + bc.tn8 + bc.tn9 + bc.tn10 + bc.tn11 + bc.tn12
                             + bc.tn13 + bc.tn14 + bc.tn15 + bc.tn16 + bc.tn17 + bc.tn18 + bc.tn19 + bc.tn20 + bc.tn21 + bc.tn22// + bc.tn23
                         }).Where(p => p.TongCong != 0).ToList();
                #region in chi tiết
                if (rdIn.SelectedIndex == 0)
                {
                    frmIn frm = new frmIn();
                    BaoCao.rep_BkeThuTienVP_BG03 rep = new BaoCao.rep_BkeThuTienVP_BG03();
                    for (int i = 1; i <= colcount; i++)
                    {
                        switch (i)
                        {
                            case 1:
                                rep.n1.Value = "Thuốc";
                                break;
                            case 2:
                                rep.n2.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 3:
                                rep.n3.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 4:
                                rep.n4.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 5:
                                rep.n5.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 6:
                                rep.n6.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 7:
                                rep.n7.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 8:
                                rep.n8.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 9:
                                rep.n9.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 10:
                                rep.n10.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 11:
                                rep.n11.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 12:
                                rep.n12.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 13:
                                rep.n13.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 14:
                                rep.n14.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 15:
                                rep.n15.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 16:
                                rep.n16.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 17:
                                rep.n17.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 18:
                                rep.n18.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 19:
                                rep.n19.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 20:
                                rep.n20.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 21:
                                rep.n21.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            case 22:
                                rep.n22.Value = ((i - 2) < tnhom.Count()) ? tnhom[i - 2].TenTN.ToString() : "";
                                break;
                            //case 23:
                            //    rep.n23.Value = tnhom[i - 2].TenTN.ToString();
                            //    break;
                        }
                    }
                    rep.Ngay.Value = "Từ ngày: " + dtTungay.Text + "  đến ngày: " + dtDenNgay.Text;
                    rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                    rep.TieuDe.Value = ("Bảng kê chi tiết viện phí").ToUpper();
                    if (lupKPhong.EditValue != null && lupKPhong.EditValue.ToString() != "0")
                        rep.TenKhoa.Value = lupKPhong.Text;
                    rep.DataSource = c;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    this.Hide();
                    frm.ShowDialog();
                    this.Show();
                }
                #endregion in chi tiết
                #region in tổng hợp
                else
                {
                    var q = (from l in c
                             group l by new { l.MaKP } into kq
                             select new
                             {
                                 kq.Key.MaKP,
                                 soBN = kq.Count(),
                                 SoNgaydt = kq.Sum(p => p.SoNgaydt),
                                 tn1 = kq.Sum(p => p.tn1),
                                 tn2 = kq.Sum(p => p.tn2),
                                 tn3 = kq.Sum(p => p.tn3),
                                 tn4 = kq.Sum(p => p.tn4),
                                 tn5 = kq.Sum(p => p.tn5),
                                 tn6 = kq.Sum(p => p.tn6),
                                 tn7 = kq.Sum(p => p.tn7),
                                 tn8 = kq.Sum(p => p.tn8),
                                 tn9 = kq.Sum(p => p.tn9),
                                 tn10 = kq.Sum(p => p.tn10),
                                 tn11 = kq.Sum(p => p.tn11),
                                 tn12 = kq.Sum(p => p.tn12),
                                 tn13 = kq.Sum(p => p.tn13),
                                 tn14 = kq.Sum(p => p.tn14),
                                 tn15 = kq.Sum(p => p.tn15),
                                 tn16 = kq.Sum(p => p.tn16),
                                 tn17 = kq.Sum(p => p.tn17),
                                 //tn18 = kq.Sum(p => p.tn18),
                                 TongCong = kq.Sum(p => p.TongCong)
                             })
                             .Where(p => p.TongCong != null && p.TongCong != 0)
                             .ToList();
                    var q1 = (from qn in q
                              join kp in data.KPhongs on qn.MaKP equals kp.MaKP into ps
                              from p in ps.DefaultIfEmpty()
                              select new
                              {
                                  TenKP = p == null ? "" : p.TenKP,
                                  qn.soBN,
                                  qn.SoNgaydt,
                                  qn.tn1,
                                  qn.tn2,
                                  qn.tn3,
                                  qn.tn4,
                                  qn.tn5,
                                  qn.tn6,
                                  qn.tn7,
                                  qn.tn8,
                                  qn.tn9,
                                  qn.tn10,
                                  qn.tn11,
                                  qn.tn12,
                                  qn.tn13,
                                  qn.tn14,
                                  qn.tn15,
                                  qn.tn16,
                                  qn.tn17,
                                  //qn.tn18,
                                  qn.TongCong,
                              }).ToList();

                    frmIn frm = new frmIn();
                    BaoCao.rep_BkeThuTienVP_BG03_TH rep = new BaoCao.rep_BkeThuTienVP_BG03_TH();
                    for (int i = 1; i <= colcount; i++)
                    {
                        switch (i)
                        {
                            case 1:
                                rep.n1.Value = "Thuốc";
                                break;
                            case 2:
                                rep.n2.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            case 3:
                                rep.n3.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            case 4:
                                rep.n4.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            case 5:
                                rep.n5.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            case 6:
                                rep.n6.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            case 7:
                                rep.n7.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            case 8:
                                rep.n8.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            case 9:
                                rep.n9.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            case 10:
                                rep.n10.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            case 11:
                                rep.n11.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            case 12:
                                rep.n12.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            case 13:
                                rep.n13.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            case 14:
                                rep.n14.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            case 15:
                                rep.n15.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            case 16:
                                rep.n16.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            case 17:
                                rep.n17.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                            //case 18:
                            //    rep.n18.Value = tnhom[i - 2].TenTN.ToString();
                            //    break;
                        }
                    }
                    rep.Ngay.Value = "Từ ngày: " + dtTungay.Text + "  đến ngày: " + dtDenNgay.Text;
                    rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                    rep.TieuDe.Value = ("Bảng tổng hợp viện phí các khoa").ToUpper();
                    rep.DataSource = q1;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    this.Hide();
                    frm.ShowDialog();
                    this.Show();
                }
                #endregion in tổng hợp
                #endregion
            }

        }

        private void labelControl6_Click(object sender, EventArgs e)
        {

        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void lupDoiTuong_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void rdNgayTK_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdNgayTK.SelectedIndex == 2)
            {
                rdDuyetThanhToan.Properties.ReadOnly = true;
                rdDuyetThanhToan.SelectedIndex = 1;
            }
            else
                rdDuyetThanhToan.Properties.ReadOnly = false;
        }


    }
}