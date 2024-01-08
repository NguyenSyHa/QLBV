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
    public partial class frm_BCThuNguonDV_XHH : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCThuNguonDV_XHH()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<KPhong> _lKPhong = new List<KPhong>();
        private void frm_BCThuNguonDV_VHH_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = DateTime.Now.AddMonths(-1);
            lupngayden.DateTime = DateTime.Now;
            var kpp = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang).OrderBy(p => p.TenKP).ToList();
            foreach(var item in kpp)
            {
                _lKPhong.Insert(0, new KPhong { MaKP = item.MaKP, TenKP = item.TenKP });
                
            }
            _lKPhong.Insert(0, new KPhong { MaKP = 1000, TenKP = "Ngoại trú" });
            _lKPhong.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả", PLoai = "Tất cả" });

            cklKhoaPhong.DataSource = _lKPhong;
            cklKhoaPhong.CheckAll();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (trongbh.Checked == false && ngoaibh.Checked == false)
            {
                MessageBox.Show("Bạn chưa chọn Trong/Ngoài BH!");
                trongbh.Focus();
            }
            else
            {
                DateTime ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                DateTime ngayden = DungChung.Ham.NgayDen(lupngayden.DateTime);
                List<int> _lkp = new List<int>();
                for (int i = 0; i < cklKhoaPhong.ItemCount; i++)
                {
                    if (cklKhoaPhong.GetItemChecked(i))
                        _lkp.Add(Convert.ToInt32(cklKhoaPhong.GetItemValue(i)));
                }
                int tnbh = 2;
                if (trongbh.Checked == true)
                {
                    tnbh = 1;
                }
                if (ngoaibh.Checked == true)
                    tnbh = 0;
                if (trongbh.Checked == true && ngoaibh.Checked == true)
                    tnbh = 2;
                var _lkpnew = (from kp in _lKPhong
                               join k in _lkp on kp.MaKP equals k
                               select new { kp.MaKP, kp.TenKP, kp.PLoai }).Distinct().ToList();
                var dv = (from a in data.DichVus
                          join b in data.TieuNhomDVs.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong) on a.IdTieuNhom equals b.IdTieuNhom
                          select new { a.MaDV, a.TenDV, b.TenRG }).ToList();

                var _lvp = (from vp in data.VienPhis.Where(p => p.NgayDuyet >= ngaytu && p.NgayDuyet <= ngayden)
                            join vpct in data.VienPhicts.Where(P => P.ThanhToan == 0) on vp.idVPhi equals vpct.idVPhi
                            join kp in data.KPhongs on vpct.MaKP equals kp.MaKP
                            join bn in data.BenhNhans.Where(p => p.DTuong == "BHYT") on vp.MaBNhan equals bn.MaBNhan
                            join tu in data.TamUngs.Where(p => p.PhanLoai == 1) on vp.MaBNhan equals tu.MaBNhan
                            select new
                            {
                                vpct.MaDV,
                                MaKP = (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham || kp.PLoai == null) ? 1000 : kp.MaKP,
                                TenKP = (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham || kp.PLoai == null) ? "Ngoại trú" : kp.TenKP,
                                vpct.TienBH,
                                vpct.idVPhict,
                                vpct.ThanhTien,
                                vp.MaBNhan,
                                vpct.TrongBH,
                                vpct.TienBN
                            }).ToList();
                var ds = (from c in dv
                          join b in _lvp.Where(p => tnbh == 2 || p.TrongBH == tnbh) on c.MaDV equals b.MaDV
                          group new { c, b } by new { b.MaKP, b.TenKP } into kq
                          select new
                          {
                              kq.Key.MaKP,
                              kq.Key.TenKP,
                              X_QuangKTS = kq.Where(p => p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Sum(p => p.b.TienBN),
                              X_QuangCiti = kq.Where(p => p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Sum(p => p.b.TienBN),
                              DoLoangXuong = kq.Where(p => p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Sum(p => p.b.TienBN),
                              Tong = kq.Where(p => p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Sum(p => p.b.TienBN) + kq.Where(p => p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Sum(p => p.b.TienBN) + kq.Where(p => p.c.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Sum(p => p.b.TienBN)
                          }).ToList();

                frmIn frm = new frmIn();
                BaoCao.rep_BCThuNguonDV_XHH rep = new BaoCao.rep_BCThuNguonDV_XHH();
                double tongtien = ds.Count > 0 ? ds.Sum(p => p.Tong) : 0;
                rep.TienThanhChu.Value = "Bằng chữ: " + DungChung.Ham.DocTienBangChu(tongtien, " đồng./.");
                rep.ngaythang.Value = "Từ ngày: " + ngaytu.ToShortDateString() + " đến ngày: " + ngayden.ToShortDateString();
                rep.ngaythangin.Value = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                rep.DataSource = ds;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }
        private void cklKhoaPhong_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                if (cklKhoaPhong.GetItemChecked(0) == true)
                    cklKhoaPhong.CheckAll();
                else
                    cklKhoaPhong.UnCheckAll();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void trongbh_CheckedChanged(object sender, EventArgs e)
        {
            //if (trongbh.Checked == false && ngoaibh.Checked == false)
            //{
            //    MessageBox.Show("Bạn chưa chọn Trong/Ngoài BH!");
            //    trongbh.Focus();
            //}
        }

        private void ngoaibh_CheckedChanged(object sender, EventArgs e)
        {
            //if (trongbh.Checked == false && ngoaibh.Checked == false)
            //{
            //    MessageBox.Show("Bạn chưa chọn Trong/Ngoài BH!");
            //    trongbh.Focus();
            //}
        }
    }
}