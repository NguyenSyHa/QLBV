using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_SoPhauThuat_ThuThuat_A4_20001 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_SoPhauThuat_ThuThuat_A4_20001()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData()
        {
           //colGhiChu.DataBindings.Add("Text", DataSource, "NoiGui");
            colHoTen.DataBindings.Add("Text",DataSource,"TenBNhan");
           colNam.DataBindings.Add("Text",DataSource,"nam");
            colNu.DataBindings.Add("Text",DataSource,"nu");
            colBHYT.DataBindings.Add("Text",DataSource,"BHYT");
            colBuongGiuong.DataBindings.Add("Text", DataSource, "BuongGiuong");
            colChanDoan.DataBindings.Add("Text", DataSource, "ChanDoan");
            colSoLan.DataBindings.Add("Text", DataSource, "SoLan");
            //colSauPT.DataBindings.Add("Text", DataSource, "KetLuan");
            colTHngaytu.DataBindings.Add("Text", DataSource, "NgayTHtu").FormatString="{0:dd/MM/yy}";
            colThucHienNgayDen.DataBindings.Add("Text", DataSource, "NgayTHden").FormatString = "{0:dd/MM/yy}";
            colYLenh.DataBindings.Add("Text", DataSource, "YeuCau");
            colTTVChinh.DataBindings.Add("Text", DataSource, "TTVChinh");
            colTTVPhu.DataBindings.Add("Text", DataSource, "TTVPhu");
            colTTVP2.DataBindings.Add("Text", DataSource, "TTVPhu2");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
          //  GroupHeader1.GroupFields.Add(new GroupField("NoiGui"));
 
        }

       
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text=DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
          
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
          
        }
    }
}
