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
    public partial class frm_BkeVienPhi : DevExpress.XtraEditors.XtraForm
    {
        public frm_BkeVienPhi()
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

            DataTable tbCP = new DataTable();
            tbCP.Columns.Add("TenCP", typeof(string));
            tbCP.Columns.Add("MaCP", typeof(int));
            tbCP.Rows.Add("Tất cả", -1);
            tbCP.Rows.Add("Trong BH", 1);
            tbCP.Rows.Add("Ngoài BH", 0);
            tbCP.Rows.Add("CP Đính kèm", 2);
            lupCP.Properties.DataSource = tbCP;

            _lKPhong = data.KPhongs.OrderBy(p => p.TenKP).ToList();
            _lKPhong.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });

            cklKhoaPhong.DataSource = _lKPhong;
            cklKhoaPhong.CheckAll();
              
           // lupKhoaPhong.Properties.DataSource = _lKPhong;
            radioGroup1.SelectedIndex = 0;
            lupDoiTuong.EditValue = "tc";
            lupCP.EditValue = -1;           
            if (DungChung.Bien.MaBV == "12001")
                ckInMaThe.Checked = true;


        }
        public DataTable tb;
        private void btnInBC_Click(object sender, EventArgs e)
        {

            DateTime tungay = DungChung.Ham.NgayTu(dtTungay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
            List<int> _lkp = new List<int>();
            for (int i = 0; i < cklKhoaPhong.ItemCount; i++)
            {
                if (cklKhoaPhong.GetItemChecked(i))
                    _lkp.Add(Convert.ToInt32(cklKhoaPhong.GetItemValue(i)));
            }
            string doituong = "tc";
            int chiphi = -1;
          //  int khoaphong = 0;
            int noingoaitru = -1;
            if (lupDoiTuong.EditValue != null)
                doituong = lupDoiTuong.EditValue.ToString();
            if (lupCP.EditValue != null)
                chiphi = Convert.ToInt32(lupCP.EditValue);
            //if (lupKhoaPhong.EditValue != null)
            //    khoaphong = Convert.ToInt32( lupKhoaPhong.EditValue);
            if (radioGroup1.SelectedIndex == 0)// tất cả
                noingoaitru = -1;
            else if (radioGroup1.SelectedIndex == 1)// nội trú
                noingoaitru = 1;
            else if (radioGroup1.SelectedIndex == 2)// ngoại trú
                noingoaitru = 0;
            int idNhomthuoc = 0;
            var nt = data.NhomDVs.Where(p => p.TenNhomCT== ("Thuốc trong danh mục BHYT"));
            if (nt.Count() > 0)
                idNhomthuoc = nt.ToList().First().IDNhom;
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int CPtheoKp = rgchonkp.SelectedIndex;

            //var _lvienphi = data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay).ToList();
            //var _vienphict=from vp in 
            var dv_tn = (from a in data.DichVus
                         join b in data.TieuNhomDVs on a.IdTieuNhom equals b.IdTieuNhom
                         select new { 
                             IdTieuNhom = (a.TenDV.ToLower().Contains("soi cổ tử cung") && b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi && DungChung.Bien.MaBV == "26007") ? 500 : b.IdTieuNhom,
                             TenTN = (a.TenDV.ToLower().Contains("soi cổ tử cung") && b.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi && DungChung.Bien.MaBV == "26007") ? "Nội soi cổ tử cung" : b.TenTN,
                             a.IDNhom,
                             b.STT,
                             a.MaDV}).ToList();
            if (CPtheoKp == 0)
            {
                #region Theo kp ra viện

                var a3 = (from c1 in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                          join a in data.VienPhicts.Where(p => chiphi == -1 ? true : p.TrongBH == chiphi) on c1.idVPhi equals a.idVPhi
                          select new { c1.MaBNhan, c1.NgayTT, a.MaDV, a.ThanhTien, a.TienBN }).ToList();
                var cphi = (from dv in dv_tn
                                //join vpct in a1 on dv.MaDV equals vpct.MaDV
                                //join vp in a2  on vpct.idVPhi equals vp.idVPhi
                                join a in a3 on dv.MaDV equals a.MaDV
                                group new { a, dv } by new { a.MaBNhan, a.NgayTT, dv.IdTieuNhom, dv.TenTN, dv.IDNhom, dv.STT } into kq
                            select new
                            {
                                kq.Key.MaBNhan,
                                kq.Key.TenTN,
                                kq.Key.IdTieuNhom,
                                kq.Key.IDNhom,
                                kq.Key.STT,
                                ThanhTien = kq.Sum(p => p.a.ThanhTien),
                                kq.Key.NgayTT,
                                TienBN = kq.Sum(p => p.a.TienBN)
                            }).ToList();
                var all = (from vp in cphi
                           join bn in data.BenhNhans.Where(p => DungChung.Bien.MaBV == "26007" ? _lkp.Contains(p.MaKP ?? 0) : true) on vp.MaBNhan equals bn.MaBNhan
                           select new
                           {
                               bn.NamSinh,
                               bn.MaBNhan,
                               bn.SThe,
                               vp.TenTN,
                               bn.TenBNhan,
                               bn.GTinh,
                               Tuoi = (bn.Tuoi == 0) ? (DungChung.Ham.TuoitheoThang(data, bn.MaBNhan, DungChung.Bien.formatAge.ToString()).ToString() + "th") : bn.Tuoi.ToString(),
                               bn.DChi,
                               vp.IdTieuNhom,
                               vp.IDNhom,
                               vp.ThanhTien,
                               vp.NgayTT,
                               bn.DTuong,
                               bn.NoiTru,
                               vp.STT,
                               vp.TienBN
                           })
                     .Where(p => doituong == "tc" || p.DTuong == doituong)
                     .Where(p => noingoaitru == -1 || p.NoiTru == noingoaitru)

                     .ToList();

                //   var tnhom1 = (from a in all where a.IDNhom != idNhomthuoc orderby a.IDNhom select new { a.TenTN, a.IdTieuNhom }).Distinct().ToList();
                var tnhom = (from a in all where (a.IDNhom != idNhomthuoc ) select new { a.IDNhom, a.TenTN, a.IdTieuNhom, a.STT }).OrderBy(p => p.IDNhom).ThenBy(p => p.STT).Select(p => new { p.TenTN, p.IdTieuNhom }).Distinct().ToList();
                if (tnhom.Count > 18)
                {
                    MessageBox.Show("Dữ liệu vượt giới hạn");
                }
                int[] arrTn = new int[20];
                for (int i = 0; i < 20; i++)
                {
                    if (i < tnhom.Count)
                        arrTn[i] = tnhom.Skip(i).Take(1).First().IdTieuNhom;
                    else
                        arrTn[i] = 0;
                }
                int colcount = tnhom.Count + 1;
                if (tnhom.Count >= 17)
                    colcount = 18;
                int stt = 1;
                var vv = (from a in all
                          join vaovien in data.VaoViens on a.MaBNhan equals vaovien.MaBNhan
                          select new { a.MaBNhan, vaovien.SoBA }).Distinct().ToList();
                var b = (from a in all
                         join rv in data.RaViens.Where(p => DungChung.Bien.MaBV != "26007" ? _lkp.Contains(p.MaKP) : true)
                             //.Where(p => khoaphong == 0 || p.MaKP == khoaphong) 
                         on a.MaBNhan equals rv.MaBNhan
                         join kp in data.KPhongs on rv.MaKP equals kp.MaKP
                         group new { a, rv, kp } by new { rv.MaICD, rv.MaKP, rv.NgayVao, rv.NgayRa, kp.TenKP, a.NamSinh, a.TenBNhan, a.MaBNhan, a.SThe, a.GTinh, a.DChi, rv.SoNgaydt, a.Tuoi, rv.ChanDoan } into kq
                         select new
                         {
                             STT = stt++,
                             kq.Key.MaBNhan,
                             kq.Key.TenBNhan,
                             kq.Key.DChi,
                             kq.Key.SThe,
                             kq.Key.MaKP,
                             kq.Key.TenKP,
                             //TuoiNam = kq.Key.GTinh == 1 ? kq.Key.Tuoi : "",
                             //TuoiNu = kq.Key.GTinh == 0 ? kq.Key.Tuoi : "",
                             Tuoi = kq.Key.Tuoi,
                             NgayRa = kq.Key.NgayRa == null ? "" : kq.Key.NgayRa.Value.ToString("dd/MM"),
                             NgayVao = kq.Key.NgayVao == null ? "" : kq.Key.NgayVao.Value.ToString("dd/MM"),
                             NamSinh = kq.Key.NamSinh,
                             ChanDoan = kq.Key.ChanDoan + " - " + kq.Key.MaICD,
                             tn1 = kq.Where(p => p.a.IDNhom == idNhomthuoc).Sum(p => p.a.ThanhTien),
                             tn2 = kq.Where(p => p.a.IdTieuNhom == arrTn[0]).Sum(p => p.a.ThanhTien),
                             tn3 = kq.Where(p => p.a.IdTieuNhom == arrTn[1]).Sum(p => p.a.ThanhTien),
                             tn4 = kq.Where(p => p.a.IdTieuNhom == arrTn[2]).Sum(p => p.a.ThanhTien),
                             tn5 = kq.Where(p => p.a.IdTieuNhom == arrTn[3]).Sum(p => p.a.ThanhTien),
                             tn6 = kq.Where(p => p.a.IdTieuNhom == arrTn[4]).Sum(p => p.a.ThanhTien),
                             tn7 = kq.Where(p => p.a.IdTieuNhom == arrTn[5]).Sum(p => p.a.ThanhTien),
                             tn8 = kq.Where(p => p.a.IdTieuNhom == arrTn[6]).Sum(p => p.a.ThanhTien),
                             tn9 = kq.Where(p => p.a.IdTieuNhom == arrTn[7]).Sum(p => p.a.ThanhTien),
                             tn10 = kq.Where(p => p.a.IdTieuNhom == arrTn[8]).Sum(p => p.a.ThanhTien),
                             tn11 = kq.Where(p => p.a.IdTieuNhom == arrTn[9]).Sum(p => p.a.ThanhTien),
                             tn12 = kq.Where(p => p.a.IdTieuNhom == arrTn[10]).Sum(p => p.a.ThanhTien),
                             tn13 = kq.Where(p => p.a.IdTieuNhom == arrTn[11]).Sum(p => p.a.ThanhTien),
                             tn14 = kq.Where(p => p.a.IdTieuNhom == arrTn[12]).Sum(p => p.a.ThanhTien),
                             tn15 = kq.Where(p => p.a.IdTieuNhom == arrTn[13]).Sum(p => p.a.ThanhTien),
                             tn16 = kq.Where(p => p.a.IdTieuNhom == arrTn[14]).Sum(p => p.a.ThanhTien),
                             tn17 = kq.Where(p => p.a.IdTieuNhom == arrTn[15]).Sum(p => p.a.ThanhTien),
                             tn18 = kq.Where(p => DungChung.Bien.MaBV == "26007" ? p.a.IdTieuNhom == arrTn[16] : p.a.IdTieuNhom == arrTn[16]).Sum(p => p.a.ThanhTien),
                             tn19 = DungChung.Bien.MaBV == "26007" ? kq.Where(p => p.a.IDNhom == arrTn[17]).Sum(p => p.a.ThanhTien) : 0,
                             tn20 = kq.Sum(p => p.a.TienBN)
                         }).ToList();
                //}
                var c = from bc in b
                        select new
                        {
                            SoVV = vv.Where(p => p.MaBNhan == bc.MaBNhan).Select(p => p.SoBA).ToList().Count > 0 ? vv.Where(p => p.MaBNhan == bc.MaBNhan).Select(p => p.SoBA).First() : "",
                            bc.STT,
                            bc.MaBNhan,
                            bc.TenBNhan,
                            bc.SThe,
                            bc.DChi,
                            bc.ChanDoan,
                            bc.NgayRa,
                            bc.NgayVao,
                            bc.NamSinh,
                            bc.MaKP,
                            bc.TenKP,
                            //bc.TuoiNam,
                            //bc.TuoiNu,
                            bc.Tuoi,
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
                            TongCong = bc.tn1 + bc.tn2 + bc.tn3 + bc.tn4 + bc.tn5 + bc.tn6 + bc.tn7 + bc.tn8 + bc.tn9 + bc.tn10 + bc.tn11 + bc.tn12
                            + bc.tn13 + bc.tn14 + bc.tn15 + bc.tn16 + bc.tn17 + bc.tn18 + (DungChung.Bien.MaBV == "26007" ? (bc.tn19) : 0)
                        };

                if (ckInMaThe.Checked)
                {
                    if (DungChung.Bien.MaBV == "26007")
                    {
                        frmIn frm = new frmIn();
                        BaoCao.rep_BkeVienPhi_InMaThe_26007 rep = new BaoCao.rep_BkeVienPhi_InMaThe_26007();
                        #region add para baocao
                        rep.Ngay.Value = "Từ ngày:  " + dtTungay.Text + "    đến ngày:  " + dtDenNgay.Text;
                        rep.TieuDe.Value = ("Bảng kê chi phí tiền thuốc và dịch vụ bảo hiểm y tế").ToUpper();
                        rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();

                        #endregion
                        #region in tiêu đề cột
                        
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
                                    rep.n19.Value = tnhom[i - 2].TenTN.ToString();
                                    break;  
                            }
                        }
                        #endregion
                        rep.DataSource = c.OrderBy(p => p.STT).ThenBy(p => p.NgayRa);
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        this.Hide();
                        frm.ShowDialog();
                        this.Show();
                    }
                    else
                    {
                        frmIn frm = new frmIn();
                        BaoCao.rep_BkeVienPhi_InMaThe rep = new BaoCao.rep_BkeVienPhi_InMaThe();
                        #region add para baocao
                        rep.Ngay.Value = "Từ ngày:  " + dtTungay.Text + "    đến ngày:  " + dtDenNgay.Text;
                        rep.TieuDe.Value = ("Bảng kê chi phí tiền thuốc và dịch vụ bảo hiểm y tế").ToUpper();
                        rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();

                        #endregion
                        #region in tiêu đề cột
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
                            }
                        }
                        #endregion
                        rep.DataSource = c.OrderBy(p => p.STT).ThenBy(p => p.NgayRa);
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        this.Hide();
                        frm.ShowDialog();
                        this.Show();
                    }
                }
                else
                {
                    if (DungChung.Bien.MaBV == "26007")
                    {
                        frmIn frm = new frmIn();
                        BaoCao.rep_BkeVienPhi_26007 rep = new BaoCao.rep_BkeVienPhi_26007(rdMauIn.SelectedIndex);
                        #region add para baocao
                        rep.Ngay.Value = "Từ ngày:  " + dtTungay.Text + "    đến ngày:  " + dtDenNgay.Text;
                        rep.TieuDe.Value = ("Bảng kê chi phí tiền thuốc và dịch vụ bảo hiểm y tế").ToUpper();
                        rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();

                        #endregion
                        #region in tiêu đề cột
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
                                    rep.tn19.Value = tnhom[i - 2].TenTN.ToString();
                                    break;   
                            }
                        }
                        #endregion
                        rep.DataSource = c.OrderBy(p => p.STT).ThenBy(p => p.NgayRa);
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        this.Hide();
                        frm.ShowDialog();
                        this.Show();
                    }
                    else
                    {
                        frmIn frm = new frmIn();
                        BaoCao.rep_BkeVienPhi rep = new BaoCao.rep_BkeVienPhi(rdMauIn.SelectedIndex);
                        #region add para baocao
                        rep.Ngay.Value = "Từ ngày:  " + dtTungay.Text + "    đến ngày:  " + dtDenNgay.Text;
                        rep.TieuDe.Value = ("Bảng kê chi phí tiền thuốc và dịch vụ bảo hiểm y tế").ToUpper();
                        rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();

                        #endregion
                        #region in tiêu đề cột
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
                            }
                        }
                        #endregion
                        rep.DataSource = c.OrderBy(p => p.STT).ThenBy(p => p.NgayRa);
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        this.Hide();
                        frm.ShowDialog();
                        this.Show();
                    }
                }
                #endregion
            }
            else
            {
                #region theo kp chỉ định

                var a3 = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                          join dt in data.DThuocs on vp.MaBNhan equals dt.MaBNhan
                          join a in data.DThuoccts.Where(p => _lkp.Contains(p.MaKP ?? 0)).Where(p => chiphi == -1 ? true : p.TrongBH == chiphi) on dt.IDDon equals a.IDDon
                          select new { vp.MaBNhan, a.MaDV, a.ThanhTien, a.MaKP, vp.NgayTT }).ToList();
                var cphi = (from dv in dv_tn
                            join vpct in a3 on dv.MaDV equals vpct.MaDV
                            group new { vpct, dv } by new { vpct.MaBNhan, vpct.NgayTT, dv.IdTieuNhom, dv.TenTN, dv.IDNhom, dv.STT, vpct.MaKP }
                            into kq
                            select new
                            {
                                kq.Key.MaBNhan,
                                kq.Key.TenTN,
                                kq.Key.IdTieuNhom,
                                kq.Key.IDNhom,
                                kq.Key.STT,
                                ThanhTien = kq.Sum(p => p.vpct.ThanhTien),
                                //TienBN = kq.Sum(p => p.vpct.TienBN),
                                kq.Key.NgayTT,
                                kq.Key.MaKP
                            })
                  .ToList();
                var all = (from vp in cphi
                           join bn in data.BenhNhans on vp.MaBNhan equals bn.MaBNhan

                           select new
                           {
                               bn.NamSinh,
                               bn.MaBNhan,
                               bn.SThe,
                               vp.TenTN,
                               bn.TenBNhan,
                               bn.GTinh,
                               Tuoi = (bn.Tuoi == 0) ? (DungChung.Ham.TuoitheoThang(data,bn.MaBNhan,DungChung.Bien.formatAge.ToString()).ToString() + "th") : bn.Tuoi.ToString(),
                               bn.DChi,
                               vp.IdTieuNhom,
                               vp.IDNhom,
                               vp.ThanhTien,
                               //vp.TienBN,
                               vp.NgayTT,
                               bn.DTuong,
                               bn.NoiTru,
                               vp.STT,
                               vp.MaKP
                           })
                     .Where(p => doituong == "tc" || p.DTuong == doituong)
                     .Where(p => noingoaitru == -1 || p.NoiTru == noingoaitru)

                     .ToList();

                //   var tnhom1 = (from a in all where a.IDNhom != idNhomthuoc orderby a.IDNhom select new { a.TenTN, a.IdTieuNhom }).Distinct().ToList();
                var tnhom = (from a in all where (a.IDNhom != idNhomthuoc) select new { a.IDNhom, a.TenTN, a.IdTieuNhom, a.STT }).OrderBy(p => p.IDNhom).ThenBy(p => p.STT).Select(p => new { p.TenTN, p.IdTieuNhom }).Distinct().ToList();
                if (tnhom.Count > 18)
                {
                    MessageBox.Show("Dữ liệu vượt giới hạn");
                }
                int[] arrTn = new int[20];
                for (int i = 0; i < 20; i++)
                {
                    if (i < tnhom.Count)
                        arrTn[i] = tnhom.Skip(i).Take(1).First().IdTieuNhom;
                    else
                        arrTn[i] = 0;
                }
                int colcount = tnhom.Count + 1;
                if (tnhom.Count >= 18)
                    colcount = 19;

                int stt = 1;
                var vv = (from a in all
                          join vaovien in data.VaoViens on a.MaBNhan equals vaovien.MaBNhan
                          select new { a.MaBNhan, vaovien.SoBA }).Distinct().ToList();
                   var b = (from a in all
                         join rv in data.RaViens
                             //.Where(p => khoaphong == 0 || p.MaKP == khoaphong) 
                         on a.MaBNhan equals rv.MaBNhan
                         join kp in data.KPhongs on a.MaKP equals kp.MaKP
                         group new { a, rv, kp } by new { rv.MaICD, rv.MaKP, rv.NgayVao, rv.NgayRa, kp.TenKP, a.NamSinh, a.TenBNhan, a.MaBNhan, a.SThe, a.GTinh, a.DChi, rv.SoNgaydt, a.Tuoi, rv.ChanDoan } into kq
                         select new
                         {
                             STT = stt++,
                             kq.Key.MaBNhan,
                             kq.Key.TenBNhan,
                             kq.Key.DChi,
                             kq.Key.SThe,
                             kq.Key.MaKP,
                             kq.Key.TenKP,
                             Tuoi = kq.Key.Tuoi,
                             NgayRa = kq.Key.NgayRa == null ? "" : kq.Key.NgayRa.Value.ToString("dd/MM"),
                             NgayVao = kq.Key.NgayVao == null ? "" : kq.Key.NgayVao.Value.ToString("dd/MM"),
                             NamSinh = kq.Key.NamSinh,
                             ChanDoan = kq.Key.ChanDoan + " - " + kq.Key.MaICD,
                             tn1 = kq.Where(p => p.a.IDNhom == idNhomthuoc ).Sum(p => p.a.ThanhTien),
                             tn2 = kq.Where(p => p.a.IdTieuNhom == arrTn[0] ).Sum(p => p.a.ThanhTien),
                             tn3 = kq.Where(p => p.a.IdTieuNhom == arrTn[1] ).Sum(p => p.a.ThanhTien),
                             tn4 = kq.Where(p => p.a.IdTieuNhom == arrTn[2] ).Sum(p => p.a.ThanhTien),
                             tn5 = kq.Where(p => p.a.IdTieuNhom == arrTn[3] ).Sum(p => p.a.ThanhTien),
                             tn6 = kq.Where(p => p.a.IdTieuNhom == arrTn[4] ).Sum(p => p.a.ThanhTien),
                             tn7 = kq.Where(p => p.a.IdTieuNhom == arrTn[5] ).Sum(p => p.a.ThanhTien),
                             tn8 = kq.Where(p => p.a.IdTieuNhom == arrTn[6] ).Sum(p => p.a.ThanhTien),
                             tn9 = kq.Where(p => p.a.IdTieuNhom == arrTn[7] ).Sum(p => p.a.ThanhTien),
                             tn10 = kq.Where(p => p.a.IdTieuNhom == arrTn[8] ).Sum(p => p.a.ThanhTien),
                             tn11 = kq.Where(p => p.a.IdTieuNhom == arrTn[9] ).Sum(p => p.a.ThanhTien),
                             tn12 = kq.Where(p => p.a.IdTieuNhom == arrTn[10] ).Sum(p => p.a.ThanhTien),
                             tn13 = kq.Where(p => p.a.IdTieuNhom == arrTn[11] ).Sum(p => p.a.ThanhTien),
                             tn14 = kq.Where(p => p.a.IdTieuNhom == arrTn[12] ).Sum(p => p.a.ThanhTien),
                             tn15 = kq.Where(p => p.a.IdTieuNhom == arrTn[13] ).Sum(p => p.a.ThanhTien),
                             tn16 = kq.Where(p => p.a.IdTieuNhom == arrTn[14] ).Sum(p => p.a.ThanhTien),
                             tn17 = kq.Where(p => p.a.IdTieuNhom == arrTn[15] ).Sum(p => p.a.ThanhTien),
                             tn18 = kq.Where(p => DungChung.Bien.MaBV == "26007" ? p.a.IdTieuNhom == arrTn[16] : p.a.IdTieuNhom == arrTn[16]).Sum(p => p.a.ThanhTien),
                             tn19 = DungChung.Bien.MaBV == "26007" ? kq.Where(p => p.a.IDNhom == arrTn[17]).Sum(p => p.a.ThanhTien) : 0,
                             //tn20 = kq.Sum(p => p.a.TienBN)
                         }).ToList();
                //}
                var c = from bc in b
                        select new
                        {
                            SoVV = vv.Where(p => p.MaBNhan == bc.MaBNhan).Select(p => p.SoBA).ToList().Count > 0 ? vv.Where(p => p.MaBNhan == bc.MaBNhan).Select(p => p.SoBA).First() : "",
                            bc.STT,
                            bc.MaBNhan,
                            bc.TenBNhan,
                            bc.SThe,
                            bc.DChi,
                            bc.ChanDoan,
                            bc.NgayRa,
                            bc.NgayVao,
                            bc.NamSinh,
                            bc.MaKP,
                            bc.TenKP,
                            //bc.TuoiNam,
                            //bc.TuoiNu,
                            bc.Tuoi,
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
                            //tn20 = bc.tn20,
                            TongCong = bc.tn1 + bc.tn2 + bc.tn3 + bc.tn4 + bc.tn5 + bc.tn6 + bc.tn7 + bc.tn8 + bc.tn9 + bc.tn10 + bc.tn11 + bc.tn12
                            + bc.tn13 + bc.tn14 + bc.tn15 + bc.tn16 + bc.tn17 + bc.tn18 + (DungChung.Bien.MaBV == "26007" ? (bc.tn19) : 0)//(DungChung.Bien.MaBV == "26007" ? 0 : bc.tn19)//coltn20 đoài 25-05
                        };

                if (ckInMaThe.Checked)
                {
                    frmIn frm = new frmIn();

                    BaoCao.rep_BkeVienPhi_InMaThe_26007 rep = new BaoCao.rep_BkeVienPhi_InMaThe_26007();
                    #region add para baocao
                    rep.Ngay.Value = "Từ ngày:  " + dtTungay.Text + "    đến ngày:  " + dtDenNgay.Text;
                    rep.TieuDe.Value = ("Bảng kê chi phí tiền thuốc và dịch vụ bảo hiểm y tế").ToUpper();
                    rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();

                    #endregion
                    #region in tiêu đề cột
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
                                rep.n19.Value = tnhom[i - 2].TenTN.ToString();
                                break;
                        }
                    }
                    #endregion
                    rep.DataSource = c.OrderBy(p => p.STT).ThenBy(p => p.NgayRa);
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    this.Hide();
                    frm.ShowDialog();
                    this.Show();
                }
                else
                {
                    if (DungChung.Bien.MaBV == "26007")
                    {
                        frmIn frm = new frmIn();
                        BaoCao.rep_BkeVienPhi_26007 rep = new BaoCao.rep_BkeVienPhi_26007(rdMauIn.SelectedIndex);
                        #region add para baocao
                        rep.Ngay.Value = "Từ ngày:  " + dtTungay.Text + "    đến ngày:  " + dtDenNgay.Text;
                        rep.TieuDe.Value = ("Bảng kê chi phí tiền thuốc và dịch vụ bảo hiểm y tế").ToUpper();
                        rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();

                        #endregion
                        #region in tiêu đề cột
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
                                    rep.tn19.Value = tnhom[i - 2].TenTN.ToString();
                                    break;
                            }
                        }
                        #endregion
                        rep.DataSource = c.OrderBy(p => p.STT).ThenBy(p => p.NgayRa);
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        this.Hide();
                        frm.ShowDialog();
                        this.Show();
                    }
                    else
                    {
                        frmIn frm = new frmIn();
                        BaoCao.rep_BkeVienPhi rep = new BaoCao.rep_BkeVienPhi(rdMauIn.SelectedIndex);
                        #region add para baocao
                        rep.Ngay.Value = "Từ ngày:  " + dtTungay.Text + "    đến ngày:  " + dtDenNgay.Text;
                        rep.TieuDe.Value = ("Bảng kê chi phí tiền thuốc và dịch vụ bảo hiểm y tế").ToUpper();
                        rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();

                        #endregion
                        #region in tiêu đề cột
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
                            }
                        }
                        #endregion
                        rep.DataSource = c.OrderBy(p => p.STT).ThenBy(p => p.NgayRa);
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        this.Hide();
                        frm.ShowDialog();
                        this.Show();
                    }
                }
                #endregion

            }

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cklKhoaPhong_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                if (cklKhoaPhong.GetItemChecked(0) == true)
                    cklKhoaPhong.CheckAll();
                else
                    cklKhoaPhong.UnCheckAll();
            }
        }


    }
}