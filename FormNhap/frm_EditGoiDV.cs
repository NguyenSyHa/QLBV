using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormNhap
{
    public partial class frm_EditGoiDV : DevExpress.XtraEditors.XtraForm
    {
        int _ID = 0, _Status = -1;
        public frm_EditGoiDV(int id, int status)//0 thêm mới, 1 sửa
        {
            InitializeComponent();
            _ID = id;
            _Status = status;
        }
        public delegate void ReLoad();
        public ReLoad reloaddata;
        private void gridControl2_Click(object sender, EventArgs e)
        {

        }
        public class DVC
        {
            public int? MaDV;
            public int? madv
            { set { MaDV = value; } get { return MaDV; } }
            public string TenDV;
            public string tendv
            { set { TenDV = value; } get { return TenDV; } }
            public double DonGia;
            public double dongia
            { set { DonGia = value; } get { return DonGia; } }
            public bool Chon;
            public bool chon
            { set { Chon = value; } get { return Chon; } }
            public int IdTieuNhom;
            public int idtieunhom
            { set { IdTieuNhom = value; } get { return IdTieuNhom; } }
            public int TrongDM;
            public int trongdm
            { set { TrongDM = value; } get { return TrongDM; } }
        }
        QLBV_Database.QLBVEntities _data;
        List<DVC> _lDVChon1 = new List<DVC>();
        List<DVC> _lDVChon2 = new List<DVC>();
        List<DichVu> _ldv = new List<DichVu>();
        List<QLBV.FormDanhMuc.usDichVu.Dtuong> _lDTBN = new List<QLBV.FormDanhMuc.usDichVu.Dtuong>();
        private void frm_EditGoiDV_Load(object sender, EventArgs e)
        {

            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lDTBN = (from dt in _data.DTBNs select new QLBV.FormDanhMuc.usDichVu.Dtuong { Check = false, IDDTBN = dt.IDDTBN, DTBN = dt.DTBN1 }).OrderBy(p => p.DTBN).ToList();
            cklDTBN.DataSource = _lDTBN;
            var _lTieuNhom = _data.TieuNhomDVs.Where(p => p.IDNhom == 1 || p.IDNhom == 2 || p.IDNhom == 3 || p.IDNhom == 13).ToList();
            _lTieuNhom.Add(new TieuNhomDV { IdTieuNhom = 0, TenRG = "Tất cả", TenTN = "Tất cả" });
            lupTieuNhom.Properties.DataSource = _lTieuNhom.OrderBy(p => p.IdTieuNhom).ToList();
            _ldv = (from dv in _data.DichVus.Where(p => p.PLoai == 2 || p.IDNhom == 1 || p.IDNhom == 2 || p.IDNhom == 3 || p.IDNhom == 13).Where(p => p.Status == 1)
                    join tn in _data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                    select dv).ToList();
            if (_Status == 0)
            {
                foreach (var item in _ldv)
                {
                    DVC moi = new DVC();
                    moi.madv = item.MaDV;
                    moi.tendv = item.TenDV;
                    moi.dongia = item.DonGia;
                    moi.idtieunhom = item.IdTieuNhom ?? 0;
                    moi.trongdm = item.TrongDM ?? 0;
                    moi.chon = false;
                    _lDVChon1.Add(moi);
                }
                this.Text = "Thêm mới gói dịch vụ";
                cboTrangThai.SelectedIndex = 1;
                cboTrongDM.SelectedIndex = 1;
                lupTieuNhom.EditValue = 0;
            }
            else
            {
                this.Text = "Sửa gói dịch vụ";
                string _id = ";" + _ID + ";";
                var _ldvdc = _ldv.Where(p => p.IDGoi != null && p.IDGoi.Contains(_id)).ToList();
                foreach (var item in _ldvdc)
                {
                    DVC moi = new DVC();
                    moi.madv = item.MaDV;
                    moi.tendv = item.TenDV;
                    moi.dongia = item.DonGia;
                    moi.chon = false;
                    _lDVChon2.Add(moi);
                }
                foreach (var item in _ldv)
                {
                    DVC moi1 = new DVC();
                    moi1.madv = item.MaDV;
                    moi1.tendv = item.TenDV;
                    moi1.dongia = item.DonGia;
                    moi1.idtieunhom = item.IdTieuNhom ?? 0;
                    moi1.trongdm = item.TrongDM ?? 0;
                    if (_ldvdc.Where(p => p.MaDV == item.MaDV).Count() > 0)
                        moi1.chon = true;
                    else
                        moi1.chon = false;
                    _lDVChon1.Add(moi1);
                }
                grcDSDV2.DataSource = null;
                grcDSDV2.DataSource = _lDVChon2.ToList();
                DmGoiDV sua = _data.DmGoiDVs.Where(p => p.IDGoi == _ID).FirstOrDefault();
                if (sua != null)
                {
                    txttengoi.Text = sua.TenGoi;
                    cboTrangThai.SelectedIndex = sua.Status;
                    txtDonGia.Text = sua.DonGia.ToString();
                    QLBV.FormDanhMuc.usDichVu._loadDSDTBN(sua.DSDTBN, _lDTBN, cklDTBN);
                }
            }
        }

        void LoadDSDV()
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int _trongdm = 0;
            _trongdm = cboTrongDM.SelectedIndex;
            if (lupTieuNhom.EditValue != null)
            {
                int idtieunhom = Convert.ToInt32(lupTieuNhom.EditValue);
                //var _ldvc = _lDVChon1.Where(p => idtieunhom == 0 ? true : p.IdTieuNhom == idtieunhom).Where(p => _trongdm == 0 ? true : p.TrongDM == 1).ToList();
                grcDSDV1.DataSource = null;
                grcDSDV1.DataSource = _lDVChon1.Where(p => idtieunhom == 0 ? true : p.IdTieuNhom == idtieunhom).Where(p => _trongdm == 0 ? true : p.TrongDM == 1).ToList();
            }
        }
        private void lupTieuNhom_EditValueChanged(object sender, EventArgs e)
        {
            LoadDSDV();
        }

        private void grvDSDV1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (_Status == 0)
            {
                int _madv = grvDSDV1.GetFocusedRowCellValue(colMaDV) == null ? 0 : Convert.ToInt32(grvDSDV1.GetFocusedRowCellValue(colMaDV));
                if (_lDVChon1.Where(p => p.madv == _madv).Count() > 0)
                {
                    DVC sua = _lDVChon1.Where(p => p.madv == _madv).First();
                    if (e.Column == colChon) // Nếu click vào cột Chọn  
                    {
                        if (sua.Chon == false)
                        {
                            sua.Chon = true;
                        }
                        else if (sua.Chon == true)
                        {
                            sua.Chon = false;
                        }
                    }
                }
                _lDVChon2 = _lDVChon1.Where(p => p.Chon == true).ToList();
                grcDSDV2.DataSource = null;
                grcDSDV2.DataSource = _lDVChon2.ToList();
            }
            else
            {
                int _madv = grvDSDV1.GetFocusedRowCellValue(colMaDV) == null ? 0 : Convert.ToInt32(grvDSDV1.GetFocusedRowCellValue(colMaDV));
                if (_lDVChon1.Where(p => p.madv == _madv).Count() > 0)
                {
                    DVC sua = _lDVChon1.Where(p => p.madv == _madv).First();
                    if (e.Column == colChon) // Nếu click vào cột Chọn  
                    {
                        if (sua.Chon == false)
                        {
                            sua.Chon = true;
                        }
                        else if (sua.Chon == true)
                        {
                            sua.Chon = false;
                        }
                    }
                    var kt = _lDVChon2.Where(p => p.madv == _madv).ToList();
                    if (kt.Count > 0)
                    {
                        MessageBox.Show("Dịch vụ đã có trong nhóm");
                        sua.chon = false;
                    }
                    else
                    {
                        _lDVChon2.Add(sua);
                        grcDSDV2.DataSource = "";
                        grcDSDV2.DataSource = _lDVChon2.ToList();
                    }
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            bool ktra = true;
            if (string.IsNullOrEmpty(txttengoi.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên gói dịch vụ");
                txttengoi.Focus();
                ktra = false;
            }
            string _maDTBN = ";";
            for (int i = 0; i < cklDTBN.ItemCount; i++)
            {
                if (cklDTBN.GetItemCheckState(i) == CheckState.Checked)
                    _maDTBN += cklDTBN.GetItemValue(i) + ";";
            }
            if (_maDTBN == ";")
            {
                MessageBox.Show("Bạn chưa chọn đối tượng bệnh nhân sử dụng");
                ktra = false;
            }
            if (_lDVChon2.Count <= 0)
            {
                MessageBox.Show("Gói chưa có dịch vụ");
                ktra = false;
            }
            if (ktra)
            {
                if (_Status == 0)//Lưu mới
                {
                    try
                    {
                        DmGoiDV moi = new DmGoiDV();
                        moi.TenGoi = txttengoi.Text;
                        if (!string.IsNullOrEmpty(txtDonGia.Text))
                            moi.DonGia = Convert.ToDouble(txtDonGia.Text);
                        moi.Status = cboTrangThai.SelectedIndex;
                        moi.TrongDM = cboTrongDM.SelectedIndex;
                        moi.DSDTBN = _maDTBN;
                        _data.DmGoiDVs.Add(moi);
                        int a = _data.SaveChanges();
                        if (a >= 0)
                        {
                            foreach (var item in _lDVChon2)
                            {
                                DichVu sua = _data.DichVus.Where(p => p.MaDV == item.MaDV).FirstOrDefault();
                                if (sua != null)
                                {
                                    if (sua.IDGoi == null)
                                    {
                                        sua.IDGoi = ";" + moi.IDGoi + ";";
                                    }
                                    else
                                    {
                                        sua.IDGoi += moi.IDGoi + ";";
                                    }
                                    _data.SaveChanges();
                                }
                            }
                        }
                        reloaddata();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi Lưu Mới: " + ex.ToString());
                    }
                }
                else
                {
                    try
                    {
                        DmGoiDV sua = _data.DmGoiDVs.Where(p => p.IDGoi == _ID).FirstOrDefault();
                        sua.TenGoi = txttengoi.Text;
                        if (!string.IsNullOrEmpty(txtDonGia.Text))
                            sua.DonGia = Convert.ToDouble(txtDonGia.Text);
                        sua.Status = cboTrangThai.SelectedIndex;
                        sua.TrongDM = cboTrongDM.SelectedIndex;
                        sua.DSDTBN = _maDTBN;
                        int a = _data.SaveChanges();
                        if (a >= 0)
                        {
                            string idsua = ";" + _ID + ";";
                            var dvdaco = _ldv.Where(p => p.IDGoi != null && p.IDGoi.Contains(idsua)).ToList();
                            foreach (var item in _lDVChon2)
                            {
                                if (dvdaco.Where(p => p.MaDV == item.madv).Count() == 0)
                                {
                                    DichVu suadv = _data.DichVus.Where(p => p.MaDV == item.MaDV).FirstOrDefault();
                                    if (suadv != null)
                                    {
                                        if (suadv.IDGoi == null)
                                        {
                                            suadv.IDGoi = ";" + sua.IDGoi + ";";
                                        }
                                        else
                                        {
                                            suadv.IDGoi += sua.IDGoi + ";";
                                        }
                                        _data.SaveChanges();
                                    }
                                }

                            }
                            foreach (var item in dvdaco)
                            {
                                if (_lDVChon2.Where(p => p.MaDV == item.MaDV).Count() == 0)
                                {
                                    DichVu suadv = _data.DichVus.Where(p => p.MaDV == item.MaDV).FirstOrDefault();
                                    suadv.IDGoi = suadv.IDGoi.Replace(idsua, ";");
                                    _data.SaveChanges();
                                }
                            }
                            //foreach (var item in _lDVChon2)
                            //{
                            //    if (dvdaco.Where(p => p.MaDV == item.madv).Count() == 0)
                            //    {
                            //        DichVu suadv = _data.DichVus.Where(p => p.MaDV == item.MaDV).FirstOrDefault();
                            //        suadv.IDGoi = suadv.IDGoi.Replace(idsua, ";");
                            //        _data.SaveChanges();
                            //    }
                            //    else
                            //    {
                            //        DichVu suadv = _data.DichVus.Where(p => p.MaDV == item.MaDV).FirstOrDefault();
                            //        if (suadv != null)
                            //        {
                            //            if (suadv.IDGoi == null)
                            //            {
                            //                suadv.IDGoi = ";" + sua.IDGoi + ";";
                            //            }
                            //            else
                            //            {
                            //                suadv.IDGoi += sua.IDGoi + ";";
                            //            }
                            //            _data.SaveChanges();
                            //        }
                            //    }
                            //}
                        }
                        reloaddata();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi sửa: " + ex.ToString());
                    }
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("Bạn muốn hủy?", "Hỏi thoát", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (Result == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void frm_EditGoiDV_FormClosed(object sender, FormClosedEventArgs e)
        {
            //QLBV.FormDanhMuc.frm_DmGoiDV frm = new QLBV.FormDanhMuc.frm_DmGoiDV();
            //frm.ShowDialog();
        }

        private void cboTrongDM_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDSDV();
        }

        private void grvDSDV2_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colChonlai")
            {
                DialogResult Result = MessageBox.Show("Bạn muốn xóa dịch vụ này khỏi nhóm ?", "Hỏi xóa", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Result == DialogResult.OK)
                {
                    _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    if (grvDSDV2.GetFocusedRowCellValue(colmadv1) != null)
                    {
                        int madv = Convert.ToInt32(grvDSDV2.GetFocusedRowCellValue(colmadv1));
                        DVC a = _lDVChon2.Where(p => p.madv == madv).FirstOrDefault();
                        _lDVChon2.Remove(a);
                        grcDSDV2.DataSource = null;
                        grcDSDV2.DataSource = _lDVChon2.ToList();
                    }
                }
            }
        }
    }
}