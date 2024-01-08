using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCBenhNhanDieuTriNgoaiTru : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCBenhNhanDieuTriNgoaiTru()
        {
            InitializeComponent();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            xrTableCell21.Text = DungChung.Bien.NguoiLapBieu;
            xrTableCell32.Text = DungChung.Bien.GiamDoc;
            xrTableCell35.Text = DungChung.Ham.NgaySangChu(DateTime.Now);

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            celTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            celTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }


        internal void BindingData()
        {
            celKhoa.DataBindings.Add("Text", DataSource, "TenKP");
            celTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            celNam.DataBindings.Add("Text", DataSource, "NSinhNam");
            celNu.DataBindings.Add("Text", DataSource, "NSinhNu");
            celSoThe.DataBindings.Add("Text", DataSource, "SThe");
            celNgayVao.DataBindings.Add("Text", DataSource, "NgayVao").FormatString = "{0:dd/MM/yyyy}";
            celNgayRa.DataBindings.Add("Text", DataSource, "NgayRa").FormatString = "{0:dd/MM/yyyy}";
            celNgayDT.DataBindings.Add("Text", DataSource, "SoNgayDT").FormatString = "{0:###}";
            celChanDoan.DataBindings.Add("Text", DataSource, "ChanDoan");
            lblSTT.DataBindings.Add("Text", DataSource, "STTNhom");
            GroupHeader1.GroupFields.Add(new GroupField("STTNhom"));

        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("STTNhom") != null)
            {
                string stt = this.GetCurrentColumnValue("STTNhom").ToString();
                switch (stt)
                {
                    case "1":
                        celSTTKhoa.Text = "I";
                        break;
                    case "2":
                        celSTTKhoa.Text = "II";
                        break;
                    case "3":
                        celSTTKhoa.Text = "III";
                        break;
                    case "4":
                        celSTTKhoa.Text = "IV";
                        break;
                    case "5":
                        celSTTKhoa.Text = "V";
                        break;
                    case "6":
                        celSTTKhoa.Text = "VI";
                        break;
                    case "7":
                        celSTTKhoa.Text = "VII";
                        break;
                }
            }
        }
        
    }
}
