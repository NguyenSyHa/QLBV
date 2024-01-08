using System;
using QLBV_Database;
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
    public partial class frm_dsBNlinhthuoc : DevExpress.XtraEditors.XtraForm
    {
        public frm_dsBNlinhthuoc()
        {
            InitializeComponent();
            colNgayKe.Visible = false;
        }
        public frm_dsBNlinhthuoc(int makp, int sopl, int madv, int status, double dongia)
        {
            InitializeComponent();
            _ttxem = status;
            _sopl = sopl;
            _madv = madv;
            _makp = makp;
            _dongia = dongia;
            if (DungChung.Bien.MaBV == "14017")
                colTenBNhan.GroupIndex = 0;
            else
                colNgayKe.Visible = false;
            colMaBN.Visible = false;
        }
        public frm_dsBNlinhthuoc(int makp, int sopl, int madv, int status, double dongia, bool isXuatNoiTru)
        {
            InitializeComponent();
            _ttxem = status;
            _sopl = sopl;
            _madv = madv;
            _makp = makp;
            _dongia = dongia;
            if (DungChung.Bien.MaBV == "14017")
                colTenBNhan.GroupIndex = 0;
            else
                colNgayKe.Visible = false;
            if (isXuatNoiTru)
            {
                colDiaChi.Visible = false;
                colNgayKe.Visible = false;
                colTuoi.Visible = false;
            }
        }
        int _ttxem = 0;
        int _sopl = 0;
        int _madv = 0, _makp = 0;
        double _dongia = 0;
        //0 là xem DSBN chưa có số PL
        //1 xem DSBN có số PL
        List<FormNhap.frmPhieulinh.LDSBNhan> _lBnhan = new List<FormNhap.frmPhieulinh.LDSBNhan>();
        string tenthuoc = "";
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public frm_dsBNlinhthuoc(List<FormNhap.frmPhieulinh.LDSBNhan> lbn, string tt, int status, double dongia)
        {
            InitializeComponent();
            _lBnhan = lbn;
            tenthuoc = tt;
            _ttxem = status;
            _dongia = dongia;
            colNgayKe.Visible = false;
        }
        //form Phieu linh new
        List<FormNhap.frmPhieuLinh_New.LDSBNhan> _lBnhan_new = new List<FormNhap.frmPhieuLinh_New.LDSBNhan>();
        public frm_dsBNlinhthuoc(List<FormNhap.frmPhieuLinh_New.LDSBNhan> lbn, string tt, int status, double dongia)
        {
            InitializeComponent();
            _lBnhan_new = lbn;
            tenthuoc = tt;
            _ttxem = status;
            _dongia = dongia;
            colNgayKe.Visible = false;
        }

        private void frm_dsBNlinhthuoc_Load(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "14017")
            {
                colDiaChi.Caption = "Giường";
            }
            this.Text = "Danh sách BN lĩnh thuốc: " + tenthuoc;
            switch (_ttxem)
            {
                case 0:
                    if (_lBnhan.Count() > 0)
                        grcDsBNhan.DataSource = _lBnhan;
                    if (_lBnhan_new.Count() > 0)
                        grcDsBNhan.DataSource = _lBnhan_new;
                    break;
                case 1:
                    var dsbn = (from bn in _data.BenhNhans
                                join dt in _data.DThuocs.Where(p => _makp == 0 ? true : p.MaKP == _makp) on bn.MaBNhan equals dt.MaBNhan
                                join dtct in _data.DThuoccts.Where(p => p.SoPL == _sopl && p.MaDV == _madv && p.DonGia == _dongia).Where(p => p.Status != -1) on dt.IDDon equals dtct.IDDon
                                select new BN { TenBNhan = bn.TenBNhan, MaBNhan = bn.MaBNhan, Tuoi = bn.Tuoi, DChi = bn.DChi, SoLuong = dtct.SoLuong, NgayKe = dt.NgayKe }).ToList();
                    if (dsbn.Count > 0)
                    {
                        if (DungChung.Bien.MaBV == "14017")
                        {
                            var maBNhans = dsbn.Select(o => o.MaBNhan).Distinct().ToList();
                            var bnkb = _data.BNKBs.Where(o => o.Giuong != null && maBNhans.Contains(o.MaBNhan ?? 0));
                            foreach (var item in dsbn)
                            {
                                item.DChi = bnkb.Where(o => o.MaBNhan == item.MaBNhan).OrderByDescending(o => o.IDKB).FirstOrDefault().Giuong.Split(';').LastOrDefault();
                            }
                        }
                        grcDsBNhan.DataSource = dsbn;
                    }
                    break;
            }

        }

        public class BN
        {
            public string TenBNhan { get; set; }
            public int MaBNhan { get; set; }
            public int? Tuoi { get; set; }
            public string DChi { get; set; }
            public double SoLuong { get; set; }
            public DateTime? NgayKe { get; set; }
        }
    }
}