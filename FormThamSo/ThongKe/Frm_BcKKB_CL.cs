using System;using QLBV_Database;
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
    public partial class Frm_BcKKB_CL : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcKKB_CL()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private bool KTtaoBc()
        {
            if (lupTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupTuNgay.Focus();
                return false;
            }
            if (lupDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
                return false;
            }

            return true;
        }
        private class KPhong
        {
            private string TenKP;
            private int MaKP;
            private bool Chon;
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
            public bool chon
            { set { Chon = value; } get { return Chon; } }
        }


        List<KPhong> _Kphong = new List<KPhong>();
        private void Frm_BcKKB_CL_Load(object sender, EventArgs e)
        {
            _Kphong.Clear();
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
            var kphong = (from kp in data.KPhongs
                          where (kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám")
                          select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                KPhong themmoi1 = new KPhong();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = true;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhong themmoi = new KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP == null ? 0 : Convert.ToInt32(a.MaKP);
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }

        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;

            DungChung.Bien.c_chuyenkhoa.f_ChuyenKhoa();
            if (KTtaoBc())
            {
                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                DateTime tungaynew = DungChung.Ham.NgayTu(tungay.AddMonths(-1));
                DateTime denngaynew = DungChung.Ham.NgayDen(denngay.AddMonths(1));
                List<KPhong> khoa = new List<KPhong>();
                List<BNKB> _lbnkb = new List<BNKB>();
                _lbnkb = data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay).ToList();
                khoa = _Kphong.Where(p => p.makp > 0 && p.chon == true).ToList();
                #region bỏ
                //int _MaKP1 = 0;
                //int _MaKP2 = 0;
                //int _MaKP3 = 0;
                //int _MaKP4 = 0;
                //int _MaKP5 = 0;
                //int _MaKP6 = 0;
                //int _MaKP7 = 0;
                //int _MaKP8 = 0;
                //int _MaKP9 = 0;
                //int _MaKP10 = 0;
                //int _MaKP11 = 0;
                //int _MaKP12 = 0;
                //int _MaKP13 = 0;
                //int _MaKP14 = 0;
                //int _MaKP15 = 0;
                //int _MaKP16 = 0;
                //int _MaKP17 = 0;
                //int _MaKP18 = 0;
                //int _MaKP19 = 0;
                //int _MaKP20 = 0;
                //int _MaKP21 = 0;
                //int _MaKP22 = 0;
                //int _MaKP23 = 0;
                //int _MaKP24 = 0;
                //for (int i = 0; i < _Kphong.Count; i++)
                //{
                //    if (_Kphong.Skip(i).First().chon == true)
                //    {
                //        switch (i)
                //        {
                //            case 0:
                //                _MaKP1 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 1:
                //                _MaKP2 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 2:
                //                _MaKP3 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 3:
                //                _MaKP4 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 4:
                //                _MaKP5 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 5:
                //                _MaKP6 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 6:
                //                _MaKP7 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 7:
                //                _MaKP8 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 8:
                //                _MaKP9 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 9:
                //                _MaKP10 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 10:
                //                _MaKP11 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 11:
                //                _MaKP12 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 12:
                //                _MaKP13 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 13:
                //                _MaKP14 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 14:
                //                _MaKP15 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 15:
                //                _MaKP16 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 16:
                //                _MaKP17 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 17:
                //                _MaKP18 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 18:
                //                _MaKP19 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 19:
                //                _MaKP20 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 20:
                //                _MaKP21 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 21:
                //                _MaKP22 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 22:
                //                _MaKP23 = _Kphong.Skip(i).First().makp;
                //                break;
                //            case 23:
                //                _MaKP24 = _Kphong.Skip(i).First().makp;
                //                break;
                //        }
                //    }
                //}
                #endregion

                string _ck1 = ""; string _ck2 = ""; string _ck3 = ""; string _ck4 = ""; string _ck5 = ""; string _ck6 = ""; string _ck7 = ""; string _ck8 = ""; string _ck9 = ""; string _ck10 = ""; string _ck11 = "";
                var qck1 = (from kp in khoa
                            join bnkb in _lbnkb on kp.makp equals bnkb.MaKP
                            select bnkb.MaCK).ToList();
                var qck = (from b in qck1
                           join c in DungChung.Bien._lChuyenKhoa on b equals c.MaCK
                           group c by c.ChuyenKhoa into kq
                           select new { CK = kq.Key, }).OrderBy(p => p.CK).ToList();
                if (qck.Count > 0)
                {
                    if (qck.Count == 1)
                    { if (qck.Skip(0).First().CK != null || qck.Skip(0).First().CK != " ") { _ck1 = qck.Skip(0).First().CK; } else { _ck1 = " "; } }
                    if (qck.Count == 2)
                    {
                        if (qck.Skip(0).First().CK != null || qck.Skip(0).First().CK != " ") { _ck1 = qck.Skip(0).First().CK; } else { _ck1 = " "; }
                        if (qck.Skip(1).First().CK != null || qck.Skip(1).First().CK != " ") { _ck2 = qck.Skip(1).First().CK; } else { _ck2 = " "; }
                    }
                    if (qck.Count == 3)
                    {
                        if (qck.Skip(0).First().CK != null || qck.Skip(0).First().CK != " ") { _ck1 = qck.Skip(0).First().CK; } else { _ck1 = " "; }
                        if (qck.Skip(1).First().CK != null || qck.Skip(1).First().CK != " ") { _ck2 = qck.Skip(1).First().CK; } else { _ck2 = " "; }
                        if (qck.Skip(2).First().CK != null || qck.Skip(2).First().CK != " ") { _ck3 = qck.Skip(2).First().CK; } else { _ck3 = " "; }
                    }
                    if (qck.Count == 4)
                    {
                        if (qck.Skip(0).First().CK != null || qck.Skip(0).First().CK != " ") { _ck1 = qck.Skip(0).First().CK; } else { _ck1 = " "; }
                        if (qck.Skip(1).First().CK != null || qck.Skip(1).First().CK != " ") { _ck2 = qck.Skip(1).First().CK; } else { _ck2 = " "; }
                        if (qck.Skip(2).First().CK != null || qck.Skip(2).First().CK != " ") { _ck3 = qck.Skip(2).First().CK; } else { _ck3 = " "; }
                        if (qck.Skip(3).First().CK != null || qck.Skip(3).First().CK != " ") { _ck4 = qck.Skip(3).First().CK; } else { _ck4 = " "; }
                    }
                    if (qck.Count == 5)
                    {
                        if (qck.Skip(0).First().CK != null || qck.Skip(0).First().CK != " ") { _ck1 = qck.Skip(0).First().CK; } else { _ck1 = " "; }
                        if (qck.Skip(1).First().CK != null || qck.Skip(1).First().CK != " ") { _ck2 = qck.Skip(1).First().CK; } else { _ck2 = " "; }
                        if (qck.Skip(2).First().CK != null || qck.Skip(2).First().CK != " ") { _ck3 = qck.Skip(2).First().CK; } else { _ck3 = " "; }
                        if (qck.Skip(3).First().CK != null || qck.Skip(3).First().CK != " ") { _ck4 = qck.Skip(3).First().CK; } else { _ck4 = " "; }
                        if (qck.Skip(4).First().CK != null || qck.Skip(4).First().CK != " ") { _ck5 = qck.Skip(4).First().CK; } else { _ck5 = " "; }
                    }
                    if (qck.Count == 6)
                    {
                        if (qck.Skip(0).First().CK != null || qck.Skip(0).First().CK != " ") { _ck1 = qck.Skip(0).First().CK; } else { _ck1 = " "; }
                        if (qck.Skip(1).First().CK != null || qck.Skip(1).First().CK != " ") { _ck2 = qck.Skip(1).First().CK; } else { _ck2 = " "; }
                        if (qck.Skip(2).First().CK != null || qck.Skip(2).First().CK != " ") { _ck3 = qck.Skip(2).First().CK; } else { _ck3 = " "; }
                        if (qck.Skip(3).First().CK != null || qck.Skip(3).First().CK != " ") { _ck4 = qck.Skip(3).First().CK; } else { _ck4 = " "; }
                        if (qck.Skip(4).First().CK != null || qck.Skip(4).First().CK != " ") { _ck5 = qck.Skip(4).First().CK; } else { _ck5 = " "; }
                        if (qck.Skip(5).First().CK != null || qck.Skip(5).First().CK != " ") { _ck6 = qck.Skip(5).First().CK; } else { _ck6 = " "; }
                    }
                    if (qck.Count == 7)
                    {
                        if (qck.Skip(0).First().CK != null || qck.Skip(0).First().CK != " ") { _ck1 = qck.Skip(0).First().CK; } else { _ck1 = " "; }
                        if (qck.Skip(1).First().CK != null || qck.Skip(1).First().CK != " ") { _ck2 = qck.Skip(1).First().CK; } else { _ck2 = " "; }
                        if (qck.Skip(2).First().CK != null || qck.Skip(2).First().CK != " ") { _ck3 = qck.Skip(2).First().CK; } else { _ck3 = " "; }
                        if (qck.Skip(3).First().CK != null || qck.Skip(3).First().CK != " ") { _ck4 = qck.Skip(3).First().CK; } else { _ck4 = " "; }
                        if (qck.Skip(4).First().CK != null || qck.Skip(4).First().CK != " ") { _ck5 = qck.Skip(4).First().CK; } else { _ck5 = " "; }
                        if (qck.Skip(5).First().CK != null || qck.Skip(5).First().CK != " ") { _ck6 = qck.Skip(5).First().CK; } else { _ck6 = " "; }
                        if (qck.Skip(6).First().CK != null || qck.Skip(6).First().CK != " ") { _ck7 = qck.Skip(6).First().CK; } else { _ck7 = " "; }
                    }
                    if (qck.Count == 8)
                    {
                        if (qck.Skip(0).First().CK != null || qck.Skip(0).First().CK != " ") { _ck1 = qck.Skip(0).First().CK; } else { _ck1 = " "; }
                        if (qck.Skip(1).First().CK != null || qck.Skip(1).First().CK != " ") { _ck2 = qck.Skip(1).First().CK; } else { _ck2 = " "; }
                        if (qck.Skip(2).First().CK != null || qck.Skip(2).First().CK != " ") { _ck3 = qck.Skip(2).First().CK; } else { _ck3 = " "; }
                        if (qck.Skip(3).First().CK != null || qck.Skip(3).First().CK != " ") { _ck4 = qck.Skip(3).First().CK; } else { _ck4 = " "; }
                        if (qck.Skip(4).First().CK != null || qck.Skip(4).First().CK != " ") { _ck5 = qck.Skip(4).First().CK; } else { _ck5 = " "; }
                        if (qck.Skip(5).First().CK != null || qck.Skip(5).First().CK != " ") { _ck6 = qck.Skip(5).First().CK; } else { _ck6 = " "; }
                        if (qck.Skip(6).First().CK != null || qck.Skip(6).First().CK != " ") { _ck7 = qck.Skip(6).First().CK; } else { _ck7 = " "; }
                        if (qck.Skip(7).First().CK != null || qck.Skip(7).First().CK != " ") { _ck8 = qck.Skip(7).First().CK; } else { _ck8 = " "; }
                    }
                    if (qck.Count == 9)
                    {
                        if (qck.Skip(0).First().CK != null || qck.Skip(0).First().CK != " ") { _ck1 = qck.Skip(0).First().CK; } else { _ck1 = " "; }
                        if (qck.Skip(1).First().CK != null || qck.Skip(1).First().CK != " ") { _ck2 = qck.Skip(1).First().CK; } else { _ck2 = " "; }
                        if (qck.Skip(2).First().CK != null || qck.Skip(2).First().CK != " ") { _ck3 = qck.Skip(2).First().CK; } else { _ck3 = " "; }
                        if (qck.Skip(3).First().CK != null || qck.Skip(3).First().CK != " ") { _ck4 = qck.Skip(3).First().CK; } else { _ck4 = " "; }
                        if (qck.Skip(4).First().CK != null || qck.Skip(4).First().CK != " ") { _ck5 = qck.Skip(4).First().CK; } else { _ck5 = " "; }
                        if (qck.Skip(5).First().CK != null || qck.Skip(5).First().CK != " ") { _ck6 = qck.Skip(5).First().CK; } else { _ck6 = " "; }
                        if (qck.Skip(6).First().CK != null || qck.Skip(6).First().CK != " ") { _ck7 = qck.Skip(6).First().CK; } else { _ck7 = " "; }
                        if (qck.Skip(7).First().CK != null || qck.Skip(7).First().CK != " ") { _ck8 = qck.Skip(7).First().CK; } else { _ck8 = " "; }
                        if (qck.Skip(8).First().CK != null || qck.Skip(8).First().CK != " ") { _ck9 = qck.Skip(8).First().CK; } else { _ck9 = " "; }
                    }
                    if (qck.Count == 10)
                    {
                        if (qck.Skip(0).First().CK != null || qck.Skip(0).First().CK != " ") { _ck1 = qck.Skip(0).First().CK; } else { _ck1 = " "; }
                        if (qck.Skip(1).First().CK != null || qck.Skip(1).First().CK != " ") { _ck2 = qck.Skip(1).First().CK; } else { _ck2 = " "; }
                        if (qck.Skip(2).First().CK != null || qck.Skip(2).First().CK != " ") { _ck3 = qck.Skip(2).First().CK; } else { _ck3 = " "; }
                        if (qck.Skip(3).First().CK != null || qck.Skip(3).First().CK != " ") { _ck4 = qck.Skip(3).First().CK; } else { _ck4 = " "; }
                        if (qck.Skip(4).First().CK != null || qck.Skip(4).First().CK != " ") { _ck5 = qck.Skip(4).First().CK; } else { _ck5 = " "; }
                        if (qck.Skip(5).First().CK != null || qck.Skip(5).First().CK != " ") { _ck6 = qck.Skip(5).First().CK; } else { _ck6 = " "; }
                        if (qck.Skip(6).First().CK != null || qck.Skip(6).First().CK != " ") { _ck7 = qck.Skip(6).First().CK; } else { _ck7 = " "; }
                        if (qck.Skip(7).First().CK != null || qck.Skip(7).First().CK != " ") { _ck8 = qck.Skip(7).First().CK; } else { _ck8 = " "; }
                        if (qck.Skip(8).First().CK != null || qck.Skip(8).First().CK != " ") { _ck9 = qck.Skip(8).First().CK; } else { _ck9 = " "; }
                        if (qck.Skip(9).First().CK != null || qck.Skip(9).First().CK != " ") { _ck10 = qck.Skip(9).First().CK; } else { _ck10 = " "; }
                    }
                    if (qck.Count == 11)
                    {
                        if (qck.Skip(0).First().CK != null || qck.Skip(0).First().CK != " ") { _ck1 = qck.Skip(0).First().CK; } else { _ck1 = " "; }
                        if (qck.Skip(1).First().CK != null || qck.Skip(1).First().CK != " ") { _ck2 = qck.Skip(1).First().CK; } else { _ck2 = " "; }
                        if (qck.Skip(2).First().CK != null || qck.Skip(2).First().CK != " ") { _ck3 = qck.Skip(2).First().CK; } else { _ck3 = " "; }
                        if (qck.Skip(3).First().CK != null || qck.Skip(3).First().CK != " ") { _ck4 = qck.Skip(3).First().CK; } else { _ck4 = " "; }
                        if (qck.Skip(4).First().CK != null || qck.Skip(4).First().CK != " ") { _ck5 = qck.Skip(4).First().CK; } else { _ck5 = " "; }
                        if (qck.Skip(5).First().CK != null || qck.Skip(5).First().CK != " ") { _ck6 = qck.Skip(5).First().CK; } else { _ck6 = " "; }
                        if (qck.Skip(6).First().CK != null || qck.Skip(6).First().CK != " ") { _ck7 = qck.Skip(6).First().CK; } else { _ck7 = " "; }
                        if (qck.Skip(7).First().CK != null || qck.Skip(7).First().CK != " ") { _ck8 = qck.Skip(7).First().CK; } else { _ck8 = " "; }
                        if (qck.Skip(8).First().CK != null || qck.Skip(8).First().CK != " ") { _ck9 = qck.Skip(8).First().CK; } else { _ck9 = " "; }
                        if (qck.Skip(9).First().CK != null || qck.Skip(9).First().CK != " ") { _ck10 = qck.Skip(9).First().CK; } else { _ck10 = " "; }
                        if (qck.Skip(10).First().CK != null || qck.Skip(10).First().CK != " ") { _ck11 = qck.Skip(10).First().CK; } else { _ck11 = " "; }
                    }
                    if (qck.Count > 11)
                    {
                        if (qck.Skip(0).First().CK != null || qck.Skip(0).First().CK != " ") { _ck1 = qck.Skip(0).First().CK; } else { _ck1 = " "; }
                        if (qck.Skip(1).First().CK != null || qck.Skip(1).First().CK != " ") { _ck2 = qck.Skip(1).First().CK; } else { _ck2 = " "; }
                        if (qck.Skip(2).First().CK != null || qck.Skip(2).First().CK != " ") { _ck3 = qck.Skip(2).First().CK; } else { _ck3 = " "; }
                        if (qck.Skip(3).First().CK != null || qck.Skip(3).First().CK != " ") { _ck4 = qck.Skip(3).First().CK; } else { _ck4 = " "; }
                        if (qck.Skip(4).First().CK != null || qck.Skip(4).First().CK != " ") { _ck5 = qck.Skip(4).First().CK; } else { _ck5 = " "; }
                        if (qck.Skip(5).First().CK != null || qck.Skip(5).First().CK != " ") { _ck6 = qck.Skip(5).First().CK; } else { _ck6 = " "; }
                        if (qck.Skip(6).First().CK != null || qck.Skip(6).First().CK != " ") { _ck7 = qck.Skip(6).First().CK; } else { _ck7 = " "; }
                        if (qck.Skip(7).First().CK != null || qck.Skip(7).First().CK != " ") { _ck8 = qck.Skip(7).First().CK; } else { _ck8 = " "; }
                        if (qck.Skip(8).First().CK != null || qck.Skip(8).First().CK != " ") { _ck9 = qck.Skip(8).First().CK; } else { _ck9 = " "; }
                        if (qck.Skip(9).First().CK != null || qck.Skip(9).First().CK != " ") { _ck10 = qck.Skip(9).First().CK; } else { _ck10 = " "; }
                        if (qck.Skip(10).First().CK != null || qck.Skip(10).First().CK != " ") { _ck11 = qck.Skip(10).First().CK; } else { _ck11 = " "; }
                    }

                }
                var qbnkb = (from ck in DungChung.Bien._lChuyenKhoa
                             join kb in _lbnkb on ck.MaCK equals kb.MaCK
                             group kb by new { kb.MaBNhan, ck.ChuyenKhoa, kb.MaKP } into kq
                             select new { Key = kq.Key.MaBNhan, IDKB = kq.Max(p => p.IDKB), ChuyenKhoa = kq.Key.ChuyenKhoa, kq.Key.MaKP }).ToList();

                var id = (from kp in khoa
                          join kb in qbnkb on kp.makp equals kb.MaKP
                          group kb by new { kb.Key, kb.ChuyenKhoa } into kq
                          select new { kq.Key.Key, kq.Key.ChuyenKhoa, IDKB = kq.Max(p => p.IDKB) }).ToList();

                var qsl = (from k in id
                           join bnkb in _lbnkb on k.IDKB equals bnkb.IDKB
                           join bn in data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                           //join kp in
                           //    (from kp2 in data.KPhongs select new { kp2.MaKP }).Distinct() on bnkb.MaKP equals kp.MaKP
                           //   where (bnkb.MaKP == _MaKP1 || bnkb.MaKP == _MaKP2 || bnkb.MaKP == _MaKP3 || bnkb.MaKP == _MaKP4 || bnkb.MaKP == _MaKP5 || bnkb.MaKP == _MaKP6 || bnkb.MaKP == _MaKP7 || bnkb.MaKP == _MaKP8 || bnkb.MaKP == _MaKP9 || bnkb.MaKP == _MaKP10 || bnkb.MaKP == _MaKP11 || bnkb.MaKP == _MaKP12 || bnkb.MaKP == _MaKP13 || bnkb.MaKP == _MaKP14 || bnkb.MaKP == _MaKP15 || bnkb.MaKP == _MaKP16 || bnkb.MaKP == _MaKP17 || bnkb.MaKP == _MaKP18 || bnkb.MaKP == _MaKP19 || bnkb.MaKP == _MaKP20 || bnkb.MaKP == _MaKP21 || bnkb.MaKP == _MaKP22 || bnkb.MaKP == _MaKP23 || bnkb.MaKP == _MaKP24)
                           group new { bnkb, bn, k } by new { bnkb.NgayKham } into kq
                           select new
                           {
                               NTN = kq.Key.NgayKham,
                               Sl1 = kq.Select(p => p.bn.MaBNhan).Count(),// == 0 ? 0 : kq.Select(p => p.bn.MaBNhan).Count(),
                               Sl2 = kq.Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),// == 0 ? 0 : kq.Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),
                               Sl3 = kq.Where(p => p.bn.Tuoi <= 6).Select(p => p.bn.MaBNhan).Count(),
                               Sl4 = kq.Where(p => p.bn.SThe.Contains("CN")).Select(p => p.bn.MaBNhan).Count(),
                               Sl5 = kq.Where(p => p.bn.SThe.Contains("HC") || p.bn.SThe.Contains("CH")).Select(p => p.bn.MaBNhan).Count(),// == 0,// ? 0 : kq.Where(p => p.bn.Tuoi <= 6).Select(p => p.bn.MaBNhan).Count(),
                               Sl6 = kq.Where(p => p.bn.Tuoi <= 6).Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),// == 0 ? 0 : kq.Where(p => p.bn.Tuoi <= 6).Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),
                               Sl7 = kq.Where(p => p.bn.Tuoi <= 6).Where(p => p.bnkb.PhuongAn == 2).Select(p => p.bn.MaBNhan).Count(),// == 0 ? 0 : kq.Where(p => p.bn.Tuoi <= 6).Where(p => p.bnkb.PhuongAn == 2).Select(p => p.bn.MaBNhan).Count(),
                               Sl8 = kq.Where(p => p.bn.Tuoi <= 6).Where(p => p.bnkb.PhuongAn == 1).Select(p => p.bn.MaBNhan).Count(),// == 0 ? 0 : kq.Where(p => p.bn.Tuoi <= 6).Where(p => p.bnkb.PhuongAn == 1).Select(p => p.bn.MaBNhan).Count(),
                               Sl9 = kq.Where(p => p.bn.Tuoi < 1).Select(p => p.bn.MaBNhan).Count(),// == 0 ? 0 : kq.Where(p => p.bn.Tuoi < 1).Select(p => p.bn.MaBNhan).Count(),
                               Sl10 = kq.Where(p => p.bn.Tuoi < 1).Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),// == 0 ? 0 : kq.Where(p => p.bn.Tuoi < 1).Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),
                               Sl11 = kq.Where(p => p.bn.Tuoi >= 1).Where(p => p.bn.Tuoi <= 6).Select(p => p.bn.MaBNhan).Count(),// == 0 ? 0 : kq.Where(p => p.bn.Tuoi >= 1).Where(p => p.bn.Tuoi <= 6).Select(p => p.bn.MaBNhan).Count(),
                               Sl12 = kq.Where(p => p.bn.Tuoi >= 1).Where(p => p.bn.Tuoi <= 6).Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),// == 0 ? 0 : kq.Where(p => p.bn.Tuoi >= 1).Where(p => p.bn.Tuoi <= 6).Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),
                               Sl13 = kq.Where(p => p.bn.Tuoi > 60).Select(p => p.bn.MaBNhan).Count(),// == 0 ? 0 : kq.Where(p => p.bn.Tuoi > 60).Select(p => p.bn.MaBNhan).Count(),
                               Sl14 = kq.Where(p => p.bn.Tuoi > 60).Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),// == 0 ? 0 : kq.Where(p => p.bn.Tuoi > 60).Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),
                               Sl15 = kq.Where(p => p.k.ChuyenKhoa == _ck1).Select(p => p.bn.MaBNhan).Count(),// == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck1).Select(p => p.bn.MaBNhan).Count(),
                               Sl16 = kq.Where(p => p.k.ChuyenKhoa == _ck1).Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),// == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck1).Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),
                               Sl17 = kq.Where(p => p.k.ChuyenKhoa == _ck1).Where(p => p.bnkb.PhuongAn == 2).Select(p => p.bn.MaBNhan).Count(),// == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck1).Where(p => p.bnkb.PhuongAn == 2).Select(p => p.bn.MaBNhan).Count(),
                               Sl18 = kq.Where(p => p.k.ChuyenKhoa == _ck1).Where(p => p.bnkb.PhuongAn == 1).Select(p => p.bn.MaBNhan).Count(),// == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck1).Where(p => p.bnkb.PhuongAn == 1).Select(p => p.bn.MaBNhan).Count(),
                               Sl19 = kq.Where(p => p.k.ChuyenKhoa == _ck2).Select(p => p.bn.MaBNhan).Count(),// == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck2).Select(p => p.bn.MaBNhan).Count(),
                               Sl20 = kq.Where(p => p.k.ChuyenKhoa == _ck2).Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),//  == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck2).Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),
                               Sl21 = kq.Where(p => p.k.ChuyenKhoa == _ck2).Where(p => p.bnkb.PhuongAn == 2).Select(p => p.bn.MaBNhan).Count(),//  == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck2).Where(p => p.bnkb.PhuongAn == 2).Select(p => p.bn.MaBNhan).Count(),
                               Sl22 = kq.Where(p => p.k.ChuyenKhoa == _ck2).Where(p => p.bnkb.PhuongAn == 1).Select(p => p.bn.MaBNhan).Count(),//  == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck2).Where(p => p.bnkb.PhuongAn == 1).Select(p => p.bn.MaBNhan).Count(),
                               Sl23 = kq.Where(p => p.k.ChuyenKhoa == _ck3).Select(p => p.bn.MaBNhan).Count(),//  == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck3).Select(p => p.bn.MaBNhan).Count(),
                               Sl24 = kq.Where(p => p.k.ChuyenKhoa == _ck3).Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),//  == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck3).Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),
                               Sl25 = kq.Where(p => p.k.ChuyenKhoa == _ck3).Where(p => p.bnkb.PhuongAn == 2).Select(p => p.bn.MaBNhan).Count(),//  == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck3).Where(p => p.bnkb.PhuongAn == 2).Select(p => p.bn.MaBNhan).Count(),
                               Sl26 = kq.Where(p => p.k.ChuyenKhoa == _ck3).Where(p => p.bnkb.PhuongAn == 1).Select(p => p.bn.MaBNhan).Count(),//  == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck3).Where(p => p.bnkb.PhuongAn == 1).Select(p => p.bn.MaBNhan).Count(),
                               Sl27 = kq.Where(p => p.k.ChuyenKhoa == _ck4).Select(p => p.bn.MaBNhan).Count(),//  == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck4).Select(p => p.bn.MaBNhan).Count(),
                               Sl28 = kq.Where(p => p.k.ChuyenKhoa == _ck4).Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),//  == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck4).Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),
                               Sl29 = kq.Where(p => p.k.ChuyenKhoa == _ck4).Where(p => p.bnkb.PhuongAn == 2).Select(p => p.bn.MaBNhan).Count(),//  == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck4).Where(p => p.bnkb.PhuongAn == 2).Select(p => p.bn.MaBNhan).Count(),
                               Sl30 = kq.Where(p => p.k.ChuyenKhoa == _ck4).Where(p => p.bnkb.PhuongAn == 1).Select(p => p.bn.MaBNhan).Count(),//
                           }).ToList().Select(x => new
                        {
                            NTN = x.NTN.ToString().Substring(0, 10),
                            x.Sl1,
                            x.Sl2,
                            x.Sl3,
                            x.Sl4,
                            x.Sl5,
                            x.Sl6,
                            x.Sl7,
                            x.Sl8,
                            x.Sl9,
                            x.Sl10,
                            x.Sl11,
                            x.Sl12,
                            x.Sl13,
                            x.Sl14,
                            x.Sl15,
                            x.Sl16,
                            x.Sl17,
                            x.Sl18,
                            x.Sl19,
                            x.Sl20,
                            x.Sl21,
                            x.Sl22,
                            x.Sl23,
                            x.Sl24,
                            x.Sl25,
                            x.Sl26,
                            x.Sl27,
                            x.Sl28,
                            x.Sl29,
                            x.Sl30,
                        }).OrderBy(p => p.NTN.Substring(0, 2)).OrderBy(p => p.NTN.Substring(3, 2)).ToList();

                var qsl1 = (from q in qsl
                            group new { q } by new { q.NTN } into kq
                            select new
                            {
                                NTN = kq.Key.NTN,
                                SL1 = (kq.Sum(p => p.q.Sl1) == 0 ? null : kq.Sum(p => p.q.Sl1).ToString()),
                                SL2 = (kq.Sum(p => p.q.Sl2) == 0 ? null : kq.Sum(p => p.q.Sl2).ToString()),
                                SL3 = (kq.Sum(p => p.q.Sl3) == 0 ? null : kq.Sum(p => p.q.Sl3).ToString()),
                                SL4 = (kq.Sum(p => p.q.Sl4) == 0 ? null : kq.Sum(p => p.q.Sl4).ToString()),
                                SL5 = (kq.Sum(p => p.q.Sl5) == 0 ? null : kq.Sum(p => p.q.Sl5).ToString()),
                                SL6 = (kq.Sum(p => p.q.Sl6) == 0 ? null : kq.Sum(p => p.q.Sl6).ToString()),
                                SL7 = (kq.Sum(p => p.q.Sl7) == 0 ? null : kq.Sum(p => p.q.Sl7).ToString()),
                                SL8 = (kq.Sum(p => p.q.Sl8) == 0 ? null : kq.Sum(p => p.q.Sl8).ToString()),
                                SL9 = (kq.Sum(p => p.q.Sl9) == 0 ? null : kq.Sum(p => p.q.Sl9).ToString()),
                                SL10 = (kq.Sum(p => p.q.Sl10) == 0 ? null : kq.Sum(p => p.q.Sl10).ToString()),
                                SL11 = (kq.Sum(p => p.q.Sl11) == 0 ? null : kq.Sum(p => p.q.Sl11).ToString()),
                                SL12 = (kq.Sum(p => p.q.Sl12) == 0 ? null : kq.Sum(p => p.q.Sl12).ToString()),
                                SL13 = (kq.Sum(p => p.q.Sl13) == 0 ? null : kq.Sum(p => p.q.Sl13).ToString()),
                                SL14 = (kq.Sum(p => p.q.Sl14) == 0 ? null : kq.Sum(p => p.q.Sl14).ToString()),
                                SL15 = (kq.Sum(p => p.q.Sl15) == 0 ? null : kq.Sum(p => p.q.Sl15).ToString()),
                                SL16 = (kq.Sum(p => p.q.Sl16) == 0 ? null : kq.Sum(p => p.q.Sl16).ToString()),
                                SL17 = (kq.Sum(p => p.q.Sl17) == 0 ? null : kq.Sum(p => p.q.Sl17).ToString()),
                                SL18 = (kq.Sum(p => p.q.Sl18) == 0 ? null : kq.Sum(p => p.q.Sl18).ToString()),
                                SL19 = (kq.Sum(p => p.q.Sl19) == 0 ? null : kq.Sum(p => p.q.Sl19).ToString()),
                                SL20 = (kq.Sum(p => p.q.Sl20) == 0 ? null : kq.Sum(p => p.q.Sl20).ToString()),
                                SL21 = (kq.Sum(p => p.q.Sl21) == 0 ? null : kq.Sum(p => p.q.Sl21).ToString()),
                                SL22 = (kq.Sum(p => p.q.Sl22) == 0 ? null : kq.Sum(p => p.q.Sl22).ToString()),
                                SL23 = (kq.Sum(p => p.q.Sl23) == 0 ? null : kq.Sum(p => p.q.Sl23).ToString()),
                                SL24 = (kq.Sum(p => p.q.Sl24) == 0 ? null : kq.Sum(p => p.q.Sl24).ToString()),
                                SL25 = (kq.Sum(p => p.q.Sl25) == 0 ? null : kq.Sum(p => p.q.Sl25).ToString()),
                                SL26 = (kq.Sum(p => p.q.Sl26) == 0 ? null : kq.Sum(p => p.q.Sl26).ToString()),
                                SL27 = (kq.Sum(p => p.q.Sl27) == 0 ? null : kq.Sum(p => p.q.Sl27).ToString()),
                                SL28 = (kq.Sum(p => p.q.Sl28) == 0 ? null : kq.Sum(p => p.q.Sl28).ToString()),
                                SL29 = (kq.Sum(p => p.q.Sl29) == 0 ? null : kq.Sum(p => p.q.Sl29).ToString()),
                                SL30 = (kq.Sum(p => p.q.Sl30) == 0 ? null : kq.Sum(p => p.q.Sl30).ToString()),
                            }).OrderBy(p => p.NTN.Substring(0, 2)).OrderBy(p => p.NTN.Substring(3, 2)).ToList();

                #region phần tiếp theo
                var qsl01 = (from k in id
                             join bnkb in _lbnkb on k.IDKB equals bnkb.IDKB
                             join bn in data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                             //join kp in
                             //    (from kp2 in data.KPhongs select new { kp2.MaKP }).Distinct() on bnkb.MaKP equals kp.MaKP
                             //where (bnkb.MaKP == _MaKP1 || bnkb.MaKP == _MaKP2 || bnkb.MaKP == _MaKP3 || bnkb.MaKP == _MaKP4 || bnkb.MaKP == _MaKP5 || bnkb.MaKP == _MaKP6 || bnkb.MaKP == _MaKP7 || bnkb.MaKP == _MaKP8 || bnkb.MaKP == _MaKP9 || bnkb.MaKP == _MaKP10 || bnkb.MaKP == _MaKP11 || bnkb.MaKP == _MaKP12 || bnkb.MaKP == _MaKP13 || bnkb.MaKP == _MaKP14 || bnkb.MaKP == _MaKP15 || bnkb.MaKP == _MaKP16 || bnkb.MaKP == _MaKP17 || bnkb.MaKP == _MaKP18 || bnkb.MaKP == _MaKP19 || bnkb.MaKP == _MaKP20 || bnkb.MaKP == _MaKP21 || bnkb.MaKP == _MaKP22 || bnkb.MaKP == _MaKP23 || bnkb.MaKP == _MaKP24)
                             group new { bnkb, bn, k } by new { bnkb.NgayKham, bnkb.MaBNhan } into kq
                             select new
                             {
                                 NTN = kq.Key.NgayKham,
                                 Sl31 = kq.Where(p => p.k.ChuyenKhoa == _ck5).Select(p => p.bn.MaBNhan).Count(),//  == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck4).Select(p => p.bn.MaBNhan).Count(),
                                 Sl32 = kq.Where(p => p.k.ChuyenKhoa == _ck5).Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),//  == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck4).Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),
                                 Sl33 = kq.Where(p => p.k.ChuyenKhoa == _ck5).Where(p => p.bnkb.PhuongAn == 2).Select(p => p.bn.MaBNhan).Count(),//  == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck4).Where(p => p.bnkb.PhuongAn == 2).Select(p => p.bn.MaBNhan).Count(),
                                 Sl34 = kq.Where(p => p.k.ChuyenKhoa == _ck5).Where(p => p.bnkb.PhuongAn == 1).Select(p => p.bn.MaBNhan).Count(),//  
                                 Sl35 = kq.Where(p => p.k.ChuyenKhoa == _ck6).Select(p => p.bn.MaBNhan).Count(),//  == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck4).Select(p => p.bn.MaBNhan).Count(),
                                 Sl36 = kq.Where(p => p.k.ChuyenKhoa == _ck6).Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),//  == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck4).Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),
                                 Sl37 = kq.Where(p => p.k.ChuyenKhoa == _ck6).Where(p => p.bnkb.PhuongAn == 2).Select(p => p.bn.MaBNhan).Count(),//  == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck4).Where(p => p.bnkb.PhuongAn == 2).Select(p => p.bn.MaBNhan).Count(),
                                 Sl38 = kq.Where(p => p.k.ChuyenKhoa == _ck6).Where(p => p.bnkb.PhuongAn == 1).Select(p => p.bn.MaBNhan).Count(),//   }).ToLis 
                                 Sl39 = kq.Where(p => p.k.ChuyenKhoa == _ck7).Select(p => p.bn.MaBNhan).Count(),//  == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck4).Select(p => p.bn.MaBNhan).Count(),
                                 Sl40 = kq.Where(p => p.k.ChuyenKhoa == _ck7).Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),//  == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck4).Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),
                                 Sl41 = kq.Where(p => p.k.ChuyenKhoa == _ck7).Where(p => p.bnkb.PhuongAn == 2).Select(p => p.bn.MaBNhan).Count(),//  == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck4).Where(p => p.bnkb.PhuongAn == 2).Select(p => p.bn.MaBNhan).Count(),
                                 Sl42 = kq.Where(p => p.k.ChuyenKhoa == _ck7).Where(p => p.bnkb.PhuongAn == 1).Select(p => p.bn.MaBNhan).Count(),//   }).ToLis 
                                 Sl43 = kq.Where(p => p.k.ChuyenKhoa == _ck8).Select(p => p.bn.MaBNhan).Count(),//  == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck4).Select(p => p.bn.MaBNhan).Count(),
                                 Sl44 = kq.Where(p => p.k.ChuyenKhoa == _ck8).Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),//  == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck4).Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),
                                 Sl45 = kq.Where(p => p.k.ChuyenKhoa == _ck8).Where(p => p.bnkb.PhuongAn == 2).Select(p => p.bn.MaBNhan).Count(),//  == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck4).Where(p => p.bnkb.PhuongAn == 2).Select(p => p.bn.MaBNhan).Count(),
                                 Sl46 = kq.Where(p => p.k.ChuyenKhoa == _ck8).Where(p => p.bnkb.PhuongAn == 1).Select(p => p.bn.MaBNhan).Count(),//   }).ToLis 
                                 Sl47 = kq.Where(p => p.k.ChuyenKhoa == _ck9).Select(p => p.bn.MaBNhan).Count(),//  == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck4).Select(p => p.bn.MaBNhan).Count(),
                                 Sl48 = kq.Where(p => p.k.ChuyenKhoa == _ck9).Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),//  == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck4).Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),
                                 Sl49 = kq.Where(p => p.k.ChuyenKhoa == _ck9).Where(p => p.bnkb.PhuongAn == 2).Select(p => p.bn.MaBNhan).Count(),//  == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck4).Where(p => p.bnkb.PhuongAn == 2).Select(p => p.bn.MaBNhan).Count(),
                                 Sl50 = kq.Where(p => p.k.ChuyenKhoa == _ck9).Where(p => p.bnkb.PhuongAn == 1).Select(p => p.bn.MaBNhan).Count(),//   }).ToLis 
                                 Sl51 = kq.Where(p => p.k.ChuyenKhoa == _ck10).Select(p => p.bn.MaBNhan).Count(),//  == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck4).Select(p => p.bn.MaBNhan).Count(),
                                 Sl52 = kq.Where(p => p.k.ChuyenKhoa == _ck10).Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),//  == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck4).Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),
                                 Sl53 = kq.Where(p => p.k.ChuyenKhoa == _ck10).Where(p => p.bnkb.PhuongAn == 2).Select(p => p.bn.MaBNhan).Count(),//  == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck4).Where(p => p.bnkb.PhuongAn == 2).Select(p => p.bn.MaBNhan).Count(),
                                 Sl54 = kq.Where(p => p.k.ChuyenKhoa == _ck10).Where(p => p.bnkb.PhuongAn == 1).Select(p => p.bn.MaBNhan).Count(),//   }).ToLis 
                                 Sl55 = kq.Where(p => p.k.ChuyenKhoa == _ck11).Select(p => p.bn.MaBNhan).Count(),//  == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck4).Select(p => p.bn.MaBNhan).Count(),
                                 Sl56 = kq.Where(p => p.k.ChuyenKhoa == _ck11).Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),//  == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck4).Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Count(),
                                 Sl57 = kq.Where(p => p.k.ChuyenKhoa == _ck11).Where(p => p.bnkb.PhuongAn == 2).Select(p => p.bn.MaBNhan).Count(),//  == 0 ? 0 : kq.Where(p => p.bnkb.ChuyenKhoa == _ck4).Where(p => p.bnkb.PhuongAn == 2).Select(p => p.bn.MaBNhan).Count(),
                                 Sl58 = kq.Where(p => p.k.ChuyenKhoa == _ck11).Where(p => p.bnkb.PhuongAn == 1).Select(p => p.bn.MaBNhan).Count(),//   }).ToLis 
                             }).ToList().Select(x => new
                             {
                                 NTN = x.NTN.ToString().Substring(0, 10),
                                 x.Sl31,
                                 x.Sl32,
                                 x.Sl33,
                                 x.Sl34,
                                 x.Sl35,
                                 x.Sl36,
                                 x.Sl37,
                                 x.Sl38,
                                 x.Sl39,
                                 x.Sl40,
                                 x.Sl41,
                                 x.Sl42,
                                 x.Sl43,
                                 x.Sl44,
                                 x.Sl45,
                                 x.Sl46,
                                 x.Sl47,
                                 x.Sl48,
                                 x.Sl49,
                                 x.Sl50,
                                 x.Sl51,
                                 x.Sl52,
                                 x.Sl53,
                                 x.Sl54,
                                 x.Sl55,
                                 x.Sl56,
                                 x.Sl57,
                                 x.Sl58
                             }).OrderBy(p => p.NTN.Substring(0, 2)).OrderBy(p => p.NTN.Substring(3, 2)).ToList();

                var qsl12 = (from q in qsl01
                             group new { q } by new { q.NTN } into kq
                             select new
                             {
                                 NTN = kq.Key.NTN,
                                 SL31 = (kq.Sum(p => p.q.Sl31) == 0 ? null : kq.Sum(p => p.q.Sl31).ToString()),
                                 SL32 = (kq.Sum(p => p.q.Sl32) == 0 ? null : kq.Sum(p => p.q.Sl32).ToString()),
                                 SL33 = (kq.Sum(p => p.q.Sl33) == 0 ? null : kq.Sum(p => p.q.Sl33).ToString()),
                                 SL34 = (kq.Sum(p => p.q.Sl34) == 0 ? null : kq.Sum(p => p.q.Sl34).ToString()),
                                 SL35 = (kq.Sum(p => p.q.Sl35) == 0 ? null : kq.Sum(p => p.q.Sl35).ToString()),
                                 SL36 = (kq.Sum(p => p.q.Sl36) == 0 ? null : kq.Sum(p => p.q.Sl36).ToString()),
                                 SL37 = (kq.Sum(p => p.q.Sl37) == 0 ? null : kq.Sum(p => p.q.Sl37).ToString()),
                                 SL38 = (kq.Sum(p => p.q.Sl38) == 0 ? null : kq.Sum(p => p.q.Sl38).ToString()),
                                 SL39 = (kq.Sum(p => p.q.Sl39) == 0 ? null : kq.Sum(p => p.q.Sl39).ToString()),
                                 SL40 = (kq.Sum(p => p.q.Sl40) == 0 ? null : kq.Sum(p => p.q.Sl40).ToString()),
                                 SL41 = (kq.Sum(p => p.q.Sl41) == 0 ? null : kq.Sum(p => p.q.Sl41).ToString()),
                                 SL42 = (kq.Sum(p => p.q.Sl42) == 0 ? null : kq.Sum(p => p.q.Sl42).ToString()),
                                 SL43 = (kq.Sum(p => p.q.Sl43) == 0 ? null : kq.Sum(p => p.q.Sl43).ToString()),
                                 SL44 = (kq.Sum(p => p.q.Sl44) == 0 ? null : kq.Sum(p => p.q.Sl44).ToString()),
                                 SL45 = (kq.Sum(p => p.q.Sl45) == 0 ? null : kq.Sum(p => p.q.Sl45).ToString()),
                                 SL46 = (kq.Sum(p => p.q.Sl46) == 0 ? null : kq.Sum(p => p.q.Sl46).ToString()),
                                 SL47 = (kq.Sum(p => p.q.Sl47) == 0 ? null : kq.Sum(p => p.q.Sl47).ToString()),
                                 SL48 = (kq.Sum(p => p.q.Sl48) == 0 ? null : kq.Sum(p => p.q.Sl48).ToString()),
                                 SL49 = (kq.Sum(p => p.q.Sl49) == 0 ? null : kq.Sum(p => p.q.Sl49).ToString()),
                                 SL50 = (kq.Sum(p => p.q.Sl50) == 0 ? null : kq.Sum(p => p.q.Sl50).ToString()),
                                 SL51 = (kq.Sum(p => p.q.Sl51) == 0 ? null : kq.Sum(p => p.q.Sl51).ToString()),
                                 SL52 = (kq.Sum(p => p.q.Sl52) == 0 ? null : kq.Sum(p => p.q.Sl52).ToString()),
                                 SL53 = (kq.Sum(p => p.q.Sl53) == 0 ? null : kq.Sum(p => p.q.Sl53).ToString()),
                                 SL54 = (kq.Sum(p => p.q.Sl54) == 0 ? null : kq.Sum(p => p.q.Sl54).ToString()),
                                 SL55 = (kq.Sum(p => p.q.Sl55) == 0 ? null : kq.Sum(p => p.q.Sl55).ToString()),
                                 SL56 = (kq.Sum(p => p.q.Sl56) == 0 ? null : kq.Sum(p => p.q.Sl56).ToString()),
                                 SL57 = (kq.Sum(p => p.q.Sl57) == 0 ? null : kq.Sum(p => p.q.Sl57).ToString()),
                                 SL58 = (kq.Sum(p => p.q.Sl58) == 0 ? null : kq.Sum(p => p.q.Sl58).ToString()),
                             }).OrderBy(p => p.NTN.Substring(0, 2)).OrderBy(p => p.NTN.Substring(3, 2)).ToList();
                var qdtnt = (from kp in khoa
                             join bn in data.BenhNhans.Where(p => p.NoiTru == 0).Where(p => p.NNhap >= tungaynew && p.NNhap <= denngaynew) on kp.makp equals bn.MaKP
                             join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                             join vp in data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                             group new { bn, vp } by new { bn.MaBNhan, vp.NgayTT } into kq
                             select new { kq.Key.MaBNhan, kq.Key.NgayTT }).ToList();
                var _ldichvu = (from dv in data.DichVus
                                join nhom in data.NhomDVs.Where(p => p.TenNhomCT.ToLower().Contains("thủ thuật, phẫu thuật")) on dv.IDNhom equals nhom.IDNhom
                                select new { dv.MaDV }).Distinct().ToList();
                var _lBenhNhan = (from bn in data.BenhNhans.Where(p => p.NNhap >= tungaynew && p.NNhap <= denngaynew)
                                  join vp in data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                                  join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                                  select new { bn.MaBNhan, bn.MaKP, vp.NgayTT, vpct.idVPhict,vpct.MaDV }).ToList();
                var qtt = (from kp in khoa
                           join bn in _lBenhNhan on kp.makp equals bn.MaBNhan
                           join dv in _ldichvu on bn.MaDV equals dv.MaDV
                           //join bn in data.BenhNhans.Where(p => p.NNhap >= tungaynew && p.NNhap <= denngaynew) on kp.makp equals bn.MaKP
                           //join vp in data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                           //join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                           //join dv in data.DichVus on vpct.MaDV equals dv.MaDV
                           //join nhom in data.NhomDVs.Where(p => p.TenNhomCT.ToLower().Contains("thủ thuật, phẫu thuật")) on dv.IDNhom equals nhom.IDNhom
                           group new { bn } by new { bn.idVPhict, bn.NgayTT } into kq
                           select new { kq.Key.idVPhict, kq.Key.NgayTT }).ToList();
                List<KKb_CLb> kkb = new List<KKb_CLb>();
                KKb_CLb moi = new KKb_CLb();
                foreach (var item in qsl12)
                {
                    moi = new KKb_CLb();
                    moi.NTN = Convert.ToDateTime(item.NTN);
                    moi.SL31 = item.SL31;
                    moi.SL32 = item.SL32;
                    moi.SL33 = item.SL33;
                    moi.SL34 = item.SL34;
                    moi.SL35 = item.SL35;
                    moi.SL36 = item.SL36;
                    moi.SL37 = item.SL37;
                    moi.SL38 = item.SL38;
                    moi.SL39 = item.SL39;
                    moi.SL40 = item.SL40;
                    moi.SL41 = item.SL41;
                    moi.SL42 = item.SL42;
                    moi.SL43 = item.SL43;
                    moi.SL44 = item.SL44;
                    moi.SL45 = item.SL45;
                    moi.SL46 = item.SL46;
                    moi.SL47 = item.SL47;
                    moi.SL48 = item.SL48;
                    moi.SL49 = item.SL49;
                    moi.SL50 = item.SL50;
                    moi.SL51 = item.SL51;
                    moi.SL52 = item.SL52;
                    moi.SL53 = item.SL53;
                    moi.SL54 = item.SL54;
                    moi.SL55 = item.SL55;
                    moi.SL56 = item.SL56;
                    moi.SL57 = item.SL57;
                    moi.SL58 = item.SL58;
                    DateTime ngayt = DungChung.Ham.NgayTu(Convert.ToDateTime(item.NTN));
                    DateTime ngayd = DungChung.Ham.NgayDen(Convert.ToDateTime(item.NTN));
                    int dtnt = qdtnt.Where(p => p.NgayTT >= ngayt && p.NgayTT <= ngayd).Select(p => p.MaBNhan).Distinct().Count();
                    int tt = qtt.Select(p => p.idVPhict).Distinct().Count();
                    moi.SL59 = dtnt == 0 ? "" : dtnt.ToString();
                    moi.SL60 = tt == 0 ? "" : tt.ToString();
                    kkb.Add(moi);
                }
                #endregion

                frmIn frm = new frmIn();
                BaoCao.Rep_BcKKB_CL rep = new BaoCao.Rep_BcKKB_CL();
                rep.TuNgayDenNgay.Value = "Từ ngày " + tungay.ToString().Substring(0, 10) + " đến ngày " + denngay.ToString().Substring(0, 10);
                rep.TenBC.Value = ("báo cáo khoa khám bệnh").ToUpper();
                rep.CK1.Value = _ck1; rep.CK2.Value = _ck2; rep.CK3.Value = _ck3; rep.CK4.Value = _ck4;
                if (_Kphong.First().chon == true)
                { rep.Phong.Value = "Khoa khám bệnh"; }
                else
                {
                    var qkp = (from k in khoa
                               join kp in data.KPhongs on k.makp equals kp.MaKP
                               select new { kp.TenKP }).Distinct().OrderBy(p => p.TenKP).ToList();
                    if (qkp.Count > 0)
                    {
                        int i = qkp.Count();
                        if (i == 0) { rep.Phong.Value = "Khoa khám bệnh "; }
                        if (i == 1) { rep.Phong.Value = qkp.First().TenKP; }
                        if (i == 2) { rep.Phong.Value = qkp.First().TenKP + ", " + qkp.Skip(1).First().TenKP; }
                        if (i == 3) { rep.Phong.Value = qkp.First().TenKP + ", " + qkp.Skip(1).First().TenKP + ", " + qkp.Skip(2).First().TenKP; }
                        if (i == 4) { rep.Phong.Value = qkp.First().TenKP + ", " + qkp.Skip(1).First().TenKP + ", " + qkp.Skip(2).First().TenKP + ", " + qkp.Skip(3).First().TenKP; }
                        if (i == 5) { rep.Phong.Value = qkp.First().TenKP + ", " + qkp.Skip(1).First().TenKP + ", " + qkp.Skip(2).First().TenKP + ", " + qkp.Skip(3).First().TenKP + ", " + qkp.Skip(4).First().TenKP; }
                        if (i == 6) { rep.Phong.Value = qkp.First().TenKP + ", " + qkp.Skip(1).First().TenKP + ", " + qkp.Skip(2).First().TenKP + ", " + qkp.Skip(3).First().TenKP + ", " + qkp.Skip(4).First().TenKP + ", " + qkp.Skip(5).First().TenKP; }
                        if (i == 7) { rep.Phong.Value = qkp.First().TenKP + ", " + qkp.Skip(1).First().TenKP + ", " + qkp.Skip(2).First().TenKP + ", " + qkp.Skip(3).First().TenKP + ", " + qkp.Skip(4).First().TenKP + ", " + qkp.Skip(5).First().TenKP + ", " + qkp.Skip(6).First().TenKP; }
                        if (i == 8) { rep.Phong.Value = qkp.First().TenKP + ", " + qkp.Skip(1).First().TenKP + ", " + qkp.Skip(2).First().TenKP + ", " + qkp.Skip(3).First().TenKP + ", " + qkp.Skip(4).First().TenKP + ", " + qkp.Skip(5).First().TenKP + ", " + qkp.Skip(6).First().TenKP + ", " + qkp.Skip(7).First().TenKP; }
                        if (i > 8) { rep.Phong.Value = "Khoa khám bệnh "; }
                    }
                }
                rep.BindingData();
                rep.DataSource = qsl1.ToList();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
                //}
                //{
                BaoCao.Rep_BcKKB_CLb rep1 = new BaoCao.Rep_BcKKB_CLb();
                rep1.CK1.Value = _ck5; rep1.CK2.Value = _ck6; rep1.CK3.Value = _ck7; rep1.CK4.Value = _ck8; rep1.CK5.Value = _ck9; rep1.CK6.Value = _ck10; rep1.CK7.Value = _ck11;
                //rep1.K1.Value = _MaKP1;
                //rep1.K2.Value = _MaKP2;
                //rep1.K3.Value = _MaKP3;
                //rep1.K4.Value = _MaKP4;
                //rep1.K5.Value = _MaKP5;
                //rep1.K6.Value = _MaKP6;
                //rep1.K7.Value = _MaKP7;
                //rep1.K8.Value = _MaKP8;
                //rep1.K9.Value = _MaKP9;
                //rep1.K10.Value = _MaKP10;
                //rep1.K11.Value = _MaKP11;
                //rep1.K12.Value = _MaKP12;
                //rep1.K13.Value = _MaKP13;
                //rep1.K14.Value = _MaKP14;
                //rep1.K15.Value = _MaKP15;
                //rep1.K16.Value = _MaKP16;
                //rep1.K17.Value = _MaKP17;
                //rep1.K18.Value = _MaKP18;
                //rep1.K19.Value = _MaKP19;
                //rep1.K20.Value = _MaKP20;
                //rep1.K21.Value = _MaKP21;
                //rep1.K22.Value = _MaKP22;
                //rep1.K23.Value = _MaKP23;
                //rep1.K24.Value = _MaKP24;
                rep1.NT.Value = tungay;
                rep1.ND.Value = denngay;
                rep1.BindingData();
                rep1.DataSource = kkb.ToList();
                rep1.CreateDocument();
                frm.prcIN.PrintingSystem = rep1.PrintingSystem;
                frm.ShowDialog();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chọn")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("tenkp") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("tenkp").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_Kphong.First().chon == true)
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = true;
                            }
                        }
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _Kphong.ToList();
                    }
                }
            }
        }
        #region class KKB_CLb
        private class KKb_CLb
        {
            public DateTime? NTN { get; set; }
            public string SL31 { get; set; }
            public string SL32 { get; set; }
            public string SL33 { get; set; }
            public string SL34 { get; set; }
            public string SL35 { get; set; }
            public string SL36 { get; set; }
            public string SL37 { get; set; }
            public string SL38 { get; set; }
            public string SL39 { get; set; }
            public string SL40 { get; set; }
            public string SL41 { get; set; }
            public string SL42 { get; set; }
            public string SL43 { get; set; }
            public string SL44 { get; set; }
            public string SL45 { get; set; }
            public string SL46 { get; set; }
            public string SL47 { get; set; }
            public string SL48 { get; set; }
            public string SL49 { get; set; }
            public string SL50 { get; set; }
            public string SL51 { get; set; }
            public string SL52 { get; set; }
            public string SL53 { get; set; }
            public string SL54 { get; set; }
            public string SL55 { get; set; }
            public string SL56 { get; set; }
            public string SL57 { get; set; }
            public string SL58 { get; set; }
            public string SL59 { get; set; }
            public string SL60 { get; set; }
        }
        #endregion
    }
}