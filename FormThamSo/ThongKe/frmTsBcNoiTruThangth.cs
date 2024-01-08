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
    public partial class frmTsBcNoiTruThangth : DevExpress.XtraEditors.XtraForm
    {
        public frmTsBcNoiTruThangth()
        {
            InitializeComponent();
        }
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
        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;


            if (KTtaoBc())
            {
                tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);

                frmIn frm = new frmIn();
                BaoCao.repBcNoiTruThangth rep = new BaoCao.repBcNoiTruThangth();
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

                rep.TuNgay.Value = dateTuNgay.Text;
                rep.DenNgay.Value = dateDenNgay.Text;
   
                var qbn = (from bn in data.BenhNhans
                           join bnkb in data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                           join kp in data.KPhongs on bnkb.MaKP equals kp.MaKP
                           where (bn.NoiTru == 1)
                           where (bnkb.NgayKham >= tungay && bnkb.NgayKham <= denngay)
                           group new { bn, bnkb, kp } by new {bn.ChuyenKhoa, bnkb.MaKP, kp.TenKP } into kq
                           select new
                           {
                               TenKP = kq.Key.ChuyenKhoa,

                               TongSo = kq.Select(p => p.bnkb.MaBNhan).Count(),
                               BHYT = kq.Where(p => p.bn.DTuong == "BHYT").Select(p => p.bnkb.MaBNhan).Count(),
                               VienPhi = kq.Where(p => p.bn.DTuong == "VienPhi").Select(p => p.bnkb.MaBNhan).Count(),
                               CapCuu = kq.Where(p => p.bn.CapCuu == 1).Select(p => p.bnkb.MaBNhan).Count(),

                           }).ToList();



                if (qbn.Count > 0)
                {
                    rep.DataSource = qbn;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }

                else
                    MessageBox.Show("Không có dữ liệu để in báo cáo");
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}