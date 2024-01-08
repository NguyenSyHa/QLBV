using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_SokhambenhA4 : DevExpress.XtraReports.UI.XtraReport
    {
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        int a = 0, _ngay=0;
        private bool htDotuoi = false;
        private bool htNgay = false;
        public Rep_SokhambenhA4()
        {
            InitializeComponent();
        }
        public Rep_SokhambenhA4(int _a, bool htDotuoi, bool htNgay,int ngay)
        {
            InitializeComponent();
            a = _a;
            _ngay = ngay;
            this.htDotuoi = htDotuoi;
            this.htNgay = htNgay;
        }
        public Rep_SokhambenhA4(int _a)
        {
            InitializeComponent();
            a = _a;
           
        }
        public void BindingData()
        {
            colTenBN.DataBindings.Add("Text", DataSource, "TenBN");
            // txtMaBN.DataBindings.Add("Text", DataSource, "maBN");
            colNam.DataBindings.Add("Text", DataSource, "tuoin");
            colNu.DataBindings.Add("Text", DataSource, "tuoinu");
            colDiachi.DataBindings.Add("Text", DataSource, "Diachi");
            colSothe.DataBindings.Add("Text", DataSource, "Sothe");
            //colNoiGT.DataBindings.Add("Text", DataSource, "NoiGT");
            //colCDTuyenduoi.DataBindings.Add("Text", DataSource, "CDNoiGT");
            if (a == 2)

            { colCDKKB.DataBindings.Add("Text", DataSource, "ticd"); }
            else
            { colCDKKB.DataBindings.Add("Text", DataSource, "noigt"); }
            colCDDT.DataBindings.Add("Text", DataSource, "ticd");
            colVVien.DataBindings.Add("Text", DataSource, "VV1");
            colTuyentren.DataBindings.Add("Text", DataSource, "TT1");
            colTuyenduoi.DataBindings.Add("Text", DataSource, "TD1");
            colNgoaitru.DataBindings.Add("Text", DataSource, "NT1");
            colVenha.DataBindings.Add("Text", DataSource, "VN1");
            //colTT.DataBindings.Add("Text", DataSource, "TT1");
            colChuyenkhoa.DataBindings.Add("Text", DataSource, "KhamCK1");
            colBS.DataBindings.Add("Text", DataSource, "TenBS");
            cell_ngayKham.DataBindings.Add("Text", DataSource, "ngay").FormatString = "{0:HH:mm dd/MM}";
            if (htDotuoi)
            {
                celDoTuoi.DataBindings.Add("Text", DataSource, "DoTuoi");
                GroupHeader2.GroupFields.Add(new GroupField("DoTuoi"));
            }

            if (htNgay)
            {
                celNgayKham.DataBindings.Add("Text", DataSource, "nkb").FormatString = "{0:dd/MM/yyyy}";
                GroupHeader1.GroupFields.Add(new GroupField("nkb"));
            }
            if (DungChung.Bien.MaBV == "27001")
            {
                cell_ngayKham.DataBindings.Add("Text", DataSource, "ngay").FormatString = "{0:dd/MM/  yyyy}";
            }


                colTenBN1.DataBindings.Add("Text", DataSource, "TenBN");
            // txtMaBN.DataBindings.Add("Text", DataSource, "maBN");
            colNam1.DataBindings.Add("Text", DataSource, "tuoin");
            colNu1.DataBindings.Add("Text", DataSource, "tuoinu");
            colDiachi1.DataBindings.Add("Text", DataSource, "Diachi");
            colSothe1.DataBindings.Add("Text", DataSource, "Sothe");
            //colNoiGT.DataBindings.Add("Text", DataSource, "NoiGT");
            //colCDTuyenduoi.DataBindings.Add("Text", DataSource, "CDNoiGT");
            if (a == 2)

            { colCDKKB1.DataBindings.Add("Text", DataSource, "ticd"); }
            else
            { colCDKKB1.DataBindings.Add("Text", DataSource, "noigt"); }
            colCDDT1.DataBindings.Add("Text", DataSource, "ticd");
            colVVien1.DataBindings.Add("Text", DataSource, "VV1");
            colTuyentren1.DataBindings.Add("Text", DataSource, "TT1");
            colTuyenduoi1.DataBindings.Add("Text", DataSource, "TD1");
            colNgoaitru1.DataBindings.Add("Text", DataSource, "NT1");
            colCDDB1.DataBindings.Add("Text", DataSource, "chidinh1");
            colVenha1.DataBindings.Add("Text", DataSource, "VN1");
            //colTT.DataBindings.Add("Text", DataSource, "TT1");
            colChuyenkhoa1.DataBindings.Add("Text", DataSource, "KhamCK1");
            colBS1.DataBindings.Add("Text", DataSource, "TenBS");
            cell_ngayKham1.DataBindings.Add("Text", DataSource, "ngay").FormatString = "{0:dd/MM}";
        }
         
      

        
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenBV.Text = DungChung.Bien.TenCQ.ToUpper();
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            if (_ngay == 0)
                cell_TD_SoNgayDT.Text = "Ngày ra viện";
            else if (_ngay == 1)
                cell_TD_SoNgayDT.Text = "Ngày thanh toán";
            else if (_ngay == 2)
                cell_TD_SoNgayDT.Text = "Ngày khám";
            if(DungChung.Bien.MaBV == "27194")
            {
                SubBand6.Visible = true;
                SubBand2.Visible = false;
            }

        }

        private void xrTableCell4_BeforePrint(object sender, CancelEventArgs e)
        {
            if (a == 2)
            { xrTableCell7.Text = "Chẩn đoán"; }
            else
            { xrTableCell7.Text = "Nơi giới thiệu"; }
        }

        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (htDotuoi)
                GroupHeader2.Visible = true;
            else
                GroupHeader2.Visible = false;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if(DungChung.Bien.MaBV == "27194")
            {
                xrTable3.Visible = false;
                Detail27194.Visible = true;
            }
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27194")
            {
                SubBand1.Visible = false;
                SubBand5.Visible = true;
            }
        }

        private void SubBand2_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (htNgay)
                GroupHeader1.Visible = true;
            else
                GroupHeader1.Visible = false;
        }

        private void colNam_BeforePrint(object sender, CancelEventArgs e)
        {
            int mabn = 0;
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" && colNam.Text != "")
            {
                var bn = _data.BenhNhans.Where(p => p.SThe == colSothe.Text).First();
                if (bn != null)
                {
                    mabn = bn.MaBNhan;
                }
                colNam.Text = DungChung.Ham.TuoitheoThang(_data, mabn, "12-30");
            }
        }

        private void colNu_BeforePrint(object sender, CancelEventArgs e)
        {
            int mabn = 0;
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" && colNu.Text != "")
            {
                var bn = _data.BenhNhans.Where(p => p.SThe == colSothe.Text).First();
                if (bn != null)
                {
                    mabn = bn.MaBNhan;
                }
                colNu.Text = DungChung.Ham.TuoitheoThang(_data, mabn, "12-30");
            }
        }
    }
}
