using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_congtacchuyentuyen_subChuyenDi : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_congtacchuyentuyen_subChuyenDi()
        {
            InitializeComponent();
        }


        internal void dataBinding()
        {
            cel_ChuyenKhoa.DataBindings.Add("Text", DataSource, "TenCK");
            cel_NgoaiTru.DataBindings.Add("Text", DataSource, "Ngoaitru");
            cel_Noitru.DataBindings.Add("Text", DataSource, "NoiTru");
            celChuyenDi.DataBindings.Add("Text", DataSource, "ChuyenDi");
            celTyLe.DataBindings.Add("Text", DataSource, "TyLe").FormatString = DungChung.Bien.FormatString[1];
            cel_Sthe.DataBindings.Add("Text", DataSource, "SThe");
            cel_1a.DataBindings.Add("Text", DataSource, "HTchuyen1a");
            cel_1b.DataBindings.Add("Text", DataSource, "HTchuyen1b");
            cel_2.DataBindings.Add("Text", DataSource, "HTchuyen2");
            cel_3.DataBindings.Add("Text", DataSource, "HTchuyen3");
            cel_4.DataBindings.Add("Text", DataSource, "LydoChuyen4");
            cel_5.DataBindings.Add("Text", DataSource, "LydoChuyen5");
            cel_Tuyen1.DataBindings.Add("Text", DataSource, "Tuyen1");
            cel_Tuyen2.DataBindings.Add("Text", DataSource, "Tuyen2");
            cel_Tuyen3.DataBindings.Add("Text", DataSource, "Tuyen3");
            cel_Tuyen4.DataBindings.Add("Text", DataSource, "Tuyen4");

           
            cel_NgoaiTruT.DataBindings.Add("Text", DataSource, "Ngoaitru");
            cel_NoitruT.DataBindings.Add("Text", DataSource, "NoiTru");
            celChuyenDiT.DataBindings.Add("Text", DataSource, "ChuyenDi");            
            cel_StheT.DataBindings.Add("Text", DataSource, "SThe");
            cel_1aT.DataBindings.Add("Text", DataSource, "HTchuyen1a");
            cel_1bT.DataBindings.Add("Text", DataSource, "HTchuyen1b");
            cel_2T.DataBindings.Add("Text", DataSource, "HTchuyen2");
            cel_3T.DataBindings.Add("Text", DataSource, "HTchuyen3");
            cel_4T.DataBindings.Add("Text", DataSource, "LydoChuyen4");
            cel_5T.DataBindings.Add("Text", DataSource, "LydoChuyen5");
            cel_Tuyen1T.DataBindings.Add("Text", DataSource, "Tuyen1");
            cel_Tuyen2T.DataBindings.Add("Text", DataSource, "Tuyen2");
            cel_Tuyen3T.DataBindings.Add("Text", DataSource, "Tuyen3");
            cel_Tuyen4T.DataBindings.Add("Text", DataSource, "Tuyen4");
            
        }

        private void celTyLe_T_BeforePrint(object sender, CancelEventArgs e)
        {
            int ngoaitru = 0, noitru = 0,  chuyentuyen = 0;
            if (!String.IsNullOrEmpty(cel_NgoaiTruT.Text))
            {
                ngoaitru = Convert.ToInt32(cel_NgoaiTruT.Text);
            }
            if (!String.IsNullOrEmpty(cel_NoitruT.Text))
            {
                noitru = Convert.ToInt32(cel_NoitruT.Text);
            }
            if (!String.IsNullOrEmpty(celChuyenDiT.Text))
            {
                chuyentuyen = Convert.ToInt32(celChuyenDiT.Text);
            }

            celTyLe_T.Text = (Math.Round(((float)chuyentuyen*100 / (noitru +ngoaitru)),2)).ToString();
        }
    }
}
