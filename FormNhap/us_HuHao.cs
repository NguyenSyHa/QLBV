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
    public partial class us_HuHao : DevExpress.XtraEditors.XtraUserControl
    {
        public us_HuHao()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<NhapD> _lNhapDuoc = new List<NhapD>();
        List<NhapDct> _lNhapDct = new List<NhapDct>();
        List<NhapDct> _lSoLo = new List<NhapDct>();
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
                MessageBox.Show("Bạn chưa chọn Ngày nhập hư hao!");
                dtNgayNhap.Focus();
                return false;
            }
            if (lupMaKP.EditValue == null || lupMaKP.EditValue.ToString() == "")
            {
                MessageBox.Show("Bạn chưa chon kho có thuốc hư hao!");
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
        int TTLuu = 0;
        #region hàm tìm kiếm
        private void TimKiem()
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _dttu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
            _dtden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
            if (lupTimMaKP.EditValue != null && lupTimMaKP.EditValue.ToString() != "")
                _makho = Convert.ToInt32(lupTimMaKP.EditValue);
            if (!string.IsNullOrEmpty(txtTimKiem.Text) && txtTimKiem.Text != "Số phiếu|Số CT")
                _soPhieu = txtTimKiem.Text;
            _lNhapDuoc = (from nd in _data.NhapDs.Where(p => p.PLoai == 3)
                          where (nd.NgayNhap >= _dttu && nd.NgayNhap <= _dtden)
                          where (nd.SoCT.Contains(_soPhieu))
                          where (nd.MaKP == _makho)
                          select nd).OrderByDescending(p => p.NgayNhap).OrderByDescending(p => p.IDNhap).ToList();
            grcNhap.DataSource = _lNhapDuoc.ToList();
        }
        #endregion
        public class ClassB
        {
            public string TenKP { get; set; }
            public int MaKP { get; set; }
        }
        private void usXuatDuoc_Load(object sender, EventArgs e)
        {
            //// tao treelist
            //TreeListNode codeNode = tre_DSXuat.AppendNode(null,null);
            //codeNode.SetValue("name","Ngay");
            //TreeListNode ngayNode = tre_DSXuat.AppendNode(null, null);
            //ngayNode.SetValue("name", "Ngay2");
            //TreeListNode nhaCCNode = tre_DSXuat.AppendNode(null, null);
            //ngayNode.SetValue("name", "NhaCC");
            //TreeListNode childNode = null;
            //childNode = tre_DSXuat.AppendNode(null, codeNode);
            //childNode.SetValue("name", " ten 1");
            //// ket treelist
            Enablebutton(true);
            EnableControl(false);
            dtTimDenNgay.DateTime = System.DateTime.Now;
            dtTimTuNgay.DateTime = System.DateTime.Now;
            dtNgayNhap.EditValue = System.DateTime.Now;
            //if (DungChung.Bien.CapDo == 9 || DungChung.Bien.CapDo == 8)
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin || (DungChung.Bien.MaBV == "30009"))
            {
                lupTimMaKP.Enabled = true;
                lupMaKP.Enabled = true;
            }
            else
            {
                lupTimMaKP.Enabled = false;
                lupMaKP.Enabled = false;
            }
            var q = (from KhoaKham in _data.KPhongs.Where(p => p.PLoai == ("Khoa dược")).Where(p => p.Status == 1).OrderBy(p => p.TenKP) select new { KhoaKham.TenKP, KhoaKham.MaKP }).ToList();
            if (q.Count > 0)
            {
                if (DungChung.Bien.MaBV == "30009")
                {
                    var b = (from cb in _data.CanBoes
                             where cb.MaCB == DungChung.Bien.MaCB
                             select new { cb.MaKPsd }).ToList();
                    string[] str = new string[100];
                    var a = b.First().MaKPsd.Split(';');
                    List<ClassB> l = new List<ClassB>();
                    for (int i = 1; i < a.Count() - 1; i++)
                    {
                        ClassB l1 = new ClassB();
                        var c = (from cd in q
                                 where cd.MaKP.ToString() == a[i]
                                 select new { cd.TenKP, cd.MaKP }).OrderBy(p => p.MaKP).ToList();
                        if (c.Count() > 0)
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

            //var sql = (from dv in _data.DichVus.Where(p => p.PLoai == 1) join nduocct in _data.NhapDcts on dv.MaDV equals nduocct.MaDV
            //           join nduoc in _data.NhapDs.Where(p=>p.PLoai==1) on nduocct.IDNhap equals nduoc.IDNhap
            //           group new {dv,nduocct} by new {dv.MaDV,dv.TenDV,dv.DonVi,nduocct.SoLo,nduocct.DonGia} into kq
            //           select new { kq.Key.MaDV, kq.Key.TenDV, kq.Key.DonVi,kq.Key.SoLo,kq.Key.DonGia }).OrderBy(p => p.TenDV).ToList();// join voi nhap duoc
            //if (sql.Count > 0)
            //{
            //    lupMaDuoc.DataSource = sql.ToList();
            //}
            lupTimMaKP.EditValue = DungChung.Bien.MaKP;
            TimKiem();
            int idct = 0;
            if (!string.IsNullOrEmpty(txtIDNhap.Text))
            {
                idct = Convert.ToInt32(txtIDNhap.Text);
            }
            //var bn = (from ds in _data.BenhNhans select new { ds.TenBNhan, ds.MaBNhan }).ToList();
            _lNhapDct = _data.NhapDcts.Where(p => p.IDNhap == idct).ToList();
            binNhapDuocct.DataSource = _lNhapDct.ToList();
            grcNhapCT.DataSource = binNhapDuocct;

        }

        private void grvNhapCT_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            txtIDNhap.Text = "1";
        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            bool _sua = true;
            _sua = DungChung.Ham.checkQuyen(this.Name)[0];
            if (_sua)
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
        }
        string _solo = "";// số lo của thuốc được focus
        DateTime? _handung = null;// hạn dùng của thuốc được focus
        private void grvNhapCT_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            int madv = 0;
            int makho = 0;
            DungChung.Ham.giaSoLoHSD dsgia = new DungChung.Ham.giaSoLoHSD();
            switch (e.Column.Name)
            {
                case "colMaDV":
                    if (grvNhapCT.GetFocusedRowCellValue(colMaDV) != null)
                        madv = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colMaDV));
                    if (!string.IsNullOrEmpty(lupMaKP.Text))
                        makho = Convert.ToInt32(lupMaKP.EditValue);
                    grvNhapCT.SetFocusedRowCellValue(colDonGia, "0");
                    //for (int i = 0; i < cboDonGia.Items.Count; i++)
                    //{
                    //    cboDonGia.Items.RemoveAt(i);
                    //}
                    cboDonGia.Items.Clear();
                    var gia = (from nhapduoc in _data.NhapDcts.Where(p => p.MaDV == madv)
                               join nduoc in _data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.MaKP == makho) on nhapduoc.IDNhap equals nduoc.IDNhap
                               group new { nhapduoc } by new { nhapduoc.DonGia, nhapduoc.DonVi } into kq
                               select new { kq.Key.DonGia, kq.Key.DonVi }).ToList();
                    if (gia.Count > 0)
                    {
                        foreach (var g in gia)
                        {
                            cboDonGia.Items.Add(g.DonGia);
                        }
                        string donvi = "";
                        donvi = gia.First().DonVi;
                        grvNhapCT.SetFocusedRowCellValue(colDonVi, donvi);
                    }

                    // kết thúc

                    dsgia = DungChung.Ham._getGia(_data, madv, makho);
                    if (grvNhapCT.GetFocusedRowCellValue(colSoLo) != null)
                        _solo = grvNhapCT.GetFocusedRowCellValue(colSoLo).ToString();
                    grvNhapCT.SetFocusedRowCellValue(colHanDung, dsgia.HanDung);
                    grvNhapCT.SetFocusedRowCellValue(colSoLo, dsgia.SoLo);
                    grvNhapCT.SetFocusedRowCellValue(colDonGia, dsgia.Gia);
                    groupControl3.Text = "Số lượng tồn: " + DungChung.Bien.SoLuongTon.ToString();
                    //if (grvNhapCT.GetFocusedRowCellValue(colHanDung) != null)
                    //                    _handung = Convert.ToDateTime(grvNhapCT.GetFocusedRowCellValue(colHanDung).ToString());  
                    //    grvNhapCT.SetFocusedRowCellValue(colDonVi, DungChung.Ham._getDonVi(_data, madv));
                    //    grvNhapCT.SetFocusedRowCellValue(colDonGia, DungChung.Ham._getGia(_data, madv,makho).Gia);
                    //    grvNhapCT.ViewCaption = "Số lượng tồn: " + DungChung.Bien.SoLuongTon.ToString();
                    //    grvNhapCT.SetFocusedRowCellValue(colSoLuong, 0);
                    break;
                case "colDonGia":

                    if (grvNhapCT.GetFocusedRowCellValue(colSoLuong) != null && grvNhapCT.GetFocusedRowCellValue(colSoLuong).ToString() != "")
                    {
                        double a = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colSoLuong));
                        if (grvNhapCT.GetFocusedRowCellValue(colDonGia) != null && grvNhapCT.GetFocusedRowCellValue(colDonGia).ToString() != "")
                        {
                            double b = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGia));
                            grvNhapCT.SetFocusedRowCellValue(colThanhTien, a * b);
                        }
                    }

                    break;
                case "colSoLuong":
                    if (grvNhapCT.GetFocusedRowCellValue(colSoLuong) != null && grvNhapCT.GetFocusedRowCellValue(colSoLuong).ToString() != "")
                    {
                        double a = double.Parse(grvNhapCT.GetFocusedRowCellValue(colSoLuong).ToString());
                        if (a >= 0)
                        {
                            switch (TTLuu)
                            {
                                case 1: // khi tao don moi
                                    if (a <= DungChung.Bien.SoLuongTon)
                                    {
                                        if (grvNhapCT.GetFocusedRowCellValue(colDonGia) != null && grvNhapCT.GetFocusedRowCellValue(colDonGia).ToString() != "")
                                        {
                                            double b = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGia));
                                            grvNhapCT.SetFocusedRowCellValue(colThanhTien, a * b);

                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Số lượng trong kho không đủ");
                                        grvNhapCT.SetFocusedRowCellValue(colSoLuong, "0");
                                        //DungChung.Bien.SoLuongTon = 0;
                                        grvNhapCT.ViewCaption = "Số lượng tồn: " + DungChung.Bien.SoLuongTon.ToString();

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
            bool _sua = true;
            _sua = DungChung.Ham.checkQuyen(this.Name)[1];
            if (_sua)
            {
                Enablebutton(false);
                EnableControl(true);
                txtSoCT.Focus();
                TTLuu = 2;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            bool _sua = true;
            _sua = DungChung.Ham.checkQuyen(this.Name)[2];
            if (_sua)
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
                                var _xoa = _data.NhapDcts.Single(p => p.IDNhapct == (xoa.IDNhapct));
                                _data.NhapDcts.Remove(_xoa);

                            }
                            var xoac = _data.NhapDs.Single(p => p.IDNhap == (id));
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
                        nhap.PLoai = 3;
                        nhap.NgayNhap = dtNgayNhap.DateTime;
                        nhap.MaKP = lupMaKP.EditValue == null ? 0 : Convert.ToInt32(lupMaKP.EditValue);

                        //nhap.MaCC = lupMaCC.EditValue.ToString();
                        //nhap.TenNguoiCC = cboNguoiGiao.Text;
                        if (!string.IsNullOrEmpty(txtGhiChu.Text))
                            nhap.GhiChu = txtGhiChu.Text;
                        //if (!string.IsNullOrEmpty(txtSoCT.Text))
                        nhap.SoCT = txtSoCT.Text;
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
                                    if (grvNhapCT.GetRowCellValue(i, colMaDV) != null)
                                    {
                                        if (grvNhapCT.GetRowCellValue(i, colSoLuong) != null && grvNhapCT.GetRowCellValue(i, colSoLuong).ToString() != "")
                                        {
                                            NhapDct nhapdct = new NhapDct();
                                            nhapdct.IDNhap = idnhap;
                                            nhapdct.MaDV = Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colMaDV));
                                            nhapdct.DonVi = grvNhapCT.GetRowCellValue(i, colDonVi).ToString().Trim();
                                            nhapdct.DonGia = double.Parse(grvNhapCT.GetRowCellValue(i, colDonGia).ToString());
                                            nhapdct.SoLuongX = double.Parse(grvNhapCT.GetRowCellValue(i, colSoLuong).ToString());
                                            nhapdct.ThanhTienX = double.Parse(grvNhapCT.GetRowCellValue(i, colThanhTien).ToString());
                                            nhapdct.SoLuongN = 0;
                                            nhapdct.ThanhTienN = 0;
                                            nhapdct.SoLuongSD = 0;
                                            nhapdct.ThanhTienSD = 0;
                                            nhapdct.SoLuongKK = 0;
                                            nhapdct.ThanhTienKK = 0;
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
                            //if (!string.IsNullOrEmpty(txtSoCT.Text))
                            nhaps.SoCT = txtSoCT.Text;
                            string thuockluus = "các thuốc không được lưu:\n";
                            int _ttthuockluus = 0;
                            DataContext.SaveChanges();
                            //Luu bang NhapDct
                            // lấy ID max trong bang NhapD

                            for (int i = 0; i < grvNhapCT.DataRowCount; i++)
                            {
                                if (grvNhapCT.GetRowCellValue(i, colMaDV) != null)
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
                                                    nhapdct.MaDV = Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colMaDV));
                                                    nhapdct.DonVi = grvNhapCT.GetRowCellValue(i, colDonVi).ToString().Trim();
                                                    nhapdct.DonGia = double.Parse(grvNhapCT.GetRowCellValue(i, colDonGia).ToString());
                                                    nhapdct.SoLuongX = double.Parse(grvNhapCT.GetRowCellValue(i, colSoLuong).ToString());
                                                    nhapdct.ThanhTienX = double.Parse(grvNhapCT.GetRowCellValue(i, colThanhTien).ToString());
                                                    nhapdct.SoLuongN = 0;
                                                    nhapdct.ThanhTienN = 0;
                                                    nhapdct.SoLuongSD = 0;
                                                    nhapdct.ThanhTienSD = 0;
                                                    nhapdct.SoLuongKK = 0;
                                                    nhapdct.ThanhTienKK = 0;
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
                                                    nhapdcts.MaDV = Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colMaDV));
                                                    nhapdcts.DonVi = grvNhapCT.GetRowCellValue(i, colDonVi).ToString().Trim();
                                                    nhapdcts.DonGia = double.Parse(grvNhapCT.GetRowCellValue(i, colDonGia).ToString());
                                                    nhapdcts.SoLuongX = double.Parse(grvNhapCT.GetRowCellValue(i, colSoLuong).ToString());
                                                    nhapdcts.ThanhTienX = double.Parse(grvNhapCT.GetRowCellValue(i, colThanhTien).ToString());
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
                lupMaKP.EditValue = _lNhapDuoc.Where(p => p.IDNhap == id).First().MaKP;
                txtGhiChu.Text = _lNhapDuoc.Where(p => p.IDNhap == id).First().GhiChu;
                if (_lNhapDuoc.Where(p => p.IDNhap == id).First().NgayNhap != null)
                    dtNgayNhap.DateTime = _lNhapDuoc.Where(p => p.IDNhap == id).First().NgayNhap.Value;
                _lNhapDct = _data.NhapDcts.Where(p => p.IDNhap == id).ToList();
                binNhapDuocct.DataSource = _lNhapDct;
                grcNhapCT.DataSource = binNhapDuocct;
            }
            else
            {
                dtNgayNhap.DateTime = DateTime.Now;
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
            switch (e.Column.Name)
            {
                case "colXoaCT":
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
                                        var _xoact = _data.NhapDcts.Single(p => p.IDNhapct == (idct));
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
                    break;
                case "colSoLo":
                    int madv = 0;
                    if (grvNhapCT.GetFocusedRowCellValue(colMaDV) != null)
                        madv = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colMaDV));
                    var _SoLo = (from solo in _data.NhapDcts.Where(p => p.SoLuongN > 0)
                                 where (solo.MaDV == madv)
                                 group solo by new { solo.SoLo, solo.DonGia } into kq
                                 select new { kq.Key.SoLo, kq.Key.DonGia }).ToList();
                    lupSoLosx.DataSource = _SoLo.ToList();
                    break;
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<RepBC> bc = new List<RepBC>();
            frmIn frm = new frmIn();
            BaoCao.repPhieuXuat rep = new BaoCao.repPhieuXuat();
            rep.tieude.Text = "THUỐC HƯ HAO";
            if (DungChung.Bien.MaBV == "30002")
                rep.xrTableCell30.Text = "Trưởng khoa dược";
            string solo = "";
            int id = 0;
            if (!string.IsNullOrEmpty(txtIDNhap.Text))
                id = int.Parse(txtIDNhap.Text);
            var par = (from nd in data.NhapDs
                       join kp in data.KPhongs on nd.MaKP equals kp.MaKP
                       where (nd.IDNhap == id)
                       select new { kp.TenKP, nd.NgayNhap, nd.TenNguoiCC, nd.GhiChu }).ToList();
            if (par.Count > 0)
            {
                rep.Soct.Value = id;
                rep.Ngaythang.Value = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                rep.Nguoinhanhang.Value = par.First().TenNguoiCC;
                rep.Noidung.Value = par.First().GhiChu;
                rep.Khoxuat.Value = par.First().TenKP;
            }

            //var q = from xd in data.XuatDs join xdct in data.XuatDcts on xd.IDXuat equals xdct.IDXuat where(xd.IDXuat == id) join dv in data.DichVus on            



            var q = (from nd in data.NhapDs
                     join ndx in data.NhapDcts on nd.IDNhap equals ndx.IDNhap
                     where (nd.IDNhap == id)
                     join dv in data.DichVus on ndx.MaDV equals dv.MaDV
                     group new { dv, nd, ndx } by new { dv.TenDV, ndx.DonVi, ndx.DonGia, ndx.MaDV, ndx.HanDung, ndx.SoLo } into kq
                     select new
                        {
                            MaDV = kq.Key.MaDV,
                            TenDV = kq.Key.TenDV,
                            HanDung = kq.Key.HanDung,
                            SoLo = kq.Key.SoLo,
                            DonVi = kq.Key.DonVi,
                            DonGia = kq.Key.DonGia,
                            SoLuongX = kq.Sum(p => p.ndx.SoLuongX),
                            ThanhTienX = kq.Sum(p => p.ndx.ThanhTienX)
                        }).ToList();
            foreach (var item in q)
            {
                RepBC r = new RepBC();
                r.MaDV = item.MaDV;
                r.TenDV = item.TenDV;
                r.HanDung = item.HanDung == null ? "" : Convert.ToDateTime(item.HanDung).ToString("dd/MM/yyyy");
                r.SoLo = item.SoLo;
                r.DonVi = item.DonVi;
                r.DonGia = item.DonGia;
                r.SoLuongX = item.SoLuongX;
                r.ThanhTienX = item.ThanhTienX;
                bc.Add(r);
            }
            if (bc.Count > 0)
            {
                rep.DataSource = bc;
                rep.TongTien.Value = bc.Sum(p => p.ThanhTienX);
                rep.BindingData();
                //rep.DataMember = "";
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {

                MessageBox.Show("Ko co du lieu");
            }


        }
        private class RepBC
        {
            public int? MaDV { get; set; }
            public string TenDV { get; set; }
            public string HanDung { get; set; }
            public string SoLo { get; set; }
            public string DonVi { get; set; }
            public double DonGia { get; set; }
            public double SoLuongX { get; set; }
            public double ThanhTienX { get; set; }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            usXuatDuoc_Load(sender, e);
        }

        private void grvNhapCT_AfterPrintRow(object sender, DevExpress.XtraGrid.Views.Printing.PrintRowEventArgs e)
        {

        }

        private void btnBBXN_Click(object sender, EventArgs e)
        {
            int id = 0;
            if (!string.IsNullOrEmpty(txtIDNhap.Text))
                id = Convert.ToInt32(txtIDNhap.Text);
            FormThamSo.frmTsBbXacNhan frm = new FormThamSo.frmTsBbXacNhan(id);
            frm.ShowDialog();
        }

        private void lupMaKP_EditValueChanged(object sender, EventArgs e)
        {
            int makho = 0;
            if (lupMaKP.EditValue != null)
                makho = Convert.ToInt32(lupMaKP.EditValue);
            var duoc = (from tenduoc in _data.DichVus.Where(p => p.PLoai == 1)
                        join nhapduoc in _data.NhapDcts on tenduoc.MaDV equals nhapduoc.MaDV
                        join nduoc in _data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.MaKP == makho) on nhapduoc.IDNhap equals nduoc.IDNhap
                        group new { tenduoc, nhapduoc, nduoc } by new { nduoc.MaKP, tenduoc.TenDV, tenduoc.MaDV, tenduoc.DonVi } into kq
                        select new { kq.Key.TenDV, kq.Key.MaDV, kq.Key.DonVi, kq.Key.MaKP }
                            ).OrderBy(p => p.TenDV).ToList();
            lupMaDuoc.DataSource = duoc.ToList();
        }

        private void grvNhapCT_FocusedRowChanged_1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int madv = 0;
            int makho = 0;
            DungChung.Ham.giaSoLoHSD dsgia = new DungChung.Ham.giaSoLoHSD();
            if (grvNhapCT.GetFocusedRowCellValue(colMaDV) != null)
            {
                madv = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colMaDV));
                if (lupMaKP.EditValue != null)
                    makho = Convert.ToInt32(lupMaKP.EditValue);
                //grvNhapCT.SetRowCellValue(e.FocusedRowHandle, colDonGia, "");
                //cboDonGia.Items.Clear();
                int cgia = 0;
                cgia = cboDonGia.Items.Count;
                //for (int i = 0; i < cgia; i++) {
                //    cboDonGia.Items.RemoveAt(i);
                //}
                cboDonGia.Items.Clear();
                var gia = (from nhapduoc in _data.NhapDcts.Where(p => p.MaDV == madv)
                           join nduoc in _data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.MaKP == makho) on nhapduoc.IDNhap equals nduoc.IDNhap
                           group new { nhapduoc } by new { nhapduoc.DonGia } into kq
                           select new { kq.Key.DonGia }).ToList();
                if (gia.Count > 0)
                {
                    foreach (var g in gia)
                    {
                        cboDonGia.Items.Add(g.DonGia);
                    }
                }
                dsgia = DungChung.Ham._getGia(_data, madv, makho);
                if (grvNhapCT.GetFocusedRowCellValue(colSoLo) != null)
                    _solo = grvNhapCT.GetFocusedRowCellValue(colSoLo).ToString();
                groupControl3.Text = "Số lượng tồn: " + DungChung.Bien.SoLuongTon.ToString();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            QLBV.FormNhap.HuHaoDongY frm = new QLBV.FormNhap.HuHaoDongY();
            frm.ShowDialog();
            TimKiem();
        }


    }
}
