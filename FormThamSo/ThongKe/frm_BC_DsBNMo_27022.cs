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
    public partial class frm_BC_DsBNMo_27022 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_DsBNMo_27022()
        {
            InitializeComponent();
        }

        List<KPhong> _lKP = new List<KPhong>();
        private void frm_BC_DsBNMo_27022_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            deNgayTu.EditValue = DateTime.Now.Date;
            deNgayDen.EditValue = DateTime.Now.Date;
            KPhong moi = new KPhong();
            moi.MaKP = 0;
            moi.TenKP = " Tất cả";
            _lKP.Add(moi);
            var kp = data.KPhongs.Where(p => p.PLoai == "Phòng khám" || p.PLoai == "Lâm sàng").OrderBy(p => p.TenKP).ToList();
            _lKP.AddRange(kp);
            lupKPhong.Properties.DataSource = _lKP;
            lupKPhong.EditValue = 0;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        List<BNMo> _lDSBN = new List<BNMo>();
        private void btnOK_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(deNgayTu.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(deNgayDen.DateTime);
            int makhoa = 0;
            if (lupKPhong.EditValue != null)
            {
                makhoa = Convert.ToInt32(lupKPhong.EditValue);
            }
            var qdv = (from dv in data.DichVus.Where(p => p.PLoai == 2)
                       join tn in data.TieuNhomDVs.Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat)) on dv.IdTieuNhom equals tn.IdTieuNhom
                       select new { dv.MaDV, dv.TenDV }).ToList();
            var kq = (from bn in data.BenhNhans
                      join cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay) on bn.MaBNhan equals cls.MaBNhan
                      join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                      join cb in data.CanBoes on cls.MaCBth equals cb.MaCB into kqCB
                      from kq1 in kqCB.DefaultIfEmpty()
                      select new
                      {
                          bn.MaBNhan,
                          bn.TenBNhan,
                          bn.Tuoi,
                          bn.DChi,
                          cls.NgayTH,
                          TenCB = kq1 == null ? "" : kq1.TenCB,
                          cd.MaDV,
                          cls.GhiChu,
                          //dtct.TrongBH,
                          cls.DSCBTH,
                          cls.MaKP
                      }).Where(p => makhoa == 0 || p.MaKP == makhoa).ToList();
            var query = (from a in kq
                         join b in qdv on a.MaDV equals b.MaDV
                         group new { a, b } by new
                         {
                             a.MaBNhan,
                             a.TenBNhan,
                             a.Tuoi,
                             a.DChi,
                             a.NgayTH,
                             a.GhiChu,
                             a.DSCBTH,
                             a.TenCB
                         } into k
                         select new
                         {
                             k.Key.MaBNhan,
                             k.Key.TenBNhan,
                             k.Key.Tuoi,
                             k.Key.DChi,
                             NgayMo = String.Format("{0:dd/MM/yyyy}", k.Key.NgayTH),
                             CBPT = k.Key.TenCB,
                             k.Key.DSCBTH,
                             k.Key.GhiChu
                         }).ToList();
            _lDSBN.Clear();
            List<string> lcb = new List<string>();
            foreach (var item in query)
            {
                BNMo moi = new BNMo();
                moi.TenBNhan = item.TenBNhan;
                moi.Tuoi = item.Tuoi.ToString();
                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                {
                    moi.Tuoi = DungChung.Ham.TuoitheoThang(data, item.MaBNhan, "12-00");
                }
                moi.DChi = item.DChi;
                moi.NgayMo = item.NgayMo;
                moi.CBPT = item.CBPT;
                moi.Mat = "";
                if (item.DSCBTH.Contains(";") == false)
                {
                    moi.Phu1 = item.DSCBTH;
                }
                else
                {
                    lcb.Clear();
                    lcb = item.DSCBTH.Split(';').ToList();
                    moi.Phu1 = lcb[0];
                    moi.Phu2 = lcb[4];
                }
                moi.GhiChu = item.GhiChu;
                _lDSBN.Add(moi);
            }

            BaoCao.Rep_BC_DsBNMo_27022 rep = new BaoCao.Rep_BC_DsBNMo_27022();
            frmIn frm = new frmIn();
            rep.DataSource = _lDSBN.OrderBy(p => p.TenBNhan).ToList();
            rep.lblTuNgay.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        #region class BNMo
        public class BNMo
        {
            public string TenBNhan { get; set; }
            public string Tuoi { get; set; }
            public string DChi { get; set; }
            public string Mat { get; set; }
            public string NgayMo { get; set; }
            public string CBPT { get; set; }
            public string Phu1 { get; set; }
            public string Phu2 { get; set; }
            public string GhiChu { get; set; }
        }
        #endregion
    }
}