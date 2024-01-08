using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.ChucNang
{
    public partial class frmCanhBaoThuoc : DevExpress.XtraEditors.XtraForm
    {
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<TonKho> lstNgayThuoc = new List<TonKho>();
        public frmCanhBaoThuoc()
        {
            InitializeComponent();
        }

        private void frmCanhBaoThuoc_Load(object sender, EventArgs e)
        {
            LoadCombo();
            lstNgayThuoc = GetTonKho();
            cboMucLuaChonHSD.EditValue = 2;
            cboMucLuaChonSL.EditValue = 1;
        }

        public class TonKho
        {
            public int madv { get; set; }
            public string tenthuoc { get; set; }
            public string ngaynhap { get; set; }
            public string handung { get; set; }
            public DateTime? ngayht { get; set; }
            public double slton { get; set; }
            public int slmin { get; set; }
            public double baosl { get; set; }
            public int baohd { get; set; }
            public string baohdtext { get; set; }
            public double dongia { get; set; }
        }

        //type=1: grid HSD
        //type=2: grid SL
        private void FillDataToGrid(int type, int mucDo)
        {
            //var kt4 = lstNgayThuoc.Where(o => o.madv == 4343).ToList();
            switch (type)
            {
                case 1:
                    {
                        switch (mucDo)
                        {
                            case 1:
                                {
                                    var dataSource = lstNgayThuoc.Where(o => o.baohd == 0).OrderBy(o => o.baohd).ToList();
                                    gridControlThuocHSD.DataSource = dataSource;
                                }
                                break;
                            case 2:
                                {
                                    var dataSource = lstNgayThuoc.Where(o => o.baohd <= 180 && o.baohd > 0).OrderBy(o => o.baohd).ToList();
                                    gridControlThuocHSD.DataSource = dataSource;
                                }
                                break;
                            case 3:
                                {
                                    var dataSource = lstNgayThuoc.Where(o => o.baohd <= 90 && o.baohd > 0).OrderBy(o => o.baohd).ToList();
                                    gridControlThuocHSD.DataSource = dataSource;
                                }
                                break;
                        }
                    }
                    break;
                case 2:
                    {
                        switch (mucDo)
                        {
                            case 1:
                                {
                                    var dataSource = lstNgayThuoc.Where(o => o.baosl >= 180 && o.baosl < 200).OrderBy(o => o.baosl).ToList();
                                    gridControlThuocSL.DataSource = dataSource;
                                }
                                break;
                            case 2:
                                {
                                    var dataSource = lstNgayThuoc.Where(o => o.baosl >= 140 && o.baosl < 200).OrderBy(o => o.baosl).ToList();
                                    gridControlThuocSL.DataSource = dataSource;
                                }
                                break;
                        }
                    }
                    break;
            }
        }

        private List<TonKho> GetTonKho()
        {
            var lstTon11 = (from nd in dataContext.NhapDs
                            join ndct in dataContext.NhapDcts on nd.IDNhap equals ndct.IDNhap
                            where ((nd.PLoai == 1 || nd.PLoai == 2))
                            select new
                            {
                                ndct.SoLo,
                                ndct.MaDV,
                                nd.NgayNhap,
                                ndct.HanDung,
                                makho = nd.MaKP,
                                slnhap = nd.PLoai == 1 ? ndct.SoLuongN : 0,
                                slxuat = nd.PLoai == 2 ? ndct.SoLuongX : 0,
                                nd.PLoai,
                                ndct.DonGia
                            }).ToList();
            //var kt1 = lstTon11.Where(o => o.MaDV == 4343).ToList();
            //var ax = lstTon11.Where(o => o.MaDV == 5208).ToList();


            var lstTon1 = (from ton in lstTon11
                           group ton by new { ton.MaDV, ton.DonGia } into kq
                           select new
                           {
                               kq.Key.DonGia,
                               kq.Key.MaDV,
                               NgayNhap = kq.Where(p => p.PLoai == 1).Min(p => p.NgayNhap),
                               HanDung = kq.Where(p => p.HanDung != null).Min(p => p.HanDung),
                               SumSLNhap = kq.Sum(p => p.slnhap),
                               SumSLXuat = kq.Sum(p => p.slxuat),
                               ton = kq.Sum(p => p.slnhap) - kq.Sum(p => p.slxuat)
                           }).ToList();
            //var kt2 = lstTon1.Where(o => o.MaDV == 4343).ToList();
            //var a = lstTon1.Where(o => o.MaDV == 5208).ToList();

            var lstThuoc = (from dv in dataContext.DichVus
                            where (dv.PLoai == 1)
                            select new
                            {
                                dv.MaDV,
                                dv.TenDV,
                                dv.SLMin
                            }).ToList();
            //var kt3 = lstThuoc.Where(o => o.MaDV == 4343).ToList();
            DateTime hdmax = new DateTime();

            var _lstNgayThuoc = (from lngay in lstTon1
                                 join thuoc in lstThuoc on lngay.MaDV equals thuoc.MaDV
                                 select new
                                 {
                                     madv = thuoc.MaDV,
                                     tenthuoc = thuoc.TenDV,
                                     ngaynhap = lngay.NgayNhap == null ? hdmax : lngay.NgayNhap.Value,
                                     handung = lngay.HanDung == null ? hdmax : lngay.HanDung.Value,
                                     ngayht = DateTime.Now,
                                     slton = lngay.ton,
                                     slmin = thuoc.SLMin == null ? 0 : thuoc.SLMin.Value,
                                     // baosl = lngay.ton==null?0:lngay.ton-thuoc.SLMin.Value,
                                     baohd = lngay.HanDung == null ? 0 : ((lngay.HanDung.Value - DateTime.Now).Days + 1),
                                     dongia = lngay.DonGia
                                 }).Distinct().ToList();

            var lstNgayThuoc = (from ngaythuoc in _lstNgayThuoc
                                select new TonKho
                                {
                                    madv = ngaythuoc.madv,
                                    tenthuoc = ngaythuoc.tenthuoc,
                                    ngaynhap = ngaythuoc.ngaynhap == hdmax ? "" : ngaythuoc.ngaynhap.ToString("dd/MM/yyyy"),
                                    handung = ngaythuoc.handung == hdmax ? "" : ngaythuoc.handung.ToString("dd/MM/yyyy"),
                                    ngayht = ngaythuoc.ngayht,
                                    slton = ngaythuoc.slton,
                                    slmin = ngaythuoc.slmin,
                                    baosl = ngaythuoc.slton - ngaythuoc.slmin == 0 ? 0 : (ngaythuoc.slton - ngaythuoc.slmin),
                                    baohd = ngaythuoc.baohd <= 0 ? 0 : ngaythuoc.baohd,
                                    baohdtext = ngaythuoc.baohd <= 0 ? "0 ngày" : CalculateDay(ngaythuoc.baohd),
                                    dongia = ngaythuoc.dongia
                                }).ToList();
            return lstNgayThuoc;
        }

        private string CalculateDay(int day)
        {
            string rs = "";

            var ngay = (day % 30).ToString();
            var thang = (day / 30).ToString();

            if (thang != "0")
                rs = thang + " tháng " + ngay + " ngày";
            else
                rs = ngay + " ngày";

            return rs;
        }

        private void LoadCombo()
        {
            List<ComboADO> listHSD = new List<ComboADO>();
            listHSD.Add(new ComboADO(1, "Đã hết hạn"));
            listHSD.Add(new ComboADO(2, "Hạn dưới 6 tháng"));
            listHSD.Add(new ComboADO(3, "Hạn dưới 3 tháng"));
            List<ComboADO> listSL = new List<ComboADO>();
            listSL.Add(new ComboADO(1, "180 - 200"));
            listSL.Add(new ComboADO(2, "140 - 200"));

            FillDataToCombo(cboMucLuaChonHSD, listHSD);
            FillDataToCombo(cboMucLuaChonSL, listSL);
        }

        private void FillDataToCombo(GridLookUpEdit cbo, List<ComboADO> dataSource)
        {
            cbo.Properties.View.OptionsView.ShowColumnHeaders = false;
            cbo.Properties.DisplayMember = "Display";
            cbo.Properties.ValueMember = "Id";
            cbo.Properties.DataSource = dataSource;
        }

        public class ComboADO
        {
            public ComboADO(int id, string display)
            {
                this.Id = id;
                this.Display = display;
            }

            public int Id { get; set; }
            public string Display { get; set; }
        }

        private void cboMucLuaChonHSD_EditValueChanged(object sender, EventArgs e)
        {
            FillDataToGrid(1, Int32.Parse(cboMucLuaChonHSD.EditValue.ToString()));
        }

        private void cboMucLuaChonSL_EditValueChanged(object sender, EventArgs e)
        {
            FillDataToGrid(2, Int32.Parse(cboMucLuaChonSL.EditValue.ToString()));
        }

        private void frmCanhBaoThuoc_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
