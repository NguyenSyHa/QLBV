using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
namespace QLBV.FormThamSo
{
    public partial class frm_BCHD_CLS : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCHD_CLS()
        {
            InitializeComponent();
        }

        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void grvKhoaPhong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colCheckGrvKP")
            {
                if (e.RowHandle == 0)
                {
                    if (grvKhoaPhong.GetFocusedRowCellValue(colCheckGrvKP) != null)
                    {
                        if (grvKhoaPhong.GetRowCellValue(0, colCheckGrvKP).ToString() == "False")
                        {
                            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
                            {
                                grvKhoaPhong.SetRowCellValue(i, "Check", true);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
                            {
                                grvKhoaPhong.SetRowCellValue(i, "Check", false);
                            }
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < grvKhoaPhong.RowCount; i++)
                    {
                        if (grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP) != null && grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP).ToString() == "True")
                        {

                        }
                        else
                        {
                            grvKhoaPhong.SetRowCellValue(0, colCheckGrvKP, false);
                            break;
                        }
                    }

                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            
            List<KetQua> _LKQ = new List<KetQua>();
            DateTime ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime ngayden = DungChung.Ham.NgayDen(lupngayden.DateTime);
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                ngaytu = lupNgaytu1.DateTime;
                ngayden = lupngayden1.DateTime;
            }
            string _dtuong = "";
            int _noitru = -1;
            _noitru = radNoiTru.SelectedIndex;
            _dtuong = cboDTuong.Text;
            List<int> _intKhoa = new List<int>();
          
            List<KhoaPhong> _lKP = new List<KhoaPhong>();
            List<KetQua> q_NgayTH = new List<KetQua>();
            List<KetQua> q_KQRiengKhoa = new List<KetQua>();// kết quả làm tại khoa

            if (ckHienThiXNKhoa.Checked)
            {
                for (int i = 0; i < cklKP.ItemCount; i++)
                {
                    if (cklKP.GetItemCheckState(i) == CheckState.Checked)
                        _intKhoa.Add(Convert.ToInt32(cklKP.GetItemValue(i)));
                }
            }
            // Lấy danh sách khoa phòng
            for (int i = 1; i < grvKhoaPhong.RowCount; i++)
            {
                if (grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP) != null && grvKhoaPhong.GetRowCellValue(i, colCheckGrvKP).ToString() == "True")
                {
                    KhoaPhong kp = new KhoaPhong();
                    kp._check = true;
                    kp._maKP = grvKhoaPhong.GetRowCellValue(i, colmaKP) == null ? 0 : Convert.ToInt32(grvKhoaPhong.GetRowCellValue(i, colmaKP));
                    if (grvKhoaPhong.GetRowCellValue(i, colTenKP) != null)
                        kp.TenKP = grvKhoaPhong.GetRowCellValue(i, colTenKP).ToString();
                    _lKP.Add(kp);
                }
            }
            if (_lKP.Count == 1)
            {
               // rep.paramKP.Value = _lKP.First().TenKP.ToUpper();
            }
            else
            {
                _lKP.Add(new KhoaPhong { MaKP = 0, TenKP = "" });
            }
            var q_DV = (from dv in data.DichVus
                        join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                        join ndv in data.NhomDVs.Where(p => p.TenNhomCT == "Chẩn đoán hình ảnh" || p.TenNhomCT == "Xét nghiệm" || p.TenNhomCT == "Thăm dò chức năng") on tn.IDNhom equals ndv.IDNhom
                        select new { dv.MaDV, tn.IdTieuNhom, tn.TenTN, tn.TenRG, ndv.IDNhom, ndv.TenNhom, ndv.TenNhomCT }).ToList();
            var q5 = (from vp in data.VienPhis.Where(p => p.NgayTT >= ngaytu && p.NgayTT <= ngayden)
                      join bn in data.BenhNhans.Where(p => cboDTuong.SelectedIndex == 2 || p.DTuong == _dtuong).Where(p => _noitru == 2 || p.NoiTru == _noitru) on vp.MaBNhan equals bn.MaBNhan
                      select new { vp.MaBNhan }).Distinct().ToList();
            #region tìm theo ngày thực hiện
            if (cbo_NgayTT.SelectedIndex == 0)
            {
                var q_CLS = (from cls in data.CLS.Where(p => p.NgayTH >= ngaytu && p.NgayTH <= ngayden)
                             join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                             select new { cls.MaBNhan,cls.MaKP, cd.MaDV,cd.TrongBH,cd.IDCD,cls.MaKPth }).ToList();

                List<KetQua> q_NgayTH0 = (from cls in q_CLS
                                          join dv in q_DV on cls.MaDV equals dv.MaDV
                                          join bn in data.BenhNhans.Where(p => cboDTuong.SelectedIndex == 2 || p.DTuong == _dtuong).Where(p => _noitru == 2 || p.NoiTru == _noitru) on cls.MaBNhan equals bn.MaBNhan
                                          where cboTrongDM.SelectedIndex == 2 ? true : cls.TrongBH == cboTrongDM.SelectedIndex
                                          group new { dv, bn } by new { cls.MaKP, dv.IdTieuNhom, dv.TenTN, dv.TenRG, dv.TenNhom, dv.TenNhomCT, bn.MaBNhan } into kq
                                          select new KetQua
                                          {
                                              MaKP = kq.Key.MaKP == null ? 0 : kq.Key.MaKP.Value,
                                              IdTieuNhom = kq.Key.IdTieuNhom,
                                              TenTN = kq.Key.TenTN,
                                              TenRG = kq.Key.TenRG,
                                              TenNhom = kq.Key.TenNhom,
                                              TenNhomCT = kq.Key.TenNhomCT,
                                              TongXN = 1
                                          }).OrderBy(p => p.TenNhom).ThenBy(p => p.TenTN).ToList();
                if (ckc_theoluot.Checked)
                {
                    List<KetQua> q_theoluot = (from cls in q_CLS
                                               join dv in q_DV on cls.MaDV equals dv.MaDV
                                               join bn in data.BenhNhans.Where(p => cboDTuong.SelectedIndex == 2 || p.DTuong == _dtuong).Where(p => _noitru == 2 || p.NoiTru == _noitru) on cls.MaBNhan equals bn.MaBNhan
                                               where cboTrongDM.SelectedIndex == 2 ? true : cls.TrongBH == cboTrongDM.SelectedIndex
                                               group new { dv, cls } by new { cls.MaKP, dv.IdTieuNhom, dv.TenTN, dv.TenRG, dv.TenNhom, dv.TenNhomCT, cls.IDCD } into kq
                                               select new KetQua
                                               {
                                                   MaKP = kq.Key.MaKP == null ? 0 : kq.Key.MaKP.Value,
                                                   IdTieuNhom = kq.Key.IdTieuNhom,
                                                   TenTN = kq.Key.TenTN,
                                                   TenRG = kq.Key.TenRG,
                                                   TenNhom = kq.Key.TenNhom,
                                                   TenNhomCT = kq.Key.TenNhomCT,
                                                   TongL = 1
                                               }).ToList();
                    q_theoluot.AddRange(q_NgayTH0);

                    q_NgayTH = (from bn in q_theoluot
                                group bn by new { bn.MaKP, bn.IdTieuNhom, bn.TenTN, bn.TenRG, bn.TenNhom, bn.TenNhomCT } into kq
                                select new KetQua
                                {
                                    MaKP = kq.Key.MaKP,
                                    IdTieuNhom = kq.Key.IdTieuNhom,
                                    TenTN = kq.Key.TenTN,
                                    TenRG = kq.Key.TenRG,
                                    TenNhom = kq.Key.TenNhom,
                                    TenNhomCT = kq.Key.TenNhomCT,
                                    TongXN = kq.Sum(p => p.TongXN),
                                    TongL = kq.Sum(p => p.TongL)
                                }).OrderBy(p => p.TenNhom).ThenBy(p => p.TenTN).ToList();
                }
                else
                {
                    q_NgayTH = (from bn in q_NgayTH0
                                group bn by new { bn.MaKP, bn.IdTieuNhom, bn.TenTN, bn.TenRG, bn.TenNhom, bn.TenNhomCT } into kq
                                select new KetQua
                                {
                                    MaKP = kq.Key.MaKP,
                                    IdTieuNhom = kq.Key.IdTieuNhom,
                                    TenTN = kq.Key.TenTN,
                                    TenRG = kq.Key.TenRG,
                                    TenNhom = kq.Key.TenNhom,
                                    TenNhomCT = kq.Key.TenNhomCT,
                                    TongXN = kq.Sum(p => p.TongXN),
                                    //TongL = kq.Sum(p => p.TongL)
                                }).OrderBy(p => p.TenNhom).ThenBy(p => p.TenTN).ToList();
                }
                if (_intKhoa.Count >0)
                {
                    var q_KQRiengKhoa0 = (from cls in q_CLS.Where(p => _intKhoa.Contains(p.MaKPth ?? 0))
                                     join bn in data.BenhNhans.Where(p => cboDTuong.SelectedIndex == 2 || p.DTuong == _dtuong).Where(p => _noitru == 2 || p.NoiTru == _noitru) on cls.MaBNhan equals bn.MaBNhan
                                     join dv in q_DV on cls.MaDV equals dv.MaDV
                                     where cboTrongDM.SelectedIndex == 2 ? true : cls.TrongBH == cboTrongDM.SelectedIndex
                                          group new { dv, bn } by new { dv.IdTieuNhom, dv.TenTN, dv.TenRG, dv.TenNhom, dv.TenNhomCT, bn.MaBNhan } into kq
                                     select new KetQua
                                     {
                                         IdTieuNhom = kq.Key.IdTieuNhom,
                                         TenTN = kq.Key.TenTN,
                                         TenRG = kq.Key.TenRG,
                                         TenNhom = kq.Key.TenNhom,
                                         TenNhomCT = kq.Key.TenNhomCT,
                                         TongXN = 1,
                                     }).OrderBy(p => p.TenNhom).ThenBy(p => p.TenTN).ToList();
                    
                    q_KQRiengKhoa = (from bn in q_KQRiengKhoa0
                                     group bn by new { bn.IdTieuNhom, bn.TenTN, bn.TenRG, bn.TenNhom, bn.TenNhomCT } into kq
                                     select new KetQua
                                     {
                                        // MaKP = _maKP,
                                         IdTieuNhom = kq.Key.IdTieuNhom,
                                         TenTN = kq.Key.TenTN,
                                         TenRG = kq.Key.TenRG,
                                         TenNhom = kq.Key.TenNhom,
                                         TenNhomCT = kq.Key.TenNhomCT,
                                         TongXN = kq.Count()
                                     }).OrderBy(p => p.TenNhom).ThenBy(p => p.TenTN).ToList();
                }

            }
            #endregion
            #region tìm theo ngày thanh toán
            else
            {
               
                //var q_DV = (from dv in data.DichVus
                //            join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                //            join ndv in data.NhomDVs.Where(p => p.TenNhomCT == "Chẩn đoán hình ảnh" || p.TenNhomCT == "Xét nghiệm") on tn.IDNhom equals ndv.IDNhom
                //            select new { dv.MaDV, tn.IdTieuNhom, tn.TenTN, tn.TenRG, ndv.IDNhom, ndv.TenNhom, ndv.TenNhomCT }).ToList();
                DateTime ngaytunew = ngaytu.AddDays(-10);
                DateTime ngaydennew = ngayden.AddDays(10);
                var q_cls = (from cls in data.CLS.Where(p => p.NgayTH >= ngaytunew && p.NgayTH <= ngaydennew)
                             join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                             select new { cls.MaBNhan, cls.MaKP, cd.MaDV, cd.TrongBH, cd.IDCD, cls.MaKPth }).ToList();

             
                List<KetQua> q_NgayTH0 = (from cls in q_cls
                                 join dv in q_DV on cls.MaDV equals dv.MaDV
                                 join vp in q5 on cls.MaBNhan equals vp.MaBNhan
                            //     join vp in data.VienPhis.Where(p => p.NgayTT >= ngaytu && p.NgayTT <= ngayden) on cls.MaBNhan equals vp.MaBNhan
                            //join bn in data.BenhNhans.Where(p => cboDTuong.SelectedIndex == 2 || p.DTuong == _dtuong).Where(p => _noitru == 2 || p.NoiTru == _noitru) on vp.MaBNhan equals bn.MaBNhan
                            // select new {tn.IDNhom,tn.IdTieuNhom,tn.TenRG,tn.TenTN,ndv.TenNhom,ndv.TenNhomCT}
                                 where cboTrongDM.SelectedIndex == 2 ? true : cls.TrongBH == cboTrongDM.SelectedIndex
                                 group new { dv, cls } by new { cls.MaKP, dv.IdTieuNhom, dv.TenTN, dv.TenRG, dv.TenNhom, dv.TenNhomCT,cls.MaBNhan} into kq
                            select new KetQua
                            {
                                MaKP = kq.Key.MaKP == null ? 0 : kq.Key.MaKP.Value,
                                IdTieuNhom = kq.Key.IdTieuNhom,
                                TenTN = kq.Key.TenTN,
                                TenRG = kq.Key.TenRG,
                                TenNhom = kq.Key.TenNhom,
                                TenNhomCT = kq.Key.TenNhomCT,
                                TongXN = 1
                            }).OrderBy(p => p.TenNhom).ThenBy(p => p.TenTN).ToList();
                if (ckc_theoluot.Checked)
                {
                    List<KetQua> q_theoluot = (from cls in q_cls
                                      join dv in q_DV on cls.MaDV equals dv.MaDV
                                      join vp in q5 on cls.MaBNhan equals vp.MaBNhan
                            //          join vp in data.VienPhis.Where(p => p.NgayTT >= ngaytu && p.NgayTT <= ngayden) on cls.MaBNhan equals vp.MaBNhan
                            //join bn in data.BenhNhans.Where(p => cboDTuong.SelectedIndex == 2 || p.DTuong == _dtuong).Where(p => _noitru == 2 || p.NoiTru == _noitru) on vp.MaBNhan equals bn.MaBNhan
                                      // select new {tn.IDNhom,tn.IdTieuNhom,tn.TenRG,tn.TenTN,ndv.TenNhom,ndv.TenNhomCT}
                                      where cboTrongDM.SelectedIndex == 2 ? true : cls.TrongBH == cboTrongDM.SelectedIndex
                                      group new { dv, cls } by new { cls.MaKP, dv.IdTieuNhom, dv.TenTN, dv.TenRG, dv.TenNhom, dv.TenNhomCT, cls.IDCD } into kq    
                                      select new KetQua
                                      {
                                          MaKP = kq.Key.MaKP == null ? 0 : kq.Key.MaKP.Value,
                                          IdTieuNhom = kq.Key.IdTieuNhom,
                                          TenTN = kq.Key.TenTN,
                                          TenRG = kq.Key.TenRG,
                                          TenNhom = kq.Key.TenNhom,
                                          TenNhomCT = kq.Key.TenNhomCT,
                                          TongL = 1
                                      }).OrderBy(p => p.TenNhom).ThenBy(p => p.TenTN).ToList();
                    q_theoluot.AddRange(q_NgayTH0);
                    q_NgayTH = (from bn in q_theoluot
                                group bn by new { bn.MaKP, bn.IdTieuNhom, bn.TenTN, bn.TenRG, bn.TenNhom, bn.TenNhomCT } into kq
                                select new KetQua
                                {
                                    MaKP = kq.Key.MaKP,
                                    IdTieuNhom = kq.Key.IdTieuNhom,
                                    TenTN = kq.Key.TenTN,
                                    TenRG = kq.Key.TenRG,
                                    TenNhom = kq.Key.TenNhom,
                                    TenNhomCT = kq.Key.TenNhomCT,
                                    TongXN = kq.Sum(p => p.TongXN),
                                    TongL = kq.Sum(p => p.TongL)
                                }).OrderBy(p => p.TenNhom).ThenBy(p => p.TenTN).ToList();
                }
                else
                {
                    q_NgayTH = (from bn in q_NgayTH0
                                group bn by new { bn.MaKP, bn.IdTieuNhom, bn.TenTN, bn.TenRG, bn.TenNhom, bn.TenNhomCT } into kq
                                select new KetQua
                                {
                                    MaKP = kq.Key.MaKP,
                                    IdTieuNhom = kq.Key.IdTieuNhom,
                                    TenTN = kq.Key.TenTN,
                                    TenRG = kq.Key.TenRG,
                                    TenNhom = kq.Key.TenNhom,
                                    TenNhomCT = kq.Key.TenNhomCT,
                                    TongXN = kq.Count()
                                }).OrderBy(p => p.TenNhom).ThenBy(p => p.TenTN).ToList();
                }
                if (_intKhoa.Count>0)
                {
                    //DateTime ngaytunew = ngaytu.AddDays(-10);
                    //DateTime ngaydennew = ngayden.AddDays(10);
                    //var q8 = (from cls in data.CLS.Where(p => p.NgayTH >= ngaytunew && p.NgayTH <= ngaydennew)
                    //          join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                    //          select new { cls.MaBNhan, cls.MaKP, cd.MaDV, cd.TrongBH, cd.IDCD, cls.MaKPth }).ToList();
                    var q1 = (from vp in q5
                              join cls in q_cls on vp.MaBNhan equals cls.MaBNhan
                              select new { vp.MaBNhan, cls.MaDV, cls.TrongBH }).ToList();

                   // int _maKP = Convert.ToInt32(lupKP.EditValue);
                    //= (from vp in data.VienPhis.Where(p => p.NgayTT >= ngaytu && p.NgayTT <= ngayden)
                    //                 join bn in data.BenhNhans.Where(p => cboDTuong.SelectedIndex == 2 || p.DTuong == _dtuong).Where(p => _noitru == 2 || p.NoiTru == _noitru) on vp.MaBNhan equals bn.MaBNhan
                    //                 join cls in data.CLS.Where(p => _intKhoa.Contains(p.MaKPth ?? 0))
                    //                 on bn.MaBNhan equals cls.MaBNhan
                    //                 join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                    //                 join dv in data.DichVus on cd.MaDV equals dv.MaDV
                    //                 join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                    //                 join ndv in data.NhomDVs.Where(p => p.TenNhomCT == "Chẩn đoán hình ảnh" || p.TenNhomCT == "Xét nghiệm") on tn.IDNhom equals ndv.IDNhom
                    var q_KQRiengKhoa0 = (from a in q1
                                          join b in q_DV on a.MaDV equals b.MaDV
                                          // select new {tn.IDNhom,tn.IdTieuNhom,tn.TenRG,tn.TenTN,ndv.TenNhom,ndv.TenNhomCT}
                                          where cboTrongDM.SelectedIndex == 2 ? true : a.TrongBH == cboTrongDM.SelectedIndex
                                          group new { a, b } by new { b.IdTieuNhom, b.TenTN, b.TenRG, b.TenNhom, b.TenNhomCT, a.MaBNhan } into kq
                                          select new KetQua
                                          {
                                              // MaKP = _maKP,
                                              IdTieuNhom = kq.Key.IdTieuNhom,
                                              TenTN = kq.Key.TenTN,
                                              TenRG = kq.Key.TenRG,
                                              TenNhom = kq.Key.TenNhom,
                                              TenNhomCT = kq.Key.TenNhomCT,
                                              TongXN = 1
                                          }).OrderBy(p => p.TenNhom).ThenBy(p => p.TenTN).ToList();

                   q_KQRiengKhoa = (from bn in q_KQRiengKhoa0
                                     group bn by new { bn.IdTieuNhom, bn.TenTN, bn.TenRG, bn.TenNhom, bn.TenNhomCT } into kq
                                     select new KetQua
                                     {
                                         // MaKP = _maKP,
                                         IdTieuNhom = kq.Key.IdTieuNhom,
                                         TenTN = kq.Key.TenTN,
                                         TenRG = kq.Key.TenRG,
                                         TenNhom = kq.Key.TenNhom,
                                         TenNhomCT = kq.Key.TenNhomCT,
                                         TongXN = kq.Count()
                                     }).OrderBy(p => p.TenNhom).ThenBy(p => p.TenTN).ToList();
                }
            }
            #endregion
            BaoCao.rep_BCHD_CLS rep = new BaoCao.rep_BCHD_CLS(q_KQRiengKhoa, ckHienThiXNKhoa.Checked);
            if (_lKP.Count == 1)
            {
                rep.paramKP.Value = _lKP.First().TenKP.ToUpper();
            }

