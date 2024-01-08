using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormThamSo
{
    public partial class Frm_TkSoLieuKCB_YSon02 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_TkSoLieuKCB_YSon02()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private bool KTtaoBc()
        {
            if (dateTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                dateTuNgay.Focus();
                return false;
            }
            if (dateDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                dateDenNgay.Focus();
                return false;
            }

            else return true;
        }
        private void Frm_TkSoLieuKCB_YSon02_Load(object sender, EventArgs e)
        {
            dateTuNgay.DateTime = System.DateTime.Now;
            dateDenNgay.DateTime = System.DateTime.Now;
            dateTuNgay.Focus();
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            if (KTtaoBc())
            {
                tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
                denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;
                int phuongan = -2;
                if (radioGroup1.SelectedIndex == 0)//không tính BN chuyển PK
                {
                    phuongan = 3;
                }
                if (radioGroup1.SelectedIndex == 1)//lấy tất cả BN khám
                {
                    phuongan = -2;
                }
                frmIn frm = new frmIn();
                BaoCao.Rep_TkSoLieuKCB_YSon02 rep = new BaoCao.Rep_TkSoLieuKCB_YSon02();
                rep.TuNgay.Value = dateTuNgay.Text;
                rep.DenNgay.Value = dateDenNgay.Text;
                rep.TuNgayDenNgay.Value = "Từ ngày " + dateTuNgay.Text + " Đến ngày " + dateDenNgay.Text;
                List<ChuyenKhoa> _lckhoa = data.ChuyenKhoas.ToList();
                var qck2 = (from bn in data.BenhNhans.Where(p => p.NoiTru == 0)
                            join bnkb in data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay).Where(p => phuongan == -2 || p.PhuongAn != phuongan) on bn.MaBNhan equals bnkb.MaBNhan
                            join kp in data.KPhongs.Where(p => p.PLoai == "Phòng khám") on bnkb.MaKP equals kp.MaKP
                            group new { bn, bnkb, kp } by new { bnkb.PhuongAn, bn.Tuoi, bnkb.IDKB, bn.DTuong, bn.SThe, bn.MaBNhan, bnkb.MaCK, bn.MaDTuong } into kq
                            select new { kq.Key.PhuongAn, kq.Key.Tuoi, kq.Key.IDKB, kq.Key.DTuong, kq.Key.SThe, kq.Key.MaBNhan, kq.Key.MaCK, kq.Key.MaDTuong }).ToList();
                var qck = (from bn in qck2
                           join ck in _lckhoa on bn.MaCK equals ck.MaCK
                           group new { bn, ck } by new { ck.MaCK, ck.TenCK } into kq
                           select new
                           {
                               IDKB = kq.Max(p => p.bn.IDKB),
                               ChuyenKhoa = kq.Key.TenCK,
                               HN = kq.Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.MaDTuong.Contains("HN")).Select(p => p.bn.MaBNhan).Count(),//
                               Khac = kq.Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.MaDTuong != "HN").Where(p => p.bn.Tuoi > 6).Select(p => p.bn.MaBNhan).Count(),
                               CK6 = kq.Where(p => p.bn.DTuong == "BHYT").Where(p => p.bn.Tuoi <= 6).Select(p => p.bn.MaBNhan).Count(),
                               CKND = kq.Where(p => p.bn.DTuong == "Dịch vụ").Select(p => p.bn.MaBNhan).Count(),
                               CT = kq.Where(p => p.bn.Tuoi > 6).Where(p => p.bn.PhuongAn == 2).Select(p => p.bn.MaBNhan).Count(),
                               CT6 = kq.Where(p => p.bn.Tuoi <= 6).Where(p => p.bn.PhuongAn == 2).Select(p => p.bn.MaBNhan).Count(),
                               TS = kq.Select(p => p.bn.MaBNhan).Count()
                           }).ToList();

                if (qck.Count > 0)
                {
                    rep.DataSource = qck;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}