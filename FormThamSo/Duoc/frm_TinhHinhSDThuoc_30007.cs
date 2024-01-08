using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormThamSo
{
    public partial class frm_TinhHinhSDThuoc_30007 : DevExpress.XtraEditors.XtraForm
    {
        public frm_TinhHinhSDThuoc_30007()
        {
            InitializeComponent();
        }

        private void frm_TinhHinhSDThuoc_30007_Load(object sender, EventArgs e)
        {
            lupTuNgay.DateTime = DateTime.Now;
            lupDenNgay.DateTime = DateTime.Now;
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<KPhong> qkp = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc).OrderBy(p=>p.TenKP).ToList();
            qkp.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
            cklKP.DataSource = qkp;
            cklKP.CheckAll();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            DateTime dengay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<int> lkp = new List<int>();
            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                if (cklKP.GetItemChecked(i))
                    lkp.Add(Convert.ToInt32(cklKP.GetItemValue(i)));
            }
            var qnd = (from nd in data.NhapDs.Where(p => p.NgayNhap <= dengay)
                       join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                       select new { nd.NgayNhap, nd.MaKP, nd.PLoai, nd.KieuDon, nd.TraDuoc_KieuDon, ndct.ThanhTienN, ndct.ThanhTienX, ndct.MaDV }).ToList();
            var qdv = (from dv in data.DichVus.Where(p => p.PLoai == 1)
                       join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom                     
                       select new { dv.MaDV, tn.TenRG, dv.NuocSX, tn.IDNhom, dv.DongY }).ToList();
            var qnd1 = (from nd in qnd
                        join kp in lkp on nd.MaKP equals kp
                        join dv in qdv on nd.MaDV equals dv.MaDV
                        select new {dv.MaDV, dv.TenRG,dv.IDNhom,dv.DongY, NuocSX = dv.NuocSX == null ? "" : dv.NuocSX.ToLower().Trim(), nd.NgayNhap, nd.PLoai, nd.KieuDon, nd.ThanhTienN, nd.ThanhTienX }).ToList();
            List<BC> lbc = new List<BC>();
            #region Tổng giá trị phân loại theo danh mục
            BC bc1 = new BC();
            bc1.STT = 1;
            bc1.NoiDung = "Tổng trị giá phân loại theo danh mục:";
            bc1.TenTN = "1.1 Thuốc tân dược";
          
            lbc.Add(bc1);

            bc1 = new BC();
            bc1.STT = 1;
            bc1.NoiDung = "Tổng trị giá phân loại theo danh mục:";           
            bc1.TenTN = "- Phân theo nước sản xuất";
            lbc.Add(bc1);

            var qtrongnuoc = qnd1.Where(p => p.NuocSX == "việt nam" || p.NuocSX == "vnam" || p.NuocSX == "vn" || p.NuocSX == "v.nam").ToList();
            var qnuocngoai = qnd1.Where(p => p.NuocSX != "việt nam" && p.NuocSX != "vnam" && p.NuocSX != "vn" && p.NuocSX != "v.nam").ToList();
            BC bc2 = new BC();
            bc2.STT = 1;
            bc2.NoiDung = "Tổng trị giá phân loại theo danh mục:";          
            bc2.TenTN = "    + Thuốc sản xuất trong nước:";
            bc2.TonDK = qtrongnuoc.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienN) - qtrongnuoc.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienX);
            bc2.NhapTK = qtrongnuoc.Where(p => p.NgayNhap >= tungay).Where(p=>p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.ThanhTienN);// tổng nhập không tính nhập trả dược
            bc2.SuDungTK = qtrongnuoc.Where(p => p.NgayNhap >= tungay).Where(p=>p.PLoai == 2 && (p.KieuDon != 2 && p.KieuDon != 3)).Sum(p => p.ThanhTienX) - qtrongnuoc.Where(p => p.NgayNhap >= tungay).Where(p=>p.PLoai ==1 && p.KieuDon == 2).Sum(p => p.ThanhTienN); //không tính xuất nội bộ, xuất ngoài Bv, trừ thêm xuất trả dược
            bc2.TonCK = qtrongnuoc.Where(p => p.NgayNhap <= dengay).Sum(p => p.ThanhTienN) - qtrongnuoc.Where(p => p.NgayNhap < dengay).Sum(p => p.ThanhTienX);
            lbc.Add(bc2);

            BC bc3 = new BC();
            bc3.STT = 1;
            bc3.NoiDung = "Tổng trị giá phân loại theo danh mục:";          
            bc3.TenTN = "    + Thuốc ngoại:";
            bc3.TonDK = qnuocngoai.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienN) - qtrongnuoc.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienX);
            bc3.NhapTK = qnuocngoai.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.ThanhTienN);// tổng nhập không tính nhập trả dược
            bc3.SuDungTK = qnuocngoai.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 2 && (p.KieuDon != 2 && p.KieuDon != 3)).Sum(p => p.ThanhTienX) - qtrongnuoc.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN); //không tính xuất nội bộ, xuất ngoài Bv, trừ thêm xuất trả dược
            bc3.TonCK = qnuocngoai.Where(p => p.NgayNhap <= dengay).Sum(p => p.ThanhTienN) - qtrongnuoc.Where(p => p.NgayNhap < dengay).Sum(p => p.ThanhTienX);
            lbc.Add(bc3);

            BC bc4 = new BC();
            bc4.STT = 1;
            bc4.NoiDung = "Tổng trị giá phân loại theo danh mục:";           
            bc4.TenTN = "- Phân theo nhóm thuốc";
            lbc.Add(bc4);

            var qkhangsinh = qnd1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_khangsinh).ToList();
            BC bc5 = new BC();
            bc5.STT = 1;
            bc5.NoiDung = "Tổng trị giá phân loại theo danh mục:";         
            bc5.TenTN = "  + Nhóm kháng sinh";
            bc5.TonDK = qkhangsinh.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienN) - qkhangsinh.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienX);
            bc5.NhapTK = qkhangsinh.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.ThanhTienN);// tổng nhập không tính nhập trả dược
            bc5.SuDungTK = qkhangsinh.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 2 && (p.KieuDon != 2 && p.KieuDon != 3)).Sum(p => p.ThanhTienX) - qkhangsinh.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN); //không tính xuất nội bộ, xuất ngoài Bv, trừ thêm xuất trả dược
            bc5.TonCK = qkhangsinh.Where(p => p.NgayNhap <= dengay).Sum(p => p.ThanhTienN) - qkhangsinh.Where(p => p.NgayNhap < dengay).Sum(p => p.ThanhTienX);
            lbc.Add(bc5);

            var qvitamin = qnd1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_vitamin).ToList();
            BC bc6 = new BC();
            bc6.STT = 1;
            bc6.NoiDung = "Tổng trị giá phân loại theo danh mục:";           
            bc6.TenTN = "  + Nhóm Vitamin";
            bc6.TonDK = qvitamin.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienN) - qvitamin.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienX);
            bc6.NhapTK = qvitamin.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.ThanhTienN);// tổng nhập không tính nhập trả dược
            bc6.SuDungTK = qvitamin.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 2 && (p.KieuDon != 2 && p.KieuDon != 3)).Sum(p => p.ThanhTienX) - qvitamin.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN); //không tính xuất nội bộ, xuất ngoài Bv, trừ thêm xuất trả dược
            bc6.TonCK = qvitamin.Where(p => p.NgayNhap <= dengay).Sum(p => p.ThanhTienN) - qvitamin.Where(p => p.NgayNhap < dengay).Sum(p => p.ThanhTienX);
            lbc.Add(bc6);

            var qdichtruyen = qnd1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_DT).ToList();
            BC bc7 = new BC();
            bc7.STT = 1;
            bc7.NoiDung = "Tổng trị giá phân loại theo danh mục:";
            bc7.TenTN = "  + Nhóm dịch truyền";
            bc7.TonDK = qdichtruyen.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienN) - qdichtruyen.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienX);
            bc7.NhapTK = qdichtruyen.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.ThanhTienN);// tổng nhập không tính nhập trả dược
            bc7.SuDungTK = qdichtruyen.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 2 && (p.KieuDon != 2 && p.KieuDon != 3)).Sum(p => p.ThanhTienX) - qdichtruyen.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN); //không tính xuất nội bộ, xuất ngoài Bv, trừ thêm xuất trả dược
            bc7.TonCK = qdichtruyen.Where(p => p.NgayNhap <= dengay).Sum(p => p.ThanhTienN) - qdichtruyen.Where(p => p.NgayNhap < dengay).Sum(p => p.ThanhTienX);
            lbc.Add(bc7);

            var qcorticoid = qnd1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_Corticoid).ToList();
            BC bc8 = new BC();
            bc8.STT = 1;
            bc8.NoiDung = "Tổng trị giá phân loại theo danh mục:";
            bc8.TenTN = "  + Nhóm Corticoid";
            bc8.TonDK = qcorticoid.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienN) - qcorticoid.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienX);
            bc8.NhapTK = qcorticoid.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.ThanhTienN);// tổng nhập không tính nhập trả dược
            bc8.SuDungTK = qcorticoid.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 2 && (p.KieuDon != 2 && p.KieuDon != 3)).Sum(p => p.ThanhTienX) - qcorticoid.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN); //không tính xuất nội bộ, xuất ngoài Bv, trừ thêm xuất trả dược
            bc8.TonCK = qcorticoid.Where(p => p.NgayNhap <= dengay).Sum(p => p.ThanhTienN) - qcorticoid.Where(p => p.NgayNhap < dengay).Sum(p => p.ThanhTienX);
            lbc.Add(bc8);


          
            BC bc9 = new BC();
            bc9.STT = 1;
            bc9.NoiDung = "Tổng trị giá phân loại theo danh mục:";
            bc9.TenTN = "  + Nhóm Tim mạch - huyết áp";
             lbc.Add(bc9);

             var qthuocChongUT= qnd1.Where(p => p.IDNhom == 5).ToList();
            BC bc10 = new BC();
            bc10.STT = 1;
            bc10.NoiDung = "Tổng trị giá phân loại theo danh mục:";           
            bc10.TenTN = "  + Nhóm thuốc ung thư";
            bc10.TonDK = qthuocChongUT.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienN) - qthuocChongUT.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienX);
            bc10.NhapTK = qthuocChongUT.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.ThanhTienN);// tổng nhập không tính nhập trả dược
            bc10.SuDungTK = qthuocChongUT.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 2 && (p.KieuDon != 2 && p.KieuDon != 3)).Sum(p => p.ThanhTienX) - qthuocChongUT.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN); //không tính xuất nội bộ, xuất ngoài Bv, trừ thêm xuất trả dược
            bc10.TonCK = qthuocChongUT.Where(p => p.NgayNhap <= dengay).Sum(p => p.ThanhTienN) - qthuocChongUT.Where(p => p.NgayNhap < dengay).Sum(p => p.ThanhTienX);
            lbc.Add(bc10);

            var qthuoctanduockhac = qnd1.Where(p => (p.IDNhom == 4 || p.IDNhom == 6) && p.DongY != 1 && p.TenRG != DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_Corticoid && p.TenRG != DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_CPYHCT && p.TenRG != DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_DT && p.TenRG != DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_khangsinh && p.TenRG != DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_vitamin).ToList();
            BC bc11 = new BC();
            bc11.STT = 1;
            bc11.NoiDung = "Tổng trị giá phân loại theo danh mục:";
            bc11.TenTN = "  + Các nhóm còn lại";
            bc11.TonDK = qthuoctanduockhac.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienN) - qthuoctanduockhac.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienX);
            bc11.NhapTK = qthuoctanduockhac.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.ThanhTienN);// tổng nhập không tính nhập trả dược
            bc11.SuDungTK = qthuoctanduockhac.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 2 && (p.KieuDon != 2 && p.KieuDon != 3)).Sum(p => p.ThanhTienX) - qthuoctanduockhac.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN); //không tính xuất nội bộ, xuất ngoài Bv, trừ thêm xuất trả dược
            bc11.TonCK = qthuoctanduockhac.Where(p => p.NgayNhap <= dengay).Sum(p => p.ThanhTienN) - qthuoctanduockhac.Where(p => p.NgayNhap < dengay).Sum(p => p.ThanhTienX);
            lbc.Add(bc11);

            var qMau = qnd1.Where(p => p.IDNhom == 7).ToList();
            BC bc12 = new BC();
            bc12.STT = 1;
            bc12.NoiDung = "Tổng trị giá phân loại theo danh mục:";           
            bc12.TenTN = "1.2 Tiền máu";
            bc12.TonDK = qMau.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienN) - qMau.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienX);
            bc12.NhapTK = qMau.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.ThanhTienN);// tổng nhập không tính nhập trả dược
            bc12.SuDungTK = qMau.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 2 && (p.KieuDon != 2 && p.KieuDon != 3)).Sum(p => p.ThanhTienX) - qMau.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN); //không tính xuất nội bộ, xuất ngoài Bv, trừ thêm xuất trả dược
            bc12.TonCK = qMau.Where(p => p.NgayNhap <= dengay).Sum(p => p.ThanhTienN) - qMau.Where(p => p.NgayNhap < dengay).Sum(p => p.ThanhTienX);
            lbc.Add(bc12);

            var qCPYHCT = qnd1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_CPYHCT && p.DongY != 1).ToList();
            BC bc13 = new BC();
            bc13.STT = 1;
            bc13.NoiDung = "Tổng trị giá phân loại theo danh mục:";
            bc13.TenTN = "1.3 Thuốc chế phẩm YHCT";           
            bc13.TonDK = qCPYHCT.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienN) - qCPYHCT.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienX);
            bc13.NhapTK = qCPYHCT.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.ThanhTienN);// tổng nhập không tính nhập trả dược
            bc13.SuDungTK = qCPYHCT.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 2 && (p.KieuDon != 2 && p.KieuDon != 3)).Sum(p => p.ThanhTienX) - qCPYHCT.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN); //không tính xuất nội bộ, xuất ngoài Bv, trừ thêm xuất trả dược
            bc13.TonCK = qCPYHCT.Where(p => p.NgayNhap <= dengay).Sum(p => p.ThanhTienN) - qCPYHCT.Where(p => p.NgayNhap < dengay).Sum(p => p.ThanhTienX);
            lbc.Add(bc13);

            var qDuocLieu = qnd1.Where(p => p.DongY != 1).ToList();
            BC bc14 = new BC();
            bc14.STT = 1;
            bc14.NoiDung = "Tổng trị giá phân loại theo danh mục:";
            bc14.TenTN = "1.4. Vị thuốc dược liệu";          
            bc14.TonDK = qDuocLieu.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienN) - qDuocLieu.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienX);
            bc14.NhapTK = qDuocLieu.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.ThanhTienN);// tổng nhập không tính nhập trả dược
            bc14.SuDungTK = qDuocLieu.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 2 && (p.KieuDon != 2 && p.KieuDon != 3)).Sum(p => p.ThanhTienX) - qDuocLieu.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN); //không tính xuất nội bộ, xuất ngoài Bv, trừ thêm xuất trả dược
            bc14.TonCK = qDuocLieu.Where(p => p.NgayNhap <= dengay).Sum(p => p.ThanhTienN) - qDuocLieu.Where(p => p.NgayNhap < dengay).Sum(p => p.ThanhTienX);
            lbc.Add(bc14);


            var qhoachat = qnd1.Where(p => p.TenRG.ToLower().Contains("hóa chất")).ToList();
            BC bc15 = new BC();
            bc15.STT = 1;
            bc15.NoiDung = "Tổng trị giá phân loại theo danh mục:";
            bc15.TenTN = "1.5. Hóa chất xét nghiệm, thuốc thử";          
            bc15.TonDK = qhoachat.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienN) - qhoachat.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienX);
            bc15.NhapTK = qhoachat.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.ThanhTienN);// tổng nhập không tính nhập trả dược
            bc15.SuDungTK = qhoachat.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 2 && (p.KieuDon != 2 && p.KieuDon != 3)).Sum(p => p.ThanhTienX) - qhoachat.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN); //không tính xuất nội bộ, xuất ngoài Bv, trừ thêm xuất trả dược
            bc15.TonCK = qhoachat.Where(p => p.NgayNhap <= dengay).Sum(p => p.ThanhTienN) - qhoachat.Where(p => p.NgayNhap < dengay).Sum(p => p.ThanhTienX);
            lbc.Add(bc15);


            BC bc16 = new BC();
            bc16.STT = 1;
            bc16.NoiDung = "Tổng trị giá phân loại theo danh mục:";
            bc16.TenTN = "1.6 Vắc xin, sinh phẩm";          
            lbc.Add(bc16);

            var qVTYTTH = qnd1.Where(p => p.IDNhom == 10 || p.IDNhom == 11).ToList();
            BC bc17 = new BC();
            bc17.STT = 1;
            bc17.NoiDung = "Tổng trị giá phân loại theo danh mục:";
            bc17.TenTN = "1.7 Vật tư y tế tiêu hao";          
            bc17.TonDK = qVTYTTH.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienN) - qVTYTTH.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienX);
            bc17.NhapTK = qVTYTTH.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.ThanhTienN);// tổng nhập không tính nhập trả dược
            bc17.SuDungTK = qVTYTTH.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 2 && (p.KieuDon != 2 && p.KieuDon != 3)).Sum(p => p.ThanhTienX) - qVTYTTH.Where(p => p.NgayNhap >= tungay).Where(p => p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN); //không tính xuất nội bộ, xuất ngoài Bv, trừ thêm xuất trả dược
            bc17.TonCK = qVTYTTH.Where(p => p.NgayNhap <= dengay).Sum(p => p.ThanhTienN) - qVTYTTH.Where(p => p.NgayNhap < dengay).Sum(p => p.ThanhTienX);
            lbc.Add(bc17);
            #endregion

            #region Tổng các nguồn tiền thuốc đã sd
            var qvp = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= dengay)
                       join bn in data.BenhNhans on vp.MaBNhan equals bn.MaBNhan
                       join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                       select new { bn.MaBNhan, bn.NoiTru, bn.Tuoi, bn.MaDTuong, vpct.MaDV, vpct.ThanhTien, vpct.TrongBH, vpct.TienBH, vpct.TienBN, bn.SThe }).ToList();
            var qvp1 = (from vp in qvp join dv in qdv on vp.MaDV equals dv.MaDV select new { vp.MaBNhan, vp.NoiTru, vp.Tuoi, vp.MaDTuong, vp.MaDV, vp.ThanhTien, vp.TrongBH, vp.TienBH, vp.TienBN, vp.SThe, dv.IDNhom }).ToList();

            var qTienThuocBHYT = qvp1.Where(p=>p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6).ToList();
            BC bc18 = new BC();
            bc18.STT = 2;
            bc18.NoiDung = "Tổng các nguồn tiền thuốc đã sử dụng (= 2.1 + 2.2 + 2.3)";
            bc18.TenTN = "2.1. Tiền thuốc BHYT";           
            bc18.SuDungTK = qTienThuocBHYT.Sum(p => p.TienBH);          
            lbc.Add(bc18);

            BC bc19 = new BC();
            bc19.STT = 2;
            bc19.NoiDung = "Tổng các nguồn tiền thuốc đã sử dụng (= 2.1 + 2.2 + 2.3)";           
            bc19.TenTN = "BHYT nội trú";
            bc19.SuDungTK = qTienThuocBHYT.Where(p=>p.NoiTru == 1).Sum(p => p.TienBH);
            lbc.Add(bc19);

            BC bc20 = new BC();
            bc20.STT = 2;
            bc20.NoiDung = "Tổng các nguồn tiền thuốc đã sử dụng (= 2.1 + 2.2 + 2.3)";           
            bc20.TenTN = "BHYT ngoại trú";
            bc20.SuDungTK = qTienThuocBHYT.Where(p => p.NoiTru == 0).Sum(p => p.TienBH);
            lbc.Add(bc20);

            BC bc21 = new BC();
            bc21.STT = 2;
            bc21.NoiDung = "Tổng các nguồn tiền thuốc đã sử dụng (= 2.1 + 2.2 + 2.3)";
            bc21.TenTN = "Tính riêng tiền thuốc cho trẻ em dưới 6 tuổi";
            bc21.SuDungTK = qTienThuocBHYT.Where(p => p.Tuoi < 6).Sum(p => p.TienBH);
            lbc.Add(bc21);

           
            BC bc = new BC();
            bc.STT = 2;
            bc.NoiDung = "Tổng các nguồn tiền thuốc đã sử dụng (= 2.1 + 2.2 + 2.3)";
            bc.TenTN = "Tính riêng tiền thuốc cho người nghèo có thẻ BHYT";
            bc.SuDungTK = qTienThuocBHYT.Where(p => p.MaDTuong != null && p.MaDTuong.Trim().ToLower() == "hn").Sum(p => p.TienBH);
            lbc.Add(bc);

            bc = new BC();
            bc.STT = 2;
            bc.NoiDung = "Tổng các nguồn tiền thuốc đã sử dụng (= 2.1 + 2.2 + 2.3)";
            bc.TenTN = "2.2. Tiền thuốc viện phí";           
            bc.SuDungTK = qTienThuocBHYT.Sum(p => p.TienBN);
            lbc.Add(bc);

            BC bc22 = new BC();
            bc22.STT = 2;
            bc22.NoiDung = "Tổng các nguồn tiền thuốc đã sử dụng (= 2.1 + 2.2 + 2.3)";
            bc22.TenTN = "2.3. Tiền thuốc khác";           
            lbc.Add(bc22);


            bc = new BC();
            bc.STT = 3;
            bc.NoiDung = "Tổng giá trị phân theo nguồn cung ứng";
            bc.TenTN = "Mua Công ty CP Dược tỉnh";                   
            lbc.Add(bc);

            bc = new BC();
            bc.STT = 3;
            bc.NoiDung = "Tổng giá trị phân theo nguồn cung ứng";
            bc.TenTN = "Mua của các nhà thầu khác";          
            lbc.Add(bc);

            bc = new BC();
            bc.STT = 3;
            bc.NoiDung = "Tổng giá trị phân theo nguồn cung ứng";
            bc.TenTN = "Thuốc tự pha chế";          
            lbc.Add(bc);

            bc = new BC();
            bc.STT = 3;
            bc.NoiDung = "Tổng giá trị phân theo nguồn cung ứng";
            bc.TenTN = "Thuốc của các nguồn khác";          
            lbc.Add(bc);
            #endregion

            foreach (var a in lbc)
            {
                a.TonDK = a.TonDK == null ? 0 : ((double) a.TonDK / 1000);
                a.NhapTK =a.NhapTK == null ? 0: ( (double)a.NhapTK / 1000);
                a.SuDungTK = a.SuDungTK == null ? 0 : ( (double)a.SuDungTK / 1000);
                a.TonCK = a.TonCK == null ? 0 : ( (double)a.TonCK / 1000);
            }
            frmIn frm = new frmIn();
            BaoCao.rep_TinhHinhSDThuoc_30007 rep = new BaoCao.rep_TinhHinhSDThuoc_30007();
            rep.celTit_TonDK.Text = "Tồn kỳ trước (" + tungay.Day + " tháng " + tungay.Month + " năm " + tungay.Year + ")";
            rep.celTitNhapTK.Text = "Mua trong năm (từ " + tungay.ToString("dd/MM/yyyy") + " tới " + dengay.ToString("dd/MM/yyyy") + ")";
            rep.celTitSDTK.Text = "Đã sử dụng trong năm (từ " + tungay.ToString("dd/MM/yyyy") + " tới " + dengay.ToString("dd/MM/yyyy") + ")";
            rep.celTitTonCK.Text = "Tồn cuối kỳ (" + dengay.Day + " tháng " + dengay.Month + " năm " + dengay.Year + ")";
            rep.DataSource = lbc;
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
         


        }
        public class BC
        {
            public int STT { set; get; }
            public string NoiDung { set; get; }
            public string TenNhom { set; get; }
            public string TenTN { set; get; }
            public double? TonDK { get; set; }
            public double? NhapTK { get; set; }
            public double? SuDungTK { get; set;}
            public double? TonCK { set; get;}
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}