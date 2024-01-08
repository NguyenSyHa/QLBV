using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
namespace QLBV.BHYT
{
    public partial class us_BHXaPhuong : DevExpress.XtraEditors.XtraUserControl
    {
        public us_BHXaPhuong()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<KPhong> _lkhoap = new List<KPhong>();
        List<vienphi> _lvp = new List<vienphi>();
        int _thang = 0, nam = 0;
        int ttluu = 0;
        List<lNhom> _lnhom = new List<lNhom>();
        private void us_BHXaPhuong_Load(object sender, EventArgs e)
        {
            cboNam.Text = System.DateTime.Now.Year.ToString();
            cboThang.Text = System.DateTime.Now.Month.ToString();

            //grcNhom.DataSource = _lnhom.ToList();
            dtNgayNhap.DateTime = System.DateTime.Now;
            _lkhoap = _Data.KPhongs.Where(p => p.PLoai.Contains("Xã phường")).ToList();
            grcDSXP.DataSource = _lkhoap;
            var dv = _Data.DichVus.Where(p => p.PLoai == 3);
            _lvp.Clear();
            foreach (var a in dv)
            {
                vienphi vp = new vienphi();
                vp.MaDV = a.MaDV;
                vp.TenDV = a.TenDV;
                vp.TongTien = 0;
                if (a.SoTT != null)
                    vp.STT = a.SoTT.Value;
                else
                    vp.STT = 0;
                _lvp.Add(vp);
            }
            grcVienPhi.DataSource = _lvp;
            lupNhom.EditValue = "HT";
            lupNhom.EditValue = "DN";
            var dtbn = _Data.DTBNs.Where(p => p.HTTT == 1).Select(p => p.IDDTBN).ToList();
            if (dtbn.Count > 0)
                iddtbn = dtbn.First();
            var bn = _Data.BenhNhans.Where(p => p.TuyenDuoi == 1 && p.IDDTBN==0).ToList();
            foreach (var item in bn) {
                item.IDDTBN = iddtbn;
                _Data.SaveChanges();
            }
        }
        int  _mabn = 0;
        string _sothe = "";
        private void grvDSXP_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            lupNhom.EditValue = "HT";
            lupNhom.EditValue = "DN";
            cboThang_SelectedIndexChanged(sender, e);
            //grcVienPhi.DataSource = null;
        }
        public class vienphi
        {
            public int madv;
            public string tendv;
            public double tongtien;
            public int stt;
            public int STT
            {
                set { stt = value; }
                get { return stt; }
            }
            public int MaDV
            {
                set { madv = value; }
                get { return madv; }
            }
            public string TenDV
            {
                set { tendv = value; }
                get { return tendv; }
            }
            public double TongTien
            {
                set { tongtien = value; }
                get { return tongtien; }
            }
        }
        public class lNhom
        {
            public string madtuong;
            public string nhom;
            public string MaDTuong
            {
                set { madtuong = value; }
                get { return madtuong; }
            }
            public string Nhom
            {
                set { nhom = value; }
                get { return nhom; }
            }
        }
        public class lNhomDT
        {
            public string madtuong;
            public string nhom;
            public int soluot;
            public double thanhtien;
            public double ThanhTien
            {
                set { thanhtien = value; }
                get { return thanhtien; }
            }
            public int SoLuot
            {
                set { soluot = value; }
                get { return soluot; }
            }
            public string MaDTuong
            {
                set { madtuong = value; }
                get { return madtuong; }
            }
            public string Nhom
            {
                set { nhom = value; }
                get { return nhom; }
            }
        }
        byte iddtbn = 99;
        private void btnOK_Click(object sender, EventArgs e)
        {
            _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (!string.IsNullOrEmpty(txtTenBNhan.Text) && !string.IsNullOrEmpty(lupNhom.Text))
            {
                if (!string.IsNullOrEmpty(dtNgayNhap.Text))
                {
                    if (ttluu == 1)
                    {
                        // luu bn
                       
                      
                        DateTime _dtNhap = DateTime.Now ;   // bien de tim kiem mabenhnhan moi nhap 
                        string _tenBN = "";
                        BenhNhan bn = new BenhNhan();
                       
                        bn.TuyenDuoi = 1;
                        _tenBN = txtTenBNhan.Text.Trim();
                        bn.TenBNhan = _tenBN;
                                      
                        bn.MaCS = txtMaXP.Text;                       
                        _dtNhap = dtNgayNhap.DateTime;
                        bn.NNhap = _dtNhap;
                        bn.DTuong = "BHYT";
                        bn.IDDTBN = iddtbn;
                        bn.SThe = lupNhom.EditValue.ToString();
                        bn.MaDTuong = lupNhom.EditValue.ToString();
                        bn.MaKCB = txtMaXP.Text;
                        if (radChon.SelectedIndex == 1)
                        {
                            string _Ten = "";
                            if (grvNhom.GetFocusedRowCellValue(colNhom) != null && grvNhom.GetFocusedRowCellValue(colNhom).ToString() != "")
                            {
                                _Ten = grvNhom.GetFocusedRowCellValue(colNhom).ToString();
                            }
                            if (_Ten.Contains("Đúng tuyến"))
                            {
                                bn.Tuyen = 1;
                            }
                            else
                            {
                                bn.Tuyen = 2;
                            }
                            if (_Ten.Contains("ban đầu"))
                            {
                                bn.NoiTinh = 1;
                            }
                            else
                            {
                                if (_Ten.Contains("Nội tỉnh đến"))
                                    bn.NoiTinh = 2;
                                else
                                    bn.NoiTinh = 3;
                            }

                        }
                        else
                        {
                            bn.Tuyen = 1;
                            bn.NoiTinh = 1;
                        }

                        if (!string.IsNullOrEmpty(txtSoLuot.Text))
                            bn.Tuoi = Convert.ToInt32(txtSoLuot.Text);
                        else bn.Tuoi = 0;
                        bn.NoiTru = 0;
                        bn.GTinh = 2;
                        _Data.BenhNhans.Add(bn);
                        if (_Data.SaveChanges() >= 0)
                        {
                            _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                            int MSBN = _Data.BenhNhans.Where(p => p.NNhap == _dtNhap).Where(p=>p.TenBNhan == _tenBN).Max(p => p.MaBNhan);
                            VienPhi vp = new VienPhi();
                            vp.MaBNhan = MSBN;
                            vp.NgayTT = dtNgayNhap.DateTime;
                            vp.NgayRa = dtNgayNhap.DateTime;
                            vp.MaCB = DungChung.Bien.MaCB;
                            _Data.VienPhis.Add(vp);
                            if (_Data.SaveChanges() >= 0)
                            {
                                int idvp = 0;
                                var q = _Data.VienPhis.Where(p => p.MaBNhan == MSBN).OrderByDescending(p => p.idVPhi).ToList();
                                if (q.Count > 0)
                                {
                                    idvp = q.First().idVPhi;
                                }
                                for (int i = 0; i < grvVienPhi.RowCount; i++)
                                {
                                    if (grvVienPhi.GetRowCellValue(i, colMaDV) != null)
                                    {
                                        VienPhict vpct = new VienPhict();
                                        vpct.idVPhi = idvp;
                                        vpct.DonGia = 1;
                                        vpct.DonVi = "";
                                        vpct.Duyet = 0;
                                        vpct.MaDV = Convert.ToInt32( grvVienPhi.GetRowCellValue(i, colMaDV));
                                        if (grvVienPhi.GetRowCellValue(i, colThanhTien) != null && grvVienPhi.GetRowCellValue(i, colThanhTien).ToString() != "")
                                        {
                                            vpct.SoLuong = Convert.ToDouble(grvVienPhi.GetRowCellValue(i, colThanhTien));
                                            vpct.ThanhTien = Convert.ToDouble(grvVienPhi.GetRowCellValue(i, colThanhTien));
                                            vpct.TienBH = Convert.ToDouble(grvVienPhi.GetRowCellValue(i, colThanhTien));
                                        }
                                        else
                                        {
                                            vpct.SoLuong = 0;
                                            vpct.ThanhTien = 0;
                                            vpct.TienBH = 0;
                                        }

                                        vpct.SoLuongD = 0;
                                        vpct.TienBN = 0;
                                        vpct.TrongBH = 1;
                                        vpct.TienChenh = 0;
                                        vpct.TienChenhBN = 0;
                                        _Data.VienPhicts.Add(vpct);
                                        _Data.SaveChanges();
                                    }
                                }
                                RaVien rv = new RaVien();
                                rv.MaBNhan = MSBN;
                                rv.NgayRa = dtNgayNhap.DateTime;
                                rv.MaKP = 0;
                                rv.MaICD = "";
                                _Data.RaViens.Add(rv);
                                _Data.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        // trường hợp sửa
                        int idvp = 0;
                        var ktvp = _Data.VienPhis.Where(p => p.MaBNhan == _mabn).ToList();
                        if (ktvp.Count > 0)
                            idvp = ktvp.First().idVPhi;
                        var vp = _Data.VienPhis.Single(p => p.idVPhi == idvp);
                        vp.NgayTT = dtNgayNhap.DateTime;
                        _Data.SaveChanges();
                        var vpct1 = _Data.VienPhicts.Where(p => p.idVPhi == idvp).ToList();
                        foreach (var a in vpct1)
                        {
                            var vpupdate = _Data.VienPhicts.Single(p => p.idVPhict == a.idVPhict);
                            _Data.VienPhicts.Remove(vpupdate);
                            _Data.SaveChanges();
                        }
                        for (int i = 0; i < grvVienPhi.RowCount; i++)
                        {
                            if (grvVienPhi.GetRowCellValue(i, colMaDV) != null )
                            {
                                VienPhict vpct = new VienPhict();
                                vpct.idVPhi = idvp;
                                vpct.DonGia = 1;
                                vpct.DonVi = "";
                                vpct.Duyet = 0;
                                vpct.TrongBH = 1;
                                vpct.MaDV =  Convert.ToInt32( grvVienPhi.GetRowCellValue(i, colMaDV));
                                if (grvVienPhi.GetRowCellValue(i, colThanhTien) != null && grvVienPhi.GetRowCellValue(i, colThanhTien).ToString() != "")
                                {
                                    vpct.SoLuong = Convert.ToDouble(grvVienPhi.GetRowCellValue(i, colThanhTien));
                                    vpct.ThanhTien = Convert.ToDouble(grvVienPhi.GetRowCellValue(i, colThanhTien));
                                    vpct.TienBH = Convert.ToDouble(grvVienPhi.GetRowCellValue(i, colThanhTien));
                                }
                                else
                                {
                                    vpct.SoLuong = 0;
                                    vpct.ThanhTien = 0;
                                    vpct.TienBH = 0;
                                }

                                vpct.SoLuongD = 0;
                                vpct.TienBN = 0;
                                vpct.TrongBH = 1;
                                vpct.TienChenh = 0;
                                vpct.TienChenhBN = 0;
                                _Data.VienPhicts.Add(vpct);
                                _Data.SaveChanges();
                            }
                        }
                        var bn = _Data.BenhNhans.Single(p => p.MaBNhan == _mabn);
                        bn.TuyenDuoi = 1;
                        bn.MaCS = txtMaXP.Text;
                        if (!string.IsNullOrEmpty(txtSoLuot.Text))
                            bn.Tuoi = Convert.ToInt32(txtSoLuot.Text);
                        else
                            bn.Tuoi = 0;
                        bn.NNhap = dtNgayNhap.DateTime;
                        _Data.SaveChanges();
                        var ravien = _Data.RaViens.Single(p => p.MaBNhan == _mabn);
                        ravien.NgayRa = dtNgayNhap.DateTime;
                        _Data.SaveChanges();
                    }
                    cboThang.Text = dtNgayNhap.DateTime.Month.ToString();
                    cboNam.Text = dtNgayNhap.DateTime.Year.ToString();
                    MessageBox.Show("Lưu thành công");
                    // us_BHXaPhuong_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Bạn chưa chọn ngày nhập");
                }
            }// ktra ten bn
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_mabn>0)
            {
                DialogResult _result = MessageBox.Show("Bạn muốn xóa chi phí xã: " + txtTenBNhan.Text, "hỏi xóa!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_result == DialogResult.Yes)
                {
                    var bn = _Data.BenhNhans.Single(p => p.MaBNhan == _mabn);
                    _Data.BenhNhans.Remove(bn);
                    _Data.SaveChanges();
                    var ravien = _Data.RaViens.Single(p => p.MaBNhan == _mabn);
                    _Data.RaViens.Remove(ravien);
                    _Data.SaveChanges();
                    int idvp = 0;
                    var ktvp = _Data.VienPhis.Where(p => p.MaBNhan == _mabn).ToList();
                    if (ktvp.Count > 0)
                        idvp = ktvp.First().idVPhi;
                    var vp = _Data.VienPhis.Single(p => p.idVPhi == idvp);
                    _Data.VienPhis.Remove(vp);
                    _Data.SaveChanges();
                    var vpct1 = _Data.VienPhicts.Where(p => p.idVPhi == idvp).ToList();
                    foreach (var a in vpct1)
                    {
                        var vpupdate = _Data.VienPhicts.Single(p => p.idVPhict == a.idVPhict);
                        _Data.VienPhicts.Remove(vpupdate);
                        _Data.SaveChanges();
                    }
                    MessageBox.Show("Xóa thành công");
                    us_BHXaPhuong_Load(sender, e);
                }
            }
        }

        private void lupNhom_EditValueChanged(object sender, EventArgs e)
        {
            if (grvDSXP.GetFocusedRowCellValue(colTenKP) != null && grvDSXP.GetFocusedRowCellValue(colTenKP).ToString() != "")
            {
                txtTenBNhan.Text = grvDSXP.GetFocusedRowCellValue(colTenKP).ToString().Trim();
                txtMaXP.Text = grvDSXP.GetFocusedRowCellValue(colMaKP).ToString().Trim();
            }
            else
            {
                txtTenBNhan.Text = "";
                txtMaXP.Text = "";
            }
            if (lupNhom.EditValue != null && lupNhom.EditValue.ToString() != "")
            {
                _sothe = lupNhom.EditValue.ToString();
            }
            if (!string.IsNullOrEmpty(txtTenBNhan.Text))
            {
                var ktbn = _Data.BenhNhans.Where(p => p.TenBNhan.Contains(txtTenBNhan.Text)).Where(p => p.SThe== (_sothe)).Where(p => p.NNhap.Value.Month == _thang).ToList();
                if (ktbn.Count > 0)
                {
                    ttluu = 2;
                    _mabn = ktbn.First().MaBNhan;
                    if (ktbn.First().Tuoi != null)
                        txtSoLuot.Text = ktbn.First().Tuoi.ToString();
                    else
                        txtSoLuot.Text = "";
                    dtNgayNhap.DateTime = ktbn.First().NNhap.Value;

                }
                else
                {
                    ttluu = 1;
                    _mabn = 0;
                    _sothe = "";
                    txtSoLuot.Text = "";
                }
                if (ktbn.Count > 0)
                {

                    int mabn = 0;
                    mabn = ktbn.First().MaBNhan;
                    var vp = (from v in _Data.VienPhis.Where(p => p.MaBNhan == mabn)
                              join vct in _Data.VienPhicts on v.idVPhi equals vct.idVPhi
                              select vct).ToList();
                    foreach (var a in vp)
                    {
                        foreach (var b in _lvp)
                        {
                            if (b.MaDV == a.MaDV)
                            {
                                b.TongTien = a.ThanhTien;
                                break;
                            }
                        }
                    }
                    //dtNgayNhap.DateTime=vp.First()
                    grcVienPhi.DataSource = null;
                    grcVienPhi.DataSource = _lvp.OrderBy(p => p.STT).ThenBy(p => p.TenDV);
                }
                else
                {
                    foreach (var b in _lvp)
                    {

                        b.TongTien = 0;

                    }
                    grcVienPhi.DataSource = null;
                    grcVienPhi.DataSource = _lvp.OrderBy(p => p.STT).ThenBy(p => p.TenDV); ;
                }
            }
        }


        private void grvNhom_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvNhom.GetFocusedRowCellValue(colMaNhom) != null && grvNhom.GetFocusedRowCellValue(colMaNhom).ToString() != "")
            {
                string _manhom = grvNhom.GetFocusedRowCellValue(colMaNhom).ToString();
                lupNhom.EditValue = "";
                lupNhom.EditValue = _manhom;
            }
        }

