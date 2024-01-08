using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using QLBV.DungChung;

namespace QLBV.FormThamSo
{
    public partial class us_chonxuatduoc : DevExpress.XtraEditors.XtraUserControl
    {
        public us_chonxuatduoc()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<DichVu> _dvu = new List<DichVu>();
        private ConnectData connect;
        private void timkiemgia()
        {
            dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int makp = 0;
            if (lupKho.EditValue != null)
                makp = Convert.ToInt32(lupKho.EditValue);
            var c = (from ut in dataContext.GiaUTs.Where(p => p.MaKP == makp) select new { ut.MaDV, ut.DonGia, ut.SoLo, ut.HanDung }).Where(p => p.MaDV != null && p.MaDV.Value > 0).ToList();
            grcDongia.DataSource = c.ToList();
        }
        List<DichVu> _ldv = new List<DichVu>();
        private void us_chonxuatduoc_Load(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                colHanSD.Visible = true;
                colHanDungGiaUT.Visible = true;
               
            }
            else
            {
                colHanSD.Visible = false;
                colHanDungGiaUT.Visible = false;
                
            }
            connect = Program._connect;
            grcTenDV.DataSource = _dvu;
            var D = (from tk in dataContext.KPhongs.Where(p => p.PLoai == ("Khoa dược")) select new { tk.TenKP, tk.MaKP, }).ToList();
            lupKho.Properties.DataSource = D.ToList();
            lupMaKho_ut.DataSource = D;
            _ldv = dataContext.DichVus.Where(p => p.PLoai == 1).ToList();
            lupTenDV.DataSource = _ldv;
            timkiemgia();
            TimKiem();
        }

