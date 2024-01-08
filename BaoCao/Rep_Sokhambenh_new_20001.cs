using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_Sokhambenh_new_20001 : DevExpress.XtraReports.UI.XtraReport
    {
        private bool htDotuoi = false;
        private bool htNgay = false;

        public Rep_Sokhambenh_new_20001()
        {
            InitializeComponent();
        }

        public Rep_Sokhambenh_new_20001(bool htDotuoi, bool htNgay)
        {
            // TODO: Complete member initialization
            this.htDotuoi = htDotuoi;
            this.htNgay = htNgay;
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenBN.DataBindings.Add("Text", DataSource, "tenbn");
            //txtMaBN.DataBindings.Add("Text", DataSource, "maBN");
            colNam.DataBindings.Add("Text", DataSource, "tuoin");
            colNu.DataBindings.Add("Text", DataSource, "tuoinu");
            colDiachi.DataBindings.Add("Text", DataSource, "Diachi");
            colSothe.DataBindings.Add("Text", DataSource, "Sothe");
            colNoiGT.DataBindings.Add("Text", DataSource, "NoiGT");
            colCDTuyenduoi.DataBindings.Add("Text", DataSource, "CDNoiGT");
            //if (DungChung.Bien.MaBV == "30003")
            //{
            colCDKKB.DataBindings.Add("Text", DataSource, "ticd");
            //}
            //else
            //{ 
            //    colCDKKB.DataBindings.Add("Text", DataSource, "CD"); 
            //}
            colICD.DataBindings.Add("Text", DataSource, "cd");
            colCDDT.DataBindings.Add("Text", DataSource, "cddt");
            colVVien.DataBindings.Add("Text", DataSource, "VV1");
            colTuyentren.DataBindings.Add("Text", DataSource, "TT1");
            colTuyenduoi.DataBindings.Add("Text", DataSource, "TD1");
            colNgoaitru.DataBindings.Add("Text", DataSource, "NT1");
            colVenha.DataBindings.Add("Text", DataSource, "VN1");
            colTT.DataBindings.Add("Text", DataSource, "ThuThuat1");
            colChuyenkhoa.DataBindings.Add("Text", DataSource, "KhamCK1");
            colBS.DataBindings.Add("Text", DataSource, "TenBS");
            colThuphi.DataBindings.Add("Text", DataSource, "dtuongtp");
            colMienphi.DataBindings.Add("Text", DataSource, "dtuongbh");
            colCapcuu.DataBindings.Add("Text", DataSource, "capcuu");
            if (htDotuoi)
            {
                celDoTuoi.DataBindings.Add("Text", DataSource, "DoTuoi");               
                GroupHeader2.GroupFields.Add(new GroupField("DoTuoi"));
            }

            if(htNgay)
            {                
                celNgayKham.DataBindings.Add("Text", DataSource, "nkb").FormatString = "{0: dd/MM/yyyy}";
                GroupHeader1.GroupFields.Add(new GroupField("nkb"));
              
            }

            //TXTCT.DataBindings.Add("Text", DataSource, "ct");
        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenBV.Text = DungChung.Bien.TenCQ.ToUpper();
            txtTecCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();

        }

        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (htDotuoi)
                GroupHeader2.Visible = true;
            else
                GroupHeader2.Visible = false;
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (htNgay)
                GroupHeader1.Visible = true;
            else
                GroupHeader1.Visible = false;
        }

      
       
    }
}
