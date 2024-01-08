using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BaoCaoThuVienPhiThang : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BaoCaoThuVienPhiThang()
        {
            InitializeComponent();
           
        }
        public rep_BaoCaoThuVienPhiThang(int mau)
        {
            InitializeComponent();
            _mau = mau;

        }
        int _mau = 1;
        internal void BindingData()
        {
            celNhom.DataBindings.Add("Text", DataSource, "TenNhom");
            celTenDV.DataBindings.Add("Text", DataSource, "TenTieuChi");
            if (_mau == 1)
            {
                cel1.DataBindings.Add("Text", DataSource, "DichVuKhamBenh").FormatString = DungChung.Bien.FormatString[0];                
                cel2.DataBindings.Add("Text", DataSource, "DichVuKhoaA").FormatString = DungChung.Bien.FormatString[1];
                cel3.DataBindings.Add("Text", DataSource, "DichVuKhoaB").FormatString = DungChung.Bien.FormatString[0];
                cel4.DataBindings.Add("Text", DataSource, "DichVuKhoaC").FormatString = DungChung.Bien.FormatString[1];
                cel5.DataBindings.Add("Text", DataSource, "TyLeKhamBenh").FormatString = DungChung.Bien.FormatString[0];               
                cel6.DataBindings.Add("Text", DataSource, "TyLeKhoaA").FormatString = DungChung.Bien.FormatString[1];
                cel7.DataBindings.Add("Text", DataSource, "TyLeKhoaB").FormatString = DungChung.Bien.FormatString[0];
                cel8.DataBindings.Add("Text", DataSource, "TyLeKhoaC").FormatString = DungChung.Bien.FormatString[1];
                cel9.DataBindings.Add("Text", DataSource, "DichVuNoiTruKB").FormatString = DungChung.Bien.FormatString[0];
                cel10.DataBindings.Add("Text", DataSource, "TyLeNoiTruKB").FormatString = DungChung.Bien.FormatString[0];

                cel1T.DataBindings.Add("Text", DataSource, "DichVuKhamBenh").FormatString = DungChung.Bien.FormatString[0];                
                cel2T.DataBindings.Add("Text", DataSource, "DichVuKhoaA").FormatString = DungChung.Bien.FormatString[1];
                cel3T.DataBindings.Add("Text", DataSource, "DichVuKhoaB").FormatString = DungChung.Bien.FormatString[0];
                cel4T.DataBindings.Add("Text", DataSource, "DichVuKhoaC").FormatString = DungChung.Bien.FormatString[1];
                cel5T.DataBindings.Add("Text", DataSource, "TyLeKhamBenh").FormatString = DungChung.Bien.FormatString[0];                
                cel6T.DataBindings.Add("Text", DataSource, "TyLeKhoaA").FormatString = DungChung.Bien.FormatString[1];
                cel7T.DataBindings.Add("Text", DataSource, "TyLeKhoaB").FormatString = DungChung.Bien.FormatString[0];
                cel8T.DataBindings.Add("Text", DataSource, "TyLeKhoaC").FormatString = DungChung.Bien.FormatString[1];
                cel9T.DataBindings.Add("Text", DataSource, "DichVuNoiTruKB").FormatString = DungChung.Bien.FormatString[1];
                cel10T.DataBindings.Add("Text", DataSource, "TyLeNoiTruKB").FormatString = DungChung.Bien.FormatString[0];


                cel1TT.DataBindings.Add("Text", DataSource, "DichVuKhamBenhTT").FormatString = DungChung.Bien.FormatString[0];               
                cel2TT.DataBindings.Add("Text", DataSource, "DichVuKhoaATT").FormatString = DungChung.Bien.FormatString[1];
                cel3TT.DataBindings.Add("Text", DataSource, "DichVuKhoaBTT").FormatString = DungChung.Bien.FormatString[0];
                cel4TT.DataBindings.Add("Text", DataSource, "DichVuKhoaCTT").FormatString = DungChung.Bien.FormatString[1];
                cel5TT.DataBindings.Add("Text", DataSource, "TyLeKhamBenhTT").FormatString = DungChung.Bien.FormatString[0];                
                cel6TT.DataBindings.Add("Text", DataSource, "TyLeKhoaATT").FormatString = DungChung.Bien.FormatString[1];
                cel7TT.DataBindings.Add("Text", DataSource, "TyLeKhoaBTT").FormatString = DungChung.Bien.FormatString[0];
                cel8TT.DataBindings.Add("Text", DataSource, "TyLeKhoaCTT").FormatString = DungChung.Bien.FormatString[1];
                cel9TT.DataBindings.Add("Text", DataSource, "DichVuNoiTruKBTT").FormatString = DungChung.Bien.FormatString[0];
                cel10TT.DataBindings.Add("Text", DataSource, "TyLeNoiTruKBTT").FormatString = DungChung.Bien.FormatString[1];

                cel1TTT.DataBindings.Add("Text", DataSource, "DichVuKhamBenhTT").FormatString = DungChung.Bien.FormatString[0];               
                cel2TTT.DataBindings.Add("Text", DataSource, "DichVuKhoaATT").FormatString = DungChung.Bien.FormatString[1];
                cel3TTT.DataBindings.Add("Text", DataSource, "DichVuKhoaBTT").FormatString = DungChung.Bien.FormatString[0];
                cel4TTT.DataBindings.Add("Text", DataSource, "DichVuKhoaCTT").FormatString = DungChung.Bien.FormatString[1];
                cel5TTT.DataBindings.Add("Text", DataSource, "TyLeKhamBenhTT").FormatString = DungChung.Bien.FormatString[0];                
                cel6TTT.DataBindings.Add("Text", DataSource, "TyLeKhoaATT").FormatString = DungChung.Bien.FormatString[1];
                cel7TTT.DataBindings.Add("Text", DataSource, "TyLeKhoaBTT").FormatString = DungChung.Bien.FormatString[0];
                cel8TTT.DataBindings.Add("Text", DataSource, "TyLeKhoaCTT").FormatString = DungChung.Bien.FormatString[1];
                cel9TTT.DataBindings.Add("Text", DataSource, "DichVuNoiTruKBTT").FormatString = DungChung.Bien.FormatString[0];
                cel10TTT.DataBindings.Add("Text", DataSource, "TyLeNoiTruKBTT").FormatString = DungChung.Bien.FormatString[1];

                cel1TTT.Summary.FormatString = DungChung.Bien.FormatString[0];
                cel2TTT.Summary.FormatString = DungChung.Bien.FormatString[1];
                cel3TTT.Summary.FormatString = DungChung.Bien.FormatString[0];
                cel4TTT.Summary.FormatString = DungChung.Bien.FormatString[1];
                cel5TTT.Summary.FormatString = DungChung.Bien.FormatString[0];
                cel6TTT.Summary.FormatString = DungChung.Bien.FormatString[1];
                cel7TTT.Summary.FormatString = DungChung.Bien.FormatString[0];
                cel8TTT.Summary.FormatString = DungChung.Bien.FormatString[1];
                cel9TTT.Summary.FormatString = DungChung.Bien.FormatString[0];
                cel10TTT.Summary.FormatString = DungChung.Bien.FormatString[1];
            }
            else
            {
                cel1.DataBindings.Add("Text", DataSource, "BHKhamBenh").FormatString = DungChung.Bien.FormatString[0];               
                cel2.DataBindings.Add("Text", DataSource, "BHKhoaA").FormatString = DungChung.Bien.FormatString[1];
                cel3.DataBindings.Add("Text", DataSource, "BHKhoaB").FormatString = DungChung.Bien.FormatString[0];
                cel4.DataBindings.Add("Text", DataSource, "BHKhoaC").FormatString = DungChung.Bien.FormatString[1];
                cel5.DataBindings.Add("Text", DataSource, "TTKhamBenh").FormatString = DungChung.Bien.FormatString[0];               
                cel6.DataBindings.Add("Text", DataSource, "TTKhoaA").FormatString = DungChung.Bien.FormatString[1];
                cel7.DataBindings.Add("Text", DataSource, "TTKhoaB").FormatString = DungChung.Bien.FormatString[0];               
                cel8.DataBindings.Add("Text", DataSource, "TTKhoaC").FormatString = DungChung.Bien.FormatString[1];
                cel9.DataBindings.Add("Text", DataSource, "BHNoiTruKB").FormatString = DungChung.Bien.FormatString[0];
                cel10.DataBindings.Add("Text", DataSource, "TTNoiTruKB").FormatString = DungChung.Bien.FormatString[1];

                cel1T.DataBindings.Add("Text", DataSource, "BHKhamBenh").FormatString = DungChung.Bien.FormatString[0];               
                cel2T.DataBindings.Add("Text", DataSource, "BHKhoaA").FormatString = DungChung.Bien.FormatString[1];
                cel3T.DataBindings.Add("Text", DataSource, "BHKhoaB").FormatString = DungChung.Bien.FormatString[0];
                cel4T.DataBindings.Add("Text", DataSource, "BHKhoaC").FormatString = DungChung.Bien.FormatString[1];
                cel5T.DataBindings.Add("Text", DataSource, "TTKhamBenh").FormatString = DungChung.Bien.FormatString[0];                
                cel6T.DataBindings.Add("Text", DataSource, "TTKhoaA").FormatString = DungChung.Bien.FormatString[1];
                cel7T.DataBindings.Add("Text", DataSource, "TTKhoaB").FormatString = DungChung.Bien.FormatString[0];
                cel8T.DataBindings.Add("Text", DataSource, "TTKhoaC").FormatString = DungChung.Bien.FormatString[1];
                cel9T.DataBindings.Add("Text", DataSource, "BHNoiTruKB").FormatString = DungChung.Bien.FormatString[0];
                cel10T.DataBindings.Add("Text", DataSource, "TTNoiTruKB").FormatString = DungChung.Bien.FormatString[1];

                cel1TT.DataBindings.Add("Text", DataSource, "BHKhamBenhTT").FormatString = DungChung.Bien.FormatString[0];
                cel2TT.DataBindings.Add("Text", DataSource, "BHKhoaATT").FormatString = DungChung.Bien.FormatString[1];
                cel3TT.DataBindings.Add("Text", DataSource, "BHKhoaBTT").FormatString = DungChung.Bien.FormatString[0];
                cel4TT.DataBindings.Add("Text", DataSource, "BHKhoaCTT").FormatString = DungChung.Bien.FormatString[1];
                cel5TT.DataBindings.Add("Text", DataSource, "TTKhamBenhTT").FormatString = DungChung.Bien.FormatString[0];
                cel6TT.DataBindings.Add("Text", DataSource, "TTKhoaATT").FormatString = DungChung.Bien.FormatString[1];
                cel7TT.DataBindings.Add("Text", DataSource, "TTKhoaBTT").FormatString = DungChung.Bien.FormatString[0];
                cel8TT.DataBindings.Add("Text", DataSource, "TTKhoaCTT").FormatString = DungChung.Bien.FormatString[1];
                cel9TT.DataBindings.Add("Text", DataSource, "BHNoiTruKBTT").FormatString = DungChung.Bien.FormatString[0];
                cel10TT.DataBindings.Add("Text", DataSource, "TTNoiTruKBTT").FormatString = DungChung.Bien.FormatString[1];

                cel1TTT.DataBindings.Add("Text", DataSource, "BHKhamBenhTT").FormatString = DungChung.Bien.FormatString[0];
                cel2TTT.DataBindings.Add("Text", DataSource, "BHKhoaATT").FormatString = DungChung.Bien.FormatString[1];
                cel3TTT.DataBindings.Add("Text", DataSource, "BHKhoaBTT").FormatString = DungChung.Bien.FormatString[0];
                cel4TTT.DataBindings.Add("Text", DataSource, "BHKhoaCTT").FormatString = DungChung.Bien.FormatString[1];
                cel5TTT.DataBindings.Add("Text", DataSource, "TTKhamBenhTT").FormatString = DungChung.Bien.FormatString[0];
                cel6TTT.DataBindings.Add("Text", DataSource, "TTKhoaATT").FormatString = DungChung.Bien.FormatString[1];
                cel7TTT.DataBindings.Add("Text", DataSource, "TTKhoaBTT").FormatString = DungChung.Bien.FormatString[0];
                cel8TTT.DataBindings.Add("Text", DataSource, "TTKhoaCTT").FormatString = DungChung.Bien.FormatString[1];
                cel9TTT.DataBindings.Add("Text", DataSource, "BHNoiTruKBTT").FormatString = DungChung.Bien.FormatString[0];
                cel10TTT.DataBindings.Add("Text", DataSource, "TTNoiTruKBTT").FormatString = DungChung.Bien.FormatString[1];


                cel1TTT.Summary.FormatString = DungChung.Bien.FormatString[0];
                cel2TTT.Summary.FormatString = DungChung.Bien.FormatString[1];
                cel3TTT.Summary.FormatString = DungChung.Bien.FormatString[0];
                cel4TTT.Summary.FormatString = DungChung.Bien.FormatString[1];
                cel5TTT.Summary.FormatString = DungChung.Bien.FormatString[0];
                cel6TTT.Summary.FormatString = DungChung.Bien.FormatString[1];
                cel7TTT.Summary.FormatString = DungChung.Bien.FormatString[0];
                cel8TTT.Summary.FormatString = DungChung.Bien.FormatString[1];
                cel9TTT.Summary.FormatString = DungChung.Bien.FormatString[0];
                cel10TTT.Summary.FormatString = DungChung.Bien.FormatString[1];
            }
           
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            celTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
           
            if (_mau == 2)
            {
                celg1.Text = "Bảo hiểm y tế";
                celg2.Text = "Tổng cộng";
                cel5TT.Text = "";
            }
      
        }

     
        private void PageFooter_BeforePrint(object sender, CancelEventArgs e)
        {
           
            if (_mau == 1)
                celSTT_Trai.Visible = true;
            else
                celstt_Phai.Visible = true;
        }
        int row = 0;
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
       {
           //if (this.GetCurrentColumnValue("Row") != null)
           //{
           //    int r = Convert.ToInt32(this.GetCurrentColumnValue("Row"));
           //    celNhom.RowSpan = r;
           //}
           //else
           //    celNhom.RowSpan = 1;
            //-----------------------------
            //if (this.GetCurrentColumnValue("TenNhom") != null)
            //{
            //    string tennhom = this.GetCurrentColumnValue("TenNhom").ToString();
            //    if (tennhom == "")

            //        celNhom.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left));
            //    else
            //        celNhom.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top));
              
            //}
           row++;
           if (this.GetCurrentColumnValue("Row") != null)
           {
               int r = Convert.ToInt32(this.GetCurrentColumnValue("Row"));
               if (row == r)
               {
                   celNhom.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom));
                   row = 0;
               }
               else
                   celNhom.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left));
           }
           else
               celNhom.Borders = ((DevExpress.XtraPrinting.BorderSide)(DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom));
        }
    }
}
