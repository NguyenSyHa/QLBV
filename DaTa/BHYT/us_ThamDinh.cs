using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
namespace QLBV.BHYT
{
    public partial class us_ThamDinh : DevExpress.XtraEditors.XtraUserControl
    {
        public us_ThamDinh()
        {
            InitializeComponent();
        }
        int idThuoc = -1, idMau = -1, idXN = -1, idCDHA = -1, idTTPT = -1, idCongKham = -1, idDVKTC = -1,
    idVTYT = -1, idNgayGiuong = -1, idChiPhiVC = -1, idVTTT = -1, idThuocUngThuCTG = -1, idHoaChat = -1;
        QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void TimKiem() {
            try
            {
                DateTime ngaytu = System.DateTime.Now.Date;
                DateTime ngayden = System.DateTime.Now.Date;
                ngaytu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
                ngayden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
                int _noitru=-1;
                _noitru=radNoiTru.SelectedIndex;
                int _duyet = -1;

                var qt = (from vp in _dataContext.VienPhis 
                          join vpct in _dataContext.VienPhicts on vp.idVPhi equals vpct.idVPhi
                          join a in _dataContext.DichVus on vpct.MaDV equals a.MaDV

                          where (vp.NgayTT >= ngaytu && vp.NgayTT <= ngayden)
                          group new { vp, a, vpct } by new
                          {
                              vpct.TrongBH,
                             a.BHTT,
                              vp.Duyet,
                              vp.NgayTT,
                              vp.MaBNhan,
                              vp.MaKP,
                              a.IDNhom,
                              vpct.ThanhTien,
                              vpct.TienBN,
                              vpct.TienBH,
                          } into kq
                          select new
                          {
                              kq.Key.Duyet,
                              kq.Key.TrongBH,
                              kq.Key.NgayTT,
                              kq.Key.MaBNhan,
                              kq.Key.IDNhom,
                              kq.Key.MaKP,
                              kq.Key.BHTT,
                              ThanhTien = kq.Sum(p => p.vpct.ThanhTien),
                              Tongchi = kq.Sum(p => p.vpct.ThanhTien),
                              Tongcong = kq.Sum(p => p.vpct.ThanhTien),
                              TienBN = kq.Sum(p => p.vpct.TienBN),
                              TienBH = kq.Sum(p => p.vpct.TienBH),
                          }).ToList();
                var qt2 = (from a in qt join bn in _dataContext.BenhNhans.Where(p => p.NoiTru == _noitru && p.DTuong=="BHYT")
                          on a.MaBNhan equals bn.MaBNhan
                          group new { a, bn } by new
                          {
                              a.TrongBH,
                              a.BHTT,
                              a.Duyet,
                              a.NgayTT,
                              a.MaBNhan,
                              a.MaKP,
                              a.IDNhom,
                              bn.DChi,
                              bn.HanBHDen,
                              bn.HanBHTu,
                              bn.TuyenDuoi,
                              bn.DTNT,
                              bn.DTuong,
                              bn.NoiTru,

                              bn.NoiTinh,
                              bn.Tuyen,
                              bn.TenBNhan,
                              bn.NamSinh,
                              bn.SThe,
                              bn.GTinh,
                              bn.MaCS,
                              bn.MaDTuong,
                              bn.CapCuu,
                              a.ThanhTien,
                              a.TienBN,
                              a.TienBH,
                              bn.Tuoi,
                          } into kq
                          select new
                          {
                              kq.Key.Duyet,
                              kq.Key.DTuong,
                              kq.Key.NoiTru,
                              kq.Key.TrongBH,
                              kq.Key.TuyenDuoi,
                              kq.Key.MaBNhan,
                              kq.Key.NgayTT,
                              kq.Key.MaDTuong,
                              kq.Key.CapCuu,
                            
                              kq.Key.IDNhom,
                              kq.Key.HanBHTu,
                              kq.Key.HanBHDen,
                              kq.Key.DChi,
                              kq.Key.DTNT,
                              kq.Key.NoiTinh,
                              kq.Key.Tuyen,
                              kq.Key.MaKP,
                              kq.Key.TenBNhan,
                              kq.Key.NamSinh,
                              kq.Key.SThe,
                              kq.Key.GTinh,
                              kq.Key.MaCS,
                              kq.Key.Tuoi,
                              kq.Key.BHTT,
                              ThanhTien = kq.Sum(p => p.a.ThanhTien),
                              Tongchi = kq.Sum(p => p.a.ThanhTien),
                              Tongcong = kq.Sum(p => p.a.ThanhTien),
                              TienBN = kq.Sum(p => p.a.TienBN),
                              TienBH = kq.Sum(p => p.a.TienBH),
                          }).ToList();
                var q2 = (from bn in _dataContext.BenhNhans.Where(p => p.NoiTru == _noitru && p.DTuong == "BHYT")
                          //on rv.MaBNhan equals bn.MaBNhan
                          join vp in _dataContext.VienPhis on bn.MaBNhan equals vp.MaBNhan
                          join vpct in _dataContext.VienPhicts on vp.idVPhi equals vpct.idVPhi
                          join dv in _dataContext.DichVus on vpct.MaDV equals dv.MaDV
                          where (vp.NgayTT >= ngaytu && vp.NgayTT <= ngayden)// && _ngaytt == 1 ? vp.NgayTT <= ngayden : rv.NgayRa <= ngayden)
                          // where
                          //(
                          //(_ngaytt == 1 && vp.NgayTT >= ngaytu && vp.NgayTT <= ngayden) || // tìm theo ngày thanh toán
                          //(_ngaytt == 0 && rv.NgayRa >= ngaytu && rv.NgayRa <= ngayden) || // tìm theo ngày ra viện
                          //(_ngaytt == 2 && dsbnTU.Contains(bn.MaBNhan))// tìm theo ngày duyệt
                          //)
                          where (vpct.TrongBH == 1)
                          select new
                          {
                              vp.Duyet,
                              vpct.TrongBH,
                              bn.DChi,
                              bn.HanBHDen,
                              bn.HanBHTu,
                              bn.TuyenDuoi,
                              bn.DTNT,
                              bn.DTuong,
                              bn.NoiTru,
                              bn.MaBNhan,
                              bn.NoiTinh,
                              bn.Tuyen,
                              vpct.MaKP,

                              bn.TenBNhan,
                              bn.NamSinh,
                              bn.SThe,
                              bn.GTinh,
                              bn.MaCS,
                              bn.MaDTuong,
                              bn.CapCuu,

                              dv.IDNhom,
                              vpct.ThanhTien,
                              vpct.TienBN,
                              vpct.TienBH,
                              dv.BHTT,
                              vp.NgayTT,
                              bn.Tuoi

                          }).OrderBy(p => p.MaBNhan).ToList();
                    var q3 = (from a in qt2

                              join rv in _dataContext.RaViens on a.MaBNhan equals rv.MaBNhan
                              //  where (cboDoiTuong.Text != "BHYT" || (cboDoiTuong.Text == "BHYT" && (a.TuyenDuoi == _td_PKKV || a.TuyenDuoi == _td_trongBV || a.TuyenDuoi == _td_XP) && (a.NoiTinh == _nt_den || a.NoiTinh == _nt_kb || a.NoiTinh == _nt_ngtinh)))
                              group new { a,rv } by new { a.Duyet, a.HanBHDen, a.HanBHTu, a.DChi, rv.SoNgaydt, a.DTNT, a.TuyenDuoi, a.NgayTT, a.DTuong, a.NoiTru, a.TrongBH, a.MaBNhan, a.NoiTinh,  a.TenBNhan, a.NamSinh, a.GTinh, a.SThe, a.MaCS, a.Tuyen, rv.NgayVao, a.MaKP, rv.MaICD, rv.NgayRa, a.MaDTuong, a.CapCuu, a.Tuoi } into kq
                              select new
                              {
                                  kq.Key.Duyet,
                                  kq.Key.SoNgaydt,
                                  kq.Key.DTuong,
                                  kq.Key.NoiTru,
                                  kq.Key.TrongBH,
                                  kq.Key.TuyenDuoi,
                                  kq.Key.MaBNhan,
                                  kq.Key.NgayTT,
                                  kq.Key.MaDTuong,
                                  kq.Key.CapCuu,
                                  NoiTinh = kq.Key.NoiTinh,
                                  Tuyen = kq.Key.Tuyen,
                                  Makp = kq.Key.MaKP,
                                 
                                  TenBNhan = kq.Key.TenBNhan,
                                  NSinh = kq.Key.NamSinh,
                                  SThe = kq.Key.SThe,
                                  Nam = kq.Key.GTinh,
                                  GTinh = kq.Key.GTinh,
                                  MaCS = kq.Key.MaCS,
                                  MaICD = kq.Key.MaICD,
                                  Ngaykham = kq.Key.NgayVao,
                                  Ngayra = kq.Key.NgayRa,
                                  Tuoi = kq.Key.Tuoi,
                                  BHtu = kq.Key.HanBHTu,
                                  BHden = kq.Key.HanBHDen,
                                  Diachi = kq.Key.DChi,
                                  Mabn = kq.Key.MaBNhan,
                                  Thuoc = kq.Where(p => p.a.IDNhom == idThuoc).Where(p => p.a.BHTT == 100).Sum(p => p.a.ThanhTien),
                                  CDHA = kq.Where(p => p.a.IDNhom == idCDHA).Where(p => p.a.BHTT == 100).Sum(p => p.a.ThanhTien),
                                  Congkham = _noitru == 1 ? kq.Where(p => p.a.IDNhom == idNgayGiuong).Sum(p => p.a.ThanhTien) : kq.Where(p => p.a.IDNhom == idCongKham).Sum(p => p.a.ThanhTien),
                                  Xetnghiem = kq.Where(p => p.a.IDNhom == idXN).Where(p => p.a.BHTT == 100).Sum(p => p.a.ThanhTien),
                                  Mau = kq.Where(p => p.a.IDNhom == idMau).Where(p => p.a.BHTT == 100).Sum(p => p.a.ThanhTien),
                                  TTPT = kq.Where(p => p.a.IDNhom == idTTPT).Where(p => p.a.BHTT == 100).Sum(p => p.a.ThanhTien),
                                  VTYT = kq.Where(p => p.a.IDNhom == idVTYT).Where(p => p.a.BHTT == 100).Sum(p => p.a.ThanhTien),
                                  DVKT_tl = kq.Where(p => p.a.IDNhom == idDVKTC).Where(p => p.a.BHTT != 100).Sum(p => p.a.ThanhTien),
                                  Thuoc_tl = kq.Where(p => p.a.IDNhom == idThuocUngThuCTG).Where(p => p.a.BHTT != 100).Sum(p => p.a.ThanhTien),
                                  VTYT_tl = kq.Where(p => p.a.IDNhom == idVTTT).Where(p => p.a.BHTT != 100).Sum(p => p.a.ThanhTien),
                                  CPVanchuyen = kq.Where(p => p.a.IDNhom == idChiPhiVC).Sum(p => p.a.ThanhTien),
                                  CPNgoaiBH = kq.Where(p => p.a.IDNhom == idChiPhiVC).Sum(p => p.a.ThanhTien),
                                  ThanhTien = kq.Sum(p => p.a.ThanhTien),
                                  Tongchi = kq.Sum(p => p.a.ThanhTien),
                                  Tongcong = kq.Sum(p => p.a.ThanhTien),
                                  TienBN = kq.Sum(p => p.a.TienBN),
                                  TienBH = kq.Sum(p => p.a.TienBH),
                              }).ToList();

                  
                    grcThamDinh.DataSource = q3.ToList();
                
              
            }
            catch (Exception ex) {
                MessageBox.Show("Lỗi tìm kiếm! "+ex.Message);
            }
        }

      
        private void us_ThamDinh_Load(object sender, EventArgs e)
        {
            dtTimTuNgay.DateTime = System.DateTime.Now;
            dtTimDenNgay.DateTime = System.DateTime.Now;
            var tenNhom = _dataContext.NhomDVs.ToList();
            foreach (var item in tenNhom)
            {
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
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            BHYT.frm_DuyetBN frm = new frm_DuyetBN(_int_maBN, txtTenBNhan.Text,true);
            frm.ShowDialog();
        }

        private void grvThamDinh_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvThamDinh.GetFocusedRowCellValue(colMaBNhan) != null)
            {
                txtMaBNhan.Text = grvThamDinh.GetFocusedRowCellValue(colMaBNhan).ToString();
                txtTenBNhan.Text = grvThamDinh.GetFocusedRowCellValue(colTenBNhan).ToString();
            }
            else {
                txtMaBNhan.Text = "";
                txtTenBNhan.Text = "";
            }
        }

