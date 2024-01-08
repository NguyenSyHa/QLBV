using QLBV_Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBV.FormThamSo
{
    public partial class frm_LoiNhuan : Form
    {
        public frm_LoiNhuan()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        public class Loinhuan
        {
            public int? MaDV { get; set; }
            public DateTime? NgayXuat { get; set; }
            public DateTime? NgayNhap { get; set; }
            public string TenThuoc { get; set; }
            public string MaNoiBo { get; set; }
            public string HamLuong { get; set; }
            public string DonVi { get; set; }
            public double? GiaNhap { get; set; }
            public double SoLuongX { get; set; }
            public double? TTGiaNhap { get; set; }
            public double DonGia { get; set; }
            public double TTGiaXuat { get; set; }
            public double? LoiNhuan { get; set; }
        }
        private void frm_LoiNhuan_Load(object sender, EventArgs e)
        {
            lupTimMaKP.EditValue = DungChung.Bien.MaKP;
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
            var KP = (from kp1 in _Data.KPhongs.Where(p => p.PLoai == ("Khoa dược")) select new { kp1.TenKP, kp1.MaKP }).ToList();
            if (KP.Count > 0)
            {
                lupTimMaKP.Properties.DataSource = KP.OrderBy(p => p.TenKP).ToList();
            }
            
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (lupTimMaKP.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn khoa phòng!");
                return;
            }
            Dictionary<string, object> _dic = new Dictionary<string, object>();
            _dic.Add("Khoa", lupTimMaKP.Text);
            _dic.Add("NgayThang", "Từ ngày " + lupTuNgay.Text + " đến ngày " + lupDenNgay.Text);
            List<Loinhuan> _ln = new List<Loinhuan>();
            DateTime _tungay = System.DateTime.Now.Date;
            DateTime _denngay = System.DateTime.Now.Date;
            _tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            _denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            int _makp = 0;
            _makp = lupTimMaKP.EditValue == null ? 0 : Convert.ToInt32(lupTimMaKP.EditValue);
            var _dl = (from dv in _Data.DichVus
                       join nhapdct in _Data.NhapDcts on dv.MaDV equals nhapdct.MaDV
                       join nhapd in _Data.NhapDs on nhapdct.IDNhap equals nhapd.IDNhap
                       where nhapd.PLoai == 2 && nhapd.KieuDon == 1 || nhapd.KieuDon == 0
                       where nhapd.NgayNhap >= _tungay && nhapd.NgayNhap <= _denngay
                       group new {dv,nhapd,nhapdct} by new {dv.MaTam, dv.TenDV,dv.MaDV,dv.HamLuong,dv.DonVi,dv.GiaNhap, nhapd.MaKP, nhapd.PLoai,nhapd.KieuDon,nhapdct.DonGia } into kq
                       select new
                          {
                           kq.Key.MaKP,
                           kq.Key.MaTam,
                           kq.Key.TenDV,
                           kq.Key.HamLuong,
                           kq.Key.DonVi,
                           kq.Key.GiaNhap,
                           soluongxuat = kq.Sum(p => p.nhapdct.SoLuongX),
                           kq.Key.DonGia,
                       }).Distinct().Where(p => p.MaKP == _makp).ToList();
            foreach (var item in _dl)
            {
                Loinhuan a = new Loinhuan();
                a.MaNoiBo = item.MaTam;
                a.HamLuong = item.HamLuong;
                a.TenThuoc = item.TenDV;
                a.DonVi = item.DonVi;
                a.SoLuongX = item.soluongxuat;
                a.GiaNhap = item.GiaNhap;
                a.TTGiaNhap =item.soluongxuat * item.GiaNhap;
                a.DonGia = item.DonGia;
                a.TTGiaXuat = item.DonGia * item.soluongxuat;
                a.LoiNhuan = a.TTGiaXuat - a.TTGiaNhap;
                _ln.Add(a);
            }
            DungChung.Ham.Print(DungChung.PrintConfig.Rep_BC_LoiNhuan, _ln, _dic, false);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
