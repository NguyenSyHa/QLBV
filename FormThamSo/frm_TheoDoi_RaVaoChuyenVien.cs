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
    public partial class frm_TheoDoi_RaVaoChuyenVien : Form
    {
        public frm_TheoDoi_RaVaoChuyenVien()
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
            public int? noitru { get; set; }
            public int? MaBN { get; set; }
            public int? GTinh { get; set; }
            public int? TuoiNam { get; set; }
            public int? TuoiNu { get; set; }
            public string DiaChi { get; set; }
            public string MaDT { get; set; }
            public string NgheNghiep { get; set; }
            public string MaNN { get; set; }
            public DateTime? NgayVao { get; set; }
            public DateTime? NgayRa { get; set; }
            public string ChanDoan { get; set; }
            public int? SoNgayDT { get; set; }
        }
        public class c_BN_Ngoaitru
        {
            public string TenBN { get; set; }
            public bool dtnt { get; set; }
            public int? noitru { get; set; }

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
        public class c_BN_C2
        {
            public string TenBN { get; set; }
            public bool dtnt { get; set; }
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
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frm_TheoDoi_RaVaoChuyenVien_Load(object sender, EventArgs e)
        {
            lupKhoa.EditValue = DungChung.Bien.MaKP;
            radBN.SelectedIndex = 1;
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
            var KP = (from kp1 in _Data.KPhongs.Where(p => p.PLoai == ("Lâm sàng")) select new { kp1.TenKP, kp1.MaKP }).ToList();
            if (KP.Count > 0)
            {
                lupKhoa.Properties.DataSource = KP.OrderBy(p => p.TenKP).ToList();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (lupKhoa.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn khoa phòng!");
                return;
            }
            Dictionary<string, object> _dic = new Dictionary<string, object>();
            _dic.Add("Khoa", lupKhoa.Text);
            _dic.Add("NgayThang", "Từ ngày " + lupTuNgay.Text + " đến ngày " + lupDenNgay.Text);
            List<c_BN> _lBN = new List<c_BN>();
            List<c_BN_Ngoaitru> _lBN_NT = new List<c_BN_Ngoaitru>();
            List<c_BN_C2> _lBN_C2 = new List<c_BN_C2>();
            DateTime _tungay = System.DateTime.Now.Date;
            DateTime _denngay = System.DateTime.Now.Date;
            int _makp = 0;
            _makp = lupKhoa.EditValue == null ? 0 : Convert.ToInt32(lupKhoa.EditValue);
            _tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            _denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            //bn ngoại trú
            if (radBN.SelectedIndex == 0)
            {
                bool dtnt = true;
                var _dsbn1 = (from vv in _Data.VaoViens
                              join bnkb in _Data.BNKBs on vv.MaBNhan equals bnkb.MaBNhan
                              join bn in _Data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                              join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan into k
                              join ttbx in _Data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                              from k1 in k.DefaultIfEmpty()
                              where bnkb.MaKP == _makp && bn.MaKP == _makp
                              select new
                              {
                                  bn.MaKP,
                                  bn.DTNT,
                                  bn.TenBNhan,
                                  bn.MaBNhan,
                                  bn.Tuoi,
                                  bn.GTinh,
                                  bn.DChi,
                                  bn.MaDTuong,
                                  vv.NgayVao,
                                  bnkb.ChanDoan,
                                  ttbx.MaNN,
                                  NgayRa = k1 != null ? k1.NgayRa : null,
                                  SoNgayDT = k1 != null ? k1.SoNgaydt : null,
                              }).Where(p => p.DTNT == true).ToList();
                var tt = (from ttbs in _dsbn1
                          join nn in _Data.DmNNs on ttbs.MaNN equals nn.MaNN into k
                          from k1 in k.DefaultIfEmpty()
                          select new
                          {
                              ttbs.MaBNhan,
                              TenNN = k1 != null ? k1.TenNN : "",
                          }).ToList();
                if (radSX.SelectedIndex == 0)
                {
                    _dsbn1 = _dsbn1.Where(p => p.NgayVao >= _tungay && p.NgayVao <= _denngay).OrderBy(p => p.NgayVao).ThenBy(p => p.TenBNhan).ToList();
                }
                if (radSX.SelectedIndex == 1)
                {
                    _dsbn1 = _dsbn1.Where(p => p.NgayRa >= _tungay && p.NgayRa <= _denngay).OrderBy(p => p.NgayRa).ThenBy(p => p.TenBNhan).ToList();
                }
                foreach (var item in _dsbn1)
                {
                    c_BN_Ngoaitru a = new c_BN_Ngoaitru();
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
                    a.SoNgayDT = item.SoNgayDT;
                    var d = tt.Where(p => p.MaBNhan == item.MaBNhan).ToList();
                    if (d.Count > 0)
                    {
                        a.NgheNghiep = d.First().TenNN;
                    }
                    a.NgayVao = item.NgayVao;
                    a.NgayRa = item.NgayRa;
                    _lBN_NT.Add(a);
                }
                DungChung.Ham.Print(DungChung.PrintConfig.rep_TheoDoi_VaoRaVien, _lBN_NT, _dic, false);
            }
            //bn nội trú
            if (radBN.SelectedIndex == 1)
            {
                int noitru = 1;
                var _dsbn = (from vv in _Data.VaoViens
                             join bnkb in _Data.BNKBs on vv.MaBNhan equals bnkb.MaBNhan
                             join bn in _Data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan into k
                             join ttbx in _Data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                             from k1 in k.DefaultIfEmpty()
                             where bnkb.MaKP == _makp && bn.MaKP == _makp
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
                                 vv.NgayVao,
                                 NgayRa = k1 != null ? k1.NgayRa : null,
                                 SoNgayDT = k1 != null ? k1.SoNgaydt : null,
                                 bnkb.ChanDoan,
                                 ttbx.MaNN,
                             }).Where(p => p.NoiTru == 1).ToList(); ;
                var tt = (from ttbs in _dsbn
                          join nn in _Data.DmNNs on ttbs.MaNN equals nn.MaNN into k
                          from k1 in k.DefaultIfEmpty()
                          select new
                          {
                              ttbs.MaBNhan,
                              TenNN = k1 != null ? k1.TenNN : "",
                          }).ToList();
                if (radSX.SelectedIndex == 0)
                {
                    _dsbn = _dsbn.Where(p => p.NgayVao >= _tungay && p.NgayVao <= _denngay).OrderBy(p => p.NgayVao).ThenBy(p => p.TenBNhan).ToList();
                }
                if (radSX.SelectedIndex == 1)
                {
                    _dsbn = _dsbn.Where(p => p.NgayRa >= _tungay && p.NgayRa <= _denngay).OrderBy(p => p.NgayRa).ThenBy(p => p.TenBNhan).ToList();
                }
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
                    a.SoNgayDT = item.SoNgayDT;
                    a.NgayVao = item.NgayVao;
                    var d = tt.Where(p => p.MaBNhan == item.MaBNhan).ToList();
                    if (d.Count > 0)
                    {
                        a.NgheNghiep = d.First().TenNN;
                    }
                    a.NgayRa = item.NgayRa;
                    _lBN.Add(a);
                }
                    DungChung.Ham.Print(DungChung.PrintConfig.rep_TheoDoi_VaoRaVien, _lBN, _dic, false);
            }
            //cả hai
            if (radBN.SelectedIndex == 2)
            {
                var _dsbn = (from vv in _Data.VaoViens
                             join bnkb in _Data.BNKBs on vv.MaBNhan equals bnkb.MaBNhan
                             join bn in _Data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                             join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan into k
                             join ttbx in _Data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                             from k1 in k.DefaultIfEmpty()
                             where bnkb.MaKP == _makp && bn.MaKP == _makp
                             select new
                             {
                                 bn.MaKP,
                                 bn.NoiTru,
                                 bn.DTNT,
                                 bn.TenBNhan,
                                 bn.MaBNhan,
                                 bn.Tuoi,
                                 bn.GTinh,
                                 bn.DChi,
                                 bn.MaDTuong,
                                 vv.NgayVao,
                                 NgayRa = k1 != null ? k1.NgayRa : null,
                                 SoNgayDT = k1 != null ? k1.SoNgaydt : null,
                                 bnkb.ChanDoan,
                                 ttbx.MaNN,
                             }).ToList();
                var tt = (from ttbs in _dsbn
                          join nn in _Data.DmNNs on ttbs.MaNN equals nn.MaNN into k
                          from k1 in k.DefaultIfEmpty()
                          select new
                          {
                              ttbs.MaBNhan,
                              TenNN = k1 != null ? k1.TenNN : "",
                          }).ToList();
                if (radSX.SelectedIndex == 0)
                {
                    _dsbn = _dsbn.Where(p => p.NgayVao >= _tungay && p.NgayVao <= _denngay).OrderBy(p => p.NgayVao).ThenBy(p => p.TenBNhan).ToList();
                }
                if (radSX.SelectedIndex == 1)
                {
                    _dsbn = _dsbn.Where(p => p.NgayRa >= _tungay && p.NgayRa <= _denngay).OrderBy(p => p.NgayRa).ThenBy(p => p.TenBNhan).ToList();
                }
                foreach (var item in _dsbn)
                {
                    c_BN_C2 a = new c_BN_C2();
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
                    a.SoNgayDT = item.SoNgayDT;
                    a.NgayVao = item.NgayVao;
                    var d = tt.Where(p => p.MaBNhan == item.MaBNhan).ToList();
                    if (d.Count > 0)
                    {
                        a.NgheNghiep = d.First().TenNN;
                    }
                    a.NgayRa = item.NgayRa;
                    _lBN_C2.Add(a);
                }
                DungChung.Ham.Print(DungChung.PrintConfig.rep_TheoDoi_VaoRaVien, _lBN_C2, _dic, false);
            }
        }
    }
}