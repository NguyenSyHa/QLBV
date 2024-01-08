using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using COMExcel = Microsoft.Office.Interop.Excel;

namespace QLBV.FormThamSo
{
    public partial class frmTsBCNXT_24009_N : DevExpress.XtraEditors.XtraForm
    {
        public frmTsBCNXT_24009_N()
        {
            InitializeComponent();
        }
        private bool KTtaoBcNXT()
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
            else return true;
        }
        private void btnInBC_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
            denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;

            List<KPhong> _kpChon = new List<KPhong>();
            if (KTtaoBcNXT())
            {
                for (int i = 0; i < cklKP.ItemCount; i++)
                {
                    if (cklKP.GetItemChecked(i))
                    {
                        _kpChon.Add(new KPhong { TenKP = cklKP.GetItemText(i), MaKP = cklKP.GetItemValue(i) == null ? 0 : Convert.ToInt32(cklKP.GetItemValue(i)) });
                    }
                }

                if (_kpChon.Count > 0)
                {
                    var q0 = (from nhapd in data.NhapDs
                              join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                              select new { nhapd.XuatTD, nhapd.MaKP, nhapdct.MaDV, nhapd.PLoai, nhapd.KieuDon, nhapd.NgayNhap, nhapdct.DonGia, nhapdct.SoLuongN, nhapdct.SoLuongX, nhapdct.ThanhTienN, nhapdct.ThanhTienX,  }).ToList();

                    List<DichVu> qdv = new List<DichVu>();
                    if (radioGroup1.SelectedIndex == 0)

                        qdv = (from dv in data.DichVus
                               join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                               where tn.TenRG == "Thuốc gây nghiện" || tn.TenRG == "Thuốc hướng tâm thần"
                               // select new DichVu { MaQD = dv.MaQD, TenDV = dv.TenDV, DonVi = dv.DonVi }).ToList();
                               select dv).ToList();
                    else if (radioGroup1.SelectedIndex == 1)
                        qdv = (from dv in data.DichVus
                               join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                               where tn.TenRG == "Thuốc thường (kháng sinh)"
                               //select new DichVu { MaQD = dv.MaQD, TenDV = dv.TenDV, DonVi = dv.DonVi }).ToList();
                               select dv).ToList();
                    else if(radioGroup1.SelectedIndex == 2)
                        qdv = (from dv in data.DichVus
                               join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                               where tn.TenRG == "Thuốc đông y"                             
                               select dv).ToList();

                    var q1 = (from nhapd in q0
                              where ((nhapd.PLoai == 1) || (nhapd.PLoai == 2))
                              join dv in qdv on nhapd.MaDV equals dv.MaDV
                              select new { nhapd.XuatTD, nhapd.MaKP, nhapd.MaDV, nhapd.PLoai, nhapd.KieuDon, nhapd.NgayNhap, nhapd.DonGia, nhapd.SoLuongN, nhapd.SoLuongX, nhapd.ThanhTienN, nhapd.ThanhTienX, dv.MaQD, dv.TenDV, dv.DonVi, dv.TenHC, dv.HamLuong, dv.SoTT, dv.NuocSX, dv.DuongD, dv.NguonGoc
                                 // , dv.MaATC 
                              }).ToList();
                    var q2 = (from a in q1
                              join kp in _kpChon on a.MaKP equals kp.MaKP
                              select new
                              {
                                  a.XuatTD,
                                  a.MaKP,
                                  a.MaDV,
                                  a.PLoai,
                                  a.KieuDon,
                                  a.NgayNhap,
                                  a.DonGia, 
                                  a.SoLuongN,
                                  a.SoLuongX,
                                  a.ThanhTienN,
                                  a.ThanhTienX,
                                  a.MaQD,
                                  a.TenDV,
                                  a.DonVi,
                                  a.TenHC,
                                  a.HamLuong,
                                  a.SoTT,// số thứ tự theo thông tư
                                  a.NuocSX,
                                  a.DuongD, 
                                  a.NguonGoc,
                                 // a.MaATC

                              }).OrderBy(p => p.TenDV).ToList();
                  
                    if (_kpChon.Count > 0)
                    {
                        #region báo cáo thuốc gây nghiện, hướng tâm thần, tiền chất                        
                        if (radioGroup1.SelectedIndex == 0)
                        {
                            var qnxt = (from a in q2
                                        group a by new
                                        {
                                            a.TenDV,
                                            a.DonVi,
                                            a.MaDV,
                                        } into kq
                                        select new
                                        {
                                            MaDV = kq.Key.MaDV,
                                            TenDichVu = kq.Key.TenDV,// Tên thương mại của dịch vụ                                           
                                            DonVi = kq.Key.DonVi, // đơn vị tính                                                                                 
                                            TonDKSL = kq.Where(p => p.NgayNhap < tungay).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap < tungay).Sum(p => p.SoLuongX),
                                            NhapTKSL = kq.Where(p => p.PLoai == 1).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongN), // phân loại = 1; nhập dược                                             
                                            XuatTKSL = kq.Where(p => p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),  //Phân loại = 2 là xuất dược
                                            TonCKSL = kq.Where(p => p.NgayNhap < denngay).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap < denngay).Sum(p => p.SoLuongX),
                                        }).Where(p => p.NhapTKSL != 0 || p.XuatTKSL != 0 || p.TonCKSL!=0).Select(p => new { p.MaDV, p.TenDichVu, p.DonVi, p.TonDKSL, p.NhapTKSL, Tong = p.TonDKSL + p.NhapTKSL, p.XuatTKSL, p.TonCKSL }).ToList();
                            BaoCao.rep_BCSDThuocGayNghien_24009 rep = new BaoCao.rep_BCSDThuocGayNghien_24009();
                            frmIn frm2 = new frmIn();
                            if (String.IsNullOrEmpty(txtThoiGian.Text))
                                rep.lab_thoigian.Text = "Từ ngày " + dateTuNgay.Text + " đến này " + dateDenNgay.Text;
                            else
                                rep.lab_thoigian.Text = txtThoiGian.Text;
                            rep.cel_diadanh.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;

                            if (String.IsNullOrEmpty(txtTieuDeBC.Text))
                                rep.lab_Tieude.Text = "BÁO CÁO SỬ DỤNG THUỐC THÀNH PHẨM CÓ CHỨA HOẠT CHẤT GÂY NGHIỆN THUỐC THÀNH PHẨM CÓ CHỨA HOẠT CHẤT HƯỚNG TÂM THẦN, THUỐC CÓ CHỨA TIỀN CHẤT";
                            else
                                rep.lab_Tieude.Text = txtTieuDeBC.Text.ToUpper();
                            rep.lblSo.Text = "Số: ....../BC-KD";
                            if (DungChung.Bien.MaBV == "24009")
                            {
                                rep.lab_kinhgui.Text = "Kính gửi: Sở Y tế Bắc Giang";
                            }
                            else
                                rep.lab_kinhgui.Text = "";
                            rep.DataSource = qnxt.OrderBy(p => p.TenDichVu).ToList();
                            rep.BindingData();
                            rep.CreateDocument();
                            frm2.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm2.ShowDialog();
                        }
                        #endregion
                        #region báo cáo sd thuốc kháng sinh
                        else if (radioGroup1.SelectedIndex == 1)
                        {
                            var q3 = (from a in q2
                                        group a by new
                                        {
                                            a.TenDV,
                                            a.DonVi,
                                            a.MaDV,
                                            a.DonGia,
                                            a.TenHC, 
                                            a.DuongD,
                                            a.HamLuong,
                                            a.NuocSX, 
                                            a.SoTT,
                                            //  a.MaATC
                                        } into kq
                                        select new
                                        {
                                            kq.Key.TenDV,
                                            kq.Key.TenHC,
                                            kq.Key.DuongD,
                                            kq.Key.HamLuong,
                                            kq.Key.NuocSX,
                                            kq.Key.SoTT,
                                            kq.Key.DonGia,
                                            MaDV = kq.Key.MaDV,
                                           // kq.Key.MaATC,
                                            //TenDichVu = kq.Key.TenDV,// Tên thương mại của dịch vụ                                           
                                            DonVi = kq.Key.DonVi, // đơn vị tính    
                                            SoLuong = kq.Where(p => p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),  //Phân loại = 2 là xuất dược

                                        }).Where(p=>p.SoLuong != 0).Select(p => new
                                            { p.MaDV, p.DonVi, p.SoLuong, p.TenDV, p.TenHC, p.DuongD, p.HamLuong, p.SoTT,
                                           p.NuocSX,
                                            p.DonGia }).ToList();

                            var qnxt = (from a in q3
                                        join dvex in data.DichVuExes on a.MaDV equals dvex.MaDV into kq
                                        from kq1 in kq.DefaultIfEmpty()
                                        select new
                                        {
                                            a.NuocSX,
                                            a.MaDV,
                                            a.DonVi,
                                            a.SoLuong,
                                            a.TenDV,
                                            a.TenHC,
                                            a.DuongD,
                                            a.HamLuong,
                                            a.SoTT,                                            
                                            a.DonGia,
                                            MaATC = kq1 == null ? "" : kq1.MaATC,
                                        }).ToList();
                            BaoCao.Rep_BCSDkhangsinh_24009 rep = new BaoCao.Rep_BCSDkhangsinh_24009();
                            frmIn frm2 = new frmIn();
                            if (String.IsNullOrEmpty(txtThoiGian.Text))
                                rep.lad_tungaydenngay.Text = "Từ ngày " + dateTuNgay.Text + " đến này " + dateDenNgay.Text;
                            else
                                rep.lad_tungaydenngay.Text = txtThoiGian.Text;
                            rep.cel_diadanh.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;

                            if (String.IsNullOrEmpty(txtTieuDeBC.Text))
                                rep.lab_Tieude.Text = "BÁO CÁO SỬ DỤNG KHÁNG SINH NĂM " + DateTime.Now.Year;
                            else
                                rep.lab_Tieude.Text = txtTieuDeBC.Text.ToUpper();                          
                            
                            rep.DataSource = qnxt.OrderBy(p => p.TenDV).ToList();
                            rep.Bindingdata();
                            rep.CreateDocument();   
                            frm2.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm2.ShowDialog();
                        }
                        #endregion
                        #region báo cáo dược liệu, YHCT
                        else if (radioGroup1.SelectedIndex == 2)
                        {
                            var qnxt = (from a in q2
                                        group a by new
                                        {
                                            a.TenDV,
                                            a.DonVi,
                                            a.MaDV,
                                            a.TenHC,
                                            a.NguonGoc
                                        } into kq
                                        select new
                                        {
                                            MaDV = kq.Key.MaDV,
                                            TenDichVu = kq.Key.TenDV,// Tên thương mại của dịch vụ                                           
                                            DonVi = kq.Key.DonVi, // đơn vị tính   
                                            kq.Key.TenHC,
                                            ThuocNam = kq.Key.NguonGoc == "N" ? "X" : "",
                                            ThuocBac = kq.Key.NguonGoc == "B" ? "X" : "",                                                                                    
                                            SoLuong = kq.Where(p => p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),  //Phân loại = 2 là xuất dược                                   
                                        }).Where(p => p.SoLuong != 0).Select(p => new { p.MaDV, p.TenDichVu, p.DonVi, p.SoLuong, p.TenHC, p.ThuocBac, p.ThuocNam }).ToList();
                            BaoCao.rep_BCSDDuocLieu_ViThuocYHCT rep = new BaoCao.rep_BCSDDuocLieu_ViThuocYHCT();
                            frmIn frm2 = new frmIn();
                            if (String.IsNullOrEmpty(txtThoiGian.Text))
                                rep.lab_thoigian.Text = "Năm : " + DateTime.Now.Year;
                            else
                                rep.lab_thoigian.Text = txtThoiGian.Text;
                            rep.cel_ngayLap.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                            rep.cellTongKhoan.Text = "Tổng số : " + qnxt.Count + " khoản";
                            rep.DataSource = qnxt.OrderBy(p => p.TenDichVu).ToList();
                            rep.BindingData();
                            rep.CreateDocument();
                            frm2.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm2.ShowDialog();
                        }
                        #endregion
                    }
                }
                else
                { MessageBox.Show("Bạn chưa chọn kho"); }
            }
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void cklKP_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                if (e.State == CheckState.Checked)
                {
                    for (int i = 1; i < cklKP.ItemCount; i++)
                    {
                        cklKP.SetItemChecked(i, true);
                    }
                }
                else
                {
                    for (int i = 1; i < cklKP.ItemCount; i++)
                    {
                        cklKP.SetItemChecked(i, false);
                    }
                }
            }
        }

        private void frmTsBCNXT_24009_N_Load(object sender, EventArgs e)
        {
            try
            {
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                dateDenNgay.DateTime = System.DateTime.Now;
                dateTuNgay.DateTime = System.DateTime.Now;
                List<KPhong> dskp = data.KPhongs.Where(p => p.PLoai == "Khoa dược").OrderBy(p => p.TenKP).ToList();
                dskp.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
                cklKP.DataSource = dskp;
                for (int i = 0; i < cklKP.ItemCount; i++)
                {
                    cklKP.SetItemChecked(i, true);
                }
            }
            catch (Exception)
            {
            }
        }





    }
}