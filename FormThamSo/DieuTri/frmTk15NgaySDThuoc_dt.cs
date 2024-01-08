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
    public partial class frmTk15NgaySDThuoc_dt : DevExpress.XtraEditors.XtraForm
    {
        public frmTk15NgaySDThuoc_dt()
        {
            InitializeComponent();
        }
        string[] _songay;
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private bool KTtaoBc()
        {
            if (dateTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                dateTuNgay.Focus();
                return false;
            }
            if (dateDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                dateDenNgay.Focus();
                return false;
            }
            if ((dateDenNgay.DateTime - dateTuNgay.DateTime).Days >15 || (dateDenNgay.DateTime - dateTuNgay.DateTime).Days <= 0) {
                MessageBox.Show("Khoảng cách giữa 2 ngày phải >0 và <=16 ngày");
                dateDenNgay.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(lupKhoa.Text)) {
                MessageBox.Show("Bạn chưa chọn khoa");
                lupKhoa.Focus();
                return false;
            }
            
           return true;
        }
        private class NGAY
        {
            private string ngay1;

            public string Ngay1
            {
                get { return ngay1; }
                set { ngay1 = value; }
            }
            private string ngay2;

            public string Ngay2
            {
                get { return ngay2; }
                set { ngay2 = value; }
            }
            private string ngay3;

            public string Ngay3
            {
                get { return ngay3; }
                set { ngay3 = value; }
            }
            private string ngay4;

            public string Ngay4
            {
                get { return ngay4; }
                set { ngay4 = value; }
            }
            private string ngay5;

            public string Ngay5
            {
                get { return ngay5; }
                set { ngay5 = value; }
            }
            private string ngay6;

            public string Ngay6
            {
                get { return ngay6; }
                set { ngay6 = value; }
            }
            private string ngay7;

            public string Ngay7
            {
                get { return ngay7; }
                set { ngay7 = value; }
            }
            private string ngay8;

            public string Ngay8
            {
                get { return ngay8; }
                set { ngay8 = value; }
            }
            private string ngay9;

            public string Ngay9
            {
                get { return ngay9; }
                set { ngay9 = value; }
            }
            private string ngay10;

            public string Ngay10
            {
                get { return ngay10; }
                set { ngay10 = value; }
            }
            private string ngay11;

            public string Ngay11
            {
                get { return ngay11; }
                set { ngay11 = value; }
            }
            private string ngay12;

            public string Ngay12
            {
                get { return ngay12; }
                set { ngay12 = value; }
            }
            private string ngay13;

            public string Ngay13
            {
                get { return ngay13; }
                set { ngay13 = value; }
            }
            private string ngay14;

            public string Ngay14
            {
                get { return ngay14; }
                set { ngay14 = value; }
            }
            private string ngay15;

            public string Ngay15
            {
                get { return ngay15; }
                set { ngay15 = value; }

            }
            private string ngay16;

            public string Ngay16
            {
                get { return ngay16; }
                set { ngay16 = value; }
            }
            
        }
        private class NGAY1
        {
            private DateTime ngay1;

            public DateTime Ngay1
            {
                get { return ngay1; }
                set { ngay1 = value; }
            }
            private DateTime ngay2;

            public DateTime Ngay2
            {
                get { return ngay2; }
                set { ngay2 = value; }
            }
            private DateTime ngay3;

            public DateTime Ngay3
            {
                get { return ngay3; }
                set { ngay3 = value; }
            }
            private DateTime ngay4;

            public DateTime Ngay4
            {
                get { return ngay4; }
                set { ngay4 = value; }
            }
            private DateTime ngay5;

            public DateTime Ngay5
            {
                get { return ngay5; }
                set { ngay5 = value; }
            }
            private DateTime ngay6;

            public DateTime Ngay6
            {
                get { return ngay6; }
                set { ngay6 = value; }
            }
            private DateTime ngay7;

            public DateTime Ngay7
            {
                get { return ngay7; }
                set { ngay7 = value; }
            }
            private DateTime ngay8;

            public DateTime Ngay8
            {
                get { return ngay8; }
                set { ngay8 = value; }
            }
            private DateTime ngay9;

            public DateTime Ngay9
            {
                get { return ngay9; }
                set { ngay9 = value; }
            }
            private DateTime ngay10;

            public DateTime Ngay10
            {
                get { return ngay10; }
                set { ngay10 = value; }
            }
            private DateTime ngay11;

            public DateTime Ngay11
            {
                get { return ngay11; }
                set { ngay11 = value; }
            }
            private DateTime ngay12;

            public DateTime Ngay12
            {
                get { return ngay12; }
                set { ngay12 = value; }
            }
            private DateTime ngay13;

            public DateTime Ngay13
            {
                get { return ngay13; }
                set { ngay13 = value; }
            }
            private DateTime ngay14;

            public DateTime Ngay14
            {
                get { return ngay14; }
                set { ngay14 = value; }
            }
            private DateTime ngay15;

            public DateTime Ngay15
            {
                get { return ngay15; }
                set { ngay15 = value; }

            }
            private DateTime ngay16;

            public DateTime Ngay16
            {
                get { return ngay16; }
                set { ngay16 = value; }
            }

        }
        private class DV
        {
            private string ngay;

            public string Ngay
            {
                get { return ngay; }
                set { ngay = value; }
            }
            private int madv;

            public int MaDV
            {
                get { return madv; }
                set { madv = value; }
            }
            private string tendv;

            public string TenDV
            {
                get { return tendv; }
                set { tendv = value; }
            }
            private string donvi;

            public string DonVi
            {
                get { return donvi; }
                set { donvi = value; }
            }
            private string quycach;

            public string Quycach
            {
                get { return quycach; }
                set { quycach = value; }
            }
            private string sl1;

            public string Sl1
            {
                get { return sl1; }
                set { sl1 = value; }
            }
            private string sl2;

            public string Sl2
            {
                get { return sl2; }
                set { sl2 = value; }
            }
            private string sl3;

            public string Sl3
            {
                get { return sl3; }
                set { sl3 = value; }
            }
            private string sl4;

            public string Sl4
            {
                get { return sl4; }
                set { sl4 = value; }
            }
            private string sl5;

            public string Sl5
            {
                get { return sl5; }
                set { sl5 = value; }
            }
            private string sl6;

            public string Sl6
            {
                get { return sl6; }
                set { sl6 = value; }
            }
            private string sl7;

            public string Sl7
            {
                get { return sl7; }
                set { sl7 = value; }
            }
            private string sl8;

            public string Sl8
            {
                get { return sl8; }
                set { sl8 = value; }
            }
            private string sl9;

            public string Sl9
            {
                get { return sl9; }
                set { sl9 = value; }
            }
            private string sl10;

            public string Sl10
            {
                get { return sl10; }
                set { sl10 = value; }
            }
            private string sl11;

            public string Sl11
            {
                get { return sl11; }
                set { sl11 = value; }
            }
            private string sl12;

            public string Sl12
            {
                get { return sl12; }
                set { sl12 = value; }
            }
            private string sl13;

            public string Sl13
            {
                get { return sl13; }
                set { sl13 = value; }
            }
            private string sl14;

            public string Sl14
            {
                get { return sl14; }
                set { sl14 = value; }
            }
            private string sl15;

            public string Sl15
             {
                get { return sl15; }
                set { sl15 = value; }
            }
            private string sl16;

            public string Sl16
            {
                get { return sl16; }
                set { sl16 = value; }
            }
            private string slt;

            public string Slt
            {
                get { return slt; }
                set { slt = value; }
            }
         }
        List<NGAY> _lngay=new List<NGAY>();
        List<NGAY1> _lngay1 = new List<NGAY1>();
   //     List<DV> _ldv1 = new List<DV>();
        List<DV> _ldv = new List<DV>();

        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;


            if (KTtaoBc())
            {
                _ldv.Clear();
                _lngay.Clear();
                _lngay1.Clear();
               
                tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);
                int ngay = (denngay - tungay).Days+1;//kiểm tra ngày >=1 và <=16
                DateTime n1 = tungay.AddDays(2);
                frmIn frm = new frmIn();
                {
                   NGAY them = new NGAY();
                   them.Ngay1 = tungay.ToString().Substring(0,10);
                   them.Ngay2 = tungay.AddDays(1).ToString().Substring(0, 10);
                   them.Ngay3 = tungay.AddDays(2).ToString().Substring(0, 10);
                   them.Ngay4 = tungay.AddDays(3).ToString().Substring(0, 10);
                   them.Ngay5 = tungay.AddDays(4).ToString().Substring(0, 10);
                   them.Ngay6 = tungay.AddDays(5).ToString().Substring(0, 10);
                   them.Ngay7 = tungay.AddDays(6).ToString().Substring(0, 10);
                   them.Ngay8 = tungay.AddDays(7).ToString().Substring(0, 10);
                   them.Ngay9 = tungay.AddDays(8).ToString().Substring(0, 10);
                   them.Ngay10 = tungay.AddDays(9).ToString().Substring(0, 10);
                   them.Ngay11 = tungay.AddDays(10).ToString().Substring(0, 10);
                   them.Ngay12 = tungay.AddDays(11).ToString().Substring(0, 10);
                   them.Ngay13 = tungay.AddDays(12).ToString().Substring(0, 10);
                   them.Ngay14 = tungay.AddDays(13).ToString().Substring(0, 10);
                   them.Ngay15 = tungay.AddDays(14).ToString().Substring(0, 10);
                   them.Ngay16 = tungay.AddDays(15).ToString().Substring(0, 10);
                   _lngay.Add(them);
                   NGAY1 them1 = new NGAY1();
                   them1.Ngay1 = tungay;
                   them1.Ngay2 = tungay.AddDays(1);
                   them1.Ngay3 = tungay.AddDays(2);
                   them1.Ngay4 = tungay.AddDays(3);
                   them1.Ngay5 = tungay.AddDays(4);
                   them1.Ngay6 = tungay.AddDays(5);
                   them1.Ngay7 = tungay.AddDays(6);
                   them1.Ngay8 = tungay.AddDays(7);
                   them1.Ngay9 = tungay.AddDays(8);
                   them1.Ngay10 = tungay.AddDays(9);
                   them1.Ngay11 = tungay.AddDays(10);
                   them1.Ngay12 = tungay.AddDays(11);
                   them1.Ngay13 = tungay.AddDays(12);
                   them1.Ngay14 = tungay.AddDays(13);
                   them1.Ngay15 = tungay.AddDays(14);
                   them1.Ngay16 = tungay.AddDays(15);
                   _lngay1.Add(them1);

                }
                 BaoCao.repTk15NgaySDThuoc_dt rep = new BaoCao.repTk15NgaySDThuoc_dt();
    
                rep.TuNgay.Value ="Từ ngày "+ dateTuNgay.Text +" đến ngày "+ dateDenNgay.Text;
                if (_lngay1.First().Ngay1 != null && _lngay1.First().Ngay1 >= tungay && _lngay1.First().Ngay1 <= denngay)
                { rep.Ngay1.Value = _lngay.First().Ngay1.ToString().Substring(0, 2); }
                else { rep.Ngay1.Value = ""; }
                if (_lngay1.First().Ngay2 != null && _lngay1.First().Ngay2 >= tungay && _lngay1.First().Ngay2 <= denngay)
                { rep.Ngay2.Value = _lngay.First().Ngay2.ToString().Substring(0, 2); }
                else { rep.Ngay2.Value = ""; }
                if (_lngay1.First().Ngay3 != null && _lngay1.First().Ngay3 >= tungay && _lngay1.First().Ngay3 <= denngay)
                { rep.Ngay3.Value = rep.Ngay3.Value = _lngay.First().Ngay3.ToString().Substring(0, 2); }
                else { rep.Ngay3.Value = ""; }
                if (_lngay1.First().Ngay4 != null && _lngay1.First().Ngay4 >= tungay && _lngay1.First().Ngay4 <= denngay)
                { rep.Ngay4.Value = _lngay.First().Ngay4.ToString().Substring(0, 2); }
                else
                { rep.Ngay4.Value = ""; }
                if (_lngay1.First().Ngay5 != null && _lngay1.First().Ngay4 >= tungay && _lngay1.First().Ngay5 <= denngay)
                    rep.Ngay5.Value = _lngay.First().Ngay5.ToString().Substring(0, 2);
                else
                { rep.Ngay5.Value = ""; }
                if (_lngay1.First().Ngay6 != null && _lngay1.First().Ngay5 >= tungay && _lngay1.First().Ngay6 <= denngay)
                    rep.Ngay6.Value = _lngay.First().Ngay6.ToString().Substring(0, 2);
                else
                { rep.Ngay6.Value = ""; }
                if (_lngay1.First().Ngay7 != null && _lngay1.First().Ngay6 >= tungay && _lngay1.First().Ngay7 <= denngay)
                    rep.Ngay7.Value = _lngay.First().Ngay7.ToString().Substring(0, 2);
                else
                { rep.Ngay7.Value = ""; }
                if (_lngay1.First().Ngay8 != null && _lngay1.First().Ngay7 >= tungay && _lngay1.First().Ngay8 <= denngay)
                    rep.Ngay8.Value = _lngay.First().Ngay8.ToString().Substring(0, 2);
                else
                { rep.Ngay8.Value = ""; }
                if (_lngay1.First().Ngay9 != null && _lngay1.First().Ngay8 >= tungay && _lngay1.First().Ngay9 <= denngay)
                    rep.Ngay9.Value = _lngay.First().Ngay9.ToString().Substring(0, 2);
                else
                { rep.Ngay9.Value = ""; }
                if (_lngay1.First().Ngay10 != null && _lngay1.First().Ngay9 >= tungay && _lngay1.First().Ngay10 <= denngay)
                    rep.Ngay10.Value = _lngay.First().Ngay10.ToString().Substring(0, 2);
                else
                { rep.Ngay10.Value = ""; }
                if (_lngay1.First().Ngay11 != null && _lngay1.First().Ngay10 >= tungay && _lngay1.First().Ngay11 <= denngay)
                    rep.Ngay11.Value = _lngay.First().Ngay11.ToString().Substring(0, 2);
                else
                { rep.Ngay11.Value = ""; }
                if (_lngay1.First().Ngay12 != null && _lngay1.First().Ngay11 >= tungay && _lngay1.First().Ngay12 <= denngay)
                    rep.Ngay12.Value = _lngay.First().Ngay12.ToString().Substring(0, 2);
                else
                { rep.Ngay12.Value = ""; }
                if (_lngay1.First().Ngay13 != null && _lngay1.First().Ngay12 >= tungay && _lngay1.First().Ngay13 <= denngay)
                    rep.Ngay13.Value = _lngay.First().Ngay13.ToString().Substring(0, 2);
                else
                { rep.Ngay13.Value = ""; }
                if (_lngay1.First().Ngay14 != null && _lngay1.First().Ngay13 >= tungay && _lngay1.First().Ngay14 <= denngay)
                    rep.Ngay14.Value = _lngay.First().Ngay14.ToString().Substring(0, 2);
                else
                { rep.Ngay14.Value = ""; }
                if (_lngay1.First().Ngay15 != null && _lngay1.First().Ngay14 >= tungay && _lngay1.First().Ngay15 <= denngay)
                    rep.Ngay15.Value = _lngay.First().Ngay15.ToString().Substring(0, 2);
                else
                { rep.Ngay15.Value = ""; }
                if (_lngay1.First().Ngay16 != null && _lngay1.First().Ngay15 >= tungay && _lngay1.First().Ngay16 <= denngay)
                    rep.Ngay16.Value = _lngay.First().Ngay16.ToString().Substring(0, 2);
                else
                { rep.Ngay16.Value = ""; }
           
                int _makho = 0;
                if (lupKho.EditValue != null )
                    _makho = Convert.ToInt32( lupKho.EditValue);

                int _makhoa = 0;
                if (lupKhoa.EditValue != null)
                    _makhoa =Convert.ToInt32( lupKhoa.EditValue);
     
                var qtenkhoa = data.KPhongs.Where(p => p.MaKP == _makhoa).Select(p => new { p.TenKP }).ToList();
      
                if (qtenkhoa.Count > 0)
                {
                    rep.Khoa.Value  = "Khoa: " + qtenkhoa.First().TenKP;
                }
                int _st = -10;
                int _st1 = -10;
                if (radIn.SelectedIndex == 0) { _st = 1; } else { _st = 0; _st1 = 1; }
               if (_makho<=0)
               {
                   var q = (from dt in data.DThuocs.Where(p => p.NgayKe >= tungay && p.NgayKe <= denngay).Where(p => p.MaKP == _makhoa)
                            join dtct in data.DThuoccts.Where(p => p.Status != -1).Where(p => p.Status != 2 && (p.Status == _st || p.Status == _st1)) on dt.IDDon equals dtct.IDDon
                            join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                            group new { dv, dt, dtct } by new { dt.NgayKe, dv.MaDV, dv.TenDV, dtct.DonVi,dv.QCPC } into kq
                            select new
                            {
                                NgayKe = kq.Key.NgayKe,
                                MaDV = kq.Key.MaDV,
                                TenDV = kq.Key.TenDV,
                                DonVi = kq.Key.DonVi,
                                QC=kq.Key.QCPC,
                                SoLuong = kq.Sum(p => p.dtct.SoLuong),
                            }).ToList().Select(p => new
                                                    {
                                                        NgayKe = p.NgayKe.ToString().Substring(0, 10),
                                                        p.MaDV,
                                                        p.TenDV,
                                                        p.DonVi,
                                                        p.QC,
                                                        p.SoLuong
                                                    }).ToList();
                   var qdv = (from dv in q
                              group new { dv } by new {dv.MaDV, dv.TenDV, dv.DonVi,dv.QC } into kq
                              select new
                              {
                                  //kq.Key.NgayKe,
                                  kq.Key.MaDV,
                                  kq.Key.TenDV,
                                  kq.Key.DonVi,
                                  kq.Key.QC,
                                  SoLuong = kq.Sum(p => p.dv.SoLuong)
                              }).ToList();
                   if (qdv.Count > 0)
                   {
                       foreach (var a in qdv)
                       {
                           DV them = new DV();
                          // them.Ngay = a.NgayKe.ToString().Substring(0, 10);
                           them.MaDV = a.MaDV;
                           them.TenDV = a.TenDV;
                           them.DonVi = a.DonVi;
                           them.Quycach = a.QC;
                          // them.Sl1 = Convert.ToInt32(a.SoLuong);
                           _ldv.Add(them);


                       }
                   }
                     foreach (var a in _lngay)
                   {
                       foreach (var b in _ldv)
                       {
                           b.Sl1 = q.Where(p => p.NgayKe == a.Ngay1).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay1).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Sl2 = q.Where(p => p.NgayKe == a.Ngay2).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay2).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Sl3 = q.Where(p => p.NgayKe == a.Ngay3).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay3).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Sl4 = q.Where(p => p.NgayKe == a.Ngay4).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay4).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Sl5 = q.Where(p => p.NgayKe == a.Ngay5).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay5).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Sl6 = q.Where(p => p.NgayKe == a.Ngay6).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay6).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Sl7 = q.Where(p => p.NgayKe == a.Ngay7).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay7).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Sl8 = q.Where(p => p.NgayKe == a.Ngay8).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay8).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Sl9 = q.Where(p => p.NgayKe == a.Ngay9).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay9).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Sl10 = q.Where(p => p.NgayKe == a.Ngay10).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay10).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Sl11 = q.Where(p => p.NgayKe == a.Ngay11).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay11).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Sl12 = q.Where(p => p.NgayKe == a.Ngay12).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay12).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Sl13 = q.Where(p => p.NgayKe == a.Ngay13).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay13).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Sl14 = q.Where(p => p.NgayKe == a.Ngay14).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay14).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Sl15 = q.Where(p => p.NgayKe == a.Ngay15).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay15).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Sl16 = q.Where(p => p.NgayKe == a.Ngay16).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay16).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Slt = q.Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : qdv.Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                       }
                   }

               }
               else
               {
                   var q = (from dt in data.DThuocs.Where(p => p.NgayKe >= tungay && p.NgayKe <= denngay).Where(p => p.MaKXuat == _makho).Where(p => p.MaKP == _makhoa)
                            join dtct in data.DThuoccts.Where(p => p.Status != -1).Where(p => p.Status != 2 && (p.Status == _st || p.Status == _st1)) on dt.IDDon equals dtct.IDDon
                            join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                            group new { dv, dt, dtct } by new { dt.NgayKe, dv.MaDV, dv.TenDV, dtct.DonVi,dv.QCPC } into kq
                            select new
                            {
                                NgayKe = kq.Key.NgayKe,
                                MaDV = kq.Key.MaDV,
                                TenDV = kq.Key.TenDV,
                                DonVi = kq.Key.DonVi,
                                QC=kq.Key.QCPC,
                                SoLuong = kq.Sum(p => p.dtct.SoLuong),
                            }).ToList().Select(p => new
                            {
                                NgayKe = p.NgayKe.ToString().Substring(0, 10),
                                p.MaDV,
                                p.TenDV,
                                p.DonVi,
                                p.QC,
                                p.SoLuong
                            }).ToList();

                   var qdv = (from dv in q
                              group new { dv } by new { dv.MaDV, dv.TenDV, dv.DonVi,dv.QC } into kq
                              select new
                              {
                                  kq.Key.MaDV,
                                  kq.Key.TenDV,
                                  kq.Key.DonVi,
                                  kq.Key.QC,
                                  SoLuong = kq.Sum(p => p.dv.SoLuong)
                              }).ToList();
                   if (qdv.Count > 0)
                   {
                       foreach (var a in qdv)
                       {
                           DV them = new DV();
                          // them.Ngay = a.NgayKe.ToString().Substring(0, 10);
                           them.MaDV = a.MaDV;
                           them.TenDV = a.TenDV;
                           them.DonVi = a.DonVi;
                           them.Quycach = a.QC;
                          // them.Sl1 = Convert.ToInt32(a.SoLuong);
                           _ldv.Add(them);


                       }
                   }
                //   int ss = 0;
                   foreach (var a in _lngay)
                   {
                       foreach (var b in _ldv)
                       {
                           b.Sl1 = q.Where(p => p.NgayKe == a.Ngay1).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay1).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Sl2 = q.Where(p => p.NgayKe == a.Ngay2).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay2).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Sl3 = q.Where(p => p.NgayKe == a.Ngay3).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay3).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Sl4 = q.Where(p => p.NgayKe == a.Ngay4).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay4).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Sl5 = q.Where(p => p.NgayKe == a.Ngay5).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay5).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Sl6 = q.Where(p => p.NgayKe == a.Ngay6).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay6).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Sl7 = q.Where(p => p.NgayKe == a.Ngay7).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay7).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Sl8 = q.Where(p => p.NgayKe == a.Ngay8).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay8).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Sl9 = q.Where(p => p.NgayKe == a.Ngay9).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay9).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Sl10 = q.Where(p => p.NgayKe == a.Ngay10).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay10).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Sl11 = q.Where(p => p.NgayKe == a.Ngay11).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay11).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Sl12 = q.Where(p => p.NgayKe == a.Ngay12).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay12).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Sl13 = q.Where(p => p.NgayKe == a.Ngay13).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay13).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Sl14 = q.Where(p => p.NgayKe == a.Ngay14).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay14).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Sl15 = q.Where(p => p.NgayKe == a.Ngay15).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay15).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Sl16 = q.Where(p => p.NgayKe == a.Ngay16).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : q.Where(p => p.NgayKe == a.Ngay16).Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
                           b.Slt = q.Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong) == 0 ? null : qdv.Where(p => p.MaDV == b.MaDV).Sum(p => p.SoLuong).ToString();
               
                       }
                   }
               }
                    rep.DataSource = _ldv.OrderBy(p=>p.TenDV).ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
        
           }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTk15NgaySDThuoc_dt_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var q = from TK in data.KPhongs select new { TK.TenKP, TK.MaKP,TK.PLoai };
            lupKhoa.Properties.DataSource = q.Where(p => p.PLoai.Contains("lâm sàng")).ToList();
            lupKho.Properties.DataSource = q.Where(p=>p.PLoai.Contains("dược")).ToList();
            dateDenNgay.DateTime = System.DateTime.Now;
            dateTuNgay.DateTime = System.DateTime.Now;
        }

        private void dateDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            //DateTime tungay = System.DateTime.Now.Date;
            //DateTime denngay = System.DateTime.Now.Date;
            //tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
            //denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);
            //int ng = (denngay - tungay).Days;
            //if (ng > 16)
            //{
            //    MessageBox.Show("Báo cáo chỉ thống kê trong 16 ngày, số ngày bạn nhập > 16. Hãy kiểm tra lại.");
            //    lupKhoa.Focus();
            //}
        }

      
    }
}