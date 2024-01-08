﻿using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repDThuoc_HaLang : DevExpress.XtraReports.UI.XtraReport
    {
        public repDThuoc_HaLang()
        {
            InitializeComponent();
        }
        //QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
        }
        public void BindData() {
            TenDV.DataBindings.Add("Text", DataSource, "TenDV");
            SoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
            DonVi.DataBindings.Add("Text", DataSource, "DonVi");
            HuongDan.DataBindings.Add("Text", DataSource, "HuongDan");
            
        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            string sthe = SThe.Value.ToString();
            if (sthe.Length == 15)
            {
                txtThe1.Text = sthe.Substring(0, 2);
                txtThe2.Text = sthe.Substring(2, 1);
                txtThe3.Text = sthe.Substring(3, 2);
                txtThe4.Text = sthe.Substring(5, 2);
                txtThe5.Text = sthe.Substring(7, 3);
                txtThe6.Text = sthe.Substring(10, 5);
            }
            string macs = MaCS.Value.ToString();
            if (macs.Length >= 4) {
                txtMacS.Text = macs.Substring(0, 2) + "-" + macs.Substring(2, 3);
            }
            txtTenCQ.Text = DungChung.Bien.TenCQ;
            if (ChuyenVien.Value.ToString().Contains("2"))
            {
                //System.Windows.Forms.MessageBox.Show("123");
                xrCongKham.Visible = true;
                txtNoiChuyen.Visible = true;
                xrChiTiet.Visible = false;
            }

        }


    }
}
