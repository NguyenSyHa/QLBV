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
    public partial class Rep_TongHop_XN_Mau_4069 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_TongHop_XN_Mau_4069()
        {
            InitializeComponent();
        }
       
        List<FormThamSo.Frm_TongHop_CLS.c_DSDV> _lDichVu = new List<FormThamSo.Frm_TongHop_CLS.c_DSDV>();
        List<FormThamSo.Frm_TongHop_CLS.BenhNhan> _lSoLuongt = new List<FormThamSo.Frm_TongHop_CLS.BenhNhan>();
        public Rep_TongHop_XN_Mau_4069(List<FormThamSo.Frm_TongHop_CLS.c_DSDV> lDichVu, List<FormThamSo.Frm_TongHop_CLS.BenhNhan> lSoLuongt)
        {
            InitializeComponent();
            this._lDichVu = lDichVu;
            this._lSoLuongt = lSoLuongt;
        }
        int count = 0;
        public void BindingData()
        {
            if (DungChung.Bien.MaBV == "30372")
            {
                #region 30372
                colMabn1.DataBindings.Add("Text", DataSource, "MaBNhan");
                colTenBN2.DataBindings.Add("Text", DataSource, "TenBNhan");
                colGioitinh2.DataBindings.Add("Text", DataSource, "TuoiNam");
                colTuoi2.DataBindings.Add("Text", DataSource, "TuoiNu");
                colDiachi2.DataBindings.Add("Text", DataSource, "diachi");
                colBHYT2.DataBindings.Add("Text", DataSource, "bhyt");
                colDV2.DataBindings.Add("Text", DataSource, "DichVu");
                colBSchiDinh2.DataBindings.Add("Text", DataSource, "TenCBcd");
                NoiGui2.DataBindings.Add("Text", DataSource, "noigui");
                colYeuCau2.DataBindings.Add("Text", DataSource, "YeuCau");
                celChanDoan2.DataBindings.Add("Text", DataSource, "ChanDoan");
                colNguoiDoc2.DataBindings.Add("Text", DataSource, "TenCBth");
                ngaythang2.DataBindings.Add("Text", DataSource, "ngaythang").FormatString = "{0:dd/MM/yy HH:mm}";
                celNgay2.DataBindings.Add("Text", DataSource, "ngaythang1").FormatString = "{0:dd/MM/yyyy}";
                GroupHeader1.GroupFields.Add(new GroupField("ngaythang1"));
                #endregion
                int i = 1;
                foreach (XRTableCell _tableCell in xrTableRow13)
                {

                    if (_tableCell.Name == "KQ" + (i).ToString())
                    {
                        _tableCell.Name = "colKQ" + (i).ToString();
                        _tableCell.DataBindings.Add("Text", DataSource, _tableCell.Name.ToString()).FormatString = "{0:0,0;-0,0;#}";
                        i++;
                    }
                }
            }
            else if (DungChung.Bien.MaBV == "24009" && DungChung.Bien.BC408_24009_ischecked == false)
            {
                #region 24009
                colMaBN_24009.DataBindings.Add("Text", DataSource, "MaBNhan");
                colTenBN_24009.DataBindings.Add("Text", DataSource, "TenBNhan");
                colGioiTinh_24009.DataBindings.Add("Text", DataSource, "TuoiNam");
                colTuoi_24009.DataBindings.Add("Text", DataSource, "TuoiNu");
                colDiaChi_24009.DataBindings.Add("Text", DataSource, "diachi");
                colBHYT_24009.DataBindings.Add("Text", DataSource, "bhyt");
                colDichVu_24009.DataBindings.Add("Text", DataSource, "DichVu");
                colBSChiDinh_24009.DataBindings.Add("Text", DataSource, "TenCBcd");
                colNoiGui_24009.DataBindings.Add("Text", DataSource, "noigui");
                colYeuCau_24009.DataBindings.Add("Text", DataSource, "YeuCau");
                colChanDoan_24009.DataBindings.Add("Text", DataSource, "ChanDoan");
                colNguoiDoc_24009.DataBindings.Add("Text", DataSource, "TenCBth");
                colNgayTH_24009.DataBindings.Add("Text", DataSource, "ngaythang").FormatString = "{0:dd/MM/yy HH:mm}";
                celNgay_24009.DataBindings.Add("Text", DataSource, "ngaythang1").FormatString = "{0:dd/MM/yyyy}";
                GroupHeader1.GroupFields.Add(new GroupField("ngaythang1"));
                #endregion
                int i = 1;
                foreach (XRTableCell _tableCell in xrTableRow22)
                {

                    if (_tableCell.Name == "KQ0" + (i).ToString())
                    {
                        _tableCell.Name = "colKQ" + (i).ToString();
                        _tableCell.DataBindings.Add("Text", DataSource, _tableCell.Name.ToString()).FormatString = "{0:0,0;-0,0;#}";
                        i++;
                    }
                }
            }
            else
            {
                colMaBN.DataBindings.Add("Text", DataSource, "MaBNhan");
                celMabn.DataBindings.Add("Text", DataSource, "MaBNhan");
                colTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
                celTenbn.DataBindings.Add("Text", DataSource, "TenBNhan");
                colGioitinh.DataBindings.Add("Text", DataSource, "TuoiNam");
                celgioitinh.DataBindings.Add("Text", DataSource, "TuoiNam");
                colTuoi.DataBindings.Add("Text", DataSource, "TuoiNu");
                celtuoi.DataBindings.Add("Text", DataSource, "TuoiNu");
                colDiachi.DataBindings.Add("Text", DataSource, "diachi");
                colBHYT.DataBindings.Add("Text", DataSource, "bhyt");
                celBHYT.DataBindings.Add("Text", DataSource, "bhyt");
                colDV.DataBindings.Add("Text", DataSource, "DichVu");
                colBSchiDinh.DataBindings.Add("Text", DataSource, "TenCBcd");
                celBSchiDinh.DataBindings.Add("Text", DataSource, "TenCBcd");
                colNoiGui.DataBindings.Add("Text", DataSource, "noigui");
                celNoigui.DataBindings.Add("Text", DataSource, "noigui");
                colYeuCau.DataBindings.Add("Text", DataSource, "YeuCau");
                celYeuCau.DataBindings.Add("Text", DataSource, "YeuCau");
                celChanDoan.DataBindings.Add("Text", DataSource, "ChanDoan");
                colNguoiDoc.DataBindings.Add("Text", DataSource, "TenCBth");
                celNguoiDoc.DataBindings.Add("Text", DataSource, "TenCBth");
                ngaythang.DataBindings.Add("Text", DataSource, "ngaythang").FormatString = "{0:dd/MM/yy HH:mm}";
                celNgay.DataBindings.Add("Text", DataSource, "ngaythang1").FormatString = "{0:dd/MM/yyyy}";
                celNgay1.DataBindings.Add("Text", DataSource, "ngaythang1").FormatString = "{0:dd/MM/yyyy}";

                GroupHeader1.GroupFields.Add(new GroupField("ngaythang1"));
                int i = 1;
                foreach (XRTableCell _tableCell in xrTableRow1 )
                {

                    if (_tableCell.Name == "colKQ" + (i).ToString())
                    {
                        _tableCell.DataBindings.Add("Text", DataSource, _tableCell.Name.ToString()).FormatString = "{0:0,0;-0,0;#}";
                        i++;
                    }
                }
                int j = 1;
                foreach (XRTableCell _tableCell in xrTableRow29)
                {
                    if (_tableCell.Name == "celKQ" + (j).ToString())
                    {
                        _tableCell.Name = "colKQ" + (j).ToString();
                        _tableCell.DataBindings.Add("Text", DataSource, _tableCell.Name.ToString()).FormatString = "{0:0,0;-0,0;#}";
                        j++;
                    }
                }
            }
            if (DungChung.Bien.MaBV == "30010")
            {
                colCKham.DataBindings.Add("Text", DataSource, "CKham");
                colCKhamT.DataBindings.Add("Text", DataSource, "CKham");
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
                SubBand2.Visible = true;
            }
            else
                SubBand1.Visible = true;


        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNLB.Text = DungChung.Bien.NguoiLapBieu;
            celNLB.Text = DungChung.Bien.NguoiLapBieu;
            colNguoiLap_24009.Text = DungChung.Bien.NguoiLapBieu;
            
            if(DungChung.Bien.MaBV == "30372")
            {
                for (int i = 0; i < _lDichVu.Count; i++)
                {
                    foreach (XRTableCell _tableCell in xrTableRow14)
                    {
                        //System.Windows.Forms.MessageBox.Show(_tableCell.Name);

                        if (_tableCell.Name == "colTong" + (i + 1).ToString())
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
            else if(DungChung.Bien.MaBV == "24009" && DungChung.Bien.BC408_24009_ischecked == false)
            {
                for (int i = 0; i < _lDichVu.Count; i++)
                {
                    foreach (XRTableCell _tableCell in xrTableRow28)
                    {
                        //System.Windows.Forms.MessageBox.Show(_tableCell.Name);

                        if (_tableCell.Name == "Tong0" + (i + 1).ToString())
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
                    foreach (XRTableCell _tableCell in xrTableRow33)
                    {
                        //System.Windows.Forms.MessageBox.Show(_tableCell.Name);

                        if (_tableCell.Name == "celT" + (i + 1).ToString())
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
            if(DungChung.Bien.MaBV == "30372")
            {
                for (int i = 0; i < _lDichVu.Count; i++)
                {
                    foreach (XRTableCell _tableCell in xrTableRow12)
                    {
                        if (_tableCell.Name == "ColTen" + (i + 1).ToString())
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
            else if (DungChung.Bien.MaBV == "24009" && DungChung.Bien.BC408_24009_ischecked == false)
            {
                for (int i = 0; i < _lDichVu.Count; i++)
                {
                    foreach (XRTableCell _tableCell in xrTableRow19)
                    {
                        if (_tableCell.Name == "colTen0" + (i + 1).ToString())
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
                    foreach (XRTableCell _tableCell in xrTableRow2)
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

                    foreach (XRTableCell _tableCell in xrTableRow24)
                    {
                        if (_tableCell.Name == "celTen" + (i + 1).ToString())
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
            count++;
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
            if (DungChung.Bien.MaBV == "30009") { colNguoiDoc.Text = "Nguyễn Thị Nhàn"; }
        }

        private void Rep_TongHop_XN_Mau_4069_BeforePrint(object sender, CancelEventArgs e)
        {
            if(DungChung.Bien.MaBV=="30372")
            {
                SubBand30372.Visible = true;
                SubBand30372_2.Visible = true;
                SubBand30372_3.Visible = true;
                SubBand30372_4.Visible = true;
            }
            else if (DungChung.Bien.MaBV == "24009" && DungChung.Bien.BC408_24009_ischecked == false)
            {
                SubBand_24009_1.Visible = true;
                SubBand_24009_2.Visible = true;
                SubBand_24009_3.Visible = true;
                SubBand_24009_4.Visible = true;
            }
           
        }
    }
}
