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
    public partial class Frm_DsNopVP_TK03 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_DsNopVP_TK03()
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
        private void Frm_DsNopVP_TK03_Load(object sender, EventArgs e)
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
                DateTime ngaytu = System.DateTime.Now.Date;
                DateTime ngayden = System.DateTime.Now.Date;
                ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupNgayden.DateTime);
               
                    BaoCao.Rep_DsNopVP_TK03 rep = new BaoCao.Rep_DsNopVP_TK03();
                    rep.TuNgayDenNgay.Value = "Từ ngày: " + ngaytu.ToString().Substring(0, 10) + " Đến ngày: " + ngayden.ToString().Substring(0, 10);

                    var qbh = (from bn in dataContext.BenhNhans
                               where (bn.DTuong.Contains("BHYT"))
                               join kb in dataContext.BNKBs on bn.MaBNhan equals kb.MaBNhan
                               join vp in dataContext.VienPhis on bn.MaBNhan equals vp.MaBNhan
                               join vpct in dataContext.VienPhicts on vp.idVPhi equals vpct.idVPhi
                               where (vp.NgayTT >= ngaytu && vp.NgayTT <= ngayden)
                               group new { bn, vpct, vp } by new { bn.TenBNhan, bn.NamSinh, bn.DChi } into kq
                               select new
                               {
                                   //  NgayThang = kq.Key.NgayTT,
                                   NamSinh = kq.Key.NamSinh,
                                   HoTen = kq.Key.TenBNhan,
                                   DiaChi = kq.Key.DChi,
                                   TongTien = kq.Sum(p => p.vpct.TienBH),
                               }).OrderByDescending(p => p.HoTen).ToList();
                    if (qbh.Count() > 0)
                    {
                        rep.TongTien.Value = qbh.Sum(p => p.TongTien).ToString();
                        rep.DataSource = qbh.ToList();
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