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
    public partial class Frm_BkChiTietPhauThuat_BG01 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BkChiTietPhauThuat_BG01()
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
        public class BNPT
        {
            private int mabnhan;
            private string tenbnhan;
            private string diachi;
            private string khoa;
            private string ngayvao;
            private string ngaytt;
            private int thuoc;
            private int tieuhao;
            private int thuthuat;
            private int tong;
            public int MaBNhan
            {
                set { mabnhan = value; }
                get { return mabnhan; }
            }
            public string TenBNhan
            {
                set { tenbnhan = value; }
                get { return tenbnhan; }
            }
            public string DChi
            { set { diachi = value; } get { return diachi; } }
            public string NgayVao
            { set { ngayvao = value; } get { return ngayvao; } }
            public string Khoa
            { set { khoa = value; } get { return khoa; } }
            public string NgayTT
            { set { ngaytt = value; } get { return ngaytt; } }
            public int Thuoc
            { set { thuoc = value; } get { return thuoc; } }
            public int TieuHao
            { set { tieuhao = value; } get { return tieuhao; } }
            public int ThuThuat
            { set { thuthuat = value; } get { return thuthuat; } }
            public int Tong
            { set { tong = value; } get { return tong; } }
        }
        List<BNPT> _BNPT = new List<BNPT>();
        private void btnOK_Click(object sender, EventArgs e)
        {
            frmIn frm = new frmIn();

            if (kt())
            {
                DateTime ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                DateTime ngayden = DungChung.Ham.NgayDen(lupNgayden.DateTime);

                BaoCao.Rep_BkChiTietPhauThuat_BG01 rep = new BaoCao.Rep_BkChiTietPhauThuat_BG01();
                //rep.TuNgay.Value = lupNgaytu.Text;
                //rep.DenNgay.Value = lupNgayden.Text;
                rep.TuNgayDenNgay.Value = " Từ ngày " + lupNgaytu.Text.Substring(0, 10) + " đến ngày " + lupNgayden.Text.Substring(0, 10);
                rep.TieuDe.Value = ("bảng kê chi tiết phẫu thuật thu phí tháng " + lupNgayden.Text.Substring(3,2)).ToUpper();
                //var KL = dataContext.BenhNhans.ToList();
                //string mm = KL.First().TenBNhan;
                //MessageBox.Show(mm);
                var qbn = (from bn in dataContext.BenhNhans.Where(p=>p.DTuong=="Dịch vụ")
                           join kp in dataContext.KPhongs on bn.MaKP equals kp.MaKP
                           join vp in dataContext.VienPhis.Where(p => p.NgayTT >= ngaytu && p.NgayTT <= ngayden) on bn.MaBNhan equals vp.MaBNhan
                           join vpct in dataContext.VienPhicts on vp.idVPhi equals vpct.idVPhi
                           join tu in (from tu2 in dataContext.TamUngs.Where(p => p.PhanLoai == 1 || p.PhanLoai == 2 || p.PhanLoai == 3) select new { tu2.MaBNhan }).Distinct() on vp.MaBNhan equals tu.MaBNhan
                           join dv in dataContext.DichVus on vpct.MaDV equals dv.MaDV
                            join ndv in dataContext.NhomDVs.Where(p => p.TenNhomCT == "Thủ thuật, phẫu thuật") on dv.IDNhom equals ndv.IDNhom
                            group new { bn, kp, vp, vpct, ndv } by new {bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.NNhap, kp.TenKP, vp.NgayTT } into kq
                           select new
                           {
                               MaBNhan=kq.Key.MaBNhan,
                               TenBN = kq.Key.TenBNhan,
                               DChi = kq.Key.DChi,
                               Khoa = kq.Key.TenKP,
                               NgayVao = kq.Key.NNhap,
                               NgayTT = kq.Key.NgayTT,
                           }).OrderByDescending(p => p.NgayTT).ToList();
                if (qbn.Count > 0)
                {
                    foreach (var a in qbn)
                    {
                        BNPT themmoi = new BNPT();
                        themmoi.MaBNhan = a.MaBNhan;
                        themmoi.TenBNhan = a.TenBN;
                        themmoi.DChi = a.DChi;
                        themmoi.Khoa = a.Khoa;
                        themmoi.NgayVao= a.NgayVao.ToString().Substring(0, 5);
                        themmoi.NgayTT = a.NgayTT.ToString().Substring(0, 5);
                        _BNPT.Add(themmoi);
                    }
                }
                var qtt = (from bn in dataContext.BenhNhans 
                           join dt in dataContext.DThuocs on bn.MaBNhan equals dt.MaBNhan
                           join dtct in dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                           join dv in dataContext.DichVus on dtct.MaDV equals dv.MaDV
                           //join tn in dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                           join ndv in dataContext.NhomDVs on dv.IDNhom equals ndv.IDNhom
                           group new {bn, dt, dtct,ndv } by new { bn.MaBNhan } into kq
                           select new
                           {
                               MaBNhan = kq.Key.MaBNhan,
                               Thuoc = kq.Where(p => p.ndv.TenNhom.Contains("Thuốc")).Sum(p => p.dtct.ThanhTien),
                               TieuHao = kq.Where(p => p.ndv.TenNhom.Contains("Vật tư tiêu hao")).Sum(p => p.dtct.ThanhTien),
                               ThuThuat = kq.Where(p => p.ndv.TenNhom.Contains("Phẫu thuật")).Sum(p => p.dtct.ThanhTien),
              //                 TongTien = kq.Where(p => p.ndv.TenNhom.Contains("Thuốc")).Sum(p => p.dtct.ThanhTien) + kq.Where(p => p.ndv.TenNhom.Contains("Vật tư tiêu hao")).Sum(p => p.dtct.ThanhTien) + kq.Where(p => p.ndv.TenNhom.Contains("Thủ thuật")).Sum(p => p.dtct.ThanhTien),
                              
                             }).ToList();
                if (qtt.Count > 0)
                {
                    foreach (var n in _BNPT)
                    {
                        foreach (var m in qtt)
                        {
                            if (n.MaBNhan == m.MaBNhan)
                            {
                                if (m.Thuoc != null && m.Thuoc != 0)
                                {
                                    n.Thuoc = Convert.ToInt32(m.Thuoc);
                                }
                                else { n.Thuoc = 0; }
                                if (m.TieuHao != null && m.TieuHao != 0)
                                {
                                    n.TieuHao = Convert.ToInt32(m.TieuHao);
                                }
                                else { n.TieuHao = 0; }
                                if (m.ThuThuat != null && m.ThuThuat != 0)
                                {
                                    n.ThuThuat = Convert.ToInt32(m.ThuThuat);
                                }
                                else { n.ThuThuat = 0; }
                                //if (m.TongTien != null && m.TongTien != 0)
                                //{
                                //}
                              }
                            //n.Tong = Convert.ToInt32(m.Thuoc + m.TieuHao + m.ThuThuat);
             
                        }
                    }

                }

                rep.DataSource = _BNPT.OrderBy(p => p.NgayTT);
                rep.ThuocT.Value = _BNPT.Sum(p => p.Thuoc);
                rep.TieuHaoT.Value = _BNPT.Sum(p=>p.TieuHao);
                rep.TThuatT.Value = _BNPT.Sum(p=>p.ThuThuat);
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                
            } 
        }

        private void Frm_BkChiTietPhauThuat_BG01_Load(object sender, EventArgs e)
        {
            lupNgaytu.Focus();
            lupNgaytu.DateTime = System.DateTime.Now;
            lupNgayden.DateTime = System.DateTime.Now;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}