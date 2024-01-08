﻿using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_TKTamThu : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_TKTamThu()
        {
            InitializeComponent();
        }

      
        public void BindingData() {
            colLyDo.DataBindings.Add("Text",DataSource ,"LyDo");
            colThanhTien.DataBindings.Add("Text", DataSource, "SoTien").FormatString=DungChung.Bien.FormatString[1];
            colTenBNhan.DataBindings.Add("Text", DataSource, "TenBNhan");
            colSoPhieu.DataBindings.Add("Text", DataSource, "IDTamUng");
            colThanhTienrf.DataBindings.Add("Text", DataSource, "SoTien").FormatString = DungChung.Bien.FormatString[1];
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoilap.Text = DungChung.Bien.NguoiLapBieu;
            colKTT.Text = DungChung.Bien.KeToanTruong;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
            txtTenCQ.Text = DungChung.Bien.TenCQ;
            string a = TT.Value.ToString();
            if (a == "0")
            {
                xrTableCell5.Text = "Tạm thu";
            }
            else
            {
                xrTableCell5.Text = "Thanh toán tạm thu";
            }
        }

    }
}
