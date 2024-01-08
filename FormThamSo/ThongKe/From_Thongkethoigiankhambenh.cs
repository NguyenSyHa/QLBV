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
    public partial class From_Thongkethoigiankhambenh : DevExpress.XtraEditors.XtraForm
    {
        public From_Thongkethoigiankhambenh()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        class DoiTuong
        {
            public string DTBN1 { get; set; }
            public byte IDDTBN { get; set; }
        }
        private void From_Thongkethoigiankhambenh_Load(object sender, EventArgs e)
        {
            List<DoiTuong> dtbns = new List<DoiTuong>();
            tungay.DateTime = DateTime.Now;
            denngay.DateTime = DateTime.Now;
            DoiTuong dtbn1 = new DoiTuong();
            dtbn1.DTBN1 = "Tất cả";
            dtbn1.IDDTBN = 0;
            dtbns.Add(dtbn1);
            var dt = (from h in data.DTBNs select new  {  h.DTBN1,  h.IDDTBN }).ToList();
            foreach (var item in dt)
            {
                DoiTuong dtbn2 = new DoiTuong();
                dtbn2.DTBN1 = item.DTBN1;
                dtbn2.IDDTBN = item.IDDTBN;
                dtbns.Add(dtbn2);
            }     
            doituong.Properties.DataSource = dtbns.OrderBy(p => p.IDDTBN);
        }
        public double chuyenngay(DateTime tungays, DateTime denngays)
        {
            System.TimeSpan db1 = denngays.Subtract(tungays);
            double days = db1.TotalHours;
            return days;
        }
        private class ds
        {
            private int? noiTru;
            public string NgayVaoVien { get; set; }
            public int? NoiTru
            {
                get { return noiTru; }
                set { noiTru = value; }
            }
            private int maBNhan;

            public int MaBNhan
            {
                get { return maBNhan; }
                set { maBNhan = value; }
            }
            private string tenBNhan, dTuong, gTinh, cKhoa, cSLTH, namSinh;

            public string NamSinh
            {
                get { return namSinh; }
                set { namSinh = value; }
            }

            public string CSLTH
            {
                get { return cSLTH; }
                set { cSLTH = value; }
            }

            public string CKhoa
            {
                get { return cKhoa; }
                set { cKhoa = value; }
            }

            public string GTinh
            {
                get { return gTinh; }
                set { gTinh = value; }
            }

            public string DTuong
            {
                get { return dTuong; }
                set { dTuong = value; }
            }

            public string TenBNhan
            {
                get { return tenBNhan; }
                set { tenBNhan = value; }
            }
            private DateTime? nNhap, ngayTT;

            public DateTime? NNhap
            {
                get { return nNhap; }
                set { nNhap = value; }
            }

            public DateTime? NgayTT
            {
                get { return ngayTT; }
                set { ngayTT = value; }
            }
            private double giokham;

            public double Giokham
            {
                get { return giokham; }
                set { giokham = value; }
            }
            private int solan;

            public int Solan
            {
                get { return solan; }
                set { solan = value; }
            }
        }
        List<ds> _ds = new List<ds>();
        public void ham_thongketgkb(int a)
        {
            _ds.Clear();
            DateTime tungays = DungChung.Ham.NgayTu(tungay.DateTime);
            DateTime denngays = DungChung.Ham.NgayDen(denngay.DateTime);
            DungChung.Bien.c_chuyenkhoa.f_ChuyenKhoa();
            var bnkb = (from c in DungChung.Bien._lChuyenKhoa
                        join b in data.BNKBs on c.MaCK equals b.MaCK
                        join bn in data.BenhNhans.Where(p => (a == 0 || a == null) ? true : p.IDDTBN == a) on b.MaBNhan equals bn.MaBNhan
                        group new { c, b, bn } by new { bn.MaBNhan, bn.TenBNhan, bn.NamSinh, bn.NNhap, bn.DTuong, bn.IDDTBN, bn.NoiTru, bn.GTinh } into kq
                        select new { kq.Key.MaBNhan, kq.Key.TenBNhan, kq.Key.NamSinh, kq.Key.NNhap, kq.Key.DTuong, kq.Key.IDDTBN, kq.Key.NoiTru, kq.Key.GTinh, ChuyenKhoa1 = String.Join(", ", kq.Select(p => p.c.ChuyenKhoa)) }).ToList();
            foreach (var item in bnkb)
            {
                ds moi = new ds();
                moi.MaBNhan = item.MaBNhan;
                moi.TenBNhan = item.TenBNhan;
                moi.NamSinh = item.NamSinh;
                moi.NNhap = item.NNhap;
                moi.DTuong = item.DTuong;
                moi.NoiTru = item.NoiTru;
                moi.GTinh = item.GTinh == 0 ? "Nữ" : "Nam";
                moi.CKhoa = item.ChuyenKhoa1;
                _ds.Add(moi);
            }
            var vp = (from aa in data.VienPhis.Where(p => p.NgayTT >= tungays && p.NgayTT <= denngays)
                      join b in data.VienPhicts on aa.idVPhi equals b.idVPhi
                      select new { aa.NgayTT, aa.idVPhi, b.MaDV, aa.MaBNhan }).ToList();

            var vienphi = (from h in bnkb
                           join h1 in vp on h.MaBNhan equals h1.MaBNhan
                           select new { h.MaBNhan, h.TenBNhan, h.NamSinh, h.NNhap, h1.NgayTT, h.DTuong, h1.idVPhi, h.NoiTru, h.GTinh, h1.MaDV }).ToList();
            var benhnhan = (from h in vienphi
                            group h by new { h.MaBNhan, h.TenBNhan, h.NamSinh, h.NNhap, h.NgayTT, h.DTuong, h.idVPhi, h.NoiTru, h.GTinh } into kq
                            select new { kq.Key.MaBNhan, kq.Key.TenBNhan, kq.Key.NamSinh, kq.Key.NNhap, kq.Key.NgayTT, kq.Key.DTuong, kq.Key.idVPhi, kq.Key.NoiTru, kq.Key.GTinh }).ToList();
            //var vienphi = (from h in benhnhan 
            //               join h1 in data.VienPhicts on h.idVPhi equals h1.idVPhi  
            //               select new { h.MaBNhan, h.TenBNhan, h.NamSinh, h.NNhap,h.DTuong,h1.idVPhi, h1.MaDV }).ToList();

            var dichvu = (from dichvu1 in data.DichVus
                          join tieunhom in data.TieuNhomDVs on dichvu1.IdTieuNhom equals tieunhom.IdTieuNhom
                          join nhom in data.NhomDVs on tieunhom.IDNhom equals nhom.IDNhom
                          where (nhom.TenNhomCT == "Xét nghiệm" || nhom.TenNhomCT == "Chẩn đoán hình ảnh")
                          select new { dichvu1.MaDV, dichvu1.IdTieuNhom, dichvu1.TenDV }).ToList();

            var ketqua1 = (from h in vienphi
                           join h1 in dichvu on h.MaDV equals h1.MaDV
                           select new { h1.IdTieuNhom, h.MaBNhan, h1.TenDV }).ToList();

            var ketqua = (from h in vienphi
                          join h1 in dichvu on h.MaDV equals h1.MaDV
                          select new { h1.IdTieuNhom, h.MaBNhan, h1.TenDV })
                          .GroupBy(p => new { p.MaBNhan, p.IdTieuNhom }).Select(p => new { p.Key.MaBNhan, p.Key.IdTieuNhom }).ToList();
            foreach (var item in ketqua1)
            {
                if (_ds.Where(p => p.MaBNhan == item.MaBNhan).Count() > 0)
                {
                    ds moi = _ds.Single(p => p.MaBNhan == item.MaBNhan);
                    if (moi.CSLTH != null)
                        moi.CSLTH = moi.CSLTH + ", " + item.TenDV;
                    else
                        moi.CSLTH = item.TenDV;
                }
            }
            var k = (from k1 in ketqua group k1 by new { k1.MaBNhan } into h select new { mabenhn = h.Key.MaBNhan, solan = h.Count() }).ToList();

            var ketquatong = (from bn in benhnhan
                              join sl in k on bn.MaBNhan equals sl.mabenhn into th
                              from h in th.DefaultIfEmpty()
                              select new
                              {
                                  bn.GTinh,
                                  bn.NoiTru,
                                  bn.MaBNhan,
                                  bn.TenBNhan,
                                  bn.NamSinh,
                                  bn.DTuong,
                                  bn.NgayTT,
                                  bn.NNhap,
                                  solan = h == null ? 0 : h.solan
                              }).ToList().Select(p => new { p.MaBNhan, p.NoiTru, p.DTuong, p.NNhap, p.solan, p.NgayTT, p.TenBNhan, p.NamSinh, giokham = chuyenngay(p.NNhap.Value, p.NgayTT.Value) }).ToList();
            foreach (var item in ketquatong)
            {
                if (_ds.Where(p => p.MaBNhan == item.MaBNhan).Count() > 0)
                {
                    ds moi = _ds.Single(p => p.MaBNhan == item.MaBNhan);
                    moi.NgayTT = item.NgayTT;
                    moi.NNhap = item.NNhap;
                    moi.Solan = item.solan;
                    moi.Giokham = item.giokham;
                    if (moi.NoiTru == 1)
                    {
                        moi.NgayVaoVien = DungChung.Ham.NgaySangChu((DateTime)data.VaoViens.Where(p => p.MaBNhan == item.MaBNhan).First().NgayVao,7);
                    }
                }
            }
            _ds = _ds.Where(p => p.NgayTT != null).ToList();
            if (tatca.Checked == true)
            {
                BaoCao.Rep_Thongkethoigiankhamchuabenh baocaoth = new BaoCao.Rep_Thongkethoigiankhamchuabenh();
                baocaoth.DataSource = _ds;
                baocaoth.tun = tungay.DateTime.ToShortDateString();
                baocaoth.denn = denngay.DateTime.ToShortDateString();
                baocaoth.hambctgkb();
                baocaoth.CreateDocument();
                frmIn inbaoc = new frmIn();
                inbaoc.prcIN.PrintingSystem = baocaoth.PrintingSystem;
                inbaoc.ShowDialog();
            }
            else if (ngoaitru.Checked == true)
            {
                BaoCao.Rep_Thongkethoigiankhamchuabenh baocaoth = new BaoCao.Rep_Thongkethoigiankhamchuabenh();
                baocaoth.DataSource = _ds.Where(p => p.NoiTru == 0).OrderBy(p => p.MaBNhan);
                baocaoth.tun = tungay.DateTime.ToShortDateString();
                baocaoth.denn = denngay.DateTime.ToShortDateString();
                baocaoth.hambctgkb();
                baocaoth.CreateDocument();
                frmIn inbaoc = new frmIn();
                inbaoc.prcIN.PrintingSystem = baocaoth.PrintingSystem;
                inbaoc.ShowDialog();
            }
            else if (noitru.Checked == true)
            {
                BaoCao.Rep_Thongkethoigiankhamchuabenh baocaoth = new BaoCao.Rep_Thongkethoigiankhamchuabenh();
                baocaoth.DataSource = _ds.Where(p => p.NoiTru == 1);
                baocaoth.tun = tungay.DateTime.ToShortDateString();
                baocaoth.denn = denngay.DateTime.ToShortDateString();
                baocaoth.hambctgkb();
                baocaoth.CreateDocument();
                frmIn inbaoc = new frmIn();
                inbaoc.prcIN.PrintingSystem = baocaoth.PrintingSystem;
                inbaoc.ShowDialog();
            }
            else if (ngoaitrumoi.Checked == true)
            {
                _ds = (from bn in _ds
                       join b in data.BenhNhans.Where(p => p.DTNT == false) on bn.MaBNhan equals b.MaBNhan
                       select bn).ToList();
                BaoCao.Rep_Thongkethoigiankhamchuabenh_New baocaoth = new BaoCao.Rep_Thongkethoigiankhamchuabenh_New();
                baocaoth.DataSource = _ds.Where(p => p.NoiTru == 0);
                baocaoth.tun = tungay.DateTime.ToShortDateString();
                baocaoth.denn = denngay.DateTime.ToShortDateString();
                baocaoth.hambctgkb();
                baocaoth.CreateDocument();
                frmIn inbaoc = new frmIn();
                inbaoc.prcIN.PrintingSystem = baocaoth.PrintingSystem;
                inbaoc.ShowDialog();
            }

        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (doituong.EditValue != null)
            {
                int id = Convert.ToInt32(doituong.EditValue.ToString());
                ham_thongketgkb(id);
            }
            else
            {
                MessageBox.Show("Chưa chọn đối tượng", "Thông báo");
            }

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}