            _LKQ = (from kp in _lKP
                    join a in q_NgayTH on kp.MaKP equals a.MaKP
                    group new { a } by new { a.IdTieuNhom, a.TenNhom, a.TenNhomCT, a.TenRG,  a.TenTN} into kq
                    select new KetQua
                    {
                        IdTieuNhom = kq.Key.IdTieuNhom,
                        TenTN = kq.Key.TenTN,
                        TenRG = kq.Key.TenRG,
                        TenNhom = kq.Key.TenNhom,
                        TenNhomCT = kq.Key.TenNhomCT,
                        TongXN = kq.Sum(p=>p.a.TongXN),
                        TongL=kq.Sum(p=>p.a.TongL)
                    }).OrderBy(p => p.TenNhom).ThenBy(p => p.TenTN).ToList();
            if (_LKQ.Count > 0)
            {

                frmIn frm = new frmIn();
                rep.DataSource = _LKQ;
                rep.paramNgay.Value = "( Từ ngày " + ngaytu.ToShortDateString() + " đến ngày " + ngayden.ToShortDateString() + " )";
                rep.paramTongNhomXN.Value = _LKQ.Where(p => p.TenNhomCT == "Xét nghiệm").Sum(p => p.TongXN);
                int _soBNRV = 0;
                var ravien = data.RaViens.Where(p => p.NgayRa >= ngaytu && p.NgayRa <= ngayden).ToList();
                #region hiển thị số bệnh nhân ra viện
                if (cbo_NgayTT.SelectedIndex == 0)
                {
                    var q3 = (from cls in data.CLS.Where(p => p.NgayTH >= ngaytu && p.NgayTH <= ngayden)
                              join bn in data.BenhNhans.Where(p => cboDTuong.SelectedIndex == 2 || p.DTuong == _dtuong).Where(p => _noitru == 2 || p.NoiTru == _noitru) on cls.MaBNhan equals bn.MaBNhan
                              join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                              select new { cls.MaKP, cd.MaDV, cd.IDCD, cls.MaBNhan, cd.TrongBH }).ToList();


                    //join cls in data.CLS.Where(p => p.NgayTH >= ngaytu && p.NgayTH <= ngayden) on kp.MaKP equals cls.MaKP
                    //join bn in data.BenhNhans.Where(p => cboDTuong.SelectedIndex == 2 || p.DTuong == _dtuong).Where(p => _noitru== 2 || p.NoiTru == _noitru) on cls.MaBNhan equals bn.MaBNhan
                    //join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                    //join dv in data.DichVus on cd.MaDV equals dv.MaDV
                    //join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                    //join ndv in data.NhomDVs.Where(p => p.TenNhomCT ==  "Chẩn đoán hình ảnh"|| p.TenNhomCT == "Xét nghiệm") on tn.IDNhom equals ndv.IDNhom
                    //join rv in data.RaViens.Where(p => p.NgayRa >= ngaytu && p.NgayRa <= ngayden) on cls.MaBNhan equals rv.MaBNhan
                    var bnRaVien = (from kp in _lKP
                                    join cd in q3 on kp.MaKP equals cd.MaKP
                                    join dv in q_DV on cd.MaDV equals dv.MaDV
                                    join rv in ravien on cd.MaBNhan equals rv.MaBNhan
                                    where cboTrongDM.SelectedIndex == 2 ? true : cd.TrongBH == cboTrongDM.SelectedIndex
                                    select rv).Distinct().ToList();
                    _soBNRV = bnRaVien.Count;
                }
                else
                {
                    DateTime ngaytunew = ngaytu.AddDays(-10);
                    DateTime ngaydennew = ngayden.AddDays(10);
                    var q8 = (from cls in data.CLS.Where(p => p.NgayTH >= ngaytunew && p.NgayTH <= ngaydennew)
                                 join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                                 select new { cls.MaBNhan, cls.MaKP, cd.MaDV, cd.TrongBH, cd.IDCD, cls.MaKPth }).ToList();
                    var q4 = (from vp in q5
                              join cls in q8 on vp.MaBNhan equals cls.MaBNhan
                              //join cls in data.CLS on vp.MaBNhan equals cls.MaBNhan
                              ////join bn in data.BenhNhans.Where(p => cboDTuong.SelectedIndex == 2 || p.DTuong == _dtuong).Where(p => _noitru == 2 || p.NoiTru == _noitru) on cls.MaBNhan equals bn.MaBNhan
                              ////join vp in data.VienPhis.Where(p => p.NgayTT >= ngaytu && p.NgayTT <= ngayden) on bn.MaBNhan equals vp.MaBNhan
                              //join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                              select new { cls.MaKP, cls.MaDV, cls.IDCD, cls.MaBNhan, cls.TrongBH }).ToList();

                    //join cls in data.CLS on kp.MaKP equals cls.MaKP
                    //join bn in data.BenhNhans.Where(p => cboDTuong.SelectedIndex == 2 || p.DTuong == _dtuong).Where(p =>_noitru == 2 || p.NoiTru == _noitru ) on cls.MaBNhan equals bn.MaBNhan
                    //join vp in data.VienPhis.Where(p => p.NgayTT >= ngaytu && p.NgayTT <= ngayden) on bn.MaBNhan equals vp.MaBNhan
                    //join cd in data.ChiDinhs.Where(p=>p.Status==1) on cls.IdCLS equals cd.IdCLS
                    //join dv in data.DichVus on cd.MaDV equals dv.MaDV
                    //join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                    //join ndv in data.NhomDVs.Where(p => p.TenNhomCT ==  "Chẩn đoán hình ảnh"|| p.TenNhomCT == "Xét nghiệm") on tn.IDNhom equals ndv.IDNhom
                    //join rv in data.RaViens.Where(p => p.NgayRa >= ngaytu && p.NgayRa <= ngayden) on cls.MaBNhan equals rv.MaBNhan
                    var bnRaVien = (from kp in _lKP
                                    join cd in q4 on kp.MaKP equals cd.MaKP
                                    join dv in q_DV on cd.MaDV equals dv.MaDV
                                    join rv in ravien on cd.MaBNhan equals rv.MaBNhan
                                    where cboTrongDM.SelectedIndex == 2 ? true : cd.TrongBH == cboTrongDM.SelectedIndex
                                    select rv).Distinct().ToList();
                    _soBNRV = bnRaVien.Count;
                }
                #endregion
                rep.paramSoBN.Value = _soBNRV.ToString() + "   bệnh nhân";
                rep.DataBindings();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Không có dữ liệu!");
            }

        }
        public class KhoaPhong
        {
            public bool _check;
            public int _maKP;
            public string _kp;

