using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class PhieuMuonTuTrang : DevExpress.XtraReports.UI.XtraReport
    {
        public PhieuMuonTuTrang()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTendodung.DataBindings.Add("Text", DataSource, "TenDV");
            colDVT.DataBindings.Add("Text", DataSource, "DonVi");
            colSL.DataBindings.Add("Text", DataSource, "SoLuong");
            colGhiChu.DataBindings.Add("Text", DataSource, "GhiChu");
            colTongSL.DataBindings.Add("Text", DataSource, "SoLuong");


        }

    }
    
}
