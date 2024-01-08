using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_ThThuVienPhis_BG07 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_ThThuVienPhis_BG07()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData()
        {
            colNTNgh.DataBindings.Add("Text", DataSource, "NThang");
            colCKgh.DataBindings.Add("Text", DataSource, "CK").FormatString = DungChung.Bien.FormatString[1];
            colSAgh.DataBindings.Add("Text", DataSource, "SA").FormatString = DungChung.Bien.FormatString[1];
            colDTgh.DataBindings.Add("Text", DataSource, "DT").FormatString = DungChung.Bien.FormatString[1];
            colXQgh.DataBindings.Add("Text", DataSource, "XQ").FormatString = DungChung.Bien.FormatString[1];
            colNSDDgh.DataBindings.Add("Text", DataSource, "NSDD").FormatString = DungChung.Bien.FormatString[1];
            colNSTMHgh.DataBindings.Add("Text", DataSource, "NSTMH").FormatString = DungChung.Bien.FormatString[1];
           // colNSTMHgh.DataBindings.Add("Text", DataSource, "KhacCD").FormatString = DungChung.Bien.FormatString[1];
            colMaugh.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colPTMgh.DataBindings.Add("Text", DataSource, "PTM").FormatString = DungChung.Bien.FormatString[1];
            colNTgh.DataBindings.Add("Text", DataSource, "NT").FormatString = DungChung.Bien.FormatString[1];
            colSHMgh.DataBindings.Add("Text", DataSource, "SHM").FormatString = DungChung.Bien.FormatString[1];
            colVGBgh.DataBindings.Add("Text", DataSource, "VGB").FormatString = DungChung.Bien.FormatString[1];
            colHIVgh.DataBindings.Add("Text", DataSource, "HIV").FormatString = DungChung.Bien.FormatString[1];
           // colAminragh.DataBindings.Add("Text", DataSource, "Aminra").FormatString = DungChung.Bien.FormatString[1];
            colBoBotgh.DataBindings.Add("Text", DataSource, "BoBot").FormatString = DungChung.Bien.FormatString[1];
            colNaoThaigh.DataBindings.Add("Text", DataSource, "NaoThai").FormatString = DungChung.Bien.FormatString[1];
            colTBegh.DataBindings.Add("Text", DataSource, "TamBe").FormatString = DungChung.Bien.FormatString[1];
            colDVKhacgh.DataBindings.Add("Text", DataSource, "Khac").FormatString = DungChung.Bien.FormatString[1];
            colBHYTgh.DataBindings.Add("Text", DataSource, "BHYT").FormatString = DungChung.Bien.FormatString[1];
            colGDSKgh.DataBindings.Add("Text", DataSource, "GDSK").FormatString = DungChung.Bien.FormatString[1];
            colKSKgh.DataBindings.Add("Text", DataSource, "KSK").FormatString = DungChung.Bien.FormatString[1];
            colNGgh.DataBindings.Add("Text", DataSource, "NG").FormatString = DungChung.Bien.FormatString[1];
            colVienPhigh.DataBindings.Add("Text", DataSource, "VP").FormatString = DungChung.Bien.FormatString[1];
            colTonggh.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];
            
            colNTN.DataBindings.Add("Text", DataSource, "NTN");
            colCK.DataBindings.Add("Text", DataSource, "CK").FormatString = DungChung.Bien.FormatString[1];
            colSA.DataBindings.Add("Text", DataSource, "SA").FormatString = DungChung.Bien.FormatString[1];
            colDT.DataBindings.Add("Text", DataSource, "DT").FormatString = DungChung.Bien.FormatString[1];
            colXQ.DataBindings.Add("Text", DataSource, "XQ").FormatString = DungChung.Bien.FormatString[1];
            colNSDD.DataBindings.Add("Text", DataSource, "NSDD").FormatString = DungChung.Bien.FormatString[1];
            colNSTMH.DataBindings.Add("Text", DataSource, "NSTMH").FormatString = DungChung.Bien.FormatString[1];
            colMau.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colPTM.DataBindings.Add("Text", DataSource, "PTM").FormatString = DungChung.Bien.FormatString[1];
            colNT.DataBindings.Add("Text", DataSource, "NT").FormatString = DungChung.Bien.FormatString[1];
            colSHM.DataBindings.Add("Text", DataSource, "SHM").FormatString = DungChung.Bien.FormatString[1];
            colVGB.DataBindings.Add("Text", DataSource, "VGB").FormatString = DungChung.Bien.FormatString[1];
            colHIV.DataBindings.Add("Text", DataSource, "HIV").FormatString = DungChung.Bien.FormatString[1];
           // colAminra.DataBindings.Add("Text", DataSource, "Aminra").FormatString = DungChung.Bien.FormatString[1];
            //colKhacXN.DataBindings.Add("Text", DataSource, "KhacXN").FormatString = DungChung.Bien.FormatString[1];
            colBoBot.DataBindings.Add("Text", DataSource, "BoBot").FormatString = DungChung.Bien.FormatString[1];
            colNaoThai.DataBindings.Add("Text", DataSource, "NaoThai").FormatString = DungChung.Bien.FormatString[1];
            colTBe.DataBindings.Add("Text", DataSource, "TamBe").FormatString = DungChung.Bien.FormatString[1];
            colDVKhac.DataBindings.Add("Text", DataSource, "Khac").FormatString = DungChung.Bien.FormatString[1];
            colBHYT.DataBindings.Add("Text", DataSource, "BHYT").FormatString = DungChung.Bien.FormatString[1];
            colGDSK.DataBindings.Add("Text", DataSource, "GDSK").FormatString = DungChung.Bien.FormatString[1];
            colKSK.DataBindings.Add("Text", DataSource, "KDK").FormatString = DungChung.Bien.FormatString[1];
            colNG.DataBindings.Add("Text", DataSource, "NG").FormatString = DungChung.Bien.FormatString[1];
            colVienPhi.DataBindings.Add("Text", DataSource, "VP").FormatString = DungChung.Bien.FormatString[1];
            colTong.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];

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
          //  colAminraTC.DataBindings.Add("Text", DataSource, "Aminra").FormatString = DungChung.Bien.FormatString[1];
            //colKhacXNTC.DataBindings.Add("Text", DataSource, "KhacXN").FormatString = DungChung.Bien.FormatString[1];
            colBoBotTC.DataBindings.Add("Text", DataSource, "BoBot").FormatString = DungChung.Bien.FormatString[1];
            colNaoThaiTC.DataBindings.Add("Text", DataSource, "NaoThai").FormatString = DungChung.Bien.FormatString[1];
            colTBeTC.DataBindings.Add("Text", DataSource, "TamBe").FormatString = DungChung.Bien.FormatString[1];
            colDVKhacTC.DataBindings.Add("Text", DataSource, "Khac").FormatString = DungChung.Bien.FormatString[1];
            colBHYTTC.DataBindings.Add("Text", DataSource, "BHYT").FormatString = DungChung.Bien.FormatString[1];
            colGDSKTC.DataBindings.Add("Text", DataSource, "GDSK").FormatString = DungChung.Bien.FormatString[1];
            colKSKTC.DataBindings.Add("Text", DataSource, "KSK").FormatString = DungChung.Bien.FormatString[1];
            colNGTC.DataBindings.Add("Text", DataSource, "NG").FormatString = DungChung.Bien.FormatString[1];
            colVienPhiTC.DataBindings.Add("Text", DataSource, "VP").FormatString = DungChung.Bien.FormatString[1];
            colTongTC.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];

            GroupHeader1.GroupFields.Add(new GroupField("NThang"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colGiamDoc.Text = DungChung.Bien.NguoiLapBieu;
            double _tt = 0, _ck = 0;
            if (!string.IsNullOrEmpty(Tong.Value.ToString()))
            {
                _tt = Convert.ToDouble(Tong.Value);
            }
            if (!string.IsNullOrEmpty(CK.Value.ToString()))
            {
                 _ck = Convert.ToDouble(CK.Value);
            }
            if (_tt == 0)
            {
                txtTM.Text = "";
            }
            else
            {
                txtTM.Text =_tt.ToString("#,##") + " ("+ DungChung.Ham.DocTienBangChu((_tt), " đồng).");
                    
            }
            if (_ck != 0)
            {
                txtCK.Text = _ck.ToString("#,##") +"   "+ BL.Value.ToString();
            }
            if (_tt != 0 || _ck != 0)
            {
                txtTC.Text =(_tt +_ck).ToString("#,##") +" (" + DungChung.Ham.DocTienBangChu((_tt + _ck), " đồng).");
            }
            
        }


        private void colNTN_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            Detail.Visible = false;
        }

        private void colNTNgh_BeforePrint(object sender, CancelEventArgs e)
        {
         
        }
       
    
        private void colGDSKgh_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }
         private void colTonggh_BeforePrint(object sender, CancelEventArgs e)
        {
          
        }
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }

        private void colKSKgh_BeforePrint(object sender, CancelEventArgs e)
        {

        }
        
    }
}