        private void grvTendv_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            taogrv();
        }


        private void taogrv()
        {
            if (grvTendv.GetFocusedRowCellValue(colMaDV) != null)
            {

                if (!string.IsNullOrEmpty(lupKho.Text))
                {
                    int _MaDV = 0;
                    if (grvTendv.GetFocusedRowCellValue(colMaDV) != null)
                    {
                        int makp = 0;
                        if (lupKho.EditValue != null)
                            makp = Convert.ToInt32(lupKho.EditValue);
                        grvChonDG.ViewCaption = grvTendv.GetFocusedRowCellValue(colTendv).ToString();
                        _MaDV = Convert.ToInt32(grvTendv.GetFocusedRowCellValue(colMaDV));
                        txtMadv.Text = _MaDV.ToString();
                        _Madv1 = txtMadv.Text == "" ? 0 : Convert.ToInt32(txtMadv.Text);
                        _Tendv1 = grvTendv.GetFocusedRowCellValue(colTendv).ToString();
                        var b0 = (from dv in dataContext.DichVus.Where(p => p.MaDV == _MaDV)
                                  join nxct in dataContext.NhapDcts on dv.MaDV equals nxct.MaDV
                                  join nd in dataContext.NhapDs.Where(p => p.MaKP == (makp)) on nxct.IDNhap equals nd.IDNhap
                                  select new { nxct.DonGia, dv.TenDV, dv.MaDV, dv.DongY, dv.TyLeBQ, dv.TyLeSP, nd.PLoai, nxct.SoLuongDY, dv.TyLeSD, nxct.SoLo, nxct.SoLuongN, nxct.SoLuongX, dv.MaTam, nxct.HanDung }).ToList();
                        #region tính hư hao
                        //double tyleBQ = 0;
                        //double tyleSP = 0;
                        //var qdv = dataContext.DichVus.Where(p=>p.MaDV == _MaDV).FirstOrDefault();
                        //if(qdv != null)
                        //{
                        //    tyleBQ = qdv.TyLeBQ??0;
                        //    tyleSP = qdv.TyLeSP??0;
                        //}

                        //double tyle = (tyleBQ + tyleSP)/100;


                        //var qpphh = dataContext.KPhongs.Where(p => p.MaKP == makp && p.PPHHDY == 0 && p.Status == 1).FirstOrDefault();

                        //var b = (from nd in b0
                        //         group nd by new { nd.DonGia, nd.TenDV, nd.MaDV, nd.solo } into kq
                        //         select new
                        //         {
                        //             SLT = (kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX) - (qpphh == null ? 0 : (kq.Sum(p=> p.SoLuongX * tyle)))),
                        //             DonGia = kq.Key.DonGia,
                        //             SoLo = kq.Key.solo,
                        //         }).ToList();
                        #endregion

                        #region lấy hư hao bằng số lượng đông y
                        if(DungChung.Bien.MaBV == "24012")
                        {
                            var b = (from nd in b0
                                     group nd by new { nd.DonGia, nd.TenDV, nd.MaDV, nd.SoLo, nd.MaTam } into kq
                                     select new
                                     {
                                         SLT = (kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX)),
                                         DonGia = kq.Key.DonGia,
                                         SoLo = kq.Key.SoLo,
                                         MaNoiBo = kq.Key.MaTam,
                                         kq.Key.MaDV
                                     }).ToList();
                            List<_list> l = new List<_list>();
                            foreach (var item in b)
                            {
                                _list a = new _list();
                                a.SLT = item.SLT;
                                a.MaNoiBo = item.MaNoiBo;
                                a.DonGia = item.DonGia;
                                a.SoLo = item.SoLo;
                                var handung = (from nd in b0.Where(p => p.PLoai == 1) select nd).ToList();
                                var qhd = handung.Where(p => p.MaDV == item.MaDV && p.SoLo == item.SoLo && p.HanDung != null).FirstOrDefault();
                                if (qhd != null)
                                    a.HanDung = qhd.HanDung;
                                l.Add(a);
                            }
                            #endregion
                            grcChondg.DataSource = l;
                        }
                        else
                        {
                            var b = (from nd in b0
                                     group nd by new { nd.DonGia, nd.TenDV, nd.MaDV, nd.MaTam } into kq
                                     select new
                                     {
                                         SLT = (kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX)),
                                         DonGia = kq.Key.DonGia,
                                         MaNoiBo = kq.Key.MaTam
                                     }).ToList();
                            grcChondg.DataSource = b;
                        }
                    }
                    else
                        grcChondg.DataSource = null;
                }
                else
                    grcChondg.DataSource = null;

            }
        }
        List<GiaUT> _giaUT = new List<GiaUT>();
        int ppxuatduoc = -1;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int tt = 0;
            if (_Dongia != "")
            {
                int _makp = 0;
                if (lupKho.EditValue != null)
                    _makp = Convert.ToInt32(lupKho.EditValue);



                List<GiaUT> _lGiaUT = new List<GiaUT>();
                if (ppxuatduoc == 3)
                {
                    _lGiaUT = dataContext.GiaUTs.Where(p => p.MaDV == _Madv1).Where(p => p.MaKP == _makp).ToList();
                }
                else
                    _lGiaUT = dataContext.GiaUTs.Where(p => p.MaDV == _Madv1).Where(p => p.MaKP == _makp).ToList();
                if (_lGiaUT.Count > 0)
                {
                    DialogResult _result = MessageBox.Show("Thuốc đã đặt đơn giá ưu tiên. Bạn có muốn thay đổi?", "Đổi giá", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_result == DialogResult.Yes)
                    {
                        foreach (var a in _lGiaUT)
                        {
                            int id = a.IdGiaUT;
                            var xoa = dataContext.GiaUTs.Single(p => p.IdGiaUT == (id));
                            dataContext.GiaUTs.Remove(xoa);
                            dataContext.SaveChanges();
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
                    GiaUT themmoi = new GiaUT();
                    int makho = 0;
                    if (lupKho.EditValue != null)
                        makho = Convert.ToInt32(lupKho.EditValue);
                    if (!string.IsNullOrEmpty(_Dongia))
                        themmoi.DonGia = Convert.ToDouble(_Dongia);
                    themmoi.MaKP = makho;
                    themmoi.MaDV = _Madv1;
                    themmoi.SoLo = _solo;

                    if (!string.IsNullOrEmpty(_handung))
                        themmoi.HanDung = Convert.ToDateTime(_handung);
                    
                    _giaUT.Add(themmoi);
                    dataContext.GiaUTs.Add(themmoi);
                    dataContext.SaveChanges();
                    timkiemgia();
                }
                _Dongia = "";
            }
        }

        // kiêm tra

        //ket thúc kiểm tra
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            foreach (var a in _giaUT)
            {
                GiaUT themmoi = new GiaUT();
                themmoi.DonGia = a.DonGia;
                themmoi.MaDV = a.MaDV;
                dataContext.GiaUTs.Add(themmoi);
                dataContext.SaveChanges();

            }
        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();

        }


        private void simpleButton4_Click(object sender, EventArgs e)
        {
            txtTimKiem.Text = "";
        }
        private void TimKiem()
        {
            dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int makp = 0;
            string tendv = "";
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
                tendv = txtTimKiem.Text;
            if (lupKho.EditValue != null)
                makp = Convert.ToInt32(lupKho.EditValue);
            if (makp > 0)
            {
                var nhapDuoc = (from nd in dataContext.NhapDs.Where(p => p.MaKP == makp && p.PLoai == 1)
                                join ndct in dataContext.NhapDcts on nd.IDNhap equals ndct.IDNhap
                                select ndct.MaDV).Distinct();
                _dvu = (from dv in dataContext.DichVus.Where(p => p.PLoai == 1 && p.TenDV.Contains(tendv))
                        join nd in nhapDuoc on dv.MaDV equals nd.Value
                        select dv).ToList();
            }
            else
            {
                _dvu = null;
            }
            grcTenDV.DataSource = _dvu;
        }
        int _makp = 0;
        private void lupKho_EditValueChanged(object sender, EventArgs e)
        {
            if (lupKho.EditValue != null)
            {
                _makp = Convert.ToInt32(lupKho.EditValue);
            }
            var pp = dataContext.KPhongs.Where(p => p.MaKP == _makp).Select(p => p.PPXuat).FirstOrDefault();
            if (pp != null)
                ppxuatduoc = pp.Value;
            TimKiem();
            timkiemgia();
        }
        int _Madv1 = 0;
        string _Tendv1 = "";
        string _Dongia = "";
        string _solo = "";
        string _handung = "";

        private void grvChonDG_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
           _handung = "";
            if (grvChonDG.GetFocusedRowCellValue(colSoLo) != null && grvChonDG.GetFocusedRowCellValue(colSoLo).ToString() != "")
                _solo = grvChonDG.GetFocusedRowCellValue(colSoLo).ToString();
            if (grvChonDG.GetFocusedRowCellValue(colHanSD) != null && grvChonDG.GetFocusedRowCellValue(colHanSD).ToString() != "")
                _handung = grvChonDG.GetFocusedRowCellValue(colHanSD).ToString();
            if (grvChonDG.GetFocusedRowCellValue(colDongiachon) != null && grvChonDG.GetFocusedRowCellValue(colDongiachon).ToString() != "")
            {
                txtDonGia.Text = grvChonDG.GetFocusedRowCellValue(colDongiachon).ToString();
                _Dongia = grvChonDG.GetFocusedRowCellValue(colDongiachon).ToString();
            }
            else
            {
                txtDonGia.Text = "";
                _Dongia = "";
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {

        }

        private void grvDongia_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colXoa")
            {
                if (grvDongia.GetFocusedRowCellValue(colMaDV) != null)
                {
                    int madv = 0;
                    madv = Convert.ToInt32(grvDongia.GetFocusedRowCellValue(colMaDV));

                    if (grvDongia.GetFocusedRowCellValue(colDongia) != null)
                    {
                        double dongia = Convert.ToDouble(grvDongia.GetFocusedRowCellValue(colDongia));
                        int makp = 0;
                        if (lupKho.EditValue != null)
                            makp = Convert.ToInt32(lupKho.EditValue);
                        DialogResult _result = MessageBox.Show("Bạn muốn bỏ giá ưu tiên của thuốc: " + grvDongia.GetFocusedRowCellDisplayText(colTendv) + "?", "Hỏi bỏ!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (_result == DialogResult.Yes)
                        {
                            var xoagia = dataContext.GiaUTs.Single(p => p.MaDV == madv && p.DonGia == dongia && p.MaKP == makp);
                            dataContext.GiaUTs.Remove(xoagia);
                            dataContext.SaveChanges();
                            grvDongia.DeleteRow(e.RowHandle);
                        }
                    }
                }
            }
        }

        private void grvTendv_DataSourceChanged(object sender, EventArgs e)
        {
            taogrv();
        }

        private void grvChonDG_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if (e.Column.Name == "colUtien")
            //{
            //    if (grvChonDG.GetFocusedRowCellValue(colDongia) != null && grvChonDG.GetFocusedRowCellValue(colDongia).ToString() != "")
            //    {
            //        _Dongia = grvChonDG.GetFocusedRowCellValue(colDongiachon).ToString();
            //    }
            //}
        }

        private void grvChonDG_DataSourceChanged(object sender, EventArgs e)
        {
            if (grvChonDG.GetFocusedRowCellValue(colDongiachon) != null && grvChonDG.GetFocusedRowCellValue(colDongiachon).ToString() != "")
            {
                txtDonGia.Text = grvChonDG.GetFocusedRowCellValue(colDongiachon).ToString();
                _Dongia = grvChonDG.GetFocusedRowCellValue(colDongiachon).ToString();
            }
        }

        private void grvDongia_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int madv = 0;
            double dongia = 0;
            double ton = 0;
            if (grvDongia.GetFocusedRowCellValue(colMaDV) != null)
                madv = Convert.ToInt32(grvDongia.GetFocusedRowCellValue(colMaDV));
            if (grvDongia.GetFocusedRowCellValue(colDongia) != null)
                dongia = Convert.ToInt32(grvDongia.GetFocusedRowCellValue(colDongia));
            if (madv > 0 && dongia > 0)
            {
                var c0 = (from nd in dataContext.NhapDs.Where(p => p.MaKP == _makp)
                          join ndct in dataContext.NhapDcts.Where(p => p.MaDV == madv).Where(p => p.DonGia == dongia) on nd.IDNhap equals ndct.IDNhap
                          group ndct by new { ndct.MaDV } into kq
                          select new { Ton = kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX) }).ToList();
                if (c0.Count > 0)
                    ton = c0.First().Ton;

            }
            //hiển thị tồn
            groupControl2.Text = "Danh sách thuốc đã chọn                                   " + "Tồn: " + ton.ToString();
        }

        private void grcTenDV_Click(object sender, EventArgs e)
        {

        }
    }
    public class _list
    {
            public double? SLT { get; set; }
            public double? DonGia { get; set; }
            public string SoLo { get; set; }
            public string MaNoiBo { get; set; }
            public DateTime? HanDung { get; set; }
    }

}