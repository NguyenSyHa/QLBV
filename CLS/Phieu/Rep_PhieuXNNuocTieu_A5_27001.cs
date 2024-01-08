using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuXNNuocTieu_A5_27001 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuXNNuocTieu_A5_27001()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.ChanDoan.Value != null)
            {
                //  System.Windows.Forms.MessageBox.Show(this.ChanDoan.Value.ToString().Length.ToString());
                if (this.ChanDoan.Value.ToString().Length > 80 && this.ChanDoan.Value.ToString().Length < 100)

                    txtChanDoan.Font = new Font("Times New Roman", 10);
                else
                    if (this.ChanDoan.Value.ToString().Length >= 100 && this.ChanDoan.Value.ToString().Length < 110)
                        txtChanDoan.Font = new Font("Times New Roman", 9);
                    else
                        if (this.ChanDoan.Value.ToString().Length >= 110)
                            txtChanDoan.Font = new Font("Times New Roman", 8);

            }
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        //private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    if (MaCBDT.Value != null)
        //    {
        //        colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
        //    }
        //    if (Macb.Value != null)
        //    {
        //        colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
        //    }
         
        //}

        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
            rep_PhieuXN_Sub_A5 repsub = (rep_PhieuXN_Sub_A5)xrSubreport1.ReportSource;
            repsub.NGAYKIXN.Value = NgayTH.Value.ToString();
            repsub.NGAYKYDT.Value = NgayCD.Value.ToString();
            repsub.BSDT.Value = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
            repsub.TKXN.Value = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            repsub.tb12128.ForeColor = System.Drawing.Color.Black;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            int sophieu = int.Parse(SoPhieu.Value.ToString());

            var qhh = (from cls in DataContect.CLS.Where(p => p.IdCLS == sophieu)
                       join chidinh in DataContect.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                       join dichvu in DataContect.DichVus on chidinh.MaDV equals dichvu.MaDV
                       join clsct in DataContect.CLScts on chidinh.IDCD equals clsct.IDCD
                       join dvct in DataContect.DichVucts on clsct.MaDVct equals dvct.MaDVct
                       select new { clsct.KetQua, clsct.STTHT, dvct.TenDVct }).ToList();
            if (qhh.Count > 0)
            {
                if (qhh.Where(p => p.TenDVct.Contains("Glu")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("Glu")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("Glu")).First().KetQua != null)
                    {
                        colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 1).First().KetQua;
                    }
                    else colKQ1.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        colKQ2.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua;
                    }
                    else colKQ2.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        colKQ3.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua;
                    }
                    else colKQ3.Text = " ";

                    if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        colKQ4.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua;
                    }
                    else colKQ4.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).Count() > 0)
                    {
                        colKQ5.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua;
                    }
                    else colKQ5.Text = " ";
                }
                if (qhh.Where(p => p.TenDVct.Contains("BIL")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("BIL")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).Count() > 0)
                    {
                        colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua;
                    }
                    else colKQ6.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        colKQ7.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua;
                    }
                    else colKQ7.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        colKQ8.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua;
                    }
                    else colKQ8.Text = " ";

                    if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        colKQ9.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua;
                    }
                    else colKQ9.Text = " ";

                }
                if (qhh.Where(p => p.TenDVct.Contains("KET")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("KET")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).Count() > 0)
                    {
                        colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua;
                    }
                    else colKQ10.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        colKQ11.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua;
                    }
                    else colKQ11.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        colKQ12.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua;
                    }
                    else colKQ12.Text = " ";

                    if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        colKQ13.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua;
                    }
                    else colKQ13.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).Count() > 0)
                    {
                        colKQ14.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua;
                    }
                    else colKQ14.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).Count() > 0)
                    {
                        colKQ15.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua;
                    }
                    else colKQ15.Text = " ";
                }
                if (qhh.Where(p => p.TenDVct.Contains("SG")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("SG")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).Count() > 0)
                    {
                        colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua;
                    }
                    else colKQ16.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        colKQ17.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua;
                    }
                    else colKQ17.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        colKQ18.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua;
                    }
                    else colKQ18.Text = " ";

                    if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        colKQ19.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua;
                    }
                    else colKQ19.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).Count() > 0)
                    {
                        colKQ20.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua;
                    }
                    else colKQ20.Text = " ";

                }
                if (qhh.Where(p => p.TenDVct.Contains("BLO")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("BLO")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).Count() > 0)
                    {
                        colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua;
                    }
                    else colKQ21.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        colKQ22.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua;
                    }
                    else colKQ22.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        colKQ23.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua;
                    }
                    else colKQ23.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        colKQ24.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua;
                    }
                    else colKQ24.Text = " ";

                    if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).Count() > 0)
                    {
                        colKQ25.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua;
                    }
                    else colKQ25.Text = " ";

                }
                if (qhh.Where(p => p.TenDVct.Contains("PH")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("PH")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).Count() > 0)
                    {
                        colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua;
                    }
                    else colKQ26.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        colKQ27.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua;
                    }
                    else colKQ27.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        colKQ28.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua;
                    }
                    else colKQ28.Text = " ";
                }
                if (qhh.Where(p => p.TenDVct.Contains("PRO")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("PRO")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).Count() > 0)
                    {
                        xrTableCell10.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua;
                    }
                    else xrTableCell10.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        xrTableCell13.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua;
                    }
                    else xrTableCell13.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        xrTableCell16.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua;
                    }
                    else xrTableCell16.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        xrTableCell19.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua;
                    }
                    else xrTableCell19.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).Count() > 0)
                    {
                        xrTableCell22.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua;
                    }
                    else xrTableCell22.Text = " ";
                }
                if (qhh.Where(p => p.TenDVct.Contains("URO")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("URO")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).Count() > 0)
                    {
                        xrTableCell37.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua;
                    }
                    else xrTableCell37.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        xrTableCell40.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua;
                    }
                    else xrTableCell40.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        xrTableCell43.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua;
                    }
                    else xrTableCell43.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        xrTableCell46.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua;
                    }
                    else xrTableCell46.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).Count() > 0)
                    {
                        xrTableCell49.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua;
                    }
                    else xrTableCell49.Text = " ";
                }
                if (qhh.Where(p => p.TenDVct.Contains("NIT")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("NIT")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).Count() > 0)
                    {
                        xrTableCell55.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().KetQua;
                    }
                    else xrTableCell55.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        xrTableCell58.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua;
                    }
                    else xrTableCell58.Text = " ";

                }
                if (qhh.Where(p => p.TenDVct.Contains("LEU")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("LEU")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).Count() > 0)
                    {
                        xrTableCell25.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().KetQua;
                    }
                    else xrTableCell25.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        xrTableCell28.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua;
                    }
                    else xrTableCell28.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        xrTableCell31.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua;
                    }
                    else xrTableCell31.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        xrTableCell34.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua;
                    }
                    else xrTableCell34.Text = " ";
                }
            }
        }

    }
}
