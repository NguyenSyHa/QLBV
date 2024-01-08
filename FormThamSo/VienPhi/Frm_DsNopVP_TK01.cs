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
    public partial class Frm_DsNopVP_TK01 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_DsNopVP_TK01()
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
            if (lupKP.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn khoa để in báo cáo");
                lupKP.Focus();
                return false;
            }
            else return true;
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
              
                BaoCao.Rep_DsNopVP_TK01 rep = new BaoCao.Rep_DsNopVP_TK01();
                rep.TuNgayDenNgay.Value = "Từ ngày: " + ngaytu.ToString().Substring(0, 10) + " Đến ngày: " + ngayden.ToString().Substring(0, 10);
                int _makp = lupKP.EditValue == null ? 0 : Convert.ToInt32(lupKP.EditValue);
                var qtenkp = (from kp in dataContext.KPhongs
                              join nhapd in dataContext.NhapDs on kp.MaKP equals nhapd.MaKP
                               where (nhapd.MaKP == _makp)
                               select new { kp.TenKP }).ToList();
                if (qtenkp.Count > 0)
                {
                    rep.Khoa.Value= qtenkp.First().TenKP;
                }
                    var q = (from bn in dataContext.BenhNhans 
                         join kb in dataContext.BNKBs.Where(p=>p.MaKP==_makp) on bn.MaBNhan equals kb.MaBNhan
                         join vp in dataContext.VienPhis on bn.MaBNhan equals vp.MaBNhan
                         join vpct in dataContext.VienPhicts on vp.idVPhi equals vpct.idVPhi
                         join dv in dataContext.DichVus on vpct.MaDV equals dv.MaDV
                         join nhom in dataContext.NhomDVs on dv.IDNhom equals nhom.IDNhom
                         where (nhom.TenNhom.Contains("thuật")||nhom.TenNhom.Contains("CĐHA")||nhom.TenNhom.Contains("nghiệm")||nhom.TenNhom.Contains("giường")||nhom.TenNhom.Contains("khám"))
                         where (vp.NgayTT >= ngaytu && vp.NgayTT <= ngayden)
                         group new {bn, dv,nhom, vpct, vp } by new {kb.MaKP,vp.NgayTT, bn.TenBNhan, bn.DChi} into kq
                         select new
                         {   Makp=kq.Key.MaKP,
                             NgayThang=kq.Key.NgayTT,
                             HoTen = kq.Key.TenBNhan,
                             DiaChi = kq.Key.DChi,
                             PhauThuat = kq.Where(p => p.nhom.TenNhom.Contains("Phẫu thuật")).Sum(p => p.vpct.ThanhTien),
                             ThuThuat = kq.Where(p => p.nhom.TenNhom.Contains("Thủ thuật")).Sum(p => p.vpct.ThanhTien),
                             XQuang = kq.Where(p => p.nhom.TenNhom.Contains("CĐHA")).Where(p=>p.dv.TenDV.Contains("chụp")).Sum(p => p.vpct.ThanhTien),
                             // TPDN = kq.Where(p => p.nhom.TenNhom.Contains("")).Sum(p => p.vpct.ThanhTien),
                             SieuAm = kq.Where(p => p.nhom.TenNhom.Contains("CĐHA")).Where(p => p.dv.TenDV.Contains("Siêu âm")).Sum(p => p.vpct.ThanhTien),
                             XetNghiem = kq.Where(p => p.nhom.TenNhom.Contains("Xét nghiệm")).Sum(p => p.vpct.ThanhTien),
                             NgayGiuong = kq.Where(p => p.nhom.TenNhom.Contains("Ngày giường")).Sum(p => p.vpct.ThanhTien),
                             CongKham = kq.Where(p => p.nhom.TenNhom.Contains("khám")).Sum(p => p.vpct.ThanhTien),
                             TongTien = kq.Sum(p => p.vpct.ThanhTien),
                          }).OrderByDescending(p => p.NgayThang).OrderBy(p => p.HoTen).ToList();
                    if (q.Count > 0)
                    {

                        rep.TongTien.Value = q.Sum(p => p.TongTien).ToString();
                        rep.DataSource = q.ToList();
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

        private void Frm_DsNopVP_TK01_Load(object sender, EventArgs e)
        {
            lupNgaytu.Focus();

            var q = from TK in dataContext.KPhongs select new { TK.TenKP, TK.MaKP };
            lupKP.Properties.DataSource = q.ToList();
            lupNgaytu.DateTime = System.DateTime.Now;
            lupNgayden.DateTime = System.DateTime.Now;
         }

       
    }
}