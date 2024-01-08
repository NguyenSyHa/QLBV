using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCTacDongDieuChinhGiaTT37 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCTacDongDieuChinhGiaTT37()
        {
            InitializeComponent();
        }

        public void BindingData()
        {
            STT.DataBindings.Add("Text", DataSource, "STT");
            NoiDung.DataBindings.Add("Text", DataSource, "NoiDung");
            SL2017.DataBindings.Add("Text", DataSource, "SL2017").FormatString = DungChung.Bien.FormatString[0];
            TT2017.DataBindings.Add("Text", DataSource, "TT2017").FormatString = DungChung.Bien.FormatString[1];
            SL2018.DataBindings.Add("Text", DataSource, "SL2018").FormatString = DungChung.Bien.FormatString[0];
            TT2018.DataBindings.Add("Text", DataSource, "TT2018").FormatString = DungChung.Bien.FormatString[1];

        }


        private void ReportHeader_BeforePrint_1(object sender, CancelEventArgs e)
        {
            txtcqcq.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtcq.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void STT_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("STT") != "")
            {
                STT.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                NoiDung.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                SL2017.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                SL2018.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                TT2017.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                TT2018.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
            else
            {
                STT.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                NoiDung.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                SL2017.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                SL2018.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                TT2017.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                TT2018.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
        }
    }
}
