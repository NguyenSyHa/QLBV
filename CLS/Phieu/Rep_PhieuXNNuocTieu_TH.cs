using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
namespace QLBV.BaoCao
{
    public partial class Rep_PhieuXNNuocTieu_TH : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuXNNuocTieu_TH()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            ////     labT1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //xrTableCell7.Borders = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            int sophieu = int.Parse(SoPhieu.Value.ToString());

            var qhh = (from cls in DataContect.CLS.Where(p => p.IdCLS == sophieu)
                       join chidinh in DataContect.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                       join dichvu in DataContect.DichVus on chidinh.MaDV equals dichvu.MaDV
                       join tn in DataContect.TieuNhomDVs.Where(p => p.TenRG.Contains("Nước tiểu")) on dichvu.IdTieuNhom equals tn.IdTieuNhom
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
                if (qhh.Where(p => p.TenDVct.Contains("BLD")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("BLD")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("BLD")).Where(p => p.STTHT == 1).Count() > 0)
                    {
                        colKQ21.Text = qhh.Where(p => p.TenDVct.Contains("BLD")).Where(p => p.STTHT == 1).First().KetQua;
                    }
                    else colKQ21.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("BLD")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        colKQ22.Text = qhh.Where(p => p.TenDVct.Contains("BLD")).Where(p => p.STTHT == 2).First().KetQua;
                    }
                    else colKQ22.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("BLD")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        colKQ23.Text = qhh.Where(p => p.TenDVct.Contains("BLD")).Where(p => p.STTHT == 3).First().KetQua;
                    }
                    else colKQ23.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("BLD")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        colKQ24.Text = qhh.Where(p => p.TenDVct.Contains("BLD")).Where(p => p.STTHT == 4).First().KetQua;
                    }
                    else colKQ24.Text = " ";

                    if (qhh.Where(p => p.TenDVct.Contains("BLD")).Where(p => p.STTHT == 5).Count() > 0)
                    {
                        colKQ25.Text = qhh.Where(p => p.TenDVct.Contains("BLD")).Where(p => p.STTHT == 5).First().KetQua;
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
                        colKQ29.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 1).First().KetQua;
                    }
                    else colKQ29.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        colKQ30.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 2).First().KetQua;
                    }
                    else colKQ30.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        colKQ31.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 3).First().KetQua;
                    }
                    else colKQ31.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        colKQ32.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 4).First().KetQua;
                    }
                    else colKQ32.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).Count() > 0)
                    {
                        colKQ33.Text = qhh.Where(p => p.TenDVct.Contains("PRO")).Where(p => p.STTHT == 5).First().KetQua;
                    }
                    else colKQ33.Text = " ";
                }
                if (qhh.Where(p => p.TenDVct.Contains("UBG")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("UBG")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("UBG")).Where(p => p.STTHT == 1).Count() > 0)
                    {
                        colKQ34.Text = qhh.Where(p => p.TenDVct.Contains("UBG")).Where(p => p.STTHT == 1).First().KetQua;
                    }
                    else colKQ34.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("UBG")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        colKQ35.Text = qhh.Where(p => p.TenDVct.Contains("UBG")).Where(p => p.STTHT == 2).First().KetQua;
                    }
                    else colKQ35.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("UBG")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        colKQ36.Text = qhh.Where(p => p.TenDVct.Contains("UBG")).Where(p => p.STTHT == 3).First().KetQua;
                    }
                    else colKQ36.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("UBG")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        colKQ37.Text = qhh.Where(p => p.TenDVct.Contains("UBG")).Where(p => p.STTHT == 4).First().KetQua;
                    }
                    else colKQ37.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("UBG")).Where(p => p.STTHT == 5).Count() > 0)
                    {
                        colKQ38.Text = qhh.Where(p => p.TenDVct.Contains("UBG")).Where(p => p.STTHT == 5).First().KetQua;
                    }
                    else colKQ38.Text = " ";
                }
                if (qhh.Where(p => p.TenDVct.Contains("NIT")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("NIT")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).Count() > 0)
                    {
                        colKQ39.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 1).First().KetQua;
                    }
                    else colKQ39.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        colKQ40.Text = qhh.Where(p => p.TenDVct.Contains("NIT")).Where(p => p.STTHT == 2).First().KetQua;
                    }
                    else colKQ40.Text = " ";

                }
                if (qhh.Where(p => p.TenDVct.Contains("LEU")).Count() > 0 && qhh.Where(p => p.TenDVct.Contains("LEU")).First().KetQua != null)
                {
                    if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).Count() > 0)
                    {
                        colKQ41.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 1).First().KetQua;
                    }
                    else colKQ41.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).Count() > 0)
                    {
                        colKQ42.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 2).First().KetQua;
                    }
                    else colKQ42.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).Count() > 0)
                    {
                        colKQ43.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 3).First().KetQua;
                    }
                    else colKQ43.Text = " ";
                    if (qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).Count() > 0)
                    {
                        colKQ44.Text = qhh.Where(p => p.TenDVct.Contains("LEU")).Where(p => p.STTHT == 4).First().KetQua;
                    }
                    else colKQ44.Text = " ";
                }
            }
        }

        private void xrTableCell7_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (DungChung.Bien.MaBV == "02005")
            //{
            //    colSo.Visible = false;
            //    colTenTKXN.Visible = false;
            //}
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            ////////////////In Phôi/////////////
            //int sta = 0;
            //sta = Convert.ToInt32(Status.Value);
            //int sta = DungChung.Bien.InPhoi;

            //if (sta == 1) //in phôi tại phòng khám
            //{
            //    colTKXN.Visible = false;

            //}
            //if (sta == 2) //in phôi tại phòng CLS
            //{
            //    colTenCQCQ.Visible = false; colTenCQ.Visible = false;

            //    txtTenBN.Visible = false; txtTuoi.Visible = false; txtDiaChi.Visible = false;
            //    txtThe1.Visible = false; txtThe2.Visible = false; txtThe3.Visible = false; txtThe4.Visible = false; txtThe5.Visible = false;
            //    txtKhoa.Visible = false; txtBuong.Visible = false; txtGiuong.Visible = false; txtChanDoan.Visible = false;


            //    lab1.Visible = false; lab4.Visible = false; lab5.Visible = false; lab6.Visible = false;
            //    lab7.Visible = false; lab8.Visible = false; lab9.Visible = false; lab10.Visible = false;
            //    lab11.Visible = false; lab12.Visible = false; lab13.Visible = false; lab14.Visible = false; lab15.Visible = false;
            //    lab16.Visible = false; lab17.Visible = false; lab18.Visible = false; lab19.Visible = false; lab20.Visible = false;
            //    lab21.Visible = false; lab22.Visible = false; lab23.Visible = false; lab24.Visible = false; lab25.Visible = false; lab26.Visible = false;
            //    lab27.Visible = false; lab28.Visible = false; lab29.Visible = false; lab30.Visible = false; lab31.Visible = false; lab32.Visible = false;
            //    lab33.Visible = false; lab34.Visible = false; lab35.Visible = false; lab36.Visible = false; lab37.Visible = false; lab38.Visible = false;
            //    lab39.Visible = false; lab40.Visible = false; lab41.Visible = false; lab42.Visible = false; lab43.Visible = false; lab44.Visible = false;
            //    lab45.Visible = false; lab46.Visible = false; lab47.Visible = false; lab48.Visible = false; lab49.Visible = false; lab50.Visible = false;
            //    lab51.Visible = false; lab52.Visible = false; lab53.Visible = false; lab54.Visible = false; lab55.Visible = false; lab56.Visible = false;
            //    lab57.Visible = false; lab58.Visible = false; lab59.Visible = false; lab60.Visible = false; lab61.Visible = false; lab62.Visible = false;
            //    lab63.Visible = false; lab64.Visible = false; lab65.Visible = false; lab66.Visible = false; lab67.Visible = false; lab68.Visible = false;
            //    lab69.Visible = false; lab70.Visible = false; lab71.Visible = false; lab72.Visible = false; lab73.Visible = false; lab74.Visible = false;
            //    lab75.Visible = false; lab76.Visible = false; lab77.Visible = false; lab78.Visible = false; lab79.Visible = false; lab80.Visible = false;
            //    lab81.Visible = false; lab82.Visible = false; lab83.Visible = false; lab84.Visible = false; lab85.Visible = false; lab86.Visible = false;
            //    lab87.Visible = false; lab88.Visible = false; lab89.Visible = false; lab90.Visible = false; lab91.Visible = false; lab92.Visible = false;
            //    lab93.Visible = false;  lab94.Visible = false;  lab95.Visible = false;  lab96.Visible = false;  lab97.Visible = false;  lab98.Visible = false; 
            //    lab99.Visible = false; lab100.Visible = false; lab101.Visible = false; lab102.Visible = false; lab103.Visible = false; lab104.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab105.Visible = false; lab106.Visible = false; lab107.Visible = false;  lab108.Visible = false;  lab109.Visible = false;  lab110.Visible = false; 
            //    lab111.Visible = false;  lab112.Visible = false;  lab113.Visible = false;  


            //    txtThe1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    txtThe2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    txtThe3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    txtThe3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    txtThe4.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    txtThe5.Borders = DevExpress.XtraPrinting.BorderSide.None;

            //    txtThuong.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    txtCapCuu.Borders = DevExpress.XtraPrinting.BorderSide.None;

            //    //lab1.Borders = DevExpress.XtraPrinting.BorderSide.None; lab4.Borders = DevExpress.XtraPrinting.BorderSide.None; lab5.Borders = DevExpress.XtraPrinting.BorderSide.None; lab6.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    //lab7.Borders = DevExpress.XtraPrinting.BorderSide.None; lab8.Borders = DevExpress.XtraPrinting.BorderSide.None; lab9.Borders = DevExpress.XtraPrinting.BorderSide.None; lab10.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    //lab11.Borders = DevExpress.XtraPrinting.BorderSide.None; lab12.Borders = DevExpress.XtraPrinting.BorderSide.None; lab13.Borders = DevExpress.XtraPrinting.BorderSide.None; lab14.Borders = DevExpress.XtraPrinting.BorderSide.None; lab15.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    //lab16.Borders = DevExpress.XtraPrinting.BorderSide.None; lab17.Borders = DevExpress.XtraPrinting.BorderSide.None; lab18.Borders = DevExpress.XtraPrinting.BorderSide.None; 
            //    lab19.Borders = DevExpress.XtraPrinting.BorderSide.None; lab20.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab21.Borders = DevExpress.XtraPrinting.BorderSide.None; lab22.Borders = DevExpress.XtraPrinting.BorderSide.None; lab23.Borders = DevExpress.XtraPrinting.BorderSide.None; lab24.Borders = DevExpress.XtraPrinting.BorderSide.None; lab25.Borders = DevExpress.XtraPrinting.BorderSide.None; lab26.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab27.Borders = DevExpress.XtraPrinting.BorderSide.None; lab28.Borders = DevExpress.XtraPrinting.BorderSide.None; lab29.Borders = DevExpress.XtraPrinting.BorderSide.None; lab30.Borders = DevExpress.XtraPrinting.BorderSide.None; lab31.Borders = DevExpress.XtraPrinting.BorderSide.None; lab32.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab33.Borders = DevExpress.XtraPrinting.BorderSide.None; lab34.Borders = DevExpress.XtraPrinting.BorderSide.None; lab35.Borders = DevExpress.XtraPrinting.BorderSide.None; lab36.Borders = DevExpress.XtraPrinting.BorderSide.None; lab37.Borders = DevExpress.XtraPrinting.BorderSide.None; lab38.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab39.Borders = DevExpress.XtraPrinting.BorderSide.None; lab40.Borders = DevExpress.XtraPrinting.BorderSide.None; lab41.Borders = DevExpress.XtraPrinting.BorderSide.None; lab42.Borders = DevExpress.XtraPrinting.BorderSide.None; lab43.Borders = DevExpress.XtraPrinting.BorderSide.None; lab44.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab45.Borders = DevExpress.XtraPrinting.BorderSide.None; lab46.Borders = DevExpress.XtraPrinting.BorderSide.None; lab47.Borders = DevExpress.XtraPrinting.BorderSide.None; lab48.Borders = DevExpress.XtraPrinting.BorderSide.None; lab49.Borders = DevExpress.XtraPrinting.BorderSide.None; lab50.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab51.Borders = DevExpress.XtraPrinting.BorderSide.None; lab52.Borders = DevExpress.XtraPrinting.BorderSide.None; lab53.Borders = DevExpress.XtraPrinting.BorderSide.None; lab54.Borders = DevExpress.XtraPrinting.BorderSide.None; lab55.Borders = DevExpress.XtraPrinting.BorderSide.None; lab56.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab57.Borders = DevExpress.XtraPrinting.BorderSide.None; lab58.Borders = DevExpress.XtraPrinting.BorderSide.None; lab59.Borders = DevExpress.XtraPrinting.BorderSide.None; lab60.Borders = DevExpress.XtraPrinting.BorderSide.None; lab61.Borders = DevExpress.XtraPrinting.BorderSide.None; lab62.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab63.Borders = DevExpress.XtraPrinting.BorderSide.None; lab64.Borders = DevExpress.XtraPrinting.BorderSide.None; lab65.Borders = DevExpress.XtraPrinting.BorderSide.None; lab66.Borders = DevExpress.XtraPrinting.BorderSide.None; lab67.Borders = DevExpress.XtraPrinting.BorderSide.None; lab68.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab69.Borders = DevExpress.XtraPrinting.BorderSide.None; lab70.Borders = DevExpress.XtraPrinting.BorderSide.None; lab71.Borders = DevExpress.XtraPrinting.BorderSide.None; lab72.Borders = DevExpress.XtraPrinting.BorderSide.None; lab73.Borders = DevExpress.XtraPrinting.BorderSide.None; lab74.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab75.Borders = DevExpress.XtraPrinting.BorderSide.None; lab76.Borders = DevExpress.XtraPrinting.BorderSide.None; lab77.Borders = DevExpress.XtraPrinting.BorderSide.None; lab78.Borders = DevExpress.XtraPrinting.BorderSide.None; lab79.Borders = DevExpress.XtraPrinting.BorderSide.None; lab80.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab81.Borders = DevExpress.XtraPrinting.BorderSide.None; lab82.Borders = DevExpress.XtraPrinting.BorderSide.None; lab83.Borders = DevExpress.XtraPrinting.BorderSide.None; lab84.Borders = DevExpress.XtraPrinting.BorderSide.None; lab85.Borders = DevExpress.XtraPrinting.BorderSide.None; lab86.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab87.Borders = DevExpress.XtraPrinting.BorderSide.None; lab88.Borders = DevExpress.XtraPrinting.BorderSide.None; lab89.Borders = DevExpress.XtraPrinting.BorderSide.None; lab90.Borders = DevExpress.XtraPrinting.BorderSide.None; lab91.Borders = DevExpress.XtraPrinting.BorderSide.None; lab92.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab93.Borders = DevExpress.XtraPrinting.BorderSide.None; lab94.Borders = DevExpress.XtraPrinting.BorderSide.None; lab95.Borders = DevExpress.XtraPrinting.BorderSide.None; lab96.Borders = DevExpress.XtraPrinting.BorderSide.None; lab97.Borders = DevExpress.XtraPrinting.BorderSide.None; lab98.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab99.Borders = DevExpress.XtraPrinting.BorderSide.None; lab100.Borders = DevExpress.XtraPrinting.BorderSide.None; lab101.Borders = DevExpress.XtraPrinting.BorderSide.None; lab102.Borders = DevExpress.XtraPrinting.BorderSide.None; lab103.Borders = DevExpress.XtraPrinting.BorderSide.None; lab104.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab105.Borders = DevExpress.XtraPrinting.BorderSide.None; lab106.Borders = DevExpress.XtraPrinting.BorderSide.None; lab107.Borders = DevExpress.XtraPrinting.BorderSide.None; lab108.Borders = DevExpress.XtraPrinting.BorderSide.None; lab109.Borders = DevExpress.XtraPrinting.BorderSide.None; lab110.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    lab111.Borders = DevExpress.XtraPrinting.BorderSide.None; lab112.Borders = DevExpress.XtraPrinting.BorderSide.None; lab113.Borders = DevExpress.XtraPrinting.BorderSide.None; 

            //    colKQ1.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ7.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ13.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ19.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ25.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    colKQ2.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ8.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ14.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ20.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ26.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    colKQ3.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ9.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ15.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ21.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ27.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    colKQ4.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ10.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ16.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ22.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ28.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    colKQ5.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ11.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ17.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ23.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ29.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    colKQ6.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ12.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ18.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ24.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ30.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    colKQ31.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ32.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ33.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ34.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ35.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    colKQ36.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ37.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ38.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ39.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ40.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    colKQ41.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ42.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ43.Borders = DevExpress.XtraPrinting.BorderSide.None; colKQ44.Borders = DevExpress.XtraPrinting.BorderSide.None; 

            //    colBSDT.Visible = false;

            //}  
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
         
                colBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
                colTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
       
        }
    }
}
