using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;

namespace QLBV.FormThamSo
{
    public partial class rep_PhieuThu_01071_A5 : DevExpress.XtraReports.UI.XtraReport
    {
        int _Ploai = 0;
        bool _inchitiet = false;
        public rep_PhieuThu_01071_A5(int a, bool _InChiTiet)
        {
            InitializeComponent();
            _Ploai = a;
            _inchitiet = _InChiTiet;
        }
        public void bindingdata(int a)
        {
            if(DungChung.Bien.MaBV == "30372")
            {
                tb_30372.Visible = true;
                tb_khac.Visible = false;
            }    
            coltendv.DataBindings.Add("Text", DataSource, "TenDV");
            colchiphi.DataBindings.Add("Text", DataSource, "ChiPhi").FormatString = "{0:##,###}";
            coltienbn.DataBindings.Add("Text", DataSource, "TenBN").FormatString = "{0:##,###}";
            coltennhom.DataBindings.Add("Text", DataSource, "TenNhom");
            GroupHeader1.GroupFields.Add(new GroupField("STT"));
            GroupHeader1.GroupFields.Add(new GroupField("TenNhom"));
            //colstt.DataBindings.Add("Text", DataSource, "STT");
            colchiphitong.DataBindings.Add("Text", DataSource, "ChiPhi");
            coltienbntong.DataBindings.Add("Text", DataSource, "TenBN");
            colcpt.DataBindings.Add("Text", DataSource, "ChiPhi");
            coltbnt.DataBindings.Add("Text", DataSource, "TenBN");
            coltongNOP.DataBindings.Add("Text", DataSource, "TenBN");
            //if(a==1)
            //{
            GroupHeader2.GroupFields.Add(new GroupField("TrongBH"));
            coltrongdm.DataBindings.Add("Text", DataSource, "TrongDM");
            coltrongdm_30372.DataBindings.Add("Text", DataSource, "TrongDM");
            coltongcpbh.DataBindings.Add("Text", DataSource, "ChiPhi");
            colbntrabh.DataBindings.Add("Text", DataSource, "TenBN");
            //}
        }


        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            labngaygio.Text = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            if (DungChung.Bien.MaBV == "30372")
            {

                subBand1.Visible = false;
                subBand2.Visible = true;
                //xrPictureBox1.Image = DungChung.Ham.GetLogo();
                xrLabel1.Text = DungChung.Ham.GetDiaChiBV();
                xrLabel2.Text = "SĐT: " + DungChung.Ham.GetSDTBV() + " - " + "Fax: " + DungChung.Ham.GetFaxBV();
            }
            else
            {
                subBand1.Visible = true;
                subBand2.Visible = false;
            }
            if (_inchitiet == false)
            {
                Detail.Visible = false;
                if (_Ploai == 1)
                {
                    xrTableRow24.Visible = true;
                    xrTableRow13.Visible = true;
                }
            }
            else
            {
                xrTableRow13.Visible = false;
            }
            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
            {
                xrTableCell2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                xrTableCell30.Visible = true;
            }
        }
        int i = 1;
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (_inchitiet == false)
            {
                colstt.Text = i.ToString();
            }
            //if (DungChung.Bien.MaBV == "12345" && _Ploai == 3)
            //{
            //    Detail.Visible = true;
            //    //GroupHeader1.Visible = false;
            //}
            else
            {
                if (this.GetCurrentColumnValue("TenNhom") != null)
                {
                    string tn = this.GetCurrentColumnValue("TenNhom").ToString();
                    if (tn == "Chẩn đoán hình ảnh" || tn == "Thủ thuật, phẫu thuật" || tn.Contains("Nội soi") || tn.ToLower().Contains("thuật"))
                    {
                        Detail.Visible = true;
                        b = 1;
                        if (DungChung.Bien.MaBV != "30372")
                        {
                            this.coltendv.Font = new System.Drawing.Font("Times New Roman", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic))));
                            this.coltendv.Font = new System.Drawing.Font("Times New Roman", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic))));
                            this.colsottdv.Font = new System.Drawing.Font("Times New Roman", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic))));
                            this.coltienbn.Font = new System.Drawing.Font("Times New Roman", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic))));
                            this.colchiphi.Font = new System.Drawing.Font("Times New Roman", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic))));
                        }
                    }
                    else
                    {
                        if (DungChung.Bien.MaBV == "30372")
                        {
                            Detail.Visible = true;
                        }
                        else
                            Detail.Visible = false;
                    }
                }
            }
            if (this.GetCurrentColumnValue("TrongDM") != null)
            {
                string tn = this.GetCurrentColumnValue("TrongDM").ToString();
                if (tn == "Chẩn đoán hình ảnh")
                {
                    b = 1;
                    if (DungChung.Bien.MaBV == "56789"/* || DungChung.Bien.MaBV == "30372"*/)
                    {
                        Detail.Visible = true;
                        this.coltendv.Font = new System.Drawing.Font("Times New Roman", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic))));
                        this.coltendv.Font = new System.Drawing.Font("Times New Roman", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic))));
                        this.colsottdv.Font = new System.Drawing.Font("Times New Roman", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic))));
                        this.coltienbn.Font = new System.Drawing.Font("Times New Roman", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic))));
                        this.colchiphi.Font = new System.Drawing.Font("Times New Roman", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic))));
                    }
                }

            }
            else
            {
                if (DungChung.Bien.MaBV != "30372")
                {
                    this.coltennhom.Font = new System.Drawing.Font("Times New Roman", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
                    this.colchiphitong.Font = new System.Drawing.Font("Times New Roman", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
                    this.coltienbntong.Font = new System.Drawing.Font("Times New Roman", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
                }
            }
            i++;


        }

        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {

        }
        int a = 1;
        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            i = 1;
            b = 1;
            switch (a)
            {
                case 1:
                    colsttbh_30372.Text = colsttbh.Text = "I.";
                    break;
                case 2:
                    colsttbh_30372.Text = colsttbh.Text = "II.";
                    break;
                case 3:
                    colsttbh_30372.Text = colsttbh.Text = "III.";
                    break;
                case 4:
                    colsttbh_30372.Text = colsttbh.Text = "IV.";
                    break;
                case 5:
                    colsttbh_30372.Text = colsttbh.Text = "V.";
                    break;
            }
            a++;
            if (_Ploai != 1)
            {
                if (DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297")
                {
                    if (this.GetCurrentColumnValue("TrongDM") != null)
                    {
                        string tn = this.GetCurrentColumnValue("TrongDM").ToString();
                        if (tn == "Chẩn đoán hình ảnh" || tn == "Thủ thuật, phẫu thuật")
                        {
                            GroupHeader1.Visible = true;
                            Detail.Visible = true;
                        }
                        else
                        {
                            GroupHeader1.Visible = false;
                        }
                    }
                }
                else
                    GroupHeader1.Visible = false;
            }
            else
            {
                if (this.GetCurrentColumnValue("TrongDM") != null)
                {
                    string tn = this.GetCurrentColumnValue("TrongDM").ToString();
                    if (tn == "Chi phí phụ thu" || tn == "Chẩn đoán hình ảnh" || tn == "Thủ thuật, phẫu thuật")
                    {
                        GroupHeader1.Visible = true;
                        Detail.Visible = true;
                    }
                    else
                    {
                        GroupHeader1.Visible = false;
                    }
                }
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            int ktradong = b + i + a;
            if(DungChung.Bien.MaBV == "30372")
            {
                xrTableCell26.Visible = false;
            }
        }
        int b = 1;
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            colsottdv.Text = b.ToString();
            b++;
        }

        private void GroupHeader4_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void GroupHeader3_BeforePrint(object sender, CancelEventArgs e)
        {

        }

    }
}
