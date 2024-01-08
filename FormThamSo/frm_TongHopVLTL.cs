using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using QLBV.BaoCao;

namespace QLBV.FormThamSo
{
    public partial class frm_TongHopVLTL : Form
    {
        public frm_TongHopVLTL()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void frm_TongHopVLTL_Load(object sender, EventArgs e)
        {
            int thisYear = DateTime.Now.Year;
            for (int i = thisYear; i > 2000; i--)
            {
                cboNam.Properties.Items.Add(i);
            }
            cboNam.SelectedIndex = 0;
            var b = (from kp in _Data.KPhongs.Where(p => p.PLoai.Contains("Lâm sàng") || p.PLoai.Contains("Cận lâm sàng"))
                     select new {
                         kp.MaKP,
                         kp.TenKP
                     }).ToList();
            LupKhoaphong.Properties.DataSource = b;
        }

        private void cboThang_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            Rep_TongHopVLTL rep = new Rep_TongHopVLTL();
            frmIn frm = new frmIn();
            if (string.IsNullOrEmpty(LupKhoaphong.Text) || string.IsNullOrEmpty(cboThang.Text) || string.IsNullOrEmpty(cboNam.Text))
            {
                MessageBox.Show("Chưa đủ thông tin");
                return;
            }
            int MaKP = Convert.ToInt32(LupKhoaphong.EditValue);
            int DaysOfThisMonth = DateTime.DaysInMonth(Convert.ToInt32(cboNam.Text), Convert.ToInt32(cboThang.Text));
            DateTime FirstDayThisMonth = Convert.ToDateTime("01" + "/" + cboThang.Text + "/" + cboNam.Text + " 00:00:00");
            DateTime LastDayThisMonth = Convert.ToDateTime(DaysOfThisMonth + "/" + cboThang.Text + "/" + cboNam.Text + " 23:59:59");
            DateTime FirstDayPreviousMonth = FirstDayThisMonth.AddMonths(-1);
            var tt = (from cd in _Data.ChiDinhs
                      join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                      join cls in _Data.CLS on cd.IdCLS equals cls.IdCLS
                      join tn in _Data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                      where (tn.TenTN.Contains("Thủ Thuật"))
                      where(cd.NgayTH < LastDayThisMonth && cd.NgayTH > FirstDayThisMonth)
                      where(cls.MaKP == MaKP)
                      group dv by new { dv.MaDV, dv.TenDV, dv.DonVi} into k
                      select new ThuThuat
                      {
                          MaDV = k.Key.MaDV,
                          TenDV = k.Key.TenDV,
                          DonVi = k.Key.DonVi,
                          SlThangNay = k.Count(),
                      }).OrderBy(p => p.TenDV).ToList();
            List<ThuThuat> _list = new List<ThuThuat>();
            List<int?> BNThisMonth = new List<int?>();
            List<int?> BNPreviousMonth = new List<int?>();
            List<int?> BNdt1 = new List<int?>();
            List<int?> BNss1 = new List<int?>();
            List<int?> sOnb1 = new List<int?>();
            List<int?> sOnbss1 = new List<int?>();
            //List<int?> ngaydt1 = new List<int?>();
            //List<int?> ngaydtss1 = new List<int?>();
            int SumThisMonth = 0;
            int SumPreviousMonth = 0;
            foreach (var item in tt)
            {
                int slTrc = (from cd in _Data.ChiDinhs
                             join cls in _Data.CLS on cd.IdCLS equals cls.IdCLS
                             where (cd.NgayTH < FirstDayThisMonth && cd.NgayTH > FirstDayPreviousMonth)
                             where (cls.MaKP == MaKP)
                             where (cd.MaDV == item.MaDV ) 
                             select new { cd.IDCD}).Count();

                var bnThis = (from cd in _Data.ChiDinhs
                           join cls in _Data.CLS on cd.IdCLS equals cls.IdCLS
                           where (cd.NgayTH < LastDayThisMonth && cd.NgayTH > FirstDayThisMonth)
                            where (cls.MaKP == MaKP)
                            where (cd.MaDV == item.MaDV)
                           select new
                           {
                               cls.MaBNhan
                           }).Distinct().ToList();
                var bnPrevious = (from cd in _Data.ChiDinhs
                              join cls in _Data.CLS on cd.IdCLS equals cls.IdCLS
                              where (cd.NgayTH < FirstDayThisMonth && cd.NgayTH > FirstDayPreviousMonth)
                              where (cls.MaKP == MaKP)
                              where (cd.MaDV == item.MaDV)
                              select new
                              {
                                  cls.MaBNhan
                              }).Distinct().ToList();
                var bndt = (from bn in _Data.BenhNhans
                              join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                              where (rv.NgayRa < LastDayThisMonth && rv.NgayRa > FirstDayThisMonth)
                              where (bn.Status == 2 || bn.Status == 3)
                              where (bn.MaKP == MaKP)
                              select new
                              {
                                  bn.MaBNhan
                              }).Distinct().ToList();
                var bnss = (from bn in _Data.BenhNhans
                              join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                            where (rv.NgayRa < FirstDayThisMonth && rv.NgayRa > FirstDayPreviousMonth)
                            where (bn.Status == 2 || bn.Status == 3)
                              where (bn.MaKP == MaKP)
                              select new
                              {
                                  bn.MaBNhan
                              }).Distinct().ToList();
                var sobnnt = (from bn in _Data.BenhNhans
                              join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                              where (rv.NgayRa < LastDayThisMonth && rv.NgayRa > FirstDayThisMonth)
                              where (bn.Status == 2 || bn.Status == 3)
                              where (bn.DTNT == true)
                              where (bn.MaKP == MaKP)
                              select new
                              {
                                  bn.MaBNhan
                              }).Distinct().ToList();
                var sobnntss = (from bn in _Data.BenhNhans
                              join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                              where (rv.NgayRa < FirstDayThisMonth && rv.NgayRa > FirstDayPreviousMonth)
                              where (bn.Status == 2 || bn.Status == 3)
                              where (bn.DTNT == true)
                              where (bn.MaKP == MaKP)
                              select new
                              {
                                  bn.MaBNhan
                              }).Distinct().ToList();
                foreach (var i in bnThis)
                {
                    BNThisMonth.Add(i.MaBNhan);
                }
                foreach (var i in bnPrevious)
                {
                    BNPreviousMonth.Add(i.MaBNhan);
                }
                foreach (var i in bndt)
                {
                    BNdt1.Add(i.MaBNhan);
                }
                foreach (var i in bnss)
                {
                    BNss1.Add(i.MaBNhan);
                }
                foreach (var i in sobnnt)
                {
                    sOnb1.Add(i.MaBNhan);
                }
                foreach (var i in sobnntss)
                {
                    sOnbss1.Add(i.MaBNhan);
                }
                //foreach (var i in Ngaydt1)
                //{
                //    ngaydt1.Add(i.SoNgaydt);
                //}
                //foreach (var i in ngaydtss)
                //{
                //    ngaydtss1.Add(i.SoNgaydt);
                //}
                ThuThuat a = new ThuThuat();
                a.TenDV = item.TenDV;
                a.DonVi = item.DonVi;
                a.SlThangNay = item.SlThangNay;
                a.SlThangTruoc = slTrc;
                _list.Add(a);
                SumThisMonth += item.SlThangNay;
                SumPreviousMonth += slTrc;
            }
            ThuThuat b = new ThuThuat();
            if (DungChung.Bien.MaBV != "24012")
            {
                b.TenDV = "Tổng số BN điều trị vật lý trị liệu";
                b.SlThangNay = BNThisMonth.Distinct().Count();
                b.SlThangTruoc = BNPreviousMonth.Distinct().Count();
            }
            else
            {
                b.TenDV = "Số BN điều trị";
                b.SlThangNay = BNdt1.Distinct().Count();
                b.SlThangTruoc = BNss1.Distinct().Count();
            }
            b.DonVi = "Người";
            _list.Add(b);
            if (DungChung.Bien.MaBV == "24012")
            {
                ThuThuat c = new ThuThuat();
                c.TenDV = "Số NB điều trị ngoại trú";
                c.DonVi = "Lượt";
                c.SlThangNay = sOnb1.Distinct().Count();
                c.SlThangTruoc = sOnbss1.Distinct().Count();
                _list.Add(c);

                var Ngaydt1 = (from bn in _Data.BenhNhans.Where(p => p.MaKP == MaKP)
                               join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                               where (rv.NgayRa < LastDayThisMonth && rv.NgayRa > FirstDayThisMonth)
                               where ((bn.Status == 2 || bn.Status == 3) && bn.DTNT == true)
                               select new
                               {
                                   rv.SoNgaydt
                               }).ToList();
                var ngaydtss = (from bn in _Data.BenhNhans.Where(p => p.MaKP == MaKP)
                                join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                                where (rv.NgayRa < FirstDayThisMonth && rv.NgayRa > FirstDayPreviousMonth)
                                where ((bn.Status == 2 || bn.Status == 3) && bn.DTNT == true)
                                select new
                                {
                                    rv.SoNgaydt
                                }).ToList();
                ThuThuat d = new ThuThuat();
                d.TenDV = "Số ngày điều trị ngoại trú";
                d.DonVi = "Ngày";
                d.SlThangNay = Convert.ToInt32(Ngaydt1.Sum(p => p.SoNgaydt));
                d.SlThangTruoc =Convert.ToInt32(ngaydtss.Sum(p => p.SoNgaydt));
                _list.Add(d);
            }
            rep.DataSource = _list;
            rep.TenBC.Value = "BÁO CÁO " + this.LupKhoaphong.Text.ToUpper() + " THÁNG " + cboThang.Text + " NĂM " + cboNam.Text;
            rep.TongThangNay.Value = SumThisMonth;
            rep.TongThangTruoc.Value = SumPreviousMonth;
            rep.TruongKhoa.Value = "";
            rep.NgayThang.Value = DungChung.Bien.DiaDanh + ", " + DungChung.Ham.NgaySangChu(DateTime.Now, 16);
            rep.Binding();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
    }
    public class ThuThuat
    {
        public int MaDV { get; set; }
        public string TenDV { get; set; }
        public string DonVi { get; set; }
        public int SlThangNay { get; set; }
        public int SlThangTruoc { get; set; }
        public DateTime? NgayTH { get; set; }
    }
}
