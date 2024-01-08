using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace QLBV.FormThamSo
{
    public partial class frm14 : DevExpress.XtraEditors.XtraForm
    {
        public frm14()
        {
            InitializeComponent();
        }
        int idThuoc = -1, idMau = -1, idXN = -1, idTTPT = -1, idCongKham = -1, idDVKTC = -1, idThuocUTCTG = -1,
idVTYT = -1, idNgayGiuongNoiTru = -1, idNgayGiuongNgoaiTru = -1, idChiPhiVC = -1, idVTTT = -1, idThuocTyLe = -1, idHoaChat = -1, idCDHA = -1, idTDCN = -1;
        private void setIDNhom()
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var tenNhom = _data.NhomDVs.ToList();
            foreach (var item in tenNhom)
            {
                switch (item.TenNhomCT)
                {
                    case "Xét nghiệm":
                        if (tenNhom.Where(a => a.TenNhomCT == "Xét nghiệm").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Xét nghiệm");
                        idXN = item.IDNhom;
                        if (idXN != 1)
                            MessageBox.Show("Nhóm xét nghiệm sai mã nhóm theo CV 9324");
                        break;
                    case "Chẩn đoán hình ảnh":
                        idCDHA = (item.IDNhom);

                        if (item.IDNhom != 2)
                            MessageBox.Show("Nhóm chẩn đoán hình ảnh sai mã nhóm theo CV 9324");
                        continue;
                    case "Thăm dò chức năng":
                        idTDCN = (item.IDNhom);
                        if (item.IDNhom != 3)
                            MessageBox.Show("Nhóm thăm dò chức năng sai mã nhóm theo CV 9324");
                        continue;
                    case "Thuốc trong danh mục BHYT":
                        idThuoc = item.IDNhom;
                        if (tenNhom.Where(a => a.TenNhomCT == "Thuốc trong danh mục BHYT").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Thuốc trong danh mục BHYT");
                        if (item.IDNhom != 4)
                            MessageBox.Show("Nhóm Thuốc trong danh mục BHYT sai mã nhóm theo CV 9324");
                        break;
                    case "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục":
                        if (tenNhom.Where(a => a.TenNhomCT == "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều Thuốc điều trị ung thư, chống thải ghép ngoài danh mục");
                        if (item.IDNhom != 5)
                            MessageBox.Show("Nhóm Thuốc điều trị ung thư, chống thải ghép ngoài danh mục sai mã nhóm theo CV 9324");
                        idThuocUTCTG = item.IDNhom;
                        break;
                    case "Thuốc thanh toán theo tỷ lệ":
                        if (tenNhom.Where(a => a.TenNhomCT == "Thuốc thanh toán theo tỷ lệ").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Thuốc thanh toán theo tỷ lệ");
                        if (item.IDNhom != 6)
                            MessageBox.Show("Nhóm Thuốc thanh toán theo tỷ lệ sai mã nhóm theo CV 9324");
                        idThuocTyLe = item.IDNhom;
                        break;
                    case "Máu và chế phẩm của máu":
                        if (tenNhom.Where(a => a.TenNhomCT == "Máu và chế phẩm của máu").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Nhóm máu và chế phẩm của máu");
                        if (item.IDNhom != 7)
                            MessageBox.Show("Nhóm Máu và chế phẩm của máu sai mã nhóm theo CV 9324");
                        idMau = item.IDNhom;
                        break;
                    case "Thủ thuật, phẫu thuật":
                        if (tenNhom.Where(a => a.TenNhomCT == "Thủ thuật, phẫu thuật").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Thủ thuật phẫu thuật");
                        if (item.IDNhom != 8)
                            MessageBox.Show("Nhóm Thủ thuật, phẫu thuật sai mã nhóm theo CV 9324");
                        idTTPT = item.IDNhom;
                        break;
                    case "DVKT thanh toán theo tỷ lệ":
                        if (tenNhom.Where(a => a.TenNhomCT == "DVKT thanh toán theo tỷ lệ").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm DVKT thanh toán theo tỷ lệ");
                        if (item.IDNhom != 9)
                            MessageBox.Show("Nhóm DVKT thanh toán theo tỷ lệ sai mã nhóm theo CV 9324");
                        idDVKTC = item.IDNhom;
                        break;
                    case "Vật tư y tế trong danh mục BHYT":
                        if (tenNhom.Where(a => a.TenNhomCT == "Vật tư y tế trong danh mục BHYT").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Vật tư y tế trong danh mục BHYT");
                        if (item.IDNhom != 10)
                            MessageBox.Show("Nhóm Vật tư y tế trong danh mục BHYT sai mã nhóm theo CV 9324");
                        idVTYT = item.IDNhom;
                        break;
                    case "VTYT thanh toán theo tỷ lệ":
                        if (tenNhom.Where(a => a.TenNhomCT == "VTYT thanh toán theo tỷ lệ").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm VTYT thanh toán theo tỷ lệ");
                        if (item.IDNhom != 11)
                            MessageBox.Show("Nhóm VTYT thanh toán theo tỷ lệ sai mã nhóm theo CV 9324");
                        idVTTT = item.IDNhom;
                        break;
                    case "Vận chuyển":
                        if (tenNhom.Where(a => a.TenNhomCT == "Vận chuyển").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Vận chuyển");
                        if (item.IDNhom != 12)
                            MessageBox.Show("Nhóm Vận chuyển sai mã nhóm theo CV 9324");
                        idChiPhiVC = item.IDNhom;
                        break;
                    case "Khám bệnh":
                        if (tenNhom.Where(a => a.TenNhomCT == "Khám bệnh").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Khám bệnh");
                        if (item.IDNhom != 13)
                            MessageBox.Show("Nhóm Khám bệnh sai mã nhóm theo CV 9324");
                        idCongKham = item.IDNhom;
                        break;

                    case "Giường điều trị ngoại trú":
                        if (tenNhom.Where(a => a.TenNhomCT == "Giường điều trị ngoại trú").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Giường điều trị ngoại trú");
                        if (item.IDNhom != 14)
                            MessageBox.Show("Nhóm Giường điều trị ngoại trú sai mã nhóm theo CV 9324");
                        idNgayGiuongNgoaiTru = item.IDNhom;
                        break;
                    case "Giường điều trị nội trú":
                        if (tenNhom.Where(a => a.TenNhomCT == "Giường điều trị nội trú").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Giường điều trị nội trú");
                        if (item.IDNhom != 15)
                            MessageBox.Show("Nhóm Giường điều trị nội trú sai mã nhóm theo CV 9324");
                        idNgayGiuongNoiTru = item.IDNhom;
                        break;
                    case "Nhóm hóa chất":
                        if (tenNhom.Where(a => a.TenNhomCT == "Nhóm hóa chất").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Nhóm hóa chất");
                        idHoaChat = item.IDNhom;
                        break;
                }
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (kt())
            {
                timquy(lupNgaytu.DateTime.Month);
                setIDNhom();
                DateTime ngaytu = System.DateTime.Now.Date;
                DateTime ngayden = System.DateTime.Now.Date;
                ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupngayden.DateTime);
                BaoCao.rep_14 rep = new BaoCao.rep_14();
                if (rad_Noitru.SelectedIndex == 1)
                    rep.txtTieuDe.Text = "THỐNG KÊ CHI PHÍ KCB NỘI TRÚ CÁC NHÓM ĐỐI TƯỢNG THEO TUYẾN CHUYÊN MÔN KỸ THUẬT";
                else
                    rep.txtTieuDe.Text = "THỐNG KÊ CHI PHÍ KCB NGOẠI TRÚ CÁC NHÓM ĐỐI TƯỢNG THEO TUYẾN CHUYÊN MÔN KỸ THUẬT";
                frmIn frm = new frmIn();
                QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var vphi = (from vp in Data.VienPhis
                            join vpct in Data.VienPhicts.Where(p => p.TrongBH == 1) on vp.idVPhi equals vpct.idVPhi
                            where (vp.NgayTT >= ngaytu && vp.NgayTT <= ngayden)
                            select new { vp.MaBNhan, vpct.SoLuong, vpct.ThanhTien, vpct.MaDV, vpct.TienChenh, vpct.TienBH, vpct.TienBN }).ToList();
                var vphi_gr = vphi.Select(p => p.MaBNhan).Distinct().ToList();
                var dtuong3 = (from vp in vphi_gr
                              join BN in Data.BenhNhans on vp.Value equals BN.MaBNhan
                              join rv  in Data.RaViens on BN.MaBNhan equals rv.MaBNhan
                              select new {BN.NoiTru,BN.DTNT, BN.Tuyen, BN.MaBNhan,BN.MaDTuong,BN.DTuong}).ToList();
                var dtuong2 = (from BN in dtuong3
                               where BN.DTuong =="BHYT" && (BN.NoiTru==rad_Noitru.SelectedIndex) &&
                               (radio_DTNT.SelectedIndex == 2 ? true : (radio_DTNT.SelectedIndex == 0 ? BN.DTNT == false : BN.DTNT == true))
                               select new { BN.Tuyen, BN.MaBNhan, BN.MaDTuong }).ToList();
                List<DTuong> _ldtuong = Data.DTuongs.ToList();
                var dtuong = (from dt2 in dtuong2 join dt in _ldtuong on dt2.MaDTuong.ToUpper() equals dt.MaDTuong.ToUpper().Trim()
                              select new { dt2.Tuyen, dt2.MaBNhan, dt.Nhom }).ToList();
                var nhomdv = (from dv in Data.DichVus select dv).ToList();
                var q = (from vp in vphi
                         join nhom in nhomdv on vp.MaDV equals nhom.MaDV
                         join dt in dtuong on vp.MaBNhan equals dt.MaBNhan
                         group new { vp, nhom, dt } by new { dt.Nhom } into kq
                         select new
                             {
                                 Tentuyen = kq.Key.Nhom,
                                 Xetnghiem = kq.Where(p => p.nhom.IDNhom == idXN).Sum(p =>(chk_TienBH.Checked? p.vp.TienBH: p.vp.ThanhTien)) - kq.Where(p => p.nhom.IDNhom == idXN).Sum(p => p.vp.TienChenh),
                                 CDHA = kq.Where(p => p.nhom.IDNhom == idCDHA || p.nhom.IDNhom == idTDCN).Sum(p => (chk_TienBH.Checked? p.vp.TienBH: p.vp.ThanhTien)) - kq.Where(p => p.nhom.IDNhom == idCDHA || p.nhom.IDNhom == idTDCN).Sum(p => p.vp.TienChenh),
                                 ThuocDT = kq.Where(p => p.nhom.IDNhom == idThuoc).Sum(p => (chk_TienBH.Checked? p.vp.TienBH: p.vp.ThanhTien)) - kq.Where(p => p.nhom.IDNhom == idThuoc).Sum(p => p.vp.TienChenh),
                                 mau = kq.Where(p => p.nhom.IDNhom == idMau).Sum(p => (chk_TienBH.Checked? p.vp.TienBH: p.vp.ThanhTien)) - kq.Where(p => p.nhom.IDNhom == idMau).Sum(p => p.vp.TienChenh),
                                 TTPT = kq.Where(p => p.nhom.IDNhom == idTTPT).Sum(p => (chk_TienBH.Checked? p.vp.TienBH: p.vp.ThanhTien)) - kq.Where(p => p.nhom.IDNhom == idTTPT).Sum(p => p.vp.TienChenh),
                                 VTYTtieuhao = kq.Where(p => p.nhom.IDNhom == idVTYT).Sum(p => (chk_TienBH.Checked? p.vp.TienBH: p.vp.ThanhTien)) - kq.Where(p => p.nhom.IDNhom == idVTYT).Sum(p => p.vp.TienChenh),
                                 VTYTthaythe = kq.Where(p => p.nhom.IDNhom == idVTTT).Sum(p => (chk_TienBH.Checked? p.vp.TienBH: p.vp.ThanhTien)) - kq.Where(p => p.nhom.IDNhom == idVTTT).Sum(p => p.vp.TienChenh),
                                 DVKTcao = kq.Where(p => p.nhom.IDNhom == idDVKTC).Sum(p => (chk_TienBH.Checked? p.vp.TienBH: p.vp.ThanhTien)) - kq.Where(p => p.nhom.IDNhom == idDVKTC).Sum(p => p.vp.TienChenh),
                                 Thuocthaighep = kq.Where(p => p.nhom.IDNhom == idThuocUTCTG).Sum(p => (chk_TienBH.Checked? p.vp.TienBH: p.vp.ThanhTien)) - kq.Where(p => p.nhom.IDNhom == idThuocUTCTG).Sum(p => p.vp.TienChenh),
                                 Tienkham = kq.Where(p => p.nhom.IDNhom == idNgayGiuongNoiTru || p.nhom.IDNhom == idNgayGiuongNgoaiTru || p.nhom.IDNhom == idCongKham).Sum(p => (chk_TienBH.Checked? p.vp.TienBH: p.vp.ThanhTien)) - kq.Where(p => p.nhom.IDNhom == idNgayGiuongNoiTru || p.nhom.IDNhom == idNgayGiuongNgoaiTru || p.nhom.IDNhom == idCongKham).Sum(p => p.vp.TienChenh),
                                 Vanchuyen = kq.Where(p => p.nhom.IDNhom == idChiPhiVC).Sum(p => (chk_TienBH.Checked? p.vp.TienBH: p.vp.ThanhTien)) - kq.Where(p => p.nhom.IDNhom == idChiPhiVC).Sum(p => p.vp.TienChenh),
                                 Benhnhandungtuyen = kq.Where(p => p.dt.Tuyen == 1).Sum(p => p.vp.TienBN),
                                 Benhnhantraituyen = kq.Where(p => p.dt.Tuyen == 2).Sum(p => p.vp.TienBN),
                                 Soluotdungtuyen = kq.Where(p => p.dt.Tuyen == 1).Select(p => p.vp.MaBNhan).Distinct().Count(),
                                 Soluottraituyen = kq.Where(p => p.dt.Tuyen == 2).Select(p => p.vp.MaBNhan).Distinct().Count(),
                                 BHXHchitra = kq.Sum(p => p.vp.TienBH) - kq.Sum(p => p.vp.TienChenh),
                                 Tongcong = kq.Sum(p => (chk_TienBH.Checked? p.vp.TienBH: p.vp.ThanhTien)) - kq.Sum(p => p.vp.TienChenh),
                                 BHXHtuchoithanhtoan = kq.Sum(p => p.vp.TienChenh)
                             }).OrderBy(p => p.Tentuyen).ToList();
                //if (q.Count > 0)
                //{
                rep.Quy.Value = theoquy();
                rep.ngaytu.Value = ngaytu.Date;
                rep.ngayden.Value = ngayden.Date;
                rep.TenDV.Value = DungChung.Bien.TenCQ;
                rep.Macs.Value = DungChung.Bien.MaBV;
                //rep.Nguoilap.Value = DungChung.Bien.Nguoiphat;
                //rep.TruongphongKHTT.Value = DungChung.Bien.TruongkhoaLS;
                //rep.Ketoan.Value = DungChung.Bien.Nguoilinh;
                //rep.ThutruongDV.Value = DungChung.Bien.TruongkhoaLS;
                rep.DataSource = q;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
                //}
                //rep.CreateDocument();
                //rep.DataMember = "Table";
                //frm.prcIN.PrintingSystem = rep.PrintingSystem;
                //frm.ShowDialog();
                //else
                //    MessageBox.Show("ko có dữ liệu");
            }
        }

        private bool kt()
        {
            if (lupNgaytu.Text == "" || lupngayden.Text == "")
            {
                MessageBox.Show("Bạn hãy chọn ngày tháng");
                lupNgaytu.Focus();
                return false;
            }
            return true;
        }
        private int timquy(int month)
        {
            if (month >= 1 && month <= 3)
            {
                return (1);
            }
            if (month > 3 && month <= 6)
            {
                return (2);
            }
            if (month > 6 && month <= 9)
            {
                return (3);
            }
            else { return (4); }
        }
        private string theoquy()
        {
            string quy = "";


            switch (timquy(lupNgaytu.DateTime.Month))
            {
                case 1:
                    quy = "Quý I";
                    break;
                case 2:
                    quy = "Quý II";
                    break;
                case 3:
                    quy = "Quý III";
                    break;
                case 4:
                    quy = "Quý IV";
                    break;
            }
            return quy;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm14b_Load(object sender, EventArgs e)
        {
            lupngayden.DateTime = System.DateTime.Now;
            lupNgaytu.DateTime = System.DateTime.Now;
            rad_Noitru_SelectedIndexChanged(sender,e);
            radio_DTNT.SelectedIndex = 2;
        }

        private void rad_Noitru_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rad_Noitru.SelectedIndex == 0)
                radio_DTNT.Enabled = true;
            else
                radio_DTNT.Enabled = false;
        }
    }
}