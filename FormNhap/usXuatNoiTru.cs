using System;
using QLBV_Database;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid;
using QLBV.DungChung;

namespace QLBV.FormNhap
{
    public partial class usXuatNoiTru : DevExpress.XtraEditors.XtraUserControl
    {
        bool isLoad = false;
        public usXuatNoiTru()
        {
            InitializeComponent();
            Mod_grcBenhNhankd();
            if (DungChung.Bien.MaBV == "27022")
            {
                cboKieuDon.Properties.Items.Add("Ngoại trú");
            }

            this.Load += UsXuatNoiTru_Load;
        }

        public void Mod_grcBenhNhankd()
        {
            if (DungChung.Bien.MaBV == "14017")
            {
                this.colChon.VisibleIndex = 0;
                this.colSoPL.AppearanceCell.Options.UseTextOptions = true;
                this.colSoPL.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                this.colSoPL.AppearanceHeader.Options.UseTextOptions = true;
                this.colSoPL.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                this.colSoPL.VisibleIndex = 2;
                this.colSoPL.Width = 60;
                this.colNgay.AppearanceCell.Options.UseTextOptions = true;
                this.colNgay.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                this.colNgay.AppearanceHeader.Options.UseTextOptions = true;
                this.colNgay.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                this.colNgay.VisibleIndex = 3;
                this.colNgay.Width = 100;
                this.colKieuDon.VisibleIndex = 4;
                this.colTenKP.VisibleIndex = 1;
                new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ThanhTien", this.colThanhTien, "{0:##,###.000}");
            }

            if (DungChung.Bien.MaBV == "24012")
            {
                btnXoaXuat.Visible = true;
                grvBNhanxd.OptionsSelection.MultiSelect = true;
                grvBNhanxd.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            }
        }

        QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public bool _Xoact = false;
        List<DThuocct> _lDThuocct = new List<DThuocct>();
        DateTime _ngayke = System.DateTime.Now;
        DateTime _dttu = System.DateTime.Now;
        DateTime _dtden = System.DateTime.Now;
        List<BenhNhan> _lTKbn = new List<BenhNhan>();
        public List<DThuocct> lstdtct = new List<DThuocct>();
        int _makpxd = 0;
        int _loaiduoc = -1;
        int _kieudon = -1;
        int[] arrIDdon;
        class DSPhieuLinh
        {
            int maBNhan;
            public int MaBNhan
            {
                get { return maBNhan; }
                set { maBNhan = value; }
            }

            string tenBNhan;
            public string TenBNhan
            {
                get { return tenBNhan; }
                set { tenBNhan = value; }
            }
            int soPL;

            public int SoPL
            {
                get { return soPL; }
                set { soPL = value; }
            }
            string tenKP;

            public string TenKP
            {
                get { return tenKP; }
                set { tenKP = value; }
            }
            int maKP;

            public int MaKP
            {
                get { return maKP; }
                set { maKP = value; }
            }
            int kieuDon;

            public int KieuDon
            {
                get { return kieuDon; }
                set { kieuDon = value; }
            }
            bool chon;

            public bool Chon
            {
                get { return chon; }
                set { chon = value; }
            }
            int maKXuat;

            public int MaKXuat
            {
                get { return maKXuat; }
                set { maKXuat = value; }
            }

            int idDon;

            public int IdDon
            {
                get { return idDon; }
                set { idDon = value; }
            }

            public DateTime? Ngay { get; set; }
            public bool IsXoaXuat { get; set; }

        }
        class c_dsThuoc
        {
            DThuoc dthuoc;

            public DThuoc Dthuoc
            {
                get { return dthuoc; }
                set { dthuoc = value; }
            }
            DThuocct dthuocct;

            public DThuocct Dthuocct
            {
                get { return dthuocct; }
                set { dthuocct = value; }
            }
        }
        List<c_dsThuoc> _lDSThuoc = new List<c_dsThuoc>();
        List<DSPhieuLinh> _lDSPhieuLinh = new List<DSPhieuLinh>();
        int dem = 0;
        #region Tìm kiếm
        void TimKiem()
        {
            if (isLoad)
                return;
            _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (dem <= 0)
                return;
            _lDSThuoc.Clear();
            _lDSPhieuLinh.Clear();
            _dttu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
            _dtden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);

            int _TimBoPhan = 0;
            if (lupTimMaKP.EditValue != null)
            {
                _makpxd = Convert.ToInt32(lupTimMaKP.EditValue);
            }
            int _sopl = 0;
            if (lupTimKP.EditValue != null)
                _TimBoPhan = Convert.ToInt32(lupTimKP.EditValue);
            if (!string.IsNullOrEmpty(txtSoPL.Text))
                _sopl = Convert.ToInt32(txtSoPL.Text);
            int status = 0;
            if (chkHuydon.Checked)
                status = 2;
            grcBenhNhankd.DataSource = null;

            DateTime tungay = _dttu.AddMonths(-2);

            _lDSThuoc = new List<c_dsThuoc>();

            #region 30002
            // phần mềm hiển thị tất cả bộ phận chờ lính thuốc trong khoảng thời gian trước đó, 1 bệnh nhân kê trong 3 ngày kiên tiếp và tạo 1 phiếu lĩnh thì sẽ tìm theo ngày kê đầu tiên -- hữu hùng yêu cầu _ jira_PMQLCCNI_91
            if (Bien.MaBV == "30002")
            {
                _lDSThuoc = (from kd in _dataContext.DThuocs.Where(p => p.PLDV == 1 && p.NgayKe >= _dttu && p.NgayKe <= _dtden)
                             join dtct in _dataContext.DThuoccts.Where(p => p.Status == status && p.SoPL > 0)
                                 on kd.IDDon equals dtct.IDDon
                             select new c_dsThuoc
                             {
                                 Dthuoc = kd,
                                 Dthuocct = dtct
                             }).ToList();
            }

            #endregion
            if (chkTuTruc.Checked)
            {
                var kpTuTruc = _lKP.Where(p => p.PLoai == "Tủ trực").ToList();
                _lDSThuoc = (from kd in _dataContext.DThuocs.Where(p => p.PLDV == 1 && kpTuTruc.Select(o => o.MaKP).Contains(p.MaKXuat ?? 0) && (p.MaKP == _TimBoPhan || _TimBoPhan == 0))
                             join dtct in _dataContext.DThuoccts.Where(p => p.Status == status)
                                 on kd.IDDon equals dtct.IDDon
                             select new c_dsThuoc { Dthuoc = kd, Dthuocct = dtct }).ToList();
                if (Bien.MaBV == "24272")
                {
                    var a = (from kd in _lDSThuoc
                             join kp in kpTuTruc on kd.Dthuoc.MaKXuat equals kp.MaKP
                             join bn in _dataContext.BenhNhans on kd.Dthuoc.MaBNhan equals bn.MaBNhan
                             select new { kd.Dthuocct.SoPL, kd.Dthuoc.IDDon, kp.TenKP, kd.Dthuoc.MaKP, kd.Dthuoc.KieuDon, MaKXuat = kd.Dthuocct.MaKXuat, kd.Dthuoc.NgayKe, kd.Dthuocct.IsXoaXuat, bn.MaBNhan, bn.TenBNhan }
                            ).Distinct().OrderBy(p => p.SoPL).ToList();
                    _lDSPhieuLinh = (from b in a
                                     group b by new { b.SoPL, b.TenKP, b.MaKP, b.KieuDon, b.MaKXuat, b.MaBNhan, b.TenBNhan } into kq
                                     select new DSPhieuLinh { MaBNhan = kq.Key.MaBNhan, TenBNhan = kq.Key.TenBNhan, SoPL = kq.Key.SoPL, TenKP = kq.Key.TenKP, MaKP = kq.Key.MaKP ?? 0, KieuDon = kq.Key.KieuDon ?? -1, MaKXuat = kq.Key.MaKXuat ?? 0, Chon = false, Ngay = kq.Max(p => p.NgayKe), IsXoaXuat = kq.FirstOrDefault(o => o.IsXoaXuat == true) != null }).Distinct().OrderBy(p => p.SoPL).ToList();
                }
                else
                {
                    var a = (from kd in _lDSThuoc
                             join kp in kpTuTruc on kd.Dthuoc.MaKXuat equals kp.MaKP
                             select new { kd.Dthuocct.SoPL, kd.Dthuoc.IDDon, kp.TenKP, kd.Dthuoc.MaKP, kd.Dthuoc.KieuDon, MaKXuat = kd.Dthuocct.MaKXuat, kd.Dthuoc.NgayKe, kd.Dthuocct.IsXoaXuat }
                            ).Distinct().OrderBy(p => p.SoPL).ToList();
                    _lDSPhieuLinh = (from b in a
                                     group b by new { b.SoPL, b.TenKP, b.MaKP, b.KieuDon, b.MaKXuat } into kq
                                     select new DSPhieuLinh { SoPL = kq.Key.SoPL, TenKP = kq.Key.TenKP, MaKP = kq.Key.MaKP ?? 0, KieuDon = kq.Key.KieuDon ?? -1, MaKXuat = kq.Key.MaKXuat ?? 0, Chon = false, Ngay = kq.Max(p => p.NgayKe), IsXoaXuat = kq.FirstOrDefault(o => o.IsXoaXuat == true) != null }).Distinct().OrderBy(p => p.SoPL).ToList();
                }
            }
            else
            {
                _lDSThuoc = (from kd in _dataContext.DThuocs.Where(p => p.PLDV == 1 && p.MaKXuat == _makpxd)
                             join dtct in _dataContext.DThuoccts.Where(p => (p.Status == status) && (p.MaKP == _TimBoPhan || _TimBoPhan == 0))
                                 on kd.IDDon equals dtct.IDDon
                             select new c_dsThuoc { Dthuoc = kd, Dthuocct = dtct }).ToList();
                if (Bien.MaBV == "24272")
                {
                    _lDSPhieuLinh = (from kd in _lDSThuoc
                                     join kp in _lKP on kd.Dthuoc.MaKP equals kp.MaKP
                                     join bn in _dataContext.BenhNhans on kd.Dthuoc.MaBNhan equals bn.MaBNhan
                                     group new { kd, kp, bn } by new { bn.MaBNhan, bn.TenBNhan, kd.Dthuocct.SoPL, kp.TenKP, kd.Dthuoc.MaKP, kd.Dthuoc.KieuDon } into kq// kd.Dthuoc.SoPL
                                     select new DSPhieuLinh { MaBNhan = kq.Key.MaBNhan, TenBNhan = kq.Key.TenBNhan, SoPL = kq.Key.SoPL, TenKP = kq.Key.TenKP, MaKP = kq.Key.MaKP ?? 0, KieuDon = kq.Key.KieuDon ?? -1, Chon = false, Ngay = (DungChung.Bien.MaBV == "14017" ? kq.Min(p => p.kd.Dthuoc.NgayKe) : kq.Max(p => p.kd.Dthuoc.NgayKe)), IsXoaXuat = kq.FirstOrDefault(o => o.kd.Dthuocct.IsXoaXuat == true) != null }).Distinct().OrderBy(p => p.SoPL).ToList();
                }
                else
                {
                    _lDSPhieuLinh = (from kd in _lDSThuoc
                                    join kp in _lKP on kd.Dthuoc.MaKP equals kp.MaKP
                                    group new { kd, kp } by new { kd.Dthuocct.SoPL, kp.TenKP, kd.Dthuoc.MaKP, kd.Dthuoc.KieuDon } into kq// kd.Dthuoc.SoPL
                                    select new DSPhieuLinh { SoPL = kq.Key.SoPL, TenKP = kq.Key.TenKP, MaKP = kq.Key.MaKP ?? 0, KieuDon = kq.Key.KieuDon ?? -1, Chon = false, Ngay = (DungChung.Bien.MaBV == "14017" ? kq.Min(p => p.kd.Dthuoc.NgayKe) : kq.Max(p => p.kd.Dthuoc.NgayKe)), IsXoaXuat = kq.FirstOrDefault(o => o.kd.Dthuocct.IsXoaXuat == true) != null }).Distinct().OrderBy(p => p.SoPL).ToList();
                }
                

            }
            timKiem2();
            GC.Collect();
        }
        #endregion
        #region TImKiem2
        void timKiem2()
        {
            if (dem <= 0)
                return;
            int _sopl = 0;
            if (!string.IsNullOrEmpty(txtSoPL.Text))
                _sopl = Convert.ToInt32(txtSoPL.Text);
            int kieuDon = -1;
            if (cboKieuDon.Text == "Tủ trực Ngtrú")
                kieuDon = 8;
            else if (cboKieuDon.Text == "Ngoại trú")
                kieuDon = -1;
            else if (cboKieuDon.Text == "Tủ trực CLS" || cboKieuDon.Text == "Tủ trực")
                kieuDon = 7;
            else
                kieuDon = cboKieuDon.SelectedIndex;
            grcBenhNhankd.DataSource = null;
            foreach (var item in _lDSPhieuLinh)
            {
                if (item.Chon)
                {
                    if (item.KieuDon == 4 || item.KieuDon == 2)
                        item.Chon = false;
                    if ((kieuDon == 2 || kieuDon == 4) && (item.KieuDon != 4 && item.KieuDon != 2))
                        item.Chon = false;
                }

            }
            grcBenhNhankd.DataSource = _lDSPhieuLinh.Where(p => p.KieuDon == kieuDon || p.Chon).Where(p => _sopl == 0 ? p.SoPL > 0 : p.SoPL == _sopl).ToList();
            
            

        }

        #endregion

