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
    public partial class Frm_THChuyenden : DevExpress.XtraEditors.XtraForm
    {
        public Frm_THChuyenden()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void sbtHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frm_THChuyenden_Load(object sender, EventArgs e)
        {
            LupNgaytu.DateTime = System.DateTime.Now;
            LupNgayden.DateTime = System.DateTime.Now;
            DateTime Ngaytu = DungChung.Ham.NgayTu(LupNgaytu.DateTime);
            DateTime Ngayden = DungChung.Ham.NgayDen(LupNgayden.DateTime);
            var bv = (from a in Data.BenhNhans
                      join b in Data.BenhViens on a.MaBV equals b.MaBV
                      join vp in Data.VienPhis on a.MaBNhan equals vp.MaBNhan
                      where (vp.NgayTT >= Ngaytu && vp.NgayTT <= Ngayden)
                      select b).Distinct().ToList();
            BenhVien themmoi1 = new BenhVien();
            themmoi1.TenBV = "Chọn tất cả";
            themmoi1.MaBV = "0";
            bv.Add(themmoi1);
            cklBV.DataSource = bv.OrderBy(p => p.MaBV).ToList();
            for (int i = 0; i < cklBV.ItemCount; i++)
            {
                cklBV.SetItemChecked(i, true);
            }
        }
        string A1 = "";
        string B1 = "", B2 = "", B3 = "";
        string C1 = "", C2 = "", C3 = "", C4 = "";
        string D1 = "";
        private void Tuyen(string T)
        {
            string _T = T.Trim();
            switch (_T)
            {
                case "A":
                    A1 = "B";
                    B1 = "C";
                    B2 = "D";
                    B3 = "E";
                    D1 = "A";
                    break;
                case "B":
                    A1 = "C";
                    B1 = "D";
                    B2 = "E";
                    C1 = "A";
                    D1 = "B";
                    break;
                case "C":
                    A1 = "D";
                    B1 = "E";
                    C1 = "A";
                    C2 = "B";
                    D1 = "C";
                    break;
                case "D":
                    A1 = "E";
                    C1 = "A";
                    C2 = "B";
                    C3 = "C";
                    D1 = "D";
                    break;
                case "E":
                    C1 = "A";
                    C2 = "B";
                    C3 = "C";
                    C4 = "D";
                    D1 = "E";
                    break;
            }
        }

        private void sbtTaoBC_Click(object sender, EventArgs e)
        {
            
            DateTime Ngaytu = DungChung.Ham.NgayTu(LupNgaytu.DateTime);
            DateTime Ngayden = DungChung.Ham.NgayDen(LupNgayden.DateTime);
            var lravien = Data.RaViens.ToList();
            var c = (from bv in Data.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV)
                     group new { bv } by new { bv.TuyenBV } into kq
                     select new { Tuyen = kq.Key.TuyenBV.Trim(), }).ToList();
            string _M = c.First().Tuyen;
            Tuyen(_M);
            List<BenhVien> _bvChon = new List<BenhVien>();
            for (int i = 0; i < cklBV.ItemCount; i++)
            {
                if (cklBV.GetItemChecked(i))
                    _bvChon.Add(new BenhVien { TenBV = cklBV.GetItemText(i), MaBV = cklBV.GetItemValue(i) == null ? "" : Convert.ToString(cklBV.GetItemValue(i)) });
            }
            var bvv = (from a in _bvChon
                       join b in Data.BenhViens on a.MaBV equals b.MaBV
                       select b).ToList();
            if (radTT.SelectedIndex == 0)
            {
                
                var q = (from bv in bvv
                         join bn in Data.BenhNhans on bv.MaBV equals bn.MaBV
                         join ttbx in Data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan into kq
                         from kq1 in kq.DefaultIfEmpty()
                         join vp in Data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                         where (vp.NgayTT >= Ngaytu && vp.NgayTT <= Ngayden)
                         select new
                         {
                             bv.MaBV,
                             TBV = bv.TenBV ?? "",
                             TuBV = bv.TuyenBV ?? "",
                             bn.MaBNhan,
                             bn.DTuong,
                             MaICD = kq1 == null ? "" : (kq1.MaICD == null ? "" : kq1.MaICD)
                         }).ToList();
                var q2 = (from a in q
                          group new { a } by new { a.MaBV, a.TBV, a.TuBV, a.MaBNhan, a.DTuong, a.MaICD } into kq
                          select new BC
                          {
                              TBV = kq.Key.TBV.ToString().Trim(),
                              TuBV = kq.Key.TuBV.ToString().Trim(),
                              MaBV = kq.Key.MaBV.ToString().Trim(),
                              TBN = kq.Select(p => p.a.MaBNhan).Count(),
                              MaBNhan = kq.Key.MaBNhan,
                              DTuong = kq.Key.DTuong,
                              BHYT = kq.Where(p => p.a.DTuong == "BHYT").Select(p => p.a.MaBNhan).Count(),
                              MaICD = kq.Key.MaICD,

                          }).ToList();
                foreach (BC bc in q2)
                {
                    var qrv = lravien.Where(p => p.MaBNhan == bc.MaBNhan).FirstOrDefault();
                    bc.CDPH = 0;
                    bc.CDKB = 0;
                    if (bc.MaICD != "" && qrv != null && qrv.MaICD != null)//&& qrv.MaICD.IndexOf(";") >0
                    {
                        string maICDRaVien = qrv.MaICD;
                        if (qrv.MaICD.IndexOf(";") > 0)
                            maICDRaVien = qrv.MaICD.Substring(0, qrv.MaICD.IndexOf(";") + 1);
                        if (bc.MaICD.Contains(maICDRaVien))
                        {
                            bc.CDPH = 1;
                            bc.CDKB = 0;
                        }
                        else
                        {
                            bc.CDKB = 1;
                            bc.CDPH = 0;
                        }
                    }
                }

                var q3 = (from a in q2
                          group new { a } by new { a.MaBV, a.TBV, a.TuBV } into kq
                          select new BC
                          {
                              TBV = kq.Key.TBV.ToString().Trim(),
                              TuBV = kq.Key.TuBV.ToString().Trim(),
                              MaBV = kq.Key.MaBV.ToString().Trim(),
                              TBN = kq.Select(p => p.a.MaBNhan).Count(),
                              BHYT = kq.Where(p => p.a.DTuong == "BHYT").Select(p => p.a.MaBNhan).Count(),
                              a = kq.Where(p => p.a.TuBV.Trim() != "").Where(p => p.a.TuBV.Trim() == A1).Select(p => p.a.MaBNhan).Count(),
                              b = kq.Where(p => p.a.TuBV.Trim() != "").Where(p => p.a.TuBV.Trim() == B1 || p.a.TuBV.Trim() == B2 || p.a.TuBV.Trim() == B3).Select(p => p.a.MaBNhan).Count(),
                              c = kq.Where(p => p.a.TuBV.Trim() != "").Where(p => p.a.TuBV.Trim() == C1 || p.a.TuBV.Trim() == C2 || p.a.TuBV.Trim() == C3 || p.a.TuBV.Trim() == C4).Select(p => p.a.MaBNhan).Count(),
                              d = kq.Where(p => p.a.TuBV.Trim() != "").Where(p => p.a.TuBV.Trim() == D1).Select(p => p.a.MaBNhan).Count(),
                              e = kq.Select(p => p.a.MaBNhan).Count(),
                              f = 0,
                              CDPH = kq.Sum(p => p.a.CDPH),
                              CDKB = kq.Sum(p => p.a.CDKB)
                          }).ToList();
                BaoCao.Rep_Tonghopchuyenden rep = new BaoCao.Rep_Tonghopchuyenden();
                frmIn frm = new frmIn();
                rep.Ngaythang.Value = "Từ ngày: " + LupNgaytu.Text.Substring(0, 10) + " đến ngày " + LupNgayden.Text.Substring(0, 10);
                rep.DataSource = q3;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                var q = (from bv in bvv
                         join bn in Data.BenhNhans on bv.MaBV equals bn.MaBV
                         join ttbx in Data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan into kq
                         from kq1 in kq.DefaultIfEmpty()
                         //join vp in Data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                         where (bn.NNhap >= Ngaytu && bn.NNhap <= Ngayden)
                         select new BC
                         {
                             MaBV = bv.MaBV,
                             TBV = bv.TenBV ?? "",
                             TuBV = bv.TuyenBV ?? "",
                             MaBNhan = bn.MaBNhan,
                             DTuong = bn.DTuong,
                             MaICD = kq1 == null ? "" : (kq1.MaICD == null ? "" : kq1.MaICD)
                         }).ToList();
                foreach (BC bc in q)
                {
                    var qrv = lravien.Where(p => p.MaBNhan == bc.MaBNhan).FirstOrDefault();

                    bc.CDPH = 0;
                    bc.CDKB = 0;
                    if (bc.MaICD != "" && qrv != null && qrv.MaICD != null)//&& qrv.MaICD.IndexOf(";") >0
                    {
                        string maICDRaVien = qrv.MaICD;
                        if (qrv.MaICD.IndexOf(";") > 0)
                            maICDRaVien = qrv.MaICD.Substring(0, qrv.MaICD.IndexOf(";") + 1);
                        if (bc.MaICD.Contains(maICDRaVien))
                        {
                            bc.CDPH = 1;
                            bc.CDKB = 0;
                        }
                        else
                        {
                            bc.CDKB = 1;
                            bc.CDPH = 0;
                        }
                    }
                }
                var q2 = (from a in q
                          group new { a } by new { a.MaBV, a.TBV, a.TuBV } into kq
                          select new BC
                          {
                              TBV = kq.Key.TBV.ToString().Trim(),
                              TuBV = kq.Key.TuBV.ToString().Trim(),
                              MaBV = kq.Key.MaBV.ToString().Trim(),
                              TBN = kq.Select(p => p.a.MaBNhan).Count(),
                              BHYT = kq.Where(p => p.a.DTuong == "BHYT").Select(p => p.a.MaBNhan).Count(),
                              a = kq.Where(p => p.a.TuBV.Trim() != "").Where(p => p.a.TuBV.Trim() == A1).Select(p => p.a.MaBNhan).Count(),
                              b = kq.Where(p => p.a.TuBV.Trim() != "").Where(p => p.a.TuBV.Trim() == B1 || p.a.TuBV.Trim() == B2 || p.a.TuBV.Trim() == B3).Select(p => p.a.MaBNhan).Count(),
                              c = kq.Where(p => p.a.TuBV.Trim() != "").Where(p => p.a.TuBV.Trim() == C1 || p.a.TuBV.Trim() == C2 || p.a.TuBV.Trim() == C3 || p.a.TuBV.Trim() == C4).Select(p => p.a.MaBNhan).Count(),
                              d = kq.Where(p => p.a.TuBV.Trim() != "").Where(p => p.a.TuBV.Trim() == D1).Select(p => p.a.MaBNhan).Count(),
                              e = kq.Select(p => p.a.MaBNhan).Count(),
                              f = 0,
                              CDPH = kq.Sum(p => p.a.CDPH),
                              CDKB = kq.Sum(p => p.a.CDKB)
                          }).ToList();
                BaoCao.Rep_Tonghopchuyenden rep = new BaoCao.Rep_Tonghopchuyenden();
                frmIn frm = new frmIn();
                rep.Ngaythang.Value = "Từ ngày: " + LupNgaytu.Text.Substring(0, 10) + " đến ngày " + LupNgayden.Text.Substring(0, 10);
                rep.DataSource = q2;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }
        public class BC
        {
            public string TBV { set; get; }

            public string TuBV { get; set; }

            public string MaBV { get; set; }

            public int TBN { get; set; }

            public int BHYT { get; set; }

            public int a { get; set; }

            public int b { get; set; }

            public int c { get; set; }

            public int d { get; set; }

            public int e { get; set; }

            public int CDPH { get; set; }

            public int f { get; set; }

            public int CDKB { get; set; }

            public int MaBNhan { get; set; }

            public string DTuong { get; set; }

            public string MaICD { get; set; }
        }

        private void cklBV_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            int check = 0;
            for (int i = 0; i < cklBV.ItemCount; i++)
            {
                if (cklBV.GetItemChecked(i))
                    check++;

            }
            if (e.Index == 0)
            {
                if (cklBV.GetItemChecked(0) == true)
                    for (int z = 0; z < cklBV.ItemCount; z++)
                    {
                        cklBV.SetItemChecked(z, true);
                    }
                else if (cklBV.GetItemChecked(0) == false)
                    for (int z = 0; z < cklBV.ItemCount; z++)
                    {
                        cklBV.SetItemChecked(z, false);
                    }
            }
        }

        private void LupNgaytu_EditValueChanged(object sender, EventArgs e)
        {
            DateTime Ngaytu = DungChung.Ham.NgayTu(LupNgaytu.DateTime);
            DateTime Ngayden = DungChung.Ham.NgayDen(LupNgayden.DateTime);
            var bv = (from a in Data.BenhNhans.Where(p => p.NNhap >= Ngaytu && p.NNhap <= Ngayden)
                      join b in Data.BenhViens on a.MaBV equals b.MaBV
                      select b).Distinct().ToList();
            if (radTT.SelectedIndex == 0)
            {
                bv = (from a in Data.BenhNhans
                      join b in Data.BenhViens on a.MaBV equals b.MaBV
                      join vp in Data.VienPhis on a.MaBNhan equals vp.MaBNhan
                      where (vp.NgayTT >= Ngaytu && vp.NgayTT <= Ngayden)
                      select b).Distinct().ToList();
            }
            BenhVien themmoi1 = new BenhVien();
            themmoi1.TenBV = "Chọn tất cả";
            themmoi1.MaBV = "0";
            bv.Add(themmoi1);
            cklBV.DataSource = bv.OrderBy(p => p.MaBV).ToList();
            for (int i = 0; i < cklBV.ItemCount; i++)
            {
                cklBV.SetItemChecked(i, true);
            }
        }

        private void LupNgayden_EditValueChanged(object sender, EventArgs e)
        {
            DateTime Ngaytu = DungChung.Ham.NgayTu(LupNgaytu.DateTime);
            DateTime Ngayden = DungChung.Ham.NgayDen(LupNgayden.DateTime);
            var bv = (from a in Data.BenhNhans.Where(p => p.NNhap >= Ngaytu && p.NNhap <= Ngayden)
                      join b in Data.BenhViens on a.MaBV equals b.MaBV
                      select b).Distinct().ToList();
            if (radTT.SelectedIndex == 0)
            {
                bv = (from a in Data.BenhNhans
                      join b in Data.BenhViens on a.MaBV equals b.MaBV
                      join vp in Data.VienPhis on a.MaBNhan equals vp.MaBNhan
                      where (vp.NgayTT >= Ngaytu && vp.NgayTT <= Ngayden)
                      select b).Distinct().ToList();
            }
            BenhVien themmoi1 = new BenhVien();
            themmoi1.TenBV = "Chọn tất cả";
            themmoi1.MaBV = "0";
            bv.Add(themmoi1);
            cklBV.DataSource = bv.OrderBy(p => p.MaBV).ToList();
            for (int i = 0; i < cklBV.ItemCount; i++)
            {
                cklBV.SetItemChecked(i, true);
            }
        }

        private void radTT_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime Ngaytu = DungChung.Ham.NgayTu(LupNgaytu.DateTime);
            DateTime Ngayden = DungChung.Ham.NgayDen(LupNgayden.DateTime);
            var bv = (from a in Data.BenhNhans.Where(p => p.NNhap >= Ngaytu && p.NNhap <= Ngayden)
                      join b in Data.BenhViens on a.MaBV equals b.MaBV
                      select b).Distinct().ToList();
            if (radTT.SelectedIndex == 0)
            {
                bv = (from a in Data.BenhNhans
                      join b in Data.BenhViens on a.MaBV equals b.MaBV
                      join vp in Data.VienPhis on a.MaBNhan equals vp.MaBNhan
                      where (vp.NgayTT >= Ngaytu && vp.NgayTT <= Ngayden)
                      select b).Distinct().ToList();
            }
            BenhVien themmoi1 = new BenhVien();
            themmoi1.TenBV = "Chọn tất cả";
            themmoi1.MaBV = "0";
            bv.Add(themmoi1);
            cklBV.DataSource = bv.OrderBy(p => p.MaBV).ToList();
            for (int i = 0; i < cklBV.ItemCount; i++)
            {
                cklBV.SetItemChecked(i, true);
            }
        }
    }
}