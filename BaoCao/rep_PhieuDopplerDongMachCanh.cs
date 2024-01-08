using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class rep_PhieuDopplerDongMachCanh : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieuDopplerDongMachCanh()
        {
            InitializeComponent();
        }

        public rep_PhieuDopplerDongMachCanh(int idcls)
        {
            InitializeComponent();
            this._IdCLS = idcls;
        }
        int _IdCLS = 0;

        internal void BindingData()
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var thongtin = (from cl in data.CLS.Where(p => p.IdCLS == _IdCLS)
                           join cd in data.ChiDinhs on cl.IdCLS equals cd.IdCLS
                           join dv in data.DichVus on cd.MaDV equals dv.MaDV
                           select new { cd.MaDV, cd.IDCD, cl.MaKP, cl.MaBNhan, cl.MaCB, cl.MaCBth, cl.NgayTH, cl.MaKPth, cd.ChiDinh1, dv.TenDV }).ToList();
            if (thongtin.Count > 0)
            {
                string macbth = thongtin.First().MaCBth;
                int mabn = thongtin.First().MaBNhan ?? 0;

                var canb = (from canbo in data.CanBoes.Where(p => p.MaCB == macbth) select new { canbo.MaCB, canbo.TenCB }).FirstOrDefault();
                if (canb != null)
                    celBS.Text = "BS. " + canb.TenCB.ToUpper();

                var qbn = data.BenhNhans.Where(parameters => parameters.MaBNhan == mabn).FirstOrDefault();
                if (qbn != null)
                {
                    celHoTen.Text = qbn.TenBNhan.ToUpper();
                    celDiaChi.Text = qbn.DChi;
                    celTuoi.Text = (qbn.Tuoi ?? 0).ToString();
                }
                celYcSieuAm.Text = thongtin.First().TenDV;
                // lấy tên khoa phòng thực hiện
                if (DungChung.Bien.MaBV == "27023") 
                {
                    int makhoaphongTH = 0;
                    makhoaphongTH = thongtin.First().MaKP == null ? 0 : thongtin.First().MaKPth.Value;
                    var NhomkhoaTH = data.KPhongs.Where(p => p.MaKP == makhoaphongTH).Select(p => p.NhomKP).First();
                    var khoaTH = (from h in data.KPhongs.Where(p => p.MaKP == NhomkhoaTH) select new { h.TenKP }).ToList();
                    string tenkhoaTH = khoaTH.Count > 0 ? khoaTH.First().TenKP.ToString() : "";
                    this.txtKhoaTH.Text = tenkhoaTH.ToUpper();
                }
            }

            var qkq = (from h in data.ChiDinhs.Where(p => p.IdCLS == _IdCLS) join h1 in data.CLScts on h.IDCD equals h1.IDCD join h2 in data.DichVucts on h1.MaDVct equals h2.MaDVct select new { h1.KetQua, h.KetLuan, h.LoiDan, h.MoTa, h2.STT, h.NgayTH }).FirstOrDefault();

            List<string> lkq = new List<string>();
            if (qkq != null)
            {
                if (qkq.KetQua != null)
                {
                    lkq = qkq.KetQua.Split(';').ToList();
                    if (lkq.Count > 0)
                        cel11.Text = lkq.First();
                    if (lkq.Count > 1)
                        cel12.Text = lkq.Skip(1).First();
                    if (lkq.Count > 2)
                        cel13.Text = lkq.Skip(2).First();
                    if (lkq.Count > 3)
                        cel14.Text = lkq.Skip(3).First();
                    if (lkq.Count > 4)
                        cel15.Text = lkq.Skip(4).First();
                    if (lkq.Count > 5)
                        cel16.Text = lkq.Skip(5).First();
                    if (lkq.Count > 6)
                        cel17.Text = lkq.Skip(6).First();
                    if (lkq.Count > 7)
                        cel18.Text = lkq.Skip(7).First();
                    if (lkq.Count > 8)
                        cel21.Text = lkq.Skip(8).First();
                    if (lkq.Count > 9)
                        cel22.Text = lkq.Skip(9).First();
                    if (lkq.Count > 10)
                        cel23.Text = lkq.Skip(10).First();
                    if (lkq.Count > 11)
                        cel24.Text = lkq.Skip(11).First();
                    if (lkq.Count > 12)
                        cel25.Text = lkq.Skip(12).First();
                    if (lkq.Count > 13)
                        cel26.Text = lkq.Skip(13).First();
                    if (lkq.Count > 14)
                        cel27.Text = lkq.Skip(14).First();
                    if (lkq.Count > 15)
                        cel28.Text = lkq.Skip(15).First();
                    if (lkq.Count > 16)
                        cel31.Text = lkq.Skip(16).First();
                    if (lkq.Count > 17)
                        cel32.Text = lkq.Skip(17).First();
                    if (lkq.Count > 18)
                        cel33.Text = lkq.Skip(18).First();
                    if (lkq.Count > 19)
                        cel34.Text = lkq.Skip(19).First();
                    if (lkq.Count > 20)
                        cel35.Text = lkq.Skip(20).First();
                    if (lkq.Count > 21)
                        cel36.Text = lkq.Skip(21).First();
                    if (lkq.Count > 22)
                        cel37.Text = lkq.Skip(22).First();
                    if (lkq.Count > 23)
                        cel38.Text = lkq.Skip(23).First();
                    if (lkq.Count > 24)
                        cel41.Text = lkq.Skip(24).First();
                    if (lkq.Count > 25)
                        cel42.Text = lkq.Skip(25).First();
                    if (lkq.Count > 26)
                        cel43.Text = lkq.Skip(26).First();
                    if (lkq.Count > 27)
                        cel44.Text = lkq.Skip(27).First();
                    if (lkq.Count > 28)
                        cel45.Text = lkq.Skip(28).First();
                    if (lkq.Count > 29)
                        cel46.Text = lkq.Skip(29).First();
                    if (lkq.Count > 30)
                        cel47.Text = lkq.Skip(30).First();
                    if (lkq.Count > 31)
                        cel48.Text = lkq.Skip(31).First();
                }
                List<string> lmota = new List<string>();
                if (qkq.MoTa != null)
                {
                    lmota = qkq.MoTa.Split(';').ToList();
                    if (lmota.Count > 0)
                    {
                        celMoTa1.Text = lmota.First();
                    }
                    if (lmota.Count > 1)
                    {
                        celMoTa2.Text = lmota.Skip(1).First();
                    }
                    if (lmota.Count > 2)
                    {
                        celMoTa3.Text = lmota.Skip(2).First();
                    }
                    if (lmota.Count > 3)
                    {
                        celMoTa4.Text = lmota.Skip(3).First();
                    }
                }

                celKL.Text = qkq.KetLuan;
                celLoiDan.Text = qkq.LoiDan;
                celNgayThang.Text = qkq.NgayTH!= null ? DungChung.Bien.DiaDanh + ", " + DungChung.Ham.NgaySangChu(Convert.ToDateTime(qkq.NgayTH)) : DungChung.Bien.DiaDanh + ", " + DungChung.Ham.NgaySangChu(DateTime.Now);

            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV != "27001")
                celNgayThang.Text = DungChung.Bien.DiaDanh + ", " + DungChung.Ham.NgaySangChu(DateTime.Now);

        }
    }
}