        #region timkiemxd
        private void timkiemxd()
        {
            _dttu = Ham.NgayTu(dtTimTuNgay.DateTime);
            _dtden = Ham.NgayDen(dtTimDenNgay.DateTime);
            int _timkho = 0;
            if (lupTimMaKP.EditValue != null && lupTimMaKP.EditValue.ToString() != "")
            {
                _timkho = Convert.ToInt32(lupTimMaKP.EditValue);
            }
            int _TimMaKP = 0;
            if (lupTimKPhong.EditValue != null && lupTimKPhong.EditValue.ToString() != "")
            {
                _TimMaKP = Convert.ToInt32(lupTimKPhong.EditValue);
            }
            int kieudon = cboKieuDon.SelectedIndex;
            if (cboKieuDon.SelectedItem.ToString() == "Ngoại trú" && DungChung.Bien.MaBV == "27022")
            {
                kieudon = -1;
            }
            //var _lnhapd = _dataContext.NhapDs.Where(p => p.NgayNhap >= _dttu && p.NgayNhap <= _dtden).ToList();

            //var bnxd2 = (from kp in _lKP
            //             join xd in _lnhapd on kp.MaKP equals xd.MaKPnx
            //             join cb in _lCanbo on xd.MaCB equals cb.MaCB into kq
            //             from kq1 in kq.DefaultIfEmpty()
            //             select new { xd.MaKPnx, xd.MaKP, xd.PLoai, xd.KieuDon, kp.TenKP, xd.XuatTD, xd.IDNhap, xd.SoPL, xd.NgayNhap, xd.SoCT, xd.MaCB, TenCB = kq1 == null ? "" : kq1.TenCB, xd.TraDuoc_KieuDon, xd.KieuDon_XDTK }).ToList();

            var bnxd2 = (from kp in _dataContext.KPhongs
                        join xd in _dataContext.NhapDs on kp.MaKP equals xd.MaKPnx
                        join cb in _dataContext.CanBoes.Where(p => p.Status == 1) on xd.MaCB equals cb.MaCB into kq
                        where kp.Status == 1 && xd.NgayNhap >= _dttu && xd.NgayNhap <= _dtden
                        from kq1 in kq.DefaultIfEmpty()
                        select new
                        {
                            xd.MaKPnx,
                            xd.MaKP,
                            xd.PLoai,
                            xd.KieuDon,
                            kp.TenKP,
                            xd.MaBNhan,
                            xd.XuatTD,
                            xd.IDNhap,
                            xd.SoPL,
                            xd.NgayNhap,
                            xd.SoCT,
                            xd.MaCB,
                            TenCB = kq1 == null ? "" : kq1.TenCB,
                            xd.TraDuoc_KieuDon,
                            xd.KieuDon_XDTK
                        }).ToList();

            if (Bien.MaBV == "14017")
            {
                if (kieudon == 2)
                {
                    var bnxd = (from xd in bnxd2.Where(p => p.PLoai == 1 && p.KieuDon == 2).Where(p => p.MaKP == (_timkho) && p.XuatTD == 1)
                                where (xd.MaKPnx == (_TimMaKP) || _TimMaKP == 0)
                                select new { xd.MaKP, xd.TenKP, xd.IDNhap, xd.NgayNhap, xd.SoCT, xd.MaCB, xd.TenCB }).OrderByDescending(p => p.SoCT);

                    grcBNhanxd.DataSource = bnxd.ToList();
                }
                else if (kieudon == 3)
                {
                    var bnxd = (from xd in bnxd2.Where(p => p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 4 || p.KieuDon == 11 || p.KieuDon == 5 || p.KieuDon == 6 || p.KieuDon == 7) && p.KieuDon_XDTK == 3).Where(p => p.MaKP == (_timkho) && p.XuatTD == 1)
                                where (xd.MaKPnx == (_TimMaKP) || _TimMaKP == 0)
                                select new { xd.MaKP, xd.TenKP, xd.IDNhap, xd.NgayNhap, xd.SoCT, xd.MaCB, xd.TenCB }).OrderByDescending(p => p.SoCT);

                    grcBNhanxd.DataSource = bnxd.ToList();
                }
                else
                {
                    var bnxd = (from xd in bnxd2.Where(p => (kieudon == 2 || kieudon == 4) ? (p.PLoai == 1 && p.KieuDon == 2) : (p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 4 || p.KieuDon == 11 || p.KieuDon == 5 || p.KieuDon == 6 || p.KieuDon == 7))).Where(p => p.MaKP == (_timkho) && p.XuatTD == 1 && (p.KieuDon_XDTK == null || p.KieuDon_XDTK == kieudon))
                                where (xd.MaKPnx == (_TimMaKP) || _TimMaKP == 0)
                                select new { xd.MaKP, xd.TenKP, xd.IDNhap, xd.NgayNhap, xd.SoCT, xd.MaCB, xd.TenCB }).OrderByDescending(p => p.SoCT);
                    grcBNhanxd.DataSource = bnxd.ToList();
                }
            }
            else if (Bien.MaBV == "24272")
            {

                var bnxd1 = (from xd in bnxd2.Where(p => (kieudon == 2 || kieudon == 4) ? (p.PLoai == 1 && p.KieuDon == 2) : (p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 4 || p.KieuDon == 11 || p.KieuDon == 5 || p.KieuDon == 6 || p.KieuDon == 7))).Where(p => p.MaKP == (_timkho) && p.XuatTD == 1)
                             join xdc in _dataContext.NhapDcts on xd.IDNhap equals xdc.IDNhap
                             join bn in _dataContext.BenhNhans on xdc.MaBNhan equals bn.MaBNhan
                             where (xd.MaKPnx == (_TimMaKP) || _TimMaKP == 0)
                            select new { xd.MaKP, xdc.MaBNhan, bn.TenBNhan, xd.TenKP, xd.IDNhap, xd.NgayNhap, xd.SoCT, xd.MaCB, xd.TenCB }).Distinct().OrderByDescending(p => p.SoCT).ToList();

