using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using static QLBV.FormNhap.Frm_baocaonxttoanvien;
using System.Windows.Media;

namespace QLBV.BaoCao
{
    public partial class rep_Phieuxetnghiemvisinh : DevExpress.XtraReports.UI.XtraReport
    {
        //QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public rep_Phieuxetnghiemvisinh()
        {
            InitializeComponent();
        }
        string ngayCD = "Giờ .... ngày... tháng... năm ......";
        string ngayTH = "Giờ .... ngày... tháng... năm ......";
        string tenBSCD = "Họ tên: ";
        string tenBSTH = "Họ tên: ";
        public void bindingData(int idCLS)
        {
          
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var qbn = (from cls in data.CLS.Where(p => p.IdCLS == idCLS)
                         join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                         join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                         join dv in data.DichVus on cd.MaDV equals dv.MaDV
                         join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                         join dvct in data.DichVucts on new{clsct.MaDVct} equals new{dvct.MaDVct} 
                       //into kq
                       //  from kq1 in kq.DefaultIfEmpty()     
                         select new{
                             cls.IdCLS,
                             cls.CapCuu,
                             cls.MaCBth,
                             cls.MaCB,
                             cls.NgayThang,
                             cls.NgayTH,
                             cls.MaKP,
                             cls.MaKPth,
                             bn.MaBNhan,
                             bn.TenBNhan,
                             bn.Tuoi,
                             bn.GTinh,
                             bn.DChi,
                             bn.SThe,
                             dv.SoTT,
                             dvct.STT,
                             dvct.TenDVct,
                             clsct.KetQua,
                             cls.Status
                         }).Distinct().OrderBy(p=>p.STT).ToList();
            if (DungChung.Bien.MaBV == "30010")
            {
                int mkpth = Convert.ToInt32(qbn.First().MaKPth.Value);
                KPTH.Value = data.KPhongs.Where(p => p.MaKP == mkpth).Select(p => p.TenKP).First().ToString();
            }//yenbg1
            if (qbn.Count() > 0)
            {
                if (qbn.FirstOrDefault().NgayThang != null)
                    ngayCD = "Giờ " + qbn.FirstOrDefault().NgayThang.Value.Hour + " ngày " + qbn.FirstOrDefault().NgayThang.Value.Day + " tháng " + qbn.FirstOrDefault().NgayThang.Value.Month + " năm " + qbn.FirstOrDefault().NgayThang.Value.Year;
                if (qbn.FirstOrDefault().NgayTH != null)
                    ngayTH = "Giờ " + qbn.FirstOrDefault().NgayTH.Value.Hour + " ngày " + qbn.FirstOrDefault().NgayTH.Value.Day + " tháng " + qbn.FirstOrDefault().NgayTH.Value.Month + " năm " + qbn.FirstOrDefault().NgayTH.Value.Year;
                if(qbn.FirstOrDefault().MaCB != null)
                {
                    string macb = qbn.FirstOrDefault().MaCB;
                    var qcb = data.CanBoes.Where(p => p.MaCB == macb).Select(p => p.TenCB).FirstOrDefault();
                    if(qcb != null)
                        tenBSCD = "Họ tên: " + qcb;
                }
                if (qbn.FirstOrDefault().MaCBth != null)
                {
                    string macb = qbn.FirstOrDefault().MaCBth;
                    var qcb = data.CanBoes.Where(p => p.MaCB == macb).Select(p => p.TenCB).FirstOrDefault();
                    if (qcb != null)
                        tenBSTH = "Họ tên: " + qcb;
                }

                var qvv = (from cls in data.CLS.Where(parameters => parameters.IdCLS == idCLS)
                           join vv in data.VaoViens on cls.MaBNhan equals vv.MaBNhan
                           select vv).FirstOrDefault();
                if (qvv != null)
                    xlbSoIdCLS.Text = qvv.SoVV;
                        if (qbn.FirstOrDefault().CapCuu == true)
                {
                    celCapcuu.Text = "x";
                }
                else celThuong.Text = "x";

                xlbTenBN.Text = qbn.FirstOrDefault().TenBNhan;
                xlbDiachi.Text = qbn.FirstOrDefault().DChi;
                xlbTuoi.Text = qbn.FirstOrDefault().Tuoi.ToString();
                if (qbn.FirstOrDefault().GTinh == 1)
                {
                    xlbNu.Text = "/";
                }
                else xlbNam.Text = "/";
                string soThe = qbn.FirstOrDefault().SThe;
                if (soThe.Length >= 15)
                {
                    txtThe1.Text = soThe.Substring(0, 3);
                    txtThe2.Text = soThe.Substring(3, 2);
                    txtThe3.Text = soThe.Substring(5, 2);
                    txtThe4.Text = soThe.Substring(7, 3);
                    txtThe5.Text = soThe.Substring(10, 5);
                }

                string[] arrThongTinBNKB = new string[5] { "", "", "", "", "" };
                arrThongTinBNKB = DungChung.Ham.laythongtinBNKB(data, Convert.ToInt32(qbn.FirstOrDefault().MaBNhan), Convert.ToInt32(qbn.FirstOrDefault().MaKP), true);
                xlbChandoan.Text = arrThongTinBNKB[1];
                xlbBuong.Text = arrThongTinBNKB[2];
                if (DungChung.Bien.MaBV == "27023")
                {
                    xlbGiuong.Text = arrThongTinBNKB[4];
                    xlbKhoa.Text = arrThongTinBNKB[3];
                }
                else
                {
                    xlbGiuong.Text = arrThongTinBNKB[3];
                    xlbKhoa.Text = arrThongTinBNKB[4];
                }
                string macbth = qbn.FirstOrDefault().MaCBth;
                //string bsdt = null;
                //if (macbth != null || macbth != "") { 
                //bsdt = DungChung.Ham._getTenCB(data,macbth).ToString();
                //}
                //    if (bsdt != null || bsdt !="") {
                //    celBacsidieutri.Text = bsdt;
                //}
                //sst=1
                    if (qbn.Where(p => p.SoTT == 1).Count() > 0 && qbn.Where(p => p.SoTT == 1).First().TenDVct != null)
                    {
                        string a = qbn.Where(p => p.SoTT == 1).First().TenDVct.ToString();
                        //a = a.Replace("#", "\n");
                        celTenDVct11.Text = a;
                    }
                    if (qbn.Where(p => p.SoTT == 1).Count() > 1 && qbn.Where(p => p.SoTT == 1).Skip(1).First().TenDVct != null)
                    {
                        string a = qbn.Where(p => p.SoTT == 1).Skip(1).First().TenDVct.ToString();
                        //a = a.Replace("#", "\n");
                        celTenDVct12.Text = a;
                    }
                    if (qbn.Where(p => p.SoTT == 1).Count() > 2 && qbn.Where(p => p.SoTT == 1).Skip(2).First().TenDVct != null)
                    {
                        string a = qbn.Where(p => p.SoTT == 1).Skip(2).First().TenDVct.ToString();
                        //a = a.Replace("#", "\n");
                        celTenDVct13.Text = a;
                    }
                    //
                    if (qbn.Where(p => p.SoTT == 1).Count() > 0 && qbn.Where(p => p.SoTT == 1).First().KetQua != null)
                    {
                        string a = qbn.Where(p => p.SoTT == 1).First().KetQua.ToString();
                        //a = a.Replace("#", "\n");
                        celKQ11.Text = a;
                    }
                    if (qbn.Where(p => p.SoTT == 1).Count() > 1 && qbn.Where(p => p.SoTT == 1).Skip(1).First().KetQua != null)
                    {
                        string a = qbn.Where(p => p.SoTT == 1).Skip(1).First().KetQua.ToString();
                        //a = a.Replace("#", "\n");
                        celKQ12.Text = a;
                    }
                    if (qbn.Where(p => p.SoTT == 1).Count() > 2 && qbn.Where(p => p.SoTT == 1).Skip(2).First().KetQua != null)
                    {
                        string a = qbn.Where(p => p.SoTT == 1).Skip(2).First().KetQua.ToString();
                        //a = a.Replace("#", "\n");
                        celKQ13.Text = a;
                    } 
                //stt=2
                    if (qbn.Where(p => p.SoTT == 2).Count() > 0 && qbn.Where(p => p.SoTT == 2).First().TenDVct != null)
                    {
                        string a = qbn.Where(p => p.SoTT == 2).First().TenDVct.ToString();
                        //a = a.Replace("#", "\n");
                        celTenDVct21.Text = a;
                    }
                    if (qbn.Where(p => p.SoTT == 2).Count() > 1 && qbn.Where(p => p.SoTT == 2).Skip(1).First().TenDVct != null)
                    {
                        string a = qbn.Where(p => p.SoTT == 2).Skip(1).First().TenDVct.ToString();
                        //a = a.Replace("#", "\n");
                        celTenDVct22.Text = a;
                    }
                    if (qbn.Where(p => p.SoTT == 2).Count() > 2 && qbn.Where(p => p.SoTT == 2).Skip(2).First().TenDVct != null)
                    {
                        string a = qbn.Where(p => p.SoTT == 2).Skip(2).First().TenDVct.ToString();
                        //a = a.Replace("#", "\n");
                        celTenDVct23.Text = a;
                    }
                    //
                    if (qbn.Where(p => p.SoTT == 2).Count() > 0 && qbn.Where(p => p.SoTT == 2).First().KetQua != null)
                    {
                        string a = qbn.Where(p => p.SoTT == 2).First().KetQua.ToString();
                        //a = a.Replace("#", "\n");
                        celKQ21.Text = a;
                    }
                    if (qbn.Where(p => p.SoTT == 2).Count() > 1 && qbn.Where(p => p.SoTT == 2).Skip(1).First().KetQua != null)
                    {
                        string a = qbn.Where(p => p.SoTT == 2).Skip(1).First().KetQua.ToString();
                        //a = a.Replace("#", "\n");
                        celKQ22.Text = a;
                    }
                    if (qbn.Where(p => p.SoTT == 2).Count() > 2 && qbn.Where(p => p.SoTT == 2).Skip(2).First().KetQua != null)
                    {
                        string a = qbn.Where(p => p.SoTT == 2).Skip(2).First().KetQua.ToString();
                        //a = a.Replace("#", "\n");
                        celKQ23.Text = a;
                    } 
                //stt=3
                    if (qbn.Where(p => p.SoTT == 3).Count() > 0 && qbn.Where(p => p.SoTT == 3).First().TenDVct != null)
                    {
                        string a = qbn.Where(p => p.SoTT == 3).First().TenDVct.ToString();
                        //a = a.Replace("#", "\n");
                        celTenDVct31.Text = a;
                    }
                    if (qbn.Where(p => p.SoTT == 3).Count() > 1 && qbn.Where(p => p.SoTT == 3).Skip(1).First().TenDVct != null)
                    {
                        string a = qbn.Where(p => p.SoTT == 3).Skip(1).First().TenDVct.ToString();
                        //a = a.Replace("#", "\n");
                        celTenDVct32.Text = a;
                    }
                    if (qbn.Where(p => p.SoTT == 3).Count() > 2 && qbn.Where(p => p.SoTT == 3).Skip(2).First().TenDVct != null)
                    {
                        string a = qbn.Where(p => p.SoTT == 3).Skip(2).First().TenDVct.ToString();
                        //a = a.Replace("#", "\n");
                        celTenDVct33.Text = a;
                    } 
                //
                    if (qbn.Where(p => p.SoTT == 3).Count() > 0 && qbn.Where(p => p.SoTT == 3).First().KetQua != null)
                    {
                        string a = qbn.Where(p => p.SoTT == 3).First().KetQua.ToString();
                        //a = a.Replace("#", "\n");
                        celKQ31.Text = a;
                    }
                    if (qbn.Where(p => p.SoTT == 3).Count() > 1 && qbn.Where(p => p.SoTT == 3).Skip(1).First().KetQua != null)
                    {
                        string a = qbn.Where(p => p.SoTT == 3).Skip(1).First().KetQua.ToString();
                        //a = a.Replace("#", "\n");
                        celKQ32.Text = a;
                    }
                    if (qbn.Where(p => p.SoTT == 3).Count() > 2 && qbn.Where(p => p.SoTT == 3).Skip(2).First().KetQua != null)
                    {
                        string a = qbn.Where(p => p.SoTT == 3).Skip(2).First().KetQua.ToString();
                        //a = a.Replace("#", "\n");
                        celKQ33.Text = a;
                    }

                #region check kq null
                //if (qbn.Count > 0)
                //{
                //    if (qbn.Where(p => p.STT == 1).FirstOrDefault().Status == 0)
                //    {
                //        celKQ11.Text = "";
                //        celKQ12.Text = "";
                //        celKQ13.Text = "";
                //    }
                //    if (qbn.Where(p => p.STT == 2).FirstOrDefault().Status == 0)
                //    {
                //        celKQ21.Text = "";
                //        celKQ22.Text = "";
                //        celKQ23.Text = "";
                //    }
                //    if (qbn.Where(p => p.STT == 3).FirstOrDefault().Status == 0)
                //    {
                //        celKQ31.Text = "";
                //        celKQ32.Text = "";
                //        celKQ33.Text = "";
                //    }
                //}
                #endregion
            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            xlbBenhvien.Text = DungChung.Bien.TenCQ.ToUpper();
            xlbSoyte.Text = DungChung.Bien.TenCQCQ.ToUpper();
            if(DungChung.Bien.MaBV == "27023")
            {
                lab14.Text = "- Khoa: ";
                lab12.Text = "Giường: ";
            }
        }

        private void celTenDVct11_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-top", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-left", "1px dashed #000000 !important");
            e.ContentCell.Style.Add("border-bottom", "3px solid #000000 !important");
            e.ContentCell.Style.Add("border-right", "3px dashed #000000 !important");   
        }

        private void GroupFooter1_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celNgaythang1.Text = ngayCD;
            celNgayThang2.Text = ngayTH;
            celBSDT.Text = tenBSCD;
            celBSChuyenKhoa.Text = tenBSTH;
        }
    }
}
