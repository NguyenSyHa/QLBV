using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBbXacNhan : DevExpress.XtraReports.UI.XtraReport
    {
        public repBbXacNhan()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenThuocGh1.DataBindings.Add("Text", DataSource, "TenTieuNhom");
            colTenThuoc.DataBindings.Add("Text", DataSource, "TenDV");
            colDonViTinh.DataBindings.Add("Text", DataSource, "DonVi");
            colSoKS.DataBindings.Add("Text", DataSource, "SoLo");
            colNuocSX.DataBindings.Add("Text", DataSource, "NuocSX");
            txtHanDung.DataBindings.Add("Text", DataSource, "HanDung");
            colSoLuong.DataBindings.Add("Text", DataSource, "soLuongX").FormatString=DungChung.Bien.FormatString[1];
            colSoLuongTong.DataBindings.Add("Text", DataSource, "soLuongX").FormatString = DungChung.Bien.FormatString[1];
           // colSoLuongGh1.DataBindings.Add("Text", DataSource, "soLuongX").FormatString = DungChung.Bien.FormatString[1];
            GroupHeader1.GroupFields.Add(new GroupField("TenTieuNhom"));    
        }

        private void repBbXacNhan_BeforePrint(object sender, CancelEventArgs e)
        {
            TenCQCQ.Value = DungChung.Bien.TenCQCQ;
            TenCQ.Value = DungChung.Bien.TenCQ;
        }

        private void colHanDung_BeforePrint(object sender, CancelEventArgs e)
        {
            if (GetCurrentColumnValue("HanDung") != null && GetCurrentColumnValue("HanDung").ToString().Length >= 10)
            {
                colHanDung.Text = txtHanDung.Text.Substring(0, 10);
            }
        }

    }
}
