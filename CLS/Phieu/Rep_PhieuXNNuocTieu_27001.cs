using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
namespace QLBV.BaoCao
{
    public partial class Rep_PhieuXNNuocTieu_27001 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuXNNuocTieu_27001()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

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
                    else if(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).Count() > 0 )
                    {
                        colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 2).First().KetQua;
                    }
                    else if(qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 3).First().KetQua;
                    }
                    else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).Count() > 0 )
                    {
                        colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 4).First().KetQua;
                    }
                    else if (qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).Count() > 0)
                    {
                        colKQ1.Text = qhh.Where(p => p.TenDVct.Contains("Glu")).Where(p => p.STTHT == 5).First().KetQua;
                    }
                    else colKQ1.Text = " ";
                }
                if (qhh.Where(p => p.TenDVct.Contains("BIL")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("BIL")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).Count() > 0)
                    {
                        colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 1).First().KetQua;
                    }
                    else if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 2).First().KetQua;
                    }
                    else if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 3).First().KetQua;
                    }
                    else if (qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        colKQ6.Text = qhh.Where(p => p.TenDVct.Contains("BIL")).Where(p => p.STTHT == 4).First().KetQua;
                    }
                    else colKQ6.Text = " ";

                }
                if (qhh.Where(p => p.TenDVct.Contains("KET")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("KET")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).Count() > 0)
                    {
                        colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 1).First().KetQua; 
                    }
                    else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 2).First().KetQua; 
                    }
                    else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 3).First().KetQua; 
                    }
                    else  if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua; 
                    }
                    else  if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).Count() > 0)
                    {
                        colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 5).First().KetQua; 
                    }
                    else if (qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 6).Count() > 0)
                    {
                        colKQ10.Text = qhh.Where(p => p.TenDVct.Contains("KET")).Where(p => p.STTHT == 4).First().KetQua; 
                    }
                    else colKQ10.Text = " ";
                }
                if (qhh.Where(p => p.TenDVct.Contains("SG")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("SG")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).Count() > 0)
                    {
                        colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 1).First().KetQua; 
                    }
                    else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 2).First().KetQua; 
                    }
                    else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 3).First().KetQua; 
                    }
                    else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 4).First().KetQua; 
                    }
                    else if (qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).Count() > 0)
                    {
                        colKQ16.Text = qhh.Where(p => p.TenDVct.Contains("SG")).Where(p => p.STTHT == 5).First().KetQua; 
                    }
                    else colKQ16.Text = " ";

                }
                if (qhh.Where(p => p.TenDVct.Contains("BLO")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("BLO")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).Count() > 0)
                    {
                        colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 1).First().KetQua; 
                    }
                    else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 2).First().KetQua; 
                    }
                    else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 3).First().KetQua; 
                    }
                    else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 4).First().KetQua; 
                    }
                    else if (qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).Count() > 0)
                    {
                        colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLO")).Where(p => p.STTHT == 5).First().KetQua; 
                    }
                    else colKQ21.Text = " ";

                }
                if (qhh.Where(p => p.TenDVct.Contains("PH")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("PH")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).Count() > 0)
                    {
                        colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 1).First().KetQua; 
                    }
                    else if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 2).First().KetQua; 
                    }
                    else if (qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        colKQ26.Text = qhh.Where(p => p.TenDVct.Contains("PH")).Where(p => p.STTHT == 3).First().KetQua; 
                    }
                    else colKQ26.Text = " ";
                }
                if (qhh.Where(p => p.TenDVct.Contains("PRO")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("PRO")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).Count() > 0)
                    {
                        colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua; 
                    }
                    else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua; 
                    }
                    else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua; 
                    }
                    else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua; 
                    }
                    else if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).Count() > 0)
                    {
                        colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua; 
                    }
                    else colKQ29.Text = " ";
                }
                if (qhh.Where(p => p.TenDVct.Contains("URO")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("URO")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).Count() > 0)
                    {
                        colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 1).First().KetQua; 
                    }
                    else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 2).First().KetQua; 
                    }
                    else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 3).First().KetQua; 
                    }
                    else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 4).First().KetQua; 
                    }
                    else if (qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).Count() > 0)
                    {
                        colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("URO")).Where(p => p.STTHT == 5).First().KetQua; 
                    }
                    else colKQ34.Text = " ";
                }
                if (qhh.Where(p => p.TenDVct.Contains("NIT")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("NIT")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).Count() > 0)
                    {
                        colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().KetQua; 
                    }
                    else if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua; 
                    }
                    else colKQ39.Text = " ";

                }
                if (qhh.Where(p => p.TenDVct.Contains("LEU")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("LEU")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).Count() > 0)
                    {
                        colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().KetQua; 
                    }
                    else if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua; 
                    }
                    else if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua; 
                    }
                    else if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua; 
                    }
                    else colKQ41.Text = " ";
                }
            }
        }

        private void xrTableCell7_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.ChanDoan.Value != null) {
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
            //if (DungChung.Bien.MaBV == "02005")
            //{
            //    colSo.Visible = false;
            //    colTenTKXN.Visible = false;
            //}
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            if (DungChung.Bien.MaBV == "30003") // Chí Linh
            {
                pnNamNu.Visible = false;
                pnNamNu_CL.Visible = true;
                if (this.Nam.Value != null && this.Nam.Value.ToString().Length > 0)
                {
                    this.Nam.Value = "".ToUpper();
                    this.Nu.Value = "X".ToUpper();
                }
                else {
                    this.Nam.Value = "X".ToUpper();
                    this.Nu.Value = "".ToUpper();
                }
            }
            
            if (DungChung.Bien.MaBV == "27183")
            {
                xrPictureBox3.Visible = true;
                colTenCQCQ.Visible = false;
                colTenCQ.Visible = false;
                xrLabel1.Visible = false;
            }
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "01049")
            {
                xrLabel1.Visible = false;
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if(DungChung.Bien.MaBV=="27001")
            {
                lab90.Visible = false;
                lab89.Visible = false;
                colBSDT.Visible = false;
            }    
            if (DungChung.Bien.MaBV == "04012")
                lab92.Text = "Trưởng khoa cận lâm sàng".ToUpper();
            if (MaCBDT.Value!=null)
            {
                colBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
            }
            if (Macb.Value != null)
            {
                var tencb = DataContect.CanBoes.Where(p => p.MaCB == Macb.Value).Select(p => p.TenCB).FirstOrDefault();
                if (tencb != null && tencb != "")
                {
                    if (DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "27001")
                        colTKXN.Text = tencb;
                    else if (DungChung.Bien.MaBV == "12001")
                        colTKXN.Text = "BS: " + tencb;
                    else
                        colTKXN.Text = "Người thực hiện: " + tencb;
                }
            }
            if (DungChung.Bien.MaBV == "30004")
            {
                lab89.Visible = false;
                lab90.Visible = false;
                colBSDT.Visible = false;
                colTKXN.Visible = false;
            }
        }

        private void lab92_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30007")
            {
                lab92.Text = "Bác Sỹ Cận Lâm Sàng".ToUpper();
            }
        }

        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
            rep_PhieuXN_Sub repsub = (rep_PhieuXN_Sub)xrSubreport1.ReportSource;
            repsub.NGAYKIXN.Value = NgayTH.Value.ToString();
            repsub.NGAYKYDT.Value = NgayCD.Value.ToString();
            repsub.BSDT.Value = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
            repsub.TKXN.Value = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            repsub.tb12128.ForeColor = System.Drawing.Color.Black;
        }

        private void Rep_PhieuXNNuocTieu_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "12128")
            {
                GroupFooter1.Visible = true;
                ReportFooter.Visible = false;
            }
        }
    }
}
