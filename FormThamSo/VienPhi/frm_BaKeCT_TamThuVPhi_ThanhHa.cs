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
    public partial class frm_BaKeCT_TamThuVPhi_ThanhHa : DevExpress.XtraEditors.XtraForm
    {
        public frm_BaKeCT_TamThuVPhi_ThanhHa()
        {
            InitializeComponent();
        }

       
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool KTtaoBc()
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

            else return true;
        }
        private void btnIn_Click(object sender, EventArgs e)
        {
            DateTime tungay = dateTuNgay.DateTime;
            DateTime denngay = dateDenNgay.DateTime;
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int idDTBN = -1;
            if (lupDTuong.EditValue != null)
                idDTBN = Convert.ToInt16(lupDTuong.EditValue);
            int Noitru = rdNNgoaiTru.SelectedIndex;
            int kp = 0;
            if (lupKPhong.EditValue != null)
                kp = Convert.ToInt32( lupKPhong.EditValue.ToString());

            if (KTtaoBc())
            {
                //tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
                //denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;

                frmIn frm = new frmIn();
                BaoCao.rep_BKeCT_TamThuVPhi_ThanhHa rep = new BaoCao.rep_BKeCT_TamThuVPhi_ThanhHa();
                rep.ngaythang.Value = "Từ ngày " + dateTuNgay.Text + " Đến ngày " + dateDenNgay.Text;
                rep.TieuDe.Value = ("Bảng kê chi tiết tạm thu viện phí").ToUpper();
                rep.TenCQCT.Value = DungChung.Bien.TenCQCQ.ToUpper();
                rep.TenCQCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                var qtu = (from tu in data.TamUngs
                           join bn in data.BenhNhans on tu.MaBNhan equals bn.MaBNhan
                           select new { bn.MaBNhan, bn.TenBNhan,bn.IDDTBN,bn.NoiTru, bn.DChi, tu.NgayThu, tu.IDTamUng, tu.PhanLoai, tu.SoTien, tu.LyDo,tu.MaKP, KPBN = bn.MaKP }).Where(p=>idDTBN == -1 || p.IDDTBN == idDTBN).Where(p=>p.NoiTru == Noitru).ToList();
                var q = (from k in _lKhoa.Where(p=>kp == 0 || p.MaKP == kp)
                         join tu in qtu.Where(p=>p.PhanLoai == 0)
                         on k.MaKP equals tu.MaKP
                         group new { tu, k } by new { tu.MaBNhan, tu.TenBNhan, tu.DChi, k.TenKP, tu.NgayThu, tu.IDTamUng, tu.PhanLoai, tu.LyDo, tu.SoTien } into kq
                         select new
                         {                             
                             TenBN = kq.Key.TenBNhan,
                             DChi = kq.Key.DChi,
                             TenKP = kq.Key.TenKP,
                             NgayThu = kq.Key.NgayThu,                             
                             PhanLoai = kq.Key.PhanLoai,
                             NoiDung = kq.Key.LyDo,
                             SoTien = kq.Key.SoTien

                         }).Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay).OrderBy(p => p.NgayThu).ThenBy(p => p.TenBN).Select(p => new
                         {                           
                             p.TenBN,
                             p.DChi,
                             NgayThu = p.NgayThu.Value.Date,
                             p.TenKP,
                             p.NoiDung,
                            p.SoTien

                         }).ToList();
                if (q.Count > 0)
                {
                    rep.DataSource = q;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu");
                }
            }  
        }

        List<KPhong> _lKhoa = new List<KPhong>();
        private void frm_BaKeCT_TamThuVPhi_ThanhHa_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            dateTuNgay.DateTime = System.DateTime.Today;
            DateTime tungay = dateTuNgay.DateTime.AddDays(1).AddHours(23).AddMinutes(59);
            dateDenNgay.DateTime = tungay;
            var q = (from tu in data.TamUngs
                          join kp in data.KPhongs on tu.MaKP equals kp.MaKP
                          group kp by new { kp.MaKP, kp.TenKP } into kq
                          select new  {TenKP = kq.Key.TenKP,MaKP = kq.Key.MaKP }).ToList();
            foreach (var kp in q)
            {
                KPhong khoa = new KPhong();
                khoa.MaKP = kp.MaKP;
                khoa.TenKP = kp.TenKP;
                _lKhoa.Add(khoa);
            }
            _lKhoa.Insert(0, new KPhong {MaKP = 0,TenKP = "Tất cả" });
            lupKPhong.Properties.DataSource = _lKhoa;
            lupKPhong.SelectedText = "Tất cả";
            List<DTBN> _lDTuong = new List<DTBN>();
            _lDTuong = data.DTBNs.Where(p=>p.Status ==1).ToList();
            lupDTuong.Properties.DataSource = _lDTuong;
            lupDTuong.SelectedText = "BHYT";
            rdNNgoaiTru.SelectedIndex = 1;
        }
    }
}