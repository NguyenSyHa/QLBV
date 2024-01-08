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
    public partial class Frm_BcXetNghiem_YM : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcXetNghiem_YM()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private bool KTtaoBc()
        {

            if (lupTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupTuNgay.Focus();
                return false;
            }
            if (lupDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
                return false;
            }
            else return true;
        }
        private string theoquy()
        {
            string quy = "";

            if (radIn.SelectedIndex == 1)
            {
                switch (timquy(lupTuNgay.DateTime.Month))
                {
                    case 1:
                        quy = " Quý I năm " + lupDenNgay.DateTime.ToString().Substring(6, 4);
                        break;
                    case 2:
                        quy = " Quý II năm " + lupDenNgay.DateTime.ToString().Substring(6, 4);
                        break;
                    case 3:
                        quy = " Quý III năm " + lupDenNgay.DateTime.ToString().Substring(6, 4);
                        break;
                    case 4:
                        quy = " Quý IV năm " + lupDenNgay.DateTime.ToString().Substring(6, 4);
                        break;
                }

            }
            if (radIn.SelectedIndex == 2)
            {
                quy = "Năm " + lupDenNgay.DateTime.ToString().Substring(6, 4);

            }
            else if (radIn.SelectedIndex == 0)
            {
                quy = "Từ ngày  " + lupTuNgay.DateTime.ToString().Substring(0, 10) + "  đến ngày  " + lupDenNgay.DateTime.ToString().Substring(0, 10);
            }
            return quy;
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
       
        private void Frm_BcXetNghiem_YM_Load(object sender, EventArgs e)
        {
            lupDenNgay.DateTime = System.DateTime.Now;
            lupTuNgay.DateTime = System.DateTime.Now;
            lupTuNgay.Focus();
        }
        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime ngaytu = System.DateTime.Now.Date;
            DateTime ngayden = System.DateTime.Now.Date;

            if (KTtaoBc())
            {
                ngaytu = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                frmIn frm = new frmIn();
                BaoCao.Rep_BcXetNghiem_YM rep = new BaoCao.Rep_BcXetNghiem_YM();
         
                rep.NTN.Value = theoquy();
                rep.TenBC.Value = ("báo cáo xét nghiệm").ToUpper();
         
                var qxn = (from cls in _Data.CLS.Where(p => p.NgayTH >= ngaytu && p.NgayTH <= ngayden)
                           join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                           join bn in _Data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                           join clsct in _Data.CLScts.Where(p => p.Status == 1) on cd.IDCD equals clsct.IDCD
                           join dvct in _Data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                           join dv in _Data.DichVus on dvct.MaDV equals dv.MaDV
                           join tn in _Data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                           group new {bn,dvct,tn,clsct} by new {bn.DTuong,bn.NoiTru,dvct.MaDVct} into kq
                           select new {DTuong=kq.Key.DTuong,
                                       NoiTru=kq.Key.NoiTru,
                                       SLSH1=kq.Where(p=>p.tn.TenRG=="XN hóa sinh máu").Select(p=>p.clsct.MaDVct).Count(),
                                       SLSH2 = kq.Where(p => p.tn.TenRG == "XN nước tiểu").Select(p => p.clsct.MaDVct).Count(),
                                       SLSH3 = kq.Where(p =>p.tn.TenRG == "XN nước tiểu" && p.dvct.TenDVct.Contains("cặn nước tiểu")).Select(p => p.clsct.MaDVct).Count(),
                                       SLHH1=kq.Where(p => p.tn.TenRG == "XN huyết học").Select(p => p.clsct.MaDVct).Count(),
                                       SLHH2 = kq.Where(p => p.dvct.TenDVct.Contains("HIV")).Select(p => p.clsct.MaDVct).Count(),
                                       SLHH3 = kq.Where(p => p.dvct.TenDVct.Contains("HBsAg")).Select(p => p.clsct.MaDVct).Count(),
                                       SLHH4 = kq.Where(p => p.dvct.TenDVct.Contains("KST sốt rét")).Select(p => p.clsct.MaDVct).Count(),
                                       SLVS1 = kq.Where(p => p.dvct.TenDVct.Contains("cặn nước tiểu")).Select(p => p.clsct.MaDVct).Count(),
                                       SLVS2 = kq.Where(p => p.dvct.TenDVct.Contains("XN phân") || p.dvct.TenDVct.Contains("Xét nghiệm phân")||p.dvct.TenDVct.Contains("xét nghiệm phân")).Select(p => p.clsct.MaDVct).Count(),
                                       SLVS3 = kq.Where(p => p.dvct.TenDVct.Contains("AFB")).Select(p => p.clsct.MaDVct).Count(),
                                       SLVS4 = kq.Where(p => p.dvct.TenDVct.Contains("soi dịch âm đạo") || p.dvct.TenDVct.Contains("Soi dịch âm đạo") || p.dvct.TenDVct.Contains("Soi dịch Âm đạo")).Select(p => p.clsct.MaDVct).Count(),
                           }).ToList();
              
                var q = (from xn in qxn
                         group new { xn } by new { xn.DTuong, xn.NoiTru } into kq
                         select new
                         {
                            DTuong=kq.Key.DTuong,
                            NNgTru=kq.Key.NoiTru,
                            SLSH=kq.Sum(p=>p.xn.SLSH1)+kq.Sum(p=>p.xn.SLSH2)-kq.Sum(p=>p.xn.SLSH3),
                            SLHH=kq.Sum(p=>p.xn.SLHH1)+kq.Sum(p=>p.xn.SLHH2)+kq.Sum(p=>p.xn.SLHH3)+kq.Sum(p=>p.xn.SLHH4),
                            SLVS1 = kq.Sum(p => p.xn.SLVS4) + kq.Sum(p => p.xn.SLVS2) + kq.Sum(p => p.xn.SLVS1),
                            SLVS2=kq.Sum(p=>p.xn.SLVS3),
                            SLVS = kq.Sum(p => p.xn.SLVS1) + kq.Sum(p => p.xn.SLVS2) + kq.Sum(p => p.xn.SLVS3) + kq.Sum(p => p.xn.SLVS4),
                            Tong = kq.Sum(p => p.xn.SLSH1) + kq.Sum(p => p.xn.SLSH2) - kq.Sum(p => p.xn.SLSH3) 
                                   + kq.Sum(p => p.xn.SLHH1) + kq.Sum(p => p.xn.SLHH2) + kq.Sum(p => p.xn.SLHH3) + kq.Sum(p => p.xn.SLHH4)
                                   + kq.Sum(p => p.xn.SLVS1) + kq.Sum(p => p.xn.SLVS2) + kq.Sum(p => p.xn.SLVS3) + kq.Sum(p => p.xn.SLVS4),
                         }).ToList();
                if (q.Count > 0) { rep.Tong.Value = "Tổng số: " + q.Count() + " lần."; }
                else { rep.Tong.Value = "Tổng số: ......... lần."; }
                rep.DataSource = q.ToList();
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
    }
}