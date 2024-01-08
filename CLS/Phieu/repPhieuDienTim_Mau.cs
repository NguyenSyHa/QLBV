
using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.IO;

namespace QLBV.BaoCao
{
    public partial class repPhieuDienTim_Mau : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuDienTim_Mau()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[2];
            if (DungChung.Bien.MaBV == "24009")
            {
                tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
            }
            txtSoYT.Text = "Điện thoại: " + DungChung.Bien.SDTCQ;
            // start HIS-1484
            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
            {
                txtdichchi.Font = txtSoYT.Font = new Font("Times New Roman", 13, FontStyle.Bold);
                txtdichchi.Text = "Địa chỉ: " + DungChung.Bien.DiaChi;
            }
            else
            {
                txtdichchi.Text = "Địa chỉ: " + DungChung.Bien.DiaChi;
            }
            // end HIS-1484 
            txtTenBV.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24297")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
            }


        }
        public void hienthiKetQua(string str)
        {
        }

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






            if (DungChung.Bien.MaBV == "24009")
            {
                lab19.Visible = false;
                xrPictureBox1.Visible = false;
                xrPictureBox2.Visible = false;
            }
            string s = paramDuongDan2.Value.ToString();
            string t = @"\s|";
            //string[] element = System.Text.RegularExpressions.Regex.Split(s,t);
            string[] element = s.Split('|');
            string _duongdan = element[0].ToString();
            if (DungChung.Bien.MaBV == "30372")
            {

                if (element[0].ToString() != "")
                {
                    this.xrPictureBox1.Image = Image.FromFile(element[0].ToString());
                }
                else
                {
                    xrPictureBox1.Image = null;
                }
                if (element[1].ToString() != "")
                {
                    this.xrPictureBox2.Image = Image.FromFile(element[1].ToString());
                }
                else
                {
                    xrPictureBox2.Image = null;
                }
            }
            else
            {
                if (DuongDan.Value != null)
                {
                    if (DuongDan.Value != "")
                        xrPictureBox1.Image = Image.FromFile(DuongDan.Value.ToString());
                }
                else
                {
                    xrPictureBox1.Image = null;
                }
                if (paramDuongDan2.Value != null && !String.IsNullOrEmpty(paramDuongDan2.Value.ToString()))
                {
                    if (File.Exists(element[0]))
                        this.xrPictureBox2.Image = Image.FromFile(element[0].ToString());
                }
                else
                {
                    xrPictureBox2.Image = null;
                    this.xrPictureBox2.Borders = DevExpress.XtraPrinting.BorderSide.None;
                }
            }



            if (Macb.Value != null)
            {
                colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            }
        }
    }
}
