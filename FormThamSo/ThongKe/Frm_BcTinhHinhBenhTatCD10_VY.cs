using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormThamSo
{
    public partial class Frm_BcTinhHinhBenhTatCD10_VY : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcTinhHinhBenhTatCD10_VY()
        {
            InitializeComponent();
        }
        private bool KTtaoBc()
        {

            if (lupTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupTuNgay.Focus();
                return false;
            }
            if (lupDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
                return false;
            }
            else return true;
        }
        //private string theoquy()
        //{
        //    string quy = "";

        //    if (radIn.SelectedIndex == 1)
        //    {
        //        switch (timquy(lupTuNgay.DateTime.Month))
        //        {
        //            case 1:
        //                quy = " Báo cáo quý I năm " + lupDenNgay.DateTime.ToString().Substring(6, 4);
        //                break;
        //            case 2:
        //                quy = " Báo cáo quý II năm " + lupDenNgay.DateTime.ToString().Substring(6, 4);
        //                break;
        //            case 3:
        //                quy = " Báo cáo quý III năm " + lupDenNgay.DateTime.ToString().Substring(6, 4);
        //                break;
        //            case 4:
        //                quy = " Báo cáo quý IV năm " + lupDenNgay.DateTime.ToString().Substring(6, 4);
        //                break;
        //        }

        //    }
        //    if (radIn.SelectedIndex == 2)
        //    {
        //        quy = "Báo cáo năm " + lupDenNgay.DateTime.ToString().Substring(6, 4);

        //    }
        //    else if (radIn.SelectedIndex == 0)
        //    {
        //        quy = "Từ ngày  " + lupTuNgay.DateTime.ToString().Substring(0, 10) + "  đến ngày  " + lupDenNgay.DateTime.ToString().Substring(0, 10);
        //    }
        //    return quy;
        //}

        //private int timquy(int month)
        //{
        //    if (month >= 1 && month <= 3)
        //    {
        //        return (1);
        //    }
        //    if (month > 3 && month <= 6)
        //    {
        //        return (2);
        //    }
        //    if (month > 6 && month <= 9)
        //    {
        //        return (3);
        //    }
        //    else { return (4); }
        //}-bỏ
        private void Frm_BcTinhHinhBenhTatCD10_VY_Load(object sender, EventArgs e)
        {
            lupDenNgay.DateTime = System.DateTime.Now;
            lupTuNgay.DateTime = System.DateTime.Now;
            lupTuNgay.Focus();

            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var q = data.DTBNs.ToList();
            cklDTBN.DataSource = q;
            for (int i = 0; i < cklDTBN.ItemCount; i++)
            {
                if (cklDTBN.GetItemText(i) == "KSK")
                    cklDTBN.SetItemChecked(i, false);
                else
                    cklDTBN.SetItemChecked(i, true);
            }

        }

        private class ICD10
        {
            private int? sTTCB;

            public int? STTCB
            {
                get { return sTTCB; }
                set { sTTCB = value; }
            }

            public string _STTChuong { get; set; }
            private string tenCB;

            public string TenCB
            {
                get { return tenCB; }
                set { tenCB = value; }
            }
            private string nhomCB;

            public string NhomCB
            {
                get { return nhomCB; }
                set { nhomCB = value; }
            }
            private int? sTTICD;

            public int? STTICD
            {
                get { return sTTICD; }
                set { sTTICD = value; }
            }
            private string maICD;

            public string MaICD
            {
                get { return maICD; }
                set { maICD = value; }
            }
            private string tenWHO;

            public string TenWHO
            {
                get { return tenWHO; }
                set { tenWHO = value; }
            }
            private string tenWHOe;

            public string TenWHOe
            {
                get { return tenWHOe; }
                set { tenWHOe = value; }
            }
            private string tenBenh;

            public string TenBenh
            {
                get { return tenBenh; }
                set { tenBenh = value; }
            }
            private int? kBTS;

            public int? KBTS
            {
                get { return kBTS; }
                set { kBTS = value; }
            }
            private int? kBNu;

            public int? KBNu
            {
                get { return kBNu; }
                set { kBNu = value; }
            }
            private int? kBTE;

            public int? KBTE
            {
                get { return kBTE; }
                set { kBTE = value; }
            }
            private int? kBTuVong;

            public int? KBTuVong
            {
                get { return kBTuVong; }
                set { kBTuVong = value; }
            }
            private int? dTMacTS;

            public int? DTMacTS
            {
                get { return dTMacTS; }
                set { dTMacTS = value; }
            }
            private int? dTMacNu;

            public int? DTMacNu
            {
                get { return dTMacNu; }
                set { dTMacNu = value; }
            }
            private int? dTTuVongTS;

            public int? DTTuVongTS
            {
                get { return dTTuVongTS; }
                set { dTTuVongTS = value; }
            }
            private int? dTTuVongNu;

            public int? DTTuVongNu
            {
                get { return dTTuVongNu; }
                set { dTTuVongNu = value; }
            }
            private int? dTTEMacTS;

            public int? DTTEMacTS
            {
                get { return dTTEMacTS; }
                set { dTTEMacTS = value; }
            }
            private int? dTTEMac5;

            public int? DTTEMac5
            {
                get { return dTTEMac5; }
                set { dTTEMac5 = value; }
            }
            private int? dTTETuVongTS;

            public int? DTTETuVongTS
            {
                get { return dTTETuVongTS; }
                set { dTTETuVongTS = value; }
            }
            private int? dTTETuVong5;

            public int? DTTETuVong5
            {
                get { return dTTETuVong5; }
                set { dTTETuVong5 = value; }
            }

            private int? sumICD;

            public int? SumICD
            {
                get { return sumICD; }
                set { sumICD = value; }
            }

            private int? soNgaydtTS;

            public int? SoNgaydtTS
            {
                get { return soNgaydtTS; }
                set { soNgaydtTS = value; }
            }


            private int? soNgaydt6;

            public int? SoNgaydt6
            {
                get { return soNgaydt6; }
                set { soNgaydt6 = value; }
            }
            public int? BN_NangxinveTS { get; set; }
            public int? BN_NangXinVeNu { get; set; }
            public int? BN_NangxinveTaiKhoa { get; set; }
            public int? TuVongTaiVien { get; set; }
            public int? TuVongTruocVien { get; set; }
            public int? TuVongDuocCapPhep { get; set; }


        }

        List<ICD10> _ICD10 = new List<ICD10>();
        /// <summary>
        /// Kiểm tra chuỗi a có chứa chuỗi b ko
        /// </summary>        
        /// <returns>trả về tru nếu có chứa, trả về false nếu không chứa</returns>
        private bool checkContain(string a, string b)
        {
            bool rs = false;
            List<string> list = a.Split(',').ToList();
            foreach (string c in list)
            {
                if (c == b)
                {
                    rs = true;
                    break;
                }
            }
            return rs;

        }
        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime ngaytu = System.DateTime.Now.Date;
            DateTime ngayden = System.DateTime.Now.Date;

            if (KTtaoBc())
            {
                ngaytu = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupDenNgay.DateTime);

                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                //List<KPhong> _lkp = data.KPhongs.ToList();
                List<int> _listDTBN = new List<int>();
                for (int i = 0; i < cklDTBN.ItemCount; i++)
                {
                    if (cklDTBN.GetItemChecked(i))
                        _listDTBN.Add(Convert.ToInt32(cklDTBN.GetItemValue(i)));
                }

                _ICD10.Clear();

                var q1 = (from rv in data.RaViens.Where(p => p.NgayRa >= ngaytu && p.NgayRa <= ngayden)
                          join bn in data.BenhNhans/**.Where(p => radXP.SelectedIndex == 3 || p.TuyenDuoi == radXP.SelectedIndex)*/ on rv.MaBNhan equals bn.MaBNhan
                          join dtbn in data.DTBNs.Where(p => _listDTBN.Contains(p.IDDTBN)) on bn.IDDTBN equals dtbn.IDDTBN
                          select new
                          { 
                              rv.KetQua, 
                              rv.MaBNhan,
                              bn.CapCuu, 
                              bn.Tuoi, 
                              bn.GTinh, 
                              rv.MaICD,
                              bn.NoiTru,
                              SoNgaydt = rv.SoNgaydt != null ? rv.SoNgaydt : 0, 
                              MaKP = rv.MaKP, 
                              PhanLoai = "" 
                          }).ToList();

                if (check1.Checked == true)
                {
                    q1 = (from rv in data.BNKBs.Where(p => p.NgayKham >= ngaytu && p.NgayKham <= ngayden)
                          join kp in data.KPhongs on rv.MaKP equals kp.MaKP
                          join bn in data.BenhNhans.Where(p => radXP.SelectedIndex == 3 || p.TuyenDuoi == radXP.SelectedIndex) on rv.MaBNhan equals bn.MaBNhan
                          join dtbn in data.DTBNs.Where(p => _listDTBN.Contains(p.IDDTBN)) on bn.IDDTBN equals dtbn.IDDTBN
                          join rv1 in data.RaViens.Where(p => p.NgayRa >= ngaytu && p.NgayRa <= ngayden) on bn.MaBNhan equals rv1.MaBNhan into k
                          from k1 in k.DefaultIfEmpty()
                          select new 
                          { 
                              KetQua = k1 != null ? k1.KetQua : "", 
                              bn.MaBNhan,
                              bn.CapCuu,
                              bn.Tuoi, 
                              bn.GTinh, 
                              rv.MaICD,
                              bn.NoiTru,
                              SoNgaydt = k1 != null ? k1.SoNgaydt : 0, 
                              MaKP = rv.MaKP ?? 0, 
                              PhanLoai = kp.PLoai
                          }).ToList();
                }
                var q3 = q1.Where(p => p.MaICD != null && p.MaICD.Length >= 3).ToList();
                var q4 = (from q in q3
                          select new
                          {
                              q.KetQua,
                              q.SoNgaydt,
                              q.MaBNhan,
                              q.Tuoi,
                              q.GTinh,
                              q.NoiTru,
                              q.PhanLoai,
                              q.CapCuu,
                              MaICD = q.MaICD.Substring(0, 3)
                          }).ToList();
                List<ICDSai> _lsai1 = (from a in q1 where a.MaICD == null || a.MaICD.Length < 3 select new ICDSai { MaBN = a.MaBNhan, MaICD1 = a.MaICD, Noitru = a.NoiTru == 1 ? "Nội trú" : "Ngoại trú", Mess = "kích thước Mã ICD <3" }).ToList();
                List<string> _listICD = data.ICD10.Where(p => p.MaICD.Length == 3).Select(p => p.MaICD).ToList();

                //List<ICDSai> _lsai2 = (from a in q4
                //                       join b in _listICD on a.MaICD equals b into kq
                //                       from kq1 in kq.DefaultIfEmpty()
                //                       where kq1 == null
                //                       select new ICDSai { MaBN = a.MaBNhan, MaICD1 = a.MaICD, Noitru = a.NoiTru == 1 ? "Nội trú" : "Ngoại trú" , Mess = "Mã ICD chưa đúng" }).ToList();             

                //_lsai1.AddRange(_lsai2);

                var _listMBICD = (from a in data.MBICDs.Where(p => p.STATUS == true)
                                  join b in data.CBICDs on a.IDCB equals b.IDCB
                                  select new
                                  {
                                      a.ID_MBICD,
                                      a.MaICD,
                                      a.IDCB,
                                      a.STATUS,
                                      a.STT,
                                      a.TenWHO,
                                      a.TenWHOe,
                                      a.DS_MaICD,
                                      b.TENCB,
                                      b.MSCB,
                                      b.NHOMCB
                                  }).ToList();
                List<ICD10> _lICD10 = new List<ICD10>();
                #region kiểm tra các mã ICD trong bảng ra viện có tồn tại trong bảng MBICd không
                foreach (var a in q4)
                {
                    int kt = 0;
                    foreach (var b in _listMBICD)
                    {
                        if (b.DS_MaICD.Contains(a.MaICD))
                        {
                            kt = 1;
                            break;
                        }
                    }
                    if (kt == 0)
                    {
                        ICDSai sai3 = new ICDSai();
                        sai3.MaBN = a.MaBNhan;
                        sai3.MaICD1 = a.MaICD;
                        sai3.Noitru = a.NoiTru == 1 ? "Nội trú" : "Ngoại trú";
                        sai3.Mess = "Mã ICD không tồn tại trong bảng MBICD";
                        _lsai1.Add(sai3);

                    }
                }
                lblSL.Text = _lsai1.Count.ToString();
                lblNoitru.Text = _lsai1.Where(p => p.Noitru == "Nội trú").ToList().Count().ToString();
                grc_BenhNhanChuaCoICD.DataSource = _lsai1;

                #endregion kiểm tra

                foreach (var a in _listMBICD)
                {
                    var q5 = q4.Where(p => checkContain(a.DS_MaICD, p.MaICD)).ToList();
                    var q6 = q5.Where(p => ((DungChung.Bien.MaBV == "30009" || DungChung.Bien.MaBV == "20001") && check1.Checked == true) ? p.PhanLoai.Contains("Phòng khám") : p.NoiTru == 0).Distinct().ToList();
                    var q7 = q5.Where(p => ((DungChung.Bien.MaBV == "30009" || DungChung.Bien.MaBV == "20001") && check1.Checked == true) ? p.PhanLoai.Contains("Lâm sàng") : p.NoiTru == 1).Distinct().ToList();
                    var q8 = q5.Where(p => p.KetQua == "Tử vong").ToList();
                    var q9 = q5.Where(p => p.KetQua == "Nặng hơn").Distinct().ToList();
                    var q10 = q5.Where(p => p.CapCuu == 2).ToList();
                    
                    //var q10 = (from t in q4 join bn in data.BenhNhans on t.MaBNhan equals bn.MaBNhan select new { t,bn.CapCuu}).ToList();

                    ICD10 newICD = new ICD10();
                    newICD.MaICD = a.MaICD;
                    newICD.STTCB = a.MSCB;// số thứ tự chương bệnh
                    newICD._STTChuong = "C0" + a.MSCB;
                    newICD.TenCB = a.TENCB;
                    newICD.NhomCB = a.NHOMCB;
                    newICD.STTICD = a.STT ?? 0;
                    newICD.TenBenh = a.TenWHO + " - " + a.TenWHOe;
                    newICD.TenWHO = a.TenWHO;
                    newICD.TenWHOe = a.TenWHOe;
                    newICD.KBTS = q6.Count();
                    newICD.KBNu = q6.Where(p => p.GTinh == 0).Count();
                    newICD.KBTE = q6.Where(p => p.Tuoi < 15).Count();
                    newICD.KBTuVong = q6.Where(p => p.KetQua == "Tử vong").GroupBy(p => p.MaBNhan).Count();
                    newICD.DTMacTS = q7.Count();
                    newICD.DTMacNu = q7.Where(p => p.GTinh == 0).Count();
                    newICD.DTTuVongTS = q8.GroupBy(p => p.MaBNhan).Count();
                    newICD.DTTuVongNu = q8.Where(p => p.GTinh == 0).GroupBy(p => p.MaBNhan).Count();
                    newICD.DTTEMacTS = q7.Where(p => p.Tuoi < 15).Count();
                    newICD.DTTEMac5 = q7.Where(p => p.Tuoi < 5).Count();
                    newICD.DTTETuVongTS = q8.Where(p => p.Tuoi < 15).GroupBy(p => p.MaBNhan).Count();
                    newICD.DTTETuVong5 = q8.Where(p => p.Tuoi < 5).GroupBy(p => p.MaBNhan).Count();
                    newICD.SoNgaydtTS = q7.Sum(p => p.SoNgaydt);
                    newICD.SoNgaydt6 = q7.Where(p => p.Tuoi < 6).Sum(p => p.SoNgaydt);

                    newICD.BN_NangxinveTaiKhoa = q6.Where(p => p.KetQua == "Nặng hơn").Count();
                    newICD.TuVongTaiVien = q6.Where(p => p.KetQua == "Tử vong").Count();
                    newICD.BN_NangXinVeNu = q9.Where(p => p.GTinh == 0).Where(p => p.NoiTru == 1).Count();
                    newICD.BN_NangxinveTS = q9.Where(p => p.NoiTru==1).Count();
                    newICD.TuVongTruocVien = q10.Count();
                    _lICD10.Add(newICD);
                }
                _lICD10 = (from a in _lICD10
                           select new ICD10
                               {
                                   MaICD = a.MaICD,
                                   STTCB = a.STTCB,
                                   _STTChuong=a._STTChuong,
                                   TenCB = a.TenCB,
                                   STTICD = a.STTICD,
                                   TenBenh = a.TenBenh,
                                   TenWHO = a.TenWHO,
                                   TenWHOe = a.TenWHOe,
                                   NhomCB = a.NhomCB,
                                   KBTS = DungChung.Bien.MaBV == "30009" ? ((a.KBTS == null ? 0 : a.KBTS) + (a.DTMacTS == null ? 0 : a.DTMacTS)) : (a.KBTS == null ? 0 : a.KBTS),
                                   KBNu = a.KBNu == 0 ? null : a.KBNu,
                                   KBTE = a.KBTE == 0 ? null : a.KBTE,
                                   KBTuVong = a.KBTuVong == 0 ? null : a.KBTuVong,
                                   DTMacTS = a.DTMacTS == 0 ? null : a.DTMacTS,
                                   DTMacNu = a.DTMacNu == 0 ? null : a.DTMacNu,
                                   DTTuVongTS = a.DTTuVongTS == 0 ? null : a.DTTuVongTS,
                                   DTTuVongNu = a.DTTuVongNu == 0 ? null : a.DTTuVongNu,
                                   DTTEMacTS = a.DTTEMacTS == 0 ? null : a.DTTEMacTS,
                                   DTTEMac5 = a.DTTEMac5 == 0 ? null : a.DTTEMac5,
                                   DTTETuVongTS = a.DTTETuVongTS == 0 ? null : a.DTTETuVongTS,
                                   DTTETuVong5 = a.DTTETuVong5 == 0 ? null : a.DTTETuVong5,

                                   BN_NangXinVeNu = a.BN_NangXinVeNu == 0 ? null : a.BN_NangXinVeNu,
                                   BN_NangxinveTS = a.BN_NangxinveTS == 0 ? null : a.BN_NangxinveTS,
                                   BN_NangxinveTaiKhoa = a.BN_NangxinveTaiKhoa == 0 ? null : a.BN_NangxinveTaiKhoa,
                                   TuVongTaiVien = a.TuVongTaiVien == 0 ? null : a.TuVongTaiVien,
                                   TuVongTruocVien = a.TuVongTruocVien == 0 ? null : a.TuVongTruocVien,

                                   SoNgaydt6 = a.SoNgaydt6 == 0 ? null : a.SoNgaydt6,
                                   SoNgaydtTS = a.SoNgaydtTS == 0 ? null : a.SoNgaydtTS,
                                   SumICD = a.KBTS + a.KBNu + a.KBTE + a.KBTuVong + a.DTMacTS + a.DTMacNu + a.DTTuVongTS + a.DTTuVongNu + a.DTTEMacTS + a.DTTEMac5 + a.DTTETuVongTS + a.DTTETuVong5,
                               }).Where(p => chkIn.Checked ? (p.SumICD > 0) : true).OrderBy(p => p.STTCB).ThenBy(p => p.STTICD).ToList();

                if (DungChung.Bien.MaBV == "04002")
                {
                    frmIn frm = new frmIn();
                    BaoCao.Rep_BcTinhHinhBenhTatICD10_04002 rep = new BaoCao.Rep_BcTinhHinhBenhTatICD10_04002();
                    if (txtNgayThang.Text.Trim() != "")
                        rep.TuNgayDenNgay.Value = txtNgayThang.Text;
                    else
                        rep.TuNgayDenNgay.Value = "Từ ngày " + lupTuNgay.DateTime.ToShortDateString() + " đến ngày " + lupDenNgay.DateTime.ToShortDateString();
                    rep.DataSource = _lICD10;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                 //   if(DungChung.Bien.MaBV == "30010" || DungChung.Bien.MaBV == "27001" || DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "20001" )
                  //  {

                        frmIn frm = new frmIn();
                        BaoCao.Rep_BcTinhHinhBenhTatICD10_VY_30010 rep = new BaoCao.Rep_BcTinhHinhBenhTatICD10_VY_30010();
                        if (txtNgayThang.Text.Trim() != "")
                            rep.TuNgayDenNgay.Value = txtNgayThang.Text;
                        else
                            rep.TuNgayDenNgay.Value = "Từ ngày " + lupTuNgay.DateTime.ToShortDateString() + " đến ngày " + lupDenNgay.DateTime.ToShortDateString();
                        rep.DataSource = _lICD10;
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    //}
                    //else
                    //{
                    //    frmIn frm = new frmIn();
                    //    BaoCao.Rep_BcTinhHinhBenhTatICD10_VY rep = new BaoCao.Rep_BcTinhHinhBenhTatICD10_VY();
                    //    if (txtNgayThang.Text.Trim() != "")
                    //        rep.TuNgayDenNgay.Value = txtNgayThang.Text;
                    //    else
                    //        rep.TuNgayDenNgay.Value = "Từ ngày " + lupTuNgay.DateTime.ToShortDateString() + " đến ngày " + lupDenNgay.DateTime.ToShortDateString();
                    //    rep.DataSource = _lICD10;
                    //    rep.BindingData();
                    //    rep.CreateDocument();
                    //    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    //    frm.ShowDialog();
                    //}
                    
                }
            }
        }
        private string getFstString(string a)
        {
            string b = "";
            if (a == "")
                b = "";
            else
            {
                #region lấy ra list các ký tự đầu tiên của các phần tử của chuỗi
                string[] strArr = a.Split(',');// mảng các phần tử của chuỗi        
                List<string> chStr = new List<string>();//list các ký tự đầu tiên của các phần tử của chuỗi
                foreach (string s in strArr)
                {
                    if (s != "")
                    {
                        string ch = s.Substring(0, 1).ToUpper(); // ký tự đầu tiên của 1 phần tử
                        if (chStr.Count > 0)
                        {
                            int count = 0;
                            foreach (string c in chStr)
                            {
                                if (c == (ch))
                                    count++;
                            }
                            if (count == 0)// ký tự này chưa tồn tại trong mảng chStr => add ch vào mảng chStr
                                chStr.Add(ch);
                        }
                        else
                            chStr.Add(ch);
                    }
                #endregion
                }
                chStr.Sort(); // sắp xếp các phần tử trong list
                foreach (string c in chStr)
                {
                    if (c != "")
                        b += getEndtring(c, a) + ";";
                }
                if (b.LastIndexOf(";") == b.Length - 1)
                    b = b.Substring(0, b.Length - 1);
            }
            return b;
        }
        private string getEndtring(string kytu, string chuoi)
        {
            string trave = "";
            #region lấy ra list các phần cuối các phần tử của chuỗi
            string[] strArr = chuoi.Split(',');// mảng các phần tử của chuỗi        
            List<int> chInt = new List<int>();//list các ký tự cuối của các phần tử của chuỗi
            foreach (string s in strArr)
            {
                if (s != "")
                {
                    if ((s.Substring(0, 1).ToLower()) == (kytu.ToLower()))
                    {
                        string ch = s.Substring(1, s.Length - 1); // phần đuôi của 1 phần tử
                        int rs;
                        if (Int32.TryParse(ch, out rs))
                        {

                            if (chInt.Count > 0)
                            {
                                int count = 0;
                                foreach (int i in chInt)
                                {
                                    if (i == Convert.ToInt32(ch))
                                        count++;
                                }
                                if (count == 0)// ký tự này chưa tồn tại trong mảng chStr => add ch vào mảng chStr
                                    chInt.Add(Convert.ToInt32(ch));
                            }
                            else
                                chInt.Add(Convert.ToInt32(ch));
                        }
                    }
                }
            }
            #endregion
            chInt.Sort(); // sắp xếp các phần tử trong list
            #region gộp chuỗi
            int n = -2;
            for (int so = 0; so < chInt.Count; so++)
            {
                if (chInt[so] != chInt[chInt.Count - 1])// s không phải phần tử cuối
                {
                    string trf;
                    if (chInt[so] < 10)
                        trf = String.Format("{0:00}", chInt[so]);
                    else
                        trf = chInt[so].ToString();

                    if (n == -2)// phần tử đầu tiên
                    {
                        trave = kytu + trf;
                    }
                    else
                    {
                        if (chInt[so] == n + 1) // có tăng
                        {

                            if (trave.Substring(trave.Length - 1) != ("-"))
                                trave = trave + "-";
                        }
                        else //không tăng
                        {

                            if (trave.Substring(trave.Length - 1) != ("-"))
                                trave = trave + "," + kytu + trf;
                            else
                            {
                                if (n < 10)
                                    trave = trave + kytu + String.Format("{0:00}", n) + "," + kytu + trf;
                                else
                                    trave = trave + kytu + n.ToString() + "," + kytu + trf;
                            }
                        }
                    }
                    n = chInt[so];
                }
                else// là phần tử cuối
                {
                    string trf;
                    if (chInt[so] < 10)
                        trf = String.Format("{0:00}", chInt[so]);
                    else
                        trf = chInt[so].ToString();
                    if (n == -2)// phần tử đầu tiên
                    {
                        trave = kytu + trf;
                    }
                    else
                    {
                        if (chInt[so] == n + 1) // có tăng
                        {

                            if (trave.Substring(trave.Length - 1) != ("-"))
                                trave = trave + "-" + kytu + trf;
                            else
                                trave = trave + kytu + trf;
                        }
                        else //không tăng
                        {
                            if (trave.Substring(trave.Length - 1) != ("-"))
                                trave = trave + "," + kytu + trf;
                            else
                            {
                                if (n < 10)
                                    trave = trave + kytu + String.Format("{0:00}", n) + "," + kytu + trf;
                                else
                                    trave = trave + kytu + n.ToString() + "," + kytu + trf;
                            }
                        }
                    }
                    n = chInt[so];
                }
            }
            return trave;
            #endregion
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public class ICD10_2
        {
            string maICD;
            string maBNhan;
            public string MaICD
            {
                get { return maICD; }
                set { maICD = value; }
            }


            public string MaBNhan
            {
                get { return maBNhan; }
                set { maBNhan = value; }
            }


        }
        public class ICDSai
        {
            private int maBN;

            public int MaBN
            {
                get { return maBN; }
                set { maBN = value; }
            }
            private string MaICD;

            public string MaICD1
            {
                get { return MaICD; }
                set { MaICD = value; }
            }
            private string mess;

            public string Mess
            {
                get { return mess; }
                set { mess = value; }
            }
            private string noitru;

            public string Noitru
            {
                get { return noitru; }
                set { noitru = value; }
            }


        }

    }

}