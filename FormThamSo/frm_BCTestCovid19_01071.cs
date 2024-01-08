using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.FormThamSo
{
    public partial class frm_BCTestCovid19_01071 : Form
    {
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public frm_BCTestCovid19_01071()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tungay.Text) || string.IsNullOrEmpty(denngay.Text))
            {
                MessageBox.Show("Chưa chọn ngày!", "Thông báo");
                return;
            }
            DateTime NgayTu = DungChung.Ham.NgayTu(Convert.ToDateTime(tungay.EditValue));
            DateTime NgayDen = DungChung.Ham.NgayDen(Convert.ToDateTime(denngay.EditValue));

            var q1 = (from bn in data.BenhNhans
                     join ttbs in data.TTboXungs on bn.MaBNhan equals ttbs.MaBNhan
                     join cls in data.CLS.Where(p => p.NgayTH > NgayTu && p.NgayTH < NgayDen) on bn.MaBNhan equals cls.MaBNhan
                     join cd in data.ChiDinhs.Where(p => p.MaDV == 9287 || p.MaDV == 9107 || p.MaDV == 9308) on cls.IdCLS equals cd.IdCLS
                     join dv in data.DichVus on cd.MaDV equals dv.MaDV
                     join clsct in data.CLScts.Where(p => p.Status ==1) on cd.IDCD equals clsct.IDCD
                      select new TTBN{
                          MaBN = bn.MaBNhan,
                          TenBN = bn.TenBNhan,
                          NoiLV = ttbs.NoiLV,
                          NgayTH = cls.NgayTH,
                          KetQua = clsct.KetQua,
                          TenDV = dv.TenDV,
                          DonGia = dv.DonGia
                      }).OrderBy(p => p.NgayTH).ToList();
            List<TTBN> rp = new List<TTBN>();
            foreach(var item in q1)
            {
                TTBN bn = new TTBN();
                bn.MaBN = item.MaBN;
                bn.TenBN = item.TenBN;
                bn.NoiLV = item.NoiLV;
                bn.TenDV = item.TenDV;
                bn.DonGia = item.DonGia;
                bn.Ngay = DungChung.Ham.NgaySangChu(Convert.ToDateTime(item.NgayTH), 7);
                bn.AmTinh = (item.KetQua.ToUpper().Contains("ÂM TÍNH") || item.KetQua.ToUpper().Contains("KHÔNG PHẢN ỨNG")) ? "1" : "";
                bn.DuongTinh = (item.KetQua.ToUpper().Contains("DƯƠNG TÍNH" ) || item.KetQua.ToUpper().Contains("CÓ PHẢN ỨNG")) ? "1" : "";
                rp.Add(bn);
            }

            var lbNgayThang = "Từ " + DungChung.Ham.NgaySangChu(NgayTu) + " đến " + DungChung.Ham.NgaySangChu(NgayDen);
            int TSAm = rp.Where(p => p.AmTinh != "").Count();
            int TSDuong = rp.Where(p => p.DuongTinh != "").Count();

            Dictionary<string, object> _dic = new Dictionary<string, object>();
            _dic.Add("TenCQCQ", DungChung.Bien.TenCQCQ);
            _dic.Add("TenCQ", DungChung.Bien.TenCQ);
            _dic.Add("Ngaythang", lbNgayThang);
            _dic.Add("TSAm", TSAm);
            _dic.Add("TSDuong", TSDuong);

            DungChung.Ham.Print(DungChung.PrintConfig.Rep_BCTestCovid19_01071, rp, _dic, false);
        }
    }
    public class TTBN
    {
        public int MaBN { get; set; }
        public string TenBN { get; set; }
        public DateTime? NgayTH { get; set; }
        public string NoiLV { get; set; }
        public string KetQua { get; set; }
        public string AmTinh { get; set; }
        public string DuongTinh { get; set; }
        public string Ngay { get; set; }
        public string TenDV { get; set; }
        public double DonGia { get; set; }

    }
}
