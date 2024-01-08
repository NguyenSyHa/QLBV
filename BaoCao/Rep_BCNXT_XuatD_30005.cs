using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BCNXT_XuatD_30005 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BCNXT_XuatD_30005()
        {
            InitializeComponent();
        }
        public void Bindingdata()
        {
            grp_Tieunhom.DataBindings.Add("Text", DataSource, "TenTN");
            cel_Tenthuoc.DataBindings.Add("Text", DataSource, "TenDV");
            cel_DVT.DataBindings.Add("Text", DataSource, "DonVi");
            cel_DonGia.DataBindings.Add("Text", DataSource, "DonGia");            
            cel_XuatND.DataBindings.Add("Text", DataSource, "XuatNhanDan");
            cel_XuatND_TT.DataBindings.Add("Text", DataSource, "XuatNhanDan_TT").FormatString = DungChung.Bien.FormatString[1];
            cel_XuatBH.DataBindings.Add("Text", DataSource, "XuatBH");
            cel_XuatBH_TT.DataBindings.Add("Text", DataSource, "XuatBH_TT").FormatString = DungChung.Bien.FormatString[1];  
            cel_Xuatmien.DataBindings.Add("Text", DataSource, "XuatMien");
            cel_Xuatmien_TT.DataBindings.Add("Text", DataSource, "XuatMien_TT").FormatString = DungChung.Bien.FormatString[1];  
            cel_Tongxuat.DataBindings.Add("Text", DataSource, "TongXuat");
            cel_Tongxuat_TT.DataBindings.Add("Text", DataSource, "TongXuat_TT").FormatString = DungChung.Bien.FormatString[1];

            cel_XuatND_G.DataBindings.Add("Text", DataSource, "XuatNhanDan_TT").FormatString = DungChung.Bien.FormatString[1];            
            cel_XuatBH_G.DataBindings.Add("Text", DataSource, "XuatBH_TT").FormatString = DungChung.Bien.FormatString[1];          
            cel_Xuatmien_G.DataBindings.Add("Text", DataSource, "XuatMien_TT").FormatString = DungChung.Bien.FormatString[1];           
            cel_Tongxuat_G.DataBindings.Add("Text", DataSource, "TongXuat_TT").FormatString = DungChung.Bien.FormatString[1];  
          
            ////GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
           
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celTruongKhoa.Text = DungChung.Bien.TruongKhoaDuoc;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
            colThuKho.Text = DungChung.Bien.ThuKho;
        }
    }
}
