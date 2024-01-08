using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

namespace QLBV.FormNhap
{
    public partial class us_KiemKe : DevExpress.XtraEditors.XtraUserControl
    {
        public us_KiemKe()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<NhapD> _lNhapDuoc = new List<NhapD>();
        List<NhapDct> _lNhapDct = new List<NhapDct>();
        List<DonGia24012> _lDonGia24012 = new List<DonGia24012>();
        int _ppxd = 1;
        private void Enablebutton(bool Status)
        {
            btnLuu.Enabled = !Status;
            btnMoi.Enabled = Status;
            btnSua.Enabled = Status;
            btnXoa.Enabled = Status;
            grvNhapCT.OptionsBehavior.Editable = !Status;
        }
        private void EnableControl(bool status)
        {
            dtNgayNhap.Properties.ReadOnly = !status;
            txtSoCT.Properties.ReadOnly = !status;
            lupMaKP.Properties.ReadOnly = !status;
            txtGhiChu.Properties.ReadOnly = !status;
            grcNhap.Enabled = !status;
        }
        private void ResetControl()
        {
            dtNgayNhap.EditValue = System.DateTime.Now;
            txtIDNhap.Text = "";
            txtSoCT.Text = "";
            txtGhiChu.Text = "";
            lupMaKP.EditValue = 0;
        }
        #region KT
        //Kiem tra trước khi lưu
        private bool KT()
        {
            if (dtNgayNhap.EditValue == null || dtNgayNhap.EditValue.ToString() == "")
            {
                MessageBox.Show("Bạn chưa chọn Ngày xuất!");
                dtNgayNhap.Focus();
                return false;
            }
            if (lupMaKP.EditValue == null || lupMaKP.EditValue.ToString() == "")
            {
                MessageBox.Show("Bạn chưa chon kho xuất!");
                lupMaKP.Focus();
                return false;
            }
            return true;
        }
        #endregion
        DateTime _dttu = System.DateTime.Now;
        DateTime _dtden = System.DateTime.Now;
        int _makho = 0;
        string _soPhieu = "";
        string _mkpnx = "";
        int TTLuu = 0;
        #region hàm tìm kiếm
        private void TimKiem()
        {
            _dttu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
            _dtden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
            if (lupTimMaKP.EditValue != null && lupTimMaKP.EditValue.ToString() != "")
                _makho = lupTimMaKP.EditValue == null ? 0 : Convert.ToInt32( lupTimMaKP.EditValue);
            if (!string.IsNullOrEmpty(txtTimKiem.Text) && txtTimKiem.Text != "Số phiếu|Số CT")
                _soPhieu = txtTimKiem.Text;
                _lNhapDuoc = (from nd in _data.NhapDs.Where(p => p.PLoai == 4)
                              where (nd.NgayNhap >= _dttu && nd.NgayNhap <= _dtden)
                              where (nd.SoCT.Contains(_soPhieu))
                              where (nd.MaKP ==_makho)
                              select nd).OrderByDescending(p => p.NgayNhap).OrderByDescending(p => p.IDNhap).ToList();
            grcNhap.DataSource = _lNhapDuoc.ToList();
        }
        #endregion
        public class ClassA {
            public string TenKP { get; set; }
            public int MaKP { get; set; }
        }
        private void usXuatDuoc_Load(object sender, EventArgs e)
        {
            Enablebutton(true);
            EnableControl(false);
            dtTimDenNgay.DateTime = System.DateTime.Now;
            dtTimTuNgay.DateTime = System.DateTime.Now;
            dtNgayNhap.EditValue = System.DateTime.Now;
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin || DungChung.Bien.MaBV == "30009")
            {
                lupTimMaKP.Enabled = true;
                if(DungChung.Bien.MaBV == "30009")
                {
                    lupMaKP.Enabled = true;
                }
            }
            else
            {
                lupTimMaKP.Enabled = false;
            }
            var q = (from KhoaKham in _data.KPhongs.Where(p => p.PLoai == ("Khoa dược")).Where(p => p.Status == 1).OrderBy(p => p.TenKP) select new { KhoaKham.TenKP, KhoaKham.MaKP, KhoaKham.MaKPsd}).ToList();                       
                if (q.Count > 0)
                {           
                    if(DungChung.Bien.MaBV == "30009")
                    {
                        var b = (from cb in _data.CanBoes
                                    where cb.MaCB == DungChung.Bien.MaCB
                                    select new {cb.MaKPsd }).ToList();
                        string[] str = new string[100];
                        var a = b.First().MaKPsd.Split(';');
                        List<ClassA> l = new List<ClassA>();
                    for(int i = 1; i < a.Count() - 1; i++)
                    {
                        ClassA l1 = new ClassA();
                        var c = (from cd in q
                                    where cd.MaKP.ToString() == a[i]
                                    select new { cd.TenKP, cd.MaKP }).OrderBy(p => p.MaKP).ToList();
                        if(c.Count() > 0)
                        {
                            l1.MaKP = c.First().MaKP;
                            l1.TenKP = c.First().TenKP;
                            l.Add(l1);
                        }                                             
                    }                     
                    lupMaKP.Properties.DataSource = l;
                    lupTimMaKP.Properties.DataSource = l;                          
                    lupMaKPds.DataSource = q.ToList();
                }
                else
                {
                    lupMaKP.Properties.DataSource = q.ToList();
                    lupTimMaKP.Properties.DataSource = q.ToList();
                    lupMaKPds.DataSource = q.ToList();
                }

                }          
            lupTimMaKP.EditValue = DungChung.Bien.MaKP;
            TimKiem();
            int idct = 0;
            if (!string.IsNullOrEmpty(txtIDNhap.Text))
            {
                idct = Convert.ToInt32(txtIDNhap.Text);
            }
            _lNhapDct = _data.NhapDcts.Where(p => p.IDNhap == idct).ToList();
            binNhapDuocct.DataSource = _lNhapDct.ToList();
            grcNhapCT.DataSource = binNhapDuocct;
            if (DungChung.Bien.MaBV == "30281")
            {
                this.colSoLuong.DisplayFormat.FormatString = DungChung.Bien.FormatString[0];
                this.colThanhTien.DisplayFormat.FormatString = DungChung.Bien.FormatString[1];
            }
        }

