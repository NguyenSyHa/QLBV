using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using GridView = DevExpress.XtraGrid.Views.Grid.GridView;
using GridColumn = DevExpress.XtraGrid.Columns.GridColumn;
namespace QLBV.FormNhap
{
    public partial class us_KhamSucKhoe : DevExpress.XtraEditors.XtraUserControl
    {
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public us_KhamSucKhoe()
        {
            InitializeComponent();
            var cb = (from c in data.CanBoes
                      join kp in data.KPhongs on c.MaKP equals kp.MaKP
                      where kp.PLoai == "Kế toán"
                      select c).ToList();
            lupCanBo.Properties.DataSource = cb;
        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }

        private void textEdit3_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnref_Click(object sender, EventArgs e)
        {
            DSBNhan();
        }

        public void DSBNhan()
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (dtTimTuNgay.EditValue != null && dtTimDenNgay.EditValue != null)
            {
                string tenBN = txtTimKiem.Text;
                DateTime ngaytu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
                DateTime ngayden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
                List<BenhNhan> _lBN = new List<BenhNhan>();//data.BenhNhans.ToList();
                _lBN = data.BenhNhans.Where(p => p.DTuong == "KSK").Where(p => p.NNhap != null && p.NNhap.Value >= ngaytu && p.NNhap.Value <= ngayden).OrderBy(p => p.NNhap).ThenBy(p => p.TenBNhan).ToList();
                //if (chkKSK.Checked)
                //{
                //    _lBN = _lBN.Where(p => p.DTuong == "KSK").ToList();
                //}
                if (_lBN.Count() > 0)
                {
                    _lBN = _lBN.Where(p => p.TenBNhan.Contains(tenBN) || p.MaBNhan.ToString().Contains(tenBN)).ToList();
                    if (c.Checked)
                    {
                        _lBN = _lBN.Where(p => p.ChuyenKhoa != null && p.ChuyenKhoa != "").ToList();
                        enablesuakl(true);
                    }
                    else
                    {
                        _lBN = _lBN.Where(p => p.ChuyenKhoa == null || p.ChuyenKhoa == "").ToList();
                        enablesuakl(false);
                    }
                    DateTime ngaytunew = ngaytu.AddMonths(-1);
                    DateTime ngaydennew = ngayden.AddMonths(1);
                    var _lTamung = data.TamUngs.Where(p => p.NgayThu >= ngaytunew && p.NgayThu <= ngaydennew).Where(p => p.PhanLoai == 1).ToList();
                    if (cboTimRaVien.SelectedIndex == 0)//chưa thanh toán
                    {
                        //_lBN=_lBN.Where(p=>p.)
                        _lBN = (from l in _lBN
                                where !(from vp in _lTamung select vp.MaBNhan).Contains(l.MaBNhan)
                                //join vp in _lTamung on l.MaBNhan equals vp.MaBNhan//data.TamUngs on l.MaBNhan equals vp.MaBNhan
                                //into kq
                                //from kq2 in kq.DefaultIfEmpty()
                                //where kq2 == null
                                select l).ToList();
                    }
                    if (cboTimRaVien.SelectedIndex == 1)//Đã thanh toán
                    {
                        _lBN = (from l in _lBN
                                join vp in _lTamung on l.MaBNhan equals vp.MaBNhan//in _lTamung on l.MaBNhan equals vp.MaBNhan//
                                select l).ToList();
                    }
                }
                //if (cboTimRaVien.SelectedIndex == 2)//Tất cả thì không làm gì
                //{}
                grcBNhan.DataSource = _lBN;
                loadSourceCBO();
            }
        }

        private void us_KhamSucKhoe_Load(object sender, EventArgs e)
        {
            dtTimTuNgay.EditValue = System.DateTime.Now;
            dtTimDenNgay.EditValue = System.DateTime.Now;
            cboTimRaVien.SelectedIndex = 0;
            enableControl(false);
            loadSourceCBO();
        }

