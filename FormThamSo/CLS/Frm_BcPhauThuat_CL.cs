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
    public partial class Frm_BcPhauThuat_CL : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcPhauThuat_CL()
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

            else return true;
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
        private class PT
        {
            private string TenKP;
            private int MaKP;
            private string CK;

            public string ck
            {
                get { return CK; }
                set { CK = value; }
            }
            private string TenDV;

            public string tendv
            {
                get { return TenDV; }
                set { TenDV = value; }
            }
            private int MaDV;

            public int madv
            {
                get { return MaDV; }
                set { MaDV = value; }
            }
            private int BHYT;

            public int bhyt
            {
                get { return BHYT; }
                set { BHYT = value; }
            }
            private int Loai1;

            public int loai1
            {
                get { return Loai1; }
                set { Loai1 = value; }
            }
            private int Loai2;

            public int loai2
            {
                get { return Loai2; }
                set { Loai2 = value; }
            }
            private int Loai3;

            public int loai3
            {
                get { return Loai3; }
                set { Loai3 = value; }
            }
            private int TS;

            public int ts
            {
                get { return TS; }
                set { TS = value; }
            }
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
          
        }

        List<KPhong> _Kphong = new List<KPhong>();
        List<PT> _PT = new List<PT>();
     
        private void Frm_BcPhauThuat_CL_Load(object sender, EventArgs e)
        {
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
            var kphong = (from kp in data.KPhongs.Where(p => p.PLoai == "Phòng khám" || p.PLoai == "Lâm sàng")
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
                    themmoi.makp = a.MaKP;
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;

            if (KTtaoBc())
            {

                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);//dateTuNgay.DateTime;
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);//dateDenNgay.DateTime;
                List<KPhong> _lKhoaP = new List<KPhong>();
       
                frmIn frm = new frmIn();
                BaoCao.Rep_BcPhauThuat_CL rep = new BaoCao.Rep_BcPhauThuat_CL();
                rep.NTN.Value = "(Từ ngày " + lupTuNgay.Text + " Đến ngày " + lupDenNgay.Text + ")";
                //  _lKhoaP = _Kphong.Where(p => p.makp != "").Where(p => p.chon == true).ToList();
                #region Lấy các lable trên BC
                rep.TP.Value = ("Thủ trưởng đơn vị").ToUpper();
                if (_Kphong.Where(p => p.chon == true).Count() == 1)
                {
                    rep.TP.Value = "Trưởng khoa".ToUpper();
                    rep.chon.Value = 1;
                    var k = (from kp in _lKhoaP
                              join p in data.KPhongs on kp.makp equals p.MaKP
                            select new {p.TenKP}).ToList();
                    if (k.Count > 0)
                        rep.Khoa.Value = k.First().TenKP.ToUpper();
                }
                #endregion

                _lKhoaP = _Kphong.Where(p => p.chon == true).ToList();
                _lKhoaP.Add(new KPhong { makp = 0, tenkp = "" });
                _PT.Clear();
                if (_lKhoaP.Count > 0)
                {
                    foreach (var a in _lKhoaP)
                    {
                        PT them = new PT();
                        them.makp = a.makp;
                        them.tenkp= a.tenkp;
                        _PT.Add(them);
                    }
                }
                if (chkBNcoCLS.Checked == true)
                {
                    var q = (from cls in data.CLS 
                             join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                              join dv in data.DichVus on cd.MaDV equals dv.MaDV
                             join tnhom in data.TieuNhomDVs on dv.IdTieuNhom equals tnhom.IdTieuNhom
                             select new { tnhom.TenRG, cls.MaBNhan, cls.MaKP, dv.MaDV, dv.TenDV, dv.Loai, cls.NgayTH }).ToList()
                             .Select(p => new {  p.TenRG, p.MaBNhan, p.MaKP,  p.MaDV, p.TenDV, Loai = p.Loai, p.NgayTH }).ToList();
                    var qth = (from ma in _lKhoaP
                               join q1 in q.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Where(p => p.TenRG == "Phẫu thuật") on ma.makp equals q1.MaKP
                               join bn in data.BenhNhans on q1.MaBNhan equals bn.MaBNhan
                               group new { bn, q1 } by new { ma.makp, q1.MaDV, q1.TenDV } into kq
                               select new
                               {
                                   MaKP = kq.Key.makp,
                                   MaDV = kq.Key.MaDV,
                                   TenDV = kq.Key.TenDV,
                                   TS = kq.Select(p => p.q1.MaDV).Count(),
                                   BHYT = kq.Where(p => p.bn.DTuong == "BHYT").Select(p => p.q1.MaDV).Count(),
                                   Loai1 = kq.Where(p => p.q1.Loai == 1).Select(p => p.q1.MaDV).Count(),
                                   Loai2 = kq.Where(p => p.q1.Loai == 2).Select(p => p.q1.MaDV).Count(),
                                   Loai3 = kq.Where(p => p.q1.Loai == 3).Select(p => p.q1.MaDV).Count(),

                               }).ToList();
                    if (qth.Count() > 0)
                    {
                        foreach (var a in _PT)
                        {
                            foreach (var b in qth)
                            {
                                if (b.MaKP == a.makp)
                                {
                                    a.madv = b.MaDV;
                                    a.tendv = b.TenDV;
                                    a.ts = Convert.ToInt32(b.TS);
                                    a.bhyt = Convert.ToInt32(b.BHYT);
                                    a.loai1 = Convert.ToInt32(b.Loai1);
                                    a.loai2 = Convert.ToInt32(b.Loai2);
                                    a.loai3 = Convert.ToInt32(b.Loai3);
                                }
                            }

                        }
                        
                    }

                    
                }
                if (chkBNkhongCLS.Checked == true)
                {
                    var qt = (from dt in data.DThuocs
                              join dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.IDCD == null || p.IDCD <= 0) on dt.IDDon equals dtct.IDDon
                              join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                              join tn in data.TieuNhomDVs.Where(p => p.TenRG == "Phẫu thuật") on dv.IdTieuNhom equals tn.IdTieuNhom
                              join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                               select new {dv.MaDV, dv.TenDV, bn.DTuong, dv.Loai, dtct.SoLuong,dtct.NgayNhap,dtct.MaKP,dtct.IDCD, tn.TenRG }).ToList()
                              .Select(p => new {p.MaDV, p.TenDV, p.MaKP, p.DTuong,p.IDCD, Loai = p.Loai, p.SoLuong ,p.NgayNhap,p.TenRG}).ToList().Distinct();
                    var q = (from k in _lKhoaP
                             join q1 in qt on k.makp equals q1.MaKP//.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.IDCD == null || p.IDCD <= 0).Where(p => p.TenRG == "Phẫu thuật")
                             group q1 by new {q1.MaDV, q1.TenDV,q1.MaKP, k.tenkp, } into kq
                             select new
                             {
                                 MaKP = kq.Key.MaKP,
                                 MaDV=kq.Key.MaDV,
                                 TenDV = kq.Key.TenDV,
                                 TS = kq.Sum(p => p.SoLuong),
                                 BHYT = kq.Where(p => p.DTuong == "BHYT").Sum(p => p.SoLuong),
                                 Loai1 = kq.Where(p => p.Loai == 1).Sum(p => p.SoLuong),
                                 Loai2 = kq.Where(p => p.Loai == 2).Sum(p => p.SoLuong),
                                 Loai3 = kq.Where(p => p.Loai == 3).Sum(p => p.SoLuong),

                             }).ToList();
                    if (q.Count > 0)
                    {
                        foreach (var a in _PT)
                        {
                            foreach (var b in q)
                            {
                                if (b.MaKP == a.makp)
                                {
                                     a.madv = b.MaDV;
                                    a.tendv = b.TenDV;
                                    a.ts = Convert.ToInt32(b.TS);
                                    a.bhyt = Convert.ToInt32(b.BHYT);
                                    a.loai1 = Convert.ToInt32(b.Loai1);
                                    a.loai2 = Convert.ToInt32(b.Loai2);
                                    a.loai3 = Convert.ToInt32(b.Loai3);
                                }
                            }
                        }
                    }
                  
                }
                
                  rep.DataSource = _PT.Where(p=>p.ts>0||p.bhyt>0||p.loai1>0||p.loai2>0||p.loai3>0).ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                
            }
        }

        private void grvKhoaphong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

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
        
    }
}