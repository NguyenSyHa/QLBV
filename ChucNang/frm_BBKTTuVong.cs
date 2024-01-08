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
using QLBV.Models.Business.DataCommunication;
using QLBV.Providers.Business.Datacommunication;
using QLBV.Utilities.Commons;
using System.IO;
using System.Threading.Tasks;
using QLBV.FormNhap;
using QLBV.Providers.Dictionaries.CanBo;
using QLBV.Signature.Models;
using static QLBV.Models.Business.DataCommunication.GiayBaoTuModel;

namespace QLBV.FormThamSo
{
    public partial class frm_BBKTTuVong : DevExpress.XtraEditors.XtraForm
    {
        int _mabn = 0;
        int _idkb = 0;
        int _id = 0;
        int TTLuu = 1;
        private readonly QLBVEntities _dataContext;
        private readonly GiayBaoTuProvider _giayBaoTuProvider;

        private StaffProvider _staffProvider;
        public StaffProvider StaffProvider
        {
            get
            {
                if (_staffProvider == null)
                    _staffProvider = new StaffProvider();

                return _staffProvider;
            }
        }

        public frm_BBKTTuVong(int mabn)
        {
            _dataContext = QLBV_Database.Common.EntityDbContext.DbContext;
            _giayBaoTuProvider = new GiayBaoTuProvider();
            _mabn = mabn;
            InitializeComponent();
        }
        #region Class KTTV
        private class KTTV
        {
            private int id;

            public int IDBB
            {
                get { return id; }
                set { id = value; }
            }
            private string tenkp;

            public string TenKP
            {
                get { return tenkp; }
                set { tenkp = value; }
            }
            private DateTime ngaydttu;

            public DateTime NgayDTTu
            {
                get { return ngaydttu; }
                set { ngaydttu = value; }
            }
            private DateTime ngaydtden;

            public DateTime NgayDTDen
            {
                get { return ngaydtden; }
                set { ngaydtden = value; }
            }
            private DateTime ngayhc;
            public DateTime NgayHC
            {
                get { return ngayhc; }
                set { ngayhc = value; }
            }
            private string buong;

            public string Buong
            {
                get { return buong; }
                set { buong = value; }
            }
            private string giuong;

            public string Giuong
            {
                get { return giuong; }
                set { giuong = value; }
            }
            private string qtdbdt;

            public string QTDBDT
            {
                get { return qtdbdt; }
                set { qtdbdt = value; }
            }
            private string ketluan;

            public string KetLuan
            {
                get { return ketluan; }
                set { ketluan = value; }
            }
            private string huongdt;

            public string HuongDT
            {
                get { return huongdt; }
                set { huongdt = value; }
            }
            private string chutoa;

            public string ChuToa
            {
                get { return chutoa; }
                set { chutoa = value; }
            }
            private string thuky;

            public string ThuKy
            {
                get { return thuky; }
                set { thuky = value; }
            }
            private string thanhvien;

            public string ThanhVien
            {
                get { return thanhvien; }
                set { thanhvien = value; }
            }
            private string chandoan;

            public string ChanDoan
            {
                get { return chandoan; }
                set { chandoan = value; }
            }
            private int makp;

            public int MaKP
            {
                get { return makp; }
                set { makp = value; }
            }
            private string macb;

            public string MaCB
            {
                get { return macb; }
                set { macb = value; }
            }
            private string macbtk;

            public string MaCBtk
            {
                get { return macbtk; }
                set { macbtk = value; }
            }

            private string nguoithan;

