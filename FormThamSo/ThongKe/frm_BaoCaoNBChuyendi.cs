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
    public partial class frm_BaoCaoNBChuyendi : DevExpress.XtraEditors.XtraForm
    {
        public frm_BaoCaoNBChuyendi()
        {
            InitializeComponent();
        }

        private void frm_BaoCaoNBChuyendi_Load(object sender, EventArgs e)
        {
            LupNgayden.DateTime = System.DateTime.Now;
            LupNgaytu.DateTime = System.DateTime.Now;
        }
        private class BC
        {
            private string tenck;
            private double slkham, sldieutri, slchuyen, tyle, bhyt, sl1a, sl1b, sl2, sl3, sl4, sl5, tuyen1, tuyen2, tuyen3, tuyen4;
            private int mabn;
            //private double tyle;
            public int MaBN
            { set { mabn = value; } get { return mabn; } }
            public string TenCK
            { set { tenck = value; } get { return tenck; } }
            public double SLKham
            { set { slkham = value; } get { return slkham; } }
            public double SLDieutri
            {
                set { sldieutri = value; }
                get { return sldieutri; }
            }
            public double SLChuyen
            { set { slchuyen = value; } get { return slchuyen; } }
            public double Tyle
            { set { tyle = value; } get { return tyle; } }
            public double BHYT
            { set { bhyt = value; } get { return bhyt; } }
            public double SL1a
            { set { sl1a = value; } get { return sl1a; } }
            public double SL1b
            { set { sl1b = value; } get { return sl1b; } }
            public double SL2
            { set { sl2 = value; } get { return sl2; } }
            public double SL3
            { set { sl3 = value; } get { return sl3; } }
            public double SL4
            { set { sl4 = value; } get { return sl4; } }
            public double SL5
            { set { sl5 = value; } get { return sl5; } }
            public double Tuyen1
            { set { tuyen1 = value; } get { return tuyen1; } }
            public double Tuyen2
            { set { tuyen2 = value; } get { return tuyen2; } }
            public double Tuyen3
            { set { tuyen3 = value; } get { return tuyen3; } }
            public double Tuyen4
            {
                set { tuyen4 = value; }
                get { return tuyen4; }
            }
        }
        private string TuyenBV(string bv, string bvchuyen)
        {
            // return 1 là chuyển từ tuyến dưới lên tuyến trên liền kề
            // return 2 là chuyển người bệnh từ tuyến dưới lên tuyến trên không liền kề
            // return 3 là chuyển người bệnh cùng tuyến
            // retunr 4 là người bệnh từ tuyến trên về tuyến dưới
            string MaBV = bv.Trim();
            string MaBVC = bvchuyen.Trim();
            switch (MaBV)
            {
                case "A":
                    if (MaBVC == "A")
                    {
                        return "3";
                    }
                    if (MaBVC == "B")
                    {
                        return "4";
                    }
                    if (MaBVC == "C")
                    {
                        return "4";
                    }
                    if (MaBVC == "D")
                    {
                        return "4";
                    }
                    if (MaBVC == "E")
                    {
                        return "4";
                    }
                    break;
                case "B":
                    if (MaBVC == "A")
                    {
                        return "1";
                    }
                    if (MaBVC == "B")
                    {
                        return "3";
                    }
                    if (MaBVC == "C")
                    {
                        return "4";
                    }
                    if (MaBVC == "D")
                    {
                        return "4";
                    }
                    if (MaBVC == "E")
                    {
                        return "4";
                    }
                    break;
                case "C":
                    if (MaBVC == "A")
                    {
                        return "2";
                    }
                    if (MaBVC == "B")
                    {
                        return "1";
                    }
                    if (MaBVC == "C")
                    {
                        return "3";
                    }
                    if (MaBVC == "D")
                    {
                        return "4";
                    }
                    if (MaBVC == "E")
                    {
                        return "4";
                    }
                    break;
                case "D":
                    if (MaBVC == "A")
                    {
                        return "2";
                    }
                    if (MaBVC == "B")
                    {
                        return "2";
                    }
                    if (MaBVC == "C")
                    {
                        return "1";
                    }
                    if (MaBVC == "D")
                    {
                        return "3";
                    }
                    if (MaBVC == "E")
                    {
                        return "4";
                    }
                    break;
                case "E":
                    if (MaBVC == "A")
                    {
                        return "2";
                    }
                    if (MaBVC == "B")
                    {
                        return "2";
                    }
                    if (MaBVC == "C")
                    {
                        return "2";
                    }
                    if (MaBVC == "D")
                    {
                        return "1";
                    }
                    if (MaBVC == "E")
                    {
                        return "3";
                    }
                    break;
            }
            return "";
        }
        List<BC> _BC = new List<BC>();
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void sbtTao_Click(object sender, EventArgs e)
        {
            _BC.Clear();
            DateTime NT = DungChung.Ham.NgayTu(LupNgaytu.DateTime);
            DateTime ND = DungChung.Ham.NgayDen(LupNgayden.DateTime);

            var q2 = (from rv in _Data.RaViens.Where(p => p.NgayRa >= NT && p.NgayRa <= ND)
                      join kp in _Data.KPhongs on rv.MaKP equals kp.MaKP
                      join ck in _Data.ChuyenKhoas on kp.MaCK equals ck.MaCK
                      select new { rv.MaKP, rv.MaBNhan, rv.MaBVC, kp.MaCK, kp.PLoai, rv.Status, rv.LyDoC, ck.TenCK }).ToList();

            foreach (var a in q2)
            {
                BC themmoi = new BC();
                themmoi.MaBN = a.MaBNhan;
                themmoi.TenCK = a.TenCK;
                if (a.PLoai == "Phòng khám")
                    themmoi.SLKham = 1;
                else
                    themmoi.SLDieutri = 1;
                if (a.Status == 1 && a.MaBVC != null)
                {
                    themmoi.SLChuyen = 1;
                    string _MaBVC = a.MaBVC;
                    var q4 = _Data.BenhViens.Where(p => p.MaBV == _MaBVC).ToList();
                    string HaBVC = q4.First().TuyenBV.Trim();
                    var q5 = _Data.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV).ToList();
                    string HaBV = q5.First().TuyenBV.Trim();
                    string kt = TuyenBV(HaBV, HaBVC);

                    switch (kt)
                    {
                        case "1":
                            themmoi.SL1a = 1;
                            break;
                        case "2":
                            themmoi.SL1b = 1;
                            break;
                        case "4":
                            themmoi.SL2 = 1;
                            break;
                        case "3":
                            themmoi.SL3 = 1;
                            break;
                    }
                    switch (HaBVC)
                    {
                        case "A":
                            themmoi.Tuyen1 = 1;
                            break;
                        case "B":
                            themmoi.Tuyen2 = 1;
                            break;
                        case "C":
                            themmoi.Tuyen3 = 1;
                            break;
                        case "D":
                            themmoi.Tuyen4 = 1;
                            break;
                    }
                    if (a.LyDoC != null && a.LyDoC.ToString() != "")
                    {
                        if (a.LyDoC.ToString().Contains("đúng tuyến"))
                        {
                            themmoi.SL4 = 1;
                        }
                        else
                        { themmoi.SL5 = 1; }
                    }
                }
                _BC.Add(themmoi);
            }

            var q3 = (from b in _BC
                      group new { b } by new { b.TenCK } into kq
                      select new BC
                      {
                          TenCK=kq.Key.TenCK,
                          SLKham = kq.Sum(p => p.b.SLKham),
                          SLDieutri = kq.Sum(p => p.b.SLDieutri),
                          SLChuyen = kq.Sum(p => p.b.SLChuyen),
                          BHYT = kq.Sum(p => p.b.BHYT),
                          SL1a = kq.Sum(p => p.b.SL1a),
                          SL1b = kq.Sum(p => p.b.SL1b),
                          SL2 = kq.Sum(p => p.b.SL2),
                          SL3 = kq.Sum(p => p.b.SL3),
                          SL4 = kq.Sum(p => p.b.SL4),
                          SL5 = kq.Sum(p => p.b.SL5),
                          Tuyen1 = kq.Sum(p => p.b.Tuyen1),
                          Tuyen2 = kq.Sum(p => p.b.Tuyen2),
                          Tuyen3 = kq.Sum(p => p.b.Tuyen3),
                          Tuyen4 = kq.Sum(p => p.b.Tuyen4),
                          Tyle = kq.Sum(p => p.b.BHYT) / (kq.Sum(p => p.b.SLKham) + kq.Sum(p => p.b.SLDieutri)),
                      }).ToList();
            #region bỏ
            //List<DungChung.Bien.c_chuyenkhoa> dsChuyenKhoa = new List<DungChung.Bien.c_chuyenkhoa>();
            //DungChung.Bien.c_chuyenkhoa.f_ChuyenKhoa();
            //var q = (from kb in _Data.BNKBs //.Where(p => p.NgayKham >= NT && p.NgayKham <= ND) bỏ theo y/c đoài 1908, tính theo ngày ra viện
            //         join kp in _Data.KPhongs.Where(p => p.PLoai == "Phòng khám" || p.PLoai == "Lâm sàng") on kb.MaKP equals kp.MaKP
            //         where ((from vp in _Data.VienPhis select vp.MaBNhan).Contains(kb.MaBNhan))
            //         where ((from rv in _Data.RaViens.Where(p => p.NgayRa >= NT && p.NgayRa <= ND) select rv.MaBNhan).Contains(kb.MaBNhan == null ? 0 : kb.MaBNhan.Value))
            //         //group new { kb, kp } by new { kb.ChuyenKhoa } into kq
            //         select new { kb.MaCK, kb.MaBNhan, kp.PLoai }).ToList();
            //var q1 = (from a in q
            //          join b in DungChung.Bien._lChuyenKhoa on a.MaCK equals b.MaCK
            //          select new { ChuyenKhoa = b.ChuyenKhoa, a.MaBNhan, a.PLoai }).ToList();

            //foreach (var a in q1)
            //{
            //    BC themmoi = new BC();
            //    themmoi.MaBN = a.MaBNhan == null ? 0 : a.MaBNhan.Value;
            //    themmoi.TenCK = a.ChuyenKhoa;
            //    if (a.PLoai == "Phòng khám")
            //        themmoi.SLKham = 1;
            //    else
            //        themmoi.SLDieutri = 1;
            //    _BC.Add(themmoi);
            //}

            //foreach (var c in _BC)
            //{
            //    var q3 = _Data.RaViens.Where(p => p.MaBNhan == c.MaBN).ToList();
            //    if (q3.Count > 0 && q3.First().Status == 1 && q3.First().MaBVC != null)
            //    {
            //        c.SLChuyen = 1;
            //        string _MaBVC = q3.First().MaBVC;
            //        var q4 = _Data.BenhViens.Where(p => p.MaBV == _MaBVC).ToList();
            //        string HaBVC = q4.First().TuyenBV.Trim();
            //        var q5 = _Data.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV).ToList();
            //        string HaBV = q5.First().TuyenBV.Trim();
            //        string kt = TuyenBV(HaBV, HaBVC);

            //        switch (kt)
            //        {
            //            case "1":
            //                c.SL1a = 1;
            //                break;
            //            case "2":
            //                c.SL1b = 1;
            //                break;
            //            case "4":
            //                c.SL2 = 1;
            //                break;
            //            case "3":
            //                c.SL3 = 1;
            //                break;
            //        }
            //        switch (HaBVC)
            //        {
            //            case "A":
            //                c.Tuyen4 = 1;
            //                break;
            //            case "B":
            //                c.Tuyen3 = 1;
            //                break;
            //            case "C":
            //                c.Tuyen2 = 1;
            //                break;
            //            case "D":
            //                c.Tuyen1 = 1;
            //                break;
            //        }
            //        if (q3.First().LyDoC != null && q3.First().LyDoC.ToString() != "")
            //        {
            //            if (q3.First().LyDoC.ToString().Contains("đúng tuyến"))
            //            {
            //                c.SL4 = 1;
            //            }
            //            else
            //            { c.SL5 = 1; }
            //        }
            //    }
            //}

            //var q7 = (from b in _BC
            //          group new { b } by new { b.TenCK } into kq
            //          select new
            //          {
            //              kq.Key.TenCK,
            //              SLKham = kq.Sum(p => p.b.SLKham),
            //              SLDieutri = kq.Sum(p => p.b.SLDieutri),
            //              SLChuyen = kq.Sum(p => p.b.SLChuyen),
            //              BHYT = kq.Sum(p => p.b.BHYT),
            //              SL1a = kq.Sum(p => p.b.SL1a),
            //              SL1b = kq.Sum(p => p.b.SL1b),
            //              SL2 = kq.Sum(p => p.b.SL2),
            //              SL3 = kq.Sum(p => p.b.SL3),
            //              SL4 = kq.Sum(p => p.b.SL4),
            //              SL5 = kq.Sum(p => p.b.SL5),
            //              Tuyen1 = kq.Sum(p => p.b.Tuyen1),
            //              Tuyen2 = kq.Sum(p => p.b.Tuyen2),
            //              Tuyen3 = kq.Sum(p => p.b.Tuyen3),
            //              Tuyen4 = kq.Sum(p => p.b.Tuyen4),
            //              TyLe = kq.Sum(p => p.b.BHYT) / (kq.Sum(p => p.b.SLKham) + kq.Sum(p => p.b.SLDieutri)),
            //          }).ToList();
            #endregion
            double T1 = q3.Sum(p => p.SLChuyen) / (q3.Sum(p => p.SLDieutri) + q3.Sum(p => p.SLKham)) * 100;
            T1 = Math.Round(T1, 1);
       
            BaoCao.Rep_BaoCaoNBChuyenDi rep = new BaoCao.Rep_BaoCaoNBChuyenDi();
            frmIn frm = new frmIn();
            rep.Tyle.Value = T1;
            rep.NgayThang.Value = "Từ ngày: " + LupNgaytu.DateTime.ToString().Substring(0, 10) + " Đến ngày: " + LupNgayden.DateTime.ToString().Substring(0, 10);
            rep.DataSource = q3;
            rep.BindingData();
            rep.CreateDocument();
            //rep.DataMember = "Table";
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void sbtHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}