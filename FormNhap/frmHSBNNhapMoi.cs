using DevExpress.XtraEditors.Controls;
using Newtonsoft.Json;
using QLBV.ChucNang;
using QLBV.DungChung;
using QLBV_Database;
using QLBV_Database.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;

namespace QLBV.FormNhap
{
    public partial class frmHSBNNhapMoi : DevExpress.XtraEditors.XtraForm
    {
        private QLBV_Database.QLBVEntities _dataContext = EntityDbContext.DbContext;
        private List<Person> _danhsachBNBHYT = new List<Person>();
        private int _noitinh = 1;
        private string _ketqua = "";
        private int _mabn = 0;
        private string _suahs = "";
        private int TTLuu = 1, _boPhan = 0;

        public delegate void getString(int mabn);

        public getString getdata;
        private int sodk = 0;

        public frmHSBNNhapMoi()
        {
            InitializeComponent();
        }

        public frmHSBNNhapMoi(int sdk)
        {
            sodk = sdk;
            InitializeComponent();
        }

        public frmHSBNNhapMoi(int ttluu, int mabn)
        {
            InitializeComponent();
            TTLuu = ttluu;
            _mabn = mabn;
        }

        public frmHSBNNhapMoi(int ttluu, int mabn, int _bPhan)
        {
            InitializeComponent();
            TTLuu = ttluu;
            _mabn = mabn;
            _boPhan = _bPhan;
        }

        public frmHSBNNhapMoi(int ttluu, int mabn, string sua)
        {
            InitializeComponent();
            TTLuu = ttluu;
            _mabn = mabn;
            _suahs = sua;
        }

        public class THX
        {
            public string MaTinh { get; set; }
            public string TenTinh { get; set; }
            public string MaHuyen { get; set; }
            public string TenHuyen { get; set; }
            public string MaXa { get; set; }
            public string TenXa { get; set; }
            public string VietTat { get; set; }
            public string TenVietTat { get; set; }
        }

        private List<THX> listTHX = new List<THX>();

        private void GetTinhHuyenXa()
        {
            listTHX = (from t in _dataContext.DmTinhs
                       join h in _dataContext.DmHuyens on t.MaTinh equals h.MaTinh
                       join x in _dataContext.DmXas on h.MaHuyen equals x.MaHuyen
                       select new THX
                       {
                           MaTinh = t.MaTinh,
                           TenTinh = t.TenTinh,
                           MaHuyen = h.MaHuyen,
                           TenHuyen = h.TenHuyen,
                           MaXa = x.MaXa,
                           TenXa = x.TenXa,
                           VietTat = x.XaVT + h.HuyenVT + t.TinhVT,
                           TenVietTat = x.TenXa + ", " + h.TenHuyen + ", " + t.TenTinh,
                       }
                       ).ToList();
        }

        private void E_TTBoXung(bool t)
        {
            lupMaTinh.Enabled = t;
            lupMaHuyen.Enabled = t;
            lupMaXa.Enabled = t;
            txtSoDT.Enabled = t;
            lupNgheNghiep.Enabled = t;
            txtNoiLV.Enabled = t;
            txtNThan.Enabled = t;
            txtDTNThan.Enabled = t;
            txtSoKSinh_CMT.Enabled = t;
            lupTinhKhaiSinh.Enabled = t;
            lupXaKhaiSinh.Enabled = t;
            lupMaHuyenKhaiSinh.Enabled = t;
            txtHKKT.Enabled = t;
            txtDChiKSinh.Enabled = t;
            lupQuanHe.Enabled = t;
        }

        private void E_BHYT(bool T)
        {
            if (cboDTuong.Text == "BHYT")
            {
                txtTimSTHE.Enabled = T;
                txtTimSTHE_New.Enabled = T;
                txtMaCS.Enabled = T;
                dtHanBHTu.Enabled = T;
                dtHanBHden.Enabled = T;
                cboCapCuu.Enabled = T;
                radTuyen.Enabled = T;
                txtDiaChi1.Enabled = txtDiaChi.Enabled = T;
                txtTrieuChung.Enabled = T;
                lupMaBVgt.Enabled = T;
                txtDT12345.Enabled = T;
                txtCDNoiGT.Enabled = T;

                lupMaICDkb.Enabled = T;

                lupChanDoanKb.Enabled = T;

                dtDu5nam.Enabled = T;
            }
        }

        private void E_TTBN(bool T)
        {
            dtNgayN.Enabled = T;
            cboDTuong.Enabled = T;
            radNoiTru.Enabled = T;
            cboCapCuu.Enabled = T;
            cboPLKham.Enabled = T;
            txtNhapTBN.Enabled = T;
            radNamNu.Enabled = T;
            txtNamSinh.Enabled = T;
            txtNgaySinh.Enabled = T;
            txtThangSinh.Enabled = T;
            lupKhoaKham.Enabled = T;
            txtDT12345.Enabled = T;
            cboChuyenKhoa.Enabled = T;
        }

        private void EnableControl(bool T)
        {
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                ckcchuyenkhoan.Enabled = false;
            else
                ckcchuyenkhoan.Enabled = !T;
            txtTimSTHE.Enabled = T;
            txtTimSTHE_New.Enabled = T;
            txtCode.Enabled = T;
            txtMaCS.Enabled = T;
            dtHanBHTu.Enabled = T;
            dtHanBHden.Enabled = T;
            radTuyen.Enabled = T;
            lupMaBVgt.Enabled = T;
            lupMaNoigt.Enabled = T;
            lupMaICDkb.Enabled = T;
            lupChanDoanKb.Enabled = T;

            dtHanBHTu.DateTime = new DateTime(DateTime.Now.Year, 1, 1);
            dtHanBHden.DateTime = DungChung.Ham.NgayDen((new DateTime(DateTime.Now.Year, 1, 1)).AddYears(1).AddDays(-1));
            radTuyen.SelectedIndex = -1;
            lupMHuong.Enabled = T;
            chkLuongCS.Enabled = T;
            cboKhuVuc.Enabled = T;
            txtTimSTHE.Text = "";
            txtTimSTHE_New.Text = "";
            txtMaCS.Text = "";
            txtCode.ResetText();
            if (T)
            {
                labSoThe.ForeColor = System.Drawing.Color.DarkRed;
                labMaCS.ForeColor = System.Drawing.Color.DarkRed;
                labHanBH.ForeColor = System.Drawing.Color.DarkRed;
            }
            else
            {
                labSoThe.ForeColor = System.Drawing.Color.DarkGray;
                labMaCS.ForeColor = System.Drawing.Color.DarkGray;
                labHanBH.ForeColor = System.Drawing.Color.DarkGray;
            }
        }

        private List<BenhNhan> _benhnhan = new List<BenhNhan>();
        private string mahuyen = DungChung.Bien.MaHuyen;

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            resetValue();
            resetTTBX();
            resetControl();
        }

        private void EnableHanMucLuongCS(bool T)
        {
            lupMHuong.Enabled = !T;
            chkLuongCS.Enabled = !T;
            cboKhuVuc.Enabled = !T;
        }

        private void resetValue()
        {
            idperson = 0;
            personCode = "";
            _ketqua = "";
        }

        private void resetControl()
        {
            txtTimSTHE.Text = "";
            txtTimSTHE_New.Text = "";
            txtMaCS.Text = "";
            dtHanBHTu.Text = "";
            dtHanBHden.Text = "";
            radTuyen.SelectedIndex = -1;
            txtNhapTBN.Text = "";
            radNamNu.SelectedIndex = -1;
            txtTuoi.Text = "";
            txtNgaySinh.Text = "";
            txtThangSinh.Text = "";
            txtNamSinh.Text = "";
            txtDiaChi1.Text = txtDiaChi.Text = "";
            txtTrieuChung.Text = "";
            lupMaBVgt.EditValue = "";

            txtCDNoiGT.Text = "";

            lupMaICDkb.Text = "";

            lupChanDoanKb.Text = "";

            lupKhoaKham.EditValue = "";

            txtSoTT.Text = "";

            chkLuongCS.Checked = false;
            lupMHuong.EditValue = "";
            cboKhuVuc.Text = "";
            txtTenBV.Text = "";
            txtMaBN.ResetText();
            txtMaDT.ResetText();
            txtDT12345.ResetText();
            dtDu5nam.DateTime = new DateTime();
            lupNoiNgoaiTinh.EditValue = -1;
        }

        private void resetTTBX()
        {
            lupMaTinh.EditValue = "";
            lupTinhKhaiSinh.EditValue = "";
            lupMaHuyenKhaiSinh.EditValue = "";
            lupXaKhaiSinh.EditValue = "";
            lupMaHuyen.EditValue = "";
            lupMaXa.EditValue = "";
            lupDantoc.EditValue = "";
            txtSoDT.Text = "";
            lupNgheNghiep.EditValue = "";
            txtNoiLV.Text = "";
            txtNThan.Text = "";
            txtDTNThan.Text = "";
            txtSoKSinh_CMT.Text = "";
            txtsohsbnmantinh.Text = "";
            txtDChiKSinh.Text = "";
            txtHKKT.Text = "";
            lupQuanHe.EditValue = "0";
        }

        //kiem tra truoc khi Save

        #region Kiemtra trước khi lưu

