﻿using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repDonThuoc_TT05_A4 : DevExpress.XtraReports.UI.XtraReport
    {
        public repDonThuoc_TT05_A4()
        {
            InitializeComponent();
        }
        //QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

        }
        public void BindData()
        {
            TenDV.DataBindings.Add("Text", DataSource, "TenDV");
            SoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
            DonVi.DataBindings.Add("Text", DataSource, "DonVi");
            HuongDan.DataBindings.Add("Text", DataSource, "HuongDan");

        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ;
            if (DungChung.Bien.MaBV == "30002")
            {
                txttenBN.Text = "Chữ ký của bệnh nhân";
            }
            if (DungChung.Bien.MaBV == "24009")
            {
                txtMaICD.Visible = true;
                txtICD.Visible = true;
            }
            if (DungChung.Bien.MaBV == "30009")
            {
                txtMaICD.Visible = true;
                txtICD.Visible = true;
                colHoTenBoMe.Visible = true;
                colLoiDanBS.Visible = true;
                xrTableCell10.Visible = true;
                colHoTenBoMe.Visible = true;
                colLoiDanBS.Visible = true;
            }
            if (DungChung.Bien.MaBV == "01830" )
                lab_TenKP.Visible = true;
            if(DungChung.Bien.MaBV == "27001")
                xrLabel7.Visible = true;
            if(DungChung.Bien.MaBV=="30007")
            {
                if(SThe.Value !=null && SThe.Value.ToString().Length>=15)
                    txtTieuDe.Text="ĐƠN THUỐC BHYT";
            }

        }

        private void xrTableCell12_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24009")
            {
                txtbnky.Visible = false;
                txttenBN.Visible = false;
            }
            if (DungChung.Bien.MaBV == "30002" || DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "26007")
            {
                xrThuKho.Visible = true;
                xrThuKhoky.Visible = true;
                xrThuKhoNguoi.Visible = true;
            }
            if (DungChung.Bien.MaBV == "30004")
                xrTenBS.Visible = false;
            if (DungChung.Bien.MaBV == "30009")
            {
                txtbnky.Visible = false;
                txttenBN.Visible = false;
                colHoTenBoMe.Visible = false;
                colLoiDanBS.Visible = false;
                xrTableCell10.Visible = false;
                colHoTenBoMe.Visible = false;
                colLoiDanBS.Visible = false;

            }
            if(DungChung.Bien.MaBV.Substring(0,2)=="30")
            xrTableCell5.Text = "Bác sỹ khám bệnh";
            //if (DungChung.Bien.MaBV == "04011")
            //{
            //    txttenBN.Text = "Họ tên bệnh nhân";
            //    txtbnky.Text = "(Ký, ghi rõ họ tên)";
            //}
        }

    }
}
