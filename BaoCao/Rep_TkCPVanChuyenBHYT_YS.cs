using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_TkCPVanChuyenBHYT_YS : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_TkCPVanChuyenBHYT_YS()
        {
            InitializeComponent();
        }
        List<BenhVien> _lbenhvien = new List<BenhVien>();
        QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
  
        public void BindingData()
        {
           
            colTenBNhan.DataBindings.Add("Text", DataSource, "TenBNhan");
            colSoThe.DataBindings.Add("Text",DataSource,"SThe");
            colKCBBanDau.DataBindings.Add("Text", DataSource, "MaCS");
            colKCBChuyenDen.DataBindings.Add("Text", DataSource, "MaBVC");
            colSoLenh.DataBindings.Add("Text", DataSource, "SoGT");
            colSoKmVC.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[0];
            colTienCSo.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTienCSoGf.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString=DungChung.Bien.FormatString[1];
            colTienCSoRf.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTienBH.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTienBHGf.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTienBHRf.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            txtNoiTinh.DataBindings.Add("Text", DataSource, "NoiTinh");
            colKhoaPhong.DataBindings.Add("Text", DataSource, "TenKP");
            colNgayChuyen.DataBindings.Add("Text", DataSource, "NgayRa");
            GroupHeader1.GroupFields.Add(new GroupField("NoiTinh"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            _lbenhvien = _dataContext.BenhViens.ToList();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("NoiTinh") != null)
            {
                string noitinh = this.GetCurrentColumnValue("NoiTinh").ToString();
                switch (noitinh)
                {
                    case "1":
                        colNoiTinh.Text = " I. Bệnh nhân đăng ký KCB ban đầu tại cơ sở KCB ".ToUpper();
                    break;
                    case "2":
                        colNoiTinh.Text = " II. Bệnh nhân chuyển đến từ cơ sở KCB khác trong tỉnh ".ToUpper();
                    break;
                    case "3":
                        colNoiTinh.Text = " II. Bệnh nhân ngoại tỉnh đến ".ToUpper();
                    break;
                }
            }
        }

        private void colKCBBanDau_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("MaCS") != null)
            {
                string _macs = GetCurrentColumnValue("MaCS").ToString();
                var cs = _lbenhvien.Where(p => p.MaBV == _macs).Select(p=>p.TenBV).ToList();
                if (cs.Count > 0)
                    colKCBBanDau.Text = cs.First().ToString();
            }
        }

        private void colKCBChuyenDen_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("MaBVC") != null)
            {
                string _macs = GetCurrentColumnValue("MaBVC").ToString();
                var cs = _lbenhvien.Where(p => p.MaBV == _macs).Select(p => p.TenBV).ToList();
                if (cs.Count > 0)
                    colKCBChuyenDen.Text = cs.First().ToString();
            }
        }
    }
}
