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
using System.Globalization;

namespace QLBV.FormNhap
{
    public partial class frm_CapNhatNgayGiuong : DevExpress.XtraEditors.XtraForm
    {
        public frm_CapNhatNgayGiuong()
        {
            InitializeComponent();
        }
        // status= 1 tính ngày giường chuyển khoa, 2 ngày ra viện
        public frm_CapNhatNgayGiuong(int maBN, DateTime ngayra, int status)
        {
            InitializeComponent();
            _maBN = maBN;
            _statusnew = status;
            _ngayRa = ngayra;
            //_makp = makp;
        }

        QLBV_Database.QLBVEntities data;
        int _maBN = 0, _statusnew = 0;
        //int _makp = 0;
        BenhNhan bnhan = new BenhNhan();

        //DateTime _ngayVao = new DateTime();
        DateTime _ngayRa = new DateTime();
        List<CanBo> _lcb = new List<CanBo>();
        public class MyObject
        {
            public int Value { set; get; }
            public string Text { set; get; }
        }
        string DTuong = ""; string iddtbn = "";
        //int TTLuu = 0;
        private void frm_CapNhatNgayGiuong_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (DungChung.Bien.MaBV.Substring(0, 2) != "24")
            {
                colXHH.Visible = false;
                ckXHH.Visible = false;
            }
            //chỉ kp có chẩn đoán mới chỉ định đc ngày giường
            var bnkb = data.BNKBs.Where(p => p.MaBNhan == _maBN).ToList();
            _lcb = data.CanBoes.Where(p => p.Status == 1 && p.MaKPsd != null && p.MaCCHN != null && p.MaCCHN != "").ToList();
            var _lkp = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).Select(p => new { p.MaKP, p.TenKP, p.PLoai }).OrderBy(p => p.PLoai).ToList();
            var kp = (from a in _lkp
                      join b in bnkb on a.MaKP equals b.MaKP
                      select new { a.MaKP, a.TenKP, b.IDKB }).Distinct().OrderByDescending(p => p.IDKB).ToList();
            //_lkp.Insert(0, new KPhong { MaKP = 0, TenKP = "" });
            lupKP.Properties.DataSource = kp;
            lupKP.Properties.DisplayMember = "TenKP";
            lupKP.Properties.ValueMember = "MaKP";
            //if (kp.Count > 0)
            //{
            //    lupKP.EditValue = kp.First().MaKP;
            //}

            //lupKPGr.DataSource = _lkp;
            lupKP.Enabled = false;
            //if (DungChung.Bien.CapDo == 9 || DungChung.Bien.PLoaiKP == "Admin")
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
            {
                lupKP.Enabled = true;
            }
            //else

            List<MyObject> _lTrongBH = new List<MyObject>();
            _lTrongBH.Add(new MyObject { Value = 0, Text = "Ngoài DM" });
            _lTrongBH.Add(new MyObject { Value = 1, Text = "Trong DM" });
            _lTrongBH.Add(new MyObject { Value = 2, Text = "Không TT" });
            lupTrongBH.DataSource = _lTrongBH;
            int noitru = 1;
            bnhan = data.BenhNhans.Where(p => p.MaBNhan == _maBN).FirstOrDefault();

