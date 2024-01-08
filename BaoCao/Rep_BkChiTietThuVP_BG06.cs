using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_BkChiTietThuVP_BG06 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BkChiTietThuVP_BG06()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colNTNgh.DataBindings.Add("Text", DataSource, "NThang");
            colCKT.DataBindings.Add("Text", DataSource, "CK").FormatString = DungChung.Bien.FormatString[1];
            colSAT.DataBindings.Add("Text", DataSource, "SA").FormatString = DungChung.Bien.FormatString[1];
            colDTT.DataBindings.Add("Text", DataSource, "DT").FormatString = DungChung.Bien.FormatString[1];
            colXQT.DataBindings.Add("Text", DataSource, "XQ").FormatString = DungChung.Bien.FormatString[1];
            colNSDDT.DataBindings.Add("Text", DataSource, "NSDD").FormatString = DungChung.Bien.FormatString[1];
            colNSTMHT.DataBindings.Add("Text", DataSource, "NSTMH").FormatString = DungChung.Bien.FormatString[1];
            colMauT.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colPTMT.DataBindings.Add("Text", DataSource, "PTM").FormatString = DungChung.Bien.FormatString[1];
            colNTT.DataBindings.Add("Text", DataSource, "NT").FormatString = DungChung.Bien.FormatString[1];
            colSHMT.DataBindings.Add("Text", DataSource, "SHM").FormatString = DungChung.Bien.FormatString[1];
            colVGBT.DataBindings.Add("Text", DataSource, "VGB").FormatString = DungChung.Bien.FormatString[1];
            colHIVT.DataBindings.Add("Text", DataSource, "HIV").FormatString = DungChung.Bien.FormatString[1];
            colBoBotT.DataBindings.Add("Text", DataSource, "BoBot").FormatString = DungChung.Bien.FormatString[1];
            colNaoThaiT.DataBindings.Add("Text", DataSource, "NaoThai").FormatString = DungChung.Bien.FormatString[1];
            colTBeT.DataBindings.Add("Text", DataSource, "TamBe").FormatString = DungChung.Bien.FormatString[1];
            colDVKhacT.DataBindings.Add("Text", DataSource, "Khac").FormatString = DungChung.Bien.FormatString[1];
         //   colBHYTT.DataBindings.Add("Text", DataSource, "BHYT").FormatString = DungChung.Bien.FormatString[1];
            colGDSKT.DataBindings.Add("Text", DataSource, "GDSK").FormatString = DungChung.Bien.FormatString[1];
         //   colKSKT.DataBindings.Add("Text", DataSource, "KSK").FormatString = DungChung.Bien.FormatString[1];
            colNGT.DataBindings.Add("Text", DataSource, "NG").FormatString = DungChung.Bien.FormatString[1];
            colVienPhiT.DataBindings.Add("Text", DataSource, "VP").FormatString = DungChung.Bien.FormatString[1];
            colTongT.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];
            //colNTN.DataBindings.Add("Text", DataSource, "NTN");
        
            colSoHD.DataBindings.Add("Text", DataSource, "SoHD");
            colTenBNhan.DataBindings.Add("Text", DataSource, "TenBN");
            colDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            //colCK.DataBindings.Add("Text", DataSource, "CK").FormatString = DungChung.Bien.FormatString[1];
            //colSA.DataBindings.Add("Text", DataSource, "SA").FormatString = DungChung.Bien.FormatString[1];
            //colDT.DataBindings.Add("Text", DataSource, "DT").FormatString = DungChung.Bien.FormatString[1];
            //colXQ.DataBindings.Add("Text", DataSource, "XQ").FormatString = DungChung.Bien.FormatString[1];
            //colKhacCD.DataBindings.Add("Text", DataSource, "KhacCD").FormatString = DungChung.Bien.FormatString[1];
            //colMau.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            //colPTM.DataBindings.Add("Text", DataSource, "PTM").FormatString = DungChung.Bien.FormatString[1];
            //colNT.DataBindings.Add("Text", DataSource, "NT").FormatString = DungChung.Bien.FormatString[1];
            //colSHM.DataBindings.Add("Text", DataSource, "SHM").FormatString = DungChung.Bien.FormatString[1];
            //colVGB.DataBindings.Add("Text", DataSource, "VGB").FormatString = DungChung.Bien.FormatString[1];
            //colHIV.DataBindings.Add("Text", DataSource, "HIV").FormatString = DungChung.Bien.FormatString[1];
            //colAminra.DataBindings.Add("Text", DataSource, "Aminra").FormatString = DungChung.Bien.FormatString[1];
            //colKhacXN.DataBindings.Add("Text", DataSource, "KhacXN").FormatString = DungChung.Bien.FormatString[1];
            //colBoBot.DataBindings.Add("Text", DataSource, "BoBot").FormatString = DungChung.Bien.FormatString[1];
            //colNaoThai.DataBindings.Add("Text", DataSource, "NaoThai").FormatString = DungChung.Bien.FormatString[1];
            //colNSDD.DataBindings.Add("Text", DataSource, "NSDD").FormatString = DungChung.Bien.FormatString[1];
            //colNSTMH.DataBindings.Add("Text", DataSource, "NSTMH").FormatString = DungChung.Bien.FormatString[1];
            //colTB.DataBindings.Add("Text", DataSource, "TamBeBHYT").FormatString = DungChung.Bien.FormatString[1];
            //colBHYT.DataBindings.Add("Text", DataSource, "BHYT").FormatString = DungChung.Bien.FormatString[1];
            //colGDSK.DataBindings.Add("Text", DataSource, "GDSK").FormatString = DungChung.Bien.FormatString[1];
            //colKSK.DataBindings.Add("Text", DataSource, "KDK").FormatString = DungChung.Bien.FormatString[1];
            //colNG.DataBindings.Add("Text", DataSource, "NG").FormatString = DungChung.Bien.FormatString[1];
            //colVienPhi.DataBindings.Add("Text", DataSource, "VP").FormatString = DungChung.Bien.FormatString[1];
            //colTong.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];

            colCKTC.DataBindings.Add("Text", DataSource, "CK").FormatString = DungChung.Bien.FormatString[1];
            colSATC.DataBindings.Add("Text", DataSource, "SA").FormatString = DungChung.Bien.FormatString[1];
            colDTTC.DataBindings.Add("Text", DataSource, "DT").FormatString = DungChung.Bien.FormatString[1];
            colXQTC.DataBindings.Add("Text", DataSource, "XQ").FormatString = DungChung.Bien.FormatString[1];
            colNSDDTC.DataBindings.Add("Text", DataSource, "NSDD").FormatString = DungChung.Bien.FormatString[1];
            colNSTMHTC.DataBindings.Add("Text", DataSource, "NSTMH").FormatString = DungChung.Bien.FormatString[1];
            colMauTC.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colPTMTC.DataBindings.Add("Text", DataSource, "PTM").FormatString = DungChung.Bien.FormatString[1];
            colNTTC.DataBindings.Add("Text", DataSource, "NT").FormatString = DungChung.Bien.FormatString[1];
            colSHMTC.DataBindings.Add("Text", DataSource, "SHM").FormatString = DungChung.Bien.FormatString[1];
            colVGBTC.DataBindings.Add("Text", DataSource, "VGB").FormatString = DungChung.Bien.FormatString[1];
            colHIVTC.DataBindings.Add("Text", DataSource, "HIV").FormatString = DungChung.Bien.FormatString[1];
            colBoBotTC.DataBindings.Add("Text", DataSource, "BoBot").FormatString = DungChung.Bien.FormatString[1];
            colNaoThaiTC.DataBindings.Add("Text", DataSource, "NaoThai").FormatString = DungChung.Bien.FormatString[1];
            colTBeTC.DataBindings.Add("Text", DataSource, "TamBe").FormatString = DungChung.Bien.FormatString[1];
            colDVKhacTC.DataBindings.Add("Text", DataSource, "Khac").FormatString = DungChung.Bien.FormatString[1];
            // colBHYTTC.DataBindings.Add("Text", DataSource, "BHYT").FormatString = DungChung.Bien.FormatString[1];
            colGDSKTC.DataBindings.Add("Text", DataSource, "GDSK").FormatString = DungChung.Bien.FormatString[1];
          //  colKSKTC.DataBindings.Add("Text", DataSource, "KSK").FormatString = DungChung.Bien.FormatString[1];
            colNGTC.DataBindings.Add("Text", DataSource, "NG").FormatString = DungChung.Bien.FormatString[1];
            colVienPhiTC.DataBindings.Add("Text", DataSource, "VP").FormatString = DungChung.Bien.FormatString[1];
            colTongTC.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];

            GroupHeader1.GroupFields.Add(new GroupField("NThang"));
        }
   

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            GroupHeader1.Visible = true;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            double v;
            if (this.GetCurrentColumnValue("ck") != null)
            {
                 v = Convert.ToDouble(this.GetCurrentColumnValue("ck"));
                if (v == 0)
                {
                    colCK.Text = "";
                }
                else { colCK.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("sa") != null)
            {
                v =Convert.ToDouble(this.GetCurrentColumnValue("sa"));
                if (v == 0)
                {
                    colSA.Text = "";
                }
                else { colSA.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("dt") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("dt"));
                if (v == 0)
                {
                    colDT.Text = "";
                }
                else { colDT.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("xq") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("xq"));
                if (v == 0)
                {
                    colXQ.Text = "";
                }
                else { colXQ.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("nsdd") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("nsdd"));
                if (v == 0)
                {
                    colNSDD.Text = "";
                }
                else { colNSDD.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("nstmh") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("nstmh"));
                if (v == 0)
                {
                    colNSTMH.Text = "";
                }
                else { colNSTMH.Text = v.ToString("#,##"); }
            }
          
            if (this.GetCurrentColumnValue("mau") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("mau"));
                if (v == 0)
                {
                    colMau.Text = "";
                }
                else { colMau.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("ptm") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("ptm"));
                if (v == 0)
                {
                    colPTM.Text = "";
                }
                else { colPTM.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("nt") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("nt"));
                if (v == 0)
                {
                    colNT.Text = "";
                }
                else { colNT.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("shm") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("shm"));
                if (v == 0)
                {
                    colSHM.Text = "";
                }
                else { colSHM.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("vgb") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("vgb"));
                if (v == 0)
                {
                    colVGB.Text = "";
                }
                else { colVGB.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("hiv") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("hiv"));
                if (v == 0)
                {
                    colHIV.Text = "";
                }
                else { colHIV.Text = v.ToString("#,##"); }
            }
            
           
            if (this.GetCurrentColumnValue("bobot") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("bobot"));
                if (v == 0)
                {
                    colBoBot.Text = "";
                }
                else { colBoBot.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("naothai") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("naothai"));
                if (v == 0)
                {
                    colNaoThai.Text = "";
                }
                else { colNaoThai.Text = v.ToString("#,##"); }
            }
           
            if (this.GetCurrentColumnValue("tambe") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("tambe"));
                if (v == 0)
                {
                    colTBe.Text = "";
                }
                else { colTBe.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("khac") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("khac"));
                if (v == 0)
                {
                    colDVKhac.Text = "";
                }
                else { colDVKhac.Text = v.ToString("#,##"); }
            }
            //if (this.GetCurrentColumnValue("bhyt") != null)
            //{
            //    v = Convert.ToDouble(this.GetCurrentColumnValue("bhyt"));
            //    if (v == 0)
            //    {
            //        colBHYT.Text = "";
            //    }
            //    else { colBHYT.Text = v.ToString("#,##"); }
            //}
            if (this.GetCurrentColumnValue("gdsk") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("gdsk"));
                if (v == 0)
                {
                    colGDSK.Text = "";
                }
                else { colGDSK.Text = v.ToString("#,##"); }
            }
            //if (this.GetCurrentColumnValue("ksk") != null)
            //{
            //    v = Convert.ToDouble(this.GetCurrentColumnValue("ksk"));
            //    if (v == 0)
            //    {
            //        colKSK.Text = "";
            //    }
            //    else { colKSK.Text = v.ToString("#,##"); }
            //}
            if (this.GetCurrentColumnValue("ng") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("ng"));
                if (v == 0)
                {
                    colNG.Text = "";
                }
                else { colNG.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("vp") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("vp"));
                if (v == 0)
                {
                    colVienPhi.Text = "";
                }
                else { colVienPhi.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("tong") != null)
            {
                v = Convert.ToDouble(this.GetCurrentColumnValue("tong"));
                if (v == 0)
                {
                    colTong.Text = "";
                }
                else { colTong.Text = v.ToString("#,##"); }
            }
            //if (this.GetCurrentColumnValue("VP") != null)
            //{
            //    v = Convert.ToDouble(this.GetCurrentColumnValue("VP"));
            //    if (v == 0)
            //    {
            //        colVienPhi.Text = "";
            //    }
            //    else { colVienPhi.Text = v.ToString("#,##"); }
            //}
            //if (this.GetCurrentColumnValue("Tong") != null)
            //{
            //    v = Convert.ToDouble(this.GetCurrentColumnValue("Tong"));
            //    if (v == 0)
            //    {
            //        colTong.Text = "";
            //    }
            //    else { colTong.Text = v.ToString("#,##"); }
            //}
           
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colGiamDoc.Text = DungChung.Bien.NguoiLapBieu;
        }

    }
}
