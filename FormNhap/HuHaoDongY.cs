using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace QLBV.FormNhap
{
    public partial class HuHaoDongY : DevExpress.XtraEditors.XtraForm
    {
        public HuHaoDongY()
        {
            InitializeComponent();
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void HuHaoDongY_Load(object sender, EventArgs e)
        {
            var _lkp = (from kp in _data.KPhongs.Where(p => p.PLoai == "Khoa dược")
                        select new { kp.MaKP, kp.TenKP }).ToList();
            lopKhoaP.Properties.DataSource = _lkp;
            cboSLMin.SelectedIndex = 0;
            datngaytao.DateTime = System.DateTime.Now;
            //var dichvu = _data.DichVus.Where(p => p.PLoai == 1).ToList();

            //grcthuocton.DataSource = dichvu;
        }
        public class ThuocTon
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
            public double SLuongTon;
            public double sluongton
            { set { SLuongTon = value; } get { return SLuongTon; } }
            public bool Chon;
            public bool chon
            { set { Chon = value; } get { return Chon; } }

        }
        List<ThuocTon> _lThuocTon = new List<ThuocTon>();
        private void btnTim_Click(object sender, EventArgs e)
        {
            _lThuocTon.Clear();
            int _MaKP = 0, _SLuongM = -1;
            if(lopKhoaP.EditValue!=null)
            {
                _MaKP = Convert.ToInt32(lopKhoaP.EditValue);
                _SLuongM = Convert.ToInt32(cboSLMin.EditValue);
                var _dichvu = _data.DichVus.Where(p => p.DongY == 1).ToList();
                 var qpphh = _data.KPhongs.Where(p => p.MaKP == _MaKP && p.PPHHDY == 0 && p.Status == 1).FirstOrDefault();

                var abc = (from nhap in _data.NhapDs.Where(p => p.MaKP == _MaKP)
                           join nhapct in _data.NhapDcts on nhap.IDNhap equals nhapct.IDNhap
                           select new { nhap.NgayNhap, nhapct.SoLuongN, nhapct.SoLo, nhapct.HanDung,nhapct.SoLuongDY , nhapct.SoLuongX, nhapct.DonGia, nhapct.MaDV, nhap.PLoai }).ToList();
                var _gia2 = (from nhap in abc
                             group new { nhap } by new { MaDV = nhap.MaDV, nhap.DonGia } into kq
                             select new { kq.Key.DonGia,kq.Key.MaDV, soluongN = kq.Where(p => p.nhap.PLoai == 1).Sum(p => p.nhap.SoLuongN), SoLuongDY = kq.Where(p=>p.nhap.PLoai == 2).Sum(p=>p.nhap.SoLuongDY) ,soLuongX = kq.Where(p => p.nhap.PLoai == 2 || p.nhap.PLoai == 3).Sum(p => p.nhap.SoLuongX), ngay = kq.Min(p => p.nhap.NgayNhap) }).OrderBy(p => p.ngay).ToList();

                var _gia = (from nhap in _gia2
                            join dv in _dichvu on nhap.MaDV equals dv.MaDV
                            select new { nhap.DonGia, SLuongTon = Math.Round( (nhap.soluongN - nhap.soLuongX - ((dv.DongY == 1 && qpphh != null) ?  nhap.SoLuongDY : 0 )) ,5), nhap.MaDV, dv.TenDV }).ToList();
                var _lTHuocton = _gia.Where(p => p.SLuongTon <= _SLuongM).Where(p => p.SLuongTon > 0).ToList();
                if (_lTHuocton.Count() > 0)
                {
                    ThuocTon Moi = new ThuocTon();
                    Moi.madv = 0;
                    Moi.tendv = "Chọn tất cả";
                    Moi.sluongton = 0;
                    Moi.chon = false;
                    _lThuocTon.Add(Moi);
                    foreach (var item in _lTHuocton)
                    {
                        ThuocTon Moi1 = new ThuocTon();
                        Moi1.madv = item.MaDV;
                        Moi1.tendv = item.TenDV;
                        Moi1.sluongton = item.SLuongTon;
                        Moi1.dongia = item.DonGia;
                        Moi1.chon = false;
                        _lThuocTon.Add(Moi1);
                    }
                    grcthuocton.DataSource = "";
                    grcthuocton.DataSource = _lThuocTon.ToList();
                    
                }
                else
                {
                    MessageBox.Show("Không có thuốc nào nhỏ hơn :" + _SLuongM.ToString(), "Thông báo", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Chưa chọn kho", "Thông báo", MessageBoxButtons.OK);
                lopKhoaP.Focus();
            }
        }

        private void grvthuocton_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChon")
            {
                if (grvthuocton.GetFocusedRowCellValue("tendv") != null)
                {
                    string Ten = grvthuocton.GetFocusedRowCellValue("tendv").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_lThuocTon.First().chon == true)
                        {
                            foreach (var a in _lThuocTon)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lThuocTon)
                            {
                                a.chon = true;
                            }
                        }
                        grcthuocton.DataSource = "";
                        grcthuocton.DataSource = _lThuocTon.ToList();
                    }
                }
            }
            int _madv = grvthuocton.GetFocusedRowCellValue(colMaDV) == null ? 0 : Convert.ToInt32(grvthuocton.GetFocusedRowCellValue(colMaDV));
            if(_lThuocTon.Where(p=>p.madv==_madv).Count()>0)
            {
                ThuocTon sua = _lThuocTon.Where(p => p.madv == _madv).First();
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
            grcthuochuhao.DataSource = "";
            grcthuochuhao.DataSource = _lThuocTon.Where(p => p.Chon == true);
        }

        private void grvthuocton_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if(e.Column.Name=="colChon")
            //{
            //    //string chon=grvthuocton.GetFocusedRowCellValue("chon").ToString();
            //    //MessageBox.Show(chon, "test", MessageBoxButtons.OK);

            //    var test = _lThuocTon.Where(p => p.chon == true).ToList();
            //    grcthuochuhao.DataSource = test;
            //}
            
        }
        private void LuuHuHao(int makp)
        {
            NhapD moi = new NhapD();
            moi.NgayNhap = datngaytao.DateTime;
            moi.SoCT = txtsoct.Text;
            moi.PLoai = 3;
            moi.MaKP = makp;
            moi.GhiChu = cbolydo.Text == null ? null : cbolydo.Text;
            _data.NhapDs.Add(moi);
            _data.SaveChanges();
            int _idnhap = moi.IDNhap;
            for(int i=0;i<grvthuochuhao.RowCount;i++)
            {
                int madv = Convert.ToInt32(grvthuochuhao.GetRowCellValue(i, colmadv1));
                if(madv>0)
                {
                    string tendv = Convert.ToString(grvthuochuhao.GetRowCellValue(i, coltendv1));
                    double dongia = Convert.ToDouble(grvthuochuhao.GetRowCellValue(i, coldongia1));
                    double soluongx = Convert.ToDouble(grvthuochuhao.GetRowCellValue(i, colsluonghuhao));
                    double thanhtienx = dongia * soluongx;
                    var donvi = _data.DichVus.Where(p => p.MaDV == madv).Select(p => p.DonVi).FirstOrDefault();
                    NhapDct moi1 =new NhapDct();
                    moi1.IDNhap = _idnhap;
                    moi1.MaDV = madv;
                    moi1.DonGiaX = dongia;
                    moi1.SoLuongX = soluongx;
                    moi1.DonVi = donvi;
                    moi1.DonGia = dongia;
                    moi1.ThanhTienX = thanhtienx;
                    _data.NhapDcts.Add(moi1);
                    _data.SaveChanges();
                }
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(lopKhoaP.EditValue!=null)
            {
                int _makp = Convert.ToInt32(lopKhoaP.EditValue);
                LuuHuHao(_makp);
                MessageBox.Show("Tạo hư hao thành công", "Thông báo", MessageBoxButtons.OK);
                grcthuocton.DataSource = "";
                grcthuochuhao.DataSource = "";
            }
            else { MessageBox.Show("chưa chọn khoa phòng", "Thông báo", MessageBoxButtons.OK); }
        }

        private void grcthuochuhao_Click(object sender, EventArgs e)
        {

        }

    }
}