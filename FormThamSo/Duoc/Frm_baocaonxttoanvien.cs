using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
namespace QLBV.FormNhap
{
    public partial class Frm_baocaonxttoanvien : DevExpress.XtraEditors.XtraForm
    {
        public Frm_baocaonxttoanvien()
        {
            InitializeComponent();
        }
        bool inbaocao;
        QLBV_Database.QLBVEntities data1 = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public DateTime chuyenngay(DateTime a, int b)
        {
            if (b == 0)
            {
                string tungay = a.ToShortDateString() + " " + "00:00:00";
                DateTime t = Convert.ToDateTime(tungay);
                return t;
            }
            else
            {
                string tungay = a.ToShortDateString() + " " + "23:59:59.998";
                DateTime t = Convert.ToDateTime(tungay);
                return t;
            }

        }
        string[] a = new string[] { "knt", "kbhnt" };
        public DateTime denngay(DateTime den)
        {
            string a = den.ToShortDateString() + " 23:59:59.998";
            return Convert.ToDateTime(a);

        }
        public DateTime tungay(DateTime den)
        {
            string a = den.ToShortDateString() + " 00:00:00";
            return Convert.ToDateTime(a);

        }
        public void hoc()
        {

            List<hhh> themmoi = new List<hhh>();
            string k1;
            for (int a = 0; a < gridView1.RowCount; a++)
            {

                //MessageBox.Show(gridView2.GetRowCellValue(a, "che").ToString());
                k1 = gridView1.GetRowCellValue(a, "che").ToString();
                if (k1 == "True")
                {
                    hhh th = new hhh();
                    th.ma = gridView1.GetRowCellValue(a, "MaKP") == null ? 0 : Convert.ToInt32(gridView1.GetRowCellValue(a, "MaKP"));
                    //MessageBox.Show(gridView2.GetRowCellValue(a, "MaKP").ToString());
                    themmoi.Add(th);
                }

            }
            string _macc = "";
            if (lup_MaCC.EditValue != null)
                _macc = lup_MaCC.EditValue.ToString();
            DateTime dn = DungChung.Ham.NgayDen(Dngay.DateTime);
            DateTime tn = DungChung.Ham.NgayTu(tngay.DateTime);

            var hkl = (from k in themmoi select new { k.ma }).ToList();
            var nhapd = (from h in data1.NhapDs.Where(p => p.NgayNhap <= dn)
                         join nhapdct in data1.NhapDcts on h.IDNhap equals nhapdct.IDNhap
                         select new { h.IDNhap, h.NgayNhap, h.KieuDon, h.XuatTD, h.MaKP, nhapdct.DonGia, nhapdct.DonVi, nhapdct.MaDV, nhapdct.SoLuongN, nhapdct.SoLuongX, nhapdct.ThanhTienN, nhapdct.ThanhTienX }).ToList();
            var ket = from h in hkl join h1 in nhapd on h.ma equals h1.MaKP select new { h1.MaKP, h1.IDNhap, h1.SoLuongN, h1.SoLuongX, h1.ThanhTienX, h1.ThanhTienN, h1.NgayNhap, h1.DonGia, h1.DonVi, h1.MaDV, h1.XuatTD, h1.KieuDon };
            var tondauky = (from h1 in ket.Where(p => p.NgayNhap < tn)
                            select new { h1.MaKP, h1.IDNhap, h1.SoLuongN, h1.SoLuongX, h1.ThanhTienX, h1.ThanhTienN, h1.NgayNhap, h1.MaDV, h1.DonVi, h1.DonGia })
                .GroupBy(p => new { p.MaDV, p.DonGia, p.DonVi })
                .Select(p => new { dkj3 = p.Key.MaDV + p.Key.DonGia, p.Key.MaDV, p.Key.DonVi, p.Key.DonGia, solton = p.Sum(k => k.SoLuongN) - p.Sum(k => k.SoLuongX) }).ToList();
            // nhấp xuất trong kỳ tính các nhấp xuất chuyển kho 
            var nhapxuattrongky = (from h1 in ket.Where(p => p.NgayNhap >= tn && p.NgayNhap <= dn)
                                   select new { h1.MaKP, h1.IDNhap, h1.SoLuongN, h1.SoLuongX, h1.ThanhTienX, h1.ThanhTienN, h1.NgayNhap, h1.MaDV, h1.DonVi, h1.DonGia })
                                   .GroupBy(p => new { p.MaDV, p.DonGia, p.DonVi }).Select(p => new { dkj3 = p.Key.MaDV + p.Key.DonGia, p.Key.MaDV, p.Key.DonVi, p.Key.DonGia, soluongn = p.Sum(k => k.SoLuongN), xuat = p.Sum(k => k.SoLuongX) }).ToList();
            // nhấp xuất trong ky tính cả nhập xuất chuyển kho 
            var nhaptrongkyktck = (from h1 in ket.Where(p => p.NgayNhap >= tn && p.NgayNhap <= dn && (p.KieuDon == 2 || p.XuatTD > 0))
                                   select new { h1.MaKP, h1.IDNhap, h1.SoLuongN, h1.SoLuongX, h1.ThanhTienX, h1.ThanhTienN, h1.NgayNhap, h1.MaDV, h1.DonVi, h1.DonGia })
                                   .GroupBy(p => new { p.MaDV, p.DonGia, p.DonVi }).Select(p => new { dkj3 = p.Key.MaDV + p.Key.DonGia, p.Key.MaDV, p.Key.DonVi, p.Key.DonGia, soluongnchuyenkho = p.Sum(k => k.SoLuongN), xuatchuenkho = p.Sum(k => k.SoLuongX) }).ToList();

            var toncuoiky = (from h1 in ket
                             select new { h1.MaKP, h1.IDNhap, h1.SoLuongN, h1.SoLuongX, h1.ThanhTienX, h1.ThanhTienN, h1.NgayNhap, h1.MaDV, h1.DonVi, h1.DonGia })
                             .GroupBy(p => new { p.MaDV, p.DonGia, p.DonVi })
                             .Select(p => new { dkj3 = p.Key.MaDV + p.Key.DonGia, p.Key.MaDV, p.Key.DonVi, p.Key.DonGia, soltonck = p.Sum(k => k.SoLuongN) - p.Sum(k => k.SoLuongX) }).ToList();
            var ketqua2 = (from h1 in toncuoiky join h2 in tondauky on h1.dkj3 equals h2.dkj3 into hk from k in hk.DefaultIfEmpty() join h3 in nhapxuattrongky on h1.dkj3 equals h3.dkj3 into kl from kc in kl.DefaultIfEmpty() join h4 in nhaptrongkyktck on h1.dkj3 equals h4.dkj3 into nxtck from nhapxuattonchuyenkho in nxtck.DefaultIfEmpty() select new { h1.MaDV, h1.DonVi, h1.DonGia, h1.soltonck, tondky = k == null ? 0 : k.solton, nhaptk = kc == null ? 0 : kc.soluongn, xuattk = kc == null ? 0 : kc.xuat, xuatchuyenkho = nhapxuattonchuyenkho == null ? 0 : nhapxuattonchuyenkho.xuatchuenkho, nhapchuyenkho = nhapxuattonchuyenkho == null ? 0 : nhapxuattonchuyenkho.soluongnchuyenkho }).ToList();
            var ketqua1 = (from kq2 in ketqua2
                           group kq2 by new { kq2.DonGia, kq2.DonVi, kq2.MaDV } into kq3
                           select new { kq3.Key.DonGia, kq3.Key.DonVi, kq3.Key.MaDV, nhapchuyenkho = kq3.Sum(p => p.nhapchuyenkho), nhaptk = kq3.Sum(p => p.nhaptk), soltonck = kq3.Sum(p => p.soltonck), tondky = kq3.Sum(p => p.tondky), xuatchuyenkho = kq3.Sum(p => p.xuatchuyenkho), xuattk = kq3.Sum(p => p.xuattk) }).ToList();
            var ketqua = (from k in ketqua1
                          join dichvu in data1.DichVus on k.MaDV equals dichvu.MaDV
                          join nhom in data1.NhomDVs on dichvu.IDNhom equals nhom.IDNhom
                          where _macc == "" ? true : dichvu.MaCC == _macc
                          select new
            {
                k.MaDV,
                dichvu.TenDV,
                nhom.TenNhom,
                dichvu.IDNhom,
                dichvu.IdTieuNhom,
                k.DonVi,
                k.DonGia,
                k.tondky,
                ttdk = k.tondky * k.DonGia,
                k.nhaptk,
                ttntk = k.nhaptk * k.DonGia,
                k.xuattk,
                ttxtk = k.xuattk * k.DonGia,
                k.soltonck,
                ttck = k.soltonck * k.DonGia,
                NCK = k.nhapchuyenkho,
                thanhtiennhapchuyenkho = k.nhapchuyenkho * k.DonGia
                ,
                xuatchuyenkhoa = k.xuatchuyenkho,
                thanhtienxuatchuyenkho = k.xuatchuyenkho * k.DonGia
                ,
                nhaptrongky1 = k.nhaptk - k.nhapchuyenkho
                ,
                thangtiennhaptrongky1 = (k.nhaptk - k.nhapchuyenkho) * k.DonGia
                ,
                xuattrongky1 = k.xuattk - k.xuatchuyenkho
                ,
                thanhtienxuattrongky1 = (k.xuattk - k.xuatchuyenkho) * k.DonGia
            }).Where(p => p.tondky != 0 || p.nhaptk != 0 || p.xuattk != 0 || p.soltonck != 0).ToList();
            if (checkEdit1.Checked == true)
            {
                BaoCao.Rep_baocaoNXTtonavienKTCK phieu = new BaoCao.Rep_baocaoNXTtonavienKTCK();
                if (radiotatca.Checked == true)
                {
                    if (inbaocao == true)
                    {

                        phieu.DataSource = ketqua.OrderBy(p => p.TenDV);
                    }
                    else
                    {

                        G_control.DataSource = ketqua.OrderBy(p => p.TenDV);
                    }
                }
                else if (radiochitet.Checked == true)
                {
                    if (inbaocao == true)
                    {
                        if (Nhomduoc.EditValue != null && Tieunhomduoc.EditValue != null)
                        {
                            phieu.DataSource = ketqua.Where(p => p.IDNhom == Convert.ToInt32(Nhomduoc.EditValue) && p.IdTieuNhom == Convert.ToInt32(Tieunhomduoc.EditValue)).OrderBy(p => p.TenDV);
                        }
                        else if (Nhomduoc.EditValue != null && Tieunhomduoc.EditValue == null)
                        {
                            phieu.DataSource = ketqua.Where(p => p.IDNhom == Convert.ToInt32(Nhomduoc.EditValue)).OrderBy(p => p.TenDV);
                        }

                    }
                    else
                    {
                        if (Nhomduoc.EditValue != null && Tieunhomduoc.EditValue != null)
                        {
                            G_control.DataSource = ketqua.Where(p => p.IDNhom == Convert.ToInt32(Nhomduoc.EditValue) && p.IdTieuNhom == Convert.ToInt32(Tieunhomduoc.EditValue)).OrderBy(p => p.TenDV);

                        }
                        else if (Nhomduoc.EditValue != null && Tieunhomduoc.EditValue == null)
                        {
                            G_control.DataSource = ketqua.Where(p => p.IDNhom == Convert.ToInt32(Nhomduoc.EditValue)).OrderBy(p => p.TenDV);

                        }

                    }
                }

                if (inbaocao == false)
                {
                    return;
                }
                phieu.tungaydenngay = string.Format("Từ ngày {0} Đến Ngày {1}", tngay.DateTime.ToShortDateString(), Dngay.DateTime.ToShortDateString());
                phieu.haminbaocao();
                phieu.CreateDocument();
                frmIn inphieu = new frmIn();
                inphieu.prcIN.PrintingSystem = phieu.PrintingSystem;
                inphieu.ShowDialog();
                themmoi.Clear();

            }
            else
            {


                BaoCao.repBcNXTtoanvien phieu = new BaoCao.repBcNXTtoanvien();
                if (radiotatca.Checked == true)
                {
                    if (inbaocao == true)
                    {

                        phieu.DataSource = ketqua.OrderBy(p => p.TenDV);
                    }
                    else
                    {

                        G_control.DataSource = ketqua.OrderBy(p => p.TenDV);
                    }
                }
                else if (radiochitet.Checked == true)
                {
                    if (inbaocao == true)
                    {
                        if (Nhomduoc.EditValue != null && Tieunhomduoc.EditValue != null)
                        {
                            phieu.DataSource = ketqua.Where(p => p.IDNhom == Convert.ToInt32(Nhomduoc.EditValue) && p.IdTieuNhom == Convert.ToInt32(Tieunhomduoc.EditValue)).OrderBy(p => p.TenDV);
                        }
                        else if (Nhomduoc.EditValue != null && Tieunhomduoc.EditValue == null)
                        {
                            phieu.DataSource = ketqua.Where(p => p.IDNhom == Convert.ToInt32(Nhomduoc.EditValue)).OrderBy(p => p.TenDV);
                        }

                    }
                    else
                    {
                        if (Nhomduoc.EditValue != null && Tieunhomduoc.EditValue != null)
                        {
                            G_control.DataSource = ketqua.Where(p => p.IDNhom == Convert.ToInt32(Nhomduoc.EditValue) && p.IdTieuNhom == Convert.ToInt32(Tieunhomduoc.EditValue)).OrderBy(p => p.TenDV);

                        }
                        else if (Nhomduoc.EditValue != null && Tieunhomduoc.EditValue == null)
                        {
                            G_control.DataSource = ketqua.Where(p => p.IDNhom == Convert.ToInt32(Nhomduoc.EditValue)).OrderBy(p => p.TenDV);

                        }

                    }
                }

                if (inbaocao == false)
                {
                    return;
                }
                phieu.tungaydenngay = string.Format("Từ ngày {0} Đến Ngày {1}", tngay.DateTime.ToShortDateString(), Dngay.DateTime.ToShortDateString());
                phieu.baocaonxttv();
                phieu.CreateDocument();
                frmIn inphieu = new frmIn();
                inphieu.prcIN.PrintingSystem = phieu.PrintingSystem;
                inphieu.ShowDialog();
                themmoi.Clear();


            }


        }
        public class hhh
        {
            public int ma { set; get; }
        }
        public class kp
        {
            public int MaKP { set; get; }
            public string TenKP { set; get; }
            public bool che { set; get; }

        }
        List<NhaCC> _lcc = new List<NhaCC>();
        private void Frm_baocaonxttoanvien_Load(object sender, EventArgs e)
        {
            _lcc = data1.NhaCCs.OrderBy(p => p.TenCC).ToList();
            _lcc.Add(new NhaCC { MaCC = "", TenCC = " Tất cả" });
            var kd = (from k in data1.KPhongs where (k.PLoai == "Khoa dược") select new kp { che = true, MaKP = k.MaKP, TenKP = k.TenKP }).ToList();
            gr_kho.DataSource = kd;
            var nhom = from k in data1.NhomDVs.Where(p => p.Status == 1) select new { k.IDNhom, k.TenNhom };
            lup_MaCC.Properties.DataSource = _lcc.OrderBy(p => p.TenCC).ToList();
            Nhomduoc.Properties.DataSource = nhom;
            tngay.DateTime = DateTime.Now;
            Dngay.DateTime = DateTime.Now;
        }
        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            if (tngay.DateTime <= Dngay.DateTime)
            {

                inbaocao = true;
                hoc();
            }
            else
            {

                MessageBox.Show("Ngày bắt đầu không thể lớn hơn ngày đến", "Thông báo");
                return;
            }

        }
        public void ham(int ID)
        {

            var tieunhom = from tn in data1.TieuNhomDVs.Where(p => p.IDNhom == ID) select new { tn.IdTieuNhom, tn.TenTN };
            Tieunhomduoc.Properties.DataSource = tieunhom;

        }
        private void Nhomduoc_EditValueChanged(object sender, EventArgs e)
        {
            if (Nhomduoc.EditValue != null)
            {
                int idk = Convert.ToInt32(Nhomduoc.EditValue);
                ham(idk);

            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radiochitet.Checked == true)
            {
                Nhomduoc.Properties.ReadOnly = false;
                Tieunhomduoc.Properties.ReadOnly = false;
                lup_MaCC.Properties.ReadOnly = false;
            }
        }

        private void radiotatca_CheckedChanged(object sender, EventArgs e)
        {
            if (radiotatca.Checked == true)
            {
                Nhomduoc.Properties.ReadOnly = true;
                Tieunhomduoc.Properties.ReadOnly = true;
                lup_MaCC.Properties.ReadOnly = true;
                Nhomduoc.Text = "";
                Tieunhomduoc.Text = "";
                lup_MaCC.EditValue = "";
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (tngay.DateTime <= Dngay.DateTime)
            {
                inbaocao = false;
                hoc();
            }
            else
            {

                MessageBox.Show("Ngày bắt đầu không thể lớn hơn ngày đến", "Thông báo");
                return;
            }

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelControl5_Paint(object sender, PaintEventArgs e)
        {

        }

    }

}