        private void loadSourceCBO()
        {
            cbeKetLuan.Properties.Items.Clear();
            cbePhanLoai.Properties.Items.Clear();
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var ploaiKSK = (from d in data.BenhNhans
                            where d.DTuong == "KSK" && d.CDNoiGT != null
                            orderby d.CDNoiGT
                            select d.CDNoiGT).Distinct().ToList();
            foreach (string item in ploaiKSK)
            {
                cbePhanLoai.Properties.Items.Add(item);
            }

            var lKetqua = (from d in data.BenhNhans
                           where d.DTuong == "KSK" && d.ChuyenKhoa != null
                           orderby d.ChuyenKhoa
                           select d.ChuyenKhoa).Distinct().ToList();
            foreach (string item in lKetqua)
            {
                cbeKetLuan.Properties.Items.Add(item);
            }
            
        }

        private void chkCLS_EditValueChanged(object sender, EventArgs e)
        {
            DSBNhan();
        }

        private void chkKSK_EditValueChanged(object sender, EventArgs e)
        {
            DSBNhan();
        }

        private void dtTimTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            DSBNhan();
        }

        private void dtTimDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            DSBNhan();
        }

        private void cboTimRaVien_EditValueChanged(object sender, EventArgs e)
        {
            DSBNhan();
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
                QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                List<QuyenSoBL> _lQuyen = new List<QuyenSoBL>();
                _lQuyen = (from a in _data.SoBienLais.Where(p => p.Status == 1 && p.PLoai == pl) select new QuyenSoBL { Quyen = a.Quyen, So = a.SoHT }).ToList();

                return _lQuyen;
            }
        }


        #endregion
        private void grvBNhan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvBNhan.GetFocusedRowCellValue(colMaBNhan) != null)
            {
                try
                {
                    int indexrow = 0;
                    indexrow = grvBNhan.GetSelectedRows()[0];
                    int maBN = Convert.ToInt32(getValueGrid(grvBNhan, colMaBNhan, indexrow));
                    dateEditNNhap.EditValue = Convert.ToDateTime(getValueGrid(grvBNhan, colNNhapkb, indexrow));
                    txtMaBN.Text = maBN.ToString();
                    txtTenBN.Text = getValueGrid(grvBNhan, colTenBNhan, indexrow);
                    txtDTuong.Text = getValueGrid(grvBNhan, colDTuong, indexrow) == "KSK" ? "Khám sức khỏe" : getValueGrid(grvBNhan, colDTuong, indexrow); ;
                    txtHThuc.Text = getValueGrid(grvBNhan, colTChung, indexrow);
                    if (getValueGrid(grvBNhan, colChuyenKhoa, indexrow) != "")
                    {
                        cbeKetLuan.SelectedItem = getValueGrid(grvBNhan, colChuyenKhoa, indexrow);

                    }
                    else
                    {

                        cbeKetLuan.Text = "";
                    }
                    if (getValueGrid(grvBNhan, colCDnoiGT, indexrow) != "")
                    {
                        cbePhanLoai.SelectedItem = getValueGrid(grvBNhan, colCDnoiGT, indexrow);
                    }
                    else cbePhanLoai.Text = "";
                    chkTTCLS.Checked = Convert.ToInt32(getValueGrid(grvBNhan, colCapCuu, indexrow)) == 0 ? false : true;
                    double sotien = 0;
                    int rs;
                    int _int_maBN = 0;
                    if (Int32.TryParse(txtMaBN.Text, out rs))
                        _int_maBN = Convert.ToInt32(txtMaBN.Text);
                    var qt = data.TamUngs.Where(p => p.MaBNhan == _int_maBN).ToList();

                    if (qt.Count > 0)
                    {
                        enableControl(true);
                        lupDichvuKSK.EditValue = null;
                        lupDichvuKSK.Enabled = false;
                        txtSo.Enabled = false;
                        cboSoTo.Enabled = false;
                        txtLyDo.Enabled = false;
                        dateNgayThu.Enabled = false;
                        dateNgayThu.EditValue = qt.First().NgayThu;
                        if (qt.First().QuyenHD != null)
                            cbo_Quyen.Text = qt.First().QuyenHD.ToString();
                        if (qt.First().SoHD != null)
                            cboSoHD.Text = qt.First().SoHD.ToString();

                        txtSo.Text = qt.First().IDTamUng.ToString();
                        cboSoTo.EditValue = qt.First().SoTo;
                        txtLyDo.Text = qt.First().LyDo;
                        if (qt.First().SoTien != null)
                            sotien = qt.First().SoTien.Value;
                        txtSotienTThu.Text = sotien.ToString();
                        txtSoTien.Text = "Số tiền bằng chữ: " + QLBV_Library.QLBV_Ham.DocTienBangChu(sotien, " đồng");
                    }
                    else
                    {
                        enableControl(false);
                        lupDichvuKSK.Enabled = true;
                        txtSo.Enabled = true;
                        cboSoTo.Enabled = true;
                        txtLyDo.Enabled = true;
                        dateNgayThu.Enabled = true;
                        dateNgayThu.EditValue = System.DateTime.Now;
                        var q = (from dv in data.DichVus
                                 join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                 join ndv in data.NhomDVs.Where(p => p.TenNhom.Contains("KSK")) on tn.IDNhom equals ndv.IDNhom
                                 select dv).ToList();
                        if (chkTTCLS.Checked)
                        {
                            lupDichvuKSK.Properties.DataSource = q.Where(p => p.DVKTC == 1).ToList();
                        }
                        else
                        {
                            lupDichvuKSK.Properties.DataSource = q.Where(p => p.DVKTC == 0).ToList();
                        }
                        txtSoTien.Text = "Số tiền bằng chữ: ";
                        txtSotienTThu.Text = "0";
                        cbo_Quyen.Text = "";
                        cboSoHD.Text = "";
                        txtLyDo.Text = "";
                        //var a = QuyenSoBL._getQuyen_SoBL(0, "");
                        //cbo_Quyen.Text = a.FirstOrDefault().Quyen;
                        //cboSoHD.Text = a.FirstOrDefault().So != null ? (a.FirstOrDefault().So + 1).ToString() : "";
                    }


                    // set txtCMT và dateeditCMT
                    var q1 = data.TTboXungs.Where(p => p.MaBNhan == maBN).ToList();
                    txtCMT.Text = q1.First().CMT;
                    dateEditCMT.EditValue = q1.First().NgayCapCMT;
                    mmGhiChu.Text = q1.First().NoiLV;
                    var q2 = data.BenhNhans.Where(p => p.MaBNhan == maBN).ToList();
                    cbePhanLoai.Text = q2.First().CDNoiGT;
                    cbeKetLuan.Text = q2.First().ChuyenKhoa;

                }
                catch (Exception)
                {
                }

            }
        }

        private string getValueGrid(GridView grv, GridColumn column, int index)
        {
            if (grv.GetRowCellValue(index, column) != null)
            {
                return grv.GetRowCellValue(index, column).ToString();
            }
            return "";
        }

        private void grvBNhan_DataSourceChanged(object sender, EventArgs e)
        {
            grvBNhan_FocusedRowChanged(sender, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(0, 0));
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBN.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBN.Text);

            if (lupCanBo.EditValue != null)
            {
                if (data.BenhNhans.Where(p => p.MaBNhan == _int_maBN).ToList().Count == 0)
                {
                    MessageBox.Show("Bạn chưa chọn Bệnh nhân để lưu");
                }
                else
                {
                    string ploai = cbePhanLoai.Text;
                    string ketluan = cbeKetLuan.Text;
                    var sua = data.BenhNhans.Where(p => p.MaBNhan == _int_maBN).First();
                    sua.ChuyenKhoa = ketluan;
                    sua.CDNoiGT = ploai;
                    var sua2 = data.TTboXungs.Where(p => p.MaBNhan == _int_maBN).First();
                    sua2.NoiLV = mmGhiChu.Text;
                    var sua3 = data.TamUngs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PhanLoai == 1).ToList();
                    if (sua3.Count > 0)
                    {
                        foreach (var item in sua3)
                        {
                            item.PhanLoai = 1;
                            item.NgayThu = dateNgayThu.DateTime;
                            item.LyDo = txtLyDo.Text;
                            item.SoTo = Convert.ToInt32(cboSoTo.SelectedIndex + 1);
                            if (txtSotienTThu.Text.Length > 0)
                            {
                                item.SoTien = Convert.ToDouble(txtSotienTThu.Text);
                            }
                            item.QuyenHD = cbo_Quyen.Text;
                            item.SoHD = cboSoHD.Text;
                        }
                    }
                    else
                    {
                        var a = usTamThu_TToan.QuyenSoBL._getQuyen_SoBL(1, "");
                        if (string.IsNullOrEmpty(cbo_Quyen.Text))
                            cbo_Quyen.Text = a.FirstOrDefault().Quyen == null ? "" : a.FirstOrDefault().Quyen;
                        if (string.IsNullOrEmpty(cboSoHD.Text))
                            cboSoHD.Text = a.FirstOrDefault().So == null ? "" : (a.FirstOrDefault().So + 1).ToString();

                        TamUng moi = new TamUng();
                        moi.PhanLoai = 1;
                        moi.NgayThu = dateNgayThu.DateTime;
                        moi.LyDo = txtLyDo.Text;
                        moi.SoTo = Convert.ToInt32(cboSoTo.SelectedIndex + 1);
                        if (txtSotienTThu.Text.Length > 0)
                        {
                            moi.SoTien = Convert.ToDouble(txtSotienTThu.Text);
                        }
                        if (lupCanBo.EditValue != null)
                        {
                            moi.MaCB = lupCanBo.EditValue.ToString();
                        }
                        moi.MaBNhan = _int_maBN;
                        moi.QuyenHD = cbo_Quyen.Text;
                        moi.SoHD = cboSoHD.Text;
                        data.TamUngs.Add(moi);
                    }
                    try
                    {
                        var soBL = data.SoBienLais.Where(p => p.PLoai == 1 && p.Quyen == cbo_Quyen.Text).ToList();
                        foreach (var item in soBL)
                        {
                            item.SoHT = item.SoHT + 1;

                        }

                    }
                    catch { }
                    data.SaveChanges();

                    MessageBox.Show("Lưu thành công!");

                    loadThu();
                    int _idtthu = 0;
                    if (!string.IsNullOrEmpty(txtSo.Text))
                        _idtthu = int.Parse(txtSo.Text);
                    usTamThu_TToan.InPhieuThuChi(_idtthu, _int_maBN);
                }
                refresh();
            }
            else
                MessageBox.Show("Bạn chưa chọn Cán Bộ");
        }
        private void loadThu()
        {

            int ot;
            int _int_maBN = 0;
            string _macb = "";
            if (Int32.TryParse(txtMaBN.Text, out ot))
                _int_maBN = Convert.ToInt32(txtMaBN.Text);
            var q = data.TamUngs.Where(p => p.MaBNhan == _int_maBN).ToList();
            _macb = q.First().MaCB;
            var cb = data.CanBoes.Where(p => p.MaCB == _macb).ToList();
            if(cb.Count()>0)
            {
                lupCanBo.EditValue = cb.First().TenCB;
            }
            dateNgayThu.EditValue = q.First().NgayThu;
            txtSo.Text = q.First().IDTamUng.ToString();
            cboSoTo.EditValue = q.First().SoTo;
            txtLyDo.Text = q.First().LyDo;
            double sotien = 0;
            if (q.First().SoTien != null)
                sotien = q.First().SoTien.Value;
            txtSotienTThu.Text = sotien.ToString();
            txtSoTien.Text = "Số tiền bằng chữ: " + QLBV_Library.QLBV_Ham.DocTienBangChu(sotien, " đồng");
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            enableControl(false);
            // cbePhanLoai.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBN.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBN.Text);
            if (data.BenhNhans.Where(p => p.MaBNhan == _int_maBN).ToList().Count == 0)
            {
                MessageBox.Show("Bạn chưa chọn Bệnh nhân để xóa!");
            }
            else
            {
                var xoa = data.TamUngs.Where(p => p.MaBNhan == _int_maBN).ToList();
                foreach(var item in xoa)
                {
                    data.TamUngs.Remove(item);
                }
                if(xoa.Count()>0)
                {
                    data.SaveChanges();
                }
                MessageBox.Show("Xóa thành công","Thông báo", MessageBoxButtons.OK);
                refresh();

            }
        }
        public void enableControl(bool T)
        {
            btnLuu.Enabled = !T;
            btnSua.Enabled = T;
            btnXoa.Enabled = T;
            cbeKetLuan.Properties.ReadOnly = T;
            cbePhanLoai.Properties.ReadOnly = T;
            mmGhiChu.Properties.ReadOnly = T;
            dateNgayThu.Enabled = !T;
            cboSoTo.Enabled = !T;
            lupDichvuKSK.Enabled = !T;
            txtSotienTThu.Enabled = !T;
            txtSoTien.Enabled = !T;
            txtLyDo.Enabled = !T;
            lupCanBo.Enabled = !T;
        }
        public void enablesuakl(bool T)
        {
            cbeKetLuan.Properties.ReadOnly = T;
            cbePhanLoai.Properties.ReadOnly = T;
            mmGhiChu.Properties.ReadOnly = T;
            btnsuakl.Enabled = T;
            btnxoakl.Enabled = T;
            btnluukl.Enabled = !T;
        }
        private void chkKetQua_EditValueChanged(object sender, EventArgs e)
        {
            DSBNhan();
            //refresh();
            
        }
        public void refresh()
        {
            txtMaBN.Text = "";
            txtTenBN.Text = "";
            txtHThuc.Text = "";
            txtDTuong.Text = "";
            chkTTCLS.Checked = false;
            cbeKetLuan.Text = "";
            cbePhanLoai.Text = "";
            mmGhiChu.Text = "";
            txtSotienTThu.Text = "";
            dateEditNNhap.EditValue = null;
            dateEditCMT.EditValue = null;
            txtSoTien.Text = "";

            txtCMT.Text = "";

            dateNgayThu.EditValue = null;
            txtSo.Text = null;
            cboSoTo.SelectedIndex = 0;
            lupDichvuKSK.EditValue = null;
            txtSoTien.Text = "";
            txtSotienTThu.Text = "";
            txtLyDo.Text = "";
            lupCanBo.EditValue = null;
        }

        private void labelControl12_Click(object sender, EventArgs e)
        {

        }

        private void groupTTBN_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lupDichvuKSK_EditValueChanged(object sender, EventArgs e)
        {

            if (lupDichvuKSK.EditValue != null)
            {
                int madv = Convert.ToInt32(lupDichvuKSK.EditValue);
                double sotien = 0;
                if (data.DichVus.Where(p => p.MaDV == madv).First().DonGia != null)
                {
                    sotien = data.DichVus.Where(p => p.MaDV == madv).First().DonGia;
                }
                txtSoTien.Text = "Số tiền bằng chữ: " + QLBV_Library.QLBV_Ham.DocTienBangChu(sotien, " đồng");
                txtSotienTThu.Text = sotien.ToString();
            }

        }

        private void txtSotienTThu_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtSotienTThu_Leave(object sender, EventArgs e)
        {
            txtSoTien.Text = "Số tiền bằng chữ: " + QLBV_Library.QLBV_Ham.DocTienBangChu(Convert.ToDouble(txtSotienTThu.Text), " đồng");
        }

        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            DSBNhan();
        }

        private void groupPloaiKLuan_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int _idtthu = 0;
            if (!string.IsNullOrEmpty(txtSo.Text))
                _idtthu = int.Parse(txtSo.Text);
            int ot;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBN.Text, out ot))
                _int_maBN = Convert.ToInt32(txtMaBN.Text);
            usTamThu_TToan.InPhieuThuChi(_idtthu, _int_maBN);
        }

        private void groupDSBN_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            enablesuakl(false);
        }

        private void btnxoakl_Click(object sender, EventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBN.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBN.Text);
            if (data.BenhNhans.Where(p => p.MaBNhan == _int_maBN).ToList().Count == 0)
            {
                MessageBox.Show("Bạn chưa chọn Bệnh nhân để xóa!");
            }
            else
            {
                var xoa = data.BenhNhans.Where(p => p.MaBNhan == _int_maBN).First();
                DialogResult _result = MessageBox.Show("Bạn có muốn xóa kết quả này không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_result == DialogResult.Yes)
                {
                    var sua = data.BenhNhans.Where(p => p.MaBNhan == _int_maBN).First();
                    sua.ChuyenKhoa = "";
                    sua.CDNoiGT = "";
                    var sua2 = data.TTboXungs.Where(p => p.MaBNhan == _int_maBN).First();
                    sua2.NoiLV = "";
                    data.SaveChanges();
                    MessageBox.Show("Xóa kết quả thành công!");
                    //DSBNhan();
                    cbeKetLuan.Text = "";
                    cbePhanLoai.Text = "";
                    mmGhiChu.Text = "";
                }
            }
            
        }

        private void btnluukl_Click(object sender, EventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBN.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBN.Text);

            if (data.BenhNhans.Where(p => p.MaBNhan == _int_maBN).ToList().Count == 0)
            {
                MessageBox.Show("Bạn chưa chọn Bệnh nhân để lưu");
            }
            else
            {
                string ploai = "",ketluan = "";
                if(cbePhanLoai.Text!="")
                {
                    ploai = cbePhanLoai.Text;
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập phân loại");
                    cbePhanLoai.Focus();
                }
                if(cbeKetLuan.Text!="")
                {
                    ketluan = cbeKetLuan.Text;
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập kết quả");
                    cbeKetLuan.Focus();
                }
                if (ploai != "" && ketluan != "")
                {
                    var sua = data.BenhNhans.Where(p => p.MaBNhan == _int_maBN).First();
                    sua.ChuyenKhoa = ketluan;
                    sua.CDNoiGT = ploai;
                    var sua2 = data.TTboXungs.Where(p => p.MaBNhan == _int_maBN).First();
                    sua2.NoiLV = mmGhiChu.Text;
                    data.SaveChanges();

                    MessageBox.Show("Lưu thành công!");
                    enablesuakl(true);
                    //DSBNhan();
                }
            }
            //refresh();
        }

        private void labelControl7_Click(object sender, EventArgs e)
        {

        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnMoi2_Click(object sender, EventArgs e)
        {

            frmHSBNNhapMoi frm = new frmHSBNNhapMoi(0);
            frm.FormClosed += new FormClosedEventHandler(this.us_KhamSucKhoe_Load);
            frm.ShowDialog();
        }

        private void lupCanBo_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Test click");
        }

        private void lupCanBo_MouseClick(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("Test  mouseclick");
        }

        private void lupCanBo_KeyUp(object sender, KeyEventArgs e)
        {
            //MessageBox.Show("Test");
        }

        private void cboTimRaVien_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
