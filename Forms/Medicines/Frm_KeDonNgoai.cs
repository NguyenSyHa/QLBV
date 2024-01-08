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
using QLBV.Providers.StoredProcedure;
using QLBV.Providers.Business.Medicines;
using QLBV.Models.Dictionaries.KPhongs;
using DevExpress.XtraGrid.Views.Grid;
using QLBV.Models.Dictionaries.Thuoc;

namespace QLBV.Forms.Medicines
{
    public partial class Frm_KeDonNgoai : DevExpress.XtraEditors.XtraForm
    {
        int _mabn = 0, _idcd = 0, _makp = 0;
        bool _kebosung = false, _bosungNgtru = false;
        string _tenPhieu = "";
        int ppxuat = -1;
        int isDonMoi;

        #region biến dùng để update tồn ở bảng MedicineList
        int maKhoaKe = 0;
        int TH = -1;
        int maDV = 0;
        int idThuoc = 0;
        double donGia = 0;
        string soLo = "";
        DateTime hanDung = new DateTime();
        double slKe = 0;
        #endregion


        List<int> deleteDThuoccts = new List<int>();

        QLBVEntities _dataContext;
        private readonly ExcuteStoredProcedureProvider _excuteStoredProcedureProvider;
        private readonly MedicinesProvider _medicinesProvider;

        public Frm_KeDonNgoai(int mabn, int idcd, int makp, bool donbs)
        {
            InitializeComponent();
            //QLBV_Database.Common.EntityDbContext.RefreshDbContext();
            _dataContext = QLBV_Database.Common.EntityDbContext.DbContext;
            _excuteStoredProcedureProvider = new ExcuteStoredProcedureProvider();
            _medicinesProvider = new MedicinesProvider();
            _mabn = mabn;
            _idcd = idcd;
            _makp = makp;
            _bosungNgtru = donbs;
            _benhnhan = _dataContext.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mabn"></param>
        /// <param name="idcd"></param>
        /// <param name="makp"></param>
        /// <param name="kebosung">kê bổ sung: true: kê đơn thuốc, vật tư độc lập, không theo gói hoặc đính kèm dịch vụ nào </param>
        public Frm_KeDonNgoai(int mabn, int idcd, int makp, bool kebosung, string tenPhieu)
        {
            InitializeComponent();

            //QLBV_Database.Common.EntityDbContext.RefreshDbContext();
            _dataContext = QLBV_Database.Common.EntityDbContext.DbContext;
            _excuteStoredProcedureProvider = new ExcuteStoredProcedureProvider();
            _medicinesProvider = new MedicinesProvider();

            _mabn = mabn;
            _idcd = idcd;
            _makp = makp;
            _kebosung = kebosung;
            _benhnhan = _dataContext.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
            _bnkb = _dataContext.BNKBs.FirstOrDefault(p => p.MaBNhan == _mabn && p.MaKPdt == makp);
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
            grvDonThuocct.OptionsBehavior.Editable = !ah;
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

                var bnkb = _dataContext.BNKBs.Where(p => p.MaBNhan == _mabn && p.MaKP == makhoa).FirstOrDefault();
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

            var dataSource = ((List<DThuocctModel>)bindingSource1.DataSource).Where(o => o.SoLuong == 0).ToList();
            if (dataSource.Count > 0)
            {
                MessageBox.Show(string.Format("Thuốc/vật tư có mã: {0} số lượng phải lớn hơn 0", string.Join(", ", dataSource.Select(o => o.MaDV))));
                return false;
            }

            bool checkton = true;
            string msg = "Các thuốc có số lượng tồn không đủ: ";
            foreach (var item in UP_tenthuoc.DataSource as List<MedicineInventoryModel>)
            {
                if (item.TonKhaDung != item.TonHienTai)
                {
                    if (!_medicinesProvider.IsDuThuoc(_makho, (int)item.MaDV, item.DonGia, item.SoLo, (DateTime)item.HanDung, item.TonKhaDung - item.TonHienTai))
                    {
                        msg += item.TenDV + " ; ";
                        checkton = false;
                    }
                }
            }
            if (!checkton)
            {
                msg += "\nVui lòng kê thuốc khác.";
                MessageBox.Show(msg, "Thuốc không đủ tồn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        List<DichVuTheoKhoXuat> _ldv = new List<DichVuTheoKhoXuat>();
        List<DichVu> _lthuocVT = new List<DichVu>();// tất cả thuốc vật tư
        List<KPhong> _lkho = new List<KPhong>();
        List<KPhong> _lkp = new List<KPhong>();
        List<CanBo> _lcb = new List<CanBo>();
        IList<MedicineInventoryModel> allMedicines = new List<MedicineInventoryModel>();

        IList<MedicineInventoryModel> medicinesByRoom = new List<MedicineInventoryModel>();
        RaVien _rv = new RaVien();
        int _makho = 0;
        string _macb = "";
        int iddonct = -10;
        int iddonkem = -1;
        int selectedIdDon = 0;
        BenhNhan _benhnhan = new BenhNhan();
        BNKB _bnkb = new BNKB();

        private void Frm_KeDonNgoai_Load(object sender, EventArgs e)
        {
            lup_khoxuat.Properties.AllowFocused = false;
            lup_bske.Properties.AllowFocused = false;

            grcDonThuoc.Enabled = true;
            isDonMoi = 0;

            var dThuocs = _medicinesProvider.GetDThuocsForKeDonNgoai(_mabn).OrderBy(p => p.IDDon);

            grcDonThuoc.DataSource = dThuocs;

            connect = Program._connect;
            statusLuu = 0;
            iddonkem = 0;
            _lthuocVT = _dataContext.DichVus.Where(p => p.PLoai == 1).ToList();
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
                    if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24297")
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
            _lkp = _dataContext.KPhongs.ToList();

            DThuoc dthuockem = new DThuoc();
            //lấy iddtct của cls đc chỉ định
            _rv = _dataContext.RaViens.Where(p => p.MaBNhan == _mabn).FirstOrDefault();

            if (rbtn_kieuke.SelectedIndex == 0)// dùng khi kê VTYT đính kèm
            {
                //btnin.Enabled = false;
                if (!_kebosung)
                {
                    var dsid = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn)
                                join dtct in _dataContext.DThuoccts.Where(p => p.IDCD == _idcd) on dt.IDDon equals dtct.IDDon
                                select new { dtct.IDDonct, dtct.NgayNhap, dtct.MaKP }).FirstOrDefault();
                    if (dsid != null)
                    {
                        lup_ngayke.Properties.ReadOnly = true;
                        iddonct = dsid.IDDonct;
                        anhien(false);
                        _ldt1 = _dataContext.DThuoccts.Where(p => p.AttachIDDonct == iddonct).ToList();
                        if (_ldt1.Count > 0)
                            iddonkem = _ldt1.First().IDDon ?? 0;
                        dthuockem = _dataContext.DThuocs.Where(p => p.IDDon == iddonkem).FirstOrDefault();
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
                    var dsid = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn)
                                join dtct in _dataContext.DThuoccts.Where(p => p.MaKP == _makp && (p.AttachIDDonct == null || p.AttachIDDonct == 0)) on dt.IDDon equals dtct.IDDon
                                select new { dtct.IDDonct, dt.IDDon, dtct.NgayNhap, dtct.MaKP }).FirstOrDefault();
                    if (dsid != null)
                    {
                        lup_ngayke.Properties.ReadOnly = true;
                        anhien(false);
                        _ldt1 = _dataContext.DThuoccts.Where(p => p.IDDon == dsid.IDDon).ToList();
                        iddonkem = dsid.IDDon;

                        if (dsid.NgayNhap != null)
                            lup_ngayke.DateTime = dsid.NgayNhap.Value;
                    }
                    dthuockem = _dataContext.DThuocs.Where(p => p.IDDon == iddonkem).FirstOrDefault();
                }
            }
            else if (rbtn_kieuke.SelectedIndex == 1)
            { // kê đơn dịch vụ
                dthuockem = _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn && p.KieuDon == 6).FirstOrDefault();
                if (dthuockem != null)
                    iddonkem = dthuockem.IDDon;
                _ldt1 = _dataContext.DThuoccts.Where(p => p.IDDon == iddonkem).ToList();
            }
            else if (rbtn_kieuke.SelectedIndex == 3)
            { // kê đơn bổ sung
                dthuockem = _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn && p.KieuDon == 1).FirstOrDefault();
                if (dthuockem != null)
                    iddonkem = dthuockem.IDDon;
                _ldt1 = _dataContext.DThuoccts.Where(p => p.IDDon == iddonkem).ToList();
            }
            else
            {
                dthuockem = _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn && p.KieuDon == -1).FirstOrDefault();
                if (dthuockem != null)
                    iddonkem = dthuockem.IDDon;
                _ldt1 = _dataContext.DThuoccts.Where(p => p.IDDon == iddonkem).ToList();
            }


