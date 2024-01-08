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
    public partial class Frm_KcBenhTatTE_TK05 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_KcBenhTatTE_TK05()
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
        private void Frm_KcBenhTatTE_TK05_Load(object sender, EventArgs e)
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

                BaoCao.Rep_KcBenhTatTE_TK05 rep = new BaoCao.Rep_KcBenhTatTE_TK05();
                rep.TuNgayDenNgay.Value = "Từ ngày " + lupNgaytu.Text + " Đến ngày " + lupNgayden.Text;
                rep.TuNgay.Value = lupNgaytu.Text;
                rep.DenNgay.Value = lupNgayden.Text;
                var qbt = (from bn in dataContext.BenhNhans.Where(p => p.Tuoi < 15)
                           join bnkb in dataContext.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                           join icd in dataContext.ICD10 on bnkb.MaICD equals icd.MaICD
                           join kp in dataContext.KPhongs on bnkb.MaKP equals kp.MaKP
                           where (bn.NNhap >= tungay && bn.NNhap <= denngay)
                           group new { kp, bn, bnkb } by new { kp.TenKP, icd.TenICD, icd.MaICD } into kq
                           select new
                           {

                               KhoaPhong = kq.Key.TenKP,
                               TenICD = kq.Key.TenICD,
                               MaICD = kq.Key.MaICD,
                               //NoiTru6 = kq.Where(p => p.bn.Tuoi <=6).Where(p=>p.bn.NoiTru==1).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               //NgoaiTru6 = kq.Where(p => p.bn.Tuoi <= 6).Where(p => p.bn.NoiTru == 0).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               //NoiTru15 = kq.Where(p => p.bn.Tuoi >6).Where(p => p.bn.Tuoi < 15).Where(p => p.bn.NoiTru == 1).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               //NgoaiTru15 = kq.Where(p => p.bn.Tuoi > 6).Where(p => p.bn.Tuoi < 15).Where(p => p.bn.NoiTru == 0).Select(p => p.bn.MaBNhan).Distinct().Count(),
                               //TongSo = kq.Select(p => p.bn.MaBNhan).Distinct().Count(),

                           }).ToList();
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