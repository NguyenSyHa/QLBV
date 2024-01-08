

using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuDienTim_01071 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuDienTim_01071()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                lab1.Font = new Font(lab1.Font, FontStyle.Regular);
                lab2.Font = new Font(lab2.Font, FontStyle.Regular);
                colSo.Font = new Font(colSo.Font, FontStyle.Regular);
                lab3.Font = new Font(lab3.Font, FontStyle.Regular);
                xrTableCell6.Font = new Font(xrTableCell6.Font, FontStyle.Regular);
                // colMaBN.Font = new Font(colMaBN.Font, FontStyle.Regular);
                sovv.Font = new Font(sovv.Font, FontStyle.Regular);
                lab54.Font = new Font(lab54.Font, FontStyle.Regular);
                colTenBSDT.Font = new Font(colTenBSDT.Font, FontStyle.Regular);
                colTenTKXN1.Font = new Font(colTenTKXN.Font, FontStyle.Regular);
                xrTableCell1.Font = new Font(xrTableCell1.Font, FontStyle.Regular);
                //colYCCC.Font = new Font(colYCCC.Font, FontStyle.Regular);
                xrLabel3.Font = new Font(xrLabel3.Font, FontStyle.Regular);
                xrRichText1.Font = new Font(xrRichText1.Font, FontStyle.Regular);
                txtmabn.Font = new Font(txtmabn.Font, FontStyle.Regular);
            }
            if (DungChung.Bien.MaBV == "24297" && DungChung.Bien.CheckInFull == 1)
            {
                SubBand2.Visible = true;
                SubBand1.Visible = false;
                xrLabel13.Visible = false;
                ReportHeader.Visible = false;
            }
            if (DungChung.Bien.MaBV == "24297" && DungChung.Bien.CheckInFull == 2)
            {
                SubBand2.Visible = false;
                SubBand1.Visible = true;
                xrTable5.Visible = false;
                rowBSDieuTri.Visible = false;
            }
            if (DungChung.Bien.MaBV == "24297" && DungChung.Bien.CheckInFull == 3)
            {
                SubBand2.Visible = false;
                SubBand1.Visible = false;
                rowBSDieuTri.Visible = true;
                Detail.Visible = false;
                ReportFooter.Visible = false;
                PageFooter.Visible = false;
            }

            if (DungChung.Bien.MaBV == "24297" && DungChung.Bien.CheckInFull == 4)
            {
                Detail.Visible = true;
                SubBand2.Visible = false;
                SubBand1.Visible = true;
                rowBSDieuTri.Visible = true;

            }

            //if (DungChung.Bien.MaBV == "01071")
            //{
            //    ReportFooter.Visible = false;
            //    PageFooter.Visible = true;
            //}
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

            QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //  string mabn = MaBNhan.Value.ToString();
            var qcls = (from dvct in DataContect.DichVucts
                        join dv in DataContect.DichVus on dvct.MaDV equals dv.MaDV
                        join tnhomdv in DataContect.TieuNhomDVs on dv.IdTieuNhom equals tnhomdv.IdTieuNhom
                        where (tnhomdv.TenTN.Contains("Điện tim"))
                        select new { dvct.MaDVct, dvct.TenDVct, dvct.STT }).ToList();

            if (qcls.Count > 0)
            {
                if (qcls.Where(p => p.STT == 1).Count() > 0 && qcls.Where(p => p.STT == 1).First().TenDVct != null)
                {
                    col1.Text = qcls.Where(p => p.STT == 1).First().TenDVct.ToString() + ": ";
                }
                if (qcls.Where(p => p.STT == 2).Count() > 0 && qcls.Where(p => p.STT == 2).First().TenDVct != null)
                {
                    col2.Text = qcls.Where(p => p.STT == 2).First().TenDVct.ToString() + ": ";
                }
                if (qcls.Where(p => p.STT == 3).Count() > 0 && qcls.Where(p => p.STT == 3).First().TenDVct != null)
                {
                    col3.Text = qcls.Where(p => p.STT == 3).First().TenDVct.ToString() + ": ";
                }
                if (qcls.Where(p => p.STT == 4).Count() > 0 && qcls.Where(p => p.STT == 4).First().TenDVct != null)
                {
                    col4.Text = qcls.Where(p => p.STT == 4).First().TenDVct.ToString() + ": ";
                }
                if (qcls.Where(p => p.STT == 5).Count() > 0 && qcls.Where(p => p.STT == 5).First().TenDVct != null)
                {
                    col5.Text = qcls.Where(p => p.STT == 5).First().TenDVct.ToString() + ": ";
                }
                if (qcls.Where(p => p.STT == 6).Count() > 0 && qcls.Where(p => p.STT == 6).First().TenDVct != null)
                {
                    col6.Text = qcls.Where(p => p.STT == 6).First().TenDVct.ToString() + ": ";
                }
                if (qcls.Where(p => p.STT == 7).Count() > 0 && qcls.Where(p => p.STT == 7).First().TenDVct != null)
                {
                    col7.Text = qcls.Where(p => p.STT == 7).First().TenDVct.ToString() + ": ";
                }
                if (qcls.Where(p => p.STT == 8).Count() > 0 && qcls.Where(p => p.STT == 8).First().TenDVct != null)
                {
                    col8.Text = qcls.Where(p => p.STT == 8).First().TenDVct.ToString() + ": ";
                }
                if (qcls.Where(p => p.STT == 9).Count() > 0 && qcls.Where(p => p.STT == 9).First().TenDVct != null)
                {
                    col9.Text = qcls.Where(p => p.STT == 9).First().TenDVct.ToString() + ": ";
                }
                if (qcls.Where(p => p.STT == 10).Count() > 0 && qcls.Where(p => p.STT == 10).First().TenDVct != null)
                {
                    col10.Text = qcls.Where(p => p.STT == 10).First().TenDVct.ToString() + ": ";
                }
                if (qcls.Where(p => p.STT == 11).Count() > 0 && qcls.Where(p => p.STT == 11).First().TenDVct != null)
                {
                    col11.Text = qcls.Where(p => p.STT == 11).First().TenDVct.ToString() + ": ";
                }
                if (qcls.Where(p => p.STT == 12).Count() > 0 && qcls.Where(p => p.STT == 12).First().TenDVct != null)
                {
                    col12.Text = qcls.Where(p => p.STT == 12).First().TenDVct.ToString() + ": ";
                }
            }
            ///////////////////////////////

            int sophieu = 0;
            int ot;
            if (Int32.TryParse(SoPhieu.Value.ToString(), out ot))
                sophieu = int.Parse(SoPhieu.Value.ToString());

            var qhh = (from cls in DataContect.CLS.Where(p => p.IdCLS == sophieu)
                       join cd in DataContect.ChiDinhs on cls.IdCLS equals cd.IdCLS
                       join clsct in DataContect.CLScts on cd.IDCD equals clsct.IDCD
                       join dvct in DataContect.DichVucts on clsct.MaDVct equals dvct.MaDVct
                       join dv in DataContect.DichVus on dvct.MaDV equals dv.MaDV
                       select new { clsct.MaDVct, clsct.KetQua, dvct.TenDVct, dvct.STT }).ToList();
            if (qhh.Count > 0)
            {
                #region bệnh viện 12345
                //if (DungChung.Bien.MaBV =="12345")
                //{
                //    if (qhh.Where(p => p.STT == 1).Count() > 0 && qhh.Where(p => p.STT == 1).First().KetQua != null)
                //    {
                //        txtKQ1.Text = qhh.Where(p => p.STT == 1).First().KetQua.ToString();

                //    }
                //    else
                //        col1.Text = "";
                //    if (qhh.First().KetQua != null)
                //    {
                //        colLoiDan.Text = qhh.First().KetQua;// lay loi dan can xem lai
                //    }
                //    if (qhh.Where(p => p.STT == 2).Count() > 0 && qhh.Where(p => p.STT == 2).First().KetQua != null)
                //    {
                //        txtKQ2.Text = qhh.Where(p => p.STT == 2).First().KetQua.ToString();

                //    }
                //    else col2.Text = "";
                //    if (qhh.Where(p => p.STT == 3).Count() > 0 && qhh.Where(p => p.STT == 3).First().KetQua != null)
                //    {
                //        txtKQ3.Text = qhh.Where(p => p.STT == 3).First().KetQua.ToString();

                //    }
                //    else col3.Text = "";
                //    if (qhh.Where(p => p.STT == 4).Count() > 0 && qhh.Where(p => p.STT == 4).First().KetQua != null)
                //    {
                //        txtKQ4.Text = qhh.Where(p => p.STT == 4).First().KetQua.ToString();

                //    }
                //    else col4.Text = "";
                //    if (qhh.Where(p => p.STT == 5).Count() > 0 && qhh.Where(p => p.STT == 5).First().KetQua != null)
                //    {
                //        txtKQ5.Text = qhh.Where(p => p.STT == 5).First().KetQua.ToString();

                //    }
                //    else col5.Text = "";
                //    if (qhh.Where(p => p.STT == 6).Count() > 0 && qhh.Where(p => p.STT == 6).First().KetQua != null)
                //    {
                //        txtKQ6.Text = qhh.Where(p => p.STT == 6).First().KetQua.ToString();

                //    }
                //    else col6.Text = "";
                //    if (qhh.Where(p => p.STT == 7).Count() > 0 && qhh.Where(p => p.STT == 7).First().KetQua != null)
                //    {
                //        txtKQ7.Text = qhh.Where(p => p.STT == 7).First().KetQua.ToString();

                //    }
                //    else col7.Text = "";
                //    if (qhh.Where(p => p.STT == 8).Count() > 0 && qhh.Where(p => p.STT == 8).First().KetQua != null)
                //    {
                //        txtKQ8.Text = qhh.Where(p => p.STT == 8).First().KetQua.ToString();

                //    }
                //    else col8.Text = "";
                //    if (qhh.Where(p => p.STT == 9).Count() > 0 && qhh.Where(p => p.STT == 9).First().KetQua != null)
                //    {
                //        txtKQ9.Text = qhh.Where(p => p.STT == 9).First().KetQua.ToString();

                //    }
                //    else col9.Text = "";
                //    if (qhh.Where(p => p.STT == 10).Count() > 0 && qhh.Where(p => p.STT == 10).First().KetQua != null)
                //    {
                //        txtKQ10.Text = qhh.Where(p => p.STT == 10).First().KetQua.ToString();

                //    }
                //    else col10.Text = "";
                //    if (qhh.Where(p => p.STT == 11).Count() > 0 && qhh.Where(p => p.STT == 11).First().KetQua != null)
                //    {
                //        txtKQ11.Text = qhh.Where(p => p.STT == 11).First().KetQua.ToString();

                //    }
                //    else col11.Text = "";
                //    if (qhh.Where(p => p.STT == 12).Count() > 0 && qhh.Where(p => p.STT == 12).First().KetQua != null)
                //    {
                //        txtKQ12.Text = qhh.Where(p => p.STT == 12).First().KetQua.ToString();
                //    }
                //    else col12.Text = "";

                //}
                #endregion
                //else
                //{
                if (qhh.Where(p => p.STT == 1).Count() > 0 && qhh.Where(p => p.STT == 1).First().KetQua != null)
                {
                    txtKQ1_01071.Text = txtKQ1.Text = qhh.Where(p => p.STT == 1).First().KetQua.ToString();
                }
                if (qhh.First().KetQua != null)
                {
                    colLoiDan.Text = qhh.First().KetQua;// lay loi dan can xem lai
                }
                if (qhh.Where(p => p.STT == 2).Count() > 0 && qhh.Where(p => p.STT == 2).First().KetQua != null)
                {
                    txtKQ2_01071.Text = txtKQ2.Text = qhh.Where(p => p.STT == 2).First().KetQua.ToString();
                }

                if (qhh.Where(p => p.STT == 3).Count() > 0 && qhh.Where(p => p.STT == 3).First().KetQua != null)
                {
                    txtKQ3_01071.Text = txtKQ3.Text = qhh.Where(p => p.STT == 3).First().KetQua.ToString();

                }

                if (qhh.Where(p => p.STT == 4).Count() > 0 && qhh.Where(p => p.STT == 4).First().KetQua != null)
                {
                    txtKQ4_01071.Text = txtKQ4.Text = qhh.Where(p => p.STT == 4).First().KetQua.ToString();

                }

                if (qhh.Where(p => p.STT == 5).Count() > 0 && qhh.Where(p => p.STT == 5).First().KetQua != null)
                {
                    txtKQ5_01071.Text = txtKQ5.Text = qhh.Where(p => p.STT == 5).First().KetQua.ToString();

                }

                if (qhh.Where(p => p.STT == 6).Count() > 0 && qhh.Where(p => p.STT == 6).First().KetQua != null)
                {
                    txtKQ6_01071.Text = txtKQ6.Text = qhh.Where(p => p.STT == 6).First().KetQua.ToString();

                }

                if (qhh.Where(p => p.STT == 7).Count() > 0 && qhh.Where(p => p.STT == 7).First().KetQua != null)
                {
                    txtKQ7_01071.Text = txtKQ7.Text = qhh.Where(p => p.STT == 7).First().KetQua.ToString();

                }

                if (qhh.Where(p => p.STT == 8).Count() > 0 && qhh.Where(p => p.STT == 8).First().KetQua != null)
                {
                    txtKQ8_01071.Text = txtKQ8.Text = qhh.Where(p => p.STT == 8).First().KetQua.ToString();

                }

                if (qhh.Where(p => p.STT == 9).Count() > 0 && qhh.Where(p => p.STT == 9).First().KetQua != null)
                {
                    txtKQ9_01071.Text = txtKQ9.Text = qhh.Where(p => p.STT == 9).First().KetQua.ToString();

                }

                if (qhh.Where(p => p.STT == 10).Count() > 0 && qhh.Where(p => p.STT == 10).First().KetQua != null)
                {
                    txtKQ10_01071.Text = txtKQ10.Text = qhh.Where(p => p.STT == 10).First().KetQua.ToString();

                }

                if (qhh.Where(p => p.STT == 11).Count() > 0 && qhh.Where(p => p.STT == 11).First().KetQua != null)
                {
                    txtKQ11_01071.Text = txtKQ11.Text = qhh.Where(p => p.STT == 11).First().KetQua.ToString();

                }

                if (qhh.Where(p => p.STT == 12).Count() > 0 && qhh.Where(p => p.STT == 12).First().KetQua != null)
                {
                    txtKQ12_01071.Text = txtKQ12.Text = qhh.Where(p => p.STT == 12).First().KetQua.ToString();
                }

                //}

            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
            {
                txtmabn.Visible = true;
                xrRichText1.Visible = true;
                lab2.Visible = true;
                thuong.Visible = true;
                thuong1.Visible = true;
                capcuu.Visible = true;
                capcuu1.Visible = true;
            }

            tb_ChiDinh.Visible = DungChung.Bien._Visible_CDHA[0];
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[2];
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            string _bs = MaCBDT.Value.ToString();

            colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, _bs);
            colTenTKXN1.Text = DungChung.Ham._getTenCB(DataContect, _bs);

            if (DungChung.Bien.MaBV == "02005")
            {
                rowBSDieuTri.Visible = false;
            }
            if (DungChung.Bien.MaBV == "30004")
            {
                colTenTKXN.Visible = false;
                colTenBSDT.Visible = false;
            }
            if (Macb.Value != null)
            {
                colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
                colTenTKXN1.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
                colTenTKXN2.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
                colTenTKXN3.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            }

            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                xrRichText1.LocationF = lab2.LocationF;
                var lf = lab2.LocationF;
                lf.X = txtmabn.LocationF.X;
                txtmabn.LocationF = lf;
                colSo.Visible = lab3.Visible = lab2.Visible = sovv.Visible = false;
                SubBand3.Visible = true;
                Detail.Visible = false;
                if (Macb.Value != null)
                    colTenTKXN2.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
                tb_KetQua1.Visible = DungChung.Bien._Visible_CDHA[1];
                tb_KetQua1.Visible = DungChung.Bien._Visible_CDHA[2];
                tb_KetQua2.Visible = DungChung.Bien._Visible_CDHA[1];
                tb_KetQua2.Visible = DungChung.Bien._Visible_CDHA[2];
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {


        }

    }
}
