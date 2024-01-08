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
    public partial class Frm_BcTKSoPhieuLinhTheoKhoa : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcTKSoPhieuLinhTheoKhoa()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private bool KTtaoBc()
        {

            if (lupTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupTuNgay.Focus();
                return false;
            }
            if (lupDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
                return false;
            }
            else return true;
        }
         private class KPhong
        {
            private string TenKP;
            private int MaKP;
            private bool Chon;
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
            public bool chon
            { set { Chon = value; } get { return Chon; } }
        }
         List<KPhong> _lkp = new List<KPhong>();
        private void Frm_BcTKSoLuotBNKNgT_HL03_Load(object sender, EventArgs e)
        {
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
            _lkp.Clear();
            var qkp = (from dt in Data.DThuocs
                       join kp in Data.KPhongs.Where(p=>p.PLoai=="Lâm sàng") on dt.MaKP equals kp.MaKP
                       group dt by new { dt.MaKP, kp.TenKP } into kq
                       select new { kq.Key.MaKP, kq.Key.TenKP }).ToList();
            if (qkp.Count > 0)
            {
                KPhong them = new KPhong();
                them.tenkp = "A.Chọn tất cả";
                them.makp = 0;
                them.chon = true;
                _lkp.Add(them);
                foreach (var a in qkp)
                {
                    KPhong them1 = new KPhong();
                    them1.makp = a.MaKP == null ? 0 :Convert.ToInt32(a.MaKP);
                    them1.tenkp = a.TenKP;
                    them1.chon = true;
                    _lkp.Add(them1);
                }
            }
            grcKhoaphong.DataSource = _lkp.OrderBy(p => p.tenkp).ToList();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime ngaytu = System.DateTime.Now.Date;
            DateTime ngayden = System.DateTime.Now.Date;
            List<KPhong> _kphong = new List<KPhong>();
        
            if (KTtaoBc())
            {
                ngaytu = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                _kphong.Clear();
                _kphong = _lkp.Where(p => p.chon == true).ToList();
                _kphong.Add(new KPhong { makp = 0 });
                frmIn frm = new frmIn();
                BaoCao.Rep_BcTKSoPhieuLinhTheoKhoa rep = new BaoCao.Rep_BcTKSoPhieuLinhTheoKhoa();
                if (radIn.SelectedIndex == 0) { rep.InTong.Value = 1; } else { rep.InTong.Value = 0; }
                if (ckThang.Checked == true)
                {
                    rep.TG.Value = "Tháng " + ngayden.Month + " năm " + ngayden.Year;
                }
                else rep.TG.Value = "Từ ngày " + ngaytu.ToString().Substring(0, 10) + " đến ngày " + ngayden.ToString().Substring(0,10);
                int _dl = -1, _cl = -1, _huy = -1;
                if (ckDa.Checked == true) { _dl = 1; }
                if (ckChua.Checked == true) { _cl = 0; }
                if (ckHuy.Checked == true) { _huy = 2; }
                var qdt = (from dt in Data.DThuocs
                           join dtct in Data.DThuoccts on dt.IDDon equals dtct.IDDon
                           select new { dt.MaKP, dtct.SoPL, dt.KieuDon, dt.NgayKe, dtct.Status, dtct.MaDV, dtct.SoLuong }).ToList();
                var qpl = (from kp in _kphong
                           join dt in qdt.Where(p => p.NgayKe >= ngaytu && p.NgayKe <= ngayden).Where(p=>p.Status==_dl||p.Status==_cl||p.Status==_huy) on kp.makp equals dt.MaKP
                           join dv in Data.DichVus on dt.MaDV equals dv.MaDV
                           group dt by new { dt.SoPL, dt.KieuDon, dt.MaDV,dv.TenDV ,kp.tenkp} into kq
                           select new
                           {
                               kq.Key.SoPL,
                               KieuDon=kq.Key.KieuDon==0?"Hàng ngày":(kq.Key.KieuDon==1?"Bổ sung":"Trả thuốc"),
                               kq.Key.TenDV,kq.Key.tenkp,
                               SL = kq.Sum(p=>p.SoLuong),
                           }).ToList();
                    rep.DataSource = qpl.OrderBy(p=>p.SoPL).ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
              
          
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChon")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("tenkp") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("tenkp").ToString();

                    if (Ten == "A.Chọn tất cả")
                    {
                        if (_lkp.First().chon == true)
                        {
                            foreach (var a in _lkp)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lkp)
                            {
                                a.chon = true;
                            }
                        }
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _lkp.ToList();
                    }
                }
            }
        }
    }
}