        private void grvNhom_DataSourceChanged(object sender, EventArgs e)
        {
            grvNhom_FocusedRowChanged(null, null);
        }
        private int _soLuot(int thang, int nam, string tenbn, string madt)
        {
            int sl = 0;
            var sluot = _Data.BenhNhans.Where(p => p.TenBNhan== (tenbn)).Where(p => p.NNhap.Value.Month == thang && p.NNhap.Value.Year == nam).Where(p => p.SThe== (madt)).Select(p => p.Tuoi).ToList();
            if (sluot.Count > 0 && sluot.First().Value > 0)
                sl = sluot.First().Value;
            return sl;
        }
        private double _thanhtien(int thang, int nam, string tenbn, string madt)
        {
            double sl = 0;
            var sluot = (from bn in _Data.BenhNhans.Where(p => p.TenBNhan== (tenbn)).Where(p => p.NNhap.Value.Month == thang && p.NNhap.Value.Year == nam).Where(p => p.SThe== (madt))
                         join vp in _Data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                         join vpct in _Data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                         group new { vpct, vp } by new { vp.MaBNhan } into kq
                         select new { ThanhTien = kq.Sum(p => p.vpct.ThanhTien) }
                           ).ToList();
            if (sluot.Count > 0 && sluot.First().ThanhTien > 0)
                sl = sluot.First().ThanhTien;
            return sl;
        }
        private void DS()
        {
            _thang = cboThang.SelectedIndex;
            nam = Convert.ToInt32(cboNam.Text);
            List<lNhomDT> _lnhomdt = new List<lNhomDT>();
            if (radChon.SelectedIndex == 0)
            {
                _lnhomdt.Clear();
                _lnhom.Clear();
                lNhom news = new lNhom();
                news.MaDTuong = "DN";
                news.Nhom = "Nhóm 1";
                _lnhom.Add(news);
                lNhom news2 = new lNhom();
                news2.MaDTuong = "HT";
                news2.Nhom = "Nhóm 2";
                _lnhom.Add(news2);
                lNhom news3 = new lNhom();
                news3.MaDTuong = "HN";
                news3.Nhom = "Nhóm 3";
                _lnhom.Add(news3);
                lNhom news4 = new lNhom();
                news4.MaDTuong = "TE";
                news4.Nhom = "Nhóm 4";
                _lnhom.Add(news4);
                lNhom news5 = new lNhom();
                news5.MaDTuong = "HS";
                news5.Nhom = "Nhóm 5";
                _lnhom.Add(news5);
                lNhom news6 = new lNhom();
                news6.MaDTuong = "XV";
                news6.Nhom = "Nhóm 6";
                _lnhom.Add(news6);
                lupNhom.Properties.DataSource = _lnhom.ToList();
                foreach (var a in _lnhom)
                {
                    lNhomDT moi = new lNhomDT();
                    moi.MaDTuong = a.MaDTuong;
                    moi.Nhom = a.Nhom;
                    moi.SoLuot = _soLuot(_thang, nam, txtTenBNhan.Text, a.MaDTuong);
                    moi.ThanhTien = _thanhtien(_thang, nam, txtTenBNhan.Text, a.MaDTuong);
                    _lnhomdt.Add(moi);
                }
            }
            else
            {
                _lnhomdt.Clear();
                _lnhom.Clear();
                lNhom news2 = new lNhom();
                news2.MaDTuong = "DN";
                news2.Nhom = "Nội tỉnh đăng ký KBC ban đầu - Đúng tuyến";
                _lnhom.Add(news2);
                lNhom news3 = new lNhom();
                news3.MaDTuong = "HT";
                news3.Nhom = "Nội tỉnh đăng ký KBC ban đầu - Trái tuyến";
                _lnhom.Add(news3);
                lNhom news4 = new lNhom();
                news4.MaDTuong = "HN";
                news4.Nhom = "Nội tỉnh đến - Đúng tuyến";
                _lnhom.Add(news4);
                lNhom news5 = new lNhom();
                news5.MaDTuong = "TE";
                news5.Nhom = "Nội tỉnh đến - Trái tuyến";
                _lnhom.Add(news5);
                lNhom news6 = new lNhom();
                news6.MaDTuong = "HS";
                news6.Nhom = "Bệnh nhân ngoại tỉnh đến - Đúng tuyến";
                _lnhom.Add(news6);
                lNhom news7 = new lNhom();
                news7.MaDTuong = "XV";
                news7.Nhom = "Bệnh nhân ngoại tỉnh đến - Trái tuyến";
                _lnhom.Add(news7);
                lupNhom.Properties.DataSource = _lnhom.ToList();
                foreach (var a in _lnhom)
                {
                    lNhomDT moi = new lNhomDT();
                    moi.MaDTuong = a.MaDTuong;
                    moi.Nhom = a.Nhom;
                    moi.SoLuot = _soLuot(_thang, nam, txtTenBNhan.Text, a.MaDTuong);
                    moi.ThanhTien = _thanhtien(_thang, nam, txtTenBNhan.Text, a.MaDTuong);
                    _lnhomdt.Add(moi);
                }
            }
            grcNhom.DataSource = "";
            grcNhom.DataSource = _lnhomdt.ToList();
        }

