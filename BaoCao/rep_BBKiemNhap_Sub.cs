using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class rep_BBKiemNhap_Sub : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BBKiemNhap_Sub()
        {
            InitializeComponent();
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var qcqcq = data.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV).FirstOrDefault();
            string _maCQCQ = "";
            if (qcqcq != null)
                _maCQCQ = qcqcq.MaChuQuan;
            if (DungChung.Bien.MaBV == "08602")
            {
                dt08602.Visible = true;
                cel_ThuKho.Text = DungChung.Bien.ThuKho;
                celTruongKhoa.Text = DungChung.Bien.TruongKhoaDuoc;
            }
            else if (DungChung.Bien.MaBV == "30009")
            {
                dt30009.Visible = true;
            }
            else if (DungChung.Bien.MaBV == "26007")
            {
                dt26007.Visible = true;
                cel_ThongkeDuoc.Text = DungChung.Bien.NguoiLapBieu;
                cel_TruongKhoaDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
                celThuKho.Text = DungChung.Bien.ThuKho;
                cel_KeToanTruong.Text = DungChung.Bien.KeToanTruong;
                celGiamDoc.Text = DungChung.Bien.GiamDoc;
            }
            else if (DungChung.Bien.MaBV == "30005")
            {
                DT_30005.Visible = true;
                col_TKD_30005.Text = DungChung.Bien.TruongKhoaDuoc;
            }
            else if(DungChung.Bien.MaBV == "24297")
            {
                dt27022.Visible = true;
                xrTableCell57.Text = "THỦ KHO";
                xrTableCell58.Text = "NGƯỜI MUA";
                xrTableCell66.Text = "KẾ TOÁN";
                xrTableCell67.Text = "LÃNH ĐẠO";
                xrTableCell72.Text = "TRƯỞNG KHOA DƯỢC";
                xrLabel1.Visible = true;
                xrLabel2.Visible = true;
                xrLabel3.Visible = true;
                xrLabel4.Visible = true;
                xrLabel5.Visible = true;

            }

            else if (DungChung.Bien.MaBV == "27021")
            {
                dt27021.Visible = true;
            }
            else if (_maCQCQ == "12001")
            {
                dt12001_xa.Visible = true;
            }
            //else if (DungChung.Bien.MaBV == "27022")
            //{
            //    dt27022.Visible = true;
            //    thukho.Text = DungChung.Bien.ThuKho;
            //    ketoantruong.Text = DungChung.Bien.KeToanTruong;
            //}
            else if (DungChung.Bien.MaBV == "30003")
            {
                dt30003.Visible = true;
            }
            else
            {
                if (DungChung.Bien.MaBV == "12122")
                    lblCKCT.Value = "CHỦ TỊCH HỘI ĐỒNG THUỐC";
                dtAll.Visible = true;
                dt08602.Visible = false;

            }

        }

    }
}
