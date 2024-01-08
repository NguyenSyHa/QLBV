using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;

using DevExpress.XtraPrinting.Drawing;


namespace QLBV.Phieu
{
    public partial class rep_ToDieuTri_34019 : DevExpress.XtraReports.UI.XtraReport
    {
        public bool ToDieuTriYHCT_14018 = false;
        public rep_ToDieuTri_34019()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            //string xr = xrPageInfo1.PageInfo.ToString();
        }
        public void BindingData()
        {

            if (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017")
            {
                SubBand6.Visible = true;
            }
            else
                SubBand5.Visible = true;
            if (ToDieuTriYHCT_14018)
            {
                SubBand7.Visible = false;
            }
            else
                SubBand8.Visible = false;
            if (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017")
            {
                if (ToDieuTriYHCT_14018)
                    colNgayGio2.DataBindings.Add("Text", DataSource, "NgayNhapYHCT14018");
                else
                    colNgayGio2.DataBindings.Add("Text", DataSource, "NgayNhapHT").FormatString = "{0:dd/MM/yyyy}";
            }
            else if (DungChung.Bien.MaBV != "34019")
                colNgayGio.DataBindings.Add("Text", DataSource, "NgayNhapHT").FormatString = "{0:dd/MM/yyyy}";

            lblNgay.DataBindings.Add("Text", DataSource, "NgayNhapHT").FormatString = "{0:dd/MM/yyyy}";
            colDienbien.DataBindings.Add("Text", DataSource, "DienBien1");
            colYLenh.DataBindings.Add("Text", DataSource, "YLenh");
            xrRichText1.DataBindings.Add("Html", DataSource, "YLenh");

            colDienBien2.DataBindings.Add("Html", DataSource, "DienBien1");
            xrRichText2.DataBindings.Add("Html", DataSource, "YLenh");

            lblCount.DataBindings.Add("Text", DataSource, "Count");
            if (DungChung.Bien.MaBV != "14018"&& DungChung.Bien.MaBV != "14017")
                GroupHeader1.GroupFields.Add(new GroupField("NgayNhapHT"));
        }

        int a = 0;
        int so = 0;
        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "34019" || DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017")
            {
                if (a % 2 == 0)
                {
                    if (ToDieuTriYHCT_14018)
                    {
                        SubBand1.Visible = false;
                        SubBand2.Visible = true;
                    }
                    else
                    {
                        SubBand1.Visible = true;
                        SubBand2.Visible = false;
                    }

                    so++;
                    if (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017")
                    {
                        xrLabel21.Text = string.Format("Số: {0}", so.ToString("D2"));
                        xrLabel43.Text = string.Format("Số: {0}", so.ToString("D2"));
                    }
                }
                else
                {
                    SubBand1.Visible = false;
                    SubBand2.Visible = false;
                }
                a++;
            }
        }

        private void rep_ToDieuTri_YHCT_BeforePrint(object sender, CancelEventArgs e)
        {
            xrLabel19.Text = DungChung.Bien.TenCQ.ToUpper();
            if (DungChung.Bien.MaBV != "34019")
            {
                SubBand4.Visible = false;
                GroupHeader1.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.None;
                if (DungChung.Bien.MaBV != "14018"&& DungChung.Bien.MaBV != "14017")
                    xrRichText1.Visible = false;
            }


        }

        //Diễn biến thứ n trong ngày
        int num = 0;
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "34019")
            {

                num++;
                string strNgayGio = "";
                string strCount = "";



                if (this.GetCurrentColumnValue("Count") != null)
                    strCount = this.GetCurrentColumnValue("Count").ToString();
                if (this.GetCurrentColumnValue("NgayNhapHT") != null)
                    strNgayGio = Convert.ToDateTime(this.GetCurrentColumnValue("NgayNhapHT")).ToString("dd/MM/yyyy");
                if (num == 1)
                {
                    colNgayGio.Text = strNgayGio;
                    xrLine2.Visible = false;
                }
                else
                {
                    colNgayGio.Text = "";
                    xrLine2.Visible = true;
                }

                GroupFooter1.Visible = true;
                if (this.GetCurrentColumnValue("YLenh") != null)
                {
                    string yl = this.GetCurrentColumnValue("YLenh").ToString();
                }
            }
            else
            {
                colNgayGio2.Visible = true;
            }
        }

        private void xrTable2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "34019")
            {
                GroupFooter1.Visible = true;
                xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right))));
                ((XRTable)sender).Rows[0].Cells[0].Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right;
                ((XRTable)sender).Rows[0].Cells[1].Borders = DevExpress.XtraPrinting.BorderSide.Right;
                ((XRTable)sender).Rows[0].Cells[2].Borders = DevExpress.XtraPrinting.BorderSide.Right;
            }
            else if (DungChung.Bien.MaBV != "14018" && DungChung.Bien.MaBV != "14017")
            {
                xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
                ((XRTable)sender).Rows[0].Cells[0].Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                ((XRTable)sender).Rows[0].Cells[1].Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                ((XRTable)sender).Rows[0].Cells[2].Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
            }
        }

        private void GroupFooter1_BeforePrint(object sender, CancelEventArgs e)
        {
            num = 0;
        }

        private void Detail_AfterPrint(object sender, EventArgs e)
        {
        }

        private void PageFooter_AfterPrint(object sender, EventArgs e)
        {
        }

        private void rep_ToDieuTri_34019_FillEmptySpace(object sender, BandEventArgs e)
        {
            if (DungChung.Bien.MaBV == "34019")
            {
                float bandHeight = GraphicsUnitConverter.Convert(e.Band.Height, ReportUnit.ToDpi(), ReportUnit.TenthsOfAMillimeter.ToDpi());
                if (bandHeight <= 63.5)
                {
                    if (bandHeight >= 1)
                    {
                        XRLine xrl = new XRLine();
                        xrl.Dpi = ReportUnit.TenthsOfAMillimeter.ToDpi();
                        xrl.LocationF = new PointF(0, 0);
                        xrl.HeightF = 1;

                        xrl.WidthF = (float)751 * (float)2.54;


                        e.Band.Controls.Add(xrl);
                    }
                    else
                        return;
                }
                else
                {
                    XRTable xrTable = new XRTable();
                    xrTable.Dpi = ReportUnit.TenthsOfAMillimeter.ToDpi();
                    xrTable.LocationF = new PointF(0, 0);
                    xrTable.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom))));
                    xrTable.BeginInit();
                    xrTable.WidthF = (float)751 * (float)2.54;

                    float rowHeight = bandHeight - 3;
                    XRTableRow row = new XRTableRow();
                    row.HeightF = rowHeight;
                    for (int j = 0; j < 3; j++)
                    {
                        XRTableCell cell = new XRTableCell();
                        cell.Text = "";
                        if (j == 0)
                        {
                            cell.WidthF = (float)125 * (float)2.54;
                        }
                        else if (j == 1)
                        {
                            cell.WidthF = (float)313 * (float)2.54;
                        }
                        else
                        {
                            cell.WidthF = (float)313 * (float)2.54;
                        }
                        cell.TextAlignment = TextAlignment.MiddleCenter;
                        row.Cells.Add(cell);
                    }
                    xrTable.Rows.Add(row);
                    // }
                    xrTable.AdjustSize();

                    xrTable.EndInit();
                    e.Band.Controls.Add(xrTable);
                }
            }

        }

        private void xrTable1_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        private void xrTable3_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        private void SubBand6_BeforePrint(object sender, CancelEventArgs e)
        {
        }
    }
}