        private void cboThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            DS();
        }


        private void btnIn_Click(object sender, EventArgs e)
        {
            string ten = "";
            int thang = cboThang.SelectedIndex, nam = Convert.ToInt32(cboNam.Text);
            ten = txtTenBNhan.Text;
            //FormThamSo.frm_14a_BHYT frm = new FormThamSo.frm_14a_BHYT(ten);
            //frm.ShowDialog();
            BaoCao.rep_14aBK rep = new BaoCao.rep_14aBK();
            frmIn frm = new frmIn();
            QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var q2 = (from DT in Data.DTuongs
                      join BN in Data.BenhNhans.Where(p => p.TenBNhan.Contains(ten) && p.TuyenDuoi == 1) on DT.MaDTuong equals BN.MaDTuong
                      join vp in Data.VienPhis on BN.MaBNhan equals vp.MaBNhan
                      join vpct in Data.VienPhicts.Where(p => p.TrongBH == 1) on vp.idVPhi equals vpct.idVPhi
                      join dv in Data.DichVus on vpct.MaDV equals dv.MaDV
                      join Nhom in Data.NhomDVs on dv.IDNhom equals Nhom.IDNhom
                      where (vp.NgayTT.Value.Month == thang && vp.NgayTT.Value.Year == nam)
                      select new
                      {
                          DT.Nhom,
                          BN.Tuoi,
                          BN.Tuyen,
                          BN.TuyenDuoi,
                          vpct.ThanhTien,
                          vpct.TienBH,
                          vpct.TienBN,
                          Nhom.TenNhomCT,
                          vpct.TienChenh
                          ,
                          vpct.TienChenhBN
                      }).ToList();
            var q = (from a in q2
                     group a by new { a.TuyenDuoi, a.Nhom, a.Tuoi } into kq
                     select new
                     {
                         HangBV = kq.Key.TuyenDuoi,
                         Tentuyen = kq.Key.Nhom,
                         Soluotdungtuyen = kq.Key.Tuoi,
                         Xetnghiem = kq.Where(p => p.TenNhomCT.Contains("Xét nghiệm")).Sum(p => p.ThanhTien) - kq.Where(p => p.TenNhomCT.Contains("Xét nghiệm")).Sum(p => p.TienChenh) - kq.Where(p => p.TenNhomCT.Contains("Xét nghiệm")).Sum(p => p.TienChenhBN),
                         CDHA = kq.Where(p => p.TenNhomCT.Contains("Chẩn đoán hình ảnh")).Sum(p => p.ThanhTien) - kq.Where(p => p.TenNhomCT.Contains("Chẩn đoán hình ảnh")).Sum(p => p.TienChenh) - kq.Where(p => p.TenNhomCT.Contains("Chẩn đoán hình ảnh")).Sum(p => p.TienChenhBN),
                         ThuocDT = kq.Where(p => p.TenNhomCT.Contains( "Thuốc trong danh mục BHYT" )).Sum(p => p.ThanhTien) - kq.Where(p => p.TenNhomCT.Contains( "Thuốc trong danh mục BHYT" )).Sum(p => p.TienChenh) - kq.Where(p => p.TenNhomCT.Contains( "Thuốc trong danh mục BHYT" )).Sum(p => p.TienChenhBN),
                         mau = kq.Where(p => p.TenNhomCT.Contains("Máu và chế phẩm của máu")).Sum(p => p.ThanhTien) - kq.Where(p => p.TenNhomCT.Contains("Máu và chế phẩm của máu")).Sum(p => p.TienChenh) - kq.Where(p => p.TenNhomCT.Contains("Máu và chế phẩm của máu")).Sum(p => p.TienChenhBN),
                         TTPT = kq.Where(p => p.TenNhomCT.Contains("Thủ thuật, phẫu thuật")).Sum(p => p.ThanhTien) - kq.Where(p => p.TenNhomCT.Contains("Thủ thuật, phẫu thuật")).Sum(p => p.TienChenh) - kq.Where(p => p.TenNhomCT.Contains("Thủ thuật, phẫu thuật")).Sum(p => p.TienChenhBN),
                         VTYTtieuhao = kq.Where(p => p.TenNhomCT.Contains("Vật tư y tế trong danh mục BHYT")).Sum(p => p.ThanhTien) - kq.Where(p => p.TenNhomCT.Contains("Vật tư y tế trong danh mục BHYT")).Sum(p => p.TienChenh) - kq.Where(p => p.TenNhomCT.Contains("Vật tư y tế trong danh mục BHYT")).Sum(p => p.TienChenhBN),
                         VTYTthaythe = kq.Where(p => p.TenNhomCT.Contains("VTYT thanh toán theo tỷ lệ")).Sum(p => p.ThanhTien) - kq.Where(p => p.TenNhomCT.Contains("VTYT thanh toán theo tỷ lệ")).Sum(p => p.TienChenh) - kq.Where(p => p.TenNhomCT.Contains("VTYT thanh toán theo tỷ lệ")).Sum(p => p.TienChenhBN),
                         DVKTcao = kq.Where(p => p.TenNhomCT.Contains("DVKT thanh toán theo tỷ lệ")).Sum(p => p.ThanhTien) - kq.Where(p => p.TenNhomCT.Contains("DVKT thanh toán theo tỷ lệ")).Sum(p => p.TienChenh) - kq.Where(p => p.TenNhomCT.Contains("DVKT thanh toán theo tỷ lệ")).Sum(p => p.TienChenhBN),
                         Thuocthaighep = kq.Where(p => p.TenNhomCT.Contains("Thuốc điều trị ung thư, chống thải ghép ngoài danh mục")).Sum(p => p.ThanhTien) - kq.Where(p => p.TenNhomCT.Contains("Thuốc điều trị ung thư, chống thải ghép ngoài danh mục")).Sum(p => p.TienChenh) - kq.Where(p => p.TenNhomCT.Contains("Thuốc điều trị ung thư, chống thải ghép ngoài danh mục")).Sum(p => p.TienChenhBN),
                         Tienkham = kq.Where(p => p.TenNhomCT.Contains("Khám bệnh")).Sum(p => p.ThanhTien) - kq.Where(p => p.TenNhomCT.Contains("Khám bệnh")).Sum(p => p.TienChenh) - kq.Where(p => p.TenNhomCT.Contains("Khám bệnh")).Sum(p => p.TienChenhBN),
                         Vanchuyen = kq.Where(p => p.TenNhomCT.Contains("Vận chuyển")).Sum(p => p.ThanhTien) - kq.Where(p => p.TenNhomCT.Contains("Vận chuyển")).Sum(p => p.TienChenh) - kq.Where(p => p.TenNhomCT.Contains("Vận chuyển")).Sum(p => p.TienChenhBN),
                         Benhnhandungtuyen = kq.Where(p => p.Tuyen == 1).Sum(p => p.TienBN) - kq.Where(p => p.Tuyen == 1).Sum(p => p.TienChenhBN),
                         Benhnhantraituyen = kq.Where(p => p.Tuyen == 2).Sum(p => p.TienBN) - kq.Where(p => p.Tuyen == 2).Sum(p => p.TienChenhBN),
                         BHXHchitra = kq.Sum(p => p.TienBH) - kq.Sum(p => p.TienChenh),
                         Tongcong = kq.Sum(p => p.ThanhTien) - kq.Sum(p => p.TienChenh) - kq.Sum(p => p.TienChenhBN),
                         BHXHtuchoithanhtoan = kq.Sum(p => p.TienChenh),
                     }).OrderBy(p => p.HangBV).OrderBy(p => p.Tentuyen).ToList();


            rep.Quy.Value = "Tháng " + thang + " -- tại đơn vị " + ten;
            rep.ngaytu.Value = thang;
            rep.ngayden.Value = ten;
            rep.TenDV.Value = DungChung.Bien.TenCQ;
            rep.Macs.Value = DungChung.Bien.MaBV;
            rep.DataSource = q;
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void radChon_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboThang_SelectedIndexChanged(sender, e);
            if (radChon.SelectedIndex == 0)
            {
                labelControl4.Text = "Nhóm:";
            }
            else
                labelControl4.Text = "N|Ng tỉnh:";
        }

        private void cboNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboThang_SelectedIndexChanged(sender, e);
        }

        private void btnIn79_Click(object sender, EventArgs e)
        {
            string ten = "";
            int thang = cboThang.SelectedIndex, _nam = Convert.ToInt32(cboNam.Text);
            ten = txtTenBNhan.Text.Trim();
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var q2 = (from bn in _dataContext.BenhNhans.Where(p => p.TenBNhan== (ten) && p.TuyenDuoi == 1 && p.NoiTru == 0).Where(p => p.DTuong == "BHYT")
                      join vp in _dataContext.VienPhis on bn.MaBNhan equals vp.MaBNhan
                      join vpct in _dataContext.VienPhicts.Where(p => p.TrongBH == 1) on vp.idVPhi equals vpct.idVPhi
                      join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                      where (vp.NgayTT.Value.Month == thang && vp.NgayTT.Value.Year == _nam)
                      select new
                      {
                          bn.DTuong,
                          bn.NoiTru,
                          vpct.TrongBH,
                          // bn.MaBNhan,
                          bn.NoiTinh,
                          bn.Tuyen,
                          rv.MaKP,
                          bn.MaBNhan,
                          bn.TenBNhan,
                          bn.NamSinh,
                          bn.SThe,
                          bn.GTinh,
                          bn.MaCS,
                          rv.MaICD,
                          rv.NgayVao,
                          rv.NgayRa,
                          vpct.ThanhTien,
                          vpct.TienBN,
                          vpct.TienBH,
                          vpct.MaDV,
                      }).OrderBy(p => p.MaBNhan).ToList();
            var nhomdv=(from    dv in _dataContext.DichVus  join nh in _dataContext.NhomDVs
                                                                on dv.IDNhom equals nh.IDNhom select new{dv.BHTT,dv.IDNhom,nh.TenNhomCT,dv.MaDV}
                                                                ).ToList();
            var q = (from a in q2
                     join nh in nhomdv     on a.MaDV equals nh.MaDV
                     group new{a,nh} by new { a.DTuong, a.NoiTru, a.TrongBH, a.NoiTinh, a.MaBNhan, a.TenBNhan, a.NamSinh, a.GTinh, a.MaCS, a.Tuyen, a.NgayVao, a.MaKP, a.MaICD, a.NgayRa } into kq
                     select new
                     {
                         kq.Key.DTuong,
                         kq.Key.NoiTru,
                         kq.Key.TrongBH,

                         NoiTinh = kq.Key.NoiTinh,
                         Tuyen = kq.Key.Tuyen,
                         Makp = kq.Key.MaKP,
                         Mabn = kq.Key.MaBNhan,
                         Ho_ten = kq.Key.TenBNhan,
                         NSinh = kq.Key.NamSinh,
                         //  SThe = kq.Key.SThe,
                         GTinh = kq.Key.GTinh,
                         Ma_dkbd = kq.Key.MaCS,
                         MaICD = kq.Key.MaICD,
                         Ngaykham = kq.Key.NgayRa,
                         Ngayra = kq.Key.NgayRa,
                         T_thuoc = kq.Where(p => p.nh.TenNhomCT == "Thuốc trong danh mục BHYT").Sum(p => p.a.ThanhTien),
                         T_cdha = kq.Where(p => p.nh.TenNhomCT == "Chẩn đoán hình ảnh" || p.nh.TenNhomCT == "Thăm dò chức năng").Sum(p => p.a.ThanhTien),
                         T_kham = kq.Where(p => p.nh.TenNhomCT == "Khám bệnh").Sum(p => p.a.ThanhTien),
                         T_xn = kq.Where(p => p.nh.TenNhomCT == "Xét nghiệm").Sum(p => p.a.ThanhTien),
                         T_mau = kq.Where(p => p.nh.TenNhomCT == "Máu và chế phẩm của máu").Sum(p => p.a.ThanhTien),
                         T_pttt = kq.Where(p => p.nh.TenNhomCT == "Thủ thuật, phẫu thuật").Sum(p => p.a.ThanhTien),
                         T_vtyt = kq.Where(p => p.nh.TenNhomCT == "Vật tư y tế trong danh mục BHYT").Sum(p => p.a.ThanhTien),
                         T_vtyt_tyle = kq.Where(p => p.nh.TenNhomCT == "VTYT thanh toán theo tỷ lệ").Sum(p => p.a.ThanhTien),
                         T_dvkt_tyle = kq.Where(p => p.nh.TenNhomCT == "DVKT thanh toán theo tỷ lệ").Sum(p => p.a.ThanhTien),
                         T_thuoc_tyle = kq.Where(p => p.nh.TenNhomCT == "Thuốc thanh toán theo tỷ lệ").Sum(p => p.a.ThanhTien),
                         T_vchuyen = kq.Where(p => p.nh.TenNhomCT == "Vận chuyển").Sum(p => p.a.ThanhTien),
                         ThanhTien = kq.Sum(p => p.a.ThanhTien),
                         Tongcong = kq.Sum(p => p.a.ThanhTien),
                         T_bntt = kq.Sum(p => p.a.TienBN),
                         T_bhtt = kq.Sum(p => p.a.TienBH),
                     }).OrderBy(p => p.Mabn).ToList();

            frmIn frmv = new frmIn();
            BaoCao.Rep_79a_HD_1399 repv = new BaoCao.Rep_79a_HD_1399(2);
            repv.DataSource = q.OrderBy(p => p.Mabn);
            repv.Ngaythang.Value = "Tháng: "+thang+" năm: "+nam;
            repv.MaCS.Value = DungChung.Bien.MaBV.Substring(0, 2) + "-" + DungChung.Bien.MaBV.Substring(2, 3);
            repv.ngaythanghientai.Value = DungChung.Ham.NgaySangChu(System.DateTime.Now);
            double st = 0;
            st = q.Sum(a => a.T_bhtt);
            repv.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
            repv.BindingData("{0:dd/MM/yy}",2);
            repv.CreateDocument();
            frmv.prcIN.PrintingSystem = repv.PrintingSystem;
            frmv.ShowDialog();

        }

      
    }
}
