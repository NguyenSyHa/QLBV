using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repThBNNoiTruTheoKhoa_HA : DevExpress.XtraReports.UI.XtraReport
    {
        public repThBNNoiTruTheoKhoa_HA()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colKhoa.DataBindings.Add("Text", DataSource, "TenKP");
            // txtMax.DataBindings.Add("Text", DataSource, "Max");
            colTongSo.DataBindings.Add("Text", DataSource, "TongSo").FormatString = DungChung.Bien.FormatString[1];
            colTongSoT.DataBindings.Add("Text", DataSource, "TongSo").FormatString = DungChung.Bien.FormatString[1];
            colTE6.DataBindings.Add("Text", DataSource, "TE6").FormatString = DungChung.Bien.FormatString[1];
            colTE6T.DataBindings.Add("Text", DataSource, "TE6").FormatString = DungChung.Bien.FormatString[1];
            colTE15.DataBindings.Add("Text", DataSource, "TE15").FormatString = DungChung.Bien.FormatString[1];
            colTE15T.DataBindings.Add("Text", DataSource, "TE15").FormatString = DungChung.Bien.FormatString[1];
            col60.DataBindings.Add("Text", DataSource, "T60").FormatString = DungChung.Bien.FormatString[1];
            col60T.DataBindings.Add("Text", DataSource, "T60").FormatString = DungChung.Bien.FormatString[1];
            col80.DataBindings.Add("Text", DataSource, "T80").FormatString = DungChung.Bien.FormatString[1];
            col80T.DataBindings.Add("Text", DataSource, "T80").FormatString = DungChung.Bien.FormatString[1];
            colNu.DataBindings.Add("Text", DataSource, "TongSoNu").FormatString = DungChung.Bien.FormatString[1];
            colNuT.DataBindings.Add("Text", DataSource, "TongSoNu").FormatString = DungChung.Bien.FormatString[1];
            colNam.DataBindings.Add("Text", DataSource, "TongSoNam").FormatString = DungChung.Bien.FormatString[1];
            colNamT.DataBindings.Add("Text", DataSource, "TongSoNam").FormatString = DungChung.Bien.FormatString[1];
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            TenCQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
            TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBang.Text = DungChung.Bien.NguoiLapBieu;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
        }

        private void xrLabel7_AfterPrint(object sender, EventArgs e)
        {

        }
        //string kp = "";
        //private string tenkp(int id)
        //{
        //    string tenkp = "";
        //    QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        //    var sql = (from bnkb in _data.BNKBs.Where(p => p.IDKB == id)
        //               join kp in _data.KPhongs on bnkb.MaKP equals kp.MaKP
        //               select new { kp.TenKP }).ToList();
        //               //group new {kp} by new {kp.TenKP} into kq
        //               //select new { TenKP=kq.Key.TenKP}).ToList();
        //    if (sql.Count > 0)
        //    {
        //        tenkp = sql.Distinct().First().TenKP;
        //    }
        //    else
        //    {
        //        tenkp = "";
        //    }
        //    return tenkp;
        //}

        private void colKhoa_BeforePrint_1(object sender, CancelEventArgs e)
        {

            //int id = 0;
            //if (GetCurrentColumnValue("Max") != null)
            //    id = Convert.ToInt32(GetCurrentColumnValue("Max"));
            //colKhoa.Text = tenkp(id);
            ////colMaBenh.Text = icd;
        }


        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

            int v;
            if (Convert.ToInt32(this.GetCurrentColumnValue("TongSo")) > 0)
            {

                v = Convert.ToInt32(this.GetCurrentColumnValue("TongSo"));
                if (v == 0)
                {
                    colTongSo.Text = "";
                }
                else { colTongSo.Text = v.ToString("#,##"); }
            }
            if (Convert.ToInt32(this.GetCurrentColumnValue("TE6")) > 0)
            {

                v = Convert.ToInt32(this.GetCurrentColumnValue("TE6"));
                if (v == 0)
                {
                    colTE6.Text = "";
                }
                else { colTE6.Text = v.ToString("#,##"); }
            }
            if (Convert.ToInt32(this.GetCurrentColumnValue("TE15")) > 0)
            {

                v = Convert.ToInt32(this.GetCurrentColumnValue("TE15"));
                if (v == 0)
                {
                    colTE15.Text = "";
                }
                else { colTE15.Text = v.ToString("#,##"); }
            }
            if (Convert.ToInt32(this.GetCurrentColumnValue("T60")) > 0)
            {

                v = Convert.ToInt32(this.GetCurrentColumnValue("T60"));
                if (v == 0)
                {
                    col60.Text = "";
                }
                else { col60.Text = v.ToString("#,##"); }
            }
            if (Convert.ToInt32(this.GetCurrentColumnValue("T80")) > 0)
            {

                v = Convert.ToInt32(this.GetCurrentColumnValue("T80"));
                if (v == 0)
                {
                    col80.Text = "";
                }
                else { col80.Text = v.ToString("#,##"); }
            }
            if (Convert.ToInt32(this.GetCurrentColumnValue("TongSoNam")) > 0)
            {

                v = Convert.ToInt32(this.GetCurrentColumnValue("TongSoNam"));
                if (v == 0)
                {
                    colNam.Text = "";
                }
                else { colNam.Text = v.ToString("#,##"); }
            }
            if (Convert.ToInt32(this.GetCurrentColumnValue("TongSoNu")) > 0)
            {

                v = Convert.ToInt32(this.GetCurrentColumnValue("TongSoNu"));
                if (v == 0)
                {
                    colNu.Text = "";
                }
                else { colNu.Text = v.ToString("#,##"); }
            }

        }




    }
}
