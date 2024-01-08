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
    public partial class frm_TKNhapCTLoaiThuoc : DevExpress.XtraEditors.XtraForm
    {
        public frm_TKNhapCTLoaiThuoc()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data;
        List<DichVu> _LDv = new List<DichVu>();
        List<NhaCC> _lNCC = new List<NhaCC>();
        bool checkLoad = false;
        private void frm_TKNhapCTLoaiThuoc_Load(object sender, EventArgs e)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var kho = (from k in _data.KPhongs
                      select k).Where( p=> p.PLoai == "Khoa dược").ToList();
            lupKho.Properties.DataSource = kho;
            _lNCC = _data.NhaCCs.OrderBy( p =>p.TenCC).ToList();
            _lNCC.Insert(0, new NhaCC { MaCC = "", TenCC = "Tất cả" });        
            lupNhaCC.Properties.DataSource = _lNCC;
            dateTuNgay.EditValue = DateTime.Now.AddMonths(-1);
            dateDenNgay.EditValue = DateTime.Now;
            loadTenThuoc("");
           
            checkLoad = true;
        }
     
        private void btnInBC_Click(object sender, EventArgs e)
        {
            if (checkBC())
            {                
                DateTime tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
                DateTime denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);
                int makho = lupKho.EditValue == null ? 0: Convert.ToInt32(lupKho.EditValue);
                string nhaCC = "";
                int tenthuoc = 0;
                if( lupNhaCC.EditValue != null)
                    nhaCC = lupNhaCC.EditValue.ToString();
                if (lupLoaiThuoc.EditValue != null)
                    tenthuoc = lupLoaiThuoc.EditValue == null ? 0 : Convert.ToInt32(lupLoaiThuoc.EditValue);
                var q = (from dct in _data.NhapDcts
                          join d in _data.NhapDs on dct.IDNhap equals d.IDNhap
                          join dv in _data.DichVus on dct.MaDV equals dv.MaDV
                          join ncc in _data.NhaCCs on dct.MaCC equals ncc.MaCC
                          join k in _data.KPhongs on d.MaKP equals k.MaKP
                          select new
                          {
                              d.IDNhap,
                              dct.MaDV,
                              dv.TenDV,
                              dv.DonVi,
                              dct.DonGia,
                              dct.DonGiaCT,
                              dct.SoLuongN,
                              dct.MaCC,
                              d.NgayNhap,
                              k.MaKP,
                              d.PLoai,
                              dct.SoLo,
                              dct.HanDung
                          }
                          ).Where(p => p.MaKP == makho).Where(p => nhaCC == "" ? true: p.MaCC == nhaCC).Where(p => tenthuoc == 0 ? true: p.MaDV == tenthuoc ).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Where( p=> p.PLoai == 1).ToList();
                var qg = (from m in q
                         group m by new
                         {
                            m.IDNhap, m.MaDV, m.TenDV,m.DonVi, m.DonGia, m.DonGiaCT, m.MaCC, m.NgayNhap,m.MaKP,m.SoLo,m.HanDung
                         } into kq
                         select new
                         {
                             MaDV = kq.Key.MaDV,
                             TenDV = kq.Key.TenDV,
                             DonVi = kq.Key.DonVi,
                             DonGia = kq.Key.DonGia,
                             DonGiaCT = kq.Key.DonGiaCT,
                             MaCC = kq.Key.MaCC,
                             NgayNhap = kq.Key.NgayNhap,
                             MaKP = kq.Key.MaKP,
                             SL = kq.Sum(p => p.SoLuongN),
                             SoLo = kq.Key.SoLo,
                             HanDung = kq.Key.HanDung
                         }).OrderBy(p => p.MaDV).ThenBy(p => p.NgayNhap).ToList();
                if (qg.Count > 0)
                {
                    if (DungChung.Bien.MaBV == "27023")
                    {
                        frmIn frm = new frmIn();
                        BaoCao.rep_TKNhapTungLoaiThuoc_27023 rep = new BaoCao.rep_TKNhapTungLoaiThuoc_27023();
                        rep.Title.Value = ("Thống kê nhập từng loại thuốc").ToUpper();
                        rep.Ngay.Value = "Từ ngày:  " + dateTuNgay.Text + " đến ngày:  " + dateDenNgay.Text;
                        rep.tenDonvi.Value = DungChung.Bien.TenCQ.ToUpper();
                        rep.TenKhoaP.Value = ("Khoa dược").ToUpper();
                        rep.TenKho.Value = lupKho.Text.ToUpper();
                        rep.DataSource = qg;
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        this.Hide();
                        frm.ShowDialog();
                        this.Show();
                    }
                    else
                    {
                        frmIn frm = new frmIn();
                        BaoCao.rep_TKNhapTungLoaiThuoc rep = new BaoCao.rep_TKNhapTungLoaiThuoc();
                        rep.Title.Value = ("Thống kê nhập từng loại thuốc").ToUpper();
                        rep.Ngay.Value = "Từ ngày:  " + dateTuNgay.Text + " đến ngày:  " + dateDenNgay.Text;
                        rep.tenDonvi.Value = DungChung.Bien.TenCQ.ToUpper();
                        rep.TenKhoaP.Value = ("Khoa dược").ToUpper();
                        rep.TenKho.Value = lupKho.Text.ToUpper();
                        rep.DataSource = qg;
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        this.Hide();
                        frm.ShowDialog();
                        this.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu ");
                }
            }
        }

        private bool checkBC()
        {
            if (dateTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                dateTuNgay.Focus();
                return false;
            }
            else if (dateDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                dateDenNgay.Focus();
                return false;
            }
            else if (lupKho.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn Kho phòng");
                lupKho.Focus();
                return false;
            }
            else
                return true;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void lupNhaCC_EditValueChanged(object sender, EventArgs e)
        {
            string nhaCC = "";
            if (lupNhaCC.EditValue != null)
                nhaCC = lupNhaCC.EditValue.ToString();
            loadTenThuoc(nhaCC);
        }

        private void loadTenThuoc(string nhaCC)
        {           
            _LDv = new List<DichVu>();
           var q = (from ndct in _data.NhapDcts
                    join dv in _data.DichVus on ndct.MaDV equals dv.MaDV
                    where (ndct.MaCC == nhaCC || nhaCC == "")                    
                    group dv by new { dv.MaDV, dv.TenDV } into kq
                    select new { kq.Key.MaDV, kq.Key.TenDV }).OrderBy(p => p.TenDV).ToList();
           foreach (var q1 in q)
           {
               _LDv.Add(new DichVu { TenDV = q1.TenDV, MaDV = q1.MaDV });
           }
            _LDv.Insert(0, new DichVu { TenDV = "Tất cả", MaDV = 0 });
            lupLoaiThuoc.Properties.DataSource = _LDv;
        }
        
       

    }
}