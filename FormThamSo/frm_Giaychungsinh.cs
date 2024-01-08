using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QLBV.BaoCao;
using QLBV.Models.Business.DataCommunication;
using QLBV.Providers.Business.Datacommunication;
using System.Threading.Tasks;
using QLBV.Utilities.Commons;
using System.IO;

namespace QLBV.FormThamSo
{
    public partial class frm_Giaychungsinh : DevExpress.XtraEditors.XtraForm
    {
        private readonly GiayChungSinh_Provider _GiayChungSinh_Provider;
        public frm_Giaychungsinh()
        {
            InitializeComponent();
        }
        int Mabn = 0;
        int socon = 0;
        public frm_Giaychungsinh(int _Mabn)
        {
            InitializeComponent();
            _GiayChungSinh_Provider = new GiayChungSinh_Provider();
            Mabn = _Mabn;
        }

        private void frm_Giaychungsinh_Load(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                var bnrv = data.RaViens.Where(p => p.MaBNhan == Mabn).ToList();

                if (bnrv.Count() > 0)
                {
                    XtraMessageBox.Show("Chú ý: Bệnh nhân đã ra viện!!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btn_Lưu.Visible = false;
                    btn_Sua.Visible = false;
                    btn_KLuu.Visible = false;
                }
            }
            if(DungChung.Bien.MaBV == "30004")
            {
                btnPost_GCS.Visible = true;
                TheoDoiThaiSan tdts = data.TheoDoiThaiSans.Where(p => p.MaBNhan == Mabn).FirstOrDefault();
                if (tdts.Status == true)
                    btnPost_GCS.Enabled = false;
            }
            var dtoc = data.DanTocs.ToList();
            lupDT.Properties.DataSource = dtoc;

            btn_IN.Enabled = true;
            btn_Thoat.Enabled = true;
            btn_Sua.Enabled = true;
            btn_Lưu.Enabled = false;
            btn_KLuu.Enabled = false;
            txtSoCSinh.Enabled = false;
            txtHoTenMe.Enabled = false;
            txtNamSinh.Enabled = false;
            lupDT.Enabled = false;
            txtCMT.Enabled = false;
            txtNgayCap.Enabled = false;
            txtNoiCap.Enabled = false;
            txtBV.Enabled = false;
            txtNguoiGhiPhieu.Enabled = false;
            txtNguoiDoDe.Enabled = false;
            txtNoiDKThuongTru.Enabled = false;
            txtNoiCap.Enabled = false;
            txtHoTenCha.Enabled = false;
            txtSinhLuc.Enabled = false;
            txtSoCon.Enabled = false;
            txtGioiTinh1.Enabled = false;
            txtGioiTinh2.Enabled = false;
            txtGioiTinh3.Enabled = false;
            txtGioiTinh4.Enabled = false;
            txtCanNang1.Enabled = false;
            txtCanNang2.Enabled = false;
            txtCanNang3.Enabled = false;
            txtCanNang4.Enabled = false;
            txtTenCon1.Enabled = false;
            txtTenCon2.Enabled = false;
            txtTenCon3.Enabled = false;
            txtTenCon4.Enabled = false;
            txtGhiChu.Enabled = false;
            dtNgayCapGCS.Enabled = false;
            txtSoLanSinh.Enabled = false;
            txtSoConSong.Enabled = false;
            chkSinhConDuoi32Tuan.Enabled = false;
            chkSinhConPhauThuat.Enabled = false;

            txtQuyenSo.Enabled = false;
            txtBV.Text = DungChung.Bien.TenCQ + " - " + DungChung.Bien.DiaChi; //HIS-1430
            txtSinhLuc.DateTime = DateTime.Now;

            #region 30005
            if (DungChung.Bien.MaBV == "30005")
            {
                var bn = (from a in data.BenhNhans.Where(p => p.MaBNhan == Mabn)
                          join b in data.TTboXungs on a.MaBNhan equals b.MaBNhan
                          join c in data.DanTocs on b.MaDT equals c.MaDT into kq
                          from k1 in kq.DefaultIfEmpty()
                          select new { a.MaBNhan, a.TenBNhan, a.NamSinh, b.MaDT, a.SThe, a.DChi, b.SoKSinh, b.NgayCapCMT, TenDT = k1 != null ? k1.TenDT : "", b.NThan, b.TTGiayCSinh }).ToList();
                string[] arr = { "", "", "", "", "", "", "", "", "", "", "" };
                if (!string.IsNullOrWhiteSpace(bn.FirstOrDefault().TTGiayCSinh))
                    arr = bn.FirstOrDefault().TTGiayCSinh.Split('|');
                txtQuyenSo.Text = arr[0];
                txtSoCSinh.Text = arr[1];
                txtHoTenMe.Text = bn.FirstOrDefault().TenBNhan.ToUpper();
                txtNamSinh.Text = bn.FirstOrDefault().NamSinh;
                txtNoiDKThuongTru.Text = arr[2];
                txtBHYT.Text = bn.FirstOrDefault().SThe;
                txtCMT.Text = bn.FirstOrDefault().SoKSinh;
                txtNgayCap.Text = bn.FirstOrDefault().NgayCapCMT != null ? (bn.FirstOrDefault().NgayCapCMT.Value.Day + "/" + bn.FirstOrDefault().NgayCapCMT.Value.Month + "/" + bn.FirstOrDefault().NgayCapCMT.Value.Year) : "";
                txtNoiCap.Text = arr[3];
                lupDT.EditValue = bn.FirstOrDefault().MaDT;
                txtHoTenCha.Text = arr[4];
                if (arr[5] != "")
                    txtSinhLuc.DateTime = Convert.ToDateTime(arr[5]);
                txtSoCon.Text = arr[6] == "" ? "1" : arr[6];

                string[] arr1 = { "", "", "", "" };
                if (!string.IsNullOrWhiteSpace(arr[7]))
                    arr1 = arr[7].Split(';');
                txtGioiTinh1.Text = arr1[0];
                txtGioiTinh2.Text = arr1[1];
                txtGioiTinh3.Text = arr1[2];
                txtGioiTinh4.Text = arr1[3];

                if (!string.IsNullOrWhiteSpace(arr[8]))
                    arr1 = arr[8].Split(';');
                txtCanNang1.Text = arr1[0];
                txtCanNang2.Text = arr1[1];
                txtCanNang3.Text = arr1[2];
                txtCanNang4.Text = arr1[3];

                if (!string.IsNullOrWhiteSpace(arr[9]))
                    arr1 = arr[9].Split(';');
                txtTenCon1.Text = arr1[0];
                txtTenCon2.Text = arr1[1];
                txtTenCon3.Text = arr1[2];
                txtTenCon4.Text = arr1[3];

                txtGhiChu.Text = arr[10];
                txtBV.Text = DungChung.Bien.TenCQ;
                txtSoCon_SelectedIndexChanged(sender, e);
            }
            #endregion

            else
            {
                txtCanNang1.Properties.Mask.EditMask = "[0-9]{0,4}";
                txtCanNang1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                txtCanNang2.Properties.Mask.EditMask = "[0-9]{0,4}";
                txtCanNang2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                txtCanNang3.Properties.Mask.EditMask = "[0-9]{0,4}";
                txtCanNang3.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                txtCanNang4.Properties.Mask.EditMask = "[0-9]{0,4}";
                txtCanNang4.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;

                var bn = (from a in data.BenhNhans.Where(p => p.MaBNhan == Mabn) select new { a.MaBNhan, a.TenBNhan, a.NamSinh, a.SThe, a.DChi }).ToList();
                var ttbx = (from b in data.TTboXungs.Where(p => p.MaBNhan == Mabn)
                            join c in data.DanTocs on b.MaDT equals c.MaDT into kq
                            from k1 in kq.DefaultIfEmpty()
                            select new { b.SoKSinh, b.NoiCapCMT, b.HKTT, b.MaDT, b.NgayCapCMT, TenDT = k1 != null ? k1.TenDT : "", b.NThan, b.TTGiayCSinh }).FirstOrDefault();

                txtHoTenMe.Text = bn.FirstOrDefault().TenBNhan.ToUpper();
                txtNamSinh.Text = bn.FirstOrDefault().NamSinh;
                if (ttbx.HKTT != null || ttbx.HKTT != "")
                {
                    txtNoiDKThuongTru.Text = ttbx.HKTT;
                }
                else
                    txtNoiDKThuongTru.Text = bn.FirstOrDefault().DChi;
                txtBHYT.Text = bn.FirstOrDefault().SThe;
                if (ttbx != null)
                {
                    txtCMT.Text = ttbx.SoKSinh;
                    txtNgayCap.Text = ttbx.NgayCapCMT != null ? (ttbx.NgayCapCMT.Value.Day + "/" + ttbx.NgayCapCMT.Value.Month + "/" + ttbx.NgayCapCMT.Value.Year) : "";
                    txtNoiCap.Text = ttbx.NoiCapCMT;
                    lupDT.EditValue = ttbx.MaDT;
                }
                TheoDoiThaiSan tdts = data.TheoDoiThaiSans.Where(p => p.MaBNhan == Mabn).FirstOrDefault();
                if (tdts != null)
                {
                    btn_Sua.Text = "Sửa";
                    if (tdts.ThoiGianSinh != null)
                    {
                        DateTime NgaySinh = tdts.ThoiGianSinh.Value;
                        txtSinhLuc.DateTime = NgaySinh;
                        txtMaGCS.Text = tdts.SoChungSinh + ".GCS." + DungChung.Bien.MaBV + "." + NgaySinh.ToString("yy");
                    }
                    txtSoCSinh.Text = tdts.SoChungSinh;
                    txtNguoiDoDe.Text = tdts.NguoiDoDe;
                    txtNguoiGhiPhieu.Text = tdts.NguoiGhiPhieu;
                    txtQuyenSo.Text = tdts.QuyenSo;
                    txtHoTenCha.Text = tdts.TenBo;
                    if (tdts.SinhCon_Duoi32Tuan != null)
                    {
                        chkSinhConDuoi32Tuan.Checked = (bool)tdts.SinhCon_Duoi32Tuan;
                    }
                    if (tdts.SinhCon_PhauThuat != null)
                    {
                        chkSinhConPhauThuat.Checked = (bool)tdts.SinhCon_PhauThuat;
                    }

                    if (tdts.SoCon != null)
                    {
                        txtSoCon.Text = tdts.SoCon.ToString();

                        if (tdts.SoCon > 0)
                        {
                            txtGioiTinh1.SelectedIndex = tdts.GioiTinhCon1 == 1 ? 0 : (tdts.GioiTinhCon1 == 0 ? 1 : 2);
                            txtCanNang1.Text = tdts.CanNang1.ToString();
                            txtTenCon1.Text = tdts.Ten1;


                        }
                        if (tdts.SoCon > 1)
                        {
                            txtGioiTinh2.SelectedIndex = tdts.GioiTinhCon2 == 1 ? 0 : (tdts.GioiTinhCon2 == 0 ? 1 : 2);
                            txtCanNang2.Text = tdts.CanNang2.ToString();
                            txtTenCon2.Text = tdts.Ten2;
                        }
                        if (tdts.SoCon > 2)
                        {
                            txtGioiTinh3.SelectedIndex = tdts.GioiTinhCon3 == 1 ? 0 : (tdts.GioiTinhCon3 == 0 ? 1 : 2);
                            txtCanNang3.Text = tdts.CanNang3.ToString();
                            txtTenCon3.Text = tdts.Ten3;
                        }
                        if (tdts.SoCon > 3)
                        {
                            txtGioiTinh4.SelectedIndex = tdts.GioiTinhCon4 == 1 ? 0 : (tdts.GioiTinhCon4 == 0 ? 1 : 2);
                            txtCanNang4.Text = tdts.CanNang4.ToString();
                            txtTenCon4.Text = tdts.Ten4;
                        }
                    }

                    txtGhiChu.Text = tdts.GhiChu;
                    txtSoLanSinh.Text = Convert.ToString(tdts.LanDe);
                    txtSoConSong.Text = Convert.ToString(tdts.SoConTT);
                    dtNgayCapGCS.DateTime = (DateTime)tdts.NgayCT;
                    if (tdts.DiaDiemSinh != null || tdts.DiaDiemSinh != "")
                    {
                        txtBV.Text = tdts.DiaDiemSinh;
                    }
                    else
                        txtBV.Text = DungChung.Bien.TenCQ + " - " + DungChung.Bien.DiaChi; //HIS - 1430

                }
                else
                {
                    btn_Sua.Text = "Thêm mới";
                    txtSoCon.SelectedIndex = 0;
                    dtNgayCapGCS.DateTime = DateTime.Now;
                    if (ttbx != null && !string.IsNullOrEmpty(ttbx.MaDT))
                    {
                        lupDT.EditValue = ttbx.MaDT;
                    }
                    else
                        lupDT.EditValue = lupDT.Properties.GetKeyValueByDisplayText("Kinh");
                    var qsobl = data.SoBienLais.Where(p => p.PLoai == 3 && p.Status == 1).FirstOrDefault();
                    var so = data.TheoDoiThaiSans.OrderByDescending(p => p.ThoiGianSinh).ThenBy(p => p.SoChungSinh).FirstOrDefault();
                    if (so != null)
                    {
                        int SoChungSinh = Convert.ToInt32(so.SoChungSinh);
                        txtQuyenSo.Text = "1";
                        DateTime ThoiGianSinh = so.ThoiGianSinh.Value;
                        if (DungChung.Bien.MaBV != "26007")
                        {
                            if (DateTime.Now.Year != ThoiGianSinh.Year)
                            {
                                txtSoCSinh.Text = "00001";
                            }
                            else
                            {
                                txtSoCSinh.Text = (SoChungSinh + 1).ToString("d5");
                            }
                        }
                    }
                    else
                        txtSoCSinh.Text = "00001";
                    //if (qsobl != null && qsobl.SoHT >= 0)
                    //{
                    //    txtQuyenSo.Text = qsobl.Quyen;
                    //    if (DungChung.Bien.MaBV != "26007")
                    //    {
                    //        txtSoCSinh.Text = (qsobl.SoHT + 1).ToString();
                    //    }

                    //}
                    //else
                    //    MessageBox.Show("Bạn chưa thiết lập số chứng sinh");
                    //var qso = data.SoPLs.Where(p => p.PhanLoai == 11).LastOrDefault();
                    //if (qso == null)
                    //{
                    //    SoPL moi = new SoPL();
                    //    moi.PhanLoai = 11;
                    //    moi.MaKP = 0;
                    //    moi.SoPL1 = 1;
                    //    moi.Status = 1;
                    //    moi.NgayNhap = DateTime.Now;
                    //    data.SoPLs.Add(moi);
                    //    data.SaveChanges();
                    //    txtSoCSinh.Text = "1";
                    //}
                    //else
                    //{
                    //    txtSoCSinh.Text = (qso.SoPL1 + 1).ToString();
                    //}

                }
            }
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void btn_IN_Click(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "30005")
            {
                frmIn frm = new frmIn();
                BaoCao.rep_GiayChungSinh rep = frm_Giaychungsinh.in2lien(data, Mabn);
                BaoCao.rep_GiayChungSinh rep2 = frm_Giaychungsinh.in2lien(data, Mabn);
                rep.Pages.AddRange(rep2.Pages);
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                var qcs = data.TheoDoiThaiSans.Where(p => p.MaBNhan == Mabn).FirstOrDefault();
                var qbn = data.BenhNhans.Where(p => p.MaBNhan == Mabn).FirstOrDefault();
                var qttbx = data.TTboXungs.Where(p => p.MaBNhan == Mabn).FirstOrDefault();

                var ht = data.HTHONGs.Where(p => p.MaBV == DungChung.Bien.MaBV).FirstOrDefault();

                if (qcs != null && qbn != null)
                {
                    if (qcs.SoCon != null)
                    {
                        for (int i = 1; i <= qcs.SoCon; i++)
                        {
                            frmIn frm = new frmIn();
                            rep_GiayChungSinh_TT56 rep = new rep_GiayChungSinh_TT56();

                            rep.TenCQ30012.Value = DungChung.Bien.TenCQ;

                            rep.SoChungSinh.Value = "Số: ";
                            rep.Mabn.Value = Mabn;
                            rep.NoiDangKyThuongTru.Value = "Nơi đăng ký thường trú: ";
                            rep.SoCMT.Value = "Giấy CMND/Thẻ căn cước/Hộ chiếu số:";
                            rep.NgayCap.Value = "Ngày cấp:                  Nơi cấp:";
                            rep.ThoigianSinh.Value = "Đã sinh con vào lúc   giờ   phút, ngày   tháng   năm     ";
                            rep.DiaDiemSinh.Value = "Tại: ";

                            rep.GioiTinh.Value = "Giới tính của con:             Cân nặng:      ";
                            rep.GhiChu.Value = "Ghi chú: ";
                            rep.NgayThang.Value = "ngày    tháng   năm       ";

                            //-----------------------------------------------
                            rep.SoChungSinh.Value = "Số: " + (qcs.SoChungSinh ?? "");
                            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                                rep.Quyen.Value = "Quyển số: " + (qcs.QuyenSo ?? "");
                            else
                                rep.Quyen.Value = "Quyển: " + (qcs.QuyenSo ?? "");
                            rep.NamSinh.Value = qbn.NamSinh;
                            rep.HoTenMe.Value = txtHoTenMe.Text;
                            rep.NoiDangKyThuongTru.Value = "Nơi đăng ký thường trú: " + (txtNoiDKThuongTru.Text ?? "");
                            rep.SoThe.Value = qbn.SThe;

                            //30012   
                            int b = 0;
                            if (qbn.DTuong == "Dịch vụ")
                            {
                                b = 1;
                                rep.check(b);
                            }
                            if (b == 1 && qbn.SThe != "" && qbn.SThe != null)
                            {
                                string a = qbn.SThe;
                                rep.SoTheBH1.Value = a.Substring(0, 2);
                                rep.SoTheBH2.Value = a.Substring(2, 1);
                                rep.SoTheBH3.Value = a.Substring(3, 2);
                                rep.SoTheBH4.Value = a.Substring(5, 10);
                            }

                            if (qttbx != null)
                            {
                                rep.SoCMT.Value = "Giấy CMND/Thẻ căn cước/Hộ chiếu số: " + qttbx.SoKSinh;
                                rep.NgayCap.Value = "Ngày cấp: " + (qttbx.NgayCapCMT == null ? "                " : qttbx.NgayCapCMT.Value.ToString("dd/MM/yyyy")) + "            Nơi cấp: " + (qttbx.NoiCapCMT == null ? "" : qttbx.NoiCapCMT);
                            }
                            rep.DanToc.Value = lupDT.Text;
                            rep.HoTenCha.Value = qcs.TenBo;
                            if (qcs.ThoiGianSinh != null)
                                rep.ThoigianSinh.Value = "Đã sinh con vào lúc " + qcs.ThoiGianSinh.Value.Hour + " giờ " + qcs.ThoiGianSinh.Value.Minute + " phút, ngày " + qcs.ThoiGianSinh.Value.Day + " tháng " + qcs.ThoiGianSinh.Value.Month + " năm " + qcs.ThoiGianSinh.Value.Year;
                            rep.DiaDiemSinh.Value = "Tại: " + (qcs.DiaDiemSinh ?? "            ");
                            rep.SoCon.Value = qcs.SoCon;
                            if (i == 1)
                            {
                                rep.GioiTinh.Value = "Giới tính của con:  " + (qcs.GioiTinhCon1 == 1 ? "Nam" : (qcs.GioiTinhCon1 == 0 ? "  Nữ" : "Không xác định")) + "      Cân nặng: " + (qcs.CanNang1 == null ? "    " : qcs.CanNang1.ToString().Trim()) + " gram";
                                rep.TenCon.Value = qcs.Ten1;
                            }
                            else if (i == 2)
                            {
                                rep.GioiTinh.Value = "Giới tính của con:  " + (qcs.GioiTinhCon2 == 1 ? "Nam" : (qcs.GioiTinhCon2 == 0 ? "  Nữ" : "Không xác định")) + "      Cân nặng: " + (qcs.CanNang2 == null ? "    " : qcs.CanNang2.ToString().Trim()) + " gram";
                                rep.TenCon.Value = qcs.Ten2;
                            }
                            else if (i == 3)
                            {
                                rep.GioiTinh.Value = "Giới tính của con:  " + (qcs.GioiTinhCon3 == 1 ? "Nam" : (qcs.GioiTinhCon3 == 0 ? "  Nữ" : "Không xác định")) + "      Cân nặng: " + (qcs.CanNang3 == null ? "    " : qcs.CanNang3.ToString().Trim()) + " gram";
                                rep.TenCon.Value = qcs.Ten3;
                            }
                            else if (i == 4)
                            {
                                rep.GioiTinh.Value = "Giới tính của con:  " + (qcs.GioiTinhCon4 == 1 ? "Nam" : (qcs.GioiTinhCon4 == 0 ? "  Nữ" : "Không xác định")) + "      Cân nặng: " + (qcs.CanNang4 == null ? "    " : qcs.CanNang4.ToString().Trim()) + " gram";
                                rep.TenCon.Value = qcs.Ten4;
                            }
                            rep.GhiChu.Value = "Ghi chú: " + (qcs.GhiChu ?? "        ");
                            if (DungChung.Bien.MaBV == "30012")
                            {
                                rep.NgayThang.Value = "Tỉnh Hải Dương, Ngày " + DateTime.Now.Day + "  tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                            }
                            else
                            {
                                rep.NgayThang.Value = "Ngày " + DateTime.Now.Day + "  tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                            }
                            rep.xrTableCell73.Text = "- Giấy chứng sinh cấp lần đầu: Số: " + (qcs.SoChungSinh ?? "         ") + "      Quyển số: " + (qcs.QuyenSo ?? "        ") + "  (nếu cấp lại) ";
                            rep.xrTableCell86.Text = "- Giấy chứng sinh cấp lần đầu: Số: " + (qcs.SoChungSinh ?? "         ") + "      Quyển số: " + (qcs.QuyenSo ?? "        ") + "  (nếu cấp lại) ";
                            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                            {
                                rep.lblKyHoTen1.Multiline = true;
                                rep.lblKyHoTen2.Multiline = true;
                                rep.lblKyHoTen1.Text = "(Ký, ghi rõ" + Environment.NewLine + "chức danh)";
                                rep.lblKyHoTen2.Text = "(Ký, ghi rõ" + Environment.NewLine + "chức danh)";
                            }

                            rep.NguoiDoDe.Value = qcs.NguoiDoDe;
                            rep.NguoiGhiPhieu.Value = qcs.NguoiGhiPhieu;
                            rep.ThuTruong.Value = ht.GiamDoc;



                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                    }
                }


            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            chkSinhConPhauThuat.Enabled = true;
            chkSinhConDuoi32Tuan.Enabled = true;
            txtNoiDKThuongTru.Enabled = true;
            txtNoiCap.Enabled = true;
            txtHoTenCha.Enabled = true;
            txtSinhLuc.Enabled = true;
            txtSoCon.Enabled = true;
            txtGioiTinh1.Enabled = true;
            txtGioiTinh2.Enabled = true;
            txtGioiTinh3.Enabled = true;
            txtGioiTinh4.Enabled = true;
            txtCanNang1.Enabled = true;
            txtCanNang2.Enabled = true;
            txtCanNang3.Enabled = true;
            txtCanNang4.Enabled = true;
            txtTenCon1.Enabled = true;
            txtTenCon2.Enabled = true;
            txtTenCon3.Enabled = true;
            txtTenCon4.Enabled = true;
            txtGhiChu.Enabled = true;
            dtNgayCapGCS.Enabled = true;
            txtSoLanSinh.Enabled = true;
            txtSoConSong.Enabled = true;
            if (DungChung.Bien.MaBV != "26007")
            {
                txtSoCSinh.Enabled = false;
            }
            else
            {
                txtSoCSinh.Enabled = true;
            }

            txtHoTenMe.Enabled = true;
            txtNamSinh.Enabled = true;
            lupDT.Enabled = true;
            txtCMT.Enabled = true;
            txtNgayCap.Enabled = true;
            txtNoiCap.Enabled = true;
            txtBV.Enabled = true;

            //txtSoCSinh.Enabled = true;
            //txtQuyenSo.Enabled = true;
            btn_IN.Enabled = false;
            btn_Thoat.Enabled = false;
            btn_Sua.Enabled = false;
            btn_Lưu.Enabled = true;
            btn_KLuu.Enabled = true;
            txtNguoiDoDe.Enabled = true;
            txtNguoiGhiPhieu.Enabled = true;



        }
        //private bool ktSochungsinh()
        //{
        //var _sochungsinh = (from ttts in data.TheoDoiThaiSans select new { ttts.MaBNhan, ttts.SoChungSinh }).ToList();
        //    string s = txtSoCSinh.Text;
        //    for(int i=0; i<_sochungsinh.Count;i++)
        //    {
        //        if (s == _sochungsinh[i].SoChungSinh) return false;
        //        else return true;
        //    }
        //    return true;
        //}
        private bool ktSochungsinh()
        {
            string s = txtSoCSinh.Text;
            var _sochungsinh = (from ttts in data.TheoDoiThaiSans.Where(p => p.SoChungSinh == s).Where(x => x.MaBNhan == Mabn) select new { ttts.MaBNhan, ttts.SoChungSinh }).ToList();

            if (_sochungsinh.Count > 0)
                return true;
            else
            {
                var _sochungsinh2 = (from ttts in data.TheoDoiThaiSans.Where(p => p.SoChungSinh == s) select new { ttts.MaBNhan, ttts.SoChungSinh }).ToList();
                if (_sochungsinh2.Count > 0) return false;
                else return true;
            }
            return true;
        }

        private void btn_Lưu_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);


            if (txtSoCon.Text != "")
            {
                socon = Convert.ToInt32(txtSoCon.Text);
                #region 30005
                if (DungChung.Bien.MaBV == "30005")
                {
                    string sinhluc = "";
                    if (txtSinhLuc.Text != "")
                        sinhluc = txtSinhLuc.DateTime.ToString("dd/MM/yyyy HH:mm");

                    string gioitinh = txtGioiTinh1.Text + ";" + (socon > 1 ? txtGioiTinh2.Text : "") + ";" + (socon > 2 ? txtGioiTinh3.Text : "") + ";" + (socon > 3 ? txtGioiTinh4.Text : "");
                    string cannang = txtCanNang1.Text + ";" + (socon > 1 ? txtCanNang2.Text : "") + ";" + (socon > 2 ? txtCanNang3.Text : "") + ";" + (socon > 3 ? txtCanNang4.Text : "");
                    string tencon = txtTenCon1.Text + ";" + (socon > 1 ? txtTenCon2.Text : "") + ";" + (socon > 2 ? txtTenCon3.Text : "") + ";" + (socon > 3 ? txtTenCon4.Text : "");
                    string ttchuyensinh = txtQuyenSo.Text + "|" + txtSoCSinh.Text + "|" + txtNoiDKThuongTru.Text + "|" + txtNoiCap.Text + "|" + txtHoTenCha.Text + "|" + sinhluc + "|" + txtSoCon.Text + "|" + gioitinh + "|" + cannang + "|" + tencon + "|" + txtGhiChu.Text;
                    TTboXung ttbx = data.TTboXungs.Single(p => p.MaBNhan == Mabn);
                    ttbx.TTGiayCSinh = ttchuyensinh;
                    data.SaveChanges();
                    frm_Giaychungsinh_Load(sender, e);
                }
                #endregion

                else
                {
                    //if (!ktSochungsinh())
                    //{
                    //    MessageBox.Show("Trùng số chứng sinh, vui lòng nhập số khác");
                    //}
                    //else
                    //{
                    TheoDoiThaiSan cs = data.TheoDoiThaiSans.Where(p => p.MaBNhan == Mabn).FirstOrDefault();

                    #region Thêm mới số chứng sinh
                    if (cs == null)
                    {


                        bool ktra = true;
                        while (ktra)
                        {
                            int ot;
                            int socs = Convert.ToInt32(txtSoCSinh.Text);
                            data.SaveChanges();
                            ktra = false;
                            //SoBienLai qsobl = (from spl in data.SoBienLais.Where(p => p.PLoai == 3 && p.Status == 1) select spl).FirstOrDefault();
                            //if (qsobl != null)
                            //{
                            //    if (socs > qsobl.SoHT)
                            //    {
                            //        qsobl.SoHT = Convert.ToInt32(txtSoCSinh.Text);
                            //        data.SaveChanges();
                            //        ktra = false;
                            //    }
                            //    else
                            //    {
                            //        ktra = true;
                            //        txtSoCSinh.Text = (socs + 1).ToString();
                            //    }
                            //}
                            //else
                            //{
                            //    MessageBox.Show("bạn chưa thiết lập quyển số chứng sinh");
                            //    return;
                            //}
                        }

                        TheoDoiThaiSan tdts = new TheoDoiThaiSan();
                        tdts.MaBNhan = Mabn;
                        tdts.SoCon = socon;
                        tdts.TenBo = txtHoTenCha.Text;
                        tdts.ThoiGianSinh = txtSinhLuc.DateTime;
                        tdts.DiaDiemSinh = txtBV.Text;
                        tdts.Ploai = 1;
                        tdts.NguoiGhiPhieu = txtNguoiGhiPhieu.Text;
                        tdts.NguoiDoDe = txtNguoiDoDe.Text;
                        tdts.SinhCon_Duoi32Tuan = chkSinhConDuoi32Tuan.Checked == true ? true : false;
                        tdts.SinhCon_PhauThuat = chkSinhConPhauThuat.Checked == true ? true : false;
                        if (socon > 0)
                        {
                            tdts.GioiTinhCon1 = txtGioiTinh1.SelectedIndex == 0 ? 1 : (txtGioiTinh1.SelectedIndex == 1 ? 0 : 2);
                            tdts.Ten1 = txtTenCon1.Text;
                            if (!string.IsNullOrEmpty(txtCanNang1.Text))
                            {
                                tdts.CanNang1 = Convert.ToInt32(txtCanNang1.Text);
                            }
                        }
                        if (socon > 1)
                        {
                            tdts.GioiTinhCon2 = txtGioiTinh2.SelectedIndex == 0 ? 1 : (txtGioiTinh2.SelectedIndex == 1 ? 0 : 2);
                            tdts.Ten2 = txtTenCon2.Text;
                            if (!string.IsNullOrEmpty(txtCanNang2.Text))
                            {
                                tdts.CanNang2 = Convert.ToInt32(txtCanNang2.Text);
                            }
                        }
                        if (socon > 2)
                        {
                            tdts.GioiTinhCon3 = txtGioiTinh3.SelectedIndex == 0 ? 1 : (txtGioiTinh3.SelectedIndex == 1 ? 0 : 2);
                            tdts.Ten3 = txtTenCon3.Text;
                            if (!string.IsNullOrEmpty(txtCanNang3.Text))
                            {
                                tdts.CanNang3 = Convert.ToInt32(txtCanNang3.Text);
                            }
                        }
                        if (socon > 3)
                        {
                            tdts.GioiTinhCon4 = txtGioiTinh4.SelectedIndex == 0 ? 1 : (txtGioiTinh4.SelectedIndex == 1 ? 0 : 2);
                            tdts.Ten4 = txtTenCon4.Text;
                            if (!string.IsNullOrEmpty(txtCanNang4.Text))
                            {
                                tdts.CanNang4 = Convert.ToInt32(txtCanNang4.Text);
                            }
                        }
                        tdts.GhiChu = txtGhiChu.Text;
                        tdts.LanDe = Convert.ToInt32(txtSoLanSinh.Text);
                        tdts.SoConTT = Convert.ToInt32(txtSoConSong.Text);
                        tdts.NgayCT = dtNgayCapGCS.DateTime;
                        tdts.QuyenSo = txtQuyenSo.Text;
                        tdts.SoChungSinh = txtSoCSinh.Text;

                        data.TheoDoiThaiSans.Add(tdts);
                        data.SaveChanges();
                        MessageBox.Show("Đã tạo Giấy chứng sinh thành công !");

                    }
                    #endregion
                    #region Sửa số chứng sinh
                    else
                    {

                        cs.MaBNhan = Mabn;
                        cs.SoCon = socon;
                        cs.TenBo = txtHoTenCha.Text;
                        cs.ThoiGianSinh = txtSinhLuc.DateTime;
                        cs.DiaDiemSinh = txtBV.Text;
                        cs.Ploai = 1;
                        cs.SinhCon_Duoi32Tuan = chkSinhConDuoi32Tuan.Checked == true ? true : false;
                        cs.SinhCon_PhauThuat = chkSinhConPhauThuat.Checked == true ? true : false;
                        if (socon > 0)
                        {
                            cs.GioiTinhCon1 = txtGioiTinh1.SelectedIndex == 0 ? 1 : (txtGioiTinh1.SelectedIndex == 1 ? 0 : 2);
                            cs.Ten1 = txtTenCon1.Text;
                            if (!string.IsNullOrEmpty(txtCanNang1.Text))
                            {
                                cs.CanNang1 = Convert.ToInt32(txtCanNang1.Text);
                            }
                        }
                        if (socon > 1)
                        {
                            cs.GioiTinhCon2 = txtGioiTinh2.SelectedIndex == 0 ? 1 : (txtGioiTinh2.SelectedIndex == 1 ? 0 : 2);
                            cs.Ten2 = txtTenCon2.Text;
                            if (!string.IsNullOrEmpty(txtCanNang2.Text))
                            {
                                cs.CanNang2 = Convert.ToInt32(txtCanNang2.Text);
                            }
                        }
                        if (socon > 2)
                        {
                            cs.GioiTinhCon3 = txtGioiTinh3.SelectedIndex == 0 ? 1 : (txtGioiTinh3.SelectedIndex == 1 ? 0 : 2);
                            cs.Ten3 = txtTenCon3.Text;
                            if (!string.IsNullOrEmpty(txtCanNang3.Text))
                            {
                                cs.CanNang3 = Convert.ToInt32(txtCanNang3.Text);
                            }
                        }
                        if (socon > 3)
                        {
                            cs.GioiTinhCon4 = txtGioiTinh4.SelectedIndex == 0 ? 1 : (txtGioiTinh4.SelectedIndex == 1 ? 0 : 2);
                            cs.Ten4 = txtTenCon4.Text;
                            if (!string.IsNullOrEmpty(txtCanNang4.Text))
                            {
                                cs.CanNang4 = Convert.ToInt32(txtCanNang4.Text);
                            }
                        }
                        cs.GhiChu = txtGhiChu.Text;
                        cs.NgayCT = dtNgayCapGCS.DateTime;
                        cs.SoConTT = Convert.ToInt32(txtSoConSong.Text);
                        cs.LanDe = Convert.ToInt32(txtSoLanSinh.Text);
                        cs.QuyenSo = txtQuyenSo.Text;
                        cs.SoChungSinh = txtSoCSinh.Text;
                        cs.NguoiDoDe = txtNguoiDoDe.Text;
                        cs.NguoiGhiPhieu = txtNguoiGhiPhieu.Text;
                        data.SaveChanges();
                        MessageBox.Show("Cập nhật giấy chứng sinh thành công !");
                    }
                    #endregion
                    #region sửa ttbx
                    TTboXung ttbx = data.TTboXungs.Where(p => p.MaBNhan == Mabn).FirstOrDefault();
                    if (ttbx == null)
                    {
                        TTboXung moi = new TTboXung();
                        moi.MaBNhan = Mabn;
                        moi.HKTT = txtNoiDKThuongTru.Text;
                        moi.CMT = txtCMT.Text;
                        if (!string.IsNullOrEmpty(txtNgayCap.Text))
                            moi.NgayCapCMT = txtNgayCap.DateTime;
                        moi.NoiCapCMT = txtNoiCap.Text;
                        if (lupDT.EditValue != null)
                        {
                            moi.MaDT = lupDT.EditValue.ToString();
                        }
                        data.TTboXungs.Add(moi);
                        data.SaveChanges();
                    }
                    else
                    {
                        ttbx.HKTT = txtNoiDKThuongTru.Text;
                        ttbx.SoKSinh = txtCMT.Text;
                        if (!string.IsNullOrEmpty(txtNgayCap.Text))
                            ttbx.NgayCapCMT = txtNgayCap.DateTime;
                        ttbx.NoiCapCMT = txtNoiCap.Text;
                        if (lupDT.EditValue != null)
                        {
                            ttbx.MaDT = lupDT.EditValue.ToString();
                        }
                        data.SaveChanges();
                    }
                    frm_Giaychungsinh_Load(sender, e);
                    #endregion
                    //}
                }

            }
            else
            {
                MessageBox.Show("Thông báo!", "Bạn chưa nhập số con!");
                txtSoCon.Focus();
            }
        }

        private void btn_KLuu_Click(object sender, EventArgs e)
        {
            frm_Giaychungsinh_Load(sender, e);
        }

        private static BaoCao.rep_GiayChungSinh in2lien(QLBV_Database.QLBVEntities _data, int mabn)
        {
            BaoCao.rep_GiayChungSinh rep = new BaoCao.rep_GiayChungSinh();
            var bn = (from a in _data.BenhNhans.Where(p => p.MaBNhan == mabn)
                      join b in _data.TTboXungs on a.MaBNhan equals b.MaBNhan
                      join c in _data.DanTocs on b.MaDT equals c.MaDT into kq
                      from k1 in kq.DefaultIfEmpty()
                      select new { a.MaBNhan, a.TenBNhan, a.NamSinh, a.SThe, b.SoKSinh, b.NgayCapCMT, TenDT = k1 != null ? k1.TenDT : "", b.NThan, b.TTGiayCSinh }).ToList();
            string[] arr = { "", "", "", "", "", "", "", "", "", "", "" };

            arr = bn.FirstOrDefault().TTGiayCSinh.Split('|');
            rep.SoCSinh.Value = arr[1];
            rep.Quyen.Value = arr[0];
            rep.HoTenMe.Value = bn.FirstOrDefault().TenBNhan.ToUpper();
            rep.NamSinh.Value = bn.FirstOrDefault().NamSinh;
            rep.NoiDKThuongTru.Value = arr[2];
            rep.BHYT.Value = bn.FirstOrDefault().SThe;
            rep.CMTND.Value = bn.FirstOrDefault().SoKSinh;
            rep.NgayCap.Value = bn.FirstOrDefault().NgayCapCMT != null ? (bn.FirstOrDefault().NgayCapCMT.Value.Day + "/" + bn.FirstOrDefault().NgayCapCMT.Value.Month + "/" + bn.FirstOrDefault().NgayCapCMT.Value.Year) : "";
            rep.NoiCap.Value = arr[3].ToString();
            rep.DanToc.Value = bn.FirstOrDefault().TenDT;
            rep.HoTenCha.Value = arr[4];
            if (arr[5] != "")
                rep.DaSinhLuc.Value = Convert.ToDateTime(arr[5]).Hour + " giờ " + Convert.ToDateTime(arr[5]).Minute + " phút,ngày " + Convert.ToDateTime(arr[5]).Day + " tháng " + Convert.ToDateTime(arr[5]).Month + " năm  " + Convert.ToDateTime(arr[5]).Year;
            else
                rep.DaSinhLuc.Value = "...giờ...phút,ngày...tháng...năm.....";
            rep.SoCon.Value = arr[6];
            string[] ar1 = { "", "", "", "" };
            ar1 = arr[7].Split(';');
            rep.GioiTinhCon.Value = ar1[0] + (ar1[1] != "" ? ", " + ar1[1] : "") + (ar1[2] != "" ? ", " + ar1[2] : "") + (ar1[3] != "" ? ", " + ar1[3] : "");
            string[] ar2 = { "", "", "", "" };
            ar2 = arr[8].Split(';');
            rep.CanNang.Value = "Cân nặng: " + ar2[0] + (ar2[1] != "" ? ", " + ar2[1] : "") + (ar2[2] != "" ? ", " + ar2[2] : "") + (ar2[3] != "" ? ", " + ar2[3] : "");
            string[] ar3 = { "", "", "", "" };
            ar3 = arr[9].Split(';');
            rep.DuDinhDatTenCon.Value = "Dự định đặt tên con: " + ar3[0] + (ar3[1] != "" ? ", " + ar3[1] : "") + (ar3[2] != "" ? ", " + ar3[2] : "") + (ar3[3] != "" ? ", " + ar3[3] : "");
            rep.GhiChu.Value = arr[10];
            rep.Tai.Value = DungChung.Bien.TenCQ;
            rep.NgayThangCapGiay.Value = DungChung.Bien.DiaDanh + " ,ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            rep.CreateDocument();
            return rep;
        }

        private void txtSoCon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtSoCon.SelectedIndex == 0)
            {
                txtGioiTinh1.Visible = true;
                txtGioiTinh2.Visible = false;
                txtGioiTinh3.Visible = false;
                txtGioiTinh4.Visible = false;

                txtCanNang1.Visible = true;
                txtCanNang2.Visible = false;
                txtCanNang3.Visible = false;
                txtCanNang4.Visible = false;

                txtTenCon1.Visible = true;
                txtTenCon2.Visible = false;
                txtTenCon3.Visible = false;
                txtTenCon4.Visible = false;
            }
            if (txtSoCon.SelectedIndex == 1)
            {
                txtGioiTinh1.Visible = true;
                txtGioiTinh2.Visible = true;
                txtGioiTinh3.Visible = false;
                txtGioiTinh4.Visible = false;

                txtCanNang1.Visible = true;
                txtCanNang2.Visible = true;
                txtCanNang3.Visible = false;
                txtCanNang4.Visible = false;

                txtTenCon1.Visible = true;
                txtTenCon2.Visible = true;
                txtTenCon3.Visible = false;
                txtTenCon4.Visible = false;
            }
            if (txtSoCon.SelectedIndex == 2)
            {
                txtGioiTinh1.Visible = true;
                txtGioiTinh2.Visible = true;
                txtGioiTinh3.Visible = true;
                txtGioiTinh4.Visible = false;

                txtCanNang1.Visible = true;
                txtCanNang2.Visible = true;
                txtCanNang3.Visible = true;
                txtCanNang4.Visible = false;

                txtTenCon1.Visible = true;
                txtTenCon2.Visible = true;
                txtTenCon3.Visible = true;
                txtTenCon4.Visible = false;
            }
            if (txtSoCon.SelectedIndex == 3)
            {
                txtGioiTinh1.Visible = true;
                txtGioiTinh2.Visible = true;
                txtGioiTinh3.Visible = true;
                txtGioiTinh4.Visible = true;

                txtCanNang1.Visible = true;
                txtCanNang2.Visible = true;
                txtCanNang3.Visible = true;
                txtCanNang4.Visible = true;

                txtTenCon1.Visible = true;
                txtTenCon2.Visible = true;
                txtTenCon3.Visible = true;
                txtTenCon4.Visible = true;
            }
        }

