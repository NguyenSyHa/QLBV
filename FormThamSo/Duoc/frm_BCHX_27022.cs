using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.FormThamSo
{
    public partial class frm_BCHX_27022 : Form
    {
        public frm_BCHX_27022()
        {
            InitializeComponent();
        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }
        QLBV_Database.QLBVEntities db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        #region Kho
        private class Kho
        {
            public bool Check { get; set; }
            public int MaKP { get; set; }
            public string TenKP { get; set; }
        }
        #endregion
        List<Kho> kho = new List<Kho>();
        private void frm_test1_Load(object sender, EventArgs e)
        {
            
            detungay.DateTime = DateTime.Now;
            dedenngay.DateTime = DateTime.Now;
            
            var kd = from khoa in db.KPhongs.Where(p => p.PLoai == "Khoa dược") select new { khoa.TenKP, khoa.MaKP };
            if (kd.Count() > 0)
            {
                Kho themmoi1 = new Kho();
                themmoi1.TenKP = "Chọn tất cả";
                themmoi1.MaKP = 0;
                themmoi1.Check = true;
                kho.Add(themmoi1);
                foreach (var a in kd)
                {
                    Kho themmoi = new Kho();
                    themmoi.TenKP = a.TenKP;
                    themmoi.MaKP = a.MaKP;
                    themmoi.Check = true;
                    kho.Add(themmoi);
                }
                grcKho.DataSource = kho.ToList();
            }

            addDataPPXuat();
        }

        private void btntaobc_Click(object sender, EventArgs e)
        {
            DateTime tungay = DungChung.Ham.NgayTu(detungay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dedenngay.DateTime);

            string tenkho = String.Empty;
            List<Kho> khochon = new List<Kho>();
            khochon = kho.Where(p => p.MaKP > 0 && p.Check == true).ToList();
            if (khochon.Count > 0)
            {
                foreach (var item in khochon)
                {
                    tenkho += item.TenKP + ", ";
                }
            }

            List<DungChung.Bien.c_PhanLoaiXuat> _lPLChon = new List<DungChung.Bien.c_PhanLoaiXuat>();
            _lPLChon = (_lPhanLoaiX.Where(p => p.Check && p.Id >= 0).OrderBy(p => p.Id).ToList());
            for (int i = _lPhanLoaiX.Where(p => p.Check && p.Id >= 0).ToList().Count; i < _lPhanLoaiX.Count; i++)
            {
                _lPLChon.Add(new DungChung.Bien.c_PhanLoaiXuat { Check = true, Id = -1, PhanLoai = "" });
               
            }
            
            List<KPhong> kphong=db.KPhongs.ToList();
            kphong.Add(new KPhong { MaKP = -2, TenKP = "Xuất kiểm nghiệm" });
            kphong.Add(new KPhong { MaKP = -3, TenKP = "Xuất khác" });
            var dstong = (from tenthuoc in db.DichVus
                          join ndct in db.NhapDcts on tenthuoc.MaDV equals ndct.MaDV
                          join nd in db.NhapDs.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p=>p.PLoai==2||p.PLoai==3) on ndct.IDNhap equals nd.IDNhap
                    
                          select new { 
                              nd.NgayNhap,
                              nd.SoCT,
                              nd.KieuDon,
                              nd.MaKPnx,
                              nd.MaKP,
                              ndct, 
                              
                              tenthuoc.TenDV }).ToList();


            var dsthuocxuat = (from tenthuoc in dstong
                               join kpc in khochon on tenthuoc.MaKP equals kpc.MaKP
                               join plx in _lPLChon on tenthuoc.KieuDon equals plx.Id
                               join kp in kphong on tenthuoc.MaKPnx equals kp.MaKP into kq
                               from c in kq.DefaultIfEmpty()
                               group tenthuoc by new
                               {

                                   
                                   NgayNhap = tenthuoc.NgayNhap.Value.ToShortDateString(),
                                   tenthuoc.SoCT,
                                   tenthuoc.ndct.MaDV,
                                   tenthuoc.TenDV,
                                   tenthuoc.ndct.DonVi,
                                   tenthuoc.ndct.DonGia,
                                   MaKPnx=c==null?(tenthuoc.KieuDon==8?-2:-3):c.MaKP

                               } into kq
                               select new
                               {

                                   NgayNhap = kq.Key.NgayNhap,
                                   kq.Key.SoCT,
                                   kq.Key.MaDV,
                                   kq.Key.TenDV,
                                   kq.Key.DonVi,
                                   
                                   SoLuongX = kq.Sum(p => p.ndct.SoLuongX),
                                   kq.Key.DonGia,
                                   TenKP=kphong.Where(p=>p.MaKP==kq.Key.MaKPnx).FirstOrDefault().TenKP,
                                   ThanhtienX = kq.Sum(p => p.ndct.ThanhTienX)
                               }).OrderBy(p => p.NgayNhap).ToList();
            frmIn frm = new frmIn();
            BaoCao.Rep_BCHX_27022 rep = new BaoCao.Rep_BCHX_27022();
            rep.lab_tencq.Text = DungChung.Bien.TenCQ.ToUpper();
            rep.lab_CQchuquan.Text = DungChung.Bien.TenCQCQ.ToUpper();
            rep.ngaythang.Value = "Từ ngày: " + tungay.ToShortDateString() + " Đến ngày: " + denngay.ToShortDateString() + " \n" + tenkho;
            rep.DataSource = dsthuocxuat;
            rep.databinding();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cklKP_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            int check = 0;
            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                if (cklKP.GetItemChecked(i))
                    check++;
            }

        }

        private void cklKP_CheckMemberChanged(object sender, EventArgs e)
        {

        }

        private void grvKho_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //string dsmakp = "";
            //for (int i = 0; i < grvKho.RowCount; i++)
            //{
            //    if( grvKho.GetRowCellValue(i, colMaKP).ToString()=="0"){
            //        for (int j = 0; j < grvKho.RowCount; j++) {
            //            if (grvKho.GetRowCellValue(i, colChon).ToString() == "True")
            //            grvKho.SetRowCellValue(i, colChon, true);
            //            else
            //                grvKho.SetRowCellValue(i, colChon, false);

            //        }
            //        break;
            //    }

            //}

        }
        
        private void grvKho_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            if (e.RowHandle == 0)
            {

                for (int j = 1; j < grvKho.RowCount; j++)
                {

                    grvKho.SetRowCellValue(j, colChon, Convert.ToBoolean(e.Value));

                }
            }
        }

        private void rdgMauBC_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void addDataPPXuat()
        {
            _lPhanLoaiX = new List<DungChung.Bien.c_PhanLoaiXuat>();
            grcPPXuat.DataSource = null;
            _lPhanLoaiX = DungChung.Bien.c_PhanLoaiXuat._setPhanLoaiXuat();
            _lPhanLoaiX.Add(new DungChung.Bien.c_PhanLoaiXuat { Id = -1, PhanLoai = "Chọn tất cả", Check = true });
            grcPPXuat.DataSource = _lPhanLoaiX.OrderBy(p => p.Id);
        }
        List<DungChung.Bien.c_PhanLoaiXuat> _lPhanLoaiX = new List<DungChung.Bien.c_PhanLoaiXuat>();
        private void grvPPXuat_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            
        }

        private void grvPPXuat_CellValueChanging_1(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Check")
                        {
                            if (grvPPXuat.GetFocusedRowCellValue("PhanLoai") != null)
                            {
                                string Ten = grvPPXuat.GetFocusedRowCellValue("PhanLoai").ToString();

                                if (Ten == "Chọn tất cả")
                                {
                                    if (_lPhanLoaiX.First().Check == true)
                                    {
                                        foreach (var a in _lPhanLoaiX)
                                        {
                                            a.Check = false;
                                        }
                                    }
                                    else
                                    {
                                        foreach (var a in _lPhanLoaiX)
                                        {
                                            a.Check = true;
                                        }
                                    }
                                    grcPPXuat.DataSource = "";
                                    grcPPXuat.DataSource = _lPhanLoaiX.ToList();
                                }
                            }
                        }
        }
    }
}
