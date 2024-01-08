using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_TkChiPhiKCBBHYT : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_TkChiPhiKCBBHYT()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            txtBN.DataBindings.Add("Text", DataSource, "NoiTru");
            colSoPhieu.DataBindings.Add("Text", DataSource, "MaBNhan");
            colMaThe.DataBindings.Add("Text", DataSource, "SThe");
            colTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            colNamSinh.DataBindings.Add("Text", DataSource, "NamSinh");
            colDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            colNoiKCBBD.DataBindings.Add("Text", DataSource, "NoiKCBBD");
            colGTriTu.DataBindings.Add("Text", DataSource, "HanBHTu").FormatString = "{0:dd/MM/yyyy}";
            colGTriDen.DataBindings.Add("Text", DataSource, "HanBHDen").FormatString="{0:dd/MM/yyyy}";
            colNgayVao.DataBindings.Add("Text", DataSource, "NgayVao").FormatString = "{0:dd/MM/yyyy}";
            colNgayRa.DataBindings.Add("Text", DataSource, "NgayRa").FormatString = "{0:dd/MM/yyyy}";
            colTongDe.DataBindings.Add("Text", DataSource, "Tong").FormatString=DungChung.Bien.FormatString[1];
            colTongGR.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];
            colTongRe.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];
            GroupHeader1.GroupFields.Add(new GroupField("NoiTru"));

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ;
            
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
           string t = "";
            if (this.GetCurrentColumnValue("NoiTru") != null)
            {
                t = this.GetCurrentColumnValue("NoiTru").ToString();
                if (t == "0")
                {
                    colPLoaiBN.Text = "Ngoại trú";
                }
                if (t == "1")
                {
                    colPLoaiBN.Text = "Nội trú";
                }
            }
            string _pl = "";
            _pl = BN.Value.ToString();

            if (_pl != "1")
            {
                GroupHeader1.Visible = false;
                GroupFooter1.Visible = false ;
            }
            

        }

    

    }
}