        private void btnPost_GCS_Click(object sender, EventArgs e)
        {
            int _mbn = Mabn;
            string username = DungChung.Bien.xmlFilePath_LIS[10];
            string pass = DungChung.Bien.xmlFilePath_LIS[11];
            GuiGiayChungSinhModel GiayChungSinh = _GiayChungSinh_Provider.GetGCS(_mbn, DungChung.Bien.MaBV);
            var bn = data.BenhNhans.FirstOrDefault(p => p.MaBNhan == _mbn);
            var token = Task.Run(async () => await DataCommunication.DataCommunication.GetToken(username, pass)).Result;
            #region check thông tin
            List<string> strErros = new List<string>();

            if (string.IsNullOrEmpty(GiayChungSinh.MA_THE_NND))
            {
                string erro = "Số thẻ";
                strErros.Add(erro);
            }
            if (string.IsNullOrEmpty(GiayChungSinh.NGAYSINH_NND))
            {
                string erro = "Ngày sinh";
                strErros.Add(erro);
            }
            if (string.IsNullOrEmpty(GiayChungSinh.MA_DANTOC_NND))
            {
                string erro = "Dân tộc";
                strErros.Add(erro);
            }
            if (string.IsNullOrEmpty(GiayChungSinh.NGAYCAP_CCCD_NND))
            {
                string erro = "Ngày cấp CCCD";
                strErros.Add(erro);
            }
            if (string.IsNullOrEmpty(GiayChungSinh.NOICAP_CCCD_NND))
            {
                string erro = "Nơi cấp CCCD";
                strErros.Add(erro);
            }
            if (string.IsNullOrEmpty(GiayChungSinh.NOI_CU_TRU_NND))
            {
                string erro = "Địa chỉ";
                strErros.Add(erro);
            }
            if (string.IsNullOrEmpty(GiayChungSinh.MATINH_CU_TRU.Trim()))
            {
                string erro = "Tỉnh cư trú";
                strErros.Add(erro);
            }
            if (string.IsNullOrEmpty(GiayChungSinh.MAHUYEN_CU_TRU.Trim()))
            {
                string erro = "Huyện cư trú";
                strErros.Add(erro);
            }
            if (string.IsNullOrEmpty(GiayChungSinh.MAXA_CU_TRU.Trim()))
            {
                string erro = "Xã cư trú";
                strErros.Add(erro);
            }
            if (string.IsNullOrEmpty(GiayChungSinh.TEN_CON))
            {
                string erro = "Tên con";
                strErros.Add(erro);
            }
            if (GiayChungSinh.GIOI_TINH_CON == null)
            {
                string erro = "Giới tính con";
                strErros.Add(erro);
            }
            if (string.IsNullOrEmpty(GiayChungSinh.SO_CON))
            {
                string erro = "Số con";
                strErros.Add(erro);
            }
            if (GiayChungSinh.LAN_SINH == null)
            {
                string erro = "Lần sinh";
                strErros.Add(erro);
            }
            if (GiayChungSinh.SO_CON_SONG == null)
            {
                string erro = "Số con sống";
                strErros.Add(erro);
            }
            if (string.IsNullOrEmpty(GiayChungSinh.NOI_SINH_CON))
            {
                string erro = "Nơi sinh";
                strErros.Add(erro);
            }
            if (string.IsNullOrEmpty(GiayChungSinh.TINH_TRANG_CON))
            {
                string erro = "Tình trạng con";
                strErros.Add(erro);
            }
            if (string.IsNullOrEmpty(GiayChungSinh.NGUOI_DO_DE))
            {
                string erro = "Người đỡ đẻ";
                strErros.Add(erro);
            }
            if (string.IsNullOrEmpty(GiayChungSinh.NGUOI_GHI_PHIEU))
            {
                string erro = "Người ghi phiếu";
                strErros.Add(erro);
            }
            if (GiayChungSinh.MA_TTDV == null)
            {
                string erro = "Mã BHXH Thủ Trưởng cơ sở KBCB";
                strErros.Add(erro);
            }
            if (string.IsNullOrEmpty(GiayChungSinh.THU_TRUONG_DVI))
            {
                string erro = "Thủ trưởng đơn vị";
                strErros.Add(erro);
            }
            if (strErros.Count > 0)
            {
                MessageBox.Show(string.Format("Thiếu một số thông tin {0}. Xin vui lòng kiểm tra lại!", string.Join(", ", strErros)));
                return;
            }
            #endregion
            var dataCommunicationXml = AppConfig.MyMapper.Map<GuiGiayChungSinhXmlModel>(GiayChungSinh);

            var hsgcs = new HSDLGCSModel()
            {
                GIAYCHUNGSINH = dataCommunicationXml
            };

            var xml = XMLHelper.SerializeObject(hsgcs);
            CreatePath.Path(AppDomain.CurrentDomain.BaseDirectory + "Xmls");
            var xmlName = bn.MaBNhan + "_" + Helpers.RemoveDiacritics(bn.TenBNhan) + "_" + dataCommunicationXml.Id;
            string filePath = AppDomain.CurrentDomain.BaseDirectory + @"Xmls\" + $"{xmlName}.xml";

            // Xuất file xml
            File.WriteAllText(filePath, xml);
            var hospitalCodes = new List<string>()
            {
                "30004"
            };
            var isSignature = true;

            if (hospitalCodes.Any(a => a.Equals(DungChung.Bien.MaBV)))
                isSignature = Helpers.SignXmlFile(filePath);
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Chưa nhập tên đăng nhập hoặc mật khẩu");
                return;
            }
            //gửi dữ liệu
            if (isSignature)
            {
                var xmlToBytes = File.ReadAllBytes(filePath);
                var xmlToBase64String = Convert.ToBase64String(xmlToBytes);

                var data = new GCS()
                {
                    maCskcb = DungChung.Bien.MaBV,
                    token = token.apiKey.access_token,
                    id_token = token.apiKey.id_token,
                    username = username,
                    password = Security.EncryptMd5(pass),
                    loaiHs = "61",
                    fileBase64Str = xmlToBase64String,
                };

                Task.Run(async () => await DataCommunication.DataCommunication.GiayChungSinh(data, token.apiKey.access_token, Mabn));
            }

        }
    }
}