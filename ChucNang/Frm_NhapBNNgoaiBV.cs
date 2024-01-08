using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormNhap
{
    public partial class Frm_NhapBNNgoaiBV : DevExpress.XtraEditors.XtraUserControl
    {
        public Frm_NhapBNNgoaiBV()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<BenhNhan> _BenhNhan = new List<BenhNhan>();
        List<VienPhict> _VienPhict = new List<VienPhict>();
        List<VienPhict> _DMChiPhi = new List<VienPhict>();
        List<DTBN> _lDTBN = new List<DTBN>();
        List<DichVu> _lDichVu = new List<DichVu>();
        List<KPhong> _lkp = new List<KPhong>();
        string _ketqua;
        int Trangthai = -1;
        private void Frm_NhapBNNgoaiBV_Load(object sender, EventArgs e)
        {
            
            _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lbv = (from bv in _Data.BenhViens select bv).ToList();
            txtTyLe.Text = "0";
            UNREADONLY(true);
            cboInBangKe.Properties.Items.Clear();
            string[] st = new string[] { "Bảng kê mẫu A5", "Bảng kê mẫu A4", "Phiếu TT ra viện(mẫu 38)", "Phiếu TT ra viện(mẫu 40)" };
            string[] st_19048 = new string[] { "Bảng kê mẫu A5", "Bảng kê mẫu A4" };
            if (DungChung.Bien.MaBV == "19048")
                cboInBangKe.Properties.Items.AddRange(st_19048);
            else
                cboInBangKe.Properties.Items.AddRange(st);
            lupMHuong.Properties.DataSource = _Data.MucTTs.ToList();
            _lDichVu = _Data.DichVus.Where(p => p.Status == 1).OrderBy(p => p.TenDV).ToList();
            if (DungChung.Bien.LoaiPM == "QLBV")
                cboNoiKCB.SelectedIndex = 2;
            else
                cboNoiKCB.SelectedIndex = 0;
            txtNgayden.DateTime = System.DateTime.Now;
            txtNgaytu.DateTime = System.DateTime.Now;
            _lDTBN = _Data.DTBNs.ToList();
            lup_Doituong.Properties.DataSource = _lDTBN;
            _lkp = (from khoaphong in _Data.KPhongs
                      where (khoaphong.PLoai == "PK khu vực" || khoaphong.PLoai == "Phòng khám" || khoaphong.PLoai == "Xã phường" || khoaphong.PLoai == "Lâm sàng")
                      select khoaphong).ToList();
         
                LupTimKP.Properties.DataSource = _lkp.OrderBy(p => p.TenKP).ToList();
      
            lupMaICD.Properties.DataSource = _Data.ICD10.OrderBy(p => p.MaICD).ToList();
            //KTDSKP("-1");
            DSBN();
            EnableControl(true);
            var MucTT = (from Muc in _Data.MucTTs select new { Muc.MaMuc, Muc.PTTT }).ToList();
            if (MucTT.Count > 0)
            {
                lupMHuong.Properties.DataSource = MucTT.OrderBy(p => p.MaMuc);
                //lupMHuong.EditValue = MaMuc;
            }
            //binsDV.DataSource = _DMChiPhi.ToList();

        }
        private void DSBN()
        {
            DateTime _Ngaytu = DungChung.Ham.NgayTu(txtNgaytu.DateTime);
            DateTime _Ngayden = DungChung.Ham.NgayDen(txtNgayden.DateTime);
            string TENBN = "";
            if (!string.IsNullOrEmpty(txtTenBN.Text))
            {
                TENBN = txtTenBN.Text.ToLower();
            }
            int tuyenduoi = -1;
            tuyenduoi = cboTimNoiKham.SelectedIndex;
            string _MaKP = ""; // giu nguyen vif lay danh sach benhvien
            if (!string.IsNullOrEmpty(LupTimKP.Text))
            {
                _MaKP = LupTimKP.EditValue.ToString();
            }
            var DSBN = (from bn in _Data.BenhNhans.Where(p => p.TenBNhan.ToLower().Contains(TENBN)).Where(p => p.TuyenDuoi == tuyenduoi && p.SoTT == 0)
                        where (bn.NNhap >= _Ngaytu && bn.NNhap <= _Ngayden)
                        where (_MaKP == "" ? true : bn.ChuyenKhoa == _MaKP)
                        where (cbo_NoiTru.SelectedIndex == 2 ? true : bn.NoiTru == cbo_NoiTru.SelectedIndex)
                        //where !(from dt in _Data.DThuocs select dt.MaBNhan).Contains(bn.MaBNhan)
                        select bn).OrderBy(p => p.MaBNhan).ToList();

            GrcBenhNhan.DataSource = "";
            GrcBenhNhan.DataSource = DSBN.OrderBy(p => p.NNhap).ToList();


        }
        private void Xoatrang()//xoá trắng thông tin hành chính BN
        {
            LupNgayNhap.Text = "";
            SoThe.Text = "";
            radNoitru.SelectedIndex = 0;
            //RadNoitru.SelectedIndex = -1;
            txtMaCS.Text = "";
            SoThe.Text = "";
            lupNgayRV.Text = "";
            dtNgayTT.Text = "";
            txtNgayDT.Text = "";
            radTuyen.SelectedIndex = -1;
            cboKhuVuc.SelectedIndex = -1;
            cboNoitinh.SelectedIndex = -1;
            txtNhapTBN.Text = "";
            radNamNu.SelectedIndex = -1;
            txtNgaySinh.Text = "";
            txtThangSinh.Text = "";
            txtNamSinh.Text = "";
            txtTuoi.Text = "";
            txtDiaChi.Text = "";
            lupMaICD.EditValue = "0";
            lupChandoan.Text = "";
            //lupPhongKham.EditValue = "";
            lupMHuong.EditValue = "";
            cboKSK.Text = "";
        }
        private void UNREADONLY(bool T)
        {
            LupNgayNhap.Properties.ReadOnly = T;
            SoThe.Properties.ReadOnly = T;
            radNoitru.Properties.ReadOnly = T;
            //RadNoitru.SelectedIndex = -1;
            txtMaCS.Properties.ReadOnly = T;
            SoThe.Properties.ReadOnly = T;
            lupNgayRV.Properties.ReadOnly = T;
            dtNgayTT.Properties.ReadOnly = T;
            txtNgayDT.Properties.ReadOnly = T;
            radTuyen.Properties.ReadOnly = T;
            cboKhuVuc.Properties.ReadOnly = T;
            cboNoitinh.Properties.ReadOnly = T;
            txtNhapTBN.Properties.ReadOnly = T;
            radNamNu.Properties.ReadOnly = T;
            txtNgaySinh.Properties.ReadOnly = T;
            txtThangSinh.Properties.ReadOnly = T;
            txtNamSinh.Properties.ReadOnly = T;
            txtTuoi.Properties.ReadOnly = T;
            txtDiaChi.Properties.ReadOnly = T;
            lupMaICD.Properties.ReadOnly = T;
            lupChandoan.Properties.ReadOnly = T;
            lupPhongKham.Properties.ReadOnly = T;
            dtHanBHden.Properties.ReadOnly = T;
            dtHanBHTu.Properties.ReadOnly = T;
            lup_Doituong.Properties.ReadOnly = T;
            cboNoiKCB.Properties.ReadOnly = T;
            lupMHuong.Properties.ReadOnly = T;
            dtDu5nam.Properties.ReadOnly = T;
            //chkDu6thang.Properties.ReadOnly = T;
            lupMaBVgt.Properties.ReadOnly = T;
            chkCapcuu.Properties.ReadOnly = T;
            cboKSK.Properties.ReadOnly = T;
        }
        private void EnableControl(bool t)
        {
            SBTLuu.Enabled = !t;
            SBTXoa.Enabled = t;
            SBTHuy.Enabled = !t;
            SBTSua.Enabled = t;
            SBTThemmoi.Enabled = t;
            GrvChiphi.OptionsBehavior.Editable = !t;
            GrcBenhNhan.Enabled = t;

        }
        private void KTICD(string _MaICD)// Khởi tạo danh mục ICD
        {
            var DMCID = (from ICD in _Data.ICD10
                         select new { ICD.MaICD, ICD.TenCB, ICD.TenICD }).ToList();
            if (DMCID.Count > 0)
            {
                lupMaICD.Properties.DataSource = DMCID.OrderBy(p => p.MaICD).ToList();
                if (_MaICD != "-1")
                {
                    lupMaICD.EditValue = _MaICD;
                }
            }
        }
        private void KTDSKP(int _MaKP)//Khởi tạo danh sách phòng khám
        {
            var KP = (from khoaphong in _Data.KPhongs
                      where (khoaphong.PLoai == "PK khu vực" || khoaphong.PLoai == "Phòng khám" || khoaphong.PLoai == "Xã phường" || khoaphong.PLoai == "Lâm sàng")
                      select new { khoaphong.TenKP, khoaphong.MaKP }).ToList();
            if (KP.Count > 0)
            {
                lupPhongKham.Properties.DataSource = KP.OrderBy(p => p.TenKP).ToList();
                if (_MaKP > 0)
                {
                    lupPhongKham.EditValue = _MaKP;
                }
            }
        }
        private bool KTTTHC()// kiểm tra thông tin hành chính
        {
            if (string.IsNullOrEmpty(lup_Doituong.Text))
            {
                MessageBox.Show("Bạn chưa chọn đối tượng!");
                lup_Doituong.Focus();
                return false;
            }
            int IDDT = Convert.ToByte(lup_Doituong.EditValue);
            int HTTT = 0;
            if (_lDTBN.Where(p => p.IDDTBN == IDDT).Select(p => p.HTTT).Count() > 0)
                HTTT = _lDTBN.Where(p => p.IDDTBN == IDDT).Select(p => p.HTTT).First();
            if (HTTT == 1)
            {
                if (string.IsNullOrEmpty(SoThe.Text) && SoThe.Text.Length != 15)
                {
                    MessageBox.Show("Số thẻ không hợp lệ!");
                    return false;
                }
                if (string.IsNullOrEmpty(txtMaCS.Text) && txtMaCS.Text.Length != 4)
                {
                    MessageBox.Show("Bạn chưa nhập mã cơ sở!");
                    txtMaCS.Focus();
                    return false;
                }


                if (string.IsNullOrEmpty(dtHanBHTu.Text))
                {
                    MessageBox.Show("Bạn chưa nhập hạn thẻ!");
                    dtHanBHTu.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(dtHanBHden.Text))
                {
                    MessageBox.Show("Bạn chưa nhập hạn thẻ!");
                    dtHanBHden.Focus();
                    return false;
                }
                if (radTuyen.SelectedIndex == -1)
                {
                    MessageBox.Show("Bạn chưa chọn đúng trái tuyến!");
                    radTuyen.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(lupMHuong.Text))
                {
                    MessageBox.Show("Bạn chưa chọn mức hưởng!");
                    lupMHuong.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(cboNoitinh.Text))
                {
                    MessageBox.Show("Bạn chưa chọn bệnh nhân nội ngoại tỉnh!");
                    cboNoitinh.Focus();
                    return false;
                }
            }
            if (string.IsNullOrEmpty(LupNgayNhap.Text))
            {
                MessageBox.Show("Bạn chưa nhập ngày nhập!");
                LupNgayNhap.Focus();
                return false;
            }

            if (radNoitru.SelectedIndex == -1)
            {
                //MessageBox.Show(radNoitru.SelectedIndex.ToString());
                MessageBox.Show("Bạn chưa chọn hình thức khám!");
                radNoitru.Focus();
                return false;
            }




            if (string.IsNullOrEmpty(txtNhapTBN.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên bệnh nhân!");
                txtNhapTBN.Focus();
                return false;
            }
            if (radNamNu.SelectedIndex == -1)
            {
                MessageBox.Show("Chưa nhập giới tính");
                radNamNu.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtNamSinh.Text))
            {
                MessageBox.Show("Bạn chưa nhập năm sinh!");
                txtNamSinh.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtDiaChi.Text))
            {

                MessageBox.Show("Bạn chưa nhập địa chỉ!");
                txtDiaChi.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(lupMaICD.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã ICD!");
                lupMaICD.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(lupPhongKham.Text))
            {
                if (cboNoiKCB.SelectedIndex == 0)
                {
                    MessageBox.Show("Bạn chưa chọn phòng khám!");
                    lupPhongKham.Focus();
                    return false;
                }
            }
            if (string.IsNullOrEmpty(lupNgayRV.Text))
            {
                MessageBox.Show("Bạn chưa nhập ngày ra viện!");
                lupNgayRV.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(cboNoiKCB.Text))
            {
                MessageBox.Show("Bạn chưa chọn nơi KCB!");
                cboNoiKCB.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtNgayDT.Text))
            {
                MessageBox.Show("Bạn chưa nhập số ngày điều trị!");
                txtNgayDT.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(lupMaBVgt.Text))
            {
                MessageBox.Show("Bạn chưa chọn nơi khám chữa bệnh");
                lupMaBVgt.Focus();
                return false;
            }
            return true;



        }
        private void TinhTT()//tính chi phí bệnh nhân và chi phí bh thực hiện hàm này sau khi dữ liệu nhập vào chính xác
        {
            if (_DMChiPhi.Count > 0)
            {
                int HBV = 0;
                int TLTT = 0;
                // kiểm tra hạng BV
                var BV = (from DMBV in _Data.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV) select new { DMBV.HangBV }).ToList();
                if (BV.Count > 0)
                {
                    HBV = BV.First().HangBV.Value;
                }
                // end hang BV
                switch (HBV)
                {
                    case 3:
                        if (radTuyen.SelectedIndex == 1)
                        {
                            if (chkDu6thang.Checked)
                            {
                                TLTT = 100;
                            }
                            else
                            {
                                int TT = 0;
                                foreach (var a in _DMChiPhi)
                                {
                                    TT = TT + Convert.ToInt32(a.ThanhTien);
                                }
                                if (TT >= DungChung.Bien.GHanTT100)
                                {
                                    TLTT = Convert.ToInt32(lupMHuong.EditValue);
                                }
                                else
                                { TLTT = 100; }
                            }
                        }
                        else
                        {
                            if (cboKhuVuc.SelectedIndex != 0)
                            {
                                TLTT = Convert.ToInt32(lupMHuong.EditValue);
                            }
                            else
                            {
                                TLTT = 70 * Convert.ToInt32(lupMHuong.EditValue) / 100;
                            }
                        }
                        break;
                }
                foreach (var b in _DMChiPhi)
                {
                    b.TienBH = (b.ThanhTien * TLTT) / 100;
                    b.TienBN = b.ThanhTien - (b.ThanhTien * TLTT) / 100;
                }
            }
        }
        private void txtNgaytu_EditValueChanged(object sender, EventArgs e)
        {
            DSBN();
        }

        private void txtNgayden_EditValueChanged(object sender, EventArgs e)
        {
            DSBN();
        }

        private void txtTenBN_EditValueChanged(object sender, EventArgs e)
        {
            DSBN();
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LupKP_EditValueChanged(object sender, EventArgs e)
        {
            DSBN();
        }

        private void GrvBenhNhan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (GrvBenhNhan.GetFocusedRowCellValue(MaBN) != null)
            {
                Xoatrang();
                int _Mabn = Convert.ToInt32(GrvBenhNhan.GetFocusedRowCellValue(MaBN));
                txtMaBNhan.Text = _Mabn.ToString();
                txtTenBenhNhan.Text = GrvBenhNhan.GetFocusedRowCellDisplayText(TenBN).ToString();
                var DSBN = (from bn in _Data.BenhNhans.Where(p => p.MaBNhan == _Mabn)
                            select bn).ToList();
                if (DSBN.Count > 0)
                {
                    LupNgayNhap.DateTime = DSBN.First().NNhap.Value;
                    txtNhapTBN.Text = DSBN.First().TenBNhan;
                    txtNamSinh.Text = DSBN.First().NamSinh;
                    if (DSBN.First().GTinh == 0)
                    {
                        radNamNu.SelectedIndex = 1;
                    }
                    else
                    {
                        radNamNu.SelectedIndex = 0;
                    }
                    if (DSBN.First().NoiTru == 0)
                    {
                        radNoitru.SelectedIndex = 0;
                    }
                    else
                    {
                        radNoitru.SelectedIndex = 1;
                    }
                    txtDiaChi.Text = DSBN.First().DChi;
                    if (DSBN.First().NoiTinh != null)
                        cboNoitinh.SelectedIndex = DSBN.First().NoiTinh.Value;
                    if (DSBN.First().TuyenDuoi != null)
                        cboNoiKCB.SelectedIndex = DSBN.First().TuyenDuoi.Value;
                    cboNoiKCB_SelectedIndexChanged(sender, e);
                    lupMaBVgt.EditValue = DSBN.First().MaKCB;
                    txtMaBVKB.Text = DSBN.First().MaKCB;
                    lup_Doituong.EditValue = DSBN.First().IDDTBN;
                    //if (DSBN.First().DTuong == "BHYT")
                    //{
                    if (DSBN.First().HanBHTu != null && DSBN.First().HanBHTu.Value.Day > 0)
                        dtHanBHden.DateTime = DSBN.First().HanBHTu.Value;
                    else
                        dtHanBHden.DateTime = new DateTime();
                    if (DSBN.First().HanBHDen != null && DSBN.First().HanBHDen.Value.Day > 0)
                        dtHanBHTu.DateTime = DSBN.First().HanBHDen.Value;
                    else
                        dtHanBHTu.DateTime = new DateTime();
                    SoThe.Text = DSBN.First().SThe;
                    txtMaCS.Text = DSBN.First().MaCS;
                    if (DSBN.First().Tuyen != null)
                    {
                        if (DSBN.First().Tuyen.Value == 1)
                            radTuyen.SelectedIndex = 0;
                        else
                            radTuyen.SelectedIndex = 1;
                    }
                    else
                        radTuyen.SelectedIndex = -1;
                    if (DSBN.First().MucHuong != null)
                        lupMHuong.EditValue = DSBN.First().MucHuong.ToString();
                    else
                        lupMHuong.EditValue = "";
                    if (DSBN.First().NgayHM != null && DSBN.First().NgayHM.Value.Day > 0)
                        dtDu5nam.DateTime = DSBN.First().NgayHM.Value;
                    else
                        dtDu5nam.DateTime = new DateTime();
                    cboKhuVuc.Text = DSBN.First().KhuVuc;
                    // }
                    cboKSK.Text = DSBN.First().TChung;

                    KTDSKP(DSBN.First().MaKP == null ? 0 : DSBN.First().MaKP.Value);
                    var MaICD = (from MB in _Data.RaViens.Where(p => p.MaBNhan == _Mabn)
                                 select new { MB.SoNgaydt, MB.MaICD, MB.ChanDoan, MB.NgayRa }).ToList();
                    if (MaICD.Count > 0)
                    {
                        if (MaICD.First().SoNgaydt != null)
                            txtNgayDT.Text = MaICD.First().SoNgaydt.ToString();
                        else
                            txtNgayDT.Text = "";

                        lupMaICD.EditValue = MaICD.First().MaICD;
                        lupChandoan.Text = MaICD.First().ChanDoan;
                        if (MaICD.First().NgayRa != null && MaICD.First().NgayRa.Value.Day > 0)
                            lupNgayRV.DateTime = MaICD.First().NgayRa.Value;
                    }
                }
                _DMChiPhi.Clear();
                lupTenchiphi.DataSource = "";
                lupTenchiphi.DataSource = _lDichVu;
                var vp1 = _Data.VienPhis.Where(p => p.MaBNhan == _Mabn).ToList();
                if (vp1.Count > 0 && vp1.First().NgayTT != null)
                    dtNgayTT.DateTime = vp1.First().NgayTT.Value;
                else
                    dtNgayTT.DateTime = new DateTime();
                var VPCT = (from vp in _Data.VienPhis.Where(p => p.MaBNhan == _Mabn)
                            join vpct1 in _Data.VienPhicts on vp.idVPhi equals vpct1.idVPhi
                            join DV in _Data.DichVus on vpct1.MaDV equals DV.MaDV
                            select vpct1).ToList();
                Bds.DataSource = VPCT.OrderBy(p => p.idVPhict).ToList();
                GrcChiphi.DataSource = Bds;
            }
            else
            {
                txtTenBenhNhan.Text = "";
                txtMaBNhan.Text = "";
                Xoatrang();
            }
        }

        private void SBTThemmoi_Click(object sender, EventArgs e)
        {
            Xoatrang();
            LupNgayNhap.DateTime = System.DateTime.Now;
            lupNgayRV.DateTime = System.DateTime.Now;
            dtNgayTT.DateTime = System.DateTime.Now;
            UNREADONLY(false);
            _DMChiPhi.Clear();
            KTDSKP(-1);
            KTICD("-1");
            _ketqua = "";
            txtNgayDT.Text = "1";
            int IDDT = 0;
            if (_lDTBN.Where(p => p.HTTT == 1).Select(p => p.IDDTBN).Count() > 0)
                IDDT = _lDTBN.Where(p => p.HTTT == 1).Select(p => p.IDDTBN).First();
            lup_Doituong.EditValue = IDDT;
            Trangthai = 0;
            Bds.DataSource = _DMChiPhi.ToList();
            GrcChiphi.DataSource = Bds;
            lupTenchiphi.DataSource = "";
            lupTenchiphi.DataSource = _lDichVu;
            //GrcChiphi.DataSource = _DMChiPhi.OrderBy(p => p.MaDV);
            //Bds.DataSource = _DMChiPhi.ToList();
            //GrcChiphi.DataSource = Bds;

            EnableControl(false);
            LupNgayNhap.Focus();

        }


        private void SBTLuu_Click(object sender, EventArgs e)
        {
            _DMChiPhi.Clear();
            //MessageBox.Show(GrvChiphi.RowCount.ToString());


            if (KTTTHC())
            {
                int IDDT = Convert.ToByte(lup_Doituong.EditValue);
                int HTTT = 0;
                if (_lDTBN.Where(p => p.IDDTBN == IDDT).Select(p => p.HTTT).Count() > 0)
                    HTTT = _lDTBN.Where(p => p.IDDTBN == IDDT).Select(p => p.HTTT).First();
                //TinhTT();
                if (HTTT == 1)
                {

                    if (Trangthai == 1)
                    {
                        _ketqua = "";
                        string chuoi_dau = SoThe.Text;
                        string[] chuoi_tach = chuoi_dau.Split(new Char[] { '-' });

                        foreach (string s in chuoi_tach)
                        {

                            if (s.Trim() != "")
                                _ketqua += s;
                        }
                    }

                    #region thêm vào bảng person
                    var y = (from STHE in _Data.People.Where(p => p.SThe == _ketqua) select STHE).ToList();
                    if (y.Count <= 0)
                    {
                        Person _person = new Person();
                        _person.SThe = _ketqua;
                        _person.TenBNhan = txtNhapTBN.Text.Trim();
                        _person.MaCS = txtMaCS.Text.Trim();
                        if (!string.IsNullOrEmpty(txtNgaySinh.Text) && txtNgaySinh.Text.Length == 2)
                            _person.NgaySinh = txtNgaySinh.Text.Trim();
                        if (!string.IsNullOrEmpty(txtThangSinh.Text) && txtThangSinh.Text.Length == 2)
                            _person.ThangSinh = txtThangSinh.Text.Trim();
                        if (!string.IsNullOrEmpty(txtNamSinh.Text))
                        {
                            _person.NSinh = Convert.ToInt32(txtNamSinh.Text);
                        }
                        if (radNamNu.SelectedIndex == 0)
                        {
                            _person.GTinh = 1;
                        }
                        else
                        {
                            if (radNamNu.SelectedIndex == 1)
                                _person.GTinh = 0;
                        }
                        _person.DChi = txtDiaChi.Text.Trim();
                        if (dtHanBHTu.DateTime.Day > 0)
                            _person.HanBHTu = dtHanBHTu.DateTime.Date;
                        if (dtHanBHden.DateTime.Day > 0)
                            _person.HanBHDen = dtHanBHden.DateTime.Date;
                        if (dtDu5nam.DateTime.Day > 0)
                            _person.NgayHM = dtDu5nam.DateTime.Date;

                        _person.KhuVuc = cboKhuVuc.Text;
                        _Data.People.Add(_person);
                        _Data.SaveChanges();

                    }
                    else // update lại                                        
                    {
                        int id = 0;
                        id = y.First().IDPerson;
                        var _person = _Data.People.Single(p => p.IDPerson == id);
                        //_person.SThe = _ketqua;
                        _person.TenBNhan = txtNhapTBN.Text.Trim();
                        _person.MaCS = txtMaCS.Text.Trim();
                        if (!string.IsNullOrEmpty(txtNamSinh.Text))
                        {
                            _person.NSinh = Convert.ToInt32(txtNamSinh.Text);
                        }
                        if (radNamNu.SelectedIndex == 0)
                        {
                            _person.GTinh = 1;
                        }
                        else
                        {
                            if (radNamNu.SelectedIndex == 1)
                                _person.GTinh = 0;
                        }
                        if (!string.IsNullOrEmpty(txtNgaySinh.Text) && txtNgaySinh.Text.Length == 2)
                            _person.NgaySinh = txtNgaySinh.Text.Trim();
                        if (!string.IsNullOrEmpty(txtThangSinh.Text) && txtThangSinh.Text.Length == 2)
                            _person.ThangSinh = txtThangSinh.Text.Trim();
                        _person.DChi = txtDiaChi.Text.Trim();
                        if (dtHanBHTu.DateTime.Day > 0)
                            _person.HanBHTu = dtHanBHTu.DateTime.Date;
                        if (dtHanBHden.DateTime.Day > 0)
                            _person.HanBHDen = dtHanBHden.DateTime.Date;
                        if (dtDu5nam.DateTime.Day > 0)
                            _person.NgayHM = dtDu5nam.DateTime.Date;
                        _person.KhuVuc = cboKhuVuc.Text;
                        _Data.SaveChanges();
                    }
                }
                    #endregion
              
                if (Trangthai == 0)// TT=0 là thêm mới, TT=1 là sửa
                {
                    #region thêm , sửa bảng bệnh nhân
                    //thêm mới bảng bệnh nhân
                    BenhNhan themmoi = new BenhNhan();
                    //int MSBN = 0;
                    //var BN1 = (from ID in _Data.BenhNhans select ID.MaBNhan).ToList();

                    //if (BN1.Count > 0)
                    //{
                    //    int i = BN1.Max() + 1;

                    //}
                    //MSBN += ms;
                    //themmoi.MaBNhan = MSBN;
                    themmoi.NNhap = LupNgayNhap.DateTime;
                    themmoi.DTuong = lup_Doituong.Text;
                    themmoi.IDDTBN = Convert.ToByte(lup_Doituong.EditValue);
                    themmoi.NoiTru = radNoitru.SelectedIndex;
                    themmoi.TuyenDuoi = cboNoiKCB.SelectedIndex;
                    if (chkCapcuu.Checked)
                    { themmoi.CapCuu = 1; }
                    else
                    { themmoi.CapCuu = 0; }
                    themmoi.SThe = _ketqua;
                    if (HTTT == 1)
                    {

                        themmoi.MaKCB = txtMaBVKB.Text;
                        themmoi.MaCS = txtMaCS.Text;
                        themmoi.HanBHTu = dtHanBHden.DateTime;
                        themmoi.HanBHDen = dtHanBHTu.DateTime;
                        themmoi.NgayHM = dtDu5nam.DateTime;
                        themmoi.Tuyen = radTuyen.SelectedIndex + 1;
                        themmoi.KhuVuc = cboKhuVuc.Text;
                        themmoi.NoiTinh = cboNoitinh.SelectedIndex;
                        themmoi.MucHuong = Convert.ToInt32(lupMHuong.Text);
                        themmoi.ChuyenKhoa = txtMaBVKB.Text;
                    }
                    if (lup_Doituong.Text == "KSK")
                    {
                        themmoi.TChung = cboKSK.Text;
                    }
                    themmoi.TenBNhan = txtNhapTBN.Text;
                    if(radNamNu.SelectedIndex ==0)                        
                    themmoi.GTinh = 1 ;
                    else if (radNamNu.SelectedIndex == 1)
                        themmoi.GTinh = 0;
                    if (txtNgaySinh.Text != null && txtNgaySinh.Text.Length == 2)
                    {
                        themmoi.NgaySinh = txtNgaySinh.Text;
                    }
                    if (txtThangSinh.Text != null && txtThangSinh.Text.Length == 2)
                    {
                        themmoi.ThangSinh = txtThangSinh.Text;
                    }
                    

                    themmoi.TChung = cboKSK.Text;
                    themmoi.SoTT = 0;
                    themmoi.NamSinh = txtNamSinh.Text;
                    themmoi.Tuoi = Convert.ToInt32(txtTuoi.Text);
                    themmoi.DChi = txtDiaChi.Text;
                    if (lupPhongKham.EditValue != null)
                        themmoi.MaKP = Convert.ToInt32(lupPhongKham.EditValue);
                    else
                        themmoi.MaKP = 0;
                    themmoi.DTNT = false;
                    _Data.BenhNhans.Add(themmoi);
                    _Data.SaveChanges();
                    TTboXung moitt = new TTboXung();
                    moitt.MaBNhan = themmoi.MaBNhan;
                    _Data.TTboXungs.Add(moitt);
                     _Data.SaveChanges();

                #endregion end thêm mới bảng bệnh nhân

                    #region// thêm mới BNKB
                    var BN = (from B in _Data.BenhNhans.OrderBy(p => p.MaBNhan) select new { B.MaBNhan }).ToList();
                    int _MaBN = BN.Last().MaBNhan;
                    BNKB themmoi1 = new BNKB();
                    themmoi1.MaBNhan = _MaBN;
                    themmoi1.MaICD = lupMaICD.EditValue.ToString();
                    if (lupPhongKham.EditValue != null)
                        themmoi1.MaKP = Convert.ToInt32(lupPhongKham.EditValue);
                    themmoi1.NgayKham = LupNgayNhap.DateTime;
                    themmoi1.PhuongAn = radNoitru.SelectedIndex;
                    _Data.BNKBs.Add(themmoi1);
                    _Data.SaveChanges();
                    #endregion  //end thêm mới BNKB


                    #region//thêm mới ra viện
                    RaVien themmoi2 = new RaVien();
                    themmoi2.ChanDoan = lupChandoan.Text;
                    themmoi2.MaBNhan = _MaBN;
                    themmoi2.MaICD = lupMaICD.EditValue.ToString();
                    if (lupPhongKham.EditValue != null)
                        themmoi2.MaKP = Convert.ToInt32(lupPhongKham.EditValue);
                    else
                        themmoi2.MaKP = 0;
                    themmoi2.NgayRa = lupNgayRV.DateTime;
                    themmoi2.NgayVao = LupNgayNhap.DateTime;
                    themmoi2.SoNgaydt = Convert.ToInt32(txtNgayDT.Text);
                    //themmoi2.SoNgaydt=
                    _Data.RaViens.Add(themmoi2);
                    _Data.SaveChanges();
                    #endregion // end thêm mới ra viện
                    #region//thêm mới viện phí
                    VienPhi themmoi3 = new VienPhi();
                    themmoi3.MaBNhan = _MaBN;
                    themmoi3.NgayTT = dtNgayTT.DateTime;
                    themmoi3.NgayDuyetCP = dtNgayTT.DateTime;
                    themmoi3.NgayRa = lupNgayRV.DateTime;
                    if (lupPhongKham.EditValue != null)
                        themmoi3.MaKP = Convert.ToInt32(lupPhongKham.EditValue);
                    else
                        themmoi3.MaKP = 0;
                    _Data.VienPhis.Add(themmoi3);
                    _Data.SaveChanges();
                    #endregion // end thêm mới Vien phi
                    #region // thêm mới VienPhict
                    string _IdVP = "";
                    var VP = (from V in _Data.VienPhis.OrderBy(p => p.idVPhi) select new { V.idVPhi }).ToList();
                    if (VP.Count > 0)
                    {
                        _IdVP = VP.Last().idVPhi.ToString();
                    }

                    double tongtienBN = 0;
                    for (int i = 0; i < GrvChiphi.RowCount; i++)
                    {
                        if (GrvChiphi.GetRowCellValue(i, MaDV) != null && GrvChiphi.GetRowCellValue(i, MaDV).ToString() != "")
                        {
                            if (GrvChiphi.GetRowCellValue(i, SoLuong) != null && GrvChiphi.GetRowCellValue(i, SoLuong).ToString() != "" && GrvChiphi.GetRowCellValue(i, DonGia) != null && GrvChiphi.GetRowCellValue(i, DonGia).ToString() != "")
                            {
                                VienPhict themmoi4 = new VienPhict();
                                if (lupPhongKham.EditValue != null)
                                    themmoi4.MaKP = Convert.ToInt32(lupPhongKham.EditValue);
                                else
                                    themmoi4.MaKP = 0;
                                themmoi4.idVPhi = Convert.ToInt32(_IdVP);
                                themmoi4.MaDV = GrvChiphi.GetRowCellValue(i, MaDV) == null ? 0 : Convert.ToInt32(GrvChiphi.GetRowCellValue(i, MaDV));
                                themmoi4.DonVi = GrvChiphi.GetRowCellValue(i, DonVi).ToString();
                                themmoi4.DonGia = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, DonGia).ToString());
                                themmoi4.SoLuong = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, SoLuong).ToString());
                                themmoi4.ThanhTien = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, ThanhTien).ToString());
                                themmoi4.TienBH = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, TienBH).ToString());
                                themmoi4.TienBN = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, TienBN).ToString());
                                tongtienBN = tongtienBN + Convert.ToDouble(GrvChiphi.GetRowCellValue(i, TienBN));
                                themmoi4.Duyet = 0;
                                if(!String.IsNullOrEmpty(txtTyLe.Text))
                                themmoi4.TyLeBHTT = Convert.ToDouble(txtTyLe.Text);
                                if (GrvChiphi.GetRowCellValue(i, colTrongBH) != null)
                                    themmoi4.TrongBH = Convert.ToInt32(GrvChiphi.GetRowCellValue(i, colTrongBH).ToString());
                                else
                                    themmoi4.TrongBH = 0;
                                double phut = Math.Round((lupNgayRV.DateTime - LupNgayNhap.DateTime).TotalMinutes/2);
                                themmoi4.NgayChiPhi = LupNgayNhap.DateTime.AddMinutes(phut);
                                themmoi4.NgayYLenh = LupNgayNhap.DateTime.AddMinutes(phut);

                                _Data.VienPhicts.Add(themmoi4);
                                _Data.SaveChanges();
                            }
                        }
                    }
                    if(tongtienBN == 0)
                    {
                        var qvp = (from vp in _Data.VienPhis.Where(p => p.MaBNhan == _MaBN) join vpct in _Data.VienPhicts on vp.idVPhi equals vpct.idVPhi select vpct).ToList();
                        foreach(VienPhict vpct in qvp)
                        {
                            vpct.TyLeBHTT = 100;
                            
                        }
                        _Data.SaveChanges();
                    }

                    DSBN();
                    Trangthai = -1;
                    UNREADONLY(true);
                    EnableControl(true);

                    #endregion end thêm mới VienPhict
                }
                else
                {
                    #region sửa thông tin hành chính bệnh nhân
                    int _MaBN = 0;
                    if (GrvBenhNhan.GetFocusedRowCellValue(MaBN) != null)
                    {
                        _MaBN = Convert.ToInt32(GrvBenhNhan.GetFocusedRowCellValue(MaBN));
                        var sua = _Data.BenhNhans.Single(p => p.MaBNhan == _MaBN);
                        sua.NNhap = LupNgayNhap.DateTime;
                        sua.DTuong = lup_Doituong.Text;
                        sua.IDDTBN = Convert.ToByte(lup_Doituong.EditValue);
                        sua.NoiTru = radNoitru.SelectedIndex;
                        sua.TuyenDuoi = cboNoiKCB.SelectedIndex;
                        if (chkCapcuu.Checked)
                        { sua.CapCuu = 1; }
                        else
                        { sua.CapCuu = 0; }
                        if (HTTT == 1)
                        {

                            sua.MaCS = txtMaCS.Text;
                            sua.HanBHTu = dtHanBHden.DateTime;
                            sua.HanBHDen = dtHanBHTu.DateTime;
                            sua.NgayHM = dtDu5nam.DateTime;
                            sua.KhuVuc = cboKhuVuc.Text;
                            sua.NoiTinh = cboNoitinh.SelectedIndex;
                            sua.MucHuong = Convert.ToInt32(lupMHuong.Text);
                            sua.ChuyenKhoa = txtMaBVKB.Text;
                        }
                        sua.SThe = SoThe.Text;
                        sua.Tuyen = radTuyen.SelectedIndex + 1;
                        sua.TenBNhan = txtNhapTBN.Text;
                        if (radNamNu.SelectedIndex == 0)
                            sua.GTinh = 1;
                        else if (radNamNu.SelectedIndex == 1)
                            sua.GTinh = 0;

                      //  sua.GTinh = radNamNu.SelectedIndex;
                        if (txtNgaySinh.Text != null && txtNgaySinh.Text.Length == 2)
                        {
                            sua.NgaySinh = txtNgaySinh.Text;
                        }
                        if (txtThangSinh.Text != null && txtThangSinh.Text.Length == 2)
                        {
                            sua.ThangSinh = txtThangSinh.Text;
                        }
                        sua.TChung = cboKSK.Text;
                        sua.NamSinh = txtNamSinh.Text;
                        sua.Tuoi = Convert.ToInt32(txtTuoi.Text);
                        sua.DChi = txtDiaChi.Text;
                        sua.MaKP = lupPhongKham.EditValue == null ? 0 : Convert.ToInt32(lupPhongKham.EditValue);
                        _Data.SaveChanges();
                    #endregion // end sua bệnh nhân
                        #region   // sửa thông tin hành chính BN

                        var sua1 = _Data.BNKBs.Where(p => p.MaBNhan == _MaBN).ToList();
                        if (sua1.Count > 0)
                        {
                            foreach (var s in sua1)
                            {
                                s.MaBNhan = _MaBN;
                                s.MaICD = lupMaICD.EditValue.ToString();
                                if (lupPhongKham.EditValue != null)
                                    s.MaKP = Convert.ToInt32(lupPhongKham.EditValue);
                                s.NgayKham = LupNgayNhap.DateTime;
                                s.PhuongAn = radNoitru.SelectedIndex;
                                _Data.SaveChanges();
                            }
                        }
                        else
                        {
                            BNKB themmoi1 = new BNKB();
                            themmoi1.MaBNhan = _MaBN;
                            themmoi1.MaICD = lupMaICD.EditValue.ToString();
                            if (lupPhongKham.EditValue != null)
                                themmoi1.MaKP = Convert.ToInt32(lupPhongKham.EditValue);
                            themmoi1.NgayKham = LupNgayNhap.DateTime;
                            themmoi1.PhuongAn = radNoitru.SelectedIndex;
                            _Data.BNKBs.Add(themmoi1);
                            _Data.SaveChanges();
                        }
                        #endregion // end sửa thông tin BNKB
                        #region // sửa thông tin Ra vien
                        var ktrv = _Data.RaViens.Where(p => p.MaBNhan == _MaBN).ToList();
                        if (ktrv.Count > 0)
                        {
                            int id = ktrv.First().MaBNhan;
                            var sua2 = _Data.RaViens.Single(p => p.MaBNhan == id);
                            sua2.ChanDoan = lupChandoan.Text;
                            sua2.MaBNhan = _MaBN;
                            sua2.MaICD = lupMaICD.EditValue.ToString();
                            if (lupPhongKham.EditValue != null)
                                sua2.MaKP = Convert.ToInt32(lupPhongKham.EditValue);
                            sua2.NgayRa = lupNgayRV.DateTime;
                            sua2.NgayVao = LupNgayNhap.DateTime;
                            sua2.SoNgaydt = Convert.ToInt32(txtNgayDT.Text);
                            _Data.SaveChanges();
                        }
                        else
                        {
                            RaVien themmoi2 = new RaVien();
                            themmoi2.ChanDoan = lupChandoan.Text;
                            themmoi2.MaBNhan = _MaBN;
                            themmoi2.MaICD = lupMaICD.EditValue.ToString();
                            if (lupPhongKham.EditValue != null)
                                themmoi2.MaKP = Convert.ToInt32(lupPhongKham.EditValue);
                            themmoi2.NgayRa = lupNgayRV.DateTime;
                            themmoi2.NgayVao = LupNgayNhap.DateTime;
                            themmoi2.SoNgaydt = Convert.ToInt32(txtNgayDT.Text);
                            //themmoi2.SoNgaydt=
                            _Data.RaViens.Add(themmoi2);
                            _Data.SaveChanges();
                        }
                        #endregion  //end sửa thông tin ra viện
                     #region   // sửa thông tin VienPhi
                        int _IdVP = -1;
                        var ktvp = _Data.VienPhis.Where(p => p.MaBNhan == (_MaBN)).ToList();
                        if (ktvp.Count > 0)
                        {
                            _IdVP = ktvp.First().idVPhi;
                            int id = -1;
                            id = ktvp.First().idVPhi;
                            var sua3 = _Data.VienPhis.Single(p => p.idVPhi == id);
                            sua3.MaBNhan = _MaBN;
                            sua3.NgayTT = dtNgayTT.DateTime;
                            sua3.NgayRa = lupNgayRV.DateTime;
                            if (lupPhongKham.EditValue != null)
                                sua3.MaKP = Convert.ToInt32(lupPhongKham.EditValue);

                            _Data.SaveChanges();
                        }
                        else
                        {
                            VienPhi themmoi3 = new VienPhi();
                            themmoi3.MaBNhan = _MaBN;
                            themmoi3.NgayTT = dtNgayTT.DateTime;
                            themmoi3.NgayDuyetCP = dtNgayTT.DateTime;
                            themmoi3.NgayRa = lupNgayRV.DateTime;
                            if (lupPhongKham.EditValue != null)
                                themmoi3.MaKP = Convert.ToInt32(lupPhongKham.EditValue);
                            _Data.VienPhis.Add(themmoi3);
                            _Data.SaveChanges();
                        }
                     #endregion  // end sửa thông tin viện phí
                        #region  // sửa thông tin VienPhict

                        var VP = (from V in _Data.VienPhis.Where(p => p.MaBNhan == _MaBN) select new { V.idVPhi }).ToList();
                        if (VP.Count > 0)
                        {
                            _IdVP = VP.Last().idVPhi;
                        }
                        if (_IdVP > 0)
                        {
                            for (int i = 0; i < GrvChiphi.RowCount; i++)
                            {
                                int ID = 0;
                                if (GrvChiphi.GetRowCellValue(i, idVPhict) != null)
                                    ID = Convert.ToInt32(GrvChiphi.GetRowCellValue(i, idVPhict));
                                if (ID <= 0)
                                {
                                    if (GrvChiphi.GetRowCellValue(i, MaDV) != null && GrvChiphi.GetRowCellValue(i, MaDV).ToString() != "")
                                    {
                                        if (GrvChiphi.GetRowCellValue(i, SoLuong) != null && GrvChiphi.GetRowCellValue(i, SoLuong).ToString() != "" && GrvChiphi.GetRowCellValue(i, DonGia) != null && GrvChiphi.GetRowCellValue(i, DonGia).ToString() != "" && GrvChiphi.GetRowCellValue(i, DonVi) != null)
                                        {
                                            VienPhict themmoi4 = new VienPhict();
                                            themmoi4.idVPhi = _IdVP;
                                            if (lupPhongKham.EditValue != null)
                                                themmoi4.MaKP = Convert.ToInt32(lupPhongKham.EditValue);
                                            else
                                                themmoi4.MaKP = 0;
                                            themmoi4.TyLeTT = 100;// tạm thời
                                            themmoi4.TyLeBHTT = Convert.ToInt32(txtTyLe.Text);
                                            themmoi4.MaDV = GrvChiphi.GetRowCellValue(i, MaDV) == null ? 0 : Convert.ToInt32(GrvChiphi.GetRowCellValue(i, MaDV));
                                            themmoi4.DonVi = GrvChiphi.GetRowCellValue(i, DonVi).ToString();
                                            themmoi4.DonGia = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, DonGia).ToString());
                                            themmoi4.SoLuong = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, SoLuong).ToString());
                                            themmoi4.ThanhTien = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, ThanhTien).ToString());
                                            themmoi4.TienBH = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, TienBH).ToString());
                                            themmoi4.TienBN = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, TienBN).ToString());
                                            if (GrvChiphi.GetRowCellValue(i, colTrongBH) != null)
                                                themmoi4.TrongBH = Convert.ToInt32(GrvChiphi.GetRowCellValue(i, colTrongBH).ToString());
                                            else
                                                themmoi4.TrongBH = 0;
                                            double phut = Math.Round((lupNgayRV.DateTime - LupNgayNhap.DateTime).TotalMinutes / 2);
                                            themmoi4.NgayYLenh = LupNgayNhap.DateTime.AddMinutes(phut);
                                            themmoi4.NgayChiPhi = LupNgayNhap.DateTime.AddMinutes(phut);
                                            _Data.VienPhicts.Add(themmoi4);
                                            _Data.SaveChanges();
                                        }
                                    }
                                }
                                else
                                {
                                    {
                                        if (GrvChiphi.GetRowCellValue(i, SoLuong) != null && GrvChiphi.GetRowCellValue(i, SoLuong).ToString() != "" && GrvChiphi.GetRowCellValue(i, DonGia) != null && GrvChiphi.GetRowCellValue(i, DonGia).ToString() != "")
                                        {
                                            var themmoi4 = _Data.VienPhicts.Single(p => p.idVPhict == ID);
                                            themmoi4.idVPhi = _IdVP;
                                            if (lupPhongKham.EditValue != null)
                                                themmoi4.MaKP = Convert.ToInt32(lupPhongKham.EditValue);
                                            else
                                                themmoi4.MaKP = 0;
                                            themmoi4.TyLeTT = 100;// tạm thời
                                            themmoi4.TyLeBHTT = Convert.ToInt32(txtTyLe.Text);
                                            themmoi4.MaDV = GrvChiphi.GetRowCellValue(i, MaDV) == null ? 0 : Convert.ToInt32(GrvChiphi.GetRowCellValue(i, MaDV));
                                            themmoi4.DonVi = GrvChiphi.GetRowCellValue(i, DonVi).ToString();
                                            themmoi4.DonGia = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, DonGia).ToString());
                                            themmoi4.SoLuong = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, SoLuong).ToString());
                                            themmoi4.ThanhTien = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, ThanhTien).ToString());
                                            themmoi4.TienBH = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, TienBH).ToString());
                                            themmoi4.TienBN = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, TienBN).ToString());
                                            if (GrvChiphi.GetRowCellValue(i, colTrongBH) != null)
                                                themmoi4.TrongBH = Convert.ToInt32(GrvChiphi.GetRowCellValue(i, colTrongBH).ToString());
                                            else
                                                themmoi4.TrongBH = 0;
                                            themmoi4.Duyet = 0;
                                            _Data.SaveChanges();
                                        }
                                    }
                                }
                            }
                        }
                        DSBN();

                        #endregion // end sửa thông tin VPct
                    }
                }
                UNREADONLY(true);
                EnableControl(true);
                Trangthai = -1;
            }
        }

        private void GrvChiphi_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "Dongia")
            {
                MessageBox.Show("tt");
            }
        }

        private void GrvChiphi_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //Bds.DataSource = _DMChiPhi.ToList();
            //GrcChiphi.DataSource = Bds;
            if (e.Column.Name == "tendv")
            {
                //string _TenDV = "";
                //_TenDV=GrvChiphi.GetFocusedRowCellValue(tendv).ToString();
                //var Donvi = (from Dichvu in _Data.DichVus.Where(p => p.TenDV == _TenDV)
                //             select new { Dichvu.DonVi, Dichvu.TenDV }).ToList();
                //if(Donvi.Count>0)
                //{
                //    foreach (var a in Donvi)
                //    {
                //        DMChiPhi themmoi = new DMChiPhi();
                //        themmoi.tendv = a.TenDV;
                //        themmoi.donvi = a.DonVi;
                //        _DMChiPhi.Add(themmoi);
                //    }
                //    GrcChiphi.DataSource = _DMChiPhi;

            }
            //MessageBox.Show(_TenDV);
        }

        private void cboDoituong_EditValueChanged(object sender, EventArgs e)
        {
            if (Trangthai == 0 || Trangthai == 1)
            {
                int IDDT = Convert.ToByte(lup_Doituong.EditValue);
                int HTTT = 0;
                if (_lDTBN.Where(p => p.IDDTBN == IDDT).Select(p => p.HTTT).Count() > 0)
                    HTTT = _lDTBN.Where(p => p.IDDTBN == IDDT).Select(p => p.HTTT).First();
                if (HTTT != 1)
                {
                    SoThe.Properties.ReadOnly = true;
                    //SoThe.Properties.e
                    txtMaCS.Properties.ReadOnly = true;
                    dtHanBHden.Properties.ReadOnly = true;
                    dtHanBHTu.Properties.ReadOnly = true;
                    dtDu5nam.Properties.ReadOnly = true;
                    //chkDu6thang.Properties.ReadOnly = true;
                    lupMHuong.Properties.ReadOnly = true;
                    radTuyen.Properties.ReadOnly = true;
                    cboKhuVuc.Properties.ReadOnly = true;
                    cboNoitinh.Properties.ReadOnly = true;

                }
                else
                {
                    SoThe.Properties.ReadOnly = false;
                    txtMaCS.Properties.ReadOnly = false;
                    dtHanBHden.Properties.ReadOnly = false;
                    dtHanBHTu.Properties.ReadOnly = false;
                    dtDu5nam.Properties.ReadOnly = false;
                    //chkDu6thang.Properties.ReadOnly = false;
                    lupMHuong.Properties.ReadOnly = false;
                    radTuyen.Properties.ReadOnly = false;
                    cboKhuVuc.Properties.ReadOnly = false;
                    cboNoitinh.Properties.ReadOnly = false;
                }
            }
        }

        private void SoThe_EditValueChanged(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(SoThe.Text) && SoThe.Text.Length == 15)
            //{
            //    string Sthe = SoThe.Text;
            //    string MaMuc = Sthe.Substring(2, 2);
            //    var MucTT = (from Muc in _Data.MucTTs select new { Muc.MaMuc, Muc.PTTT }).ToList();
            //    if (MucTT.Count > 0)
            //    {
            //        lupMHuong.Properties.DataSource = MucTT.OrderBy(p => p.MaMuc);
            //        lupMHuong.EditValue = MaMuc;
            //    }
            //}
        }
        private void MaCS_EditValueChanged(object sender, EventArgs e)
        {
            if (txtMaCS.Text != null && txtMaCS.Text.Length == 5)
            {
                string Ma = txtMaCS.Text;
                var TenBV = (from bv in _Data.BenhViens.Where(p => p.MaBV == Ma) select new { bv.TenBV }).ToList();//kiểm tra MaCS trong danh muc
                if (TenBV.Count > 0)
                {
                    txtTenBV.Text = TenBV.First().TenBV;
                }
                else
                {
                    MessageBox.Show("Mã bệnh viện chưa có trong danh mục");
                }
            }
        }

        private void MaCS_Move(object sender, EventArgs e)
        {
            MessageBox.Show("OK");
        }

        private void txtNamsinh_EditValueChanged(object sender, EventArgs e)
        {
            if (txtNamSinh.Text != null && txtNamSinh.Text.Length == 4)
            {
                int HT = Convert.ToInt32(System.DateTime.Now.ToString().Substring(6, 4));
                int NS = Convert.ToInt32(txtNamSinh.Text.Trim());
                int T = HT - NS;
                if (T < 0 || T > 100)
                {
                    MessageBox.Show("Năm sinh không chính xác");
                }
                else
                {
                    txtTuoi.Text = T.ToString();
                }
            }
        }

        private void SBTSua_Click(object sender, EventArgs e)
        {
            Trangthai = 1;
            UNREADONLY(false);
            //Bds.DataSource = _DMChiPhi.ToList();
            //GrcChiphi.DataSource = Bds;
            EnableControl(false);

        }

        private void SBTXoa_Click(object sender, EventArgs e)
        {
            int _MaBN = 0;
            if (GrvBenhNhan.GetFocusedRowCellValue(MaBN) != null)
            {
                int ID = 0;
                _MaBN = Convert.ToInt32(GrvBenhNhan.GetFocusedRowCellValue(MaBN));
                //Xoá Benh nhan
                var xoa = _Data.BenhNhans.Single(p => p.MaBNhan == _MaBN);
                _Data.BenhNhans.Remove(xoa);
                //end xoá benh nhan
                // xoá BNKB
                var ktbnkb = _Data.BNKBs.Where(p => p.MaBNhan == _MaBN).ToList();
                if (ktbnkb.Count > 0)
                {
                    ID = ktbnkb.First().IDKB;
                    var xoa1 = _Data.BNKBs.Single(p => p.IDKB == ID);
                    _Data.BNKBs.Remove(xoa1);
                }
                // end xoá BNKB
                // xoá ra viện
                var ktrv = _Data.RaViens.Where(p => p.MaBNhan == _MaBN).ToList();
                foreach (var item in ktrv)
                {

                    _Data.RaViens.Remove(item);
                }
                //end xoá ra viện
                // xoá Vienphi
                var ktvp = _Data.VienPhis.Where(p => p.MaBNhan == _MaBN).ToList();
                if (ktvp.Count > 0)
                {
                    ID = ktvp.First().idVPhi;
                    // end xoá vien phi
                    // xoá viện phí ct
                    var vpct = _Data.VienPhicts.Where(p => p.idVPhi == ID).ToList();
                    foreach (var d in vpct)
                    {
                        int idct = d.idVPhict;
                        var xoa4 = _Data.VienPhicts.Single(p => p.idVPhict == idct);
                        _Data.VienPhicts.Remove(xoa4);
                    }

                    var xoa3 = _Data.VienPhis.Single(p => p.idVPhi == ID);
                    _Data.VienPhis.Remove(xoa3);
                }
                // end xoá VPct
                _Data.SaveChanges();
                DSBN();

            }
        }

        public bool GiaCu(string Dtuong, int TrongDM, DateTime ngaychidinh,DateTime NgayNhap)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            bool giacu = false;
            DateTime ngaythang = DungChung.Ham.NgayTu(DungChung.Bien.ngayGiaMoiDV);
            if (Dtuong == "BHYT" && TrongDM == 1)
                ngaythang = DungChung.Ham.NgayTu(DungChung.Bien.ngayGiaMoi);
            if (ngaychidinh.Date < ngaythang)
            {
                giacu = true;
            }
            else
            {
                giacu = false;
            }
            if (NgayNhap < ngaythang)
            {
                giacu = true;
            }
            //if (vaovien.Count <= 0 && ttbn.First().NoiTru == 0 && ttbn.First().NNhap.Value.Date < ngaythang)//dùng chung DungChung.Bien.MaBV=="30007"
            //    giacu = true;

            return giacu;
        }
        private void GrvChiphi_CellValueChanged_1(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "TenCP")
            {
                if (GrvChiphi.GetFocusedRowCellValue(TenCP) != null)
                {
                    int _MaDV = Convert.ToInt32(GrvChiphi.GetFocusedRowCellValue(TenCP));
                    var KT = (from dv in _lDichVu.Where(p => p.MaDV == _MaDV)
                              select new { dv.PLoai, DonGia = dv.DonGia, dv.DonVi, dv.TrongDM,dv.DonGiaBHYT,dv.DonGiaTT15,dv.DonGiaTT39 }).OrderByDescending(p => p.PLoai).ToList();
                    if (KT.First().PLoai == 1)
                    {

                        if (KT.Count > 0)
                        {
                            foreach (var a in KT)
                            {
                                cboDonGia.Items.Add(a.DonGia);
                            }
                            GrvChiphi.SetFocusedRowCellValue("DonGia", KT.First().DonGia);
                            GrvChiphi.SetFocusedRowCellValue("DonVi", KT.First().DonVi);
                            GrvChiphi.SetFocusedRowCellValue(colTrongBH, KT.First().TrongDM);
                            if (GrvChiphi.GetFocusedRowCellValue("idVPhict") != null && Convert.ToInt32(GrvChiphi.GetFocusedRowCellValue("idVPhict")) > 0)
                            {

                            }
                            else
                                GrvChiphi.SetFocusedRowCellValue("idVPhict", -1);
                            if (GrvChiphi.GetFocusedRowCellValue("idVPhi") != null && Convert.ToInt32(GrvChiphi.GetFocusedRowCellValue("idVPhi")) > 0)
                            {

                            }
                            else
                                GrvChiphi.SetFocusedRowCellValue("idVPhi", -1);
                        }
                    }
                    else
                    {
                        if (KT.Count > 0)
                        {
                            int Trongdm = 0;
                            if (GrvChiphi.GetFocusedRowCellValue(colTrongBH) != null && GrvChiphi.GetFocusedRowCellValue(colTrongBH) != null)
                            {
                                Trongdm = Convert.ToInt32(GrvChiphi.GetFocusedRowCellValue(colTrongBH));
                            }
                            string dtuong = lup_Doituong.Text;
                            bool giacu = GiaCu(dtuong, Trongdm, LupNgayNhap.DateTime, LupNgayNhap.DateTime);
                            foreach (var a in KT)
                            {
                                if (giacu)
                                    cboDonGia.Items.Add(a.DonGiaTT15);
                                else
                                    cboDonGia.Items.Add(a.DonGiaTT39);
                            }
                            GrvChiphi.SetFocusedRowCellValue("DonGia", giacu == true ? KT.First().DonGiaTT15 : KT.First().DonGiaTT39);
                            GrvChiphi.SetFocusedRowCellValue("DonVi", KT.First().DonVi);
                            GrvChiphi.SetFocusedRowCellValue(colTrongBH, KT.First().TrongDM);
                            if (GrvChiphi.GetFocusedRowCellValue("idVPhict") != null && Convert.ToInt32(GrvChiphi.GetFocusedRowCellValue("idVPhict")) > 0)
                            {

                            }
                            else
                                GrvChiphi.SetFocusedRowCellValue("idVPhict", -1);
                            if (GrvChiphi.GetFocusedRowCellValue("idVPhi") != null && Convert.ToInt32(GrvChiphi.GetFocusedRowCellValue("idVPhi")) > 0)
                            {

                            }
                            else
                                GrvChiphi.SetFocusedRowCellValue("idVPhi", -1);
                        }
                    }

                }
            }
            if (e.Column.Name == "SoLuong" || e.Column.Name == "DonGia")
            {
                double _tyle = 0;
                double _tienbn = 0, _tienbh = 0, _Thanhtien = 0, _DGia = 0, _tongtien = 0;
                _tyle = Convert.ToDouble(txtTyLe.Text);
                _tyle = _tyle / 100;
                if (GrvChiphi.GetFocusedRowCellValue(SoLuong) != null && GrvChiphi.GetFocusedRowCellValue(DonGia) != null)
                {
                    int _SLuong = 0;
                    try
                    {
                        _SLuong = Convert.ToInt32(GrvChiphi.GetFocusedRowCellValue(SoLuong));
                    }
                    catch
                    {
                        MessageBox.Show("Nhập số lượng chưa đúng!");
                    }
                    if (_SLuong >= 0)
                    {
                        _DGia = Convert.ToDouble(GrvChiphi.GetFocusedRowCellValue(DonGia));
                        _Thanhtien = _DGia * _SLuong;
                        GrvChiphi.SetFocusedRowCellValue(ThanhTien, _Thanhtien);
                        if (radTuyen.SelectedIndex == 0)
                        {
                            for (int i = 0; i < GrvChiphi.RowCount; i++)
                            {
                                if (GrvChiphi.GetRowCellValue(i, ThanhTien) != null)
                                {
                                    if (GrvChiphi.GetRowCellValue(i, colTrongBH) != null && GrvChiphi.GetRowCellValue(i, colTrongBH).ToString() == "1")
                                        _tongtien += Convert.ToDouble(GrvChiphi.GetRowCellValue(i, ThanhTien));
                                }
                            }
                            _tongtien += _Thanhtien;
                            if (_tongtien >= DungChung.Bien.GHanTT100)
                            {
                                for (int i = 0; i < GrvChiphi.RowCount; i++)
                                {
                                    double tt = 0;
                                    if (GrvChiphi.GetRowCellValue(i, ThanhTien) != null)
                                        tt = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, ThanhTien));
                                    if (GrvChiphi.GetRowCellValue(i, colTrongBH) != null && GrvChiphi.GetRowCellValue(i, colTrongBH).ToString() == "1")
                                    {
                                        _tienbh = tt * _tyle;
                                        _tienbn = tt - _tienbh;
                                        GrvChiphi.SetRowCellValue(i, TienBH, _tienbh);
                                        GrvChiphi.SetRowCellValue(i, TienBN, _tienbn);
                                    }
                                    else
                                    {
                                        GrvChiphi.SetRowCellValue(i, TienBH, 0);
                                        GrvChiphi.SetRowCellValue(i, TienBN, tt);
                                    }
                                }
                                _tienbh = _Thanhtien * _tyle;
                                _tienbn = _Thanhtien - _tienbh;
                                GrvChiphi.SetFocusedRowCellValue(TienBH, _tienbh);
                                GrvChiphi.SetFocusedRowCellValue(TienBN, _tienbn);
                            }
                            else
                            {
                              
                                for (int i = 0; i < GrvChiphi.RowCount; i++)
                                {
                                    double tt = 0;
                                    if (GrvChiphi.GetRowCellValue(i, ThanhTien) != null)
                                        tt = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, ThanhTien));
                                    if (GrvChiphi.GetRowCellValue(i, colTrongBH) != null && GrvChiphi.GetRowCellValue(i, colTrongBH).ToString() == "1")
                                    {
                                        GrvChiphi.SetRowCellValue(i, TienBH, tt);
                                        GrvChiphi.SetRowCellValue(i, TienBN, 0);
                                    }
                                    else
                                    {
                                        GrvChiphi.SetRowCellValue(i, TienBH, 0);
                                        GrvChiphi.SetRowCellValue(i, TienBN, tt);
                                    }
                                }
                                if (GrvChiphi.GetFocusedRowCellValue(colTrongBH) != null && GrvChiphi.GetFocusedRowCellValue(colTrongBH).ToString() == "1")
                                {
                                    GrvChiphi.SetFocusedRowCellValue(TienBH, _Thanhtien);
                                    GrvChiphi.SetFocusedRowCellValue(TienBN, 0);
                                }
                                else
                                {
                                    GrvChiphi.SetFocusedRowCellValue(TienBH, 0);
                                    GrvChiphi.SetFocusedRowCellValue(TienBN, _Thanhtien);
                                }
                            }
                        }
                        else
                        {
                            if (GrvChiphi.GetFocusedRowCellValue(colTrongBH) != null && GrvChiphi.GetFocusedRowCellValue(colTrongBH).ToString() == "1")
                            {
                                _tienbh = _Thanhtien * _tyle;
                                _tienbn = _Thanhtien - _tienbh;
                                GrvChiphi.SetFocusedRowCellValue(TienBH, _tienbh);
                                GrvChiphi.SetFocusedRowCellValue(TienBN, _tienbn);
                            }
                            else
                            {
                                GrvChiphi.SetFocusedRowCellValue(TienBH, 0);
                                GrvChiphi.SetFocusedRowCellValue(TienBN, _Thanhtien);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Số lượng phải >0");
                        GrvChiphi.FocusedColumn = GrvChiphi.VisibleColumns[2];
                    }
                }
            }
            if (e.Column.Name == "colTrongBH" && Trangthai != -1)
            {
                if (GrvChiphi.GetFocusedRowCellValue(TenCP) != null)
                {
                    int _MaDV = Convert.ToInt32(GrvChiphi.GetFocusedRowCellValue(TenCP));
                    var KT = (from dv in _lDichVu.Where(p => p.MaDV == _MaDV)
                              select new { dv.PLoai, DonGia = dv.DonGia, dv.DonVi, dv.TrongDM, dv.DonGiaBHYT, dv.DonGiaTT15 ,dv.DonGiaTT39}).OrderByDescending(p => p.PLoai).ToList();
                    if (KT.Count > 0 && KT.First().PLoai == 2)
                    {
                        int Trongdm = 0;
                        if (GrvChiphi.GetFocusedRowCellValue(colTrongBH) != null && GrvChiphi.GetFocusedRowCellValue(colTrongBH) != null)
                        {
                            Trongdm = Convert.ToInt32(GrvChiphi.GetFocusedRowCellValue(colTrongBH));
                        }
                        string dtuong = lup_Doituong.Text;
                        bool giacu = GiaCu(dtuong, Trongdm, LupNgayNhap.DateTime, LupNgayNhap.DateTime);
                        foreach (var a in KT)
                        {
                            if (giacu)
                                cboDonGia.Items.Add(a.DonGiaTT15);
                            else
                                cboDonGia.Items.Add(a.DonGiaTT39);
                        }
                        GrvChiphi.SetFocusedRowCellValue("DonGia", giacu == true ? KT.First().DonGiaTT15 : KT.First().DonGiaTT39);
                    }
                }
                txtTyLe_EditValueChanged(null, null);
            }
             //if (e.Column.Name == "colTrongBH" )
             //{

             //}
        }

        private void lupMaICD_EditValueChanged(object sender, EventArgs e)
        {
            if (lupMaICD.Text != null)
            {
                string _MaICD = lupMaICD.Text;
                var TB = (from icd in _Data.ICD10.Where(p => p.MaICD == _MaICD) select new { icd.TenICD }).ToList();
                if (TB.Count > 0)
                {
                    lupChandoan.Text = TB.First().TenICD;
                }
            }
        }

        private void SoThe_Leave(object sender, EventArgs e)
        {
            if (Trangthai == 0 || Trangthai == 1)
            {
                string muc = "", _dtuong = "";
                _ketqua = "";
                if (!string.IsNullOrEmpty(SoThe.Text) && SoThe.Text.Length == 20)
                {
                    string chuoi_dau = SoThe.Text;
                    string[] chuoi_tach = chuoi_dau.Split(new Char[] { '-' });

                    foreach (string s in chuoi_tach)
                    {

                        if (s.Trim() != "")
                            _ketqua += s;
                    }
                    //MessageBox.Show(_ketqua + " độ dài "+_ketqua.Length);
                    if (_ketqua.Length == 15)
                    {
                        muc = _ketqua.ToString().Substring(2, 1);
                        _dtuong = _ketqua.ToString().Substring(0, 2);
                        var _person = (_Data.People.Where(p => p.SThe.Contains(_ketqua)).OrderBy(a => a.TenBNhan)).ToList();
                        if (_person.Count > 0)
                        {

                            //set gia tri
                            txtNhapTBN.Text = _person.First().TenBNhan;

                            if (_person.First().NSinh != null)
                                txtNamSinh.Text = _person.First().NSinh.ToString();
                            txtNgaySinh.Text = _person.First().NgaySinh;
                            txtThangSinh.Text = _person.First().ThangSinh;
                            if (_person.First().NgayHM != null)
                                dtDu5nam.DateTime = _person.First().NgayHM.Value;
                            dtHanBHden.DateTime = _person.First().HanBHDen.Value;
                            dtHanBHTu.DateTime = _person.First().HanBHTu.Value;
                            txtDiaChi.Text = _person.First().DChi;
                            txtMaCS.Text = _person.First().MaCS.Trim();
                            if (_person.First().GTinh.ToString() == "1")
                            {
                                radNamNu.SelectedIndex = 0;
                            }
                            else
                            {
                                radNamNu.SelectedIndex = 1;
                            }
                            cboKhuVuc.Text = _person.First().KhuVuc;


                            //hiển thông tin bổ xung
                            //lupMaTinh.EditValue = _person.First().MaTinh;
                            //lupMaHuyen.EditValue = _person.First().MaHuyen;
                            //lupMaXa.EditValue = _person.First().MaXa;

                            // mức hưởng

                            int muccu = 0;
                            if (!string.IsNullOrEmpty(muc))
                                muccu = Convert.ToInt32(muc);
                            var dt = _Data.DTuongs.Where(p => p.MaDTuong == (_dtuong)).Where(p => p.MucCu == muccu).Select(p => p.MaMuc).ToList();
                            if (dt.Count > 0 && dt.First() != null)
                                lupMHuong.EditValue = dt.First().ToString().Trim();
                            else
                            {

                                lupMHuong.EditValue = muc;
                            }
                            // lupMHuong.Text = muc;
                            //if (muc == "6" || muc == "7") {
                            //    MessageBox.Show("Số thẻ BH đã cũ, Bạn hãy chọn mức hưởng mới cho BN");
                            //lupMHuong.Focus();
                            //}

                            txtMaCS.Focus();
                        }
                        else
                        {
                            int muccu = -1;
                            if (!string.IsNullOrEmpty(muc))
                                muccu = Convert.ToInt32(muc);
                            var dt = _Data.DTuongs.Where(p => p.MaDTuong == (_dtuong)).Where(p => p.MucCu == muccu).Select(p => p.MaMuc).ToList();
                            if (dt.Count > 0 && dt.First() != null)
                                lupMHuong.EditValue = dt.First().ToString().Trim();
                            else
                            {

                                lupMHuong.EditValue = muc;
                            }
                            //resetControl();
                        }
                    }
                    else
                    {
                        //MessageBox.Show("Mã thẻ không hợp lệ! ...");
                        //SoThe.Focus();
                    }
                }
            }
        }



        private void MaCS_Leave(object sender, EventArgs e)
        {
            int _noitinh = -1;
            cboNoitinh.Focus();
            if (!string.IsNullOrEmpty(txtMaCS.Text) && !string.IsNullOrEmpty(SoThe.Text))
            {
                if (txtMaCS.Text.Length < 5 || txtMaCS.Text.Length > 5)
                {
                    MessageBox.Show("Mã CS không hợp lệ");
                    txtMaCS.Focus();
                }
                else
                {
                    var que = (from CSDK in _Data.BenhViens.Where(p => p.MaBV == txtMaCS.Text) select CSDK).ToList();
                    if (que.Count > 0)
                    {
                        txtTenBV.Text = que.First().TenBV.ToString().Trim();
                        string ma = txtMaCS.Text;
                        //var q = from CSDK in DataContect.BenhViens.Where(p => p.MaBV == ma) select  CSDK.MaHuyen;
                        string MaChuQuan = que.First().MaChuQuan.ToString().Trim();
                        if (DungChung.Bien.MaBV.Substring(0, 2) == _ketqua.Substring(3, 2))
                        {
                            if
                                (ma == DungChung.Bien.MaBV)
                            {
                                radTuyen.SelectedIndex = 0;
                                if (_ketqua.Substring(5, 2) == DungChung.Bien.MaHuyen)
                                {
                                    _noitinh = 1;
                                }
                                else
                                {
                                    _noitinh = 1;
                                }
                            }
                            else
                            {
                                if (MaChuQuan == DungChung.Bien.MaBV)
                                {
                                    DialogResult result;
                                    result = MessageBox.Show("Bệnh nhân có giấy giới thiệu?", "BN trong huyện.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (result == DialogResult.Yes)
                                    {
                                        radTuyen.SelectedIndex = 0;
                                    }
                                    else
                                    {
                                        radTuyen.SelectedIndex = 1;
                                    }
                                    if (_ketqua.Substring(5, 2) == DungChung.Bien.MaHuyen)
                                    {
                                        _noitinh = 1;
                                    }
                                    else
                                    {
                                        _noitinh = 1;
                                    }
                                }
                                else
                                {
                                    radTuyen.SelectedIndex = 1;
                                    _noitinh = 2;
                                }

                            }

                        }
                        else
                        {

                            if (MaChuQuan == DungChung.Bien.MaBV)
                                radTuyen.SelectedIndex = 0;
                            else
                                radTuyen.SelectedIndex = 1;
                            //if (ma == DungChung.Bien.MaBV)
                            //    _noitinh = 1;
                            //else
                            _noitinh = 3;
                        }
                        cboNoitinh.SelectedIndex = _noitinh;
                    }
                    else
                    {
                        DialogResult _result = MessageBox.Show("Mã số cơ sở không có trong danh mục CSDK KCB \n Bạn muốn có muốn thêm Mã CS vào trong DM ?", "Hỏi thêm mã CS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (_result == DialogResult.Yes)
                        {
                            FormDanhMuc.Frm_Dm_BenhVien frm = new FormDanhMuc.Frm_Dm_BenhVien(txtMaCS.Text.Trim());
                            frm.ShowDialog();
                            txtMaCS.Focus();
                        }
                    }
                }
            }
        }

        private void HoTen_Leave(object sender, EventArgs e)
        {

            txtNhapTBN.Text = DungChung.Ham.ToFirstUpper(txtNhapTBN.Text.Trim());
        }


        private void SBTHuy_Click(object sender, EventArgs e)
        {
            this.Frm_NhapBNNgoaiBV_Load(sender, e);
            EnableControl(true);
            Trangthai = -1;
            //if (Trangthai == 0)
            //{
            //    Xoatrang();
            //}
            //else {
            //    MessageBox.Show("Hủy trong trường hợp thêm mới BN");
            //}
        }

        private void lupNgayRV_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lupNgayRV.Text))
                if ((lupNgayRV.DateTime - LupNgayNhap.DateTime).Days < 0)
                {
                    MessageBox.Show("Ngày ra viện phải >= ngày vào viện!");
                    lupNgayRV.Focus();
                }
                else
                {
                    {
                        int ngay = (lupNgayRV.DateTime - LupNgayNhap.DateTime).Days;
                        if (ngay == 0)
                            txtNgayDT.Text = "1";
                        else
                            txtNgayDT.Text = (ngay).ToString();
                    }
                }
        }

      
        double _ptthanhtoan = 0;
        private void lupMHuong_EditValueChanged(object sender, EventArgs e)
        {
            if (lup_Doituong.Text == "BHYT" && lupMHuong.EditValue != null && lupMHuong.EditValue.ToString() != "")
            {
                int _tuyen = -1;
                if (radTuyen.SelectedIndex == 0)
                    _tuyen = 1;
                else
                    _tuyen = 2;
                int _tuyenbv = 0;
                int _pttt = 0;
                string _muc = "";
                _muc = lupMHuong.EditValue.ToString();
                _pttt = DungChung.Ham._PtramTT(_Data, _muc);
                _tuyenbv = DungChung.Ham.hangBV(DungChung.Bien.MaBV);
                switch (_tuyenbv)
                {
                    case 1:

                        break;
                    case 2:
                        break;
                    case 3:
                        if (_tuyen == 1)
                        {
                            if (chkDu6thang.Checked && (dtDu5nam.DateTime - LupNgayNhap.DateTime).Days >= 0)
                                _ptthanhtoan = 100;
                            else
                                _ptthanhtoan = _pttt;
                        }
                        else
                        {
                            if (cboKhuVuc.Text == "K1" || cboKhuVuc.Text == "K2" || cboKhuVuc.Text == "K3")
                            {
                                _ptthanhtoan = _pttt;
                            }
                            else
                                _ptthanhtoan = _pttt * 0.7;
                        }
                        break;
                    case 4:
                        if (_tuyen == 1)
                        {
                            _ptthanhtoan = 100;
                        }
                        else
                        {
                            _ptthanhtoan = 70;
                        }
                        break;
                }
                txtTyLe.Text = _ptthanhtoan.ToString();
            }
            else
            {
                txtTyLe.Text = "0";
            }
        }

        private void radTuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            lupMHuong_EditValueChanged(null, null);
        }

        private void txtMaBVKB_EditValueChanged(object sender, EventArgs e)
        {
            lupMHuong_EditValueChanged(null, null);

        }
        
        List<BenhVien> _lbv = new List<BenhVien>();
        private void cboNoiKCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboNoiKCB.SelectedIndex)
            {
                case 0:
                   
                    lupMaBVgt.Properties.DataSource = _lbv.Where(p => p.MaBV == (DungChung.Bien.MaBV)).ToList();
                    break;
                case 1:
                    _lbv = (from bv in _Data.BenhViens
                            join kp in _Data.KPhongs.Where(p => p.PLoai == ("Xã phường")) on bv.MaBV equals kp.MaBVsd
                            select bv).OrderBy(p => p.TenBV).ToList();
                    lupMaBVgt.Properties.DataSource = _lbv.ToList();
                    break;
                case 2:
                    _lbv = (from bv in _Data.BenhViens
                            join kp in _Data.KPhongs.Where(p => p.PLoai == ("PK khu vực")) on bv.MaBV equals kp.MaBVsd
                            select bv).OrderBy(p => p.TenBV).ToList();
                    lupMaBVgt.Properties.DataSource = _lbv.ToList();
                    break;
            }
        }

        private void lupMaBVgt_EditValueChanged(object sender, EventArgs e)
        {
            if (lupMaBVgt.EditValue != null)
                txtMaBVKB.Text = lupMaBVgt.EditValue.ToString();
            else
                txtMaBVKB.Text = "";
        }

        private void cboDoituong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lup_Doituong.Text == "BHYT")
            {
                panelControl3.Visible = true;
                cboKSK.Visible = false;
                radNoitru.Visible = true;
            }
            else
            {
                panelControl3.Visible = false;
                if (lup_Doituong.Text == "Dịch vụ")
                {
                    cboKSK.Visible = false;
                    radNoitru.Visible = true;
                }
                else
                {
                    cboKSK.Visible = true;
                    radNoitru.Visible = false;
                }
            }
        }

        private void chkDu6thang_CheckedChanged(object sender, EventArgs e)
        {
            lupMHuong_EditValueChanged(null, null);
        }

        private void txtTyLe_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTyLe.Text) && (Trangthai != -1))
            {
                double _tyle = 0;
                double _tienbn = 0, _tienbh = 0, _Thanhtien = 0, _DGia = 0, _tongtien = 0;
                _tyle = Convert.ToDouble(txtTyLe.Text);
                _tyle = _tyle / 100;
                if (lup_Doituong.Text == "BHYT")
                {
                    if (radTuyen.SelectedIndex == 0)
                    {
                        for (int i = 0; i <= GrvChiphi.RowCount; i++)
                        {
                            if (GrvChiphi.GetRowCellValue(i, ThanhTien) != null)
                            {
                                if (GrvChiphi.GetRowCellValue(i, colTrongBH) != null && GrvChiphi.GetRowCellValue(i, colTrongBH).ToString() == "1")
                                    _tongtien += Convert.ToDouble(GrvChiphi.GetRowCellValue(i, ThanhTien));
                            }
                        }
                        if (_tongtien >= DungChung.Bien.GHanTT100)
                        {
                            for (int i = 0; i <= GrvChiphi.RowCount; i++)
                            {
                                double tt = 0;
                                if (GrvChiphi.GetRowCellValue(i, ThanhTien) != null)
                                    tt = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, ThanhTien));
                                if (GrvChiphi.GetRowCellValue(i, colTrongBH) != null && GrvChiphi.GetRowCellValue(i, colTrongBH).ToString() == "1")
                                {
                                    _tienbh = tt * _tyle;
                                    _tienbn = tt - _tienbh;
                                    GrvChiphi.SetRowCellValue(i, TienBH, _tienbh);
                                    GrvChiphi.SetRowCellValue(i, TienBN, _tienbn);
                                }
                                else
                                {
                                    GrvChiphi.SetRowCellValue(i, TienBH, 0);
                                    GrvChiphi.SetRowCellValue(i, TienBN, tt);
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < GrvChiphi.RowCount; i++)
                            {
                                double tt = 0;
                                if (GrvChiphi.GetRowCellValue(i, ThanhTien) != null)
                                    tt = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, ThanhTien));
                                if (GrvChiphi.GetRowCellValue(i, colTrongBH) != null && GrvChiphi.GetRowCellValue(i, colTrongBH).ToString() == "1")
                                {
                                    GrvChiphi.SetRowCellValue(i, TienBH, tt);
                                    GrvChiphi.SetRowCellValue(i, TienBN, 0);
                                }
                                else
                                {
                                    GrvChiphi.SetRowCellValue(i, TienBH, 0);
                                    GrvChiphi.SetRowCellValue(i, TienBN, tt);
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i <= GrvChiphi.RowCount; i++)
                        {

                            double tt = 0;
                            if (GrvChiphi.GetRowCellValue(i, ThanhTien) != null)
                                tt = Convert.ToDouble(GrvChiphi.GetRowCellValue(i, ThanhTien));
                            if (GrvChiphi.GetRowCellValue(i, colTrongBH) != null && GrvChiphi.GetRowCellValue(i, colTrongBH).ToString() == "1")
                            {
                                _tienbh = tt * _tyle;
                                _tienbn = tt - _tienbh;
                                GrvChiphi.SetRowCellValue(i, TienBH, _tienbh);
                                GrvChiphi.SetRowCellValue(i, TienBN, _tienbn);
                            }
                            else
                            {
                                GrvChiphi.SetRowCellValue(i, TienBH, 0);
                                GrvChiphi.SetRowCellValue(i, TienBN, tt);
                            }
                        }
                    }
                }
            }
        }

        private void GrvChiphi_RowCellClick_1(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colXoa")
            {
                DialogResult _result = MessageBox.Show("Xóa chi phí: " + GrvChiphi.GetFocusedRowCellDisplayText(MaDV), "xóa chi tiết!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_result == DialogResult.Yes)
                {
                    int xoaid = 0;
                    if (GrvChiphi.GetFocusedRowCellValue(idVPhict) != null && GrvChiphi.GetFocusedRowCellValue(idVPhict).ToString() != "")
                        xoaid = Convert.ToInt32(GrvChiphi.GetFocusedRowCellValue(idVPhict));
                    if (xoaid > 0)
                    {
                        var xoa = _Data.VienPhicts.Single(p => p.idVPhict == xoaid);
                        _Data.VienPhicts.Remove(xoa);
                        _Data.SaveChanges();
                        GrvChiphi.DeleteSelectedRows();
                    }
                    else
                    {
                        GrvChiphi.DeleteSelectedRows();
                    }
                }
            }
        }

        private void GrvBenhNhan_DataSourceChanged(object sender, EventArgs e)
        {
            GrvBenhNhan_FocusedRowChanged(null, null);
        }

        private void dtDu5nam_EditValueChanged(object sender, EventArgs e)
        {
            if ((dtDu5nam.DateTime - LupNgayNhap.DateTime).Days >= 0)
            {
                chkDu6thang.Properties.ReadOnly = false;
            }
            else
            {
                chkDu6thang.Checked = false;
                chkDu6thang.Properties.ReadOnly = true;
            }
        }

        private void cboKhuVuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            lupMHuong_EditValueChanged(null, null);
        }
        int _maubk = 0;
        private void btnIn_Click(object sender, EventArgs e)
        {
            //int _mabn = 0, ot = 0;
            //    if (Int32.TryParse(txtMaBNhan.Text, out ot))
            //        _mabn = Convert.ToInt32(txtMaBNhan.Text);
            int _mabn = 0;
            if (GrvBenhNhan.GetFocusedRowCellValue(MaBN) != null)
            {
                _mabn = Convert.ToInt32(GrvBenhNhan.GetFocusedRowCellValue(MaBN));
                QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                DungChung.Ham.In_BangKe01_02(_data, _mabn, _maubk, 0);
                _maubk = 0;
            }

        }

        private void lupPhongKham_EditValueChanged(object sender, EventArgs e)
        {
            if (lupPhongKham.EditValue != null)
            {
                int _ma = Convert.ToInt32(lupPhongKham.EditValue);
                var xoa = _Data.KPhongs.Where(p => p.MaKP == _ma).Select(p => p.PLoai).ToList();
                if (xoa.Count > 0)
                {
                    switch (xoa.First())
                    {
                        case "PK khu vực":
                            cboNoiKCB.SelectedIndex = 2;
                            break;
                        case "Xã phường":
                            cboNoiKCB.SelectedIndex = 1;
                            break;
                        case "Phòng khám":
                            cboNoiKCB.SelectedIndex = 0;
                            break;
                        case "Lâm sàng":
                            cboNoiKCB.SelectedIndex = 0;
                            break;
                    }
                }
            }
        }
        List<BenhVien> _lbv2 = new List<BenhVien>();
        private void cboTimNoiKham_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboTimNoiKham.SelectedIndex)
            {
                case 0:
                    _lbv2 = _Data.BenhViens.OrderBy(p => p.TenBV).ToList();
                    LupTimKP.Properties.DataSource = _lbv2.Where(p => p.MaBV == (DungChung.Bien.MaBV)).ToList();
                    break;
                case 1:
                    _lbv2 = (from bv in _Data.BenhViens
                             join kp in _Data.KPhongs.Where(p => p.PLoai == ("Xã phường")) on bv.MaBV equals kp.MaQD
                             select bv).OrderBy(p => p.TenBV).ToList();
                    LupTimKP.Properties.DataSource = _lbv2.ToList();
                    break;
                case 2:
                    _lbv2 = (from bv in _Data.BenhViens
                             join kp in _Data.KPhongs.Where(p => p.PLoai == ("PK khu vực")) on bv.MaBV equals kp.MaQD
                             select bv).OrderBy(p => p.TenBV).ToList();
                    LupTimKP.Properties.DataSource = _lbv2.ToList();
                    break;
            }

            DSBN();
        }

        private void cbo_NoiTru_SelectedIndexChanged(object sender, EventArgs e)
        {
            DSBN();
        }

        private void cboInBangKe_SelectedIndexChanged(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //int _mabn = 0, ot = 0;
            //if (Int32.TryParse(txtMaBNhan.Text, out ot))
            //    _mabn = Convert.ToInt32(txtMaBNhan.Text);
            int _mabn = 0;
            if (GrvBenhNhan.GetFocusedRowCellValue(MaBN) != null)
            {
                _mabn = Convert.ToInt32(GrvBenhNhan.GetFocusedRowCellValue(MaBN));

                switch (cboInBangKe.SelectedIndex)
                {
                    case 0:
                        _maubk = 5;
                        break;
                    case 1:
                        _maubk = 4;
                        break;
                    case 2:
                        // phiếu thanh toán ra viện mẫu 38
                        FormThamSo.frm_XemChiPhi._phieuTTRV_MS38(_dataContext, _mabn);

                        break;
                    case 3:
                        FormThamSo.frm_XemChiPhi._phieuTTRV_MS40(_dataContext, _mabn);
                        break;
                }
                if (cboInBangKe.SelectedIndex != -1 && cboInBangKe.SelectedIndex != 2 && cboInBangKe.SelectedIndex != 3)
                    btnIn_Click(sender, e);
                _maubk = 0;
                cboInBangKe.SelectedIndex = -1;
            }
        }
    }
}

