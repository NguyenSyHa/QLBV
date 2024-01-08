using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_6NXT_22_24009 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_6NXT_22_24009()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            celSTTNhom.DataBindings.Add("Text", DataSource, "SttLaMa");
            lblSTTNhom.DataBindings.Add("Text", DataSource, "STTTN");
            row_TenTN.DataBindings.Add("Text", DataSource, "TenTN");
            cel_2.DataBindings.Add("Text", DataSource, "cot2");
            cel_3.DataBindings.Add("Text", DataSource, "TenThuongMai");
            cel_4.DataBindings.Add("Text", DataSource, "cot4");
            cel_5.DataBindings.Add("Text", DataSource, "cot5");
            cel_6.DataBindings.Add("Text", DataSource, "NCC");
            cel_DVT.DataBindings.Add("Text", DataSource, "DonVi");
            cel_DonGia.DataBindings.Add("Text", DataSource, "DonGia");
            cel_KQdauthau.DataBindings.Add("Text", DataSource, "KQdauthau");
            cel_dthau_dvitc.DataBindings.Add("Text", DataSource, "Dauthau_dvitc");
            cel_hthuckhac.DataBindings.Add("Text", DataSource, "hthuckhac");
            cel_TonDKSL.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[1];
            cel_TonDKTT.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            cel_MuaTKSL.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[1];
            cel_MuaTKTT.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            cel_SDnoitruSL.DataBindings.Add("Text", DataSource, "XuatNoiTruSL").FormatString = DungChung.Bien.FormatString[1];
            cel_SDnoitruTT.DataBindings.Add("Text", DataSource, "xuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
            cel_SDngtruSL.DataBindings.Add("Text", DataSource, "XuatNgoaiTruSL").FormatString = DungChung.Bien.FormatString[1];
            cel_SDngtruTT.DataBindings.Add("Text", DataSource, "xuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
            cel_TonCKSL.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[1];
            cel_TonCKTT.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            cel_dukienSL.DataBindings.Add("Text", DataSource, "dukienSL").FormatString = DungChung.Bien.FormatString[1];

            //TỔNG NHÓM
            
            cel_TonDKTT_T.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];           
            cel_MuaTKTT_T.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];           
            cel_SDnoitruTT_T.DataBindings.Add("Text", DataSource, "xuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];           
            cel_SDngtruTT_T.DataBindings.Add("Text", DataSource, "xuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];           
            cel_TonCKTT_T.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];

            //TỔNG CHUNG BÁO CÁO
            
            cel_TonDKTT_R.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];          
            cel_MuaTKTT_R.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];           
            cel_SDnoitruTT_R.DataBindings.Add("Text", DataSource, "xuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];          
            cel_SDngtruTT_R.DataBindings.Add("Text", DataSource, "xuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];           
            cel_TonCKTT_R.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            GroupHeader1.GroupFields.Add(new GroupField("STTTN"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            
            lab_TenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            cel_GD.Text = DungChung.Bien.GiamDoc;
            
        }
    }
}