            _ldt1 = _dataContext.DThuoccts.Where(p => p.IDDon == selectedIdDon).ToList();

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
            DsDonThuoc = _medicinesProvider.ViewInfoMedicineDThuoc(selectedIdDon);


            bindingSource1.DataSource = DsDonThuoc;
            grcDonThuocct.DataSource = bindingSource1;



            // load ds khoa kê đơn
            lup_khoake.Properties.DataSource = _excuteStoredProcedureProvider.ExcuteStoredProcedure<KPhongModel>("sp_RoomList",
                                                                                                                    new Dictionary<string, string>()
                                                                                                                    {
                                                                                                                        { "@MaCB", DungChung.Bien.MaCB },
                                                                                                                        { "@viewKP", "1" }
                                                                                                                    });

            lup_khoxuat.Properties.DataSource = _excuteStoredProcedureProvider.ExcuteStoredProcedure<KPhongModel>("sp_RoomList",
                                                                                                                    new Dictionary<string, string>()
                                                                                                                    {
                                                                                                                        { "@MaCB", DungChung.Bien.MaCB },
                                                                                                                        { "@viewKP", "0" }
                                                                                                                    });


            //rbtn_kieuke.Properties.ReadOnly = true;
            if (_rv != null)
            {
                MessageBox.Show("bệnh nhân đã ra viện!");
                anhien(true);
            }
            lup_khoxuat_EditValueChanged(sender, e);
            txtIDDon.Text = iddonkem.ToString();

            if (DungChung.Bien.MaBV == "24297")
            {
                col_bhyt.OptionsColumn.AllowEdit = false;
            }