                grcBNhanxd.DataSource = bnxd1.ToList();
            }
            else if (Bien.MaBV == "24012" || Bien.MaBV == "24297")
            {
                var bnxd = (from xd in bnxd2.Where(p => (kieudon == 2 || kieudon == 4) ? (p.PLoai == 1 && p.KieuDon == 2) : (p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 4 || p.KieuDon == 11 || p.KieuDon == 5 || p.KieuDon == 6 || p.KieuDon == 7))).Where(p => p.MaKP == (_timkho) && p.XuatTD == 1)
                            join dtct in _dataContext.DThuoccts on xd.SoPL equals dtct.SoPL
                            join dt in _dataContext.DThuocs on dtct.IDDon equals dt.IDDon
                            where (xd.MaKPnx == (_TimMaKP) || _TimMaKP == 0)
                            group new { xd, dt } by new { xd.MaKP, xd.TenKP, xd.IDNhap, xd.NgayNhap, xd.SoCT, xd.MaCB, xd.TenCB } into kq
                            select new { kq.Key.MaKP, kq.Key.TenKP, kq.Key.IDNhap, kq.Key.NgayNhap, kq.Key.SoCT, kq.Key.MaCB, kq.Key.TenCB, NgayTaoPL = kq.Max(p => p.dt.NgayKe) }).OrderByDescending(p => p.SoCT);

                grcBNhanxd.DataSource = bnxd.ToList();
            }
            else
            {
                var bnxd = (from xd in bnxd2.Where(p => (kieudon == 2 || kieudon == 4) ? (p.PLoai == 1 && p.KieuDon == 2) : (p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 4 || p.KieuDon == 11 || p.KieuDon == 5 || p.KieuDon == 6 || p.KieuDon == 7))).Where(p => p.MaKP == (_timkho) && p.XuatTD == 1)
                            where (xd.MaKPnx == (_TimMaKP) || _TimMaKP == 0)
                            select new { xd.MaKP, xd.TenKP, xd.IDNhap, xd.NgayNhap, xd.SoCT, xd.MaCB, xd.TenCB }).OrderByDescending(p => p.SoCT);

                grcBNhanxd.DataSource = bnxd.ToList();
            }


        }
        #endregion
        List<KPhong> _lKP = new List<KPhong>();
        List<DichVu> _lDichVu = new List<DichVu>();
        List<CanBo> _lCanbo = new List<CanBo>();
        string _maCQCQ = "";
        DungChung.ProcessCommandSQL process_CommandSQL = new DungChung.ProcessCommandSQL();

        private void UsXuatNoiTru_Load(object sender, EventArgs e)
        {
            try
            {
                isLoad = true;
                check_commandSQL.Checked = true;
                process_CommandSQL.set_connectString(DungChung.Bien.TenServer, DungChung.Bien.TenCSDL, DungChung.Bien.accountsql, DungChung.Bien.passql);
                _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var qCQCQ = _dataContext.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV).FirstOrDefault();
                if (qCQCQ != null)
                    _maCQCQ = qCQCQ.MaChuQuan;
                _lDichVu = _dataContext.DichVus.Where(p => p.PLoai == 1 || p.PLoai == 5).ToList();
                var qdv = (from dv in _lDichVu select new { dv.MaDV, TenDV = (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? (dv.TenRG ?? "") : dv.TenDV, dv.DonGia, dv.DonVi, dv.DongY, dv.DuongD, dv.HamLuong, dv.IdTieuNhom, dv.IDNhom, dv.MaQD, dv.SoDK, dv.SoTT, dv.MaCC }).ToList();
                _lCanbo = _dataContext.CanBoes.Where(p => p.Status == 1).ToList();
                lupMaDV.DataSource = qdv;
                lupMaDVct.DataSource = qdv;
                _lKP = _dataContext.KPhongs.Where(p => p.Status == 1).ToList();
                if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
                {
                    lupTimMaKP.Properties.DataSource = _lKP.Where(p => p.PLoai.Contains("Khoa dược")).ToList();
                }
                else
                {
                    var _lkp1 = (from a in _lKP.Where(p => p.PLoai.Contains("Khoa dược"))
                                 join b in DungChung.Bien.listKPHoatDong on a.MaKP equals b
                                 select a).ToList();
                    lupTimMaKP.Properties.DataSource = _lkp1;
                    lupTimMaKP.EditValue = DungChung.Bien.MaKho;
                }

                if (DungChung.Bien.MaBV != "24272")
                {
                    colMaBNhan.Visible = false;
                    colMaBNhanxd.Visible = false;
                    colTenBNhan.Visible = false;
                    colTenBNhanxd.Visible = false;
                }

                var kp = (from kphong in _lKP where (kphong.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc || kphong.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || kphong.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc || kphong.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham || kphong.PLoai == DungChung.Bien.st_PhanLoaiKP.CanLamSang) select kphong).ToList();
                lupTimKP.Properties.DataSource = kp.ToList();
                lupTimKPhong.Properties.DataSource = kp.ToList();
                dtTimDenNgay.DateTime = System.DateTime.Now;
                dtTimTuNgay.DateTime = System.DateTime.Now;
                dtNgayXuat.DateTime = System.DateTime.Now;
                List<kieudon> _lkd = new List<kieudon>();
                _lkd.Add(new kieudon { Ten = "Hàng ngày", KieuDon = 0 });
                _lkd.Add(new kieudon { Ten = "Bổ xung", KieuDon = 1 });
                _lkd.Add(new kieudon { Ten = "Trả thuốc", KieuDon = 2 });
                _lkd.Add(new kieudon { Ten = "Lĩnh về khoa|Phòng", KieuDon = 3 });
                _lkd.Add(new kieudon { Ten = "Khoa|Phòng trả thuốc", KieuDon = 4 });
                _lkd.Add(new kieudon { Ten = "Trực(Ngoài giờ)", KieuDon = 5 });
                lupKieuDongv.DataSource = _lkd.ToList();
                if (DungChung.Bien.MaBV == "14017")
                {
                    colGhiChu.GroupIndex = 0;
                    grvDonThuocct.ExpandAllGroups();
                    this.cboKieuDon.Properties.Items[2] = "Trả thuốc nội trú"; // sửa trả thuốc BN -> trả thuốc nội trú
                }
                if (DungChung.Bien.MaBV != "24012")
                {
                    colNgayTaoPL.Visible = false;
                }

                dem++;
                isLoad = false;
                TimKiem();
                timkiemxd();
            }
            finally
            {
                isLoad = false;
            }
        }
        public class kieudon
        {
            public int index;
            public string ten;
            public int KieuDon
            {
                set { index = value; }
                get { return index; }
            }
            public string Ten
            {
                set { ten = value; }
                get { return ten; }
            }
        }

        private void grvBenhNhankd_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            _lDThuocct = new List<DThuocct>();
            int _makp = 0;
            int sopl = 0;
            if (grvBenhNhankd.GetFocusedRowCellValue(colSoPL) != null && grvBenhNhankd.GetFocusedRowCellValue(colSoPL).ToString() != "")
            {
                if (grvBenhNhankd.GetFocusedRowCellValue(colMaKP) != null)
                    _makp = Convert.ToInt32(grvBenhNhankd.GetFocusedRowCellValue(colMaKP));
                if (DungChung.Bien.MaBV != "19048")
                    _makp = 0;
                var mabnnn = grvBenhNhankd.GetFocusedRowCellValue(colMaBNhan);
                int _mabnnn = mabnnn == null ? 0 : Convert.ToInt32(mabnnn);
                groupControl1.Text = "Chi tiết phiếu lĩnh";
                sopl = Convert.ToInt32(grvBenhNhankd.GetFocusedRowCellValue(colSoPL));
                txtSoPLinh.Text = sopl.ToString();
                var dsdon = (from kd in _lDSThuoc.Where(p => p.Dthuocct.SoPL == sopl && (_makp == 0 ? true : p.Dthuoc.MaKP == _makp)).Where(p => p.Dthuocct.Status == 0 || p.Dthuocct.Status == null || p.Dthuocct.Status == 2)
                             select kd).ToList();
                if (DungChung.Bien.MaBV == "24272")
                {
                    dsdon = (from kd in _lDSThuoc.Where(p => p.Dthuocct.SoPL == sopl && (_makp == 0 ? true : p.Dthuoc.MaKP == _makp)).Where(p => p.Dthuocct.Status == 0 || p.Dthuocct.Status == null || p.Dthuocct.Status == 2)
                             join dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _mabnnn) on kd.Dthuocct.IDDon equals dt.IDDon
                             select kd).ToList();
                }

                _lDThuocct = (from kd in dsdon
                              join dv in _lDichVu on kd.Dthuocct.MaDV equals dv.MaDV
                              group new { kd, dv } by new { MaBNhan = chk_BNhan.Checked ? (kd.Dthuoc.MaBNhan == null ? 0 : kd.Dthuoc.MaBNhan.Value) : 0, kd.Dthuocct.Status, kd.Dthuocct.DonGia, kd.Dthuocct.DonVi, dv.TenDV, dv.MaDV, kd.Dthuocct.SoLo, kd.Dthuocct.HanDung, dv.IDNhom } into kq
                              select new { kq.Key.MaBNhan, kq.Key.TenDV, Status = kq.Key.Status == null ? 0 : kq.Key.Status.Value, MaDV = kq.Key.MaDV, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kd.Dthuocct.SoLuong), ThanhTien = kq.Sum(p => p.kd.Dthuocct.ThanhTien), DonGia = kq.Key.DonGia == null ? 0 : kq.Key.DonGia, kq.Key.SoLo, kq.Key.HanDung, kq.Key.IDNhom }).ToList().Select(x => new DThuocct
                              {
                                  TrongBH = x.MaBNhan,
                                  Status = x.Status,
                                  DuongD = x.TenDV,
                                  MaDV = x.MaDV,
                                  DonVi = x.DonVi,
                                  SoLuong = x.SoLuong,
                                  ThanhTien = x.ThanhTien,
                                  DonGia = x.DonGia,
                                  SoLo = x.SoLo,
                                  HanDung = x.HanDung,
                                  Loai = (x.IDNhom == 10 || x.IDNhom == 11) ? (byte)0 : (byte)1,
                              }).ToList<DThuocct>();

                List<DThuocct> _ldthuoct_gc = new List<DThuocct>();
                
                if (DungChung.Bien.MaBV == "24272")
                {
                    _ldthuoct_gc = (from a in _lDThuocct
                                    group a by new { a.SoLuong, a.ThanhTien, a.Status, a.DonGia, a.DonVi, a.DuongD, a.MaDV, a.SoLo, a.HanDung, a.Loai } into kq
                                    select new DThuocct
                                    {
                                        SoLo = kq.Key.SoLo,
                                        HanDung = kq.Key.HanDung,
                                        Status = kq.Key.Status,
                                        DuongD = kq.Key.DuongD,
                                        MaDV = kq.Key.MaDV,
                                        DonVi = kq.Key.DonVi,
                                        SoLuong = kq.Key.SoLuong,
                                        ThanhTien = kq.Key.ThanhTien,
                                        DonGia = kq.Key.DonGia,
                                        GhiChu = kq.Key.Loai == 1 ? "Thuốc" : "Vật tư"
                                    }).ToList<DThuocct>();
                }
                else
                {
                    _ldthuoct_gc = (from a in _lDThuocct
                                    group a by new { a.Status, a.DonGia, a.DonVi, a.DuongD, a.MaDV, a.SoLo, a.HanDung, a.Loai } into kq
                                    select new DThuocct
                                    {
                                        SoLo = kq.Key.SoLo,
                                        HanDung = kq.Key.HanDung,
                                        Status = kq.Key.Status,
                                        DuongD = kq.Key.DuongD,
                                        MaDV = kq.Key.MaDV,
                                        DonVi = kq.Key.DonVi,
                                        SoLuong = kq.Sum(p => p.SoLuong),
                                        ThanhTien = kq.Sum(p => p.ThanhTien),
                                        DonGia = kq.Key.DonGia,
                                        GhiChu = kq.Key.Loai == 1 ? "Thuốc" : "Vật tư"
                                    }).ToList<DThuocct>();
                }
                if (DungChung.Bien.MaBV == "30002")
                    grcDonThuocct.DataSource = _ldthuoct_gc.OrderBy(p => p.MaDV).ToList();
                else if (DungChung.Bien.MaBV == "14017")
                    grcDonThuocct.DataSource = _ldthuoct_gc.OrderBy(p => p.DuongD).ToList();
                else
                    grcDonThuocct.DataSource = _ldthuoct_gc.OrderBy(p => p.DonVi).ThenBy(p => p.DuongD).ToList();
                var _iddon = (from kd in _dataContext.DThuocs.Where(p => (_makp == 0 ? true : p.MaKP == _makp))
                              join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == sopl) on kd.IDDon equals dtct.IDDon
                              select dtct.IDDonct).ToList();
                int j = 0;
                arrIDdon = new int[_iddon.Count];
                foreach (var i in _iddon)
                {
                    arrIDdon[j] = i;
                    j++;
                }


            }
            else
            {
                txtSoPLinh.Text = "";
                sopl = 0;
                grcDonThuocct.DataSource = null;
            }
            GC.Collect();
        }
        public static bool _XuatDuocPL(int makp, int makho, int _sopl, DateTime _ngayxuat, bool tachBN, bool statusDT)
        {
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            try
            {
                List<KPhong> _lkphong = new List<KPhong>();
                _lkphong = _dataContext.KPhongs.Where(p => p.Status == 1).ToList();
                bool _DY_Xuat = true;
                DialogResult _result;
                string _ploai = "";
                int[] arrIDdonct = new int[1000];
                var kt = _dataContext.KPhongs.Where(p => p.MaKP == makp).ToList();
                _ploai = kt.First().PLoai;
                // kiểm tra tồn
                string _dsThuoc_kdu = "\n ";
                int _dem = 0;
                List<DThuocct> _ldthuoct_gc = new List<DThuocct>();
                List<DThuocct> _lDThuocct = new List<DThuocct>();
                List<DThuoc> _lDThuoc = new List<DThuoc>();
                var tong = (from dt in _dataContext.DThuocs
                            join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _sopl) on dt.IDDon equals dtct.IDDon
                            select new { dt, dtct }).ToList();
                _lDThuoc = (from dt in tong
                            select dt.dt).ToList();

                _lDThuocct = (from dt in tong
                              select dt.dtct).ToList();
                int i = 0;
                foreach (var item in _lDThuocct)
                {

                    arrIDdonct[i] = item.IDDonct;
                    i++;
                }
                _ldthuoct_gc = (from a in _lDThuocct
                                group a by new { a.Status, a.DonGia, a.DonVi, a.DuongD, a.MaDV, IDDonct = tachBN ? a.IDDon : 0, a.SoLo, a.HanDung } into kq
                                select new DThuocct
                                {
                                    SoLo = kq.Key.SoLo,
                                    HanDung = kq.Key.HanDung,
                                    Status = kq.Key.Status,
                                    DuongD = kq.Key.DuongD,
                                    MaDV = kq.Key.MaDV,
                                    DonVi = kq.Key.DonVi,
                                    SoLuong = kq.Sum(p => p.SoLuong),
                                    ThanhTien = kq.Sum(p => p.ThanhTien),
                                    DonGia = kq.Key.DonGia,
                                }).ToList<DThuocct>();
                foreach (var c in _ldthuoct_gc)
                {
                    int madv = 0;
                    string solo = "";
                    double dongia = 0, soluong = 0, soluongton = 0;
                    madv = c.MaDV == null ? 0 : c.MaDV.Value;
                    solo = c.SoLo;
                    dongia = c.DonGia;
                    soluong = c.SoLuong;
                    if (_ploai == "Tủ trực")
                    {
                        // kiểm tra lượng tồn

                        soluongton = DungChung.Ham._checkTon(_dataContext, madv, makho, dongia, 0, solo);
                        if (DungChung.Bien.MaBV == "30007" && DateTime.Now <= Convert.ToDateTime("01/05/2016"))
                        {
                            soluongton = 1000;
                        }
                        if (soluongton < 0)
                        {
                            _dem++;
                            var dv = _dataContext.DichVus.Where(p => p.MaDV == madv).Select(p => p.TenDV).ToList();
                            if (dv.Count > 0)
                                _dsThuoc_kdu += _dem + ". " + dv.First() + ". \n";

                        }

                    }
                    else
                    {
                        if (soluong >= 0)
                        {
                            soluongton = 0;
                            soluongton = DungChung.Ham._checkTon(_dataContext, madv, makho, dongia, 0, solo);
                            if (soluongton < 0)
                            {
                                _dem++;
                                var dv = _dataContext.DichVus.Where(p => p.MaDV == madv).Select(p => p.TenDV).ToList();
                                if (dv.Count > 0)
                                    _dsThuoc_kdu += _dem + ". " + dv.First() + ". \n";
                            }
                        }

                    }
                }

                if (_dem > 0)
                {
                    _DY_Xuat = false;
                    MessageBox.Show("Các thuốc trong kho có số lượng không đủ: " + _dsThuoc_kdu + " \n Bạn không thể xuất phiếu lĩnh này");
                }
                //

                if (_DY_Xuat)
                {
                    _result = MessageBox.Show("Xuất dược theo phiếu lĩnh số: " + _sopl + " ?", "Xuất dược", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_result == DialogResult.No)
                    {
                        _DY_Xuat = false;
                    }
                }
                if (_DY_Xuat)
                {


                    {
                        NhapD _xuat = new NhapD();
                        _xuat.MaKPnx = makp;
                        _xuat.SoCT = _sopl.ToString();
                        _xuat.PLoai = 2;
                        _xuat.XuatTD = 1;
                        _xuat.SoPL = _sopl;
                        if (kt.Count > 0)

                            switch (_ploai)
                            {
                                case "Khoa dược":
                                    _xuat.KieuDon = 2;
                                    break;
                                case "Tủ trực":
                                    _xuat.KieuDon = 6;
                                    break;
                                case "Lâm sàng":
                                    var ktdtuong = (from bn in _dataContext.BenhNhans
                                                    join dt in _dataContext.DThuocs on bn.MaBNhan equals dt.MaBNhan
                                                    join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _sopl) on dt.IDDon equals dtct.IDDon
                                                    select new { bn.DTuong }).ToList();
                                    if (ktdtuong.Count > 0)
                                    {
                                        if (ktdtuong.First().DTuong == "BHYT")
                                        {
                                            _xuat.KieuDon = 1;
                                        }
                                        else
                                        {
                                            if (DungChung.Bien.MaBV == "06007")
                                                _xuat.KieuDon = 4;
                                            else
                                                _xuat.KieuDon = 1;
                                        }
                                    }
                                    else
                                    {
                                        _xuat.KieuDon = 1;
                                    }
                                    break;
                                case "Cận lâm sàng":
                                    _xuat.KieuDon = 5;
                                    break;
                                case "Phòng khám":
                                    _xuat.KieuDon = 7;
                                    break;

                            }
                        _xuat.NgayNhap = _ngayxuat;
                        _xuat.MaKP = makho;
                        _xuat.MaCB = DungChung.Bien.MaCB;
                        _dataContext.NhapDs.Add(_xuat);
                        if (_dataContext.SaveChanges() >= 0)
                        {
                            //int iddt=Convert.ToInt32(txtIDDon.Text);
                            //var statusKD = _dataContext.DThuocs.Single(p => p.IDDon == iddt);
                            //statusKD.Status = 1;
                            //_dataContext.SaveChanges();
                            var maxid = _dataContext.NhapDs.Max(p => p.IDNhap);
                            int id = maxid;
                            foreach (var a in _ldthuoct_gc)
                            {
                                int madv = 0;
                                string macc = "";
                                double dongia = 0, soluong = 0, soluongton = 0;
                                madv = a.MaDV == null ? 0 : a.MaDV.Value;
                                macc = a.MaCC;
                                dongia = a.DonGia;
                                soluong = a.SoLuong;

                                // kiểm tra lượng tồn

                                //soluongton = DungChung.Ham._checkTon(_dataContext, madv, makho, 1, dongia, 0, macc);
                                //if (soluongton >= soluong)
                                //{
                                NhapDct _xuatct = new NhapDct();
                                _xuatct.IDNhap = id;
                                _xuatct.MaDV = a.MaDV;
                                _xuatct.DonVi = a.DonVi;
                                if (DungChung.Bien.MaBV == "30340")// || DungChung.Bien.MaBV == "01071")
                                {
                                    _xuatct.DonGiaX = a.DonGia;
                                    _xuatct.DonGia = DungChung.Ham._getGiaSD(_dataContext, a.MaDV ?? 0, a.DonGia, a.TrongBH, 1, makho, DateTime.Now);// laays don gia nhap-- 

                                }
                                else
                                    _xuatct.DonGia = a.DonGia;


                                _xuatct.SoLuongX = a.SoLuong;
                                _xuatct.ThanhTienX = Math.Round(dongia * soluong, 3);
                                _xuatct.SoLuongDY = DungChung.Ham._getSL_DongY(_dataContext, madv, soluong, makho);
                                _xuatct.ThanhTienDY = Math.Round(DungChung.Ham._getSL_DongY(_dataContext, madv, soluong, makho) * dongia, 3);
                                _xuatct.MaBNhan = a.TrongBH;
                                _xuatct.MaCC = a.MaCC;
                                _xuatct.SoLuongN = 0;
                                _xuatct.ThanhTienN = 0;
                                _xuatct.SoLuongSD = 0;
                                _xuatct.ThanhTienSD = 0;
                                _xuatct.SoLuongKK = 0;
                                _xuatct.ThanhTienKK = 0;
                                _xuatct.SoLo = a.SoLo;
                                _xuatct.HanDung = a.HanDung;
                                _dataContext.NhapDcts.Add(_xuatct);

                                //}
                                //else
                                //{
                                //    MessageBox.Show("Thuốc " + grvDonThuocct.GetRowCellDisplayText(i, colMaDV).ToString() + " trong kho không đủ");
                                //}


                            }
                            if (_lDThuocct.Count > 0)
                                _dataContext.SaveChanges();
                        }
                        // set status đã xuất vào bảng Dthuoc
                        if (statusDT)
                            foreach (var item in arrIDdonct)
                            {
                                var donthuoc = _dataContext.DThuoccts.Single(p => p.IDDonct == item);
                                donthuoc.Status = 1;
                                _dataContext.SaveChanges();
                            }
                    }


                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                return false;
            }

        }

        private void XuatDuoc(DSPhieuLinh DSPhieuLinh)
        {
            _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            bool _DY_Xuat = true;
            DialogResult _result;
            int makp = 0;
            var macqcq = _dataContext.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV).ToList();
            // kiểm tra tồn
            string _dsThuoc_kdu = "\n ";
            int _dem = 0;
            // kiểm tra khoa kê đơn
            if (chkTuTruc.Checked)
            {
                var ktkhoa = (from kd in _lDSThuoc
                              where kd.Dthuocct.SoPL == DSPhieuLinh.SoPL
                              select kd.Dthuoc.MaKXuat).Distinct().ToList();
                if (ktkhoa.Count > 1)
                {
                    MessageBox.Show("Bạn không thể xuất phiếu lĩnh của nhiều tủ trực khác nhau trên 1 chứng từ!", "Xuất dược");
                    return;
                }
                else
                {
                    if (ktkhoa.Count == 1)
                    {
                        makp = ktkhoa.First().Value;
                    }
                }
            }
            else
            {

                var ktkhoa = (from kd in _lDSThuoc
                              where kd.Dthuocct.SoPL == DSPhieuLinh.SoPL
                              select kd.Dthuoc.MaKP).Distinct().ToList();
                if (ktkhoa.Count > 1)
                {
                    MessageBox.Show("Bạn không thể xuất phiếu lĩnh của nhiều khoa khác nhau trên 1 chứng từ!", "Xuất dược");
                    return;
                }
                else
                {
                    if (ktkhoa.Count == 1)
                    {
                        makp = ktkhoa.First().Value;
                    }
                }
            }
            string _ploai = "";
            var kt = _lKP.Where(p => p.MaKP == makp).ToList();
            if (kt.Count > 0)
                _ploai = kt.First().PLoai;
            string dsChungTu = DSPhieuLinh.SoPL.ToString();
            var dsdon = (from kd in _lDSThuoc.Where(p => chkTuTruc.Checked ? p.Dthuocct.MaKXuat == makp : (makp == 0 ? true : p.Dthuoc.MaKP == makp)).Where(p => p.Dthuocct.Status == 0 || p.Dthuocct.Status == null)
                         where kd.Dthuocct.SoPL == DSPhieuLinh.SoPL
                         select kd).ToList();
            foreach (var a in dsdon)
            {
                if (a.Dthuoc.KieuDon == 3)
                    a.Dthuoc.MaBNhan = a.Dthuoc.MaBNhanChiTiet;
            }

            var _iddonct = (from ds in dsdon select new { ds.Dthuocct.IDDonct }).Distinct().ToList();
            arrIDdon = new int[_iddonct.Count];
            int j = 0;
            foreach (var i in _iddonct)
            {
                arrIDdon[j] = i.IDDonct;
                j++;
            }
            var dsmaBN = dsdon.Select(p => p.Dthuoc.MaBNhan).Distinct().ToList();
            var dsbn = (
                        from bn in _dataContext.BenhNhans
                        where dsmaBN.Contains(bn.MaBNhan)
                        select new { bn.MaBNhan, bn.IDDTBN, bn.NoiTru, bn.DTNT }).ToList();
            // lấy tên khoa
            var dskhoa = (from ds in dsdon join kp in _lKP on ds.Dthuoc.MaKP equals kp.MaKP select kp).ToList().Distinct();
            var dstenkp = dskhoa.Select(p => p.TenKP).Distinct().ToList();
            if (dstenkp.Count > 1)
            {
                MessageBox.Show("Bạn không thể xuất cho 2 khoa|Phòng khác nhau trên một phiếu xuất");
                return;
            }
            string ghichu = string.Join(";", dstenkp);
            _lDThuocct = (from kd in dsdon
                          join dv in _lDichVu on kd.Dthuocct.MaDV equals dv.MaDV
                          join bn in dsbn on kd.Dthuoc.MaBNhan equals bn.MaBNhan into lkq
                          from kq2 in lkq.DefaultIfEmpty()
                          group new { kd, dv, kq2 } by new { kd.Dthuocct.TrongBH, IDDTBN = kq2 != null ? kq2.IDDTBN : 99, MaBNhan = chk_BNhan.Checked ? (kd.Dthuoc.KieuDon == 3 ? kd.Dthuoc.MaBNhanChiTiet ?? 0 : kd.Dthuoc.MaBNhan ?? 0) : 0, kd.Dthuocct.Status, kd.Dthuocct.DonGia, kd.Dthuocct.DonVi, dv.TenDV, dv.MaDV, kd.Dthuocct.SoLo, kd.Dthuocct.HanDung } into kq
                          select new { kq.Key.TrongBH, kq.Key.SoLo, kq.Key.HanDung, kq.Key.IDDTBN, kq.Key.MaBNhan, kq.Key.TenDV, Status = kq.Key.Status == null ? 0 : kq.Key.Status.Value, MaDV = kq.Key.MaDV, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kd.Dthuocct.SoLuong) == null ? 0 : kq.Sum(p => p.kd.Dthuocct.SoLuong), ThanhTien = kq.Sum(p => p.kd.Dthuocct.ThanhTien) == null ? 0 : kq.Sum(p => p.kd.Dthuocct.ThanhTien), DonGia = kq.Key.DonGia == null ? 0 : kq.Key.DonGia }).ToList().Select(x => new DThuocct
                          {
                              IDCD = x.TrongBH,
                              IDKB = x.IDDTBN,
                              TrongBH = x.MaBNhan,
                              Status = x.Status,
                              DuongD = x.TenDV,
                              MaDV = x.MaDV,
                              DonVi = x.DonVi,
                              SoLuong = x.SoLuong,
                              ThanhTien = x.ThanhTien,
                              DonGia = x.DonGia,
                              SoLo = x.SoLo,
                              HanDung = x.HanDung
                          }).ToList<DThuocct>();
            List<DThuocct> _ldthuoct_gc = new List<DThuocct>();
            _ldthuoct_gc = (from a in _lDThuocct
                            group a by new { a.IDCD, a.IDKB, a.Status, a.DonGia, a.DonVi, a.DuongD, a.MaDV, a.SoLo, a.HanDung, a.TrongBH } into kq
                            select new DThuocct
                            {
                                IDCD = kq.Key.IDCD,
                                IDKB = kq.Key.IDKB,
                                Status = kq.Key.Status,
                                DuongD = kq.Key.DuongD,
                                MaDV = kq.Key.MaDV,
                                DonVi = kq.Key.DonVi,
                                SoLo = kq.Key.SoLo,
                                HanDung = kq.Key.HanDung,
                                SoLuong = kq.Sum(p => p.SoLuong),
                                ThanhTien = kq.Sum(p => p.ThanhTien),
                                DonGia = kq.Key.DonGia,
                                TrongBH = kq.Key.TrongBH,//mã bnhan
                            }).ToList<DThuocct>();
            //if (DungChung.Bien.MaBV == "24272")
            //{
            //    _ldthuoct_gc = (from a in _lDThuocct
            //                    group a by new { a.SoLuong, a.ThanhTien, a.IDCD, a.IDKB, a.Status, a.DonGia, a.DonVi, a.DuongD, a.MaDV, a.SoLo, a.HanDung, a.TrongBH } into kq
            //                    select new DThuocct
            //                    {
            //                        IDCD = kq.Key.IDCD,
            //                        IDKB = kq.Key.IDKB,
            //                        Status = kq.Key.Status,
            //                        DuongD = kq.Key.DuongD,
            //                        MaDV = kq.Key.MaDV,
            //                        DonVi = kq.Key.DonVi,
            //                        SoLo = kq.Key.SoLo,
            //                        HanDung = kq.Key.HanDung,
            //                        SoLuong = kq.Key.SoLuong,
            //                        ThanhTien = kq.Key.ThanhTien,
            //                        DonGia = kq.Key.DonGia,
            //                        TrongBH = kq.Key.TrongBH,//mã bnhan
            //                    }).ToList<DThuocct>();
            //}
            //else
            //{
            //    _ldthuoct_gc = (from a in _lDThuocct
            //                    group a by new { a.IDCD, a.IDKB, a.Status, a.DonGia, a.DonVi, a.DuongD, a.MaDV, a.SoLo, a.HanDung, a.TrongBH } into kq
            //                    select new DThuocct
            //                    {
            //                        IDCD = kq.Key.IDCD,
            //                        IDKB = kq.Key.IDKB,
            //                        Status = kq.Key.Status,
            //                        DuongD = kq.Key.DuongD,
            //                        MaDV = kq.Key.MaDV,
            //                        DonVi = kq.Key.DonVi,
            //                        SoLo = kq.Key.SoLo,
            //                        HanDung = kq.Key.HanDung,
            //                        SoLuong = kq.Sum(p => p.SoLuong),
            //                        ThanhTien = kq.Sum(p => p.ThanhTien),
            //                        DonGia = kq.Key.DonGia,
            //                        TrongBH = kq.Key.TrongBH,//mã bnhan
            //                    }).ToList<DThuocct>();
            //}
            List<DThuocct> _ldthuoct_checkTon = new List<DThuocct>();
            var abc = (from a in _lDThuocct
                       group a by new { a.DonGia, a.DonVi, a.DuongD, a.MaDV, a.TrongBH, a.SoLo, a.HanDung } into kq
                       select new
                       {
                           DuongD = kq.Key.DuongD,
                           MaDV = kq.Key.MaDV,
                           DonVi = kq.Key.DonVi,
                           SoLuong = kq.Sum(p => p.SoLuong),
                           ThanhTien = kq.Sum(p => p.ThanhTien),
                           DonGia = kq.Key.DonGia,
                           TrongBH = kq.Key.TrongBH,
                           kq.Key.SoLo,
                           kq.Key.HanDung
                       }).ToList();
            //if (DungChung.Bien.MaBV == "24272")
            //{
            //    abc = (from a in _lDThuocct
            //           group a by new { a.SoLuong, a.ThanhTien, a.DonGia, a.DonVi, a.DuongD, a.MaDV, a.TrongBH, a.SoLo, a.HanDung } into kq
            //           select new
            //           {
            //               DuongD = kq.Key.DuongD,
            //               MaDV = kq.Key.MaDV,
            //               DonVi = kq.Key.DonVi,
            //               SoLuong = kq.Key.SoLuong,
            //               ThanhTien = kq.Key.ThanhTien,
            //               DonGia = kq.Key.DonGia,
            //               TrongBH = kq.Key.TrongBH,
            //               kq.Key.SoLo,
            //               kq.Key.HanDung
            //           }).ToList();
            //}
            _ldthuoct_checkTon = (from a in abc
                                  select new DThuocct
                                  {
                                      SoLo = a.SoLo,
                                      HanDung = a.HanDung,
                                      DuongD = a.DuongD,
                                      MaDV = a.MaDV,
                                      DonVi = a.DonVi,
                                      SoLuong = a.SoLuong,
                                      ThanhTien = a.ThanhTien,
                                      DonGia = a.DonGia,
                                      TrongBH = a.TrongBH
                                  }).ToList<DThuocct>();
            int _ktra = 0, _ktraplus = 0;
            string _ktraten = "", _ktratenplus = "";
            foreach (var c in _ldthuoct_checkTon)
            {
                int madv = 0;
                int makho = 0;
                string solo = c.SoLo;
                double dongia = 0, soluong = 0, soluongton = 0;
                makho = lupTimMaKP.EditValue == null ? 0 : Convert.ToInt32(lupTimMaKP.EditValue); //Kho tong
                madv = c.MaDV == null ? 0 : c.MaDV.Value;

                dongia = c.DonGia;
                soluong = c.SoLuong;
                if (_ploai == "Tủ trực")
                {

                    if (soluong >= 0)
                    {
                        if (DungChung.Bien.MaBV == "30007" && DateTime.Now <= Convert.ToDateTime("01/05/2016"))
                        {
                            soluongton = 1000;
                        }
                        List<DungChung.Ham.ThuocTonTuTruc> ketqua = new List<DungChung.Ham.ThuocTonTuTruc>();
                        ketqua = DungChung.Ham.ChechTon_XuatTuTuc(_dataContext, madv, makho, dongia, soluong, solo);
                        if (ketqua.Count == 1)
                        {
                            soluongton = ketqua.First().SoLuonT;
                            if (soluongton < 0)
                            {
                                _dem++;
                                var dv = _lDichVu.Where(p => p.MaDV == madv).Select(p => (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? p.TenRG : p.TenDV).ToList();
                                if (dv.Count > 0)
                                    _dsThuoc_kdu += _dem + ". " + dv.First() + ". \n";

                            }
                            else
                            {
                                foreach (var item in _lDThuocct)
                                {
                                    if (item.MaDV == madv)
                                        item.DonGia = ketqua.First()._DonGia;
                                }
                            }
                            if (ketqua.First()._DonGia != dongia && ketqua.First()._DonGia != 0)
                            {
                                _ktra++;
                                var dv = _lDichVu.Where(p => p.MaDV == madv).Select(p => (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? p.TenRG : p.TenDV).ToList();
                                if (dv.Count > 0)
                                    _ktraten += _ktra + ". " + dv.First() + " - " + ketqua.First()._DonGia + " đồng. \n";
                            }
                        }
                        else
                        {
                            DThuocct moi = _lDThuocct.Where(p => p.MaDV == madv).FirstOrDefault();
                            _lDThuocct.Remove(moi);
                            int bbb = 1;
                            foreach (var item in ketqua)
                            {

                                _lDThuocct.Add(new DThuocct
                                {
                                    MaDV = moi.MaDV,
                                    DonGia = item._DonGia,
                                    IDDon = moi.IDDon,
                                    IDDonct = moi.IDDonct + bbb,
                                    DonVi = moi.DonVi,
                                    SoLo = moi.SoLo,
                                    ThanhTien = item._DonGia * item.SoLuonT,
                                    TienBH = moi.TienBH,
                                    TienBN = moi.TienBN,
                                    TienChenh = moi.TienChenh,
                                    TrongBH = moi.TrongBH,
                                    NgayNhap = moi.NgayNhap,
                                    DuongD = moi.DuongD,
                                    HanDung = moi.HanDung,
                                    MoiLan = moi.MoiLan,
                                    DviUong = moi.DviUong,
                                    SoLan = moi.SoLan,
                                    SoLuongct = item.SoLuonT,
                                    Loai = moi.Loai,
                                    LoaiDV = moi.LoaiDV,
                                    XHH = moi.XHH,
                                    AttachIDDonct = moi.AttachIDDonct,
                                    MaCB = moi.MaCB,
                                    MaCC = moi.MaCC,
                                    MaKP = moi.MaKP,
                                    MaKPtk = moi.MaKPtk,
                                    MaKXuat = moi.MaKXuat,
                                    TyLeTT = moi.TyLeTT,
                                    Mien = moi.Mien,
                                    GhiChu = moi.GhiChu,
                                    IDCD = moi.IDCD,
                                    DSCBTH = moi.DSCBTH,
                                    IDKB = moi.IDKB,
                                    ThanhToan = moi.ThanhToan,
                                    Status = moi.Status,
                                    SoLuong = item.SoLuonT,
                                    SoPL = moi.SoPL



                                });
                                bbb++;
                            }
                            _ktraplus++;
                            var dv = _lDichVu.Where(p => p.MaDV == madv).Select(p => (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? p.TenRG : p.TenDV).ToList();
                            if (dv.Count > 0)
                                _ktratenplus += _ktraplus + ". " + dv.First() + " \n";
                        }
                        //}
                    }

                }
                else
                {
                    if (soluong >= 0)
                    {
                        soluongton = 0;
                        soluongton = DungChung.Ham._checkTon_XuatDuoc(_dataContext, madv, makho, dongia, soluong, solo);
                        if (soluongton < 0)
                        {
                            _dem++;
                            var dv = _lDichVu.Where(p => p.MaDV == madv).Select(p => (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? p.TenRG : p.TenDV).ToList();
                            if (dv.Count > 0)
                                _dsThuoc_kdu += _dem + ". " + dv.First() + ". \n";
                        }
                    }

                }
            }

            if (_dem > 0)
            {
                _DY_Xuat = false;
                MessageBox.Show("Các thuốc trong kho có số lượng không đủ: \n" + _dsThuoc_kdu + " \n Bạn không thể xuất phiếu lĩnh này");
            }
            if (_DY_Xuat && _ktra > 0)
            {
                DialogResult aaa = MessageBox.Show("Các thuốc sau có số lượng không đủ theo đơn giá trên phiếu lĩnh, nhưng có đủ số lượng theo đơn giá sau:" + _ktraten + "\n Bạn có muốn xuất theo đơn giá này ?", "Thông báo", MessageBoxButtons.OKCancel);
                if (aaa == DialogResult.OK)
                    _DY_Xuat = true;
                else
                    _DY_Xuat = false;
            }
            if (_DY_Xuat && _ktraplus > 0)
            {
                DialogResult aaa = MessageBox.Show("Các thuốc sau có số lượng không đủ theo đơn giá trên phiếu lĩnh, nhưng có đủ số lượng tổng của nhiều đơn giá khác nhau:" + _ktratenplus + "\n Bạn có muốn xuất theo đơn giá này ?", "Thông báo", MessageBoxButtons.OKCancel);
                if (aaa == DialogResult.OK)
                    _DY_Xuat = true;
                else
                    _DY_Xuat = false;
            }
            //
            if (_DY_Xuat)
                if (string.IsNullOrEmpty(txtSoPLinh.Text) || string.IsNullOrEmpty(lupTimMaKP.Text))
                {
                    _DY_Xuat = false;
                    MessageBox.Show("Bạn chưa chọn chứng từ hoặc kho xuất");
                }
            string DSPL = "";
            int demz = 0;
            if (DungChung.Bien.MaBV == "12122")
            {
                var ktraduyet = _dataContext.SoPLs.Where(p => p.PhanLoai == 1 && p.SoPL1 == DSPhieuLinh.SoPL).FirstOrDefault();
                if (ktraduyet != null && ktraduyet.Status != 1)
                {
                    demz++;
                    DSPL += DSPhieuLinh.SoPL.ToString() + ";";
                }
                if (demz > 0)
                {
                    MessageBox.Show("Số phiếu lĩnh: " + DSPL + " chưa được duyệt \n Bạn không thể xuất!");
                    _DY_Xuat = false;
                }
            }
            if (_DY_Xuat)
            {
                int kieudon = cboKieuDon.SelectedIndex;
                if (cboKieuDon.SelectedItem.ToString() == "Ngoại trú" && DungChung.Bien.MaBV == "27022")
                {
                    kieudon = -1;
                }
                int makhox = 0;
                if (lupTimMaKP.EditValue != null && lupTimMaKP.EditValue.ToString() != "")
                    makhox = Convert.ToInt32(lupTimMaKP.EditValue);
                var nhapDuocSPL = _dataContext.NhapDs.FirstOrDefault(o => o.SoPL == DSPhieuLinh.SoPL);
                if (nhapDuocSPL != null && nhapDuocSPL.MaKPnx == makp)
                {
                    MessageBox.Show(string.Format("Phiếu lĩnh: {0} đã được xuất!", DSPhieuLinh.SoPL), "Xuất dược");
                    return;
                }
                //                Hàng ngày
                //Bổ sung
                //Trả thuốc(BN)
                //Lĩnh về khoa
                //Trả thuốc(Khoa)
                //Trực
                //Tủ trực Ngtrú
                byte IdDTBN = 0;
                {

                    NhapD _xuat = new NhapD();
                    _xuat.MaKPnx = makp;
                    _xuat.SoCT = dsChungTu;
                    if (kieudon == 2 || kieudon == 4)
                    {
                        _xuat.PLoai = 1;
                        _xuat.KieuDon = 2;
                        _xuat.SoPhieu = DungChung.Ham._idphieutheokp(1, Convert.ToInt32(makhox));

                        switch (_ploai)
                        {
                            case "Khoa dược":
                                _xuat.TraDuoc_KieuDon = 2;
                                break;
                            case "Tủ trực":
                                _xuat.TraDuoc_KieuDon = 6;
                                break;
                            case "Lâm sàng":
                                if (dsbn.Count > 0)
                                {
                                    if (kieudon == 2)
                                    {
                                        if (dsbn.First().NoiTru == 0 && dsbn.First().DTNT == true)
                                            _xuat.TraDuoc_KieuDon = 4;// thay xuất điều trị ngoại trú cho xuất nhân dân
                                        else
                                            _xuat.TraDuoc_KieuDon = 1;
                                    }
                                    else
                                        _xuat.TraDuoc_KieuDon = 11;
                                }
                                else
                                    _xuat.TraDuoc_KieuDon = 11;
                                break;
                            case "Cận lâm sàng":
                                _xuat.TraDuoc_KieuDon = 5;
                                break;
                            case "Phòng khám":
                                _xuat.TraDuoc_KieuDon = 7;
                                break;
                        }

                    }
                    else
                    {
                        _xuat.PLoai = 2;
                        _xuat.SoPhieu = DungChung.Ham._idphieutheokp(2, Convert.ToInt32(makhox));

                        if (kt.Count > 0)
                            switch (_ploai)
                            {
                                case "Khoa dược":
                                    _xuat.KieuDon = 2;
                                    break;
                                case "Tủ trực":
                                    _xuat.KieuDon = 6;
                                    break;
                                case "Lâm sàng":
                                    if (dsbn.Count > 0 && dsbn.First().NoiTru == 0 && dsbn.First().DTNT)
                                        _xuat.KieuDon = 4;// thay xuất điều trị ngoại trú cho xuất nhân dân
                                    else if (kieudon == 0 || kieudon == 1)
                                        _xuat.KieuDon = 1;
                                    else if (kieudon == 3)
                                    {
                                        if (DungChung.Bien.MaBV == "12001" || (macqcq != null && macqcq.First().MaChuQuan == "12001"))
                                        {
                                            _xuat.KieuDon = 11;
                                        }
                                        else
                                        {
                                            _xuat.KieuDon = 1;
                                        }
                                    }
                                    else
                                        _xuat.KieuDon = 11;
                                    break;
                                case "Cận lâm sàng":
                                    _xuat.KieuDon = 5;
                                    break;
                                case "Phòng khám":
                                    _xuat.KieuDon = 7;
                                    break;

                            }

                    }
                    _xuat.KieuDon_XDTK = kieudon;
                    _xuat.XuatTD = 1;
                    _xuat.SoPL = DSPhieuLinh.SoPL;
                    _xuat.NgayNhap = dtNgayXuat.DateTime;

                    _xuat.MaKP = makhox;
                    _xuat.TenNguoiCC = ghichu;
                    _xuat.MaCB = DungChung.Bien.MaCB;
                    List<DichVu> listDichVus = new List<DichVu>();
                    bool success = false;
                    if (check_commandSQL.Checked)
                    {
                        List<NhapDct> listNhapDuocs = new List<NhapDct>();
                        int _makhoxd = 0;
                        _makhoxd = lupTimMaKP.EditValue == null ? 0 : Convert.ToInt32(lupTimMaKP.EditValue);
                        if (chk_BNhan.Checked)
                        {
                            foreach (var a in _lDThuocct)
                            {
                                double _soluongn = 0, _thanhtienn = 0, _soluongx = 0, _thanhtienx = 0, _soluongdy = 0, _thanhtiendy = 0;
                                int madv = 0;
                                madv = a.MaDV == null ? 0 : a.MaDV.Value;
                                if (kieudon == 2 || kieudon == 4)
                                {
                                    _soluongn = a.SoLuong * (-1);
                                    _thanhtienn = Math.Round(a.DonGia * a.SoLuong * (-1), 3);
                                }
                                else
                                {
                                    _soluongx = a.SoLuong;
                                    _thanhtienx = Math.Round(a.DonGia * a.SoLuong, 3);
                                    _soluongdy = DungChung.Ham._getSL_DongY(_dataContext, madv, a.SoLuong, _makhoxd);
                                    _thanhtiendy = Math.Round(_soluongdy * a.DonGia, 3);
                                }

                                NhapDct insert = new NhapDct();
                                insert.MaDV = a.MaDV;
                                insert.DonVi = a.DonVi;
                                insert.DonGia = a.DonGia;
                                insert.SoLo = a.SoLo;
                                insert.HanDung = a.HanDung;
                                insert.SoLuongX = _soluongx;
                                insert.ThanhTienX = _thanhtienx;
                                insert.SoLuongN = _soluongn;
                                insert.ThanhTienN = _thanhtienn;
                                insert.SoLuongDY = _soluongdy;
                                insert.ThanhTienDY = _thanhtiendy;
                                insert.MaBNhan = a.TrongBH;
                                insert.MaCC = a.MaCC;
                                insert.IDDTBN = (byte)a.IDKB;
                                insert.TrongBH = 1;
                                listNhapDuocs.Add(insert);
                                listDichVus.Add(new DichVu { MaDV = insert.MaDV ?? 0 });
                            }
                        }
                        else
                        {
                            foreach (var a in _ldthuoct_gc)
                            {
                                double _soluongn = 0, _thanhtienn = 0, _soluongx = 0, _thanhtienx = 0, _soluongdy = 0, _thanhtiendy = 0;
                                int madv = 0;
                                madv = a.MaDV == null ? 0 : a.MaDV.Value;
                                if (kieudon == 2 || kieudon == 4)
                                {
                                    _soluongn = a.SoLuong * (-1);
                                    _thanhtienn = Math.Round(a.DonGia * a.SoLuong * (-1), 3);
                                    _soluongdy = DungChung.Ham._getSL_DongY(_dataContext, madv, a.SoLuong, _makhoxd) * (-1);
                                    _thanhtiendy = Math.Round(_soluongdy * a.DonGia, 3) * (-1);
                                }
                                else
                                {
                                    _soluongx = a.SoLuong;
                                    _thanhtienx = Math.Round(a.DonGia * a.SoLuong, 3);
                                    _soluongdy = DungChung.Ham._getSL_DongY(_dataContext, madv, a.SoLuong, _makhoxd);
                                    _thanhtiendy = Math.Round(_soluongdy * a.DonGia, 3);
                                }
                                NhapDct insert = new NhapDct();
                                insert.MaDV = a.MaDV;
                                insert.DonVi = a.DonVi;
                                insert.DonGia = a.DonGia;
                                insert.SoLo = a.SoLo;
                                insert.HanDung = a.HanDung;
                                insert.SoLuongX = _soluongx;
                                insert.ThanhTienX = _thanhtienx;
                                insert.SoLuongN = _soluongn;
                                insert.ThanhTienN = _thanhtienn;
                                insert.SoLuongDY = _soluongdy;
                                insert.ThanhTienDY = _thanhtiendy;
                                insert.MaBNhan = a.TrongBH;
                                insert.MaCC = a.MaCC;
                                insert.IDDTBN = (byte)a.IDKB;
                                insert.TrongBH = 1;
                                listNhapDuocs.Add(insert);
                                listDichVus.Add(new DichVu { MaDV = insert.MaDV ?? 0 });
                            }
                        }

                        //var dataNhapDcts = listNhapDuocs.Select(o => new NhapDuocCT(o)).ToList();



                        List<NhapDuocCT> listNDct = new List<NhapDuocCT>();
                        foreach (var i in listNhapDuocs)
                        {
                            NhapDuocCT ndct = new NhapDuocCT();
                            ndct.IDNhap = i.IDNhap;
                            ndct.MaDV = i.MaDV;
                            ndct.SoLo = i.SoLo;
                            ndct.SoDangKy = i.SoDangKy;
                            ndct.HanDung = i.HanDung;
                            ndct.DonVi = i.DonVi;
                            ndct.DonGiaCT = i.DonGiaCT;
                            ndct.DonGia = i.DonGia;
                            ndct.VAT = i.VAT;
                            ndct.SoLuongN = i.SoLuongN;
                            ndct.ThanhTienN = i.ThanhTienN;
                            ndct.SoLuongX = i.SoLuongX;
                            ndct.ThanhTienX = i.ThanhTienX;
                            ndct.SoLuongKK = i.SoLuongKK;
                            ndct.ThanhTienKK = i.ThanhTienKK;
                            ndct.SoLuongSD = i.SoLuongSD;
                            ndct.ThanhTienSD = i.ThanhTienSD;
                            ndct.MaCC = i.MaCC;
                            ndct.DonGiaDY = i.DonGiaDY;
                            ndct.SoLuongDY = i.SoLuongDY;
                            ndct.ThanhTienDY = i.ThanhTienDY;
                            ndct.MaBNhan = i.MaBNhan;
                            ndct.IDDTBN = i.IDDTBN;
                            ndct.DonGiaX = i.DonGiaX;
                            ndct.GhiChu = i.GhiChu;
                            ndct.TrongBH = i.TrongBH;
                            ndct.TyLeCK = i.TyLeCK;
                            ndct.MienCT = i.MienCT;
                            listNDct.Add(ndct);
                        }

                        foreach (var item in listNDct)
                        {
                            Console.WriteLine("A" + item.IDDTBN);
                        }

                        List<System.Data.SqlClient.SqlBulkCopyColumnMapping> listColumnMappings = new List<System.Data.SqlClient.SqlBulkCopyColumnMapping>();
                        List<string> propertiesIgnore = new List<string>();
                        List<DataColumn> columns = new List<DataColumn>();
                        DataTable dt = DungChung.Ham.ConvertToDataTable((System.Collections.IList)listNDct, ref columns, propertiesIgnore);

                        List<NhapDuoc> listNhapDs = new List<NhapDuoc>();
                        listNhapDs.Add(new NhapDuoc(_xuat));
                        List<System.Data.SqlClient.SqlBulkCopyColumnMapping> listColumnMappingNhapDs = new List<System.Data.SqlClient.SqlBulkCopyColumnMapping>();
                        List<string> propertiesIgnoreNhapD = new List<string>();
                        List<DataColumn> columnNhapDs = new List<DataColumn>();
                        DataTable dtNhapD = DungChung.Ham.ConvertToDataTable((System.Collections.IList)listNhapDs, ref columns, propertiesIgnore);

                        string[] strpara = new string[] { "@NhapD", "@NhapDct" };
                        object[] oValue = new object[] { dtNhapD, dt };
                        SqlDbType[] sqlDBType = new SqlDbType[] { SqlDbType.Structured, SqlDbType.Structured };
                        success = ADOConnect.ExecuteNonQuery("sp_XuatNoiTru", CommandType.StoredProcedure, strpara, oValue, sqlDBType, true);

                        if (DungChung.Bien.MaBV == "200012" && listDichVus.Count > 0) //tét
                        {
                            DungChung.Ham.UpdateTonDichVu(listDichVus, _xuat.MaKP ?? 0);
                        }
                        if (DungChung.Bien.MaBV == "14017")
                        {
                            var nhapDuocUpdate = _dataContext.NhapDs.FirstOrDefault(o => o.SoCT == _xuat.SoCT && o.PLoai == _xuat.PLoai && o.MaKPnx == _xuat.MaKPnx && o.MaCB == _xuat.MaCB && o.KieuDon == _xuat.KieuDon && o.XuatTD == _xuat.XuatTD && o.MaKP == _xuat.MaKP);
                            if (nhapDuocUpdate != null)
                            {
                                nhapDuocUpdate.KieuDon_XDTK = _xuat.KieuDon_XDTK;
                                _dataContext.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        _dataContext.NhapDs.Add(_xuat);
                        int count = _dataContext.SaveChanges();
                        if (count > 0)
                        {
                            int id = _xuat.IDNhap;
                            try
                            {
                                if (chk_BNhan.Checked)
                                {
                                    foreach (var a in _lDThuocct)
                                    {
                                        int madv = 0, makho = 0;
                                        string macc = "";
                                        double dongia = 0, soluong = 0;
                                        makho = lupTimMaKP.EditValue == null ? 0 : Convert.ToInt32(lupTimMaKP.EditValue);
                                        madv = a.MaDV == null ? 0 : a.MaDV.Value;
                                        macc = a.MaCC;
                                        dongia = a.DonGia;
                                        soluong = a.SoLuong;
                                        NhapDct _xuatct = new NhapDct();
                                        _xuatct.IDNhap = id;
                                        _xuatct.MaDV = a.MaDV;
                                        _xuatct.DonVi = a.DonVi;
                                        _xuatct.DonGia = a.DonGia;
                                        _xuatct.SoLo = a.SoLo;
                                        _xuatct.HanDung = a.HanDung;
                                        if (kieudon == 2 || kieudon == 4)
                                        {
                                            _xuatct.SoLuongN = a.SoLuong * (-1);
                                            _xuatct.ThanhTienN = Math.Round(dongia * soluong * (-1), 3);
                                        }
                                        else
                                        {
                                            _xuatct.SoLuongX = a.SoLuong;
                                            _xuatct.ThanhTienX = Math.Round(dongia * soluong, 3);
                                            _xuatct.SoLuongDY = DungChung.Ham._getSL_DongY(_dataContext, madv, soluong, makho);
                                            _xuatct.ThanhTienDY = Math.Round(DungChung.Ham._getSL_DongY(_dataContext, madv, soluong, makho) * dongia, 3);
                                        }
                                        _xuatct.MaBNhan = a.TrongBH;
                                        _xuatct.MaCC = a.MaCC;
                                        _xuatct.IDDTBN = Convert.ToByte(a.IDKB);
                                        _xuatct.TrongBH = a.IDCD ?? -1;
                                        _dataContext.NhapDcts.Add(_xuatct);
                                        listDichVus.Add(new DichVu { MaDV = _xuatct.MaDV ?? 0 });
                                    }

                                    if (_lDThuocct.Count > 0 && _dataContext.SaveChanges() >= 0)
                                    {
                                        success = true;
                                    }
                                    if (DungChung.Bien.MaBV == "200012" && listDichVus.Count > 0)
                                    {
                                        DungChung.Ham.UpdateTonDichVu(listDichVus, _xuat.MaKP ?? 0); // tét
                                    }
                                }
                                else
                                {
                                    foreach (var a in _ldthuoct_gc)
                                    {
                                        int madv = 0, makho = 0;
                                        string macc = "";
                                        double dongia = 0, soluong = 0;
                                        makho = Convert.ToInt32(lupTimMaKP.EditValue);
                                        madv = a.MaDV == null ? 0 : a.MaDV.Value;
                                        macc = a.MaCC;
                                        dongia = a.DonGia;
                                        soluong = a.SoLuong;

                                        NhapDct _xuatct = new NhapDct();
                                        _xuatct.IDNhap = id;
                                        _xuatct.MaDV = a.MaDV;
                                        _xuatct.DonVi = a.DonVi;
                                        _xuatct.SoLo = a.SoLo;
                                        _xuatct.HanDung = a.HanDung;
                                        if (DungChung.Bien.MaBV == "30340")
                                        {
                                            _xuatct.DonGiaX = a.DonGia;
                                            _xuatct.DonGia = DungChung.Ham._getGiaSD(_dataContext, a.MaDV ?? 0, a.DonGia, a.TrongBH, 1, makho, DateTime.Now);
                                        }
                                        else
                                            _xuatct.DonGia = a.DonGia;
                                        if (kieudon == 2 || kieudon == 4)
                                        {
                                            _xuatct.SoLuongN = a.SoLuong * (-1);
                                            _xuatct.ThanhTienN = Math.Round(dongia * soluong * (-1), 3);
                                            _xuatct.SoLuongDY = DungChung.Ham._getSL_DongY(_dataContext, madv, soluong, makho) * (-1);
                                            _xuatct.ThanhTienDY = Math.Round(DungChung.Ham._getSL_DongY(_dataContext, madv, soluong, makho) * dongia, 3) * (-1);
                                        }
                                        else
                                        {
                                            _xuatct.SoLuongX = a.SoLuong;
                                            _xuatct.ThanhTienX = Math.Round(dongia * soluong, 3);
                                            _xuatct.SoLuongDY = DungChung.Ham._getSL_DongY(_dataContext, madv, soluong, makho);
                                            _xuatct.ThanhTienDY = Math.Round(DungChung.Ham._getSL_DongY(_dataContext, madv, soluong, makho) * dongia, 3);
                                        }
                                        _xuatct.MaBNhan = a.TrongBH;
                                        _xuatct.MaCC = a.MaCC;
                                        _xuatct.TrongBH = a.IDCD ?? -1;
                                        _xuatct.IDDTBN = Convert.ToByte(a.IDKB);

                                        _dataContext.NhapDcts.Add(_xuatct);
                                        listDichVus.Add(new DichVu { MaDV = _xuatct.MaDV ?? 0 });

                                    }
                                    if (_lDThuocct.Count > 0 && _dataContext.SaveChanges() >= 0)
                                    {
                                        success = true;
                                    }
                                    if (DungChung.Bien.MaBV == "200012" && listDichVus.Count > 0)
                                    {
                                        DungChung.Ham.UpdateTonDichVu(listDichVus, _xuat.MaKP ?? 0); // tét
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                var xoand = _dataContext.NhapDs.Where(p => p.IDNhap == id).ToList();
                                foreach (var item in xoand)
                                {
                                    _dataContext.NhapDs.Remove(item);
                                    _dataContext.SaveChanges();
                                }
                                throw ex;
                            }
                        }
                    }

                    if (success)
                    {
                        var updateDT = (from b in _dataContext.DThuoccts
                                        where arrIDdon.Contains(b.IDDonct)
                                        select b).ToList();
                        foreach (var item in updateDT)
                        {
                            item.Status = 1;
                            item.IsXoaXuat = null;
                        }
                        var _lsopl = _dataContext.SoPLs.Where(p => p.PhanLoai == 1 && p.SoPL1 == DSPhieuLinh.SoPL).FirstOrDefault();
                        if (_lsopl != null)
                            _lsopl.Status = 2;
                        try
                        {
                            _dataContext.SaveChanges();
                        }
                        catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                        {
                            Exception raise = dbEx;
                            foreach (var validationErrors in dbEx.EntityValidationErrors)
                            {
                                foreach (var validationError in validationErrors.ValidationErrors)
                                {
                                    string message = string.Format("{0}:{1}",
                                      validationErrors.Entry.Entity.ToString(),
                                        validationError.ErrorMessage);
                                    raise = new InvalidOperationException(message, raise);
                                }

                            }
                            throw raise;
                        }
                    }
                }
            }
        }

        public class NhapDuocCT
        {
            //public NhapDuocCT(NhapDct data)
            //{
            //    LibraryStore.Mapper.DataObjectMapper.Map<NhapDuocCT>(this, data);
            //}
            public Nullable<int> IDNhap { get; set; }
            public Nullable<int> MaDV { get; set; }
            public string SoLo { get; set; }
            public string SoDangKy { get; set; }
            public Nullable<System.DateTime> HanDung { get; set; }
            public string DonVi { get; set; }
            public double DonGiaCT { get; set; }
            public double DonGia { get; set; }
            public int VAT { get; set; }
            public double SoLuongN { get; set; }
            public double ThanhTienN { get; set; }
            public double SoLuongX { get; set; }
            public double ThanhTienX { get; set; }
            public double SoLuongKK { get; set; }
            public double ThanhTienKK { get; set; }
            public double SoLuongSD { get; set; }
            public double ThanhTienSD { get; set; }
            public string MaCC { get; set; }
            public double DonGiaDY { get; set; }
            public double SoLuongDY { get; set; }
            public double ThanhTienDY { get; set; }
            public Nullable<int> MaBNhan { get; set; }
            public byte IDDTBN { get; set; }
            public double DonGiaX { get; set; }
            public string GhiChu { get; set; }
            public int TrongBH { get; set; }
            public int TyLeCK { get; set; }
            public Nullable<int> MienCT { get; set; }
        }

        public class NhapDuoc
        {
            public NhapDuoc(NhapD data)
            {
                LibraryStore.Mapper.DataObjectMapper.Map<NhapDuoc>(this, data);
            }
            public Nullable<System.DateTime> NgayNhap { get; set; }
            public string SoCT { get; set; }
            public Nullable<int> MaKP { get; set; }
            public string TenNguoiCC { get; set; }
            public string MaCC { get; set; }
            public string GhiChu { get; set; }
            public Nullable<int> Status { get; set; }
            public Nullable<int> PLoai { get; set; }
            public Nullable<int> MaKPnx { get; set; }
            public Nullable<int> KieuDon { get; set; }
            public Nullable<int> MaBNhan { get; set; }
            public string MaXP { get; set; }
            public Nullable<int> XuatTD { get; set; }
            public string MaCB { get; set; }
            public Nullable<int> SoPL { get; set; }
            public string DiaChi { get; set; }
            public int Mien { get; set; }
            public int TraDuoc_KieuDon { get; set; }
            public Nullable<System.DateTime> NgayTT { get; set; }
            public Nullable<System.DateTime> NgayNhap_NVL { get; set; }
            public Nullable<int> TangGiaSX { get; set; }
            public Nullable<int> IDSXThuoc { get; set; }
            public string SoPhieu { get; set; }
            public Nullable<int> LoaiTang { get; set; }
            public Nullable<bool> LienThongBanLe_B1 { get; set; }
            public Nullable<bool> LienThongBanLe_B2 { get; set; }
        }

        private void btnXuatDuoc_Click(object sender, EventArgs e)
        {
            var qds = _lDSPhieuLinh.Where(p => p.Chon == true).ToList();
            if (qds.Count > 0)
            {
                DialogResult _result;
                _result = MessageBox.Show("Xuất dược theo phiếu lĩnh số: " + string.Join(",", qds.Select(p => p.SoPL)) + " ?", "Xuất dược", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_result == DialogResult.Yes)
                {
                    foreach (DSPhieuLinh pl in qds)
                    {
                        if (pl.Chon)
                            XuatDuoc(pl);
                    }
                    TimKiem();
                    timkiemxd();
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn đơn để xuất");
            }

        }

        private void dtTimTuNgay_Leave(object sender, EventArgs e)
        {
            TimKiem();
            timkiemxd();
        }

        private void dtTimDenNgay_Leave(object sender, EventArgs e)
        {
            TimKiem();
            timkiemxd();
        }

        private void lupTimMaKP_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
            timkiemxd();
            int _makp = 0;
            if (lupTimMaKP.EditValue != null)
                _makp = Convert.ToInt32(lupTimMaKP.EditValue);
            List<DungChung.Ham.ThuocTon> _lthuocton = new List<DungChung.Ham.ThuocTon>();
            _lthuocton = DungChung.Ham.KiemTraTonDuoc(_dataContext, _makp);
            if (_lthuocton.Count > 0)
                txtTonDuoc.Text = "Có: " + _lthuocton.Count + " loại thuốc có số lượng <0";
            else
            {
                txtTonDuoc.Text = "";
            }
        }

        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            TimKiem();
            timkiemxd();
        }

        private void grvBenhNhankd_DataSourceChanged(object sender, EventArgs e)
        {
            grvBenhNhankd_FocusedRowChanged(null, null);
        }

        private void grvDonThuocct_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == STT)
            {
                e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        private void txtTimKiem_Click(object sender, EventArgs e)
        {
            if (lupTimKP.Text == "Tìm tên|Mã BN")
                lupTimKP.Text = "";
        }

        private void txtTimKiemxd_Leave(object sender, EventArgs e)
        {
            timkiemxd();
        }

        private void txtTimKiemxd_Click(object sender, EventArgs e)
        {
            if (lupTimKPhong.Text == "Tìm tên|Mã BN")
                lupTimKPhong.Text = "";
        }

        private void dtNgayXuat_Leave(object sender, EventArgs e)
        {
            _ngayke = dtNgayXuat.DateTime;
        }
        public void _getValue(bool a)
        {
            _ktmatkhau = a;
        }
        public bool _ktmatkhau = false;
        public void XoaXuat(int id, int[] arr_sopl)
        {
            var dthuocct = _dataContext.NhapDcts.Where(p => p.IDNhap == id).ToList();
            var nhapD = _dataContext.NhapDs.FirstOrDefault(p => p.IDNhap == id);
            var listDichVus = dthuocct.Select(o => new DichVu { MaDV = (o.MaDV ?? 0) }).ToList();
            //
            string cm = "delete from NhapDct where IDNhap = " + id;
            string kq = process_CommandSQL.ExecuteCommand(cm);
            if (!string.IsNullOrEmpty(kq))
            {
                MessageBox.Show("lỗi xóa NhapDct! " + kq);
                return;
            }
            cm = "delete from NhapD where IDNhap = " + id;
            kq = process_CommandSQL.ExecuteCommand(cm);
            if (!string.IsNullOrEmpty(kq))
            {
                MessageBox.Show("lỗi xóa NhapD! " + kq);
                return;
            }
            string inpara = "(";
            int dem = 0;
            foreach (var item in arr_sopl)
            {
                dem++;
                inpara += item + (dem >= arr_sopl.Length ? "" : ",");
            }
            inpara += ")";
            string cm2 = "update  DThuocct set DThuocct.Status =0,DThuocct.IsXoaXuat=1  From DThuoc inner join DThuocct on DThuoc.IDDon = DThuocct.IDDon" +
" where (DThuocct.Status <> -1) and (DThuocct.SoPL in " + inpara + ")";
            string kq2 = process_CommandSQL.ExecuteCommand(cm2);
            if (!string.IsNullOrEmpty(kq2))
            {
                MessageBox.Show("lỗi update DThuocct.Status =0 ! " + kq2);

            }

            var _lsopl = _dataContext.SoPLs.Where(p => p.PhanLoai == 1 && arr_sopl.Contains(p.SoPL1)).ToList();
            foreach (var item3 in _lsopl)
            {
                item3.Status = 1;
            }
            _dataContext.SaveChanges();
        }
        private void grvBNhanxd_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colXoaXuat")
            {
                int id = 0;
                if (grvBNhanxd.GetFocusedRowCellValue(colIDNhap) != null && grvBNhanxd.GetFocusedRowCellValue(colIDNhap).ToString() != "")
                {
                    id = Convert.ToInt32(grvBNhanxd.GetFocusedRowCellValue(colIDNhap));
                    int sopl = 0;
                    string soCT = "";
                    if (grvBNhanxd.GetFocusedRowCellValue(colSoCT) != null && grvBNhanxd.GetFocusedRowCellValue(colSoCT).ToString() != "")
                        soCT = grvBNhanxd.GetFocusedRowCellValue(colSoCT).ToString();
                    string[] dsSoPL = soCT.Split(';');
                    int[] arr_sopl = new int[dsSoPL.Count()];
                    for (int i = 0; i < dsSoPL.Count(); i++)
                    {
                        arr_sopl[i] = Convert.ToInt32(dsSoPL[i]);
                    }
                    var kt = _dataContext.NhapDs.Where(p => p.IDNhap == id).ToList();

                    if (kt.Count > 0 && kt.First().Status != 1)
                    {
                        if (DungChung.Bien.MaBV == "14017" || DungChung.Bien.MaBV == "24272")
                        {
                            _ktmatkhau = true;
                        }
                        else
                        {
                            ChucNang.frm_CheckPass frm = new ChucNang.frm_CheckPass();
                            frm.ok = new ChucNang.frm_CheckPass._getdata(_getValue);
                            frm.ShowDialog();
                        }
                        if (!_ktmatkhau)
                            return;
                        #region kiểm tra tồn dược đối với xuất trả dược
                        if (kt.First().PLoai == 1 && kt.First().KieuDon == 2)
                        {
                            var qndct = _dataContext.NhapDcts.Where(p => p.IDNhap == id).ToList();
                            foreach (var a in qndct)
                            {

                                double soluong = qndct.Where(p => p.MaDV == a.MaDV && p.DonGia == a.DonGia && p.DonVi == a.DonVi && p.SoLo == a.SoLo).Sum(p => p.SoLuongN);
                                double soluongton = DungChung.Ham._checkTon(_dataContext, a.MaDV ?? 0, kt.First().MaKP ?? 0, a.DonGia, soluong, a.SoLo);
                                if (soluong > soluongton)
                                {
                                    var qdv = _dataContext.DichVus.Where(p => p.MaDV == a.MaDV).FirstOrDefault();
                                    if (qdv != null)
                                    {
                                        MessageBox.Show("Thuốc, vật tư: " + qdv.TenDV + " có số lượng không đủ, bạn không thể xóa xuất");
                                    }
                                    else
                                        MessageBox.Show("Thuốc, vật tư: " + a.MaDV + " có số lượng không đủ, bạn không thể xóa xuất");
                                    return;
                                }
                            }
                        }

                        #endregion
                        DialogResult _result;
                        _result = MessageBox.Show("Bạn muốn xóa xuất dược có số phiếu lĩnh: " + soCT, "xóa chứng từ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (_result == DialogResult.Yes)
                        {
                            var dthuocct = _dataContext.NhapDcts.Where(p => p.IDNhap == id).ToList();
                            var nhapD = _dataContext.NhapDs.FirstOrDefault(p => p.IDNhap == id);
                            var listDichVus = dthuocct.Select(o => new DichVu { MaDV = (o.MaDV ?? 0) }).ToList();
                            //
                            string cm = "delete from NhapDct where IDNhap = " + id;
                            string kq = process_CommandSQL.ExecuteCommand(cm);
                            if (!string.IsNullOrEmpty(kq))
                            {
                                MessageBox.Show("lỗi xóa NhapDct! " + kq);
                                return;
                            }
                            cm = "delete from NhapD where IDNhap = " + id;
                            kq = process_CommandSQL.ExecuteCommand(cm);
                            if (!string.IsNullOrEmpty(kq))
                            {
                                MessageBox.Show("lỗi xóa NhapD! " + kq);
                                return;
                            }
                            if (listDichVus.Count > 0 && DungChung.Bien.MaBV == "200012") // tét
                            {
                                DungChung.Ham.UpdateTonDichVu(listDichVus, nhapD.MaKP ?? 0, true);
                            }
                            string inpara = "(";
                            int dem = 0;
                            foreach (var item in arr_sopl)
                            {
                                dem++;
                                inpara += item + (dem >= arr_sopl.Length ? "" : ",");
                            }
                            inpara += ")";
                            string cm2 = "update  DThuocct set DThuocct.Status =0,DThuocct.IsXoaXuat=1  From DThuoc inner join DThuocct on DThuoc.IDDon = DThuocct.IDDon" +
" where (DThuocct.Status <> -1) and (DThuocct.SoPL in " + inpara + ")";
                            string kq2 = process_CommandSQL.ExecuteCommand(cm2);
                            if (!string.IsNullOrEmpty(kq2))
                            {
                                MessageBox.Show("lỗi update DThuocct.Status =0 ! " + kq2);

                            }

                            var _lsopl = _dataContext.SoPLs.Where(p => p.PhanLoai == 1 && arr_sopl.Contains(p.SoPL1)).ToList();
                            foreach (var item3 in _lsopl)
                            {
                                item3.Status = 1;
                            }
                            _dataContext.SaveChanges();
                            TimKiem();
                            timkiemxd();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Chứng từ đã bị khóa, bạn không thể xóa!");
                    }
                }
                else
                {
                    MessageBox.Show("Không có chứng từ để xóa!");
                }
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void lupTimKP_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void txtSoPL_EditValueChanged(object sender, EventArgs e)
        {
            timKiem2();
        }

        private void grvBNhanxd_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                //if (true)
                //{
                _makpxd = Convert.ToInt32(grvBNhanxd.GetFocusedRowCellValue(colMaKPxd));
                string tenkp = grvBNhanxd.GetFocusedRowCellValue(colTenKPxd).ToString();
                var _makplinh = _dataContext.KPhongs.Where(p => p.TenKP == tenkp).Select(p => p.MaKP).FirstOrDefault();
                var _lDThuocct = new List<DThuocct>();
                int sopl = 0;
                int mabn = 0;
                if (grvBNhanxd.GetFocusedRowCellValue(colSoCT) != null && grvBNhanxd.GetFocusedRowCellValue(colSoCT).ToString() != "")
                {
                    groupControl1.Text = "Chi tiết phiếu lĩnh";
                    
                    sopl = Convert.ToInt32(grvBNhanxd.GetFocusedRowCellValue(colSoCT));
                    txtSoPLinh.Text = sopl.ToString();
                    var dsdon = (from kd in _dataContext.DThuocs.Where(p => p.PLDV == 1 && p.MaKXuat == _makpxd)
                                 join dtct in _dataContext.DThuoccts.Where(p => p.Status == 1 && (p.MaKP == _makplinh) && p.SoPL == sopl)
                                     on kd.IDDon equals dtct.IDDon
                                 select new c_dsThuoc { Dthuoc = kd, Dthuocct = dtct }).ToList();
                    if (DungChung.Bien.MaBV == "24272")
                    {
                        var mabnn = grvBNhanxd.GetFocusedRowCellValue(colMaBNhanxd);
                        mabn = mabnn == null ? 0 : Convert.ToInt32(mabnn);
                        dsdon = (from kd in _dataContext.DThuocs.Where(x => x.MaBNhan == mabn).Where(p => p.PLDV == 1 && p.MaKXuat == _makpxd)
                                 join dtct in _dataContext.DThuoccts.Where(p => p.Status == 1 && (p.MaKP == _makplinh) && p.SoPL == sopl)
                                     on kd.IDDon equals dtct.IDDon
                                 select new c_dsThuoc { Dthuoc = kd, Dthuocct = dtct }).ToList();
                    }
                    _lDThuocct = (from kd in dsdon
                                  join dv in _lDichVu on kd.Dthuocct.MaDV equals dv.MaDV
                                  group new { kd, dv } by new { MaBNhan = chk_BNhan.Checked ? (kd.Dthuoc.MaBNhan == null ? 0 : kd.Dthuoc.MaBNhan.Value) : 0, kd.Dthuocct.Status, kd.Dthuocct.DonGia, kd.Dthuocct.DonVi, dv.TenDV, dv.MaDV, kd.Dthuocct.SoLo, kd.Dthuocct.HanDung, dv.IDNhom } into kq
                                  select new { kq.Key.MaBNhan, kq.Key.TenDV, Status = kq.Key.Status == null ? 0 : kq.Key.Status.Value, MaDV = kq.Key.MaDV, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.kd.Dthuocct.SoLuong), ThanhTien = kq.Sum(p => p.kd.Dthuocct.ThanhTien), DonGia = kq.Key.DonGia == null ? 0 : kq.Key.DonGia, kq.Key.SoLo, kq.Key.HanDung, kq.Key.IDNhom }).ToList().Select(x => new DThuocct
                                  {
                                      TrongBH = x.MaBNhan,
                                      Status = x.Status,
                                      DuongD = x.TenDV,
                                      MaDV = x.MaDV,
                                      DonVi = x.DonVi,
                                      SoLuong = x.SoLuong,
                                      ThanhTien = x.ThanhTien,
                                      DonGia = x.DonGia,
                                      SoLo = x.SoLo,
                                      HanDung = x.HanDung,
                                      Loai = (x.IDNhom == 10 || x.IDNhom == 11) ? (byte)0 : (byte)1,
                                  }).ToList<DThuocct>();

                    List<DThuocct> _ldthuoct_gc = new List<DThuocct>();
                    if (DungChung.Bien.MaBV == "24272")
                    {
                        _ldthuoct_gc = (from a in _lDThuocct
                                        group a by new { a.SoLuong, a.ThanhTien, a.IDCD, a.IDKB, a.Status, a.DonGia, a.DonVi, a.DuongD, a.MaDV, a.SoLo, a.HanDung, a.TrongBH } into kq
                                        select new DThuocct
                                        {
                                            IDCD = kq.Key.IDCD,
                                            IDKB = kq.Key.IDKB,
                                            Status = kq.Key.Status,
                                            DuongD = kq.Key.DuongD,
                                            MaDV = kq.Key.MaDV,
                                            DonVi = kq.Key.DonVi,
                                            SoLo = kq.Key.SoLo,
                                            HanDung = kq.Key.HanDung,
                                            SoLuong = kq.Key.SoLuong,
                                            ThanhTien = kq.Key.ThanhTien,
                                            DonGia = kq.Key.DonGia,
                                            TrongBH = kq.Key.TrongBH,//mã bnhan
                                        }).ToList<DThuocct>();
                    }
                    else
                    {
                        _ldthuoct_gc = (from a in _lDThuocct
                                        group a by new { a.IDCD, a.IDKB, a.Status, a.DonGia, a.DonVi, a.DuongD, a.MaDV, a.SoLo, a.HanDung, a.TrongBH } into kq
                                        select new DThuocct
                                        {
                                            IDCD = kq.Key.IDCD,
                                            IDKB = kq.Key.IDKB,
                                            Status = kq.Key.Status,
                                            DuongD = kq.Key.DuongD,
                                            MaDV = kq.Key.MaDV,
                                            DonVi = kq.Key.DonVi,
                                            SoLo = kq.Key.SoLo,
                                            HanDung = kq.Key.HanDung,
                                            SoLuong = kq.Sum(p => p.SoLuong),
                                            ThanhTien = kq.Sum(p => p.ThanhTien),
                                            DonGia = kq.Key.DonGia,
                                            TrongBH = kq.Key.TrongBH,//mã bnhan
                                        }).ToList<DThuocct>();
                    }
                    List<DThuocct> lstdt = _ldthuoct_gc.OrderBy(p => p.DonVi).ThenBy(p => p.DuongD).ToList();
                    string tenkpxd = "";
                    if (grvBNhanxd.GetFocusedRowCellDisplayText(colTenKPxd) != null)
                    {
                        tenkpxd = grvBNhanxd.GetFocusedRowCellDisplayText(colTenKPxd);
                    }
                    if (_lDThuocct.Count > 0)
                    {
                        mabn = _lDThuocct.First().TrongBH;
                    }
                    

                    frm_XemXuatDuoc frm = new frm_XemXuatDuoc(lstdt, tenkpxd, mabn);
                    if (lupTimMaKP.Text == "Kho Đông y")
                    {
                        frm.TenBN24012.Visible = true;
                    }
                    else
                    {
                        frm.TenBN24012.Visible = false;
                    }
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi! " + ex.Message);
            }
        }

        private void btnHuyPL_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSoPLinh.Text))
            {
                DialogResult _resul = MessageBox.Show("Bạn muốn hủy phiếu lĩnh số: " + txtSoPLinh.Text, "Hủy phiếu lĩnh", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_resul == DialogResult.Yes)
                {
                    if (!string.IsNullOrEmpty(txtSoPLinh.Text))
                    {
                        int sopl = Convert.ToInt32(txtSoPLinh.Text);

                        var kt0 = (from dt in _dataContext.DThuocs
                                   join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == sopl) on dt.IDDon equals dtct.IDDon
                                   select new { dt.MaBNhan, dt.IDDon, dtct.Status, dtct.MaKP }).ToList();
                        if (kt0.Count > 0)
                        {
                            int makhoake = kt0.First().MaKP ?? 0;
                            var kt = _dataContext.DThuoccts.Where(p => p.SoPL == sopl).Select(p => p.Status).ToList();
                            if (kt.Count > 0)
                            {
                                bool ktrahuy = true;
                                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                                {
                                    string mess = "";
                                    List<int> _lmabn = kt0.Select(p => p.MaBNhan ?? 0).Distinct().ToList();
                                    foreach (var item in _lmabn)
                                    {
                                        var ktrarv = _dataContext.RaViens.Where(p => p.MaBNhan == item).ToList();
                                        if (ktrarv.Count > 0)
                                        {
                                            var tenbn = _dataContext.BenhNhans.Where(p => p.MaBNhan == item).FirstOrDefault();
                                            mess += tenbn.TenBNhan + ",\n";
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(mess))
                                    {
                                        ktrahuy = false;
                                        MessageBox.Show("Phiếu lĩnh có bệnh nhân:\n" + mess + "đã ra viện, không thể hủy!", "Thông báo", MessageBoxButtons.OK);
                                    }
                                }
                                if (kt.First().Value == 0)
                                {
                                    if (!ktrahuy)
                                    {
                                        var qtutruc = _dataContext.KPhongs.Where(p => p.MaKP == makhoake && p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc).FirstOrDefault();
                                        if (qtutruc != null)
                                        {
                                            FormNhap.frm_Check frm = new frm_Check(sopl, 9, makhoake, "");
                                            frm.ShowDialog();

                                        }
                                        else
                                        {
                                            FormNhap.frm_Check frm = new frm_Check(sopl, 5);
                                            frm.ShowDialog();
                                        }

                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Phiếu lĩnh đã được xuất dược, Bạn không thể hủy");
                                }
                            }
                        }
                    }
                    TimKiem();
                    timkiemxd();
                }
            }
        }

        private void chkHuydon_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHuydon.Checked)
                btnHuyPL.Enabled = false;
            else
                btnHuyPL.Enabled = true;
            TimKiem();
        }

        private void lupTimKPhong_EditValueChanged(object sender, EventArgs e)
        {
            timkiemxd();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            FormThamSo.Frm_TamTraThuoc frm = new FormThamSo.Frm_TamTraThuoc();
            frm.ShowDialog();
        }

        private void chkTuTruc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTuTruc.Checked)
            {
                labelKho.Text = "Kho xuất:";
                TimKiem();
            }
            else
            {
                labelKho.Text = "Kho xuất:";
                TimKiem();
            }
        }

        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            FormTraCuu.FrmTC_NhapXuatTon frm = new FormTraCuu.FrmTC_NhapXuatTon();
            frm.ShowDialog();
            int _makp = 0;
            if (lupTimMaKP.EditValue != null)
                _makp = Convert.ToInt32(lupTimMaKP.EditValue);
            List<DungChung.Ham.ThuocTon> _lthuocton = new List<DungChung.Ham.ThuocTon>();
            _lthuocton = DungChung.Ham.KiemTraTonDuoc(_dataContext, _makp);
            if (_lthuocton.Count > 0)
                txtTonDuoc.Text = "Có: " + _lthuocton.Count + " loại thuốc có số lượng <0";
            else
            {
                txtTonDuoc.Text = "";
            }
        }
        List<DungChung.Ham.ThuocTon> _lthuocton = new List<DungChung.Ham.ThuocTon>();
        private void txtTonDuoc_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            _lthuocton = DungChung.Ham.KiemTraTonDuoc(_dataContext, _makpxd);
            TraCuu.frm_DSTHuocAm frm = new TraCuu.frm_DSTHuocAm(_lthuocton);
            frm.ShowDialog();
        }

        private void chk_BNhan_CheckedChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng đang nâng cấp...");
            int _sopl = 0;
            if (!string.IsNullOrEmpty(txtSoPLinh.Text))
                _sopl = Convert.ToInt32(txtSoPLinh.Text);
            FormNhap.frm_UpdateDonGia frm = new frm_UpdateDonGia(_sopl);
            frm.ShowDialog();
        }

        private void grvBenhNhankd_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

        }

        private void cboKieuDon_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            timKiem2();
            timkiemxd();
        }

        private void grvBenhNhankd_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                if (e.HitInfo.InRow)
                {
                    grvBenhNhankd.FocusedRowHandle = e.HitInfo.RowHandle;
                    GridView view = sender as GridView;
                    contextMenuStrip1.Show(view.GridControl, e.Point);
                }
            }
            catch (Exception ex)
            {
                DungChung.WriteLog.Warn(ex);
            }
        }

        private void hủyPhiếuLĩnhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var row = (DSPhieuLinh)grvBenhNhankd.GetFocusedRow();
            if (row != null)
            {
                QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var dThuocct = dataContext.DThuoccts.Where(o => o.SoPL == row.SoPL).ToList();
                if (dThuocct.Count > 0)
                {
                    foreach (var item in dThuocct)
                    {
                        var dtct = dataContext.DThuoccts.FirstOrDefault(o => o.IDDonct == item.IDDonct);
                        if (dtct != null)
                        {
                            dtct.Status = 2;
                            dataContext.SaveChanges();
                        }
                    }
                    MessageBox.Show("Hủy thành công!");
                    TimKiem();
                }
                else
                {
                    MessageBox.Show("Không có thuốc/vật tư trong phiếu lĩnh");
                }
            }
        }

        private void grvBenhNhankd_RowStyle(object sender, RowStyleEventArgs e)
        {
            var row = (DSPhieuLinh)grvBenhNhankd.GetRow(e.RowHandle);
            if (row != null && row.IsXoaXuat)
            {
                e.Appearance.ForeColor = Color.Orange;
            }
        }

        public class Params
        {
            public int IdXoa { get; set; }
            int[] arr_sopl;
            public int[] ArrSoPL { get; set; }
        }

        private void btnXoaXuat_Click(object sender, EventArgs e)
        {
            List<Params> lstParam = new List<Params>();
            int[] arr_sopl = new int[0];
            int id = 0;
            string soCT = "";
            string loiSLKdu = "";
            string xacNhan = "";
            string loiKhoaCT = "";
            string loiKCoCT = "";

            int[] index = grvBNhanxd.GetSelectedRows();
            for (int j = 0; j < index.Count(); j++)
            {
                //add params
                if (grvBNhanxd.GetRowCellValue(index[j], colIDNhap) != null && grvBNhanxd.GetRowCellValue(index[j], colIDNhap).ToString() != "")
                    id = Convert.ToInt32(grvBNhanxd.GetRowCellValue(index[j], colIDNhap));
                if (grvBNhanxd.GetRowCellValue(index[j], colSoCT) != null && grvBNhanxd.GetRowCellValue(index[j], colSoCT).ToString() != "")
                    soCT = grvBNhanxd.GetRowCellValue(index[j], colSoCT).ToString();
                string[] dsSoPL = soCT.Split(';');
                arr_sopl = new int[dsSoPL.Count()];
                for (int i = 0; i < dsSoPL.Count(); i++)
                {
                    arr_sopl[i] = Convert.ToInt32(dsSoPL[i]);
                }

                Params param = new Params();
                param.IdXoa = id;
                param.ArrSoPL = arr_sopl;

                lstParam.Add(param);

                //check data
                if (grvBNhanxd.GetRowCellValue(index[j], colIDNhap) != null && grvBNhanxd.GetRowCellValue(index[j], colIDNhap).ToString() != "")
                {
                    var kt = _dataContext.NhapDs.Where(p => p.IDNhap == id).ToList();

                    if (kt.Count > 0 && kt.First().Status != 1)
                    {
                        #region kiểm tra tồn dược đối với xuất trả dược
                        if (kt.First().PLoai == 1 && kt.First().KieuDon == 2)
                        {
                            var qndct = _dataContext.NhapDcts.Where(p => p.IDNhap == id).ToList();
                            foreach (var a in qndct)
                            {

                                double soluong = qndct.Where(p => p.MaDV == a.MaDV && p.DonGia == a.DonGia && p.DonVi == a.DonVi && p.SoLo == a.SoLo).Sum(p => p.SoLuongN);
                                double soluongton = DungChung.Ham._checkTon(_dataContext, a.MaDV ?? 0, kt.First().MaKP ?? 0, a.DonGia, soluong, a.SoLo);
                                if (soluong > soluongton)
                                {
                                    var qdv = _dataContext.DichVus.Where(p => p.MaDV == a.MaDV).FirstOrDefault();
                                    if (!loiSLKdu.Contains(qdv.TenDV))
                                    {
                                        if (qdv != null)
                                        {
                                            loiSLKdu += qdv.TenDV + " ";
                                        }
                                        else
                                            loiSLKdu += qdv.MaDV + " ";
                                        return;
                                    }
                                }
                            }
                        }

                        #endregion
                        xacNhan += soCT + " ";
                    }
                    else
                    {
                        loiKhoaCT += soCT + " ";
                    }
                }
            }

            if (DungChung.Bien.MaBV == "14017")
            {
                _ktmatkhau = true;
            }
            else
            {
                ChucNang.frm_CheckPass frm = new ChucNang.frm_CheckPass();
                frm.ok = new ChucNang.frm_CheckPass._getdata(_getValue);
                frm.ShowDialog();
            }
            if (!_ktmatkhau)
                return;
            if (!string.IsNullOrEmpty(loiSLKdu))
            {
                MessageBox.Show("Các Thuốc, vật tư: " + loiSLKdu + " có số lượng không đủ, bạn không thể xóa xuất");
                return;
            }
            if (!string.IsNullOrEmpty(loiKhoaCT))
            {
                MessageBox.Show("Chứng từ " + loiKhoaCT + "đã bị khóa, bạn không thể xóa!");
                return;
            }

            DialogResult _result;
            _result = MessageBox.Show("Bạn muốn xóa các xuất dược có số phiếu lĩnh: " + xacNhan, "xóa chứng từ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (_result == DialogResult.Yes)
            {
                foreach (var x in lstParam)
                {
                    XoaXuat(x.IdXoa, x.ArrSoPL);
                }
                TimKiem();
                timkiemxd();
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if(grvBenhNhankd.RowCount > 0)
            {
                for (int i = 0; i < grvBenhNhankd.RowCount; i++)
                {
                    grvBenhNhankd.SetRowCellValue(i, colChon, chkSelectAll.Checked);
                }
            }   
        }
    }
}
