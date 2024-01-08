using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BC_HoatDongDTNoiTru : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BC_HoatDongDTNoiTru()
        {
            InitializeComponent();
        }

        public void BindingData()
        {
            khoa.DataBindings.Add("Text", DataSource, "TenKP");
            Songuoidauky.DataBindings.Add("Text", DataSource, "dauky");
            tongvv.DataBindings.Add("Text", DataSource, "tongdt");
            treemvv.DataBindings.Add("Text", DataSource, "treemnt");
            capcuuvv.DataBindings.Add("Text", DataSource, "capcuunt");
            songaydt.DataBindings.Add("Text", DataSource, "songaydt");
            tongsotuvong.DataBindings.Add("Text", DataSource, "tongtuvong");
            treemtuvong.DataBindings.Add("Text", DataSource, "treemtv");
            tuvongtruoc24h.DataBindings.Add("Text", DataSource, "tvtruoc24h");
            nguoibenhbhyt.DataBindings.Add("Text", DataSource, "bhyt");
            conlai.DataBindings.Add("Text", DataSource, "conlai");
            trungbinhnoitru.DataBindings.Add("Text", DataSource, "songaydttb");

            khoa.DataBindings.Add("Text", DataSource, "TenKP");
            Songuoidaukytong.DataBindings.Add("Text", DataSource, "dauky");
            tongvvtong.DataBindings.Add("Text", DataSource, "tongdt");
            treemvvtong.DataBindings.Add("Text", DataSource, "treemnt");
            capcuuvvtong.DataBindings.Add("Text", DataSource, "capcuunt");
            songaydttong.DataBindings.Add("Text", DataSource, "songaydt");
            tongsotuvongtong.DataBindings.Add("Text", DataSource, "tongtuvong");
            treemtuvongtong.DataBindings.Add("Text", DataSource, "treemtv");
            tuvongtruoc24htong.DataBindings.Add("Text", DataSource, "tvtruoc24h");
            nguoibenhbhyttong.DataBindings.Add("Text", DataSource, "bhyt");
            conlaitong.DataBindings.Add("Text", DataSource, "conlai");

        }
    }
}
