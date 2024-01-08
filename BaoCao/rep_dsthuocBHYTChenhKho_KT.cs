using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_dsthuocBHYTChenhKho_KT : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_dsthuocBHYTChenhKho_KT()
        {
            InitializeComponent();
            cqcq.Text = DungChung.Bien.TenCQCQ;
            cq.Text = DungChung.Bien.TenCQ;
        }

        public void BindingData()
        {
            GroupHeader1.GroupFields.Add(new GroupField("thang2"));
            ngaythang.DataBindings.Add("Text", DataSource, "thang2");
            xuatkt.DataBindings.Add("Text", DataSource, "xuatvp1").FormatString = DungChung.Bien.FormatString[1];
            tenthuoc.DataBindings.Add("Text", DataSource, "TenDV1");
            dvt.DataBindings.Add("Text", DataSource, "DonVi1");
            TonDK.DataBindings.Add("Text", DataSource, "TonDKSL1").FormatString = DungChung.Bien.FormatString[1];
            NhapTK.DataBindings.Add("Text", DataSource, "NhapTKSL1").FormatString = DungChung.Bien.FormatString[1]; ;
            TonCK.DataBindings.Add("Text", DataSource, "TonCKSL1").FormatString = DungChung.Bien.FormatString[1]; ;
            xuatkhoNT.DataBindings.Add("Text", DataSource, "XuatNgoaiTruSL1").FormatString = DungChung.Bien.FormatString[1];
            //xuatkhoKhac.DataBindings.Add("Text", DataSource, "TongXuatTKSL1").FormatString = DungChung.Bien.FormatString[1];
        }
    }
}
