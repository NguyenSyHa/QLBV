using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;

namespace QLBV.FormThamSo
{
    public partial class frm_BCSL_BenhNhantheo_BS : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCSL_BenhNhantheo_BS()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DateTime tungay = DungChung.Ham.NgayTu(dtpTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dtpDenNgay.DateTime);

            if (tungay > denngay)
            {
                XtraMessageBox.Show("Xin chọn lại thời gian. \n lưu ý: (Từ ngày) không thể lớn hơn (đến ngày)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpTuNgay.Focus();
                return;
            }
            else
            {
                var HienThiDanhSach = (from hienthi in data.CanBoes
                                       join bnkb in data.BNKBs on hienthi.MaCB equals bnkb.MaCB
                                       where (bnkb.NgayKham >= tungay && bnkb.NgayKham <= denngay)
                                       group hienthi by new { hienthi.MaCCHN, hienthi.TenCB } into kq


                                       select new
                                       {
                                           MaCanBo = kq.Key.MaCCHN,
                                           TenCanBo = kq.Key.TenCB,
                                           TongBenhNhan = kq.Count(),
                                       });

                BaoCao.Rep_BC_SoLuongBenhNhanTheo_BS BCSLBenhNhanTheoBacSi = new BaoCao.Rep_BC_SoLuongBenhNhanTheo_BS();
                if (DungChung.Bien.MaBV == "24012")
                {
                    BCSLBenhNhanTheoBacSi.Parameters["TenGD"].Value = DungChung.Bien.GiamDoc;
                }
                else
                    BCSLBenhNhanTheoBacSi.Parameters["TenGD"].Value = "Lại Thị Nguyệt";
                BCSLBenhNhanTheoBacSi.TenCQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                BCSLBenhNhanTheoBacSi.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                BCSLBenhNhanTheoBacSi.DataSource = HienThiDanhSach.ToList();
                BCSLBenhNhanTheoBacSi.CreateDocument();
                frmIn frm = new frmIn();
                frm.prcIN.PrintingSystem = BCSLBenhNhanTheoBacSi.PrintingSystem;
                frm.ShowDialog();
            }
            if (tungay == null)
            {
                XtraMessageBox.Show("Trường thời gian từ ngày NULL xin hãy chọn giá trị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpTuNgay.Focus();
                return;
            }
            if (denngay == null)
            {
                XtraMessageBox.Show("Trường thời gian từ ngày NULL xin hãy chọn giá trị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpDenNgay.Focus();
                return;
            }

        }

        private void frm_BCSL_BenhNhantheo_BS_Load(object sender, EventArgs e)
        {
            dtpTuNgay.DateTime = System.DateTime.Now;
            dtpDenNgay.DateTime = System.DateTime.Now;

        }
    }
}