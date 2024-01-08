using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using COMExcel = Microsoft.Office.Interop.Excel;
using QLBV.DungChung;
using OpenXmlPackaging;
using System.IO;


namespace QLBV.FormThamSo
{
    public partial class frm_BCSoBN24006 : Form
    {
        public frm_BCSoBN24006()
        {
            InitializeComponent();
        }

        public class BaoCao574
        {
            public string TenKP { get; set; }
            public int BNCu { get; set; }
            public int BNVao { get; set; }
            public int BNRa { get; set; }
            public int BNChuyen { get; set; }
            public int BNHienTai { get; set; }

        }

        private class KPhongc
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
        List<KPhong> _lkpall = new List<KPhong>();
        List<KPhongc> _Kphong = new List<KPhongc>();
        QLBVEntities _Data = new QLBVEntities(DungChung.Bien.StrCon);
        List<KPhong> _lKphong = new List<KPhong>();
        List<DichVu> _ldv = new List<DichVu>();
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_BCSoBN24006_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = DateTime.Now;
            lupNgayden.DateTime = DateTime.Now;
            _lkpall = _Data.KPhongs.Where(p => p.Status == 1).ToList();
            var kphong = (from kp in _lkpall
                          where (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || kp.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang)
                          where (kp.Status == 1)
                          select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                KPhongc themmoi1 = new KPhongc();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = true;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhongc themmoi = new KPhongc();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities DaTaContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (kt())
            {
                DateTime ngaytu = System.DateTime.Now.Date;
                DateTime ngayden = System.DateTime.Now.Date;
                ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupNgayden.DateTime);
                List<int> lkp = _Kphong.Where(p => p.chon == true).Select(p => p.makp).ToList();
                List<BaoCao574> bc = new List<BaoCao574>();
                foreach (var _makp in lkp)
                {
                    if (_makp != 0)
                    {
                        BaoCao574 a = new BaoCao574();
                        // lấy số liệu demo cho viện 12/04/2022
                        string TenKP = _Kphong.Where(p => p.makp == _makp).Select(p => p.tenkp).First().ToString();
                        int bnHienTai = 0, bnChuyen = 0, bnRa = 0, bnVao = 0, bnCu = 0;
                        //bnHienTai = (from bn in DaTaContext.BenhNhans.Where(p => (p.Status == 1 || p.Status == 4 || p.Status == 5))
                        //             join bnkb in DaTaContext.BNKBs.Where(o => (o.MaKP == _makp || o.MaKPDTKH == _makp)) on bn.MaBNhan equals bnkb.MaBNhan into kq
                        //             from bkkbc in kq.DefaultIfEmpty()
                        //             where (bn.MaKP == _makp || bn.MaKPDTKH == _makp)
                        //             //where (bn.MaKCB == DungChung.Bien.MaBV)
                        //             select new { bn.MaBNhan }).Distinct().Count();
                        bnChuyen = (from bn in DaTaContext.BenhNhans.Where(p => (p.MaKP == _makp || p.MaKPDTKH == _makp))
                                    join kb in DaTaContext.RaViens.Where(p => p.Status == 1) on bn.MaBNhan equals kb.MaBNhan
                                    where (kb.NgayRa >= ngaytu && kb.NgayRa <= ngayden)
                                    //where (bn.MaKCB == DungChung.Bien.MaBV)
                                    select new { bn.MaBNhan }).Distinct().Count();
                        bnRa = (from bn in DaTaContext.BenhNhans.Where(p => (p.MaKP == _makp || p.MaKPDTKH == _makp))
                                join kb in DaTaContext.RaViens.Where(p => p.Status == 2) on bn.MaBNhan equals kb.MaBNhan
                                where (kb.NgayRa >= ngaytu && kb.NgayRa <= ngayden)
                                //where (bn.MaKCB == DungChung.Bien.MaBV)
                                select new { bn.MaBNhan }).Distinct().Count();
                        bnVao = DaTaContext.BenhNhans.Where(p => p.Status == 0 || p.Status == 1 || p.Status == 4 || p.Status == 5).Where(p => (p.MaKP == _makp || p.MaKPDTKH == _makp)).Select(p => p.MaBNhan).Distinct().Count(); // vào khoa trực tiếp    .Where(p => p.MaKCB == DungChung.Bien.MaBV)
                        bnCu = (from bn in DaTaContext.BenhNhans.Where(p => (p.Status == 1 || p.Status == 4 || p.Status == 5))
                                join vv in DaTaContext.VaoViens.Where(p => p.NgayVao < ngaytu) on bn.MaBNhan equals vv.MaBNhan
                                where (bn.MaKP == _makp || bn.MaKPDTKH == _makp)
                                //where (bn.MaKCB == DungChung.Bien.MaBV)
                                select new { bn.MaBNhan }).Distinct().Count();

                        a.TenKP = TenKP;
                        a.BNCu = bnCu;
                        a.BNVao = bnVao;
                        a.BNRa = bnRa;
                        a.BNChuyen = bnChuyen;
                        a.BNHienTai = bnCu + bnVao - bnRa - bnChuyen;
                        bc.Add(a);
                    }
                }
                #region In mới

                Dictionary<string, object> _dic = new Dictionary<string, object>();
                _dic.Add("TenCQCQ", DungChung.Bien.TenCQCQ);
                _dic.Add("TenCQ", DungChung.Bien.TenCQ);
                string ngaythang = "Từ ngày " + lupNgaytu.DateTime.ToShortDateString() + " đến ngày " + lupNgayden.DateTime.ToShortDateString();
                _dic.Add("Ngaythang", ngaythang);
                string ngayKy = DungChung.Ham.GetTenTinh(DungChung.Bien.MaBV) + ", " + DungChung.Ham.NgaySangChu(DateTime.Now, 1);
                _dic.Add("NgayKy", ngayKy);
                DungChung.Ham.Print(DungChung.PrintConfig.Rep_BCSoBN24006, bc, _dic, false);
                #endregion

            }
        }

        private bool kt()
        {
            if (string.IsNullOrEmpty(lupNgaytu.Text))
            {
                MessageBox.Show("Bạn hãy chọn ngày tháng");
                lupNgaytu.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(lupNgayden.Text))
            {
                MessageBox.Show("Bạn hãy chọn ngày tháng");
                lupNgayden.Focus();
                return false;
            }
            else if ((lupNgayden.DateTime - lupNgaytu.DateTime).Days < 0)
            {
                MessageBox.Show("Ngày đến phải lớn hơn hoặc bằng ngày từ");
                lupNgayden.Focus();
                return false;
            }
            return true;
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chọn")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("tenkp") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("tenkp").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_Kphong.First().chon == true)
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = true;
                            }
                        }
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _Kphong.ToList();
                    }
                }
            }
        }
    }
}
