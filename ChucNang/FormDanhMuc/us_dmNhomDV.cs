using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormDanhMuc
{
    public partial class us_dmNhomDV : DevExpress.XtraEditors.XtraUserControl
    {
        public us_dmNhomDV()
        {
            InitializeComponent();
        }
        int _TTLuu = 0;
        int _stt = 0;
        int _id = 0;
        private void enableControl(bool T)
        {
            txtSTT.Enabled = T;
            txtTenNhom.Enabled = T;
            lupTenNhomCT.Enabled = T;
            cboStatus.Enabled = T;
            btnLuuKb.Enabled = T;
            btnMoiKb.Enabled = !T;
            btnSuaKb.Enabled = !T;
            btnXoaKb.Enabled = !T;
            grcNhomDV.Enabled = !T;
        }
        private void resetControl()
        {
            txtSTT.Text = "";
            txtTenNhom.Text = "";
            lupTenNhomCT.Text = "";
            cboStatus.Text = "";
        }
        #region
        private bool KTLuu()
        {
            if (string.IsNullOrEmpty(txtSTT.Text))
            {
                MessageBox.Show("Bạn chưa nhập số thứ tự");
                txtSTT.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTenNhom.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên nhóm dịch vụ");
                txtTenNhom.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(lupTenNhomCT.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên nhóm CT dịch vụ");
                lupTenNhomCT.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(cboStatus.Text))
            {
                MessageBox.Show("Bạn chưa chọn trạng thái");
                cboStatus.Focus();
                return false;
            }
            return true;
        }

        #endregion
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<NhomDV> _lNhomDV = new List<NhomDV>();
        private void us_dmNhomDV_Load(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                var _lNhomDV1 = (from ndv in dataContext.NhomDVs
                                 join dv in dataContext.DichVus on ndv.IDNhom equals dv.IDNhom
                                 select new { ndv.IDNhom, ndv.TenNhom, ndv.TenNhomCT, ndv.STT, ndv.Status }).ToList();
                grcNhomDV.DataSource = _lNhomDV1;
            }
            else
            {
                _lNhomDV = dataContext.NhomDVs.OrderBy(p => p.TenNhom).ToList();
                lupTenNhomCT.Properties.Items.AddRange(DungChung.Bien.NhomDV_QD.SetNhomDV().Select(p => p.TenNhomCT).ToList());
                grcNhomDV.DataSource = _lNhomDV;
            }

            enableControl(false);
        }

        private void btnMoiKb_Click(object sender, EventArgs e)
        {
            enableControl(true);
            resetControl();
            _TTLuu = 1;
            cboStatus.SelectedIndex = 0;
            txtTenNhom.Focus();
        }

        private void btnSuaKb_Click(object sender, EventArgs e)
        {
            enableControl(true);
            txtSTT.Enabled = false;
            _TTLuu = 2;
            txtTenNhom.Focus();
        }

        private void btnXoaKb_Click(object sender, EventArgs e)
        {

            _id = int.Parse(txtID.Text);
            var dv = dataContext.DichVus.Where(p => p.IDNhom == _id).ToList();
            var tndv = dataContext.TieuNhomDVs.Where(p => p.IDNhom == _id).ToList();
            if (dv.Count > 0 || tndv.Count > 0)
            {
                MessageBox.Show("Nhóm dịch vụ đã được sử dụng, Bạn không thể xóa.");
            }
            else
            {
                DataRow dr = grvNhomDV.GetFocusedDataRow();
                DialogResult dia = MessageBox.Show("Bạn có chắc chắn muốn xóa nhóm dịch vụ đã chọn?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dia == DialogResult.Yes)
                {
                    //grcDanToc.ControlRemoved();
                    var xoa = dataContext.NhomDVs.Single(p => p.IDNhom == _id);
                    dataContext.NhomDVs.Remove(xoa);
                    dataContext.SaveChanges();
                    btnXoaKb.Enabled = true;

                }
                _lNhomDV = dataContext.NhomDVs.OrderBy(p => p.TenNhom).ToList();
                grcNhomDV.DataSource = _lNhomDV.ToList();
            }
        }

        private void btnLuuKb_Click(object sender, EventArgs e)
        {
            if (KTLuu())
            {
                switch (_TTLuu)
                {
                    case 1:
                        //_stt = txtSTT.Text.Trim();
                        _stt = int.Parse(txtSTT.Text);
                        var tt = dataContext.NhomDVs.Where(p => p.STT == _stt).ToList();
                        if (tt.Count > 0)
                        {
                            MessageBox.Show("Số thứ tự đã có, vui lòng nhập số khác");
                        }
                        else
                        {
                            NhomDV dv = new NhomDV();
                            dv.TenNhom = txtTenNhom.Text;
                            dv.TenNhomCT = lupTenNhomCT.Text;
                            dv.STT = int.Parse(txtSTT.Text);
                            dv.Status = cboStatus.SelectedIndex;
                            dataContext.NhomDVs.Add(dv);
                            dataContext.SaveChanges();
                            enableControl(false);
                            MessageBox.Show("Lưu thành công!");
                        }
                        break;
                    case 2:
                        if (!string.IsNullOrEmpty(txtID.Text))
                        {
                            _id = int.Parse(txtID.Text);
                            NhomDV dvsua = dataContext.NhomDVs.Single(p => p.IDNhom == _id);
                            dvsua.TenNhom = txtTenNhom.Text;
                            dvsua.TenNhomCT = lupTenNhomCT.Text;
                            //dvsua.STT = int.Parse(txtSTT.Text);
                            dvsua.Status = cboStatus.SelectedIndex;
                            dataContext.SaveChanges();
                            MessageBox.Show("Lưu thành công!");
                            enableControl(false);
                        }
                        break;
                }
                _lNhomDV = dataContext.NhomDVs.OrderBy(p => p.TenNhom).ToList();
                grcNhomDV.DataSource = _lNhomDV.ToList();
            }
        }

        private void grvNhomDV_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvNhomDV.GetFocusedRowCellValue(colSTT) != null && grvNhomDV.GetFocusedRowCellValue(colSTT).ToString() != "")
            {
                txtSTT.Text = grvNhomDV.GetFocusedRowCellValue(colSTT).ToString();
                if (grvNhomDV.GetFocusedRowCellValue(colTenNhom) != null && grvNhomDV.GetFocusedRowCellValue(colTenNhom).ToString() != "")
                {
                    txtTenNhom.Text = grvNhomDV.GetFocusedRowCellValue(colTenNhom).ToString();
                }
                else
                {
                    txtTenNhom.Text = "";
                }
                if (grvNhomDV.GetFocusedRowCellValue(colTenNhomCT) != null && grvNhomDV.GetFocusedRowCellValue(colTenNhomCT).ToString() != "")
                {
                    lupTenNhomCT.Text = grvNhomDV.GetFocusedRowCellValue(colTenNhomCT).ToString();
                }
                else
                {
                    lupTenNhomCT.Text = "";
                }
                if (grvNhomDV.GetFocusedRowCellValue(colIDNhom) != null && grvNhomDV.GetFocusedRowCellValue(colIDNhom).ToString() != "")
                {
                    txtID.Text = grvNhomDV.GetFocusedRowCellValue(colIDNhom).ToString();
                    // txtIDNhom.Text = grvNhomDV.GetFocusedRowCellValue(colIDNhom).ToString();
                }
                else
                {
                    txtID.Text = "";
                    //txtIDNhom.Text = "";
                }

                if (grvNhomDV.GetFocusedRowCellValue(colStatus) != null && grvNhomDV.GetFocusedRowCellValue(colStatus).ToString() != "")
                {
                    int a = int.Parse(grvNhomDV.GetFocusedRowCellValue(colStatus).ToString());
                    cboStatus.SelectedIndex = a;
                }
                else
                {
                    cboStatus.SelectedIndex = -1;
                }
            }
            else
            {
                txtSTT.Text = "";
            }
        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            string tk = "";
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                tk = txtTimKiem.Text;
            }
            grcNhomDV.DataSource = _lNhomDV.Where(p => p.TenNhom.Contains(tk));

        }

        private void txtTenNhomCT_SelectedValueChanged(object sender, EventArgs e)
        {

        }
        #region thaythe
        bool _thaythe(int idcu, int idmoi)
        {
            dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<TieuNhomDV> qTieuNhom = dataContext.TieuNhomDVs.Where(p => p.IDNhom == idcu).ToList();
            List<DichVu> qDichVu = dataContext.DichVus.Where(p => p.IDNhom == idcu).ToList();
            foreach (TieuNhomDV n in qTieuNhom)
            {
                n.IDNhom = idmoi;
                dataContext.SaveChanges();
            }
            foreach (DichVu n in qDichVu)
            {
                n.IDNhom = idmoi;
                dataContext.SaveChanges();
            }


            return true;
        }
        #endregion
        #region kiểm tra khớp CV 9324
        bool _check9324()
        {
            dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<NhomDV> _nhom = (from cv in DungChung.Bien.NhomDV_QD.SetNhomDV()
                                  join nhom in dataContext.NhomDVs on cv.TenNhomCT equals nhom.TenNhomCT
                                  where cv.IDNhom != nhom.IDNhom
                                  select nhom).ToList();
            foreach (var item in _nhom)
            {
                int idcu = item.IDNhom;
                int id = DungChung.Bien.NhomDV_QD.SetNhomDV().Where(p => p.TenNhomCT == item.TenNhomCT).First().IDNhom;
                dataContext.NhomDVs.Remove(item);
                dataContext.SaveChanges();
                NhomDV nhom = item;
                nhom.IDNhom = id;
                dataContext.NhomDVs.Add(nhom);
                dataContext.SaveChanges();
                dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                List<TieuNhomDV> qTieuNhom = dataContext.TieuNhomDVs.Where(p => p.IDNhom == idcu).ToList();
                List<DichVu> qDichVu = dataContext.DichVus.Where(p => p.IDNhom == idcu).ToList();
                foreach (TieuNhomDV n in qTieuNhom)
                {
                    n.IDNhom = id;
                    dataContext.SaveChanges();
                }
                foreach (DichVu n in qDichVu)
                {
                    n.IDNhom = id;
                    dataContext.SaveChanges();
                }
            }
            return true;
        }
        #endregion
        #region thay đổi theo CV 9324
        bool ThayDoi()
        {
            List<NhomDV> _listNhom = dataContext.NhomDVs.ToList();
            #region add IDList Nhóm Dv vào Cột BHYT trong listNhomDv_QD (_listQD_New), xóa nhóm thuốc cũ khi nhóm thuốc có nhóm mới tương ứng
            foreach (NhomDV nhom in _listNhom)
            {
                List<QLBV.DungChung.Bien.NhomDV_QD> _listQD = DungChung.Bien.NhomDV_QD.SetNhomDV();
                _listQD = DungChung.Bien.NhomDV_QD.SetNhomDV();
                string _tenct = "";
                if (nhom.TenNhomCT == "Thuốc trong danh mục BHYT")
                {
                    int idcu = nhom.IDNhom;
                    int idNhom9324 = _listQD.Where(p => p.TenNhomCT == "Thuốc trong danh mục BHYT").Select(p => p.IDNhom).First();
                    if (_listNhom.Where(p => p.IDNhom == idNhom9324).ToList().Count > 0)
                    {
                        _thaythe(idNhom9324, 0);
                    }
                    _thaythe(idcu, idNhom9324);
                }
                else if (nhom.TenNhomCT == "Máu và chế phẩm của máu")
                {

                    int idcu = nhom.IDNhom;
                    int idNhom9324 = _listQD.Where(p => p.TenNhomCT == "Máu và chế phẩm của máu").Select(p => p.IDNhom).First();
                    if (_listNhom.Where(p => p.IDNhom == idNhom9324).ToList().Count > 0)
                    {
                        _thaythe(idNhom9324, 0);
                    }
                    _thaythe(idcu, idNhom9324);

                }
                else if (nhom.TenNhomCT == "Xét nghiệm")
                {
                    int idcu = nhom.IDNhom;
                    int idNhom9324 = _listQD.Where(p => p.TenNhomCT == "Xét nghiệm").Select(p => p.IDNhom).First();
                    if (_listNhom.Where(p => p.IDNhom == idNhom9324).ToList().Count > 0)
                    {
                        _thaythe(idNhom9324, 0);
                    }
                    _thaythe(idcu, idNhom9324);
                }
                else if (nhom.TenNhomCT == "Chẩn đoán hình ảnh")
                {
                    int idcu = nhom.IDNhom;
                    int idNhom9324 = _listQD.Where(p => p.TenNhomCT == "Chẩn đoán hình ảnh").Select(p => p.IDNhom).First();
                    if (_listNhom.Where(p => p.IDNhom == idNhom9324).ToList().Count > 0)
                    {
                        _thaythe(idNhom9324, 0);
                    }
                    _thaythe(idcu, idNhom9324);
                }
                else if (nhom.TenNhomCT == "Khám bệnh")
                {

                    int idcu = nhom.IDNhom;
                    int idNhom9324 = _listQD.Where(p => p.TenNhomCT == "Khám bệnh").Select(p => p.IDNhom).First();
                    if (_listNhom.Where(p => p.IDNhom == idNhom9324).ToList().Count > 0)
                    {
                        _thaythe(idNhom9324, 0);
                    }
                    _thaythe(idcu, idNhom9324);
                }
                else if (nhom.TenNhomCT == "DVKT thanh toán theo tỷ lệ")
                {
                    int idcu = nhom.IDNhom;
                    int idNhom9324 = _listQD.Where(p => p.TenNhomCT == "DVKT thanh toán theo tỷ lệ").Select(p => p.IDNhom).First();
                    if (_listNhom.Where(p => p.IDNhom == idNhom9324).ToList().Count > 0)
                    {
                        _thaythe(idNhom9324, 0);
                    }
                    _thaythe(idcu, idNhom9324);
                }
                else if (nhom.TenNhomCT == "Vật tư y tế trong danh mục BHYT")
                {
                    int idcu = nhom.IDNhom;
                    int idNhom9324 = _listQD.Where(p => p.TenNhomCT == "Vật tư y tế trong danh mục BHYT").Select(p => p.IDNhom).First();
                    if (_listNhom.Where(p => p.IDNhom == idNhom9324).ToList().Count > 0)
                    {
                        _thaythe(idNhom9324, 0);
                    }
                    _thaythe(idcu, idNhom9324);
                }
                else if (nhom.TenNhomCT == "Giường điều trị nội trú")
                {

                    int idcu = nhom.IDNhom;
                    int idNhom9324 = _listQD.Where(p => p.TenNhomCT == "Giường điều trị nội trú").Select(p => p.IDNhom).First();
                    if (_listNhom.Where(p => p.IDNhom == idNhom9324).ToList().Count > 0)
                    {
                        _thaythe(idNhom9324, 0);
                    }
                    _thaythe(idcu, idNhom9324);
                }
                else if (nhom.TenNhomCT == "Vận chuyển")
                {
                    int idcu = nhom.IDNhom;
                    int idNhom9324 = _listQD.Where(p => p.TenNhomCT == "Vận chuyển").Select(p => p.IDNhom).First();
                    if (_listNhom.Where(p => p.IDNhom == idNhom9324).ToList().Count > 0)
                    {
                        _thaythe(idNhom9324, 0);
                    }
                    _thaythe(idcu, idNhom9324);
                }
                else if (nhom.TenNhomCT == "VTYT thanh toán theo tỷ lệ")
                {
                    int idcu = nhom.IDNhom;
                    int idNhom9324 = _listQD.Where(p => p.TenNhomCT == "VTYT thanh toán theo tỷ lệ").Select(p => p.IDNhom).First();
                    if (_listNhom.Where(p => p.IDNhom == idNhom9324).ToList().Count > 0)
                    {
                        _thaythe(idNhom9324, 0);
                    }
                    _thaythe(idcu, idNhom9324);
                }
                else if (nhom.TenNhomCT == "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục")
                {

                    int idcu = nhom.IDNhom;
                    int idNhom9324 = _listQD.Where(p => p.TenNhomCT == "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục").Select(p => p.IDNhom).First();
                    if (_listNhom.Where(p => p.IDNhom == idNhom9324).ToList().Count > 0)
                    {
                        _thaythe(idNhom9324, 0);
                    }
                    _thaythe(idcu, idNhom9324);
                }
                else if (nhom.TenNhomCT == "Thủ thuật, phẫu thuật")
                {
                    int idcu = nhom.IDNhom;
                    int idNhom9324 = _listQD.Where(p => p.TenNhomCT == "Thủ thuật, phẫu thuật").Select(p => p.IDNhom).First();
                    if (_listNhom.Where(p => p.IDNhom == idNhom9324).ToList().Count > 0)
                    {
                        _thaythe(idNhom9324, 0);
                    }
                    _thaythe(idcu, idNhom9324);
                }
                dataContext.SaveChanges();

            }
            #endregion
            return true;
        }
        #endregion
        private void btnSuaDM_All_Click(object sender, EventArgs e)
        {
            cachmoi();
            MessageBox.Show("Cập nhật thành công !");

            dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lNhomDV = dataContext.NhomDVs.OrderBy(p => p.TenNhom).ToList();
            grcNhomDV.DataSource = _lNhomDV;
        }
        private bool cachmoi()
        {
            //add lại danh mục cũ thay mã mới
            dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<NhomDV> _lnhom = new List<NhomDV>();
            _lnhom = dataContext.NhomDVs.ToList();

            foreach (var item in _lnhom)
            {
                int idcu = item.IDNhom;
                int idmoi = 0;
                for (int j = 20; j < 100; j++)
                {
                    if (dataContext.NhomDVs.Where(p => p.IDNhom == j).ToList().Count <= 0)
                    {
                        idmoi = j;
                        break;
                    }
                }
                var kt = (from a in DungChung.Bien.NhomDV_QD.SetNhomDV()
                          where a.IDNhom == item.IDNhom && item.TenNhomCT == a.TenNhomCT
                          select new { IDNhom_moi = a.IDNhom, item.IDNhom }).ToList();
                if (kt.Count <= 0)
                {
                    NhomDV moi = new NhomDV();
                    moi.IDNhom = idmoi;
                    moi.TenNhom = item.TenNhom;
                    moi.TenNhomCT = item.TenNhomCT;
                    moi.Status = item.Status;
                    moi.STT = item.STT;
                    dataContext.NhomDVs.Add(moi);
                    dataContext.SaveChanges();
                    _thaythe(idcu, idmoi);
                    var xoa = dataContext.NhomDVs.Single(p => p.IDNhom == idcu);
                    dataContext.NhomDVs.Remove(xoa);
                    dataContext.SaveChanges();
                }
            }
            // add mã theo CV9324
            dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            foreach (var item in DungChung.Bien.NhomDV_QD.SetNhomDV())
            {
                var kt = dataContext.NhomDVs.Where(p => p.TenNhomCT == item.TenNhomCT).ToList();
                if (kt.Count <= 0)
                {
                    NhomDV moi = new NhomDV();
                    moi.IDNhom = item.IDNhom;
                    moi.TenNhom = item.TenNhom;
                    moi.TenNhomCT = item.TenNhomCT;
                    moi.Status = item.Status;
                    moi.STT = item.STT;
                    dataContext.NhomDVs.Add(moi);
                    dataContext.SaveChanges();
                }
            }
            //update
            dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var nhom = dataContext.NhomDVs.ToList();
            var ds = (from a in DungChung.Bien.NhomDV_QD.SetNhomDV()
                      join b in nhom on a.Tennhomcu equals b.TenNhomCT
                      select new { IDNhom_moi = a.IDNhom, b.IDNhom }).ToList();
            foreach (var item in ds)
            {
                _thaythe(item.IDNhom, item.IDNhom_moi);
                var xoa = dataContext.NhomDVs.Single(p => p.IDNhom == item.IDNhom);
                dataContext.NhomDVs.Remove(xoa);
                dataContext.SaveChanges();
            }

            dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var nhom2 = dataContext.NhomDVs.ToList();
            var ds2 = (from a in DungChung.Bien.NhomDV_QD.SetNhomDV()
                       join b in nhom2 on a.TenNhomCT equals b.TenNhomCT
                       where a.IDNhom != b.IDNhom
                       select new { IDNhom_moi = a.IDNhom, b.IDNhom }).ToList();
            foreach (var item in ds2)
            {
                _thaythe(item.IDNhom, item.IDNhom_moi);
                var xoa = dataContext.NhomDVs.Single(p => p.IDNhom == item.IDNhom);
                dataContext.NhomDVs.Remove(xoa);
                dataContext.SaveChanges();
            }
            return true;
        }
    }
}
