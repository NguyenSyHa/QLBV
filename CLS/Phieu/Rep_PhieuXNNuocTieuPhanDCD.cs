using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuXNNuocTieuPhanDCD : DevExpress.XtraReports.UI.XtraReport
    {
        List<int> _lIdCLS = new List<int>();
        List<int?> _lMaDV = new List<int?>();
        public Rep_PhieuXNNuocTieuPhanDCD()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "27777" || DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
                SubBand3.Visible = false;
                xrPictureBox1.Image = DungChung.Ham.GetLogo();
                if (DungChung.Bien.MaBV == "24272")
                {
                    xrPictureBox1.Visible = false;
                    xrPictureBox2.Visible = true;
                    xrPictureBox2.Image = DungChung.Ham.GetLogo();
                }
            }
            else
            {
                SubBand1.Visible = true;
                SubBand2.Visible = false;
                SubBand3.Visible = false;
            }

        }
        public Rep_PhieuXNNuocTieuPhanDCD(List<int> _lIdCLS, List<int?> _lMaDV)
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "27777" || DungChung.Bien.MaBV == "24272" || DungChung.Bien.MaBV == "30372")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
                SubBand3.Visible = false;
                xrPictureBox1.Image = DungChung.Ham.GetLogo();
                if(DungChung.Bien.MaBV == "24009")
                {
                    xrPanel1.Visible = true;
                    xrPanel2.Visible = false;
                }
                if (DungChung.Bien.MaBV == "24272")
                {
                    xrPictureBox1.Visible = false;
                    xrPictureBox2.Visible = true;
                    xrPictureBox2.Image = DungChung.Ham.GetLogo();
                }
                if (DungChung.Bien.MaBV == "24272")
                {
                    xrPictureBox1.Visible = false;
                    xrPictureBox3.Visible = true;
                }
            }
            else
            {
                SubBand1.Visible = true;
                SubBand2.Visible = false;
                SubBand3.Visible = false;
            }
            this._lIdCLS = _lIdCLS;
            this._lMaDV = _lMaDV;
        }

        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297")
            {
                xrLabel96.Visible = false;
                xrLabel97.Visible = false;
            }
            
            if (DungChung.Bien.MaBV == "30009")
            {
                colSo.Visible = false;
            }
            if (DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                xrLabel3.Visible = false;
            xrLabel101.Text = xrLabel84.Text = xrLabel17.Text = colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            xrLabel100.Text = xrLabel83.Text = xrLabel18.Text = colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            if (DungChung.Bien.MaBV == "20001")
            {
                SubBand3.Visible = true;
                SubBand2.Visible = false;
                SubBand1.Visible = false;
                xrLine1.Visible = true;
                this.colTenCQCQ.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrLabel84.Font = new Font("Times New Roman", 10f, FontStyle.Regular);
                xrLabel83.Font = new Font("Times New Roman", 10f, FontStyle.Bold);
                this.colTenCQ.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                
            }
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
            {
                colSo.Font = new Font("Times New Roman", 12f, FontStyle.Bold);
                lab3.Font = new Font("Times New Roman", 12f, FontStyle.Bold);
                lblKhoaXN.Text = "KHOA XÉT NGHIỆM";
                lblKhoaXN.Visible = true;
                xrLabel3.Visible = false;
                lab2.Visible = false;
                if (DungChung.Bien.MaBV == "01049")
                {
                    xrLabel4.Visible = true;
                    xrLabel5.Visible = true;
                }
            }
            else
            {
                lblKhoaXN.Visible = false;
                xrLabel3.Visible = true;
            }
            //if(DungChung.Bien.MaBV=="30003")
            //{
            //    xrTable4.Visible = true;
            //    xrPageBreak1.Visible = true;
            //}
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            var qcls = (from dvct in DataContect.DichVucts
                        join dv in DataContect.DichVus on dvct.MaDV equals dv.MaDV
                        join tnhomdv in DataContect.TieuNhomDVs on dv.IdTieuNhom equals tnhomdv.IdTieuNhom
                        where (tnhomdv.TenRG.Contains("chọc dò"))
                        select new { tnhomdv.TenTN, dvct.MaDVct, dvct.TenDVct, dvct.TSBT, dvct.STT, dv.MaDV }).ToList();
            if (qcls.Count > 0)
            {
                if (qcls.Where(p => p.STT == 1).Count() > 0 && qcls.Where(p => p.STT == 1).First().TenDVct != null && qcls.Where(p => p.STT == 1).First().TSBT == null)
                {
                    l1.Text = qcls.Where(p => p.STT == 1).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 1).Count() > 0 && qcls.Where(p => p.STT == 1).First().TenDVct != null && qcls.Where(p => p.STT == 1).First().TSBT != null)
                {
                    l1.Text = qcls.Where(p => p.STT == 1).First().TenDVct.ToString() + qcls.Where(p => p.STT == 1).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 2).Count() > 0 && qcls.Where(p => p.STT == 2).First().TenDVct != null && qcls.Where(p => p.STT == 2).First().TSBT == null)
                {
                    l2.Text = qcls.Where(p => p.STT == 2).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 2).Count() > 0 && qcls.Where(p => p.STT == 2).First().TenDVct != null && qcls.Where(p => p.STT == 2).First().TSBT != null)
                {
                    l2.Text = qcls.Where(p => p.STT == 2).First().TenDVct.ToString() + qcls.Where(p => p.STT == 2).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 3).Count() > 0 && qcls.Where(p => p.STT == 3).First().TenDVct != null && qcls.Where(p => p.STT == 3).First().TSBT == null)
                {
                    l3.Text = qcls.Where(p => p.STT == 3).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 3).Count() > 0 && qcls.Where(p => p.STT == 3).First().TenDVct != null && qcls.Where(p => p.STT == 3).First().TSBT != null)
                {
                    l3.Text = qcls.Where(p => p.STT == 3).First().TenDVct.ToString() + qcls.Where(p => p.STT == 3).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 4).Count() > 0 && qcls.Where(p => p.STT == 4).First().TenDVct != null && qcls.Where(p => p.STT == 4).First().TSBT == null)
                {
                    l4.Text = qcls.Where(p => p.STT == 4).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 4).Count() > 0 && qcls.Where(p => p.STT == 4).First().TenDVct != null && qcls.Where(p => p.STT == 4).First().TSBT != null)
                {
                    l4.Text = qcls.Where(p => p.STT == 4).First().TenDVct.ToString() + qcls.Where(p => p.STT == 4).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 5).Count() > 0 && qcls.Where(p => p.STT == 5).First().TenDVct != null && qcls.Where(p => p.STT == 5).First().TSBT == null)
                {
                    l5.Text = qcls.Where(p => p.STT == 5).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 5).Count() > 0 && qcls.Where(p => p.STT == 5).First().TenDVct != null && qcls.Where(p => p.STT == 5).First().TSBT != null)
                {
                    l5.Text = qcls.Where(p => p.STT == 5).First().TenDVct.ToString() + qcls.Where(p => p.STT == 5).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 6).Count() > 0 && qcls.Where(p => p.STT == 6).First().TenDVct != null && qcls.Where(p => p.STT == 6).First().TSBT == null)
                {
                    l6.Text = qcls.Where(p => p.STT == 6).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 6).Count() > 0 && qcls.Where(p => p.STT == 6).First().TenDVct != null && qcls.Where(p => p.STT == 6).First().TSBT != null)
                {
                    l6.Text = qcls.Where(p => p.STT == 6).First().TenDVct.ToString() + qcls.Where(p => p.STT == 6).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 7).Count() > 0 && qcls.Where(p => p.STT == 7).First().TenDVct != null && qcls.Where(p => p.STT == 7).First().TSBT == null)
                {
                    l7.Text = qcls.Where(p => p.STT == 7).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 7).Count() > 0 && qcls.Where(p => p.STT == 7).First().TenDVct != null && qcls.Where(p => p.STT == 7).First().TSBT != null)
                {
                    l7.Text = qcls.Where(p => p.STT == 7).First().TenDVct.ToString() + qcls.Where(p => p.STT == 7).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 8).Count() > 0 && qcls.Where(p => p.STT == 8).First().TenDVct != null && qcls.Where(p => p.STT == 8).First().TSBT == null)
                {
                    l8.Text = qcls.Where(p => p.STT == 8).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 8).Count() > 0 && qcls.Where(p => p.STT == 8).First().TenDVct != null && qcls.Where(p => p.STT == 8).First().TSBT != null)
                {
                    l8.Text = qcls.Where(p => p.STT == 8).First().TenDVct.ToString() + qcls.Where(p => p.STT == 8).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 9).Count() > 0 && qcls.Where(p => p.STT == 9).First().TenDVct != null && qcls.Where(p => p.STT == 9).First().TSBT == null)
                {
                    l9.Text = qcls.Where(p => p.STT == 9).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 9).Count() > 0 && qcls.Where(p => p.STT == 9).First().TenDVct != null && qcls.Where(p => p.STT == 9).First().TSBT != null)
                {
                    l9.Text = qcls.Where(p => p.STT == 9).First().TenDVct.ToString() + qcls.Where(p => p.STT == 9).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 10).Count() > 0 && qcls.Where(p => p.STT == 10).First().TenDVct != null && qcls.Where(p => p.STT == 10).First().TSBT == null)
                {
                    l10.Text = qcls.Where(p => p.STT == 10).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 10).Count() > 0 && qcls.Where(p => p.STT == 10).First().TenDVct != null && qcls.Where(p => p.STT == 10).First().TSBT != null)
                {
                    l10.Text = qcls.Where(p => p.STT == 10).First().TenDVct.ToString() + qcls.Where(p => p.STT == 10).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 11).Count() > 0 && qcls.Where(p => p.STT == 11).First().TenDVct != null && qcls.Where(p => p.STT == 11).First().TSBT == null)
                {
                    l11.Text = qcls.Where(p => p.STT == 11).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 11).Count() > 0 && qcls.Where(p => p.STT == 11).First().TenDVct != null && qcls.Where(p => p.STT == 11).First().TSBT != null)
                {
                    l11.Text = qcls.Where(p => p.STT == 11).First().TenDVct.ToString() + qcls.Where(p => p.STT == 11).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 12).Count() > 0 && qcls.Where(p => p.STT == 12).First().TenDVct != null && qcls.Where(p => p.STT == 12).First().TSBT == null)
                {
                    l12.Text = qcls.Where(p => p.STT == 12).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 12).Count() > 0 && qcls.Where(p => p.STT == 12).First().TenDVct != null && qcls.Where(p => p.STT == 12).First().TSBT != null)
                {
                    l12.Text = qcls.Where(p => p.STT == 12).First().TenDVct.ToString() + qcls.Where(p => p.STT == 12).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 13).Count() > 0 && qcls.Where(p => p.STT == 13).First().TenDVct != null && qcls.Where(p => p.STT == 13).First().TSBT == null)
                {
                    l13.Text = qcls.Where(p => p.STT == 13).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 13).Count() > 0 && qcls.Where(p => p.STT == 13).First().TenDVct != null && qcls.Where(p => p.STT == 13).First().TSBT != null)
                {
                    l13.Text = qcls.Where(p => p.STT == 13).First().TenDVct.ToString() + qcls.Where(p => p.STT == 13).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 14).Count() > 0 && qcls.Where(p => p.STT == 14).First().TenDVct != null && qcls.Where(p => p.STT == 14).First().TSBT == null)
                {
                    l14.Text = qcls.Where(p => p.STT == 14).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 14).Count() > 0 && qcls.Where(p => p.STT == 14).First().TenDVct != null && qcls.Where(p => p.STT == 14).First().TSBT != null)
                {
                    l14.Text = qcls.Where(p => p.STT == 14).First().TenDVct.ToString() + qcls.Where(p => p.STT == 14).First().TSBT.ToString();
                }
                /////Nuoc tieu 24 gio
                if (qcls.Where(p => p.STT == 15).Count() > 0 && qcls.Where(p => p.STT == 15).First().TenDVct != null && qcls.Where(p => p.STT == 15).First().TSBT == null)
                {
                    l15.Text = qcls.Where(p => p.STT == 15).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 15).Count() > 0 && qcls.Where(p => p.STT == 15).First().TenDVct != null && qcls.Where(p => p.STT == 15).First().TSBT != null)
                {
                    l15.Text = qcls.Where(p => p.STT == 15).First().TenDVct.ToString() + qcls.Where(p => p.STT == 15).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 16).Count() > 0 && qcls.Where(p => p.STT == 16).First().TenDVct != null && qcls.Where(p => p.STT == 16).First().TSBT == null)
                {
                    l16.Text = qcls.Where(p => p.STT == 16).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 16).Count() > 0 && qcls.Where(p => p.STT == 16).First().TenDVct != null && qcls.Where(p => p.STT == 16).First().TSBT != null)
                {
                    l16.Text = qcls.Where(p => p.STT == 16).First().TenDVct.ToString() + qcls.Where(p => p.STT == 16).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 17).Count() > 0 && qcls.Where(p => p.STT == 17).First().TenDVct != null && qcls.Where(p => p.STT == 17).First().TSBT == null)
                {
                    l17.Text = qcls.Where(p => p.STT == 17).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 17).Count() > 0 && qcls.Where(p => p.STT == 17).First().TenDVct != null && qcls.Where(p => p.STT == 17).First().TSBT != null)
                {
                    l17.Text = qcls.Where(p => p.STT == 17).First().TenDVct.ToString() + qcls.Where(p => p.STT == 17).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 18).Count() > 0 && qcls.Where(p => p.STT == 18).First().TenDVct != null && qcls.Where(p => p.STT == 18).First().TSBT == null)
                {
                    l18.Text = qcls.Where(p => p.STT == 18).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 18).Count() > 0 && qcls.Where(p => p.STT == 18).First().TenDVct != null && qcls.Where(p => p.STT == 18).First().TSBT != null)
                {
                    l18.Text = qcls.Where(p => p.STT == 18).First().TenDVct.ToString() + qcls.Where(p => p.STT == 18).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 19).Count() > 0 && qcls.Where(p => p.STT == 19).First().TenDVct != null && qcls.Where(p => p.STT == 19).First().TSBT == null)
                {
                    l19.Text = qcls.Where(p => p.STT == 19).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 19).Count() > 0 && qcls.Where(p => p.STT == 19).First().TenDVct != null && qcls.Where(p => p.STT == 19).First().TSBT != null)
                {
                    l19.Text = qcls.Where(p => p.STT == 19).First().TenDVct.ToString() + qcls.Where(p => p.STT == 19).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 20).Count() > 0 && qcls.Where(p => p.STT == 20).First().TenDVct != null && qcls.Where(p => p.STT == 20).First().TSBT == null)
                {
                    l20.Text = qcls.Where(p => p.STT == 20).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 20).Count() > 0 && qcls.Where(p => p.STT == 20).First().TenDVct != null && qcls.Where(p => p.STT == 20).First().TSBT != null)
                {
                    l20.Text = qcls.Where(p => p.STT == 20).First().TenDVct.ToString() + qcls.Where(p => p.STT == 20).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 21).Count() > 0 && qcls.Where(p => p.STT == 21).First().TenDVct != null && qcls.Where(p => p.STT == 21).First().TSBT == null)
                {
                    l21.Text = qcls.Where(p => p.STT == 21).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 21).Count() > 0 && qcls.Where(p => p.STT == 21).First().TenDVct != null && qcls.Where(p => p.STT == 21).First().TSBT != null)
                {
                    l21.Text = qcls.Where(p => p.STT == 21).First().TenDVct.ToString() + qcls.Where(p => p.STT == 21).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 22).Count() > 0 && qcls.Where(p => p.STT == 22).First().TenDVct != null && qcls.Where(p => p.STT == 22).First().TSBT == null)
                {
                    l22.Text = qcls.Where(p => p.STT == 22).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 22).Count() > 0 && qcls.Where(p => p.STT == 22).First().TenDVct != null && qcls.Where(p => p.STT == 22).First().TSBT != null)
                {
                    l22.Text = qcls.Where(p => p.STT == 22).First().TenDVct.ToString() + qcls.Where(p => p.STT == 22).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 23).Count() > 0 && qcls.Where(p => p.STT == 23).First().TenDVct != null && qcls.Where(p => p.STT == 23).First().TSBT == null)
                {
                    l23.Text = qcls.Where(p => p.STT == 23).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 23).Count() > 0 && qcls.Where(p => p.STT == 23).First().TenDVct != null && qcls.Where(p => p.STT == 23).First().TSBT != null)
                {
                    l23.Text = qcls.Where(p => p.STT == 23).First().TenDVct.ToString() + qcls.Where(p => p.STT == 23).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 24).Count() > 0 && qcls.Where(p => p.STT == 24).First().TenDVct != null && qcls.Where(p => p.STT == 24).First().TSBT == null)
                {
                    l24.Text = qcls.Where(p => p.STT == 24).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 24).Count() > 0 && qcls.Where(p => p.STT == 24).First().TenDVct != null && qcls.Where(p => p.STT == 24).First().TSBT != null)
                {
                    l24.Text = qcls.Where(p => p.STT == 24).First().TenDVct.ToString() + qcls.Where(p => p.STT == 24).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 25).Count() > 0 && qcls.Where(p => p.STT == 25).First().TenDVct != null && qcls.Where(p => p.STT == 25).First().TSBT == null)
                {
                    l25.Text = qcls.Where(p => p.STT == 25).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 25).Count() > 0 && qcls.Where(p => p.STT == 25).First().TenDVct != null && qcls.Where(p => p.STT == 25).First().TSBT != null)
                {
                    l25.Text = qcls.Where(p => p.STT == 25).First().TenDVct.ToString() + qcls.Where(p => p.STT == 25).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 26).Count() > 0 && qcls.Where(p => p.STT == 26).First().TenDVct != null && qcls.Where(p => p.STT == 26).First().TSBT == null)
                {
                    l26.Text = qcls.Where(p => p.STT == 26).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 26).Count() > 0 && qcls.Where(p => p.STT == 26).First().TenDVct != null && qcls.Where(p => p.STT == 26).First().TSBT != null)
                {
                    l26.Text = qcls.Where(p => p.STT == 26).First().TenDVct.ToString() + qcls.Where(p => p.STT == 26).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 27).Count() > 0 && qcls.Where(p => p.STT == 27).First().TenDVct != null && qcls.Where(p => p.STT == 27).First().TSBT == null)
                {
                    l27.Text = qcls.Where(p => p.STT == 27).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 27).Count() > 0 && qcls.Where(p => p.STT == 27).First().TenDVct != null && qcls.Where(p => p.STT == 27).First().TSBT != null)
                {
                    l27.Text = qcls.Where(p => p.STT == 27).First().TenDVct.ToString() + qcls.Where(p => p.STT == 27).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 28).Count() > 0 && qcls.Where(p => p.STT == 28).First().TenDVct != null && qcls.Where(p => p.STT == 28).First().TSBT == null)
                {
                    l28.Text = qcls.Where(p => p.STT == 28).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 28).Count() > 0 && qcls.Where(p => p.STT == 28).First().TenDVct != null && qcls.Where(p => p.STT == 28).First().TSBT != null)
                {
                    l28.Text = qcls.Where(p => p.STT == 28).First().TenDVct.ToString() + qcls.Where(p => p.STT == 28).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 29).Count() > 0 && qcls.Where(p => p.STT == 29).First().TenDVct != null && qcls.Where(p => p.STT == 29).First().TSBT == null)
                {
                    l29.Text = qcls.Where(p => p.STT == 29).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 29).Count() > 0 && qcls.Where(p => p.STT == 29).First().TenDVct != null && qcls.Where(p => p.STT == 29).First().TSBT != null)
                {
                    l29.Text = qcls.Where(p => p.STT == 29).First().TenDVct.ToString() + qcls.Where(p => p.STT == 29).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 30).Count() > 0 && qcls.Where(p => p.STT == 30).First().TenDVct != null && qcls.Where(p => p.STT == 30).First().TSBT == null)
                {
                    l30.Text = qcls.Where(p => p.STT == 30).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 30).Count() > 0 && qcls.Where(p => p.STT == 30).First().TenDVct != null && qcls.Where(p => p.STT == 30).First().TSBT != null)
                {
                    l30.Text = qcls.Where(p => p.STT == 30).First().TenDVct.ToString() + qcls.Where(p => p.STT == 30).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 31).Count() > 0 && qcls.Where(p => p.STT == 31).First().TenDVct != null && qcls.Where(p => p.STT == 31).First().TSBT == null)
                {
                    l31.Text = qcls.Where(p => p.STT == 31).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 31).Count() > 0 && qcls.Where(p => p.STT == 31).First().TenDVct != null && qcls.Where(p => p.STT == 31).First().TSBT != null)
                {
                    l31.Text = qcls.Where(p => p.STT == 31).First().TenDVct.ToString() + qcls.Where(p => p.STT == 31).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 32).Count() > 0 && qcls.Where(p => p.STT == 32).First().TenDVct != null && qcls.Where(p => p.STT == 32).First().TSBT == null)
                {
                    l32.Text = qcls.Where(p => p.STT == 32).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 32).Count() > 0 && qcls.Where(p => p.STT == 32).First().TenDVct != null && qcls.Where(p => p.STT == 32).First().TSBT != null)
                {
                    l32.Text = qcls.Where(p => p.STT == 32).First().TenDVct.ToString() + qcls.Where(p => p.STT == 32).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 33).Count() > 0 && qcls.Where(p => p.STT == 33).First().TenDVct != null && qcls.Where(p => p.STT == 33).First().TSBT == null)
                {
                    l33.Text = qcls.Where(p => p.STT == 33).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 33).Count() > 0 && qcls.Where(p => p.STT == 33).First().TenDVct != null && qcls.Where(p => p.STT == 33).First().TSBT != null)
                {
                    l33.Text = qcls.Where(p => p.STT == 33).First().TenDVct.ToString() + qcls.Where(p => p.STT == 33).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 34).Count() > 0 && qcls.Where(p => p.STT == 34).First().TenDVct != null && qcls.Where(p => p.STT == 34).First().TSBT == null)
                {
                    l34.Text = qcls.Where(p => p.STT == 34).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 34).Count() > 0 && qcls.Where(p => p.STT == 34).First().TenDVct != null && qcls.Where(p => p.STT == 34).First().TSBT != null)
                {
                    l34.Text = qcls.Where(p => p.STT == 34).First().TenDVct.ToString() + qcls.Where(p => p.STT == 34).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 35).Count() > 0 && qcls.Where(p => p.STT == 35).First().TenDVct != null && qcls.Where(p => p.STT == 35).First().TSBT == null)
                {
                    l35.Text = qcls.Where(p => p.STT == 35).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 35).Count() > 0 && qcls.Where(p => p.STT == 35).First().TenDVct != null && qcls.Where(p => p.STT == 35).First().TSBT != null)
                {
                    l35.Text = qcls.Where(p => p.STT == 35).First().TenDVct.ToString() + qcls.Where(p => p.STT == 35).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 36).Count() > 0 && qcls.Where(p => p.STT == 36).First().TenDVct != null && qcls.Where(p => p.STT == 36).First().TSBT == null)
                {
                    l36.Text = qcls.Where(p => p.STT == 36).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 36).Count() > 0 && qcls.Where(p => p.STT == 36).First().TenDVct != null && qcls.Where(p => p.STT == 36).First().TSBT != null)
                {
                    l36.Text = qcls.Where(p => p.STT == 36).First().TenDVct.ToString() + qcls.Where(p => p.STT == 36).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 37).Count() > 0 && qcls.Where(p => p.STT == 37).First().TenDVct != null && qcls.Where(p => p.STT == 37).First().TSBT == null)
                {
                    l37.Text = qcls.Where(p => p.STT == 37).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 37).Count() > 0 && qcls.Where(p => p.STT == 37).First().TenDVct != null && qcls.Where(p => p.STT == 37).First().TSBT != null)
                {
                    l37.Text = qcls.Where(p => p.STT == 37).First().TenDVct.ToString() + qcls.Where(p => p.STT == 37).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 38).Count() > 0 && qcls.Where(p => p.STT == 38).First().TenDVct != null && qcls.Where(p => p.STT == 38).First().TSBT == null)
                {
                    l38.Text = qcls.Where(p => p.STT == 38).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 38).Count() > 0 && qcls.Where(p => p.STT == 38).First().TenDVct != null && qcls.Where(p => p.STT == 38).First().TSBT != null)
                {
                    l38.Text = qcls.Where(p => p.STT == 38).First().TenDVct.ToString() + qcls.Where(p => p.STT == 38).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 39).Count() > 0 && qcls.Where(p => p.STT == 39).First().TenDVct != null && qcls.Where(p => p.STT == 39).First().TSBT == null)
                {
                    l39.Text = qcls.Where(p => p.STT == 39).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 39).Count() > 0 && qcls.Where(p => p.STT == 39).First().TenDVct != null && qcls.Where(p => p.STT == 39).First().TSBT != null)
                {
                    l39.Text = qcls.Where(p => p.STT == 39).First().TenDVct.ToString() + qcls.Where(p => p.STT == 39).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 40).Count() > 0 && qcls.Where(p => p.STT == 40).First().TenDVct != null && qcls.Where(p => p.STT == 40).First().TSBT == null)
                {
                    l40.Text = qcls.Where(p => p.STT == 40).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 40).Count() > 0 && qcls.Where(p => p.STT == 40).First().TenDVct != null && qcls.Where(p => p.STT == 40).First().TSBT != null)
                {
                    l40.Text = qcls.Where(p => p.STT == 40).First().TenDVct.ToString() + qcls.Where(p => p.STT == 40).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 41).Count() > 0 && qcls.Where(p => p.STT == 41).First().TenDVct != null && qcls.Where(p => p.STT == 41).First().TSBT == null)
                {
                    l41.Text = qcls.Where(p => p.STT == 41).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 41).Count() > 0 && qcls.Where(p => p.STT == 41).First().TenDVct != null && qcls.Where(p => p.STT == 41).First().TSBT != null)
                {
                    l41.Text = qcls.Where(p => p.STT == 41).First().TenDVct.ToString() + qcls.Where(p => p.STT == 41).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 42).Count() > 0 && qcls.Where(p => p.STT == 42).First().TenDVct != null && qcls.Where(p => p.STT == 42).First().TSBT == null)
                {
                    l42.Text = qcls.Where(p => p.STT == 42).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 42).Count() > 0 && qcls.Where(p => p.STT == 42).First().TenDVct != null && qcls.Where(p => p.STT == 42).First().TSBT != null)
                {
                    l42.Text = qcls.Where(p => p.STT == 42).First().TenDVct.ToString() + qcls.Where(p => p.STT == 42).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 43).Count() > 0 && qcls.Where(p => p.STT == 43).First().TenDVct != null && qcls.Where(p => p.STT == 43).First().TSBT == null)
                {
                    l43.Text = qcls.Where(p => p.STT == 43).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 43).Count() > 0 && qcls.Where(p => p.STT == 43).First().TenDVct != null && qcls.Where(p => p.STT == 43).First().TSBT != null)
                {
                    l43.Text = qcls.Where(p => p.STT == 43).First().TenDVct.ToString() + qcls.Where(p => p.STT == 43).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 44).Count() > 0 && qcls.Where(p => p.STT == 44).First().TenDVct != null && qcls.Where(p => p.STT == 44).First().TSBT == null)
                {
                    l44.Text = qcls.Where(p => p.STT == 44).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 44).Count() > 0 && qcls.Where(p => p.STT == 44).First().TenDVct != null && qcls.Where(p => p.STT == 44).First().TSBT != null)
                {
                    l44.Text = qcls.Where(p => p.STT == 44).First().TenDVct.ToString() + qcls.Where(p => p.STT == 44).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 45).Count() > 0 && qcls.Where(p => p.STT == 45).First().TenDVct != null && qcls.Where(p => p.STT == 45).First().TSBT == null)
                {
                    l45.Text = qcls.Where(p => p.STT == 45).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 45).Count() > 0 && qcls.Where(p => p.STT == 45).First().TenDVct != null && qcls.Where(p => p.STT == 45).First().TSBT != null)
                {
                    l45.Text = qcls.Where(p => p.STT == 45).First().TenDVct.ToString() + qcls.Where(p => p.STT == 45).First().TSBT.ToString();
                }
            }
            ////////////////
            int sophieu = int.Parse(SoPhieu.Value.ToString());

            var qhh = (from cls in DataContect.CLS.Where(p => p.IdCLS == sophieu)
                       join chidinh in DataContect.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                       join dichvu in DataContect.DichVus on chidinh.MaDV equals dichvu.MaDV
                       join tn in DataContect.TieuNhomDVs on dichvu.IdTieuNhom equals tn.IdTieuNhom
                       join clsct in DataContect.CLScts on chidinh.IDCD equals clsct.IDCD
                       join dvct in DataContect.DichVucts on clsct.MaDVct equals dvct.MaDVct
                       select new { clsct.MaDVct, clsct.KetQua, dvct.STT, clsct.Status, chidinh.MaDV }).ToList();
            if(_lIdCLS != null)
            {
                qhh = (from cls in DataContect.CLS.Where(p => DungChung.Bien.MaBV == "30372" ? _lIdCLS.Contains(p.IdCLS) : p.IdCLS == sophieu)
                       join chidinh in DataContect.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                       join dichvu in DataContect.DichVus on chidinh.MaDV equals dichvu.MaDV
                       join tn in DataContect.TieuNhomDVs on dichvu.IdTieuNhom equals tn.IdTieuNhom
                       join clsct in DataContect.CLScts on chidinh.IDCD equals clsct.IDCD
                       join dvct in DataContect.DichVucts on clsct.MaDVct equals dvct.MaDVct
                       select new { clsct.MaDVct, clsct.KetQua, dvct.STT, clsct.Status, chidinh.MaDV }).ToList();
            }
            if (qhh.Count > 0)
            {
                if (qhh.Where(p => p.STT == 1 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 1).First().MaDVct != null && qhh.Where(p => p.STT == 1).First().Status != -1)
                {
                    a2.Text = "X";
                }
                if (qhh.Where(p => p.STT == 1 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 1).First().KetQua != null)
                {
                    col2.Text = qhh.Where(p => p.STT == 1).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 2 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 2).First().MaDVct != null && qhh.Where(p => p.STT == 2).First().Status != -1)
                {
                    a3.Text = "X";
                }
                if (qhh.Where(p => p.STT == 2 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 2).First().KetQua != null)
                {
                    col3.Text = qhh.Where(p => p.STT == 2).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 3 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 3).First().MaDVct != null && qhh.Where(p => p.STT == 3).First().Status != -1)
                {
                    a4.Text = "X";
                }
                if (qhh.Where(p => p.STT == 3 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 3).First().KetQua != null)
                {
                    col4.Text = qhh.Where(p => p.STT == 3).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 4 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 4).First().MaDVct != null && qhh.Where(p => p.STT == 4).First().Status != -1)
                {
                    a5.Text = "X";
                }
                if (qhh.Where(p => p.STT == 4 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 4).First().KetQua != null)
                {
                    col5.Text = qhh.Where(p => p.STT == 4).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 5 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 5).First().MaDVct != null && qhh.Where(p => p.STT == 5).First().Status != -1)
                {
                    a6.Text = "X";
                }
                if (qhh.Where(p => p.STT == 5 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 5).First().KetQua != null)
                {
                    col6.Text = qhh.Where(p => p.STT == 5).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 6 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 6).First().MaDVct != null && qhh.Where(p => p.STT == 6).First().Status != -1)
                {
                    a7.Text = "X";
                }
                if (qhh.Where(p => p.STT == 6 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 6).First().KetQua != null)
                {
                    col7.Text = qhh.Where(p => p.STT == 6).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 7 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 7).First().MaDVct != null && qhh.Where(p => p.STT == 7).First().Status != -1)
                {
                    a8.Text = "X";
                }
                if (qhh.Where(p => p.STT == 7 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 7).First().KetQua != null)
                {
                    col8.Text = qhh.Where(p => p.STT == 7).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 8 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 8).First().MaDVct != null && qhh.Where(p => p.STT == 8).First().Status != -1)
                {
                    a9.Text = "X";
                }
                if (qhh.Where(p => p.STT == 8 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 8).First().KetQua != null)
                {
                    col9.Text = qhh.Where(p => p.STT == 8).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 9 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 9).First().MaDVct != null && qhh.Where(p => p.STT == 9).First().Status != -1)
                {
                    a10.Text = "X";
                }
                if (qhh.Where(p => p.STT == 9 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 9).First().KetQua != null)
                {
                    col10.Text = qhh.Where(p => p.STT == 9).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 10 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 10).First().MaDVct != null && qhh.Where(p => p.STT == 10).First().Status != -1)
                {
                    a11.Text = "X";
                }
                if (qhh.Where(p => p.STT == 10 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 10).First().KetQua != null)
                {
                    col11.Text = qhh.Where(p => p.STT == 10).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 11 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 11).First().MaDVct != null && qhh.Where(p => p.STT == 11).First().Status != -1)
                {
                    a12.Text = "X";
                }
                if (qhh.Where(p => p.STT == 11 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 11).First().KetQua != null)
                {
                    col12.Text = qhh.Where(p => p.STT == 11).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 12 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 12).First().MaDVct != null && qhh.Where(p => p.STT == 12).First().Status != -1)
                {
                    a13.Text = "X";
                }
                if (qhh.Where(p => p.STT == 12 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 12).First().KetQua != null)
                {
                    col13.Text = qhh.Where(p => p.STT == 12).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 13 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 13).First().MaDVct != null && qhh.Where(p => p.STT == 13).First().Status != -1)
                {
                    a14.Text = "X";
                }
                if (qhh.Where(p => p.STT == 13 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 13).First().KetQua != null)
                {
                    col14.Text = qhh.Where(p => p.STT == 13).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 14 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 14).First().MaDVct != null && qhh.Where(p => p.STT == 14).First().Status != -1)
                {
                    a15.Text = "X";
                }
                if (qhh.Where(p => p.STT == 14 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 14).First().KetQua != null)
                {
                    col15.Text = qhh.Where(p => p.STT == 14).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 15 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 15).First().MaDVct != null && qhh.Where(p => p.STT == 15).First().Status != -1)
                {
                    a17.Text = "X";
                }
                if (qhh.Where(p => p.STT == 15 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 15).First().KetQua != null)
                {
                    col17.Text = qhh.Where(p => p.STT == 15).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 16 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 16).First().MaDVct != null && qhh.Where(p => p.STT == 16).First().Status != -1)
                {
                    a18.Text = "X";
                }
                if (qhh.Where(p => p.STT == 16 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 16).First().KetQua != null)
                {
                    col18.Text = qhh.Where(p => p.STT == 16).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 17 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 17).First().MaDVct != null && qhh.Where(p => p.STT == 17).First().Status != -1)
                {
                    a181.Text = "X";
                }
                if (qhh.Where(p => p.STT == 17 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 17).First().KetQua != null)
                {
                    col181.Text = qhh.Where(p => p.STT == 17).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 18 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 18).First().MaDVct != null && qhh.Where(p => p.STT == 18).First().Status != -1)
                {
                    a182.Text = "X";
                }
                if (qhh.Where(p => p.STT == 18 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 18).First().KetQua != null)
                {
                    col182.Text = qhh.Where(p => p.STT == 18).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 19 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 19).First().MaDVct != null && qhh.Where(p => p.STT == 19).First().Status != -1)
                {
                    a183.Text = "X";
                }
                if (qhh.Where(p => p.STT == 19 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 19).First().KetQua != null)
                {
                    col183.Text = qhh.Where(p => p.STT == 19).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 20 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 20).First().MaDVct != null && qhh.Where(p => p.STT == 20).First().Status != -1)
                {
                    a184.Text = "X";
                }
                if (qhh.Where(p => p.STT == 20 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 20).First().KetQua != null)
                {
                    col184.Text = qhh.Where(p => p.STT == 20).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 21 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 21).First().MaDVct != null && qhh.Where(p => p.STT == 21).First().Status != -1)
                {
                    a19.Text = "X";
                }
                if (qhh.Where(p => p.STT == 21 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 21).First().KetQua != null)
                {
                    col19.Text = qhh.Where(p => p.STT == 21).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 22 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 22).First().MaDVct != null && qhh.Where(p => p.STT == 22).First().Status != -1)
                {
                    a20.Text = "X";
                }
                if (qhh.Where(p => p.STT == 22 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 22).First().KetQua != null)
                {
                    col20.Text = qhh.Where(p => p.STT == 22).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 23 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 23).First().MaDVct != null && qhh.Where(p => p.STT == 23).First().Status != -1)
                {
                    a21.Text = "X";
                }
                if (qhh.Where(p => p.STT == 23 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 23).First().KetQua != null)
                {
                    col21.Text = qhh.Where(p => p.STT == 23).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 24 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 24).First().MaDVct != null && qhh.Where(p => p.STT == 24).First().Status != -1)
                {
                    a22.Text = "X";
                }
                if (qhh.Where(p => p.STT == 24 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 24).First().KetQua != null)
                {
                    col22.Text = qhh.Where(p => p.STT == 24).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 25 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 25).First().MaDVct != null && qhh.Where(p => p.STT == 25).First().Status != -1)
                {
                    a24.Text = "X";
                }
                if (qhh.Where(p => p.STT == 25 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 25).First().KetQua != null)
                {
                    col24.Text = qhh.Where(p => p.STT == 25).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 26 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 26).First().MaDVct != null && qhh.Where(p => p.STT == 26).First().Status != -1)
                {
                    a25.Text = "X";
                }
                if (qhh.Where(p => p.STT == 26 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 26).First().KetQua != null)
                {
                    col25.Text = qhh.Where(p => p.STT == 26).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 27 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 27).First().MaDVct != null && qhh.Where(p => p.STT == 27).First().Status != -1)
                {
                    a26.Text = "X";
                }
                if (qhh.Where(p => p.STT == 27 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 27).First().KetQua != null)
                {
                    col26.Text = qhh.Where(p => p.STT == 27).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 28 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 28).First().MaDVct != null && qhh.Where(p => p.STT == 28).First().Status != -1)
                {
                    a27.Text = "X";
                }
                if (qhh.Where(p => p.STT == 28 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 28).First().KetQua != null)
                {
                    col27.Text = qhh.Where(p => p.STT == 28).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 29 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 29).First().MaDVct != null && qhh.Where(p => p.STT == 29).First().Status != -1)
                {
                    a28.Text = "X";
                }
                if (qhh.Where(p => p.STT == 29 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 29).First().KetQua != null)
                {
                    col28.Text = qhh.Where(p => p.STT == 29).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 30 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 30).First().MaDVct != null && qhh.Where(p => p.STT == 30).First().Status != -1)
                {
                    a29.Text = "X";
                }
                if (qhh.Where(p => p.STT == 30 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 30).First().KetQua != null)
                {
                    col29.Text = qhh.Where(p => p.STT == 30).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 31 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 31).First().MaDVct != null && qhh.Where(p => p.STT == 31).First().Status != -1)
                {
                    a31.Text = "X";
                }
                if (qhh.Where(p => p.STT == 31 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 31).First().KetQua != null)
                {
                    col31.Text = qhh.Where(p => p.STT == 31).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 32 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 32).First().MaDVct != null && qhh.Where(p => p.STT == 32).First().Status != -1)
                {
                    a32.Text = "X";
                }
                if (qhh.Where(p => p.STT == 32 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 32).First().KetQua != null)
                {
                    col32.Text = qhh.Where(p => p.STT == 32).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 33 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 33).First().MaDVct != null && qhh.Where(p => p.STT == 33).First().Status != -1)
                {
                    a33.Text = "X";
                }
                if (qhh.Where(p => p.STT == 33 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 33).First().KetQua != null)
                {
                    col33.Text = qhh.Where(p => p.STT == 33).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 34 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 34).First().MaDVct != null && qhh.Where(p => p.STT == 34).First().Status != -1)
                {
                    a34.Text = "X";
                }
                if (qhh.Where(p => p.STT == 34 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 34).First().KetQua != null)
                {
                    col34.Text = qhh.Where(p => p.STT == 34).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 35 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 35).First().MaDVct != null && qhh.Where(p => p.STT == 35).First().Status != -1)
                {
                    a35.Text = "X";
                }
                if (qhh.Where(p => p.STT == 35 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 35).First().KetQua != null)
                {
                    col35.Text = qhh.Where(p => p.STT == 35).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 36 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 36).First().MaDVct != null && qhh.Where(p => p.STT == 36).First().Status != -1)
                {
                    a36.Text = "X";
                }
                if (qhh.Where(p => p.STT == 36 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 36).First().KetQua != null)
                {
                    col36.Text = qhh.Where(p => p.STT == 36).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 37 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 37).First().MaDVct != null && qhh.Where(p => p.STT == 37).First().Status != -1)
                {
                    a38.Text = "X";
                }
                if (qhh.Where(p => p.STT == 37 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 37).First().KetQua != null)
                {
                    col38.Text = qhh.Where(p => p.STT == 37).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 38 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 38).First().MaDVct != null && qhh.Where(p => p.STT == 38).First().Status != -1)
                {
                    a39.Text = "X";
                }
                if (qhh.Where(p => p.STT == 38 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 38).First().KetQua != null)
                {
                    col39.Text = qhh.Where(p => p.STT == 38).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 39 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 39).First().MaDVct != null && qhh.Where(p => p.STT == 39).First().Status != -1)
                {
                    a40.Text = "X";
                }
                if (qhh.Where(p => p.STT == 39 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 39).First().KetQua != null)
                {
                    col40.Text = qhh.Where(p => p.STT == 39).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 40 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 40).First().MaDVct != null && qhh.Where(p => p.STT == 40).First().Status != -1)
                {
                    a401.Text = "X";
                }
                if (qhh.Where(p => p.STT == 40 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 40).First().KetQua != null)
                {
                    col401.Text = qhh.Where(p => p.STT == 40).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 41 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 41).First().MaDVct != null && qhh.Where(p => p.STT == 41).First().Status != -1)
                {
                    a402.Text = "X";
                }
                if (qhh.Where(p => p.STT == 41 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 41).First().KetQua != null)
                {
                    col402.Text = qhh.Where(p => p.STT == 41).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 42 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 42).First().MaDVct != null && qhh.Where(p => p.STT == 42).First().Status != -1)
                {
                    a403.Text = "X";
                }
                if (qhh.Where(p => p.STT == 42 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 42).First().KetQua != null)
                {
                    col403.Text = qhh.Where(p => p.STT == 42).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 43 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 43).First().MaDVct != null && qhh.Where(p => p.STT == 43).First().Status != -1)
                {
                    a41.Text = "X";

                }
                if (qhh.Where(p => p.STT == 43 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 43).First().KetQua != null)
                {
                    col41.Text = qhh.Where(p => p.STT == 43).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 44 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 44).First().KetQua != null && qhh.Where(p => p.STT == 44).First().Status != -1)
                {
                    a42.Text = "X";

                }
                if (qhh.Where(p => p.STT == 44 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 44).First().MaDVct != null)
                {
                    col42.Text = qhh.Where(p => p.STT == 44).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 45 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 45).First().KetQua != null && qhh.Where(p => p.STT == 45).First().Status != -1)
                {
                    a43.Text = "X";

                }
                if (qhh.Where(p => p.STT == 45 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)).Count() > 0 && qhh.Where(p => p.STT == 45).First().MaDVct != null)
                {
                    col43.Text = qhh.Where(p => p.STT == 45).First().KetQua.ToString();
                }
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {

            //if (DungChung.Bien.MaBV == "30009") {
            //    xrTableBSDT.Visible = false;
            //}
            if (MaCBDT.Value != null)
            {
                //  colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
            }

            if (Macb.Value != null)
            {
                //     colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            }
            if (DungChung.Bien.MaBV == "30004")
            {
                xrTableBSDT.Visible = false;
                colTenBSDT.Visible = false;
                colTenTKXN.Visible = false;
            }
        }

        private void lab113_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30007")
            {
                lab113.Text = "Bác Sỹ Cận Lâm Sàng".ToUpper();
            }
            if (DungChung.Bien.MaBV == "27001")
            {
                lab112.Text = "Y BÁC SỸ";
                lab113.Text = "PHÒNG XÉT NGHIỆM";
            }
            if (DungChung.Bien.MaBV == "20001")
                lab113.Text = "TRƯỞNG KHOA XÉT NGHIỆM";
            if (DungChung.Bien.MaBV == "04012")
                lab113.Text = "Trưởng khoa cận lâm sàng".ToUpper();
            if (DungChung.Bien.MaBV == "27023")
                lab113.Text = "Bác sĩ chuyên khoa".ToUpper();
        }

        private void Rep_PhieuXNNuocTieuPhanDCD_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "12122" || DungChung.Bien.MaBV == "20001")
            {
                IEnumerable allLable = this.AllControls<XRLabel>();
                foreach (XRLabel a in allLable)
                {
                    a.ForeColor = Color.Black;
                    a.BorderColor = Color.Black;
                }
                IEnumerable allCel = this.AllControls<XRTableCell>();
                foreach (XRTableCell a in allCel)
                {
                    a.ForeColor = Color.Black;
                    a.BorderColor = Color.Black;
                }
                xrLine3.ForeColor = Color.Black;
                

            }
            if (DungChung.Bien.MaBV == "12128")
            {
                GroupFooter1.Visible = true;
                ReportFooter.Visible = false;
            }
        }

        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
            rep_PhieuXN_Sub repsub = (rep_PhieuXN_Sub)xrSubreport1.ReportSource;
            repsub.NGAYKIXN.Value = NgayTH.Value.ToString();
            repsub.NGAYKYDT.Value = NgayCD.Value.ToString();
            repsub.BSDT.Value = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
            repsub.TKXN.Value = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            repsub.tb12128.ForeColor = System.Drawing.Color.DodgerBlue;

        }

        private void xrTableCell25_BeforePrint(object sender, CancelEventArgs e)
        {

        }
    }
}
