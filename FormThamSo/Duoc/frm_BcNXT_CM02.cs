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
    public partial class frm_BcNXT_CM02 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BcNXT_CM02()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
     
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
            if (lupKho.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn Kho");
                lupKho.Focus();
                return false;
            }
            if (cmbPL.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn Phân loại");
                dateDenNgay.Focus();
                return false;
            }
              else return true;
        }

        private void frm_BcNXT_CM02_Load(object sender, EventArgs e)
        {
            dateTuNgay.DateTime = System.DateTime.Now;
            dateDenNgay.DateTime = System.DateTime.Now;
            cmbPL.EditValue = "Thuốc";
            var q = from TK in data.KPhongs.Where(p => p.PLoai== ("Khoa dược")) select new { TK.TenKP, TK.MaKP };
            lupKho.Properties.DataSource = q.ToList();
           
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;


            if (KTtaoBcNXT())
            {
                int _kho = 0;
                if (lupKho.EditValue != null)
                    _kho = lupKho.EditValue == null ? 0 : Convert.ToInt32( lupKho.EditValue);
                tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
                denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;

                frmIn frm = new frmIn();

                BaoCao.rep_BcNXT_CM02 rep = new BaoCao.rep_BcNXT_CM02(chkHienThi.Checked);
                rep.Kho.Value = _kho;
                rep.TuNgayDenNgay.Value = "Từ ngày " + dateTuNgay.Text + " Đến ngày " + dateDenNgay.Text;
                var qtenkho = (from kp in data.KPhongs
                               join nhapd in data.NhapDs on kp.MaKP equals nhapd.MaKP
                               where (nhapd.MaKP == _kho)
                               select new { kp.TenKP }).ToList();
                if (qtenkho.Count > 0)
                {
                    rep.Kho.Value = qtenkho.First().TenKP;
                }
                
                if (cmbPL.EditValue =="Thuốc")
                {
                    rep.BaoCao.Value = "BÁO CÁO NHẬP XUẤT TỒN THUỐC " + qtenkho.First().TenKP.ToUpper();
                }
                if(cmbPL.EditValue =="Vật tư y tế")
                {
                    rep.BaoCao.Value = "BÁO CÁO NHẬP XUẤT TỒN VẬT TƯ Y TẾ TIÊU HAO " + qtenkho.First().TenKP.ToUpper();
                }
                if (cmbPL.EditValue == "Hóa chất")
                {
                    rep.BaoCao.Value = "BÁO CÁO NHẬP XUẤT TỒN HOÁ CHÂT " + qtenkho.First().TenKP.ToUpper();
                }
                var qnxt = (from nhapd in data.NhapDs.Where(p=>p.MaKP==_kho)
                            join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                            join kp in data.KPhongs on nhapd.MaKP equals kp.MaKP
                            join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                            join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                            join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                            where (kp.PLoai == "Khoa dược")
                            //where (nhapd.NgayNhap >= tungay && nhapd.NgayNhap <= denngay)
                            where((nhapd.PLoai == 1)||(nhapd.PLoai ==2) || (nhapd.PLoai == 3))
                            group new { nhomdv, tieunhomdv, dv, nhapd, nhapdct } by new { nhomdv.TenNhom, tieunhomdv.TenTN, dv.TenDV, dv.DonVi, nhapdct.DonGia, dv.NuocSX } into kq
                            select new
                                {
                                    TenNhom=kq.Key.TenNhom,
                                    TenTieuNhomDV = kq.Key.TenTN,
                                    TenDV = kq.Key.TenDV,
                                    DVT = kq.Key.DonVi,
                                    DonGia = kq.Key.DonGia,
                                    NuocSX = kq.Key.NuocSX,

                                    TonDKSL = kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.SoLuongN) - kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.SoLuongX),
                                    TonDKTT = kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.ThanhTienN) - kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.ThanhTienX),

                                    NhapTKSL = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongN),
                                    NhapTKTT = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienN),

                                    XuatTKSL = kq.Where(p => p.nhapd.KieuDon == 3).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongX),
                                    xuatTKTT = kq.Where(p => p.nhapd.KieuDon == 3).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX),

                                    XuatTKKhacSL = kq.Where(p => p.nhapd.KieuDon == 2).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongX),
                                    XuatTKKhacTT = kq.Where(p => p.nhapd.KieuDon == 2).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX),

                                    TongXuatSL = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongX),
                                    TongXuatTT = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX),

                                    TonCKSL = kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.SoLuongN) - kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.SoLuongX),
                                    TonCKTT = kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.ThanhTienN) - kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.ThanhTienX),

                                }).OrderBy(p=>p.TenDV).ToList();
                
                double TT = 0;

                if (cmbPL.EditValue =="Thuốc")
                {
                    qnxt = qnxt.Where(p => p.TenNhom.Contains("Thuốc")).ToList();
                    rep.DataSource = qnxt.ToList();
                    
                    for (int i = 0; i < qnxt.Count(); i++)
                    {
                        if (qnxt.ToList().Skip(i).Take(1).First().TonCKTT != null)
                        {
                            string tam = qnxt.Skip(i).First().TonCKTT.ToString();
                            TT = TT + Convert.ToDouble(tam);
                        }
                    }
                    rep.TongTien.Value = TT;
                   // MessageBox.Show(TT.ToString());
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                if (cmbPL.EditValue == "Vật tư y tế" )
                {
                    qnxt = qnxt.Where(p => p.TenNhom.Contains("Vật tư y tế")).ToList();
                    rep.DataSource = qnxt.Where(p => p.TonDKSL > 0 || p.TonDKSL < 0 || p.TonCKSL < 0 || p.TonCKSL>0 || p.NhapTKSL>0).ToList();

                    for (int i = 0; i < qnxt.Count(); i++)
                    {
                        if (qnxt.ToList().Skip(i).Take(1).First().TonCKTT != null)
                        {
                            string tam = qnxt.Skip(i).First().TonCKTT.ToString();
                            TT = TT + Convert.ToDouble(tam);
                        }
                    }
                    rep.TongTien.Value = TT;
                    // MessageBox.Show(TT.ToString());
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                if (cmbPL.EditValue == "Hóa chất")
                {
                    qnxt = qnxt.Where(p => p.TenNhom.Contains("Hóa chất")).ToList();
                    rep.DataSource = qnxt.Where(p => p.TonDKSL > 0 || p.TonDKSL < 0 || p.TonCKSL < 0 || p.TonCKSL>0 || p.NhapTKSL>0).ToList();

                    for (int i = 0; i < qnxt.Count(); i++)
                    {
                        if (qnxt.ToList().Skip(i).Take(1).First().TonCKTT != null)
                        {
                            string tam = qnxt.Skip(i).First().TonCKTT.ToString();
                            TT = TT + Convert.ToDouble(tam);
                        }
                    }
                    rep.TongTien.Value = TT;
                    // MessageBox.Show(TT.ToString());
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
         
                //else
                //    MessageBox.Show("Không có dữ liệu để in báo cáo");
            }
        }

        private void lupKho_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}