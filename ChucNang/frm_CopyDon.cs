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
using System.Transactions;
//using System.Data.Entity.Core;
namespace QLBV.FormNhap
{
    public partial class frm_CopyDon : DevExpress.XtraEditors.XtraForm
    {
        public frm_CopyDon()
        {
            InitializeComponent();
        }
        int _mabn = 0, _makp = 0;
        int iddon = 0;
        bool _noitru = true;
        int _mabncu = 0;
        bool copyFalse;
        DateTime _ngaybdke = new DateTime();
        DateTime _ngaycuoike = new DateTime();

        public frm_CopyDon(int mabn, int id, bool noitru, int makp)
        {
            InitializeComponent();
            _mabncu = mabn;
            _mabn = mabn;
            iddon = id;
            _makp = makp;
            this._noitru = noitru;
        }

        #region KtraLuuKedon
        private bool KTraKD()
        {
            var tenBN = _data.BenhNhans.Where(p => p.MaBNhan == _mabn).Select(p => p.TenBNhan).FirstOrDefault();

            if (dtNgayKe.EditValue == null)
            {
                MessageBox.Show("Bạn chưa nhập ngày kê đơn!");
                dtNgayKe.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(lupBPKe.Text))
            {
                MessageBox.Show("Bạn chưa chọn bộ phận kê đơn!");
                lupBPKe.Focus();
                return false;
            }
            else
            {
                int _makp = Convert.ToInt32(lupBPKe.EditValue);
                var ngaykham = _data.BNKBs.Where(p => p.MaBNhan == _mabn && (p.MaKP == _makp || p.MaKPDTKH == _makp)).Select(p => p.NgayKham).FirstOrDefault();
                if (ngaykham == null)
                {
                    if (DungChung.Bien.MaBV == "30010")
                    {
                        MessageBox.Show("Bệnh nhân " + tenBN + " (" + _mabn + ") Chưa có chẩn đoán tại khoa phòng");
                    }
                    else
                    {
                        MessageBox.Show("Chưa có chẩn đoán tại khoa phòng");
                    }
                    lupBPKe.Focus();
                    return false;
                }
                else if (ngaykham.Value > dtNgayKe.DateTime)
                {
                    MessageBox.Show("Ngày kê không được nhỏ hơn ngày khám: " + ngaykham.ToString());
                    dtNgayKe.Focus();
                    return false;
                }
            }
            if (_noitru)
            {
                if (string.IsNullOrEmpty(lupKieuDon.Text))
                {
                    MessageBox.Show("Bạn chưa chọn kiểu đơn!");
                    lupKieuDon.Focus();
                    return false;
                }
                //if (string.IsNullOrEmpty(cboNhomDuoc.Text))
                //{
                //    MessageBox.Show("Bạn chưa chọn nhóm dược!");
                //    cboNhomDuoc.Focus();
                //    return false;
                //}

            }
            if (string.IsNullOrEmpty(lupNguoiKe.Text))
            {
                MessageBox.Show("Bạn chưa chọn người kê đơn!");
                lupNguoiKe.Focus();
                return false;
            }
            else
            {
                if (lupKieuDon.EditValue != null && Convert.ToInt32(lupKieuDon.EditValue) == 2)
                {
                    var cbke = _data.DThuocs.Where(p => p.IDDon == id).Select(p => p.MaCB).FirstOrDefault();
                    if (cbke != null && cbke != lupNguoiKe.EditValue.ToString())
                    {
                        MessageBox.Show("Bác sỹ kê ko hợp lệ");
                        return false;
                    }
                }
            }
            if (string.IsNullOrEmpty(lupKhoXuat.Text))
            {
                MessageBox.Show("Bạn chưa chọn kho xuất thuốc!");
                lupKhoXuat.Focus();
                return false;
            }

            if (lupKieuDon.EditValue != null && Convert.ToInt32(lupKieuDon.EditValue) == 2)
            {

                var dthuoc = _data.DThuoccts.Where(p => p.Status == 0 || p.Status == null).Where(p => p.IDDon == id).ToList();
                if (dthuoc.Count > 0)
                {
                    MessageBox.Show("Trả thuốc chỉ sao đơn được những đơn đã xuất dược");
                    lupKieuDon.Focus();
                    return false;
                }
                DateTime ngayketra = dtNgayKe.DateTime;
                if (grvDonThuoc.GetFocusedRowCellValue(colNgayKe) != null)
                {
                    DateTime NgayKecu = Convert.ToDateTime(grvDonThuoc.GetFocusedRowCellValue(colNgayKe));
                    if (ngayketra.ToShortDateString() != NgayKecu.ToShortDateString())
                    {
                        MessageBox.Show("Trả thuốc chỉ có thể trả trong ngày kê");
                        return false;
                    }
                    DateTime tu = DungChung.Ham.NgayTu(ngayketra);
                    DateTime den = DungChung.Ham.NgayDen(ngayketra);
                    string dsthuoc = "";
                    int dem = 0;
                    foreach (var a in _ldthuocct)
                    {

                        var ktton = (from dt in _data.DThuocs.Where(p => p.MaBNhan == _mabn).Where(p => p.NgayKe >= tu && p.NgayKe <= den).Where(p => p.KieuDon != 2)
                                     join dtct in _data.DThuoccts.Where(p => p.MaDV == a.dThuocct.MaDV).Where(p => p.DonGia == a.dThuocct.DonGia).Where(p => p.Status == 1) on dt.IDDon equals dtct.IDDon
                                     select dtct.SoLuong).ToList();
                        var ktton1 = (from dt in _data.DThuocs.Where(p => p.MaBNhan == _mabn).Where(p => p.NgayKe >= tu && p.NgayKe <= den).Where(p => p.KieuDon == 2)
                                      join dtct in _data.DThuoccts.Where(p => p.MaDV == a.dThuocct.MaDV).Where(p => p.DonGia == a.dThuocct.DonGia) on dt.IDDon equals dtct.IDDon
                                      select dtct.SoLuong).ToList();
                        double ton = ktton.Sum(p => p);
                        double ton2 = ktton1.Sum(p => p);
                        if ((ton + ton2) < a.dThuocct.SoLuong)
                        {
                            var tendv = _data.DichVus.Where(p => p.MaDV == (a.dThuocct.MaDV)).Select(p => p.TenDV).FirstOrDefault();
                            if (tendv != null)
                                dsthuoc += tendv.ToString() + ";";
                            dem++;
                        }
                    }
                    if (dem > 0)
                    {
                        MessageBox.Show("Thuốc: " + dsthuoc + " có số lượng trả nhiều hơn số lượng đã lĩnh trong ngày \nBạn không thể trả thuốc");
                        return false;
                    }
                }


            }


            var rv = _data.RaViens.Where(p => p.MaBNhan == _mabn).ToList();
            if (rv.Count > 0)
            {
                if (DungChung.Bien.MaBV == "30010")
                {
                    MessageBox.Show("Bệnh nhân " + tenBN + " (" + _mabn + ") đã làm thủ tục ra viện, bạn không thể sao đơn");
                }
                MessageBox.Show("Bệnh nhân đã làm thủ tục ra viện, bạn không thể sao đơn");
                return false;
            }

            if (chkSaoNhieuNgay.Checked)
            {
                if (dtNgayKeDen.EditValue == null)
                {
                    MessageBox.Show("Bạn chưa chọn ngày kê đến");
                    return false;
                }
                if (dtNgayKe.DateTime > dtNgayKeDen.DateTime)
                {
                    MessageBox.Show("Ngày kê từ không được lớn hơn ngày kê đến");
                    return false;
                }
            }

            return true;
        }
        public class _ldthuoc
        {
            public int _iddon { get; set; }
            public int _makxuat { get; set; }
            public DateTime? _ngayke { get; set; }
            public string _kieudon { get; set; }
            public int KieuDon { get; set; }
            public int? IDDon_Mau { get; set; }
        }

