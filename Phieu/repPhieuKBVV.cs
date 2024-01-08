using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repPhieuKBVV : DevExpress.XtraReports.UI.XtraReport
    {
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public repPhieuKBVV()
        {
            InitializeComponent();

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if(DungChung.Bien.MaBV == "24272")
            {
                xrTable17.Visible = false;
                bmi_24272.Visible = true;
            }    
            if (DungChung.Bien.MaBV == "30002")
            {
                NhietDo.Type = typeof(String);
                xrTableCell47.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Left;
                xrTableCell48.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                xrTableCell49.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right;
            }
            if (DungChung.Bien.MaBV == "14017")
            {
                xrLabel132.Visible = false;
            }
            if (DungChung.Bien.MaBV == "27022")
            {
                xrTableCell11.Text = "";
                xrTableCell12.Text = "";
            }
            if (DungChung.Bien.MaBV == "14018")
            {
                txtTenCQCQ.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                txtTenCQ.Font = new Font("Times New Roman", 12, FontStyle.Bold);

            }

            //BMI && Chieu Cao
            if (DungChung.Bien.MaBV == "12121" || DungChung.Bien.MaBV == "26007")
            {

                RowChieuCao.Visible = true;
                if (DungChung.Bien.MaBV == "26027")
                {
                    xrTableRow27.Visible = RowBMI.Visible = true;
                    xrTableCell2.Text = "BMI:";
                    xrTableCell5.Text = "Kg/m²";
                }
            }
            else if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                RowChieuCao.Visible = true;
                RowBMI.Visible = true;

            }
            else if (DungChung.Bien.MaBV == "20001")
            {
                RowChieuCao.Visible = true;

            }
            else
            {
                xrTableRow27.Visible = RowBMI.Visible = false;
                RowChieuCao.Visible = false;
            }//

            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                xrLabel53.Text = "3. Tóm tắt kết quả lâm sàng và cận lâm sàng:";

            if (DungChung.Bien.MaBV == "30009" || DungChung.Bien.MaBV == "04018")
                txtSoVV.Visible = false;
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();

            if (GTinh.Value != null && GTinh.Value.ToString() == "1")
            {
                colNam2.Text = colNam1.Text = colNam.Text = "X";
            }
            else
            {
                if (GTinh.Value != null && GTinh.Value.ToString() == "0")
                    colNu2.Text = colNu1.Text = colNu.Text = "X";
            }

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

            if (NgoaiKieu.Value != null && NgoaiKieu.Value.ToString() == "0")
            {
                txtngoaikieu.Text = "  ";
                xrLabel139.Text = "";
            }
            else
            {
                if (NgoaiKieu.Value != null && NgoaiKieu.Value.ToString() == "1")
                {
                    txtngoaikieu.Text = "Việt kiều";
                    xrLabel139.Text = "";
                }

            }
            xrPictureBox1.Image = DungChung.Ham.GetLogo();
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = false;
                SubBand6.Visible = true;
            }

            if (SThe.Value != null && SThe.Value.ToString() != "" && SThe.Value.ToString().Length >= 15)
            {
                col101.Text = col1.Text = col201.Text = SThe.Value.ToString().Substring(0, 2);
                col102.Text = col2.Text = col202.Text = SThe.Value.ToString().Substring(2, 1);
                col103.Text = col3.Text = col203.Text = SThe.Value.ToString().Substring(3, 2);
                col104.Text = col4.Text = col204.Text = SThe.Value.ToString().Substring(5, 2);
                col105.Text = col5.Text = col205.Text = SThe.Value.ToString().Substring(7, 3);
                col106.Text = col6.Text = col206.Text = SThe.Value.ToString().Substring(10, 5);
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
                    xrTableCell11.Text = "";
                    xrTableCell12.Text = "";
                }
            }
            if (DungChung.Bien.MaBV == "24009")
            {
                xrLabel127.Visible = true;
                xrLabel139.Visible = true;
            }

            // #24012
            if (DungChung.Bien.MaBV == "24012")
            {
                int mabn = 0;
                if(MaBNhan.Value != null)
                {
                    mabn = Convert.ToInt32(MaBNhan.Value);
                }
                var ttbx = data.TTboXungs.Where(p => p.MaBNhan == mabn).ToList();
                int count = 0;
                
                // tuổi
                if (Tuoi.Value != null && Tuoi.Value.ToString() != "")
                {
                    var tuoi = Tuoi.Value.ToString();
                    count = tuoi.Length;
                    if (tuoi.Length == 1)
                    {
                        lblTuoi0.Text = "0";
                        lblTuoi1.Text = tuoi[0].ToString();
                    }
                    else if (tuoi.Length == 2)
                    {
                        lblTuoi0.Text = tuoi[0].ToString();
                        lblTuoi1.Text = tuoi[1].ToString();
                    }
                    if (Tuoi.Value != null && (Tuoi.Value.ToString().ToLower().Contains("tháng") || Tuoi.Value.ToString().ToLower().Contains("ngày")) && Tuoi.Value.ToString().Substring(1, 1) == " ")
                    {
                        lblTuoi0.Text = "0";
                        lblTuoi1.Text = tuoi[0].ToString();
                    }
                    if (Tuoi.Value != null && Tuoi.Value.ToString().Contains("tháng"))
                    {
                        labTuoi.Text = "Tháng";
                        string temp = Tuoi.Value.ToString().Substring(0, 2);
                        if(Convert.ToInt32(temp) > 9)
                        {
                            lblTuoi0.Text = temp[0].ToString();
                            lblTuoi1.Text = temp[1].ToString();
                        } else 
                        {
                            lblTuoi0.Text = "0";
                            lblTuoi1.Text = temp[0].ToString();
                        }
                        
                    }
                    if (Tuoi.Value != null && Tuoi.Value.ToString().Contains("ngày"))
                    {
                        labTuoi.Text = "Ngày";
                        string temp1 = Tuoi.Value.ToString().Substring(0, 2);
                        if (Convert.ToInt32(temp1) > 9)
                        {
                            lblTuoi0.Text = temp1[0].ToString();
                            lblTuoi1.Text = temp1[1].ToString();
                        }
                        else
                        {
                            lblTuoi0.Text = "0";
                            lblTuoi1.Text = temp1[0].ToString();
                        }
                    }
                }
                
                // ngày sinh
                if(NSinh.Value != null && NSinh.Value.ToString() != "")
                {
                    var nsinh = NSinh.Value.ToString().Replace("/", "");
                    ns1.Text = nsinh[0].ToString();
                    ns2.Text = nsinh[1].ToString();
                    ns3.Text = nsinh[2].ToString();
                    ns4.Text = nsinh[3].ToString();
                    ns5.Text = nsinh[4].ToString();
                    ns6.Text = nsinh[5].ToString();
                    ns7.Text = nsinh[6].ToString();
                    ns8.Text = nsinh[7].ToString();
                }

                //nghề nghiệp
                if(MaNN.Value != null && MaNN.Value.ToString() != "")
                {
                    var nn = MaNN.Value.ToString();

                    maNN1.Text = nn[0].ToString();
                    maNN2.Text = nn[1].ToString();
                }

                //dân tộc
                if (MaDT.Value != null && MaDT.Value.ToString() != "")
                {
                    var dtoc = MaDT.Value.ToString();
                    if (dtoc.Length == 1)
                    {
                        madt0.Text = "0";
                        madt1.Text = dtoc[0].ToString();
                    }
                    else if (dtoc.Length == 2)
                    {
                        madt0.Text = dtoc[0].ToString();
                        madt1.Text = dtoc[1].ToString();
                    }
                }

                //địa chỉ
                if (ttbx.Count() > 0)
                {
                    var maTinh = ttbx.First().MaTinh;
                    if(maTinh != null && maTinh.ToString() != "")
                    {
                        maTinh0.Text = maTinh[0].ToString();
                        maTinh1.Text = maTinh[1].ToString();
                    }
                    var maHuyen = ttbx.First().MaHuyen;
                    if (maHuyen != null && maHuyen.ToString() != "")
                    {
                        maHuyen0.Text = maHuyen[0].ToString();
                        maHuyen1.Text = maHuyen[1].ToString();
                        maHuyen2.Text = maHuyen[2].ToString();
                    }
                } 
                if(DChi.Value != null && DChi.Value.ToString() != "")
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

                if (DTuong.Value != null)
                {
                    switch (DTuong.Value.ToString())
                    {
                        case "BHYT":
                            colBHYT2.Text = "X";
                            break;
                        case "Dịch vụ":
                            colThuPhi2.Text = "X";
                            break;
                        case "Miễn":
                            colMien2.Text = "X";
                            break;
                        case "Khác":
                            colKhac2.Text = "X";
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

        private void repPhieuKBVV_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "24389")
            {
                SubBand1.Visible = false;
                SubBand3.Visible = false;
                SubBand4.Visible = true;
            }
            else if (DungChung.Bien.MaBV == "14017")
            {
                SubBand1.Visible = true;
                SubBand2.Visible = false;
                SubBand3.Visible = false;
                SubBand4.Visible = true;
            }
            else if (DungChung.Bien.MaBV == "24012")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
                SubBand3.Visible = false;
                SubBand4.Visible = false;
                SubBand5.Visible = true;
            }
            else
            {
                SubBand2.Visible = false;
            }
        }

        private void SubBand1_BeforePrint(object sender, CancelEventArgs e)
        {

        }

    }
}