            public string NguoiThan
            {
                get { return nguoithan; }
                set { nguoithan = value; }
            }
        }
        #endregion
        List<KPhong> _kp = new List<KPhong>();
        List<KTTV> _bbhc = new List<KTTV>();

        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_BBKTTuVong_Load(object sender, EventArgs e)
        {

            var bnn = DataContect.BenhNhans.Where(p => p.MaBNhan == _mabn && p.CapCuu == 2);
            if (bnn.Count() > 0)
            {
                //EnableButton(true);
                thoigiandukien.DateTime = DateTime.Now;
                _bbhc.Clear();
                var qkp = (from kp in DataContect.KPhongs.Where(p => p.PLoai == "Phòng khám" || p.PLoai == "Lâm sàng") select new { kp.MaKP, kp.TenKP }).ToList();
                if (qkp.Count > 0)
                {
                    lupKhoa.Properties.DataSource = qkp.ToList();
                }
                List<KPhong> _lkp = new List<KPhong>();
                List<CanBo> _lcb = new List<CanBo>();
                _lkp = DataContect.KPhongs.ToList();
                _lcb = DataContect.CanBoes.ToList();
                lupChuToa.Properties.DataSource = _lcb;
                lupThuKy.Properties.DataSource = _lcb;
                txtKetLuan.Text = null;
                txtnguoithan.Text = null;
                txtTVTG.Text = null;
                txtTTDB.Text = null;
                thoigiandukien.DateTime = DateTime.Now;
                lupKhoa.EditValue = null;
                lupChuToa.EditValue = null;
                lupThuKy.EditValue = null;
                ngaylapbb.DateTime = DateTime.Now;
                tuvongluc.DateTime = DateTime.Now;
                var bn = (from a in DataContect.BenhNhans.Where(p => p.MaBNhan == _mabn)
                          select new
                          {
                              a.MaBNhan,
                              a.TenBNhan,
                              a.Tuoi,
                              GTinh = (a.GTinh == 1) ? "Nam" : "Nữ"
                          }).ToList();
                txtTenBNhan.Text = bn.First().TenBNhan;
                txtMaBN.Text = Convert.ToString(bn.First().MaBNhan);
                txtGTinh.Text = bn.First().GTinh;
                txtTuoi.Text = Convert.ToString(bn.First().Tuoi);
                var id = (from bb in DataContect.BBHCs.Where(p => p.MaBNhan == _mabn)
                          group bb by bb.MaBNhan into kq
                          select new { kq.Key, IDKB = kq.Max(p => p.IDBB) }).ToList();
                if (id.Count() > 0)
                {
                    _id = id.First().IDKB;
                    txtID.Text = _id.ToString();

                    var bbtv = (from a in DataContect.BBHCs.Where(p => p.MaBNhan == _mabn && p.PhanLoai == 1 && p.IDBB == _id)
                                select new
                                {
                                    a.MaBNhan,
                                    a.MaKP,
                                    a.ThoiGianDuKienPT,
                                    a.MaCB,
                                    a.MaCBtk,
                                    a.ThanhVien,
                                    a.NguoiThan,
                                    a.HuongDT,
                                    a.KetLuan,
                                    a.NgayDTTu,
                                    a.NgayHC
                                }).ToList();
                    if (bbtv.Count() > 0)
                    {
                        txtKetLuan.Text = bbtv.First().KetLuan;
                        txtnguoithan.Text = bbtv.First().NguoiThan;
                        txtTVTG.Text = bbtv.First().ThanhVien;
                        txtTTDB.Text = bbtv.First().HuongDT;
                        thoigiandukien.DateTime = bbtv.First().ThoiGianDuKienPT ?? DateTime.Now;
                        lupKhoa.EditValue = bbtv.First().MaKP;
                        lupChuToa.EditValue = bbtv.First().MaCB;
                        lupThuKy.EditValue = bbtv.First().MaCBtk;
                        tuvongluc.DateTime = bbtv.First().NgayDTTu ?? DateTime.Now;
                        ngaylapbb.DateTime = bbtv.First().NgayHC ?? DateTime.Now;
                        //TTLuu = 2;
                        //txtID.Text = Convert.ToString(_idkb);
                        //_id = _idkb;
                        simpleButton2.Enabled = true;
                        btnXoa.Enabled = true;
                        btnInPhieu.Enabled = true;
                        txtKetLuan.Enabled = false;
                        txtnguoithan.Enabled = false;
                        txtTVTG.Enabled = false;
                        txtTTDB.Enabled = false;
                        thoigiandukien.Enabled = false;
                        lupKhoa.Enabled = false;
                        lupChuToa.Enabled = false;
                        lupThuKy.Enabled = false;
                        tuvongluc.Enabled = false;
                        ngaylapbb.Enabled = false;
                    }

                }
                else
                {
                    simpleButton2.Enabled = false;
                    btnXoa.Enabled = false;
                    btnInPhieu.Enabled = false;
                    TTLuu = 1;
                }
                var gbt = _dataContext.GiayBaoTus.FirstOrDefault(p => p.MA_BN == _mabn);
                if (gbt != null)
                {
                    if (gbt.Status == true)
                    {
                        btnGuiGiayChungTu.Enabled = true;
                    }
                    else
                    {
                        btnGuiGiayChungTu.Enabled = false;
                    }
                }
            }
            else
            {
                MessageBox.Show("Không thể sử dụng chức năng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private bool KTLuu()
        {
            if (string.IsNullOrEmpty(txtTenBNhan.Text))
            {
                MessageBox.Show("Chưa có bệnh nhân để lưu");
                txtTenBNhan.Focus();
                return false;
            }
            if (thoigiandukien.Text == "")
            {
                MessageBox.Show("Chưa có thời gian kiểm điểm tử vong");
                tuvongluc.Focus();
                return false;
            }
            if (tuvongluc.Text == "")
            {
                MessageBox.Show("Chưa có thời gian tử vong");
                tuvongluc.Focus();
                return false;
            }
            if (ngaylapbb.Text == "")
            {
                MessageBox.Show("Chưa có thời gian tử lập biên bản kiểm thảo tử vong");
                tuvongluc.Focus();
                return false;
            }
            if (tuvongluc.DateTime >= thoigiandukien.DateTime)
            {
                MessageBox.Show("Thời gian tử vong phải nhỏ hơn thời gian kiểm điểm tử vong");
                tuvongluc.Focus();
                return false;
            }
            if (thoigiandukien.DateTime > ngaylapbb.DateTime)
            {
                MessageBox.Show("Thời gian kiểm điểm tử vong phải nhỏ hơn hoặc bằng thời gian lập biên bản");
                tuvongluc.Focus();
                return false;
            }
            return true;
        }

        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click_1(object sender, EventArgs e)
        {
            if (KTLuu())
            {
                int ot;
                int _int_maBN = 0;
                if (Int32.TryParse(txtMaBN.Text, out ot))
                    _int_maBN = Convert.ToInt32(txtMaBN.Text);
                switch (TTLuu)
                {
                    case 1:
                        BBHC moibb = new BBHC();
                        moibb.MaBNhan = _int_maBN;
                        if (lupKhoa.EditValue != null)
                        { moibb.MaKP = lupKhoa.EditValue == null ? 0 : Convert.ToInt32(lupKhoa.EditValue); }
                        if (lupChuToa.EditValue != null)
                        { moibb.MaCB = lupChuToa.EditValue.ToString(); }
                        if (lupThuKy.EditValue != null)
                        { moibb.MaCBtk = lupThuKy.EditValue.ToString(); }
                        moibb.ThanhVien = txtTVTG.Text;
                        moibb.KetLuan = txtKetLuan.Text;
                        moibb.NguoiThan = txtnguoithan.Text;
                        moibb.PhanLoai = 1;
                        moibb.HuongDT = txtTTDB.Text;
                        moibb.NgayDTTu = tuvongluc.DateTime;
                        moibb.NgayHC = ngaylapbb.DateTime;
                        moibb.ThoiGianDuKienPT = thoigiandukien.DateTime;
                        moibb.SoBB = DataContect.BBHCs.Where(p => p.SoBB != null).Max(p => p.SoBB) + 1;
                        DataContect.BBHCs.Add(moibb);
                        if (DataContect.SaveChanges() >= 0)
                        {
                            MessageBox.Show("Tạo mới thành công");
                            frm_BBKTTuVong_Load(sender, e);
                            TTLuu = 0;
                        }
                        break;
                    case 2:
                        var id = DataContect.BBHCs.Where(p => p.IDBB == _id).ToList();
                        if (id.Count > 0)
                        {
                            // sua
                            BBHC suabb = DataContect.BBHCs.Single(p => p.IDBB == _id);
                            if (lupKhoa.EditValue != null)
                            { suabb.MaKP = lupKhoa.EditValue == null ? 0 : Convert.ToInt32(lupKhoa.EditValue); }
                            if (lupChuToa.EditValue != null)
                            { suabb.MaCB = lupChuToa.EditValue.ToString(); }
                            if (lupThuKy.EditValue != null)
                            { suabb.MaCBtk = lupThuKy.EditValue.ToString(); }
                            suabb.ThanhVien = txtTVTG.Text;
                            suabb.HuongDT = txtTTDB.Text;
                            suabb.KetLuan = txtKetLuan.Text;
                            suabb.NguoiThan = txtnguoithan.Text;
                            suabb.HuongDT = txtTTDB.Text;
                            suabb.NgayDTTu = tuvongluc.DateTime;
                            suabb.ThoiGianDuKienPT = thoigiandukien.DateTime;
                            if (DataContect.SaveChanges() >= 0)
                            {
                                TTLuu = 0;
                                MessageBox.Show("Sửa thành công");
                                frm_BBKTTuVong_Load(sender, e);
                            }
                        }
                        break;
                        //btnLuu.Enabled = false;
                }


            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //txtTenBNhan.pr.ReadOnly = true;
            //txtTenBNhan.Properties.AllowFocused = false;
            //txtTuoi.Properties.ReadOnly = true;
            //txtGTinh.Properties.ReadOnly = true;
            if (!string.IsNullOrEmpty(txtTenBNhan.Text))
            {
                TTLuu = 2;
                txtKetLuan.Enabled = true;
                txtnguoithan.Enabled = true;
                txtTVTG.Enabled = true;
                txtTTDB.Enabled = true;
                thoigiandukien.Enabled = true;
                lupKhoa.Enabled = true;
                lupChuToa.Enabled = true;
                lupThuKy.Enabled = true;
                tuvongluc.Enabled = true;
                ngaylapbb.Enabled = true;
            }
            else
            {
                MessageBox.Show("không có biên bản kiểm thảo tử vong để sửa");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtID.Text))
            {
                int idbb = Convert.ToInt32(txtID.Text);
                var query = (from bnrv in DataContect.VaoViens.Where(p => p.MaBNhan == _mabn)
                             join a in DataContect.RaViens on bnrv.MaBNhan equals a.MaBNhan
                             select new
                             {
                                 bnrv.MaBNhan
                             }).ToList();
                if (query.Count == 0)
                {
                    var ktbb = DataContect.BBHCs.Where(p => p.IDBB == idbb).ToList();
                    if (ktbb.Count > 0)
                    {
                        DialogResult _reuslt = MessageBox.Show("Bạn thực sự muốn xóa biên bản kiểm thảo tử vong của BN: " + txtTenBNhan.Text, "Xóa Biên bản kiểm thảo", MessageBoxButtons.YesNo, MessageBoxIcon.Question); if (_reuslt == DialogResult.Yes)
                        {
                            var xoa = DataContect.BBHCs.Single(p => p.IDBB == idbb);
                            DataContect.BBHCs.Remove(xoa);
                            DataContect.SaveChanges();
                            MessageBox.Show("Xóa biên bản kiểm thảo tử vong thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            frm_BBKTTuVong_Load(sender, e);
                        }
                    }
                    else
                    {
                        var xoa = DataContect.BBHCs.Single(p => p.IDBB == idbb);
                        DataContect.BBHCs.Remove(xoa);
                        DataContect.SaveChanges();
                        MessageBox.Show("Xóa biên bản kiểm thảo tử vong thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frm_BBKTTuVong_Load(sender, e);
                    }
                }
                else
                {
                    MessageBox.Show("Bệnh nhân đã ra viện, không thể xóa biên bản kiểm thử tử vong.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                txtKetLuan.Enabled = true;
                txtnguoithan.Enabled = true;
                txtTVTG.Enabled = true;
                txtTTDB.Enabled = true;
                thoigiandukien.Enabled = true;
                lupKhoa.Enabled = true;
                lupChuToa.Enabled = true;
                lupThuKy.Enabled = true;
                tuvongluc.Enabled = true;
                ngaylapbb.Enabled = true;
            }
        }

        private void btnInPhieu_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            frmIn frm = new frmIn();

            var par = (from bb in data.BBHCs.Where(p => p.IDBB == _id)
                       join bn in data.BenhNhans on bb.MaBNhan equals bn.MaBNhan
                       select new
                       {
                           bn.TenBNhan,
                           bn.GTinh,
                           bn.Tuoi,
                           bb.MaKP,
                           bb.ChanDoan,
                           bb.NgayHC,
                           bb.MaCB,
                           bb.MaCBtk,
                           bb.ThanhVien,
                           bb.KetLuan,
                           bb.HuongDT,
                           bb.SoBB,
                           NgayVao = bn.NNhap,
                           NgayTV = bb.NgayDTTu,
                           Ngaykdtv = bb.ThoiGianDuKienPT,
                           bb.NguoiThan
                       }).ToList();
            if (DungChung.Bien.MaBV == "12122")
            {
                BaoCao.Rep_BBKTTuVong_4069 rep = new BaoCao.Rep_BBKTTuVong_4069();
                if (par.Count() > 0)
                {
                    rep.Sobb.Value = par.First().SoBB;
                    rep.TenBN.Value = par.First().TenBNhan;
                    rep.Tuoi.Value = par.First().Tuoi;
                    if (par.First().GTinh == 0) { rep.Nam.Value = "/".ToUpper(); }
                    if (par.First().GTinh == 1) { rep.Nu.Value = "/".ToUpper(); }
                    rep.vaovienluc.Value = par.First().NgayVao.Value.Hour + " giờ " + par.First().NgayVao.Value.Minute + " phút,ngày " + par.First().NgayVao.Value.Day + " tháng " + par.First().NgayVao.Value.Month + " năm " + par.First().NgayVao.Value.Year;
                    rep.Tuvongluc.Value = par.First().NgayTV.Value.Hour + " giờ " + par.First().NgayTV.Value.Minute + " phút,ngày " + par.First().NgayTV.Value.Day + " tháng " + par.First().NgayTV.Value.Month + " năm " + par.First().NgayTV.Value.Year;
                    if (par.First().MaKP != null)
                    {
                        int _mp = par.First().MaKP.Value;
                        var kp = DataContect.KPhongs.Where(p => p.MaKP == _mp).Select(x => new { x.TenKP }).ToList();
                        rep.Khoa.Value = kp.First().TenKP;
                    }
                    rep.kdtvluc.Value = par.First().Ngaykdtv.Value.Hour + " giờ " + par.First().Ngaykdtv.Value.Minute + " phút,ngày " + par.First().Ngaykdtv.Value.Day + " tháng " + par.First().Ngaykdtv.Value.Month + " năm " + par.First().Ngaykdtv.Value.Year;
                    if (par.First().MaCB != null)
                    {
                        string _cb1 = par.First().MaCB;
                        var cb1 = DataContect.CanBoes.Where(p => p.MaCB == _cb1).Select(x => new { x.TenCB }).ToList();
                        if (cb1.Count > 0)
                        {
                            rep.ChuToa.Value = cb1.First().TenCB;
                            rep.chutoa1.Value = "Họ và tên: " + cb1.First().TenCB;
                        }
                    }
                    if (par.First().MaCBtk != null)
                    {
                        string _cb1 = par.First().MaCBtk;
                        var cb1 = DataContect.CanBoes.Where(p => p.MaCB == _cb1).Select(x => new { x.TenCB }).ToList();
                        if (cb1.Count > 0)
                        {
                            rep.ThuKy.Value = cb1.First().TenCB;
                            rep.thuky1.Value = "Họ và tên: " + cb1.First().TenCB;
                        }
                    }

                    rep.TVTG.Value = par.First().ThanhVien;
                    rep.nguoithan.Value = par.First().NguoiThan;
                    rep.HDTT.Value = par.First().HuongDT;
                    rep.KetLuan.Value = par.First().KetLuan;
                    rep.NgayHoiChan.Value = "Ngày " + par.First().NgayHC.Value.Day + " tháng " + par.First().NgayHC.Value.Month + " năm " + par.First().NgayHC.Value.Year;

                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                }
                else
                {
                    BaoCao.Rep_BBKTTuVong rep1 = new BaoCao.Rep_BBKTTuVong();
                    if (par.Count() > 0)
                    {
                        rep1.Sobb.Value = par.First().SoBB;
                        rep1.TenBN.Value = par.First().TenBNhan;
                        rep1.Tuoi.Value = par.First().Tuoi;
                        if (par.First().GTinh == 0) { rep1.Nam.Value = "/".ToUpper(); }
                        if (par.First().GTinh == 1) { rep1.Nu.Value = "/".ToUpper(); }
                        rep1.vaovienluc.Value = par.First().NgayVao.Value.Hour + " giờ " + par.First().NgayVao.Value.Minute + " phút,ngày " + par.First().NgayVao.Value.Day + " tháng " + par.First().NgayVao.Value.Month + " năm " + par.First().NgayVao.Value.Year;
                        rep1.Tuvongluc.Value = par.First().NgayTV.Value.Hour + " giờ " + par.First().NgayTV.Value.Minute + " phút,ngày " + par.First().NgayTV.Value.Day + " tháng " + par.First().NgayTV.Value.Month + " năm " + par.First().NgayTV.Value.Year;
                        if (par.First().MaKP != null)
                        {
                            int _mp = par.First().MaKP.Value;
                            var kp = DataContect.KPhongs.Where(p => p.MaKP == _mp).Select(x => new { x.TenKP }).ToList();
                            rep1.Khoa.Value = kp.First().TenKP;
                        }
                        rep1.kdtvluc.Value = par.First().Ngaykdtv.Value.Hour + " giờ " + par.First().Ngaykdtv.Value.Minute + " phút,ngày " + par.First().Ngaykdtv.Value.Day + " tháng " + par.First().Ngaykdtv.Value.Month + " năm " + par.First().Ngaykdtv.Value.Year;
                        if (par.First().MaCB != null)
                        {
                            string _cb1 = par.First().MaCB;
                            var cb1 = DataContect.CanBoes.Where(p => p.MaCB == _cb1).Select(x => new { x.TenCB }).ToList();
                            if (cb1.Count > 0)
                            {
                                rep1.ChuToa.Value = cb1.First().TenCB;
                                rep1.chutoa1.Value = "Họ và tên: " + cb1.First().TenCB;
                            }
                        }
                        if (par.First().MaCBtk != null)
                        {
                            string _cb1 = par.First().MaCBtk;
                            var cb1 = DataContect.CanBoes.Where(p => p.MaCB == _cb1).Select(x => new { x.TenCB }).ToList();
                            if (cb1.Count > 0)
                            {
                                rep1.ThuKy.Value = cb1.First().TenCB;
                                rep1.thuky1.Value = "Họ và tên: " + cb1.First().TenCB;
                            }
                        }

                        rep1.TVTG.Value = par.First().ThanhVien;
                        rep1.nguoithan.Value = par.First().NguoiThan;
                        rep1.HDTT.Value = par.First().HuongDT;
                        rep1.KetLuan.Value = par.First().KetLuan;
                        rep1.NgayHoiChan.Value = "Ngày " + par.First().NgayHC.Value.Day + " tháng " + par.First().NgayHC.Value.Month + " năm " + par.First().NgayHC.Value.Year;

                        rep1.CreateDocument();
                        frm.prcIN.PrintingSystem = rep1.PrintingSystem;
                    }
                }
                frm.ShowDialog();
            }


        }

        private void btnGuiGiayChungTu_Click(object sender, EventArgs e)
        {
            try
            {
                string username = DungChung.Bien.xmlFilePath_LIS[10];
                string pass = DungChung.Bien.xmlFilePath_LIS[11];
                var hospitalCodes = new List<string>()
                {
                    "30004"
                };
                var isSignature = true;
                var bn = _dataContext.BenhNhans.FirstOrDefault(p => p.MaBNhan == _mabn);

                //Đăng nhập lấy token
                var token = DataCommunication.DataCommunication.GetToken(username, pass);
                var gbt = _dataContext.GiayBaoTus.FirstOrDefault(p => p.MA_BN == _mabn);

                if (gbt == null)
                {
                    MessageBox.Show("Vui lòng nhập thông tin bệnh nhân tử vong!");
                    Frm_NhapTTTuVong frm = new Frm_NhapTTTuVong(_mabn);
                    frm.ShowDialog();
                    return;
                }

                //Lấy thông tin bn tử vong từ db
                GiayBaoTuModel giayBaoTuModel = _giayBaoTuProvider.GetGiayBaoTu(_mabn);

                giayBaoTuModel.TTRUONG_DVI = DungChung.Bien.GiamDoc;
                giayBaoTuModel.MACSKCB = DungChung.Bien.MaBV;
                giayBaoTuModel.DIACHI_CSKCB = DungChung.Bien.DiaChi;
                giayBaoTuModel.MA_GBT = string.Format("{0}.GBT.{1}.{2}", gbt.SOCT, DungChung.Bien.MaBV, ((DateTime)gbt.NGAY_CAPGIAYBT).ToString("yy"));

                #region check thông tin thiếu

                #endregion

                var dataCommunicationXml = AppConfig.MyMapper.Map<GiayBaoTuXmlModel>(giayBaoTuModel);

                var hsgbt = new HSDLGBTModel()
                {
                    GIAYBAOTU = dataCommunicationXml
                };

                var xml = XMLHelper.SerializeObject(hsgbt);
                CreatePath.Path(AppDomain.CurrentDomain.BaseDirectory + "Xmls"); // Tạo thư mục xml để chứa file ký
                var xmlName = bn.MaBNhan + "_" + Helpers.RemoveDiacritics(bn.TenBNhan) + "_" + dataCommunicationXml.Id;
                string filePath = AppDomain.CurrentDomain.BaseDirectory + @"Xmls\" + $"{xmlName}.xml";


                // Xuất file xml
                File.WriteAllText(filePath, xml);

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(pass))
                {
                    MessageBox.Show("Chưa nhập tên đăng nhập hoặc mật khẩu");
                    return;
                }

                //Gửi dữ liệu
                if (isSignature)
                {
                    var xmlToBytes = File.ReadAllBytes(filePath);
                    var xmlToBase64String = Convert.ToBase64String(xmlToBytes);
                    //GiayChungSinh.CHUKYDONVI = xmlToBase64String;

                    var data = new GBT()
                    {
                        maCskcb = DungChung.Bien.MaBV,
                        token = token.Result.apiKey.access_token,
                        id_token = token.Result.apiKey.id_token,
                        username = username,
                        password = Security.EncryptMd5(pass),
                        loaiHs = "60",
                        fileBase64Str = xmlToBase64String,
                    };

                    Task.Run(async () => await DataCommunication.DataCommunication.SyncGiayBaoTu(data, token.Result.apiKey.access_token, _mabn));
                }
            }
            catch (Exception ex)
            {
                if(ex.Message.ToLower().Contains("see inner exception for details"))
                {
                    MessageBox.Show("Lỗi: " + ex.Message + Environment.NewLine + "Inner Exception:" + ex.InnerException.Message);
                }
                else
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }

        }

        private void btnInputInfo_Click(object sender, EventArgs e)
        {
            Frm_NhapTTTuVong frm = new Frm_NhapTTTuVong(_mabn);
            frm.ShowDialog();
        }
    }
}