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
    public partial class frm_SoTTBNDungThuocKSDB_27022 : Form
    {
        public frm_SoTTBNDungThuocKSDB_27022()
        {
            InitializeComponent();
        }

        public class BaoCao
        {
            public string TenThuoc { get; set; }
            public string DVT { get; set; }
            public double SoLuong { get; set; }
            public string GhiChu { get; set; }
            public string TenBN { get; set; }
            public string NgayKe { get; set; }
            public string DChi { get; set; }
            public string HoatChat { get; set; }
        }

        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void frm_SoTTBNDungThuocKSDB_27022_Load(object sender, EventArgs e)
        {
            txtTuNgay.DateTime = DateTime.Now;
            txtDenNgay.DateTime = DateTime.Now;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DungChung.Ham.CallProcessWaitingForm(TaoBaoCao);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TaoBaoCao()
        {

            List<BaoCao> rep = new List<BaoCao>();
            DateTime TuNgay = DungChung.Ham.NgayTu(txtTuNgay.DateTime);
            DateTime DenNgay = DungChung.Ham.NgayDen(txtDenNgay.DateTime);

            var thuoc = (from a in data.DThuocs.Where(p => p.MaKXuat == 25).Where(p => p.NgayKe >= TuNgay && p.NgayKe <= DenNgay)
                         join b in data.DThuoccts.Where(p => p.Status == 1) on a.IDDon equals b.IDDon
                         join c in data.DichVus.Where(p => p.MaDV == 3329 || p.MaDV == 5360 || p.MaDV == 41 || p.MaDV == 5361 || p.MaDV == 991 || p.MaDV == 3230 || p.MaDV == 5349 || p.MaDV == 5254) on b.MaDV equals c.MaDV
                         join d in data.BenhNhans on a.MaBNhan equals d.MaBNhan
                         select new
                         {
                             a.NgayKe,
                             b.SoLuong,
                             c.TenDV,
                             a.GhiChu,
                             c.DonVi,
                             d.TenBNhan,
                             d.DChi,
                             c.TenHC
                         }).OrderBy(p => p.NgayKe).ToList();

            if (thuoc.Count > 0)
            {
                foreach (var item in thuoc)
                {
                    BaoCao bc = new BaoCao();
                    bc.TenThuoc = item.TenDV;
                    bc.SoLuong = item.SoLuong;
                    bc.DVT = item.DonVi;
                    bc.GhiChu = item.GhiChu;
                    bc.NgayKe = Convert.ToDateTime(item.NgayKe).ToString("dd/MM/yyyy");
                    bc.TenBN = item.TenBNhan;
                    bc.DChi = item.DChi;
                    bc.HoatChat = item.TenHC;
                    
                    rep.Add(bc);
                }
                if (rep.Count > 0)
                {
                    Dictionary<string, object> _dic = new Dictionary<string, object>();
                    _dic.Add("Ngay", "Từ ngày " + TuNgay.ToString("dd/MM/yyyy") + " đến ngày " + DenNgay.ToString("dd/MM/yyyy"));
                    DungChung.Ham.Print(DungChung.PrintConfig.rep_ThuocKSDB_27022, rep, _dic, false);
                }
                else
                {

                    MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {

                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}
