using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormThamSo
{
    public partial class Frm_BcSuDungThuocTp_NTL : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcSuDungThuocTp_NTL()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private class _BCThanhpham 
        {
            private string _tenbn;

            public string Tenbn
            {
                get { return _tenbn; }
                set { _tenbn = value; }
            }
            private string _chandoan;

            public string Chandoan
            {
                get { return _chandoan; }
                set { _chandoan = value; }
            }
            private int? _tuoi;

            public int? Tuoi
            {
                get { return _tuoi; }
                set { _tuoi = value; }
            }
            private string _tenthuoc;

            public string Tenthuoc
            {
                get { return _tenthuoc; }
                set { _tenthuoc = value; }
            }
            private string _dvt;

            public string Dvt
            {
                get { return _dvt; }
                set { _dvt = value; }
            }
            private double _soluong;

            public double Soluong
            {
                get { return _soluong; }
                set { _soluong = value; }
            }
            private string _diachi;

            public string Diachi
            {
                get { return _diachi; }
                set { _diachi = value; }
            }
            private DateTime? _ghichu;

            public DateTime? Ghichu
            {
                get { return _ghichu; }
                set { _ghichu = value; }
            }
        }


        private void btnReport_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> _dic = new Dictionary<string, object>();
            _dic.Add("TenCQCQ", DungChung.Bien.TenCQCQ);
            _dic.Add("TenCQ", DungChung.Bien.TenCQ);

            string kp1 = lupKP.Text;
            _dic.Add("Khoaphong","Khoa : "+kp1);
            string tenthuoc = lupthuoc.Text;
            List<_BCThanhpham> _listbc = new List<_BCThanhpham>();
            DateTime tungay = DungChung.Ham.NgayTu(Convert.ToDateTime( txtTungay.Text));
            DateTime dengay = DungChung.Ham.NgayDen(Convert.ToDateTime(txtDenngay.Text));
        //        SELECT BenhNhan.MaBNhan, BenhNhan.TenBNhan, DThuoc.MaKP, DThuoc.NgayKe, DichVu.TenDV, TieuNhomDV.TenRG, BNKB.ChanDoan
        //FROM DThuoc INNER JOIN
        //DThuocct ON DThuoc.IDDon = DThuocct.IDDon INNER JOIN
        //DichVu ON DThuocct.MaDV = DichVu.MaDV INNER JOIN
        //TieuNhomDV ON DichVu.IdTieuNhom = TieuNhomDV.IdTieuNhom INNER JOIN
        //BenhNhan ON DThuoc.MaBNhan = BenhNhan.MaBNhan INNER JOIN
        //BNKB ON DThuocct.IDKB = BNKB.IDKB
        //WHERE (DThuoc.NgayKe > CONVERT(DATETIME, '2019-11-01 00:00:00', 102)) AND (TieuNhomDV.TenRG = N'Thuốc hướng tâm thần' OR
        //TieuNhomDV.TenRG = N'Thuốc gây nghiện') AND (DThuoc.MaKP = 33)

            var arr = (from bn in _data.BenhNhans
                       join dt in _data.DThuocs.Where(p => p.NgayKe >= tungay && p.NgayKe <= dengay) on bn.MaBNhan equals dt.MaBNhan
                       join dtct in _data.DThuoccts on dt.IDDon equals dtct.IDDon
                       join dv in _data.DichVus on dtct.MaDV equals dv.MaDV
                       join tndv in _data.TieuNhomDVs.Where(p => p.TenRG == "Thuốc hướng tâm thần" || p.TenRG == "Thuốc gây nghiện") on dv.IdTieuNhom equals tndv.IdTieuNhom
                       join bnkb in _data.BNKBs on dtct.IDKB equals bnkb.IDKB
                       join kp in _data.KPhongs on dt.MaKP equals kp.MaKP
                       select new
                       {
                           bn.MaBNhan,
                           bn.TenBNhan,
                           bn.DChi,
                           dt.MaKP,
                           dt.NgayKe,
                           dv.TenDV,
                           tenthuoc2 = dv.TenDV +" "+ dv.HamLuong ,
                           tndv.TenRG,
                           bnkb.ChanDoan,
                           bn.Tuoi,
                           dtct.DonVi,
                           dtct.SoLuong,
                           kp.TenKP
                       }).Where(x => kp1 == "" ? true : x.TenKP == kp1).Where(x => tenthuoc == "" ? true : x.TenDV == tenthuoc).Select(p => new 
                       {
                           p.MaBNhan,
                           p.TenBNhan,
                           p.DChi,
                           p.MaKP,
                           p.NgayKe,
                           p.tenthuoc2,
                           p.TenRG,
                           p.ChanDoan,
                           p.Tuoi,
                           p.DonVi,
                           p.SoLuong,
                           p.TenKP
                       }).OrderBy(p=>p.NgayKe).ToList();
            if (arr.Count > 0)
            {
                foreach (var item in arr)
                {
                    _BCThanhpham _bc = new _BCThanhpham();
                    _bc.Tenbn = item.TenBNhan;
                    _bc.Diachi = item.DChi;
                    _bc.Chandoan = item.ChanDoan;
                    _bc.Dvt = item.DonVi;
                    _bc.Tuoi = item.Tuoi;
                    _bc.Ghichu = item.NgayKe;
                    _bc.Soluong = item.SoLuong;
                    _bc.Dvt = item.DonVi;
                    _bc.Tenthuoc = item.tenthuoc2;
                    _listbc.Add(_bc);
                }
                DungChung.Ham.Print(DungChung.PrintConfig.Rp_SoQLyThanhPham_NTL, _listbc, _dic, false);
            }
            else MessageBox.Show("Không có dữ liệu !");
            
        }

        private void Frm_BcSuDungThuocTp_NTL_Load(object sender, EventArgs e)
        {
            var _pb = (from kp in _data.KPhongs.Where(p=>p.PLoai=="Phòng khám" || p.PLoai=="Lâm sàng" ) select new { kp.TenKP});
            lupKP.Properties.DataSource = _pb.ToList();
            var q = (from dv in _data.DichVus
                     join tndv in _data.TieuNhomDVs.Where(p => p.TenRG == "Thuốc hướng tâm thần" || p.TenRG == "Thuốc gây nghiện") on dv.IdTieuNhom equals tndv.IdTieuNhom
                     select new
                     {
                         dv.TenDV
                     });
            lupthuoc.Properties.DataSource = q.ToList();
            
        }
    }
}