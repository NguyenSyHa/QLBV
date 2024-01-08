using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBV.BaoCao
{
    public partial class frm_BangKe_TT_BenhNhanCungChiTraBHYT : Form
    {
        public frm_BangKe_TT_BenhNhanCungChiTraBHYT()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_BangKe_TT_BenhNhanCungChiTraBHYT_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = System.DateTime.Now.Date;
            lupNgayden.DateTime = System.DateTime.Now.Date;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private class BNBHYT
        {
            public int MaBN { get; set; }
            public string TenBN { get; set; }
            public string NamSinh { get; set; }
            public string DiaChi { get; set; }
            public double SoTien { get; set; }
            public string LyDo { get; set; }
        }
        List<BNBHYT> _BN = new List<BNBHYT>();

        private void butTaoBC_Click(object sender, EventArgs e)
        {
            _BN.Clear();
            DateTime ngaytu = System.DateTime.Now.Date;
            DateTime ngayden = System.DateTime.Now.Date;
            ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            ngayden = DungChung.Ham.NgayDen(lupNgayden.DateTime);
            #region ngày thanh toán
            if (chk_NgayTT.Checked)
            {
                var bnbhyt = (from bn in data.BenhNhans
                              join vp in data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                              join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                              where bn.DTuong == "BHYT"
                              where vp.NgayTT >= ngaytu && vp.NgayTT <= ngayden
                              where vpct.TienBN != 0 && vpct.TrongBH == 1
                              group new { bn, vp, vpct } by new { bn.TenBNhan, bn.NamSinh, bn.DChi, bn.MaBNhan, vpct.TyLeTT, vpct.TyLeBHTT, vpct.TrongBH } into kq
                              select new
                              {
                                  kq.Key.TrongBH,
                                  kq.Key.TenBNhan,
                                  kq.Key.NamSinh,
                                  kq.Key.DChi,
                                  kq.Key.MaBNhan,
                                  kq.Key.TyLeTT,
                                  kq.Key.TyLeBHTT,
                                  TienBN = kq.Sum(p => p.vpct.TienBN),
                              }).ToList();
                if (bnbhyt.Count() > 0)
                {
                    foreach (var bn in bnbhyt)
                    {
                        BNBHYT them = new BNBHYT();
                        them.TenBN = bn.TenBNhan;
                        them.NamSinh = bn.NamSinh;
                        them.DiaChi = bn.DChi;
                        them.MaBN = bn.MaBNhan;
                        them.SoTien = bn.TienBN;
                        them.LyDo = "Thu " + (bn.TyLeTT - bn.TyLeBHTT) + " % BHYT bệnh nhân";

                        _BN.Add(them);
                    }
                }
                var _lbn = _BN.GroupBy(x => new
                {
                    x.TenBN,
                    x.NamSinh,
                    x.DiaChi,
                    x.MaBN
                }).Select(s => new BNBHYT
                {
                    TenBN = s.Key.TenBN,
                    NamSinh = s.Key.NamSinh,
                    DiaChi = s.Key.DiaChi,
                    MaBN = s.Key.MaBN,
                    SoTien = s.Sum(p => p.SoTien),
                    LyDo = s.OrderByDescending(x => x.SoTien).Select(x => x.LyDo).FirstOrDefault()
                }).ToList();
                if (_BN.Count() > 0)
                {
                    Rep_ChiPhiBN_CungChiTraBHYT rep = new Rep_ChiPhiBN_CungChiTraBHYT();
                    frmIn frm = new frmIn();
                    double tien = _lbn.Sum(p => p.SoTien);
                    DateTime NgayTao = DateTime.Now;
                    rep.Parameters["Ngay"].Value = "Từ ngày " + ngaytu.ToString("dd/MM/yyyy") + " Đến ngày " + ngayden.ToString("dd/MM/yyyy");
                    rep.Parameters["NgayTao"].Value = "Việt Yên, " + "Ngày " + NgayTao.ToString("dd") + " tháng " + NgayTao.ToString("MM") + " năm " + NgayTao.ToString("yyyy");
                    rep.Parameters["TongTien"].Value = tien;
                    rep.Parameters["TienChu"].Value = DungChung.Ham.DocTienBangChu(tien, "");
                    rep.DataSource = _lbn;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                    MessageBox.Show("Không có dữ liệu");
            }
            #endregion
            #region ngày ra viện
            else if (chk_NgayRV.Checked)
            {
                var bnbhyt = (from bn in data.BenhNhans
                              join vp in data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                              join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                              join dv in data.DichVus on vpct.MaDV equals dv.MaDV
                              where bn.DTuong == "BHYT" 
                              where vp.NgayRa >= ngaytu && vp.NgayRa <= ngayden
                              where vpct.TienBN != 0 && vpct.TrongBH == 1
                              group new { bn, vp, vpct } by new { bn.TenBNhan, bn.NamSinh, bn.DChi, bn.MaBNhan, vpct.TyLeTT, vpct.TyLeBHTT, vpct.TrongBH } into kq
                              select new
                              {
                                  kq.Key.TrongBH,
                                  kq.Key.TenBNhan,
                                  kq.Key.NamSinh,
                                  kq.Key.DChi,
                                  kq.Key.MaBNhan,
                                  kq.Key.TyLeTT,
                                  kq.Key.TyLeBHTT,
                                  TienBN = kq.Sum(p => p.vpct.TienBN),
                              }).ToList();
                if (bnbhyt.Count() > 0)
                {
                    foreach (var bn in bnbhyt)
                    {
                        BNBHYT them = new BNBHYT();
                        them.TenBN = bn.TenBNhan;
                        them.NamSinh = bn.NamSinh;
                        them.DiaChi = bn.DChi;
                        them.MaBN = bn.MaBNhan;
                        them.SoTien = bn.TienBN;
                        them.LyDo = "Thu " + (bn.TyLeTT - bn.TyLeBHTT) + " % BHYT bệnh nhân";
                        _BN.Add(them);
                    }
                }
                if (_BN.Count() > 0)
                {
                    Rep_ChiPhiBN_CungChiTraBHYT rep = new Rep_ChiPhiBN_CungChiTraBHYT();
                    frmIn frm = new frmIn();
                    double tien = _BN.Sum(p => p.SoTien);
                    rep.Parameters["Ngay"].Value = "Từ ngày " + ngaytu.ToString("dd/MM/yyyy") + " Đến ngày " + ngayden.ToString("dd/MM/yyyy");
                    rep.Parameters["NgayTao"].Value = "Việt Yên, " + DungChung.Ham.NgaySangChu(DateTime.Now, 1);
                    rep.Parameters["TongTien"].Value = tien;
                    rep.Parameters["TienChu"].Value = DungChung.Ham.DocTienBangChu(tien, "");
                    rep.DataSource = _BN;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                    MessageBox.Show("Không có dữ liệu");
            }
            #endregion
            else
            {
                MessageBox.Show("Chưa chọn kiểu ngày in báo cáo!!");
                return;
            }
        }

        private void chk_NgayTT_CheckedChanged(object sender, EventArgs e)
        {
            if(chk_NgayTT.Checked)
            {
                chk_NgayRV.Checked = false;
            }    
        }

        private void chk_NgayRV_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_NgayRV.Checked)
            {
                chk_NgayTT.Checked = false;
            }
        }
    }
}
