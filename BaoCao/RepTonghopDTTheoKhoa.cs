using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class RepTonghopDTTheoKhoa : DevExpress.XtraReports.UI.XtraReport
    {
        public RepTonghopDTTheoKhoa()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colMaBN.DataBindings.Add("Text", DataSource, "MaBN");
            colChuanDoan.DataBindings.Add("Text", DataSource, "ChuanDoan");
            colDiachi.DataBindings.Add("Text", DataSource, "DiaChi");
            colGT.DataBindings.Add("Text", DataSource, "Gtinh");
            colNamsinh.DataBindings.Add("Text", DataSource, "Tuoi");
            colNgayra.DataBindings.Add("Text", DataSource, "Ngayra");
            colNgayvao.DataBindings.Add("Text", DataSource, "Ngayvao");
            colTenBN.DataBindings.Add("Text", DataSource, "TenBN");
            colSongadt.DataBindings.Add("Text", DataSource, "Songay");
            colSothe.DataBindings.Add("Text", DataSource, "Sothe");
            colSongadt_GF.DataBindings.Add("Text", DataSource, "Songay");
            colSongadt_RF.DataBindings.Add("Text", DataSource, "Songay");
            GroupHeader1.GroupFields.Add(new GroupField("Nhom"));
        }

        private void colGT_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("Gtinh") != null && this.GetCurrentColumnValue("Tuoi") != null)
            {
                string gt = this.GetCurrentColumnValue("Gtinh").ToString();
                string t = this.GetCurrentColumnValue("Tuoi").ToString();
                if (gt == "1")
                {
                    colNam.Text = t;
                    colNu.Text = "";
                }
                else
                {
                    colNam.Text = "";
                    colNu.Text = t;
                }
            }
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("Nhom") != null)
            {
                string n = this.GetCurrentColumnValue("Nhom").ToString();
                switch (n)
                {
                    case "1":
                        colTenGH.Text = "Nhóm trẻ em dưới 15 tuổi";
                        break;
                    case "2":
                        colTenGH.Text = "Nhóm hộ nghèo";
                        break;
                    case "3":
                        colTenGH.Text = "Nhóm bệnh nhân tham gia bảo hiểm khác (không thuộc trẻ em và hộ nghèo)";
                        break;
                    case "4":
                        colTenGH.Text = "Nhóm bệnh nhân nhân dân";
                        break;
                }
            }
        }

    }
}
