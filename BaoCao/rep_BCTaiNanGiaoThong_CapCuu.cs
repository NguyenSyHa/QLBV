using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class rep_BCTaiNanGiaoThong_CapCuu : DevExpress.XtraReports.UI.XtraReport
    {      

        public rep_BCTaiNanGiaoThong_CapCuu()
        {
            InitializeComponent();
        }
        System.Collections.Generic.List<FormThamSo.frm_BCTaiNanGiaoThong_CapCuu.clsTaiNanGT> _l = new System.Collections.Generic.List<FormThamSo.frm_BCTaiNanGiaoThong_CapCuu.clsTaiNanGT>();
        public rep_BCTaiNanGiaoThong_CapCuu(System.Collections.Generic.List<FormThamSo.frm_BCTaiNanGiaoThong_CapCuu.clsTaiNanGT> _l)
        {
            InitializeComponent();
            this._l = _l;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lab_TenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            lab_Tieude.Text = "CÁC TRƯỜNG HỢP TAI NẠN GIAO THÔNG ĐẾN CẤP CỨU TẠI BỆNH VIỆN";
           
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {           
            var a = _l.First();
            cel1Nam.Text = a.Nam == 0 ? "" : a.Nam.ToString();
            cel1Nu.Text = a.Nu == 0 ? "" : a.Nu.ToString();
            cel14.Text = a.SL4 == 0 ? "" : a.SL4.ToString();
            cel114.Text = a.SL14 == 0 ? "" : a.SL14.ToString();
            cel119.Text = a.SL19 == 0 ? "" : a.SL19.ToString();
            cel159.Text = a.SL59 == 0 ? "" : a.SL59.ToString();
            cel160.Text = a.SL60 == 0 ? "" : a.SL60.ToString();

            a = _l.Skip(1).First();
            cel2Nam.Text = a.Nam == 0 ? "" : a.Nam.ToString();
            cel2Nu.Text = a.Nu == 0 ? "" : a.Nu.ToString();
            cel24.Text = a.SL4 == 0 ? "" : a.SL4.ToString();
            cel214.Text = a.SL14 == 0 ? "" : a.SL14.ToString();
            cel219.Text = a.SL19 == 0 ? "" : a.SL19.ToString();
            cel259.Text = a.SL59 == 0 ? "" : a.SL59.ToString();
            cel260.Text = a.SL60 == 0 ? "" : a.SL60.ToString();

            cel2bNam.Text = a.Nam == 0 ? "" : a.Nam.ToString();
            cel2bNu.Text = a.Nu == 0 ? "" : a.Nu.ToString();
            cel2b4.Text = a.SL4 == 0 ? "" : a.SL4.ToString();
            cel2b14.Text = a.SL14 == 0 ? "" : a.SL14.ToString();
            cel2b19.Text = a.SL19 == 0 ? "" : a.SL19.ToString();
            cel2b59.Text = a.SL59 == 0 ? "" : a.SL59.ToString();
            cel2b60.Text = a.SL60 == 0 ? "" : a.SL60.ToString();
            a = _l.Skip(2).First();
            cel3Nam.Text = a.Nam == 0 ? "" : a.Nam.ToString();
            cel3Nu.Text = a.Nu == 0 ? "" : a.Nu.ToString();
            cel34.Text = a.SL4 == 0 ? "" : a.SL4.ToString();
            cel314.Text = a.SL14 == 0 ? "" : a.SL14.ToString();
            cel319.Text = a.SL19 == 0 ? "" : a.SL19.ToString();
            cel359.Text = a.SL59 == 0 ? "" : a.SL59.ToString();
            cel360.Text = a.SL60 == 0 ? "" : a.SL60.ToString();
            a = _l.Skip(3).First();
            cel4Nam.Text = a.Nam == 0 ? "" : a.Nam.ToString();
            cel4Nu.Text = a.Nu == 0 ? "" : a.Nu.ToString();
            cel44.Text = a.SL4 == 0 ? "" : a.SL4.ToString();
            cel414.Text = a.SL14 == 0 ? "" : a.SL14.ToString();
            cel419.Text = a.SL19 == 0 ? "" : a.SL19.ToString();
            cel459.Text = a.SL59 == 0 ? "" : a.SL59.ToString();
            cel460.Text = a.SL60 == 0 ? "" : a.SL60.ToString();

            a = _l.Skip(4).First();
            celSoNaoNam.Text = a.Nam == 0 ? "" : a.Nam.ToString();
            celSoNaoNu.Text = a.Nu == 0 ? "" : a.Nu.ToString();
            celSoNao0.Text = a.SL4 == 0 ? "" : a.SL4.ToString();
            celSoNao5.Text = a.SL14 == 0 ? "" : a.SL14.ToString();
            celSoNao15.Text = a.SL19 == 0 ? "" : a.SL19.ToString();
            celSoNao20.Text = a.SL59 == 0 ? "" : a.SL59.ToString();
            celSoNao60.Text = a.SL60 == 0 ? "" : a.SL60.ToString();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
            cel_GD.Text = DungChung.Bien.GiamDoc;
            cel_diadanh.Text = "......., Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
        }

    }
}
