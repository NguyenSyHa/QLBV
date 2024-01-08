using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormTraCuu
{
    public partial class FrmTC_NhapXuatTon : DevExpress.XtraEditors.XtraForm
    {
        int _makho=0           , _madv=0;
        double _dongia = -1;
        public FrmTC_NhapXuatTon()
        {
            InitializeComponent();
        }
        public FrmTC_NhapXuatTon(int makho, int madv, double dg)
        {
            InitializeComponent();
            _madv = madv;
            _makho = makho;
            _dongia = dg;
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        DateTime _dttu = DungChung.Ham.NgayTu(System.DateTime.Now);
        DateTime _dtden = DungChung.Ham.NgayDen(System.DateTime.Now);
        private class NXT
        {
            private DateTime NgayNhap;
            private string NgayThang;
            private int ID;
            private string TenDV;
            private string SoCT;
            private string SoLo;
            private string HanDung;
            private string DonVi;
            private double DonGiaCT;
            private double DonGia;
            private int SoLuongN;
            private int PLoai;
            private double ThanhTienN;
            private int SoLuongSD;
            private double ThanhTienSD;
            private int SoLuongX;
            private int SLTon;
            private double ThanhTienX;
            public int id
            { set { ID = value; } get { return ID; } }
            public DateTime ngaynhap
            { set { NgayNhap = value; } get { return NgayNhap; } }
            public string ngaythang
            { set { NgayThang = value; } get { return NgayThang; } }
            public string soct
              { set { SoCT = value; } get { return SoCT; } }
            public string tendv
            { set { TenDV = value; } get { return TenDV; } }
            public string donvi
            { set { DonVi = value; } get { return DonVi; } }
            public string solo
            { set { SoLo = value; } get { return SoLo; } }
            public string handung
            { set { HanDung = value; } get { return HanDung; } }
            public int ploai
            { set { PLoai = value; } get { return PLoai; } }
            public double dongiact
            { set { DonGiaCT = value; } get { return DonGiaCT; } }
            public double dongia
            { set { DonGia = value; } get { return DonGia; } }
            public int soluongn
            { set { SoLuongN = value; } get { return SoLuongN; } }
            public double thanhtienn
            { set { ThanhTienN = value; } get { return ThanhTienN; } }
            public int soluongx
            { set { SoLuongX = value; } get { return SoLuongX; } }
            public double thanhtienx
            { set { ThanhTienX = value; } get { return ThanhTienX; } }
            public int soluongsd
            { set { SoLuongSD = value; } get { return SoLuongSD; } }
            public double thanhtiensd
            { set { ThanhTienSD = value; } get { return ThanhTienSD; } }
            public int slton
            { set { SLTon = value; } get { return SLTon; } }
        }
        List<NXT> _NXT = new List<NXT>();
        int makho = 0; string nhacc="";
        int idnhom = -1, idtnhom = -1, idnhom2 = -1, idtnhom2 = -1;
        int madv = 0, madv2 = 0;
        string mancc = "";
        double dg = 0, dg2 = 0;
        #region Tạo các list chứa các tham số
        private class ncc
        {
            private string MaNCC;
            private string TenNCC;

            public string TenNCC1
            {
                get { return TenNCC; }
                set { TenNCC = value; }
            }
            public string MaNCC1
            {
                get { return MaNCC; }
                set { MaNCC = value; }
            }

        }
        private class kho
        {
            private int MaKho;

            public int MaKho1
            {
                get { return MaKho; }
                set { MaKho = value; }
            }
        }
        private class nhomdv
        {
            private int IdNhom;

            public int IdNhom1
            {
                get { return IdNhom; }
                set { IdNhom = value; }
            }
        }
        private class tieunhomdv
        {
            private int IDTieuNhom;

            public int IDTieuNhom1
            {
                get { return IDTieuNhom; }
                set { IDTieuNhom = value; }
            }
        }
        private class dichvu
        {
            private int MaDV;

            public int MaDV1
            {
                get { return MaDV; }
                set { MaDV = value; }
            }
        }
        private class dongia
        {
            private double DonGia;

            public double DonGia1
            {
                get { return DonGia; }
                set { DonGia = value; }
            }
        }
        #endregion
        List<ncc> _ncc = new List<ncc>();
        List<kho> _kho = new List<kho>();
        List<nhomdv> _nhomdv = new List<nhomdv>();
        List<tieunhomdv> _tieunhomdv = new List<tieunhomdv>();
        List<dichvu> _dv = new List<dichvu>();
        List<dongia> _dg = new List<dongia>();
        List<KPhong> _lkp = new List<KPhong>();
        private void FrmTC_NhapXuatTon_Load(object sender, EventArgs e)
        {
            dtTimTuNgay.DateTime = System.DateTime.Now.AddYears(-1);
            dtTimDenNgay.DateTime = System.DateTime.Now;
            dtTimTuNgay2.DateTime = System.DateTime.Now.AddYears(-1);
            dtTimDenNgay2.DateTime = System.DateTime.Now;
         
          var nhapd = (from nduoc in _data.NhapDs.Where(p => p.PLoai == 1 || p.PLoai == 2)
                        join nduocct in _data.NhapDcts on nduoc.IDNhap equals nduocct.IDNhap
                   select new { nduoc.PLoai, nduoc.MaKP, nduocct.MaDV, nduocct.DonGia }).ToList();
          var dm = (from dv in _data.DichVus
                    join nhom in _data.NhomDVs.Where(p => p.Status == 1) on dv.IDNhom equals nhom.IDNhom
                    join tnhom in _data.TieuNhomDVs on nhom.IDNhom equals tnhom.IDNhom
                    select new { nhom.IDNhom, nhom.TenNhomCT, nhom.Status, tnhom.IdTieuNhom, tnhom.TenTN, dv.TenDV, dv.MaDV, dv.DonVi,dv.MaCC }).ToList();
          var qnd = (from nduoc in nhapd
                     join d in dm on nduoc.MaDV equals d.MaDV
                     select new { d.IDNhom, d.TenNhomCT, d.Status, d.IdTieuNhom, d.TenTN, d.TenDV, nduoc.MaKP, nduoc.PLoai, d.MaCC, nduoc.MaDV, nduoc.DonGia, d.DonVi, }).ToList();
            var _kho = (from nd in qnd
                      join kp in _data.KPhongs on nd.MaKP equals kp.MaKP
                        group kp by new { kp.TenKP, kp.MaKP } into kq
                        select new { kq.Key.TenKP, kq.Key.MaKP }).OrderBy(p => p.TenKP).ToList();
            foreach (var a in _kho)
            { _lkp.Add(new KPhong { MaKP = a.MaKP, TenKP = a.TenKP }); }
            var qcc = (from nd in qnd.Where(p => p.Status == 1).Where(p => p.PLoai == 1 || p.PLoai == 2)
                      join cc in _data.NhaCCs on nd.MaCC equals cc.MaCC
                      group cc by new {cc.MaCC,cc.TenCC} into kq select new { kq.Key.MaCC, kq.Key.TenCC }).ToList();
             var nhomdv = from nhom in qnd group nhom by new { nhom.IDNhom, nhom.TenNhomCT } into kq select new { kq.Key.IDNhom, kq.Key.TenNhomCT };
             var tnhomdv = from tnhom in qnd group tnhom by new { tnhom.IdTieuNhom, tnhom.TenTN } into kq select new { kq.Key.IdTieuNhom, kq.Key.TenTN };
             var qdv = (from dv in qnd group dv by new { dv.TenDV, dv.MaDV } into kq select new { kq.Key.TenDV, kq.Key.MaDV});
             #region Load lên Tab1
             lupNhomDV2.Properties.DataSource = nhomdv;
             lupTieuNhomDV2.Properties.DataSource = tnhomdv;
             lupMaDV2.Properties.DataSource = qdv.ToList();

             if (_kho.Count() > 0)
             {
                 for (int i = 0; i < _kho.Count(); i++)
                 {                
                     switch (i)
                     {
                         case 0:
                             if (_kho.Skip(i).First().TenKP != null)
                             { gridColumn2.Caption = _kho.Skip(i).First().TenKP; }
                             break;
                         case 1:
                             if (_kho.Skip(i).First().TenKP != null)
                             { gridColumn3.Caption = _kho.Skip(i).First().TenKP; }
                             break;
                         case 2:
                             if (_kho.Skip(i).First().TenKP != null)
                             { gridColumn4.Caption = _kho.Skip(i).First().TenKP; }
                             break;
                         case 3:
                             if (_kho.Skip(i).First().TenKP != null)
                             { gridColumn5.Caption = _kho.Skip(i).First().TenKP; }
                             break;
                         case 4:
                             if (_kho.Skip(i).First().TenKP != null)
                             { gridColumn6.Caption = _kho.Skip(i).First().TenKP; }
                             break;
                         case 5:
                             if (_kho.Skip(i).First().TenKP != null)
                             { gridColumn7.Caption = _kho.Skip(i).First().TenKP; }
                              break;
                         case 6:
                             if (_kho.Skip(i).First().TenKP != null)
                             { gridColumn8.Caption = _kho.Skip(i).First().TenKP; }
                              break;
                         case 7:
                             if (_kho.Skip(i).First().TenKP != null)
                             { gridColumn9.Caption = _kho.Skip(i).First().TenKP; }
                             break;
                         case 8:
                             if (_kho.Skip(i).First().TenKP != null)
                             { gridColumn10.Caption = _kho.Skip(i).First().TenKP; }
                             break;

                     }
                 }
             }
             #endregion
            #region Load lên Tab2
            lupKhoXuat.Properties.DataSource = _kho;
            lupNhaCC.Properties.DataSource = qcc;
            lupNhomDV.Properties.DataSource = nhomdv;
            lupTieuNhomDV.Properties.DataSource = tnhomdv;
            lupMaDV.Properties.DataSource = qdv;
            var qdgia = (from dv in qnd group dv by new { dv.DonGia } into kq select new { kq.Key.DonGia }).ToList();
            var gia = qdgia.Select(p => new { p.DonGia }).ToList();
            if (gia.Count > 0)
            {
                foreach (var g in gia)
                {
                    cboDonGia.Properties.Items.Add(g.DonGia);
                    cboDonGia2.Properties.Items.Add(g.DonGia);
                }
            }
            #endregion
          
        }
        #region Lấy dữ liệu load lên form khi thay đổi các lựa chọn tham số
    
        private void lupKhoXuat_EditValueChanged(object sender, EventArgs e)
        {
            if (lupKhoXuat.EditValue != null)
            makho =Convert.ToInt32( lupKhoXuat.EditValue);
            var qdv = (from nduoc in _data.NhapDs
                       join nduocct in _data.NhapDcts on nduoc.IDNhap equals nduocct.IDNhap
                       join dv in _data.DichVus on nduocct.MaDV equals dv.MaDV
                       join ndv in _data.NhomDVs on dv.IDNhom equals ndv.IDNhom
                       join tnhom in _data.TieuNhomDVs on dv.IdTieuNhom equals tnhom.IdTieuNhom
                       select new { nduoc.PLoai, PLoaiDV = dv.PLoai, ndv.IDNhom, tnhom.IdTieuNhom, ndv.TenNhomCT, tnhom.TenTN,nduoc.MaCC, nduoc.MaKP,nduocct.DonGia, dv.TenDV, dv.MaDV, dv.DonVi }).ToList();
            //var ncc = (from nd in qdv.Where(p => p.MaKP== makho).Where(p => p.PLoai == 1).Where(p => p.PLoaiDV == 1)
            //           join nc in _data.NhaCCs on nd.MaCC equals nc.MaCC
            //            group nc by new {nc.MaCC,nc.TenCC } into kq
            //            select new { kq.Key.MaCC, kq.Key.TenCC }).ToList();
            //lupNhaCC.Properties.DataSource = ncc.ToList();
            var nhom = (from nd in qdv.Where(p => p.MaKP== makho).Where(p => p.PLoai == 1).Where(p => p.PLoaiDV == 1)
                        group  nd by new { nd.IDNhom,nd.TenNhomCT} into kq
                        select new { kq.Key.IDNhom,kq.Key.TenNhomCT }).ToList();
            lupNhomDV.Properties.DataSource = nhom.ToList();
            var tieunhom = (from nd in qdv.Where(p => p.MaKP== makho).Where(p => p.PLoai == 1).Where(p => p.PLoaiDV == 1)
                            group nd by new { nd.IdTieuNhom, nd.TenTN } into kq
                            select new { kq.Key.IdTieuNhom, kq.Key.TenTN }).ToList();
            lupTieuNhomDV.Properties.DataSource = tieunhom.ToList();
            var duoc = (from d in qdv.Where(p => p.MaKP== makho).Where(p => p.PLoai == 1).Where(p => p.PLoaiDV == 1)
                        group d by new { d.MaKP, d.TenDV, d.MaDV, d.DonVi ,d.DonGia} into kq
                        select new { kq.Key.TenDV, kq.Key.MaDV, kq.Key.DonVi, kq.Key.MaKP ,kq.Key.DonGia}
                        ).OrderBy(p => p.TenDV).ToList(); 
            lupMaDV.Properties.DataSource = duoc.ToList();
            var gia = (from nhapduoc in _data.NhapDcts
                       join nduoc in _data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.MaKP== makho) on nhapduoc.IDNhap equals nduoc.IDNhap
                       group new { nhapduoc } by new { nhapduoc.DonGia } into kq
                       select new { kq.Key.DonGia }).ToList();
            gia = gia.ToList();
            if (gia.Count > 0)
            {
                foreach (var a in gia)
                {
                    cboDonGia.Properties.Items.Add(a.DonGia);
                }
            }
           
        }
        private void lupNhaCC_EditValueChanged(object sender, EventArgs e)
        {
            if (lupNhaCC.EditValue != null && lupNhaCC.EditValue.ToString() != "")
                mancc = lupNhaCC.EditValue.ToString();
            var cc = (from nd in _data.NhapDs.Where(p => p.MaCC == mancc)
                      join kp in _data.KPhongs on nd.MaKP equals kp.MaKP
                      select new { kp.MaKP, kp.TenKP }).Distinct().ToList();

            lupKhoXuat.Properties.DataSource = cc.ToList();
        }

        private void lupNhomDV_EditValueChanged(object sender, EventArgs e)
        {
            if (lupNhomDV.EditValue != null && lupNhomDV.EditValue.ToString() != "")
                idnhom =Convert.ToInt32(lupNhomDV.EditValue);
            var qdv = (from nduoc in _data.NhapDs
                       join nduocct in _data.NhapDcts on nduoc.IDNhap equals nduocct.IDNhap
                       join dv in _data.DichVus on nduocct.MaDV equals dv.MaDV
                       join tnhom in _data.TieuNhomDVs on dv.IdTieuNhom equals tnhom.IdTieuNhom
                       select new {tnhom.IDNhom, tnhom.IdTieuNhom, tnhom.TenTN,dv.TenDV,dv.MaDV}).ToList();
            var tieunhom = (from nd in qdv.Where(p=>p.IDNhom==idnhom)
                            group nd by new { nd.IdTieuNhom, nd.TenTN } into kq
                            select new { kq.Key.IdTieuNhom, kq.Key.TenTN }).ToList();
            lupTieuNhomDV.Properties.DataSource = tieunhom.ToList();
            var dvu = (from d in qdv.Where(p => p.IDNhom == idnhom)
                       group d by new { d.TenDV, d.MaDV } into kq
                       select new { kq.Key.MaDV, kq.Key.TenDV }).ToList();
            lupMaDV.Properties.DataSource = dvu.ToList();
            
        }
        private void lupTieuNhomDV_EditValueChanged(object sender, EventArgs e)
        {
            if (lupTieuNhomDV.EditValue != null && lupTieuNhomDV.EditValue.ToString() != "")
                idtnhom = Convert.ToInt32(lupTieuNhomDV.EditValue);
            var qdv = (from nduoc in _data.NhapDs
                       join nduocct in _data.NhapDcts on nduoc.IDNhap equals nduocct.IDNhap
                       join dv in _data.DichVus.Where(p=>p.IdTieuNhom==idtnhom) on nduocct.MaDV equals dv.MaDV
                       select new { dv.TenDV,dv.MaDV,dv.DonVi,dv.IdTieuNhom }).ToList();
            var dvu = (from d in qdv.Where(p => p.IdTieuNhom == idtnhom)
                            group d by new { d.TenDV,d.MaDV } into kq
                            select new { kq.Key.MaDV, kq.Key.TenDV }).ToList();
            lupMaDV.Properties.DataSource = dvu.ToList();
        }

        private void lupMaDV_EditValueChanged(object sender, EventArgs e)
        {
            cboDonGia.Text = "";
            for (int a = 0; a < cboDonGia.Properties.Items.Count; a++)
            {
                cboDonGia.Properties.Items.RemoveAt(a);
            }
            if (lupMaDV.EditValue != null)
            {
                madv = Convert.ToInt32( lupMaDV.EditValue);
                var gia = (from nhapduoc in _data.NhapDcts.Where(p => p.MaDV== madv)
                           join nduoc in _data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.MaKP== makho) on nhapduoc.IDNhap equals nduoc.IDNhap
                           group new { nhapduoc } by new { nhapduoc.DonGia } into kq
                           select new { kq.Key.DonGia }).ToList();
                if (gia.Count > 0)
                {
                    foreach (var g in gia)
                    {
                        cboDonGia.Properties.Items.Add(g.DonGia);
                    }
                }
            }
        }
        #endregion
        private bool KT()
        {

            if (dtTimTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu");
                dtTimTuNgay.Focus();
                return false;
            }
            if (dtTimDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc");
                dtTimDenNgay.Focus();
                return false;
            }
            if (lupKhoXuat.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn kho");
                lupKhoXuat.Focus();
                return false;
            }
            else return true;
        }
        private bool KT2()
        {

            if (dtTimTuNgay2.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu");
                dtTimTuNgay2.Focus();
                return false;
            }
            if (dtTimDenNgay2.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc");
                dtTimDenNgay2.Focus();
                return false;
            }
            //if (lupKhoXuat.EditValue == null)
            //{
            //    MessageBox.Show("Bạn chưa chọn kho");
            //    lupKhoXuat.Focus();
            //    return false;
            //}
            else return true;
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            _NXT.Clear();
            _dttu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
            _dtden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
            _ncc.Clear(); _kho.Clear(); _nhomdv.Clear(); _tieunhomdv.Clear(); _dv.Clear(); _dg.Clear();
            int _sd = 1;
            if (cmbTD.EditValue == "1. Nhập - Xuất") { _sd = 1; }
            if (cmbTD.EditValue == "2. Nhập - Sử dụng") { _sd = 2; }
            if (cmbTD.EditValue == "3. Nhập - Xuất - Sử dụng") { _sd = 3; }
            if (KT())
            {
                #region Lấy list các dữ liệu theo tham số chọn
                if (!string.IsNullOrEmpty(lupNhaCC.Text))
                {
                    if (lupNhaCC.EditValue != null && lupNhaCC.EditValue.ToString() != "")
                        nhacc = lupNhaCC.EditValue.ToString();
                    { _ncc.Add(new ncc { MaNCC1 = nhacc }); }
                    
                }
                else
                {
                    var ncc = _data.NhaCCs.Select(p => new { p.MaCC }).ToList();
                     foreach (var a in ncc)
                     { _ncc.Add(new ncc { MaNCC1 = a.MaCC }); }
                    _ncc.Add(new ncc { MaNCC1 = "" });

                }
                if (!string.IsNullOrEmpty(lupKhoXuat.Text))
                {
                    if (lupKhoXuat.EditValue != null )
                        makho = Convert.ToInt32( lupKhoXuat.EditValue);
                    { _kho.Add(new kho { MaKho1 = makho }); }
                }
                else
                {
                    var khox = _data.KPhongs.Where(p => p.PLoai.Contains("dược")).Select(p => new { p.MaKP }).ToList();
                    if (khox.Count > 0)
                    {
                        foreach (var a in khox)
                        {_kho.Add(new kho { MaKho1 = a.MaKP });}
                    }
                    _kho.Add(new kho { MaKho1 = 0 });
      
                }
                if (!string.IsNullOrEmpty(lupNhomDV.Text))
                {
                    if (lupNhomDV.EditValue != null && lupNhomDV.EditValue.ToString() != "")
                    {
                        idnhom = Convert.ToInt32(lupNhomDV.EditValue);
                        _nhomdv.Add(new nhomdv { IdNhom1 = idnhom });
                     
                    }
                }
                else
                {
                    var nhomdv = _data.NhomDVs.Where(p => p.Status == 1).Select(p => new { p.IDNhom }).ToList();
                    if (nhomdv.Count > 0)
                    {
                        foreach (var a in nhomdv)
                        {
                            _nhomdv.Add(new nhomdv{IdNhom1=a.IDNhom});
                        }
                    }
                }
                if (!string.IsNullOrEmpty(lupTieuNhomDV.Text))
                {
                    if (lupTieuNhomDV.EditValue != null && lupTieuNhomDV.EditValue.ToString() != "")
                    {
                        _tieunhomdv.Add(new tieunhomdv{IDTieuNhom1=idnhom});
                      
                    }
                }
                else
                {
                    var tnhomdv = (from nhom in _data.NhomDVs.Where(p => p.Status == 1)
                                   join tnhom in _data.TieuNhomDVs on nhom.IDNhom equals tnhom.IDNhom
                                   group tnhom by new { tnhom.IdTieuNhom } into kq
                                   select new { kq.Key.IdTieuNhom }).ToList();
                    if (tnhomdv.Count > 0)
                    {
                        foreach (var a in tnhomdv)
                        {
                            _tieunhomdv.Add(new tieunhomdv {IDTieuNhom1=a.IdTieuNhom});
                        }
                    }
                }
                var qdv1 = (from nduoc in _data.NhapDs
                            join nduocct in _data.NhapDcts on nduoc.IDNhap equals nduocct.IDNhap
                            join dv in _data.DichVus on nduocct.MaDV equals dv.MaDV
                            select new { nduoc.PLoai, nduoc.MaKP, dv.TenDV, dv.MaDV, dv.DonVi, nduocct.DonGia }).ToList();
                var qdv = (from dv in qdv1.Where(p => p.PLoai == 1 || p.PLoai == 2)
                           group dv by new { dv.TenDV, dv.MaDV, dv.DonGia } into kq
                           select new { kq.Key.MaDV, kq.Key.DonGia }).ToList();
                if (!string.IsNullOrEmpty(lupMaDV.Text))
                {
                    if (lupMaDV.EditValue != null )
                    {
                        madv = Convert.ToInt32( lupMaDV.EditValue); 
                      }
                }
                else
                {

                    var dvu = qdv.Select(p => new { p.MaDV }).ToList();
                    if (dvu.Count > 0)
                    {
                        foreach (var a in dvu)
                        {
                            _dv.Add(new dichvu { MaDV1 = a.MaDV });
                        }
                    }
                    _dv.Add(new dichvu { MaDV1 = 0 });
                }
                if (!string.IsNullOrEmpty(cboDonGia.Text))
                {
                    dg = Convert.ToDouble(cboDonGia.EditValue);

                    {_dg.Add(new dongia{DonGia1=dg});}
                }
                else
                {

                    var dgia = qdv.Select(p => new { p.DonGia }).ToList();
                    if (dgia.Count > 0)
                    {
                        foreach (var a in dgia)
                        {_dg.Add(new dongia{DonGia1= Convert.ToDouble(a.DonGia)}); }
                    }
                }
                #endregion
                #region Lấy dữ liệu đưa lên list 
                var qnhapd = (from nd in _data.NhapDs
                            join ndct in _data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                            select new { nd.MaKP, nd.PLoai, nd.NgayNhap, ndct.MaCC, ndct.MaDV, nd.IDNhap, nd.SoCT, ndct.SoLo, ndct.HanDung, ndct.DonVi, ndct.DonGiaCT, ndct.DonGia, ndct.SoLuongN, ndct.ThanhTienN, ndct.ThanhTienX, ndct.SoLuongX, ndct.ThanhTienSD, ndct.SoLuongSD }).ToList();
                var qnhom = (from dv in _data.DichVus
                            join ndv in _data.NhomDVs on dv.IDNhom equals ndv.IDNhom
                            join tndv in _data.TieuNhomDVs on dv.IdTieuNhom equals tndv.IdTieuNhom
                            select new {dv.MaDV, dv.TenDV, ndv.IDNhom, tndv.IdTieuNhom,}).ToList();
                var qnxt = (from nd in qnhapd
                            join nhom in qnhom on nd.MaDV equals nhom.MaDV
                            select new { nd.MaKP,nd.PLoai, nd.NgayNhap, nd.MaCC, nd.MaDV,nhom.TenDV, nhom.IDNhom, nhom.IdTieuNhom, nd.IDNhap, nd.SoCT, nd.SoLo, nd.HanDung, nd.DonVi, nd.DonGiaCT, nd.DonGia, nd.SoLuongN, nd.ThanhTienN, nd.ThanhTienX, nd.SoLuongX ,nd.ThanhTienSD,nd.SoLuongSD}).ToList();
                var nxt1 = (from a in _kho
                            join q in qnxt on a.MaKho1 equals q.MaKP
                            select new { q.MaKP,q.PLoai, q.NgayNhap, q.MaCC, q.MaDV,q.TenDV, q.IDNhom, q.IdTieuNhom, q.IDNhap, q.SoCT, q.SoLo, q.HanDung, q.DonVi, q.DonGiaCT, q.DonGia, q.SoLuongN, q.ThanhTienN, q.ThanhTienX, q.SoLuongX,q.ThanhTienSD,q.SoLuongSD }).ToList();
                var nxt2 = (from a in _ncc
                            join q in nxt1 on a.MaNCC1 equals q.MaCC
                            select new { q.MaKP, q.PLoai, q.NgayNhap, q.MaCC, q.MaDV, q.TenDV, q.IDNhom, q.IdTieuNhom, q.IDNhap, q.SoCT, q.SoLo, q.HanDung, q.DonVi, q.DonGiaCT, q.DonGia, q.SoLuongN, q.ThanhTienN, q.ThanhTienX, q.SoLuongX, q.ThanhTienSD, q.SoLuongSD }).ToList();
                var nxt3 = (from a in _nhomdv
                            join q in nxt2 on a.IdNhom1 equals q.IDNhom
                            select new { q.MaKP, q.PLoai, q.NgayNhap, q.MaCC, q.MaDV, q.TenDV, q.IDNhom, q.IdTieuNhom, q.IDNhap, q.SoCT, q.SoLo, q.HanDung, q.DonVi, q.DonGiaCT, q.DonGia, q.SoLuongN, q.ThanhTienN, q.ThanhTienX, q.SoLuongX, q.ThanhTienSD, q.SoLuongSD }).ToList();
                var nxt4 = (from a in _tieunhomdv
                            join q in nxt3 on a.IDTieuNhom1 equals q.IdTieuNhom
                            select new { q.MaKP, q.PLoai, q.NgayNhap, q.MaCC, q.MaDV, q.TenDV, q.IDNhom, q.IdTieuNhom, q.IDNhap, q.SoCT, q.SoLo, q.HanDung, q.DonVi, q.DonGiaCT, q.DonGia, q.SoLuongN, q.ThanhTienN, q.ThanhTienX, q.SoLuongX, q.ThanhTienSD, q.SoLuongSD }).ToList();
                var nxt5 = (from a in _dv
                            join q in nxt4 on a.MaDV1 equals q.MaDV
                            select new { q.MaKP, q.PLoai, q.NgayNhap, q.MaCC, q.MaDV, q.TenDV, q.IDNhom, q.IdTieuNhom, q.IDNhap, q.SoCT, q.SoLo, q.HanDung, q.DonVi, q.DonGiaCT, q.DonGia, q.SoLuongN, q.ThanhTienN, q.ThanhTienX, q.SoLuongX, q.ThanhTienSD, q.SoLuongSD }).ToList();
                var nxt6 = (from a in _dg
                            join q in nxt5 on a.DonGia1 equals q.DonGia
                            select new { q.MaKP, q.PLoai, q.NgayNhap, q.MaCC, q.MaDV, q.TenDV, q.IDNhom, q.IdTieuNhom, q.IDNhap, q.SoCT, q.SoLo, q.HanDung, q.DonVi, q.DonGiaCT, q.DonGia, q.SoLuongN, q.ThanhTienN, q.ThanhTienX, q.SoLuongX, q.ThanhTienSD, q.SoLuongSD }).ToList();

                var nxt = (from nd in nxt6.Where(p => p.NgayNhap >= _dttu).Where(p => p.NgayNhap <= _dtden)
                           group nd by new { nd.IDNhap, nd.PLoai, nd.NgayNhap, nd.TenDV, nd.SoCT, nd.SoLo, nd.HanDung, nd.DonVi, nd.DonGiaCT, nd.DonGia, } into kq
                           select new
                           {
                               ID = kq.Key.IDNhap,
                               NgayNhap = kq.Key.NgayNhap,
                               PLoai=kq.Key.PLoai,
                               SoCT = kq.Key.SoCT,
                               DonVi = kq.Key.DonVi,
                               TenDV=kq.Key.TenDV,
                               SoLo = kq.Key.SoLo,
                               HanDung = kq.Key.HanDung,
                               DonGiaCT = kq.Key.DonGiaCT,
                               DonGia = kq.Key.DonGia,
                               SoLuongN = kq.Sum(p=>p.SoLuongN),
                               ThanhTienN = kq.Sum(p=>p.ThanhTienN),
                               SoLuongX = kq.Sum(p=>p.SoLuongX),
                               ThanhTienX = kq.Sum(p => p.ThanhTienX),
                               SoLuongSD = kq.Sum(p => p.SoLuongSD),
                               ThanhTienSD = kq.Sum(p => p.ThanhTienSD),
                             //  SLTon = _sd == 1 ? kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX) : (_sd == 2 ? kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongSD) : kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX) - kq.Sum(p => p.SoLuongSD))
                           }).ToList();
                if (nxt.Count > 0)
                {
                    foreach (var x in nxt)
                    {
                        NXT them = new NXT();
                        them.id = Convert.ToInt32(x.ID);
                        them.ngaynhap = Convert.ToDateTime(x.NgayNhap);
                        them.ploai = Convert.ToInt32(x.PLoai);
                        them.ngaythang = x.NgayNhap.ToString();
                        them.tendv = x.TenDV;
                        them.soct = x.SoCT.ToString();
                        if (x.DonVi != null || x.DonVi != " ")
                        {
                            them.donvi = x.DonVi.ToString();
                        }
                        else them.donvi = "";
                        if (x.SoLo != null && x.SoLo.ToString() != " ")
                        { them.solo = x.SoLo.ToString(); }
                        else them.solo = "";
                        if (x.HanDung != null)
                        { them.handung = x.HanDung.ToString(); }
                        else them.handung = " ";
                        them.dongia = Convert.ToDouble(x.DonGia);
                        them.dongiact = Convert.ToDouble(x.DonGiaCT);
                        them.soluongn = Convert.ToInt32(x.SoLuongN);
                        them.thanhtienn = Convert.ToDouble(x.ThanhTienN);
                        them.thanhtienx = Convert.ToDouble(x.ThanhTienX);
                        them.soluongx = Convert.ToInt32(x.SoLuongX);
                        them.soluongsd = Convert.ToInt32(x.SoLuongSD);
                        them.thanhtiensd = Convert.ToDouble(x.ThanhTienSD);
                       // them.slton = Convert.ToInt32(x.SLTon);
                        _NXT.Add(them);
                    }
                 }
                #endregion
                #region Tính tồn cuối
                if (_dv.Count == 1)
            {
                       var nxttk = (from nd in nxt6.Where(p => p.NgayNhap >= _dttu).Where(p => p.NgayNhap <= _dtden)
                             group nd by new { nd.NgayNhap } into kq
                             select new
                             {
                                 NgayNhap = kq.Key.NgayNhap,
                                 SoLuongN = kq.Sum(p=>p.SoLuongN),
                                 SoLuongX = kq.Sum(p=>p.SoLuongX),
                                 SoLuongSD=kq.Sum(p=>p.SoLuongSD),
                             }).OrderBy(p=>p.NgayNhap).ToList();
             
                var nxtdk = (from nd in nxt6.Where(p => p.NgayNhap < _dttu)
                             group nd by new { } into kq
                             select new
                             {
                                 SoLuong = _sd == 1 ? kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX) : (_sd == 2 ? kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongSD) : kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX) - kq.Sum(p => p.SoLuongSD))
                             }).ToList();
                if (nxtdk.Count > 0)
                {
                    if (nxtdk.First().SoLuong !=0)
                    {
                        _NXT.Add(new NXT { id = 0, soct = ("Tồn đầu kỳ").ToUpper(), slton = Convert.ToInt32(nxtdk.First().SoLuong) });
                    }
                    
                }
                else
                {
                    _NXT.Add(new NXT { id = 0, soct = ("Tồn đầu kỳ").ToUpper(), slton = 0 });

                }

                _NXT = _NXT.OrderBy(p => p.ngaynhap).ThenBy(p => p.ploai).ToList();
           
               
                    for (int i = 1; i < _NXT.Count; i++)
                    {
                            double slt = 0, sln = 0, slx = 0,slsd=0;
                            slt = _NXT.First().slton;
                        if (_NXT.Skip(i-1).Take(1).First().slton != null)
                        {
                            slt = _NXT.Skip(i-1).Take(1).First().slton;
                        }
                        if (_NXT.Skip(i-1).Take(1).First().soluongx != null)
                        {
                            slx = _NXT.Skip(i).Take(1).First().soluongx;
                        }
                        if (_NXT.Skip(i).Take(1).First().soluongn != null)
                        {
                            sln = _NXT.Skip(i).Take(1).First().soluongn;
                        }
                        if(_NXT.Skip(i).Take(1).First().soluongsd!=null)
                        {
                            slsd=_NXT.Skip(i).Take(1).First().soluongsd;
                        }
                        _NXT.Skip(i).Take(1).First().slton =_sd==1?Convert.ToInt32(slt + sln - slx):(_sd==2?Convert.ToInt32(slt+sln-slsd):Convert.ToInt32(slt+sln-slx-slsd));
                    }
                }
                #endregion
                grcTraCuu.DataSource = _NXT.OrderBy(p=>p.ngaynhap);
            }
        }

     
        private void grvTraCuu_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == STT)
            {
                e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void _hien(object sender, EventArgs e)
        {
            this.Show();
        }
        private void grvTraCuu_DoubleClick(object sender, EventArgs e)
        {
            _id = 0;
            if (grvTraCuu.GetFocusedRowCellValue(colID) != null && grvTraCuu.GetFocusedRowCellValue(colID).ToString() != "")
            {
                _id = Convert.ToInt32(grvTraCuu.GetFocusedRowCellValue(colID));//.ToString();

            }
            if (_id > 0)
            {
                this.Hide();
                FormTraCuu.Frm_TcNhapXuatTonct frm = new FormTraCuu.Frm_TcNhapXuatTonct(_id);
               // frm.FormClosed += new FormClosedEventHandler(this.frmHSBN_Load);
                frm.FormClosed += new FormClosedEventHandler(this._hien);
                frm.ShowDialog();
            }
        }
        int _id = -1;
       private void grvTraCuu_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
              if (grvTraCuu.GetFocusedRowCellValue(colID) != null && grvTraCuu.GetFocusedRowCellValue(colID).ToString() != "")
            {
                _id =Convert.ToInt32(grvTraCuu.GetFocusedRowCellValue(colID));//.ToString();

            }
        }

       private class NXT2
       {
           private int MaKP;

           public int MaKP1
           {
               get { return MaKP; }
               set { MaKP = value; }
           }
           private int MaDV;

           public int MaDV1
           {
               get { return MaDV; }
               set { MaDV = value; }
           }
           private string TenDV;

           public string TenDV1
           {
               get { return TenDV; }
               set { TenDV = value; }
           }
           private double DonGia;

           public double DonGia1
           {
               get { return DonGia; }
               set { DonGia = value; }
           }
           private double Ton1;

           public double Ton11
           {
               get { return Ton1; }
               set { Ton1 = value; }
           }
           private double Ton2;

           public double Ton21
           {
               get { return Ton2; }
               set { Ton2 = value; }
           }
           private double Ton3;

           public double Ton31
           {
               get { return Ton3; }
               set { Ton3 = value; }
           }
           private double Ton4;

           public double Ton41
           {
               get { return Ton4; }
               set { Ton4 = value; }
           }
           private double Ton5;

           public double Ton51
           {
               get { return Ton5; }
               set { Ton5 = value; }
           }
           private double Ton6;

           public double Ton61
           {
               get { return Ton6; }
               set { Ton6 = value; }
           }
           private double Ton7;

           public double Ton71
           {
               get { return Ton7; }
               set { Ton7 = value; }
           }
           private double Ton8;

           public double Ton81
           {
               get { return Ton8; }
               set { Ton8 = value; }
           }
           private double Ton9;

           public double Ton91
           {
               get { return Ton9; }
               set { Ton9 = value; }
           }
           private double Ton10;

           public double Ton101
           {
               get { return Ton10; }
               set { Ton10 = value; }
           }
           private double TonTong;

           public double TonTong1
           {
               get { return TonTong; }
               set { TonTong = value; }
           }
           
       }
       private void btnTimKiem2_Click(object sender, EventArgs e)
       {
           _NXT.Clear();
           _dttu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
           _dtden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
           _ncc.Clear(); _kho.Clear(); _nhomdv.Clear(); _tieunhomdv.Clear(); _dv.Clear(); _dg.Clear();
           int _sd = 1; 
           if (cmbTD2.EditValue=="1. Nhập - Xuất") { _sd = 1; }
           if (cmbTD2.EditValue == "2. Nhập - Sử dụng") { _sd = 2; }
           if (cmbTD2.EditValue == "3. Nhập - Xuất - Sử dụng") { _sd = 3; }
           if (KT2())
           {
               #region Lấy list các dữ liệu theo tham số chọn
               
              if (!string.IsNullOrEmpty(lupNhomDV2.Text))
               {
                   if (lupNhomDV2.EditValue != null && lupNhomDV2.EditValue.ToString() != "")
                   {
                       _nhomdv.Add(new nhomdv { IdNhom1 = idnhom });
                   }
               }
               else
               {
                   var nhomdv = _data.NhomDVs.Where(p => p.Status == 1).Select(p => new { p.IDNhom }).ToList();
                   if (nhomdv.Count > 0)
                   {
                       foreach (var a in nhomdv)
                       {
                           _nhomdv.Add(new nhomdv { IdNhom1 = a.IDNhom });
                       }
                   }
               }
               if (!string.IsNullOrEmpty(lupTieuNhomDV2.Text))
               {
                   if (lupTieuNhomDV2.EditValue != null && lupTieuNhomDV2.EditValue.ToString() != "")
                   {
                       idtnhom = Convert.ToInt32(lupTieuNhomDV2.EditValue);
                       _tieunhomdv.Add(new tieunhomdv { IDTieuNhom1 = idnhom });
                    }
               }
               else
               {
                   var tnhomdv = (from nhom in _data.NhomDVs.Where(p => p.Status == 1)
                                  join tnhom in _data.TieuNhomDVs on nhom.IDNhom equals tnhom.IDNhom
                                  group tnhom by new { tnhom.IdTieuNhom } into kq
                                  select new { kq.Key.IdTieuNhom }).ToList();
                   if (tnhomdv.Count > 0)
                   {
                       foreach (var a in tnhomdv)
                       {
                           _tieunhomdv.Add(new tieunhomdv { IDTieuNhom1 = a.IdTieuNhom });
                       }
                   }
               }
               var qdv1 = (from nduoc in _data.NhapDs
                           join nduocct in _data.NhapDcts on nduoc.IDNhap equals nduocct.IDNhap
                           join dv in _data.DichVus on nduocct.MaDV equals dv.MaDV
                           select new { nduoc.PLoai, nduoc.MaKP, dv.TenDV, dv.MaDV, dv.DonVi, nduocct.DonGia }).ToList();
               var qdv = (from dv in qdv1.Where(p => p.PLoai == 1 || p.PLoai == 2)
                          group dv by new { dv.TenDV, dv.MaDV, dv.DonGia } into kq
                          select new { kq.Key.MaDV, kq.Key.DonGia }).ToList();
                if (lupMaDV2.EditValue != null)
                   {
                       madv = Convert.ToInt32( lupMaDV2.EditValue);
                       _dv.Add(new dichvu { MaDV1 = madv });
                    }
               
               else
               {

                   var dvu = qdv.Select(p => new { p.MaDV }).ToList();
                   if (dvu.Count > 0)
                   {
                       foreach (var a in dvu)
                       {
                           _dv.Add(new dichvu { MaDV1 = a.MaDV });
                       }
                   }
                   _dv.Add(new dichvu { MaDV1 = 0 });
               }
               if (!string.IsNullOrEmpty(cboDonGia2.Text))
               {
                   dg = Convert.ToDouble(cboDonGia2.EditValue);

                   {
                       _dg.Add(new dongia { DonGia1 = dg });
                   }
               }
               else
               {

                   var dgia = qdv.Select(p => new { p.DonGia }).ToList();
                   if (dgia.Count > 0)
                   {
                       foreach (var a in dgia)
                       { _dg.Add(new dongia { DonGia1 = Convert.ToDouble(a.DonGia) }); }
                   }
               }
               #endregion
               List < NXT2 > _lnxt2 = new List<NXT2>();
               #region Lấy dữ liệu
               var qnhapd = (from nd in _data.NhapDs
                             join ndct in _data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                             select new { nd.MaKP, nd.PLoai, nd.NgayNhap, ndct.MaCC, ndct.MaDV, nd.IDNhap, nd.SoCT, ndct.SoLo, ndct.HanDung, ndct.DonVi, ndct.DonGiaCT, ndct.DonGia, ndct.SoLuongN, ndct.ThanhTienN, ndct.ThanhTienX, ndct.SoLuongX, ndct.ThanhTienSD, ndct.SoLuongSD }).ToList();
               var qnhom = (from dv in _data.DichVus
                            join ndv in _data.NhomDVs on dv.IDNhom equals ndv.IDNhom
                            join tndv in _data.TieuNhomDVs on dv.IdTieuNhom equals tndv.IdTieuNhom
                            select new { dv.MaDV, dv.TenDV, ndv.IDNhom, tndv.IdTieuNhom, }).ToList();
               var qnxt = (from nd in qnhapd
                           join nhom in qnhom on nd.MaDV equals nhom.MaDV
                           select new { nd.MaKP, nd.PLoai, nd.NgayNhap, nd.MaCC, nd.MaDV, nhom.TenDV, nhom.IDNhom, nhom.IdTieuNhom, nd.IDNhap, nd.SoCT, nd.SoLo, nd.HanDung, nd.DonVi, nd.DonGiaCT, nd.DonGia, nd.SoLuongN, nd.ThanhTienN, nd.ThanhTienX, nd.SoLuongX, nd.ThanhTienSD, nd.SoLuongSD }).ToList();
         
               var nxt2 = (from a in _nhomdv
                           join q in qnxt on a.IdNhom1 equals q.IDNhom
                           select new { q.MaKP, q.NgayNhap, q.MaDV, q.IDNhom, q.IdTieuNhom, q.IDNhap, q.SoLuongN, q.SoLuongX,q.SoLuongSD, q.DonGia,q.TenDV }).ToList();
               var nxt3 = (from a in _tieunhomdv
                           join q in nxt2 on a.IDTieuNhom1 equals q.IdTieuNhom
                           select new { q.MaKP,  q.NgayNhap, q.MaDV, q.IDNhom, q.IdTieuNhom, q.IDNhap, q.SoLuongN, q.SoLuongX, q.SoLuongSD, q.DonGia, q.TenDV }).ToList();
               var nxt4 = (from a in _dv
                           join q in nxt3 on a.MaDV1 equals q.MaDV
                           select new { q.MaKP, q.NgayNhap, q.MaDV, q.IDNhom, q.IdTieuNhom, q.IDNhap, q.SoLuongN, q.SoLuongX, q.SoLuongSD, q.DonGia, q.TenDV }).ToList();
               var nxt5 = (from a in _dg
                           join q in nxt4 on a.DonGia1 equals q.DonGia
                           select new { q.MaKP, q.NgayNhap, q.MaDV, q.DonGia, q.TenDV, q.SoLuongN, q.SoLuongSD, q.SoLuongX }).ToList();

               var nxt = (from nd in nxt5.Where(p => p.NgayNhap >= _dttu).Where(p => p.NgayNhap <= _dtden)
                          group nd by new { nd.DonGia,nd.MaKP,nd.MaDV,nd.TenDV } into kq
                          select new
                          {
                              MaKP = kq.Key.MaKP,
                              DonGia = kq.Key.DonGia,
                              MaDV=kq.Key.MaDV,
                              TenDV=kq.Key.TenDV,
                              SLTon = _sd == 1 ? kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX) : (_sd == 2 ? kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongSD) : kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX) - kq.Sum(p => p.SoLuongSD))
                          }).ToList();
               var nxtx = (from nd in nxt5.Where(p => p.NgayNhap >= _dttu).Where(p => p.NgayNhap <= _dtden)
                          group nd by new { nd.MaDV, nd.TenDV } into kq
                          select new
                          {
                              MaDV = kq.Key.MaDV,
                              TenDV = kq.Key.TenDV,
                              SLTon = _sd == 1 ? kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX) : (_sd == 2 ? kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongSD) : kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX) - kq.Sum(p => p.SoLuongSD))
                          }).ToList();
               if (nxtx.Count > 0)
               {
                   foreach (var a in nxtx.Where(p=>p.SLTon>0))
                   {
                       _lnxt2.Add(new NXT2 { MaDV1 = a.MaDV == null ? 0 : a.MaDV.Value, TenDV1 = a.TenDV });
                   }

               }
               if (_lnxt2.Count > 0)
               {
                   foreach (var n in _lnxt2)
                   {
                       foreach (var m in nxt)
                       {
                           if (n.MaDV1 == m.MaDV)
                           {
                               n.TonTong1 =n.TonTong1+ Convert.ToDouble(m.SLTon);
                               if (m.SLTon != null && m.SLTon != 0)
                               {
                                   for (int i = 0; i < _lkp.Count; i++)
                                   {
                                       if (m.MaKP == _lkp.Skip(i).First().MaKP)
                                       {
                                           switch (i)
                                           {
                                               case 0:
                                                   n.Ton11 =n.Ton11+ Convert.ToDouble(m.SLTon);
                                                   break;
                                               case 1:
                                                   n.Ton21 =n.Ton21+ Convert.ToDouble(m.SLTon);
                                                   break;
                                               case 2:
                                                   n.Ton31 =n.Ton31+ Convert.ToDouble(m.SLTon);
                                                   break;
                                               case 3:
                                                   n.Ton41 =n.Ton41+ Convert.ToDouble(m.SLTon);
                                                   break;
                                               case 4:
                                                   n.Ton51 =n.Ton51+ Convert.ToDouble(m.SLTon);
                                                   break;
                                               case 5:
                                                   n.Ton61 =n.Ton61+ Convert.ToDouble(m.SLTon);
                                                   break;
                                               case 6:
                                                   n.Ton71 =n.Ton71+ Convert.ToDouble(m.SLTon);
                                                   break;
                                               case 7:
                                                   n.Ton81 = n.Ton81+Convert.ToDouble(m.SLTon);
                                                   break;
                                               case 8:
                                                   n.Ton91 =n.Ton91+ Convert.ToDouble(m.SLTon);
                                                   break;
                                               case 9:
                                                   n.Ton101 =n.Ton101+ Convert.ToDouble(m.SLTon);
                                                   break;
                                           }

                                       }
                                       
                                   }
                                  
                               }
                               
                           }
                       }

                   }
               }
              
               grcKP.DataSource = _lnxt2.OrderBy(p => p.TenDV1).ToList();
               #endregion
           }
       }

     
       private void simpleButton3_Click(object sender, EventArgs e)
       {
           this.Close();
       }

       private void grvKP_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
       {
           if (e.Column == colSTT)
           {
               e.DisplayText = Convert.ToString(e.RowHandle + 1);
           }
       }

       private void lupNhomDV2_EditValueChanged(object sender, EventArgs e)
       {
           if (lupNhomDV2.EditValue != null && lupNhomDV2.EditValue.ToString() != "")
               idnhom2 = Convert.ToInt32(lupNhomDV2.EditValue);
           var qdv = (from nduoc in _data.NhapDs
                      join nduocct in _data.NhapDcts on nduoc.IDNhap equals nduocct.IDNhap
                      join dv in _data.DichVus on nduocct.MaDV equals dv.MaDV
                      join tnhom in _data.TieuNhomDVs on dv.IdTieuNhom equals tnhom.IdTieuNhom
                      select new { tnhom.IDNhom, tnhom.IdTieuNhom, tnhom.TenTN, dv.TenDV, dv.MaDV }).ToList();
           var tieunhom = (from nd in qdv.Where(p => p.IDNhom == idnhom2)
                           group nd by new { nd.IdTieuNhom, nd.TenTN } into kq
                           select new { kq.Key.IdTieuNhom, kq.Key.TenTN }).ToList();
           lupTieuNhomDV2.Properties.DataSource = tieunhom.ToList();
           var dvu = (from d in qdv.Where(p => p.IDNhom == idnhom)
                      group d by new { d.TenDV, d.MaDV } into kq
                      select new { kq.Key.MaDV, kq.Key.TenDV }).ToList();
           lupMaDV2.Properties.DataSource = dvu.ToList();
       }

       private void lupTieuNhomDV2_EditValueChanged(object sender, EventArgs e)
       {
           if (lupTieuNhomDV2.EditValue != null && lupTieuNhomDV2.EditValue.ToString() != "")
               idtnhom2 = Convert.ToInt32(lupTieuNhomDV2.EditValue);
           var qdv = (from nduoc in _data.NhapDs
                      join nduocct in _data.NhapDcts on nduoc.IDNhap equals nduocct.IDNhap
                      join dv in _data.DichVus.Where(p => p.IdTieuNhom == idtnhom2) on nduocct.MaDV equals dv.MaDV
                      select new { dv.TenDV, dv.MaDV, dv.DonVi, dv.IdTieuNhom }).ToList();
           var dvu = (from d in qdv.Where(p => p.IdTieuNhom == idtnhom2)
                      group d by new { d.TenDV, d.MaDV } into kq
                      select new { kq.Key.MaDV, kq.Key.TenDV }).ToList();
           lupMaDV2.Properties.DataSource = dvu.ToList();
       }

       private void lupMaDV2_EditValueChanged(object sender, EventArgs e)
       {
           cboDonGia2.Text = "";
           for (int a = 0; a < cboDonGia2.Properties.Items.Count; a++)
           {
               cboDonGia2.Properties.Items.RemoveAt(a);
           }
           if (lupMaDV2.EditValue != null)
           {
               madv2 = Convert.ToInt32( lupMaDV2.EditValue);
               var gia = (from nhapduoc in _data.NhapDcts.Where(p => p.MaDV==madv2)
                          join nduoc in _data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.MaKP== makho) on nhapduoc.IDNhap equals nduoc.IDNhap
                          group new { nhapduoc } by new { nhapduoc.DonGia } into kq
                          select new { kq.Key.DonGia }).ToList();
               if (gia.Count > 0)
               {
                   foreach (var g in gia)
                   {
                       cboDonGia2.Properties.Items.Add(g.DonGia);
                   }
               }
           }
       }

       private void groupControl1_Paint(object sender, PaintEventArgs e)
       {

       }

     

   
    }
}