        private void grvThamDinh_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (grvThamDinh.GetRowCellValue(e.RowHandle, colDuyet) != null && grvThamDinh.GetRowCellValue(e.RowHandle, colDuyet).ToString() != "")
            {
                int duyet = Convert.ToInt32(grvThamDinh.GetRowCellValue(e.RowHandle, colDuyet));
                switch (duyet) { 
                    case 1:
                        e.Appearance.ForeColor = Color.Blue;
                        break;
                    case 2:
                        e.Appearance.ForeColor = Color.DarkRed;
                        break;
                    case 3:
                        e.Appearance.ForeColor = Color.DimGray;
                        break;
                }
            }
        }

        private void grvThamDinh_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colXemCP") {
                btnXem_Click(sender, e);
            }
        }

        private void grvThamDinh_DoubleClick(object sender, EventArgs e)
        {
            if (grvThamDinh.GetFocusedRowCellValue(colMaBNhan) != null) {
                btnXem_Click(sender, e);
            }
        }

        private void btnDuyet_Click(object sender, EventArgs e)
        {
            DialogResult _result = MessageBox.Show("Bạn chắc chắn duyệt tất cả BN trong danh sách?", "Hỏi duyệt", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_result == DialogResult.Yes)
                {

                    for (int i = 0; i < grvThamDinh.RowCount; i++)
                    {
                        int  _mabn = 0;
                        int duyet = 0;
                        try{
                            if (grvThamDinh.GetRowCellValue(i, colDuyet) != null)
                                duyet = Convert.ToInt32(grvThamDinh.GetRowCellValue(i, colDuyet));
                            if (duyet <= 0)
                            {
                                if (grvThamDinh.GetRowCellValue(i, colMaBNhan) != null)
                                {
                                    _mabn =Convert.ToInt32( grvThamDinh.GetRowCellValue(i, colMaBNhan));
                                    var vp = (from vphi in _dataContext.VienPhis.Where(p => p.MaBNhan == _mabn &&( p.Duyet==null || p.Duyet==0)) select new { vphi.idVPhi }).ToList();
                                    if (vp.Count > 0)
                                    {
                                        int idvp = vp.First().idVPhi;
                                        var udvp = _dataContext.VienPhis.Single(p => p.idVPhi == idvp);
                                        udvp.Duyet = 1;
                                        if (_dataContext.SaveChanges() >= 0)
                                        {
                                            var vpct = _dataContext.VienPhicts.Where(p => p.idVPhi == idvp).ToList();
                                            foreach (var a in vpct)
                                            {
                                                //var udvpct = _dataContext.VienPhicts.Single(p => p.idVPhict == a.idVPhict);
                                                //udvpct.Duyet = 1;
                                                //udvpct.SoLuongD = 0;
                                                //udvpct.TienChenh = 0;
                                                //udvpct.TienChenhBN = 0;
                                                //_dataContext.SaveChanges();
                                                a.Duyet = 1;
                                                a.SoLuongD = 0;
                                                a.TienChenh = 0;
                                                a.TienChenhBN = 0;
                                               
                                            }
                                            if(vpct.Count>0)
                                                _dataContext.SaveChanges();
                                        }
                                    }
                                }

                            }
                        
                        } catch(Exception ex){
                        MessageBox.Show("Lỗi không duyệt được BN: "+grvThamDinh.GetRowCellDisplayText(i,colTenBNhan).ToString()+" "+ ex.Message);
                        }
                    } // kt for
                    TimKiem();
                    MessageBox.Show("Đã duyệt song");
                }
                       
        }

        private void grvThamDinh_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == STT)
            {
                e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        //private void grvThamDinh_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        //{
        //    //Tạo số thự tự
        //    if (e.Column == STT)
        //    {
        //        e.DisplayText = Convert.ToString(e.RowHandle + 1);
        //    }
        //}
    }
}
