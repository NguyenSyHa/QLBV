using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.FormThamSo
{
    public partial class frm_ThongKeBNHuySoHSBA : Form
    {
        public frm_ThongKeBNHuySoHSBA()
        {
            InitializeComponent();
        }
        private class PLSoHuy
        {
            public int index { get; set; }
            public string loai { get; set; }
            public PLSoHuy(int _index, string _loai)
            {
                this.index = _index;
                this.loai = _loai;
            }

        }
        public class KhoaPhong
        {
            public int MaKP { get; set; }
            public string  TenKP { get; set; }
        }
        private void frm_ThongKeBNHuySoHSBA_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<KhoaPhong> _lstKP = new List<KhoaPhong>();
            _lstKP = _Data.KPhongs.Where(p => p.PLoai.Equals("Lâm sàng") || p.PLoai.Equals("Phòng khám")).Select(p => new KhoaPhong { MaKP=p.MaKP,TenKP= p.TenKP }).ToList();

            KhoaPhong allkp = new KhoaPhong();
            allkp.MaKP=-1;
            allkp.TenKP="Tất cả";
            _lstKP.Insert(0, allkp);

            KhoaPhong kpToanvien = new KhoaPhong();
            allkp.MaKP = 0;
            allkp.TenKP = "Toàn viện";
            _lstKP.Insert(1, kpToanvien);

            lupKP.Properties.DataSource = _lstKP;
            
            lupTuNgay.DateTime = DateTime.Now;
            lupDenNgay.DateTime = DateTime.Now ;
            List<PLSoHuy> lstSoHuy = new List<PLSoHuy>();
            PLSoHuy kloai = new PLSoHuy(0, "Tất cả");
            lstSoHuy.Add(kloai);
            PLSoHuy loai1 = new PLSoHuy(1,"Số phiếu lĩnh");
            lstSoHuy.Add(loai1);
            PLSoHuy loai2 = new PLSoHuy(2, "số vào viện");
            lstSoHuy.Add(loai2);
            PLSoHuy loai3 = new PLSoHuy(3, "Số khám bệnh");
            lstSoHuy.Add(loai3);
            PLSoHuy loai4 = new PLSoHuy(4, "Số bệnh án");
            lstSoHuy.Add(loai4);
            PLSoHuy loai5 = new PLSoHuy(5, "Số chuyển viện");
            lstSoHuy.Add(loai5);
            PLSoHuy loai6 = new PLSoHuy(6, "Số TT thanh toán trong ngày(27001)");
            lstSoHuy.Add(loai6);
            PLSoHuy loai7 = new PLSoHuy(7, "Số lưu trữ(số hồ sơ)");
            lstSoHuy.Add(loai7);
            PLSoHuy loai8 = new PLSoHuy(8, "Mã y tế");
            lstSoHuy.Add(loai8);
            PLSoHuy loai9 = new PLSoHuy(9, "Số ra viện");
            lstSoHuy.Add(loai9);

            lupSo.Properties.DataSource = lstSoHuy;
            lupKP.EditValue = 0;
            lupSo.EditValue = 0;

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInBaoCao_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            RepBNHuyXoaSoHSBA(lupTuNgay.Text, lupDenNgay.Text, int.Parse(lupKP.EditValue.ToString()), int.Parse(lupSo.EditValue.ToString()));
        }

        private void RepBNHuyXoaSoHSBA(string tngay, string dngay, int makp, int so)
        {
            QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            BaoCao.rep_BNHuySoHSBA rep = new BaoCao.rep_BNHuySoHSBA();
            rep.paramTuNgay.Value = "Từ ngày " + lupTuNgay.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupDenNgay.DateTime.ToString("dd/MM/yyyy");          
            rep.paramNguoiLP.Value = DungChung.Bien.NguoiLapBieu;
            rep.paramGiamDoc.Value = DungChung.Bien.GiamDoc;
            DateTime _tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            DateTime _denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            var _lstBNHuy = (from hsh in _Data.HSHuys.Where(p => p.NgayHuy >= _tungay && p.NgayHuy <= _denngay).Where(p => so == 0 || p.PLoai == so)
                             join bn in _Data.BenhNhans on hsh.MaBN equals bn.MaBNhan
                             join kp in _Data.KPhongs on hsh.MaKP equals kp.MaKP into kq from kq1 in kq.DefaultIfEmpty()
                             select new
                             {
                                 bn.TenBNhan,
                                 bn.MaBNhan,
                                 bn.DChi,
                                 TenKP = kq1 == null ? "" :  kq1.TenKP,
                                 hsh.PLoai,
                                 hsh.SoHuy,
                                 hsh.MaKP,
                                 
                             }).ToList();
            var lstBNHuy = (from a in _lstBNHuy group a by new { a.TenBNhan,a.MaBNhan, a.DChi, a.TenKP, a.MaKP } into kq select new
                      {
                          kq.Key.TenBNhan,
                          kq.Key.MaBNhan,
                          kq.Key.DChi,
                          kq.Key.TenKP,                         
                          kq.Key.MaKP,
                          sokhambenh = string.Join(",", kq.Where(p => p.PLoai == 3).Select(p => p.SoHuy).Distinct()),//  kq.Where(p=>p.PLoai == 3).Max(p=>p.SoHuy),
                          mayte = string.Join(",", kq.Where(p => p.PLoai == 8).Select(p => p.SoHuy).Distinct()),
                          sophieuCLS = 0,//a.PLoai == 3 ? a.SoHuy : null,
                          maluutru = string.Join(",", kq.Where(p => p.PLoai == 7).Select(p => p.SoHuy).Distinct()),
                          sovvien = string.Join(",", kq.Where(p => p.PLoai == 2).Select(p => p.SoHuy).Distinct()),
                          soBA = string.Join(",", kq.Where(p => p.PLoai == 4).Select(p => p.SoHuy).Distinct()),
                          soravien = string.Join(",", kq.Where(p => p.PLoai == 9).Select(p => p.SoHuy).Distinct()),
                          sochuyenvien = string.Join(",", kq.Where(p => p.PLoai == 5).Select(p => p.SoHuy).Distinct()),
                      }).OrderBy(p=>p.MaBNhan).ToList();
            if (makp != 0)
            {
                lstBNHuy = lstBNHuy.Where(p => p.MaKP == makp).ToList();
            }            
            frmIn frm = new frmIn();
            rep.DataSource = lstBNHuy;
            rep.Bind();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
           
        }
        private string GetDate(DateTime ngay)
        {
            DateTime date = DateTime.Parse(ngay.ToShortDateString());
            return date.Day.ToString() + "/" + date.Month.ToString() + "/" + date.Year.ToString();
        }

       
    }
}
