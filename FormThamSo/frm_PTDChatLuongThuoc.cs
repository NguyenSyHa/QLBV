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
    public partial class frm_PTDChatLuongThuoc : DevExpress.XtraEditors.XtraForm
    {
        public frm_PTDChatLuongThuoc()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        
        private void frm_PTDChatLuongThuoc_Load(object sender, EventArgs e)
        {
            LupNgaytu.DateTime = DungChung.Ham.NgayTu(DateTime.Now.AddDays(-1));
            LupNgayden.DateTime = DungChung.Ham.NgayDen(DateTime.Now);
            var kho = _data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc).ToList();
            List<KPhong> kp = new List<KPhong>();
            KPhong them = new KPhong();
            them.MaKP = 0;
            them.TenKP = "Tất cả";
            kp.Add(them);
            foreach(var item in kho)
            {
                KPhong moi = new KPhong();
                moi.MaKP = item.MaKP;
                moi.TenKP = item.TenKP;
                kp.Add(moi);
            }
            lup_KPhong.Properties.DataSource = kp.ToList();
            var thuoc = _data.DichVus.Where(p => p.PLoai == 1).OrderBy(p => p.TenDV).ToList();
            lup_Thuoc.Properties.DataSource = thuoc.ToList();
        }

        private void ButHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void butTaoBC_Click(object sender, EventArgs e)
        {
            DateTime ngaytu = DungChung.Ham.NgayTu(LupNgaytu.DateTime);
            DateTime ngayden = DungChung.Ham.NgayDen(LupNgayden.DateTime);
            if(lup_Thuoc.Text != "")
            {
                int madv = 0, makp = 0;
                madv = Convert.ToInt32(lup_Thuoc.EditValue.ToString());
                if (lup_KPhong.Text != "" && lup_KPhong.Text != "Tất cả")
                {
                    makp = Convert.ToInt32(lup_KPhong.EditValue.ToString());
                }
                var nd = (from a in _data.NhapDs.Where(p => p.NgayNhap >= ngaytu && p.NgayNhap <= ngayden).Where(p => p.MaKP == makp || makp == 0).Where(p => p.PLoai == 1)
                          join b in _data.NhapDcts.Where(p => p.MaDV == madv) on a.IDNhap equals b.IDNhap
                          join c in _data.NhaCCs on a.MaCC equals c.MaCC into k
                          join d in _data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc) on a.MaKP equals d.MaKP
                          from k1 in k.DefaultIfEmpty()
                          select new { 
                              a.NgayNhap,
                              a.SoCT,
                              b.SoLo,
                              b.HanDung,
                              b.SoLuongN,
                              NhaSX = k1 != null ? k1.TenCC : "",
                          }).ToList();
                BaoCao.rep_PTDChatLuongThuoc rep = new BaoCao.rep_PTDChatLuongThuoc();
                frmIn frm2 = new frmIn();
                var dv = _data.DichVus.Where(p => p.MaDV == madv).ToList();
                rep.KhoaPhong.Value = lup_KPhong.Text.ToUpper();
                rep.TenThuoc.Value = dv.FirstOrDefault().TenDV + (dv.FirstOrDefault().HamLuong != null ?(" (" + dv.FirstOrDefault().HamLuong + ")") : "");
                rep.DVT.Value = dv.FirstOrDefault().DonVi;
                rep.NoiSanXuat.Value = dv.FirstOrDefault().NhaSX;
                rep.DataSource = nd.OrderBy(p => p.NgayNhap).ToList();
                rep.BindingData();
                rep.CreateDocument();
                frm2.prcIN.PrintingSystem = rep.PrintingSystem;
                frm2.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn thuốc!", "Thông báo");
            }
        }

        private void lup_KPhong_EditValueChanged(object sender, EventArgs e)
        {
            string x = "";
            if (lup_KPhong.Text != "" && lup_KPhong.Text != "Tất cả")
            {
                x = ";" + lup_KPhong.EditValue.ToString() + ";";
                var thuoc = _data.DichVus.Where(p => p.PLoai == 1 && p.MaKPsd.Contains(x)).ToList();
                lup_Thuoc.Properties.DataSource = thuoc.ToList();
            }
            else
            {
                var thuoc = _data.DichVus.Where(p => p.PLoai == 1).ToList();
                lup_Thuoc.Properties.DataSource = thuoc.ToList();
            }
        }
    }
}