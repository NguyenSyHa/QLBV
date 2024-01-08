using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Data;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class rep_DSBNPTTT_26007 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_DSBNPTTT_26007()
        {
            InitializeComponent();
        }

        public void BindingData()
        {
            celMaBNhan.DataBindings.Add("Text", DataSource, "MaBNhan");
            celHoTen.DataBindings.Add("Text", DataSource, "TenBNhan");
            celMaThe.DataBindings.Add("Text", DataSource, "SThe");
            celNgayTH.DataBindings.Add("Text", DataSource, "NgayTH");
            celTenTT.DataBindings.Add("Text", DataSource, "TenDV");
            celLoaiTT.DataBindings.Add("Text", DataSource, "Loai");
            celBSTH.DataBindings.Add("Text", DataSource, "TenCB");
            celKhoaCD.DataBindings.Add("Text", DataSource, "TenKP");
            celSoLuong.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[0];
            celDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            celThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            celMaQD.DataBindings.Add("Text", DataSource, "MaQD");
            GroupHeader1.GroupFields.Add(new GroupField("MaDV"));
            celSoLuong_G.DataBindings.Add("Text", DataSource, "SoLuong");
            celSoLuong_G.Summary.FormatString = DungChung.Bien.FormatString[0];
            celThanhTienG.DataBindings.Add("Text", DataSource, "ThanhTien");
            celThanhTienG.Summary.FormatString = DungChung.Bien.FormatString[1];
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtcqcq.Text = DungChung.Bien.TenCQCQ;
            txtcq.Text = DungChung.Bien.TenCQ;
            _count = Convert.ToInt32(this.count.Value);

        }

        int num = 0;
        int _count = 0;
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            detailht = true;
            num++;
            if (this.GetCurrentColumnValue("PTVPhu") != null)
            {
                string dscb = this.GetCurrentColumnValue("PTVPhu").ToString();
                string[] arrDSCB = QLBV_Library.QLBV_Ham.LayChuoi(';', dscb);
                string x = "";
                for (int i = 0; i < arrDSCB.Count(); i++)
                {
                    switch (i)
                    {
                        case 0:
                            if (arrDSCB[0] != "")
                                x = arrDSCB[0];
                            break;
                        case 4:
                            if (arrDSCB[0] != "")
                                x = x + "; " + arrDSCB[4];
                            break;
                        case 5:
                            if (arrDSCB[0] != "")
                                x = x + "; " + arrDSCB[5];
                            break;
                        default:
                            break;
                    }
                }
                celPTVPhu.Text = x;
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            //xrTableCell47.Text = DungChung.Bien.TruongKhoaLS;
            xrTableCell50.Text = DungChung.Bien.GiamDoc;
        }

        private void celMaQD_BeforePrint(object sender, CancelEventArgs e)
        {

            if (this.GetCurrentColumnValue("MaQD") != null)
            {
                string maqd = this.GetCurrentColumnValue("MaQD").ToString();
                celMaQD.Text = "Mã QĐ: " + maqd;
            }
        }

        bool ht = false;
        bool detailht = false;
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            ht = false;
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (detailht)
            {
                PageHeader.Visible = ht;               
            }
            else
                PageHeader.Visible = false; 
            ht = true;
        }

        private void xrPageBreak1_BeforePrint(object sender, CancelEventArgs e)
        {
            //ht = false;
        }

        private void xrPageBreak1_BeforePrint_1(object sender, CancelEventArgs e)
        {

            detailht = false;
            if (num == _count)
                xrPageBreak1.Visible = false;
        }

    }
}
