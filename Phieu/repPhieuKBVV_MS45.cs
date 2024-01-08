using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repPhieuKBVV_MS45 : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuKBVV_MS45()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27022")
            {
                xrTableCell6.Text = "";
                xrTableCell7.Text = "";
            }
            if (DungChung.Bien.MaBV == "30009")
                txtSoVV.Visible = false;
            txtTenCQCQ.Text = txtTenCQCQ1.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtTenCQ.Text = txtTenCQ1.Text = DungChung.Bien.TenCQ.ToUpper();

            if (DungChung.Bien.MaBV == "24012")
            {
                SBChung.Visible = false;
                SB24012.Visible = true;
            }

            //Giới tính
            if (GTinh.Value != null && GTinh.Value.ToString() == "1")
            {
                colNam.Text = colNam1.Text = "X";
            }
            else if (GTinh.Value != null && GTinh.Value.ToString() == "0")
            {
                colNu.Text = colNu1.Text = "X";
            }

            //Đối tượng
            if (DTuong.Value != null)
            {
                switch (DTuong.Value.ToString())
                {
                    case "BHYT":
                        colBHYT.Text = colBHYT1.Text = "X";
                        break;
                    case "Dịch vụ":
                        colThuPhi.Text = colThuPhi1.Text = "X";
                        break;
                    case "Miễn":
                        colMien.Text = colMien1.Text = "X";
                        break;
                    case "Khác":
                        colKhac.Text = colKhac1.Text = "X";
                        break;
                }
            }

            //Ngoại kiều
            if (NgoaiKieu.Value != null && NgoaiKieu.Value.ToString() == "0")
            {
                txtngoaikieu.Text = txtngoaikieu1.Text = "  ";

            }
            else
            {
                if (NgoaiKieu.Value != null && NgoaiKieu.Value.ToString() == "1")
                    txtngoaikieu.Text = txtngoaikieu1.Text = "Việt kiều";
            }

            if (MaNK.Value != null && MaNK.Value.ToString().Length == 1)
            {
                colNK1.Text = colNK11.Text = "0";
                colNK2.Text = colNK12.Text = MaNK.Value.ToString().Substring(0, 1);
            }
            if (MaNK.Value != null && MaNK.Value.ToString().Length >= 2)
            {
                colNK1.Text = colNK11.Text = MaNK.Value.ToString().Substring(0, 1);
                colNK2.Text = colNK12.Text = MaNK.Value.ToString().Substring(1, 1);
            }

            // Số thẻ BHYT
            if (SThe.Value != null && SThe.Value.ToString() != "" && SThe.Value.ToString().Length >= 15)
            {
                col1.Text = col11.Text = SThe.Value.ToString().Substring(0, 2);
                col2.Text = col12.Text = SThe.Value.ToString().Substring(2, 1);
                col3.Text = col13.Text = SThe.Value.ToString().Substring(3, 2);
                col4.Text = col14.Text = SThe.Value.ToString().Substring(5, 2);
                col5.Text = col15.Text = SThe.Value.ToString().Substring(7, 3);
                col6.Text = col16.Text = SThe.Value.ToString().Substring(10, 5);
            }

            //Ngày tháng năm sinh
            if (NgaySinh.Value != null && NgaySinh.Value.ToString().Length >= 2)
            {
                colSN1.Text = colNS11.Text = NgaySinh.Value.ToString().Substring(0, 1);
                colSN2.Text = colNS12.Text = NgaySinh.Value.ToString().Substring(1, 1);
            }
            if (ThangSinh.Value != null & ThangSinh.Value.ToString().Length >= 2)
            {
                colSN3.Text = colNS13.Text = ThangSinh.Value.ToString().Substring(0, 1);
                colSN4.Text = colNS14.Text = ThangSinh.Value.ToString().Substring(1, 1);
            }
            if (NSinh.Value != null && NSinh.Value.ToString().Length >= 4)
            {
                string nams = NSinh.Value.ToString();
                colSN5.Text = colNS15.Text = NSinh.Value.ToString().Substring(0, 1);
                colSN6.Text = colNS16.Text = NSinh.Value.ToString().Substring(1, 1);
                colSN7.Text = colNS17.Text = NSinh.Value.ToString().Substring(2, 1);
                colSN8.Text = colNS18.Text = NSinh.Value.ToString().Substring(3, 1);
            }

            //Tuổi
            if (Tuoi.Value != null && Tuoi.Value.ToString().Length == 1)
            {
                colTuoi1.Text = colTuoi11.Text = "0";
                colTuoi2.Text = colTuoi12.Text = Tuoi.Value.ToString().Substring(0, 1);
            }

            if (Tuoi.Value != null && Tuoi.Value.ToString().Length >= 2)
            {
                colTuoi1.Text = colTuoi11.Text = Tuoi.Value.ToString().Substring(0, 1);
                colTuoi2.Text = colTuoi12.Text = Tuoi.Value.ToString().Substring(1, 1);
            }
            if (Tuoi.Value != null && (Tuoi.Value.ToString().ToLower().Contains("tháng") || Tuoi.Value.ToString().ToLower().Contains("ngày")) && Tuoi.Value.ToString().Substring(1, 1) == " ")
            {
                colTuoi1.Text = colTuoi11.Text = "0";
                colTuoi2.Text = colTuoi12.Text = Tuoi.Value.ToString().Substring(0, 1);
            }
            if (Tuoi.Value != null && Tuoi.Value.ToString().Contains("tháng"))
            {
                labTuoi.Text = labTuoi1.Text = "Tháng";
            }
            if (Tuoi.Value != null && Tuoi.Value.ToString().Contains("ngày"))
            {
                labTuoi.Text = labTuoi1.Text = "Ngày";
            }

            //Mã nghề nghiệp
            if (MaNN.Value != null && MaNN.Value.ToString().Length == 1)
            {
                colNN1.Text = colNN11.Text = "0";
                colNN2.Text = colNN12.Text = MaNN.Value.ToString().Substring(0, 1);
            }
            if (MaNN.Value != null && MaNN.Value.ToString().Length >= 2)
            {
                colNN1.Text = colNN11.Text = MaNN.Value.ToString().Substring(0, 1);
                colNN2.Text = colNN12.Text = MaNN.Value.ToString().Substring(1, 1);
            }
            //if (MaNN.Value != null && MaNN.Value.ToString().Length >= 2)
            //{
            //    colNN1.Text = MaNN.Value.ToString().Substring(0, 1);
            //    colNN2.Text = MaNN.Value.ToString().Substring(1, 1);
            //}

            //Mã dân tộc
            if (MaDT.Value != null && MaDT.Value.ToString().Length == 1)
            {
                colDT1.Text = colDT11.Text = "0";
                colDT2.Text = colDT12.Text = MaDT.Value.ToString().Substring(0, 1);
            }
            if (MaDT.Value != null && MaDT.Value.ToString().Length >= 2)
            {
                colDT1.Text = colDT11.Text = MaDT.Value.ToString().Substring(0, 1);
                colDT2.Text = colDT12.Text = MaDT.Value.ToString().Substring(1, 1);
            }

            if (DungChung.Bien.MaBV == "27021")
                lblBSDtri.Visible = true;
            else
                lblBSDtri.Visible = false;
            if (DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023")
            {
                tblChung.Visible = false;
                tbl27023.Visible = true;
                if (DungChung.Bien.MaBV == "27023")
                {
                    xrTableCell6.Text = "";
                    xrTableCell7.Text = "";
                }
            }


            // Mã huyện tỉnh
            //24012
            if (MaHuyen.Value != null && MaHuyen.Value.ToString().Length == 1)
            {
                maHuyen0.Text = "0";
                maHuyen1.Text = "0";
                maHuyen2.Text = MaHuyen.Value.ToString().Substring(0, 1);
            }
            else if (MaHuyen.Value != null && MaHuyen.Value.ToString().Length == 2)
            {
                maHuyen0.Text = "0";
                maHuyen1.Text = MaHuyen.Value.ToString().Substring(0, 1);
                maHuyen2.Text = MaHuyen.Value.ToString().Substring(1, 1);
            }
            else if (MaHuyen.Value != null && MaHuyen.Value.ToString().Length >= 3)
            {
                maHuyen0.Text = MaHuyen.Value.ToString().Substring(0, 1);
                maHuyen1.Text = MaHuyen.Value.ToString().Substring(1, 1);
                maHuyen2.Text = MaHuyen.Value.ToString().Substring(2, 1);
            }
            if (MaTinh.Value != null && MaTinh.Value.ToString().Length == 1)
            {
                maTinh0.Text = "0";
                maTinh1.Text = MaTinh.Value.ToString().Substring(0, 1);
            }
            if (MaTinh.Value != null && MaTinh.Value.ToString().Length >= 2)
            {
                maTinh0.Text = MaTinh.Value.ToString().Substring(0, 1);
                maTinh1.Text = MaTinh.Value.ToString().Substring(1, 1);
            }

            //Địa chỉ
            if (DChi.Value != null && DChi.Value.ToString() != "")
            {
                char[] splitChar = new char[] { ',', '-' };
                var dchi = DChi.Value.ToString().Split(splitChar);
                switch (dchi.Count())
                {
                    case 0:
                        break;
                    case 1:
                        lblTinh.Text = dchi[0].ToString();
                        break;
                    case 2:
                        lblTinh.Text = dchi[1].ToString();
                        lblHuyen.Text = dchi[0].ToString();
                        break;
                    case 3:
                        lblTinh.Text = dchi[2].ToString();
                        lblHuyen.Text = dchi[1].ToString();
                        lblXa.Text = dchi[0].ToString();
                        break;
                    default:
                        lblTinh.Text = dchi[dchi.Count() - 1].ToString();
                        lblHuyen.Text = dchi[dchi.Count() - 2].ToString();
                        lblXa.Text = dchi[dchi.Count() - 3].ToString();
                        lblThon.Text = dchi[dchi.Count() - 4].ToString();
                        break;
                }
            }

            //1.10 Hạn thẻ NHYT
            if (HanBHDen.Value != null && HanBHDen.Value.ToString() != "")
            {
                var hanBH = HanBHDen.Value.ToString().Split('/');
                if (hanBH[2].Length > 4)
                {
                    hanBH[2] = hanBH[2].Remove(4);
                }
                lblHanBH.Text = hanBH[0] + " tháng " + hanBH[1] + " năm " + hanBH[2];
            }
            //1.12 Đến khám
            if (NgayKham.Value != null && NgayKham.Value.ToString() != "")
            {
                var ngayKham = NgayKham.Value.ToString().Split(',');
                lblNgayKham.Text = ngayKham[0] + " phút" + " \t " + ngayKham[1];
            }
        }

    }
}
