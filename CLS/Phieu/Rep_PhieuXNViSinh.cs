using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuXNViSinh : DevExpress.XtraReports.UI.XtraReport
    {
        List<int> _lIdCLS = new List<int>();
        List<int?> _lMaDV = new List<int?>();
        public Rep_PhieuXNViSinh()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "27777" || DungChung.Bien.MaBV == "24272")
            {
                if(DungChung.Bien.MaBV == "24009")
                {
                    xrPanel1.Visible = true;
                    xrPanel2.Visible = false;
                }    
                SubBand1.Visible = false;
                SubBand2.Visible = true;
                xrPictureBox2.Image = DungChung.Ham.GetLogo();
                xrLabel60.Text = colDiaChi.Text = colDiaChi2.Text = DungChung.Ham.GetDiaChiBV();
                if (DungChung.Bien.MaBV == "24272")
                {
                    SubBand1.Visible = false;
                    SubBand2.Visible = false;
                    SubBand3.Visible = true;
                    xrPictureBox2.Visible = false;
                    xrPictureBox1.Visible = true;
                    xrPictureBox1.Image = DungChung.Ham.GetLogo();
                }
            }
            else
            {
                SubBand1.Visible = true;
                SubBand2.Visible = false;
            }
        }
        public Rep_PhieuXNViSinh(List<int> _lIdCLS, List<int?> _lMaDV)
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "27777" || DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "24272")
            {
                if (DungChung.Bien.MaBV == "24009")
                {
                    xrPanel1.Visible = true;
                    xrPanel2.Visible = false;
                }
                SubBand1.Visible = false;
                SubBand2.Visible = true;
                xrPictureBox2.Image = xrPictureBox3.Image = DungChung.Ham.GetLogo();
                colDiaChi.Text = colDiaChi2.Text = DungChung.Ham.GetDiaChiBV();
                if (DungChung.Bien.MaBV == "24272")
                {
                    SubBand1.Visible = false;
                    SubBand2.Visible = false;
                    SubBand3.Visible = true;
                    xrPictureBox2.Visible = false;
                    xrPictureBox1.Visible = true;
                    xrPictureBox1.Image = DungChung.Ham.GetLogo();
                }
                if (DungChung.Bien.MaBV == "30372")
                {
                    xrPictureBox2.Visible = false;
                    xrPictureBox3.Visible = true;
                }
            }
            else
            {
                SubBand1.Visible = true;
                SubBand2.Visible = false;
            }
            this._lMaDV = _lMaDV;
            this._lIdCLS = _lIdCLS;
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                xrTable2.Visible = true;
            xrLabel59.Text = xrLabel20.Text = colTenCQCQ.Text = colTenCQCQ2.Text = DungChung.Bien.TenCQCQ.ToUpper();
            xrLabel58.Text = xrLabel21.Text = colTenCQ.Text = colTenCQ2.Text = DungChung.Bien.TenCQ.ToUpper();
            if (DungChung.Bien.MaBV == "01830")
            {
                DetailReport.Visible = false;

            }
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                xrLabel46.Text = lblKhoaXN.Text = lblKhoaXN2.Text = "KHOA XÉT NGHIỆM";
            xrTable5.Visible = true;
            //xrTable3.Visible = false;
            //xrTable7.Visible = false;
            xrTable6.Visible = true;
            if (DungChung.Bien.MaBV == "30012")
            {
                //xrTable5.Visible = true;
                //xrTable3.Visible = true;
                //xrTable7.Visible = true;
                //Detail1.Visible = false;
                xrPageBreak1.Visible = false;
                Detail1.Visible = false;
                //xrTable6.Visible = false;
            }
           
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            #region Hiển thị dịch vụ lên phiếu
            var qcls = (from dvct in DataContect.DichVucts
                        join dv in DataContect.DichVus on dvct.MaDV equals dv.MaDV
                        join tnhomdv in DataContect.TieuNhomDVs on dv.IdTieuNhom equals tnhomdv.IdTieuNhom
                        where (tnhomdv.TenRG == "XN vi sinh")
                        select new { tnhomdv.TenTN, dvct.MaDVct, dvct.TenDVct, dvct.TSBT, dvct.STT }).ToList();
            if (qcls.Count > 0)
            {
                if (qcls.Where(p => p.STT == 1).Count() > 0 && qcls.Where(p => p.STT == 1).First().TenDVct != null && qcls.Where(p => p.STT == 1).First().TSBT == null)
                {
                    c1.Text = qcls.Where(p => p.STT == 1).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 1).Count() > 0 && qcls.Where(p => p.STT == 1).First().TenDVct != null && qcls.Where(p => p.STT == 1).First().TSBT != null)
                {
                    c1.Text = qcls.Where(p => p.STT == 1).First().TenDVct.ToString() + qcls.Where(p => p.STT == 1).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 2).Count() > 0 && qcls.Where(p => p.STT == 2).First().TenDVct != null && qcls.Where(p => p.STT == 2).First().TSBT == null)
                {
                    c2.Text = qcls.Where(p => p.STT == 2).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 2).Count() > 0 && qcls.Where(p => p.STT == 2).First().TenDVct != null && qcls.Where(p => p.STT == 2).First().TSBT != null)
                {
                    c2.Text = qcls.Where(p => p.STT == 2).First().TenDVct.ToString() + qcls.Where(p => p.STT == 2).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 3).Count() > 0 && qcls.Where(p => p.STT == 3).First().TenDVct != null && qcls.Where(p => p.STT == 3).First().TSBT == null)
                {
                    c3.Text = qcls.Where(p => p.STT == 3).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 3).Count() > 0 && qcls.Where(p => p.STT == 3).First().TenDVct != null && qcls.Where(p => p.STT == 3).First().TSBT != null)
                {
                    c3.Text = qcls.Where(p => p.STT == 3).First().TenDVct.ToString() + qcls.Where(p => p.STT == 3).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 4).Count() > 0 && qcls.Where(p => p.STT == 4).First().TenDVct != null && qcls.Where(p => p.STT == 4).First().TSBT == null)
                {
                    c4.Text = qcls.Where(p => p.STT == 4).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 4).Count() > 0 && qcls.Where(p => p.STT == 4).First().TenDVct != null && qcls.Where(p => p.STT == 4).First().TSBT != null)
                {
                    c4.Text = qcls.Where(p => p.STT == 4).First().TenDVct.ToString() + qcls.Where(p => p.STT == 4).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 5).Count() > 0 && qcls.Where(p => p.STT == 5).First().TenDVct != null && qcls.Where(p => p.STT == 5).First().TSBT == null)
                {
                    c5.Text = qcls.Where(p => p.STT == 5).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 5).Count() > 0 && qcls.Where(p => p.STT == 5).First().TenDVct != null && qcls.Where(p => p.STT == 5).First().TSBT != null)
                {
                    c5.Text = qcls.Where(p => p.STT == 5).First().TenDVct.ToString() + qcls.Where(p => p.STT == 5).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 6).Count() > 0 && qcls.Where(p => p.STT == 6).First().TenDVct != null && qcls.Where(p => p.STT == 6).First().TSBT == null)
                {
                    c6.Text = qcls.Where(p => p.STT == 6).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 6).Count() > 0 && qcls.Where(p => p.STT == 6).First().TenDVct != null && qcls.Where(p => p.STT == 6).First().TSBT != null)
                {
                    c6.Text = qcls.Where(p => p.STT == 6).First().TenDVct.ToString() + qcls.Where(p => p.STT == 6).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 7).Count() > 0 && qcls.Where(p => p.STT == 7).First().TenDVct != null && qcls.Where(p => p.STT == 7).First().TSBT == null)
                {
                    c7.Text = qcls.Where(p => p.STT == 7).First().TenDVct.ToString();
                    //c7N.Text = qcls.Where(p => p.STT == 7).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 7).Count() > 0 && qcls.Where(p => p.STT == 7).First().TenDVct != null && qcls.Where(p => p.STT == 7).First().TSBT != null)
                {
                    c7.Text = qcls.Where(p => p.STT == 7).First().TenDVct.ToString() + qcls.Where(p => p.STT == 7).First().TSBT.ToString();
                    //c7N.Text = qcls.Where(p => p.STT == 7).First().TenDVct.ToString() + qcls.Where(p => p.STT == 7).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 8).Count() > 0 && qcls.Where(p => p.STT == 8).First().TenDVct != null && qcls.Where(p => p.STT == 8).First().TSBT == null)
                {
                    c8.Text = qcls.Where(p => p.STT == 8).First().TenDVct.ToString();
                    //c8N.Text = qcls.Where(p => p.STT == 8).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 8).Count() > 0 && qcls.Where(p => p.STT == 8).First().TenDVct != null && qcls.Where(p => p.STT == 8).First().TSBT != null)
                {
                    c8.Text = qcls.Where(p => p.STT == 8).First().TenDVct.ToString() + qcls.Where(p => p.STT == 8).First().TSBT.ToString();
                    //c8N.Text = qcls.Where(p => p.STT == 8).First().TenDVct.ToString() + qcls.Where(p => p.STT == 8).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 9).Count() > 0 && qcls.Where(p => p.STT == 9).First().TenDVct != null && qcls.Where(p => p.STT == 9).First().TSBT == null)
                {
                    k1.Text = qcls.Where(p => p.STT == 9).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 9).Count() > 0 && qcls.Where(p => p.STT == 9).First().TenDVct != null && qcls.Where(p => p.STT == 9).First().TSBT != null)
                {
                    k1.Text = qcls.Where(p => p.STT == 9).First().TenDVct.ToString() + qcls.Where(p => p.STT == 9).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 10).Count() > 0 && qcls.Where(p => p.STT == 10).First().TenDVct != null && qcls.Where(p => p.STT == 10).First().TSBT == null)
                {
                    k2.Text = qcls.Where(p => p.STT == 10).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 10).Count() > 0 && qcls.Where(p => p.STT == 10).First().TenDVct != null && qcls.Where(p => p.STT == 10).First().TSBT != null)
                {
                    k2.Text = qcls.Where(p => p.STT == 10).First().TenDVct.ToString() + qcls.Where(p => p.STT == 10).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 11).Count() > 0 && qcls.Where(p => p.STT == 11).First().TenDVct != null && qcls.Where(p => p.STT == 11).First().TSBT == null)
                {
                    k3.Text = qcls.Where(p => p.STT == 11).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 11).Count() > 0 && qcls.Where(p => p.STT == 11).First().TenDVct != null && qcls.Where(p => p.STT == 11).First().TSBT != null)
                {
                    k3.Text = qcls.Where(p => p.STT == 11).First().TenDVct.ToString() + qcls.Where(p => p.STT == 11).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 12).Count() > 0 && qcls.Where(p => p.STT == 12).First().TenDVct != null && qcls.Where(p => p.STT == 12).First().TSBT == null)
                {
                    k4.Text = qcls.Where(p => p.STT == 12).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 12).Count() > 0 && qcls.Where(p => p.STT == 12).First().TenDVct != null && qcls.Where(p => p.STT == 12).First().TSBT != null)
                {
                    k4.Text = qcls.Where(p => p.STT == 12).First().TenDVct.ToString() + qcls.Where(p => p.STT == 12).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 13).Count() > 0 && qcls.Where(p => p.STT == 13).First().TenDVct != null && qcls.Where(p => p.STT == 13).First().TSBT == null)
                {
                    k5.Text = qcls.Where(p => p.STT == 13).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 13).Count() > 0 && qcls.Where(p => p.STT == 13).First().TenDVct != null && qcls.Where(p => p.STT == 13).First().TSBT != null)
                {
                    k5.Text = qcls.Where(p => p.STT == 13).First().TenDVct.ToString() + qcls.Where(p => p.STT == 13).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 14).Count() > 0 && qcls.Where(p => p.STT == 14).First().TenDVct != null && qcls.Where(p => p.STT == 14).First().TSBT == null)
                {
                    k6.Text = qcls.Where(p => p.STT == 14).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 14).Count() > 0 && qcls.Where(p => p.STT == 14).First().TenDVct != null && qcls.Where(p => p.STT == 14).First().TSBT != null)
                {
                    k6.Text = qcls.Where(p => p.STT == 14).First().TenDVct.ToString() + qcls.Where(p => p.STT == 14).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 15).Count() > 0 && qcls.Where(p => p.STT == 15).First().TenDVct != null && qcls.Where(p => p.STT == 15).First().TSBT == null)
                {
                    k7.Text = qcls.Where(p => p.STT == 15).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 15).Count() > 0 && qcls.Where(p => p.STT == 15).First().TenDVct != null && qcls.Where(p => p.STT == 15).First().TSBT != null)
                {
                    k7.Text = qcls.Where(p => p.STT == 15).First().TenDVct.ToString() + qcls.Where(p => p.STT == 15).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 16).Count() > 0 && qcls.Where(p => p.STT == 16).First().TenDVct != null && qcls.Where(p => p.STT == 16).First().TSBT == null)
                {
                    k8.Text = qcls.Where(p => p.STT == 16).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 16).Count() > 0 && qcls.Where(p => p.STT == 16).First().TenDVct != null && qcls.Where(p => p.STT == 16).First().TSBT != null)
                {
                    k8.Text = qcls.Where(p => p.STT == 16).First().TenDVct.ToString() + qcls.Where(p => p.STT == 16).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 17).Count() > 0 && qcls.Where(p => p.STT == 17).First().TenDVct != null && qcls.Where(p => p.STT == 17).First().TSBT == null)
                {
                    k9.Text = qcls.Where(p => p.STT == 17).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 17).Count() > 0 && qcls.Where(p => p.STT == 17).First().TenDVct != null && qcls.Where(p => p.STT == 17).First().TSBT != null)
                {
                    k9.Text = qcls.Where(p => p.STT == 17).First().TenDVct.ToString() + qcls.Where(p => p.STT == 17).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 18).Count() > 0 && qcls.Where(p => p.STT == 18).First().TenDVct != null && qcls.Where(p => p.STT == 18).First().TSBT == null)
                {
                    k10.Text = qcls.Where(p => p.STT == 18).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 18).Count() > 0 && qcls.Where(p => p.STT == 18).First().TenDVct != null && qcls.Where(p => p.STT == 18).First().TSBT != null)
                {
                    k10.Text = qcls.Where(p => p.STT == 18).First().TenDVct.ToString() + qcls.Where(p => p.STT == 18).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 19).Count() > 0 && qcls.Where(p => p.STT == 19).First().TenDVct != null && qcls.Where(p => p.STT == 19).First().TSBT == null)
                {
                    k11.Text = qcls.Where(p => p.STT == 19).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 19).Count() > 0 && qcls.Where(p => p.STT == 19).First().TenDVct != null && qcls.Where(p => p.STT == 19).First().TSBT != null)
                {
                    k11.Text = qcls.Where(p => p.STT == 19).First().TenDVct.ToString() + qcls.Where(p => p.STT == 19).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 20).Count() > 0 && qcls.Where(p => p.STT == 20).First().TenDVct != null && qcls.Where(p => p.STT == 20).First().TSBT == null)
                {
                    k12.Text = qcls.Where(p => p.STT == 20).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 20).Count() > 0 && qcls.Where(p => p.STT == 20).First().TenDVct != null && qcls.Where(p => p.STT == 20).First().TSBT != null)
                {
                    k12.Text = qcls.Where(p => p.STT == 20).First().TenDVct.ToString() + qcls.Where(p => p.STT == 20).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 21).Count() > 0 && qcls.Where(p => p.STT == 21).First().TenDVct != null && qcls.Where(p => p.STT == 21).First().TSBT == null)
                {
                    k13.Text = qcls.Where(p => p.STT == 21).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 21).Count() > 0 && qcls.Where(p => p.STT == 21).First().TenDVct != null && qcls.Where(p => p.STT == 21).First().TSBT != null)
                {
                    k13.Text = qcls.Where(p => p.STT == 21).First().TenDVct.ToString() + qcls.Where(p => p.STT == 21).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 22).Count() > 0 && qcls.Where(p => p.STT == 22).First().TenDVct != null && qcls.Where(p => p.STT == 22).First().TSBT == null)
                {
                    k14.Text = qcls.Where(p => p.STT == 22).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 22).Count() > 0 && qcls.Where(p => p.STT == 22).First().TenDVct != null && qcls.Where(p => p.STT == 22).First().TSBT != null)
                {
                    k14.Text = qcls.Where(p => p.STT == 22).First().TenDVct.ToString() + qcls.Where(p => p.STT == 22).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 23).Count() > 0 && qcls.Where(p => p.STT == 23).First().TenDVct != null && qcls.Where(p => p.STT == 23).First().TSBT == null)
                {
                    k15.Text = qcls.Where(p => p.STT == 23).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 23).Count() > 0 && qcls.Where(p => p.STT == 23).First().TenDVct != null && qcls.Where(p => p.STT == 23).First().TSBT != null)
                {
                    k15.Text = qcls.Where(p => p.STT == 23).First().TenDVct.ToString() + qcls.Where(p => p.STT == 23).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 24).Count() > 0 && qcls.Where(p => p.STT == 24).First().TenDVct != null && qcls.Where(p => p.STT == 24).First().TSBT == null)
                {
                    k16.Text = qcls.Where(p => p.STT == 24).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 24).Count() > 0 && qcls.Where(p => p.STT == 24).First().TenDVct != null && qcls.Where(p => p.STT == 24).First().TSBT != null)
                {
                    k16.Text = qcls.Where(p => p.STT == 24).First().TenDVct.ToString() + qcls.Where(p => p.STT == 24).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 25).Count() > 0 && qcls.Where(p => p.STT == 25).First().TenDVct != null && qcls.Where(p => p.STT == 25).First().TSBT == null)
                {
                    k17.Text = qcls.Where(p => p.STT == 25).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 25).Count() > 0 && qcls.Where(p => p.STT == 25).First().TenDVct != null && qcls.Where(p => p.STT == 25).First().TSBT != null)
                {
                    k17.Text = qcls.Where(p => p.STT == 25).First().TenDVct.ToString() + qcls.Where(p => p.STT == 25).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 26).Count() > 0 && qcls.Where(p => p.STT == 26).First().TenDVct != null && qcls.Where(p => p.STT == 26).First().TSBT == null)
                {
                    k18.Text = qcls.Where(p => p.STT == 26).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 26).Count() > 0 && qcls.Where(p => p.STT == 26).First().TenDVct != null && qcls.Where(p => p.STT == 26).First().TSBT != null)
                {
                    k18.Text = qcls.Where(p => p.STT == 26).First().TenDVct.ToString() + qcls.Where(p => p.STT == 26).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 27).Count() > 0 && qcls.Where(p => p.STT == 27).First().TenDVct != null && qcls.Where(p => p.STT == 27).First().TSBT == null)
                {
                    k19.Text = qcls.Where(p => p.STT == 27).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 27).Count() > 0 && qcls.Where(p => p.STT == 27).First().TenDVct != null && qcls.Where(p => p.STT == 27).First().TSBT != null)
                {
                    k19.Text = qcls.Where(p => p.STT == 27).First().TenDVct.ToString() + qcls.Where(p => p.STT == 27).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 28).Count() > 0 && qcls.Where(p => p.STT == 28).First().TenDVct != null && qcls.Where(p => p.STT == 28).First().TSBT == null)
                {
                    k20.Text = qcls.Where(p => p.STT == 28).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 28).Count() > 0 && qcls.Where(p => p.STT == 28).First().TenDVct != null && qcls.Where(p => p.STT == 28).First().TSBT != null)
                {
                    k20.Text = qcls.Where(p => p.STT == 28).First().TenDVct.ToString() + qcls.Where(p => p.STT == 28).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 29).Count() > 0 && qcls.Where(p => p.STT == 29).First().TenDVct != null && qcls.Where(p => p.STT == 29).First().TSBT == null)
                {
                    k21.Text = qcls.Where(p => p.STT == 29).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 29).Count() > 0 && qcls.Where(p => p.STT == 29).First().TenDVct != null && qcls.Where(p => p.STT == 29).First().TSBT != null)
                {
                    k21.Text = qcls.Where(p => p.STT == 29).First().TenDVct.ToString() + qcls.Where(p => p.STT == 29).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 30).Count() > 0 && qcls.Where(p => p.STT == 30).First().TenDVct != null && qcls.Where(p => p.STT == 30).First().TSBT == null)
                {
                    k22.Text = qcls.Where(p => p.STT == 30).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 30).Count() > 0 && qcls.Where(p => p.STT == 30).First().TenDVct != null && qcls.Where(p => p.STT == 30).First().TSBT != null)
                {
                    k22.Text = qcls.Where(p => p.STT == 30).First().TenDVct.ToString() + qcls.Where(p => p.STT == 30).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 31).Count() > 0 && qcls.Where(p => p.STT == 31).First().TenDVct != null && qcls.Where(p => p.STT == 31).First().TSBT == null)
                {
                    k23.Text = qcls.Where(p => p.STT == 31).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 31).Count() > 0 && qcls.Where(p => p.STT == 31).First().TenDVct != null && qcls.Where(p => p.STT == 31).First().TSBT != null)
                {
                    k23.Text = qcls.Where(p => p.STT == 31).First().TenDVct.ToString() + qcls.Where(p => p.STT == 31).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 32).Count() > 0 && qcls.Where(p => p.STT == 32).First().TenDVct != null && qcls.Where(p => p.STT == 32).First().TSBT == null)
                {
                    k24.Text = qcls.Where(p => p.STT == 32).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 32).Count() > 0 && qcls.Where(p => p.STT == 32).First().TenDVct != null && qcls.Where(p => p.STT == 32).First().TSBT != null)
                {
                    k24.Text = qcls.Where(p => p.STT == 32).First().TenDVct.ToString() + qcls.Where(p => p.STT == 32).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 33).Count() > 0 && qcls.Where(p => p.STT == 33).First().TenDVct != null && qcls.Where(p => p.STT == 33).First().TSBT == null)
                {
                    k25.Text = qcls.Where(p => p.STT == 33).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 33).Count() > 0 && qcls.Where(p => p.STT == 33).First().TenDVct != null && qcls.Where(p => p.STT == 33).First().TSBT != null)
                {
                    k25.Text = qcls.Where(p => p.STT == 33).First().TenDVct.ToString() + qcls.Where(p => p.STT == 33).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 34).Count() > 0 && qcls.Where(p => p.STT == 34).First().TenDVct != null && qcls.Where(p => p.STT == 34).First().TSBT == null)
                {
                    k26.Text = qcls.Where(p => p.STT == 34).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 34).Count() > 0 && qcls.Where(p => p.STT == 34).First().TenDVct != null && qcls.Where(p => p.STT == 34).First().TSBT != null)
                {
                    k26.Text = qcls.Where(p => p.STT == 34).First().TenDVct.ToString() + qcls.Where(p => p.STT == 34).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 35).Count() > 0 && qcls.Where(p => p.STT == 35).First().TenDVct != null && qcls.Where(p => p.STT == 35).First().TSBT == null)
                {
                    k27.Text = qcls.Where(p => p.STT == 35).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 35).Count() > 0 && qcls.Where(p => p.STT == 35).First().TenDVct != null && qcls.Where(p => p.STT == 35).First().TSBT != null)
                {
                    k27.Text = qcls.Where(p => p.STT == 35).First().TenDVct.ToString() + qcls.Where(p => p.STT == 35).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 36).Count() > 0 && qcls.Where(p => p.STT == 36).First().TenDVct != null && qcls.Where(p => p.STT == 36).First().TSBT == null)
                {
                    k28.Text = qcls.Where(p => p.STT == 36).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 36).Count() > 0 && qcls.Where(p => p.STT == 36).First().TenDVct != null && qcls.Where(p => p.STT == 36).First().TSBT != null)
                {
                    k28.Text = qcls.Where(p => p.STT == 36).First().TenDVct.ToString() + qcls.Where(p => p.STT == 36).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 37).Count() > 0 && qcls.Where(p => p.STT == 37).First().TenDVct != null && qcls.Where(p => p.STT == 37).First().TSBT == null)
                {
                    k29.Text = qcls.Where(p => p.STT == 37).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 37).Count() > 0 && qcls.Where(p => p.STT == 37).First().TenDVct != null && qcls.Where(p => p.STT == 37).First().TSBT != null)
                {
                    k29.Text = qcls.Where(p => p.STT == 37).First().TenDVct.ToString() + qcls.Where(p => p.STT == 37).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 38).Count() > 0 && qcls.Where(p => p.STT == 38).First().TenDVct != null && qcls.Where(p => p.STT == 38).First().TSBT == null)
                {
                    k30.Text = qcls.Where(p => p.STT == 38).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 38).Count() > 0 && qcls.Where(p => p.STT == 38).First().TenDVct != null && qcls.Where(p => p.STT == 38).First().TSBT != null)
                {
                    k30.Text = qcls.Where(p => p.STT == 38).First().TenDVct.ToString() + qcls.Where(p => p.STT == 38).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 39).Count() > 0 && qcls.Where(p => p.STT == 39).First().TenDVct != null && qcls.Where(p => p.STT == 39).First().TSBT == null)
                {
                    k31.Text = qcls.Where(p => p.STT == 39).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 39).Count() > 0 && qcls.Where(p => p.STT == 39).First().TenDVct != null && qcls.Where(p => p.STT == 39).First().TSBT != null)
                {
                    k31.Text = qcls.Where(p => p.STT == 39).First().TenDVct.ToString() + qcls.Where(p => p.STT == 39).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 40).Count() > 0 && qcls.Where(p => p.STT == 40).First().TenDVct != null && qcls.Where(p => p.STT == 40).First().TSBT == null)
                {
                    k32.Text = qcls.Where(p => p.STT == 40).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 40).Count() > 0 && qcls.Where(p => p.STT == 40).First().TenDVct != null && qcls.Where(p => p.STT == 40).First().TSBT != null)
                {
                    k32.Text = qcls.Where(p => p.STT == 40).First().TenDVct.ToString() + qcls.Where(p => p.STT == 40).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 41).Count() > 0 && qcls.Where(p => p.STT == 41).First().TenDVct != null && qcls.Where(p => p.STT == 41).First().TSBT == null)
                {
                    k33.Text = qcls.Where(p => p.STT == 41).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 41).Count() > 0 && qcls.Where(p => p.STT == 41).First().TenDVct != null && qcls.Where(p => p.STT == 41).First().TSBT != null)
                {
                    k33.Text = qcls.Where(p => p.STT == 41).First().TenDVct.ToString() + qcls.Where(p => p.STT == 41).First().TSBT.ToString();
                }
                if (qcls.Where(p => p.STT == 42).Count() > 0 && qcls.Where(p => p.STT == 42).First().TenDVct != null && qcls.Where(p => p.STT == 42).First().TSBT == null)
                {
                    k34.Text = qcls.Where(p => p.STT == 42).First().TenDVct.ToString();
                }
                else if (qcls.Where(p => p.STT == 42).Count() > 0 && qcls.Where(p => p.STT == 42).First().TenDVct != null && qcls.Where(p => p.STT == 42).First().TSBT != null)
                {
                    k34.Text = qcls.Where(p => p.STT == 42).First().TenDVct.ToString() + qcls.Where(p => p.STT == 42).First().TSBT.ToString();
                }

            }
            #endregion
            #region Lấy kết quả
            int sophieu = int.Parse(SoPhieu.Value.ToString());

            var qhh = (from cls in DataContect.CLS.Where(p => p.IdCLS == sophieu)
                       join chidinh in DataContect.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                       join dichvu in DataContect.DichVus on chidinh.MaDV equals dichvu.MaDV
                       join tn in DataContect.TieuNhomDVs.Where(p => p.TenRG == "XN vi sinh") on dichvu.IdTieuNhom equals tn.IdTieuNhom
                       join clsct in DataContect.CLScts on chidinh.IDCD equals clsct.IDCD
                       join dvct in DataContect.DichVucts on clsct.MaDVct equals dvct.MaDVct
                       select new { clsct.MaDVct, clsct.KetQua, dvct.STT }).ToList();
            if(_lIdCLS != null && _lMaDV != null)
            {
                qhh = (from cls in DataContect.CLS.Where(p => DungChung.Bien.MaBV == "30372" ? _lIdCLS.Contains(p.IdCLS) : p.IdCLS == sophieu)
                       join chidinh in DataContect.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                       join dichvu in DataContect.DichVus on chidinh.MaDV equals dichvu.MaDV
                       join tn in DataContect.TieuNhomDVs.Where(p => p.TenRG == "XN vi sinh") on dichvu.IdTieuNhom equals tn.IdTieuNhom
                       join clsct in DataContect.CLScts on chidinh.IDCD equals clsct.IDCD
                       join dvct in DataContect.DichVucts on clsct.MaDVct equals dvct.MaDVct
                       where DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(chidinh.MaDV) : true
                       select new { clsct.MaDVct, clsct.KetQua, dvct.STT }).ToList();
            }
            if (qhh.Count > 0)
            {
                if (qhh.Where(p => p.STT == 1).Count() > 0 && qhh.Where(p => p.STT == 1).First().MaDVct != null && qhh.Where(p => p.STT == 1).First().KetQua != null)
                {
                    q1.Text = qhh.Where(p => p.STT == 1).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 2).Count() > 0 && qhh.Where(p => p.STT == 2).First().MaDVct != null && qhh.Where(p => p.STT == 2).First().KetQua != null)
                {
                    q2.Text = qhh.Where(p => p.STT == 2).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 3).Count() > 0 && qhh.Where(p => p.STT == 3).First().MaDVct != null && qhh.Where(p => p.STT == 3).First().KetQua != null)
                {
                    q3.Text = qhh.Where(p => p.STT == 3).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 4).Count() > 0 && qhh.Where(p => p.STT == 4).First().MaDVct != null && qhh.Where(p => p.STT == 4).First().KetQua != null)
                {
                    q4.Text = qhh.Where(p => p.STT == 4).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 5).Count() > 0 && qhh.Where(p => p.STT == 5).First().MaDVct != null && qhh.Where(p => p.STT == 5).First().KetQua != null)
                {
                    q5.Text = qhh.Where(p => p.STT == 5).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 6).Count() > 0 && qhh.Where(p => p.STT == 6).First().MaDVct != null && qhh.Where(p => p.STT == 6).First().KetQua != null)
                {
                    q6.Text = qhh.Where(p => p.STT == 6).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 7).Count() > 0 && qhh.Where(p => p.STT == 7).First().MaDVct != null && qhh.Where(p => p.STT == 7).First().KetQua != null)
                {
                    q7.Text = qhh.Where(p => p.STT == 7).First().KetQua.ToString();
                    //q7N.Text = qhh.Where(p => p.STT == 7).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 8).Count() > 0 && qhh.Where(p => p.STT == 8).First().MaDVct != null && qhh.Where(p => p.STT == 8).First().KetQua != null)
                {
                    q8.Text = qhh.Where(p => p.STT == 8).First().KetQua.ToString();
                    //q8N.Text = qhh.Where(p => p.STT == 8).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 9).Count() > 0 && qhh.Where(p => p.STT == 9).First().MaDVct != null && qhh.Where(p => p.STT == 9).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 9).First().KetQua == "s" || qhh.Where(p => p.STT == 9).First().KetQua == "S")
                    { s1.Text = "X"; }
                    if (qhh.Where(p => p.STT == 9).First().KetQua == "i" || qhh.Where(p => p.STT == 9).First().KetQua == "I")
                    { i1.Text = "X"; }
                    if (qhh.Where(p => p.STT == 9).First().KetQua == "r" || qhh.Where(p => p.STT == 9).First().KetQua == "R")
                    { r1.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 10).Count() > 0 && qhh.Where(p => p.STT == 10).First().MaDVct != null && qhh.Where(p => p.STT == 10).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 10).First().KetQua == "s" || qhh.Where(p => p.STT == 10).First().KetQua == "S")
                    { s2.Text = "X"; }
                    if (qhh.Where(p => p.STT == 10).First().KetQua == "i" || qhh.Where(p => p.STT == 10).First().KetQua == "I")
                    { i2.Text = "X"; }
                    if (qhh.Where(p => p.STT == 10).First().KetQua == "r" || qhh.Where(p => p.STT == 10).First().KetQua == "R")
                    { r2.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 11).Count() > 0 && qhh.Where(p => p.STT == 11).First().MaDVct != null && qhh.Where(p => p.STT == 11).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 11).First().KetQua == "s" || qhh.Where(p => p.STT == 11).First().KetQua == "S")
                    { s3.Text = "X"; }
                    if (qhh.Where(p => p.STT == 11).First().KetQua == "i" || qhh.Where(p => p.STT == 11).First().KetQua == "I")
                    { i3.Text = "X"; }
                    if (qhh.Where(p => p.STT == 11).First().KetQua == "r" || qhh.Where(p => p.STT == 11).First().KetQua == "R")
                    { r3.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 12).Count() > 0 && qhh.Where(p => p.STT == 12).First().MaDVct != null && qhh.Where(p => p.STT == 12).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 12).First().KetQua == "s" || qhh.Where(p => p.STT == 12).First().KetQua == "S")
                    { s4.Text = "X"; }
                    if (qhh.Where(p => p.STT == 12).First().KetQua == "i" || qhh.Where(p => p.STT == 12).First().KetQua == "I")
                    { i4.Text = "X"; }
                    if (qhh.Where(p => p.STT == 12).First().KetQua == "r" || qhh.Where(p => p.STT == 12).First().KetQua == "R")
                    { r4.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 13).Count() > 0 && qhh.Where(p => p.STT == 13).First().MaDVct != null && qhh.Where(p => p.STT == 13).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 13).First().KetQua == "s" || qhh.Where(p => p.STT == 13).First().KetQua == "S")
                    { s5.Text = "X"; }
                    if (qhh.Where(p => p.STT == 13).First().KetQua == "i" || qhh.Where(p => p.STT == 13).First().KetQua == "I")
                    { i5.Text = "X"; }
                    if (qhh.Where(p => p.STT == 13).First().KetQua == "r" || qhh.Where(p => p.STT == 13).First().KetQua == "R")
                    { r5.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 14).Count() > 0 && qhh.Where(p => p.STT == 14).First().MaDVct != null && qhh.Where(p => p.STT == 14).FirstOrDefault().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 14).First().KetQua == "s" || qhh.Where(p => p.STT == 14).First().KetQua == "S")
                    { s6.Text = "X"; }
                    if (qhh.Where(p => p.STT == 14).First().KetQua == "i" || qhh.Where(p => p.STT == 14).First().KetQua == "I")
                    { i6.Text = "X"; }
                    if (qhh.Where(p => p.STT == 14).First().KetQua == "r" || qhh.Where(p => p.STT == 14).First().KetQua == "R")
                    { r6.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 15).Count() > 0 && qhh.Where(p => p.STT == 15).First().MaDVct != null && qhh.Where(p => p.STT == 15).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 15).First().KetQua == "s" || qhh.Where(p => p.STT == 15).First().KetQua == "S")
                    { s7.Text = "X"; }
                    if (qhh.Where(p => p.STT == 15).First().KetQua == "i" || qhh.Where(p => p.STT == 15).First().KetQua == "I")
                    { i7.Text = "X"; }
                    if (qhh.Where(p => p.STT == 15).First().KetQua == "r" || qhh.Where(p => p.STT == 15).First().KetQua == "R")
                    { r7.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 16).Count() > 0 && qhh.Where(p => p.STT == 16).First().MaDVct != null && qhh.Where(p => p.STT == 16).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 16).First().KetQua == "s" || qhh.Where(p => p.STT == 16).First().KetQua == "S")
                    { s8.Text = "X"; }
                    if (qhh.Where(p => p.STT == 16).First().KetQua == "i" || qhh.Where(p => p.STT == 16).First().KetQua == "I")
                    { i8.Text = "X"; }
                    if (qhh.Where(p => p.STT == 16).First().KetQua == "r" || qhh.Where(p => p.STT == 16).First().KetQua == "R")
                    { r8.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 17).Count() > 0 && qhh.Where(p => p.STT == 17).First().MaDVct != null && qhh.Where(p => p.STT == 17).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 17).First().KetQua == "s" || qhh.Where(p => p.STT == 17).First().KetQua == "S")
                    { s9.Text = "X"; }
                    if (qhh.Where(p => p.STT == 17).First().KetQua == "i" || qhh.Where(p => p.STT == 17).First().KetQua == "I")
                    { i9.Text = "X"; }
                    if (qhh.Where(p => p.STT == 17).First().KetQua == "r" || qhh.Where(p => p.STT == 17).First().KetQua == "R")
                    { r9.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 18).Count() > 0 && qhh.Where(p => p.STT == 18).First().MaDVct != null && qhh.Where(p => p.STT == 18).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 18).First().KetQua == "s" || qhh.Where(p => p.STT == 18).First().KetQua == "S")
                    { s10.Text = "X"; }
                    if (qhh.Where(p => p.STT == 18).First().KetQua == "i" || qhh.Where(p => p.STT == 18).First().KetQua == "I")
                    { i10.Text = "X"; }
                    if (qhh.Where(p => p.STT == 18).First().KetQua == "r" || qhh.Where(p => p.STT == 18).First().KetQua == "R")
                    { r10.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 19).Count() > 0 && qhh.Where(p => p.STT == 19).First().MaDVct != null && qhh.Where(p => p.STT == 19).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 19).First().KetQua == "s" || qhh.Where(p => p.STT == 19).First().KetQua == "S")
                    { s11.Text = "X"; }
                    if (qhh.Where(p => p.STT == 19).First().KetQua == "i" || qhh.Where(p => p.STT == 19).First().KetQua == "I")
                    { i11.Text = "X"; }
                    if (qhh.Where(p => p.STT == 19).First().KetQua == "r" || qhh.Where(p => p.STT == 19).First().KetQua == "R")
                    { r11.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 20).Count() > 0 && qhh.Where(p => p.STT == 20).First().MaDVct != null && qhh.Where(p => p.STT == 20).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 20).First().KetQua == "s" || qhh.Where(p => p.STT == 20).First().KetQua == "S")
                    { s12.Text = "X"; }
                    if (qhh.Where(p => p.STT == 20).First().KetQua == "i" || qhh.Where(p => p.STT == 20).First().KetQua == "I")
                    { i12.Text = "X"; }
                    if (qhh.Where(p => p.STT == 20).First().KetQua == "r" || qhh.Where(p => p.STT == 20).First().KetQua == "R")
                    { r12.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 21).Count() > 0 && qhh.Where(p => p.STT == 21).First().MaDVct != null && qhh.Where(p => p.STT == 21).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 21).First().KetQua == "s" || qhh.Where(p => p.STT == 21).First().KetQua == "S")
                    { s13.Text = "X"; }
                    if (qhh.Where(p => p.STT == 21).First().KetQua == "i" || qhh.Where(p => p.STT == 21).First().KetQua == "I")
                    { i13.Text = "X"; }
                    if (qhh.Where(p => p.STT == 21).First().KetQua == "r" || qhh.Where(p => p.STT == 21).First().KetQua == "R")
                    { r13.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 22).Count() > 0 && qhh.Where(p => p.STT == 22).First().MaDVct != null && qhh.Where(p => p.STT == 22).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 22).First().KetQua == "s" || qhh.Where(p => p.STT == 22).First().KetQua == "S")
                    { s14.Text = "X"; }
                    if (qhh.Where(p => p.STT == 22).First().KetQua == "i" || qhh.Where(p => p.STT == 22).First().KetQua == "I")
                    { i14.Text = "X"; }
                    if (qhh.Where(p => p.STT == 22).First().KetQua == "r" || qhh.Where(p => p.STT == 22).First().KetQua == "R")
                    { r14.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 23).Count() > 0 && qhh.Where(p => p.STT == 23).First().MaDVct != null && qhh.Where(p => p.STT == 23).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 23).First().KetQua == "s" || qhh.Where(p => p.STT == 23).First().KetQua == "S")
                    { s15.Text = "X"; }
                    if (qhh.Where(p => p.STT == 23).First().KetQua == "i" || qhh.Where(p => p.STT == 23).First().KetQua == "I")
                    { i15.Text = "X"; }
                    if (qhh.Where(p => p.STT == 23).First().KetQua == "r" || qhh.Where(p => p.STT == 23).First().KetQua == "R")
                    { r15.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 24).Count() > 0 && qhh.Where(p => p.STT == 24).First().MaDVct != null && qhh.Where(p => p.STT == 24).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 24).First().KetQua == "s" || qhh.Where(p => p.STT == 24).First().KetQua == "S")
                    { s16.Text = "X"; }
                    if (qhh.Where(p => p.STT == 24).First().KetQua == "i" || qhh.Where(p => p.STT == 24).First().KetQua == "I")
                    { i16.Text = "X"; }
                    if (qhh.Where(p => p.STT == 24).First().KetQua == "r" || qhh.Where(p => p.STT == 24).First().KetQua == "R")
                    { r16.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 25).Count() > 0 && qhh.Where(p => p.STT == 25).First().MaDVct != null && qhh.Where(p => p.STT == 25).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 25).First().KetQua == "s" || qhh.Where(p => p.STT == 25).First().KetQua == "S")
                    { s16.Text = "X"; }
                    if (qhh.Where(p => p.STT == 25).First().KetQua == "i" || qhh.Where(p => p.STT == 25).First().KetQua == "I")
                    { i16.Text = "X"; }
                    if (qhh.Where(p => p.STT == 25).First().KetQua == "r" || qhh.Where(p => p.STT == 25).First().KetQua == "R")
                    { r16.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 26).Count() > 0 && qhh.Where(p => p.STT == 26).First().MaDVct != null && qhh.Where(p => p.STT == 26).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 26).First().KetQua == "s" || qhh.Where(p => p.STT == 26).First().KetQua == "S")
                    { s17.Text = "X"; }
                    if (qhh.Where(p => p.STT == 26).First().KetQua == "i" || qhh.Where(p => p.STT == 26).First().KetQua == "I")
                    { i17.Text = "X"; }
                    if (qhh.Where(p => p.STT == 26).First().KetQua == "r" || qhh.Where(p => p.STT == 26).First().KetQua == "R")
                    { r17.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 27).Count() > 0 && qhh.Where(p => p.STT == 27).First().MaDVct != null && qhh.Where(p => p.STT == 27).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 27).First().KetQua == "s" || qhh.Where(p => p.STT == 27).First().KetQua == "S")
                    { s18.Text = "X"; }
                    if (qhh.Where(p => p.STT == 27).First().KetQua == "i" || qhh.Where(p => p.STT == 27).First().KetQua == "I")
                    { i18.Text = "X"; }
                    if (qhh.Where(p => p.STT == 27).First().KetQua == "r" || qhh.Where(p => p.STT == 27).First().KetQua == "R")
                    { r18.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 28).Count() > 0 && qhh.Where(p => p.STT == 28).First().MaDVct != null && qhh.Where(p => p.STT == 28).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 28).First().KetQua == "s" || qhh.Where(p => p.STT == 28).First().KetQua == "S")
                    { s19.Text = "X"; }
                    if (qhh.Where(p => p.STT == 28).First().KetQua == "i" || qhh.Where(p => p.STT == 28).First().KetQua == "I")
                    { i19.Text = "X"; }
                    if (qhh.Where(p => p.STT == 28).First().KetQua == "r" || qhh.Where(p => p.STT == 28).First().KetQua == "R")
                    { r19.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 29).Count() > 0 && qhh.Where(p => p.STT == 29).First().MaDVct != null && qhh.Where(p => p.STT == 29).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 29).First().KetQua == "s" || qhh.Where(p => p.STT == 29).First().KetQua == "S")
                    { s20.Text = "X"; }
                    if (qhh.Where(p => p.STT == 29).First().KetQua == "i" || qhh.Where(p => p.STT == 29).First().KetQua == "I")
                    { i20.Text = "X"; }
                    if (qhh.Where(p => p.STT == 29).First().KetQua == "r" || qhh.Where(p => p.STT == 29).First().KetQua == "R")
                    { r20.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 30).Count() > 0 && qhh.Where(p => p.STT == 30).First().MaDVct != null && qhh.Where(p => p.STT == 30).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 30).First().KetQua == "s" || qhh.Where(p => p.STT == 30).First().KetQua == "S")
                    { s21.Text = "X"; }
                    if (qhh.Where(p => p.STT == 30).First().KetQua == "i" || qhh.Where(p => p.STT == 30).First().KetQua == "I")
                    { i21.Text = "X"; }
                    if (qhh.Where(p => p.STT == 30).First().KetQua == "r" || qhh.Where(p => p.STT == 30).First().KetQua == "R")
                    { r21.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 31).Count() > 0 && qhh.Where(p => p.STT == 31).First().MaDVct != null && qhh.Where(p => p.STT == 31).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 31).First().KetQua == "s" || qhh.Where(p => p.STT == 31).First().KetQua == "S")
                    { s22.Text = "X"; }
                    if (qhh.Where(p => p.STT == 31).First().KetQua == "i" || qhh.Where(p => p.STT == 31).First().KetQua == "I")
                    { i22.Text = "X"; }
                    if (qhh.Where(p => p.STT == 31).First().KetQua == "r" || qhh.Where(p => p.STT == 31).First().KetQua == "R")
                    { r22.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 32).Count() > 0 && qhh.Where(p => p.STT == 32).First().MaDVct != null && qhh.Where(p => p.STT == 32).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 32).First().KetQua == "s" || qhh.Where(p => p.STT == 32).First().KetQua == "S")
                    { s23.Text = "X"; }
                    if (qhh.Where(p => p.STT == 32).First().KetQua == "i" || qhh.Where(p => p.STT == 32).First().KetQua == "I")
                    { i23.Text = "X"; }
                    if (qhh.Where(p => p.STT == 32).First().KetQua == "r" || qhh.Where(p => p.STT == 32).First().KetQua == "R")
                    { r23.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 33).Count() > 0 && qhh.Where(p => p.STT == 33).First().MaDVct != null && qhh.Where(p => p.STT == 33).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 33).First().KetQua == "s" || qhh.Where(p => p.STT == 33).First().KetQua == "S")
                    { s24.Text = "X"; }
                    if (qhh.Where(p => p.STT == 33).First().KetQua == "i" || qhh.Where(p => p.STT == 33).First().KetQua == "I")
                    { i24.Text = "X"; }
                    if (qhh.Where(p => p.STT == 33).First().KetQua == "r" || qhh.Where(p => p.STT == 33).First().KetQua == "R")
                    { r24.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 34).Count() > 0 && qhh.Where(p => p.STT == 34).First().MaDVct != null && qhh.Where(p => p.STT == 34).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 34).First().KetQua == "s" || qhh.Where(p => p.STT == 34).First().KetQua == "S")
                    { s25.Text = "X"; }
                    if (qhh.Where(p => p.STT == 34).First().KetQua == "i" || qhh.Where(p => p.STT == 34).First().KetQua == "I")
                    { i25.Text = "X"; }
                    if (qhh.Where(p => p.STT == 34).First().KetQua == "r" || qhh.Where(p => p.STT == 34).First().KetQua == "R")
                    { r25.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 35).Count() > 0 && qhh.Where(p => p.STT == 35).First().MaDVct != null && qhh.Where(p => p.STT == 35).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 35).First().KetQua == "s" || qhh.Where(p => p.STT == 35).First().KetQua == "S")
                    { s26.Text = "X"; }
                    if (qhh.Where(p => p.STT == 35).First().KetQua == "i" || qhh.Where(p => p.STT == 35).First().KetQua == "I")
                    { i26.Text = "X"; }
                    if (qhh.Where(p => p.STT == 35).First().KetQua == "r" || qhh.Where(p => p.STT == 35).First().KetQua == "R")
                    { r26.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 36).Count() > 0 && qhh.Where(p => p.STT == 36).First().MaDVct != null && qhh.Where(p => p.STT == 36).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 36).First().KetQua == "s" || qhh.Where(p => p.STT == 36).First().KetQua == "S")
                    { s27.Text = "X"; }
                    if (qhh.Where(p => p.STT == 36).First().KetQua == "i" || qhh.Where(p => p.STT == 36).First().KetQua == "I")
                    { i27.Text = "X"; }
                    if (qhh.Where(p => p.STT == 36).First().KetQua == "r" || qhh.Where(p => p.STT == 36).First().KetQua == "R")
                    { r27.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 39).Count() > 0 && qhh.Where(p => p.STT == 39).First().MaDVct != null && qhh.Where(p => p.STT == 39).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 39).First().KetQua == "s" || qhh.Where(p => p.STT == 39).First().KetQua == "S")
                    { s28.Text = "X"; }
                    if (qhh.Where(p => p.STT == 39).First().KetQua == "i" || qhh.Where(p => p.STT == 39).First().KetQua == "I")
                    { i28.Text = "X"; }
                    if (qhh.Where(p => p.STT == 39).First().KetQua == "r" || qhh.Where(p => p.STT == 39).First().KetQua == "R")
                    { r28.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 40).Count() > 0 && qhh.Where(p => p.STT == 40).First().MaDVct != null && qhh.Where(p => p.STT == 40).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 40).First().KetQua == "s" || qhh.Where(p => p.STT == 40).First().KetQua == "S")
                    { s29.Text = "X"; }
                    if (qhh.Where(p => p.STT == 40).First().KetQua == "i" || qhh.Where(p => p.STT == 40).First().KetQua == "I")
                    { i29.Text = "X"; }
                    if (qhh.Where(p => p.STT == 40).First().KetQua == "r" || qhh.Where(p => p.STT == 40).First().KetQua == "R")
                    { r29.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 41).Count() > 0 && qhh.Where(p => p.STT == 41).First().MaDVct != null && qhh.Where(p => p.STT == 41).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 41).First().KetQua == "s" || qhh.Where(p => p.STT == 41).First().KetQua == "S")
                    { s30.Text = "X"; }
                    if (qhh.Where(p => p.STT == 41).First().KetQua == "i" || qhh.Where(p => p.STT == 41).First().KetQua == "I")
                    { i30.Text = "X"; }
                    if (qhh.Where(p => p.STT == 41).First().KetQua == "r" || qhh.Where(p => p.STT == 41).First().KetQua == "R")
                    { r30.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 42).Count() > 0 && qhh.Where(p => p.STT == 42).First().MaDVct != null && qhh.Where(p => p.STT == 42).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 42).First().KetQua == "s" || qhh.Where(p => p.STT == 42).First().KetQua == "S")
                    { s31.Text = "X"; }
                    if (qhh.Where(p => p.STT == 42).First().KetQua == "i" || qhh.Where(p => p.STT == 42).First().KetQua == "I")
                    { i31.Text = "X"; }
                    if (qhh.Where(p => p.STT == 42).First().KetQua == "r" || qhh.Where(p => p.STT == 42).First().KetQua == "R")
                    { r31.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 43).Count() > 0 && qhh.Where(p => p.STT == 43).First().MaDVct != null && qhh.Where(p => p.STT == 43).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 43).First().KetQua == "s" || qhh.Where(p => p.STT == 43).First().KetQua == "S")
                    { s32.Text = "X"; }
                    if (qhh.Where(p => p.STT == 43).First().KetQua == "i" || qhh.Where(p => p.STT == 43).First().KetQua == "I")
                    { i32.Text = "X"; }
                    if (qhh.Where(p => p.STT == 43).First().KetQua == "r" || qhh.Where(p => p.STT == 43).First().KetQua == "R")
                    { r32.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 44).Count() > 0 && qhh.Where(p => p.STT == 44).First().MaDVct != null && qhh.Where(p => p.STT == 44).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 44).First().KetQua == "s" || qhh.Where(p => p.STT == 44).First().KetQua == "S")
                    { s33.Text = "X"; }
                    if (qhh.Where(p => p.STT == 44).First().KetQua == "i" || qhh.Where(p => p.STT == 44).First().KetQua == "I")
                    { i33.Text = "X"; }
                    if (qhh.Where(p => p.STT == 44).First().KetQua == "r" || qhh.Where(p => p.STT == 44).First().KetQua == "R")
                    { r33.Text = "X"; }
                }
                if (qhh.Where(p => p.STT == 45).Count() > 0 && qhh.Where(p => p.STT == 45).First().MaDVct != null && qhh.Where(p => p.STT == 45).First().KetQua != null)
                {
                    if (qhh.Where(p => p.STT == 45).First().KetQua == "s" || qhh.Where(p => p.STT == 45).First().KetQua == "S")
                    { s34.Text = "X"; }
                    if (qhh.Where(p => p.STT == 45).First().KetQua == "i" || qhh.Where(p => p.STT == 45).First().KetQua == "I")
                    { i34.Text = "X"; }
                    if (qhh.Where(p => p.STT == 45).First().KetQua == "r" || qhh.Where(p => p.STT == 45).First().KetQua == "R")
                    { r34.Text = "X"; }
                }
            #endregion
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
            //if (DungChung.Bien.MaBV == "30004")
            //{
            //    colTenBSDT.Visible = false;
            //    colTenTKXN.Visible = false;
            //}
        }

        private void lab113_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (DungChung.Bien.MaBV == "30007")
            //{
            //    lab113.Text = "Bác Sỹ Cận Lâm Sàng".ToUpper();
            //}
        }

        private void Rep_PhieuXNViSinh_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30010")
            {
                Detail1.Visible = false;
            }
            else Detail1.Visible = false;
        }
    }
}
