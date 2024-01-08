using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using Newtonsoft.Json.Linq;
using QLBV.ChucNang;
using QLBV.DungChung;
using QLBV.Forms.ViewPDF;
using QLBV.FormThamSo;
using QLBV.Models.Business.Invoices;
using QLBV.Utilities.Commons;
using QLBV_Database;
using QLBV_Database.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Transactions;
using System.Windows.Forms;

namespace QLBV.FormNhap
{
    public partial class usTamThu_TToan : DevExpress.XtraEditors.XtraUserControl
    {
        public usTamThu_TToan()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        //QLBVEntities _dataContext = EntityDbContext.DbContext;
        //QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
        List<TamUng> _litamung = new List<TamUng>();
        int _mabn = 0;
        string _muc = "";
        double _tienBH = 0;
        double _tienBN = 0;
        double _tienTThu = 0;
        int _pttt = 0;
        int _tuyen = 1;
        int idvp = 0;
        int TTLuutthu = 0;
        string _DTuong = "";
        int _HangBV = 4;
        List<BenhNhan> _lTKbn = new List<BenhNhan>();
        List<BenhNhan24272> _lTKbn_24272 = new List<BenhNhan24272>();
        DateTime _dttu = DungChung.Ham.NgayTu(System.DateTime.Now);
        DateTime _dtden = DungChung.Ham.NgayDen(System.DateTime.Now);
        int _makp = Bien.MaKP;
        private void EnableControl(bool T)
        {
            cbo_quyenTU.Enabled = T;
            cbo_soHD_TU.Enabled = T;
            dtNgayThu.Enabled = T;
            lupBPThu.Enabled = T;
            lupNguoiThu.Enabled = T;
            if (cboPLThu.SelectedIndex == 3)
            {
                txtSoTien.Enabled = false;
            }
            else
                txtSoTien.Enabled = T;
            _trangthai = T;
            txtSoTienBangChu.Enabled = T;
            txtNoiDung.Enabled = T;
            cboPLThu.Enabled = T;
            chkKetLuan.Enabled = T;
            btnMoi.Enabled = !T;
            btnSua.Enabled = !T;
            btnXoatthu.Enabled = !T;
            btnLuutthu.Enabled = T;
            if (Bien.MaBV == "24012")
            {
                cboPLThu.Properties.Items[7] = "Tạm Thu Phòng Nhu Cầu";
            }

        }
        private void EnableControlTC(bool T)
        {
            //dtNgayTC.Properties.ReadOnly = T;
            // lupBPTC.Properties.ReadOnly = T;
            lupCBTC.Properties.ReadOnly = T;
            radThuChi.Properties.ReadOnly = T;
            // txtNoiDungTC.Properties.ReadOnly = T;// dung sửa 14/05/2016 cho bệnh viện 30005
            txtSoTienTC.Properties.ReadOnly = T;
            txtBangChuTC.Properties.ReadOnly = T;
            txtBangChuCP.Properties.ReadOnly = T;
            txtSoTienCP.Properties.ReadOnly = T;
            cboKLKSK.Properties.ReadOnly = T;
            txtSoTo.Properties.ReadOnly = T;


        }
        int _HTTT = 99;
        private bool KTLuu()
        {
            if (dtNgayThu.DateTime.Day <= 0)
            {
                MessageBox.Show("Bạn chưa nhập ngày");
                dtNgayThu.Focus();
                return false;
            }
            if (lupNguoiThu.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn cán bộ thu");
                lupNguoiThu.Focus();
                return false;
            }
            if (lupBPThu.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn bộ phận");
                lupNguoiThu.Focus();
                return false;
            }
            //if (string.IsNullOrEmpty(txtSoTien.Text))
            //{
            //    MessageBox.Show("Bạn chưa nhập số tiền");
            //    txtSoTien.Focus();
            //    return false;
            //}
            if (cboPLThu.SelectedText == null)
            {
                MessageBox.Show("Bạn chưa chọn phân loại thu");
                cboPLThu.Focus();
                return false;
            }
            return true;
        }
        private bool KTLuuTC()
        {
            if (dtNgayTC.DateTime.Day <= 0)
            {
                MessageBox.Show("Bạn chưa nhập ngày");
                dtNgayTC.Focus();
                return false;
            }
            if (lupCBTC.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn cán bộ thu");
                lupCBTC.Focus();
                return false;
            }
            if (lupBPTC.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn bộ phận");
                lupBPTC.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtSoTienTC.Text))
            {
                MessageBox.Show("Bạn chưa nhập số tiền");
                txtSoTienTC.Focus();
                return false;
            }
            if (radThuChi.SelectedIndex != 1 && radThuChi.SelectedIndex != 0)
            {
                MessageBox.Show("Bạn chưa chọn phân loại thu chi");
                cboPLThu.Focus();
                return false;
            }
            return true;
        }
        void TimKiem()
        {
            _makp = 0;
            _dttu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
            _dtden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
            _lTKbn.Clear();
            if (lupTimMaKP.EditValue != null)
            {
                _makp = Convert.ToInt32(lupTimMaKP.EditValue);
            }
            string _tk = "";
            int ot;
            int _int_maBN = 0;
            if (!string.IsNullOrEmpty(txtTimKiem.Text) && txtTimKiem.Text != "Nhập tên|Mã số|Số thẻ BN")
            {
                _tk = txtTimKiem.Text.ToLower();

                if (Int32.TryParse(txtTimKiem.Text, out ot))
                    _int_maBN = Convert.ToInt32(txtTimKiem.Text);
            }
            int noitru = 0;
            noitru = cboNoitru.SelectedIndex;
            int IDDTBN = (cboDTuong.EditValue != null && cboDTuong.EditValue.ToString() != "") ? Convert.ToInt32(cboDTuong.EditValue) : 99;
            int HTTT = 99;
            if (_ldtbn.Where(p => p.IDDTBN == IDDTBN).ToList().Count > 0)
                HTTT = _ldtbn.Where(p => p.IDDTBN == IDDTBN).ToList().First().HTTT;
            //
            if (HTTT == 0 || HTTT == 1 || HTTT == 2 || HTTT == 99)
            {
                if (cboTimTT.SelectedIndex == 0)
                {
                    if (chkChiDinh.Checked)
                    {
                        _lTKbn = (from bn in _dataContext.BenhNhans.Where(p => p.NNhap >= _dttu && p.NNhap <= _dtden && p.Status != 3)//.Where(p => p.Status != 3)
                                  join cls in _dataContext.CLS on bn.MaBNhan equals cls.MaBNhan
                                  // where (bn.NNhap >= _dttu && bn.NNhap <= _dtden)
                                  select bn).ToList();
                        _lTKbn = (from bn in _lTKbn
                                  select bn).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).Distinct().ToList();
                        if (DungChung.Bien.MaBV == "24272")
                        {
                            _lTKbn_24272 = (from bn in _dataContext.BenhNhans.Where(p => p.NNhap >= _dttu && p.NNhap <= _dtden && p.Status != 3)//.Where(p => p.Status != 3)11
                                            join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan into results
                                            join cls in _dataContext.CLS on bn.MaBNhan equals cls.MaBNhan
                                            // where (bn.NNhap >= _dttu && bn.NNhap <= _dtden)
                                            from rs in results.DefaultIfEmpty()
                                            select new
                                            {
                                                bn,
                                                rs
                                            }).ToList().Select(x => new BenhNhan24272 
                                            {
                                                MaKP = x.bn.MaKP,
                                                MaBNhan = x.bn.MaBNhan,
                                                TenBNhan = x.bn.TenBNhan,
                                                Tuoi = x.bn.Tuoi,
                                                DChi = x.bn.DChi,
                                                SoTT = x.bn.SoTT,
                                                SThe = x.bn.SThe,
                                                Tuyen = x.bn.Tuyen,
                                                DTuong = x.bn.DTuong,
                                                NNhap = x.bn.NNhap.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                                GTinh = x.bn.GTinh,
                                                IDDTBN = x.bn.IDDTBN,
                                                NamSinh = x.bn.NamSinh,
                                                NgayNghi = x.rs == null ? "" : x.rs.NgayRa.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                                Status = x.bn.Status,
                                            }).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).Distinct().ToList();
                        }
                    }
                    else
                    {
                        _lTKbn = (from bn in _dataContext.BenhNhans//.Where(p => p.Status != 3)
                                  where (bn.NNhap >= _dttu && bn.NNhap <= _dtden && bn.Status != 3)
                                  select bn).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).ToList();
                        if (DungChung.Bien.MaBV == "24272")
                        {
                            _lTKbn_24272 = (from bn in _dataContext.BenhNhans//.Where(p => p.Status != 3)
                                            join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan into results
                                            from rs in results.DefaultIfEmpty()
                                            where (bn.NNhap >= _dttu && bn.NNhap <= _dtden && bn.Status != 3)
                                            select new 
                                            {
                                                bn,
                                                rs
                                            }).ToList().Select(x => new BenhNhan24272 
                                            {
                                                MaKP = x.bn.MaKP,
                                                MaBNhan = x.bn.MaBNhan,
                                                TenBNhan = x.bn.TenBNhan,
                                                Tuoi = x.bn.Tuoi,
                                                DChi = x.bn.DChi,
                                                SoTT = x.bn.SoTT,
                                                SThe = x.bn.SThe,
                                                Tuyen = x.bn.Tuyen,
                                                DTuong = x.bn.DTuong,
                                                NNhap = x.bn.NNhap.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                                GTinh = x.bn.GTinh,
                                                IDDTBN = x.bn.IDDTBN,
                                                NamSinh = x.bn.NamSinh,
                                                NgayNghi = x.rs == null ? "" : x.rs.NgayRa.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                                Status = x.bn.Status,
                                            }).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).ToList();
                        }
                    }

                }
                else if (cboTimTT.SelectedIndex == 1)
                {
                    if (chkNgoaiH.Checked)
                    {
                        List<int> qmabn = (from tu in _dataContext.TamUngs.Where(p => p.PhanLoai == 1 || p.PhanLoai == 2) select tu.MaBNhan.Value).ToList();
                        int count = qmabn.Count;
                        _lTKbn = (from kb in _dataContext.VienPhis.Where(p => p.NgayTT >= _dttu && p.NgayTT <= _dtden && p.MaKP == _makp && p.NgoaiGio == 1)
                                  join bn in _dataContext.BenhNhans on kb.MaBNhan equals bn.MaBNhan
                                  where (count == 0 || !qmabn.Contains(kb.MaBNhan ?? 0))
                                  //   where !(from tu in _dataContext.TamUngs.Where(p => p.PhanLoai == 1 || p.PhanLoai == 2) select tu.MaBNhan).Contains(kb.MaBNhan)

                                  // where (kb.NgayTT >= _dttu && kb.NgayTT <= _dtden)
                                  select bn).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).ToList();
                        if (DungChung.Bien.MaBV == "24272")
                        {
                            _lTKbn_24272 = (from kb in _dataContext.VienPhis.Where(p => p.NgayTT >= _dttu && p.NgayTT <= _dtden && p.MaKP == _makp && p.NgoaiGio == 1)
                                            join bn in _dataContext.BenhNhans on kb.MaBNhan equals bn.MaBNhan
                                            join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                                            where (count == 0 || !qmabn.Contains(kb.MaBNhan ?? 0))
                                            //   where !(from tu in _dataContext.TamUngs.Where(p => p.PhanLoai == 1 || p.PhanLoai == 2) select tu.MaBNhan).Contains(kb.MaBNhan)
                                            // where (kb.NgayTT >= _dttu && kb.NgayTT <= _dtden)
                                            select new
                                            {
                                                bn,
                                                rv
                                            }).ToList().Select(x => new BenhNhan24272 
                                            {
                                                MaKP = x.bn.MaKP,
                                                MaBNhan = x.bn.MaBNhan,
                                                TenBNhan = x.bn.TenBNhan,
                                                Tuoi = x.bn.Tuoi,
                                                DChi = x.bn.DChi,
                                                SoTT = x.bn.SoTT,
                                                SThe = x.bn.SThe,
                                                Tuyen = x.bn.Tuyen,
                                                DTuong = x.bn.DTuong,
                                                NNhap = x.bn.NNhap.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                                GTinh = x.bn.GTinh,
                                                IDDTBN = x.bn.IDDTBN,
                                                NamSinh = x.bn.NamSinh,
                                                NgayNghi = x.rv.NgayRa.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                                Status = x.bn.Status,
                                            }).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).ToList();
                        }
                    }
                    else
                    {
                        if (chkChiDinh.Checked)
                        {
                            List<int> qmabn = (from tu in _dataContext.TamUngs.Where(p => p.PhanLoai == 1 || p.PhanLoai == 2) select tu.MaBNhan.Value).ToList();
                            int count = qmabn.Count;

                            _lTKbn = (from kb in _dataContext.VienPhis.Where(p => p.NgayTT >= _dttu && p.NgayTT <= _dtden)
                                      join bn in _dataContext.BenhNhans on kb.MaBNhan equals bn.MaBNhan
                                      join cls in _dataContext.CLS on bn.MaBNhan equals cls.MaBNhan
                                      where (count == 0 || !qmabn.Contains(kb.MaBNhan ?? 0))
                                      //  where (kb.NgayTT >= _dttu && kb.NgayTT <= _dtden)
                                      select bn).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).Distinct().ToList();
                            if (DungChung.Bien.MaBV == "24272")
                            {
                                _lTKbn_24272 = (from kb in _dataContext.VienPhis.Where(p => p.NgayTT >= _dttu && p.NgayTT <= _dtden)
                                                join bn in _dataContext.BenhNhans on kb.MaBNhan equals bn.MaBNhan
                                                join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                                                join cls in _dataContext.CLS on bn.MaBNhan equals cls.MaBNhan
                                                where (count == 0 || !qmabn.Contains(kb.MaBNhan ?? 0))
                                                //  where (kb.NgayTT >= _dttu && kb.NgayTT <= _dtden)
                                                select new
                                                {
                                                    bn,
                                                    rv
                                                }).ToList().Select(x => new BenhNhan24272 
                                                {
                                                    MaKP = x.bn.MaKP,
                                                    MaBNhan = x.bn.MaBNhan,
                                                    TenBNhan = x.bn.TenBNhan,
                                                    Tuoi = x.bn.Tuoi,
                                                    DChi = x.bn.DChi,
                                                    SoTT = x.bn.SoTT,
                                                    SThe = x.bn.SThe,
                                                    Tuyen = x.bn.Tuyen,
                                                    DTuong = x.bn.DTuong,
                                                    NNhap = x.bn.NNhap.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                                    GTinh = x.bn.GTinh,
                                                    IDDTBN = x.bn.IDDTBN,
                                                    NamSinh = x.bn.NamSinh,
                                                    NgayNghi = x.rv.NgayRa.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                                    Status = x.bn.Status,
                                                }).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).Distinct().ToList();
                            }
                        }
                        else
                        {
                            //

                            _lTKbn = (from bn in _dataContext.BenhNhans.Where(p => p.Status == 3)
                                      join kb in _dataContext.VienPhis on bn.MaBNhan equals kb.MaBNhan into kq
                                      from a in kq.DefaultIfEmpty()
                                      where (a != null ? (a.NgayTT >= _dttu && a.NgayTT <= _dtden) : (bn.NNhap >= _dttu && bn.NNhap <= _dtden))
                                      select bn).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).ToList();
                            if (DungChung.Bien.MaBV == "24272")
                            {
                                _lTKbn_24272 = (from bn in _dataContext.BenhNhans.Where(p => p.Status == 3)
                                                join kb in _dataContext.VienPhis on bn.MaBNhan equals kb.MaBNhan into kq
                                                join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                                                from a in kq.DefaultIfEmpty()
                                                where (a != null ? (a.NgayTT >= _dttu && a.NgayTT <= _dtden) : (bn.NNhap >= _dttu && bn.NNhap <= _dtden))
                                                select new
                                                {
                                                    bn,
                                                    rv
                                                }).ToList().Select(x => new BenhNhan24272 
                                                {
                                                    MaKP = x.bn.MaKP,
                                                    MaBNhan = x.bn.MaBNhan,
                                                    TenBNhan = x.bn.TenBNhan,
                                                    Tuoi = x.bn.Tuoi,
                                                    DChi = x.bn.DChi,
                                                    SoTT = x.bn.SoTT,
                                                    SThe = x.bn.SThe,
                                                    Tuyen = x.bn.Tuyen,
                                                    DTuong = x.bn.DTuong,
                                                    NNhap = x.bn.NNhap.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                                    GTinh = x.bn.GTinh,
                                                    IDDTBN = x.bn.IDDTBN,
                                                    NamSinh = x.bn.NamSinh,
                                                    NgayNghi = x.rv.NgayRa.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                                    Status = x.bn.Status,
                                                }).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).ToList();
                            }
                        }
                    }
                }
                else if (cboTimTT.SelectedIndex == 2)
                {
                    _lTKbn = (from bn in _dataContext.BenhNhans
                              where (bn.NNhap >= _dttu && bn.NNhap <= _dtden)
                              select bn).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).ToList();
                    if (DungChung.Bien.MaBV == "24272")
                    {
                        _lTKbn_24272 = (from bn in _dataContext.BenhNhans
                                        join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                                        where (bn.NNhap >= _dttu && bn.NNhap <= _dtden)
                                        select new
                                        {
                                            bn,
                                            rv
                                        }).ToList().Select(x => new BenhNhan24272 
                                        {
                                            MaKP = x.bn.MaKP,
                                            MaBNhan = x.bn.MaBNhan,
                                            TenBNhan = x.bn.TenBNhan,
                                            Tuoi = x.bn.Tuoi,
                                            DChi = x.bn.DChi,
                                            SoTT = x.bn.SoTT,
                                            SThe = x.bn.SThe,
                                            Tuyen = x.bn.Tuyen,
                                            DTuong = x.bn.DTuong,
                                            NNhap = x.bn.NNhap.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                            GTinh = x.bn.GTinh,
                                            IDDTBN = x.bn.IDDTBN,
                                            NamSinh = x.bn.NamSinh,
                                            NgayNghi = x.rv.NgayRa.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                            Status = x.bn.Status,
                                        }).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).ToList();
                    }
                }

                else if (cboTimTT.SelectedIndex == 3)
                {
                    _lTKbn = (from vp in _dataContext.VienPhis.Where(p => p.NgayTT >= _dttu && p.NgayTT <= _dtden).Where(p => p.NgayDuyet == null)
                              join bn in _dataContext.BenhNhans on vp.MaBNhan equals bn.MaBNhan
                              //join tu in _dataContext.TamUngs.Where(p => p.PhanLoai == 1 || p.PhanLoai == 2) on bn.MaBNhan equals tu.MaBNhan into kq
                              //from a in kq.DefaultIfEmpty()
                              //where a == null
                              // where (vp.NgayTT >= _dttu && vp.NgayTT <= _dtden)
                              select bn).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).ToList();
                    if (DungChung.Bien.MaBV == "24272")
                    {
                        _lTKbn_24272 = (from vp in _dataContext.VienPhis.Where(p => p.NgayTT >= _dttu && p.NgayTT <= _dtden).Where(p => p.NgayDuyet == null)
                                        join bn in _dataContext.BenhNhans on vp.MaBNhan equals bn.MaBNhan
                                        join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                                        //join tu in _dataContext.TamUngs.Where(p => p.PhanLoai == 1 || p.PhanLoai == 2) on bn.MaBNhan equals tu.MaBNhan into kq
                                        //from a in kq.DefaultIfEmpty()
                                        //where a == null
                                        // where (vp.NgayTT >= _dttu && vp.NgayTT <= _dtden)
                                        select new
                                        {
                                            bn,
                                            rv
                                        }).ToList().Select(x => new BenhNhan24272 
                                        {
                                            MaKP = x.bn.MaKP,
                                            MaBNhan = x.bn.MaBNhan,
                                            TenBNhan = x.bn.TenBNhan,
                                            Tuoi = x.bn.Tuoi,
                                            DChi = x.bn.DChi,
                                            SoTT = x.bn.SoTT,
                                            SThe = x.bn.SThe,
                                            Tuyen = x.bn.Tuyen,
                                            DTuong = x.bn.DTuong,
                                            NNhap = x.bn.NNhap.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                            GTinh = x.bn.GTinh,
                                            IDDTBN = x.bn.IDDTBN,
                                            NamSinh = x.bn.NamSinh,
                                            NgayNghi = x.rv.NgayRa.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                            Status = x.bn.Status,
                                        }).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).ToList();
                    }

                }
                else if (cboTimTT.SelectedIndex == 4)
                {
                    _lTKbn = (from kb in _dataContext.VienPhis.Where(p => p.NgayTT >= _dttu && p.NgayTT <= _dtden).Where(p => p.NgayDuyet != null)  //on bn.MaBNhan equals kb.MaBNhan
                              join bn in _dataContext.BenhNhans on kb.MaBNhan equals bn.MaBNhan
                              //join tt in _dataContext.TamUngs.Where(p => p.PhanLoai == 1 || p.PhanLoai == 2) on bn.MaBNhan equals tt.MaBNhan
                              //where (kb.NgayTT >= _dttu && kb.NgayTT <= _dtden)
                              select bn).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).ToList();
                    if (DungChung.Bien.MaBV == "24272")
                    {
                        _lTKbn_24272 = (from kb in _dataContext.VienPhis.Where(p => p.NgayTT >= _dttu && p.NgayTT <= _dtden).Where(p => p.NgayDuyet != null)  //on bn.MaBNhan equals kb.MaBNhan
                                        join bn in _dataContext.BenhNhans on kb.MaBNhan equals bn.MaBNhan
                                        join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                                        //join tt in _dataContext.TamUngs.Where(p => p.PhanLoai == 1 || p.PhanLoai == 2) on bn.MaBNhan equals tt.MaBNhan
                                        //where (kb.NgayTT >= _dttu && kb.NgayTT <= _dtden)
                                        select new
                                        {
                                            bn,
                                            rv
                                        }).ToList().Select(x => new BenhNhan24272 
                                        {
                                            MaKP = x.bn.MaKP,
                                            MaBNhan = x.bn.MaBNhan,
                                            TenBNhan = x.bn.TenBNhan,
                                            Tuoi = x.bn.Tuoi,
                                            DChi = x.bn.DChi,
                                            SoTT = x.bn.SoTT,
                                            SThe = x.bn.SThe,
                                            Tuyen = x.bn.Tuyen,
                                            DTuong = x.bn.DTuong,
                                            NNhap = x.bn.NNhap.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                            GTinh = x.bn.GTinh,
                                            IDDTBN = x.bn.IDDTBN,
                                            NamSinh = x.bn.NamSinh,
                                            NgayNghi = x.rv.NgayRa.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                            Status = x.bn.Status,
                                        }).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).ToList();
                    }
                }
                else if (cboTimTT.SelectedIndex == 5)
                {
                    DateTime _NgayTT1 = _dttu.AddMonths(-1);
                    DateTime _NgayTT2 = _dtden.AddMonths(1);
                    var _lbn = (from rv in _dataContext.RaViens.Where(p => p.NgayRa >= _dttu && p.NgayRa <= _dtden)
                                join bn in _dataContext.BenhNhans on rv.MaBNhan equals bn.MaBNhan
                                select bn).ToList();
                    var _lvp = _dataContext.VienPhis.Where(p => p.NgayTT >= _NgayTT1 && p.NgayTT <= _NgayTT2).Select(p => p.MaBNhan).ToList();

                    _lTKbn = (from bn in _lbn
                              join vp in _lvp on bn.MaBNhan equals vp into kq
                              from a in kq.DefaultIfEmpty()
                                  //    in _dataContext.BenhNhans
                                  //join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                                  //join vp in _dataContext.VienPhis on bn.MaBNhan equals vp.MaBNhan into kq
                                  //from a in kq.DefaultIfEmpty()
                              where a == null
                              //where (rv.NgayRa >= _dttu && rv.NgayRa <= _dtden)
                              select bn).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).ToList();
                    if (DungChung.Bien.MaBV == "24272")
                    {
                        _lTKbn_24272 = (from bn in _lbn
                                        join vp in _lvp on bn.MaBNhan equals vp into kq
                                        join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                                        from a in kq.DefaultIfEmpty()
                                            //    in _dataContext.BenhNhans
                                            //join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                                            //join vp in _dataContext.VienPhis on bn.MaBNhan equals vp.MaBNhan into kq
                                            //from a in kq.DefaultIfEmpty()
                                        where a == null
                                        //where (rv.NgayRa >= _dttu && rv.NgayRa <= _dtden)
                                        select new
                                        {
                                            bn,
                                            rv
                                        }).ToList().Select(x => new BenhNhan24272 
                                        {
                                            MaKP = x.bn.MaKP,
                                            MaBNhan = x.bn.MaBNhan,
                                            TenBNhan = x.bn.TenBNhan,
                                            Tuoi = x.bn.Tuoi,
                                            DChi = x.bn.DChi,
                                            SoTT = x.bn.SoTT,
                                            SThe = x.bn.SThe,
                                            Tuyen = x.bn.Tuyen,
                                            DTuong = x.bn.DTuong,
                                            NNhap = x.bn.NNhap.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                            GTinh = x.bn.GTinh,
                                            IDDTBN = x.bn.IDDTBN,
                                            NamSinh = x.bn.NamSinh,
                                            NgayNghi = x.rv.NgayRa.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                            Status = x.bn.Status,
                                        }).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).ToList();
                    }
                }
                else if (cboTimTT.SelectedIndex == 6)
                {
                    _lTKbn = (from kb in _dataContext.VienPhis.Where(p => p.NgayRa >= _dttu && p.NgayRa <= _dtden && p.Status == 0)
                              join bn in _dataContext.BenhNhans on kb.MaBNhan equals bn.MaBNhan
                              // where (kb.NgayTT >= _dttu && kb.NgayTT <= _dtden)
                              select bn).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).ToList();
                    if (DungChung.Bien.MaBV == "24272")
                    {
                        _lTKbn_24272 = (from kb in _dataContext.VienPhis.Where(p => p.NgayRa >= _dttu && p.NgayRa <= _dtden && p.Status == 0)
                                        join bn in _dataContext.BenhNhans on kb.MaBNhan equals bn.MaBNhan
                                        join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                                        // where (kb.NgayTT >= _dttu && kb.NgayTT <= _dtden)
                                        select new
                                        {
                                            bn,
                                            rv
                                        }).ToList().Select(x => new BenhNhan24272 
                                        {
                                            MaKP = x.bn.MaKP,
                                            MaBNhan = x.bn.MaBNhan,
                                            TenBNhan = x.bn.TenBNhan,
                                            Tuoi = x.bn.Tuoi,
                                            DChi = x.bn.DChi,
                                            SoTT = x.bn.SoTT,
                                            SThe = x.bn.SThe,
                                            Tuyen = x.bn.Tuyen,
                                            DTuong = x.bn.DTuong,
                                            NNhap = x.bn.NNhap.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                            GTinh = x.bn.GTinh,
                                            IDDTBN = x.bn.IDDTBN,
                                            NamSinh = x.bn.NamSinh,
                                            NgayNghi = x.rv.NgayRa.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                            Status = x.bn.Status,
                                        }).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).ToList();
                    }
                }
                else if (cboTimTT.SelectedIndex == 7)
                {
                    _lTKbn = (from kb in _dataContext.VienPhis.Where(p => p.NgayRa >= _dttu && p.NgayRa <= _dtden && p.Status == 1)
                              join bn in _dataContext.BenhNhans on kb.MaBNhan equals bn.MaBNhan
                              select bn).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).ToList();
                    if (DungChung.Bien.MaBV == "24272")
                    {
                        _lTKbn_24272 = (from kb in _dataContext.VienPhis.Where(p => p.NgayRa >= _dttu && p.NgayRa <= _dtden && p.Status == 1)
                                        join bn in _dataContext.BenhNhans on kb.MaBNhan equals bn.MaBNhan
                                        join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                                        select new
                                        {
                                            bn,
                                            rv
                                        }).ToList().Select(x => new BenhNhan24272 
                                        {
                                            MaKP = x.bn.MaKP,
                                            MaBNhan = x.bn.MaBNhan,
                                            TenBNhan = x.bn.TenBNhan,
                                            Tuoi = x.bn.Tuoi,
                                            DChi = x.bn.DChi,
                                            SoTT = x.bn.SoTT,
                                            SThe = x.bn.SThe,
                                            Tuyen = x.bn.Tuyen,
                                            DTuong = x.bn.DTuong,
                                            NNhap = x.bn.NNhap.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                            GTinh = x.bn.GTinh,
                                            IDDTBN = x.bn.IDDTBN,
                                            NamSinh = x.bn.NamSinh,
                                            NgayNghi = x.rv.NgayRa.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                            Status = x.bn.Status,
                                        }).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).ToList();
                    }
                }

            }
            else
            {
                if (cboTimTT.SelectedIndex == 0)
                {
                    if (Bien.MaBV == "27183")
                    {
                        _lTKbn = (from bn in _dataContext.BenhNhans.Where(p => p.Status < 3).OrderBy(p => p.MaBNhan) where (bn.NNhap >= _dttu && bn.NNhap <= _dtden) select bn).ToList();
                    }
                    else
                    {
                        _lTKbn = (from bn in _dataContext.BenhNhans.Where(p => p.NNhap >= _dttu && p.NNhap <= _dtden)
                                  join tu in _dataContext.TamUngs.Where(p => p.PhanLoai == 1 || p.PhanLoai == 2) on bn.MaBNhan equals tu.MaBNhan into kq
                                  from a in kq.DefaultIfEmpty()
                                  where a == null
                                  // where (bn.NNhap >= _dttu && bn.NNhap <= _dtden)
                                  select bn).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).ToList();
                        if (DungChung.Bien.MaBV == "24272")
                        {
                            _lTKbn_24272 = (from bn in _dataContext.BenhNhans.Where(p => p.NNhap >= _dttu && p.NNhap <= _dtden)
                                            join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                                            join tu in _dataContext.TamUngs.Where(p => p.PhanLoai == 1 || p.PhanLoai == 2) on bn.MaBNhan equals tu.MaBNhan into kq
                                            from a in kq.DefaultIfEmpty()
                                            where a == null
                                            // where (bn.NNhap >= _dttu && bn.NNhap <= _dtden)
                                            select new
                                            {
                                                bn,
                                                rv
                                            }).ToList().Select(x => new BenhNhan24272 
                                            {
                                                MaKP = x.bn.MaKP,
                                                MaBNhan = x.bn.MaBNhan,
                                                TenBNhan = x.bn.TenBNhan,
                                                Tuoi = x.bn.Tuoi,
                                                DChi = x.bn.DChi,
                                                SoTT = x.bn.SoTT,
                                                SThe = x.bn.SThe,
                                                Tuyen = x.bn.Tuyen,
                                                DTuong = x.bn.DTuong,
                                                NNhap = x.bn.NNhap.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                                GTinh = x.bn.GTinh,
                                                IDDTBN = x.bn.IDDTBN,
                                                NamSinh = x.bn.NamSinh,
                                                NgayNghi = x.rv.NgayRa.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                                Status = x.bn.Status,
                                            }).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).ToList();
                        }
                    }

                }
                else if (cboTimTT.SelectedIndex == 1)
                {
                    _lTKbn = (from bn in _dataContext.BenhNhans.Where(p => p.NNhap >= _dttu && p.NNhap <= _dtden && ((Bien.MaBV == "27183" || Bien.MaBV == "01071" || Bien.MaBV == "01049" || Bien.MaBV == "12345" || Bien.MaBV == "24297" || Bien.MaBV == "56789") ? p.Status == 3 : true))
                              join tu in _dataContext.TamUngs.Where(p => p.PhanLoai == 1 || p.PhanLoai == 2) on bn.MaBNhan equals tu.MaBNhan
                          into kq
                              from a in kq.DefaultIfEmpty()
                              where Bien.MaBV == "27183" ? true : a != null
                              //  where (bn.NNhap >= _dttu && bn.NNhap <= _dtden)
                              select bn).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).ToList();
                    if (DungChung.Bien.MaBV == "24272")
                    {
                        _lTKbn_24272 = (from bn in _dataContext.BenhNhans.Where(p => p.NNhap >= _dttu && p.NNhap <= _dtden && ((Bien.MaBV == "27183" || Bien.MaBV == "01071" || Bien.MaBV == "01049" || Bien.MaBV == "12345" || Bien.MaBV == "24297" || Bien.MaBV == "56789") ? p.Status == 3 : true))
                                        join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                                        join tu in _dataContext.TamUngs.Where(p => p.PhanLoai == 1 || p.PhanLoai == 2) on bn.MaBNhan equals tu.MaBNhan
                                    into kq
                                        from a in kq.DefaultIfEmpty()
                                        where Bien.MaBV == "27183" ? true : a != null
                                        //  where (bn.NNhap >= _dttu && bn.NNhap <= _dtden)
                                        select new
                                        {
                                            bn,
                                            rv
                                        }).ToList().Select(x => new BenhNhan24272 
                                        {
                                            MaKP = x.bn.MaKP,
                                            MaBNhan = x.bn.MaBNhan,
                                            TenBNhan = x.bn.TenBNhan,
                                            Tuoi = x.bn.Tuoi,
                                            DChi = x.bn.DChi,
                                            SoTT = x.bn.SoTT,
                                            SThe = x.bn.SThe,
                                            Tuyen = x.bn.Tuyen,
                                            DTuong = x.bn.DTuong,
                                            NNhap = x.bn.NNhap.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                            GTinh = x.bn.GTinh,
                                            IDDTBN = x.bn.IDDTBN,
                                            NamSinh = x.bn.NamSinh,
                                            NgayNghi = x.rv.NgayRa.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                            Status = x.bn.Status,
                                        }).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).ToList();
                    }
                }
                else
                    if (cboTimTT.SelectedIndex == 2)
                {
                    if (Bien.MaBV == "27183")
                    {
                        _lTKbn = (from bn in _dataContext.BenhNhans.Where(p => p.NNhap >= _dttu && p.NNhap <= _dtden)
                                  join tu in _dataContext.TamUngs.Where(p => p.PhanLoai == 1 || p.PhanLoai == 2) on bn.MaBNhan equals tu.MaBNhan into kq
                                  from a in kq.DefaultIfEmpty()
                                  where a == null
                                  //  where (bn.NNhap >= _dttu && bn.NNhap <= _dtden)
                                  select bn).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).ToList();
                    }
                    else
                    {
                        _lTKbn = (from bn in _dataContext.BenhNhans
                                  where (bn.NNhap >= _dttu && bn.NNhap <= _dtden)
                                  select bn).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).ToList();
                        if (DungChung.Bien.MaBV == "24272")
                        {
                            _lTKbn_24272 = (from bn in _dataContext.BenhNhans
                                            join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                                            where (bn.NNhap >= _dttu && bn.NNhap <= _dtden)
                                            select new
                                            {
                                                bn,
                                                rv
                                            }).ToList().Select(x => new BenhNhan24272 
                                            {
                                                MaKP = x.bn.MaKP,
                                                MaBNhan = x.bn.MaBNhan,
                                                TenBNhan = x.bn.TenBNhan,
                                                Tuoi = x.bn.Tuoi,
                                                DChi = x.bn.DChi,
                                                SoTT = x.bn.SoTT,
                                                SThe = x.bn.SThe,
                                                Tuyen = x.bn.Tuyen,
                                                DTuong = x.bn.DTuong,
                                                NNhap = x.bn.NNhap.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                                GTinh = x.bn.GTinh,
                                                IDDTBN = x.bn.IDDTBN,
                                                NamSinh = x.bn.NamSinh,
                                                NgayNghi = x.rv.NgayRa.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                                Status = x.bn.Status,
                                            }).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).ToList();
                        }
                    }

                }
                else
                {
                    if (Bien.MaBV == "27183")
                    {
                        _lTKbn = (from bn in _dataContext.BenhNhans.Where(p => p.NNhap >= _dttu && p.NNhap <= _dtden)
                                  join tu in _dataContext.TamUngs.Where(p => p.PhanLoai == 1 || p.PhanLoai == 2) on bn.MaBNhan equals tu.MaBNhan
                                  //  where (bn.NNhap >= _dttu && bn.NNhap <= _dtden)
                                  select bn).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).ToList();
                    }
                    else
                    {
                        _lTKbn = (from bn in _dataContext.BenhNhans

                                  where (bn.NNhap >= _dttu && bn.NNhap <= _dtden)
                                  select bn).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).ToList();
                        if (DungChung.Bien.MaBV == "24272")
                        {
                            _lTKbn_24272 = (from bn in _dataContext.BenhNhans
                                            join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                                            where (bn.NNhap >= _dttu && bn.NNhap <= _dtden)
                                            select new
                                            {
                                                bn,
                                                rv
                                            }).ToList().Select(x => new BenhNhan24272 
                                            {
                                                MaKP = x.bn.MaKP,
                                                MaBNhan = x.bn.MaBNhan,
                                                TenBNhan = x.bn.TenBNhan,
                                                Tuoi = x.bn.Tuoi,
                                                DChi = x.bn.DChi,
                                                SoTT = x.bn.SoTT,
                                                SThe = x.bn.SThe,
                                                Tuyen = x.bn.Tuyen,
                                                DTuong = x.bn.DTuong,
                                                NNhap = x.bn.NNhap.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                                GTinh = x.bn.GTinh,
                                                IDDTBN = x.bn.IDDTBN,
                                                NamSinh = x.bn.NamSinh,
                                                NgayNghi = x.rv.NgayRa.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                                Status = x.bn.Status,
                                            }).OrderBy(p => p.SoTT).OrderBy(p => p.MaBNhan).ToList();
                        }
                    }
                }
            }
            // }
            grcBNhantt.DataSource = null;
            _lTKbn = _lTKbn.Where(p => (noitru == 2 ? true : p.NoiTru == noitru)).Where(p => p.MaKCB == Bien.MaBV).Where(p => IDDTBN == 99 ? true : p.IDDTBN == IDDTBN).ToList();
            if (Bien.MaBV == "30004")// lấy thêm ngày vào viên
            {
                colNgayVaoVien.FieldName = "NgayHM";
                var qvv = _dataContext.VaoViens.ToList();
                foreach (BenhNhan bn in _lTKbn)// tạm để ngày vào viện vào ngày hạn mức để hiển thị
                {
                    VaoVien vv = qvv.Where(p => p.MaBNhan == bn.MaBNhan).FirstOrDefault();
                    if (vv != null)
                        bn.NgayHM = vv.NgayVao;
                    else
                        bn.NgayHM = null;
                }
            }
            if (Bien.MaBV == "24012")
            {
                var qvv = _dataContext.VaoViens.ToList();
                foreach (BenhNhan bn in _lTKbn)// tạm để ngày vào viện vào ngày MaIK để hiển thị
                {
                    VaoVien vv = qvv.Where(p => p.MaBNhan == bn.MaBNhan).FirstOrDefault();
                    if (vv != null)
                    {
                        bn.Ma_lk = (vv.NgayVao != null ? vv.NgayVao.ToString() : null);
                    }
                    else
                    {
                        bn.Ma_lk = null;
                    }
                }
                var qrv = _dataContext.RaViens.ToList();
                foreach (BenhNhan bn in _lTKbn)// tạm để ngày ra viện vào PID để hiển thị
                {
                    RaVien rv = qrv.Where(p => p.MaBNhan == bn.MaBNhan).FirstOrDefault();
                    if (rv != null)
                        bn.PID = (rv.NgayRa != null ? rv.NgayRa.ToString() : null);
                    else
                        bn.PID = null;
                }
                grcBNhantt.DataSource = _lTKbn.Where(p => p.TenBNhan.ToLower().Contains(_tk) || p.MaBNhan == _int_maBN).ToList();
            }
            else
            {
                grcBNhantt.DataSource = _lTKbn.Where(p => p.TenBNhan.ToLower().Contains(_tk) || p.MaBNhan == _int_maBN).ToList();
                if (DungChung.Bien.MaBV == "24272")
                {
                    grcBNhantt.DataSource = _lTKbn_24272.Where(p => p.TenBNhan.ToLower().Contains(_tk) || p.MaBNhan == _int_maBN).Distinct().ToList();
                }
            }
        }
        List<DTBN> _ldtbn = new List<DTBN>();
        List<PhanLoaiTT> _lLoaiTTs = new List<PhanLoaiTT>();
        List<DungChung.Ham.TrongDMBHYT> _lTrongDM = new List<DungChung.Ham.TrongDMBHYT>();
        bool checkThuTien = false;
        List<DichVu> _lthuoc;
        private void usTamThu_TToan_Load(object sender, EventArgs e)
        {
            _lLoaiTTs.Clear();
            if (DungChung.Bien.MaBV == "24012")
            {
                _lLoaiTTs.Add(new PhanLoaiTT { SoPhanLoai = 0, TenPhanLoai = "Tạm thu" });
                _lLoaiTTs.Add(new PhanLoaiTT { SoPhanLoai = 1, TenPhanLoai = "Thu TT" });
                _lLoaiTTs.Add(new PhanLoaiTT { SoPhanLoai = 2, TenPhanLoai = "Chi TT" });
                _lLoaiTTs.Add(new PhanLoaiTT { SoPhanLoai = 3, TenPhanLoai = "Thu trực tiếp" });
                _lLoaiTTs.Add(new PhanLoaiTT { SoPhanLoai = 4, TenPhanLoai = "Chi tạm thu" });
                _lLoaiTTs.Add(new PhanLoaiTT { SoPhanLoai = 5, TenPhanLoai = "Thu ngân hàng" });
                _lLoaiTTs.Add(new PhanLoaiTT { SoPhanLoai = 6, TenPhanLoai = "Chi trực tiếp" });
                _lLoaiTTs.Add(new PhanLoaiTT { SoPhanLoai = 7, TenPhanLoai = "Tạm thu nhu cầu" });
            }
            else if (DungChung.Bien.MaBV == "01071")
            {
                _lLoaiTTs.Add(new PhanLoaiTT { SoPhanLoai = 0, TenPhanLoai = "Tạm thu" });
                _lLoaiTTs.Add(new PhanLoaiTT { SoPhanLoai = 1, TenPhanLoai = "Thu TT" });
                _lLoaiTTs.Add(new PhanLoaiTT { SoPhanLoai = 2, TenPhanLoai = "Chi TT" });
                _lLoaiTTs.Add(new PhanLoaiTT { SoPhanLoai = 3, TenPhanLoai = "Thu trực tiếp" });
                _lLoaiTTs.Add(new PhanLoaiTT { SoPhanLoai = 4, TenPhanLoai = "Chi tạm thu" });
                _lLoaiTTs.Add(new PhanLoaiTT { SoPhanLoai = 5, TenPhanLoai = "Thu ngân hàng" });
                _lLoaiTTs.Add(new PhanLoaiTT { SoPhanLoai = 6, TenPhanLoai = "Chi trực tiếp" });
                _lLoaiTTs.Add(new PhanLoaiTT { SoPhanLoai = 7, TenPhanLoai = "Chuyển khoản tạm thu" });
            }
            else
            {
                _lLoaiTTs.Add(new PhanLoaiTT { SoPhanLoai = 0, TenPhanLoai = "Tạm thu" });
                _lLoaiTTs.Add(new PhanLoaiTT { SoPhanLoai = 1, TenPhanLoai = "Thu TT" });
                _lLoaiTTs.Add(new PhanLoaiTT { SoPhanLoai = 2, TenPhanLoai = "Chi TT" });
                _lLoaiTTs.Add(new PhanLoaiTT { SoPhanLoai = 3, TenPhanLoai = "Thu trực tiếp" });
                _lLoaiTTs.Add(new PhanLoaiTT { SoPhanLoai = 4, TenPhanLoai = "Chi tạm thu" });
                _lLoaiTTs.Add(new PhanLoaiTT { SoPhanLoai = 5, TenPhanLoai = "Thu ngân hàng" });
                _lLoaiTTs.Add(new PhanLoaiTT { SoPhanLoai = 6, TenPhanLoai = "Chi trực tiếp" });
            }
            
            if (Bien.MaBV != "24012" && Bien.MaBV != "24272")
            {
                colNgayVaoVien.Visible = false;
                colNgayRaVien.Visible = false;
            }
            if (cboPLThu.Properties.Items.Count == 0)
            {
                foreach (var item in _lLoaiTTs)
                {
                    cboPLThu.Properties.Items.Add(item.TenPhanLoai);
                }
            }
            if (Bien.MaBV == "24272")
            {
                colNNhap.Visible = false;
            }
            if (Bien.MaBV == "27023")
            {
                colSua.Visible = true;
            }
            if (Bien.MaBV == "27022")
            {
                string[] TenPhieu = new string[] { "Phiếu phát thuốc", "Phiếu xuất kho" };
                cboPhieu.Properties.Items.Clear();
                cboPhieu.Properties.Items.AddRange(TenPhieu);
            }
            if (Bien.MaBV != "12345" && Bien.MaBV != "24297")
            {
                colNguoiDuyet.Visible = false;
                colTrangThai.Visible = false;
            }
            if (Bien.MaBV == "01071" || Bien.MaBV == "01049")
            {
                lupNguoiTT.Enabled = false;
                btnXacNhan.Visible = true;
                colThuNH.Visible = true;
            }

            else
            {
                grvTamUng.OptionsSelection.MultiSelect = false;
                grvTamUng.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            }
            if (DungChung.Ham.checkQuyenFalse(xtraTToan.Name)[0])
            {
                btnThanhToan.Enabled = true;
                btnXoa.Enabled = true;
                btnHuyDon.Enabled = true;
            }
            else
            {
                btnThanhToan.Enabled = false;
                btnXoa.Enabled = false;
                btnHuyDon.Enabled = false;
            }

            if (Bien.MaBV == "27022")
                this.colSoLuong.DisplayFormat.FormatString = "##,###.##";
            if (Bien.MaBV == "30004")
            {
                colNgayVaoVien.Visible = true;// ngày vào viện
                colNgayVaoVien.VisibleIndex = 6;
            }
            if (Bien.MaBV == "01071" || Bien.MaBV == "01049" || Bien.MaBV == "12345" || Bien.MaBV == "24297" || Bien.MaBV == "56789" || Bien.MaBV == "30372")
            {
                cboNoitru.SelectedIndex = 2;
                ckcDonNgoai.Visible = true;
            }
            if (Bien.MaBV.Substring(0, 2) != "24")
                colXHH.Visible = false;
            //if (Bien.MaBV == "")
            dtNgayDuyetCP.DateTime = DateTime.Now;
            cboInBangKe.Properties.Items.Clear();
            string[] st = new string[] { "Bảng kê mẫu A5", "Bảng kê mẫu A4", "Bảng kê mẫu A4_New", "Phiếu TT ra viện(mẫu 38)", "Phiếu TT ra viện(mẫu 40)", "Bảng kê QĐ6556" };
            string[] st_19048 = new string[] { "Bảng kê mẫu A5", "Bảng kê mẫu A4", "Bảng kê mẫu A4_New" };
            if (Bien.MaBV == "19048")
                cboInBangKe.Properties.Items.AddRange(st_19048);
            else
                cboInBangKe.Properties.Items.AddRange(st);
            xtraTToan.PageEnabled = DungChung.Ham.checkQuyen(xtraTToan.Name)[3];
            xtraTThu.PageEnabled = DungChung.Ham.checkQuyen(xtraTThu.Name)[3];
            xtraDuyet.PageEnabled = DungChung.Ham.checkQuyen(xtraDuyet.Name)[3];
            checkThuTien = DungChung.Ham.checkQuyenFalse(xtraDuyet.Name)[1];
            EnableControlTC(true);
            if (Bien.MaBV == "30002")
            {
                xtraTToan_TThu.SelectedTabPage = xtraDuyet;
                cboTimTT.SelectedIndex = 1;
                btnInBK_30002.Visible = true;
            }
            //var que =(from bn in _dataContext.BenhNhans join bndt in _dataContext.DThuocs on bn.MaBNhan equals bndt.MaBNhan
            //          select new {bn.MaBNhan,bn.TenBNhan,bn.Tuoi,bn.DChi,bn.Status}).Where(p=>p.Status== (9));
            //var que = _dataContext.BenhNhans.Where(p => p.Status == 1).OrderByDescending(p => p.IDBNhan).ToList();
            //var cb = (from _cb in _dataContext.CanBoes select new { _cb.TenCB, _cb.MaCB }).ToList();
            //if (cb.Count > 0)
            //    lupNguoiTT.Properties.DataSource = cb.ToList();

            _ldtbn = _dataContext.DTBNs.OrderBy(p => p.DTBN1).ToList();
            _ldtbn.Add(new DTBN { IDDTBN = 99, DTBN1 = "Tất cả", HTTT = 99 });
            cboDTuong.Properties.DataSource = null;
            cboDTuong.Properties.DataSource = _ldtbn;
            _lTrongDM = DungChung.Ham._SetValue_TrongDMBH();
            lupTrongBH.DataSource = _lTrongDM;
            lup_TrongDMtu.DataSource = _lTrongDM;
            lupNguoiTT.EditValue = Bien.MaCB;
            var q = (from kp in _dataContext.KPhongs where (kp.PLoai == Bien.st_PhanLoaiKP.LamSang || kp.PLoai == Bien.st_PhanLoaiKP.KeToan || kp.PLoai == Bien.st_PhanLoaiKP.PhongKham) select new { kp.TenKP, kp.MaKP, kp.PLoai }).OrderBy(p => p.TenKP).ToList();
            lup_KhoaPhong.DataSource = q.ToList();
            lupBPKe.Properties.DataSource = q.ToList();
            lupBPThu.Properties.DataSource = q.ToList();
            lupBPTC.Properties.DataSource = q.ToList();
            var _tencb = _dataContext.CanBoes.OrderBy(p => p.TenCB).ToList();
            lupNguoiThu.Properties.DataSource = _tencb.ToList();
            lupMaKPbn.DataSource = _dataContext.KPhongs.ToList();
            lupTenCB.DataSource = _tencb.ToList();
            _lCanBo = _dataContext.CanBoes.OrderBy(p => p.TenCB).ToList();
            lupNguoiTT.Properties.DataSource = _lCanBo.Where(p => p.Status == 1).ToList();
            lupCBTC.Properties.DataSource = _tencb.ToList();
            List<DungChung.Ham.LKhoaPhong> _lkhoap = new List<DungChung.Ham.LKhoaPhong>(DungChung.Ham.TatCa());
            foreach (var a in q)
            {
                DungChung.Ham.LKhoaPhong moi = new DungChung.Ham.LKhoaPhong();
                moi.MaKP = a.MaKP;
                moi.TenKP = a.TenKP;
                moi.PLoai = a.PLoai;
                _lkhoap.Add(moi);
            }
            var q1 = _dataContext.DichVus.OrderBy(p => p.TenDV).ToList();
            _lthuoc = q1.Where(p => p.PLoai == 1).ToList();
            lupMaDVtt.DataSource = q1.ToList();
            lup_TenDVtu.DataSource = q1.ToList();
            lupMaDVdatt.DataSource = q1.ToList();
            lupMaDvThuThang.DataSource = q1.Where(p => p.PLoai == 2).ToList();
            lupMaDVTra.DataSource = q1.Where(p => p.PLoai == 2).ToList();
            lupMaQD.DataSource = q1.ToList();
            lupMaQDdatt.DataSource = q1.ToList();
            lupTimMaKP.Properties.DataSource = _lkhoap.ToList();

            if (dtNgayTT.DateTime == (DateTime)default)
                dtNgayTT.DateTime = System.DateTime.Now;

            if (dtTimTuNgay.DateTime == (DateTime)default)
                dtTimTuNgay.DateTime = System.DateTime.Now;

            if (dtTimDenNgay.DateTime == (DateTime)default)
                dtTimDenNgay.DateTime = System.DateTime.Now;

            if (Bien.MaBV == "27001")
                cboTimTT.SelectedIndex = 5;
            //TimKiem();
            grcBNhantt.DataSource = _lTKbn;
            if (DungChung.Bien.MaBV == "24272")
            {
                grcBNhantt.DataSource = _lTKbn_24272;
            }
            TTLuutthu = 0;
            //if (Bien.MaBV == "30009")
            //{
            //    ckDuyet.Visible = true;
            //    dtNgayThuTien.Visible = true;
            //}
            //else
            //{
            //    ckDuyet.Visible = false;
            //    dtNgayThuTien.Visible = false;
            //}
            //worker.DoWork += worker_DoWork;
            //worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            timer1.Interval = 60000;
            if (Bien.MaBV == "20001" || Bien.MaBV == "24009")
                timer1.Interval = 180000;
            //timer1.Tick += timer1_Tick;
            timer1.Start();

            if (Bien.MaBV == "27022")
            {
                cboPhieu.Visible = true;
            }
            else
                simpleButton3.Visible = true;
            GC.Collect();
        }

        private void grvBNhantt_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            EnableControl(false);
            if (DungChung.Ham.checkQuyen(xtraTThu.Name)[0])
            {
                btnMoi.Enabled = true;
                btnSua.Enabled = true;
                btnXoatthu.Enabled = true;

            }
            else
            {
                btnMoi.Enabled = false;
                btnSua.Enabled = false;
                btnXoatthu.Enabled = false;
            }
            _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
            tienbn = -1;

            if (xtraTThu.Text.Contains("*"))
            {
                DialogResult _result = MessageBox.Show("Bạn chưa lưu dữ liệu, Bạn có muốn lưu không?", "thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_result == DialogResult.Yes)
                {
                    btnLuutthu_Click(sender, e);
                }
                xtraTThu.Text = "Tạm ứng|Thu-Chi TT";
                TTLuutthu = 0;
            }
            bool TTBenhNHan = true;
            if (grvBNhantt.GetFocusedRowCellValue(colIDNhap) != null)
                TTBenhNHan = false;
            #region Thanh toán bệnh nhân
            if (TTBenhNHan)
            {
                _HTTT = 99;

                if (grvBNhantt.GetFocusedRowCellValue(colMaBNhan) != null)
                {
                    int _mabnhan = Convert.ToInt32(grvBNhantt.GetFocusedRowCellValue(colMaBNhan));
                    #region Ẩn/ hiện nút gửi HSSK - 200219 - dungtt
                    var qrv = _dataContext.RaViens.Where(p => p.MaBNhan == _mabnhan).FirstOrDefault();
                    if (qrv == null)
                    {
                        btnGuiHSSK.Enabled = false;
                    }
                    else if (qrv.maGiaoDichHSSK != null && qrv.maGiaoDichHSSK != "")
                    {
                        btnGuiHSSK.Enabled = false;
                    }
                    else
                    {
                        btnGuiHSSK.Enabled = true;

                    }

                    #endregion


                    var ktraHTTT = _dataContext.TTboXungs.FirstOrDefault(p => p.MaBNhan == _mabnhan);
                    if (ktraHTTT != null)
                    {
                        ckcthanhtoan.Checked = Convert.ToBoolean(ktraHTTT.HTThanhToan);
                        btnXacNhan.Enabled = !Convert.ToBoolean(ktraHTTT.HTThanhToan);
                    }
                    else
                    {
                        ckcthanhtoan.Checked = false;
                        btnXacNhan.Enabled = false;
                    }

                    int IDDTBN = 99;
                    if (grvBNhantt.GetFocusedRowCellValue("IDDTBN") != null)
                        IDDTBN = Convert.ToInt32(grvBNhantt.GetFocusedRowCellValue("IDDTBN"));
                    if (_ldtbn.Where(p => p.IDDTBN == IDDTBN).ToList().Count > 0)
                        _HTTT = _ldtbn.Where(p => p.IDDTBN == IDDTBN).ToList().First().HTTT;
                    cboInBangKe.SelectedIndex = -1;
                    txtMaBNhan.Text = grvBNhantt.GetFocusedRowCellValue(colMaBNhan).ToString();
                    txtTenBenhNhan.Text = grvBNhantt.GetFocusedRowCellValue(colTenBNhan).ToString();
                    //  txtIdbn.Text = grvBNhantt.GetFocusedRowCellValue(colIDBNhan).ToString();
                    //dtNgayTT.DateTime=grvBNhantt.GetFocusedRowCellValue(
                    if (grvBNhantt.GetFocusedRowCellValue(colSThe) != null && grvBNhantt.GetFocusedRowCellValue(colSThe).ToString() != "")
                        txtSoThe.Text = grvBNhantt.GetFocusedRowCellValue(colSThe).ToString();
                    else
                        txtSoThe.Text = "";
                    if (grvBNhantt.GetFocusedRowCellValue(colTuoi) != null && grvBNhantt.GetFocusedRowCellValue(colTuoi).ToString() != "")
                        txtTuoi.Text = grvBNhantt.GetFocusedRowCellValue(colTuoi).ToString();
                    //1 hiển thị số tiền tạm thu
                    var _ttamthu = (from tt in _dataContext.TamUngs
                                    where (tt.MaBNhan == _mabnhan)
                                    //where (tt.PhanLoai== ("Tạm thu"))
                                    group tt by new { tt.PhanLoai, tt.SoTo, tt.KetLuan, tt.ThanhToan } into kq
                                    select new { kq.Key.PhanLoai, kq.Key.SoTo, kq.Key.KetLuan, _tongtien = kq.Sum(p => p.SoTien), kq.Key.ThanhToan }).ToList();
                    if (_ttamthu.Count() > 0)
                    {
                        if (_ttamthu.First()._tongtien != null)
                            _tienTThu = _ttamthu.First()._tongtien.Value;
                        if (_ttamthu.Where(p => p.PhanLoai == 0).Count() > 0)
                            txtTienTamUng.Text = _ttamthu.Where(p => p.PhanLoai == 0).First()._tongtien.ToString();
                        else
                            txtTienTamUng.Text = "0";
                        if (_ttamthu.First().SoTo != null)
                            txtSoTo.Text = _ttamthu.First().SoTo.ToString();
                        else
                            txtSoTo.Text = "";
                        if (_ttamthu.First().KetLuan != null)
                            cboKLKSK.SelectedIndex = _ttamthu.First().KetLuan.Value;
                        else
                            cboKLKSK.SelectedIndex = -1;
                        //ckcthanhtoan.Checked = Convert.ToBoolean(_ttamthu.First().ThanhToan);
                        //if()

                    }
                    else
                    {
                        if (_HTTT == 3)
                            cboPLThu.SelectedIndex = 1;
                        _tienTThu = 0;
                        txtTienTamUng.Text = "0";
                        txtSoTo.Text = "";
                        cboKLKSK.SelectedIndex = -1;
                        //ckcthanhtoan.Checked = false;
                    }

                    // kết thúc 1
                    // 2 hiện thị số tiền thanh toán

                    var _ttoan = (from vp in _dataContext.VienPhis.Where(p => p.MaBNhan == _mabnhan)
                                  join vpct in _dataContext.VienPhicts.OrderBy(p => p.idVPhict) on vp.idVPhi equals vpct.idVPhi
                                  select new { vp, vpct }).ToList();
                    var q_vpct = (from ct in _ttoan select ct.vpct).ToList();
                    grcDaTT.DataSource = q_vpct.ToList();
                    if (_ttoan.Count > 0)
                    {
                        if (_ttoan.First().vp.Status == 1)
                        {

                            chkbtn_DuyetBK.Text = "Hủy duyệt chi phí";

                        }
                        else
                        {

                            chkbtn_DuyetBK.Text = "Duyệt Chi phí";
                        }
                        //if (ckcthanhtoan.Checked)

                        lupBPKe.Properties.ReadOnly = true;
                        lupNguoiTT.Properties.ReadOnly = true;
                        dtNgayTT.Properties.ReadOnly = true;
                        btnThanhToan.Enabled = false;
                        if (DungChung.Ham.checkQuyenFalse(xtraTToan.Name)[0])
                        {

                            btnXoa.Enabled = true;
                        }
                        else
                        {
                            btnXoa.Enabled = false;
                        }

                        btnXem.Enabled = true;
                        //if(_ttoan.First().vp.NgayDuyetCP != null)
                        //dtNgayDuyetCP.DateTime = _ttoan.First().vp.NgayDuyetCP.Value;
                        double tbntt = _ttoan.Sum(p => p.vpct.TBNCTT) + _ttoan.Sum(p => p.vpct.TBNTT);
                        if (tbntt > 0)
                        {
                            //_tienBN = _ttoan.Sum(p => p.vpct.TienBN);
                            txtTienBN.Text = tbntt.ToString();
                        }
                        else
                            txtTienBN.Text = "0";
                        // hiện thị CB thanh toán và khoa thanh toán
                        if (_ttoan.First().vp.MaKP != null)
                        {
                            lupBPKe.EditValue = _ttoan.First().vp.MaKP;
                            lupBPThu.EditValue = _ttoan.First().vp.MaKP;
                        }
                        else
                        {
                            lupBPKe.EditValue = "";
                            lupBPThu.EditValue = "";
                        }
                        if (_ttoan.First().vp.NgoaiGio != null && _ttoan.First().vp.NgoaiGio == 1)
                            chkNgoaiGio.Checked = true;
                        else
                            chkNgoaiGio.Checked = false;
                        if (_ttoan.First().vp.MaCB != null && _ttoan.First().vp.MaCB.ToString() != "")
                            lupNguoiTT.EditValue = _ttoan.First().vp.MaCB.ToString();
                        else
                            lupNguoiTT.EditValue = "";
                        if (_ttoan.First().vp.NgayTT != null)
                            dtNgayTT.DateTime = _ttoan.First().vp.NgayTT.Value;
                        else
                            dtNgayTT.DateTime = System.DateTime.Now;

                        if (_ttoan.First().vp.NgayDuyetCP != null)
                            dtNgayDuyetCP.DateTime = _ttoan.First().vp.NgayDuyetCP.Value;
                        else
                            dtNgayDuyetCP.DateTime = System.DateTime.Now;

                        // hiển thị trong tab_Duyet - chi phi
                        double _tientamthu = 0, _tienbnhan = 0, _tienthu = 0, _tienthutt = 0, _tongchitamthu = 0, _tienchi = 0;
                        var _tamthu_D = _dataContext.TamUngs.Where(p => p.MaBNhan == _mabnhan).ToList();

                        // tiền thu trực tiếp
                        if (_tamthu_D.Where(p => p.PhanLoai == 3).Sum(p => p.SoTien) != null)
                            _tienthutt = _tamthu_D.Where(p => p.PhanLoai == 3).Sum(p => p.SoTien) ?? 0;
                        txtDaTT.Text = _tienthutt.ToString("##,###");
                        //
                        if (_tamthu_D.Where(p => p.PhanLoai == 0).Sum(p => p.SoTien) != null)
                        {
                            _tientamthu = _tamthu_D.Where(p => p.PhanLoai == 0).Sum(p => p.SoTien).Value;
                            _tongchitamthu = _tamthu_D.Where(p => p.PhanLoai == 4).Sum(p => p.SoTien).Value;
                        }
                        txt_TongTamThu.Text = _tientamthu.ToString("##,###");
                        txtchitamthu.Text = _tongchitamthu.ToString("##,###");
                        double _tienNguonKhac = (double)_ttoan.Sum(p => p.vpct.TienNguonKhac);
                        txt_CPDT.Text = (_ttoan.Sum(p => p.vpct.TBNCTT) + _tienNguonKhac + _ttoan.Sum(p => p.vpct.TBNTT) + _ttoan.Sum(p => p.vpct.TienBH)).ToString("##,###");
                        _tienbnhan = _ttoan.Sum(p => p.vpct.TBNCTT) + _ttoan.Sum(p => p.vpct.TBNTT);//_ttoan.Sum(p => p.vpct.TienBN);
                        if (_tamthu_D.Where(p => p.PhanLoai == 2).Sum(p => p.SoTien) != null)
                        {
                            _tienchi = _tamthu_D.Where(p => p.PhanLoai == 2).Sum(p => p.SoTien).Value;
                        }
                        if (_tamthu_D.Where(p => p.PhanLoai == 1).Sum(p => p.SoTien) != null)
                        {
                            _tienthu = _tamthu_D.Where(p => p.PhanLoai == 1).Sum(p => p.SoTien).Value;
                        }
                        //_tienthuchi = Math.Round(_tienbnhan - _tienthutt - (_tientamthu - _tongchitamthu), 3);// - _tienthutt,tiền bn trả thêm hay nhận lại ko tính số tiền đã thu trực tiếp, đức 11/11
                        if (_tienchi >= 0)
                        {
                            txt_TienTra.Text = _tienchi.ToString("##,###");
                            //txt_TienThu.Text = _tienthuchi;

                        }
                        else
                        {
                            txt_TienTra.Text = "";
                        }
                        if (_tienthu >= 0)
                        {
                            txt_TienThu.Text = _tienthu.ToString("##,###");
                        }
                        else
                        {
                            txt_TienThu.Text = "";
                        }
                        txt_TongCPTT.Text = _tienbnhan.ToString("##,###");

                        //
                    }
                    else
                    {
                        if (Bien.MaBV == "01071" || Bien.MaBV == "01049" || Bien.MaBV == "12345" || Bien.MaBV == "24297" || Bien.MaBV == "56789" || Bien.MaBV == "30303")
                        {
                            var ktrack = _dataContext.TTboXungs.Where(p => p.MaBNhan == _mabnhan).FirstOrDefault();
                            if (ktrack != null && ktrack.HTThanhToan != 1)
                            {
                                double TienBN = 0, TienTU = 0, TienBNTraT = 0;
                                TienBN = DungChung.Ham.SoTienCanThu(_mabnhan);
                                TienTU = DungChung.Ham.TongTienTU(_dataContext, _mabnhan);
                                TienBNTraT = DungChung.Ham.TienBNTraThem(_dataContext, _mabnhan);
                                if (TienBNTraT >= 100000)
                                {
                                    MessageBox.Show("Bệnh nhân sắp hết tiền tạm thu " + "\n Tổng tiền tạm thu: " + TienTU.ToString("###,###") + "\n Tổng tiền bệnh nhân phải trả: " + TienBN.ToString("###,###") + "\n Tổng tiền BN phải trả thêm:" + TienBNTraT.ToString("###,###"), "Thông báo", MessageBoxButtons.OK);
                                }
                            }
                        }
                        chkbtn_DuyetBK.Checked = false;
                        chkbtn_DuyetBK.Text = "Duyệt bảng kê";
                        txtDaTT.ResetText();
                        txt_TienTra.Text = "";
                        txt_TienThu.Text = "";
                        txt_TongTamThu.Text = "";
                        txtchitamthu.Text = "";
                        txt_TongCPTT.Text = "";
                        //if(Bien.MaBV=="01071")
                        txtTienBN.Text = "";
                        dtNgayDuyetCP.DateTime = DateTime.Now;
                        lupBPKe.Properties.ReadOnly = false;
                        lupNguoiTT.Properties.ReadOnly = false;
                        dtNgayTT.Properties.ReadOnly = false;

                        if (DungChung.Ham.checkQuyenFalse(xtraTToan.Name)[0])
                        {
                            btnThanhToan.Enabled = true;
                        }
                        btnXoa.Enabled = false;
                        btnXem.Enabled = false;
                        lupBPKe.EditValue = Bien.MaKP;
                        lupNguoiTT.EditValue = Bien.MaCB;
                        dtNgayTT.DateTime = System.DateTime.Now;
                    }

                }
                else
                {
                    txtTenBenhNhan.Text = "";
                    txtSoThe.Text = "";
                    txtTuoi.Text = "";
                    txtMaBNhan.Text = "";
                    btnGuiHSSK.Enabled = false;

                }
                //ket thuc 2


                if (grvBNhantt.GetFocusedRowCellValue(colSThe) != null && grvBNhantt.GetFocusedRowCellValue(colSThe).ToString() != "")
                {
                    txtSoThe.Text = grvBNhantt.GetFocusedRowCellValue(colSThe).ToString();
                }
                else
                {

                }
                if (grvBNhantt.GetFocusedRowCellValue(colDTuong) != null && grvBNhantt.GetFocusedRowCellValue(colDTuong).ToString() != "")
                {
                    _DTuong = grvBNhantt.GetFocusedRowCellValue(colDTuong).ToString();
                }
                else
                {
                    _DTuong = "";
                }
                if (grvBNhantt.GetFocusedRowCellValue(colTuyen) != null && grvBNhantt.GetFocusedRowCellValue(colTuyen).ToString() != "")
                {
                    _tuyen = int.Parse(grvBNhantt.GetFocusedRowCellValue(colTuyen).ToString());
                }
                // xem chi phí bệnh nhân
                int ot;
                _lThuThangChuaTH = new List<ThuThang>();
                if (Int32.TryParse(txtMaBNhan.Text, out ot))
                {
                    _mabn = Convert.ToInt32(txtMaBNhan.Text);

                    var kt = (from ds in _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn).OrderBy(p => p.IDDon) select ds).ToList();
                    var vienphi = (from kd in _dataContext.DThuocs.Where(p => DungChung.Bien.MaBV == "30372" ? (p.KieuDon != -2 || p.KieuDon != 6) : p.KieuDon != 2)
                                   join kdct in _dataContext.DThuoccts.Where(o => o.IsMuaNgoai == null || o.IsMuaNgoai == false).OrderBy(p => p.IDDonct) on kd.IDDon equals kdct.IDDon
                                   where (kd.MaBNhan == _mabn)
                                   where kd.KieuDon != 6
                                   group kdct by new { kdct.IDDon, kdct.Mien, kdct.MaKP, kdct.MaDV, kdct.DonGia, kdct.DonVi, kdct.TrongBH, kdct.TyLeTT, kdct.XHH } into kq
                                   select new { kq.Key.IDDon, kq.Key.Mien, MaKP = kq.Key.MaKP, kq.Key.TrongBH, XHH = kq.Key.XHH == 1 ? true : false, madv = kq.Key.MaDV, dongia = kq.Key.DonGia, kq.Key.TyLeTT, donvi = kq.Key.DonVi, soluong = kq.Sum(p => p.SoLuong), thanhtien = kq.Sum(p => p.ThanhTien) }).Where(p => p.soluong != 0).OrderBy(p => p.madv).ToList();
                    grcThanhToan.DataSource = vienphi.ToList();

                    #region hiển thị chi phí thu thẳng cho chọn được trả tiền lại cho bệnh nhân hay không //dungtt_120117
                    var qthuthang = (from tu in _dataContext.TamUngs.Where(p => p.MaBNhan == _mabn).Where(p => p.PhanLoai == 3)
                                     join tuct in _dataContext.TamUngcts on tu.IDTamUng equals tuct.IDTamUng
                                     join cls in _dataContext.CLS on tu.MaBNhan equals cls.MaBNhan
                                     join cd in _dataContext.ChiDinhs.Where(p => p.Status != 1) on new { cls.IdCLS, tuct.MaDV } equals new { IdCLS = cd.IdCLS ?? 0, MaDV = cd.MaDV ?? 0 }
                                     select new ThuThang { MaDV = tuct.MaDV, DonGia = tuct.DonGia, SoLuong = tuct.SoLuong, ThanhTien = tuct.TienBN, MaKP = tu.MaKP ?? 0 }).ToList();
                    if (qthuthang.Count() > 0)
                    {
                        var qtralai = (from tu in _dataContext.TamUngs.Where(p => p.MaBNhan == _mabn).Where(p => p.PhanLoai == 5)
                                       join tuct in _dataContext.TamUngcts on tu.IDTamUng equals tuct.IDTamUng
                                       select new { tu.MaKP, tuct.MaDV, tuct.SoLuong, tuct.TienBN }).ToList();
                        if (qtralai.Count > 0)
                        {
                            foreach (var a in qthuthang)
                            {

                                a.SoLuong = a.SoLuong - (qtralai.Where(p => p.MaDV == a.MaDV).Sum(p => p.TienBN));

                            }
                        }
                        _lThuThangChuaTH = qthuthang.Where(p => p.SoLuong > 0).ToList();
                        grc_ThuThang.DataSource = _lThuThangChuaTH;
                    }
                    else
                        grc_ThuThang.DataSource = null;
                    #endregion

                }
                else
                {
                    grcThanhToan.DataSource = null;
                    grc_ThuThang.DataSource = null;
                }
                binTra.DataSource = new List<ThuThang>();
                grcTraTamUng.DataSource = binTra;
                //kết thúc xem
                // tam thu
                _litamung = _dataContext.TamUngs.Where(p => p.MaBNhan == _mabn && p.PhanLoai != 5).OrderByDescending(p => p.IDTamUng).ToList();
                if (_litamung.Count == 0)
                {
                    btnTaoHD3.Visible = false;
                    btnTaoHD3.Text = "Tạo HĐ";
                }
                else
                {
                    btnTaoHD3.Visible = false;
                    btnTaoHD3.Text = "Tạo HĐ";
                }
                grcTamUng.DataSource = _litamung.ToList();
                List<TamUng> _ltung = new List<TamUng>();
                _ltung = _litamung.Where(p => p.PhanLoai == 1 || p.PhanLoai == 2).ToList();
                if (_ltung.Count > 1)
                {
                    MessageBox.Show("Bệnh nhân có nhiều hơn 1 chứng từ thu-chi TT\n Hãy hủy đi 1 chứng từ");
                }
                int id = 0;
                if (_ltung.Count > 0)
                {
                    btnDuyet.Enabled = false;
                    if (DungChung.Ham.checkQuyenFalse(xtraDuyet.Name)[0])
                        btnHuyDuyet.Enabled = true;
                    else
                        btnHuyDuyet.Enabled = false;
                    id = Convert.ToInt32(_ltung.First().IDTamUng);
                    txtIDtamungtc.Text = id.ToString();
                    if (_ltung.First().NgayThu != null)
                        dtNgayTC.DateTime = _ltung.First().NgayThu.Value;
                    else
                        dtNgayTC.DateTime = DateTime.Now;
                    //lupBPThu.EditValue=_ltung.First()
                    lupCBTC.EditValue = _ltung.First().MaCB;
                    lupBPTC.EditValue = _ltung.First().MaKP;
                    if (_ltung.First().PhanLoai != null)
                    {
                        if (_ltung.First().PhanLoai.Value == 1)
                            radThuChi.SelectedIndex = 0;
                        else
                            radThuChi.SelectedIndex = 1;
                    }
                    else
                        radThuChi.SelectedIndex = -1;
                    if (_ltung.First().SoTien != null)
                    {
                        txtSoTienCP.Text = _ltung.First().SoTien.ToString();
                        //  txtBangChuCP.Text = DungChung.Ham.DocTienBangChu(_ltung.First().SoTien.Value, " đồng.");

                    }
                    else
                    {
                        txtSoTienCP.Text = "0";
                    }

                    txtSoTienTC.Text = _ltung.First().TienChenh.ToString();
                    //   txtBangChuTC.Text = DungChung.Ham.DocTienBangChu(_ltung.First().SoTien.Value, " đồng.");


                    txtNoiDungTC.Text = _ltung.First().LyDo;
                    cbo_Quyen.Text = _ltung.First().QuyenHD;
                    cboSoHD.Text = _ltung.First().SoHD;
                    if (_litamung.First().PhanLoai == 2)
                    {
                        btnTaoHD.Visible = false;
                        btnXemHDDuyet.Visible = false;
                        HuyHD.Visible = false;
                    }
                    else if (_litamung.First().PhanLoai == 1 && _litamung.First().SoTien > 0)
                    {
                        btnTaoHD.Visible = true;
                        HuyHD.Visible = false;
                        btnXemHDDuyet.Visible = true;

                        if (Bien.MaBV == "24009")
                        {
                            HuyHD.Visible = true;
                            if (_litamung.First().StatusHD == 2 || _litamung.First().StatusHD == 1)
                            {
                                btnTaoHD.Enabled = false;
                                HuyHD.Enabled = true;
                                btnXemHDDuyet.Enabled = true;
                            }

                            else
                            {
                                btnHuyDuyet.Enabled = true;
                                btnTaoHD.Enabled = true;
                                HuyHD.Enabled = false;
                                btnXemHDDuyet.Enabled = false;
                            }
                            if (_litamung.First().StatusHD == 4)
                            {
                                btnHuyDuyet.Enabled = true;
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(_litamung.First().MaHD) || !string.IsNullOrEmpty(_litamung.First().FkeyVNPT))
                            {
                                btnTaoHD.Enabled = false;
                                HuyHD.Enabled = true;
                                btnXemHDDuyet.Enabled = true;
                            }
                            else
                            {
                                btnTaoHD.Enabled = true;
                                HuyHD.Enabled = false;
                                btnXemHDDuyet.Enabled = false;
                            }
                        }
                    }
                    else
                    {
                        btnTaoHD.Visible = false;
                        HuyHD.Visible = false;
                        btnXemHDDuyet.Visible = false;
                    }
                }
                else
                {
                    if (DungChung.Ham.checkQuyenFalse(xtraDuyet.Name)[0])
                        btnDuyet.Enabled = true;
                    else
                        btnDuyet.Enabled = true;

                    btnTaoHD.Visible = false;
                    HuyHD.Visible = false;
                    btnTaoHD.Enabled = true;
                    btnXemHDDuyet.Visible = false;
                    btnXemHDDuyet.Enabled = false;

                    btnHuyDuyet.Enabled = false;
                    dtNgayTC.DateTime = System.DateTime.Now;
                    lupCBTC.EditValue = "";
                    lupBPTC.EditValue = "";
                    txtSoTienTC.Text = "";
                    txtBangChuTC.Text = "";
                    txtSoTienCP.Text = "";
                    txtBangChuCP.Text = "";
                    radThuChi.SelectedIndex = -1;
                    txtNoiDungTC.Text = "";
                    txtIDtamungtc.Text = "";
                    cbo_Quyen.Text = "";
                    cboSoHD.Text = "";

                }
                if (grvBNhantt.GetFocusedRowCellValue(colMaBNhan) != null)
                {
                    int _mabnhan = Convert.ToInt32(grvBNhantt.GetFocusedRowCellValue(colMaBNhan));
                    if (Bien.MaBV == "27183")
                    {
                        var _DtuongBN = _lTKbn.Where(p => p.MaBNhan == _mabnhan).Select(p => p.DTuong).FirstOrDefault();
                        if (_DTuong == "BHYT")
                        {
                            if (DungChung.Ham.checkQuyenFalse(xtraTToan.Name)[0])
                            {

                                btnXoa.Enabled = true;

                            }
                            else
                            {

                                btnXoa.Enabled = false;

                            }

                        }
                        else
                        {
                            btnXoa.Enabled = false;
                        }
                    }
                }
                #endregion
            }
            else
            #region thanh toán cho đơn kê ngoài
            {
                int idnhap = Convert.ToInt32(grvBNhantt.GetFocusedRowCellValue(colIDNhap));
                var _lnhapdct = (from ndct in _dataContext.NhapDcts.Where(p => p.IDNhap == idnhap)
                                 select new { madv = ndct.MaDV, donvi = ndct.DonVi, dongia = ndct.DonGia, soluong = ndct.SoLuongX, thanhtien = ndct.ThanhTienX, TrongBH = 0, TyLeTT = 100, XHH = 0, Mien = 0 }).ToList();
                grcThanhToan.DataSource = null;
                grcThanhToan.DataSource = _lnhapdct.Where(p => p.soluong > 0).ToList();
                var CheckTT = _dataContext.TamUngs.Where(p => p.IDNhapD == idnhap).ToList();
                if (CheckTT.Count > 0)
                {
                    btnThanhToan.Enabled = false;
                    btnXoa.Enabled = true;
                }
                else
                {
                    btnThanhToan.Enabled = true;
                    btnXoa.Enabled = false;
                }
                cboInBangKe.SelectedIndex = -1;
            }
            #endregion
            var test = (from a in _dataContext.BenhNhans.Where(p => p.MaBNhan == _mabn && (p.DTuong == "BHYT" || (p.DTuong == "Dịch vụ" && p.NoiTru == 0 && p.DTNT == false)))
                        join b in _dataContext.RaViens on a.MaBNhan equals b.MaBNhan
                        join c in _dataContext.VienPhis on a.MaBNhan equals c.MaBNhan
                        select new { a.MaBNhan, a.TenBNhan }).ToList();
            var dt = (from a in _dataContext.VienPhis.Where(p => p.MaBNhan == _mabn)
                      join b in _dataContext.VienPhicts on a.idVPhi equals b.idVPhi
                      join c in _dataContext.DichVus.Where(p => p.PLoai == 1).Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6) on b.MaDV equals c.MaDV
                      select new
                      {
                          a.MaBNhan,
                          TenDV = c.TenDV + "/ " + c.HamLuong,
                          b.DonVi,
                          b.SoLuong,
                          b.DonGia,
                          b.ThanhTien,
                          c.NuocSX,
                      }).ToList();
            if (test.Count > 0 && dt.Count > 0)
            {
                simpleButton3.Enabled = true;
            }
            else
            {
                simpleButton3.Enabled = false;
            }
            if (Bien.MaBV == "01071" || Bien.MaBV == "01049")
            {
                btnTaoHD.Visible = false;
            }
        }
        List<ThuThang> _lThuThangChuaTH = new List<ThuThang>();
        /// <summary>
        /// dùng để chọn dịch vụ thu thẳng được trả lại
        /// </summary>
        public class ThuThang
        {
            //MaKP = kq.Key.MaKP, kq.Key.TrongBH, madv = kq.Key.MaDV, dongia = kq.Key.DonGia, donvi = kq.Key.DonVi, soluong = kq.Sum(p => p.SoLuong), thanhtien = kq.Sum(p => p.ThanhTien) }).OrderBy(p => p.madv).ToList();
            public int MaKP { set; get; }
            public int MaDV { set; get; }
            public double DonGia { set; get; }
            public double SoLuong { set; get; }
            public double ThanhTien { set; get; }

        }

        #region Ktra ra viện
        private bool KTRavien(int _mabn)
        {
            var noitru = _dataContext.BenhNhans.Where(p => p.MaBNhan == _mabn).Select(p => p.NoiTru).ToList();
            if (noitru.Count > 0 && noitru.First() == 1)
            {
                var rv = _dataContext.RaViens.Where(p => p.MaBNhan == _mabn).ToList();
                if (rv.Count > 0)
                {
                    if (rv.First().Status == 3)
                    {
                        MessageBox.Show("Bệnh nhân chốn viện, bạn không thể thanh toán");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    MessageBox.Show("Bệnh nhân chưa làm thủ tục ra viện");
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
        #endregion

        //private static void updateStatus(int _mabn)
        //{
        //    QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);

        //    double thanhtien = 0;
        //    var q = (from vp in _dataContext.VienPhis.Where(p => p.MaBNhan == _mabn)
        //             join vpct in _dataContext.VienPhicts on vp.idVPhi equals vpct.idVPhi
        //             group new { vp, vpct } by vp.idVPhi into kq
        //             select new { kq.Key, ThanhTien = kq.Sum(p => p.vpct.TienBN) }).FirstOrDefault();
        //    if (q != null)
        //    {
        //        thanhtien = q.ThanhTien;
        //        if (thanhtien == 0)
        //        {
        //            VienPhi vp = _dataContext.VienPhis.Single(p => p.idVPhi == q.Key);
        //            vp.Status = 1;
        //            _dataContext.SaveChanges();
        //        }
        //    }

        //}
        public void _getNgayTachCP(DateTime ngaycp, bool tt2the)
        {
            _ngaytachcp = ngaycp;
            TT2The = tt2the;
        }

        DateTime _ngaytachcp = DateTime.Now;
        bool TT2The = false;
        string maCb1, tenCb1;
        private bool KiemtraCCHN(int _maBN)
        {
            bool kt = true;
            //var cchn = (from dt in _dataContext.DThuocs.Where(o => o.MaBNhan == _maBN)
            //            join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
            //            join cb in _dataContext.CanBoes on dtct.MaCB equals cb.MaCB
            //            group new { dt, dtct, cb } by new { cb } into kq
            //            select new
            //            {
            //                kq.Key.cb.MaCB,
            //                kq.Key.cb.TenCB,
            //                kq.Key.cb.MaCCHN
            //            }).ToList();

            var cchn = (from dt in _dataContext.DThuocs.Where(o => o.MaBNhan == _maBN)
                        join cb in _dataContext.CanBoes on dt.MaCB equals cb.MaCB
                        group new { dt, cb } by new { cb } into kq
                        select new
                        {
                            kq.Key.cb.MaCB,
                            kq.Key.cb.TenCB,
                            kq.Key.cb.MaCCHN
                        }).ToList();

            foreach (var item in cchn)
            {
                if (string.IsNullOrEmpty(item.MaCCHN)
                    || string.IsNullOrWhiteSpace(item.MaCCHN))
                {
                    maCb1 += item.MaCB + ", ";
                    tenCb1 += item.TenCB + ", ";

                    kt = false;
                }
            }

            if (!kt)
            {
                XtraMessageBox.Show("Hiện tại các cán bộ sau chưa có chứng chỉ hành nghề: \n Mã cán bộ: " + maCb1 + "\n Tên cán bộ:" + tenCb1 + "\n Bạn không thể thanh toán khi các bác sỹ chưa có chứng chỉ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                maCb1 = null;
                tenCb1 = null;
            }

            return kt;
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            _dataContext = new QLBVEntities(AppConfig.SqlConnection);

            bool TTBN = true;
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);

            bool kt = true;
            if (DungChung.Bien.MaBV == "24272")
            {
                var checkCLS = (from cls in _dataContext.CLS
                                join cd in _dataContext.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                join dv in _dataContext.DichVus on cd.MaDV equals dv.MaDV
                                where cls.MaBNhan == _int_maBN && cls.Status == 0
                                select new { cls.IdCLS, cd.MaDV, dv.TenDV }).ToList();
                string DVChuaTH = "";
                if (checkCLS.Count > 0)
                {
                    foreach (var a in checkCLS)
                    {
                        DVChuaTH += a.TenDV + ", ";
                    }
                    MessageBox.Show("Bệnh nhân có dịch vụ " + DVChuaTH + " chưa thực hiện, không thể thanh toán");
                    return;
                }
            }
            var ktkb = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).OrderBy(p => p.IDKB).ToList();
            if ((Bien.MaBV == "24012" || Bien.MaBV == "24389") && (Convert.ToDateTime(ktkb.First().NgayKham).AddMinutes(5) > dtNgayTT.DateTime))
            {
                string time = "Thời gian khám bệnh của bệnh nhân < 5 phút (" + (dtNgayTT.DateTime - Convert.ToDateTime(ktkb.First().NgayKham)).Minutes + "p) \nBạn có muốn tiếp tục?";
                DialogResult dr = MessageBox.Show(time, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                    return;
            }
            //var muondo = (from dthuoc in _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN && p.Status == 0 &&p.PLDV == 3) select new { dthuoc.IDDon }).ToList();
            //if (muondo.Count>0)
            //{
            //   if(MessageBox.Show("Bệnh nhân chưa trả tư trang, Bạn có muốn tiếp tục thanh toán","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.No)
            //    {
            //        return;
            //    }    
            //}    

            if (Bien.MaBV == "12345" || Bien.MaBV == "56789")
            {
                var qdt = _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN && p.PLDV == 1 && (p.SoVV == null || p.SoVV != -1)).ToList();
                if (qdt.Count > 0)
                {
                    MessageBox.Show("Bệnh nhân có đơn thuốc chưa được duyệt, không thể thanh toán");
                    kt = false;
                }
            }
            if (kt && KiemtraCCHN(_mabn))
            {
                if (ckcDonNgoai.Checked == true)
                {
                    TTBN = false;
                }

                if (TTBN)
                {

                    bool _sua = true;
                    _sua = DungChung.Ham.checkQuyen(xtraTToan.Name)[0];
                    if (_sua == false)
                    {
                        MessageBox.Show("Chức năng bị khóa");
                    }
                    #region
                    if (_sua)
                    {

                        int _makptt = 0;
                        if (lupBPKe.EditValue != null)
                        {
                            _makptt = Convert.ToInt32(lupBPKe.EditValue);
                        }

                        if (ktraGiaTT39(_dataContext, _int_maBN, dtNgayTT.DateTime))
                        {
                            if (Bien.MaBV == "27001" && DungChung.Ham.KTCBCongKhamBNKB(_dataContext, _int_maBN))
                            {
                                MessageBox.Show("Cán bộ khám không khớp nhau!", "Thông báo");
                                return;
                            }
                            if (DungChung.Ham.Ktra2The(_dataContext, _int_maBN))
                            {
                                QLBV.FormThamSo.frm_NgayTachCP2The frm = new QLBV.FormThamSo.frm_NgayTachCP2The(_int_maBN, dtNgayTT.DateTime);
                                frm.getdata = new QLBV.FormThamSo.frm_NgayTachCP2The.getValue(_getNgayTachCP);
                                frm.ShowDialog();
                                if (TT2The)
                                {
                                    if (DungChung.Ham.ThanhToan2The(_dataContext, _int_maBN, dtNgayTT.DateTime))
                                    {
                                        _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
                                        var vp1 = (from vp in _dataContext.VienPhis.Where(p => p.MaBNhan == _int_maBN)
                                                   join vpct in _dataContext.VienPhicts on vp.idVPhi equals vpct.idVPhi
                                                   select new { vpct.TBNCTT, vpct.TBNTT, vpct.ThanhToan, vpct.idVPhict }).ToList();
                                        double tienbn = vp1.Where(p => p.ThanhToan == 0).Sum(p => p.TBNTT) + vp1.Where(p => p.ThanhToan == 0).Sum(p => p.TBNCTT);
                                        if (tienbn > 0)
                                        {
                                            MessageBox.Show("Số tiền bệnh nhân phải trả: " + Math.Round(tienbn, 2, MidpointRounding.AwayFromZero));
                                        }
                                        btnXem_Click(sender, e);

                                        if (Bien.MaBV == "30007" || Bien.MaBV == "08104" || Bien.MaBV == "27001")
                                        {
                                            var bn = _dataContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN).ToList();
                                            if (bn.Count > 0 && bn.First().NoiTru == 0 && (bn.First().DTNT == false))
                                                btnDuyet_Click(sender, e);
                                        }
                                        int noingoai = CheckNoiNgoaiTru(_int_maBN);
                                        bool ktraNgoaiGio = false; // bệnh nhân ngoài giờ -30002 tự động gửi BHXH
                                        if (Bien.MaBV == "30002" || Bien.MaBV == "30009" || Bien.MaBV == "20001" || Bien.MaBV == "30303")
                                        {
                                            _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
                                            var vp = _dataContext.VienPhis.Where(p => p.MaBNhan == _int_maBN).ToList();
                                            if (vp.Count > 0 && vp.First().NgoaiGio == 1)
                                                ktraNgoaiGio = true;
                                        }
                                        var qchuyenvien = _dataContext.RaViens.Where(p => p.MaBNhan == _int_maBN && p.Status == 1).FirstOrDefault();
                                        if (Bien.MaBV == "26007" && qchuyenvien != null)
                                        {
                                            // updateExPort = true;
                                            List<DungChung.Cls79_80.cl_79_80> _l = new List<Cls79_80.cl_79_80>();
                                            _l.Add(new DungChung.Cls79_80.cl_79_80 { Ma_bn = _int_maBN, Export = false });
                                            BHYT.us_Export_XML_2348.user = Bien.xmlFilePath_LIS[10];
                                            BHYT.us_Export_XML_2348.user = Bien.xmlFilePath_LIS[11];
                                            BHYT.us_Export_XML_2348 clsBHXH = new BHYT.us_Export_XML_2348();
                                            clsBHXH._updateExPort(_dataContext, _l, _int_maBN, false, 1);
                                        }
                                        else if ((noingoai == 0 && (Bien.PPXuat_BHXH[0] == 4 || Bien.PPXuat_BHXH[0] == 5)) || (noingoai == 1 && (Bien.PPXuat_BHXH[1] == 4 || Bien.PPXuat_BHXH[1] == 5)) || ktraNgoaiGio)
                                        {
                                            // updateExPort = true;
                                            _lDSBNGui.Add(new MyObject { MaBNhan = _int_maBN, NgayTT = DateTime.Now });
                                        }

                                        if (Bien.MaBV == "12345" || Bien.MaBV == "24297")
                                        {
                                            btnDuyet_Click(null, null);
                                        }
                                    }
                                    else
                                    {
                                        //MessageBox.Show("Lỗi!");
                                    }
                                }
                                else
                                {
                                    if (DungChung.Ham.ThanhToan(_dataContext, _int_maBN, dtNgayTT.DateTime, _makptt))
                                    {
                                        _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
                                        var vp1 = (from vp in _dataContext.VienPhis.Where(p => p.MaBNhan == _int_maBN)
                                                   join vpct in _dataContext.VienPhicts on vp.idVPhi equals vpct.idVPhi
                                                   select new { vpct.TBNCTT, vpct.TBNTT, vpct.ThanhToan, vpct.idVPhict }).ToList();
                                        double tienbn = vp1.Where(p => p.ThanhToan == 0).Sum(p => p.TBNTT) + vp1.Where(p => p.ThanhToan == 0).Sum(p => p.TBNCTT);
                                        if (tienbn > 0)
                                        {
                                            MessageBox.Show("Số tiền bệnh nhân phải trả: " + tienbn.ToString("###,###.00"));
                                        }
                                        btnXem_Click(sender, e);
                                        if (Bien.MaBV == "30007" || Bien.MaBV == "08104" || Bien.MaBV == "27001")
                                        {
                                            var bn = _dataContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN).ToList();
                                            if (bn.Count > 0 && bn.First().NoiTru == 0 && (bn.First().DTNT == false))
                                                btnDuyet_Click(sender, e);
                                        }
                                        int noingoai = CheckNoiNgoaiTru(_int_maBN);
                                        bool ktraNgoaiGio = false; // bệnh nhân ngoài giờ -30002 tự động gửi BHXH
                                        if (Bien.MaBV == "30002" || Bien.MaBV == "30009" || Bien.MaBV == "20001" || Bien.MaBV == "30303")
                                        {
                                            _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
                                            var vp = _dataContext.VienPhis.Where(p => p.MaBNhan == _int_maBN).ToList();
                                            if (vp.Count > 0 && vp.First().NgoaiGio == 1)
                                                ktraNgoaiGio = true;

                                        }
                                        var qchuyenvien = _dataContext.RaViens.Where(p => p.MaBNhan == _int_maBN && p.Status == 1).FirstOrDefault();
                                        if (Bien.MaBV == "26007" && qchuyenvien != null)
                                        {
                                            List<DungChung.Cls79_80.cl_79_80> _l = new List<Cls79_80.cl_79_80>();
                                            _l.Add(new DungChung.Cls79_80.cl_79_80 { Ma_bn = _int_maBN, Export = false });
                                            BHYT.us_Export_XML_2348.user = Bien.xmlFilePath_LIS[10];
                                            BHYT.us_Export_XML_2348.user = Bien.xmlFilePath_LIS[11];
                                            BHYT.us_Export_XML_2348 clsBHXH = new BHYT.us_Export_XML_2348();
                                            clsBHXH._updateExPort(_dataContext, _l, _int_maBN, false, 1);
                                        }
                                        else if ((noingoai == 0 && (Bien.PPXuat_BHXH[0] == 4 || Bien.PPXuat_BHXH[0] == 5)) || (noingoai == 1 && (Bien.PPXuat_BHXH[1] == 4 || Bien.PPXuat_BHXH[1] == 5)) || ktraNgoaiGio)
                                        {
                                            if (DungChung.Bien.MaBV != "27001")
                                            {
                                                _lDSBNGui.Add(new MyObject { MaBNhan = _int_maBN, NgayTT = DateTime.Now });
                                                worker.RunWorkerAsync();
                                            }
                                            else
                                            {
                                                List<DungChung.Cls79_80.cl_79_80> _listVPBH = new List<DungChung.Cls79_80.cl_79_80>();
                                                _listVPBH.Add(new DungChung.Cls79_80.cl_79_80 { Ma_bn = _int_maBN, Export = false });
                                                BHYT.us_Export_XML_2348.user = Bien.xmlFilePath_LIS[10];
                                                BHYT.us_Export_XML_2348.user = Bien.xmlFilePath_LIS[11];
                                                BHYT.us_Export_XML_2348 clsBHXH = new BHYT.us_Export_XML_2348();
                                                clsBHXH._updateExPort(_dataContext, _listVPBH, _int_maBN, false, 1);
                                            }
                                        }

                                        if (Bien.MaBV == "12345" || Bien.MaBV == "24297")
                                        {
                                            btnDuyet_Click(null, null);
                                        }
                                    }
                                    else
                                    {
                                        //MessageBox.Show("Lỗi!");
                                    }
                                }
                            }
                            else
                            {
                                if (DungChung.Ham.ThanhToan(_dataContext, _int_maBN, dtNgayTT.DateTime, _makptt))
                                {
                                    _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
                                    var vp1 = (from vp in _dataContext.VienPhis.Where(p => p.MaBNhan == _int_maBN)
                                               join vpct in _dataContext.VienPhicts on vp.idVPhi equals vpct.idVPhi
                                               select new { vpct.TBNCTT, vpct.TBNTT, vpct.ThanhToan, vpct.idVPhict }).ToList();
                                    double tienbn = vp1.Where(p => p.ThanhToan == 0).Sum(p => p.TBNTT) + vp1.Where(p => p.ThanhToan == 0).Sum(p => p.TBNCTT);
                                    if (tienbn > 0)
                                    {
                                        MessageBox.Show("Số tiền bệnh nhân phải trả: " + tienbn.ToString("###,###.00"));
                                    }
                                    btnXem_Click(sender, e);
                                    if (Bien.MaBV == "30007" || Bien.MaBV == "08104" || Bien.MaBV == "27001")
                                    {
                                        var bn = _dataContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN).ToList();
                                        if (bn.Count > 0 && bn.First().NoiTru == 0 && (bn.First().DTNT == false))
                                            btnDuyet_Click(sender, e);
                                    }
                                    int noingoai = CheckNoiNgoaiTru(_int_maBN);
                                    bool ktraNgoaiGio = false; // bệnh nhân ngoài giờ -30002 tự động gửi BHXH
                                    if (Bien.MaBV == "30002" || Bien.MaBV == "30009" || Bien.MaBV == "20001" || Bien.MaBV == "30303")
                                    {
                                        _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
                                        var vp = _dataContext.VienPhis.Where(p => p.MaBNhan == _int_maBN).ToList();
                                        if (vp.Count > 0 && vp.First().NgoaiGio == 1)
                                            ktraNgoaiGio = true;

                                    }
                                    var qchuyenvien = _dataContext.RaViens.Where(p => p.MaBNhan == _int_maBN && p.Status == 1).FirstOrDefault();
                                    if (Bien.MaBV == "26007" && qchuyenvien != null)
                                    {
                                        List<DungChung.Cls79_80.cl_79_80> _l = new List<Cls79_80.cl_79_80>();
                                        _l.Add(new DungChung.Cls79_80.cl_79_80 { Ma_bn = _int_maBN, Export = false });
                                        BHYT.us_Export_XML_2348.user = Bien.xmlFilePath_LIS[10];
                                        BHYT.us_Export_XML_2348.user = Bien.xmlFilePath_LIS[11];
                                        BHYT.us_Export_XML_2348 clsBHXH = new BHYT.us_Export_XML_2348();
                                        clsBHXH._updateExPort(_dataContext, _l, _int_maBN, false, 1);
                                    }
                                    else if ((noingoai == 0 && (Bien.PPXuat_BHXH[0] == 4 || Bien.PPXuat_BHXH[0] == 5)) || (noingoai == 1 && (Bien.PPXuat_BHXH[1] == 4 || Bien.PPXuat_BHXH[1] == 5)) || ktraNgoaiGio)
                                    {
                                        if (DungChung.Bien.MaBV == "27001")
                                        {
                                            List<DungChung.Cls79_80.cl_79_80> _listVPBH = new List<DungChung.Cls79_80.cl_79_80>();
                                            _listVPBH.Add(new DungChung.Cls79_80.cl_79_80 { Ma_bn = _int_maBN, Export = false });
                                            BHYT.us_Export_XML_2348.user = Bien.xmlFilePath_LIS[10];
                                            BHYT.us_Export_XML_2348.user = Bien.xmlFilePath_LIS[11];
                                            BHYT.us_Export_XML_2348 clsBHXH = new BHYT.us_Export_XML_2348();
                                            clsBHXH._updateExPort(_dataContext, _listVPBH, _int_maBN, false, 1);
                                        }
                                        else
                                        {
                                            updateExPort = true;
                                            _lDSBNGui.Add(new MyObject { MaBNhan = _int_maBN, NgayTT = DateTime.Now });
                                            worker.RunWorkerAsync();
                                        }    
                                    }
                                    if (Bien.MaBV == "12345" || Bien.MaBV == "24297")
                                    {
                                        btnDuyet_Click(null, null);
                                    }
                                }
                                else
                                {
                                    //MessageBox.Show("Lỗi!");
                                }
                            }
                        }
                    }// kết thúc kiểm tra ma bn
                    #endregion
                }
                else
                {

                    int _idnhap = 0;
                    if (grvBNhantt.GetFocusedRowCellValue(colIDNhap) != null)
                    {
                        _idnhap = Convert.ToInt32(grvBNhantt.GetFocusedRowCellValue(colIDNhap));
                        NhapD _lnhapd = _dataContext.NhapDs.Where(p => p.IDNhap == _idnhap).FirstOrDefault();
                        List<NhapDct> _lnhapdct = _dataContext.NhapDcts.Where(p => p.IDNhap == _idnhap).ToList();
                        if (_lnhapd != null)
                        {
                            if (dtNgayTT.DateTime > _lnhapd.NgayNhap.Value)
                            {
                                double SoTien = _lnhapdct.Count() > 0 ? _lnhapdct.Sum(p => p.ThanhTienX) : 0;
                                DialogResult hoitt = MessageBox.Show("Số tiền thanh toán: " + SoTien.ToString("###,###.00"), "Hỏi thu", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                                if (hoitt == DialogResult.OK)
                                {
                                    var a = QuyenSoBL._getQuyen_SoBL(1, "");
                                    if (a != null)
                                    {
                                        double sotien = _lnhapdct.Count() > 0 ? _lnhapdct.Sum(p => p.ThanhTienX) : 0;
                                        var maCB = lupNguoiTT.EditValue != null ? Convert.ToString(lupNguoiTT.EditValue) : "";
                                        var ngayThu = dtNgayTT.DateTime;
                                        TamUng moi = new TamUng();
                                        moi.LyDo = _lnhapd.GhiChu;
                                        moi.SoTien = sotien;
                                        moi.MaCB = lupNguoiTT.EditValue != null ? Convert.ToString(lupNguoiTT.EditValue) : null;
                                        moi.NgayThu = ngayThu;
                                        moi.PhanLoai = 1;
                                        moi.QuyenHD = a.FirstOrDefault().Quyen;
                                        moi.SoHD = a.FirstOrDefault().So != null ? (a.FirstOrDefault().So + 1).ToString() : "";
                                        moi.TienChenh = _lnhapdct.Count() > 0 ? _lnhapdct.Sum(p => p.ThanhTienX) : 0;
                                        moi.NgoaiGio = Convert.ToInt32(chkNgoaiGio.Checked);
                                        moi.IDNhapD = _idnhap;
                                        _dataContext.TamUngs.Add(moi);
                                        if (_dataContext.SaveChanges() >= 0)
                                        {
                                            InPhieuThuThuocNgoai(_dataContext, _idnhap);
                                            //BaoCao.rep_PhieuThuChi_TT107 rep = new BaoCao.rep_PhieuThuChi_TT107();
                                            //rep.TieuDe.Value = "PHIẾU THU";
                                            //rep.xrTableCell11.Text = "Họ và tên người nộp tiền: ";
                                            //rep.NguoiNop.Value = "NGƯỜI NỘP";
                                            //rep.NguoiNhan.Value = "THỦ QUỸ";
                                            //rep.xrTableCell56.Text = _lnhapd.TenNguoiCC;
                                            //rep.xrTableCell2.Text = "Mẫu số: C40-BB";
                                            //rep.xrTableCell72.Text = "Mẫu số: C40-BB";
                                            //rep.So.Value = "Số: " + moi.SoHD;
                                            //rep.QuyenSo.Value = "Quyển số: " + moi.QuyenHD;
                                            //rep.No.Value = "Nợ:";
                                            //rep.Co.Value = "Có:";
                                            //rep.SubBand1.Visible = false;
                                            //rep.SubBand2.Visible = true;
                                            //rep.clMaBNhan.Text = "Mã BN: ";// + pt.MaBNhan;
                                            //rep.HoTen.Value = _lnhapd.TenNguoiCC.ToUpper();
                                            //rep.DChi.Value = _lnhapd.DiaChi;
                                            //rep.NoiDung.Value = _lnhapd.GhiChu;
                                            //string[] ar = Bien.FormatString[1].Split(';');
                                            //rep.SoTien.Value = (_lnhapdct.Count() > 0 ? _lnhapdct.Sum(p => p.ThanhTienX) : 0).ToString("###,###.00");//(pt.PhanLoai == 3 ? (pt.SoTien ?? 0.0).ToString(ar[0].Substring(3)) : (_mauin == 0 ? pt.TienChenh.ToString(ar[0].Substring(3)) : (pt.SoTien ?? 0.0).ToString(ar[0].Substring(3)))) + " VNĐ";
                                            //rep.SoThanhChu.Value = DungChung.Ham.DocTienBangChu((_lnhapdct.Count() > 0 ? _lnhapdct.Sum(p => p.ThanhTienX) : 0), "đồng");
                                            //rep.NguoiLap.Value = lupNguoiTT.Text;
                                            //rep.NgayThang.Value = "Ngày " + dtNgayThu.DateTime.Day + " tháng " + dtNgayThu.DateTime.Month + " năm " + dtNgayThu.DateTime.Year;
                                            //rep.CreateDocument();
                                            //frmIn frm = new frmIn();
                                            //frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                            //frm.ShowDialog();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ngày Thanh toán không được nhỏ hơn ngày xuất: " + _lnhapd.NgayNhap.ToString());
                            }
                            grvBNhantt_FocusedRowChanged(null, null);
                        }
                    }
                }
                var test = (from a in _dataContext.BenhNhans.Where(p => p.MaBNhan == _mabn && (p.DTuong == "BHYT" || (p.DTuong == "Dịch vụ" && p.NoiTru == 0 && p.DTNT == false)))
                            join b in _dataContext.RaViens on a.MaBNhan equals b.MaBNhan
                            join c in _dataContext.VienPhis on a.MaBNhan equals c.MaBNhan
                            select new { a.MaBNhan, a.TenBNhan }).ToList();

                var dt = (from a in _dataContext.VienPhis.Where(p => p.MaBNhan == _mabn)
                          join b in _dataContext.VienPhicts on a.idVPhi equals b.idVPhi
                          join c in _dataContext.DichVus.Where(p => p.PLoai == 1).Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6) on b.MaDV equals c.MaDV
                          select new
                          {
                              a.MaBNhan,
                              TenDV = c.TenDV + "/ " + c.HamLuong,
                              b.DonVi,
                              b.SoLuong,
                              b.DonGia,
                              b.ThanhTien,
                              c.NuocSX,
                          }).ToList();

                if (test.Count > 0 && dt.Count > 0)
                {
                    simpleButton3.Enabled = true;
                }
                else
                {
                    simpleButton3.Enabled = false;
                }

                var _bnkb = _dataContext.BNKBs.Where(x => x.MaBNhan == _mabn).OrderByDescending(s => s.NgayKham).FirstOrDefault();
                if (_bnkb.PhuongAn == 0)
                {
                    usTamThu_TToan_Load(sender, e);
                    return;
                }

                usTamThu_TToan_Load(sender, e);
            }
        }
        
        private bool ktraGiaTT39(QLBV_Database.QLBVEntities _data, int _int_maBN, DateTime ngaytt)
        {
            bool kt = true;
            var qbn = _data.BenhNhans.Where(p => p.MaBNhan == _int_maBN).FirstOrDefault();
            var qvv = _data.VaoViens.Where(p => p.MaBNhan == _int_maBN).FirstOrDefault();
            var qdichvu = _data.DichVus.Where(p => p.PLoai == 2).ToList();
            List<int> lIDCD = new List<int>();
            if (qbn != null && ngaytt >= Bien.ngayGiaMoiTT39 && Bien.ngayGiaMoiTT39 > new DateTime(2000, 01, 01))
            {

                if (qbn.NNhap != null && qbn.DTuong == "BHYT")
                {
                    string ms = "";
                    if ((qbn.NNhap.Value >= Bien.ngayGiaMoiTT39 && qvv == null) || (qvv != null && qvv.NgayVao >= Bien.ngayGiaMoiTT39))
                    {
                        var qcls = (from cls in _data.CLS.Where(p => p.MaBNhan == _int_maBN)
                                    join cd in _data.ChiDinhs.Where(p => p.TrongBH != null && p.TrongBH == 1) on cls.IdCLS equals cd.IdCLS
                                    select new { cls.IdCLS, cls.NgayThang, cd.MaDV, cd.DonGia, cd.IDCD }).ToList();

                        foreach (var a in qcls)
                        {
                            var qdv = qdichvu.Where(p => p.MaDV == a.MaDV).FirstOrDefault();
                            if (qdv != null)
                            {
                                if (a.DonGia != qdv.DonGiaTT39)
                                {
                                    ms = ms + qdv.MaDV + "-" + qdv.TenDV + " , ";
                                    lIDCD.Add(a.IDCD);
                                }
                            }
                        }

                        var qdthuoc = (from dt in _data.DThuocs.Where(p => p.MaBNhan == _int_maBN)
                                       join dtct in _data.DThuoccts.Where(p => p.TrongBH != null && p.TrongBH == 1) on dt.IDDon equals dtct.IDDon
                                       join dichvu in _data.DichVus.Where(p => p.PLoai == 2) on dtct.MaDV equals dichvu.MaDV
                                       select new { dt.NgayKe, dt.IDDon, dtct.IDDonct, dtct.MaDV, dtct.DonGia, dtct.IDCD, dtct.SoLuong, dtct.TyLeTT }).ToList();

                        foreach (var a in qdthuoc)
                        {
                            var qdv = qdichvu.Where(p => p.MaDV == a.MaDV).FirstOrDefault();
                            if (qdv != null)
                            {
                                if (a.DonGia != qdv.DonGiaTT39)
                                {

                                    if (a.IDCD == null)
                                    {
                                        ms = ms + qdv.MaDV + "-" + qdv.TenDV + " , ";
                                    }
                                    else
                                    {
                                        if (lIDCD.Count == 0 || !lIDCD.Contains(a.IDCD ?? 0))
                                        {
                                            ms = ms + qdv.MaDV + "-" + qdv.TenDV + " , ";
                                        }
                                    }


                                }
                            }
                        }
                        if (ms != "")
                        {
                            MessageBox.Show("Các dịch vụ: " + ms + " chưa được cập nhật về giá TT39");
                            kt = false;
                        }
                    }
                }
            }

            return kt;

        }

        void InPhieuThuThuocNgoai(QLBV_Database.QLBVEntities data, int idnhapd)
        {
            var _lnhapd = data.NhapDs.Where(p => p.IDNhap == idnhapd).FirstOrDefault();
            if (_lnhapd != null)
            {
                var _lTamUng = data.TamUngs.Where(p => p.IDNhapD == idnhapd).FirstOrDefault();
                if (_lTamUng != null)
                {
                    if (Bien.MaBV == "12345" || Bien.MaBV == "24297")
                    {
                        //var _ldv = data.DichVus.Where(p => p.PLoai == 1).ToList();
                        //var _lndct = data.NhapDcts.Where(p => p.IDNhap == idnhapd).ToList();
                        //var _lkq = (from ndct in _lndct
                        //            join dv in _ldv on ndct.MaDV equals dv.MaDV
                        //            select new { dv.MaDV, dv.TenDV, dv.DonVi, ndct.DonGiaX, ndct.ThanhTienX, ndct.SoLuongX }).ToList();

                        BaoCao.rep_phieuthu01071_A5 rep = new BaoCao.rep_phieuthu01071_A5(idnhapd);
                        rep.CreateDocument();
                        frmIn frm = new frmIn();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                    {
                        BaoCao.rep_PhieuThuChi_TT107 rep = new BaoCao.rep_PhieuThuChi_TT107();
                        rep.TieuDe.Value = "PHIẾU THU";
                        rep.xrTableCell11.Text = "Họ và tên người nộp tiền: ";
                        rep.Nguoi11.Value = "NGƯỜI NỘP";
                        rep.NguoiNop.Value = "NGƯỜI NỘP";
                        rep.NguoiNhan.Value = "THỦ QUỸ";
                        rep.xrTableCell56.Text = _lnhapd.TenNguoiCC;
                        rep.xrTableCell2.Text = "Mẫu số: C40-BB";
                        rep.xrTableCell72.Text = "Mẫu số: C40-BB";
                        rep.So.Value = "Số: " + _lTamUng.SoHD;
                        rep.QuyenSo.Value = "Quyển số: " + _lTamUng.QuyenHD;
                        rep.No.Value = "Nợ:";
                        rep.Co.Value = "Có:";
                        rep.SubBand1.Visible = false;
                        rep.SubBand2.Visible = true;
                        rep.clMaBNhan.Text = "Mã BN: ";// + pt.MaBNhan;
                        rep.HoTen.Value = _lnhapd.TenNguoiCC.ToUpper();
                        rep.DChi.Value = _lnhapd.DiaChi;
                        rep.NoiDung.Value = _lnhapd.GhiChu;
                        string[] ar = Bien.FormatString[1].Split(';');
                        rep.SoTien.Value = _lTamUng.SoTien.Value.ToString("###,###.00");//(pt.PhanLoai == 3 ? (pt.SoTien ?? 0.0).ToString(ar[0].Substring(3)) : (_mauin == 0 ? pt.TienChenh.ToString(ar[0].Substring(3)) : (pt.SoTien ?? 0.0).ToString(ar[0].Substring(3)))) + " VNĐ";
                        rep.SoThanhChu.Value = DungChung.Ham.DocTienBangChu(_lTamUng.SoTien.Value, "đồng");
                        rep.NguoiLap.Value = lupNguoiTT.Text;
                        rep.NgayThang.Value = "Ngày " + _lTamUng.NgayThu.Value.Day + " tháng " + _lTamUng.NgayThu.Value.Month + " năm " + _lTamUng.NgayThu.Value.Year;
                        rep.CreateDocument();
                        frmIn frm = new frmIn();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                }
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            maBNGui = 0;
            updateExPort = false;
        }
        //BackgroundWorker worker = new BackgroundWorker();

        /// <summary>
        /// false: Không gửi;
        /// True: cho phép gửi
        /// </summary>
        bool updateExPort = false;
        int maBNGui = 0;

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {


            maBNGui = Convert.ToInt32(txtMaBNhan.Text);
            //Thread.Sleep(30000);

            List<DungChung.Cls79_80.cl_79_80> _listVPBH = new List<DungChung.Cls79_80.cl_79_80>();
            _listVPBH.Add(new DungChung.Cls79_80.cl_79_80 { Ma_bn = maBNGui, Export = false });
            BHYT.us_Export_XML_2348.user = Bien.xmlFilePath_LIS[10];
            BHYT.us_Export_XML_2348.user = Bien.xmlFilePath_LIS[11];
            BHYT.us_Export_XML_2348 clsBHXH = new BHYT.us_Export_XML_2348();
            clsBHXH._updateExPort(_dataContext, _listVPBH, maBNGui, false, 1);

            //List<DungChung.Cls79_80.cl_79_80> _listVPBH = new List<DungChung.Cls79_80.cl_79_80>();
            //_listVPBH.Add(new DungChung.Cls79_80.cl_79_80 { Ma_bn = _int_maBN, Export = false });
            //BHYT.us_Export_XML_2348.user = Bien.xmlFilePath_LIS[10];
            //BHYT.us_Export_XML_2348.user = Bien.xmlFilePath_LIS[11];
            //BHYT.us_Export_XML_2348 clsBHXH = new BHYT.us_Export_XML_2348();
            //clsBHXH._updateExPort(_dataContext, _listVPBH, _int_maBN, false, 1);

        }
        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            if (ckcDonNgoai.Checked == true)
            {
                TimKiemDonThuoc();
            }
            else
                TimKiem();
        }

        private void dtTimTuNgay_Leave(object sender, EventArgs e)
        {
            if (ckcDonNgoai.Checked == true)
            {
                TimKiemDonThuoc();
            }
            else
                TimKiem();
        }

        private void dtTimDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (ckcDonNgoai.Checked == true)
            {
                TimKiemDonThuoc();
            }
            else
                TimKiem();
        }

        private void lupTimMaKP_EditValueChanged(object sender, EventArgs e)
        {
            if (ckcDonNgoai.Checked == true)
            {
                TimKiemDonThuoc();
            }
            else
                TimKiem();
        }

        private void cboTimTT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ckcDonNgoai.Checked == true)
            {
                TimKiemDonThuoc();
            }
            else
            {
                TimKiem();
                if (cboTimTT.SelectedIndex == 0)
                {
                    dtTimDenNgay.Enabled = true;
                    dtTimTuNgay.Enabled = true;
                    labKhoaPhong.Visible = false;
                    lupTimMaKP.Visible = false;
                    chkNgoaiH.Visible = false;
                }
                else
                {
                    dtTimDenNgay.Enabled = true;
                    dtTimTuNgay.Enabled = true;
                    labKhoaPhong.Visible = true;
                    lupTimMaKP.Visible = false;
                    chkNgoaiH.Visible = false;
                    if (cboTimTT.SelectedIndex == 1 || cboTimTT.SelectedIndex == 3)
                    {
                        lupTimMaKP.Visible = true;
                        chkNgoaiH.Visible = true;
                    }
                }
            }
        }

        private void txtTimKiem_Click(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "Nhập tên|Mã số|Số thẻ BN")
                txtTimKiem.Text = "";
        }
        string _lydoLodg = "";
        public void _getLyDoLog(string a)
        {
            _lydoLodg = a;
        }
        public void _getValue(bool a)
        {
            _ktmatkhau = a;
        }
        public bool _ktmatkhau = false;
        public bool ktraCLSKSK(QLBV_Database.QLBVEntities _dataContext, int _soPhieu)
        {
            var tu = _dataContext.TamUngs.Where(p => p.IDTamUng == _soPhieu).FirstOrDefault();
            if (tu != null)
            {
                if (tu.PhanLoai == 3 && tu.IDGoiDV != null && tu.IDGoiDV > 0 && Bien.MaBV == "12345" || Bien.MaBV == "24297" || Bien.MaBV == "56789")
                {
                    int Mabn = tu.MaBNhan ?? 0;
                    int IdGoi = tu.IDGoiDV ?? 0;
                    var ktrcd = (from cls in _dataContext.CLS.Where(p => p.MaBNhan == Mabn)
                                 join cd in _dataContext.ChiDinhs.Where(p => p.Status == 1 && p.IDGoi == IdGoi) on cls.IdCLS equals cd.IdCLS
                                 select cd).ToList();
                    if (ktrcd.Count() > 0)
                    {
                        return false;
                    }
                    else
                    {
                        var ktck = (from dt in _dataContext.DThuocs.Where(p => p.PLDV == 2 && p.MaBNhan == Mabn)
                                    join dtct in _dataContext.DThuoccts.Where(p => p.ThanhToan == 1) on dt.IDDon equals dtct.IDDon
                                    join dv in _dataContext.DichVus on dtct.MaDV equals dv.MaDV
                                    join tn in _dataContext.TieuNhomDVs.Where(p => p.IDNhom == 13) on dv.IdTieuNhom equals tn.IdTieuNhom
                                    select dtct);
                        if (ktck.Count() > 0)
                            return false;
                    }
                }
            }
            return true;
        }
        private void btnXoaKb_Click(object sender, EventArgs e)
        {
            int _soPhieu = Convert.ToInt32(txtIdTamUng.Text);
            var tk = _dataContext.ADMINs.FirstOrDefault(o => o.TenDN == Bien.TenDN);
            if (Bien.MaBV == "12345" || Bien.MaBV == "24297" || Bien.MaBV == "24297")
            {
                // Kiểm tra phiếu thu đã được duyệt chưa
                var checkDuyet = _dataContext.HTHONGs.First().TuDongDuyet ?? false;
                if (checkDuyet == false)
                {
                    var pt = _dataContext.TamUngs.Where(p => p.IDTamUng == _soPhieu).First().DuyetPhieuThu ?? false;
                    if (pt)
                    {
                        XtraMessageBox.Show("Phiếu thu đã được duyệt bạn không thể xóa phiếu thu.\n Yêu cầu hủy duyệt phiếu thu trước khi xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

            }

            if (Bien.MaBV == "30372" || Bien.MaBV == "12345")
            {
                var xuatDuoc = (from tu in _dataContext.TamUngs.Where(o => o.IDTamUng == _soPhieu)
                                join tuct in _dataContext.TamUngcts on tu.IDTamUng equals tuct.IDTamUng
                                join dtct in _dataContext.DThuoccts on tuct.IDDonct equals dtct.IDDonct
                                select dtct).ToList();
                if (xuatDuoc != null && xuatDuoc.Exists(o => o.Status == 1))
                {
                    MessageBox.Show("Phiếu có thuốc đã xuất dược không thể xóa", "Thông báo");
                    return;
                }
            }

            if (Bien.MaBV == "56789" && tk != null && tk.CapDo != 9)
            {
                MessageBox.Show("Tài khoản không có quyền xóa tạm thu");
                return;
            }

            bool _sua = true;
            _sua = DungChung.Ham.checkQuyen(xtraTThu.Name)[2];
            if (_sua)
            {
                if (!string.IsNullOrEmpty(txtMaBNhan.Text))
                {
                    if (!string.IsNullOrEmpty(txtIdTamUng.Text))
                    {

                        int mabn = Convert.ToInt32(txtMaBNhan.Text);
                        List<TamUng> _ltung = _dataContext.TamUngs.Where(p => p.IDTamUng == _soPhieu).ToList();
                        if (_ltung.Count > 0 && !string.IsNullOrWhiteSpace(_ltung.First().transactionID))
                        {
                            MessageBox.Show("Đã tạo hóa đơn không thể xóa", "Thông báo");
                            return;
                        }

                        var tamUng = _dataContext.TamUngs.FirstOrDefault(o => o.IDTamUng == _soPhieu);
                        if (tamUng.LoaiThanhToan == 1)
                        {
                            MessageBox.Show("Không thể xóa phiếu thu ngân hàng!");
                            return;
                        }
                        if (DungChung.Bien.MaBV != "30372")
                        {
                            if ((tamUng.PhanLoai == 0 || tamUng.PhanLoai == 1 || tamUng.PhanLoai == 3) && Bien.MaBV == "30372" && CheckPL1(mabn))
                            {
                                MessageBox.Show("Không cho phép xóa phiếu!");
                                return;
                            }
                        }
                        bool ThanhToan = true;
                        var qtt = _dataContext.VienPhis.Where(p => p.MaBNhan == mabn).FirstOrDefault();
                        if (qtt != null)
                        {
                            if (Bien.MaBV == "30010")
                            {
                                if (_ltung.First().PhanLoai != 0)
                                    ThanhToan = false;
                            }
                            else
                            {
                                ThanhToan = false;
                            }
                        }
                        if (ThanhToan)
                        {
                            if (_ktThuCLS(_dataContext, _soPhieu))
                            {
                                if (_ltung.Where(p => p.Status == true).ToList().Count > 0)
                                {
                                    MessageBox.Show("Dịch vụ CLS đã được thực hiện, bạn không thể xóa!\n Liên hệ người quản lý nếu muốn xóa.", "Thông báo");
                                    _sua = false;
                                }
                                else if ((Bien.MaBV == "12345" || Bien.MaBV == "24297" || Bien.MaBV == "56789") && !ktraCLSKSK(_dataContext, _soPhieu))
                                {
                                    MessageBox.Show("Dịch vụ CLS đã được thực hiện, bạn không thể xóa!\n Liên hệ người quản lý nếu muốn xóa.", "Thông báo");
                                    _sua = false;
                                }
                                else
                                {
                                    _ktmatkhau = false;
                                    ChucNang.frm_CheckPass frm = new ChucNang.frm_CheckPass();
                                    frm.ok = new ChucNang.frm_CheckPass._getdata(_getValue);
                                    frm.ShowDialog();
                                    if (_ktmatkhau)
                                    {


                                        foreach (var item in _ltung)
                                        {
                                            item.Status = false;
                                            _dataContext.SaveChanges();

                                        }

                                    }
                                    else
                                    {
                                        _sua = false;

                                    }
                                }
                            }
                            bool xoaTamUng01830 = false;
                            if (Bien.MaBV == "01830" && _ltung.Where(p => p.PhanLoai == 0).ToList().Count > 0) //a quý y/c 15-03
                            {
                                string macbthuold = "";
                                macbthuold = _ltung.Where(p => p.PhanLoai == 0).Select(p => p.MaCB).FirstOrDefault();
                                //if (lupNguoiThu.EditValue != null)
                                //    macbthu = lupNguoiThu.EditValue.ToString();
                                if (Bien.MaCB == macbthuold)
                                {
                                    _sua = true;
                                    xoaTamUng01830 = true;
                                }
                                else
                                {
                                    MessageBox.Show("Chứng từ tạm thu không thể xóa, chỉ có thể sửa số tiền về 0 đồng");
                                    _sua = false;
                                }
                            }
                            if (_sua)
                            {
                                if (KtraTCTT(Convert.ToInt32(txtMaBNhan.Text), 2))
                                {
                                    int id = int.Parse(txtIdTamUng.Text);
                                    DialogResult _result = DialogResult.No;
                                    if (xoaTamUng01830)
                                        _result = MessageBox.Show("Bạn muốn sửa số tiền tạm thu BN: " + txtTenBenhNhan.Text + " thành 0 đồng ?", "xóa tạm thu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    else
                                        _result = MessageBox.Show("Bạn muốn xóa tạm thu BN: " + txtTenBenhNhan.Text, "xóa tạm thu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (_result == DialogResult.Yes)
                                    {
                                        if (Bien.MaBV == "01071" || Bien.MaBV == "01049")
                                        {
                                            if (_ltung.First().MaCB == Bien.MaCB)
                                            {
                                                _ktmatkhau = false;
                                                ChucNang.frm_CheckPass frm = new ChucNang.frm_CheckPass();
                                                frm.ok = new ChucNang.frm_CheckPass._getdata(_getValue);
                                                frm.ShowDialog();
                                                if (!_ktmatkhau)
                                                {
                                                    MessageBox.Show("Bạn nhập sai mật khẩu!", "Thông báo");
                                                    return;
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Không phải cán bộ thu bạn không được xóa!", "Thông báo");
                                                return;
                                            }
                                        }

                                        int _maCK = 0;
                                        var dt = _dataContext.BenhNhans.Where(p => p.MaBNhan == _mabn).ToList();
                                        bool KtraNoiTru = false;
                                        int _idDTBN = -1;
                                        if (dt.Count > 0)
                                        {
                                            _idDTBN = dt.First().IDDTBN;
                                            if (dt.First().NoiTru == 0 && dt.First().DTNT == false) //ktra nội trú
                                                KtraNoiTru = false;
                                            else
                                                KtraNoiTru = true;
                                        }
                                        var ck = (from nhom in _dataContext.NhomDVs.Where(p => p.TenNhomCT.Contains("Khám bệnh"))
                                                  join dvu in _dataContext.DichVus.Where(p => p.PLoai == 2 && p.Loai == _idDTBN).Where(p => p.Status == 1) on nhom.IDNhom equals dvu.IDNhom
                                                  select new { dvu.DonGia, dvu.MaDV, dvu.DonVi, dvu.TrongDM }).OrderByDescending(p => p.DonGia).ToList();
                                        List<int> _lMaCK = new List<int>();
                                        _lMaCK = ck.Select(p => p.MaDV).Distinct().ToList();

                                        //if (Bien.MaBV == "27001")
                                        //{
                                        //    var dthuoct = _dataContext.DThuoccts.Where(p => p.IDCD == id && (_lMaCK.Contains(p.MaDV ?? 0))).ToList();
                                        //    var BNKB = _dataContext.BNKBs.Where(p => p.MaBNhan == _mabn).ToList();
                                        //    if (dthuoct != null && dthuoct.Count > 0 && BNKB != null && BNKB.Count > 0)
                                        //    {
                                        //        var check = dthuoct.Where(o => BNKB.Select(p => o.IDKB).Contains(o.IDKB) && !BNKB.Select(p => o.MaCB).Contains(o.MaCB)).ToList();
                                        //        if (check != null && check.Count > 0)
                                        //        {
                                        //            return;
                                        //        }
                                        //    }

                                        //}

                                        var chidinh = _dataContext.ChiDinhs.Where(p => p.SoPhieu == id).ToList();

                                        foreach (var a in chidinh)
                                        {
                                            a.SoPhieu = -1;
                                            a.TamThu = null;
                                            _dataContext.SaveChanges();
                                        }

                                        //if (ck.Count > 0)
                                        //    _maCK = ck.First().MaDV;
                                        //var dthuoct = _dataContext.DThuoccts.Where(p => p.IDCD == id && p.MaDV == _maCK).ToList();
                                        //foreach (var a in dthuoct)
                                        //{
                                        //    a.IDCD = 0;
                                        //    _dataContext.SaveChanges();
                                        //}
                                        if (_lMaCK.Count > 0)
                                        {
                                            if (!KtraNoiTru) //nếu là bn ngoại trú thì xóa cả đơn thuốc ct của công khám
                                            {
                                                var dthuoct = _dataContext.DThuoccts.Where(p => p.IDCD == id && (_lMaCK.Contains(p.MaDV ?? 0))).ToList();
                                                foreach (var a in dthuoct)
                                                {
                                                    int idDon = a.IDDon ?? 0;
                                                    _dataContext.DThuoccts.Remove(a);
                                                    _dataContext.SaveChanges();
                                                    if (_dataContext.DThuoccts.Where(p => p.IDDon == idDon).Count() == 0)
                                                    {
                                                        var qdthuoc = _dataContext.DThuocs.Where(p => p.IDDon == idDon).FirstOrDefault();
                                                        if (qdthuoc != null)
                                                        {
                                                            _dataContext.DThuocs.Remove(qdthuoc);
                                                            _dataContext.SaveChanges();
                                                        }
                                                    }

                                                }
                                            }
                                            else // với bn nội trú, đt ngoại trú thì ko xóa dtct công khám
                                            {
                                                var dthuoct = _dataContext.DThuoccts.Where(p => p.IDCD == id).Where(p => _lMaCK.Contains(p.MaDV ?? 0)).ToList();
                                                foreach (var a in dthuoct)
                                                {
                                                    a.ThanhToan = 0;
                                                    a.IDCD = 0;
                                                    _dataContext.SaveChanges();
                                                    //int idDon = a.IDDon ?? 0;
                                                    //_dataContext.Remove(a);
                                                    //_dataContext.SaveChanges();
                                                    //if (_dataContext.DThuoccts.Where(p => p.IDDon == idDon).Count() == 0)
                                                    //{
                                                    //    var qdthuoc = _dataContext.DThuocs.Where(p => p.IDDon == idDon).FirstOrDefault();
                                                    //    if (qdthuoc != null)
                                                    //    {
                                                    //        _dataContext.Remove(qdthuoc);
                                                    //        _dataContext.SaveChanges();
                                                    //    }
                                                    //}

                                                }
                                            }
                                        }



                                        List<TamUngct> _xoact = _dataContext.TamUngcts.Where(p => p.IDTamUng == id).ToList();
                                        foreach (var item in _xoact)
                                        {
                                            if (item.IDDonct != null && item.IDDonct != 0)
                                            {
                                                var dtct = _dataContext.DThuoccts.Where(p => p.IDDonct == item.IDDonct).FirstOrDefault();
                                                if (dtct != null)
                                                    dtct.ThanhToan = 0;
                                            }
                                            _dataContext.TamUngcts.Remove(item);
                                        }
                                        if (_xoact.Count > 0)
                                            _dataContext.SaveChanges();
                                        var _xoa = _dataContext.TamUngs.Single(p => p.IDTamUng == (id));
                                        if (Bien.MaBV == "01071" || Bien.MaBV == "01049" || Bien.MaBV == "12345" || Bien.MaBV == "24297" || Bien.MaBV == "56789") //những bn dịch chỉ định CLS ở phòng tiếp đón, khi thu trực tiếp tính là đã thanh toán đức 04-10
                                        {
                                            var BNKB = _dataContext.BNKBs.Where(p => p.MaBNhan == _mabn).ToList();
                                            if (BNKB.Count == 0)
                                            {
                                                var TTBN = _dataContext.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                                                if (TTBN != null && TTBN.DTuong == "Dịch vụ")
                                                {
                                                    TTBN.Status = 0;
                                                    _dataContext.SaveChanges();
                                                }
                                            }
                                        }
                                        if (xoaTamUng01830)
                                            _xoa.SoTien = 0;
                                        else
                                            _dataContext.TamUngs.Remove(_xoa);
                                        _dataContext.SaveChanges();
                                        //_litamung = _dataContext.TamUngs.Where(p => p.MaBNhan== (txtMaBNhan.Text)).OrderByDescending(p=>p.IDTamUng).ToList();
                                        //grcTamUng.DataSource = _litamung.ToList();
                                        grvBNhantt_FocusedRowChanged(null, null);
                                    }
                                }
                            }
                        }
                        else
                            MessageBox.Show("Bệnh nhân đã thanh toán, bạn không thể xóa");
                    }
                }
                else
                {
                    MessageBox.Show("Không có bệnh nhân để xóa");
                }
            }
            else
            {
                MessageBox.Show("Chức năng bị giới hạn");
            }
            grvBNhantt_FocusedRowChanged(null, null);
        }
        #region KtraTCTT // kiểm tra thu chi TT
        public bool KtraTCTT(int mabn, int status, string maBV = "")
        {
            try
            {
                var kt = _dataContext.TamUngs.Where(p => p.PhanLoai == 1 || p.PhanLoai == 2).Where(p => p.MaBNhan == mabn).ToList();
                if (kt.Count > 0)
                {
                    if (status == 1)
                    {
                        int id = 0;
                        id = kt.First().IDTamUng;
                        var xoa = _dataContext.TamUngs.Single(p => p.IDTamUng == id);
                        _dataContext.TamUngs.Remove(xoa);
                        _dataContext.SaveChanges();
                        return true;
                    }
                    if (status == 0 && maBV != "30372")
                    {
                        MessageBox.Show("Bệnh nhân đã được nhập biên lai thu-chi thanh toán, bạn không thể xóa", "Thông báo");
                        return false;
                    }
                    if (status == 2 && maBV != "30372")
                    {
                        MessageBox.Show("Bệnh nhân đã được nhập biên lai thu-chi thanh toán, bạn không thể tạo-sửa-xóa chứng từ", "Thông báo");
                        return false;
                    }


                }
                return true;
            }
            catch (Exception)
            {
                //MessageBox.Show("Lỗi xóa biên lai thu - chi TT");
                return false;
            }
        }
        #endregion
        //
        private void btnXoa_Click(object sender, EventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            //try
            //{
            bool _sua = true;
            _sua = DungChung.Ham.checkQuyen(xtraTToan.Name)[2];
            if (_sua == false)
            {
                MessageBox.Show("Chức năng bị khóa");
            }
            if (_sua)
            {
                if (ckcDonNgoai.Checked == true)
                {
                    int _idnhap = 0;
                    if (grvBNhantt.GetFocusedRowCellValue(colIDNhap) != null)
                    {
                        _idnhap = Convert.ToInt32(grvBNhantt.GetFocusedRowCellValue(colIDNhap));
                        var checkthu = _dataContext.TamUngs.Where(p => p.IDNhapD == _idnhap).FirstOrDefault();
                        if (checkthu != null)
                        {
                            _dataContext.TamUngs.Remove(checkthu);
                            _dataContext.SaveChanges();
                            MessageBox.Show("Xóa thành công");
                            grcThanhToan.DataSource = "";
                            grvBNhantt_FocusedRowChanged(null, null);
                        }
                    }
                }
                else
                {
                    if (_int_maBN > 0 && DungChung.Ham._checkNgayKhoa(_dataContext, dtNgayTT.DateTime, "KhoaVP") == false)
                    {
                        bool _xoavp = true;
                        List<VienPhi> xoa = new List<VienPhi>();
                        // string _mabn = txtMaBNhan.Text;
                        xoa = (from bn in _dataContext.VienPhis.Where(p => p.MaBNhan == _int_maBN) select bn).ToList();
                        if (xoa.Count <= 0)
                        {

                            MessageBox.Show("bệnh nhân không có thanh toán để xóa!");
                            _sua = false;
                            return;
                        }
                        else
                        {
                            if ((Bien.MaBV == "30009" || Bien.MaBV == "30007" || Bien.MaBV == "30002") && xoa.First().Status == 1 && Bien.MaBV != "30372")
                            {
                                MessageBox.Show("Bệnh nhân đã được duyệt chi phí, bạn không thể xóa!");
                                return;
                            }
                        }
                        if (_sua && xoa.Count > 0 && (xoa.First().Export == true || xoa.First().ExportBHXH == true || xoa.First().ExportBYT == true))
                        {
                            MessageBox.Show("Dữ liệu đã được gửi, bạn không thể xóa");
                            _sua = false;
                            return;
                        }
                        var ktduyet = _dataContext.TamUngs.Where(p => p.MaBNhan == _int_maBN && (p.PhanLoai == 1 || p.PhanLoai == 2)).ToList();
                        if (ktduyet.Count > 0)
                        {
                            if (Bien.MaBV != "30372")
                            {
                                MessageBox.Show("Bệnh nhân đã được duyệt, bạn không thể xóa!");
                                return;
                            }

                        }
                        var ktxd = _dataContext.NhapDs.Where(p => p.MaBNhan == _int_maBN).ToList();
                        if (_sua && ktxd.Count > 0)
                        {
                            if (Bien.MaBV != "24012")
                            {
                                MessageBox.Show("Bệnh nhân đã được xuất dược, bạn không thể xóa");
                                _sua = false;
                                return;
                            }
                        }

                        if (Bien.MaBV != "27001" && _sua && !DungChung.Ham._KiemTraCBSuaXoa(_dataContext, xoa.First().MaCB, Bien.MaCB))
                        {
                            _xoavp = false;
                            MessageBox.Show("Mã cán bộ không khớp, bạn không thể xóa!");
                            return;
                        }
                        if (!KtraTCTT(_int_maBN, 0, Bien.MaBV))
                        {
                            _sua = false;
                            return;
                        }

                        int _idxoa = xoa.First().idVPhi;
                        if (_sua)
                        {
                            DialogResult result;
                            result = MessageBox.Show("Bạn muốn xóa thanh toán BN: " + txtTenBenhNhan.Text, "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.No)
                                _sua = false;
                            _ktmatkhau = false;
                            ChucNang.frm_CheckPass frm = new ChucNang.frm_CheckPass();
                            frm.ok = new ChucNang.frm_CheckPass._getdata(_getValue);
                            frm.ShowDialog();
                            if (!_ktmatkhau)
                                _sua = false;
                        }
                        if (_sua)
                        {
                            string tb = "Nhập lý do xóa thanh toán BN: " + _int_maBN.ToString();
                            QLBV.frm_GetLyDoLog frm = new frm_GetLyDoLog(tb);
                            frm.ok = new frm_GetLyDoLog._getdata(_getLyDoLog);
                            frm.ShowDialog();
                            if (!string.IsNullOrEmpty(_lydoLodg))
                                _sua = true;
                            else
                                _sua = false;
                        }
                        if (_sua)
                        {
                            List<VienPhict> sl = new List<VienPhict>();
                            sl = _dataContext.VienPhicts.Where(p => p.idVPhi == _idxoa).ToList();
                            if (sl.Count > 0)
                            {

                                foreach (var s in sl)
                                {
                                    var dtct = _dataContext.VienPhicts.Single(p => p.idVPhict == s.idVPhict);
                                    if (dtct != null)
                                    {
                                        _dataContext.VienPhicts.Remove(dtct);
                                        _dataContext.SaveChanges();
                                    }
                                }
                            }
                            var xoad = _dataContext.VienPhis.Single(p => p.idVPhi == _idxoa);
                            if (xoad != null)
                            {
                                _dataContext.VienPhis.Remove(xoad);
                                var abc = _dataContext.SaveChanges();
                                if (abc > 0)
                                {
                                    LOG moi = new LOG();
                                    moi.DateLog = DateTime.Now;
                                    moi.LyDo = _lydoLodg;
                                    moi.UserName = Bien.TenDN;
                                    moi.MaBNhan = _int_maBN;
                                    moi.IdForm = 904;
                                    moi.ComputerName = SystemInformation.ComputerName;
                                    moi.MaCB = Bien.MaCB;
                                    moi.Status = 4;
                                    _dataContext.LOGs.Add(moi);
                                    _dataContext.SaveChanges();
                                    _lydoLodg = "";
                                }
                            }


                            // xóa sao lưu\
                            //DungChung.Ham.xoaChiPhiDV(_dataContext, _idxoa);
                            // xóa ra viện
                            try
                            {
                                int _ntru = 0;
                                var noitru = _dataContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN).Select(p => p.NoiTru).ToList();
                                if (noitru.Count > 0)
                                {
                                    _ntru = noitru.First().Value;
                                }
                                if (_ntru == 0)
                                {
                                    var vvien = _dataContext.VaoViens.Where(p => p.MaBNhan == _int_maBN).ToList();
                                    if (vvien.Count <= 0)
                                    {
                                        var ravien = _dataContext.RaViens.Where(p => p.MaBNhan == _int_maBN && p.Status == 2).ToList();
                                        if (Bien.MaBV == "24272")
                                        {
                                            DungChung.Ham._setStatus(_int_maBN, 2);
                                        }
                                        else if (ravien.Count > 0) // 24272 yêu cầu k xóa ra viện, HIS 2384
                                        {
                                            if (Bien.MaBV == "27001")
                                            {
                                                DungChung.Ham._LuuXoaRaVien(_dataContext, _int_maBN, DateTime.Now, "luu", 2);
                                            }
                                            else
                                            {
                                                DungChung.Ham._LuuXoaRaVien(_dataContext, _int_maBN, DateTime.Now, "Xoa", 2);
                                            }
                                            //var _xoaravien = _dataContext.RaViens.Single(p => p.MaBNhan == _int_maBN);
                                            //_dataContext.Remove(_xoaravien);
                                            //_dataContext.SaveChanges();

                                            ////dung290516
                                            //// DungChung.Ham._setStatus(_int_maBN, 1);
                                            //var qcls = _dataContext.CLS.Where(p => p.MaBNhan == _int_maBN).ToList();
                                            //if (qcls.Count == 0)
                                            //    DungChung.Ham._setStatus(_int_maBN, 1);// bệnh nhân đã khám
                                            //else
                                            //    DungChung.Ham._setStatus(_int_maBN, 5);// bệnh nhân đã có kqCLS
                                            //-----------
                                        }
                                    }
                                    else
                                    {
                                        //dung290516
                                        // DungChung.Ham._setStatus(_int_maBN, 2);
                                        var qcls = _dataContext.CLS.Where(p => p.MaBNhan == _int_maBN).ToList();
                                        if (qcls.Count == 0)
                                            DungChung.Ham._setStatus(_int_maBN, 2);// bệnh nhân đã khám
                                        else
                                            DungChung.Ham._setStatus(_int_maBN, 2);// bệnh nhân đã có kqCLS
                                        //-----------
                                    }
                                }
                                else
                                {
                                    //dung290516
                                    // DungChung.Ham._setStatus(_int_maBN, 2);
                                    var qcls = _dataContext.CLS.Where(p => p.MaBNhan == _int_maBN).ToList();
                                    if (qcls.Count == 0)
                                        DungChung.Ham._setStatus(_int_maBN, 2);// bệnh nhân đã khám
                                    else
                                        DungChung.Ham._setStatus(_int_maBN, 2);// bệnh nhân đã có kqCLS
                                    //-----------
                                }
                                grcThanhToan.DataSource = "";
                                //DungChung.Ham._LuuXoaRaVien(_dataContext, _int_maBN, DateTime.Now, "Xoa", 2);
                                //#region xóa STT thanh toán trong bảng SoDKKB 27001

                                //if(Bien.MaBV == "27001")
                                //{
                                //    List<SoDKKB> qsodk = _dataContext.SoDKKBs.Where(p => p.Status == 3 && p.MaBNhan == _int_maBN).ToList();
                                //    foreach(SoDKKB a in qsodk)
                                //    {
                                //        _dataContext.SoDKKBs.Remove(a);
                                //    }
                                //    _dataContext.SaveChanges();
                                //}
                                //#endregion

                                MessageBox.Show("Xóa thành công");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi xóa ra viện:" + ex.Message);
                            }

                        }
                        // sau này tách rieng code ravien chuyển thành     DungChung.Ham._setStatus(txtMaBNhan.Text, 2)

                    }
                    else
                    {
                        MessageBox.Show("Bạn chưa chọn bệnh nhân hoặc không có bệnh nhân để xóa tt");
                    }
                }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("Lỗi xóa thanh toán: " + ex.Message);
                //}
            }
            usTamThu_TToan_Load(sender, e);
        }

        private List<string> _strLyDo(QLBV_Database.QLBVEntities _data)
        {
            List<string> _str = new List<string>();

            _str = _data.TamUngs.Where(p => p.LyDo != null).Select(p => p.LyDo).Distinct().OrderBy(p => p).ToList();

            return _str;
        }
        private void xtraTToan_TThu_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {

            EnableControl(false);
            if (DungChung.Ham.checkQuyen(xtraTThu.Name)[0])
            {
                btnMoi.Enabled = true;
                btnSua.Enabled = true;
                btnXoatthu.Enabled = true;

            }
            else
            {
                btnMoi.Enabled = false;
                btnSua.Enabled = false;
                btnXoatthu.Enabled = false;
            }


            if (xtraTToan_TThu.SelectedTabPageIndex == 1)
            {
                txtNoiDung.Properties.Items.AddRange(_strLyDo(_dataContext));
            }
            //switch (xtraTToan_TThu.SelectedTabPageIndex) { 
            //    case 0:
            //        break;
            //    case 1:
            //        if (!string.IsNullOrEmpty(txtMaBNhan.Text)) {
            //            _litamung = _dataContext.TamUngs.Where(p => p.MaBNhan== (txtMaBNhan.Text)).OrderByDescending(p=>p.IDTamUng).ToList();
            //            grcTamUng.DataSource = _litamung.ToList();
            //        }
            //        break;
            //}
        }

        private void grvThanhToan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            List<TamUng> _ltung = new List<TamUng>();
            int id = 0;
            if (grvTamUng.RowCount > 0 && grvTamUng.GetFocusedRowCellValue(colIDTamUng) != null)
            {
                id = int.Parse(grvTamUng.GetFocusedRowCellValue(colIDTamUng).ToString());
                txtIdTamUng.Text = id.ToString();
            }
            else
                txtIdTamUng.Text = "";
            _ltung = _litamung.Where(p => p.IDTamUng == (id)).ToList();
            if (_ltung.Count > 0)
            {
                if (_ltung.First().NgayThu != null)
                    dtNgayThu.DateTime = _ltung.First().NgayThu.Value;
                else
                    dtNgayThu.DateTime = DateTime.Now;
                //lupBPThu.EditValue=_ltung.First()
                lupNguoiThu.EditValue = _ltung.First().MaCB;
                if (_ltung.First().SoTien != null)
                {
                    txtSoTien.Text = _ltung.First().SoTien.ToString();
                    txtSoTienBangChu.Text = DungChung.Ham.DocTienBangChu(_ltung.First().SoTien.Value, " đồng");
                }
                if (_ltung.First().PhanLoai != null)
                    cboPLThu.SelectedIndex = _ltung.First().PhanLoai.Value;
                else
                    cboPLThu.SelectedIndex = -1;
                if (_ltung.First().LyDo != null)
                    txtNoiDung.Text = _ltung.First().LyDo;
            }
            else
            {
                grcTamUng.DataSource = null;
                grcTamUngct.DataSource = null;
                dtNgayThu.DateTime = System.DateTime.Now;
                lupNguoiThu.EditValue = "";
                txtSoTien.Text = "";
                txtSoTienBangChu.Text = "";
                cboPLThu.SelectedIndex = -1;
            }
            GC.Collect();
        }
        #region get số biên lai
        public class QuyenSoBL
        {
            string quyen;

            public string Quyen
            {
                get { return quyen; }
                set { quyen = value; }
            }
            int so;

            public int So
            {
                get { return so; }
                set { so = value; }
            }
            public static List<QuyenSoBL> _getQuyen_SoBL(int pl, string macb)
            {
                QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(Bien.StrCon);
                List<QuyenSoBL> _lQuyen = new List<QuyenSoBL>();
                var sobl = (from a in _data.SoBienLais.Where(p => p.Status == 1 && p.PLoai == pl) select a).ToList();
                if (sobl.Count > 1)
                {
                    MessageBox.Show("Hiện tại có 2 quyển hóa đơn cùng phần loại đang sử dụng \nThiết lập lại danh mục biên lai để lấy số hóa đơn");
                    return null;
                }
                else if (sobl.Count == 0)
                {
                    MessageBox.Show("Bạn chưa thiết lập danh mục biên lai, thiết lập danh mục biên lai để lấy số hóa đơn");
                    return null;
                }
                else
                {
                    int soht = sobl.First().SoHT;
                    int sotu = sobl.First().SoTu;
                    int soden = sobl.First().SoDen;
                    if (soht < sotu || soht > soden)
                    {
                        MessageBox.Show("Quyển biên lai thiết lập chưa đúng, thiết lập lại danh mục biên lai để lấy số hóa đơn");
                        return null;
                    }
                    else
                    {
                        _lQuyen = (from b in sobl select new QuyenSoBL { quyen = b.Quyen, so = b.SoHT }).ToList();
                    }
                }
                return _lQuyen;
            }
        }

        #endregion
        private void themmoitamungthutt(int pl)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            bool _sua = true;
            _sua = DungChung.Ham.checkQuyen(xtraTThu.Name)[0];
            if (_sua)
            {
                if (_int_maBN > 0)
                {
                    if (KtraTCTT(_int_maBN, 2))
                    {
                        var a = QuyenSoBL._getQuyen_SoBL(pl, "");
                        if (a != null)
                        {
                            cbo_quyenTU.Text = a.FirstOrDefault().Quyen;
                            cbo_soHD_TU.Text = (a.FirstOrDefault().So + 1).ToString();
                            xtraTThu.Text = "Tạm ứng*";
                            EnableControl(true);
                            lupNguoiThu.EditValue = Bien.MaCB;
                            lupBPThu.EditValue = Bien.MaKP;
                            dtNgayThu.DateTime = System.DateTime.Now;
                            txtSoTien.Text = "";
                            txtSoTienBangChu.Text = ""; string DTuong = "";
                            int noitru = -1;
                            var kp = (from k in _dataContext.KPhongs
                                      join bnkb in _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN) on k.MaKP equals bnkb.MaKP
                                      select k).ToList();
                            var kp2 = kp.Union(from k in _dataContext.KPhongs
                                               join bnkb in _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN) on k.MaKP equals bnkb.MaKPdt
                                               select k).ToList();
                            lupBPThu.Properties.DataSource = kp2.Distinct().ToList();
                            var TTBN = _dataContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN).ToList();
                            if (TTBN.Count > 0)
                            {
                                DTuong = TTBN.First().DTuong;
                                if (TTBN.First().NoiTru != null)
                                    noitru = TTBN.First().NoiTru.Value;
                                lupBPThu.EditValue = TTBN.First().MaKP;
                            }
                            btnThuBangThe.Enabled = true;
                            cboPLThu.SelectedIndex = 0;
                            _lchidinh.Clear();
                            TTLuutthu = 1;

                            cboPLThu.Focus();
                        }
                        //else
                        //{
                        //    break;
                        //}
                    }
                }
                else
                {
                    MessageBox.Show("Bạn chưa chọn bệnh nhân");
                }
            }
        }
        private void btnMoi_Click(object sender, EventArgs e)
        {
            themmoitamungthutt(0);
            if (Bien.MaBV == "01071" || Bien.MaBV == "01049")
            {
                lupNguoiThu.Properties.DataSource = _lCanBo.Where(p => p.MaCB == Bien.MaCB).ToList();
            }
        }
        /// <summary>
        /// kiểm tra các chỉ định đã thu tiền và đã thực hiện thì không cho sửa xóa tạm thu
        /// </summary>
        /// <returns></returns>
        private bool _ktThuCLS(QLBV_Database.QLBVEntities _data, int _soPhieu)
        {
            //var tu = _data.TamUngs.Where(p => p.IDTamUng == _soPhieu).FirstOrDefault();
            //if (tu != null)
            //{

            var chidinh = _data.ChiDinhs.Where(p => p.SoPhieu == _soPhieu).Where(p => p.Status == 1).ToList();
            if (chidinh.Count > 0)
                return true;
            //}
            return false;
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            var tk = _dataContext.ADMINs.FirstOrDefault(o => o.TenDN == Bien.TenDN);

            if (Bien.MaBV == "56789" && tk != null && tk.CapDo != 9)
            {
                MessageBox.Show("Tài khoản không có quyền sửa tạm thu");
                return;
            }
            bool _sua = true;
            _sua = DungChung.Ham.checkQuyen(xtraTThu.Name)[1];
            if (_sua == false)
            {
                MessageBox.Show("Chức năng bị giới hạn");
            }


            if (_sua && string.IsNullOrEmpty(txtMaBNhan.Text))
            {
                MessageBox.Show("Bạn chưa chọn bệnh nhân");
                _sua = false;

            }
            if (_sua && string.IsNullOrEmpty(txtIdTamUng.Text))
            {
                _sua = false;
                MessageBox.Show("Không có chứng từ để sửa");
            }
            int _soPhieu = 0;
            if (!string.IsNullOrEmpty(txtIdTamUng.Text))
                _soPhieu = Convert.ToInt32(txtIdTamUng.Text);
            if (_sua && _ktThuCLS(_dataContext, _soPhieu))
            {
                _sua = false;
                MessageBox.Show("Dịch vụ cận lâm sàng đã được thực hiện, bạn không thể sửa phiếu thu này");
            }

            var tamUng = _dataContext.TamUngs.FirstOrDefault(o => o.IDTamUng == _soPhieu);
            if (tamUng.LoaiThanhToan == 1)
            {
                MessageBox.Show("Không thể sửa phiếu thu ngân hàng!");
                return;
            }

            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);

            if ((tamUng.PhanLoai == 0 || tamUng.PhanLoai == 1 || tamUng.PhanLoai == 3) && Bien.MaBV == "30372" && CheckPL1(_int_maBN))
            {
                MessageBox.Show("Không cho phép sửa phiếu!");
                return;
            }
            var ktraduyet = _dataContext.TamUngs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PhanLoai == 2 || p.PhanLoai == 1).ToList();
            if (ktraduyet.Count() > 0)
            {
                _sua = false;
                MessageBox.Show("Bệnh nhân đã duyệt thanh toán, không thế sửa !");
            }
            if (_sua)
            {
                xtraTThu.Text = "Tạm ứng*";
                if (cboPLThu.SelectedIndex != 0)
                {
                    EnableControl(true);
                    cboPLThu.Enabled = false;
                    TTLuutthu = 2;
                    cboPLThu.Focus();
                }
                else
                {
                    if (KtraTCTT(!String.IsNullOrEmpty(txtMaBNhan.Text) ? 0 : Convert.ToInt32(txtMaBNhan.Text), 2))//0k
                    {

                        EnableControl(true);
                        cboPLThu.Enabled = false;
                        TTLuutthu = 2;
                        cboPLThu.Focus();
                    }
                }



            }
        }

        private void btnLuutthu_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboPLThu.SelectedIndex == 6)
                {
                    MessageBox.Show("Không thể thu trực tiếp phân loại này");
                    return;
                }
                int rs;
                int _int_maBN = 0;
                if (Int32.TryParse(txtMaBNhan.Text, out rs))
                    _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
                bool _tthai = false;
                int IdGoiDV = 0;
                if (cboPLThu.SelectedIndex == 0)
                    _tthai = DungChung.Ham.KTraTT(_dataContext, _int_maBN);
                int ploai = DungChung.Bien.MaBV == "01071" ? ((cboPLThu.SelectedIndex == 3 || cboPLThu.SelectedIndex == 1 || cboPLThu.SelectedIndex == 8) ? 2 : (((cboPLThu.SelectedIndex == 2 || cboPLThu.SelectedIndex == 7 || cboPLThu.SelectedIndex == 0) ? 1 : cboPLThu.SelectedIndex))) :((cboPLThu.SelectedIndex == 3 || cboPLThu.SelectedIndex == 1) ? 2 : (((cboPLThu.SelectedIndex == 2 || cboPLThu.SelectedIndex == 0) ? 1 : cboPLThu.SelectedIndex)));
                {
                    if (_tthai == false)
                    {

                        xtraTThu.Text = "Tạm ứng";
                        double sotien = 0;
                        if (!string.IsNullOrEmpty(txtSoTien.Text))
                            sotien = double.Parse(txtSoTien.Text);
                        if (sotien == 0 && Bien.MaBV != "12345" && Bien.MaBV != "24297" && Bien.MaBV != "56789" && Bien.MaBV != "30372")
                        {
                            if (Bien.MaBV == "01830" && cboPLThu.SelectedIndex == 0)
                            {

                            }
                            else
                                return;
                        }
                        switch (TTLuutthu)
                        {

                            case 1:
                                if (_ktQuyen_SoHD(_dataContext, ploai, cbo_quyenTU.Text.Trim(), cbo_soHD_TU.Text.Trim()))
                                {
                                    DialogResult _result2 = MessageBox.Show("Số hóa đơn đã được sử dụng, bạn vẫn muốn lưu?", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (_result2 == DialogResult.No)
                                        return;
                                }
                                //if (Bien.MaBV == "01071") //những bn dịch chỉ định CLS ở phòng tiếp đón, khi thu trực tiếp tính là đã thanh toán đức 04-10
                                //{
                                //    var BNKB = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).ToList();
                                //    if (BNKB.Count == 0)
                                //    {
                                //        var TTBN = _dataContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN).FirstOrDefault();
                                //        if (TTBN != null && TTBN.DTuong == "Dịch vụ")
                                //        {
                                //            TTBN.Status = 3;
                                //            _dataContext.SaveChanges();
                                //        }
                                //    }
                                //}

                                var maCB = lupNguoiThu.EditValue.ToString();
                                var ngayThu = dtNgayThu.DateTime;
                                ConnectBanking.Agribank.ParamResult paramResult = null;
                                int? idTamUngThuThe = null;
                                if (thuBangThe)
                                {
                                    if (!DungChung.Ham.ThanhToanPOS_Agribank(_int_maBN, sotien, maCB, ref paramResult))
                                    {
                                        MessageBox.Show("Thanh toán không thành công!");
                                        return;
                                    }
                                    else
                                    {
                                        TamUng moiThe = new TamUng();
                                        moiThe.SoTien = sotien;
                                        moiThe.MaCB = lupNguoiThu.EditValue.ToString();
                                        moiThe.NgayThu = ngayThu;
                                        moiThe.NgayTaoGDThe = DateTime.Now;
                                        moiThe.MaBNhan = _int_maBN;
                                        moiThe.PhanLoai = 5;
                                        moiThe.LoaiThanhToan = 1;
                                        moiThe.LyDo = "Thu bằng thẻ ngân hàng";
                                        if (paramResult != null && paramResult.Sale != null && paramResult.Sale.transInfo != null)
                                        {
                                            moiThe.ClientId = paramResult.Sale.clientId;
                                            moiThe.RequestId = paramResult.Sale.requestId;
                                            moiThe.ReceiptNo = paramResult.Sale.transInfo.receiptNo;
                                            moiThe.SoTien = double.Parse(paramResult.Sale.transInfo.amount);
                                        }
                                        _dataContext.TamUngs.Add(moiThe);
                                        _dataContext.SaveChanges();
                                        MessageBox.Show("Thanh toán thành công!");
                                        idTamUngThuThe = moiThe.IDTamUng;
                                    }
                                }

                                TTLuutthu = 0;
                                TamUng _tamung = new TamUng();
                                bool _ngoaih = false;
                                _ngoaih = DungChung.Ham.CheckNGioHC(dtNgayThu.DateTime);

                                if (_ngoaih == true)
                                {
                                    MessageBox.Show("Thu-Chi ngoài giờ HC");
                                    _tamung.NgoaiGio = 1;
                                }
                                else
                                {
                                    _tamung.NgoaiGio = 0;
                                }
                                if (!string.IsNullOrEmpty(txtIDGoiDV.Text))
                                {
                                    IdGoiDV = Convert.ToInt32(txtIDGoiDV.Text);
                                    _tamung.IDGoiDV = IdGoiDV;
                                }
                                _tamung.Status = true;
                                _tamung.NgayThu = ngayThu;
                                _tamung.MaKP = lupBPThu.EditValue == null ? 0 : Convert.ToInt32(lupBPThu.EditValue);//0k
                                _tamung.MaCB = lupNguoiThu.EditValue.ToString();
                                _tamung.MaBNhan = _int_maBN;
                                var CheckTuDuyet = _dataContext.HTHONGs.First().TuDongDuyet ?? false;
                                if ((Bien.MaBV == "12345" || Bien.MaBV == "24297") && CheckTuDuyet)
                                {
                                    _tamung.DuyetPhieuThu = true;
                                }
                                if (cboPLThu.SelectedIndex == 1)
                                {
                                    if (!string.IsNullOrEmpty(cboKLKSK.Text))
                                        _tamung.KetLuan = cboKLKSK.SelectedIndex;
                                }
                                else
                                {
                                    if (chkKetLuan.Checked)
                                        _tamung.KetLuan = 2;
                                    else
                                        _tamung.KetLuan = -1;
                                }
                                if (_lchidinh.Count() > 0)
                                {
                                    _tamung.TongTien = _lchidinh.Sum(p => p.DonGia);
                                    _tamung.Mien = _lchidinh.First().Mien;
                                }
                                _tamung.PhanLoai = cboPLThu.SelectedIndex;
                                if (cboPLThu.SelectedIndex == 3)
                                {
                                    _tamung.TienChenh = double.Parse(txtSoTien.Text);
                                }
                                if(DungChung.Bien.MaBV == "30372")
                                {
                                    _tamung.TienChenh = double.Parse(txtSoTien.Text);
                                }
                                _tamung.SoTien = double.Parse(txtSoTien.Text);
                                _tamung.LyDo = txtNoiDung.Text + (idTamUngThuThe == null ? "" : ("(Thu bằng thẻ ngân hàng)"));
                                if (!string.IsNullOrEmpty(txtSoTo.Text))
                                    _tamung.SoTo = Convert.ToInt32(txtSoTo.Text);
                                else
                                    _tamung.SoTo = 0;
                                _tamung.QuyenHD = cbo_quyenTU.Text.Trim();
                                _tamung.IDTamUngThe = idTamUngThuThe;
                                _tamung.SoHD = cbo_soHD_TU.Text.Trim();
                                _dataContext.TamUngs.Add(_tamung);
                                _dataContext.SaveChanges();

                                _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
                                var id1 = _tamung.IDTamUng;

                                if (id1 > 0)
                                {
                                    if (DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "24009")
                                    {
                                        ploai = _dataContext.SoBienLais.Where(p => p.Quyen == cbo_quyenTU.Text).Select(p => p.PLoai).FirstOrDefault();
                                    }
                                    var so = _dataContext.SoBienLais.Where(p => p.PLoai == ploai && p.Quyen == cbo_quyenTU.Text).FirstOrDefault();//var soBL = _dataContext.SoBienLais.Where(p => p.PLoai == 0 && p.Quyen == cbo_quyenTU.Text).ToList(); sai
                                    SoBienLai soBL = so;
                                    int soht = soBL.SoHT + 1;
                                    if (soht == soBL.SoDen)
                                    {
                                        soBL.SoHT = soht;
                                        soBL.Status = 2;
                                        _dataContext.SaveChanges();
                                        MessageBox.Show("quyển hóa đơn: " + cbo_quyenTU.Text + "đã sử dụng hết, \nthiết lập thêm quyển khác để sử dụng cho hóa đơn sau");
                                    }
                                    else
                                    {
                                        soBL.SoHT = soht;
                                        _dataContext.SaveChanges();
                                    }
                                    //foreach (var item in soBL)
                                    //{
                                    //    int soht = item.SoHT + 1;
                                    //    if (soht==item.SoDen)
                                    //    {

                                    //    }
                                    //    item.SoHT = item.SoHT + 1;
                                    //    _dataContext.SaveChanges();
                                    //}
                                    if (IdGoiDV <= 0)//Thu trực tiếp đối tượng khám sức khỏe thì ko cần thêm vào tamungct và dthuoc
                                    {
                                        _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
                                        List<QLBV.FormNhap.frm_CPChiDinh.lCPCHiDinh> _lChuaCoDT = new List<frm_CPChiDinh.lCPCHiDinh>();// những dịch vụ thu thẳng mà chưa có trong đơn thuốc
                                        foreach (var a in _lchidinh)
                                        {
                                            if (a.Chon)
                                            {

                                                // lưu tạm ứng ct
                                                TamUngct moi = new TamUngct();
                                                moi.IDTamUng = id1;
                                                moi.MaKP = a.MaKP;
                                                moi.MaKPth = a.MaKPth;
                                                moi.MaDV = a.MaDV;
                                                moi.SoLuong = a.SoLuong;
                                                moi.DonGia = a.DonGia;
                                                moi.ThanhTien = a.ThanhTien;
                                                moi.SoTien = a.TienBN;
                                                moi.TrongBH = a.TrongBH;
                                                moi.TienBN = a.TienBN;
                                                moi.Mien = a.Mien;
                                                moi.IDDonct = a.IDDonct;
                                                _dataContext.TamUngcts.Add(moi);
                                                _dataContext.SaveChanges();
                                                //
                                                if (_lmack.Contains(a.MaDV))//  if (a.MaDV == _maCKham)
                                                {
                                                    var _update = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn)
                                                                   join dtct in _dataContext.DThuoccts.Where(p => p.MaDV == a.MaDV) on dt.IDDon equals dtct.IDDon
                                                                   select dtct).ToList();
                                                    if (_update.Count > 0)
                                                    {
                                                        foreach (var b in _update)
                                                        {
                                                            b.IDCD = id1;
                                                            b.ThanhToan = 1;
                                                            _dataContext.SaveChanges();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        _lChuaCoDT.Add(a);

                                                    }
                                                }

                                                var sophieu = _dataContext.ChiDinhs.Where(p => p.IDCD == a.IDCD).FirstOrDefault();
                                                if (sophieu != null)
                                                    sophieu.SoPhieu = id1;
                                                var dthuoctDVThuThang = _dataContext.DThuoccts.Where(p => p.IDDonct == a.IDDonct).FirstOrDefault();
                                                if (dthuoctDVThuThang != null)
                                                    dthuoctDVThuThang.ThanhToan = 1;
                                                var dthuoct = _dataContext.DThuoccts.Where(p => p.IDCD == a.IDCD).FirstOrDefault();
                                                if (dthuoct != null)
                                                    dthuoct.ThanhToan = 1;

                                                _dataContext.SaveChanges();
                                            }

                                        }


                                        #region add thêm đơn thuốc
                                        var CheckCK = (from ck in _lmack
                                                       join a in _lChuaCoDT on ck equals a.MaDV
                                                       select a).ToList();
                                        if (CheckCK.Count > 0) // đức 27-09
                                        {
                                            if (_lChuaCoDT.Count > 0)
                                            {
                                                int makp = 0;
                                                DThuoc dthuoccd = new DThuoc();
                                                var qbnkb = _dataContext.BNKBs.Where(p => p.MaBNhan == _mabn).OrderBy(p => p.IDKB).ToList();
                                                var bnhan = _dataContext.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                                                if (qbnkb.Count > 0)
                                                {
                                                    makp = qbnkb.FirstOrDefault().MaKP ?? 0;
                                                    dthuoccd.NgayKe = qbnkb.First().NgayKham;
                                                }
                                                else
                                                {

                                                    if (bnhan != null)
                                                        makp = bnhan.MaKP ?? 0;
                                                    dthuoccd.NgayKe = bnhan.NNhap;
                                                }
                                                dthuoccd.MaBNhan = _mabn;
                                                dthuoccd.KieuDon = -1;
                                                dthuoccd.PLDV = 2;
                                                if (qbnkb.Count > 0 && qbnkb.First().MaCB != null)
                                                    dthuoccd.MaCB = qbnkb.First().MaCB;

                                                _dataContext.DThuocs.Add(dthuoccd);
                                                _dataContext.SaveChanges();

                                                foreach (var b in _lChuaCoDT)
                                                {
                                                    if (_lmack.Contains(b.MaDV))//chỉ thêm vào DThuocct khi dịch vụ là công khám 
                                                    {
                                                        DThuocct dtct = new DThuocct();
                                                        dtct.MaKP = makp;
                                                        dtct.IDDon = dthuoccd.IDDon;
                                                        if (qbnkb.Count > 0)
                                                            dtct.IDKB = qbnkb.First().IDKB;
                                                        dtct.MaDV = b.MaDV;
                                                        dtct.SoLuong = 1;
                                                        if (qbnkb.Count > 0 && qbnkb.First().NgayKham != null)
                                                            dtct.NgayNhap = qbnkb.First().NgayKham;
                                                        else
                                                            dtct.NgayNhap = bnhan.NNhap;
                                                        dtct.DonVi = b.DonVi;
                                                        dtct.DonGia = b.DonGia;
                                                        dtct.ThanhTien = b.DonGia;
                                                        dtct.TrongBH = 0; //item.TrongDM == null ? 0 : item.TrongDM.Value;
                                                        dtct.ThanhToan = 1;
                                                        dtct.TyLeTT = 100;
                                                        dtct.IDCD = id1;
                                                        _dataContext.DThuoccts.Add(dtct);
                                                        _dataContext.SaveChanges();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (Bien.MaBV == "12345" || Bien.MaBV == "24297")
                                    {
                                        #region Update chidinh.tamthu=1 gói dv cho cls
                                        _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
                                        var clsCD = (from cls in _dataContext.CLS.Where(o => o.MaBNhan == _mabn)
                                                     join cd in _dataContext.ChiDinhs.Where(o => o.IDGoi == IdGoiDV && o.Status != 1 && (o.TamThu != 1 || o.TamThu == null)) on cls.IdCLS equals cd.IdCLS
                                                     select cd).ToList();
                                        if (clsCD != null && clsCD.Count > 0)
                                        {
                                            using (TransactionScope scope = new TransactionScope())
                                            {
                                                foreach (var item in clsCD)
                                                {
                                                    var cdUpdate = _dataContext.ChiDinhs.FirstOrDefault(o => o.IDCD == item.IDCD);
                                                    if (cdUpdate != null)
                                                    {
                                                        cdUpdate.TamThu = 1;
                                                        cdUpdate.SoPhieu = _tamung.IDTamUng;
                                                    }
                                                }
                                                _dataContext.SaveChanges();
                                                scope.Complete();
                                            }
                                        }

                                        #endregion
                                    }
                                    #endregion
                                }
                                // update SoBienLai

                                //
                                _litamung = _dataContext.TamUngs.Where(p => p.MaBNhan == _int_maBN && p.PhanLoai != 5).OrderByDescending(p => p.IDTamUng).ToList();
                                grcTamUng.DataSource = _litamung.ToList();
                                EnableControl(false);

                                if (Bien.MaBV == "30005")
                                {
                                    btnIn_Click(sender, e);
                                }
                                else
                                {
                                    DialogResult _result = MessageBox.Show("Bạn muốn in phiếu tạm thu?", "Hỏi in", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (_result == DialogResult.Yes)
                                        btnIn_Click(sender, e);
                                }

                                break;
                            case 2:
                                TTLuutthu = 0;
                                if (!string.IsNullOrEmpty(txtIdTamUng.Text))
                                {
                                    int id = int.Parse(txtIdTamUng.Text);
                                    TamUng _tamungs = _dataContext.TamUngs.Single(p => p.IDTamUng == (id));
                                    bool _ngoaih2 = false;
                                    _ngoaih2 = DungChung.Ham.CheckNGioHC(dtNgayThu.DateTime);

                                    if (_ngoaih2 == true)
                                    {
                                        MessageBox.Show("Thu-Chi ngoài giờ HC");
                                        _tamungs.NgoaiGio = 1;
                                    }
                                    else
                                    {
                                        _tamungs.NgoaiGio = 0;
                                    }
                                    _tamungs.NgayThu = dtNgayThu.DateTime;
                                    _tamungs.MaKP = lupBPThu.EditValue == null ? 0 : Convert.ToInt32(lupBPThu.EditValue);
                                    _tamungs.MaCB = lupNguoiThu.EditValue.ToString();
                                    _tamungs.MaBNhan = _int_maBN;
                                    if (cboPLThu.SelectedIndex == 1)
                                    {
                                        if (!string.IsNullOrEmpty(cboKLKSK.Text))
                                            _tamungs.KetLuan = cboKLKSK.SelectedIndex;
                                    }
                                    else
                                    {
                                        if (chkKetLuan.Checked)
                                            _tamungs.KetLuan = 2;
                                        else
                                            _tamungs.KetLuan = -1;
                                    }
                                    if (!string.IsNullOrEmpty(cboPLThu.Text))
                                        _tamungs.PhanLoai = cboPLThu.SelectedIndex;
                                    _tamungs.SoTien = double.Parse(txtSoTien.Text);
                                    if (!string.IsNullOrEmpty(txtNoiDung.Text))
                                        _tamungs.LyDo = txtNoiDung.Text;
                                    if (!string.IsNullOrEmpty(txtSoTo.Text))
                                        _tamungs.SoTo = Convert.ToInt32(txtSoTo.Text);
                                    else
                                        _tamungs.SoTo = 0;
                                    _tamungs.QuyenHD = cbo_quyenTU.Text.Trim();
                                    _tamungs.SoHD = cbo_soHD_TU.Text.Trim();
                                    _dataContext.SaveChanges();
                                    _litamung = _dataContext.TamUngs.Where(p => p.MaBNhan == _int_maBN && p.PhanLoai != 5).OrderByDescending(p => p.IDTamUng).ToList();
                                    grcTamUng.DataSource = _litamung.ToList();
                                    EnableControl(false);
                                    if (Bien.MaBV == "30005")
                                    {
                                        btnIn_Click(sender, e);
                                    }
                                    else
                                    {
                                        DialogResult _result2 = MessageBox.Show("Bạn muốn in biên lai?", "Hỏi In", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                        if (_result2 == DialogResult.Yes)
                                            btnIn_Click(sender, e);
                                    }
                                }
                                break;
                        }
                        TTLuutthu = 0;

                    }
                    else
                    {
                        MessageBox.Show("Bệnh nhân đã thanh toán, bạn không thể thu tiền tạm ứng");
                    }
                }
            }
            finally
            {
                thuBangThe = false;
                //grvBNhantt_FocusedRowChanged(null, null);
            }
        }

        public void grvTamUng_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //List<TamUng> _ltung = new List<TamUng>();
            int id = 0;
            if (grvTamUng.RowCount > 0 && grvTamUng.GetFocusedRowCellValue(colIDTamUng) != null)
            {
                id = int.Parse(grvTamUng.GetFocusedRowCellValue(colIDTamUng).ToString());
                txtIdTamUng.Text = id.ToString();
            }
            else
            {
                txtIdTamUng.Text = "";
                return;
            }

            //    _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
            var _ltung = _dataContext.TamUngs.FirstOrDefault(p => p.IDTamUng == id);
            if (_ltung != null)
            {
                cbo_quyenTU.Text = _ltung.QuyenHD;
                cbo_soHD_TU.Text = _ltung.SoHD;
                if (_ltung.NgayThu != null)
                    dtNgayThu.DateTime = _ltung.NgayThu.Value;
                else
                    dtNgayThu.DateTime = System.DateTime.Now;
                lupBPThu.EditValue = _ltung.MaKP;
                lupNguoiThu.EditValue = _ltung.MaCB;
                if (_ltung.PhanLoai != null)
                    cboPLThu.SelectedIndex = _ltung.PhanLoai.Value;
                else
                    cboPLThu.SelectedIndex = -1;
                if (_ltung.SoTien != null)
                {
                    txtSoTien.Text = _ltung.SoTien.ToString();
                    txtSoTienBangChu.Text = DungChung.Ham.DocTienBangChu(_ltung.SoTien.Value, " đồng.");

                }
                if ((_ltung.SoTien > 0 && _ltung.PhanLoai == 3) || Bien.MaBV == "01071" || Bien.MaBV == "01049")
                {
                    btnTaoHD3.Visible = true;
                    btnXemHD.Visible = true;
                    if (!string.IsNullOrEmpty(_ltung.MaHD) || !string.IsNullOrEmpty(_ltung.FkeyVNPT))
                    {
                        btnXemHD.Enabled = true;
                        btnTaoHD3.Enabled = false;
                    }
                    else
                    {
                        btnTaoHD3.Enabled = true;
                        btnXemHD.Enabled = false;
                    }
                }
                else
                {
                    btnTaoHD3.Visible = false;
                    btnXemHD.Visible = false;
                }
                if (_ltung.KetLuan != null && _ltung.KetLuan.Value == 2)
                {
                    chkKetLuan.Checked = true;
                }
                else
                    chkKetLuan.Checked = false;
                //if (_ltung.First().LyDo != null)
                txtNoiDung.Text = _ltung.LyDo;
                if (_ltung.MaKP != null)
                    lupBPThu.EditValue = _ltung.MaKP;
                else
                    lupBPThu.EditValue = "";
            }
            else
            {
                cbo_quyenTU.Text = "";
                cbo_soHD_TU.Text = "";
                dtNgayThu.DateTime = System.DateTime.Now;
                lupNguoiThu.EditValue = "";
                lupBPThu.EditValue = "";
                txtSoTien.Text = "";
                txtSoTienBangChu.Text = "";
                cboPLThu.SelectedIndex = -1;
                chkKetLuan.Checked = false;
            }
            List<TamUngct> _ltuct = _dataContext.TamUngcts.Where(p => p.IDTamUng == id).Where(p => p.Status == 0).ToList();
            grcTamUngct.DataSource = _ltuct;
            btnThuBangThe.Enabled = false;

            //btnTaoHD3.Enabled = true;
        }

        private void txtSoTien_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSoTien.Text))
            {
                double stien = double.Parse(txtSoTien.Text);
                txtSoTienBangChu.Text = DungChung.Ham.DocTienBangChu(stien, " đồng.");
            }
        }

        #region Hóa đơn TT05/2016
        public static bool In_HoaDon_TT05(int ID)
        {
            BaoCao.rep_HDBH_08204 rep = new BaoCao.rep_HDBH_08204();
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
            frmIn frm = new frmIn();
            rep.lab_ngaythangnam.Text = "Ngày " + DateTime.Now.Day + "tháng " + DateTime.Now.Month + "năm " + DateTime.Now.Year;
            dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);

            var qhd = (from tamung in dataContext.TamUngs
                       join tamungct in dataContext.TamUngcts on tamung.IDTamUng equals tamungct.IDTamUng
                       join Dichvu in dataContext.DichVus on tamungct.MaDV equals Dichvu.MaDV
                       join bn in dataContext.BenhNhans on tamung.MaBNhan equals bn.MaBNhan
                       select new { bn.TenBNhan, bn.DChi, Dichvu.TenDV, Dichvu.DonVi, tamungct.DonGia, tamungct.SoLuong, tamungct.ThanhTien }).ToList();
            rep.DataSource = qhd;
            double tongtien = Math.Round(qhd.Sum(p => p.ThanhTien), 3);
            rep.lab_tongtien.Text = tongtien.ToString("##,###.###");
            rep.lab_ttchu.Text = DungChung.Ham.DocTienBangChu(tongtien, " đồng");
            //var dt = (from tamungct in _data.TamUngcts select tamungct).ToList();
            rep.Bindingdata();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
            return true;

        }

        #endregion
        private void btnIn_Click(object sender, EventArgs e)
        {
            //    if (Bien.MaBV == "19048" || Bien.MaBV=="24009")
            int ot;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out ot))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            int _idtthu = 0;
            if (!string.IsNullOrEmpty(txtIdTamUng.Text))
                _idtthu = int.Parse(txtIdTamUng.Text);

            if (Bien.MaBV == "20001")
                PhieuThuChi_C40_20001(_idtthu);
            else
                InPhieuTC_TT107(_idtthu, 0);



        }
        /// <summary>
        /// Kiểu in tự động hoặc theo mẫu; 0: tự động, 1, Theo mẫu
        /// </summary>
        int _kieu = 0;
        private void btnXem_Click(object sender, EventArgs e)
        {
            //try
            //{
            int ot;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out ot))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);


            if (Bien.MaBV == "30340" || DateTime.Now > Convert.ToDateTime("01/12/2018"))
            {
                DungChung.Ham.In_BangKe_2018(_dataContext, _int_maBN);
            }
            else
                DungChung.Ham.In_BangKe01_02(_dataContext, _int_maBN, _maubk, _kieu);
        }

        private void grvBNhantt_DataSourceChanged(object sender, EventArgs e)
        {
            //grvBNhantt_FocusedRowChanged(null, null);
        }

        private void cboNoitru_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void btnHuyDon_Click(object sender, EventArgs e)
        {
            bool _sua = true;
            _sua = DungChung.Ham.checkQuyen(xtraTThu.Name)[0];
            int ot;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out ot))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            if (_sua)
            {
                if (!string.IsNullOrEmpty(txtMaBNhan.Text))
                {
                    if (cboNoitru.SelectedIndex == 0)
                    {
                        if (cboTimTT.SelectedIndex == 0)
                        {
                            var kt = _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).OrderBy(p => p.PLDV).ToList();
                            if (kt.Where(p => p.PLDV == 1).ToList().Count > 0)
                            {
                                if (kt.Where(p => p.PLDV == 1).ToList().Count > 1)
                                {
                                    MessageBox.Show("Đây là BN điều trị ngoại trú, bạn không thể hủy đơn");
                                }
                                else
                                {
                                    DialogResult _resul = MessageBox.Show("Bạn muốn hủy đơn của BN: " + txtTenBenhNhan.Text, "Hủy đơn", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (_resul == DialogResult.Yes)
                                    {
                                        int id = 0;
                                        if (kt.First().PLDV == 1)
                                            id = kt.First().IDDon;
                                        frm_Check frm = new frm_Check(id, _int_maBN, 8, true);
                                        frm.ShowDialog();
                                        TimKiem();
                                    }
                                }
                            }
                            else
                            {
                                if (kt.Where(p => p.PLDV == 2).ToList().Count > 0)
                                {
                                    DialogResult _resul = MessageBox.Show("Bạn muốn hủy đơn của BN: " + txtTenBenhNhan.Text, "Hủy đơn", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (_resul == DialogResult.Yes)
                                    {
                                        int id = 0;
                                        frm_Check frm = new frm_Check(id, _int_maBN, 8, true);
                                        frm.ShowDialog();
                                        TimKiem();
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Chức năng hủy chỉ dùng cho BN chưa TT");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Chức năng hủy đơn chỉ dùng cho BN ngoại trú");
                    }
                }
                else
                {
                    MessageBox.Show("Không có BN để hủy");
                }
            }
        }
        List<CanBo> _lCanBo = new List<CanBo>();

        private void lupBPKe_EditValueChanged(object sender, EventArgs e)
        {
            lupNguoiTT.Properties.DataSource = _lCanBo.Where(p => p.Status == 1).ToList();
        }

        private void chkNgoaiH_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }
        int _maubk = 0;
        private void cboInBangKe_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idnhapd = 0;
            if (grvBNhantt.GetFocusedRowCellValue(colIDNhap) != null)
                idnhapd = Convert.ToInt32(grvBNhantt.GetFocusedRowCellValue(colIDNhap));
            switch (cboInBangKe.SelectedIndex)
            {
                case 0:

                    if (ckcDonNgoai.Checked == true)
                        InPhieuThuThuocNgoai(_dataContext, idnhapd);
                    else
                        _maubk = 5;
                    break;
                case 1:
                    _maubk = 4;
                    break;
                case 2:
                    _maubk = 41;
                    break;
                case 3:
                    // phiếu thanh toán ra viện mẫu 38
                    FormThamSo.frm_XemChiPhi._phieuTTRV_MS38(_dataContext, _mabn);
                    break;
                case 4:
                    FormThamSo.frm_XemChiPhi._phieuTTRV_MS40(_dataContext, _mabn);
                    break;
            }
            if (ckcDonNgoai.Checked == false)
            {
                if (cboInBangKe.SelectedIndex >= 0 && cboInBangKe.SelectedIndex <= 2)
                {
                    _kieu = 1;
                    DungChung.Ham.In_BangKe01_02(_dataContext, _mabn, _maubk, _kieu);
                }
                if (cboInBangKe.SelectedIndex >= 5)
                {
                    var vp1 = (from vp in _dataContext.VienPhis.Where(p => p.MaBNhan == _mabn)
                               join vpct in _dataContext.VienPhicts on vp.idVPhi equals vpct.idVPhi
                               select vpct).ToList();
                    double tienbnPT = vp1.Where(p => p.ThanhToan == 0).Sum(p => p.TBNTT) + vp1.Where(p => p.ThanhToan == 0).Sum(p => p.TBNCTT);
                    if (tienbnPT > 0)
                    {
                        MessageBox.Show("Số tiền bệnh nhân phải trả: " + tienbnPT.ToString("###,###.00"));
                    }
                    DungChung.Ham.In_BangKe_2018(_dataContext, _mabn);
                }
                _maubk = 0;
                cboInBangKe.SelectedIndex = -1;
            }
        }

        private void dtTimTuNgay_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void chkKSK_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkKSK.Checked)
            //{
            //    chkChiDinh.Checked = false;
            //}
            TimKiem();

        }


        //public void _getValue(string gt1, string gt2, string gt3)
        //{
        //    themmoitamungthutt(1);
        //    txtSoTien.Text = gt1;
        //    txtNoiDung.Text = gt2;
        //}
        List<FormNhap.frm_CPChiDinh.lCPCHiDinh> _lchidinh = new List<frm_CPChiDinh.lCPCHiDinh>();
        //public void _getValue(string gt1, string gt2, string gt3, bool tinhtamung, List<FormNhap.frm_CPChiDinh.lCPCHiDinh> lchidinh, int plthu, int _mack)
        //{
        //    themmoitamungthutt(1);
        //    txtSoTien.Text = gt1;
        //    txtNoiDung.Text = gt2;
        //    _lchidinh = lchidinh;
        //    chkKetLuan.Checked = tinhtamung;
        //    cboPLThu.SelectedIndex = plthu;
        //    if (lchidinh.Count > 0)
        //        lupBPThu.EditValue = lchidinh.First().MaKP;
        //    _maCKham = _mack;
        //}
        List<int> _lmack = new List<int>();
        public void _getValue(string gt1, string gt2, string gt3, bool tinhtamung, List<FormNhap.frm_CPChiDinh.lCPCHiDinh> lchidinh, int plthu, List<int> _lmack, int IdGoiDV, bool _thuBangThe)
        {
            this.thuBangThe = _thuBangThe;
            themmoitamungthutt(1);
            txtIDGoiDV.Text = IdGoiDV.ToString();
            txtSoTien.Text = gt1;
            txtNoiDung.Text = gt2;
            _lchidinh = lchidinh;
            chkKetLuan.Checked = tinhtamung;
            cboPLThu.SelectedIndex = plthu;
            if (lchidinh.Count > 0)
                lupBPThu.EditValue = lchidinh.First().MaKP;
            this._lmack = _lmack;
        }

        bool _thutrucTiep = false;
        int _maCKham = 0;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                _thutrucTiep = true;
                int ot;
                int _int_maBN = 0;
                _maCKham = 0;
                _lchidinh.Clear();
                if (Int32.TryParse(txtMaBNhan.Text, out ot))
                    _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
                frm_CPChiDinh frm = new frm_CPChiDinh(_int_maBN);
                frm.GetData = new frm_CPChiDinh._getstring(_getValue);
                frm.ShowDialog();
                if (_lchidinh.Count == 0)
                    return;
                btnLuutthu_Click(sender, e);
                grvTamUng_FocusedRowChanged(null, null);
            }
            finally
            {
                _thutrucTiep = false;
            }

        }

        private void chkChiDinh_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkChiDinh.Checked)
            //    chkKSK.Checked = false;
            TimKiem();
        }

        private void grvBNhantt_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (grvBNhantt.GetRowCellValue(e.RowHandle, colDTuong) != null && grvBNhantt.GetRowCellValue(e.RowHandle, colDTuong).ToString() != "BHYT")
            {
                e.Appearance.ForeColor = Color.Blue;
            }
        }

        private void btnThuChiTT_Click(object sender, EventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            // lưu thu chi thanh toán
            if (_int_maBN > 0)
            {
                if (DungChung.Ham.KTraTT(_dataContext, _int_maBN))
                {
                    DialogResult _result = MessageBox.Show("Bạn muốn duyệt chi phí của bệnh nhân: " + txtTenBenhNhan.Text, "Hỏi duyệt", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (KtraTCTT(_int_maBN, 2) && _result == DialogResult.Yes)
                    {

                        xtraTThu.Text = "Tạm ứng|Thu-Chi TT*";
                        EnableControl(true);
                        lupNguoiThu.EditValue = Bien.MaCB;
                        lupBPThu.EditValue = Bien.MaKP;
                        dtNgayThu.DateTime = System.DateTime.Now;
                        txtSoTien.Text = "";
                        txtSoTienBangChu.Text = "";
                        if (_HTTT == 3)
                        {
                            cboPLThu.SelectedIndex = 1;
                            cboKLKSK.SelectedIndex = 1;
                        }

                        TTLuutthu = 1;
                        double _tientu = 0, _tienbn = 0;
                        double _tienchenh = 0;
                        var tu = _dataContext.TamUngs.Where(p => p.MaBNhan == _int_maBN).ToList();
                        if (tu.Where(p => p.PhanLoai == 0).ToList().Count > 0)
                            _tientu = tu.Where(p => p.PhanLoai == 0).Sum(p => p.SoTien).Value;
                        var tienbn = (from vp in _dataContext.VienPhis.Where(p => p.MaBNhan == _int_maBN)
                                      join vpct in _dataContext.VienPhicts on vp.idVPhi equals vpct.idVPhi
                                      select new { vpct.TienBN, vpct.ThanhTien }).ToList();
                        if (tienbn.Count > 0)
                            _tienbn = tienbn.Sum(p => p.TienBN);
                        _tienchenh = _tientu - _tienbn;
                        if (_tienchenh > 0)
                        {
                            cboPLThu.SelectedIndex = 2;
                            txtSoTien.Text = _tienchenh.ToString();
                            txtNoiDung.Text = "Chi trả tiền cho bệnh nhân";
                        }
                        else
                        {
                            cboPLThu.SelectedIndex = 1;
                            txtSoTien.Text = (_tienchenh * -1).ToString();
                            txtNoiDung.Text = "Thu tiền của bệnh nhân";
                        }
                        cboPLThu.Focus();
                        btnLuutthu_Click(sender, e);
                    }

                }
                else
                {
                    MessageBox.Show("Bệnh nhân chưa TT, bạn không thể duyệt");
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn bệnh nhân");
            }

            //
        }

        private void grvThanhToan_DataSourceChanged(object sender, EventArgs e)
        {
            grvThanhToan_FocusedRowChanged(null, null);
        }

        private void grvTamUng_DataSourceChanged(object sender, EventArgs e)
        {
            grvTamUng_FocusedRowChanged(null, null);
        }

        private void xtraTToan_TThu_SelectedPageChanging(object sender, DevExpress.XtraTab.TabPageChangingEventArgs e)
        {
            if (xtraTThu.Text.Contains("*"))
            {
                DialogResult _result = MessageBox.Show("Bạn chưa lưu dữ liệu, Bạn có muốn lưu không?", "thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_result == DialogResult.Yes)
                {
                    btnLuutthu_Click(sender, e);
                }
                xtraTThu.Text = "Tạm ứng|Thu-Chi TT";
                TTLuutthu = 0;
            }
            int thuchi = 1;

        }
        public void _getValue(string noidung, int soto, int ketluan, double sotien)
        {
            txtNoiDungTC.Text = noidung;
            txtSoTo.Text = soto.ToString();
            cboKLKSK.SelectedIndex = ketluan;
            txtSoTienTC.Text = sotien.ToString();
            txtSoTienCP.Text = sotien.ToString();
        }
        int _khongmangphieutamthu = 0;
        public bool _ktQuyen_SoHD(QLBV_Database.QLBVEntities _data, int ploai, string _quyen, string _so)
        {
            if (string.IsNullOrEmpty(_so))
                return false;
            var kt = _data.TamUngs.Where(p => p.PhanLoai == ploai && p.QuyenHD == _quyen && p.SoHD == _so).ToList();
            if (kt.Count > 0)
                return true;
            return false;
        }
        bool checkDuyet = false;
        bool _KoTU = false;
        public void GetValueDuyet(string noidung, bool cd, bool KoTU)
        {
            _KoTU = KoTU;
            checkDuyet = cd;
            txtNoiDungTC.Text = noidung;
        }
        void xoachitamung(int mabn)
        {
            QLBV_Database.QLBVEntities _db = new QLBV_Database.QLBVEntities(Bien.StrCon);
            var tu = _db.TamUngs.Where(p => p.MaBNhan == mabn && p.PhanLoai == 4).ToList();
            foreach (var item in tu)
            {
                _db.TamUngs.Remove(item);
                _db.SaveChanges();
            }
        }
        bool chitamung(int maBnhan)
        {
            try
            {
                var a = QuyenSoBL._getQuyen_SoBL(2, "");
                if (a != null)
                {
                    themmoitamungthutt(2);
                    dtNgayThu.DateTime = dtNgayTC.DateTime;
                    cboPLThu.SelectedIndex = 4;
                    cboPLThu_SelectedIndexChanged(null, null);

                    //var a = QuyenSoBL._getQuyen_SoBL(4, "");
                    //cbo_quyenTU.Text = a.FirstOrDefault().Quyen;
                    //cbo_soHD_TU.Text = a.FirstOrDefault().So != null ? (a.FirstOrDefault().So + 1).ToString() : "";
                    btnLuutthu_Click(null, null);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi tạo chi tạm ứng");
                return false;
            }

        }

        private bool CheckPL1(int maBNhan)
        {
            bool result = false;

            //    _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
            var tamUng = _dataContext.TamUngs.FirstOrDefault(o => o.MaBNhan == maBNhan);
            if (tamUng != null)
                result = true;

            return result;
        }

        private void btnDuyet_Click(object sender, EventArgs e)
        {
            //    _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            bool _sua = true;
            if (Bien.MaBV == "30009" || Bien.MaBV == "30010" || Bien.MaBV == "30007" || Bien.MaBV == "30002")
                _sua = DungChung.Ham.checkQuyenFalse(xtraDuyet.Name)[0];
            else
                _sua = DungChung.Ham.checkQuyen(xtraDuyet.Name)[0];
            if (Bien.MaBV == "30002" && tienbn == 0)
                _sua = true;
            if (_sua == false)
            {
                MessageBox.Show("Chức năng bị khóa");
            }
            if (tudongduyet)
                _sua = true;

            var ktdt = _dataContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN).ToList();
            if (_sua)
            {
                _khongmangphieutamthu = 0;
                bool hienbienlai = true;
                // lưu thu chi thanh toán

                if (_int_maBN > 0 && DungChung.Ham._checkNgayKhoa(_dataContext, dtNgayTC.DateTime, "KhoaVP") == false)
                {
                    if (ktdt.Count > 0 && (ktdt.First().DTuong == "KSK"))
                    {
                        ChucNang.frm_ThuKSK frm = new ChucNang.frm_ThuKSK(_int_maBN);
                        frm.GetData = new ChucNang.frm_ThuKSK._getstring(_getValue);
                        frm.ShowDialog();
                        radThuChi.SelectedIndex = 0;
                        lupBPTC.EditValue = Bien.MaKP;
                        lupCBTC.EditValue = Bien.MaCB;
                        var a = QuyenSoBL._getQuyen_SoBL(radThuChi.SelectedIndex + 1, "");
                        if (a != null)
                        {
                            if (string.IsNullOrEmpty(cbo_Quyen.Text))
                                cbo_Quyen.Text = a.FirstOrDefault().Quyen;
                            if (string.IsNullOrEmpty(cboSoHD.Text))
                                cboSoHD.Text = (a.FirstOrDefault().So + 1).ToString();

                            if (_ktQuyen_SoHD(_dataContext, (cboPLThu.SelectedIndex == 4 ? -1 : cboPLThu.SelectedIndex), cbo_Quyen.Text.Trim(), cboSoHD.Text.Trim()))
                            {
                                DialogResult _result2 = MessageBox.Show("Số hóa đơn đã được sử dụng, bạn vẫn muốn lưu?");
                                if (_result2 == DialogResult.No)
                                    return;
                            }
                            btlLuuTC_Click(sender, e);
                        }
                    }
                    else
                    {
                        bool _duyet = true;
                        if (!DungChung.Ham.KTraTT(_dataContext, _int_maBN))
                        {
                            MessageBox.Show("Bệnh nhân chưa TT, bạn không thể duyệt TT");
                            return;
                        }
                        var qtienbn = (from vp in _dataContext.VienPhis.Where(p => p.MaBNhan == _int_maBN)
                                       join vpct in _dataContext.VienPhicts on vp.idVPhi equals vpct.idVPhi
                                       select new { vp.NgayTT, vp.Status, vpct }).ToList();
                        DateTime ngaytt = DateTime.Now;
                        if (qtienbn.Count > 0 && qtienbn.First().NgayTT != null)
                            ngaytt = qtienbn.First().NgayTT.Value;
                        if ((dtNgayTC.DateTime - ngaytt).Milliseconds <= 0)
                        {
                            MessageBox.Show("Ngày duyệt: " + dtNgayTC.DateTime.ToString("dd/MM/yyyy HHmm") + " phải > ngày TT:" + ngaytt.ToString("dd/MM/yyyy HHmm"));
                            return;
                        }
                        if (Bien.MaBV == "30009" || Bien.MaBV == "30007")
                        {
                            if (qtienbn.Count > 0 && (qtienbn.First().Status == null || qtienbn.First().Status == 0))
                            {
                                MessageBox.Show("Chưa duyệt chi phí của bệnh nhân, bạn không thể duyệt thanh toán");
                                return;
                            }
                        }
                        checkDuyet = false;
                        if (Bien.MaBV == "30003" || Bien.MaBV == "30005" || Bien.MaBV == "30012" || Bien.MaBV == "24272")
                        {
                            string _muc = "", dtuong = "", kvuc = "", muchuong = "";
                            int dungtuyen = 0;
                            if (ktdt.Count > 0)
                            {
                                _muc = ktdt.First().MucHuong.ToString();
                                kvuc = ktdt.First().KhuVuc;
                                dungtuyen = ktdt.First().Tuyen ?? 0;
                                dtuong = ktdt.First().DTuong;
                            }
                            int _hangbv = DungChung.Ham.hangBV(Bien.MaBV);
                            if (dtuong == "BHYT")
                            {
                                if (dungtuyen == 1)
                                {
                                    muchuong = 100 - DungChung.Ham._PtramTT(_dataContext, _muc) + "%";
                                }
                                else if (dungtuyen == 2)
                                {
                                    switch (_hangbv)
                                    {
                                        case 1:

                                            muchuong = 100 - DungChung.Ham._PtramTT(_dataContext, _muc) * 0.4 + "%";
                                            break;
                                        case 2:
                                            if (Bien.MaBV == "01830")
                                                muchuong = 100 - DungChung.Ham._PtramTT(_dataContext, _muc) * 0.7 + "%";
                                            else
                                                muchuong = 100 - +DungChung.Ham._PtramTT(_dataContext, _muc) * 0.6 + "%";
                                            break;
                                        case 3:
                                            if (kvuc.ToLower().Contains("k"))
                                                muchuong = 100 - +DungChung.Ham._PtramTT(_dataContext, _muc) + "%";
                                            else
                                                muchuong = 100 - +DungChung.Ham._PtramTT(_dataContext, _muc) * 0.7 + "%";
                                            break; ;
                                        case 4:
                                            muchuong = 100 - +DungChung.Ham._PtramTT(_dataContext, _muc) + "%";
                                            break;
                                    }
                                }

                                if (Bien.MaBV == "30003")
                                {
                                    string asa = muchuong;
                                    muchuong = "thành tiền " + asa + " bệnh nhân cùng chi trả BHYT";
                                    if (ktdt.First().NoiTru == 0)
                                    {
                                        muchuong += " ngoại trú";
                                    }
                                    else
                                    {
                                        var kprv = (from rv in _dataContext.RaViens.Where(p => p.MaBNhan == _int_maBN)
                                                    join kp in _dataContext.KPhongs on rv.MaKP equals kp.MaKP
                                                    select new { kp.TenKP }).ToList();
                                        if (kprv.Count() > 0)
                                        {
                                            muchuong += " tại " + kprv.First().TenKP;
                                        }
                                    }
                                }
                                else if (Bien.MaBV == "30012")
                                {
                                    string asa = muchuong;
                                    muchuong = "Thu tiền " + asa + " BHYT";
                                }
                                else if (Bien.MaBV == "24272")
                                {
                                    muchuong = "Thu "+ muchuong + " BHYT Bệnh nhân cùng chi trả.";
                                }
                                else
                                {
                                    muchuong += " cùng chi trả BHYT";
                                }
                            }
                            else
                            {
                                muchuong = "Thanh toán viện phí";
                            }
                            ChucNang.frm_CheckDuyet frm = new ChucNang.frm_CheckDuyet(tudongduyet, muchuong);
                            frm.getdata = new ChucNang.frm_CheckDuyet.getString(GetValueDuyet);
                            frm.ShowDialog();
                        }
                        else
                        {
                            ChucNang.frm_CheckDuyet frm = new ChucNang.frm_CheckDuyet(tudongduyet);
                            frm.getdata = new ChucNang.frm_CheckDuyet.getString(GetValueDuyet);
                            frm.ShowDialog();
                        }
                        if (checkDuyet == false)
                            return;

                        if (!KtraTCTT(_int_maBN, 2))
                        {
                            return;
                        }
                        //thay đổi bằng việc thiết lập hệ thống 30-01 đức
                        // if (Bien.MaBV == "30002" || Bien.MaBV == "27183" || Bien.MaBV == "20001" || Bien.MaBV == "01071")
                        if (Bien.ChiTamUng && _KoTU == false)
                            if (!chitamung(_int_maBN))
                                return;
                        lupCBTC.EditValue = Bien.MaCB;
                        if (lupBPKe.EditValue != null)
                            lupBPTC.EditValue = lupBPKe.EditValue;
                        else
                            lupBPTC.EditValue = Bien.MaKP;

                        if (_HTTT == 3)
                        {
                            cboKLKSK.SelectedIndex = 1;
                        }
                        //    _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
                        TTLuutthu = 1;
                        double _tientu = 0, _tienbn = 0, _tiendatt = 0;
                        double _tienchenh = 0, _tienconlaiphainop = 0;
                        var tu = _dataContext.TamUngs.Where(p => p.MaBNhan == _int_maBN).ToList();
                        if (tu.Where(p => p.PhanLoai == 0).ToList().Count > 0)
                            _tientu = tu.Where(p => p.PhanLoai == 0).Sum(p => p.SoTien).Value;
                        if (tu.Where(p => p.PhanLoai == 4).ToList().Count > 0)
                            _tientu = _tientu - tu.Where(p => p.PhanLoai == 4).Sum(p => p.SoTien).Value;
                        if (tu.Where(p => p.PhanLoai == 3).ToList().Count > 0)
                            _tiendatt = tu.Where(p => p.PhanLoai == 3).Sum(p => p.SoTien).Value;
                        if (_tientu > 0 && Bien.MaBV == "30003")
                        {
                            DialogResult _resultkl = MessageBox.Show("Bệnh nhân có mang phiếu tạm thu?", "Hỏi phiếu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (_resultkl == DialogResult.No)
                                _khongmangphieutamthu = 4;
                        }
                        if (_KoTU)
                        {
                            _khongmangphieutamthu = 4;
                        }
                        if (qtienbn.Count > 0)
                        {
                            _tienbn = qtienbn.Where(p => p.vpct.ThanhToan == 0).Sum(p => p.vpct.TBNCTT);
                        }
                        //_tienconlaiphainop = (_tienbn - _tiendatt);
                        if (_khongmangphieutamthu == 4)
                        {
                            _tienchenh = (_tienbn);
                            radThuChi.SelectedIndex = 0;
                            txtSoTienTC.Text = _tienchenh.ToString();
                            if (String.IsNullOrEmpty(txtNoiDungTC.Text))
                                txtNoiDungTC.Text = "Thu tiền của bệnh nhân";
                            txtSoTienCP.Text = _tienbn.ToString();
                        }
                        else
                        {
                            _tienchenh = (Bien.MaBV == "01830" || Bien.MaBV == "30003") ? _tienbn : (_tientu - _tienbn);//01830 chỉ tạo phiếu thu khi duyệt thanh toán a quý y/c 14-03 đức
                            if (Bien.MaBV == "01830" || Bien.MaBV == "30003")
                            {
                                radThuChi.SelectedIndex = 0;
                                txtSoTienTC.Text = _tienchenh.ToString();
                                if (String.IsNullOrEmpty(txtNoiDungTC.Text))
                                    txtNoiDungTC.Text = "Thu tiền của bệnh nhân";
                                txtSoTienCP.Text = _tienbn.ToString();
                            }
                            else
                            {
                                if (_tienchenh > 0)
                                {
                                    radThuChi.SelectedIndex = 1;
                                    txtSoTienTC.Text = _tienchenh.ToString();
                                    if (String.IsNullOrEmpty(txtNoiDungTC.Text))
                                        txtNoiDungTC.Text = Bien.MaBV != "30005" ? "Chi trả tiền cho bệnh nhân" : "";
                                    txtSoTienCP.Text = _tienchenh.ToString();
                                }
                                else
                                {
                                    if (_tienchenh == 0)
                                    {
                                        txtSoTienTC.Text = "0";
                                        if (String.IsNullOrEmpty(txtNoiDungTC.Text))
                                            txtNoiDungTC.Text = Bien.MaBV != "30005" ? "Bệnh nhân không mất tiền" : "";
                                        radThuChi.SelectedIndex = 0;
                                        txtSoTienCP.Text = _tienchenh.ToString();
                                        //hienbienlai = false;
                                    }
                                    else
                                    {
                                        radThuChi.SelectedIndex = 0;
                                        txtSoTienTC.Text = (_tienchenh * -1).ToString();
                                        if (String.IsNullOrEmpty(txtNoiDungTC.Text))
                                            txtNoiDungTC.Text = "Thu tiền của bệnh nhân";
                                        txtSoTienCP.Text = (_tienchenh * -1).ToString();
                                    }
                                }
                            }
                        }
                        //_tienconlaiphainop.ToString();

                        // số biên lai

                        var a = QuyenSoBL._getQuyen_SoBL(radThuChi.SelectedIndex + 1, "");
                        if (a != null)
                        {
                            if (string.IsNullOrEmpty(cbo_Quyen.Text) && _tienchenh != 0)
                                cbo_Quyen.Text = a.FirstOrDefault().Quyen;
                            if (string.IsNullOrEmpty(cboSoHD.Text) && _tienchenh != 0)
                                cboSoHD.Text = (a.FirstOrDefault().So + 1).ToString();
                            if (_ktQuyen_SoHD(_dataContext, (cboPLThu.SelectedIndex == 4 ? -1 : cboPLThu.SelectedIndex), cbo_Quyen.Text.Trim(), cboSoHD.Text.Trim()) && cboSoHD.Text != "0")
                            {
                                DialogResult _result2 = MessageBox.Show("Số hóa đơn đã được sử dụng, bạn vẫn muốn lưu?");
                                if (_result2 == DialogResult.No)
                                    return;
                            }
                            //
                            if (hienbienlai)
                                btlLuuTC_Click(sender, e);
                        }
                        else
                        {
                            return;
                        }

                        int noingoai = CheckNoiNgoaiTru(_int_maBN);
                        if ((noingoai == 0 && (Bien.PPXuat_BHXH[0] == 1 || Bien.PPXuat_BHXH[0] == 2)) || (noingoai == 1 && (Bien.PPXuat_BHXH[1] == 1 || Bien.PPXuat_BHXH[1] == 2)))
                        {
                            List<DungChung.Cls79_80.cl_79_80> _listVPBH = new List<DungChung.Cls79_80.cl_79_80>();
                            _listVPBH.Add(new DungChung.Cls79_80.cl_79_80 { Ma_bn = _int_maBN, Export = false });
                            BHYT.us_Export_XML_2348.user = Bien.xmlFilePath_LIS[10];
                            BHYT.us_Export_XML_2348.user = Bien.xmlFilePath_LIS[11];
                            BHYT.us_Export_XML_2348 clsBHXH = new BHYT.us_Export_XML_2348();
                            clsBHXH._updateExPort(_dataContext, _listVPBH, _int_maBN, false, 1);
                        }

                    }

                }
                else
                {
                    MessageBox.Show("Bạn chưa chọn bệnh nhân");
                }
            }
            #region chuyển trạng thái in tự động  VienPhi.InDonTuDong = 1
            if (Bien.MaBV == "34019" || Bien.MaBV == "30007")
            {
                VienPhi qvp = _dataContext.VienPhis.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                if (qvp != null)
                {
                    if (qvp.NgayDuyet != null && qvp.NgayDuyet.Value > new DateTime(2000, 1, 1))
                    {
                        var dthuoc0 = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == qvp.MaBNhan.Value).Where(p => p.MaKXuat != null)
                                       join dtct in _dataContext.DThuoccts.Where(p => p.Status == 0) on dt.IDDon equals dtct.IDDon
                                       group new { dt, dtct } by new { dt.IDDon, dt.MaKXuat, dtct.MaDV } into kq
                                       select new { kq.Key.IDDon, kq.Key.MaKXuat, kq.Key.MaDV, SoLuong = kq.Sum(p => p.dtct.SoLuong) }).ToList();
                        var qkp = dthuoc0.Select(p => p.MaKXuat.Value).Distinct().ToList();
                        foreach (var a in qkp)
                        {
                            var dthuoc = (from dt in dthuoc0.Where(p => p.MaKXuat == a)
                                          join dv in _lthuoc on dt.MaDV equals dv.MaDV
                                          select new { dt.IDDon, dv.MaDV, dv.TenDV, dt.SoLuong }
                                        ).OrderBy(p => p.TenDV).ToList();
                            if (dthuoc.Count > 0)
                            {
                                List<int> _lidDon = dthuoc.Select(p => p.IDDon).Distinct().ToList();
                                foreach (int iddon in _lidDon)
                                {
                                    var qthuocin = dthuoc.Where(p => p.IDDon == iddon).ToList();
                                    #region in đơn
                                    BaoCao.rep_InDonTuDong_34019 rep = new BaoCao.rep_InDonTuDong_34019();
                                    frmIn frm = new frmIn();
                                    rep.DataSource = qthuocin;

                                    rep.lblTenBNhan.Text = ktdt.First().TenBNhan.ToUpper();
                                    rep.lblMaBNhan.Text = "Mã BN: " + txtMaBNhan.Text;
                                    rep.lblIdDon.Text = "IDDon: " + iddon.ToString();

                                    //var qlaninlai = _dataContext.InLaiDons.Where(p => p.MaBNhan ==_int_maBN).FirstOrDefault();
                                    //if (qlaninlai != null)
                                    //{
                                    //    rep.lblLanIn.Text = "Lần: " + qlaninlai.LanInLai + 1;
                                    //}
                                    rep.databinding();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    // frm.Show();
                                    try
                                    {
                                        PrinterSettings printer = new PrinterSettings();
                                        var qkp1 = _dataContext.KPhongs.Where(p => p.MaKP == a).FirstOrDefault();
                                        if (qkp1 != null && qkp1.MayInDefault != null && qkp1.MayInDefault != "")
                                            printer.PrinterName = qkp1.MayInDefault;
                                        else
                                            printer.PrinterName = "";

                                        if (printer.IsValid)
                                        {
                                            Printer.SetDefaultPrinter(printer.PrinterName);
                                            ReportPrintTool printtool = new ReportPrintTool(rep);
                                            printtool.Print();
                                        }
                                        //else
                                        //    MessageBox.Show("Không tìm thấy máy in");
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }

                                    #region tự động in
                                    #endregion
                                    // frm.Close();

                                    #endregion
                                }
                            }


                        }

                        var qPhongKT = _dataContext.KPhongs.Where(p => p.MaKP == Bien.MaKP).FirstOrDefault();
                        if (qPhongKT != null && qPhongKT.MayInDefault != null && qPhongKT.MayInDefault != "")
                        {
                            Printer.SetDefaultPrinter(qPhongKT.MayInDefault);
                        }

                    }
                }
            }
            #endregion
            //int ot = 0;
            //if (Int32.TryParse(txtMaBNhan.Text, out ot))
            //{
            //    _mabn = Convert.ToInt32(txtMaBNhan.Text);
            //    if (_mabn > 0)
            //    {

            //        //if (vphi != null && vphi.NgayDuyet != null && vphi.NgayDuyet != ngayduyet && checkThuTien)
            //        //    ckDuyet.ReadOnly = false;
            //        //else
            //        //    ckDuyet.Enabled = true;
            //    }
            //    //if (Bien.MaBV == "30009")
            //    //    updateStatus(_mabn);
            //}

        }
        public static class Printer
        {
            [DllImport("winspool.drv", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern bool SetDefaultPrinter(string Printer);

        }

        private void _update_IDTU(QLBV_Database.QLBVEntities _data, int _id, bool _update)
        {
            int _mabn = 0;
            var ngayduyet = _data.TamUngs.Where(p => p.IDTamUng == _id).ToList();
            if (ngayduyet.Count > 0)
                _mabn = ngayduyet.First().MaBNhan == null ? 0 : ngayduyet.First().MaBNhan.Value;
            if (_update)
            {
                DateTime _ngayduyet = Convert.ToDateTime("01/01/2000");

                if (ngayduyet.Count > 0 && ngayduyet.First().NgayThu != null)
                    _ngayduyet = ngayduyet.First().NgayThu.Value;
                int idvp = 0;
                var updatevp = (from vp in _data.VienPhis.Where(p => p.MaBNhan == _mabn) select vp).ToList();
                foreach (var a in updatevp)
                {
                    idvp = a.idVPhi;
                    if (_ngayduyet.Year > 2000)
                    {
                        a.NgayDuyet = _ngayduyet;
                        _data.SaveChanges();
                    }

                }
                var update = (from vpct in _data.VienPhicts.Where(p => p.idVPhi == idvp)
                              select vpct).ToList();

                foreach (var a in update)
                {

                    if (a.IDTamUng == null || a.IDTamUng == 0)
                    {
                        a.IDTamUng = _id;
                        _data.SaveChanges();
                    }
                }
            }
            else
            {
                var updatevp = (from vpct in _data.VienPhis.Where(p => p.MaBNhan == _mabn)
                                select vpct).ToList();
                foreach (var a in updatevp)
                {
                    a.NgayDuyet = null;
                    _data.SaveChanges();
                }
                var update = (from vpct in _data.VienPhicts.Where(p => p.IDTamUng == _id)
                              select vpct).ToList();
                foreach (var a in update)
                {

                    a.IDTamUng = 0;
                    _data.SaveChanges();
                }
            }

        }

        private void btlLuuTC_Click(object sender, EventArgs e)
        {
            try
            {
                int rs;
                int _int_maBN = 0;
                if (Int32.TryParse(txtMaBNhan.Text, out rs))
                    _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
                bool _tthai = false;

                var bn = _dataContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN).ToList();
                var ttbx = _dataContext.TTboXungs.Where(p => p.MaBNhan == _int_maBN).FirstOrDefault();
                if (bn.Count > 0)
                {
                    if (bn.First().DTuong == "KSK")
                        _tthai = true;
                    else
                    {
                        _tthai = DungChung.Ham.KTraTT(_dataContext, _int_maBN);
                        if (_tthai == false)
                        {
                            MessageBox.Show("Bệnh nhân chưa thanh toán, bạn không thể duyệt");
                            return;
                        }
                    }
                }

                var kttu = _dataContext.TamUngs.Where(p => p.PhanLoai == 1 || p.PhanLoai == 2).Where(p => p.MaBNhan == _int_maBN).ToList();
                if (kttu.Count > 0)
                {
                    MessageBox.Show("Bệnh nhân đã được duyệt, bạn không thể duyệt thêm");
                    return;
                }
                if (KTLuuTC())
                {
                    if (_tthai == true)
                    {
                        var vphi = _dataContext.VienPhis.FirstOrDefault(o => o.MaBNhan == _int_maBN);
                        TTLuutthu = 0;
                        int pl = radThuChi.SelectedIndex + 1;
                        TamUng _tamung = new TamUng();
                        bool _ngoaih = false;
                        _ngoaih = DungChung.Ham.CheckNGioHC(dtNgayThu.DateTime);

                        if (_ngoaih == true)
                        {
                            MessageBox.Show("Thu-Chi ngoài giờ HC");
                            _tamung.NgoaiGio = 1;
                        }
                        else
                        {
                            _tamung.NgoaiGio = 0;
                        }
                        _tamung.QuyenHD = cbo_Quyen.Text.Trim();
                        _tamung.SoHD = cboSoHD.Text.Trim();
                        _tamung.NgayThu = dtNgayTC.DateTime;
                        _tamung.MaKP = lupBPTC.EditValue == null ? 0 : Convert.ToInt32(lupBPTC.EditValue);
                        _tamung.MaCB = lupCBTC.EditValue == null ? "" : lupCBTC.EditValue.ToString();
                        _tamung.MaBNhan = _int_maBN;
                        _tamung.PhanLoai = pl;
                        //_tamung.ThanhToan = Convert.ToInt32(ckcthanhtoan.Checked);
                        if (radThuChi.SelectedIndex == 0)
                        {
                            if (!string.IsNullOrEmpty(cboKLKSK.Text))
                                _tamung.KetLuan = cboKLKSK.SelectedIndex;
                        }
                        else
                        {
                            _tamung.KetLuan = _khongmangphieutamthu;
                        }
                        if (!string.IsNullOrEmpty(txtSoTienCP.Text))
                            _tamung.SoTien = double.Parse(txtSoTienCP.Text);
                        else
                            _tamung.SoTien = 0;
                        if (!string.IsNullOrEmpty(txtSoTienTC.Text))
                            _tamung.TienChenh = double.Parse(txtSoTienTC.Text);

                        if (!string.IsNullOrEmpty(txtNoiDungTC.Text))
                            _tamung.LyDo = txtNoiDungTC.Text;
                        //else
                        if (!string.IsNullOrEmpty(txtSoTo.Text))
                            _tamung.SoTo = Convert.ToInt32(txtSoTo.Text);
                        else
                            _tamung.SoTo = 0;
                        if (ttbx != null && ttbx.HTThanhToan == 1)
                            _tamung.ThanhToan = 0;
                        else
                            _tamung.ThanhToan = 1;
                        _dataContext.TamUngs.Add(_tamung);
                        _dataContext.SaveChanges();
                        // update SoBienLai
                        try
                        {
                            SoBienLai soBL = _dataContext.SoBienLais.Where(p => p.PLoai == pl && p.Quyen == cbo_Quyen.Text).FirstOrDefault();
                            if (soBL != null)
                            {
                                int soht = soBL.SoHT + 1;
                                if (soht == soBL.SoDen)
                                {
                                    soBL.Status = 2;
                                    soBL.SoHT = soht;
                                    _dataContext.SaveChanges();
                                    MessageBox.Show("quyển hóa đơn: " + cbo_quyenTU.Text + "đã sử dụng hết, \nthiết lập thêm quyển khác để sử dụng cho hóa đơn sau");
                                }
                                else
                                {
                                    soBL.SoHT = soht;
                                    _dataContext.SaveChanges();
                                }
                            }
                        }
                        catch { }
                        //
                        //    _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
                        double tienBN = 0;
                        var tu = _dataContext.TamUngs.Where(p => p.MaBNhan == _int_maBN && (p.PhanLoai == 1 || p.PhanLoai == 2)).ToList();
                        int _id = 0;
                        if (tu.Count > 0)
                        {
                            tienBN = tu.Sum(p => p.TienChenh);
                            _id = tu.First().IDTamUng;
                        }
                        _update_IDTU(_dataContext, _id, true);
                        //_litamung = _dataContext.TamUngs.Where(p => p.MaBNhan== (txtMaBNhan.Text)).OrderByDescending(p => p.IDTamUng).ToList();
                        //grcTamUng.DataSource = _litamung.ToList();
                        EnableControl(false);
                        grvBNhantt_FocusedRowChanged(null, null);

                        int noingoai = CheckNoiNgoaiTru(_int_maBN);
                        var ktrgui = _dataContext.VienPhis.Where(p => p.MaBNhan == _int_maBN).ToList();
                        if (ktrgui.Count > 0 && ktrgui.First().ExportBHXH == false)//nếu đã gửi thì ko tự động gửi lại nữa
                        {
                            if ((noingoai == 0 && (Bien.PPXuat_BHXH[0] == 1 || Bien.PPXuat_BHXH[0] == 2)) || (noingoai == 1 && (Bien.PPXuat_BHXH[1] == 1 || Bien.PPXuat_BHXH[1] == 2)))
                            {
                                List<DungChung.Cls79_80.cl_79_80> _listVPBH = new List<DungChung.Cls79_80.cl_79_80>();
                                _listVPBH.Add(new DungChung.Cls79_80.cl_79_80 { Ma_bn = _int_maBN, Export = false });
                                BHYT.us_Export_XML_2348.user = Bien.xmlFilePath_LIS[10];
                                BHYT.us_Export_XML_2348.user = Bien.xmlFilePath_LIS[11];
                                BHYT.us_Export_XML_2348 clsBHXH = new BHYT.us_Export_XML_2348();
                                clsBHXH._updateExPort(_dataContext, _listVPBH, _int_maBN, false, 1);
                            }
                        }
                        if (tienBN > 1)
                        {
                            if (Bien.MaBV == "30005")
                            {
                                btnInTC_Click(sender, e);
                            }
                            else
                            {
                                DialogResult _result = MessageBox.Show("Bạn muốn in biên lai?", "Hỏi In", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (_result == DialogResult.Yes)
                                    btnInTC_Click(sender, e);
                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnInTC_Click(object sender, EventArgs e)
        {
            //if (Bien.MaBV == "19048")
            //{
            int _idtthu = 0;
            if (!string.IsNullOrEmpty(txtIDtamungtc.Text))
                _idtthu = int.Parse(txtIDtamungtc.Text);
            int ot;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out ot))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            //InPhieuThuChi(_idtthu, _int_maBN);
            InPhieuTC_TT107(_idtthu, 0);
        }

        private void btnHuyDuyet_Click(object sender, EventArgs e)
        {
            //    _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
            bool _sua = true;
            if (Bien.MaBV == "30009" || Bien.MaBV == "30007" || Bien.MaBV == "30002")
                _sua = DungChung.Ham.checkQuyenFalse(xtraDuyet.Name)[2];
            else
                _sua = DungChung.Ham.checkQuyen(xtraDuyet.Name)[2];
            if (_sua == false)
            {
                MessageBox.Show("Chức năng bị khóa");
            }

            //Chưa có tên đăng nhập
            if (Bien.MaBV == "30372" && Bien.TenDN != "abc")
            {
                MessageBox.Show("Tài khoản không có quyền hủy duyệt!");
                return;
            }

            if (tudongduyet)
                _sua = true;

            if (_sua)
            {
                if (!string.IsNullOrEmpty(txtMaBNhan.Text) && DungChung.Ham._checkNgayKhoa(_dataContext, dtNgayTC.DateTime, "KhoaVP") == false)
                {
                    int mabn = Convert.ToInt32(txtMaBNhan.Text);
                    // VienPhi vp = _dataContext.VienPhis.Where(p => p.MaBNhan == mabn && p.Status != null && p.Status == 1).FirstOrDefault();
                    var qVP = (from vp in _dataContext.VienPhis.Where(p => p.MaBNhan == mabn)
                               join vpct in _dataContext.VienPhicts on vp.idVPhi equals vpct.idVPhi
                               group new { vp, vpct } by new { vp.idVPhi, vp.Status } into kq
                               select new { kq.Key.Status, kq.Key.idVPhi, ThanhTienBN = kq.Sum(p => p.vpct.TienBN) }).FirstOrDefault();

                    if (Bien.MaBV == "30009" || Bien.MaBV == "30002")
                    {
                        var duyet = _dataContext.NhapDs.Where(p => p.MaBNhan == mabn).FirstOrDefault();
                        if (duyet != null)
                        {
                            MessageBox.Show("Bệnh nhân đã xuất dược, bạn không thể hủy duyệt");
                            return;
                        }
                    }
                    if (!string.IsNullOrEmpty(txtIDtamungtc.Text))
                    {
                        int id = int.Parse(txtIDtamungtc.Text);
                        //Đã tạo hóa đơn không thể hủy duyệt
                        var check = _dataContext.TamUngs.FirstOrDefault(p => p.IDTamUng == (id));
                        if (Bien.MaBV == "24009")
                        {
                            if (check != null && (check.StatusHD == 1 || check.StatusHD == 2))
                            {
                                MessageBox.Show("Đã tạo hóa đơn không thể hủy duyệt", "Thông báo");
                                return;
                            }
                        }
                        else
                        {
                            if (check != null && !string.IsNullOrWhiteSpace(check.transactionID))
                            {
                                MessageBox.Show("Đã tạo hóa đơn không thể hủy duyệt", "Thông báo");
                                return;
                            }
                        }

                        DialogResult _result = DialogResult.No;
                        _result = MessageBox.Show("Bạn muốn hủy duyệt BN: " + txtTenBenhNhan.Text, "Hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (_result == DialogResult.Yes)
                        {
                            _ktmatkhau = false;
                            ChucNang.frm_CheckPass frm = new ChucNang.frm_CheckPass();
                            frm.ok = new ChucNang.frm_CheckPass._getdata(_getValue);
                            frm.ShowDialog();
                            if (_ktmatkhau)
                            {
                                string tb = "Nhập lý do hủy duyệt thanh toán: " + mabn.ToString();
                                QLBV.frm_GetLyDoLog frm1 = new frm_GetLyDoLog(tb);
                                frm1.ok = new frm_GetLyDoLog._getdata(_getLyDoLog);
                                frm1.ShowDialog();
                                if (!string.IsNullOrEmpty(_lydoLodg))
                                    _ktmatkhau = true;
                                else
                                    _ktmatkhau = false;
                            }
                            if (_ktmatkhau)
                            {
                                var _xoa = _dataContext.TamUngs.Single(p => p.IDTamUng == (id));
                                var xoaNGayDuyetVP = _dataContext.VienPhis.Where(panelControl1 => panelControl1.MaBNhan == mabn).FirstOrDefault();
                                if (xoaNGayDuyetVP != null)
                                {
                                    xoaNGayDuyetVP.NgayDuyet = null;
                                }
                                _dataContext.TamUngs.Remove(_xoa);
                                if (_dataContext.SaveChanges() > 0)
                                {
                                    LOG moi = new LOG();
                                    moi.DateLog = DateTime.Now;
                                    moi.LyDo = _lydoLodg;
                                    moi.UserName = Bien.TenDN;
                                    moi.MaBNhan = mabn;
                                    moi.IdForm = 904;
                                    moi.ComputerName = SystemInformation.ComputerName;
                                    moi.MaCB = Bien.MaCB;
                                    moi.Status = 5;
                                    _dataContext.LOGs.Add(moi);
                                    _dataContext.SaveChanges();
                                    _lydoLodg = "";

                                    _update_IDTU(_dataContext, id, false);

                                    if (Bien.ChiTamUng)
                                        xoachitamung(mabn);

                                    grvBNhantt_FocusedRowChanged(null, null);
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không có bệnh nhân để xóa");
                }

                grvBNhantt_FocusedRowChanged(null, null);
            }
        }

        private void txtSoTienTC_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSoTienTC.Text))
            {
                double stien = double.Parse(txtSoTienTC.Text);
                txtBangChuTC.Text = DungChung.Ham.DocTienBangChu(stien, " đồng.");
            }
        }

        private void grvBNhantt_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colXemHSBN")
            {
                int ot;
                int _int_maBN = 0;
                if (Int32.TryParse(txtMaBNhan.Text, out ot))
                    _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
                frmHSBNNhapMoi frm = new frmHSBNNhapMoi(2, _int_maBN, 1);
                frm.ShowDialog();
            }
        }

        private void grvThanhToan_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colSua")
            {
                bool _sua = true;
                _sua = DungChung.Ham.checkQuyen(xtraTToan.Name)[1];
                if (_sua == false)
                {
                    MessageBox.Show("Chức năng bị khóa");
                }
                else
                {
                    int _mabn = 0, _madv = 0;
                    int _trongBH = -1;
                    int xhh = 0, _makp = 0;
                    _mabn = String.IsNullOrEmpty(txtMaBNhan.Text) ? 0 : Convert.ToInt32(txtMaBNhan.Text);
                    if (grvThanhToan.GetFocusedRowCellValue(colMaDV) != null)
                        _madv = Convert.ToInt32(grvThanhToan.GetFocusedRowCellValue(colMaDV));
                    if (grvThanhToan.GetFocusedRowCellValue(colTrongBH) != null)
                        _trongBH = Convert.ToInt32(grvThanhToan.GetFocusedRowCellValue(colTrongBH));
                    int _mien = Convert.ToInt32(grvThanhToan.GetFocusedRowCellValue(colMien));
                    int _tyleTT = Convert.ToInt32(grvThanhToan.GetFocusedRowCellValue(colTyLeTT));

                    if (grvThanhToan.GetFocusedRowCellValue(colXHH) != null)
                    {
                        if (grvThanhToan.GetFocusedRowCellValue(colXHH).ToString() == "True")
                            xhh = 1;
                    }
                    if (grvThanhToan.GetFocusedRowCellValue(colMaKPkd) != null)
                    {
                        _makp = Convert.ToInt32(grvThanhToan.GetFocusedRowCellValue(colMaKPkd));
                    }
                    var ktratt = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn)
                                  join dtct in _dataContext.DThuoccts.Where(p => p.MaDV == _madv).Where(p => p.ThanhToan == 0) on dt.IDDon equals dtct.IDDon//|| (p.IDCD != null && p.IDCD > 0)
                                  select dtct).FirstOrDefault();
                    if (ktratt != null)
                    {
                        ChucNang.frm_UpdateTrongDM frm = new ChucNang.frm_UpdateTrongDM(_mabn, _madv, _trongBH, _mien, xhh, _tyleTT, _makp);
                        frm.ShowDialog();
                        grvBNhantt_FocusedRowChanged(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Dịch vụ đã thu trực tiếp hoặc dịch vụ đã qua chỉ định, không thể sửa");
                    }
                    //   MessageBox.Show("Đang được nâng cấp!.... " + _madv);
                }
            }
        }

        private void txt_TongCPTT_EditValueChanged(object sender, EventArgs e)
        {
            double stien = 0;
            if (!string.IsNullOrEmpty(txt_TongCPTT.Text))
            {
                stien = double.Parse(txt_TongCPTT.Text);

            }
            txt_TongCPTT_c.Text = DungChung.Ham.DocTienBangChu(stien, " đồng.");
        }

        private void txt_TongTamThu_EditValueChanged(object sender, EventArgs e)
        {
            double stien = 0;
            if (!string.IsNullOrEmpty(txt_TongTamThu.Text))
            {
                stien = double.Parse(txt_TongTamThu.Text);

            }
            txt_TongTamThu_c.Text = DungChung.Ham.DocTienBangChu(stien, " đồng.");
        }

        private void txt_TienTra_EditValueChanged(object sender, EventArgs e)
        {
            double stien = 0;
            if (!string.IsNullOrEmpty(txt_TienTra.Text))
            {
                stien = double.Parse(txt_TienTra.Text);

            }
            txt_TienTra_c.Text = DungChung.Ham.DocTienBangChu(stien, " đồng.");
        }

        private void txt_TienThu_EditValueChanged(object sender, EventArgs e)
        {
            double stien = 0;
            if (!string.IsNullOrEmpty(txt_TienThu.Text))
            {
                stien = double.Parse(txt_TienThu.Text);

            }
            txt_TienThu_c.Text = DungChung.Ham.DocTienBangChu(stien, " đồng.");
        }

        private void txt_CPDT_EditValueChanged(object sender, EventArgs e)
        {
            double stien = 0;
            if (!string.IsNullOrEmpty(txt_CPDT.Text.Trim()))
            {
                stien = Convert.ToDouble(txt_CPDT.Text);

            }
            txt_CPDT_c.Text = DungChung.Ham.DocTienBangChu(stien, " đồng.");
        }

        private void txtDaTT_EditValueChanged(object sender, EventArgs e)
        {
            double stien = 0;
            if (!string.IsNullOrEmpty(txtDaTT.Text))
            {
                stien = double.Parse(txtDaTT.Text);

            }
            txtDaTT_c.Text = DungChung.Ham.DocTienBangChu(stien, " đồng.");
        }
        #region _InPhieuThuChi
        #region phiếu chi việt yên
        public static bool InPhieuChi_VY(QLBV_Database.QLBVEntities _data, int id)
        {
            int _ID = id;
            BaoCao.Rep_Chitamung_new rep = new BaoCao.Rep_Chitamung_new();
            rep.Quyen.Value = "Quyển số: ...............";
            rep.So.Value = "Số: ............";
            rep.No.Value = "Nợ:.............";
            rep.Co.Value = "Có:.............";
            var q = _data.TamUngs.Where(p => p.IDTamUng == _ID).ToList();
            if (q.Count > 0 && q.First().PhanLoai == 4)
            {
                int _Mbn = q.First().MaBNhan == null ? 0 : Convert.ToInt32(q.First().MaBNhan);
                var q1 = _data.BenhNhans.Where(p => p.MaBNhan == _Mbn).ToList();
                if (q1.Count > 0)
                {

                    rep.MaBN.Value = q1.First().MaBNhan;
                    rep.Hoten.Value = q1.First().TenBNhan;
                    rep.Diachi.Value = q1.First().DChi;
                    rep.Lydochi.Value = q.First().LyDo;
                    rep.Sotien.Value = q.First().SoTien;
                    double st = Convert.ToDouble(q.First().SoTien);
                    rep.Quyen.Value = "Quyển số: " + q.First().QuyenHD;
                    rep.So.Value = "Số: " + q.First().SoHD;

                    rep.Bangchu.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng/.");
                    DateTime ngay = Convert.ToDateTime(q.First().NgayThu);
                    rep.Ngaythang.Value = DungChung.Ham.NgaySangChu(ngay);

                    if (Bien.MaBV == "01071" || Bien.MaBV == "01049" || Bien.MaBV == "12345" || Bien.MaBV == "24297" || Bien.MaBV == "56789")
                    {
                        rep.lbltit.Text = "PHIẾU HOÀN TẠM ỨNG";
                        rep.lblKy1.Text = "";
                        rep.lblKy21.Text = "";
                        rep.lblKy2.Text = "";
                        rep.lblKy22.Text = "";
                        rep.lblKy3.Text = "Người trả tiền";
                        rep.lblKy33.Text = Bien.NguoiLapBieu;
                        rep.lblKy4.Text = "";
                        rep.lblKy24.Text = "";
                        rep.lblKy35.Text = q1.First().TenBNhan;
                    }
                    else if (Bien.MaBV == "30003")
                    {
                        var a = _data.CanBoes.Where(p => p.MaCB == (Bien.MaCB)).ToList();
                        if (a.Count > 0)
                            rep.lblKy33.Text = a.First().TenCB;
                    }
                    frmIn frm = new frmIn();
                    rep.CreateDocument();
                    rep.DataMember = "Table";
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                    return true;
                }
                return false;
            }

            return false;
        }

        public static bool InPhieuChi_24009(QLBV_Database.QLBVEntities _data, int id)
        {
            int _ID = id;
            BaoCao.Rep_PhieuChi_24009_new rep = new BaoCao.Rep_PhieuChi_24009_new();
            rep.xrLabel63.Text = "Số: " + id.ToString();
            rep.xrLabel64.Text = "Số: " + id.ToString();
            rep.Quyen.Value = "Quyển số: ...............";
            rep.So.Value = "Số: ............";
            rep.No.Value = "Nợ:.............";
            rep.Co.Value = "Có:.............";
            var q = _data.TamUngs.Where(p => p.IDTamUng == _ID).ToList();
            if (q.Count > 0)
            {
                if (q.First().PhanLoai == 4 || q.First().PhanLoai == 2 || q.First().PhanLoai == 6)
                {
                    rep.TieuDe.Value = "PHIẾU CHI";
                    rep.xrTableTren.Visible = false;
                    rep.xrTabletren2.Visible = false;
                    rep.xrTableDuoi.Visible = true;
                    rep.xrTable3.Visible = true;
                }
                else
                {
                    rep.TieuDe.Value = "PHIẾU THU";
                    rep.xrLabel10.Text = rep.xrLabel41.Text = "Họ và tên người nộp tiền:";
                    rep.xrTableTren.Visible = true;
                    rep.xrTabletren2.Visible = true;
                    rep.xrTableDuoi.Visible = false;
                    rep.xrTable3.Visible = false;
                }
                int _Mbn = q.First().MaBNhan == null ? 0 : Convert.ToInt32(q.First().MaBNhan);
                var q1 = _data.BenhNhans.Where(p => p.MaBNhan == _Mbn).ToList();
                if (q1.Count > 0)
                {

                    rep.MaBN.Value = q1.First().MaBNhan;
                    rep.Hoten.Value = q1.First().TenBNhan;
                    rep.Diachi.Value = q1.First().DChi;
                    rep.Lydochi.Value = q.First().LyDo;
                    double st = Convert.ToDouble(q.First().SoTien);
                    string fomat = "{0:#,#}";
                    if (Bien.FormatString[1] != "")
                        fomat = Bien.FormatString[1];
                    rep.Sotien.Value = string.Format(fomat, st) + " VNĐ";
                    rep.Quyen.Value = "Quyển số: " + q.First().QuyenHD;
                    rep.So.Value = "Số: " + q.First().SoHD;

                    rep.Bangchu.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng/.");
                    DateTime ngay = Convert.ToDateTime(q.First().NgayThu);
                    rep.Ngaythang.Value = DungChung.Ham.NgaySangChu(ngay);

                    if (Bien.MaBV == "01071" || Bien.MaBV == "01049" || Bien.MaBV == "12345" || Bien.MaBV == "24297" || Bien.MaBV == "56789")
                    {
                        rep.lbltit.Text = "PHIẾU HOÀN TẠM ỨNG";
                        rep.lblKy1.Text = "";
                        rep.lblKy21.Text = "";
                        rep.lblKy2.Text = "";
                        rep.lblKy22.Text = "";
                        rep.lblKy3.Text = "Người trả tiền";
                        rep.lblKy33.Text = Bien.NguoiLapBieu;
                        rep.lblKy35.Text = q1.First().TenBNhan;
                    }
                    else if (Bien.MaBV == "30003")
                    {
                        var a = _data.CanBoes.Where(p => p.MaCB == (Bien.MaCB)).ToList();
                        if (a.Count > 0)
                            rep.lblKy33.Text = a.First().TenCB;
                    }
                    frmIn frm = new frmIn();
                    rep.CreateDocument();
                    rep.DataMember = "Table";
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                    return true;
                }
                return false;
            }

            return false;
        }
        #endregion
        #region phiếu thu- chi thái nguyên
        public static bool _phieuThu_Chi_TN(QLBV_Database.QLBVEntities _Data, int _Sophieu)
        {
            try
            {

                var q = _Data.TamUngs.Where(p => p.IDTamUng == _Sophieu).ToList();
                int _MaBN = 0;
                if (q.Count > 0)
                {
                    _MaBN = q.First().MaBNhan == null ? 0 : q.First().MaBNhan.Value;
                    if (q.First().PhanLoai == 1)
                    {
                        BaoCao.Rep_PhieuThuTN rep = new BaoCao.Rep_PhieuThuTN();
                        frmIn frm = new frmIn();
                        rep.Quyenso.Value = "Quyển số:" + q.First().QuyenHD;
                        rep.Soct.Value = "Số CT:" + q.First().SoHD;
                        var a = _Data.BenhNhans.Where(p => p.MaBNhan == _MaBN).ToList();
                        if (a.Count > 0)
                        {
                            rep.Hoten.Value = a.First().TenBNhan.ToUpper();
                            rep.MaBN.Value = a.First().MaBNhan;
                            int _MKP = q.First().MaKP == null ? 0 : q.First().MaKP.Value;
                            var a1 = _Data.KPhongs.Where(p => p.MaKP == _MKP).ToList();
                            if (a1.Count > 0)
                            {
                                rep.Khoa.Value = a1.First().TenKP;
                            }
                            rep.DTuong.Value = a.First().DTuong;
                            rep.NamSinh.Value = a.First().NamSinh;
                            rep.Sotien.Value = q.First().TienChenh.ToString("##,###") + " đồng";
                            double st = q.First().TienChenh;
                            rep.Bangchu.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng/.");
                            DateTime ngay = Convert.ToDateTime(q.First().NgayThu);
                            rep.Ngaythang.Value = DungChung.Ham.NgaySangChu(ngay);

                            rep.Tenphieu.Value = "BIÊN LAI THU TIỀN PHÍ, LỆ PHÍ";
                            rep.Lydo.Value = q.First().LyDo;
                            rep.Nguoinhan.Value = "";
                            rep.Nguoithu.Value = Bien.NguoiLapBieu;
                            var TamUng = _Data.TamUngs.Where(p => p.MaBNhan == _MaBN && p.PhanLoai == 0).Sum(p => p.SoTien);
                            if (TamUng != null)
                                rep.TienUng.Value = TamUng.Value.ToString("##,###");
                            rep.CreateDocument();
                            //rep.DataMember = "Table";
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                            return true;
                        }
                    }
                    if (q.First().PhanLoai == 2)
                    {
                        BaoCao.Rep_PhieuChiTN rep = new BaoCao.Rep_PhieuChiTN();
                        frmIn frm = new frmIn();
                        rep.Quyenso.Value = "Quyển số:" + q.First().QuyenHD;
                        rep.Soct.Value = "Số CT:" + q.First().SoHD;
                        var a = _Data.BenhNhans.Where(p => p.MaBNhan == _MaBN).ToList();
                        if (a.Count > 0)
                        {
                            rep.Hoten.Value = a.First().TenBNhan.ToUpper();
                            rep.MaBN.Value = a.First().MaBNhan;
                            rep.DTuong.Value = a.First().DTuong;
                            int _MKP = q.First().MaKP == null ? 0 : q.First().MaKP.Value;
                            var a1 = _Data.KPhongs.Where(p => p.MaKP == _MKP).ToList();
                            if (a1.Count > 0)
                            {
                                rep.Khoa.Value = a1.First().TenKP;
                            }
                            rep.NamSinh.Value = a.First().NamSinh;
                            rep.Sotien.Value = q.First().TienChenh.ToString("##,###") + " đồng";
                            double st = q.First().TienChenh;
                            rep.Bangchu.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng/.");
                            DateTime ngay = Convert.ToDateTime(q.First().NgayThu);
                            rep.Ngaythang.Value = DungChung.Ham.NgaySangChu(ngay);

                            rep.Tenphieu.Value = "PHIẾU CHI";
                            rep.Lydo.Value = q.First().LyDo;
                            rep.Nguoinhan.Value = "";
                            rep.Nguoithu.Value = Bien.NguoiLapBieu;

                            rep.CreateDocument();
                            //rep.DataMember = "Table";
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                            return true;
                        }
                    }
                    if (q.First().PhanLoai == 0)
                    {
                        BaoCao.Rep_TamUngTN rep = new BaoCao.Rep_TamUngTN();
                        frmIn frm = new frmIn();
                        rep.QuyenSo.Value = "Quyển số:" + q.First().QuyenHD;
                        rep.SoCT.Value = "Số CT:" + q.First().SoHD;
                        rep.Ngayin.Value = "Ngày in:" + System.DateTime.Now.ToString();
                        var a = _Data.BenhNhans.Where(p => p.MaBNhan == _MaBN).ToList();
                        if (a.Count > 0)
                        {
                            rep.TenBN.Value = a.First().TenBNhan.ToUpper();
                            rep.NamSinh.Value = a.First().NamSinh;
                            rep.MaBN.Value = a.First().MaBNhan;
                            rep.Diachi.Value = a.First().DChi;
                            int _MKP = q.First().MaKP == null ? 0 : Convert.ToInt32(q.First().MaKP);
                            var a1 = _Data.KPhongs.Where(p => p.MaKP == _MKP).ToList();
                            if (a1.Count > 0)
                            {
                                rep.Khoa.Value = a1.First().TenKP;
                            }
                            rep.DTuong.Value = a.First().DTuong;
                            if (a.First().SThe != null)
                            {
                                rep.Sothe.Value = a.First().SThe;
                            }
                            rep.Lydo.Value = q.First().LyDo;
                            rep.Sotien.Value = q.First().SoTien.Value.ToString("##,###");
                            double st = q.First().SoTien.Value;
                            rep.Bangchu.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng/.");
                            DateTime ngay = Convert.ToDateTime(q.First().NgayThu);
                            rep.ngaythangnam.Value = DungChung.Ham.NgaySangChu(ngay);
                            rep.CreateDocument();
                            //rep.DataMember = "Table";
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                            return true;
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        #region phiếu bình giang
        private bool _InPhieu_BinhGiang(QLBV_Database.QLBVEntities _Data, int _Sophieu)
        {
            var q = _Data.TamUngs.Where(p => p.IDTamUng == _Sophieu).ToList();
            int _MaBN = 0;
            if (q.Count > 0)
            {
                _MaBN = q.First().MaBNhan == null ? 0 : q.First().MaBNhan.Value;
                BaoCao.Rep_BienLaiBG rep = new BaoCao.Rep_BienLaiBG();
                frmIn frm = new frmIn();
                rep.QuyenSo.Value = "Quyển số:" + q.First().QuyenHD;
                rep.SoCT.Value = "Số CT:" + q.First().SoHD;
                var a = _Data.BenhNhans.Where(p => p.MaBNhan == _MaBN).ToList();
                if (a.Count > 0)
                {
                    rep.TenBN.Value = a.First().TenBNhan;
                    rep.MaBN.Value = a.First().MaBNhan;
                    rep.NamSinh.Value = a.First().NamSinh;
                    rep.Diachi.Value = a.First().DChi;
                    rep.Sothe.Value = a.First().SThe;
                    int _MKP = q.First().MaKP == null ? 0 : q.First().MaKP.Value;
                    var a1 = _Data.KPhongs.Where(p => p.MaKP == _MKP).ToList();
                    if (a1.Count > 0)
                    {
                        rep.Khoa.Value = a1.First().TenKP;
                    }
                    rep.Sotien.Value = q.First().SoTien.Value.ToString("##,###");
                    double st = q.First().SoTien.Value;
                    rep.Bangchu.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng/.");
                    DateTime ngay = Convert.ToDateTime(q.First().NgayThu);
                    rep.ngaythangnam.Value = DungChung.Ham.NgaySangChu(ngay);
                    var a2 = (from vp in _Data.VienPhis.Where(p => p.MaBNhan == _MaBN)
                              join vpct in _Data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                              select new { vpct.MaDV, vpct.DonGia, vpct.SoLuong, vpct.ThanhTien, vpct.TrongBH, vpct.TienBH, vpct.TienBN }).ToList();
                    if (a2.Count > 0)
                    {
                        double st1 = 0, st2 = 0;
                        st1 = a2.Where(p => p.TrongBH == 1).Sum(p => p.TienBN);
                        rep.BHYT.Value = st1;
                        st2 = a2.Where(p => p.TrongBH == 0).Sum(p => p.TienBN);
                        rep.NgoaiDM.Value = st2;
                        rep.STPhaiNop.Value = st1 + st2;
                    }
                    rep.CreateDocument();
                    //rep.DataMember = "Table";
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                    return true;
                }
            }
            return false;
        }
        #endregion
        #region Phiếu tạm thuViệt Yên
        public static bool _InPhieuTamThu_VY(QLBV_Database.QLBVEntities data, int _Sophieu)
        {
            try
            {
                frmIn frm = new frmIn();
                BaoCao.repTamUngVienPhi_A4 rep = new BaoCao.repTamUngVienPhi_A4();
                int _idtthu = _Sophieu;
                string _tronggio = "";
                var q = (from bn in data.BenhNhans
                         join tt in data.TamUngs on bn.MaBNhan equals tt.MaBNhan
                         join cb in data.CanBoes on tt.MaCB equals cb.MaCB
                         join kp in data.KPhongs on tt.MaKP equals kp.MaKP
                         where (tt.IDTamUng == _idtthu)
                         select new { tt.SoHD, bn.MaBNhan, tt.QuyenHD, bn.SThe, tt.PhanLoai, bn.SoTT, bn.TenBNhan, bn.DTuong, bn.NamSinh, bn.NoiTru, kp.TenKP, bn.DChi, tt.SoTien, cb.TenCB, tt.LyDo, tt.NgayThu, tt.IDTamUng, tt.NgoaiGio, tt.IDTamUngThe }).ToList();
                if (q.Count > 0)
                {
                    string tenloaiphi = "";
                    rep.txtmabn.Text = "Mã BN: " + q.First().MaBNhan;
                    rep.IDTamUngThe = q.First().IDTamUngThe;
                    rep.txtmabn2.Text = "Mã BN: " + q.First().MaBNhan;
                    rep.TieuDe.Value = "PHIẾU TẠM THU TIỀN";
                    rep.CBTHU.Value = q.First().TenCB;
                    if (Bien.MaBV == "01071" || Bien.MaBV == "01049" || Bien.MaBV == "12345" || Bien.MaBV == "24297" || Bien.MaBV == "56789")
                        rep.TieuDe.Value = "PHIẾU TẠM THU TIỀN VIỆN PHÍ";
                    tenloaiphi = "Tạm ứng khám bệnh";

                    if (q.First().DTuong == "BHYT" && Bien.MaBV == "30009")
                        rep.SoThe.Value = "Số thẻ BHYT:" + q.First().SThe;
                    rep.QuyenHD.Value = q.FirstOrDefault().QuyenHD;
                    rep.TenBNhan.Value = q.First().TenBNhan.ToUpper();
                    if (Bien.MaBV == "01071" || Bien.MaBV == "01049" || Bien.MaBV == "12345" || Bien.MaBV == "24297" || Bien.MaBV == "56789")
                    {
                        rep.colnguoinop1.Text = "";//q.First().TenBNhan;
                        rep.colnguoinop2.Text = "";//q.First().TenBNhan;
                    }
                    //if (Bien.MaBV == "12121")
                    //{
                    //    rep.colnguoinop1.Text = q.First().TenBNhan;
                    //    rep.colnguoinop2.Text = q.First().TenBNhan;
                    //}
                    rep.NamSinh.Value = q.First().NamSinh;
                    rep.DChi.Value = q.First().DChi;
                    rep.SoPhieu.Value = Bien.MaBV == "24009" ? (q.First().IDTamUng == null ? "" : q.First().IDTamUng.ToString()) : q.First().SoHD;
                    if (q.First().NgoaiGio != null)
                        if (q.First().NgoaiGio.Value == 1)
                        {
                            _tronggio = "Ngoài giờ";
                        }
                        else
                        {
                            _tronggio = "Trong giờ";
                        }
                    string _tenloai = "";
                    if (q.First().DTuong == "KSK")
                    {
                        _tenloai = "Tên loại phí, lệ phí:Tạm ứng khám sức khỏe";
                    }
                    else
                    {
                        if (q.First().DTuong == "BHYT")
                        {
                            _tenloai = "Tên loại phí, lệ phí: " + tenloaiphi + " - BHYT";
                        }
                        else
                        {
                            _tenloai = "Tên loại phí, lệ phí: " + tenloaiphi + " - Thu phí";
                        }
                        if (q.First().NoiTru.Value == 0)
                            _tenloai += " - Ngoại trú";
                        else
                            _tenloai += " - Nội trú";
                    }
                    if (q.First().NoiTru == 0)
                    {
                        rep.KyHieu.Value = "PTNgT" + System.DateTime.Now.Year;
                    }
                    else
                    {
                        rep.KyHieu.Value = "PTNT" + System.DateTime.Now.Year;
                    }
                    rep.TenLoai.Value = _tenloai;
                    rep.SoTT.Value = "STT:" + q.First().SoTT;
                    rep.TenKP.Value = q.First().TenKP; // gan sau
                    rep.LyDoThu.Value = q.First().LyDo;
                    double sotien = 0;
                    if (q.First().SoTien != null)
                    {
                        rep.SoTien.Value = q.First().SoTien;
                        sotien = Convert.ToDouble(q.First().SoTien);
                    }
                    rep.TenCB.Value = q.First().TenCB;
                    if (Bien.MaBV == "01071" || Bien.MaBV == "01049" || Bien.MaBV == "12345" || Bien.MaBV == "24297" || Bien.MaBV == "56789")
                        rep.CBTHU.Value = q.First().TenCB;
                    if (q.First().NgayThu != null)
                    {
                        if (Bien.MaBV == "01071" || Bien.MaBV == "01049" || Bien.MaBV == "12345" || Bien.MaBV == "24297" || Bien.MaBV == "56789")
                            rep.NgayThang.Value = DungChung.Ham.NgaySangChu(q.First().NgayThu.Value, 3);
                        else
                            rep.NgayThang.Value = DungChung.Ham.NgaySangChu(q.First().NgayThu.Value);

                    }
                    if (q.First().PhanLoai != 7)
                    {
                        rep.HTTT.Value = "Hình thức thanh toán: tiền mặt";
                    }
                    else
                    {
                        if (DungChung.Bien.MaBV == "01071")
                        {
                            rep.HTTT.Value = "Hình thức thanh toán: Chuyển khoản";
                        }
                        else
                        {
                            rep.HTTT.Value = "Hình thức thanh toán: tiền mặt";
                        }
                        
                    }
                    rep.DocTien.Value = DungChung.Ham.DocTienBangChu(sotien, " đồng.");
                }
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        #region Phiếu tạm thu
        public static BaoCao.repTamUngVienPhi_A5_01830 _InPhieuTamThu_Nhieulien_(QLBV_Database.QLBVEntities data, int _Sophieu, string lien)
        {
            BaoCao.repTamUngVienPhi_A5_01830 rep = new BaoCao.repTamUngVienPhi_A5_01830();
            int _idtthu = _Sophieu;
            string _tronggio = "";
            var q = (from bn in data.BenhNhans
                     join tt in data.TamUngs on bn.MaBNhan equals tt.MaBNhan
                     join cb in data.CanBoes on tt.MaCB equals cb.MaCB
                     join kp in data.KPhongs on tt.MaKP equals kp.MaKP
                     where (tt.IDTamUng == _idtthu)
                     select new { tt.SoHD, tt.QuyenHD, bn.MaBNhan, bn.SThe, tt.PhanLoai, bn.SoTT, bn.TenBNhan, bn.DTuong, bn.NamSinh, bn.NoiTru, kp.TenKP, bn.DChi, tt.SoTien, cb.TenCB, tt.LyDo, tt.NgayThu, tt.IDTamUng, tt.NgoaiGio }).ToList();
            if (q.Count > 0)
            {

                string tenloaiphi = "";
                rep.MaBNhan.Value = q.First().MaBNhan;
                rep.TieuDe.Value = "PHIẾU TẠM THU TIỀN";
                tenloaiphi = "Tạm ứng khám bệnh";

                if (q.First().DTuong == "BHYT" && Bien.MaBV == "30009")
                    rep.SoThe.Value = "Số thẻ BHYT:" + q.First().SThe;
                rep.TenBNhan.Value = q.First().TenBNhan.ToUpper();
                if (Bien.MaBV == "01071" || Bien.MaBV == "01049" || Bien.MaBV == "12345" || Bien.MaBV == "24297" || Bien.MaBV == "56789")
                {
                    rep.txtNguoiNop1_3.Text = q.First().TenBNhan;
                    //rep.txtNguoiNop2_3.Text = q.First().TenBNhan;
                }
                rep.NamSinh.Value = q.First().NamSinh;
                rep.DChi.Value = q.First().DChi;
                rep.SoPhieu.Value = q.First().IDTamUng;
                rep.Quyen.Value = q.First().QuyenHD;
                rep.So.Value = q.First().SoHD;
                if (q.First().NgoaiGio != null)
                    if (q.First().NgoaiGio.Value == 1)
                    {
                        _tronggio = "Ngoài giờ";
                    }
                    else
                    {
                        _tronggio = "Trong giờ";
                    }
                string _tenloai = "";
                if (q.First().DTuong == "KSK")
                {
                    _tenloai = "Tên loại phí, lệ phí:Tạm ứng khám sức khỏe";
                }
                else
                {
                    if (q.First().DTuong == "BHYT")
                    {
                        _tenloai = "Tên loại phí, lệ phí: " + tenloaiphi + " - BHYT";
                    }
                    else
                    {
                        _tenloai = "Tên loại phí, lệ phí: " + tenloaiphi + " - Thu phí";
                    }
                    if (q.First().NoiTru.Value == 0)
                        _tenloai += " - Ngoại trú";
                    else
                        _tenloai += " - Nội trú";
                }
                if (q.First().NoiTru == 0)
                {
                    rep.KyHieu.Value = "PTNgT" + System.DateTime.Now.Year;
                }
                else
                {
                    rep.KyHieu.Value = "PTNT" + System.DateTime.Now.Year;
                }
                rep.TenLoai.Value = _tenloai;
                rep.SoTT.Value = "STT:" + q.First().SoTT;
                rep.TenKP.Value = q.First().TenKP; // gan sau
                rep.LyDoThu.Value = q.First().LyDo;
                double sotien = 0;
                if (q.First().SoTien != null)
                {
                    rep.SoTien.Value = q.First().SoTien;
                    sotien = Convert.ToDouble(q.First().SoTien);
                }
                rep.TenCB.Value = q.First().TenCB;
                if (q.First().NgayThu != null)
                {
                    rep.NgayThang.Value = DungChung.Ham.NgaySangChu(q.First().NgayThu.Value);

                }
                rep.DocTien.Value = DungChung.Ham.DocTienBangChu(sotien, " đồng.");
            }
            rep.Lien.Value = lien;
            rep.CreateDocument();
            return rep;
        }
        public static BaoCao.repTamUngVienPhi_A5_bvKhac _InPhieuTamThu_Nhieulien_bvkhac(QLBV_Database.QLBVEntities data, int _Sophieu, string lien)
        {
            BaoCao.repTamUngVienPhi_A5_bvKhac rep = new BaoCao.repTamUngVienPhi_A5_bvKhac();
            int _idtthu = _Sophieu;
            string _tronggio = "";
            var q = (from bn in data.BenhNhans
                     join tt in data.TamUngs on bn.MaBNhan equals tt.MaBNhan
                     join cb in data.CanBoes on tt.MaCB equals cb.MaCB
                     join kp in data.KPhongs on tt.MaKP equals kp.MaKP
                     where (tt.IDTamUng == _idtthu)
                     select new { tt.SoHD, tt.QuyenHD, bn.MaBNhan, bn.SThe, tt.PhanLoai, bn.SoTT, bn.TenBNhan, bn.NgaySinh, bn.ThangSinh, bn.DTuong, bn.NamSinh, bn.NoiTru, kp.TenKP, bn.DChi, tt.SoTien, cb.TenCB, tt.LyDo, tt.NgayThu, tt.IDTamUng, tt.NgoaiGio }).ToList();
            if (q.Count > 0)
            {

                string tenloaiphi = "";
                rep.MaBNhan.Value = q.First().MaBNhan;
                rep.TieuDe.Value = "PHIẾU TẠM THU TIỀN";

                tenloaiphi = "Tạm ứng khám bệnh";

                if (q.First().DTuong == "BHYT" && Bien.MaBV == "30009")
                    rep.SoThe.Value = "Số thẻ BHYT:" + q.First().SThe;
                rep.TenBNhan.Value = q.First().TenBNhan.ToUpper();
                if (Bien.MaBV == "01071" || Bien.MaBV == "01049" || Bien.MaBV == "12345" || Bien.MaBV == "24297" || Bien.MaBV == "56789")
                {
                    rep.txtNguoiNop1_3.Text = q.First().TenBNhan;
                    //rep.txtNguoiNop2_3.Text = q.First().TenBNhan;
                }
                rep.NamSinh.Value = Bien.MaBV == "30005" ? " (SN: " + q.First().NgaySinh + "/" + q.First().ThangSinh + "/" + q.First().NamSinh + ")" : q.First().NamSinh;
                rep.DChi.Value = q.First().DChi;
                rep.SoPhieu.Value = q.First().IDTamUng;
                rep.Quyen.Value = q.First().QuyenHD;
                rep.So.Value = q.First().SoHD;
                string _tendn = Bien.TenDN;
                string _nameDN = (from ad in data.ADMINs.Where(p => p.TenDN == _tendn)
                                  join cb in data.CanBoes on ad.MaCB equals cb.MaCB
                                  select new { cb.TenCB }).Select(p => p.TenCB).First().ToString();
                if (Bien.MaBV == "30372") rep.TenDN.Value = _nameDN;
                if (Bien.MaBV == "30005" || Bien.MaBV == "30010")
                {
                    rep.TenDN.Value = q.First().TenCB == null ? "" : q.First().TenCB;
                }
                if (q.First().NgoaiGio != null)
                    if (q.First().NgoaiGio.Value == 1)
                    {
                        _tronggio = "Ngoài giờ";
                    }
                    else
                    {
                        _tronggio = "Trong giờ";
                    }
                string _tenloai = "";
                if (q.First().DTuong == "KSK")
                {
                    _tenloai = "Tên loại phí, lệ phí:Tạm ứng khám sức khỏe";
                }
                else
                {
                    if (q.First().DTuong == "BHYT")
                    {
                        _tenloai = "Tên loại phí, lệ phí: " + tenloaiphi + " - BHYT";
                    }
                    else
                    {
                        _tenloai = "Tên loại phí, lệ phí: " + tenloaiphi + " - Thu phí";
                    }
                    if (q.First().NoiTru.Value == 0)
                        _tenloai += " - Ngoại trú";
                    else
                        _tenloai += " - Nội trú";
                }
                if (q.First().NoiTru == 0)
                {
                    rep.KyHieu.Value = "PTNgT" + System.DateTime.Now.Year;
                }
                else
                {
                    rep.KyHieu.Value = "PTNT" + System.DateTime.Now.Year;
                }
                rep.TenLoai.Value = _tenloai;
                rep.SoTT.Value = "STT:" + q.First().SoTT;
                rep.TenKP.Value = q.First().TenKP; // gan sau
                rep.LyDoThu.Value = q.First().LyDo;
                rep.TenDN.Value = q.First().TenCB;
                double sotien = 0;
                if (q.First().SoTien != null)
                {
                    rep.SoTien.Value = q.First().SoTien;
                    sotien = Convert.ToDouble(q.First().SoTien);
                }
                if (Bien.MaBV == "30005")
                {
                    var tencb = data.CanBoes.Where(p => p.MaCB == Bien.MaCB).Select(p => p.TenCB).FirstOrDefault();
                    rep.nguoithu1.Text = tencb != null ? tencb : "";
                }
                rep.TenCB.Value = q.First().TenCB;
                if (q.First().NgayThu != null)
                {
                    rep.NgayThang.Value = DungChung.Ham.NgaySangChu(q.First().NgayThu.Value);

                }
                rep.DocTien.Value = DungChung.Ham.DocTienBangChu(sotien, " đồng.");
            }
            rep.Lien.Value = lien;
            rep.CreateDocument();
            return rep;
        }
        public static bool _InPhieuTamThu(QLBV_Database.QLBVEntities data, int _Sophieu, int _mauinPT)
        {
            try
            {
                if (Bien.MaBV == "27183" || Bien.MaBV == "27021" || Bien.MaBV == "27023" || Bien.MaBV == "20001")
                {
                    #region BV 27021, 27023
                    frmIn frm = new frmIn();
                    BaoCao.repThuChi_2lien_A5_27023 rep = new BaoCao.repThuChi_2lien_A5_27023();

                    int _idtthu = _Sophieu;
                    string _tronggio = "";
                    var q = (from bn in data.BenhNhans
                             join tt in data.TamUngs on bn.MaBNhan equals tt.MaBNhan
                             join cb in data.CanBoes on tt.MaCB equals cb.MaCB
                             join kp in data.KPhongs on tt.MaKP equals kp.MaKP
                             where (tt.IDTamUng == _idtthu)
                             select new { bn.SThe, tt.PhanLoai, bn.SoTT, bn.TenBNhan, bn.DTuong, bn.NamSinh, bn.NoiTru, kp.TenKP, bn.DChi, tt.SoTien, cb.TenCB, tt.LyDo, tt.NgayThu, tt.IDTamUng, tt.NgoaiGio }).ToList();

                    if (q.Count > 0)
                    {

                        string tenloaiphi = "";
                        tenloaiphi = "Tạm ứng khám bệnh";
                        rep.LyDoThu.Value = q.First().LyDo;
                        if (q.First().PhanLoai == 0)
                        {
                            rep.cel_hoten.Text = "Họ, tên người nộp tiền:";
                            rep.lblLyDoThu.Value = "Lý do thu: ";
                            rep.TieuDe.Value = "PHIẾU THU";
                            rep.lblKy1.Value = "Người nộp";
                        }
                        else if (q.First().PhanLoai == 4)
                        {
                            rep.cel_hoten.Text = "Họ, tên người nhận tiền";
                            rep.lblLyDoThu.Value = "Lý do chi: ";
                            rep.TieuDe.Value = "PHIẾU CHI";
                            rep.lblKy1.Value = "Người nhận tiền";
                        }
                        rep.lblKy2.Value = "Thủ quỹ";

                        rep.MST.Value = "Mã đơn vị SDNS:.............";
                        if (Bien.MaBV == "27023")
                        {
                            rep.lblNguoiLap11.Text = "Người nộp tiền";
                            rep.lblNguoiLap21.Text = "Người nộp tiền";
                        }
                        DateTime ngayduyet = q.First().NgayThu.Value;
                        rep.NgayDuyet.Value = "Ngày " + ngayduyet.Day.ToString("D2") + " tháng " + ngayduyet.Month.ToString("D2") + " năm " + ngayduyet.Year;
                        rep.TenBNhan.Value = q.First().TenBNhan.ToUpper();
                        if (Bien.MaBV == "20001")
                        {
                            rep.nguoiky.Value = q.First().TenBNhan;
                        }
                        rep.NamSinh.Value = q.First().NamSinh;
                        rep.DChi.Value = q.First().DChi;
                        rep.SoPhieu.Value = q.First().IDTamUng;
                        if (q.First().NgoaiGio != null)
                            if (q.First().NgoaiGio.Value == 1)
                            {
                                _tronggio = "Ngoài giờ";
                            }
                            else
                            {
                                _tronggio = "Trong giờ";
                            }
                        string _tenloai = "";
                        if (q.First().DTuong == "KSK")
                        {
                            _tenloai = "Tên loại phí, lệ phí:Tạm ứng khám sức khỏe";
                        }
                        else
                        {
                            if (q.First().DTuong == "BHYT")
                            {
                                _tenloai = "Tên loại phí, lệ phí: " + tenloaiphi + " - BHYT";
                            }
                            else
                            {
                                _tenloai = "Tên loại phí, lệ phí: " + tenloaiphi + " - Thu phí";
                            }
                            if (q.First().NoiTru.Value == 0)
                                _tenloai += " - Ngoại trú";
                            else
                                _tenloai += " - Nội trú";
                        }
                        if (q.First().NoiTru == 0)
                        {
                            rep.KyHieu.Value = "PTNgT" + System.DateTime.Now.Year;
                        }
                        else
                        {
                            rep.KyHieu.Value = "PTNT" + System.DateTime.Now.Year;
                        }
                        rep.TenLoai.Value = _tenloai;
                        rep.SoTT.Value = "STT:" + q.First().SoTT;
                        rep.TenKP.Value = "Bộ phận: " + q.First().TenKP; // gan sau                       
                        rep.LyDoThu.Value = q.First().LyDo;
                        double sotien = 0;
                        if (q.First().SoTien != null)
                        {
                            rep.SoTien.Value = q.First().SoTien;
                            sotien = Convert.ToDouble(q.First().SoTien);
                        }
                        rep.TenCB.Value = q.First().TenCB;
                        if (q.First().NgayThu != null)
                        {
                            rep.NgayThang.Value = DungChung.Ham.NgaySangChu(q.First().NgayThu.Value);

                        }
                        rep.DocTien.Value = DungChung.Ham.DocTienBangChu(sotien, " đồng.");
                    }
                    rep.lblDaNhanTien1.Visible = true;
                    rep.lblDaNhanTien2.Visible = true;
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                    return true;
                    #endregion
                }
                else if (Bien.MaBV == "01830" || Bien.MaBV == "30003")
                {
                    #region BV 01830
                    frmIn frm = new frmIn();

                    BaoCao.repTamUngVienPhi_A5_01830 rep = usTamThu_TToan._InPhieuTamThu_Nhieulien_(data, _Sophieu, "Liên 1: lưu bệnh nhân");
                    if (_mauinPT == 0)
                    {
                        BaoCao.repTamUngVienPhi rep3 = new BaoCao.repTamUngVienPhi();
                        int _idtthu = _Sophieu;
                        string _tronggio = "";
                        var q = (from bn in data.BenhNhans
                                 join tt in data.TamUngs on bn.MaBNhan equals tt.MaBNhan
                                 join cb in data.CanBoes on tt.MaCB equals cb.MaCB
                                 join kp in data.KPhongs on tt.MaKP equals kp.MaKP
                                 where (tt.IDTamUng == _idtthu)
                                 select new { tt.QuyenHD, tt.SoHD, bn.SThe, tt.PhanLoai, bn.SoTT, bn.TenBNhan, bn.DTuong, bn.NamSinh, bn.NoiTru, kp.TenKP, bn.DChi, tt.SoTien, cb.TenCB, tt.LyDo, tt.NgayThu, tt.IDTamUng, tt.NgoaiGio, bn.MaBNhan, GTinh = bn.GTinh == 1 ? "Nam" : "Nữ", bn.Tuoi, tt.NgayTaoHD }).ToList();

                        if (q.Count > 0)
                        {
                            string[] arr = new string[4];
                            var q1 = (from a in data.TamUngs.Where(p => p.MaBNhan == q.First().MaBNhan && p.PhanLoai == 0)
                                      select new
                                      {
                                          a.NgayTaoHD
                                      }).OrderBy(p => p.NgayTaoHD).ToArray();
                            foreach (var item in q1)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    arr[i] = item.NgayTaoHD;
                                }
                            }
                            string tenloaiphi = "";
                            for (int j = 0; j < 4; j++)
                            {
                                if (arr[j] == q.First().NgayTaoHD)
                                {
                                    rep3.SoLan.Value = j;
                                }
                            }
                            rep3.TieuDe.Value = "PHIẾU TẠM THU TIỀN";
                            tenloaiphi = "Tạm ứng khám bệnh";

                            if (q.First().DTuong == "BHYT" && Bien.MaBV == "30009")
                                rep.SoThe.Value = "Số thẻ BHYT:" + q.First().SThe;
                            rep3.TenBNhan.Value = q.First().TenBNhan.ToUpper();
                            if (Bien.MaBV == "01071" || Bien.MaBV == "01049" || Bien.MaBV == "12345" || Bien.MaBV == "24297" || Bien.MaBV == "56789")
                            {
                                rep3.txtNguoiNop1_3.Text = q.First().TenBNhan;
                                rep3.txtNguoiNop2_3.Text = q.First().TenBNhan;
                            }

                            rep3.NamSinh.Value = Bien.MaBV == "14017" ? q.First().Tuoi.ToString() : q.First().NamSinh;
                            rep3.DChi.Value = q.First().DChi;

                            if (Bien.MaBV == "01830")
                                rep3.SoPhieu.Value = q.First().SoHD;
                            else
                                rep3.SoPhieu.Value = q.First().IDTamUng;
                            if (q.First().NgoaiGio != null)
                                if (q.First().NgoaiGio.Value == 1)
                                {
                                    _tronggio = "Ngoài giờ";
                                }
                                else
                                {
                                    _tronggio = "Trong giờ";
                                }
                            rep3.TrongGio.Value = Bien.MaBV == "14017" ? "Mã bệnh án:.............." : "";
                            string _tenloai = "";
                            if (q.First().DTuong == "KSK")
                            {
                                _tenloai = Bien.MaBV == "14017" ? q.First().GTinh : "Tên loại phí, lệ phí:Tạm ứng khám sức khỏe";
                            }
                            else
                            {
                                if (q.First().DTuong == "BHYT")
                                {
                                    _tenloai = Bien.MaBV == "14017" ? "Giới tính: " + q.First().GTinh : "Tên loại phí, lệ phí: " + tenloaiphi + " - BHYT";
                                }
                                else
                                {
                                    _tenloai = Bien.MaBV == "14017" ? "Giới tính: " + q.First().GTinh : "Tên loại phí, lệ phí: " + tenloaiphi + " - Thu phí";
                                }
                                if (q.First().NoiTru.Value == 0 && Bien.MaBV != "14017")
                                    _tenloai += " - Ngoại trú";
                                else
                                    _tenloai += " - Nội trú";
                            }
                            if (q.First().NoiTru == 0)
                            {
                                rep3.KyHieu.Value = "PTNgT" + System.DateTime.Now.Year;
                            }
                            else
                            {
                                rep3.KyHieu.Value = "PTNT" + System.DateTime.Now.Year;
                            }
                            rep3.TenLoai.Value = _tenloai;
                            rep3.SoTT.Value = Bien.MaBV == "14017" ? "Mã bệnh nhân: " + q.First().MaBNhan : "STT:" + q.First().SoTT;
                            rep3.TenKP.Value = q.First().TenKP; // gan sau
                            rep3.LyDoThu.Value = q.First().LyDo;
                            double sotien = 0;
                            if (q.First().SoTien != null)
                            {
                                rep3.SoTien.Value = q.First().SoTien;
                                sotien = Convert.ToDouble(q.First().SoTien);
                            }
                            rep3.TenCB.Value = q.First().TenCB;
                            if (q.First().NgayThu != null)
                            {
                                rep3.NgayThang.Value = DungChung.Ham.NgaySangChu(q.First().NgayThu.Value);

                            }
                            rep3.DocTien.Value = DungChung.Ham.DocTienBangChu(sotien, " đồng.");

                        }
                        rep3.CreateDocument();
                        frm.prcIN.PrintingSystem = rep3.PrintingSystem;
                        frm.ShowDialog();
                    }
                    if (_mauinPT >= 1)
                    {
                        BaoCao.repTamUngVienPhi_A5_01830 rep2 = usTamThu_TToan._InPhieuTamThu_Nhieulien_(data, _Sophieu, "Liên 2: lưu kế toán");
                        rep.Pages.AddRange(rep2.Pages);
                    }
                    if (_mauinPT >= 2)
                    {
                        BaoCao.repTamUngVienPhi_A5_01830 rep2 = usTamThu_TToan._InPhieuTamThu_Nhieulien_(data, _Sophieu, "Liên 3: lưu thủ quỹ");
                        rep.Pages.AddRange(rep2.Pages);
                    }
                    if (_mauinPT > 0)
                    {
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }

                    return true;
                    #endregion
                }
                else
                {
                    #region BV khác
                    frmIn frm = new frmIn();
                    if (Bien.MaBV == "30010")
                        _mauinPT = 2;

                    string strLien = "";
                    strLien = Bien.MaBV == "30010" ? "Liên 1: Lưu" : (Bien.MaBV == "30002" ? "Liên 1: lưu kế toán" : (Bien.MaBV == "24012" ? "" : "Liên 1: lưu bệnh nhân"));
                    BaoCao.repTamUngVienPhi_A5_bvKhac rep = usTamThu_TToan._InPhieuTamThu_Nhieulien_bvkhac(data, _Sophieu, strLien);
                    if (_mauinPT == 0)
                    {
                        BaoCao.repTamUngVienPhi rep3 = new BaoCao.repTamUngVienPhi();
                        int _idtthu = _Sophieu;
                        string _tronggio = "";
                        List<TamUng> arrLanTao = new List<TamUng>();
                        var q = (from bn in data.BenhNhans
                                 join tt in data.TamUngs on bn.MaBNhan equals tt.MaBNhan
                                 join cb in data.CanBoes on tt.MaCB equals cb.MaCB
                                 join kp in data.KPhongs on tt.MaKP equals kp.MaKP
                                 where (tt.IDTamUng == _idtthu)
                                 select new { tt.QuyenHD, tt.SoHD, bn.SThe, tt.PhanLoai, bn.SoTT, bn.TenBNhan, bn.DTuong, bn.NamSinh, bn.ThangSinh, bn.NgaySinh, bn.NoiTru, kp.TenKP, bn.DChi, tt.SoTien, cb.TenCB, tt.LyDo, tt.NgayThu, tt.IDTamUng, tt.NgoaiGio, bn.MaBNhan, tt.NgayTaoHD, GTinh = bn.GTinh == 1 ? "Nam" : "Nữ", bn.Tuoi }).ToList();
                        if (q.Count > 0)
                        {

                            string tenloaiphi = "";
                            // lần thu tạm ứng
                            int? mabn = q.First().MaBNhan;
                            arrLanTao = data.TamUngs.Where(p => p.MaBNhan == mabn).OrderBy(p => p.IDTamUng).ToList();

                            for (int i = 0; i < arrLanTao.Count(); i++)
                            {
                                if (q.First().NgayThu == arrLanTao[i].NgayThu)
                                {
                                    rep3.txtLanThu.Text = rep3.xrLabel38.Text = rep3.xrLabel40.Text = Convert.ToString(i + 1);
                                }
                            }//
                            //var soBenhAn = data.VaoViens.Where(p=>p.MaBNhan == mabn).ToList();

                            rep3.TieuDe.Value = "PHIẾU TẠM THU TIỀN";
                            tenloaiphi = "Tạm ứng khám bệnh";

                            if (q.First().DTuong == "BHYT" && Bien.MaBV == "30009")
                                rep3.SoThe.Value = "Số thẻ BHYT:" + q.First().SThe;
                            rep3.TenBNhan.Value = q.First().TenBNhan.ToUpper();
                            if (Bien.MaBV == "01071" || Bien.MaBV == "01049" || Bien.MaBV == "12345" || Bien.MaBV == "24297" || Bien.MaBV == "56789")
                            {
                                rep3.txtNguoiNop1_3.Text = q.First().TenBNhan;
                                rep3.txtNguoiNop2_3.Text = q.First().TenBNhan;
                            }
                            if (Bien.MaBV == "14017")
                            {
                                rep3.txtCQCQ.Text = Bien.TenCQCQ;
                                rep3.txtCQ.Text = Bien.TenCQ;
                                //rep3.txtSoBA.Text = soBenhAn.First().SoBA != "" ? soBenhAn.First().SoBA : ".........";
                                rep3.txtSThe.Text = q.First().SThe;
                                rep3.txtTienBangChu.Text = DungChung.Ham.DocTienBangChu(Convert.ToDouble(q.First().SoTien), " đồng.");
                                rep3.txtNgayThang.Text = DungChung.Ham.NgaySangChu(Convert.ToDateTime(q.First().NgayThu), 2);
                                rep3.txtNgThuTien.Text = q.First().TenCB;



                            }
                            else
                            {
                                rep3.NamSinh.Value = Bien.MaBV == "30005" ? "       (SN: " + q.First().NgaySinh + "/" + q.First().ThangSinh + "/" + q.First().NamSinh + ")" : q.First().NamSinh;
                            }
                            rep3.MaBN.Value = q.First().MaBNhan;
                            rep3.GTinh.Value = q.First().GTinh;
                            rep3.Tuoi.Value = q.First().Tuoi;
                            rep3.DChi.Value = q.First().DChi;
                            rep3.SoPhieu.Value = q.First().IDTamUng;
                            if (q.First().NgoaiGio != null)
                                if (q.First().NgoaiGio.Value == 1)
                                {
                                    _tronggio = "Ngoài giờ";
                                }
                                else
                                {
                                    _tronggio = "Trong giờ";
                                }
                            string _tenloai = "";
                            if (Bien.MaBV == "14017")
                            {
                                _tenloai = "Giới tính: " + q.First().GTinh;
                            }
                            else
                            {

                                if (q.First().DTuong == "KSK")
                                {
                                    _tenloai = "Tên loại phí, lệ phí:Tạm ứng khám sức khỏe";
                                }
                                else
                                {
                                    if (q.First().DTuong == "BHYT")
                                    {
                                        _tenloai = "Tên loại phí, lệ phí: " + tenloaiphi + " - BHYT";
                                    }
                                    else
                                    {
                                        _tenloai = "Tên loại phí, lệ phí: " + tenloaiphi + " - Thu phí";
                                    }
                                    if (q.First().NoiTru.Value == 0)
                                        _tenloai += " - Ngoại trú";
                                    else
                                        _tenloai += " - Nội trú";
                                }
                            }

                            if (q.First().NoiTru == 0)
                            {
                                rep3.KyHieu.Value = "PTNgT" + System.DateTime.Now.Year;
                            }
                            else
                            {
                                rep3.KyHieu.Value = "PTNT" + System.DateTime.Now.Year;
                            }
                            rep3.TenLoai.Value = _tenloai;
                            rep3.SoTT.Value = "STT:" + q.First().SoTT;
                            rep3.TenKP.Value = q.First().TenKP; // gan sau
                            rep3.LyDoThu.Value = q.First().LyDo;

                            double sotien = 0;
                            if (q.First().SoTien != null)
                            {
                                rep3.SoTien.Value = q.First().SoTien;
                                sotien = Convert.ToDouble(q.First().SoTien);
                            }
                            rep3.TenCB.Value = q.First().TenCB;
                            if (q.First().NgayThu != null)
                            {
                                rep3.NgayThang.Value = DungChung.Ham.NgaySangChu(q.First().NgayThu.Value);

                            }
                            rep3.DocTien.Value = DungChung.Ham.DocTienBangChu(sotien, " đồng.");

                        }
                        //if (Bien.MaBV == "30005")
                        //{
                        //    var tencb = data.CanBoes.Where(p => p.MaCB == Bien.MaCB).Select(p => p.TenCB).FirstOrDefault();
                        //    rep3.nguoithu1.Text = tencb != null ? tencb : "";
                        //}
                        rep3.CreateDocument();
                        frm.prcIN.PrintingSystem = rep3.PrintingSystem;
                        frm.ShowDialog();
                    }
                    if (_mauinPT == 1)
                    {
                        BaoCao.repTamUngVienPhi_A5_bvKhac rep2 = usTamThu_TToan._InPhieuTamThu_Nhieulien_bvkhac(data, _Sophieu, Bien.MaBV == "30010" ? "Liên 2: Giao bệnh nhân" : (Bien.MaBV == "30002" ? "Liên 2: lưu bệnh nhân" : "Liên 2: lưu kế toán"));
                        rep.Pages.AddRange(rep2.Pages);
                    }
                    if (_mauinPT == 2)
                    {
                        if (DungChung.Bien.MaBV == "30010")
                        {
                            BaoCao.repTamUngVienPhi_A5_bvKhac rep1 = usTamThu_TToan._InPhieuTamThu_Nhieulien_bvkhac(data, _Sophieu, Bien.MaBV == "30010" ? "Liên 2: Giao bệnh nhân" : (Bien.MaBV == "30002" ? "Liên 2: lưu bệnh nhân" : "Liên 2: lưu kế toán"));
                            rep.Pages.AddRange(rep1.Pages);
                            BaoCao.repTamUngVienPhi_A5_bvKhac rep2 = usTamThu_TToan._InPhieuTamThu_Nhieulien_bvkhac(data, _Sophieu, Bien.MaBV == "30010" ? "Liên 3: Giao khoa phòng" : "Liên 3: lưu khoa phòng");
                            rep.Pages.AddRange(rep2.Pages);
                        }
                        else
                        {
                            BaoCao.repTamUngVienPhi_A5_bvKhac rep2 = usTamThu_TToan._InPhieuTamThu_Nhieulien_bvkhac(data, _Sophieu, Bien.MaBV == "30010" ? "Liên 3: Giao khoa phòng" : "Liên 3: lưu khoa phòng");
                            rep.Pages.AddRange(rep2.Pages);
                        }
                    }
                    if (_mauinPT > 0)
                    {
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }

                    return true;
                    #endregion
                }
            }
            catch (Exception ex)
            {
                return false;

            }
        }
        #endregion
        #region Phiếu tạm thu
        public static bool _InPhieuTamThu_04006_30003(QLBV_Database.QLBVEntities data, int _Sophieu)
        {
            try
            {
                QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
                BaoCao.repTamUng rep = new BaoCao.repTamUng();
                int _idtthu = _Sophieu;
                string _tronggio = "";
                var q = (from bn in data.BenhNhans
                         join tt in data.TamUngs on bn.MaBNhan equals tt.MaBNhan
                         join cb in data.CanBoes on tt.MaCB equals cb.MaCB
                         join kp in data.KPhongs on tt.MaKP equals kp.MaKP
                         where (tt.IDTamUng == _idtthu)
                         select new { bn.MaBNhan, bn.SoTT, bn.TenBNhan, bn.NamSinh, kp.TenKP, bn.DChi, tt.SoTien, cb.TenCB, tt.LyDo, tt.NgayThu, tt.IDTamUng, tt.NgoaiGio }).ToList();
                if (q.Count > 0)
                {
                    int _MaBN = q.First().MaBNhan == null ? 0 : Convert.ToInt32(q.First().MaBNhan);
                    rep.TenBNhan.Value = q.First().TenBNhan.ToUpper();
                    rep.NamSinh.Value = q.First().NamSinh;
                    rep.DChi.Value = q.First().DChi;
                    rep.SoPhieu.Value = q.First().IDTamUng;
                    if (q.First().NgoaiGio != null)
                        if (q.First().NgoaiGio.Value == 1)
                        {
                            _tronggio = "Ngoài giờ";
                        }
                        else
                        {
                            _tronggio = "Trong giờ";
                        }
                    string _tenloai = "";
                    var _dtuong = _dataContext.BenhNhans.Where(p => p.MaBNhan == _MaBN).ToList();
                    if (_dtuong.Count > 0)
                    {
                        if (_dtuong.First().DTuong == "KSK")
                        {
                            _tenloai = "Tên loại phí, lệ phí:Tạm ứng khám sức khỏe";
                        }
                        else
                        {
                            if (_dtuong.First().DTuong == "BHYT")
                            {
                                _tenloai = "Tên loại phí, lệ phí: Tạm ứng khám bệnh - BHYT";
                            }
                            else
                            {
                                _tenloai = "Tên loại phí, lệ phí: Tạm ứng khám bệnh - Thu phí";
                            }
                            if (_dtuong.First().NoiTru.Value == 0)
                                _tenloai += " - Ngoại trú";
                            else
                                _tenloai += " - Nội trú";
                        }
                    }
                    //rep.TenLoai.Value = _tenloai;
                    //rep.SoTT.Value = "STT:" + q.First().SoTT;
                    rep.TenKP.Value = q.First().TenKP; // gan sau
                    rep.LyDoThu.Value = q.First().LyDo;
                    double sotien = 0;
                    if (q.First().SoTien != null)
                    {
                        rep.SoTien.Value = q.First().SoTien;
                        sotien = Convert.ToDouble(q.First().SoTien);
                    }
                    rep.TenCB.Value = q.First().TenCB;
                    if (q.First().NgayThu != null)
                    {
                        rep.NgayThang.Value = DungChung.Ham.NgaySangChu(q.First().NgayThu.Value);

                    }
                    rep.DocTien.Value = DungChung.Ham.DocTienBangChu(sotien, " đồng.");

                }
                rep.CreateDocument();
                frmIn frm = new frmIn();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        #region Phiếu Thu
        public static bool _InPhieuThu(QLBV_Database.QLBVEntities data, int _MaBN)
        {

            try
            {
                QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
                #region BV 27183
                if (Bien.MaBV == "27183")
                {
                    BaoCao.rep_phieuthuchi_A5_27183 rep = new BaoCao.rep_phieuthuchi_A5_27183();
                    double sotientu = 0;
                    double sotienbn = 0;
                    double sotientc = 0;
                    string _tronggio = "";
                    var q = (from bn in data.BenhNhans
                             join tt in data.TamUngs.Where(p => p.MaBNhan == _MaBN) on bn.MaBNhan equals tt.MaBNhan
                             join cb in data.CanBoes on tt.MaCB equals cb.MaCB into kq
                             from kq2 in kq.DefaultIfEmpty()
                             select new { bn.MaBNhan, tt.TienChenh, bn.NoiTru, bn.DTuong, bn.SoTT, tt.PhanLoai, tt.NgoaiGio, bn.TenBNhan, bn.NamSinh, bn.DChi, tt.MaKP, tt.SoTien, TenCB = kq2 == null ? "" : kq2.TenCB, tt.LyDo, tt.NgayThu, tt.IDTamUng }).ToList();
                    if (q.Count > 0)
                    {
                        if (q.Where(p => p.PhanLoai == 1).ToList().Count > 0 && q.Where(p => p.PhanLoai == 1).First().NgoaiGio != null)
                            if (q.Where(p => p.PhanLoai == 1).First().NgoaiGio.Value == 1)
                            {
                                _tronggio = "Ngoài giờ";
                            }
                            else
                            {
                                _tronggio = "Trong giờ";
                            }
                        rep.TrongGio.Value = "Thu: " + _tronggio;
                        int _makp = 0;
                        if (q.Where(p => p.PhanLoai == 1).ToList().Count > 0 && q.Where(p => p.PhanLoai == 1).First().MaKP != null)
                            _makp = q.Where(p => p.PhanLoai == 1).First().MaKP.Value;
                        var kp = _dataContext.KPhongs.Where(p => p.MaKP == _makp).ToList();
                        if (kp.Count > 0)
                            rep.TenKP.Value = "Bộ phận: " + kp.First().TenKP;
                        rep.TenBNhan.Value = q.First().TenBNhan.ToUpper();
                        rep.NamSinh.Value = q.First().NamSinh;
                        rep.DChi.Value = q.First().DChi;
                        //rep.TenKP.Value = q.First().TenKP; // gan sau
                        if (q.Where(p => p.PhanLoai == 1).ToList().Count > 0)
                        {
                            rep.LyDoThu.Value = q.Where(p => p.PhanLoai == 1).First().LyDo;
                            DateTime ngayduyet = q.Where(p => p.PhanLoai == 1).First().NgayThu.Value;
                            rep.NgayDuyet.Value = "Ngày " + ngayduyet.Day.ToString("D2") + " tháng " + ngayduyet.Month.ToString("D2") + " năm " + ngayduyet.Year;
                        }
                        rep.lblLyDoThu.Value = "Lý do thu: ";
                        rep.TieuDe.Value = "PHIẾU THU";
                        rep.TenCB.Value = q.First().TenCB;
                        rep.lblKy1.Value = "Người nộp";
                        rep.lblKy2.Value = "Thủ quỹ";

                        rep.MST.Value = "Mã đơn vị SDNS:.............";

                        if (q.First().NgayThu != null)
                        {
                            rep.NgayThang.Value = DungChung.Ham.NgaySangChu(q.First().NgayThu.Value);
                        }

                        if (q.Where(p => p.PhanLoai == 1).ToList().Count > 0 && q.Where(p => p.PhanLoai == 1).Sum(p => p.SoTien).Value > 0)
                        {
                            sotienbn = q.Where(p => p.PhanLoai == 1).Sum(p => p.SoTien).Value;

                            rep.SoPhieu.Value = q.Where(p => p.PhanLoai == 1).First().IDTamUng;
                        }
                        sotientc = q.Where(p => p.PhanLoai == 1).Sum(p => p.TienChenh);
                        sotientu = q.Where(p => p.PhanLoai == 0).Sum(p => p.SoTien).Value - q.Where(p => p.PhanLoai == 4).Sum(p => p.SoTien).Value; ;



                        rep.SoTienNop.Value = sotienbn;
                        rep.SoTien.Value = sotientc;
                        rep.SoTienTraNop.Value = sotientc;
                        //rep.mauso.Value = "01BLP2-001";
                        rep.NgayThang.Value = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                        string _tenloai = "";
                        if (q.First().DTuong == "KSK")
                        {
                            _tenloai = "Tên loại phí, lệ phí: Khám sức khỏe";
                            rep.SoTT.Value = "STT:" + q.First().SoTT;
                        }
                        else
                        {
                            if (q.First().DTuong == "BHYT")
                            {
                                _tenloai = "Tên loại phí, lệ phí: Chi phí KCB - BHYT";

                            }
                            else
                            {
                                _tenloai = "Tên loại phí, lệ phí: Chi phí KCB - Thu phí";
                            }
                            if (q.First().NoiTru.Value == 0)
                                _tenloai += " - Ngoại trú";
                            else
                                _tenloai += " - Nội trú";
                        }
                        if (Bien.MaBV == "27001")
                        {
                            rep.SoTT.Value = "Mã BN:" + q.First().MaBNhan;
                        }
                        rep.TienBNtranop.Value = "Tiền bệnh nhân phải nộp thêm:";
                        rep.NguoiNopThu.Value = "Người thu tiền";
                        rep.TenLoai.Value = _tenloai;
                        rep.DocTien.Value = DungChung.Ham.DocTienBangChu(sotientc, " đồng.");
                        rep.lblDaNhanTien1.Visible = true;
                        //rep.lblDaNhanTien2.Visible = true;
                        rep.CreateDocument();
                        frmIn frm = new frmIn();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Bệnh nhân chưa thanh toán");
                    }
                    sotientu = 0;
                    sotienbn = 0;
                    sotientc = 0;

                    return true;

                }
                #endregion
                else if (Bien.MaBV == "27023" || Bien.MaBV == "27021" || Bien.MaBV == "20001")
                {
                    #region BV 27023, 27021
                    BaoCao.repThuChi_2lien_A5_27023 rep = new BaoCao.repThuChi_2lien_A5_27023();
                    double sotientu = 0;
                    double sotienbn = 0;
                    double sotientc = 0;
                    string _tronggio = "";
                    var q = (from bn in data.BenhNhans
                             join tt in data.TamUngs.Where(p => p.MaBNhan == _MaBN) on bn.MaBNhan equals tt.MaBNhan
                             join cb in data.CanBoes on tt.MaCB equals cb.MaCB into kq
                             from kq2 in kq.DefaultIfEmpty()
                             select new { bn.MaBNhan, tt.TienChenh, bn.NoiTru, bn.DTuong, bn.SoTT, tt.PhanLoai, tt.NgoaiGio, bn.TenBNhan, bn.NamSinh, bn.DChi, tt.MaKP, tt.SoTien, TenCB = kq2 == null ? "" : kq2.TenCB, tt.LyDo, tt.NgayThu, tt.IDTamUng }).ToList();
                    if (q.Count > 0)
                    {
                        if (q.Where(p => p.PhanLoai == 1).ToList().Count > 0 && q.Where(p => p.PhanLoai == 1).First().NgoaiGio != null)
                            if (q.Where(p => p.PhanLoai == 1).First().NgoaiGio.Value == 1)
                            {
                                _tronggio = "Ngoài giờ";
                            }
                            else
                            {
                                _tronggio = "Trong giờ";
                            }
                        rep.TrongGio.Value = "Thu: " + _tronggio;
                        int _makp = 0;
                        if (q.Where(p => p.PhanLoai == 1).ToList().Count > 0 && q.Where(p => p.PhanLoai == 1).First().MaKP != null)
                            _makp = q.Where(p => p.PhanLoai == 1).First().MaKP.Value;
                        var kp = _dataContext.KPhongs.Where(p => p.MaKP == _makp).ToList();
                        if (kp.Count > 0)
                            rep.TenKP.Value = "Bộ phận: " + kp.First().TenKP;
                        rep.TenBNhan.Value = q.First().TenBNhan.ToUpper();
                        rep.NamSinh.Value = q.First().NamSinh;
                        rep.DChi.Value = q.First().DChi;
                        //rep.TenKP.Value = q.First().TenKP; // gan sau
                        if (q.Where(p => p.PhanLoai == 1).ToList().Count > 0)
                        {
                            rep.LyDoThu.Value = q.Where(p => p.PhanLoai == 1).First().LyDo;
                            DateTime ngayduyet = q.Where(p => p.PhanLoai == 1).First().NgayThu.Value;
                            rep.NgayDuyet.Value = "Ngày " + ngayduyet.Day.ToString("D2") + " tháng " + ngayduyet.Month.ToString("D2") + " năm " + ngayduyet.Year;
                        }
                        rep.lblLyDoThu.Value = "Lý do thu: ";
                        rep.TieuDe.Value = "PHIẾU THU";
                        rep.TenCB.Value = q.First().TenCB;
                        rep.lblKy1.Value = "Người nộp";
                        rep.lblKy2.Value = "Thủ quỹ";
                        if (Bien.MaBV == "27023")
                        {
                            rep.lblNguoiLap11.Text = "Người nộp tiền";
                            rep.lblNguoiLap21.Text = "Người nộp tiền";
                        }
                        rep.MST.Value = "Mã đơn vị SDNS:.............";

                        if (q.First().NgayThu != null)
                        {
                            rep.NgayThang.Value = DungChung.Ham.NgaySangChu(q.First().NgayThu.Value);
                        }

                        if (q.Where(p => p.PhanLoai == 1).ToList().Count > 0 && q.Where(p => p.PhanLoai == 1).Sum(p => p.SoTien).Value > 0)
                        {
                            sotienbn = q.Where(p => p.PhanLoai == 1).Sum(p => p.SoTien).Value;

                            rep.SoPhieu.Value = q.Where(p => p.PhanLoai == 1).First().IDTamUng;
                        }
                        sotientc = q.Where(p => p.PhanLoai == 1).Sum(p => p.TienChenh);
                        sotientu = q.Where(p => p.PhanLoai == 0).Sum(p => p.SoTien).Value - q.Where(p => p.PhanLoai == 4).Sum(p => p.SoTien).Value; ;



                        rep.SoTienNop.Value = sotienbn;
                        rep.SoTien.Value = sotientc;
                        rep.SoTienTraNop.Value = sotientc;
                        rep.mauso.Value = "01BLP2-001";
                        rep.NgayThang.Value = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                        string _tenloai = "";
                        if (q.First().DTuong == "KSK")
                        {
                            _tenloai = "Tên loại phí, lệ phí: Khám sức khỏe";
                            rep.SoTT.Value = "STT:" + q.First().SoTT;
                        }
                        else
                        {
                            if (q.First().DTuong == "BHYT")
                            {
                                _tenloai = "Tên loại phí, lệ phí: Chi phí KCB - BHYT";

                            }
                            else
                            {
                                _tenloai = "Tên loại phí, lệ phí: Chi phí KCB - Thu phí";
                            }
                            if (q.First().NoiTru.Value == 0)
                                _tenloai += " - Ngoại trú";
                            else
                                _tenloai += " - Nội trú";
                        }
                        if (Bien.MaBV == "27001")
                        {
                            rep.SoTT.Value = "Mã BN:" + q.First().MaBNhan;
                        }
                        rep.TienBNtranop.Value = "Tiền bệnh nhân phải nộp thêm:";
                        rep.NguoiNopThu.Value = "Người thu tiền";
                        rep.TenLoai.Value = _tenloai;
                        rep.DocTien.Value = DungChung.Ham.DocTienBangChu(sotientc, " đồng.");
                        rep.lblDaNhanTien1.Visible = true;
                        rep.lblDaNhanTien2.Visible = true;
                        rep.CreateDocument();
                        frmIn frm = new frmIn();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Bệnh nhân chưa thanh toán");
                    }
                    sotientu = 0;
                    sotienbn = 0;
                    sotientc = 0;

                    return true;
                    #endregion
                }
                else
                {
                    #region BV khác
                    BaoCao.repThuChi_2lien_A5 rep = new BaoCao.repThuChi_2lien_A5();
                    double sotientu = 0;
                    double sotienbn = 0;
                    double sotientc = 0;
                    string _tronggio = "";
                    var q = (from bn in data.BenhNhans
                             join tt in data.TamUngs.Where(p => p.MaBNhan == _MaBN) on bn.MaBNhan equals tt.MaBNhan
                             join cb in data.CanBoes on tt.MaCB equals cb.MaCB into kq
                             from kq2 in kq.DefaultIfEmpty()
                             select new { bn.MaBNhan, tt.TienChenh, bn.NoiTru, bn.DTuong, bn.SoTT, tt.PhanLoai, tt.NgoaiGio, bn.TenBNhan, bn.NamSinh, bn.DChi, tt.MaKP, tt.SoTien, TenCB = kq2 == null ? "" : kq2.TenCB, tt.LyDo, tt.NgayThu, tt.IDTamUng, QuyenHD = tt.QuyenHD == null ? "" : tt.QuyenHD }).ToList();
                    if (q.Count > 0)
                    {
                        if (q.Where(p => p.PhanLoai == 1).ToList().Count > 0 && q.Where(p => p.PhanLoai == 1).First().NgoaiGio != null)
                            if (q.Where(p => p.PhanLoai == 1).First().NgoaiGio.Value == 1)
                            {
                                _tronggio = "Ngoài giờ";
                            }
                            else
                            {
                                _tronggio = "Trong giờ";
                            }
                        rep.QuyenHD.Value = q.First().QuyenHD.ToString();
                        rep.TrongGio.Value = "Thu: " + _tronggio;
                        int _makp = 0;
                        if (q.Where(p => p.PhanLoai == 1).ToList().Count > 0 && q.Where(p => p.PhanLoai == 1).First().MaKP != null)
                            _makp = q.Where(p => p.PhanLoai == 1).First().MaKP.Value;
                        var kp = _dataContext.KPhongs.Where(p => p.MaKP == _makp).ToList();
                        if (kp.Count > 0)
                            rep.TenKP.Value = kp.First().TenKP;
                        rep.TenBNhan.Value = q.First().TenBNhan.ToUpper();
                        rep.NamSinh.Value = q.First().NamSinh;
                        rep.DChi.Value = q.First().DChi;
                        //rep.TenKP.Value = q.First().TenKP; // gan sau
                        if (q.Where(p => p.PhanLoai == 1).ToList().Count > 0)
                            rep.LyDoThu.Value = q.Where(p => p.PhanLoai == 1).First().LyDo;
                        rep.TenCB.Value = q.First().TenCB;
                        if (q.First().NgayThu != null)
                        {
                            rep.NgayThang.Value = DungChung.Ham.NgaySangChu(q.First().NgayThu.Value);
                        }

                        if (q.Where(p => p.PhanLoai == 1).ToList().Count > 0 && q.Where(p => p.PhanLoai == 1).Sum(p => p.SoTien).Value > 0)
                        {
                            sotienbn = q.Where(p => p.PhanLoai == 1).Sum(p => p.SoTien).Value;

                            rep.SoPhieu.Value = q.Where(p => p.PhanLoai == 1).First().IDTamUng;
                        }
                        sotientc = q.Where(p => p.PhanLoai == 1).Sum(p => p.TienChenh);
                        sotientu = q.Where(p => p.PhanLoai == 0).Sum(p => p.SoTien).Value - q.Where(p => p.PhanLoai == 4).Sum(p => p.SoTien).Value; ;



                        rep.SoTienNop.Value = sotienbn;
                        rep.SoTien.Value = sotientu;
                        rep.SoTienTraNop.Value = sotientc;
                        rep.mauso.Value = "01BLP2-001";
                        if (Bien.MaBV == "30004")
                            rep.TieuDe.Value = "PHIẾU THU TIỀN PHÍ, LỆ PHÍ";
                        else
                            rep.TieuDe.Value = "BIÊN LAI THU TIỀN PHÍ, LỆ PHÍ";
                        string _tenloai = "";
                        if (q.First().DTuong == "KSK")
                        {
                            _tenloai = "Tên loại phí, lệ phí: Khám sức khỏe";
                            rep.SoTT.Value = "STT:" + q.First().SoTT;
                        }
                        else
                        {
                            if (q.First().DTuong == "BHYT")
                            {
                                _tenloai = "Tên loại phí, lệ phí: Chi phí KCB - BHYT";

                            }
                            else
                            {
                                _tenloai = "Tên loại phí, lệ phí: Chi phí KCB - Thu phí";
                            }
                            if (q.First().NoiTru.Value == 0)
                                _tenloai += " - Ngoại trú";
                            else
                                _tenloai += " - Nội trú";
                        }
                        if (Bien.MaBV == "27001")
                        {
                            rep.SoTT.Value = "Mã BN:" + q.First().MaBNhan;
                        }
                        rep.TienBNtranop.Value = "Tiền bệnh nhân phải nộp thêm:";
                        rep.NguoiNopThu.Value = "Người thu tiền";
                        rep.TenLoai.Value = _tenloai;
                        rep.DocTien.Value = DungChung.Ham.DocTienBangChu(sotientc, " đồng.");
                        rep.CreateDocument();
                        frmIn frm = new frmIn();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Bệnh nhân chưa thanh toán");
                    }
                    sotientu = 0;
                    sotienbn = 0;
                    sotientc = 0;

                    return true;
                    #endregion}}
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        #region Phiếu Chi

        public static bool _InPhieuChi(QLBV_Database.QLBVEntities data, int _MaBN)
        {
            try
            {
                QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
                #region ( Bien.MaBV == "27023" || Bien.MaBV == "27021" || Bien.MaBV == "20001")
                if (Bien.MaBV == "27023" || Bien.MaBV == "27021" || Bien.MaBV == "20001")
                {
                    string _tronggio = "";
                    BaoCao.repThuChi_2lien_A5_27023 rep = new BaoCao.repThuChi_2lien_A5_27023();
                    double sotientu = 0;
                    double sotienbn = 0;
                    double sotientc = 0;
                    var q = (from bn in data.BenhNhans
                             join tt in data.TamUngs.Where(p => p.MaBNhan == _MaBN) on bn.MaBNhan equals tt.MaBNhan
                             join cb in data.CanBoes on tt.MaCB equals cb.MaCB into kq
                             from kq2 in kq.DefaultIfEmpty()
                             select new { bn.MaBNhan, tt.QuyenHD, tt.SoHD, bn.NoiTru, bn.DTuong, tt.TienChenh, bn.SoTT, tt.NgoaiGio, tt.PhanLoai, bn.TenBNhan, tt.MaKP, bn.NamSinh, bn.DChi, tt.SoTien, TenCB = kq2 == null ? "" : kq2.TenCB, tt.LyDo, tt.NgayThu, tt.IDTamUng }).ToList();
                    if (q.Count > 0)
                    {
                        if (q.Where(p => p.PhanLoai == 2).ToList().Count > 0 && q.Where(p => p.PhanLoai == 2).First().NgoaiGio != null)
                            if (q.Where(p => p.PhanLoai == 2).First().NgoaiGio.Value == 1)
                            {
                                _tronggio = "Ngoài giờ";
                            }
                            else
                            {
                                _tronggio = "Trong giờ";
                            }
                        int _makp = 0;
                        if (q.Where(p => p.PhanLoai == 2).ToList().Count > 0 && q.Where(p => p.PhanLoai == 2).First().MaKP != null)
                            _makp = q.Where(p => p.PhanLoai == 2).First().MaKP.Value;
                        var kp = _dataContext.KPhongs.Where(p => p.MaKP == _makp).ToList();
                        if (kp.Count > 0)
                            rep.TenKP.Value = "Bộ phận: " + kp.First().TenKP;
                        rep.TrongGio.Value = "Chi: " + _tronggio;
                        rep.TenBNhan.Value = q.First().TenBNhan.ToUpper();
                        //rep.celNguoiNhan1.Text = q.First().TenBNhan;
                        //rep.celNguoiNhan2.Text = q.First().TenBNhan;
                        rep.lblMauSo.Text = "Mẫu số C31 - BB";
                        rep.lblMauSo2.Text = "Mẫu số C31 - BB";
                        rep.NamSinh.Value = q.First().NamSinh;
                        rep.DChi.Value = q.First().DChi;
                        rep.lblLyDoThu.Value = "Lý do chi";
                        if (q.Where(p => p.PhanLoai == 2).ToList().Count > 0)
                        {
                            rep.LyDoThu.Value = q.Where(p => p.PhanLoai == 2).First().LyDo;
                            rep.MST.Value = "MST:";
                            DateTime ngayduyet = q.Where(p => p.PhanLoai == 2).First().NgayThu.Value;
                            rep.NgayDuyet.Value = "Ngày " + ngayduyet.Day.ToString("D2") + " tháng " + ngayduyet.Month.ToString("D2") + " năm " + ngayduyet.Year;
                        }
                        rep.TieuDe.Value = "PHIẾU CHI";
                        rep.NgayThang.Value = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                        //rep.TenKP.Value = q.First().TenKP; // gan sau                       
                        rep.TenCB.Value = q.First().TenCB;
                        rep.lblKy1.Value = "Thủ quỹ";
                        rep.lblKy2.Value = "Người nhận tiền";
                        if (Bien.MaBV == "27023")
                        {
                            rep.lblNguoiLap11.Text = "Người nhận tiền";
                            rep.lblNguoiLap21.Text = "Người nhận tiền";
                        }

                        if (q.First().NgayThu != null)
                        {
                            rep.NgayThang.Value = DungChung.Ham.NgaySangChu(q.First().NgayThu.Value);
                        }

                        if (q.Where(p => p.PhanLoai == 2).ToList().Count > 0 && q.Where(p => p.PhanLoai == 2).Sum(p => p.SoTien).Value > 0)
                        {
                            sotienbn = q.Where(p => p.PhanLoai == 2).Sum(p => p.SoTien).Value;
                            rep.SoPhieu.Value = q.Where(p => p.PhanLoai == 2).First().SoHD;
                            rep.QuyenHD.Value = q.Where(p => p.PhanLoai == 2).FirstOrDefault().QuyenHD;
                        }

                        sotientc = q.Where(p => p.PhanLoai == 2).Sum(p => p.TienChenh);
                        sotientu = q.Where(p => p.PhanLoai == 0).Sum(p => p.SoTien).Value - q.Where(p => p.PhanLoai == 4).Sum(p => p.SoTien).Value;
                    }
                    string _tenloai = "";
                    if (q.Count > 0)
                    {
                        if (q.First().DTuong == "KSK")
                        {
                            _tenloai = "Tên loại phí, lệ phí:Chi trả tạm thu Khám sức khỏe";
                            rep.SoTT.Value = "STT:" + q.First().SoTT;
                        }
                        else
                        {
                            if (q.First().DTuong == "BHYT")
                            {
                                _tenloai = "Tên loại phí, lệ phí: Chi trả tạm thu - BHYT";
                            }
                            else
                            {
                                _tenloai = "Tên loại phí, lệ phí: Chi trả tạm thu - Thu phí";
                            }
                            if (q.First().NoiTru.Value == 0)
                                _tenloai += " - Ngoại trú";
                            else
                                _tenloai += " - Nội trú";
                        }
                        if (Bien.MaBV == "27001")
                        {
                            rep.SoTT.Value = "Mã BN:" + q.First().MaBNhan;
                        }
                    }

                    rep.SoTienNop.Value = sotienbn;
                    rep.SoTien.Value = sotientc;
                    rep.SoTienTraNop.Value = sotientc;


                    rep.TienBNtranop.Value = "Tiền bệnh nhân được trả lại:";
                    rep.NguoiNopThu.Value = "Người chi tiền";
                    rep.TenLoai.Value = _tenloai;
                    rep.DocTien.Value = DungChung.Ham.DocTienBangChu(sotientc, " đồng.");
                    rep.CreateDocument();
                    frmIn frm = new frmIn();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                    sotientu = 0;
                    sotienbn = 0;
                    sotientc = 0;

                    return true;
                }
                #endregion
                #region BV 27183
                else if (Bien.MaBV == "27183")
                {
                    string _tronggio = "";
                    BaoCao.rep_phieuthuchi_A5_27183 rep = new BaoCao.rep_phieuthuchi_A5_27183();
                    double sotientu = 0;
                    double sotienbn = 0;
                    double sotientc = 0;
                    var q = (from bn in data.BenhNhans
                             join tt in data.TamUngs.Where(p => p.MaBNhan == _MaBN) on bn.MaBNhan equals tt.MaBNhan
                             join cb in data.CanBoes on tt.MaCB equals cb.MaCB into kq
                             from kq2 in kq.DefaultIfEmpty()
                             select new { bn.MaBNhan, tt.QuyenHD, tt.SoHD, bn.NoiTru, bn.DTuong, tt.TienChenh, bn.SoTT, tt.NgoaiGio, tt.PhanLoai, bn.TenBNhan, tt.MaKP, bn.NamSinh, bn.DChi, tt.SoTien, TenCB = kq2 == null ? "" : kq2.TenCB, tt.LyDo, tt.NgayThu, tt.IDTamUng }).ToList();
                    if (q.Count > 0)
                    {
                        if (q.Where(p => p.PhanLoai == 2).ToList().Count > 0 && q.Where(p => p.PhanLoai == 2).First().NgoaiGio != null)
                            if (q.Where(p => p.PhanLoai == 2).First().NgoaiGio.Value == 1)
                            {
                                _tronggio = "Ngoài giờ";
                            }
                            else
                            {
                                _tronggio = "Trong giờ";
                            }
                        int _makp = 0;
                        if (q.Where(p => p.PhanLoai == 2).ToList().Count > 0 && q.Where(p => p.PhanLoai == 2).First().MaKP != null)
                            _makp = q.Where(p => p.PhanLoai == 2).First().MaKP.Value;
                        var kp = _dataContext.KPhongs.Where(p => p.MaKP == _makp).ToList();
                        if (kp.Count > 0)
                            rep.TenKP.Value = "Bộ phận: " + kp.First().TenKP;
                        rep.TrongGio.Value = "Chi: " + _tronggio;
                        rep.TenBNhan.Value = q.First().TenBNhan.ToUpper();
                        rep.lblMauSo.Text = "Mẫu số C31 - BB";
                        rep.NamSinh.Value = q.First().NamSinh;
                        rep.DChi.Value = q.First().DChi;
                        rep.lblLyDoThu.Value = "Lý do chi";
                        if (q.Where(p => p.PhanLoai == 2).ToList().Count > 0)
                        {
                            rep.LyDoThu.Value = q.Where(p => p.PhanLoai == 2).First().LyDo;
                            rep.MST.Value = "MST:";
                            DateTime ngayduyet = q.Where(p => p.PhanLoai == 2).First().NgayThu.Value;
                            rep.NgayDuyet.Value = "Ngày " + ngayduyet.Day.ToString("D2") + " tháng " + ngayduyet.Month.ToString("D2") + " năm " + ngayduyet.Year;
                        }
                        rep.TieuDe.Value = "PHIẾU CHI";
                        rep.NgayThang.Value = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                        rep.TenCB.Value = q.First().TenCB;
                        rep.lblKy1.Value = "Thủ quỹ";
                        rep.lblKy2.Value = "Người nhận tiền";

                        if (q.First().NgayThu != null)
                        {
                            rep.NgayThang.Value = DungChung.Ham.NgaySangChu(q.First().NgayThu.Value);
                        }

                        if (q.Where(p => p.PhanLoai == 2).ToList().Count > 0 && q.Where(p => p.PhanLoai == 2).Sum(p => p.SoTien).Value > 0)
                        {
                            sotienbn = q.Where(p => p.PhanLoai == 2).Sum(p => p.SoTien).Value;
                            rep.SoPhieu.Value = q.Where(p => p.PhanLoai == 2).First().SoHD;
                            rep.QuyenHD.Value = q.Where(p => p.PhanLoai == 2).FirstOrDefault().QuyenHD;
                        }

                        sotientc = q.Where(p => p.PhanLoai == 2).Sum(p => p.TienChenh);
                        sotientu = q.Where(p => p.PhanLoai == 0).Sum(p => p.SoTien).Value - q.Where(p => p.PhanLoai == 4).Sum(p => p.SoTien).Value;
                    }
                    string _tenloai = "";
                    if (q.Count > 0)
                    {
                        if (q.First().DTuong == "KSK")
                        {
                            _tenloai = "Tên loại phí, lệ phí:Chi trả tạm thu Khám sức khỏe";
                            rep.SoTT.Value = "STT:" + q.First().SoTT;
                        }
                        else
                        {
                            if (q.First().DTuong == "BHYT")
                            {
                                _tenloai = "Tên loại phí, lệ phí: Chi trả tạm thu - BHYT";
                            }
                            else
                            {
                                _tenloai = "Tên loại phí, lệ phí: Chi trả tạm thu - Thu phí";
                            }
                            if (q.First().NoiTru.Value == 0)
                                _tenloai += " - Ngoại trú";
                            else
                                _tenloai += " - Nội trú";
                        }

                    }

                    rep.SoTienNop.Value = sotienbn;
                    rep.SoTien.Value = sotientc;
                    rep.SoTienTraNop.Value = sotientc;


                    rep.TienBNtranop.Value = "Tiền bệnh nhân được trả lại:";
                    rep.NguoiNopThu.Value = "Người chi tiền";
                    rep.TenLoai.Value = _tenloai;
                    rep.DocTien.Value = DungChung.Ham.DocTienBangChu(sotientc, " đồng.");
                    rep.CreateDocument();
                    frmIn frm = new frmIn();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                    sotientu = 0;
                    sotienbn = 0;
                    sotientc = 0;

                    return true;
                }
                #endregion

                else
                {
                    #region

                    string _tronggio = "";
                    BaoCao.repThuChi_2lien_A5 rep = new BaoCao.repThuChi_2lien_A5();
                    double sotientu = 0;
                    double sotienbn = 0;
                    double sotientc = 0;
                    var q = (from bn in data.BenhNhans
                             join tt in data.TamUngs.Where(p => p.MaBNhan == _MaBN) on bn.MaBNhan equals tt.MaBNhan
                             join cb in data.CanBoes on tt.MaCB equals cb.MaCB into kq
                             from kq2 in kq.DefaultIfEmpty()
                             select new { bn.MaBNhan, tt.QuyenHD, tt.SoHD, bn.NoiTru, bn.DTuong, tt.TienChenh, bn.SoTT, tt.NgoaiGio, tt.PhanLoai, bn.TenBNhan, tt.MaKP, bn.NamSinh, bn.DChi, tt.SoTien, TenCB = kq2 == null ? "" : kq2.TenCB, tt.LyDo, tt.NgayThu, tt.IDTamUng }).ToList();
                    if (q.Count > 0)
                    {
                        if (q.Where(p => p.PhanLoai == 2).ToList().Count > 0 && q.Where(p => p.PhanLoai == 2).First().NgoaiGio != null)
                            if (q.Where(p => p.PhanLoai == 2).First().NgoaiGio.Value == 1)
                            {
                                _tronggio = "Ngoài giờ";
                            }
                            else
                            {
                                _tronggio = "Trong giờ";
                            }
                        int _makp = 0;
                        if (q.Where(p => p.PhanLoai == 2).ToList().Count > 0 && q.Where(p => p.PhanLoai == 2).First().MaKP != null)
                            _makp = q.Where(p => p.PhanLoai == 2).First().MaKP.Value;
                        var kp = _dataContext.KPhongs.Where(p => p.MaKP == _makp).ToList();
                        if (kp.Count > 0)
                            rep.TenKP.Value = kp.First().TenKP;
                        rep.TrongGio.Value = "Chi: " + _tronggio;
                        rep.TenBNhan.Value = q.First().TenBNhan.ToUpper();
                        rep.NamSinh.Value = q.First().NamSinh;
                        rep.DChi.Value = q.First().DChi;

                        //rep.TenKP.Value = q.First().TenKP; // gan sau
                        if (q.Where(p => p.PhanLoai == 2).ToList().Count > 0)
                            rep.LyDoThu.Value = q.Where(p => p.PhanLoai == 2).First().LyDo;
                        rep.TenCB.Value = q.First().TenCB;
                        if (q.First().NgayThu != null && Bien.MaBV != "27022")
                        {
                            rep.NgayThang.Value = DungChung.Ham.NgaySangChu(q.First().NgayThu.Value);
                        }

                        if (q.Where(p => p.PhanLoai == 2).ToList().Count > 0 && q.Where(p => p.PhanLoai == 2).Sum(p => p.SoTien).Value > 0)
                        {
                            sotienbn = q.Where(p => p.PhanLoai == 2).Sum(p => p.SoTien).Value;
                            rep.SoPhieu.Value = q.Where(p => p.PhanLoai == 2).First().SoHD;
                            rep.QuyenHD.Value = q.Where(p => p.PhanLoai == 2).FirstOrDefault().QuyenHD;
                        }

                        sotientc = q.Where(p => p.PhanLoai == 2).Sum(p => p.TienChenh);
                        sotientu = q.Where(p => p.PhanLoai == 0).Sum(p => p.SoTien).Value - q.Where(p => p.PhanLoai == 4).Sum(p => p.SoTien).Value;
                    }
                    string _tenloai = "";
                    if (q.Count > 0)
                    {
                        if (q.First().DTuong == "KSK")
                        {
                            _tenloai = "Tên loại phí, lệ phí:Chi trả tạm thu Khám sức khỏe";
                            rep.SoTT.Value = "STT:" + q.First().SoTT;
                        }
                        else
                        {
                            if (q.First().DTuong == "BHYT")
                            {
                                _tenloai = "Tên loại phí, lệ phí: Chi trả tạm thu - BHYT";
                            }
                            else
                            {
                                _tenloai = "Tên loại phí, lệ phí: Chi trả tạm thu - Thu phí";
                            }
                            if (q.First().NoiTru.Value == 0)
                                _tenloai += " - Ngoại trú";
                            else
                                _tenloai += " - Nội trú";
                        }
                        if (Bien.MaBV == "27001")
                        {
                            rep.SoTT.Value = "Mã BN:" + q.First().MaBNhan;
                        }
                        if (Bien.MaBV != "27022")
                        {
                            rep.celNguoiNhan.Text = q.First().TenBNhan;
                            rep.celNgNhanTien.Text = q.First().TenBNhan;
                        }
                    }

                    rep.SoTienNop.Value = sotienbn;
                    rep.SoTien.Value = sotientu;
                    rep.SoTienTraNop.Value = sotientc;
                    rep.mauso.Value = "01BLP2-001";
                    if (Bien.MaBV == "30004")
                        rep.TieuDe.Value = "PHIẾU THU TIỀN PHÍ, LỆ PHÍ";
                    else if (Bien.MaBV == "27022")
                        rep.TieuDe.Value = "BIÊN LAI CHI TIỀN VIỀN PHÍ, LỆ PHÍ";
                    else
                        if (Bien.MaBV == "01071" || Bien.MaBV == "01049" || Bien.MaBV == "12345" || Bien.MaBV == "24297" || Bien.MaBV == "56789")
                    {
                        _tenloai = "";
                        rep.TieuDe.Value = "PHIẾU HOÀN ỨNG VIỆN PHÍ";
                    }

                    else
                        rep.TieuDe.Value = "BIÊN LAI THU TIỀN PHÍ, LỆ PHÍ";
                    rep.TienBNtranop.Value = "Tiền bệnh nhân được trả lại:";
                    rep.NguoiNopThu.Value = "Người chi tiền";
                    if (Bien.MaBV == "27022")
                    {
                        rep.xrTable4.Visible = true;
                        rep.xrTable3.Visible = false;
                        rep.NguoiNopThu.Value = "Người nhận tiền";
                        rep.xrTable6.Visible = true;
                        rep.xrTable2.Visible = false;
                    }
                    else
                    {
                        rep.xrTable4.Visible = false;
                        rep.xrTable3.Visible = true;
                        rep.xrTable6.Visible = false;
                        rep.xrTable2.Visible = true;
                    }

                    rep.TenLoai.Value = _tenloai;
                    rep.DocTien.Value = DungChung.Ham.DocTienBangChu(sotientc, " đồng.");
                    rep.CreateDocument();
                    frmIn frm = new frmIn();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                    sotientu = 0;
                    sotienbn = 0;
                    sotientc = 0;

                    return true;
                    #endregion
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool _InphieuhoanUng01071(int sophieu)
        {

            try
            {
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(Bien.StrCon);
                string _tronggio = "";
                BaoCao.repThuChi_2lien_A4 rep = new BaoCao.repThuChi_2lien_A4();
                double sotientu = 0;
                double sotienbn = 0;
                double sotientc = 0;
                var q = (from bn in data.BenhNhans
                         join tt in data.TamUngs.Where(p => p.IDTamUng == sophieu) on bn.MaBNhan equals tt.MaBNhan
                         join cb in data.CanBoes on tt.MaCB equals cb.MaCB into kq
                         from kq2 in kq.DefaultIfEmpty()
                         select new { bn.MaBNhan, tt.QuyenHD, tt.SoHD, bn.NoiTru, bn.DTuong, tt.TienChenh, bn.SoTT, tt.NgoaiGio, tt.PhanLoai, bn.TenBNhan, tt.MaKP, bn.NamSinh, bn.DChi, tt.SoTien, TenCB = kq2 == null ? "" : kq2.TenCB, tt.LyDo, tt.NgayThu, tt.IDTamUng }).ToList();
                if (q.Count > 0)
                {

                    int _makp = 0;
                    if (q.Where(p => p.PhanLoai == 4).ToList().Count > 0 && q.Where(p => p.PhanLoai == 4).First().MaKP != null)
                        _makp = q.Where(p => p.PhanLoai == 4).First().MaKP.Value;
                    var kp = data.KPhongs.Where(p => p.MaKP == _makp).ToList();
                    if (kp.Count > 0)
                        rep.TenKP.Value = kp.First().TenKP;
                    rep.TrongGio.Value = "Chi: " + _tronggio;
                    rep.TenBNhan.Value = q.First().TenBNhan.ToUpper();
                    rep.NamSinh.Value = q.First().NamSinh;
                    rep.DChi.Value = q.First().DChi;

                    //rep.TenKP.Value = q.First().TenKP; // gan sau
                    if (q.Where(p => p.PhanLoai == 4).ToList().Count > 0)
                        rep.LyDoThu.Value = q.Where(p => p.PhanLoai == 4).First().LyDo;
                    rep.TenCB.Value = q.First().TenCB;
                    if (q.First().NgayThu != null)
                    {
                        rep.NgayThang.Value = DungChung.Ham.NgaySangChu(q.First().NgayThu.Value);
                    }

                    if (q.Where(p => p.PhanLoai == 4).ToList().Count > 0 && q.Where(p => p.PhanLoai == 4).Sum(p => p.SoTien).Value > 0)
                    {
                        sotienbn = q.Where(p => p.PhanLoai == 4).Sum(p => p.SoTien).Value;
                        rep.SoPhieu.Value = q.Where(p => p.PhanLoai == 4).First().SoHD;
                        rep.QuyenHD.Value = q.Where(p => p.PhanLoai == 4).FirstOrDefault().QuyenHD;
                    }


                    sotientu = q.Where(p => p.PhanLoai == 4).Sum(p => p.SoTien).Value;
                }

                if (q.Count > 0)
                {
                    rep.SoTT.Value = "STT:" + q.First().SoTT;
                }


                rep.SoTien.Value = sotientu;
                rep.mauso.Value = "01BLP2-001";
                rep.TieuDe.Value = "PHIẾU HOÀN ỨNG VIỆN PHÍ";
                rep.NguoiNopThu.Value = "Người chi tiền";
                rep.MaBN.Value = "Mã BN: " + q.First().MaBNhan.ToString();
                //rep.benhnhan.Value = q.First().TenBNhan;
                rep.DocTien.Value = DungChung.Ham.DocTienBangChu(sotientu, " đồng.");
                rep.CreateDocument();
                frmIn frm = new frmIn();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
                sotientu = 0;
                sotienbn = 0;
                sotientc = 0;

                return true;
            }
            catch
            {
                return false;
            }



        }
        #endregion
        #region Phiếu Thu thẳng
        public static bool _InPhieuThuThang(QLBV_Database.QLBVEntities data, int _Sophieu)
        {
            try
            {
                QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
                BaoCao.repThuChi_2lien_A5 rep = new BaoCao.repThuChi_2lien_A5();
                double sotientu = 0;
                double sotientc = 0;
                int _idtthu = _Sophieu;
                string _tronggio = "";
                int _MaBN = 0;
                var q = (from bn in data.BenhNhans
                         join tt in data.TamUngs.Where(p => p.IDTamUng == _idtthu) on bn.MaBNhan equals tt.MaBNhan
                         join cb in data.CanBoes on tt.MaCB equals cb.MaCB
                         select new { bn.DTuong, bn.NoiTru, bn.MaBNhan, bn.SoTT, tt.NgoaiGio, tt.PhanLoai, bn.TenBNhan, tt.MaKP, bn.NamSinh, bn.DChi, tt.SoTien, cb.TenCB, tt.LyDo, tt.NgayThu, tt.IDTamUng }).ToList();
                if (q.Count > 0)
                {
                    _MaBN = q.First().MaBNhan;
                    if (q.Where(p => p.PhanLoai == 3).ToList().Count > 0 && q.Where(p => p.PhanLoai == 3).First().NgoaiGio != null)
                        if (q.Where(p => p.PhanLoai == 3).First().NgoaiGio.Value == 1)
                        {
                            _tronggio = "Ngoài giờ";
                        }
                        else
                        {
                            _tronggio = "Trong giờ";
                        }
                    int _makp = 0;
                    if (q.Where(p => p.PhanLoai == 3).ToList().Count > 0 && q.Where(p => p.PhanLoai == 3).First().MaKP != null)
                        _makp = q.Where(p => p.PhanLoai == 3).First().MaKP.Value;
                    var kp = _dataContext.KPhongs.Where(p => p.MaKP == _makp).ToList();
                    if (kp.Count > 0)
                        rep.TenKP.Value = kp.First().TenKP;
                    rep.TrongGio.Value = "Thu: " + _tronggio;
                    rep.TenBNhan.Value = q.First().TenBNhan.ToUpper();
                    rep.NamSinh.Value = q.First().NamSinh;
                    rep.DChi.Value = q.First().DChi;
                    rep.SoPhieu.Value = q.First().IDTamUng;
                    //rep.TenKP.Value = q.First().TenKP; // gan sau
                    if (q.Where(p => p.PhanLoai == 3).ToList().Count > 0)
                        rep.LyDoThu.Value = q.Where(p => p.PhanLoai == 3).First().LyDo;
                    rep.TenCB.Value = q.First().TenCB;
                    if (q.First().NgayThu != null)
                    {
                        rep.NgayThang.Value = DungChung.Ham.NgaySangChu(q.First().NgayThu.Value);
                    }

                    if (q.Where(p => p.PhanLoai == 3).ToList().Count > 0 && q.Where(p => p.PhanLoai == 3).Sum(p => p.SoTien).Value > 0)
                    {
                        sotientc = q.Where(p => p.PhanLoai == 3).Sum(p => p.SoTien).Value;
                        rep.SoPhieu.Value = q.Where(p => p.PhanLoai == 3).First().IDTamUng;
                    }
                    //if (q.Where(p => p.PhanLoai == 0).ToList().Count > 0 && q.Where(p => p.PhanLoai == 0).Sum(p => p.SoTien).Value > 0)
                    //{
                    //    sotientu = q.Where(p => p.PhanLoai == 0).Sum(p => p.SoTien).Value;
                    //}
                }
                string _tenloai = "";
                if (q.Count > 0)
                {
                    if (q.First().DTuong == "KSK")
                    {
                        _tenloai = "Tên loại phí, lệ phí:Chi trả tạm thu Khám sức khỏe";
                        rep.SoTT.Value = "STT:" + q.First().SoTT;
                    }
                    else
                    {
                        if (q.First().DTuong == "BHYT")
                        {
                            _tenloai = "Tên loại phí, lệ phí:  Chi phí KCB - BHYT";
                        }
                        else
                        {
                            _tenloai = "Tên loại phí, lệ phí:  Chi phí KCB - Thu phí";
                        }
                        if (q.First().NoiTru.Value == 0)
                            _tenloai += " - Ngoại trú";
                        else
                            _tenloai += " - Nội trú";
                    }
                    rep.SoTT.Value = "Mã BN: " + q.First().MaBNhan;
                }

                rep.SoTienNop.Value = 0;
                rep.SoTien.Value = sotientu;
                rep.SoTienTraNop.Value = sotientc;
                rep.mauso.Value = "01BLP2-001";
                if (Bien.MaBV == "30004")
                    rep.TieuDe.Value = "PHIẾU THU TIỀN PHÍ, LỆ PHÍ";
                else
                    rep.TieuDe.Value = "BIÊN LAI THU TIỀN PHÍ, LỆ PHÍ";
                rep.TienBNtranop.Value = "Tiền bệnh nhân phải nộp thêm:";
                rep.NguoiNopThu.Value = "Người thu tiền";
                rep.TenLoai.Value = _tenloai;
                rep.DocTien.Value = DungChung.Ham.DocTienBangChu(sotientc, " đồng.");
                rep.CreateDocument();
                frmIn frm = new frmIn();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
                sotientu = 0;
                sotientc = 0;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        #region phiếu thu thẳng _BV Công An
        public static bool InPhieuThu_CATQ(QLBV_Database.QLBVEntities _data, int _ID)
        {
            var q = _data.TamUngs.Where(p => p.IDTamUng == _ID).ToList();
            if (q.Count > 0 && q.First().PhanLoai == 3)
            {
                int _Mbn = q.First().MaBNhan == null ? 0 : q.First().MaBNhan.Value;
                var q1 = _data.BenhNhans.Where(p => p.MaBNhan == _Mbn).ToList();
                if (q1.Count > 0)
                {
                    BaoCao.Rep_PhieuthuCATQ rep = new BaoCao.Rep_PhieuthuCATQ();
                    rep.Hoten.Value = q1.First().TenBNhan;
                    rep.Diachi.Value = q1.First().DChi;
                    rep.Sotien.Value = q.First().SoTien;
                    rep.Tuoi.Value = q1.First().Tuoi.Value;
                    double st = Convert.ToDouble(q.First().SoTien);
                    rep.Bangchu.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng/.");
                    DateTime ngay = Convert.ToDateTime(q.First().NgayThu);
                    rep.Ngaythu.Value = DungChung.Ham.NgaySangChu(ngay);
                    frmIn frm = new frmIn();
                    rep.CreateDocument();
                    rep.DataMember = "Table";
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                    return true;
                }
                return false;
            }

            return false;
        }


        #endregion
        #region phiếu thu nam thăng long 010171
        private class Listdv01071
        {
            public int? STT { get; set; }
            public string TenNhom { get; set; }
            public string TenDV { get; set; }
            public double ChiPhi { get; set; }
            public double TenBN { get; set; }
            public string TrongDM { get; set; }
            public int TrongBH { get; set; }
        }
        public static bool _intMauChiTietA5 = false;
        public static bool _InPhieuThu_01071(int _IdVienPhi, int _Ploai, bool _inchitiet)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(Bien.StrCon);
            List<Listdv01071> _lkq = new List<Listdv01071>();
            int _MaBN = 0;
            var _ldv = (from n in _data.NhomDVs
                        join tn in _data.TieuNhomDVs on n.IDNhom equals tn.IDNhom
                        join dv in _data.DichVus on tn.IdTieuNhom equals dv.IdTieuNhom
                        select new { n.TenNhomCT, dv.PLoai, dv.TenDV, tn.TenRG, dv.MaDV, n.IDNhom, n.STT }).ToList();
            if (_Ploai == 1) //Thu thường
            {
                var _lVienphi = (from vp in _data.TamUngs.Where(p => p.IDTamUng == _IdVienPhi)
                                 select new
                                 {
                                     vp.MaBNhan,
                                     vp.NgayThu
                                 }).ToList();
                if (_lVienphi.Count() > 0)
                {
                    _MaBN = Convert.ToInt32(_lVienphi.First().MaBNhan);

                    var _lTTbn = (from bn in _data.BenhNhans.Where(p => p.MaBNhan == _MaBN)
                                  select new
                                  {
                                      bn.TenBNhan,
                                      bn.GTinh,
                                      bn.SThe,
                                      bn.Tuoi,
                                      bn.NNhap,
                                      bn.DChi,
                                      bn.DTuong
                                  }).ToList();


                    var _lvphict = (from vp in _data.VienPhis.Where(p => p.MaBNhan == _MaBN)
                                    join vpct in _data.VienPhicts.Where(p => p.ThanhToan == 0).Where(p => p.TrongBH == 0 || p.TrongBH == 1) on vp.idVPhi equals vpct.idVPhi
                                    select new { vpct.MaDV, vpct.TienBN, vpct.ThanhTien, vpct.TrongBH }).ToList();
                    if (_lTTbn.Count() > 0)
                    {
                        frmIn frm = new frmIn();
                        if (_intMauChiTietA5)
                        {
                            BaoCao.rep_phieuthu01071_A5 rep = new BaoCao.rep_phieuthu01071_A5(_MaBN, _IdVienPhi, _Ploai, _inchitiet);
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                            _intMauChiTietA5 = false;
                            return true;
                        }
                        else
                        {
                            BaoCao.rep_phieuthu01071 rep = new BaoCao.rep_phieuthu01071(_MaBN, _IdVienPhi, _Ploai, _inchitiet);
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                            return true;
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Bệnh nhân chưa thanh toán", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }
            }
            else // thu trực tiếp
            {
                var _lTamThu = (from tu in _data.TamUngs.Where(p => p.IDTamUng == _IdVienPhi)
                                select new { tu.IDTamUng, tu.MaBNhan, tu.NgayThu }).ToList();
                if (_lTamThu.Count() > 0)
                {
                    _MaBN = Convert.ToInt32(_lTamThu.First().MaBNhan);
                    var _lTTbn = (from bn in _data.BenhNhans.Where(p => p.MaBNhan == _MaBN)
                                  select new
                                  {
                                      bn.TenBNhan,
                                      bn.GTinh,
                                      bn.SThe,
                                      bn.Tuoi,
                                      bn.NNhap,
                                      bn.DChi,
                                      bn.DTuong
                                  }).ToList();

                    var _lTamungct = (from tuct in _data.TamUngcts.Where(p => p.IDTamUng == _IdVienPhi)
                                      select new { tuct.MaDV, tuct.TienBN, tuct.ThanhTien }).ToList();
                    if (_lTTbn.Count() > 0)
                    {
                        frmIn frm = new frmIn();
                        if (_intMauChiTietA5)
                        {
                            BaoCao.rep_phieuthu01071_A5 rep = new BaoCao.rep_phieuthu01071_A5(_MaBN, _IdVienPhi, _Ploai, _inchitiet);
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                            _intMauChiTietA5 = false;
                            return true;
                        }
                        else
                        {
                            BaoCao.rep_phieuthu01071 rep = new BaoCao.rep_phieuthu01071(_MaBN, _IdVienPhi, _Ploai, _inchitiet);
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                            return true;
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Chưa thu trực tiếp", "Thông báo", MessageBoxButtons.OK);
                    return true;
                }
            }
            return false;
        }
        #endregion
        public static bool InPhieuThuChi(int sophieu, int mabn)
        {
            int _Sophieu = sophieu;
            int _MaBN = mabn;
            QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(Bien.StrCon);
            int _mauin = 0;
            int _mauinPT = 0;
            var qthong = _Data.HTHONGs.Where(p => p.MaBV == Bien.MaBV).Where(p => p.MauIn != null).Select(p => p.MauIn).FirstOrDefault();
            if (qthong != null)
            {
                string[] mauin = qthong.Split(';');
                if (mauin.Length > 2)
                    _mauin = Convert.ToInt32(mauin[2]);
                if (mauin.Length > 3)
                    _mauinPT = Convert.ToInt32(mauin[3]);
            }
            if (Bien.MaBV == "19048")
            {
                return usTamThu_TToan._phieuThu_Chi_TN(_Data, _Sophieu);
            }
            // ngày 14/09/16 quynv yêu cầu bỏ, thực hiện theo BV 30005
            //if (Bien.MaBV == "30002")
            //{
            //    return _InPhieu_BinhGiang(_Data, _Sophieu);
            //}

            else
            {
                int _ploaitamthu = -1;
                var q1 = _Data.TamUngs.Where(p => p.IDTamUng == _Sophieu).ToList();
                if (q1.Count > 0)
                    _ploaitamthu = q1.First().PhanLoai.Value;
                frmIn frm = new frmIn();
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(Bien.StrCon);
                if (_Sophieu > 0)
                {
                    if (Bien.MaBV == "24009")
                    {
                        return usTamThu_TToan.InPhieuChi_24009(_Data, _Sophieu);
                    }
                    else
                    {
                        if (_ploaitamthu == 0 || _ploaitamthu == 7)
                        {
                            if (Bien.MaBV == "04006") // || Bien.MaBV == "30003" in giống mẫu thường tín, aquy y/s 24.03 đức
                            {
                                return usTamThu_TToan._InPhieuTamThu_04006_30003(_Data, _Sophieu);
                            }
                            else if (Bien.MaBV == "30303")
                            {
                                //frmIn frm = new frmIn();
                                BaoCao.repTamUngVienPhi_30303 rep = usTamThu_TToan.in2lienTamUng_30303(data, _MaBN, _Sophieu, "Liên 1: Lưu tại cơ quan");
                                BaoCao.repTamUngVienPhi_30303 rep2 = usTamThu_TToan.in2lienTamUng_30303(data, _MaBN, _Sophieu, "Liên 2: Giao cho bệnh nhân");
                                rep.Pages.AddRange(rep2.Pages);
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                                return true;
                            }
                            else
                            {
                                if (Bien.MaBV == "24009" || Bien.MaTinh == "12" || Bien.MaBV == "01071" || Bien.MaBV == "01049" || Bien.MaBV == "12345" || Bien.MaBV == "24297" || Bien.MaBV == "56789" || Bien.MaBV == "27123")// || Bien.MaBV == "30303"
                                    return usTamThu_TToan._InPhieuTamThu_VY(_Data, _Sophieu);
                                else
                                    return usTamThu_TToan._InPhieuTamThu(_Data, _Sophieu, _mauinPT);
                            }
                        }
                        if (_ploaitamthu == 1)
                        {//his2106 dự 30303 giống 30009
                            if (Bien.MaTinh == "12" || Bien.MaBV == "30303" || Bien.MaBV == "01830" || Bien.MaBV == "30003" || Bien.MaBV == "30002" || Bien.MaBV == "30005" || Bien.MaBV == "30010" || Bien.MaBV == "30281" || Bien.MaBV == "30280" || Bien.MaBV == "30009" || Bien.MaBV == "30340")
                            {
                                return usTamThu_TToan._InPhieu_KinhMon(_Data, _Sophieu, (Bien.MaBV == "30002" || Bien.MaBV == "30005") ? true : false);
                            }
                            if (Bien.MaBV == "08204")
                                return usTamThu_TToan.In_HoaDon_TT05(_Sophieu);
                            if (Bien.MaBV == "01071" || Bien.MaBV == "01049" || Bien.MaBV == "12345" || Bien.MaBV == "24297" || Bien.MaBV == "56789" || Bien.MaBV == "27123")// || Bien.MaBV == "30303" 
                                return usTamThu_TToan._InPhieuThu_01071(_Sophieu, _ploaitamthu, false);
                            return usTamThu_TToan._InPhieuThu(_Data, mabn);
                        }
                        if (_ploaitamthu == 2)
                        {
                            if (Bien.MaTinh == "12" || Bien.MaBV == "01830" || Bien.MaBV == "30003" || Bien.MaBV == "30002" || Bien.MaBV == "30005" || Bien.MaBV == "30010" || Bien.MaBV == "30281" || Bien.MaBV == "30280" || Bien.MaBV == "30009" || Bien.MaBV == "30340")
                            {
                                return usTamThu_TToan._InPhieu_KinhMon(_Data, _Sophieu, Bien.MaBV == "30002" ? true : false);
                            }
                            return usTamThu_TToan._InPhieuChi(_Data, mabn);
                        }

                        if (_ploaitamthu == 3)
                        {//his2106 dự 30303 giống 30009
                            if (Bien.MaTinh == "12" || Bien.MaBV == "30303" || Bien.MaBV == "01830" || Bien.MaBV == "30003" || Bien.MaBV == "30002" || Bien.MaBV == "30005" || Bien.MaBV == "30010" || Bien.MaBV == "30281" || Bien.MaBV == "30280" || Bien.MaBV == "30009" || Bien.MaBV == "30340")
                            {
                                return usTamThu_TToan._InPhieu_KinhMon(_Data, _Sophieu, (Bien.MaBV == "30002" || Bien.MaBV == "30005") ? true : false);
                            }
                            if (Bien.MaBV == "08104")
                                return usTamThu_TToan.InPhieuThu_CATQ(_Data, _Sophieu);
                            if (Bien.MaBV == "08204")
                                return usTamThu_TToan.In_HoaDon_TT05(_Sophieu);
                            if (Bien.MaBV == "01071" || Bien.MaBV == "01049" || Bien.MaBV == "12345" || Bien.MaBV == "24297" || Bien.MaBV == "56789" || Bien.MaBV == "27123")
                                return usTamThu_TToan._InPhieuThu_01071(_Sophieu, _ploaitamthu, false);
                            return usTamThu_TToan._InPhieuThuThang(_Data, _Sophieu);
                        }
                        if (_ploaitamthu == 4)
                        {
                            if (Bien.MaBV == "20001")
                                return usTamThu_TToan._InPhieuTamThu(_Data, _Sophieu, _mauinPT);
                            else if (Bien.MaBV == "01071" || Bien.MaBV == "01049" || Bien.MaBV == "12345" || Bien.MaBV == "24297" || Bien.MaBV == "56789")
                                return usTamThu_TToan._InphieuhoanUng01071(_Sophieu);
                            else
                                return usTamThu_TToan.InPhieuChi_VY(_Data, _Sophieu);
                        }
                        return false;
                    }

                }
                else
                {
                    MessageBox.Show("Không có chứng từ");
                    return false;
                }
            }
        }

        public static BaoCao.repThuChi_2lien_A5_30005 _InPhieu_KinhMonTEST(QLBV_Database.QLBVEntities _Data, int _Sophieu, string lien)
        {
            BaoCao.repThuChi_2lien_A5_30005 rep = new BaoCao.repThuChi_2lien_A5_30005();
            var q = _Data.TamUngs.Where(p => p.IDTamUng == _Sophieu).ToList();
            int _MaBN = 0;
            if (q.Count > 0)
            {
                _MaBN = q.First().MaBNhan == null ? 0 : q.First().MaBNhan.Value;
                rep.lbLien.Text = lien;
                frmIn frm = new frmIn();
                rep.lblQuyenSo.Text = "Quyển số:" + q.First().QuyenHD;
                rep.lblSo.Text = "Số:" + q.First().SoHD;
                double st = 0;
                if (q.First().PhanLoai == 1 && (Bien.MaBV != "30005" && Bien.MaBV != "30009"))
                    st = q.First().TienChenh;
                else
                    st = q.First().SoTien.Value;
                var a = _Data.BenhNhans.Where(p => p.MaBNhan == _MaBN).ToList();
                if (a.Count > 0)
                {
                    rep.TenBNhan.Value = a.First().TenBNhan;
                    rep.lblLyDoNop.Text = q.First().LyDo;
                    rep.NamSinh.Value = a.First().NamSinh;
                    rep.DChi.Value = a.First().DChi;
                    int _MKP = q.First().MaKP == null ? 0 : q.First().MaKP.Value;
                    var a1 = _Data.KPhongs.Where(p => p.MaKP == _MKP).ToList();
                    if (a1.Count > 0)
                    {
                        rep.TenKP.Value = "Bộ phận: " + a1.First().TenKP;
                    }


                    rep.SoTienTraNop.Value = Math.Round(st, 0).ToString();
                    rep.DocTien.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng/.");
                    DateTime ngay = Convert.ToDateTime(q.First().NgayThu);
                    rep.NgayThang.Value = DungChung.Ham.NgaySangChu(ngay);
                    var a2 = (from vp in _Data.VienPhis.Where(p => p.MaBNhan == _MaBN)
                              join vpct in _Data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                              select new { vpct.MaDV, vpct.DonGia, vpct.SoLuong, vpct.ThanhTien, vpct.TrongBH, vpct.TienBH, vpct.TienBN }).ToList();
                    if (a2.Count > 0)
                    {
                        double st1 = 0, st2 = 0;
                        //st1 = a2.Where(p => p.TrongBH == 1).Sum(p => p.TienBN);
                        //rep.BHYT.Value = st1;
                        //st2 = a2.Where(p => p.TrongBH == 0).Sum(p => p.TienBN);
                        //rep.NgoaiDM.Value = st2;
                        //rep.STPhaiNop.Value = st1 + st2;
                    }
                    rep.CreateDocument();


                    return rep;
                }
            }
            return rep;

        }
        public static bool _InPhieu_KinhMon(QLBV_Database.QLBVEntities _Data, int _Sophieu, bool hailien)
        {
            try
            {
                frmIn frm = new frmIn();

                BaoCao.repThuChi_2lien_A5_30005 rep = usTamThu_TToan._InPhieu_KinhMonTEST(_Data, _Sophieu, hailien ? "Liên 1: lưu" : "");
                if (hailien)
                {
                    BaoCao.repThuChi_2lien_A5_30005 rep2 = usTamThu_TToan._InPhieu_KinhMonTEST(_Data, _Sophieu, "Liên 2: giao cho khách hàng");
                    rep.Pages.AddRange(rep2.Pages);
                }

                //rep.DataMember = "Table";
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
                return true;
            }
            catch
            {
                return false;
            }

        }
        //public static bool _InPhieu_KinhMon(QLBV_Database.QLBVEntities _Data, int _Sophieu)
        //{

        //    var q = _Data.TamUngs.Where(p => p.IDTamUng == _Sophieu).ToList();

        //    int _MaBN = 0;
        //    if (q.Count > 0)
        //    {
        //        _MaBN = q.First().MaBNhan == null ? 0 : q.First().MaBNhan.Value;
        //        BaoCao.repThuChi_2lien_A5_30005 rep = new BaoCao.repThuChi_2lien_A5_30005();
        //        frmIn frm = new frmIn();
        //        rep.lblQuyenSo.Text = "Quyển số:" + q.First().QuyenHD;
        //        rep.lblSo.Text = "Số:" + q.First().SoHD;
        //        double st = 0;
        //        st = q.First().SoTien.Value;
        //        var a = _Data.BenhNhans.Where(p => p.MaBNhan == _MaBN).ToList();
        //        if (a.Count > 0)
        //        {
        //            rep.TenBNhan.Value = a.First().TenBNhan;
        //            rep.lblLyDoNop.Text = q.First().LyDo;
        //            rep.NamSinh.Value = a.First().NamSinh;
        //            rep.DChi.Value = a.First().DChi;
        //            int _MKP = q.First().MaKP == null ? 0 : q.First().MaKP.Value;
        //            var a1 = _Data.KPhongs.Where(p => p.MaKP == _MKP).ToList();
        //            if (a1.Count > 0)
        //            {
        //                rep.TenKP.Value = "Bộ phận: " + a1.First().TenKP;
        //            }
        //            rep.SoTienTraNop.Value = Math.Round(st, 0).ToString();
        //            rep.DocTien.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng/.");
        //            DateTime ngay = Convert.ToDateTime(q.First().NgayThu);
        //            rep.NgayThang.Value = DungChung.Ham.NgaySangChu(ngay);
        //            var a2 = (from vp in _Data.VienPhis.Where(p => p.MaBNhan == _MaBN)
        //                      join vpct in _Data.VienPhicts on vp.idVPhi equals vpct.idVPhi
        //                      select new { vpct.MaDV, vpct.DonGia, vpct.SoLuong, vpct.ThanhTien, vpct.TrongBH, vpct.TienBH, vpct.TienBN }).ToList();
        //            if (a2.Count > 0)
        //            {
        //                double st1 = 0, st2 = 0;
        //            }
        //            rep.CreateDocument();

        //            //rep.DataMember = "Table";
        //            frm.prcIN.PrintingSystem = rep.PrintingSystem;
        //            frm.ShowDialog();
        //            return true;
        //        }
        //    }
        //    return false;

        //}
        #endregion
        #region InBangKe Bình giang
        private void inBK_BG(int _MaBN)
        {

            QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(Bien.StrCon);
            var q = _Data.TamUngs.Where(p => p.MaBNhan == _MaBN && (p.PhanLoai == 1 || p.PhanLoai == 2)).ToList();
            if (q.Count > 0)
            {
                BaoCao.Rep_PhieuThuBG rep = new BaoCao.Rep_PhieuThuBG();
                frmIn frm = new frmIn();
                rep.QuyenSo.Value = "Quyển số:" + q.First().QuyenHD;
                rep.SoCT.Value = "Số CT:" + q.First().SoHD;
                var a = _Data.BenhNhans.Where(p => p.MaBNhan == _MaBN).ToList();
                if (a.Count > 0)
                {
                    rep.TenBN.Value = a.First().TenBNhan;
                    rep.MaBN.Value = a.First().MaBNhan;
                    rep.NamSinh.Value = a.First().NamSinh;
                    rep.Diachi.Value = a.First().DChi;
                    rep.Sothe.Value = a.First().SThe;
                    int _MKP = q.First().MaKP == null ? 0 : q.First().MaKP.Value;
                    var a1 = _Data.KPhongs.Where(p => p.MaKP == _MKP).ToList();
                    if (a1.Count > 0)
                    {
                        rep.Khoa.Value = a1.First().TenKP;
                    }
                    rep.Sotien.Value = q.First().TienChenh.ToString("##,###");
                    if (q.First().PhanLoai == 1)
                    {
                        rep.Tralaihoacnopthem.Value = "Số tiền bệnh nhân phải nộp thêm:";
                    }
                    else
                    {
                        rep.Tralaihoacnopthem.Value = "Số tiền bệnh nhân được trả lại:";
                    }
                    double st = q.First().TienChenh;
                    rep.Bangchu.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng/.");
                    DateTime ngay = Convert.ToDateTime(q.First().NgayThu);
                    rep.ngaythangnam.Value = DungChung.Ham.NgaySangChu(ngay);
                    var a2 = (from vp in _Data.VienPhis.Where(p => p.MaBNhan == _MaBN)
                              join vpct in _Data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                              select new { vpct.MaDV, vpct.DonGia, vpct.SoLuong, vpct.ThanhTien, vpct.TrongBH, vpct.TienBH, vpct.TienBN }).ToList();
                    if (a2.Count > 0)
                    {
                        double st1 = 0, st2 = 0, st3 = 0;
                        st1 = a2.Where(p => p.TrongBH == 1).Sum(p => p.TienBN);
                        rep.BHYT.Value = st1.ToString("##,###");
                        st2 = a2.Where(p => p.TrongBH == 0).Sum(p => p.TienBN);
                        rep.NgoaiDM.Value = st2.ToString("##,###");
                        rep.STPhaiNop.Value = (st1 + st2).ToString("##,###");


                    }
                    rep.CreateDocument();
                    //rep.DataMember = "Table";
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
            }
        }
        #endregion

        private void btnInBK_30002_Click(object sender, EventArgs e)
        {
            int ot;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out ot))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            inBK_BG(_int_maBN);
        }

        private void txtSoTienCP_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSoTienCP.Text))
            {
                double stien = double.Parse(txtSoTienCP.Text);
                txtBangChuCP.Text = DungChung.Ham.DocTienBangChu(stien, " đồng.");
            }
        }

        private void cboPLThu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPLThu.SelectedIndex == 0 || cboPLThu.SelectedIndex == 3)
            {
                btnThuBangThe.Enabled = true;
            }
            else
                btnThuBangThe.Enabled = false;
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            if (_thutrucTiep == false && (TTLuutthu == 1 || TTLuutthu == 2))
            {
                if (cboPLThu.SelectedIndex == 4)
                {
                    var tienTT = _dataContext.TamUngs.Where(p => p.MaBNhan == _int_maBN && p.PhanLoai == 4).ToList();
                    if (tienTT.Count > 0)
                    {
                        MessageBox.Show("Bệnh nhân đã có chứng từ chi tạm thu, bạn không thể chọn");
                        cboPLThu.SelectedIndex = 0;
                    }
                    else
                    {
                        var tienU = _dataContext.TamUngs.Where(p => p.MaBNhan == _int_maBN && p.PhanLoai == 0).Sum(p => p.SoTien);
                        if (tienU != null)
                        {
                            txtSoTien.Text = tienU.Value.ToString();
                            txtNoiDung.Text = "Chi trả tiền tạm thu";
                        }
                        else
                        {
                            txtSoTien.Text = "";
                        }
                    }
                }
                else
                {
                    txtSoTien.Text = "";
                    txtNoiDung.Text = "";
                }
                if (cboPLThu.SelectedIndex == 1 || cboPLThu.SelectedIndex == 2 || cboPLThu.SelectedIndex == 3)
                {
                    MessageBox.Show("Bạn chỉ được phép chọn 'Tạm thu' hoặc 'Chi tạm thu'");
                    cboPLThu.SelectedIndex = 0;
                }
            }


        }
        bool _trangthai = true;
        private void cbo_quyenTU_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (TTLuutthu == 1)
            //{
            //    try
            //    {
            //        if (Bien.MaBV != "30004")
            //        {
            //            var So = _dataContext.TamUngs.Where(p => p.QuyenHD == cbo_quyenTU.Text).Select(p => p.SoHD.Max()).ToList();
            //            if (So.Count > 0)
            //            {
            //                Int64 _so = Convert.ToInt64(So.First().ToString());
            //                cboSoHD.Text = (_so + 1).ToString();

            //            }
            //        }
            //        else
            //        {
            //            cboSoHD.Text = "";
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        cboSoHD.Text = "";
            //    }
            //}
        }

        private void cboDTuong_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }
        public void TImKiem2()
        {
            string _tk = "";
            int ot;
            int _int_maBN = 0;
            if (!string.IsNullOrEmpty(txtTimKiem.Text) && txtTimKiem.Text != "Nhập tên|Mã số|Số thẻ BN")
            {
                _tk = txtTimKiem.Text.ToLower();

                if (Int32.TryParse(txtTimKiem.Text, out ot))
                    _int_maBN = Convert.ToInt32(txtTimKiem.Text);
            }
            grcBNhantt.DataSource = null;
            grcBNhantt.DataSource = _lTKbn.Where(p => (p.SThe == null || p.SThe.ToLower().Contains(_tk)) || p.TenBNhan.ToLower().Contains(_tk) || p.MaBNhan == _int_maBN).ToList();

        }
        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            //TImKiem2();

        }


        bool tudongduyet = false;
        /// <summary>
        /// nếu tiền bệnh nhân = 0 => cho có quyền duyệt
        /// </summary>
        double tienbn = -1;

        private void chkbtn_DuyetBK_Click(object sender, EventArgs e)
        {
            bool _duyet = true;
            int _int_maBN = 0; int ot;
            if (Int32.TryParse(txtMaBNhan.Text, out ot))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            if (_int_maBN > 0)
            {
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(Bien.StrCon);
                double thanhtien = 0;
                var q = (from vp1 in _dataContext.VienPhis.Where(p => p.MaBNhan == _mabn)
                         join vpct in _dataContext.VienPhicts on vp1.idVPhi equals vpct.idVPhi
                         group new { vp1, vpct } by vp1.idVPhi into kq
                         select new { kq.Key, ThanhTien = kq.Sum(p => p.vpct.TienBN) }).FirstOrDefault();
                if (q != null)
                {
                    thanhtien = q.ThanhTien;
                }
                var vp = data.VienPhis.Where(p => p.MaBNhan == _int_maBN).FirstOrDefault();
                var tu = _dataContext.TamUngs.Where(p => p.PhanLoai == 1 || p.PhanLoai == 2).Where(p => p.MaBNhan == _mabn).FirstOrDefault();

                if (vp != null)
                {

                    if (vp.Status == 1)
                    {
                        if (Bien.MaBV == "30002")
                            _duyet = DungChung.Ham.checkQuyenFalse("xtraDuyetCP")[2];
                        if (_duyet)
                        {
                            DialogResult result;
                            result = MessageBox.Show("Hủy duyệt chi phí  bệnh nhân: " + txtTenBenhNhan.Text + " ?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                if (tu != null && thanhtien > 0)
                                {
                                    MessageBox.Show("Bệnh nhân đã duyệt TT, bạn không thể hủy duyệt chi phí");
                                    return;
                                }
                                vp.NgayDuyetCP = null;
                                vp.Status = 0;
                                data.SaveChanges();
                                if (Bien.MaBV != "30002")
                                {
                                    if (thanhtien == 0)
                                    {
                                        tudongduyet = true;
                                        btnHuyDuyet_Click(sender, e);
                                        tudongduyet = false;
                                    }
                                }
                            }

                        }
                        else
                        {
                            MessageBox.Show("Tài khoản chưa được cấp quyền");
                        }
                    }
                    else
                    {
                        if (Bien.MaBV == "30002")
                            _duyet = DungChung.Ham.checkQuyenFalse("xtraDuyetCP")[0];
                        if (_duyet)
                        {
                            if (dtNgayDuyetCP.DateTime < vp.NgayTT)
                            {
                                MessageBox.Show("Thời gian duyệt phải sau thời gian thanh toán ");
                            }
                            else
                            {
                                DialogResult result;
                                result = MessageBox.Show("Duyệt chi phí bệnh nhân: " + txtTenBenhNhan.Text + "'", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (result == DialogResult.Yes)
                                {
                                    vp.NgayDuyetCP = dtNgayDuyetCP.DateTime;
                                    vp.Status = 1;
                                    data.SaveChanges();
                                    _kieu = 0;
                                    btnXem_Click(sender, e);
                                    int noingoai = CheckNoiNgoaiTru(_int_maBN);
                                    var ktrgui = _dataContext.VienPhis.Where(p => p.MaBNhan == _int_maBN).ToList();
                                    if (ktrgui.Count > 0 && ktrgui.First().ExportBHXH == false)//nếu đã gửi thì ko tự động gửi lại nữa
                                    {
                                        if ((noingoai == 0 && (Bien.PPXuat_BHXH[0] == 6 || Bien.PPXuat_BHXH[0] == 7)) || (noingoai == 1 && (Bien.PPXuat_BHXH[1] == 6 || Bien.PPXuat_BHXH[1] == 7)))
                                        {
                                            List<DungChung.Cls79_80.cl_79_80> _listVPBH = new List<DungChung.Cls79_80.cl_79_80>();
                                            _listVPBH.Add(new DungChung.Cls79_80.cl_79_80 { Ma_bn = _int_maBN, Export = false });
                                            BHYT.us_Export_XML_2348.user = Bien.xmlFilePath_LIS[10];
                                            BHYT.us_Export_XML_2348.user = Bien.xmlFilePath_LIS[11];
                                            BHYT.us_Export_XML_2348 clsBHXH = new BHYT.us_Export_XML_2348();
                                            clsBHXH._updateExPort(_dataContext, _listVPBH, _int_maBN, false, 1);
                                        }
                                    }
                                    if (thanhtien == 0)
                                    {
                                        tienbn = 0;
                                        tudongduyet = true;
                                        btnDuyet_Click(sender, e);
                                        tudongduyet = false;
                                    }


                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Tài khoản chưa được cấp quyền ");
                        }
                    }
                    grvBNhantt_FocusedRowChanged(null, null);

                }
                else
                {
                    MessageBox.Show("bệnh nhân chưa thanh toán!");
                }
            }

        }

        private void grvTraTamUng_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            switch (e.Column.Name)
            {
                case "colMaDVTra":

                    if (grvTraTamUng.GetFocusedRowCellValue(colMaDVTra) != null)
                    {
                        int maDV = 0;

                        maDV = Convert.ToInt32(grvTraTamUng.GetFocusedRowCellValue(colMaDVTra));
                        var q = _lThuThangChuaTH.Where(p => p.MaDV == maDV).FirstOrDefault();
                        if (q != null)
                        {
                            grvTraTamUng.SetFocusedRowCellValue(colDonGiaTra, q.DonGia);
                            grvTraTamUng.SetFocusedRowCellValue(colSoLuongTra, q.SoLuong);
                        }
                        else
                        {
                            MessageBox.Show("Dịch vụ được trả lại phải là dịch vụ đã thu tiền và chưa thực hiện");
                            grvTraTamUng.DeleteRow(grvTraTamUng.FocusedRowHandle);
                        }
                    }
                    break;
                case "colSoLuongTra":

                    if (grvTraTamUng.GetFocusedRowCellValue(colMaDVTra) != null && grvTraTamUng.GetFocusedRowCellValue(colSoLuongTra) != null && grvTraTamUng.GetFocusedRowCellValue(colDonGiaTra) != null)
                    {
                        double dongia = 0; double soluong = 0;
                        int maDV = 0;
                        maDV = Convert.ToInt32(grvTraTamUng.GetFocusedRowCellValue(colMaDVTra));
                        dongia = Convert.ToDouble(grvTraTamUng.GetFocusedRowCellValue(colDonGiaTra));
                        int fc = grvTraTamUng.FocusedRowHandle;
                        for (int i = 0; i <= grvTraTamUng.RowCount; i++)
                        {
                            if (grvTraTamUng.GetRowCellValue(i, colMaDVTra) != null)
                            {
                                int madv = Convert.ToInt32(grvTraTamUng.GetRowCellValue(i, colMaDVTra));
                                if (maDV == madv)
                                {
                                    MessageBox.Show("Dịch vụ đã có bạn không thể thêm");
                                    grvTraTamUng.DeleteRow(grvTraTamUng.FocusedRowHandle);
                                    break;
                                }
                            }
                        }
                        soluong = soluong + Convert.ToDouble(grvTraTamUng.GetFocusedRowCellValue(colSoLuongTra));
                        grvTraTamUng.SetFocusedRowCellValue(colThanhTienTra, dongia * soluong);
                        var qsl = _lThuThangChuaTH.Where(p => p.MaDV == maDV).FirstOrDefault();
                        if (qsl != null)
                        {
                            if (soluong > qsl.SoLuong)
                            {
                                MessageBox.Show("Số lượng trả lại không được lớn hơn số lượng thanh toán");
                                grvTraTamUng.SetFocusedRowCellValue(colSoLuongTra, 0);
                            }
                        }

                    }
                    break;
            }
        }

        List<MyObject> _lDSBNGui = new List<MyObject>();

        /// <summary>
        /// Set thời gian delay gửi dữ liệu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            int minute = 5;
            if (Bien.MaBV == "27001")
            {
                minute = 10;
            }
            else if (Bien.MaBV == "30003")
                minute = 60;
            List<MyObject> _lds = new List<MyObject>();


            if (Bien.MaBV == "20001")
            {
                DateTime dtkt = DateTime.Now.AddMinutes(-minute);
                DateTime ngaytu = DungChung.Ham.NgayTu(DateTime.Now);
                bool xuatnoitru = false;
                bool xuatngoaitru = false;
                if (Bien.PPXuat_BHXH[0] == 4 || Bien.PPXuat_BHXH[0] == 5)
                    xuatngoaitru = true;
                if (Bien.PPXuat_BHXH[1] == 4 || Bien.PPXuat_BHXH[1] == 5)
                    xuatnoitru = true;
                var _lds0 = (from vp in _dataContext.VienPhis.Where(p => p.NgayTT < dtkt && p.NgayTT >= ngaytu && p.ExportBHXH == false)
                             join bn in _dataContext.BenhNhans.Where(p => p.DTuong == "BHYT") on vp.MaBNhan equals bn.MaBNhan
                             select new { MaBNhan = vp.MaBNhan.Value, NgayTT = vp.NgayTT.Value, bn.NoiTru }).ToList();
                _lds = (from vp in _lds0.Where(p => (p.NoiTru == 0 && xuatngoaitru) || (p.NoiTru == 1 && xuatnoitru) || ktraNgoaiGo(p.MaBNhan, _dataContext))
                        select new MyObject { MaBNhan = vp.MaBNhan, NgayTT = vp.NgayTT }).ToList();
                //_lds = (from vp in _dataContext.VienPhis.Where(p => p.NgayTT < dtkt && p.NgayTT >= ngaytu && p.ExportBHXH == false)
                //        join bn in _dataContext.BenhNhans.Where(p => p.DTuong == "BHYT" && ((p.NoiTru == 0 && xuatngoaitru) || (p.NoiTru == 1 && xuatnoitru) || ktraNgoaiGo(p.MaBNhan, _dataContext))) on vp.MaBNhan equals bn.MaBNhan
                //        select new MyObject { MaBNhan = vp.MaBNhan.Value, NgayTT = vp.NgayTT.Value }).ToList();
            }
            else
                _lds = _lDSBNGui.Where(p => p.NgayTT.AddMinutes(minute) < DateTime.Now).ToList();

            foreach (var obj in _lds)
            {
                int noingoai = CheckNoiNgoaiTru(obj.MaBNhan);
                bool ktraNgoaigio = ktraNgoaiGo(obj.MaBNhan, _dataContext);
                if ((noingoai == 0 && (Bien.PPXuat_BHXH[0] == 4 || Bien.PPXuat_BHXH[0] == 5)) || (noingoai == 1 && (Bien.PPXuat_BHXH[1] == 4 || Bien.PPXuat_BHXH[1] == 5)) || ktraNgoaigio)
                {
                    List<DungChung.Cls79_80.cl_79_80> _listVPBH = new List<DungChung.Cls79_80.cl_79_80>();
                    _listVPBH.Add(new DungChung.Cls79_80.cl_79_80 { Ma_bn = obj.MaBNhan, Export = false });
                    BHYT.us_Export_XML_2348.user = Bien.xmlFilePath_LIS[10];
                    BHYT.us_Export_XML_2348.user = Bien.xmlFilePath_LIS[11];
                    BHYT.us_Export_XML_2348 clsBHXH = new BHYT.us_Export_XML_2348();
                    clsBHXH._updateExPort(_dataContext, _listVPBH, obj.MaBNhan, false, 1);
                    if (Bien.MaBV != "20001")
                        _lDSBNGui.Remove(obj);
                }
            }
            GC.Collect();
            // updateExPort = false;

        }
        public bool ktraNgoaiGo(int maBN, QLBV_Database.QLBVEntities data)
        {
            if (Bien.MaBV == "30002" || Bien.MaBV == "30009" || Bien.MaBV == "20001" || Bien.MaBV == "30303")
            {
                var qvp = data.VienPhis.Where(p => p.MaBNhan == maBN).FirstOrDefault();
                if (qvp != null && qvp.NgoaiGio == 1)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        public class MyObject
        {
            public int MaBNhan { set; get; }
            public DateTime NgayTT { set; get; }
        }

        private void chkbtn_DuyetBK_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtchitamthu_EditValueChanged(object sender, EventArgs e)
        {
            double stien = 0;
            if (!string.IsNullOrEmpty(txtchitamthu.Text))
            {
                stien = double.Parse(txtchitamthu.Text);

            }
            txtchitamthuchu.Text = DungChung.Ham.DocTienBangChu(stien, " đồng.");
        }

        private void grvThanhToan_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {


        }

        private void xtraTToan_TThu_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (ckcDonNgoai.Checked == true)
                TimKiemDonThuoc();
            else
                TimKiem();
        }

        private void grvTamUngCt_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            int rs;
            int _int_maBN = 0, _IDTUct = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            if (e.Column.Name == "Col_Xoa")
            {
                if (Bien.MaBV == "12345" || Bien.MaBV == "24297")
                {
                    int IDTamung = Convert.ToInt32(txtIdTamUng.Text);
                    // Kiểm tra phiếu thu đã được duyệt chưa
                    var pt = _dataContext.TamUngs.Where(p => p.IDTamUng == IDTamung).First().DuyetPhieuThu ?? false;
                    if (pt)
                    {
                        XtraMessageBox.Show("Phiếu thu đã được duyệt bạn không thể xóa phiếu thu.\n Yêu cầu hủy duyệt phiếu thu trước khi xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                var tk = _dataContext.ADMINs.FirstOrDefault(o => o.TenDN == Bien.TenDN);

                if (Bien.MaBV == "56789" && tk != null && tk.CapDo != 9)
                {
                    MessageBox.Show("Tài khoản không có quyền xóa TƯ");
                    return;
                }
                if (grvTamUngCt.GetFocusedRowCellValue(col_IDTUct) != null)
                {
                    _IDTUct = Convert.ToInt32(grvTamUngCt.GetFocusedRowCellValue(col_IDTUct));
                    var tamung = (from tu in _dataContext.TamUngs
                                  join tuct in _dataContext.TamUngcts.Where(o => o.IDTamUngct == _IDTUct) on tu.IDTamUng equals tuct.IDTamUng
                                  join dv in _dataContext.DichVus on tuct.MaDV equals dv.MaDV
                                  select new { tu, dv.IDNhom }).ToList();
                    if (tamung.Count > 0 && !string.IsNullOrWhiteSpace(tamung.First().tu.transactionID))
                    {
                        MessageBox.Show("Đã tạo hóa đơn không thể xóa", "Thông báo");
                        return;
                    }
                    if (tamung.Count > 0 && tamung.First().IDNhom == 13)
                    {
                        MessageBox.Show("Không thể xóa tiền công khám", "Thông báo");
                        return;
                    }
                    if (Bien.MaBV == "30372" || Bien.MaBV == "12345" || Bien.MaBV == "24297")
                    {
                        var xuatDuoc = (from tuct in _dataContext.TamUngcts.Where(o => o.IDTamUngct == _IDTUct)
                                        join dtct in _dataContext.DThuoccts on tuct.IDDonct equals dtct.IDDonct
                                        select dtct).FirstOrDefault();
                        if (xuatDuoc != null && xuatDuoc.Status == 1)
                        {
                            MessageBox.Show("Đã xuất dược không thể xóa", "Thông báo");
                            return;
                        }
                    }
                    var qtt = _dataContext.VienPhis.Where(p => p.MaBNhan == _int_maBN).FirstOrDefault();
                    if (qtt != null)
                        MessageBox.Show("Bệnh nhân đã thanh toán, \nkhông thể xóa tạm thu", "Thông báo", MessageBoxButtons.OK);
                    else
                    {
                        if (DungChung.Ham._CheckThucHienDVDaTamThu(_dataContext, _int_maBN, _IDTUct))
                        {
                            DialogResult _result = MessageBox.Show("Xóa tạm thu dịch vụ:" + grvTamUngCt.GetFocusedRowCellDisplayText(madv_tu), "xóa chi tiết!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (_result == DialogResult.Yes)
                            {
                                if (Bien.MaBV == "01071" || Bien.MaBV == "01049" || Bien.MaBV == "12345" || Bien.MaBV == "24297" || Bien.MaBV == "56789") //những bn dịch chỉ định CLS ở phòng tiếp đón, khi thu trực tiếp tính là đã thanh toán đức 04-10
                                {
                                    var BNKB = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).ToList();
                                    if (BNKB.Count == 0)
                                    {
                                        var TTBN = _dataContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN).FirstOrDefault();
                                        if (TTBN != null && TTBN.DTuong == "Dịch vụ")
                                        {
                                            TTBN.Status = 0;
                                            _dataContext.SaveChanges();
                                        }
                                    }
                                }
                                DungChung.Ham.XoaTamUngct(_dataContext, _int_maBN, _IDTUct);
                                grvTamUng_FocusedRowChanged(null, null);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Dịch vụ đã thực hiện, \nkhông thể xóa tạm thu", "Thông báo", MessageBoxButtons.OK);
                        }
                    }
                }
            }
        }

        private void grcTamUngct_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _idtthu = 0;
            if (!string.IsNullOrEmpty(txtIdTamUng.Text))
                _idtthu = int.Parse(txtIdTamUng.Text);
            switch (comboBoxEdit1.SelectedIndex)
            {
                case 0:
                    _InPhieuThu_01071(_idtthu, 3, true);
                    break;
                case 1:
                    InPhieuTC_TT107(_idtthu, 1);
                    break;
            }
            comboBoxEdit1.SelectedIndex = -1;
        }

        private void btnXacnhanCK_Click(object sender, EventArgs e)
        {
            if (ckcthanhtoan.Checked)
            {
                int rs;
                int _int_maBN = 0;
                if (Int32.TryParse(txtMaBNhan.Text, out rs))
                    _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
                ChucNang.frm_tranfer frm = new ChucNang.frm_tranfer(_int_maBN);
                frm.ShowDialog();
            }
            else
                MessageBox.Show("Xác nhận chuyển chuyển khoản chỉ dành cho bệnh nhân \n có hình thức thanh toán chuyển khoản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void TimKiemDonThuoc()
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(Bien.StrCon);
            List<TamUng> _lTamUng = _data.TamUngs.Where(p => p.IDNhapD != null).ToList();
            DateTime _ngaytu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
            DateTime _ngayden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
            string TenNgmua = "";
            if (!string.IsNullOrEmpty(txtTimKiem.Text) && txtTimKiem.Text != "Nhập tên|Mã số|Số thẻ BN")
            {
                TenNgmua = txtTimKiem.Text.ToLower();
            }
            int _makho = 0;
            int TTThanhToan = cboTimTT.SelectedIndex;
            if (lupTimMaKP.EditValue != null)
                _makho = Convert.ToInt32(lupTimMaKP.EditValue);
            var _lNhapDuoc = (from nd in _data.NhapDs.Where(p => p.KieuDon == 9 && p.PLoai == 2).Where(p => p.NgayNhap >= _ngaytu && p.NgayNhap <= _ngayden).Where(p => TenNgmua != "" ? p.TenNguoiCC.ToLower().Contains(TenNgmua) : true).Where(p => _makho != 0 ? p.MaKP == _makho : true)
                              select new
                              {
                                  TenBNhan = nd.TenNguoiCC,
                                  DChi = nd.DiaChi,
                                  IDNhap = nd.IDNhap,
                                  MaKP = nd.MaKP,
                                  NNhap = nd.NgayNhap
                              }).ToList();
            if (TTThanhToan == 1)
            {
                _lNhapDuoc = (from nd in _lNhapDuoc
                              join tu in _lTamUng on nd.IDNhap equals tu.IDNhapD
                              select nd).ToList();
            }
            if (TTThanhToan == 0)
            {
                _lNhapDuoc = (from nd in _lNhapDuoc
                              join tu in _lTamUng on nd.IDNhap equals tu.IDNhapD into k1
                              from k in k1.DefaultIfEmpty()
                              where (k == null)
                              select nd).ToList();
            }
            grcBNhantt.DataSource = null;
            grcBNhantt.DataSource = _lNhapDuoc.OrderBy(p => p.IDNhap).ThenBy(p => p.TenBNhan).ToList();
        }
        public static BaoCao.rep_PhieuThuChi_TT107_A5 _NhieuLienChi(int idTU, int _mauin, string lien)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(Bien.StrCon);

            var pt = (from a in data.TamUngs.Where(p => p.IDTamUng == idTU)
                      join b in data.BenhNhans on a.MaBNhan equals b.MaBNhan
                      join c in data.CanBoes on a.MaCB equals c.MaCB into k
                      from k1 in k.DefaultIfEmpty()
                      select new { a.MaBNhan, a.NgayThu, a.PhanLoai, b.TenBNhan, b.DChi, a.SoTien, a.LyDo, a.TienChenh, a.QuyenHD, a.SoHD, TenCB = k1 != null ? k1.TenCB : "" }).FirstOrDefault();
            BaoCao.rep_PhieuThuChi_TT107_A5 rep = new BaoCao.rep_PhieuThuChi_TT107_A5();
            if (pt != null)
            {
                rep.TieuDe.Value = "PHIẾU CHI";
                rep.Lien.Value = lien;
                rep.MaBNhan.Value = "Mã BNhan:  " + pt.MaBNhan.ToString();
                rep.xrTableCell11.Text = "Họ và tên người nhận tiền: ";
                rep.NguoiNop.Value = "THỦ QUỸ";
                rep.NguoiNhan.Value = "NGƯỜI NHẬN TIỀN";
                rep.nguoithu1.NullValueText = pt.TenBNhan;
                rep.xrTableCell2.Text = "Mẫu số: C41-BB";
                rep.So.Value = "Số: " + pt.SoHD;
                rep.QuyenSo.Value = "Quyển số: " + pt.QuyenHD;
                rep.No.Value = "Nợ:";
                rep.Co.Value = "Có:";
                rep.SubBand1.Visible = false;
                rep.SubBand2.Visible = true;
                rep.MaBNhan.Value = pt.MaBNhan;
                rep.clMaBNhan.Text = "Mã BN: " + pt.MaBNhan;
                rep.HoTen.Value = pt.TenBNhan.ToUpper();
                rep.DChi.Value = pt.DChi;
                rep.NoiDung.Value = pt.LyDo;
                string[] ar = Bien.FormatString[1].Split(';');
                rep.SoTien.Value = (pt.PhanLoai == 3 ? (pt.SoTien ?? 0.0).ToString(ar[0].Substring(3)) : (_mauin == 0 ? pt.TienChenh.ToString(ar[0].Substring(3)) : (pt.SoTien ?? 0.0).ToString(ar[0].Substring(3)))) + " VNĐ";
                rep.SoThanhChu.Value = DungChung.Ham.DocTienBangChu((pt.PhanLoai == 3 ? pt.SoTien : (_mauin == 0 ? pt.TienChenh : pt.SoTien)) ?? 0.0, "") + (Bien.MaBV == "30010" ? " đồng" : "");
                rep.NguoiLap.Value = pt.TenCB;
                rep.NgayThang.Value = "Ngày " + pt.NgayThu.Value.Day + " tháng " + pt.NgayThu.Value.Month + " năm " + pt.NgayThu.Value.Year;
                rep.CreateDocument();
                return rep;
            }
            return rep;
        }

        public void PhieuThuChi_C40_20001(int idtu)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(Bien.StrCon);

            var listBN = (from a in data.TamUngs.Where(p => p.IDTamUng == idtu)
                          join b in data.BenhNhans on a.MaBNhan equals b.MaBNhan
                          join c in data.CanBoes on a.MaCB equals c.MaCB into k
                          from k1 in k.DefaultIfEmpty()
                          select new { a.MaBNhan, a.NgayThu, a.PhanLoai, b.TenBNhan, b.NgaySinh, b.ThangSinh, b.NamSinh, b.DChi, a.SoTien, a.LyDo, a.TienChenh, a.QuyenHD, a.SoHD, TenCB = k1 != null ? k1.TenCB : "" }).ToList();
            if (listBN.Count() > 0)
            {
                Dictionary<string, object> _dic = new Dictionary<string, object>();
                _dic.Add("SoTienBangChu", DungChung.Ham.DocTienBangChu((double)listBN.FirstOrDefault().SoTien, " đồng."));
                _dic.Add("Ngay", DungChung.Ham.NgaySangChu((DateTime)listBN.FirstOrDefault().NgayThu, 1));
                _dic.Add("DiaChi", Bien.DiaChi ?? "");
                _dic.Add("ThuTruongDonVi", Bien.GiamDoc ?? "");
                _dic.Add("KeToanTruong", Bien.KeToanTruong ?? "");
                _dic.Add("NguoiLap", listBN.FirstOrDefault().TenCB ?? "");
                double SoTien = (double)listBN.FirstOrDefault().SoTien;
                _dic.Add("SoTien", listBN.FirstOrDefault().SoTien == null ? "0 (VNĐ)" : SoTien.ToString("#,##0.###") + " (VNĐ)");
                _dic.Add("SoTien2", listBN.FirstOrDefault().SoTien == null ? "0 (VNĐ)" : "- Bằng số: " + Convert.ToDouble(listBN.FirstOrDefault().SoTien).ToString("#,##0.###") + " (VNĐ)");
                _dic.Add("SoTienBangChu2", "- Bằng chữ: " + DungChung.Ham.DocTienBangChu((double)listBN.FirstOrDefault().SoTien, " đồng."));
                _dic.Add("SoHD", "Số: " + listBN.FirstOrDefault().SoHD);
                DungChung.Ham.Print(DungChung.PrintConfig.Rep_PhieuThu_C40_20001, listBN, _dic, false);
            }



        }
        public static BaoCao.rep_PhieuThuChi_TT107_A5 _NhieuLien(int idTU, int _mauin, string lien)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(Bien.StrCon);

            var pt = (from a in data.TamUngs.Where(p => p.IDTamUng == idTU)
                      join b in data.BenhNhans on a.MaBNhan equals b.MaBNhan
                      join c in data.CanBoes on a.MaCB equals c.MaCB into k
                      from k1 in k.DefaultIfEmpty()
                      select new { a.MaBNhan, a.NgayThu, a.PhanLoai, b.TenBNhan, b.NgaySinh, b.ThangSinh, b.NamSinh, b.DChi, a.SoTien, a.LyDo, a.TienChenh, a.QuyenHD, a.SoHD, TenCB = k1 != null ? k1.TenCB : "" }).FirstOrDefault();
            BaoCao.rep_PhieuThuChi_TT107_A5 rep = new BaoCao.rep_PhieuThuChi_TT107_A5();

            if (pt != null)
            {
                rep.MaBNhan.Value = pt.MaBNhan;
                rep.Lien.Value = lien;
                rep.TieuDe.Value = "PHIẾU THU";
                rep.xrTableCell11.Text = "Họ và tên người nộp tiền: ";
                rep.NguoiNop.Value = "NGƯỜI NỘP";
                rep.NguoiNhan.Value = "THỦ QUỸ";
                rep.xrTableCell2.Text = "Mẫu số: C40-BB";
                if (Bien.MaBV == "24272")
                {
                    rep.Parameters["so1"].Value = "Số: " + pt.SoHD;
                }
                else
                {
                    rep.So.Value = "Số: " + pt.SoHD;
                }
                rep.QuyenSo.Value = "Quyển số: " + pt.QuyenHD;
                rep.No.Value = "Nợ:";
                rep.Co.Value = "Có:";
                rep.SubBand1.Visible = true;
                rep.SubBand2.Visible = false;
                rep.MaBNhan.Value = pt.MaBNhan;
                rep.clMaBNhan.Text = "Mã BN: " + pt.MaBNhan;
                rep.HoTen.Value = pt.TenBNhan.ToUpper() + "       (SN: " + pt.NgaySinh + "/" + pt.ThangSinh + "/" + pt.NamSinh + ")";
                rep.DChi.Value = pt.DChi;
                rep.NoiDung.Value = pt.LyDo;
                string _tendn = Bien.TenDN;
                string _nameDN = (from ad in data.ADMINs.Where(p => p.TenDN == _tendn)
                                  join cb in data.CanBoes on ad.MaCB equals cb.MaCB
                                  select new { cb.TenCB }).Select(p => p.TenCB).First().ToString();
                if (Bien.MaBV == "30372") rep.Tendn.Value = _nameDN;
                if (Bien.MaBV == "30005" || Bien.MaBV == "30010")
                {
                    rep.Tendn.Value = pt.TenCB.ToString();
                }
                string[] ar = Bien.FormatString[1].Split(';');
                if (pt.PhanLoai == 0)
                {
                    rep.SoTien.Value = (pt.SoTien ?? 0.0).ToString() + " VNĐ";
                    rep.SoThanhChu.Value = DungChung.Ham.DocTienBangChu(pt.SoTien ?? 0.0, " đồng");

                }
                else
                {
                    rep.SoTien.Value = (pt.PhanLoai == 3 ? (pt.SoTien ?? 0.0).ToString(ar[0].Substring(3)) : (_mauin == 0 ? pt.TienChenh.ToString(ar[0].Substring(3)) : (pt.SoTien ?? 0.0).ToString(ar[0].Substring(3)))) + " VNĐ";
                    rep.SoThanhChu.Value = DungChung.Ham.DocTienBangChu((pt.PhanLoai == 3 ? pt.SoTien : (_mauin == 0 ? pt.TienChenh : pt.SoTien)) ?? 0.0, "") + " đồng.";
                }
                if (Bien.MaBV == "30005")
                {
                    var tencb = data.CanBoes.Where(p => p.MaCB == Bien.MaCB).Select(p => p.TenCB).FirstOrDefault();
                    rep.nguoithu1.Text = tencb != null ? tencb : "";
                }
                rep.NguoiLap.Value = pt.TenCB;
                rep.NgayThang.Value = "Ngày " + pt.NgayThu.Value.Day + " tháng " + pt.NgayThu.Value.Month + " năm " + pt.NgayThu.Value.Year;
                rep.CreateDocument();
                return rep;
            }
            return rep;
        }
        public static BaoCao.repTamUngVienPhi_30303 in2lienTamUng_30303(QLBV_Database.QLBVEntities data, int _Mabn, int ID, string lien)
        {
            frmIn frm = new frmIn();
            BaoCao.repTamUngVienPhi_30303 rep = new BaoCao.repTamUngVienPhi_30303();
            int _idtthu = ID;
            string _tronggio = "";
            var q = (from bn in data.BenhNhans
                     join tt in data.TamUngs on bn.MaBNhan equals tt.MaBNhan
                     join cb in data.CanBoes on tt.MaCB equals cb.MaCB
                     join kp in data.KPhongs on tt.MaKP equals kp.MaKP
                     where (tt.IDTamUng == _idtthu)
                     select new { tt.SoHD, bn.MaBNhan, tt.QuyenHD, bn.SThe, tt.PhanLoai, bn.SoTT, bn.TenBNhan, bn.DTuong, bn.NamSinh, bn.NoiTru, kp.TenKP, bn.DChi, tt.SoTien, cb.TenCB, tt.LyDo, tt.NgayThu, tt.IDTamUng, tt.NgoaiGio }).ToList();
            if (q.Count > 0)
            {

                string tenloaiphi = "";
                rep.TieuDe.Value = "PHIẾU TẠM THU TIỀN";
                tenloaiphi = "Tạm ứng khám bệnh";

                if (q.First().DTuong == "BHYT" && Bien.MaBV == "30009")
                    rep.SoThe.Value = "Số thẻ BHYT:" + q.First().SThe;
                rep.QuyenHD.Value = q.FirstOrDefault().QuyenHD;
                rep.TenBNhan.Value = q.First().TenBNhan.ToUpper();
                rep.NamSinh.Value = q.First().NamSinh;
                rep.DChi.Value = q.First().DChi;
                rep.SoPhieu.Value = Bien.MaBV == "24009" ? (q.First().IDTamUng == null ? "" : q.First().IDTamUng.ToString()) : q.First().SoHD;
                if (q.First().NgoaiGio != null)
                    if (q.First().NgoaiGio.Value == 1)
                    {
                        _tronggio = "Ngoài giờ";
                    }
                    else
                    {
                        _tronggio = "Trong giờ";
                    }
                string _tenloai = "";
                if (q.First().DTuong == "KSK")
                {
                    _tenloai = "Tên loại phí, lệ phí:Tạm ứng khám sức khỏe";
                }
                else
                {
                    if (q.First().DTuong == "BHYT")
                    {
                        _tenloai = "Tên loại phí, lệ phí: " + tenloaiphi + " - BHYT";
                    }
                    else
                    {
                        _tenloai = "Tên loại phí, lệ phí: " + tenloaiphi + " - Thu phí";
                    }
                    if (q.First().NoiTru.Value == 0)
                        _tenloai += " - Ngoại trú";
                    else
                        _tenloai += " - Nội trú";
                }
                if (q.First().NoiTru == 0)
                {
                    rep.KyHieu.Value = "PTNgT" + System.DateTime.Now.Year;
                }
                else
                {
                    rep.KyHieu.Value = "PTNT" + System.DateTime.Now.Year;
                }
                rep.TenLoai.Value = _tenloai;
                rep.SoTT.Value = "STT:" + q.First().SoTT;
                rep.TenKP.Value = q.First().TenKP; // gan sau
                rep.LyDoThu.Value = q.First().LyDo;
                double sotien = 0;
                if (q.First().SoTien != null)
                {
                    rep.SoTien.Value = q.First().SoTien;
                    sotien = Convert.ToDouble(q.First().SoTien);
                }
                rep.TenCB.Value = q.First().TenCB;
                if (q.First().NgayThu != null)
                {
                    if (Bien.MaBV == "01071" || Bien.MaBV == "01049" || Bien.MaBV == "12345" || Bien.MaBV == "24297" || Bien.MaBV == "56789")
                        rep.NgayThang.Value = DungChung.Ham.NgaySangChu(q.First().NgayThu.Value, 3);
                    else
                        rep.NgayThang.Value = DungChung.Ham.NgaySangChu(q.First().NgayThu.Value);

                }
                rep.DocTien.Value = DungChung.Ham.DocTienBangChu(sotien, " đồng.");
                rep.Lien.Value = lien;
            }
            rep.CreateDocument();
            return rep;
        }//in2lien in2lienTamUng_30303
        public static BaoCao.Rep_PhieuThuChi_2Lien_30003 in2lien(QLBV_Database.QLBVEntities data, int _Mabn, int ID, string lien)
        {
            BaoCao.Rep_PhieuThuChi_2Lien_30003 rep = new BaoCao.Rep_PhieuThuChi_2Lien_30003();
            rep.TENCQCQ.Value = Bien.TenCQCQ.ToUpper();
            rep.TENCQ.Value = Bien.TenCQ.ToUpper();
            int _hangbv = DungChung.Ham.hangBV(Bien.MaBV);
            string line1 = "", line2 = "", line3 = "", line4 = "", line5 = "", _muc = "", kvuc = "", muchuong = "", dtuong = "";
            int dungtuyen = -1;
            var TTBN = data.BenhNhans.Where(p => p.MaBNhan == _Mabn).ToList();
            if (TTBN.Count > 0)
            {
                rep.TENBN.Value = TTBN.First().TenBNhan.ToUpper() + " (SN: " + TTBN.First().NgaySinh + "/" + TTBN.First().ThangSinh + "/" + TTBN.First().NamSinh + ")";
                rep.MABN.Value = "Mã số BN: " + _Mabn.ToString();
                rep.DIACHI.Value = TTBN.First().DChi;
                int makp = TTBN.First().MaKP ?? 0;
                var tenkp = data.KPhongs.Where(p => p.MaKP == makp).Select(p => p.TenKP).FirstOrDefault();
                if (tenkp != null)
                    rep.KHOA.Value = tenkp.ToString();
                //_muc = TTBN.First().MucHuong.ToString();
                //kvuc = TTBN.First().KhuVuc;
                //dungtuyen = TTBN.First().Tuyen ?? 0;
                dtuong = TTBN.First().DTuong;
            }
            var _lTamung = data.TamUngs.Where(p => p.MaBNhan == _Mabn).ToList();
            var TTBL = _lTamung.Where(p => p.IDTamUng == ID).ToList();
            if (TTBL.Count > 0)
            {
                if (dtuong == "BHYT")
                    rep.LYDO.Value = _lTamung.First().LyDo;
                else
                    rep.LYDO.Value = TTBL.First().LyDo;
                rep.SOTIEN.Value = TTBL.First().TienChenh.ToString("###,###.00") + " đồng";
                rep.BANGCHU.Value = DungChung.Ham.DocTienBangChu(TTBL.First().TienChenh, " đồng");
                rep.SOHD.Value = "Số: " + TTBL.First().SoHD;
                rep.QUYENHD.Value = "Quyển số: " + TTBL.First().QuyenHD;
                if (TTBL.First().PhanLoai == 1 || TTBL.First().PhanLoai == 3)
                {
                    rep.TIEUDE.Value = "PHIẾU THU";

                    line5 = (TTBL.First().PhanLoai == 1 ? (dtuong == "BHYT" ? "-Bệnh nhân phải nộp: " : "-Thu viện phí: ") : "-Thu viện phí: ") + TTBL.First().TienChenh.ToString("###,###.00");
                    if (TTBL.First().PhanLoai == 1)
                    {
                        if (TTBN.Count > 0 && TTBN.First().NoiTru == 0)
                            line1 = "Ghi chú: (thanh toán ra viện ngoại trú)";
                        else
                        {
                            line1 = "Ghi chú: (thanh toán ra viện nội trú)";

                        }
                    }
                    else
                    {
                        line1 = "Ghi chú: (thu trực tiếp dịch vụ)";
                    }
                }
                else if (TTBL.First().PhanLoai == 2)
                {
                    rep.xrTableCell24.Text = "";
                    rep.xrTableCell27.Text = "";
                    rep.TIEUDE.Value = "PHIẾU CHI";
                    line5 = "-Bệnh nhân nhận lại: " + TTBL.First().TienChenh.ToString("###,###.00");
                    if (TTBN.Count > 0 && TTBN.First().NoiTru == 0)
                        line1 = "Ghi chú: (thanh toán ra viện ngoại trú)";
                    else
                        line1 = "Ghi chú: (thanh toán ra viện nội trú)";
                }

                rep.NGAYLAP.Value = "Ngày " + TTBL.First().NgayThu.Value.Day + " tháng " + TTBL.First().NgayThu.Value.Month + " năm " + TTBL.First().NgayThu.Value.Year;
            }
            var Tientamung = _lTamung.Where(p => p.PhanLoai == 0).ToList();
            var _lvienphi = (from vp in data.VienPhis.Where(p => p.MaBNhan == _Mabn)
                             join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                             select vpct).ToList();

            var cb = data.CanBoes.Where(p => p.MaCB == Bien.MaCB).ToList();
            if (cb.Count > 0)
            {
                rep.NGUOILAPPHIEU.Value = cb.First().TenCB;
            }
            if (_lvienphi.Count > 0)
            {
                double tongcp = _lvienphi.Sum(p => p.ThanhTien);
                //double tienbn = _lvienphi.Sum(p => p.TienBN);
                double tienbh = _lvienphi.Sum(p => p.TienBH);
                double tientamung = Tientamung.Sum(p => p.SoTien) ?? 0;
                if (tongcp > 0)
                    line2 = "-Tổng chi phí: " + tongcp.ToString("###,###.00");
                if (tientamung > 0)
                    line3 = "-Tạm ứng: " + tientamung.ToString("###,###.00");
                line4 = "-BHYT trả: " + tienbh.ToString("###,###.00");
            }
            rep.LIEN.Value = lien;
            rep.txtghichu.Text = line1 + (string.IsNullOrEmpty(line2) ? "" : ("\n" + line2)) + (string.IsNullOrEmpty(line3) ? "" : ("\n" + line3)) + (string.IsNullOrEmpty(line4) ? "" : ("\n" + line4)) + (string.IsNullOrEmpty(line5) ? "" : ("\n" + line5));
            rep.CreateDocument();
            return rep;
        }
        public static void InPhieuTC_TT107(int idTU, int mau)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(Bien.StrCon);

            var pt = (from a in data.TamUngs.Where(p => p.IDTamUng == idTU)
                      join b in data.BenhNhans on a.MaBNhan equals b.MaBNhan
                      join c in data.CanBoes on a.MaCB equals c.MaCB into k
                      from k1 in k.DefaultIfEmpty()
                      select new { a.MaBNhan, a.NgayThu, a.PhanLoai, b.TenBNhan, b.NgaySinh, b.ThangSinh, b.NamSinh, b.DChi, a.SoTien, a.LyDo, a.TienChenh, a.QuyenHD, a.SoHD, TenCB = k1 != null ? k1.TenCB : "" }).FirstOrDefault();
            int _mauin = 0;
            int _mauinPT = 0;
            var qthong = data.HTHONGs.Where(p => p.MaBV == Bien.MaBV).Where(p => p.MauIn != null).Select(p => p.MauIn).FirstOrDefault();
            if (qthong != null)
            {
                string[] mauin = qthong.Split(';');
                if (mauin.Length > 2)
                    _mauin = Convert.ToInt32(mauin[2]);
                if (mauin.Length > 3)
                    _mauinPT = Convert.ToInt32(mauin[3]);
            }
            if (pt != null)
            {
                int _mabn = pt.MaBNhan ?? 0;
                if (Bien.MaBV == "24009")
                {
                    InPhieuThuChi(idTU, pt.MaBNhan ?? 0);
                    return;
                }
                if (mau == 0)
                {
                    if (((pt.PhanLoai == 3 || pt.PhanLoai == 1) && Bien.MaBV != "01071" && Bien.MaBV != "01049" && Bien.MaBV != "30009" && Bien.MaBV != "12345" && Bien.MaBV != "24297" && Bien.MaBV != "56789" && Bien.MaBV != "30303" && Bien.MaBV != "27123") || (pt.PhanLoai == 0 && Bien.MaBV == "20001") || (pt.PhanLoai == 0 && Bien.MaBV == "27023"))
                    {
                        if (Bien.MaBV == "30004")
                        {
                            BaoCao.rep_PhieuThuChi_TT107_A5_30004 rep = new BaoCao.rep_PhieuThuChi_TT107_A5_30004();
                            rep.TieuDe.Value = "BIÊN LAI THU TIỀN";
                            rep.No.Value = "Quyển số: " + pt.QuyenHD;
                            rep.Co.Value = "Số: " + pt.SoHD;
                            rep.xrTableCell9.Text = pt.TenBNhan;
                            rep.xrTableCell76.Text = pt.TenBNhan;
                            rep.TenCB.Value = pt.TenCB;

                            rep.HoTen.Value = pt.TenBNhan.ToUpper();
                            rep.DChi.Value = pt.DChi;
                            rep.NoiDung.Value = pt.LyDo;
                            double st = (pt.PhanLoai == 3 ? (pt.SoTien ?? 0.0) : pt.TienChenh);
                            string fomat = "{0:#,#}";
                            if (Bien.FormatString[1] != "")
                                fomat = Bien.FormatString[1];
                            rep.SoTien.Value = string.Format(fomat, st) + " VNĐ";
                            rep.SoThanhChu.Value = DungChung.Ham.DocTienBangChu((pt.PhanLoai == 3 ? pt.SoTien : pt.TienChenh) ?? 0.0, "");
                            rep.NguoiLap.Value = pt.TenCB;
                            rep.NgayThang.Value = "Ngày " + pt.NgayThu.Value.Day + " tháng " + pt.NgayThu.Value.Month + " năm " + pt.NgayThu.Value.Year;
                            rep.CreateDocument();
                            _mauinPT = -1;
                            frmIn frm = new frmIn();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        else if (Bien.MaBV == "30003")
                        {

                            frmIn frm = new frmIn();
                            BaoCao.Rep_PhieuThuChi_2Lien_30003 rep = usTamThu_TToan.in2lien(data, _mabn, idTU, "Liên 1: Giao cho bệnh nhân");
                            BaoCao.Rep_PhieuThuChi_2Lien_30003 rep2 = usTamThu_TToan.in2lien(data, _mabn, idTU, "Liên 2: Lưu cơ quan");
                            rep.Pages.AddRange(rep2.Pages);
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();

                        }
                        //else if (Bien.MaBV == "30303" && pt.PhanLoai == 3)
                        //{
                        //    _InPhieuThu_01071(idTU, 3, true);
                        //}
                        else if (_mauinPT == 0 || Bien.MaBV == "24012")
                        {
                            BaoCao.rep_PhieuThuChi_TT107 rep = new BaoCao.rep_PhieuThuChi_TT107();
                            rep.TieuDe.Value = "PHIẾU THU";
                            rep.xrTableCell11.Text = "Họ và tên người nộp tiền: ";
                            rep.NguoiNop.Value = "NGƯỜI NỘP";
                            rep.NguoiNhan.Value = "THỦ QUỸ";
                            rep.xrTableCell56.Text = pt.TenBNhan;
                            rep.Nguoi11.Value = "NGƯỜI NỘP";
                            rep.TenCB.Value = pt.TenCB;


                            rep.xrTableCell2.Text = Bien.MaBV == "14017" ? "Mã bệnh nhân: " + pt.MaBNhan : "Mẫu số: C40-BB";
                            rep.xrTableCell72.Text = Bien.MaBV == "14017" ? "Mã bệnh nhân: " + pt.MaBNhan : "Mẫu số: C40-BB";

                            rep.So.Value = "Số: " + pt.SoHD;
                            rep.QuyenSo.Value = "Quyển số: " + pt.QuyenHD;
                            rep.No.Value = "Nợ:";
                            rep.Co.Value = "Có:";
                            rep.SubBand1.Visible = false;
                            rep.SubBand2.Visible = true;
                            rep.MaBN.Value = pt.MaBNhan;
                            rep.clMaBNhan.Text = "Mã BN: " + pt.MaBNhan;
                            rep.clMaBNhan1.Text = "Mã BN: " + pt.MaBNhan;
                            rep.HoTen.Value = pt.TenBNhan.ToUpper() + "       (SN: " + pt.NgaySinh + "/" + pt.ThangSinh + "/" + pt.NamSinh + ")";
                            rep.DChi.Value = pt.DChi;
                            rep.NoiDung.Value = pt.LyDo;
                            // string[] ar = Bien.FormatString[1].Split(';');
                            if (pt.PhanLoai == 0)
                            {
                                string fomat = "{0:#,#}";
                                if (Bien.FormatString[1] != "")
                                    fomat = Bien.FormatString[1];
                                rep.SoTien.Value = string.Format(fomat, pt.SoTien ?? 0.0) + " VNĐ";
                                rep.SoThanhChu.Value = DungChung.Ham.DocTienBangChu(pt.SoTien ?? 0.0, " đồng");

                            }
                            else
                            {


                                double st = (pt.PhanLoai == 3 ? (pt.SoTien ?? 0.0) : (_mauin == 0 ? pt.TienChenh : (pt.SoTien ?? 0.0)));
                                string fomat = "{0:#,#}";
                                if (Bien.FormatString[1] != "")
                                    fomat = Bien.FormatString[1];
                                rep.SoTien.Value = string.Format(fomat, st) + " VNĐ";

                                rep.SoThanhChu.Value = DungChung.Ham.DocTienBangChu((pt.PhanLoai == 3 ? pt.SoTien : (_mauin == 0 ? pt.TienChenh : pt.SoTien)) ?? 0.0, "") + (Bien.MaBV == "30010" ? " đồng" : "");
                            }
                            if (Bien.MaBV == "30005" && Bien.MaBV == "30010")
                            {
                                var tencb = data.CanBoes.Where(p => p.MaCB == Bien.MaCB).Select(p => p.TenCB).FirstOrDefault();
                                rep.celngthu1.Text = tencb != null ? tencb : "";
                                rep.celngthu2.Text = tencb != null ? tencb : "";
                            }
                            rep.NguoiLap.Value = pt.TenCB;
                            rep.NgayThang.Value = "Ngày " + pt.NgayThu.Value.Day + " tháng " + pt.NgayThu.Value.Month + " năm " + pt.NgayThu.Value.Year;
                            rep.CreateDocument();
                            frmIn frm = new frmIn();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        else if (_mauinPT == 1 || _mauinPT == 2)
                        {
                            //BaoCao.rep_PhieuThuChi_TT107_A5 rep = new BaoCao.rep_PhieuThuChi_TT107_A5();
                            //rep.TieuDe.Value = "PHIẾU THU";
                            //rep.xrTableCell11.Text = "Họ và tên người nộp tiền: ";
                            //rep.NguoiNop.Value = "NGƯỜI NỘP";
                            //rep.NguoiNhan.Value = "THỦ QUỸ";
                            //rep.xrTableCell2.Text = "Mẫu số: C40-BB";
                            //rep.So.Value = "Số: " + pt.SoHD;
                            //rep.QuyenSo.Value = "Quyển số: " + pt.QuyenHD;
                            //rep.No.Value = "Nợ:";
                            //rep.Co.Value = "Có:";
                            //rep.SubBand1.Visible = true;
                            //rep.clMaBNhan.Text = "Mã BN: " + pt.MaBNhan;
                            //rep.HoTen.Value = pt.TenBNhan.ToUpper();
                            //rep.DChi.Value = pt.DChi;
                            //rep.NoiDung.Value = pt.LyDo;
                            //string[] ar = Bien.FormatString[1].Split(';');
                            //rep.SoTien.Value = (pt.PhanLoai == 3 ? (pt.SoTien ?? 0.0).ToString(ar[0].Substring(3)) : (_mauin == 0 ? pt.TienChenh.ToString(ar[0].Substring(3)) : (pt.SoTien ?? 0.0).ToString(ar[0].Substring(3)))) + " VNĐ";
                            //rep.SoThanhChu.Value = DungChung.Ham.DocTienBangChu((pt.PhanLoai == 3 ? pt.SoTien : (_mauin == 0 ? pt.TienChenh : pt.SoTien)) ?? 0.0, "");
                            //rep.NguoiLap.Value = pt.TenCB;
                            //rep.NgayThang.Value = "Ngày " + pt.NgayThu.Value.Day + " tháng " + pt.NgayThu.Value.Month + " năm " + pt.NgayThu.Value.Year;
                            //rep.CreateDocument();
                            frmIn frm = new frmIn();

                            //if (_mauinPT == 1)
                            //    rep.Pages.AddRange(rep.Pages);
                            //if (_mauinPT == 2)
                            //{
                            //    rep.Pages.AddRange(rep);
                            //    rep.Pages.AddRange(rep);
                            //}

                            BaoCao.rep_PhieuThuChi_TT107_A5 rep = usTamThu_TToan._NhieuLien(idTU, _mauin, Bien.MaBV == "30010" ? "Liên 1: Lưu" : (Bien.MaBV == "30002" ? "Liên 1: lưu kế toán" : "Liên 1: giao bệnh nhân"));
                            BaoCao.rep_PhieuThuChi_TT107_A5 rep1 = usTamThu_TToan._NhieuLien(idTU, _mauin, "");

                            if (_mauinPT > 0 && Bien.MaBV != "24012")
                            {
                                BaoCao.rep_PhieuThuChi_TT107_A5 rep2 = usTamThu_TToan._NhieuLien(idTU, _mauin, Bien.MaBV == "30010" ? "Liên 2: Giao bệnh nhân" : (Bien.MaBV == "30002" ? "Liên 2: giao bệnh nhân" : "Liên 2: lưu kế toán"));

                                rep.Pages.AddRange(rep2.Pages);
                            }
                            if (_mauinPT == 2 && Bien.MaBV != "24012")
                            {
                                BaoCao.rep_PhieuThuChi_TT107_A5 rep2 = usTamThu_TToan._NhieuLien(idTU, _mauin, "Liên 3: lưu thủ quỹ");
                                rep.Pages.AddRange(rep2.Pages);
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                            else if (DungChung.Bien.MaBV == "30010")
                            {
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                            else
                            {
                                frm.prcIN.PrintingSystem = rep1.PrintingSystem;
                                frm.ShowDialog();
                            }
                        }
                    }
                    else if (pt.PhanLoai == 2 && Bien.MaBV != "01071" && Bien.MaBV != "01049" && Bien.MaBV != "12345" && Bien.MaBV != "24297" && Bien.MaBV != "56789" && Bien.MaBV != "27183")
                    {
                        if (Bien.MaBV == "30003")
                        {
                            frmIn frm = new frmIn();
                            BaoCao.Rep_PhieuThuChi_2Lien_30003 rep = usTamThu_TToan.in2lien(data, _mabn, idTU, "Liên 1: lưu cơ quan");
                            BaoCao.Rep_PhieuThuChi_2Lien_30003 rep2 = usTamThu_TToan.in2lien(data, _mabn, idTU, "Liên 2: Giao cho bệnh nhân");
                            rep.Pages.AddRange(rep2.Pages);
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        else if (Bien.MaBV == "27022")
                        {
                            frmIn frm = new frmIn();
                            BaoCao.rep_PhieuThuChi_TT107_A5 rep = usTamThu_TToan._NhieuLienChi(idTU, _mauin, "Liên 1: lưu bệnh nhân");
                            BaoCao.rep_PhieuThuChi_TT107_A5 rep2 = usTamThu_TToan._NhieuLienChi(idTU, _mauin, "Liên 2: lưu kế toán");
                            rep.Pages.AddRange(rep2.Pages);
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        else
                        {
                            BaoCao.rep_PhieuThuChi_TT107 rep = new BaoCao.rep_PhieuThuChi_TT107();
                            rep.TieuDe.Value = "PHIẾU CHI";
                            rep.MaBN.Value = "Mã BNhan:  " + pt.MaBNhan.ToString();
                            rep.Nguoi11.Value = "NGƯỜI NHẬN TIỀN";
                            rep.xrTableCell11.Text = "Họ và tên người nhận tiền: ";
                            rep.NguoiNop.Value = "THỦ QUỸ";
                            rep.NguoiNhan.Value = "NGƯỜI NHẬN TIỀN";
                            rep.nguoithu1.NullValueText = pt.TenBNhan;
                            rep.TenCB.Value = pt.TenCB;

                            rep.xrTableCell2.Text = "Mẫu số: C41-BB";
                            rep.xrTableCell72.Text = "Mẫu số: C41-BB";
                            rep.So.Value = "Số: " + pt.SoHD;
                            rep.QuyenSo.Value = "Quyển số: " + pt.QuyenHD;
                            rep.No.Value = "Nợ:";
                            rep.Co.Value = "Có:";
                            rep.SubBand1.Visible = false;
                            rep.SubBand2.Visible = true;
                            rep.clMaBNhan.Text = "Mã BN: " + pt.MaBNhan;
                            rep.HoTen.Value = pt.TenBNhan.ToUpper();
                            rep.DChi.Value = pt.DChi;
                            rep.NoiDung.Value = pt.LyDo;

                            double st = (pt.PhanLoai == 3 ? (pt.SoTien ?? 0.0) : (_mauin == 0 ? pt.TienChenh : (pt.SoTien ?? 0.0)));
                            string fomat = "{0:#,#}";
                            if (Bien.FormatString[1] != "")
                                fomat = Bien.FormatString[1];
                            rep.SoTien.Value = string.Format(fomat, st) + " VNĐ";
                            rep.SoThanhChu.Value = DungChung.Ham.DocTienBangChu((pt.PhanLoai == 3 ? pt.SoTien : (_mauin == 0 ? pt.TienChenh : pt.SoTien)) ?? 0.0, "") + (Bien.MaBV == "30010" ? " đồng" : "");
                            rep.NguoiLap.Value = pt.TenCB;
                            rep.NgayThang.Value = "Ngày " + pt.NgayThu.Value.Day + " tháng " + pt.NgayThu.Value.Month + " năm " + pt.NgayThu.Value.Year;
                            rep.CreateDocument();
                            frmIn frm = new frmIn();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                    }
                    else if (pt.PhanLoai == 0 || pt.PhanLoai == 7 || pt.PhanLoai == 4 || Bien.MaBV == "01071" || Bien.MaBV == "01049" || Bien.MaBV == "30009" || Bien.MaBV == "12345" || Bien.MaBV == "24297" || Bien.MaBV == "56789" || Bien.MaBV == "30303" || Bien.MaBV == "27123")
                    {
                        InPhieuThuChi(idTU, pt.MaBNhan ?? 0);
                    }
                }
                else if (mau == 1)
                {
                    BaoCao.rep_PhieuThuChi_TT107 rep = new BaoCao.rep_PhieuThuChi_TT107();
                    rep.TieuDe.Value = "BIÊN LAI THU TIỀN";
                    rep.Nguoi11.Value = "NGƯỜI NỘP";
                    rep.xrTableCell11.Text = "Họ và tên người nộp: ";
                    rep.xrTableCell2.Text = "Mẫu số: C45-BB";
                    rep.xrTableCell72.Text = "Mẫu số: C45-BB";
                    rep.No.Value = "Quyển số: " + pt.QuyenHD;
                    rep.Co.Value = "Số: " + pt.SoHD;
                    rep.xrTableCell68.Text = pt.TenBNhan;
                    rep.celngthu1.Text = pt.TenCB;
                    rep.xrTableCell109.Text = pt.TenBNhan;
                    rep.celngthu2.Text = pt.TenCB;
                    rep.SubBand1.Visible = true;
                    rep.SubBand2.Visible = false;
                    rep.clMaBNhan.Text = "Mã BN: " + pt.MaBNhan;
                    rep.HoTen.Value = pt.TenBNhan.ToUpper();
                    rep.DChi.Value = pt.DChi;
                    rep.NoiDung.Value = pt.LyDo;

                    double st = (pt.PhanLoai == 3 ? (pt.SoTien ?? 0.0) : pt.TienChenh);
                    string fomat = "{0:#,#}";
                    if (Bien.FormatString[1] != "")
                        fomat = Bien.FormatString[1];
                    rep.SoTien.Value = string.Format(fomat, st) + " VNĐ";
                    rep.SoThanhChu.Value = DungChung.Ham.DocTienBangChu((pt.PhanLoai == 3 ? pt.SoTien : pt.TienChenh) ?? 0.0, "");
                    rep.NguoiLap.Value = pt.TenCB;
                    rep.NgayThang.Value = "Ngày " + pt.NgayThu.Value.Day + " tháng " + pt.NgayThu.Value.Month + " năm " + pt.NgayThu.Value.Year;
                    rep.CreateDocument();
                    frmIn frm = new frmIn();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
            }
        }
        void enableDonNgoai(bool t)
        {
            //cboTimTT.Enabled = !t;
            cboNoitru.Enabled = !t;
            cboDTuong.Enabled = !t;
            colMaBNhan.Visible = !t;
            colXemHSBN.Visible = !t;
            colNamSinh.Visible = !t;
            //colNNhap.Visible = !t;
            //colMaKPbn.Visible = !t;
            colSThe.Visible = !t;
            colGTinh.Visible = !t;
            colSua.Visible = !t;
            xtraDuyet.PageVisible = !t;
            xtraTThu.PageVisible = !t;
        }
        private void ckcDonNgoai_CheckedChanged(object sender, EventArgs e)
        {
            if (ckcDonNgoai.Checked == true)
            {
                TimKiemDonThuoc();
                enableDonNgoai(true);
                labKhoaPhong.Text = "Kho xuất:";
                //txtTimKiem.Properties.NullText = "Nhập Số CT||Tên Ng Mua";
                colIDNhap.Visible = true;
                colTenBNhan.Caption = "Tên người mua";
                colMaKPbn.Caption = "Kho xuất";
                cboInBangKe.Properties.Items.Clear();
                string[] st = new string[] { "In phiếu thu" };
                cboInBangKe.Properties.Items.AddRange(st);
                //cboTimTT
            }
            else
            {
                TimKiem();
                cboInBangKe.Properties.Items.Clear();
                string[] st = new string[] { "Bảng kê mẫu A5", "Bảng kê mẫu A4", "Bảng kê mẫu A4_New", "Phiếu TT ra viện(mẫu 38)", "Phiếu TT ra viện(mẫu 40)", "Bảng kê QĐ 6556" };
                string[] st_19048 = new string[] { "Bảng kê mẫu A5", "Bảng kê mẫu A4", "Bảng kê mẫu A4_New" };
                if (Bien.MaBV == "19048")
                    cboInBangKe.Properties.Items.AddRange(st_19048);
                else
                    cboInBangKe.Properties.Items.AddRange(st);
                enableDonNgoai(false);
                //txtTimKiem.Properties.NullText = "Nhập tên BN|Mã số|Số thẻ";
                labKhoaPhong.Text = "K.Phòng:";
                colIDNhap.Visible = false;
                colTenBNhan.Caption = "Tên bệnh nhân";
                colMaKPbn.Caption = "Khoa phòng";
            }
        }
        public class PhieuPhatThuoc
        {
            public int MaDV { get; set; }
            public string TenDV { get; set; }
            public string DonVi { get; set; }
            public double DonGia { get; set; }
            public double SoLuong { get; set; }
            public double ThanhTien { get; set; }
            public string NuocSX { get; set; }
            public string Thue { get; set; }
        }
        public static BaoCao.rep_PhieuPhatThuoc innhieudon(QLBVEntities _dataContext, int _Mabn, int IDDon, int MaKPX)
        {
            var dt = (from a in _dataContext.VienPhis.Where(p => p.MaBNhan == _Mabn)
                      join b in _dataContext.DThuocs.Where(p => p.PLDV == 1 && p.IDDon == IDDon) on a.MaBNhan equals b.MaBNhan
                      join d in _dataContext.DThuoccts.Where(p => p.MaKXuat == MaKPX) on b.IDDon equals d.IDDon
                      join c in _dataContext.DichVus.Where(p => p.PLoai == 1).Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6) on d.MaDV equals c.MaDV
                      select new
                      {
                          b.IDDon,
                          b.MaKP,
                          b.MaKXuat,
                          a.MaBNhan,
                          TenDV = c.TenDV + "/ " + c.HamLuong,
                          d.DonVi,
                          d.SoLuong,
                          d.DonGia,
                          d.ThanhTien,
                          a.NgayTT,
                          c.NuocSX,
                          d.SoLo,
                          d.MaDV
                      }).ToList();
            if (Bien.MaBV == "56789")
            {
                List<PhieuPhatThuoc> ptt = new List<PhieuPhatThuoc>();
                foreach (var item in dt)
                {
                    PhieuPhatThuoc moi = new PhieuPhatThuoc();
                    moi.MaDV = item.MaDV ?? 0;
                    moi.TenDV = item.TenDV;
                    moi.DonVi = item.DonVi;
                    moi.SoLuong = item.SoLuong;
                    moi.DonGia = item.DonGia;
                    moi.ThanhTien = item.ThanhTien;
                    moi.NuocSX = item.NuocSX;
                    if (!string.IsNullOrEmpty(item.SoLo))
                    {
                        var qq = (from nd in _dataContext.NhapDs.Where(p => p.PLoai == 1)
                                  join ndct in _dataContext.NhapDcts.Where(p => p.MaDV == item.MaDV && p.SoLo == item.SoLo) on nd.IDNhap equals ndct.IDNhap
                                  select new
                                  {
                                      ndct.VAT,
                                      ndct.SoLo
                                  }).ToList();
                        if (qq.Count > 0)
                            moi.Thue = qq.First().VAT + "%";
                    }
                    ptt.Add(moi);
                }
                int makp2 = dt.FirstOrDefault().MaKP ?? 0;
                var bnkb = (from a in _dataContext.BNKBs.Where(p => p.MaBNhan == _Mabn && p.MaKP == makp2)
                            group a by new { a.MaBNhan } into kq
                            select new
                            {
                                kq.Key.MaBNhan,
                                IDBK = kq.Max(p => p.IDKB),
                                ChanDoan = "",
                                ChanDoanKhac = "",
                            }).ToList();
                var makp = (from a in bnkb
                            join b in _dataContext.BNKBs on a.IDBK equals b.IDKB
                            join c in _dataContext.KPhongs on b.MaKP equals c.MaKP
                            join d in _dataContext.RaViens on a.MaBNhan equals d.MaBNhan
                            select new { c.TenKP, c.MaKP, b.ChanDoan, b.BenhKhac }).ToList();
                BaoCao.rep_PhieuPhatThuoc rep = new BaoCao.rep_PhieuPhatThuoc();
                var dtct = _dataContext.DThuoccts.Where(p => p.IDDon == IDDon).ToList();
                rep.TongKhoan.Value = dt.Count().ToString();
                rep.TongTien.Value = dt.Sum(p => p.ThanhTien).ToString("###,###.00") + " đồng";
                rep.TenKP.Value = makp.FirstOrDefault().TenKP;
                int makp1 = dtct.FirstOrDefault().MaKXuat ?? 0;
                rep.TenKho.Value = _dataContext.KPhongs.Where(p => p.MaKP == makp1).FirstOrDefault().TenKP;
                rep.ChanDoan.Value = makp.FirstOrDefault().ChanDoan;
                rep.BenhKhac.Value = makp.FirstOrDefault().BenhKhac;
                var bn = _dataContext.BenhNhans.Where(p => p.MaBNhan == _Mabn).ToList().FirstOrDefault();
                rep._MaBNhan.Value = bn.MaBNhan.ToString();
                rep._TenBNhan.Value = bn.TenBNhan;
                rep.Tuoi.Value = bn.Tuoi;
                rep.GTinh.Value = bn.GTinh == 0 ? "Nữ" : "Nam";
                rep.DiaChi.Value = bn.DChi;
                rep.SThe.Value = bn.SThe;
                rep.GT.Value = bn.HanBHTu.Value.ToString("dd/MM/yyyy");
                rep.Den.Value = bn.HanBHDen.Value.ToString("dd/MM/yyyy");
                rep.DT.Value = bn.DTuong;
                rep.ngayke.Value = DateTime.Now.Hour + " " + "giờ" + " " + DateTime.Now.Minute + "," + " " + "ngày" + " " + DateTime.Now.Day + " " + "tháng" + " " + DateTime.Now.Month + " " + "năm" + " " + DateTime.Now.Year; //dt.Count > 0 ? dt.First().NgayTT : DateTime.Now;
                if (ptt.Count > 0)
                {
                    rep.DataSource = ptt.ToList();
                    rep.BindData();
                    rep.CreateDocument();
                }
                return rep;
            }
            else
            {
                int makp2 = dt.FirstOrDefault().MaKP ?? 0;
                var bnkb = (from a in _dataContext.BNKBs.Where(p => p.MaBNhan == _Mabn && p.MaKP == makp2)
                            group a by new { a.MaBNhan } into kq
                            select new
                            {
                                kq.Key.MaBNhan,
                                IDBK = kq.Max(p => p.IDKB),
                                ChanDoan = "",
                                ChanDoanKhac = "",
                            }).ToList();
                var makp = (from a in bnkb
                            join b in _dataContext.BNKBs on a.IDBK equals b.IDKB
                            join c in _dataContext.KPhongs on b.MaKP equals c.MaKP
                            join d in _dataContext.RaViens on a.MaBNhan equals d.MaBNhan
                            select new { c.TenKP, c.MaKP, b.ChanDoan, b.BenhKhac }).ToList();
                BaoCao.rep_PhieuPhatThuoc rep = new BaoCao.rep_PhieuPhatThuoc();
                var dtct = _dataContext.DThuoccts.Where(p => p.IDDon == IDDon).ToList();
                rep.TongKhoan.Value = dt.Count().ToString();
                rep.TongTien.Value = dt.Sum(p => p.ThanhTien).ToString("###,###.00") + " đồng";
                rep.TenKP.Value = makp.FirstOrDefault().TenKP;
                int makp1 = dtct.FirstOrDefault().MaKXuat ?? 0;
                rep.TenKho.Value = _dataContext.KPhongs.Where(p => p.MaKP == makp1).FirstOrDefault().TenKP;
                rep.ChanDoan.Value = makp.FirstOrDefault().ChanDoan;
                rep.BenhKhac.Value = makp.FirstOrDefault().BenhKhac;
                var bn = _dataContext.BenhNhans.Where(p => p.MaBNhan == _Mabn).ToList().FirstOrDefault();
                rep._MaBNhan.Value = bn.MaBNhan.ToString();
                rep._TenBNhan.Value = bn.TenBNhan;
                rep.Tuoi.Value = bn.Tuoi;
                rep.GTinh.Value = bn.GTinh == 0 ? "Nữ" : "Nam";
                rep.DiaChi.Value = bn.DChi;
                rep.SThe.Value = bn.SThe;
                //rep.ngayke.Value = DateTime.Now.Hour + " " + "giờ" + " " + DateTime.Now.Minute + "," + " " + "ngày" + " " + DateTime.Now.Day + " " + "tháng" + " " + DateTime.Now.Month + " " + "năm" + " " + DateTime.Now.Year; //dt.Count > 0 ? dt.First().NgayTT : DateTime.Now;

                if (bn.HanBHTu != null)
                {
                    rep.GT.Value = bn.HanBHTu.Value.ToString("dd/MM/yyyy");
                }
                if (bn.HanBHDen != null)
                {
                    rep.Den.Value = bn.HanBHDen.Value.ToString("dd/MM/yyyy");
                }


                rep.DT.Value = bn.DTuong;
                //rep.ngayke.Value = dt.Count > 0 ? dt.First().NgayTT : DateTime.Now;

                if (dt.Count > 0)
                {
                    DateTime date = dt.First().NgayTT.Value;
                    rep.ngayke.Value = date.Hour + " " + "giờ" + " " + date.Minute + "," + " " + "ngày" + " " + date.Day + " " + "tháng" + " " + date.Month + " " + "năm" + " " + date.Year;
                }
                else
                {
                    rep.ngayke.Value = DateTime.Now.Hour + " " + "giờ" + " " + DateTime.Now.Minute + "," + " " + "ngày" + " " + DateTime.Now.Day + " " + "tháng" + " " + DateTime.Now.Month + " " + "năm" + " " + DateTime.Now.Year; //dt.Count > 0 ? dt.First().NgayTT : DateTime.Now;
                }
                if (dt.Count > 0)
                {
                    rep.DataSource = dt.ToList();
                    rep.BindData();
                    rep.CreateDocument();
                }
                return rep;
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            int rs;
            int id = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                id = Convert.ToInt32(txtMaBNhan.Text);

            //    _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
            var dt = (from a in _dataContext.VienPhis.Where(p => p.MaBNhan == id)
                      join b in _dataContext.DThuocs.Where(p => p.PLDV == 1) on a.MaBNhan equals b.MaBNhan
                      join d in _dataContext.DThuoccts on b.IDDon equals d.IDDon
                      join c in _dataContext.DichVus.Where(p => p.PLoai == 1).Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6) on d.MaDV equals c.MaDV
                      select new
                      {
                          b.IDDon,
                          b.MaKP,
                          a.MaBNhan,
                          TenDV = c.TenDV + "/ " + c.HamLuong,
                          d.DonVi,
                          d.SoLuong,
                          d.DonGia,
                          d.ThanhTien,
                          c.NuocSX,
                          d.MaKXuat
                      }).ToList();
            var x1 = (from a in dt
                      group a by new { a.IDDon, a.MaKP, a.MaKXuat } into kq
                      select new { kq.Key.IDDon, kq.Key.MaKP, kq.Key.MaKXuat }).OrderBy(p => p.IDDon).ToList();
            int minid = x1.Min(p => p.IDDon);
            int makpx = x1.Where(p => p.IDDon == minid).First().MaKXuat ?? 0;
            frmIn frm = new frmIn();
            BaoCao.rep_PhieuPhatThuoc rep = usTamThu_TToan.innhieudon(_dataContext, id, minid, makpx);
            foreach (var item in x1.Where(p => p.IDDon != minid))
            {
                BaoCao.rep_PhieuPhatThuoc rep2 = usTamThu_TToan.innhieudon(_dataContext, id, item.IDDon, item.MaKXuat ?? 0);
                rep.Pages.AddRange(rep2.Pages);
            }
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void btnInChiTiet_Click(object sender, EventArgs e)
        {
            int _idtthu = 0;
            _intMauChiTietA5 = true;
            if (!string.IsNullOrEmpty(txtIdTamUng.Text))
            {
                _idtthu = int.Parse(txtIdTamUng.Text);

                _InPhieuThu_01071(_idtthu, 3, true);


            }
        }

        public void CreateInvoice(int patientId, int advanceId, bool isAdvance)
        {
            BenhNhan patient = _dataContext.BenhNhans.FirstOrDefault(p => p.MaBNhan == patientId);
            TTboXung additionalInformation = _dataContext.TTboXungs.FirstOrDefault(p => p.MaBNhan == patientId);
            TamUng advances = _dataContext.TamUngs.FirstOrDefault(p => p.MaBNhan == patientId && (isAdvance ? p.IDTamUng == advanceId : p.PhanLoai == 1));
            var _ttoan = (from vp in _dataContext.VienPhis.Where(p => p.MaBNhan == patientId)
                          join vpct in _dataContext.VienPhicts.OrderBy(p => p.idVPhict) on vp.idVPhi equals vpct.idVPhi
                          select new { vp, vpct }).ToList();
            if (!string.IsNullOrEmpty(Bien.xmlFilePath_LIS[20]) && Bien.xmlFilePath_LIS[20] == "0")
            {
                var isValid = true;
                if (string.IsNullOrEmpty(Bien.xmlFilePath_LIS[21]))
                {
                    isValid = false;
                    MessageBox.Show("Chưa thiết lập loại hóa đơn");
                }
                if (string.IsNullOrEmpty(Bien.xmlFilePath_LIS[22]))
                {
                    isValid = false;
                    MessageBox.Show("Chưa thiết lập mẫu hóa đơn");
                }
                if (string.IsNullOrEmpty(Bien.xmlFilePath_LIS[23]))
                {
                    isValid = false;
                    MessageBox.Show("Chưa thiết lập kí hiệu hóa đơn");
                }
                if (patient == null || additionalInformation == null || advances == null)
                {
                    isValid = false;
                    MessageBox.Show("Bệnh nhân không đủ điều kiện để lập hóa đơn");
                }

                if (isValid)
                {
                    int amount = 0;
                    if (DungChung.Bien.MaBV == "27022")
                        amount = Convert.ToInt32(_ttoan.Sum(p => p.vpct.TBNCTT) + _ttoan.Sum(p => p.vpct.TBNTT));
                    else
                        amount = (int)Math.Round(advances.SoTien ?? 0, 0);
                    string taxCode = Bien.xmlFilePath_LIS[17];

                    InvoiceInfo objInvoice = new InvoiceInfo()
                    {
                        transactionUuid = Guid.NewGuid().ToString(),
                        invoiceType = Bien.xmlFilePath_LIS[21], //Mã loại hóa đơn chỉ nhận các giá trị sau: 01GTKT, 02GTTT, 07KPTQ, 03XKNB, 04HGDL. tuân thủ theo quy định ký hiệu loại hóa đơn của Thông tư hướng dẫn thi hành nghị định số 51/2010/NĐ-CP
                        templateCode = Bien.xmlFilePath_LIS[22], //Mã mẫu hóa đơn, tuân thủ theo quy định ký hiệu mẫu hóa đơn của Thông tư hướng dẫn thi hành nghị định số 51/2010/NĐ-CP
                        invoiceSeries = Bien.xmlFilePath_LIS[23], //Là “Ký hiệu hóa đơn” tuân thủ theo quy tắc tạo ký hiệu hóa đơn của Thông tư hướng 
                        invoiceIssuedDate = ((Int64)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds),
                        currencyCode = "VND",
                        adjustmentType = "1", // "1";//Trạng thái điều chỉnh hóa đơn: 1: Hóa đơn gốc 3: Hóa đơn thay thế 5: Hóa đơn điều chỉnh
                        paymentStatus = true, //True: Đã thanh toán
                        paymentType = "TM", //Loại hình thức thanh toán: Bao gồm: 
                    };

                    //CK – Chuyển khoản
                    //DTCN – Đối trừ công nợ
                    //TM – tiền mặt
                    //TM/CK – Tiền mặt/ Chuyển khoản
                    //Nếu paymentStatus = true  thì bắt buộc phải truyền
                    objInvoice.paymentTypeName = "TM";//Tên phương thức thanh toán
                    //CK – Chuyển khoản
                    //DTCN – Đối trừ công nợ
                    //TM – tiền mặt
                    //TM/CK – Tiền mặt/ Chuyển khoản
                    //Nếu paymentStatus = true  thì bắt buộc phải truyền
                    objInvoice.cusGetInvoiceRight = true;
                    objInvoice.buyerIdNo = "";//Số giấy tờ của khách hàng
                    objInvoice.buyerIdType = "";//Loại giấy tờ của khách hàng:
                    //-	1: Số CMND
                    //-	3: Giấy phép kinh doanh
                    //-	2: Hộ chiếu
                    BuyerInfo objBuyer = new BuyerInfo();
                    if (!string.IsNullOrEmpty(additionalInformation.TenDonVi) && Bien.MaBV == "24009")
                        objBuyer.buyerAddressLine = additionalInformation.DiaChiNoiLV;
                    else
                        objBuyer.buyerAddressLine = patient.DChi;//"HN VN";//Địa chỉ bưu điện người mua 
                    objBuyer.buyerIdNo = objInvoice.buyerIdNo;//Số giấy tờ của khách hàng
                    objBuyer.buyerIdType = objInvoice.buyerIdType;//Loại giấy tờ của khách hàng:
                    //-	1: Số CMND
                    //-	3: Giấy phép kinh doanh
                    //-	2: Hộ chiếu
                    objBuyer.buyerName = patient.TenBNhan.ToUpper();
                    objBuyer.buyerPhoneNumber = additionalInformation.DThoai ?? string.Empty;//Điện thoại
                    objBuyer.buyerEmail = "";//Email
                    objBuyer.buyerLegalName = additionalInformation.TenDonVi ?? string.Empty;//Tên (đăng ký kinh doanh trong trường hợp là doanh nghiệp) của người mua
                    objBuyer.buyerTaxCode = additionalInformation.MaSoThue ?? string.Empty;//Mã số thuế
                    objBuyer.buyerBankName = additionalInformation.TenNganHang ?? string.Empty;//Tên ngân hàng
                    objBuyer.buyerBankAccount = additionalInformation.SoTaiKhoan ?? string.Empty;//Số tài khoản

                    //Seller
                    SellerInfo objSeller = new SellerInfo();
                    objSeller.sellerAddressLine = Bien.DiaChi ?? string.Empty;//"HN VN";//Địa chỉ bưu điện người bán
                    objSeller.sellerBankAccount = "";//Tài khoản ngân hàng của người bán
                    objSeller.sellerBankName = "";
                    objSeller.sellerEmail = "";
                    objSeller.sellerFaxNumber = "";
                    objSeller.sellerWebsite = "";
                    objSeller.sellerLegalName = Bien.TenCQ ?? string.Empty;//Tên (đăng ký kinh doanh trong trường hợp là doanh nghiệp) của người bán
                    objSeller.sellerPhoneNumber = Bien.SDTCQ ?? string.Empty;
                    objSeller.sellerTaxCode = taxCode ?? string.Empty;//Mã số thuế người bán được cấp bởi TCT Việt Nam. Mẫu 1: 0312770607 Mẫu 2: 0312770607-001

                    List<ItemInfo> lstItem = new List<ItemInfo>()
                    {
                        new ItemInfo()
                        {
                            discount = 0,// "0.0";//giảm giá
                            itemCode = "",//Mã hàng hóa, dịch vụ
                            itemDiscount = 0,//"0.0";
                            itemName = advances.LyDo,//"Tiền viện phí";
                            itemTotalAmountWithoutTax = amount,
                            lineNumber = 1,//"1";
                            quantity = 1,//"1";//Số lượng
                            taxAmount = 0,//"0.0";//Tổng tiền thuế
                            taxPercentage = 0,//"0";//Thuế suất của hàng hóa, dịch vụ 
                            unitName = "Lần",
                            unitPrice = amount,
                        }
                    };

                    SummarizeInfo objSummary = new SummarizeInfo();
                    objSummary.discountAmount = 0;//"0";//Tổng tiền chiết khấu thương mại trên toàn hóa đơn trước khi tính thuế. Chú ý: Khi tính chiết khấu, toàn hóa đơn chỉ sử dụng một mức thuế
                    objSummary.settlementDiscountAmount = 0;//"0";//Tổng tiền chiết khấu thanh toán trên toàn hóa đơn sau khi tính thuế. Chú ý: Khi tính chiết khấu, toàn hóa đơn chỉ sử dụng một mức thuế.
                    objSummary.sumOfTotalLineAmountWithoutTax = amount;//Tổng thành tiền cộng gộp của tất cả các dòng hóa đơn chưa bao gồm VAT.
                    //-	Hóa đơn thường: Tổng tiền HHDV trên các dòng HĐ. 
                    //-	Hóa đơn điều chỉnh: Tổng tiền điều chỉnh của các dòng HĐ.

                    objSummary.taxPercentage = 0;//"0";//Mức thuế: khai báo giá trị như sau 
                    //-	0%: 0 
                    //-	5%: 5 
                    //-	10%: 10 
                    //-	không phải kê khai và tính thuế GTGT: -1
                    //-	Không chịu thuế:  -2 

                    objSummary.totalAmountWithoutTax = amount;//Tổng tiền hóa đơn chưa bao gồm VAT. 
                    //-	Hóa đơn thường: Tổng tiền HHDV trên các dòng HĐ và các khoản tăng/giảm khác trên toàn HĐ. 
                    //-	Hóa đơn điều chỉnh: Tổng tiền điều chỉnh của các dòng HĐ và các khoản tăng/giảm khác trên toàn HĐ.

                    objSummary.totalAmountWithTax = amount;//Tổng tiền trên hóa đơn đã bao gồm VAT.
                    //-	Hóa đơn thường: Tổng tiền HHDV trên các dòng HĐ và các khoản tăng/giảm khác trên toàn HĐ đã bao gồm cả VAT.
                    //-	Hóa đơn điều chỉnh: Tổng tiền điều chỉnh của các dòng HĐ và các khoản tăng/giảm khác trên toàn HĐ đã bao gồm cả VAT

                    objSummary.totalAmountWithTaxInWords = QLBV.DungChung.Ham.DocSoThanhChu(objSummary.totalAmountWithTax.ToString()); ;// NumberUtil.DocSoThanhChu(objSummary.totalAmountWithTax);
                    objSummary.totalTaxAmount = 0;//"0";//Tổng tiền thuế trên toàn hóa đơn.
                                                  //-	Hóa đơn thường: Tổng tiền VAT trên các dòng HĐ và các khoản thuế khác trên toàn HĐ.
                                                  //-	Hóa đơn điều chỉnh: Tổng tiền VAT điều chỉnh của các dòng HĐ và các khoản tăng/giảm VAT khác trên toàn HĐ.

                    Invoice invoice = new Invoice()
                    {
                        generalInvoiceInfo = objInvoice,
                        buyerInfo = objBuyer,
                        sellerInfo = objSeller,
                        extAttribute = new List<ExtAttribute>(),
                        payments = new List<Payment>()
                        {
                            new Payment()
                            {
                                paymentMethodName = objInvoice.paymentTypeName,
                            }
                        },
                        deliveryInfo = new DeliveryInfo(),
                        itemInfo = lstItem,
                        discountItemInfo = new List<DiscountItemInfo>(),
                        meterReading = new List<MeterReading>()
                        {
                            new MeterReading()
                            {
                                previousIndex = "5454",
                                currentIndex = "244",
                                factor = "22",
                                amount = "2"
                            },
                            new MeterReading()
                            {
                                previousIndex = "44",
                                currentIndex = "44",
                                factor = "33",
                                amount = "3"
                            }
                        },
                        summarizeInfo = objSummary,
                        taxBreakdowns = new List<TaxBreakdowns>()
                        {
                            new TaxBreakdowns()
                            {
                                taxPercentage = objSummary.taxPercentage,
                                taxableAmount = objSummary.totalTaxAmount,
                                taxAmount = objSummary.totalTaxAmount,
                            }
                        }
                    };

                    string userpass = Bien.xmlFilePath_LIS[18];
                    string url = Bien.xmlFilePath_LIS[19];

                    // Tao hoa don
                    var result = new Utilities.Commons.ResultApi<ResultCreateInvoice>();
                    if (url.Contains("sinvoice"))
                        result = Invoices.Invoice.CreateInvoiceV1(url, Security.Base64Encode(userpass), taxCode, invoice);
                    else
                        result = Invoices.Invoice.CreateInvoiceV2(url, userpass, taxCode, invoice);

                    if (result.Message != null)
                    {
                        MessageBox.Show(result.Message);
                        return;
                    }
                    else
                    {
                        advances.MaHD = result.Result.Result.InvoiceNo;
                        advances.transactionID = result.Result.Result.TransactionId;
                        advances.reservationCode = result.Result.Result.ReservationCode;
                        advances.templateCode = objInvoice.templateCode;
                        advances.StatusHD = 2;

                        string ngayTaoHD = "";

                        var date = (new DateTime(1970, 1, 1)).AddMilliseconds(double.Parse(objInvoice.invoiceIssuedDate.ToString()));
                        if (date != null)
                        {
                            DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.Local);
                            ngayTaoHD = cstTime.ToString("yyyyMMddHHmmss");
                        }
                        else
                            ngayTaoHD = DateTime.Now.ToString("yyyyMMddHHmmss");

                        advances.NgayTaoHD = ngayTaoHD;
                        advances.MaCBTaoHD = Bien.MaCB;

                        MessageBox.Show("Tạo hóa đơn thành công");

                        LOG moi = new LOG()
                        {
                            DateLog = DateTime.Now,
                            LyDo = "Tạo hóa đơn điện tử Viettel thành công",
                            UserName = Bien.TenDN,
                            MaBNhan = patientId,
                            IdForm = 904,
                            MaCB = Bien.MaCB,
                            ComputerName = SystemInformation.ComputerName,
                            Status = 1,
                        };

                        _dataContext.LOGs.Add(moi);
                        _dataContext.SaveChanges();
                    }
                }
            }
        }
        public void ViewInvoice(int patientId, int advanceId, bool isAdvance)
        {
            BenhNhan patient = _dataContext.BenhNhans.FirstOrDefault(p => p.MaBNhan == patientId);
            TTboXung additionalInformation = _dataContext.TTboXungs.FirstOrDefault(p => p.MaBNhan == patientId);
            TamUng advances = _dataContext.TamUngs.FirstOrDefault(p => p.MaBNhan == patientId && (isAdvance ? p.IDTamUng == advanceId : p.PhanLoai == 1));

            #region xem hóa đơn
            if (!string.IsNullOrEmpty(Bien.xmlFilePath_LIS[20]) && Bien.xmlFilePath_LIS[20] == "0")
            {
                if (!string.IsNullOrEmpty(advances.pathHD) && File.Exists(advances.pathHD))
                {
                    //FileInfo file = new FileInfo(advances.pathHD);
                    //Process.Start(advances.pathHD);

                    FrmPDFViewer frm = new FrmPDFViewer(advances.pathHD);
                    frm.ShowDialog();
                }
                else
                {
                    string userpass = Bien.xmlFilePath_LIS[18];
                    string url = Bien.xmlFilePath_LIS[19];
                    string taxCode = Bien.xmlFilePath_LIS[17];
                    string pathsave = Bien.xmlFilePath_LIS[16];

                    // Tao hoa don
                    var result = new Utilities.Commons.ResultApi<CreaeteFileInvoice>();
                    if (url.Contains("sinvoice"))
                        result = Invoices.Invoice.ViewInvoiceV1(url, userpass, taxCode, advances.MaHD, advances.templateCode, advances.transactionID, pathsave);
                    else
                        result = Invoices.Invoice.ViewInvoiceV2(url, userpass, taxCode, advances.MaHD, advances.templateCode, advances.transactionID, pathsave);

                    if (result.Message != null)
                    {
                        MessageBox.Show(result.Message);
                        return;
                    }

                    else if (!string.IsNullOrEmpty(result.Result.FileName) && File.Exists(result.Result.FileName))
                    {
                        advances.pathHD = result.Result.FileName;
                        _dataContext.SaveChanges();

                        //FileInfo file = new FileInfo(result.Result.FileName);
                        //if (file.Exists)
                        //    Process.Start(result.Result.FileName);

                        FrmPDFViewer frm = new FrmPDFViewer(advances.pathHD);
                        frm.ShowDialog();
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(advances.pathHD))
                {
                    FileInfo file = new FileInfo(advances.pathHD);
                    if (file.Exists)
                    {
                        Process.Start(advances.pathHD);
                    }
                }
                else
                {
                    string _fkey = advances.FkeyVNPT;
                    string userWS = Bien.xmlFilePath_LIS[27];
                    string passWS = Bien.xmlFilePath_LIS[28];
                    string pathsave = Bien.xmlFilePath_LIS[24];
                    int DeMo = Convert.ToInt32(Bien.xmlFilePath_LIS[40]);
                    string InvObj = "";

                    try
                    {
                        if (_fkey.Length > 3)
                        {
                            if (DeMo == 1)//cong that
                            {
                                if (Bien.MaBV == "30005")
                                {
                                    InvPortalService.PortalService poS = new InvPortalService.PortalService();
                                    InvObj = poS.downloadInvPDFFkey(_fkey, userWS, passWS);
                                }
                                else if (Bien.MaBV == "30012")
                                {
                                    InvPortalServiceCongThat_30012_TT78.PortalService poS = new InvPortalServiceCongThat_30012_TT78.PortalService();
                                    InvObj = poS.downloadInvPDFFkey(_fkey, userWS, passWS);
                                }
                                else if (Bien.MaBV == "30007")
                                {
                                    InvPortalService30007.PortalService poS = new InvPortalService30007.PortalService();
                                    InvObj = poS.downloadInvPDFFkey(_fkey, userWS, passWS);
                                }
                                else if (Bien.MaBV == "30003")
                                {
                                    InvPotalServiceCongThat_30003_TT78.PortalService poS = new InvPotalServiceCongThat_30003_TT78.PortalService();
                                    InvObj = poS.downloadInvPDFFkey(_fkey, userWS, passWS);
                                }
                                else if (Bien.MaBV == "27022")
                                {
                                    InvPotalServiceCongThat_30003_TT78.PortalService poS = new InvPotalServiceCongThat_30003_TT78.PortalService();
                                    InvObj = poS.downloadInvPDFFkey(_fkey, userWS, passWS);
                                }
                                else
                                {
                                    MessageBox.Show("Chưa thiết lập webservice hóa đơn");
                                    return;
                                }
                            }
                            else// cong demo
                            {
                                if (Bien.MaBV == "30005")
                                {
                                    InvPortalServicedemo.PortalService poS = new InvPortalServicedemo.PortalService();
                                    InvObj = poS.downloadInvPDFFkey(_fkey, userWS, passWS);
                                }
                                else if (Bien.MaBV == "30003")
                                {
                                    InvPotalServicedemo_30003_TT78.PortalService poS = new InvPotalServicedemo_30003_TT78.PortalService();
                                    InvObj = poS.downloadInvPDFFkey(_fkey, userWS, passWS);
                                }
                                else if (Bien.MaBV == "30012")
                                {
                                    InvPortalServicedemo_30012_TT78.PortalService poS = new InvPortalServicedemo_30012_TT78.PortalService();
                                    InvObj = poS.downloadInvPDFFkey(_fkey, userWS, passWS);
                                }
                                else if (Bien.MaBV == "30007")
                                {
                                    InvPortalServiceDemo30007.PortalService poS = new InvPortalServiceDemo30007.PortalService();
                                    InvObj = poS.downloadInvPDFFkey(_fkey, userWS, passWS);
                                }
                                else if (Bien.MaBV == "27022")
                                {
                                    InvPortalService27022.PortalService poS = new InvPortalService27022.PortalService();
                                    InvObj = poS.downloadInvPDFFkey(_fkey, userWS, passWS);
                                }
                                else
                                {
                                    MessageBox.Show("Chưa thiết lập webservice hóa đơn");
                                    return;
                                }
                            }

                            byte[] bytes = Convert.FromBase64String(InvObj);
                            String filepath = pathsave + @"\" + _fkey + ".pdf";
                            if (!System.IO.File.Exists(filepath))
                            {
                                FileStream stream = new FileStream(filepath, FileMode.CreateNew);
                                BinaryWriter writer = new BinaryWriter(stream);
                                writer.Write(bytes, 0, bytes.Length);
                                writer.Close();
                            }
                            advances.pathHD = filepath;
                            _dataContext.SaveChanges();
                            Process.Start(filepath);
                        }
                    }
                    catch
                    {
                        MessageBox.Show(checkRes(InvObj));
                    }
                }
            }
            #endregion
        }

        public void CreateInvoice01071(int patientId, List<int> advanceIds, ref bool rollBack)
        {
            TTboXung additionalInformation = _dataContext.TTboXungs.FirstOrDefault(p => p.MaBNhan == patientId);

            if (additionalInformation != null && additionalInformation.HTThanhToan == 1)
            {
                frm_NhapTTHD frm = new frm_NhapTTHD(patientId);
                frm.ShowDialog();
            }

            BenhNhan patient = _dataContext.BenhNhans.FirstOrDefault(p => p.MaBNhan == patientId);
            HTHONG system = _dataContext.HTHONGs.FirstOrDefault(p => p.MaBV == Bien.MaBV);

            string khCoThue = "";
            string khKhongThue = "";

            #region Tạo hóa đơn viettel
            if (!string.IsNullOrEmpty(Bien.xmlFilePath_LIS[20]) && Bien.xmlFilePath_LIS[20] == "0")
            {
                bool isValid = true;

                if (string.IsNullOrEmpty(Bien.xmlFilePath_LIS[21]))
                {
                    isValid = false;
                    MessageBox.Show("Chưa thiết lập loại hóa đơn");
                }
                if (string.IsNullOrEmpty(Bien.xmlFilePath_LIS[22]))
                {
                    isValid = false;
                    MessageBox.Show("Chưa thiết lập mẫu hóa đơn");
                }
                if (string.IsNullOrEmpty(Bien.xmlFilePath_LIS[23]))
                {
                    isValid = false;
                    MessageBox.Show("Chưa thiết lập kí hiệu hóa đơn");
                }
                if (!string.IsNullOrEmpty(Bien.xmlFilePath_LIS[23]))
                {
                    var splitKH = Bien.xmlFilePath_LIS[23].Split(':');
                    if (splitKH.Count() != 2)
                    {
                        isValid = false;
                        MessageBox.Show("Ký hiệu hóa đơn thiết lập không đúng định dạng");
                    }
                    else
                    {
                        khCoThue = splitKH[0];
                        khKhongThue = splitKH[1];
                        if (string.IsNullOrWhiteSpace(khCoThue) || string.IsNullOrWhiteSpace(khKhongThue))
                        {
                            isValid = false;
                            MessageBox.Show("Ký hiệu hóa đơn thiết lập không đúng định dạng");
                        }

                    }
                }

                if (patient == null || additionalInformation == null)
                {
                    isValid = false;
                    MessageBox.Show("Bệnh nhân không đủ điều kiện để lập hóa đơn");
                }

                if (!isValid)
                    rollBack = true;

                if (isValid)
                {
                    var advances = _dataContext.TamUngs.Where(p => p.MaBNhan == patientId && (p.PhanLoai == 1 || p.PhanLoai == 3) && advanceIds.Contains(p.IDTamUng)).ToList();
                    var dTuong = patient.DTuong;

                    List<TTHD> listTTHD = new List<TTHD>();
                    //Tiền thu trực tiếp
                    if (advances.Exists(o => o.PhanLoai == 3))
                    {
                        var tamUngCT = (from tuct in _dataContext.TamUngcts.Where(o => o.Status == 0)
                                        join tu in _dataContext.TamUngs.Where(p => p.MaBNhan == patientId && p.PhanLoai == 3).Where(p => advanceIds.Contains(p.IDTamUng)) on tuct.IDTamUng equals tu.IDTamUng
                                        join dv in _dataContext.DichVus on tuct.MaDV equals dv.MaDV
                                        join ndv in _dataContext.NhomDVs on dv.IDNhom equals ndv.IDNhom
                                        select new TTHD
                                        {
                                            IDNhom = ndv.IDNhom,
                                            TenNhom = ndv.TenNhom,
                                            SoTien = tuct.SoTien,
                                            IDTamUng = tu.IDTamUng
                                        }).ToList();
                        listTTHD.AddRange(tamUngCT);
                    }

                    if (advances.Exists(o => o.PhanLoai == 1))
                    {
                        var advanceId = advances.FirstOrDefault(o => o.PhanLoai == 1).IDTamUng;

                        // Tính tiền không thu trực tiếp
                        var vpCT_TT = (from vp in _dataContext.VienPhis.Where(o => o.MaBNhan == patientId)
                                       join vpct in _dataContext.VienPhicts.Where(o => o.ThanhToan == 0) on vp.idVPhi equals vpct.idVPhi
                                       join dv in _dataContext.DichVus on vpct.MaDV equals dv.MaDV
                                       join ndv in _dataContext.NhomDVs on dv.IDNhom equals ndv.IDNhom
                                       select new TTHD
                                       {
                                           IDNhom = ndv.IDNhom,
                                           TenNhom = ndv.TenNhom,
                                           SoTien = vpct.TienBN,
                                           IDTamUng = advanceId
                                       }).ToList();

                        listTTHD.AddRange(vpCT_TT);
                    }

                    //Tiền vận chuyển tính thuế
                    var tienvanchuyen = (from gr in listTTHD.Where(o => o.IDNhom == 12 && o.SoTien > 0)
                                         group gr by new
                                         {
                                             gr.IDNhom,
                                             gr.TenNhom
                                         } into kq
                                         select new TTHD
                                         {
                                             TenNhom = kq.Key.TenNhom,
                                             IDNhom = kq.Key.IDNhom,
                                             SoTien = kq.Sum(o => o.SoTien),
                                             IsCoThue = true,
                                             DS_IDTamUng = kq.Select(o => o.IDTamUng).Distinct().ToList()
                                         }).ToList();

                    //Tiền không tính thuế
                    var tienkhongtinhthue = (from gr in listTTHD.Where(o => o.IDNhom != 12)
                                             group gr by new { gr.IDNhom, gr.TenNhom } into kq
                                             select new TTHD
                                             {
                                                 TenNhom = dTuong != "BHYT" ? (kq.Key.IDNhom == 4 ? "Tiền thuốc sử dụng" : (kq.Key.IDNhom == 10 ? "Tiền vật tư y tế" : kq.Key.TenNhom)) : kq.Key.TenNhom,
                                                 IDNhom = kq.Key.IDNhom,
                                                 SoTien = kq.Sum(o => o.SoTien),
                                                 IsCoThue = dTuong == "BHYT" ? false : true
                                             }
                                        ).ToList();

                    List<List<TTHD>> listIn = new List<List<TTHD>>();
                    listIn.Add(tienvanchuyen);
                    listIn.Add(tienkhongtinhthue);

                    bool success = false;

                    foreach (var ds in listIn)
                    {
                        if (ds.Count > 0)
                        {
                            success = true;
                            string taxCode = Bien.xmlFilePath_LIS[17];

                            InvoiceInfo objInvoice = new InvoiceInfo();
                            objInvoice.transactionUuid = System.Guid.NewGuid().ToString();
                            objInvoice.invoiceType = Bien.xmlFilePath_LIS[21]; //Mã loại hóa đơn chỉ nhận các giá trị sau: 01GTKT, 02GTTT, 07KPTQ, 03XKNB, 04HGDL. tuân thủ theo quy định ký hiệu loại hóa đơn của Thông tư hướng dẫn thi hành nghị định số 51/2010/NĐ-CP
                            objInvoice.templateCode = Bien.xmlFilePath_LIS[22];//Mã mẫu hóa đơn, tuân thủ theo quy định ký hiệu mẫu hóa đơn của Thông tư hướng dẫn thi hành nghị định số 51/2010/NĐ-CP

                            if (ds.First().IsCoThue)
                                objInvoice.invoiceSeries = khCoThue;
                            else
                                objInvoice.invoiceSeries = khKhongThue;//Là “Ký hiệu hóa đơn” tuân thủ theo quy tắc tạo ký hiệu hóa đơn của Thông tư hướng 
                            //dẫn thi hành nghị định số 51/2010/NĐ-CP. 
                            //Ví dụ AA/16E.
                            //Chú ý: Nếu trường hợp nhập invoiceSeries thì hệ thống sẽ lấy theo dữ liệu truyền vào, nếu không nhập invoiceSeries thì hệ thống sẽ lấy ký hiệu hóa đơn đang được phát hành theo mẫu hóa đơn. Đối với hóa đơn có nhiều dải thì dữ liệu invoiceSeries là yêu cầu bắt buộc
                            //DateTime a = DateTime.UtcNow;
                            //DateTime b = tttu.NgayThu.Value;
                            objInvoice.invoiceIssuedDate = ((Int64)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds);//((Int64)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds).ToString();//Ngày lập hóa đơn quy đổi ra số (time in miliseconds) được thiết lập theo Nghị định 51/2010/NĐ-CP.
                            //Hoặc 1 số định dạng như sau: 
                            //"yyyy-MM-dd'T'HH:mm:ss.SSSZ", "yyyy-MM-dd'T'HH:mm:ss.SSS'Z'", "EEE, dd MMM yyyy HH:mm:ss zzz", "yyyy-MM-dd")
                            objInvoice.currencyCode = "VND";
                            objInvoice.adjustmentType = "1";// "1";//Trạng thái điều chỉnh hóa đơn: 1: Hóa đơn gốc 3: Hóa đơn thay thế 5: Hóa đơn điều chỉnh
                            objInvoice.paymentStatus = true;//True: Đã thanh toán
                            objInvoice.paymentType = "TM";//Loại hình thức thanh toán: Bao gồm: 
                            //CK – Chuyển khoản
                            //DTCN – Đối trừ công nợ
                            //TM – tiền mặt
                            //TM/CK – Tiền mặt/ Chuyển khoản
                            //Nếu paymentStatus = true  thì bắt buộc phải truyền
                            if (additionalInformation.HTThanhToan == 1)
                                objInvoice.paymentTypeName = "CK";
                            else
                                objInvoice.paymentTypeName = "TM";//Tên phương thức thanh toán
                            //CK – Chuyển khoản
                            //DTCN – Đối trừ công nợ
                            //TM – tiền mặt
                            //TM/CK – Tiền mặt/ Chuyển khoản
                            //Nếu paymentStatus = true  thì bắt buộc phải truyền
                            objInvoice.cusGetInvoiceRight = true;
                            objInvoice.buyerIdNo = "";//Số giấy tờ của khách hàng
                            objInvoice.buyerIdType = "";//Loại giấy tờ của khách hàng:
                            //-	1: Số CMND
                            //-	3: Giấy phép kinh doanh
                            //-	2: Hộ chiếu

                            BuyerInfo objBuyer = new BuyerInfo();
                            if (!string.IsNullOrEmpty(additionalInformation.TenDonVi))
                                objBuyer.buyerAddressLine = additionalInformation.DiaChiNoiLV ?? string.Empty;
                            else
                                objBuyer.buyerAddressLine = patient.DChi ?? string.Empty;//"HN VN";//Địa chỉ bưu điện người mua 
                            objBuyer.buyerIdNo = objInvoice.buyerIdNo ?? string.Empty;//Số giấy tờ của khách hàng
                            objBuyer.buyerIdType = objInvoice.buyerIdType ?? string.Empty;//Loại giấy tờ của khách hàng:
                            //-	1: Số CMND
                            //-	3: Giấy phép kinh doanh
                            //-	2: Hộ chiếu
                            objBuyer.buyerName = patient.TenBNhan + " (Mã số BN: " + patient.MaBNhan + ")";
                            objBuyer.buyerPhoneNumber = additionalInformation.DThoai ?? string.Empty;//Điện thoại
                            objBuyer.buyerEmail = "";//Email
                            objBuyer.buyerLegalName = additionalInformation.TenDonVi ?? "";//Tên (đăng ký kinh doanh trong trường hợp là doanh nghiệp) của người mua
                            objBuyer.buyerTaxCode = additionalInformation.MaSoThue ?? "";//Mã số thuế
                            objBuyer.buyerBankName = additionalInformation.TenNganHang ?? string.Empty;//Tên ngân hàng
                            objBuyer.buyerBankAccount = additionalInformation.SoTaiKhoan ?? string.Empty;//Số tài khoản

                            SellerInfo objSeller = new SellerInfo();
                            if (system != null)
                            {
                                objSeller.sellerBankAccount = system.SoTaiKhoan ?? string.Empty;//Tài khoản ngân hàng của người bán
                                objSeller.sellerBankName = system.TenNganHang ?? string.Empty;//Tên ngân hàng
                                objSeller.sellerEmail = system.Email ?? string.Empty;//Email
                                objSeller.sellerFaxNumber = system.Fax ?? string.Empty;
                                objSeller.sellerWebsite = system.Website ?? string.Empty;
                            }
                            objSeller.sellerAddressLine = Bien.DiaChi ?? string.Empty;//"HN VN";//Địa chỉ bưu điện người bán
                            objSeller.sellerLegalName = Bien.TenCQ ?? string.Empty;//Tên (đăng ký kinh doanh trong trường hợp là doanh nghiệp) của người bán
                            objSeller.sellerPhoneNumber = Bien.SDTCQ ?? string.Empty;
                            objSeller.sellerTaxCode = taxCode ?? string.Empty;//Mã số thuế người bán được cấp bởi TCT Việt Nam. Mẫu 1: 0312770607 Mẫu 2: 0312770607-001

                            List<ItemInfo> lstItem = new List<ItemInfo>();
                            double tongtien = 0;
                            int line = 1;

                            foreach (var detail in ds)
                            {
                                tongtien += detail.SoTien;

                                ItemInfo item = new ItemInfo();
                                int sotien = (int)Math.Round(detail.SoTien, 0);
                                item.discount = 0;//"0.0";//giảm giá
                                item.itemCode = "";//Mã hàng hóa, dịch vụ
                                item.itemDiscount = 0;//"0.0";
                                item.itemName = detail.TenNhom;//"Tiền viện phí";
                                item.itemTotalAmountWithoutTax = sotien;
                                item.lineNumber = line;
                                item.quantity = 1;//"1";//Số lượng
                                item.taxAmount = 0;//"0.0";//Tổng tiền thuế
                                item.taxPercentage = 0;//"0";//Thuế suất của hàng hóa, dịch vụ 
                                item.unitName = "Lần";
                                item.unitPrice = sotien;

                                lstItem.Add(item);
                                line++;
                            }

                            int tongtienStr = (int)Math.Round(tongtien, 0);

                            SummarizeInfo objSummary = new SummarizeInfo();
                            objSummary.discountAmount = 0;//"0";//Tổng tiền chiết khấu thương mại trên toàn hóa đơn trước khi tính thuế. Chú ý: Khi tính chiết khấu, toàn hóa đơn chỉ sử dụng một mức thuế
                            objSummary.settlementDiscountAmount = 0;// "0";//Tổng tiền chiết khấu thanh toán trên toàn hóa đơn sau khi tính thuế. Chú ý: Khi tính chiết khấu, toàn hóa đơn chỉ sử dụng một mức thuế.
                            objSummary.sumOfTotalLineAmountWithoutTax = tongtienStr;//Tổng thành tiền cộng gộp của tất cả các dòng hóa đơn chưa bao gồm VAT.
                            //-	Hóa đơn thường: Tổng tiền HHDV trên các dòng HĐ. 
                            //-	Hóa đơn điều chỉnh: Tổng tiền điều chỉnh của các dòng HĐ.

                            //Vận chuyển thì thuế = 0, các dv khác thì k tính thuế
                            if (ds.FirstOrDefault().IDNhom == 12)
                                objSummary.taxPercentage = 0;//"0";
                            else
                                objSummary.taxPercentage = -2;//"-2";//Mức thuế: khai báo giá trị như sau 
                            //-	0%: 0 
                            //-	5%: 5 
                            //-	10%: 10 
                            //-	không phải kê khai và tính thuế GTGT: -1
                            //-	Không chịu thuế:  -2 

                            objSummary.totalAmountWithoutTax = tongtienStr;//Tổng tiền hóa đơn chưa bao gồm VAT. 
                            //-	Hóa đơn thường: Tổng tiền HHDV trên các dòng HĐ và các khoản tăng/giảm khác trên toàn HĐ. 
                            //-	Hóa đơn điều chỉnh: Tổng tiền điều chỉnh của các dòng HĐ và các khoản tăng/giảm khác trên toàn HĐ.

                            objSummary.totalAmountWithTax = tongtienStr;//Tổng tiền trên hóa đơn đã bao gồm VAT.
                            //-	Hóa đơn thường: Tổng tiền HHDV trên các dòng HĐ và các khoản tăng/giảm khác trên toàn HĐ đã bao gồm cả VAT.
                            //-	Hóa đơn điều chỉnh: Tổng tiền điều chỉnh của các dòng HĐ và các khoản tăng/giảm khác trên toàn HĐ đã bao gồm cả VAT

                            objSummary.totalAmountWithTaxInWords = "";// NumberUtil.DocSoThanhChu(objSummary.totalAmountWithTax);
                            objSummary.totalTaxAmount = 0;//"0";//Tổng tiền thuế trên toàn hóa đơn.
                            //-	Hóa đơn thường: Tổng tiền VAT trên các dòng HĐ và các khoản thuế khác trên toàn HĐ.
                            //-	Hóa đơn điều chỉnh: Tổng tiền VAT điều chỉnh của các dòng HĐ và các khoản tăng/giảm VAT khác trên toàn HĐ.

                            Invoice invoice = new Invoice()
                            {
                                generalInvoiceInfo = objInvoice,
                                buyerInfo = objBuyer,
                                sellerInfo = objSeller,
                                extAttribute = new List<ExtAttribute>(),
                                payments = new List<Payment>()
                                {
                                    new Payment()
                                    {
                                        paymentMethodName = objInvoice.paymentTypeName,
                                    }
                                },
                                deliveryInfo = new DeliveryInfo(),
                                itemInfo = lstItem,
                                discountItemInfo = new List<DiscountItemInfo>(),
                                meterReading = new List<MeterReading>()
                                {
                                    new MeterReading()
                                    {
                                        previousIndex = "5454",
                                        currentIndex = "244",
                                        factor = "22",
                                        amount = "2"
                                    },
                                    new MeterReading()
                                    {
                                        previousIndex = "44",
                                        currentIndex = "44",
                                        factor = "33",
                                        amount = "3"
                                    }
                                },
                                summarizeInfo = objSummary,
                                taxBreakdowns = new List<TaxBreakdowns>()
                                {
                                    new TaxBreakdowns()
                                    {
                                        taxPercentage = objSummary.taxPercentage,
                                        taxableAmount = objSummary.totalTaxAmount,
                                        taxAmount = objSummary.totalTaxAmount,
                                    }
                                }
                            };

                            string userpass = Bien.xmlFilePath_LIS[18];
                            string url = Bien.xmlFilePath_LIS[19];

                            // Tao hoa don
                            var result = new Utilities.Commons.ResultApi<ResultCreateInvoice>();
                            if (url.Contains("sinvoice"))
                                result = Invoices.Invoice.CreateInvoiceV1(url, Security.Base64Encode(userpass), taxCode, invoice);
                            else
                                result = Invoices.Invoice.CreateInvoiceV2(url, userpass, taxCode, invoice);

                            if (result.Message != null)
                            {
                                MessageBox.Show(result.Message);

                                LOG log = new LOG()
                                {
                                    DateLog = DateTime.Now,
                                    LyDo = string.Format("Tạo hóa đơn điện tử Viettel thất bại. Chi tiết lỗi: {0}", result.Message),
                                    UserName = Bien.TenDN,
                                    MaBNhan = patientId,
                                    IdForm = 904,
                                    MaCB = Bien.MaCB,
                                    ComputerName = SystemInformation.ComputerName,
                                    Status = 1,
                                };
                                _dataContext.LOGs.Add(log);
                                _dataContext.SaveChanges();

                                foreach (var advance in advances)
                                {
                                    advance.StatusHD = 3;
                                }

                                _dataContext.SaveChanges();

                                return;
                            }
                            else
                            {
                                foreach (var advance in advances)
                                {
                                    advance.MaHD = advance.MaHD + result.Result.Result.InvoiceNo + "|";
                                    advance.transactionID = advance.transactionID + result.Result.Result.TransactionId + "|";
                                    advance.reservationCode = advance.reservationCode + result.Result.Result.ReservationCode + "|";
                                    advance.templateCode = advance.templateCode + objInvoice.templateCode + "|";

                                    string ngayTaoHD = "";

                                    var date = (new DateTime(1970, 1, 1)).AddMilliseconds(objInvoice.invoiceIssuedDate);
                                    if (date != null)
                                    {
                                        DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.Local);
                                        ngayTaoHD = cstTime.ToString("yyyyMMddHHmmss");
                                    }
                                    else
                                        ngayTaoHD = DateTime.Now.ToString("yyyyMMddHHmmss");

                                    advance.NgayTaoHD = ngayTaoHD;
                                    advance.MaCBTaoHD = Bien.MaCB;
                                    advance.StatusHD = 2;

                                    _dataContext.SaveChanges();
                                }

                                MessageBox.Show("Tạo hóa đơn thành công");

                                LOG log = new LOG()
                                {
                                    DateLog = DateTime.Now,
                                    LyDo = string.Format("Tạo hóa đơn điện tử Viettel thất bại. Chi tiết lỗi: {0}", result.Message),
                                    UserName = Bien.TenDN,
                                    MaBNhan = patientId,
                                    IdForm = 904,
                                    MaCB = Bien.MaCB,
                                    ComputerName = SystemInformation.ComputerName,
                                    Status = 1,
                                };

                                _dataContext.LOGs.Add(log);
                                _dataContext.SaveChanges();
                            }
                        }
                    }

                    if (!success)
                    {
                        rollBack = true;
                        MessageBox.Show("Không có dữ liệu tạo hóa đơn");
                    }
                }
            }
            #endregion
        }
        public void ViewInvoice01071(int patientId, List<int> advanceIds)
        {
            BenhNhan patient = _dataContext.BenhNhans.FirstOrDefault(p => p.MaBNhan == patientId);
            TTboXung additionalInformation = _dataContext.TTboXungs.FirstOrDefault(p => p.MaBNhan == patientId);
            HTHONG system = _dataContext.HTHONGs.FirstOrDefault(p => p.MaBV == Bien.MaBV);
            List<TamUng> advances = _dataContext.TamUngs.Where(o => advanceIds.Contains(o.IDTamUng)).ToList();

            if (advances != null && advances.Count > 0)
            {
                #region xem hóa đơn
                if (!string.IsNullOrEmpty(Bien.xmlFilePath_LIS[20]) && Bien.xmlFilePath_LIS[20] == "0")
                {
                    if (!string.IsNullOrEmpty(advances.FirstOrDefault().pathHD))
                    {
                        var pathSplit = advances.FirstOrDefault().pathHD.Split('|');
                        if (pathSplit.Count() > 0)
                        {
                            foreach (var item in pathSplit)
                            {
                                if (!string.IsNullOrWhiteSpace(item) && File.Exists(item))
                                {
                                    frm_ViewPDF frm = new frm_ViewPDF(item);
                                    frm.ShowDialog();
                                }
                            }
                        }

                    }
                    else
                    {
                        string userpass = Bien.xmlFilePath_LIS[18];
                        string url = Bien.xmlFilePath_LIS[19];
                        string taxCode = Bien.xmlFilePath_LIS[17];
                        string pathsave = Bien.xmlFilePath_LIS[16];

                        string path = "";
                        if (!string.IsNullOrWhiteSpace(advances.FirstOrDefault().MaHD) && !string.IsNullOrWhiteSpace(advances.FirstOrDefault().templateCode))
                        {
                            var spMaHD = advances.FirstOrDefault().MaHD.Split('|');
                            var spTemplateCode = advances.FirstOrDefault().templateCode.Split('|');
                            var transactionID = advances.FirstOrDefault().transactionID.Split('|');

                            for (int i = 0; i < spMaHD.Count(); i++)
                            {
                                var ma = spMaHD[i];
                                var tem = spTemplateCode[i];
                                var tran = transactionID[i];

                                if (!string.IsNullOrWhiteSpace(ma))
                                {
                                    // Tao hoa don
                                    var result = new Utilities.Commons.ResultApi<CreaeteFileInvoice>();
                                    if (url.Contains("sinvoice"))
                                        result = QLBV.Invoices.Invoice.ViewInvoiceV1(url, userpass, taxCode, ma, tem, tran, pathsave);
                                    else
                                        result = QLBV.Invoices.Invoice.ViewInvoiceV2(url, userpass, taxCode, ma, tem, tran, pathsave);

                                    if (!string.IsNullOrEmpty(result.Result.FileName) && File.Exists(result.Result.FileName))
                                    {
                                        frm_ViewPDF frm = new frm_ViewPDF(result.Result.FileName);
                                        frm.ShowDialog();

                                        path += result.Result.FileName + "|";
                                    }
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(path))
                        {
                            foreach (var advance in advances)
                            {
                                advance.pathHD = path;
                                _dataContext.SaveChanges();
                            }
                        }
                    }
                }
                #endregion
            }
        }

        public void TaoHD(QLBV_Database.QLBVEntities _data, int id, bool HDThuTT, int idtu)
        {
            BenhNhan ttbn = _dataContext.BenhNhans.Where(p => p.MaBNhan == id).FirstOrDefault();
            TTboXung ttbx = _dataContext.TTboXungs.Where(p => p.MaBNhan == id).FirstOrDefault();
            TamUng tttu = _dataContext.TamUngs.Where(p => p.MaBNhan == id).Where(p => HDThuTT == true ? p.IDTamUng == idtu : p.PhanLoai == 1).FirstOrDefault();

            #region Tạo hóa đơn viettel
            if (!string.IsNullOrEmpty(Bien.xmlFilePath_LIS[20]) && Bien.xmlFilePath_LIS[20] == "0")
            {
                bool kttao = true;
                string _invoiceType = "";
                if (string.IsNullOrEmpty(Bien.xmlFilePath_LIS[21]))
                {
                    kttao = false;
                    MessageBox.Show("Chưa thiết lập loại hóa đơn");
                }
                if (string.IsNullOrEmpty(Bien.xmlFilePath_LIS[22]))
                {
                    kttao = false;
                    MessageBox.Show("Chưa thiết lập mẫu hóa đơn");
                }
                if (string.IsNullOrEmpty(Bien.xmlFilePath_LIS[23]))
                {
                    kttao = false;
                    MessageBox.Show("Chưa thiết lập kí hiệu hóa đơn");
                }

                if (ttbn == null || ttbx == null || tttu == null)
                {
                    kttao = false;
                    MessageBox.Show("Bệnh nhân không đủ điều kiện để lập hóa đơn");
                }
                if (kttao)
                {
                    decimal sotien = (decimal)Math.Round(tttu.SoTien ?? 0, 0);
                    string codeTax = Bien.xmlFilePath_LIS[17];
                    Bien.InvoiceInfo objInvoice = new Bien.InvoiceInfo();
                    objInvoice.transactionUuid = System.Guid.NewGuid().ToString();
                    objInvoice.invoiceType = Bien.xmlFilePath_LIS[21]; //Mã loại hóa đơn chỉ nhận các giá trị sau: 01GTKT, 02GTTT, 07KPTQ, 03XKNB, 04HGDL. tuân thủ theo quy định ký hiệu loại hóa đơn của Thông tư hướng dẫn thi hành nghị định số 51/2010/NĐ-CP
                    objInvoice.templateCode = Bien.xmlFilePath_LIS[22];//Mã mẫu hóa đơn, tuân thủ theo quy định ký hiệu mẫu hóa đơn của Thông tư hướng dẫn thi hành nghị định số 51/2010/NĐ-CP
                    objInvoice.invoiceSeries = Bien.xmlFilePath_LIS[23];//Là “Ký hiệu hóa đơn” tuân thủ theo quy tắc tạo ký hiệu hóa đơn của Thông tư hướng 
                    //dẫn thi hành nghị định số 51/2010/NĐ-CP. 
                    //Ví dụ AA/16E.
                    //Chú ý: Nếu trường hợp nhập invoiceSeries thì hệ thống sẽ lấy theo dữ liệu truyền vào, nếu không nhập invoiceSeries thì hệ thống sẽ lấy ký hiệu hóa đơn đang được phát hành theo mẫu hóa đơn. Đối với hóa đơn có nhiều dải thì dữ liệu invoiceSeries là yêu cầu bắt buộc
                    //DateTime a = DateTime.UtcNow;
                    //DateTime b = tttu.NgayThu.Value;
                    objInvoice.invoiceIssuedDate = ((Int64)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds);//((Int64)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds).ToString();//Ngày lập hóa đơn quy đổi ra số (time in miliseconds) được thiết lập theo Nghị định 51/2010/NĐ-CP.
                    //Hoặc 1 số định dạng như sau: 
                    //"yyyy-MM-dd'T'HH:mm:ss.SSSZ", "yyyy-MM-dd'T'HH:mm:ss.SSS'Z'", "EEE, dd MMM yyyy HH:mm:ss zzz", "yyyy-MM-dd")
                    objInvoice.currencyCode = "VND";
                    objInvoice.adjustmentType = "1";// "1";//Trạng thái điều chỉnh hóa đơn: 1: Hóa đơn gốc 3: Hóa đơn thay thế 5: Hóa đơn điều chỉnh
                    objInvoice.paymentStatus = true;//True: Đã thanh toán
                    objInvoice.paymentType = "TM";//Loại hình thức thanh toán: Bao gồm: 
                    //CK – Chuyển khoản
                    //DTCN – Đối trừ công nợ
                    //TM – tiền mặt
                    //TM/CK – Tiền mặt/ Chuyển khoản
                    //Nếu paymentStatus = true  thì bắt buộc phải truyền
                    objInvoice.paymentTypeName = "TM";//Tên phương thức thanh toán
                    //CK – Chuyển khoản
                    //DTCN – Đối trừ công nợ
                    //TM – tiền mặt
                    //TM/CK – Tiền mặt/ Chuyển khoản
                    //Nếu paymentStatus = true  thì bắt buộc phải truyền
                    objInvoice.cusGetInvoiceRight = true;
                    objInvoice.buyerIdNo = "";//Số giấy tờ của khách hàng
                    objInvoice.buyerIdType = "";//Loại giấy tờ của khách hàng:
                    //-	1: Số CMND
                    //-	3: Giấy phép kinh doanh
                    //-	2: Hộ chiếu
                    Bien.BuyerInfo objBuyer = new Bien.BuyerInfo();
                    if (!string.IsNullOrEmpty(ttbx.TenDonVi) && Bien.MaBV == "24009")
                        objBuyer.buyerAddressLine = ttbx.DiaChiNoiLV;
                    else
                        objBuyer.buyerAddressLine = ttbn.DChi;//"HN VN";//Địa chỉ bưu điện người mua 
                    objBuyer.buyerIdNo = objInvoice.buyerIdNo;//Số giấy tờ của khách hàng
                    objBuyer.buyerIdType = objInvoice.buyerIdType;//Loại giấy tờ của khách hàng:
                    //-	1: Số CMND
                    //-	3: Giấy phép kinh doanh
                    //-	2: Hộ chiếu
                    objBuyer.buyerName = ttbn.TenBNhan.ToUpper();
                    objBuyer.buyerPhoneNumber = ttbx.DThoai;//Điện thoại
                    objBuyer.buyerEmail = "";//Email
                    objBuyer.buyerLegalName = ttbx.TenDonVi;//Tên (đăng ký kinh doanh trong trường hợp là doanh nghiệp) của người mua
                    objBuyer.buyerTaxCode = ttbx.MaSoThue;//Mã số thuế
                    objBuyer.buyerBankName = ttbx.TenNganHang;//Tên ngân hàng
                    objBuyer.buyerBankAccount = ttbx.SoTaiKhoan;//Số tài khoản


                    //seller
                    Bien.SellerInfo objSeller = new Bien.SellerInfo();
                    objSeller.sellerAddressLine = Bien.DiaChi;//"HN VN";//Địa chỉ bưu điện người bán
                    objSeller.sellerBankAccount = "";//Tài khoản ngân hàng của người bán
                    objSeller.sellerBankName = "";
                    objSeller.sellerEmail = "";
                    objSeller.sellerLegalName = Bien.TenCQ;//Tên (đăng ký kinh doanh trong trường hợp là doanh nghiệp) của người bán
                    objSeller.sellerPhoneNumber = Bien.SDTCQ;
                    objSeller.sellerTaxCode = codeTax;//Mã số thuế người bán được cấp bởi TCT Việt Nam. Mẫu 1: 0312770607 Mẫu 2: 0312770607-001
                    List<Bien.ItemInfo> lstItem = new List<Bien.ItemInfo>();
                    Bien.ItemInfo item = new Bien.ItemInfo();
                    item.discount = 0;// "0.0";//giảm giá
                    item.itemCode = "";//Mã hàng hóa, dịch vụ
                    item.itemDiscount = 0;//"0.0";
                    item.itemName = tttu.LyDo;//"Tiền viện phí";
                    item.itemTotalAmountWithoutTax = sotien;
                    item.lineNumber = 1;//"1";
                    item.quantity = 1;//"1";//Số lượng
                    item.taxAmount = 0;//"0.0";//Tổng tiền thuế
                    item.taxPercentage = 0;//"0";//Thuế suất của hàng hóa, dịch vụ 
                    item.unitName = "Lần";
                    item.unitPrice = sotien;
                    lstItem.Add(item);
                    Bien.SummarizeInfo objSummary = new Bien.SummarizeInfo();
                    objSummary.discountAmount = 0;//"0";//Tổng tiền chiết khấu thương mại trên toàn hóa đơn trước khi tính thuế. Chú ý: Khi tính chiết khấu, toàn hóa đơn chỉ sử dụng một mức thuế
                    objSummary.settlementDiscountAmount = 0;//"0";//Tổng tiền chiết khấu thanh toán trên toàn hóa đơn sau khi tính thuế. Chú ý: Khi tính chiết khấu, toàn hóa đơn chỉ sử dụng một mức thuế.
                    objSummary.sumOfTotalLineAmountWithoutTax = sotien;//Tổng thành tiền cộng gộp của tất cả các dòng hóa đơn chưa bao gồm VAT.
                    //-	Hóa đơn thường: Tổng tiền HHDV trên các dòng HĐ. 
                    //-	Hóa đơn điều chỉnh: Tổng tiền điều chỉnh của các dòng HĐ.

                    objSummary.taxPercentage = 0;//"0";//Mức thuế: khai báo giá trị như sau 
                    //-	0%: 0 
                    //-	5%: 5 
                    //-	10%: 10 
                    //-	không phải kê khai và tính thuế GTGT: -1
                    //-	Không chịu thuế:  -2 

                    objSummary.totalAmountWithoutTax = sotien;//Tổng tiền hóa đơn chưa bao gồm VAT. 
                    //-	Hóa đơn thường: Tổng tiền HHDV trên các dòng HĐ và các khoản tăng/giảm khác trên toàn HĐ. 
                    //-	Hóa đơn điều chỉnh: Tổng tiền điều chỉnh của các dòng HĐ và các khoản tăng/giảm khác trên toàn HĐ.

                    objSummary.totalAmountWithTax = sotien;//Tổng tiền trên hóa đơn đã bao gồm VAT.
                    //-	Hóa đơn thường: Tổng tiền HHDV trên các dòng HĐ và các khoản tăng/giảm khác trên toàn HĐ đã bao gồm cả VAT.
                    //-	Hóa đơn điều chỉnh: Tổng tiền điều chỉnh của các dòng HĐ và các khoản tăng/giảm khác trên toàn HĐ đã bao gồm cả VAT

                    objSummary.totalAmountWithTaxInWords = "";// NumberUtil.DocSoThanhChu(objSummary.totalAmountWithTax);
                    objSummary.totalTaxAmount = 0;//"0";//Tổng tiền thuế trên toàn hóa đơn.
                    //-	Hóa đơn thường: Tổng tiền VAT trên các dòng HĐ và các khoản thuế khác trên toàn HĐ.
                    //-	Hóa đơn điều chỉnh: Tổng tiền VAT điều chỉnh của các dòng HĐ và các khoản tăng/giảm VAT khác trên toàn HĐ.

                    //string UserPass, string _codeTax, string url, Bien.Model.InvoiceInfo _objInvoice, Bien.Model.BuyerInfo _BuyerInfo, Bien.Model.SellerInfo _SellerInfo, List<Bien.Model.ItemInfo> _lstItem, Bien.Model.SummarizeInfo _SummarizeInfo
                    string userpass = Bien.xmlFilePath_LIS[18];
                    string url = Bien.xmlFilePath_LIS[19];

                    //string result = DungChung.Ham.CreateInvoice_v2(userpass, codeTax, url, objInvoice, objBuyer, objSeller, lstItem, objSummary, id);
                    var result = DungChung.Ham.CreateInvoice_v1(userpass, codeTax, url, objInvoice, objBuyer, objSeller, lstItem, objSummary);

                    if (result.Contains("Lỗi tạo hóa đơn"))
                    {
                        MessageBox.Show(result);
                    }
                    else
                    {
                        JObject json = JObject.Parse(result);
                        string _erro = (string)json.SelectToken("errorCode");
                        if (!string.IsNullOrEmpty(_erro))
                        {
                            string des = (string)json.SelectToken("description");
                            MessageBox.Show("Lỗi tạo hóa đơn:" + des);
                        }
                        else
                        {
                            string _invoiceNo = (string)json.Last.First.SelectToken("invoiceNo");
                            string _transactionID = (string)json.Last.First.SelectToken("transactionID");
                            string _resevationCode = (string)json.Last.First.SelectToken("reservationCode");
                            tttu.MaHD = _invoiceNo;
                            tttu.transactionID = _transactionID;
                            tttu.reservationCode = _resevationCode;
                            tttu.templateCode = objInvoice.templateCode;
                            tttu.StatusHD = 2;
                            string ngayTaoHD = "";
                            //if (!string.IsNullOrEmpty(objInvoice.invoiceIssuedDate))
                            //{
                            var date = (new DateTime(1970, 1, 1)).AddMilliseconds(double.Parse(objInvoice.invoiceIssuedDate.ToString()));
                            if (date != null)
                            {
                                DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.Local);
                                ngayTaoHD = cstTime.ToString("yyyyMMddHHmmss");
                            }
                            //}
                            else
                            {
                                ngayTaoHD = DateTime.Now.ToString("yyyyMMddHHmmss");
                            }
                            tttu.NgayTaoHD = ngayTaoHD;
                            tttu.MaCBTaoHD = Bien.MaCB;
                            _dataContext.SaveChanges();
                            MessageBox.Show("Tạo hóa đơn thành công");
                            #region Ghi log
                            //    _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
                            LOG moi = new LOG();
                            moi.DateLog = DateTime.Now;
                            moi.LyDo = "Tạo hóa đơn điện tử Viettel thành công";
                            moi.UserName = Bien.TenDN;
                            moi.MaBNhan = id;
                            moi.IdForm = 904;
                            moi.MaCB = Bien.MaCB;
                            moi.ComputerName = SystemInformation.ComputerName;
                            moi.Status = 1;
                            _dataContext.LOGs.Add(moi);
                            _dataContext.SaveChanges();
                            #endregion
                        }
                    }
                }
            }
            #endregion
            #region VNPT
            else
            {
                string UserAdmin = Bien.xmlFilePath_LIS[25];
                string PassAdmin = Bien.xmlFilePath_LIS[26];
                string UserWS = Bien.xmlFilePath_LIS[27];
                string PassWS = Bien.xmlFilePath_LIS[28];
                string PublishAPI = Bien.xmlFilePath_LIS[29];
                string BusinessAPI = Bien.xmlFilePath_LIS[30];
                string PortalAPI = Bien.xmlFilePath_LIS[31];
                string MauHDVNPT = Bien.xmlFilePath_LIS[32];
                string SerialVNPT = Bien.xmlFilePath_LIS[33];
                int DeMo = Convert.ToInt32(Bien.xmlFilePath_LIS[40]);
                bool kttao = true;
                if (string.IsNullOrEmpty(UserAdmin) || string.IsNullOrEmpty(PassAdmin))
                {
                    kttao = false;
                    MessageBox.Show("Chưa có tài khoản Admin");
                }
                if (string.IsNullOrEmpty(UserWS) || string.IsNullOrEmpty(PassWS))
                {
                    kttao = false;
                    MessageBox.Show("Chưa có tài khoản Service");
                }
                //if (string.IsNullOrEmpty(PublishAPI) || string.IsNullOrEmpty(BusinessAPI) || string.IsNullOrEmpty(PortalAPI))
                //{
                //    kttao = false;
                //    MessageBox.Show("Chưa nhập đầy đủ Link Webservice");
                //}
                if (string.IsNullOrEmpty(MauHDVNPT) || string.IsNullOrEmpty(SerialVNPT))
                {
                    kttao = false;
                    MessageBox.Show("Chưa nhập đầy đủ thông tin mẫu đơn");
                }
                if (ttbn == null || ttbx == null || tttu == null)
                {
                    kttao = false;
                    MessageBox.Show("Bệnh nhân không đủ điều kiện để lập hóa đơn");

                }
                if (kttao)
                {
                    string xmlstring = DungChung.Ham.getDemoInvPublishData(ttbn, ttbx, tttu);
                    //string xmlstring_tt78 = DungChung.Ham.getDemoInvPublishData_TT78(ttbn, ttbx, tttu);
                    string res = "";
                    if (DeMo == 1)// cong that
                    {
                        if (Bien.MaBV == "30005")
                        {
                            InvPublishService.PublishService puS = new InvPublishService.PublishService();
                            res = puS.ImportAndPublishInv(UserAdmin, PassAdmin, xmlstring, UserWS, PassWS, MauHDVNPT, SerialVNPT, 0);
                        }
                        else if (Bien.MaBV == "30012")
                        {
                            InvPublishServiceCongThat_30012_TT78.PublishService puS = new InvPublishServiceCongThat_30012_TT78.PublishService();
                            res = puS.ImportAndPublishInv(UserAdmin, PassAdmin, xmlstring, UserWS, PassWS, MauHDVNPT, SerialVNPT, 0);
                        }
                        else if (Bien.MaBV == "27022")
                        {
                            InvPublishServiceCongThat_27022_TT78.PublishService puS = new InvPublishServiceCongThat_27022_TT78.PublishService();
                            res = puS.ImportAndPublishInv(UserAdmin, PassAdmin, xmlstring, UserWS, PassWS, MauHDVNPT, SerialVNPT, 0);
                        }
                        else if (Bien.MaBV == "30003")
                        {
                            InvPublishServiceCongThat30003_TT78.PublishService puS = new InvPublishServiceCongThat30003_TT78.PublishService();
                            res = puS.ImportAndPublishInv(UserAdmin, PassAdmin, xmlstring, UserWS, PassWS, MauHDVNPT, SerialVNPT, 0);
                        }
                        else if (Bien.MaBV == "30007")
                        {
                            InvPublishService30007.PublishService puS = new InvPublishService30007.PublishService();
                            res = puS.ImportAndPublishInv(UserAdmin, PassAdmin, xmlstring, UserWS, PassWS, MauHDVNPT, SerialVNPT, 0);
                        }
                        else
                        {
                            MessageBox.Show("Chưa thiết lập webservice hóa đơn");
                            return;
                        }
                    }
                    else//cong demo
                    {
                        if (Bien.MaBV == "30005")
                        {
                            //InvPublishServicedemo.PublishService puS = new InvPublishServicedemo.PublishService();
                            InvPublishServicedemo.PublishService puS = new InvPublishServicedemo.PublishService();
                            res = puS.ImportAndPublishInv(UserAdmin, PassAdmin, xmlstring, UserWS, PassWS, MauHDVNPT, SerialVNPT, 0);
                        }
                        else if (Bien.MaBV == "30003")
                        {
                            InvPublishServicedemo30003_TT78.PublishService puS = new InvPublishServicedemo30003_TT78.PublishService();
                            res = puS.ImportAndPublishInv(UserAdmin, PassAdmin, xmlstring, UserWS, PassWS, MauHDVNPT, SerialVNPT, 0);
                        }
                        else if (Bien.MaBV == "30007")
                        {
                            InvPublishServiceDemo30007.PublishService puS = new InvPublishServiceDemo30007.PublishService();
                            res = puS.ImportInv(xmlstring, UserWS, PassWS, 0);
                            //res = puS.ImportAndPublishInv(UserAdmin, PassAdmin, xmlstring, UserWS, PassWS, MauHDVNPT, SerialVNPT, 0);
                        }
                        else if (Bien.MaBV == "30012")
                        {
                            InvPublishServicedemo_30012_TT78.PublishService puS = new InvPublishServicedemo_30012_TT78.PublishService();
                            res = puS.ImportAndPublishInv(UserAdmin, PassAdmin, xmlstring, UserWS, PassWS, MauHDVNPT, SerialVNPT, 0);
                        }
                        else if (Bien.MaBV == "27022")
                        {
                            InvPublishService27022.PublishService puS = new InvPublishService27022.PublishService();
                            //res = puS.ImportInv(xmlstring, UserWS, PassWS, 0);
                            res = puS.ImportAndPublishInv(UserAdmin, PassAdmin, xmlstring, UserWS, PassWS, MauHDVNPT, SerialVNPT, 0);
                        }
                        else
                        {
                            MessageBox.Show("Chưa thiết lập webservice hóa đơn");
                            return;
                        }
                    }
                    if (res.Substring(0, 2).ToLower() == "ok")
                    {
                        String Pattern = "";
                        String Serial = "";
                        String firstFkey = "";
                        String firstNo = "";
                        String billInfo = res.Substring(3);
                        String[] billInfoArr = billInfo.Split('-');
                        if (billInfoArr.Length == 2)
                        {
                            String[] billHTokenArr = billInfoArr[0].Split(';');
                            if (billHTokenArr.Length == 2)
                            {
                                Pattern = billHTokenArr[0];
                                Serial = billHTokenArr[1];
                            }

                            string[] BillPrivArr = billInfoArr[1].Split(',');

                            if (BillPrivArr.Length > 0)
                            {
                                String[] firstArr = BillPrivArr[0].Split('_');
                                if (firstArr.Length == 2)
                                {
                                    firstFkey = firstArr[0];
                                    firstNo = firstArr[1];
                                }
                                else if (firstArr.Length == 1)
                                {
                                    firstFkey = firstArr[0];
                                }
                            }

                        }
                        string token = Pattern + ";" + Serial + ";" + firstNo;
                        tttu.FkeyVNPT = firstFkey;
                        tttu.TokenVNPT = token;
                        _dataContext.SaveChanges();
                        MessageBox.Show("Tạo hóa đơn thành công");
                        #region Ghi log
                        //    _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
                        LOG moi = new LOG();
                        moi.DateLog = DateTime.Now;
                        moi.LyDo = "Tạo hóa đơn điện tử VNPT thành công";
                        moi.UserName = Bien.TenDN;
                        moi.MaBNhan = id;
                        moi.IdForm = 904;
                        moi.MaCB = Bien.MaCB;
                        moi.ComputerName = SystemInformation.ComputerName;
                        moi.Status = 1;
                        _dataContext.LOGs.Add(moi);
                        _dataContext.SaveChanges();
                        #endregion
                    }
                    else
                    {
                        MessageBox.Show((res));
                    }
                }
            }
            #endregion
        }

        public void XemHD(QLBV_Database.QLBVEntities _data, int id, bool HDThuTT, int idtu)
        {
            BenhNhan ttbn = _dataContext.BenhNhans.Where(p => p.MaBNhan == id).FirstOrDefault();
            TTboXung ttbx = _dataContext.TTboXungs.Where(p => p.MaBNhan == id).FirstOrDefault();
            TamUng tttu = _dataContext.TamUngs.Where(p => p.MaBNhan == id).Where(p => HDThuTT == true ? p.IDTamUng == idtu : p.PhanLoai == 1).FirstOrDefault();

            #region xem hóa đơn
            if (!string.IsNullOrEmpty(Bien.xmlFilePath_LIS[20]) && Bien.xmlFilePath_LIS[20] == "0")
            {
                if (!string.IsNullOrEmpty(tttu.pathHD))
                {
                    FileInfo file = new FileInfo(tttu.pathHD);
                    if (file.Exists)
                    {
                        Process.Start(tttu.pathHD);
                    }
                }
                else
                {
                    string userpass = Bien.xmlFilePath_LIS[18];
                    string url = Bien.xmlFilePath_LIS[19];
                    string codeTax = Bien.xmlFilePath_LIS[17];
                    string pathsave = Bien.xmlFilePath_LIS[16];

                    string path = DungChung.Ham.ViewHoaDon_v2(userpass, codeTax, url, tttu.MaHD, tttu.templateCode, tttu.transactionID, pathsave);
                    if (!string.IsNullOrEmpty(path))
                    {
                        tttu.pathHD = path;
                        _dataContext.SaveChanges();
                        FileInfo file = new FileInfo(path);
                        if (file.Exists)
                        {
                            Process.Start(path);
                        }
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(tttu.pathHD))
                {
                    FileInfo file = new FileInfo(tttu.pathHD);
                    if (file.Exists)
                    {
                        Process.Start(tttu.pathHD);
                    }
                }
                else
                {
                    string _fkey = tttu.FkeyVNPT;
                    string UserWS = Bien.xmlFilePath_LIS[27];
                    string PassWS = Bien.xmlFilePath_LIS[28];
                    string pathsave = Bien.xmlFilePath_LIS[24];
                    int DeMo = Convert.ToInt32(Bien.xmlFilePath_LIS[40]);
                    string InvObj = "";
                    try
                    {
                        if (_fkey.Length > 3)
                        {
                            if (DeMo == 1)//cong that
                            {
                                if (Bien.MaBV == "30005")
                                {
                                    InvPortalService.PortalService poS = new InvPortalService.PortalService();
                                    InvObj = poS.downloadInvPDFFkey(_fkey, UserWS, PassWS);
                                }
                                else if (Bien.MaBV == "30012")
                                {
                                    InvPortalServiceCongThat_30012_TT78.PortalService poS = new InvPortalServiceCongThat_30012_TT78.PortalService();
                                    InvObj = poS.downloadInvPDFFkey(_fkey, UserWS, PassWS);
                                }
                                else if (Bien.MaBV == "30007")
                                {
                                    InvPortalService30007.PortalService poS = new InvPortalService30007.PortalService();
                                    InvObj = poS.downloadInvPDFFkey(_fkey, UserWS, PassWS);
                                }
                                else if (Bien.MaBV == "30003")
                                {
                                    InvPotalServiceCongThat_30003_TT78.PortalService poS = new InvPotalServiceCongThat_30003_TT78.PortalService();
                                    InvObj = poS.downloadInvPDFFkey(_fkey, UserWS, PassWS);
                                }
                                else if (Bien.MaBV == "27022")
                                {
                                    InvPotalServiceCongThat_30003_TT78.PortalService poS = new InvPotalServiceCongThat_30003_TT78.PortalService();
                                    InvObj = poS.downloadInvPDFFkey(_fkey, UserWS, PassWS);
                                }
                                else
                                {
                                    MessageBox.Show("Chưa thiết lập webservice hóa đơn");
                                    return;
                                }
                            }
                            else// cong demo
                            {
                                if (Bien.MaBV == "30005")
                                {
                                    InvPortalServicedemo.PortalService poS = new InvPortalServicedemo.PortalService();
                                    InvObj = poS.downloadInvPDFFkey(_fkey, UserWS, PassWS);
                                }
                                else if (Bien.MaBV == "30003")
                                {
                                    InvPotalServicedemo_30003_TT78.PortalService poS = new InvPotalServicedemo_30003_TT78.PortalService();
                                    InvObj = poS.downloadInvPDFFkey(_fkey, UserWS, PassWS);
                                }
                                else if (Bien.MaBV == "30012")
                                {
                                    InvPortalServicedemo_30012_TT78.PortalService poS = new InvPortalServicedemo_30012_TT78.PortalService();
                                    InvObj = poS.downloadInvPDFFkey(_fkey, UserWS, PassWS);
                                }
                                else if (Bien.MaBV == "30007")
                                {
                                    InvPortalServiceDemo30007.PortalService poS = new InvPortalServiceDemo30007.PortalService();
                                    InvObj = poS.downloadInvPDFFkey(_fkey, UserWS, PassWS);
                                }
                                else if (Bien.MaBV == "27022")
                                {
                                    InvPortalService27022.PortalService poS = new InvPortalService27022.PortalService();
                                    InvObj = poS.downloadInvPDFFkey(_fkey, UserWS, PassWS);
                                }
                                else
                                {
                                    MessageBox.Show("Chưa thiết lập webservice hóa đơn");
                                    return;
                                }
                            }

                            byte[] bytes = Convert.FromBase64String(InvObj);
                            String filepath = pathsave + @"\" + _fkey + ".pdf";
                            if (!System.IO.File.Exists(filepath))
                            {
                                System.IO.FileStream stream = new System.IO.FileStream(filepath, System.IO.FileMode.CreateNew);
                                System.IO.BinaryWriter writer = new System.IO.BinaryWriter(stream);
                                writer.Write(bytes, 0, bytes.Length);
                                writer.Close();
                            }
                            tttu.pathHD = filepath;
                            _dataContext.SaveChanges();
                            System.Diagnostics.Process.Start(filepath);

                        }
                    }
                    catch
                    {
                        MessageBox.Show(checkRes(InvObj));
                    }
                }
            }
            #endregion
        }

        public class TTHD
        {
            public List<int> DS_IDTamUng { get; set; }
            public int IDTamUng { get; set; }
            public int IDNhom { get; set; }
            public string TenNhom { get; set; }
            public double SoTien { get; set; }
            public bool IsCoThue { get; set; }
        }

        public void TaoHD_01071(QLBV_Database.QLBVEntities _data, int id, List<int> idtu, ref bool rollBack)
        {
            TTboXung bs = _dataContext.TTboXungs.FirstOrDefault(p => p.MaBNhan == id);
            if (bs != null && bs.HTThanhToan == 1)
            {
                frm_NhapTTHD frm = new frm_NhapTTHD(id);
                frm.ShowDialog();
            }
            //    _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
            BenhNhan ttbn = _dataContext.BenhNhans.Where(p => p.MaBNhan == id).FirstOrDefault();
            TTboXung ttbx = _dataContext.TTboXungs.Where(p => p.MaBNhan == id).FirstOrDefault();
            HTHONG heThong = _dataContext.HTHONGs.FirstOrDefault(p => p.MaBV == Bien.MaBV);
            string khCoThue = "";
            string khKhongThue = "";
            #region Tạo hóa đơn viettel
            if (!string.IsNullOrEmpty(Bien.xmlFilePath_LIS[20]) && Bien.xmlFilePath_LIS[20] == "0")
            {
                bool kttao = true;
                string _invoiceType = "";
                if (string.IsNullOrEmpty(Bien.xmlFilePath_LIS[21]))
                {
                    kttao = false;
                    MessageBox.Show("Chưa thiết lập loại hóa đơn");
                }
                if (string.IsNullOrEmpty(Bien.xmlFilePath_LIS[22]))
                {
                    kttao = false;
                    MessageBox.Show("Chưa thiết lập mẫu hóa đơn");
                }
                if (string.IsNullOrEmpty(Bien.xmlFilePath_LIS[23]))
                {
                    kttao = false;
                    MessageBox.Show("Chưa thiết lập kí hiệu hóa đơn");
                }
                if (!string.IsNullOrEmpty(Bien.xmlFilePath_LIS[23]))
                {
                    //MessageBox.Show("Ký hiệu hóa đơn thiết lập không đúng định dạng");
                    var splitKH = Bien.xmlFilePath_LIS[23].Split(':');
                    if (splitKH.Count() != 2)
                    {
                        kttao = false;
                        MessageBox.Show("Ký hiệu hóa đơn thiết lập không đúng định dạng");
                    }
                    else
                    {
                        khCoThue = splitKH[0];
                        khKhongThue = splitKH[1];
                        if (string.IsNullOrWhiteSpace(khCoThue) || string.IsNullOrWhiteSpace(khKhongThue))
                        {
                            kttao = false;
                            MessageBox.Show("Ký hiệu hóa đơn thiết lập không đúng định dạng");
                        }

                    }
                }

                if (ttbn == null || ttbx == null)
                {
                    kttao = false;
                    MessageBox.Show("Bệnh nhân không đủ điều kiện để lập hóa đơn");

                }

                if (!kttao) rollBack = true;
                if (kttao)
                {
                    var tttu = _dataContext.TamUngs.Where(p => p.MaBNhan == id && (p.PhanLoai == 1 || p.PhanLoai == 3)).Where(p => idtu.Contains(p.IDTamUng)).ToList();
                    var dTuong = _dataContext.BenhNhans.FirstOrDefault(o => o.MaBNhan == id).DTuong;

                    List<TTHD> listTTHD = new List<TTHD>();
                    //Tiền thu trực tiếp
                    if (tttu.Exists(o => o.PhanLoai == 3))
                    {
                        var tamUngCT = (from tuct in _dataContext.TamUngcts.Where(o => o.Status == 0)
                                        join tu in _dataContext.TamUngs.Where(p => p.MaBNhan == id && p.PhanLoai == 3).Where(p => idtu.Contains(p.IDTamUng)) on tuct.IDTamUng equals tu.IDTamUng
                                        join dv in _dataContext.DichVus on tuct.MaDV equals dv.MaDV
                                        join ndv in _dataContext.NhomDVs on dv.IDNhom equals ndv.IDNhom
                                        select new TTHD { IDNhom = ndv.IDNhom, TenNhom = ndv.TenNhom, SoTien = tuct.SoTien, IDTamUng = tu.IDTamUng }
                                          ).ToList();
                        listTTHD.AddRange(tamUngCT);
                    }

                    if (tttu.Exists(o => o.PhanLoai == 1))
                    {
                        var idTamUng = tttu.FirstOrDefault(o => o.PhanLoai == 1).IDTamUng;
                        //Tính tiền không thu trực tiếp
                        var vpCT_TT = (from vp in _dataContext.VienPhis.Where(o => o.MaBNhan == id)
                                       join vpct in _dataContext.VienPhicts.Where(o => o.ThanhToan == 0) on vp.idVPhi equals vpct.idVPhi
                                       join dv in _dataContext.DichVus on vpct.MaDV equals dv.MaDV
                                       join ndv in _dataContext.NhomDVs on dv.IDNhom equals ndv.IDNhom
                                       select new TTHD { IDNhom = ndv.IDNhom, TenNhom = ndv.TenNhom, SoTien = vpct.TienBN, IDTamUng = idTamUng }
                                          ).ToList();

                        listTTHD.AddRange(vpCT_TT);
                    }

                    //Tiền vận chuyển tính thuế
                    var tienvanchuyen = (from gr in listTTHD.Where(o => o.IDNhom == 12 && o.SoTien > 0)
                                         group gr by new { gr.IDNhom, gr.TenNhom } into kq
                                         select new TTHD { TenNhom = kq.Key.TenNhom, IDNhom = kq.Key.IDNhom, SoTien = kq.Sum(o => o.SoTien), IsCoThue = true, DS_IDTamUng = kq.Select(o => o.IDTamUng).Distinct().ToList() }
                                        ).ToList();

                    //Tiền không tính thuế
                    var tienkhongtinhthue = (from gr in listTTHD.Where(o => o.IDNhom != 12)
                                             group gr by new { gr.IDNhom, gr.TenNhom } into kq
                                             select new TTHD { TenNhom = dTuong != "BHYT" ? (kq.Key.IDNhom == 4 ? "Tiền thuốc sử dụng" : (kq.Key.IDNhom == 10 ? "Tiền vật tư y tế" : kq.Key.TenNhom)) : kq.Key.TenNhom, IDNhom = kq.Key.IDNhom, SoTien = kq.Sum(o => o.SoTien), IsCoThue = (dTuong == "BHYT" ? false : true) }
                                        ).ToList();

                    List<List<TTHD>> listIn = new List<List<TTHD>>();
                    listIn.Add(tienvanchuyen);
                    listIn.Add(tienkhongtinhthue);

                    bool success = false;

                    foreach (var ds in listIn)
                    {
                        if (ds.Count > 0)
                        {
                            success = true;
                            string codeTax = Bien.xmlFilePath_LIS[17];
                            Bien.InvoiceInfo objInvoice = new Bien.InvoiceInfo();
                            objInvoice.transactionUuid = System.Guid.NewGuid().ToString();
                            objInvoice.invoiceType = Bien.xmlFilePath_LIS[21]; //Mã loại hóa đơn chỉ nhận các giá trị sau: 01GTKT, 02GTTT, 07KPTQ, 03XKNB, 04HGDL. tuân thủ theo quy định ký hiệu loại hóa đơn của Thông tư hướng dẫn thi hành nghị định số 51/2010/NĐ-CP
                            objInvoice.templateCode = Bien.xmlFilePath_LIS[22];//Mã mẫu hóa đơn, tuân thủ theo quy định ký hiệu mẫu hóa đơn của Thông tư hướng dẫn thi hành nghị định số 51/2010/NĐ-CP


                            if (ds.First().IsCoThue)
                                objInvoice.invoiceSeries = khCoThue;
                            //objInvoice.invoiceSeries = khCoThue;
                            else
                                objInvoice.invoiceSeries = khKhongThue;//Là “Ký hiệu hóa đơn” tuân thủ theo quy tắc tạo ký hiệu hóa đơn của Thông tư hướng 
                            //dẫn thi hành nghị định số 51/2010/NĐ-CP. 
                            //Ví dụ AA/16E.
                            //Chú ý: Nếu trường hợp nhập invoiceSeries thì hệ thống sẽ lấy theo dữ liệu truyền vào, nếu không nhập invoiceSeries thì hệ thống sẽ lấy ký hiệu hóa đơn đang được phát hành theo mẫu hóa đơn. Đối với hóa đơn có nhiều dải thì dữ liệu invoiceSeries là yêu cầu bắt buộc
                            //DateTime a = DateTime.UtcNow;
                            //DateTime b = tttu.NgayThu.Value;
                            objInvoice.invoiceIssuedDate = ((Int64)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds);//((Int64)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds).ToString();//Ngày lập hóa đơn quy đổi ra số (time in miliseconds) được thiết lập theo Nghị định 51/2010/NĐ-CP.
                            //Hoặc 1 số định dạng như sau: 
                            //"yyyy-MM-dd'T'HH:mm:ss.SSSZ", "yyyy-MM-dd'T'HH:mm:ss.SSS'Z'", "EEE, dd MMM yyyy HH:mm:ss zzz", "yyyy-MM-dd")
                            objInvoice.currencyCode = "VND";
                            objInvoice.adjustmentType = "1";// "1";//Trạng thái điều chỉnh hóa đơn: 1: Hóa đơn gốc 3: Hóa đơn thay thế 5: Hóa đơn điều chỉnh
                            objInvoice.paymentStatus = true;//True: Đã thanh toán
                            objInvoice.paymentType = "TM";//Loại hình thức thanh toán: Bao gồm: 
                            //CK – Chuyển khoản
                            //DTCN – Đối trừ công nợ
                            //TM – tiền mặt
                            //TM/CK – Tiền mặt/ Chuyển khoản
                            //Nếu paymentStatus = true  thì bắt buộc phải truyền
                            if (ttbx.HTThanhToan == 1)
                                objInvoice.paymentTypeName = "CK";
                            else
                                objInvoice.paymentTypeName = "TM";//Tên phương thức thanh toán
                            //CK – Chuyển khoản
                            //DTCN – Đối trừ công nợ
                            //TM – tiền mặt
                            //TM/CK – Tiền mặt/ Chuyển khoản
                            //Nếu paymentStatus = true  thì bắt buộc phải truyền
                            objInvoice.cusGetInvoiceRight = true;
                            objInvoice.buyerIdNo = "";//Số giấy tờ của khách hàng
                            objInvoice.buyerIdType = "";//Loại giấy tờ của khách hàng:
                            //-	1: Số CMND
                            //-	3: Giấy phép kinh doanh
                            //-	2: Hộ chiếu
                            Bien.BuyerInfo objBuyer = new Bien.BuyerInfo();
                            if (!string.IsNullOrEmpty(ttbx.TenDonVi))
                                objBuyer.buyerAddressLine = ttbx.DiaChiNoiLV;
                            else
                                objBuyer.buyerAddressLine = ttbn.DChi;//"HN VN";//Địa chỉ bưu điện người mua 
                            objBuyer.buyerIdNo = objInvoice.buyerIdNo;//Số giấy tờ của khách hàng
                            objBuyer.buyerIdType = objInvoice.buyerIdType;//Loại giấy tờ của khách hàng:
                            //-	1: Số CMND
                            //-	3: Giấy phép kinh doanh
                            //-	2: Hộ chiếu
                            objBuyer.buyerName = ttbn.TenBNhan + " (Mã số BN: " + ttbn.MaBNhan + ")";
                            objBuyer.buyerPhoneNumber = ttbx.DThoai;//Điện thoại
                            objBuyer.buyerEmail = "";//Email
                            objBuyer.buyerLegalName = ttbx.TenDonVi;//Tên (đăng ký kinh doanh trong trường hợp là doanh nghiệp) của người mua
                            objBuyer.buyerTaxCode = ttbx.MaSoThue;//Mã số thuế
                            objBuyer.buyerBankName = ttbx.TenNganHang;//Tên ngân hàng
                            objBuyer.buyerBankAccount = ttbx.SoTaiKhoan;//Số tài khoản
                            Bien.SellerInfo objSeller = new Bien.SellerInfo();
                            if (heThong != null)
                            {
                                objSeller.sellerBankAccount = heThong.SoTaiKhoan;//Tài khoản ngân hàng của người bán
                                objSeller.sellerBankName = heThong.TenNganHang;//Tên ngân hàng
                                objSeller.sellerEmail = heThong.Email;//Email
                                objSeller.sellerFaxNumber = heThong.Fax;
                                objSeller.sellerWebsite = heThong.Website;
                            }
                            objSeller.sellerAddressLine = Bien.DiaChi;//"HN VN";//Địa chỉ bưu điện người bán
                            objSeller.sellerLegalName = Bien.TenCQ;//Tên (đăng ký kinh doanh trong trường hợp là doanh nghiệp) của người bán
                            objSeller.sellerPhoneNumber = Bien.SDTCQ;
                            objSeller.sellerTaxCode = codeTax;//Mã số thuế người bán được cấp bởi TCT Việt Nam. Mẫu 1: 0312770607 Mẫu 2: 0312770607-001
                            List<Bien.ItemInfo> lstItem = new List<Bien.ItemInfo>();
                            double tongtien = 0;
                            int line = 1;
                            foreach (var detail in ds)
                            {
                                tongtien += detail.SoTien;
                                Bien.ItemInfo item = new Bien.ItemInfo();
                                decimal sotien = (decimal)Math.Round(detail.SoTien, 0);
                                item.discount = 0;//"0.0";//giảm giá
                                item.itemCode = "";//Mã hàng hóa, dịch vụ
                                item.itemDiscount = 0;//"0.0";
                                item.itemName = detail.TenNhom;//"Tiền viện phí";
                                item.itemTotalAmountWithoutTax = sotien;
                                item.lineNumber = line;
                                item.quantity = 1;//"1";//Số lượng
                                item.taxAmount = 0;//"0.0";//Tổng tiền thuế
                                item.taxPercentage = 0;//"0";//Thuế suất của hàng hóa, dịch vụ 
                                item.unitName = "Lần";
                                item.unitPrice = sotien;
                                lstItem.Add(item);
                                line++;
                            }

                            decimal tongtienStr = (decimal)Math.Round(tongtien, 0);

                            Bien.SummarizeInfo objSummary = new Bien.SummarizeInfo();
                            objSummary.discountAmount = 0;//"0";//Tổng tiền chiết khấu thương mại trên toàn hóa đơn trước khi tính thuế. Chú ý: Khi tính chiết khấu, toàn hóa đơn chỉ sử dụng một mức thuế
                            objSummary.settlementDiscountAmount = 0;// "0";//Tổng tiền chiết khấu thanh toán trên toàn hóa đơn sau khi tính thuế. Chú ý: Khi tính chiết khấu, toàn hóa đơn chỉ sử dụng một mức thuế.
                            objSummary.sumOfTotalLineAmountWithoutTax = tongtienStr;//Tổng thành tiền cộng gộp của tất cả các dòng hóa đơn chưa bao gồm VAT.
                            //-	Hóa đơn thường: Tổng tiền HHDV trên các dòng HĐ. 
                            //-	Hóa đơn điều chỉnh: Tổng tiền điều chỉnh của các dòng HĐ.


                            //Vận chuyển thì thuế = 0, các dv khác thì k tính thuế
                            if (ds.FirstOrDefault().IDNhom == 12)
                                objSummary.taxPercentage = 0;//"0";
                            else
                                objSummary.taxPercentage = -2;//"-2";//Mức thuế: khai báo giá trị như sau 
                            //-	0%: 0 
                            //-	5%: 5 
                            //-	10%: 10 
                            //-	không phải kê khai và tính thuế GTGT: -1
                            //-	Không chịu thuế:  -2 

                            objSummary.totalAmountWithoutTax = tongtienStr;//Tổng tiền hóa đơn chưa bao gồm VAT. 
                            //-	Hóa đơn thường: Tổng tiền HHDV trên các dòng HĐ và các khoản tăng/giảm khác trên toàn HĐ. 
                            //-	Hóa đơn điều chỉnh: Tổng tiền điều chỉnh của các dòng HĐ và các khoản tăng/giảm khác trên toàn HĐ.

                            objSummary.totalAmountWithTax = tongtienStr;//Tổng tiền trên hóa đơn đã bao gồm VAT.
                            //-	Hóa đơn thường: Tổng tiền HHDV trên các dòng HĐ và các khoản tăng/giảm khác trên toàn HĐ đã bao gồm cả VAT.
                            //-	Hóa đơn điều chỉnh: Tổng tiền điều chỉnh của các dòng HĐ và các khoản tăng/giảm khác trên toàn HĐ đã bao gồm cả VAT

                            objSummary.totalAmountWithTaxInWords = "";// NumberUtil.DocSoThanhChu(objSummary.totalAmountWithTax);
                            objSummary.totalTaxAmount = 0;//"0";//Tổng tiền thuế trên toàn hóa đơn.
                            //-	Hóa đơn thường: Tổng tiền VAT trên các dòng HĐ và các khoản thuế khác trên toàn HĐ.
                            //-	Hóa đơn điều chỉnh: Tổng tiền VAT điều chỉnh của các dòng HĐ và các khoản tăng/giảm VAT khác trên toàn HĐ.

                            //string UserPass, string _codeTax, string url, Bien.Model.InvoiceInfo _objInvoice, Bien.Model.BuyerInfo _BuyerInfo, Bien.Model.SellerInfo _SellerInfo, List<Bien.Model.ItemInfo> _lstItem, Bien.Model.SummarizeInfo _SummarizeInfo
                            string userpass = Bien.xmlFilePath_LIS[18];
                            string url = Bien.xmlFilePath_LIS[19];

                            string result = DungChung.Ham.CreateInvoice_v2(userpass, codeTax, url, objInvoice, objBuyer, objSeller, lstItem, objSummary, id);
                            if (result.Contains("Lỗi tạo hóa đơn"))
                            {
                                MessageBox.Show(result);
                                //    _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
                                LOG moi = new LOG();
                                moi.DateLog = DateTime.Now;
                                moi.LyDo = string.Format("Tạo hóa đơn điện tử Viettel thất bại. Chi tiết lỗi: {0}", result);
                                moi.UserName = Bien.TenDN;
                                moi.MaBNhan = id;
                                moi.IdForm = 904;
                                moi.MaCB = Bien.MaCB;
                                moi.ComputerName = SystemInformation.ComputerName;
                                moi.Status = 1;
                                _dataContext.LOGs.Add(moi);
                                _dataContext.SaveChanges();
                                foreach (var subId in idtu)
                                {
                                    var tu = _dataContext.TamUngs.FirstOrDefault(o => o.IDTamUng == subId);
                                    tu.StatusHD = 3;
                                    _dataContext.SaveChanges();
                                }
                            }
                            else
                            {
                                JObject json = JObject.Parse(result);
                                string _erro = (string)json.SelectToken("errorCode");
                                if (!string.IsNullOrEmpty(_erro))
                                {
                                    string des = (string)json.SelectToken("description");
                                    MessageBox.Show("Lỗi tạo hóa đơn:" + des);
                                    //    _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
                                    LOG moi = new LOG();
                                    moi.DateLog = DateTime.Now;
                                    moi.LyDo = string.Format("Tạo hóa đơn điện tử Viettel thất bại. Mã lỗi: {0}. Chi tiết lỗi: {1}", _erro, des);
                                    moi.UserName = Bien.TenDN;
                                    moi.MaBNhan = id;
                                    moi.IdForm = 904;
                                    moi.MaCB = Bien.MaCB;
                                    moi.ComputerName = SystemInformation.ComputerName;
                                    moi.Status = 1;
                                    _dataContext.LOGs.Add(moi);
                                    _dataContext.SaveChanges();
                                    foreach (var subId in idtu)
                                    {
                                        var tu = _dataContext.TamUngs.FirstOrDefault(o => o.IDTamUng == subId);
                                        tu.StatusHD = 3;
                                        _dataContext.SaveChanges();
                                    }
                                }
                                else
                                {
                                    string _invoiceNo = (string)json.Last.First.SelectToken("invoiceNo");
                                    string _transactionID = (string)json.Last.First.SelectToken("transactionID");
                                    string _resevationCode = (string)json.Last.First.SelectToken("reservationCode");
                                    foreach (var subId in idtu)
                                    {
                                        var tu = _dataContext.TamUngs.FirstOrDefault(o => o.IDTamUng == subId);
                                        tu.MaHD = tu.MaHD + _invoiceNo + "|";
                                        tu.transactionID = tu.transactionID + _transactionID + "|";
                                        tu.reservationCode = tu.reservationCode + _resevationCode + "|";
                                        tu.templateCode = tu.templateCode + objInvoice.templateCode + "|";
                                        string ngayTaoHD = "";
                                        //if (!string.IsNullOrEmpty(objInvoice.invoiceIssuedDate))
                                        //{
                                        var date = (new DateTime(1970, 1, 1)).AddMilliseconds(objInvoice.invoiceIssuedDate);
                                        if (date != null)
                                        {
                                            DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.Local);
                                            ngayTaoHD = cstTime.ToString("yyyyMMddHHmmss");
                                        }
                                        //}
                                        else
                                        {
                                            ngayTaoHD = DateTime.Now.ToString("yyyyMMddHHmmss");
                                        }
                                        tu.NgayTaoHD = ngayTaoHD;
                                        tu.MaCBTaoHD = Bien.MaCB;
                                        tu.StatusHD = 2;
                                        _dataContext.SaveChanges();
                                    }
                                    MessageBox.Show("Tạo hóa đơn thành công");
                                    #region Ghi log
                                    //    _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
                                    LOG moi = new LOG();
                                    moi.DateLog = DateTime.Now;
                                    moi.LyDo = "Tạo hóa đơn điện tử Viettel thành công";
                                    moi.UserName = Bien.TenDN;
                                    moi.MaBNhan = id;
                                    moi.IdForm = 904;
                                    moi.MaCB = Bien.MaCB;
                                    moi.ComputerName = SystemInformation.ComputerName;
                                    moi.Status = 1;
                                    _dataContext.LOGs.Add(moi);
                                    _dataContext.SaveChanges();
                                    #endregion
                                }
                            }
                        }
                    }

                    if (!success)
                    {
                        rollBack = true;
                        MessageBox.Show("Không có dữ liệu tạo hóa đơn");
                    }
                }
            }
            #endregion
        }

        public void XemHD_01071(QLBV_Database.QLBVEntities _data, int id, List<int> idtu)
        {
            //    _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
            BenhNhan ttbn = _dataContext.BenhNhans.Where(p => p.MaBNhan == id).FirstOrDefault();
            TTboXung ttbx = _dataContext.TTboXungs.Where(p => p.MaBNhan == id).FirstOrDefault();
            HTHONG heThong = _dataContext.HTHONGs.FirstOrDefault(p => p.MaBV == Bien.MaBV);
            var tttu = _dataContext.TamUngs.Where(o => idtu.Contains(o.IDTamUng)).ToList();
            if (tttu != null && tttu.Count > 0)
            {
                #region xem hóa đơn
                if (!string.IsNullOrEmpty(Bien.xmlFilePath_LIS[20]) && Bien.xmlFilePath_LIS[20] == "0")
                {
                    if (!string.IsNullOrEmpty(tttu.FirstOrDefault().pathHD))
                    {
                        var pathSplit = tttu.FirstOrDefault().pathHD.Split('|');
                        if (pathSplit.Count() > 0)
                        {
                            foreach (var item in pathSplit)
                            {
                                if (!string.IsNullOrWhiteSpace(item) && File.Exists(item))
                                {
                                    frm_ViewPDF frm = new frm_ViewPDF(item);
                                    frm.ShowDialog();
                                }
                            }
                        }

                    }
                    else
                    {
                        string userpass = Bien.xmlFilePath_LIS[18];
                        string url = Bien.xmlFilePath_LIS[19];
                        string codeTax = Bien.xmlFilePath_LIS[17];
                        string pathsave = Bien.xmlFilePath_LIS[16];

                        string path = "";
                        if (!string.IsNullOrWhiteSpace(tttu.FirstOrDefault().MaHD) && !string.IsNullOrWhiteSpace(tttu.FirstOrDefault().templateCode))
                        {
                            var spMaHD = tttu.FirstOrDefault().MaHD.Split('|');
                            var spTemplateCode = tttu.FirstOrDefault().templateCode.Split('|');
                            var transactionID = tttu.FirstOrDefault().transactionID.Split('|');
                            for (int i = 0; i < spMaHD.Count(); i++)
                            {
                                var ma = spMaHD[i];
                                var tem = spTemplateCode[i];
                                var tran = transactionID[i];
                                if (!string.IsNullOrWhiteSpace(ma))
                                {
                                    var pathView = DungChung.Ham.ViewHoaDon_v2(userpass, codeTax, url, ma, tem, tran, pathsave);//ViewHoaDon(userpass, codeTax, url, ma, spTemplateCode[i],  pathsave);
                                    if (!string.IsNullOrEmpty(pathView) && File.Exists(pathView))
                                    {
                                        frm_ViewPDF frm = new frm_ViewPDF(pathView);
                                        frm.ShowDialog();
                                        path += pathView + "|";
                                    }
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(path))
                        {
                            foreach (var item in tttu)
                            {
                                var tamung = _dataContext.TamUngs.FirstOrDefault(o => o.IDTamUng == item.IDTamUng);
                                tamung.pathHD = path;
                                _dataContext.SaveChanges();
                            }
                        }
                    }
                }
                #endregion
            }

        }

        private void btnTaoHD_Click(object sender, EventArgs e)
        {
            try
            {
                btnTaoHD.Enabled = false;
                //    _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
                int rs;
                int id = 0;
                if (Int32.TryParse(txtMaBNhan.Text, out rs))
                    id = Convert.ToInt32(txtMaBNhan.Text);
                var checkkytu = CheckKyTu.hasSpecialChar(txtTenBenhNhan.Text);
                if (checkkytu == true)
                {
                    MessageBox.Show("Tên bệnh nhân không được chứa ký tự đặc biệt");
                    return;
                }
                if (Bien.MaBV == "01071" || Bien.MaBV == "01049")
                {
                    bool rollBack = false;
                    var tamung = _dataContext.TamUngs.FirstOrDefault(o => o.PhanLoai == 1 && o.MaBNhan == id && o.StatusHD != 1);
                    if (tamung != null)
                    {
                        if (tamung.ThoiGianGuiHDDT != null && DateTime.Now <= tamung.ThoiGianGuiHDDT.Value.AddMinutes(2))
                        {
                            btnTaoHD.Enabled = true;
                            MessageBox.Show("Hóa đơn đang được xử lý, xin vui lòng đợi", "Thông báo");
                            return;
                        }
                        List<int> listTamUng = new List<int>();
                        listTamUng.Add(tamung.IDTamUng);
                        tamung.StatusHD = 1;
                        tamung.ThoiGianGuiHDDT = DateTime.Now;
                        _dataContext.SaveChanges();
                        try
                        {
                            //TaoHD_01071(_dataContext, id, listTamUng, ref rollBack);
                            CreateInvoice01071(id, listTamUng, ref rollBack);
                        }
                        catch (Exception ex)
                        {
                            foreach (var item in listTamUng)
                            {
                                var tu = _dataContext.TamUngs.FirstOrDefault(o => o.IDTamUng == item);
                                tu.ThoiGianGuiHDDT = null;
                                tu.StatusHD = null;
                                _dataContext.SaveChanges();
                            }
                            throw ex;
                        }

                        if (rollBack)
                        {
                            foreach (var item in listTamUng)
                            {
                                var tu = _dataContext.TamUngs.FirstOrDefault(o => o.IDTamUng == item);
                                tu.ThoiGianGuiHDDT = null;
                                tu.StatusHD = null;
                                _dataContext.SaveChanges();
                            }
                        }

                        grvBNhantt_FocusedRowChanged(null, null);
                    }
                }

                else
                {
                    var tamung = _dataContext.TamUngs.FirstOrDefault(o => o.PhanLoai == 1 && o.MaBNhan == id && (o.StatusHD == 1 || o.StatusHD == 2));
                    if (tamung != null)
                    {
                        MessageBox.Show("Hóa đơn đã được tạo!");
                        btnXemHDDuyet.Enabled = true;
                        HuyHD.Enabled = true;

                        return;
                    }

                    //TaoHD(_dataContext, id, false, 0);
                    CreateInvoice(id, 0, false);
                }
                grvBNhantt_FocusedRowChanged(null, null);
            }
            finally
            {

            }
        }

        private string checkRes(string res)
        {
            string mes = "";
            if (res.Contains("ERR:1"))
            {
                mes = "Lỗi: Tài khoản đăng nhập sai hoặc không có quyền.";
            }
            else if (res.Contains("ERR:2"))
            {
                mes = "Lỗi: Hóa đơn cần điều chỉnh không tồn tại.";
            }
            else if (res.Contains("ERR:3"))
            {
                mes = "Lỗi: Dữ liệu xml đầu vào không đúng quy định.";
            }
            else if (res.Contains("ERR:4"))
            {
                mes = "Lỗi: Công ty chưa được đăng kí mẫu hóa đơn nào.";
            }
            else if (res.Contains("ERR:5"))
            {
                mes = "Lỗi: Không phát hành được hóa đơn.";
            }
            else if (res.Contains("ERR:6"))
            {
                mes = "Lỗi: Dải hóa đơn cũ đã hết.";
            }
            else if (res.Contains("ERR:7"))
            {
                mes = "Lỗi: User name không phù hợp, không tìm thấy company tương ứng cho user.";
            }
            else if (res.Contains("ERR:8"))
            {
                mes = "Lỗi: Hóa đơn cần điều chỉnh đã bị thay thế. Không thể điều chỉnh được nữa.";
            }
            else if (res.Contains("ERR:9"))
            {
                mes = "Lỗi: Trạng thái hóa đơn không được điều chỉnh.";
            }
            else if (res.Contains("ERR:10"))
            {
                mes = "Lỗi: Lô có số hóa đơn vượt quá max cho phép.";
            }
            else if (res.Contains("ERR:11"))
            {
                mes = "Lỗi: Hóa đơn chưa thanh toán nên không xem được.";
            }
            else if (res.Contains("ERR:11"))
            {
                mes = "Lỗi: Hóa đơn đã được gạch nợ.";
            }
            else if (res.Contains("ERR:20"))
            {
                mes = "Lỗi: Mẫu số và Ký hiệu không phù hợp, hoặc không tồn tại hóa đơn đã đăng kí có sử dụng Mẫu số và Ký hiệu truyền vào.";
            }
            else
            {
                mes = res;
            }

            return mes;
        }

        private void cboPLThu_EditValueChanged(object sender, EventArgs e)
        {
            if (cboPLThu.SelectedIndex == 3) // thu thẳng
            {
                _trangthai = txtSoTien.Enabled;
                txtSoTien.Enabled = false;
            }
            else
                txtSoTien.Enabled = _trangthai;
        }

        private void btnTaoHD3_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Bạn có muốn tạo hóa đơn không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
                btnTaoHD3.Enabled = false;
                //    _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
                int rs;
                int id = 0;
                if (Int32.TryParse(txtMaBNhan.Text, out rs))
                    id = Convert.ToInt32(txtMaBNhan.Text);
                int _idtthu = 0;
                if (!string.IsNullOrEmpty(txtIdTamUng.Text))
                    _idtthu = int.Parse(txtIdTamUng.Text);
                if (Bien.MaBV == "01071" || Bien.MaBV == "01049")
                {
                    bool rollBack = false;
                    List<int> idSelect = new List<int>();
                    var selectPT = grvTamUng.GetSelectedRows();
                    if (selectPT.Count() > 0)
                    {
                        foreach (var item in selectPT)
                        {
                            var row = (TamUng)grvTamUng.GetRow(item);
                            if (row != null && (row.PhanLoai == 1 || row.PhanLoai == 3))
                            {
                                var tuCheck = _dataContext.TamUngs.FirstOrDefault(o => o.IDTamUng == row.IDTamUng);
                                if (tuCheck != null)
                                {
                                    if (tuCheck.ThoiGianGuiHDDT != null && DateTime.Now <= tuCheck.ThoiGianGuiHDDT.Value.AddMinutes(2))
                                    {
                                        MessageBox.Show("Hóa đơn đang được xử lý, xin vui lòng đợi", "Thông báo");
                                        btnTaoHD3.Enabled = true;
                                        return;
                                    }
                                    if (string.IsNullOrWhiteSpace(tuCheck.MaHD) && tuCheck.StatusHD != 1)
                                    {
                                        idSelect.Add(tuCheck.IDTamUng);
                                        tuCheck.ThoiGianGuiHDDT = DateTime.Now;
                                        tuCheck.StatusHD = 1;
                                        _dataContext.SaveChanges();
                                    }
                                    else
                                    {
                                        MessageBox.Show(string.Format("Phiếu thu: {0} đã được tạo hóa đơn", tuCheck.IDTamUng));
                                        return;
                                    }
                                }
                            }
                        }
                        if (idSelect.Count > 0)
                        {
                            try
                            {
                                //TaoHD_01071(_dataContext, id, idSelect, ref rollBack); // ko cần nữa. tạo bn test khác nè, à cấu hình như bên 01071 nhé
                                CreateInvoice01071(id, idSelect, ref rollBack);
                            }
                            catch (Exception ex)
                            {
                                foreach (var item in idSelect)
                                {
                                    var tu = _dataContext.TamUngs.FirstOrDefault(o => o.IDTamUng == item);
                                    tu.StatusHD = null;
                                    tu.ThoiGianGuiHDDT = null;
                                    _dataContext.SaveChanges();
                                }
                                throw ex;
                            }
                            if (rollBack)
                            {
                                foreach (var item in idSelect)
                                {
                                    var tu = _dataContext.TamUngs.FirstOrDefault(o => o.IDTamUng == item);
                                    tu.StatusHD = null;
                                    tu.ThoiGianGuiHDDT = null;
                                    _dataContext.SaveChanges();
                                }
                            }
                            grvBNhantt_FocusedRowChanged(null, null);
                        }
                        else
                            MessageBox.Show("Phiếu thu không hợp lệ");
                    }
                    else
                        MessageBox.Show("Không có phiếu thu được chọn");
                }
                else
                    //TaoHD(_dataContext, id, true, _idtthu);
                    CreateInvoice(id, _idtthu, true);

                grvTamUng_FocusedRowChanged(null, null);

            }
            finally
            {

            }
        }

        private void btnGuiHSSK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Bien.UrlPID))
            {
                MessageBox.Show("Chưa cấu hình Url PID", "Thông báo");
                return;
            }
            if (string.IsNullOrWhiteSpace(Bien.UrlUploadHSSK))
            {
                MessageBox.Show("Chưa cấu hình Url Upload HSSK", "Thông báo");
                return;
            }
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(Bien.StrCon);
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            var qtt = _data.VienPhis.Where(p => p.MaBNhan == _int_maBN).FirstOrDefault();
            var qrv = _data.RaViens.Where(p => p.MaBNhan == _int_maBN).FirstOrDefault();
            if (qtt == null)
                MessageBox.Show("Bệnh nhân chưa thanh toán");
            else if (qrv == null)
                MessageBox.Show("Bệnh nhân chưa ra viện");
            else if (qrv.maGiaoDichHSSK != null && qrv.maGiaoDichHSSK != "")
                MessageBox.Show("Bệnh nhân đã có mã giao dịch, bạn không thể gửi hồ sơ");
            else
            {
                List<QLBV.DungChung.LienThongHSSK.MessageHSSK> listMessages = new List<QLBV.DungChung.LienThongHSSK.MessageHSSK>();
                LienThongHSSK hssk = new LienThongHSSK();
                if (hssk.GetHoSoKhamChuaBenh(_data, _int_maBN, Bien.xmlFilePath_LIS[35], ref listMessages))
                {
                    if (listMessages.Count() <= 0)
                    {
                        MessageBox.Show("Gửi bệnh nhân thành công");
                    }
                    else
                        MessageBox.Show(string.Format("Gửi bệnh nhân thành công", Environment.NewLine + string.Join(Environment.NewLine, listMessages.Select(o => o.Message))));
                }
                else
                {
                    MessageBox.Show(string.Format("Gửi bệnh nhân thất bại", Environment.NewLine + string.Join(Environment.NewLine, listMessages)));
                }

            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(Bien.StrCon);
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);

            var ttbs = _data.TTboXungs.FirstOrDefault(o => o.MaBNhan == _int_maBN);
            if (ttbs != null && MessageBox.Show("Bạn có muốn xác nhận bệnh nhân là đối tượng chuyển khoản?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ttbs.HTThanhToan = 1;

                if (_data.SaveChanges() > 0)
                {
                    MessageBox.Show("Xác nhận thành công", "Thông báo");
                    btnXacNhan.Enabled = false;
                }
                else
                    MessageBox.Show("Xác nhận thất bại", "Thông báo");
            }
        }

        int rowHandle;
        private void grvBNhantt_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                if (e.HitInfo.InRow)
                {
                    GridView view = sender as GridView;
                    view.FocusedRowHandle = e.HitInfo.RowHandle;
                    rowHandle = e.HitInfo.RowHandle;
                    contextMenuStrip1.Show(view.GridControl, e.Point);
                }
            }
            catch (Exception ex)
            {
                DungChung.WriteLog.Warn(ex);
            }
        }

        private void nhậpThôngTinBổSungToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var row = (BenhNhan)grvBNhantt.GetRow(rowHandle);
            if (row != null)
            {
                frm_NhapTTHD frm = new frm_NhapTTHD(row.MaBNhan);
                frm.ShowDialog();
            }
        }

        TamUng currentTamung;
        private void grvTamUng_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            currentTamung = null;
            try
            {
                if (e.HitInfo.InRow)
                {
                    //    _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
                    var clickTamUng = (TamUng)grvTamUng.GetRow(e.HitInfo.RowHandle);
                    currentTamung = clickTamUng != null ? _dataContext.TamUngs.FirstOrDefault(o => o.IDTamUng == clickTamUng.IDTamUng) : null;
                    if (currentTamung != null)
                    {
                        if (string.IsNullOrEmpty(currentTamung.MaHD) && string.IsNullOrEmpty(currentTamung.FkeyVNPT))
                        {
                            grvBNhantt_FocusedRowChanged(null, null);
                            return;
                        }
                        ContextMenuStrip cms = new ContextMenuStrip();
                        if (Bien.MaBV == "01071" || Bien.MaBV == "01049")
                        {
                            if (!string.IsNullOrEmpty(currentTamung.pathHDCD))
                            {
                                cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xemHóaĐơnChuyểnĐổiToolStripMenuItem});
                            }
                            else
                            {
                                cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lậpHóaĐơnChuyểnĐổiToolStripMenuItem});
                            }
                        }

                        var hThongUser = _dataContext.HThong_User.FirstOrDefault(o => o.TenDN == Bien.TenDN);
                        if (hThongUser != null && hThongUser.IsAllowCancelHDDT == true)
                        {
                            cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hủyHóaĐơnToolStripMenuItem});
                        }

                        GridView view = sender as GridView;
                        view.FocusedRowHandle = e.HitInfo.RowHandle;
                        cms.Show(view.GridControl, e.Point);
                    }
                }
            }
            catch (Exception ex)
            {
                DungChung.WriteLog.Warn(ex);
            }
        }

        private void lậpHóaĐơnChuyểnĐổiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTamung != null)
            {
                if (string.IsNullOrEmpty(currentTamung.pathHDCD))
                {
                    if (!string.IsNullOrEmpty(Bien.xmlFilePath_LIS[20]) && Bien.xmlFilePath_LIS[20] == "0")
                    {
                        string userpass = Bien.xmlFilePath_LIS[18];
                        string url = Bien.xmlFilePath_LIS[19];
                        string codeTax = Bien.xmlFilePath_LIS[17];
                        string pathsave = Bien.xmlFilePath_LIS[16] + @"\HoaDonChuyenDoi";
                        if (!Directory.Exists(pathsave))
                        {
                            Directory.CreateDirectory(pathsave);
                        }
                        string tenCB = "";
                        var cb = _dataContext.CanBoes.FirstOrDefault(o => o.MaCB == Bien.MaCB);
                        if (cb != null)
                            tenCB = cb.TenCB;
                        string ngayTaoHD = "";
                        if (!string.IsNullOrEmpty(currentTamung.NgayTaoHD))
                        {
                            var date = DateTime.ParseExact(currentTamung.NgayTaoHD, "yyyyMMddHHmmss", null);
                            if (date != null)
                            {
                                ngayTaoHD = date.ToUniversalTime().ToString("yyyyMMddHHmmss");
                            }
                        }
                        else
                        {
                            ngayTaoHD = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                        }

                        DateTime ngayTaoHD_mili;
                        string ngayTaoHD_str = "";
                        string contentType = "POST";
                        if (ngayTaoHD != null)
                        {
                            int ngay = Convert.ToInt32(ngayTaoHD.Substring(0, 4));
                            int thang = Convert.ToInt32(ngayTaoHD.Substring(4, 2));
                            int nam = Convert.ToInt32(ngayTaoHD.Substring(6, 2));

                            int gio = Convert.ToInt32(ngayTaoHD.Substring(8, 2));
                            int phut = Convert.ToInt32(ngayTaoHD.Substring(10, 2));
                            int giay = Convert.ToInt32(ngayTaoHD.Substring(12, 2));

                            ngayTaoHD_mili = new DateTime(ngay, thang, nam, gio, phut, giay);
                            ngayTaoHD_mili = ngayTaoHD_mili.AddHours(7);
                            ngayTaoHD_str = (ngayTaoHD_mili.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local)).TotalMilliseconds).ToString();
                        }
                        string path = "";
                        if (!string.IsNullOrWhiteSpace(currentTamung.MaHD) && !string.IsNullOrWhiteSpace(currentTamung.templateCode))
                        {
                            var spMaHD = currentTamung.MaHD.Split('|');
                            var spTemplateCode = currentTamung.templateCode.Split('|');
                            for (int i = 0; i < spMaHD.Count(); i++)
                            {
                                var ma = spMaHD[i];
                                if (!string.IsNullOrWhiteSpace(ma))
                                {
                                    var pathView = DungChung.Ham.ViewHoaDonChuyenDoi(userpass, codeTax, url, ma, spTemplateCode[i], ngayTaoHD_str, tenCB, pathsave);
                                    if (!string.IsNullOrEmpty(pathView) && File.Exists(pathView))
                                    {
                                        var tamung = _dataContext.TamUngs.FirstOrDefault(o => o.IDTamUng == currentTamung.IDTamUng);
                                        frm_ViewPDF frm = new frm_ViewPDF(pathView, tamung.IsPrintHDCD ?? false, UpdatePrintHDCD);
                                        frm.ShowDialog();
                                        path += pathView + "|";
                                    }
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(path))
                        {
                            var tamung = _dataContext.TamUngs.FirstOrDefault(o => o.IDTamUng == currentTamung.IDTamUng);
                            tamung.pathHDCD = path;
                            _dataContext.SaveChanges();
                            grvBNhantt_FocusedRowChanged(null, null);
                        }
                    }
                }
            }
        }

        private void xemHóaĐơnChuyểnĐổiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTamung != null)
            {
                var tamung = _dataContext.TamUngs.FirstOrDefault(o => o.IDTamUng == currentTamung.IDTamUng);
                if (tamung != null && !string.IsNullOrEmpty(Bien.xmlFilePath_LIS[20]) && Bien.xmlFilePath_LIS[20] == "0")
                {
                    if (!string.IsNullOrEmpty(currentTamung.pathHDCD))
                    {
                        var pathSplit = currentTamung.pathHDCD.Split('|');
                        if (pathSplit.Count() > 0)
                        {
                            foreach (var item in pathSplit)
                            {
                                if (!string.IsNullOrWhiteSpace(item) && File.Exists(item))
                                {
                                    frm_ViewPDF frm = new frm_ViewPDF(item, tamung.IsPrintHDCD ?? false, UpdatePrintHDCD);
                                    frm.ShowDialog();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void UpdatePrintHDCD()
        {
            if (currentTamung != null)
            {
                //    _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
                var tamung = _dataContext.TamUngs.FirstOrDefault(o => o.IDTamUng == currentTamung.IDTamUng);
                tamung.IsPrintHDCD = true;
                _dataContext.SaveChanges();
            }
        }

        private void grvTamUng_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var row = (TamUng)grvTamUng.GetRow(e.RowHandle);
            if (row != null)
            {
                if (row.MaHD != null)
                {
                    e.Appearance.ForeColor = Color.Red;

                }
                else
                {
                    e.Appearance.ForeColor = Color.Black;
                }
            }
        }

        private void hủyHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentTamung != null)
            {
                //    _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
                var tamung = _dataContext.TamUngs.FirstOrDefault(o => o.IDTamUng == currentTamung.IDTamUng);
                if (tamung != null && (!string.IsNullOrWhiteSpace(tamung.MaHD) || !string.IsNullOrWhiteSpace(tamung.FkeyVNPT)))
                {
                    if (MessageBox.Show("Bạn có muốn hủy hóa đơn?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        tamung.MaHD = null;
                        tamung.transactionID = null;
                        tamung.reservationCode = null;
                        tamung.pathHD = null;
                        tamung.templateCode = null;
                        tamung.pathHDCD = null;
                        tamung.NgayTaoHD = null;
                        tamung.MaCBTaoHD = null;
                        tamung.FkeyVNPT = null;
                        tamung.TokenVNPT = null;
                        tamung.IsPrintHDCD = null;
                        tamung.ThoiGianGuiHDDT = null;
                        if (_dataContext.SaveChanges() > 0)
                        {
                            LOG moi = new LOG();
                            moi.DateLog = DateTime.Now;
                            moi.LyDo = string.Format("Hủy hóa đơn điện tử: {0} của phiếu thu {1}", (!string.IsNullOrWhiteSpace(currentTamung.MaHD) ? currentTamung.MaHD : currentTamung.FkeyVNPT), currentTamung.IDTamUng);
                            moi.UserName = Bien.TenDN;
                            moi.MaBNhan = currentTamung.MaBNhan;
                            moi.IdForm = 904;
                            moi.ComputerName = SystemInformation.ComputerName;
                            moi.MaCB = Bien.MaCB;
                            moi.Status = 3;
                            _dataContext.LOGs.Add(moi);
                            _dataContext.SaveChanges();
                            MessageBox.Show("Xóa thành công!");
                            grvBNhantt_FocusedRowChanged(null, null);
                        }
                        else
                        {
                            MessageBox.Show("Xóa thất bại!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Phiếu thu chưa được tạo hóa đơn!");
                }
            }
        }

        bool thuBangThe = false;
        private void btnThuBangThe_Click(object sender, EventArgs e)
        {
            thuBangThe = true;
            btnLuutthu_Click(null, null);
        }

        private void btnThanhToanThe_Click(object sender, EventArgs e)
        {
            //thuBangThe = true;
            //btnThanhToan_Click(null, null);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Bạn có muốn hủy không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
            //    QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
            //    var vienPhi = dataContext.VienPhis.Where(o => o.MaBNhan == _mabn).ToList();
            //    if (vienPhi.Count > 0)
            //    {
            //        foreach (var item in vienPhi)
            //        {
            //            var vp = dataContext.VienPhis.FirstOrDefault(o => o.idVPhi == item.idVPhi);
            //            if (vp != null)
            //            {
            //                ConnectBanking.Agribank.ParamResult paramResult = null;
            //                if (DungChung.Ham.HuyThanhToanPOS_Agribank(vp.MaBNhan, vp.ReceiptNo, vp.ClientId, vp.RequestId, ref paramResult))
            //                {
            //                    vp.LoaiThanhToan = null;
            //                    vp.ClientId = null;
            //                    vp.RequestId = null;
            //                    vp.ReceiptNo = null;
            //                    dataContext.SaveChanges();
            //                    MessageBox.Show("Hủy thành công!");
            //                }
            //                else
            //                {
            //                    MessageBox.Show("Không hủy được!");
            //                }
            //            }
            //        }
            //        grvBNhantt_FocusedRowChanged(null, null);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Bệnh nhân chưa thanh toán");
            //    }
            //}
        }

        private void btnCancelTT_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn hủy không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
                if (!string.IsNullOrEmpty(txtIdTamUng.Text))
                {
                    int _soPhieu = Convert.ToInt32(txtIdTamUng.Text);
                    int mabn = Convert.ToInt32(txtMaBNhan.Text);
                    var tamung = dataContext.TamUngs.Where(o => o.IDTamUng == _soPhieu).ToList();
                    if (tamung.Count > 0)
                    {
                        foreach (var item in tamung)
                        {
                            var tu = dataContext.TamUngs.FirstOrDefault(o => o.IDTamUng == item.IDTamUng);
                            if (tu != null)
                            {
                                ConnectBanking.Agribank.ParamResult paramResult = null;
                                if (DungChung.Ham.HuyThanhToanPOS_Agribank(tu.MaBNhan, tu.ReceiptNo, tu.ClientId, tu.RequestId, ref paramResult))
                                {
                                    tu.LoaiThanhToan = null;
                                    tu.ClientId = null;
                                    tu.RequestId = null;
                                    tu.ReceiptNo = null;
                                    dataContext.SaveChanges();
                                    MessageBox.Show("Hủy thành công!");
                                }
                                else
                                {
                                    MessageBox.Show("Không hủy được!");
                                }
                            }
                        }
                        grvBNhantt_FocusedRowChanged(null, null);
                    }
                }
            }
        }

        private void btnTongKet_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn tổng kết?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (DungChung.Ham.TongKetPOS_Agribank(Bien.MaCB, SystemInformation.ComputerName, true))
                {
                    MessageBox.Show("Tổng kết thành công!");
                }
                else
                    MessageBox.Show("Tổng kết thất bại!");
            }
        }

        private void cboPhieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPhieu.SelectedIndex == 0)
            {
                int rs;
                int id = 0;
                if (Int32.TryParse(txtMaBNhan.Text, out rs))
                    id = Convert.ToInt32(txtMaBNhan.Text);

                //    _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
                var dt = (from a in _dataContext.VienPhis.Where(p => p.MaBNhan == id)
                          join b in _dataContext.DThuocs.Where(p => p.PLDV == 1) on a.MaBNhan equals b.MaBNhan
                          join d in _dataContext.DThuoccts on b.IDDon equals d.IDDon
                          join c in _dataContext.DichVus.Where(p => p.PLoai == 1).Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6) on d.MaDV equals c.MaDV
                          select new
                          {
                              b.IDDon,
                              b.MaKP,
                              a.MaBNhan,
                              TenDV = c.TenDV + "/ " + c.HamLuong,
                              d.DonVi,
                              d.SoLuong,
                              d.DonGia,
                              d.ThanhTien,
                              c.NuocSX,
                              d.MaKXuat
                          }).ToList();
                var x1 = (from a in dt
                          group a by new { a.IDDon, a.MaKP, a.MaKXuat } into kq
                          select new { kq.Key.IDDon, kq.Key.MaKP, kq.Key.MaKXuat }).OrderBy(p => p.IDDon).ToList();
                int minid = x1.Min(p => p.IDDon);
                int makpx = x1.Where(p => p.IDDon == minid).First().MaKXuat ?? 0;
                frmIn frm = new frmIn();
                BaoCao.rep_PhieuPhatThuoc rep = usTamThu_TToan.innhieudon(_dataContext, id, minid, makpx);
                foreach (var item in x1.Where(p => p.IDDon != minid))
                {
                    BaoCao.rep_PhieuPhatThuoc rep2 = usTamThu_TToan.innhieudon(_dataContext, id, item.IDDon, item.MaKXuat ?? 0);
                    rep.Pages.AddRange(rep2.Pages);
                }
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            if (cboPhieu.SelectedIndex == 1)
            {
                if (Bien.MaBV == "27022")
                {
                    int IDDon = (int)grvThanhToan.GetFocusedRowCellValue(colIDDon);
                    var dt = _dataContext.DThuocs.Where(p => p.IDDon == IDDon && p.PLDV == 1).ToList();
                    if (dt.Count > 0)
                    {
                        DungChung.Ham.InDon(IDDon, _mabn, 0, 0, true, 1);
                    }

                }
                else
                {


                    QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(Bien.StrCon);
                    bool XN = true;
                    int rs;
                    int _int_maBN = 0;
                    if (grvBNhantt.RowCount > 0)
                    {
                        _int_maBN = Convert.ToInt32(grvBNhantt.GetFocusedRowCellValue("MaBNhan"));

                        var getidKB = data.BNKBs.Where(p => p.MaBNhan == _int_maBN).First();
                        var getidDontv = data.DThuoctvs.Where(p => p.MaBNhan == _int_maBN).ToList();

                        int idkb = Convert.ToInt32(getidKB.IDKB);

                        QLBV.FormThamSo.frm_KeDonTV frm = new QLBV.FormThamSo.frm_KeDonTV(_int_maBN, idkb);
                        if (getidDontv.Count > 0)
                        {
                            int iddtv = Convert.ToInt32(getidDontv[0].IDDontv);
                            frm_KeDonTV.Hamindon(_int_maBN, idkb, iddtv);
                        }
                        else
                        {
                            MessageBox.Show("Bệnh nhân chưa có kê đơn.");
                            //frm.ShowDialog();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Bạn chưa chọn bệnh nhân!");
                    }
                }
            }
            cboPhieu.SelectedIndex = -1;
            cboPhieu.EditValue = "Chọn phiếu";
        }

        private void btnXemHD_Click(object sender, EventArgs e)
        {
            //    _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
            int rs;
            int id = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                id = Convert.ToInt32(txtMaBNhan.Text);
            int _idtthu = 0;
            if (!string.IsNullOrEmpty(txtIdTamUng.Text))
                _idtthu = int.Parse(txtIdTamUng.Text);
            if (Bien.MaBV == "01071" || Bien.MaBV == "01049")
            {
                bool rollBack = false;
                List<int> idSelect = new List<int>();
                var selectPT = grvTamUng.GetSelectedRows();

                idSelect.Add(_idtthu);
                //XemHD_01071(_dataContext, id, idSelect);
                ViewInvoice01071(id, idSelect);
            }
            else
                //XemHD(_dataContext, id, true, _idtthu);
                ViewInvoice(id, _idtthu, true);
        }

        private void btnXemHDDuyet_Click(object sender, EventArgs e)
        {
            //    _dataContext = new QLBV_Database.QLBVEntities(Bien.StrCon);
            int rs;
            int id = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                id = Convert.ToInt32(txtMaBNhan.Text);
            if (Bien.MaBV == "01071" || Bien.MaBV == "01049")
            {
                var tamung = _dataContext.TamUngs.FirstOrDefault(o => o.PhanLoai == 1 && o.MaBNhan == id && o.StatusHD != 1);
                if (tamung != null)
                {
                    List<int> listTamUng = new List<int>();
                    listTamUng.Add(tamung.IDTamUng);
                    //XemHD_01071(_dataContext, id, listTamUng);
                    ViewInvoice01071(id, listTamUng);
                }
            }
            else
            {
                //XemHD(_dataContext, id, false, 0);
                ViewInvoice(id, 0, false);
            }
        }

        private void grcBNhantt_Click(object sender, EventArgs e)
        {

        }

        private void HuyHD_Click(object sender, EventArgs e)
        {

            string message = "Bạn có muốn hủy hóa đơn?";
            string title = "Thông báo";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.No)
            {
                return;
            }
            var tamung = _dataContext.TamUngs.FirstOrDefault(o => o.PhanLoai == 1 && o.MaBNhan == _mabn && o.StatusHD != 1 && o.StatusHD != 4);
            if (tamung != null)
            {

                try
                {
                    tamung.StatusHD = 4;
                    tamung.pathHD = null;
                    int sv = _dataContext.SaveChanges();
                    if (sv > 0)
                    {
                        btnTaoHD.Enabled = true;
                        HuyHD.Enabled = false;
                        btnXemHDDuyet.Enabled = false;
                        btnHuyDuyet.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Hủy không thành công");
            }
        }

        private void grcThanhToan_Click(object sender, EventArgs e)
        {

        }

        private void grvBNhantt_PrintInitialize(object sender, DevExpress.XtraGrid.Views.Base.PrintInitializeEventArgs e)
        {

        }

        private void grcThanhToan_Click_1(object sender, EventArgs e)
        {

        }

        private void groupControl3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void grvTamUng_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            var row = (TamUng)grvTamUng.GetRow(e.RowHandle);
            if (row != null)
            {
                if (e.Column.FieldName == "ThuNH")
                {
                    e.RepositoryItem = row.IDTamUngThe != null ? repositoryItemButtonEdit_ThuNH_Enable : null;
                }
            }
        }

        public int CheckNoiNgoaiTru(int mabn)
        {
            var bn = _dataContext.BenhNhans.Where(p => p.MaBNhan == mabn).Select(p => p.NoiTru).FirstOrDefault();
            if (bn != null)
                return bn.Value;
            return -1;
        }
    }

    public class BenhNhan24272
    {
        public int? MaKP { get; set; }
        public int? MaBNhan { get; set; }
        public string TenBNhan { get; set; }
        public int? Tuoi { get; set; }
        public string DChi { get; set; }
        public int? SoTT { get; set; }
        public string SThe { get; set; }
        public int? Tuyen { get; set; }
        public string DTuong { get; set; }

        public string NNhap { get; set; }
        public int? GTinh { get; set; }
        public byte IDDTBN { get; set; }
        public string NamSinh { get; set; }
        public string NgayNghi { get; set; }
        public int? Status { get; set; }
    }

    public class PhanLoaiTT
    {
        public int SoPhanLoai { get; set; }
        public string TenPhanLoai { get; set; }
    }
}
