using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;
using QLBV.CLS;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuXNTongHop : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuXNTongHop(int gtinh)
        {
            InitializeComponent();
            GT = gtinh;
            if (DungChung.Bien.MaBV == "30004")
            {
                lblNgayIn.Visible = false;
            }
            else if (DungChung.Bien.MaBV == "24012")
            {
                GroupHeader3_24012.Visible = true;
                GroupHeader2.Visible = false;
                GroupFooter_24012.Visible = true;
            }
        }

        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<DichVuct> _ldvct = new List<DichVuct>();
        int GT = 0;
        List<string> _lTen = new List<string>();
        public class BaoCao
        {
            public string MaDVct { get; set; }

            public string KetQua { get; set; }

            public string TSBT { get; set; }

            public string TenDV { get; set; }

            public string MaTam { get; set; }

            public int MaDV { get; set; }

            public string DonVi { get; set; }

            public int? STTDV { get; set; }

            public int? STTdvct { get; set; }

            public string TenDVct { get; set; }

            public int STTNhomHT { get; set; }
            
        }
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
            {
                xrLabel14.Text = "PHÒNG XÉT NGHIỆM";
                lab55.Text = "PHÒNG XÉT NGHIỆM";
            }
            if (DungChung.Bien.MaBV == "30004")
            {
                lab55.Text = "PHÒNG XÉT NGHIỆM";
                xrLabel3.Visible = xrLabel4.Visible = false;
            }
            _ldvct = DataContect.DichVucts.ToList();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();

            if (DungChung.Bien.MaBV == "14018")
            {
                xrLabel34.Text = DungChung.Bien.TenCQ;
            }

            if (DungChung.Bien.MaBV == "27777")
            {
                //xrLabel20.Text = DungChung.Ham.GetDiaChiBV();
                //colDiaChi.Text = DungChung.Ham.GetDiaChiBV();
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if(DungChung.Bien.MaBV=="30372")
            {
                SubBand5.Visible = false;
                SubBand6.Visible = true;
                xrMota.BorderColor = Color.DarkGray;
                xrLabel55.BorderColor = Color.DarkGray;


            }
            else
            {
                SubBand5.Visible = true;
                SubBand6.Visible = false;
                if (DungChung.Bien.MaBV == "24012")
                {
                    xrLabel60.Visible = true;
                    xrLabel57.Visible = true;
                    lab55.Text = "KHOA XÉT NGHIỆM";
                    xrLabel60.Text = "BÁC SỸ CHỈ ĐỊNH";
                }
            }

            lblNgayIn.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
        

        internal void dataBinhding()
        {
            celTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            CelGroupTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            celMa.DataBindings.Add("Text", DataSource, "MaDVct");
            celTenXN.DataBindings.Add("Text", DataSource, "TenDVct");
            colTenXetNghiem14018.DataBindings.Add("Text", DataSource, "TenDVct");
            celKQ.DataBindings.Add("Text", DataSource, "KetQua");
            colKetQua14018.DataBindings.Add("Text", DataSource, "KetQua");
            celTSBT.DataBindings.Add("Text", DataSource, "TSBT");
            colBinhThuong14018.DataBindings.Add("Text", DataSource, "TSBT");
            celDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            if (DungChung.Bien.MaBV != "30004")
            {
                GroupHeader1.GroupFields.Add(new GroupField("MaDV"));
                GroupHeader2.GroupFields.Add(new GroupField("TenRG"));
            }
            if (DungChung.Bien.MaBV == "24012")
            {
                GroupHeader3_24012.GroupFields.Add(new GroupField("IdCLS"));
            }
            colTenTN.DataBindings.Add("Text", DataSource, "TenTN");
            txtTenTN30004.DataBindings.Add("Text", DataSource, "TenTN");
            txtTenTN24012.DataBindings.Add("Text", DataSource, "TenTN");
            //30372
            celTenXN1.DataBindings.Add("Text", DataSource, "TenDVct");
            celKQ1.DataBindings.Add("Text", DataSource, "KetQua");
            celTSBT1.DataBindings.Add("Text", DataSource, "TSBT");
            celDonVi1.DataBindings.Add("Text", DataSource, "DonVi");
            celMaMay.DataBindings.Add("Text", DataSource, "MaMay");
            colTenTN1.DataBindings.Add("Text", DataSource, "TenTN");
            //30004
            celKQ30004.DataBindings.Add("Text", DataSource, "KetQua");
            celTenXN30004.DataBindings.Add("Text", DataSource, "TenDVct");
            celDonVi30004.DataBindings.Add("Text", DataSource, "DonVi");
            celTSBT30004.DataBindings.Add("Text", DataSource, "TSBT");
            celDonVi30004.DataBindings.Add("Text", DataSource, "DonVi");
            ColMaMay.DataBindings.Add("Text", DataSource, "MaMay");
            //24012
            celKQ24012.DataBindings.Add("Text", DataSource, "KetQua");
            celTenXN24012.DataBindings.Add("Text", DataSource, "TenDVct");
            celThoiGian24012.DataBindings.Add("Text", DataSource, "DonVi");
            celTSBT24012.DataBindings.Add("Text", DataSource, "TSBT");
            celPPXN24012.DataBindings.Add("Text", DataSource, "PPXN");
            celMaMay24012.DataBindings.Add("Text", DataSource, "MaMay");
            celThoiGian24012.DataBindings.Add("Text", DataSource, "ThoiGian");
            celDonVi24012.DataBindings.Add("Text", DataSource, "DonVi");
            celGhiChu24012.DataBindings.Add("Text", DataSource, "GhiChu");
            celDonVi24012.DataBindings.Add("Text", DataSource, "DonVi");
            switch (DungChung.Bien.MaBV)
            {
                case "30372":
                    xrTableCell1.DataBindings.Add("Text", DataSource, "STT");
                    SubBandDetail30372.Visible = true;
                    SubBandGroupHeader230372.Visible = true;
                    SubBand1.Visible = false;
                    SubBand3.Visible = false;
                    xrTable3.BorderColor = Color.DarkGray;
                    break;
                case "14017":
                    xrTableCell1.DataBindings.Add("Text", DataSource, "STT");
                    SubBandDetail30372.Visible = true;
                    SubBandGroupHeader230372.Visible = true;
                    SubBand1.Visible = false;
                    SubBand3.Visible = false;
                    xrTable3.BorderColor = Color.DarkGray;
                    break;
                case "14018":
                    if (InPhieu._getTenRG == "XN nước tiểu")
                    {
                        SubBand14018.Visible = true;
                        SbGroupHeader14018.Visible = true;
                        SbDetail14018.Visible = true;
                        SubBandDetail30372.Visible = false;
                        SubBandGroupHeader230372.Visible = false;
                        SubBand1.Visible = false;
                        SubBand3.Visible = false;
                        SubBand4.Visible = true;
                    }
                    else
                    {
                        SubBandDetail30372.Visible = false;
                        SubBandGroupHeader230372.Visible = false;
                        SubBand1.Visible = true;
                        SubBand3.Visible = true;
                    }
                    break;
                case "30004":
                    SubBandDetail30372.Visible = false;
                    SubBandGroupHeader230372.Visible = false;
                    SubBand1.Visible = false;
                    SubBand3.Visible = false;
                    sbdt30004.Visible = true;
                    SB30004.Visible = true;
                    break;
                case "24012":
                    SubBandDetail30372.Visible = false;
                    SubBandGroupHeader230372.Visible = false;
                    SubBand1.Visible = false;
                    SubBand3.Visible = false;
                    sbdt30004.Visible = true;
                    SB30004.Visible = true;
                    break;

                default:
                    //xrTableCell1.Summary = new XRSummary(SummaryRunning.Group, SummaryFunc.RecordNumber, "");
                    SubBandDetail30372.Visible = false;
                    SubBandGroupHeader230372.Visible = false;
                    SubBand1.Visible = true;
                    SubBand3.Visible = true;
                    break;
            }
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("TenDV") == null || string.IsNullOrWhiteSpace(this.GetCurrentColumnValue("TenDV").ToString()))
            {
                e.Cancel = true;
            }

        }

        private void lblNgayIn_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {

        }

        private void PageFooter_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void Detail_BeforePrint_1(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                if (DungChung.Bien.check == 0 || DungChung.Bien.check == 1)
                {
                    xrTable1.Visible = false;
                    xrTable14.Visible = false;
                    tb2.Visible = true;
                }
            }
        }

        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("TenRG") != null)
            {
                string a = this.GetCurrentColumnValue("TenRG").ToString();
                if ((a == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || a == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac || a == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNUngThu) && DungChung.Bien.MaBV != "30372")
                {
                    SubBand2.Visible = false;
                    //this.celTenXN.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold | FontStyle.Underline);
                }
                else
                {
                    if (DungChung.Bien.MaBV == "14018" && InPhieu._getTenRG == "XN nước tiểu")
                    {
                        SubBand2.Visible = false;
                    }
                    else
                        SubBand2.Visible = true;
                }
            }
        }

        private void ThuanAnheader_BeforePrint(object sender, CancelEventArgs e)
        {
            xrPictureBox1.Image = DungChung.Ham.GetLogo();

        }

        private void Rep_PhieuXNTongHop_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30372")
            {
                BV30372header.Visible = true;
            }
            else
                if (DungChung.Bien.MaBV == "14018" && InPhieu._getTenRG == "XN nước tiểu")
                {
                    SubBand14018.Visible = true;
                }
                else HeaderBVKhac.Visible = true;
        }

        private void SubBand3_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV != "30372")
            {
                if (this.GetCurrentColumnValue("MaDVct") != null && this.GetCurrentColumnValue("KetQua") != null)
                {
                    string a = this.GetCurrentColumnValue("MaDVct").ToString();
                    var dvct = _ldvct.Where(p => p.MaDVct == a).FirstOrDefault();
                    if (dvct != null)
                    {
                        try
                        {
                            if (GT == 0)
                            {
                                if (dvct.TSnuTu != null && dvct.TSnuDen != null)
                                {
                                    double tstu = Convert.ToDouble(dvct.TSnuTu);
                                    double tsden = Convert.ToDouble(dvct.TSnuDen);
                                    double kq = Convert.ToDouble(this.GetCurrentColumnValue("KetQua"));
                                    if (kq < tstu)
                                    {
                                        this.celKQ.ForeColor = System.Drawing.Color.Red;
                                        celKQ.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                        celKQ.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                    }
                                    else if (kq > tsden)
                                    {
                                        this.celKQ.ForeColor = System.Drawing.Color.Red;
                                        celKQ.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                        celKQ.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                    }
                                    else
                                    {
                                        this.celKQ.ForeColor = System.Drawing.Color.Black;
                                        celKQ.Font = new System.Drawing.Font("Times New Roman", 11, FontStyle.Regular);
                                        celKQ.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                    }
                                }
                                else if (dvct.TSnuTu != null && dvct.TSnuDen == null)//trị số nhỏ nhất
                                {
                                    double tstu = Convert.ToDouble(dvct.TSnuTu);
                                    double kq = Convert.ToDouble(this.GetCurrentColumnValue("KetQua"));
                                    if (tstu > kq)
                                    {
                                        this.celKQ.ForeColor = System.Drawing.Color.Red;
                                        celKQ.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                        celKQ.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
                                    }
                                    else
                                    {
                                        this.celKQ.ForeColor = System.Drawing.Color.Black;
                                        celKQ.Font = new System.Drawing.Font("Times New Roman", 11, FontStyle.Regular);
                                        celKQ.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                    }
                                }
                                //giá trị lớn nhất
                                else if (dvct.TSnuTu == null && dvct.TSnuDen != null)
                                {
                                    double tsden = Convert.ToDouble(dvct.TSnuDen);
                                    double kq = Convert.ToDouble(this.GetCurrentColumnValue("KetQua"));
                                    if (kq > tsden)
                                    {
                                        this.celKQ.ForeColor = System.Drawing.Color.Red;
                                        celKQ.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                        celKQ.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                    }
                                    else
                                    {
                                        this.celKQ.ForeColor = System.Drawing.Color.Black;
                                        celKQ.Font = new System.Drawing.Font("Times New Roman", 11, FontStyle.Regular);
                                        celKQ.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                    }

                                }
                                else
                                {
                                    this.celKQ.ForeColor = System.Drawing.Color.Black;
                                    celKQ.Font = new System.Drawing.Font("Times New Roman", 11, FontStyle.Regular);
                                    celKQ.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                            else
                            {
                                if (dvct.TSnTu != null && dvct.TSnDen != null)
                                {
                                    double tstu = Convert.ToDouble(dvct.TSnTu);
                                    double tsden = Convert.ToDouble(dvct.TSnDen);
                                    double kq = Convert.ToDouble(this.GetCurrentColumnValue("KetQua"));
                                    if (kq < tstu)
                                    {
                                        this.celKQ.ForeColor = System.Drawing.Color.Red;
                                        celKQ.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                        celKQ.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                    }
                                    else if (kq > tsden)
                                    {
                                        this.celKQ.ForeColor = System.Drawing.Color.Red;
                                        celKQ.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                        celKQ.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                    }
                                    else
                                    {
                                        this.celKQ.ForeColor = System.Drawing.Color.Black;
                                        celKQ.Font = new System.Drawing.Font("Times New Roman", 11, FontStyle.Regular);
                                        celKQ.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                                    }
                                }
                                else if (dvct.TSnTu != null && dvct.TSnDen == null)//trị số nhỏ nhất
                                {
                                    double tstu = Convert.ToDouble(dvct.TSnTu);
                                    double kq = Convert.ToDouble(this.GetCurrentColumnValue("KetQua"));
                                    if (tstu > kq)
                                    {
                                        this.celKQ.ForeColor = System.Drawing.Color.Red;
                                        celKQ.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                        celKQ.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                    }
                                    else
                                    {
                                        this.celKQ.ForeColor = System.Drawing.Color.Black;
                                        celKQ.Font = new System.Drawing.Font("Times New Roman", 11, FontStyle.Regular);
                                        celKQ.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                    }
                                }
                                //giá trị lớn nhất
                                else if (dvct.TSnTu == null && dvct.TSnDen != null)
                                {
                                    double tsden = Convert.ToDouble(dvct.TSnDen);
                                    double kq = Convert.ToDouble(this.GetCurrentColumnValue("KetQua"));
                                    if (kq >= tsden)
                                    {
                                        this.celKQ.ForeColor = System.Drawing.Color.Red;
                                        celKQ.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                        celKQ.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                    }
                                    else
                                    {
                                        this.celKQ.ForeColor = System.Drawing.Color.Black;
                                        celKQ.Font = new System.Drawing.Font("Times New Roman", 11, FontStyle.Regular);
                                        celKQ.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                    }

                                }
                                else
                                {
                                    this.celKQ.ForeColor = System.Drawing.Color.Black;
                                    celKQ.Font = new System.Drawing.Font("Times New Roman", 11, FontStyle.Regular);
                                    celKQ.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                }
                            }
                        }
                        catch
                        {
                            this.celKQ.ForeColor = System.Drawing.Color.Black;
                            celKQ.Font = new System.Drawing.Font("Times New Roman", 11, FontStyle.Regular);
                            celKQ.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        }

                    }

                }
            }
        }

        private void SubBandDetail30372_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30372")
            {
                if (this.GetCurrentColumnValue("MaDVct") != null && this.GetCurrentColumnValue("KetQua") != null)
                {
                    string a = this.GetCurrentColumnValue("MaDVct").ToString();
                    var dvct = _ldvct.Where(p => p.MaDVct == a).FirstOrDefault();
                    if (dvct != null)
                    {
                        try
                        {
                            if (GT == 0)
                            {
                                if (dvct.TSnuTu != null && dvct.TSnuDen != null)
                                {
                                    double tstu = Convert.ToDouble(dvct.TSnuTu);
                                    double tsden = Convert.ToDouble(dvct.TSnuDen);
                                    double kq = Convert.ToDouble(this.GetCurrentColumnValue("KetQua"));
                                    if (kq < tstu)
                                    {
                                        this.celKQ1.ForeColor = System.Drawing.Color.Red;
                                        celKQ1.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Underline);
                                    }
                                    else if (kq > tsden)
                                    {
                                        this.celKQ1.ForeColor = System.Drawing.Color.Red;
                                        celKQ1.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Underline);
                                    }
                                    else
                                    {
                                        this.celKQ1.ForeColor = System.Drawing.Color.Black;
                                        celKQ1.Font = new System.Drawing.Font("Times New Roman", 11, FontStyle.Regular);
                                    }
                                }
                                else if (dvct.TSnuTu != null && dvct.TSnuDen == null)//trị số nhỏ nhất
                                {
                                    double tstu = Convert.ToDouble(dvct.TSnuTu);
                                    double kq = Convert.ToDouble(this.GetCurrentColumnValue("KetQua"));
                                    if (tstu > kq)
                                    {
                                        this.celKQ1.ForeColor = System.Drawing.Color.Red;
                                        celKQ1.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Underline);
                                    }
                                    else
                                    {
                                        this.celKQ1.ForeColor = System.Drawing.Color.Black;
                                        celKQ1.Font = new System.Drawing.Font("Times New Roman", 11, FontStyle.Regular);
                                    }
                                }
                                else if (dvct.TSnuTu == null && dvct.TSnuDen != null)//giá trị lớn nhất
                                {
                                    double tsden = Convert.ToDouble(dvct.TSnuDen);
                                    double kq = Convert.ToDouble(this.GetCurrentColumnValue("KetQua"));
                                    if (kq > tsden)
                                    {
                                        this.celKQ1.ForeColor = System.Drawing.Color.Red;
                                        celKQ1.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Underline);
                                    }
                                    else
                                    {
                                        this.celKQ1.ForeColor = System.Drawing.Color.Black;
                                        celKQ1.Font = new System.Drawing.Font("Times New Roman", 11, FontStyle.Regular);
                                    }

                                }
                                else
                                {
                                    this.celKQ1.ForeColor = System.Drawing.Color.Black;
                                    celKQ1.Font = new System.Drawing.Font("Times New Roman", 11, FontStyle.Regular);
                                }
                            }
                            else
                            {
                                if (dvct.TSnTu != null && dvct.TSnDen != null)
                                {
                                    double tstu = Convert.ToDouble(dvct.TSnTu);
                                    double tsden = Convert.ToDouble(dvct.TSnDen);
                                    double kq = Convert.ToDouble(this.GetCurrentColumnValue("KetQua"));
                                    if (kq < tstu)
                                    {
                                        this.celKQ1.ForeColor = System.Drawing.Color.Red;
                                        celKQ1.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Underline);
                                    }
                                    else if (kq > tsden)
                                    {
                                        this.celKQ1.ForeColor = System.Drawing.Color.Red;
                                        celKQ1.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Underline);
                                    }
                                    else
                                    {
                                        this.celKQ1.ForeColor = System.Drawing.Color.Black;
                                        celKQ1.Font = new System.Drawing.Font("Times New Roman", 11, FontStyle.Regular);

                                    }
                                }
                                else if (dvct.TSnTu != null && dvct.TSnDen == null)//trị số nhỏ nhất
                                {
                                    double tstu = Convert.ToDouble(dvct.TSnTu);
                                    double kq = Convert.ToDouble(this.GetCurrentColumnValue("KetQua"));
                                    if (tstu > kq)
                                    {
                                        this.celKQ1.ForeColor = System.Drawing.Color.Red;
                                        celKQ1.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Underline);
                                    }
                                    else
                                    {
                                        this.celKQ1.ForeColor = System.Drawing.Color.Black;
                                        celKQ1.Font = new System.Drawing.Font("Times New Roman", 11, FontStyle.Regular);
                                    }
                                }
                                else if (dvct.TSnTu == null && dvct.TSnDen != null)//giá trị lớn nhất
                                {
                                    double tsden = Convert.ToDouble(dvct.TSnDen);
                                    double kq = Convert.ToDouble(this.GetCurrentColumnValue("KetQua"));
                                    if (kq >= tsden)
                                    {
                                        this.celKQ1.ForeColor = System.Drawing.Color.Red;
                                        celKQ1.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Underline);
                                    }
                                    else
                                    {
                                        this.celKQ1.ForeColor = System.Drawing.Color.Black;
                                        celKQ1.Font = new System.Drawing.Font("Times New Roman", 11, FontStyle.Regular);
                                    }

                                }
                                else
                                {
                                    this.celKQ1.ForeColor = System.Drawing.Color.Black;
                                    celKQ1.Font = new System.Drawing.Font("Times New Roman", 11, FontStyle.Regular);
                                }
                            }
                        }
                        catch
                        {
                            this.celKQ1.ForeColor = System.Drawing.Color.Black;
                            celKQ1.Font = new System.Drawing.Font("Times New Roman", 11, FontStyle.Regular);
                        }

                    }

                }
            }
        }

        private void sbdt30004_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV != "30372")
            {
                if (this.GetCurrentColumnValue("MaDVct") != null && this.GetCurrentColumnValue("KetQua") != null)
                {
                    string a = this.GetCurrentColumnValue("MaDVct").ToString();
                    var dvct = _ldvct.Where(p => p.MaDVct == a).FirstOrDefault();
                    if (dvct != null)
                    {
                        try
                        {
                            if (GT == 0)
                            {
                                if (dvct.TSnuTu != null && dvct.TSnuDen != null)
                                {
                                    double tstu = Convert.ToDouble(dvct.TSnuTu);
                                    double tsden = Convert.ToDouble(dvct.TSnuDen);
                                    if (DungChung.Bien.MaBV == "24012")
                                    {
                                        if(DungChung.Bien.check == 0 && DungChung.Bien.MaBV == "24012")
                                        {
                                            xrTable1.Visible = false;
                                            xrTable14.Visible = false;
                                            tb2.Visible = true;
                                        }
                                        if (dvct.TenDVct == "Số lượng bạch cầu" || dvct.TenDVct == "Số lượng tiểu cầu" || dvct.TenDVct == "Số lượng HC")
                                        {
                                            celTenXN30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                        }
                                        else
                                        {
                                            celTenXN30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Regular);
                                        }
                                    }


                                    double kq = Convert.ToDouble(this.GetCurrentColumnValue("KetQua"));
                                    if (kq < tstu)
                                    {
                                        
                                        this.celKQ30004.ForeColor = System.Drawing.Color.Red;
                                        celKQ30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                        if (DungChung.Bien.MaBV == "24012")
                                        {
                                            if (dvct.TenDVct == "Số lượng bạch cầu" || dvct.TenDVct == "Số lượng tiểu cầu" || dvct.TenDVct == "Số lượng HC")
                                            {
                                                celTenXN30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                            }
                                            else
                                            {
                                                celTenXN30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Regular);
                                            }
                                        }
                                    }
                                    else if (kq > tsden)
                                    {
                                        this.celKQ30004.ForeColor = System.Drawing.Color.Red;
                                        celKQ30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                        if (DungChung.Bien.MaBV == "24012")
                                        {
                                            if (dvct.TenDVct == "Số lượng bạch cầu" || dvct.TenDVct == "Số lượng tiểu cầu" || dvct.TenDVct == "Số lượng HC")
                                            {
                                                celTenXN30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                            }
                                            else
                                            {
                                                celTenXN30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Regular);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        this.celKQ30004.ForeColor = System.Drawing.Color.Black;
                                        celKQ30004.Font = new System.Drawing.Font("Times New Roman", 11, FontStyle.Regular);
                                        if (DungChung.Bien.MaBV == "24012")
                                        {
                                            if (dvct.TenDVct == "Số lượng bạch cầu" || dvct.TenDVct == "Số lượng tiểu cầu" || dvct.TenDVct == "Số lượng HC")
                                            {
                                                celTenXN30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                            }
                                            else
                                            {
                                                celTenXN30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Regular);
                                            }
                                        }
                                    }
                                }
                                else if (dvct.TSnuTu != null && dvct.TSnuDen == null)//trị số nhỏ nhất
                                {
                                    double tstu = Convert.ToDouble(dvct.TSnuTu);
                                    double kq = Convert.ToDouble(this.GetCurrentColumnValue("KetQua"));
                                    if (tstu > kq)
                                    {
                                        this.celKQ30004.ForeColor = System.Drawing.Color.Red;
                                        celKQ30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                        if (DungChung.Bien.MaBV == "24012")
                                        {
                                            if (dvct.TenDVct == "Số lượng bạch cầu" || dvct.TenDVct == "Số lượng tiểu cầu" || dvct.TenDVct == "Số lượng HC")
                                            {
                                                celTenXN30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                            }
                                            else
                                            {
                                                celTenXN30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Regular);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        this.celKQ30004.ForeColor = System.Drawing.Color.Black;
                                        celKQ30004.Font = new System.Drawing.Font("Times New Roman", 11, FontStyle.Regular);
                                        if (DungChung.Bien.MaBV == "24012")
                                        {
                                            if (dvct.TenDVct == "Số lượng bạch cầu" || dvct.TenDVct == "Số lượng tiểu cầu" || dvct.TenDVct == "Số lượng HC")
                                            {
                                                celTenXN30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                            }
                                            else
                                            {
                                                celTenXN30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Regular);
                                            }
                                        }
                                    }
                                }
                                else if (dvct.TSnuTu == null && dvct.TSnuDen != null)//giá trị lớn nhất
                                {
                                    double tsden = Convert.ToDouble(dvct.TSnuDen);
                                    double kq = Convert.ToDouble(this.GetCurrentColumnValue("KetQua"));
                                    if (kq > tsden)
                                    {
                                        this.celKQ30004.ForeColor = System.Drawing.Color.Red;
                                        celKQ30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                        if (DungChung.Bien.MaBV == "24012")
                                        {
                                            if (dvct.TenDVct == "Số lượng bạch cầu" || dvct.TenDVct == "Số lượng tiểu cầu" || dvct.TenDVct == "Số lượng HC")
                                            {
                                                celTenXN30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                            }
                                            else
                                            {
                                                celTenXN30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Regular);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        this.celKQ30004.ForeColor = System.Drawing.Color.Black;
                                        celKQ30004.Font = new System.Drawing.Font("Times New Roman", 11, FontStyle.Regular);
                                        if (DungChung.Bien.MaBV == "24012")
                                        {
                                            if (dvct.TenDVct == "Số lượng bạch cầu" || dvct.TenDVct == "Số lượng tiểu cầu" || dvct.TenDVct == "Số lượng HC")
                                            {
                                                celTenXN30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                            }
                                            else
                                            {
                                                celTenXN30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Regular);
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    this.celKQ30004.ForeColor = System.Drawing.Color.Black;
                                    celKQ30004.Font = new System.Drawing.Font("Times New Roman", 11, FontStyle.Regular);
                                    if (DungChung.Bien.MaBV == "24012")
                                    {
                                        if (dvct.TenDVct == "Số lượng bạch cầu" || dvct.TenDVct == "Số lượng tiểu cầu" || dvct.TenDVct == "Số lượng HC")
                                        {
                                            celTenXN30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                        }
                                        else
                                        {
                                            celTenXN30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Regular);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (dvct.TSnTu != null && dvct.TSnDen != null)
                                {
                                    double tstu = Convert.ToDouble(dvct.TSnTu);
                                    double tsden = Convert.ToDouble(dvct.TSnDen);
                                    double kq = Convert.ToDouble(this.GetCurrentColumnValue("KetQua"));
                                    if (DungChung.Bien.check == 0 && DungChung.Bien.MaBV == "24012")
                                    {
                                        xrTable1.Visible = false;
                                        xrTable14.Visible = false;
                                        tb2.Visible = true;
                                    }
                                    if (kq < tstu)
                                    {
                                        this.celKQ30004.ForeColor = System.Drawing.Color.Red;
                                        celKQ30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                        this.celKQ24012.ForeColor = System.Drawing.Color.Red;
                                        celKQ24012.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);

                                    }
                                    else if (kq > tsden)
                                    {
                                        this.celKQ30004.ForeColor = System.Drawing.Color.Red;
                                        celKQ30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                        this.celKQ24012.ForeColor = System.Drawing.Color.Red;
                                        celKQ24012.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                    }
                                    else
                                    {
                                        this.celKQ30004.ForeColor = System.Drawing.Color.Black;
                                        celKQ30004.Font = new System.Drawing.Font("Times New Roman", 11, FontStyle.Regular);
                                        this.celKQ24012.ForeColor = System.Drawing.Color.Black;
                                        celKQ24012.Font = new System.Drawing.Font("Times New Roman", 11, FontStyle.Regular);
                                    }
                                    if (DungChung.Bien.MaBV == "24012")
                                    {
                                        if (dvct.TenDVct == "Số lượng bạch cầu" || dvct.TenDVct == "Số lượng tiểu cầu" || dvct.TenDVct == "Số lượng HC")
                                        {
                                            celTenXN30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                            celTenXN24012.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                        }
                                        else
                                        {
                                            celTenXN30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Regular);
                                            celTenXN24012.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Regular);
                                        }
                                    }
                                }
                                else if (dvct.TSnTu != null && dvct.TSnDen == null)//trị số nhỏ nhất
                                {
                                    double tstu = Convert.ToDouble(dvct.TSnTu);
                                    double kq = Convert.ToDouble(this.GetCurrentColumnValue("KetQua"));
                                    if (tstu > kq)
                                    {
                                        this.celKQ30004.ForeColor = System.Drawing.Color.Red;
                                        celKQ30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                        this.celKQ30004.ForeColor = System.Drawing.Color.Red;
                                        celKQ24012.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                    }
                                    else
                                    {
                                        this.celKQ30004.ForeColor = System.Drawing.Color.Black;
                                        celKQ30004.Font = new System.Drawing.Font("Times New Roman", 11, FontStyle.Regular);
                                        this.celKQ30004.ForeColor = System.Drawing.Color.Black;
                                        celKQ24012.Font = new System.Drawing.Font("Times New Roman", 11, FontStyle.Regular);
                                    }
                                    if (DungChung.Bien.MaBV == "24012")
                                    {
                                        if (dvct.TenDVct == "Số lượng bạch cầu" || dvct.TenDVct == "Số lượng tiểu cầu" || dvct.TenDVct == "Số lượng HC")
                                        {
                                            celTenXN30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                            celTenXN24012.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                        }
                                        else
                                        {
                                            celTenXN30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Regular);
                                            celTenXN24012.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                        }
                                    }
                                }
                                else if (dvct.TSnTu == null && dvct.TSnDen != null)//giá trị lớn nhất
                                {
                                    double tsden = Convert.ToDouble(dvct.TSnDen);
                                    double kq = Convert.ToDouble(this.GetCurrentColumnValue("KetQua"));
                                   
                                    if (kq >= tsden)
                                    {
                                        this.celKQ24012.ForeColor = System.Drawing.Color.Red;
                                        celKQ24012.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                        this.celKQ30004.ForeColor = System.Drawing.Color.Red;
                                        celKQ30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                    }
                                    else
                                    {
                                        this.celKQ30004.ForeColor = System.Drawing.Color.Black;
                                        celKQ30004.Font = new System.Drawing.Font("Times New Roman", 11, FontStyle.Regular);
                                        this.celKQ24012.ForeColor = System.Drawing.Color.Black;
                                        celKQ24012.Font = new System.Drawing.Font("Times New Roman", 11, FontStyle.Regular);
                                    }
                                    if (DungChung.Bien.MaBV == "24012")
                                    {
                                        if (dvct.TenDVct == "Số lượng bạch cầu" || dvct.TenDVct == "Số lượng tiểu cầu" || dvct.TenDVct == "Số lượng HC")
                                        {
                                            celTenXN30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                            celTenXN24012.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                        }
                                        else
                                        {
                                            celTenXN30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Regular);
                                            celTenXN24012.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Regular);
                                        }
                                    }
                                }
                                else
                                {
                                    
                                    this.celKQ30004.ForeColor = System.Drawing.Color.Black;
                                    celKQ30004.Font = new System.Drawing.Font("Times New Roman", 11, FontStyle.Regular);
                                    if (DungChung.Bien.MaBV == "24012")
                                    {
                                        if (dvct.TenDVct == "Số lượng bạch cầu" || dvct.TenDVct == "Số lượng tiểu cầu" || dvct.TenDVct == "Số lượng HC")
                                        {
                                            celTenXN30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                            celTenXN24012.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                        }
                                        else
                                        {
                                            celTenXN30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Regular);
                                            celTenXN24012.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Regular);
                                        }
                                    }
                                }
                            }
                        }
                        catch
                        {
                            this.celKQ30004.ForeColor = System.Drawing.Color.Black;
                            celKQ30004.Font = new System.Drawing.Font("Times New Roman", 11, FontStyle.Regular);
                            if (DungChung.Bien.MaBV == "24012")
                            {
                                if (dvct.TenDVct == "Số lượng bạch cầu" || dvct.TenDVct == "Số lượng tiểu cầu" || dvct.TenDVct == "Số lượng HC")
                                {
                                    celTenXN30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                    celTenXN24012.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Bold);
                                }
                                else
                                {
                                    celTenXN30004.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Regular);
                                    celTenXN24012.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Regular);
                                }
                            }
                        }

                    }

                }
            }
        }

        private void GroupHeader3_24012_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.check == 0 || DungChung.Bien.check == 1)
            {
                xrTable15.Visible = false;
                tb1.Visible = true;
            }
           
        }
    }
}
