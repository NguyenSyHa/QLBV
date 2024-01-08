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
    public partial class frmTk15NgaySDThuoc_NB01 : DevExpress.XtraEditors.XtraForm
    {
        public frmTk15NgaySDThuoc_NB01()
        {
            InitializeComponent();
        }
        string[] _songay;
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
            if ((dateDenNgay.DateTime - dateTuNgay.DateTime).Days >15 || (dateDenNgay.DateTime - dateTuNgay.DateTime).Days <= 0) {
                MessageBox.Show("Khoảng cách giữa 2 ngày phải >0 và <=16 ngày");
                dateDenNgay.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(lupKho.Text)) {
                MessageBox.Show("Bạn chưa chọn kho");
                lupKho.Focus();
                return false;
            }
            
           return true;
        }
        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;


            if (KTtaoBcNXT())
            {
                int _makho = 0;
                if (lupKho.EditValue != null)
                    _makho =Convert.ToInt32( lupKho.EditValue);
                tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);
                int ngay = (denngay - tungay).Days+1;//kiểm tra ngày >=1 và <=16
                frmIn frm = new frmIn();
                string songay1=tungay.ToString().Substring(0,10);
                int sngay=tungay.Day;
                int sthang=tungay.Month;
                int snam=tungay.Year;
                if (ngay <= 16 && ngay >= 1)
                {
                    DateTime d1 = new DateTime();
                    _songay = new string[50];
                    for (int j = 0; j < ngay; j++)
                    {
                        d1 = tungay.AddDays(j);
                        _songay[j] = d1.ToString();
                    }
                    for (int k = ngay; k < 50; k++)
                    {
                        _songay[k] = "";
                    }
                              
                }
                int makhoa = 0;
                if (lupKhoa.EditValue != null)
                    makhoa = Convert.ToInt32( lupKhoa.EditValue);
                BaoCao.repTk15NgaySDThuoc_NB01 rep = new BaoCao.repTk15NgaySDThuoc_NB01(_songay,makhoa);
                // QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

                rep.TuNgay.Value = dateTuNgay.Text;
                rep.DenNgay.Value = dateDenNgay.Text;
                int _khoa = 0;
                if (lupKhoa.EditValue != null)
                    _khoa =Convert.ToInt32( lupKhoa.EditValue);
               // rep.Khoa.Value  = _khoa;

                var qtenkhoa = (from kp in data.KPhongs
                                join nhapd in data.NhapDs on kp.MaKP equals nhapd.MaKPnx
                                where (nhapd.MaKPnx == _khoa)
                                select new { kp.TenKP }).ToList();

                if (qtenkhoa.Count > 0)
                {
                    rep.Khoa.Value  = "Khoa: " + qtenkhoa.First().TenKP;
                }

                

                //if (q.Count > 0)
                //{
                //    rep.DataSource = q.ToList();
                //    rep.BindingData();
                //    rep.CreateDocument();
                //    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                //    frm.ShowDialog();
                //}
                if (_khoa<=0)
                {
                    var q = (from nhapd in data.NhapDs.Where(p => p.KieuDon == 1).Where(p=>p.MaKP==_makho)
                             where (nhapd.PLoai == 2)
                             join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                             join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                             where (nhapd.NgayNhap >= tungay && nhapd.NgayNhap <= denngay)
                             group new { dv, nhapd, nhapdct } by new {dv.MaDV, dv.TenDV, nhapdct.DonVi } into kq
                             select new
                             {
                                MaKPnx=0,
                                 MaDV = kq.Key.MaDV,
                                 TenDV = kq.Key.TenDV,
                                 DonVi = kq.Key.DonVi,
                                 SoLuongT = kq.Sum(p => p.nhapdct.SoLuongX),
                             }).ToList();
                    rep.DataSource = q.ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    var q = (from nhapd in data.NhapDs.Where(p => p.MaKPnx==_khoa).Where(p => p.KieuDon == 1).Where(p => p.MaKP == _makho)
                             where (nhapd.PLoai == 2)
                             join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                             join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                             where (nhapd.NgayNhap >= tungay && nhapd.NgayNhap <= denngay)
                             group new { dv, nhapd, nhapdct } by new {nhapd.MaKPnx, dv.MaDV, dv.TenDV, nhapdct.DonVi } into kq
                             select new
                             {
                                 MaKPnx = kq.Key.MaKPnx,
                                 MaDV = kq.Key.MaDV,
                                 TenDV = kq.Key.TenDV,
                                 DonVi = kq.Key.DonVi,
                                 SoLuongT = kq.Sum(p => p.nhapdct.SoLuongX),

                             }).ToList();
                    rep.DataSource = q.Where(p => p.MaKPnx==_khoa).ToList();
                    //rep.DataSource = q.ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }


                //else
                //    MessageBox.Show("Không có dữ liệu để in báo cáo");
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTk15NgaySDThuoc_NB01_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var q = from TK in data.KPhongs select new { TK.TenKP, TK.MaKP,TK.PLoai };
            lupKhoa.Properties.DataSource = q.Where(p => p.PLoai.Contains("lâm sàng")).ToList();
            lupKho.Properties.DataSource = q.Where(p=>p.PLoai.Contains("dược")).ToList();
            dateDenNgay.DateTime = System.DateTime.Now;
            dateTuNgay.DateTime = System.DateTime.Now;
        }

        private void dateDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
            denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);
            int ng = (denngay - tungay).Days;
            if (ng > 16)
            {
                MessageBox.Show("Báo cáo chỉ thống kê trong 16 ngày, số ngày bạn nhập > 16. Hãy kiểm tra lại.");
                lupKhoa.Focus();
            }
        }

      
    }
}