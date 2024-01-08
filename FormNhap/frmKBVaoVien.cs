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
using DevExpress.XtraTab;
//using System.Data.Entity.Core;

namespace QLBV.FormNhap
{
    public partial class frmKBVaoVien : DevExpress.XtraEditors.XtraForm
    {
        int _mabn = 0, _makp = 0;
        bool _vvNgoaiTru;

        string _chuyenkhoa = "";
        public frmKBVaoVien()
        {
            InitializeComponent();
        }
        //  usKhamBenh uskb = new usKhamBenh();
        int _luutd = 0;
        int ttdong = 0;
        public frmKBVaoVien(int mabn)
        {
            _mabn = mabn;
            InitializeComponent();
        }

        public frmKBVaoVien(int mabn, int makp)
        {
            _mabn = mabn;
            _makp = makp;
            InitializeComponent();
        }

        public frmKBVaoVien(int mabn, int makp, bool VVNgoaiTru)
        {
            _mabn = mabn;
            _makp = makp;
            _vvNgoaiTru = VVNgoaiTru;
            InitializeComponent();
        }
        private void Enablebutton(bool Status)
        {
            btnLuu.Enabled = Status;
            btnInPhieu.Enabled = Status;
            //btnSua.Enabled = Status;
            btnThoat.Enabled = Status;
            //groupControl1.Visible = true;
            //groupControl2.Visible = false;
            //groupControl3.Visible = false;
        }
        private void labelControl1_Click(object sender, EventArgs e)
        {

        }
        bool inchuyenkhoa = false;
        List<VaoVien> _vaovien = new List<VaoVien>();
        List<KPhong> _kp = new List<KPhong>();
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        int noitru = -1;
        bool _sua = false;

        int _sovv = 0;
        void setSoVV()
        {
            bool setDTNgoaiTru = true;
            if (DungChung.Bien.MaBV == "01830" && radNoiNgoaiTru.SelectedIndex == 0)
                setDTNgoaiTru = false;
            if (!setDTNgoaiTru)
                return;
            int makp = 0;
            int noingoaitru = -1; // -1: không set; 0: n goại trú; 1 nội trú
            #region (DungChung.Bien.MaBV == "01071" || (DungChung.Bien.MaBV == "01049"
            if (DungChung.Bien.MaBV == "01071" || (DungChung.Bien.MaBV == "01049"))
            {
                noingoaitru = radNoiNgoaiTru.SelectedIndex;

                if (noingoaitru != 1)
                {
                    txtSoVV.Text = "";
                    txtSoBA.Text = "";
                    return;
                }
                string sovaovien = "";
                int nam1 = DateTime.Now.Year;
                var qsopl = DataContect.SoPLs.Where(p => p.PhanLoai == 2 && p.NoiTru == 1 && p.NgayNhap != null && p.NgayNhap.Value.Year == nam1).OrderByDescending(p => p.SoPL1).ToList();
                if (qsopl.Count > 0)
                {
                    _sovv = qsopl.First().SoPL1 + 1;
                }
                else
                    _sovv = 1;
                DateTime ngayvao = dtNgayVao.DateTime;
                int nam = ngayvao.Year, manam = 0;
                if (nam.ToString().Length > 2)
                    manam = Convert.ToInt32(nam.ToString().Substring(nam.ToString().Length - 2, 2));
                if (_sovv > 0)
                {
                    sovaovien = manam.ToString() + _sovv.ToString("D6");
                    txtSoVV.Text = sovaovien;
                    txtSoBA.Text = txtSoVV.Text;
                }
                else
                {
                    txtSoVV.Text = "";
                    txtSoBA.Text = "";
                }

            }
            #endregion
            #region bv khác
            else
            {

                if (DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "20001")
                {
                    noingoaitru = radNoiNgoaiTru.SelectedIndex;
                    if (DungChung.Bien.MaBV == "27021")
                    {
                        if (noingoaitru == 0)
                            txtSoVVPre.Text = "Ng";
                        else if (noingoaitru == 1)
                            txtSoVVPre.Text = "N";
                    }

                }
                else if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "24297")
                {
                    //noingoaitru = radNoiNgoaiTru.SelectedIndex;
                    DateTime ngayvao = dtNgayVao.DateTime;
                    int nam = ngayvao.Year, manam = 0;
                    if (nam.ToString().Length > 2)
                        manam = Convert.ToInt32(nam.ToString().Substring(nam.ToString().Length - 2, 2));
                    txtSoVVPre.Text = "0101071" + manam.ToString();
                }

                else
                    txtSoVVPre.Text = "";


                string sovaovien = "";
                if (DungChung.Bien.PP_SoVV == 1)
                {
                    if (xtraTabControl2.SelectedTabPageIndex == 0)
                    {
                        if (lupKhoaDT.EditValue != null && lupKhoaDT.EditValue.ToString() != "")
                        {
                            makp = Convert.ToInt32(lupKhoaDT.EditValue);
                            _sovv = DungChung.Ham.GetSoPL(2, makp, noingoaitru);
                            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "24297")
                            {
                                sovaovien = _sovv.ToString("D5");
                            }
                            else
                                sovaovien = _sovv.ToString("D6");
                        }
                        else
                        {
                            MessageBox.Show("chưa chọn khoa điều trị, không lấy được số vào viện");

                        }
                    }
                    else
                    {
                        if (lupKhoaDT1.EditValue != null && lupKhoaDT1.EditValue.ToString() != "")
                        {
                            makp = Convert.ToInt32(lupKhoaDT1.EditValue);
                            _sovv = DungChung.Ham.GetSoPL(2, makp, noingoaitru);
                            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789")
                            {
                                sovaovien = _sovv.ToString("D5");
                            }
                            else
                                sovaovien = _sovv.ToString("D6");
                        }
                        else
                        {
                            MessageBox.Show("chưa chọn khoa điều trị, không lấy được số vào viện");

                        }
                    }
                }
                else if (DungChung.Bien.PP_SoVV == 2)
                {
                    _sovv = DungChung.Ham.GetSoPL(2, 0, noingoaitru);
                    if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "24297")
                    {
                        sovaovien = _sovv.ToString("D5");
                    }
                    else
                        sovaovien = _sovv.ToString("D6");
                }
                txtSoVV.Text = sovaovien;


