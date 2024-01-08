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
    public partial class Frm_ThVPhi_TU_TTTU_BG : DevExpress.XtraEditors.XtraForm
    {
        public Frm_ThVPhi_TU_TTTU_BG()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private bool KTtaoBc()
        {
            if (dateTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                dateTuNgay.Focus();
                return false;
            }
            if (dateDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                dateDenNgay.Focus();
                return false;
            }

            else return true;
        }
        List<KPhong> _lKPhong = new List<KPhong>();
   
        private void Frm_ThVPhi_TU_TTTU_BG_Load(object sender, EventArgs e)
        {
            dateTuNgay.DateTime = System.DateTime.Now;
            dateDenNgay.DateTime = System.DateTime.Now;
            dateTuNgay.Focus();
           
            DataTable tbDoiTuong = new DataTable();
            tbDoiTuong.Columns.Add("TenDoiTuong", typeof(string));
            tbDoiTuong.Columns.Add("MaDoiTuong", typeof(string));
            tbDoiTuong.Rows.Add("Tất cả", "tc");
            tbDoiTuong.Rows.Add("BHYT", "BHYT");
            tbDoiTuong.Rows.Add("Dịch vụ", "Dịch vụ");
            tbDoiTuong.Rows.Add("KSK", "KSK");
            lupDoiTuong.Properties.DataSource = tbDoiTuong;
            _lKPhong = data.KPhongs.OrderBy(p => p.TenKP).ToList();
            _lKPhong.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
            radBN.SelectedIndex = 0;
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;


            if (KTtaoBc())
            {

                tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
                denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;

                frmIn frm = new frmIn();
                BaoCao.Rep_ThVPhi_TU_TTTU_BG rep = new BaoCao.Rep_ThVPhi_TU_TTTU_BG();
                rep.TuNgayDenNgay.Value = "Từ ngày " + dateTuNgay.Text + " Đến ngày " + dateDenNgay.Text;
                string _dt1="",_dt2="",_dt3="";
                if (lupDoiTuong.EditValue.ToString() != null)
                {
                    if (lupDoiTuong.EditValue.ToString() == "tc")
                    { _dt1 = "BHYT"; _dt2 = "Dịch vụ"; _dt3 = "KSK"; }
                    if (lupDoiTuong.EditValue.ToString() == "BHYT")
                    { _dt1 = "BHYT"; _dt2 = ""; _dt3 = ""; }

                    if (lupDoiTuong.EditValue.ToString() == "Dịch vụ")
                    { _dt1 = ""; _dt2 = "Dịch vụ"; _dt3 = ""; }

                    if (lupDoiTuong.EditValue.ToString() == "KSK")
                    { _dt1 = ""; _dt2 = ""; _dt3 = "KSK"; }
                   
                }
                
                int _nt = -1; int _ngt = -1;
                if (radBN.SelectedIndex == 0) { _nt = 1; _ngt = 0; }
                if (radBN.SelectedIndex == 1) { _nt = 1; _ngt = -1; }
                if (radBN.SelectedIndex == 2) { _nt = -1; _ngt = 0; }

              
                var qtu = (from bn in data.BenhNhans.Where(p => p.NoiTru == _nt || p.NoiTru == _ngt)
                           join t in data.TamUngs
                           .Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay) on bn.MaBNhan equals t.MaBNhan
                           select new {bn.MaBNhan,bn.DTuong, t.NgayThu, t.PhanLoai, t.SoTien }).ToList().Select(p => new {p.MaBNhan,p.DTuong, NgayThu=p.NgayThu,p.PhanLoai,p.SoTien}).ToList();
                var q = (from tt in qtu.Where(p => p.PhanLoai == 2 || p.PhanLoai == 1)
                         join tu in data.TamUngs.Where(p => p.PhanLoai == 0) on tt.MaBNhan equals tu.MaBNhan
                         select tu).ToList();

                var VP = (from tu in qtu.Where(p => p.DTuong == _dt1 || p.DTuong == _dt2 || p.DTuong == _dt3)
                          group new { tu } by new { NgayTT = tu.NgayThu.Value.Date, tu.MaBNhan, tu.PhanLoai } into kq
                          select new
                          {
                              NgayTT = kq.Key.NgayTT,
                              TienVP = kq.Where(p => p.tu.PhanLoai == 1 || p.tu.PhanLoai == 3).Sum(p => p.tu.SoTien) == null ? 0 : kq.Where(p => p.tu.PhanLoai == 1 || p.tu.PhanLoai == 3).Sum(p => p.tu.SoTien),
                              TienThu = kq.Where(p => p.tu.PhanLoai == 0).Sum(p => p.tu.SoTien) == null ? 0 : kq.Where(p => p.tu.PhanLoai == 0).Sum(p => p.tu.SoTien),
                              TienChi = kq.Key.PhanLoai == 4 ? (from k in q
                                         where k.MaBNhan == kq.Key.MaBNhan
                                         select k.SoTien).Sum() : 0,
                          }).ToList().Select(p => new
                          {
                              NTN = p.NgayTT,
                              VP = p.TienVP == 0 ? null : p.TienVP,
                              TU = p.TienThu == 0 ? null : p.TienThu,
                              TT = p.TienChi == 0 ? null : p.TienChi,
                              CL = p.TienVP + p.TienThu - p.TienChi == 0 ? null : p.TienVP + p.TienThu - p.TienChi,
                          }).ToList().OrderBy(p => p.NTN).Where(p => p.VP > 0 || p.TU > 0 || p.TT > 0 || p.CL > 0).ToList();
              
               
                rep.DataSource = VP;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
           
        }
       
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}