            if (bnhan != null)
            {
                string iddtbn = ";" + (bnhan.IDDTBN == null ? "-100" : bnhan.IDDTBN.ToString()) + ";";
                noitru = bnhan.NoiTru ?? 1;
                lblHoten.Text = bnhan.TenBNhan;
                lblMaBN.Text = bnhan.MaBNhan.ToString();
                DTuong = bnhan.DTuong.ToString();
            }
            var vaovien = data.VaoViens.Where(panelControl1 => panelControl1.MaBNhan == _maBN).FirstOrDefault();
            if (vaovien != null && vaovien.NgayVao != null)
            {
                lblNgayVao.Text = vaovien.NgayVao.Value.ToString("dd/MM/yyyy hh:mm:ss tt", new CultureInfo("en-US"));

            }
            lblNgayRa.Text = _ngayRa.ToString("dd/MM/yyyy hh:mm:ss tt", new CultureInfo("en-US"));
            if (_statusnew == 1)
            {
                lblNgayRa.Text = _ngayRa.ToString("dd/MM/yyyy hh:mm:ss tt", new CultureInfo("en-US"));
                labngay.Text = "Ngày C.Khoa:";
            }
            else if (_statusnew == 2)
            {
                lblNgayRa.Text = _ngayRa.ToString("dd/MM/yyyy hh:mm:ss tt", new CultureInfo("en-US"));
                labngay.Text = "Ngày RV:";
            }
            else
            {
                lblNgayRa.Text = "";
                labngay.Text = "Ngày RV:";
            }
            loadData(0);
        }
        private void EnableControl(bool enable)
        {
            lupDV.Enabled = enable;
            LupNgaytu.Enabled = enable;
            LupNgayden.Enabled = enable;
            radTrongBH.Enabled = enable;
            ckXHH.Enabled = enable;
            txtTyLeTT.Enabled = enable;
            txtSoluong.Enabled = enable;
        }
        private void btnMoi_Click(object sender, EventArgs e)
        {
            lupDV.EditValue = 0;
            ckXHH.Checked = false;
            txtTyLeTT.Text = "100";
            btnLuu.Enabled = true;
            btnMoi.Enabled = false;
            btnsua.Enabled = false;
            txtSoluong.Visible = false;
            labelControl9.Visible = false;
            trangthai = 1;
            loadData(1);
            EnableTrongBH();
            grcTienGiuong.Enabled = false;
            lupKP.EditValue = DungChung.Bien.MaKP;
        }
        int trangthai = 0; // 1: Thêm mới; 2: Sửa

        public static DateTime backtimeam(DateTime dateTime)
        {
            var yesterday = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59).AddDays(-1);
            System.Globalization.GregorianCalendar PC = new System.Globalization.GregorianCalendar();
            PC.CalendarType = System.Globalization.GregorianCalendarTypes.USEnglish;
            return
            PC.GetHour(dateTime) > 11 ? dateTime :
            yesterday;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

            int MaKP = 0, madv = 0;
            DateTime _ngaytu = DateTime.Now;
            DateTime _ngayden = DateTime.Now;
            _ngaytu = LupNgaytu.DateTime;
            _ngayden = DungChung.Bien.MaBV == "14017" ? (LupNgayden.DateTime).AddDays(-1) : LupNgayden.DateTime;
            //_ngayden = LupNgayden.DateTime;//duc sua lai ngay 03/2
            if (lupKP.EditValue != null)
            {
                MaKP = Convert.ToInt32(lupKP.EditValue);
            }
            if (lupDV.EditValue != null)
                madv = Convert.ToInt32(lupDV.EditValue);
            if (CheckValidate())
            {
                if (trangthai == 1)
                {
                    List<NgayGiuongNew> _lngaygiuong = new List<NgayGiuongNew>();
                    _lngaygiuong = GetListNgayGiuong(data, _maBN, madv, _ngaytu, _ngayden, _status);
                    var _ldthuoc = data.DThuocs.Where(p => p.MaBNhan == _maBN).Where(p => p.PLDV == 2).ToList();
                    if (_ldthuoc.Count > 0)
                    {
                        int _iddon = _ldthuoc.First().IDDon;
                        foreach (var item in _lngaygiuong)
                        {
                            DThuocct moi = new DThuocct();
                            moi.IDDon = _iddon;
                            moi.MaDV = item.MaDV;
                            moi.SoLuong = 1;
                            moi.DonGia = item.DonGia;
                            moi.DonVi = item.DonVi;
                            moi.TrongBH = item.TrongBH;
                            moi.TyLeTT = DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017" ? 100 : (DTuong == "BHYT" ? item.TyLeTT : 100);
                            moi.MaKP = MaKP;
                            moi.XHH = item.XHH;
                            moi.MaCB = lupNguoiKe.EditValue.ToString();
                            moi.ThanhTien = DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017" ? item.DonGia : (item.DonGia * item.TyLeTT) / 100;
                            moi.NgayNhap = item.NgayNhap;
                            data.DThuoccts.Add(moi);
                            data.SaveChanges();
                        }
                    }
                    else
                    {
                        DThuoc moi = new DThuoc();
                        moi.MaKP = MaKP;
                        moi.MaCB = lupNguoiKe.EditValue.ToString();
                        moi.MaBNhan = _maBN;
                        moi.PLDV = 2;
                        moi.NgayKe = _ngaytu;
                        moi.KieuDon = -1;
                        //moi.SoPL = 0;
                        data.DThuocs.Add(moi);
                        if (data.SaveChanges() >= 0)
                        {
                            int _iddon = moi.IDDon;
                            foreach (var item in _lngaygiuong)
                            {
                                DThuocct moi1 = new DThuocct();
                                moi1.IDDon = _iddon;
                                moi1.MaDV = item.MaDV;
                                moi1.SoLuong = 1;
                                moi1.DonGia = item.DonGia;
                                moi1.DonVi = item.DonVi;
                                moi1.TrongBH = item.TrongBH;
                                moi1.MaCB = lupNguoiKe.EditValue.ToString();
                                moi1.TyLeTT = DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017" ? 100 : (DTuong == "BHYT" ? item.TyLeTT : 100);
                                moi1.XHH = item.XHH;
                                moi.MaKP = MaKP;
                                moi1.ThanhTien = DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017" ? item.DonGia : (item.DonGia * item.TyLeTT) / 100;
                                moi1.NgayNhap = item.NgayNhap;
                                data.DThuoccts.Add(moi1);
                                data.SaveChanges();
                            }
                        }
                    }

                }
                else if (trangthai == 2)
                {
                    if (grvTienGiuong.GetFocusedRowCellValue(coliddonct) != null)
                    {
                        double _dongia = Convert.ToDouble(grvTienGiuong.GetFocusedRowCellValue(coldongia));
                        int iddonct = Convert.ToInt32(grvTienGiuong.GetFocusedRowCellValue(coliddonct));
                        DThuocct sua = data.DThuoccts.Where(p => p.IDDonct == iddonct).FirstOrDefault();
                        if (sua != null)
                        {
                            sua.MaKP = Convert.ToInt32(lupKP.EditValue);
                            sua.MaDV = Convert.ToInt32(lupDV.EditValue);
                            sua.TrongBH = radTrongBH.SelectedIndex;
                            sua.TyLeTT = Convert.ToDouble(txtTyLeTT.Text);
                            sua.XHH = Convert.ToInt32(ckXHH.Checked);
                            sua.SoLuong = Convert.ToDouble(txtSoluong.Text);
                            sua.ThanhTien = (Convert.ToDouble(txtSoluong.Text) * Convert.ToDouble(txtTyLeTT.Text) * _dongia) / 100;
                            data.SaveChanges();
                            MessageBox.Show("Sửa thành công!");
                        }
                    }
                    txtSoluong.Visible = false;
                    labelControl9.Visible = false;
                }
                loadData(0);
                btnMoi.Enabled = true;
                btnsua.Enabled = true;
                btnLuu.Enabled = false;
            }
        }

        private bool CheckValidate()
        {
            if (lupKP.EditValue == null || lupKP.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn khoa phòng");
                return false;
            }
            else if (lupDV.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn dịch vụ ngày giường");
                return false;
            }
            else if (LupNgaytu.DateTime == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày từ");
                return false;
            }
            else if (LupNgayden.DateTime == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày đến");
                return false;
            }
            else if (LupNgayden.DateTime < LupNgaytu.DateTime)
            {
                MessageBox.Show("Ngày đến phải sau ngày từ");
                return false;
            }
            else
            {
                DateTime b = LupNgaytu.DateTime;
                DateTime a = LupNgayden.DateTime;
                TimeSpan c = a - b;
                if (c.TotalHours < 4)
                {
                    MessageBox.Show("số giờ nhỏ hơn 4, không thể thêm ngày giờ");
                    return false;
                }
                if (a > DateTime.Today.AddDays(1))
                {
                    if (DungChung.Bien.MaBV != "14017")
                    {
                        MessageBox.Show("Bạn chỉ có thể chỉ định ngày giường đến ngày hôm nay");
                        return false;
                    }
                }
            }
            if (lupNguoiKe.EditValue == null || lupNguoiKe.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn cán bộ");
                return false;
            }
            var ravien = data.RaViens.Where(p => p.MaBNhan == _maBN).FirstOrDefault();
            if (ravien != null)
            {
                MessageBox.Show("Bệnh nhân đã ra viện, ko thể thêm ngày giường !");
                return false;
            }
            if (trangthai == 2 && !string.IsNullOrEmpty(txtSoluong.Text))
            {
                if (Convert.ToDouble(txtSoluong.Text) <= 0)
                {
                    MessageBox.Show("Số lượng phải lớn hơn 0");
                    return false;
                }
            }
            return true;

        }
        int _status = 0;
        List<DichVu> _ldv = new List<DichVu>();

        public class DSGiuong
        {
            public int MaDV { get; set; }
            public string TenDV { get; set; }
            public string MaQD { get; set; }
            public int? MaKP { get; set; }
            public int? MaKPtk { get; set; }
            public int IDDonct { get; set; }
            public string DonVi { get; set; }
            public double DonGia { get; set; }
            public DateTime NgayNhap { get; set; }
            public double SoLuong { get; set; }
            public double ThanhTien { get; set; }
            public double TyLeTT { get; set; }
            public int XHH { get; set; }
            public string TrongBH { get; set; }
            public string TenKP { get; set; }
            public string Xoa { get; set; }
            public int SoLuongNguoi { get; set; }
        }
        private void loadData(int sss)
        {
            grcTienGiuong.Enabled = true;
            _ldv = (from n in data.NhomDVs.Where(p => p.TenNhomCT == "Giường điều trị ngoại trú" || p.TenNhomCT == "Giường điều trị nội trú")
                    join tn in data.TieuNhomDVs on n.IDNhom equals tn.IDNhom
                    join dv in data.DichVus on tn.IdTieuNhom equals dv.IdTieuNhom
                    select dv).ToList();
            var _lkp = data.KPhongs.ToList();
            var qdtct0 = (from
                         dt in data.DThuocs.Where(p => p.MaBNhan == _maBN)
                          join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
                          select dtct).Where(p => p.NgayNhap != null).ToList();
            var qdtct = (from dt in qdtct0
                         join dv in _ldv on dt.MaDV equals dv.MaDV
                         join kp in _lkp on dt.MaKP equals kp.MaKP
                         select new DSGiuong
                         {
                             MaDV = dv.MaDV,
                             TenDV = dv.TenDV,
                             MaQD = dv.MaQD,
                             MaKP = dt.MaKP,
                             MaKPtk = dt.MaKPtk,
                             IDDonct = dt.IDDonct,
                             DonVi = dt.DonVi,
                             DonGia = dt.DonGia,
                             NgayNhap = dt.NgayNhap.Value.Date,
                             SoLuong = dt.SoLuong,
                             ThanhTien = dt.ThanhTien,
                             TyLeTT = dt.TyLeTT,
                             XHH = dt.XHH,
                             TrongBH = dt.TrongBH == 0 ? "Ngoài DM" : "Trong DM",
                             TenKP = kp.TenKP,
                             Xoa = "Xóa",
                             SoLuongNguoi = 1,
                         }).OrderByDescending(p => p.NgayNhap).ToList();

            var listMaKP = qdtct.Select(o => o.MaKP).Distinct().ToList();
            var bnkb = (from kb in data.BNKBs.Where(o => listMaKP.Contains(o.MaKP))
                        join bn in data.BenhNhans.Where(o => o.Status != 3) on kb.MaBNhan equals bn.MaBNhan
                        join vv in data.VaoViens on kb.MaBNhan equals vv.MaBNhan
                        join rv1 in data.RaViens on kb.MaBNhan equals rv1.MaBNhan into kq
                        from rv in kq.DefaultIfEmpty()
                        select new
                        {
                            kb.IDKB,
                            kb.MaKP,
                            kb.Buong,
                            kb.Giuong,
                            kb.MaBNhan,
                            kb.NgayChuyenBG,
                            NgayVao = vv.NgayVao ?? DateTime.MinValue,
                            NgayRa = rv != null ? rv.NgayRa : null
                        }).ToList();

            foreach (var item in qdtct)
            {
                var bnkbMaKP = bnkb.FirstOrDefault(o => o.MaKP == item.MaKP && o.MaBNhan == _maBN);
                if (bnkbMaKP != null)
                {
                    List<DateTime> ngayChuyens = new List<DateTime>();
                    List<string> buongs = new List<string>();
                    List<string> giuongs = new List<string>();
                    if (!string.IsNullOrWhiteSpace(bnkbMaKP.NgayChuyenBG))
                    {
                        var spNgayChuyen = bnkbMaKP.NgayChuyenBG.Split(';').Where(o => !string.IsNullOrWhiteSpace(o));
                        foreach (var item1 in spNgayChuyen)
                        {
                            DateTime dt = new DateTime();
                            if (DateTime.TryParse(item1, out dt))
                            {
                                ngayChuyens.Add(dt.Date);
                            }
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(bnkbMaKP.Buong))
                    {
                        buongs = bnkbMaKP.Buong.Split(';').Where(o => !string.IsNullOrWhiteSpace(o)).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(bnkbMaKP.Giuong))
                    {
                        giuongs = bnkbMaKP.Giuong.Split(';').Where(o => !string.IsNullOrWhiteSpace(o)).ToList();
                    }

                    string buong = "";
                    string giuong = "";
                    if (ngayChuyens.Count <= 0)
                    {
                        if (buongs.Count() > 0)
                        {
                            buong = buongs.Last();
                        }
                        if (giuongs.Count() > 0)
                        {
                            giuong = giuongs.Last();
                        }
                    }
                    else
                    {
                        for (int i = 0; i < ngayChuyens.Count; i++)
                        {
                            if (buongs.Count > (i + 1) && giuongs.Count > (i + 1))
                            {
                                if (i == (ngayChuyens.Count - 1) && item.NgayNhap.Date >= ngayChuyens[i] && (bnkbMaKP.NgayRa != null ? (item.NgayNhap.Date < bnkbMaKP.NgayRa) : true))
                                {
                                    buong = buongs[i + 1];
                                    giuong = giuongs[i + 1];
                                    break;
                                }
                                else if (ngayChuyens.Count > (i + 1) && ngayChuyens[i] <= item.NgayNhap.Date && ngayChuyens[i + 1] > item.NgayNhap.Date)
                                {
                                    buong = buongs[i + 1];
                                    giuong = giuongs[i + 1];
                                    break;
                                }
                                else if (i == 0 && ngayChuyens[i] > item.NgayNhap.Date && item.NgayNhap.Date >= bnkbMaKP.NgayVao.Date)
                                {
                                    buong = buongs[i];
                                    giuong = giuongs[i];
                                    break;
                                }
                            }
                            //if (ngayChuyens[i] > item.NgayNhap.Date && buongs.Count > (i + 1) && giuongs.Count > (i + 1))
                            //{
                            //    buong = buongs[i + 1];
                            //    giuong = giuongs[i + 1];
                            //    //item.SoLuongNguoi = bnkb.Where(o => o.MaKP == item.MaKP && o.Buong.Split(';').Where(p => !string.IsNullOrWhiteSpace(p)).Contains(buongs[i + 1]) && o.Giuong.Split(';').Where(p => !string.IsNullOrWhiteSpace(p)).Contains(giuongs[i + 1]) && o.NgayVao <= item.NgayNhap && (o.NgayRa != null ? o.NgayRa > item.NgayNhap : true)).Count();
                            //    break;
                            //}
                            //else if (ngayChuyens.Count > (i + 1) && ngayChuyens[i + 1] > item.NgayNhap.Date && buongs.Count > (i + 2) && giuongs.Count > (i + 2))
                            //{
                            //    buong = buongs[i + 2];
                            //    giuong = giuongs[i + 2];
                            //    break;
                            //}
                            //else if (bnkbMaKP.NgayRa > item.NgayNhap.Date && buongs.Count > (i + 1) && giuongs.Count > (i + 1))
                            //{
                            //    buong = buongs[i + 1];
                            //    giuong = giuongs[i + 1];
                            //    break;
                            //}
                        }
                    }
                    int soNguoi = 0;
                    if (string.IsNullOrWhiteSpace(buong) || string.IsNullOrWhiteSpace(giuong))
                        soNguoi = 0;
                    else
                    {
                        foreach (var bg in bnkb.Where(o => o.MaKP == item.MaKP && o.Buong.Split(';').Contains(buong) && o.Giuong.Split(';').Contains(giuong)))
                        {
                            List<DateTime> ngayChuyenTinhs = new List<DateTime>();
                            List<string> buongTinhs = new List<string>();
                            List<string> giuongTinhs = new List<string>();
                            if (!string.IsNullOrWhiteSpace(bg.NgayChuyenBG))
                            {
                                var spNgayChuyen = bg.NgayChuyenBG.Split(';').Where(o => !string.IsNullOrWhiteSpace(o));
                                foreach (var item1 in spNgayChuyen)
                                {
                                    DateTime dt = new DateTime();
                                    if (DateTime.TryParse(item1, out dt))
                                    {
                                        ngayChuyenTinhs.Add(dt.Date);
                                    }
                                }
                            }
                            if (!string.IsNullOrWhiteSpace(bg.Buong))
                            {
                                buongTinhs = bg.Buong.Split(';').Where(o => !string.IsNullOrWhiteSpace(o)).ToList();
                            }
                            if (!string.IsNullOrWhiteSpace(bg.Giuong))
                            {
                                giuongTinhs = bg.Giuong.Split(';').Where(o => !string.IsNullOrWhiteSpace(o)).ToList();
                            }

                            if (ngayChuyenTinhs.Count <= 0 && bg.NgayVao.Date <= item.NgayNhap.Date && (bg.NgayRa != null ? bg.NgayRa.Value.Date > item.NgayNhap.Date : true))
                            {
                                soNguoi++;
                            }
                            else
                            {
                                for (int i = 0; i < ngayChuyenTinhs.Count; i++)
                                {
                                    if (buongTinhs.Count > (i + 1) && giuongTinhs.Count > (i + 1))
                                    {
                                        if (i == (ngayChuyenTinhs.Count - 1) && item.NgayNhap.Date >= ngayChuyenTinhs[i] && buongTinhs[i + 1] == buong && giuongTinhs[i + 1] == giuong)
                                        {
                                            soNguoi++;
                                            break;
                                        }
                                        else if (ngayChuyenTinhs.Count > i + 1 && ngayChuyenTinhs[i] <= item.NgayNhap.Date && ngayChuyenTinhs[i + 1] > item.NgayNhap.Date && buongTinhs[i + 1] == buong && giuongTinhs[i + 1] == giuong)
                                        {
                                            soNguoi++;
                                            break;
                                        }
                                        else if (i == 0 && item.NgayNhap.Date < ngayChuyenTinhs[i] && buongTinhs[i] == buong && giuongTinhs[i] == giuong)
                                        {
                                            soNguoi++;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (soNguoi == 0)
                        soNguoi = 1;
                    item.SoLuongNguoi = soNguoi;
                }
            }

            grcTienGiuong.DataSource = qdtct;
            if (qdtct.Count > 0)
            {
                LupNgaytu.DateTime = qdtct.First().NgayNhap;
                LupNgayden.DateTime = qdtct.Last().NgayNhap;
                grvTienGiuong_FocusedRowChanged(null, null);
            }
            var _lvaovien = data.VaoViens.Where(p => p.MaBNhan == _maBN).FirstOrDefault();
            DateTime _TuNgay = DateTime.Now;
            DateTime _DenNgay = DateTime.Now;
            if (sss > 0)
            {
                EnableControl(true);
                switch (_statusnew)
                {
                    case 1: //tạo ngày giường sau khi lưu chuyển khoa điều trị
                        if (qdtct.Count > 0)
                        {

                            _TuNgay = qdtct.First().NgayNhap;
                            if (_TuNgay.Date >= _ngayRa)
                            {
                                MessageBox.Show("Bệnh nhân đã có chỉ định ngày đến ngày: " + _ngayRa.ToShortDateString() + " bạn không thể chỉ định thêm ngày giường !");
                                //EnableControl(false);
                                btnXoa_Click(null, null);
                            }
                            else
                            {
                                LupNgaytu.DateTime = DungChung.Ham.NgayTu(_TuNgay.AddDays(1));
                                LupNgaytu.Enabled = false;
                                LupNgayden.DateTime = _ngayRa;
                                _status = 2;
                                LupNgayden.Enabled = false;
                            }
                        }
                        else
                        {
                            grcTienGiuong.DataSource = null;
                            if (_lvaovien != null)
                            {
                                _TuNgay = _lvaovien.NgayVao.Value;
                                LupNgaytu.DateTime = _TuNgay;
                                LupNgaytu.Enabled = false;
                                LupNgayden.DateTime = _ngayRa;
                                _status = 1;
                                LupNgayden.Enabled = false;
                            }
                        }
                        break;

                    case 2:
                        if (qdtct.Count > 0)
                        {

                            _TuNgay = qdtct.First().NgayNhap;
                            if (_TuNgay.ToShortDateString() == _ngayRa.ToShortDateString())
                            {
                                int iddonct = qdtct.First().IDDonct;
                                DThuocct sua = data.DThuoccts.Where(p => p.IDDonct == iddonct).FirstOrDefault();
                                if (_TuNgay.Hour < 12)
                                {
                                    sua.TyLeTT = 50;
                                }
                                else
                                {
                                    sua.TyLeTT = 70;
                                }
                                data.SaveChanges();
                                EnableControl(false);
                            }
                            else
                            {
                                LupNgaytu.DateTime = DungChung.Ham.NgayTu(_TuNgay.AddDays(1));
                                LupNgaytu.Enabled = false;
                                LupNgayden.DateTime = _ngayRa;
                                _status = 3;
                                LupNgayden.Enabled = false;
                            }
                        }
                        else
                        {
                            grcTienGiuong.DataSource = null;
                            if (_lvaovien != null)
                            {
                                _TuNgay = _lvaovien.NgayVao.Value;
                                LupNgaytu.DateTime = _TuNgay;
                                LupNgaytu.Enabled = false;
                                LupNgayden.DateTime = _ngayRa;
                                _status = 4;
                                LupNgayden.Enabled = false;
                            }
                        }
                        break;
                    case 3:
                        if (qdtct.Count > 0)
                        {
                            _TuNgay = qdtct.First().NgayNhap;
                            LupNgaytu.DateTime = DungChung.Ham.NgayTu(_TuNgay.AddDays(1));
                            LupNgaytu.Enabled = false;
                            LupNgayden.DateTime = _ngayRa;
                            _status = 2;
                            LupNgayden.Enabled = true;
                        }
                        else
                        {
                            grcTienGiuong.DataSource = null;
                            if (_lvaovien != null)
                            {
                                _TuNgay = _lvaovien.NgayVao.Value;
                                LupNgaytu.DateTime = _TuNgay;
                                LupNgaytu.Enabled = false;
                                LupNgayden.DateTime = _ngayRa;
                                _status = 1;
                                LupNgayden.Enabled = true;
                            }
                        }
                        break;
                }
            }
            else
                EnableControl(false);
        }

        public class BC
        {
            public int MaKPtk { set; get; }
            public DateTime DenNgay { get; set; }
            public double DonGia { get; set; }
            public int DonVi { get; set; }
            public int MaDV { get; set; }
            public int? MaKP { get; set; }
            public int TrongBH { get; set; }
            public DateTime TuNgay { get; set; }
            public double TyLeTT { get; set; }
            public int XHH { get; set; }
            public string MaQD { get; set; }
            public double SoLuong { get; internal set; }
        }
        public class dsNgay
        {
            public DateTime tungay { set; get; }
            public DateTime denngay { set; get; }
        }

        public List<dsNgay> GetDSNgay(List<DateTime> _list)
        {
            _list = _list.Distinct().OrderBy(p => p).ToList();
            DateTime ngayMin = _list.Min();
            DateTime ngayMax = _list.Max();

            List<dsNgay> _lds = new List<dsNgay>();

            dsNgay moi = new dsNgay();
            moi.tungay = _list.Min();
            moi.denngay = _list.Min();
            for (int i = 0; ngayMin.AddDays(i) <= ngayMax; i++)
            {
                if (ngayMin.AddDays(i) == ngayMax)
                {
                    if (moi.tungay == new DateTime(1, 1, 1))
                        moi.tungay = ngayMax;
                    moi.denngay = ngayMax;
                    _lds.Add(moi);
                    break;
                }
                else
                {
                    if (_list.Contains(ngayMin.AddDays(i)))
                    {

                        if (moi.tungay == new DateTime(1, 1, 1) && moi.denngay == new DateTime(1, 1, 1))
                        {
                            moi.tungay = ngayMin.AddDays(i);
                            moi.denngay = ngayMin.AddDays(i);
                        }
                        else
                            moi.denngay = ngayMin.AddDays(i);
                    }
                    else
                    {
                        if (moi.tungay != new DateTime(1, 1, 1) && moi.denngay != new DateTime(1, 1, 1))
                            _lds.Add(moi);
                        moi = new dsNgay();

                    }
                }
            }
            return _lds;
        }
        private void lupDV_EditValueChanged(object sender, EventArgs e)
        {
            EnableTrongBH();
        }

        private void grvTienGiuong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvTienGiuong.GetFocusedRowCellValue(coliddonct) != null)
            {

                int _iddonct = Convert.ToInt32(grvTienGiuong.GetFocusedRowCellValue(coliddonct));
                var _ldtct = data.DThuoccts.Where(p => p.IDDonct == _iddonct).FirstOrDefault();
                if (_ldtct != null)
                {
                    lupKP.EditValue = _ldtct.MaKP;
                    lupDV.EditValue = _ldtct.MaDV;
                    LupNgaytu.DateTime = DungChung.Ham.NgayTu(_ldtct.NgayNhap.Value);
                    LupNgayden.DateTime = DungChung.Ham.NgayDen(_ldtct.NgayNhap.Value);
                    radTrongBH.SelectedIndex = _ldtct.TrongBH;
                    txtTyLeTT.Text = _ldtct.TyLeTT.ToString();
                    ckXHH.Checked = Convert.ToBoolean(_ldtct.XHH);
                    txtSoluong.Text = _ldtct.SoLuong.ToString();
                }
            }

        }

        private void btnSua_Click(object sender, EventArgs e)
        {

        }
        private void lupKP_EditValueChanged(object sender, EventArgs e)
        {
            #region loadDV
            string MaKP = "0";
            if (lupKP.EditValue != null)
                MaKP = ";" + lupKP.EditValue.ToString() + ";";
            var dscb = _lcb.Where(p => p.MaKPsd.Contains(MaKP)).OrderBy(p => p.TenCB).ToList();
            lupNguoiKe.Properties.DataSource = dscb;

            List<DichVu> _ldvu = new List<DichVu>();
            _ldvu = (from n in data.NhomDVs.Where(p => p.IDNhom == 14 || p.IDNhom == 15)//.Where(p => bnhan.NoiTru == 0 ? (p.TenNhomCT == "Giường điều trị ngoại trú") : (p.TenNhomCT == "Giường điều trị nội trú"))
                     join tn in data.TieuNhomDVs on n.IDNhom equals tn.IDNhom
                     join dv in data.DichVus.Where(p => p.Status == 1 && p.DSDTBN != null && p.DSDTBN.Contains(iddtbn)).Where(p => p.MaKPsd.Contains(MaKP)) on tn.IdTieuNhom equals dv.IdTieuNhom
                     select dv).ToList();
            //_ldvu.Insert(0, new DichVu { MaDV = 0, TenDV = "" });
            //bool Giacu=DungChung.Ham.GiaCu()
            var dsgiuong = (from dv in _ldvu
                            select new
                            {
                                dv.MaDV,
                                dv.TenDV,
                                DonGia = DTuong == "BHYT" ? dv.DonGiaTT39 : dv.DonGiaTT15,
                                dv.MaQD
                            }).ToList();
            lupDV.Properties.DataSource = dsgiuong;

            var qcls = (from cls in data.CLS.Where(p => p.MaBNhan == _maBN)
                        join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                        join dv in data.DichVus on cd.MaDV equals dv.MaDV
                        join tn in data.TieuNhomDVs.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat) on dv.IdTieuNhom equals tn.IdTieuNhom
                        select dv
                            ).FirstOrDefault();
            if (qcls != null)
            {

                List<DichVu> qdvchon = new List<DichVu>();

                if (qcls.Loai == 1)
                {
                    qdvchon = _ldvu.Where(p => p.TenDV.Contains("loại 1")).Where(p => p.TrongDM == qcls.TrongDM).ToList();
                }
                else if (qcls.Loai == 2)
                {
                    qdvchon = _ldvu.Where(p => p.TenDV.Contains("loại 2")).Where(p => p.TrongDM == qcls.TrongDM).ToList();
                }
                else if (qcls.Loai == 3)
                {
                    qdvchon = _ldvu.Where(p => p.TenDV.Contains("loại 3")).Where(p => p.TrongDM == qcls.TrongDM).ToList();
                }
                if (qdvchon.Count > 0)
                    lupDV.EditValue = qdvchon.First().MaDV;

            }
            #endregion
        }

        private void grcTienGiuong_DataSourceChanged(object sender, EventArgs e)
        {
            // grvTienGiuong_FocusedRowChanged(null, null);

        }

        private void grvTienGiuong_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            //if (trangthai == 1 || trangthai == 2)
            //{
            //    DialogResult _result = MessageBox.Show("Dữ liệu chưa được lưu, bạn có muốn lưu không ?", "thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (_result == DialogResult.Yes)
            //    {
            //        btnLuu_Click(null, null);

            //    }
            //}

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            EnableControl(true);
            btnLuu.Enabled = false;
            btnMoi.Enabled = true;
            grcTienGiuong.Enabled = true;
            grvTienGiuong_FocusedRowChanged(null, null);
        }

        private void grvTienGiuong_DataSourceChanged(object sender, EventArgs e)
        {
            //grvTienGiuong_FocusedRowChanged(null, null);
        }

        private void ckXHH_CheckedChanged(object sender, EventArgs e)
        {
            if (ckXHH.Checked)
            {
                radTrongBH.Enabled = false;
                radTrongBH.SelectedIndex = 0;

            }
            else
                EnableTrongBH();
        }

        private void EnableTrongBH()
        {
            int maDV = 0;
            radTrongBH.Enabled = false;
            if (lupDV.EditValue != null)
            {
                maDV = Convert.ToInt32(lupDV.EditValue);
            }
            if (maDV > 0 && bnhan != null)
            {
                if (bnhan.DTuong != "BHYT")
                {
                    radTrongBH.SelectedIndex = 0;

                }
                else
                {
                    var qdv = data.DichVus.Where(p => p.MaDV == maDV).FirstOrDefault();
                    if (qdv != null)
                    {
                        if (qdv.TrongDM == 1)
                        {
                            radTrongBH.Enabled = true;
                        }

                        radTrongBH.SelectedIndex = qdv.TrongDM ?? 0;
                    }
                    else
                        radTrongBH.SelectedIndex = 0;
                }
            }
            else
                radTrongBH.SelectedIndex = 0;
        }
        public class NgayGiuongNew
        {
            public int MaDV { get; set; }
            public double DonGia { get; set; }
            public string DonVi { get; set; }
            public int TyLeTT { get; set; }
            public int TrongBH { get; set; }
            public int XHH { get; set; }
            public DateTime NgayNhap { get; set; }
        }
        #region Tạo list ngày giường
        //_status=1:Bệnh nhân chỉ định ngày giường lần đầu,chưa ra viện
        //_status=2:Bệnh nhân chưa ra viện nhưng đã chỉ định ngày giường hơn 1 lần
        //_status=3:Bệnh nhân đã ra viện nhưng đã chỉ định ngày giường hơn 1 lần
        //_status=4:Bệnh nhân đã ra viện, nhưng chưa chỉ định ngày giường
        public List<NgayGiuongNew> GetListNgayGiuong(QLBV_Database.QLBVEntities _data, int _MaBN, int _MaDV, DateTime _TuNgay, DateTime _DenNgay, int _Status)
        {
            List<NgayGiuongNew> KetQua = new List<NgayGiuongNew>();
            double _Dongia = 0;
            int giacu = 39;
            int tylettngayvao = 100;
            int _TrongBH = radTrongBH.SelectedIndex;
            int _TyLeTT = Convert.ToInt32(txtTyLeTT.Text);
            int _XHH = Convert.ToInt32(ckXHH.Checked);
            var _ldichvu = _data.DichVus.Where(p => p.MaDV == _MaDV).FirstOrDefault();
            string donvi = _ldichvu.DonVi;
            switch (_Status)
            {
                case 1:

                    giacu = DungChung.Ham.GiaCu(_MaBN, _TrongBH);
                    if (radTrongBH.SelectedIndex == 1)
                    {
                        if (giacu == 15)
                            _Dongia = _ldichvu.DonGiaTT15;
                        else if (giacu == 39)
                            _Dongia = _ldichvu.DonGiaTT39;
                        else if (giacu == 0)
                            _Dongia = _ldichvu.DonGiaBHYT;
                    }
                    else
                    {
                        if (giacu == 0)
                            _Dongia = _ldichvu.DonGia2;
                        else
                            _Dongia = _ldichvu.DonGiaDV2;
                    }
                    if (_TuNgay.Hour < 12)
                        tylettngayvao = 70;
                    else
                        tylettngayvao = 50;
                    //add ngay giương ngày vào
                    NgayGiuongNew moi = new NgayGiuongNew();
                    moi.MaDV = _MaDV;
                    moi.DonGia = _Dongia;
                    moi.DonVi = donvi;
                    moi.TrongBH = _TrongBH;
                    moi.TyLeTT = _TrongBH == 1 ? tylettngayvao : _TyLeTT;
                    moi.XHH = _XHH;
                    moi.NgayNhap = _TuNgay.AddSeconds(5);
                    KetQua.Add(moi);

                    DateTime ngaytu = DungChung.Ham.NgayDen(_TuNgay);
                    DateTime ngayden = DungChung.Ham.NgayDen(_DenNgay);
                    TimeSpan TongNgay = ngayden - ngaytu;
                    int _songay = 0;


                    _songay = TongNgay.Days;


                    if (_songay > 0)
                    {
                        for (int i = 0; i < _songay; i++)
                        {
                            DateTime _ngaynhap = _TuNgay.AddDays(i + 1);
                            double _DongiaN = 0;
                            giacu = DungChung.Ham.GiaCu(_MaBN, _TrongBH);
                            if (radTrongBH.SelectedIndex == 1)
                            {
                                if (giacu == 15)
                                    _DongiaN = _ldichvu.DonGiaTT15;
                                else if (giacu == 39)
                                    _DongiaN = _ldichvu.DonGiaTT39;
                                else if (giacu == 0)
                                    _DongiaN = _ldichvu.DonGiaBHYT;
                            }
                            else
                            {
                                if (giacu == 0)
                                    _DongiaN = _ldichvu.DonGia2;
                                else
                                    _DongiaN = _ldichvu.DonGiaDV2;
                            }

                            NgayGiuongNew moi1 = new NgayGiuongNew();
                            moi1.MaDV = _MaDV;
                            moi1.DonGia = _DongiaN;
                            moi1.DonVi = donvi;
                            moi1.TrongBH = _TrongBH;
                            moi1.TyLeTT = _TrongBH == 1 ? 100 : _TyLeTT;
                            moi1.XHH = _XHH;
                            moi1.NgayNhap = _ngaynhap;
                            KetQua.Add(moi1);
                        }
                        return KetQua;
                    }
                    break;
                case 2:
                    DateTime ngaytu2 = DungChung.Ham.NgayTu(_TuNgay);
                    DateTime ngayden2 = DungChung.Ham.NgayTu(_DenNgay);
                    if (ngaytu2 == ngayden2)
                    {
                        DateTime _ngaynhap = ngaytu2;
                        double _DongiaN = 0;
                        giacu = DungChung.Ham.GiaCu(_MaBN, _TrongBH);
                        if (radTrongBH.SelectedIndex == 1)
                        {
                            if (giacu == 15)
                                _DongiaN = _ldichvu.DonGiaTT15;
                            else if (giacu == 39)
                                _DongiaN = _ldichvu.DonGiaTT39;
                            else if (giacu == 0)
                                _DongiaN = _ldichvu.DonGiaBHYT;
                        }
                        else
                        {
                            if (giacu == 0)
                                _DongiaN = _ldichvu.DonGia2;
                            else
                                _DongiaN = _ldichvu.DonGiaDV2;
                        }

                        NgayGiuongNew moi1 = new NgayGiuongNew();
                        moi1.MaDV = _MaDV;
                        moi1.DonGia = _DongiaN;
                        moi1.DonVi = donvi;
                        moi1.TrongBH = _TrongBH;
                        moi1.TyLeTT = _TrongBH == 1 ? 100 : _TyLeTT;
                        moi1.XHH = _XHH;
                        moi1.NgayNhap = _ngaynhap.AddMinutes(5);
                        KetQua.Add(moi1);
                        return KetQua;
                    }
                    else
                    {
                        TimeSpan TongNgay2 = ngayden2 - ngaytu2;
                        int _songay2 = TongNgay2.Days;
                        if (_songay2 > 0)
                        {
                            for (int i = 0; i <= _songay2; i++)
                            {
                                DateTime _ngaynhap = _TuNgay.AddDays(i);
                                double _DongiaN = 0;
                                giacu = DungChung.Ham.GiaCu(_MaBN, _TrongBH);
                                if (radTrongBH.SelectedIndex == 1)
                                {
                                    if (giacu == 15)
                                        _DongiaN = _ldichvu.DonGiaTT15;
                                    else if (giacu == 39)
                                        _DongiaN = _ldichvu.DonGiaTT39;
                                    else if (giacu == 0)
                                        _DongiaN = _ldichvu.DonGiaBHYT;
                                }
                                else
                                {
                                    if (giacu == 0)
                                        _DongiaN = _ldichvu.DonGia2;
                                    else
                                        _DongiaN = _ldichvu.DonGiaDV2;
                                }

                                NgayGiuongNew moi1 = new NgayGiuongNew();
                                moi1.MaDV = _MaDV;
                                moi1.DonGia = _DongiaN;
                                moi1.DonVi = donvi;
                                moi1.TrongBH = _TrongBH;
                                moi1.TyLeTT = _TrongBH == 1 ? 100 : _TyLeTT;
                                moi1.XHH = _XHH;
                                moi1.NgayNhap = _ngaynhap;
                                KetQua.Add(moi1);
                            }
                            return KetQua;
                        }
                    }
                    break;

                case 3:
                    giacu = DungChung.Ham.GiaCu(_MaBN, _TrongBH);
                    if (radTrongBH.SelectedIndex == 1)
                    {
                        if (giacu == 15)
                            _Dongia = _ldichvu.DonGiaTT15;
                        else if (giacu == 39)
                            _Dongia = _ldichvu.DonGiaTT39;
                        else if (giacu == 0)
                            _Dongia = _ldichvu.DonGiaBHYT;
                    }
                    else
                    {
                        if (giacu == 0)
                            _Dongia = _ldichvu.DonGia2;
                        else
                            _Dongia = _ldichvu.DonGiaDV2;
                    }
                    if (_DenNgay.Hour < 12)
                        tylettngayvao = 50;
                    else
                        tylettngayvao = 70;
                    //add ngay giương ngày ra
                    NgayGiuongNew moi3 = new NgayGiuongNew();
                    moi3.MaDV = _MaDV;
                    moi3.DonGia = _Dongia;
                    moi3.DonVi = donvi;
                    moi3.TrongBH = _TrongBH;
                    moi3.TyLeTT = _TrongBH == 1 ? tylettngayvao : _TyLeTT;
                    moi3.XHH = _XHH;
                    moi3.NgayNhap = _DenNgay.AddSeconds(-5);
                    KetQua.Add(moi3);

                    DateTime tungay3 = DungChung.Ham.NgayTu(_TuNgay);
                    DateTime denngay3 = DungChung.Ham.NgayTu(_DenNgay);
                    TimeSpan Tongngay3 = denngay3 - tungay3;
                    int songay3 = Tongngay3.Days;
                    if (songay3 > 0)
                    {
                        for (int i = 0; i < songay3; i++)
                        {
                            DateTime _ngaynhap = _TuNgay.AddDays(i);
                            double _DongiaN = 0;
                            giacu = DungChung.Ham.GiaCu(_MaBN, _TrongBH);
                            if (radTrongBH.SelectedIndex == 1)
                            {
                                if (giacu == 15)
                                    _DongiaN = _ldichvu.DonGiaTT15;
                                else if (giacu == 39)
                                    _DongiaN = _ldichvu.DonGiaTT39;
                                else if (giacu == 0)
                                    _DongiaN = _ldichvu.DonGiaBHYT;
                            }
                            else
                            {
                                if (giacu == 0)
                                    _DongiaN = _ldichvu.DonGia2;
                                else
                                    _DongiaN = _ldichvu.DonGiaDV2;
                            }

                            NgayGiuongNew moi1 = new NgayGiuongNew();
                            moi1.MaDV = _MaDV;
                            moi1.DonGia = _DongiaN;
                            moi1.DonVi = donvi;
                            moi1.TrongBH = _TrongBH;
                            moi1.TyLeTT = _TrongBH == 1 ? 100 : _TyLeTT;
                            moi1.XHH = _XHH;
                            moi1.NgayNhap = _ngaynhap;
                            KetQua.Add(moi1);
                        }
                        return KetQua;
                    }
                    break;

                case 4:

                    DateTime ngayvao4 = DungChung.Ham.NgayTu(_TuNgay);
                    DateTime ngayra4 = DungChung.Ham.NgayTu(_DenNgay);
                    if (ngayvao4 == ngayra4)
                    {
                        double _DongiaN = 0;
                        giacu = DungChung.Ham.GiaCu(_MaBN, _TrongBH);
                        if (radTrongBH.SelectedIndex == 1)
                        {
                            if (giacu == 15)
                                _DongiaN = _ldichvu.DonGiaTT15;
                            else if (giacu == 39)
                                _DongiaN = _ldichvu.DonGiaTT39;
                            else if (giacu == 0)
                                _DongiaN = _ldichvu.DonGiaBHYT;
                        }
                        else
                        {
                            if (giacu == 0)
                                _DongiaN = _ldichvu.DonGia2;
                            else
                                _DongiaN = _ldichvu.DonGiaDV2;
                        }

                        if (_TuNgay.Hour < 12)
                        {
                            if (_DenNgay.Hour < 12)
                            {
                                NgayGiuongNew moi4 = new NgayGiuongNew();
                                moi4.MaDV = _MaDV;
                                moi4.DonGia = _DongiaN;
                                moi4.DonVi = donvi;
                                moi4.TyLeTT = _TrongBH == 1 ? 35 : _TyLeTT;
                                moi4.XHH = _XHH;
                                moi4.TrongBH = _TrongBH;
                                moi4.NgayNhap = _TuNgay.AddMinutes(5);
                                KetQua.Add(moi4);
                                return KetQua;
                            }
                            else
                            {
                                NgayGiuongNew moi4 = new NgayGiuongNew();
                                moi4.MaDV = _MaDV;
                                moi4.DonGia = _DongiaN;
                                moi4.DonVi = donvi;
                                moi4.TyLeTT = _TrongBH == 1 ? 49 : _TyLeTT;
                                moi4.XHH = _XHH;
                                moi4.TrongBH = _TrongBH;
                                moi4.NgayNhap = _TuNgay.AddMinutes(5);
                                KetQua.Add(moi4);
                                return KetQua;
                            }
                        }
                        else
                        {
                            NgayGiuongNew moi4 = new NgayGiuongNew();
                            moi4.MaDV = _MaDV;
                            moi4.DonGia = _DongiaN;
                            moi4.DonVi = donvi;
                            moi4.TyLeTT = _TrongBH == 1 ? 49 : _TyLeTT;
                            moi4.XHH = _XHH;
                            moi4.TrongBH = _TrongBH;
                            moi4.NgayNhap = _TuNgay.AddMinutes(5);
                            KetQua.Add(moi4);
                            return KetQua;
                        }
                    }
                    else
                    {
                        //ad tiền giường ngày vào
                        if (_TuNgay.Hour < 12)
                        {
                            giacu = DungChung.Ham.GiaCu(_MaBN, _TrongBH);
                            if (radTrongBH.SelectedIndex == 1)
                            {
                                if (giacu == 15)
                                    _Dongia = _ldichvu.DonGiaTT15;
                                else if (giacu == 39)
                                    _Dongia = _ldichvu.DonGiaTT39;
                                else if (giacu == 0)
                                    _Dongia = _ldichvu.DonGiaBHYT;
                            }
                            else
                            {
                                if (giacu == 0)
                                    _Dongia = _ldichvu.DonGia2;
                                else
                                    _Dongia = _ldichvu.DonGiaDV2;
                            }
                            NgayGiuongNew moi5 = new NgayGiuongNew();
                            moi5.MaDV = _MaDV;
                            moi5.DonGia = _Dongia;
                            moi5.DonVi = donvi;
                            moi5.TrongBH = _TrongBH;
                            moi5.TyLeTT = _TrongBH == 1 ? 70 : _TyLeTT;
                            moi5.XHH = _XHH;
                            moi5.NgayNhap = _TuNgay.AddSeconds(5);
                            KetQua.Add(moi5);
                        }
                        else
                        {
                            giacu = DungChung.Ham.GiaCu(_MaBN, _TrongBH);
                            if (radTrongBH.SelectedIndex == 1)
                            {
                                if (giacu == 15)
                                    _Dongia = _ldichvu.DonGiaTT15;
                                else if (giacu == 39)
                                    _Dongia = _ldichvu.DonGiaTT39;
                                else if (giacu == 0)
                                    _Dongia = _ldichvu.DonGiaBHYT;
                            }
                            else
                            {
                                if (giacu == 0)
                                    _Dongia = _ldichvu.DonGia2;
                                else
                                    _Dongia = _ldichvu.DonGiaDV2;
                            }
                            NgayGiuongNew moi5 = new NgayGiuongNew();
                            moi5.MaDV = _MaDV;
                            moi5.DonGia = _Dongia;
                            moi5.DonVi = donvi;
                            moi5.TrongBH = _TrongBH;
                            moi5.TyLeTT = _TrongBH == 1 ? 50 : _TyLeTT;
                            moi5.XHH = _XHH;
                            moi5.NgayNhap = _TuNgay.AddSeconds(5);
                            KetQua.Add(moi5);
                        }
                        //ad ngày giường ngày ra
                        if (_DenNgay.Hour < 12)
                        {
                            giacu = DungChung.Ham.GiaCu(_MaBN, _TrongBH);
                            if (radTrongBH.SelectedIndex == 1)
                            {
                                if (giacu == 15)
                                    _Dongia = _ldichvu.DonGiaTT15;
                                else if (giacu == 39)
                                    _Dongia = _ldichvu.DonGiaTT39;
                                else if (giacu == 0)
                                    _Dongia = _ldichvu.DonGiaBHYT;
                            }
                            else
                            {
                                if (giacu == 0)
                                    _Dongia = _ldichvu.DonGia2;
                                else
                                    _Dongia = _ldichvu.DonGiaDV2;
                            }
                            NgayGiuongNew moi5 = new NgayGiuongNew();
                            moi5.MaDV = _MaDV;
                            moi5.DonGia = _Dongia;
                            moi5.DonVi = donvi;
                            moi5.TrongBH = _TrongBH;
                            moi5.TyLeTT = _TrongBH == 1 ? 50 : _TyLeTT;
                            moi5.XHH = _XHH;
                            moi5.NgayNhap = _DenNgay.AddSeconds(-5);
                            KetQua.Add(moi5);
                        }
                        else
                        {
                            giacu = DungChung.Ham.GiaCu(_MaBN, _TrongBH);
                            if (radTrongBH.SelectedIndex == 1)
                            {
                                if (giacu == 15)
                                    _Dongia = _ldichvu.DonGiaTT15;
                                else if (giacu == 39)
                                    _Dongia = _ldichvu.DonGiaTT39;
                                else if (giacu == 0)
                                    _Dongia = _ldichvu.DonGiaBHYT;
                            }
                            else
                            {
                                if (giacu == 0)
                                    _Dongia = _ldichvu.DonGia2;
                                else
                                    _Dongia = _ldichvu.DonGiaDV2;
                            }
                            NgayGiuongNew moi5 = new NgayGiuongNew();
                            moi5.MaDV = _MaDV;
                            moi5.DonGia = _Dongia;
                            moi5.DonVi = donvi;
                            moi5.TrongBH = _TrongBH;
                            moi5.TyLeTT = _TrongBH == 1 ? 70 : _TyLeTT;
                            moi5.XHH = _XHH;
                            moi5.NgayNhap = _DenNgay.AddSeconds(-5);
                            KetQua.Add(moi5);
                        }

                        //ad ngày giường các ngày còn lại
                        DateTime ngaytu4 = DungChung.Ham.NgayDen(_TuNgay);
                        DateTime ngayden4 = DungChung.Ham.NgayTu(_DenNgay);
                        TimeSpan tongngay4 = ngayden4 - ngaytu4;
                        int songay4 = 0;
                        //if (DungChung.Bien.MaBV == "14017")
                        //{
                        //    songay4 = tongngay4.Days - 1;
                        //}
                        //else
                        songay4 = tongngay4.Days;
                        if (songay4 > 0)
                        {
                            for (int i = 0; i < songay4; i++)
                            {
                                DateTime _ngaynhap = _TuNgay.AddDays(i + 1);
                                double _DongiaN = 0;
                                giacu = DungChung.Ham.GiaCu(_MaBN, _TrongBH);
                                if (radTrongBH.SelectedIndex == 1)
                                {
                                    if (giacu == 15)
                                        _DongiaN = _ldichvu.DonGiaTT15;
                                    else if (giacu == 39)
                                        _DongiaN = _ldichvu.DonGiaTT39;
                                    else if (giacu == 0)
                                        _DongiaN = _ldichvu.DonGiaBHYT;
                                }
                                else
                                {
                                    if (giacu == 0)
                                        _DongiaN = _ldichvu.DonGia2;
                                    else
                                        _DongiaN = _ldichvu.DonGiaDV2;
                                }

                                NgayGiuongNew moi1 = new NgayGiuongNew();
                                moi1.MaDV = _MaDV;
                                moi1.DonGia = _DongiaN;
                                moi1.DonVi = donvi;
                                moi1.TrongBH = _TrongBH;
                                moi1.TyLeTT = _TrongBH == 1 ? 100 : _TyLeTT;
                                moi1.XHH = _XHH;
                                moi1.NgayNhap = _ngaynhap;
                                KetQua.Add(moi1);
                            }
                            return KetQua;
                        }
                    }
                    break;
            }
            return KetQua;
        }
        #endregion

        private void LupNgayden_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void grvTienGiuong_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colxoa")
            {
                bool DY_Xoa = true;
                int _iddonct = 0, _iddon = 0;
                var Ktrv = data.RaViens.Where(p => p.MaBNhan == _maBN).ToList();
                if (Ktrv.Count > 0)
                {
                    MessageBox.Show("Bệnh nhân đã ra viện, không thể xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DY_Xoa = false;
                }
                var Ktrvp = data.VienPhis.Where(p => p.MaBNhan == _maBN).ToList();
                if (Ktrvp.Count > 0)
                {
                    MessageBox.Show("Bệnh nhân đã thanh toán, không thể xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DY_Xoa = false;
                }
                DialogResult Result = MessageBox.Show("Bạn muốn xóa chỉ định ngày giường này ?", "Hỏi xóa", MessageBoxButtons.OKCancel);
                if (Result == DialogResult.Cancel)
                    DY_Xoa = false;
                if (DY_Xoa)
                {
                    if (grvTienGiuong.GetFocusedRowCellValue(coliddonct) != null)
                    {
                        _iddonct = Convert.ToInt32(grvTienGiuong.GetFocusedRowCellValue(coliddonct));
                        var dthuocct = data.DThuoccts.Where(p => p.IDDonct == _iddonct).FirstOrDefault();
                        if (dthuocct != null)
                        {
                            _iddon = Convert.ToInt32(dthuocct.IDDon);
                            data.DThuoccts.Remove(dthuocct);
                            data.SaveChanges();
                        }

                        var _dthuocct = data.DThuoccts.Where(p => p.IDDon == _iddon).ToList();
                        if (_dthuocct.Count == 0)
                        {
                            DThuoc delete = data.DThuocs.Where(p => p.IDDon == _iddon).FirstOrDefault();
                            data.DThuocs.Remove(delete);
                        }
                        loadData(0);
                    }
                }
            }
        }

        private void btnHuongDan_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hướng dẫn chỉ định ngày giường: \n1.Tiền giường ngày vào: vào trước 12h tính 70%, sau 12h tính 50% \n2. Tiền giường ngày ra: ra trước 12h tính 50%, ra sau 12h tính 70% \n3. Các ngày còn lại tính 100% \n4. Chức năng chỉ định ngày giường tự động chỉ chính xác 100% \nkhi chỉ định vào ngày cuối cùng điều trị trước khi ra viện");
        }

        private void btnsua_Click_1(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            btnMoi.Enabled = false;
            trangthai = 2;
            EnableControl(true);
            LupNgaytu.Enabled = false;
            LupNgayden.Enabled = false;
            txtSoluong.Visible = true;
            labelControl9.Visible = true;
        }

        private void lupNguoiKe_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}