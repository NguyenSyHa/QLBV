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
    public partial class frm_TamUngVPHangNgay_30005 : DevExpress.XtraEditors.XtraForm
    {
        public frm_TamUngVPHangNgay_30005()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_TamUngVP_30005_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            lupNgaytu.EditValue = System.DateTime.Now.Date;
            lupngayden.EditValue = System.DateTime.Now.Date;
            lupNgaytu.Focus();
            List<DTBN> _lDTBN = new List<DTBN>();
            _lDTBN = data.DTBNs.Where(p => p.Status == 1).ToList();
            _lDTBN.Insert(0, new DTBN { IDDTBN = 100, DTBN1 = "Tất cả" });
            lupDoituong.Properties.DataSource = _lDTBN;
            lupDoituong.EditValue = lupDoituong.Properties.GetKeyValueByDisplayText("Tất cả");
          
            radio_noitru.SelectedIndex = 2;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            //Nội ngoại trú
            int noitru = -1;
            if (radio_noitru.SelectedIndex != null)
                noitru = radio_noitru.SelectedIndex;

            //đối tượng bệnh nhân
            int dtbn = -1;
            if (lupDoituong.EditValue != null)
                dtbn = Convert.ToInt32(lupDoituong.EditValue);

            //Thời gian
            DateTime tungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupngayden.DateTime);

            //Chi phí trong ngoài danh mục
           

            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            var qbn = (from bn in data.BenhNhans.Where(p => p.TuyenDuoi == 0).Where(p => dtbn == 100 || p.IDDTBN == dtbn).Where(p => noitru == 2 || p.NoiTru == noitru)
                       join dt in data.DTBNs on bn.IDDTBN equals dt.IDDTBN                    
                    
                      select new { bn.MaBNhan, bn.TenBNhan, bn.Tuoi, bn.DChi, bn.NoiTru, dt.DTBN1}).ToList();

                #region Tạm ứng
            var qtamung = (from tu in data.TamUngs.Where(p=>p.PhanLoai == 0).Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay)
                         
                               select new {
                                   tu.MaBNhan,tu.NgayThu,tu.MaKP,tu.SoHD, tu.SoTien,                           
                               }).ToList();
            var qkp = data.KPhongs.ToList(); 
                var q1 = (from tu in qtamung 
                          join bn in qbn on tu.MaBNhan equals bn.MaBNhan  
                          join kp in qkp on tu.MaKP equals kp.MaKP
                          select new {NgayThu =  tu.NgayThu.Value.Date,tu.MaKP,tu.SoTien, tu.SoHD, tu.MaBNhan,bn.DTBN1, bn.TenBNhan, bn.Tuoi, bn.DChi, bn.NoiTru, kp.TenKP}).ToList();                          

                #endregion
            

            #region Viện phí
            var q2 = (from bn in q1
                      group bn by new { bn.TenBNhan, bn.Tuoi, bn.DChi, bn.MaBNhan, bn.NgayThu, bn.TenKP, bn.SoHD, bn.DTBN1} into kq
                      select new
                      {
                          kq.Key.TenBNhan,
                          kq.Key.Tuoi,
                          kq.Key.DChi,
                          kq.Key.MaBNhan,
                          kq.Key.NgayThu,
                          kq.Key.TenKP,
                          kq.Key.SoHD,
                          vphi = kq.Key.DTBN1 == "BHYT" ? 0 : kq.Sum(p=>p.SoTien),
                          Bhiem = kq.Key.DTBN1 == "BHYT" ? kq.Sum(p => p.SoTien): 0,
                          Tong = kq.Sum(p => p.SoTien)
                      }).OrderBy(p => p.NgayThu).ToList();//.Where(p=>p.Tong != 0).ToList();
            if (q2.Count > 0)
            {
                BaoCao.rep_TamUngVPHangNgay_30005 rep = new BaoCao.rep_TamUngVPHangNgay_30005();
                frmIn frm = new frmIn();

                rep.DataSource = q2;
                rep.colCQ.Text = DungChung.Bien.TenCQ.ToUpper();
                rep.cel_diadanh.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                rep.celNgayThang.Text = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy");
                if (String.IsNullOrEmpty(txtTieude.Text))
                {
                    rep.CelTieuDe.Text = "BẢNG KÊ TẠM ỨNG VIỆN PHÍ";
                }
                else
                { rep.CelTieuDe.Text = txtTieude.Text.ToUpper(); }

                if (!String.IsNullOrEmpty(txtNgayThang.Text))
                    rep.celNgayThang.Text = txtNgayThang.Text;
                rep.cel_NguoiLap.Text = DungChung.Bien.NguoiLapBieu;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
                MessageBox.Show("Không có dữ liệu");
            #endregion


        }
    }
}