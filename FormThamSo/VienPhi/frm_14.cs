using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace QLBV.FormThamSo
{
    public partial class frm_14b : DevExpress.XtraEditors.XtraForm
    {
        public frm_14b()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
             if (kt())
            {
                timquy(lupNgaytu.DateTime.Month);
                
                DateTime ngaytu = System.DateTime.Now.Date;
                DateTime ngayden = System.DateTime.Now.Date;
                ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupngayden.DateTime);
                BaoCao.rep_14 rep = new BaoCao.rep_14();
                frmIn frm = new frmIn();
                QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var q = (from DT in Data.DTuongs
                         join BN in Data.BenhNhans.Where(p => p.NoiTru == 1) on DT.MaDTuong equals BN.SThe.Substring(0, 2)
                         join BV in Data.BenhViens on BN.MaCS equals BV.MaBV
                         join vp in Data.VienPhis on BN.MaBNhan equals vp.MaBNhan
                         join vpct in Data.VienPhicts.Where(p => p.TrongBH == 1) on vp.idVPhi equals vpct.idVPhi
                         join dv in Data.DichVus on vpct.MaDV equals dv.MaDV
                         join Nhom in Data.NhomDVs on dv.IDNhom equals Nhom.IDNhom
                         where (vp.NgayTT >= ngaytu && vp.NgayTT <= ngayden)
                         where (vp.Duyet != 0 && vp.Duyet != 3 && vp.Duyet != null)
                         group new { DT, BN, vp, vpct, Nhom, BV } by new { BN.TuyenDuoi, DT.Nhom } into kq
                         select new
                         {
                             HangBV = kq.Key.TuyenDuoi,
                             Tentuyen = kq.Key.Nhom,
                              Xetnghiem = kq.Where(p => p.Nhom.TenNhomCT.Contains("Xét nghiệm")).Sum(p => p.vpct.TienBH) - kq.Where(p => p.Nhom.TenNhomCT.Contains("Xét nghiệm")).Sum(p => p.vpct.TienChenh),
                             CDHA = kq.Where(p => p.Nhom.TenNhomCT.Contains("Chẩn đoán hình ảnh")).Sum(p => p.vpct.TienBH) - kq.Where(p => p.Nhom.TenNhomCT.Contains("Chẩn đoán hình ảnh")).Sum(p => p.vpct.TienChenh),
                             ThuocDT = kq.Where(p => p.Nhom.TenNhomCT.Contains( "Thuốc trong danh mục BHYT" )).Sum(p => p.vpct.TienBH) - kq.Where(p => p.Nhom.TenNhomCT.Contains( "Thuốc trong danh mục BHYT" )).Sum(p => p.vpct.TienChenh),
                             mau = kq.Where(p => p.Nhom.TenNhomCT.Contains("Máu và chế phẩm của máu")).Sum(p => p.vpct.TienBH) - kq.Where(p => p.Nhom.TenNhomCT.Contains("Máu và chế phẩm của máu")).Sum(p => p.vpct.TienChenh),
                             TTPT = kq.Where(p => p.Nhom.TenNhomCT.Contains("Thủ thuật, phẫu thuật")).Sum(p => p.vpct.TienBH) - kq.Where(p => p.Nhom.TenNhomCT.Contains("Thủ thuật, phẫu thuật")).Sum(p => p.vpct.TienChenh),
                             VTYTtieuhao = kq.Where(p => p.Nhom.TenNhomCT.Contains("Vật tư y tế trong danh mục BHYT")).Sum(p => p.vpct.TienBH) - kq.Where(p => p.Nhom.TenNhomCT.Contains("Vật tư y tế trong danh mục BHYT")).Sum(p => p.vpct.TienChenh),
                             VTYTthaythe = kq.Where(p => p.Nhom.TenNhomCT.Contains("VTYT thanh toán theo tỷ lệ")).Sum(p => p.vpct.TienBH) - kq.Where(p => p.Nhom.TenNhomCT.Contains("VTYT thanh toán theo tỷ lệ")).Sum(p => p.vpct.TienChenh),
                             DVKTcao = kq.Where(p => p.Nhom.TenNhomCT.Contains("DVKT thanh toán theo tỷ lệ")).Sum(p => p.vpct.TienBH) - kq.Where(p => p.Nhom.TenNhomCT.Contains("DVKT thanh toán theo tỷ lệ")).Sum(p => p.vpct.TienChenh),
                             Thuocthaighep = kq.Where(p => p.Nhom.TenNhomCT.Contains("Thuốc điều trị ung thư, chống thải ghép ngoài danh mục")).Sum(p => p.vpct.TienBH) - kq.Where(p => p.Nhom.TenNhomCT.Contains("Thuốc điều trị ung thư, chống thải ghép ngoài danh mục")).Sum(p => p.vpct.TienChenh),
                             Tienkham = kq.Where(p => p.Nhom.TenNhomCT.Contains("Giường điều trị nội trú")).Sum(p => p.vpct.TienBH) - kq.Where(p => p.Nhom.TenNhomCT.Contains("Giường điều trị nội trú")).Sum(p => p.vpct.TienChenh),
                             Vanchuyen = kq.Where(p => p.Nhom.TenNhomCT.Contains("Vận chuyển")).Sum(p => p.vpct.TienBH) - kq.Where(p => p.Nhom.TenNhomCT.Contains("Vận chuyển")).Sum(p => p.vpct.TienChenh),
                             Benhnhandungtuyen = kq.Where(p => p.BN.Tuyen == 1).Sum(p => p.vpct.TienBN) - kq.Where(p => p.BN.Tuyen == 1).Sum(p => p.vpct.TienChenhBN),
                             Benhnhantraituyen = kq.Where(p => p.BN.Tuyen == 2).Sum(p => p.vpct.TienBN) - kq.Where(p => p.BN.Tuyen == 2).Sum(p => p.vpct.TienChenhBN),
                             BHXHchitra = kq.Sum(p => p.vpct.TienBH) - kq.Sum(p => p.vpct.TienChenh),
                             Tongcong = kq.Sum(p => p.vpct.ThanhTien) - kq.Sum(p => p.vpct.TienChenh) - kq.Sum(p => p.vpct.TienChenhBN),
                             BHXHtuchoithanhtoan = kq.Sum(p => p.vpct.TienChenh)

                         }).OrderBy(p => p.HangBV).OrderBy(p => p.Tentuyen).ToList();
                if (q.Count > 0)
                {
                    rep.Quy.Value = theoquy();
                    rep.ngaytu.Value = ngaytu.Date;
                    rep.ngayden.Value = ngayden.Date;
                    rep.TenDV.Value = DungChung.Bien.TenCQ;
                    rep.Macs.Value = DungChung.Bien.MaBV;
                    //rep.Nguoilap.Value = DungChung.Bien.Nguoiphat;
                    //rep.TruongphongKHTT.Value = DungChung.Bien.TruongkhoaLS;
                    //rep.Ketoan.Value = DungChung.Bien.Nguoilinh;
                    //rep.ThutruongDV.Value = DungChung.Bien.TruongkhoaLS;
                    rep.DataSource = q;
                    rep.BindingData();
                    rep.CreateDocument();
                    rep.DataMember = "Table";
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                //rep.CreateDocument();
                //rep.DataMember = "Table";
                //frm.prcIN.PrintingSystem = rep.PrintingSystem;
                //frm.ShowDialog();
                //else
                //    MessageBox.Show("ko có dữ liệu");
            }
        }
        
        private bool kt()
        {
            if (lupNgaytu.Text == "" || lupngayden.Text == "")
            {
                MessageBox.Show("Bạn hãy chọn ngày tháng");
                lupNgaytu.Focus();
                return false;
            }
            return true;
        }
        private int timquy(int month)
        {
            if (month >= 1 && month <= 3)
            {
                return (1);
            }
            if (month > 3 && month <= 6)
            {
                return (2);
            }
            if (month > 6 && month <= 9)
            {
                return (3);
            }
            else { return (4); }
        }
        private string theoquy()
        {
            string quy = "";


            switch (timquy(lupNgaytu.DateTime.Month))
            {
                case 1:
                    quy = "Quý I";
                    break;
                case 2:
                    quy = "Quý II";
                    break;
                case 3:
                    quy = "Quý III";
                    break;
                case 4:
                    quy = "Quý IV";
                    break;
            }
            return quy;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm14b_Load(object sender, EventArgs e)
        {
            lupngayden.DateTime = System.DateTime.Now;
            lupNgaytu.DateTime = System.DateTime.Now;
        }
    }
}