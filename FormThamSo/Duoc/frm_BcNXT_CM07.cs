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
    public partial class frm_BcNXT_CM07 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BcNXT_CM07()
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

        private void frm_BcNXT_CM07_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //var q = (from xp in data.KPhongs.Where(p => p.PLoai== ("Xã phường"))
            //         select new {xp.MaKP, xp.TenKP }).ToList();
            //lupXP.Properties.DataSource = q.ToList();

            var qcc = from CC in data.NhaCCs select new { CC.MaCC, CC.TenCC };
            lupNhaCC.Properties.DataSource = qcc.ToList();

            dateTuNgay.DateTime = System.DateTime.Now;
            dateDenNgay.DateTime = System.DateTime.Now;
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
                BaoCao.Rep_BcNXT_CM07 rep = new BaoCao.Rep_BcNXT_CM07();
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

                rep.TuNgay.Value = dateTuNgay.Text;
                rep.DenNgay.Value = dateDenNgay.Text;
                rep.TuNgayDenNgay.Value = "Từ ngày " + dateTuNgay.Text + " Đến ngày " + dateDenNgay.Text;

                //string _xp = "";
                //if (lupXP.EditValue != null)
                //    _xp = lupXP.EditValue.ToString();
                //var qtenxp = (from xp in data.KPhongs.Where(p=>p.MaKP==_xp)
                //              select new { xp.TenKP }).ToList();
                //if (qtenxp.Count > 0)
                //{
                //    rep.XaPhuong.Value = (qtenxp.First().TenKP);
                //}
                string _nhacc = "";
                if (lupNhaCC.EditValue != null)
                    _nhacc = lupNhaCC.EditValue.ToString();
                var qtenncc = (from nhapd in data.NhapDs
                               join nhacc in data.NhaCCs on nhapd.MaCC equals nhacc.MaCC
                               where (nhacc.MaCC == _nhacc)
                               select new { nhacc.TenCC }).ToList();
                if (qtenncc.Count > 0)
                {
                    rep.NhaCC.Value = qtenncc.First().TenCC;
                }
                var qnxt = (from nhapd in data.NhapDs
                            join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                            join kp in data.KPhongs on nhapd.MaKP equals kp.MaKP 
                            join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                            join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                            join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                            where (kp.PLoai == "Khoa dược" && kp.TenKP.Contains("xã"))
                           // where (nhapd.NgayNhap >= tungay && nhapd.NgayNhap <= denngay)
                            where (nhapd.PLoai == 1||nhapd.PLoai==5)
                            group new { nhomdv, tieunhomdv, dv, nhapd, nhapdct } by new { nhomdv.TenNhom, tieunhomdv.TenTN, dv.TenDV, dv.NuocSX, nhapd.MaCC, dv.DonVi, nhapdct.DonGia } into kq
                            select new
                                {
                                    MaCC=kq.Key.MaCC,
                                    NhomDV = kq.Key.TenNhom,
                                    TieuNhomDV = kq.Key.TenTN,
                                    TenDV = kq.Key.TenDV,
                                    DVT = kq.Key.DonVi,
                                    DonGia = kq.Key.DonGia,
                                    NuocSX = kq.Key.NuocSX,

                                    TonDKSL = kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.SoLuongN) - kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.SoLuongSD),
                                    TonDKTT = kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.ThanhTienN) - kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.ThanhTienSD),

                                    NhapTKSL = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongN),
                                    NhapTKTT = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienN),

                                    SDSL = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongSD),
                                    SDTT = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienSD),

                                    TonCKSL = kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.SoLuongN) - kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.SoLuongSD),
                                    TonCKTT = kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.ThanhTienN) - kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.ThanhTienSD)
                                }).OrderBy(p => p.TenDV).ToList();


                if (_nhacc==null ||_nhacc=="")
                {

                    rep.DataSource = qnxt.Where(p => p.TonDKSL > 0 || p.TonDKSL < 0 || p.TonCKSL < 0 || p.TonCKSL > 0 || p.NhapTKSL > 0).OrderBy(p => p.TenDV).ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
               
                if (_nhacc != null && _nhacc != ""&&_nhacc.Length>0)
                {

                    rep.DataSource = qnxt.Where(p => p.TonDKSL > 0 || p.TonDKSL < 0 || p.TonCKSL < 0 || p.TonCKSL > 0 || p.NhapTKSL > 0).Where(p => p.MaCC == _nhacc).OrderBy(p => p.TenDV).ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
               

            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}