using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace QLBV.BaoCao
{
    public partial class Rep_ChiPhiBN_CungChiTraBHYT : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_ChiPhiBN_CungChiTraBHYT()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colKCB.DataBindings.Add("Text", DataSource, "MaBN");
            colTenBN.DataBindings.Add("Text", DataSource, "TenBN");
            colNamSinh.DataBindings.Add("Text", DataSource, "NamSinh");
            colDiaChi.DataBindings.Add("Text", DataSource, "DiaChi");
            colSoTien.DataBindings.Add("Text", DataSource, "SoTien").FormatString = DungChung.Bien.FormatString[1];
            colLyDo.DataBindings.Add("Text", DataSource, "LyDo");
        }
    }
}
