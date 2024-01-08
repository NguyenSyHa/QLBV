using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuXetNghiemNuocDich : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuXetNghiemNuocDich()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            celTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            //colTK.Text = DungChung.Bien.TruongKhoaLS;
            //celNgayky.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
        }

        internal void databinding(QLBV_Database.QLBVEntities data, int idCLS)
        {
            int kpth = DungChung.Bien.MaKP;
            int maDV = 0;

            var qbn = (from bn in data.BenhNhans join cls in data.CLS.Where(p => p.IdCLS == idCLS) on bn.MaBNhan equals cls.MaBNhan select new { bn.MaBNhan, bn.TenBNhan, bn.Tuoi, bn.GTinh, bn.CapCuu, bn.DChi, bn.SThe, cls.MaKP, cls.MaCB, cls.NgayTH, cls.NgayThang, cls.MaCBth, cls.BenhPham,cls.Status }).FirstOrDefault();

            // hiển thị ngày tháng chỉ định từ cột ngaythang trong bảng CLS
            if (qbn != null && qbn.NgayThang != null)
                lblNgayThangChiDinh.Text = "Ngày " + qbn.NgayThang.Value.Day + " tháng " + qbn.NgayThang.Value.Month + " năm " + qbn.NgayThang.Value.Year;
            if (qbn != null && qbn.NgayTH != null)
                celNgayky.Text = "Ngày " + qbn.NgayTH.Value.Day + " tháng " + qbn.NgayTH.Value.Month + " năm " + qbn.NgayTH.Value.Year;
            if (qbn != null)
            {
                if (DungChung.Bien.MaBV == "12122")
                {
                    if (qbn.Status == 0)
                    {
                        celNgayky.Visible = false;
                        xrTableCell81.Visible = false;
                        colTK.Visible = false;
                    }
                    else
                    {
                        lblNgayThangChiDinh.Visible = false;
                        xrTableCell80.Visible = false;
                        colNLB.Visible = false;
                    }
                }
                // Hiển thị thông tin chung bệnh nhân
                if (qbn.CapCuu == 1)
                    celCapCuu.Text = "X";
                else
                    celThuong.Text = "X";
                lblBenhPham.Text = "Bệnh phẩm: " + qbn.BenhPham;

                celTenNguoiBenh.Text = qbn.TenBNhan;
                celTuoi.Text = qbn.Tuoi.ToString();
                if (qbn.SThe != null && qbn.SThe.Length == 15)
                {
                    BH1.Text = qbn.SThe.Substring(0, 3);
                    BH2.Text = qbn.SThe.Substring(3, 2);
                    BH3.Text = qbn.SThe.Substring(5, 2);
                    BH4.Text = qbn.SThe.Substring(7, 3);
                    BH5.Text = qbn.SThe.Substring(10, 5);
                }

                if (qbn.GTinh == 1)
                    lblNu.Visible = true;
                else
                    lblNam.Visible = true;
                celDiaChi.Text = qbn.DChi;

                #region tạm bỏ điều kiện ngày tháng
                ////Join với bảng BNKB để lấy ra thông tin chẩn đoán, buồng, giường ( điều kiện CLS ( mã bệnh nhân, mã khoa phòng, ngày tháng) phải bằng với các trường trên trong bảng BNKB             
                //var qbnkb = data.BNKBs.Where(p => p.MaBNhan == qbn.MaBNhan && p.MaKP == qbn.MaKP).ToList();
                //var qkb = qbnkb.Where(p => (p.NgayKham != null && qbn.NgayThang != null) && p.NgayKham.Value.Date == qbn.NgayThang.Value.Date).FirstOrDefault();
                //if (qkb != null)
                //{
                //    celChanDoan.Text = qkb.ChanDoan;
                //    celBuong.Text = qkb.Buong;
                //    celGiuong.Text = qkb.Giuong;

                //}
                //Join với bảng BNKB để lấy ra thông tin chẩn đoán, buồng, giường ( điều kiện CLS ( mã bệnh nhân, mã khoa phòng) phải bằng với các trường trên trong bảng BNKB             
                var qbnkb = data.BNKBs.Where(p => p.MaBNhan == qbn.MaBNhan && p.MaKP == qbn.MaKP).FirstOrDefault();
                if (qbnkb != null)
                {
                    celChanDoan.Text = qbnkb.ChanDoan;
                    celBuong.Text = qbnkb.Buong;
                    celGiuong.Text = qbnkb.Giuong;
                }
                #endregion
                //Lấy ra tên khoa phòng chỉ định
                var qKhoa = data.KPhongs.Where(p => p.MaKP == qbn.MaKP).FirstOrDefault();
                if (qKhoa != null)
                    celKhoa.Text = qKhoa.TenKP;

                // lấy ra tên bác sĩ điều trị (chỉ định)
                var qbs = data.CanBoes.Where(p => p.MaCB == qbn.MaCB).FirstOrDefault();
                if (qbs != null)
                    colNLB.Text = qbs.TenCB;
                // lấy ra bác sỹ thực hiện xét nghiệm
                var qbsTH = data.CanBoes.Where(p => p.MaCB == qbn.MaCBth).FirstOrDefault();
                if (qbsTH != null)
                    colTK.Text = qbsTH.TenCB;
                // Lấy ra kết quả xét nghiệm
                var qcls = (from cl in data.CLS.Where(p => p.IdCLS == idCLS)
                            join cd in data.ChiDinhs on cl.IdCLS equals cd.IdCLS
                            join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                            join dvct in data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                            join dv in data.DichVus on dvct.MaDV equals dv.MaDV
                            select new { cl.IdCLS, cl.MaKPth, cl.MaKP, cl.MaCB, cl.NgayThang, clsct.MaDVct, clsct.KetQua, clsct.STTHT, dv.SoTT, dv.MaDV, dv.TenDV, STT_F = dvct.STT }).ToList();// dvct.STT là số tự phiếu, 
                //var qdv = (from dv in qcls group dv by new { dv.MaDV, dv.TenDV, dv.SoTT } into kq select new { kq.Key.MaDV, kq.Key.TenDV, kq.Key.SoTT }).OrderBy(p => p.SoTT).ToList();
                
                var qdv = (from dv in data.DichVus.Where(p => p.SoTT != null) join tn in data.TieuNhomDVs.Where(p => p.TenRG == "XN tế bào nước dịch") on dv.IdTieuNhom equals tn.IdTieuNhom select dv).OrderBy(p => p.SoTT).ToList();
                #region in xét nghiệm nước tiểu
                if (qdv.Count > 0)
                {
                    var q = qcls.Where(p => p.MaDV == qdv.First().MaDV).OrderBy(p => p.STT_F).ToList();

                    foreach (XRTableCell cell in xrTableRow9)
                    {
                        string celName = cell.Name;
                        for (int c = 1; c <= 8; c++)
                        {
                            if (c <= q.Count)
                            {
                                if (celName == "cel_Nuoctieu" + c.ToString())
                                {
                                    var kqua = q.Skip(c - 1).Take(1).Select(p => p.KetQua).FirstOrDefault();
                                    cell.Text = kqua == null ? "" : kqua.ToString();
                                    break;
                                }
                            }
                            else
                            {

                                cell.Text = "";
                                break;
                            }
                        }


                    }

                    foreach (XRTableCell cell in xrTableRow12)
                    {
                        string celName = cell.Name;
                        for (int c = 1; c <= 7; c++)
                        {
                            if ((c + 8) <= q.Count)
                            {
                                if (celName == "cel_Nuoctieu2" + c.ToString())
                                {
                                    var kqua = q.Skip(c + 7).Take(1).Select(p => p.KetQua).FirstOrDefault();
                                    cell.Text = kqua == null ? "" : kqua.ToString();
                                    break;
                                }
                            }
                            else
                            {
                                cell.Text = "";
                                break;
                            }
                        }


                    }
                }
                #endregion
                #region in nước não tủy
                if (DungChung.Bien.MaBV == "12122")
                {
                    
                    var dv = qdv.Skip(1).FirstOrDefault();

                    var _ldvct = (from q in qcls.Where(p => p.MaDVct.Trim().ToLower().Contains("nnt")&&p.MaDV==dv.MaDV)
                                  select q).OrderBy(p => p.STT_F).ToList();
                    foreach (XRTableRow row in xrTable7)
                    {
                        foreach (XRTableCell cell in row)
                        {
                            string celName = cell.Name;
                            for (int c = 1; c <= 6; c++)
                            {
                                if (c <= _ldvct.Count)
                                {
                                    if (celName == "celNT" + c.ToString())
                                    {
                                        var kqua = _ldvct.Skip(c - 1).Take(1).Select(p => p.KetQua).FirstOrDefault();
                                        cell.Text = kqua == null ? "" : kqua.ToString();
                                        break;
                                    }
                                }
                                else
                                {
                                    cell.Text = "";
                                    break;
                                }
                            }
                        }


                    }

                    foreach (XRTableRow row in xrTable13)
                    {
                        foreach (XRTableCell cell in row)
                        {
                            string celName = cell.Name;
                            for (int c = 1; c <= 5; c++)
                            {
                                if (c <= _ldvct.Count)
                                {
                                    if (celName == "celDK" + c.ToString())
                                    {
                                        var kqua = _ldvct.Skip(c + 5).Take(1).Select(p => p.KetQua).FirstOrDefault();
                                        cell.Text = kqua == null ? "" : kqua.ToString();
                                        break;
                                    }
                                }
                                else
                                {
                                    cell.Text = "";
                                    break;
                                }
                            }

                        }
                    }


                }
                else
                {


                    if (qdv.Count > 1)
                    {
                        var dv = qdv.Skip(1).Take(1).FirstOrDefault();
                        var q = qcls.Where(p => p.MaDV == dv.MaDV).OrderBy(p => p.STT_F).ToList();

                        foreach (XRTableRow row in xrTable7)
                        {
                            foreach (XRTableCell cell in row)
                            {
                                string celName = cell.Name;
                                for (int c = 1; c <= 6; c++)
                                {
                                    if (c <= q.Count)
                                    {
                                        if (celName == "celNT" + c.ToString())
                                        {
                                            var kqua = q.Skip(c - 1).Take(1).Select(p => p.KetQua).FirstOrDefault();
                                            cell.Text = kqua == null ? "" : kqua.ToString();
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        cell.Text = "";
                                        break;
                                    }
                                }
                            }


                        }


                    }
                #endregion

                    #region in dịch khác
                    if (qdv.Count > 2)
                    {
                        var dv = qdv.Skip(2).Take(1).FirstOrDefault();
                        var q = qcls.Where(p => p.MaDV == dv.MaDV).OrderBy(p => p.STT_F).ToList();

                        foreach (XRTableRow row in xrTable13)
                        {
                            foreach (XRTableCell cell in row)
                            {
                                string celName = cell.Name;
                                for (int c = 1; c <= 5; c++)
                                {
                                    if (c <= q.Count)
                                    {
                                        if (celName == "celDK" + c.ToString())
                                        {
                                            var kqua = q.Skip(c - 1).Take(1).Select(p => p.KetQua).FirstOrDefault();
                                            cell.Text = kqua == null ? "" : kqua.ToString();
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        cell.Text = "";
                                        break;
                                    }
                                }

                            }
                        }


                    }
                }
                #endregion
            }
        }
    }
}
