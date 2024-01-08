using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class rep_ChiDaoTuyen : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_ChiDaoTuyen()
        {
            InitializeComponent();
        }
        public rep_ChiDaoTuyen(List<DeTai> list)
        {
            InitializeComponent();
            _list = list;
        }
        public List<DeTai> _list = new List<DeTai>();
        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
           
            rep_sub_ChiDaoTuyen_DeTaiKH _repSub = (rep_sub_ChiDaoTuyen_DeTaiKH)xrSubreport1.ReportSource;
            _repSub.DataSource = _list;
            _repSub.BindingData();
            _repSub.CreateDocument();
        }


       
    }
}
