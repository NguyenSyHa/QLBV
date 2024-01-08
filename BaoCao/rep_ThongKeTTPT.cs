using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class rep_ThongKeTTPT : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_ThongKeTTPT()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            labTenCQ.Text = DungChung.Bien.TenCQ;
        }
        public void BindingData()
        {

            cellTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            cellTuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            cellCanBenh.DataBindings.Add("Text", DataSource, "CanBenh");
            cellBH.DataBindings.Add("Text", DataSource, "BHYT");
            cellTP.DataBindings.Add("Text", DataSource, "DichVu");
            cellnoi.DataBindings.Add("Text", DataSource, "KhoaNoi");
            cellNgoai.DataBindings.Add("Text", DataSource, "KhoaNgoai");
            cellPK.DataBindings.Add("Text", DataSource, "PhongKham");
            cellKhac.DataBindings.Add("Text", DataSource, "Khoa");
            cellTong.DataBindings.Add("Text", DataSource, "Tong");

            //colSoLuong.DataBindings.Add("Text", DataSource, "SoLuongT").FormatString = DungChung.Bien.FormatString[0];
            //colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            //colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTienT").FormatString = DungChung.Bien.FormatString[1];
            //colThanhTienTong.DataBindings.Add("Text", DataSource, "ThanhTienT").FormatString = DungChung.Bien.FormatString[1];
            //  colMaDV.DataBindings.Add("Text", DataSource, "MaDV");
            //GroupHeader1.GroupFields.Add(new GroupField("TenNhomDV"));
        }
        int tong = 0;
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            string[] ngay = new string[32];

            object ngay2 = GetCurrentColumnValue("Ngay");
            ngay = (string[])ngay2;
            int i = 1;
            if(ngay!=null)
            foreach (XRTableCell cell in xrTableRow2)
            {
                if (i > 31)
                    break;
                if (cell.Index == (i + 8))
                {
                    if (ngay.Length >= i - 1)
                        cell.Text = ngay[i - 1];
                    if(ngay[i-1]=="X")
                    tong++;
                    i++;

                }
                
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            cellTongcong.Text = tong.ToString();
        }
    }
}
