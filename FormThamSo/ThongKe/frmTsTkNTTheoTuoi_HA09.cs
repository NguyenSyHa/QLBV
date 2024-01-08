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
    public partial class frmTsTkNTTheoTuoi_HA09 : DevExpress.XtraEditors.XtraForm
    {
        public frmTsTkNTTheoTuoi_HA09()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private bool KTtaoBcNXT()
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


            if (KTtaoBcNXT())
            {

                tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);

                frmIn frm = new frmIn();
                BaoCao.rep_TkNTTheoTuoi_HA09 rep = new BaoCao.rep_TkNTTheoTuoi_HA09();
                // QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

                rep.TuNgay.Value = dateTuNgay.Text;
                rep.DenNgay.Value = dateDenNgay.Text;

                var qsl = (from bn in data.BenhNhans
                           where (bn.NoiTru == 1 && bn.NNhap >= tungay && bn.NNhap <= denngay)
                           group new { bn } by new { bn.MaBNhan } into kq
                           select new
                           {
                              TongCongNu = kq.Where(p=>p.bn.GTinh==0).Select(p => p.bn.MaBNhan).Count(),
                              TongCongNam = kq.Where(p=>p.bn.GTinh==1).Select(p=>p.bn.MaBNhan).Count(),
                              TongCongTong = kq.Select(p => p.bn.MaBNhan).Count(),

                              T6Nu = kq.Where(p => p.bn.GTinh == 0).Where(p => p.bn.Tuoi < 6).Select(p => p.bn.MaBNhan).Count(),
                              T6Nam = kq.Where(p => p.bn.GTinh == 1).Where(p=>p.bn.Tuoi<6).Select(p => p.bn.MaBNhan).Count(),
                              T6Tong = kq.Where(p => p.bn.Tuoi < 6).Select(p => p.bn.MaBNhan).Count(),

                              T15Nu = kq.Where(p => p.bn.GTinh == 0).Where(p => p.bn.Tuoi >= 6).Where(p => p.bn.Tuoi < 15).Select(p => p.bn.MaBNhan).Count(),
                              T15Nam = kq.Where(p => p.bn.GTinh == 1).Where(p => p.bn.Tuoi >= 6).Where(p => p.bn.Tuoi < 15).Select(p => p.bn.MaBNhan).Count(),
                              T15Tong = kq.Where(p => p.bn.Tuoi >= 6).Where(p => p.bn.Tuoi < 15).Select(p => p.bn.MaBNhan).Count(),

                              T55Nu = kq.Where(p => p.bn.GTinh == 0).Where(p => p.bn.Tuoi > 55).Where(p => p.bn.Tuoi <= 60).Select(p => p.bn.MaBNhan).Count(),
                              T55Nam = kq.Where(p => p.bn.GTinh == 1).Where(p => p.bn.Tuoi > 55).Where(p => p.bn.Tuoi <= 60).Select(p => p.bn.MaBNhan).Count(),
                              T55Tong = kq.Where(p => p.bn.Tuoi > 55).Where(p => p.bn.Tuoi <= 60).Select(p => p.bn.MaBNhan).Count(),

                              T60Nu = kq.Where(p => p.bn.GTinh == 0).Where(p => p.bn.Tuoi > 60).Where(p => p.bn.Tuoi <= 80).Select(p => p.bn.MaBNhan).Count(),
                              T60Nam = kq.Where(p => p.bn.GTinh == 1).Where(p => p.bn.Tuoi > 60).Where(p => p.bn.Tuoi <= 80).Select(p => p.bn.MaBNhan).Count(),
                              T60Tong = kq.Where(p => p.bn.Tuoi > 60).Where(p => p.bn.Tuoi <= 80).Select(p => p.bn.MaBNhan).Count(),

                              T80Nu = kq.Where(p => p.bn.GTinh == 0).Where(p => p.bn.Tuoi > 80).Select(p => p.bn.MaBNhan).Count(),
                              T80Nam = kq.Where(p => p.bn.GTinh == 1).Where(p => p.bn.Tuoi > 80).Select(p => p.bn.MaBNhan).Count(),
                              T80Tong = kq.Where(p => p.bn.Tuoi > 80).Select(p => p.bn.MaBNhan).Count(),

                           }).ToList();

                if (qsl.Count > 0)
                {


                    rep.DataSource = qsl.ToList();
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