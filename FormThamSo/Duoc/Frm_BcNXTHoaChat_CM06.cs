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
    public partial class Frm_BcNXTHoaChat_CM06 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcNXTHoaChat_CM06()
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
        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;


            if (KTtaoBcNXT())
            {

                tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
                denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;

                frmIn frm = new frmIn();
                BaoCao.Rep_BcNXTHoaChat_CM06 rep = new BaoCao.Rep_BcNXTHoaChat_CM06(chkHienThi.Checked);
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

                rep.TuNgayDenNgay.Value = "Từ ngày " + dateTuNgay.Text + " Đến ngày " + dateDenNgay.Text;
                rep.TuNgay.Value = dateTuNgay.Text;
                rep.DenNgay.Value = dateDenNgay.Text;
                int _kho = 0;
                if (lupKho.EditValue != null)
                    _kho = Convert.ToInt32(lupKho.EditValue);
                rep.MaKP.Value = _kho;
                var qtenkho = (from kp in data.KPhongs
                               join nhapd in data.NhapDs on kp.MaKP equals nhapd.MaKP
                               where (nhapd.MaKP == _kho)
                               select new { kp.TenKP }).ToList();
                if (qtenkho.Count > 0)
                {
                    rep.TenKP.Value = qtenkho.First().TenKP.ToUpper();
                }

                var qnxt = (from nhapd in data.NhapDs
                            join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                            join kp in data.KPhongs on nhapd.MaKP equals kp.MaKP
                            join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                            join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                            join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                            where (kp.MaKP == _kho)
                            //  where (kp.PLoai == "Khoa dược")
                            //where (nhapd.NgayNhap >= tungay && nhapd.NgayNhap <= denngay)
                            where ((nhapd.PLoai == 1) || (nhapd.PLoai == 2))
                            where (nhomdv.TenNhom.Contains("Hóa chất"))
                            group new { kp, tieunhomdv, dv, nhapd, nhapdct } by new { tieunhomdv.TenTN, dv.MaDV, dv.TenDV, dv.NuocSX, dv.DonVi, nhapdct.DonGia } into kq
                            select new
                            {
                                MaDV = kq.Key.MaDV,
                                TenTieuNhomDV = kq.Key.TenTN,
                                TenDV = kq.Key.TenDV,
                                DVT = kq.Key.DonVi,
                                DonGia = kq.Key.DonGia,

                                TonDKSL = kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.SoLuongN) - kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.SoLuongX),
                                TonDKTT = kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.ThanhTienN) - kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.ThanhTienX),

                                NhapTKSL = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongN),
                                NhapTKTT = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienN),

                                TongXuatSL = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongX),
                                TongXuatTT = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX),

                                TonCKSL = kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.SoLuongN) - kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.SoLuongX),
                                TonCKTT = kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.ThanhTienN) - kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.ThanhTienX)
                            }).ToList().OrderBy(p=>p.TenDV).ToList();

                double TT = 0;


                for (int i = 0; i < qnxt.Count(); i++)
                {
                    if (qnxt.ToList().Skip(i).Take(1).First().TonCKTT != null)
                    {
                        string tt = qnxt.Skip(i).Take(1).First().TonCKTT.ToString();
                        TT = TT + Convert.ToDouble(tt);
                    }
                }
                rep.TongTien.Value = TT;
                //MessageBox.Show(TT.ToString());
                rep.DataSource = qnxt.OrderBy(p => p.TenDV).ToList();
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frm_BcNXTHoaChat_CM06_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var q = from TK in data.KPhongs.Where(p => p.PLoai== ("Khoa dược")) select new { TK.TenKP, TK.MaKP };
            lupKho.Properties.DataSource = q.ToList();
            dateTuNgay.DateTime = System.DateTime.Now;
            dateDenNgay.DateTime = System.DateTime.Now;
        }
    }
}