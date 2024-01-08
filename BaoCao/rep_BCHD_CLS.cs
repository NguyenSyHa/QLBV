using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class rep_BCHD_CLS : DevExpress.XtraReports.UI.XtraReport
    {
        //private DateTime ngaytu;
        //private DateTime ngayden;
        private string _dtuong;
        private int _noitru;
        private int loaiNgay;
        bool hienthi = false;
        private System.Collections.Generic.List<FormThamSo.frm_BCHD_CLS.KetQua> q_KQRiengKhoa;
        public rep_BCHD_CLS()
        {
            InitializeComponent();
        }

        

        public rep_BCHD_CLS(System.Collections.Generic.List<FormThamSo.frm_BCHD_CLS.KetQua> q_KQRiengKhoa, bool hienthi)
        {
            this.hienthi = hienthi;
            this.q_KQRiengKhoa = q_KQRiengKhoa;
            InitializeComponent();
            
        }
        public void DataBindings()
        {
            colTenTN.DataBindings.Add("Text", DataSource, "TenTN");
            colSoLuong.DataBindings.Add("Text", DataSource, "TongXN").FormatString=DungChung.Bien.FormatString[0];
            colTongSoXN.DataBindings.Add("Text", DataSource, "TongXN").FormatString = DungChung.Bien.FormatString[0];
            colNhomDV.DataBindings.Add("Text", DataSource, "TenNhom");
            colTongSL_RF.DataBindings.Add("Text", DataSource, "TongXN").FormatString = DungChung.Bien.FormatString[0];
            colSoXNTB.DataBindings.Add("Text", DataSource, "TongL").FormatString = DungChung.Bien.FormatString[0];
            col_tonggrl.DataBindings.Add("Text", DataSource, "TongL").FormatString = DungChung.Bien.FormatString[0];
            col_tongl.DataBindings.Add("Text", DataSource, "TongL").FormatString = DungChung.Bien.FormatString[0];
            GroupHeader1.GroupFields.Add(new GroupField("TenNhom"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lbCQ.Text = DungChung.Bien.TenCQ.ToUpper();
           
        }
        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
            rep_BCHD_CLS_sub sub = (rep_BCHD_CLS_sub)xrSubreport1.ReportSource;
            sub.DataSource = q_KQRiengKhoa;
            sub.binding();
           
            xrSubreport1.Visible = hienthi;
           

            //data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //rep_BCHD_CLS_sub sub = new rep_BCHD_CLS_sub();
            //var q = data.KPhongs.ToList();
          
            //sub.DataSource = q;
            //sub.binding();
        }
    }
}
