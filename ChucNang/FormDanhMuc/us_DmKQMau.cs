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
    public partial class us_DmKQMau : DevExpress.XtraEditors.XtraUserControl
    {
        public us_DmKQMau()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        int _TTLuu = 0;
        //int TTXoa = 0;
        string _Makqm = "";
        string[] arr_kq = new string[10];
        private void enableControl(bool T)
        {
            txtMaKQ.Properties.ReadOnly = !T;
            txtTenKQ.Properties.ReadOnly = !T;
            txtKetLuan.Properties.ReadOnly = !T;
            txtLoiDan.Properties.ReadOnly = !T;
            txtTenRG.Properties.ReadOnly = !T;
            txtCDM.Properties.ReadOnly = !T;
            txtCDTT.Properties.ReadOnly = !T;
            txtGoc.Properties.ReadOnly = !T;
            txtQRS.Properties.ReadOnly = !T;
            txtP1.Properties.ReadOnly = !T;
            txtP2.Properties.ReadOnly = !T;
            txtQT.Properties.ReadOnly = !T;
            txtST.Properties.ReadOnly = !T;
            txtT.Properties.ReadOnly = !T;
            txtTTT.Properties.ReadOnly = !T;
            txtTruc.Properties.ReadOnly = !T;
            txtNTS.Properties.ReadOnly = !T;
            lupMaDV.Properties.ReadOnly = !T;
            btnLuu.Enabled = T;
            btnMoi.Enabled = !T;
            btnSua.Enabled = !T;
            btnXoa.Enabled = !T;
            grcKQMau.Enabled = !T;
        }
        private void resetControl()
        {
            txtMaKQ.Text = "";
            txtTenKQ.Text = "";
            txtKetLuan.Text = "";
            txtLoiDan.Text = "";
            txtTTT.Text = "";
            txtTruc.Text = "";
            txtT.Text = "";
            txtP1.Text = "";
            txtP2.Text = "";
            txtQT.Text = "";
            txtQRS.Text = "";
            txtGoc.Text = "";
            txtCDTT.Text = "";
            txtCDM.Text = "";
            txtNTS.Text = "";
            txtST.Text = "";
            lupMaDV.Text = "";
            txtTenRG.Text = "";
        }
        #region
        private bool KTLuu()
        {
            if (string.IsNullOrEmpty(txtMaKQ.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã kết quả");
                txtMaKQ.Focus();
                return false;
            }
         
            return true;
        }

        #endregion
        List<KQMau> _lkq = new List<KQMau>();
        private void TimKiem()
        {
            string _tenkq = "";
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
                _tenkq = txtTimKiem.Text.Trim();
           _lkq = (from kq in dataContext.KQMaus where (kq.TenKQ.Contains(_tenkq)) select kq).OrderBy(p => p.TenKQ).ToList();
            grcKQMau.DataSource = _lkq.ToList();
        }
        private void us_DmKQMau_Load(object sender, EventArgs e)
        {
            
            var q = (from dv in dataContext.DichVus.Where(p=>p.PLoai==2) join tnhom in dataContext.TieuNhomDVs on dv.IdTieuNhom equals tnhom.IdTieuNhom select new { dv.MaDV, dv.TenDV,tnhom.TenRG }).OrderBy(p=>p.TenRG).OrderBy(p=>p.TenDV).ToList();
            lupMaDV.Properties.DataSource = q.ToList();
            _lkq = dataContext.KQMaus.OrderBy(p => p.TenKQ).ToList();
            grcKQMau.DataSource = _lkq;
            enableControl(false);
            
        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            enableControl(true);
            resetControl();
            _TTLuu = 1;
            txtMaKQ.Focus();
            var q = (from dv in dataContext.DichVus.Where(p => p.PLoai == 2) join tnhom in dataContext.TieuNhomDVs on dv.IdTieuNhom equals tnhom.IdTieuNhom select new { dv.MaDV, dv.TenDV, tnhom.TenRG }).OrderBy(p => p.TenRG).OrderBy(p => p.TenDV).ToList();
            lupMaDV.Properties.DataSource = q.ToList();
            if (Convert.ToInt32(lupMaDV.EditValue) == 6049 || Convert.ToInt32(lupMaDV.EditValue) == 6292)
            {
                txtTenKQ.Enabled = false;
                txtCDM.Enabled = true;
                txtNTS.Enabled = true;
                txtGoc.Enabled = true;
                txtTruc.Enabled = true;
                txtTTT.Enabled = true;
                txtP1.Enabled = true;
                txtP2.Enabled = true;
                txtQRS.Enabled = true;
                txtST.Enabled = true;
                txtT.Enabled = true;
                txtQT.Enabled = true;
                txtCDTT.Enabled = true;

            }
            else
            {
                txtTenKQ.Enabled = true;
                txtCDM.Enabled = false;
                txtNTS.Enabled = false;
                txtGoc.Enabled = false;
                txtTruc.Enabled = false;
                txtTTT.Enabled = false;
                txtP1.Enabled = false;
                txtP2.Enabled = false;
                txtQRS.Enabled = false;
                txtST.Enabled = false;
                txtT.Enabled = false;
                txtQT.Enabled = false;
                txtCDTT.Enabled = false;
            }
            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            enableControl(true);
            txtMaKQ.Enabled = false;
            _TTLuu = 2;
            txtTenKQ.Focus();
            if (Convert.ToInt32(lupMaDV.EditValue) == 6049 || Convert.ToInt32(lupMaDV.EditValue) == 6292)
            {
                txtTenKQ.Enabled = false;
                txtCDM.Enabled = true;
                txtNTS.Enabled = true;
                txtGoc.Enabled = true;
                txtTruc.Enabled = true;
                txtTTT.Enabled = true;
                txtP1.Enabled = true;
                txtP2.Enabled = true;
                txtQRS.Enabled = true;
                txtST.Enabled = true;
                txtT.Enabled = true;
                txtQT.Enabled = true;
                txtCDTT.Enabled = true;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            _Makqm = txtMaKQ.Text;
            
                DataRow dr = grvKQMau.GetFocusedDataRow();
                DialogResult dia = MessageBox.Show("Bạn có chắc chắn muốn xóa kết quả mẫu đã chọn?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dia == DialogResult.Yes)
                {
                    var xoa = dataContext.KQMaus.Single(p => p.MaKQ== (_Makqm));
                    dataContext.KQMaus.Remove(xoa);
                    dataContext.SaveChanges();
                    //btnXoa.Enabled = true;

                }
                var _lkq = dataContext.KQMaus.OrderBy(p => p.TenKQ).ToList();
                grcKQMau.DataSource = _lkq.ToList();
            
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (KTLuu())
            {
                switch (_TTLuu)
                {
                    case 1:
                        _Makqm = txtMaKQ.Text.Trim();
                        var ma = dataContext.KQMaus.Where(p => p.MaKQ== (_Makqm)).ToList();
                        if (ma.Count > 0)
                        {
                            MessageBox.Show("Mã kết quả mẫu đã có, vui lòng nhập mã khác");
                        }
                        else
                        {
                            KQMau kqm = new KQMau();
                            kqm.MaKQ = txtMaKQ.Text;
                            kqm.MaDV = lupMaDV.EditValue == null ? 0 : Convert.ToInt32(lupMaDV.EditValue);
                            if (Convert.ToInt32(lupMaDV.EditValue) == 6049 || Convert.ToInt32(lupMaDV.EditValue) == 6292)
                            {
                                string[] str = new string[] { txtCDM.Text, txtNTS.Text, txtGoc.Text, txtTruc.Text, txtTTT.Text, txtP1.Text, txtP2.Text, txtQRS.Text, txtST.Text, txtT.Text, txtQT.Text, txtCDTT.Text };
                                string str1 = QLBV_Library.QLBV_Ham.LuuChuoi(';', str);
                                kqm.TenKQ = str1;
                            }
                            else
                            {
                                kqm.TenKQ = txtTenKQ.Text;
                            }
                            kqm.TenRG = txtTenRG.Text;
                            kqm.KetLuan = txtKetLuan.Text;
                            kqm.LoiDan = txtLoiDan.Text;
                            dataContext.KQMaus.Add(kqm);
                            dataContext.SaveChanges();
                            enableControl(false);
                            MessageBox.Show("Lưu thành công!");
                        }
                        break;

                    case 2:
                        if (!string.IsNullOrEmpty(txtMaKQ.Text))
                        {
                            string makq = txtMaKQ.Text;
                            KQMau kqmsua = dataContext.KQMaus.Single(p => p.MaKQ== (makq));
                            kqmsua.MaKQ = txtMaKQ.Text;
                            kqmsua.MaDV = lupMaDV.EditValue == null ? 0 : Convert.ToInt32(lupMaDV.EditValue);
                            if (Convert.ToInt32(lupMaDV.EditValue) == 6049 || Convert.ToInt32(lupMaDV.EditValue) == 6292)
                            {
                                string[] str = new string[] { txtCDM.Text, txtNTS.Text, txtGoc.Text, txtTruc.Text, txtTTT.Text, txtP1.Text, txtP2.Text, txtQRS.Text, txtST.Text, txtT.Text, txtQT.Text, txtCDTT.Text };
                                string str1 = QLBV_Library.QLBV_Ham.LuuChuoi(';', str);
                                kqmsua.TenKQ = str1;
                            }
                            else
                            {
                                kqmsua.TenKQ = txtTenKQ.Text;
                            }
                            kqmsua.TenRG = txtTenRG.Text;
                            kqmsua.KetLuan = txtKetLuan.Text;
                            kqmsua.LoiDan = txtLoiDan.Text;
                            dataContext.SaveChanges();
                            MessageBox.Show("Lưu thành công!");
                            enableControl(false);
                        }
                        break;
                }
                var _lkqm = dataContext.KQMaus.OrderBy(p => p.TenKQ).ToList();
                grcKQMau.DataSource = _lkqm.ToList();
            }
        }

        private void grvKQMau_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvKQMau.GetFocusedRowCellValue(colMaKQ) != null && grvKQMau.GetFocusedRowCellValue(colMaKQ).ToString() != "")
            {
                txtMaKQ.Text = grvKQMau.GetFocusedRowCellValue(colMaKQ).ToString();
                if (grvKQMau.GetFocusedRowCellValue(colMaKQ) != null && grvKQMau.GetFocusedRowCellValue(colMaKQ).ToString() != "")
                {
                    txtMaKQ.Text = grvKQMau.GetFocusedRowCellValue(colMaKQ).ToString();
                }
                else
                {
                    txtMaKQ.Text = "";
                }
                if (grvKQMau.GetFocusedRowCellValue(colMaDV) != null && grvKQMau.GetFocusedRowCellValue(colMaDV).ToString() != "")
                {
                    lupMaDV.EditValue = Convert.ToInt32(grvKQMau.GetFocusedRowCellValue(colMaDV));
                }
                else
                {
                    lupMaDV.EditValue =0;
                }
                if (grvKQMau.GetFocusedRowCellValue(colTenKQ) != null && grvKQMau.GetFocusedRowCellValue(colTenKQ).ToString() != "")
                {
                    
                    if (Convert.ToInt32(grvKQMau.GetFocusedRowCellValue(colMaDV)) == 6049 || Convert.ToInt32(grvKQMau.GetFocusedRowCellValue(colMaDV)) == 6292)
                    {
                        txtTenKQ.Enabled = false;
                        string ketqua = "";
                        if (!string.IsNullOrEmpty(grvKQMau.GetFocusedRowCellValue(colTenKQ).ToString()))
                        {
                            ketqua = grvKQMau.GetFocusedRowCellValue(colTenKQ).ToString();
                        }
                        
                        arr_kq = QLBV_Library.QLBV_Ham.LayChuoi(';', ketqua);
                        txtCDM.Text = arr_kq[0] == null ? "" : arr_kq[0];
                        txtNTS.Text = arr_kq[1] == null ? "" : arr_kq[1];
                        txtGoc.Text = arr_kq[2] == null ? "" : arr_kq[2];
                        txtTruc.Text = arr_kq[3] == null ? "" : arr_kq[3];
                        txtTTT.Text = arr_kq[4] == null ? "" : arr_kq[4];
                        txtP1.Text = arr_kq[5] == null ? "" : arr_kq[5];
                        txtP2.Text = arr_kq[6] == null ? "" : arr_kq[6];
                        txtQRS.Text = arr_kq[7] == null ? "" : arr_kq[7];
                        txtST.Text = arr_kq[8] == null ? "" : arr_kq[8];
                        txtT.Text = arr_kq[9] == null ? "" : arr_kq[9];
                        txtQT.Text = arr_kq[10] == null ? "" : arr_kq[10];
                        txtCDTT.Text = arr_kq[11] == null ? "" : arr_kq[11];
                        txtTenKQ.Text = "";
                    }
                    else
                    {
                        txtTenKQ.Text = grvKQMau.GetFocusedRowCellValue(colTenKQ).ToString();
                        txtCDM.Text = ""; ;
                        txtNTS.Text = ""; ;
                        txtGoc.Text = ""; ;
                        txtTruc.Text = ""; ;
                        txtTTT.Text = ""; ;
                        txtP1.Text = ""; ;
                        txtP2.Text = ""; ;
                        txtQRS.Text = ""; ;
                        txtST.Text = ""; ;
                        txtT.Text = ""; ;
                        txtQT.Text = ""; ;
                        txtCDTT.Text = ""; ;
                    }
                }
                else
                {
                    txtTenKQ.Text = "";
                }

                if (grvKQMau.GetFocusedRowCellValue(colTenRG) != null && grvKQMau.GetFocusedRowCellValue(colTenRG).ToString() != "")
                {
                    txtTenRG.Text = grvKQMau.GetFocusedRowCellValue(colTenRG).ToString();
                }
                else
                {
                    txtTenRG.Text = "";
                }
                if (grvKQMau.GetFocusedRowCellValue(colKetLuan) != null && grvKQMau.GetFocusedRowCellValue(colKetLuan).ToString() != "")
                {
                    txtKetLuan.Text = grvKQMau.GetFocusedRowCellValue(colKetLuan).ToString();
                }
                else
                {
                    txtKetLuan.Text = "";
                }

                if (grvKQMau.GetFocusedRowCellValue(colLoiDan) != null && grvKQMau.GetFocusedRowCellValue(colLoiDan).ToString() != "")
                {
                    txtLoiDan.Text = grvKQMau.GetFocusedRowCellValue(colLoiDan).ToString();
                }
                else
                {
                    txtLoiDan.Text = "";
                }

            }
            else
            {
                txtMaKQ.Text = "";
            }
        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            string tk = "";
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                tk = txtTimKiem.Text;
            }
            grcKQMau.DataSource = _lkq.Where(p => p.TenKQ.Contains(tk));
        }

        private void lupMaDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lupMaDV.EditValue) == 6049 || Convert.ToInt32(lupMaDV.EditValue) == 6292)
            {
                txtTenKQ.Enabled = false;
                txtCDM.Enabled = true;
                txtNTS.Enabled = true;
                txtGoc.Enabled = true;
                txtTruc.Enabled = true;
                txtTTT.Enabled = true;
                txtP1.Enabled = true;
                txtP2.Enabled = true;
                txtQRS.Enabled = true;
                txtST.Enabled = true;
                txtT.Enabled = true;
                txtQT.Enabled = true;
                txtCDTT.Enabled = true;
            
            }
            else
            {
                txtTenKQ.Enabled = true;
                txtCDM.Enabled = false;
                txtNTS.Enabled = false;
                txtGoc.Enabled = false;
                txtTruc.Enabled = false;
                txtTTT.Enabled = false;
                txtP1.Enabled = false;
                txtP2.Enabled = false;
                txtQRS.Enabled = false;
                txtST.Enabled = false;
                txtT.Enabled = false;
                txtQT.Enabled = false;
                txtCDTT.Enabled = false;
            }
        }

        
    }
}
