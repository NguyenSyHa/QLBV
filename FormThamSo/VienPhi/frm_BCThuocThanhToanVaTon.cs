using System;using QLBV_Database;
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
    public partial class frm_BCThuocThanhToanVaTon : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCThuocThanhToanVaTon()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay1; 
            DateTime dtCuoiKytruoc;
            DateTime tungay = DungChung.Ham.NgayTu( dtHienTai.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
           // tungay1 = new DateTime(tungay.Year, tungay.Month - 1, 1);
            tungay1 = tungay.AddMonths(-1);
            dtCuoiKytruoc = denngay.AddMonths(-1);
           
            int iddt = 100;           
            int trongBH = rdTrongNgoaiDM.SelectedIndex;
            int noitru = radio_DTNT.SelectedIndex;
            if(lupDoituong.EditValue != null)
                iddt = Convert.ToInt32(lupDoituong.EditValue);
            List<KhoaPhong> _lKP = new List<KhoaPhong>();
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
            //var q0 = (
            //             from dt in data.DThuocs.Where(p=>p.NgayKe >= tungay1)
            //             join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan                        
            //             select new {dt.IDDon,dt.MaBNhan, dt.NgayKe,bn.NoiTru, bn.IDDTBN, bn.DTNT }).ToList();
            //var qdv = (                       
            //             from dtct in data.DThuoccts 
            //             join dv in data.DichVus on dtct.MaDV equals dv.MaDV
            //             select new {dtct.IDDon, dtct.IDDonct, dtct.MaDV, dtct.DonVi, dtct.SoLuong, dtct.ThanhTien, dtct.TrongBH, dtct.DonGia, dv.TenDV }).ToList();
            //var q1 = (
            //            from a in q0
            //            join b in qdv on a.IDDon equals b.IDDonct                       
            //            select new { a.MaBNhan, a.NgayKe, b.MaDV, b.DonVi, b.SoLuong, b.ThanhTien, b.TrongBH, b.DonGia, b.TenDV, a.NoiTru, a.IDDTBN, a.DTNT }).ToList();
            var q0 = (   from dt in data.DThuocs.Where(p => p.NgayKe >= tungay1)                         
                         join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                         join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
                         join dv in data.DichVus.Where(p=>p.PLoai == 1) on dtct.MaDV equals dv.MaDV
                         select new { dt.MaBNhan,dt.MaKP, dt.NgayKe, dtct.MaDV, dtct.DonVi, dtct.SoLuong, dtct.ThanhTien, dtct.TrongBH, dtct.DonGia, dv.TenDV, bn.NoiTru, bn.IDDTBN, bn.DTNT }).ToList();
            var q1 = (from a in q0 join kp in _lKP on a.MaKP equals kp.MaKP select a).ToList();

            var q2 = q1.Where(p => iddt == 100 || p.IDDTBN == iddt).Where(p => trongBH == 2 || p.TrongBH == trongBH).Where(p => noitru == 0 ? (p.NoiTru == 0 && p.DTNT == true) : (noitru == 1 ? p.NoiTru == 1 : (p.NoiTru == 1 || (p.NoiTru == 0 && p.DTNT == true)))).ToList();
           
            var q3 = (from a in q2
                      join vp in data.VienPhis on a.MaBNhan equals vp.MaBNhan
                          into kq
                      from kq1 in kq.DefaultIfEmpty()
                      select new
                      {
                          a.MaBNhan,
                          a.NgayKe,
                          a.MaDV,
                          a.DonVi,
                          a.DonGia,
                          a.SoLuong,
                          a.ThanhTien,
                          a.TrongBH,
                          a.TenDV,
                          a.NoiTru,
                          a.IDDTBN,
                          a.DTNT,
                          NgayTT = kq1 != null ? kq1.NgayTT : null,
                          vp = kq1 == null? 0: 1
                      }).ToList();

            var q4 = (from a in q3
                      group a by new {a.DonGia, a.MaDV, a.TenDV, a.DonVi} into kq
                      select new { 
                      kq.Key.TenDV,
                      kq.Key.DonGia,
                      kq.Key.DonVi,
                      Ton1T_SL = kq.Where(p => p.NgayKe < dtCuoiKytruoc && p.NgayKe >= tungay1).Where(p => p.vp == 0 || (p.vp ==1 && p.NgayTT > dtCuoiKytruoc)).Sum(p => p.SoLuong), // tồn số lượng kỳ trước chưa thanh toán
                      Ton1T_TT = kq.Where(p => p.NgayKe < dtCuoiKytruoc && p.NgayKe >= tungay1).Where(p => p.vp == 0 || (p.vp == 1 && p.NgayTT > dtCuoiKytruoc)).Sum(p => p.ThanhTien),
                      TonTKChuaTT_SL = kq.Where(p => p.NgayKe <= denngay && p.NgayKe >= tungay).Where(p => p.vp == 0 || (p.vp == 1 &&  p.NgayTT > denngay)).Sum(p => p.SoLuong),// tồn trong kỳ chưa thanh toán
                      TonTKChuaTT_TT = kq.Where(p => p.NgayKe <= denngay && p.NgayKe >= tungay).Where(p => p.vp == 0 || (p.vp == 1 && p.NgayTT > denngay)).Sum(p => p.ThanhTien),
                      TonTKDaTT_SL = kq.Where(p => p.NgayKe <= denngay && p.NgayKe >= tungay).Where(p => p.vp == 1 && p.NgayTT <= denngay && p.NgayTT >= tungay).Sum(p => p.SoLuong),// tồn trong kỳ đã thanh toán
                      TonTKDaTT_TT = kq.Where(p => p.NgayKe <= denngay && p.NgayKe >= tungay).Where(p => p.vp == 1 && p.NgayTT <= denngay && p.NgayTT >= tungay ).Sum(p => p.ThanhTien),
                      }).ToList();
            var q5 = (from a in q4
                      select new {
                      a.TenDV,
                      a.DonVi,
                      a.DonGia,
                      a.Ton1T_SL,
                      a.Ton1T_TT,
                      a.TonTKChuaTT_SL,
                      a.TonTKChuaTT_TT,
                      a.TonTKDaTT_SL,
                      a.TonTKDaTT_TT,
                      SL = a.Ton1T_SL+ a.TonTKChuaTT_SL ,
                      TT = a.Ton1T_TT + a.TonTKChuaTT_TT
                      }).Where(p=>p.SL != 0).OrderBy(p=>p.TenDV).ToList();
            string[] _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
            string[] _tieude = { "stt", "Tên dịch vụ", "Đơn vị", "Đơn giá", "Tồn kỳ trước _SL", "Tồn kỳ trước _TT", "Tồn trong kỳ Chưa TT _SL", "Tồn trong kỳ chưa TT _TT", "Trong kỳ_ đã Thanh toán _SL", "Trong kỳ đã thanh toán _TT", "Tổng _SL", "Tổng _TT"};
            string _filePath = "D:\\BCThuocDaVaChuaThanhToan.xls";
            int[] _arrWidth = new int[] { };
            DungChung.Bien.MangHaiChieu = new Object[q5.Count + 1, 12];
            for (int i = 0; i < 12; i++)
            {
                DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
            }
            int num = 1;
            foreach (var r in q5)
            {
                DungChung.Bien.MangHaiChieu[num, 0] = num;
                DungChung.Bien.MangHaiChieu[num, 1] = r.TenDV;
                DungChung.Bien.MangHaiChieu[num, 2] = r.DonVi;
                DungChung.Bien.MangHaiChieu[num, 3] = r.DonGia;
                DungChung.Bien.MangHaiChieu[num, 4] = r.Ton1T_SL;
                DungChung.Bien.MangHaiChieu[num, 5] = r.Ton1T_TT;
                DungChung.Bien.MangHaiChieu[num, 6] = r.TonTKChuaTT_SL;
                DungChung.Bien.MangHaiChieu[num, 7] = r.TonTKChuaTT_TT;
                DungChung.Bien.MangHaiChieu[num, 8] = r.TonTKDaTT_SL;
                DungChung.Bien.MangHaiChieu[num, 9] = r.TonTKDaTT_TT;
                DungChung.Bien.MangHaiChieu[num, 10] = r.SL;
                DungChung.Bien.MangHaiChieu[num, 11] = r.TT;               
                num++;
            }
          
            BaoCao.rep_BCThuocThanhToanVaTon rep = new BaoCao.rep_BCThuocThanhToanVaTon();
            frmIn frm2 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "sheet1", _filePath, true, this.Name);
            rep.DataSource = q5;
            rep.BindingData();
            rep.lab_TenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            rep.lab_TenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            rep.lab_tungaydenngay.Text  = "Từ ngày " + dtHienTai.DateTime.ToShortDateString() + " đến ngày " + dtDenNgay.DateTime.ToShortDateString();
            rep.cel_diadanh.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            rep.cel_NguoilapBieu.Text = DungChung.Bien.NguoiLapBieu;
            rep.CreateDocument();
            frm2.prcIN.PrintingSystem = rep.PrintingSystem;
            frm2.ShowDialog();
            
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_BCThuocThanhToanVaTon_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            dtHienTai.DateTime = DateTime.Now;
            dtDenNgay.DateTime = DateTime.Now;
            radio_DTNT.SelectedIndex = 2;
            rdTrongNgoaiDM.SelectedIndex = 2;
            var q = (from k in data.KPhongs.Where(p=>p.TrongBV ==1).Where(p=>p.PLoai == ("Lâm sàng") || p.PLoai== ("Phòng khám"))                    
                     select new KhoaPhong()
                     {
                         Check = false,
                         MaKP = k.MaKP,
                         TenKP = k.TenKP
                     }).Distinct().ToList();
            List<KhoaPhong> _lKP2 = new List<KhoaPhong>(q.Count + 1);
            _lKP2.Add(new KhoaPhong { Check = false, MaKP = 0, TenKP = "Tất cả" });
            _lKP2.InsertRange(1, q);
            grcKhoaPhong.DataSource = _lKP2;
            for (int i = 0; i < grvKhoaPhong.RowCount; i++)
            {
                grvKhoaPhong.SetRowCellValue(i, colCheckGrvKP, true);
            }           
            List<DTBN> dtbn = data.DTBNs.Where(p => p.Status == 1).OrderBy(p=>p.IDDTBN).ToList();
            dtbn.Insert(0, new DTBN { IDDTBN = 100, DTBN1 = "Tất cả" });
            lupDoituong.Properties.DataSource = dtbn;
            
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

        private void grvKhoaPhong_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            
        }

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
                            grvKhoaPhong.SetRowCellValue(0, colCheckGrvKP, false);
                            break;
                        }
                        else
                        {

                        }
                    }

                }

            }
        }
    }
}