                // số ba
            }
            #endregion

        }
        void setSoBA()
        {
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                return;
            bool setDTNgoaiTru = true;
            int noingoaitru = -1;
            if (DungChung.Bien.MaBV == "01830" && radNoiNgoaiTru.SelectedIndex == 1)
                noingoaitru = 1;
            if (DungChung.Bien.MaBV == "01830" && radNoiNgoaiTru.SelectedIndex == 0)
                setDTNgoaiTru = false;
            if (!setDTNgoaiTru)
                return;
            string soba = "";
            if (DungChung.Bien.PP_SoBA == 1)
            {
                int makp = 0;

                if (xtraTabControl2.SelectedTabPageIndex == 0)
                {
                    if (lupKhoaDT.EditValue != null && lupKhoaDT.EditValue.ToString() != "")
                    {
                        makp = Convert.ToInt32(lupKhoaDT.EditValue);

                        soba = DungChung.Ham.GetSoPL(4, makp, noingoaitru).ToString();
                    }
                    else
                    {
                        MessageBox.Show("chưa chọn khoa điều trị, không lấy được số bệnh án");
                    }
                }
                else
                {
                    if (lupKhoaDT1.EditValue != null && lupKhoaDT1.EditValue.ToString() != "")
                    {
                        makp = Convert.ToInt32(lupKhoaDT1.EditValue);

                        soba = DungChung.Ham.GetSoPL(4, makp, noingoaitru).ToString();
                    }
                    else
                    {
                        MessageBox.Show("chưa chọn khoa điều trị, không lấy được số bệnh án");
                    }
                }
            }
            else if (DungChung.Bien.PP_SoBA == 2)
            {
                soba = DungChung.Ham.GetSoPL(4, 0, noingoaitru).ToString();
            }
            txtSoBA.Text = soba;

        }
        bool TE = false;
        private void frmKBVaoVien_Load(object sender, EventArgs e)
        {
            if(DungChung.Bien.MaBV == "24012")
            {
                txtSoVV.Enabled = false;
                txtSoBA.Enabled = false;
            }
            DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var _lKhamBenh = DataContect.BNKBs.Where(p => p.MaBNhan == _mabn).OrderByDescending(p => p.IDKB).ToList();
            if (DungChung.Bien.MaBV == "01830" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                txtSoVV.Properties.ReadOnly = true;
                txtSoBA.Properties.ReadOnly = true;
            }

            if (DungChung.Bien.MaBV == "14018")
            {
                txtNhietDo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtNhietDo.Properties.Mask.EditMask = "n1";
                txtChieuCao.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtChieuCao.Properties.Mask.EditMask = "n1";
                txtCanNang.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtCanNang.Properties.Mask.EditMask = "n2";

            }

            //if (DungChung.Bien.MaBV == "20001")
            //{
            //    txtSoVV.Properties.MaxLength = 6;
            //    txtSoVV.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //    txtSoVV.Properties.DisplayFormat.FormatString = "D6";
            //    txtSoVV.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //    txtSoVV.Properties.EditFormat.FormatString = "D6";
            //}


            if (DungChung.Bien.MaBV == "30007")
            {
                xtraTabControl2.SelectedTabPageIndex = 1;
                xtraTabPage1.PageVisible = false;
                xtraTabPage2.PageVisible = true;
                var qdiung = DataContect.TienSuDiUngs.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                if (qdiung != null)
                {
                    txtDiUng11.Text = qdiung.Thuoc;
                    txtDiUng13.Text = qdiung.Thuoc_MoTa;
                    if (qdiung.Thuoc_SL != null)
                        txtDiUng12.Text = qdiung.Thuoc_SL.ToString();

                    txtDiUng21.Text = qdiung.ConTrung;
                    txtDiUng23.Text = qdiung.ConTrung_MoTa;
                    if (qdiung.ConTrung_SL != null)
                        txtDiUng22.Text = qdiung.ConTrung_SL.ToString();

                    txtDiUng31.Text = qdiung.ThucPham;
                    txtDiUng33.Text = qdiung.ThucPham_MoTa;
                    if (qdiung.ThucPham_SL != null)
                        txtDiUng32.Text = qdiung.ThucPham_SL.ToString();

                    txtDiUng41.Text = qdiung.Khac;
                    txtDiUng43.Text = qdiung.Khac_MoTa;
                    if (qdiung.Khac_SL != null)
                        txtDiUng42.Text = qdiung.Khac_SL.ToString();

                    txtDiUng51.Text = qdiung.TienSuBanThan;
                    txtDiUng53.Text = qdiung.TienSuBanThan_MoTa;
                    if (qdiung.TienSuBanThan_SL != null)
                        txtDiUng52.Text = qdiung.TienSuBanThan_SL.ToString();

                    txtDiUng61.Text = qdiung.TienSuGiaDinh;
                    txtDiUng63.Text = qdiung.TienSuGiaDinh_MoTa;
                    if (qdiung.TienSuGiaDinh_SL != null)
                        txtDiUng62.Text = qdiung.TienSuGiaDinh_SL.ToString();
                }
            }
            else
            {
                xtraTabControl2.SelectedTabPageIndex = 0;
                xtraTabPage1.PageVisible = true;
                xtraTabPage2.PageVisible = false;
            }
            TTboXung _ttbx = DataContect.TTboXungs.Where(p => p.MaBNhan == _mabn).Select(p => p).FirstOrDefault();
            dtNgayVao.DateTime = System.DateTime.Now;
            _vaovien = DataContect.VaoViens.Where(p => p.MaBNhan == _mabn).ToList();

            if (_vaovien.Count > 0)
            {
                lupKhoaDT.Enabled = false;
                lupKhoaDT1.Enabled = false;
                if (_vaovien.First().NgayVao != null)
                    dtNgayVao.DateTime = _vaovien.First().NgayVao.Value;
                int idkb = _vaovien.First().IDKB;
                var makp = DataContect.BNKBs.Where(p => p.IDKB == idkb).ToList();
                if (makp.Count > 0 && makp.First().MaKP == _makp)
                    btnXoa.Enabled = true;

            }
            else
            {
                if (_lKhamBenh.Where(p => p.PhuongAn == 1).ToList().Count > 0 && _lKhamBenh.Where(p => p.PhuongAn == 1).First().NgayNghi != null)
                    dtNgayVao.DateTime = _lKhamBenh.Where(p => p.PhuongAn == 1).First().NgayNghi.Value;
                if (_ttbx != null)
                {
                    if (!string.IsNullOrEmpty(_ttbx.CanNang_ChieuCao) && _ttbx.CanNang_ChieuCao.Contains(";"))
                    {
                        string[] arrcannang = _ttbx.CanNang_ChieuCao.Split(';');

                        if (arrcannang != null && arrcannang.Length > 0)
                            txtCanNang.Text = arrcannang[0];
                        if (arrcannang != null && arrcannang.Length > 1)
                            txtChieuCao.Text = arrcannang[1];
                    }
                    if (!string.IsNullOrEmpty(_ttbx.Mach_NDo_HAp) && _ttbx.Mach_NDo_HAp.Contains(";"))
                    {
                        string[] machNDHA = _ttbx.Mach_NDo_HAp.Split(';');
                        if (machNDHA != null && machNDHA.Length > 0)
                            txtMach.Text = machNDHA[0];
                        if (machNDHA != null && machNDHA.Length > 1)
                            txtNhietDo.Text = machNDHA[1];
                        if (machNDHA != null && machNDHA.Length > 2)
                            txtHuyetAp.Text = machNDHA[2];
                        if (machNDHA != null && machNDHA.Length > 3)
                            txtNhipTho.Text = machNDHA[3];
                    }

                }
                btnXoa.Enabled = false;
            }
            //radNoiNgoaiTru.SelectedIndex = 1;
            DateTime _ngaytu = System.DateTime.Now;
            int _sovv = 0;

            // thay đổi lại cách lấy số vào viện

            if (DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                txtSoBA.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            }
            else
                txtSoBA.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;

            if (DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                txtSoVV.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                    txtSoVVPre.Visible = false;
                else
                    txtSoVVPre.Visible = true;
            }
            else
            {
                txtSoVV.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                txtSoVVPre.Visible = false;
            }



            if (DungChung.Bien.MaBV == "30012" || DungChung.Bien.MaBV == "12121")
                labelControl33.Text = "3. Tóm tắt kết quả: ";
            var bn = DataContect.BenhNhans.Where(p => p.MaBNhan == _mabn).ToList();
            if (bn.Count > 0)
            {
                DateTime _NgaySinh = DateTime.Now;
                try
                {
                    _NgaySinh = Convert.ToDateTime((string.IsNullOrEmpty(bn.First().NgaySinh) ? "01" : bn.First().NgaySinh) + "/" + (string.IsNullOrEmpty(bn.First().ThangSinh) ? "01" : bn.First().ThangSinh) + "/" + bn.First().NamSinh);
                }
                catch
                {
                    _NgaySinh = Convert.ToDateTime("01/01/" + bn.First().NamSinh);
                }
                DateTime _NgayVao = bn.First().NNhap.Value;
                if ((_NgayVao - _NgaySinh).TotalDays < 365)
                    TE = true;
                noitru = bn.First().NoiTru.Value;
                string mdatoc = _ttbx != null ? _ttbx.MaDT : "";
                if (DungChung.Bien.MaBV == "01049")
                {
                    var dtoc = (from dt in DataContect.DanTocs.Where(p => p.MaDTBak == mdatoc)
                                select new { dt.TenDT }).ToList();
                    if (dtoc.Count > 0)
                        txtTenDT.Text = dtoc.First().TenDT;
                }
                else
                {
                    var dtoc = (from dt in DataContect.DanTocs.Where(p => p.MaDT == mdatoc) select new { dt.TenDT }).ToList();
                    if (dtoc.Count > 0)
                        txtTenDT.Text = dtoc.First().TenDT;
                }
                txtTenBNhan.Text = bn.First().TenBNhan;
                txtNamSinh.Text = bn.First().NamSinh;
                if (bn.First().GTinh == 1)
                {
                    txtGTinh.Text = "Nam";
                }
                else txtGTinh.Text = "Nữ";
                if (bn.First().Tuoi != null)
                    txtTuoi.Text = bn.First().Tuoi.ToString();
                txtDChi.Text = bn.First().DChi;
                txtSThe.Text = bn.First().SThe;
                txtMaBNhan.Text = bn.First().MaBNhan.ToString();
                txtNgayVao.DateTime = bn.First().NNhap.Value;

                txtCDNoiGT.Text = bn.First().CDNoiGT;
            }

            #region sửa vào viện
            if (_vaovien.Count > 0)
            {
                radNoiNgoaiTru.SelectedIndex = bn.First().NoiTru.Value;
                dtNgayVao.DateTime = _vaovien.First().NgayVao.Value;
                txtLyDo.Text = _vaovien.First().LyDo;
                txtBenhLy.Text = _vaovien.First().BenhLy;
                txtBenhLy1.Text = _vaovien.First().BenhLy;
                txtTienSuBT.Text = _vaovien.First().TienSuBT;
                txtTienSuGD.Text = _vaovien.First().TienSuGD;
                txtTienSuBT1.Text = _vaovien.First().TienSuBT;
                txtTienSuGD1.Text = _vaovien.First().TienSuGD;
                txtMach.Text = _vaovien.First().Mach;
                txtNhietDo.Text = _vaovien.First().NhietDo;
                txtHuyetAp.Text = _vaovien.First().HuyetAp;
                txtNhipTho.Text = _vaovien.First().NhipTho;
                txtCanNang.Text = _vaovien.First().CanNang;
                txtChieuCao.Text = _vaovien.First().ChieuCao;
                txtTai.Text = _vaovien.First().Tai;
                txtMui.Text = _vaovien.First().Mui;
                txtHong.Text = _vaovien.First().Hong;
                txtKKmp.Text = _vaovien.First().MatPKK;
                txtKKmt.Text = _vaovien.First().MatTKK;
                txtCKmp.Text = _vaovien.First().MatP;
                txtCKmt.Text = _vaovien.First().MatT;
                txtNAmp.Text = _vaovien.First().NhanApP;
                ckDiUng1.Text = _vaovien.First().NhanApT;
                txtKhamTThan.Text = _vaovien.First().KhamTThan;
                txtKhamBPhan.Text = _vaovien.First().KhamBPhan;
                txtkQLamSang.Text = _vaovien.First().kQLamSang;
                txtDaXuLy.Text = _vaovien.First().DaXuLy;
                txtKhamTThan1.Text = _vaovien.First().KhamTThan;
                txtKhamBPhan1.Text = _vaovien.First().KhamBPhan;
                txtkQLamSang1.Text = _vaovien.First().kQLamSang;
                txtDaXuLy1.Text = _vaovien.First().DaXuLy;

                txtChuY.Text = _vaovien.First().ChuY;
                txtNhomMau.Text = _vaovien.First().NhomMau;
                if (_vaovien.First().MaKP != null)
                {
                    lupKhoaDT.EditValue = _vaovien.First().MaKP;
                    lupKhoaDT1.EditValue = _vaovien.First().MaKP;
                }
                else
                {
                    lupKhoaDT.EditValue = DungChung.Bien.MaKP;
                    lupKhoaDT1.EditValue = DungChung.Bien.MaKP;
                }

                _chuyenkhoa = _vaovien.First().ChuyenKhoa;
                if (_vaovien.First().SoBA != null)
                    txtSoBA.Text = _vaovien.First().SoBA;
                if (_vaovien.First().SoVV != null)
                {
                    if (DungChung.Bien.MaBV == "27021")
                    {
                        if (radNoiNgoaiTru.SelectedIndex == 0)
                            txtSoVVPre.Text = "Ng";
                        else
                            txtSoVVPre.Text = "N";
                        txtSoVV.Text = _vaovien.First().SoVV;

                    }
                    else if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                    {
                        string sovv = _vaovien.First().SoVV;
                        if (sovv.Length >= 14)
                        {
                            txtSoVVPre.Text = sovv.Substring(0, 8);
                            txtSoVV.Text = sovv.Substring(8);
                        }
                        else
                            txtSoVV.Text = sovv;
                        //string[] arr = _vaovien.First().SoVV.Split('-');
                        //if (arr.Length > 1)
                        //{
                        //    txtSoVVPre.Text = arr[0];
                        //    txtSoVV.Text = arr[1];
                        //}
                        //else
                        //{
                        //    txtSoVVPre.Text = Convert.ToString(_vaovien.First().NgayVao.Value.Year);
                        //    txtSoVV.Text = arr[0];
                        //}
                    }
                    else
                        txtSoVV.Text = _vaovien.First().SoVV;
                }
                else
                    txtSoVV.Text = _sovv.ToString();
                //txtTenICD.Text= _vaovien.First().


            }

            #endregion
            #region thêm mới vào viện
            else
            {
                _ngaytu = DungChung.Ham.NgayDen(dtNgayVao.DateTime);
                string _ketqua = "";
                if (DungChung.Bien.MaBV != "30303")
                {
                    var kqcls = (from cls in DataContect.CLS.Where(p => p.NgayTH <= _ngaytu).Where(p => p.MaBNhan == _mabn)
                                 join chidinh in DataContect.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                                 join clsct in DataContect.CLScts on chidinh.IDCD equals clsct.IDCD
                                 join dv in DataContect.DichVus on chidinh.MaDV equals dv.MaDV
                                 join tn in DataContect.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                 join nhom in DataContect.NhomDVs on tn.IDNhom equals nhom.IDNhom
                                 where (nhom.TenNhomCT == ("Chẩn đoán hình ảnh"))
                                 select new { dv.TenDV, clsct.KetQua }).ToList();
                    foreach (var a in kqcls)
                    {
                        _ketqua += a.KetQua + ". ";
                    }
                    if (DungChung.Bien.MaBV == "30009")
                    {
                        var kqcls_XN = (from cls in DataContect.CLS.Where(p => p.NgayTH <= _ngaytu).Where(p => p.MaBNhan == _mabn)
                                        join chidinh in DataContect.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                                        join clsct in DataContect.CLScts on chidinh.IDCD equals clsct.IDCD
                                        join dvct in DataContect.DichVucts on clsct.MaDVct equals dvct.MaDVct
                                        join dv in DataContect.DichVus on dvct.MaDV equals dv.MaDV
                                        join tn in DataContect.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                        join nhom in DataContect.NhomDVs on tn.IDNhom equals nhom.IDNhom
                                        where (nhom.TenNhomCT == ("Xét nghiệm"))
                                        select new { tn.IdTieuNhom, dv.TenDV, dvct.TenDVct, clsct.KetQua }).OrderBy(p => p.IdTieuNhom).ThenBy(p => p.TenDV).ToList();
                        _ketqua += "\n";
                        foreach (var a in kqcls_XN)
                        {
                            _ketqua += a.TenDVct + ": " + a.KetQua + ". ";
                        }
                    }
                }
                txtkQLamSang.Text = _ketqua;
                txtkQLamSang1.Text = _ketqua;
                int makpvv = 0;
                if (DungChung.Bien.MaBV != "30010")
                {
                    txtTai.Text = "Ống tai sạch, màng nhĩ sáng.";
                    txtMui.Text = "Cuốn mũi bình thường, sàn mũi sạch.";
                    txtHong.Text = "Niêm mạc bình thường, thành sau họng có nhiều tổ chức Limphô.";
                }


            }

            #endregion
            if (_vvNgoaiTru && _makp > 0)
            {
                var q = from TK in DataContect.KPhongs.Where(p => p.MaKP == _makp) select new { TK.TenKP, TK.MaKP };
                lupKhoaDT.Properties.DataSource = q.ToList();
                lupKhoaDT1.Properties.DataSource = q.ToList();
                lupPhongKham.Properties.DataSource = q.ToList();
            }
            else
            {
                var q = from TK in DataContect.KPhongs.Where(p => p.PLoai == ("Lâm sàng")) select new { TK.TenKP, TK.MaKP };
                lupKhoaDT.Properties.DataSource = q.ToList();
                lupKhoaDT1.Properties.DataSource = q.ToList();
                lupPhongKham.Properties.DataSource = q.ToList();
            }

            
            var tenkp = DataContect.BNKBs.Where(p => p.MaBNhan == _mabn).Where(p => p.PhuongAn == 1).OrderByDescending(p => p.IDKB).ToList();
            if (tenkp.Count > 0 && DungChung.Bien.MaBV != "24012" && DungChung.Bien.MaBV != "24272")
            {
                lupPhongKham.EditValue = tenkp.First().MaKP;
                lupKhoaDT.EditValue = tenkp.First().MaKPdt;
                lupKhoaDT1.EditValue = tenkp.First().MaKPdt;
                if (_vaovien.Count <= 0)
                    radNoiNgoaiTru.SelectedIndex = 2;


                txtTenICD.Text = DungChung.Ham.GetChanDoanKB_ByKP(DataContect, _mabn, Convert.ToInt32(tenkp.First().MaKP));
                txtTenICD1.Text = txtTenICD.Text;
                if (string.IsNullOrEmpty(_chuyenkhoa))
                {
                    _chuyenkhoa = DungChung.Bien._lChuyenKhoa.Where(p => p.MaCK == tenkp.First().MaCK).Select(p => p.ChuyenKhoa).FirstOrDefault();
                }

            }
            else
            {
                var tenkp2 = DataContect.BNKBs.Where(p => p.MaBNhan == _mabn).OrderBy(p => p.IDKB).ToList();
                if (tenkp2.Count > 0)
                {
                    lupPhongKham.EditValue = tenkp2.First().MaKP;
                    if(tenkp2.First().MaKPdt > 0)
                    {
                        lupKhoaDT.EditValue = tenkp2.First().MaKPdt;
                        lupKhoaDT1.EditValue = tenkp2.First().MaKPdt;
                    }

                    else
                    {
                        lupKhoaDT.EditValue = _makp;
                    }
                    
                    if (_vaovien.Count <= 0)
                        radNoiNgoaiTru.SelectedIndex = 1;
                    if (_vaovien.Count <= 0 && _vvNgoaiTru)
                        radNoiNgoaiTru.SelectedIndex = 0;
                    txtTenICD.Text = DungChung.Ham.GetICDstr(tenkp2.First().ChanDoan + ";" + tenkp2.First().BenhKhac);
                    txtTenICD1.Text = txtTenICD.Text;
                    if (string.IsNullOrEmpty(_chuyenkhoa))
                        _chuyenkhoa = DungChung.Bien._lChuyenKhoa.Where(p => p.MaCK == tenkp2.First().MaCK).Select(p => p.ChuyenKhoa).FirstOrDefault();
                }
            }
            var tenkpvv = DataContect.VaoViens.Where(p => p.MaBNhan == _mabn).OrderByDescending(p => p.idVaoVien).ToList();
            if (tenkpvv.Count > 0)
            {

                lupKhoaDT.EditValue = tenkpvv.First().MaKP;
                lupKhoaDT1.EditValue = tenkpvv.First().MaKP;
            }
            //if (_vaovien.Count <= 0)
            //{
            //    setSoVV();
            //    setSoBA();
            //}
            Enablebutton(true);
            txtLyDo.Focus();
            //if (DungChung.Bien.MaBV == "30009")
            inchuyenkhoa = true;
            if (_chuyenkhoa == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKTaiMuiHong && inchuyenkhoa)
            {

                xtraTabControl1.SelectedTabPageIndex = 1;
                ((Control)this.xtraTabMat).Enabled = false;
                txtKhamBPhan.Properties.ReadOnly = true;
                txtKhamBPhan1.Properties.ReadOnly = true;
                txtGD.Text = "Bệnh chuyên khoa";
            }
            if (_chuyenkhoa == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKMat && inchuyenkhoa)
            {

                ((Control)this.xtraTabTMH).Enabled = false;
                xtraTabControl1.SelectedTabPageIndex = 0;
                txtKhamBPhan.Properties.ReadOnly = true;
                txtKhamBPhan1.Properties.ReadOnly = true;
                txtGD.Text = "Bệnh chuyên khoa";
            }
            if (_chuyenkhoa == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKRangHamMat && inchuyenkhoa)
            {

                txtKhamBPhan.Properties.ReadOnly = true;
                txtKhamBPhan1.Properties.ReadOnly = true;
                txtGD.Text = "Bệnh chuyên khoa";
            }

            //else 
            //{
            //    groupControl1.Visible = true;
            //    groupControl2.Visible = false;
            //    groupControl3.Visible = false;
            //    txtGD.Visible = true;
            //    txtBCK.Visible = false; 
            //}

            if (_vaovien.Count > 0)
            {
                btnLuu.Enabled = false;
                var makp = _vaovien.First().MaKP;
                var bnkbKT = DataContect.BNKBs.FirstOrDefault(o => o.MaBNhan == _mabn && o.MaKP == makp);
                if (bnkbKT != null)
                {
                    lupKhoaDT.Enabled = false;
                    lupKhoaDT1.Enabled = false;
                }
                else
                {
                    lupKhoaDT.Enabled = true;
                    lupKhoaDT1.Enabled = true;
                }
            }
            else
            {
                btnInPhieu.Enabled = false;
                if (DungChung.Bien.MaBV == "20001")
                    txtSoVV.Text = "000001";
            }
            if (String.IsNullOrEmpty(_chuyenkhoa))
                labKBVV.Text = "KHÁM BỆNH VÀO VIỆN";
            else
                labKBVV.Text = "KHÁM BỆNH VÀO VIỆN - " + _chuyenkhoa.ToUpper();
            _sua = true;
            if (DungChung.Bien.MaBV == "24297")
            {
                txtTenICD.Properties.ReadOnly = false;
                if (_vaovien.Count <= 0)
                    radNoiNgoaiTru.SelectedIndex = 0;
                else
                    txtTenICD.Text = _vaovien.First().ChanDoan;
            }
        }

        private void DisableAllControls(Control container)
        {
            foreach (Control c in container.Controls)
            {
                DisableAllControls(c);
                if (!(c is XtraTabControl) && c.Name != "btnInPhieu" && c.Name != "btnThoat" && !(c is PanelControl))
                {
                    c.Enabled = false;
                }
            }
        }

        private bool KTLuu()
        {
            DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var ravien = DataContect.RaViens.Where(p => p.MaBNhan == _mabn).ToList();
            if (ravien.Count > 0)
            {
                MessageBox.Show("Bệnh nhân đã ra viện, bạn không thể lưu");
                return false;
            }
            if (xtraTabControl2.SelectedTabPageIndex == 0)
            {
                if (string.IsNullOrEmpty(lupKhoaDT.Text))
                {
                    MessageBox.Show("Bạn chưa chọn Khoa điều trị");
                    lupKhoaDT.Focus();
                    return false;
                }
            }
            //var vaovien = DataContect.VaoViens.Where(p => p.MaBNhan == _mabn).ToList();
            //if (vaovien.Count > 0)
            //{
            //    MessageBox.Show("Bệnh nhân đã vào viện, bạn không thể lưu");
            //    return false;
            //}
            else
            {
                if (string.IsNullOrEmpty(lupKhoaDT1.Text))
                {
                    MessageBox.Show("Bạn chưa chọn Khoa điều trị");
                    lupKhoaDT1.Focus();
                    return false;
                }
            }
            var kt = DataContect.BNKBs.Where(p => p.MaBNhan == _mabn).OrderBy(p => p.IDKB).ToList();
            if (dtNgayVao.DateTime < kt.First().NgayKham.Value) //(dtNgayVao.DateTime.Date - kt.First().NgayKham.Value.Date).Days < 0)
            {
                MessageBox.Show("Ngày vào viện không được nhỏ hơn ngày khám bệnh");
                dtNgayVao.Focus();
                return false;
            }
            if (TE && string.IsNullOrEmpty(txtCanNang.Text))
            {
                MessageBox.Show("Trẻ e dưới 1 tuổi bắt buộc phải nhập cân nặng!");
                txtCanNang.Focus();
                return false;
            }
            if (DungChung.Bien.MaBV == "12122")
            {
                var _lcls = (from a in DataContect.CLS.Where(p => p.MaBNhan == _mabn)
                             join b in DataContect.KPhongs.Where(p => p.PLoai.Contains("Phòng khám")) on a.MaKP equals b.MaKP
                             select a).OrderByDescending(p => p.NgayThang).ToList();
                if (_lcls.Count > 0)
                {
                    if (dtNgayVao.DateTime < _lcls.First().NgayThang.Value)
                    {
                        MessageBox.Show("Bệnh nhân vào viện, ngày vào không được nhỏ hơn ngày chỉ định CLS");
                        dtNgayVao.Focus();
                        return false;

                    }
                }
            }
            //if (xtraTabControl2.SelectedTabPageIndex == 1)
            //{
            //    if (string.IsNullOrEmpty(txtDiUng12.Text) && string.IsNullOrEmpty(txtDiUng11.Text) && string.IsNullOrEmpty(txtDiUng21.Text) && string.IsNullOrEmpty(txtDiUng31.Text) && string.IsNullOrEmpty(txtDiUng41.Text) && string.IsNullOrEmpty(txtDiUng51.Text) && string.IsNullOrEmpty(txtDiUng61.Text))
            //    {
            //        MessageBox.Show("Bệnh nhân chưa nhập thông tin tiền sử dị ứng");
            //        txtDiUng11.Focus();
            //        return false;
            //    }

            //}
            return true;
        }
        void setDBsoVV()
        {
            //bool kq = true;
            if (DungChung.Bien.MaBV == "01830" && radNoiNgoaiTru.SelectedIndex == 0)
                return;
            if (DungChung.Bien.PP_SoVV == 1 || DungChung.Bien.PP_SoVV == 2)
            {
                int rs, sovaovien = 0, makpvv = 0;
                int noingoaitru = -1;
                if (DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                    noingoaitru = radNoiNgoaiTru.SelectedIndex;
                if ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049"))
                {
                    if (noingoaitru == 1)
                    {
                        int nam = DateTime.Now.Year;
                        var qsopl = DataContect.SoPLs.Where(p => p.PhanLoai == 2 && p.NoiTru == 1 && p.NgayNhap != null && p.NgayNhap.Value.Year == nam).OrderByDescending(p => p.SoPL1).ToList();
                        if (qsopl.Count == 0)
                        {

                            int soht = _sovv - 1;
                            var qvv = DataContect.SoPLs.Where(p => p.PhanLoai == 2 && p.SoPL1 == soht && p.NgayNhap != null && p.NgayNhap.Value.Year == nam && p.NoiTru == 1).ToList();
                            if (qvv.Count > 0 || _sovv == 1)
                            {
                                foreach (var a in qvv)
                                {
                                    DataContect.SoPLs.Remove(a);
                                }

                                DataContect.SaveChanges();
                                SoPL soPLMoi = new SoPL();
                                soPLMoi.SoPL1 = _sovv;
                                soPLMoi.Status = 1;
                                soPLMoi.PhanLoai = 2;
                                soPLMoi.NgayNhap = DateTime.Now;
                                soPLMoi.NoiTru = 1;
                                DataContect.SoPLs.Add(soPLMoi);
                                DataContect.SaveChanges();
                            }

                        }
                        else
                        {
                            setSoVV();
                            int soht = _sovv - 1;
                            var qvv = DataContect.SoPLs.Where(p => p.PhanLoai == 2 && p.SoPL1 == soht && p.NgayNhap != null && p.NgayNhap.Value.Year == nam && p.NoiTru == 1).ToList();
                            if (qvv.Count > 0)
                            {
                                foreach (var a in qvv)
                                {
                                    DataContect.SoPLs.Remove(a);
                                }
                                DataContect.SaveChanges();
                                SoPL soPLMoi = new SoPL();
                                soPLMoi.SoPL1 = _sovv;
                                soPLMoi.Status = 1;
                                soPLMoi.PhanLoai = 2;
                                soPLMoi.NgayNhap = DateTime.Now;
                                soPLMoi.NoiTru = 1;
                                DataContect.SoPLs.Add(soPLMoi);
                                DataContect.SaveChanges();
                            }
                        }


                    }
                    else
                        return;
                }
                else
                {
                    if (xtraTabControl2.SelectedTabPageIndex == 0)
                    {
                        if (DungChung.Bien.PP_SoVV == 1 && lupKhoaDT.EditValue != null && lupKhoaDT.EditValue.ToString() != "")
                            makpvv = Convert.ToInt32(lupKhoaDT.EditValue);
                    }
                    else
                    {
                        if (DungChung.Bien.PP_SoVV == 1 && lupKhoaDT1.EditValue != null && lupKhoaDT1.EditValue.ToString() != "")
                            makpvv = Convert.ToInt32(lupKhoaDT1.EditValue);
                    }
                    if (DungChung.Bien.PP_SoVV == 1 || DungChung.Bien.PP_SoVV == 2)
                    {
                        //if (Int32.TryParse(txtSoVV.Text, out rs))
                        //{
                        //    sovaovien = _sovv;//Convert.ToInt32(txtSoVV.Text);
                        //}
                        if (DungChung.Ham.checkSoPL(makpvv, _sovv, 2, noingoaitru) == false)
                        {
                            DungChung.Ham.SetSoPL(makpvv, _sovv, 2, noingoaitru);
                        }
                        else
                        {
                            //DungChung.Ham.SetSoPL(makpvv, sovaovien, 2, noingoaitru);
                            setSoVV();
                            DungChung.Ham.SetSoPL(makpvv, _sovv, 2, noingoaitru);
                        }

                    }
                }
            }
            //return kq;
        }
        void setDBSoBA()
        {
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                return;
            if (DungChung.Bien.MaBV == "01830" && radNoiNgoaiTru.SelectedIndex == 0)
                return;

            int noingoaitru = -1;
            if (DungChung.Bien.MaBV == "01830" && radNoiNgoaiTru.SelectedIndex == 1)
                noingoaitru = 1;
            if (DungChung.Bien.PP_SoBA == 1 || DungChung.Bien.PP_SoBA == 2)
            {
                int rs, soba = 0, makpvv = 0;
                if (xtraTabControl2.SelectedTabPageIndex == 0)
                {
                    if (DungChung.Bien.PP_SoBA == 1 && lupKhoaDT.EditValue != null && lupKhoaDT.EditValue.ToString() != "")
                        makpvv = Convert.ToInt32(lupKhoaDT.EditValue);
                }
                else
                {
                    if (DungChung.Bien.PP_SoBA == 1 && lupKhoaDT1.EditValue != null && lupKhoaDT1.EditValue.ToString() != "")
                        makpvv = Convert.ToInt32(lupKhoaDT1.EditValue);
                }
                if (Int32.TryParse(txtSoBA.Text, out rs))
                {
                    soba = Convert.ToInt32(txtSoBA.Text);
                }

                if (!DungChung.Ham.checkSoPL(makpvv, soba, 4, noingoaitru))
                {
                    DungChung.Ham.SetSoPL(makpvv, soba, 4, noingoaitru);
                }
                else
                {
                    DungChung.Ham.SetSoPL(makpvv, 0, 4, noingoaitru);
                    setSoBA();
                }
                // }
            }
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            int ot;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out ot))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            luutiep = true;
            DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //try
            //{
            var kp = DataContect.BNKBs.Where(p => p.MaBNhan == _mabn).OrderByDescending(p => p.IDKB).ToList();
            int idkb = 0;
            if (_makp > 0)
            {
                if(DungChung.Bien.MaBV == "24012" && _vvNgoaiTru)
                {
                    var kpLS = DataContect.KPhongs.FirstOrDefault(o => o.MaKP == _makp);
                    if (kpLS != null)
                    {
                        var bnkb = kp.Where(p => p.PhuongAn == 4 || p.PhuongAn == 1).FirstOrDefault();
                        if (bnkb != null)
                            idkb = bnkb.IDKB;
                    }
                }
                else
                {
                    var kpLS = DataContect.KPhongs.FirstOrDefault(o => o.MaKP == _makp);
                    if (kpLS != null && kpLS.PLoai == "Lâm sàng")
                    {
                        var bnkb = kp.Where(p => p.PhuongAn == 4 || p.PhuongAn == 1).FirstOrDefault();
                        if (bnkb != null)
                            idkb = bnkb.IDKB;
                    }
                }
               

            }
            else
            {
                if (kp.Where(p => p.PhuongAn == 1).FirstOrDefault() != null)
                    idkb = kp.Where(p => p.PhuongAn == 1).FirstOrDefault().IDKB;
            }
            // luu bang VaoVien
            if (_int_maBN > 0 && KTLuu())
            {
                //_mabn = txtMaBNhan.Text;
                var kt = DataContect.VaoViens.Where(p => p.MaBNhan == _mabn).ToList();
                if (kt.Count > 0)
                {
                    //sửa
                    int id = kt.First().idVaoVien;
                    VaoVien nhapvv = DataContect.VaoViens.Where(p => p.idVaoVien == id).FirstOrDefault();
                    BNKB bnkb = DataContect.BNKBs.FirstOrDefault(o => o.IDKB == nhapvv.IDKB);
                    nhapvv.ChuyenKhoa = _chuyenkhoa;
                    nhapvv.MaCK = DungChung.Bien._lChuyenKhoa.Where(p => p.ChuyenKhoa == _chuyenkhoa).Select(p => p.MaCK).FirstOrDefault();
                    nhapvv.LyDo = txtLyDo.Text;
                    nhapvv.Mach = txtMach.Text.Trim();
                    nhapvv.NhietDo = txtNhietDo.Text.Trim();
                    nhapvv.HuyetAp = txtHuyetAp.Text.Trim();
                    nhapvv.NhipTho = txtNhipTho.Text.Trim();
                    //string[] arr1 = { "", "" };
                    //if (txtCanNang.Text != "" && txtCanNang.Text != ".") arr1 = txtCanNang.Text.Split('.');
                    //if (arr1[0] == "01") arr1[0] = "1"; if (arr1[0] == "02") arr1[0] = "2"; if (arr1[0] == "03") arr1[0] = "3"; if (arr1[0] == "04") arr1[0] = "4"; if (arr1[0] == "05") arr1[0] = "5";
                    //if (arr1[0] == "06") arr1[0] = "6"; if (arr1[0] == "07") arr1[0] = "7"; if (arr1[0] == "08") arr1[0] = "8"; if (arr1[0] == "09") arr1[0] = "9";

                    //string x1 = (arr1[0] != "" && arr1[1] != "") ? (arr1[0] + "." + arr1[1]) : "";

                    nhapvv.CanNang = txtCanNang.Text.Trim();
                    nhapvv.ChieuCao = txtChieuCao.Text.Trim();
                    nhapvv.ChanDoan = txtTenICD.Text;
                    if (xtraTabControl2.SelectedTabPageIndex == 1)
                        nhapvv.ChanDoan = txtTenICD1.Text;
                    if (_chuyenkhoa == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKTaiMuiHong)
                    {
                        nhapvv.Tai = txtTai.Text;
                        nhapvv.Mui = txtMui.Text;
                    }
                    if (_chuyenkhoa == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKMat)
                    {
                        nhapvv.MatPKK = txtKKmp.Text;
                        nhapvv.MatTKK = txtKKmt.Text;
                    }
                    nhapvv.Hong = txtHong.Text;
                    nhapvv.MatP = txtCKmp.Text;
                    nhapvv.MatT = txtCKmt.Text;
                    nhapvv.NhanApP = txtNAmp.Text;
                    nhapvv.NhanApT = ckDiUng1.Text;

                    //if(DungChung.Bien.MaBV == "01071")
                    //    nhapvv.SoBA = txtSoVVPre.Text + "-" + txtSoVV.Text;
                    //else
                    nhapvv.SoBA = txtSoBA.Text;
                    nhapvv.ChuY = txtChuY.Text;
                    nhapvv.NgayVao = dtNgayVao.DateTime;
                    if (DungChung.Bien.ngayGiaMoiTT39 > new DateTime(2000, 01, 01))
                    {
                        if (dtNgayVao.DateTime > DungChung.Bien.ngayGiaMoiTT39)
                        {
                            CapNhatSangGiaTT39(nhapvv.MaBNhan);
                        }
                        else
                            CapNhatSangGiaTT15(nhapvv.MaBNhan);
                    }

                    nhapvv.NhomMau = txtNhomMau.Text.Trim();
                    nhapvv.HeMau = nhommaurh.Text.Trim();
                    if (string.IsNullOrEmpty(txtSoVV.Text))
                        nhapvv.SoVV = null;
                    else if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "24297")
                        nhapvv.SoVV = txtSoVVPre.Text + "-" + txtSoVV.Text;
                    else
                        nhapvv.SoVV = txtSoVV.Text;


                    int kpdt = 0;//dung020616
                    if (xtraTabControl2.SelectedTabPageIndex == 0)
                    {
                        if (lupKhoaDT.EditValue != null && lupKhoaDT.Enabled)
                        {
                            bnkb.MaKPdt = Convert.ToInt32(lupKhoaDT.EditValue);
                            nhapvv.MaKP = Convert.ToInt32(lupKhoaDT.EditValue);
                            kpdt = Convert.ToInt32(lupKhoaDT.EditValue);
                        }
                    }
                    else
                    {
                        if (lupKhoaDT1.EditValue != null && lupKhoaDT1.Enabled)
                        {
                            bnkb.MaKPdt = Convert.ToInt32(lupKhoaDT1.EditValue);
                            nhapvv.MaKP = Convert.ToInt32(lupKhoaDT1.EditValue);
                            kpdt = Convert.ToInt32(lupKhoaDT1.EditValue);
                        }
                    }
                    bool ttluu = false;

                    try
                    {
                        DataContect.SaveChanges();
                        ttluu = true;
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        ttluu = false;
                        Exception raise = dbEx;

                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {

                            foreach (var validationError in validationErrors.ValidationErrors)
                            {

                                string message = string.Format("{0}:{1}",

                                  validationErrors.Entry.Entity.ToString(),

                                    validationError.ErrorMessage);

                                // raise a new exception nesting

                                // the current instance as InnerException

                                raise = new InvalidOperationException(message, raise);

                            }

                        }

                        throw raise;
                    }
                    //int a = DataContect.SaveChanges();
                    if (ttluu)
                    //if(ttluu)
                    {
                        var makp = _vaovien.First().MaKP;
                        var bnkbKT = DataContect.BNKBs.FirstOrDefault(o => o.MaBNhan == _mabn && o.IDKB == nhapvv.IDKB);
                        if (bnkbKT != null)
                        {
                            if (lupKhoaDT.EditValue != null)
                                DungChung.Ham._setMaKP_BenhNhan(DataContect, _int_maBN, nhapvv.MaKP ?? 0, radNoiNgoaiTru.SelectedIndex);

                        }
                        if (DungChung.Bien.MaBV == "24012")
                        {
                            if (lupKhoaDT.EditValue != null)
                            {
                                DungChung.Ham._setMaKP_BenhNhan_24012(DataContect, _int_maBN, Convert.ToInt32(lupKhoaDT.EditValue), radNoiNgoaiTru.SelectedIndex, _vvnt);
                            }
                        }
                        BNKB kb = DataContect.BNKBs.Where(p => p.IDKB == idkb).FirstOrDefault();
                        if (kb != null)
                            kb.NgayNghi = dtNgayVao.DateTime;
                        //if (lupKhoaDT.EditValue != null)
                        //    kb.MaKPdt = Convert.ToInt32(lupKhoaDT.EditValue); // đối với phương án=-1 MaKPdt=0 đức sửa 25-09
                        if (DataContect.SaveChanges() >= 0)
                        {
                            string cannang_chieucao = "", Mach_ND_HA = "";
                            string[] arr = { "", "" };
                            //if (txtCanNang.Text != "" && txtCanNang.Text != ".") arr = txtCanNang.Text.Split('.');
                            //if (arr[0] == "01") arr[0] = "1"; if (arr[0] == "02") arr[0] = "2"; if (arr[0] == "03") arr[0] = "3"; if (arr[0] == "04") arr[0] = "4"; if (arr[0] == "05") arr[0] = "5";
                            //if (arr[0] == "06") arr[0] = "6"; if (arr[0] == "07") arr[0] = "7"; if (arr[0] == "08") arr[0] = "8"; if (arr[0] == "09") arr[0] = "9";
                            //string x = (arr[0] != "" && arr[1] != "") ? (arr[0] + "." + arr[1]) : "";
                            cannang_chieucao = txtCanNang.Text + ";" + txtChieuCao.Text;
                            Mach_ND_HA = txtMach.Text + ";" + txtNhietDo.Text + ";" + txtHuyetAp.Text + ";" + txtNhipTho.Text;
                            var ttbx = DataContect.TTboXungs.Where(p => p.MaBNhan == _mabn).FirstOrDefault();

                            if (ttbx != null)
                            {
                                ttbx.CanNang_ChieuCao = cannang_chieucao;
                                ttbx.Mach_NDo_HAp = Mach_ND_HA;
                                DataContect.SaveChanges();
                            }
                            else
                            {
                                TTboXung ttbx_new = new TTboXung();
                                ttbx_new.MaBNhan = _mabn;
                                ttbx_new.CanNang_ChieuCao = cannang_chieucao;
                                ttbx_new.Mach_NDo_HAp = Mach_ND_HA;
                                DataContect.TTboXungs.Add(ttbx_new);
                                DataContect.SaveChanges();
                            }
                        }
                    }

                    // Lưu tiếp. 
                    //"bệnh lý","tiền sử bản thân","tiền sử gia đình","toàn thân","các bộ phận","kết quả cls","đã xử lý"
                    if (xtraTabControl2.SelectedTabPageIndex == 0)
                    {
                        luuTiepVV(nhapvv, DataContect, "bệnh lý", txtBenhLy, "1.Quá trình bệnh lý: ");
                        luuTiepVV(nhapvv, DataContect, "tiền sử bản thân", txtTienSuBT, "2.Tiền sử bản thân: ");
                        luuTiepVV(nhapvv, DataContect, "tiền sử gia đình", txtTienSuGD, "2.Tiền sử gia đình: ");
                        luuTiepVV(nhapvv, DataContect, "toàn thân", txtKhamTThan, "1.Toàn thân: ");
                        luuTiepVV(nhapvv, DataContect, "các bộ phận", txtKhamBPhan, "2.Các bộ phận: ");
                        luuTiepVV(nhapvv, DataContect, "kết quả cls", txtkQLamSang, "3.Tóm tắt kết quả CLS: ");
                        luuTiepVV(nhapvv, DataContect, "đã xử lý", txtDaXuLy, "5.Đã XL(Thuốc-chăm sóc): ");
                    }
                    else
                    {
                        luuTiepVV(nhapvv, DataContect, "bệnh lý", txtBenhLy1, "1.Quá trình bệnh lý: ");
                        luuTiepVV(nhapvv, DataContect, "tiền sử bản thân", txtTienSuBT1, "2.Tiền sử bản thân: ");
                        luuTiepVV(nhapvv, DataContect, "tiền sử gia đình", txtTienSuGD1, "2.Tiền sử gia đình: ");
                        luuTiepVV(nhapvv, DataContect, "toàn thân", txtKhamTThan1, "1.Toàn thân: ");
                        luuTiepVV(nhapvv, DataContect, "các bộ phận", txtKhamBPhan1, "2.Các bộ phận: ");
                        luuTiepVV(nhapvv, DataContect, "kết quả cls", txtkQLamSang1, "3.Tóm tắt kết quả CLS: ");
                        luuTiepVV(nhapvv, DataContect, "đã xử lý", txtDaXuLy1, "5.Đã XL(Thuốc-chăm sóc): ");
                        #region tiền sử dị ứng
                        if (DungChung.Bien.MaBV == "30007")
                        {
                            if (!String.IsNullOrEmpty(txtDiUng11.Text) || !String.IsNullOrEmpty(txtDiUng21.Text) || !String.IsNullOrEmpty(txtDiUng31.Text)
                                || !String.IsNullOrEmpty(txtDiUng41.Text) || !String.IsNullOrEmpty(txtDiUng51.Text) || !String.IsNullOrEmpty(txtDiUng61.Text))
                            {
                                var qDiUng = DataContect.TienSuDiUngs.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                                if (qDiUng != null)
                                {
                                    TienSuDiUng old = new TienSuDiUng();
                                    old.Thuoc = qDiUng.Thuoc;
                                    old.ConTrung = qDiUng.ConTrung;
                                    old.ThucPham = qDiUng.ThucPham;
                                    old.Khac = qDiUng.Khac;
                                    old.TienSuBanThan = qDiUng.TienSuBanThan;
                                    old.TienSuGiaDinh = qDiUng.TienSuGiaDinh;

                                    old.Thuoc_SL = qDiUng.Thuoc_SL;
                                    old.ConTrung_SL = qDiUng.ConTrung_SL;
                                    old.ThucPham_SL = qDiUng.ThucPham_SL;
                                    old.Khac_SL = qDiUng.Khac_SL;
                                    old.TienSuBanThan_SL = qDiUng.TienSuBanThan_SL;
                                    old.TienSuGiaDinh_SL = qDiUng.TienSuGiaDinh_SL;

                                    old.Thuoc_MoTa = qDiUng.Thuoc_MoTa;
                                    old.ConTrung_MoTa = qDiUng.ConTrung_MoTa;
                                    old.ThucPham_MoTa = qDiUng.ThucPham_MoTa;
                                    old.Khac_MoTa = qDiUng.Khac_MoTa;
                                    old.TienSuBanThan_MoTa = qDiUng.TienSuBanThan_MoTa;
                                    old.TienSuGiaDinh_MoTa = qDiUng.TienSuGiaDinh_MoTa;

                                    #region tiền sử dị ứng
                                    qDiUng.Thuoc = txtDiUng11.Text;
                                    qDiUng.ConTrung = txtDiUng21.Text;
                                    qDiUng.ThucPham = txtDiUng31.Text;
                                    qDiUng.Khac = txtDiUng41.Text;
                                    qDiUng.TienSuBanThan = txtDiUng51.Text;
                                    qDiUng.TienSuGiaDinh = txtDiUng61.Text;
                                    if (txtDiUng12.Text != null && txtDiUng12.Text != "")
                                        qDiUng.Thuoc_SL = Convert.ToInt16(txtDiUng12.Text);
                                    else
                                        qDiUng.Thuoc_SL = 0;
                                    qDiUng.ConTrung_SL = Convert.ToInt16(txtDiUng22.Text);
                                    qDiUng.ThucPham_SL = Convert.ToInt16(txtDiUng32.Text);
                                    qDiUng.Khac_SL = Convert.ToInt16(txtDiUng42.Text);
                                    qDiUng.TienSuBanThan_SL = Convert.ToInt16(txtDiUng52.Text);
                                    qDiUng.TienSuGiaDinh_SL = Convert.ToInt16(txtDiUng62.Text);

                                    qDiUng.Thuoc_MoTa = txtDiUng13.Text;
                                    qDiUng.ConTrung_MoTa = txtDiUng23.Text;
                                    qDiUng.ThucPham_MoTa = txtDiUng33.Text;
                                    qDiUng.Khac_MoTa = txtDiUng43.Text;
                                    qDiUng.TienSuBanThan_MoTa = txtDiUng53.Text;
                                    qDiUng.TienSuGiaDinh_MoTa = txtDiUng63.Text;

                                    if (old.Thuoc != qDiUng.Thuoc ||
                                    old.ConTrung != qDiUng.ConTrung ||
                                    old.ThucPham != qDiUng.ThucPham ||
                                    old.Khac != qDiUng.Khac ||
                                    old.TienSuBanThan != qDiUng.TienSuBanThan ||
                                    old.TienSuGiaDinh != qDiUng.TienSuGiaDinh ||
                                    old.Thuoc_SL != qDiUng.Thuoc_SL ||
                                    old.ConTrung_SL != qDiUng.ConTrung_SL ||
                                    old.ThucPham_SL != qDiUng.ThucPham_SL ||
                                    old.Khac_SL != qDiUng.Khac_SL ||
                                    old.TienSuBanThan_SL != qDiUng.TienSuBanThan_SL ||
                                    old.TienSuGiaDinh_SL != qDiUng.TienSuGiaDinh_SL ||
                                    old.Thuoc_MoTa != qDiUng.Thuoc_MoTa ||
                                    old.ConTrung_MoTa != qDiUng.ConTrung_MoTa ||
                                    old.ThucPham_MoTa != qDiUng.ThucPham_MoTa ||
                                    old.Khac_MoTa != qDiUng.Khac_MoTa ||
                                    old.TienSuBanThan_MoTa != qDiUng.TienSuBanThan_MoTa ||
                                    old.TienSuGiaDinh_MoTa != qDiUng.TienSuGiaDinh_MoTa)
                                    {
                                        qDiUng.MaCB = DungChung.Bien.MaCB;
                                        qDiUng.MaKP = DungChung.Bien.MaKP;
                                        qDiUng.NgayTao = DateTime.Now;
                                    }
                                    DataContect.SaveChanges();
                                    #endregion

                                }
                                else
                                {
                                    #region tiền sử dị ứng
                                    TienSuDiUng ts = new TienSuDiUng();
                                    ts.MaBNhan = _mabn;
                                    ts.Thuoc = txtDiUng11.Text;
                                    ts.ConTrung = txtDiUng21.Text;
                                    ts.ThucPham = txtDiUng31.Text;
                                    ts.Khac = txtDiUng41.Text;
                                    ts.TienSuBanThan = txtDiUng51.Text;
                                    ts.TienSuGiaDinh = txtDiUng61.Text;
                                    if (txtDiUng12.Text != null && txtDiUng12.Text != "")
                                        ts.Thuoc_SL = Convert.ToInt16(txtDiUng12.Text);
                                    else
                                        ts.Thuoc_SL = 0;
                                    ts.ConTrung_SL = Convert.ToInt16(txtDiUng22.Text);
                                    ts.ThucPham_SL = Convert.ToInt16(txtDiUng32.Text);
                                    ts.Khac_SL = Convert.ToInt16(txtDiUng42.Text);
                                    ts.TienSuBanThan_SL = Convert.ToInt16(txtDiUng52.Text);
                                    ts.TienSuGiaDinh_SL = Convert.ToInt16(txtDiUng62.Text);

                                    ts.Thuoc_MoTa = txtDiUng13.Text;
                                    ts.ConTrung_MoTa = txtDiUng23.Text;
                                    ts.ThucPham_MoTa = txtDiUng33.Text;
                                    ts.Khac_MoTa = txtDiUng43.Text;
                                    ts.TienSuBanThan_MoTa = txtDiUng53.Text;
                                    ts.TienSuGiaDinh_MoTa = txtDiUng63.Text;
                                    ts.MaCB = DungChung.Bien.MaCB;
                                    ts.MaKP = DungChung.Bien.MaKP;
                                    ts.NgayTao = DateTime.Now;
                                    DataContect.TienSuDiUngs.Add(ts);
                                    DataContect.SaveChanges();
                                    #endregion
                                }
                            }
                        }
                        #endregion
                    }
                }
                else //tạo mới
                {
                    _luutd = 1;
                    if (DungChung.Bien.MaBV == "30007")
                    {
                        if (!String.IsNullOrEmpty(txtDiUng11.Text) || !String.IsNullOrEmpty(txtDiUng21.Text) || !String.IsNullOrEmpty(txtDiUng31.Text)
                               || !String.IsNullOrEmpty(txtDiUng41.Text) || !String.IsNullOrEmpty(txtDiUng51.Text) || !String.IsNullOrEmpty(txtDiUng61.Text))
                        {
                            #region tiền sử dị ứng
                            TienSuDiUng ts = new TienSuDiUng();
                            ts.MaBNhan = _mabn;
                            ts.Thuoc = txtDiUng11.Text;
                            ts.ConTrung = txtDiUng21.Text;
                            ts.ThucPham = txtDiUng31.Text;
                            ts.Khac = txtDiUng41.Text;
                            ts.TienSuBanThan = txtDiUng51.Text;
                            ts.TienSuGiaDinh = txtDiUng61.Text;
                            if (txtDiUng12.Text != null && txtDiUng12.Text != "")
                                ts.Thuoc_SL = Convert.ToInt16(txtDiUng12.Text);
                            else
                                ts.Thuoc_SL = 0;
                            ts.ConTrung_SL = Convert.ToInt16(txtDiUng22.Text);
                            ts.ThucPham_SL = Convert.ToInt16(txtDiUng32.Text);
                            ts.Khac_SL = Convert.ToInt16(txtDiUng42.Text);
                            ts.TienSuBanThan_SL = Convert.ToInt16(txtDiUng52.Text);
                            ts.TienSuGiaDinh_SL = Convert.ToInt16(txtDiUng62.Text);

                            ts.Thuoc_MoTa = txtDiUng13.Text;
                            ts.ConTrung_MoTa = txtDiUng23.Text;
                            ts.ThucPham_MoTa = txtDiUng33.Text;
                            ts.Khac_MoTa = txtDiUng43.Text;
                            ts.TienSuBanThan_MoTa = txtDiUng53.Text;
                            ts.TienSuGiaDinh_MoTa = txtDiUng63.Text;
                            ts.MaCB = DungChung.Bien.MaCB;
                            ts.MaKP = DungChung.Bien.MaKP;
                            ts.NgayTao = DateTime.Now;
                            DataContect.TienSuDiUngs.Add(ts);
                            DataContect.SaveChanges();
                            #endregion
                        }
                    }
                    setDBsoVV();
                    setDBSoBA();
                    if (DungChung.Bien.MaBV == "01071")
                        txtSoBA.Text = txtSoVV.Text;
                    VaoVien nhapvv = new VaoVien();
                    nhapvv.ChuyenKhoa = _chuyenkhoa;
                    nhapvv.MaCK = DungChung.Bien._lChuyenKhoa.Where(p => p.ChuyenKhoa == _chuyenkhoa).Select(p => p.MaCK).FirstOrDefault();
                    nhapvv.MaBNhan = _int_maBN;
                    nhapvv.IDKB = idkb;
                    nhapvv.LyDo = txtLyDo.Text;
                    if (!string.IsNullOrEmpty(txtMach.Text))
                        nhapvv.Mach = txtMach.Text;
                    if (!string.IsNullOrEmpty(txtNhietDo.Text))
                        nhapvv.NhietDo = txtNhietDo.Text;
                    nhapvv.HuyetAp = txtHuyetAp.Text;
                    if (!string.IsNullOrEmpty(txtNhipTho.Text))
                        nhapvv.NhipTho = txtNhipTho.Text;
                    if (!string.IsNullOrEmpty(txtCanNang.Text))
                    {
                        nhapvv.CanNang = txtCanNang.Text.Trim();
                    }
                    nhapvv.ChieuCao = txtChieuCao.Text.Trim();
                    #region kieemr tra vaf update so vao vien, so BA



                    #endregion
                    if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "24297")
                    {
                        nhapvv.SoVV = txtSoVVPre.Text + txtSoVV.Text;
                    }
                    else
                        nhapvv.SoVV = txtSoVV.Text;
                    nhapvv.SoBA = txtSoBA.Text;
                    nhapvv.ChanDoan = txtTenICD.Text;
                    if (xtraTabControl2.SelectedTabPageIndex == 1)
                        nhapvv.ChanDoan = txtTenICD1.Text;
                    if (_chuyenkhoa == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKTaiMuiHong)
                    {
                        nhapvv.Tai = txtTai.Text;
                        nhapvv.Mui = txtMui.Text;
                    }
                    if (_chuyenkhoa == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKMat)
                    {
                        nhapvv.Tai = txtKKmp.Text;
                        nhapvv.Mui = txtKKmt.Text;
                    }
                    nhapvv.Hong = txtHong.Text;
                    nhapvv.MatP = txtCKmp.Text;
                    nhapvv.MatT = txtCKmt.Text;
                    nhapvv.NhanApP = txtNAmp.Text;
                    nhapvv.NhanApT = ckDiUng1.Text;
                    nhapvv.ChuY = txtChuY.Text;
                    nhapvv.NgayVao = dtNgayVao.DateTime;
                    nhapvv.NhomMau = txtNhomMau.Text.Trim();
                    if (xtraTabControl2.SelectedTabPageIndex == 0)
                    {
                        if (lupKhoaDT.EditValue != null)
                            nhapvv.MaKP = Convert.ToInt32(lupKhoaDT.EditValue);
                        else
                            nhapvv.MaKP = 0;
                    }
                    else
                    {
                        if (lupKhoaDT1.EditValue != null)
                            nhapvv.MaKP = Convert.ToInt32(lupKhoaDT1.EditValue);
                        else
                            nhapvv.MaKP = 0;
                    }
                    DataContect.VaoViens.Add(nhapvv);
                    if (DataContect.SaveChanges() >= 0)
                    {
                        DungChung.Ham._setMaKP_BenhNhan(DataContect, _int_maBN, nhapvv.MaKP ?? 0, radNoiNgoaiTru.SelectedIndex);
                        foreach (var item in kp)
                        {
                            DungChung.Ham.Update_Delete_CongKham(_int_maBN, item.IDKB, false, dtNgayVao.DateTime);
                        }
                        int idmin = -1;
                        if (kp.Count > 0)
                            idmin = kp.Last().IDKB;
                        if (idmin > 0)
                            if (dtNgayVao.DateTime > DungChung.Ham.NgayDen(Convert.ToDateTime("29/02/2016")))
                                DungChung.Ham.Update_Delete_CongKham(_int_maBN, idmin, true, dtNgayVao.DateTime);
                        string cannang_chieucao = "", Mach_ND_HA = "";
                        string[] arr = { "", "" };
                        if (txtCanNang.Text != "" && txtCanNang.Text != ".") arr = txtCanNang.Text.Split('.');
                        if (arr[0] == "01") arr[0] = "1"; if (arr[0] == "02") arr[0] = "2"; if (arr[0] == "03") arr[0] = "3"; if (arr[0] == "04") arr[0] = "4"; if (arr[0] == "05") arr[0] = "5";
                        if (arr[0] == "06") arr[0] = "6"; if (arr[0] == "07") arr[0] = "7"; if (arr[0] == "08") arr[0] = "8"; if (arr[0] == "09") arr[0] = "9";
                        string x = "";
                        if (arr.Count() > 1)
                            x = (arr[0] != "" && arr[1] != "") ? (arr[0] + "." + arr[1]) : "";
                        else x = arr[0];
                        cannang_chieucao = x + ";" + txtChieuCao.Text;
                        Mach_ND_HA = txtMach.Text + ";" + txtNhietDo.Text + ";" + txtHuyetAp.Text + ";" + txtNhipTho.Text;
                        var ttbx = DataContect.TTboXungs.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                        if (ttbx != null)
                        {
                            ttbx.CanNang_ChieuCao = cannang_chieucao;
                            ttbx.Mach_NDo_HAp = Mach_ND_HA;
                            DataContect.SaveChanges();
                        }
                        else
                        {
                            TTboXung ttbx_new = new TTboXung();
                            ttbx_new.MaBNhan = _mabn;
                            ttbx_new.CanNang_ChieuCao = cannang_chieucao;
                            ttbx_new.Mach_NDo_HAp = Mach_ND_HA;
                            DataContect.TTboXungs.Add(ttbx_new);
                            DataContect.SaveChanges();
                        }

                    }
                    int kpdt = 0;
                    // dung020616
                    try
                    {

                        if (xtraTabControl2.SelectedTabPageIndex == 0 && lupKhoaDT.EditValue != null)
                        {

                            kpdt = Convert.ToInt32(lupKhoaDT.EditValue);

                        }
                        else if (xtraTabControl2.SelectedTabPageIndex == 1 && lupKhoaDT1.EditValue != null)
                        {
                            kpdt = Convert.ToInt32(lupKhoaDT1.EditValue);
                        }

                        if (idkb > 0)
                        {
                            BNKB kb = DataContect.BNKBs.Single(p => p.IDKB == idkb);
                            if (xtraTabControl2.SelectedTabPageIndex == 0)
                            {
                                if (lupKhoaDT.EditValue != null)
                                {
                                    kb.MaKPdt = Convert.ToInt32(lupKhoaDT.EditValue);
                                    if ((DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389") && _vvNgoaiTru)
                                        kb.PhuongAn = 4;
                                    else
                                        kb.PhuongAn = 1;
                                    //kb.PhuongAn = 1;
                                    kb.NgayNghi = dtNgayVao.DateTime;
                                    DataContect.SaveChanges();
                                }
                            }
                            else
                            {
                                if (lupKhoaDT1.EditValue != null)
                                {
                                    kb.MaKPdt = Convert.ToInt32(lupKhoaDT1.EditValue);
                                    if ((DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389") && _vvNgoaiTru)
                                        kb.PhuongAn = 4;
                                    else
                                        kb.PhuongAn = 1;
                                    kb.NgayNghi = dtNgayVao.DateTime;
                                    DataContect.SaveChanges();
                                }
                            }
                        }
                    }
                    catch
                    {

                    }
                    DungChung.Ham._setMaKP_BenhNhan(DataContect, _int_maBN, kpdt, radNoiNgoaiTru.SelectedIndex);
                    //-------------------------------------------------------------------------
                    // Lưu tiếp. 
                    VaoVien vv = DataContect.VaoViens.Where(p => p.MaBNhan == _int_maBN).First();
                    //"bệnh lý","tiền sử bản thân","tiền sử gia đình","toàn thân","các bộ phận","kết quả cls","đã xử lý"
                    if (xtraTabControl2.SelectedTabPageIndex == 0)
                    {
                        luuTiepVV(vv, DataContect, "bệnh lý", txtBenhLy, "1.Quá trình bệnh lý: ");
                        luuTiepVV(vv, DataContect, "tiền sử bản thân", txtTienSuBT, "2.Tiền sử bản thân: ");
                        luuTiepVV(vv, DataContect, "tiền sử gia đình", txtTienSuGD, "2.Tiền sử gia đình: ");
                        luuTiepVV(vv, DataContect, "toàn thân", txtKhamTThan, "1.Toàn thân: ");
                        luuTiepVV(vv, DataContect, "các bộ phận", txtKhamBPhan, "2.Các bộ phận: ");
                        luuTiepVV(vv, DataContect, "kết quả cls", txtkQLamSang, "3.Tóm tắt kết quả CLS: ");
                        luuTiepVV(vv, DataContect, "đã xử lý", txtDaXuLy, "5.Đã XL(Thuốc-chăm sóc): ");
                    }
                    else
                    {
                        luuTiepVV(vv, DataContect, "bệnh lý", txtBenhLy1, "1.Quá trình bệnh lý: ");
                        luuTiepVV(vv, DataContect, "tiền sử bản thân", txtTienSuBT1, "2.Tiền sử bản thân: ");
                        luuTiepVV(vv, DataContect, "tiền sử gia đình", txtTienSuGD1, "2.Tiền sử gia đình: ");
                        luuTiepVV(vv, DataContect, "toàn thân", txtKhamTThan1, "1.Toàn thân: ");
                        luuTiepVV(vv, DataContect, "các bộ phận", txtKhamBPhan1, "2.Các bộ phận: ");
                        luuTiepVV(vv, DataContect, "kết quả cls", txtkQLamSang1, "3.Tóm tắt kết quả CLS: ");
                        luuTiepVV(vv, DataContect, "đã xử lý", txtDaXuLy1, "5.Đã XL(Thuốc-chăm sóc): ");
                    }
                }
                btnInPhieu.Enabled = true;
                btnLuu.Enabled = false;
                if (ttdong == 0)
                    btnInPhieu_Click(sender, e);
                #region cập nhật giá 39 - his 2110- dung 14.12.2018

                CapNhatSangGiaTT39(_int_maBN);


                #endregion
            }
            //Enablebutton(true);
            //EnableControl(false);
            //usNhapDuoc_Load(sender, e);

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Lỗi lưu khám bệnh vào viện: " + ex.Message);
            //}
            frmKBVaoVien_Load(sender, e);
        }

        private void CapNhatSangGiaTT39(int _int_maBN)
        {
            var benhnhan = DataContect.BenhNhans.Single(p => p.MaBNhan == _int_maBN);
            var qdichvu = DataContect.DichVus.Where(p => p.PLoai == 2).ToList();
            if (benhnhan.NNhap != null && benhnhan.DTuong == "BHYT" && DungChung.Bien.ngayGiaMoiTT39 > new DateTime(2000, 01, 01) && dtNgayVao.DateTime >= DungChung.Bien.ngayGiaMoiTT39)
            {
                string ms = "";
                List<DSDichVuGiaMoi> ldsDv = new List<DSDichVuGiaMoi>();
                var qcls = (from cls in DataContect.CLS.Where(p => p.MaBNhan == _int_maBN)
                            join cd in DataContect.ChiDinhs.Where(p => p.TrongBH != null && p.TrongBH == 1) on cls.IdCLS equals cd.IdCLS
                            select new { cls.IdCLS, cls.NgayThang, cd.MaDV, cd.DonGia, cd.IDCD }).ToList();

                foreach (var a in qcls)
                {
                    var qdv = qdichvu.Where(p => p.MaDV == a.MaDV).FirstOrDefault();
                    if (qdv != null)
                    {
                        if (a.DonGia != qdv.DonGiaTT39)
                        {
                            DSDichVuGiaMoi moi = new DSDichVuGiaMoi();
                            moi.MaDV = a.MaDV ?? 0;
                            moi.TenDV = qdv.TenDV;
                            moi.IDCD = a.IDCD;
                            moi.GiaCu = qdv.DonGiaTT15;
                            moi.GiaMoi = qdv.DonGiaTT39;
                            ldsDv.Add(moi);
                            ChiDinh chidinhUp = DataContect.ChiDinhs.Single(p => p.IDCD == a.IDCD);
                            chidinhUp.DonGia = qdv.DonGiaTT39;

                            DataContect.SaveChanges();
                            ms = ms + qdv.MaDV + "-" + qdv.TenDV + " , ";
                        }
                    }
                }


                List<DSDichVuGiaMoi> ldsDv2 = new List<DSDichVuGiaMoi>();
                var qdthuoc = (from dt in DataContect.DThuocs.Where(p => p.MaBNhan == _int_maBN)
                               join dtct in DataContect.DThuoccts.Where(p => p.TrongBH != null && p.TrongBH == 1) on dt.IDDon equals dtct.IDDon
                               join dichvu in DataContect.DichVus.Where(p => p.PLoai == 2) on dtct.MaDV equals dichvu.MaDV
                               select new { dt.NgayKe, dt.IDDon, dtct.IDDonct, dtct.MaDV, dtct.DonGia, dtct.IDCD, dtct.SoLuong, dtct.TyLeTT }).ToList();

                foreach (var a in qdthuoc)
                {
                    var qdv = qdichvu.Where(p => p.MaDV == a.MaDV).FirstOrDefault();
                    if (qdv != null)
                    {
                        if (a.DonGia != qdv.DonGiaTT39)
                        {
                            DSDichVuGiaMoi moi = new DSDichVuGiaMoi();
                            moi.MaDV = a.MaDV ?? 0;
                            moi.IDCD = a.IDDonct;
                            moi.GiaCu = qdv.DonGiaTT15;
                            moi.GiaMoi = qdv.DonGiaTT39;
                            moi.TenDV = qdv.TenDV;
                            ldsDv2.Add(moi);

                            DThuocct dtctUp = DataContect.DThuoccts.Single(p => p.IDDonct == a.IDDonct);
                            dtctUp.DonGia = qdv.DonGiaTT39;
                            dtctUp.ThanhTien = Math.Round(qdv.DonGiaTT39 * a.SoLuong * a.TyLeTT / 100, 5);
                            DataContect.SaveChanges();
                            if (a.IDCD == null)
                            {
                                ms = ms + qdv.MaDV + "-" + qdv.TenDV + " , ";
                            }
                            else
                            {
                                if (ldsDv.Count == 0 || ldsDv.Where(p => p.IDCD == a.IDCD).Count() == 0)
                                {
                                    ms = ms + qdv.MaDV + "-" + qdv.TenDV + " , ";
                                }
                            }


                        }
                    }
                }
                if (ms != "")
                    MessageBox.Show("Các dịch vụ: " + ms + " đã được cập nhật về giá mới");
            }
        }
        bool luutiep = true;
        private class DSDichVuGiaMoi
        {
            public int MaDV { set; get; }
            public int IDCD { set; get; }
            public int IDDonct { set; get; }
            public double GiaCu { set; get; }
            public double GiaMoi { set; get; }

            public string TenDV { get; set; }
        }
        private void luuTiepVV(VaoVien vv, QLBV_Database.QLBVEntities _data, string column, MemoEdit mmedit, string TenHienThi)
        {
            if (luutiep)
            {
                try
                {
                    switch (column)
                    {
                        case "bệnh lý":
                            vv.BenhLy = mmedit.Text;
                            break;
                        case "tiền sử bản thân":
                            vv.TienSuBT = mmedit.Text;
                            break;
                        case "tiền sử gia đình":
                            vv.TienSuGD = mmedit.Text;
                            break;
                        case "toàn thân":
                            vv.KhamTThan = mmedit.Text;
                            break;
                        case "các bộ phận":
                            vv.KhamBPhan = mmedit.Text;
                            break;
                        case "kết quả cls":
                            vv.kQLamSang = mmedit.Text;
                            break;
                        case "đã xử lý":
                            vv.DaXuLy = mmedit.Text;
                            break;
                        default:
                            break;
                    }
                    _data.SaveChanges();
                }
                catch (System.Data.UpdateException ex)
                {
                    MessageBox.Show(TenHienThi + ex.InnerException.Message.ToString());
                    mmedit.Focus();
                    mmedit.SelectAll();
                    luutiep = false;
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        class PKBVV
        {
            public string CQ { get; set; }
            public string CQCQ { get; set; }
            public string ChieuCao { get; set; }
            public string TenBNhan { get; set; }
            public string NgayVao { get; set; }
            public int? GTinh { get; set; }
            public string Tuoi { get; set; }
            public string DanToc { get; set; }
            public string TenKP { get; set; }
            public string DChi { get; set; }
            public string NgaySinh { get; set; }
            public string NgayThang { get; set; }
            public string CDNoiGT { get; set; }
            public string HanBHDen { get; set; }


            public string ST1 { get; set; }
            public string ST2 { get; set; }
            public string ST3 { get; set; }
            public string ST4 { get; set; }
            public string ST5 { get; set; }
            public string ST6 { get; set; }
            public string MCS { get; set; }
            public string Nam { get; set; }
            public string Nu { get; set; }

            public string BHYT { get; set; }
            public string DichVu { get; set; }
            public string MaNN { get; set; }
            public string NgoaiKieu { get; set; }
            public int? DTuong { get; set; }
            public string NThan { get; set; }
            public string DThoaiNT { get; set; }

            public string BenhLy { get; set; }
            public string TienSuBT { get; set; }
            public string TienSuGD { get; set; }
            public string KhamTThan { get; set; }
            public string Mach { get; set; }
            public string NhietDo { get; set; }
            public string HuyetAp { get; set; }
            public string NhipTho { get; set; }
            public string CanNang { get; set; }
            public string KhamBPhan { get; set; }
            public string kQLamSang { get; set; }
            public string ChanDoan { get; set; }
            public string SoVV { get; set; }
            public string ChuY { get; set; }
            public string DaXuLy { get; set; }
            public string NoiLV { get; set; }
            public string MaCS { get; set; }
            public string LyDo { get; set; }
            public string KPDT { get; set; }
            public string TenCB { get; set; }
        }
        private void btnInPhieu_Click(object sender, EventArgs e)
        {
 
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            frmIn frm = new frmIn();
            if (_chuyenkhoa == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKTaiMuiHong && inchuyenkhoa)
            {
                #region Tai mũi họng
                BaoCao.repPhieuKBVV_THa_TMH rep = new BaoCao.repPhieuKBVV_THa_TMH();
                int id = 0;
                if (_mabn > 0)
                {
                    var par = (from bn in data.BenhNhans.Where(p => p.MaBNhan == _mabn)
                               join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                               join ttbx in data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                               select new
                               {
                                   bn.MaBNhan,
                                   bn.TenBNhan,
                                   bn.GTinh,
                                   bn.Tuoi,
                                   bn.DChi,
                                   bn.NamSinh,
                                   bn.NgaySinh,
                                   bn.ThangSinh,
                                   bn.CDNoiGT,
                                   bn.HanBHDen,
                                   bn.SThe,
                                   bn.DTuong,
                                   bn.MaCS,
                                   bn.CapCuu,
                                   ttbx.MaNN,
                                   ttbx.NgoaiKieu,
                                   ttbx.NThan,
                                   ttbx.DThoaiNT,
                                   ttbx.MaDT,
                                   ttbx.NoiLV,
                                   ttbx.MaHuyen,
                                   ttbx.MaTinh,
                                   ttbx.MaXa,
                                   ttbx.ThonPho,
                                   //   dmnn.MaNN,dmdt.TenDT,dmt.TenTinh,
                                   vv.NgayVao,
                                   vv.BenhLy,
                                   vv.TienSuBT,
                                   vv.TienSuGD,
                                   vv.KhamTThan,
                                   vv.Tai,
                                   vv.Mui,
                                   vv.Hong,
                                   vv.kQLamSang,
                                   vv.ChanDoan,
                                   vv.SoVV,
                                   vv.ChuY,
                                   vv.DaXuLy,
                                   vv.LyDo
                               }).ToList();

                    var kpk = (from kbvv in data.BNKBs.Where(p => p.MaBNhan == _mabn).OrderByDescending(p => p.IDKB)
                               join kp in data.KPhongs on kbvv.MaKP equals kp.MaKP
                               join cb in data.CanBoes on kbvv.MaCB equals cb.MaCB
                               select new { kp.TenKP, kbvv.IDKB, TenCB = cb.CapBac + ": " + cb.TenCB, kbvv.NgayKham }).ToList().OrderByDescending(p => p.IDKB).ToList();
                    if (kpk.Count > 0)
                    {
                        rep.TenCB.Value = kpk.First().TenCB == null ? "" : kpk.First().TenCB.ToString();
                        rep.TenKP.Value = "PHÒNG KHÁM BỆNH: " + kpk.First().TenKP.ToString().ToUpper();
                        int i = int.Parse(kpk.First().IDKB.ToString());
                        var kpdt = (from kbvv in data.BNKBs.Where(p => p.IDKB == i)
                                    join kp in data.KPhongs on kbvv.MaKPdt equals kp.MaKP
                                    select new { kp.TenKP }).ToList();

                        if (kpdt.Count > 0)
                        {
                            rep.TenKPdt.Value = kpdt.First().TenKP;
                        }
                    }
                    var dtoc = (from dt in data.DanTocs join ttbx in data.TTboXungs.Where(p => p.MaBNhan == _mabn) on dt.MaDT equals ttbx.MaDT select dt.TenDT).ToList();
                    if (dtoc.Count > 0)
                        rep.TenDT.Value = dtoc.First();
                    var dnn = (from nn in data.DmNNs join ttbx in data.TTboXungs.Where(p => p.MaBNhan == _mabn) on nn.MaNN equals ttbx.MaNN select nn.TenNN).ToList();
                    if (dnn.Count > 0)
                        rep.NgheNghiep.Value = dnn.First();
                    //var dxa = (from xa in data.DmXas join ttbx in data.TTboXungs.Where(p => p.MaBNhan == _mabn) on xa.MaXa equals ttbx.MaXa select xa.TenXa).ToList();
                    //if (dxa.Count > 0)
                    //    rep.XaPhuong.Value = dxa.First();
                    //var dh = (from hu in data.DmHuyens join ttbx in data.TTboXungs.Where(p => p.MaBNhan == _mabn) on hu.MaHuyen equals ttbx.MaHuyen select hu.TenHuyen).ToList();
                    //if (dh.Count > 0)
                    //    rep.Huyen.Value = dh.First();
                    //var dti = (from ti in data.DmTinhs join ttbx in data.TTboXungs.Where(p => p.MaBNhan == _mabn) on ti.MaTinh equals ttbx.MaTinh select ti.TenTinh).ToList();
                    //if (dti.Count > 0)
                    //    rep.Tinh.Value = dti.First();
                    if (par.Count > 0)
                    {
                        rep.SoVV.Value = par.First().SoVV;
                        rep.CapCuu.Value = par.First().CapCuu;
                        rep.TenBNhan.Value = par.First().TenBNhan.ToUpper();
                        rep.Nam.Value = par.First().GTinh;
                        rep.NgaySinh.Value = par.First().NgaySinh;
                        rep.ThangSinh.Value = par.First().ThangSinh;
                        rep.NSinh.Value = par.First().NamSinh;
                        rep.Tuoi.Value = DungChung.Bien.MaBV == "24012" ? DungChung.Ham.TuoitheoThang(data, _mabn, DungChung.Bien.formatAge_24012) : DungChung.Ham.TuoitheoThang(data, _mabn, DungChung.Bien.formatAge);                        
                        //rep.NgheNghiep.Value = par.First().NgheNghiep;
                        rep.NgoaiKieu.Value = par.First().NgoaiKieu;
                        rep.MaDT.Value = par.First().MaDT;
                        rep.MaNN.Value = par.First().MaNN;
                        rep.MaHuyen.Value = par.First().MaHuyen;
                        rep.MaTinh.Value = par.First().MaTinh;
                        rep.DChi.Value = par.First().DChi;
                        rep.NoiLV.Value = par.First().NoiLV;
                        //rep.XaPhuong.Value = par.First().TenXa;
                        rep.DTuong.Value = par.First().DTuong;
                        if (par.First().DTuong == "BHYT")
                        {
                            rep.HanBHDen.Value = par.First().HanBHDen.ToString().Substring(0, 10);
                        }
                        rep.NguoiNha.Value = par.First().NThan;
                        rep.DienThoai.Value = par.First().DThoaiNT;
                        rep.CDNoiGT.Value = par.First().CDNoiGT;
                        if (DungChung.Bien.MaBV == "30009")
                            rep.NgayKham.Value = DungChung.Ham.NgaySangChu(kpk.Last().NgayKham.Value, DungChung.Bien.FormatDate);
                        else
                            rep.NgayKham.Value = DungChung.Ham.NgaySangChu(par.First().NgayVao.Value, 2);
                        if (DungChung.Bien.MaBV == "01830")
                        {
                            rep.MaBNhan.Value = "Mã BN: " + par.First().MaBNhan;
                        }
                        //if (par.First().NgayVao != null) {
                        //    rep.NgayKham.Value = par.First().NgayVao.Value.Hour+":"+par.First().NgayVao.Value.Minute+"  ";
                        //}
                        rep.BenhLy.Value = par.First().BenhLy;
                        rep.BanThan.Value = par.First().TienSuBT;
                        rep.GiaDinh.Value = par.First().TienSuGD;
                        rep.ToanThan.Value = par.First().KhamTThan;
                        rep.Tai.Value = par.First().Tai;
                        rep.Mui.Value = par.First().Mui;
                        rep.Hong.Value = par.First().Hong;
                        rep.CDoan.Value = DungChung.Ham.GetICDstr(par.First().ChanDoan);
                        rep.ChuY.Value = par.First().ChuY;
                        rep.MCS.Value = par.First().MaCS;
                        rep.SThe.Value = par.First().SThe;
                        rep.NguoiThan.Value = par.First().NThan;
                        rep.LyDo.Value = par.First().LyDo;
                        rep.TomTatCLS.Value = par.First().kQLamSang;
                        rep.DaXLy.Value = par.First().DaXuLy;

                        if (par.First().NgayVao != null)
                        {
                            rep.NgayThangNam.Value = DungChung.Ham.NgaySangChu(par.First().NgayVao.Value, DungChung.Bien.FormatDate);
                        }
                        if (xtraTabControl2.SelectedTabPageIndex == 0)
                            rep.TenKPdt.Value = lupKhoaDT.Text;
                        else
                            rep.TenKPdt.Value = lupKhoaDT1.Text;
                    }
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("bệnh nhân chưa nhập vào viện");
                }
                #endregion

            }
            else
            {
                if (_chuyenkhoa == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKMat && inchuyenkhoa)
                {
                    #region mắt
                    BaoCao.repPhieuKBVV_THa_Mat rep = new BaoCao.repPhieuKBVV_THa_Mat();
                    int id = 0;
                    if (!string.IsNullOrEmpty(txtMaBNhan.Text))
                    {
                        int rs;
                        int _int_maBN = 0;
                        if (Int32.TryParse(txtMaBNhan.Text, out rs))
                            _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
                        //string _mabn = txtMaBNhan.Text;
                        var par = (from bn in data.BenhNhans.Where(p => p.MaBNhan == _int_maBN)
                                   join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                                   select new
                                   {
                                       bn.MaBNhan,
                                       bn.TenBNhan,
                                       bn.GTinh,
                                       bn.Tuoi,
                                       bn.DChi,
                                       bn.NamSinh,
                                       bn.NgaySinh,
                                       bn.ThangSinh,
                                       bn.CDNoiGT,
                                       bn.HanBHDen,
                                       bn.SThe,
                                       bn.DTuong,
                                       bn.MaCS,
                                       bn.CapCuu,

                                       vv.NgayVao,
                                       vv.BenhLy,
                                       vv.TienSuBT,
                                       vv.TienSuGD,
                                       vv.KhamTThan,
                                       vv.Tai,
                                       vv.Mui,
                                       vv.MatP,
                                       vv.MatT,
                                       vv.NhanApP,
                                       vv.NhanApT,
                                       vv.kQLamSang,
                                       vv.ChanDoan,
                                       vv.SoVV,
                                       vv.ChuY,
                                       vv.DaXuLy,
                                       vv.LyDo,
                                       vv.Mach,
                                       vv.HuyetAp,
                                       vv.CanNang,
                                       vv.NhietDo,
                                       vv.NhipTho,
                                       vv.ChieuCao,
                                       vv.NhomMau,
                                       vv.HeMau
                                   }).ToList();

                        var kpk = (from kbvv in data.BNKBs.Where(p => p.MaBNhan == _int_maBN).OrderByDescending(p => p.IDKB)
                                   join kp in data.KPhongs on kbvv.MaKP equals kp.MaKP
                                   join cb in data.CanBoes on kbvv.MaCB equals cb.MaCB
                                   select new { kp.TenKP, kbvv.IDKB, TenCB = cb.CapBac + ": " + cb.TenCB, kbvv.NgayKham }).ToList().OrderByDescending(p => p.IDKB).ToList();
                        if (kpk.Count > 0)
                        {
                            rep.TenCB.Value = kpk.First().TenCB.ToString();
                            rep.TenKP.Value = "PHÒNG KHÁM BỆNH: " + kpk.First().TenKP.ToString().ToUpper();
                            int i = int.Parse(kpk.First().IDKB.ToString());
                            var kpdt = (from kbvv in data.BNKBs.Where(p => p.IDKB == i)
                                        join kp in data.KPhongs on kbvv.MaKPdt equals kp.MaKP
                                        select new { kp.TenKP }).ToList();

                            if (kpdt.Count > 0)
                            {
                                rep.TenKPdt.Value = kpdt.First().TenKP;
                            }
                        }
                        var qttbx = (from ttbx in data.TTboXungs.Where(p => p.MaBNhan == _int_maBN)
                                     select new
                                     {
                                         ttbx.MaNN,
                                         ttbx.NgoaiKieu,
                                         ttbx.NThan,
                                         ttbx.DThoaiNT,
                                         ttbx.MaDT,
                                         ttbx.NoiLV,
                                         ttbx.MaHuyen,
                                         ttbx.MaTinh,
                                         ttbx.ThonPho,
                                     }).ToList();
                        if (qttbx.Count > 0)
                        {
                            if (DungChung.Bien.MaBV == "01830")
                            {
                                rep.MaBNhan.Value = "Mã BN: " + par.First().MaBNhan;
                            }

                            rep.xrLabelCanNang.Text = par.First().CanNang;
                            rep.xrLabelHuyetAp.Text = par.First().HuyetAp;
                            rep.xrLabelMach.Text = par.First().Mach;
                            rep.xrLabelNhietDo.Text = par.First().NhietDo;
                            rep.xrLabelNhipTho.Text = par.First().NhipTho;

                            rep.NgoaiKieu.Value = qttbx.First().NgoaiKieu;
                            rep.MaDT.Value = qttbx.First().MaDT;
                            rep.MaNN.Value = qttbx.First().MaNN;
                            rep.MaHuyen.Value = qttbx.First().MaHuyen;
                            rep.MaTinh.Value = qttbx.First().MaTinh;
                            rep.NoiLV.Value = qttbx.First().NoiLV;
                            rep.NguoiNha.Value = qttbx.First().NThan;
                            rep.DienThoai.Value = qttbx.First().DThoaiNT;
                            rep.NguoiThan.Value = qttbx.First().NThan;


                        }
                        var dtoc = (from dt in data.DanTocs join ttbx in data.TTboXungs.Where(p => p.MaBNhan == _mabn) on dt.MaDT equals ttbx.MaDT select dt.TenDT).ToList();
                        if (dtoc.Count > 0)
                            rep.TenDT.Value = dtoc.First();
                        var dnn = (from nn in data.DmNNs join ttbx in data.TTboXungs.Where(p => p.MaBNhan == _mabn) on nn.MaNN equals ttbx.MaNN select nn.TenNN).ToList();
                        if (dnn.Count > 0)
                            rep.NgheNghiep.Value = dnn.First();
                        //var dxa = (from xa in data.DmXas join ttbx in data.TTboXungs.Where(p => p.MaBNhan == _mabn) on xa.MaXa equals ttbx.MaXa select xa.TenXa).ToList();
                        //if (dxa.Count > 0)
                        //    rep.XaPhuong.Value = dxa.First();
                        //var dh = (from hu in data.DmHuyens join ttbx in data.TTboXungs.Where(p => p.MaBNhan == _mabn) on hu.MaHuyen equals ttbx.MaHuyen select hu.TenHuyen).ToList();
                        //if (dh.Count > 0)
                        //    rep.Huyen.Value = dh.First();
                        //var dti = (from ti in data.DmTinhs join ttbx in data.TTboXungs.Where(p => p.MaBNhan == _mabn) on ti.MaTinh equals ttbx.MaTinh select ti.TenTinh).ToList();
                        //if (dti.Count > 0)
                        //    rep.Tinh.Value = dti.First();
                        if (par.Count > 0)
                        {
                            rep.SoVV.Value = par.First().SoVV;
                            rep.CapCuu.Value = par.First().CapCuu;
                            rep.TenBNhan.Value = par.First().TenBNhan.ToUpper();
                            rep.Nam.Value = par.First().GTinh;
                            rep.NgaySinh.Value = par.First().NgaySinh;
                            rep.ThangSinh.Value = par.First().ThangSinh;
                            rep.NSinh.Value = par.First().NamSinh;
                            rep.Tuoi.Value = DungChung.Ham.TuoitheoThang(data, _mabn, DungChung.Bien.formatAge);
                            rep.DChi.Value = par.First().DChi;
                            rep.DTuong.Value = par.First().DTuong;
                            if (par.First().DTuong == "BHYT")
                            {
                                rep.HanBHDen.Value = par.First().HanBHDen.ToString().Substring(0, 10);
                            }
                            rep.CDNoiGT.Value = par.First().CDNoiGT;
                            if (DungChung.Bien.MaBV == "30009")
                            {
                                rep.NgayKham.Value = DungChung.Ham.NgaySangChu(kpk.Last().NgayKham.Value, DungChung.Bien.FormatDate);
                            }
                            else if (DungChung.Bien.MaBV == "27022")//minhvd
                            {
                                var bn = data.BenhNhans.Where(p => p.MaBNhan == _mabn).ToList();
                                rep.NgayKham.Value = DungChung.Ham.NgaySangChu(bn.First().NNhap.Value, 2);
                            }
                            else
                                rep.NgayKham.Value = DungChung.Ham.NgaySangChu(par.First().NgayVao.Value, 2);
                            //if (par.First().NgayVao != null) {
                            //    rep.NgayKham.Value = par.First().NgayVao.Value.Hour+":"+par.First().NgayVao.Value.Minute+"  ";
                            //}
                            rep.BenhLy.Value = par.First().BenhLy;
                            rep.BanThan.Value = par.First().TienSuBT;
                            rep.GiaDinh.Value = par.First().TienSuGD;
                            rep.ToanThan.Value = par.First().KhamTThan;
                            rep.TLKKmp.Value = par.First().Tai;
                            rep.TLKKmt.Value = par.First().Mui;
                            rep.TLCKmp.Value = par.First().MatP;
                            rep.TLCKmt.Value = par.First().MatT;
                            rep.NAmp.Value = par.First().NhanApP;
                            rep.NAmt.Value = par.First().NhanApT;
                            rep.CDoan.Value = par.First().ChanDoan;
                            rep.ChuY.Value = par.First().ChuY;
                            rep.MCS.Value = par.First().MaCS;
                            rep.SThe.Value = par.First().SThe;
                            rep.LyDo.Value = par.First().LyDo;
                            rep.TomTatCLS.Value = par.First().kQLamSang;
                            rep.DaXLy.Value = par.First().DaXuLy;

                            if (par.First().NgayVao != null)
                            {
                                rep.NgayThangNam.Value = DungChung.Ham.NgaySangChu(par.First().NgayVao.Value, DungChung.Bien.FormatDate);

                            }
                            if (xtraTabControl2.SelectedTabPageIndex == 0)
                                rep.TenKPdt.Value = lupKhoaDT.Text;
                            else
                                rep.TenKPdt.Value = lupKhoaDT1.Text;
                        }
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();



                    }
                    else
                    {
                        MessageBox.Show("bệnh nhân chưa nhập vào viện");
                    }
                    #endregion
                }
                else
                {
                    int rs;
                    int _int_maBN = 0;
                    if (Int32.TryParse(txtMaBNhan.Text, out rs))
                        _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
                    if (_chuyenkhoa == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKRangHamMat && inchuyenkhoa)
                    {
                        #region Răng hàm mặt
                        BaoCao.repPhieuKBVV_MS45 rep = new BaoCao.repPhieuKBVV_MS45();
                        int id = 0;
                        if (!string.IsNullOrEmpty(txtMaBNhan.Text))
                        {
                            //string _mabn = txtMaBNhan.Text;
                            var par = (from bn in data.BenhNhans
                                       join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                                       join ttbx in data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                                       where (bn.MaBNhan == _int_maBN)
                                       select new
                                       {
                                           ttbx.MaNN,
                                           ttbx.NgoaiKieu,
                                           ttbx.NThan,
                                           ttbx.DThoaiNT,
                                           ttbx.MaDT,
                                           ttbx.NoiLV,
                                           ttbx.MaHuyen,
                                           ttbx.MaTinh,
                                           ttbx.ThonPho,
                                           bn.TenBNhan,
                                           vv.NgayVao,
                                           bn.GTinh,
                                           bn.MaBNhan,
                                           bn.Tuoi,
                                           bn.DChi,
                                           bn.NamSinh,
                                           bn.NgaySinh,
                                           bn.ThangSinh,
                                           bn.CDNoiGT,
                                           bn.HanBHDen,
                                           bn.SThe,
                                           bn.DTuong,
                                           vv.BenhLy,
                                           vv.TienSuBT,
                                           vv.TienSuGD,
                                           vv.KhamTThan,
                                           vv.Mach,
                                           vv.NhietDo,
                                           vv.HuyetAp,
                                           vv.NhipTho,
                                           vv.CanNang,
                                           vv.KhamBPhan,
                                           vv.kQLamSang,
                                           vv.ChanDoan,
                                           vv.SoVV,
                                           vv.ChuY,
                                           vv.DaXuLy,
                                           bn.MaCS,
                                           vv.LyDo
                                       }).ToList();

                            var kpk = (from kbvv in data.BNKBs.Where(p => p.MaBNhan == _mabn).OrderByDescending(p => p.IDKB)
                                       join kp in data.KPhongs on kbvv.MaKP equals kp.MaKP
                                       join cb in data.CanBoes on kbvv.MaCB equals cb.MaCB
                                       select new { kp.TenKP, kbvv.IDKB, TenCB = cb.CapBac + ": " + cb.TenCB, kbvv.NgayKham }).ToList().OrderByDescending(p => p.IDKB).ToList();
                            if (kpk.Count > 0)
                            {
                                rep.TenCB.Value = kpk.First().TenCB.ToString();
                                rep.TenKP.Value = "PHÒNG KHÁM BỆNH: " + kpk.First().TenKP.ToString().ToUpper();
                                int i = int.Parse(kpk.First().IDKB.ToString());
                                var kpdt = (from kbvv in data.BNKBs.Where(p => p.IDKB == i)
                                            join kp in data.KPhongs on kbvv.MaKPdt equals kp.MaKP
                                            select new { kp.TenKP }).ToList();

                                if (kpdt.Count > 0)
                                {
                                    rep.TenKPdt.Value = kpdt.First().TenKP;
                                }
                            }
                            var dtoc = (from dt in data.DanTocs join ttbx in data.TTboXungs.Where(p => p.MaBNhan == _mabn) on dt.MaDT equals ttbx.MaDT select dt.TenDT).ToList();
                            if (dtoc.Count > 0)
                                rep.TenDT.Value = dtoc.First();
                            //var dxa = (from xa in data.DmXas join ttbx in data.TTboXungs.Where(p => p.MaBNhan == _mabn) on xa.MaXa equals ttbx.MaXa select xa.TenXa).ToList();
                            //if (dxa.Count > 0)
                            //    rep.XaPhuong.Value = dxa.First();
                            //var dh = (from hu in data.DmHuyens join ttbx in data.TTboXungs.Where(p => p.MaBNhan == _mabn) on hu.MaHuyen equals ttbx.MaHuyen select hu.TenHuyen).ToList();
                            //if (dh.Count > 0)
                            //    rep.Huyen.Value = dh.First();
                            //var dti = (from ti in data.DmTinhs join ttbx in data.TTboXungs.Where(p => p.MaBNhan == _mabn) on ti.MaTinh equals ttbx.MaTinh select ti.TenTinh).ToList();
                            //if (dti.Count > 0)
                            //    rep.Tinh.Value = dti.First();
                            if (par.Count > 0)
                            {
                                rep.SoVV.Value = par.First().SoVV;
                                rep.TenBNhan.Value = par.First().TenBNhan.ToUpper();
                                rep.GTinh.Value = par.First().GTinh;
                                rep.Tuoi.Value = DungChung.Ham.TuoitheoThang(data, _mabn, DungChung.Bien.formatAge);
                                rep.NgaySinh.Value = par.First().NgaySinh;
                                rep.ThangSinh.Value = par.First().ThangSinh;
                                rep.NSinh.Value = par.First().NamSinh;
                                if (DungChung.Bien.MaBV == "01830")
                                {
                                    rep.MaBNhan.Value = "Mã BN: " + par.First().MaBNhan;
                                }
                                //string _ngaysinh = "";
                                // if (par.First().NgaySinh != null && par.First().NgaySinh.ToString() != "")
                                //     _ngaysinh = par.First().NgaySinh.ToString() + "/";
                                // if (par.First().ThangSinh != null && par.First().ThangSinh.ToString() != "")
                                //     _ngaysinh = _ngaysinh + par.First().ThangSinh.ToString() + "/";
                                // if (par.First().NamSinh != null && par.First().NamSinh.ToString() != "")
                                //     _ngaysinh = _ngaysinh + par.First().NamSinh.ToString();
                                //// rep.NSinh.Value = _ngaysinh;
                                string mann = "";
                                if (par.First().MaNN != null)
                                    mann = par.First().MaNN;
                                var nn = data.DmNNs.Where(p => p.MaNN == (mann)).ToList();
                                if (nn.Count > 0)
                                    rep.NgheNghiep.Value = nn.First().TenNN;
                                rep.MaDT.Value = par.First().MaDT;
                                rep.MaNN.Value = par.First().MaNN;
                                rep.MaHuyen.Value = par.First().MaHuyen;
                                rep.MaTinh.Value = par.First().MaTinh;
                                rep.NgoaiKieu.Value = par.First().NgoaiKieu;
                                rep.MaDT.Value = par.First().MaDT;
                                rep.DChi.Value = par.First().DChi;
                                rep.NoiLV.Value = par.First().NoiLV;
                                rep.DTuong.Value = par.First().DTuong;
                                if (par.First().DTuong == "BHYT")
                                {
                                    rep.HanBHDen.Value = par.First().HanBHDen;
                                }
                                rep.NguoiNha.Value = par.First().NThan;
                                rep.DienThoai.Value = par.First().DThoaiNT;
                                rep.CDNoiGT.Value = par.First().CDNoiGT;
                                if (DungChung.Bien.MaBV == "30009")
                                {
                                    rep.NgayKham.Value = DungChung.Ham.NgaySangChu(kpk.Last().NgayKham.Value, DungChung.Bien.FormatDate);
                                }
                                else
                                    rep.NgayKham.Value = DungChung.Ham.NgaySangChu(par.First().NgayVao.Value, 2);
                                //if (par.First().NgayVao != null) {
                                //    rep.NgayKham.Value = par.First().NgayVao.Value.Hour+":"+par.First().NgayVao.Value.Minute+"  ";
                                //}
                                rep.BenhLy.Value = par.First().BenhLy;
                                rep.BanThan.Value = par.First().TienSuBT;
                                rep.GiaDinh.Value = par.First().TienSuGD;
                                rep.ToanThan.Value = par.First().KhamTThan;
                                rep.CacBoPhan.Value = par.First().KhamBPhan;
                                rep.Mach.Value = par.First().Mach;
                                rep.NhietDo.Value = par.First().NhietDo;
                                rep.HuyetAp.Value = par.First().HuyetAp;
                                rep.NhipTho.Value = par.First().NhipTho;
                                rep.CanNang.Value = par.First().CanNang;
                                rep.CacBoPhan.Value = par.First().KhamBPhan;
                                rep.CDoan.Value = par.First().ChanDoan;
                                rep.ChuY.Value = par.First().ChuY;
                                #region tiền sử dị ứng
                                if (DungChung.Bien.MaBV == "30007")
                                {
                                    var qtiensudiung = data.TienSuDiUngs.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                                    if (qtiensudiung != null)
                                    {
                                        string mb = "Tiền sử dị ứng:" + Environment.NewLine;
                                        if (qtiensudiung.Thuoc != "" && qtiensudiung.Thuoc != null)
                                            mb += "- Dị ứng thuốc: " + qtiensudiung.Thuoc + "  - Số lần: ";
                                        if (qtiensudiung.Thuoc_SL != null)
                                            mb += qtiensudiung.Thuoc_SL.ToString();
                                        if (qtiensudiung.Thuoc_MoTa != "" && qtiensudiung.Thuoc_MoTa != null)
                                            mb += "   - Mô tả, xử lý: " + qtiensudiung.Thuoc_MoTa + Environment.NewLine;

                                        if (qtiensudiung.ConTrung != "" && qtiensudiung.ConTrung != null)
                                            mb += "- Dị ứng côn trùng: " + qtiensudiung.ConTrung + "  - Số lần: ";
                                        if (qtiensudiung.ConTrung_SL != null)
                                            mb += qtiensudiung.ConTrung_SL.ToString();
                                        if (qtiensudiung.ConTrung_MoTa != "" && qtiensudiung.ConTrung_MoTa != null)
                                            mb += "   - Mô tả, xử lý: " + qtiensudiung.ConTrung_MoTa + Environment.NewLine;

                                        if (qtiensudiung.ThucPham != "" && qtiensudiung.ThucPham != null)
                                            mb += "- Dị ứng thực phẩm: " + qtiensudiung.ThucPham + "  - Số lần: ";
                                        if (qtiensudiung.ThucPham_SL != null)
                                            mb += qtiensudiung.ThucPham_SL.ToString();
                                        if (qtiensudiung.ThucPham_MoTa != "" && qtiensudiung.ThucPham_MoTa != null)
                                            mb += "   - Mô tả, xử lý: " + qtiensudiung.ThucPham_MoTa + Environment.NewLine;

                                        if (qtiensudiung.Khac != "" && qtiensudiung.Khac != null)
                                            mb += "- Dị ứng khác: " + qtiensudiung.Khac + "  - Số lần: ";
                                        if (qtiensudiung.Khac_SL != null)
                                            mb += qtiensudiung.Khac_SL.ToString();
                                        if (qtiensudiung.Khac_MoTa != "" && qtiensudiung.Khac_MoTa != null)
                                            mb += "   - Mô tả, xử lý: " + qtiensudiung.Khac_MoTa + Environment.NewLine;

                                        if (qtiensudiung.TienSuBanThan != "" && qtiensudiung.TienSuBanThan != null)
                                            mb += "- Tiền sử dị ứng bản thân: " + qtiensudiung.TienSuBanThan + "  - Số lần: ";
                                        if (qtiensudiung.TienSuBanThan_SL != null)
                                            mb += qtiensudiung.TienSuBanThan_SL.ToString();
                                        if (qtiensudiung.TienSuBanThan_MoTa != "" && qtiensudiung.TienSuBanThan_MoTa != null)
                                            mb += "   - Mô tả, xử lý: " + qtiensudiung.TienSuBanThan_MoTa + Environment.NewLine;

                                        if (qtiensudiung.TienSuGiaDinh != "" && qtiensudiung.TienSuGiaDinh != null)
                                            mb += "- Tiền sử dị ứng gia đình: " + qtiensudiung.TienSuGiaDinh + "  - Số lần: ";
                                        if (qtiensudiung.TienSuGiaDinh_SL != null)
                                            mb += qtiensudiung.TienSuGiaDinh_SL.ToString();
                                        if (qtiensudiung.TienSuGiaDinh_MoTa != "" && qtiensudiung.TienSuGiaDinh_MoTa != null)
                                            mb += "   - Mô tả, xử lý: " + qtiensudiung.TienSuGiaDinh_MoTa;
                                        rep.ChuY.Value = par.First().ChuY + Environment.NewLine + mb;
                                    }
                                }


                                #endregion

                                rep.MCS.Value = par.First().MaCS;
                                rep.SThe.Value = par.First().SThe;
                                rep.NguoiThan.Value = par.First().NThan;
                                rep.LyDo.Value = par.First().LyDo;
                                rep.TomTatCLS.Value = par.First().kQLamSang;
                                rep.DaXLy.Value = par.First().DaXuLy;
                                if (par.First().NgayVao != null)
                                {
                                    rep.NgayThangNam.Value = DungChung.Ham.NgaySangChu(par.First().NgayVao.Value, DungChung.Bien.FormatDate);

                                }
                                if (xtraTabControl2.SelectedTabPageIndex == 0)
                                    rep.TenKPdt.Value = lupKhoaDT.Text;
                                else
                                {
                                    rep.TenKPdt.Value = lupKhoaDT1.Text;
                                }
                            }
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();

                            #endregion
                        }
                    }
                    else
                    {
                        #region (DungChung.Bien.MaBV == "30012" || DungChung.Bien.MaBV == "30009" || DungChung.Bien.MaBV == "30003")
                        if (DungChung.Bien.MaBV == "30012" || DungChung.Bien.MaBV == "30009" || DungChung.Bien.MaBV == "30003")
                        {
                            BaoCao.repPhieuKBVV_MS42 rep = new BaoCao.repPhieuKBVV_MS42();
                            int id = 0;
                            if (!string.IsNullOrEmpty(txtMaBNhan.Text))
                            {
                                // string _mabn = txtMaBNhan.Text;
                                var par = (from bn in data.BenhNhans
                                           join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                                           join ttbx in data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                                           where (bn.MaBNhan == _int_maBN)
                                           select new
                                           {
                                               ttbx.MaNN,
                                               ttbx.NgoaiKieu,
                                               ttbx.NThan,
                                               ttbx.DThoaiNT,
                                               ttbx.MaDT,
                                               ttbx.NoiLV,
                                               ttbx.MaHuyen,
                                               ttbx.MaTinh,
                                               ttbx.ThonPho,
                                               bn.TenBNhan,
                                               vv.NgayVao,
                                               bn.MaBNhan,
                                               bn.GTinh,
                                               bn.Tuoi,
                                               bn.DChi,
                                               bn.NamSinh,
                                               bn.NgaySinh,
                                               bn.ThangSinh,
                                               bn.CDNoiGT,
                                               bn.HanBHDen,
                                               bn.SThe,
                                               bn.DTuong,
                                               bn.MaCS,
                                               vv.BenhLy,
                                               vv.TienSuBT,
                                               vv.TienSuGD,
                                               vv.KhamTThan,
                                               vv.Mach,
                                               vv.NhietDo,
                                               vv.HuyetAp,
                                               vv.NhipTho,
                                               vv.CanNang,
                                               vv.KhamBPhan,
                                               vv.kQLamSang,
                                               vv.ChanDoan,
                                               vv.SoVV,
                                               vv.ChuY,
                                               vv.DaXuLy,
                                               vv.LyDo
                                           }).ToList();

                                var kpk = (from kbvv in data.BNKBs.Where(p => p.MaBNhan == _int_maBN)
                                           join kp in data.KPhongs on kbvv.MaKP equals kp.MaKP
                                           join cb in data.CanBoes on kbvv.MaCB equals cb.MaCB
                                           select new { kp.TenKP, kbvv.IDKB, kbvv.PhuongAn, TenCB = cb.CapBac + ": " + cb.TenCB, kbvv.NgayKham }).OrderBy(p => p.IDKB).ToList();
                                if (kpk.Count > 0)
                                {
                                    if (kpk.Where(p => p.PhuongAn == 1).ToList().Count > 0)
                                    {
                                        rep.TenCB.Value = kpk.Where(p => p.PhuongAn == 1).First().TenCB;//.CapBac + ": " + kpk.Where(p => p.PhuongAn == 1).First().TenCB;
                                        rep.TenKP.Value = "PHÒNG KHÁM BỆNH: " + kpk.Where(p => p.PhuongAn == 1).First().TenKP.ToString().ToUpper();
                                        int i = kpk.Where(p => p.PhuongAn == 1).First().IDKB;
                                        var kpdt = (from kbvv in data.BNKBs.Where(p => p.IDKB == i)
                                                    join kp in data.KPhongs on kbvv.MaKPdt equals kp.MaKP
                                                    select new { kp.TenKP }).ToList();

                                        if (kpdt.Count > 0)
                                        {
                                            rep.TenKPdt.Value = kpdt.First().TenKP;
                                        }
                                    }
                                    else
                                    {
                                        rep.TenCB.Value = kpk.First().TenCB;//.CapBac + ": " + kpk.First().TenCB.ToString();
                                        rep.TenKP.Value = "PHÒNG KHÁM BỆNH: " + kpk.First().TenKP.ToString().ToUpper();
                                        int i = int.Parse(kpk.First().IDKB.ToString());
                                        var kpdt = (from kbvv in data.BNKBs.Where(p => p.IDKB == i)
                                                    join kp in data.KPhongs on kbvv.MaKPdt equals kp.MaKP
                                                    select new { kp.TenKP }).ToList();

                                        if (kpdt.Count > 0)
                                        {
                                            rep.TenKPdt.Value = kpdt.First().TenKP;
                                        }
                                    }
                                    //rep.TenCB.Value = kpk.First().TenCB.ToString();
                                    //rep.TenKP.Value = "PHÒNG KHÁM BỆNH: " + kpk.First().TenKP.ToString().ToUpper();
                                    //int i = int.Parse(kpk.First().IDKB.ToString());
                                    //var kpdt = (from kbvv in data.BNKBs.Where(p => p.IDKB == i)
                                    //            join kp in data.KPhongs on kbvv.MaKPdt equals kp.MaKP
                                    //            select new { kp.TenKP }).ToList();

                                    //if (kpdt.Count > 0)
                                    //{
                                    //    rep.TenKPdt.Value = kpdt.First().TenKP;
                                    //}
                                }
                                var dtoc = (from dt in data.DanTocs join ttbx in data.TTboXungs.Where(p => p.MaBNhan == _mabn) on dt.MaDT equals ttbx.MaDT select dt.TenDT).ToList();
                                if (dtoc.Count > 0)
                                    rep.TenDT.Value = dtoc.First();
                                var dxa = (from xa in data.DmXas join ttbx in data.TTboXungs.Where(p => p.MaBNhan == _mabn) on xa.MaXa equals ttbx.MaXa select xa.TenXa).ToList();
                                if (dxa.Count > 0)
                                    rep.XaPhuong.Value = dxa.First();
                                var dh = (from hu in data.DmHuyens join ttbx in data.TTboXungs.Where(p => p.MaBNhan == _mabn) on hu.MaHuyen equals ttbx.MaHuyen select hu.TenHuyen).ToList();
                                if (dh.Count > 0)
                                    rep.Huyen.Value = dh.First();
                                var dti = (from ti in data.DmTinhs join ttbx in data.TTboXungs.Where(p => p.MaBNhan == _mabn) on ti.MaTinh equals ttbx.MaTinh select ti.TenTinh).ToList();
                                if (dti.Count > 0)
                                    rep.Tinh.Value = dti.First();
                                if (par.Count > 0)
                                {
                                    if (DungChung.Bien.MaBV == "01830")
                                    {
                                        rep.MaBNhan.Value = "Mã BN: " + par.First().MaBNhan;
                                    }
                                    rep.SoVV.Value = par.First().SoVV;
                                    rep.TenBNhan.Value = par.First().TenBNhan.ToUpper();
                                    rep.GTinh.Value = par.First().GTinh;
                                    rep.Tuoi.Value = DungChung.Ham.TuoitheoThang(data, _mabn, DungChung.Bien.formatAge);
                                    rep.NgaySinh.Value = par.First().NgaySinh;
                                    rep.ThangSinh.Value = par.First().ThangSinh;
                                    rep.NSinh.Value = par.First().NamSinh;
                                    //string _ngaysinh = "";
                                    // if (par.First().NgaySinh != null && par.First().NgaySinh.ToString() != "")
                                    //     _ngaysinh = par.First().NgaySinh.ToString() + "/";
                                    // if (par.First().ThangSinh != null && par.First().ThangSinh.ToString() != "")
                                    //     _ngaysinh = _ngaysinh + par.First().ThangSinh.ToString() + "/";
                                    // if (par.First().NamSinh != null && par.First().NamSinh.ToString() != "")
                                    //     _ngaysinh = _ngaysinh + par.First().NamSinh.ToString();
                                    //// rep.NSinh.Value = _ngaysinh;
                                    string mann = "";
                                    if (par.First().MaNN != null)
                                        mann = par.First().MaNN;
                                    var nn = data.DmNNs.Where(p => p.MaNN == (mann)).ToList();
                                    if (nn.Count > 0)
                                        rep.NgheNghiep.Value = nn.First().TenNN;
                                    rep.MaDT.Value = par.First().MaDT;
                                    rep.MaNN.Value = par.First().MaNN;
                                    rep.MaHuyen.Value = par.First().MaHuyen;
                                    rep.MaTinh.Value = par.First().MaTinh;
                                    rep.NgoaiKieu.Value = par.First().NgoaiKieu;
                                    rep.MaDT.Value = par.First().MaDT;
                                    rep.DChi.Value = par.First().ThonPho;
                                    rep.NoiLV.Value = par.First().NoiLV;
                                    rep.DTuong.Value = par.First().DTuong;
                                    if (par.First().DTuong == "BHYT")
                                    {
                                        rep.HanBHDen.Value = par.First().HanBHDen;
                                    }
                                    rep.NguoiNha.Value = par.First().NThan;
                                    rep.DienThoai.Value = par.First().DThoaiNT;
                                    rep.CDNoiGT.Value = par.First().CDNoiGT;
                                    if (DungChung.Bien.MaBV == "30009")
                                    {
                                        rep.NgayKham.Value = DungChung.Ham.NgaySangChu(kpk.First().NgayKham.Value, DungChung.Bien.FormatDate);
                                    }
                                    else
                                        rep.NgayKham.Value = DungChung.Ham.NgaySangChu(par.First().NgayVao.Value, 2);
                                    //if (par.First().NgayVao != null) {
                                    //    rep.NgayKham.Value = par.First().NgayVao.Value.Hour+":"+par.First().NgayVao.Value.Minute+"  ";
                                    //}
                                    rep.BenhLy.Value = par.First().BenhLy;
                                    rep.BanThan.Value = par.First().TienSuBT;
                                    rep.GiaDinh.Value = par.First().TienSuGD;
                                    rep.ToanThan.Value = par.First().KhamTThan;
                                    rep.CacBoPhan.Value = par.First().KhamBPhan;
                                    rep.Mach.Value = par.First().Mach;
                                    rep.NhietDo.Value = par.First().NhietDo;
                                    rep.HuyetAp.Value = par.First().HuyetAp;
                                    rep.NhipTho.Value = par.First().NhipTho;
                                    rep.CanNang.Value = par.First().CanNang;
                                    rep.CacBoPhan.Value = par.First().KhamBPhan;
                                    rep.CDoan.Value = par.First().ChanDoan;
                                    rep.ChuY.Value = par.First().ChuY;
                                    rep.MCS.Value = par.First().MaCS;
                                    rep.SThe.Value = par.First().SThe;
                                    rep.NguoiThan.Value = par.First().NThan;
                                    rep.LyDo.Value = par.First().LyDo;
                                    rep.TomTatCLS.Value = par.First().kQLamSang;
                                    rep.DaXLy.Value = par.First().DaXuLy;
                                    if (par.First().NgayVao != null)
                                    {
                                        rep.NgayThangNam.Value = DungChung.Ham.NgaySangChu(par.First().NgayVao.Value, DungChung.Bien.FormatDate);
                                    }
                                    if (xtraTabControl2.SelectedTabPageIndex == 0)
                                        rep.TenKPdt.Value = lupKhoaDT.Text;
                                    else
                                        rep.TenKPdt.Value = lupKhoaDT1.Text;
                                    #region tiền sử dị ứng
                                    if (DungChung.Bien.MaBV == "30007")
                                    {
                                        var qtiensudiung = data.TienSuDiUngs.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                                        if (qtiensudiung != null)
                                        {
                                            string mb = "Tiền sử dị ứng:" + Environment.NewLine;
                                            if (qtiensudiung.Thuoc != "" && qtiensudiung.Thuoc != null)
                                                mb += "- Dị ứng thuốc: " + qtiensudiung.Thuoc + "  - Số lần: ";
                                            if (qtiensudiung.Thuoc_SL != null)
                                                mb += qtiensudiung.Thuoc_SL.ToString();
                                            if (qtiensudiung.Thuoc_MoTa != "" && qtiensudiung.Thuoc_MoTa != null)
                                                mb += "   - Mô tả, xử lý: " + qtiensudiung.Thuoc_MoTa + Environment.NewLine;

                                            if (qtiensudiung.ConTrung != "" && qtiensudiung.ConTrung != null)
                                                mb += "- Dị ứng côn trùng: " + qtiensudiung.ConTrung + "  - Số lần: ";
                                            if (qtiensudiung.ConTrung_SL != null)
                                                mb += qtiensudiung.ConTrung_SL.ToString();
                                            if (qtiensudiung.ConTrung_MoTa != "" && qtiensudiung.ConTrung_MoTa != null)
                                                mb += "   - Mô tả, xử lý: " + qtiensudiung.ConTrung_MoTa + Environment.NewLine;

                                            if (qtiensudiung.ThucPham != "" && qtiensudiung.ThucPham != null)
                                                mb += "- Dị ứng thực phẩm: " + qtiensudiung.ThucPham + "  - Số lần: ";
                                            if (qtiensudiung.ThucPham_SL != null)
                                                mb += qtiensudiung.ThucPham_SL.ToString();
                                            if (qtiensudiung.ThucPham_MoTa != "" && qtiensudiung.ThucPham_MoTa != null)
                                                mb += "   - Mô tả, xử lý: " + qtiensudiung.ThucPham_MoTa + Environment.NewLine;

                                            if (qtiensudiung.Khac != "" && qtiensudiung.Khac != null)
                                                mb += "- Dị ứng khác: " + qtiensudiung.Khac + "  - Số lần: ";
                                            if (qtiensudiung.Khac_SL != null)
                                                mb += qtiensudiung.Khac_SL.ToString();
                                            if (qtiensudiung.Khac_MoTa != "" && qtiensudiung.Khac_MoTa != null)
                                                mb += "   - Mô tả, xử lý: " + qtiensudiung.Khac_MoTa + Environment.NewLine;

                                            if (qtiensudiung.TienSuBanThan != "" && qtiensudiung.TienSuBanThan != null)
                                                mb += "- Tiền sử dị ứng bản thân: " + qtiensudiung.TienSuBanThan + "  - Số lần: ";
                                            if (qtiensudiung.TienSuBanThan_SL != null)
                                                mb += qtiensudiung.TienSuBanThan_SL.ToString();
                                            if (qtiensudiung.TienSuBanThan_MoTa != "" && qtiensudiung.TienSuBanThan_MoTa != null)
                                                mb += "   - Mô tả, xử lý: " + qtiensudiung.TienSuBanThan_MoTa + Environment.NewLine;

                                            if (qtiensudiung.TienSuGiaDinh != "" && qtiensudiung.TienSuGiaDinh != null)
                                                mb += "- Tiền sử dị ứng gia đình: " + qtiensudiung.TienSuGiaDinh + "  - Số lần: ";
                                            if (qtiensudiung.TienSuGiaDinh_SL != null)
                                                mb += qtiensudiung.TienSuGiaDinh_SL.ToString();
                                            if (qtiensudiung.TienSuGiaDinh_MoTa != "" && qtiensudiung.TienSuGiaDinh_MoTa != null)
                                                mb += "   - Mô tả, xử lý: " + qtiensudiung.TienSuGiaDinh_MoTa;
                                            rep.ChuY.Value = par.First().ChuY + Environment.NewLine + mb;
                                        }
                                    }
                                    #endregion
                                }
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();


                            }
                            #endregion
                        }
                        else
                        {
                            if (DungChung.Bien.MaBV == "14018")
                            {
                                List<PKBVV> KBVV = new List<PKBVV>();
                                var par = (from bn in data.BenhNhans
                                           join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                                           join ttbx in data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                                           where (bn.MaBNhan == _int_maBN)
                                           select new
                                           {
                                               vv.ChieuCao,
                                               bn.TenBNhan,
                                               vv.NgayVao,
                                               bn.GTinh,
                                               bn.Tuoi,
                                               ttbx.MaDT,
                                               bn.MaKP,
                                               bn.MaBNhan,
                                               bn.DChi,
                                               bn.NamSinh,
                                               bn.NgaySinh,
                                               bn.ThangSinh,
                                               bn.CDNoiGT,
                                               bn.HanBHDen,
                                               bn.SThe,
                                               ttbx.MaNN,
                                               ttbx.NgoaiKieu,
                                               bn.DTuong,
                                               ttbx.NThan,
                                               ttbx.DThoaiNT,
                                               ttbx.ThonPho,
                                               vv.BenhLy,
                                               vv.TienSuBT,
                                               vv.TienSuGD,
                                               vv.KhamTThan,
                                               vv.Mach,
                                               vv.NhietDo,
                                               vv.HuyetAp,
                                               vv.NhipTho,
                                               vv.CanNang,
                                               vv.KhamBPhan,
                                               vv.kQLamSang,
                                               vv.ChanDoan,
                                               vv.SoVV,
                                               vv.ChuY,
                                               vv.DaXuLy,
                                               ttbx.NoiLV,
                                               bn.MaCS,
                                               vv.LyDo

                                           }).ToList();
                                foreach (var item in par.ToList())
                                {
                                    PKBVV themmoi = new PKBVV();
                                    themmoi.SoVV = item.SoVV;
                                    themmoi.ChieuCao = item.ChieuCao;
                                    themmoi.TenBNhan = item.TenBNhan.ToUpper();
                                    themmoi.NgayVao = DungChung.Ham.NgaySangChu(Convert.ToDateTime(item.NgayVao), 8);

                                    if (Convert.ToInt32(item.GTinh) == 1)
                                    {
                                        themmoi.Nam = "X";
                                    }
                                    else
                                    {
                                        themmoi.Nu = "X";
                                    }

                                    themmoi.Tuoi = ": " + DungChung.Ham.CalculateAge(item.NgaySinh, item.ThangSinh, item.NamSinh, " tháng.");
                                    themmoi.NgayThang = DungChung.Ham.NgaySangChu(System.DateTime.Now, 1);
                                    if (item.MaDT != null)
                                    {
                                        themmoi.DanToc = data.DanTocs.Where(p => p.MaDT == item.MaDT).First().TenDT;
                                    }
                                    else
                                    {
                                        themmoi.DanToc = "";
                                    }
                                    if (item.MaKP != null)
                                    {
                                        themmoi.TenKP = data.KPhongs.Where(p => p.MaKP == item.MaKP).First().TenKP;
                                    }

                                    themmoi.DChi = item.DChi;
                                    themmoi.NgaySinh = item.NgaySinh + " / " + item.ThangSinh + " / " + item.NamSinh;
                                    themmoi.CDNoiGT = item.CDNoiGT == null ? "" : item.CDNoiGT; ;
                                    if (item.HanBHDen != null)
                                    {
                                        themmoi.HanBHDen = DungChung.Ham.NgaySangChu(Convert.ToDateTime(item.HanBHDen), 7);
                                    }
                                    if (item.SThe != "")
                                    {
                                        themmoi.ST1 = item.SThe.Substring(0, 2);
                                        themmoi.ST2 = item.SThe.Substring(2, 1);
                                        themmoi.ST3 = item.SThe.Substring(3, 2);
                                        themmoi.ST4 = item.SThe.Substring(5, 2);
                                        themmoi.ST5 = item.SThe.Substring(7, 3);
                                        themmoi.ST6 = item.SThe.Substring(10, 5);
                                    }
                                    themmoi.MaCS = item.MaCS == null ? "" : item.MaCS;
                                    if (item.MaNN != null)
                                    {
                                        themmoi.MaNN = data.DmNNs.Where(p => p.MaNN == item.MaNN).First().TenNN;
                                    }
                                    else
                                    {
                                        themmoi.MaNN = "";
                                    }
                                    themmoi.NgoaiKieu = item.NgoaiKieu;
                                    if (item.DTuong == "BHYT")
                                    {
                                        themmoi.BHYT = "X";
                                    }
                                    else
                                    {
                                        themmoi.DichVu = "X";
                                    }

                                    themmoi.NThan = item.NThan == null ? "" : item.NThan; ;
                                    themmoi.DThoaiNT = item.DThoaiNT == null ? "" : item.DThoaiNT;
                                    themmoi.BenhLy = item.BenhLy;
                                    themmoi.TienSuBT = "1. Bản thân:" + item.TienSuBT;
                                    themmoi.TienSuGD = "2. Gia Đình:" + item.TienSuGD;
                                    themmoi.KhamTThan = "1. Toàn thân: " + item.KhamTThan;
                                    themmoi.Mach = item.Mach;
                                    themmoi.NhietDo = item.NhietDo;
                                    themmoi.HuyetAp = item.HuyetAp;
                                    themmoi.NhipTho = item.NhipTho;
                                    themmoi.CanNang = item.CanNang;
                                    themmoi.KhamBPhan = "2. Các bộ phận: " + item.KhamBPhan;
                                    themmoi.kQLamSang = item.kQLamSang;
                                    themmoi.ChanDoan = DungChung.Ham.GetChanDoanKB(data, _mabn);
                                    themmoi.SoVV = item.SoVV;
                                    themmoi.ChuY = item.ChuY;
                                    themmoi.DaXuLy = item.DaXuLy;
                                    themmoi.NoiLV = item.NoiLV == null ? "" : item.NoiLV;
                                    themmoi.MaCS = item.MaCS;
                                    themmoi.LyDo = item.LyDo;
                                    themmoi.CQCQ = DungChung.Bien.TenCQCQ.ToUpper();
                                    themmoi.CQ = DungChung.Bien.TenCQ.ToUpper();
                                    themmoi.TenCB = DungChung.Ham._getTenCB(data, DungChung.Bien.MaCB);
                                    KBVV.Add(themmoi);
                                }

                                DungChung.Ham.Print(DungChung.PrintConfig.Rep_KBVV_14018, KBVV, new Dictionary<string, object>(), false);

                            }
                            else
                            {


                                BaoCao.repPhieuKBVV rep = new BaoCao.repPhieuKBVV();
                                if (DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "24012")
                                {
                                    rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                                }
                                else
                                {
                                    rep.TenCQ.Value = DungChung.Bien.TenCQ;
                                }
                                if (DungChung.Bien.MaBV == "24012")
                                {
                                    rep.TenCQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                                }
                                else
                                {
                                    rep.TenCQCQ.Value = DungChung.Bien.TenCQCQ;
                                }
                                int id = 0;
                                if (!string.IsNullOrEmpty(txtMaBNhan.Text))
                                {
                                    // string _mabn = txtMaBNhan.Text;
                                    var par = (from bn in data.BenhNhans
                                               join bnkb in data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                                               join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                                               join ttbx in data.TTboXungs on bn.MaBNhan equals ttbx.MaBNhan
                                               where (bn.MaBNhan == _int_maBN)
                                               select new
                                               {
                                                   bnkb.ChanDoanYHCT,
                                                   bnkb.IDKB,
                                                   bnkb.NgayKham,
                                                   bnkb.BenhKhacYHCT,
                                                   bnkb.BenhKhac,
                                                   bnkb.MaICD2,
                                                   vv.ChieuCao,
                                                   bn.TenBNhan,
                                                   vv.NgayVao,
                                                   bn.GTinh,
                                                   bn.Tuoi,
                                                   ttbx.MaDT,
                                                   bn.MaBNhan,
                                                   bn.DChi,
                                                   bn.NamSinh,
                                                   bn.NgaySinh,
                                                   bn.ThangSinh,
                                                   bn.CDNoiGT,
                                                   bn.HanBHDen,
                                                   bn.SThe,
                                                   ttbx.MaNN,
                                                   ttbx.NgoaiKieu,
                                                   bn.DTuong,
                                                   ttbx.NThan,
                                                   ttbx.DThoaiNT,
                                                   ttbx.ThonPho,
                                                   vv.BenhLy,
                                                   vv.TienSuBT,
                                                   vv.TienSuGD,
                                                   vv.KhamTThan,
                                                   vv.Mach,
                                                   vv.NhietDo,
                                                   vv.HuyetAp,
                                                   vv.NhipTho,
                                                   vv.CanNang,
                                                   vv.KhamBPhan,
                                                   vv.kQLamSang,
                                                   vv.ChanDoan,
                                                   bnkb.MaICD,
                                                   vv.SoVV,
                                                   vv.ChuY,
                                                   vv.DaXuLy,
                                                   ttbx.NoiLV,
                                                   bn.MaCS,
                                                   vv.LyDo,
                                                   ttbx.DCNguoiThan
                                               }).OrderByDescending(p => p.NgayKham).ToList();

                                    var getcdpk = data.BNKBs.Where(p => p.MaBNhan == _mabn).Where(p => p.PhuongAn == 1).OrderByDescending(p => p.IDKB).ToList();

                                    var kpk = (from kbvv in data.BNKBs.Where(p => p.MaBNhan == _int_maBN)
                                               join kp in data.KPhongs on kbvv.MaKP equals kp.MaKP
                                               join cb in data.CanBoes on kbvv.MaCB equals cb.MaCB
                                               select new { cb.CapBac, kp.TenKP, kbvv.IDKB, cb.TenCB, kbvv.PhuongAn, kbvv.NgayKham }).OrderBy(p => p.IDKB).ToList();
                                    if (kpk.Count > 0)
                                    {
                                        if (kpk.Where(p => p.PhuongAn == 1).ToList().Count > 0)
                                        {
                                            rep.TenCB.Value = kpk.Where(p => p.PhuongAn == 1).First().CapBac + ": " + kpk.Where(p => p.PhuongAn == 1).First().TenCB;
                                            if (DungChung.Bien.MaBV == "14017" || DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "24272")
                                            {
                                                rep.TenKP.Value = "PHÒNG: " + kpk.Where(p => p.PhuongAn == 1).First().TenKP.ToString().ToUpper();
                                            }
                                            else
                                            {
                                                rep.TenKP.Value = "PHÒNG KHÁM BỆNH: " + kpk.Where(p => p.PhuongAn == 1).First().TenKP.ToString().ToUpper();
                                            }


                                            int i = kpk.Where(p => p.PhuongAn == 1).First().IDKB;
                                            var kpdt = (from kbvv in data.BNKBs.Where(p => p.IDKB == i)
                                                        join kp in data.KPhongs on kbvv.MaKPdt equals kp.MaKP
                                                        select new { kp.TenKP }).ToList();

                                            if (kpdt.Count > 0)
                                            {
                                                rep.TenKPdt.Value = kpdt.First().TenKP;
                                            }
                                        }
                                        else
                                        {
                                            rep.TenCB.Value = kpk.First().CapBac + ": " + kpk.First().TenCB.ToString();
                                            if (DungChung.Bien.MaBV == "14017")
                                            {
                                                rep.TenKP.Value = "PHÒNG: " + kpk.First().TenKP.ToString().ToUpper();
                                            }
                                            else if ( DungChung.Bien.MaBV == "24272")
                                            {
                                                rep.TenKP.Value = kpk.First().TenKP.ToString().ToUpper();
                                            }
                                            else 
                                            { 
                                                rep.TenKP.Value = "PHÒNG KHÁM BỆNH: " + kpk.First().TenKP.ToString().ToUpper(); 
                                            }

                                            int i = int.Parse(kpk.First().IDKB.ToString());
                                            var kpdt = (from kbvv in data.BNKBs.Where(p => p.IDKB == i)
                                                        join kp in data.KPhongs on kbvv.MaKPdt equals kp.MaKP
                                                        select new { kp.TenKP }).ToList();

                                            if (kpdt.Count > 0)
                                            {
                                                rep.TenKPdt.Value = kpdt.First().TenKP;
                                            }
                                        }
                                    }
                                    if (DungChung.Bien.MaBV == "01049")
                                    {
                                        var dtoc = (from ttbx in data.TTboXungs.Where(p => p.MaBNhan == _mabn)
                                                    join dt in data.DanTocs on ttbx.MaDT equals dt.MaDTBak
                                                    select dt.TenDT).ToList();
                                        if (dtoc.Count > 0)
                                            rep.TenDT.Value = dtoc.First();
                                    }
                                    else
                                    {
                                        var dtoc = (from dt in data.DanTocs join ttbx in data.TTboXungs.Where(p => p.MaBNhan == _mabn) on dt.MaDT equals ttbx.MaDT select dt.TenDT).ToList();
                                        if (dtoc.Count > 0)
                                            rep.TenDT.Value = dtoc.First();
                                    }
                                    if (par.Count > 0)
                                    {
                                        if (DungChung.Bien.MaBV == "14017")
                                        {
                                            string[] _MaICDarr = DungChung.Ham.getMaICDarrFull(data, _mabn, DungChung.Bien.GetICD, 0);
                                            string[] icd = _MaICDarr[0].Split(';');
                                            string[] tenicd = _MaICDarr[1].Split(';');
                                            string lydo = "";
                                            if (icd.Length > 0 && !string.IsNullOrEmpty(icd[0]))
                                            {
                                                lydo += " " + icd[0] + "-" + tenicd[0] + ";";
                                            }
                                            if (icd.Length > 1 && !string.IsNullOrEmpty(icd[1]))
                                            {
                                                lydo += " " + icd[1] + "-" + tenicd[1] + ";";
                                            }
                                            if (icd.Length > 2 && !string.IsNullOrEmpty(icd[2]))
                                            {
                                                lydo += " " + icd[2] + "-" + tenicd[2] + ";";
                                            }
                                            if (icd.Length > 3)
                                            {
                                                string mabk = DungChung.Ham.FreshString(string.Join(";", icd.Skip(3)));
                                                string mab1k = DungChung.Ham.FreshString(string.Join(";", tenicd.Skip(3)));
                                                lydo += " " + icd[3] + "-" + mab1k + ";";
                                            }
                                            if (_MaICDarr.Length >= 8)
                                                lydo += DungChung.Ham.FreshString(_MaICDarr[4]);
                                            rep.CDoan.Value = DungChung.Ham.FreshString(lydo);
                                        }
                                        else if (DungChung.Bien.MaBV == "24297")
                                        {
                                            string chandoanFull = "";
                                            var tenkp = data.BNKBs.Where(p => p.MaBNhan == _mabn).Where(p => p.PhuongAn == 1).OrderByDescending(p => p.IDKB).ToList();
                                            var icd = data.ICD10.Where(o => true).ToList();
                                            string ic = getcdpk.First().MaICD + ";" + getcdpk.First().MaICD2;
                                            if (!string.IsNullOrEmpty(getcdpk.First().MaICD))
                                            {
                                                chandoanFull += DungChung.Ham.GhepChuoiChanDoanYHCT(icd, "", getcdpk.First().MaICD);
                                            }
                                            var icdBenhkhac = getcdpk.First().MaICD2.Split(';');
                                            var tenBenhkhac = getcdpk.First().BenhKhac.Split(';');
                                            string benhkocoICD = "";
                                            string benhcoICD = "";
                                            if (icdBenhkhac.Count() > 0)
                                            {
                                                int flag = 1;
                                                for (int i = 0; i < icdBenhkhac.Count(); i++)
                                                {
                                                    if (!string.IsNullOrEmpty(icdBenhkhac[i]))
                                                    {
                                                        flag += 1;
                                                    }
                                                }
                                                if (flag == 1)
                                                {
                                                    chandoanFull += ";" + getcdpk.First().BenhKhac;
                                                }
                                                else
                                                {

                                                    if (tenBenhkhac.Count() > flag)
                                                    {
                                                        for (int i = flag - 1; i < tenBenhkhac.Count(); i++)
                                                        {
                                                            benhkocoICD += tenBenhkhac[i];
                                                        }
                                                        benhkocoICD += benhcoICD;
                                                    }
                                                    benhcoICD += DungChung.Ham.GhepChuoiChanDoanYHCT(icd, "", getcdpk.First().MaICD2);
                                                    chandoanFull += benhcoICD;
                                                }
                                            }
                                            var kq = (chandoanFull).Split(';');
                                            string cd = "";
                                            for (int i = 0; i < kq.Count(); i++)
                                            {
                                                if (!string.IsNullOrEmpty(kq[i]) && !string.IsNullOrWhiteSpace(kq[i]))
                                                {
                                                    cd += kq[i] + ";";
                                                }
                                            }
                                            //rep.CDoan.Value = cd + benhkocoICD;
                                            rep.CDoan.Value = par.First().ChanDoan;
                                        }
                                        else { rep.CDoan.Value = par.First().ChanDoan; }
                                        rep.SoVV.Value = par.First().SoVV;
                                        rep.TenBNhan.Value = par.First().TenBNhan.ToUpper();
                                        rep.GTinh.Value = par.First().GTinh;
                                        rep.Tuoi.Value = DungChung.Ham.TuoitheoThang(data, _mabn, DungChung.Bien.formatAge);
                                        if (DungChung.Bien.MaBV == "01830")
                                        {
                                            rep.MaBNhan.Value = "Mã BN: " + par.First().MaBNhan;
                                        }
                                        if (DungChung.Bien.MaBV == "24012")
                                        {
                                            rep.MaBNhan.Value = par.First().MaBNhan;
                                        }
                                        string _ngaysinh = "";
                                        if (par.First().NgaySinh != null && par.First().NgaySinh.ToString() != "")
                                            _ngaysinh = par.First().NgaySinh.ToString() + "/";
                                        if (par.First().ThangSinh != null && par.First().ThangSinh.ToString() != "")
                                            _ngaysinh = _ngaysinh + par.First().ThangSinh.ToString() + "/";
                                        if (par.First().NamSinh != null && par.First().NamSinh.ToString() != "")
                                            _ngaysinh = _ngaysinh + par.First().NamSinh.ToString();
                                        rep.NSinh.Value = _ngaysinh;
                                        string mann = "";
                                        if (par.First().MaNN != null)
                                            mann = par.First().MaNN;
                                        var nn = data.DmNNs.Where(p => p.MaNN == (mann)).ToList();
                                        if (nn.Count > 0)
                                        {
                                            rep.NgheNghiep.Value = nn.First().TenNN;
                                            rep.MaNN.Value = nn.First().MaNN;
                                        }
                                        rep.NgoaiKieu.Value = par.First().NgoaiKieu;
                                        rep.MaDT.Value = par.First().MaDT;
                                        rep.DChi.Value = par.First().DChi;
                                        rep.NoiLV.Value = par.First().NoiLV;
                                        rep.DTuong.Value = par.First().DTuong;
                                        if (par.First().DTuong == "BHYT")
                                        {
                                            rep.HanBHDen.Value = par.First().HanBHDen;
                                        }
                                        rep.NguoiNha.Value = par.First().NThan;
                                        rep.DienThoai.Value = par.First().DThoaiNT;
                                        if (DungChung.Bien.MaBV == "14017")
                                        {
                                            //hthong != null && !string.IsNullOrWhiteSpace(hthong.ImageURL)
                                            if (string.IsNullOrWhiteSpace(par.First().CDNoiGT))
                                            {
                                                rep.CDNoiGT.Value = "Tự đến";
                                            }
                                            else { rep.CDNoiGT.Value = par.First().CDNoiGT; }

                                        }
                                        else { rep.CDNoiGT.Value = par.First().CDNoiGT; }

                                        if (DungChung.Bien.MaBV == "30009")
                                        {
                                            rep.NgayKham.Value = DungChung.Ham.NgaySangChu(kpk.First().NgayKham.Value, DungChung.Bien.FormatDate);
                                        }
                                        else
                                            rep.NgayKham.Value = DungChung.Ham.NgaySangChu(par.First().NgayVao.Value, 2);
                                        //if (par.First().NgayVao != null) {
                                        //    rep.NgayKham.Value = par.First().NgayVao.Value.Hour+":"+par.First().NgayVao.Value.Minute+"  ";
                                        //}
                                        rep.BenhLy.Value = par.First().BenhLy;
                                        rep.BanThan.Value = par.First().TienSuBT;
                                        rep.GiaDinh.Value = par.First().TienSuGD;
                                        rep.ToanThan.Value = par.First().KhamTThan;
                                        rep.CacBoPhan.Value = par.First().KhamBPhan;
                                        rep.Mach.Value = par.First().Mach;
                                        rep.NhietDo.Value = par.First().NhietDo;
                                        rep.HuyetAp.Value = par.First().HuyetAp;
                                        rep.NhipTho.Value = par.First().NhipTho;
                                        rep.CanNang.Value = par.First().CanNang;
                                        rep.ChieuCao.Value = par.First().ChieuCao;

                                        try
                                        {
                                            double cannang = 0, chieucao = 0;
                                            if(par.Count() > 0)
                                            {
                                                if (par.First().CanNang != null && par.First().CanNang.Trim() != "")
                                                    cannang = Convert.ToDouble(par.First().CanNang);
                                                if (par.First().ChieuCao != null && par.First().ChieuCao.Trim() != "")
                                                    chieucao = Convert.ToDouble(par.First().ChieuCao);
                                                if(cannang != 0 || chieucao != 0)
                                                rep.BMI.Value = Math.Round(cannang / (chieucao * 0.01 * chieucao * 0.01), 2);
                                            }
                                        }
                                        catch
                                        {
                                            rep.BMI.Value = "";
                                        }
                                        rep.CacBoPhan.Value = par.First().KhamBPhan;



                                        rep.ChuY.Value = par.First().ChuY;
                                        rep.MCS.Value = par.First().MaCS;
                                        rep.SThe.Value = par.First().SThe;
                                        string DCNT = DungChung.Bien.MaBV != "20001" ? "" : (par.First().DCNguoiThan == null ? " " : " : " + par.First().DCNguoiThan);
                                        rep.NguoiThan.Value = par.First().NThan + " " + DCNT;
                                        rep.LyDo.Value = par.First().LyDo;
                                        rep.TomTatCLS.Value = par.First().kQLamSang;
                                        rep.DaXLy.Value = par.First().DaXuLy;
                                        if (par.First().NgayVao != null)
                                        {
                                            rep.NgayThangNam.Value = DungChung.Ham.NgaySangChu(par.First().NgayVao.Value, DungChung.Bien.FormatDate);

                                        }
                                        if (xtraTabControl2.SelectedTabPageIndex == 0)
                                            rep.TenKPdt.Value = lupKhoaDT.Text;
                                        else
                                            rep.TenKPdt.Value = lupKhoaDT1.Text;
                                        #region tiền sử dị ứng
                                        if (DungChung.Bien.MaBV == "30007")
                                        {
                                            var qtiensudiung = data.TienSuDiUngs.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                                            if (qtiensudiung != null)
                                            {
                                                string mb = "Tiền sử dị ứng:" + Environment.NewLine;
                                                if (qtiensudiung.Thuoc != "" && qtiensudiung.Thuoc != null)
                                                {
                                                    mb += "- Dị ứng thuốc: " + qtiensudiung.Thuoc + "  - Số lần: ";
                                                    if (qtiensudiung.Thuoc_SL != null)
                                                        mb += qtiensudiung.Thuoc_SL.ToString();
                                                    if (qtiensudiung.Thuoc_MoTa != "" && qtiensudiung.Thuoc_MoTa != null)
                                                        mb += "   - Mô tả, xử lý: " + qtiensudiung.Thuoc_MoTa + Environment.NewLine;
                                                }

                                                if (qtiensudiung.ConTrung != "" && qtiensudiung.ConTrung != null)
                                                {
                                                    mb += "- Dị ứng côn trùng: " + qtiensudiung.ConTrung + "  - Số lần: ";
                                                    if (qtiensudiung.ConTrung_SL != null)
                                                        mb += qtiensudiung.ConTrung_SL.ToString();
                                                    if (qtiensudiung.ConTrung_MoTa != "" && qtiensudiung.ConTrung_MoTa != null)
                                                        mb += "   - Mô tả, xử lý: " + qtiensudiung.ConTrung_MoTa + Environment.NewLine;
                                                }

                                                if (qtiensudiung.ThucPham != "" && qtiensudiung.ThucPham != null)
                                                {
                                                    mb += "- Dị ứng thực phẩm: " + qtiensudiung.ThucPham + "  - Số lần: ";
                                                    if (qtiensudiung.ThucPham_SL != null)
                                                        mb += qtiensudiung.ThucPham_SL.ToString();
                                                    if (qtiensudiung.ThucPham_MoTa != "" && qtiensudiung.ThucPham_MoTa != null)
                                                        mb += "   - Mô tả, xử lý: " + qtiensudiung.ThucPham_MoTa + Environment.NewLine;
                                                }

                                                if (qtiensudiung.Khac != "" && qtiensudiung.Khac != null)
                                                {
                                                    mb += "- Dị ứng khác: " + qtiensudiung.Khac + "  - Số lần: ";
                                                    if (qtiensudiung.Khac_SL != null)
                                                        mb += qtiensudiung.Khac_SL.ToString();
                                                    if (qtiensudiung.Khac_MoTa != "" && qtiensudiung.Khac_MoTa != null)
                                                        mb += "   - Mô tả, xử lý: " + qtiensudiung.Khac_MoTa + Environment.NewLine;
                                                }

                                                if (qtiensudiung.TienSuBanThan != "" && qtiensudiung.TienSuBanThan != null)
                                                {
                                                    mb += "- Tiền sử dị ứng bản thân: " + qtiensudiung.TienSuBanThan + "  - Số lần: ";
                                                    if (qtiensudiung.TienSuBanThan_SL != null)
                                                        mb += qtiensudiung.TienSuBanThan_SL.ToString();
                                                    if (qtiensudiung.TienSuBanThan_MoTa != "" && qtiensudiung.TienSuBanThan_MoTa != null)
                                                        mb += "   - Mô tả, xử lý: " + qtiensudiung.TienSuBanThan_MoTa + Environment.NewLine;
                                                }

                                                if (qtiensudiung.TienSuGiaDinh != "" && qtiensudiung.TienSuGiaDinh != null)
                                                {
                                                    mb += "- Tiền sử dị ứng gia đình: " + qtiensudiung.TienSuGiaDinh + "  - Số lần: ";
                                                    if (qtiensudiung.TienSuGiaDinh_SL != null)
                                                        mb += qtiensudiung.TienSuGiaDinh_SL.ToString();
                                                    if (qtiensudiung.TienSuGiaDinh_MoTa != "" && qtiensudiung.TienSuGiaDinh_MoTa != null)
                                                        mb += "   - Mô tả, xử lý: " + qtiensudiung.TienSuGiaDinh_MoTa;
                                                }

                                                if (qtiensudiung.Thuoc_SL == 0
                                                    && qtiensudiung.ConTrung_SL == 0
                                                    && qtiensudiung.ThucPham_SL == 0
                                                    && qtiensudiung.Khac_SL == 0
                                                    && qtiensudiung.TienSuBanThan_SL == 0
                                                    && qtiensudiung.TienSuGiaDinh_SL == 0)
                                                {
                                                    rep.ChuY.Value = par.First().ChuY + Environment.NewLine;
                                                }
                                                else
                                                {
                                                    rep.ChuY.Value = par.First().ChuY + Environment.NewLine + mb;
                                                }
                                            }
                                        }
                                        #endregion
                                    }
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                                else
                                {
                                    MessageBox.Show("bệnh nhân chưa nhập vào viện");
                                }
                            }
                        }
                    }
                }
            }
        }

        private void txtTienSuBT_EditValueChanged(object sender, EventArgs e)
        {
            if (_vaovien.Count > 0)
            {
                if (txtTienSuBT.Text != _vaovien.First().TienSuBT)
                    btnLuu.Enabled = true;
                else
                    btnLuu.Enabled = false;
            }
            else btnLuu.Enabled = true;
        }
        private void txtBenhLy_EditValueChanged(object sender, EventArgs e)
        {
            if (_vaovien.Count > 0)
            {
                if (txtBenhLy.Text != _vaovien.First().BenhLy)
                    btnLuu.Enabled = true;
                else
                    btnLuu.Enabled = false;
            }
            else btnLuu.Enabled = true;

        }

        private void txtTienSuGD_EditValueChanged(object sender, EventArgs e)
        {
            if (_vaovien.Count > 0)
            {
                if (txtTienSuGD.Text != _vaovien.First().TienSuGD)
                    btnLuu.Enabled = true;
                else
                    btnLuu.Enabled = false;
            }
            else btnLuu.Enabled = true;
        }

        private void txtLyDo_EditValueChanged(object sender, EventArgs e)
        {
            if (_vaovien.Count > 0)
            {
                if (txtLyDo.Text != _vaovien.First().LyDo)
                    btnLuu.Enabled = true;
                else
                    btnLuu.Enabled = false;
            }
            else btnLuu.Enabled = true;
        }

        private void btnLuu_EnabledChanged(object sender, EventArgs e)
        {
            if (_vaovien.Count > 0)
            {
                if (btnLuu.Enabled == true)
                    btnInPhieu.Enabled = false;
                else
                    btnInPhieu.Enabled = true;
            }
            else
            {
                if (btnLuu.Enabled == true)
                    btnInPhieu.Enabled = false;
                else
                    btnInPhieu.Enabled = true;
            }
            // else btnLuu.Enabled = true;
        }


        private void txtMach_EditValueChanged(object sender, EventArgs e)
        {
            if (_vaovien.Count > 0)
            {
                if (txtMach.Text != _vaovien.First().Mach)
                    btnLuu.Enabled = true;
                else
                    btnLuu.Enabled = false;
            }
            else { btnLuu.Enabled = true; }
        }

        private void txtKhamTThan_EditValueChanged(object sender, EventArgs e)
        {
            if (_vaovien.Count > 0)
            {
                if (txtKhamTThan.Text != _vaovien.First().KhamTThan)
                    btnLuu.Enabled = true;
                else
                    btnLuu.Enabled = false;
            }
            else btnLuu.Enabled = true;
        }


        private void txtKhamBPhan_EditValueChanged(object sender, EventArgs e)
        {
            if (_vaovien.Count > 0)
            {
                if (txtKhamBPhan.Text != _vaovien.First().KhamBPhan)
                    btnLuu.Enabled = true;
                else
                    btnLuu.Enabled = false;
            }
            else btnLuu.Enabled = true;
        }

        private void txtkQLamSang_EditValueChanged(object sender, EventArgs e)
        {
            if (_vaovien.Count > 0)
            {
                if (txtkQLamSang.Text != _vaovien.First().kQLamSang)
                    btnLuu.Enabled = true;
                else
                    btnLuu.Enabled = false;
            }
            else btnLuu.Enabled = true;
        }

        private void txtTenICD_EditValueChanged(object sender, EventArgs e)
        {
            if (_vaovien.Count > 0)
            {
                if (txtTenICD.Text != _vaovien.First().ChanDoan)
                    btnLuu.Enabled = true;
                else
                    btnLuu.Enabled = false;
            }
            else btnLuu.Enabled = true;
        }

        private void txtDaXuLy_EditValueChanged(object sender, EventArgs e)
        {
            if (_vaovien.Count > 0)
            {
                if (txtDaXuLy.Text != _vaovien.First().DaXuLy)
                    btnLuu.Enabled = true;
                else
                    btnLuu.Enabled = false;
            }
            else btnLuu.Enabled = true;
        }

        //private void txtTenKP_EditValueChanged(object sender, EventArgs e)
        //{
        //    if (_kp.Count > 0)
        //    {
        //        if (lupKh.Text != _vaovien.First())
        //            btnLuu.Enabled = true;
        //        else
        //            btnLuu.Enabled = false;
        //    }
        //    else btnLuu.Enabled = true;
        //}

        private void txtChuY_EditValueChanged(object sender, EventArgs e)
        {
            if (_vaovien.Count > 0)
            {
                if (txtChuY.Text != _vaovien.First().ChuY)
                    btnLuu.Enabled = true;
                else
                    btnLuu.Enabled = false;
            }
            btnLuu.Enabled = true;
        }


        private void lupKhoa_EditValueChanged(object sender, EventArgs e)
        {
            if (_kp.Count > 0)
            {
                if (xtraTabControl2.SelectedTabPageIndex == 0)
                {
                    if (lupKhoaDT.EditValue != null && Convert.ToInt32(lupKhoaDT.EditValue) != _kp.First().MaKP)
                        btnLuu.Enabled = true;
                    else
                        btnLuu.Enabled = false;
                }
                else
                {
                    if (lupKhoaDT1.EditValue != null && Convert.ToInt32(lupKhoaDT1.EditValue) != _kp.First().MaKP)
                        btnLuu.Enabled = true;
                    else
                        btnLuu.Enabled = false;
                }
            }
            else btnLuu.Enabled = true;
            var kt = DataContect.VaoViens.Where(p => p.MaBNhan == _mabn).ToList();

            if (kt.Count > 0)
            {
                int idkb = kt.First().IDKB;
                var makp = DataContect.BNKBs.Where(p => p.IDKB == idkb).ToList();
                if (makp.Count > 0 && makp.First().MaKP == _makp)
                    btnXoa.Enabled = true;
            }
            else
                btnXoa.Enabled = false;
            if (kt.Count == 0)
            {
                setSoBA();
                if (DungChung.Bien.PP_SoVV != 0)
                    setSoVV();
            }
        }

        private void txtNhietDo_EditValueChanged(object sender, EventArgs e)
        {

            if (_vaovien.Count > 0)
            {
                if (txtNhietDo.Text != _vaovien.First().NhietDo)
                    btnLuu.Enabled = true;
                else
                    btnLuu.Enabled = false;
            }
            else btnLuu.Enabled = true;
        }

        private void txtHuyetAp_EditValueChanged(object sender, EventArgs e)
        {
            if (_vaovien.Count > 0)
            {
                if (txtHuyetAp.Text != _vaovien.First().HuyetAp)
                    btnLuu.Enabled = true;
                else
                    btnLuu.Enabled = false;
            }
            else btnLuu.Enabled = true;
        }

        private void txtNhipTho_EditValueChanged(object sender, EventArgs e)
        {
            if (_vaovien.Count > 0)
            {
                if (txtNhipTho.Text != _vaovien.First().NhipTho)
                    btnLuu.Enabled = true;
                else
                    btnLuu.Enabled = false;
            }
            else btnLuu.Enabled = true;
        }

        private void txtCanNang_EditValueChanged(object sender, EventArgs e)
        {
            if (_vaovien.Count > 0)
            {
                if (txtCanNang.Text != _vaovien.First().CanNang)
                    btnLuu.Enabled = true;
                else
                    btnLuu.Enabled = false;
            }
            else btnLuu.Enabled = true;

            if (txtCanNang.EditValue != null && !string.IsNullOrWhiteSpace(txtCanNang.EditValue.ToString()) && txtChieuCao.EditValue != null && !string.IsNullOrWhiteSpace(txtChieuCao.EditValue.ToString()))
            {
                //string txtCannang = txtCanNang.EditValue.ToString();
                string txtCannang = txtCanNang.EditValue.ToString().Replace(".", ",");
                var canNang = double.Parse(txtCannang);
                var chieuCao = double.Parse(txtChieuCao.EditValue.ToString());
                double bmi = 0;
                string ketqua = "";
                DungChung.Ham.CalculateBMI(canNang, chieuCao, ref bmi, ref ketqua);
                txtBMI.Text = string.Format("{0:0.0}", bmi);
                lblChiSo.Text = ketqua;
            }
        }

        private void txtGTinh_EditValueChanged(object sender, EventArgs e)
        {

        }
        bool _cothexoa = true;
        bool _vvnt = false;
        private void btnXoa_Click(object sender, EventArgs e)
        {

            if (_mabn > 0)
            {
                var ktrv = DataContect.RaViens.Where(p => p.MaBNhan == _mabn).ToList();
                if (ktrv.Count <= 0)
                {
                    var _bnhans = DataContect.BenhNhans.Where(p => p.MaBNhan == _mabn).ToList();
                    var bnkbs = DataContect.BNKBs.Where(p => p.MaBNhan == _mabn).OrderByDescending(p => p.NgayKham).ToList();
                    _vaovien = DataContect.VaoViens.Where(p => p.MaBNhan == _mabn).ToList();
                    if (_vaovien.Count > 0)
                    {
                        int _makp = 0; int _makpKhamBenh = 0; // mã khoa phòng khám bệnh cho bệnh nhân vào viện
                        _makp = _vaovien.First().MaKP == null ? 0 : _vaovien.First().MaKP.Value; ;
                        var ktdt = DataContect.BNKBs.Where(p => p.MaKPdt == _makp && p.PhuongAn == 1 && p.MaBNhan == _mabn).ToList();
                        int _makpdt = 0;
                        if (ktdt.Count > 0)
                        {
                            _makpKhamBenh = ktdt.First().MaKP ?? 0;
                            _makpdt = ktdt.First().MaKPdt ?? 0;
                            var ktrakb = DataContect.BNKBs.Where(p => p.MaKP == _makpdt && p.MaBNhan == _mabn).ToList();
                            //if (ktrakb.Count > 0)
                            //{
                            //    _cothexoa = false;
                            //    MessageBox.Show("Bệnh nhân đã có chẩn đoán tại khoa điều trị, không thể xóa ");
                            //}
                            if (DungChung.Bien.MaKP != ktdt.First().MaKP)
                            {
                                //if (DungChung.Bien.CapDo != 9)
                                if (DungChung.Bien.PLoaiKP != DungChung.Bien.st_PhanLoaiKP.Admin)
                                {
                                    _cothexoa = false;
                                    MessageBox.Show("Khoa|Phòng của bạn không khớp với khoa phòng cho vào viện, bạn không thể xóa ");

                                }
                            }
                            else
                            {

                            }
                        }
                        var ktdthuoc = DataContect.DThuocs.Where(p => p.PLDV == 1).Where(p => p.MaBNhan == _mabn && p.MaKP == _makpdt).ToList();
                        if (_cothexoa && ktdthuoc.Count > 0)
                        {
                            MessageBox.Show("Bệnh nhân đã được kê đơn tại khoa điều trị, bạn không thể xóa");
                            _cothexoa = false;
                        }
                        if (_cothexoa)
                        {
                            int id = _vaovien.First().idVaoVien;


                            DialogResult _reuslt = MessageBox.Show("Bạn muốn xóa khám bệnh vào viện của BN: " + txtTenBNhan.Text, "Xóa khám bệnh vv", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (_reuslt == DialogResult.Yes)
                            {
                                ttdong = 1;
                                var xoa = DataContect.VaoViens.Single(p => p.idVaoVien == id);
                                int noingoaitru = -1;
                                noingoaitru = radNoiNgoaiTru.SelectedIndex;

                                #region update SoBA, soVV
                                if (DungChung.Bien.MaBV != "01071" || noingoaitru != 1)
                                {
                                    if ((DungChung.Bien.PP_SoVV == 1 || DungChung.Bien.PP_SoVV == 2))
                                    {
                                        int rs, sovaovien = 0, makpvv = 0;
                                        if (xtraTabControl2.SelectedTabPageIndex == 0)
                                        {
                                            if (DungChung.Bien.PP_SoVV == 1 && lupKhoaDT.EditValue != null && lupKhoaDT.EditValue.ToString() != "")
                                                makpvv = Convert.ToInt32(lupKhoaDT.EditValue);
                                        }
                                        else
                                        {
                                            if (DungChung.Bien.PP_SoVV == 1 && lupKhoaDT1.EditValue != null && lupKhoaDT1.EditValue.ToString() != "")
                                                makpvv = Convert.ToInt32(lupKhoaDT1.EditValue);
                                        }
                                        //if (Int32.TryParse(txtSoVV.Text, out rs))
                                        //{
                                        //    sovaovien = Convert.ToInt32(txtSoVV.Text);
                                        //}
                                        //   DungChung.Ham.SetStatusSoPL(makpvv, sovaovien, 2, -1);
                                        DungChung.Ham.UpdateHSHuy(_mabn, makpvv, txtSoVV.Text, 2, noingoaitru);

                                    }
                                    if (DungChung.Bien.PP_SoBA == 1 || DungChung.Bien.PP_SoBA == 2)
                                    {
                                        int rs, soba = 0, makpvv = 0;
                                        if (xtraTabControl2.SelectedTabPageIndex == 0)
                                        {
                                            if (DungChung.Bien.PP_SoBA == 1 && lupKhoaDT.EditValue != null && lupKhoaDT.EditValue.ToString() != "")
                                                makpvv = Convert.ToInt32(lupKhoaDT.EditValue);
                                        }
                                        else
                                        {
                                            if (DungChung.Bien.PP_SoBA == 1 && lupKhoaDT1.EditValue != null && lupKhoaDT1.EditValue.ToString() != "")
                                                makpvv = Convert.ToInt32(lupKhoaDT1.EditValue);
                                        }
                                        if (Int32.TryParse(txtSoBA.Text, out rs))
                                        {
                                            soba = Convert.ToInt32(txtSoBA.Text);
                                        }
                                        //  DungChung.Ham.SetStatusSoPL(makpvv, soba, 4, -1);
                                        DungChung.Ham.UpdateHSHuy(_mabn, makpvv, txtSoBA.Text, 4, noingoaitru);
                                    }
                                    //update benhnhan, bnkb
                                    if (_bnhans.Count() > 0 && bnkbs.Count() > 0)
                                    {
                                        var bnhan = _bnhans.First();
                                        bnhan.MaKP = _makpKhamBenh;

                                        var bnkb = bnkbs.First();
                                        bnkb.PhuongAn = 4;
                                        bnkb.MaKPdt = 0;
                                    }
                                }
                                #endregion

                                DataContect.VaoViens.Remove(xoa);

                                if (DataContect.SaveChanges() >= 0)
                                {
                                    refreshText();
                                    var bnkb = DataContect.BNKBs.Where(p => p.MaBNhan == _mabn).OrderBy(p => p.IDKB).ToList();
                                    foreach (var item in bnkb)
                                    {
                                        DungChung.Ham.Update_Delete_CongKham(_mabn, item.IDKB, false, dtNgayVao.DateTime);
                                    }
                                    foreach (var item in bnkb)
                                    {
                                        DungChung.Ham.Update_Delete_CongKham(_mabn, item.IDKB, true, dtNgayVao.DateTime);
                                    }
                                    if (bnkb.Count > 0)
                                    {
                                        var idkb = bnkb.Where(p => p.PhuongAn == 1).Select(p => p.IDKB).FirstOrDefault();
                                        var idkb_vvnt = bnkb.Where(p => p.PhuongAn == 4 && p.MaKPdt != 0).Select(p => p.IDKB).FirstOrDefault();
                                        if (idkb_vvnt > 0)
                                            _vvnt = true;
                                        if (idkb > 0 || _vvnt)
                                        {
                                            var sua = DataContect.BNKBs.Where(p => p.IDKB == idkb || p.IDKB == idkb_vvnt).FirstOrDefault();
                                            if (sua != null)
                                            {
                                                sua.PhuongAn = 4;
                                                sua.MaKPdt = 0;
                                                sua.NgayNghi = null;
                                                DataContect.SaveChanges();
                                            }
                                        }

                                        var DBien = DataContect.DienBiens.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                                        if(DBien != null && DungChung.Bien.MaBV == "24012" && _vvnt)
                                        {
                                            //DungChung.Ham._setMaKP_BenhNhan_24012(DataContect, _mabn, _makpKhamBenh, 0, true);
                                        }
                                        else
                                        {
                                            DungChung.Ham._setMaKP_BenhNhan(DataContect, _mabn, _makpKhamBenh, 0);
                                        }
                                        int makpmoi = 0;
                                        makpmoi = bnkb.First().MaKP == null ? 0 : bnkb.First().MaKP.Value;
                                        
                                        CapNhatSangGiaTT15(_mabn);

                                    }

                                    if (DungChung.Bien.MaBV == "30007")
                                    {
                                        var qDiUng = DataContect.TienSuDiUngs.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                                        if (qDiUng != null)
                                        {
                                            DataContect.TienSuDiUngs.Remove(qDiUng);
                                            DataContect.SaveChanges();
                                        }
                                    }

                                }
                                TTboXung _ttbx = DataContect.TTboXungs.Where(p => p.MaBNhan == _mabn).Select(p => p).FirstOrDefault();
                                _ttbx.Mach_NDo_HAp = "";
                                _ttbx.CanNang_ChieuCao = "";
                                DataContect.SaveChanges();
                                this.Dispose();
                            }
                        }
                        else
                        {
                            if (xtraTabControl2.SelectedTabPageIndex == 0)
                                MessageBox.Show("Bệnh nhân đã được: " + lupKhoaDT.Text + " tiếp nhận, bạn không thể xóa");
                            else
                                MessageBox.Show("Bệnh nhân đã được: " + lupKhoaDT1.Text + " tiếp nhận, bạn không thể xóa");
                        }
                    }
                    else
                    {
                        MessageBox.Show("BN không có khám bệnh vào viện để xóa!");
                    }
                }
                else
                {
                    MessageBox.Show("BN đã ra viện bạn không được xóa!");
                }
            }
            else
            {
                MessageBox.Show("Không có BN để xóa!");
            }
        }

        private void CapNhatSangGiaTT15(int _mabn)
        {
            #region cập nhật lại giá tt15 - - dung 14.12.2018
            var benhnhan = DataContect.BenhNhans.Single(p => p.MaBNhan == _mabn);
            var qdichvu = DataContect.DichVus.Where(p => p.PLoai == 2).ToList();
            if (benhnhan.NNhap != null && benhnhan.DTuong == "BHYT" && benhnhan.NNhap.Value < DungChung.Bien.ngayGiaMoiTT39)
            {
                string ms = "";
                List<DSDichVuGiaMoi> ldsDv = new List<DSDichVuGiaMoi>();
                var qcls = (from cls in DataContect.CLS.Where(p => p.MaBNhan == _mabn)
                            join cd in DataContect.ChiDinhs.Where(p => p.TrongBH != null && p.TrongBH == 1) on cls.IdCLS equals cd.IdCLS
                            select new { cls.IdCLS, cls.NgayThang, cd.MaDV, cd.DonGia, cd.IDCD }).ToList();

                foreach (var a in qcls)
                {
                    var qdv = qdichvu.Where(p => p.MaDV == a.MaDV).FirstOrDefault();
                    if (qdv != null)
                    {
                        if (a.DonGia != qdv.DonGiaTT15)
                        {
                            DSDichVuGiaMoi moi = new DSDichVuGiaMoi();
                            moi.MaDV = a.MaDV ?? 0;
                            moi.TenDV = qdv.TenDV;
                            moi.IDCD = a.IDCD;
                            moi.GiaCu = qdv.DonGiaTT15;
                            moi.GiaMoi = qdv.DonGiaTT39;
                            ldsDv.Add(moi);
                            ChiDinh chidinhUp = DataContect.ChiDinhs.Single(p => p.IDCD == a.IDCD);
                            chidinhUp.DonGia = qdv.DonGiaTT15;
                            DataContect.SaveChanges();
                            ms = ms + qdv.MaDV + "-" + qdv.TenDV + " , ";
                        }
                    }
                }


                List<DSDichVuGiaMoi> ldsDv2 = new List<DSDichVuGiaMoi>();
                var qdthuoc = (from dt in DataContect.DThuocs.Where(p => p.MaBNhan == _mabn)
                               join dtct in DataContect.DThuoccts.Where(p => p.TrongBH != null && p.TrongBH == 1) on dt.IDDon equals dtct.IDDon
                               join dichvu in DataContect.DichVus.Where(p => p.PLoai == 2) on dtct.MaDV equals dichvu.MaDV
                               select new { dt.NgayKe, dt.IDDon, dtct.IDDonct, dtct.MaDV, dtct.DonGia, dtct.IDCD, dtct.SoLuong, dtct.TyLeTT }).ToList();

                foreach (var a in qdthuoc)
                {
                    var qdv = qdichvu.Where(p => p.MaDV == a.MaDV).FirstOrDefault();
                    if (qdv != null)
                    {
                        if (a.DonGia != qdv.DonGiaTT15)
                        {
                            DSDichVuGiaMoi moi = new DSDichVuGiaMoi();
                            moi.MaDV = a.MaDV ?? 0;
                            moi.IDCD = a.IDDonct;
                            moi.GiaCu = qdv.DonGiaTT15;
                            moi.GiaMoi = qdv.DonGiaTT39;
                            moi.TenDV = qdv.TenDV;
                            ldsDv2.Add(moi);

                            DThuocct dtctUp = DataContect.DThuoccts.Single(p => p.IDDonct == a.IDDonct);
                            dtctUp.DonGia = qdv.DonGiaTT15;
                            dtctUp.ThanhTien = Math.Round(qdv.DonGiaTT15 * a.SoLuong * a.TyLeTT / 100, 5);
                            DataContect.SaveChanges();
                            if (a.IDCD == null)
                            {
                                ms = ms + qdv.MaDV + "-" + qdv.TenDV + " , ";
                            }
                            else
                            {
                                if (ldsDv.Count == 0 || ldsDv.Where(p => p.IDCD == a.IDCD).Count() == 0)
                                {
                                    ms = ms + qdv.MaDV + "-" + qdv.TenDV + " , ";
                                }
                            }


                        }
                    }
                }
                if (ldsDv.Count > 0)
                    MessageBox.Show("Các dịch vụ: " + ms + " đã được cập nhật về giá cũ");
            }
            #endregion
        }


        private void frmKBVaoVien_FormClosing(object sender, FormClosingEventArgs e)
        {
            //* uskb._luutudong = _luutd;
            /*
               0. Về nhà
                1. Vào viện
               2. Chuyển viện
               3. Chuyển phòng khám
               4. Đang khám
             */
            var ktkb = DataContect.VaoViens.Where(p => p.MaBNhan == _mabn).ToList();
            if (ktkb.Count <= 0)
            {
                if (ttdong != 1)
                {
                    DialogResult _result = MessageBox.Show("Bạn chưa lưu khám bệnh vào viện, bạn có muốn lưu không?", "hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_result == DialogResult.Yes)
                    {
                        btnLuu_Click(sender, e);
                        e.Cancel = true;
                    }
                    else
                    {
                        var bnkb = DataContect.BNKBs.Where(p => p.MaBNhan == _mabn).OrderByDescending(p => p.IDKB).ToList();
                        if (bnkb.Count > 0 && bnkb.First().PhuongAn == 1)
                        {
                            int id = bnkb.First().IDKB;
                            var sua = DataContect.BNKBs.Single(p => p.IDKB == id);
                            sua.PhuongAn = 4;
                            sua.MaKPdt = null;
                            sua.NgayNghi = null;
                            var bn = DataContect.BenhNhans.Single(p => p.MaBNhan == _mabn);
                            bn.NoiTru = 0;
                            bn.MaKP = sua.MaKP;
                            DataContect.SaveChanges();

                        }
                    }
                }
            }
        }

        private void txtCK_EditValueChanged(object sender, EventArgs e)
        {
            //if (txtCK.EditValue == "Tai Mũi Họng")
            //{
            //    groupControl1.Visible = false;
            //    panelControl2.Visible = true;
            //    txtGD.Visible = false;
            //    txtBCK.Visible = true;
            //}
            //else
            //{
            //    groupControl1.Visible = true;
            //    panelControl2.Visible = false;
            //    txtGD.Visible = true;
            //    txtBCK.Visible = false;
            //}
        }

        private int danhSoVaoVien(int ngay)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int sovv = 0;
            DateTime ngayBatDau = DateTime.Now;
            if (ngay == DateTime.Now.Day) // Trường hợp ngày = ngày hiện tại
            {
                var q = data.VaoViens.Where(p => p.NgayVao != null).Where(p => p.NgayVao.Value.Month == DateTime.Now.Month && p.NgayVao.Value.Year == DateTime.Now.Year && p.NgayVao.Value.Day == DateTime.Now.Day).ToList();
                if (q.Count == 0)
                {
                    sovv = 1;
                }
                else
                {
                    try
                    {
                        sovv = Convert.ToInt32(q.Max(p => p.SoVV)) + 1;
                    }
                    catch (Exception)
                    {
                        sovv = 1;
                    }
                }
            }
            else
            {
                if (ngay < DateTime.Now.Day) // Trường hợp ngày < ngày hiện tại
                {
                    ngayBatDau = new DateTime(DateTime.Now.Year, DateTime.Now.Month, ngay);
                }
                if (ngay > DateTime.Now.Day) // Trường hợp ngày > ngày hiện tại
                {
                    if (DateTime.Now.Month > 1)
                        ngayBatDau = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, ngay);
                    else
                        ngayBatDau = new DateTime(DateTime.Now.Year - 1, 12, ngay);
                }
                DateTime ngaytu = DungChung.Ham.NgayTu(ngayBatDau);
                DateTime ngayden = DungChung.Ham.NgayDen(ngayBatDau.AddMonths(1).AddDays(-1));
                var q = data.VaoViens.Where(p => p.NgayVao >= ngaytu && p.NgayVao <= ngayden).ToList();
                if (q.Count == 0)
                {
                    sovv = 1;
                }
                else
                {
                    List<int> _lSoVV = new List<int>();
                    foreach (var item in q)
                    {
                        int so = 0;
                        int.TryParse(item.SoVV, out so);
                        _lSoVV.Add(so);
                    }
                    sovv = _lSoVV.Max() + 1;
                }
            }

            return sovv;
        }



        private void txtCKmt_EditValueChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
        }

        private void txtNAmp_EditValueChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {

        }

        private void txtKKmp_EditValueChanged(object sender, EventArgs e)
        {
            //if (_vaovien.Count > 0)
            //{
            //    if (txtKKmp.Text != _vaovien.First().CanNang.ToString())
            //        btnLuu.Enabled = true;
            //    else
            //        btnLuu.Enabled = false;
            //}
            //else
            btnLuu.Enabled = true;
        }

        private void txtKKmt_EditValueChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
        }

        private void txtCKmp_EditValueChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
        }

        private void txtNAmt_EditValueChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
        }

        private void dtNgayVao_EditValueChanged(object sender, EventArgs e)
        {

        }
        private void refreshText()
        {
            txtBenhLy.Text = "";
            txtBenhLy1.Text = "";
            txtTienSuBT.Text = "";
            txtTienSuBT1.Text = "";
            txtTienSuGD.Text = "";
            txtTienSuGD1.Text = "";
            txtKKmp.Text = "";
            txtKKmt.Text = "";
            txtCKmp.Text = "";
            txtCKmt.Text = "";
            txtNAmp.Text = "";
            ckDiUng1.Text = "";
            txtKhamTThan.Text = "";
            txtKhamBPhan.Text = "";
            txtkQLamSang.Text = "";
            txtDaXuLy.Text = "";
            txtKhamTThan1.Text = "";
            txtKhamBPhan1.Text = "";
            txtkQLamSang1.Text = "";
            txtDaXuLy1.Text = "";
            txtChuY.Text = "";
            txtTai.Text = "";
            txtMui.Text = "";
            txtHong.Text = "";
            txtMach.Text = "";
            txtNhietDo.Text = "";
            txtHuyetAp.Text = "";
            txtNhipTho.Text = "";
            txtCanNang.Text = "";
        }

        private void lupKhoa_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (_sua)
            {
                if (!string.IsNullOrEmpty(lupKhoaDT.Text) || !string.IsNullOrEmpty(lupKhoaDT1.Text))
                {
                    var ktdt = DataContect.DThuocs.Where(p => p.PLDV == 1).Where(p => p.MaBNhan == _mabn).ToList();
                    if (ktdt.Count > 1)
                    {
                        e.Cancel = true;
                        MessageBox.Show("Bệnh nhân đã được kê đơn tại khoa điều trị, bạn không thể sửa");
                    }
                }
            }
        }

        private void radNoiNgoaiTru_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_vaovien.Count > 0)
            {
                btnLuu.Enabled = true;
            }
            else
            {
                if (DungChung.Bien.MaBV == "01830")
                {
                    if (radNoiNgoaiTru.SelectedIndex == 0)
                    {
                        txtSoVV.Text = "";
                        txtSoBA.Text = "";
                    }
                    else
                    {
                        setSoVV();
                        setSoBA();
                    }
                }
                else if (DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "20001")
                {
                    setSoVV();
                    if (DungChung.Bien.MaBV == "27021")
                        setSoBA();
                }
                else if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                    setSoVV();


            }


        }

        private void txtBenhLy1_EditValueChanged(object sender, EventArgs e)
        {
            if (_vaovien.Count > 0)
            {
                if (txtBenhLy1.Text != _vaovien.First().BenhLy)
                    btnLuu.Enabled = true;
                else
                    btnLuu.Enabled = false;
            }
            else btnLuu.Enabled = true;
        }

        private void txtTienSuBT1_EditValueChanged(object sender, EventArgs e)
        {
            if (_vaovien.Count > 0)
            {
                if (txtTienSuBT1.Text != _vaovien.First().TienSuBT)
                    btnLuu.Enabled = true;
                else
                    btnLuu.Enabled = false;
            }
            else btnLuu.Enabled = true;
        }

        private void txtTienSuGD1_EditValueChanged(object sender, EventArgs e)
        {
            if (_vaovien.Count > 0)
            {
                if (txtTienSuGD1.Text != _vaovien.First().TienSuGD)
                    btnLuu.Enabled = true;
                else
                    btnLuu.Enabled = false;
            }
            else btnLuu.Enabled = true;
        }

        private void txtKhamTThan1_EditValueChanged(object sender, EventArgs e)
        {
            if (_vaovien.Count > 0)
            {
                if (txtKhamTThan1.Text != _vaovien.First().KhamTThan)
                    btnLuu.Enabled = true;
                else
                    btnLuu.Enabled = false;
            }
            else btnLuu.Enabled = true;
        }

        private void txtKhamBPhan1_EditValueChanged(object sender, EventArgs e)
        {
            if (_vaovien.Count > 0)
            {
                if (txtKhamBPhan1.Text != _vaovien.First().KhamBPhan)
                    btnLuu.Enabled = true;
                else
                    btnLuu.Enabled = false;
            }
            else btnLuu.Enabled = true;
        }

        private void txtkQLamSang1_EditValueChanged(object sender, EventArgs e)
        {
            if (_vaovien.Count > 0)
            {
                if (txtkQLamSang1.Text != _vaovien.First().kQLamSang)
                    btnLuu.Enabled = true;
                else
                    btnLuu.Enabled = false;
            }
            else btnLuu.Enabled = true;
        }

        private void txtTenICD1_EditValueChanged(object sender, EventArgs e)
        {
            if (_vaovien.Count > 0)
            {
                if (txtTenICD1.Text != _vaovien.First().ChanDoan)
                    btnLuu.Enabled = true;
                else
                    btnLuu.Enabled = false;
            }
            else btnLuu.Enabled = true;
        }

        private void txtDaXuLy1_EditValueChanged(object sender, EventArgs e)
        {
            if (_vaovien.Count > 0)
            {
                if (txtDaXuLy1.Text != _vaovien.First().DaXuLy)
                    btnLuu.Enabled = true;
                else
                    btnLuu.Enabled = false;
            }
            else btnLuu.Enabled = true;
        }

        private void txtDiUng11_EditValueChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
        }

        private void txtDiUng12_EditValueChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
        }

        private void txtDiUng13_EditValueChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
        }

        private void txtDiUng21_EditValueChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
        }

        private void txtDiUng22_EditValueChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
        }

        private void txtDiUng23_EditValueChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
        }

        private void txtDiUng31_EditValueChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
        }

        private void txtDiUng32_EditValueChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
        }

        private void txtDiUng33_EditValueChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
        }

        private void txtDiUng41_EditValueChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
        }

        private void txtDiUng42_EditValueChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
        }

        private void txtDiUng43_EditValueChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
        }

        private void txtDiUng51_EditValueChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
        }

        private void txtDiUng52_EditValueChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
        }

        private void txtDiUng53_EditValueChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
        }

        private void txtDiUng61_EditValueChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
        }

        private void txtDiUng62_EditValueChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
        }

        private void txtDiUng63_EditValueChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
        }

        private void txtTenBNhan_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void groupControl3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtChieuCao_EditValueChanged(object sender, EventArgs e)
        {
            if (txtCanNang.EditValue != null && !string.IsNullOrWhiteSpace(txtCanNang.EditValue.ToString()) && txtChieuCao.EditValue != null && !string.IsNullOrWhiteSpace(txtChieuCao.EditValue.ToString()))
            {
                double canNang = 0;
                double chieuCao = 0;

                string txtCao = txtChieuCao.EditValue.ToString().Replace(".", ",");
                string txtNang = txtCanNang.EditValue.ToString().Replace(".", ",");
                if (double.TryParse(txtNang, out canNang) && double.TryParse(txtCao, out chieuCao))
                {
                    double bmi = 0;
                    string ketqua = "";
                    DungChung.Ham.CalculateBMI(canNang, chieuCao, ref bmi, ref ketqua);
                    txtBMI.Text = string.Format("{0:0.0}", bmi);
                    lblChiSo.Text = ketqua;
                }
            }
        }




    }
}