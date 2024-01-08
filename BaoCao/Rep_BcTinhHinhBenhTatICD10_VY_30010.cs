using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BcTinhHinhBenhTatICD10_VY_30010 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcTinhHinhBenhTatICD10_VY_30010()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
           
            colTenCB.DataBindings.Add("Text", DataSource, "TenCB");
            colMaCB.DataBindings.Add("Text", DataSource, "NhomCB");
            xrTableCell22.DataBindings.Add("Text", DataSource, DungChung.Bien.MaBV == "30010" ? "_STTChuong" : "STTCB");

            colSTT.DataBindings.Add("Text", DataSource, "STTICD");
            colTenICD.DataBindings.Add("Text", DataSource, "TenBenh");
            colMaICD.DataBindings.Add("Text",DataSource,"MaICD");

            colKBTS.DataBindings.Add("Text", DataSource, "KBTS");
            colKBNu.DataBindings.Add("Text", DataSource, "KBNu");
            colKBTE.DataBindings.Add("Text", DataSource, "KBTE");
            //col_TuVongTaiVienTaiKhoa_Detail.DataBindings.Add("Text", DataSource, "KBTuVong");
            colDTMacTS.DataBindings.Add("Text", DataSource, "DTMacTS");
            colDTMacNu.DataBindings.Add("Text", DataSource, "DTMacNu");
            colDTTuVongTS.DataBindings.Add("Text", DataSource, "DTTuVongTS");
            colDTTuVongNu.DataBindings.Add("Text", DataSource, "DTTuVongNu");
            colDTTEMacTS.DataBindings.Add("Text", DataSource, "DTTEMacTS");
            colDTTEMac5.DataBindings.Add("Text", DataSource, "DTTEMac5");
            colDTTETuVongTS.DataBindings.Add("Text", DataSource, "DTTETuVongTS");
            colDTTETuVong5.DataBindings.Add("Text", DataSource, "DTTETuVong5");

            col_GH_TuVongTruocVien.DataBindings.Add("Text", DataSource, "TuVongTruocVien");
            col_TuVongTruocVienTaiKhoa_DeTail.DataBindings.Add("Text", DataSource, "TuVongTruocVien");

            col_GH_BNNangxinveTaiKhoa.DataBindings.Add("Text", DataSource, "BN_NangxinveTaiKhoa");
            colBN_NangxinveTaiKhoa_Detail.DataBindings.Add("Text", DataSource, "BN_NangxinveTaiKhoa");

            col_GH_TuVongTaiVienTaiKhoa.DataBindings.Add("Text", DataSource, "TuVongTaiVien");
            col_TuVongTaiVienTaiKhoa_Detail.DataBindings.Add("Text", DataSource, "TuVongTaiVien");

            col_GH_BNNangXinVeTS_NoiTru.DataBindings.Add("Text", DataSource, "BN_NangxinveTS");
            colBN_NangxinveTS_Detail.DataBindings.Add("Text", DataSource, "BN_NangxinveTS");

            col_GH_BNNangXinVeNu_NoiTru.DataBindings.Add("Text", DataSource, "BN_NangXinVeNu");
            colBN_NangXinVeNu_Detail.DataBindings.Add("Text", DataSource, "BN_NangXinVeNu");
            //    public int? BN_NangxinveTS { get; set; }
            //public int? BN_NangXinVeNu { get; set; }
            //public int? BN_NangxinveTaiKhoa { get; set; }
            //public int? TuVongTaiVien { get; set; }
            //public int? TuVongTruocVien { get; set; }
            //public int? TuVongDuocCapPhep { get; set; }
           
            colKBTSt.DataBindings.Add("Text", DataSource, "KBTS");
            colKBNut.DataBindings.Add("Text", DataSource, "KBNu");
            colKBTEt.DataBindings.Add("Text", DataSource, "KBTE");
            //col_GH_TuVongTaiVienTaiKhoa.DataBindings.Add("Text", DataSource, "KBTuVong");
            colDTMacTSt.DataBindings.Add("Text", DataSource, "DTMacTS");
            colDTMacNut.DataBindings.Add("Text", DataSource, "DTMacNu");
            colDTTuVongTSt.DataBindings.Add("Text", DataSource, "DTTuVongTS");
            colDTTuVongNut.DataBindings.Add("Text", DataSource, "DTTuVongNu");
            colDTTEMacTSt.DataBindings.Add("Text", DataSource, "DTTEMacTS");
            colDTTEMac5t.DataBindings.Add("Text", DataSource, "DTTEMac5");
            colDTTETuVongTSt.DataBindings.Add("Text", DataSource, "DTTETuVongTS");
            colDTTETuVong5t.DataBindings.Add("Text", DataSource, "DTTETuVong5");

            c1t.DataBindings.Add("Text", DataSource, "KBTS");
            c2t.DataBindings.Add("Text", DataSource, "KBNu");
            c3t.DataBindings.Add("Text", DataSource, "KBTE");
            c4t.DataBindings.Add("Text", DataSource, "KBTuVong");
            c5t.DataBindings.Add("Text", DataSource, "DTMacTS");
            c6t.DataBindings.Add("Text", DataSource, "DTMacNu");
            c7t.DataBindings.Add("Text", DataSource, "DTTuVongTS");
            c8t.DataBindings.Add("Text", DataSource, "DTTuVongNu");
            c9t.DataBindings.Add("Text", DataSource, "DTTEMacTS");
            c10t.DataBindings.Add("Text", DataSource, "DTTEMac5");
            c11t.DataBindings.Add("Text", DataSource, "DTTETuVongTS");
            c12t.DataBindings.Add("Text", DataSource, "DTTETuVong5");
            xrTableCell44.DataBindings.Add("Text", DataSource, "BN_NangxinveTaiKhoa");
            xrTableCell45.DataBindings.Add("Text", DataSource, "TuVongTruocVien");
            xrTableCell46.DataBindings.Add("Text", DataSource, "BN_NangxinveTS");
            xrTableCell47.DataBindings.Add("Text", DataSource, "colBN_NangXinVeNu_Detail");
            GroupHeader1.GroupFields.Add(new GroupField("STTCB"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            colTTDV.Text = DungChung.Bien.GiamDoc;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

            //if (Convert.ToInt32(this.GetCurrentColumnValue("kbts")) > 0)
            //{
            //    string _kbts = Convert.ToInt32(this.GetCurrentColumnValue("kbts")).ToString();
            //    colKBTS.Text = _kbts;
            //}
            //else { colKBTS.Text = ""; }
            //if (Convert.ToInt32(this.GetCurrentColumnValue("kbnu")) != 0)
            //{ colKBNu.Text = Convert.ToInt32(this.GetCurrentColumnValue("kbnu")).ToString(); }
            //else { colKBNu.Text = ""; }
            //if (Convert.ToInt32(this.GetCurrentColumnValue("kbte")) != 0)
            //{ colKBTE.Text = Convert.ToInt32(this.GetCurrentColumnValue("kbte")).ToString(); }
            //else { colKBTE.Text = ""; }
            //if (Convert.ToInt32(this.GetCurrentColumnValue("kbtuvong")) != 0)
            //{ colKBTuVong.Text = Convert.ToInt32(this.GetCurrentColumnValue("kbtuvong")).ToString(); }
            //else { colKBTuVong.Text = ""; }
            //if (Convert.ToInt32(this.GetCurrentColumnValue("dtmacts")) != 0)
            //{ colDTMacTS.Text = Convert.ToInt32(this.GetCurrentColumnValue("dtmacts")).ToString(); }
            //else { colDTMacTS.Text = ""; }
            //if (Convert.ToInt32(this.GetCurrentColumnValue("dtmacnu")) != 0)
            //{ colDTMacNu.Text = Convert.ToInt32(this.GetCurrentColumnValue("dtmacnu")).ToString(); }
            //else { colDTMacNu.Text = ""; }
            //if (Convert.ToInt32(this.GetCurrentColumnValue("dttuvongts")) != 0)
            //{ colDTTuVongTS.Text = Convert.ToInt32(this.GetCurrentColumnValue("dttuvongts")).ToString(); }
            //else { colDTTuVongTS.Text = ""; }
            //if (Convert.ToInt32(this.GetCurrentColumnValue("dttuvongnu")) != 0)
            //{ colDTTuVongNu.Text = Convert.ToInt32(this.GetCurrentColumnValue("dttuvongnu")).ToString(); }
            //else { colDTTuVongNu.Text = ""; }
            //if (Convert.ToInt32(this.GetCurrentColumnValue("dttemacts")) != 0)
            //{ colDTTEMacTS.Text = Convert.ToInt32(this.GetCurrentColumnValue("dttemacts")).ToString(); }
            //else { colDTTEMacTS.Text = ""; }
            //if (Convert.ToInt32(this.GetCurrentColumnValue("dttemac5")) != 0)
            //{ colDTTEMac5.Text = Convert.ToInt32(this.GetCurrentColumnValue("dttemac5")).ToString(); }
            //else { colDTTEMac5.Text = ""; }
            //if (Convert.ToInt32(this.GetCurrentColumnValue("dttetuvongts")) != 0)
            //{ colDTTETuVongTS.Text = Convert.ToInt32(this.GetCurrentColumnValue("dttetuvongts")).ToString(); }
            //else { colDTTETuVongTS.Text = ""; }
            //if (Convert.ToInt32(this.GetCurrentColumnValue("dttetuvong5")) != 0)
            //{ colDTTETuVong5.Text = Convert.ToInt32(this.GetCurrentColumnValue("dttetuvong5")).ToString(); }
            //else { colDTTETuVong5.Text = ""; } 
         }

        private void colKBTS_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }

        private void colMaICD_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (this.GetCurrentColumnValue("kbts") != null && this.GetCurrentColumnValue("kbts").ToString() != "")
            //{
            //    string a = this.GetCurrentColumnValue("KBTS").ToString();
            //}
        }
    }
}