        private void grvNhapCT_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            txtIDNhap.Text = "1";
        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            Enablebutton(false);
            EnableControl(true);
            ResetControl();
            txtSoCT.Focus();
            lupMaKP.EditValue = DungChung.Bien.MaKP;
            _lNhapDct = _data.NhapDcts.Where(p => p.IDNhap == 0).ToList();
            binNhapDuocct.DataSource = _lNhapDct;
            grcNhapCT.DataSource = binNhapDuocct;
            TTLuu = 1;
        }

        private void grvNhapCT_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            int madv = 0;
            int makho = 0;
            
            switch (e.Column.Name)
            {
                case "colMaDV":
                    if (grvNhapCT.GetFocusedRowCellValue(colMaDV) != null)
                        madv = Convert.ToInt32( grvNhapCT.GetFocusedRowCellValue(colMaDV));
                    if (lupMaKP.EditValue != null)
                        makho = Convert.ToInt32( lupMaKP.EditValue);
                    grvNhapCT.SetFocusedRowCellValue(colDonGia, 0);
                    cboDonGia.Items.Clear();
                    var gia2 = (from nhapduoc in _data.NhapDcts.Where(p => p.MaDV == madv)
                                    join nduoc in _data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.MaKP == makho) on nhapduoc.IDNhap equals nduoc.IDNhap
                                    group new { nhapduoc } by new { nhapduoc.DonGia, nhapduoc.SoLo, nhapduoc.HanDung } into kq
                                    select new { kq.Key.DonGia, kq.Key.SoLo, kq.Key.HanDung }).ToList();
                
                    if (gia2.Count > 0) 
                    {
                        var gia = gia2.Select(p => p.DonGia).Distinct().ToList();
                        if (gia.Count > 0)
                        {
                            foreach (var g in gia)
                            {
                                cboDonGia.Items.Add(g);
                            }
                        }
                        _lDonGia24012.Clear();
                        for (int i = 0; i < gia2.Count; i++)
                        {
                            DonGia24012 a = new DonGia24012();
                            a.DonGia = gia2[i].DonGia;
                            a.HanDung = gia2[i].HanDung;
                            a.SoLo = gia2[i].SoLo;
                            _lDonGia24012.Add(a);
                        }
                    }
            
                    // kết thúc
                    grvNhapCT.SetFocusedRowCellValue(colDonVi, DungChung.Ham._getDonVi(_data, madv));
                    grvNhapCT.SetFocusedRowCellValue(colDonGia, DungChung.Ham._getGia(_data, madv,makho).Gia);
                    grvNhapCT.SetFocusedRowCellValue(colMaCC, DungChung.Bien._maCC);
                    grvNhapCT.ViewCaption = "Số lượng tồn: " + DungChung.Bien.SoLuongTon.ToString();
                    grvNhapCT.SetFocusedRowCellValue(colSoLuong, 0);
                    break;
                case "colDonGia":
                    if (grvNhapCT.GetFocusedRowCellValue(colSoLuong) != null && grvNhapCT.GetFocusedRowCellValue(colSoLuong).ToString() != "")
                    {
                        double a = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colSoLuong));
                        if (grvNhapCT.GetFocusedRowCellValue(colDonGia) != null && grvNhapCT.GetFocusedRowCellValue(colDonGia).ToString() != "")
                        {
                            double b = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGia).ToString());
                            grvNhapCT.SetFocusedRowCellValue(colThanhTien, a * b);
                            var macc = (from nd in _data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.MaKP == makho)
                                        join ndct in _data.NhapDcts.Where(p => p.MaDV == madv).Where(p => p.DonGia == b) on nd.IDNhap equals ndct.IDNhap
                                        select ndct.MaCC).ToList();
                            if (macc.Count > 0)
                            {
                                grvNhapCT.SetFocusedRowCellValue(colMaCC, macc.First());
                            }
                        }
                    }
                    if (grvNhapCT.GetFocusedRowCellValue(colDonGia) != null && grvNhapCT.GetFocusedRowCellValue(colDonGia).ToString() != "")
                    {
                        double dongia = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGia));
                        var listSoLo = _lDonGia24012.Where(p => p.DonGia == dongia).Select(p => p.SoLo).Distinct().ToList();
                        cboSoLo.Items.Clear();
                        grvNhapCT.SetFocusedRowCellValue(colHanDung, "");
                        grvNhapCT.SetFocusedRowCellValue(colSoLo, "");
                        foreach (var item in listSoLo)
                        {
                            cboSoLo.Items.Add(item);
                        }
                    }

                        break;

                case "colSoLo":
                    if (DungChung.Bien.MaBV == "24012" && grvNhapCT.GetFocusedRowCellValue(colSoLo) != null && grvNhapCT.GetFocusedRowCellValue(colSoLo).ToString() != "")
                    {
                        string solo = grvNhapCT.GetFocusedRowCellValue(colSoLo).ToString();
                        double dongia = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGia));
                        if (!string.IsNullOrEmpty(solo))
                        {
                            var date = _lDonGia24012.Where(p => p.DonGia == dongia).Where(p => p.SoLo == solo).Where(p => !string.IsNullOrEmpty(p.HanDung.ToString())).Select(p => p.HanDung).ToList();
                            if (date.Count > 0)
                            {
                                grvNhapCT.SetFocusedRowCellValue(colHanDung, Convert.ToDateTime(date.First()));
                            }
                        }
                    }
                    break;

                case "colSoLuong":
                    if (grvNhapCT.GetFocusedRowCellValue(colSoLuong) != null && grvNhapCT.GetFocusedRowCellValue(colSoLuong).ToString() != "")
                    {
                        double a = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colSoLuong).ToString());
                        if (a >= 0)
                        {
                            switch (TTLuu)
                            {
                                case 1: // khi tao don moi
                                        if (grvNhapCT.GetFocusedRowCellValue(colDonGia) != null && grvNhapCT.GetFocusedRowCellValue(colDonGia).ToString() != "")
                                        {
                                            double b =Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGia));
                                            grvNhapCT.SetFocusedRowCellValue(colThanhTien, a * b);

                                        }
                                    break;
                            }
                            if (grvNhapCT.GetFocusedRowCellValue(colDonGia) != null && grvNhapCT.GetFocusedRowCellValue(colDonGia).ToString() != "")
                            {
                                double b = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGia));
                                grvNhapCT.SetFocusedRowCellValue(colThanhTien, a * b);

                            }
                        }
                        else
                        {
                            MessageBox.Show("Số lượng phải > 0!");
                            grvNhapCT.SetFocusedRowCellValue(colSoLuong, 0);

                        }
                        e.Column.DisplayFormat.FormatString = DungChung.Bien.FormatString[0];
                    }
                    break;
                case "colThanhTien":
                    {
                        e.Column.DisplayFormat.FormatString = DungChung.Bien.FormatString[1];
                    }
                    break;
            }
        }

        private void grvNhapCT_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == STT)
            {
                e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Enablebutton(false);
            EnableControl(true);
            txtSoCT.Focus();
            TTLuu = 2;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIDNhap.Text))
            {
                int id = Int32.Parse(txtIDNhap.Text);
                var kt = _data.NhapDs.Where(p => p.IDNhap == id).ToList();
                if (kt.Count > 0 && kt.First().Status != 1)
                {
                    DialogResult _result;
                    _result = MessageBox.Show("Bạn muốn xóa chứng từ số: " + txtSoCT.Text, "xóa chứng từ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_result == DialogResult.Yes)
                    {
                        
                        var xoact = _data.NhapDcts.Where(p => p.IDNhap == id).ToList();
                        foreach (var xoa in xoact)
                        {
                            var _xoa = _data.NhapDcts.Single(p => p.IDNhapct== (xoa.IDNhapct));
                            _data.NhapDcts.Remove(_xoa);
                            _data.SaveChanges();
                        }
                        var xoac = _data.NhapDs.Single(p => p.IDNhap== (id));
                        _data.NhapDs.Remove(xoac);
                        _data.SaveChanges();
                        TimKiem();
                    }
                }
                else
                {
                    MessageBox.Show("Chứng từ đã bị khóa, bạn không thể xóa!");
                }
            }
            else
            {
                MessageBox.Show("Không có chứng từ để xóa!");
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

            QLBV_Database.QLBVEntities DataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            // luu bang NhapD
            if (KT())
            {
                switch (TTLuu)
                {
                    case 1:
                        NhapD nhap = new NhapD();
                        nhap.PLoai = 4;
                        nhap.NgayNhap = dtNgayNhap.DateTime;
                        nhap.MaKP = lupMaKP.EditValue == null ? 0 : Convert.ToInt32(lupMaKP.EditValue);
                        //nhap.MaCC = lupMaCC.EditValue.ToString();
                        //nhap.TenNguoiCC = cboNguoiGiao.Text;
                        if (!string.IsNullOrEmpty(txtGhiChu.Text))
                            nhap.GhiChu = txtGhiChu.Text;
                        if (!string.IsNullOrEmpty(txtSoCT.Text))
                            nhap.SoCT = txtSoCT.Text;
                        else
                            nhap.SoCT = "";
                        DataContext.NhapDs.Add(nhap);
                        string thuockluu = "các thuốc không được lưu:\n";
                        int _ttthuockluu = 0;
                        if (DataContext.SaveChanges() >= 0)
                        {

                            //Luu bang NhapDct
                            // lấy ID max trong bang NhapD
                            int idnhap = 0;
                            var que = (from IDMax in DataContext.NhapDs orderby IDMax.IDNhap descending select IDMax.IDNhap).ToList();
                            if (que.Count > 0)
                            {
                                idnhap = que.First();

                                for (int i = 0; i < grvNhapCT.DataRowCount; i++)
                                {
                                    if (grvNhapCT.GetRowCellValue(i, colMaDV) != null )
                                    {
                                            if (grvNhapCT.GetRowCellValue(i, colSoLuong) != null && grvNhapCT.GetRowCellValue(i, colSoLuong).ToString() != "")
                                            {
                                                NhapDct nhapdct = new NhapDct();
                                                nhapdct.IDNhap = idnhap;
                                                nhapdct.MaDV = Convert.ToInt32( grvNhapCT.GetRowCellValue(i, colMaDV));
                                                nhapdct.DonVi = grvNhapCT.GetRowCellValue(i, colDonVi).ToString().Trim();
                                                nhapdct.DonGia = double.Parse(grvNhapCT.GetRowCellValue(i, colDonGia).ToString());
                                                nhapdct.SoLuongKK = double.Parse(grvNhapCT.GetRowCellValue(i, colSoLuong).ToString());
                                                nhapdct.ThanhTienKK = double.Parse(grvNhapCT.GetRowCellValue(i, colThanhTien).ToString());
                                                nhapdct.SoLuongN = 0;
                                                nhapdct.ThanhTienN = 0;
                                                nhapdct.SoLuongSD = 0;
                                                nhapdct.ThanhTienSD = 0;
                                                nhapdct.SoLuongX = 0;
                                                nhapdct.ThanhTienX = 0;
                                                if (grvNhapCT.GetRowCellValue(i, colSoLo) != null && grvNhapCT.GetRowCellValue(i, colSoLo).ToString() != "")
                                                    nhapdct.SoLo = grvNhapCT.GetRowCellValue(i, colSoLo).ToString();
                                                if (grvNhapCT.GetRowCellValue(i, colMaCC) != null && grvNhapCT.GetRowCellValue(i, colMaCC).ToString() != "")
                                                    nhapdct.MaCC = grvNhapCT.GetRowCellValue(i, colMaCC).ToString();
                                                if (grvNhapCT.GetRowCellValue(i, colSoDangKy) != null && grvNhapCT.GetRowCellValue(i, colSoDangKy).ToString() != "")
                                                    nhapdct.SoDangKy = grvNhapCT.GetRowCellValue(i, colSoDangKy).ToString();
                                                if (grvNhapCT.GetRowCellValue(i, colHanDung) != null && grvNhapCT.GetRowCellValue(i, colHanDung).ToString() != "")
                                                    nhapdct.HanDung = Convert.ToDateTime(grvNhapCT.GetRowCellValue(i, colHanDung));
                                                _data.NhapDcts.Add(nhapdct);
                                                _data.SaveChanges();
                                            }
                                            else
                                            {
                                                thuockluu += grvNhapCT.GetRowCellValue(i, colMaDV).ToString() + ", ";
                                                _ttthuockluu = 1;
                                            }
                                    }

                                }
                            }

                        }
                        if (_ttthuockluu == 1)
                            MessageBox.Show(thuockluu);
                        Enablebutton(true);
                        EnableControl(false);
                        usXuatDuoc_Load(sender, e);
                        break;
                    case 2:
                        if (!string.IsNullOrEmpty(txtIDNhap.Text))
                        {
                            int id = Convert.ToInt32(txtIDNhap.Text);
                            NhapD nhaps = _data.NhapDs.Single(p => p.IDNhap == id);
                            nhaps.NgayNhap = dtNgayNhap.DateTime;
                            nhaps.MaKP = lupMaKP.EditValue == null ? 0 : Convert.ToInt32(lupMaKP.EditValue);
                            //nhaps.MaCC = lupMaCC.EditValue.ToString();
                            //nhaps.TenNguoiCC = cboNguoiGiao.Text;
                            if (!string.IsNullOrEmpty(txtGhiChu.Text))
                                nhaps.GhiChu = txtGhiChu.Text;
                            if (!string.IsNullOrEmpty(txtSoCT.Text))
                                nhaps.SoCT = txtSoCT.Text;
                            string thuockluus = "các thuốc không được lưu:\n";
                            int _ttthuockluus = 0;
                            DataContext.SaveChanges();
                            //Luu bang NhapDct
                            // lấy ID max trong bang NhapD

                            for (int i = 0; i < grvNhapCT.DataRowCount; i++)
                            {
                                if (grvNhapCT.GetRowCellValue(i, colMaDV) != null )
                                {
                                    if (grvNhapCT.GetRowCellValue(i, colDonGia) != null && grvNhapCT.GetRowCellValue(i, colDonGia).ToString() != "")
                                    {
                                        if (grvNhapCT.GetRowCellValue(i, colSoLuong) != null && grvNhapCT.GetRowCellValue(i, colSoLuong).ToString() != "")
                                        {
                                            int idct = 0;
                                            if (grvNhapCT.GetRowCellValue(i, colIDNhapct) != null && grvNhapCT.GetRowCellValue(i, colIDNhapct).ToString() != "")
                                            {
                                                idct = Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colIDNhapct).ToString());
                                                if (idct <= 0) // them row moi
                                                {
                                                    NhapDct nhapdct = new NhapDct();
                                                    nhapdct.IDNhap = id;
                                                    nhapdct.MaDV = Convert.ToInt32( grvNhapCT.GetRowCellValue(i, colMaDV));
                                                    nhapdct.DonVi = grvNhapCT.GetRowCellValue(i, colDonVi).ToString().Trim();
                                                    nhapdct.DonGia = double.Parse(grvNhapCT.GetRowCellValue(i, colDonGia).ToString());
                                                    nhapdct.SoLuongKK = double.Parse(grvNhapCT.GetRowCellValue(i, colSoLuong).ToString());
                                                    nhapdct.ThanhTienKK = double.Parse(grvNhapCT.GetRowCellValue(i, colThanhTien).ToString());
                                                    nhapdct.SoLuongN = 0;
                                                    nhapdct.ThanhTienN = 0;
                                                    nhapdct.SoLuongSD = 0;
                                                    nhapdct.ThanhTienSD = 0;
                                                    nhapdct.SoLuongX = 0;
                                                    nhapdct.ThanhTienX = 0;
                                                    if (grvNhapCT.GetRowCellValue(i, colMaCC) != null && grvNhapCT.GetRowCellValue(i, colMaCC).ToString() != "")
                                                        nhapdct.MaCC = grvNhapCT.GetRowCellValue(i, colMaCC).ToString();
                                                    if (grvNhapCT.GetRowCellValue(i, colSoLo) != null && grvNhapCT.GetRowCellValue(i, colSoLo).ToString() != "")
                                                        nhapdct.SoLo = grvNhapCT.GetRowCellValue(i, colSoLo).ToString();
                                                    if (grvNhapCT.GetRowCellValue(i, colSoDangKy) != null && grvNhapCT.GetRowCellValue(i, colSoDangKy).ToString() != "")
                                                        nhapdct.SoDangKy = grvNhapCT.GetRowCellValue(i, colSoDangKy).ToString();
                                                    if (grvNhapCT.GetRowCellValue(i, colHanDung) != null && grvNhapCT.GetRowCellValue(i, colHanDung).ToString() != "")
                                                        nhapdct.HanDung = Convert.ToDateTime(grvNhapCT.GetRowCellValue(i, colHanDung));
                                                    _data.NhapDcts.Add(nhapdct);
                                                    _data.SaveChanges();
                                                }
                                                else
                                                {
                                                    NhapDct nhapdcts = _data.NhapDcts.Single(p => p.IDNhapct == idct);
                                                    nhapdcts.MaDV = Convert.ToInt32( grvNhapCT.GetRowCellValue(i, colMaDV));
                                                    nhapdcts.DonVi = grvNhapCT.GetRowCellValue(i, colDonVi).ToString().Trim();
                                                    nhapdcts.DonGia = double.Parse(grvNhapCT.GetRowCellValue(i, colDonGia).ToString());
                                                    nhapdcts.SoLuongKK = double.Parse(grvNhapCT.GetRowCellValue(i, colSoLuong).ToString());
                                                    nhapdcts.ThanhTienKK = double.Parse(grvNhapCT.GetRowCellValue(i, colThanhTien).ToString());
                                                    if (grvNhapCT.GetRowCellValue(i, colMaCC) != null && grvNhapCT.GetRowCellValue(i, colMaCC).ToString() != "")
                                                        nhapdcts.MaCC = grvNhapCT.GetRowCellValue(i, colMaCC).ToString();
                                                    if (grvNhapCT.GetRowCellValue(i, colSoLo) != null && grvNhapCT.GetRowCellValue(i, colSoLo).ToString() != "")
                                                        nhapdcts.SoLo = grvNhapCT.GetRowCellValue(i, colSoLo).ToString();
                                                    if (grvNhapCT.GetRowCellValue(i, colSoDangKy) != null && grvNhapCT.GetRowCellValue(i, colSoDangKy).ToString() != "")
                                                        nhapdcts.SoDangKy = grvNhapCT.GetRowCellValue(i, colSoDangKy).ToString();
                                                    if (grvNhapCT.GetRowCellValue(i, colHanDung) != null && grvNhapCT.GetRowCellValue(i, colHanDung).ToString() != "")
                                                        nhapdcts.HanDung = Convert.ToDateTime(grvNhapCT.GetRowCellValue(i, colHanDung));
                                                    _data.SaveChanges();
                                                }
                                            }
                                        }//
                                        else
                                        {
                                            thuockluus += grvNhapCT.GetRowCellValue(i, colMaDV).ToString() + ", ";
                                            _ttthuockluus = 1;
                                        }
                                    }
                                    else
                                    {
                                        thuockluus += grvNhapCT.GetRowCellValue(i, colMaDV).ToString() + ", ";
                                        _ttthuockluus = 1;
                                    }
                                }

                            }

                            if (_ttthuockluus == 1)
                                MessageBox.Show(thuockluus);
                            Enablebutton(true);
                            EnableControl(false);
                            usXuatDuoc_Load(sender, e);
                        }

                        break;
                }
            }
        }

        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void dtTimTuNgay_Leave(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void dtTimDenNgay_Leave(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void lupTimMaKP_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void lupTimMaKPnx_EditValueChanged(object sender, EventArgs e)
        {
            //TimKiem();
        }
        private void grvNhap_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int id = 0;
            if (grvNhap.GetFocusedRowCellValue(colIDNhaps) != null && grvNhap.GetFocusedRowCellValue(colIDNhaps).ToString() != "")
            {
                    txtIDNhap.Text = grvNhap.GetFocusedRowCellValue(colIDNhaps).ToString();
                    id = Convert.ToInt32(grvNhap.GetFocusedRowCellValue(colIDNhaps));
                    txtSoCT.Text = _lNhapDuoc.Where(p => p.IDNhap == id).First().SoCT;
                    lupMaKP.EditValue= _lNhapDuoc.Where(p => p.IDNhap == id).First().MaKP;
                    dtNgayNhap.DateTime = _lNhapDuoc.Where(p => p.IDNhap == id).First().NgayNhap.Value;
                    txtGhiChu.Text = _lNhapDuoc.Where(p => p.IDNhap == id).First().GhiChu;
                    _lNhapDct = _data.NhapDcts.Where(p => p.IDNhap == id).ToList();
                    binNhapDuocct.DataSource = _lNhapDct;
                    grcNhapCT.DataSource = binNhapDuocct;
            }
            else
            {
                txtIDNhap.Text = "";
                lupMaKP.EditValue = 0;
                txtGhiChu.Text = "";
                txtSoCT.Text = "";
                _lNhapDct = _data.NhapDcts.Where(p => p.IDNhap == id).ToList();
                binNhapDuocct.DataSource = _lNhapDct;
                grcNhapCT.DataSource = binNhapDuocct;
            }
        }

        private void grvNhap_DataSourceChanged(object sender, EventArgs e)
        {
            int id = 0;
            if (grvNhap.GetFocusedRowCellValue(colIDNhaps) != null && grvNhap.GetFocusedRowCellValue(colIDNhaps).ToString() != "")
            {
                txtIDNhap.Text = grvNhap.GetFocusedRowCellValue(colIDNhaps).ToString();
                id = Convert.ToInt32(grvNhap.GetFocusedRowCellValue(colIDNhaps));
                txtSoCT.Text = _lNhapDuoc.Where(p => p.IDNhap == id).First().SoCT;
                lupMaKP.EditValue = _lNhapDuoc.Where(p => p.IDNhap == id).First().MaKP;
                dtNgayNhap.DateTime = _lNhapDuoc.Where(p => p.IDNhap == id).First().NgayNhap.Value;
                txtGhiChu.Text = _lNhapDuoc.Where(p => p.IDNhap == id).First().GhiChu;
                _lNhapDct = _data.NhapDcts.Where(p => p.IDNhap == id).ToList();
                binNhapDuocct.DataSource = _lNhapDct;
                grcNhapCT.DataSource = binNhapDuocct;
            }
            else
            {
                txtIDNhap.Text = "";
                lupMaKP.EditValue = 0;
                txtGhiChu.Text = "";
                txtSoCT.Text = "";
                _lNhapDct = _data.NhapDcts.Where(p => p.IDNhap == id).ToList();
                binNhapDuocct.DataSource = _lNhapDct;
                grcNhapCT.DataSource = binNhapDuocct;
            }
        }

        private void grvNhapCT_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colXoaCT")
            {
                if (!string.IsNullOrEmpty(txtIDNhap.Text))
                {
                    int id = Int32.Parse(txtIDNhap.Text);
                    var kt = _data.NhapDs.Where(p => p.IDNhap == id).ToList();
                    if (kt.Count > 0 && kt.First().Status != 1)
                    {
                        if (grvNhapCT.GetFocusedRowCellValue(colIDNhapct) != null && grvNhapCT.GetFocusedRowCellValue(colIDNhapct).ToString() != "")
                        {
                            if (grvNhapCT.GetFocusedRowCellDisplayText(colMaDV) != null && grvNhapCT.GetFocusedRowCellDisplayText(colMaDV).ToString() != "")
                            {
                                string tenthuoc = grvNhapCT.GetFocusedRowCellDisplayText(colMaDV).ToString();
                                if (MessageBox.Show("Bạn muốn xóa thuốc: " + tenthuoc, "Xóa chi tiết", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    int idct = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colIDNhapct));
                                    var _xoact = _data.NhapDcts.Single(p => p.IDNhapct== (idct));
                                    _data.NhapDcts.Remove(_xoact);
                                    _data.SaveChanges();
                                    _lNhapDct = _data.NhapDcts.Where(p => p.IDNhap == id).ToList();
                                    binNhapDuocct.DataSource = _lNhapDct;
                                    grcNhapCT.DataSource = binNhapDuocct;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Chứng từ đã bị khóa, bạn không thể xóa!");
                    }
                }
                else
                {
                    MessageBox.Show("Không có chứng từ để xóa!");
                }
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            int id = 0;
            if (!string.IsNullOrEmpty(txtIDNhap.Text))
                id = Convert.ToInt32(txtIDNhap.Text);
           // FormThamSo.frmTsBbKKThuoc frm = new FormThamSo.frmTsBbKKThuoc(id);
            FormThamSo.frmTsBbKKThuoc frm = new FormThamSo.frmTsBbKKThuoc();
            frm.ShowDialog();

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int id = 0;
            if (!string.IsNullOrEmpty(txtIDNhap.Text))
                id = Convert.ToInt32(txtIDNhap.Text);
         //   FormThamSo.frmTsBbKKThuoc frm = new FormThamSo.frmTsBbKKThuoc(id);
            FormThamSo.frmTsBbKKThuoc frm = new FormThamSo.frmTsBbKKThuoc();
            frm.ShowDialog();
        }

        private void grvNhapCT_FocusedRowChanged_1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int madv = 0;
            int makho = 0;
            if (grvNhapCT.GetFocusedRowCellValue(colMaDV) != null)
            {
                madv = Convert.ToInt32( grvNhapCT.GetFocusedRowCellValue(colMaDV));
                if (lupMaKP.EditValue != null )
                    makho = Convert.ToInt32( lupMaKP.EditValue);
                //grvNhapCT.SetRowCellValue(e.FocusedRowHandle, colDonGia, "");
                //cboDonGia.Items.Clear();
                int cgia = 0;
                cgia = cboDonGia.Items.Count;
                //for (int i = 0; i < cgia; i++) {
                //    cboDonGia.Items.RemoveAt(i);
                //}
                cboDonGia.Items.Clear();
                var gia = (from nhapduoc in _data.NhapDcts.Where(p => p.MaDV== madv)
                           join nduoc in _data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.MaKP== makho) on nhapduoc.IDNhap equals nduoc.IDNhap
                           group new { nhapduoc } by new { nhapduoc.DonGia } into kq
                           select new { kq.Key.DonGia }).ToList();
                if (gia.Count > 0)
                {
                    foreach (var g in gia)
                    {
                        cboDonGia.Items.Add(g.DonGia);
                    }
                }
            }
        }

        private void lupMaKP_EditValueChanged(object sender, EventArgs e)
        {
            int makho = 0;
            if (lupMaKP.EditValue != null)
                makho = Convert.ToInt32( lupMaKP.EditValue);
            var duoc = (from tenduoc in _data.DichVus.Where(p => p.PLoai == 1)
                        join nhapduoc in _data.NhapDcts on tenduoc.MaDV equals nhapduoc.MaDV
                        join nduoc in _data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.MaKP== makho) on nhapduoc.IDNhap equals nduoc.IDNhap
                        group new { tenduoc, nhapduoc, nduoc } by new { nduoc.MaKP, tenduoc.TenDV, tenduoc.MaDV, tenduoc.DonVi } into kq
                        select new { kq.Key.TenDV, kq.Key.MaDV, kq.Key.DonVi, kq.Key.MaKP }
                            ).OrderBy(p => p.TenDV).ToList();
            lupMaDuoc.DataSource = duoc.ToList();
        }

        private void btnKKTD_Click(object sender, EventArgs e)
        {
            ChucNang.frm_KKTuDong frm = new ChucNang.frm_KKTuDong(1);
            frm.FormClosed += new FormClosedEventHandler(this.usXuatDuoc_Load);
            frm.ShowDialog();
        }
        public class DonGia24012
        {
            public int ID { get; set; }
            public double? DonGia { get; set; }
            public string SoLo { get; set; }
            public DateTime? HanDung { get; set; }
        }
    }
}
