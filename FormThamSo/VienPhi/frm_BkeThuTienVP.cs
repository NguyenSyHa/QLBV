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
    public partial class frm_BkeThuTienVP : DevExpress.XtraEditors.XtraForm
    {
        public frm_BkeThuTienVP()
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
            DataTable tbDoiTuong = new DataTable();
            tbDoiTuong.Columns.Add("TenDoiTuong", typeof(string));
            tbDoiTuong.Columns.Add("MaDoiTuong", typeof(string));
            tbDoiTuong.Rows.Add("Tất cả", "tc");
            tbDoiTuong.Rows.Add("BHYT", "BHYT");
            tbDoiTuong.Rows.Add("Dịch vụ", "Dịch vụ");
            tbDoiTuong.Rows.Add("KSK", "KSK");
            lupDoiTuong.Properties.DataSource = tbDoiTuong;
            _lKPhong = data.KPhongs.OrderBy(p => p.TenKP).ToList();
            _lKPhong.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
            radioGroup1.SelectedIndex = 0;
        }
        public DataTable tb;
        private void btnInBC_Click(object sender, EventArgs e)
        {

            DateTime tungay = DungChung.Ham.NgayTu(dtTungay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
            string doituong = "tc";
            string khoaphong = "tc";
            int noingoaitru = -1;
            if (lupDoiTuong.EditValue != null)
                doituong = lupDoiTuong.EditValue.ToString();
            if (radioGroup1.SelectedIndex == 0)// tất cả
                noingoaitru = -1;
            else if (radioGroup1.SelectedIndex == 1)// nội trú
                noingoaitru = 1;
            else if (radioGroup1.SelectedIndex == 2)// ngoại trú
                noingoaitru = 0;

            frmIn frm = new frmIn();
            BaoCao.rep_BkeThuTienVP rep = new BaoCao.rep_BkeThuTienVP();
            BaoCao.rep_BkeThuTienVP_More rep2 = new BaoCao.rep_BkeThuTienVP_More();
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            #region add para baocao
            int idNhomthuoc = 0;
            var nt = data.NhomDVs.Where(p => p.TenNhom == ("Thuốc, Dịch truyền"));
            if (nt.Count() > 0)
                idNhomthuoc = nt.ToList().First().IDNhom;


            rep.Ngay.Value = "Từ ngày: " + dtTungay.Text + "  đến ngày: " + dtDenNgay.Text;
            rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
            rep.TieuDe.Value = ("Bảng kê thu viện phí").ToUpper();
            rep2.Ngay.Value = "Từ ngày: " + dtTungay.Text + "  đến ngày: " + dtDenNgay.Text;
            rep2.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
            rep2.TieuDe.Value = ("Bảng kê thu viện phí").ToUpper();
            #endregion
            int trongdm = 1, ngoaidm = 0;
            if (cbo_trongDM.SelectedIndex != 2)
            {
                trongdm = cbo_trongDM.SelectedIndex;
                ngoaidm = cbo_trongDM.SelectedIndex;
            }

            var query0 = (from bn in data.BenhNhans.Where(p => (doituong == "tc" || p.DTuong == doituong) && (noingoaitru == -1 || p.NoiTru == noingoaitru))
                          join vp in data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                          join vpct in data.VienPhicts.Where(o => o.TrongBH == trongdm || o.TrongBH == ngoaidm) on vp.idVPhi equals vpct.idVPhi
                          join tu in data.TamUngs.Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay && (p.PhanLoai == 1 || p.PhanLoai == 2)) on vpct.IDTamUng equals tu.IDTamUng
                          select new { bn, vp, vpct, tu });
            var query1 = (from dv in data.DichVus
                          join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                          select new { dv, tn });

            var all = (from q0 in query0
                       join q1 in query1 on q0.vpct.MaDV equals q1.dv.MaDV
                       select new
                       {
                           q0.vpct.idVPhict,
                           q1.tn.TenTN,
                           q0.bn.MaBNhan,
                           q0.bn.TenBNhan,
                           q0.bn.GTinh,
                           q0.bn.Tuoi,
                           q0.bn.DChi,
                           q1.dv.IDNhom,
                           q1.dv.IdTieuNhom,
                           q0.vpct.SoLuong,
                           q0.vpct.ThanhTien,
                           q0.vpct.TienBN,
                           q0.vp.NgayTT,
                           q0.bn.DTuong,
                           q0.vpct.TrongBH,
                           q0.bn.NoiTru,
                           q0.tu.PhanLoai,
                           q0.tu.NgayThu
                       }).OrderBy(o => o.MaBNhan).ThenBy(o => o.IDNhom).ToList();

            var tnhom = (from a in all where a.IDNhom != idNhomthuoc select new { a.TenTN, a.IdTieuNhom, a.IDNhom }).OrderBy(p => p.IDNhom).Distinct().ToList();
            //if (tnhom.Count > 17)
            //{
            //    MessageBox.Show("Dữ liệu vượt giới hạn");
            //}
            int[] arrTn = new int[50];
            for (int i = 0; i < 50; i++)
            {
                if (i < tnhom.Count)
                    arrTn[i] = tnhom.Skip(i).Take(1).First().IdTieuNhom.Value;
                else
                    arrTn[i] = 0;
            }
            int colcount = tnhom.Count + 1;
            //if (tnhom.Count >= 17)
            //    colcount = 18;

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
                    case 18:
                        rep.n18.Value = tnhom[i - 2].TenTN.ToString();
                        break;
                    case 19:
                        rep2.n1.Value = tnhom[i - 2].TenTN.ToString();
                        break;
                    case 20:
                        rep2.n2.Value = tnhom[i - 2].TenTN.ToString();
                        break;
                    case 21:
                        rep2.n3.Value = tnhom[i - 2].TenTN.ToString();
                        break;
                    case 22:
                        rep2.n4.Value = tnhom[i - 2].TenTN.ToString();
                        break;
                    case 23:
                        rep2.n5.Value = tnhom[i - 2].TenTN.ToString();
                        break;
                    case 24:
                        rep2.n6.Value = tnhom[i - 2].TenTN.ToString();
                        break;
                    case 25:
                        rep2.n7.Value = tnhom[i - 2].TenTN.ToString();
                        break;
                    case 26:
                        rep2.n8.Value = tnhom[i - 2].TenTN.ToString();
                        break;
                    case 27:
                        rep2.n9.Value = tnhom[i - 2].TenTN.ToString();
                        break;
                    case 28:
                        rep2.n10.Value = tnhom[i - 2].TenTN.ToString();
                        break;
                    case 29:
                        rep2.n11.Value = tnhom[i - 2].TenTN.ToString();
                        break;
                    case 30:
                        rep2.n12.Value = tnhom[i - 2].TenTN.ToString();
                        break;
                    case 31:
                        rep2.n13.Value = tnhom[i - 2].TenTN.ToString();
                        break;
                    case 32:
                        rep2.n14.Value = tnhom[i - 2].TenTN.ToString();
                        break;
                    case 33:
                        rep2.n15.Value = tnhom[i - 2].TenTN.ToString();
                        break;
                    case 34:
                        rep2.n16.Value = tnhom[i - 2].TenTN.ToString();
                        break;
                    case 35:
                        rep2.n17.Value = tnhom[i - 2].TenTN.ToString();
                        break;
                    case 36:
                        rep2.n18.Value = tnhom[i - 2].TenTN.ToString();
                        break;
                    case 37:
                        rep2.n19.Value = tnhom[i - 2].TenTN.ToString();
                        break;
                    case 38:
                        rep2.n20.Value = tnhom[i - 2].TenTN.ToString();
                        break;
                    case 39:
                        rep2.n21.Value = tnhom[i - 2].TenTN.ToString();
                        break;
                    
                    
                }
            }
            int stt = 1;
            var b = (from a in all
                     group a by new { a.TenBNhan, a.MaBNhan, a.GTinh, a.DChi, a.Tuoi } into kq
                     select new
                     {
                         STT = stt++,
                         kq.Key.MaBNhan,
                         kq.Key.TenBNhan,
                         kq.Key.DChi,
                         TuoiNam = kq.Key.GTinh == 1 ? kq.Key.Tuoi : null,
                         TuoiNu = kq.Key.GTinh == 0 ? kq.Key.Tuoi : null,
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
                         tn23 = kq.Where(p => p.IdTieuNhom == arrTn[21]).Sum(p => p.TienBN),
                         tn24 = kq.Where(p => p.IdTieuNhom == arrTn[22]).Sum(p => p.TienBN),
                         tn25=  kq.Where(p => p.IdTieuNhom == arrTn[23]).Sum(p => p.TienBN),
                         tn26 = kq.Where(p => p.IdTieuNhom == arrTn[24]).Sum(p => p.TienBN),
                         tn27 = kq.Where(p => p.IdTieuNhom == arrTn[25]).Sum(p => p.TienBN),
                         tn28 = kq.Where(p => p.IdTieuNhom == arrTn[26]).Sum(p => p.TienBN),
                         tn29 = kq.Where(p => p.IdTieuNhom == arrTn[27]).Sum(p => p.TienBN),
                         tn30 = kq.Where(p => p.IdTieuNhom == arrTn[28]).Sum(p => p.TienBN),
                         tn31 = kq.Where(p => p.IdTieuNhom == arrTn[29]).Sum(p => p.TienBN),
                         tn32 = kq.Where(p => p.IdTieuNhom == arrTn[30]).Sum(p => p.TienBN),
                         tn33 = kq.Where(p => p.IdTieuNhom == arrTn[31]).Sum(p => p.TienBN),
                         tn34 = kq.Where(p => p.IdTieuNhom == arrTn[32]).Sum(p => p.TienBN),
                         tn35 = kq.Where(p => p.IdTieuNhom == arrTn[33]).Sum(p => p.TienBN),
                         tn36 = kq.Where(p => p.IdTieuNhom == arrTn[34]).Sum(p => p.TienBN),
                         tn37 = kq.Where(p => p.IdTieuNhom == arrTn[35]).Sum(p => p.TienBN),
                         tn38 = kq.Where(p => p.IdTieuNhom == arrTn[36]).Sum(p => p.TienBN),
                         tn39 = kq.Where(p => p.IdTieuNhom == arrTn[37]).Sum(p => p.TienBN),
                         tn40 = kq.Where(p => p.IdTieuNhom == arrTn[38]).Sum(p => p.TienBN),
                         tn41 = kq.Where(p => p.IdTieuNhom == arrTn[39]).Sum(p => p.TienBN)
                     }).ToList();
            var c = (from bc in b
                     select new
                     {
                         bc.STT,
                         bc.MaBNhan,
                         bc.TenBNhan,
                         bc.DChi,
                         bc.TuoiNam,
                         bc.TuoiNu,
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
                         tn24 = bc.tn24,
                         tn25 = bc.tn25,
                         tn26 = bc.tn26,
                         tn27 = bc.tn27,
                         tn28 = bc.tn28,
                         tn29 = bc.tn29,
                         tn30 = bc.tn30,
                         tn31 = bc.tn31,
                         tn32 = bc.tn32,
                         tn33 = bc.tn33,
                         tn34 = bc.tn34,
                         tn35 = bc.tn35,
                         tn36 = bc.tn36,
                         tn37 = bc.tn37,
                         tn38 = bc.tn38,
                         tn39 = bc.tn39,

                         TongCong1 = bc.tn1 + bc.tn2 + bc.tn3 + bc.tn4 + bc.tn5 + bc.tn6 + bc.tn7 + bc.tn8 + bc.tn9 + bc.tn10 + bc.tn11 + bc.tn12
                         + bc.tn13 + bc.tn14 + bc.tn15 + bc.tn16 + bc.tn17 + bc.tn18,
                         TongCong2 = bc.tn19 + bc.tn20 + bc.tn21 + bc.tn22 + bc.tn23 + bc.tn24 + bc.tn25 + bc.tn26 + bc.tn27 + bc.tn28 + bc.tn29 + bc.tn30
                         + bc.tn31 + bc.tn32 + bc.tn33 + bc.tn34 + bc.tn35 + bc.tn36 + bc.tn37 + bc.tn38 + bc.tn39
                     }).ToList();//.Where(p => p.TongCong != 0).ToList();

            rep.DataSource = c;
            rep2.DataSource = c;
            rep.BindingData();
            rep2.BindingData();
            rep.CreateDocument();
            rep2.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frmIn frm2 = new frmIn();
            frm2.prcIN.PrintingSystem = rep2.PrintingSystem;
            this.Hide();
            frm.ShowDialog();
            frm2.ShowDialog();
            this.Show();

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
            if (lupDoiTuong.Text != "BHYT")
            {
                cbo_trongDM.SelectedIndex = 2;
                cbo_trongDM.Properties.ReadOnly = true;
            }
            else
            {
                cbo_trongDM.Properties.ReadOnly = false;
            }
        }


    }
}