            public int MaKP { get { return _maKP; } set { _maKP = value; } }
            public bool Check { get { return _check; } set { _check = value; } }
            public string TenKP { get { return _kp; } set { _kp = value; } }
        }

        private void frm_BCHD_CLS_Load(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                lupNgaytu1.Visible = true;
                lupngayden1.Visible = true;
                lupNgaytu.Visible = false;
                lupngayden.Visible = false;
                lupNgaytu1.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                lupngayden1.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 0);
            }
            else
            {
                lupNgaytu.DateTime = System.DateTime.Now;
                lupngayden.DateTime = System.DateTime.Now;
            }
            var q = (from kp in data.KPhongs.Where(p => p.PLoai == "Phòng khám" || p.PLoai == "Lâm sàng")
                     select new KhoaPhong()
                     {
                         Check = false,
                         MaKP = kp.MaKP,
                         TenKP = kp.TenKP
                     }).Distinct().ToList();

            var q1 = (from kp in data.KPhongs.Where(p => p.PLoai == "Cận lâm sàng")
                     select new KhoaPhong()
                     {
                         Check = false,
                         MaKP = kp.MaKP,
                         TenKP = kp.TenKP
                     }).Distinct().ToList();
          //  lupKP.Properties.DataSource = q1;
            cklKP.DisplayMember = "TenKP";
            cklKP.ValueMember = "MaKP";
            cklKP.DataSource = q1;
            cboTrongDM.SelectedIndex = 2;
            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                if (cklKP.GetItemValue(i) != null && Convert.ToInt32(cklKP.GetItemValue(i)) == DungChung.Bien.MaKP)
                    cklKP.SetItemChecked(i, true);
            }

            List<KhoaPhong> _lKP2 = new List<KhoaPhong>(q.Count + 1);
            _lKP2.Add(new KhoaPhong { Check = false, MaKP = 0, TenKP = "Tất cả" });
            _lKP2.InsertRange(1, q);
            grcKhoaPhong.DataSource = _lKP2;
            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
            {
                grvKhoaPhong.SetRowCellValue(i, colCheckGrvKP, true);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public class KetQua
        {
            private int idTieuNhom;
            private string tenTN;
            private string tenNhom;
            private int tongNhomXN;
            private int tongXN;
            private int tongL;
            private string tenRG;
            private string tenNhomCT;
            private int makp;
            public int TongL
            {
                get { return tongL; }
                set { tongL = value; }
            }
            public int MaKP
            {
                get { return makp; }
                set { makp = value; }
            }
            public string TenNhomCT
            {
                get { return tenNhomCT; }
                set { tenNhomCT = value; }
            }

            public string TenTN
            {
                get { return tenTN; }
                set { tenTN = value; }
            }
            public string TenRG
            {
                get { return tenRG; }
                set { tenRG = value; }
            }
            public string TenNhom
            {
                get { return tenNhom; }
                set { tenNhom = value; }
            }
            public int TongNhomXN
            {
                get { return tongNhomXN; }
                set { tongNhomXN = value; }
            }
            public int TongXN
            {
                get { return tongXN; }
                set { tongXN = value; }
            }
            public int IdTieuNhom
            {
                get { return idTieuNhom; }
                set { idTieuNhom = value; }
            }
        }

        private void ckHienThiXNKhoa_CheckedChanged(object sender, EventArgs e)
        {
           
                cklKP.Enabled = ckHienThiXNKhoa.Checked;
        }
    }
}