            allMedicines = (from a in _dataContext.DichVus
                            from b in _dataContext.MedicineLists
                            where a.MaDV == b.MaDV
                            select new MedicineInventoryModel()
                            {
                                IDThuoc = b.IDThuoc,
                                MaDV = a.MaDV,
                                TenDV = a.TenDV
                            }).ToList();
        }

        IList<DThuocctModel> DsDonThuoc = new List<DThuocctModel>();
        IList<DThuocct> _ldt1 = new List<DThuocct>();
        private ConnectData connect;
        List<DichVuTheoKhoXuat> _ldvKD(int makp, int makho)
        {
            connect = Program._connect;
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
                    if (DungChung.Bien.MaBV == "24012")
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
            var qtt = _dataContext.KPhongs.Where(p => p.MaKP == makho).Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc).ToList();
            if (qtt.Count > 0)
            {
                var q = (from dt in _dataContext.DThuocs.Where(p => p.KieuDon == 3 || p.KieuDon == 4 || p.KieuDon == 7).Where(p => p.MaKP == makho)
                         join dtct in _dataContext.DThuoccts.Where(p => p.MaDV == madv).Where(p => p.Status == 1) on dt.IDDon equals dtct.IDDon
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
                var q = (from dt in _dataContext.DThuocs.Where(p => p.KieuDon == 3 || p.KieuDon == 4 || p.KieuDon == 7).Where(p => p.MaKP == makp && p.MaKXuat == makho)
                         join dtct in _dataContext.DThuoccts.Where(p => p.MaDV == madv) on dt.IDDon equals dtct.IDDon
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
        internal static double _getGiaSD(QLBV_Database.QLBVEntities _dataContext, int madv, double dongiaSD, int trongBH, int nhapxuat, int maKP)
        {
            double rs = 0;
            if (nhapxuat == 1)
            {

                rs = dongiaSD;
                var qdongia = _dataContext.DonGiaDVs.Where(p => p.MaDV == madv && p.Status == true).Where(p => (trongBH == 1 && p.DonGiaX_BH == dongiaSD) || (trongBH == 0 && p.DonGiaX_DV == dongiaSD)).Select(p => p.DonGiaN).FirstOrDefault();
                if (qdongia != null)
                {
                    rs = qdongia;
                }

            }
            else // lấy giá xuất
            {
                var qdongia = _dataContext.DonGiaDVs.Where(p => p.MaDV == madv && p.Status == true).Select(p => new { DonGiaX = trongBH == 1 ? p.DonGiaX_BH : p.DonGiaX_DV, p.DonGiaN }).FirstOrDefault();
                if (qdongia != null)
                {
                    rs = qdongia.DonGiaX;

                    var qnd = (from nhap in _dataContext.NhapDs.Where(p => p.MaKP == maKP).OrderByDescending(p => p.NgayNhap)
                               join nhapct in _dataContext.NhapDcts.Where(p => p.MaDV == madv && p.DonGia == qdongia.DonGiaN) on nhap.IDNhap equals nhapct.IDNhap
                               group new { nhapct, nhap } by new { nhapct.MaDV, nhapct.DonGia } into kq
                               select new { kq.Key.DonGia, SoLuong = (kq.Sum(p => p.nhapct.SoLuongN) - kq.Sum(p => p.nhapct.SoLuongX)) }).FirstOrDefault();
                    var q = (from dt in _dataContext.DThuocs.Where(p => p.KieuDon == 6).Where(p => p.MaKXuat == maKP)
                             join dtct in _dataContext.DThuoccts.Where(p => p.MaDV == madv && p.DonGia == rs && p.Status == 0) on dt.IDDon equals dtct.IDDon

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
                string dd = "";
                var ddung = _dataContext.DichVus.FirstOrDefault(p => p.MaDV == madv);
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

        bool isUpdate = true;
        List<MedicineInventoryModel> lupMedicine = new List<MedicineInventoryModel>();
        MedicineInventoryModel selectedMedicine = new MedicineInventoryModel();
        private void grvDonThuocct_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            statusLuu = 1;

            var a = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(col_IDThuoc));

            if (e != null && e.Column != null && e.Column.FieldName != null && sender is GridView gridView && gridView.ActiveEditor != null)
            {
                var oldValue = gridView.ActiveEditor.OldEditValue;
                var newValue = gridView.ActiveEditor.EditValue;

                double oldSL = 0;
                double oldDonGia = 0;
                int oldIDThuoc = 0;
                int newIDThuoc = 0;
                double newSL = 0;
                double newDonGia = 0;
                int idDon = 0;


                switch (e.Column.FieldName)
                {
                    case nameof(DThuocctModel.IDThuoc):
                        {
                            isUpdate = true;

                            newIDThuoc = Convert.ToInt32(newValue);
                            int trongDM = _medicinesProvider.isTrongDMBHYT(_mabn);

                            if (grvDonThuoc.GetFocusedRowCellValue(colIDDon) != null)
                                idDon = Convert.ToInt32(grvDonThuoc.GetFocusedRowCellValue(colIDDon));

                            //lấy chi tiết đơn thuốc
                            lupMedicine = ((List<MedicineInventoryModel>)UP_tenthuoc.DataSource);
                            selectedMedicine = lupMedicine.FirstOrDefault(p => p.IDThuoc == newIDThuoc);

                            if (oldValue != null) // trường hợp sửa tên thuốc
                            {
                                // SL trc khi thay đổi MaDV
                                if (grvDonThuocct.GetFocusedRowCellValue(col_soluong) != null && grvDonThuocct.GetFocusedRowCellValue(col_soluong).ToString() != "")
                                    oldSL = Convert.ToDouble(grvDonThuocct.GetFocusedRowCellValue(col_soluong));
                                oldDonGia = Convert.ToDouble(grvDonThuocct.GetFocusedRowCellValue(col_dongia));

                                oldIDThuoc = Convert.ToInt32(oldValue);

                                _medicinesProvider.EditStockByIDThuoc(UP_tenthuoc, oldSL, 0, oldIDThuoc, 0, oldDonGia, 0, ppxuat);
                            }

                            if (selectedMedicine != null)
                            {
                                isUpdate = false;

                                grvDonThuocct.SetFocusedRowCellValue(col_soluong, 0);
                                grvDonThuocct.SetFocusedRowCellValue(col_DonVi, selectedMedicine.DonVi);
                                grvDonThuocct.SetFocusedRowCellValue(col_dongia, selectedMedicine.DonGia);
                                grvDonThuocct.SetFocusedRowCellValue(col_bhyt, trongDM == 1 ? selectedMedicine.TrongDM : 0);
                                grvDonThuocct.SetFocusedRowCellValue(colDuongDung, selectedMedicine.DuongD);
                                grvDonThuocct.SetFocusedRowCellValue(colLan, 1);
                                grvDonThuocct.SetFocusedRowCellValue(colMoiLan, " lần, mỗi lần ");
                                grvDonThuocct.SetFocusedRowCellValue(colLuong, 1);
                                grvDonThuocct.SetFocusedRowCellValue(colSoLo, selectedMedicine.SoLo);
                                grvDonThuocct.SetFocusedRowCellValue(colHanDung, selectedMedicine.HanDung);
                                grvDonThuocct.SetFocusedRowCellValue(colDonViUong, selectedMedicine.DonVi.Trim());
                                grvDonThuocct.SetFocusedRowCellValue(col_tltt, selectedMedicine.BHTT);
                                grvDonThuocct.SetFocusedRowCellValue(col_ghichu, "");
                                SetTextStock();

                                isUpdate = true;
                            }
                        }
                        break;
                    case nameof(DThuocctModel.SoLuong):
                        {
                            #region
                            // Sửa số lượng thì trừ số lượng tồn trong danh mục tồn thuốc
                            var idThuocObj = gridView.GetRowCellValue(e.RowHandle, col_IDThuoc);
                            var donGiaObj = gridView.GetRowCellValue(e.RowHandle, col_dongia);

                            if (gridView.ActiveEditor.OldEditValue != null && gridView.ActiveEditor.EditValue != null && idThuocObj != null)
                            {
                                newIDThuoc = Convert.ToInt32(idThuocObj);
                                oldSL = Convert.ToDouble(gridView.ActiveEditor.OldEditValue);
                                newSL = Convert.ToDouble(gridView.ActiveEditor.EditValue);
                                newDonGia = Convert.ToDouble(donGiaObj);


                                lupMedicine = ((List<MedicineInventoryModel>)UP_tenthuoc.DataSource);
                                selectedMedicine = lupMedicine.FirstOrDefault(p => p.IDThuoc == newIDThuoc);

                                if (isUpdate)
                                {
                                    if (newSL <= 0)
                                    {
                                        isUpdate = false;
                                        grvDonThuocct.SetRowCellValue(e.RowHandle, col_soluong, oldSL);
                                        MessageBox.Show("Số lượng phải lớn hơn 0", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        isUpdate = true;
                                    }
                                    else if (newSL > (selectedMedicine.TonHienTai + oldSL))
                                    {
                                        isUpdate = false;
                                        grvDonThuocct.SetRowCellValue(e.RowHandle, col_soluong, oldSL);
                                        MessageBox.Show("Số lượng thuốc không đủ", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        isUpdate = true;
                                    }
                                    else
                                    {
                                        _medicinesProvider.EditStockByIDThuoc(UP_tenthuoc, oldSL, newSL, newIDThuoc, newIDThuoc, newDonGia, newDonGia, ppxuat);
                                        this.grvDonThuocct.ViewCaption = "Số lượng tồn: " + selectedMedicine.TonHienTai;

                                        var soLuong = Convert.ToDouble(gridView.GetRowCellValue(e.RowHandle, col_soluong));
                                        var donGia = Convert.ToDouble(gridView.GetRowCellValue(e.RowHandle, col_dongia));
                                        int tylett = 1;
                                        if (grvDonThuocct.GetRowCellValue(e.RowHandle, col_tltt) != null)
                                            tylett = Convert.ToInt32(grvDonThuocct.GetRowCellValue(e.RowHandle, col_tltt));

                                        double thanhTien = Math.Round((double)(soLuong * donGia * tylett / 100), DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero);

                                        grvDonThuocct.SetRowCellValue(e.RowHandle, col_thanhtien, thanhTien);
                                        grvDonThuocct.SetRowCellValue(e.RowHandle, col_soluong, newSL);
                                    }
                                }
                            }
                            #endregion
                        }
                        break;
                }
            }
        }
        private void luudon(int AttachIDDonct, int makp)
        {
            bool isSuccess = true;
            bool luudon = false;
            if (grvDonThuocct.RowCount > 0) // kiểm tra có thuốc mới lưu kê đơn
            {
                if (grvDonThuocct.RowCount == 1)// kiểm tra row đầu tiền có dữ liệu không
                {
                    if (grvDonThuocct.GetRowCellValue(1, colIDDonct) != null && grvDonThuocct.GetRowCellValue(1, colIDDonct).ToString() != "" && Convert.ToInt32(grvDonThuocct.GetRowCellValue(1, colIDDonct)) > 0)
                    {
                        luudon = true;
                    }
                    else
                    {
                        luudon = false;
                    }
                }
                else
                {
                    luudon = true;
                }
            }
            var dThuoc = _dataContext.DThuocs.Where(p => p.IDDon == selectedIdDon).FirstOrDefault();
            int idkb = _dataContext.BNKBs.FirstOrDefault(p => p.MaBNhan == _mabn && p.MaKP == _makp && p.PhuongAn == 4).IDKB;
            if (dThuoc == null || isDonMoi == 1)
            {
                if (luudon)
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
                    _dataContext.DThuocs.Add(dthuoccd);
                    _dataContext.SaveChanges();

                    int IDDon = dthuoccd.IDDon;

                    for (int i = 0; i < grvDonThuocct.RowCount; i++)
                    {
                        float sluong = 0;
                        if (grvDonThuocct.GetRowCellValue(i, col_soluong) != null)
                            sluong = Convert.ToUInt32(grvDonThuocct.GetRowCellValue(i, col_soluong));
                        int madv = 0;
                        int idThuoc = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, col_IDThuoc));
                        if (idThuoc > 0) // && sluong > 0
                        {
                            madv = _medicinesProvider.GetMaDVbyIDThuoc(idThuoc);
                            string donvi = Convert.ToString(grvDonThuocct.GetRowCellValue(i, col_DonVi));


                            double dgia = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, col_dongia));

                            int bh = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, col_bhyt));
                            string ghichu = Convert.ToString(grvDonThuocct.GetRowCellValue(i, col_ghichu));
                            int tltt = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, col_tltt));
                            double tt = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, col_thanhtien));

                            string lan = Convert.ToString(grvDonThuocct.GetRowCellValue(i, colLan));
                            string luong = Convert.ToString(grvDonThuocct.GetRowCellValue(i, colLuong));
                            string moilan = Convert.ToString(grvDonThuocct.GetRowCellValue(i, colMoiLan));
                            string dviUong = Convert.ToString(grvDonThuocct.GetRowCellValue(i, colDonViUong));
                            string ddung = Convert.ToString(grvDonThuocct.GetRowCellValue(i, colDuongDung));
                            DThuocct moi = new DThuocct();
                            moi.MaKP = _makp;
                            moi.MaKXuat = _makho;
                            moi.NgayNhap = lup_ngayke.DateTime;
                            moi.MaDV = madv;
                            moi.DonVi = donvi;
                            moi.DonGia = dgia;
                            moi.SoLuong = sluong;
                            moi.SoLuongct = sluong;
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
                            moi.IDKB = idkb;

                            if (grvDonThuocct.GetRowCellValue(i, colSoLo) != null)
                                moi.SoLo = grvDonThuocct.GetRowCellValue(i, colSoLo).ToString();
                            if (grvDonThuocct.GetRowCellValue(i, colHanDung) != null)
                                moi.HanDung = Convert.ToDateTime(grvDonThuocct.GetRowCellValue(i, colHanDung));
                            if (AttachIDDonct > 0)
                                moi.AttachIDDonct = AttachIDDonct;
                            if (grvDonThuocct.GetRowCellValue(i, colMien) != null)
                                moi.MienCT = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMien).ToString());
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
                                moi.Status = 2;
                            _dataContext.DThuoccts.Add(moi);
                        }
                    }
                }

                if (_dataContext.SaveChanges() > 0)
                {
                    MessageBox.Show("Thêm mới đơn thuốc thành công!", "Thông báo!");


                    //Cập nhật tồn
                    if (_medicinesProvider.isTuTruc(_makho))
                    {
                        foreach (var item in UP_tenthuoc.DataSource as List<MedicineInventoryModel>)
                        {
                            if (item.TonKhaDung != item.TonHienTai)
                            {
                                _medicinesProvider.UpdateMedicineListPPX3((int)item.MaDV, item.DonGia, item.SoLo, (DateTime)item.HanDung, 0, _makho, item.TonKhaDung - item.TonHienTai, 2);
                            }
                        }
                    }
                    else
                    {
                        _medicinesProvider.UpdateMedicineListPPX3(0, 0, "", new DateTime(), 0, _makho, 0, 0);
                    }
                    
                    UP_tenthuoc.DataSource = medicinesByRoom = _medicinesProvider.GetLupMaDuoc(_makho, selectedIdDon, 0);
                }
                else
                {
                    MessageBox.Show("Thêm mới đơn thuốc không thành công!", "Thông báo!");
                }
            }
            else
            {
                if (luudon)
                {
                    dThuoc.NgayKe = lup_ngayke.DateTime;
                    dThuoc.MaBNhan = _mabn;
                    dThuoc.MaKP = _makp;
                    dThuoc.MaKXuat = _makho;
                    dThuoc.MaCB = _macb;
                    _dataContext.SaveChanges();
                    for (int i = 0; i < grvDonThuocct.RowCount; i++)
                    {
                        int iddonct = 0;
                        if (grvDonThuocct.GetRowCellValue(i, colIDDonct) != null)
                            iddonct = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colIDDonct));
                        int madv = 0;
                        if (grvDonThuocct.GetRowCellValue(i, col_IDThuoc) != null)
                        {
                            madv = _medicinesProvider.GetMaDVbyIDThuoc(Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, col_IDThuoc)));
                        }
                        string donvi = Convert.ToString(grvDonThuocct.GetRowCellValue(i, col_DonVi));
                        string lan = Convert.ToString(grvDonThuocct.GetRowCellValue(i, colLan));
                        string luong = Convert.ToString(grvDonThuocct.GetRowCellValue(i, colLuong));
                        string moilan = Convert.ToString(grvDonThuocct.GetRowCellValue(i, colMoiLan));
                        string dviUong = Convert.ToString(grvDonThuocct.GetRowCellValue(i, colDonViUong));
                        double dgia = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, col_dongia));
                        float sluong = Convert.ToUInt32(grvDonThuocct.GetRowCellValue(i, col_soluong));
                        int bh = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, col_bhyt));
                        string ghichu = Convert.ToString(grvDonThuocct.GetRowCellValue(i, col_ghichu));
                        int tltt = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, col_tltt));
                        double tt = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, col_thanhtien));
                        string ddung = Convert.ToString(grvDonThuocct.GetRowCellValue(i, colDuongDung));
                        if (madv > 0 && sluong > 0)
                        {
                            var dtct = _dataContext.DThuoccts.Where(p => p.IDDonct == iddonct).FirstOrDefault();
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
                                moi.SoLuongct = sluong;
                                moi.TrongBH = bh;
                                moi.GhiChu = ghichu;
                                moi.TyLeTT = tltt;
                                moi.ThanhTien = tt;
                                moi.IDDon = dThuoc.IDDon;
                                moi.SoLan = lan;
                                moi.MoiLan = moilan;
                                moi.Luong = luong;
                                moi.DviUong = dviUong;
                                moi.DuongD = ddung;
                                moi.IDKB = idkb;

                                if (grvDonThuocct.GetRowCellValue(i, colSoLo) != null)
                                    moi.SoLo = grvDonThuocct.GetRowCellValue(i, colSoLo).ToString();
                                if (grvDonThuocct.GetRowCellValue(i, colHanDung) != null)
                                    moi.HanDung = Convert.ToDateTime(grvDonThuocct.GetRowCellValue(i, colHanDung));
                                if (grvDonThuocct.GetRowCellValue(i, colMien) != null)
                                    moi.MienCT = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMien).ToString());
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
                                _dataContext.DThuoccts.Add(moi);
                                _dataContext.SaveChanges();
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
                                dtct.SoLuongct = sluong;
                                dtct.TrongBH = bh;
                                dtct.GhiChu = ghichu;
                                dtct.TyLeTT = tltt;
                                dtct.ThanhTien = tt;
                                dtct.IDDon = dThuoc.IDDon;
                                dtct.SoLan = lan;
                                dtct.MoiLan = moilan;
                                dtct.Luong = luong;
                                dtct.DviUong = dviUong;
                                dtct.DuongD = ddung;
                                dtct.IDKB = idkb;

                                if (grvDonThuocct.GetRowCellValue(i, colSoLo) != null)
                                    dtct.SoLo = grvDonThuocct.GetRowCellValue(i, colSoLo).ToString();
                                if (grvDonThuocct.GetRowCellValue(i, colHanDung) != null)
                                    dtct.HanDung = Convert.ToDateTime(grvDonThuocct.GetRowCellValue(i, colHanDung));
                                if (grvDonThuocct.GetRowCellValue(i, colMien) != null)
                                    dtct.MienCT = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMien).ToString());

                                if (AttachIDDonct > 0)
                                    dtct.AttachIDDonct = AttachIDDonct;
                                else
                                    dtct.AttachIDDonct = null;
                            }
                        }
                    }
                }
                else
                {
                    _dataContext.DThuocs.Remove(dThuoc);
                }

                
                
                //Xóa đơn
                if (deleteDThuoccts.Count() > 0)
                {
                    foreach (var item in deleteDThuoccts)
                    {
                        isSuccess = _medicinesProvider.DeleteDThuocAndDThuocctbyIDDonct(item);
                    }
                    deleteDThuoccts.Clear();
                }
                if (_dataContext.SaveChanges() >= 0 || isSuccess)
                {
                    MessageBox.Show("Sửa đơn thuốc thành công!", "Thông báo!");

                    //Cập nhật tồn
                    if (_medicinesProvider.isTuTruc(_makho))
                    {
                        foreach (var item in UP_tenthuoc.DataSource as List<MedicineInventoryModel>)
                        {
                            if (item.TonKhaDung != item.TonHienTai)
                            {
                                _medicinesProvider.UpdateMedicineListPPX3((int)item.MaDV, item.DonGia, item.SoLo, (DateTime)item.HanDung, 0, _makho, item.TonKhaDung - item.TonHienTai, 2);
                            }
                        }
                    }
                    else
                    {
                        _medicinesProvider.UpdateMedicineListPPX3(0, 0, "", new DateTime(), 0, _makho, 0, 0);
                    }

                    UP_tenthuoc.DataSource = medicinesByRoom = _medicinesProvider.GetLupMaDuoc(_makho, selectedIdDon, 0);
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
                Frm_KeDonNgoai_Load(sender, e);

            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            if (_rv != null)
            {
                MessageBox.Show("Bệnh nhân đã ra viện, không thể sửa", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            var qdt = (from dt in _dataContext.DThuocs.Where(p => p.IDDon == selectedIdDon) join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon select new { dtct.Status, dtct.SoPL }).FirstOrDefault();
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

                UP_tenthuoc.DataSource = medicinesByRoom;

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
            _lcb = _dataContext.CanBoes.ToList();

            if (lup_khoake.EditValue != null)
            {
                lup_khoxuat.Properties.AllowFocused = true;
                _makp = Convert.ToInt32(lup_khoake.EditValue);
            }


            string _makpsd = ";" + _makp + ";";

            lup_bske.Properties.DataSource = _lcb.Where(p => p.Status == 1).Where(p => p.MaKPsd.Contains(_makpsd)).Where(p => p.CapBac != null).Where(p => p.CapBac.ToLower().Contains("bs") || p.CapBac.ToLower().Contains("bác sĩ") || p.CapBac.ToLower().Contains("bác sỹ") || p.CapBac.ToLower().Contains("ys") || p.CapBac.ToLower().Contains("y sĩ") || p.CapBac.ToLower().Contains("y sỹ") || p.CapBac.ToLower().Contains("giáo sư") || p.CapBac.ToLower().Contains("gs") || p.CapBac.ToLower().Contains("tiến sĩ") || p.CapBac.ToLower().Contains("ts")).ToList();

        }

        private class DichVuTheoKhoXuat
        {
            public int? MaDV { get; set; }
            public string TenDV { get; set; }
            public string HamLuong { get; set; }
            public string SoLo { get; set; }
            public DateTime? HanDung { get; set; }
        }
        //void loaddsThuoc(int makho, int pl, int makp)
        //{
        //    _ldv.Clear();
        //    if (pl == 0)
        //    {
        //        _ldv = _ldvKD(makp, makho).ToList();
        //        UP_tenthuoc.DataSource = (from dv in _ldv
        //                                  select new DichVuTheoKhoXuat()
        //                                  {
        //                                      SoLo = dv.SoLo,
        //                                      HanDung = dv.HanDung,
        //                                      MaDV = dv.MaDV,
        //                                      TenDV = dv.TenDV,
        //                                      HamLuong = dv.HamLuong
        //                                  }).ToList();
        //    }

        //    else
        //    {
        //        if (ppxuat == 3)
        //        {
        //            var q = (from nd in _dataContext.NhapDs.Where(p => p.PLoai == 1 && p.MaKP == makho)
        //                     join ndct in _dataContext.NhapDcts on nd.IDNhap equals ndct.IDNhap
        //                     join dv in _dataContext.DichVus on ndct.MaDV equals dv.MaDV
        //                     //join dgdv in _dataContext.DonGiaDVs on dv.MaDV equals dgdv.MaDV
        //                     select new { ndct.MaDV, dv.TenDV, dv.HamLuong, ndct.SoLo, ndct.HanDung }).ToList().Distinct().ToList();

        //            UP_tenthuoc.DataSource = (from dv in q
        //                                      select new DichVuTheoKhoXuat()
        //                                      {
        //                                          SoLo = dv.SoLo,
        //                                          HanDung = dv.HanDung,
        //                                          MaDV = dv.MaDV,
        //                                          TenDV = dv.TenDV,
        //                                          HamLuong = dv.HamLuong
        //                                      }).ToList();
        //        }
        //        else
        //        {
        //            var q = (from nd in _dataContext.NhapDs.Where(p => p.PLoai == 1 && p.MaKP == makho)
        //                     join ndct in _dataContext.NhapDcts on nd.IDNhap equals ndct.IDNhap
        //                     join dv in _dataContext.DichVus on ndct.MaDV equals dv.MaDV
        //                     //join dgdv in _dataContext.DonGiaDVs on dv.MaDV equals dgdv.MaDV
        //                     select new { ndct.MaDV, dv.TenDV, dv.HamLuong }).ToList().Distinct().ToList();

        //            UP_tenthuoc.DataSource = (from dv in q
        //                                      select new DichVuTheoKhoXuat()
        //                                      {
        //                                          MaDV = dv.MaDV,
        //                                          TenDV = dv.TenDV,
        //                                          HamLuong = dv.HamLuong
        //                                      }).ToList();
        //        }
        //    }
        //}
        private void lup_khoxuat_EditValueChanged(object sender, EventArgs e)
        {
            if (lup_khoxuat.EditValue != null)
            {
                if (lup_khoxuat.GetColumnValue("PPXuat") != null)
                    ppxuat = Convert.ToInt32(lup_khoxuat.GetColumnValue("PPXuat"));
                _makho = Convert.ToInt32(lup_khoxuat.EditValue);
                lup_bske.Properties.AllowFocused = true;
            }
            if (lup_khoake.EditValue != null)
                _makp = Convert.ToInt32(lup_khoake.EditValue);

            UP_tenthuoc.DataSource = medicinesByRoom = _medicinesProvider.GetLupMaDuoc(_makho, selectedIdDon, 0);
        }

        private void lup_bske_EditValueChanged(object sender, EventArgs e)
        {
            if (lup_bske.EditValue != null)
            {
                _macb = lup_bske.EditValue.ToString();
            }
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
            var qdt = (from dt in _dataContext.DThuocs.Where(p => p.IDDon == selectedIdDon) join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon select new { dtct.Status, dtct.SoPL }).FirstOrDefault();
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

            var xoa = (from dt in _dataContext.DThuocs.Where(p => p.IDDon == selectedIdDon)
                       join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                       select new { dt, dtct }).ToList();
            if (xoa.Where(p => p.dtct.ThanhToan == 1).Count() > 0)
            {
                MessageBox.Show("Thuốc/vật tư đã thu trực tiếp không thể xóa!");
                return;
            }

            DialogResult _result = MessageBox.Show("Bạn có muốn xóa đơn thuốc?", "Hỏi xóa", MessageBoxButtons.YesNo);
            if (_result == DialogResult.Yes)
            {
                var dThuoccts = _dataContext.DThuoccts.Where(p => p.IDDon == selectedIdDon).ToList();// _medicinesProvider.ViewInfoMedicineDThuoc(selectedIdDon);
                if (dThuoccts.Count() > 0)
                {
                    foreach (var s in dThuoccts)
                    {
                        _medicinesProvider.DeleteDThuocAndDThuocctbyIDDonct(s.IDDonct);

                        if (_medicinesProvider.isTuTruc((int)s.MaKXuat))
                            _medicinesProvider.UpdateMedicineListPPX3((int)s.MaDV, s.DonGia, s.SoLo, (DateTime)s.HanDung, 0, (int)s.MaKXuat, -s.SoLuong, 2);

                    }

                    if (!_medicinesProvider.isTuTruc((int)dThuoccts.First().MaKXuat))
                        _medicinesProvider.UpdateMedicineListPPX3(0, 0, "", new DateTime(), 0, (int)dThuoccts.First().MaKXuat, 0, 0);
                }

                UP_tenthuoc.DataSource = medicinesByRoom = _medicinesProvider.GetLupMaDuoc(_makho, selectedIdDon, 0);

                MessageBox.Show("Xóa thành công!");

                Frm_KeDonNgoai_Load(sender, e);

            }
        }

        private void btn_kluu_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn có muốn hủy đơn đang kê ?", "Thông báo !", MessageBoxButtons.YesNo);
            if (kq == DialogResult.Yes)
            {
                Frm_KeDonNgoai_Load(sender, e);
            }
        }

        private void grvDonThuocct_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if (grvDonThuocct.RowCount > 0)
            {
                rbtn_kieuke.Enabled = false;
            }
            else
            {

                rbtn_kieuke.Enabled = true;
                Frm_KeDonNgoai_Load(sender, e);
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
            List<thongke> _lthongke = new List<thongke>();
            frmIn frm = new frmIn();
            BaoCao.RepBTKThuoc_VTTHPhongMo_ rep = new BaoCao.RepBTKThuoc_VTTHPhongMo_();
            var bs = (from cls in _dataContext.CLS.Where(p => p.MaBNhan == _mabn)
                      join cd in _dataContext.ChiDinhs.Where(p => p.IDCD == _idCD) on cls.IdCLS equals cd.IdCLS
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
                var qbn = (from a in _dataContext.BenhNhans.Where(p => p.MaBNhan == mabn) select new { a.TenBNhan, a.Tuoi }).ToList();
                rep.TenBN.Value = qbn.First().TenBNhan.ToUpper();
                if (DungChung.Bien.MaBV == "24012")
                {
                    rep.Tuoi.Value = Convert.ToInt32(DungChung.Ham.TuoitheoThang(_dataContext, _mabn, DungChung.Bien.formatAge_24012));
                }
                else
                    rep.Tuoi.Value = Convert.ToInt32(qbn.First().Tuoi);
                string macbth = bs.First().MaCBth;
                var cb = _dataContext.CanBoes.Where(p => p.MaCB == macbth).FirstOrDefault();
                if (cb != null)
                    rep.BSPT.Value = cb.TenCB;
                if (bs.First().NgayTH != null)
                    rep.NgayKe.Value = "Ngày " + bs.First().NgayTH.Value.Day + " tháng " + bs.First().NgayTH.Value.Month + " năm " + bs.First().NgayTH.Value.Year;
                else
                    rep.NgayKe.Value = "Ngày ......... tháng ........ năm";

            }
            var vv = (from a in _dataContext.VaoViens.Where(p => p.MaBNhan == mabn) select new { a.SoBA }).ToList();
            if (vv.Count() > 0)
            {
                rep.SoHSBN.Value = vv.First().SoBA;
            }
            var dthuocct = _dataContext.DThuoccts.Where(p => p.IDCD == _idCD).FirstOrDefault();
            int iddonct = 0;
            //if (dthuocct != null)
            //    iddonct = dthuocct.IDDonct;
            var dt = (from a in _dataContext.DThuocs.Where(p => p.MaBNhan == mabn && p.MaKP == _makp)
                      join b in _dataContext.DThuoccts.Where(p => iddonct == 0 ? (p.AttachIDDonct == 0 || p.AttachIDDonct == null) : p.AttachIDDonct == iddonct) on a.IDDon equals b.IDDon
                      join c in _dataContext.DichVus on b.MaDV equals c.MaDV
                      join d in _dataContext.TieuNhomDVs.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 10 || p.IDNhom == 11) on c.IdTieuNhom equals d.IdTieuNhom
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
                if (grvDonThuocct.RowCount > 1)
                {
                    MessageBox.Show("Đơn đã có thuốc, bạn không thể sửa kho xuất!");
                    e.Cancel = true;
                }
                if (grvDonThuocct.RowCount == 1)
                {
                    if (grvDonThuocct.GetRowCellValue(1, col_IDThuoc) != null && Convert.ToInt32(grvDonThuocct.GetRowCellValue(1, col_IDThuoc)) > 0)
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
                if (grvDonThuocct.RowCount > 1)
                {
                    MessageBox.Show("Đơn đã có thuốc, bạn không thể sửa khoa kê!");
                    e.Cancel = true;
                }
                if (grvDonThuocct.RowCount == 1)
                {
                    if (grvDonThuocct.GetRowCellValue(1, col_IDThuoc) != null && Convert.ToInt32(grvDonThuocct.GetRowCellValue(1, col_IDThuoc)) > 0)
                    {
                        MessageBox.Show("Đơn đã có thuốc, bạn không thể sửa khoa kê!");
                        e.Cancel = true;

                    }

                }

            }

        }

        private void grvDonThuocct_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SetTextStock();
            //statusLuu = 1;
            //int madv = 0;
            //int makho = 0;
            //int _mien = 0;

            //if (lup_khoxuat.EditValue != null)
            //    makho = Convert.ToInt32(lup_khoxuat.EditValue);
            //int makp = 0;
            //if (lup_khoake.EditValue != null)
            //    makp = Convert.ToInt32(lup_khoake.EditValue);
            //List<DungChung.Ham.giaSoLoHSD> dsgia = new List<QLBV.DungChung.Ham.giaSoLoHSD>();
            //double thanhtien = 0, soluong = 0, dongia = 0;
            //if (grvDonThuocct.GetFocusedRowCellValue(col_MaDV) != null)
            //{
            //    if (grvDonThuocct.GetFocusedRowCellValue(col_MaDV) != null)
            //        madv = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(col_MaDV));
            //    if (grvDonThuocct.GetFocusedRowCellValue(col_soluong) != null)
            //        soluong = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(col_soluong));
            //    double soluongtong = soluong;
            //    if (grvDonThuocct.GetFocusedRowCellValue(col_dongia) != null)
            //        dongia = Convert.ToDouble(grvDonThuocct.GetFocusedRowCellValue(col_dongia));
            //    for (int i = 0; i < grvDonThuocct.RowCount; i++)
            //    {
            //        if (grvDonThuocct.GetRowCellValue(i, col_MaDV) != null && grvDonThuocct.GetRowCellValue(i, colIDDonct) != null && grvDonThuocct.GetFocusedRowCellValue(col_MaDV) != null)
            //        {
            //            if (Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colIDDonct)) <= 0 && Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, col_MaDV)) == Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(col_MaDV)) && i != e.FocusedRowHandle)
            //                soluongtong += Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, col_soluong));
            //        }
            //    }
            //    if (grvDonThuocct.GetFocusedRowCellValue(colMien) != null)
            //        _mien = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colMien));
            //    if (soluong > 0 && dongia > 0)
            //    {
            //        thanhtien = _mien == 1 ? 0 : (soluong * dongia);
            //        grvDonThuocct.SetFocusedRowCellValue(col_thanhtien, thanhtien);
            //    }
            //    int iddonct = 0;
            //    if (grvDonThuocct.GetFocusedRowCellValue(colIDDonct) != null)
            //        iddonct = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colIDDonct));
            //    int status = -1;
            //    if (grvDonThuocct.GetFocusedRowCellValue(colStatus) != null)
            //        status = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colStatus));
            //    if (rbtn_kieuke.SelectedIndex == 1)
            //    {
            //        dsgia = QLBV.DungChung.Ham._getDSGia(_dataContext, madv, makho);
            //        // dongia = dsgia.First().Gia;//_getGiaSD(_Data, madv, 0, 0, 2, makho);
            //        if (dsgia.Count() == 0 || dsgia.First().Gia <= 0)
            //            MessageBox.Show("Thuốc chưa có giá trong DS Đơn Giá!");
            //        grvDonThuocct.SetFocusedRowCellValue(col_bhyt, 0);
            //    }
            //    else
            //    {
            //        //dongia = getGiaKD(madv, makp, makho);
            //        dsgia = QLBV.DungChung.Ham._getDSGia(_dataContext, madv, makho);
            //        if (dsgia.Count > 0)
            //            dongia = dsgia.First().Gia;
            //        if (_kebosung || _bosungNgtru)
            //        {
            //            if (_benhnhan != null && _benhnhan.DTuong == "BHYT")
            //            {
            //                var qbh = _lthuocVT.Where(p => p.MaDV == madv).Where(p => p.TrongDM == 1).ToList();
            //                if (qbh.Count > 0)
            //                    grvDonThuocct.SetFocusedRowCellValue(col_bhyt, 1);//trường hợp kê độc lập có thanh toán
            //                else
            //                {
            //                    qbh = _lthuocVT.Where(p => p.MaDV == madv).Where(p => p.TrongDM == 0).ToList();
            //                    if (qbh.Count > 0)
            //                    {
            //                        grvDonThuocct.SetFocusedRowCellValue(col_bhyt, 0);
            //                    }
            //                    else
            //                    {
            //                        grvDonThuocct.SetFocusedRowCellValue(col_bhyt, 2);
            //                    }
            //                }
            //            }
            //            else
            //                grvDonThuocct.SetFocusedRowCellValue(col_bhyt, 0);//trường hợp kê độc lập có thanh toán
            //        }
            //        else
            //            grvDonThuocct.SetFocusedRowCellValue(col_bhyt, 2);

            //    }
            //    if (rbtn_kieuke.SelectedIndex == 0)
            //    {

            //        if (iddonct <= 0)
            //        {
            //            double ton = 0;
            //            if (dsgia.Count > 0)
            //                ton = dsgia.First().SoLuong - soluongtong;
            //            gcdonthuoc.Text = "Số lượng tồn: " + ton.ToString();
            //            if (soluong != 0 && (dsgia.Count <= 0 || soluongtong > dsgia.First().SoLuong))
            //            {
            //                MessageBox.Show("Số lượng trong kho không đủ");
            //                grvDonThuocct.SetFocusedRowCellValue(col_soluong, "0");
            //                grvDonThuocct.FocusedColumn = grvDonThuocct.VisibleColumns[2];
            //            }
            //        }
            //        else
            //        {
            //            var soluongdk = _dataContext.DThuoccts.Where(p => p.IDDonct == iddonct).Select(p => p.SoLuong).FirstOrDefault();
            //            double ton = soluongdk;
            //            if (dsgia.Count > 0)
            //                ton = dsgia.First().SoLuong + ton;
            //            gcdonthuoc.Text = "Số lượng tồn: " + ton.ToString();
            //            if (status == 0)
            //            {
            //                if (soluongtong != 0 && (dsgia.Count <= 0 || (soluong > ton)))
            //                {
            //                    MessageBox.Show("Số lượng trong kho không đủ");
            //                    grvDonThuocct.SetFocusedRowCellValue(col_soluong, "0");
            //                    grvDonThuocct.FocusedColumn = grvDonThuocct.VisibleColumns[2];
            //                }
            //            }

            //        }
            //    }
            //    else if (rbtn_kieuke.SelectedIndex == 1 || rbtn_kieuke.SelectedIndex == 3)
            //    {
            //        List<Ham.giaSoLoHSD> tongSL = new List<Ham.giaSoLoHSD>();
            //        double SLKe = 0;//sl kê hiện tại
            //        for (int i = 0; i <= grvDonThuocct.RowCount; i++)
            //        {

            //            if (i != e.FocusedRowHandle && grvDonThuocct.GetRowCellValue(i, col_MaDV) != null && grvDonThuocct.GetRowCellValue(i, col_soluong) != null && grvDonThuocct.GetRowCellValue(i, col_MaDV).ToString() != "" && grvDonThuocct.GetRowCellValue(i, col_soluong).ToString() != "")
            //            {
            //                if (Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, col_MaDV)) == madv)
            //                {
            //                    Ham.giaSoLoHSD moi = new Ham.giaSoLoHSD();
            //                    moi.SoLuong = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, col_soluong));
            //                    if (grvDonThuocct.GetRowCellValue(i, col_dongia) != null && grvDonThuocct.GetRowCellValue(i, col_dongia).ToString() != "")
            //                        moi.Gia = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, col_dongia));
            //                    if (grvDonThuocct.GetRowCellValue(i, colSoLo) != null)
            //                        moi.SoLo = grvDonThuocct.GetRowCellValue(i, colSoLo).ToString();
            //                    if (grvDonThuocct.GetRowCellValue(i, colHanDung) != null && grvDonThuocct.GetRowCellValue(i, colHanDung).ToString() != "")
            //                        moi.HanDung = Convert.ToDateTime(grvDonThuocct.GetRowCellValue(i, colHanDung));
            //                    tongSL.Add(moi);
            //                }
            //            }
            //        }
            //        if (grvDonThuocct.GetRowCellValue(e.FocusedRowHandle, col_soluong) != null && grvDonThuocct.GetRowCellValue(e.FocusedRowHandle, col_soluong).ToString() != "")
            //            SLKe = Convert.ToDouble(grvDonThuocct.GetRowCellValue(e.FocusedRowHandle, col_soluong));

            //        dsgia = QLBV.DungChung.Ham._getDSGia(_dataContext, madv, makho);
            //        double SlTondg1 = 0;
            //        if (dsgia.Count > 0)
            //        {
            //            SlTondg1 = dsgia.First().SoLuong;
            //        }
            //        if (iddonct <= 0)
            //        {
            //            double ton = 0;
            //            if (dsgia.Count > 0)
            //                ton = SlTondg1 - soluongtong;
            //            gcdonthuoc.Text = "Số lượng tồn: " + ton.ToString();
            //            if (soluong != 0 && soluongtong > SlTondg1)//DungChung.Bien.SoLuongTon)
            //            {
            //                MessageBox.Show("Số lượng trong kho không đủ");
            //                grvDonThuocct.SetFocusedRowCellValue(col_soluong, "0");
            //                grvDonThuocct.FocusedColumn = grvDonThuocct.VisibleColumns[2];
            //            }
            //        }
            //        else
            //        {
            //            var soluongdk = _dataContext.DThuoccts.Where(p => p.IDDonct == iddonct).Select(p => p.SoLuong).FirstOrDefault();
            //            gcdonthuoc.Text = "Số lượng tồn: " + (SlTondg1 + soluongdk - soluong).ToString();
            //            //if (status == 0)
            //            //{
            //            if (soluong != 0 && soluong > (SlTondg1 + soluongdk))
            //            {
            //                gcdonthuoc.Text = "Số lượng tồn: " + SlTondg1.ToString();
            //                MessageBox.Show("Số lượng trong kho không đủ");
            //                grvDonThuocct.SetFocusedRowCellValue(col_soluong, "0");
            //                grvDonThuocct.FocusedColumn = grvDonThuocct.VisibleColumns[2];
            //            }
            //            //}
            //        }
            //    }
            //}
        }

        private void grvDonThuoc_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvDonThuoc.GetFocusedRowCellValue(colIDDon) != null)
            {
                selectedIdDon = Convert.ToInt32(grvDonThuoc.GetFocusedRowCellValue(colIDDon));

                lup_khoxuat.Properties.DataSource = _excuteStoredProcedureProvider.ExcuteStoredProcedure<KPhongModel>("sp_RoomList",
                                                                                                                    new Dictionary<string, string>()
                                                                                                                    {
                                                                                                                        { "@MaCB", DungChung.Bien.MaCB },
                                                                                                                        { "@viewKP", "0" }
                                                                                                                    });

                UP_tenthuoc.DataSource = allMedicines;

                _lcb = _dataContext.CanBoes.ToList();
                if (lup_khoake.EditValue != null)
                    _makp = Convert.ToInt32(lup_khoake.EditValue);
                var dthuoc = _medicinesProvider.GetDThuocsByIDDon(selectedIdDon);

                if (dthuoc.Count() > 0)
                {
                    lup_ngayke.EditValue = dthuoc.First().NgayKe;
                    lup_khoake.EditValue = dthuoc.First().MaKP;
                    lup_khoxuat.EditValue = dthuoc.First().MaKXuat;
                    lup_bske.EditValue = dthuoc.First().MaCB;
                    txtIDDon.Text = selectedIdDon.ToString();

                    if (dthuoc.First().PLDV.Value == 7)
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

                DsDonThuoc = _medicinesProvider.ViewInfoMedicineDThuoc(selectedIdDon);

                bindingSource1.DataSource = DsDonThuoc;
                grcDonThuocct.DataSource = bindingSource1;
                anhien(true);
            }

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isDonMoi = 1;
            grcDonThuoc.Enabled = false;
            lup_khoxuat.EditValue = -1;
            lup_ngayke.EditValue = DateTime.Now;
            lup_khoxuat.EditValue = null;
            lup_bske.EditValue = null;
            lup_khoake.EditValue = null;
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
                lup_khoake.Properties.ReadOnly = false;
            else
                lup_khoake.Properties.ReadOnly = true;
            if (_makp > 0)
                lup_khoake.EditValue = _makp;
            else
                lup_khoake.EditValue = DungChung.Bien.MaKP;

            lup_khoxuat.Properties.DataSource = _excuteStoredProcedureProvider.ExcuteStoredProcedure<KPhongModel>("sp_RoomList",
                                                                                                                    new Dictionary<string, string>()
                                                                                                                    {
                                                                                                                        { "@MaCB", DungChung.Bien.MaCB },
                                                                                                                        { "@viewKP", "0" }
                                                                                                                    });

            UP_tenthuoc.DataSource = medicinesByRoom;

            string _makpsd = ";" + _makp + ";";
            lup_bske.Properties.DataSource = _lcb.Where(p => p.Status == 1).Where(p => p.MaKPsd.Contains(_makpsd)).Where(p => p.CapBac != null).Where(p => p.CapBac.ToLower().Contains("bs") || p.CapBac.ToLower().Contains("bác sĩ") || p.CapBac.ToLower().Contains("bác sỹ") || p.CapBac.ToLower().Contains("ys") || p.CapBac.ToLower().Contains("y sĩ") || p.CapBac.ToLower().Contains("y sỹ") || p.CapBac.ToLower().Contains("giáo sư") || p.CapBac.ToLower().Contains("gs") || p.CapBac.ToLower().Contains("tiến sĩ") || p.CapBac.ToLower().Contains("ts")).ToList();


            while (grvDonThuocct.RowCount > 1)
            {
                grvDonThuocct.DeleteRow(0);
            }

            anhien(false);
        }

        private void grvDonThuoc_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            grvDonThuoc_FocusedRowChanged(null, null);
        }

        private void grvDonThuocct_ShowingEditor(object sender, CancelEventArgs e)
        {
            var row = (DThuocctModel)grvDonThuocct.GetFocusedRow();
            if (row != null)
            {
                if (row.ThanhToan == 1)
                    e.Cancel = true;
            }
        }

        private void grvDonThuocct_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            var row = (DThuocctModel)grvDonThuocct.GetRow(e.RowHandle);
            if (row != null)
            {
                if (row.ThanhToan == 1)
                    e.Appearance.ForeColor = Color.Red;
            }
        }

        private void grvDonThuocct_ColumnChanged(object sender, EventArgs e)
        {
            SetTextStock();
        }

        private void UP_tenthuoc_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
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
                var row = (DThuocctModel)grvDonThuocct.GetFocusedRow();
                if (row == null)
                    return;

                var dthuocct = _dataContext.DThuoccts.FirstOrDefault(o => o.IDDonct == row.IDDonct);
                if (dthuocct != null && dthuocct.ThanhToan == 1)
                {
                    MessageBox.Show("Thuốc/vật tư đã thu trực tiếp không thể xóa!");
                    return;
                }

                DialogResult _result = MessageBox.Show("Bạn có muốn xóa thuốc đã kê?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_result == DialogResult.Yes)
                {
                    int iddonct = 0;
                    if (grvDonThuocct.GetFocusedRowCellValue(colIDDonct) != null)
                        iddonct = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colIDDonct));
                    if (iddonct > 0)
                    {
                        var xoa = _dataContext.DThuoccts.Where(p => p.IDDonct == iddonct).FirstOrDefault();
                        if (xoa != null)
                            _dataContext.DThuoccts.Remove(xoa);
                        _dataContext.SaveChanges();
                        grvDonThuocct.DeleteSelectedRows();
                    }
                    else
                    {
                        grvDonThuocct.DeleteSelectedRows();
                    }

                }
            }
        }

        private void grvDonThuocct_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (grvDonThuocct.OptionsBehavior.Editable == true)
            {
                switch (e.Column.Name)
                {
                    case "colXoactdt":
                        {
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
                            var row = (DThuocctModel)grvDonThuocct.GetFocusedRow();
                            if (row == null)
                                return;

                            var dthuocct = _dataContext.DThuoccts.FirstOrDefault(o => o.IDDonct == row.IDDonct);
                            if (dthuocct != null && dthuocct.ThanhToan == 1)
                            {
                                MessageBox.Show("Thuốc/vật tư đã thu trực tiếp không thể xóa!");
                                return;
                            }

                            DialogResult _result = MessageBox.Show("Bạn có muốn xóa thuốc đã kê?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (_result == DialogResult.Yes)
                            {
                                int iddonct = 0;
                                if (grvDonThuocct.GetFocusedRowCellValue(colIDDonct) != null)
                                    iddonct = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colIDDonct));

                                if (iddonct > 0)
                                {
                                    deleteDThuoccts.Add(iddonct);
                                }

                                if (grvDonThuocct.GetFocusedRowCellValue(col_IDThuoc) != null && grvDonThuocct.GetFocusedRowCellValue(col_IDThuoc).ToString() != "")
                                    idThuoc = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(col_IDThuoc));
                                if (grvDonThuocct.GetFocusedRowCellValue(col_dongia) != null && grvDonThuocct.GetFocusedRowCellValue(col_dongia).ToString() != "")
                                    donGia = Convert.ToDouble(grvDonThuocct.GetFocusedRowCellValue(col_dongia));
                                if (grvDonThuocct.GetFocusedRowCellValue(col_soluong) != null && grvDonThuocct.GetFocusedRowCellValue(col_soluong).ToString() != "")
                                    slKe = Convert.ToDouble(grvDonThuocct.GetFocusedRowCellValue(col_soluong));

                                _medicinesProvider.EditStockByIDThuoc(UP_tenthuoc, slKe, 0, idThuoc, 0, donGia, 0, ppxuat);
                                grvDonThuocct.DeleteSelectedRows();

                            }
                        }
                        break;
                }
            }
        }
        private void SetTextStock()
        {
            int idThuoc = 0;
            if (grvDonThuocct.GetFocusedRowCellValue(col_IDThuoc) == null)
            {
                this.grvDonThuocct.ViewCaption = "Danh sách đơn thuốc";
            }
            else
            {
                if ((grvDonThuocct.GetFocusedRowCellValue(col_IDThuoc) != null && grvDonThuocct.GetFocusedRowCellValue(col_IDThuoc).ToString() != ""))
                {
                    idThuoc = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(col_IDThuoc));

                    lupMedicine = ((List<MedicineInventoryModel>)UP_tenthuoc.DataSource);
                    selectedMedicine = lupMedicine.FirstOrDefault(p => p.IDThuoc == idThuoc);

                    if (selectedMedicine != null)
                        this.grvDonThuocct.ViewCaption = "Số lượng tồn: " + selectedMedicine.TonHienTai;
                }
            }
        }
        //private void SetTextStock()
        //{
        //    int maDV = 0;
        //    string soLo = "";
        //    DateTime hanDung = new DateTime();
        //    double donGia = 0;
        //    if (grvDonThuocct.GetFocusedRowCellValue(col_IDThuoc) == null)
        //    {
        //        this.grvDonThuocct.ViewCaption = "Danh sách đơn thuốc";
        //    }
        //    else if (ppxuat == 1)
        //    {
        //        if ((grvDonThuocct.GetFocusedRowCellValue(col_IDThuoc) != null && grvDonThuocct.GetFocusedRowCellValue(col_IDThuoc).ToString() != "") &&
        //        (grvDonThuocct.GetFocusedRowCellValue(col_dongia) != null && grvDonThuocct.GetFocusedRowCellValue(col_dongia).ToString() != ""))
        //        {
        //            maDV = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(col_IDThuoc));
        //            donGia = Convert.ToDouble(grvDonThuocct.GetFocusedRowCellValue(col_dongia));

        //            var medicine = medicinesByRoom.FirstOrDefault(p => p.MaDV == maDV && p.DonGia == donGia);

        //            if (medicine != null)
        //                this.grvDonThuocct.ViewCaption = "Số lượng tồn: " + medicine.TonHienTai;
        //        }
        //    }
        //    else
        //    {
        //        if ((grvDonThuocct.GetFocusedRowCellValue(col_IDThuoc) != null && grvDonThuocct.GetFocusedRowCellValue(col_IDThuoc).ToString() != "") &&
        //        (grvDonThuocct.GetFocusedRowCellValue(colSoLo) != null && grvDonThuocct.GetFocusedRowCellValue(colSoLo).ToString() != "") &&
        //        (grvDonThuocct.GetFocusedRowCellValue(colHanDung) != null) &&
        //        (grvDonThuocct.GetFocusedRowCellValue(col_dongia) != null))
        //        {
        //            maDV = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(col_IDThuoc));
        //            soLo = grvDonThuocct.GetFocusedRowCellValue(colSoLo).ToString();
        //            hanDung = Convert.ToDateTime(grvDonThuocct.GetFocusedRowCellValue(colHanDung));
        //            donGia = Convert.ToDouble(grvDonThuocct.GetFocusedRowCellValue(col_dongia));

        //            var medicine = medicinesByRoom.FirstOrDefault(p => p.MaDV == maDV && p.SoLo == soLo && p.HanDung == hanDung && p.DonGia == donGia);

        //            if (medicine != null)
        //                this.grvDonThuocct.ViewCaption = "Số lượng tồn: " + medicine.TonHienTai;
        //        }
        //    }
        //}
    }
}