        #endregion
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<_ldthuoc> _lDthuoc = new List<_ldthuoc>();
        List<DThuocctEx> _ldthuocct = new List<DThuocctEx>();
        BenhNhan _lbn = new BenhNhan();

        public class DThuocctEx
        {
            public DThuocct dThuocct { get; set; }
            public int? StatusDV { get; set; }
        }
        private void frm_CopyDon_Load(object sender, EventArgs e)
        {
            DateTime Update = Convert.ToDateTime("2018-01-01 00:00:00");
            if (DateTime.Now < Update)
                DungChung.Ham.UpDateMaKXuat(_data);
            if (_makp == 0)
                if (!copyFalse)
                    ckcBNKhac.Visible = false;
            var dv = _data.DichVus.Where(p => p.PLoai == 1).ToList();
            lupMaDV.DataSource = dv;
            lupKieuDon.Properties.DataSource = DungChung.Bien._lKieuDonBN().Where(p => p.value == 0 || p.value == 1).ToList();
            lupkieudonnew.DataSource = DungChung.Bien._lKieuDonBN();
            var q = _data.KPhongs.Where(p => p.Status == 1).OrderBy(p => p.TenKP).ToList();
            //lupBPKe.Properties.DataSource = q.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).ToList();
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
            {
                lupBPKe.Properties.DataSource = q.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).ToList();
            }
            else
            {
                var _kpke = (from kp in q.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham)
                             join kp1 in DungChung.Bien.listKPHoatDong on kp.MaKP equals kp1
                             select kp).ToList();
                lupBPKe.Properties.DataSource = _kpke;
            }
            if (_makp == 0)
            {
                if (!copyFalse)
                    ckcBNKhac.Visible = false;
            }
            else
            {
                if (!copyFalse)
                {
                    lupBPKe.EditValue = _makp;
                    lupBPKe.ReadOnly = true;
                }
            }
            string MKP = _makp.ToString();
            lupKhoXuat.Properties.DataSource = q.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc || (DungChung.Bien.MaBV == "24012" ? (p.MaKPsd != null ? (p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc) && p.MaKPsd.Contains(MKP) : true) : true)).ToList();
            _lbn = _data.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
            int _rowhandle = 1;

            int i = -1;
            if (_noitru)
            {
                _lDthuoc = (from dt in _data.DThuocs.Where(p => p.MaBNhan == _mabn && ((DungChung.Bien.MaBV == "14017" && _makp > 0) ? p.MaKP == _makp : true)).Where(p => p.KieuDon == 0 || p.KieuDon == 1).OrderByDescending(p => p.NgayKe)
                            join dtct in _data.DThuoccts on dt.IDDon equals dtct.IDDon
                            select new _ldthuoc { _iddon = dt.IDDon, _makxuat = dt.MaKXuat ?? 0, _ngayke = dt.NgayKe, _kieudon = dt.KieuDon == 0 ? "Hàng ngày" : "Bổ sung", KieuDon = dt.KieuDon ?? 0, IDDon_Mau = dt.IDDon_Mau }).Distinct().ToList();
            }
            else
            {
                string sthe = _lbn.SThe;
                if (!string.IsNullOrEmpty(sthe))
                {
                    _lDthuoc = (from bn in _data.BenhNhans.Where(p => p.SThe == sthe)
                                join dt in _data.DThuocs.Where(p => p.PLDV == 1 && ((DungChung.Bien.MaBV == "14017" && _makp > 0) ? p.MaKP == _makp : true)) on bn.MaBNhan equals dt.MaBNhan
                                join dtct in _data.DThuoccts on dt.IDDon equals dtct.IDDon
                                select new _ldthuoc { _iddon = dt.IDDon, _makxuat = dt.MaKXuat ?? 0, _ngayke = dt.NgayKe, _kieudon = dt.KieuDon.ToString(), KieuDon = dt.KieuDon ?? 0, IDDon_Mau = dt.IDDon_Mau }).OrderBy(p => p._ngayke).Distinct().ToList();
                }
                var bnkb = _data.BNKBs.Where(p => p.IDKB == iddon).FirstOrDefault();
                if (bnkb == null)
                {
                    MessageBox.Show("Bạn chưa lưu chẩn đoán nên không thể sao đơn");
                    return;
                    this.Dispose();
                }
                if (!copyFalse)
                {
                    dtNgayKe.DateTime = bnkb.NgayKham.Value;
                    dtNgayKeDen.DateTime = bnkb.NgayKham.Value;
                }

                lupKieuDon.EditValue = -1;
                lupKieuDon.ReadOnly = true;
            }
            grcDonThuoc.DataSource = _lDthuoc.ToList();
            
            foreach (var a in _lDthuoc)
            {
                i++;
                if (a._iddon == iddon)
                {
                    _rowhandle = i;
                    break;
                }
            }
            grvDonThuoc.FocusedRowHandle = _rowhandle;
            grvDonThuoc_FocusedRowChanged(null, null);
            if (!copyFalse)
            {
                lupDenNgay.DateTime = dtNgayKe.DateTime;
            }


        }
        int id = 0;
        int? idDonMau = null;
        private void grvDonThuoc_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            if (grvDonThuoc.GetFocusedRowCellValue(colIDDon) != null && grvDonThuoc.GetFocusedRowCellValue(colIDDon).ToString() != "")
            {
                var row = (_ldthuoc)grvDonThuoc.GetFocusedRow();
                if (row != null)
                {
                    idDonMau = row.IDDon_Mau;
                }
                else
                    idDonMau = null;
                id = Convert.ToInt32(grvDonThuoc.GetFocusedRowCellValue(colIDDon));
                _ldthuocct = (from a in _data.DThuoccts.Where(p => p.IDDon == id)
                              join b in _data.DichVus on a.MaDV equals b.MaDV
                              select new DThuocctEx { dThuocct = a, StatusDV = b.Status }).OrderBy(p => p.dThuocct.IDDonct).ToList();



                var qtn = (from dt in _data.DThuoccts.Where(p => p.IDDon == id)
                           join dv in _data.DichVus.Where(o => o.DongY == 1) on dt.MaDV equals dv.MaDV
                           join tn in _data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                           select tn).FirstOrDefault();
                if (qtn != null)
                {
                    cboSoThang.ReadOnly = false;
                }
                else
                {
                    cboSoThang.ReadOnly = true;
                }
                if (_ldthuocct.Count > 0)
                    cboSoThang.Text = _ldthuocct.First().dThuocct.Loai.ToString();
                grcDonThuocct.DataSource = _ldthuocct.Select(o => o.dThuocct).OrderBy(p => p.IDDonct).ToList();
                if (grvDonThuoc.GetFocusedRowCellValue(colMaKPnx) != null && grvDonThuoc.GetFocusedRowCellValue(colMaKPnx).ToString() != "")
                    lupKhoXuat.EditValue = Convert.ToInt32(grvDonThuoc.GetFocusedRowCellValue(colMaKPnx));
                else
                    lupKhoXuat.EditValue = 0;
                if (grvDonThuoc.GetFocusedRowCellValue(colKieuDon) != null && grvDonThuoc.GetFocusedRowCellValue(colKieuDon).ToString() != "")
                    lupKieuDon.EditValue = Convert.ToInt16(grvDonThuoc.GetFocusedRowCellValue(colKieuDon));
                else
                    lupKieuDon.EditValue = -1;
                if (_noitru)
                    dtNgayKe.DateTime = System.DateTime.Now;
                lupNguoiKe.EditValue = DungChung.Bien.MaCB;
                if (DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "27023")
                {
                    var db = _data.DienBiens.Where(p => p.MaBNhan == _mabn).Where(p => p.IDDon == id).ToList();
                    memoDienBien.Text = db.Count > 0 ? db.First().DienBien1 : "";
                }
            }
            else
            {

            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void grvDonThuocct_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == STT)
            {
                e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            #region 30010
            if (DungChung.Bien.MaBV == "30010") // 
            {
                List<string> errors = new List<string>();
                //lay ds ma bn
                List<int> lstBNhan = new List<int>();
                int[] index = grvDSBNhan.GetSelectedRows();
                for (int i = 0; i < index.Count(); i++)
                {
                    int mabn = Convert.ToInt32(grvDSBNhan.GetRowCellValue(index[i], colMaBNhan));
                    lstBNhan.Add(mabn);
                }
                //sao đơn cho BN đã chọn
                if (ckcBNKhac.Checked == false)
                {
                    _mabn = _mabncu;

                    if (KTraKD())
                    {
                        var dv = _data.DichVus.ToList();
                        if (chkSaoNhieuNgay.Checked)
                        {
                            var ngayKe = dtNgayKe.DateTime;
                            var ngayKeDen = dtNgayKeDen.DateTime.Date;
                            while (ngayKe.Date <= ngayKeDen.Date)
                            {
                                SaoDon(dv, ngayKe, ref errors);
                                ngayKe = ngayKe.AddDays(1);
                            }
                        }
                        else
                        {
                            SaoDon(dv, dtNgayKe.DateTime, ref errors);
                        }

                        if (errors.Count > 0)
                        {
                            MessageBox.Show(string.Join(Environment.NewLine, errors.Distinct().ToList()));
                            copyFalse = true;
                            frm_CopyDon_Load(null, null);
                            simpleButton2.Enabled = false;
                        }
                        else
                            this.Dispose();
                    }
                }

                //sao đơn cho nhiều BN khác
                else
                {
                    if (lstBNhan.Count == 0)
                    {
                        MessageBox.Show("Chưa chọn bệnh nhân sao đơn");
                        return;
                    }
                    else
                    {
                        foreach (var mabn in lstBNhan)
                        {
                            _mabn = mabn;

                            if (KTraKD())
                            {
                                var dv = _data.DichVus.ToList();
                                if (chkSaoNhieuNgay.Checked)
                                {
                                    var ngayKe = dtNgayKe.DateTime;
                                    var ngayKeDen = dtNgayKeDen.DateTime.Date;
                                    while (ngayKe.Date <= ngayKeDen.Date)
                                    {
                                        SaoDon(dv, ngayKe, ref errors);
                                        ngayKe = ngayKe.AddDays(1);
                                    }
                                }
                                else
                                {
                                    SaoDon(dv, dtNgayKe.DateTime, ref errors);
                                }
                            }
                        }
                        if (errors.Count > 0)
                        {
                            MessageBox.Show(string.Join(Environment.NewLine, errors.Distinct().ToList()));
                            copyFalse = true;
                            frm_CopyDon_Load(null, null);
                            simpleButton2.Enabled = false;
                        }
                        else
                            this.Dispose();
                    }
                }
            }
            #endregion
            #region vien khac
            else
            {
                if (ckcBNKhac.Checked)
                {
                    if (lupDSBNhan.EditValue == null)
                    {
                        MessageBox.Show("Chưa chọn bệnh nhân sao đơn");
                        return;
                    }
                    else
                    {
                        _mabn = Convert.ToInt32(lupDSBNhan.EditValue);
                    }
                }
                else
                {
                    _mabn = _mabncu;
                }
                if (KTraKD())
                {
                    var dv = _data.DichVus.ToList();
                    List<string> errors = new List<string>();
                    if (chkSaoNhieuNgay.Checked)
                    {
                        var ngayKe = dtNgayKe.DateTime;
                        var ngayKeDen = dtNgayKeDen.DateTime.Date;
                        while (ngayKe.Date <= ngayKeDen.Date)
                        {

                            SaoDon(dv, ngayKe, ref errors);
                            ngayKe = ngayKe.AddDays(1);
                        }
                        if (ngayKe > ngayKeDen)
                        {
                            _ngaybdke = ngayKe;
                            _ngaycuoike = ngayKe;
                        }
                        else
                        {
                            _ngaybdke = ngayKe;
                            _ngaycuoike = ngayKeDen;
                        }
                    }
                    else
                    {
                        SaoDon(dv, dtNgayKe.DateTime, ref errors);
                    }

                    if (errors.Count > 0)
                    {
                        MessageBox.Show(string.Join(Environment.NewLine, errors.Distinct().ToList()));
                        copyFalse = true;
                        frm_CopyDon_Load(null, null);
                        simpleButton2.Enabled = false;
                    }
                    else
                        this.Dispose();
                }
            }
            #endregion
        }

        bool isNotVatTu;
        bool isLuuDienBien;
        private void SaoDon(List<DichVu> dv, DateTime ngayKe, ref List<string> listErrors)
        {
            DThuoc dthuoc = new DThuoc();
            dthuoc.MaBNhan = _mabn;
            dthuoc.NgayKe = ngayKe;
            dthuoc.MaKP = lupBPKe.EditValue == null ? 0 : Convert.ToInt32(lupBPKe.EditValue);
            dthuoc.MaCB = lupNguoiKe.EditValue.ToString();
            dthuoc.MaKXuat = lupKhoXuat.EditValue == null ? 0 : Convert.ToInt32(lupKhoXuat.EditValue);
            dthuoc.PLDV = 1;
            dthuoc.IDDon_Mau = idDonMau;
            var qdtct = (from dtct in _ldthuocct
                         join dvu in _data.DichVus.Where(o => o.DongY == 1) on dtct.dThuocct.MaDV equals dvu.MaDV
                         select dvu).ToList();
            if (dv.Count > 0)
            {
                if (DungChung.Bien.MaBV == "14017" || DungChung.Bien.MaBV == "24272")
                {
                    var donThuoc = _data.DThuocs.FirstOrDefault(p => p.IDDon == id);
                    if (donThuoc != null)
                    {
                        var ghiChuSp = donThuoc.GhiChu.Split(';');
                        if (ghiChuSp.Count() > 0)
                            ghiChuSp[0] = cboSoThang.Text;
                        if (ghiChuSp.Count() > 1)
                            ghiChuSp[1] = ngayKe.ToString("dd/MM/yyyy");
                        if (ghiChuSp.Count() > 2)
                            ghiChuSp[2] = lupDenNgay.DateTime.ToString("dd/MM/yyyy");
                        dthuoc.GhiChu = string.Join(";", ghiChuSp);
                        if (DungChung.Bien.MaBV == "24272" && chkSaoNhieuNgay.Checked)
                        {
                            if (ghiChuSp.Count() > 1)
                                ghiChuSp[1] = _ngaybdke.ToString("dd/MM/yyyy");
                            if (ghiChuSp.Count() > 2)
                                ghiChuSp[2] = _ngaycuoike.ToString("dd/MM/yyyy");
                        }
                    }
                }
                else
                    dthuoc.GhiChu = "Sử dụng từ ngày " + ngayKe.ToString("dd/MM/yyyy") + " đến ngày " + lupDenNgay.DateTime.ToString("dd/MM/yyyy");
            }
            else
            {
                dthuoc.GhiChu = cboSoThang.Text;
            }


            if (_noitru && lupKieuDon.EditValue != null)
            {
                dthuoc.KieuDon = Convert.ToInt32(lupKieuDon.EditValue);
            }
            else
            {
                dthuoc.KieuDon = -1;
            }
            _data.DThuocs.Add(dthuoc);
            if (_data.SaveChanges() >= 0)
            {
                int maxid = dthuoc.IDDon;
                int _madv = 0;
                int _makho = lupKhoXuat.EditValue == null ? 0 : Convert.ToInt32(lupKhoXuat.EditValue);
                var khoKe = _data.KPhongs.FirstOrDefault(o => o.MaKP == _makho);
                double _dongia = 0;
                double _soluong = 0;
                string _ylenh = "";
                byte sothang = Convert.ToByte(cboSoThang.Text);
                if (sothang <= 0)
                    sothang = 1;

                foreach (var a in _ldthuocct)
                {
                    var tendv = dv.Where(p => p.MaDV == (a.dThuocct.MaDV)).ToList();
                    string maKPSD = tendv.First().MaKPsd;
                    string maKP = lupBPKe.EditValue.ToString();
                    string[] maKPSDS = maKPSD.Split(';');
                    int checkKPSD = 0;
                    for (int i = 0; i < maKPSDS.Length; i++)
                    {
                        if (maKP == maKPSDS[i])
                        {
                            checkKPSD = 1;
                            break;
                        }
                    }
                    if (checkKPSD == 0)
                    {
                        MessageBox.Show(tendv.First().TenDV + " không được kê trong khoa trong khoa này, không thể sao");
                        continue;
                    }

                    if (a.StatusDV != 1)
                    {
                        if (tendv.Count > 0)
                        {
                            listErrors.Add("Thuốc: " + tendv.First().TenDV + " không được sử dụng không thể sao");
                        }
                        continue;
                    }
                    _madv = a.dThuocct.MaDV == null ? 0 : a.dThuocct.MaDV.Value;
                    _dongia = a.dThuocct.DonGia;
                    _soluong = a.dThuocct.SoLuongct * sothang;

                    double soluongton = 0;
                    string solo = "";
                    if (a.dThuocct.SoLo != null)
                        solo = a.dThuocct.SoLo;
                    if (lupKieuDon.EditValue != null && Convert.ToInt32(lupKieuDon.EditValue) == 2)
                    {
                        DThuocct _newdtct = new DThuocct();
                        _newdtct.MaKP = lupBPKe.EditValue == null ? 0 : Convert.ToInt32(lupBPKe.EditValue);
                        _newdtct.MaKPtk = lupBPKe.EditValue == null ? 0 : Convert.ToInt32(lupBPKe.EditValue);
                        _newdtct.NgayNhap = ngayKe;
                        _newdtct.IDDon = maxid;
                        _newdtct.TrongBH = a.dThuocct.TrongBH;
                        _newdtct.MaDV = a.dThuocct.MaDV;
                        _newdtct.Luong = a.dThuocct.Luong;
                        _newdtct.SoLan = a.dThuocct.SoLan;
                        _newdtct.MoiLan = a.dThuocct.MoiLan;
                        _newdtct.DviUong = a.dThuocct.DviUong;
                        _newdtct.MaKXuat = _makho;
                        _newdtct.IsMuaNgoai = (khoKe != null && khoKe.IsMuaNgoai == true);
                        _newdtct.MaCB = lupNguoiKe.EditValue.ToString();
                        _newdtct.SoLuongct = a.dThuocct.SoLuongct * (-1);
                        _newdtct.SoLuong = _soluong * (-1);
                        _newdtct.DonVi = a.dThuocct.DonVi;
                        _newdtct.DonGia = a.dThuocct.DonGia;
                        _newdtct.ThanhTien = a.dThuocct.ThanhTien * (-1);
                        _newdtct.TyLeTT = a.dThuocct.TyLeTT;
                        _newdtct.SoLo = a.dThuocct.SoLo;
                        _newdtct.MaCC = a.dThuocct.MaCC;
                        _newdtct.GhiChu = a.dThuocct.GhiChu;
                        _newdtct.ThuocDVGayTe = "";
                        if (a.dThuocct.Status == 1 || a.dThuocct.Status == 2)
                            _newdtct.Status = 0;
                        else
                            _newdtct.Status = a.dThuocct.Status;
                        _newdtct.IDKB = a.dThuocct.IDKB;

                        _data.DThuoccts.Add(_newdtct);
                        _data.SaveChanges();
                    }
                    else
                    {
                        var ktradongy = dv.Where(p => p.MaDV == _madv && p.DongY == 1).ToList();
                        soluongton = DungChung.Ham._checkTon_KD(_data, _madv, _makho, _dongia, 0, solo);
                        if (ktradongy.Count > 0)
                            soluongton = soluongton - DungChung.Ham._getSL_DongY(_data, _madv, _soluong, _makho);
                        if (soluongton >= _soluong)
                        {
                            DThuocct _newdtct = new DThuocct();
                            _newdtct.MaKP = lupBPKe.EditValue == null ? 0 : Convert.ToInt32(lupBPKe.EditValue);
                            _newdtct.MaKPtk = lupBPKe.EditValue == null ? 0 : Convert.ToInt32(lupBPKe.EditValue);
                            _newdtct.NgayNhap = ngayKe;
                            _newdtct.IDDon = maxid;
                            _newdtct.TrongBH = a.dThuocct.TrongBH;
                            _newdtct.MaDV = a.dThuocct.MaDV;
                            _newdtct.SoLuong = _soluong;
                            _newdtct.Loai = sothang;
                            _newdtct.SoLuongct = a.dThuocct.SoLuongct;
                            _newdtct.DonVi = a.dThuocct.DonVi;
                            _newdtct.DonGia = a.dThuocct.DonGia;
                            _newdtct.Luong = a.dThuocct.Luong;
                            _newdtct.SoLan = a.dThuocct.SoLan;
                            _newdtct.MoiLan = a.dThuocct.MoiLan;
                            _newdtct.DviUong = a.dThuocct.DviUong;
                            _newdtct.MaCB = lupNguoiKe.EditValue.ToString();
                            _newdtct.GhiChu = a.dThuocct.GhiChu;
                            _newdtct.MaKXuat = _makho;
                            _newdtct.ThanhTien = Math.Round(a.dThuocct.DonGia * a.dThuocct.SoLuongct * sothang, 4);
                            _newdtct.TyLeTT = a.dThuocct.TyLeTT;
                            _newdtct.SoLo = a.dThuocct.SoLo;
                            _newdtct.HanDung = a.dThuocct.HanDung;
                            _newdtct.MaCC = a.dThuocct.MaCC;
                            _newdtct.ThuocDVGayTe = "";
                            _newdtct.IsMuaNgoai = (khoKe != null && khoKe.IsMuaNgoai == true);
                            if (a.dThuocct.Status == 1 || a.dThuocct.Status == 2)
                                _newdtct.Status = 0;
                            else
                                _newdtct.Status = a.dThuocct.Status;
                            _newdtct.IDKB = a.dThuocct.IDKB;
                            _data.DThuoccts.Add(_newdtct);

                            if (_data.SaveChanges() >= 0)
                            {
                                if (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017")
                                {
                                    if (dthuoc.KieuDon == 1 || dthuoc.KieuDon == 0)
                                        DungChung.Ham.Update_STT_ThuocGNHT(_data, _mabn);
                                }
                                var qtnhom = (from dichvu in _data.DichVus.Where(p => p.MaDV == a.dThuocct.MaDV) join tn in _data.TieuNhomDVs on dichvu.IdTieuNhom equals tn.IdTieuNhom select tn).FirstOrDefault();
                                if (qtnhom != null && qtnhom.TenRG != null && (DungChung.Bien.MaBV == "30007" ? true : qtnhom.TenRG.Contains("Thuốc")))
                                {
                                    if (lupKieuDon.EditValue != null && Convert.ToInt32(lupKieuDon.EditValue) != 2)
                                    {
                                        var ttthuoc = dv.Where(p => p.MaDV == a.dThuocct.MaDV).FirstOrDefault();
                                        if (ttthuoc != null)
                                        {
                                            if (DungChung.Bien.MaBV == "24009")
                                            {
                                                isNotVatTu = false;
                                                if (ttthuoc.IDNhom != 10 && ttthuoc.IDNhom != 11)
                                                {
                                                    isNotVatTu = true;
                                                    isLuuDienBien = true;
                                                }
                                            }
                                            if (DungChung.Bien.MaBV == "20001")
                                            {
                                                string lieudung = "";
                                                if (a.dThuocct.SoLan != "")
                                                    lieudung += " " + a.dThuocct.SoLan;
                                                if (a.dThuocct.MoiLan != "")
                                                    lieudung += " " + a.dThuocct.MoiLan;
                                                if (a.dThuocct.Luong != "")
                                                    lieudung += " " + a.dThuocct.Luong;
                                                if (a.dThuocct.DviUong != "")
                                                    lieudung += " " + a.dThuocct.DviUong;
                                                if (ttthuoc.DongY == 1)
                                                {
                                                    _ylenh += ttthuoc.TenDV + " X " + a.dThuocct.SoLuongct + "  " + a.dThuocct.DonVi + (a.dThuocct.GhiChu == "" ? "" : ("," + a.dThuocct.GhiChu)) + ", \n";
                                                }
                                                else
                                                {
                                                    _ylenh += ttthuoc.TenDV + " X " + a.dThuocct.SoLuongct + "  " + a.dThuocct.DonVi + ": " + lieudung + (a.dThuocct.GhiChu == "" ? "" : ("," + a.dThuocct.GhiChu)) + ", \n";
                                                }
                                            }
                                            else if ((DungChung.Bien.MaBV == "24009" && isNotVatTu) || DungChung.Bien.MaBV != "24009")
                                                _ylenh += ttthuoc.TenDV + (ttthuoc.HamLuong != null ? ":" + ttthuoc.HamLuong : "") + " X " + a.dThuocct.SoLuong + "  " + a.dThuocct.DonVi + ", " + a.dThuocct.GhiChu + ", \n";
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (tendv.Count > 0)
                            {
                                listErrors.Add("Thuốc: " + tendv.First().TenDV + " trong kho không đủ");
                            }
                        }
                    }
                }
                // lưu diễn biến
                if (lupKieuDon.EditValue != null && Convert.ToInt32(lupKieuDon.EditValue) < 2 && _ylenh.Length > 0)
                {
                    if (Convert.ToInt32(lupKieuDon.EditValue) != 2)
                    {
                        //if (DungChung.Bien.MaBV == "30007")
                        //{
                        //    DateTime NgayKetu = DungChung.Ham.NgayTu(ngayKe);
                        //    DateTime NgayKeden = DungChung.Ham.NgayDen(ngayKe);
                        //    var ktdb = _data.DienBiens.Where(p => p.NgayNhap >= NgayKetu && p.NgayNhap <= NgayKeden).Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                        //    if (ktdb != null)
                        //    {
                        //        ktdb.YLenh += _ylenh;
                        //        ktdb.DienBien1 += "\n" + memoDienBien.Text;
                        //        _data.SaveChanges();
                        //    }
                        //    else
                        //    {
                        //        DienBien dbmoi = new DienBien();
                        //        dbmoi.NgayNhap = ngayKe;
                        //        dbmoi.IDDon = maxid;
                        //        dbmoi.YLenh = _ylenh;
                        //        dbmoi.DienBien1 = memoDienBien.Text;
                        //        dbmoi.MaCB = lupNguoiKe.EditValue.ToString();
                        //        dbmoi.MaBNhan = _mabn;
                        //        _data.DienBiens.Add(dbmoi);
                        //        _data.SaveChanges();
                        //    }
                        //}
                        //else
                        //{

                        if (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017")
                        {
                            var maKP = lupBPKe.EditValue == null ? 0 : Convert.ToInt32(lupBPKe.EditValue);
                            usDieuTri.UpdateDienBien_DonThuoc_CLS(_data, _mabn, DungChung.Bien.MaCB, Convert.ToInt32(lupKieuDon.EditValue), ngayKe, memoDienBien.Text, maKP, false, false);
                        }
                        else
                        {
                            var ktdb = _data.DienBiens.Where(p => p.MaBNhan == _mabn).Where(p => p.IDDon == maxid).ToList();
                            if (ktdb.Count > 0)
                            {
                                int id = ktdb.First().ID;
                                var sua = _data.DienBiens.Single(p => p.ID == id);
                                sua.NgayNhap = ngayKe;
                                sua.IDDon = maxid;
                                sua.YLenh = _ylenh;
                                sua.DienBien1 = memoDienBien.Text;
                                sua.MaCB = lupNguoiKe.EditValue.ToString();
                                sua.MaBNhan = _mabn;
                                _data.SaveChanges();
                            }
                            else
                            {
                                DienBien dbmoi = new DienBien();
                                dbmoi.NgayNhap = ngayKe;
                                dbmoi.IDDon = maxid;
                                dbmoi.YLenh = _ylenh;
                                dbmoi.DienBien1 = memoDienBien.Text;
                                dbmoi.MaCB = lupNguoiKe.EditValue.ToString();
                                dbmoi.MaBNhan = _mabn;
                                if (DungChung.Bien.MaBV != "24009" || (isLuuDienBien && DungChung.Bien.MaBV == "24009"))
                                    _data.DienBiens.Add(dbmoi);
                                _data.SaveChanges();
                            }
                        }
                    }
                }
            }
        }

        private void cboKieuDon_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void grvDonThuoc_DataSourceChanged(object sender, EventArgs e)
        {
            grvDonThuoc_FocusedRowChanged(null, null);
        }

        public class ClassA
        {
            public string TenCB { get; set; }
            public string MaCB { get; set; }
        }

        private void lupBPKe_EditValueChanged(object sender, EventArgs e)
        {
            if (lupBPKe.EditValue != null)
            {
                int makp = Convert.ToInt32(lupBPKe.EditValue);
                string _makp = makp.ToString();

                var query = _data.CanBoes.Where(p => p.Status == 1).Where(p => p.MaKPsd.Contains(_makp)).OrderBy(p => p.TenCB).ToList();
                List<ClassA> l = new List<ClassA>();
                lupNguoiKe.Properties.DataSource = query.ToList();
                if (DungChung.Bien.MaBV == "30007")
                {
                    if (DungChung.Bien.PLoaiKP != DungChung.Bien.st_PhanLoaiKP.Admin)
                    {
                        if (query.Where(p => p.MaCB == DungChung.Bien.MaCB).Count() > 0)
                            lupNguoiKe.EditValue = DungChung.Bien.MaCB;
                    }
                }
                else if (DungChung.Bien.MaBV == "01071")
                {
                    for (int i = 0; i < query.Count; i++)
                    {
                        var a = query[i].MaKPsd.Split(';');
                        for (int j = 0; j < a.Count(); j++)
                        {
                            if (a[j] != "" && a[j] == _makp)
                            {
                                ClassA l1 = new ClassA();
                                l1.TenCB = query[i].TenCB;
                                l1.MaCB = query[i].MaCB;
                                l.Add(l1);
                            }
                        }
                    }
                    lupNguoiKe.Properties.DataSource = l.ToList();
                }
            }
            else
            {
                lupNguoiKe.Properties.DataSource = null;
            }

        }

        private void groupControl3_Paint(object sender, PaintEventArgs e)
        {

        }
        void setngayden()
        {
            if (dtNgayKe.DateTime != null)
            {
                int sothang = Convert.ToInt32(cboSoThang.Text);
                lupDenNgay.DateTime = dtNgayKe.DateTime.AddDays(sothang - 1);
            }
        }
        private void lupTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            setngayden();
        }

        private void cboSoThang_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dtNgayKe_EditValueChanged(object sender, EventArgs e)
        {
            setngayden();
        }

        private void cboSoThang_EditValueChanged(object sender, EventArgs e)
        {
            setngayden();
        }

        private void ckcBNKhac_CheckedChanged(object sender, EventArgs e)
        {
            var _lTKbn = (from bn in _data.BenhNhans.Where(p => (p.Status == 1 || p.Status == 4 || p.Status == 5)).Where(p => p.NoiTru == 1)
                          where (bn.MaKP == _makp)
                          where (bn.MaKCB == DungChung.Bien.MaBV)
                          select new { bn.MaBNhan, bn.TenBNhan }).OrderBy(p => p.MaBNhan).OrderBy(p => p.TenBNhan).ToList();
            if (DungChung.Bien.MaBV == "24272")
            {
                _lTKbn = (from bn in _data.BenhNhans.Where(p => (p.Status == 1 || p.Status == 4 || p.Status == 5)).Where(p => p.NoiTru == 1 || p.NoiTru == 0)
                          where (bn.MaKP == _makp)
                          where (bn.MaKCB == DungChung.Bien.MaBV)
                          select new { bn.MaBNhan, bn.TenBNhan }).OrderBy(p => p.MaBNhan).OrderBy(p => p.TenBNhan).ToList();
            }

            if (DungChung.Bien.MaBV == "30010")
            {
                if (ckcBNKhac.Checked)
                {
                    pceDSBNhan.Visible = true;
                    labelControl2.Visible = true;
                    grcDSBNhan.DataSource = _lTKbn;
                }
                else
                {
                    pceDSBNhan.Visible = false;
                    labelControl2.Visible = false;
                }
            }
            else
            {
                if (ckcBNKhac.Checked)
                {
                    lupDSBNhan.Visible = true;
                    labelControl2.Visible = true;
                    lupDSBNhan.Properties.DataSource = _lTKbn;
                }
                else
                {
                    lupDSBNhan.Visible = false;
                    labelControl2.Visible = false;
                }
            }
        }

        private void dtNgayKe_EditValueChanged_1(object sender, EventArgs e)
        {
            setngayden();
        }

        private void chkSaoNhieuNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtNgayKeDen.Visible = chkSaoNhieuNgay.Checked;
            lblNgayKeDen.Visible = chkSaoNhieuNgay.Checked;
            labelControl6.Text = chkSaoNhieuNgay.Checked ? "Ngày kê từ:" : "Ngày kê:";
        }
    }
}