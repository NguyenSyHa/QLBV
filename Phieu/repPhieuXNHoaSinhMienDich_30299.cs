using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
//using DevExpress.XtraRichEdit.API.Word;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class repPhieuXNHoaSinhMienDich_30299 : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuXNHoaSinhMienDich_30299()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if(DungChung.Bien.MaBV=="27001")
            {
                lab112.Visible = false;
                lab110.Visible = false;
                colTenBSDT.Visible = false;
            }    
            if (DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297")
            {
                xrLabel12.Visible = false;
                xrLabel13.Visible = false;
            }
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {

                SubBand3.Visible = true;
            }
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            colDiaChi.Text = DungChung.Ham.GetDiaChiBV();
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
            {
                lblKhoaXN.Visible = true;
                txtMaBnhan.Visible = true;
                xrLabel11.Visible = true;
                lab2.Visible = false;
            }
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
            {
                xrLabel3.Visible = false;

            }

            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                xrLabel11.LocationF = lab2.LocationF;
                var lf = lab2.LocationF;
                lf.X = txtMaBnhan.LocationF.X;
                txtMaBnhan.LocationF = lf;
                colSo.Visible = lab3.Visible = lab2.Visible = false; txtBarcode.Visible = lblBarcode.Visible = true;
                lab1.Font = new System.Drawing.Font(lab1.Font, FontStyle.Regular);
                lblKhoaXN.Font = new System.Drawing.Font(lblKhoaXN.Font, FontStyle.Regular);
                lab2.Font = new System.Drawing.Font(lab2.Font, FontStyle.Regular);
                colSo.Font = new System.Drawing.Font(colSo.Font, FontStyle.Regular);
                lab3.Font = new System.Drawing.Font(lab3.Font, FontStyle.Regular);
                xrLabel11.Font = new System.Drawing.Font(xrLabel11.Font, FontStyle.Regular);
                txtMaBnhan.Font = new System.Drawing.Font(txtMaBnhan.Font, FontStyle.Regular);
                xrLabel10.Font = new System.Drawing.Font(xrLabel10.Font, FontStyle.Regular);
                xrTableCell4.Font = new System.Drawing.Font(xrTableCell4.Font, FontStyle.Regular);
                lab16.Font = new System.Drawing.Font(lab16.Font, FontStyle.Regular);
                lab17.Font = new System.Drawing.Font(lab17.Font, FontStyle.Regular);
                lab18.Font = new System.Drawing.Font(lab18.Font, FontStyle.Regular);
                lab112.Font = new System.Drawing.Font(lab112.Font, FontStyle.Regular);
                lab113.Font = new System.Drawing.Font(lab113.Font, FontStyle.Regular);
                colTenTKXN.Font = new System.Drawing.Font(colTenTKXN.Font, FontStyle.Regular);
                colTenBSDT.Font = new System.Drawing.Font(colTenBSDT.Font, FontStyle.Regular);

            }

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {

            if (MaCBDT.Value != null)
            {
                colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
            }

            if (Macb.Value != "")
            {
                if (DungChung.Bien.MaBV == "27001")
                {
                    colTenTKXN.Text = DataContect.CanBoes.Where(p => p.MaCB == Macb.Value).FirstOrDefault().TenCB.ToString();

                }
                else
                {
                    colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
                }


            }
            if (DungChung.Bien.MaBV == "27001")
            {
                lab112.Text = "Y BÁC SỸ";
                lab113.Text = "PHÒNG XÉT NGHIỆM";
            }
            
        }
        
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                
                SubBand2.Visible = true;
            }
            else if (DungChung.Bien.MaBV == "30003")
            {
                SubBand4.Visible = true;
            }
            else
            {
                SubBand1.Visible = true;
            }
            if (DungChung.Bien.MaBV != "01071" && DungChung.Bien.MaBV != "01049")
            {
                var qcls = (from dvct in DataContect.DichVucts
                            join dv in DataContect.DichVus on dvct.MaDV equals dv.MaDV
                            join tnhomdv in DataContect.TieuNhomDVs.Where(p => p.TenRG == "XN hóa sinh miễn dịch") on dv.IdTieuNhom equals tnhomdv.IdTieuNhom
                            select new { tnhomdv.TenTN, dvct.MaDVct, dvct.TenDVct, dvct.TSBT, dvct.STT, dv.MaQD, tnhomdv.TenRG, dvct.DonVi}).ToList();

                if (qcls.Count > 0)
                {
                    #region xn hóa sinh miễn dịch
                    if (DungChung.Bien.MaBV == "30003")
                    {
                        if (qcls.Where(p => p.STT == 1).Count() > 0 && qcls.Where(p => p.STT == 1).First().TenDVct != null)
                        {
                            col_txn1.Text = qcls.Where(p => p.STT == 1).First().TenDVct.ToString();
                        }
                        if (qcls.Where(p => p.STT == 1).Count() > 0 && qcls.Where(p => p.STT == 1).First().DonVi != null)
                        {
                            col_dv1.Text = qcls.Where(p => p.STT == 1).First().DonVi.ToString();
                        }
                        if (qcls.Where(p => p.STT == 1).Count() > 0 && qcls.Where(p => p.STT == 1).First().MaQD != null)
                        {
                            col_ma1.Text = qcls.Where(p => p.STT == 1).First().MaQD.ToString();
                        }
                        if (qcls.Where(p => p.STT == 1).Count() > 0 && qcls.Where(p => p.STT == 1).First().TSBT != null)
                        {
                            col_tsbt1.Text = qcls.Where(p => p.STT == 1).First().TSBT.ToString();
                        }
                    }
                    if (qcls.Where(p => p.STT == 1).Count() > 0 && qcls.Where(p => p.STT == 1).First().TenDVct != null)
                    {
                        lab22.Text = qcls.Where(p => p.STT == 1).First().TenDVct.ToString();
                    }
                    if (qcls.Where(p => p.STT == 1).Count() > 0 && qcls.Where(p => p.STT == 1).First().MaQD != null)
                    {
                        ma1.Text = qcls.Where(p => p.STT == 1).First().MaQD.ToString();
                    }
                    if (qcls.Where(p => p.STT == 1).Count() > 0 && qcls.Where(p => p.STT == 1).First().TSBT != null)
                    {
                        lab23.Text = qcls.Where(p => p.STT == 1).First().TSBT.ToString();
                    }
                    if (qcls.Where(p => p.STT == 2).Count() > 0 && qcls.Where(p => p.STT == 2).First().TenDVct != null)
                    {
                        lab26.Text = qcls.Where(p => p.STT == 2).First().TenDVct.ToString();
                    }
                    if (qcls.Where(p => p.STT == 2).Count() > 0 && qcls.Where(p => p.STT == 2).First().MaQD != null)
                    {
                        ma2.Text = qcls.Where(p => p.STT == 2).First().MaQD.ToString();
                    }
                    if (qcls.Where(p => p.STT == 2).Count() > 0 && qcls.Where(p => p.STT == 2).First().TSBT != null)
                    {
                        lab27.Text = qcls.Where(p => p.STT == 2).First().TSBT.ToString();
                    }
                    if (qcls.Where(p => p.STT == 3).Count() > 0 && qcls.Where(p => p.STT == 3).First().TenDVct != null)
                    {
                        lab30.Text = qcls.Where(p => p.STT == 3).First().TenDVct.ToString();
                    }
                    if (qcls.Where(p => p.STT == 3).Count() > 0 && qcls.Where(p => p.STT == 3).First().MaQD != null)
                    {
                        ma3.Text = qcls.Where(p => p.STT == 3).First().MaQD.ToString();
                    }
                    if (qcls.Where(p => p.STT == 3).Count() > 0 && qcls.Where(p => p.STT == 3).First().TSBT != null)
                    {
                        lab31.Text = qcls.Where(p => p.STT == 3).First().TSBT.ToString();
                    }
                    if (qcls.Where(p => p.STT == 4).Count() > 0 && qcls.Where(p => p.STT == 4).First().TenDVct != null)
                    {
                        lab34.Text = qcls.Where(p => p.STT == 4).First().TenDVct.ToString();
                    }
                    if (qcls.Where(p => p.STT == 4).Count() > 0 && qcls.Where(p => p.STT == 4).First().MaQD != null)
                    {
                        ma4.Text = qcls.Where(p => p.STT == 4).First().MaQD.ToString();
                    }
                    if (qcls.Where(p => p.STT == 4).Count() > 0 && qcls.Where(p => p.STT == 4).First().TSBT != null)
                    {
                        lab35.Text = qcls.Where(p => p.STT == 4).First().TSBT.ToString();
                    }
                    if (qcls.Where(p => p.STT == 5).Count() > 0 && qcls.Where(p => p.STT == 5).First().TenDVct != null)
                    {
                        lab38.Text = qcls.Where(p => p.STT == 5).First().TenDVct.ToString();
                    }
                    if (qcls.Where(p => p.STT == 5).Count() > 0 && qcls.Where(p => p.STT == 5).First().MaQD != null)
                    {
                        ma5.Text = qcls.Where(p => p.STT == 5).First().MaQD.ToString();
                    }
                    if (qcls.Where(p => p.STT == 5).Count() > 0 && qcls.Where(p => p.STT == 5).First().TSBT != null)
                    {
                        lab39.Text = qcls.Where(p => p.STT == 5).First().TSBT.ToString();
                    }
                    if (qcls.Where(p => p.STT == 6).Count() > 0 && qcls.Where(p => p.STT == 6).First().TenDVct != null)
                    {
                        lab42.Text = qcls.Where(p => p.STT == 6).First().TenDVct.ToString();
                    }
                    if (qcls.Where(p => p.STT == 6).Count() > 0 && qcls.Where(p => p.STT == 6).First().MaQD != null)
                    {
                        ma6.Text = qcls.Where(p => p.STT == 6).First().MaQD.ToString();
                    }
                    if (qcls.Where(p => p.STT == 6).Count() > 0 && qcls.Where(p => p.STT == 6).First().TSBT != null)
                    {
                        lab43.Text = qcls.Where(p => p.STT == 6).First().TSBT.ToString();
                    }
                    if (qcls.Where(p => p.STT == 7).Count() > 0 && qcls.Where(p => p.STT == 7).First().TenDVct != null)
                    {
                        lab46.Text = qcls.Where(p => p.STT == 7).First().TenDVct.ToString();
                    }
                    if (qcls.Where(p => p.STT == 7).Count() > 0 && qcls.Where(p => p.STT == 7).First().MaQD != null)
                    {
                        ma7.Text = qcls.Where(p => p.STT == 7).First().MaQD.ToString();
                    }
                    if (qcls.Where(p => p.STT == 7).Count() > 0 && qcls.Where(p => p.STT == 7).First().TSBT != null)
                    {
                        lab47.Text = qcls.Where(p => p.STT == 7).First().TSBT.ToString();
                    }
                    if (qcls.Where(p => p.STT == 8).Count() > 0 && qcls.Where(p => p.STT == 8).First().TenDVct != null)
                    {
                        lab50.Text = qcls.Where(p => p.STT == 8).First().TenDVct.ToString();
                    }
                    if (qcls.Where(p => p.STT == 8).Count() > 0 && qcls.Where(p => p.STT == 8).First().MaQD != null)
                    {
                        ma8.Text = qcls.Where(p => p.STT == 8).First().MaQD.ToString();
                    }
                    if (qcls.Where(p => p.STT == 8).Count() > 0 && qcls.Where(p => p.STT == 8).First().TSBT != null)
                    {
                        lab51.Text = qcls.Where(p => p.STT == 8).First().TSBT.ToString();
                    }
                    if (qcls.Where(p => p.STT == 9).Count() > 0 && qcls.Where(p => p.STT == 9).First().TenDVct != null)
                    {
                        lab54.Text = qcls.Where(p => p.STT == 9).First().TenDVct.ToString();
                    }
                    if (qcls.Where(p => p.STT == 9).Count() > 0 && qcls.Where(p => p.STT == 9).First().MaQD != null)
                    {
                        ma9.Text = qcls.Where(p => p.STT == 9).First().MaQD.ToString();
                    }
                    if (qcls.Where(p => p.STT == 9).Count() > 0 && qcls.Where(p => p.STT == 9).First().TSBT != null)
                    {
                        lab55.Text = qcls.Where(p => p.STT == 9).First().TSBT.ToString();
                    }
                    if (qcls.Where(p => p.STT == 10).Count() > 0 && qcls.Where(p => p.STT == 10).First().TenDVct != null)
                    {
                        lab58.Text = qcls.Where(p => p.STT == 10).First().TenDVct.ToString();
                    }
                    if (qcls.Where(p => p.STT == 10).Count() > 0 && qcls.Where(p => p.STT == 10).First().MaQD != null)
                    {
                        ma10.Text = qcls.Where(p => p.STT == 10).First().MaQD.ToString();
                    }
                    if (qcls.Where(p => p.STT == 10).Count() > 0 && qcls.Where(p => p.STT == 10).First().TSBT != null)
                    {
                        lab59.Text = qcls.Where(p => p.STT == 10).First().TSBT.ToString();
                    }
                    if (qcls.Where(p => p.STT == 11).Count() > 0 && qcls.Where(p => p.STT == 11).First().TenDVct != null)
                    {
                        lab62.Text = qcls.Where(p => p.STT == 11).First().TenDVct.ToString();
                    }
                    if (qcls.Where(p => p.STT == 11).Count() > 0 && qcls.Where(p => p.STT == 11).First().MaQD != null)
                    {
                        ma11.Text = qcls.Where(p => p.STT == 11).First().MaQD.ToString();
                    }
                    if (qcls.Where(p => p.STT == 11).Count() > 0 && qcls.Where(p => p.STT == 11).First().TSBT != null)
                    {
                        lab63.Text = qcls.Where(p => p.STT == 11).First().TSBT.ToString();
                    }
                    if (qcls.Where(p => p.STT == 12).Count() > 0 && qcls.Where(p => p.STT == 12).First().TenDVct != null)
                    {
                        lab66.Text = qcls.Where(p => p.STT == 12).First().TenDVct.ToString();
                    }
                    if (qcls.Where(p => p.STT == 12).Count() > 0 && qcls.Where(p => p.STT == 12).First().MaQD != null)
                    {
                        ma12.Text = qcls.Where(p => p.STT == 12).First().MaQD.ToString();
                    }
                    if (qcls.Where(p => p.STT == 12).Count() > 0 && qcls.Where(p => p.STT == 12).First().TSBT != null)
                    {
                        lab67.Text = qcls.Where(p => p.STT == 12).First().TSBT.ToString();
                    }
                    if (qcls.Where(p => p.STT == 13).Count() > 0 && qcls.Where(p => p.STT == 13).First().TenDVct != null)
                    {
                        lab70.Text = qcls.Where(p => p.STT == 13).First().TenDVct.ToString();
                    }
                    if (qcls.Where(p => p.STT == 13).Count() > 0 && qcls.Where(p => p.STT == 13).First().MaQD != null)
                    {
                        ma13.Text = qcls.Where(p => p.STT == 13).First().MaQD.ToString();
                    }
                    if (qcls.Where(p => p.STT == 13).Count() > 0 && qcls.Where(p => p.STT == 13).First().TSBT != null)
                    {
                        lab71.Text = qcls.Where(p => p.STT == 13).First().TSBT.ToString();
                    }
                    if (qcls.Where(p => p.STT == 14).Count() > 0 && qcls.Where(p => p.STT == 14).First().TenDVct != null)
                    {
                        lab74.Text = qcls.Where(p => p.STT == 14).First().TenDVct.ToString();
                    }
                    if (qcls.Where(p => p.STT == 14).Count() > 0 && qcls.Where(p => p.STT == 14).First().TSBT != null)
                    {
                        lab75.Text = qcls.Where(p => p.STT == 14).First().TSBT.ToString();
                    }
                    if (qcls.Where(p => p.STT == 14).Count() > 0 && qcls.Where(p => p.STT == 14).First().MaQD != null)
                    {
                        ma14.Text = qcls.Where(p => p.STT == 14).First().MaQD.ToString();
                    }
                    if (qcls.Where(p => p.STT == 15).Count() > 0 && qcls.Where(p => p.STT == 15).First().TenDVct != null)
                    {
                        lab78.Text = qcls.Where(p => p.STT == 15).First().TenDVct.ToString();
                    }
                    if (qcls.Where(p => p.STT == 15).Count() > 0 && qcls.Where(p => p.STT == 15).First().MaQD != null)
                    {
                        ma15.Text = qcls.Where(p => p.STT == 15).First().MaQD.ToString();
                    }
                    if (qcls.Where(p => p.STT == 15).Count() > 0 && qcls.Where(p => p.STT == 15).First().TSBT != null)
                    {
                        lab79.Text = qcls.Where(p => p.STT == 15).First().TSBT.ToString();
                    }


                    #endregion
                    int sophieu = int.Parse(SoPhieu.Value.ToString());
                    //int sophieu = 956;
                    var qhh = (from cls in DataContect.CLS.Where(p => p.IdCLS == sophieu)
                               join chidinh in DataContect.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                               join dichvu in DataContect.DichVus on chidinh.MaDV equals dichvu.MaDV
                               join tn in DataContect.TieuNhomDVs on dichvu.IdTieuNhom equals tn.IdTieuNhom
                               join clsct in DataContect.CLScts on chidinh.IDCD equals clsct.IDCD
                               join dvct in DataContect.DichVucts on clsct.MaDVct equals dvct.MaDVct
                               select new { clsct.MaDVct, clsct.KetQua, dvct.STT, tn.TenRG, cls.Status }).ToList();
                    
                    if (qhh.Count > 0)
                    {

                        #region xét nghiệm hóa sinh miễn dịch
                        if (DungChung.Bien.MaBV == "30003")
                        {
                            if (qhh.Where(p => p.STT == 1).Count() > 0 && qhh.Where(p => p.STT == 1).FirstOrDefault().MaDVct != null)
                            {
                                col_x1.Text = "X";
                            }
                            if (qhh.Where(p => p.STT == 1).Count() > 0 && qhh.Where(p => p.STT == 1).FirstOrDefault().KetQua != null)
                            {
                                col_kq1.Text = qhh.Where(p => p.STT == 1).FirstOrDefault().KetQua.ToString();
                            }
                        }
                        if (qhh.Where(p => p.STT == 1).Count() > 0 && qhh.Where(p => p.STT == 1).FirstOrDefault().MaDVct != null)
                        {
                            a1.Text = "X";
                        }
                        if (qhh.Where(p => p.STT == 1).Count() > 0 && qhh.Where(p => p.STT == 1).FirstOrDefault().KetQua != null)
                        {
                            col1.Text = qhh.Where(p => p.STT == 1).FirstOrDefault().KetQua.ToString();
                        }
                        if (qhh.Where(p => p.STT == 2).Count() > 0 && qhh.Where(p => p.STT == 2).FirstOrDefault().MaDVct != null)
                        {
                            a2.Text = "X";
                        }
                        if (qhh.Where(p => p.STT == 2).Count() > 0 && qhh.Where(p => p.STT == 2).FirstOrDefault().KetQua != null)
                        {
                            col2.Text = qhh.Where(p => p.STT == 2).FirstOrDefault().KetQua.ToString();
                        }
                        if (qhh.Where(p => p.STT == 3).Count() > 0 && qhh.Where(p => p.STT == 3).FirstOrDefault().MaDVct != null)
                        {
                            a3.Text = "X";
                        }
                        if (qhh.Where(p => p.STT == 3).Count() > 0 && qhh.Where(p => p.STT == 3).FirstOrDefault().KetQua != null)
                        {
                            col3.Text = qhh.Where(p => p.STT == 3).FirstOrDefault().KetQua.ToString();
                        }
                        if (qhh.Where(p => p.STT == 4).Count() > 0 && qhh.Where(p => p.STT == 4).FirstOrDefault().MaDVct != null)
                        {
                            a4.Text = "X";
                        }
                        if (qhh.Where(p => p.STT == 4).Count() > 0 && qhh.Where(p => p.STT == 4).FirstOrDefault().KetQua != null)
                        {
                            col4.Text = qhh.Where(p => p.STT == 4).FirstOrDefault().KetQua.ToString();
                        }
                        if (qhh.Where(p => p.STT == 5).Count() > 0 && qhh.Where(p => p.STT == 5).FirstOrDefault().MaDVct != null)
                        {
                            a5.Text = "X";
                        }
                        if (qhh.Where(p => p.STT == 5).Count() > 0 && qhh.Where(p => p.STT == 5).FirstOrDefault().KetQua != null)
                        {
                            col5.Text = qhh.Where(p => p.STT == 5).FirstOrDefault().KetQua.ToString();
                        }
                        if (qhh.Where(p => p.STT == 6).Count() > 0 && qhh.Where(p => p.STT == 6).FirstOrDefault().MaDVct != null)
                        {
                            a6.Text = "X";
                        }
                        if (qhh.Where(p => p.STT == 6).Count() > 0 && qhh.Where(p => p.STT == 6).FirstOrDefault().KetQua != null)
                        {
                            col6.Text = qhh.Where(p => p.STT == 6).FirstOrDefault().KetQua.ToString();
                        }
                        if (qhh.Where(p => p.STT == 7).Count() > 0 && qhh.Where(p => p.STT == 7).FirstOrDefault().MaDVct != null)
                        {
                            a7.Text = "X";
                        }
                        if (qhh.Where(p => p.STT == 7).Count() > 0 && qhh.Where(p => p.STT == 7).FirstOrDefault().KetQua != null)
                        {
                            col7.Text = qhh.Where(p => p.STT == 7).FirstOrDefault().KetQua.ToString();
                        }
                        if (qhh.Where(p => p.STT == 8).Count() > 0 && qhh.Where(p => p.STT == 8).FirstOrDefault().MaDVct != null)
                        {
                            a8.Text = "X";
                        }
                        if (qhh.Where(p => p.STT == 8).Count() > 0 && qhh.Where(p => p.STT == 8).FirstOrDefault().KetQua != null)
                        {
                            col8.Text = qhh.Where(p => p.STT == 8).FirstOrDefault().KetQua.ToString();
                        }
                        if (qhh.Where(p => p.STT == 9).Count() > 0 && qhh.Where(p => p.STT == 9).FirstOrDefault().MaDVct != null)
                        {
                            a9.Text = "X";
                        }
                        if (qhh.Where(p => p.STT == 9).Count() > 0 && qhh.Where(p => p.STT == 9).FirstOrDefault().KetQua != null)
                        {
                            col9.Text = qhh.Where(p => p.STT == 9).FirstOrDefault().KetQua.ToString();
                        }
                        if (qhh.Where(p => p.STT == 10).Count() > 0 && qhh.Where(p => p.STT == 10).FirstOrDefault().MaDVct != null)
                        {
                            a10.Text = "X";
                        }
                        if (qhh.Where(p => p.STT == 10).Count() > 0 && qhh.Where(p => p.STT == 10).FirstOrDefault().KetQua != null)
                        {
                            col10.Text = qhh.Where(p => p.STT == 10).FirstOrDefault().KetQua.ToString();
                        }
                        if (qhh.Where(p => p.STT == 11).Count() > 0 && qhh.Where(p => p.STT == 11).FirstOrDefault().MaDVct != null)
                        {
                            a11.Text = "X";
                        }
                        if (qhh.Where(p => p.STT == 11).Count() > 0 && qhh.Where(p => p.STT == 11).FirstOrDefault().KetQua != null)
                        {
                            col11.Text = qhh.Where(p => p.STT == 11).FirstOrDefault().KetQua.ToString();
                        }
                        if (qhh.Where(p => p.STT == 12).Count() > 0 && qhh.Where(p => p.STT == 12).FirstOrDefault().MaDVct != null)
                        {
                            a12.Text = "X";
                        }
                        if (qhh.Where(p => p.STT == 12).Count() > 0 && qhh.Where(p => p.STT == 12).FirstOrDefault().KetQua != null)
                        {
                            col12.Text = qhh.Where(p => p.STT == 12).FirstOrDefault().KetQua.ToString();
                        }
                        if (qhh.Where(p => p.STT == 13).Count() > 0 && qhh.Where(p => p.STT == 13).FirstOrDefault().MaDVct != null)
                        {
                            a13.Text = "X";
                        }
                        if (qhh.Where(p => p.STT == 13).Count() > 0 && qhh.Where(p => p.STT == 13).FirstOrDefault().KetQua != null)
                        {
                            col13.Text = qhh.Where(p => p.STT == 13).FirstOrDefault().KetQua.ToString();
                        }
                        if (qhh.Where(p => p.STT == 14).Count() > 0 && qhh.Where(p => p.STT == 14).FirstOrDefault().MaDVct != null)
                        {
                            a14.Text = "X";
                        }
                        if (qhh.Where(p => p.STT == 14).Count() > 0 && qhh.Where(p => p.STT == 14).FirstOrDefault().KetQua != null)
                        {
                            col14.Text = qhh.Where(p => p.STT == 14).FirstOrDefault().KetQua.ToString();
                        }
                        if (qhh.Where(p => p.STT == 15).Count() > 0 && qhh.Where(p => p.STT == 15).FirstOrDefault().MaDVct != null)
                        {
                            a15.Text = "X";
                        }
                        if (qhh.Where(p => p.STT == 15).Count() > 0 && qhh.Where(p => p.STT == 15).FirstOrDefault().KetQua != null)
                        {
                            col15.Text = qhh.Where(p => p.STT == 15).FirstOrDefault().KetQua.ToString();
                        }

                        #endregion
                    }

                    #region check kq null
                    //if (qhh.Count > 0)
                    //{
                    //    if (qhh.Where(p => p.STT == 1).FirstOrDefault().Status == 0)
                    //    {
                    //        col1.Text = "";
                    //    }
                    //    if (qhh.Where(p => p.STT == 2).FirstOrDefault().Status == 0)
                    //    {
                    //        col2.Text = "";
                    //    }
                    //    if (qhh.Where(p => p.STT == 3).FirstOrDefault().Status == 0)
                    //    {
                    //        col3.Text = "";
                    //    }
                    //    if (qhh.Where(p => p.STT == 4).FirstOrDefault().Status == 0)
                    //    {
                    //        col4.Text = "";
                    //    }
                    //    if (qhh.Where(p => p.STT == 5).FirstOrDefault().Status == 0)
                    //    {
                    //        col5.Text = "";
                    //    }
                    //    if (qhh.Where(p => p.STT == 6).FirstOrDefault().Status == 0)
                    //    {
                    //        col6.Text = "";
                    //    }
                    //    if (qhh.Where(p => p.STT == 7).FirstOrDefault().Status == 0)
                    //    {
                    //        col7.Text = "";
                    //    }
                    //    if (qhh.Where(p => p.STT == 8).FirstOrDefault().Status == 0)
                    //    {
                    //        col8.Text = "";
                    //    }
                    //    if (qhh.Where(p => p.STT == 9).FirstOrDefault().Status == 0)
                    //    {
                    //        col9.Text = "";
                    //    }
                    //    if (qhh.Where(p => p.STT == 10).FirstOrDefault().Status == 0)
                    //    {
                    //        col10.Text = "";
                    //    }
                    //    if (qhh.Where(p => p.STT == 11).FirstOrDefault().Status == 0)
                    //    {
                    //        col11.Text = "";
                    //    }
                    //    if (qhh.Where(p => p.STT == 12).FirstOrDefault().Status == 0)
                    //    {
                    //        col12.Text = "";
                    //    }
                    //    if (qhh.Where(p => p.STT == 13).FirstOrDefault().Status == 0)
                    //    {
                    //        col13.Text = "";
                    //    }
                    //    if (qhh.Where(p => p.STT == 14).FirstOrDefault().Status == 0)
                    //    {
                    //        col14.Text = "";
                    //    }
                    //    if (qhh.Where(p => p.STT == 15).FirstOrDefault().Status == 0)
                    //    {
                    //        col15.Text = "";
                    //    }
                    //}
                    #endregion
                }
            }
            
        }

        private void lab113_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        public void BindingData()
        {
            col1_1.DataBindings.Add("Text", DataSource, "_Maqd");
            col1_2.DataBindings.Add("Text", DataSource, "_checkX");
            col1_3.DataBindings.Add("Text", DataSource, "_Tendvct");
            col1_4.DataBindings.Add("Text", DataSource, "_Ketqua");
            col1_5.DataBindings.Add("Text", DataSource, "_TSBT");
        }

    }
}
