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
    public partial class frm_NhapPhanUngThuoc : DevExpress.XtraEditors.XtraForm
    {
        public frm_NhapPhanUngThuoc()
        {
            InitializeComponent();
        }
        int _maBnhan = 0;
        public frm_NhapPhanUngThuoc(int mbn)
        {
            InitializeComponent();
            _maBnhan = mbn;
        }
        QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }
        List<PhanUngT> _lPuT = new List<PhanUngT>();

        private void frm_NhapPhanUngThuoc_Load(object sender, EventArgs e)
        {

            try
            {

                BenhNhan _bn = _dataContext.BenhNhans.Where(p => p.MaBNhan == _maBnhan).Single();
                txt_MaBenhNhan.Text = _bn.MaBNhan.ToString();
                txt_TenBenhNhan.Text = _bn.TenBNhan;

                List<CanBo> _lcb = _dataContext.CanBoes.ToList();
                lup_KP.Properties.DataSource = _dataContext.KPhongs.ToList();
                lup_MaBacSiChiDinh.Properties.DataSource = _lcb;
                lup_TenThuoc.Properties.DataSource = _dataContext.DichVus.ToList();
                lup_NguoiThu.Properties.DataSource = _lcb;
                lup_MaBacSiDoc.Properties.DataSource = _lcb;
                LoadDRC();

            }
            catch (Exception) { }

        }

        void LoadDRC()
        {
            var _ld = (from pu in _dataContext.PhanUngTs.Where(p => p.MaBNhan == _maBnhan)
                       join bn in _dataContext.BenhNhans on pu.MaBNhan equals bn.MaBNhan
                       join tt in _dataContext.DichVus on pu.MaDV equals tt.MaDV
                       join kp in _dataContext.KPhongs on pu.MaKP equals kp.MaKP
                       select new { MaBNhan = pu.MaBNhan, ID_PUT = pu.ID_PUT, TenBNhan = bn.TenBNhan, TimeStart = pu.TimeStart, TimeClose = pu.TimeClose, TenDV = tt.TenDV, TenKP = kp.TenKP, pu.KetQua }).ToList();
            grc_DSPUThuoc.DataSource = _ld;
        }


        private void grv_DSPUThuoc_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(grv_DSPUThuoc.GetFocusedRowCellValue(colID));
                //lab_ID_PUT.Text = id.ToString();
                var _value = (from pu in _dataContext.PhanUngTs.Where(p => p.ID_PUT == id)
                              join bn in _dataContext.BenhNhans on pu.MaBNhan equals bn.MaBNhan
                              select new { pu, bn }).Single();
                dt_BatDau.DateTime = _value.pu.TimeStart;
                txt_MaBenhNhan.Text = _value.pu.MaBNhan.ToString();
                if (_value.pu.TimeClose != null)
                {
                    dt_DocKQ.DateTime = Convert.ToDateTime(_value.pu.TimeClose);
                }
                txt_TenBenhNhan.Text = _value.bn.TenBNhan;
                txt_PhuongPhap.Text = _value.pu.PPThu;
                _maBnhan = _value.bn.MaBNhan;
                lup_KP.EditValue = _value.pu.MaKP;
                lup_TenThuoc.EditValue = _value.pu.MaDV;
                string[] _macb = _value.pu.MaCB.Split(';');
                lup_MaBacSiChiDinh.EditValue = _macb[0];
                lup_NguoiThu.EditValue = _macb[1];
                lup_MaBacSiDoc.EditValue = _macb[2];
                txtKQThu.Text = _value.pu.KetQua;
            }
            catch (Exception) { }
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            int _mbn = Convert.ToInt32(txt_MaBenhNhan.Text.Trim());
            int _mdv = Convert.ToInt32(lup_TenThuoc.EditValue);
            int _mkp = Convert.ToInt32(lup_KP.EditValue);
            var _lv = (from pu in _dataContext.PhanUngTs.Where(p => p.MaBNhan == _mbn && p.MaDV == _mdv) select pu).ToList();
            if (_lv.Count > 0)
            {
                MessageBox.Show("Dữ liệu này đã có.");
            }
            else
            {


                if (lup_KP.EditValue != null && lup_TenThuoc.EditValue != null && dt_BatDau.DateTime != null)
                {
                    PhanUngT _newData = new PhanUngT
                    {
                        MaBNhan = _mbn,
                        TimeStart = dt_BatDau.DateTime,
                        TimeClose = dt_DocKQ.DateTime,
                        MaDV = _mdv,
                        MaKP = _mkp,
                        MaCB = string.Format("{0};{1};{2}", lup_MaBacSiChiDinh.EditValue, lup_NguoiThu.EditValue, lup_MaBacSiDoc.EditValue),
                        PPThu = txt_PhuongPhap.Text.Trim(),
                        KetQua = txtKQThu.Text.Trim()
                    };
                    _dataContext.PhanUngTs.Add(_newData);
                    _dataContext.SaveChanges();
                    LoadDRC();
                }
                else
                {
                    MessageBox.Show("Nhập đầy đủ thông tin yêu cầu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            //grv_DSPUThuoc.DeleteRow(grv_DSPUThuoc.FocusedRowHandle);\
            if (MessageBox.Show("Bạn có muốn xóa dữ liệu này không?", "Xác nhận thông tin xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                if (grv_DSPUThuoc.RowCount > 0)
                {
                    int _id = Convert.ToInt32(grv_DSPUThuoc.GetFocusedRowCellValue(colID));
                    _dataContext.PhanUngTs.Remove(_dataContext.PhanUngTs.Where(p => p.ID_PUT == _id).Single());
                    _dataContext.SaveChanges();
                    LoadDRC();
                }
            }


        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            try
            {
                int _id = Convert.ToInt32(grv_DSPUThuoc.GetFocusedRowCellValue(colID));
                PhanUngT _data = _dataContext.PhanUngTs.Where(p => p.ID_PUT == _id).FirstOrDefault();
                if (_data != null)
                {
                    _data.MaDV = Convert.ToInt32(lup_TenThuoc.EditValue);
                    _data.MaKP = Convert.ToInt32(lup_KP.EditValue);
                    _data.MaCB = string.Format("{0};{1};{2}", lup_MaBacSiChiDinh.EditValue, lup_NguoiThu.EditValue, lup_MaBacSiDoc.EditValue);
                    _data.PPThu = txt_PhuongPhap.Text;
                    _data.TimeStart = dt_BatDau.DateTime;
                    _data.TimeClose = dt_DocKQ.DateTime;
                    _data.KetQua = txtKQThu.Text.Trim();
                    _dataContext.SaveChanges();
                    LoadDRC();
                }

            }
            catch (Exception)
            {


            }
        }
        string ChuyenDoiTen(string _value, int _type)
        {
            string _kq = string.Empty;
            try
            {
                string[] _args = _value.Split(';');
                string _cb = _args[0];
                var _lcb = from cb in _dataContext.CanBoes select cb;
                switch (_type)
                {
                    case 1: _cb = _args[0]; _kq = _lcb.Where(d => d.MaCB == _cb).Single().TenCB;
                        break;
                    case 2: _cb = _args[1]; _kq = _lcb.Where(d => d.MaCB == _cb).Single().TenCB;
                        break;
                    case 3: _cb = _args[2]; _kq = _lcb.Where(d => d.MaCB == _cb).Single().TenCB;
                        break;
                }

                if (DungChung.Bien.MaBV == "30007")
                    _kq = Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + _kq;
            }

            catch (Exception) { }
            return _kq;
        }
        private void btn_IN_Click(object sender, EventArgs e)
        {
            try
            {
                BaoCao.Rep_BCThuPhanUngThuoc _rep = new BaoCao.Rep_BCThuPhanUngThuoc();
                int mabn = Convert.ToInt32(txt_MaBenhNhan.Text);
                var _lD = (from pu in _dataContext.PhanUngTs.Where(d => d.MaBNhan == mabn)
                           join tt in _dataContext.DichVus on pu.MaDV equals tt.MaDV
                           join bn in _dataContext.BenhNhans on pu.MaBNhan equals bn.MaBNhan
                           join kp in _dataContext.KPhongs on pu.MaKP equals kp.MaKP
                           join dv in _dataContext.DichVus on pu.MaDV equals dv.MaDV
                           select new { pu, tt, bn, kp, dv }).AsEnumerable().Select(x => new { x.pu.MaKP, x.pu.MaCB, TenBNhan = x.bn.TenBNhan, Tuoi = x.bn.Tuoi, GioiTinh = x.bn.GTinh, DiaChi = x.bn.DChi, Khoa = x.kp.TenKP, TenThuoc = String.Format("{0}, {1}, {2}, {3}", x.dv.TenDV, x.dv.NuocSX, x.dv.HamLuong, x.dv.DonVi), BatDau = x.pu.TimeStart, KetThuc = x.pu.TimeClose, PhuongPhap = x.pu.PPThu, BacSiChiDinh = ChuyenDoiTen(x.pu.MaCB, 1), NguoiThu = ChuyenDoiTen(x.pu.MaCB, 2), BacSiDoc = ChuyenDoiTen(x.pu.MaCB, 3), x.pu.KetQua }).ToList();
                if (_lD.Count > 0)
                {
                    var _sovaovien = _dataContext.VaoViens.Where(p => p.MaBNhan == mabn).ToList();
                    if (_sovaovien.Count > 0)
                    {
                        _rep.Sovaovien.Value = _sovaovien.FirstOrDefault().SoVV;
                    }
                    _rep.tenBenhNhan.Value = _lD.FirstOrDefault().TenBNhan;
                    _rep.Tuoi.Value = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.TinhTuoiBenhNhan(_dataContext, mabn) : (DungChung.Bien.MaBV == "24012" ? DungChung.Ham.TuoitheoThang(_dataContext, mabn, DungChung.Bien.formatAge_24012) : _lD.FirstOrDefault().Tuoi.ToString());
                    _rep.GioiTinh.Value = _lD.FirstOrDefault().GioiTinh;
                    _rep.DiaChi.Value = _lD.FirstOrDefault().DiaChi;
                    _rep.Khoa.Value = _lD.FirstOrDefault().Khoa;
                    int _makp = _lD.FirstOrDefault().MaKP;
                    var bnkb = _dataContext.BNKBs.Where(p => p.MaBNhan == mabn && p.MaKP == _makp).ToList();
                    if (bnkb.Count > 0)
                    {
                        _rep.Sobuong.Value = bnkb.FirstOrDefault().Buong;
                        _rep.SoGiuong.Value = bnkb.FirstOrDefault().Giuong;
                        _rep.ChanDoan.Value = bnkb.FirstOrDefault().ChanDoan;
                    }
                    _rep.DataSource = _lD;
                    _rep.BindingData();
                    frmIn _frm = new frmIn();
                    _rep.CreateDocument();
                    _frm.prcIN.PrintingSystem = _rep.PrintingSystem;
                    _frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Bệnh nhân này chưa có dữ liệu thử phản ứng thuốc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception) { }
        }
    }
}