using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace QLBV.BaoCao
{ 
    public partial class Rep_TongHop_XN : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_TongHop_XN()
        {
            InitializeComponent();
        }
       
        List<FormThamSo.Frm_TongHop_CLS.c_DSDV> _lDichVu = new List<FormThamSo.Frm_TongHop_CLS.c_DSDV>();
        List<FormThamSo.Frm_TongHop_CLS.BenhNhan> _lSoLuongt = new List<FormThamSo.Frm_TongHop_CLS.BenhNhan>();
        public Rep_TongHop_XN(List<FormThamSo.Frm_TongHop_CLS.c_DSDV> lDichVu, List<FormThamSo.Frm_TongHop_CLS.BenhNhan> lSoLuongt)
        {
            InitializeComponent();
            this._lDichVu = lDichVu;
            this._lSoLuongt = lSoLuongt;
        }
        public void BindingData()
        {
            if (DungChung.Bien.MaBV != "30372")
            {
                celTen.DataBindings.Add("Text", DataSource, "TenBNhan");
                celTen1.DataBindings.Add("Text", DataSource, "TenBNhan");
                if (DungChung.Bien.MaBV == "24012")
                {
                    celTuoi2.DataBindings.Add("Text", DataSource, "tuoinam");
                    celTuoi1.DataBindings.Add("Text", DataSource, "tuoinu");
                }
                else
                {
                    celTuoi2.DataBindings.Add("Text", DataSource, "gioitinh");
                    celTuoi1.DataBindings.Add("Text", DataSource, "tuoi");
                }
                celGT.DataBindings.Add("Text", DataSource, "gioitinh");
                celTuoi.DataBindings.Add("Text", DataSource, "tuoi");
                celDC.DataBindings.Add("Text", DataSource, "diachi");
                celBHYT.DataBindings.Add("Text", DataSource, "bhyt");
                celBHYT1.DataBindings.Add("Text", DataSource, "bhyt");
                celNoiGui1.DataBindings.Add("Text", DataSource, "noigui");

                celYC1.DataBindings.Add("Text", DataSource, "chandoan");
                celDiaChi1.DataBindings.Add("Text", DataSource, "diachi");

                celNDoc.DataBindings.Add("Text", DataSource, "TenCBth");
                celNoiGui.DataBindings.Add("Text", DataSource, "noigui");
                celYC.DataBindings.Add("Text", DataSource, "YeuCau");
                celYC2.DataBindings.Add("Text", DataSource, "YeuCau");

                celNDoc1.DataBindings.Add("Text", DataSource, "TenCBth");
                //colChanDoan.DataBindings.Add("Text", DataSource, "TenCBth"); // chưa rõ bệnh viện nào
                celChanDoan.DataBindings.Add("Text", DataSource, "chandoan");
                if (DungChung.Bien.MaBV == "30010")
                {
                    colCKham.DataBindings.Add("Text", DataSource, "CKham");
                    colCKhamT.DataBindings.Add("Text", DataSource, "CKham");
                }
                if (DungChung.Bien.MaBV == "24012")
                {
                    celNgayTH.DataBindings.Add("Text", DataSource, "ngaythang").FormatString = "{0:dd/MM }";
                }
                else
                    celNgayTH.DataBindings.Add("Text", DataSource, "ngaythang").FormatString = "{0:dd/MM HH:mm}";
                celNgay.DataBindings.Add("Text", DataSource, "ngaythang1").FormatString = "{0:dd/MM/yyyy}";
                celNgay1.DataBindings.Add("Text", DataSource, "ngaythang1").FormatString = "{0:dd/MM/yyyy}";
                cel_ngay1.DataBindings.Add("Text", DataSource, "ngaythang1").FormatString = "{0:dd/MM/yyyy}";

                GroupHeader1.GroupFields.Add(new GroupField("ngaythang1"));
                GroupHeader2.GroupFields.Add(new GroupField("ngaythang1"));
                if (DungChung.Bien.MaBV == "27001")
                {
                    celNgayTH.DataBindings.Add("Text", DataSource, "ngaythang").FormatString = "{0:dd/MM/yyyy }";
                    celNgay.DataBindings.Add("Text", DataSource, "ngaythang1").FormatString = "{0:dd/MM/yyyy}";
                    celNgay1.DataBindings.Add("Text", DataSource, "ngaythang1").FormatString = "{0:dd/MM/yyyy}";
                }


                int i = 1;
                foreach (XRTableCell _tableCell in xrTableRow17)
                {

                    if (_tableCell.Name == "cel" + (i).ToString())
                    {
                        _tableCell.DataBindings.Add("Text", DataSource, _tableCell.Name.ToString()).FormatString = "{0:0,0;-0,0;#}";
                        i++;
                    }
                }
                int j = 1;
                foreach (XRTableCell _tableCell in xrTableRow21)
                {

                    if (_tableCell.Name == "cl" + (j).ToString())
                    {
                        _tableCell.Name = "cel" + (j).ToString();
                        _tableCell.DataBindings.Add("Text", DataSource, _tableCell.Name.ToString()).FormatString = "{0:0,0;-0,0;#}";
                        j++;
                    }
                }
            }
            else
            {
                colBS.DataBindings.Add("Text", DataSource, "TenCBcd");
                colHoten.DataBindings.Add("Text", DataSource, "TenBNhan");
                colGioitinh.DataBindings.Add("Text", DataSource, "gioitinh");
                colTuoi.DataBindings.Add("Text", DataSource, "tuoi");
                colDiachi.DataBindings.Add("Text", DataSource, "diachi");
                colBHYT.DataBindings.Add("Text", DataSource, "bhyt");
                colTenKP.DataBindings.Add("Text", DataSource, "noigui");
                colYeuCau.DataBindings.Add("Text", DataSource, "YeuCau");
                colNDoc.DataBindings.Add("Text", DataSource, "TenCBth");
                //colChanDoan.DataBindings.Add("Text", DataSource, "TenCBth"); // chưa rõ bệnh viện nào
                colChanDoan.DataBindings.Add("Text", DataSource, "chandoan");
                if (DungChung.Bien.MaBV == "30010")
                {
                    colCKham.DataBindings.Add("Text", DataSource, "CKham");
                    colCKhamT.DataBindings.Add("Text", DataSource, "CKham");
                }
                col_ngaythang.DataBindings.Add("Text", DataSource, "ngaythang").FormatString = "{0:dd/MM HH:mm}";
                celNgay.DataBindings.Add("Text", DataSource, "ngaythang1").FormatString = "{0:dd/MM/yyyy}";
                GroupHeader1.GroupFields.Add(new GroupField("ngaythang1"));
                int i = 1, j = 1;
                foreach (XRTableCell _tableCell in xrTableRow1)
                {

                    if (_tableCell.Name == "colKQ" + (i).ToString())
                    {
                        _tableCell.DataBindings.Add("Text", DataSource, _tableCell.Name.ToString()).FormatString = "{0:0,0;-0,0;#}";
                        i++;
                    }
                }
                foreach (XRTableCell _tableCell in xrTableRow17)
                {

                    if (_tableCell.Name == "cel" + (j).ToString())
                    {
                        _tableCell.DataBindings.Add("Text", DataSource, _tableCell.Name.ToString()).FormatString = "{0:0,0;-0,0;#}";
                        j++;
                    }
                }
            }
            
         }

        private void colTenKP_BeforePrint(object sender, CancelEventArgs e)
        {
          
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            txtTenCQCQ2.Text = DungChung.Bien.TenCQCQ.ToUpper();
            int _makp = DungChung.Bien.MaKP;
            var q = from kp in _data.KPhongs.Where(p => p.MaKP == _makp) select new { kp.TenKP };
            if (q.Count() > 0)
            {
                colKP.Text = q.First().TenKP;
            }
            if (DungChung.Bien.MaBV == "30003")
            {
                SubBand9.Visible = true;
            }
            else if(DungChung.Bien.MaBV=="24012")
            {
                SubBand1.Visible = true;
            }    
            else
                SubBand2.Visible = true;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            
            if (DungChung.Bien.MaBV == "30372")
            {
                colNLB.Text = DungChung.Bien.NguoiLapBieu;
                for (int i = 0; i < _lDichVu.Count; i++)
                {
                    foreach (XRTableCell _tableCell in xrTableRow3)
                    {
                        //System.Windows.Forms.MessageBox.Show(_tableCell.Name);

                        if (_tableCell.Name == "colT" + (i + 1).ToString())
                        {
                            string _madvct = "";
                            if (_lDichVu.Skip(i).ToList().Count > 0)
                                _madvct = _lDichVu.Skip(i).First().MaDVct;
                            string _soluong = "";
                            if (_lSoLuongt.Where(p => p.MaDVct == _madvct).ToList().Count > 0 && _lSoLuongt.Where(p => p.MaDVct == _madvct).ToList().First().KetQua != null)
                                _soluong = _lSoLuongt.Where(p => p.MaDVct == _madvct).ToList().First().KetQua;
                            else
                                _soluong = "";
                            _tableCell.Text = _soluong.ToString();
                            break;
                        }
                    }

                }
            }
            else
            {
                celNLB.Text = DungChung.Bien.NguoiLapBieu;
                for (int i = 0; i < _lDichVu.Count; i++)
                {
                    foreach (XRTableCell _tableCell in xrTableRow2)
                    {
                        //System.Windows.Forms.MessageBox.Show(_tableCell.Name);

                        if (_tableCell.Name == "t" + (i + 1).ToString())
                        {
                            string _madvct = "";
                            if (_lDichVu.Skip(i).ToList().Count > 0)
                                _madvct = _lDichVu.Skip(i).First().MaDVct;
                            string _soluong = "";
                            if (_lSoLuongt.Where(p => p.MaDVct == _madvct).ToList().Count > 0 && _lSoLuongt.Where(p => p.MaDVct == _madvct).ToList().First().KetQua != null)
                                _soluong = _lSoLuongt.Where(p => p.MaDVct == _madvct).ToList().First().KetQua;
                            else
                                _soluong = "";
                            _tableCell.Text = _soluong.ToString();
                            break;
                        }
                    }
                    foreach (XRTableCell _tableCell in xrTableRow25)
                    {
                        //System.Windows.Forms.MessageBox.Show(_tableCell.Name);

                        if (_tableCell.Name == "te" + (i + 1).ToString())
                        {
                            string _madvct = "";
                            if (_lDichVu.Skip(i).ToList().Count > 0)
                                _madvct = _lDichVu.Skip(i).First().MaDVct;
                            string _soluong = "";
                            if (_lSoLuongt.Where(p => p.MaDVct == _madvct).ToList().Count > 0 && _lSoLuongt.Where(p => p.MaDVct == _madvct).ToList().First().KetQua != null)
                                _soluong = _lSoLuongt.Where(p => p.MaDVct == _madvct).ToList().First().KetQua;
                            else
                                _soluong = "";
                            _tableCell.Text = _soluong.ToString();
                            break;
                        }
                    }
                }
            }
            
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV=="30372")
            {
                for (int i = 0; i < _lDichVu.Count; i++)
                {
                    foreach (XRTableCell _tableCell in xrTableRow12)
                    {
                        if (_tableCell.Name == "ColTEN" + (i + 1).ToString())
                        {
                            string _tendv = _lDichVu.Skip(i).First().TenDVct;
                            _tableCell.Text = _tendv;
                            if (!string.IsNullOrEmpty(_tendv) && _tendv.Length > 40)
                            {
                                _tableCell.Font = new Font("Times New Roman", 7, System.Drawing.FontStyle.Bold);
                            }
                        }
                    }

                }
            }
            else
            {
                for (int i = 0; i < _lDichVu.Count; i++)
                {
                    foreach (XRTableCell _tableCell in xrTableRow14)
                    {
                        if (_tableCell.Name == "TEN" + (i + 1).ToString())
                        {
                            string _tendv = _lDichVu.Skip(i).First().TenDVct;
                            _tableCell.Text = _tendv;
                            if (!string.IsNullOrEmpty(_tendv) && _tendv.Length > 40)
                            {
                                _tableCell.Font = new Font("Times New Roman", 7, System.Drawing.FontStyle.Bold);
                            }
                        }
                    }
                    foreach (XRTableCell _tableCell in xrTableRow19)
                    {
                        if (_tableCell.Name == "celT" + (i + 1).ToString())
                        {
                            string _tendv = _lDichVu.Skip(i).First().TenDVct;
                            _tableCell.Text = _tendv;
                            if (!string.IsNullOrEmpty(_tendv) && _tendv.Length > 40)
                            {
                                _tableCell.Font = new Font("Times New Roman", 7, System.Drawing.FontStyle.Bold);
                            }
                        }
                    }
                }
            }
            
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
          //  for (int i = 0; i < _lDichVu.Count; i++)
            //{
            //    foreach (XRTableCell _tableCell in xrTableRow1)
            //    {
            //        //System.Windows.Forms.MessageBox.Show(_tableCell.Name);

            //        if (_tableCell.Name == "colKQ" + (i + 1).ToString())
            //        {
            //            int _madv = 0;
            //            int  _mabn = 0;
            //            double _id = 0;
            //            if (_lDichVu.Skip(i).ToList().Count > 0)
            //                _madv = _lDichVu.Skip(i).First().MaDV;
            //            if (this.GetCurrentColumnValue("maBN") != null && this.GetCurrentColumnValue("IDCLS") != null)
            //            {
            //                _mabn = this.GetCurrentColumnValue("maBN").ToString();
            //                _id = Convert.ToDouble(this.GetCurrentColumnValue("IDCLS").ToString());
            //            }
            //            string _soluong = "0";
            //            if (_lSoLuong.Where(p => p.MaDVct == _madv && p.MaBNhan == _mabn && p.IDCLS == _id).ToList().Count > 0 && _lSoLuong.Where(p => p.MaDVct == _madv && p.MaBNhan == _mabn && p.IDCLS == _id).First().KetQua!=null)
            //            {
            //                _soluong = _lSoLuong.Where(p => p.MaDVct == _madv && p.MaBNhan == _mabn && p.IDCLS == _id).First().KetQua;
            //            }
            //            else
            //            { _soluong = "0"; }
            //            if (_soluong != "0")
            //                _tableCell.Text = _soluong.ToString();
            //            else
            //                _tableCell.Text = "";
            //        }
            //    }
            //}
        }

        private void colNDoc_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30009") { colNDoc.Text = "Nguyễn Thị Nhàn"; }
        }
    }
}
