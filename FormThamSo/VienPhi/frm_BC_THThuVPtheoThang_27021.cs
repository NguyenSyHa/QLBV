using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils;

namespace QLBV.FormThamSo
{
    public partial class frm_BC_THThuVPtheoThang_27021 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_THThuVPtheoThang_27021()
        {
            InitializeComponent();
        }

        private void frm_BC_THThuVP_27021_Load(object sender, EventArgs e)
        {
            List<int> lthang = new List<int>();
            for (int i = 1; i <= 12; i++)
            {
                lthang.Add(i);
            }
            lupThang.Properties.DataSource = lthang;
            List<int> lnam = new List<int>();
            int namht = DateTime.Now.Year;
            for (int i = namht - 5; i <= namht; i++)
            {
                lnam.Add(i);
            }
            lupNam.Properties.DataSource = lnam;
            lupThang.EditValue = DateTime.Now.Month;
            lupNam.EditValue = DateTime.Now.Year;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        List<TTVienPhi> _lvienphi = new List<TTVienPhi>();
        private void btnInBC_Click(object sender, EventArgs e)
        {
            _lvienphi.Clear();
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<DateTime> lngay = new List<DateTime>();
            int thang = Convert.ToInt32(lupThang.EditValue);
            int nam = Convert.ToInt32(lupNam.EditValue);
            DateTime firstDayOfMonth = new DateTime(nam, thang, 1);
            DateTime lastDayOfMonth = new DateTime(nam, thang, DateTime.DaysInMonth(nam, thang));
            for (DateTime i = firstDayOfMonth; i <= lastDayOfMonth; i = i.AddDays(1.0))
            {
                if (CheckDate(i) == 2)
                {
                    lngay.Add(i);
                }
            }
            DateTime tungay = DungChung.Ham.NgayTu(lngay.First().Date);
            DateTime denngay = DungChung.Ham.NgayDen(lngay.Last().Date);

            var qtu = (from tu in data.TamUngs
                       join bn in data.BenhNhans on tu.MaBNhan equals bn.MaBNhan
                       join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                       select new { bn.NoiTru, tu.IDTamUng, tu.PhanLoai, tu.SoTien, tu.TienChenh, tu.NgayThu, tu.MaBNhan }).ToList();
            var qtu2 = (from tu in qtu.Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay)
                        group tu by new { NgayThu = tu.NgayThu.Value.Date } into kq
                        select new
                        {
                            NgayThu = kq.Key.NgayThu,
                            ThuNT = kq.Where(p => p.NoiTru == 1).Where(p => p.PhanLoai == 1 || p.PhanLoai == 2).Sum(p => p.SoTien),
                            ThuNgT = kq.Where(p => p.NoiTru == 0).Where(p => p.PhanLoai == 1 || p.PhanLoai == 2).Sum(p => p.SoTien),
                            ThuTU = kq.Where(p => p.PhanLoai == 0).Sum(p => p.SoTien),
                            ThuThem = kq.Where(p => p.PhanLoai == 1).Sum(p => p.TienChenh),
                            TraLai = kq.Where(p => p.PhanLoai == 2).Sum(p => p.TienChenh),
                            NoiTruThuThem = kq.Where(p => p.NoiTru == 1).Where(p => p.PhanLoai == 1 || p.PhanLoai == 2).Sum(p => p.SoTien) - kq.Where(p => p.PhanLoai == 1).Sum(p => p.TienChenh),
                        }).OrderBy(p => p.NgayThu).ToList();
            if (qtu2.Count > 0)
            {
                for (int i = 0; i < qtu2.Count; i++)
                {
                    TTVienPhi moi = new TTVienPhi();
                    if (i < 5)
                        moi.Tuan = "Tuần 1";
                    if (i >= 5 && i < 10)
                        moi.Tuan = "Tuần 2";
                    if (i >= 10 && i < 15)
                        moi.Tuan = "Tuần 3";
                    if (i >= 15 && i < 20)
                        moi.Tuan = "Tuần 4";
                    if (i >= 20)
                        moi.Tuan = "Tuần 5";
                    moi.NgayThu = qtu2[i].NgayThu;
                    moi.ThuNT = qtu2[i].ThuNT;
                    moi.ThuNgT = qtu2[i].ThuNgT;
                    moi.ThuThem = qtu2[i].ThuThem;
                    moi.ThuTU = qtu2[i].ThuTU;
                    moi.TraLai = qtu2[i].TraLai;
                    moi.NoiTruThuThem = qtu2[i].NoiTruThuThem;
                    _lvienphi.Add(moi);
                }
            }
            BaoCao.rep_BC_THThuVPTheoThang_MauDoc_27021 rep1 = new BaoCao.rep_BC_THThuVPTheoThang_MauDoc_27021();
            frmIn frm = new frmIn();
            rep1.lblThang.Text = "Tháng " + thang + " năm " + nam;
            rep1.DataSource = _lvienphi.ToList();
            rep1.BindingData();
            rep1.CreateDocument();
            frm.prcIN.PrintingSystem = rep1.PrintingSystem;
            frm.ShowDialog();
        }

        private int CheckDate(DateTime date)
        {
            int ngay = 0;
            if (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday)
            {
                return ngay = 8;
            }
            else
                return ngay = 2;
        }

        #region class TTVienPhi
        private class TTVienPhi
        {
            public string Tuan { get; set; }
            public DateTime NgayThu { get; set; }
            public double? ThuNT { get; set; }
            public double? ThuNgT { get; set; }
            public double? ThuTU { get; set; }
            public double? ThuThem { get; set; }
            public double? TraLai { get; set; }
            public double? NoiTruThuThem { get; set; }
        }
        #endregion
    }
}