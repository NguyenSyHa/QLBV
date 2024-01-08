using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class rep_PhieuXN_VKLao_27023 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieuXN_VKLao_27023()
        {
            InitializeComponent();
        }
        internal void dataBindDing(int idCLS)
        {
            lblTenCQ.Text = DungChung.Bien.TenCQ;
            lblTenCQCQ.Text = DungChung.Bien.TenCQCQ;
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var bn = (from benhnhan in data.BenhNhans
                      join cls in data.CLS.Where(p => p.IdCLS == idCLS) on benhnhan.MaBNhan equals cls.MaBNhan
                      join ttbx in data.TTboXungs on benhnhan.MaBNhan equals ttbx.MaBNhan into kq
                      join kp in data.KPhongs on benhnhan.MaKP equals kp.MaKP
                      join kb in data.BNKBs on benhnhan.MaBNhan equals kb.MaBNhan
                      from kq1 in kq.DefaultIfEmpty()
                      select new
                      {
                          benhnhan.NNhap,
                          benhnhan.MaBNhan,
                          benhnhan.SThe,
                          benhnhan.TenBNhan,
                          benhnhan.Tuoi,
                          benhnhan.GTinh,
                          benhnhan.DChi,
                          kb.Giuong,
                          kb.Buong,
                          kb.ChanDoan,
                          kp.TenKP,
                          cls.CapCuu,
                          cls.IdCLS,
                          cls.NgayThang,
                          cls.MaCB,
                          cls.MaCBth,
                          cls.NgayTH,
                          cls.MaKP,
                          cls.GhiChu,
                          cls.ThoiGianLayMau,
                          cls.ThoiGianNhanMau,
                          cls.TrangThaiBP,
                          cls.BenhPham,
                          cls.Status,
                          so_eBTM = kq1 == null ? "" : kq1.So_eTBM,
                          thangTheoDoi = kq1 == null ? 0 : kq1.ThangTheoDoi ?? 0,
                          chanDoanLao = kq1 == null ? 0 : kq1.ChanDoanLao ?? 0,
                          TienSuDTri = kq1 == null ? 0 : kq1.TienSuDTri,
                          TinhTrangH = kq1 == null ? 0 : kq1.TinhTrangH,
                          DTuongLao = kq1 == null ? null : kq1.DTuongLao,
                          sdt = kq1 == null ? "" : kq1.DThoai,
                          MaXa = kq1 == null ? "" : kq1.MaXa,
                          MaHuyen = kq1 == null ? "" : kq1.MaHuyen,
                          MaTinh = kq1 == null ? "" : kq1.MaTinh,
                      }).FirstOrDefault();
            int _maBN = 0;
            int _maKP = 0;
            if (bn != null)
            {
                #region thông tin chung
                var kpYeuCau = data.KPhongs.Where(p => p.MaKP == bn.MaKP).FirstOrDefault();

                if (kpYeuCau != null)
                    //celDonViYeuCau.Text = kpYeuCau.TenKP;
                    celTenBNhan.Text = bn.TenBNhan;
                _maBN = bn.MaBNhan;
                _maKP = kpYeuCau.MaKP;
                if (bn.Tuoi != null)
                    celTuoi.Text = bn.Tuoi.Value.ToString();
                if (bn.GTinh == 1)
                    celNam.Text = "x";
                else if (bn.GTinh == 0)
                    celNu.Text = "x";
                if (bn.CapCuu == true)
                {
                    cellCapCuu.Text = "x";
                }
                else celThuong.Text = "x";
                celDiaChi.Text = bn.DChi;
                if (bn.SThe.Length >= 15)
                {
                    txtThe1.Text = bn.SThe.Substring(0, 3);
                    txtThe2.Text = bn.SThe.Substring(3, 2);
                    txtThe3.Text = bn.SThe.Substring(5, 2);
                    txtThe4.Text = bn.SThe.Substring(7, 3);
                    txtThe5.Text = bn.SThe.Substring(10, 5);
                }
                if (DungChung.Bien.MaBV == "27023")
                    xlbSoIdCLS.Text = so.Value.ToString();
                else
                    xlbSoIdCLS.Text = bn.IdCLS.ToString();
                string[] arrThongTinBNKB = new string[5] { "", "", "", "", "" };
                arrThongTinBNKB = DungChung.Ham.laythongtinBNKB(data, _maBN, _maKP, true);
                celChanDoan.Text = (bn.ChanDoan != null || bn.ChanDoan != "") && DungChung.Bien.MaBV == "24272" ? bn.ChanDoan + ";" : "";
                celChanDoan.Text += DungChung.Ham.FreshString(arrThongTinBNKB[1]);
                celBuong.Text = arrThongTinBNKB[2];
                celGiuong.Text = arrThongTinBNKB[3];
                celKP.Text = arrThongTinBNKB[4];

                celSoDKDT.Text = bn.MaBNhan.ToString();
                cel_eTBM.Text = bn.so_eBTM;
                #endregion
                #region chẩn đoán lao và đối tượng chẩn đoán
                if (bn.chanDoanLao == 1)
                {
                    celChandoan1.Text = "x";
                    //if (bn.DTuongLao != null && bn.DTuongLao > 0)
                    //{
                    //    switch (bn.DTuongLao.Value)
                    //    {
                    //        case 1:
                    //            cel1_1.Text = "x";
                    //            break;
                    //        case 2:
                    //            cel1_2.Text = "x";
                    //            break;
                    //        case 3:
                    //            cel1_3.Text = "x";
                    //            break;
                    //        case 4:
                    //            cel1_4.Text = "x";
                    //            break;
                    //    }
                    //}
                }
                else if (bn.chanDoanLao == 2)
                {
                    celChandoan2.Text = "x";
                    //if (bn.DTuongLao != null && bn.DTuongLao > 0)
                    //{
                    //switch (bn.DTuongLao.Value)
                    //{
                    //    case 1:
                    //        cel2_1.Text = "x";
                    //        break;
                    //    case 2:
                    //        cel2_2.Text = "x";
                    //        break;
                    //    case 3:
                    //        cel2_3.Text = "x";
                    //        break;
                    //    case 4:
                    //        cel2_4.Text = "x";
                    //        break;
                    //    case 5:
                    //        cel2_5.Text = "x";
                    //        break;
                    //    case 6:
                    //        cel2_6.Text = "x";
                    //        break;
                    //    case 7:
                    //        cel2_7.Text = "x";
                    //        break;
                    //    case 8:
                    //        cel2_8.Text = "x";
                    //        break;
                    //    case 9:
                    //        cel2_9.Text = "x";
                    //        break;
                    //}
                    // }
                }
                /*           else if (bn.chanDoanLao == 3)
                           {
                               celChandoan3.Text = "x";
                               if (bn.DTuongLao != null && bn.DTuongLao > 0)
                               {
                                   switch (bn.DTuongLao.Value)
                                   {
                                       case 1:
                                           cel3_1.Text = "x";
                                           break;
                                       case 2:
                                           cel3_2.Text = "x";
                                           break;
                                       case 3:
                                           cel3_3.Text = "x";
                                           break;
                                       case 4:
                                           cel3_4.Text = "x";
                                           break;
                                       case 5:
                                           cel3_5.Text = "x";
                                           break;
                                       case 6:
                                           cel3_6.Text = "x";
                                           break;

                                   }
                               }
                           }
                           else if (bn.chanDoanLao == 4)
                           {
                               celChanDoan4.Text = "x";
                           }
                 */
                #endregion
                #region thônt tin chung (tiếp)

                if (bn.thangTheoDoi > 0)
                {
                    celTheoDoi.Text = "x";
                    celTheoDoiThang.Text = "tháng thứ: " + bn.thangTheoDoi.ToString();
                }

                //if (bn.TienSuDTri != null && bn.TienSuDTri > 0)
                //    celTienSuHon1th.Text = "x";
                //else
                //    celTienSuDuoi1Thang.Text = "x";
                //if (bn.TinhTrangH != null)
                //    celTinhtrangH.Text = bn.TinhTrangH.Value.ToString();
                if (bn.NgayThang != null)
                    celNgayChiDinh.Text = "Ngày " + bn.NgayThang.Value.Day + " tháng " + bn.NgayThang.Value.Month + " năm " + bn.NgayThang.Value.Year;

                var BS = data.CanBoes.Where(p => p.MaCB == bn.MaCB).FirstOrDefault();
                if (BS != null)
                    celBSChiDinh.Text = BS.TenCB;
                var ngth = data.CanBoes.Where(p => p.MaCB == bn.MaCBth).FirstOrDefault();
                if (ngth != null)
                    celTenNgTH.Text = ngth.TenCB;

                #endregion
                #region chỉ định dịch vụ
                var qcd = (from cd in data.ChiDinhs.Where(p => p.IdCLS == idCLS)
                           join dv in data.DichVus on cd.MaDV equals dv.MaDV


                           select new
                           {
                               cd,
                               dv
                           }).FirstOrDefault();// mỗi phiếu chỉ sử dụng cho 1 loại xét nghiệm

                int num = 1;
                var qmauso0 = (from cls in data.CLS.Where(p => p.MaBNhan == _maBN)
                               join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                               join dv in data.DichVus on cd.MaDV equals dv.MaDV
                               join tn in data.TieuNhomDVs.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom) on dv.IdTieuNhom equals tn.IdTieuNhom
                               group cls by new { cls.IdCLS, cls.NgayThang } into kq
                               select new { kq.Key.IdCLS, kq.Key.NgayThang }).OrderBy(p => p.NgayThang).ToList();
                var qmauso = (from a in qmauso0 select new { STT = num++, a.IdCLS }).ToList();

                if (qcd != null)
                {
                    lblSoXN.Text = "Số XN: " + qcd.cd.Mau_Lan_MTruongXN;
                    var mau = qmauso.Where(p => p.IdCLS == idCLS).FirstOrDefault();
                    if (qcd.dv.SoTT == 1)//số thứ tự phiếu
                    {
                        celYC1.Text = "x";
                        if (mau != null)
                            celMau1.Text = mau.STT.ToString();  // qcd.cd.Mau_Lan_MTruongXN;

                    }
                    else if (qcd.dv.SoTT == 2)
                    {
                        celYC2.Text = "x";
                        if (mau != null)
                            celMau2.Text = mau.STT.ToString(); //celMau2.Text = qcd.cd.Mau_Lan_MTruongXN;
                    }

                    else


                        //if (qcd.dv.SoTT == 3)
                        //    celYC3.Text = "x";
                        //else 
                        if (qcd.dv.SoTT == 4)
                            celYC4.Text = "x";
                        else
                            //        if (qcd.dv.SoTT == 5)
                            //    celYC5.Text = "x";
                            //else 
                            if (qcd.dv.SoTT == 6)
                            {
                                celYC6.Text = "x";
                                c.Text = bn.GhiChu;
                            }
                    //else if (qcd.dv.SoTT == 7)
                    //    celYC7.Text = "x";
                    //else if (qcd.dv.SoTT == 8)
                    //    celYC8.Text = "x";
                    //else if (qcd.dv.SoTT == 9)
                    //    celYC9.Text = "x";
                    //else if (qcd.dv.SoTT == 10)
                    //    celYC10.Text = "x";
                    //else if (qcd.dv.SoTT == 11)
                    //    celYC11.Text = "x";
                    //else if (qcd.dv.SoTT == 12)
                    //    celYC12.Text = "x";
                }

                #endregion


                #region Thời gian lấy mẫu, làm xn
                if (bn.ThoiGianLayMau != null)
                    celThoiGianLayMau.Text = bn.ThoiGianLayMau.Value.Hour.ToString("D2") + " giờ " + bn.ThoiGianLayMau.Value.Minute.ToString("D2") + " ngày " + bn.ThoiGianLayMau.Value.ToString("dd/MM/yyyy");
                if (bn.ThoiGianNhanMau != null)
                    celTGNhanMau.Text = bn.ThoiGianNhanMau.Value.Hour.ToString("D2") + " giờ " + bn.ThoiGianNhanMau.Value.Minute.ToString("D2") + " ngày " + bn.ThoiGianNhanMau.Value.ToString("dd/MM/yyyy");
                //if (bn.NgayTH != null)
                //    celTGLamXN.Text = bn.NgayTH.Value.Hour.ToString("D2") + " giờ " + bn.NgayTH.Value.Minute.ToString("D2") + " ngày " + bn.NgayTH.Value.ToString("dd/MM/yyyy");
                // ngày thực hiện:
                //if (bn.NgayTH != null)
                //    celNgayTH.Text = bn.NgayTH.Value.Hour.ToString("D2") + " giờ " + bn.NgayTH.Value.Minute.ToString("D2") + " ngày " + bn.NgayTH.Value.ToString("dd/MM/yyyy");
                //Loại bệnh phẩm
                if (!String.IsNullOrEmpty(bn.BenhPham))
                {
                    string benhpham = bn.BenhPham.Trim().ToLower();
                    if (benhpham == "đờm" || benhpham.Contains("nm") || benhpham == "m")
                        celDom.Text = "x";
                    else
                        celBPhanKhac.Text = "x";
                }
                #endregion

                #region lấy kết quả
                var qclsct = (from cls in data.CLScts.Where(p => p.IDCD == qcd.cd.IDCD)
                              join dvct in data.DichVucts on cls.MaDVct equals dvct.MaDVct
                              select new { cls.MaDVct, cls.KetQua, cls.STTHT, dvct.STT, cls.Status }).OrderBy(p => p.STTHT).ToList();
                if (qcd != null)
                {
                    //nhuộm soi
                    if (qcd.dv.SoTT == 1 || qcd.dv.SoTT == 2)
                    {
                        celBenhPham1.Text = bn.TrangThaiBP;
                        var qdv = qclsct.FirstOrDefault();

                        if (qdv != null && qdv.Status == 1 && qdv.KetQua != null)
                        {
                            string kq = qdv.KetQua.Trim().ToLower();
                            if (kq == "âm tính")
                                celkq11.Text = "x";
                            else if (kq == "1+")
                                celkq13.Text = "x";
                            else if (kq == "2+")
                                celkq14.Text = "x";
                            else if (kq == "3+")
                                celkq15.Text = "x";
                            else
                                celkq12.Text = qdv.KetQua;
                        }

                    }
                    //RMP xpert
                    else if (qcd.dv.SoTT == 6)
                    {
                        celBenhpham2.Text = bn.TrangThaiBP;
                        if (qclsct.FirstOrDefault() != null && qclsct.First().Status == 1)
                        {
                            var strkq = qclsct.FirstOrDefault();
                            if (strkq.STTHT == 1)
                                celkq21.Text = "x";
                            else if (strkq.STTHT == 2)
                                celkq22.Text = "x";
                            else if (strkq.STTHT == 3)
                                celkq23.Text = "x";
                            else if (strkq.STTHT == 4)
                                celkq24.Text = "x";
                            else
                                celkq25.Text = strkq.KetQua;
                        }
                    }

                    //nuôi cấy
                    else if (qcd.dv.SoTT == 3 || qcd.dv.SoTT == 4)
                    {
                        celBenhPham3.Text = bn.TrangThaiBP;
                        if (qclsct.First().Status == 1)
                        {
                            if (qclsct.Where(p => p.STT == 1).Count() > 0)
                            {
                                var strkq = qclsct.Where(p => p.STT == 1).FirstOrDefault();

                                if (strkq.STTHT == 1)
                                    celkq31.Text = "x";
                                if (strkq.STTHT == 2)
                                    celkq32.Text = "x";
                                if (strkq.STTHT == 3)
                                    celkq33.Text = "x";

                            }
                            if (qclsct.Where(p => p.STT == 2).Count() > 0)
                                celkq34.Text = qclsct.Where(p => p.STT == 2).FirstOrDefault().KetQua;
                            if (qclsct.Where(p => p.STT == 3).Count() > 0)
                                celkq35.Text = qclsct.Where(p => p.STT == 3).FirstOrDefault().KetQua;
                        }
                        //if (qclsct.FirstOrDefault() != null)
                        //{
                        //    var strkq = qclsct.FirstOrDefault();
                        //    if (strkq.STTHT == 1)
                        //        celkq31.Text = "x";
                        //    if (strkq.STTHT == 2)
                        //        celkq32.Text = "x";
                        //    if (strkq.STTHT == 3)
                        //        celkq33.Text = "x";

                        //}
                        //if (qclsct.Count > 1)// kết quả nuôi cấy MTB
                        //{
                        //    var strkq1 = qclsct.Skip(1).First();
                        //    celkq34.Text = strkq1.KetQua;
                        //}

                        //if (qclsct.Count > 2)// kết quả nuôi cấy NTM
                        //{
                        //    var strkq1 = qclsct.Skip(2).First();
                        //    celkq35.Text = strkq1.KetQua;
                        //}
                    }
                    //NTM định danh
                    //else if (qcd.dv.SoTT == 5)
                    //{
                    //    if (qclsct.FirstOrDefault() != null)
                    //    {
                    //        string strkq = qclsct.FirstOrDefault().KetQua;
                    //        celKqNTM.Text = strkq;
                    //    }
                    //}

                    //đa kháng
                    //else if (qcd.dv.SoTT == 11)
                    //{
                    //    celBenhPham4.Text = bn.TrangThaiBP;
                    //    int kt = 0;// kiểm tra có kháng thuốc  ko
                    //    if (qclsct.FirstOrDefault() != null)
                    //    {
                    //        var strkq = qclsct.FirstOrDefault();
                    //        celkq41.Text = strkq.KetQua;
                    //        if (!String.IsNullOrEmpty(strkq.KetQua))
                    //            kt++;//kháng thuốc Isoniazid
                    //    }
                    //    if (qclsct.Count > 1)
                    //    {
                    //        var strkq = qclsct.Skip(1).FirstOrDefault();
                    //        celKQ42.Text = strkq.KetQua;
                    //        if (!String.IsNullOrEmpty(strkq.KetQua))
                    //            kt++;//kháng thuốc rifampicin
                    //    }
                    //    if (qclsct.Count > 2)
                    //    {
                    //        var strkq = qclsct.Skip(2).FirstOrDefault();
                    //        celkq43.Text = strkq.KetQua;
                    //        if (!String.IsNullOrEmpty(strkq.KetQua))
                    //            kt++;//kháng thuốc fluorquinilones
                    //    }
                    //    if (kt > 0)
                    //        celKCoMTB.Text = "x";
                    //    else
                    //        celMTB.Text = "x";
                    //}

                    //siêu kháng
                    //else if (qcd.dv.SoTT == 12)
                    //{
                    //    celBenhPham4.Text = bn.BenhPham;
                    //    int kt = 0;// kiểm tra có kháng thuốc  ko
                    //    if (qclsct.FirstOrDefault() != null)
                    //    {
                    //        var strkq = qclsct.FirstOrDefault();
                    //        celkq44.Text = strkq.KetQua;
                    //        if (!String.IsNullOrEmpty(strkq.KetQua))
                    //            kt++;//kháng thuốc Capremycin
                    //    }
                    //    if (qclsct.Count > 1)
                    //    {
                    //        var strkq = qclsct.Skip(1).FirstOrDefault();
                    //        celkq45.Text = strkq.KetQua;

                    //        if (!String.IsNullOrEmpty(strkq.KetQua))
                    //            kt++;//kháng thuốc Vlomycin
                    //    }
                    //    if (qclsct.Count > 2)
                    //    {
                    //        var strkq = qclsct.Skip(2).FirstOrDefault();
                    //        celkq46.Text = strkq.KetQua;
                    //        if (!String.IsNullOrEmpty(strkq.KetQua))
                    //            kt++;//kháng thuốc Amikacin
                    //    }
                    //    if (kt > 0)
                    //        celKCoMTB.Text = "x";
                    //    else
                    //        celMTB.Text = "x";
                    //}
                    ////Kháng thuốc hàng 1
                    //else if (qcd.dv.SoTT == 7 || qcd.dv.SoTT == 8)
                    //{
                    //    if (qclsct.FirstOrDefault() != null)
                    //    {
                    //        var strkq = qclsct.FirstOrDefault();
                    //        celkq51.Text = strkq.KetQua;
                    //    }
                    //    if (qclsct.Count > 1)
                    //    {
                    //        var strkq = qclsct.Skip(1).FirstOrDefault();
                    //        celkq52.Text = strkq.KetQua;
                    //    }
                    //    if (qclsct.Count > 2)
                    //    {
                    //        var strkq = qclsct.Skip(2).FirstOrDefault();
                    //        celkq53.Text = strkq.KetQua;
                    //    }
                    //    if (qclsct.Count > 3)
                    //    {
                    //        var strkq = qclsct.Skip(3).FirstOrDefault();
                    //        celkq54.Text = strkq.KetQua;
                    //    }
                    //    if (qclsct.Count > 4)
                    //    {
                    //        var strkq = qclsct.Skip(4).FirstOrDefault();
                    //        celkq55.Text = strkq.KetQua;
                    //    }
                    //}

                    //Kháng thuốc hàng 2
                    //else if (qcd.dv.SoTT == 9 || qcd.dv.SoTT == 10)
                    //{
                    //    if (qclsct.FirstOrDefault() != null)
                    //    {
                    //        var strkq = qclsct.FirstOrDefault();
                    //        celkq56.Text = strkq.KetQua;
                    //    }
                    //    if (qclsct.Count > 1)
                    //    {
                    //        var strkq = qclsct.Skip(1).FirstOrDefault();
                    //        celkq57.Text = strkq.KetQua;
                    //    }
                    //    if (qclsct.Count > 2)
                    //    {
                    //        var strkq = qclsct.Skip(2).FirstOrDefault();
                    //        celkq58.Text = strkq.KetQua;
                    //    }
                    //    if (qclsct.Count > 3)
                    //    {
                    //        var strkq = qclsct.Skip(3).FirstOrDefault();
                    //        celkq59.Text = strkq.KetQua;
                    //    }

                    //}

                }

                #endregion
            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
        }
    }
}
