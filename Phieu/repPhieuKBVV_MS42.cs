using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repPhieuKBVV_MS42 : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuKBVV_MS42()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if(DungChung.Bien.MaBV=="30009")
            txtSoVV.Visible = false;
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            if (GTinh.Value !=null && GTinh.Value.ToString() == "1")
            {
                colNam.Text = "X";
            }
            else  {
                if (GTinh.Value != null && GTinh.Value.ToString() == "0")
                    colNu.Text = "X";
                    }
            if (DTuong.Value != null && DTuong.Value.ToString() == "BHYT")
            {
                colBHYT.Text = "X";

            }
            else
            {
                if (DTuong.Value != null &&  DTuong.Value.ToString() == "Dịch vụ")
                {
                    colThuPhi.Text = "X";
                }
                else
                {
                    if (DTuong.Value != null && DTuong.Value.ToString() == "Miễn")
                    {
                        colMien.Text = "X";
                    }
                    else
                    {
                        if (DTuong.Value != null && DTuong.Value.ToString() == "Khác")
                        {
                            colKhac.Text = "X";
                        }
                    }
                }
            }
            if (NgoaiKieu.Value != null && NgoaiKieu.Value.ToString() == "0")
            {
               txtngoaikieu.Text = "  ";

            }
            else
            {
                if (NgoaiKieu.Value != null && NgoaiKieu.Value.ToString() == "1")
                    txtngoaikieu.Text = "Việt kiều";
            }

            if (SThe.Value != null && SThe.Value.ToString() != "" && SThe.Value.ToString().Length>=15) {
                col1.Text = SThe.Value.ToString().Substring(0, 2);
                col2.Text = SThe.Value.ToString().Substring(2, 1);
                col3.Text = SThe.Value.ToString().Substring(3, 2);
                col4.Text = SThe.Value.ToString().Substring(5,2);
                col5.Text = SThe.Value.ToString().Substring(7,3);
                col6.Text = SThe.Value.ToString().Substring(10,5);
            }
            if (NgaySinh.Value != null && NgaySinh.Value.ToString().Length >= 2)
            {
                colSN1.Text = NgaySinh.Value.ToString().Substring(0, 1);
                colSN2.Text = NgaySinh.Value.ToString().Substring(1, 1);
            }
            if (ThangSinh.Value != null & ThangSinh.Value.ToString().Length >= 2)
            {
                colSN3.Text = ThangSinh.Value.ToString().Substring(0, 1);
                colSN4.Text = ThangSinh.Value.ToString().Substring(1, 1);
            }
            if (NSinh.Value != null && NSinh.Value.ToString().Length >= 4)
            {
                string nams = NSinh.Value.ToString();
                colSN5.Text = NSinh.Value.ToString().Substring(0, 1);
                colSN6.Text = NSinh.Value.ToString().Substring(1, 1);
                colSN7.Text = NSinh.Value.ToString().Substring(2, 1);
                colSN8.Text = NSinh.Value.ToString().Substring(3, 1);
            }
            if (Tuoi.Value != null && Tuoi.Value.ToString().Length == 1)
            {
                colTuoi1.Text = "0";
                colTuoi2.Text = Tuoi.Value.ToString().Substring(0, 1);
            }
            if (Tuoi.Value != null && Tuoi.Value.ToString().Length >= 2)
            {
                colTuoi1.Text = Tuoi.Value.ToString().Substring(0, 1);
                colTuoi2.Text = Tuoi.Value.ToString().Substring(1, 1);
            }
            if (MaNN.Value != null && MaNN.Value.ToString()!="")
            {
                if (MaNN.Value.ToString().Length == 1) { 
                colNN1.Text = "0";
                colNN2.Text = MaNN.Value.ToString().Substring(0, 1);
                }
                else
                {
                    colNN1.Text = MaNN.Value.ToString().Substring(0, 1);
                    colNN2.Text = MaNN.Value.ToString().Substring(1, 1);
                }
            }
            if (Tuoi.Value != null && Tuoi.Value.ToString().Contains("tháng"))
            {
                labTuoi.Text = "Tháng";
            }
            if (Tuoi.Value != null && Tuoi.Value.ToString().Contains("ngày"))
            {
                labTuoi.Text = "Ngày";
            }
        
            if (MaDT.Value != null && MaDT.Value.ToString().Length == 1)
            {
                colDT1.Text = "0";
                colDT2.Text = MaDT.Value.ToString().Substring(0, 1);
            }
            if (MaDT.Value != null && MaDT.Value.ToString().Length >= 2)
            {
                colDT1.Text = MaDT.Value.ToString().Substring(0, 1);
                colDT2.Text = MaDT.Value.ToString().Substring(1, 1);
            }

            if (MaNK.Value != null && MaNK.Value.ToString().Length == 1)
            {
                colNK1.Text = "0";
                colNK2.Text = MaNK.Value.ToString().Substring(0, 1);
            }
            if (MaNK.Value != null && MaNK.Value.ToString().Length >= 2)
            {
                colNK1.Text = MaNK.Value.ToString().Substring(0, 1);
                colNK2.Text = MaNK.Value.ToString().Substring(1, 1);
            }
            if (DungChung.Bien.MaBV == "30003")
            {
                //if (this.MCS.Value != null && this.MCS.Value.ToString() == "30003")
                //{

                //    colHuyen1.Text = "0";
                //    colHuyen2.Text = "3";
                //    colTinh0.Text = "1";
                //    colTinh1.Text = "0";
                //    colTinh2.Text = "7";
                //    colTinh0.Visible = true;
                //    colTinh0.Visible = true;
                //}
                //else
                //{
                
                if (MaHuyen.Value != null && MaHuyen.Value.ToString().Length == 1)
                {
                    colHuyen1.Text = "0";
                    colHuyen2.Text = MaHuyen.Value.ToString().Substring(0, 1);
                }
                if (MaHuyen.Value != null && MaHuyen.Value.ToString().Length >= 2)
                {
                    colHuyen1.Text = MaHuyen.Value.ToString().Substring(0, 1);
                    colHuyen2.Text = MaHuyen.Value.ToString().Substring(1, 1);
                }
                //if (this.MCS.Value != null && this.MCS.Value.ToString().Length >= 2 && this.MCS.Value.ToString().Substring(0, 2) == "30")
                //{
                //    colTinh0.Text = "1";
                //    colTinh1.Text = "0";
                //    colTinh2.Text = "7";
                //    colTinh0.Visible = true;
                //}
                //else
                //{
                if (MaTinh.Value != null && MaTinh.Value.ToString().Length == 1)
                {
                    colTinh1.Text = "0";
                    colTinh2.Text = MaTinh.Value.ToString().Substring(0, 1);
                }
                if (MaTinh.Value != null && MaTinh.Value.ToString().Length >= 2)
                {
                    colTinh1.Text = MaTinh.Value.ToString().Substring(0, 1);
                    colTinh2.Text = MaTinh.Value.ToString().Substring(1, 1);
                }
                //}
                    
                //}
            }
        }

    }
}
