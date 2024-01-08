using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QLBV.DungChung;


namespace QLBV.FormNhap
{
    public partial class frm_kedon : DevExpress.XtraEditors.XtraForm
    {

        int _mabn = 0, _idcd = 0, _makp = 0;
        bool _kebosung = false, _bosungNgtru = false;
        string _tenPhieu = "";
        int ppxuat = -1;
        int isDonMoi;

        public frm_kedon(int mabn, int idcd, int makp, bool donbs)
        {
            InitializeComponent();
            _mabn = mabn;
            _idcd = idcd;
            _makp = makp;
            _bosungNgtru = donbs;
            _benhnhan = _Data.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mabn"></param>
        /// <param name="idcd"></param>
        /// <param name="makp"></param>
        /// <param name="kebosung">kê bổ sung: true: kê đơn thuốc, vật tư độc lập, không theo gói hoặc đính kèm dịch vụ nào </param>
        public frm_kedon(int mabn, int idcd, int makp, bool kebosung, string tenPhieu)
        {
            InitializeComponent();
            _mabn = mabn;
            _idcd = idcd;
            _makp = makp;
            _kebosung = kebosung;
            _benhnhan = _Data.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
            _tenPhieu = tenPhieu;
        }

        class trongDM
        {
            public int TrongDM { set; get; }
            public string Ten { set; get; }
        }
        void anhien(bool ah)
        {
            lup_khoake.ReadOnly = ah;
            lup_khoxuat.ReadOnly = ah;
            lup_bske.ReadOnly = ah;
            lup_ngayke.ReadOnly = ah;
            btn_sua.Enabled = ah;
            btn_delete.Enabled = ah;
            btn_luu.Enabled = !ah;
            btn_kluu.Enabled = !ah;
            btnThem.Enabled = ah;
        }


        private bool ktranhapvtyt()
        {
            if (string.IsNullOrEmpty(lup_bske.Text))
            {
                MessageBox.Show("Bạn chưa nhập BS.Kê");
                lup_bske.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(lup_khoake.Text))
            {
                MessageBox.Show("Bạn chưa nhập khoa kê");
                lup_khoake.Focus();
                return false;
            }
            if (rbtn_kieuke.SelectedIndex == 1 || rbtn_kieuke.SelectedIndex == 3)
            {
                int makhoa = Convert.ToInt32(lup_khoake.EditValue);

                var bnkb = _Data.BNKBs.Where(p => p.MaBNhan == _mabn && p.MaKP == makhoa).FirstOrDefault();
                if (bnkb == null)
                {
                    MessageBox.Show("Bệnh nhân chưa có chẩn đoán tại: " + lup_khoake.Text);
                    return false;
                }
                if (string.IsNullOrEmpty(lup_khoxuat.Text))
                {
                    MessageBox.Show("Bạn chưa nhập kho xuất");
                    lup_khoxuat.Focus();
                    return false;
                }
            }
            var dataSource = ((List<Dtct>)bindingSource1.DataSource).Where(o => o.SoLuong == 0).ToList();
            if (dataSource.Count > 0)
            {
                MessageBox.Show(string.Format("Thuốc/vật tư có mã: {0} số lượng phải lớn hơn 0", string.Join(", ", dataSource.Select(o => o.MaDV))));
                return false;
            }
            return true;
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<DichVuTheoKhoXuat> _ldv = new List<DichVuTheoKhoXuat>();
        List<DichVu> _lthuocVT = new List<DichVu>();// tất cả thuốc vật tư
        List<KPhong> _lkho = new List<KPhong>();
        List<KPhong> _lkp = new List<KPhong>();
        List<CanBo> _lcb = new List<CanBo>();
        RaVien _rv = new RaVien();
        int _makho = 0;
        string _macb = "";
        int iddonct = -10;
        int iddonkem = -1;
        int selectedIdDon = 0;
        BenhNhan _benhnhan;
        List<Dtct> DsDonThuoc = new List<Dtct>();
        List<DThuocct> _ldt1 = new List<DThuocct>();
        private class Dtct
        {
            public int? MaDV { get; set; }
            public double SoLuong { get; set; }
            public string DonVi { get; set; }
            public double DonGia { get; set; }
            public int TrongBH { get; set; }
            public string DuongD { get; set; }
            public string SoLan { get; set; }
            public string MoiLan { get; set; }
            public string Luong { get; set; }
            public string DviUong { get; set; }
            public string GhiChu { get; set; }
            public double TyLeTT { get; set; }
            public double ThanhTien { get; set; }
            public int IDDonct { get; set; }
            public string SoLo { get; set; }
            public DateTime? HanDung { get; set; }
            public int ThanhToan { get; set; }
        }


        public void frm_kedon_Load(object sender, EventArgs e)
        {
            grcDonThuocdt.Enabled = true;
            isDonMoi = 0;

            var _kdngoai = _Data.HThong_User.Where(p => p.TenDN == DungChung.Bien.TenDN).ToList();
            if (_kdngoai.Count() > 0)
            {
                lup_khoxuat.EditValue = _kdngoai.First().MaKhoKDNgoai;
            }
            var lstDthuoc = (from a in _Data.DThuocs
                             join b in _Data.KPhongs on a.MaKXuat equals b.MaKP
                              where a.PLDV == 1 && (a.KieuDon == 1 || a.KieuDon == 6 || a.KieuDon == 7)
                                    && a.MaBNhan == _mabn
                              select new { a.NgayKe, a.IDDon, a.MaKXuat, b.TenKP }).ToList();
            grcDonThuocdt.DataSource = lstDthuoc.OrderBy(p => p.IDDon);

            if (lup_khoxuat.EditValue != null)
            { 
                int? makho = Convert.ToInt32(lup_khoxuat.EditValue);
                var getppxuat = _lkho.Where(p => p.MaKP == makho).Select(p => p.PPXuat).FirstOrDefault();
                if (getppxuat != null)
                    ppxuat = getppxuat.Value;
            }

            connect = Program._connect;
            statusLuu = 0;
            iddonkem = 0;
            _lthuocVT = _Data.DichVus.Where(p => p.PLoai == 1).ToList();
            //if (DungChung.Bien.CapDo == 9)
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
                lup_khoake.Properties.ReadOnly = false;
            else
                lup_khoake.Properties.ReadOnly = true;
            if (_makp > 0)
                lup_khoake.EditValue = _makp;
            else
                lup_khoake.EditValue = DungChung.Bien.MaKP;
            List<trongDM> _trong = new List<trongDM>();
            _trong.Add(new trongDM { TrongDM = 0, Ten = "Ngoài DMBH" });
            _trong.Add(new trongDM { TrongDM = 1, Ten = "Trong DMBH" });
            _trong.Add(new trongDM { TrongDM = 2, Ten = "Không TT" });
            lup_trongDMBH.DataSource = _trong;
            if (_idcd == -1)
            {
                if (_bosungNgtru)
                {
                    if(DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24297")
                    {
                        this.rbtn_kieuke.Properties.Items[1].Enabled = false;
                    }
                    rbtn_kieuke.SelectedIndex = 3;
                }
                else
                    rbtn_kieuke.SelectedIndex = 1;
            }
            else
                rbtn_kieuke.SelectedIndex = 0;
            _lkp = _Data.KPhongs.ToList();
            
            DThuoc dthuockem = new DThuoc();
            //lấy iddtct của cls đc chỉ định
            _rv = _Data.RaViens.Where(p => p.MaBNhan == _mabn).FirstOrDefault();

            if (rbtn_kieuke.SelectedIndex == 0)// dùng khi kê VTYT đính kèm
            {
                //btnin.Enabled = false;
                if (!_kebosung)
                {
                    var dsid = (from dt in _Data.DThuocs.Where(p => p.MaBNhan == _mabn)
                                join dtct in _Data.DThuoccts.Where(p => p.IDCD == _idcd) on dt.IDDon equals dtct.IDDon
                                select new { dtct.IDDonct, dtct.NgayNhap, dtct.MaKP }).FirstOrDefault();
                    if (dsid != null)
                    {
                        lup_ngayke.Properties.ReadOnly = true;
                        iddonct = dsid.IDDonct;
                        anhien(false);
                        _ldt1 = _Data.DThuoccts.Where(p => p.AttachIDDonct == iddonct).ToList();
                        if (_ldt1.Count > 0)
                            iddonkem = _ldt1.First().IDDon ?? 0;
                        dthuockem = _Data.DThuocs.Where(p => p.IDDon == iddonkem).FirstOrDefault();
                        if (dsid.NgayNhap != null)
                            lup_ngayke.DateTime = dsid.NgayNhap.Value;
                        // lup_khoake.EditValue = dsid.MaKP;
                    }

                    else
                    {
                        if (_rv == null)
                        {
                            MessageBox.Show("Dịch vụ chưa được thực hiện, bạn không thể thêm VTYT");
                        }

                        anhien(true);

                    }
                }
                else
                {
                    var dsid = (from dt in _Data.DThuocs.Where(p => p.MaBNhan == _mabn)
                                join dtct in _Data.DThuoccts.Where(p => p.MaKP == _makp && (p.AttachIDDonct == null || p.AttachIDDonct == 0)) on dt.IDDon equals dtct.IDDon
                                select new { dtct.IDDonct, dt.IDDon, dtct.NgayNhap, dtct.MaKP }).FirstOrDefault();
                    if (dsid != null)
                    {
                        lup_ngayke.Properties.ReadOnly = true;
                        anhien(false);
                        _ldt1 = _Data.DThuoccts.Where(p => p.IDDon == dsid.IDDon).ToList();
                        iddonkem = dsid.IDDon;

                        if (dsid.NgayNhap != null)
                            lup_ngayke.DateTime = dsid.NgayNhap.Value;
                    }
                    dthuockem = _Data.DThuocs.Where(p => p.IDDon == iddonkem).FirstOrDefault();
                }
            }
            else if (rbtn_kieuke.SelectedIndex == 1)
            { // kê đơn dịch vụ
                dthuockem = _Data.DThuocs.Where(p => p.MaBNhan == _mabn && p.KieuDon == 6).FirstOrDefault();
                if (dthuockem != null)
                    iddonkem = dthuockem.IDDon;
                _ldt1 = _Data.DThuoccts.Where(p => p.IDDon == iddonkem).ToList();
            }
            else if (rbtn_kieuke.SelectedIndex == 3)
            { // kê đơn dịch vụ
                dthuockem = _Data.DThuocs.Where(p => p.MaBNhan == _mabn && p.KieuDon == 1).FirstOrDefault();
                if (dthuockem != null)
                    iddonkem = dthuockem.IDDon;
                _ldt1 = _Data.DThuoccts.Where(p => p.IDDon == iddonkem).ToList();
            }
            else
            {
                dthuockem = _Data.DThuocs.Where(p => p.MaBNhan == _mabn && p.KieuDon == -1).FirstOrDefault();
                if (dthuockem != null)
                    iddonkem = dthuockem.IDDon;
                _ldt1 = _Data.DThuoccts.Where(p => p.IDDon == iddonkem).ToList();
            }

            
            _ldt1 = _Data.DThuoccts.Where(p => p.IDDon == selectedIdDon).ToList();

            if (dthuockem != null)
            {
                anhien(true);
                lup_khoake.EditValue = dthuockem.MaKP;
                lup_bske.EditValue = dthuockem.MaCB;
                lup_khoxuat.EditValue = dthuockem.MaKXuat;
                if (dthuockem.NgayKe != null)
                    lup_ngayke.DateTime = dthuockem.NgayKe.Value;
            }
            else
            {
                anhien(false);
                lup_ngayke.DateTime = System.DateTime.Now;
            }
            DsDonThuoc = (from dt in _ldt1
                          join dv in _Data.DichVus.Where(p => p.PLoai == 1) on dt.MaDV equals dv.MaDV
                          select new Dtct
                          {
                              MaDV = dt.MaDV,
                              SoLuong = dt.SoLuong,
                              DonVi = dt.DonVi,
                              DonGia = dt.DonGia,
                              TrongBH = dt.TrongBH,
                              DuongD = dt.DuongD,
                              SoLan = dt.SoLan,
                              MoiLan = dt.MoiLan,
                              Luong = dt.Luong,
                              DviUong = dt.DviUong,
                              GhiChu = dt.GhiChu,
                              TyLeTT = dt.TyLeTT,
                              ThanhTien = dt.ThanhTien,
                              IDDonct = dt.IDDonct,
                              SoLo = dt.SoLo,
                              HanDung = dt.HanDung,
                              ThanhToan = dt.ThanhToan,
                          }).ToList();


            bindingSource1.DataSource = DsDonThuoc;
            grCPdinhkem.DataSource = bindingSource1;

            lup_khoake.Properties.DataSource = _lkp.Where(p => p.PLoai == "Phòng khám" || p.PLoai == "Cận lâm sàng" || p.PLoai == "Lâm sàng").ToList();

            if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24297")
            {
                string MKP = _makp.ToString();
                lup_khoxuat.Properties.DataSource = (_lkp.Where(p => (p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc || (p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc && (p.MaKPsd != null ? p.MaKPsd.Contains(MKP) : false))) && p.Status == 1 && p.MaBVsd == DungChung.Bien.MaBV)).ToList();
            }

            else
            {
                if (rbtn_kieuke.SelectedIndex == 0)
                {
                    lup_khoxuat.Properties.DataSource = _lkp.Where(p => (p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc && p.NhomKP == _makp)).Where(p => p.MaBVsd == DungChung.Bien.MaBV && p.Status == 1).ToList();
                }
                else
                    lup_khoxuat.Properties.DataSource = _lkp.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc).Where(p => p.MaBVsd == DungChung.Bien.MaBV && p.Status == 1).ToList();
            }
            string _makpsd = ";" + _makp + ";";
            if (DungChung.Bien.MaBV != "27022" && DungChung.Bien.MaBV != "24012" && DungChung.Bien.MaBV != "24272")
            {
                if (DungChung.Bien.MaBV == "30372")
                {
                    lup_bske.Properties.DataSource = _lcb.Where(p => p.MaKP == _makp || p.MaKPsd.Contains(_makpsd)).ToList();
                }
                else
                {
                    lup_bske.Properties.DataSource = _lcb.Where(p => p.MaKP == _makp).ToList();
                }
            }
            else
            {
                lup_bske.Properties.DataSource = _lcb.Where(p => p.Status == 1).Where(p => p.MaKPsd.Contains(_makpsd)).Where(p => p.CapBac != null).Where(p => p.CapBac.ToLower().Contains("bs") || p.CapBac.ToLower().Contains("bác sĩ") || p.CapBac.ToLower().Contains("bác sỹ") || p.CapBac.ToLower().Contains("ys") || p.CapBac.ToLower().Contains("y sĩ") || p.CapBac.ToLower().Contains("y sỹ") || p.CapBac.ToLower().Contains("giáo sư") || p.CapBac.ToLower().Contains("gs") || p.CapBac.ToLower().Contains("tiến sĩ") || p.CapBac.ToLower().Contains("ts")).ToList();
            }


            //rbtn_kieuke.Properties.ReadOnly = true;
            if (_rv != null)
            {
                MessageBox.Show("bệnh nhân đã ra viện!");
                anhien(true);
            }
            lup_khoxuat_EditValueChanged(sender, e);
            txtIDDon.Text = iddonkem.ToString();

            if(DungChung.Bien.MaBV == "24297")
            {
                col_bhyt.OptionsColumn.AllowEdit = false;
            }

        }
        //string[] dsgia(int madv)
        //{
        //    QLBV_Database.QLBVEntities _db=new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        //    var q = (from nd in _db.NhapDs.Where(p => p.PLoai == 1)
        //             join ndct in _db.NhapDcts.Where(p => p.MaDV == madv) on nd.IDNhap equals ndct.IDNhap
        //             orderby nd.NgayNhap
        //             select ndct.DonGia).ToList();


        //}
        private ConnectData connect;
        List<DichVuTheoKhoXuat> _ldvKD(int makp, int makho)
        {
            connect = Program._connect;
            //QLBV_Database.QLBVEntities _db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //List<DichVu> qsoluong = (from dt in _db.DThuocs.Where(p => p.KieuDon == 3 || p.KieuDon == 4 || p.KieuDon == 7).Where(p => p.MaKP == makp && p.MaKXuat == makho)
            //                         join dtct in _db.DThuoccts.Where(p => p.Status == 1) on dt.IDDon equals dtct.IDDon
            //                         join dv in _db.DichVus on dtct.MaDV equals dv.MaDV
            //                         select dv).ToList().Distinct().ToList();
            //return qsoluong;
            List<DichVuTheoKhoXuat> list = new List<DichVuTheoKhoXuat>();
            if (connect.isConnect)
            {
                string strSQL = "sp_KB_DichVuTheoKhoXuat";
                string[] strpara = new string[] { "@ppTinhTon", "@MaKP", "@MaKCB", "@phanloaiTuTruc" };
                object[] oValue = new object[] { DungChung.Bien.PPTinhTon, makho, DungChung.Bien.MaBV, DungChung.Bien.st_PhanLoaiKP.TuTruc };
                SqlDbType[] sqlDBType = new SqlDbType[] { SqlDbType.Int, SqlDbType.Int, SqlDbType.VarChar, SqlDbType.NVarChar };
                DataTable dtTble = connect.FillDatatable(strSQL, CommandType.StoredProcedure, strpara, oValue, sqlDBType);
                for (int i = 0; i < dtTble.Rows.Count; i++)
                {
                    DichVuTheoKhoXuat objDV = new DichVuTheoKhoXuat();
                    objDV.MaDV = Convert.ToInt32(dtTble.Rows[i]["MaDV"].ToString());
                    objDV.TenDV = dtTble.Rows[i]["TenDV"].ToString();
                    objDV.HamLuong = dtTble.Rows[i]["HamLuong"].ToString();
                    if(DungChung.Bien.MaBV == "24012")
                    {
                        objDV.SoLo = dtTble.Rows[i]["SoLo"].ToString();
                        if (dtTble.Rows[i]["HanDung"] != null && dtTble.Rows[i]["HanDung"].ToString() != "")
                        {
                            objDV.HanDung = Convert.ToDateTime(dtTble.Rows[i]["HanDung"].ToString());
                        }
                    }
                    
                    list.Add(objDV);
                }
            }
            return list;
        }
        double getGiaKD(int madv, int makp, int makho)
        {
            QLBV_Database.QLBVEntities _db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var qtt = _db.KPhongs.Where(p => p.MaKP == makho).Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc).ToList();
            if (qtt.Count > 0)
            {
                var q = (from dt in _db.DThuocs.Where(p => p.KieuDon == 3 || p.KieuDon == 4 || p.KieuDon == 7).Where(p => p.MaKP == makho)
                         join dtct in _db.DThuoccts.Where(p => p.MaDV == madv).Where(p => p.Status == 1) on dt.IDDon equals dtct.IDDon
                         where dt.KieuDon != null && dtct.DonGia != null && dtct.SoLuong != null && dtct.Status != null
                         select new { dt.NgayKe, dt.KieuDon, dtct.DonGia, dtct.SoLuong, dtct.Status }).ToList();
                var qsoluong = (from dt in q
                                orderby dt.NgayKe
                                group new { dt } by new { dt.DonGia } into kq
                                select new
                                {
                                    DonGia = kq.Key.DonGia,
                                    SoLuongT = kq.Where(p => p.dt.KieuDon == 3 || p.dt.KieuDon == 4).Where(p => p.dt.Status == 1).Sum(p => p.dt.SoLuong)
                                   - kq.Where(p => p.dt.KieuDon == 7).Sum(p => p.dt.SoLuong),
                                }).ToList();

                foreach (var item in qsoluong)
                {
                    if (item.SoLuongT > 0)
                    {

                        DungChung.Bien.SoLuongTon = item.SoLuongT;
                        return item.DonGia;
                    }
                }
                DungChung.Bien.SoLuongTon = 0;
                return 0;
            }
            else
            {
                var q = (from dt in _db.DThuocs.Where(p => p.KieuDon == 3 || p.KieuDon == 4 || p.KieuDon == 7).Where(p => p.MaKP == makp && p.MaKXuat == makho)
                         join dtct in _db.DThuoccts.Where(p => p.MaDV == madv) on dt.IDDon equals dtct.IDDon
                         where dt.KieuDon != null && dtct.DonGia != null && dtct.SoLuong != null && dtct.Status != null
                         select new { dt.NgayKe, dt.KieuDon, dtct.DonGia, dtct.SoLuong, dtct.Status }).ToList();
                var qsoluong = (from dt in q
                                orderby dt.NgayKe
                                group new { dt } by new { dt.DonGia } into kq
                                select new
                                {
                                    DonGia = kq.Key.DonGia,
                                    SoLuongT = kq.Where(p => p.dt.KieuDon == 3 || p.dt.KieuDon == 4).Where(p => p.dt.Status == 1).Sum(p => p.dt.SoLuong)
                                   - kq.Where(p => p.dt.KieuDon == 7).Sum(p => p.dt.SoLuong),
                                }).ToList();

                foreach (var item in qsoluong)
                {
                    if (item.SoLuongT > 0)
                    {

                        DungChung.Bien.SoLuongTon = item.SoLuongT;
                        return item.DonGia;
                    }
                }
                DungChung.Bien.SoLuongTon = 0;
                return 0;
            }
        }
        internal static double _getGiaSD(QLBV_Database.QLBVEntities data, int madv, double dongiaSD, int trongBH, int nhapxuat, int maKP)
        {
            double rs = 0;
            if (nhapxuat == 1)
            {

                rs = dongiaSD;
                var qdongia = data.DonGiaDVs.Where(p => p.MaDV == madv && p.Status == true).Where(p => (trongBH == 1 && p.DonGiaX_BH == dongiaSD) || (trongBH == 0 && p.DonGiaX_DV == dongiaSD)).Select(p => p.DonGiaN).FirstOrDefault();
                if (qdongia != null)
                {
                    rs = qdongia;
                }

            }
            else // lấy giá xuất
            {
                var qdongia = data.DonGiaDVs.Where(p => p.MaDV == madv && p.Status == true).Select(p => new { DonGiaX = trongBH == 1 ? p.DonGiaX_BH : p.DonGiaX_DV, p.DonGiaN }).FirstOrDefault();
                if (qdongia != null)
                {
                    rs = qdongia.DonGiaX;

                    var qnd = (from nhap in data.NhapDs.Where(p => p.MaKP == maKP).OrderByDescending(p => p.NgayNhap)
                               join nhapct in data.NhapDcts.Where(p => p.MaDV == madv && p.DonGia == qdongia.DonGiaN) on nhap.IDNhap equals nhapct.IDNhap
                               group new { nhapct, nhap } by new { nhapct.MaDV, nhapct.DonGia } into kq
                               select new { kq.Key.DonGia, SoLuong = (kq.Sum(p => p.nhapct.SoLuongN) - kq.Sum(p => p.nhapct.SoLuongX)) }).FirstOrDefault();
                    var q = (from dt in data.DThuocs.Where(p => p.KieuDon == 6).Where(p => p.MaKXuat == maKP)
                             join dtct in data.DThuoccts.Where(p => p.MaDV == madv && p.DonGia == rs && p.Status == 0) on dt.IDDon equals dtct.IDDon

                             select new { dtct.SoLuong }).ToList().Sum(p => p.SoLuong);
                    double soluongton = 0;
                    if (qnd != null)
                        soluongton = qnd.SoLuong - q;

                    DungChung.Bien.SoLuongTon = soluongton;
                }
            }
            return rs;


        }

        private string _getDDung(int madv)
        {
            //try
            //{
            //    if (connect.isConnect)
            //    {
            //        string strSQL = "SELECT DuongD FROM dbo.DichVu WHERE MaDV = '" + madv + "'";

            //        DataTable dtTble = connect.FillDatatable(strSQL, CommandType.Text);
            //        if (dtTble.Rows.Count > 0)
            //        {
            //            if (!string.IsNullOrEmpty(dtTble.Rows[0]["DuongD"].ToString()))
            //                return dtTble.Rows[0]["DuongD"].ToString() + " ngày ";
            //            else
            //                return "";
            //        }
            //        else
            //            return "";
            //    }
            //    else
            //    {
            //        MessageBox.Show("Lỗi kết nối CSDL");
            //        return "";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Lỗi kết nối CSDL");
            //    return "";
            //}
            try
            {
                QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                string dd = "";
                var ddung = dataContext.DichVus.FirstOrDefault(p => p.MaDV == madv);
                if (ddung != null)
                {
                    dd = ddung.DuongD + " ngày ";
                }
                return dd;
            }
            catch (Exception)
            {
                MessageBox.Show("Thuốc chưa có đường dùng");
                return "";
            }
        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            statusLuu = 1;
            int madv = 0;
            int makho = 0;
            int _mien = 0;
            if (lup_khoxuat.EditValue != null)
                makho = Convert.ToInt32(lup_khoxuat.EditValue);
            int makp = 0;
            if (lup_khoake.EditValue != null)
                makp = Convert.ToInt32(lup_khoake.EditValue);
            List<DungChung.Ham.giaSoLoHSD> dsgia = new List<QLBV.DungChung.Ham.giaSoLoHSD>();
            List<DungChung.Ham.giaSoLoHSD> dsgiaHT = new List<QLBV.DungChung.Ham.giaSoLoHSD>();
            QLBV_Database.QLBVEntities _db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (e.Column.Name == "col_MaDV")
            {
                if (gridView2.GetFocusedRowCellValue(col_MaDV) != null)
                    madv = Convert.ToInt32(gridView2.GetFocusedRowCellValue(col_MaDV));
                double soluongtong = 0;
                soluongtong = ((List<Dtct>)bindingSource1.DataSource).Where(o => o.MaDV == madv).Sum(o => o.SoLuong);
                string donvi = DungChung.Ham._getDonVi(_Data, madv);
                var ttdv = _Data.DichVus.Where(p => p.MaDV == madv).FirstOrDefault();
                if (DungChung.Bien.MaBV == "34019" && ttdv != null)
                {
                    _mien = ttdv.Mien;
                    gridView2.SetFocusedRowCellValue(colMien, _mien);
                }
                gridView2.SetFocusedRowCellValue(col_DonVi, donvi);
                double dongia = 0;
                string solo = "";
                string handung = "";
                if (rbtn_kieuke.SelectedIndex == 1)
                {
                    gridView2.SetFocusedRowCellValue(col_bhyt, 0);
                    double soluongTon = 0;
                    dsgia = QLBV.DungChung.Ham._getDSGia(_db, madv, makho);
                    if (dsgia.Count > 0)
                    {
                        dongia = dsgia.First().Gia;
                        if (ppxuat == 3)
                        {
                            solo = dsgia.First().SoLo;
                            handung = dsgia.First().HanDung != null ? dsgia.First().HanDung.ToString() : "";
                        }
                        soluongTon = dsgia.First().SoLuong;
                    }
                    grvdonthuoc.Text = "Số lượng tồn: " + (soluongTon - soluongtong).ToString("##,##0");
                }
                else
                {
                    List<Ham.giaSoLoHSD> tongSL = new List<Ham.giaSoLoHSD>();
                    double SLKe = 0;//sl kê hiện tại
                    for (int i = 0; i <= gridView2.RowCount; i++)
                    {

                        if (i != e.RowHandle && gridView2.GetRowCellValue(i, col_MaDV) != null && gridView2.GetRowCellValue(i, col_soluong) != null && gridView2.GetRowCellValue(i, col_MaDV).ToString() != "" && gridView2.GetRowCellValue(i, col_soluong).ToString() != "")
                        {
                            if (Convert.ToDouble(gridView2.GetRowCellValue(i, col_MaDV)) == madv)
                            {
                                Ham.giaSoLoHSD moi = new Ham.giaSoLoHSD();
                                moi.SoLuong = Convert.ToDouble(gridView2.GetRowCellValue(i, col_soluong));
                                if (gridView2.GetRowCellValue(i, col_dongia) != null && gridView2.GetRowCellValue(i, col_dongia).ToString() != "")
                                    moi.Gia = Convert.ToDouble(gridView2.GetRowCellValue(i, col_dongia));
                                if (gridView2.GetRowCellValue(i, colSoLo) != null)
                                    moi.SoLo = gridView2.GetRowCellValue(i, colSoLo).ToString();
                                if (gridView2.GetRowCellValue(i, colHanDung) != null && gridView2.GetRowCellValue(i, colHanDung).ToString() != "")
                                    moi.HanDung = Convert.ToDateTime(gridView2.GetRowCellValue(i, colHanDung));
                                tongSL.Add(moi);
                            }
                        }
                    }
                    if (gridView2.GetRowCellValue(e.RowHandle, col_soluong) != null && gridView2.GetRowCellValue(e.RowHandle, col_soluong).ToString() != "")
                        SLKe = Convert.ToDouble(gridView2.GetRowCellValue(e.RowHandle, col_soluong));
                    double soluongTon = 0;
                    dsgia = QLBV.DungChung.Ham._getDSGia(_db, madv, makho);
                    dsgiaHT = LayDSGiaHT(tongSL, dsgia, SLKe, 0);
                    if (dsgiaHT.Count > 0)
                    {
                        dongia = dsgiaHT.First().Gia;
                        if (ppxuat == 3)
                        {
                            solo = dsgiaHT.First().SoLo;
                            handung = dsgiaHT.First().HanDung != null ? dsgiaHT.First().HanDung.ToString() : "";
                        }
                        soluongTon = dsgiaHT.First().SoLuong;
                    }
                    grvdonthuoc.Text = "Số lượng tồn: " + (soluongTon - soluongtong).ToString("##,##0");
                    if (_kebosung || _bosungNgtru)
                    {
                        if (_benhnhan != null && _benhnhan.DTuong == "BHYT")
                        {
                            var qbh = _lthuocVT.Where(p => p.MaDV == madv).Where(p => p.TrongDM == 1).ToList();
                            if (qbh.Count > 0)
                                gridView2.SetFocusedRowCellValue(col_bhyt, 1);//trường hợp kê độc lập có thanh toán
                            else
                            {
                                qbh = _lthuocVT.Where(p => p.MaDV == madv).Where(p => p.TrongDM == 0).ToList();
                                if (qbh.Count > 0)
                                {
                                    gridView2.SetFocusedRowCellValue(col_bhyt, 0);
                                }
                                else
                                {
                                    gridView2.SetFocusedRowCellValue(col_bhyt, 2);
                                }
                            }
                        }
                        else
                            gridView2.SetFocusedRowCellValue(col_bhyt, 0);//trường hợp kê độc lập có thanh toán

                    }
                    else
                        gridView2.SetFocusedRowCellValue(col_bhyt, 2);

                    // dongia = getGiaKD(madv, makp, makho);
                    // gridView2.SetFocusedRowCellValue(col_bhyt, 2);
                    //grvdonthuoc.Text = DungChung.Bien.SoLuongTon.ToString("##,##0");
                }
                gridView2.SetFocusedRowCellValue(col_dongia, dongia);
                gridView2.SetFocusedRowCellValue(colHanDung, handung);
                gridView2.SetFocusedRowCellValue(colSoLo, solo);
                int tylett = 100;
                var qdv = _lthuocVT.Where(p => p.MaDV == madv).FirstOrDefault();
                if (qdv != null)
                    tylett = qdv.BHTT ?? 0;
                gridView2.SetFocusedRowCellValue(col_tltt, tylett);
                double thanhtien = 0, soluong = 0;
                if (gridView2.GetFocusedRowCellValue(col_soluong) != null)
                    soluong = Convert.ToInt32(gridView2.GetFocusedRowCellValue(col_soluong));
                if (gridView2.GetFocusedRowCellValue(col_dongia) != null)
                    dongia = Convert.ToDouble(gridView2.GetFocusedRowCellValue(col_dongia));
                if (gridView2.GetFocusedRowCellValue(colMien) != null)
                    _mien = Convert.ToInt32(gridView2.GetFocusedRowCellValue(colMien));
                if (soluong > 0 && dongia > 0)
                {
                    thanhtien = _mien == 1 ? 0 : (soluong * dongia);
                    gridView2.SetFocusedRowCellValue(col_thanhtien, thanhtien);
                }
                if (gridView2.IsNewItemRow(gridView2.FocusedRowHandle))
                {
                    gridView2.SetFocusedRowCellValue(colDuongDung, _getDDung(madv));
                    gridView2.SetFocusedRowCellValue(colLan, "1");
                    gridView2.SetFocusedRowCellValue(colLuong, "1");
                    gridView2.SetFocusedRowCellValue(colMoiLan, "lần, mỗi lần");
                    gridView2.SetFocusedRowCellValue(colDonViUong, donvi);
                }
                else
                {
                    gridView2.SetFocusedRowCellValue(colDuongDung, _getDDung(madv));
                    if (gridView2.GetFocusedRowCellValue(colLan) == null || gridView2.GetFocusedRowCellValue(colLan).ToString() == "")
                        gridView2.SetFocusedRowCellValue(colLan, "1");
                    if (gridView2.GetFocusedRowCellValue(colLuong) == null || gridView2.GetFocusedRowCellValue(colLuong).ToString() == "")
                        gridView2.SetFocusedRowCellValue(colLuong, "1");
                    if (gridView2.GetFocusedRowCellValue(colMoiLan) == null || gridView2.GetFocusedRowCellValue(colMoiLan).ToString() == "")
                        gridView2.SetFocusedRowCellValue(colMoiLan, "lần, mỗi lần");
                    if (gridView2.GetFocusedRowCellValue(colDonViUong) == null || gridView2.GetFocusedRowCellValue(colDonViUong).ToString() == "")
                        gridView2.SetFocusedRowCellValue(colDonViUong, donvi);
                }

            }
            if (e.Column.Name == "col_soluong")
            {
                double thanhtien = 0, soluong = 0, dongia = 0;
                if (gridView2.GetFocusedRowCellValue(col_MaDV) != null)
                    madv = Convert.ToInt32(gridView2.GetFocusedRowCellValue(col_MaDV));
                if (gridView2.GetFocusedRowCellValue(col_soluong) != null)
                    soluong = Convert.ToInt32(gridView2.GetFocusedRowCellValue(col_soluong));
                double soluongtong = soluong;
                if (gridView2.GetFocusedRowCellValue(col_dongia) != null)
                    dongia = Convert.ToDouble(gridView2.GetFocusedRowCellValue(col_dongia));
                for (int i = 0; i < gridView2.RowCount; i++)
                {
                    if (gridView2.GetRowCellValue(i, col_MaDV) != null && gridView2.GetRowCellValue(i, colIDDonct) != null && gridView2.GetFocusedRowCellValue(col_MaDV) != null)
                    {
                        if (Convert.ToInt32(gridView2.GetRowCellValue(i, colIDDonct)) <= 0 && Convert.ToInt32(gridView2.GetRowCellValue(i, col_MaDV)) == Convert.ToInt32(gridView2.GetFocusedRowCellValue(col_MaDV)) && i != e.RowHandle)
                            soluongtong += Convert.ToInt32(gridView2.GetRowCellValue(i, col_soluong));
                    }
                }
                if (gridView2.GetFocusedRowCellValue(colMien) != null)
                    _mien = Convert.ToInt32(gridView2.GetFocusedRowCellValue(colMien));
                if (soluong > 0 && dongia > 0)
                {
                    thanhtien = _mien == 1 ? 0 : (soluong * dongia);
                    gridView2.SetFocusedRowCellValue(col_thanhtien, thanhtien);
                }
                int iddonct = 0;
                if (gridView2.GetFocusedRowCellValue(colIDDonct) != null)
                    iddonct = Convert.ToInt32(gridView2.GetFocusedRowCellValue(colIDDonct));
                int status = -1;
                if (gridView2.GetFocusedRowCellValue(colStatus) != null)
                    status = Convert.ToInt32(gridView2.GetFocusedRowCellValue(colStatus));
                if (rbtn_kieuke.SelectedIndex == 1)
                {
                    dsgia = QLBV.DungChung.Ham._getDSGia(_db, madv, makho);
                    // dongia = dsgia.First().Gia;//_getGiaSD(_Data, madv, 0, 0, 2, makho);
                    if (dsgia.Count() == 0 || dsgia.First().Gia <= 0)
                        MessageBox.Show("Thuốc chưa có giá trong DS Đơn Giá!");
                    gridView2.SetFocusedRowCellValue(col_bhyt, 0);
                }
                else
                {
                    //dongia = getGiaKD(madv, makp, makho);
                    dsgia = QLBV.DungChung.Ham._getDSGia(_db, madv, makho);
                    if (dsgia.Count > 0)
                        dongia = dsgia.First().Gia;
                    if (_kebosung || _bosungNgtru)
                    {
                        if (_benhnhan != null && _benhnhan.DTuong == "BHYT")
                        {
                            var qbh = _lthuocVT.Where(p => p.MaDV == madv).Where(p => p.TrongDM == 1).ToList();
                            if (qbh.Count > 0)
                                gridView2.SetFocusedRowCellValue(col_bhyt, 1);//trường hợp kê độc lập có thanh toán
                            else
                            {
                                qbh = _lthuocVT.Where(p => p.MaDV == madv).Where(p => p.TrongDM == 0).ToList();
                                if (qbh.Count > 0)
                                {
                                    gridView2.SetFocusedRowCellValue(col_bhyt, 0);
                                }
                                else
                                {
                                    gridView2.SetFocusedRowCellValue(col_bhyt, 2);
                                }
                            }
                        }
                        else
                            gridView2.SetFocusedRowCellValue(col_bhyt, 0);//trường hợp kê độc lập có thanh toán
                    }
                    else
                        gridView2.SetFocusedRowCellValue(col_bhyt, 2);

                }
                if (rbtn_kieuke.SelectedIndex == 0)
                {

                    if (iddonct <= 0)
                    {
                        double ton = 0;
                        if (dsgia.Count > 0)
                            ton = dsgia.First().SoLuong - soluongtong;
                        grvdonthuoc.Text = "Số lượng tồn: " + ton.ToString();
                        if (soluong != 0 && (dsgia.Count <= 0 || soluongtong > dsgia.First().SoLuong))
                        {
                            MessageBox.Show("Số lượng trong kho không đủ");
                            gridView2.SetFocusedRowCellValue(col_soluong, "0");
                            gridView2.FocusedColumn = gridView2.VisibleColumns[2];
                        }
                    }
                    else
                    {
                        var soluongdk = _Data.DThuoccts.Where(p => p.IDDonct == iddonct).Select(p => p.SoLuong).FirstOrDefault();
                        double ton = soluongdk;
                        if (dsgia.Count > 0)
                            ton = dsgia.First().SoLuong + ton;
                        grvdonthuoc.Text = "Số lượng tồn: " + ton.ToString();
                        if (status == 0)
                        {
                            if (soluongtong != 0 && (dsgia.Count <= 0 || (soluong > ton)))
                            {
                                MessageBox.Show("Số lượng trong kho không đủ");
                                gridView2.SetFocusedRowCellValue(col_soluong, "0");
                                gridView2.FocusedColumn = gridView2.VisibleColumns[2];
                            }
                        }

                    }
                }
                else if (rbtn_kieuke.SelectedIndex == 1 || rbtn_kieuke.SelectedIndex == 3)
                {
                    dsgia = QLBV.DungChung.Ham._getDSGia(_db, madv, makho);
                    double SlTondg1 = 0;
                    if (dsgia.Count > 0)
                    {
                        SlTondg1 = dsgia.First().SoLuong;
                    }
                    if (iddonct <= 0)
                    {
                        double ton = 0;
                        if (dsgia.Count > 0)
                            ton = SlTondg1 - soluongtong;
                        grvdonthuoc.Text = "Số lượng tồn: " + ton.ToString();
                        if (soluong != 0 && soluongtong > SlTondg1)//DungChung.Bien.SoLuongTon)
                        {
                            MessageBox.Show("Số lượng trong kho không đủ");
                            gridView2.SetFocusedRowCellValue(col_soluong, "0");
                            gridView2.FocusedColumn = gridView2.VisibleColumns[2];
                        }
                    }
                    else
                    {
                        var soluongdk = _Data.DThuoccts.Where(p => p.IDDonct == iddonct).Select(p => p.SoLuong).FirstOrDefault();
                        grvdonthuoc.Text = "Số lượng tồn: " + (SlTondg1 + soluongdk - soluong).ToString();
                        //if (status == 0)
                        //{
                        if (soluong != 0 && soluong > (SlTondg1 + soluongdk))
                        {
                            grvdonthuoc.Text = "Số lượng tồn: " + SlTondg1.ToString();
                            MessageBox.Show("Số lượng trong kho không đủ");
                            gridView2.SetFocusedRowCellValue(col_soluong, "0");
                            gridView2.FocusedColumn = gridView2.VisibleColumns[2];
                        }
                        //}
                    }
                }
            }
            if (e.Column.Name == "col_bhyt")
            {
                int trongBH = -1;
                if (gridView2.GetFocusedRowCellValue(col_bhyt) != null)
                {
                    trongBH = Convert.ToInt32(gridView2.GetFocusedRowCellValue(col_bhyt));
                }

                if (trongBH == 1)
                {
                    if (_benhnhan != null && _benhnhan.DTuong != "BHYT")
                    {
                        MessageBox.Show("Bệnh nhân không có Thẻ BHYT, chí phí không được quyết toán trong DMBH");
                        gridView2.SetFocusedRowCellValue(col_bhyt, 0);
                    }
                    else
                    {
                        if (gridView2.GetFocusedRowCellValue(col_MaDV) != null)
                        {
                            madv = Convert.ToInt32(gridView2.GetFocusedRowCellValue(col_MaDV));
                        }
                        var qdv = _lthuocVT.Where(p => p.MaDV == madv).Where(p => p.TrongDM == 1).FirstOrDefault();
                        if (qdv == null)
                        {
                            MessageBox.Show("Thuốc / vật tư không nằm trong danh mục BHTT");
                            gridView2.SetFocusedRowCellValue(col_bhyt, 0);
                        }
                    }
                }

            }
            if (e.Column.Name == "colMien")
            {
                if (gridView2.GetFocusedRowCellValue(col_dongia) != null)
                {
                    _mien = Convert.ToInt32(gridView2.GetFocusedRowCellValue(colMien));
                    if (_mien == 1)
                        gridView2.SetFocusedRowCellValue(col_thanhtien, 0);
                    else
                    {
                        double thanhtien = 0, soluong = 0, dongia = 0;
                        if (gridView2.GetFocusedRowCellValue(col_soluong) != null)
                            soluong = Convert.ToInt32(gridView2.GetFocusedRowCellValue(col_soluong));
                        if (gridView2.GetFocusedRowCellValue(col_dongia) != null)
                            dongia = Convert.ToDouble(gridView2.GetFocusedRowCellValue(col_dongia));
                        thanhtien = soluong * dongia;
                        gridView2.SetFocusedRowCellValue(col_thanhtien, thanhtien);
                    }

                }

            }

            //if (e.Column.FieldName == "MaDV")
            //{
            //    var dataRow = gridView2.GetRow(e.RowHandle) as Dtct;
            //    if (dataRow != null)
            //    {
            //        var dichVuTheoKhoXuats = UP_tenthuoc.DataSource as List<DichVuTheoKhoXuat>;
            //        if (dichVuTheoKhoXuats != null && dichVuTheoKhoXuats.Count > 0)
            //        {
            //            var dichVu = dichVuTheoKhoXuats.Find(f => f.MaDV == dataRow.MaDV);
            //            if (dichVu != null)
            //            {
            //                dataRow.SoLo = dichVu.SoLo;
            //                dataRow.HanDung = dichVu.HanDung;
            //            }
            //        }
            //    }
            //}
        }

        private void luudon(int AttachIDDonct, int makp)
        {
            
            QLBV_Database.QLBVEntities _db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var dt = _db.DThuocs.Where(p => p.IDDon == selectedIdDon).FirstOrDefault();
            if (dt == null || isDonMoi == 1)
            {
                DThuoc dthuoccd = new DThuoc();
                dthuoccd.NgayKe = lup_ngayke.DateTime;
                dthuoccd.MaBNhan = _mabn;
                dthuoccd.MaKP = _makp;
                dthuoccd.MaKXuat = _makho;
                dthuoccd.MaCB = _macb;
                dthuoccd.PLDV = 1;
                if (rbtn_kieuke.SelectedIndex == 1)
                {
                    dthuoccd.KieuDon = 6; // kê đơn ngoài
                    //dthuoccd.Status = 0;
                }

                else if (rbtn_kieuke.SelectedIndex == 3)
                {
                    dthuoccd.KieuDon = 1; // kê đơn bổ sung ngoại trú
                    //dthuoccd.Status = 0;
                }
                else
                {
                    //dthuoccd.Status = 2;
                    dthuoccd.KieuDon = 7; // đơn kèm dịch vụ
                }
                _Data.DThuocs.Add(dthuoccd);
                _Data.SaveChanges();
                int IDDon = dthuoccd.IDDon;
                for (int i = 0; i < gridView2.RowCount; i++)
                {
                    float sluong = 0;
                    if (gridView2.GetRowCellValue(i, col_soluong) != null)
                        sluong = Convert.ToUInt32(gridView2.GetRowCellValue(i, col_soluong));
                    int madv = Convert.ToInt32(gridView2.GetRowCellValue(i, col_MaDV));
                    if (madv > 0) // && sluong > 0
                    {
                        string donvi = Convert.ToString(gridView2.GetRowCellValue(i, col_DonVi));


                        double dgia = Convert.ToDouble(gridView2.GetRowCellValue(i, col_dongia));

                        int bh = Convert.ToInt32(gridView2.GetRowCellValue(i, col_bhyt));
                        string ghichu = Convert.ToString(gridView2.GetRowCellValue(i, col_ghichu));
                        int tltt = Convert.ToInt32(gridView2.GetRowCellValue(i, col_tltt));
                        float tt = Convert.ToUInt32(gridView2.GetRowCellValue(i, col_thanhtien));

                        string lan = Convert.ToString(gridView2.GetRowCellValue(i, colLan));
                        string luong = Convert.ToString(gridView2.GetRowCellValue(i, colLuong));
                        string moilan = Convert.ToString(gridView2.GetRowCellValue(i, colMoiLan));
                        string dviUong = Convert.ToString(gridView2.GetRowCellValue(i, colDonViUong));
                        string ddung = Convert.ToString(gridView2.GetRowCellValue(i, colDuongDung));
                        DThuocct moi = new DThuocct();
                        moi.MaKP = _makp;
                        moi.MaKXuat = _makho;
                        moi.NgayNhap = lup_ngayke.DateTime;
                        moi.MaDV = madv;
                        moi.DonVi = donvi;
                        moi.DonGia = dgia;
                        moi.SoLuong = sluong;
                        moi.TrongBH = bh;
                        moi.GhiChu = ghichu;
                        moi.TyLeTT = tltt;
                        moi.ThanhTien = tt;
                        moi.IDDon = IDDon;
                        moi.MaCB = _macb;
                        moi.SoLan = lan;
                        moi.MoiLan = moilan;
                        moi.Luong = luong;
                        moi.DviUong = dviUong;
                        moi.DuongD = ddung;
                        if (gridView2.GetRowCellValue(i, colSoLo) != null)
                            moi.SoLo = gridView2.GetRowCellValue(i, colSoLo).ToString();
                        if (gridView2.GetRowCellValue(i, colHanDung) != null)
                            moi.HanDung = Convert.ToDateTime(gridView2.GetRowCellValue(i, colHanDung));
                        if (AttachIDDonct > 0)
                            moi.AttachIDDonct = AttachIDDonct;
                        if (gridView2.GetRowCellValue(i, colMien) != null)
                            moi.MienCT = Convert.ToInt32(gridView2.GetRowCellValue(i, colMien).ToString());
                        if (rbtn_kieuke.SelectedIndex == 1)
                        {
                            if (DungChung.Bien.MaBV == "34019")
                                moi.ThanhToan = 0;
                            else
                                moi.ThanhToan = 0;
                            moi.Status = 0;
                        }
                        else if (rbtn_kieuke.SelectedIndex == 0)// dung thêm 080818
                        {
                            moi.Status = 0;// kê vào tủ trực => lên phiếu lĩnh cho tủ trực bình thường, không phải lấy thẳng từ khoa
                        }
                        else if (rbtn_kieuke.SelectedIndex == 3)
                        {
                            moi.ThanhToan = 0;
                            moi.Status = 0;
                        }
                        else
                            moi.Status = 2;
                        _db.DThuoccts.Add(moi);
                    }
                }
                if (_db.SaveChanges() > 0)
                {
                    MessageBox.Show("Thêm mới đơn thuốc thành công!", "Thông báo!");
                }
                else
                {
                    MessageBox.Show("Thêm mới đơn thuốc không thành công!", "Thông báo!");
                }
            }
            else
            {

                dt.NgayKe = lup_ngayke.DateTime;
                dt.MaBNhan = _mabn;
                dt.MaKP = _makp;
                dt.MaKXuat = _makho;
                dt.MaCB = _macb;
                _Data.SaveChanges();
                for (int i = 0; i < gridView2.RowCount; i++)
                {
                    int iddonct = 0;
                    if (gridView2.GetRowCellValue(i, colIDDonct) != null)
                        iddonct = Convert.ToInt32(gridView2.GetRowCellValue(i, colIDDonct));
                    int madv = Convert.ToInt32(gridView2.GetRowCellValue(i, col_MaDV));
                    string donvi = Convert.ToString(gridView2.GetRowCellValue(i, col_DonVi));
                    string lan = Convert.ToString(gridView2.GetRowCellValue(i, colLan));
                    string luong = Convert.ToString(gridView2.GetRowCellValue(i, colLuong));
                    string moilan = Convert.ToString(gridView2.GetRowCellValue(i, colMoiLan));
                    string dviUong = Convert.ToString(gridView2.GetRowCellValue(i, colDonViUong));
                    double dgia = Convert.ToDouble(gridView2.GetRowCellValue(i, col_dongia));
                    float sluong = Convert.ToUInt32(gridView2.GetRowCellValue(i, col_soluong));
                    int bh = Convert.ToInt32(gridView2.GetRowCellValue(i, col_bhyt));
                    string ghichu = Convert.ToString(gridView2.GetRowCellValue(i, col_ghichu));
                    int tltt = Convert.ToInt32(gridView2.GetRowCellValue(i, col_tltt));
                    float tt = Convert.ToUInt32(gridView2.GetRowCellValue(i, col_thanhtien));
                    string ddung = Convert.ToString(gridView2.GetRowCellValue(i, colDuongDung));
                    if (madv > 0 && sluong > 0)
                    {
                        var dtct = _db.DThuoccts.Where(p => p.IDDonct == iddonct).FirstOrDefault();
                        if (dtct == null)
                        {
                            DThuocct moi = new DThuocct();
                            moi.MaKP = _makp;
                            moi.MaKXuat = _makho;
                            moi.NgayNhap = lup_ngayke.DateTime;
                            moi.MaDV = madv;
                            moi.DonVi = donvi;
                            moi.DonGia = dgia;
                            moi.SoLuong = sluong;
                            moi.TrongBH = bh;
                            moi.GhiChu = ghichu;
                            moi.TyLeTT = tltt;
                            moi.ThanhTien = tt;
                            moi.IDDon = dt.IDDon;
                            moi.SoLan = lan;
                            moi.MoiLan = moilan;
                            moi.Luong = luong;
                            moi.DviUong = dviUong;
                            moi.DuongD = ddung;
                            if (gridView2.GetRowCellValue(i, colSoLo) != null)
                                moi.SoLo = gridView2.GetRowCellValue(i, colSoLo).ToString();
                            if (gridView2.GetRowCellValue(i, colHanDung) != null)
                                moi.HanDung = Convert.ToDateTime(gridView2.GetRowCellValue(i, colHanDung));
                            if (gridView2.GetRowCellValue(i, colMien) != null)
                                moi.MienCT = Convert.ToInt32(gridView2.GetRowCellValue(i, colMien).ToString());
                            if (rbtn_kieuke.SelectedIndex == 1)
                            {
                                if (DungChung.Bien.MaBV == "34019")
                                    moi.ThanhToan = 0;
                                else
                                    moi.ThanhToan = 2;
                                moi.Status = 0;
                            }
                            else if (rbtn_kieuke.SelectedIndex == 0)// dung thêm 080818
                            {
                                moi.Status = 0;// kê vào tủ trực => lên phiếu lĩnh cho tủ trực bình thường, không phải lấy thẳng từ khoa
                            }
                            else if (rbtn_kieuke.SelectedIndex == 3)
                            {
                                moi.ThanhToan = 0;
                                moi.Status = 0;
                            }
                            else
                            {
                                moi.Status = 2;
                            }
                            if (AttachIDDonct > 0)
                                moi.AttachIDDonct = AttachIDDonct;
                            _db.DThuoccts.Add(moi);
                            _db.SaveChanges();
                        }
                        else
                        {
                            if (dtct.ThanhToan == 1)
                                continue;

                            if (rbtn_kieuke.SelectedIndex == 1)
                            {
                                if (DungChung.Bien.MaBV != "34019")
                                    dtct.ThanhToan = 2;
                                dtct.Status = 0;
                            }

                            else if (rbtn_kieuke.SelectedIndex == 0)
                            {
                                dtct.Status = 0;// tính cho tủ trực

                            }
                            else if (rbtn_kieuke.SelectedIndex == 3)
                            {
                                dtct.Status = 0;
                            }
                            else
                                dtct.Status = 2;
                            dtct.MaKP = _makp;
                            dtct.MaKXuat = _makho;
                            dtct.NgayNhap = lup_ngayke.DateTime;
                            dtct.MaDV = madv;
                            dtct.DonVi = donvi;
                            dtct.DonGia = dgia;
                            dtct.SoLuong = sluong;
                            dtct.TrongBH = bh;
                            dtct.GhiChu = ghichu;
                            dtct.TyLeTT = tltt;
                            dtct.ThanhTien = tt;
                            dtct.IDDon = dt.IDDon;
                            dtct.SoLan = lan;
                            dtct.MoiLan = moilan;
                            dtct.Luong = luong;
                            dtct.DviUong = dviUong;
                            dtct.DuongD = ddung;
                            if (AttachIDDonct > 0)
                                dtct.AttachIDDonct = AttachIDDonct;
                            else
                                dtct.AttachIDDonct = null;
                        }
                    }
                }
                if (_db.SaveChanges() > 0)
                {
                    MessageBox.Show("Sửa đơn thuốc thành công!", "Thông báo!");
                }
                else
                {
                    MessageBox.Show("Sửa đơn thuốc không thành công!", "Thông báo!");
                }
            }
        }

        private void btn_huy_Click(object sender, EventArgs e)
        {
            this.Dispose();

        }

        private void labelControl4_Click(object sender, EventArgs e)
        {

        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            if (ktranhapvtyt())
            {
                luudon(iddonct, _makp);
                statusLuu = 0;
                anhien(true);
                frm_kedon_Load(sender, e);

            }
        }

        private void grCPdinhkem_Click(object sender, EventArgs e)
        {

        }

        private void gridView2_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {

        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            if (_rv != null)
            {
                MessageBox.Show("Bệnh nhân đã ra viện, không thể sửa", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            QLBV_Database.QLBVEntities _db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var qdt = (from dt in _db.DThuocs.Where(p => p.IDDon == selectedIdDon) join dtct in _db.DThuoccts on dt.IDDon equals dtct.IDDon select new { dtct.Status, dtct.SoPL }).FirstOrDefault();
            if (qdt != null)
            {
                if (qdt.Status == 1)
                {
                    MessageBox.Show("Bệnh nhân đã xuất dược, không thể sửa", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
                if (qdt.SoPL > 0)
                {
                    MessageBox.Show("Đơn thuốc đã lên phiếu lĩnh, không thể sửa", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
            }
            DialogResult _result = MessageBox.Show("Bạn có muốn sửa đơn ?", "Hỏi sửa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (_result == DialogResult.Yes)
            {
                statusLuu = 1;

                anhien(false);

            }
        }

        private void rbtn_kieuke_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void rbtn_kieuke_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtn_kieuke.SelectedIndex == 1)
            {
                List<trongDM> _trong = new List<trongDM>();
                _trong.Add(new trongDM { TrongDM = 0, Ten = "Ngoài DMBH" });
                _trong.Add(new trongDM { TrongDM = 2, Ten = "Không TT" });
                lup_khoxuat.Enabled = true;
                btn_luu.Enabled = true;
                if (DungChung.Bien.MaBV == "30010")
                {
                    col_bhyt.OptionsColumn.AllowFocus = true;
                    col_bhyt.OptionsColumn.ReadOnly = false;
                }
                else
                {
                    col_bhyt.OptionsColumn.AllowFocus = false;
                    col_bhyt.OptionsColumn.ReadOnly = true;
                }
                lup_trongDMBH.DataSource = _trong;
            }
            else
            {
                List<trongDM> _trong = new List<trongDM>();
                _trong.Add(new trongDM { TrongDM = 0, Ten = "Ngoài DMBH" });
                _trong.Add(new trongDM { TrongDM = 1, Ten = "Trong DMBH" });
                _trong.Add(new trongDM { TrongDM = 2, Ten = "Không TT" });
                if (rbtn_kieuke.SelectedIndex == 3)
                    lup_khoxuat.Enabled = true;
                else
                    lup_khoxuat.Enabled = false;
                col_bhyt.OptionsColumn.AllowFocus = true;
                col_bhyt.OptionsColumn.ReadOnly = false;
                lup_trongDMBH.DataSource = _trong;
            }

        }

        private void lup_khoake_EditValueChanged(object sender, EventArgs e)
        {
            _lcb = _Data.CanBoes.ToList();
            if (lup_khoake.EditValue != null)
                _makp = Convert.ToInt32(lup_khoake.EditValue);
            string _makpsd = ";" + _makp + ";";

            if (DungChung.Bien.MaBV != "27022" && DungChung.Bien.MaBV != "24012" && DungChung.Bien.MaBV != "24297" && DungChung.Bien.MaBV != "24272" && DungChung.Bien.MaBV != "24272")
            {
                if (DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "27194")
                {
                    lup_bske.Properties.DataSource = _lcb.Where(p => p.MaKP == _makp || p.MaKPsd.Contains(_makpsd)).ToList();
                }
                else
                    lup_bske.Properties.DataSource = _lcb.Where(p => p.MaKP == _makp).ToList();
            }
            else
                lup_bske.Properties.DataSource = _lcb.Where(p => p.Status == 1).Where(p => p.MaKPsd.Contains(_makpsd)).Where(p => p.CapBac != null).Where(p => p.CapBac.ToLower().Contains("bs") || p.CapBac.ToLower().Contains("bác sĩ") || p.CapBac.ToLower().Contains("bác sỹ") || p.CapBac.ToLower().Contains("ys") || p.CapBac.ToLower().Contains("y sĩ") || p.CapBac.ToLower().Contains("y sỹ") || p.CapBac.ToLower().Contains("giáo sư") || p.CapBac.ToLower().Contains("gs") || p.CapBac.ToLower().Contains("tiến sĩ") || p.CapBac.ToLower().Contains("ts")).ToList();
            if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24297")
            {
                string MKP = _makp.ToString();
                lup_khoxuat.Properties.DataSource = (_lkp.Where(p => (p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc || (p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc && (p.MaKPsd != null ? p.MaKPsd.Contains(MKP) : false))) && p.Status == 1 && p.MaBVsd == DungChung.Bien.MaBV)).ToList();
            }

            else if (rbtn_kieuke.SelectedIndex == 0)
            {
                lup_khoxuat.Properties.DataSource = _lkp.Where(p => (p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc && p.NhomKP == _makp) && p.Status == 1).ToList();
            }
            else
                lup_khoxuat.Properties.DataSource = _lkp.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc && p.Status == 1).ToList();
            lup_khoxuat_EditValueChanged(sender, e);
        }

        private class DichVuTheoKhoXuat
        {
            public int? MaDV { get; set; }
            public string TenDV { get; set; }
            public string HamLuong { get; set; }
            public string SoLo { get; set; }
            public DateTime? HanDung { get; set; }
        }
        void loaddsThuoc(int makho, int pl, int makp)
        {
            _ldv.Clear();
            if (pl == 0)
            {
                _ldv = _ldvKD(makp, makho).ToList();
                UP_tenthuoc.DataSource = (from dv in _ldv
                                          select new DichVuTheoKhoXuat()
                                          {
                                              SoLo = dv.SoLo,
                                              HanDung = dv.HanDung,
                                              MaDV = dv.MaDV,
                                              TenDV = dv.TenDV,
                                              HamLuong = dv.HamLuong
                                          }).ToList();
            }

            else
            {
                if (ppxuat == 3)
                {
                    var q = (from nd in _Data.NhapDs.Where(p => p.PLoai == 1 && p.MaKP == makho)
                             join ndct in _Data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                             join dv in _Data.DichVus on ndct.MaDV equals dv.MaDV
                             //join dgdv in _Data.DonGiaDVs on dv.MaDV equals dgdv.MaDV
                             where DungChung.Bien.MaBV == "27183" ? dv.Status != 0 : true
                             select new { ndct.MaDV, dv.TenDV, dv.HamLuong, ndct.SoLo, ndct.HanDung }).ToList().Distinct().ToList();

                    UP_tenthuoc.DataSource = (from dv in q
                                              select new DichVuTheoKhoXuat()
                                              {
                                                  SoLo = dv.SoLo,
                                                  HanDung = dv.HanDung,
                                                  MaDV = dv.MaDV,
                                                  TenDV = dv.TenDV,
                                                  HamLuong = dv.HamLuong
                                              }).ToList();
                }
                else
                {
                    var q = (from nd in _Data.NhapDs.Where(p => p.PLoai == 1 && p.MaKP == makho)
                             join ndct in _Data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                             join dv in _Data.DichVus on ndct.MaDV equals dv.MaDV
                             //join dgdv in _Data.DonGiaDVs on dv.MaDV equals dgdv.MaDV
                             where DungChung.Bien.MaBV == "27183" ? dv.Status != 0 : true
                             select new { ndct.MaDV, dv.TenDV, dv.HamLuong}).ToList().Distinct().ToList();

                    UP_tenthuoc.DataSource = (from dv in q
                                              select new DichVuTheoKhoXuat()
                                              {
                                                  MaDV = dv.MaDV,
                                                  TenDV = dv.TenDV,
                                                  HamLuong = dv.HamLuong
                                              }).ToList();
                }
            }
        }
        private void lup_khoxuat_EditValueChanged(object sender, EventArgs e)
        {
            if (lup_khoxuat.EditValue != null)
            {
                _makho = Convert.ToInt32(lup_khoxuat.EditValue);
                var getppxuat = _lkp.Where(p => p.MaKP == _makho).Select(p => p.PPXuat).FirstOrDefault();
                if (getppxuat != null)
                    ppxuat = getppxuat.Value;
            }
            if (lup_khoake.EditValue != null)
                _makp = Convert.ToInt32(lup_khoake.EditValue);

            var khoxuat = (from a in _Data.DThuocs
                           join b in _Data.KPhongs on a.MaKXuat equals b.MaKP
                           where a.PLDV == 1 && (a.KieuDon == 1 || a.KieuDon == 6 || a.KieuDon == 7)
                                 && a.MaBNhan == _mabn
                           select a.MaKXuat).ToList();

            //if (khoxuat.Contains(_makho) && lup_khoxuat.ReadOnly == false && lup_khoxuat.Focused == true)
            //{
            //    MessageBox.Show("Kho đã có đơn thuốc", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    lup_khoxuat.Focus();
            //    btn_luu.Enabled = false;
            //}
            //else
            //    btn_luu.Enabled = true;


            loaddsThuoc(_makho, rbtn_kieuke.SelectedIndex, _makp);
        }

        private void lup_bske_EditValueChanged(object sender, EventArgs e)
        {
            if (lup_bske.EditValue != null)
                _macb = lup_bske.EditValue.ToString();
        }

        private void btn_xoa_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            // hỏi xóa
            // ra viện không cho xóa
            if (_rv != null)
            {
                MessageBox.Show("Bệnh nhân đã ra viện, không thể xóa", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            QLBV_Database.QLBVEntities _db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var qdt = (from dt in _db.DThuocs.Where(p => p.IDDon == selectedIdDon) join dtct in _db.DThuoccts on dt.IDDon equals dtct.IDDon select new { dtct.Status, dtct.SoPL }).FirstOrDefault();
            if (qdt != null)
            {
                if (qdt.Status == 1)
                {
                    MessageBox.Show("Bệnh nhân đã xuất dược, không thể xóa", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
                if (qdt.SoPL > 0)
                {
                    MessageBox.Show("Đơn thuốc đã lên phiếu lĩnh, không thể xóa", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
            }

            var xoa = (from dt in _db.DThuocs.Where(p => p.IDDon == selectedIdDon)
                       join dtct in _db.DThuoccts on dt.IDDon equals dtct.IDDon
                       select new { dt, dtct }).ToList();
            if (xoa.Where(p => p.dtct.ThanhToan == 1).Count() > 0)
            {
                MessageBox.Show("Thuốc/vật tư đã thu trực tiếp không thể xóa!");
                return;
            }

            DialogResult _result = MessageBox.Show("Bạn có muốn xóa đơn thuốc?", "Hỏi xóa", MessageBoxButtons.YesNo);
            if (_result == DialogResult.Yes)
            {

                var dthuocct = _Data.DThuoccts.Where(p => p.IDDon == selectedIdDon).ToList();
                foreach (var item in dthuocct)
                {
                    _Data.DThuoccts.Remove(item);

                }
                if (dthuocct.Count > 0)
                {
                    _Data.SaveChanges();
                }
                var dthuoc = _Data.DThuocs.Where(p => p.IDDon == selectedIdDon).ToList();
                foreach (var item in dthuoc)
                {
                    _Data.DThuocs.Remove(item);

                }
                if (dthuoc.Count > 0)
                {
                    _Data.SaveChanges();
                }
                MessageBox.Show("Xóa thành công!");

                frm_kedon_Load(sender, e);

            }
        }

        private void btn_kluu_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn có muốn hủy đơn đang kê ?", "Thông báo !", MessageBoxButtons.YesNo);
            if (kq == DialogResult.Yes)
            {
                frm_kedon_Load(sender, e);
            }
        }

        private void gridView2_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if (gridView2.RowCount > 0)
            {
                rbtn_kieuke.Enabled = false;
            }
            else
            {

                rbtn_kieuke.Enabled = true;
                frm_kedon_Load(sender, e);
            }
        }
        private class thongke
        {
            private string thuoc, vtth, dvtThuoc, dvtVTTH;
            private double slThuoc, slVTTH;
            private int sttThuoc, sttVTYT;

            public int SttVTYT
            {
                get { return sttVTYT; }
                set { sttVTYT = value; }
            }

            public int SttThuoc
            {
                get { return sttThuoc; }
                set { sttThuoc = value; }
            }

            public double SlVTTH
            {
                get { return slVTTH; }
                set { slVTTH = value; }
            }

            public double SlThuoc
            {
                get { return slThuoc; }
                set { slThuoc = value; }
            }
            public string DvtVTTH
            {
                get { return dvtVTTH; }
                set { dvtVTTH = value; }
            }

            public string DvtThuoc
            {
                get { return dvtThuoc; }
                set { dvtThuoc = value; }
            }

            public string Vtth
            {
                get { return vtth; }
                set { vtth = value; }
            }

            public string Thuoc
            {
                get { return thuoc; }
                set { thuoc = value; }
            }

        }
        private void thongk(int _idCD)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<thongke> _lthongke = new List<thongke>();
            frmIn frm = new frmIn();
            BaoCao.RepBTKThuoc_VTTHPhongMo_ rep = new BaoCao.RepBTKThuoc_VTTHPhongMo_();
            var bs = (from cls in data.CLS.Where(p => p.MaBNhan == _mabn)
                      join cd in data.ChiDinhs.Where(p => p.IDCD == _idCD) on cls.IdCLS equals cd.IdCLS
                      select new { cls.DSCBTH, cd.MaCBth, cls.MaBNhan, cd.NgayTH }).ToList();
            int mabn = 0;
            int stt1 = 0, stt2 = 0;

            if (bs.Count > 0)
            {
                if (bs.First().DSCBTH != null)
                {
                    string _dscb = bs.First().DSCBTH.ToString();
                    string[] a = new string[5];
                    a = QLBV_Library.QLBV_Ham.LayChuoi(';', _dscb);
                    rep.BSGM.Value = a[1];
                }
                mabn = bs.First().MaBNhan ?? 0;
                var qbn = (from a in data.BenhNhans.Where(p => p.MaBNhan == mabn) select new { a.TenBNhan, a.Tuoi }).ToList();
                rep.TenBN.Value = qbn.First().TenBNhan.ToUpper();
                if (DungChung.Bien.MaBV == "24012")
                {
                    rep.Tuoi.Value = Convert.ToInt32(DungChung.Ham.TuoitheoThang(data, _mabn, DungChung.Bien.formatAge_24012));
                }
                else
                    rep.Tuoi.Value = Convert.ToInt32(qbn.First().Tuoi);
                string macbth = bs.First().MaCBth;
                var cb = data.CanBoes.Where(p => p.MaCB == macbth).FirstOrDefault();
                if (cb != null)
                    rep.BSPT.Value = cb.TenCB;
                if (bs.First().NgayTH != null)
                    rep.NgayKe.Value = "Ngày " + bs.First().NgayTH.Value.Day + " tháng " + bs.First().NgayTH.Value.Month + " năm " + bs.First().NgayTH.Value.Year;
                else
                    rep.NgayKe.Value = "Ngày ......... tháng ........ năm";

            }
            var vv = (from a in data.VaoViens.Where(p => p.MaBNhan == mabn) select new { a.SoBA }).ToList();
            if (vv.Count() > 0)
            {
                rep.SoHSBN.Value = vv.First().SoBA;
            }
            var dthuocct = data.DThuoccts.Where(p => p.IDCD == _idCD).FirstOrDefault();
            int iddonct = 0;
            //if (dthuocct != null)
            //    iddonct = dthuocct.IDDonct;
            var dt = (from a in data.DThuocs.Where(p => p.MaBNhan == mabn && p.MaKP == _makp)
                      join b in data.DThuoccts.Where(p => iddonct == 0 ? (p.AttachIDDonct == 0 || p.AttachIDDonct == null) : p.AttachIDDonct == iddonct) on a.IDDon equals b.IDDon
                      join c in data.DichVus on b.MaDV equals c.MaDV
                      join d in data.TieuNhomDVs.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 10 || p.IDNhom == 11) on c.IdTieuNhom equals d.IdTieuNhom
                      select new
                      {
                          b.MaDV,
                          c.TenDV,
                          c.DonVi,
                          b.SoLuong,
                          d.IDNhom,
                      }).OrderBy(p => new { p.IDNhom, p.TenDV }).ToList();
            var thuoc = dt.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6).ToList();
            var VTYT = dt.Where(p => p.IDNhom == 10 || p.IDNhom == 11).ToList();
            int dem = (VTYT.Count > thuoc.Count) ? VTYT.Count : thuoc.Count;
            for (int i = 0; i < dem; i++)
            {
                thongke ds = new thongke();
                if (thuoc.Count > i)
                {
                    var t = thuoc.Skip(i).Take(1).FirstOrDefault();
                    ds.Thuoc = t.TenDV;
                    ds.DvtThuoc = t.DonVi;
                    ds.SlThuoc = t.SoLuong;
                    stt1++;
                    ds.SttThuoc = stt1;

                }
                if (VTYT.Count > i)
                {
                    var vt = VTYT.Skip(i).Take(1).FirstOrDefault();
                    ds.Vtth = vt.TenDV;
                    ds.DvtVTTH = vt.DonVi;
                    ds.SlVTTH = vt.SoLuong;
                    stt2++;
                    ds.SttVTYT = stt2;
                }
                _lthongke.Add(ds);
            }
            if (_tenPhieu != "")
                rep.lab1.Text = _tenPhieu.ToUpper();

            var lthongke = _lthongke.Select(p => new { SttThuoc = (p.SttThuoc > 0) ? p.SttThuoc.ToString() : null }).ToList();

            rep.DataSource = _lthongke;
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
        private void btnin_Click(object sender, EventArgs e)
        {
            if (rbtn_kieuke.SelectedIndex == 0)
            {
                thongk(_idcd);
            }
            else
            {
                DungChung.Ham.InDon(selectedIdDon, _mabn, 0, 0, DungChung.Bien.MaBV == "30010" ? false : true);
            }
        }

        private void lup_khoxuat_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (statusLuu > 0 && isDonMoi != 1)
            {
                if (gridView2.RowCount > 1)
                {
                    MessageBox.Show("Đơn đã có thuốc, bạn không thể sửa kho xuất!");
                    e.Cancel = true;
                }
                if (gridView2.RowCount == 1)
                {
                    if (gridView2.GetRowCellValue(1, col_MaDV) != null && Convert.ToInt32(gridView2.GetRowCellValue(1, col_MaDV)) > 0)
                    {
                        MessageBox.Show("Đơn đã có thuốc, bạn không thể sửa kho xuất!");
                        e.Cancel = true;

                    }

                }
            }

        }
        int statusLuu = 0;
        private void lup_khoake_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (statusLuu > 0)
            {
                if (gridView2.RowCount > 1)
                {
                    MessageBox.Show("Đơn đã có thuốc, bạn không thể sửa khoa kê!");
                    e.Cancel = true;
                }
                if (gridView2.RowCount == 1)
                {
                    if (gridView2.GetRowCellValue(1, col_MaDV) != null && Convert.ToInt32(gridView2.GetRowCellValue(1, col_MaDV)) > 0)
                    {
                        MessageBox.Show("Đơn đã có thuốc, bạn không thể sửa khoa kê!");
                        e.Cancel = true;

                    }

                }

            }

        }

        private void gridView2_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {

        }

        private void grvdonthuoc_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            statusLuu = 1;
            int madv = 0;
            int makho = 0;
            int _mien = 0;
            List<DungChung.Ham.giaSoLoHSD> dsgiaHT = new List<QLBV.DungChung.Ham.giaSoLoHSD>();

            if (lup_khoxuat.EditValue != null)
                makho = Convert.ToInt32(lup_khoxuat.EditValue);
            int makp = 0;
            if (lup_khoake.EditValue != null)
                makp = Convert.ToInt32(lup_khoake.EditValue);
            List<DungChung.Ham.giaSoLoHSD> dsgia = new List<QLBV.DungChung.Ham.giaSoLoHSD>();
            QLBV_Database.QLBVEntities _db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            double thanhtien = 0, soluong = 0, dongia = 0;
            if (gridView2.GetFocusedRowCellValue(col_MaDV) != null)
            {
                if (gridView2.GetFocusedRowCellValue(col_MaDV) != null)
                    madv = Convert.ToInt32(gridView2.GetFocusedRowCellValue(col_MaDV));
                if (gridView2.GetFocusedRowCellValue(col_soluong) != null)
                    soluong = Convert.ToInt32(gridView2.GetFocusedRowCellValue(col_soluong));
                double soluongtong = soluong;
                if (gridView2.GetFocusedRowCellValue(col_dongia) != null)
                    dongia = Convert.ToDouble(gridView2.GetFocusedRowCellValue(col_dongia));
                for (int i = 0; i < gridView2.RowCount; i++)
                {
                    if (gridView2.GetRowCellValue(i, col_MaDV) != null && gridView2.GetRowCellValue(i, colIDDonct) != null && gridView2.GetFocusedRowCellValue(col_MaDV) != null)
                    {
                        if (Convert.ToInt32(gridView2.GetRowCellValue(i, colIDDonct)) <= 0 && Convert.ToInt32(gridView2.GetRowCellValue(i, col_MaDV)) == Convert.ToInt32(gridView2.GetFocusedRowCellValue(col_MaDV)) && i != e.FocusedRowHandle)
                            soluongtong += Convert.ToInt32(gridView2.GetRowCellValue(i, col_soluong));
                    }
                }
                if (gridView2.GetFocusedRowCellValue(colMien) != null)
                    _mien = Convert.ToInt32(gridView2.GetFocusedRowCellValue(colMien));
                if (soluong > 0 && dongia > 0)
                {
                    thanhtien = _mien == 1 ? 0 : (soluong * dongia);
                    gridView2.SetFocusedRowCellValue(col_thanhtien, thanhtien);
                }
                int iddonct = 0;
                if (gridView2.GetFocusedRowCellValue(colIDDonct) != null)
                    iddonct = Convert.ToInt32(gridView2.GetFocusedRowCellValue(colIDDonct));
                int status = -1;
                if (gridView2.GetFocusedRowCellValue(colStatus) != null)
                    status = Convert.ToInt32(gridView2.GetFocusedRowCellValue(colStatus));
                if (rbtn_kieuke.SelectedIndex == 1)
                {
                    dsgia = QLBV.DungChung.Ham._getDSGia(_db, madv, makho);
                    // dongia = dsgia.First().Gia;//_getGiaSD(_Data, madv, 0, 0, 2, makho);
                    if (dsgia.Count() == 0 || dsgia.First().Gia <= 0)
                        MessageBox.Show("Thuốc chưa có giá trong DS Đơn Giá!");
                    gridView2.SetFocusedRowCellValue(col_bhyt, 0);
                }
                else
                {
                    //dongia = getGiaKD(madv, makp, makho);
                    dsgia = QLBV.DungChung.Ham._getDSGia(_db, madv, makho);
                    if (dsgia.Count > 0)
                        dongia = dsgia.First().Gia;
                    if (_kebosung || _bosungNgtru)
                    {
                        if (_benhnhan != null && _benhnhan.DTuong == "BHYT")
                        {
                            var qbh = _lthuocVT.Where(p => p.MaDV == madv).Where(p => p.TrongDM == 1).ToList();
                            if (qbh.Count > 0)
                                gridView2.SetFocusedRowCellValue(col_bhyt, 1);//trường hợp kê độc lập có thanh toán
                            else
                            {
                                qbh = _lthuocVT.Where(p => p.MaDV == madv).Where(p => p.TrongDM == 0).ToList();
                                if (qbh.Count > 0)
                                {
                                    gridView2.SetFocusedRowCellValue(col_bhyt, 0);
                                }
                                else
                                {
                                    gridView2.SetFocusedRowCellValue(col_bhyt, 2);
                                }
                            }
                        }
                        else
                            gridView2.SetFocusedRowCellValue(col_bhyt, 0);//trường hợp kê độc lập có thanh toán
                    }
                    else
                        gridView2.SetFocusedRowCellValue(col_bhyt, 2);

                }
                if (rbtn_kieuke.SelectedIndex == 0)
                {

                    if (iddonct <= 0)
                    {
                        double ton = 0;
                        if (dsgia.Count > 0)
                            ton = dsgia.First().SoLuong - soluongtong;
                        grvdonthuoc.Text = "Số lượng tồn: " + ton.ToString();
                        if (soluong != 0 && (dsgia.Count <= 0 || soluongtong > dsgia.First().SoLuong))
                        {
                            MessageBox.Show("Số lượng trong kho không đủ");
                            gridView2.SetFocusedRowCellValue(col_soluong, "0");
                            gridView2.FocusedColumn = gridView2.VisibleColumns[2];
                        }
                    }
                    else
                    {
                        var soluongdk = _Data.DThuoccts.Where(p => p.IDDonct == iddonct).Select(p => p.SoLuong).FirstOrDefault();
                        double ton = soluongdk;
                        if (dsgia.Count > 0)
                            ton = dsgia.First().SoLuong + ton;
                        grvdonthuoc.Text = "Số lượng tồn: " + ton.ToString();
                        if (status == 0)
                        {
                            if (soluongtong != 0 && (dsgia.Count <= 0 || (soluong > ton)))
                            {
                                MessageBox.Show("Số lượng trong kho không đủ");
                                gridView2.SetFocusedRowCellValue(col_soluong, "0");
                                gridView2.FocusedColumn = gridView2.VisibleColumns[2];
                            }
                        }

                    }
                }
                else if (rbtn_kieuke.SelectedIndex == 1 || rbtn_kieuke.SelectedIndex == 3)
                {
                    List<Ham.giaSoLoHSD> tongSL = new List<Ham.giaSoLoHSD>();
                    double SLKe = 0;//sl kê hiện tại
                    for (int i = 0; i <= gridView2.RowCount; i++)
                    {

                        if (i != e.FocusedRowHandle && gridView2.GetRowCellValue(i, col_MaDV) != null && gridView2.GetRowCellValue(i, col_soluong) != null && gridView2.GetRowCellValue(i, col_MaDV).ToString() != "" && gridView2.GetRowCellValue(i, col_soluong).ToString() != "")
                        {
                            if (Convert.ToDouble(gridView2.GetRowCellValue(i, col_MaDV)) == madv)
                            {
                                Ham.giaSoLoHSD moi = new Ham.giaSoLoHSD();
                                moi.SoLuong = Convert.ToDouble(gridView2.GetRowCellValue(i, col_soluong));
                                if (gridView2.GetRowCellValue(i, col_dongia) != null && gridView2.GetRowCellValue(i, col_dongia).ToString() != "")
                                    moi.Gia = Convert.ToDouble(gridView2.GetRowCellValue(i, col_dongia));
                                if (gridView2.GetRowCellValue(i, colSoLo) != null)
                                    moi.SoLo = gridView2.GetRowCellValue(i, colSoLo).ToString();
                                if (gridView2.GetRowCellValue(i, colHanDung) != null && gridView2.GetRowCellValue(i, colHanDung).ToString() != "")
                                    moi.HanDung = Convert.ToDateTime(gridView2.GetRowCellValue(i, colHanDung));
                                tongSL.Add(moi);
                            }
                        }
                    }
                    if (gridView2.GetRowCellValue(e.FocusedRowHandle, col_soluong) != null && gridView2.GetRowCellValue(e.FocusedRowHandle, col_soluong).ToString() != "")
                        SLKe = Convert.ToDouble(gridView2.GetRowCellValue(e.FocusedRowHandle, col_soluong));

                    dsgia = QLBV.DungChung.Ham._getDSGia(_db, madv, makho);
                    dsgiaHT = LayDSGiaHT(tongSL, dsgia, SLKe, 0);
                    double SlTondg1 = 0;
                    if (dsgia.Count > 0)
                    {
                        SlTondg1 = dsgia.First().SoLuong;
                    }
                    if (iddonct <= 0)
                    {
                        double ton = 0;
                        if (dsgia.Count > 0)
                            ton = SlTondg1 - soluongtong;
                        grvdonthuoc.Text = "Số lượng tồn: " + ton.ToString();
                        if (soluong != 0 && soluongtong > SlTondg1)//DungChung.Bien.SoLuongTon)
                        {
                            MessageBox.Show("Số lượng trong kho không đủ");
                            gridView2.SetFocusedRowCellValue(col_soluong, "0");
                            gridView2.FocusedColumn = gridView2.VisibleColumns[2];
                        }
                    }
                    else
                    {
                        var soluongdk = _Data.DThuoccts.Where(p => p.IDDonct == iddonct).Select(p => p.SoLuong).FirstOrDefault();
                        grvdonthuoc.Text = "Số lượng tồn: " + (SlTondg1 + soluongdk - soluong).ToString();
                        //if (status == 0)
                        //{
                        if (soluong != 0 && soluong > (SlTondg1 + soluongdk))
                        {
                            grvdonthuoc.Text = "Số lượng tồn: " + SlTondg1.ToString();
                            MessageBox.Show("Số lượng trong kho không đủ");
                            gridView2.SetFocusedRowCellValue(col_soluong, "0");
                            gridView2.FocusedColumn = gridView2.VisibleColumns[2];
                        }
                        //}
                    }
                }
            }
        }

        private void grvDonThuocdt_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if(grvDonThuocdt.GetFocusedRowCellValue(colIDDon) != null)
            {
                selectedIdDon = Convert.ToInt32(grvDonThuocdt.GetFocusedRowCellValue(colIDDon));

                _lkp = _Data.KPhongs.ToList();

                lup_khoake.Properties.DataSource = _lkp.Where(p => p.PLoai == "Phòng khám" || p.PLoai == "Cận lâm sàng" || p.PLoai == "Lâm sàng").ToList();
                _lcb = _Data.CanBoes.ToList();
                if (lup_khoake.EditValue != null)
                    _makp = Convert.ToInt32(lup_khoake.EditValue);
                string _makpsd = ";" + _makp + ";";

                if (DungChung.Bien.MaBV != "27022" && DungChung.Bien.MaBV != "24012" && DungChung.Bien.MaBV != "24297" && DungChung.Bien.MaBV != "24272")
                {
                    lup_bske.Properties.DataSource = _lcb.Where(p => p.MaKP == _makp).ToList();
                }
                else
                    lup_bske.Properties.DataSource = _lcb.Where(p => p.Status == 1).Where(p => p.MaKPsd.Contains(_makpsd)).Where(p => p.CapBac != null).Where(p => p.CapBac.ToLower().Contains("bs") || p.CapBac.ToLower().Contains("bác sĩ") || p.CapBac.ToLower().Contains("bác sỹ") || p.CapBac.ToLower().Contains("ys") || p.CapBac.ToLower().Contains("y sĩ") || p.CapBac.ToLower().Contains("y sỹ") || p.CapBac.ToLower().Contains("giáo sư") || p.CapBac.ToLower().Contains("gs") || p.CapBac.ToLower().Contains("tiến sĩ") || p.CapBac.ToLower().Contains("ts")).ToList();
                if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24297")
                {
                    string MKP = _makp.ToString();
                    lup_khoxuat.Properties.DataSource = (_lkp.Where(p => (p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc || (p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc && (p.MaKPsd != null ? p.MaKPsd.Contains(MKP) : false))) && p.Status == 1 && p.MaBVsd == DungChung.Bien.MaBV)).ToList();
                }

                else if (rbtn_kieuke.SelectedIndex == 0)
                {
                    lup_khoxuat.Properties.DataSource = _lkp.Where(p => (p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc && p.NhomKP == _makp) && p.Status == 1).ToList();
                }
                else
                    lup_khoxuat.Properties.DataSource = _lkp.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc && p.Status == 1).ToList();

                var dthuoc = _Data.DThuocs.Where(p => p.IDDon == selectedIdDon).ToList();
                _ldt1 = _Data.DThuoccts.Where(p => p.IDDon == selectedIdDon).ToList();

                if(dthuoc.Count() > 0)
                {
                    lup_ngayke.EditValue = dthuoc.First().NgayKe;
                    lup_khoake.EditValue = dthuoc.First().MaKP;
                    lup_khoxuat.EditValue = dthuoc.First().MaKXuat;
                    lup_bske.EditValue = dthuoc.First().MaCB;
                    txtIDDon.Text = selectedIdDon.ToString();

                    if(dthuoc.First().PLDV.Value == 7)
                    {
                        rbtn_kieuke.SelectedIndex = 0;
                    }
                    else if (dthuoc.First().PLDV.Value == 6)
                    {
                        rbtn_kieuke.SelectedIndex = 1;
                    }
                    else if (dthuoc.First().PLDV.Value == -1)
                    {
                        rbtn_kieuke.SelectedIndex = 2;
                    }
                    else
                    {
                        rbtn_kieuke.SelectedIndex = 3;
                    }
                }

                DsDonThuoc = (from dt in _ldt1
                              join dv in _Data.DichVus.Where(p => p.PLoai == 1) on dt.MaDV equals dv.MaDV
                              select new Dtct
                              {
                                  MaDV = dt.MaDV,
                                  SoLuong = dt.SoLuong,
                                  DonVi = dt.DonVi,
                                  DonGia = dt.DonGia,
                                  TrongBH = dt.TrongBH,
                                  DuongD = dt.DuongD,
                                  SoLan = dt.SoLan,
                                  MoiLan = dt.MoiLan,
                                  Luong = dt.Luong,
                                  DviUong = dt.DviUong,
                                  GhiChu = dt.GhiChu,
                                  TyLeTT = dt.TyLeTT,
                                  ThanhTien = dt.ThanhTien,
                                  IDDonct = dt.IDDonct,
                                  SoLo = dt.SoLo,
                                  HanDung = dt.HanDung,
                                  ThanhToan = dt.ThanhToan,
                              }).ToList();

                bindingSource1.DataSource = DsDonThuoc;
                grCPdinhkem.DataSource = bindingSource1;
                anhien(true);
            }
            
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isDonMoi = 1;
            grcDonThuocdt.Enabled = false;
            lup_ngayke.EditValue = DateTime.Now;
            lup_khoxuat.EditValue = null;
            lup_bske.EditValue = null;
            lup_khoake.EditValue = null;
            var _kdngoai = _Data.HThong_User.Where(p => p.TenDN == DungChung.Bien.TenDN).ToList();
            if (_kdngoai.Count() > 0)
            {
                lup_khoxuat.EditValue = _kdngoai.First().MaKhoKDNgoai;
            }
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
                lup_khoake.Properties.ReadOnly = false;
            else
                lup_khoake.Properties.ReadOnly = true;
            if (_makp > 0)
                lup_khoake.EditValue = _makp;
            else
                lup_khoake.EditValue = DungChung.Bien.MaKP;
            if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24297")
            {
                string MKP = _makp.ToString();
                lup_khoxuat.Properties.DataSource = (_lkp.Where(p => (p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc || (p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc && (p.MaKPsd != null ? p.MaKPsd.Contains(MKP) : false))) && p.Status == 1 && p.MaBVsd == DungChung.Bien.MaBV)).ToList();
            }

            else
            {
                if (rbtn_kieuke.SelectedIndex == 0)
                {
                    lup_khoxuat.Properties.DataSource = _lkp.Where(p => (p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc && p.NhomKP == _makp)).Where(p => p.MaBVsd == DungChung.Bien.MaBV && p.Status == 1).ToList();
                }
                else
                    lup_khoxuat.Properties.DataSource = _lkp.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc).Where(p => p.MaBVsd == DungChung.Bien.MaBV && p.Status == 1).ToList();
            }

            if (DungChung.Bien.MaBV != "27022" && DungChung.Bien.MaBV != "24012" && DungChung.Bien.MaBV != "24272")
            {
                lup_bske.Properties.DataSource = _lcb.Where(p => p.MaKP == _makp).ToList();
            }
            else
            {
                string _makpsd = ";" + _makp + ";";
                lup_bske.Properties.DataSource = _lcb.Where(p => p.Status == 1).Where(p => p.MaKPsd.Contains(_makpsd)).Where(p => p.CapBac != null).Where(p => p.CapBac.ToLower().Contains("bs") || p.CapBac.ToLower().Contains("bác sĩ") || p.CapBac.ToLower().Contains("bác sỹ") || p.CapBac.ToLower().Contains("ys") || p.CapBac.ToLower().Contains("y sĩ") || p.CapBac.ToLower().Contains("y sỹ") || p.CapBac.ToLower().Contains("giáo sư") || p.CapBac.ToLower().Contains("gs") || p.CapBac.ToLower().Contains("tiến sĩ") || p.CapBac.ToLower().Contains("ts")).ToList();
            }

            while (gridView2.RowCount > 1)
            {
                gridView2.DeleteRow(0);
            }

            anhien(false);
        }