        private bool KT()
        {
            //var tk = DataContext.ADMINs.FirstOrDefault(o => o.TenDN == DungChung.Bien.TenDN);

            //if (TTLuu == 2 && tk != null && tk.CapDo != 9)
            //{
            //    MessageBox.Show("Tài khoản không có quyền sửa HSBN");
            //    return false;
            //}

            if (string.IsNullOrEmpty(cboDTuong.Text))
            {
                MessageBox.Show("Bạn chưa chọn đối tượng khám");
                return false;
            }
            //if (ckcCDCLS.Checked)
            //{
            //    if (txtTrieuChung != null)
            //    {
            //        MessageBox.Show("Nhập triệu chứng để có thể \n chỉ định CLS tại phòng tiếp đón", "Thông báo", MessageBoxButtons.OK);
            //        txtTrieuChung.Focus();
            //        return false;
            //    }
            //}
            var vp = _dataContext.VienPhis.Where(p => p.MaBNhan == _mabn && p.ExportBHXH == true).FirstOrDefault();
            if (vp != null && TTLuu == 2)
            {
                MessageBox.Show("Đã gửi dữ liệu, không thể sửa!", "Thông báo", MessageBoxButtons.OK);

                return false;
            }

            if (cboCapCuu.SelectedIndex < 0)
            {
                MessageBox.Show("Bạn chưa chọn hiện trạng", "Thông báo", MessageBoxButtons.OK);
                cboCapCuu.Focus();
                return false;
            }
            if (DungChung.Bien.MaBV == "01071" ||
                DungChung.Bien.MaBV == "01049" ||
                DungChung.Bien.MaBV == "12345" ||
                DungChung.Bien.MaBV == "24297" ||
                DungChung.Bien.MaBV == "56789")
            {
                if (cboCapCuu.SelectedIndex == 1 && cboChuyenKhoa.Text == null)
                {
                    MessageBox.Show("Với bệnh nhân cấp cứu, yêu cầu nhập đầy đủ trường 'Tai nạn'",
                        "Thông báo", MessageBoxButtons.OK);
                    cboChuyenKhoa.Focus();
                    return false;
                }
            }
            if (TTLuu == 2)
            {
                var kb = _dataContext.BNKBs.Where(p => p.MaBNhan == _mabn).OrderBy(p => p.IDKB).FirstOrDefault();
                if (kb != null)
                {
                    if (dtNgayN.DateTime > kb.NgayKham.Value)
                    {
                        MessageBox.Show("Ngày nhập không được lớn hơn ngày khám", "Thông báo", MessageBoxButtons.OK);
                        dtNgayN.Focus();
                        return false;
                    }
                }
            }
            if (TTLuu == 1 && (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009"))
            {
                var kt = (from bn in _dataP.BenhNhans
                          .Where(p => p.Status != 3)
                          .Where(p => _ketqua.Length > 0 && p.SThe == (_ketqua))
                          select bn).ToList();
                if (kt.Count > 0)
                {
                    MessageBox.Show("Số thẻ đã được nhập cho bệnh nhân mã: " + kt.First().MaBNhan.ToString() + " đang điều trị, không thể thêm mới");
                    return false;
                }
            }

            //if (!string.IsNullOrWhiteSpace(txtSoDT.Text.Trim()) && DungChung.Bien.MaBV == "56789")
            //{
            //    var ttbx = _dataP.TTboXungs.Where(o => o.DThoai == txtSoDT.Text.Trim());

            //    if (TTLuu == 1 && ttbx != null && ttbx.Count() > 0)
            //    {
            //        var bn = _dataP.BenhNhans.Where(o => ttbx.Select(p => p.MaBNhan).Contains(o.MaBNhan)).ToList();
            //        if (bn != null && bn.Count > 0)
            //        {
            //            var benhNhan = bn.FirstOrDefault(o => o.NNhap.Value.Date == dtNgayN.DateTime.Date);
            //            if (benhNhan != null)
            //            {
            //                if (benhNhan.TenBNhan == txtNhapTBN.Text.Trim())
            //                {
            //                    if (benhNhan.Status == 3)
            //                    {
            //                        XtraMessageBox.Show("Bệnh nhân đã thanh toán trong ngày", "Thông báo");
            //                        return false;
            //                    }
            //                    else
            //                    {
            //                        var kp = DataContext.KPhongs.FirstOrDefault(o => o.MaKP == benhNhan.MaKP);
            //                        if (kp != null)
            //                        {
            //                            XtraMessageBox.Show("Bệnh nhân đang điều trị tại phòng " + kp.TenKP, "Thông báo");
            //                            return false;
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    XtraMessageBox.Show("Số điện thoại " + ttbx.First().DThoai + " đã được nhập cho bệnh nhân " + benhNhan.TenBNhan + ",bạn không thể lưu", "Thông báo");
            //                    return false;
            //                }
            //            }
            //        }
            //    }
            //}

            if (cboDTuong.Text == "BHYT")
            {
                if (string.IsNullOrEmpty(txtTimSTHE.Text))
                {
                    MessageBox.Show("Bạn chưa nhập thẻ BHYT");
                    txtTimSTHE.Focus();
                    return false;
                }
                else
                {
                    if (_ketqua.Length < 15 || _ketqua.Length > 15)
                    {
                        MessageBox.Show("Số thẻ không hợp lệ");
                        txtTimSTHE.Focus();
                        return false;
                    }
                    else
                    {
                        if (_ketqua.Length == 15)
                        {
                            //kiểm tra nhóm đối tượng có trong dm
                            string madt = _ketqua.Substring(0, 2);
                            var ktMaDT = _dataP.DTuongs.Where(p => p.MaDTuong.Contains(madt)).ToList();
                            if (ktMaDT.Count <= 0)
                            {
                                MessageBox.Show("Mã đối tượng không hợp lệ!");
                                txtTimSTHE.Focus();
                                return false;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(lupMHuong.Text))
                                {
                                    MessageBox.Show("Bạn chưa chọn mức hưởng");
                                    lupMHuong.Focus();
                                    return false;
                                }
                                else
                                {
                                    int a;

                                    a = Convert.ToInt32(lupMHuong.Text);
                                    if (a < 1 || a > 5)
                                    {
                                        MessageBox.Show("Mức hưởng không hợp lệ");
                                        lupMHuong.Focus();
                                        return false;
                                    }
                                    else
                                    {
                                        #region dung thêm

                                        int namsinh = Int32.Parse(txtNamSinh.Text.Trim());
                                        if (dtHanBHTu.EditValue != null)
                                        {
                                            if (namsinh - dtHanBHTu.DateTime.Year > 0)
                                            {
                                                MessageBox.Show("Giới hạn thẻ phải lớn hơn ngày sinh");
                                                txtNamSinh.Focus();
                                                return false;
                                            }
                                            else if (namsinh - dtHanBHTu.DateTime.Year == 0)
                                            {
                                                if (txtNgaySinh.Text == "" || txtThangSinh.Text == "")
                                                {
                                                    MessageBox.Show("Giới hạn thẻ phải lớn hơn ngày sinh");
                                                    txtNamSinh.Focus();
                                                    return false;
                                                }
                                                else if (Convert.ToInt32(txtThangSinh.Text) > dtHanBHTu.DateTime.Month)
                                                {
                                                    MessageBox.Show("Giới hạn thẻ phải lớn hơn ngày sinh");
                                                    txtThangSinh.Focus();
                                                    return false;
                                                }
                                                else if ((Convert.ToInt32(txtThangSinh.Text) == dtHanBHTu.DateTime.Month) && (Convert.ToInt32(txtNgaySinh.Text) > dtHanBHTu.DateTime.Day))
                                                {
                                                    MessageBox.Show("Giới hạn thẻ phải lớn hơn ngày sinh");
                                                    txtNgaySinh.Focus();
                                                    return false;
                                                }
                                            }
                                        }

                                        #endregion dung thêm

                                        var kt = (from bn in _dataP.BenhNhans.Where(p => p.SThe == (_ketqua))
                                                  select bn).ToList();
                                        if (kt.Count > 0 && kt.Where(p => p.TenBNhan.ToLower() != txtNhapTBN.Text.ToLower()).ToList().Count > 0)
                                        {
                                            bool ktraSoThe = true;
                                            if (DungChung.Bien.MaBV == "30004" &&
                                                _ketqua.Length > 2 &&
                                                _ketqua.Substring(0, 2) == "TE")
                                            {
                                                ktraSoThe = false;
                                            }
                                            if (ktraSoThe == true)
                                            {
                                                DialogResult _result = MessageBox.Show("Số thẻ:'" + _ketqua +
                                                    "' đã nhập cho bệnh nhân: " + kt.First().TenBNhan +
                                                    "\n Bạn vẫn muốn lưu", "Hỏi lưu",
                                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                                if (_result == DialogResult.No)
                                                {
                                                    txtTimSTHE.Focus();
                                                    return false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    string tenkpdt = DungChung.Ham.KT_RaVien(_dataP, _ketqua, TTLuu);
                    if (!string.IsNullOrEmpty(tenkpdt))
                    {
                        if (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009")
                            return false;
                        else if (DungChung.Bien.MaBV != "30372")
                        {
                            DialogResult _result = MessageBox.Show("Bệnh nhân: " + txtNhapTBN.Text +
                                " đang được khám|điều trị tại: " + tenkpdt +
                                ", bạn vẫn muốn nhập tiếp?", "Hỏi lưu",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (_result == DialogResult.No)
                                return false;
                        }
                    }
                    if (string.IsNullOrEmpty(txtMaCS.Text))
                    {
                        MessageBox.Show("Bạn chưa nhập mã cơ sở");
                        txtMaCS.Focus();
                        return false;
                    }
                    else
                    {
                        if (txtMaCS.Text.Length != 5)
                        {
                            MessageBox.Show("Mã cơ sở không hợp lệ");
                            txtMaCS.Focus();
                            return false;
                        }
                        else
                        {
                            _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                            var que = (from CSDK in _dataContext.BenhViens.Where(p => p.MaBV == txtMaCS.Text)
                                       select CSDK).ToList();
                            if (que.Count <= 0)
                            {
                                MessageBox.Show("Mã cơ sở không hợp lệ");
                                txtMaCS.Focus();
                                return false;
                            }
                        }
                    }
                    if (dtHanBHTu.EditValue != null)
                    {
                        if (DungChung.Ham.NgayTu(dtHanBHTu.DateTime) > DungChung.Ham.NgayTu(System.DateTime.Now))
                        {
                            MessageBox.Show("Hạn thẻ BHYT từ lớn hơn ngày hiện tại, vui lòng kiểm tra lại");
                            dtHanBHTu.Focus();
                            return false;
                        }
                        if (dtHanBHTu.DateTime.Year < 2000)
                        {
                            MessageBox.Show("Hạn thẻ BH không hợp lệ, vui lòng kiểm tra lại");
                            dtHanBHTu.Focus();
                            return false;
                        }
                        if (ckcBoXungThe.Checked == false &&
                            dtHanBHTu.DateTime > dtNgayN.DateTime &&
                            cboDTuong.Text == "BHYT")
                        {
                            MessageBox.Show("Ngày nhập không được nhỏ hơn Hạn thẻ từ");
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bạn chưa nhập hạn thẻ BH");
                        dtHanBHTu.Focus();
                        return false;
                    }
                    if (dtHanBHden.EditValue != null)
                    {
                        if (DungChung.Ham.NgayTu(dtHanBHden.DateTime) < DungChung.Ham.NgayTu(System.DateTime.Now))
                        {
                            MessageBox.Show("Thẻ bảo hiểm đã hết hạn, vui lòng kiểm tra lại");
                            dtHanBHden.Focus();
                            return false;
                        }
                        if (dtHanBHden.DateTime.Year - 6 > System.DateTime.Now.Year)
                        {
                            MessageBox.Show("Hạn thẻ BH không hợp lệ, vui lòng kiểm tra lại");
                            dtHanBHden.Focus();
                            return false;
                        }
                        // bỏ nhập hạn thẻ nhỏ hơn 1 tháng tất cả các đơn vị c.liêu
                        //if ((dtHanBHden.DateTime - dtHanBHTu.DateTime).Days < 28 && DungChung.Bien.MaBV != "20001")
                        //{
                        //    MessageBox.Show("Giới hạn thẻ phải lớn hơn 01 tháng, vui lòng kiểm tra lại hạn thẻ");
                        //    return false;
                        //}
                    }
                    else
                    {
                        MessageBox.Show("Bạn chưa nhập hạn thẻ BH");
                        dtHanBHden.Focus();
                        return false;
                    }
                    //if (DungChung.Bien.MaBV != "08204")
                    //{
                    //    if (string.IsNullOrEmpty(cboChuyenKhoa.Text))
                    //    {
                    //        MessageBox.Show("Bạn chưa nhập chuyên khoa");
                    //        cboChuyenKhoa.Focus();
                    //        return false;
                    //    }
                    //}
                    if (string.IsNullOrEmpty(lupKhoaKham.Text))
                    {
                        MessageBox.Show("Bạn chưa chọn phòng khám");
                        lupKhoaKham.Focus();
                        return false;
                    }

                    if (DungChung.Bien.MaBV == "30340" && string.IsNullOrEmpty(txtSoDT.Text))
                    {
                        MessageBox.Show("Bạn chưa nhập số điện thoại");
                        txtSoDT.Focus();
                        return false;
                    }
                }

                if (DungChung.Bien.MaBV == "27001" && radTuyen.SelectedIndex == 1)
                {
                    DialogResult dr = MessageBox.Show("Bệnh nhân ngoại tỉnh đến trái tuyến, bạn có muốn lưu?",
                        "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dr == DialogResult.No)
                    {
                        return false;
                    }
                }
            }
            else
            {
                if (cboDTuong.Text == "KSK")
                {
                    if (string.IsNullOrEmpty(lupHinhThucKham.Text))
                    {
                        MessageBox.Show("Bạn chưa chọn hình thức KSK");
                        lupHinhThucKham.Focus();
                        return false;
                    }
                }
                else
                {
                    if (lupKhoaKham.EditValue == null || string.IsNullOrEmpty(lupKhoaKham.Text))
                    {
                        MessageBox.Show("Bạn chưa chọn phòng khám");
                        lupKhoaKham.Focus();
                        return false;
                    }

                    if (cboDTuong.Text == "Dịch vụ")
                    {
                        if (DungChung.Bien.MaBV == "30340" && string.IsNullOrEmpty(txtSoDT.Text))
                        {
                            MessageBox.Show("Bạn chưa nhập số điện thoại");
                            txtSoDT.Focus();
                            return false;
                        }

                        HTHONG hthong = _dataContext.HTHONGs.FirstOrDefault(o => o.MaBV == DungChung.Bien.MaBV);
                        if ((DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && hthong != null && hthong.IsTV == true)
                        {
                            if (string.IsNullOrEmpty(txtDT12345.Text))
                            {
                                MessageBox.Show("Bạn chưa nhập số điện thoại");
                                txtDT12345.Focus();
                                return false;
                            }
                        }
                    }
                }
            }
            //chưa kiểm tra hạn thẻ BH
            if (string.IsNullOrEmpty(txtNhapTBN.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên bệnh nhân");
                txtNhapTBN.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtNamSinh.Text))
            {
                MessageBox.Show("Bạn chưa nhập ngày sinh");
                txtNamSinh.Focus();
                return false;
            }

            //if (string.IsNullOrEmpty(lupDanToc.Text)) {
            //    MessageBox.Show("Bạn chưa chọn dân tộc");
            //    lupDanToc.Focus();
            //    return false;
            //}
            if (Bien.MaBV == "30372")
            {
                if (string.IsNullOrEmpty(txtDiaChi.Text))
                {
                    MessageBox.Show("Bạn chưa nhập địa chỉ");
                    txtDiaChi.Focus();
                    return false;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtDiaChi1.Text))
                {
                    MessageBox.Show("Bạn chưa nhập địa chỉ");
                    txtDiaChi.Focus();
                    return false;
                }
            }
            if (radNamNu.SelectedIndex < 0)
            {
                MessageBox.Show("Bạn chưa chọn giới tính");
                radNamNu.Focus();
                return false;
            }

            if (TTLuu == 1)
            {
                if (lupKhoaKham.EditValue != null)
                {
                    _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    DateTime dt = dtNgayN.DateTime.Date;
                    int mak = Convert.ToInt32(lupKhoaKham.EditValue);
                    int STT = 0;
                    if (!string.IsNullOrEmpty(txtSoTT.Text))
                        STT = Convert.ToInt32(txtSoTT.Text);
                    var kt = (from MaxSTT in _dataContext.BenhNhans
                              .Where(p => p.NNhap.Value.Day == dt.Day)
                              .Where(p => p.NNhap.Value.Month == dt.Month)
                              .Where(p => p.NNhap.Value.Year == dt.Year)
                              .Where(p => p.MaKP == mak)
                              .Where(p => p.SoTT == STT)
                              select new { MaxSTT.SoTT })
                              .OrderByDescending(p => p.SoTT).ToList();
                    if (kt.Count > 0)
                        lupKhoaKham_EditValueChanged(null, null);
                }
            }
            if (string.IsNullOrEmpty(txtNgaySinh.Text.Trim()) && !string.IsNullOrEmpty(txtThangSinh.Text.Trim()))
            {
                MessageBox.Show("Bạn chưa nhập ngày sinh");
                txtNgaySinh.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtThangSinh.Text.Trim()) && !string.IsNullOrEmpty(txtNgaySinh.Text.Trim()))
            {
                MessageBox.Show("Bạn chưa nhập tháng sinh");
                txtThangSinh.Focus();
                return false;
            }
            if (DungChung.Ham.CalculateAgeByDate(txtNgaySinh.Text, txtThangSinh.Text, txtNamSinh.Text) < 0)
            {
                MessageBox.Show("Ngày sinh không được lớn hơn ngày hiện tại");
                return false;
            }
            if (TTLuu == 2)
            {
                QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var ktkb = _data.BNKBs.Where(p => p.MaBNhan == _mabn).OrderBy(p => p.NgayKham).ToList();
                if (ktkb.Count > 0)
                {
                    if ((dtNgayN.DateTime - ktkb.First().NgayKham.Value).Days > 0)
                    {
                        MessageBox.Show("Bệnh nhân đã được khám, Ngày nhập phải <= " +
                            ktkb.First().NgayKham.Value.ToShortDateString());
                        dtNgayN.Focus();
                        return false;
                    }
                }
                //if (DungChung.Bien.MaBV == "01830")
                //{
                //    if (!DungChung.Ham.KTraCBNhap(_data, _mabn, DungChung.Bien.MaCB))
                //        return false;
                //}
            }

            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "24012")
            {
                if (lupMaTinh.EditValue == null || lupMaTinh.EditValue == "" || lupMaTinh.Text == "")
                {
                    MessageBox.Show("Tỉnh không được để trống!");
                    return false;
                }
                if (lupMaHuyen.EditValue == null || lupMaHuyen.EditValue == "" || lupMaHuyen.Text == "")
                {
                    MessageBox.Show("Huyện không được để trống!");
                    return false;
                }
                if (lupMaXa.EditValue == null || lupMaXa.EditValue == "" || lupMaXa.Text == "")
                {
                    MessageBox.Show("Xã không được để trống!");
                    return false;
                }
                if (lupTinhKhaiSinh.EditValue == null || lupTinhKhaiSinh.EditValue == "" || lupTinhKhaiSinh.Text == "")
                {
                    MessageBox.Show("Tỉnh khai sinh không được để trống!");
                    return false;
                }
                if ((lupMaHuyenKhaiSinh.EditValue == null || lupMaHuyenKhaiSinh.EditValue == "" || lupMaHuyenKhaiSinh.Text == "") && (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "24012"))
                {
                    MessageBox.Show("Huyện khai sinh không được để trống!");
                    return false;
                }
                if ((lupXaKhaiSinh.EditValue == null || lupXaKhaiSinh.EditValue == "" || lupXaKhaiSinh.Text == "") && (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "24012"))
                {
                    MessageBox.Show("Xã khai sinh không được để trống!");
                    return false;
                }
                if (dtNgayCap.EditValue != null && dtNgayCap.DateTime.Year <= 1800)
                {
                    MessageBox.Show("Năm cấp CMT không được nhỏ hơn 1800");
                    return false;
                }
                if (DungChung.Ham.CalculateAgeByDate(txtNgaySinh.Text, txtThangSinh.Text, txtNamSinh.Text) < 1)
                {
                    if (lupQuanHe.EditValue == "" || lupQuanHe.EditValue == null || string.IsNullOrWhiteSpace(lupQuanHe.Text) || lupQuanHe.EditValue.ToString().Trim() == "0")
                    {
                        MessageBox.Show("Trẻ em dưới 1 tuổi chưa nhập thông tin quan hệ nhân thân");
                        return false;
                    }
                    if (string.IsNullOrWhiteSpace(txtNThan.Text))
                    {
                        MessageBox.Show("Trẻ em dưới 1 tuổi chưa nhập thông tin người thân");
                        return false;
                    }
                }
            }
            return true;
        }

        #endregion Kiemtra trước khi lưu

        private int _mabnhan = 0;

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (KT())
            {
                _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                //kiểm tra bn vào viện trong 24h

                DateTime _dtNgayN = Convert.ToDateTime(dtNgayN.DateTime.Day.ToString() + "/" + dtNgayN.DateTime.Month.ToString() + "/" + dtNgayN.DateTime.Year.ToString() + " " + dtNgayN.DateTime.Hour.ToString() + ":" + dtNgayN.DateTime.Minute.ToString() + ":" + dtNgayN.DateTime.Second.ToString() + "." + dtNgayN.DateTime.Millisecond.ToString());
                _mabnhan = 0;
                if (lupNoiNgoaiTinh.EditValue != null && lupNoiNgoaiTinh.EditValue.ToString() != "")
                    _noitinh = Convert.ToInt32(lupNoiNgoaiTinh.EditValue);
                DialogResult _result = DialogResult.Yes;
                string bntrongngay = "";
                switch (TTLuu)
                {
                    #region thêm mới

                    case 1:

                        if (ktraHSSK())
                        {
                            if (txtNhapTBN.Text.Length > 0 && txtNhapTBN.Text.Length <= 50)
                            {
                                if (_ketqua.Length == 15 || _ketqua.Length == 16 || _ketqua.Length == 0)
                                {
                                    if (!kiemtralichsuKCB(idperson, 1))
                                        return;
                                    bntrongngay = DungChung.Ham.KTTheBHYT(_dataP, _ketqua, dtNgayN.DateTime);
                                    if (!string.IsNullOrEmpty(bntrongngay))
                                    {
                                        _result = MessageBox.Show("Bệnh nhân đã nhập|Thanh toán trong ngày! tại " + bntrongngay + "\nBạn có muốn nhập tiếp không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    }
                                    else
                                    {
                                        bntrongngay = DungChung.Ham.KTTheBHYT(_dataP, _ketqua, dtNgayN.DateTime, 1);
                                        if (!string.IsNullOrEmpty(bntrongngay))
                                        {
                                            _result = MessageBox.Show("Bệnh nhân đã nhập trong tuần! tại " + bntrongngay + "\nBạn có muốn nhập tiếp không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                        }
                                    }
                                    if (cboDTuong.Text != "BHYT")
                                        _ketqua = "";
                                    if (_result == DialogResult.Yes)
                                    {
                                        if (idperson <= 0)
                                        {
                                            if (!chkNoThe.Checked)
                                            {
                                                Person _person = new Person();
                                                _person.SThe = _ketqua.ToUpper();
                                                _person.TenBNhan = frmHSBN.ToFirstUpper(txtNhapTBN.Text).Trim();
                                                if (!string.IsNullOrEmpty(txtNgaySinh.Text))
                                                    _person.NgaySinh = txtNgaySinh.Text.Trim();
                                                if (!string.IsNullOrEmpty(txtThangSinh.Text))
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
                                                if (Bien.MaBV == "30372")
                                                {
                                                    _person.DChi = txtDiaChi.Text.Trim();
                                                }
                                                else
                                                {
                                                    _person.DChi = txtDiaChi1.Text.Trim();
                                                }
                                                if (!string.IsNullOrWhiteSpace(_ketqua))
                                                {
                                                    if (dtHanBHTu.EditValue != null)
                                                        _person.HanBHTu = dtHanBHTu.DateTime.Date;
                                                    else
                                                        _person.HanBHTu = null;
                                                    if (dtHanBHden.EditValue != null)
                                                        _person.HanBHDen = dtHanBHden.DateTime.Date;
                                                    else
                                                        _person.HanBHDen = null;
                                                    if (dtDu5nam.EditValue != null)
                                                        _person.NgayHM = dtDu5nam.DateTime.Date;
                                                    else
                                                        _person.NgayHM = null;
                                                    string ma_cs = txtMaCS.Text.Trim();
                                                    if (!ma_cs.StartsWith("24") && DungChung.Bien.MaBV == "24272")
                                                    {
                                                        MessageBox.Show($"Bệnh nhân có nơi KCBBĐ là {ma_cs} nên không thể lưu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        resetControl();
                                                        this.Dispose();
                                                        return;
                                                    }
                                                    _person.MaCS = ma_cs;
                                                    _person.KhuVuc = cboKhuVuc.Text;
                                                }
                                                else
                                                {
                                                    _person.HanBHTu = null;
                                                    _person.HanBHDen = null;
                                                    _person.NgayHM = null;

                                                    _person.KhuVuc = null;
                                                    _person.MaCS = null;
                                                }

                                                if (!string.IsNullOrEmpty(filePath))
                                                {
                                                    _person.FileAnh = filePath;
                                                }
                                                _dataContext.People.Add(_person);
                                                _dataContext.SaveChanges();
                                                var updatePersonCode = _dataContext.People.FirstOrDefault(o => o.IDPerson == _person.IDPerson);
                                                if (updatePersonCode != null)
                                                {
                                                    updatePersonCode.PersonCode = DungChung.Bien.MaBV + _person.NSinh.Value.ToString("D4") + _person.GTinh + _person.IDPerson;
                                                    _dataContext.SaveChanges();
                                                    personCode = updatePersonCode.PersonCode;
                                                }
                                                idperson = _person.IDPerson;
                                            }
                                        }
                                        else
                                        {
                                            var _person = _dataContext.People.Single(p => p.IDPerson == idperson);
                                            if (_person != null)
                                            {
                                                if (!string.IsNullOrWhiteSpace(_person.SThe)
                                                    && !string.IsNullOrWhiteSpace(_ketqua) &&
                                                    _person.SThe.ToUpper() != _ketqua.ToUpper() ||
                                                    _person.TenBNhan != txtNhapTBN.Text.Trim())
                                                {
                                                    MessageBox.Show("Bệnh nhân không khớp với thông tin đã có. " +
                                                        "Vui lòng gọi hỗ trợ");
                                                    return;
                                                }
                                                else
                                                {
                                                    _person.SThe = _ketqua.ToUpper();
                                                    _person.TenBNhan = frmHSBN.ToFirstUpper(txtNhapTBN.Text).Trim();
                                                    if (!string.IsNullOrEmpty(txtNgaySinh.Text))
                                                        _person.NgaySinh = txtNgaySinh.Text.Trim();
                                                    if (!string.IsNullOrEmpty(txtThangSinh.Text))
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
                                                    if (Bien.MaBV == "30372")
                                                    {
                                                        _person.DChi = txtDiaChi.Text.Trim();
                                                    }
                                                    else
                                                    {
                                                        _person.DChi = txtDiaChi1.Text.Trim();
                                                    }
                                                    if (!string.IsNullOrWhiteSpace(_ketqua))
                                                    {
                                                        if (dtHanBHTu.EditValue != null)
                                                            _person.HanBHTu = dtHanBHTu.DateTime.Date;
                                                        if (dtHanBHden.EditValue != null)
                                                            _person.HanBHDen = dtHanBHden.DateTime.Date;
                                                        if (dtDu5nam.EditValue != null)
                                                            _person.NgayHM = dtDu5nam.DateTime.Date;
                                                        string ma_cs = txtMaCS.Text.Trim();
                                                        if (!ma_cs.StartsWith("24") && DungChung.Bien.MaBV == "24272")
                                                        {
                                                            MessageBox.Show($"Bệnh nhân có nơi KCBBĐ là {ma_cs} nên không thể lưu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            resetControl();
                                                            this.Dispose();
                                                            return;
                                                        }
                                                        _person.MaCS = ma_cs;
                                                        _person.KhuVuc = cboKhuVuc.Text;
                                                    }
                                                    if (!string.IsNullOrEmpty(filePath))
                                                    {
                                                        _person.FileAnh = filePath;
                                                    }
                                                    _dataContext.SaveChanges();
                                                    var updatePersonCode = _dataContext.People.FirstOrDefault(o => o.IDPerson == _person.IDPerson);
                                                    if (updatePersonCode != null)
                                                    {
                                                        updatePersonCode.PersonCode = DungChung.Bien.MaBV + _person.NSinh.Value.ToString("D4") + _person.GTinh + _person.IDPerson;
                                                        _dataContext.SaveChanges();
                                                        personCode = updatePersonCode.PersonCode;
                                                    }
                                                    idperson = _person.IDPerson;
                                                }    
                                            }
                                        }

                                        BenhNhan BN = new BenhNhan();
                                        TienSuTiemChung TSTC = new TienSuTiemChung();
                                        TTboXung ttbx = new TTboXung();
                                        if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389")
                                        {
                                            var ttbx1 = _dataContext.TTboXungs.Where(p => p.MaBNCovid == txtMaBNCovid.Text && p.MaBNCovid != "" && p.MaBNhan != _mabn);
                                            var ttbx2 = (from ttbx3 in ttbx1
                                                         join bn in _dataContext.BenhNhans on ttbx3.MaBNhan equals bn.MaBNhan
                                                         select new { bn.TenBNhan });
                                            if (ttbx1.Count() > 0)
                                            {
                                                MessageBox.Show("Mã " + txtMaBNCovid.Text + " đã được sử dụng cho bệnh nhân " + ttbx2.First().TenBNhan);
                                                return;
                                            }
                                            if (txtMaBNCovid.Text.Length > 10)
                                            {
                                                var ttbx11 = _dataContext.TTboXungs.Where(p => p.MaBNCovid == txtMaBNCovid.Text.Remove(10) && p.MaBNCovid != "" && p.MaBNhan != _mabn);
                                                var ttbx21 = (from ttbx3 in ttbx11
                                                              join bn in _dataContext.BenhNhans on ttbx3.MaBNhan equals bn.MaBNhan
                                                              select new { bn.TenBNhan });
                                                if (ttbx11.Count() > 0)
                                                {
                                                    MessageBox.Show("Mã " + txtMaBNCovid.Text.Remove(10) + " đã được sử dụng cho bệnh nhân " + ttbx21.First().TenBNhan);
                                                    return;
                                                }
                                                else
                                                {
                                                    ttbx.MaBNCovid = txtMaBNCovid.Text.Remove(10);
                                                }
                                            }
                                            else
                                            {
                                                ttbx.MaBNCovid = txtMaBNCovid.Text;
                                            }
                                        }
                                        bool _ngoaih = false;
                                        _ngoaih = DungChung.Ham.CheckNGioHC(dtNgayN.DateTime);

                                        if (_ngoaih == true)
                                        {
                                            BN.NgoaiGio = 1;
                                        }
                                        else { BN.NgoaiGio = 0; }
                                        if (!string.IsNullOrEmpty(txtNhapTBN.Text))
                                            BN.TenBNhan = frmHSBN.ToFirstUpper(txtNhapTBN.Text).Trim();
                                        string MaDTuong = "";
                                        string STHEs = "";
                                        if (!string.IsNullOrWhiteSpace(txtTimSTHE.Text))
                                        {
                                            STHEs = txtTimSTHE.Text.Replace("-", "");
                                            MaDTuong = _ketqua.Substring(0, 2);
                                        }
                                        if (chk_CCCD.Checked)
                                        {
                                            BN.IsCCCD = true;
                                        }
                                        else
                                            BN.IsCCCD = false;
                                        BN.DTuong = cboDTuong.Text;
                                        if (cboDTuong.EditValue != null)
                                            BN.IDDTBN = Convert.ToByte(cboDTuong.EditValue);
                                        BN.IDPerson = idperson;
                                        BN.PersonCode = personCode;
                                        BN.Status = 0;
                                        BN.TuyenDuoi = DungChung.Bien._tuyenduoi;
                                        BN.ChuyenKhoa = cboChuyenKhoa.Text;
                                        BN.NoiTinh = _noitinh;
                                        BN.SThe = STHEs.ToUpper();
                                        if (lupNguoiNhap.EditValue != null)
                                            BN.MaCB = lupNguoiNhap.EditValue.ToString();

                                        BN.NNhap = _dtNgayN;
                                        if (!string.IsNullOrEmpty(txtMaCS.Text))
                                            BN.MaCS = txtMaCS.Text.ToUpper();
                                        if (radNamNu.SelectedIndex == 0)
                                        {
                                            BN.GTinh = 1;
                                        }
                                        else
                                        {
                                            if (radNamNu.SelectedIndex == 1)
                                                BN.GTinh = 0;
                                        }

                                        BN.CapCuu = cboCapCuu.SelectedIndex; //0 bệnh nhan khám bệnh thường
                                        BN.PLKham = cboPLKham.SelectedIndex;

                                        if (radNoiTru.SelectedIndex == 0)
                                        {
                                            BN.NoiTru = 0;// 0 là bệnh nhân ngoại trú
                                        }
                                        else
                                        {
                                            BN.NoiTru = 1;//1 la bệnh nhân nội trú
                                        }

                                        if (lupKhoaKham.EditValue != null)
                                        {
                                            BN.MaKP = Convert.ToInt32(lupKhoaKham.EditValue);
                                        }
                                        else
                                            BN.MaKP = 0;
                                        int _sott = 1;
                                        if (!string.IsNullOrEmpty(txtSoTT.Text))
                                        {
                                            _sott = int.Parse(txtSoTT.Text);
                                            int _sott_tam = _sott;
                                            for (int i = 1; i < 10; i++)
                                            {
                                                int _soTT_KT = _KT_SoTT(_dataContext, dtNgayN.DateTime.Date,
                                                    lupKhoaKham.EditValue == null ? 0 :
                                                    Convert.ToInt32(lupKhoaKham.EditValue), _sott);
                                                if (_sott_tam < _soTT_KT)
                                                {
                                                    _sott_tam++;
                                                }
                                                else
                                                {
                                                    _sott = _sott_tam;
                                                    break;
                                                }
                                            }
                                            txtSoTT.Text = _sott.ToString();

                                            BN.SoTT = int.Parse(txtSoTT.Text);
                                        }
                                        if (!string.IsNullOrWhiteSpace(STHEs))
                                        {
                                            // hạn bảo hiểm
                                            if (dtHanBHTu.DateTime.Day > 0)
                                            {
                                                BN.HanBHTu = dtHanBHTu.DateTime;
                                            }
                                            if (dtHanBHden.DateTime.Day > 0)
                                            {
                                                BN.HanBHDen = dtHanBHden.DateTime;
                                            }
                                            if (dtDu5nam.DateTime.Day > 0)
                                                BN.NgayHM = dtDu5nam.DateTime.Date;
                                            if (!string.IsNullOrEmpty(cboKhuVuc.Text))
                                                BN.KhuVuc = cboKhuVuc.Text;
                                            else
                                                BN.KhuVuc = "";
                                            if (lupMHuong.EditValue != null && lupMHuong.EditValue.ToString() != "")
                                                BN.MucHuong = Convert.ToInt32(lupMHuong.EditValue.ToString().Trim());
                                            else
                                                BN.MucHuong = 0;
                                            if (chkLuongCS.Checked)
                                                BN.LuongCS = 1;
                                            else
                                                BN.LuongCS = 0;
                                            if (radTuyen.SelectedIndex == 0)
                                            {
                                                BN.Tuyen = 1;// bệnh nhân đúng tuyến
                                            }
                                            else if (radTuyen.SelectedIndex == 2)
                                            {
                                                BN.Tuyen = 1; // bn thong tuyen
                                            }
                                            else
                                            {
                                                if (radTuyen.SelectedIndex == 1)
                                                    BN.Tuyen = 2;//bệnh nhân trái tuyến
                                            }
                                            BN.NoThe = false;
                                        }
                                        else
                                        {
                                            BN.HanBHTu = null;
                                            BN.HanBHDen = null;
                                            BN.NgayHM = null;
                                            BN.KhuVuc = null;
                                            BN.MucHuong = null;
                                            BN.LuongCS = null;
                                            BN.Tuyen = null;
                                        }
                                        if (Bien.MaBV == "30372")
                                        {
                                            if (!string.IsNullOrEmpty(txtDiaChi.Text))
                                            {
                                                BN.DChi = DungChung.Ham.ToFirstUpper(txtDiaChi.Text.Trim());
                                            }
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(txtDiaChi1.Text))
                                            {
                                                BN.DChi = DungChung.Ham.ToFirstUpper(txtDiaChi1.Text.Trim());
                                            }
                                        }
                                        if (cboDTuong.Text == "BHYT" || cboDTuong.Text == "Dịch vụ")
                                        {
                                            if (!string.IsNullOrEmpty(txtTrieuChung.Text))
                                                BN.TChung = txtTrieuChung.Text;
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(lupHinhThucKham.Text))
                                                BN.TChung = lupHinhThucKham.Text;
                                        }
                                        if (lupMaBVgt.EditValue != null && lupMaBVgt.EditValue.ToString() != "")
                                            BN.MaBV = lupMaBVgt.EditValue.ToString();

                                        if (lupMaICDkb.EditValue != null && !string.IsNullOrEmpty(txtCDNoiGT.Text))
                                            BN.CDNoiGT = txtCDNoiGT.Text;

                                        if (!string.IsNullOrWhiteSpace(txtTuoi.Text))
                                        {
                                            BN.Tuoi = Convert.ToInt32(txtTuoi.Text);
                                        }
                                        else
                                            BN.Tuoi = null;
                                        if (!string.IsNullOrEmpty(txtNamSinh.Text))
                                        {
                                            BN.NamSinh = txtNamSinh.Text.Trim();
                                        }
                                        if (!string.IsNullOrEmpty(txtNgaySinh.Text))
                                            BN.NgaySinh = txtNgaySinh.Text.Trim();
                                        else
                                            if (DungChung.Bien.MaTinh == "04")
                                            BN.NgaySinh = "01";
                                        else
                                            BN.NgaySinh = "";

                                        if (!string.IsNullOrEmpty(txtThangSinh.Text))
                                            BN.ThangSinh = txtThangSinh.Text.Trim();
                                        else
                                            if (DungChung.Bien.MaTinh == "04")
                                            BN.ThangSinh = "01";
                                        else
                                            BN.ThangSinh = "";

                                        BN.UuTien = chkuutien.Checked;
                                        BN.NoThe = chkNoThe.Checked;
                                        BN.MaDTuong = MaDTuong != null ? MaDTuong.ToUpper() : "";
                                        BN.DTNT = false;
                                        BN.MaKCB = DungChung.Bien.MaBV;
                                        BN.MaCB = DungChung.Bien.MaCB;
                                        BN.SoDK = sodk;
                                        if (!string.IsNullOrEmpty(txtSoKB.Text))
                                            BN.SoKB = Convert.ToInt32(txtSoKB.Text);
                                        if (DungChung.Bien.MaBV == "24009" ||
                                            DungChung.Bien.MaBV == "24000" ||
                                            DungChung.Bien.MaBV == "24208" ||
                                            DungChung.Bien.MaBV == "24209" ||
                                            DungChung.Bien.MaBV == "24210" ||
                                            DungChung.Bien.MaBV == "24211" ||
                                            DungChung.Bien.MaBV == "24212" ||
                                            DungChung.Bien.MaBV == "24213" ||
                                            DungChung.Bien.MaBV == "24214" ||
                                            DungChung.Bien.MaBV == "24215" ||
                                            DungChung.Bien.MaBV == "24216" ||
                                            DungChung.Bien.MaBV == "24217" ||
                                            DungChung.Bien.MaBV == "24218" ||
                                            DungChung.Bien.MaBV == "24219" ||
                                            DungChung.Bien.MaBV == "24220" ||
                                            DungChung.Bien.MaBV == "24221" ||
                                            DungChung.Bien.MaBV == "24223" ||
                                            DungChung.Bien.MaBV == "24224" ||
                                            DungChung.Bien.MaBV == "24225" ||
                                            DungChung.Bien.MaBV == "24226")
                                        {
                                            BN.BNhanLao = ckBNhanLao.Checked;
                                        }
                                        _dataContext.BenhNhans.Add(BN);
                                        if (_dataContext.SaveChanges() >= 0)
                                        {
                                            string ten = frmHSBN.ToFirstUpper(txtNhapTBN.Text.Trim());
                                            _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

                                            _setSoTT(_dataContext, dtNgayN.DateTime, lupKhoaKham.EditValue == null ? 0 : Convert.ToInt32(lupKhoaKham.EditValue), _sott, BN.MaBNhan);
                                            _mabnhan = BN.MaBNhan;
                                            if (DungChung.Bien.MaBV == "27021")
                                            {
                                                // DungChung.Ham.setSoKB(_mabnhan);
                                                setDBSoBKB();
                                            }
                                            if (getdata != null)
                                            {
                                                getdata(_mabnhan);
                                            }
                                            //lưu thông tin bổ xung

                                            var kt = _dataContext.TTboXungs.Where(p => p.MaBNhan == _mabnhan).ToList();

                                            if (kt.Count <= 0)
                                            {
                                                ttbx.MaBNhan = _mabnhan;
                                                if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389")
                                                {
                                                    if (txtMaBNCovid.Text.Length > 10)
                                                    {
                                                        var ttbx11 = _dataContext.TTboXungs.Where(p => p.MaBNCovid == txtMaBNCovid.Text && p.MaBNCovid != "" && p.MaBNhan != _mabn);
                                                        var ttbx21 = (from ttbx3 in ttbx11
                                                                      join bn in _dataContext.BenhNhans on ttbx3.MaBNhan equals bn.MaBNhan
                                                                      select new { bn.TenBNhan });
                                                        if (ttbx11.Count() > 0)
                                                        {
                                                            MessageBox.Show("Mã " + txtMaBNCovid.Text.Remove(10) + " đã được sử dụng cho bệnh nhân " + ttbx21.First().TenBNhan);
                                                            return;
                                                        }
                                                        else
                                                        {
                                                            ttbx.MaBNCovid = txtMaBNCovid.Text.Remove(10);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ttbx.MaBNCovid = txtMaBNCovid.Text;
                                                    }
                                                }
                                                int i = 1;
                                                ttbx.HTThanhToan = Convert.ToInt32(ckcchuyenkhoan.Checked);
                                                if (!string.IsNullOrEmpty(txtHoTenBo.Text) || !string.IsNullOrEmpty(txtHoTenMe.Text))
                                                {
                                                    string Nthan = txtHoTenBo.Text + ";" + txtHoTenMe.Text;
                                                    ttbx.HoTenBoMe = Nthan;
                                                    i = 1;
                                                }
                                                if (lupCVu.EditValue != null && lupCVu.EditValue.ToString() != null)
                                                {
                                                    ttbx.ID_CV = Convert.ToSByte(lupCVu.EditValue);
                                                    i = 1;
                                                }
                                                if (!string.IsNullOrEmpty(txtDiaChiNguoiThan.Text))
                                                {
                                                    ttbx.DCNguoiThan = txtDiaChiNguoiThan.Text;
                                                    i = 1;
                                                }
                                                if (lup_CCap.EditValue != null && lup_CCap.EditValue.ToString() != null)
                                                {
                                                    ttbx.ID_CB = Convert.ToSByte(lup_CCap.EditValue);
                                                    i = 1;
                                                }
                                                if (lupMaTinh.EditValue != null && lupMaTinh.EditValue.ToString() != null)
                                                {
                                                    ttbx.MaTinh = lupMaTinh.EditValue.ToString();
                                                    i = 1;
                                                }
                                                if (!string.IsNullOrEmpty(txtSoto.Text))
                                                {
                                                    ttbx.SoTo = txtSoto.Text;
                                                    i = 1;
                                                }
                                                if (lupMaHuyen.EditValue != null && lupMaHuyen.EditValue.ToString() != null)
                                                {
                                                    ttbx.MaHuyen = lupMaHuyen.EditValue.ToString();
                                                    i = 1;
                                                }
                                                if (lupMaXa.EditValue != null && lupMaXa.EditValue.ToString() != null)
                                                {
                                                    ttbx.MaXa = lupMaXa.EditValue.ToString();
                                                    i = 1;
                                                }
                                                if (lupMaHuyenKhaiSinh.EditValue != null && lupMaHuyenKhaiSinh.EditValue.ToString() != null && (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "24012"))
                                                {
                                                    ttbx.MaHuyenKhaiSinh = lupMaHuyenKhaiSinh.EditValue.ToString();
                                                    i = 1;
                                                }
                                                if (lupXaKhaiSinh.EditValue != null && lupXaKhaiSinh.EditValue.ToString() != null && (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "24012"))
                                                {
                                                    ttbx.MaXaKhaiSinh = lupXaKhaiSinh.EditValue.ToString();
                                                    i = 1;
                                                }
                                                if (lupDantoc.EditValue != null && lupDantoc.EditValue.ToString() != null)
                                                {
                                                    ttbx.MaDT = lupDantoc.EditValue.ToString();
                                                    i = 1;
                                                }
                                                if (!string.IsNullOrEmpty(txtSoDT.Text))
                                                {
                                                    ttbx.DThoai = txtSoDT.Text.Trim();
                                                    i = 1;
                                                }

                                                if (!string.IsNullOrEmpty(txtDT12345.Text))
                                                {
                                                    ttbx.DThoai = txtDT12345.Text.Trim();
                                                }
                                                if (!string.IsNullOrEmpty(lupNgheNghiep.Text))
                                                {
                                                    ttbx.MaNN = lupNgheNghiep.EditValue.ToString();
                                                    i = 1;
                                                }
                                                if (!string.IsNullOrEmpty(txtNoiLV.Text))
                                                {
                                                    ttbx.NoiLV = txtNoiLV.Text.Trim();
                                                    i = 1;
                                                }
                                                if (!string.IsNullOrEmpty(txtNThan.Text))
                                                {
                                                    ttbx.NThan = txtNThan.Text.Trim();
                                                    i = 1;
                                                }
                                                if (lupQuanHe.EditValue != null)
                                                {
                                                    ttbx.QuanHeNhanThan = lupQuanHe.EditValue.ToString().Trim();
                                                    i = 1;
                                                }
                                                if (!string.IsNullOrEmpty(txtDTNThan.Text))
                                                {
                                                    ttbx.DThoaiNT = txtDTNThan.Text.Trim();
                                                    i = 1;
                                                }
                                                if (!string.IsNullOrEmpty(txtSoKSinh_CMT.Text))
                                                {
                                                    ttbx.SoKSinh = txtSoKSinh_CMT.Text.Trim();
                                                    i = 1;
                                                }
                                                if (!string.IsNullOrEmpty(txt_NoiCapCMT.Text))
                                                {
                                                    ttbx.NoiCapCMT = txt_NoiCapCMT.Text.Trim();
                                                    i = 1;
                                                }
                                                if (!string.IsNullOrEmpty(cboQuocTich.Text))
                                                {
                                                    ttbx.NgoaiKieu = cboQuocTich.Text.Trim();
                                                    i = 1;
                                                }
                                                if (!string.IsNullOrEmpty(dtNgayCap.Text))
                                                {
                                                    ttbx.NgayCapCMT = dtNgayCap.DateTime;
                                                    i = 1;
                                                }
                                                if (!string.IsNullOrEmpty(txtThonPho.Text))
                                                {
                                                    ttbx.ThonPho = txtThonPho.Text;
                                                    i = 1;
                                                }
                                                if (!string.IsNullOrEmpty(filePath))
                                                {
                                                    ttbx.FileAnh = filePath;
                                                    i = 1;
                                                }
                                                if (!string.IsNullOrEmpty(txtsohsbnmantinh.Text))
                                                {
                                                    ttbx.So_eTBM = txtsohsbnmantinh.Text;
                                                    i = 1;
                                                }
                                                if (!string.IsNullOrEmpty(lupMaBVgt.Text) && !string.IsNullOrEmpty(lupMaICDkb.Text))
                                                {
                                                    ttbx.MaICD = lupMaICDkb.Text;
                                                    i = 1;
                                                }
                                                if (lupTinhKhaiSinh.EditValue != null && lupTinhKhaiSinh.EditValue.ToString() != null)
                                                {
                                                    ttbx.MaTinhKhaiSinh = lupTinhKhaiSinh.EditValue.ToString().Trim();
                                                    i = 1;
                                                }
                                                if (lupMaHuyenKhaiSinh.EditValue != null && lupMaHuyenKhaiSinh.EditValue.ToString() != null && (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "24012"))
                                                {
                                                    ttbx.MaHuyenKhaiSinh = lupMaHuyenKhaiSinh.EditValue.ToString().Trim();
                                                    i = 1;
                                                }
                                                if (lupXaKhaiSinh.EditValue != null && lupXaKhaiSinh.EditValue.ToString() != null && (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "24012"))
                                                {
                                                    ttbx.MaXaKhaiSinh = lupXaKhaiSinh.EditValue.ToString().Trim();
                                                    i = 1;
                                                }
                                                if (!string.IsNullOrEmpty(txtDChiKSinh.Text))
                                                {
                                                    ttbx.DchiKhaiSinh = txtDChiKSinh.Text.Trim();
                                                    i = 1;
                                                }
                                                if (!string.IsNullOrEmpty(txtHKKT.Text))
                                                {
                                                    ttbx.HKTT = txtHKKT.Text.Trim();
                                                    i = 1;
                                                }

                                                if (i == 1)
                                                {
                                                    _dataContext.TTboXungs.Add(ttbx);
                                                    _dataContext.SaveChanges();
                                                }
                                            }

                                            // thêm vào bảng khám bệnh nếu BN vào nội trú
                                            int noingoai = CheckNoiNgoaiTru(_mabn);
                                            if ((noingoai == 0 && (DungChung.Bien.PPXuat_BHXH[0] == 1)) || (noingoai == 1 && (DungChung.Bien.PPXuat_BHXH[1] == 1)))
                                                ChucNang.frm_CheckIn.export_CheckIn(_dataContext, _mabnhan, false, "", "", 2);
                                        }
                                        if (chkLuonHienThi.Checked)
                                        {
                                            btnIn_Click(sender, e);
                                            frmHSBNNhapMoi_Load(sender, e);
                                            TTLuu = 1;
                                            resetControl();
                                            resetTTBX();
                                        }
                                        else
                                        {
                                            btnIn_Click(sender, e);

                                            frmHSBN frm = new frmHSBN();
                                            frm.Show();
                                            this.Dispose();
                                        }

                                        #region nhập chỉ số cơ thể

                                        if (DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "24009")
                                        {
                                            if (_mabnhan > 0)
                                            {
                                                frm_TTKhamBenh frm = new frm_TTKhamBenh(_mabnhan);
                                                frm.ShowDialog();
                                            }
                                            else
                                                MessageBox.Show("Bạn chưa lưu thông tin bệnh nhân");
                                        }

                                        #endregion nhập chỉ số cơ thể

                                        #region nhập tiền sử tiêm vắc-xin

                                        if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389")
                                        {
                                            if (_mabnhan > 0)
                                            {
                                                DialogResult dlr = MessageBox.Show("Bạn có muốn nhập Tiền sử tiêm Vắc-xin cho bệnh nhân không?", "Thông báo!", MessageBoxButtons.YesNo);
                                                if (dlr == DialogResult.Yes)
                                                {
                                                    frm_TienSuVacXin frm = new frm_TienSuVacXin(_mabnhan);
                                                    frm.ShowDialog();
                                                }
                                            }
                                        }

                                        #endregion nhập tiền sử tiêm vắc-xin
                                    }
                                    //btnCDCLS.Enabled = true;
                                }///kt ktsthe
                                else
                                {
                                    int a = 0;
                                    if (_ketqua.Length > 0)
                                        a = 15 - _ketqua.Length;
                                    MessageBox.Show("Số thẻ BH phải đủ 15 ký tự,bạn nhập thiếu: " + a + " ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Tên BN không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtNhapTBN.Focus();
                            }
                        }
                        break;

                    #endregion thêm mới

                    #region sửa

                    case 2:

                        bool TheMoi = ckcBoXungThe.Checked;
                        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

                        if (_ketqua.Length == 15 && !kiemtralichsuKCB(idperson, 2))
                            return;

                        BenhNhan BN1 = _data.BenhNhans.Single(p => p.MaBNhan == _mabn);
                        int _makpcu = BN1.MaKP ?? 0;
                        bool _suaq = true;
                        bool luulog = false; ;

                        #region lưu log khi sửa -his 2135

                        if (DungChung.Bien.MaBV == "01830")
                        {
                            string dtuongcu = BN1.DTuong; // đối tượng bệnh nhân trước khi sửa
                            if (dtuongcu == "BHYT" || cboDTuong.Text == "BHYT")
                            {
                                if (_suaq)
                                {
                                    string tb = "Nhập lý do sửa thông tin bệnh nhân:" + _mabn.ToString();
                                    QLBV.frm_GetLyDoLog frm = new frm_GetLyDoLog(tb);
                                    frm.ok = new frm_GetLyDoLog._getdata(_getLyDoLog);
                                    frm.ShowDialog();
                                    if (!string.IsNullOrEmpty(_lydoLodg))
                                    {
                                        _suaq = true;
                                        luulog = true;
                                    }
                                    else
                                        _suaq = false;
                                }
                            }
                        }

                        #endregion lưu log khi sửa -his 2135

                        #region sua

                        if (_suaq)
                        {
                            #region cập nhật bn nợ thẻ

                            string _dtuongSua = _data.BenhNhans.Single(p => p.MaBNhan == _mabn).DTuong;

                            #endregion cập nhật bn nợ thẻ

                            if (TheMoi)
                            {
                                DialogResult ReSult = MessageBox.Show("Bạn muốn bổ sung thẻ mới ?", "Hỏi lưu!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (ReSult == DialogResult.Yes)
                                {
                                    //var a = _lTTTheCu.First();
                                    int GTinh = radNamNu.SelectedIndex == 0 ? 1 : 0;
                                    string NGS = txtNgaySinh.Text + "/" + txtThangSinh.Text + "/" + txtNamSinh.Text;
                                    string Sthe = _ketqua;
                                    string ttbxold = Sthe.ToUpper() + ";" + txtNhapTBN.Text + ";" + (Bien.MaBV == "30372" ? txtDiaChi.Text : txtDiaChi1.Text) + ";" + GTinh.ToString() + ";" + NGS.ToString() + ";" + dtHanBHTu.DateTime.ToShortDateString() + ";" + dtHanBHden.DateTime.ToShortDateString() + ";" + txtMaCS.Text + ";" + DateTime.Now.ToShortDateString() + ";";
                                    TTboXung ttbx = _data.TTboXungs.Single(p => p.MaBNhan == _mabn);
                                    ttbx.TTTheBHYTold = ttbxold;
                                    _data.SaveChanges();
                                    frmHSBNNhapMoi_Load(sender, e);
                                }
                            }
                            else
                            {
                                _mabnhan = _mabn;
                                if (txtNhapTBN.Text.Length > 0 && txtNhapTBN.Text.Length <= 50 && !TheMoi)
                                {
                                    if (_ketqua.Length == 15 || _ketqua.Length == 0)
                                    {
                                        if (cboDTuong.Text != "BHYT")
                                            _ketqua = "";
                                        bool _ngoaih = false;
                                        _ngoaih = DungChung.Ham.CheckNGioHC(dtNgayN.DateTime);

                                        #region cập nhật bảng person

                                        var _person = _dataContext.People.FirstOrDefault(p => p.IDPerson == idperson);
                                        if (_person != null && !(_dtuongSua == "BHYT" && cboDTuong.Text != "BHYT"))
                                        {
                                            if (!string.IsNullOrWhiteSpace(_person.SThe) && !string.IsNullOrWhiteSpace(_ketqua) && ((_person.SThe.ToUpper() != _ketqua.ToUpper() || _person.TenBNhan != txtNhapTBN.Text.Trim())))
                                            {
                                                MessageBox.Show("Bệnh nhân không khớp với thông tin đã có. Vui lòng gọi hỗ trợ");
                                                return;
                                            }
                                            _person.SThe = _ketqua.ToUpper();
                                            _person.TenBNhan = frmHSBN.ToFirstUpper(txtNhapTBN.Text).Trim();
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
                                            if (Bien.MaBV == "30372")
                                            {
                                                _person.DChi = txtDiaChi.Text.Trim();
                                            }
                                            else
                                            {
                                                _person.DChi = txtDiaChi1.Text.Trim();
                                            }
                                            if (!string.IsNullOrWhiteSpace(_ketqua))
                                            {
                                                if (dtHanBHTu.EditValue != null)
                                                    _person.HanBHTu = dtHanBHTu.DateTime.Date;
                                                else
                                                    _person.HanBHTu = null;
                                                if (dtHanBHden.EditValue != null)
                                                    _person.HanBHDen = dtHanBHden.DateTime.Date;
                                                else
                                                    _person.HanBHDen = null;
                                                if (dtDu5nam.EditValue != null)
                                                    _person.NgayHM = dtDu5nam.DateTime.Date;
                                                else
                                                    _person.NgayHM = null;
                                                _person.MaCS = txtMaCS.Text.Trim();
                                                _person.KhuVuc = cboKhuVuc.Text;
                                            }
                                            else
                                            {
                                                _person.HanBHTu = null;
                                                _person.HanBHDen = null;
                                                _person.NgayHM = null;
                                                _person.KhuVuc = null;
                                                _person.MaCS = null;
                                            }
                                            if (!string.IsNullOrEmpty(filePath))
                                            {
                                                _person.FileAnh = filePath;
                                            }
                                            _person.PersonCode = DungChung.Bien.MaBV + _person.NSinh.Value.ToString("D4") + _person.GTinh + _person.IDPerson;
                                            _dataContext.SaveChanges();
                                            personCode = _person.PersonCode;
                                        }
                                        else if (!chkNoThe.Checked && cboDTuong.Text == "BHYT")
                                        {
                                            Person _per = new Person();
                                            _per.SThe = _ketqua.ToUpper();
                                            _per.TenBNhan = txtNhapTBN.Text.Trim();
                                            if (!string.IsNullOrEmpty(txtNgaySinh.Text))
                                                _per.NgaySinh = txtNgaySinh.Text.Trim();
                                            if (!string.IsNullOrEmpty(txtThangSinh.Text))
                                                _per.ThangSinh = txtThangSinh.Text.Trim();
                                            if (!string.IsNullOrEmpty(txtNamSinh.Text))
                                            {
                                                _per.NSinh = Convert.ToInt32(txtNamSinh.Text);
                                            }
                                            if (radNamNu.SelectedIndex == 0)
                                            {
                                                _per.GTinh = 1;
                                            }
                                            else
                                            {
                                                if (radNamNu.SelectedIndex == 1)
                                                    _per.GTinh = 0;
                                            }
                                            if (Bien.MaBV == "30372")
                                            {
                                                _per.DChi = txtDiaChi.Text.Trim();
                                            }
                                            else
                                            {
                                                _per.DChi = txtDiaChi1.Text.Trim();
                                            }
                                            if (!string.IsNullOrWhiteSpace(_ketqua))
                                            {
                                                if (dtHanBHTu.EditValue != null)
                                                    _per.HanBHTu = dtHanBHTu.DateTime.Date;
                                                else
                                                    _per.HanBHTu = null;
                                                if (dtHanBHden.EditValue != null)
                                                    _per.HanBHDen = dtHanBHden.DateTime.Date;
                                                else
                                                    _per.HanBHDen = null;
                                                if (dtDu5nam.EditValue != null)
                                                    _per.NgayHM = dtDu5nam.DateTime.Date;
                                                else
                                                    _per.NgayHM = null;
                                                _per.MaCS = txtMaCS.Text.Trim();
                                                _per.KhuVuc = cboKhuVuc.Text;
                                            }
                                            else
                                            {
                                                _per.HanBHTu = null;
                                                _per.HanBHDen = null;
                                                _per.NgayHM = null;
                                                _per.KhuVuc = null;
                                                _per.MaCS = null;
                                            }

                                            if (!string.IsNullOrEmpty(filePath))
                                            {
                                                _per.FileAnh = filePath;
                                            }
                                            _dataContext.People.Add(_per);
                                            _dataContext.SaveChanges();
                                            idperson = _per.IDPerson;
                                            var updatePersonCode = _dataContext.People.FirstOrDefault(o => o.IDPerson == _per.IDPerson);
                                            if (updatePersonCode != null)
                                            {
                                                updatePersonCode.PersonCode = DungChung.Bien.MaBV + _per.NSinh.Value.ToString("D4") + _per.GTinh + _per.IDPerson;
                                                _dataContext.SaveChanges();
                                                personCode = updatePersonCode.PersonCode;
                                            }
                                        }
                                        BN1.IDPerson = idperson;
                                        BN1.PersonCode = personCode;

                                        #endregion cập nhật bảng person

                                        if (_ngoaih == true)
                                        {
                                            BN1.NgoaiGio = 1;
                                        }

                                        if (!string.IsNullOrEmpty(txtNhapTBN.Text))
                                            BN1.TenBNhan = frmHSBN.ToFirstUpper(txtNhapTBN.Text).Trim();
                                        string STHEs = "", MaDTuong = "";
                                        if (!string.IsNullOrEmpty(txtTimSTHE.Text))
                                        {
                                            STHEs = _ketqua.ToUpper();
                                            if (_ketqua.Length > 2)
                                                MaDTuong = _ketqua.Substring(0, 2);
                                        }
                                        BN1.DTuong = cboDTuong.Text;
                                        if (cboDTuong.EditValue != null)
                                            BN1.IDDTBN = Convert.ToByte(cboDTuong.EditValue);

                                        BN1.NoiTinh = _noitinh;
                                        BN1.ChuyenKhoa = cboChuyenKhoa.Text;
                                        BN1.SThe = STHEs.ToUpper();
                                        BN1.NNhap = _dtNgayN;
                                        if (lupNguoiNhap.EditValue != null)
                                            BN1.MaCB = lupNguoiNhap.EditValue.ToString();
                                        if (radNamNu.SelectedIndex == 0)
                                        {
                                            BN1.GTinh = 1;
                                        }
                                        else
                                        {
                                            if (radNamNu.SelectedIndex == 1)
                                                BN1.GTinh = 0;
                                        }

                                        BN1.CapCuu = cboCapCuu.SelectedIndex; //0 bệnh nhan khám bệnh thường
                                        BN1.PLKham = cboPLKham.SelectedIndex;
                                        if (chk_CCCD.Checked)
                                        {
                                            BN1.IsCCCD = true;
                                        }
                                        else
                                            BN1.IsCCCD = false;
                                        if (radNoiTru.SelectedIndex == 0)
                                        {
                                            BN1.NoiTru = 0;// 0 là bệnh nhân ngoại trú
                                        }
                                        else
                                        {
                                            BN1.NoiTru = 1;//1 la bệnh nhân nội trú
                                        }
                                        if (!string.IsNullOrEmpty(txtSoTT.Text))
                                        {
                                            if (BN1.MaKP != (lupKhoaKham.EditValue == null ? 0 : Convert.ToInt32(lupKhoaKham.EditValue)))
                                                BN1.SoTT = int.Parse(txtSoTT.Text);
                                        }
                                        if (lupKhoaKham.EditValue != null)
                                        {
                                            BN1.MaKP = Convert.ToInt32(lupKhoaKham.EditValue);
                                        }

                                        if (!string.IsNullOrWhiteSpace(STHEs))
                                        {
                                            // hạn bảo hiểm
                                            if (dtHanBHTu.DateTime.Day > 0)
                                            {
                                                BN1.HanBHTu = dtHanBHTu.DateTime;
                                            }
                                            if (dtHanBHden.DateTime.Day > 0)
                                            {
                                                BN1.HanBHDen = dtHanBHden.DateTime;
                                            }
                                            if (dtDu5nam.DateTime.Day > 0)
                                                BN1.NgayHM = dtDu5nam.DateTime.Date;
                                            if (!string.IsNullOrEmpty(cboKhuVuc.Text))
                                                BN1.KhuVuc = cboKhuVuc.Text;
                                            else
                                                BN1.KhuVuc = "";
                                            if (lupMHuong.EditValue != null && lupMHuong.EditValue.ToString() != "")
                                                BN1.MucHuong = Convert.ToInt32(lupMHuong.EditValue.ToString().Trim());
                                            else
                                                BN1.MucHuong = 0;
                                            if (chkLuongCS.Checked)
                                                BN1.LuongCS = 1;
                                            else
                                                BN1.LuongCS = 0;
                                            if (radTuyen.SelectedIndex == 0)
                                            {
                                                BN1.Tuyen = 1;// bệnh nhân đúng tuyến
                                            }
                                            else if (radTuyen.SelectedIndex == 2)
                                            {
                                                BN1.Tuyen = 1; // bn thong tuyen
                                            }
                                            else
                                            {
                                                if (radTuyen.SelectedIndex == 1)
                                                    BN1.Tuyen = 2;//bệnh nhân trái tuyến
                                            }
                                            BN1.NoThe = false;
                                            if (!string.IsNullOrEmpty(txtMaCS.Text))
                                                BN1.MaCS = txtMaCS.Text.ToUpper();
                                            else
                                                BN1.MaCS = null;
                                        }
                                        else
                                        {
                                            BN1.MaCS = null;
                                            BN1.HanBHTu = null;
                                            BN1.HanBHDen = null;
                                            BN1.NgayHM = null;
                                            BN1.KhuVuc = null;
                                            BN1.MucHuong = null;
                                            BN1.LuongCS = null;
                                            BN1.Tuyen = null;
                                        }
                                        if (Bien.MaBV == "30372")
                                        {
                                            if (!string.IsNullOrEmpty(txtDiaChi.Text))
                                            {
                                                BN1.DChi = txtDiaChi.Text.Trim();
                                            }
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(txtDiaChi1.Text))
                                            {
                                                BN1.DChi = txtDiaChi1.Text.Trim();
                                            }
                                        }
                                        if (cboDTuong.Text == "BHYT" || cboDTuong.Text == "Dịch vụ")
                                        {
                                            if (!string.IsNullOrEmpty(txtTrieuChung.Text))
                                                BN1.TChung = txtTrieuChung.Text;
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(lupHinhThucKham.Text))
                                                BN1.TChung = lupHinhThucKham.Text;
                                        }

                                        if (lupMaBVgt.EditValue != null) //&& lupMaBVgt.EditValue.ToString() != "")
                                            BN1.MaBV = lupMaBVgt.EditValue.ToString();

                                        if (lupMaICDkb.EditValue != null && !string.IsNullOrEmpty(txtCDNoiGT.Text))
                                            BN1.CDNoiGT = txtCDNoiGT.Text;
                                        if (!string.IsNullOrWhiteSpace(txtTuoi.Text))
                                        {
                                            BN1.Tuoi = Convert.ToInt32(txtTuoi.Text);
                                        }
                                        else
                                            BN1.Tuoi = null;
                                        if (!string.IsNullOrEmpty(txtNamSinh.Text))
                                        {
                                            BN1.NamSinh = txtNamSinh.Text;
                                        }

                                        BN1.NgaySinh = txtNgaySinh.Text;
                                        BN1.ThangSinh = txtThangSinh.Text;
                                        BN1.UuTien = chkuutien.Checked;
                                        if (DungChung.Bien.MaBV == "30005")
                                        {
                                            BN1.NoThe = chkNoThe30005.Checked;
                                        }
                                        else
                                            BN1.NoThe = chkNoThe.Checked;
                                        BN1.MaDTuong = MaDTuong != null ? MaDTuong.ToUpper() : "";
                                        if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "24000" || DungChung.Bien.MaBV == "24208" || DungChung.Bien.MaBV == "24209" || DungChung.Bien.MaBV == "24210" || DungChung.Bien.MaBV == "24211" || DungChung.Bien.MaBV == "24212" || DungChung.Bien.MaBV == "24213" || DungChung.Bien.MaBV == "24214" || DungChung.Bien.MaBV == "24215" || DungChung.Bien.MaBV == "24216" || DungChung.Bien.MaBV == "24217" || DungChung.Bien.MaBV == "24218" || DungChung.Bien.MaBV == "24219" || DungChung.Bien.MaBV == "24220" || DungChung.Bien.MaBV == "24221" || DungChung.Bien.MaBV == "24223" || DungChung.Bien.MaBV == "24224" || DungChung.Bien.MaBV == "24225" || DungChung.Bien.MaBV == "24226")
                                        {
                                            BN1.BNhanLao = ckBNhanLao.Checked;
                                        }
                                        _data.SaveChanges();
                                        int _makpmoi = BN1.MaKP ?? 0;
                                        if (_makpcu != _makpmoi)//Thay đổi khoa phòng thì sét lại số tt
                                        {
                                            _setSoTT(_dataContext, dtNgayN.DateTime, lupKhoaKham.EditValue == null ? 0 : Convert.ToInt32(lupKhoaKham.EditValue), BN1.SoTT ?? 0, BN1.MaBNhan);
                                        }

                                        #region cập nhật TTbx

                                        var tt = _data.TTboXungs.Where(p => p.MaBNhan == (_mabn)).ToList();
                                        if (tt.Count > 0)
                                        {
                                            // sửa thông tin bổ xung
                                            TTboXung ttbx = _data.TTboXungs.Single(p => p.MaBNhan == (_mabn));
                                            ttbx.MaBNhan = _mabn;
                                            ttbx.HTThanhToan = Convert.ToInt32(ckcchuyenkhoan.Checked);
                                            string Nthan = txtHoTenBo.Text + ";" + txtHoTenMe.Text;
                                            ttbx.HoTenBoMe = Nthan;
                                            if (lupCVu.EditValue != null && lupCVu.EditValue.ToString() != null)
                                            {
                                                ttbx.ID_CV = Convert.ToSByte(lupCVu.EditValue);
                                            }
                                            if (!string.IsNullOrEmpty(txtDiaChiNguoiThan.Text))
                                            {
                                                ttbx.DCNguoiThan = txtDiaChiNguoiThan.Text;
                                            }
                                            if (lup_CCap.EditValue != null && lup_CCap.EditValue.ToString() != null)
                                            {
                                                ttbx.ID_CB = Convert.ToSByte(lup_CCap.EditValue);
                                            }
                                            if (lupMaTinh.EditValue != null && lupMaTinh.EditValue.ToString() != null)
                                            {
                                                ttbx.MaTinh = lupMaTinh.EditValue.ToString();
                                            }
                                            if (lupMaHuyen.EditValue != null && lupMaHuyen.EditValue.ToString() != null)
                                            {
                                                ttbx.MaHuyen = lupMaHuyen.EditValue.ToString();
                                            }
                                            if (lupMaXa.EditValue != null && lupMaXa.EditValue.ToString() != null)
                                            {
                                                ttbx.MaXa = lupMaXa.EditValue.ToString();
                                            }
                                            if (lupDantoc.EditValue != null && lupDantoc.EditValue.ToString() != null)
                                            {
                                                ttbx.MaDT = lupDantoc.EditValue.ToString();
                                            }
                                            if (!string.IsNullOrEmpty(txtSoto.Text))
                                            {
                                                ttbx.SoTo = txtSoto.Text;
                                            }
                                            if (!string.IsNullOrEmpty(txtSoDT.Text))
                                            {
                                                ttbx.DThoai = txtSoDT.Text.Trim();
                                                txtDT12345.Visible = false;
                                            }
                                            if (!string.IsNullOrEmpty(txtDT12345.Text) && txtDT12345.Visible == true)
                                                ttbx.DThoai = txtDT12345.Text.Trim();
                                            if (!string.IsNullOrEmpty(lupNgheNghiep.Text))
                                                ttbx.MaNN = lupNgheNghiep.EditValue.ToString();
                                            if (!string.IsNullOrEmpty(txtNoiLV.Text))
                                                ttbx.NoiLV = txtNoiLV.Text.Trim();
                                            if (!string.IsNullOrEmpty(txtNThan.Text))
                                                ttbx.NThan = txtNThan.Text.Trim();
                                            if (lupQuanHe.EditValue != null)
                                            {
                                                ttbx.QuanHeNhanThan = lupQuanHe.EditValue.ToString().Trim();
                                            }
                                            else
                                                ttbx.QuanHeNhanThan = "0";
                                            if (!string.IsNullOrEmpty(txtDTNThan.Text))
                                                ttbx.DThoaiNT = txtDTNThan.Text.Trim();
                                            if (!string.IsNullOrEmpty(txtSoKSinh_CMT.Text))
                                                ttbx.SoKSinh = txtSoKSinh_CMT.Text.Trim();
                                            if (!string.IsNullOrEmpty(txt_NoiCapCMT.Text))
                                                ttbx.NoiCapCMT = txt_NoiCapCMT.Text.Trim();
                                            if (!string.IsNullOrEmpty(cboQuocTich.Text))
                                            {
                                                ttbx.NgoaiKieu = cboQuocTich.Text.ToString();
                                            }
                                            if (!string.IsNullOrEmpty(dtNgayCap.Text))
                                            {
                                                ttbx.NgayCapCMT = dtNgayCap.DateTime;
                                            }
                                            if (!string.IsNullOrEmpty(filePath))
                                            {
                                                ttbx.FileAnh = filePath;
                                            }
                                            ttbx.ThonPho = txtThonPho.Text;
                                            if (lupTinhKhaiSinh.EditValue != null && lupTinhKhaiSinh.EditValue.ToString() != null)
                                            {
                                                ttbx.MaTinhKhaiSinh = lupTinhKhaiSinh.EditValue.ToString().Trim();
                                            }
                                            if (lupMaHuyenKhaiSinh.EditValue != null && lupMaHuyenKhaiSinh.EditValue.ToString() != null && (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "24012"))
                                            {
                                                ttbx.MaHuyenKhaiSinh = lupMaHuyenKhaiSinh.EditValue.ToString().Trim();
                                            }
                                            if (lupXaKhaiSinh.EditValue != null && lupXaKhaiSinh.EditValue.ToString() != null && (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "24012"))
                                            {
                                                ttbx.MaXaKhaiSinh = lupXaKhaiSinh.EditValue.ToString().Trim();
                                            }
                                            if (!string.IsNullOrEmpty(txtDChiKSinh.Text))
                                            {
                                                ttbx.DchiKhaiSinh = txtDChiKSinh.Text.Trim();
                                            }
                                            if (!string.IsNullOrEmpty(txtHKKT.Text))
                                            {
                                                ttbx.HKTT = txtHKKT.Text.Trim();
                                            }
                                            if (!string.IsNullOrEmpty(lupMaBVgt.Text) && !string.IsNullOrEmpty(lupMaICDkb.Text))
                                            {
                                                ttbx.MaICD = lupMaICDkb.Text;
                                            }
                                            if (!string.IsNullOrEmpty(txtMaBNCovid.Text))
                                            {
                                                var ttbx1 = _dataContext.TTboXungs.Where(p => p.MaBNCovid == txtMaBNCovid.Text && p.MaBNCovid != "" && p.MaBNhan != _mabn);
                                                var ttbx2 = (from ttbx3 in ttbx1
                                                             join bn in _dataContext.BenhNhans on ttbx3.MaBNhan equals bn.MaBNhan
                                                             select new { bn.TenBNhan });
                                                if (ttbx1.Count() > 0)
                                                {
                                                    MessageBox.Show("Mã " + txtMaBNCovid.Text + " đã được sử dụng cho bệnh nhân " + ttbx2.First().TenBNhan);
                                                    return;
                                                }
                                                if (txtMaBNCovid.Text.Length > 10)
                                                {
                                                    var ttbx11 = _dataContext.TTboXungs.Where(p => p.MaBNCovid == txtMaBNCovid.Text.Remove(10) && p.MaBNCovid != "" && p.MaBNhan != _mabn);
                                                    var ttbx21 = (from ttbx3 in ttbx11
                                                                  join bn in _dataContext.BenhNhans on ttbx3.MaBNhan equals bn.MaBNhan
                                                                  select new { bn.TenBNhan });
                                                    if (ttbx11.Count() > 0)
                                                    {
                                                        MessageBox.Show("Mã " + txtMaBNCovid.Text.Remove(10) + " đã được sử dụng cho bệnh nhân " + ttbx21.First().TenBNhan);
                                                        return;
                                                    }
                                                    else
                                                    {
                                                        ttbx.MaBNCovid = txtMaBNCovid.Text.Remove(10);
                                                    }
                                                }
                                                else
                                                {
                                                    ttbx.MaBNCovid = txtMaBNCovid.Text;
                                                }
                                                _data.SaveChanges();
                                            }
                                            else
                                            {
                                                ttbx.MaBNCovid = null;
                                            }
                                            _data.SaveChanges();
                                        }
                                        else
                                        {
                                            //tạo mới thông tin bổ xung
                                            TTboXung ttbx = new TTboXung();
                                            ttbx.MaBNhan = _mabn;
                                            ttbx.HTThanhToan = Convert.ToInt32(ckcchuyenkhoan.Checked);
                                            if (lupMaTinh.EditValue != null && lupMaTinh.EditValue.ToString() != null)
                                            {
                                                ttbx.MaTinh = lupMaTinh.EditValue.ToString();
                                            }
                                            ttbx.DchiKhaiSinh = txtDChiKSinh.Text;
                                            ttbx.HKTT = txtHKKT.Text;
                                            if (lupTinhKhaiSinh.EditValue != null && lupTinhKhaiSinh.EditValue.ToString() != null)
                                            {
                                                ttbx.MaTinhKhaiSinh = lupTinhKhaiSinh.EditValue.ToString().Trim();
                                            }
                                            if (lupMaHuyenKhaiSinh.EditValue != null && lupMaHuyenKhaiSinh.EditValue.ToString() != null && (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "24012"))
                                            {
                                                ttbx.MaHuyenKhaiSinh = lupMaHuyenKhaiSinh.EditValue.ToString().Trim();
                                            }
                                            if (lupXaKhaiSinh.EditValue != null && lupXaKhaiSinh.EditValue.ToString() != null && (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "24012"))
                                            {
                                                ttbx.MaXaKhaiSinh = lupXaKhaiSinh.EditValue.ToString().Trim();
                                            }
                                            if (lupMaHuyen.EditValue != null && lupMaHuyen.EditValue.ToString() != null)
                                            {
                                                ttbx.MaHuyen = lupMaHuyen.EditValue.ToString();
                                            }
                                            if (lupMaXa.EditValue != null && lupMaXa.EditValue.ToString() != null)
                                            {
                                                ttbx.MaXa = lupMaXa.EditValue.ToString();
                                            }
                                            if (lupDantoc.EditValue != null && lupDantoc.EditValue.ToString() != null)
                                            {
                                                ttbx.MaDT = lupDantoc.EditValue.ToString();
                                            }
                                            if (!string.IsNullOrEmpty(txtSoDT.Text))
                                                ttbx.DThoai = txtSoDT.Text.Trim();
                                            if (!string.IsNullOrEmpty(txtDT12345.Text))
                                                ttbx.DThoai = txtDT12345.Text.Trim();
                                            if (!string.IsNullOrEmpty(lupNgheNghiep.Text))
                                                ttbx.MaNN = lupNgheNghiep.EditValue.ToString();
                                            if (!string.IsNullOrEmpty(txtNoiLV.Text))
                                                ttbx.NoiLV = txtNoiLV.Text.Trim();
                                            if (!string.IsNullOrEmpty(txtNThan.Text))
                                                ttbx.NThan = txtNThan.Text.Trim();
                                            if (lupQuanHe.EditValue != null)
                                                ttbx.QuanHeNhanThan = lupQuanHe.EditValue.ToString().Trim();
                                            if (!string.IsNullOrEmpty(txtDTNThan.Text))
                                                ttbx.DThoaiNT = txtDTNThan.Text.Trim();
                                            if (!string.IsNullOrEmpty(txtSoKSinh_CMT.Text))
                                                ttbx.SoKSinh = txtSoKSinh_CMT.Text.Trim();
                                            if (!string.IsNullOrEmpty(txt_NoiCapCMT.Text))
                                                ttbx.NoiCapCMT = txt_NoiCapCMT.Text.Trim();
                                            if (!string.IsNullOrEmpty(cboQuocTich.Text))
                                            {
                                                ttbx.NgoaiKieu = cboQuocTich.Text.ToString();
                                            }
                                            if (!string.IsNullOrEmpty(dtNgayCap.Text))
                                            {
                                                ttbx.NgayCapCMT = dtNgayCap.DateTime;
                                            }
                                            if (!string.IsNullOrEmpty(filePath))
                                            {
                                                ttbx.FileAnh = filePath;
                                            }
                                            if (!string.IsNullOrEmpty(txtMaBNCovid.Text))
                                            {
                                                if (txtMaBNCovid.Text.Length > 10)
                                                {
                                                    var ttbx11 = _dataContext.TTboXungs.Where(p => p.MaBNCovid == txtMaBNCovid.Text.Remove(10) && p.MaBNCovid != "" && p.MaBNhan != _mabn);
                                                    var ttbx21 = (from ttbx3 in ttbx11
                                                                  join bn in _dataContext.BenhNhans on ttbx3.MaBNhan equals bn.MaBNhan
                                                                  select new { bn.TenBNhan });
                                                    if (ttbx11.Count() > 0)
                                                    {
                                                        MessageBox.Show("Mã " + txtMaBNCovid.Text.Remove(10) + " đã được sử dụng cho bệnh nhân " + ttbx21.First().TenBNhan);
                                                        return;
                                                    }
                                                    else
                                                    {
                                                        ttbx.MaBNCovid = txtMaBNCovid.Text.Remove(10);
                                                    }
                                                }
                                                else
                                                {
                                                    ttbx.MaBNCovid = txtMaBNCovid.Text;
                                                }
                                                var ttbx1 = _dataContext.TTboXungs.Where(p => p.MaBNCovid == txtMaBNCovid.Text && p.MaBNCovid != "" && p.MaBNhan != _mabn);
                                                var ttbx2 = (from ttbx3 in ttbx1
                                                             join bn in _dataContext.BenhNhans on ttbx3.MaBNhan equals bn.MaBNhan
                                                             select new { bn.TenBNhan });
                                                if (ttbx1.Count() > 0)
                                                {
                                                    MessageBox.Show("Mã " + txtMaBNCovid.Text + " đã được sử dụng cho bệnh nhân " + ttbx2.First().TenBNhan);
                                                    return;
                                                }
                                            }
                                            ttbx.ThonPho = txtThonPho.Text;
                                            _data.TTboXungs.Add(ttbx);
                                            _data.SaveChanges();
                                        }

                                        #endregion cập nhật TTbx

                                        #region nhapcannangTEduoi1tuoi

                                        //"12-0" dành cho các viện khác đối với viện 30010 hiện lên cho trẻ em <= 72 tháng tuối

                                        if (DungChung.Bien.MaBV != "30010")
                                        {
                                            nhapcannangTEduoi1tuoi(_data, _mabn, "12-0");
                                        }
                                        else if (DungChung.Bien.MaBV == "30003")
                                        {
                                            nhapcannangTEduoi1tuoi(_data, _mabn, "84-0");
                                        }
                                        else
                                        {
                                            nhapcannangTEduoi1tuoi(_data, _mabn, "72-0");
                                        }

                                        #endregion nhapcannangTEduoi1tuoi

                                        #region cập nhật lại bn nợ thẻ

                                        bool capnhat = false;
                                        var _ldthuocct = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _mabnhan)
                                                          join dtct in _dataContext.DThuoccts.Where(p => p.ThanhToan == 0) on dt.IDDon equals dtct.IDDon
                                                          select dtct).ToList();
                                        if (_ldthuocct.Count > 0)
                                            capnhat = true;
                                        var _lcd = (from cls in _dataContext.CLS.Where(p => p.MaBNhan == _mabnhan)
                                                    join cd in _dataContext.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                                    select cd).ToList();
                                        if (_lcd.Count > 0)
                                            capnhat = true;
                                        if (capnhat)
                                        {
                                            if (_dtuongSua != "BHYT" && cboDTuong.Text == "BHYT")
                                            {
                                                //DialogResult dialogResult = MessageBox.Show("Bệnh nhân được cập nhật thành đối tượng BHYT, bạn có muốn cập nhật lại chi phí?", "Hỏi cập nhật", MessageBoxButtons.YesNo);
                                                //if (dialogResult == DialogResult.Yes)
                                                //{
                                                FormThamSo.frm_CapNhatDVBenhNhanNoThe frmCNBN = new FormThamSo.frm_CapNhatDVBenhNhanNoThe(_mabn, 1);
                                                frmCNBN.ShowDialog();
                                                //}
                                            }

                                            #endregion cập nhật lại bn nợ thẻ

                                            #region cập nhật từ BN BH sang BN dịch vụ

                                            else if (_dtuongSua == "BHYT" && cboDTuong.Text != "BHYT")
                                            {
                                                //DialogResult dialogResult = MessageBox.Show("Bệnh nhân được cập nhật thành đối tượng không phải Bảo hiểm, bạn có muốn cập nhật lại chi phí?", "Hỏi cập nhật", MessageBoxButtons.YesNo);
                                                //if (dialogResult == DialogResult.Yes)
                                                ////{
                                                FormThamSo.frm_CapNhatDVBenhNhanNoThe frmCNBN = new FormThamSo.frm_CapNhatDVBenhNhanNoThe(_mabn, 2);
                                                frmCNBN.ShowDialog();
                                                //}
                                            }
                                        }

                                        #endregion cập nhật từ BN BH sang BN dịch vụ

                                        if (chkLuonHienThi.Checked)
                                        {
                                            btnIn_Click(sender, e);
                                            frmHSBNNhapMoi_Load(sender, e);
                                            TTLuu = 1;
                                            resetControl();
                                            resetTTBX();
                                        }
                                        else
                                        {
                                            btnIn_Click(sender, e);
                                            frmHSBN frm = new frmHSBN();
                                            frm.Show();
                                            this.Dispose();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Số thẻ BH không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        luulog = false;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Tên BN không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    txtNhapTBN.Focus();
                                    luulog = false;
                                }
                            }
                        }

                        #endregion sua

                        if (luulog)
                        {
                            LOG moi = new LOG();
                            moi.DateLog = DateTime.Now;
                            moi.LyDo = _lydoLodg;
                            moi.UserName = DungChung.Bien.TenDN;
                            moi.MaBNhan = _mabn;
                            moi.IdForm = 9045;
                            moi.MaCB = DungChung.Bien.MaCB;
                            moi.ComputerName = SystemInformation.ComputerName;
                            moi.Status = 2;
                            _data.LOGs.Add(moi);
                            _data.SaveChanges();
                            _lydoLodg = "";
                        }
                        break;

                        #endregion sửa
                }
            }
        }

        private void nhapcannangTEduoi1tuoi(QLBV_Database.QLBVEntities data, int mabn, string GioiHan)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string tuoi = DungChung.Ham.TuoitheoThang(data, mabn, GioiHan);
            var bn = data.TTboXungs.Where(p => p.MaBNhan == mabn).Select(p => p).FirstOrDefault();
            if (tuoi.ToLower().Contains("tháng"))
            {
                if (bn == null || string.IsNullOrEmpty(bn.CanNang_ChieuCao) || (bn.CanNang_ChieuCao.Contains(";") && string.IsNullOrEmpty(bn.CanNang_ChieuCao.Split(';')[0])))
                {
                    frm_TTKhamBenh frm = new frm_TTKhamBenh(mabn);
                    frm.ShowDialog();
                }
            }
        }

        public string thangTuoi(string gioihan, DateTime ngaynhap, string tuoi, string ngaysinh, string thangsinh, string namsinh)
        {
            try
            {
                string[] _gioihan;
                _gioihan = gioihan.Split('-');
                int gioihanthang = 0, gioihanngay = 0;
                if (_gioihan.Length > 1)
                {
                    int.TryParse(_gioihan[0], out gioihanthang);
                    int.TryParse(_gioihan[1], out gioihanngay);
                }

                int _ngays = 1;
                int _thangs = 1;
                int _nams = 1900;
                if (ngaysinh != null && ngaysinh != "")
                    _ngays = Convert.ToInt32(ngaysinh);
                if (thangsinh != null && thangsinh != "")
                    _thangs = Convert.ToInt32(thangsinh);
                if (namsinh != null && namsinh != "")
                    _nams = Convert.ToInt32(namsinh);
                int nam = ngaynhap.Year - _nams;
                int thangtuoi = 0, ngaytuoi = 0;
                if (_ngays == 0)
                    _ngays = 1;
                if (_thangs == 0)
                    _thangs = 1;
                string a = _ngays + "/" + _thangs + "/" + _nams;
                DateTime _ngaysinh = Convert.ToDateTime(a);
                if (nam <= 0)
                {
                    thangtuoi = (ngaynhap.Month - _thangs);
                }
                else
                {
                    thangtuoi = (ngaynhap.Month - _thangs) + 12 * nam;
                }
                ngaytuoi = (ngaynhap - _ngaysinh).Days;
                if (thangtuoi <= gioihanthang)
                {
                    if (ngaytuoi <= gioihanngay)
                    {
                        tuoi = ngaytuoi.ToString() + " ngày";
                    }
                    else
                    {
                        tuoi = thangtuoi.ToString() + " tháng";
                    }
                }

                return tuoi;
            }
            catch (Exception ex)
            {
                return tuoi;
            }
        }

        private bool ktraHSSK()
        {
            if (DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "24009")
            {
                bool BNTreEm = false;

                string matinh = "", maxa = "", mahuyen = "";
                string matinhkhaisinh = "", diachikhaisinh = "", hokhauthuongtru = "", mahuyenkhaisinh = "", maxakhaisinh = "";
                string maNgheNghiep = "", noilamviec = "";
                string ngaysinh = "", thangsinh = "";
                string soCMT = "", sodienthoai = "";
                DateTime? ngaycap = null;
                string moiQH = "", TenNguoiThan = "", soDTNguoiThan = "";

                if (lupMaTinh.EditValue != null)
                    matinh = lupMaTinh.EditValue.ToString().Trim();
                if (lupMaXa.EditValue != null)
                    maxa = lupMaXa.EditValue.ToString().Trim();
                if (lupMaHuyen.EditValue != null)
                    mahuyen = lupMaHuyen.EditValue.ToString().Trim();
                if (lupTinhKhaiSinh.EditValue != null)
                    matinhkhaisinh = lupTinhKhaiSinh.EditValue.ToString().Trim();
                if (lupMaHuyenKhaiSinh.EditValue != null && (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "30372"))
                {
                    mahuyenkhaisinh = lupMaHuyenKhaiSinh.EditValue.ToString().Trim();
                }
                if (lupXaKhaiSinh.EditValue != null && (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "30372"))
                {
                    maxakhaisinh = lupXaKhaiSinh.EditValue.ToString().Trim();
                }
                //diachikhaisinh = txtDChiKSinh.Text;
                hokhauthuongtru = txtHKKT.Text;
                if (lupMaNN_ma.EditValue != null)
                {
                    maNgheNghiep = lupMaNN_ma.EditValue.ToString().Trim();
                }
                noilamviec = txtNoiLV.Text;
                ngaysinh = txtNgaySinh.Text;
                thangsinh = txtThangSinh.Text;
                soCMT = txtSoKSinh_CMT.Text;
                if (dtNgayCap.EditValue != null)
                    ngaycap = dtNgayCap.DateTime;
                if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                {
                    sodienthoai = txtDT12345.Text.Trim();
                }
                else
                {
                    sodienthoai = txtSoDT.Text.Trim();
                }
                if (lupQuanHe.EditValue != null)
                {
                    moiQH = lupQuanHe.EditValue.ToString().Trim();
                }
                TenNguoiThan = txtNThan.Text;
                soDTNguoiThan = txtDTNThan.Text.Trim();
                int ot;
                string tuoi = thangTuoi("72-0", dtNgayN.DateTime, txtTuoi.Text, ngaysinh, thangsinh, txtNamSinh.Text);
                if (tuoi.ToLower().Contains("tháng"))
                {
                    BNTreEm = true;
                }
                if (matinh == "" && matinh.Length < 2 || matinh.Length > 3)
                {
                    MessageBox.Show("Bệnh nhân chưa có mã tỉnh");
                    return false;
                }
                if (mahuyen == "")
                {
                    MessageBox.Show("Bệnh nhân chưa có mã huyện");
                    return false;
                }
                if (maxa == "")
                {
                    MessageBox.Show("Bệnh nhân chưa có mã xã");
                    return false;
                }
                if (matinhkhaisinh == "" && matinhkhaisinh.Length < 2 || matinhkhaisinh.Length > 3)
                {
                    MessageBox.Show("Bệnh nhân chưa có mã tỉnh khai sinh");
                    return false;
                }
                if (diachikhaisinh == "" && DungChung.Bien.MaBV != "24009" && DungChung.Bien.MaBV != "24012")
                {
                    MessageBox.Show("Bệnh nhân chưa có địa chỉ khai sinh");
                    return false;
                }
                if (hokhauthuongtru == "" && DungChung.Bien.MaBV != "24009" && DungChung.Bien.MaBV != "24012")
                {
                    MessageBox.Show("Bệnh nhân chưa có hộ khẩu thường trú");
                    return false;
                }
                if (!int.TryParse(ngaysinh, out ot))
                {
                    MessageBox.Show("Ngày sinh không đúng định dạng");
                    return false;
                }
                if (Convert.ToInt32(ngaysinh) < 1 || Convert.ToInt32(ngaysinh) > 31)
                {
                    MessageBox.Show("Ngày sinh không đúng định dạng");
                    return false;
                }
                if (!int.TryParse(thangsinh, out ot))
                {
                    MessageBox.Show("Tháng sinh không đúng định dạng");
                    return false;
                }
                if (Convert.ToInt32(thangsinh) < 1 || Convert.ToInt32(thangsinh) > 12)
                {
                    MessageBox.Show("Tháng sinh không đúng định dạng");
                    return false;
                }
                if (!BNTreEm)
                {
                    if (TenNguoiThan.Contains(":") || TenNguoiThan.Contains(";") || TenNguoiThan.Contains("."))
                    {
                        MessageBox.Show("Tên người thân có ký tự lạ");
                        return false;
                    }
                    if (DungChung.Bien.MaBV == "20001" && ngaycap == null)
                    {
                        MessageBox.Show("Ngày cấp CMT không được để trống");
                        return false;
                    }
                    if (ngaycap != null && ngaycap < new DateTime(1801, 1, 1))
                    {
                        MessageBox.Show("Ngày cấp CMT không được nhỏ hơn hoặc bằng 1800");
                        return false;
                    }
                    if (DungChung.Bien.MaBV == "20001" && soCMT == "")
                    {
                        MessageBox.Show("Bệnh nhân chưa có số CMND hoặc số thẻ căn cước");
                        return false;
                    }
                    if (soCMT != "" && soCMT.Length < 9 || soCMT.Length > 12)
                    {
                        MessageBox.Show("Số chứng minh nhân dân, thẻ căn cước không được có số ký tự nhỏ hơn 9 hoặc >12");
                        return false;
                    }
                    if (DungChung.Bien.MaBV == "20001" && sodienthoai == "")
                    {
                        MessageBox.Show("Bệnh nhân chưa có số điện thoại");
                        return false;
                    }
                    if (sodienthoai != "" && !int.TryParse(sodienthoai, out ot))
                    {
                        MessageBox.Show("Số điện thoại không đúng định dạng");
                        return false;
                    }
                    if (!string.IsNullOrEmpty(maNgheNghiep) && noilamviec == "")
                    {
                        MessageBox.Show("Bệnh nhân chưa có nơi làm việc");
                        return false;
                    }
                }
                else
                {
                    if (!int.TryParse(moiQH, out ot))
                    {
                        MessageBox.Show("Quan hệ với người thân không đúng định dạng");
                        return false;
                    }
                    int qh = Convert.ToInt32(moiQH);
                    if (qh < 1 || qh > 17 || qh == 14 || qh == 15)
                    {
                        MessageBox.Show("Mã quan hệ với người thân: " + moiQH + " không đúng ");
                        return false;
                    }
                    if (TenNguoiThan == "")
                    {
                        MessageBox.Show("Tên người thân không được để trống");
                        return false;
                    }
                    if (TenNguoiThan.Contains(":") || TenNguoiThan.Contains(";") || TenNguoiThan.Contains("."))
                    {
                        MessageBox.Show("Tên người thân có ký tự lạ");
                        return false;
                    }
                    if (soDTNguoiThan == "")
                    {
                        MessageBox.Show("SĐT người thân không được để trống");
                        return false;
                    }
                }
                return true;
            }
            else
                return true;
        }

        private void setDBSoBKB()
        {
            int noingoaitru = -1;

            if (DungChung.Bien.PP_SoKB == 1 || DungChung.Bien.PP_SoKB == 2)
            {
                int rs, so = 0, makp = 0;
                if (DungChung.Bien.PP_SoKB == 1 &&
                    lupKhoaKham.EditValue != null &&
                    lupKhoaKham.EditValue.ToString() != "")
                    makp = Convert.ToInt32(lupKhoaKham.EditValue);
                if (Int32.TryParse(txtSoKB.Text, out rs))
                {
                    so = Convert.ToInt32(txtSoKB.Text);
                }

                if (!DungChung.Ham.checkSoPL(makp, so, 3, noingoaitru))
                {
                    DungChung.Ham.SetSoPL(makp, so, 3, noingoaitru);
                }
                else
                {
                    DungChung.Ham.SetSoPL(makp, 0, 3, noingoaitru);
                    setSoKB();
                }
                // }
            }
        }

        private void txtNhapTBN_EditValueChanged(object sender, EventArgs e)
        {
            if (lupKhoaKham.Properties.ReadOnly == true)
                lupKhoaKham_EditValueChanged(null, null);
        }

        public int CheckNoiNgoaiTru(int mabn)
        {
            var bn = _dataContext.BenhNhans.Where(p => p.MaBNhan == mabn).Select(p => p.NoiTru).FirstOrDefault();
            if (bn != null)
                return bn.Value;
            return -1;
        }

        public class NoiTinh
        {
            private string tennt;
            private int noitinh;

            public string TenNoiTinh
            {
                set { tennt = value; }
                get { return tennt; }
            }

            public int _NoiTinh
            {
                set { noitinh = value; }
                get { return noitinh; }
            }
        }

        //
        // Tai nạn giao thông(Đường bộ)
        // Tai nạn giao thông(Đường sắt)
        // Tai nạn giao thông(Đường thủy)
        // Tai nạn lao động
        // Tai nạn sinh hoạt
        //Đánh nhau
        //Tự tử
        //Ngộ độc
        //Đuối nước
        //Khác
        private int _load = 0; private List<DTBN> _ldtbn = new List<DTBN>();

        public List<HinhThucKham> HINH_THUC_KHAM_KSK = new List<HinhThucKham>();

        public List<HinhThucKham> GetHinhthuckham_KSK(string hinhthuckham)
        {
            HINH_THUC_KHAM_KSK = new List<HinhThucKham>();

            if (hinhthuckham == "ksk")
            {
                if (DungChung.Bien.MaBV == "30010")
                {
                    HINH_THUC_KHAM_KSK.Add(new HinhThucKham { Value = "1", Text = "Khám sức khỏe (Trên 18 tuổi)" });
                    HINH_THUC_KHAM_KSK.Add(new HinhThucKham { Value = "2", Text = "Khám sức khỏe (Dưới 18 tuổi)" });
                    HINH_THUC_KHAM_KSK.Add(new HinhThucKham { Value = "3", Text = "Khám sức khỏe lái xe ô tô" });
                    HINH_THUC_KHAM_KSK.Add(new HinhThucKham { Value = "4", Text = "Khám sức khỏe học sinh" });
                    HINH_THUC_KHAM_KSK.Add(new HinhThucKham { Value = "5", Text = "Khám sức khỏe lái xe máy" });
                }
                else
                {
                    if (DungChung.Bien.MaBV == "30009")
                    {
                        HINH_THUC_KHAM_KSK.Add(new HinhThucKham { Value = "1", Text = "Người lao động" });
                        HINH_THUC_KHAM_KSK.Add(new HinhThucKham { Value = "2", Text = "Học sinh, sinh viên" });
                        HINH_THUC_KHAM_KSK.Add(new HinhThucKham { Value = "3", Text = "Khám sức khỏe định kỳ" });
                        HINH_THUC_KHAM_KSK.Add(new HinhThucKham { Value = "4", Text = "Cấp bằng lái xe mới" });
                    }
                    else
                    {
                        HINH_THUC_KHAM_KSK.Add(new HinhThucKham { Value = "1", Text = "Khám tuyển" });
                        HINH_THUC_KHAM_KSK.Add(new HinhThucKham { Value = "2", Text = "Khám tuyển sinh" });
                        HINH_THUC_KHAM_KSK.Add(new HinhThucKham { Value = "3", Text = "Người lao động, sinh viên (Trên 18 tuổi)" });
                        HINH_THUC_KHAM_KSK.Add(new HinhThucKham { Value = "4", Text = "Học sinh, sinh siên (Dưới 18 tuổi)" });
                        HINH_THUC_KHAM_KSK.Add(new HinhThucKham { Value = "5", Text = "Khám sức khỏe định kỳ" });
                        HINH_THUC_KHAM_KSK.Add(new HinhThucKham { Value = "6", Text = "Cấp bằng lái xe máy" });
                        HINH_THUC_KHAM_KSK.Add(new HinhThucKham { Value = "7", Text = "Đổi bằng lái xem máy" });
                        HINH_THUC_KHAM_KSK.Add(new HinhThucKham { Value = "8", Text = "Cấp bằng lái xe máy" });
                        HINH_THUC_KHAM_KSK.Add(new HinhThucKham { Value = "9", Text = "Cấp bằng ô tô" });
                        HINH_THUC_KHAM_KSK.Add(new HinhThucKham { Value = "10", Text = "Đổi bằng ô tô" });
                        HINH_THUC_KHAM_KSK.Add(new HinhThucKham { Value = "11", Text = "Giám định sức khỏe" });
                        HINH_THUC_KHAM_KSK.Add(new HinhThucKham { Value = "12", Text = "Khám sức khỏe (Trên 18 tuổi)" });
                        HINH_THUC_KHAM_KSK.Add(new HinhThucKham { Value = "13", Text = "Khám sức khỏe (Dưới 18 tuổi)" });
                        HINH_THUC_KHAM_KSK.Add(new HinhThucKham { Value = "14", Text = "Khám sức khỏe (Lái xe)" });
                        HINH_THUC_KHAM_KSK.Add(new HinhThucKham { Value = "15", Text = "Khác" });
                    }
                }
            }
            else if (hinhthuckham == "bhyt" || hinhthuckham == "cbcs")
            {
                HINH_THUC_KHAM_KSK.Add(new HinhThucKham { Value = "0", Text = "Ngoại trú" });
                HINH_THUC_KHAM_KSK.Add(new HinhThucKham { Value = "1", Text = "Nội trú" });
            }

            return HINH_THUC_KHAM_KSK;
        }

        public class HinhThucKham
        {
            public string Value { get; set; }
            public string Text { get; set; }
        }
        private class c_ICD
        {
            private string maICD;

            public string MaICD
            {
                get { return maICD; }
                set { maICD = value; }
            }

            private string tenICD;

            public string TenICD
            {
                get { return tenICD; }
                set { tenICD = value; }
            }
        }

        private void setSoKB()
        {
            int noingoaitru = -1;

            int soKB = 0;
            if (DungChung.Bien.PP_SoKB == 1)
            {
                int makp = 0;
                if (lupKhoaKham.EditValue != null && lupKhoaKham.EditValue.ToString() != "")
                {
                    makp = Convert.ToInt32(lupKhoaKham.EditValue);

                    soKB = DungChung.Ham.GetSoPL(3, makp, noingoaitru);
                }
                else
                {
                    MessageBox.Show("chưa chọn khoa khám bệnh, không lấy được số khám bệnh");
                }
            }
            else if (DungChung.Bien.PP_SoKB == 2)
            {
                soKB = DungChung.Ham.GetSoPL(3, 0, noingoaitru);
            }
            txtSoKB.Text = soKB.ToString();
        }

        private List<BenhVien> _lBenhVien = new List<BenhVien>();
        private List<KPhong> _lkp = new List<KPhong>();
        private List<c_ICD> lICD = new List<c_ICD>();
        private bool KtraBNTHA = false;

        private class TTTheCu
        {
            public string Sthe { get; set; }
            public string TenBN { get; set; }
            public string DiaChi { get; set; }
            public int GTinh { get; set; }
            public string NgaySinh { get; set; }
            public string HanTu { get; set; }
            public string HanDen { get; set; }
            public string MaDKKCB { get; set; }
        }

        private List<TTTheCu> _lTTTheCu = new List<TTTheCu>();
        private string _maCQCQ = "";

        public void frmHSBNNhapMoi_Load(object sender, EventArgs e)
        {
            if(DungChung.Bien.MaBV == "30372")
            {
                txtDiaChi1.Visible = false;
                txtDiaChi.Visible = true;
            }    
            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "24012")
            {
                lupMaHuyenKhaiSinh.Visible = true;
                btnHuyenKhaiSinh.Visible = true;
                labelControl46.Text = "Huyện khai sinh:";
            }

            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "24000" || DungChung.Bien.MaBV == "24208" || DungChung.Bien.MaBV == "24209" || DungChung.Bien.MaBV == "24210" || DungChung.Bien.MaBV == "24211" || DungChung.Bien.MaBV == "24212" || DungChung.Bien.MaBV == "24213" || DungChung.Bien.MaBV == "24214" || DungChung.Bien.MaBV == "24215" || DungChung.Bien.MaBV == "24216" || DungChung.Bien.MaBV == "24217" || DungChung.Bien.MaBV == "24218" || DungChung.Bien.MaBV == "24219" || DungChung.Bien.MaBV == "24220" || DungChung.Bien.MaBV == "24221" || DungChung.Bien.MaBV == "24223" || DungChung.Bien.MaBV == "24224" || DungChung.Bien.MaBV == "24225" || DungChung.Bien.MaBV == "24226")
            {
                ckBNhanLao.Visible = true;
            }
            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
            {
                labelControl53.Visible = true;
                labelControl12.Visible = false;
                txtSoDT.Visible = false;
                txtDT12345.Visible = true;
            }
            else
            {
                labelControl53.Visible = false;
                txtDT12345.Visible = false;

                labelControl12.Visible = true;
                txtSoDT.Visible = true;
            }
            idperson = 0;
            personCode = "";
            if (DungChung.Bien.MaBV == "30005" && TTLuu == 2)
            {
                if (cboDTuong.Text == "BHYT")
                    chkNoThe30005.Visible = false;
                else
                    chkNoThe30005.Visible = true;
            }
            if (DungChung.Bien.MaBV == "30005" && TTLuu == 1)
            {
                chkNoThe.Visible = false;
            }

            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                ckcchuyenkhoan.Enabled = false;
            _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            bool ketoan30005 = false, kt30303 = false;

            txtTTThe_old.Properties.ReadOnly = true;
            GetTinhHuyenXa();

            #region set tabindex

            if (DungChung.Bien.MaBV == "30303")
            {
                txtTimSTHE.TabIndex = 1;
                txtMaCS.TabIndex = 2;
                dtHanBHTu.TabIndex = 3;
                dtHanBHden.TabIndex = 4;
                radTuyen.TabIndex = 5;
                txtNhapTBN.TabIndex = 6;
                radNamNu.TabIndex = 7;
                txtNgaySinh.TabIndex = 8;
                txtThangSinh.TabIndex = 9;
                txtNamSinh.TabIndex = 10;
                txtDiaChi1.TabIndex = 11;
                cboCapCuu.TabIndex = 12;
                txtTrieuChung.TabIndex = 13;
                lupMaICDkb.TabIndex = 14;
                lupKhoaKham.TabIndex = 15;
                btnOK.TabIndex = 16;
            }
            //if (DungChung.Bien.MaBV == "34019")
            //{
            //    // if(radNoiTru.SelectedIndexChanged)
            //}

            #endregion set tabindex

            var qCQCQ = _dataContext.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV).FirstOrDefault();
            if (qCQCQ != null && qCQCQ.MaChuQuan != null)
                _maCQCQ = qCQCQ.MaChuQuan.Trim();
            if (TTLuu == 1)
            {
                if (DungChung.Ham.checkQuyen(this.Name)[0])
                    btnOK.Enabled = true;
                else
                    btnOK.Enabled = false;
                txtMaDT.Enabled = true;
                txtMaBN.Enabled = true;
            }
            if (TTLuu == 2)
            {
                if (DungChung.Ham.checkQuyen(this.Name)[1])
                    btnOK.Enabled = true;
                else
                    btnOK.Enabled = false;
                if (DungChung.Bien.MaBV == "30012")
                    dtNgayN.ReadOnly = true;
                txtMaDT.Enabled = false;
                txtMaBN.Enabled = false;
                chkNoThe.Enabled = false;
                if (DungChung.Bien.MaBV == "24012")
                {
                    dtNgayN.ReadOnly = false;
                }
            }

            // https://vssoft.atlassian.net/jira/software/projects/HIS/boards/1?selectedIssue=HIS-603
            if (DungChung.Bien.MaBV == "30002")
            {
                lbSoto.Visible = true;
                txtSoto.Visible = true;
            }

            if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389")
            {
                labelControl54.Visible = true;
                txtMaBNCovid.Visible = true;
            }

            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                cboCapCuu.SelectedIndex = -1;
                chkIn.Checked = true;
            }
            if (DungChung.Bien.MaBV == "01830" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "30303" || DungChung.Bien.MaBV == "34019" || DungChung.Bien.MaBV == "30007")
            {
                chkIn.Checked = true;
            }

            if (DungChung.Bien.MaBV == "30372"/*DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "30303" ||*/)
            {
                // load ds địa chỉ;
                var bn = (from a in _dataContext.BenhNhans
                          select new { DChi = a.DChi }).Distinct().OrderBy(p => p).ToList();
                //bn.Insert(0, "");
                //var qdiaChi = bn.ToArray();
                txtDiaChi.Properties.DataSource = bn;
            }
            if (DungChung.Bien.MaBV == "27021")
            {
                txtsohsbnmantinh.Visible = true;
                labelControl42.Visible = true;
                ckcbnmantinh.Visible = true;
            }
            else
            {
                txtsohsbnmantinh.Visible = false;
                labelControl42.Visible = false;
                ckcbnmantinh.Visible = false;
            }
            if (DungChung.Bien.MaBV == "34019")
            {
                cboPLKham.Visible = true;
                labelControl42.Text = "PL Khám:";
                labelControl42.Visible = true;
            }
            _load = 0;
            lICD = (from ICD in _dataContext.ICD10 select new c_ICD { MaICD = ICD.MaICD ?? "", TenICD = ICD.TenICD ?? "" }).OrderBy(p => p.TenICD).ToList();
            lICD.Insert(0, new c_ICD { MaICD = "0", TenICD = "" });
            lupChanDoanKb.Properties.DataSource = lICD.ToList();

            lupMaICDkb.Properties.DataSource = lICD.ToList();

            dtNgayN.DateTime = System.DateTime.Now;
            _lkp = _dataContext.KPhongs.Where(p => p.Status == 1).ToList();
            var q = (from KhoaKham in _lkp
                     where (KhoaKham.PLoai == ("Lâm sàng") || KhoaKham.PLoai == ("Phòng khám") || ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "30303") ? KhoaKham.PLoai == DungChung.Bien.st_PhanLoaiKP.HanhChinh : false))
                     select KhoaKham).OrderByDescending(p => p.PLoai).ThenBy(p => p.TenKP).ToList();
            if (q.Count > 0)
            {
                lupKhoaKham.Properties.DataSource = q.ToList();
            }
            _lBenhVien = _dataContext.BenhViens.ToList();

            cboChuyenKhoa.Properties.DataSource = DungChung.Bien._listTaiNan.Where(p => p.Status);
            lupCVu.Properties.DataSource = _dataContext.ChucVus.OrderBy(p => p.Ten_CV).ToList();
            lup_CCap.Properties.DataSource = _dataContext.CapBacs.OrderBy(p => p.Ten_CB).ToList();
            _ldtbn = _dataContext.DTBNs.OrderBy(p => p.DTBN1).ToList();
            cboDTuong.Properties.DataSource = null;
            cboDTuong.Properties.DataSource = _ldtbn;
            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "27123")
            {
                if (_ldtbn.Where(p => p.HTTT == 2).ToList().Count > 0)
                    cboDTuong.EditValue = _ldtbn.Where(p => p.HTTT == 2).ToList().First().IDDTBN;
            }
            else if (DungChung.Bien.MaBV == "24297")
            {
                if (_ldtbn.Where(p => p.HTTT == 1).ToList().Count > 0)
                    cboDTuong.EditValue = _ldtbn.Where(p => p.HTTT == 1).ToList().First().IDDTBN;
            }
            else
                if (_ldtbn.Where(p => p.HTTT == 1).ToList().Count > 0)
                cboDTuong.EditValue = _ldtbn.Where(p => p.HTTT == 1).ToList().First().IDDTBN;
            if (DungChung.Bien.MaBV == "30005" || DungChung.Bien.MaBV == "27001")
            {
                string _ploaiKP = _dataContext.KPhongs.Where(p => p.MaKP == DungChung.Bien.MaKP).Select(p => p.PLoai).FirstOrDefault();
                if (_ploaiKP != null && _ploaiKP.ToString() == DungChung.Bien.st_PhanLoaiKP.KeToan)
                {
                    ketoan30005 = true;
                    if (DungChung.Bien.MaBV == "30005")
                    {
                        var qdtbn = _dataContext.DTBNs.Where(p => p.DTBN1 == "KSK").FirstOrDefault();
                        if (qdtbn != null)
                        {
                            cboDTuong.EditValue = qdtbn.IDDTBN;
                            cboDTuong.Enabled = false;
                        }
                        else
                        {
                            cboDTuong.EditValue = -1;
                            cboDTuong.Enabled = false;
                        }
                    }
                }
            }
            cboDTuong_EditValueChanged(sender, e);

            var qHinhThucKSK =
            _ketqua = "";
            if (DungChung.Bien.PLoaiKP == "Admin")
            {
                btnLuuTBNV.Visible = true;
            }
            else
            {
                btnLuuTBNV.Visible = false;
            }
            lupKhoaKham.EditValue = 0;

            lupNguoiNhap.Properties.DataSource = _dataContext.CanBoes.ToList();

            lupNguoiNhap.EditValue = DungChung.Bien.MaCB;

            var khoaphong = _lkp.Where(p => p.MaKP == (DungChung.Bien.MaKP)).ToList();
            if (khoaphong.Count > 0)
            {
                if (khoaphong.First().PLoai != "Hành chính" && khoaphong.First().PLoai != "Admin")
                {
                    if ((DungChung.Bien.MaBV != "30005" && DungChung.Bien.MaBV != "27001") || ketoan30005 == false)
                        lupKhoaKham.Properties.ReadOnly = true;
                    if (khoaphong.First().PLoai == DungChung.Bien.st_PhanLoaiKP.KeToan && DungChung.Bien.MaBV == "30303")
                    {
                        lupKhoaKham.Properties.ReadOnly = false;
                    }

                    lupKhoaKham.EditValue = DungChung.Bien.MaKP;
                }
                else
                {
                    if (DungChung.Bien.MaBV != "06007" && DungChung.Bien.MaBV != "30009")
                    {
                        lupKhoaKham.Properties.ReadOnly = false;
                        lupKhoaKham.EditValue = DungChung.Bien.MaKP;
                    }
                }
                if (khoaphong.First().PLoai == "Admin" || (DungChung.Bien.MaBV == "26007") || DungChung.Bien.MaBV == "30012")
                {
                    radNoiTru.Properties.ReadOnly = false;
                    lupNoiNgoaiTinh.Properties.ReadOnly = false;
                }
                else
                {
                    radNoiTru.Properties.ReadOnly = true;
                }
            }
            _lmucTT = _dataContext.MucTTs.ToList();
            List<NoiTinh> _lnoitinh = new List<NoiTinh>();
            _lnoitinh.Add(new NoiTinh { TenNoiTinh = "A. BN nội tỉnh KCB ban đầu", _NoiTinh = 1 });
            _lnoitinh.Add(new NoiTinh { TenNoiTinh = "B. BN nội tỉnh đến", _NoiTinh = 2 });
            _lnoitinh.Add(new NoiTinh { TenNoiTinh = "C. BN ngoại tỉnh đến", _NoiTinh = 3 });

            lupNoiNgoaiTinh.Properties.DataSource = _lnoitinh.ToList();
            var dtoc = _dataContext.DanTocs.OrderBy(p => p.TenDT).ToList();

            lupDantoc.Properties.DataSource = dtoc.ToList();
            lupMaDT_ma.Properties.DataSource = dtoc.ToList();
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                var maDtoc = _dataContext.DanTocs.OrderBy(p => p.MaDTBak).ToList();
                lupMaDT_ma.Properties.DataSource = maDtoc.ToList();
                lupDantoc.Properties.DataSource = maDtoc.ToList();

                lupMaDT_ma.Properties.DisplayMember = "MaDTBak";
                lupMaDT_ma.Properties.ValueMember = "MaDTBak";

                lupDantoc.Properties.DisplayMember = "TenDT";
                lupDantoc.Properties.ValueMember = "MaDTBak";
            }

            // quốc tịch
            var qt = _dataContext.TTboXungs.Select(p => p.NgoaiKieu).Distinct().ToList();
            if (qt.Count > 0)
            {
                foreach (var a in qt)
                {
                    if (a != null && a.ToString() != "")
                        cboQuocTich.Properties.Items.Add(a);
                }
            }
            var NN = _dataContext.DmNNs.OrderBy(p => p.TenNN).ToList();
            lupNgheNghiep.Properties.DataSource = NN;
            lupMaNN_ma.Properties.DataSource = NN;
            lupMHuong.Properties.DataSource = _dataContext.MucTTs.ToList();
            //quan hệ
            List<QuanHeGiaDinh> qqhgd = _dataContext.QuanHeGiaDinhs.ToList();
            qqhgd.Insert(0, new QuanHeGiaDinh { MaQuanHe = "0", TenQuanHe = "" });
            lupQuanHe.Properties.DataSource = qqhgd;
            lupQuanHe.Properties.DisplayMember = "TenQuanHe";
            lupQuanHe.Properties.ValueMember = "MaQuanHe";
            lupQuanHe.EditValue = lupQuanHe.Properties.GetKeyValueByDisplayText("");

            //----------

            List<BenhVien> _lbv = new List<BenhVien>();
            if (DungChung.Bien.MaBV == "24012")
            {
                _lbv = (from ten in _lBenhVien.Where(p => p.status == 2 || p.status == 1 || p.MaChuQuan == DungChung.Bien.MaBV)
                        select ten).OrderBy(p => p.TenBV).ToList();
            }
            else
                _lbv = (from ten in _lBenhVien.Where(p => p.status == 2 || p.MaChuQuan == DungChung.Bien.MaBV)
                        select ten).OrderBy(p => p.TenBV).ToList();
            _lbv.Add(new BenhVien { TenBV = "", MaBV = "" });

            #region (TTLuu == 1)

            if (TTLuu == 1)
            {
                txtTimSTHE.ResetText();
                lupMaBVgt.Properties.DataSource = _lbv.OrderBy(p => p.TenBV).ToList();

                lupMaNoigt.Properties.DataSource = _lbv.ToList().OrderBy(p => p.MaBV).ToList();

                var MS = (from ID in _dataContext.BenhNhans select ID.MaBNhan).ToList();
                string Maso = "";
                if (MS.Count > 0)
                    Maso = MS.Max().ToString();
                if (Maso != "")
                {
                    txtMaBenhNhan.Text = Maso;
                }
                else
                {
                    txtMaBenhNhan.Text = "1";
                }
                var tinh = (from tin in _dataContext.DmTinhs select new { tin.TenTinh, tin.MaTinh }).OrderBy(p => p.TenTinh).ToList();
                lupMaTinh.Properties.DataSource = tinh.ToList();
                lupTinhKhaiSinh.Properties.DataSource = tinh.ToList();
                var huyen = (from h in _dataContext.DmHuyens.Where(p => p.MaTinh == (_matinh)) select new { h.TenHuyen, h.MaHuyen }).OrderBy(p => p.TenHuyen).ToList();
                lupMaHuyen.Properties.DataSource = huyen.ToList();
                var xa = (from x in _dataContext.DmXas.Where(p => p.MaHuyen == (_mahuyen)) select new { x.TenXa, x.MaXa }).OrderBy(p => p.TenXa).ToList();
                lupMaXa.Properties.DataSource = xa.ToList();
                if (DungChung.Bien.MaBV == "24009")
                {
                    lupMaHuyenKhaiSinh.Properties.DataSource = huyen.ToList();
                    lupXaKhaiSinh.Properties.DataSource = xa.ToList();
                }
                txtCode.Focus();
                cboPLKham.SelectedIndex = 0;
            }

            #endregion (TTLuu == 1)

            else
            {
                #region (TTLuu == 2)

                if (TTLuu == 2)
                {
                    ckcBoXungThe.Visible = true;
                    btnOK.Enabled = true;
                    btnThoat.Visible = false;
                    grcBHYT.Visible = false;
                    lupKhoaKham.Properties.DataSource = _dataContext.KPhongs.Where(s=> s.PLoai == ("Lâm sàng") || s.PLoai == ("Phòng khám") || ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "30303") ? s.PLoai == DungChung.Bien.st_PhanLoaiKP.HanhChinh : false)).Where(p => p.Status == 1).ToList();

                    var tinh = (from tin in _dataContext.DmTinhs select new { tin.TenTinh, tin.MaTinh }).OrderBy(p => p.TenTinh).ToList();
                    lupMaTinh.Properties.DataSource = tinh.ToList();
                    lupTinhKhaiSinh.Properties.DataSource = tinh.ToList();
                    var huyen = (from h in _dataContext.DmHuyens.Where(p => p.MaTinh == (_matinh)) select new { h.TenHuyen, h.MaHuyen }).OrderBy(p => p.TenHuyen).ToList();
                    lupMaHuyen.Properties.DataSource = huyen.ToList();
                    var xa = (from x in _dataContext.DmXas.Where(p => p.MaHuyen == (_mahuyen)) select new { x.TenXa, x.MaXa }).OrderBy(p => p.TenXa).ToList();
                    lupMaXa.Properties.DataSource = xa.ToList();
                    _benhnhan = _dataContext.BenhNhans.Where(p => p.MaBNhan == (_mabn)).ToList();

                    txtMaBN.Text = DungChung.Bien.MaBV == "30007" ? _benhnhan.First().PersonCode : _benhnhan.First().IDPerson.ToString();
                    txtMaDT.Text = _benhnhan.First().MaBNhan.ToString();

                    var bnhancv = _dataContext.TTboXungs.Where(p => p.MaBNhan == (_mabn)).ToList();
                    if (_benhnhan.First().IsCCCD == true)
                    {
                        chk_CCCD.Checked = true;
                    }
                    else if (_benhnhan.First().IsCCCD == false)
                    {
                        chk_CCCD.Checked = false;
                    }
                    if (bnhancv.First().MaBNCovid == null)
                    {
                        txtMaBNCovid.Text = "";
                    }
                    else
                    {
                        txtMaBNCovid.Text = bnhancv.First().MaBNCovid.ToString();
                    }

                    if (DungChung.Bien.MaBV == "30005")
                    {
                        chkNoThe30005.Checked = _benhnhan.First().NoThe;
                    }

                    lupMaBVgt.Properties.DataSource = _lbv.OrderBy(p => p.TenBV).ToList();
                    lupMaNoigt.Properties.DataSource = _lbv.OrderBy(p => p.MaBV).ToList();

                    FillThongTinBenhNhan(_benhnhan.First(), false);

                    var _ttoan = _dataContext.VienPhis.Where(p => p.MaBNhan == (_mabn)).ToList();

                    if (_ttoan.Count > 0)
                    {
                        E_TTBoXung(true);
                        E_BHYT(false);
                        E_TTBN(false);
                        EnableHanMucLuongCS(true);
                        dtNgayN.Enabled = false;
                    }
                    else
                    {
                        var _dt = _dataContext.BNKBs.Where(p => p.MaBNhan == (_mabn)).ToList();
                        if (_dt.Count > 0)
                        {
                            E_TTBoXung(true);
                            E_BHYT(true);
                            E_TTBN(true);
                            dtNgayN.Enabled = false;
                            cboCapCuu.Enabled = true;
                        }

                        if (_boPhan == 1)
                            EnableHanMucLuongCS(false);

                        if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389")
                        {
                            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                            var ktrv = _data.RaViens.Where(p => p.MaBNhan == _mabn).ToList();
                            if (ktrv.Count > 0)
                            {
                                dtNgayN.ReadOnly = true;
                            }
                            else
                            {
                                dtNgayN.Enabled = true;
                            }

                            var ktkb = _data.BNKBs.Where(p => p.MaBNhan == _mabn).OrderBy(p => p.NgayKham).ToList();
                            if (ktkb.Count > 0)
                            {
                                if ((dtNgayN.DateTime - ktkb.First().NgayKham.Value).Hours > 0)
                                {
                                    MessageBox.Show("Bệnh nhân đã được khám, Ngày nhập phải <= " + ktkb.First().NgayKham.Value.ToShortDateString());
                                    dtNgayN.Focus();
                                    return;
                                }
                            }
                        }
                    }
                }

                #endregion (TTLuu == 2)
            }
            if (btnOK.Enabled == true && ketoan30005 && cboDTuong.Text != "KSK" && DungChung.Bien.MaBV == "30005")
            {
                btnOK.Enabled = false;
            }

            if (DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "24009")
            {
                lupMaDT_ma.Properties.DisplayMember = "MaDanToc";
                lupDantoc.Properties.Columns[1].FieldName = "MaDanToc";
                lupMaDT_ma.Properties.Columns[0].FieldName = "MaDanToc";
            }

            if (TTLuu == 2)
            {
                var bnkb = _dataContext.BNKBs.Where(o => o.MaBNhan == _mabn).ToList();
                if (bnkb != null && bnkb.Count > 0)
                {
                    if (DungChung.Bien.MaBV != "30012")
                        radNoiTru.Enabled = false;
                    else
                        radNoiTru.Enabled = true;
                    lupKhoaKham.Enabled = false;
                }

                if (DungChung.Bien.MaBV == "34019" && DungChung.Bien.PLoaiKP != "Admin")
                {
                    txtMaCS.Enabled = false;
                    dtHanBHden.Enabled = false;
                    dtHanBHTu.Enabled = false;
                    lupMaBVgt.Enabled = false;
                    cboDTuong.Enabled = false;
                }
                if (DungChung.Bien.MaBV == "30372")
                {
                    var VP = _dataContext.VienPhis.Where(p => p.MaBNhan == _mabn).ToList();
                    if (VP.Count > 0)
                    {
                        if (_dataContext.VienPhis.Where(p => p.MaBNhan == _mabn).First().ExportBHXH == true)
                        {
                            dtNgayN.Enabled = false;
                        }
                    }
                    dtNgayN.ReadOnly = false;
                }
            }
            _load++;
        }

        private void FillThongTinBenhNhan(BenhNhan bn, bool sdt)
        {
            idperson = bn.IDPerson ?? 0;
            personCode = bn.PersonCode;
            // thông tin bổ xung
            var _ttbx = _dataContext.TTboXungs.Where(p => p.MaBNhan == (bn.MaBNhan)).ToList();

            if (!sdt)
                dtNgayN.DateTime = bn.NNhap.Value;
            else
                dtNgayN.DateTime = DateTime.Now;

            cboDTuong.Text = bn.DTuong; // tạm thời, sau đổi sang IDDTBN
            cboChuyenKhoa.EditValue = bn.ChuyenKhoa;
            txtSoKB.Text = bn.SoKB.ToString();
            lupNguoiNhap.EditValue = bn.MaCB;
            cboCapCuu.SelectedIndex = bn.CapCuu ?? 0;
            cboPLKham.SelectedIndex = bn.PLKham;
            txtTimSTHE.Text = bn.SThe;
            _ketqua = !string.IsNullOrWhiteSpace(bn.SThe) ? bn.SThe.ToUpper() : "";
            txtMaCS.Text = bn.MaCS;
            lupNoiNgoaiTinh.EditValue = bn.NoiTinh;
            dtHanBHTu.EditValue = bn.HanBHTu;
            dtHanBHden.EditValue = bn.HanBHDen;
            string NT = _ttbx.First().HoTenBoMe;
            if (NT != null)
            {
                string[] NguoiThan = NT.Split(';');
                txtHoTenBo.Text = NguoiThan[0].ToString();
                txtHoTenMe.Text = NguoiThan[1].ToString();
            }
            txtNhapTBN.Text = bn.TenBNhan;
            if (bn.GTinh != null)
                switch (bn.GTinh)
                {
                    case 1:
                        radNamNu.SelectedIndex = 0;
                        break;

                    case 0:
                        radNamNu.SelectedIndex = 1;
                        break;
                }
            txtNamSinh.Text = bn.NamSinh;
            txtNgaySinh.Text = bn.NgaySinh == null ? "" : (bn.NgaySinh.Length == 1 ? ("0" + bn.NgaySinh) : bn.NgaySinh);
            txtThangSinh.Text = bn.ThangSinh == null ? "" : (bn.ThangSinh.Length == 1 ? ("0" + bn.ThangSinh) : bn.ThangSinh);
            txtTuoi.Text = (DateTime.Now.Year - int.Parse(DungChung.Ham.Right(bn.NamSinh, 4))).ToString();

            if (bn.Tuoi != null && !sdt)
                txtTuoi.Text = bn.Tuoi.ToString();
            txtDiaChi1.Text = txtDiaChi.Text = bn.DChi;
            if (cboDTuong.Text == "KSK")
            {
                lupHinhThucKham.Text = bn.TChung;
            }
            else
                txtTrieuChung.Text = bn.TChung;
            lupMaBVgt.EditValue = bn.MaBV;

            txtCDNoiGT.Text = bn.CDNoiGT;

            if (!sdt)
            {
                lupKhoaKham.EditValue = bn.MaKP;
            }
            else
            {
                lupKhoaKham.EditValue = null;
                lupKhoaKham.EditValue = null;
            }
            if (bn.SoTT != null && !sdt)
            {
                txtSoTT.Text = bn.SoTT.ToString();
            }

            if (bn.MucHuong != null)
                lupMHuong.Text = bn.MucHuong.ToString();
            dtDu5nam.EditValue = bn.NgayHM;
            cboKhuVuc.Text = bn.KhuVuc;
            if (bn.LuongCS != null && bn.LuongCS.Value == 1)
            {
                chkLuongCS.Checked = true;
            }
            else
                chkLuongCS.Checked = false;
            if (bn.UuTien != null)
            {
                chkuutien.Checked = bn.UuTien.Value;
            }
            else
            {
                chkuutien.Checked = false;
            }

            if (bn.NoThe != null)
            {
                chkNoThe.Checked = bn.NoThe;
            }
            else
            {
                chkNoThe.Checked = false;
            }

            if (bn.Tuyen != null)
            {
                if (bn.Tuyen == 1)
                {
                    int tt = DungChung.Ham.checkThongTuyen(bn.MaKCB, bn.MaCS);
                    if (tt == 4)
                        radTuyen.SelectedIndex = 2;
                    else
                        radTuyen.SelectedIndex = 0;

                    //radTuyen.SelectedIndex = 0;
                }
                else
                {
                    radTuyen.SelectedIndex = 1;
                }
            }
            if (DungChung.Bien.MaBV == "24009" ||
                DungChung.Bien.MaBV == "24000" ||
                DungChung.Bien.MaBV == "24208" ||
                DungChung.Bien.MaBV == "24209" ||
                DungChung.Bien.MaBV == "24210" ||
                DungChung.Bien.MaBV == "24211" ||
                DungChung.Bien.MaBV == "24212" ||
                DungChung.Bien.MaBV == "24213" ||
                DungChung.Bien.MaBV == "24214" ||
                DungChung.Bien.MaBV == "24215" ||
                DungChung.Bien.MaBV == "24216" ||
                DungChung.Bien.MaBV == "24217" ||
                DungChung.Bien.MaBV == "24218" ||
                DungChung.Bien.MaBV == "24219" ||
                DungChung.Bien.MaBV == "24220" || DungChung.Bien.MaBV == "24221" || DungChung.Bien.MaBV == "24223" || DungChung.Bien.MaBV == "24224" || DungChung.Bien.MaBV == "24225" || DungChung.Bien.MaBV == "24226")
            {
                ckBNhanLao.Checked = bn.BNhanLao == true ? true : false;
            }

            if (_ttbx.Count > 0)
            {
                txtSoDT.Text = _ttbx.First().DThoai;
                txtDT12345.Text = _ttbx.First().DThoai;
                lup_CCap.EditValue = _ttbx.First().ID_CB;
                lupCVu.EditValue = _ttbx.First().ID_CV;
                //dtNgayCap.DateTime = _ttbx.First().NgayCapCMT.Value;
                txt_NoiCapCMT.Text = _ttbx.First().NoiCapCMT;
                lupMaTinh.EditValue = _ttbx.First().MaTinh;
                lupMaHuyen.EditValue = _ttbx.First().MaHuyen;
                lupMaXa.EditValue = _ttbx.First().MaXa;
                lupDantoc.EditValue = _ttbx.First().MaDT;
                lupNgheNghiep.EditValue = _ttbx.First().MaNN;
                txtNoiLV.Text = _ttbx.First().NoiLV;
                txtDTNThan.Text = _ttbx.First().DThoaiNT;
                txtNThan.Text = _ttbx.First().NThan;
                txtSoKSinh_CMT.Text = _ttbx.First().SoKSinh;
                cboQuocTich.Text = _ttbx.First().NgoaiKieu;
                dtNgayCap.EditValue = _ttbx.First().NgayCapCMT;
                txtThonPho.Text = _ttbx.First().ThonPho;
                lupMaICDkb.EditValue = _ttbx.First().MaICD;
                ckcchuyenkhoan.Checked = Convert.ToBoolean(_ttbx.First().HTThanhToan);
                txtTTThe_old.Text = _ttbx.First().TTTheBHYTold;
                lupTinhKhaiSinh.EditValue = string.IsNullOrEmpty(_ttbx.First().MaTinhKhaiSinh) ? null : _ttbx.First().MaTinhKhaiSinh.Trim();
                if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "24012")
                {
                    lupMaHuyenKhaiSinh.EditValue = string.IsNullOrEmpty(_ttbx.First().MaHuyenKhaiSinh) ? null : _ttbx.First().MaHuyenKhaiSinh.Trim();
                    lupXaKhaiSinh.EditValue = string.IsNullOrEmpty(_ttbx.First().MaXaKhaiSinh) ? null : _ttbx.First().MaXaKhaiSinh.Trim();
                }
                //txtDChiKSinh.Text = _ttbx.First().DchiKhaiSinh;
                txtHKKT.Text = _ttbx.First().HKTT;
                lupQuanHe.EditValue = string.IsNullOrEmpty(_ttbx.First().QuanHeNhanThan) ? null : _ttbx.First().QuanHeNhanThan.Trim();
                txtSoto.Text = _ttbx.First().SoTo;
                txtDiaChiNguoiThan.Text = _ttbx.First().DCNguoiThan;
            }
            if (bn.NoiTru != null)
            {
                switch (bn.NoiTru)
                {
                    case 0:
                        radNoiTru.SelectedIndex = 0;

                        break;

                    case 1:
                        radNoiTru.SelectedIndex = 1;
                        break;
                }
            }
            fillImage();
        }

        private void txtTimSTHE_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void txtNhapTBN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtNhapTBN.Text.Length < 40)
            {
                if (!Char.IsControl(e.KeyChar) && !Char.IsLetter(e.KeyChar) && !Char.IsPunctuation(e.KeyChar) &&
                    !Char.IsWhiteSpace(e.KeyChar))
                    e.Handled = true;
            }
            else
            {
                MessageBox.Show("Tên không hợp lệ!");
            }
        }

        private QLBV_Database.QLBVEntities _dataP = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private List<Person> _person = new List<Person>();

        //

        #region 3815

        public static string KTLSKCB(GiamDinhBHXH.BHXH_Model.ApiTheBHYT2018 sthe, GiamDinhBHXH.BHXH_Model.theBHYT sthecu, int _status)//_status 0: trả về ls,1trar về tt thẻ
        {
            string mota = "";
            try
            {
                if (!string.IsNullOrEmpty(DungChung.Bien.xmlFilePath_LIS[10]) && !string.IsNullOrEmpty(DungChung.Bien.xmlFilePath_LIS[11]))
                {
                    // lấy lịch sử KCB trên toàn quốc
                    GiamDinhBHXH.BHXH_Model.KQNhanLichSuKCBBS rp = GiamDinhBHXH.BHXH_Model.Service.NhanLichSuKCBBS_CV366(DungChung.Bien.xmlFilePath_LIS[10], DungChung.Bien.xmlFilePath_LIS[11], sthe);
                    if (rp != null && !string.IsNullOrEmpty(rp.maKetQua))
                    {
                        if (_status == 0)
                        {
                            //mota = rp.maKetQua;
                            string makq = rp.maKetQua;
                            if (makq.Contains("000"))
                            {
                                if (rp.maDKBD != sthecu.maCSKCB)
                                {
                                    mota += "Nhập sai cơ sở KCB, mã đúng: " + rp.maDKBD + "\n";
                                }
                                if (rp.gtTheTu != sthecu.ngayBD)
                                {
                                    mota += "Nhập sai hạn từ, hạn từ đúng: " + rp.gtTheTu + "\n";
                                }
                                if (rp.gtTheDen != sthecu.ngayKT)
                                {
                                    mota += "Nhập sai hạn đến, hạn đến đúng: " + rp.gtTheDen + "\n";
                                }
                                if (rp.gioiTinh.ToLower() != (sthecu.gioiTinh == 1 ? "nam" : "nữ"))
                                {
                                    mota += "Nhập sai giới tính, giới tính đúng: " + rp.gioiTinh + "\n";
                                }
                                //if (rp.diaChi.ToLower() != sthecu.d)
                                //{
                                //    mota += "Nhập sai ngày sinh, ngày sinh đúng: " + rp.ngaySinh + "\n";
                                //}
                                if (rp.gtTheDen != null)
                                {
                                    mota += "Giới hạn thẻ đến ngày: " + rp.gtTheDen;
                                }
                            }
                            else if (makq.Contains("004"))
                            {
                                mota = rp.maKetQua + "\nMã thẻ cũ: " + rp.maTheCu + "\nMã thẻ mới: " + rp.maTheMoi + "\nHạn thẻ mới: " + rp.gtTheDenMoi;
                            }
                            else if (makq.Contains("003"))
                            {
                                mota = rp.maKetQua + "\nMã thẻ cũ: " + rp.maTheCu + "\nMã thẻ mới: " + rp.maTheMoi + "\nHạn thẻ mới: " + rp.gtTheDenMoi;
                            }
                            else { mota = rp.maKetQua; }
                        }
                        else
                        {
                            mota = rp.ghiChu;
                        }
                    }
                }

                return mota;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion 3815

        #region 917

        //public static string KTLSKCB(GiamDinhBHXH.BHXH_Model.theBHYT sthe)
        //{
        //    List<GiamDinhBHXH.BHXH_Model.LichSuKCB> lLichSuKCB = new List<GiamDinhBHXH.BHXH_Model.LichSuKCB>();
        //    GiamDinhBHXH.BHXH_Model.HoSoKCBChiTiet lLichSuKCBct = new GiamDinhBHXH.BHXH_Model.HoSoKCBChiTiet();

        //    string mota = "";
        //    try
        //    {
        //        // lấy lịch sử KCB trên toàn quốc

        //        string loi = "",loict="";

        //        GiamDinhBHXH.BHXH_Model.Service.NhanLichSuKCB(DungChung.Bien.xmlFilePath_LIS[10], DungChung.Bien.xmlFilePath_LIS[11], sthe, ref lLichSuKCB, ref loi);
        //        string mahs = lLichSuKCB.Count > 0 ? lLichSuKCB.First().maHoSo : "";
        //        GiamDinhBHXH.BHXH_Model.Service.NhanHoSoKCBChiTiet(DungChung.Bien.xmlFilePath_LIS[10], DungChung.Bien.xmlFilePath_LIS[11], mahs, ref lLichSuKCBct, ref loict);

        //        if (!string.IsNullOrEmpty(loi))
        //            switch (loi)
        //            {
        //                #region cũ
        //                case "00":
        //                    mota = "";
        //                    break;
        //                case "01":
        //                    mota = "Thẻ hết hạn sử dụng";
        //                    break;
        //                case "02":
        //                    mota = "KCB khi chưa đến hạn";
        //                    break;
        //                case "03":
        //                    mota = "Hết hạn thẻ khi chưa ra viện";
        //                    break;
        //                case "04":
        //                    mota = "Thẻ có giá trị khi đang nằm viện";
        //                    break;
        //                case "05":
        //                    mota = "Mã thẻ không có trong dữ liệu thẻ";
        //                    break;
        //                case "06":
        //                    mota = "Thẻ sai họ tên";
        //                    break;
        //                case "07":
        //                    mota = "Thẻ sai ngày sinh";
        //                    break;
        //                case "08":
        //                    mota = "Thẻ sai giới tính";
        //                    break;
        //                case "09":
        //                    mota = "Thẻ sai đăng ký KCB ban đầu";
        //                    break;
        //                #endregion
        //                case "000":
        //                    mota = "";
        //                    break;
        //                case "001":
        //                    mota = "Thẻ do Bộ Quốc Phòng quản lý, đề nghị kiểm tra thẻ và thông tin giấy tờ tùy thân";
        //                    break;
        //                case "002":
        //                    mota = "Thẻ do Bộ Công An quản lý, đề nghị kiểm tra thẻ và thông tin giấy tờ tùy thân";
        //                    break;
        //                case "010":
        //                    mota = "Thẻ hết hạn sử dụng";
        //                    break;
        //                case "051":
        //                    mota = "Mã thẻ không đúng";
        //                    break;
        //                case "052":
        //                    mota = "Mã tỉnh cấp thẻ (kí tự thứ 4,5 của mã thẻ) không đúng";
        //                    break;
        //                case "053":
        //                    mota = "Mã quyền lợi thẻ (ký tự thứ 3 của mã thẻ) không đúng";
        //                    break;
        //                case "050":
        //                    mota = "TKhông thấy thông tin thẻ bhyt";
        //                    break;
        //                case "060":
        //                    mota = "Thẻ sai họ tên";
        //                    break;
        //                case "070":
        //                    mota = "Thẻ sai ngày sinh";
        //                    break;

        //                case "080":
        //                    mota = "Thẻ sai giới tính";
        //                    break;
        //                case "090":
        //                    mota = "Thẻ sai nơi đăng ký KCB ban đầu";
        //                    break;
        //                case "100":
        //                    mota = "Lỗi khi lấy dữ liệu số thẻ";
        //                    break;
        //                case "101":
        //                    mota = "Lỗi server";
        //                    break;
        //                case "110":
        //                    mota = "Thẻ đã thu hồi";
        //                    break;
        //                case "120":
        //                    mota = "Thẻ đã báo giảm";
        //                    break;
        //                case "121":
        //                    mota = "Thẻ đã báo giảm. Giảm chuyển ngoại tỉnh";
        //                    break;
        //                case "122":
        //                    mota = "Thẻ đã báo giảm. Giảm chuyển nội tỉnh";
        //                    break;
        //                case "123":
        //                    mota = "Thẻ đã báo giảm. Thu hồi do tăng lại cùng đơn vị";
        //                    break;

        //                case "124":
        //                    mota = "Thẻ đã báo giảm. Ngừng tham gia";
        //                    break;

        //                case "130":
        //                    mota = "Trẻ em không xuất trình th";
        //                    break;

        //            }
        //        return mota;
        //    }
        //    catch
        //    {
        //        return mota;
        //    }
        //}

        #endregion 917

        private bool kiemtralichsuKCB(int idps, int action)
        {
            string kt = ktrathongtinLS(0);
            if (!string.IsNullOrEmpty(kt))
            {
                DialogResult result = MessageBox.Show(kt + ". Bạn muốn nhập tiếp?", "check thông tin GĐBH", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            if (idps <= 0)
                return true;

            //Thêm mới mới check
            if (action == 1 && DungChung.Bien.MaBV != "30372")
            {
                var lsu1 = (from bn in _dataP.BenhNhans.Where(p => p.IDPerson == idps)
                            join rv in _dataP.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                            from b in kq.DefaultIfEmpty()
                            join tt in _dataP.VienPhis on bn.MaBNhan equals tt.MaBNhan
                          into kq2
                            from b2 in kq2.DefaultIfEmpty()
                            select new { chuara = b == null ? true : false, idVienPhi = b2 == null ? -1 : b2.idVPhi, NgayTT = b2 == null ? null : b2.NgayTT, MaKP = b == null ? bn.MaKP : b.MaKP, NgayRa = b == null ? null : b.NgayRa, ChanDoan = b == null ? "" : b.ChanDoan, bn.MaBNhan, bn.NNhap }).ToList();
                var lsu = (from a in lsu1
                           join b in _lkp on a.MaKP equals b.MaKP
                           select new
                           {
                               a.chuara,
                               a.NgayTT,
                               TenKP = b.TenKP,
                               NgayRa = a.NgayRa,
                               ChanDoan = a.ChanDoan,
                               a.MaBNhan,
                               a.NNhap,
                           }).OrderByDescending(p => p.NNhap).ToList();
                grcLichSuKCB.DataSource = lsu.ToList();
                if (lsu.Count > 0)
                {
                    if (lsu.First().chuara)
                    {
                        MessageBox.Show("Bệnh nhân đang điều trị, chưa ra viện, bạn không thể nhập");
                        return false;
                    }
                    else if (lsu.First().NgayTT == null)
                    {
                        if (DialogResult.No == MessageBox.Show("Bệnh nhân đang điều trị, chưa thanh toán \n Bạn vẫn muốn nhập", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private bool kiemtralichsuKCBBNMT(string sohsmantinh)
        {
            if (sohsmantinh != "" && sohsmantinh != null)
            {
                var lsu1 = (from ttbx in _dataP.TTboXungs.Where(p => p.So_eTBM == sohsmantinh)
                            join bn in _dataP.BenhNhans on ttbx.MaBNhan equals bn.MaBNhan
                            join rv in _dataP.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                            from b in kq.DefaultIfEmpty()
                            join vp in _dataP.VienPhis on bn.MaBNhan equals vp.MaBNhan into kq2
                            from b2 in kq2.DefaultIfEmpty()
                            select new { chuara = b == null ? true : false, idVienPhi = b2 == null ? -1 : b2.idVPhi, NgayTT = b2 == null ? null : b2.NgayTT, MaKP = b == null ? bn.MaKP : b.MaKP, NgayRa = b == null ? null : b.NgayRa, ChanDoan = b == null ? "" : b.ChanDoan, bn.MaBNhan, bn.NNhap }).ToList();
                var lsu = (from a in lsu1
                           join b in _lkp on a.MaKP equals b.MaKP
                           select new
                           {
                               a.chuara,
                               a.NgayTT,
                               TenKP = b.TenKP,
                               NgayRa = a.NgayRa,
                               ChanDoan = a.ChanDoan,
                               a.MaBNhan,
                               a.NNhap,
                           }).OrderByDescending(p => p.NNhap).ToList();
                grcLichSuKCB.DataSource = lsu.ToList();
                if (lsu.Count > 0)
                {
                    if (lsu.First().chuara)
                    {
                        MessageBox.Show("Bệnh nhân đang điều trị, chưa ra viện, bạn không thể nhập");
                        return false;
                    }
                    else if (lsu.First().NgayTT == null)
                    {
                        if (DialogResult.No == MessageBox.Show("Bệnh nhân đang điều trị, chưa thanh toán \n Bạn vẫn muốn nhập", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private int idperson = 0;
        private string personCode = "";
        private bool process = false;

        public void loadTTBS(string sthe)
        {
            if (sthe.Length < 10)
                return;
            var ttbsung = (from bv in _dataContext.BenhNhans.Where(p => p.SThe == sthe)
                           join ttbx in _dataContext.TTboXungs on bv.MaBNhan equals ttbx.MaBNhan
                           select ttbx).OrderByDescending(p => p.MaBNhan).FirstOrDefault();
            if (ttbsung != null)
            {
                lupMaTinh.EditValue = ttbsung.MaTinh;
                lupMaHuyen.EditValue = ttbsung.MaHuyen;
                lupMaXa.EditValue = ttbsung.MaXa;
                txtThonPho.Text = ttbsung.ThonPho;
                lupDantoc.EditValue = ttbsung.MaDT;
                cboQuocTich.Text = ttbsung.NgoaiKieu;
                lupNgheNghiep.EditValue = ttbsung.MaNN;
                txtNoiLV.Text = ttbsung.NoiLV;
                txtSoDT.Text = ttbsung.DThoai;
                txtNThan.Text = ttbsung.NThan;
                txtDTNThan.Text = ttbsung.DThoaiNT;
                txtDiaChiNguoiThan.Text = ttbsung.DCNguoiThan;
                if (ttbsung.HoTenBoMe != null && ttbsung.HoTenBoMe != "")
                {
                    string[] strS = ttbsung.HoTenBoMe.Split(';');
                    txtHoTenBo.Text = strS[0];
                    txtHoTenMe.Text = strS[1];
                }
                txtSoKSinh_CMT.Text = ttbsung.SoKSinh;
                if (ttbsung.NgayCapCMT != null && ttbsung.NgayCapCMT.Value.Year > 1000)
                    dtNgayCap.DateTime = ttbsung.NgayCapCMT.Value;
                lup_CCap.EditValue = ttbsung.ID_CB;
                lupCVu.EditValue = ttbsung.ID_CV;
                if (ttbsung.MaTinhKhaiSinh != null)
                    lupTinhKhaiSinh.EditValue = ttbsung.MaTinhKhaiSinh.Trim();
                if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "24012")
                {
                    if (ttbsung.MaXaKhaiSinh != null)
                        lupXaKhaiSinh.EditValue = ttbsung.MaXaKhaiSinh.Trim();
                    if (ttbsung.MaHuyenKhaiSinh != null)
                        lupMaHuyenKhaiSinh.EditValue = ttbsung.MaHuyenKhaiSinh.Trim();
                }
                //txtDChiKSinh.Text = ttbsung.DchiKhaiSinh;
                txtHKKT.Text = ttbsung.HKTT;
            }
        }

        private void txtTimSTHE_Leave(object sender, EventArgs e)
        {
            string ketquaMoi = string.Empty;
            string ketquaCu = string.Empty;
            string macsCu = string.Empty;
            string macsMoi = string.Empty;
            string hanTuMoi = string.Empty;
            string hanDenMoi = string.Empty;
            string hanTuCu = string.Empty;
            string hanDenCu = string.Empty;
            string ngayDu5Nam = string.Empty;
            string ngayDu5NamMoi = string.Empty;

            if (!string.IsNullOrEmpty(DungChung.Bien.xmlFilePath_LIS[10]) && !string.IsNullOrEmpty(DungChung.Bien.xmlFilePath_LIS[11]))
            {
                ApiTheBHYT2018 _sthe = new ApiTheBHYT2018();
                _sthe.maThe = _ketqua.ToUpper();
                _sthe.hoTen = txtNhapTBN.Text;
                _sthe.ngaySinh = txtNamSinh.Text;
                ApiToken _token = new ApiToken();
                _token.username = DungChung.Bien.xmlFilePath_LIS[10];
                _token.passwword = QLBV_Library.QLBV_Ham.MaHoa(DungChung.Bien.xmlFilePath_LIS[11]);
                KQNhanLichSuKCBBS n = NhanLSKCBNew_CV366(_token, _sthe);

                if (n != null)
                {
                    ketquaMoi = n.maTheMoi;
                    ketquaCu = n.maTheCu;
                    macsCu = n.maDKBD;
                    macsMoi = n.maDKBDmoi;
                    hanTuCu = n.gtTheTu;
                    hanDenMoi = n.gtTheDen;
                    hanTuMoi = n.gtTheTuMoi;
                    hanDenMoi = n.gtTheDenMoi;
                    ngayDu5Nam = n.ngayDu5Nam;
                    ngayDu5NamMoi = n.ngayDu5NamMoi;
                }
            }

            string muc = "", _dtuong = "";
            _ketqua = "";

            if (chkMauTheMoi.Checked)
            {
                if (!string.IsNullOrEmpty(txtTimSTHE_New.Text) && txtTimSTHE_New.Text.Length == 10)
                {
                    _ketqua += txtTimSTHE_New.Text;
                }
            }
            else if (!string.IsNullOrEmpty(txtTimSTHE.Text) && txtTimSTHE.Text.Length == 20)
            {
                string chuoi_dau = txtTimSTHE.Text;
                string[] chuoi_tach = chuoi_dau.Split(new Char[] { '-' });

                foreach (string s in chuoi_tach)
                {
                    if (s.Trim() != "")
                        _ketqua += s;
                }
                if (_ketqua.Length == 15)
                {
                    // mức hưởng
                    muc = _ketqua.ToString().Substring(2, 1);
                    _dtuong = _ketqua.ToString().Substring(0, 2);
                    _person = (_dataP.People.Where(p => p.SThe.Contains(_ketqua)).OrderBy(a => a.TenBNhan)).ToList();

                    if (_person.Count > 0)
                    {
                        // set gia tri

                        if (DungChung.Bien.MaBV == "30004" && _ketqua.Contains("TE") && _ketqua.Contains("KT"))
                        {
                        }
                        else
                        {
                            idperson = _person.First().IDPerson;
                            personCode = _person.First().PersonCode;
                            txtMaBN.Text = DungChung.Bien.MaBV == "30007" ? _person.First().PersonCode : _person.First().IDPerson.ToString();
                            txtNhapTBN.Text = _person.First().TenBNhan;

                            if (_person.First().NSinh != null)
                                txtNamSinh.Text = _person.First().NSinh.ToString();
                            string NS = "", TS = "";
                            if (_person.First().NgaySinh != null)
                                NS = _person.First().NgaySinh.ToString();
                            if (_person.First().ThangSinh != null)
                                TS = _person.First().ThangSinh.ToString();
                            txtNgaySinh.Text = (NS.Trim().Length > 0 && NS.Trim().Length < 2) ? ("0" + NS.ToString()) : NS.ToString();
                            txtThangSinh.Text = (TS.Trim().Length < 2 && TS.Trim().Length > 0) ? ("0" + TS.ToString()) : TS.ToString();
                            if (_person.First().NgayHM != null)
                                dtDu5nam.DateTime = _person.First().NgayHM.Value;
                            if (_person.First().KhuVuc != null)
                                cboKhuVuc.Text = _person.First().KhuVuc;
                            if (_person.First().HanBHDen != null)
                                dtHanBHden.DateTime = _person.First().HanBHDen.Value;
                            else
                                dtHanBHden.EditValue = null;
                            if (_person.First().HanBHTu != null)
                                dtHanBHTu.DateTime = _person.First().HanBHTu.Value;
                            else
                                dtHanBHTu.EditValue = null;
                            txtDiaChi1.Text = txtDiaChi.Text = _person.First().DChi;
                            if (!string.IsNullOrWhiteSpace(_person.First().MaCS))
                                txtMaCS.Text = _person.First().MaCS.Trim();
                            else
                                txtMaCS.Text = "";
                            if (_person.First().GTinh.ToString() == "1")
                            {
                                radNamNu.SelectedIndex = 0;
                            }
                            else
                            {
                                radNamNu.SelectedIndex = 1;
                            }
                            if (txtTimSTHE.Text == ketquaCu)
                            {
                                dtHanBHTu.DateTime = Convert.ToDateTime(hanTuCu);
                                dtHanBHden.DateTime = Convert.ToDateTime(hanDenCu);
                                txtMaCS.Text = macsCu;
                                dtDu5nam.DateTime = Convert.ToDateTime(ngayDu5Nam);
                            }

                            if (txtTimSTHE.Text == ketquaMoi)
                            {
                                dtHanBHTu.DateTime = Convert.ToDateTime(hanTuMoi);
                                dtHanBHden.DateTime = Convert.ToDateTime(hanDenMoi);
                                txtMaCS.Text = macsMoi;
                                dtDu5nam.DateTime = Convert.ToDateTime(ngayDu5NamMoi);
                            }
                            cboKhuVuc.Text = _person.First().KhuVuc;
                            // hiển thông tin bổ xung
                            lupMaTinh.EditValue = _person.First().MaTinh;
                            lupMaHuyen.EditValue = _person.First().MaHuyen;
                            lupMaXa.EditValue = _person.First().MaXa;
                            if (!string.IsNullOrEmpty(_person.First().FileAnh))
                            {
                                ptPhoto.Image = Image.FromFile(_person.First().FileAnh);
                            }
                            else
                                ptPhoto.Image = null;
                            //
                            //load ttbs
                            if (DungChung.Bien.MaBV == "26007")
                            {
                                if (_person.First().GhiChu != null && _person.First().GhiChu.Trim().Contains("THA"))
                                {
                                    KtraBNTHA = true;
                                }
                            }
                            loadTTBS(_ketqua);
                            //
                        }
                        int muccu = 0;
                        if (!string.IsNullOrEmpty(muc))
                            muccu = Convert.ToInt32(muc);
                        var dt = _dataContext.DTuongs.Where(p => p.MaDTuong == (_dtuong)).Where(p => p.MucCu == muccu).Select(p => p.MaMuc).ToList();
                        if (dt.Count > 0 && dt.First() != null)
                            lupMHuong.EditValue = dt.First().ToString().Trim();
                        else
                        {
                            lupMHuong.EditValue = muc;
                        }
                        txtMaCS.Focus();
                    }
                    else
                    {
                        if (DungChung.Bien.MaBV == "30004" && txtTimSTHE.Text.Substring(0, 2).ToUpper() == "TE")
                        {
                        }
                        else
                        {
                            txtMaCS.Text = setStheTE(txtTimSTHE.Text, dtNgayN.DateTime)[1];
                        }
                        int muccu = -1;
                        if (!string.IsNullOrEmpty(muc))
                            muccu = Convert.ToInt32(muc);
                        var dt = _dataContext.DTuongs.Where(p => p.MaDTuong == (_dtuong)).Where(p => p.MucCu == muccu).Select(p => p.MaMuc).ToList();
                        if (dt.Count > 0 && dt.First() != null)
                            lupMHuong.EditValue = dt.First().ToString().Trim();
                        else
                        {
                            lupMHuong.EditValue = muc;
                        }
                        txtMaCS_Leave(null, null);
                    }
                }
                else
                {
                    MessageBox.Show("Mã thẻ không hợp lệ! ...");
                    txtTimSTHE.Focus();
                }
            }
        }

        #region cảnh báo bệnh nhân vào viện lại trong 24 giờ - 26007

        /// <summary>
        ///
        /// </summary>
        /// <returns>tru: bệnh nhân vào viện trong vòng 24 giờ; false: bệnh nhân vào viện sau 24h</returns>
        private bool KtraThoiGianVaoVien()
        {
            bool kt = false;
            if (DungChung.Bien.MaBV == "26007" && dtNgayN.DateTime != null && !string.IsNullOrEmpty(_ketqua))
            {
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

                var qbnhan = data.BenhNhans.Where(p => p.SThe == _ketqua).OrderByDescending(p => p.MaBNhan).FirstOrDefault();
                if (qbnhan != null)
                {
                    var qravien = data.RaViens.Where(p => p.MaBNhan == qbnhan.MaBNhan).FirstOrDefault();
                    if (qravien != null)
                    {
                        if ((dtNgayN.DateTime - qravien.NgayRa.Value).TotalHours < 24)
                        {
                            // MessageBox.Show("Bệnh nhân mới ra viện trong vòng 24 giờ.");
                            kt = true;
                        }
                    }
                }
            }
            return kt;
        }

        #endregion cảnh báo bệnh nhân vào viện lại trong 24 giờ - 26007

        private void txtMaCS_Leave(object sender, EventArgs e)
        {
            QLBVEntities _data = new QLBVEntities(DungChung.Bien.StrCon);
            int hangbv = DungChung.Ham.hangBV(DungChung.Bien.MaBV);
            int hangbvDK = -1;
            string mahuyenDK = "-1";
            string mahuyenKB = "-2";
            string mabvgt = "";
            int csKCB_BanDau = 0;
            int csKCB_Serial = 0;
            if (lupMaBVgt.EditValue != null)
                mabvgt = lupMaBVgt.EditValue.ToString();
            radTuyen.Properties.ReadOnly = false;

            if (!string.IsNullOrEmpty(txtMaCS.Text) && (!string.IsNullOrEmpty(txtTimSTHE.Text) || !string.IsNullOrEmpty(txtTimSTHE_New.Text)))
            {
                if (txtMaCS.Text.Length < 5 || txtMaCS.Text.Length > 5)
                {
                    MessageBox.Show("Mã CS không hợp lệ");
                    txtMaCS.Focus();
                }
                else
                {
                    if (DungChung.Bien.MaBV == "24012")
                    {
                        if (txtMaCS.Text != "24012")
                        {
                            //if (string.IsNullOrEmpty(mabvgt))
                            //    radTuyen.SelectedIndex = 1;
                            //if (txtMaCS.Text.Substring(0, 2) == "24")
                            //{
                            //    radTuyen.SelectedIndex = 0;
                            //}
                            //else if(!string.IsNullOrEmpty(mabvgt))
                            //    radTuyen.SelectedIndex = 0;
                            //else
                            //    radTuyen.SelectedIndex = 1;
                            
                                if (string.IsNullOrEmpty(mabvgt))
                                    radTuyen.SelectedIndex = 1;
                                else
                                    radTuyen.SelectedIndex = 0;

                                DialogResult _result = MessageBox.Show("Thẻ không được đăng ký tại BV PHCN Bắc Giang bạn có muốn tiếp tục?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (_result == DialogResult.No)
                            {
                                txtTenBV.Text = "";
                                txtTimSTHE.Text = "";
                                this.txtMaCS.Text = "";
                                return;
                            }
                            if (_result == DialogResult.Yes)
                            {
                                return;
                            }
                        }

                    }
                    var que = (from CSDK in _data.BenhViens.Where(p => p.MaBV == txtMaCS.Text) select CSDK).ToList();
                    if (que.Count > 0)
                    {
                        string maChuQuan_maCS = que.FirstOrDefault().MaChuQuan;
                        string tuyenBenhVien_maCS = que.FirstOrDefault().TuyenBV.Trim();

                        var thongTinBenhVien_serial = _data.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV).ToList();
                        string maChuQuan_MaBVSerial = thongTinBenhVien_serial.FirstOrDefault().MaChuQuan;
                        string tuyenBenhVien_srerial = thongTinBenhVien_serial.FirstOrDefault().TuyenBV.Trim();

                        txtTenBV.Text = que.First().TenBV;
                        if (que.First().MaHuyen != null)
                            mahuyenDK = que.First().MaHuyen.Trim();
                        var abc = que.Where(p => p.MaBV == DungChung.Bien.MaBV).Select(p => p.MaHuyen).FirstOrDefault();
                        if (abc != null)
                            mahuyenKB = abc.Trim();
                        string matinh = DungChung.Bien.MaBV.Substring(0, 2);
                        hangbvDK = DungChung.Ham.hangBV(txtMaCS.Text);
                        string ma = txtMaCS.Text;
                        string MaChuQuan = que.First().MaChuQuan.ToString().Trim();

                        if (tuyenBenhVien_srerial == "C")
                        {
                            int tt = DungChung.Ham.KTraTTHuyen(_data, _ketqua.Substring(3, 2), DungChung.Bien.MaBV, txtMaCS.Text, tuyenBenhVien_maCS, tuyenBenhVien_srerial);
                            if (tt == -10)
                            {
                                resetControl();
                                this.Dispose();
                            }
                            int noiNgoaiTinh = DungChung.Ham.KTraNoiNgoaiTinh(_ketqua.Substring(3, 2), DungChung.Bien.MaBV, txtMaCS.Text, tuyenBenhVien_maCS, tuyenBenhVien_srerial);

                            //_noitinh = noiNgoaiTinh;
                            lupNoiNgoaiTinh.EditValue = noiNgoaiTinh;
                            if (cboCapCuu.SelectedIndex == 1)
                                radTuyen.SelectedIndex = 0;

                            switch (tt)
                            {
                                case 1:
                                    radTuyen.SelectedIndex = 0;
                                    break;

                                case 3:
                                    radTuyen.SelectedIndex = 1;
                                    break;

                                case 4:
                                    radTuyen.SelectedIndex = 2;
                                    break;
                            }
                            return;
                        }
                        if (tuyenBenhVien_srerial == "D")
                        {
                            int tt = DungChung.Ham.KTraTTXa(_data, _ketqua.Substring(3, 2), DungChung.Bien.MaBV, txtMaCS.Text, tuyenBenhVien_maCS, tuyenBenhVien_srerial);
                            if (tt == -10)
                            {
                                resetControl();
                                this.Dispose();
                            }
                            int noiNgoaiTinh = DungChung.Ham.KTraNoiNgoaiTinh(_ketqua.Substring(3, 2), DungChung.Bien.MaBV, txtMaCS.Text, tuyenBenhVien_maCS, tuyenBenhVien_srerial);

                            //_noitinh = noiNgoaiTinh;
                            lupNoiNgoaiTinh.EditValue = noiNgoaiTinh;
                            if (cboCapCuu.SelectedIndex == 1)
                                radTuyen.SelectedIndex = 0;

                            switch (tt)
                            {
                                case 1:
                                    radTuyen.SelectedIndex = 0;
                                    break;

                                case 3:
                                    radTuyen.SelectedIndex = 1;
                                    break;

                                case 4:
                                    radTuyen.SelectedIndex = 2;
                                    break;
                            }
                            return;
                        }

                        // cảnh báo bệnh viện tuyến tỉnh khi khám tại bệnh viện tuyến huyện
                        if (hangbvDK == 2 && hangbvDK < hangbv && DungChung.Bien.MaBV == "27001")
                        {
                            DialogResult _result = MessageBox.Show(que.First().TenBV + ": tương đương tuyến tỉnh, bạn vẫn muốn nhập? ", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (_result == DialogResult.No)
                            {
                                this.frmHSBNNhapMoi_Load(sender, e);
                                return;
                            }
                        }
                        string macqBVKB = "";
                        var a = _lBenhVien.Where(p => p.MaBV == DungChung.Bien.MaBV).Select(p => p.MaChuQuan).FirstOrDefault();
                        if (a != null)
                            macqBVKB = a.Trim();

                        var que2 = (from CSDK in que.Where(p => p.MaBV == MaChuQuan) select CSDK).FirstOrDefault();
                        string MachuQuancq = "";
                        if (que2 != null)
                            MachuQuancq = que2.MaChuQuan == null ? "" : que2.MaChuQuan.ToString().Trim();

                        #region trùng mã tỉnh

                        if (matinh == _ketqua.Substring(3, 2))
                        {
                            if (matinh == ma.Substring(0, 2) && (DungChung.Bien.MaBV != "20001"))
                            {
                                radTuyen.SelectedIndex = 0;
                            }
                            else
                                radTuyen.SelectedIndex = 1;
                            // radTuyen.Properties.ReadOnly = true;
                            if (ma == DungChung.Bien.MaBV)
                            {
                                radTuyen.SelectedIndex = 0;
                                if (DungChung.Bien.MaBV == "30003" && _ketqua.Substring(0, 2).ToLower() == "tq" || _ketqua.Substring(0, 2).ToLower() == "ta")
                                {
                                    _noitinh = 1;
                                }
                                else
                                {
                                    if (_ketqua.Substring(5, 2) == DungChung.Bien.MaHuyen)
                                    {
                                        _noitinh = 1;
                                    }
                                    else
                                    {
                                        _noitinh = 1;
                                    }
                                }
                            }
                            else
                            {
                                if (MaChuQuan == DungChung.Bien.MaBV || MachuQuancq == DungChung.Bien.MaBV)
                                {
                                    lupMaBVgt.Enabled = true;
                                    txtCDNoiGT.Enabled = true;
                                    radTuyen.SelectedIndex = 0;
                                    //MessageBox.Show(que.First().MaBV);
                                    //lupMaBVgt.EditValue = que.First().MaBV;

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
                                    if (DungChung.Bien.MaBV == "04018")
                                    {
                                        if (que.First().status == 2)
                                        {
                                            DialogResult result;
                                            result = MessageBox.Show("Bệnh nhân có giấy giới thiệu?", "BN trong huyện.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                            if (result == DialogResult.Yes)
                                            {
                                                lupMaBVgt.Enabled = true;
                                                txtCDNoiGT.Enabled = true;
                                                radTuyen.SelectedIndex = 0;
                                                //MessageBox.Show(que.First().MaBV);
                                                lupMaBVgt.EditValue = que.First().MaBV;
                                                _noitinh = 2;
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
                                        int Tuyen_serial = TuyenBenhVien(tuyenBenhVien_srerial.TrimEnd());
                                        int Tuyen_CS = TuyenBenhVien(tuyenBenhVien_maCS.TrimEnd());
                                        if ((Tuyen_CS == 2 || Tuyen_CS == 3 || Tuyen_CS == 1))
                                        {
                                            radTuyen.SelectedIndex = (Tuyen_CS > Tuyen_serial ? 1 : Tuyen_CS == Tuyen_serial ? 0 : 0);
                                            if (DungChung.Bien.MaBV == "01071")
                                                radTuyen.SelectedIndex = 1;
                                        }
                                        else
                                        {
                                            radTuyen.SelectedIndex = 0;
                                        }

                                        if (Tuyen_CS == 0 && DungChung.Bien.MaBV == "01071")
                                        {
                                            radTuyen.SelectedIndex = 1;
                                        }
                                        //if (((Tuyen_serial == 1 && Tuyen_CS == 2) || (Tuyen_serial == 2 && Tuyen_CS == 2)))
                                        //{
                                        //    radTuyen.SelectedIndex = 1;
                                        //}
                                        //if (((Tuyen_serial == 1 && Tuyen_CS == 0) ||(Tuyen_serial == 1 && Tuyen_CS == 1) || (Tuyen_serial == 2 && Tuyen_CS == 2)))
                                        //{
                                        //    radTuyen.SelectedIndex = 1;
                                        //}

                                        if (maChuQuan_maCS != maChuQuan_MaBVSerial)
                                        {
                                            _noitinh = 2;
                                        }
                                    }
                                    if (DungChung.Bien.MaBV == "30003" && _ketqua.Substring(0, 2).ToLower() == "tq" || _ketqua.Substring(0, 2).ToLower() == "ta")
                                    {
                                        _noitinh = 2;
                                    }
                                }
                            }
                            if ((hangbvDK > 2) && (macqBVKB == MaChuQuan || macqBVKB == MachuQuancq))
                                _noitinh = 1;

                            //if (DungChung.Bien.MaBV != txtMaCS.Text)
                            //{
                            //    radTuyen.SelectedIndex = 2;
                            //}

                            //string maTinhNoiKCB = DungChung.Bien.MaBV.Substring(0, 2);
                            //string maTinhNoiDK = txtMaCS.Text.Substring(0, 2);
                            //if (maTinhNoiKCB == maTinhNoiDK)
                            //{
                            //    if (DungChung.Bien.MaBV != txtMaCS.Text)
                            //    {
                            //        radTuyen.SelectedIndex = 2;
                            //    }
                            //}
                            //else
                            //{
                            //    radTuyen.SelectedIndex = 1;
                            //}

                            //if (csKCB_BanDau < csKCB_Serial)
                            //{
                            //    radTuyen.SelectedIndex = 1;
                            //}
                        }

                        #endregion trùng mã tỉnh

                        #region không trùng mã tỉnh giữa trên thẻ và serial

                        else
                        {
                            if (DungChung.Bien.MaBV == txtMaCS.Text)
                            {
                                radTuyen.SelectedIndex = 0;
                                _noitinh = 1;
                            }
                            else
                            {
                                int Tuyen_serial = TuyenBenhVien(tuyenBenhVien_srerial.TrimEnd());
                                int Tuyen_CS = TuyenBenhVien(tuyenBenhVien_maCS.TrimEnd());
                                if (Tuyen_CS == Tuyen_serial && Tuyen_serial != 1)
                                {
                                    radTuyen.SelectedIndex = 0;
                                }
                                else
                                {
                                    radTuyen.SelectedIndex = 1;
                                }

                                if (DungChung.Bien.MaBV == "01071")
                                {
                                    radTuyen.SelectedIndex = 1;
                                }
                                _noitinh = 3;
                            }
                            //string maTinhNoiKCB = DungChung.Bien.MaBV.Substring(0, 2);
                            //string maTinhNoiDK = txtMaCS.Text.Substring(0, 2);
                            //if (maTinhNoiKCB == maTinhNoiDK)
                            //{
                            //    if (DungChung.Bien.MaBV != txtMaCS.Text)
                            //    {
                            //        radTuyen.SelectedIndex = 2;
                            //    }
                            //}

                            //if (csKCB_BanDau < csKCB_Serial)
                            //{
                            //    radTuyen.SelectedIndex = 1;
                            //}
                        }

                        #endregion không trùng mã tỉnh giữa trên thẻ và serial

                        #region set nội tỉnh mã thẻ

                        string _maCQCQ = "";
                        var qCQCQ = que.Where(p => p.MaBV == DungChung.Bien.MaBV).FirstOrDefault();
                        if (qCQCQ != null)
                            _maCQCQ = qCQCQ.MaChuQuan;
                        if ((DungChung.Bien.MaBV == "12001" || _maCQCQ == "12001"))
                        {
                            if (_ketqua.Contains("TE") && _ketqua.Contains("KT"))
                                _noitinh = 2;
                        }

                        #endregion set nội tỉnh mã thẻ

                        lupNoiNgoaiTinh.EditValue = _noitinh;
                        if (cboCapCuu.SelectedIndex == 1)
                            radTuyen.SelectedIndex = 0;
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

        private int Tuyen_benhVien(string maCSKhamChuaBenhTrenTheBHYT)
        {
            if (!string.IsNullOrEmpty(maCSKhamChuaBenhTrenTheBHYT))
            {
                QLBVEntities _data = new QLBVEntities(DungChung.Bien.StrCon);
                string maCS_seri = DungChung.Bien.MaBV;
                var benhVien_trenThe = _data.BenhViens.Where(p => p.MaBV.Contains(maCSKhamChuaBenhTrenTheBHYT)).FirstOrDefault();
                var benhVien_serial = _data.BenhViens.Where(p => p.MaBV.Contains(maCS_seri)).FirstOrDefault();
                if (benhVien_trenThe.MaChuQuan == benhVien_serial.MaChuQuan)
                {
                    return 0; // Đúng tuyến
                }
                else
                {
                    if (benhVien_trenThe.MaChuQuan.Substring(0, 2) == benhVien_serial.MaChuQuan.Substring(0, 2))
                    {
                        if (TuyenBenhVien(benhVien_trenThe.TuyenBV) == 1 || TuyenBenhVien(benhVien_trenThe.TuyenBV) == 2 || TuyenBenhVien(benhVien_trenThe.TuyenBV) == 3)
                        {
                            // trong trường hợp từ bệnh viện tuyến trên xuống là trái tuyến còn chuyển lên trên là đúng tuyến
                            return TuyenBenhVien(benhVien_trenThe.TuyenBV) > TuyenBenhVien(benhVien_serial.TuyenBV) ? 1 : (TuyenBenhVien(benhVien_trenThe.TuyenBV) == TuyenBenhVien(benhVien_serial.TuyenBV) ? 0 : 0);
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            else
            {
                return -1;
            }
        }

        private int TuyenBenhVien(string Tuyen)
        {
            switch (Tuyen)
            {
                case "A":
                    return 3;

                case "B":
                    return 2;

                case "C":
                    return 1;

                default:
                    return 0;
            }
        }

        private void txtMaCS_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (txtMaCS.Text.Length == 5)
            //{
            //    dtHanBHTu.Focus();
            //}
        }

        private void cboDTuong_EditValueChanged(object sender, EventArgs e)
        {
            int _iddt = -1;
            int _httt = -1;
            string[] mota = new string[] { };
            string hthuc = "";
            _ketqua = "";
            if (cboDTuong.EditValue != null)
                _iddt = Convert.ToByte(cboDTuong.EditValue);
            if (_ldtbn.Where(p => p.IDDTBN == _iddt).ToList().Count > 0)
            {
                _httt = _ldtbn.Where(p => p.IDDTBN == _iddt).First().HTTT;
                if (_ldtbn.Where(p => p.IDDTBN == _iddt).First().MoTa != null)
                {
                    mota = _ldtbn.Where(p => p.IDDTBN == _iddt).First().MoTa.Split(';').ToArray();
                    if (mota.Length >= 3)
                    {
                        hthuc = mota[2].Trim();
                    }
                }
            }

            if (hthuc == "Khám tuyển")
            {
                GetHinhthuckham_KSK("ksk");
                radNoiTru.Visible = false;
                lupHinhThucKham.Visible = true;
            }
            else
            {
                GetHinhthuckham_KSK("bhyt");
                lupHinhThucKham.Visible = false;
                radNoiTru.Visible = true;
            }
            lupHinhThucKham.Properties.DataSource = HINH_THUC_KHAM_KSK;
            labelControl43.Text = "Hiện trạng:";
            cboCapCuu.Properties.Items.Clear();
            cboCapCuu.Properties.Items.Add("Thường");
            cboCapCuu.Properties.Items.Add("Cấp cứu");
            cboCapCuu.Properties.Items.Add("Tử vong");
            switch (_httt)
            {
                case 0:
                    EnableControl(false);
                    // radNoiTru.Visible = true;
                    labHinhthucKSK.Visible = false;
                    // cboKSK.Visible = false;
                    txtTrieuChung.Enabled = true;
                    lupNoiNgoaiTinh.Visible = false;
                    labNoiTinh.Visible = false;
                    chkNoThe.Visible = true;
                    ckcBoXungThe.Enabled = false;
                    break;

                case 1:
                    EnableControl(true);
                    // radNoiTru.Visible = true;
                    labHinhthucKSK.Visible = false;
                    // cboKSK.Visible = false;
                    txtTrieuChung.Enabled = true;
                    lupNoiNgoaiTinh.Visible = true;
                    labNoiTinh.Visible = true;
                    chkNoThe.Checked = false;
                    chkNoThe.Visible = false;
                    ckcBoXungThe.Enabled = true;

                    break;

                case 2:
                    EnableControl(false);
                    // radNoiTru.Visible = true;
                    labHinhthucKSK.Visible = false;
                    //  cboKSK.Visible = false;
                    txtTrieuChung.Enabled = true;
                    lupNoiNgoaiTinh.Visible = false;
                    labNoiTinh.Visible = false;
                    chkNoThe.Visible = true;
                    ckcBoXungThe.Enabled = false;
                    break;
            }

            if (cboDTuong.Text == "KSK")
            {
                EnableControl(false);
                if (DungChung.Bien.MaBV == "20001")
                {
                    radNoiTru.Visible = false;
                    lupHinhThucKham.Visible = true;
                }
                labHinhthucKSK.Visible = true;
                chk_CCCD.Visible = false;
                txtTrieuChung.Enabled = false;
                lupNoiNgoaiTinh.Visible = false;
                labNoiTinh.Visible = false;
                chkNoThe.Visible = false;
                labelControl43.Text = "KSK có CLS:";
                cboCapCuu.Properties.Items.Clear();
                cboCapCuu.Properties.Items.Add("không CLS");
                cboCapCuu.Properties.Items.Add("Có CLS");
            }

            if (cboDTuong.Text == "BHYT")
            {
                if (DungChung.Bien.MaBV == "30005")
                {
                    chkNoThe30005.Checked = false;
                }
                chk_CCCD.Visible = true;
            }

            if (_httt != 1)
            {
                radTuyen.SelectedIndex = -1;
            }

            cboCapCuu.SelectedIndex = 0;

            if (DungChung.Bien.MaBV == "30005" && TTLuu == 1)
                chkNoThe.Visible = false;
        }

        public static int _getSoTT(QLBV_Database.QLBVEntities _data, DateTime _dt, int _makp)
        {
            int _sott = 1;
            var maxSTT = _data.SoTTs.Where(p => p.NgayDK == _dt.Date && p.MaKP == _makp).ToList();
            if (maxSTT.Count > 0)
                _sott = maxSTT.Max(p => p.SoTT1) + 1;
            return _sott;
        }

        private int _KT_SoTT(QLBV_Database.QLBVEntities _data, DateTime _dt, int _makp, int _sott)
        {
            var maxSTT = _data.SoTTs.Where(p => p.NgayDK == _dt.Date && p.MaKP == _makp && p.SoTT1 == _sott).ToList();
            if (maxSTT.Count > 0)
                return _getSoTT(_data, _dt, _makp);
            return _sott;
        }

        public static bool _setSoTT(QLBV_Database.QLBVEntities _data, DateTime _dt, int _makp, int _sott, int _mabn)
        {
            try
            {
                var kt = _data.SoTTs.Where(p => p.NgayDK == _dt.Date && p.MaKP == _makp && p.SoTT1 == _sott).ToList();
                if (kt.Count <= 0)
                {
                    SoTT moi = new SoTT();
                    moi.NgayDK = _dt.Date;
                    moi.MaKP = _makp;
                    moi.SoTT1 = _sott;
                    moi.MaBNhan = _mabn;
                    _data.SoTTs.Add(moi);
                    _data.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void lupKhoaKham_EditValueChanged(object sender, EventArgs e)
        {
            if (dtNgayN.DateTime != null)
            {
                if (lupKhoaKham.EditValue != null)
                {
                    _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    DateTime dt = dtNgayN.DateTime.Date;
                    int mak = Convert.ToInt32(lupKhoaKham.EditValue);
                    txtSoTT.Text = _getSoTT(_dataContext, dt, mak).ToString();
                    if (DungChung.Bien.MaBV == "30010" && lupKhoaKham.Text.ToLower().Contains("cấp cứu"))
                    {
                        cboCapCuu.SelectedIndex = 1;
                    }
                    if (DungChung.Bien.MaBV != "30009")
                    {
                        //Tất cả các viện sẽ tích nội trú khi chọn kp lâm sàng
                        var LS = _dataContext.KPhongs.Where(p => p.PLoai == "Lâm sàng").Select(o => o.TenKP).ToList();

                        if (LS.Exists(o => o == lupKhoaKham.Text))
                        {
                            radNoiTru.SelectedIndex = 1;
                        }
                        else
                            radNoiTru.SelectedIndex = 0;
                    }
                }
            }

            if (TTLuu == 1 && DungChung.Bien.MaBV == "27021")
            {
                if (DungChung.Bien.PP_SoKB != 0 && _load > 0)
                    setSoKB();
            }
        }

        public static string TuoitheoThang(QLBV_Database.QLBVEntities _data, string ngay, string thang, string namsinh, string gioihan)
        {
            string tuoi = "";
            try
            {
                string[] _gioihan;
                _gioihan = gioihan.Split('-');
                int gioihanthang = 0, gioihanngay = 0;
                if (_gioihan.Length > 1)
                {
                    int.TryParse(_gioihan[0], out gioihanthang);
                    int.TryParse(_gioihan[1], out gioihanngay);
                }

                int _tuoi = DungChung.Ham.CalculateAgeByDate(ngay, thang, namsinh);
                tuoi = _tuoi.ToString();
                int _ngays = 1;
                int _thangs = 1;
                int _nams = 1900;
                if (string.IsNullOrEmpty(ngay))
                    _ngays = Convert.ToInt32(ngay);

                if (string.IsNullOrEmpty(thang))
                    _thangs = Convert.ToInt32(thang);

                if (string.IsNullOrEmpty(namsinh))
                    _nams = Convert.ToInt32(namsinh);
                int nam = DateTime.Now.Year - _nams;
                int thangtuoi = 0, ngaytuoi = 0;
                if (_ngays == 0)
                    _ngays = 1;
                if (_thangs == 0)
                    _thangs = 1;
                string a = _ngays + "/" + _thangs + "/" + _nams;
                DateTime _ngaysinh = Convert.ToDateTime(a);
                if (nam <= 0)
                {
                    thangtuoi = (DateTime.Now.Month - _thangs);
                }
                else
                {
                    thangtuoi = (DateTime.Now.Month - _thangs) + 12 * nam;
                }
                ngaytuoi = (DateTime.Now - _ngaysinh).Days;
                if (thangtuoi <= gioihanthang)
                {
                    if (ngaytuoi <= gioihanngay)
                    {
                        tuoi = ngaytuoi.ToString() + " ngày";
                    }
                    else
                    {
                        tuoi = thangtuoi.ToString() + " tháng";
                    }
                }
                else
                {
                    tuoi = _tuoi.ToString();
                }
                return tuoi;
            }
            catch (Exception)
            {
                return tuoi;
            }
        }

        private void txtNgaySinh_Leave(object sender, EventArgs e)
        {
            txtThangTuoi.Text = TuoitheoThang(_dataContext, txtNgaySinh.Text, txtThangSinh.Text, txtNamSinh.Text, DungChung.Bien.formatAge);
            if (!string.IsNullOrWhiteSpace(txtNamSinh.Text) && txtNamSinh.Text.Length == 4)
                txtTuoi.Text = DungChung.Ham.CalculateAgeByDate((!string.IsNullOrWhiteSpace(txtNgaySinh.Text) && !txtThangSinh.Text.Contains("_")) ? txtNgaySinh.Text : "01", (!string.IsNullOrWhiteSpace(txtThangSinh.Text) && !txtThangSinh.Text.Contains("_")) ? txtThangSinh.Text : "01", txtNamSinh.Text).ToString();
        }

        private void lupMaTinh_Leave(object sender, EventArgs e)
        {
        }

        private void txtNgaySinh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtTimSTHE.Text.Length == 5)
            {
                txtDiaChi.Focus();
                txtDiaChi1.Focus();
            }
        }

        private class TinhBN
        {
            public string TenTinh { get; set; }
        }

        private List<TinhBN> _lstt = new List<TinhBN>();

        private void txtMaCS_EditValueChanged(object sender, EventArgs e)
        {
            if (txtMaCS.Text.Length == 5)
            {
                _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var macq = _dataContext.BenhViens.Where(p => p.MaBV == txtMaCS.Text).Select(p => p.MaChuQuan).FirstOrDefault();
                var que = (from CSDK in _lBenhVien.Where(p => p.MaBV == txtMaCS.Text) select CSDK).ToList();
                if (que.Count > 0)
                {
                    txtTenBV.Text = que.First().TenBV;
                }
                if (DungChung.Bien.MaBV == "27001")
                {
                    if (txtMaCS.Text == DungChung.Bien.MaBV || macq == DungChung.Bien.MaBV)
                    {
                        radTuyen.SelectedIndex = 0;
                    }
                    else
                    {
                        radTuyen.SelectedIndex = 1;
                    }
                }
                else
                {
                    if (txtMaCS.Text == DungChung.Bien.MaBV)
                    {
                        radTuyen.SelectedIndex = 0;
                    }
                    else
                    {
                        if (DungChung.Bien.MaBV == "24297" && txtMaCS.Text.StartsWith("24"))
                        {
                            if (txtMaCS.Text.StartsWith("24"))
                            {
                                if (txtMaCS.Text == "24297")
                                {
                                    radTuyen.SelectedIndex = 0;
                                }
                                else
                                {
                                    radTuyen.SelectedIndex = 2;
                                }
                            }
                            else
                            {
                                radTuyen.SelectedIndex = 1;
                            }
                        }
                    }
                }
                if (DungChung.Bien.MaBV == "24272")
                {
                    string ma_cs = txtMaCS.Text;
                    //
                    var bv = _dataContext.BenhViens.Where(x => x.MaBV == ma_cs).FirstOrDefault();
                    if (bv == null)
                    {
                        MessageBox.Show($"Bệnh viện có {ma_cs} chưa có trong danh mục hệ thống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (ma_cs.Length >= 5)
                        {
                            if (!ma_cs.StartsWith("24"))
                            {
                                MessageBox.Show($"Bệnh nhân có nơi KCBBĐ là {ma_cs} nên không thể lưu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                resetControl();
                                this.Dispose();
                                return;
                            }
                            else if (ma_cs.StartsWith("24"))
                            {
                                if (bv.status == 4 || bv.status == null || bv.status.ToString().Length == 0)
                                {
                                    MessageBox.Show($"Bệnh nhân có nơi KCBBĐ là {ma_cs} nên không thể lưu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    resetControl();
                                    this.Dispose();
                                    return;
                                }
                                else
                                {
                                    if (ma_cs == "24272")
                                    {
                                        radTuyen.SelectedIndex = 0;
                                    }
                                    else
                                    {
                                        radTuyen.SelectedIndex = 2;
                                    }
                                }

                            }
                        }
                    }
                }
                else
                {
                    //string ma_cs = txtMaCS.Text;
                    //if (txtMaCS)
                    //{
                    //}
                }
                var bvkb = _dataContext.BenhViens.Where(p => p.MaBV.Equals(DungChung.Bien.MaBV)).FirstOrDefault();
                string diaChi = string.Empty;
                //string makcb = string.Empty;
                //if (!string.IsNullOrEmpty(DungChung.Bien.xmlFilePath_LIS[10]) && !string.IsNullOrEmpty(DungChung.Bien.xmlFilePath_LIS[11]))
                //{
                //    ApiTheBHYT2018 _sthe = new ApiTheBHYT2018();
                //    _sthe.maThe = _ketqua.ToUpper();
                //    _sthe.hoTen = txtNhapTBN.Text;
                //    _sthe.ngaySinh = txtNamSinh.Text;
                //    ApiToken _token = new ApiToken();
                //    _token.username = DungChung.Bien.xmlFilePath_LIS[10];
                //    _token.passwword = QLBV_Library.QLBV_Ham.MaHoa(DungChung.Bien.xmlFilePath_LIS[11]);
                //    KQNhanLichSuKCBBS n = NhanLSKCBNew_CV366(_token, _sthe);
                //    if (n != null)
                //    {
                //        diaChi = n.diaChi;
                //        if (!string.IsNullOrEmpty(n.maDKBD))
                //        {
                //            makcb = n.maDKBD.Substring(0, 2);
                //        }

                //        txtDiaChi.Text = diaChi;
                //    }
                //}

                string makcb = txtMaCS.Text;
                if (makcb != null && makcb.Length >= 2)
                    makcb = makcb.Substring(0, 2);

                var tinhBNhan = _dataContext.DmTinhs.Where(p => p.MaTinh == makcb).Select(x => x.TenTinh.ToLower()).FirstOrDefault();
                //var tinhBNhan = _dataContext.DmTinhs.Where(x => diaChi.ToLower().Contains(x.TenTinh.ToLower())).Select(x => x.TenTinh.ToLower()).FirstOrDefault();
                var tinhBVkb = _dataContext.DmTinhs.Where(x => bvkb.DiaChi.ToLower().Contains(x.TenTinh.ToLower())).Select(x => x.TenTinh.ToLower()).FirstOrDefault();
                if (DungChung.Bien.MaBV == "27001")
                {
                    if ((txtMaCS.Text == DungChung.Bien.MaBV || macq == DungChung.Bien.MaBV) && tinhBNhan == tinhBVkb)
                    {
                        lupNoiNgoaiTinh.EditValue = 1;
                    }
                    else if (tinhBNhan == tinhBVkb)
                    {
                        lupNoiNgoaiTinh.EditValue = 2;
                    }
                    else
                    {
                        lupNoiNgoaiTinh.EditValue = 3;
                    }
                }
                else
                {
                    if (DungChung.Bien.MaBV != "24272")
                    {
                        if (txtMaCS.Text == DungChung.Bien.MaBV && tinhBNhan == tinhBVkb)
                        {
                            lupNoiNgoaiTinh.EditValue = 1;
                        }
                        else if (tinhBNhan == tinhBVkb)
                        {
                            lupNoiNgoaiTinh.EditValue = 2;
                        }
                        else
                        {
                            lupNoiNgoaiTinh.EditValue = 3;
                        }
                    }
                    else
                    {
                        if (txtMaCS.Text == DungChung.Bien.MaBV && tinhBNhan == tinhBVkb)
                        {
                            lupNoiNgoaiTinh.EditValue = 1;
                        }
                        else if (tinhBNhan == tinhBVkb)
                        {
                            lupNoiNgoaiTinh.EditValue = 2;
                        }
                    }
                }
            }
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtNhapTBN_Leave(object sender, EventArgs e)
        {
            txtNhapTBN.Text = DungChung.Ham.ToFirstUpper(txtNhapTBN.Text.Trim());
            if (!string.IsNullOrEmpty(txtNhapTBN.Text.Trim()) && (DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "30372"))
            {
                var benhNhan = _dataContext.BenhNhans.Where(o => o.TenBNhan.Contains(txtNhapTBN.Text.Trim())).OrderByDescending(o => o.NNhap).ToList();
                if (benhNhan != null && benhNhan.Count > 0)
                {
                    frmHSBNChonBN frm = new frmHSBNChonBN(benhNhan, FillTTBNHoTen);
                    frm.ShowDialog();
                }
            }
        }

        private void txtDiaChi_Leave(object sender, EventArgs e)
        {
            txtDiaChi.Text = DungChung.Ham.ToFirstUpper(txtDiaChi.Text);
            txtDiaChi1.Text = DungChung.Ham.ToFirstUpper(txtDiaChi1.Text);
            cboCapCuu.Focus();
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            bool _NgoaiH = false;
            _NgoaiH = DungChung.Ham.CheckNGioHC(dtNgayN.DateTime);
            MessageBox.Show(_NgoaiH.ToString());
        }

        private void cboKSK_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void chkCapCuu_CheckedChanged(object sender, EventArgs e)
        {
            if (cboCapCuu.SelectedIndex == 1)
            {
                radTuyen.Enabled = false;
                radTuyen.SelectedIndex = 0;
            }
            else
            {
                radTuyen.Enabled = true;
                txtMaCS.Focus();
            }
        }

        private void txtTuoi_Leave(object sender, EventArgs e)
        {
            if (txtTuoi.EditValue != null)
            {
                int tuoi = 0;
                if (!string.IsNullOrEmpty(txtTuoi.Text))
                    tuoi = Convert.ToInt32(txtTuoi.Text.Trim());
                var now = DateTime.Now.AddYears(-tuoi);
                var ngaySinh = DungChung.Ham.ConvertNgay(DungChung.Ham.GhepNgaySinh("/", (!string.IsNullOrWhiteSpace(txtNgaySinh.Text) && !txtThangSinh.Text.Contains("_")) ? txtNgaySinh.Text : "01", (!string.IsNullOrWhiteSpace(txtThangSinh.Text) && !txtThangSinh.Text.Contains("_")) ? txtThangSinh.Text : "01", now.Year.ToString()));

                if (ngaySinh.Date > now.Date)
                    txtNamSinh.Text = (now.Year - 1).ToString();
                else
                    txtNamSinh.Text = now.Year.ToString();
            }
            else
                txtTuoi.EditValue = 0;
        }

        private void btnThemNN_Click(object sender, EventArgs e)
        {
            FormDanhMuc.Frm_Dm_NgheNghiep frm = new FormDanhMuc.Frm_Dm_NgheNghiep();
            frm.ShowDialog();
            var NN = _dataContext.DmNNs.OrderBy(p => p.TenNN).ToList();
            lupNgheNghiep.Properties.DataSource = NN;
        }

        private void lupNgheNghiep_Click(object sender, EventArgs e)
        {
        }

        private string _matinh = "", _mahuyen = "", _maxa = "";

        private void btnTinh_Click(object sender, EventArgs e)
        {
            FormDanhMuc.Frm_DmTinh frm = new FormDanhMuc.Frm_DmTinh();
            frm.ShowDialog();
            var tinh = (from tin in _dataContext.DmTinhs select new { tin.TenTinh, tin.MaTinh }).OrderBy(p => p.TenTinh).ToList();
            lupMaTinh.Properties.DataSource = tinh.ToList();
        }

        private void btnHuyen_Click(object sender, EventArgs e)
        {
            FormDanhMuc.Frm_DmHuyen frm = new FormDanhMuc.Frm_DmHuyen();
            frm.ShowDialog();
            var huyen = (from h in _dataContext.DmHuyens.Where(p => p.MaTinh == (_matinh)) select new { h.TenHuyen, h.MaHuyen }).OrderBy(p => p.TenHuyen).ToList();
            lupMaHuyen.Properties.DataSource = huyen.ToList();
        }

        private void btnXa_Click(object sender, EventArgs e)
        {
            FormDanhMuc.Frm_DmXa frm = new FormDanhMuc.Frm_DmXa();
            frm.ShowDialog();
            var xa = (from x in _dataContext.DmXas.Where(p => p.MaHuyen == (_mahuyen)) select new { x.TenXa, x.MaXa }).OrderBy(p => p.TenXa).ToList();
            lupMaXa.Properties.DataSource = xa.ToList();
        }

        private void lupMaTinh_EditValueChanged(object sender, EventArgs e)
        {
            if (lupMaTinh.EditValue != null)
                _matinh = lupMaTinh.EditValue.ToString();
            else
                _matinh = "";
            var huyen = (from h in _dataContext.DmHuyens.Where(p => p.MaTinh == (_matinh)) select new { h.TenHuyen, h.MaHuyen }).OrderBy(p => p.TenHuyen).ToList();
            lupMaHuyen.Properties.DataSource = huyen.ToList();
            if (cboDTuong.Text == "Dịch vụ" && (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789") || DungChung.Bien.MaBV == "30303")
            {
                var TenTinh = _dataContext.DmTinhs.Where(p => p.MaTinh == _matinh).Select(p => p.TenTinh).FirstOrDefault();
                if (TenTinh != null)
                    txtDiaChi1.Text = TenTinh.ToString();
            }
        }

        private void lupMaHuyen_EditValueChanged(object sender, EventArgs e)
        {
            if (lupMaHuyen.EditValue != null)
                _mahuyen = lupMaHuyen.EditValue.ToString();
            else
                _mahuyen = "";
            string DiaChi = "";
            if(DungChung.Bien.MaBV == "30372")
                DiaChi = txtDiaChi.Text;
            else
                DiaChi = txtDiaChi1.Text;
            var xa = (from x in _dataContext.DmXas.Where(p => p.MaHuyen == (_mahuyen)) select new { x.TenXa, x.MaXa }).OrderBy(p => p.TenXa).ToList();
            lupMaXa.Properties.DataSource = xa.ToList();
            if (cboDTuong.Text == "Dịch vụ" && (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "30303"))
            {
                string[] _lTentinh = DiaChi.Split('-');
                var TenHuyen = _dataContext.DmHuyens.Where(p => p.MaHuyen == _mahuyen).Select(p => p.TenHuyen).FirstOrDefault();
                if (TenHuyen != null)
                {
                    if (_lTentinh.Length > 1)
                    {
                        txtDiaChi1.Text = txtDiaChi1.Text != null ? (TenHuyen.ToString() + "-" + _lTentinh[1].ToString()) : TenHuyen.ToString();
                    }
                    else
                    {
                        txtDiaChi1.Text = txtDiaChi1.Text != null ? (TenHuyen.ToString() + "-" + txtDiaChi1.Text) : TenHuyen.ToString();
                    }
                }
            }
        }

        private void dtDu5nam_EditValueChanged(object sender, EventArgs e)
        {
            DateTime _dt = System.DateTime.Now;
            if (dtDu5nam.DateTime.Day > 0)
                _dt = dtDu5nam.DateTime;
            if ((System.DateTime.Now - _dt).Days >= 0)
            {
                chkLuongCS.Enabled = true;
                chkLuongCS.ForeColor = System.Drawing.Color.DarkRed;
            }
            else
            {
                chkLuongCS.Enabled = false;
                chkLuongCS.Checked = false;
                chkLuongCS.ForeColor = System.Drawing.Color.Gray;
            }
            qrcode = false;
        }

        private void chkLuongCS_CheckedChanged(object sender, EventArgs e)
        {
        }

        private List<MucTT> _lmucTT = new List<MucTT>();

        private void lupMHuong_EditValueChanged(object sender, EventArgs e)
        {
            TinhMucHuongNhapMoiBN();
        }

        private string[] setStheTE(string str, DateTime ngayNhap)
        {
            string[] a = new string[2] { "", "" };

            a[0] = str;

            if (str != null && str.ToUpper().Contains("KT"))
            {
                str = str.ToUpper();
                string[] arr = str.Split('-');
                str = "";
                foreach (var item in arr)
                {
                    str += item;
                }
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                DateTime dt = DungChung.Ham.NgayTu(DungChung.Ham.ConvertNgay("07/07/2015"));
                if (DungChung.Bien.MaBV == "30004")
                {
                    dt = new DateTime(ngayNhap.Year, 1, 1);
                }
                var the = (from d in data.BenhNhans.Where(p => p.SThe.Length == 15).Where(p => p.NNhap >= dt)
                           where d.SThe.Substring(0, 2) == ("TE") && d.SThe.Substring(5, 2) == ("KT")
                           select d.SThe.Substring(7, 8)).ToList().Max();
                if (!String.IsNullOrEmpty(str) && str.Substring(0, 2) == ("TE") && str.Substring(5, 2) == ("KT"))
                {
                    a[1] = DungChung.Bien.MaTinh + "000";
                    string chuoi = txtTimSTHE.Text.Substring(14, 6);
                    if (true)
                    {
                        int _dodai = 5;
                        string _sothe = "", _macs = "";
                        _dodai = Convert.ToInt32(the).ToString().Length;
                        _sothe = (Convert.ToInt32(the) + 1).ToString("D8");
                        a[0] = str.Substring(0, 7) + _sothe;
                    }
                }
                else
                {
                    if (DungChung.Bien.MaBV == "30004")
                    {
                        a[0] = "TE1" + DungChung.Bien.MaTinh.Substring(0, 2) + "KT00000001";
                    }
                }
            }

            return a;
        }

        private void txtTimSTHE_KeyUp(object sender, KeyEventArgs e)
        {
            if (TTLuu == 1)
            {
                txtTimSTHE.Text = setStheTE(txtTimSTHE.Text, dtNgayN.DateTime)[0];
                if (txtTimSTHE.Text.Length == 20)
                    txtMaCS.Focus();
            }
            if (TTLuu == 2)
            {
                QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var ktthe = _data.BenhNhans.Where(p => p.MaBNhan == _mabn).ToList();
                if (ktthe.Count > 0 && (ktthe.First().SThe == null || ktthe.First().SThe.Length < 15))
                {
                    txtTimSTHE.Text = setStheTE(txtTimSTHE.Text, dtNgayN.DateTime)[0];
                }
            }
        }

        private string[] _code = new string[15] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };

        private bool qrcode = false;

        private void txtCode_Leave(object sender, EventArgs e)
        {
            /* if (string.IsNullOrWhiteSpace(txtCode.Text))
                 return;
             var spQr = txtCode.Text.Split('|');
             if (spQr.Length == 15)
             {
                 if (spQr[0].Count() == 15)
                 {
                     chkMauTheMoi.Checked = false;
                     try
                     {
                         qrcode = false;
                         if (txtCode.Text.Length > 1)
                         {
                             string[] _codephu;
                             _codephu = txtCode.Text.Split('|');
                             int j = 0; if (_codephu.Length > 0)
                                 j = _codephu.Length;
                             for (int i = 0; i < j; i++)
                             {
                                 _code[i] = _codephu[i];
                             }
                             if (15 - j > 0)
                             {
                                 for (int i = j; i < 15; i++)
                                 {
                                     _code[i] = "";
                                 }
                             }

                             if (_code[0].Length >= 15)
                             {
                                 txtTimSTHE.Text = _code[0].Substring(0, 15);//Mã Thẻ
                                 _ketqua = _code[0].Substring(0, 15).ToUpper();
                                 string muc = "", _dtuong = "";
                                 muc = _ketqua.ToString().Substring(2, 1);
                                 int muccu = 0;
                                 if (!string.IsNullOrEmpty(muc))
                                     muccu = Convert.ToInt32(muc);
                                 _dtuong = _ketqua.ToString().Substring(0, 2);
                                 var dt = DataContext.DTuongs.Where(p => p.MaDTuong == (_dtuong)).Where(p => p.MucCu == muccu).Select(p => p.MaMuc).ToList();
                                 if (dt.Count > 0 && dt.First() != null)
                                     lupMHuong.EditValue = dt.First().ToString().Trim();//Mức hưởng
                                 else
                                 {
                                     lupMHuong.EditValue = muc;
                                 }
                             }
                             try
                             {
                                 string tenbn = _code[1];
                                 tenbn = QLBV_Library.QLBV_Ham.Replace_AA(tenbn);
                                 txtNhapTBN.Text = QLBV_Library.QLBV_Ham.ConvertHexToString(tenbn);//Tên BN
                             }
                             catch
                             {
                                 txtNhapTBN.Text = "";
                             }
                             DateTime _namsinh;
                             if (_code[2] != null)
                             {
                                 if (DateTime.TryParse(_code[2], out _namsinh))
                                 {
                                     txtNgaySinh.Text = _namsinh.Day.ToString("d2");
                                     txtThangSinh.Text = _namsinh.Month.ToString("d2");
                                     txtNamSinh.Text = _namsinh.Year.ToString(); //Ngày tháng năm sinh
                                 }
                                 else
                                     txtNamSinh.Text = _code[2];
                             }
                             if (_code[3] == "1")
                                 radNamNu.SelectedIndex = 0;//giới tính
                             else
                                 radNamNu.SelectedIndex = 1;
                             try
                             {
                                 string diachi = "";
                                 diachi = _code[4];
                                 diachi = QLBV_Library.QLBV_Ham.Replace_AA(diachi);
                                 txtDiaChi.Text = QLBV_Library.QLBV_Ham.ConvertHexToString(diachi);//địa chỉ
                             }
                             catch
                             {
                                 txtDiaChi.Text = "";
                             }
                             string macs = "";
                             if (_code[5] != null && _code[5].Length >= 2)
                                 macs = _code[5].Substring(0, 2);
                             if (_code[5] != null && _code[5].Length >= 5)
                             {
                                 int i = _code[5].Length;
                                 macs += _code[5].Substring(i - 3, 3);
                             }
                             txtMaCS.Text = macs;//mã cs
                             DateTime _hantu, _handen, _ngaycap;
                             if (_code[6] != null)
                                 if (DateTime.TryParse(_code[6], out _hantu))
                                 {
                                     dtHanBHTu.DateTime = _hantu;//hạn BH từ
                                 }
                             if (_code[7] != null)
                                 if (DateTime.TryParse(_code[7], out _handen))
                                 {
                                     dtHanBHden.DateTime = _handen;//hạn BH đến
                                 }
                             if (_code[8] != null)
                                 if (DateTime.TryParse(_code[8], out _ngaycap))
                                 {
                                     dtNgayCapThe.DateTime = _ngaycap;//ngày cấp thẻ
                                 }
                             if (!string.IsNullOrEmpty(_code[12]))
                             {
                                 DateTime ngayDu5Nam = new DateTime();
                                 if (DateTime.TryParse(_code[12], out ngayDu5Nam))
                                 {
                                     dtDu5nam.DateTime = ngayDu5Nam;//đủ 5 năm từ ngày cấp
                                 }
                                 else
                                     dtDu5nam.EditValue = null;
                                 qrcode = true;
                             }

                             //_code[9] // chưa rõ
                             //lupMHuong.EditValue = _code[11];
                             if (!string.IsNullOrWhiteSpace(_ketqua))
                             {
                                 idperson = 0;
                                 personCode = "";
                                 txtMaBN.ResetText();
                                 var person = DataContext.People.FirstOrDefault(o => o.SThe == _ketqua);
                                 if (person != null)
                                 {
                                     idperson = person.IDPerson;
                                     personCode = person.PersonCode;
                                     txtMaBN.Text = DungChung.Bien.MaBV == "30007" ? person.PersonCode : person.IDPerson.ToString();
                                 }
                             }

                             txtMaCS.Focus();
                             loadTTBS(_ketqua);
                         }
                     }
                     catch
                     {
                     }
                 }
                 else if (spQr[0].Count() == 10)
                 {
                     chkMauTheMoi.Checked = true;
                     qrcode = false;
                     txtTimSTHE_New.Text = spQr[0].Substring(0, 10);//Mã Thẻ
                     _ketqua = txtTimSTHE_New.Text;
                     string muc = "";
                     muc = spQr[14];
                     lupMHuong.EditValue = muc;
                     string tenbn = spQr[1];
                     tenbn = QLBV_Library.QLBV_Ham.Replace_AA(tenbn);
                     txtNhapTBN.Text = QLBV_Library.QLBV_Ham.ConvertHexToString(tenbn);//Tên BN
                     DateTime _namsinh;
                     if (spQr[2] != null)
                     {
                         if (DateTime.TryParse(spQr[2], out _namsinh))
                         {
                             txtNgaySinh.Text = _namsinh.Day.ToString("d2");
                             txtThangSinh.Text = _namsinh.Month.ToString("d2");
                             txtNamSinh.Text = _namsinh.Year.ToString(); //Ngày tháng năm sinh
                         }
                         else
                             txtNamSinh.Text = spQr[2];
                     }
                     if (spQr[3] == "1")
                         radNamNu.SelectedIndex = 0;//giới tính
                     else
                         radNamNu.SelectedIndex = 1;
                     try
                     {
                         string diachi = "";
                         diachi = spQr[4];
                         diachi = QLBV_Library.QLBV_Ham.Replace_AA(diachi);
                         txtDiaChi.Text = QLBV_Library.QLBV_Ham.ConvertHexToString(diachi);//địa chỉ
                     }
                     catch
                     {
                         txtDiaChi.Text = "";
                     }
                     string macs = "";
                     var spMaCS = spQr[5].Split('-');
                     if (spMaCS.Count() == 2)
                     {
                         macs = spMaCS[0].Trim() + spMaCS[1].Trim();
                     }
                     txtMaCS.Text = macs;//mã cs
                     DateTime _hantu, _handen, _ngaycap;
                     if (spQr[6] != null)
                         if (DateTime.TryParse(spQr[6], out _hantu))
                         {
                             dtHanBHTu.DateTime = _hantu;//hạn BH từ
                         }
                     if (spQr[7] != null)
                         if (DateTime.TryParse(spQr[7], out _handen))
                         {
                             dtHanBHden.DateTime = _handen;//hạn BH đến
                         }
                     if (spQr[8] != null)
                         if (DateTime.TryParse(spQr[8], out _ngaycap))
                         {
                             dtNgayCapThe.DateTime = _ngaycap;//ngày cấp thẻ
                         }
                     if (!string.IsNullOrEmpty(spQr[12]))
                     {
                         DateTime ngayDu5Nam = new DateTime();
                         if (DateTime.TryParse(spQr[12], out ngayDu5Nam))
                         {
                             dtDu5nam.DateTime = ngayDu5Nam;//đủ 5 năm từ ngày cấp
                         }
                         else
                             dtDu5nam.EditValue = null;
                         qrcode = true;
                     }
                     if (!string.IsNullOrWhiteSpace(_ketqua))
                     {
                         idperson = 0;
                         personCode = "";
                         txtMaBN.ResetText();
                         var person = DataContext.People.FirstOrDefault(o => o.SThe == _ketqua);
                         if (person != null)
                         {
                             idperson = person.IDPerson;
                             personCode = person.PersonCode;
                             txtMaBN.Text = DungChung.Bien.MaBV == "30007" ? person.PersonCode : person.IDPerson.ToString();
                         }
                     }
                     txtMaCS.Focus();
                     loadTTBS(_ketqua);
                 }
             }
             else
             {
                 ApiTheBHYT2018 _sthe = new ApiTheBHYT2018();
                 _sthe.maThe = spQr[0];
                 _sthe.hoTen = spQr[2];
                 string date = spQr[3].Substring(0, 2) + "/" + spQr[3].Substring(2, 2) + "/" + spQr[3].Substring(4, 4);
                 _sthe.ngaySinh = date;
                 ApiToken _token = new ApiToken();
                 //_token.username = "01071_BV";
                 //_token.passwword = QLBV_Library.QLBV_Ham.MaHoa("BVntl01071");
                 _token.username = DungChung.Bien.UrserNameBHXH;
                 _token.passwword = QLBV_Library.QLBV_Ham.MaHoa(DungChung.Bien.PassWordBHXK);
                 KQNhanLichSuKCBBS n = NhanLSKCBNew_CV3666(_token, _sthe);
                 if (n != null && n.ghiChu != "Thẻ không tồn tại!")
                 {
                     txtTimSTHE.Visible = true;
                     txtTimSTHE_New.Visible = false;
                     // dtNgayN.Text = DateTime.Now.ToString();
                     txtTimSTHE.Text = n.maThe;
                     dtHanBHTu.Text = n.gtTheTu;
                     dtHanBHden.Text = n.gtTheDen;
                     txtNhapTBN.Text = n.hoTen;
                     txtDiaChi.Text = n.diaChi;

                     dtDu5nam.Text = n.ngayDu5Nam;
                     string muc = n.maThe.Substring(2, 1);
                     lupMHuong.EditValue = muc;

                     if (n.gioiTinh == "Nam")
                     {
                         radNamNu.SelectedIndex = 0;
                     }
                     else radNamNu.SelectedIndex = 1;

                     DateTime _namsinh = DateTime.Parse(n.ngaySinh);
                     txtNgaySinh.Text = _namsinh.Day.ToString("d2");
                     txtThangSinh.Text = _namsinh.Month.ToString("d2");
                     txtNamSinh.Text = _namsinh.Year.ToString(); //Ngày tháng
                     txtMaCS.Text = n.maDKBD;
                     _ketqua = txtTimSTHE.Text;
                     if (!string.IsNullOrWhiteSpace(_ketqua))
                     {
                         idperson = 0;
                         personCode = "";
                         txtMaBN.ResetText();
                         var person = DataContext.People.FirstOrDefault(o => o.SThe == _ketqua);
                         if (person != null)
                         {
                             idperson = person.IDPerson;
                             personCode = person.PersonCode;
                             txtMaBN.Text = DungChung.Bien.MaBV == "30007" ? person.PersonCode : person.IDPerson.ToString();
                         }
                     }

                     loadTTBS(_ketqua);
                     txtMaCS.Focus();
                 }
                 else MessageBox.Show("Thẻ không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
             }*/
        }

        private void txtCode_Click(object sender, EventArgs e)
        {
            txtCode.SelectAll();
        }

        private void txtThangSinh_EditValueChanged(object sender, EventArgs e)
        {
            if (txtThangSinh.Focused && txtThangSinh.Text != null && txtThangSinh.Text.Length == 2)
                txtNamSinh.Focus();
            txtThangTuoi.Text = TuoitheoThang(_dataContext, txtNgaySinh.Text, txtThangSinh.Text, txtNamSinh.Text, DungChung.Bien.formatAge);
            if (!string.IsNullOrWhiteSpace(txtNamSinh.Text) && txtNamSinh.Text.Length == 4 && !txtThangSinh.Text.Contains("_"))
                txtTuoi.Text = DungChung.Ham.CalculateAgeByDate((!string.IsNullOrWhiteSpace(txtNgaySinh.Text) && !txtThangSinh.Text.Contains("_")) ? txtNgaySinh.Text : "01", (!string.IsNullOrWhiteSpace(txtThangSinh.Text) && !txtThangSinh.Text.Contains("_")) ? txtThangSinh.Text : "01", txtNamSinh.Text).ToString();
        }

        public static void InPhieuGiuThe_TB(int mabn)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var qbn = (from bn in data.BenhNhans.Where(p => p.MaBNhan == mabn)
                       join bv in data.BenhViens on bn.MaKCB equals bv.MaBV
                       join kp in data.KPhongs on bn.MaKP equals kp.MaKP
                       select new { bn.SoTT, bn.TenBNhan, bn.NgaySinh, bn.ThangSinh, bn.NamSinh, bn.GTinh, bn.DChi, bn.SThe, bn.HanBHDen, bn.HanBHTu, bn.MaCS, bv.TenBV, kp.TenKP, DiaChiPK = kp.DChi, bn.TChung }).ToList();
            if (qbn.Count > 0)
            {
                BaoCao.rep_GiayGiuThe rep = new BaoCao.rep_GiayGiuThe();
                //var SoDK = data.SoDKKBs.Where(p => p.MaBNhan == mabn).Select(p => p.SoDK).FirstOrDefault();
                //if (SoDK != null)
                //   rep.SoTT.Value = SoDK;
                rep.SoTT.Value = qbn.First().SoTT.Value.ToString("d3");
                rep.HoTen.Value = qbn.First().TenBNhan;
                rep.mabn.Value = mabn.ToString();
                rep.lydokham.Value = qbn.First().TChung;
                rep.NgaySinh.Value = qbn.First().NgaySinh + "/" + qbn.First().ThangSinh + "/" + qbn.First().NamSinh;
                rep.GioiTinh.Value = qbn.First().GTinh == 0 ? "Nữ" : "Nam";
                rep.DiaChi.Value = qbn.First().DChi;
                if (!string.IsNullOrWhiteSpace(qbn.First().SThe) && qbn.First().SThe.Length == 10)
                {
                    rep.BHYT4.Value = (qbn.First().SThe.Trim() == "" || qbn.First().SThe == null) ? "" : qbn.First().SThe.Substring(0, 2);
                    rep.BHYT5.Value = (qbn.First().SThe.Trim() == "" || qbn.First().SThe == null) ? "" : qbn.First().SThe.Substring(2, 3);
                    rep.BHYT6.Value = (qbn.First().SThe.Trim() == "" || qbn.First().SThe == null) ? "" : qbn.First().SThe.Substring(5, 5);
                }
                else
                {
                    rep.BHYT1.Value = (qbn.First().SThe.Trim() == "" || qbn.First().SThe == null) ? "" : qbn.First().SThe.Substring(0, 2);
                    rep.BHYT2.Value = (qbn.First().SThe.Trim() == "" || qbn.First().SThe == null) ? "" : qbn.First().SThe.Substring(2, 1);
                    rep.BHYT3.Value = (qbn.First().SThe.Trim() == "" || qbn.First().SThe == null) ? "" : qbn.First().SThe.Substring(3, 2);
                    rep.BHYT4.Value = (qbn.First().SThe.Trim() == "" || qbn.First().SThe == null) ? "" : qbn.First().SThe.Substring(5, 2);
                    rep.BHYT5.Value = (qbn.First().SThe.Trim() == "" || qbn.First().SThe == null) ? "" : qbn.First().SThe.Substring(7, 3);
                    rep.BHYT6.Value = (qbn.First().SThe.Trim() == "" || qbn.First().SThe == null) ? "" : qbn.First().SThe.Substring(10, 5);
                }
                rep.HanBHTu.Value = String.Format("{0:dd/MM/yyyy}", qbn.First().HanBHTu);
                rep.HanBHDen.Value = String.Format("{0:dd/MM/yyyy}", qbn.First().HanBHDen);
                rep.TenCSKCB.Value = qbn.First().TenBV;
                rep.MaCSKCB.Value = qbn.First().MaCS;
                rep.PhongKham.Value = qbn.First().DiaChiPK + " - " + qbn.First().TenKP;

                frmIn frm = new frmIn();
                rep.lblNgayThangNam.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                //rep.DataSource = qcls.ToList();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            int mbn = _mabnhan;
            bool _in = false;
            if (chkIn.Checked)
                _in = true;
            if (_in)
            {
                #region 30009

                if (DungChung.Bien.MaBV == "30009")
                {
                    QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    frmIn frm = new frmIn();
                    BaoCao.repPhieuDangKyKB_TH rep = new BaoCao.repPhieuDangKyKB_TH();

                    var par = (from bn in data.BenhNhans
                               join kp in data.KPhongs on bn.MaKP equals kp.MaKP
                               where (bn.MaBNhan == mbn)
                               select new { bn.Tuyen, bn.NoiTinh, bn.MaCS, bn.TenBNhan, bn.SoTT, bn.MaBNhan, bn.Tuoi, bn.DTuong, bn.HanBHTu, bn.HanBHDen, bn.NgaySinh, bn.ThangSinh, bn.NamSinh, bn.SThe, bn.DChi, kp.TenKP, bn.NNhap, bn.TChung }).ToList();
                    if (par.Count > 0)
                    {
                        string _mabv = "";
                        if (par.First().MaCS != null)
                            _mabv = par.First().MaCS;
                        var ten = data.BenhViens.Where(p => p.MaBV == (_mabv)).Select(p => p.TenBV).ToList();
                        if (ten.Count > 0)
                            _mabv += " - " + ten.First();
                        string _namsinh = "";
                        if (par.First().NgaySinh != null && par.First().NgaySinh.ToString() != "")
                            _namsinh += par.First().NgaySinh.ToString();
                        else
                            _namsinh += "...";
                        if (par.First().ThangSinh != null && par.First().ThangSinh.ToString() != "")
                            _namsinh += " / " + par.First().ThangSinh.ToString();
                        else
                            _namsinh += " / ...";
                        if (par.First().NamSinh != null && par.First().NamSinh.ToString() != "")
                            _namsinh += " / " + par.First().NamSinh.ToString();
                        else
                            _namsinh += " / ...";
                        if (par.First().Tuyen != null && par.First().DTuong == "BHYT")
                        {
                            if (par.First().Tuyen == 1)
                                rep.Tuyen.Value = "Đúng|Trái tuyến: Đúng tuyến";
                            else
                                rep.Tuyen.Value = "Đúng|Trái tuyến: Trái tuyến";
                        }
                        if (par.First().NoiTinh == 1)
                        {
                            rep.NoiNgoaiTinh.Value = "BN nội tỉnh KCB ban đầu";
                        }
                        else if (par.First().NoiTinh == 2)
                        {
                            rep.NoiNgoaiTinh.Value = "BN nội tỉnh đến";
                        }
                        else if (par.First().NoiTinh == 3)
                        {
                            rep.NoiNgoaiTinh.Value = "BN ngoại tỉnh đến";
                        }
                        rep.NgaySinh.Value = _namsinh;
                        rep.MaCSDK.Value = _mabv;
                        rep.TenKP.Value = par.First().TenKP;
                        rep.TenBNhan.Value = par.First().TenBNhan.ToUpper();
                        rep.MaBNhan.Value = par.First().MaBNhan;
                        rep.Tuoi.Value = par.First().Tuoi;
                        rep.DTuong.Value = par.First().DTuong;
                        rep.SThe.Value = par.First().SThe.ToString();
                        rep.SoTT.Value = par.First().SoTT;
                        rep.DChi.Value = par.First().DChi;
                        rep.TChung.Value = par.First().TChung;
                        rep.NgayTu.Value = par.First().HanBHTu;
                        rep.NgayDen.Value = par.First().HanBHDen;
                        if (par.First().NNhap != null)
                        {
                            rep.Ngaythang.Value = DungChung.Ham.NgaySangChu(par.First().NNhap.Value, DungChung.Bien.FormatDate);
                        }
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                }

                #endregion 30009

                else
                {
                    #region 24009, 26007, 01830, 34019

                    if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "01830" || DungChung.Bien.MaBV == "34019" || DungChung.Bien.MaBV == "30007")
                    {
                        frmHSBN._InGiayDK(_dataP, mbn);
                    }

                    #endregion 24009, 26007, 01830, 34019

                    else if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                    {
                        InPhieuGiuThe_TB(mbn);
                    }
                    else
                    {
                        #region chung

                        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                        frmIn frm = new frmIn();
                        BaoCao.repPhieuDangKyKB rep = new BaoCao.repPhieuDangKyKB();

                        var par = (from bn in data.BenhNhans
                                   join kp in data.KPhongs on bn.MaKP equals kp.MaKP
                                   where (bn.MaBNhan == mbn)
                                   select new { bn.MaCS, bn.TenBNhan, bn.SoTT, bn.MaBNhan, bn.Tuoi, bn.DTuong, bn.HanBHTu, bn.HanBHDen, bn.NgaySinh, bn.ThangSinh, bn.NamSinh, bn.SThe, bn.DChi, kp.TenKP, bn.NNhap, bn.TChung }).ToList();
                        if (par.Count > 0)
                        {
                            string _mabv = "";
                            if (par.First().MaCS != null)
                                _mabv = par.First().MaCS;
                            var ten = data.BenhViens.Where(p => p.MaBV == (_mabv)).Select(p => p.TenBV).ToList();
                            if (ten.Count > 0)
                                _mabv += " - " + ten.First();
                            string _namsinh = "";
                            if (par.First().NgaySinh != null && par.First().NgaySinh.ToString() != "")
                                _namsinh += par.First().NgaySinh.ToString();
                            else
                                _namsinh += "...";
                            if (par.First().ThangSinh != null && par.First().ThangSinh.ToString() != "")
                                _namsinh += " / " + par.First().ThangSinh.ToString();
                            else
                                _namsinh += " / ...";
                            if (par.First().NamSinh != null && par.First().NamSinh.ToString() != "")
                                _namsinh += " / " + par.First().NamSinh.ToString();
                            else
                                _namsinh += " / ...";
                            rep.NgaySinh.Value = _namsinh;
                            rep.MaCSDK.Value = _mabv;
                            rep.TenKP.Value = par.First().TenKP;
                            rep.TenBNhan.Value = par.First().TenBNhan.ToUpper();
                            rep.MaBNhan.Value = par.First().MaBNhan;
                            rep.Tuoi.Value = par.First().Tuoi;
                            rep.DTuong.Value = par.First().DTuong;
                            rep.SThe.Value = par.First().SThe.ToString();
                            rep.SoTT.Value = par.First().SoTT;
                            rep.DChi.Value = par.First().DChi;
                            rep.TChung.Value = par.First().TChung;
                            rep.NgayTu.Value = par.First().HanBHTu;
                            rep.NgayDen.Value = par.First().HanBHDen;
                            if (par.First().NNhap != null)
                            {
                                rep.Ngaythang.Value = DungChung.Ham.NgaySangChu(par.First().NNhap.Value, 2);
                            }
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }

                        #endregion chung
                    }
                }

                //
            }
        }

        private void btnLuuTBNV_Click(object sender, EventArgs e)
        {
            ChucNang.frm_LuuSerialThietBi frm = new ChucNang.frm_LuuSerialThietBi();
            frm.ShowDialog();
        }

        private void txtTimSTHE_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void lupMaBVgt_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("Để hiện thị được danh sách Bệnh viện, trong danh mục Bệnh Viện phải có trường 'Status'=2 hoặc Mã chủ quản = Mã đơn vị");
        }

        private void dtNgayN_EditValueChanged(object sender, EventArgs e)
        {
            lupKhoaKham_EditValueChanged(sender, e);

            // KtraThoiGianVaoVien();
        }

        private void dtNgayN_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
        }

        private void txtDiaChi_EditValueChanged(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "24009")
            {
                txtNoiLV.Text = txtDiaChi1.Text;
            }
        }

        private void dtHanBHden_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void lupMaBVgt_EditValueChanged(object sender, EventArgs e)
        {
            if (lupMaBVgt.Text.Length > 1)
            {
                if (DungChung.Bien.MaBV != "30003" && DungChung.Bien.MaBV != "01830" && DungChung.Bien.MaBV != "24009" && DungChung.Bien.MaBV != "26007" && DungChung.Bien.MaBV != "30010")
                {
                    radTuyen.Properties.ReadOnly = false;
                    radTuyen.SelectedIndex = 0;
                }
            }
            else
            {
                txtMaCS_Leave(sender, e);
                radTuyen.Properties.ReadOnly = true;
            }
            lupMaNoigt.EditValue = lupMaBVgt.EditValue;
        }

        private void txtNgaySinh_EditValueChanged(object sender, EventArgs e)
        {
            if (txtNgaySinh.Focused && txtNgaySinh.Text != null && txtNgaySinh.Text.Length == 2)
                txtThangSinh.Focus();
            txtThangTuoi.Text = TuoitheoThang(_dataContext, txtNgaySinh.Text, txtThangSinh.Text, txtNamSinh.Text, DungChung.Bien.formatAge);
            if (!string.IsNullOrWhiteSpace(txtNamSinh.Text) && txtNamSinh.Text.Length == 4 && !txtNgaySinh.Text.Contains("_"))
                txtTuoi.Text = DungChung.Ham.CalculateAgeByDate((!string.IsNullOrWhiteSpace(txtNgaySinh.Text) && !txtThangSinh.Text.Contains("_")) ? txtNgaySinh.Text : "01", (!string.IsNullOrWhiteSpace(txtThangSinh.Text) && !txtThangSinh.Text.Contains("_")) ? txtThangSinh.Text : "01", txtNamSinh.Text).ToString();
        }

        public void ktrathongtinLS2018(int _status)
        {
            if (_ketqua != null && !string.IsNullOrEmpty(txtNhapTBN.Text) && !string.IsNullOrEmpty(txtNamSinh.Text))
            {
                ApiTheBHYT2018 _sthe = new ApiTheBHYT2018();
                _sthe.maThe = _ketqua.ToUpper();
                _sthe.hoTen = txtNhapTBN.Text;
                _sthe.ngaySinh = txtNamSinh.Text;
                if (!string.IsNullOrEmpty(DungChung.Bien.xmlFilePath_LIS[10]) && !string.IsNullOrEmpty(DungChung.Bien.xmlFilePath_LIS[11]))
                {
                    ApiToken _token = new ApiToken();
                    _token.username = DungChung.Bien.xmlFilePath_LIS[10];
                    _token.passwword = QLBV_Library.QLBV_Ham.MaHoa(DungChung.Bien.xmlFilePath_LIS[11]);
                    KQNhanLichSuKCBBS n = NhanLSKCBNew_CV366(_token, _sthe);
                    if (n != null)
                    {
                        string ketqua = n.maTheMoi;
                        string macs = n.maDKBD; /*n.maDKBDmoi;*/
                        string hanTuMoi = n.gtTheTuMoi;
                        string hanDenMoi = n.gtTheDenMoi;
                        string ngayDu5namMoi = n.ngayDu5Nam;
                        if (ketqua != null)
                        {
                            if (ketqua == null)
                            {
                                ketqua = n.maTheCu;
                            }
                            txtTimSTHE.Text = ketqua;
                            txtMaCS.Text = macs;
                            dtHanBHTu.DateTime = Convert.ToDateTime(hanTuMoi);
                            dtHanBHden.DateTime = Convert.ToDateTime(hanDenMoi);
                            dtDu5nam.DateTime = Convert.ToDateTime(ngayDu5namMoi);
                            if (DungChung.Bien.MaBV == "24297")
                            {
                                if (DungChung.Bien.MaBV == txtMaCS.Text)
                                {
                                    radTuyen.SelectedIndex = 0;
                                }
                                else
                                {
                                    if (txtMaCS.Text == "24297")
                                    {
                                        radTuyen.SelectedIndex = 0;
                                    }
                                    else if (txtMaCS.Text.StartsWith("24"))
                                    {
                                        radTuyen.SelectedIndex = 2;
                                    }
                                    else
                                    {
                                        radTuyen.SelectedIndex = 1;
                                    }
                                }
                            }
                        }
                        if (macs == DungChung.Bien.MaBV)
                        {
                            lupNoiNgoaiTinh.EditValue = 1;
                        }
                        else if (macs.Substring(0, 2) == DungChung.Bien.MaBV.Substring(0, 2))
                        {
                            lupNoiNgoaiTinh.EditValue = 2;
                        }
                        else
                        {
                            lupNoiNgoaiTinh.EditValue = 3;
                        }
                        txtNhapTBN.Text = n.hoTen;
                        if (!n.ngaySinh.Contains("/"))
                        {
                            txtNamSinh.Text = n.ngaySinh;
                        }
                        else if (n.ngaySinh.Split('/').Count() == 2)
                        {
                            txtThangSinh.Text = n.ngaySinh.Split('/')[0];
                            txtNamSinh.Text = n.ngaySinh.Split('/')[1];
                        }
                        else
                        {
                            DateTime ngaysinh = Convert.ToDateTime(n.ngaySinh);
                            txtNgaySinh.Text = ngaysinh.Day > 9 ? ngaysinh.Day.ToString() : "0" + ngaysinh.Day.ToString();
                            txtThangSinh.Text = ngaysinh.Month > 9 ? ngaysinh.Month.ToString() : "0" + ngaysinh.Month.ToString();
                            txtNamSinh.Text = ngaysinh.Year.ToString();
                        }
                        if (n.gioiTinh == "Nam")
                        {
                            radNamNu.SelectedIndex = 0;
                        }
                        else
                        {
                            radNamNu.SelectedIndex = 1;
                        }
                        txtDiaChi1.Text = txtDiaChi.Text = n.diaChi;
                        var maTinh = _dataContext.DmTinhs
                            .Where(p => n.diaChi.Contains(p.TenTinh))
                            .Select(x => x.MaTinh).FirstOrDefault();
                        var maHuyen = _dataContext.DmHuyens
                            .Where(p => n.diaChi.Contains(p.TenHuyen) && p.MaTinh == maTinh)
                            .Select(x => x.MaHuyen).FirstOrDefault();
                        var maXa = _dataContext.DmXas
                            .Where(p => n.diaChi.Contains(p.TenXa) && p.MaTinh == maTinh && p.MaHuyen == maHuyen)
                            .Select(x => x.MaXa).FirstOrDefault();
                        lupMaTinh.EditValue = lupTinhKhaiSinh.EditValue = maTinh;
                        lupMaHuyen.EditValue = lupMaHuyenKhaiSinh.EditValue = maHuyen;
                        lupMaXa.EditValue = lupXaKhaiSinh.EditValue = maXa;
                        string muc = ketqua != null ? ketqua.ToString().Substring(2, 1) : "";
                        string _dtuong = ketqua != null ? ketqua.ToString().Substring(0, 2) : "";

                        int muccu = 0;
                        if (!string.IsNullOrEmpty(muc))
                            muccu = Convert.ToInt32(muc);
                        var dt = _dataContext.DTuongs.Where(p => p.MaDTuong == (_dtuong)).Where(p => p.MucCu == muccu).Select(p => p.MaMuc).ToList();
                        if (dt.Count > 0 && dt.First() != null)
                            lupMHuong.EditValue = dt.First().ToString().Trim();
                        else
                        {
                            lupMHuong.EditValue = muc;
                        }

                        cboKhuVuc.Text = n.maKV;
                        if (_status == 0)
                        {
                            string mess = "Cổng BHXH trả lời: " + n.ghiChu;
                            if (n.maKetQua == "000")
                            {
                                chkMauTheMoi.Checked = false;
                                txtTimSTHE.Text = n.maThe;
                                _ketqua = n.maThe;
                                txtMaCS.Text = n.maDKBD;
                                txtMaCS_Leave(null, null);
                                if (txtTimSTHE.Text != null)
                                {
                                    var idper = _dataContext.People.Where(p => p.SThe.Contains(txtTimSTHE.Text)).ToList();
                                    if (idper.Count > 0)
                                    {
                                        idperson = idper.First().IDPerson;
                                        txtMaBN.EditValue = idper.First().IDPerson.ToString();
                                        txtMaBN.Text = idper.First().IDPerson.ToString();
                                        txtTimSTHE.Enabled = true;
                                    }
                                    if (!string.IsNullOrWhiteSpace(txtTimSTHE.Text))
                                    {
                                        string mucc = "", _dttuong = "";
                                        if (!string.IsNullOrWhiteSpace(txtTimSTHE.Text))
                                        {
                                            muc = txtTimSTHE.Text.Substring(2, 1);
                                            _dttuong = txtTimSTHE.Text.Substring(0, 2);
                                        }
                                        int mucccu = 0;
                                        if (!string.IsNullOrEmpty(mucc))
                                            mucccu = Convert.ToInt32(mucc);
                                        var dtt = _dataContext.DTuongs.Where(p => p.MaDTuong == (_dttuong)).Where(p => p.MucCu == muccu).Select(p => p.MaMuc).ToList();
                                        if (dtt.Count > 0 && dt.First() != null)
                                            lupMHuong.EditValue = dt.First().ToString().Trim();
                                        else
                                        {
                                            lupMHuong.EditValue = muc;
                                        }
                                    }
                                    var ttbcbn = (from bn in _dataContext.BenhNhans.Where(p => p.SThe.Contains(txtTimSTHE.Text))
                                                  join ttbx in _dataContext.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                                                  select new { ttbx, bn }).OrderBy(p => p.bn.MaBNhan).ToList();

                                    if (ttbcbn.Count > 0)
                                    {
                                        txtSoDT.Text = ttbcbn.Last().ttbx.DThoai;
                                        txtDT12345.Text = ttbcbn.Last().ttbx.DThoai;
                                        lup_CCap.EditValue = ttbcbn.Last().ttbx.ID_CB;
                                        lupCVu.EditValue = ttbcbn.Last().ttbx.ID_CV;
                                        lupMaTinh.EditValue = ttbcbn.Last().ttbx.MaTinh;
                                        lupMaHuyen.EditValue = ttbcbn.Last().ttbx.MaHuyen;
                                        lupMaXa.EditValue = ttbcbn.Last().ttbx.MaXa;
                                        lupDantoc.EditValue = ttbcbn.Last().ttbx.MaDT;
                                        lupNgheNghiep.EditValue = ttbcbn.Last().ttbx.MaNN;
                                        txtNoiLV.Text = ttbcbn.Last().ttbx.NoiLV;
                                        txtDTNThan.Text = ttbcbn.Last().ttbx.DThoaiNT;
                                        txtNThan.Text = ttbcbn.Last().ttbx.NThan;
                                        txtSoKSinh_CMT.Text = ttbcbn.Last().ttbx.SoKSinh;
                                        cboQuocTich.Text = ttbcbn.Last().ttbx.NgoaiKieu;
                                        dtNgayCap.EditValue = ttbcbn.Last().ttbx.NgayCapCMT;
                                        txtThonPho.Text = ttbcbn.Last().ttbx.ThonPho;
                                        ckcchuyenkhoan.Checked = Convert.ToBoolean(ttbcbn.Last().ttbx.HTThanhToan);
                                        txtTTThe_old.Text = ttbcbn.Last().ttbx.TTTheBHYTold;
                                        lupTinhKhaiSinh.EditValue = string.IsNullOrEmpty(ttbcbn.Last().ttbx.MaTinhKhaiSinh) ? null : ttbcbn.Last().ttbx.MaTinhKhaiSinh.Trim();
                                        if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "24012")
                                        {
                                            lupMaHuyenKhaiSinh.EditValue = string.IsNullOrEmpty(ttbcbn.Last().ttbx.MaHuyenKhaiSinh) ? null : ttbcbn.Last().ttbx.MaHuyenKhaiSinh.Trim();
                                            lupXaKhaiSinh.EditValue = string.IsNullOrEmpty(ttbcbn.Last().ttbx.MaXaKhaiSinh) ? null : ttbcbn.Last().ttbx.MaXaKhaiSinh.Trim();
                                        }
                                        //txtDChiKSinh.Text = ttbcbn.Last().ttbx.DchiKhaiSinh;
                                        txtHKKT.Text = ttbcbn.Last().ttbx.HKTT;
                                        lupQuanHe.EditValue = string.IsNullOrEmpty(ttbcbn.Last().ttbx.QuanHeNhanThan) ? null : ttbcbn.Last().ttbx.QuanHeNhanThan.Trim();
                                        txtSoto.Text = ttbcbn.Last().ttbx.SoTo;
                                        txtDiaChiNguoiThan.Text = ttbcbn.Last().ttbx.DCNguoiThan;
                                    }
                                }
                                dtHanBHTu.DateTime = Convert.ToDateTime(n.gtTheTu);
                                dtHanBHden.DateTime = Convert.ToDateTime(n.gtTheDen);
                                try
                                {
                                    if (!n.ngaySinh.Contains("/"))
                                    {
                                        txtNamSinh.Text = n.ngaySinh;
                                    }
                                    else if (n.ngaySinh.Split('/').Count() == 2)
                                    {
                                        txtThangSinh.Text = n.ngaySinh.Split('/')[0];
                                        txtNamSinh.Text = n.ngaySinh.Split('/')[1];
                                    }
                                    else
                                    {
                                        DateTime _ngaysinh = Convert.ToDateTime(n.ngaySinh);
                                        txtNgaySinh.Text = _ngaysinh.Day.ToString("D2");
                                        txtThangSinh.Text = _ngaysinh.Month.ToString("D2");
                                        txtNamSinh.Text = _ngaysinh.Year.ToString();
                                    }
                                    dtDu5nam.DateTime = Convert.ToDateTime(n.ngayDu5Nam);
                                    cboKhuVuc.EditValue = n.maKV;
                                }
                                catch
                                {
                                }
                                if (n.gioiTinh == "Nam")
                                    radNamNu.SelectedIndex = 0;
                                else
                                    radNamNu.SelectedIndex = 1;
                                txtDiaChi1.Text = txtDiaChi.Text = n.diaChi;
                                QLBV.FormThamSo.frm_MessgeBox frm = new FormThamSo.frm_MessgeBox(mess, 0);
                                frm.ShowDialog();
                            }
                            else
                            {
                                QLBV.FormThamSo.frm_MessgeBox frm = new FormThamSo.frm_MessgeBox(mess, 1);
                                frm.ShowDialog();
                            }
                        }
                        else
                        {
                            List<LichSuKCB2018> _ls = n.dsLichSuKCB2018;
                            if (_ls != null)
                            {
                                QLBV.FormNhap.frm_ThongTinTheBHYT frm = new frm_ThongTinTheBHYT(_ls);
                                frm.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("Cổng BHXH trả lời: không có lịch sử KCB trên cổng BHXH", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Chưa thiết lập tài khoản và mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Nhập đầy đủ thông tin: Số thẻ, Họ tên và năm sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ktrathongtinLS(int _status)
        {
            if (_ketqua != null && _ketqua.Length > 12 && _ketqua.Substring(2, 10).ToUpper().Contains("KT"))
                return "";
            if (_status == 1)
            {
                ApiTheBHYT2018 _sthe = new ApiTheBHYT2018();
                _sthe.maThe = _ketqua.ToUpper();
                _sthe.hoTen = txtNhapTBN.Text;
                _sthe.ngaySinh = txtNamSinh.Text;
                ApiToken _token = new ApiToken();
                _token.username = DungChung.Bien.xmlFilePath_LIS[10];
                _token.passwword = QLBV_Library.QLBV_Ham.MaHoa(DungChung.Bien.xmlFilePath_LIS[11]);
                KQNhanLichSuKCBBS n = NhanLSKCBNew_CV366(_token, _sthe);
                return "";
            }
            else
            {
                if (_ketqua != null && !string.IsNullOrEmpty(txtNhapTBN.Text) && !string.IsNullOrEmpty(txtMaCS.Text) && dtHanBHTu.Text != null && dtHanBHden.Text != null)
                {
                    GiamDinhBHXH.BHXH_Model.ApiTheBHYT2018 sthe = new GiamDinhBHXH.BHXH_Model.ApiTheBHYT2018();
                    sthe.maThe = _ketqua.ToUpper();
                    sthe.hoTen = txtNhapTBN.Text;
                    sthe.ngaySinh = txtNgaySinh.Text.Trim() + "/" + txtThangSinh.Text.Trim() + "/" + txtNamSinh.Text;
                    if (sthe.ngaySinh.Length < 8)
                        sthe.ngaySinh = txtNamSinh.Text;
                    GiamDinhBHXH.BHXH_Model.theBHYT sthecu = new GiamDinhBHXH.BHXH_Model.theBHYT();
                    sthecu.maThe = _ketqua.ToUpper();
                    sthecu.hoTen = txtNhapTBN.Text;
                    sthecu.maCSKCB = txtMaCS.Text;
                    sthecu.gioiTinh = radNamNu.SelectedIndex == 0 ? 1 : 2;
                    sthecu.ngaySinh = txtNgaySinh.Text.Trim() + "/" + txtThangSinh.Text.Trim() + "/" + txtNamSinh.Text;
                    if (sthe.ngaySinh.Length < 8)
                        sthe.ngaySinh = txtNamSinh.Text;
                    sthecu.ngayBD = dtHanBHTu.DateTime.ToString("dd/MM/yyyy");
                    sthecu.ngayKT = dtHanBHden.DateTime.ToString("dd/MM/yyyy");
                    string _lLS = frmHSBNNhapMoi.KTLSKCB(sthe, sthecu, _status);

                    return _lLS;
                }
                else
                {
                    if (_ketqua != null && _ketqua.Length == 15)
                    {
                        MessageBox.Show("Lỗi! \n Nhập đầy đủ thông tin: Số thẻ, tên bệnh nhân, mã đk, hạn thẻ, ngày sinh");
                    }
                    return "";
                }
            }
        }

        private void btnKT_Click(object sender, EventArgs e)
        {
            string _lLS = ktrathongtinLS(0);

            if (_ketqua != null && !string.IsNullOrEmpty(txtNhapTBN.Text) && !string.IsNullOrEmpty(txtMaCS.Text) && dtHanBHTu.Text != null && dtHanBHden.Text != null)
            {
                GiamDinhBHXH.BHXH_Model.ApiTheBHYT2018 sthe = new GiamDinhBHXH.BHXH_Model.ApiTheBHYT2018();
                sthe.maThe = _ketqua;
                sthe.hoTen = txtNhapTBN.Text;
                string ngaysinh = txtNgaySinh.Text.Trim(), thangsinh = txtThangSinh.Text.Trim();
                if (txtNgaySinh.Text.Trim().Length == 1)
                    ngaysinh = "0" + txtNgaySinh.Text.Trim();
                if (txtNgaySinh.Text.Trim().Length == 1)
                    thangsinh = "0" + txtThangSinh.Text.Trim();
                sthe.ngaySinh = ngaysinh + "/" + thangsinh + "/" + txtNamSinh.Text;
                if (sthe.ngaySinh.Length < 10)
                    sthe.ngaySinh = txtNamSinh.Text;
                ChucNang.frm_LichSuKCB frm = new ChucNang.frm_LichSuKCB(sthe, DungChung.Bien.xmlFilePath_LIS[10], DungChung.Bien.xmlFilePath_LIS[11]);
                frm.ShowDialog();
            }
        }

        private void lupMaNoigt_EditValueChanged(object sender, EventArgs e)
        {
            lupMaBVgt.EditValue = lupMaNoigt.EditValue;
        }

        private void lupNgheNghiep_EditValueChanged(object sender, EventArgs e)
        {
            lupMaNN_ma.EditValue = lupNgheNghiep.EditValue;
        }

        private void lupMaNN_ma_EditValueChanged(object sender, EventArgs e)
        {
            lupNgheNghiep.EditValue = lupMaNN_ma.EditValue;
        }

        private void lupDantoc_EditValueChanged(object sender, EventArgs e)
        {
            lupMaDT_ma.EditValue = lupDantoc.EditValue;
        }

        private void lupMaDT_ma_EditValueChanged(object sender, EventArgs e)
        {
            lupDantoc.EditValue = lupMaDT_ma.EditValue;
        }

        private void txtSoDT_Click(object sender, EventArgs e)
        {
            txtSoDT.SelectAll();
        }

        private int z = 1;
        private string filePath = "";

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            bool tontai = true; bool kt = false;
            if (ptPhoto.Image == null)
                tontai = false;

            if (tontai)
            {
                DialogResult _dresult = MessageBox.Show("Bạn có muốn chọn lại ảnh!", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dresult == DialogResult.Yes)
                {
                    kt = true;
                }
            }
            else
            {
                kt = true;
            }
            if (kt)
            {
                string fileName = string.Empty;
                OpenFileDialog op = new OpenFileDialog();
                op.Multiselect = false;
                op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp";

                if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = op.FileName;
                    string ex = Path.GetExtension(op.FileName);
                    if (!string.IsNullOrEmpty(fileName))
                        ptPhoto.Image = Image.FromFile(fileName);
                    else
                        ptPhoto.Image = null;
                    string _tenfileanh = DungChung.Bien.DuongDan;
                    // _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".jpg";
                    if (_mabn == 0)
                    {
                        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                        _mabn = data.BenhNhans.Max(p => p.MaBNhan) + 1;
                    }
                    _tenfileanh += _mabn + ex;
                    filePath = layTenFileAnh(fileName, _tenfileanh);
                }
            }
        }

        public string layTenFileAnh(string fileAnhTMH, string tenfileanh)
        {
            if (!string.IsNullOrEmpty(fileAnhTMH))
            {
                if (!File.Exists(tenfileanh))
                {
                    File.Copy(fileAnhTMH, tenfileanh);
                }
                else
                {
                    for (int i = 0; i < 100; i++)
                    {
                        string a = "";
                        string ex = Path.GetExtension(tenfileanh);
                        a = tenfileanh.Replace(ex, i + ex);
                        if (!File.Exists(a))
                        {
                            File.Copy(fileAnhTMH, a);
                            tenfileanh = a;
                            break;
                        }
                    }
                }
            }
            return tenfileanh;
        }

        private void btnXoaAnh1_Click(object sender, EventArgs e)
        {
            filePath = "";
            ptPhoto.Image = null;
            patdpimg = null;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            fillImage();
        }

        public void fillImage()
        {
            if (_mabn > 0)
            {
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var qduongdan = (from ttbx in data.TTboXungs.Where(p => p.MaBNhan == _mabn) select ttbx).FirstOrDefault();
                if (qduongdan != null)
                {
                    filePath = qduongdan.FileAnh;
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        ptPhoto.Image = Image.FromFile(filePath);
                    }
                    else
                        ptPhoto.Image = null;
                }
                else
                    ptPhoto.Image = null;
            }
        }

        private void txtsohsbnmantinh_Leave(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            string sohsbnmantinh = "";
            if (txtsohsbnmantinh.Text.Length > 1)
            {
                sohsbnmantinh = txtsohsbnmantinh.Text;
                kiemtralichsuKCBBNMT(sohsbnmantinh);

                int _mabn = -1;
                var ttbsung = data.TTboXungs.Where(p => p.So_eTBM == sohsbnmantinh).FirstOrDefault();
                if (ttbsung != null)
                {
                    _mabn = ttbsung.MaBNhan;
                    var _lttbn = data.BenhNhans.Where(p => p.MaBNhan == _mabn).ToList();
                    if (_lttbn.Count() > 0)
                    {
                        var _iddtbn = data.DTBNs.Where(p => p.HTTT == 2).Select(p => p.IDDTBN).FirstOrDefault();
                        if (_iddtbn != null)
                        {
                            cboDTuong.EditValue = _iddtbn;
                        }
                        txtNhapTBN.Text = _lttbn.First().TenBNhan;
                        txtDiaChi1.Text = txtDiaChi.Text = _lttbn.First().DChi;
                        if (_lttbn.First().GTinh == 0)
                        {
                            radNamNu.SelectedIndex = 0;
                        }
                        else
                        {
                            radNamNu.SelectedIndex = 1;
                        }
                        if (_lttbn.First().NgaySinh != "")
                        {
                            txtNgaySinh.Text = _lttbn.First().NgaySinh;
                        }
                        if (_lttbn.First().ThangSinh != "")
                        {
                            txtThangSinh.Text = _lttbn.First().ThangSinh;
                        }
                        if (_lttbn.First().NamSinh != "")
                        {
                            txtNamSinh.Text = _lttbn.First().NamSinh;
                        }
                    }

                    lupMaTinh.EditValue = ttbsung.MaTinh;
                    lupMaHuyen.EditValue = ttbsung.MaHuyen;
                    lupMaXa.EditValue = ttbsung.MaXa;
                    txtThonPho.Text = ttbsung.ThonPho;
                    lupDantoc.EditValue = ttbsung.MaDT;
                    cboQuocTich.Text = ttbsung.NgoaiKieu;
                    lupNgheNghiep.EditValue = ttbsung.MaNN;
                    txtNoiLV.Text = ttbsung.NoiLV;
                    txtSoDT.Text = ttbsung.DThoai;
                    txtNThan.Text = ttbsung.NThan;
                    txtDTNThan.Text = ttbsung.DThoaiNT;
                    txtSoKSinh_CMT.Text = ttbsung.SoKSinh;
                    if (ttbsung.NgayCapCMT != null)
                        dtNgayCap.DateTime = ttbsung.NgayCapCMT.Value;
                    lup_CCap.EditValue = ttbsung.ID_CB;
                    lupCVu.EditValue = ttbsung.ID_CV;
                    if (ttbsung.MaTinhKhaiSinh != null)
                        lupTinhKhaiSinh.EditValue = ttbsung.MaTinhKhaiSinh.Trim();
                    if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "24012")
                    {
                        if (ttbsung.MaXaKhaiSinh != null)
                            lupXaKhaiSinh.EditValue = ttbsung.MaXaKhaiSinh.Trim();
                        if (ttbsung.MaHuyenKhaiSinh != null)
                            lupMaHuyenKhaiSinh.EditValue = ttbsung.MaHuyenKhaiSinh.Trim();
                    }
                    //txtDChiKSinh.Text = ttbsung.DchiKhaiSinh;
                    txtHKKT.Text = ttbsung.HKTT;
                }
            }
            else
            {
                MessageBox.Show("Số hồ sơ không chính xác", "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void txtsohsbnmantinh_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void txtsohsbnmantinh_DoubleClick(object sender, EventArgs e)
        {
            loadhsbnmantinh();
        }

        private void loadhsbnmantinh()
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var _lbn = (from ttbx in data.TTboXungs.Where(p => p.So_eTBM != "" && p.So_eTBM != null)
                        join bn in data.BenhNhans on ttbx.MaBNhan equals bn.MaBNhan
                        select new
                        {
                            TenBNhan = bn.TenBNhan,
                            SoHS = ttbx.So_eTBM
                        }).ToList();
            txtsohsbnmantinh.Properties.DataSource = _lbn;
        }

        private void txtsohsbnmantinh_Click(object sender, EventArgs e)
        {
        }

        private void ckcbnmantinh_CheckedChanged(object sender, EventArgs e)
        {
            if (ckcbnmantinh.Checked)
            {
                txtsohsbnmantinh.Visible = true;
                labelControl42.Visible = true;
            }
            else
            {
                txtsohsbnmantinh.Visible = false;
                labelControl42.Visible = false;
            }
        }

        private void radNoiTru_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMaCS_Leave(null, null);
            TinhMucHuongNhapMoiBN();
        }

        private void lupChanDoanKb_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lupChanDoanKb.Text))
            {
                if (lupChanDoanKb.EditValue.ToString() == "0")
                {
                    lupChanDoanKb.EditValue = "";
                    lupMaICDkb.EditValue = "";
                    txtCDNoiGT.Text = "";
                }
                else
                {
                    lupMaICDkb.EditValue = lupChanDoanKb.EditValue;
                    txtCDNoiGT.Text = lupChanDoanKb.Text;
                }
            }
            else
            {
                lupMaICDkb.EditValue = "";
                txtCDNoiGT.Text = "";
            }
        }

        private void lupMaICDkb_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lupMaICDkb.Text))
            {
                if (lupMaICDkb.EditValue.ToString() == "0")
                {
                    lupChanDoanKb.EditValue = "";
                    lupMaICDkb.EditValue = "";
                }
                else
                    lupChanDoanKb.EditValue = lupMaICDkb.EditValue;
            }
            else
            {
                lupChanDoanKb.EditValue = "";
            }
        }

        private void lupMaICDkb_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                FormThamSo.frm_TimKiem frm = new FormThamSo.frm_TimKiem();
                frm.GetData = new FormThamSo.frm_TimKiem._getstring(getICD);
                frm.ShowDialog();
            }
        }

        public void getICD(string _maicd)
        {
            lupMaICDkb.EditValue = _maicd;
        }

        private void lupChanDoanKb_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                FormThamSo.frm_TimKiem frm = new FormThamSo.frm_TimKiem();
                frm.GetData = new FormThamSo.frm_TimKiem._getstring(getICD);
                frm.ShowDialog();
            }
        }

        private void lupMaXa_EditValueChanged(object sender, EventArgs e)
        {
            if (lupMaXa.EditValue != null)
            {
                _maxa = lupMaXa.EditValue.ToString();
            }
            else
            {
                _maxa = "";
            }
            string DiaChi = "";
            if (Bien.MaBV == "30372")
                DiaChi = txtDiaChi.Text;
            else
                DiaChi = txtDiaChi1.Text;
            if (cboDTuong.Text == "Dịch vụ" && (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789"))
            {
                string[] _lDiachi = DiaChi.Split('-');
                var TenXa = _dataContext.DmXas.Where(p => p.MaXa == _maxa).ToList();
                if (TenXa.Count > 0)
                {
                    if (_lDiachi.Length > 2)
                    {
                        txtDiaChi1.Text = TenXa.First().TenXa.ToString() + "-" + _lDiachi[1].ToString() + "-" + _lDiachi[2].ToString();
                    }
                    else
                    {
                        txtDiaChi1.Text = TenXa.First().TenXa.ToString() + "-" + txtDiaChi1.Text;
                    }
                }
            }
        }

        private void cboCapCuu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mabvgt = "";
            if (lupMaBVgt.EditValue != null)
                mabvgt = lupMaBVgt.EditValue.ToString();

            if (cboCapCuu.SelectedIndex == 1)//&& ( DungChung.Bien.MaBV != "01071" || mabvgt != ""))
            {
                radTuyen.SelectedIndex = 0;
            }
            else
            {
                //if (TTLuu == 1)
                txtMaCS_Leave(sender, e);
            }
        }

        private void ckcBoXungThe_CheckedChanged(object sender, EventArgs e)
        {
            if (ckcBoXungThe.Checked)
            {
                E_TTBN(false);
                txtDiaChi1.Enabled = txtDiaChi.Enabled = false;
                txtTimSTHE.Text = "";
                txtTimSTHE_New.Text = "";
                txtMaCS.Text = "";
            }
            else
            {
                E_TTBN(true);
                txtDiaChi1.Enabled = txtDiaChi.Enabled = true;
                frmHSBNNhapMoi_Load(null, null);
            }
        }

        private void simpleButton2_Click_2(object sender, EventArgs e)
        {
            ktrathongtinLS2018(0);
        }

        public static KQNhanLichSuKCBBS NhanLSKCBNew_CV366(ApiToken _apitoken, ApiTheBHYT2018 _thebhyt)
        {
            string url = "api/token/take";
            var token = Utilities.Commons.AppApi.PostAsync<KQPhienLamViec, object>(Bien.URL_BHXH, "api/token/take", new
            {
                username = _apitoken.username,
                password = _apitoken.passwword
            });

            if (token.Result == null)
                MessageBox.Show("Đăng nhập không thành công!");
            url = string.Format("api/egw/NhanLichSuKCB2018?token={0}&id_token={1}&username={2}&password={3}", token.Result.APIKey.access_token, token.Result.APIKey.id_token, _apitoken.username, _apitoken.passwword);
            return Utilities.Commons.AppApi.PostAsync<KQNhanLichSuKCBBS, object>(Bien.URL_BHXH, url, _thebhyt).Result;
        }

        public static KQNhanLichSuKCBBS NhanLSKCBNew_CV3666(ApiToken _apitoken, ApiTheBHYT2018 _thebhyt)
        {
            KQNhanLichSuKCBBS _lskcb = new KQNhanLichSuKCBBS();
            using (var client = new HttpClient())
            {
                string serviceUrl = "http://egw.baohiemxahoi.gov.vn";
                client.BaseAddress = new Uri(serviceUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var values = new Dictionary<string, string>
                {
                    {"username",_apitoken.username},
                    {"password",_apitoken.passwword}
                };
                var content = new FormUrlEncodedContent(values);
                HttpResponseMessage reponse = client.PostAsync("api/token/take", content).Result;
                if (reponse.IsSuccessStatusCode)
                {
                    KQPhienLamViec plv = reponse.Content.ReadAsAsync<KQPhienLamViec>().Result;
                    var key = plv.APIKey;
                    if (key != null)
                    {
                        string data2 = string.Format("token={0}&id_token={1}&username={2}&password={3}", key.access_token, key.id_token, _apitoken.username, _apitoken.passwword);
                        HttpResponseMessage response2 = client.PostAsJsonAsync("api/egw/NhanLichSuKCB2018?" + data2, _thebhyt).Result;
                        if (response2.IsSuccessStatusCode)
                        {
                            string result = response2.Content.ReadAsStringAsync().Result;
                            try
                            {
                                _lskcb = (KQNhanLichSuKCBBS)JsonConvert.DeserializeObject<KQNhanLichSuKCBBS>(result);
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sai tài khoản hoặc mật khẩu", "Thông báo");
                    }
                }
                else
                {
                    MessageBox.Show("Không gọi được lên cổng BHYT", "Thông báo");
                }
            }
            return _lskcb;
        }

        public static KQNhanLichSuKCBBS NhanLSKCBNew_CV36676(ApiToken _apitoken, ApiTheBHYT2018 _thebhyt)
        {
            KQNhanLichSuKCBBS _lskcb = new KQNhanLichSuKCBBS();
            using (var client = new HttpClient())
            {
                string serviceUrl = "http://ytcs.ytebacgiang.com/oauth/token";
                client.BaseAddress = new Uri(serviceUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var values = new Dictionary<string, string>
                {
                    {"username",_apitoken.username},
                    {"password",_apitoken.passwword}
                };
                var content = new FormUrlEncodedContent(values);
                HttpResponseMessage reponse = client.PostAsync("api/token/take", content).Result;
                if (reponse.IsSuccessStatusCode)
                {
                    KQPhienLamViec plv = reponse.Content.ReadAsAsync<KQPhienLamViec>().Result;
                    var key = plv.APIKey;
                    if (key != null)
                    {
                        string data2 = string.Format("token={0}&id_token={1}&username={2}&password={3}", key.access_token, key.id_token, _apitoken.username, _apitoken.passwword);
                        HttpResponseMessage response2 = client.PostAsJsonAsync("api/egw/NhanLichSuKCB2018?" + data2, _thebhyt).Result;
                        if (response2.IsSuccessStatusCode)
                        {
                            string result = response2.Content.ReadAsStringAsync().Result;
                            try
                            {
                                _lskcb = (KQNhanLichSuKCBBS)JsonConvert.DeserializeObject<KQNhanLichSuKCBBS>(result);
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sai tài khoản hoặc mật khẩu", "Thông báo");
                    }
                }
                else
                {
                    MessageBox.Show("Không gọi được lên cổng BHYT", "Thông báo");
                }
            }
            return _lskcb;
        }

        public class ApiToken
        {
            public string username { get; set; }
            public string passwword { get; set; }
        }

        public class KQPhienLamViec
        {
            public string maKetQua { get; set; }
            public ApiKey APIKey { get; set; }
        }

        public class ApiKey
        {
            public string access_token { get; set; }
            public string id_token { get; set; }
            public string token_type { get; set; }
            public string username { get; set; }
            public DateTime expires_in { get; set; }
        }

        public class ApiTheBHYT2018
        {
            public string maThe { get; set; }
            public string hoTen { get; set; }
            public string ngaySinh { get; set; }
        }

        public class KQNhanLichSuKCBBS
        {
            public string maKetQua { get; set; }
            public string ghiChu { get; set; }
            public string maThe { set; get; }
            public string hoTen { get; set; }
            public string ngaySinh { set; get; }
            public string gioiTinh { get; set; }
            public string diaChi { get; set; }
            public string maDKBD { get; set; }
            public string maDKBDmoi { get; set; }
            public string cqBHXH { get; set; }
            public string gtTheTu { get; set; }
            public string gtTheDen { get; set; }
            public string maKV { get; set; }
            public string ngayDu5Nam { get; set; }
            public string ngayDu5NamMoi { get; set; }
            public string maSoBHXH { get; set; }
            public string maTheCu { get; set; }
            public string maTheMoi { get; set; }
            public string gtTheTuMoi { get; set; }
            public string gtTheDenMoi { get; set; }
            public List<LichSuKCB2018> dsLichSuKCB2018 { set; get; }
            public List<LichSuKT2018> dsLichSuKT2018 { set; get; }
        }

        public class LichSuKCB2018
        {
            public string maHoSo { set; get; }
            public string maCSKCB { set; get; }
            public string ngayVao { set; get; }
            public string ngayRa { set; get; }
            public string tenBenh { set; get; }
            public string tinhTrang { set; get; }
            public string kqDieuTri { set; get; }
        }

        public class LichSuKT2018
        {
            public string userKT { set; get; }
            public string thoiGianKT { set; get; }
            public string thongBao { set; get; }
            public string maLoi { set; get; }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            ktrathongtinLS2018(1);
        }

        private void txtTenBV_EditValueChanged(object sender, EventArgs e)
        {
        }

        private string _lydoLodg = "";

        public void _getLyDoLog(string a)
        {
            _lydoLodg = a;
        }

        private void btnPID_Click(object sender, EventArgs e)
        {
            int ma = _mabn;
            if (_mabn > 0)
                ma = _mabn;
            else
                ma = _mabnhan;
            if (ma > 0)
            {
                frm_TTKhamBenh frm = new frm_TTKhamBenh(ma);
                frm.ShowDialog();
            }
            else
                MessageBox.Show("Bạn chưa lưu thông tin bệnh nhân");
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            FormDanhMuc.Frm_DmTinh frm = new FormDanhMuc.Frm_DmTinh();
            frm.ShowDialog();
            var tinh = (from tin in _dataContext.DmTinhs select new { tin.TenTinh, tin.MaTinh }).OrderBy(p => p.TenTinh).ToList();
            lupTinhKhaiSinh.Properties.DataSource = tinh.ToList();
        }

        private void cboKhuVuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "20001")
            {
                if (!string.IsNullOrEmpty(cboKhuVuc.Text))
                    radTuyen.SelectedIndex = 0;
                else
                {
                    txtMaCS_Leave(null, null);
                }
            }
            TinhMucHuongNhapMoiBN();
        }

        private void txtSoDT_Leave(object sender, EventArgs e)
        {
        }

        private void FillTTBNHoTen(BenhNhan bn)
        {
            FillThongTinBenhNhan(bn, true);
        }

        public void txtDiaChi_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (DungChung.Bien.MaBV == "30372")
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        if (!string.IsNullOrWhiteSpace(txtDiaChi.Text) && listTHX != null && listTHX.Count > 0)
                        {
                            string dc = txtDiaChi.Text;
                            var vt = listTHX.FirstOrDefault(o => o.VietTat != null && o.VietTat.ToLower() == dc.ToLower());
                            if (vt != null)
                            {
                                txtDiaChi.Text = vt.TenVietTat;
                            }
                        }
                    }
                }
                else
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        if (!string.IsNullOrWhiteSpace(txtDiaChi1.Text) && listTHX != null && listTHX.Count > 0)
                        {
                            string dc = txtDiaChi1.Text;
                            var vt = listTHX.FirstOrDefault(o => o.VietTat != null && o.VietTat.ToLower() == dc.ToLower());
                            if (vt != null)
                            {
                                txtDiaChi1.Text = vt.TenVietTat;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Library.Logging.WriteLog.Error(ex);
            }
        }

        private void dtHanBHTu_EditValueChanged(object sender, EventArgs e)
        {
            if (dtHanBHTu.EditValue != null && !qrcode)
            {
                dtDu5nam.EditValue = dtHanBHTu.DateTime.AddYears(5);
            }
        }

        private void txtMaBN_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int idP = 0;
                    string perCode = "";
                    if (DungChung.Bien.MaBV == "30007")
                    {
                        perCode = txtMaBN.Text;
                    }
                    else
                        idP = LibraryStore.TypeConvert.Parse.ToInt32(txtMaBN.Text);
                    var person = _dataContext.People.FirstOrDefault(o => DungChung.Bien.MaBV == "30007" ? o.PersonCode == perCode : o.IDPerson == idP);
                    if (person != null)
                    {
                        LoadTTBNhanByPerson(person);
                    }
                }
            }
            catch (Exception ex)
            {
                DungChung.WriteLog.Warn(ex);
            }
        }

        private void LoadTTBNhanByPerson(Person person)
        {
            txtMaBN.Text = DungChung.Bien.MaBV == "30007" ? person.PersonCode : person.IDPerson.ToString();
            txtTimSTHE.ReadOnly = true;
            txtTimSTHE_New.ReadOnly = true;
            string muc = "", _dtuong = "";
            _ketqua = person.SThe;
            idperson = person.IDPerson;
            personCode = person.PersonCode;
            if (TTLuu == 1)
            {
                // set gia tri

                txtNhapTBN.Text = person.TenBNhan;
                txtTimSTHE.Text = person.SThe;

                if (!string.IsNullOrWhiteSpace(person.SThe))
                {
                    muc = person.SThe.ToString().Substring(2, 1);
                    _dtuong = person.SThe.ToString().Substring(0, 2);
                }

                if (person.NSinh != null)
                    txtNamSinh.Text = person.NSinh.ToString();
                string NS = "", TS = "";
                if (person.NgaySinh != null)
                    NS = person.NgaySinh.ToString();
                if (person.ThangSinh != null)
                    TS = person.ThangSinh.ToString();
                txtNgaySinh.Text = (NS.Trim().Length > 0 && NS.Trim().Length < 2) ? ("0" + NS.ToString()) : NS.ToString();
                txtThangSinh.Text = (TS.Trim().Length < 2 && TS.Trim().Length > 0) ? ("0" + TS.ToString()) : TS.ToString();
                radNamNu.SelectedIndex = person.GTinh == 1 ? 0 : 1;
                if (person.NgayHM != null)
                    dtDu5nam.DateTime = person.NgayHM.Value;
                if (person.HanBHDen != null)
                    dtHanBHden.DateTime = person.HanBHDen.Value;
                if (person.HanBHTu != null)
                    dtHanBHTu.DateTime = person.HanBHTu.Value;
                txtDiaChi1.Text = txtDiaChi.Text = person.DChi;
                txtMaCS.Text = person.MaCS.Trim();
                if (person.GTinh.ToString() == "1")
                {
                    radNamNu.SelectedIndex = 0;
                }
                else
                {
                    radNamNu.SelectedIndex = 1;
                }
                cboKhuVuc.Text = person.KhuVuc;
                // hiển thông tin bổ xung
                lupMaTinh.EditValue = person.MaTinh;
                lupMaHuyen.EditValue = person.MaHuyen;
                lupMaXa.EditValue = person.MaXa;
                if (!string.IsNullOrEmpty(person.FileAnh))
                {
                    ptPhoto.Image = Image.FromFile(person.FileAnh);
                }
                else
                    ptPhoto.Image = null;
                //
                //load ttbs
                if (DungChung.Bien.MaBV == "26007")
                {
                    if (person.GhiChu != null && person.GhiChu.Trim().Contains("THA"))
                    {
                        KtraBNTHA = true;
                    }
                }
                loadTTBS(_ketqua);
                //
                int muccu = 0;
                if (!string.IsNullOrEmpty(muc))
                    muccu = Convert.ToInt32(muc);
                var dt = _dataContext.DTuongs.Where(p => p.MaDTuong == (_dtuong)).Where(p => p.MucCu == muccu).Select(p => p.MaMuc).ToList();
                if (dt.Count > 0 && dt.First() != null)
                    lupMHuong.EditValue = dt.First().ToString().Trim();
                else
                {
                    lupMHuong.EditValue = muc;
                }
            }
            txtMaCS.Focus();
        }

        private void txtMaDT_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    var idP = LibraryStore.TypeConvert.Parse.ToInt32(txtMaDT.Text);
                    var bnhan = _dataContext.BenhNhans.FirstOrDefault(o => o.MaBNhan == idP);
                    if (bnhan != null)
                    {
                        var person = _dataContext.People.FirstOrDefault(o => DungChung.Bien.MaBV == "30007" ? o.PersonCode == bnhan.PersonCode : o.IDPerson == bnhan.IDPerson);
                        if (person != null)
                        {
                            LoadTTBNhanByPerson(person);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DungChung.WriteLog.Warn(ex);
            }
        }

        private void lupChanDoanKb_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                lupChanDoanKb.EditValue = null;
                txtCDNoiGT.Text = "";
                lupChanDoanKb.Properties.View.ActiveFilterString = "";
            }
        }

        private void txtCDNoiGT_EditValueChanging(object sender, ChangingEventArgs e)
        {
        }

        private void btnMoMayAnh_Click(object sender, EventArgs e)
        {
            frm_MoMayAnh momayanh = new frm_MoMayAnh();
            momayanh.ShowDialog();
            Photo();
        }

        public static string patdpimg;

        public void Photo()
        {
            if (patdpimg != null)
            {
                ptPhoto.Image = Image.FromFile(patdpimg);
                save();
            }
            else
                ptPhoto.Image = null;
        }

        private void save()
        {
            if (ptPhoto.Image != null)
            {
                string fileName = string.Empty;
                if (patdpimg != null)
                {
                    fileName = patdpimg;
                    string ex = Path.GetExtension(fileName);
                    string _tenfileanh = DungChung.Bien.DuongDan;
                    if (_mabn == 0)
                    {
                        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                        _mabn = data.BenhNhans.Max(p => p.MaBNhan) + 1;
                    }
                    _tenfileanh += _mabn + ex;
                    filePath = layTenFileAnh(fileName, _tenfileanh);
                }
            }
        }

        private void lup_CCap_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void cboChuyenKhoa_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void dtNgayCap_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void txtNThan_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void txtHKKT_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void txtDT12345_Click(object sender, EventArgs e)
        {
            txtDT12345.SelectAll();
        }

        private void txtDiaChi_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void txtDChiKSinh_Leave(object sender, EventArgs e)
        {
            txtDChiKSinh.Text = DungChung.Ham.ToFirstUpper(txtDChiKSinh.Text);
        }

        private void txtHKKT_Leave(object sender, EventArgs e)
        {
            txtHKKT.Text = DungChung.Ham.ToFirstUpper(txtHKKT.Text);
        }

        private void txtThonPho_Leave(object sender, EventArgs e)
        {
            txtThonPho.Text = DungChung.Ham.ToFirstUpper(txtThonPho.Text);
        }

        private void txtNoiLV_Leave(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV != "01071")
            {
                txtNoiLV.Text = DungChung.Ham.ToFirstUpper(txtNoiLV.Text);
            }
        }

        private void txtNThan_Leave(object sender, EventArgs e)
        {
            txtNThan.Text = DungChung.Ham.ToFirstUpper(txtNThan.Text);
        }

        private void txtDiaChiNguoiThan_Leave(object sender, EventArgs e)
        {
            txtDiaChiNguoiThan.Text = DungChung.Ham.ToFirstUpper(txtDiaChiNguoiThan.Text);
        }

        private void txtHoTenBo_Leave(object sender, EventArgs e)
        {
            txtHoTenBo.Text = DungChung.Ham.ToFirstUpper(txtHoTenBo.Text);
        }

        private void txtHoTenMe_Leave(object sender, EventArgs e)
        {
            txtHoTenMe.Text = DungChung.Ham.ToFirstUpper(txtHoTenMe.Text);
        }

        private void lupHinhThucKham_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void chkMauTheMoi_CheckedChanged(object sender, EventArgs e)
        {
            txtTimSTHE_New.Visible = chkMauTheMoi.Checked;
            if (chkMauTheMoi.Checked)
            {
                txtTimSTHE.Text = "";
            }
            else
                txtTimSTHE_New.Text = "";
        }

        private void txtNoiLV_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void txtMaDT_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void txtMaBNCovid_EditValueChanged(object sender, EventArgs e)
        {
            if (txtMaBNCovid.Text.Length > 10)
            {
                MessageBox.Show("Bạn đã nhập mã BN covid quá 10 kí tự. Yều cầu nhập lại");
                return;
            }
        }

        private void txtTrieuChung_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void labelControl54_Click(object sender, EventArgs e)
        {
        }

        private void lbSoto_Click(object sender, EventArgs e)
        {
        }

        private void txtSoto_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void btnTSVS_Click(object sender, EventArgs e)
        {
            int ma = _mabn;
            if (_mabn > 0)
                ma = _mabn;
            else
                ma = _mabnhan;
            if (ma > 0)
            {
                frm_TienSuVacXin frm = new frm_TienSuVacXin(ma);
                frm.ShowDialog();
            }
            else
                MessageBox.Show("Bạn chưa lưu thông tin bệnh nhân");
        }

        private void lupMaHuyenKhaiSinh_EditValueChanged(object sender, EventArgs e)
        {
            if (lupMaHuyenKhaiSinh.EditValue != null)
                _mahuyen = lupMaHuyenKhaiSinh.EditValue.ToString();
            else
                _mahuyen = "";
            string DiaChi = "";
            if (Bien.MaBV == "30372")
                DiaChi = txtDiaChi.Text;
            else
                DiaChi = txtDiaChi1.Text;
            var xa = (from x in _dataContext.DmXas.Where(p => p.MaHuyen == (_mahuyen)) select new { x.TenXa, x.MaXa }).OrderBy(p => p.TenXa).ToList();
            lupXaKhaiSinh.Properties.DataSource = xa.ToList();
        }

        private void btnHuyenKhaiSinh_Click(object sender, EventArgs e)
        {
            FormDanhMuc.Frm_DmHuyen frm = new FormDanhMuc.Frm_DmHuyen();
            frm.ShowDialog();
            var huyen = (from h in _dataContext.DmHuyens.Where(p => p.MaTinh == (_matinh)) select new { h.TenHuyen, h.MaHuyen }).OrderBy(p => p.TenHuyen).ToList();
            lupMaHuyenKhaiSinh.Properties.DataSource = huyen.ToList();
        }

        private void btnXaKhaiSinh_Click(object sender, EventArgs e)
        {
            FormDanhMuc.Frm_DmXa frm = new FormDanhMuc.Frm_DmXa();
            frm.ShowDialog();
            var xa = (from x in _dataContext.DmXas.Where(p => p.MaHuyen == (_mahuyen)) select new { x.TenXa, x.MaXa }).OrderBy(p => p.TenXa).ToList();
            lupXaKhaiSinh.Properties.DataSource = xa.ToList();
        }

        private void lupXaKhaiSinh_EditValueChanged(object sender, EventArgs e)
        {
            string MaXa = "";
            if (lupXaKhaiSinh.EditValue != null)
                MaXa = Convert.ToString(lupXaKhaiSinh.EditValue);
            string DiaChi = "";
            if (Bien.MaBV == "30372")
                DiaChi = txtDiaChi.Text;
            else
                DiaChi = txtDiaChi1.Text;
        }

        private void lupTinhKhaiSinh_EditValueChanged(object sender, EventArgs e)
        {
            if (lupTinhKhaiSinh.EditValue != null)
                _matinh = lupTinhKhaiSinh.EditValue.ToString();
            else
                _matinh = "";
            var huyen = (from h in _dataContext.DmHuyens.Where(p => p.MaTinh == (_matinh)) select new { h.TenHuyen, h.MaHuyen }).OrderBy(p => p.TenHuyen).ToList();
            lupMaHuyenKhaiSinh.Properties.DataSource = huyen.ToList();
        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(txtCode.Text))
                    return;
                var spQr = txtCode.Text.Split('|');
                if (spQr.Length == 15)
                {
                    if (spQr[0].Count() == 15)
                    {
                        chkMauTheMoi.Checked = false;
                        try
                        {
                            qrcode = false;
                            if (txtCode.Text.Length > 1)
                            {
                                string[] _codephu;
                                _codephu = txtCode.Text.Split('|');
                                int j = 0; if (_codephu.Length > 0)
                                    j = _codephu.Length;
                                for (int i = 0; i < j; i++)
                                {
                                    _code[i] = _codephu[i];
                                }
                                if (15 - j > 0)
                                {
                                    for (int i = j; i < 15; i++)
                                    {
                                        _code[i] = "";
                                    }
                                }

                                if (_code[0].Length >= 15)
                                {
                                    txtTimSTHE.Text = _code[0].Substring(0, 15);//Mã Thẻ
                                    _ketqua = _code[0].Substring(0, 15).ToUpper();
                                    string muc = "", _dtuong = "";
                                    muc = _ketqua.ToString().Substring(2, 1);
                                    int muccu = 0;
                                    if (!string.IsNullOrEmpty(muc))
                                        muccu = Convert.ToInt32(muc);
                                    _dtuong = _ketqua.ToString().Substring(0, 2);
                                    var dt = _dataContext.DTuongs.Where(p => p.MaDTuong == (_dtuong)).Where(p => p.MucCu == muccu).Select(p => p.MaMuc).ToList();
                                    if (dt.Count > 0 && dt.First() != null)
                                        lupMHuong.EditValue = dt.First().ToString().Trim();//Mức hưởng
                                    else
                                    {
                                        lupMHuong.EditValue = muc;
                                    }
                                }
                                try
                                {
                                    string tenbn = _code[1];
                                    tenbn = QLBV_Library.QLBV_Ham.Replace_AA(tenbn);
                                    txtNhapTBN.Text = QLBV_Library.QLBV_Ham.ConvertHexToString(tenbn);//Tên BN
                                }
                                catch
                                {
                                    txtNhapTBN.Text = "";
                                }
                                DateTime _namsinh;
                                if (_code[2] != null)
                                {
                                    if (DateTime.TryParse(_code[2], out _namsinh))
                                    {
                                        txtNgaySinh.Text = _namsinh.Day.ToString("d2");
                                        txtThangSinh.Text = _namsinh.Month.ToString("d2");
                                        txtNamSinh.Text = _namsinh.Year.ToString(); //Ngày tháng năm sinh
                                    }
                                    else
                                        txtNamSinh.Text = _code[2];
                                }
                                if (_code[3] == "1")
                                    radNamNu.SelectedIndex = 0;//giới tính
                                else
                                    radNamNu.SelectedIndex = 1;
                                try
                                {
                                    string diachi = "";
                                    diachi = _code[4];
                                    diachi = QLBV_Library.QLBV_Ham.Replace_AA(diachi);
                                    txtDiaChi1.Text = txtDiaChi.Text = QLBV_Library.QLBV_Ham.ConvertHexToString(diachi);//địa chỉ
                                }
                                catch
                                {
                                    txtDiaChi1.Text = txtDiaChi.Text = "";
                                }
                                if(Bien.MaBV == "30372")
                                {
                                    var Chuoi = txtDiaChi.Text.Split(',');
                                    if(!string.IsNullOrEmpty(txtDiaChi.Text))
                                    {
                                        lupMaXa.Text = Chuoi[0];
                                        lupMaHuyen.Text = Chuoi[1];
                                        lupMaTinh.Text = Chuoi[2];
                                    }    
                                }    
                                string macs = "";
                                if (_code[5] != null && _code[5].Length >= 2)
                                    macs = _code[5].Substring(0, 2);
                                if (_code[5] != null && _code[5].Length >= 5)
                                {
                                    int i = _code[5].Length;
                                    macs += _code[5].Substring(i - 3, 3);
                                }
                                txtMaCS.Text = macs;//mã cs
                                DateTime _hantu, _handen, _ngaycap;
                                if (_code[6] != null)
                                    if (DateTime.TryParse(_code[6], out _hantu))
                                    {
                                        dtHanBHTu.DateTime = _hantu;//hạn BH từ
                                    }
                                if (_code[7] != null)
                                    if (DateTime.TryParse(_code[7], out _handen))
                                    {
                                        dtHanBHden.DateTime = _handen;//hạn BH đến
                                    }
                                if (_code[8] != null)
                                    if (DateTime.TryParse(_code[8], out _ngaycap))
                                    {
                                        dtNgayCapThe.DateTime = _ngaycap;//ngày cấp thẻ
                                    }
                                if (!string.IsNullOrEmpty(_code[12]))
                                {
                                    DateTime ngayDu5Nam = new DateTime();
                                    if (DateTime.TryParse(_code[12], out ngayDu5Nam))
                                    {
                                        dtDu5nam.DateTime = ngayDu5Nam;//đủ 5 năm từ ngày cấp
                                    }
                                    else
                                        dtDu5nam.EditValue = null;
                                    qrcode = true;
                                }

                                //_code[9] // chưa rõ
                                //lupMHuong.EditValue = _code[11];
                                if (!string.IsNullOrWhiteSpace(_ketqua))
                                {
                                    idperson = 0;
                                    personCode = "";
                                    txtMaBN.ResetText();
                                    var person = _dataContext.People.FirstOrDefault(o => o.SThe == _ketqua);
                                    if (person != null)
                                    {
                                        idperson = person.IDPerson;
                                        personCode = person.PersonCode;
                                        txtMaBN.Text = DungChung.Bien.MaBV == "30007" ? person.PersonCode : person.IDPerson.ToString();
                                    }
                                }

                                txtMaCS.Focus();
                                loadTTBS(_ketqua);
                            }
                        }
                        catch
                        {
                        }
                    }
                    else if (spQr[0].Count() == 10)
                    {
                        chkMauTheMoi.Checked = true;
                        qrcode = false;
                        txtTimSTHE_New.Text = spQr[0].Substring(0, 10);//Mã Thẻ
                        _ketqua = txtTimSTHE_New.Text;
                        string muc = "";
                        muc = spQr[14];
                        lupMHuong.EditValue = muc;
                        string tenbn = spQr[1];
                        tenbn = QLBV_Library.QLBV_Ham.Replace_AA(tenbn);
                        txtNhapTBN.Text = QLBV_Library.QLBV_Ham.ConvertHexToString(tenbn);//Tên BN
                        DateTime _namsinh;
                        if (spQr[2] != null)
                        {
                            if (DateTime.TryParse(spQr[2], out _namsinh))
                            {
                                txtNgaySinh.Text = _namsinh.Day.ToString("d2");
                                txtThangSinh.Text = _namsinh.Month.ToString("d2");
                                txtNamSinh.Text = _namsinh.Year.ToString(); //Ngày tháng năm sinh
                            }
                            else
                                txtNamSinh.Text = spQr[2];
                        }
                        if (spQr[3] == "1")
                            radNamNu.SelectedIndex = 0;//giới tính
                        else
                            radNamNu.SelectedIndex = 1;
                        try
                        {
                            string diachi = "";
                            diachi = spQr[4];
                            diachi = QLBV_Library.QLBV_Ham.Replace_AA(diachi);
                            txtDiaChi1.Text = txtDiaChi.Text = QLBV_Library.QLBV_Ham.ConvertHexToString(diachi);//địa chỉ
                        }
                        catch
                        {
                            txtDiaChi1.Text = txtDiaChi.Text = "";
                        }
                        string macs = "";
                        var spMaCS = spQr[5].Split('-');
                        if (spMaCS.Count() == 2)
                        {
                            macs = spMaCS[0].Trim() + spMaCS[1].Trim();
                        }
                        txtMaCS.Text = macs;//mã cs
                        DateTime _hantu, _handen, _ngaycap;
                        if (spQr[6] != null)
                            if (DateTime.TryParse(spQr[6], out _hantu))
                            {
                                dtHanBHTu.DateTime = _hantu;//hạn BH từ
                            }
                        if (spQr[7] != null)
                            if (DateTime.TryParse(spQr[7], out _handen))
                            {
                                dtHanBHden.DateTime = _handen;//hạn BH đến
                            }
                        if (spQr[8] != null)
                            if (DateTime.TryParse(spQr[8], out _ngaycap))
                            {
                                dtNgayCapThe.DateTime = _ngaycap;//ngày cấp thẻ
                            }
                        if (!string.IsNullOrEmpty(spQr[12]))
                        {
                            DateTime ngayDu5Nam = new DateTime();
                            if (DateTime.TryParse(spQr[12], out ngayDu5Nam))
                            {
                                dtDu5nam.DateTime = ngayDu5Nam;//đủ 5 năm từ ngày cấp
                            }
                            else
                                dtDu5nam.EditValue = null;
                            qrcode = true;
                        }
                        if (!string.IsNullOrWhiteSpace(_ketqua))
                        {
                            idperson = 0;
                            personCode = "";
                            txtMaBN.ResetText();
                            var person = _dataContext.People.FirstOrDefault(o => o.SThe == _ketqua);
                            if (person != null)
                            {
                                idperson = person.IDPerson;
                                personCode = person.PersonCode;
                                txtMaBN.Text = DungChung.Bien.MaBV == "30007" ? person.PersonCode : person.IDPerson.ToString();
                            }
                        }
                        txtMaCS.Focus();
                        loadTTBS(_ketqua);
                    }
                }
                else if (spQr.Length == 17)
                {
                    chkMauTheMoi.Checked = true;
                    qrcode = false;
                    txtTimSTHE_New.Text = spQr[0].Substring(0, 10);//Mã Thẻ
                    _ketqua = txtTimSTHE_New.Text;
                    string muc = "";
                    muc = spQr[14];
                    lupMHuong.EditValue = muc;
                    string tenbn = spQr[1];
                    tenbn = QLBV_Library.QLBV_Ham.Replace_AA(tenbn);
                    txtNhapTBN.Text = QLBV_Library.QLBV_Ham.ConvertHexToString(tenbn);//Tên BN
                    DateTime _namsinh;
                    if (spQr[2] != null)
                    {
                        if (DateTime.TryParse(spQr[2], out _namsinh))
                        {
                            txtNgaySinh.Text = _namsinh.Day.ToString("d2");
                            txtThangSinh.Text = _namsinh.Month.ToString("d2");
                            txtNamSinh.Text = _namsinh.Year.ToString(); //Ngày tháng năm sinh
                        }
                        else
                            txtNamSinh.Text = spQr[2];
                    }
                    if (spQr[3] == "1")
                        radNamNu.SelectedIndex = 0;//giới tính
                    else
                        radNamNu.SelectedIndex = 1;
                    try
                    {
                        string diachi = "";
                        diachi = spQr[4];
                        diachi = QLBV_Library.QLBV_Ham.Replace_AA(diachi);
                        txtDiaChi1.Text = txtDiaChi.Text = QLBV_Library.QLBV_Ham.ConvertHexToString(diachi);//địa chỉ
                    }
                    catch
                    {
                        txtDiaChi1.Text = txtDiaChi.Text = "";
                    }

                    //string macs = "";
                    //var spMaCS = spQr[5].Split('-');
                    //if (spMaCS.Count() == 2)
                    //{
                    //    macs = spMaCS[0].Trim() + spMaCS[1].Trim();
                    //}
                    //txtMaCS.Text = macs;//mã cs

                    if (spQr[5] != null)
                    {
                        txtMaCS.Text = spQr[5].Replace("-", "").Replace(" ", "");
                        //txtMaCS.Text = string.Join("", spQr[5].Split('-').Select(s => s.Trim()).ToList());
                    }

                    DateTime _hantu, _handen, _ngaycap;
                    if (spQr[6] != null)
                        if (DateTime.TryParse(spQr[6], out _hantu))
                        {
                            dtHanBHTu.DateTime = _hantu;//hạn BH từ
                        }
                    if (spQr[7] != null)
                        if (DateTime.TryParse(spQr[7], out _handen))
                        {
                            dtHanBHden.DateTime = _handen;//hạn BH đến
                        }
                    if (spQr[8] != null)
                        if (DateTime.TryParse(spQr[8], out _ngaycap))
                        {
                            dtNgayCapThe.DateTime = _ngaycap;//ngày cấp thẻ
                        }
                    if (!string.IsNullOrEmpty(spQr[12]))
                    {
                        DateTime ngayDu5Nam = new DateTime();
                        if (DateTime.TryParse(spQr[12], out ngayDu5Nam))
                        {
                            dtDu5nam.DateTime = ngayDu5Nam;//đủ 5 năm từ ngày cấp
                        }
                        else
                            dtDu5nam.EditValue = null;
                        qrcode = true;
                    }
                    if (!string.IsNullOrWhiteSpace(_ketqua))
                    {
                        idperson = 0;
                        personCode = "";
                        txtMaBN.ResetText();
                        var person = _dataContext.People.FirstOrDefault(o => o.SThe == _ketqua);
                        if (person != null)
                        {
                            idperson = person.IDPerson;
                            personCode = person.PersonCode;
                            txtMaBN.Text = DungChung.Bien.MaBV == "30007" ? person.PersonCode : person.IDPerson.ToString();
                        }
                    }
                    txtMaCS.Focus();
                    loadTTBS(_ketqua);
                }
                else
                {
                    ApiTheBHYT2018 _sthe = new ApiTheBHYT2018();
                    _sthe.maThe = spQr[0];
                    _sthe.hoTen = spQr[2];
                    string date = spQr[3].Substring(0, 2) + "/" + spQr[3].Substring(2, 2) + "/" + spQr[3].Substring(4, 4);
                    _sthe.ngaySinh = date;
                    ApiToken _token = new ApiToken();
                    _token.username = DungChung.Bien.xmlFilePath_LIS[10];
                    _token.passwword = QLBV_Library.QLBV_Ham.MaHoa(DungChung.Bien.xmlFilePath_LIS[11]);
                    //_token.username = "01071_BV";
                    //_token.passwword = QLBV_Library.QLBV_Ham.MaHoa("BVntl01071");

                    KQNhanLichSuKCBBS n = NhanLSKCBNew_CV3666(_token, _sthe);
                    if (n != null && n.ghiChu != "Thẻ không tồn tại!")
                    {
                        txtTimSTHE.Visible = true;
                        txtTimSTHE_New.Visible = false;
                        // dtNgayN.Text = DateTime.Now.ToString();
                        txtTimSTHE.Text = n.maThe;
                        dtHanBHTu.Text = n.gtTheTu;
                        dtHanBHden.Text = n.gtTheDen;
                        txtNhapTBN.Text = n.hoTen;
                        txtDiaChi1.Text = txtDiaChi.Text = n.diaChi;

                        dtDu5nam.Text = n.ngayDu5Nam;
                        string muc = n.maThe.Substring(2, 1);
                        lupMHuong.EditValue = muc;

                        if (n.gioiTinh == "Nam")
                        {
                            radNamNu.SelectedIndex = 0;
                        }
                        else radNamNu.SelectedIndex = 1;

                        DateTime _namsinh = DateTime.Parse(n.ngaySinh);
                        txtNgaySinh.Text = _namsinh.Day.ToString("d2");
                        txtThangSinh.Text = _namsinh.Month.ToString("d2");
                        txtNamSinh.Text = _namsinh.Year.ToString(); //Ngày tháng
                        txtMaCS.Text = n.maDKBD;
                        _ketqua = txtTimSTHE.Text;
                        if (!string.IsNullOrWhiteSpace(_ketqua))
                        {
                            idperson = 0;
                            personCode = "";
                            txtMaBN.ResetText();
                            var person = _dataContext.People.FirstOrDefault(o => o.SThe == _ketqua);
                            if (person != null)
                            {
                                idperson = person.IDPerson;
                                personCode = person.PersonCode;
                                txtMaBN.Text = DungChung.Bien.MaBV == "30007" ? person.PersonCode : person.IDPerson.ToString();
                            }
                        }

                        loadTTBS(_ketqua);
                        txtMaCS.Focus();
                    }
                    else MessageBox.Show("Thẻ không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (txtNhapTBN.Text == "")
            {
                MessageBox.Show("Nhập họ tên bệnh nhân!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNhapTBN.Focus();
                return;
            }
            if (txtNgaySinh.Text == "")
            {
                MessageBox.Show("Nhập ngày sinh bệnh nhân!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNgaySinh.Focus();
                return;
            }
            if (txtThangSinh.Text == "")
            {
                MessageBox.Show("Nhập tháng sinh bệnh nhân!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtThangSinh.Focus();
                return;
            }
            if (txtNamSinh.Text == "")
            {
                MessageBox.Show("Nhập năm sinh bệnh nhân!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamSinh.Focus();
                return;
            }
            if (txtSoKSinh_CMT.Text == "")
            {
                MessageBox.Show("Nhập số CCCD bệnh nhân!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoKSinh_CMT.Focus();
                return;
            }
            if (txtSoKSinh_CMT.Text.Length != 12)
            {
                MessageBox.Show("Số CCCD không hợp lệ!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoKSinh_CMT.Focus();
                txtSoKSinh_CMT.SelectAll();
                return;
            }

            ApiTheBHYT2018 _sthe = new ApiTheBHYT2018();
            _sthe.maThe = txtSoKSinh_CMT.Text;
            _sthe.hoTen = txtNhapTBN.Text;
            string date = txtNgaySinh.Text + "/" + txtThangSinh.Text + "/" + txtNamSinh.Text;
            _sthe.ngaySinh = date;
            ApiToken _token = new ApiToken();
            _token.username = DungChung.Bien.xmlFilePath_LIS[10];
            _token.passwword = QLBV_Library.QLBV_Ham.MaHoa(DungChung.Bien.xmlFilePath_LIS[11]);
            //_token.username = "01071_BV";
            //_token.passwword = QLBV_Library.QLBV_Ham.MaHoa("BVntl01071");

            KQNhanLichSuKCBBS n = NhanLSKCBNew_CV3666(_token, _sthe);
            if (n != null)
            {
                if (n.ghiChu.Contains("Thẻ không tồn tại!"))
                {
                    MessageBox.Show("Thẻ không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (n.ghiChu.Contains("Họ tên không đúng"))
                {
                    MessageBox.Show("Họ tên không đúng với số CCCD!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (n.ghiChu.Contains("Ngày sinh không đúng"))
                {
                    MessageBox.Show("Ngày sinh không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    txtTimSTHE.Visible = true;
                    txtTimSTHE_New.Visible = false;
                    // dtNgayN.Text = DateTime.Now.ToString();
                    txtTimSTHE.Text = n.maThe;
                    dtHanBHTu.Text = n.gtTheTu;
                    dtHanBHden.Text = n.gtTheDen;
                    txtNhapTBN.Text = n.hoTen;
                    txtDiaChi1.Text = txtDiaChi.Text = n.diaChi;

                    dtDu5nam.Text = n.ngayDu5Nam;
                    string muc = n.maThe.Substring(2, 1);
                    lupMHuong.EditValue = muc;

                    if (n.gioiTinh == "Nam")
                    {
                        radNamNu.SelectedIndex = 0;
                    }
                    else radNamNu.SelectedIndex = 1;

                    DateTime _namsinh = DateTime.Parse(n.ngaySinh);
                    txtNgaySinh.Text = _namsinh.Day.ToString("d2");
                    txtThangSinh.Text = _namsinh.Month.ToString("d2");
                    txtNamSinh.Text = _namsinh.Year.ToString(); //Ngày tháng
                    txtMaCS.Text = n.maDKBD;
                    _ketqua = txtTimSTHE.Text;
                    if (!string.IsNullOrWhiteSpace(_ketqua))
                    {
                        idperson = 0;
                        personCode = "";
                        txtMaBN.ResetText();
                        var person = _dataContext.People.FirstOrDefault(o => o.SThe == _ketqua);
                        if (person != null)
                        {
                            idperson = person.IDPerson;
                            personCode = person.PersonCode;
                            txtMaBN.Text = DungChung.Bien.MaBV == "30007" ? person.PersonCode : person.IDPerson.ToString();
                        }
                    }

                    loadTTBS(_ketqua);
                    txtMaCS.Focus();
                }
            }
        }

        private void txtNgaySinh_Leave_1(object sender, EventArgs e)
        {
            if (txtNgaySinh.Text.Length < 2 && txtNgaySinh.Text != "")
            {
                txtNgaySinh.Text = "0" + txtNgaySinh.Text;
            }
        }

        private void txtThangSinh_Leave(object sender, EventArgs e)
        {
            if (txtThangSinh.Text.Length < 2 && txtThangSinh.Text != "")
            {
                txtThangSinh.Text = "0" + txtThangSinh.Text;
            }
        }

        private void btnUpdateInfoBHYT_Click(object sender, EventArgs e)
        {
            UpdateThongTinBHYTmoi();
        }

        public void UpdateThongTinBHYTmoi()
        {
            if (_ketqua != null && !string.IsNullOrEmpty(txtNhapTBN.Text) && !string.IsNullOrEmpty(txtNamSinh.Text))
            {
                ApiTheBHYT2018 _sthe = new ApiTheBHYT2018();
                _sthe.maThe = _ketqua.ToUpper();
                _sthe.hoTen = txtNhapTBN.Text;
                _sthe.ngaySinh = txtNamSinh.Text;
                if (!string.IsNullOrEmpty(DungChung.Bien.xmlFilePath_LIS[10]) && !string.IsNullOrEmpty(DungChung.Bien.xmlFilePath_LIS[11]))
                {
                    ApiToken _token = new ApiToken();
                    _token.username = DungChung.Bien.xmlFilePath_LIS[10];
                    _token.passwword = QLBV_Library.QLBV_Ham.MaHoa(DungChung.Bien.xmlFilePath_LIS[11]);
                    KQNhanLichSuKCBBS n = NhanLSKCBNew_CV366(_token, _sthe);
                    if (n != null)
                    {
                        string ketqua = n.maTheMoi;
                        string macs = n.maDKBDmoi;
                        string hanTuMoi = n.gtTheTuMoi;
                        string hanDenMoi = n.gtTheDenMoi;
                        string ngayDu5nam = n.ngayDu5Nam;
                        if (ketqua != null)
                        {
                            txtTimSTHE.Text = ketqua;
                            txtMaCS.Text = macs;
                            dtHanBHTu.DateTime = Convert.ToDateTime(hanTuMoi);
                            dtHanBHden.DateTime = Convert.ToDateTime(hanDenMoi);
                            dtDu5nam.DateTime = Convert.ToDateTime(ngayDu5nam);
                            if (DungChung.Bien.MaBV == macs)
                            {
                                radTuyen.SelectedIndex = 0;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Chưa thiết lập tài khoản và mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Nhập đầy đủ thông tin: Số thẻ, Họ tên và năm sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lupNoiNgoaiTinh_EditValueChanged(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "24272")
            {
                //if (lupNoiNgoaiTinh.EditValue.ToString() == "3")
                //{
                //    MessageBox.Show("Không tiếp nhận bệnh nhân có nơi đăng ký khám chữa bệnh ở bhyt từ khu vực ngoại tỉnh đến", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    this.Dispose();
                //}
            }
        }

        private void txtTuoi_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void radTuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            TinhMucHuongNhapMoiBN();
        }

        private void TinhMucHuongNhapMoiBN()
        {
            var dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var bv = dataContext.BenhViens.FirstOrDefault(o => o.MaBV == DungChung.Bien.MaBV);
            if (bv == null) return;
            if (lupMHuong.EditValue != null && !string.IsNullOrWhiteSpace(lupMHuong.Text))
            {
                if (radTuyen.SelectedIndex == 1 || radTuyen.SelectedIndex == 2)
                {
                    cboTyLe.Text = DungChung.Ham.TinhMucHuong(2, lupMHuong.Text, cboKhuVuc.Text, null, bv.HangBV ?? 0, radNoiTru.SelectedIndex, _ketqua, txtMaCS.Text) + "%";
                }
                else if (radTuyen.SelectedIndex == 0)
                {
                    cboTyLe.Text = DungChung.Ham.TinhMucHuong(1, lupMHuong.Text, cboKhuVuc.Text, null, bv.HangBV ?? 0, radNoiTru.SelectedIndex, _ketqua, txtMaCS.Text) + "%";
                }
                else
                {
                    cboTyLe.Text = "";
                }
            }
        }
    }
}