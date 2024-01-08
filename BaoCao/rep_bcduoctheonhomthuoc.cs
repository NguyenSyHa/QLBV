using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_bcduoctheonhomthuoc : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_bcduoctheonhomthuoc()
        {
            InitializeComponent();
        }
        public double[] ketquas = new double[14];
        public string CHUYEN(double A)
        {
            string ts = DungChung.Bien.FormatString[1];
            return string.Format(ts, A);
           

        }
        public void loat() 
        {
            CQ.Value = DungChung.Bien.TenCQ;
            cqcq.Value = DungChung.Bien.TenCQCQ;
            A1.Text = CHUYEN((ketquas[0] + ketquas[1] + ketquas[2] + ketquas[3]));
            A2.Text = CHUYEN(ketquas[0]);
            A3.Text =CHUYEN(ketquas[1]);
            A4.Text = CHUYEN(ketquas[2]);
            A5.Text = CHUYEN(ketquas[3]);
            A6.Text = CHUYEN(ketquas[4]);
            A7.Text = CHUYEN(ketquas[5]);
            A8.Text = CHUYEN(ketquas[5]);
            A9.Text = CHUYEN(ketquas[6]);
            //A10.Text = ketquas[9];
            // tai bien do su dung thuoc
            //A11.Text = ketquas[10];
            // tong hai 
            A12.Text =CHUYEN(ketquas[12] + ketquas[13]);
            //thuoc trong nuoc
            A13.Text =CHUYEN(ketquas[12]);
            // thuoc ngaoi nuoc
            A14.Text =CHUYEN(ketquas[13]);
        }

        
    }
}
