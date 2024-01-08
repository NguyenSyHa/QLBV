using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormThamSo
{
    public partial class frm_BCKhamBenh_TamDuong : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCKhamBenh_TamDuong()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void frm_BCKhamBenh_TamDuong_Load(object sender, EventArgs e)
        {
            lup_NgayTu.DateTime = DateTime.Now.AddMonths(-1);
            lup_NgayDen.DateTime = DateTime.Now;
            var khoa = _data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham || p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang).ToList();
            KPhong moi = new KPhong();
            moi.MaKP = 0;
            moi.TenKP = "Tất cả";
            khoa.Add(moi);
            lup_Khoa.Properties.DataSource = khoa.OrderBy(p => p.MaKP).ToList();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DateTime ngaytu = DungChung.Ham.NgayTu(lup_NgayTu.DateTime);
            DateTime ngayden = DungChung.Ham.NgayDen(lup_NgayDen.DateTime);
            if (lup_Khoa.Text != "")
            {
                int makp = Convert.ToInt32(lup_Khoa.EditValue.ToString());
                var bnkb = (from a in _data.BNKBs.Where(p => p.NgayKham >= ngaytu && p.NgayKham <= ngayden).Where(p => p.MaKP == makp || makp ==0)
                            join b in _data.BenhNhans on a.MaBNhan equals b.MaBNhan
                            select new { 
                                b.MaBNhan, b.DTNT, b.NoiTru, b.MaDTuong, b.GTinh, b.Tuoi, b.DTuong,
                            }).ToList();
                var cdha = (from a in _data.CLS.Where(p => p.MaKP == makp || makp == 0)
                            join b in _data.ChiDinhs.Where(p => p.NgayTH >= ngaytu && p.NgayTH <= ngayden) on a.IdCLS equals b.IdCLS
                            join c in _data.DichVus on b.MaDV equals c.MaDV
                            join d in _data.TieuNhomDVs on c.IdTieuNhom equals d.IdTieuNhom
                            join h in _data.BenhNhans on a.MaBNhan equals h.MaBNhan
                            group new {a,b,c,d} by new {a.MaBNhan, d.IdTieuNhom, d.TenRG, h.GTinh, h.Tuoi} into kq
                            select new {
                                kq.Key.MaBNhan, 
                                kq.Key.IdTieuNhom,
                                kq.Key.TenRG,
                                kq.Key.GTinh,
                                kq.Key.Tuoi
                            }).ToList();
                
                BaoCao.rep_BCKhamBenh_TamDuong rep = new BaoCao.rep_BCKhamBenh_TamDuong();
                rep.th1.Text = Convert.ToString(bnkb.Count());
                rep.nu1.Text = Convert.ToString(bnkb.Where(p => p.GTinh == 0).Count());
                rep.TE1.Text = Convert.ToString(bnkb.Where(p => p.Tuoi < 15).Count());

                rep.th2.Text = Convert.ToString(bnkb.Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count());
                rep.nu2.Text = Convert.ToString(bnkb.Where(p => p.NoiTru == 1).Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Distinct().Count());
                rep.TE2.Text = Convert.ToString(bnkb.Where(p => p.NoiTru == 1).Where(p => p.Tuoi < 15).Select(p => p.MaBNhan).Distinct().Count());

                rep.th3.Text = Convert.ToString(bnkb.Where(p => p.NoiTru == 0).Select(p => p.MaBNhan).Distinct().Count());
                rep.nu3.Text = Convert.ToString(bnkb.Where(p => p.NoiTru == 0).Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Distinct().Count());
                rep.TE3.Text = Convert.ToString(bnkb.Where(p => p.NoiTru == 0).Where(p => p.Tuoi < 15).Select(p => p.MaBNhan).Distinct().Count());

                rep.th4.Text = Convert.ToString(bnkb.Where(p => p.DTuong == "BHYT").Where(p =>  p.MaDTuong.ToLower().Contains("hn") || p.MaDTuong.ToLower().Contains("dt")).Count());
                rep.nu4.Text = Convert.ToString(bnkb.Where(p => p.DTuong == "BHYT").Where(p =>  p.MaDTuong.ToLower().Contains("hn") || p.MaDTuong.ToLower().Contains("dt")).Where(p => p.GTinh == 0).Count());
                rep.TE4.Text = Convert.ToString(bnkb.Where(p => p.DTuong == "BHYT").Where(p =>  p.MaDTuong.ToLower().Contains("hn") || p.MaDTuong.ToLower().Contains("dt")).Where(p => p.Tuoi < 15).Count());

                var test1 = bnkb.Where(p => p.DTuong == "BHYT").Where(p => p.MaDTuong.ToLower().Contains("hn") || p.MaDTuong.ToLower().Contains("dt")).ToList();
                rep.th5.Text = Convert.ToString(bnkb.Where(p => p.DTuong == "BHYT").Where(p => p.MaDTuong.ToLower().Contains("hn") || p.MaDTuong.ToLower().Contains("dt")).Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count());
                rep.nu5.Text = Convert.ToString(bnkb.Where(p => p.DTuong == "BHYT").Where(p => p.MaDTuong.ToLower().Contains("hn") || p.MaDTuong.ToLower().Contains("dt")).Where(p => p.NoiTru == 1).Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Distinct().Count());
                rep.TE5.Text = Convert.ToString(bnkb.Where(p => p.DTuong == "BHYT").Where(p => p.MaDTuong.ToLower().Contains("hn") || p.MaDTuong.ToLower().Contains("dt")).Where(p => p.NoiTru == 1).Where(p => p.Tuoi < 15).Select(p => p.MaBNhan).Distinct().Count());

                rep.th6.Text = Convert.ToString(bnkb.Where(p => p.DTuong == "BHYT").Where(p => p.MaDTuong.ToLower().Contains("hn") || p.MaDTuong.ToLower().Contains("dt")).Where(p => p.NoiTru == 0).Select(p => p.MaBNhan).Distinct().Count());
                rep.nu6.Text = Convert.ToString(bnkb.Where(p => p.DTuong == "BHYT").Where(p => p.MaDTuong.ToLower().Contains("hn") || p.MaDTuong.ToLower().Contains("dt")).Where(p => p.NoiTru == 0).Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Distinct().Count());
                rep.TE6.Text = Convert.ToString(bnkb.Where(p => p.DTuong == "BHYT").Where(p => p.MaDTuong.ToLower().Contains("hn") || p.MaDTuong.ToLower().Contains("dt")).Where(p => p.NoiTru == 0).Where(p => p.Tuoi < 15).Select(p => p.MaBNhan).Distinct().Count());

                rep.th7.Text = Convert.ToString(bnkb.Where(p => p.Tuoi < 6).Count());
                rep.nu7.Text = Convert.ToString(bnkb.Where(p => p.Tuoi < 6).Where(p => p.GTinh == 0).Count());
                rep.TE7.Text = Convert.ToString(bnkb.Where(p => p.Tuoi < 6).Where(p => p.Tuoi < 15).Count());

                rep.th8.Text = Convert.ToString(bnkb.Where(p => p.Tuoi < 6).Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count());
                rep.nu8.Text = Convert.ToString(bnkb.Where(p => p.Tuoi < 6).Where(p => p.NoiTru == 1).Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Distinct().Count());
                rep.TE8.Text = Convert.ToString(bnkb.Where(p => p.Tuoi < 6).Where(p => p.NoiTru == 1).Where(p => p.Tuoi < 15).Select(p => p.MaBNhan).Distinct().Count());

                rep.th9.Text = Convert.ToString(bnkb.Where(p => p.Tuoi < 6).Where(p => p.NoiTru == 0).Select(p => p.MaBNhan).Distinct().Count());
                rep.nu9.Text = Convert.ToString(bnkb.Where(p => p.Tuoi < 6).Where(p => p.NoiTru == 0).Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Distinct().Count());
                rep.TE9.Text = Convert.ToString(bnkb.Where(p => p.Tuoi < 6).Where(p => p.NoiTru == 0).Where(p => p.Tuoi < 15).Select(p => p.MaBNhan).Distinct().Count());

                rep.th10.Text = Convert.ToString(bnkb.Where(p => p.DTuong == "BHYT").Where(p =>  !p.MaDTuong.ToLower().Contains("hn") && !p.MaDTuong.ToLower().Contains("dt")).Count());
                rep.nu10.Text = Convert.ToString(bnkb.Where(p => p.DTuong == "BHYT").Where(p =>  !p.MaDTuong.ToLower().Contains("hn") && !p.MaDTuong.ToLower().Contains("dt")).Where(p => p.GTinh == 0).Count());
                rep.TE10.Text = Convert.ToString(bnkb.Where(p => p.DTuong == "BHYT").Where(p =>  !p.MaDTuong.ToLower().Contains("hn") && !p.MaDTuong.ToLower().Contains("dt")).Where(p => p.Tuoi < 15).Count());

                rep.th11.Text = Convert.ToString(bnkb.Where(p => p.DTuong == "BHYT").Where(p => !p.MaDTuong.ToLower().Contains("hn") && !p.MaDTuong.ToLower().Contains("dt")).Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count());
                rep.nu11.Text = Convert.ToString(bnkb.Where(p => p.DTuong == "BHYT").Where(p => !p.MaDTuong.ToLower().Contains("hn") && !p.MaDTuong.ToLower().Contains("dt")).Where(p => p.NoiTru == 1).Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Distinct().Count());
                rep.TE11.Text = Convert.ToString(bnkb.Where(p => p.DTuong == "BHYT").Where(p => !p.MaDTuong.ToLower().Contains("hn") && !p.MaDTuong.ToLower().Contains("dt")).Where(p => p.NoiTru == 1).Where(p => p.Tuoi < 15).Select(p => p.MaBNhan).Distinct().Count());

                rep.th12.Text = Convert.ToString(bnkb.Where(p => p.DTuong == "BHYT").Where(p => !p.MaDTuong.ToLower().Contains("hn") && !p.MaDTuong.ToLower().Contains("dt")).Where(p => p.NoiTru == 0).Select(p => p.MaBNhan).Distinct().Count());
                rep.nu12.Text = Convert.ToString(bnkb.Where(p => p.DTuong == "BHYT").Where(p => !p.MaDTuong.ToLower().Contains("hn") && !p.MaDTuong.ToLower().Contains("dt")).Where(p => p.NoiTru == 0).Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Distinct().Count());
                rep.TE12.Text = Convert.ToString(bnkb.Where(p => p.DTuong == "BHYT").Where(p => !p.MaDTuong.ToLower().Contains("hn") && !p.MaDTuong.ToLower().Contains("dt")).Where(p => p.NoiTru == 0).Where(p => p.Tuoi < 15).Select(p => p.MaBNhan).Distinct().Count());

                rep.th13.Text = Convert.ToString(cdha.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS).Count());
                rep.nu13.Text = Convert.ToString(cdha.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS).Where(p => p.GTinh == 0).Count());
                rep.TE13.Text = Convert.ToString(cdha.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS).Where(p => p.Tuoi < 15).Count());

                rep.th14.Text = Convert.ToString(cdha.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Count());
                rep.nu14.Text = Convert.ToString(cdha.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Where(p => p.GTinh == 0).Count());
                rep.TE14.Text = Convert.ToString(cdha.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Where(p => p.Tuoi < 15).Count());

                rep.th15.Text = Convert.ToString(cdha.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi).Count());
                rep.nu15.Text = Convert.ToString(cdha.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi).Where(p => p.GTinh == 0).Count());
                rep.TE15.Text = Convert.ToString(cdha.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi).Where(p => p.Tuoi < 15).Count());

                rep.th16.Text = Convert.ToString(cdha.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim ).Count());
                rep.nu16.Text = Convert.ToString(cdha.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim ).Where(p => p.GTinh == 0).Count());
                rep.TE16.Text = Convert.ToString(cdha.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim ).Where(p => p.Tuoi < 15).Count());

                rep.th17.Text = Convert.ToString(bnkb.Where(p => p.Tuoi >= 60).Count());
                rep.nu17.Text = Convert.ToString(bnkb.Where(p => p.Tuoi >= 60).Where(p => p.GTinh == 0).Count());
                rep.TE17.Text = "";

                var chuyenvien = (from a in _data.RaViens.Where(p => p.NgayRa >= ngaytu && p.NgayRa <= ngayden && (p.MaKP == makp || makp == 0) && p.Status == 1)
                                  join b in _data.BenhNhans on a.MaBNhan equals b.MaBNhan 
                                  select new { a.MaBNhan, b.Tuoi, b.GTinh }).ToList();
                rep.th18.Text = Convert.ToString(chuyenvien.Count());
                rep.nu18.Text = Convert.ToString(chuyenvien.Where(p => p.GTinh == 0).Count());
                rep.TE18.Text = Convert.ToString(chuyenvien.Where(p => p.Tuoi < 15).Count());

                rep.th19.Text = Convert.ToString(bnkb.Where(p => p.DTuong == "KSK").Select(p => p.MaBNhan).Distinct().Count());
                rep.nu19.Text = Convert.ToString(bnkb.Where(p => p.DTuong == "KSK").Where(p => p.GTinh == 0).Select(p => p.MaBNhan).Distinct().Count());
                rep.TE19.Text = Convert.ToString(bnkb.Where(p => p.DTuong == "KSK").Where(p => p.Tuoi < 15).Select(p => p.MaBNhan).Distinct().Count());

                rep.th20.Text = "";
                rep.nu20.Text = "";
                rep.TE20.Text = "";

                rep.TieuDe.Value = "BÁO CÁO KHÁM BỆNH THÁNG " + ngaytu.AddMonths(1).Month + "/" + ngaytu.AddMonths(1).Year;
                rep.NgaythangTD.Value = "(Từ " + ngaytu.Day + "/" + ngaytu.Month + "/" + ngaytu.Year + " đến " + ngayden.Day + "/" + ngayden.Month + "/" + ngayden.Year + ")";
                rep.NgayThangBC.Value = DungChung.Bien.DiaDanh + ", ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                rep.Khoa.Value = lup_Khoa.Text.ToUpper();
                rep.CreateDocument();
                frmIn frm = new frmIn();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn khoa phòng!", "Thông báo!");
                lup_Khoa.Focus();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}