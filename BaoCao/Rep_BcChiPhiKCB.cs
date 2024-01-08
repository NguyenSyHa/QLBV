using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BcChiPhiKCB : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcChiPhiKCB()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV1");
            colSTTNhom.DataBindings.Add("Text",DataSource,"STTNhom1");
            colMaBN.DataBindings.Add("Text",DataSource,"MaBN1");
            colTenBN.DataBindings.Add("Text",DataSource,"TenBN1");
            colSThe.DataBindings.Add("Text",DataSource,"SThe1");
            colMaCD.DataBindings.Add("Text",DataSource,"MaICD1");
            colNgayVao.DataBindings.Add("Text",DataSource,"NgayVao1");
            colNgayRa.DataBindings.Add("Text", DataSource, "NgayRa1");
            colKP.DataBindings.Add("Text",DataSource,"KPhong1");
            colBS.DataBindings.Add("Text", DataSource, "BacSy1");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia1").FormatString = DungChung.Bien.FormatString[1];
            colSL.DataBindings.Add("Text",DataSource,"Sl1").FormatString=DungChung.Bien.FormatString[0];
            colTT.DataBindings.Add("Text", DataSource, "ThanhTien1").FormatString = DungChung.Bien.FormatString[1];
            colDiaChi.DataBindings.Add("Text", DataSource, "DiaChi1");
            colThanhTienT.DataBindings.Add("Text", DataSource, "ThanhTien1");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBang.Text = DungChung.Bien.NguoiLapBieu;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
        }

    }
}
