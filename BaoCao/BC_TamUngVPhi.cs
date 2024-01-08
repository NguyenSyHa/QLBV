using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class BC_TamUngVPhi : DevExpress.XtraReports.UI.XtraReport
    {
        public BC_TamUngVPhi()
        {
            InitializeComponent();
        }
        public void  BindingData(){
            colSTT.DataBindings.Add("Text", DataSource, "STT");
            colTen.DataBindings.Add("Text", DataSource, "TenBNhan");
            colTuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            colBL.DataBindings.Add("Text", DataSource, "SoBL");
            colDChi.DataBindings.Add("Text", DataSource, "DChi");
            colKhoa.DataBindings.Add("Text", DataSource, "Khoa");
            colVP.DataBindings.Add("Text", DataSource, "VP").FormatString="{0:#,###}";
            colBaoHiem.DataBindings.Add("Text", DataSource, "BaoHiem").FormatString = "{0:#,###}";
            colTong.DataBindings.Add("Text", DataSource, "Tong").FormatString = "{0:#,###}";
            NgayThu.DataBindings.Add("Text",DataSource,"NgayThu").FormatString="Ngày: {0:dd/MM/yyyy}";
            GroupHeader1.GroupFields.Add(new GroupField("NgayThu"));
           
        }

        private void BC_TamUngVPhi_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

    }
}
