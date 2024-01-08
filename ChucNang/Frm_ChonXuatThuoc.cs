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
    public partial class Frm_ChonXuatThuoc : DevExpress.XtraEditors.XtraForm
    {
        public Frm_ChonXuatThuoc()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void Frm_ChonXuatThuoc_Load(object sender, EventArgs e)
        {
            var kp = (from k in _Data.KPhongs.Where(p => p.PLoai== ("Khoa dược")) select new { k.TenKP, k.MaKP }).ToList();
            if (kp.Count > 0)
            {
                lupKho.Properties.DataSource = kp;
            }

        }
        private class DS
        {
            private string MaQD1;
            private int MaDV1;
            private string TenDV1;
            private string DonGia1;
            private string DonVi1;
            private string TenNCC1;
            private string SLT1;
            private bool chon;
            public string MaQD
            { set { MaQD1 = value; } get { return MaQD1; } }
            public int MaDV
            { set { MaDV1 = value; } get { return MaDV1; } }
            public string TenDV
            { set { TenDV1 = value; } get { return TenDV1; } }
            public string DonGia
            { set { DonGia1 = value; } get { return DonGia1; } }
            public string DonVi
            { set { DonVi1 = value; } get { return DonVi1; } }
            public string TenNCC
            { set { TenNCC1 = value; } get { return TenNCC1; } }
            public string SLT
            { set { SLT1 = value; } get { return SLT1; } }
            public bool Chon
            { set { chon = value; } get { return chon; } }
        }
        List<DS> _DS = new List<DS>();
        private void sbtTimkiem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lupKho.Text))
            {
                MessageBox.Show("Bạn cần chọn kho dược!");
            }
            else
            {
                int _MaKP = Convert.ToInt32( lupKho.EditValue);
                _DS.Clear();
                var dm = (from nd in _Data.NhapDs.Where(p => p.MaKP == _MaKP)
                          join ndct in _Data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                          join dv in _Data.DichVus on ndct.MaDV equals dv.MaDV
                          join ncc in _Data.NhaCCs on dv.MaCC equals ncc.MaCC
                          group new { dv, ndct, nd } by new { dv.MaQD, dv.TenDV, dv.MaDV, ndct.DonGia, ndct.DonVi, ncc.TenCC, dv.Status } into kp
                          select new
                          {
                              MaQD = kp.Key.MaQD,
                              MaDV = kp.Key.MaDV,
                              TenDV = kp.Key.TenDV,
                              DonGia = kp.Key.DonGia,
                              DonVi = kp.Key.DonVi,
                              TenNCC = kp.Key.TenCC,
                              Chon = kp.Key.Status,
                              SLT = kp.Sum(p => p.ndct.SoLuongN) - kp.Sum(p => p.ndct.SoLuongX),
                          }).ToList();
                if (dm.Count > 0)
                {
                    foreach (var a in dm)
                    {
                        DS themmoi = new DS();
                        if (a.Chon == 1)
                        {
                            themmoi.Chon = true;
                        }
                        else
                        { themmoi.Chon = false; }
                        themmoi.DonGia = a.DonGia.ToString();
                        themmoi.DonVi = a.DonVi;
                        themmoi.MaDV = a.MaDV;
                        themmoi.MaQD = a.MaQD;
                        themmoi.SLT = a.SLT.ToString();
                        themmoi.TenDV = a.TenDV;
                        themmoi.TenNCC = a.TenNCC;
                        _DS.Add(themmoi);
                    }
                    grcDichVu.DataSource = _DS;
                }

            }
        }

        private void sbtCapNatQD_Click(object sender, EventArgs e)
        {
            var DichVu = (from dv in _Data.DichVus.Where(p => p.PLoai == 1) group new { dv } by new { dv.TenDV } into kl select new {kl.Key.TenDV }).ToList();
            if (DichVu.Count > 0)
            {
                foreach (var a in DichVu)
                {
                    var MaDV = (from dv1 in _Data.DichVus.Where(p => p.TenDV== (a.TenDV)) select new { dv1.MaDV, dv1.MaQD }).ToList();
                    if (MaDV.Count > 0)
                    {
                        int i = -1;
                        string _MaQD = "";
                        foreach (var b in MaDV)
                        {
                            if (!string.IsNullOrEmpty(b.MaQD))
                            { i = i + 1; }
                        }
                        if (i > -1)
                        {
                            _MaQD = MaDV.Skip(i).First().MaQD;
                        }
                        else
                        {
                            int m = 0;
                           for(int n =0; n<1000; n++)
                           {
                                 string _maqd="Vssoft"+n;
                                 var Ten = (from dv in _Data.DichVus.Where(p => p.MaQD.Contains(_maqd)) select new { dv.MaQD }).ToList();
                                 if (Ten.Count == 0)
                                 {
                                     m = n;
                                     break;
                                 }
                           }
                           _MaQD = "Vssoft" + m;
                        }
                        foreach (var c in MaDV)
                        {
                            var sua = _Data.DichVus.Single(p => p.MaDV== (c.MaDV));
                            sua.MaQD = _MaQD;
                            _Data.SaveChanges();
                        }
                    }
                }
            }
            MessageBox.Show("Đã cập nhật thành công!");
        }

        private void sbtThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sbtLuu_Click(object sender, EventArgs e)
        {
            foreach (var a in _DS)
            {
                int TT = 0;
                if (a.Chon == true)
                {
                    TT = 1;
                }
                else
                { TT = 0; }
                var sua = _Data.DichVus.Single(p => p.MaDV == a.MaDV);
                sua.Status = TT;
                _Data.SaveChanges();
            }
            MessageBox.Show("Lưu thành công!");
        }


    }
}