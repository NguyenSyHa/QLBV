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
    public partial class Frm_DinhMucTheoNgay_30010 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_DinhMucTheoNgay_30010()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public class DinhMuc 
        {
            private string _banKham;

            public string BanKham
            {
                get { return _banKham; }
                set { _banKham = value; }
            }
            private int _soBanKham;

            public int SoBanKham
            {
                get { return _soBanKham; }
                set { _soBanKham = value; }
            }
            private int _dinhMucBan;

            public int DinhMucBan
            {
                get { return _dinhMucBan; }
                set { _dinhMucBan = value; }
            }

            private int _ngay1;

            public int Ngay1
            {
                get { return _ngay1; }
                set { _ngay1 = value; }
            }
            private int _ngay2;

            public int Ngay2
            {
                get { return _ngay2; }
                set { _ngay2 = value; }
            }
            private int _ngay3;

            public int Ngay3
            {
                get { return _ngay3; }
                set { _ngay3 = value; }
            }
            private int _ngay4;

            public int Ngay4
            {
                get { return _ngay4; }
                set { _ngay4 = value; }
            }
            private int _ngay5;

            public int Ngay5
            {
                get { return _ngay5; }
                set { _ngay5 = value; }
            }
            private int _ngay6;

            public int Ngay6
            {
                get { return _ngay6; }
                set { _ngay6 = value; }
            }
            private int _ngay7;

            public int Ngay7
            {
                get { return _ngay7; }
                set { _ngay7 = value; }
            }
            private int _ngay8;

            public int Ngay8
            {
                get { return _ngay8; }
                set { _ngay8 = value; }
            }
            private int _ngay9;

            public int Ngay9
            {
                get { return _ngay9; }
                set { _ngay9 = value; }
            }
            private int _ngay10;

            public int Ngay10
            {
                get { return _ngay10; }
                set { _ngay10 = value; }
            }
            private int _ngay11;

            public int Ngay11
            {
                get { return _ngay11; }
                set { _ngay11 = value; }
            }
            private int _ngay12;

            public int Ngay12
            {
                get { return _ngay12; }
                set { _ngay12 = value; }
            }
            private int _ngay13;

            public int Ngay13
            {
                get { return _ngay13; }
                set { _ngay13 = value; }
            }
            private int _ngay14;

            public int Ngay14
            {
                get { return _ngay14; }
                set { _ngay14 = value; }
            }
            private int _ngay15;

            public int Ngay15
            {
                get { return _ngay15; }
                set { _ngay15 = value; }
            }
            

        }
        private void btnReport_Click(object sender, EventArgs e)
        {
//            SELECT        COUNT(DISTINCT BenhNhan.MaBNhan) AS Expr1, KPhong.TenKP
//FROM            BenhNhan INNER JOIN
//                         BNKB ON BenhNhan.MaBNhan = BNKB.MaBNhan INNER JOIN
//                         KPhong ON BNKB.MaKP = KPhong.MaKP
//WHERE        (KPhong.PLoai = N'Phòng khám') AND (BNKB.NgayKham > CONVERT(DATETIME, '2019-09-01 00:00:00', 102)) AND (BNKB.NgayKham < CONVERT(DATETIME, 
//                         '2019-10-01 00:00:00', 102))
//GROUP BY KPhong.TenKP

            var dsphongkham = (from pk in _data.KPhongs.Where(p => p.PLoai == "Phòng khám")
                               select new { pk.TenKP,pk.MaKP}).ToList();
            DateTime _tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(txtTungay.Text));
            DateTime _denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(txtTungay.Text));
            
            List<DinhMuc> _listDm = new List<DinhMuc>();
            Dictionary<string, object> _dic = new Dictionary<string, object>();
            _dic.Add("TenCQCQ", DungChung.Bien.TenCQCQ);
            _dic.Add("TenCQ", DungChung.Bien.TenCQ);
            _dic.Add("Ngay1", _tungay.ToString("dd/MM/yy"));
            _dic.Add("Ngay2", _tungay.AddDays(1).ToString("dd/MM/yy"));
            _dic.Add("Ngay3", _tungay.AddDays(2).ToString("dd/MM/yy"));
            _dic.Add("Ngay4", _tungay.AddDays(3).ToString("dd/MM/yy"));
            _dic.Add("Ngay5", _tungay.AddDays(4).ToString("dd/MM/yy"));
            _dic.Add("Ngay6", _tungay.AddDays(5).ToString("dd/MM/yy"));
            _dic.Add("Ngay7", _tungay.AddDays(6).ToString("dd/MM/yy"));
            _dic.Add("Ngay8", _tungay.AddDays(7).ToString("dd/MM/yy"));
            _dic.Add("Ngay9", _tungay.AddDays(8).ToString("dd/MM/yy"));
            _dic.Add("Ngay10", _tungay.AddDays(9).ToString("dd/MM/yy"));
            _dic.Add("Ngay11", _tungay.AddDays(10).ToString("dd/MM/yy"));
            _dic.Add("Ngay12", _tungay.AddDays(11).ToString("dd/MM/yy"));
            _dic.Add("Ngay13", _tungay.AddDays(12).ToString("dd/MM/yy"));
            _dic.Add("Ngay14", _tungay.AddDays(13).ToString("dd/MM/yy"));
            _dic.Add("Ngay15", _tungay.AddDays(14).ToString("dd/MM/yy"));
            

            for(int i = 0; i< dsphongkham.Count;i++)
            {
                DinhMuc _dm = new DinhMuc();
                _dm.BanKham = dsphongkham[i].TenKP.ToString();
                int _makp = dsphongkham[i].MaKP;
                var _tsbn = (from bn in _data.BenhNhans join bnkb in _data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan join kphong in _data.KPhongs.Where(x => x.MaKP == _makp) on bnkb.MaKP equals kphong.MaKP select new { bn.MaBNhan, bn.TenBNhan, kphong.MaKP, bnkb.NgayKham }).ToList();
                
                _dm.Ngay1 = _tsbn.Where(p => p.NgayKham >= _tungay && p.NgayKham <=_denngay).Count();
                _dm.Ngay2 = _tsbn.Where(p => p.NgayKham >= _tungay.AddDays(1) && p.NgayKham <= _denngay.AddDays(1)).Count();
                _dm.Ngay3 = _tsbn.Where(p => p.NgayKham >= _tungay.AddDays(2) && p.NgayKham <= _denngay.AddDays(2)).Count();
                _dm.Ngay4 = _tsbn.Where(p => p.NgayKham >= _tungay.AddDays(3) && p.NgayKham <= _denngay.AddDays(3)).Count();
                _dm.Ngay5 = _tsbn.Where(p => p.NgayKham >= _tungay.AddDays(4) && p.NgayKham <= _denngay.AddDays(4)).Count();
                _dm.Ngay6 = _tsbn.Where(p => p.NgayKham >= _tungay.AddDays(5) && p.NgayKham <= _denngay.AddDays(5)).Count();
                _dm.Ngay7 = _tsbn.Where(p => p.NgayKham >= _tungay.AddDays(6) && p.NgayKham <= _denngay.AddDays(6)).Count();
                _dm.Ngay8 = _tsbn.Where(p => p.NgayKham >= _tungay.AddDays(7) && p.NgayKham <= _denngay.AddDays(7)).Count();
                _dm.Ngay9 = _tsbn.Where(p => p.NgayKham >= _tungay.AddDays(8) && p.NgayKham <= _denngay.AddDays(8)).Count();
                _dm.Ngay10 = _tsbn.Where(p => p.NgayKham >= _tungay.AddDays(9) && p.NgayKham <= _denngay.AddDays(9)).Count();
                _dm.Ngay11 = _tsbn.Where(p => p.NgayKham >= _tungay.AddDays(10) && p.NgayKham <= _denngay.AddDays(10)).Count();
                _dm.Ngay12 = _tsbn.Where(p => p.NgayKham >= _tungay.AddDays(11) && p.NgayKham <= _denngay.AddDays(11)).Count();
                _dm.Ngay13 = _tsbn.Where(p => p.NgayKham >= _tungay.AddDays(12) && p.NgayKham <= _denngay.AddDays(12)).Count();
                _dm.Ngay14 = _tsbn.Where(p => p.NgayKham >= _tungay.AddDays(13) && p.NgayKham <= _denngay.AddDays(13)).Count();
                _dm.Ngay15 = _tsbn.Where(p => p.NgayKham >= _tungay.AddDays(14) && p.NgayKham <= _denngay.AddDays(14)).Count();
                _listDm.Add(_dm);
            }

            DungChung.Ham.Print(DungChung.PrintConfig.Rp_DinhMucTheoNgay_30010, _listDm, _dic, false);
        }
    }
}