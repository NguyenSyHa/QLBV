﻿using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormThamSo
{
    public partial class frm_giaUT : DevExpress.XtraEditors.XtraForm
    {
        public frm_giaUT()
        {
            InitializeComponent();
        }

        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        int maDV = 0;
        int maKP = 0;
        double donGia = 0;
        public static int status = 0;

        private void frm_giaUT_Load(object sender, EventArgs e)
        {
            var D = (from tk in data.KPhongs.Where(p => p.PLoai == ("Khoa dược")) select new { tk.TenKP, tk.MaKP }).ToList();
            lupKho.Properties.DataSource = D.ToList();
            DSThuocUT();
            //gán max status
            var maxStatus = data.GiaUTs.OrderByDescending(p => p.Status).FirstOrDefault();
            status = Convert.ToInt32(maxStatus.Status);
        }

        private void lupKho_EditValueChanged(object sender, EventArgs e)
        {
            DSDuoc();
            DSThuocUT();
        }

        #region DSDuoc
        private void DSDuoc()
        {
            List<DichVu> _dvu = new List<DichVu>();
            int makp = 0;
            string tendv = "";
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
                tendv = txtTimKiem.Text;
            if (lupKho.EditValue != null)
                makp = Convert.ToInt32(lupKho.EditValue);
            if (makp > 0)
            {
                _dvu = (from dv in data.DichVus.Where(p => p.PLoai == 1).Where(p => p.TenDV.Contains(tendv))
                        where (from nd in data.NhapDs.Where(p => p.MaKP == makp) join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap select ndct.MaDV).Contains(dv.MaDV)
                        select dv).ToList();
            }
            else
            {
                _dvu = null;
            }
            grvTenDV.DataSource = _dvu;
        }
        #endregion

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            DSDuoc();
        }

        List<GiaUT> _giaUT = new List<GiaUT>();
        #region taogrv
        private void taogrv()
        {
            GiaUT themmoi = new GiaUT();
            List<string> _donGia = new List<string>();
            if (grvDmDuoc.GetFocusedRowCellValue(colMaDV) != null)
            {

                if (!string.IsNullOrEmpty(lupKho.Text))
                {
                    int _MaDV = 0;
                    string _TenDV = string.Empty;
                    if (grvDmDuoc.GetFocusedRowCellValue(colMaDV) != null)
                    {
                        int makp = 0;
                        if (lupKho.EditValue != null)
                            makp = Convert.ToInt32(lupKho.EditValue);
                        lblTenThuoc.Text = grvDmDuoc.GetFocusedRowCellValue(colTenDV).ToString();
                        _TenDV = grvDmDuoc.GetFocusedRowCellValue(colTenDV).ToString();
                        _MaDV = Convert.ToInt32(grvDmDuoc.GetFocusedRowCellValue(colMaDV));
                        //txtMadv.Text = _MaDV.ToString();
                        //_Madv1 = txtMadv.Text == "" ? 0 : Convert.ToInt32(txtMadv.Text);
                        //_Tendv1 = grvTendv.GetFocusedRowCellValue(colTendv).ToString();
                        var b = (from dv in data.DichVus
                                 join nxct in data.NhapDcts on dv.MaDV equals nxct.MaDV
                                 join nd in data.NhapDs.Where(p => p.MaKP == (makp)) on nxct.IDNhap equals nd.IDNhap
                                 where (nxct.MaDV == _MaDV)
                                 group new { nxct, dv } by new { dv.DSDonGia, dv.TenDV, dv.MaDV } into kq
                                 select new
                                 {
                                     SLuongN = kq.Sum(p => p.nxct.SoLuongN),
                                     SLuongX = kq.Sum(p => p.nxct.SoLuongX),
                                     SLT = ((kq.Sum(p => p.nxct.SoLuongN) - (kq.Sum(p => p.nxct.SoLuongX)))),
                                     kq.Key.DSDonGia,
                                     kq.Key.MaDV
                                 }).ToList();
                        foreach (var item in b)
                        {
                            //if (item.SLT > 0)
                            //{
                            if (!string.IsNullOrEmpty(item.DSDonGia))
                            {
                                _donGia = item.DSDonGia.Split(';').ToList();
                                foreach (var itemDG in _donGia)
                                {
                                    LuuGiaUT(item.MaDV, itemDG);
                                }
                            }
                            //}
                            //else
                            //{
                            //    MessageBox.Show("Không thể thêm thuốc khi số lượng tồn đã hết.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //}
                        }
                        var _lgia = (from g in data.GiaUTs.Where(p => p.MaKP == makp).Where(p => p.MaDV == _MaDV).Where(p => p.Status == -2)
                                     join dv in data.DichVus on g.MaDV equals dv.MaDV
                                     join ndct in data.NhapDcts.Where(p => p.MaDV == _MaDV) on g.MaDV equals ndct.MaDV
                                     join nd in data.NhapDs.Where(p => p.MaKP == makp) on ndct.IDNhap equals nd.IDNhap
                                     group new { g, dv, ndct, nd } by new { g.MaDV, g.MaKP, g.DonGia } into kq
                                     select new
                                     {
                                         kq.Key.DonGia,
                                         SoLuongTon = kq.Sum(p => p.ndct.SoLuongN) - kq.Sum(p => p.ndct.SoLuongX)
                                     }).ToList();
                        grcChonDG.DataSource = _lgia.ToList();
                    }
                    else
                        grcChonDG.DataSource = null;
                }
                else
                    grcChonDG.DataSource = null;

            }
        }
        #endregion
        #region LuuGiaUT
        private void LuuGiaUT(int madv, string dongia)
        {
            int tt = 0;
            if (dongia != "")
            {
                int _makp = 0;
                if (lupKho.EditValue != null)
                    _makp = Convert.ToInt32(lupKho.EditValue);
                double _dg = Convert.ToDouble(dongia);
                var kt = data.GiaUTs.Where(p => p.MaDV == madv).Where(p => p.MaKP == _makp).Where(p => p.DonGia == _dg).ToList();
                if (kt.Count > 0)
                {
                    DialogResult _result = MessageBox.Show("Thuốc đã đặt đơn giá ưu tiên. Bạn có muốn thay đổi?", "Đổi giá", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_result == DialogResult.Yes)
                    {
                        foreach (var a in kt)
                        {
                            int id = a.IdGiaUT;
                            var xoa = data.GiaUTs.Single(p => p.IdGiaUT == (id));
                            data.GiaUTs.Remove(xoa);
                            data.SaveChanges();
                        }
                        tt = 0;

                    }
                    else
                    {
                        tt = -1;
                    }
                }
                if (tt == 0)
                {
                    //DialogResult _result = MessageBox.Show("Bạn có chắc chắn muốn thêm thuốc này vào danh mục giá ưu tiên?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //if (_result == DialogResult.Yes)
                    //{
                    GiaUT themmoi = new GiaUT();
                    int makho = 0;
                    if (lupKho.EditValue != null)
                        makho = Convert.ToInt32(lupKho.EditValue);
                    if (!string.IsNullOrEmpty(dongia))
                        themmoi.DonGia = Convert.ToDouble(dongia);
                    themmoi.MaKP = makho;
                    themmoi.MaDV = madv;
                    themmoi.Status = -2;

                    _giaUT.Add(themmoi);
                    data.GiaUTs.Add(themmoi);
                    data.SaveChanges();
                    //}
                    //timkiemgia();
                }
            }
        }
        #endregion

        private void grvDmDuoc_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            taogrv();
            DSThuocUT();
        }

        private void grvChonDG_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colChonUT")
            {
                double _dgia = Convert.ToInt32(grvChonDG.GetFocusedRowCellValue(colDonGia).ToString());
                int _madv = Convert.ToInt32(grvDmDuoc.GetFocusedRowCellValue(colMaDV).ToString());
                int _makp = 0;
                if (lupKho.EditValue != null)
                    _makp = Convert.ToInt32(lupKho.EditValue);

                var kt = (from dv in data.DichVus
                          join ndct in data.NhapDcts.Where(p => p.MaDV == _madv) on dv.MaDV equals ndct.MaDV
                          join nd in data.NhapDs.Where(p => p.MaKP == _makp) on ndct.IDNhap equals nd.IDNhap
                          group new { dv, ndct, nd } by new { dv.MaDV, nd.MaKP } into kq
                          select new
                          {
                              SoLuongTon = kq.Sum(p => p.ndct.SoLuongN) - kq.Sum(p => p.ndct.SoLuongX)
                          }).FirstOrDefault();
                if (kt.SoLuongTon == 0)
                {
                    DialogResult _result = MessageBox.Show("Số lượng tồn đã hết. Bạn có chắc muốn thêm vào giá ưu tiên?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_result == DialogResult.Yes)
                    {
                        var suaGiaUT = data.GiaUTs.Where(p => p.MaKP == _makp && p.MaDV == _madv && p.DonGia == _dgia).FirstOrDefault();
                        suaGiaUT.Status = -1;
                        data.SaveChanges();
                        DSThuocUT();
                        //gán max status
                        var maxStatus = data.GiaUTs.OrderByDescending(p => p.Status).FirstOrDefault();
                        status = Convert.ToInt32(maxStatus.Status);
                    }
                }
                else
                {
                    var suaGiaUT = data.GiaUTs.Where(p => p.MaKP == _makp && p.MaDV == _madv && p.DonGia == _dgia).FirstOrDefault();
                    suaGiaUT.Status = -1;
                    data.SaveChanges();
                    DSThuocUT();
                    //gán max status
                    var maxStatus = data.GiaUTs.OrderByDescending(p => p.Status).FirstOrDefault();
                    status = Convert.ToInt32(maxStatus.Status);
                }
            }
        }

        #region Load danh sách thuốc ưu tiên
        public void DSThuocUT()
        {
            int makp = 0;
            if (lupKho.EditValue != null)
                makp = Convert.ToInt32(lupKho.EditValue);

            var _thuoc = (from g in data.GiaUTs.Where(p => p.MaKP == makp).Where(p => p.Status != -2)
                          join dv in data.DichVus on g.MaDV equals dv.MaDV
                          join ndct in data.NhapDcts on g.MaDV equals ndct.MaDV
                          join nd in data.NhapDs.Where(p => p.MaKP == makp) on ndct.IDNhap equals nd.IDNhap
                          join kp in data.KPhongs on g.MaKP equals kp.MaKP
                          group new { g, dv, ndct, nd } by new { g.MaDV, g.MaKP, g.DonGia, kp.TenKP, dv.TenDV, dv.DonVi, g.Status } into kq
                          select new
                          {
                              kq.Key.MaDV,
                              kq.Key.TenKP,
                              kq.Key.TenDV,
                              kq.Key.DonVi,
                              kq.Key.DonGia,
                              kq.Key.Status,
                              SoLuongTon = kq.Sum(p => p.ndct.SoLuongN) - kq.Sum(p => p.ndct.SoLuongX)
                          }).OrderByDescending(p => p.Status).ToList();
            grcGiaUT.DataSource = _thuoc;
        }
        #endregion

        private void grvGiaUT_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            double _dgia = Convert.ToInt32(grvGiaUT.GetFocusedRowCellValue(colDonGia).ToString());
            int _madv = Convert.ToInt32(grvGiaUT.GetFocusedRowCellValue(colMaDV).ToString());
            int _makp = 0;
            if (lupKho.EditValue != null)
                _makp = Convert.ToInt32(lupKho.EditValue);
            if (e.Column.Name == "colUuTien")
            {
                var suaGiaUT = data.GiaUTs.Where(p => p.MaKP == _makp && p.MaDV == _madv && p.DonGia == _dgia).FirstOrDefault();
                suaGiaUT.Status = status + 1;

                //lưu tạm status
                status = Convert.ToInt32(suaGiaUT.Status);

                data.SaveChanges();

                DSThuocUT();
            }
            if (e.Column.Name == "colBoChon")
            {
                var suaGiaUT = data.GiaUTs.Where(p => p.MaKP == _makp && p.MaDV == _madv && p.DonGia == _dgia).FirstOrDefault();
                suaGiaUT.Status = 0;

                data.SaveChanges();

                DSThuocUT();
            }
        }

        private void txtTKUuTien_EditValueChanged(object sender, EventArgs e)
        {
            int makp = 0;
            if (lupKho.EditValue != null)
                makp = Convert.ToInt32(lupKho.EditValue);
            string _thuocUT = "";
            if (!string.IsNullOrEmpty(txtTKUuTien.Text))
                _thuocUT = txtTKUuTien.Text;
            var _thuoc = (from g in data.GiaUTs.Where(p => p.MaKP == makp).Where(p => p.Status != -2)
                          join dv in data.DichVus.Where(p => p.TenDV.Contains(_thuocUT)) on g.MaDV equals dv.MaDV
                          join ndct in data.NhapDcts on g.MaDV equals ndct.MaDV
                          join nd in data.NhapDs on ndct.IDNhap equals nd.IDNhap
                          join kp in data.KPhongs on g.MaKP equals kp.MaKP
                          group new { g, dv, ndct, nd } by new { g.MaDV, g.MaKP, g.DonGia, kp.TenKP, dv.TenDV, dv.DonVi, g.Status } into kq
                          select new
                          {
                              kq.Key.MaDV,
                              kq.Key.TenKP,
                              kq.Key.TenDV,
                              kq.Key.DonVi,
                              kq.Key.DonGia,
                              kq.Key.Status,
                              SoLuongTon = kq.Sum(p => p.ndct.SoLuongN) - kq.Sum(p => p.ndct.SoLuongX)
                          }).OrderByDescending(p => p.Status).ToList();
            grcGiaUT.DataSource = _thuoc;
        }
    }
}