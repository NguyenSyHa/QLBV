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
    public partial class Frm_BKThuocDV : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BKThuocDV()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
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
        private class BC
        {
            private string tenbn;
            private double ngaydt;
            private double xn;
            private double sa;
            private double xq;
            private double dt;
            private double ns;
            private double thuoc;
            private double tiengiuong;
            private double vtyt;
            private double ttpt;
            private double tg;
            private double tt;
            public string TenBN
            { set { tenbn = value; }
                get { return tenbn; }
            }
            public double Ngaydt
            { set { ngaydt = value; }
                get { return ngaydt; }
            }
            public double XN
            { set { xn = value; }
                get { return xn; }
            }
            public double SA
            {
                set { sa = value; }
                get { return sa; }
            }
            public double XQ
            {
                set { xq = value; }
                get { return xq; }
            }
            public double DT
            {
                set { dt = value; }
                get { return dt; }
            }
            public double NS
            { set { ns = value; }
                get { return ns; }
            }
            public double Thuoc
            { set { thuoc = value; }
                get { return thuoc; }
            }
            
            public double VTTH
            { set { vtyt = value; }
                get { return vtyt; }
            }
            public double TTPT
            { set { ttpt = value; }
                get { return ttpt; }
            }
            public double Congkham
            { set { tg = value; }
                get { return tg; }
            }
            public double TienGiuong
            {
                set { tiengiuong = value; }
                get { return tiengiuong; }
            }
            public double TT
            {
                set { tt = value; }
                get { return tt; }
            }
        }
        List<BC> _BC = new List<BC>();

        private void Frm_BKThuocDV_Load(object sender, EventArgs e)
        {
            _Kphong.Clear();
            radNT.SelectedIndex = 1;
            lupNgaytu.DateTime = System.DateTime.Now;
            lupNgayden.DateTime = System.DateTime.Now;
            var kphong = (from kp in _Data.KPhongs
                          where (kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám")
                          select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                foreach (var a in kphong)
                {
                    KPhong themmoi = new KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                _Kphong.Add(new KPhong { tenkp = " Tất cả", makp = 0, chon = true });
                grcKhoaphong.DataSource = _Kphong.ToList().OrderBy(p=>p.tenkp);
            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
       
            DateTime _Ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime _Ngayden = DungChung.Ham.NgayDen(lupNgayden.DateTime);
            int _MaKP1 = 0;
            int _MaKP2 = 0;
            int _MaKP3 = 0;
            int _MaKP4 = 0;
            int _MaKP5 = 0;
            int _MaKP6 = 0;
            int _MaKP7 = 0;
            int _MaKP8 = 0;
            int _MaKP9 = 0;
            int _MaKP10 = 0;
            int _MaKP11 = 0;
            int _MaKP12 = 0;
            int _MaKP13 = 0;
            int _MaKP14 = 0;
            int _MaKP15 = 0;
            for (int i = 0; i < _Kphong.Count; i++)
            {
                if (_Kphong.Skip(i).First().chon == true)
                {
                    switch (i)
                    {
                        case 0:
                            _MaKP1 = _Kphong.Skip(i).First().makp;
                            break;
                        case 1:
                            _MaKP2 = _Kphong.Skip(i).First().makp;
                            break;
                        case 2:
                            _MaKP3 = _Kphong.Skip(i).First().makp;
                            break;
                        case 3:
                            _MaKP4 = _Kphong.Skip(i).First().makp;
                            break;
                        case 4:
                            _MaKP5 = _Kphong.Skip(i).First().makp;
                            break;
                        case 5:
                            _MaKP6 = _Kphong.Skip(i).First().makp;
                            break;
                        case 6:
                            _MaKP7 = _Kphong.Skip(i).First().makp;
                            break;
                        case 7:
                            _MaKP8 = _Kphong.Skip(i).First().makp;
                            break;
                        case 8:
                            _MaKP9 = _Kphong.Skip(i).First().makp;
                            break;
                        case 9:
                            _MaKP10 = _Kphong.Skip(i).First().makp;
                            break;
                        case 10:
                            _MaKP11 = _Kphong.Skip(i).First().makp;
                            break;
                        case 11:
                            _MaKP12 = _Kphong.Skip(i).First().makp;
                            break;
                        case 12:
                            _MaKP13 = _Kphong.Skip(i).First().makp;
                            break;
                        case 13:
                            _MaKP14 = _Kphong.Skip(i).First().makp;
                            break;
                        case 14:
                            _MaKP15 = _Kphong.Skip(i).First().makp;
                            break;
                    }
                }
            }
            int dem=0;
            int _makp = 0;
            string tenkhoa = "";
            for (int i = 0; i < _Kphong.Count; i++)
            {
                if (_Kphong.Skip(i).First().chon == true)
                { dem++;
                _makp = _Kphong.Skip(i).First().makp;
                }
            }
            if (dem == 1) {
                var ten = _Data.KPhongs.Where(p => p.MaKP == _makp).ToList();
                if(ten.Count>0)
                    tenkhoa = ten.First().TenKP;
            }
            string _dtuong = "BHYT";
            int trongbh1 = 1,trongbh2=0;
            if (radDoiTuong.SelectedIndex == 1)
            {
                _dtuong = "Dịch vụ";

            }
            else {
                trongbh2 = 1;
            }
            var q2 = (from bn in _Data.BenhNhans.Where(p => p.DTuong == _dtuong).Where(p => p.NoiTru == radNT.SelectedIndex)
                     join vp in _Data.VienPhis.Where(P => P.NgayTT >= _Ngaytu).Where(p => p.NgayTT <= _Ngayden) on bn.MaBNhan equals vp.MaBNhan
                     join vpct in _Data.VienPhicts.Where(p => p.TrongBH == trongbh1 || p.TrongBH==trongbh2) on vp.idVPhi equals vpct.idVPhi
                     join dv in _Data.DichVus on vpct.MaDV equals dv.MaDV
                     join TN in _Data.TieuNhomDVs on dv.IdTieuNhom equals TN.IdTieuNhom
                     join nhom in _Data.NhomDVs on TN.IDNhom equals nhom.IDNhom
                     join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                     where (rv.MaKP == _MaKP1 || rv.MaKP == _MaKP10 || rv.MaKP == _MaKP11 || rv.MaKP == _MaKP12 || rv.MaKP == _MaKP13 || rv.MaKP == _MaKP14 || rv.MaKP == _MaKP15 || rv.MaKP == _MaKP2 || rv.MaKP == _MaKP3 || rv.MaKP == _MaKP4 || rv.MaKP == _MaKP5 || rv.MaKP == _MaKP6 || rv.MaKP == _MaKP7 || rv.MaKP == _MaKP8 || rv.MaKP == _MaKP9)
                 
                     select new
                     {
                        bn.MaBNhan,
                         bn.TenBNhan,
                   TenRG=TN.TenRG,
                   TenNhomCT=nhom.TenNhomCT,
                   ThanhTien=vpct.ThanhTien,
                         rv.SoNgaydt,
                         TT = 0,
                     }).OrderBy(p => p.MaBNhan).ToList();
            var q =( from a in q2 group new {a } by new {  a.MaBNhan, a.TenBNhan,a.SoNgaydt } into kq
                     select new
                     {
                         kq.Key.MaBNhan,
                         Mabn = kq.Key.MaBNhan,
                         TenBN = kq.Key.TenBNhan,
                         Thuoc = kq.Where(p => p.a.TenNhomCT ==  "Thuốc trong danh mục BHYT" ).Sum(p => p.a.ThanhTien),
                         Congkham = kq.Where(p => DungChung.Bien.MaBV == "20001" ? (( p.a.TenNhomCT == "Khám bệnh")) : (p.a.TenNhomCT == "Giường điều trị nội trú" || p.a.TenNhomCT == "Khám bệnh")).Sum(p => p.a.ThanhTien),
                         TienGiuong = kq.Where(p=> DungChung.Bien.MaBV == "20001" ?((p.a.TenNhomCT == "Giường điều trị ngoại trú" || p.a.TenNhomCT == "Giường điều trị nội trú")) : p.a.TenNhomCT == "ko lam gi").Sum(p => p.a.ThanhTien),
                         XN = kq.Where(p => p.a.TenNhomCT == "Xét nghiệm").Sum(p => p.a.ThanhTien),
                         TTPT = kq.Where(p => p.a.TenNhomCT == "Thủ thuật, phẫu thuật").Sum(p => p.a.ThanhTien),
                         VTTH = kq.Where(p => p.a.TenNhomCT =="Vật tư y tế trong danh mục BHYT").Sum(p => p.a.ThanhTien),
                         XQ=kq.Where(p=>p.a.TenRG=="X-Quang").Sum(p=>p.a.ThanhTien),
                         SA = kq.Where(p => p.a.TenRG == "Siêu âm").Sum(p => p.a.ThanhTien),
                         DT = kq.Where(p => p.a.TenRG == "Điện tim").Sum(p => p.a.ThanhTien),
                         NS = kq.Where(p => p.a.TenRG == "Nội soi").Sum(p => p.a.ThanhTien),
                         Ngaydt = kq.Key.SoNgaydt,
                         TT=kq.Sum(p => p.a.ThanhTien),
                     }).OrderBy(p => p.MaBNhan).ToList().Select(x=> new{
                         MaBNhan=x.MaBNhan,
                         x.Mabn,
                         x.TenBN,
                         Ngaydt=x.Ngaydt,
                         Thuoc=x.Thuoc,
                         Congkham =  x.Congkham,
                         TienGiuong = x.TienGiuong,
                         XN = x.XN,
                         TTPT =  x.TTPT,
                         VTTH = x.VTTH,
                         XQ = x.XQ,
                         SA =  x.SA,
                         DT =  x.DT,
                         NS =  x.NS,
                         TT=x.TT,
                     }).ToList().OrderBy(p=>p.MaBNhan);
            //foreach (var b in q)
            //{
            //    BC themmoi = new BC();
            //    if (b.Congkham != null)
            //    {
            //        themmoi.Congkham = b.Congkham.Value;
            //    }
            //    else
            //    { themmoi.Congkham = 0; }
            //    if (b.DT != null)
            //    {
            //        themmoi.DT = b.DT.Value;
            //    }
            //    else
            //    { themmoi.DT = 0; }
            //    if (b.NS != null)
            //    {
            //        themmoi.NS = b.NS.Value;
            //    }
            //    else
            //    { themmoi.NS = 0; }
            //    if (b.Ngaydt != null)
            //    {
            //        themmoi.Ngaydt = b.Ngaydt.Value;
            //    }
            //    else
            //    { themmoi.Ngaydt = 0; }
            //    if (b.SA != null)
            //    {
            //        themmoi.SA = b.SA.Value;
            //    }
            //    else
            //    { themmoi.SA = 0; }

            //    themmoi.TenBN = b.TenBN;
            //    if (b.TTPT != null)
            //    {
            //        themmoi.TTPT = b.TTPT.Value;
            //    }
            //    else
            //    { themmoi.TTPT = 0; }
            //    if (b.Thuoc != null)
            //    {
            //        themmoi.Thuoc = b.Thuoc.Value;
            //    }
            //    else
            //    { themmoi.Thuoc = 0; }
            //    if (b.VTTH != null)
            //    {
            //        themmoi.VTTH = b.VTTH.Value;
            //    }
            //    else
            //    { themmoi.VTTH = 0; }
            //    if (b.XN != null)
            //    {
            //        themmoi.XN = b.XN.Value;
            //    }
            //    else
            //    { themmoi.XN = 0; }
            //    if (b.XQ != null)
            //    {
            //        themmoi.XQ = b.XQ.Value;
            //    }
            //    else
            //    { themmoi.XQ = 0; }
            //    themmoi.TT = themmoi.Congkham + themmoi.DT + themmoi.NS + themmoi.SA + themmoi.TTPT + themmoi.Thuoc + themmoi.VTTH + themmoi.XN + themmoi.XQ;
            //    _BC.Add(themmoi);
            //}
            frmIn frm = new frmIn();
            BaoCao.Rep_BKThuocDV rep = new BaoCao.Rep_BKThuocDV();
            string noingoaitru = "NỘI TRÚ";
            if(radNT.SelectedIndex==0)
                noingoaitru = "NGOẠI TRÚ";
            string tieude = "BẢNG KÊ CHI PHÍ KHÁM CHỮA BỆNH " + noingoaitru;
            if (_dtuong == "Dịch vụ")
                rep.txtTieuDe.Text =tieude+ " DỊCH VỤ";
            else
                rep.txtTieuDe.Text = tieude + " BẢO HIỂM Y TẾ";
            rep.Khoa.Value = tenkhoa;
            rep.ngaythang.Value = " Từ ngày " + lupNgaytu.Text.Substring(0, 10) + " đến ngày " + lupNgayden.Text.Substring(0, 10);
            rep.DataSource = q;
            rep.BindingData();
            rep.CreateDocument();
            //rep.DataMember = "Table";
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
            _BC.Clear();

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChon")
            {

                if (e.RowHandle == 0)
                {
                    if (grvKhoaphong.GetFocusedRowCellValue(colChon) != null)
                    {
                        if (grvKhoaphong.GetRowCellValue(0, colChon).ToString() == "False")
                        {
                            for (int i = 0; i < grvKhoaphong.RowCount; i++)
                            {
                                grvKhoaphong.SetRowCellValue(i, "chon", true);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < grvKhoaphong.RowCount; i++)
                            {
                                grvKhoaphong.SetRowCellValue(i, "chon", false);
                            }
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < grvKhoaphong.RowCount; i++)
                    {
                        if (grvKhoaphong.GetRowCellValue(i, colChon) != null && grvKhoaphong.GetRowCellValue(i, colChon).ToString() == "True")
                        {
                            grvKhoaphong.SetRowCellValue(0, colChon, false);
                            break;
                        }
                        else
                        {

                        }
                    }

                }

            }
        }
    }
}