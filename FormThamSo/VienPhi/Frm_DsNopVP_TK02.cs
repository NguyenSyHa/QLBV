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
    public partial class Frm_DsNopVP_TK02 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_DsNopVP_TK02()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
    
        private bool kt()
        {
            if (lupNgaytu.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupNgaytu.Focus();
                return false;
            }
            if (lupNgayden.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                lupNgayden.Focus();
                return false;
            }
           
            else return true;
        }
        List<DTBN> _lDTBN = new List<DTBN>();
        private void Frm_DsNopVP_TK02_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            lupNgaytu.Focus();
            lupNgaytu.DateTime = System.DateTime.Now;
            lupNgayden.DateTime = System.DateTime.Now;
            _lDTBN = _data.DTBNs.Where(p => p.Status == 1).ToList();
            lupDoituong.Properties.DataSource = _lDTBN;
        }
        private void setIDNhom()
        {

            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var tenNhom = _data.NhomDVs.ToList();
            foreach (var item in tenNhom)
            {
                if (item.TenNhomCT.Contains("khám") && item.TenNhomCT != "Khám bệnh") {
                    idCK_KSK = item.IDNhom;
                }
                switch (item.TenNhomCT)
                {
                    case  "Thuốc trong danh mục BHYT" :
                        idThuoc = item.IDNhom;
                        if (tenNhom.Where(a => a.TenNhomCT ==  "Thuốc trong danh mục BHYT" ).Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Nhóm thuốc, dịch truyền");
                        break;
                    case "Máu và chế phẩm của máu":
                        if (tenNhom.Where(a => a.TenNhomCT == "Máu và chế phẩm của máu").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Nhóm máu và chế phẩm của máu");
                        idMau = item.IDNhom;
                        break;
                    case "Xét nghiệm":
                        if (tenNhom.Where(a => a.TenNhomCT == "Xét nghiệm").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Nhóm xét nghiệm");
                        idXN = item.IDNhom;
                        break;
                    case "Chẩn đoán hình ảnh":
                        if (tenNhom.Where(a => a.TenNhomCT == "Chẩn đoán hình ảnh").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Nhóm chẩn đoán hình ảnh & TDCN");
                        idCDHA = item.IDNhom;
                        break;
                    case "Thủ thuật, phẫu thuật":
                        if (tenNhom.Where(a => a.TenNhomCT == "Thủ thuật, phẫu thuật").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Nhóm phẫu thuật, thủ thuật");
                        idTTPT = item.IDNhom;
                        break;
                    case "Khám bệnh":
                        if (tenNhom.Where(a => a.TenNhomCT == "Khám bệnh").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Nhóm tiền công khám");
                        idCongKham = item.IDNhom;
                        break;
                    case "DVKT thanh toán theo tỷ lệ":
                        if (tenNhom.Where(a => a.TenNhomCT == "DVKT thanh toán theo tỷ lệ").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Nhóm dịch vụ kỹ thuật cao");
                        idDVKTC = item.IDNhom;
                        break;
                    case"Vật tư y tế trong danh mục BHYT":
                        if (tenNhom.Where(a => a.TenNhomCT =="Vật tư y tế trong danh mục BHYT").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Nhóm vật tư y tế tiêu hao");
                        idVTYT = item.IDNhom;
                        break;
                    case "Giường điều trị nội trú":
                        if (tenNhom.Where(a => a.TenNhomCT == "Giường điều trị nội trú").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Nhóm ngày giường");
                        idNgayGiuong = item.IDNhom;
                        break;
                    case "Vận chuyển":
                        if (tenNhom.Where(a => a.TenNhomCT == "Vận chuyển").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Chi phí vận chuyển");
                        idChiPhiVC = item.IDNhom;
                        break;
                    case"VTYT thanh toán theo tỷ lệ":
                        if (tenNhom.Where(a => a.TenNhomCT =="VTYT thanh toán theo tỷ lệ").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Nhóm vật tư y tế thay thế");
                        idVTTT = item.IDNhom;
                        break;
                    case "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục":
                        if (tenNhom.Where(a => a.TenNhomCT == "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Nhóm thuốc ung thư, chống thải ghép");
                        idThuocUngThuCTG = item.IDNhom;
                        break;
                    case "Nhóm hóa chất":
                        if (tenNhom.Where(a => a.TenNhomCT == "Nhóm hóa chất").Count() > 1)
                            MessageBox.Show("Danh mục nhóm Dịch vụ đang tồn tại nhiều nhóm Nhóm hóa chất");
                        idHoaChat = item.IDNhom;
                        break;
                }

            }
        }   int idThuoc = -1, idMau = -1, idXN = -1, idCDHA = -1, idTTPT = -1, idCongKham = -1, idDVKTC = -1,
            idVTYT = -1, idNgayGiuong = -1, idChiPhiVC = -1, idVTTT = -1, idThuocUngThuCTG = -1, idHoaChat = -1, idCK_KSK=-1;
        private void btnOK_Click(object sender, EventArgs e)
        {
            frmIn frm = new frmIn();
            setIDNhom();
            if (kt())
            {
                DateTime ngaytu = System.DateTime.Now.Date;
                DateTime ngayden = System.DateTime.Now.Date;
                ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupNgayden.DateTime);

                byte _dtBN = 0;
                if (lupDoituong.EditValue != null)
                    _dtBN = Convert.ToByte(lupDoituong.EditValue.ToString());
                    BaoCao.Rep_DsNopVP_TK02 rep = new BaoCao.Rep_DsNopVP_TK02();
                    rep.TuNgayDenNgay.Value = "Từ ngày: " + ngaytu.ToString().Substring(0, 10) + " Đến ngày: " + ngayden.ToString().Substring(0, 10);
                    var q2 = (from vp in dataContext.VienPhis.Where(p => p.NgayTT >= ngaytu && p.NgayTT <= ngayden)
                             join vpct in dataContext.VienPhicts on vp.idVPhi equals vpct.idVPhi
                             select new {vpct.MaDV,  vp.NgayTT,vpct.ThanhTien,vp.MaBNhan } ).ToList();
                    var q = (from  vp in q2
                             join bn in dataContext.BenhNhans.Where(p => p.IDDTBN == _dtBN) on vp.MaBNhan equals bn.MaBNhan
                             select new { vp.MaDV, bn.TenBNhan, bn.NamSinh, bn.DChi, bn.TChung, vp.NgayTT, vp.ThanhTien }).ToList();
                    var qksk = (from a in q  
                             join dv in dataContext.DichVus on a.MaDV equals dv.MaDV
                                group new { a,dv } by new {dv.IDNhom, a.TenBNhan, a.NamSinh, a.DChi,a.TChung, a.NgayTT } into kq
                                select new
                                {
                                    NgayThang = kq.Key.NgayTT,
                                    NamSinh = kq.Key.NamSinh,
                                    HoTen = kq.Key.TenBNhan,
                                    DiaChi = kq.Key.DChi,
                                    NoiDung=kq.Key.TChung,
                                    XetNghiem = kq.Where(p => p.dv.IDNhom==idXN).Sum(p => p.a.ThanhTien),
                                    CongKham = kq.Where(p => p.dv.IDNhom == idCongKham || p.dv.IDNhom == idCK_KSK).Sum(p => p.a.ThanhTien),
                                    CDHA = kq.Where(p => p.dv.IDNhom == idCDHA).Sum(p => p.a.ThanhTien),
                                    TongTien = kq.Sum(p => p.a.ThanhTien),
                                }).OrderByDescending(p => p.NgayThang).ThenBy(p => p.HoTen).ToList();
                    if (qksk.Count > 0)
                    {
                        rep.TongTien.Value = qksk.Sum(p => p.TongTien).ToString();
                        rep.DataSource = qksk.ToList();
                    }
                       rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                   
                }
                              
        }

     
        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}