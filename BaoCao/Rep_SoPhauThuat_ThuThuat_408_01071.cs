using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_SoPhauThuat_ThuThuat_408_01071 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_SoPhauThuat_ThuThuat_408_01071()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData()
        {
            colGhiChu.DataBindings.Add("Text", DataSource, "NoiGui");
            colMaBNhan.DataBindings.Add("Text", DataSource, "MaBNhan");
            colTenBNhan.DataBindings.Add("Text", DataSource, "TenBNhan");
            //txtMaBNhan.DataBindings.Add("Text",DataSource,"MaBNhan");
            colNam.DataBindings.Add("Text", DataSource, "gioitinh");
            colNu.DataBindings.Add("Text", DataSource, "gioitinh_nu");
            //cell_Nam_RF.DataBindings.Add("Text", DataSource, "gioitinh");
            //cell_Nu_RF.DataBindings.Add("Text", DataSource, "gioitinh_nu");
            colBHYT.DataBindings.Add("Text", DataSource, "bhyt");
            //cell_CoBHYT_RF.DataBindings.Add("Text", DataSource, "bhyt");
            colDiaChi.DataBindings.Add("Text", DataSource, "diachi");
            colCDTruocPT.DataBindings.Add("Text", DataSource, "ChanDoan");
            colCDSauPT.DataBindings.Add("Text", DataSource, "KetQua");
            colNgayPT.DataBindings.Add("Text", DataSource, "ngaythang").FormatString = "{0:dd/MM HH:mm }";
            if(DungChung.Bien.MaBV=="12001")
                colPPPT.DataBindings.Add("Text", DataSource, "TenDV");  
            else
            colPPPT.DataBindings.Add("Text", DataSource, "KetLuan");
            colPPVC.DataBindings.Add("Text", DataSource, "LoiDan");
            colLoaiPT.DataBindings.Add("Text", DataSource, "LoaiTTPT");
            colBSCD.DataBindings.Add("Text", DataSource, "TenCBcd");
            colBSPT.DataBindings.Add("Text", DataSource, "TenCBth");
            colBSGM.DataBindings.Add("Text", DataSource, "DSCBTH");
            if(DungChung.Bien.MaBV == "30010")
            {
                colCKham.DataBindings.Add("Text", DataSource, "CKham");
                colCKhamT.DataBindings.Add("Text", DataSource, "CKham");
            }
            
           // GroupHeader1.GroupFields.Add(new GroupField("NoiGui"));
 
        }

       
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text=DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            GroupHeader1.Visible = false;
   
        }

   
    }
}
