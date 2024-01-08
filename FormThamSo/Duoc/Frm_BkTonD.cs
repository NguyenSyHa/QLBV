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
    public partial class Frm_BkTonD : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BkTonD()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<PPXD> _PPXD = new List<PPXD>();
        List<KP> _KP = new List<KP>();
        //List<PLDV> _PLDV = new List<PLDV>();
        private void Frm_BkTonD_Load(object sender, EventArgs e)
        {
            dateTuNgay.DateTime = System.DateTime.Now;
            dateDenNgay.DateTime = System.DateTime.Now;
            var kd = (from khoa in data.KPhongs.Where(p => p.PLoai == "Khoa dược" || p.PLoai == "Xã phường") select new { khoa.TenKP, khoa.MaKP, khoa.PLoai }).ToList();
            if (kd.Count() > 0)
            {
                PPXD themmoi1 = new PPXD();
                themmoi1.tenppxd = "Chọn tất cả";
                themmoi1.mappxd = 0;
                themmoi1.PL1 = "1";
                themmoi1.chon = false;
                _PPXD.Add(themmoi1);
                foreach (var a in kd)
                {
                    PPXD themmoi = new PPXD();
                    themmoi.tenppxd =a.TenKP;
                    themmoi.mappxd = a.MaKP;
                    themmoi.PL1 = a.PLoai;
                    themmoi.chon = false;
                    _PPXD.Add(themmoi);
                }
            }

            grcPPXuat.DataSource = "";
            grcPPXuat.DataSource = _PPXD.ToList();
            //addDataPPXuat();
            var pldv =(from nhom in  data.NhomDVs.Where(p=>p.Status==1) 
                       select new 
                        {
                            IDNhom = nhom.IDNhom,
                            TenNhomCT = nhom.TenNhomCT,
                            nhom.TenNhom,
                        })
                        .OrderBy(n=>n.TenNhom).ToList();
             NhomDV moi2 = new NhomDV();
                moi2.TenNhom = " Tất cả";
                moi2.IDNhom = 100;
                moi2.TenNhomCT =  "Tất cả";
                _lnhom.Add(moi2);
            _lnhom.Add(new NhomDV { IDNhom = 0, TenNhomCT = "Tất cả", TenNhom = "Tất cả" });
            //_lnhom.InsertRange(1,"");
            foreach (var a in pldv) {
                NhomDV moi = new NhomDV();
                moi.TenNhom = a.TenNhom;
                moi.IDNhom = a.IDNhom;
                moi.TenNhomCT = a.TenNhomCT;
                _lnhom.Add(moi);
            }
            lupPLDV.Properties.DataSource = _lnhom;
        }
        List<DSKP> _DSKP = new List<DSKP>(25);
        List<XuatDuoc> _XuatDuoc = new List<XuatDuoc>();
    
        private void btnInBC_Click(object sender, EventArgs e)
        {
            if (KTtaoBc())
            {
                
                foreach (var a in _PPXD)
                {
                    if (a.chon == true)
                    {
                        KP themmoi = new KP();
                        themmoi.KP0 = a.mappxd;
                        themmoi.tenkp = a.tenppxd;
                        themmoi.PL = a.PL1;
                        _KP.Add(themmoi);
                    }
                }
                for (int i = 0; i < _KP.Count; i++)
                {
                    if (_KP.Skip(i).First().PL == "Khoa dược")
                    {
                        int _M = _KP.Skip(i).First().KP0;
                        var q = (from nd in data.NhapDs.Where(p => p.MaKP == _M)
                                 join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                                 join dv in data.DichVus on ndct.MaDV equals dv.MaDV
                                 group new { ndct, dv } by new { ndct.MaDV, dv.TenDV, ndct.DonVi, ndct.DonGia } into kq
                                 select new
                                 {
                                     tendv = kq.Key.TenDV,
                                     madv = kq.Key.MaDV,
                                     donvi = kq.Key.DonVi,
                                     dongia = kq.Key.DonGia,
                                     SLT = (kq.Sum(p => p.ndct.SoLuongN) == null ? 0 : kq.Sum(p => p.ndct.SoLuongN)) - (kq.Sum(p => p.ndct.SoLuongX) == null ? 0 : kq.Sum(p => p.ndct.SoLuongX)),
                                 }).ToList();
                        if (q.Count > 0)
                        {
                            //List<XuatDuoc> _XuatDuoc1 = new List<XuatDuoc>();
                             List<XuatDuoc> _XuatDuoc1 = new List<XuatDuoc>();
                                    foreach (var b in q)
                                    {
                                        XuatDuoc themmoi = new XuatDuoc();
                                        themmoi.tendv = b.tendv;
                                        themmoi.dvt = b.donvi;
                                        themmoi.madv = b.madv == null ? 0 : b.madv.Value;
                                        themmoi.nuocsx = b.dongia;
                                        themmoi.sl1 =i==0? b.SLT:0;
                                        themmoi.sl2 =i==1? b.SLT:0;
                                        themmoi.sl3 =i==2? b.SLT:0;
                                        themmoi.sl4 =i==3? b.SLT:0;
                                        themmoi.sl5 =i==4? b.SLT:0;
                                        themmoi.sl6 =i==5? b.SLT:0;
                                        themmoi.sl7 =i==6? b.SLT:0;
                                        themmoi.sl8 =i==7? b.SLT:0;
                                        themmoi.sl9 =i==8? b.SLT:0;
                                        themmoi.sl10 =i==9? b.SLT:0;
                                        themmoi.sl11=i==10? b.SLT:0;
                                        themmoi.sl12 =i==11? b.SLT:0;
                                        themmoi.sl13 =i==12? b.SLT:0;
                                        themmoi.sl14=i==13? b.SLT:0;
                                        themmoi.sl15=i==14? b.SLT:0;
                                        themmoi.sl16=i==15? b.SLT:0;
                                        themmoi.sl17=i==16? b.SLT:0;
                                        themmoi.sl18=i==17? b.SLT:0;
                                        themmoi.sl19 =i==18? b.SLT:0;
                                        themmoi.sl20=i==19? b.SLT:0;
                                        themmoi.sl21=i==20? b.SLT:0;
                                        themmoi.sl22 =i==21? b.SLT:0;
                                        themmoi.sl23=i==22? b.SLT:0;
                                        themmoi.sl24=i==23? b.SLT:0;
                                        themmoi.sl25=i==24? b.SLT:0;
                                        themmoi.sl26 =i==25? b.SLT:0;
                                        themmoi.sl27=i==26? b.SLT:0;
                                        themmoi.sl28=i==27? b.SLT:0;
                                        _XuatDuoc.Add(themmoi);
                                    }
                                   
                        }
                    }
                    else
                    {
                        if (_KP.Skip(i).First().PL == "Xã phường")
                        {
                            int _M = _KP.Skip(i).First().KP0;
                            var q = (from nd in data.NhapDs.Where(p => p.MaKPnx == _M)
                                 join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                                 join dv in data.DichVus on ndct.MaDV equals dv.MaDV
                                 group new { ndct, dv, nd } by new { ndct.MaDV, dv.TenDV, ndct.DonVi, ndct.DonGia } into kq
                                 select new
                                 {
                                     tendv = kq.Key.TenDV,
                                     madv = kq.Key.MaDV,
                                     donvi = kq.Key.DonVi,
                                     dongia = kq.Key.DonGia,
                                     s = kq.Where(p => p.nd.PLoai == 2).Sum(p => p.ndct.SoLuongN) == null ? 0 : kq.Where(p => p.nd.PLoai == 2).Sum(p => p.ndct.SoLuongN),
                                     v = kq.Where(p => p.nd.PLoai == 5).Sum(p => p.ndct.SoLuongX) == null ? 0 : kq.Where(p => p.nd.PLoai == 5).Sum(p => p.ndct.SoLuongX),
                                     SLT = (kq.Where(p => p.nd.PLoai == 2).Sum(p => p.ndct.SoLuongN) == null ? 0 : kq.Where(p => p.nd.PLoai == 2).Sum(p => p.ndct.SoLuongN)) - (kq.Where(p => p.nd.PLoai == 5).Sum(p => p.ndct.SoLuongX)==null ?0:  kq.Where(p => p.nd.PLoai == 5).Sum(p => p.ndct.SoLuongX))
                                 }).ToList();
                            if (q.Count > 0)
                            {
                                foreach (var b in q)
                                {
                                    XuatDuoc themmoi = new XuatDuoc();
                                    themmoi.tendv = b.tendv;
                                    themmoi.dvt = b.donvi;
                                    themmoi.madv = b.madv == null ? 0 : b.madv.Value;
                                    themmoi.nuocsx = b.dongia;
                                    themmoi.sl1 = i == 0 ? b.SLT : 0;
                                    themmoi.sl2 = i == 1 ? b.SLT : 0;
                                    themmoi.sl3 = i == 2 ? b.SLT : 0;
                                    themmoi.sl4 = i == 3 ? b.SLT : 0;
                                    themmoi.sl5 = i == 4 ? b.SLT : 0;
                                    themmoi.sl6 = i == 5 ? b.SLT : 0;
                                    themmoi.sl7 = i == 6 ? b.SLT : 0;
                                    themmoi.sl8 = i == 7 ? b.SLT : 0;
                                    themmoi.sl9 = i == 8 ? b.SLT : 0;
                                    themmoi.sl10 = i == 9 ? b.SLT : 0;
                                    themmoi.sl11 = i == 10 ? b.SLT : 0;
                                    themmoi.sl12 = i == 11 ? b.SLT : 0;
                                    themmoi.sl13 = i == 12 ? b.SLT : 0;
                                    themmoi.sl14 = i == 13 ? b.SLT : 0;
                                    themmoi.sl15 = i == 14 ? b.SLT : 0;
                                    themmoi.sl16 = i == 15 ? b.SLT : 0;
                                    themmoi.sl17 = i == 16 ? b.SLT : 0;
                                    themmoi.sl18 = i == 17 ? b.SLT : 0;
                                    themmoi.sl19 = i == 18 ? b.SLT : 0;
                                    themmoi.sl20 = i == 19 ? b.SLT : 0;
                                    themmoi.sl21 = i == 20 ? b.SLT : 0;
                                    themmoi.sl22 = i == 21 ? b.SLT : 0;
                                    themmoi.sl23 = i == 22 ? b.SLT : 0;
                                    themmoi.sl24 = i == 23 ? b.SLT : 0;
                                    themmoi.sl25 = i == 24 ? b.SLT : 0;
                                    themmoi.sl26 = i == 25 ? b.SLT : 0;
                                    themmoi.sl27 = i == 26 ? b.SLT : 0;
                                    themmoi.sl28 = i == 27 ? b.SLT : 0;
                                    _XuatDuoc.Add(themmoi);
                                }

                            }
                        }
                    }
                }

                var c = (from d in _XuatDuoc
                         join dv in data.DichVus on d.madv equals dv.MaDV
                         group new { d, dv } by new { d.dvt, d.madv, d.nuocsx, d.tendv, } into kq
                         select new
                         {
                             dvt = kq.Key.dvt,
                             madv = kq.Key.madv,
                             nuocsx = kq.Key.nuocsx,
                             tendv = kq.Key.tendv,
                             sl1 = kq.Sum(p => p.d.sl1),
                             sl2 = kq.Sum(p => p.d.sl2),
                             sl3 = kq.Sum(p => p.d.sl3),
                             sl4 = kq.Sum(p => p.d.sl4),
                             sl5 = kq.Sum(p => p.d.sl5),
                             sl6 = kq.Sum(p => p.d.sl6),
                             sl7 = kq.Sum(p => p.d.sl7),
                             sl8 = kq.Sum(p => p.d.sl8),
                             sl9 = kq.Sum(p => p.d.sl9),
                             sl10 = kq.Sum(p => p.d.sl10),
                             sl11 = kq.Sum(p => p.d.sl11),
                             sl12 = kq.Sum(p => p.d.sl12),
                             sl13 = kq.Sum(p => p.d.sl13),
                             sl14 = kq.Sum(p => p.d.sl14),
                             sl15 = kq.Sum(p => p.d.sl15),
                             sl16 = kq.Sum(p => p.d.sl16),
                             sl17 = kq.Sum(p => p.d.sl17),
                             sl18 = kq.Sum(p => p.d.sl18),
                             sl19 = kq.Sum(p => p.d.sl19),
                             sl20 = kq.Sum(p => p.d.sl20),
                             sl21 = kq.Sum(p => p.d.sl21),
                             sl22 = kq.Sum(p => p.d.sl22),
                             sl23 = kq.Sum(p => p.d.sl23),
                             sl24 = kq.Sum(p => p.d.sl24),
                             sl25 = kq.Sum(p => p.d.sl25),
                             sl26 = kq.Sum(p => p.d.sl26),
                             sl27 = kq.Sum(p => p.d.sl27),
                             sl28 = kq.Sum(p => p.d.sl28),
                             TC = kq.Sum(p => p.d.sl1) + kq.Sum(p => p.d.sl2) + kq.Sum(p => p.d.sl3) + kq.Sum(p => p.d.sl4) + kq.Sum(p => p.d.sl5) + kq.Sum(p => p.d.sl6) + kq.Sum(p => p.d.sl7) + kq.Sum(p => p.d.sl8)+kq.Sum(p=>p.d.sl9)+kq.Sum(p=>p.d.sl10)+kq.Sum(p=>p.d.sl11)+kq.Sum(p=>p.d.sl12)+kq.Sum(p=>p.d.sl13)+kq.Sum(p=>p.d.sl14)+kq.Sum(p=>p.d.sl15)+kq.Sum(p=>p.d.sl16)+kq.Sum(p=>p.d.sl17)+kq.Sum(p=>p.d.sl18)+kq.Sum(p=>p.d.sl19)+kq.Sum(p=>p.d.sl20)+kq.Sum(p=>p.d.sl21)+kq.Sum(p=>p.d.sl22)+kq.Sum(p=>p.d.sl23)+kq.Sum(p=>p.d.sl24)+kq.Sum(p=>p.d.sl25)+kq.Sum(p=>p.d.sl26)+kq.Sum(p=>p.d.sl27)+kq.Sum(p=>p.d.sl28)
                         }).ToList();

                frmIn frm = new frmIn();
                BaoCao.Rep_BkTonD rep = new BaoCao.Rep_BkTonD(_DSKP);
                rep.TenBC.Value = "Báo cáo nhập xuất tồn";
                String ngaythang = "Từ ngày " + dateTuNgay.Text + " Đến ngày " + dateDenNgay.Text;
                rep.TuNgayDenNgay.Value = ngaythang.ToString();
                //rep.TenBC.Value = ("Báo cáo xuất " + tenPLDV + ": " + tenKP).ToUpper();
                //rep.DataSource = qsl2;
                //rep.BindingData();
                //rep.CreateDocument();
                //frm.prcIN.PrintingSystem = rep.PrintingSystem;
                //frm.ShowDialog();
                for (int i = 0; i < _KP.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            rep.KP1.Value = _KP.Skip(i).First().tenkp;
                            break;
                        case 1:
                            rep.KP2.Value = _KP.Skip(i).First().tenkp;
                            break;
                        case 2:
                            rep.KP3.Value = _KP.Skip(i).First().tenkp;
                            break;
                        case 3:
                            rep.KP4.Value = _KP.Skip(i).First().tenkp;
                            break;
                        case 4:
                            rep.KP5.Value = _KP.Skip(i).First().tenkp;
                            break;
                        case 5:
                            rep.KP6.Value = _KP.Skip(i).First().tenkp;
                            break;
                        case 6:
                            rep.KP7.Value = _KP.Skip(i).First().tenkp;
                            break;
                        case 7:
                            rep.KP8.Value = _KP.Skip(i).First().tenkp;
                            break;
                        case 8:
                            rep.KP9.Value = _KP.Skip(i).First().tenkp;
                            break;
                        case 9:
                            rep.KP10.Value = _KP.Skip(i).First().tenkp;
                            break;
                        case 10:
                            rep.KP11.Value = _KP.Skip(i).First().tenkp;
                            break;
                        case 11:
                            rep.KP12.Value = _KP.Skip(i).First().tenkp;
                            break;
                        case 12:
                            rep.KP13.Value = _KP.Skip(i).First().tenkp;
                            break;
                        case 13:
                            rep.KP14.Value = _KP.Skip(i).First().tenkp;
                            break;
                        case 14:
                            rep.KP15.Value = _KP.Skip(i).First().tenkp;
                            break;
                        case 15:
                            rep.KP16.Value = _KP.Skip(i).First().tenkp;
                            break;
                        case 16:
                            rep.KP17.Value = _KP.Skip(i).First().tenkp;
                            break;
                        case 17:
                            rep.KP18.Value = _KP.Skip(i).First().tenkp;
                            break;
                        case 18:
                            rep.KP19.Value = _KP.Skip(i).First().tenkp;
                            break;
                        case 19:
                            rep.KP20.Value = _KP.Skip(i).First().tenkp;
                            break;
                        case 20:
                            rep.KP21.Value = _KP.Skip(i).First().tenkp;
                            break;
                        case 21:
                            rep.KP22.Value = _KP.Skip(i).First().tenkp;
                            break;
                        case 22:
                            rep.KP23.Value = _KP.Skip(i).First().tenkp;
                            break;
                        case 23:
                            rep.KP24.Value = _KP.Skip(i).First().tenkp;
                            break;
                        case 24:
                            rep.KP25.Value = _KP.Skip(i).First().tenkp;
                            break;
                        case 25:
                            rep.KP26.Value = _KP.Skip(i).First().tenkp;
                            break;
                        case 26:
                            rep.KP27.Value = _KP.Skip(i).First().tenkp;
                            break;
                        case 27:
                            rep.KP28.Value = _KP.Skip(i).First().tenkp;
                            break;
                    }
                }
                rep.DataSource = c.OrderBy(p => p.tendv);
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

        private void grvPPXuat_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chọn")
            {
                if (grvPPXuat.GetFocusedRowCellValue("tenppxd") != null)
                {
                    string Ten = grvPPXuat.GetFocusedRowCellValue("tenppxd").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_PPXD.First().chon == true)
                        {
                            foreach (var a in _PPXD)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _PPXD)
                            {
                                a.chon = true;
                            }
                        }
                        grcPPXuat.DataSource = "";
                        grcPPXuat.DataSource = _PPXD.ToList();
                    }
                    //else
                    //{
                    //    //_PPXD.First().chon = false;
                    //    //grcPPXuat.DataSource = "";
                    //    //grcPPXuat.DataSource = _PPXD.ToList();
                    //}
                }
            }
        }
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
            //if (lupKho.EditValue == null)
            //{
            //    MessageBox.Show("Bạn chưa chọn kho xuất dược");
            //    lupKho.Focus();
            //    return false;
            //}

            if (lupPLDV.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn phân loại dược");
                lupPLDV.Focus();
                return false;
            }
            return true;
        }
        private class PPXD
        {
            private int MaPPXD;
            private string TenPPXD;
            private string pl;
            private bool Chon;
            public string PL1
            { set { pl = value; } get { return pl; } }
            public int mappxd { set { MaPPXD = value; } get { return MaPPXD; } }
            public string tenppxd { set { TenPPXD = value; } get { return TenPPXD; } }
            public bool chon { set { Chon = value; } get { return Chon; } }
        }
        private class KP
        {
            private int kp0;
            private string pl;
            private string Tenkp;
            public string tenkp
            { set { Tenkp = value; } get { return Tenkp; } }
            public string PL
            { set { pl = value; } get { return pl; } }
            public int KP0
            { set { kp0 = value; } get { return kp0; } }
        }
        private class XuatDuoc
        {
            private int MaDV;
            private string TenDV;
            private double NuocSX;
            private string DVT;
            private double SL1;
            private double SL2;
            private double SL3;
            private double SL4;
            private double SL5;
            private double SL6;
            private double SL7;
            private double SL8;
            private double SL9;
            private double SL10;
            private double SL11;
            private double SL12;
            private double SL13;
            private double SL14;
            private double SL15;
            private double SL16;
            private double SL17;
            private double SL18;
            private double SL19;
            private double SL20;
            private double SL21;
            private double SL22;
            private double SL23;
            private double SL24;
            private double SL25;
            private double SL26;
            private double SL27;
            private double SL28;
            private double TC;
            private string NhomDV;

            public string nhomdv
            { set { NhomDV = value; } get { return NhomDV; } }
            public int madv
            { set { MaDV = value; } get { return MaDV; } }
            public string tendv
            { set { TenDV = value; } get { return TenDV; } }
            public double nuocsx
            { set { NuocSX = value; } get { return NuocSX; } }
            public string dvt
            { set { DVT = value; } get { return DVT; } }
            public double sl1
            { set { SL1 = value; } get { return SL1; } }
            public double sl2
            { set { SL2 = value; } get { return SL2; } }
            public double sl3
            { set { SL3 = value; } get { return SL3; } }
            public double sl4
            { set { SL4 = value; } get { return SL4; } }
            public double sl5
            { set { SL5 = value; } get { return SL5; } }
            public double sl6
            { set { SL6 = value; } get { return SL6; } }
            public double sl7
            { set { SL7 = value; } get { return SL7; } }
            public double sl8
            { set { SL8 = value; } get { return SL8; } }
            public double sl9
            { set { SL9 = value; } get { return SL9; } }
            public double sl10
            { set { SL10 = value; } get { return SL10; } }
            public double sl11
            { set { SL11 = value; } get { return SL11; } }
            public double sl12
            { set { SL12 = value; } get { return SL12; } }
            public double sl13
            { set { SL13 = value; } get { return SL13; } }
            public double sl14
            { set { SL14 = value; } get { return SL14; } }
            public double sl15
            { set { SL15 = value; } get { return SL15; } }
            public double sl16
            { set { SL16 = value; } get { return SL16; } }
            public double sl17
            { set { SL17 = value; } get { return SL17; } }
            public double sl18
            { set { SL18 = value; } get { return SL18; } }
            public double sl19
            { set { SL19 = value; } get { return SL19; } }
            public double sl20
            { set { SL20 = value; } get { return SL20; } }
            public double sl21
            { set { SL21 = value; } get { return SL21; } }
            public double sl22
            { set { SL22= value; } get { return SL22; } }
            public double sl23
            { set { SL23= value; } get { return SL23; } }
            public double sl24
            { set { SL24= value; } get { return SL24; } }
            public double sl25
            { set { SL25= value; } get { return SL25; } }
            public double sl26
            { set { SL26 = value; } get { return SL26; } }
            public double sl27
            { set { SL27 = value; } get { return SL27; } }
            public double sl28
            { set { SL28 = value; } get { return SL28; } }
            public double tc
            { set { TC = value; } get { return TC; } }

        }

        public class DSKP
        {
            private string TenKP;
            private string MaKP;
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
            public string makp
            { set { MaKP = value; } get { return MaKP; } }
        }
        List<NhomDV> _lnhom = new List<NhomDV>();
        private void grvPPXuat_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if (e.Column.Name == "Chọn")
            //{
            //    grcPPXuat.DataSource = "";
            //    grcPPXuat.DataSource = _PPXD.ToList();
            //}
        }
        
        //public class PLDV 
        //{
        //    public int IdNhom;
        //    public string TenNhom;
        //    public string manhom;
        //    public string TenNhomDV
        //    { set { value = TenNhom; } get { return TenNhom; } }
        //    public string MaNhomDV
        //    { set { value = manhom; } get { return manhom; } }
        //}
    }
}