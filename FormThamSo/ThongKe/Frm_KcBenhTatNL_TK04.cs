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
    public partial class Frm_KcBenhTatNL_TK04 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_KcBenhTatNL_TK04()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private bool kt()
        {
            if (lupNgaytu.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupNgaytu.Focus();
                return false;
            }
            if (lupNgayden.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                lupNgayden.Focus();
                return false;
            }

            else return true;
        }
        private void Frm_KcBenhTatNL_TK04_Load(object sender, EventArgs e)
        {
            lupNgaytu.Focus();
            lupNgaytu.DateTime = System.DateTime.Now;
            lupNgayden.DateTime = System.DateTime.Now;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            frmIn frm = new frmIn();

            if (kt())
            {
                DateTime tungay = System.DateTime.Now.Date;
                DateTime denngay = System.DateTime.Now.Date;
                tungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                denngay = DungChung.Ham.NgayDen(lupNgayden.DateTime);

                BaoCao.Rep_KcBenhTatNL_TK04 rep = new BaoCao.Rep_KcBenhTatNL_TK04();
                rep.TuNgayDenNgay.Value = "Từ ngày " + lupNgaytu.Text + " Đến ngày " + lupNgayden.Text;
                rep.TuNgay.Value = lupNgaytu.Text;
                rep.DenNgay.Value = lupNgayden.Text;
                var qbt2 = (from bn in dataContext.BenhNhans.Where(p => p.Tuoi >= 16)
                           join bnkb in dataContext.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                           join icd in dataContext.ICD10 on bnkb.MaICD equals icd.MaICD
                           join kp in dataContext.KPhongs on bnkb.MaKP equals kp.MaKP
                           where (bn.NNhap >= tungay && bn.NNhap <= denngay)
                           select new
                           {
                               kp.TenKP,
                               icd.TenICD,
                               icd.MaICD,
                               bn.DTuong,
                               bn.MaBNhan,
                               kp
                           }).ToList();
                var qbt = (from bn in qbt2
                           group new { bn } by new { bn.TenKP, bn.TenICD, bn.MaICD } into kq
                            select new
                            {

                               // Mabn = kq.Key.MaBNhan,
                               // Max = kq.Max(p => p.bnkb.IDKB),
                                KhoaPhong=kq.Key.TenKP,
                                TenICD=kq.Key.TenICD,
                                MaICD=kq.Key.MaICD,
                                CanBo = kq.Where(p => p.bn.DTuong == "BHYT").Select(p => p.bn.MaBNhan).Distinct().Count(),
                                NhanDan = kq.Where(p => p.bn.DTuong == "Dịch vụ").Select(p => p.bn.MaBNhan).Distinct().Count(),
                                TongSo = kq.Select(p => p.bn.MaBNhan).Distinct().Count(),
                            }).OrderBy(p=>p.TenICD).ToList();
                if (qbt.Count() > 0)
                {
                    rep.DataSource = qbt.ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else MessageBox.Show("Không có dữ liệu");
            }
           
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}