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
    public partial class frm_BcNXT_CM05 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BcNXT_CM05()
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

            if (lupKho.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn kho để in báo cáo");
                lupKho.Focus();
                return false;
            }

            else return true;
        }

        private void frm_BcNXT_CM02_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var q = from TK in data.KPhongs.Where(p => p.PLoai== ("Khoa dược")) select new { TK.TenKP, TK.MaKP };
            lupKho.Properties.DataSource = q.ToList();
            dateTuNgay.DateTime = System.DateTime.Now;
            dateDenNgay.DateTime = System.DateTime.Now;
            cmbPL.EditValue = "Thuốc";
           
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
                
                tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
                denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;

                frmIn frm = new frmIn();
                BaoCao.rep_BcNXT_CM05 rep = new BaoCao.rep_BcNXT_CM05(chkHienThi.Checked);
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

                rep.TuNgay.Value = dateTuNgay.Text;
                rep.DenNgay.Value = dateDenNgay.Text;
                rep.TuNgayDenNgay.Value = "Từ ngày " + dateTuNgay.Text + " Đến ngày " + dateDenNgay.Text;
                rep.TenD.Value = "Tên " + cmbPL.Text.ToLower() + " và hàm lượng";
                
                int _kho = 0;
                if (lupKho.EditValue != null)
                    _kho = Convert.ToInt32( lupKho.EditValue);
                rep.MaKP.Value = _kho;
                var qtenkho = (from kp in data.KPhongs
                               join nhapd in data.NhapDs on kp.MaKP  equals nhapd.MaKP 
                               where (nhapd.MaKP == _kho)
                               select new { kp.TenKP }).ToList();
                if (qtenkho.Count > 0)
                {
                    rep.BaoCao.Value = ("báo cáo xuất nhập tồn " + cmbPL.Text + " " + qtenkho.First().TenKP).ToUpper();
                }
                var qnxt = (from nhapd in data.NhapDs
                            join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                            join kp in data.KPhongs on nhapd.MaKP equals kp.MaKP
                            join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                            join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                            join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                            where (kp.MaKP ==_kho)
                            //where (nhapd.NgayNhap >= tungay && nhapd.NgayNhap <= denngay)
                            where ((nhapd.PLoai == 1) || (nhapd.PLoai == 2))
                            group new { kp, nhomdv, tieunhomdv, dv, nhapd, nhapdct } by new { nhomdv.TenNhomCT, tieunhomdv.TenTN, dv.MaDV, dv.TenDV, dv.DonVi, nhapdct.DonGia, dv.NuocSX } into kq
                            select new
                                {
                                    //MaKP = kq.Key.MaKP,
                                    MaDV = kq.Key.MaDV,
                                    TenNhomDV=kq.Key.TenNhomCT,
                                    TenTieuNhomDV = kq.Key.TenTN,
                                    TenDV = kq.Key.TenDV,
                                    DVT = kq.Key.DonVi,
                                    DonGia = kq.Key.DonGia,
                                    NuocSX = kq.Key.NuocSX,
                                    
                                    TonDKSL = kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.SoLuongN) - kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.SoLuongX),
                                    TonDKTT = kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.ThanhTienN) - kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.ThanhTienX),

                                    NhapTKSL = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongN),
                                    NhapTKTT = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienN),

                                    XuatNoiTSL = kq.Where(p => p.nhapd.KieuDon == 1).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongX),
                                    xuatNoiTTT = kq.Where(p => p.nhapd.KieuDon == 1).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX),
                                 
                                    XuatNgTSL = kq.Where(p => p.nhapd.KieuDon == 0).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongX),
                                    xuatNgTTT = kq.Where(p => p.nhapd.KieuDon == 0).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX),

                                    XuatTuTraSL = kq.Where(p => p.nhapd.KieuDon == 4).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongX),
                                    xuatTuTraTT = kq.Where(p => p.nhapd.KieuDon == 4).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX),

                                     XuatTuTrucSL = kq.Where(p => p.nhapd.KieuDon == 6).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongX),
                                    xuatTuTrucTT = kq.Where(p => p.nhapd.KieuDon == 6).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX),

                                    TongXuatSL = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongX),
                                    TongXuatTT = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX),
                                 
                                    TonCKSL = kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.SoLuongN) - kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.SoLuongX),
                                    TonCKTT = kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.ThanhTienN) - kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.ThanhTienX),
                                           
                                 }).OrderBy(p=>p.TenDV).ToList();
                List<DungChung.Ham.NXT> _lNXT = new List<DungChung.Ham.NXT>();
                foreach (var a in qnxt)
                {

                    //if ((a.TonDKSL > 0 || a.NhapTKSL > 0) && a.TonCKSL > 0)
                    if ((a.TonDKSL == 0 && a.NhapTKSL == 0 && a.TonCKSL == 0)) { 
                    
                    } else
                    {
                        DungChung.Ham.NXT _addnxt = new DungChung.Ham.NXT();
                        if(a.DonGia!=null)
                        _addnxt.DonGia = a.DonGia;
                        if (a.DVT != null)
                        _addnxt.DonVi = a.DVT;
                        //_addnxt.MaCC=a.m
                        if (a.MaDV != null)
                        _addnxt.MaDV = a.MaDV;
                        if (a.NuocSX != null)
                        _addnxt.NuocSX = a.NuocSX;
                        if (a.NhapTKSL != null)
                        _addnxt.NhapTK_sl = a.NhapTKSL;
                        if (a.NhapTKTT != null)
                        _addnxt.NhapTK_tt = a.NhapTKTT;
                        //_addnxt.SoDK=
                        //_addnxt.SoLo=
                        if (a.TenDV != null)
                        _addnxt.TenDV = a.TenDV;
                        if (a.TenNhomDV != null)
                        _addnxt.TenNhom = a.TenNhomDV;
                        if (a.TenTieuNhomDV != null)
                        _addnxt.TenTN = a.TenTieuNhomDV;
                        if (a.TonCKSL != null)
                        _addnxt.TonCK_SL = a.TonCKSL;
                        if (a.TonCKTT != null)
                        _addnxt.TonCK_TT = a.TonCKTT;
                        if (a.TonDKSL != null)
                        _addnxt.TonDK_SL = a.TonDKSL;
                        if (a.TonDKTT != null)
                        _addnxt.TonDK_TT = a.TonDKTT;
                        if (a.XuatTuTraSL != null && a.XuatTuTraSL >0)
                        _addnxt.XuatKhac_sl = a.XuatTuTraSL;
                        if (a.xuatTuTraTT != null)
                        _addnxt.XuatKhac_tt = a.xuatTuTraTT;
                        if (a.XuatNoiTSL != null && a.XuatNoiTSL>0)
                        _addnxt.XuatNoiT_sl = a.XuatNoiTSL;
                        if (a.xuatNoiTTT != null)
                        _addnxt.XuatNoiT_tt = a.xuatNoiTTT;
                        if (a.XuatNgTSL != null)
                        _addnxt.XuatNgoaiT_sl = a.XuatNgTSL;
                        if (a.xuatNgTTT != null)
                        _addnxt.XuatNgoaiT_tt = a.xuatNgTTT;
                        if (a.TongXuatSL != null)
                        _addnxt.XuatTK_sl = a.TongXuatSL;
                        if (a.TongXuatTT != null)
                        _addnxt.XuatTK_tt = a.TongXuatTT;
                        if (a.XuatTuTrucSL != null)
                        _addnxt.XuatTuTruc_sl = a.XuatTuTrucSL;
                        if (a.xuatTuTrucTT != null)
                        _addnxt.XuatTuTruc_tt = a.xuatTuTrucTT;
                        _lNXT.Add(_addnxt);
                    }
                }
                double TT = 0;
                string _ploai = "";
                _ploai = cmbPL.Text.ToLower();
                TT = _lNXT.Where(p => p.TenNhom.Contains(_ploai)).Sum(p => p.TonCK_TT);
                    rep.TongTien.Value =DungChung.Ham.DocTienBangChu(TT, "đồng.");
                    rep.PhanLoai.Value = cmbPL.Text;
                    //MessageBox.Show(TT.ToString());
                    rep.DataSource = _lNXT.OrderBy(p=>p.TenDV).ToList().Where(p => p.TenNhom.Contains(_ploai));
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                
            }
        }

        private void lupKho_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void cmbPL_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}