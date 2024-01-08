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
    public partial class Frm_BcKSK_TKy : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcKSK_TKy()
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
        private void Frm_BcKSK_TKy_Load(object sender, EventArgs e)
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

                frmIn frm = new frmIn();
                BaoCao.Rep_BcKSK_TKy rep = new BaoCao.Rep_BcKSK_TKy();
                rep.TNDN.Value = "Từ ngày " + dateTuNgay.Text + " Đến ngày " + dateDenNgay.Text;

                var qksk = ((from bn in data.BenhNhans.Where(p => p.DTuong == "KSK").Where(p => p.NNhap >= tungay && p.NNhap <= denngay)
                             join tu in data.TamUngs on bn.MaBNhan equals tu.MaBNhan
                             // group new { bn, tu } by new { bn.NNhap, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.CDNoiGT, tu.SoTo, tu.KetLuan } into kq
                             select new
                             {
                                 bn.NNhap,
                                 bn.TenBNhan,
                                 bn.DChi,
                                 bn.GTinh,
                                 bn.Tuoi,
                                 bn.CDNoiGT,
                                 tu.SoTo,
                                 bn.ChuyenKhoa,
                             }).OrderBy(p => p.NNhap).OrderBy(p => p.TenBNhan).ToList())
                             .Select(p => new {
                                 NNhap=p.NNhap.ToString().PadLeft(10),
                                 p.TenBNhan, p.DChi,p.GTinh,p.Tuoi,p.CDNoiGT,p.SoTo,p.ChuyenKhoa,
                             }).ToList();

                if (qksk.Count > 0)
                {

                    rep.DataSource = qksk;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else MessageBox.Show("Không có dữ liệu!");

            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}