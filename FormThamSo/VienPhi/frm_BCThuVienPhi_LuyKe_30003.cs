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
    public partial class frm_BCThuVienPhi_LuyKe_30003 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCThuVienPhi_LuyKe_30003()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string fomat = "{0:N0}";

            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //tháng tìm kiếm
            DateTime tungayT = new DateTime(Convert.ToInt32(cbNam.SelectedValue), Convert.ToInt16(cboThang.SelectedValue), 1);
           
            DateTime denngayT = DungChung.Ham.NgayDen(tungayT.AddMonths(1).AddDays(-1));
            //Các tháng trước
            DateTime tungayDK = new DateTime(Convert.ToInt32(cbNam.SelectedValue), 1, 1);
            DateTime denngayDK = DungChung.Ham.NgayDen(tungayT.AddDays(-1));

            int trongNgoaiDM = -1;
            if (ckTrongNgoaiBH.GetItemCheckState(0) == CheckState.Checked && ckTrongNgoaiBH.GetItemCheckState(1) == CheckState.Checked)
                trongNgoaiDM = -1;
            else if (ckTrongNgoaiBH.GetItemCheckState(0) == CheckState.Checked)
                trongNgoaiDM = 0;
            else if (ckTrongNgoaiBH.GetItemCheckState(1) == CheckState.Checked)
                trongNgoaiDM = 1;
            int idDTBN = -1;
            if (lupDoituong.EditValue != null)
                idDTBN = Convert.ToInt32(lupDoituong.EditValue);
            bool CPBN = false, CPBH = false, CPMien = false;
            if (ckListCP.GetItemCheckState(0) == CheckState.Checked)
                CPBN = true;
            if (ckListCP.GetItemCheckState(1) == CheckState.Checked)
                CPBH = true;
            if (ckListCP.GetItemCheckState(2) == CheckState.Checked)
                CPMien = true;


            var qdv = (from dv in data.DichVus
                       join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                       join n in data.NhomDVs on tn.IDNhom equals n.IDNhom
                       select new { dv.MaDV, dv.TenDV, tn.TenRG, tn.IdTieuNhom, n.TenNhomCT, n.IDNhom }).ToList();

            var qvp = (from vpct in data.VienPhicts.Where(p=> trongNgoaiDM == -1 || p.TrongBH == trongNgoaiDM)
                       join vp in data.VienPhis.Where(p => p.NgayTT >= tungayDK && p.NgayTT <= denngayT) on vpct.idVPhi equals vp.idVPhi
                       join kp in data.KPhongs//.Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám") 
                       on vpct.MaKP equals kp.MaKP
                       select new
                       {
                           MaBN = vp.MaBNhan,
                           NgayTT = vp.NgayTT,
                           MaKP = vpct.MaKP,
                           TenKP = kp.TenKP,
                           kp.PLoai,
                           TienBN = vpct.TienBN,
                           TienBH = vpct.TienBH,
                           ThanhTien = vpct.ThanhTien,
                           vpct.Mien,
                           vpct.MaDV,
                       }).ToList();

            var qMaBNhan = (from bn in data.BenhNhans
                            where bn.IDDTBN == idDTBN
                            select new { bn.MaBNhan, bn.NoiTru }).ToList();
            var q1 = (from vp in qvp
                      join bn in qMaBNhan on vp.MaBN equals bn.MaBNhan
                      join dv in qdv on vp.MaDV equals dv.MaDV
                      //where (bn.NoiTru == 0) || (bn.NoiTru == 1 && vp.PLoai == "Lâm sàng" )
                      select new
                      {
                          vp.NgayTT,
                          vp.MaKP,
                          vp.TenKP,
                          vp.TienBN,
                          vp.TienBH,
                          vp.ThanhTien,
                          bn.MaBNhan,
                          bn.NoiTru,
                          dv.MaDV,
                          dv.TenDV,
                          dv.TenNhomCT,
                          dv.TenRG,
                          vp.PLoai
                      }).ToList();

            var q2 = (from vp in q1
                      select new
                      {
                          vp.NgayTT,
                          vp.MaKP,
                          vp.TenKP,
                          vp.PLoai,
                          vp.TienBN,
                          vp.TienBH,
                          vp.ThanhTien,
                          vp.MaBNhan,
                          vp.NoiTru,
                          vp.MaDV,
                          vp.TenDV,
                          vp.TenNhomCT,
                          vp.TenRG,
                          Cphi = (CPBN ? vp.TienBN : 0) + (CPBH ? vp.TienBH : 0) + (CPMien ? (vp.ThanhTien - vp.TienBN - vp.TienBH) : 0)
                      }).ToList();

            var q3 = (from vp in q2
                      select new
                      {
                          vp.MaBNhan,
                          vp.MaKP,
                          vp.NgayTT,
                          vp.PLoai,
                          TenKhoa = vp.PLoai == "Lâm sàng" ? vp.TenKP : "Ngoại trú",
                          TenKPhongCt = vp.PLoai == "Lâm sàng" ? "" : vp.TenKP,
                          Mau = vp.TenNhomCT == "Máu và chế phẩm của máu" ? vp.Cphi : 0,
                          ThuocDY = vp.TenRG == "Thuốc đông y" ? vp.Cphi : 0,
                          ThuocDich = (vp.TenRG != "Thuốc đông y" && vp.TenNhomCT.Contains("Thuốc")) ? vp.Cphi : 0,
                          VTYT = (vp.TenNhomCT == "Vật tư y tế trong danh mục BHYT" || vp.TenNhomCT == "VTYT thanh toán theo tỷ lệ") ? vp.Cphi : 0,
                          TienGiuong = vp.TenNhomCT.Contains("Giường") ? vp.Cphi : 0,
                          KhamBenh = (vp.TenNhomCT == "Khám bệnh" && vp.TenRG != "KSK") ? vp.Cphi : 0,
                          XetNghiem = vp.TenNhomCT == "Xét nghiệm" ? vp.Cphi : 0,
                          KSK = vp.TenRG == "KSK" ? vp.Cphi : 0,
                          SA = vp.TenRG == "Siêu âm" ? vp.Cphi : 0,
                          DienTim = vp.TenRG == "Điện tim" ? vp.Cphi : 0,
                          TTPT = vp.TenNhomCT == "Thủ thuật, phẫu thuật" ? vp.Cphi : 0,
                          Tong = vp.Cphi,

                      }).ToList();

            var q4 = (from vp in q3
                      group vp by new { vp.MaKP, vp.TenKhoa, vp.TenKPhongCt, vp.PLoai } into kq
                      select new
                          {
                              kq.Key.MaKP,
                              kq.Key.TenKhoa,
                              kq.Key.TenKPhongCt,
                              STT = kq.Key.PLoai == "Lâm sàng" ? 1 : 2,
                              Luot = kq.Where(p => p.NgayTT >= tungayT).Select(p => p.MaBNhan).Distinct().Count(),
                              Mau = kq.Where(p => p.NgayTT >= tungayT).Sum(p => p.Mau),
                              ThuocDY = kq.Where(p => p.NgayTT >= tungayT).Sum(p => p.ThuocDY),
                              ThuocDich = kq.Where(p => p.NgayTT >= tungayT).Sum(p => p.ThuocDich),
                              VTYT = kq.Where(p => p.NgayTT >= tungayT).Sum(p => p.VTYT),
                              TienGiuong = kq.Where(p => p.NgayTT >= tungayT).Sum(p => p.TienGiuong),
                              KhamBenh = kq.Where(p => p.NgayTT >= tungayT).Sum(p => p.KhamBenh),
                              XetNghiem = kq.Where(p => p.NgayTT >= tungayT).Sum(p => p.XetNghiem),
                              KSK = kq.Where(p => p.NgayTT >= tungayT).Sum(p => p.KSK),
                              SA = kq.Where(p => p.NgayTT >= tungayT).Sum(p => p.SA),
                              DienTim = kq.Where(p => p.NgayTT >= tungayT).Sum(p => p.DienTim),
                              TTPT = kq.Where(p => p.NgayTT >= tungayT).Sum(p => p.TTPT),
                              Tong = kq.Where(p => p.NgayTT >= tungayT).Sum(p => p.Tong),
                          }
                ).ToList();

            var q5 = (from vp in q4                      
                      select new
                      {

                          vp.MaKP,
                          vp.TenKhoa,
                          vp.TenKPhongCt,
                          vp.STT,
                          vp.Luot,
                          vp.Mau,
                          vp.ThuocDY,
                          vp.ThuocDich,
                          vp.VTYT,
                          vp.TienGiuong,
                          vp.KhamBenh,
                          vp.XetNghiem,
                          vp.KSK,
                          vp.SA,
                          vp.DienTim,
                          vp.TTPT,
                          vp.Tong,
                          Khac = vp.Tong - vp.Mau - vp.ThuocDY - vp.ThuocDich  - vp.VTYT - vp.TienGiuong - vp.KhamBenh - vp.XetNghiem - vp.KSK - vp.SA - vp.DienTim - vp.TTPT
                      }
                ).OrderBy(p => p.STT).ThenBy(p => p.TenKPhongCt).ToList();

           
                     
            frmIn frm = new frmIn();
            BaoCao.rep_BCThuVienPhi_LuyKe_30003 rep = new BaoCao.rep_BCThuVienPhi_LuyKe_30003(ckHTCTPhongKham.Checked);


            // các tháng trước
            var qttr = q3.Where(p => p.NgayTT < tungayT);

            int t1 = 0;
            double t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0, t7 = 0, t8 = 0, t9 = 0, t10 = 0, t11 = 0, t12 = 0, t13 = 0, t14 = 0;


            

            rep.celTrongdo.Text = txtTrongdo.Text;
            rep.lab_tungaydenngay.Text = ("Tháng " + cboThang.Text + " năm " + cbNam.Text).ToUpper();
            rep.celCongThangTitle.Text = "Cộng T" + cboThang.Text;
            #region hiển thị dữ liệu các tháng trước
            if (Convert.ToInt32(cboThang.Text) > 1)
            {
                rep.celCacThangTruoc_Title.Text = (Convert.ToInt32(cboThang.Text) - 1) + " T MS";
                t1 = qttr.Select(p => p.MaBNhan).Distinct().Count();
                t2 = qttr.Sum(p => p.Mau);
                t3 = qttr.Sum(p => p.ThuocDY);
                t4 = qttr.Sum(p => p.ThuocDich);
                t5 = qttr.Sum(p => p.VTYT);
                t6 = qttr.Sum(p => p.TienGiuong);
                t7 = qttr.Sum(p => p.KhamBenh);
                t8 = qttr.Sum(p => p.XetNghiem);
                t9 = qttr.Sum(p => p.KSK);
                t10 = qttr.Sum(p => p.SA);
                t11 = qttr.Sum(p => p.DienTim);
                t12 = qttr.Sum(p => p.TTPT);
                t14 = qttr.Sum(p => p.Tong);
                t13 = t14 - t12 - t11 - t10 - t9 - t8 - t7 - t6 - t5 - t4 - t3 - t2;

                rep.celTr1.Text = String.Format(fomat, t1);
                rep.celTr2.Text = String.Format(fomat, t2);
                rep.celTr3.Text = String.Format(fomat, t3);
                rep.celTr4.Text = String.Format(fomat, t4);
                rep.celTr5.Text = String.Format(fomat, t5);
                rep.celTr6.Text = String.Format(fomat, t6);
                rep.celTr7.Text = String.Format(fomat, t7);
                rep.celTr8.Text = String.Format(fomat, t8);
                rep.celTr9.Text = String.Format(fomat, t9);
                rep.celTr10.Text = String.Format(fomat, t10);
                rep.celTr11.Text = String.Format(fomat, t11);
                rep.celTr12.Text = String.Format(fomat, t12);
                rep.celTr13.Text = String.Format(fomat, t13);
                rep.celTr14.Text = String.Format(fomat, t14);
            }
            #endregion
            #region hiển thị lũy kế
            t1 = q3.Select(p => p.MaBNhan).Distinct().Count();
            t2 = q3.Sum(p => p.Mau);
            t3 = q3.Sum(p => p.ThuocDY);
            t4 = q3.Sum(p => p.ThuocDich);
            t5 = q3.Sum(p => p.VTYT);
            t6 = q3.Sum(p => p.TienGiuong);
            t7 = q3.Sum(p => p.KhamBenh);
            t8 = q3.Sum(p => p.XetNghiem);
            t9 = q3.Sum(p => p.KSK);
            t10 = q3.Sum(p => p.SA);
            t11 = q3.Sum(p => p.DienTim);
            t12 = q3.Sum(p => p.TTPT);
            t14 = q3.Sum(p => p.Tong);
            t13 = t14 - t12 - t11 - t10 - t9 - t8 - t7 - t6 - t5 - t4 - t3 - t2;

            rep.celLK1.Text = String.Format(fomat, t1);
            rep.celLK2.Text = String.Format(fomat, t2);
            rep.celLK3.Text = String.Format(fomat, t3);
            rep.celLK4.Text = String.Format(fomat, t4);
            rep.celLK5.Text = String.Format(fomat, t5);
            rep.celLK6.Text = String.Format(fomat, t6);
            rep.celLK7.Text = String.Format(fomat, t7);
            rep.celLK8.Text = String.Format(fomat, t8);
            rep.celLK9.Text = String.Format(fomat, t9);
            rep.celLK10.Text = String.Format(fomat, t10);
            rep.celLK11.Text = String.Format(fomat, t11);
            rep.celLK12.Text = String.Format(fomat, t12);
            rep.celLK13.Text = String.Format(fomat, t13);
            rep.celLK14.Text = String.Format(fomat, t14);
            #endregion
            rep.lab_Tieude.Text = "BÁO CÁO CHI TIẾT THU VIỆN PHÍ ĐỐI TƯỢNG BỆNH NHÂN " + lupDoituong.Text.ToUpper();
           
            double st = q5.Sum(p => p.Tong);
            rep.celTienBangChu.Text = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");                 
            rep.celNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
            rep.celGD.Text = DungChung.Bien.GiamDoc;
            rep.DataSource = q5;
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
        public class MyObject
        {
            public int Value { set; get; }
        }
        private void frm_BCThuVienPhi_LuyKe_30003_Load(object sender, EventArgs e)
        {
            int namHT = DateTime.Now.Year;
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<MyObject> _list = new List<MyObject>();
            List<MyObject> _listthang = new List<MyObject>();
            for (int i = namHT - 10; i < namHT + 11; i++)
            {
                MyObject obj = new MyObject();
                obj.Value = i;
                _list.Add(obj);
            }

            for (int i = 1; i < 13; i++)
            {
                MyObject obj = new MyObject();
                obj.Value = i;
                _listthang.Add(obj);
            }
            cbNam.DisplayMember = "Value";
            cbNam.ValueMember = "Value";
            cbNam.DataSource = _list;
            cbNam.SelectedValue = namHT;

            cboThang.DisplayMember = "Value";
            cboThang.ValueMember = "Value";
            cboThang.DataSource = _listthang;
            cboThang.SelectedValue = DateTime.Now.Month - 1;

            ckListCP.SetItemChecked(0, true);

            ckTrongNgoaiBH.SetItemChecked(0, true);
            ckTrongNgoaiBH.SetItemChecked(1, true);

            List<DTBN> _lDTBN = new List<DTBN>();
            _lDTBN = data.DTBNs.Where(p => p.Status == 1).ToList();
            lupDoituong.Properties.DataSource = _lDTBN;
            lupDoituong.Properties.DisplayMember = "DTBN1";
            lupDoituong.Properties.ValueMember = "IDDTBN";
            lupDoituong.EditValue = lupDoituong.Properties.GetKeyValueByDisplayText("BHYT");
      
           
        }

       
    }
}