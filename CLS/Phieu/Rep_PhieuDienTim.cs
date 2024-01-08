using System;
using System.ComponentModel;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuDienTim : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuDienTim()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "27777" || DungChung.Bien.MaBV == "24272" || DungChung.Bien.MaBV == "30372")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
                xrPictureBox1.Image = DungChung.Ham.GetLogo();
                colDiaChi.Text = colDiaChi2.Text = DungChung.Ham.GetDiaChiBV();
                if (DungChung.Bien.MaBV == "24272")
                {
                    SubBand1.Visible = false;
                    SubBand2.Visible = false;
                    SubBand3.Visible = true;
                    xrPictureBox1.Visible = false;
                    xrPictureBox2.Visible = true;
                    xrPictureBox2.Image = DungChung.Ham.GetLogo();
                }
            }
            else
            {
                SubBand1.Visible = true;
                SubBand2.Visible = false;
            }
        }

        private QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //  string mabn = MaBNhan.Value.ToString();
            var qcls = (from dvct in DataContect.DichVucts
                        join dv in DataContect.DichVus on dvct.MaDV equals dv.MaDV
                        join tnhomdv in DataContect.TieuNhomDVs on dv.IdTieuNhom equals tnhomdv.IdTieuNhom
                        where (tnhomdv.TenTN.Contains("Điện tim"))

                        select new { dvct.MaDVct, dvct.TenDVct, dvct.STT }).ToList();
            int sophieu = 0;
            int ot;
            if (Int32.TryParse(SoPhieu.Value.ToString(), out ot))
                sophieu = int.Parse(SoPhieu.Value.ToString());

            var qhh = (from cls in DataContect.CLS.Where(p => p.IdCLS == sophieu)
                       join cd in DataContect.ChiDinhs on cls.IdCLS equals cd.IdCLS
                       join clsct in DataContect.CLScts on cd.IDCD equals clsct.IDCD
                       join dvct in DataContect.DichVucts on clsct.MaDVct equals dvct.MaDVct
                       join dv in DataContect.DichVus on dvct.MaDV equals dv.MaDV
                       select new { clsct.MaDVct, clsct.KetQua, dvct.TenDVct, dvct.STT, clsct.KetQua_Rtf }).ToList();
            if (qcls.Count > 0)
            {
                if (DungChung.Bien.MaBV != "30004")
                {
                    if (qcls.Where(p => p.STT == 1).Count() > 0 && qcls.Where(p => p.STT == 1).First().TenDVct != null)
                    {
                        col1.Text = qcls.Where(p => p.STT == 1).First().TenDVct.ToString() + ": ";
                    }
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
            if (qhh.Count > 0)
            {
                if (qhh.Where(p => p.STT == 1).Count() > 0 && qhh.Where(p => p.STT == 1).First().KetQua != null)
                {
                    txtKQ1.Text = qhh.Where(p => p.STT == 1).First().KetQua.ToString();

                }
                if (qhh.First().KetQua != null)
                {
                    colLoiDan.Text = qhh.First().KetQua;// lay loi dan can xem lai
                }
                if (qhh.Where(p => p.STT == 2).Count() > 0 && qhh.Where(p => p.STT == 2).First().KetQua != null)
                {
                    txtKQ2.Text = qhh.Where(p => p.STT == 2).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 3).Count() > 0 && qhh.Where(p => p.STT == 3).First().KetQua != null)
                {
                    txtKQ3.Text = qhh.Where(p => p.STT == 3).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 4).Count() > 0 && qhh.Where(p => p.STT == 4).First().KetQua != null)
                {
                    txtKQ4.Text = qhh.Where(p => p.STT == 4).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 5).Count() > 0 && qhh.Where(p => p.STT == 5).First().KetQua != null)
                {
                    txtKQ5.Text = qhh.Where(p => p.STT == 5).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 6).Count() > 0 && qhh.Where(p => p.STT == 6).First().KetQua != null)
                {
                    txtKQ6.Text = qhh.Where(p => p.STT == 6).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 7).Count() > 0 && qhh.Where(p => p.STT == 7).First().KetQua != null)
                {
                    txtKQ7.Text = qhh.Where(p => p.STT == 7).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 8).Count() > 0 && qhh.Where(p => p.STT == 8).First().KetQua != null)
                {
                    txtKQ8.Text = qhh.Where(p => p.STT == 8).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 9).Count() > 0 && qhh.Where(p => p.STT == 9).First().KetQua != null)
                {
                    txtKQ9.Text = qhh.Where(p => p.STT == 9).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 10).Count() > 0 && qhh.Where(p => p.STT == 10).First().KetQua != null)
                {
                    txtKQ10.Text = qhh.Where(p => p.STT == 10).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 11).Count() > 0 && qhh.Where(p => p.STT == 11).First().KetQua != null)
                {
                    txtKQ11.Text = qhh.Where(p => p.STT == 11).First().KetQua.ToString();
                }
                if (qhh.Where(p => p.STT == 12).Count() > 0 && qhh.Where(p => p.STT == 12).First().KetQua != null)
                {
                    txtKQ12.Text = qhh.Where(p => p.STT == 12).First().KetQua.ToString();
                }
            }

            if (DungChung.Bien.MaBV == "24272")
            {
                col2.Visible = false;
                col3.Visible = false;
                col4.Visible = false;
                col5.Visible = false;
                col7.Visible = false;
                col6.Visible = false;
                col8.Visible = false;
                col9.Visible = false;
                col10.Visible = false;
                col11.Visible = false;
                col12.Visible = false;
                txtKQ2.Visible = false;
                txtKQ3.Visible = false;
                txtKQ4.Visible = false;
                txtKQ5.Visible = false;
                txtKQ6.Visible = false;
                txtKQ7.Visible = false;
                txtKQ8.Visible = false;
                txtKQ9.Visible = false;
                txtKQ10.Visible = false;
                txtKQ11.Visible = false;
                txtKQ12.Visible = false;
                this.tb_KetQua.SizeF = new System.Drawing.SizeF(750.4166F, 10.9471F);
            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
            {
                txtmabn.Visible = true;
                //xrRichText1.Visible = true;
                lab2.Visible = false;
                thuong.Visible = true;
                thuong1.Visible = true;
                capcuu.Visible = true;
                capcuu1.Visible = true;
            }
            tb_ChiDinh.Visible = DungChung.Bien._Visible_CDHA[0];
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[2];
            if (DungChung.Bien.MaBV == "24009")
            {
                xrTable2.Visible = DungChung.Bien._Visible_CDHA[0];
                tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
            }
            xrLabel53.Text = colTenCQ.Text = colTenCQ2.Text = DungChung.Bien.TenCQ.ToUpper();
            xrLabel54.Text = colTenCQCQ.Text = colTenCQCQ2.Text = DungChung.Bien.TenCQCQ.ToUpper();
            string _bs = MaCBDT.Value.ToString();

            xrLabel56.Text = colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, _bs);
            //if (DungChung.Bien.MaBV == "27001")
            //{
            //    tb_ChiDinh.Visible = false;
            //    tb_KetQua.Visible = false;
            //}

            if (DungChung.Bien.MaBV == "02005")
            {
                rowBSDieuTri.Visible = false;
            }
            if (DungChung.Bien.MaBV == "30004")
            {
                colTenTKXN.Visible = false;
                colTenBSDT.Visible = false;
                tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
            }
            if (Macb.Value != null)
                colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            if (DungChung.Bien.MaBV == "26007")
                xrBarCode1.Visible = true;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
        }
    }
}