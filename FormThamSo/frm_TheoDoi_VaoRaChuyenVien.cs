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
    public partial class frm_TheoDoi_VaoRaChuyenVien : DevExpress.XtraEditors.XtraForm
    {
        public frm_TheoDoi_VaoRaChuyenVien()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public class c_Check
        {
            string tenkp;
            public string khoaphong
            {
                set { tenkp = value; }
                get { return tenkp; }
            }
        }
        List<c_Check> _kp = new List<c_Check>();
        public class c_BN
        {
            public string TenBN { get; set; }
            public int? MaBN { get; set; }
            public int? GTinh { get; set; }
            public int? TuoiNam { get; set; }
            public int? TuoiNu { get; set; }
            public string DiaChi { get; set; }
            public string MaDT { get; set; }
            public string NgheNghiep { get; set; }
            public DateTime? NgayVao { get; set; }
            public DateTime? NgayRa { get; set; }
            public string ChanDoan { get; set; }
            public int? SoNgayDT { get; set; }
        }
        private void Taoso_Click(object sender, EventArgs e)
        {
            if (LupKhoaPhong.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn khoa phòng!");
                return;
            }
            Dictionary<string, object> _dic = new Dictionary<string, object>();
            _dic.Add("Khoa", LupKhoaPhong.Text);
            _dic.Add("NgayThang", "Từ ngày " + LupNgayTu.Text + " đến ngày " + LupNgayDen.Text);
            List<c_BN> _lBN = new List<c_BN>();
            DateTime _tungay = System.DateTime.Now.Date;
            DateTime _denngay = System.DateTime.Now.Date;
            int _makp = 0;
            int noitru = 1;
            _makp = LupKhoaPhong.EditValue == null ? 0 : Convert.ToInt32(LupKhoaPhong.EditValue);
            _tungay = DungChung.Ham.NgayTu(LupNgayTu.DateTime);
            _denngay = DungChung.Ham.NgayDen(LupNgayDen.DateTime);
            //DS Bệnh nhân
            var _dsbn = (from rv in _Data.RaViens.Where(p => p.NgayVao >= _tungay && p.NgayVao <= _denngay && p.MaKP == _makp)
                         join ttbx in _Data.TTboXungs on rv.MaBNhan equals ttbx.MaBNhan
                         join bn in _Data.BenhNhans on ttbx.MaBNhan equals bn.MaBNhan
                         select new
                         {
                             bn.MaKP,
                             bn.NoiTru,
                             bn.TenBNhan,
                             bn.MaBNhan,
                             bn.Tuoi,
                             bn.GTinh,
                             bn.DChi,
                             bn.MaDTuong,
                             ttbx.NgheNghiep,
                             rv.NgayVao,
                             rv.NgayRa,
                             rv.SoNgaydt,
                             rv.ChanDoan,
                         }).ToList();
            foreach (var item in _dsbn)
            {
                c_BN a = new c_BN();
                a.MaBN = item.MaBNhan;
                a.TenBN = item.TenBNhan;
                a.DiaChi = item.DChi;
                a.ChanDoan = item.ChanDoan;
                a.GTinh = item.GTinh;
                if (a.GTinh == 1)
                {
                    a.TuoiNam = item.Tuoi;
                }
                else
                {
                    a.TuoiNu = item.Tuoi;
                }
                a.MaDT = item.MaDTuong;
                a.NgheNghiep = item.NgheNghiep;
                a.SoNgayDT = item.SoNgaydt;
                a.NgayVao = item.NgayVao;
                a.NgayRa = item.NgayRa;
                _lBN.Add(a);
            }
            DungChung.Ham.Print(DungChung.PrintConfig.rep_TheoDoi_VaoRaVien, _lBN, _dic, false);
        }

        private void Huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_TheoDoi_VaoRaChuyenVien_Load(object sender, EventArgs e)
        {
            LupKhoaPhong.EditValue = DungChung.Bien.MaKP;
            LupNgayTu.DateTime = System.DateTime.Now;
            LupNgayDen.DateTime = System.DateTime.Now;
            var KP = (from kp1 in _Data.KPhongs.Where(p => p.PLoai == ("Lâm sàng")) select new { kp1.TenKP, kp1.MaKP }).ToList();
            if (KP.Count > 0)
            {
                LupKhoaPhong.Properties.DataSource = KP.OrderBy(p => p.TenKP).ToList();
            }
        }
    }
}
