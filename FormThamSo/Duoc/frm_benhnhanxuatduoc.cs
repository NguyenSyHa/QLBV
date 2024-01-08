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
    public partial class frm_benhnhanxuatduoc : DevExpress.XtraEditors.XtraForm
    {
        public frm_benhnhanxuatduoc()
        {
            InitializeComponent();
        }

        QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_benhnhanxuatduoc_Load(object sender, EventArgs e)
        {

            var q = from kp in Data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham || p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang) select new { kp.TenKP, kp.MaKP };
            lupKhoaphong.Properties.DataSource = q.ToList();
            lupNgaytu.DateTime = System.DateTime.Now;
            lupNgayden.DateTime = System.DateTime.Now;
            var dskp = (from kp in Data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc)
                        select new
                        {
                            MaKP = kp.MaKP,
                            TenKP = kp.TenKP
                        }).OrderBy(p => p.TenKP).ToList();
            
            cklKP.DataSource = dskp;
            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                if (cklKP.GetItemValue(i) != null && Convert.ToInt32(cklKP.GetItemValue(i)) == DungChung.Bien.MaKP)
                    cklKP.SetItemChecked(i, true);
                else
                    cklKP.SetItemChecked(i, false);
            }
        }
        public class _dsBN {
            DateTime ngayxuat;

            public DateTime Ngayxuat
            {
                get { return ngayxuat; }
                set { ngayxuat = value; }
            }
            private double tt;
            private int mabn;
            string tenbn;

            public string Tenbn
            {
                get { return tenbn; }
                set { tenbn = value; }
            }
            int maKP;

            public int MaKP
            {
                get { return maKP; }
                set { maKP = value; }
            }
            double tTD;

            public double TTD
            {
                get { return tTD; }
                set { tTD = value; }
            }
            double tTTT;

            public double TTTT
            {
                get { return tTTT; }
                set { tTTT = value; }
            }
            double dichvu;

            public double Dichvu
            {
                get { return dichvu; }
                set { dichvu = value; }
            }
            double thuoc;

            public double Thuoc
            {
                get { return thuoc; }
                set { thuoc = value; }
            }
            double vTYT;

            public double VTYT
            {
                get { return vTYT; }
                set { vTYT = value; }
            }
            public double ThanhTien {
                set { tt = value; }
                get { return tt; }
            }
            public int MaBNhan {
                set { mabn = value; }
                get { return mabn; }
            }
            private string tenThuoc;

            public string TenThuoc
            {
                get { return tenThuoc; }
                set { tenThuoc = value; }
            }
            private int maDV;

            public int MaDV
            {
                get { return maDV; }
                set { maDV = value; }
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DateTime _Ngaytu=DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime _Ngayden=DungChung.Ham.NgayDen(lupNgayden.DateTime);
            List<KPhong> _kpChon = new List<KPhong>();
            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                if (cklKP.GetItemChecked(i))
                    _kpChon.Add(new KPhong { TenKP = cklKP.GetItemText(i), MaKP = cklKP.GetItemValue(i) == null ? 0 : Convert.ToInt32(cklKP.GetItemValue(i)) });
            }
            if (_kpChon.Count() > 0)
            {
                var nduoc1 = (from nd in Data.NhapDs.Where(p => p.KieuDon == 0 || p.KieuDon == 4)
                              join ndct in Data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                              where (nd.NgayNhap >= _Ngaytu && nd.NgayNhap <= _Ngayden)
                              select new { nd.NgayNhap, nd.MaBNhan, ndct.MaDV,                                           
                                           SoLuongX = (nd.PLoai == 2 || nd.PLoai == 3) ? ndct.SoLuongX : 0,                                          
                                           ThanhTienX = (nd.PLoai == 2 || nd.PLoai == 3) ? ndct.ThanhTienX : 0,
                                   nd.MaKP }
                         ).ToList();
                var nduoc = (from a in nduoc1
                             join b in _kpChon on a.MaKP equals b.MaKP
                             select a).ToList();
                //var mabn = nduoc.Select(p => p.MaBNhan).Distinct().ToList();
                List<int> mabn = nduoc.Select(p => p.MaBNhan ?? 0).Distinct().ToList(); ///group a by a.MaBNhan into kq select new { MaBNhan = kq.Key.Value }).ToList();
                var bnhan = (from bn in Data.BenhNhans.Where(p => mabn.Contains(p.MaBNhan))
                             //    mabn
                             //join bn in Data.BenhNhans on ma equals bn.MaBNhan //.Where(p => p.NoiTru == 0)
                             select new { bn.TenBNhan, bn.MaBNhan, bn.MaKP }).ToList();
                var dvu = (from dv in Data.DichVus
                           join nhom in Data.NhomDVs on dv.IDNhom equals nhom.IDNhom
                           select new { dv.MaDV, nhom, dv.TenDV }).ToList();
                var vphi = (from vp in Data.VienPhis.Where(p => mabn.Contains(p.MaBNhan ?? 0))
                            //    mabn
                            //join vp in Data.VienPhis on ma equals vp.MaBNhan
                            select new { vp.MaBNhan, vp.idVPhi, vp.NgayTT }).ToList();
                DateTime t1 = _Ngaytu.AddDays(-10);
                DateTime t2 = _Ngayden.AddDays(10);
                var test = (from vp in Data.VienPhis.Where(p => p.NgayTT >= t1 && p.NgayTT <= t2)
                            join vpct in Data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                            select new { vpct.MaDV, vpct.ThanhTien, vp.idVPhi }).ToList();
                var vphict = (from vp in vphi
                              join vpct in test on vp.idVPhi equals vpct.idVPhi
                              select new { vp.MaBNhan, vpct.MaDV, vpct.ThanhTien, vp.NgayTT }).ToList();
                List<_dsBN> tienVP = (from vp in vphict
                                      join dv in dvu on vp.MaDV equals dv.MaDV
                                      join bn in bnhan on vp.MaBNhan equals bn.MaBNhan
                                      group new { vp, dv, bn } by new { vp.MaBNhan, bn.TenBNhan, bn.MaKP, dv.MaDV, dv.TenDV, vp.NgayTT } into kp
                                      select new _dsBN

                                      {
                                          MaDV = kp.Key.MaDV,
                                          Tenbn = kp.Key.TenBNhan,
                                          MaKP = kp.Key.MaKP ?? 0,
                                          MaBNhan = kp.Key.MaBNhan ?? 0,
                                          TenThuoc = kp.Key.TenDV,
                                          TTD = 0.0,
                                          TTTT = kp.Sum(p => p.vp.ThanhTien),
                                          Dichvu = kp.Where(p => p.dv.nhom.Status == 2).Sum(p => p.vp.ThanhTien),
                                          Thuoc = kp.Where(p => p.dv.nhom.TenNhomCT.Contains("Thuốc")).Sum(p => p.vp.ThanhTien),
                                          VTYT = kp.Where(p => p.dv.nhom.TenNhomCT.Contains("Vật tư")).Sum(p => p.vp.ThanhTien),
                                      }).ToList();
                if (checkChenh.Checked == true)
                {
                    vphict = (from vp in Data.VienPhis.Where(p => p.NgayTT >= _Ngaytu && p.NgayTT <= _Ngayden)
                              join vpct in Data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                              select new
                              {
                                  vp.MaBNhan,
                                  vpct.MaDV,
                                  vpct.ThanhTien,
                                  vp.NgayTT
                              }).ToList();
                    tienVP = (from vp in vphict
                              join dv in dvu on vp.MaDV equals dv.MaDV
                              join bn in Data.BenhNhans.Where(p => p.NoiTru == 0) on vp.MaBNhan equals bn.MaBNhan
                              group new { vp, dv, bn } by new { vp.MaBNhan, bn.TenBNhan, bn.MaKP, dv.MaDV, dv.TenDV, vp.NgayTT } into kp
                              select new _dsBN

                              {
                                  MaDV = kp.Key.MaDV,
                                  Tenbn = kp.Key.TenBNhan,
                                  MaKP = kp.Key.MaKP ?? 0,
                                  MaBNhan = kp.Key.MaBNhan ?? 0,
                                  Ngayxuat = kp.Key.NgayTT == null ? DateTime.Now : kp.Key.NgayTT.Value.Date,
                                  TenThuoc = kp.Key.TenDV,
                                  TTD = 0.0,
                                  TTTT = kp.Sum(p => p.vp.ThanhTien),
                                  Dichvu = kp.Where(p => p.dv.nhom.Status == 2).Sum(p => p.vp.ThanhTien),
                                  Thuoc = kp.Where(p => p.dv.nhom.TenNhomCT.Contains("Thuốc")).Sum(p => p.vp.ThanhTien),
                                  VTYT = kp.Where(p => p.dv.nhom.TenNhomCT.Contains("Vật tư")).Sum(p => p.vp.ThanhTien),
                              }).ToList();
                }

                List<_dsBN> d = (from nd in nduoc
                                 join dv in dvu on nd.MaDV equals dv.MaDV
                                 join bn in bnhan on nd.MaBNhan equals bn.MaBNhan
                                 group new { bn, nd, dv } by new { nd.NgayNhap, bn.TenBNhan, bn.MaBNhan, bn.MaKP, dv.MaDV, dv.TenDV } into kp
                                 select new _dsBN
                                 {
                                     MaDV = kp.Key.MaDV,
                                     Ngayxuat = kp.Key.NgayNhap == null ? DateTime.Now : kp.Key.NgayNhap.Value.Date,
                                     Tenbn = kp.Key.TenBNhan,
                                     MaKP = kp.Key.MaKP ?? 0,
                                     TenThuoc = kp.Key.TenDV,
                                     MaBNhan = kp.Key.MaBNhan,
                                     TTD = kp.Sum(p => p.nd.ThanhTienX),
                                     Dichvu = 0.0,
                                     Thuoc = 0.0,
                                     VTYT = 0.0,
                                 }).OrderBy(p => p.Tenbn).ToList();
                List<_dsBN> d_s = d.Concat(tienVP).ToList();
                d_s = d_s.ToList();
                if (checkChenh.Checked == false)
                {
                    var data_s = (from a in d_s
                                  group a by new { a.MaBNhan, a.MaKP, a.Tenbn } into kq
                                  select new _dsBN
                                  {
                                      Ngayxuat = kq.Max(p => p.Ngayxuat),
                                      MaBNhan = kq.Key.MaBNhan,
                                      Tenbn = kq.Key.Tenbn,
                                      MaKP = kq.Key.MaKP,
                                      TTD = kq.Sum(p => p.TTD),
                                      TTTT = kq.Sum(p => p.TTTT),
                                      ThanhTien = kq.Sum(p => p.Thuoc) - kq.Sum(p => p.TTD),
                                      Dichvu = kq.Sum(p => p.Dichvu),
                                      Thuoc = kq.Sum(p => p.Thuoc),
                                      VTYT = kq.Sum(p => p.VTYT),
                                  }).OrderBy(p => p.Ngayxuat).ToList();

                    frmIn frm = new frmIn();
                    BaoCao.rep_Benhnhanxuatduoc rep = new BaoCao.rep_Benhnhanxuatduoc();
                    rep.Ngaythang.Value = "Từ ngày: " + lupNgaytu.Text.Substring(0, 10) + "  Đến ngày: " + lupNgayden.Text.Substring(0, 10);

                    int makp = 0;
                    makp = lupKhoaphong.EditValue == null ? 0 : Convert.ToInt32(lupKhoaphong.EditValue);
                    rep.DataSource = data_s.Where(p => (makp == 0 ? true : p.MaKP == makp)).ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    //rep.DataMember = "Table";
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    var data_s = (from a in d_s
                                  group a by new { a.MaBNhan, a.MaKP, a.Tenbn, a.MaDV, a.TenThuoc } into kq
                                  select new _dsBN
                                  {
                                      Ngayxuat = kq.Max(p => p.Ngayxuat),
                                      MaBNhan = kq.Key.MaBNhan,
                                      Tenbn = kq.Key.Tenbn,
                                      TenThuoc = kq.Key.TenThuoc,
                                      MaDV = kq.Key.MaBNhan,
                                      MaKP = kq.Key.MaKP,
                                      TTD = kq.Sum(p => p.TTD),
                                      TTTT = kq.Sum(p => p.TTTT),
                                      ThanhTien = Math.Round(kq.Sum(p => p.Thuoc) - kq.Sum(p => p.TTD) + kq.Sum(p => p.VTYT), 0),
                                      Dichvu = kq.Sum(p => p.Dichvu),
                                      Thuoc = kq.Sum(p => p.Thuoc),
                                      VTYT = kq.Sum(p => p.VTYT),
                                  }).ToList();
                    int makp = 0;
                    makp = lupKhoaphong.EditValue == null ? 0 : Convert.ToInt32(lupKhoaphong.EditValue);
                    data_s = data_s.Where(p => p.ThanhTien != 0).Where(p => (makp == 0 ? true : p.MaKP == makp)).ToList();
                    frmIn frm = new frmIn();
                    BaoCao.rep_Benhnhanxuatduoc_New rep = new BaoCao.rep_Benhnhanxuatduoc_New();
                    rep.Ngaythang.Value = "Từ ngày: " + lupNgaytu.Text.Substring(0, 10) + "  Đến ngày: " + lupNgayden.Text.Substring(0, 10);
                    rep.DataSource = data_s.OrderBy(p => p.Ngayxuat).ThenBy(p => p.Tenbn).ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    //rep.DataMember = "Table";
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn kho");

            }
            
            
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cklKP_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {

        }
    }
}