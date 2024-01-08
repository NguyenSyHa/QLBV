using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;
//using DevExpress.XtraRichEdit.API.Word;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuXNHuyetHoc_01071 : DevExpress.XtraReports.UI.XtraReport
    {
        string _TenTN = "";
        public Rep_PhieuXNHuyetHoc_01071(string _tenTN, int gtinh)
        {
            InitializeComponent();
            _lTen.Add("XN huyết học");
            _TenTN = _tenTN;
            GT = gtinh;
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
            public byte STT_F { get; set; }
            public int? STTdvct { get; set; }

            public string TenDVct { get; set; }

            public int STTNhomHT { get; set; }
        }
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297")
            {
                xrLabel5.Visible = false;
                xrLabel6.Visible = false;
            }
            _ldvct = DataContect.DichVucts.ToList();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            if (DungChung.Bien.MaBV == "01049")
            {
                xrLabel3.Visible = true;
                xrLabel4.Visible = true;
            }
            if (Macb.Value != null)
            {
                //Start HIS 1455
                //colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
                colTenTKXN.Text = (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") ? "" : DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
                //end HIS 1455
            }
            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
            {
                lab54.Text = "BÁC SĨ CHỈ ĐỊNH";
                lab55.Text = "PHÒNG XÉT NGHIỆM";
                xrLabel14.Text = "PHÒNG XÉT NGHIỆM";
                xrLabel2.Visible = false;
                xrLabel1.Visible = false;
                //this.celKQ.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            }
            if (DungChung.Bien.MaBV == "24297")
            {
                colTenBSCD.Visible = true;
            }
            if (DungChung.Bien.MaBV == "30005")
            {
                lblTenXN.Text = "ĐÔNG MÁU";
            }

            if (DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "01071")
            {
                xrLabel17.LocationF = lab2.LocationF;
                var lf = lab2.LocationF;
                lf.X = xrLabel15.LocationF.X;
                xrLabel15.LocationF = lf;
                colSo.Visible = lab3.Visible = lab2.Visible = false; lblBarcode.Visible = txtBarcode.Visible = true;
                xrLabel14.Font = new System.Drawing.Font(xrLabel14.Font,FontStyle.Regular);
                lab1.Font = new System.Drawing.Font(lab1.Font, FontStyle.Regular);
                lblTenXN.Font = new System.Drawing.Font(lblTenXN.Font, FontStyle.Regular);
                lab2.Font = new System.Drawing.Font(lab2.Font, FontStyle.Regular);
                lab3.Font = new System.Drawing.Font(lab3.Font, FontStyle.Regular);
                colSo.Font = new System.Drawing.Font(colSo.Font, FontStyle.Regular);
                xrLabel17.Font = new System.Drawing.Font(xrLabel17.Font, FontStyle.Regular);
                xrLabel15.Font = new System.Drawing.Font(xrLabel15.Font, FontStyle.Regular);
                xrTableCell14.Font = new System.Drawing.Font(xrTableCell14.Font, FontStyle.Regular);
                xrTableCell15.Font = new System.Drawing.Font(xrTableCell15.Font, FontStyle.Regular);
                xrTableCell16.Font = new System.Drawing.Font(xrTableCell16.Font, FontStyle.Regular);
                xrTableCell17.Font = new System.Drawing.Font(xrTableCell17.Font, FontStyle.Regular);
                xrTableCell18.Font = new System.Drawing.Font(xrTableCell18.Font, FontStyle.Regular);
                xrTableCell19.Font = new System.Drawing.Font(xrTableCell19.Font, FontStyle.Regular);
                xrLabel1.Font = new System.Drawing.Font(xrLabel1.Font, FontStyle.Regular);
                xrLabel2.Font = new System.Drawing.Font(xrLabel2.Font, FontStyle.Regular);
                lab54.Font = new System.Drawing.Font(lab54.Font, FontStyle.Regular);
                lab55.Font = new System.Drawing.Font(lab55.Font, FontStyle.Regular);
                colTenTKXN.Font = new System.Drawing.Font(colTenTKXN.Font, FontStyle.Regular);
                celTenDV.Font = new System.Drawing.Font(celTenDV.Font, FontStyle.Regular);
                lab54.Visible = false;
                colTenBSCD.Visible = false;
            }
            //colTenTKXN.Text=DungChung.Bien.

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            lblNgayIn.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            if (DungChung.Bien.MaBV == "30005")
            {
                xrLabel2.Visible = false;
                lab54.Text = "BÁC SỸ ĐIỀU TRỊ";
            }
        }



        internal void dataBinhding()
        {
            celTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            celMa.DataBindings.Add("Text", DataSource, "MaDVct");
            celTenXN.DataBindings.Add("Text", DataSource, "TenDVct");
            celKQ.DataBindings.Add("Text", DataSource, "KetQua");
            celTSBT.DataBindings.Add("Text", DataSource, "TSBT");
            celDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            GroupHeader1.GroupFields.Add(new GroupField("STTNhomHT"));
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (_TenTN == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)
            {
                if (this.GetCurrentColumnValue("STTNhomHT") != null)
                {
                    int a = Convert.ToInt32(this.GetCurrentColumnValue("STTNhomHT"));
                    if (a > 1)
                    {
                        GroupHeader1.Visible = false;
                        this.celTenXN.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
                    }
                }
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
                            else if (dvct.TSnuTu == null && dvct.TSnuDen != null)//giá trị lớn nhất
                            {
                                double tsden = Convert.ToDouble(dvct.TSnuDen);
                                double kq = Convert.ToDouble(dvct.KetQua);
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
                            else if (dvct.TSnTu == null && dvct.TSnDen != null)//giá trị lớn nhất
                            {
                                double tsden = Convert.ToDouble(dvct.TSnDen);
                                double kq = Convert.ToDouble(dvct.KetQua);
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
}
