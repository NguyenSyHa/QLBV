using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class rep_14 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_14()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colBenhnhandungtuyen.DataBindings.Add("Text", DataSource, "Benhnhandungtuyen").FormatString = DungChung.Bien.FormatString[1];
            colBenhnhantraituyen.DataBindings.Add("Text", DataSource, "Benhnhantraituyen").FormatString = DungChung.Bien.FormatString[1];
            colBHXHchitra.DataBindings.Add("Text", DataSource, "BHXHchitra").FormatString = DungChung.Bien.FormatString[1];
            colBHXHtuchoithanhtoan.DataBindings.Add("Text", DataSource, "BHXHtuchoithanhtoan").FormatString = DungChung.Bien.FormatString[1];
            colCDHA.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colDVKTcao.DataBindings.Add("Text", DataSource, "DVKTcao").FormatString = DungChung.Bien.FormatString[1];
            colSoluotdungtuyen.DataBindings.Add("Text", DataSource, "Soluotdungtuyen");
            colSoluotTTTT.DataBindings.Add("Text", DataSource, "SoluotTTTT");
            colSoluottraituyen.DataBindings.Add("Text", DataSource, "Soluottraituyen");
            colSTT.DataBindings.Add("Text", DataSource, "STT");
            colTentuyen.DataBindings.Add("Text", DataSource, "Tentuyen");
            colTienkham.DataBindings.Add("Text", DataSource, "Tienkham").FormatString = DungChung.Bien.FormatString[1];
            colTongcong.DataBindings.Add("Text", DataSource, "Tongcong").FormatString = DungChung.Bien.FormatString[1];
            colTTPT.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colThuocDT.DataBindings.Add("Text", DataSource, "ThuocDT").FormatString = DungChung.Bien.FormatString[1];
            colThuocthaighep.DataBindings.Add("Text", DataSource, "Thuocthaighep").FormatString = DungChung.Bien.FormatString[1];
            colVanchuyen.DataBindings.Add("Text", DataSource, "Vanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colVTYTtieuhao.DataBindings.Add("Text", DataSource, "VTYTtieuhao").FormatString = DungChung.Bien.FormatString[1];
            colVTYTthaythe.DataBindings.Add("Text", DataSource, "VTYTthaythe").FormatString = DungChung.Bien.FormatString[1];
            colXetnghiem.DataBindings.Add("Text", DataSource, "Xetnghiem").FormatString = DungChung.Bien.FormatString[1];
            colMau.DataBindings.Add("Text", DataSource, "mau").FormatString = DungChung.Bien.FormatString[1];
            colRFBHXH.DataBindings.Add("Text", DataSource, "BHXHchitra").FormatString = DungChung.Bien.FormatString[1];
            colRFCDHA.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colRFDungtuyen.DataBindings.Add("Text", DataSource, "Soluotdungtuyen");
            colRFDVKTC.DataBindings.Add("Text", DataSource, "DVKTcao").FormatString = DungChung.Bien.FormatString[1];
            colRFMau.DataBindings.Add("Text", DataSource, "mau").FormatString = DungChung.Bien.FormatString[1];
            colRFTienDT.DataBindings.Add("Text", DataSource, "Benhnhandungtuyen").FormatString = DungChung.Bien.FormatString[1];
            colRFTienkham.DataBindings.Add("Text", DataSource, "Tienkham").FormatString = DungChung.Bien.FormatString[1];
            colRFTKTG.DataBindings.Add("Text", DataSource, "Thuocthaighep").FormatString = DungChung.Bien.FormatString[1];
            colRFTongcong.DataBindings.Add("Text", DataSource, "Tongcong").FormatString = DungChung.Bien.FormatString[1];
            colRFTTPT.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colRFTtraituyen.DataBindings.Add("Text", DataSource, "Benhnhantraituyen").FormatString = DungChung.Bien.FormatString[1];
            colRFThuoc.DataBindings.Add("Text", DataSource, "ThuocDT").FormatString = DungChung.Bien.FormatString[1];
            colRFTraituyen.DataBindings.Add("Text", DataSource, "Soluottraituyen");
            colRFVanchuyen.DataBindings.Add("Text", DataSource, "Vanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colRFVTYTtieuhao.DataBindings.Add("Text", DataSource, "VTYTtieuhao").FormatString = DungChung.Bien.FormatString[1];
            colRFVTYTThaythe.DataBindings.Add("Text", DataSource, "VTYTthaythe").FormatString = DungChung.Bien.FormatString[1];
            colRFxetnghiem.DataBindings.Add("Text", DataSource, "Xetnghiem").FormatString = DungChung.Bien.FormatString[1];
            colRFBHXHtuchoi.DataBindings.Add("Text", DataSource, "BHXHtuchoithanhtoan").FormatString = DungChung.Bien.FormatString[1];
            colRFSoluottructiep.DataBindings.Add("Text", DataSource, "SLTT");
            txtHangBV.DataBindings.Add("Text", DataSource, "HangBV");
            colRFTT2.DataBindings.Add("Text", DataSource, "TT2");
            colRFTT3.DataBindings.Add("Text", DataSource, "TT3");
            GroupHeader1.GroupFields.Add(new GroupField("HangBV"));         
            
        }
        int tt1 = 0;
        int dt1 = 0;
        int dt2 = 0;
        int tt2 = 0;
        int dt3 = 0;
        int tt3 = 0;
        int dt4 = 0;
        int tt4 = 0;
        int dt5 = 0;
        int tt5 = 0;
        int dt6 = 0;
        int tt6 = 0;
        int dt = 0;
        int tt = 0;
        //DateTime ngaytu1 = System.DateTime.Now.Date;
        //DateTime ngayden1 = System.DateTime.Now.Date;
  
      

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtBenhvien.Text = DungChung.Bien.TenCQ;
            txtMacs.Text = DungChung.Bien.MaBV;
            txtSoyte.Text = DungChung.Bien.TenCQCQ;
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("HangBV") != null)
            {
                string HangBV = this.GetCurrentColumnValue("HangBV").ToString().Trim();
                switch (HangBV)
                {
                 
                    case "A":
                        colGHTenBV.Text = "A. TRUNG ƯƠNG";
                        break;
                    case "B":
                        colGHTenBV.Text = "B. TUYẾN TỈNH";
                        break;
                    case "C":
                        colGHTenBV.Text = "C. TUYẾN HUYỆN";
                        break;
                    case "D":
                        colGHTenBV.Text = "D. TUYẾN XÃ";
                        break;
                }
            }
        }

       
    }
}
        

     

        
   