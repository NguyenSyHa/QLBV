using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
namespace QLBV.BaoCao
{
    public partial class Rep_SoKiemNhap_A4_20001 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_SoKiemNhap_A4_20001()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities();

        public void BindingData(int Dk, string tencb)
        {
            celtenthuoc.DataBindings.Add("Text", DataSource, "TenDV");

            celncc.DataBindings.Add("Text", DataSource, "NhaSX");
            if (DungChung.Bien.MaBV == "20001")
            {
                celncc.DataBindings.Add("Text", DataSource, "TenCC");
                celchuacb.Text = "";
                celdacb.Text = "X";
                if (Dk == 1)
                {
                    xrTableCell62.Text = tencb;
                }
                else if (Dk == 2)
                {
                    xrTableCell62.Text = tencb;
                }
                else
                {
                    xrTableCell62.DataBindings.Add("Text", DataSource, "TenNguoiCC");
                };
            }
            else
            {
                celchuacb.DataBindings.Add("Text", DataSource, "chuacb");
                celdacb.DataBindings.Add("Text", DataSource, "dacb");
            }
            celsolo.DataBindings.Add("Text", DataSource, "SoLo");
            celhandung.DataBindings.Add("Text", DataSource, "HanDung").FormatString = "{0:dd/MM/yyyy}";
            celkhoilg.DataBindings.Add("Text", DataSource, "SoLuongN").FormatString = DungChung.Bien.FormatString[0];
            GroupHeader1.GroupFields.Add(new GroupField("IDNhap"));
            col_soct.DataBindings.Add("Text", DataSource, "SoCT");


        }



        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "20001")
            {
                xrTableCell51.Text = "Đạt";
                xrTableCell59.Text = DungChung.Bien.TruongKhoaDuoc;
            }
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {


        }

        private void colBSTH_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            cel_ketoantruong.Text = DungChung.Bien.KeToanTruong;
            cel_thukho.Text = DungChung.Bien.ThuKho;


        }
    }

}