        private void grvDonThuocdt_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            grvDonThuocdt_FocusedRowChanged(null, null);
        }

        private void gridView2_ShowingEditor(object sender, CancelEventArgs e)
        {
            var row = (Dtct)gridView2.GetFocusedRow();
            if (row != null)
            {
                if (row.ThanhToan == 1)
                    e.Cancel = true;
            }
        }

        private void gridView2_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            var row = (Dtct)gridView2.GetRow(e.RowHandle);
            if (row != null)
            {
                if (row.ThanhToan == 1)
                    e.Appearance.ForeColor = Color.Red;
            }
        }
        private void UP_tenthuoc_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                if (_rv != null)
                {
                    MessageBox.Show("Bệnh nhân đã ra viện, bạn không được xóa!");
                    return;
                }
                if (statusLuu < 1)
                {
                    MessageBox.Show("Nhấn 'sửa' trước khi xóa!");
                    return;
                }
                var row = (Dtct)gridView2.GetFocusedRow();
                if (row == null)
                    return;

                var dthuocct = _Data.DThuoccts.FirstOrDefault(o => o.IDDonct == row.IDDonct);
                if (dthuocct != null && dthuocct.ThanhToan == 1)
                {
                    MessageBox.Show("Thuốc/vật tư đã thu trực tiếp không thể xóa!");
                    return;
                }

                DialogResult _result = MessageBox.Show("Bạn có muốn xóa thuốc đã kê?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_result == DialogResult.Yes)
                {
                    int iddonct = 0;
                    if (gridView2.GetFocusedRowCellValue(colIDDonct) != null)
                        iddonct = Convert.ToInt32(gridView2.GetFocusedRowCellValue(colIDDonct));
                    if (iddonct > 0)
                    {
                        var xoa = _Data.DThuoccts.Where(p => p.IDDonct == iddonct).FirstOrDefault();
                        if (xoa != null)
                            _Data.DThuoccts.Remove(xoa);
                        _Data.SaveChanges();
                        gridView2.DeleteSelectedRows();
                    }
                    else
                    {
                        gridView2.DeleteSelectedRows();
                    }

                }
            }
        }
        /// <summary>
        /// Gộp lại những giá giống nhau
        /// </summary>
        /// <param name="_Rlist"></param>
        /// <returns></returns>
        public List<Ham.giaSoLoHSD> RefreshList(List<Ham.giaSoLoHSD> _Rlist)
        {
            List<Ham.giaSoLoHSD> _FreshList = new List<Ham.giaSoLoHSD>();
            Ham.giaSoLoHSD tem = new Ham.giaSoLoHSD();

            foreach (var a in _Rlist)
            {
                if (_FreshList.Count == 0)
                    _FreshList.Add(a);
                else
                {
                    tem = _FreshList.Last();
                    if (a.Gia == tem.Gia && a.SoLo == tem.SoLo && a.HanDung == tem.HanDung)
                        tem.SoLuong = tem.SoLuong + a.SoLuong;
                    else
                        _FreshList.Add(a);
                }
            }

            return _FreshList;
        }
        
        /// <summary>
         /// 
         /// </summary>
         /// <param name="tongSL">Tổng số lượng đã kê trong 1 đơn, trừ số lượng dòng hiện tại</param>
         /// <param name="dsgia">danh sách giá, số lượng tồn của mã dịch vụ hiện tại</param>
         /// <param name="soluongke">số lượng kê trong dòng hiện tại</param>
         /// <returns></returns>
        private List<Ham.giaSoLoHSD> LayDSGiaHT(List<Ham.giaSoLoHSD> tongSL, List<Ham.giaSoLoHSD> dsgia, double soluongke, double gia)
        {
            List<Ham.giaSoLoHSD> _Rlist = new List<Ham.giaSoLoHSD>();
            if (gia == 0)// kê số lượng mới
            {
                foreach (var a in tongSL)
                {
                    foreach (var b in dsgia)
                    {
                        if (a.Gia == b.Gia && a.SoLo == b.SoLo && a.HanDung == b.HanDung)
                        {
                            if (a.SoLuong <= b.SoLuong)
                            {
                                b.SoLuong = b.SoLuong - a.SoLuong;
                                a.SoLuong = 0;

                                break;
                            }
                            else
                            {
                                a.SoLuong = a.SoLuong - b.SoLuong;
                                b.SoLuong = 0;
                            }
                        }
                    }
                }

                dsgia = dsgia.Where(p => p.SoLuong != 0).ToList();
                foreach (var a in dsgia)
                {
                    QLBV.DungChung.Ham.giaSoLoHSD moi = new QLBV.DungChung.Ham.giaSoLoHSD();
                    moi.Gia = a.Gia;
                    moi.HanDung = a.HanDung;
                    moi.SoLo = a.SoLo;
                    if (soluongke <= a.SoLuong)
                    {
                        if (soluongke == 0) // TH mới chọn dịch vụ
                            moi.SoLuong = a.SoLuong;
                        else
                            moi.SoLuong = soluongke;
                        _Rlist.Add(moi);
                        break;
                    }
                    else
                    {
                        soluongke = soluongke - a.SoLuong;
                        moi.SoLuong = a.SoLuong;
                        _Rlist.Add(moi);
                    }
                }
            }
            else// sửa số lượng
            {
                foreach (var a in tongSL)
                {
                    foreach (var b in dsgia)
                    {
                        if (a.Gia == b.Gia && a.SoLo == b.SoLo && a.HanDung == b.HanDung)
                        {
                            if (a.SoLuong <= b.SoLuong)
                            {
                                b.SoLuong = b.SoLuong - a.SoLuong;
                                a.SoLuong = 0;

                                break;
                            }
                            else
                            {
                                a.SoLuong = a.SoLuong - b.SoLuong;
                                b.SoLuong = 0;
                            }
                        }
                    }
                }

                dsgia = dsgia.Where(p => p.SoLuong != 0).ToList();
                foreach (var a in dsgia)
                {
                    if (a.Gia == gia)
                    {
                        QLBV.DungChung.Ham.giaSoLoHSD moi = new QLBV.DungChung.Ham.giaSoLoHSD();
                        moi.Gia = a.Gia;
                        moi.HanDung = a.HanDung;
                        moi.SoLo = a.SoLo;
                        if (soluongke <= a.SoLuong)
                        {
                            if (soluongke == 0) // TH mới chọn dịch vụ
                                moi.SoLuong = a.SoLuong;
                            else
                                moi.SoLuong = soluongke;
                            _Rlist.Add(moi);
                            a.SoLuong = a.SoLuong - soluongke;
                            soluongke = 0;//đã đủ thuốc kê đơn
                            break;
                        }
                        else
                        {
                            soluongke = soluongke - a.SoLuong;
                            moi.SoLuong = a.SoLuong;
                            a.SoLuong = 0;
                            _Rlist.Add(moi);
                        }
                    }
                }
                if (soluongke > 0)
                {
                    dsgia = dsgia.Where(p => p.SoLuong != 0).ToList();
                    foreach (var a in dsgia)
                    {

                        QLBV.DungChung.Ham.giaSoLoHSD moi = new QLBV.DungChung.Ham.giaSoLoHSD();
                        moi.Gia = a.Gia;
                        moi.HanDung = a.HanDung;
                        moi.SoLo = a.SoLo;
                        if (soluongke <= a.SoLuong)
                        {
                            if (soluongke == 0) // TH mới chọn dịch vụ
                                moi.SoLuong = a.SoLuong;
                            else
                                moi.SoLuong = soluongke;
                            _Rlist.Add(moi);
                            break;
                        }
                        else
                        {
                            soluongke = soluongke - a.SoLuong;
                            moi.SoLuong = a.SoLuong;
                            _Rlist.Add(moi);
                        }

                    }
                }

            }
            _Rlist = _Rlist.Where(p => p.SoLuong > 0).ToList();
            return RefreshList(_Rlist);

        }
    }
}