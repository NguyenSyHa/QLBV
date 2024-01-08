using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormNhap
{
    public partial class frm_SaoDonNgTru : DevExpress.XtraEditors.XtraForm
    {
        string sthe = "";
        int mabn = 0;
        int makho = 0;
        public frm_SaoDonNgTru(string st, int mbn,int makho)
        {
            InitializeComponent();
            sthe = st;
            mabn = mbn;
            this.makho = makho;
        }
        QLBV_Database.QLBVEntities DaTaContext;

        List<DichVu> _ldichvu = new List<DichVu>();
        private void frm_SaoDonNgTru_Load(object sender, EventArgs e)
        {
            DaTaContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime Update = Convert.ToDateTime("2018-01-01 00:00:00");
            if (DateTime.Now < Update)
                DungChung.Ham.UpDateMaKXuat(DaTaContext);
            
            txtTenMaSo.Text = sthe;
            List<listKP> _lkp = new List<listKP>();
            var _lKPhong_data = DaTaContext.KPhongs.Where(p => p.Status == 1).ToList();
            _lkp = (from kp in _lKPhong_data
                    where (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham)
                    select new listKP { TenKP = kp.TenKP, MaKP = kp.MaKP }).OrderBy(p => p.TenKP).ToList();
            _lkp.Add(new listKP { MaKP = 0, TenKP = "" });
            lupTimMaKP.Properties.DataSource = _lkp;
            //if (DungChung.Bien.CapDo == 9)
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
                lupTimMaKP.Properties.ReadOnly = false;
            else
                lupTimMaKP.Properties.ReadOnly = true;
            lupTimMaKP.EditValue = DungChung.Bien.MaKP;
            dtTimTuNgay.DateTime = DateTime.Now.AddMonths(-3);
            dtTimDenNgay.DateTime = DateTime.Now;
            lupMaDuocdt.DataSource = DaTaContext.DichVus.ToList();

            txtTenMaSo.Focus();
            txtTenMaSo.SelectAll();
            TimKiem();
        }
        void TimKiem()
        {
            DaTaContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string ten = txtTenMaSo.Text.Trim().ToUpper();
            DateTime ngaytu = dtTimTuNgay.DateTime;
            DateTime ngayden = dtTimDenNgay.DateTime.AddDays(1).AddMilliseconds(-1);
            int mabn = 0;
            int.TryParse(ten, out mabn);
            int makp = 0;
            string makpn = "", _makho = makho.ToString();
            if (lupTimMaKP.EditValue != null)
            {
                makp = Convert.ToInt32(lupTimMaKP.EditValue);
                makpn = makp.ToString();
            }

            _ldichvu = DaTaContext.DichVus.Where(p => p.PLoai == 1 && p.Status == 1).Where(p => p.MaKPsd.Contains(makpn) && p.MaKPsd.Contains(_makho)).ToList();
            var dsbn = (from bn in DaTaContext.BenhNhans.Where(p => p.MaKCB == DungChung.Bien.MaBV && p.NoiTru == 0 && p.DTNT == false)
                        join bnkb in DaTaContext.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                        join dt in DaTaContext.DThuocs.Where(p => p.PLDV == 1 && p.MaKXuat== makho && p.MaKP == makp) on bn.MaBNhan equals dt.MaBNhan
                        where (bnkb.NgayKham >= ngaytu && bnkb.NgayKham <= ngayden)
                        select new { dt.IDDon, bn.SThe, bn.MaBNhan, bn.TenBNhan, bn.NamSinh, bnkb }).ToList();
            var q = (from a in dsbn
                     select new { a.IDDon, a.SThe, a.TenBNhan, a.MaBNhan, a.NamSinh, a.bnkb.NgayKham, MaBenh = DungChung.Ham.FreshString((a.bnkb.MaICD ?? "") + " / " + (a.bnkb.MaICD2 ?? "") + " / " + (a.bnkb.ChanDoan ?? "") + " / " + (a.bnkb.BenhKhac ?? "")) }
                     ).ToList();
            grcBNhankb.DataSource = (from a in q
                                     where (a.MaBNhan == mabn || a.TenBNhan.ToUpper().Contains(ten) || a.SThe.ToUpper().Contains(ten) || a.MaBenh.ToUpper().Contains(ten))
                                     select a).ToList();
        }
        List<DThuocct> _ldthuocct = new List<DThuocct>();
        private void grvBNhankb_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            _ldthuocct.Clear();
            int IDDon = 0;
            
            if (grvBNhankb.GetFocusedRowCellValue(colIDDonBN) != null)
                IDDon = Convert.ToInt32(grvBNhankb.GetFocusedRowCellValue(colIDDonBN));
            if (IDDon > 0)
            {
                DaTaContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                _ldthuocct = (from dv in _ldichvu
                              join dtct in DaTaContext.DThuoccts.Where(p => p.IDDon == IDDon) on dv.MaDV equals dtct.MaDV
                              select dtct).ToList();
            }
            foreach (var item in _ldthuocct)
            {
                item.IDDon = 0;
                item.Status = 0;
                item.SoPL = 0;
                item.IDKB = 0;
            }
            grcDonThuocct.DataSource = "";
            grcDonThuocct.DataSource = _ldthuocct;
        }

        private void grvBNhankb_DataSourceChanged(object sender, EventArgs e)
        {
            grvBNhankb_FocusedRowChanged(null, null);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public delegate void getlist(List<DThuocct> dthuocct);
        public getlist getdata;
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            getdata(_ldthuocct);
            this.Dispose();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void txtTenMaSo_Click(object sender, EventArgs e)
        {
            txtTenMaSo.SelectAll();
        }
    }
}