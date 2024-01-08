using System;using QLBV_Database;
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
    public partial class us_DmDichVuCLS : DevExpress.XtraEditors.XtraUserControl
    {
        public us_DmDichVuCLS()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        int _TTLuu = 0;
        //int TTXoa = 0;
        string _Madvct = "";
        private void enableControl(bool T)
        {
            txtMaDV.Properties.ReadOnly = !T;
            txtTenDV.Properties.ReadOnly = !T;
            cboTSBTn.Properties.ReadOnly = !T;
            txtSTT.Properties.ReadOnly = !T;
            txtSTTBC.Properties.ReadOnly = !T;
            txtSoTTF.Properties.ReadOnly = !T;
            lupMaDV.Properties.ReadOnly = !T;
            cboDonVi.Properties.ReadOnly = !T;
            txtTenMay.Properties.ReadOnly = !T;
            cboTSBTnu.Properties.ReadOnly = !T;
            txtTSnTu.Properties.ReadOnly = !T;
            txtTSnDen.Properties.ReadOnly = !T;
            txtTSnuTu.Properties.ReadOnly = !T;
            txtTSnuDen.Properties.ReadOnly = !T;
            txtKetQua.Properties.ReadOnly = !T;
            txtTSBT.Properties.ReadOnly = !T;
            txtMaQuyTrinh.Properties.ReadOnly = !T;
            btnLuu.Enabled = T;
            btnMoi.Enabled = !T;
            btnSua.Enabled = !T;
            btnXoa.Enabled = !T;
            grcDichVuct.Enabled = !T;
        }
        private void resetControl()
        {
            txtMaDV.Text = "";
            txtTenDV.Text = "";
            cboTSBTn.Text = "";
            txtSTT.Text = "";
            txtSoTTF.Text = "";
            txtSTTBC.Text = "";
            lupMaDV.Text = "";
            cboDonVi.Text = "";
            txtTenMay.Text = "";
            cboTSBTnu.Text = "";
            txtTSnTu.Text = "";
            txtTSnDen.Text = "";
            txtTSnuTu.Text = "";
            txtTSnuDen.Text = "";
            txtKetQua.Text = "";
            txtTSBT.Text = "";
            txtMaQuyTrinh.Text = "";
        }

        private void sf(string n)
        {
            String.Format("0:00.0000", n);
        }


        #region
        private bool KTLuu()
        {
            if (string.IsNullOrEmpty(txtMaDV.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã dịch vụ!");
                txtMaDV.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtSTT.Text))
            {
                MessageBox.Show("Bạn chưa chọn số thứ tự!");
                txtSTT.Focus();
                return false;
            }
            else
            {
                int maDV = 0;
                int stt = Convert.ToInt32(txtSTT.Text);
               
                int idTN = 0;
                if (lupMaDV.EditValue != null)
                    maDV = Convert.ToInt32( lupMaDV.EditValue);
                var qtn = dataContext.DichVus.Where(p => p.MaDV == maDV).ToList();
                if (qtn.Count > 0)
                {
                    idTN = qtn.First().IdTieuNhom == null ? 0 : qtn.First().IdTieuNhom.Value;
                }
                if (_TTLuu == 1) // Mới
                {
                    var ktSTT = from tn in dataContext.TieuNhomDVs.Where(p => p.IdTieuNhom == idTN)
                                join dv in dataContext.DichVus on tn.IdTieuNhom equals dv.IdTieuNhom
                                join dvct in dataContext.DichVucts.Where(p => p.STT == stt) on dv.MaDV equals dvct.MaDV
                                select dvct;
                    if (ktSTT.Count() > 0)
                    {
                        MessageBox.Show("STT này đã có trong Tiểu nhóm.!");
                        return false;
                    }
                }
                if (_TTLuu == 2) // Sửa
                {
                    var ktSTT = dataContext.DichVucts.Where(p => p.MaDVct == txtMaDV.Text && p.STT == stt).ToList();
                    if (ktSTT.Count == 0)
                    {
                        var ktSTT2 = from tn in dataContext.TieuNhomDVs.Where(p => p.IdTieuNhom == idTN)
                                     join dv in dataContext.DichVus on tn.IdTieuNhom equals dv.IdTieuNhom
                                     join dvct in dataContext.DichVucts.Where(p => p.STT == stt) on dv.MaDV equals dvct.MaDV
                                     select dvct;
                        if (ktSTT.Count() > 0)
                        {
                            MessageBox.Show("STT này đã có trong Tiểu nhóm.!");
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        #endregion
        List<DichVuct> _ldvct = new List<DichVuct>();


        private void us_DmDichVuCLS_Load(object sender, EventArgs e)
        {
            if(DungChung.Bien.MaBV != "24012")
            {
                colMaQuyTrinh.Visible = false;
            }

            var dlThua = dataContext.DichVucts.Where(p => p.MaDV == null || p.MaDV == 0).ToList();
            foreach (var item in dlThua)
            {
                dataContext.DichVucts.Remove(item);
            }
            dataContext.SaveChanges();
            lupMaDVgv.DataSource = dataContext.DichVus.ToList();
            var q = (from dv in dataContext.DichVus
                     join tn in dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                     join ndv in dataContext.NhomDVs on tn.IDNhom equals ndv.IDNhom
                     where (ndv.Status==2)
                     select new { dv.MaDV, dv.TenDV, tn.TenTN }).ToList();
            txtTimKiem.Text = "";
            if (q.Count > 0)
            {
                lupMaDV.Properties.DataSource = q.OrderBy(p => p.TenTN);
            }
            _ldvct = dataContext.DichVucts.OrderBy(p => p.TenDVct).ToList();

            grcDichVuct.DataSource = _ldvct;
            var qtn = from dv in dataContext.DichVus.Where(p => p.PLoai == 2) select new { dv.TenDV, dv.MaDV };
            lupTimTenDV.Properties.DataSource = qtn.OrderBy(p => p.TenDV).ToList();
            List<String> _lDonvi = new List<String>();
            _lDonvi.Add("µmol/l");
            _lDonvi.Add("mmol/l");
            _lDonvi.Add("g/l");
            _lDonvi.Add("l/l");
            _lDonvi.Add("fl");
            _lDonvi.Add("pg");
            _lDonvi.Add("%");
            _lDonvi.Add("x10³/l");
            _lDonvi.Add("x10⁹/l");
            _lDonvi.Add("x10¹²/l");
            _lDonvi.Add("phút");
            var qdv = dataContext.DichVucts.Where(p => p.DonVi != null && p.DonVi != "").Select(p => p.DonVi).Distinct().ToList();
            foreach (var item in qdv)
            {
                _lDonvi.Add(item);
            }
            foreach (var item in _lDonvi.Distinct())
            {
                cboDonVi.Properties.Items.Add(item);
            }
            List<TieuNhomDV> _lTN = new List<TieuNhomDV>();
            List<TieuNhomDV> qtndv = (from tn in dataContext.TieuNhomDVs
                                      join ndv in dataContext.NhomDVs.Where(p => p.TenNhomCT == "Xét nghiệm" || p.TenNhomCT == "Thủ thuật, phẫu thuật" || p.TenNhomCT == "Chẩn đoán hình ảnh" || p.TenNhomCT == "Thăm dò chức năng")
                                         on tn.IDNhom equals ndv.IDNhom
                                      select tn).OrderBy(p => p.TenTN).ToList();
            _lTN.Add(new TieuNhomDV { TenTN = "Tất cả", IdTieuNhom = 0 });
            _lTN.AddRange(qtndv);
            lupTieuNhom.Properties.DataSource = _lTN;
            lupTieuNhom.EditValue = 0;
            enableControl(false);
        }
        private void btnMoi_Click(object sender, EventArgs e)
        {
            enableControl(true);
            resetControl();
            _TTLuu = 1;
            txtMaDV.Enabled = true;
            txtMaDV.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            enableControl(true);
            txtMaDV.Enabled = false;
            _TTLuu = 2;
            txtTenDV.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            _Madvct = txtMaDV.Text;
            var ma = dataContext.CLScts.Where(p => p.MaDVct== (_Madvct)).ToList();
            if (ma.Count > 0)
            {
                MessageBox.Show("Mã dịch vụ CLS đã được sử dụng, Bạn không thể xóa.");
            }
            else
            {
                DataRow dr = grvDichVuct.GetFocusedDataRow();
                DialogResult dia = MessageBox.Show("Bạn có chắc chắn muốn xóa dịch vụ CLS đã chọn?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dia == DialogResult.Yes)
                {
                    //grcDanToc.ControlRemoved();
                    var xoa = dataContext.DichVucts.Single(p => p.MaDVct== (_Madvct));
                    dataContext.DichVucts.Remove(xoa);
                    dataContext.SaveChanges();
                    //btnXoa.Enabled = true;

                }

                grcDichVuct.DataSource = _ldvct.ToList();
            }

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (KTLuu())
            {
                switch (_TTLuu)
                {
                    case 1:
                        _Madvct = txtMaDV.Text.Trim();
                        var ma = dataContext.DichVucts.Where(p => p.MaDVct== (_Madvct)).ToList();
                        if (ma.Count > 0)
                        {
                            MessageBox.Show("Mã dịch vụ đã có, vui lòng nhập mã khác");
                        }
                        else
                        {
                            DichVuct dv = new DichVuct();
                            dv.MaDVct = txtMaDV.Text;
                            dv.MaDV = lupMaDV.EditValue == null ? 0 : Convert.ToInt32(lupMaDV.EditValue);
                            dv.TenDVct = txtTenDV.Text;
                            dv.TSBTn = cboTSBTn.Text;
                            dv.TSBTnu = cboTSBTnu.Text;
                            if (DungChung.Bien.MaBV == "24012")
                            {
                                dv.MaQuyTrinh = txtMaQuyTrinh.Text;
                            }
                            int stt = 0;
                            if (int.TryParse(txtSTT.Text, out stt))
                                dv.STT = stt;
                            else
                                dv.STT = -1;
                            Byte sttBC = 0;
                            if (Byte.TryParse(txtSTTBC.Text, out sttBC))
                                dv.STT_R = sttBC;
                            else
                                dv.STT_R = 0;

                            try { 
                            dv.STT_F = Convert.ToByte(txtSoTTF.Text);
                                }
                            catch
                            {

                                dv.STT_F = 0;
                            }
                            dv.Status = ckSuDung.Checked ? 1 : 0; //cboLuustatus.SelectedIndex;
                            dv.DonVi = cboDonVi.Text;
                            if (!string.IsNullOrEmpty(txtTSnTu.Text))
                                dv.TSnTu = Convert.ToDouble(txtTSnTu.Text);
                            if (!string.IsNullOrEmpty(txtTSnDen.Text))
                                dv.TSnDen = Convert.ToDouble(txtTSnDen.Text);
                            else
                                dv.TSnDen = null;
                            if (!string.IsNullOrEmpty(txtTSnuTu.Text))
                                dv.TSnuTu = Convert.ToDouble(txtTSnuTu.Text);
                            if (!string.IsNullOrEmpty(txtTSnuDen.Text))
                                dv.TSnuDen = Convert.ToDouble(txtTSnuDen.Text);
                            else
                                dv.TSnuDen = null;
                            dv.TenMay = txtTenMay.Text;
                            dv.TSBT = txtTSBT.Text;
                            dv.KetQua = txtKetQua.Text;
                            dataContext.DichVucts.Add(dv);
                            dataContext.SaveChanges();
                            enableControl(false);
                            MessageBox.Show("Lưu thành công!");
                            grcDichVuct.DataSource = _ldvct.ToList();
                        }
                        break;

                    case 2:
                        if (!string.IsNullOrEmpty(txtMaDV.Text))
                        {
                            string madv =txtMaDV.Text; // dichj vuj chi tieets
                            DichVuct dvsua = dataContext.DichVucts.Single(p => p.MaDVct== madv);
                            dvsua.MaDVct = txtMaDV.Text;
                            dvsua.MaDV = lupMaDV.EditValue == null ? 0 : Convert.ToInt32(lupMaDV.EditValue);
                            dvsua.TenDVct = txtTenDV.Text;
                            dvsua.TSBTn = cboTSBTn.Text;
                            dvsua.TSBTnu = cboTSBTnu.Text;
                            if(DungChung.Bien.MaBV == "24012")
                            {
                                dvsua.MaQuyTrinh = txtMaQuyTrinh.Text;
                            }
                            int stt = 0;
                            int.TryParse(txtSTT.Text, out stt);
                            dvsua.STT = stt;
                            try
                            {
                                dvsua.STT_F = Convert.ToByte(txtSoTTF.Text);
                            }
                            catch
                            {

                                dvsua.STT_F = 0;
                            }

                            try
                            {
                                dvsua.STT_R = Convert.ToByte(txtSTTBC.Text);
                            }
                            catch
                            {

                                dvsua.STT_R = 0;
                            }
                            dvsua.Status = ckSuDung.Checked ? 1 : 0; // cboLuustatus.SelectedIndex;
                            dvsua.DonVi = cboDonVi.Text;
                            if (!string.IsNullOrEmpty(txtTSnTu.Text))
                                dvsua.TSnTu = Convert.ToDouble(txtTSnTu.Text);
                            if (!string.IsNullOrEmpty(txtTSnDen.Text))
                                dvsua.TSnDen = Convert.ToDouble(txtTSnDen.Text);
                            else
                                dvsua.TSnDen = null;
                            if (!string.IsNullOrEmpty(txtTSnuTu.Text))
                                dvsua.TSnuTu = Convert.ToDouble(txtTSnuTu.Text);
                            if (!string.IsNullOrEmpty(txtTSnuDen.Text))
                                dvsua.TSnuDen = Convert.ToDouble(txtTSnuDen.Text);
                            else
                                dvsua.TSnuDen = null;
                            dvsua.TenMay = txtTenMay.Text;
                            dvsua.TSBT = txtTSBT.Text;
                            dvsua.KetQua = txtKetQua.Text;
                            dataContext.SaveChanges();
                            MessageBox.Show("Lưu thành công!");
                            enableControl(false);
                        }
                        break;
                }

               
            }
        }

        private void grvDichVuct_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvDichVuct.GetFocusedRowCellValue(colMaDVct) != null && grvDichVuct.GetFocusedRowCellValue(colMaDVct).ToString() != "")
            {
                txtMaDV.Text = grvDichVuct.GetFocusedRowCellValue(colMaDVct).ToString();
                var qdvct = dataContext.DichVucts.Where(p => p.MaDVct == txtMaDV.Text).FirstOrDefault();
                if(qdvct != null)
                {
                    if (qdvct.Status == 1)
                        ckSuDung.Checked = true;
                    else
                        ckSuDung.Checked = false;
                    }

                if (grvDichVuct.GetFocusedRowCellValue(colMaDVct) != null && grvDichVuct.GetFocusedRowCellValue(colMaDVct).ToString() != "")
                {
                    txtMaDV.Text = grvDichVuct.GetFocusedRowCellValue(colMaDVct).ToString();
                }
                else
                {
                    txtMaDV.Text = "";
                }
                if (grvDichVuct.GetFocusedRowCellValue(colMaDV) != null)
                {
                    lupMaDV.EditValue = Convert.ToInt32( grvDichVuct.GetFocusedRowCellValue(colMaDV));
                }
                else
                {
                    lupMaDV.EditValue = 0;
                }
                if (grvDichVuct.GetFocusedRowCellValue(colTenDVct) != null && grvDichVuct.GetFocusedRowCellValue(colTenDVct).ToString() != "")
                {
                    txtTenDV.Text = grvDichVuct.GetFocusedRowCellValue(colTenDVct).ToString();
                }
                else
                {
                    txtTenDV.Text = "";
                }

                if (grvDichVuct.GetFocusedRowCellValue(colTSBTn) != null && grvDichVuct.GetFocusedRowCellValue(colTSBTn).ToString() != "")
                {
                    cboTSBTn.Text = grvDichVuct.GetFocusedRowCellValue(colTSBTn).ToString();
                }
                else
                {
                    cboTSBTn.Text = "";
                }
                if (grvDichVuct.GetFocusedRowCellValue(colTSBT) != null && grvDichVuct.GetFocusedRowCellValue(colTSBT).ToString() != "")
                {
                    txtTSBT.Text = grvDichVuct.GetFocusedRowCellValue(colTSBT).ToString();
                }
                else
                {
                    txtTSBT.Text = "";
                }

                if (grvDichVuct.GetFocusedRowCellValue(colSTT) != null && grvDichVuct.GetFocusedRowCellValue(colSTT).ToString() != "")
                {
                    txtSTT.Text = grvDichVuct.GetFocusedRowCellValue(colSTT).ToString();
                }
                else
                {
                    txtSTT.Text = "";
                }
                if (grvDichVuct.GetFocusedRowCellValue(colSoTT_F) != null && grvDichVuct.GetFocusedRowCellValue(colSoTT_F).ToString() != "")
                {
                    txtSoTTF.Text = grvDichVuct.GetFocusedRowCellValue(colSoTT_F).ToString();
                }
                else
                {
                    txtSoTTF.Text = "";
                }

                if (grvDichVuct.GetFocusedRowCellValue(colSTT_BC) != null && grvDichVuct.GetFocusedRowCellValue(colSTT_BC).ToString() != "")
                {
                    txtSTTBC.Text = grvDichVuct.GetFocusedRowCellValue(colSTT_BC).ToString();
                }
                else
                {
                    txtSTTBC.Text = "";
                }
                if (grvDichVuct.GetFocusedRowCellValue(colDonVi) != null && grvDichVuct.GetFocusedRowCellValue(colDonVi).ToString() != "")
                {
                    cboDonVi.Text = grvDichVuct.GetFocusedRowCellValue(colDonVi).ToString();
                }
                else
                {
                    cboDonVi.Text = "";
                }
                if (grvDichVuct.GetFocusedRowCellValue(colTSBTnu) != null && grvDichVuct.GetFocusedRowCellValue(colTSBTnu).ToString() != "")
                {
                    cboTSBTnu.Text = grvDichVuct.GetFocusedRowCellValue(colTSBTnu).ToString();
                }
                else
                {
                    cboTSBTnu.Text = "";
                }
                if (grvDichVuct.GetFocusedRowCellValue(colTSnTu) != null && grvDichVuct.GetFocusedRowCellValue(colTSnTu).ToString() != "")
                {
                    txtTSnTu.Text = string.Format("{0:0.00}", grvDichVuct.GetFocusedRowCellValue(colTSnTu));
                }
                else
                {
                    txtTSnTu.Text = "";
                }
                if (grvDichVuct.GetFocusedRowCellValue(colTSnuTu) != null && grvDichVuct.GetFocusedRowCellValue(colTSnuTu).ToString() != "")
                {
                    txtTSnuTu.Text = string.Format("{0:0.00}", grvDichVuct.GetFocusedRowCellValue(colTSnuTu));
                }
                else
                {
                    txtTSnuTu.Text = "";
                }
                if (grvDichVuct.GetFocusedRowCellValue(colTSnDen) != null && grvDichVuct.GetFocusedRowCellValue(colTSnDen).ToString() != "")
                {
                    txtTSnDen.Text = string.Format("{0:0.00}",grvDichVuct.GetFocusedRowCellValue(colTSnDen));
                }
                else
                {
                    txtTSnDen.Text = "";
                }
                if (grvDichVuct.GetFocusedRowCellValue(colTSnuDen) != null && grvDichVuct.GetFocusedRowCellValue(colTSnuDen).ToString() != "")
                {
                    txtTSnuDen.Text = string.Format("{0:0.00}", grvDichVuct.GetFocusedRowCellValue(colTSnuDen));
                    //grvDichVuct.GetFocusedRowCellValue(colTSnuDen).ToString()
                }
                else
                {
                    txtTSnuDen.Text = "";
                }
                if (grvDichVuct.GetFocusedRowCellValue(colTenMay) != null && grvDichVuct.GetFocusedRowCellValue(colTenMay).ToString() != "")
                {
                    txtTenMay.Text = grvDichVuct.GetFocusedRowCellValue(colTenMay).ToString();
                }
                else
                {
                    txtTenMay.Text = "";
                }
                if (grvDichVuct.GetFocusedRowCellValue(colKetQua) != null && grvDichVuct.GetFocusedRowCellValue(colKetQua).ToString() != "")
                {
                    txtKetQua.Text = grvDichVuct.GetFocusedRowCellValue(colKetQua).ToString();
                }
                else
                {
                    txtKetQua.Text = "";
                }
                if (grvDichVuct.GetFocusedRowCellValue(colMaQuyTrinh) != null && grvDichVuct.GetFocusedRowCellValue(colMaQuyTrinh).ToString() != "")
                {
                    txtMaQuyTrinh.Text = grvDichVuct.GetFocusedRowCellValue(colMaQuyTrinh).ToString();
                }
                else
                {
                    txtTenMay.Text = "";
                }
            }
            else
            {
                txtMaDV.Text = "";
            }
        }

        private void txtMaDV_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtTenDV_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            timkiem();
        }



        private void lupTimTenDV_EditValueChanged(object sender, EventArgs e)
        {
            timkiem();
        }
        private void timkiem()
        {
            int idTN = 0;
            if (lupTieuNhom.EditValue != null)
            {
                idTN = Convert.ToInt32(lupTieuNhom.EditValue);
            }
            int tkdv = 0;// max dichj vuj
            if (lupTimTenDV.EditValue != null)
            {
                tkdv = Convert.ToInt32(lupTimTenDV.EditValue);

            }
            var q = from ndv in dataContext.NhomDVs.Where(p => p.TenNhomCT == "Chẩn đoán hình ảnh" || p.TenNhomCT == "Xét nghiệm" || p.TenNhomCT == "Thủ thuật, phẫu thuật" || p.TenNhomCT == "Thăm dò chức năng")
                            join tn in dataContext.TieuNhomDVs on ndv.IDNhom equals tn.IDNhom
                            join dv in dataContext.DichVus on tn.IdTieuNhom equals dv.IdTieuNhom
                            join dvct in dataContext.DichVucts on dv.MaDV equals dvct.MaDV
                            where (tkdv == 0?true: dv.MaDV==tkdv)
                            where (idTN==0?true: tn.IdTieuNhom==idTN)
                            where dvct.TenDVct.Contains(txtTimKiem.Text) || dvct.MaDVct.Contains(txtTimKiem.Text)
                            select dvct;
                    grcDichVuct.DataSource = q.ToList();
           
        }

        private void lupTieuNhom_EditValueChanged(object sender, EventArgs e)
        {
            int idTN = 0;
            if (lupTieuNhom.EditValue != null)
            {
                idTN = Convert.ToInt32(lupTieuNhom.EditValue);
            }
            List<DichVu> _lDv = new List<DichVu>();
            List<DichVu> qDV = new List<DichVu>();
            _lDv.Add(new DichVu { TenDV = "Tất cả", MaDV = 0 });
            qDV = (from ndv in dataContext.NhomDVs.Where(p => p.TenNhomCT == "Chẩn đoán hình ảnh" || p.TenNhomCT == "Xét nghiệm" || p.TenNhomCT == "Thủ thuật, phẫu thuật" || p.TenNhomCT == "Thăm dò chức năng")
                       join tn in dataContext.TieuNhomDVs on ndv.IDNhom equals tn.IDNhom
                       join dv in dataContext.DichVus on tn.IdTieuNhom equals dv.IdTieuNhom
                       where (idTN == 0 ? true : tn.IdTieuNhom == idTN)
                       select dv).ToList();
            _lDv.AddRange(qDV);
            lupTimTenDV.Properties.DataSource = _lDv;
            lupTimTenDV.EditValue = 0;
            timkiem();
        }

        private void grcDichVuct_DataSourceChanged(object sender, EventArgs e)
        {
            grvDichVuct_FocusedRowChanged(sender, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(0, 0));
        }

        private void txtMaDV_Leave(object sender, EventArgs e)
        {

            string madvct = txtMaDV.Text;
            var ktMadv = dataContext.DichVucts.Where(p => p.MaDVct == madvct).ToList();
            if (_TTLuu == 1 && ktMadv.Count > 0)
            {
                MessageBox.Show("Mã Dịch vụ chi tiết đã tồn tại");
            }

        }

        private void layTSBT()
        {
            String _nam = "", _nu = "";
            if (cboTSBTn.Text == "") // Nam
            {
                _nam += txtTSnTu.Text + "-" + txtTSnDen.Text;
            }
            else
            {
                if (txtTSnTu.Text == "")
                {
                    _nam += cboTSBTn.Text + txtTSnDen.Text;
                }
                else if (txtTSnDen.Text == "")
                {
                    _nam += cboTSBTn.Text + txtTSnTu.Text;
                }
                else
                    _nam += cboTSBTn.Text + "(" + txtTSnTu.Text + "-" + txtTSnDen.Text + ") ";
            }

            if (cboTSBTnu.Text == "") // Nữ
            {
                _nu = "";
            }
            else
            {
                if (txtTSnuTu.Text == "")
                {
                    _nu += cboTSBTnu.Text + txtTSnuDen.Text;
                }
                else if (txtTSnuDen.Text == "")
                {
                    _nu += cboTSBTnu.Text + txtTSnuTu.Text;
                }
                else
                    _nu += cboTSBTnu.Text + "(" + txtTSnuTu.Text + "-" + txtTSnuDen.Text + ") ";
            }
            if (_nu == "")
            {
                txtTSBT.Text = _nam;
            }
            else
                txtTSBT.Text = _nam + "\r\n" + _nu;

        }

        private void cboTSBTn_Leave(object sender, EventArgs e)
        {
            layTSBT();
        }

        private void txtTSnTu_Leave(object sender, EventArgs e)
        {
            layTSBT();
        }

        private void txtTSnDen_Leave(object sender, EventArgs e)
        {
            layTSBT();
        }

        private void cboTSBTnu_Leave(object sender, EventArgs e)
        {
            layTSBT();
        }

        private void txtTSnuTu_Leave(object sender, EventArgs e)
        {
            layTSBT();
        }

        private void txtTSnuDen_Leave(object sender, EventArgs e)
        {
            layTSBT();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var q = data.DichVucts.ToList();
            bool _dongy = true;
            foreach (var a in q) {
                if (!string.IsNullOrEmpty(a.TSBTn))
                {
                    _dongy = false;
                    break;
                }
            }
            if (_dongy)
            {
                foreach (var item in q)
                {
                    item.TSBTn = item.TSBT;
                    // Lấy TSBT
                    String _nam = "", _nu = "";
                    if (String.IsNullOrEmpty(item.TSBTn)) // Nam
                    {
                        _nam += item.TSnTu + "-" + item.TSnDen;
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(item.TSBTnu))
                        {
                            _nam += item.TSBTn + item.TSnDen;
                        }
                        else if (txtTSnDen.Text == "")
                        {
                            _nam += item.TSBTn + item.TSnTu;
                        }
                        else
                            _nam += item.TSBTn + "(" + item.TSnTu + "-" + item.TSnDen + ") ";
                    }

                    if (String.IsNullOrEmpty(item.TSBTnu)) // Nữ
                    {
                        _nu = "";
                    }
                    else
                    {
                        if (item.TSnuTu == null)
                        {
                            _nu += item.TSBTnu + item.TSnuDen;
                        }
                        else if (txtTSnuDen.Text == "")
                        {
                            _nu += item.TSBTnu + item.TSnuTu;
                        }
                        else
                            _nu += item.TSBTnu + "(" + item.TSnuTu + "-" + item.TSnuDen + ") ";
                    }
                    if (_nu == "")
                    {
                        item.TSBT = _nam;
                    }
                    else
                        item.TSBT = _nam + "\r\n" + _nu;
                }
            }
            data.SaveChanges();
        }

        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            timkiem();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            enableControl(false);
            grvDichVuct_FocusedRowChanged(sender, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(0, 0));
        }

        private void btnUpdateDV_Click(object sender, EventArgs e)
        {
            FormThamSo.frm_UpdateDVct frm = new FormThamSo.frm_UpdateDVct();
            frm.ShowDialog();
        }

        private void grvDichVuct_DataSourceChanged(object sender, EventArgs e)
        {
            grvDichVuct_FocusedRowChanged(null,null);
        }

        private void txtTSnTu_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void grcDichVuct_Click(object sender, EventArgs e)
        {

        }

        private void cboTSBTn_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
