using QLBV.DungChung;
using QLBV_Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QLBV.FormThamSo
{
    public partial class frm_79_80aHD_1399 : DevExpress.XtraEditors.XtraForm
    {
        private List<string> _lDSMaICD = new List<string>();

        public frm_79_80aHD_1399()
        {
            InitializeComponent();
        }

        private int idThuoc = -1, idMau = -1, idXN = -1, idTTPT = -1, idCongKham = -1, idDVKTC = -1, idThuocUTCTG = -1,
            idVTYT = -1, idNgayGiuongNoiTru = -1, idNgayGiuongNgoaiTru = -1, idChiPhiVC = -1, idVTTT = -1, idThuocTyLe = -1, idHoaChat = -1;

        private string Title = string.Empty;
        private List<int> _lstIdCDHA;

        private void setIDNhom()
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var tenNhom = _data.NhomDVs.ToList();
            _lstIdCDHA = new List<int>();
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
                        _lstIdCDHA.Add(item.IDNhom);

                        if (item.IDNhom != 2)
                            MessageBox.Show("Nhóm chẩn đoán hình ảnh sai mã nhóm theo CV 9324");
                        continue;
                    case "Thăm dò chức năng":
                        _lstIdCDHA.Add(item.IDNhom);
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

        private List<Cls79_80.cl_79_80> _listVPBH = new List<Cls79_80.cl_79_80>();
        private List<TamUng> _lIDtamung = new List<TamUng>();

        private void btnOK_Click(object sender, EventArgs e)
        {
            List<int> lstMaKP = new List<int>();
            List<string> lstTenKP = new List<string>();
            lstMaKP.Clear();
            lstTenKP.Clear();
            //int[] index = grvKP.GetSelectedRows();
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon, 180);
            if (_Kphong.Count() > 0)
            {
                foreach (var a in _Kphong)
                {
                    if (a.chon == true)
                    {
                        lstMaKP.Add(a.makp);
                        lstTenKP.Add(a.tenkp);
                    }
                }
            }
            //for (int i = 0; i < index.Count(); i++)
            //{
            //    int makp = Convert.ToInt32(grvKP.GetRowCellValue(index[i], colMaKP));
            //    lstMaKP.Add(makp);

            //    string tenkp = grvKP.GetRowCellValue(index[i], colTenKP).ToString();
            //    lstTenKP.Add(tenkp);
            //}

            //if (lstMaKP.Count() == 0)
            //{
            //    //_lKphong.RemoveAt(0);
            //    foreach (var j in _lKphong)
            //    {
            //        lstMaKP.Add(j.MaKP);
            //        lstTenKP.Add(j.TenKP);
            //    }
            //}

            List<string> _dsCSKCB = new List<string>();
            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                if (cklKP.GetItemChecked(i))
                {
                    _dsCSKCB.Add(cklKP.GetItemValue(i).ToString());
                }
            }
            int SXTheoDT = 0;
            if (ckcNhomDTuong.Checked)
            {
                SXTheoDT = 1;
            }
            int HTThanhToan = rdHTThanhToan.SelectedIndex;
            int ChuyenKhoan = rdCKhoan.SelectedIndex;
            _dsCSKCB = _dsCSKCB.Distinct().ToList();
            _listVPBH.Clear();
            if (false)
                setIDNhom();
            int trongBH = 0;
            trongBH = rdTrongBH.SelectedIndex;
            int _intduyet = 2;
            if (rad_Duyet.SelectedIndex != null)
                _intduyet = rad_Duyet.SelectedIndex;
            // string DoiTuongKham = "BHYT";
            // DoiTuongKham = lupDoituong.Text;
            int id_DoiTuongKham = -1;
            if (lupDoituong.EditValue != null)
                id_DoiTuongKham = Convert.ToInt32(lupDoituong.EditValue);
            int _ngaytt = radTimKiem.SelectedIndex;
            int _GuiBHXH = rgGuiBHXH.SelectedIndex;
            List<string> lmaDtuong = new List<string>();
            string macb = "";
            if (lupCanBoTT.EditValue != null)
                macb = lupCanBoTT.EditValue.ToString();
            for (int i = 0; i < cklMaDTuong.ItemCount; i++)
            {
                if (cklMaDTuong.GetItemChecked(i) == true)
                    lmaDtuong.Add(cklMaDTuong.GetItemValue(i).ToString());
            }
            if (kt())
            {
                //progess
                //ChucNang.progess.DisplayProgress(null, "Đang xử lý...");
                //
                DateTime ngaytu = System.DateTime.Now.Date;
                DateTime ngayden = System.DateTime.Now.Date;
                ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupngayden.DateTime);

                DateTime ngaytunew = ngaytu.AddMonths(-6);
                DateTime ngaydennew = ngayden.AddMonths(6);
                int _MaKPc = 0;
                if (!string.IsNullOrEmpty(lupKhoaphong.Text))
                {
                    _MaKPc = Convert.ToInt32(lupKhoaphong.EditValue);
                }
                List<string> _ltamung = new List<string>();
                int _CP_BH = -1;
                //if (ckTLBH.Checked)
                //    _CP_BH = 1;
                //if (ckTLBN.Checked)
                //    _CP_BH = 2;
                //if (ckTLBN.Checked && ckTLBH.Checked)
                //    _CP_BH = 0;
                if ((ckc_BNCCT.Checked && ckTLBH.Checked && ckTLBN.Checked) || (!ckc_BNCCT.Checked && !ckTLBH.Checked && !ckTLBN.Checked))
                    _CP_BH = 0;

                var q22 = (from vp in _dataContext.VienPhis
                               // where vp.MaBNhan==113423
                           join vpct in _dataContext.VienPhicts on vp.idVPhi equals vpct.idVPhi
                           where (macb == "" || (rdCanBo.SelectedIndex == 0 && vp.MaCB == macb) || (rdCanBo.SelectedIndex == 1))
                           where ((_ngaytt == 2) ? (vp.NgayDuyet >= ngaytu && vp.NgayDuyet <= ngayden) :
                                  (_ngaytt == 1 ? (vp.NgayTT >= ngaytu && vp.NgayTT <= ngayden) :
                                  (_ngaytt == 3 ?
                                                (DungChung.Bien.MaBV == "30002" ?
                                                        (_intduyet == 0 ? (vp.NgayTT >= ngaytu && vp.NgayTT <= ngayden && vp.NgayDuyetCP == null) :
                                                        (_intduyet == 1 ? (vp.NgayDuyetCP >= ngaytu && vp.NgayDuyetCP <= ngayden)
                                                        : ((vp.NgayTT >= ngaytu && vp.NgayTT <= ngayden && vp.NgayDuyetCP == null) || (vp.NgayDuyetCP >= ngaytu && vp.NgayDuyetCP <= ngayden))
                                                        ))
                                                : vp.NgayDuyetCP >= ngaytu && vp.NgayDuyetCP <= ngayden)
                                  : (vp.NgayRa >= ngaytu && vp.NgayRa <= ngayden))))
                           //where (_GuiBHXH == 2 ? true : (_GuiBHXH == 0 ? vp.ExportBHXH == false : vp.ExportBHXH == true))
                           select new
                           {
                               vpct.SoLuong,
                               vpct.DonGia,
                               vpct.TyLeTT,
                               vpct.XHH,
                               vpct.IDTamUng,
                               vpct.MaDV,
                               vp.MaBNhan,
                               vp.NgayDuyet,
                               vpct.TrongBH,
                               vpct.MaKP,
                               vpct.LoaiDV,
                               vpct.TyLeBHTT,
                               vpct.ThanhToan,
                               ThanhTien = vpct.ThanhTien,
                               TienBN = vpct.TBNCTT + vpct.TBNTT,
                               TienBH = vpct.TienBH,
                               vpct.TBNCTT,
                               vpct.TBNTT,
                               vp.NgayTT,
                               vp.ExportBHXH
                           }).ToList().Select(x => new
                           {
                               x.XHH,
                               x.IDTamUng,
                               x.MaDV,
                               x.MaBNhan,
                               x.NgayDuyet,
                               x.TrongBH,
                               x.MaKP,
                               x.TyLeBHTT,
                               x.ThanhToan,
                               x.LoaiDV,
                               ThanhTien = _CP_BH == 0 ? x.ThanhTien : ((ckTLBH.Checked ? x.TienBH : 0) + (ckTLBN.Checked ? x.TBNTT : 0) + (ckc_BNCCT.Checked ? x.TBNCTT : 0)), //(_CP_BH == 0 ? x.ThanhTien : (_CP_BH == 1 ? x.TienBH : x.TienBN)),// chkLamTron.Checked ? ((Math.Round(x.SoLuong, 2) * Math.Round(x.DonGia, 2) * (x.TyLeTT / 100))) : x.ThanhTien,// (_CP_BH == 0 ? x.ThanhTien : (_CP_BH == 1 ? x.TienBH : x.TienBN)),
                               TienBN = (_CP_BH == 0 ? x.TienBN : (ckTLBN.Checked ? x.TBNTT : 0) + (ckc_BNCCT.Checked ? x.TBNCTT : 0)),
                               TienBH = (_CP_BH == 0 ? x.TienBH : (ckTLBH.Checked ? x.TienBH : 0)),
                               //ThanhTienVTYT = (Math.Round(x.SoLuong, 2) * Math.Round(x.DonGia, 2) * (x.TyLeTT / 100)),
                               x.NgayTT,
                               x.ExportBHXH
                           }).ToList();

                #region chỉ tìm những bệnh nhân có thực hiện dịch vụ nào đấy

                List<int> _lMaBN_DV = new List<int>();
                if (_lDSMaDV.Count > 0)
                {
                    var qdsbn = (from bn in q22 join dv in _lDSMaDV on bn.MaDV equals dv group bn by bn.MaBNhan into kq1 select new { MaBNhan = kq1.Key.Value }).ToList();
                    _lMaBN_DV = qdsbn.Select(p => p.MaBNhan).ToList();
                }

                #endregion chỉ tìm những bệnh nhân có thực hiện dịch vụ nào đấy

                int lamtron = 4;
                if (chkLamTron.Checked)
                    lamtron = 2;
                var q6 = (from a in q22.Where(p => (_lDSMaDV.Count > 0) ? _lMaBN_DV.Contains(p.MaBNhan ?? 0) : true)
                          where (DungChung.Bien.MaBV == "30002" ? true : ((_intduyet == 2 ? true : (_intduyet == 1 ? a.NgayDuyet != null : a.NgayDuyet == null))))
                          select a.MaBNhan).Distinct().ToList();
                //if (radioThuChi.SelectedIndex < 2)
                //{
                //    _lIDtamung = (from a in q6
                //                  join tu in _dataContext.TamUngs on a equals tu.MaBNhan
                //                  where radioThuChi.SelectedIndex == 0 ? (tu.PhanLoai == 3) : (tu.PhanLoai == 1 || tu.PhanLoai == 2)
                //                  select tu).ToList();
                //}

                var q2 = (from a in q22.Where(p => _GuiBHXH == 2 ? true : (_GuiBHXH == 0 ? p.ExportBHXH == false : p.ExportBHXH == true))
                          select new
                          {
                              a.XHH,
                              // IDTamUng = _lIDtamung.Where(p => p.IDTamUng == a.IDTamUng).ToList().Count > 0 ? _lIDtamung.Where(p => p.IDTamUng == a.IDTamUng).First().IDTamUng : 0,
                              a.MaDV,
                              a.MaBNhan,
                              a.NgayDuyet,
                              a.TrongBH,
                              a.MaKP,
                              a.TyLeBHTT,
                              a.ThanhToan,
                              a.LoaiDV,
                              ThanhTien = a.ThanhTien, //chkLamTron.Checked ? a.ThanhTien : (_CP_BH == 0 ? a.ThanhTien : (_CP_BH == 1 ? (a.TienBH) : a.TienBN)),
                              TienBN = (chkLamTron.Checked ? (a.ThanhTien - Math.Round(a.ThanhTien * (a.TyLeBHTT / 100), 2)) : a.TienBN),
                              TienBH = (chkLamTron.Checked ? Math.Round(a.ThanhTien * (a.TyLeBHTT / 100), 2) : (a.TienBH)),
                              //ThanhTienVTYT = a.ThanhTienVTYT,
                              a.NgayTT
                          }).ToList();
                var q71 = (from bn in _dataContext.BenhNhans
                           join rv in _dataContext.RaViens.Where(p => p.NgayRa >= ngaytunew && p.NgayRa <= ngaydennew) on bn.MaBNhan equals rv.MaBNhan
                           join ttbx in _dataContext.TTboXungs.Where(p => HTThanhToan == 2 ? true : p.HTThanhToan == HTThanhToan) on bn.MaBNhan equals ttbx.MaBNhan
                           where ((id_DoiTuongKham == 99 ? true : bn.IDDTBN == id_DoiTuongKham) && bn.NoiTru == rdBieu.SelectedIndex)
                           select new
                           {
                               MaKCB = bn.MaKCB == null ? "" : bn.MaKCB.Trim().ToUpper(),
                               bn.DChi,
                               bn.HanBHDen,
                               bn.HanBHTu,
                               bn.TuyenDuoi,
                               bn.DTNT,
                               bn.DTuong,
                               bn.NoiTru,
                               bn.NoiTinh,
                               bn.Tuyen,
                               bn.MaBNhan,
                               bn.TenBNhan,
                               bn.NamSinh,
                               bn.NgaySinh,
                               bn.ThangSinh,
                               bn.SThe,
                               bn.GTinh,
                               bn.MaCS,
                               MaDTuong = bn.MaDTuong == null ? "" : bn.MaDTuong.Trim().ToUpper(),
                               bn.CapCuu,
                               bn.Tuoi,
                               bn.KhuVuc,
                               bn.MaBV,
                               bn.SoDK,
                               bn.NNhap,
                               rv.MaICD,
                               rv.NgayVao,
                               rv.NgayRa,
                               rv.SoNgaydt,
                               rv.Status,
                               rv.KetQua,
                               rv.ChanDoan,
                               KhoaTongKet = rv.MaKP,
                               MaKPBn = bn.MaKP
                           }).Distinct().ToList();
                if (rdBieu.SelectedIndex == 2)
                {
                    q71 = (from bn in _dataContext.BenhNhans
                           join rv in _dataContext.RaViens.Where(p => p.NgayRa >= ngaytunew && p.NgayRa <= ngaydennew) on bn.MaBNhan equals rv.MaBNhan
                           join ttbx in _dataContext.TTboXungs.Where(p => HTThanhToan == 2 ? true : p.HTThanhToan == HTThanhToan) on bn.MaBNhan equals ttbx.MaBNhan
                           where ((id_DoiTuongKham == 99 ? true : bn.IDDTBN == id_DoiTuongKham))
                           select new
                           {
                               MaKCB = bn.MaKCB == null ? "" : bn.MaKCB.Trim().ToUpper(),
                               bn.DChi,
                               bn.HanBHDen,
                               bn.HanBHTu,
                               bn.TuyenDuoi,
                               bn.DTNT,
                               bn.DTuong,
                               bn.NoiTru,
                               bn.NoiTinh,
                               bn.Tuyen,
                               bn.MaBNhan,
                               bn.TenBNhan,
                               bn.NamSinh,
                               bn.NgaySinh,
                               bn.ThangSinh,
                               bn.SThe,
                               bn.GTinh,
                               bn.MaCS,
                               MaDTuong = bn.MaDTuong == null ? "" : bn.MaDTuong.Trim().ToUpper(),
                               bn.CapCuu,
                               bn.Tuoi,
                               bn.KhuVuc,
                               bn.MaBV,
                               bn.SoDK,
                               bn.NNhap,
                               rv.MaICD,
                               rv.NgayVao,
                               rv.NgayRa,
                               rv.SoNgaydt,
                               rv.Status,
                               rv.KetQua,
                               rv.ChanDoan,
                               KhoaTongKet = rv.MaKP,
                               MaKPBn = bn.MaKP
                           }).Distinct().ToList();
                }
                var q7 = (from a in q6
                          join bn in q71 on a equals bn.MaBNhan
                          //join bn in _dataContext.BenhNhans on a equals bn.MaBNhan
                          //join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                          //join ttbx in _dataContext.TTboXungs.Where(p => HTThanhToan == 2 ? true : p.HTThanhToan == HTThanhToan) on bn.MaBNhan equals ttbx.MaBNhan
                          //where ((id_DoiTuongKham == 99 ? true : bn.IDDTBN == id_DoiTuongKham) && bn.NoiTru == rdBieu.SelectedIndex)
                          select new
                          {
                              bn.MaKCB,
                              bn.DChi,
                              bn.HanBHDen,
                              bn.HanBHTu,
                              bn.TuyenDuoi,
                              bn.DTNT,
                              bn.DTuong,
                              bn.NoiTru,
                              bn.NoiTinh,
                              bn.Tuyen,
                              bn.MaBNhan,
                              bn.TenBNhan,
                              bn.NamSinh,
                              bn.NgaySinh,
                              bn.ThangSinh,
                              bn.SThe,
                              bn.GTinh,
                              bn.MaCS,
                              bn.MaDTuong,
                              bn.CapCuu,
                              bn.Tuoi,
                              bn.KhuVuc,
                              bn.MaBV,
                              bn.SoDK,
                              bn.MaICD,
                              bn.NgayVao,
                              bn.NgayRa,
                              bn.SoNgaydt,
                              bn.Status,
                              bn.KetQua,
                              bn.ChanDoan,
                              bn.NNhap,
                              bn.MaKPBn,
                              KhoaTongKet = bn.KhoaTongKet
                          }).OrderBy(p => p.MaBNhan).ToList();

                var q4 = (from a in q2.Where(p => ckcDVTheoYC.Checked ? p.LoaiDV == 1 : p.LoaiDV == 0) // check để lấy DV theo yêu cầu
                          join bn in q7 on a.MaBNhan equals bn.MaBNhan
                          join dt in lmaDtuong on bn.MaDTuong equals dt
                          join cskcb in _dsCSKCB on bn.MaKCB equals cskcb
                          where (radioThuChi.SelectedIndex == 2 ? true : (//a.IDTamUng > 0 &&
                          (radioThuChi.SelectedIndex == 0 ? a.ThanhToan == 1 : a.ThanhToan == 0)))
                          && (radio_DTNT.SelectedIndex == 2 ? true : (radio_DTNT.SelectedIndex == 0 ? bn.DTNT == false : bn.DTNT == true))
                          select new
                          {
                              a.XHH,
                              bn.MaKCB,
                              a.TrongBH,
                              a.MaKP,
                              bn.DChi,
                              bn.HanBHDen,
                              bn.HanBHTu,
                              bn.TuyenDuoi,
                              bn.DTNT,
                              bn.DTuong,
                              bn.NoiTru,
                              bn.SoDK,
                              bn.NoiTinh,
                              bn.Tuyen,
                              bn.MaBNhan,
                              bn.TenBNhan,
                              bn.NamSinh,
                              bn.NgaySinh,
                              bn.ThangSinh,
                              bn.SThe,
                              bn.GTinh,
                              bn.MaCS,
                              bn.MaDTuong,
                              bn.CapCuu,
                              bn.MaICD,
                              ChanDoan = (bn.MaICD != null && bn.MaICD != "") ? bn.ChanDoan : "",
                              bn.NgayVao,
                              bn.NgayRa,
                              bn.SoNgaydt,
                              bn.Status,
                              bn.KetQua,
                              a.MaDV,
                              a.ThanhTien,
                              //a.ThanhTienVTYT,
                              a.TienBN,
                              a.TienBH,
                              a.NgayTT,
                              bn.Tuoi,
                              bn.KhuVuc,
                              bn.MaBV,
                              bn.NNhap,
                              TyLeBHTT = bn.DTuong == "BHYT" ? a.TyLeBHTT : 0,
                              KhoaTongKet = bn.KhoaTongKet == null ? "" : MaKPQD(bn.KhoaTongKet),
                              bn.MaKPBn
                          }).OrderBy(p => p.MaBNhan).ToList();
                if (rdBieu.SelectedIndex == 2)
                {
                    q4 = (from a in q2.Where(p => ckcDVTheoYC.Checked ? p.LoaiDV == 1 : p.LoaiDV == 0) // check để lấy DV theo yêu cầu
                          join bn in q7 on a.MaBNhan equals bn.MaBNhan
                          join dt in lmaDtuong on bn.MaDTuong equals dt
                          join cskcb in _dsCSKCB on bn.MaKCB equals cskcb
                          where (radioThuChi.SelectedIndex == 2 ? true : (//a.IDTamUng > 0 &&
                          (radioThuChi.SelectedIndex == 0 ? a.ThanhToan == 1 : a.ThanhToan == 0)))
                          select new
                          {
                              a.XHH,
                              bn.MaKCB,
                              a.TrongBH,
                              a.MaKP,
                              bn.DChi,
                              bn.HanBHDen,
                              bn.HanBHTu,
                              bn.TuyenDuoi,
                              bn.DTNT,
                              bn.DTuong,
                              bn.NoiTru,
                              bn.SoDK,
                              bn.NoiTinh,
                              bn.Tuyen,
                              bn.MaBNhan,
                              bn.TenBNhan,
                              bn.NamSinh,
                              bn.NgaySinh,
                              bn.ThangSinh,
                              bn.SThe,
                              bn.GTinh,
                              bn.MaCS,
                              bn.MaDTuong,
                              bn.CapCuu,
                              bn.MaICD,
                              ChanDoan = (bn.MaICD != null && bn.MaICD != "") ? bn.ChanDoan : "",
                              bn.NgayVao,
                              bn.NgayRa,
                              bn.SoNgaydt,
                              bn.Status,
                              bn.KetQua,
                              a.MaDV,
                              a.ThanhTien,
                              //a.ThanhTienVTYT,
                              a.TienBN,
                              a.TienBH,
                              a.NgayTT,
                              bn.Tuoi,
                              bn.KhuVuc,
                              bn.MaBV,
                              bn.NNhap,
                              TyLeBHTT = bn.DTuong == "BHYT" ? a.TyLeBHTT : 0,
                              KhoaTongKet = MaKPQD(bn.KhoaTongKet),
                              bn.MaKPBn
                          }).OrderBy(p => p.MaBNhan).ToList();
                }
                var listAllKP = _lKphong.Select(o => o.MaKP).ToList();
                var q33 = (from a in q4
                           join dv in _ldv.Where(p => p.MaNhom5937 != null) on a.MaDV equals dv.MaDV
                           where (lupDoituong.Text == "BHYT" ? ((radXP.SelectedIndex < 3 ? a.TuyenDuoi == radXP.SelectedIndex : true) && (cboNoiTinh.SelectedIndex > 0 ? a.NoiTinh == cboNoiTinh.SelectedIndex : true)) : true)
                           where trongBH > 2 ? true : (a.TrongBH == trongBH)
                           //where ckcDVTheoYC.Checked?
                           where chkXHH.Checked ? a.XHH >= 1 : a.XHH == 0
                           //chon nhieu kp
                           where Bien.MaBV == "24012" || Bien.MaBV == "24009" || Bien.MaBV == "27001" || Bien.MaBV == "27023" ? lstMaKP.Contains(a.MaKP ?? 0) : (_MaKPc == 0 ? (listAllKP.Contains(a.MaKP ?? 0)) : (DungChung.Bien.MaBV == "26007" ? a.MaKPBn == _MaKPc : a.MaKP == _MaKPc))//26007 tìm kiếm theo MaKP trong bảng bn Liễu y/c 22-07
                           group new { a, dv } by
                           new
                           {
                               a.TyLeBHTT,
                               a.NNhap,
                               a.MaKCB,
                               a.SoDK,
                               a.HanBHDen,
                               a.HanBHTu,
                               a.DChi,
                               a.SoNgaydt,
                               a.DTNT,
                               a.TuyenDuoi,
                               a.NgayTT,
                               a.DTuong,
                               a.NoiTru,
                               a.MaBNhan,
                               a.NoiTinh,
                               a.TenBNhan,
                               a.NamSinh,
                               a.NgaySinh,
                               a.ThangSinh,
                               a.GTinh,
                               a.SThe,
                               a.MaCS,
                               a.Tuyen,
                               a.NgayVao,
                               a.MaICD,
                               a.ChanDoan,
                               a.NgayRa,
                               a.MaDTuong,
                               a.CapCuu,
                               a.KetQua,
                               a.Status,
                               a.Tuoi,
                               a.KhuVuc,
                               a.MaBV,
                               a.KhoaTongKet
                           } into kq
                           select new
                           {
                               kq.Key.MaKCB,
                               MaKP = _MaKPc == 0 ? "" : MaKPQD(_MaKPc),
                               kq.Key.SoNgaydt,
                               kq.Key.DTuong,
                               kq.Key.NoiTru,
                               TrongBH = rdTrongBH.SelectedIndex,// kq.Key.TrongBH,
                               kq.Key.TuyenDuoi,
                               kq.Key.DTNT,
                               kq.Key.NgayTT,
                               kq.Key.MaDTuong,
                               kq.Key.CapCuu,
                               kq.Key.NgaySinh,
                               kq.Key.ThangSinh,
                               kq.Key.KhuVuc,
                               kq.Key.MaBV,
                               kq.Key.KetQua,
                               kq.Key.Status,
                               kq.Key.KhoaTongKet,
                               kq.Key.SoDK,
                               kq.Key.NNhap,
                               NoiTinh = kq.Key.NoiTinh,
                               Tuyen = kq.Key.Tuyen,
                               MaBNhan = kq.Key.MaBNhan,
                               TenBNhan = kq.Key.TenBNhan,
                               NSinh = kq.Key.NamSinh,
                               SThe = kq.Key.SThe,
                               Nam = kq.Key.GTinh,
                               GTinh = kq.Key.GTinh,
                               MaCS = kq.Key.MaCS,
                               MaICD = kq.Key.MaICD,
                               ChanDoan = kq.Key.ChanDoan,
                               Ngaykham = kq.Key.NgayVao,
                               Ngayra = kq.Key.NgayRa,
                               Tuoi = kq.Key.Tuoi,
                               BHtu = kq.Key.HanBHTu,
                               BHden = kq.Key.HanBHDen,
                               Diachi = kq.Key.DChi,
                               Mabn = kq.Key.MaBNhan,
                               TyLeBHTT = kq.Key.TyLeBHTT,
                               Ma_pttt_qt = String.Join(";", kq.Where(p => p.dv.MaNhom5937 == 10).Select(p => p.dv.MaQD).Where(p => p != null).Distinct()),
                               Thuoc = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 4).Where(p => p.dv.BHTT == 100).Sum(p => p.a.ThanhTien), lamtron),
                               CDHA = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 2 || p.dv.MaNhom5937 == 3).Sum(p => p.a.ThanhTien), lamtron),//(p.dv.IDNhom)).Sum(p => p.a.ThanhTien),
                               Congkham = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 13).Sum(p => p.a.ThanhTien), lamtron),
                               TienGiuong = Math.Round(rdBieu.SelectedIndex == 0 ? kq.Where(p => p.dv.MaNhom5937 == 14 || p.dv.MaNhom5937 == 15).Sum(p => p.a.ThanhTien) : kq.Where(p => p.dv.MaNhom5937 == 14 || p.dv.MaNhom5937 == 15).Sum(p => p.a.ThanhTien), lamtron),
                               Xetnghiem = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 1).Sum(p => p.a.ThanhTien), lamtron),
                               Mau = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 7 || p.dv.MaNhom5937 == 17).Sum(p => p.a.ThanhTien), lamtron),
                               TTPT = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 8 || p.dv.MaNhom5937 == 18).Sum(p => p.a.ThanhTien), lamtron),
                               VTYT = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 10).Sum(p => p.a.ThanhTien), lamtron),
                               DVKT_tl = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 9).Sum(p => p.a.ThanhTien), lamtron),
                               Thuoc_tl = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 6).Sum(p => p.a.ThanhTien), lamtron),
                               VTYT_tl = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 11).Sum(p => p.a.ThanhTien), lamtron),
                               CPVanchuyen = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 12).Sum(p => p.a.ThanhTien), lamtron),
                               CPNgoaiBH = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 12).Sum(p => p.a.ThanhTien), lamtron),
                               ThanhTien = Math.Round(kq.Sum(p => p.a.ThanhTien), lamtron),
                               Tongchi = Math.Round(kq.Sum(p => p.a.ThanhTien), lamtron),
                               Tongcong = Math.Round(kq.Sum(p => p.a.ThanhTien), lamtron),
                               TienBN = Math.Round(kq.Sum(p => p.a.TienBN), lamtron),// Math.Round(kq.Sum(p => p.a.ThanhTien), lamtron) - Math.Round(kq.Sum(p => p.a.ThanhTien) * ((kq.Key.TyLeBHTT) / 100), lamtron),
                               TienBH = Math.Round(kq.Sum(p => p.a.TienBH), lamtron),//Math.Round(kq.Sum(p => p.a.ThanhTien) * ((kq.Key.TyLeBHTT) / 100), lamtron),
                                                                                     //VTYT27022 = Math.Round(kq.Where(p => p.dv.IDNhom == idVTYT).Sum(p => p.a.ThanhTienVTYT), lamtron)
                           }).Where(p => (ckTLBH.Checked && p.TienBH != 0) || (ckTLBN.Checked && p.TienBN != 0) || (!ckTLBH.Checked && !ckTLBN.Checked)).ToList();

                if (DungChung.Bien.MaBV == "30009")
                {
                    q33 = (from a in q4
                           join dv in _ldv.Where(p => p.MaNhom5937 != null) on a.MaDV equals dv.MaDV
                           join bv in _dataContext.BenhViens on a.MaCS equals bv.MaBV
                           where (lupDoituong.Text == "BHYT" ? ((radXP.SelectedIndex < 3 ? a.TuyenDuoi == radXP.SelectedIndex : true) && ((cboNoiTinh.SelectedIndex > 0 ? (a.NoiTinh == cboNoiTinh.SelectedIndex && ((DungChung.Bien.MaBV == "27023" && cboNoiTinh.SelectedIndex == 2 || cboNoiTinh.SelectedIndex == 3) ? bv.MaChuQuan != DungChung.Bien.MaBV : true)) : true))) : true)
                           where trongBH > 2 ? true : (a.TrongBH == trongBH)
                           //where ckcDVTheoYC.Checked?
                           where chkXHH.Checked ? a.XHH >= 1 : a.XHH == 0
                           //chon nhieu kp
                           where Bien.MaBV == "24012" || Bien.MaBV == "24009" || Bien.MaBV == "27001" || Bien.MaBV == "27023" ? lstMaKP.Contains(a.MaKP ?? 0) : (_MaKPc == 0 ? (listAllKP.Contains(a.MaKP ?? 0)) : (DungChung.Bien.MaBV == "26007" ? a.MaKPBn == _MaKPc : a.MaKP == _MaKPc))//26007 tìm kiếm theo MaKP trong bảng bn Liễu y/c 22-07
                           group new { a, dv } by new { a.TyLeBHTT, a.NNhap, a.MaKCB, a.SoDK, a.HanBHDen, a.HanBHTu, a.DChi, a.SoNgaydt, a.DTNT, a.TuyenDuoi, a.NgayTT, a.DTuong, a.NoiTru, a.MaBNhan, a.NoiTinh, a.TenBNhan, a.NamSinh, a.NgaySinh, a.ThangSinh, a.GTinh, a.SThe, a.MaCS, a.Tuyen, a.NgayVao, a.MaICD, a.ChanDoan, a.NgayRa, a.MaDTuong, a.CapCuu, a.KetQua, a.Status, a.Tuoi, a.KhuVuc, a.MaBV, a.KhoaTongKet } into kq
                           select new
                           {
                               kq.Key.MaKCB,
                               MaKP = _MaKPc == 0 ? "" : MaKPQD(_MaKPc),
                               kq.Key.SoNgaydt,
                               kq.Key.DTuong,
                               kq.Key.NoiTru,
                               TrongBH = rdTrongBH.SelectedIndex,// kq.Key.TrongBH,
                               kq.Key.TuyenDuoi,
                               kq.Key.DTNT,
                               kq.Key.NgayTT,
                               kq.Key.MaDTuong,
                               kq.Key.CapCuu,
                               kq.Key.NgaySinh,
                               kq.Key.ThangSinh,
                               kq.Key.KhuVuc,
                               kq.Key.MaBV,
                               kq.Key.KetQua,
                               kq.Key.Status,
                               kq.Key.KhoaTongKet,
                               kq.Key.SoDK,
                               kq.Key.NNhap,
                               NoiTinh = kq.Key.NoiTinh,
                               Tuyen = kq.Key.Tuyen,
                               MaBNhan = kq.Key.MaBNhan,
                               TenBNhan = kq.Key.TenBNhan,
                               NSinh = kq.Key.NamSinh,
                               SThe = kq.Key.SThe,
                               Nam = kq.Key.GTinh,
                               GTinh = kq.Key.GTinh,
                               MaCS = kq.Key.MaCS,
                               MaICD = kq.Key.MaICD,
                               ChanDoan = kq.Key.ChanDoan,
                               Ngaykham = kq.Key.NgayVao,
                               Ngayra = kq.Key.NgayRa,
                               Tuoi = kq.Key.Tuoi,
                               BHtu = kq.Key.HanBHTu,
                               BHden = kq.Key.HanBHDen,
                               Diachi = kq.Key.DChi,
                               Mabn = kq.Key.MaBNhan,
                               TyLeBHTT = kq.Key.TyLeBHTT,
                               Ma_pttt_qt = String.Join(";", kq.Where(p => p.dv.MaNhom5937 == 10).Select(p => p.dv.MaQD).Where(p => p != null).Distinct()),
                               Thuoc = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 4).Where(p => p.dv.BHTT == 100).Sum(p => p.a.ThanhTien), lamtron),
                               CDHA = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 2 || p.dv.MaNhom5937 == 3).Sum(p => p.a.ThanhTien), lamtron),//(p.dv.IDNhom)).Sum(p => p.a.ThanhTien),

                               Congkham = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 13).Sum(p => p.a.ThanhTien), lamtron),
                               TienGiuong = Math.Round(rdBieu.SelectedIndex == 0 ? kq.Where(p => p.dv.MaNhom5937 == 14 || p.dv.MaNhom5937 == 15).Sum(p => p.a.ThanhTien) : kq.Where(p => p.dv.MaNhom5937 == 14 || p.dv.MaNhom5937 == 15).Sum(p => p.a.ThanhTien), lamtron),
                               Xetnghiem = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 1).Sum(p => p.a.ThanhTien), lamtron),
                               Mau = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 7 || p.dv.MaNhom5937 == 17).Sum(p => p.a.ThanhTien), lamtron),
                               TTPT = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 8 || p.dv.MaNhom5937 == 18).Sum(p => p.a.ThanhTien), lamtron),
                               VTYT = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 10).Sum(p => p.a.ThanhTien), lamtron),
                               DVKT_tl = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 9).Sum(p => p.a.ThanhTien), lamtron),
                               Thuoc_tl = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 6).Sum(p => p.a.ThanhTien), lamtron),
                               VTYT_tl = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 11).Sum(p => p.a.ThanhTien), lamtron),
                               CPVanchuyen = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 12).Sum(p => p.a.ThanhTien), lamtron),
                               CPNgoaiBH = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 12).Sum(p => p.a.ThanhTien), lamtron),
                               ThanhTien = Math.Round(kq.Sum(p => p.a.ThanhTien), lamtron),
                               Tongchi = Math.Round(kq.Sum(p => p.a.ThanhTien), lamtron),
                               Tongcong = Math.Round(kq.Sum(p => p.a.ThanhTien), lamtron),
                               TienBN = Math.Round(kq.Sum(p => p.a.TienBN), lamtron),// Math.Round(kq.Sum(p => p.a.ThanhTien), lamtron) - Math.Round(kq.Sum(p => p.a.ThanhTien) * ((kq.Key.TyLeBHTT) / 100), lamtron),
                               TienBH = Math.Round(kq.Sum(p => p.a.TienBH), lamtron),//Math.Round(kq.Sum(p => p.a.ThanhTien) * ((kq.Key.TyLeBHTT) / 100), lamtron),
                                                                                     //VTYT27022 = Math.Round(kq.Where(p => p.dv.IDNhom == idVTYT).Sum(p => p.a.ThanhTienVTYT), lamtron)
                           }).Where(p => (ckTLBH.Checked && p.TienBH != 0) || (ckTLBN.Checked && p.TienBN != 0) || (!ckTLBH.Checked && !ckTLBN.Checked)).ToList();
                }

                //chon nhieu kp
                else if (DungChung.Bien.MaBV != "24012" && DungChung.Bien.MaBV != "24009" && DungChung.Bien.MaBV != "27001" && DungChung.Bien.MaBV != "27023")
                {
                    q33 = (from a in q4
                           join dv in _ldv on a.MaDV equals dv.MaDV
                           where (lupDoituong.Text == "BHYT" ? ((radXP.SelectedIndex < 3 ? a.TuyenDuoi == radXP.SelectedIndex : true) && (cboNoiTinh.SelectedIndex > 0 ? a.NoiTinh == cboNoiTinh.SelectedIndex : true)) : true)
                           where trongBH > 2 ? true : (a.TrongBH == trongBH)
                           //where ckcDVTheoYC.Checked?
                           where chkXHH.Checked ? a.XHH >= 1 : a.XHH == 0
                           where (_MaKPc == 0 ? (listAllKP.Contains(a.MaKP ?? 0)) : (DungChung.Bien.MaBV == "26007" ? a.MaKPBn == _MaKPc : a.MaKP == _MaKPc))//26007 tìm kiếm theo MaKP trong bảng bn Liễu y/c 22-07
                           group new { a, dv } by
                           new
                           {
                               a.TyLeBHTT,
                               a.NNhap,
                               a.MaKCB,
                               a.SoDK,
                               a.HanBHDen,
                               a.HanBHTu,
                               a.DChi,
                               a.SoNgaydt,
                               a.DTNT,
                               a.TuyenDuoi,
                               a.NgayTT,
                               a.DTuong,
                               a.NoiTru,
                               a.MaBNhan,
                               a.NoiTinh,
                               a.TenBNhan,
                               a.NamSinh,
                               a.NgaySinh,
                               a.ThangSinh,
                               a.GTinh,
                               a.SThe,
                               a.MaCS,
                               a.Tuyen,
                               a.NgayVao,
                               a.MaICD,
                               a.ChanDoan,
                               a.NgayRa,
                               a.MaDTuong,
                               a.CapCuu,
                               a.KetQua,
                               a.Status,
                               a.Tuoi,
                               a.KhuVuc,
                               a.MaBV,
                               a.KhoaTongKet
                           } into kq
                           select new
                           {
                               kq.Key.MaKCB,
                               MaKP = _MaKPc == 0 ? "" : MaKPQD(_MaKPc),
                               kq.Key.SoNgaydt,
                               kq.Key.DTuong,
                               kq.Key.NoiTru,
                               TrongBH = rdTrongBH.SelectedIndex,// kq.Key.TrongBH,
                               kq.Key.TuyenDuoi,
                               kq.Key.DTNT,
                               kq.Key.NgayTT,
                               kq.Key.MaDTuong,
                               kq.Key.CapCuu,
                               kq.Key.NgaySinh,
                               kq.Key.ThangSinh,
                               kq.Key.KhuVuc,
                               kq.Key.MaBV,
                               kq.Key.KetQua,
                               kq.Key.Status,
                               kq.Key.KhoaTongKet,
                               kq.Key.SoDK,
                               kq.Key.NNhap,
                               NoiTinh = kq.Key.NoiTinh,
                               Tuyen = kq.Key.Tuyen,
                               MaBNhan = kq.Key.MaBNhan,
                               TenBNhan = kq.Key.TenBNhan,
                               NSinh = kq.Key.NamSinh,
                               SThe = kq.Key.SThe,
                               Nam = kq.Key.GTinh,
                               GTinh = kq.Key.GTinh,
                               MaCS = kq.Key.MaCS,
                               MaICD = kq.Key.MaICD,
                               ChanDoan = kq.Key.ChanDoan,
                               Ngaykham = kq.Key.NgayVao,
                               Ngayra = kq.Key.NgayRa,
                               Tuoi = kq.Key.Tuoi,
                               BHtu = kq.Key.HanBHTu,
                               BHden = kq.Key.HanBHDen,
                               Diachi = kq.Key.DChi,
                               Mabn = kq.Key.MaBNhan,
                               TyLeBHTT = kq.Key.TyLeBHTT,
                               Ma_pttt_qt = String.Join(";", kq.Where(p => p.dv.MaNhom5937 == 10).Select(p => p.dv.MaQD).Where(p => p != null).Distinct()),
                               Thuoc = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 4).Where(p => p.dv.BHTT == 100).Sum(p => p.a.ThanhTien), lamtron),
                               CDHA = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 2 || p.dv.MaNhom5937 == 3).Sum(p => p.a.ThanhTien), lamtron),//(p.dv.IDNhom)).Sum(p => p.a.ThanhTien),

                               Congkham = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 13).Sum(p => p.a.ThanhTien), lamtron),
                               TienGiuong = Math.Round(rdBieu.SelectedIndex == 0 ? kq.Where(p => p.dv.MaNhom5937 == 14 || p.dv.MaNhom5937 == 15).Sum(p => p.a.ThanhTien) : kq.Where(p => p.dv.MaNhom5937 == 14 || p.dv.MaNhom5937 == 15).Sum(p => p.a.ThanhTien), lamtron),
                               Xetnghiem = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 1).Sum(p => p.a.ThanhTien), lamtron),
                               Mau = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 7 || p.dv.MaNhom5937 == 17).Sum(p => p.a.ThanhTien), lamtron),
                               TTPT = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 8 || p.dv.MaNhom5937 == 18).Sum(p => p.a.ThanhTien), lamtron),
                               VTYT = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 10).Sum(p => p.a.ThanhTien), lamtron),
                               DVKT_tl = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 9).Sum(p => p.a.ThanhTien), lamtron),
                               Thuoc_tl = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 6).Sum(p => p.a.ThanhTien), lamtron),
                               VTYT_tl = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 11).Sum(p => p.a.ThanhTien), lamtron),
                               CPVanchuyen = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 12).Sum(p => p.a.ThanhTien), lamtron),
                               CPNgoaiBH = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 12).Sum(p => p.a.ThanhTien), lamtron),
                               ThanhTien = Math.Round(kq.Sum(p => p.a.ThanhTien), lamtron),
                               Tongchi = Math.Round(kq.Sum(p => p.a.ThanhTien), lamtron),
                               Tongcong = Math.Round(kq.Sum(p => p.a.ThanhTien), lamtron),
                               TienBN = Math.Round(kq.Sum(p => p.a.TienBN), lamtron),// Math.Round(kq.Sum(p => p.a.ThanhTien), lamtron) - Math.Round(kq.Sum(p => p.a.ThanhTien) * ((kq.Key.TyLeBHTT) / 100), lamtron),
                               TienBH = Math.Round(kq.Sum(p => p.a.TienBH), lamtron),//Math.Round(kq.Sum(p => p.a.ThanhTien) * ((kq.Key.TyLeBHTT) / 100), lamtron),
                                                                                     //VTYT27022 = Math.Round(kq.Where(p => p.dv.IDNhom == idVTYT).Sum(p => p.a.ThanhTienVTYT), lamtron)
                           }).Where(p => (ckTLBH.Checked && p.TienBH != 0) || (ckTLBN.Checked && p.TienBN != 0) || (!ckTLBH.Checked && !ckTLBN.Checked)).ToList();
                }
                if (rdBieu.SelectedIndex == 2)
                {
                    q33 = (from a in q4
                           join dv in _ldv.Where(p => p.MaNhom5937 != null) on a.MaDV equals dv.MaDV
                           where (lupDoituong.Text == "BHYT" ? ((radXP.SelectedIndex < 3 ? a.TuyenDuoi == radXP.SelectedIndex : true) && (cboNoiTinh.SelectedIndex > 0 ? a.NoiTinh == cboNoiTinh.SelectedIndex : true)) : true)
                           where trongBH > 2 ? true : (a.TrongBH == trongBH)
                           //where ckcDVTheoYC.Checked?
                           //where chkXHH.Checked ? a.XHH >= 1 : a.XHH == 0
                           //where (_MaKPc == 0 ? (listAllKP.Contains(a.MaKP ?? 0)) : a.MaKP == _MaKPc)
                           group new { a, dv } by
                           new
                           {
                               a.TyLeBHTT,
                               a.NNhap,
                               a.MaKCB,
                               a.SoDK,
                               a.HanBHDen,
                               a.HanBHTu,
                               a.DChi,
                               a.SoNgaydt,
                               a.DTNT,
                               a.TuyenDuoi,
                               a.NgayTT,
                               a.DTuong,
                               a.NoiTru,
                               a.MaBNhan,
                               a.NoiTinh,
                               a.TenBNhan,
                               a.NamSinh,
                               a.NgaySinh,
                               a.ThangSinh,
                               a.GTinh,
                               a.SThe,
                               a.MaCS,
                               a.Tuyen,
                               a.NgayVao,
                               a.MaICD,
                               a.ChanDoan,
                               a.NgayRa,
                               a.MaDTuong,
                               a.CapCuu,
                               a.KetQua,
                               a.Status,
                               a.Tuoi,
                               a.KhuVuc,
                               a.MaBV,
                               a.KhoaTongKet
                           } into kq
                           select new
                           {
                               kq.Key.MaKCB,
                               MaKP = _MaKPc == 0 ? "" : MaKPQD(_MaKPc),
                               kq.Key.SoNgaydt,
                               kq.Key.DTuong,
                               kq.Key.NoiTru,
                               TrongBH = rdTrongBH.SelectedIndex,// kq.Key.TrongBH,
                               kq.Key.TuyenDuoi,
                               kq.Key.DTNT,
                               kq.Key.NgayTT,
                               kq.Key.MaDTuong,
                               kq.Key.CapCuu,
                               kq.Key.NgaySinh,
                               kq.Key.ThangSinh,
                               kq.Key.KhuVuc,
                               kq.Key.MaBV,
                               kq.Key.KetQua,
                               kq.Key.Status,
                               kq.Key.KhoaTongKet,
                               kq.Key.SoDK,
                               kq.Key.NNhap,
                               NoiTinh = kq.Key.NoiTinh,
                               Tuyen = kq.Key.Tuyen,
                               MaBNhan = kq.Key.MaBNhan,
                               TenBNhan = kq.Key.TenBNhan,
                               NSinh = kq.Key.NamSinh,
                               SThe = kq.Key.SThe,
                               Nam = kq.Key.GTinh,
                               GTinh = kq.Key.GTinh,
                               MaCS = kq.Key.MaCS,
                               MaICD = kq.Key.MaICD,
                               ChanDoan = kq.Key.ChanDoan,
                               Ngaykham = kq.Key.NgayVao,
                               Ngayra = kq.Key.NgayRa,
                               Tuoi = kq.Key.Tuoi,
                               BHtu = kq.Key.HanBHTu,
                               BHden = kq.Key.HanBHDen,
                               Diachi = kq.Key.DChi,
                               Mabn = kq.Key.MaBNhan,
                               TyLeBHTT = kq.Key.TyLeBHTT,
                               Ma_pttt_qt = String.Join(";", kq.Where(p => p.dv.MaNhom5937 == 10).Select(p => p.dv.MaQD).Where(p => p != null).Distinct()),
                               Thuoc = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 4).Where(p => p.dv.BHTT == 100).Sum(p => p.a.ThanhTien), lamtron),
                               CDHA = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 2 || p.dv.MaNhom5937 == 3).Sum(p => p.a.ThanhTien), lamtron),//(p.dv.IDNhom)).Sum(p => p.a.ThanhTien),

                               Congkham = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 13).Sum(p => p.a.ThanhTien), lamtron),
                               TienGiuong = Math.Round(rdBieu.SelectedIndex == 0 ? kq.Where(p => p.dv.MaNhom5937 == 14 || p.dv.MaNhom5937 == 15).Sum(p => p.a.ThanhTien) : kq.Where(p => p.dv.MaNhom5937 == 14 || p.dv.MaNhom5937 == 15).Sum(p => p.a.ThanhTien), lamtron),
                               Xetnghiem = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 1).Sum(p => p.a.ThanhTien), lamtron),
                               Mau = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 7 || p.dv.MaNhom5937 == 17).Sum(p => p.a.ThanhTien), lamtron),
                               TTPT = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 8 || p.dv.MaNhom5937 == 18).Sum(p => p.a.ThanhTien), lamtron),
                               VTYT = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 10).Sum(p => p.a.ThanhTien), lamtron),
                               DVKT_tl = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 9).Sum(p => p.a.ThanhTien), lamtron),
                               Thuoc_tl = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 6).Sum(p => p.a.ThanhTien), lamtron),
                               VTYT_tl = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 11).Sum(p => p.a.ThanhTien), lamtron),
                               CPVanchuyen = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 12).Sum(p => p.a.ThanhTien), lamtron),
                               CPNgoaiBH = Math.Round(kq.Where(p => p.dv.MaNhom5937 == 12).Sum(p => p.a.ThanhTien), lamtron),
                               ThanhTien = Math.Round(kq.Sum(p => p.a.ThanhTien), lamtron),
                               Tongchi = Math.Round(kq.Sum(p => p.a.ThanhTien), lamtron),
                               Tongcong = Math.Round(kq.Sum(p => p.a.ThanhTien), lamtron),
                               TienBN = Math.Round(kq.Sum(p => p.a.TienBN), lamtron),// Math.Round(kq.Sum(p => p.a.ThanhTien), lamtron) - Math.Round(kq.Sum(p => p.a.ThanhTien) * ((kq.Key.TyLeBHTT) / 100), lamtron),
                               TienBH = Math.Round(kq.Sum(p => p.a.TienBH), lamtron),//Math.Round(kq.Sum(p => p.a.ThanhTien) * ((kq.Key.TyLeBHTT) / 100), lamtron),
                               //VTYT27022 = Math.Round(kq.Where(p => p.dv.IDNhom == idVTYT).Sum(p => p.a.ThanhTienVTYT), lamtron)
                           }).Where(p => (ckTLBH.Checked && p.TienBH != 0) || (ckTLBN.Checked && p.TienBN != 0) || (!ckTLBH.Checked && !ckTLBN.Checked)).ToList();
                }

                var q3 = q33.Select(x => new
                {
                    x.MaKCB,
                    x.MaKP,
                    x.SoNgaydt,
                    x.DTuong,
                    x.NoiTru,
                    x.TrongBH,
                    x.TuyenDuoi,
                    x.DTNT,
                    x.NgayTT,
                    x.MaDTuong,
                    x.CapCuu,
                    x.NgaySinh,
                    x.ThangSinh,
                    x.KhuVuc,
                    x.MaBV,
                    x.KetQua,
                    x.Status,
                    x.KhoaTongKet,
                    x.SoDK,
                    x.NNhap,
                    NoiTinh = x.NoiTinh,
                    Tuyen = x.Tuyen,
                    MaBNhan = x.MaBNhan,
                    TenBNhan = x.TenBNhan,
                    NSinh = x.NSinh,
                    SThe = x.SThe,
                    Nam = x.GTinh,
                    GTinh = x.GTinh,
                    MaCS = x.MaCS,
                    MaICD = x.MaICD,
                    ChanDoan = x.ChanDoan,
                    Ngaykham = x.Ngaykham,
                    Ngayra = x.Ngayra,
                    Tuoi = x.Tuoi,
                    BHtu = x.BHtu,
                    BHden = x.BHden,
                    Diachi = x.Diachi,
                    Mabn = x.MaBNhan,
                    TyLeBHTT = x.TyLeBHTT,
                    x.Ma_pttt_qt,
                    Tong = Math.Round(x.Thuoc, lamtron) + Math.Round(x.CDHA, lamtron) + Math.Round(x.Congkham, lamtron) + Math.Round(x.TienGiuong, lamtron) + Math.Round(x.Xetnghiem, lamtron) + Math.Round(x.Mau, lamtron) + Math.Round(x.TTPT, lamtron) + Math.Round(x.VTYT, lamtron) + Math.Round(x.VTYT_tl, lamtron) + Math.Round(x.DVKT_tl, lamtron) + Math.Round(x.Thuoc_tl, lamtron) + Math.Round(x.CPVanchuyen, lamtron),

                    Thuoc = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (x.Thuoc) : (_CP_BH == 1 ? (x.Thuoc * (x.TyLeBHTT / 100)) : (x.Thuoc - x.Thuoc * (x.TyLeBHTT / 100))), lamtron) : Math.Round(x.Thuoc, lamtron),
                    CDHA = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (x.CDHA) : (_CP_BH == 1 ? (x.CDHA * (x.TyLeBHTT / 100)) : (x.CDHA - x.CDHA * (x.TyLeBHTT / 100))), lamtron) : Math.Round(x.CDHA, lamtron),//(p.dv.IDNhom)).Sum(p => p.a.ThanhTien),

                    Congkham = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (x.Congkham) : (_CP_BH == 1 ? (x.Congkham * (x.TyLeBHTT / 100)) : (x.Congkham - x.Congkham * (x.TyLeBHTT / 100))), lamtron) : Math.Round(x.Congkham, lamtron),
                    TienGiuong = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (x.TienGiuong) : (_CP_BH == 1 ? (x.TienGiuong * (x.TyLeBHTT / 100)) : (x.TienGiuong - x.TienGiuong * (x.TyLeBHTT / 100))), lamtron) : Math.Round(x.TienGiuong, lamtron),
                    Xetnghiem = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (x.Xetnghiem) : (_CP_BH == 1 ? (x.Xetnghiem * (x.TyLeBHTT / 100)) : (x.Xetnghiem - x.Xetnghiem * (x.TyLeBHTT / 100))), lamtron) : Math.Round(x.Xetnghiem, lamtron),
                    Mau = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (x.Mau) : (_CP_BH == 1 ? (x.Mau * (x.TyLeBHTT / 100)) : (x.Mau - x.Mau * (x.TyLeBHTT / 100))), lamtron) : Math.Round(x.Mau, lamtron),
                    TTPT = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (x.TTPT) : (_CP_BH == 1 ? (x.TTPT * (x.TyLeBHTT / 100)) : (x.TTPT - x.TTPT * (x.TyLeBHTT / 100))), lamtron) : Math.Round(x.TTPT, lamtron),
                    VTYT = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (x.VTYT) : (_CP_BH == 1 ? (x.VTYT * (x.TyLeBHTT / 100)) : (x.VTYT - x.VTYT * (x.TyLeBHTT / 100))), lamtron) : Math.Round(x.VTYT, lamtron),
                    DVKT_tl = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (x.DVKT_tl) : (_CP_BH == 1 ? (x.DVKT_tl * (x.TyLeBHTT / 100)) : (x.DVKT_tl - x.DVKT_tl * (x.TyLeBHTT / 100))), lamtron) : Math.Round(x.DVKT_tl, lamtron),
                    Thuoc_tl = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (x.Thuoc_tl) : (_CP_BH == 1 ? (x.Thuoc_tl * (x.TyLeBHTT / 100)) : (x.Thuoc_tl - x.Thuoc_tl * (x.TyLeBHTT / 100))), lamtron) : Math.Round(x.Thuoc_tl, lamtron),
                    VTYT_tl = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (x.VTYT_tl) : (_CP_BH == 1 ? (x.VTYT_tl * (x.TyLeBHTT / 100)) : (x.VTYT_tl - x.VTYT_tl * (x.TyLeBHTT / 100))), lamtron) : Math.Round(x.VTYT_tl, lamtron),
                    CPVanchuyen = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (x.CPVanchuyen) : (_CP_BH == 1 ? (x.CPVanchuyen * (x.TyLeBHTT / 100)) : (x.CPVanchuyen - x.CPVanchuyen * (x.TyLeBHTT / 100))), lamtron) : Math.Round(x.CPVanchuyen, lamtron),
                    CPNgoaiBH = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (x.CPNgoaiBH) : (_CP_BH == 1 ? (x.CPNgoaiBH * (x.TyLeBHTT / 100)) : (x.CPNgoaiBH - x.CPNgoaiBH * (x.TyLeBHTT / 100))), lamtron) : Math.Round(x.CPNgoaiBH, lamtron),
                    ThanhTien = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (x.ThanhTien) : (_CP_BH == 1 ? (x.ThanhTien * (x.TyLeBHTT / 100)) : (x.ThanhTien - x.ThanhTien * (x.TyLeBHTT / 100))), lamtron) : Math.Round(x.ThanhTien, lamtron),
                    Tongchi = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (x.Tongchi) : (_CP_BH == 1 ? (x.Tongchi * (x.TyLeBHTT / 100)) : (x.Tongchi - x.Tongchi * (x.TyLeBHTT / 100))), lamtron) : Math.Round(x.Tongchi, lamtron),
                    Tongcong = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (x.Tongcong) : (_CP_BH == 1 ? (x.Tongcong * (x.TyLeBHTT / 100)) : (x.Tongcong - x.Tongcong * (x.TyLeBHTT / 100))), lamtron) : Math.Round(x.Tongcong, lamtron),
                    TienBH = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (x.Tongchi) : (x.Tongchi * (x.TyLeBHTT / 100)), lamtron) : Math.Round(x.TienBH, lamtron),
                    TienBN = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (x.Tongchi) : (x.Tongchi - x.Tongchi * (x.TyLeBHTT / 100)), lamtron) : Math.Round(x.TienBN, lamtron),
                }).ToList();

                var ss = q3.GroupBy(g => new
                {
                    g.MaKCB,
                    g.MaKP,
                    g.SoNgaydt,
                    g.DTuong,
                    g.NoiTru,
                    g.TrongBH,
                    g.TuyenDuoi,
                    g.DTNT,
                    g.NgayTT,
                    g.MaDTuong,
                    g.CapCuu,
                    g.NgaySinh,
                    g.ThangSinh,
                    g.KhuVuc,
                    g.MaBV,
                    g.KetQua,
                    g.Status,
                    g.KhoaTongKet,
                    g.SoDK,
                    g.NNhap,
                    g.NoiTinh,
                    g.Tuyen,
                    g.MaBNhan,
                    g.TenBNhan,
                    g.NSinh,
                    g.SThe,
                    g.Nam,
                    g.GTinh,
                    g.MaCS,
                    g.MaICD,
                    g.ChanDoan,
                    g.Ngaykham,
                    g.Ngayra,
                    g.Tuoi,
                    g.BHtu,
                    g.BHden,
                    g.Diachi,
                    g.Mabn,
                    g.Ma_pttt_qt,
                }).Select(s => new
                {
                    s.Key.MaKCB,
                    s.Key.MaKP,
                    s.Key.SoNgaydt,
                    s.Key.DTuong,
                    s.Key.NoiTru,
                    s.Key.TrongBH,
                    s.Key.TuyenDuoi,
                    s.Key.DTNT,
                    s.Key.NgayTT,
                    s.Key.MaDTuong,
                    s.Key.CapCuu,
                    s.Key.NgaySinh,
                    s.Key.ThangSinh,
                    s.Key.KhuVuc,
                    s.Key.MaBV,
                    s.Key.KetQua,
                    s.Key.Status,
                    s.Key.KhoaTongKet,
                    s.Key.SoDK,
                    s.Key.NNhap,
                    s.Key.NoiTinh,
                    s.Key.Tuyen,
                    s.Key.MaBNhan,
                    s.Key.TenBNhan,
                    s.Key.NSinh,
                    s.Key.SThe,
                    s.Key.Nam,
                    s.Key.GTinh,
                    s.Key.MaCS,
                    s.Key.MaICD,
                    s.Key.ChanDoan,
                    s.Key.Ngaykham,
                    s.Key.Ngayra,
                    s.Key.Tuoi,
                    s.Key.BHtu,
                    s.Key.BHden,
                    s.Key.Diachi,
                    s.Key.Mabn,
                    TyLeBHTT = s.OrderByDescending(x => x.NNhap).Select(x => x.TyLeBHTT).FirstOrDefault(),
                    s.Key.Ma_pttt_qt,
                    Tong = s.Sum(o => o.Tong),
                    Thuoc = s.Sum(o => o.Thuoc),
                    CDHA = s.Sum(o => o.CDHA),
                    Congkham = s.Sum(o => o.Congkham),
                    TienGiuong = s.Sum(o => o.TienGiuong),
                    Xetnghiem = s.Sum(o => o.Xetnghiem),
                    Mau = s.Sum(o => o.Mau),
                    TTPT = s.Sum(o => o.TTPT),
                    VTYT = s.Sum(o => o.VTYT),
                    DVKT_tl = s.Sum(o => o.DVKT_tl),
                    Thuoc_tl = s.Sum(o => o.Thuoc_tl),
                    VTYT_tl = s.Sum(o => o.VTYT_tl),
                    CPVanchuyen = s.Sum(o => o.CPVanchuyen),
                    CPNgoaiBH = s.Sum(o => o.CPNgoaiBH),
                    ThanhTien = s.Sum(o => o.ThanhTien),
                    Tongchi = s.Sum(o => o.Tongchi),
                    Tongcong = s.Sum(o => o.Tongcong),
                    TienBH = s.Sum(o => o.TienBH),
                    TienBN = s.Sum(o => o.TienBN),

                }).ToList();

                if (DungChung.Bien.MaBV == "24012" && _lDSMaICD.Count() > 0)
                {
                    q3 = (from a in q33
                          join b in _dataContext.BenhNhans on a.TenBNhan equals b.TenBNhan
                          join c in _dataContext.BNKBs on a.MaBNhan equals c.MaBNhan
                          join d in _lDSMaICD on c.MaICD equals d
                          select new
                          {
                              a.MaKCB,
                              a.MaKP,
                              a.SoNgaydt,
                              a.DTuong,
                              a.NoiTru,
                              a.TrongBH,
                              a.TuyenDuoi,
                              a.DTNT,
                              a.NgayTT,
                              a.MaDTuong,
                              a.CapCuu,
                              a.NgaySinh,
                              a.ThangSinh,
                              a.KhuVuc,
                              a.MaBV,
                              a.KetQua,
                              a.Status,
                              a.KhoaTongKet,
                              a.SoDK,
                              a.NNhap,
                              NoiTinh = a.NoiTinh,
                              Tuyen = a.Tuyen,
                              MaBNhan = a.MaBNhan,
                              TenBNhan = a.TenBNhan,
                              NSinh = a.NSinh,
                              SThe = a.SThe,
                              Nam = a.GTinh,
                              GTinh = a.GTinh,
                              MaCS = a.MaCS,
                              MaICD = ck_HienThiMaICD2.Checked ? a.MaICD : c.MaICD,
                              ChanDoan = a.ChanDoan,
                              Ngaykham = a.Ngaykham,
                              Ngayra = a.Ngayra,
                              Tuoi = a.Tuoi,
                              BHtu = a.BHtu,
                              BHden = a.BHden,
                              Diachi = a.Diachi,
                              Mabn = a.MaBNhan,
                              TyLeBHTT = a.TyLeBHTT,
                              a.Ma_pttt_qt,
                              Tong = Math.Round(a.Thuoc, lamtron) +
                                      Math.Round(a.CDHA, lamtron) +
                                      Math.Round(a.Congkham, lamtron) +
                                      Math.Round(a.TienGiuong, lamtron) +
                                      Math.Round(a.Xetnghiem, lamtron) +
                                      Math.Round(a.Mau, lamtron) +
                                      Math.Round(a.TTPT, lamtron) +
                                      Math.Round(a.VTYT, lamtron) +
                                      Math.Round(a.VTYT_tl, lamtron) +
                                      Math.Round(a.DVKT_tl, lamtron) +
                                      Math.Round(a.Thuoc_tl, lamtron) +
                                      Math.Round(a.CPVanchuyen, lamtron),

                              Thuoc = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (a.Thuoc) :
                                      (_CP_BH == 1 ? (a.Thuoc * (a.TyLeBHTT / 100)) :
                                      (a.Thuoc - a.Thuoc * (a.TyLeBHTT / 100))), lamtron) :
                                      Math.Round(a.Thuoc, lamtron),

                              CDHA = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (a.CDHA) :
                                      (_CP_BH == 1 ? (a.CDHA * (a.TyLeBHTT / 100)) :
                                      (a.CDHA - a.CDHA * (a.TyLeBHTT / 100))), lamtron) :
                                      Math.Round(a.CDHA, lamtron),//(p.dv.IDNhom)).Sum(p => p.a.ThanhTien),

                              Congkham = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (a.Congkham) :
                                          (_CP_BH == 1 ? (a.Congkham * (a.TyLeBHTT / 100)) :
                                          (a.Congkham - a.Congkham * (a.TyLeBHTT / 100))), lamtron) :
                                          Math.Round(a.Congkham, lamtron),

                              TienGiuong = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (a.TienGiuong) :
                                              (_CP_BH == 1 ? (a.TienGiuong * (a.TyLeBHTT / 100)) :
                                              (a.TienGiuong - a.TienGiuong * (a.TyLeBHTT / 100))), lamtron) :
                                              Math.Round(a.TienGiuong, lamtron),

                              Xetnghiem = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (a.Xetnghiem) :
                                          (_CP_BH == 1 ? (a.Xetnghiem * (a.TyLeBHTT / 100)) :
                                          (a.Xetnghiem - a.Xetnghiem * (a.TyLeBHTT / 100))), lamtron) :
                                          Math.Round(a.Xetnghiem, lamtron),

                              Mau = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (a.Mau) :
                                        (_CP_BH == 1 ? (a.Mau * (a.TyLeBHTT / 100)) :
                                        (a.Mau - a.Mau * (a.TyLeBHTT / 100))), lamtron) :
                                        Math.Round(a.Mau, lamtron),

                              TTPT = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (a.TTPT) :
                                      (_CP_BH == 1 ? (a.TTPT * (a.TyLeBHTT / 100)) :
                                      (a.TTPT - a.TTPT * (a.TyLeBHTT / 100))), lamtron) :
                                      Math.Round(a.TTPT, lamtron),

                              VTYT = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (a.VTYT) :
                                      (_CP_BH == 1 ? (a.VTYT * (a.TyLeBHTT / 100)) :
                                      (a.VTYT - a.VTYT * (a.TyLeBHTT / 100))), lamtron) :
                                      Math.Round(a.VTYT, lamtron),

                              DVKT_tl = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (a.DVKT_tl) :
                                      (_CP_BH == 1 ? (a.DVKT_tl * (a.TyLeBHTT / 100)) :
                                      (a.DVKT_tl - a.DVKT_tl * (a.TyLeBHTT / 100))), lamtron) :
                                      Math.Round(a.DVKT_tl, lamtron),

                              Thuoc_tl = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (a.Thuoc_tl) :
                                      (_CP_BH == 1 ? (a.Thuoc_tl * (a.TyLeBHTT / 100)) :
                                      (a.Thuoc_tl - a.Thuoc_tl * (a.TyLeBHTT / 100))), lamtron) :
                                      Math.Round(a.Thuoc_tl, lamtron),

                              VTYT_tl = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (a.VTYT_tl) :
                                      (_CP_BH == 1 ? (a.VTYT_tl * (a.TyLeBHTT / 100)) :
                                      (a.VTYT_tl - a.VTYT_tl * (a.TyLeBHTT / 100))), lamtron) :
                                      Math.Round(a.VTYT_tl, lamtron),

                              CPVanchuyen = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (a.CPVanchuyen) :
                                      (_CP_BH == 1 ? (a.CPVanchuyen * (a.TyLeBHTT / 100)) :
                                      (a.CPVanchuyen - a.CPVanchuyen * (a.TyLeBHTT / 100))), lamtron) :
                                      Math.Round(a.CPVanchuyen, lamtron),

                              CPNgoaiBH = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (a.CPNgoaiBH) :
                                      (_CP_BH == 1 ? (a.CPNgoaiBH * (a.TyLeBHTT / 100)) :
                                      (a.CPNgoaiBH - a.CPNgoaiBH * (a.TyLeBHTT / 100))), lamtron) :
                                      Math.Round(a.CPNgoaiBH, lamtron),

                              ThanhTien = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (a.ThanhTien) :
                                      (_CP_BH == 1 ? (a.ThanhTien * (a.TyLeBHTT / 100)) :
                                      (a.ThanhTien - a.ThanhTien * (a.TyLeBHTT / 100))), lamtron) :
                                      Math.Round(a.ThanhTien, lamtron),

                              Tongchi = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (a.Tongchi) :
                                      (_CP_BH == 1 ? (a.Tongchi * (a.TyLeBHTT / 100)) :
                                      (a.Tongchi - a.Tongchi * (a.TyLeBHTT / 100))), lamtron) :
                                      Math.Round(a.Tongchi, lamtron),

                              Tongcong = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (a.Tongcong) :
                                      (_CP_BH == 1 ? (a.Tongcong * (a.TyLeBHTT / 100)) :
                                      (a.Tongcong - a.Tongcong * (a.TyLeBHTT / 100))), lamtron) :
                                      Math.Round(a.Tongcong, lamtron),

                              TienBH = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (a.Tongchi) :
                                      (a.Tongchi * (a.TyLeBHTT / 100)), lamtron) : Math.Round(a.TienBH, lamtron),
                              TienBN = chkLamTron.Checked ? Math.Round(_CP_BH == 0 ? (a.Tongchi) :
                                      (a.Tongchi - a.Tongchi * (a.TyLeBHTT / 100)), lamtron) :
                                      Math.Round(a.TienBN, lamtron),
                          }).Distinct().ToList();

                    ss = q3.GroupBy(g => new
                    {
                        g.MaKCB,
                        g.MaKP,
                        g.SoNgaydt,
                        g.DTuong,
                        g.NoiTru,
                        g.TrongBH,
                        g.TuyenDuoi,
                        g.DTNT,
                        g.NgayTT,
                        g.MaDTuong,
                        g.CapCuu,
                        g.NgaySinh,
                        g.ThangSinh,
                        g.KhuVuc,
                        g.MaBV,
                        g.KetQua,
                        g.Status,
                        g.KhoaTongKet,
                        g.SoDK,
                        g.NNhap,
                        g.NoiTinh,
                        g.Tuyen,
                        g.MaBNhan,
                        g.TenBNhan,
                        g.NSinh,
                        g.SThe,
                        g.Nam,
                        g.GTinh,
                        g.MaCS,
                        g.MaICD,
                        g.ChanDoan,
                        g.Ngaykham,
                        g.Ngayra,
                        g.Tuoi,
                        g.BHtu,
                        g.BHden,
                        g.Diachi,
                        g.Mabn,
                        //g.TyLeBHTT,
                        g.Ma_pttt_qt,
                    }).Select(s => new
                    {
                        s.Key.MaKCB,
                        s.Key.MaKP,
                        s.Key.SoNgaydt,
                        s.Key.DTuong,
                        s.Key.NoiTru,
                        s.Key.TrongBH,
                        s.Key.TuyenDuoi,
                        s.Key.DTNT,
                        s.Key.NgayTT,
                        s.Key.MaDTuong,
                        s.Key.CapCuu,
                        s.Key.NgaySinh,
                        s.Key.ThangSinh,
                        s.Key.KhuVuc,
                        s.Key.MaBV,
                        s.Key.KetQua,
                        s.Key.Status,
                        s.Key.KhoaTongKet,
                        s.Key.SoDK,
                        s.Key.NNhap,
                        s.Key.NoiTinh,
                        s.Key.Tuyen,
                        s.Key.MaBNhan,
                        s.Key.TenBNhan,
                        s.Key.NSinh,
                        s.Key.SThe,
                        s.Key.Nam,
                        s.Key.GTinh,
                        s.Key.MaCS,
                        s.Key.MaICD,
                        s.Key.ChanDoan,
                        s.Key.Ngaykham,
                        s.Key.Ngayra,
                        s.Key.Tuoi,
                        s.Key.BHtu,
                        s.Key.BHden,
                        s.Key.Diachi,
                        s.Key.Mabn,
                        TyLeBHTT = s.OrderByDescending(x => x.NNhap).Select(x => x.TyLeBHTT).FirstOrDefault(),
                        s.Key.Ma_pttt_qt,

                        Tong = s.Sum(o => o.Tong),
                        Thuoc = s.Sum(o => o.Thuoc),
                        CDHA = s.Sum(o => o.CDHA),
                        Congkham = s.Sum(o => o.Congkham),
                        TienGiuong = s.Sum(o => o.TienGiuong),
                        Xetnghiem = s.Sum(o => o.Xetnghiem),
                        Mau = s.Sum(o => o.Mau),
                        TTPT = s.Sum(o => o.TTPT),
                        VTYT = s.Sum(o => o.VTYT),
                        DVKT_tl = s.Sum(o => o.DVKT_tl),
                        Thuoc_tl = s.Sum(o => o.Thuoc_tl),
                        VTYT_tl = s.Sum(o => o.VTYT_tl),
                        CPVanchuyen = s.Sum(o => o.CPVanchuyen),
                        CPNgoaiBH = s.Sum(o => o.CPNgoaiBH),
                        ThanhTien = s.Sum(o => o.ThanhTien),
                        Tongchi = s.Sum(o => o.Tongchi),
                        Tongcong = s.Sum(o => o.Tongcong),
                        TienBH = s.Sum(o => o.TienBH),
                        TienBN = s.Sum(o => o.TienBN),

                    }).ToList();
                }
                var qvv = (from a in q6
                           join vv in _dataContext.VaoViens
                           on a equals vv.MaBNhan
                           select vv
                           ).ToList();// vào viện
                List<DTuong> _lDTuong = new List<DTuong>();
                _lDTuong = _dataContext.DTuongs.ToList();
                List<MucTT> _listMucTT = new List<MucTT>();
                _listMucTT = _dataContext.MucTTs.ToList();
                int lamtrontong = 4;
                if (chkLamTron.Checked)
                    lamtrontong = 0;
                List<BNKB> _lbnkb = new List<BNKB>();
                if (DungChung.Bien.MaBV == "27023")
                {
                    List<int> _lmabn = ss.Select(p => p.Mabn).ToList();
                    _lbnkb = _dataContext.BNKBs.Where(p => _lmabn.Contains(p.MaBNhan ?? 0)).ToList();
                }
                foreach (var n in ss)
                {
                    Cls79_80.cl_79_80 vpbh = new Cls79_80.cl_79_80();
                    double tongtien = 0;
                    vpbh.Ma_bn = n.MaBNhan;
                    if (n.Tuyen != null)
                        vpbh.Tuyen = n.Tuyen.Value;
                    //if (rdBieu.SelectedIndex == 0)
                    //    vpbh.So_ngay_dtri = 1;
                    if (DungChung.Bien.MaBV == "27023" && _MaKPc > 0)
                    {
                        var songdt = _lbnkb.Where(p => p.MaBNhan == n.Mabn && p.MaKP == _MaKPc).ToList();
                        vpbh.So_ngay_dtri = songdt.Sum(p => p.SoNgaydt ?? 0);
                    }
                    else
                    {
                        if (n.SoNgaydt != null)
                            vpbh.So_ngay_dtri = n.SoNgaydt.Value;
                        else vpbh.So_ngay_dtri = 1;
                    }
                    if (n.BHtu != null)
                        vpbh.Gt_the_tu = n.BHtu.Value;
                    if (n.BHden != null)
                        vpbh.Gt_the_den = n.BHden.Value;
                    if (n.Diachi != null)
                        vpbh.Dia_chi = n.Diachi;

                    if (n.NoiTru != null)
                    {
                        if (n.NoiTru == 1)
                            vpbh.Ma_loaikcb = 3;
                        else
                        {
                            if (n.DTNT == true)
                                vpbh.Ma_loaikcb = 2;
                            else
                                vpbh.Ma_loaikcb = 1;
                        }
                    }

                    vpbh.TyLeBHTT = ckTLBN.Checked ? (100 - n.TyLeBHTT) : n.TyLeBHTT;
                    if (n.KhuVuc != null)
                        vpbh.Ma_khuvuc = n.KhuVuc;
                    if (n.Mabn != null)
                        vpbh.Ma_bn = n.Mabn;
                    if (n.Tuyen != null && lupDoituong.Text == "BHYT")
                        vpbh.Tuyen = n.Tuyen.Value;
                    else
                        vpbh.Tuyen = -1;
                    if (n.Tuyen != null && n.CapCuu != null)
                    {
                        if (n.CapCuu == 0)// thường
                            if (n.Tuyen == 1)
                                vpbh.Ma_lydo_vvien = 1;// đúng tuyến
                            else
                                vpbh.Ma_lydo_vvien = 2;// trái tuyến
                        else// cấp cứu
                            vpbh.Ma_lydo_vvien = 3;
                    }

                    if (n.TenBNhan != null)
                        vpbh.Ho_ten = n.TenBNhan;
                    if (n.NSinh != null)
                        vpbh.NSinh = n.NSinh;
                    if (n.ThangSinh != null && n.NgaySinh != null)
                        vpbh.Ngay_sinh = n.NSinh.ToString().Trim() + (n.ThangSinh.ToString().Trim().Length == 1 ? ("0" + n.ThangSinh.ToString().Trim()) : n.ThangSinh.ToString().Trim()) + (n.NgaySinh.ToString().Trim().Length == 1 ? ("0" + n.NgaySinh.ToString().Trim()) : n.NgaySinh.ToString().Trim());
                    else
                        vpbh.Ngay_sinh = n.NSinh == null ? "" : n.NSinh.ToString().Trim();
                    if (n.SThe != null)
                        vpbh.Ma_the = n.SThe;
                    vpbh.Gioi_tinh = Convert.ToBoolean(n.GTinh);
                    if (n.MaCS != null)
                        vpbh.Ma_dkbd = n.MaCS;
                    vpbh.Ma_cskcb = n.MaKCB;
                    if (n.MaICD != null)
                        vpbh.Ma_benh = ck_HienThiMaICD2.Checked ? n.MaICD : Cls79_80.GetICD(n.MaICD)[0];
                    vpbh.Capcuu = Convert.ToInt32(n.CapCuu);
                    vpbh.Ngay_vao = Convert.ToDateTime(n.NNhap);// đoài y/c //rdBieu.SelectedIndex == 0 ? Convert.ToDateTime(n.NNhap) : Convert.ToDateTime(n.Ngaykham);
                    vpbh.Ngay_ra = Convert.ToDateTime(n.Ngayra);
                    vpbh.T_thuoc = Convert.ToDouble(n.Thuoc);
                    vpbh.T_cdha = Convert.ToDouble(n.CDHA);
                    vpbh.T_kham = Convert.ToDouble(n.Congkham); //rdBieu.SelectedIndex == 1 ? (ck_InTienCongKham.Checked || ckTLBH.Checked || ckTLBN.Checked) ? Convert.ToDouble(n.Congkham) : (Convert.ToDouble(n.Congkham) + Convert.ToDouble(n.TienGiuong)):(DungChung.Ham.hangBVCK(DungChung.Bien.MaBV) == 4 || DungChung.Bien.MaBV == "30280") ? Convert.ToDouble(n.Congkham) : ((ck_InTienCongKham.Checked || ckTLBH.Checked || ckTLBN.Checked) ? Convert.ToDouble(n.Congkham) : (Convert.ToDouble(n.Congkham) + Convert.ToDouble(n.TienGiuong)));
                    //if (ck_InTienCongKham.Checked || DungChung.Ham.hangBVCK(DungChung.Bien.MaBV) == 4 || DungChung.Bien.MaBV == "30280")
                    vpbh.T_giuong = Convert.ToDouble(n.TienGiuong);
                    vpbh.T_xn = Convert.ToDouble(n.Xetnghiem);
                    vpbh.T_mau = Convert.ToDouble(n.Mau);
                    vpbh.T_pttt = Convert.ToDouble(n.TTPT);
                    vpbh.T_vtyt = Convert.ToDouble(n.VTYT);
                    vpbh.T_vtyt_tyle = Convert.ToDouble(n.VTYT_tl);
                    vpbh.T_dvkt_tyle = Convert.ToDouble(n.DVKT_tl);
                    vpbh.T_thuoc_tyle = Convert.ToDouble(n.Thuoc_tl);
                    vpbh.T_vchuyen = Convert.ToDouble(n.CPVanchuyen);

                    tongtien = n.Thuoc + n.CDHA + n.Congkham + n.TienGiuong + n.Xetnghiem + n.Mau + n.TTPT + n.VTYT + n.VTYT_tl + n.DVKT_tl + n.Thuoc_tl + n.CPVanchuyen;
                    vpbh.T_tongchi = Math.Round(tongtien, lamtrontong, MidpointRounding.AwayFromZero);
                    double bhtt = 0, bntt = 0;
                    if (chkLamTron.Checked)
                    {
                        if (_CP_BH == 0)
                        {
                            bhtt = Math.Round(tongtien * (n.TyLeBHTT / 100), lamtrontong, MidpointRounding.AwayFromZero);
                            bntt = Math.Round(tongtien, lamtrontong, MidpointRounding.AwayFromZero) - bhtt;
                            vpbh.T_tongchi = Math.Round(tongtien, lamtrontong, MidpointRounding.AwayFromZero);
                        }
                        else if (_CP_BH == 1)
                        {
                            bhtt = Math.Round(n.Tong * (n.TyLeBHTT / 100), lamtrontong, MidpointRounding.AwayFromZero);
                            vpbh.T_tongchi = bhtt;
                        }
                        else
                        {
                            bntt = Math.Round(n.Tong, lamtrontong, MidpointRounding.AwayFromZero) - Math.Round(n.Tong * (n.TyLeBHTT / 100), lamtrontong, MidpointRounding.AwayFromZero);
                            vpbh.T_tongchi = bntt;
                        }
                    }
                    else
                    {
                        bhtt = n.TienBH;
                        bntt = n.TienBN;
                        //if (_CP_BH == 1)
                        //    bhtt = n.TienBN;
                        //else if (_CP_BH == 2)
                        //    bntt = tongtien;
                        //else
                        //{
                        //    bhtt = n.TienBH;
                        //    bntt = n.TienBN;
                        //}
                    }

                    vpbh.T_bhtt = bhtt;

                    vpbh.T_bntt = bntt;
                    vpbh.NgayTT = Convert.ToDateTime(n.NgayTT);
                    if (n.DTuong != null)
                        vpbh.DTuong = n.DTuong;
                    vpbh.IdBenhNhan = Convert.ToInt32(n.MaBNhan);
                    vpbh.Ngaykham = rdBieu.SelectedIndex == 0 ? Convert.ToDateTime(n.NNhap) : Convert.ToDateTime(n.Ngaykham);
                    tongtien = Math.Round(tongtien, lamtrontong, MidpointRounding.AwayFromZero);
                    vpbh.TongCong = tongtien;
                    vpbh.Thanhtien = tongtien;

                    if (n.NoiTinh != null && lupDoituong.Text == "BHYT")
                    {
                        var mcq = (from bn in _dataContext.BenhNhans.Where(p => p.MaBNhan == n.MaBNhan)
                                   join bv in _dataContext.BenhViens on bn.MaCS equals bv.MaBV
                                   select new
                                   {
                                       bv.MaChuQuan,
                                   }).ToList();

                        if (mcq.Count > 0)
                        {
                            if (mcq.First().MaChuQuan == DungChung.Bien.MaBV)
                            {
                                vpbh.NTinh = 1;
                            }
                            else
                            {
                                vpbh.NTinh = n.NoiTinh.Value;
                            }
                        }
                        else
                        {
                            vpbh.NTinh = n.NoiTinh.Value;
                        }
                    }
                    else
                        vpbh.NTinh = -1;
                    vpbh.CPNgoaiBH = Convert.ToDouble(n.CPNgoaiBH);
                    vpbh.T_ngoaids = n.CPVanchuyen;
                    if (DungChung.Bien.MaBV == "08602")
                        if (n.MaICD.Contains("N18"))
                        {
                            vpbh.T_ngoaids = n.TienBH;
                        }
                    vpbh.Soluot = 1;
                    if (n.MaDTuong != null)
                        vpbh.MaDTuong = n.MaDTuong;
                    if (n.MaDTuong != null)
                        vpbh.NhomDTuong = _lDTuong.Where(p => p.MaDTuong == n.MaDTuong).Count() > 0 ? _lDTuong.Single(p => p.MaDTuong == n.MaDTuong).Nhom : "";
                    if (n.KetQua != null)
                    {
                        if (n.KetQua == "Khỏi")
                            vpbh.Ket_qua_dtri = 1;
                        else if (n.KetQua == "Đỡ|Giảm")
                            vpbh.Ket_qua_dtri = 2;
                        else if (n.KetQua == "Không T.đổi")
                            vpbh.Ket_qua_dtri = 3;
                        else if (n.KetQua == "Nặng hơn")
                            vpbh.Ket_qua_dtri = 4;
                        else if (n.KetQua == "Tử vong")
                            vpbh.Ket_qua_dtri = 5;
                        else
                            vpbh.Ket_qua_dtri = 2;
                    }
                    else
                        vpbh.Ket_qua_dtri = 2;
                    if (n.Status != null)
                    {
                        if (n.Status == 1)
                            vpbh.Tinh_trang_rv = 2;
                        else if (n.Status == 2)
                            vpbh.Tinh_trang_rv = 1;
                        else
                            vpbh.Tinh_trang_rv = Convert.ToInt32(n.Status);
                    }
                    if (n.MaBV != null)
                        vpbh.Ma_noi_chuyen = n.MaBV;
                    vpbh.Chandoan = n.ChanDoan;
                    if (n.Tuoi == 0 || n.Tuoi == 1)
                    {
                        var qcn = qvv.Where(p => p.MaBNhan == n.MaBNhan);
                        if (qcn.Count() > 0 && !string.IsNullOrEmpty(qcn.First().CanNang))
                        {
                            double _cannang = 0;
                            if (Double.TryParse(qcn.First().CanNang, out _cannang))
                                vpbh.Cannang = string.Format(System.Globalization.CultureInfo.GetCultureInfo("de-DE"), "{0:0.0}", _cannang);
                        }
                    }
                    vpbh.MaKhoaTongKet = n.KhoaTongKet;// = n.KhoaTongKet == null ? "" : MaKPQD(n.KhoaTongKet.ToString()) ;
                    vpbh.Ma_pttt_qt = n.Ma_pttt_qt;
                    vpbh.SoDK = n.SoDK ?? 0;
                    // n.NgayTT == null ? 0: DungChung.Ham._getmuc(_listMucTT, _hangbv,n.SThe, Convert.ToInt16(n.Tuyen),Convert.ToInt16(n.NoiTru), Convert.ToDateTime(n.NgayTT.ToString()));
                    _listVPBH.Add(vpbh);
                }

                //   var test = (from a in _listVPBH group a by new { a.Ma_bn } into kq select new { kq.Key.Ma_bn, ten = kq.Select(p => p.Ho_ten).Count() }).Where(p => p.ten > 1).ToList();

                // int stt = 1;
                //frm_BNSai._ktbnsai(_listVPBH);
                //ChucNang.progess.CloseProgress();
                //
                List<int> _ltranfer = _dataContext.Tranfers.Select(p => p.MaBNhan).ToList();
                _listVPBH = (from ketqua in _listVPBH.Where(p => ChuyenKhoan == 1 ? _ltranfer.Contains(p.Ma_bn) : (ChuyenKhoan == 0 ? !_ltranfer.Contains(p.Ma_bn) : true))
                             select ketqua).ToList();

                if (rdCanBo.SelectedIndex == 1 && macb != "")
                {
                    var qtu = _dataContext.TamUngs.Where(p => p.MaCB == macb && (p.PhanLoai == 1 || p.PhanLoai == 2)).ToList();
                    _listVPBH = (from vp in _listVPBH join tu in qtu on vp.Ma_bn equals tu.MaBNhan select vp).ToList();
                }

                if (_listVPBH.Count > 0)
                {
                    #region in chi tiết

                    if (rad_MauIn.SelectedIndex == 0)
                    {
                        if (cbosx.SelectedIndex == 0) // order by Mã bệnh nhân
                        {
                            _listVPBH = ckHienthiTuyen.Checked ? (_listVPBH.OrderBy(p => p.NTinh).ThenBy(p => p.Tuyen).ThenBy(p => p.IdBenhNhan).ToList()) : (_listVPBH.OrderBy(p => p.NTinh).ThenBy(p => p.IdBenhNhan).ToList());
                        }
                        else
                        {
                            if (cbosx.SelectedIndex == 1) //  order by ngày thanh toán
                            {
                                _listVPBH = ckHienthiTuyen.Checked ? (_listVPBH.OrderBy(p => p.NTinh).ThenBy(p => p.Tuyen).ThenBy(p => p.NgayTT).ThenBy(p => p.IdBenhNhan).ToList()) : (_listVPBH.OrderBy(p => p.NTinh).ThenBy(p => p.NgayTT).ThenBy(p => p.IdBenhNhan).ToList());
                            }
                            else
                            {
                                if (cbosx.SelectedIndex == 2)// order by theo Ngày ra
                                {
                                    _listVPBH = ckHienthiTuyen.Checked ? (_listVPBH.OrderBy(p => p.NTinh).ThenBy(p => p.Tuyen).ThenBy(p => p.Ngay_ra).ThenBy(prop => prop.IdBenhNhan).ToList()) : (_listVPBH.OrderBy(p => p.NTinh).ThenBy(p => p.Ngay_ra).ThenBy(p => p.IdBenhNhan).ToList());
                                }
                                else// order by theo Nhóm đối tượng
                                {
                                    _listVPBH = ckHienthiTuyen.Checked ? (_listVPBH.OrderBy(p => p.NTinh).ThenBy(p => p.Tuyen).ThenBy(p => p.NhomDTuong).ThenBy(p => p.MaDTuong).ThenBy(p => p.IdBenhNhan).ToList()) : (_listVPBH.OrderBy(p => p.NTinh).ThenBy(p => p.NhomDTuong).ThenBy(p => p.MaDTuong).ThenBy(p => p.IdBenhNhan).ToList());
                                }
                            }
                        }
                        string kieuNT = cboKieuNT.Text; // kiểu định dạng ngày tháng

                        #region báo cáo  biểu 80 chi tiết

                        if (rdBieu.SelectedIndex == 1)
                        {
                            #region in theo tỷ lệ BHTT

                            if (ckTLBH.Checked || ckTLBN.Checked)
                            {
                                BaoCao.Rep_80a_HD_1399_CongKham_Tyle rep = new BaoCao.Rep_80a_HD_1399_CongKham_Tyle(SXTheoDT);
                                rep.HtMaICD = chk_MaICD.Checked;
                                rep.Dtuong.Value = lupDoituong.Text;
                                rep.Ngaythang.Value = theoquy();
                                rep.HtTuyen = ckHienthiTuyen.Checked;
                                rep.Nguoilapbieu.Value = DungChung.Bien.NguoiLapBieu;
                                rep.Ketoantruong.Value = DungChung.Bien.KeToanTruong;
                                rep.Thutruong.Value = DungChung.Bien.GiamDoc;
                                if (DungChung.Bien.MaBV == "24012" && chkTitleICD.Checked == true)
                                {
                                    rep.txtDSMaBenhChon.Visible = true;
                                }
                                rep.DSMaBenhChon.Value = "Mã ICD: ";
                                if (_lDSMaICD.Count() == 0 || _lDSMaICD.Count() == _dataContext.ICD10.Count())
                                {
                                    rep.DSMaBenhChon.Value += "Tất cả";
                                }
                                else
                                    foreach (var ma in _lDSMaICD)
                                    {
                                        var ten = _dataContext.ICD10.Where(p => p.MaICD == ma).Select(p => p.TenICD).FirstOrDefault();
                                        if (ten != null)
                                            rep.DSMaBenhChon.Value += ma + ": " + ten + "; ";
                                    }
                                if (DungChung.Bien.MaBV == "30303")
                                {
                                    rep.xrTableCell115.Text = "Trưởng phòng KHTCDD";
                                }
                                double st = 0;
                                if (chk_MaICD.Checked)
                                {
                                    if (lupDoituong.Text == "BHYT" || lupDoituong.Text == "Tất cả")//&& ckTLBN.Checked == false)
                                    {
                                        rep.Title.Value = ("THỐNG KÊ CHI PHÍ KCB NỘI TRÚ THEO MÃ BỆNH").ToUpper();
                                        rep.TongtienString.Value = ("Tổng số tiền đề nghị thanh toán (Viết bằng chữ)");
                                        st = _listVPBH.Sum(p => p.T_bhtt);
                                        Title = ("THỐNG KÊ CHI PHÍ KCB NỘI TRÚ THEO MÃ BỆNH").ToUpper();
                                    }
                                    else
                                    {
                                        rep.Title.Value = ("THỐNG KÊ CHI PHÍ KCB NỘI TRÚ THEO MÃ BỆNH").ToUpper();
                                        rep.TongtienString.Value = ("Tổng số tiền thanh toán (Viết bằng chữ)");
                                        st = _listVPBH.Sum(p => p.T_bntt);
                                        Title = ("THỐNG KÊ CHI PHÍ KCB NỘI TRÚ THEO MÃ BỆNH").ToUpper();
                                    }
                                }
                                else
                                {
                                    if (lupDoituong.Text == "BHYT" || lupDoituong.Text == "Tất cả")//&& ckTLBN.Checked == false)
                                    {
                                        rep.Title.Value = ("Danh sách người bệnh bảo hiểm y tế khám chữa bệnh nội trú đề nghị thanh toán").ToUpper();
                                        rep.TongtienString.Value = ("Tổng số tiền đề nghị thanh toán (Viết bằng chữ)");
                                        st = _listVPBH.Sum(p => p.T_bhtt);
                                        Title = ("Danh sách người bệnh bảo hiểm y tế khám chữa bệnh nội trú đề nghị thanh toán").ToUpper();
                                    }
                                    else
                                    {
                                        rep.Title.Value = ("Danh sách người bệnh khám chữa bệnh nội trú thanh toán").ToUpper();
                                        rep.TongtienString.Value = ("Tổng số tiền thanh toán (Viết bằng chữ)");
                                        st = _listVPBH.Sum(p => p.T_bntt);
                                        Title = ("Danh sách người bệnh khám chữa bệnh nội trú thanh toán").ToUpper();
                                    }
                                }

                                #region xuat Excel

                                string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                int[] _arrWidth = new int[] { };
                                string[] _tieude = { "TT", "Họ và tên", "Năm sinh nam", "Năm sinh nữ", "Tỷ lệ BHTT", "Mã thẻ BHYT", "Mã ĐK KCB", "Mã bệnh", "Ngày vào", "Ngày ra", "Số ngày điều trị", "Tổng chi phí khám chưa bệnh BHYT", "Không áp dụng TLTT Xét nghiệm", "Không áp dụng TLTT CĐHA TDCN", "Không áp dụng TLTT Thuốc DT", "Không áp dụng TLTT Máu", "Không áp dụng TLTT TT PT", "Không áp dụng TLTT VTYT", "Thanh toán theo tỷ lệ DV KT", "Thanh toán theo tỷ lệ Thuốc", "Thanh toán theo tỷ lệ VTYT", "Tiền CK", "Tiền giường", "Vận chuyển", "Người bệnh cùng chi trả", "Chi phí đề nghị BHXH thanh toán Tổng", "Chi phí đề nghị BHXH thanh toán Trong đó CP ngoài quỹ ĐS" };
                                DungChung.Bien.MangHaiChieu = new Object[_listVPBH.ToList().Count + 1, 27];
                                for (int i = 0; i < _tieude.Length; i++)
                                {
                                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                }

                                int num = 1;
                                foreach (var r in _listVPBH)
                                {
                                    DungChung.Bien.MangHaiChieu[num, 0] = num;
                                    DungChung.Bien.MangHaiChieu[num, 1] = r.Ho_ten;
                                    if (Convert.ToBoolean(r.Gioi_tinh))
                                    {
                                        DungChung.Bien.MangHaiChieu[num, 2] = r.NSinh;
                                        DungChung.Bien.MangHaiChieu[num, 3] = "";
                                    }
                                    else
                                    {
                                        DungChung.Bien.MangHaiChieu[num, 2] = "";
                                        DungChung.Bien.MangHaiChieu[num, 3] = r.NSinh;
                                    }
                                    DungChung.Bien.MangHaiChieu[num, 4] = r.TyLeBHTT;
                                    DungChung.Bien.MangHaiChieu[num, 5] = r.Ma_the;
                                    DungChung.Bien.MangHaiChieu[num, 6] = r.Ma_dkbd;
                                    DungChung.Bien.MangHaiChieu[num, 7] = r.Ma_benh;
                                    DungChung.Bien.MangHaiChieu[num, 8] = r.Ngaykham;
                                    DungChung.Bien.MangHaiChieu[num, 9] = r.Ngay_ra;
                                    DungChung.Bien.MangHaiChieu[num, 10] = r.So_ngay_dtri;
                                    DungChung.Bien.MangHaiChieu[num, 11] = r.T_tongchi;
                                    DungChung.Bien.MangHaiChieu[num, 12] = r.T_xn;
                                    DungChung.Bien.MangHaiChieu[num, 13] = r.T_cdha;
                                    DungChung.Bien.MangHaiChieu[num, 14] = r.T_thuoc;
                                    DungChung.Bien.MangHaiChieu[num, 15] = r.T_mau;
                                    DungChung.Bien.MangHaiChieu[num, 16] = r.T_pttt;
                                    DungChung.Bien.MangHaiChieu[num, 17] = r.T_vtyt;
                                    DungChung.Bien.MangHaiChieu[num, 18] = r.T_dvkt_tyle;
                                    DungChung.Bien.MangHaiChieu[num, 19] = r.T_thuoc_tyle;
                                    DungChung.Bien.MangHaiChieu[num, 20] = r.T_vtyt_tyle;
                                    DungChung.Bien.MangHaiChieu[num, 21] = r.T_kham;
                                    DungChung.Bien.MangHaiChieu[num, 22] = r.T_giuong;
                                    DungChung.Bien.MangHaiChieu[num, 23] = r.T_vchuyen;
                                    DungChung.Bien.MangHaiChieu[num, 24] = r.T_bntt;
                                    DungChung.Bien.MangHaiChieu[num, 25] = r.T_bhtt;
                                    DungChung.Bien.MangHaiChieu[num, 26] = r.CPNgoaiBH;
                                    num++;
                                }

                                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, Title, "C:\\Biểu 80.xls", true, this.Name);

                                #endregion xuat Excel

                                st = Math.Round(st, 0);
                                rep.Sotien.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                                //chon nhieu kp
                                if (DungChung.Bien.MaBV == "24012" || Bien.MaBV == "24009")
                                {
                                    rep.paramKhoaPhong.Value = "  Khoa phòng: ";
                                    if (lstTenKP.Contains("Chọn tất cả"))
                                    {
                                        rep.paramKhoaPhong.Value += "Tất cả";
                                    }
                                    else
                                        foreach (var i in lstTenKP)
                                        {
                                            rep.paramKhoaPhong.Value += i + "; ";
                                        }
                                }
                                else if (_MaKPc != null && _MaKPc != 0)
                                    rep.paramKhoaPhong.Value = "  Khoa phòng: " + lupKhoaphong.Text;
                                rep.DataSource = _listVPBH;
                                rep.BindingData(kieuNT, SXTheoDT);
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }

                            #endregion in theo tỷ lệ BHTT

                            #region in tiền công khám

                            else if (ck_InTienCongKham.Checked)
                            {
                                BaoCao.Rep_80a_HD_1399_CongKham rep = new BaoCao.Rep_80a_HD_1399_CongKham(SXTheoDT);
                                rep.HtMaICD = chk_MaICD.Checked;
                                rep.Dtuong.Value = lupDoituong.Text;
                                rep.Ngaythang.Value = theoquy();
                                rep.HtTuyen = ckHienthiTuyen.Checked;
                                rep.Nguoilapbieu.Value = DungChung.Bien.NguoiLapBieu;
                                rep.Ketoantruong.Value = DungChung.Bien.KeToanTruong;
                                rep.Thutruong.Value = DungChung.Bien.GiamDoc;
                                if (DungChung.Bien.MaBV == "30303")
                                {
                                    rep.xrTableCell115.Text = "Trưởng phòng KHTCDD";
                                }
                                double st = 0;
                                if (chk_MaICD.Checked)
                                {
                                    if (lupDoituong.Text == "BHYT" || lupDoituong.Text == "Tất cả")//&& ckTLBN.Checked == false)
                                    {
                                        rep.Title.Value = ("THỐNG KÊ CHI PHÍ KCB NỘI TRÚ THEO MÃ BỆNH").ToUpper();
                                        rep.TongtienString.Value = ("Tổng số tiền đề nghị thanh toán (Viết bằng chữ)");
                                        st = _listVPBH.Sum(p => p.T_bhtt);
                                        Title = ("THỐNG KÊ CHI PHÍ KCB NỘI TRÚ THEO MÃ BỆNH").ToUpper();
                                    }
                                    else
                                    {
                                        rep.Title.Value = ("THỐNG KÊ CHI PHÍ KCB NỘI TRÚ THEO MÃ BỆNH").ToUpper();
                                        rep.TongtienString.Value = ("Tổng số tiền thanh toán (Viết bằng chữ)");
                                        st = _listVPBH.Sum(p => p.T_bntt);
                                        Title = ("THỐNG KÊ CHI PHÍ KCB NỘI TRÚ THEO MÃ BỆNH").ToUpper();
                                    }
                                }
                                else
                                {
                                    if (lupDoituong.Text == "BHYT" || lupDoituong.Text == "Tất cả")//&& ckTLBN.Checked == false)
                                    {
                                        rep.Title.Value = ("Danh sách người bệnh bảo hiểm y tế khám chữa bệnh nội trú đề nghị thanh toán").ToUpper();
                                        rep.TongtienString.Value = ("Tổng số tiền đề nghị thanh toán (Viết bằng chữ)");
                                        st = _listVPBH.Sum(p => p.T_bhtt);
                                        Title = ("Danh sách người bệnh bảo hiểm y tế khám chữa bệnh nội trú đề nghị thanh toán").ToUpper();
                                    }
                                    else
                                    {
                                        rep.Title.Value = ("Danh sách người bệnh khám chữa bệnh nội trú thanh toán").ToUpper();
                                        rep.TongtienString.Value = ("Tổng số tiền thanh toán (Viết bằng chữ)");
                                        st = _listVPBH.Sum(p => p.T_bntt);
                                        Title = ("Danh sách người bệnh khám chữa bệnh nội trú thanh toán").ToUpper();
                                    }
                                }

                                #region xuat Excel

                                string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                int[] _arrWidth = new int[] { };
                                string[] _tieude = { "TT", "Họ và tên", "Năm sinh nam", "Năm sinh nữ", "Mã thẻ BHYT", "Mã ĐK KCB", "Mã bệnh", "Ngày vào", "Ngày ra", "Số ngày điều trị", "Tổng chi phí khám chưa bệnh BHYT", "Không áp dụng TLTT Xét nghiệm", "Không áp dụng TLTT CĐHA TDCN", "Không áp dụng TLTT Thuốc DT", "Không áp dụng TLTT Máu", "Không áp dụng TLTT TT PT", "Không áp dụng TLTT VTYT", "Thanh toán theo tỷ lệ DV KT", "Thanh toán theo tỷ lệ Thuốc", "Thanh toán theo tỷ lệ VTYT", "Tiền CK", "Tiền giường", "Vận chuyển", "Người bệnh cùng chi trả", "Chi phí đề nghị BHXH thanh toán Tổng", "Chi phí đề nghị BHXH thanh toán Trong đó CP ngoài quỹ ĐS" };
                                DungChung.Bien.MangHaiChieu = new Object[_listVPBH.ToList().Count + 1, 26];
                                for (int i = 0; i < _tieude.Length; i++)
                                {
                                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                }

                                int num = 1;
                                foreach (var r in _listVPBH)
                                {
                                    DungChung.Bien.MangHaiChieu[num, 0] = num;
                                    DungChung.Bien.MangHaiChieu[num, 1] = r.Ho_ten;
                                    if (Convert.ToBoolean(r.Gioi_tinh))
                                    {
                                        DungChung.Bien.MangHaiChieu[num, 2] = r.NSinh;
                                        DungChung.Bien.MangHaiChieu[num, 3] = "";
                                    }
                                    else
                                    {
                                        DungChung.Bien.MangHaiChieu[num, 2] = "";
                                        DungChung.Bien.MangHaiChieu[num, 3] = r.NSinh;
                                    }
                                    DungChung.Bien.MangHaiChieu[num, 4] = r.Ma_the;
                                    DungChung.Bien.MangHaiChieu[num, 5] = r.Ma_dkbd;
                                    DungChung.Bien.MangHaiChieu[num, 6] = r.Ma_benh;
                                    DungChung.Bien.MangHaiChieu[num, 7] = r.Ngaykham;
                                    DungChung.Bien.MangHaiChieu[num, 8] = r.Ngay_ra;
                                    DungChung.Bien.MangHaiChieu[num, 9] = r.So_ngay_dtri;
                                    DungChung.Bien.MangHaiChieu[num, 10] = r.T_tongchi;
                                    DungChung.Bien.MangHaiChieu[num, 11] = r.T_xn;
                                    DungChung.Bien.MangHaiChieu[num, 12] = r.T_cdha;
                                    DungChung.Bien.MangHaiChieu[num, 13] = r.T_thuoc;
                                    DungChung.Bien.MangHaiChieu[num, 14] = r.T_mau;
                                    DungChung.Bien.MangHaiChieu[num, 15] = r.T_pttt;
                                    DungChung.Bien.MangHaiChieu[num, 16] = r.T_vtyt;
                                    DungChung.Bien.MangHaiChieu[num, 17] = r.T_dvkt_tyle;
                                    DungChung.Bien.MangHaiChieu[num, 18] = r.T_thuoc_tyle;
                                    DungChung.Bien.MangHaiChieu[num, 19] = r.T_vtyt_tyle;
                                    DungChung.Bien.MangHaiChieu[num, 20] = r.T_kham;
                                    DungChung.Bien.MangHaiChieu[num, 21] = r.T_giuong;
                                    DungChung.Bien.MangHaiChieu[num, 22] = r.T_vchuyen;
                                    DungChung.Bien.MangHaiChieu[num, 23] = r.T_bntt;
                                    DungChung.Bien.MangHaiChieu[num, 24] = r.T_bhtt;
                                    DungChung.Bien.MangHaiChieu[num, 25] = r.CPNgoaiBH;
                                    num++;
                                }

                                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, Title, "C:\\Biểu 80.xls", true, this.Name);

                                #endregion xuat Excel

                                st = Math.Round(st, 0);
                                rep.Sotien.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                                if (_MaKPc != null && _MaKPc != 0)
                                    rep.paramKhoaPhong.Value = "  Khoa phòng: " + lupKhoaphong.Text; ;
                                rep.DataSource = _listVPBH;
                                rep.BindingData(kieuNT, SXTheoDT);
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }

                            #endregion in tiền công khám

                            #region chung

                            else
                            {
                                BaoCao.Rep_80a_HD_1399 rep = new BaoCao.Rep_80a_HD_1399(SXTheoDT);
                                rep.HtMaICD = chk_MaICD.Checked;
                                rep.Dtuong.Value = lupDoituong.Text;
                                rep.Ngaythang.Value = theoquy();
                                rep.HtTuyen = ckHienthiTuyen.Checked;
                                rep.Nguoilapbieu.Value = DungChung.Bien.NguoiLapBieu;
                                rep.Ketoantruong.Value = DungChung.Bien.KeToanTruong;
                                rep.Thutruong.Value = DungChung.Bien.GiamDoc;
                                if (DungChung.Bien.MaBV == "30303")
                                {
                                    rep.xrTableCell115.Text = "Trưởng phòng KHTCDD";
                                }
                                double st = 0;
                                if (chk_MaICD.Checked)
                                {
                                    if (lupDoituong.Text == "BHYT" || lupDoituong.Text == "Tất cả")// && ckTLBN.Checked == false)
                                    {
                                        rep.Title.Value = ("THỐNG KÊ CHI PHÍ KCB NỘI TRÚ THEO MÃ BỆNH").ToUpper();
                                        rep.TongtienString.Value = ("Tổng số tiền đề nghị thanh toán (Viết bằng chữ)");
                                        st = _listVPBH.Sum(p => p.T_bhtt);
                                        Title = ("THỐNG KÊ CHI PHÍ KCB NỘI TRÚ THEO MÃ BỆNH").ToUpper();
                                    }
                                    else
                                    {
                                        rep.Title.Value = ("THỐNG KÊ CHI PHÍ KCB NỘI TRÚ THEO MÃ BỆNH").ToUpper();
                                        rep.TongtienString.Value = ("Tổng số tiền thanh toán (Viết bằng chữ)");
                                        st = _listVPBH.Sum(p => p.T_bntt);
                                        Title = ("THỐNG KÊ CHI PHÍ KCB NỘI TRÚ THEO MÃ BỆNH").ToUpper();
                                    }
                                }
                                else
                                {
                                    if (lupDoituong.Text == "BHYT" || lupDoituong.Text == "Tất cả")// && ckTLBN.Checked == false)
                                    {
                                        rep.Title.Value = ("Danh sách người bệnh bảo hiểm y tế khám chữa bệnh nội trú đề nghị thanh toán").ToUpper();
                                        rep.TongtienString.Value = ("Tổng số tiền đề nghị thanh toán (Viết bằng chữ)");
                                        st = _listVPBH.Sum(p => p.T_bhtt);
                                        Title = ("Danh sách người bệnh bảo hiểm y tế khám chữa bệnh nội trú đề nghị thanh toán").ToUpper();
                                    }
                                    else
                                    {
                                        rep.Title.Value = ("Danh sách người bệnh khám chữa bệnh nội trú thanh toán").ToUpper();
                                        rep.TongtienString.Value = ("Tổng số tiền thanh toán (Viết bằng chữ)");
                                        st = _listVPBH.Sum(p => p.T_bntt);
                                        Title = ("Danh sách người bệnh khám chữa bệnh nội trú thanh toán").ToUpper();
                                    }
                                }

                                #region xuat Excel

                                string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                int[] _arrWidth = new int[] { };
                                string[] _tieude = { "TT", "Họ và tên", "Năm sinh nam", "Năm sinh nữ", "Mã thẻ BHYT", "Mã ĐK KCB", "Mã bệnh", "Ngày vào", "Ngày ra", "Số ngày điều trị", "Tổng chi phí khám chưa bệnh BHYT", "Không áp dụng TLTT Xét nghiệm", "Không áp dụng TLTT CĐHA TDCN", "Không áp dụng TLTT Thuốc DT", "Không áp dụng TLTT Máu", "Không áp dụng TLTT TT PT", "Không áp dụng TLTT VTYT", "Thanh toán theo tỷ lệ DV KT", "Thanh toán theo tỷ lệ Thuốc", "Thanh toán theo tỷ lệ VTYT", "Tiền giường", "Vận chuyển", "Người bệnh cùng chi trả", "Chi phí đề nghị BHXH thanh toán Tổng", "Chi phí đề nghị BHXH thanh toán Trong đó CP ngoài quỹ ĐS" };
                                DungChung.Bien.MangHaiChieu = new Object[_listVPBH.ToList().Count + 1, 26];
                                for (int i = 0; i < _tieude.Length; i++)
                                {
                                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                }

                                int num = 1;
                                foreach (var r in _listVPBH)
                                {
                                    DungChung.Bien.MangHaiChieu[num, 0] = num;
                                    DungChung.Bien.MangHaiChieu[num, 1] = r.Ho_ten;
                                    if (Convert.ToBoolean(r.Gioi_tinh))
                                    {
                                        DungChung.Bien.MangHaiChieu[num, 2] = r.NSinh;
                                        DungChung.Bien.MangHaiChieu[num, 3] = "";
                                    }
                                    else
                                    {
                                        DungChung.Bien.MangHaiChieu[num, 2] = "";
                                        DungChung.Bien.MangHaiChieu[num, 3] = r.NSinh;
                                    }
                                    DungChung.Bien.MangHaiChieu[num, 4] = r.Ma_the;
                                    DungChung.Bien.MangHaiChieu[num, 5] = r.Ma_dkbd;
                                    DungChung.Bien.MangHaiChieu[num, 6] = r.Ma_benh;
                                    DungChung.Bien.MangHaiChieu[num, 7] = r.Ngaykham;
                                    DungChung.Bien.MangHaiChieu[num, 8] = r.Ngay_ra;
                                    DungChung.Bien.MangHaiChieu[num, 9] = r.So_ngay_dtri;
                                    DungChung.Bien.MangHaiChieu[num, 10] = r.T_tongchi;
                                    DungChung.Bien.MangHaiChieu[num, 11] = r.T_xn;
                                    DungChung.Bien.MangHaiChieu[num, 12] = r.T_cdha;
                                    DungChung.Bien.MangHaiChieu[num, 13] = r.T_thuoc;
                                    DungChung.Bien.MangHaiChieu[num, 14] = r.T_mau;
                                    DungChung.Bien.MangHaiChieu[num, 15] = r.T_pttt;
                                    DungChung.Bien.MangHaiChieu[num, 16] = r.T_vtyt;
                                    DungChung.Bien.MangHaiChieu[num, 17] = r.T_dvkt_tyle;
                                    DungChung.Bien.MangHaiChieu[num, 18] = r.T_thuoc_tyle;
                                    DungChung.Bien.MangHaiChieu[num, 19] = r.T_vtyt_tyle;
                                    DungChung.Bien.MangHaiChieu[num, 20] = r.T_kham;
                                    DungChung.Bien.MangHaiChieu[num, 21] = r.T_vchuyen;
                                    DungChung.Bien.MangHaiChieu[num, 22] = r.T_bntt;
                                    DungChung.Bien.MangHaiChieu[num, 23] = r.T_bhtt;
                                    DungChung.Bien.MangHaiChieu[num, 24] = r.CPNgoaiBH;
                                    num++;
                                }

                                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, Title, "C:\\Biểu 80.xls", true, this.Name);

                                #endregion xuat Excel

                                st = Math.Round(st, 0);
                                rep.Sotien.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                                if (_MaKPc != null && _MaKPc != 0)
                                    rep.paramKhoaPhong.Value = "  Khoa phòng: " + lupKhoaphong.Text; ;
                                rep.DataSource = _listVPBH;
                                rep.BindingData(kieuNT, SXTheoDT);
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }

                            #endregion chung
                        }

                        #endregion báo cáo  biểu 80 chi tiết

                        #region báo cáo biểu 79 chi tiết

                        else
                        {
                            #region in theo tỷ lệ BHTT

                            if (ckTLBH.Checked || ckTLBN.Checked)
                            {
                                if (DungChung.Ham.hangBVCK(DungChung.Bien.MaBV) == 4 || DungChung.Bien.MaBV == "30280")
                                {
                                    BaoCao.Rep_79a_HD_1399_Tyle_TienGiuong repv = new BaoCao.Rep_79a_HD_1399_Tyle_TienGiuong(SXTheoDT);
                                    repv.Parameters["TongSoBN"].Value = _listVPBH.Count();
                                    repv.HtMaICD = chk_MaICD.Checked;
                                    repv.Ngaythang.Value = theoquy();
                                    repv.HtTuyen = ckHienthiTuyen.Checked;
                                    repv.MaCS.Value = DungChung.Bien.MaBV.Substring(0, 2) + "-" + DungChung.Bien.MaBV.Substring(2, 3);
                                    repv.ngaythanghientai.Value = DungChung.Ham.NgaySangChu(System.DateTime.Now);
                                    repv.Nguoilapbieu.Value = DungChung.Bien.NguoiLapBieu;
                                    repv.Ketoantruong.Value = DungChung.Bien.KeToanTruong;
                                    repv.Thutruong.Value = DungChung.Bien.GiamDoc;
                                    if (DungChung.Bien.MaBV == "30303")
                                    {
                                        repv.xrTableCell73.Text = "Trưởng phòng KHTCDD";
                                    }
                                    if (_MaKPc != null && _MaKPc != 0)
                                    {
                                        repv.paramKhoaPhong.Value = lupKhoaphong.Text.ToUpper();
                                    }
                                    string kieungay = cboKieuNT.Text;
                                    double st = 0;
                                    if (chk_MaICD.Checked)
                                    {
                                        if (lupDoituong.Text == "BHYT" || lupDoituong.Text == "Tất cả")//&& ckTLBN.Checked == false)
                                        {
                                            repv.Title.Value = ("THỐNG KÊ CHI PHÍ KCB NGOẠI TRÚ THEO MÃ BỆNH").ToUpper();
                                            repv.TongtienString.Value = ("Tổng số tiền đề nghị thanh toán (Viết bằng chữ)");
                                            st = _listVPBH.Sum(p => p.T_bhtt);
                                            Title = ("THỐNG KÊ CHI PHÍ KCB NGOẠI TRÚ THEO MÃ BỆNH").ToUpper();
                                        }
                                        else
                                        {
                                            repv.Title.Value = ("THỐNG KÊ CHI PHÍ KCB NGOẠI TRÚ THEO MÃ BỆNH").ToUpper();
                                            repv.TongtienString.Value = ("Tổng số tiền thanh toán (Viết bằng chữ)");
                                            st = _listVPBH.Sum(p => p.T_bntt);
                                            Title = ("THỐNG KÊ CHI PHÍ KCB NGOẠI TRÚ THEO MÃ BỆNH").ToUpper();
                                        }
                                    }
                                    else
                                    {
                                        if (lupDoituong.Text == "BHYT" || lupDoituong.Text == "Tất cả")//&& ckTLBN.Checked == false)
                                        {
                                            repv.Title.Value = ("Danh sách người bệnh bảo hiểm y tế khám chữa bệnh ngoại trú đề nghị thanh toán").ToUpper();
                                            st = _listVPBH.Sum(p => p.T_bhtt);
                                            repv.TongtienString.Value = ("Tổng số tiền đề nghị thanh toán (Viết bằng chữ)");
                                            Title = ("Danh sách người bệnh bảo hiểm y tế khám chữa bệnh ngoại trú đề nghị thanh toán").ToUpper();
                                        }
                                        else
                                        {
                                            repv.Title.Value = ("Danh sách người bệnh khám chữa bệnh ngoại trú thanh toán").ToUpper();
                                            repv.TongtienString.Value = ("Tổng số tiền thanh toán (Viết bằng chữ)");
                                            st = _listVPBH.Sum(p => p.T_bntt);
                                            Title = ("Danh sách người bệnh khám chữa bệnh ngoại trú thanh toán").ToUpper();
                                        }
                                    }

                                    #region xuat Excel

                                    string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                    int[] _arrWidth = new int[] { };
                                    string[] _tieude = { "TT", "Họ và tên", "Năm sinh nam", "Năm sinh nữ", "Mã thẻ BHYT", "Tỷ lệ BHTT", "Mã ĐK KCB", "Mã bệnh", "Ngày khám", "Tổng chi phí khám chưa bệnh BHYT", "Không áp dụng TLTT Xét nghiệm", "Không áp dụng TLTT CĐHA TDCN", "Không áp dụng TLTT Thuốc DT", "Không áp dụng TLTT Máu", "Không áp dụng TLTT TT PT", "Không áp dụng TLTT VTYT", "Thanh toán theo tỷ lệ DV KT", "Thanh toán theo tỷ lệ Thuốc", "Thanh toán theo tỷ lệ VTYT", "Tiền khám", "Tiền giường", "Vận chuyển", "Người bệnh cùng chi trả", "Chi phí đề nghị BHXH thanh toán Tổng", "Chi phí đề nghị BHXH thanh toán Trong đó CP ngoài quỹ ĐS" };
                                    DungChung.Bien.MangHaiChieu = new Object[_listVPBH.ToList().Count + 1, 27];
                                    for (int i = 0; i < _tieude.Length; i++)
                                    {
                                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                    }

                                    int num = 1;
                                    foreach (var r in _listVPBH)
                                    {
                                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                                        DungChung.Bien.MangHaiChieu[num, 1] = r.Ho_ten;
                                        if (Convert.ToBoolean(r.Gioi_tinh))
                                        {
                                            DungChung.Bien.MangHaiChieu[num, 2] = r.NSinh;
                                            DungChung.Bien.MangHaiChieu[num, 3] = "";
                                        }
                                        else
                                        {
                                            DungChung.Bien.MangHaiChieu[num, 2] = "";
                                            DungChung.Bien.MangHaiChieu[num, 3] = r.NSinh;
                                        }
                                        DungChung.Bien.MangHaiChieu[num, 4] = r.Ma_the;
                                        DungChung.Bien.MangHaiChieu[num, 5] = r.TyLeBHTT;
                                        DungChung.Bien.MangHaiChieu[num, 6] = r.Ma_dkbd;
                                        DungChung.Bien.MangHaiChieu[num, 7] = r.Ma_benh;
                                        DungChung.Bien.MangHaiChieu[num, 8] = r.Ngaykham;
                                        DungChung.Bien.MangHaiChieu[num, 9] = r.T_tongchi;
                                        DungChung.Bien.MangHaiChieu[num, 10] = r.T_xn;
                                        DungChung.Bien.MangHaiChieu[num, 11] = r.T_cdha;
                                        DungChung.Bien.MangHaiChieu[num, 12] = r.T_thuoc;
                                        DungChung.Bien.MangHaiChieu[num, 13] = r.T_mau;
                                        DungChung.Bien.MangHaiChieu[num, 14] = r.T_pttt;
                                        DungChung.Bien.MangHaiChieu[num, 15] = r.T_vtyt;
                                        DungChung.Bien.MangHaiChieu[num, 16] = r.T_dvkt_tyle;
                                        DungChung.Bien.MangHaiChieu[num, 17] = r.T_thuoc_tyle;
                                        DungChung.Bien.MangHaiChieu[num, 18] = r.T_vtyt_tyle;
                                        DungChung.Bien.MangHaiChieu[num, 19] = r.T_kham;
                                        DungChung.Bien.MangHaiChieu[num, 20] = r.T_giuong;
                                        DungChung.Bien.MangHaiChieu[num, 21] = r.T_vchuyen;
                                        DungChung.Bien.MangHaiChieu[num, 22] = r.T_bntt;
                                        DungChung.Bien.MangHaiChieu[num, 23] = r.T_bhtt;
                                        DungChung.Bien.MangHaiChieu[num, 24] = r.CPNgoaiBH;
                                        num++;
                                    }

                                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, Title, "C:\\Biểu 79.xls", true, this.Name);

                                    #endregion xuat Excel

                                    st = Math.Round(st, 0);
                                    repv.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                                    repv.DataSource = _listVPBH;
                                    repv.BindingData(kieungay, SXTheoDT);
                                    repv.CreateDocument();
                                    frm.prcIN.PrintingSystem = repv.PrintingSystem;
                                    frm.ShowDialog();
                                }
                                else
                                {
                                    BaoCao.Rep_79a_HD_1399_Tyle repv = new BaoCao.Rep_79a_HD_1399_Tyle(SXTheoDT);
                                    if (DungChung.Bien.MaBV == "24012" && chkTitleICD.Checked == true)
                                    {
                                        repv.txtDSMaBenhChon.Visible = true;
                                    }
                                    repv.DSMaBenhChon.Value = "Mã ICD: ";
                                    if (_lDSMaDV.Count() == 0 || _lDSMaDV.Count() == _dataContext.ICD10.Count())
                                    {
                                        repv.DSMaBenhChon.Value += "Tất cả";
                                    }
                                    else
                                        foreach (var ma in _lDSMaICD)
                                        {
                                            var ten = _dataContext.ICD10.Where(p => p.MaICD == ma).Select(p => p.TenICD).FirstOrDefault();
                                            if (ten != null)
                                                repv.DSMaBenhChon.Value += ma + ": " + ten + "; ";
                                        }
                                    repv.HtMaICD = chk_MaICD.Checked;
                                    repv.Ngaythang.Value = theoquy();
                                    repv.HtTuyen = ckHienthiTuyen.Checked;
                                    repv.MaCS.Value = DungChung.Bien.MaBV.Substring(0, 2) + "-" + DungChung.Bien.MaBV.Substring(2, 3);
                                    repv.ngaythanghientai.Value = DungChung.Ham.NgaySangChu(System.DateTime.Now);
                                    repv.Nguoilapbieu.Value = DungChung.Bien.NguoiLapBieu;
                                    repv.Ketoantruong.Value = DungChung.Bien.KeToanTruong;
                                    repv.Thutruong.Value = DungChung.Bien.GiamDoc;
                                    //chon nhieu kp
                                    if (DungChung.Bien.MaBV == "24012" || Bien.MaBV == "24009")
                                    {
                                        repv.paramKhoaPhong.Value = "  Khoa phòng: ";
                                        if (lstTenKP.Contains("Chọn tất cả"))
                                        {
                                            repv.paramKhoaPhong.Value += "Tất cả";
                                        }
                                        else
                                            foreach (var i in lstTenKP)
                                            {
                                                repv.paramKhoaPhong.Value += i + "; ";
                                            }
                                    }
                                    else if (_MaKPc != 0)
                                    {
                                        repv.paramKhoaPhong.Value = lupKhoaphong.Text.ToUpper();
                                    }
                                    string kieungay = cboKieuNT.Text;
                                    double st = 0;
                                    if (chk_MaICD.Checked)
                                    {
                                        if (lupDoituong.Text == "BHYT" || lupDoituong.Text == "Tất cả")//&& ckTLBN.Checked == false)
                                        {
                                            repv.Title.Value = ("THỐNG KÊ CHI PHÍ KCB NGOẠI TRÚ THEO MÃ BỆNH").ToUpper();
                                            repv.TongtienString.Value = ("Tổng số tiền đề nghị thanh toán (Viết bằng chữ)");
                                            st = _listVPBH.Sum(p => p.T_bhtt);
                                            Title = ("THỐNG KÊ CHI PHÍ KCB NGOẠI TRÚ THEO MÃ BỆNH").ToUpper();
                                        }
                                        else
                                        {
                                            repv.Title.Value = ("THỐNG KÊ CHI PHÍ KCB NGOẠI TRÚ THEO MÃ BỆNH").ToUpper();
                                            repv.TongtienString.Value = ("Tổng số tiền thanh toán (Viết bằng chữ)");
                                            st = _listVPBH.Sum(p => p.T_bntt);
                                            Title = ("THỐNG KÊ CHI PHÍ KCB NGOẠI TRÚ THEO MÃ BỆNH").ToUpper();
                                        }
                                    }
                                    else
                                    {
                                        if (lupDoituong.Text == "BHYT" || lupDoituong.Text == "Tất cả")//&& ckTLBN.Checked == false)
                                        {
                                            repv.Title.Value = ("Danh sách người bệnh bảo hiểm y tế khám chữa bệnh ngoại trú đề nghị thanh toán").ToUpper();
                                            st = _listVPBH.Sum(p => p.T_bhtt);
                                            repv.TongtienString.Value = ("Tổng số tiền đề nghị thanh toán (Viết bằng chữ)");
                                            Title = ("Danh sách người bệnh bảo hiểm y tế khám chữa bệnh ngoại trú đề nghị thanh toán").ToUpper();
                                        }
                                        else
                                        {
                                            repv.Title.Value = ("Danh sách người bệnh khám chữa bệnh ngoại trú thanh toán").ToUpper();
                                            repv.TongtienString.Value = ("Tổng số tiền thanh toán (Viết bằng chữ)");
                                            st = _listVPBH.Sum(p => p.T_bntt);
                                            Title = ("Danh sách người bệnh khám chữa bệnh ngoại trú thanh toán").ToUpper();
                                        }
                                    }

                                    #region xuat Excel

                                    string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                    int[] _arrWidth = new int[] { };
                                    string[] _tieude = { "TT", "Họ và tên", "Năm sinh nam", "Năm sinh nữ", "Mã thẻ BHYT", "Tỷ lệ BHTT", "Mã ĐK KCB", "Mã bệnh", "Ngày khám", "Tổng chi phí khám chưa bệnh BHYT", "Không áp dụng TLTT Xét nghiệm", "Không áp dụng TLTT CĐHA TDCN", "Không áp dụng TLTT Thuốc DT", "Không áp dụng TLTT Máu", "Không áp dụng TLTT TT PT", "Không áp dụng TLTT VTYT", "Thanh toán theo tỷ lệ DV KT", "Thanh toán theo tỷ lệ Thuốc", "Thanh toán theo tỷ lệ VTYT", "Tiền khám", "Vận chuyển", "Người bệnh cùng chi trả", "Chi phí đề nghị BHXH thanh toán Tổng", "Chi phí đề nghị BHXH thanh toán Trong đó CP ngoài quỹ ĐS" };
                                    DungChung.Bien.MangHaiChieu = new Object[_listVPBH.ToList().Count + 1, 27];
                                    for (int i = 0; i < _tieude.Length; i++)
                                    {
                                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                    }

                                    int num = 1;
                                    foreach (var r in _listVPBH)
                                    {
                                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                                        DungChung.Bien.MangHaiChieu[num, 1] = r.Ho_ten;
                                        if (Convert.ToBoolean(r.Gioi_tinh))
                                        {
                                            DungChung.Bien.MangHaiChieu[num, 2] = r.NSinh;
                                            DungChung.Bien.MangHaiChieu[num, 3] = "";
                                        }
                                        else
                                        {
                                            DungChung.Bien.MangHaiChieu[num, 2] = "";
                                            DungChung.Bien.MangHaiChieu[num, 3] = r.NSinh;
                                        }
                                        DungChung.Bien.MangHaiChieu[num, 4] = r.Ma_the;
                                        DungChung.Bien.MangHaiChieu[num, 5] = r.TyLeBHTT;
                                        DungChung.Bien.MangHaiChieu[num, 6] = r.Ma_dkbd;
                                        DungChung.Bien.MangHaiChieu[num, 7] = r.Ma_benh;
                                        DungChung.Bien.MangHaiChieu[num, 8] = r.Ngaykham;
                                        DungChung.Bien.MangHaiChieu[num, 9] = r.T_tongchi;
                                        DungChung.Bien.MangHaiChieu[num, 10] = r.T_xn;
                                        DungChung.Bien.MangHaiChieu[num, 11] = r.T_cdha;
                                        DungChung.Bien.MangHaiChieu[num, 12] = r.T_thuoc;
                                        DungChung.Bien.MangHaiChieu[num, 13] = r.T_mau;
                                        DungChung.Bien.MangHaiChieu[num, 14] = r.T_pttt;
                                        DungChung.Bien.MangHaiChieu[num, 15] = r.T_vtyt;
                                        DungChung.Bien.MangHaiChieu[num, 16] = r.T_dvkt_tyle;
                                        DungChung.Bien.MangHaiChieu[num, 17] = r.T_thuoc_tyle;
                                        DungChung.Bien.MangHaiChieu[num, 18] = r.T_vtyt_tyle;
                                        DungChung.Bien.MangHaiChieu[num, 19] = r.T_kham;
                                        DungChung.Bien.MangHaiChieu[num, 20] = r.T_vchuyen;
                                        DungChung.Bien.MangHaiChieu[num, 21] = r.T_bntt;
                                        DungChung.Bien.MangHaiChieu[num, 22] = r.T_bhtt;
                                        DungChung.Bien.MangHaiChieu[num, 23] = r.CPNgoaiBH;
                                        num++;
                                    }

                                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, Title, "C:\\Biểu 79.xls", true, this.Name);

                                    #endregion xuat Excel

                                    st = Math.Round(st, 0);
                                    repv.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                                    repv.DataSource = _listVPBH;
                                    repv.BindingData(kieungay, SXTheoDT);
                                    repv.CreateDocument();
                                    frm.prcIN.PrintingSystem = repv.PrintingSystem;
                                    frm.ShowDialog();
                                }
                            }

                            #endregion in theo tỷ lệ BHTT

                            #region bc chung

                            else
                            {
                                if (DungChung.Ham.hangBVCK(DungChung.Bien.MaBV) == 4 || DungChung.Bien.MaBV == "30280")
                                {
                                    BaoCao.Rep_79a_HD_1399_TienGiuong repv = new BaoCao.Rep_79a_HD_1399_TienGiuong(SXTheoDT);
                                    repv.HtMaICD = chk_MaICD.Checked;
                                    repv.Ngaythang.Value = theoquy();
                                    repv.HtTuyen = ckHienthiTuyen.Checked;
                                    repv.Nguoilapbieu.Value = DungChung.Bien.NguoiLapBieu;
                                    repv.Ketoantruong.Value = DungChung.Bien.KeToanTruong;
                                    repv.Thutruong.Value = DungChung.Bien.GiamDoc;
                                    repv.MaCS.Value = DungChung.Bien.MaBV.Substring(0, 2) + "-" + DungChung.Bien.MaBV.Substring(2, 3);
                                    repv.ngaythanghientai.Value = DungChung.Ham.NgaySangChu(System.DateTime.Now);
                                    repv.xrTableCell73.Text = "Trưởng phòng KHTCDD";
                                    if (_MaKPc != null && _MaKPc != 0)
                                    {
                                        repv.paramKhoaPhong.Value = lupKhoaphong.Text.ToUpper();
                                    }
                                    string kieungay = cboKieuNT.Text;
                                    double st = 0;
                                    if (chk_MaICD.Checked)
                                    {
                                        if (lupDoituong.Text == "BHYT" || lupDoituong.Text == "Tất cả") //&& ckTLBN.Checked == false)
                                        {
                                            repv.Title.Value = ("THỐNG KÊ CHI PHÍ KCB NGOẠI TRÚ THEO MÃ BỆNH").ToUpper();
                                            repv.TongtienString.Value = ("Tổng số tiền đề nghị thanh toán (Viết bằng chữ)");
                                            st = _listVPBH.Sum(p => p.T_bhtt);
                                            Title = ("THỐNG KÊ CHI PHÍ KCB NGOẠI TRÚ THEO MÃ BỆNH").ToUpper();
                                        }
                                        else
                                        {
                                            repv.Title.Value = ("THỐNG KÊ CHI PHÍ KCB NGOẠI TRÚ THEO MÃ BỆNH").ToUpper();
                                            repv.TongtienString.Value = ("Tổng số tiền thanh toán (Viết bằng chữ)");
                                            st = _listVPBH.Sum(p => p.T_bntt);
                                            Title = ("THỐNG KÊ CHI PHÍ KCB NGOẠI TRÚ THEO MÃ BỆNH").ToUpper();
                                        }
                                    }
                                    else
                                    {
                                        if (lupDoituong.Text == "BHYT" || lupDoituong.Text == "Tất cả")//&& ckTLBN.Checked == false)
                                        {
                                            repv.Title.Value = ("Danh sách người bệnh bảo hiểm y tế khám chữa bệnh ngoại trú đề nghị thanh toán").ToUpper();
                                            st = _listVPBH.Sum(p => p.T_bhtt);
                                            repv.TongtienString.Value = ("Tổng số tiền đề nghị thanh toán (Viết bằng chữ)");
                                            Title = ("Danh sách người bệnh bảo hiểm y tế khám chữa bệnh ngoại trú đề nghị thanh toán").ToUpper();
                                        }
                                        else
                                        {
                                            repv.Title.Value = ("Danh sách người bệnh khám chữa bệnh ngoại trú thanh toán").ToUpper();
                                            repv.TongtienString.Value = ("Tổng số tiền thanh toán (Viết bằng chữ)");
                                            st = _listVPBH.Sum(p => p.T_bntt);
                                            Title = ("Danh sách người bệnh khám chữa bệnh ngoại trú thanh toán").ToUpper();
                                        }
                                    }

                                    #region xuat Excel

                                    string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                    int[] _arrWidth = new int[] { };
                                    string[] _tieude = { "TT", "Họ và tên", "Năm sinh nam", "Năm sinh nữ", "Mã thẻ BHYT", "Mã ĐK KCB", "Mã bệnh", "Ngày khám", "Tổng chi phí khám chưa bệnh BHYT", "Không áp dụng TLTT Xét nghiệm", "Không áp dụng TLTT CĐHA TDCN", "Không áp dụng TLTT Thuốc DT", "Không áp dụng TLTT Máu", "Không áp dụng TLTT TT PT", "Không áp dụng TLTT VTYT", "Thanh toán theo tỷ lệ DV KT", "Thanh toán theo tỷ lệ Thuốc", "Thanh toán theo tỷ lệ VTYT", "Tiền khám", "Tiền giường", "Vận chuyển", "Người bệnh cùng chi trả", "Chi phí đề nghị BHXH thanh toán Tổng", "Chi phí đề nghị BHXH thanh toán Trong đó CP ngoài quỹ ĐS" };
                                    DungChung.Bien.MangHaiChieu = new Object[_listVPBH.ToList().Count + 1, 27];
                                    for (int i = 0; i < _tieude.Length; i++)
                                    {
                                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                    }

                                    int num = 1;
                                    foreach (var r in _listVPBH)
                                    {
                                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                                        DungChung.Bien.MangHaiChieu[num, 1] = r.Ho_ten;
                                        if (Convert.ToBoolean(r.Gioi_tinh))
                                        {
                                            DungChung.Bien.MangHaiChieu[num, 2] = r.NSinh;
                                            DungChung.Bien.MangHaiChieu[num, 3] = "";
                                        }
                                        else
                                        {
                                            DungChung.Bien.MangHaiChieu[num, 2] = "";
                                            DungChung.Bien.MangHaiChieu[num, 3] = r.NSinh;
                                        }
                                        DungChung.Bien.MangHaiChieu[num, 4] = r.Ma_the;
                                        DungChung.Bien.MangHaiChieu[num, 5] = r.Ma_dkbd;
                                        DungChung.Bien.MangHaiChieu[num, 6] = r.Ma_benh;
                                        DungChung.Bien.MangHaiChieu[num, 7] = r.Ngaykham;
                                        DungChung.Bien.MangHaiChieu[num, 8] = r.T_tongchi;
                                        DungChung.Bien.MangHaiChieu[num, 9] = r.T_xn;
                                        DungChung.Bien.MangHaiChieu[num, 10] = r.T_cdha;
                                        DungChung.Bien.MangHaiChieu[num, 11] = r.T_thuoc;
                                        DungChung.Bien.MangHaiChieu[num, 12] = r.T_mau;
                                        DungChung.Bien.MangHaiChieu[num, 13] = r.T_pttt;
                                        DungChung.Bien.MangHaiChieu[num, 14] = r.T_vtyt;
                                        DungChung.Bien.MangHaiChieu[num, 15] = r.T_dvkt_tyle;
                                        DungChung.Bien.MangHaiChieu[num, 16] = r.T_thuoc_tyle;
                                        DungChung.Bien.MangHaiChieu[num, 17] = r.T_vtyt_tyle;
                                        DungChung.Bien.MangHaiChieu[num, 18] = r.T_kham;
                                        DungChung.Bien.MangHaiChieu[num, 19] = r.T_giuong;
                                        DungChung.Bien.MangHaiChieu[num, 20] = r.T_vchuyen;
                                        DungChung.Bien.MangHaiChieu[num, 21] = r.T_bntt;
                                        DungChung.Bien.MangHaiChieu[num, 22] = r.T_bhtt;
                                        DungChung.Bien.MangHaiChieu[num, 23] = r.CPNgoaiBH;
                                        num++;
                                    }

                                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, Title, "C:\\Biểu 79.xls", true, this.Name);

                                    #endregion xuat Excel

                                    st = Math.Round(st, 0);
                                    repv.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                                    repv.DataSource = _listVPBH;
                                    repv.BindingData(kieungay, SXTheoDT);
                                    repv.CreateDocument();
                                    frm.prcIN.PrintingSystem = repv.PrintingSystem;
                                    frm.ShowDialog();
                                }
                                else
                                {
                                    BaoCao.Rep_79a_HD_1399 repv = new BaoCao.Rep_79a_HD_1399(SXTheoDT);

                                    repv.HtMaICD = chk_MaICD.Checked;
                                    repv.Ngaythang.Value = theoquy();
                                    repv.HtTuyen = ckHienthiTuyen.Checked;
                                    repv.MaCS.Value = DungChung.Bien.MaBV.Substring(0, 2) + "-" + DungChung.Bien.MaBV.Substring(2, 3);
                                    repv.ngaythanghientai.Value = DungChung.Ham.NgaySangChu(System.DateTime.Now);
                                    if (_MaKPc != null && _MaKPc != 0)
                                    {
                                        repv.paramKhoaPhong.Value = lupKhoaphong.Text.ToUpper();
                                    }
                                    string kieungay = cboKieuNT.Text;
                                    double st = 0;
                                    if (chk_MaICD.Checked)
                                    {
                                        if (lupDoituong.Text == "BHYT" || lupDoituong.Text == "Tất cả")//&& ckTLBN.Checked == false)
                                        {
                                            repv.Title.Value = ("THỐNG KÊ CHI PHÍ KCB NGOẠI TRÚ THEO MÃ BỆNH").ToUpper();
                                            repv.TongtienString.Value = ("Tổng số tiền đề nghị thanh toán (Viết bằng chữ)");
                                            st = _listVPBH.Sum(p => p.T_bhtt);
                                            Title = ("THỐNG KÊ CHI PHÍ KCB NGOẠI TRÚ THEO MÃ BỆNH").ToUpper();
                                        }
                                        else
                                        {
                                            repv.Title.Value = ("THỐNG KÊ CHI PHÍ KCB NGOẠI TRÚ THEO MÃ BỆNH").ToUpper();
                                            repv.TongtienString.Value = ("Tổng số tiền thanh toán (Viết bằng chữ)");
                                            st = _listVPBH.Sum(p => p.T_bntt);
                                            Title = ("THỐNG KÊ CHI PHÍ KCB NGOẠI TRÚ THEO MÃ BỆNH").ToUpper();
                                        }
                                    }
                                    else
                                    {
                                        if (lupDoituong.Text == "BHYT" || lupDoituong.Text == "Tất cả")//&& ckTLBN.Checked == false)
                                        {
                                            repv.Title.Value = ("Danh sách người bệnh bảo hiểm y tế khám chữa bệnh ngoại trú đề nghị thanh toán").ToUpper();
                                            st = _listVPBH.Sum(p => p.T_bhtt);
                                            repv.TongtienString.Value = ("Tổng số tiền đề nghị thanh toán (Viết bằng chữ)");
                                            Title = ("Danh sách người bệnh bảo hiểm y tế khám chữa bệnh ngoại trú đề nghị thanh toán").ToUpper();
                                        }
                                        else
                                        {
                                            repv.Title.Value = ("Danh sách người bệnh khám chữa bệnh ngoại trú thanh toán").ToUpper();
                                            repv.TongtienString.Value = ("Tổng số tiền thanh toán (Viết bằng chữ)");
                                            st = _listVPBH.Sum(p => p.T_bntt);
                                            Title = ("Danh sách người bệnh khám chữa bệnh ngoại trú thanh toán").ToUpper();
                                        }
                                    }

                                    #region xuat Excel

                                    string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                    int[] _arrWidth = new int[] { };
                                    string[] _tieude = { "TT", "Họ và tên", "Năm sinh nam", "Năm sinh nữ", "Mã thẻ BHYT", "Mã ĐK KCB", "Mã bệnh", "Ngày khám", "Tổng chi phí khám chưa bệnh BHYT", "Không áp dụng TLTT Xét nghiệm", "Không áp dụng TLTT CĐHA TDCN", "Không áp dụng TLTT Thuốc DT", "Không áp dụng TLTT Máu", "Không áp dụng TLTT TT PT", "Không áp dụng TLTT VTYT", "Thanh toán theo tỷ lệ DV KT", "Thanh toán theo tỷ lệ Thuốc", "Thanh toán theo tỷ lệ VTYT", "Tiền khám", "Vận chuyển", "Người bệnh cùng chi trả", "Chi phí đề nghị BHXH thanh toán Tổng", "Chi phí đề nghị BHXH thanh toán Trong đó CP ngoài quỹ ĐS" };
                                    DungChung.Bien.MangHaiChieu = new Object[_listVPBH.ToList().Count + 1, 27];
                                    for (int i = 0; i < _tieude.Length; i++)
                                    {
                                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                    }

                                    int num = 1;
                                    foreach (var r in _listVPBH)
                                    {
                                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                                        DungChung.Bien.MangHaiChieu[num, 1] = r.Ho_ten;
                                        if (Convert.ToBoolean(r.Gioi_tinh))
                                        {
                                            DungChung.Bien.MangHaiChieu[num, 2] = r.NSinh;
                                            DungChung.Bien.MangHaiChieu[num, 3] = "";
                                        }
                                        else
                                        {
                                            DungChung.Bien.MangHaiChieu[num, 2] = "";
                                            DungChung.Bien.MangHaiChieu[num, 3] = r.NSinh;
                                        }
                                        DungChung.Bien.MangHaiChieu[num, 4] = r.Ma_the;
                                        DungChung.Bien.MangHaiChieu[num, 5] = r.Ma_dkbd;
                                        DungChung.Bien.MangHaiChieu[num, 6] = r.Ma_benh;
                                        DungChung.Bien.MangHaiChieu[num, 7] = r.Ngaykham;
                                        DungChung.Bien.MangHaiChieu[num, 8] = r.T_tongchi;
                                        DungChung.Bien.MangHaiChieu[num, 9] = r.T_xn;
                                        DungChung.Bien.MangHaiChieu[num, 10] = r.T_cdha;
                                        DungChung.Bien.MangHaiChieu[num, 11] = r.T_thuoc;
                                        DungChung.Bien.MangHaiChieu[num, 12] = r.T_mau;
                                        DungChung.Bien.MangHaiChieu[num, 13] = r.T_pttt;
                                        DungChung.Bien.MangHaiChieu[num, 14] = r.T_vtyt;
                                        DungChung.Bien.MangHaiChieu[num, 15] = r.T_dvkt_tyle;
                                        DungChung.Bien.MangHaiChieu[num, 16] = r.T_thuoc_tyle;
                                        DungChung.Bien.MangHaiChieu[num, 17] = r.T_vtyt_tyle;
                                        DungChung.Bien.MangHaiChieu[num, 18] = r.T_kham;
                                        DungChung.Bien.MangHaiChieu[num, 19] = r.T_vchuyen;
                                        DungChung.Bien.MangHaiChieu[num, 20] = r.T_bntt;
                                        DungChung.Bien.MangHaiChieu[num, 21] = r.T_bhtt;
                                        DungChung.Bien.MangHaiChieu[num, 22] = r.CPNgoaiBH;
                                        num++;
                                    }

                                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, Title, "C:\\Biểu 79.xls", true, this.Name);

                                    #endregion xuat Excel

                                    st = Math.Round(st, 0);
                                    repv.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                                    repv.DataSource = _listVPBH;
                                    repv.BindingData(kieungay, SXTheoDT);
                                    repv.CreateDocument();
                                    frm.prcIN.PrintingSystem = repv.PrintingSystem;
                                    frm.ShowDialog();
                                }
                            }

                            #endregion bc chung
                        }

                        #endregion báo cáo biểu 79 chi tiết

                        bool chonFont = false;
                        if (rdFont.SelectedIndex == 0)
                            chonFont = true;
                        else
                            chonFont = false;
                        string font = "TCVN3";
                        if (Xuatex.Checked)
                        {
                            if (chk_MaICD.Checked)
                            {
                                if (Cls79_80.xuatExcel_ICD10(_listVPBH, txtFilePath.Text, chonFont, font))
                                    MessageBox.Show("Xuất thành công!");
                                else
                                    MessageBox.Show("Lỗi xuất Excel!");
                            }
                            else if (ck3360.Checked)
                            {
                                // if (Cls79_80.xuatExcel( _listVPBH, txtFilePath.Text,chonFont, font))
                                if (Cls79_80.xuatExcelRange(_listVPBH, txtFilePath.Text, chonFont, font, ck_InTienCongKham.Checked))
                                    MessageBox.Show("Xuất thành công!");
                                else
                                    MessageBox.Show("Lỗi xuất Excel!");
                            }
                            else if (chk_QD917_excell.Checked)
                            {
                                if (Cls79_80.xuatExcelRangeQD917(_listVPBH, txtFilePath.Text, chonFont, font))
                                    MessageBox.Show("Xuất thành công!");
                                else
                                    MessageBox.Show("Lỗi xuất Excel!");
                            }
                            else if (ckXuatTheoBC.Checked)
                            {
                                if (ckTLBH.Checked || ckTLBN.Checked)
                                {
                                    if (Cls79_80.xuatExcelRangeBC_TyleBHTT(_listVPBH, txtFilePath.Text, chonFont, font, ck_InTienCongKham.Checked, theoquy()))
                                        MessageBox.Show("Xuất thành công!");
                                    else
                                        MessageBox.Show("Lỗi xuất Excel!");
                                }
                                else
                                {
                                    if (Cls79_80.xuatExcelRangeBC(_listVPBH, txtFilePath.Text, chonFont, font, ck_InTienCongKham.Checked, theoquy()))
                                        MessageBox.Show("Xuất thành công!");
                                    else
                                        MessageBox.Show("Lỗi xuất Excel!");
                                }
                            }
                            else
                            {
                                if (Cls79_80.xuatExcelRange_Old(_listVPBH, txtFilePath.Text, chonFont, font, ck_InTienCongKham.Checked))
                                    MessageBox.Show("Xuất thành công!");
                                else
                                    MessageBox.Show("Lỗi xuất Excel!");
                            }
                        }
                        if (ckXuatXML.Checked)
                        {
                            // if (Cls79_80.xuatExcel( _listVPBH, txtFilePath.Text,chonFont, font))
                            if (Cls79_80.xuatXML(_listMucTT, _hangbv, _listVPBH, txtXMLFilePath.Text, rdBieu.SelectedIndex))
                                MessageBox.Show("Xuất xml thành công!");
                            else
                                MessageBox.Show("Lỗi xuất XML!");
                        }
                    }

                    #endregion in chi tiết

                    #region in tổng hợp

                    else
                    {
                        var q = (from l in _listVPBH
                                 group l by new { l.Tuyen, l.NTinh } into kq
                                 select new
                                 {
                                     NTinh = kq.Key.NTinh,
                                     Tuyen = kq.Key.Tuyen == 1 ? "Đúng tuyến" : "Trái tuyến",
                                     STT = kq.Key.Tuyen == 1 ? "I" : "II",
                                     So_ngay_dtri = kq.Sum(p => p.So_ngay_dtri),
                                     T_thuoc = kq.Sum(p => p.T_thuoc),
                                     T_cdha = kq.Sum(p => p.T_cdha),
                                     T_kham = kq.Sum(p => p.T_kham),
                                     T_giuong = kq.Sum(p => p.T_giuong),
                                     T_xn = kq.Sum(p => p.T_xn),
                                     T_mau = kq.Sum(p => p.T_mau),
                                     T_pttt = kq.Sum(p => p.T_pttt),
                                     T_vtyt = kq.Sum(p => p.T_vtyt),
                                     T_vtyt_tyle = kq.Sum(p => p.T_vtyt_tyle),
                                     T_dvkt_tyle = kq.Sum(p => p.T_dvkt_tyle),
                                     T_thuoc_tyle = kq.Sum(p => p.T_thuoc_tyle),
                                     T_vchuyen = kq.Sum(p => p.T_vchuyen),
                                     T_bhtt = kq.Sum(p => p.T_bhtt),
                                     T_bntt = kq.Sum(p => p.T_bntt),
                                     T_tongchi = kq.Sum(p => p.T_tongchi),
                                     TongCong = kq.Sum(p => p.TongCong),
                                     Thanhtien = kq.Sum(p => p.Thanhtien),
                                     Soluot = kq.Sum(p => p.Soluot)
                                 }).OrderBy(p => p.NTinh).ThenBy(p => p.Tuyen).ToList();
                        var q1 = (from l in _listVPBH.Distinct()
                                  group l by new { l.NTinh, l.Ma_lydo_vvien } into kq
                                  select new
                                  {
                                      NTinh = kq.Key.NTinh,
                                      Tuyen = kq.Key.Ma_lydo_vvien == 3 ? "Cấp cứu" : (kq.Key.Ma_lydo_vvien == 1 ? "Đúng tuyến" : "Trái tuyến"),
                                      STT = kq.Key.Ma_lydo_vvien == 3 ? "III" : (kq.Key.Ma_lydo_vvien == 1 ? "I" : "II"),
                                      So_ngay_dtri = kq.Sum(p => p.So_ngay_dtri),
                                      T_thuoc = kq.Sum(p => p.T_thuoc),
                                      T_cdha = kq.Sum(p => p.T_cdha),
                                      T_kham = kq.Sum(p => p.T_kham),
                                      T_giuong = kq.Sum(p => p.T_giuong),
                                      T_xn = kq.Sum(p => p.T_xn),
                                      T_mau = kq.Sum(p => p.T_mau),
                                      T_pttt = kq.Sum(p => p.T_pttt),
                                      T_vtyt = kq.Sum(p => p.T_vtyt),
                                      T_vtyt_tyle = kq.Sum(p => p.T_vtyt_tyle),
                                      T_dvkt_tyle = kq.Sum(p => p.T_dvkt_tyle),
                                      T_thuoc_tyle = kq.Sum(p => p.T_thuoc_tyle),
                                      T_vchuyen = kq.Sum(p => p.T_vchuyen),
                                      T_bhtt = kq.Sum(p => p.T_bhtt),
                                      T_bntt = kq.Sum(p => p.T_bntt),
                                      T_tongchi = kq.Sum(p => p.T_tongchi),
                                      TongCong = kq.Sum(p => p.TongCong),
                                      Thanhtien = kq.Sum(p => p.Thanhtien),
                                      Soluot = kq.Sum(p => p.Soluot)
                                  }).Distinct().OrderBy(p => p.NTinh).ThenBy(p => p.STT).ToList();

                        double st = 0;

                        #region in tổng hợp biểu 80

                        if (rdBieu.SelectedIndex == 1)
                        {
                            if (ck_InTienCongKham.Checked)
                            {
                                BaoCao.Rep_80aTH_1399_CongKham rep = new BaoCao.Rep_80aTH_1399_CongKham(SXTheoDT);
                                rep.Ngaythang.Value = theoquy();
                                rep.Dtuong.Value = lupDoituong.Text;
                                rep.HtTuyen = ckHienthiTuyen.Checked;
                                rep.Nguoilapbieu.Value = DungChung.Bien.NguoiLapBieu;
                                rep.Ketoantruong.Value = DungChung.Bien.KeToanTruong;
                                rep.Thutruong.Value = DungChung.Bien.GiamDoc;
                                if (lupDoituong.Text == "BHYT" || lupDoituong.Text == "Tất cả")//&& ckTLBN.Checked == false)
                                {
                                    rep.Title.Value = ("Danh sách đề nghị thanh toán chi phí KCB nội trú").ToUpper();
                                    rep.TongtienString.Value = ("Tổng số tiền đề nghị thanh toán (Viết bằng chữ)");
                                    st = _listVPBH.Sum(p => p.T_bhtt);
                                    Title = ("Danh sách đề nghị thanh toán chi phí KCB nội trú").ToUpper();
                                }
                                else
                                {
                                    rep.Title.Value = ("Danh sách thanh toán chi phí KCB nội trú của bệnh nhân").ToUpper();
                                    rep.TongtienString.Value = ("Tổng số tiền thanh toán (Viết bằng chữ)");
                                    st = _listVPBH.Sum(p => p.T_bntt);
                                    Title = ("Danh sách thanh toán chi phí KCB nội trú của bệnh nhân").ToUpper();
                                }
                                if (_MaKPc != null && _MaKPc != 0)
                                {
                                    rep.paramKhoaPhong.Value = "Khoa phòng: " + lupKhoaphong.Text;
                                }

                                #region xuat Excel

                                string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                int[] _arrWidth = new int[] { };
                                string[] _tieude = { "TT", "Khám chữa bệnh", "Số lượt", "Số ngày", "Tổng chi phí khám chưa bệnh BHYT", "Không áp dụng TLTT Xét nghiệm", "Không áp dụng TLTT CĐHA TDCN", "Không áp dụng TLTT Thuốc DT", "Không áp dụng TLTT Máu", "Không áp dụng TLTT TT PT", "Không áp dụng TLTT VTYT", "Thanh toán theo tỷ lệ DV KT", "Thanh toán theo tỷ lệ Thuốc", "Thanh toán theo tỷ lệ VTYT", "Tiền CK", "Tiền giường", "Vận chuyển", "Người bệnh cùng chi trả", "Chi phí đề nghị BHXH thanh toán Tổng", "Chi phí đề nghị BHXH thanh toán Trong đó CP ngoài quỹ ĐS" };
                                DungChung.Bien.MangHaiChieu = new Object[q.ToList().Count + 1, 27];
                                for (int i = 0; i < _tieude.Length; i++)
                                {
                                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                }

                                int num = 1;
                                foreach (var r in q)
                                {
                                    DungChung.Bien.MangHaiChieu[num, 0] = num;
                                    DungChung.Bien.MangHaiChieu[num, 1] = r.Tuyen;
                                    DungChung.Bien.MangHaiChieu[num, 2] = r.Soluot;
                                    DungChung.Bien.MangHaiChieu[num, 3] = r.So_ngay_dtri;
                                    DungChung.Bien.MangHaiChieu[num, 4] = r.T_tongchi;
                                    DungChung.Bien.MangHaiChieu[num, 5] = r.T_xn;
                                    DungChung.Bien.MangHaiChieu[num, 6] = r.T_cdha;
                                    DungChung.Bien.MangHaiChieu[num, 7] = r.T_thuoc;
                                    DungChung.Bien.MangHaiChieu[num, 8] = r.T_mau;
                                    DungChung.Bien.MangHaiChieu[num, 9] = r.T_pttt;
                                    DungChung.Bien.MangHaiChieu[num, 10] = r.T_vtyt;
                                    DungChung.Bien.MangHaiChieu[num, 11] = r.T_dvkt_tyle;
                                    DungChung.Bien.MangHaiChieu[num, 12] = r.T_thuoc_tyle;
                                    DungChung.Bien.MangHaiChieu[num, 13] = r.T_vtyt_tyle;
                                    DungChung.Bien.MangHaiChieu[num, 14] = r.T_kham;
                                    DungChung.Bien.MangHaiChieu[num, 15] = r.T_giuong;
                                    DungChung.Bien.MangHaiChieu[num, 16] = r.T_vchuyen;
                                    DungChung.Bien.MangHaiChieu[num, 17] = r.T_bntt;
                                    DungChung.Bien.MangHaiChieu[num, 18] = r.T_bhtt;
                                    DungChung.Bien.MangHaiChieu[num, 19] = r.T_vchuyen;
                                    num++;
                                }

                                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, Title, "C:\\Biểu 80 tổng hợp.xls", true, this.Name);

                                #endregion xuat Excel

                                st = Math.Round(st, 0);
                                rep.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                                rep.DataSource = q;
                                rep.BindingData(SXTheoDT);
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                            else
                            {
                                BaoCao.Rep_80aTH_1399 rep = new BaoCao.Rep_80aTH_1399();
                                if (DungChung.Bien.MaBV == "24012" && chkTitleICD.Checked == true)
                                {
                                    rep.txtDSMaBenhChon.Visible = true;
                                }
                                rep.DSMaBenhChon.Value = "Mã ICD: ";
                                if (_lDSMaDV.Count() == 0 || _lDSMaDV.Count() == _dataContext.ICD10.Count())
                                {
                                    rep.DSMaBenhChon.Value += "Tất cả";
                                }
                                else
                                    foreach (var ma in _lDSMaICD)
                                    {
                                        var ten = _dataContext.ICD10.Where(p => p.MaICD == ma).Select(p => p.TenICD).FirstOrDefault();
                                        if (ten != null)
                                            rep.DSMaBenhChon.Value += ma + ": " + ten + "; ";
                                    }
                                rep.Ngaythang.Value = theoquy();
                                rep.Dtuong.Value = lupDoituong.Text;
                                rep.HtTuyen = ckHienthiTuyen.Checked;
                                rep.Nguoilapbieu.Value = DungChung.Bien.NguoiLapBieu;
                                rep.Ketoantruong.Value = DungChung.Bien.KeToanTruong;
                                rep.Thutruong.Value = DungChung.Bien.GiamDoc;
                                if (lupDoituong.Text == "BHYT" || lupDoituong.Text == "Tất cả")// && ckTLBN.Checked == false)
                                {
                                    rep.Title.Value = ("Danh sách đề nghị thanh toán chi phí KCB nội trú").ToUpper();
                                    rep.TongtienString.Value = ("Tổng số tiền đề nghị thanh toán (Viết bằng chữ)");
                                    st = _listVPBH.Sum(p => p.T_bhtt);
                                    Title = ("Danh sách đề nghị thanh toán chi phí KCB nội trú").ToUpper();
                                }
                                else
                                {
                                    rep.Title.Value = ("Danh sách thanh toán chi phí KCB nội trú của bệnh nhân").ToUpper();
                                    rep.TongtienString.Value = ("Tổng số tiền thanh toán (Viết bằng chữ)");
                                    st = _listVPBH.Sum(p => p.T_bntt);
                                    Title = ("Danh sách thanh toán chi phí KCB nội trú của bệnh nhân").ToUpper();
                                }
                                if (DungChung.Bien.MaBV == "24009")
                                {
                                    rep.paramKhoaPhong.Value = "  Khoa phòng: ";
                                    if (lstMaKP.Count() == _lKphong.Where(p => p.TenKP != "Tất cả").Count())
                                    {
                                        rep.paramKhoaPhong.Value += "Tất cả";
                                    }
                                    else
                                        foreach (var i in lstTenKP)
                                        {
                                            rep.paramKhoaPhong.Value += i + "; ";
                                        }
                                }

                                #region xuat Excel

                                string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                int[] _arrWidth = new int[] { };
                                string[] _tieude = { "TT", "Khám chữa bệnh", "Số lượt", "Số ngày", "Tổng chi phí khám chưa bệnh BHYT", "Không áp dụng TLTT Xét nghiệm", "Không áp dụng TLTT CĐHA TDCN", "Không áp dụng TLTT Thuốc DT", "Không áp dụng TLTT Máu", "Không áp dụng TLTT TT PT", "Không áp dụng TLTT VTYT", "Thanh toán theo tỷ lệ DV KT", "Thanh toán theo tỷ lệ Thuốc", "Thanh toán theo tỷ lệ VTYT", "Tiền giường", "Vận chuyển", "Người bệnh cùng chi trả", "Chi phí đề nghị BHXH thanh toán Tổng", "Chi phí đề nghị BHXH thanh toán Trong đó CP ngoài quỹ ĐS" };
                                DungChung.Bien.MangHaiChieu = new Object[q.ToList().Count + 1, 27];
                                for (int i = 0; i < _tieude.Length; i++)
                                {
                                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                }

                                int num = 1;
                                foreach (var r in q)
                                {
                                    DungChung.Bien.MangHaiChieu[num, 0] = num;
                                    DungChung.Bien.MangHaiChieu[num, 1] = r.Tuyen;
                                    DungChung.Bien.MangHaiChieu[num, 2] = r.Soluot;
                                    DungChung.Bien.MangHaiChieu[num, 3] = r.So_ngay_dtri;
                                    DungChung.Bien.MangHaiChieu[num, 4] = r.T_tongchi;
                                    DungChung.Bien.MangHaiChieu[num, 5] = r.T_xn;
                                    DungChung.Bien.MangHaiChieu[num, 6] = r.T_cdha;
                                    DungChung.Bien.MangHaiChieu[num, 7] = r.T_thuoc;
                                    DungChung.Bien.MangHaiChieu[num, 8] = r.T_mau;
                                    DungChung.Bien.MangHaiChieu[num, 9] = r.T_pttt;
                                    DungChung.Bien.MangHaiChieu[num, 10] = r.T_vtyt;
                                    DungChung.Bien.MangHaiChieu[num, 11] = r.T_dvkt_tyle;
                                    DungChung.Bien.MangHaiChieu[num, 12] = r.T_thuoc_tyle;
                                    DungChung.Bien.MangHaiChieu[num, 13] = r.T_vtyt_tyle;
                                    DungChung.Bien.MangHaiChieu[num, 14] = r.T_kham;
                                    DungChung.Bien.MangHaiChieu[num, 15] = r.T_vchuyen;
                                    DungChung.Bien.MangHaiChieu[num, 16] = r.T_bntt;
                                    DungChung.Bien.MangHaiChieu[num, 17] = r.T_bhtt;
                                    DungChung.Bien.MangHaiChieu[num, 18] = r.T_vchuyen;
                                    num++;
                                }

                                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, Title, "C:\\Biểu 80 tổng hợp.xls", true, this.Name);

                                #endregion xuat Excel

                                st = Math.Round(st, 0);
                                rep.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                                rep.DataSource = q;
                                rep.BindingData(SXTheoDT);
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                        }

                        #endregion in tổng hợp biểu 80

                        #region in tổng hợp

                        else if (rdBieu.SelectedIndex == 2)
                        {
                            if (ck_InTienCongKham.Checked)
                            {
                                BaoCao.Rep_79_80_TH rep = new BaoCao.Rep_79_80_TH();
                                rep.Ngaythang.Value = theoquy();
                                rep.Dtuong.Value = lupDoituong.Text;
                                rep.HtTuyen = ckHienthiTuyen.Checked;
                                rep.Nguoilapbieu.Value = DungChung.Bien.NguoiLapBieu;
                                rep.Ketoantruong.Value = DungChung.Bien.KeToanTruong;
                                rep.Thutruong.Value = DungChung.Bien.GiamDoc;
                                if (lupDoituong.Text == "BHYT" || lupDoituong.Text == "Tất cả")//&& ckTLBN.Checked == false)
                                {
                                    rep.Title.Value = ("Danh sách đề nghị thanh toán chi phí KCB").ToUpper();
                                    rep.TongtienString.Value = ("Tổng số tiền đề nghị thanh toán (Viết bằng chữ)");
                                    st = _listVPBH.Sum(p => p.T_bhtt);
                                    Title = ("Danh sách đề nghị thanh toán chi phí KCB").ToUpper();
                                }
                                else
                                {
                                    rep.Title.Value = ("Danh sách thanh toán chi phí KCB").ToUpper();
                                    rep.TongtienString.Value = ("Tổng số tiền thanh toán (Viết bằng chữ)");
                                    st = _listVPBH.Sum(p => p.T_bntt);
                                    Title = ("Danh sách thanh toán chi phí KCB").ToUpper();
                                }
                                if (_MaKPc != null && _MaKPc != 0)
                                {
                                    rep.paramKhoaPhong.Value = "Khoa phòng: " + lupKhoaphong.Text;
                                }

                                #region xuat Excel

                                string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                int[] _arrWidth = new int[] { };
                                string[] _tieude = { "TT", "Khám chữa bệnh", "Số lượt", "Số ngày", "Tổng chi phí khám chưa bệnh BHYT", "Không áp dụng TLTT Xét nghiệm", "Không áp dụng TLTT CĐHA TDCN", "Không áp dụng TLTT Thuốc DT", "Không áp dụng TLTT Máu", "Không áp dụng TLTT TT PT", "Không áp dụng TLTT VTYT", "Thanh toán theo tỷ lệ DV KT", "Thanh toán theo tỷ lệ Thuốc", "Thanh toán theo tỷ lệ VTYT", "Tiền CK", "Tiền giường", "Vận chuyển", "Người bệnh cùng chi trả", "Chi phí đề nghị BHXH thanh toán Tổng", "Chi phí đề nghị BHXH thanh toán Trong đó CP ngoài quỹ ĐS" };
                                DungChung.Bien.MangHaiChieu = new Object[q.ToList().Count + 1, 27];
                                for (int i = 0; i < _tieude.Length; i++)
                                {
                                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                }

                                int num = 1;
                                foreach (var r in q)
                                {
                                    DungChung.Bien.MangHaiChieu[num, 0] = num;
                                    DungChung.Bien.MangHaiChieu[num, 1] = r.Tuyen;
                                    DungChung.Bien.MangHaiChieu[num, 2] = r.Soluot;
                                    DungChung.Bien.MangHaiChieu[num, 3] = r.So_ngay_dtri;
                                    DungChung.Bien.MangHaiChieu[num, 4] = r.T_tongchi;
                                    DungChung.Bien.MangHaiChieu[num, 5] = r.T_xn;
                                    DungChung.Bien.MangHaiChieu[num, 6] = r.T_cdha;
                                    DungChung.Bien.MangHaiChieu[num, 7] = r.T_thuoc;
                                    DungChung.Bien.MangHaiChieu[num, 8] = r.T_mau;
                                    DungChung.Bien.MangHaiChieu[num, 9] = r.T_pttt;
                                    DungChung.Bien.MangHaiChieu[num, 10] = r.T_vtyt;
                                    DungChung.Bien.MangHaiChieu[num, 11] = r.T_dvkt_tyle;
                                    DungChung.Bien.MangHaiChieu[num, 12] = r.T_thuoc_tyle;
                                    DungChung.Bien.MangHaiChieu[num, 13] = r.T_vtyt_tyle;
                                    DungChung.Bien.MangHaiChieu[num, 14] = r.T_kham;
                                    DungChung.Bien.MangHaiChieu[num, 15] = r.T_giuong;
                                    DungChung.Bien.MangHaiChieu[num, 16] = r.T_vchuyen;
                                    DungChung.Bien.MangHaiChieu[num, 17] = r.T_bntt;
                                    DungChung.Bien.MangHaiChieu[num, 18] = r.T_bhtt;
                                    DungChung.Bien.MangHaiChieu[num, 19] = r.T_vchuyen;
                                    num++;
                                }

                                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, Title, "C:\\Biểu tổng hợp.xls", true, this.Name);

                                #endregion xuat Excel

                                st = Math.Round(st, 0);
                                rep.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                                rep.DataSource = q1;
                                rep.BindingData(SXTheoDT);
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                            else
                            {
                                BaoCao.Rep_79_80_TH rep = new BaoCao.Rep_79_80_TH();
                                rep.Ngaythang.Value = theoquy();
                                rep.Dtuong.Value = lupDoituong.Text;
                                rep.HtTuyen = ckHienthiTuyen.Checked;
                                rep.Nguoilapbieu.Value = DungChung.Bien.NguoiLapBieu;
                                rep.Ketoantruong.Value = DungChung.Bien.KeToanTruong;
                                rep.Thutruong.Value = DungChung.Bien.GiamDoc;
                                if (lupDoituong.Text == "BHYT" || lupDoituong.Text == "Tất cả")// && ckTLBN.Checked == false)
                                {
                                    rep.Title.Value = ("Danh sách đề nghị thanh toán chi phí KCB").ToUpper();
                                    rep.TongtienString.Value = ("Tổng số tiền đề nghị thanh toán (Viết bằng chữ)");
                                    st = _listVPBH.Sum(p => p.T_bhtt);
                                    Title = ("Danh sách đề nghị thanh toán chi phí KCB").ToUpper();
                                }
                                else
                                {
                                    rep.Title.Value = ("Danh sách thanh toán chi phí KCB").ToUpper();
                                    rep.TongtienString.Value = ("Tổng số tiền thanh toán (Viết bằng chữ)");
                                    st = _listVPBH.Sum(p => p.T_bntt);
                                    Title = ("Danh sách thanh toán chi phí KCB").ToUpper();
                                }
                                if (_MaKPc != null && _MaKPc != 0)
                                {
                                    rep.paramKhoaPhong.Value = "Khoa phòng: " + lupKhoaphong.Text;
                                }

                                #region xuat Excel

                                string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                int[] _arrWidth = new int[] { };
                                string[] _tieude = { "TT", "Khám chữa bệnh", "Số lượt", "Số ngày", "Tổng chi phí khám chưa bệnh BHYT", "Không áp dụng TLTT Xét nghiệm", "Không áp dụng TLTT CĐHA TDCN", "Không áp dụng TLTT Thuốc DT", "Không áp dụng TLTT Máu", "Không áp dụng TLTT TT PT", "Không áp dụng TLTT VTYT", "Thanh toán theo tỷ lệ DV KT", "Thanh toán theo tỷ lệ Thuốc", "Thanh toán theo tỷ lệ VTYT", "Tiền giường", "Vận chuyển", "Người bệnh cùng chi trả", "Chi phí đề nghị BHXH thanh toán Tổng", "Chi phí đề nghị BHXH thanh toán Trong đó CP ngoài quỹ ĐS" };
                                DungChung.Bien.MangHaiChieu = new Object[q.ToList().Count + 1, 27];
                                for (int i = 0; i < _tieude.Length; i++)
                                {
                                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                }

                                int num = 1;
                                foreach (var r in q)
                                {
                                    DungChung.Bien.MangHaiChieu[num, 0] = num;
                                    DungChung.Bien.MangHaiChieu[num, 1] = r.Tuyen;
                                    DungChung.Bien.MangHaiChieu[num, 2] = r.Soluot;
                                    DungChung.Bien.MangHaiChieu[num, 3] = r.So_ngay_dtri;
                                    DungChung.Bien.MangHaiChieu[num, 4] = r.T_tongchi;
                                    DungChung.Bien.MangHaiChieu[num, 5] = r.T_xn;
                                    DungChung.Bien.MangHaiChieu[num, 6] = r.T_cdha;
                                    DungChung.Bien.MangHaiChieu[num, 7] = r.T_thuoc;
                                    DungChung.Bien.MangHaiChieu[num, 8] = r.T_mau;
                                    DungChung.Bien.MangHaiChieu[num, 9] = r.T_pttt;
                                    DungChung.Bien.MangHaiChieu[num, 10] = r.T_vtyt;
                                    DungChung.Bien.MangHaiChieu[num, 11] = r.T_dvkt_tyle;
                                    DungChung.Bien.MangHaiChieu[num, 12] = r.T_thuoc_tyle;
                                    DungChung.Bien.MangHaiChieu[num, 13] = r.T_vtyt_tyle;
                                    DungChung.Bien.MangHaiChieu[num, 14] = r.T_kham;
                                    DungChung.Bien.MangHaiChieu[num, 15] = r.T_vchuyen;
                                    DungChung.Bien.MangHaiChieu[num, 16] = r.T_bntt;
                                    DungChung.Bien.MangHaiChieu[num, 17] = r.T_bhtt;
                                    DungChung.Bien.MangHaiChieu[num, 18] = r.T_vchuyen;
                                    num++;
                                }

                                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, Title, "C:\\Biểu tổng hợp.xls", true, this.Name);

                                #endregion xuat Excel

                                st = Math.Round(st, 0);
                                rep.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(st, " đồng.");
                                rep.DataSource = q1;
                                rep.BindingData(SXTheoDT);
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                        }

                        #endregion in tổng hợp

                        #region in tổng hợp biểu 79

                        else
                        {
                            if (DungChung.Ham.hangBVCK(DungChung.Bien.MaBV) == 4 || DungChung.Bien.MaBV == "30280")
                            {
                                BaoCao.Rep_79aTH_1399_TienGiuong rep = new BaoCao.Rep_79aTH_1399_TienGiuong(SXTheoDT);
                                rep.ngaythanghientai.Value = DungChung.Ham.NgaySangChu(System.DateTime.Now);
                                rep.MaCS.Value = DungChung.Bien.MaBV.Substring(0, 2) + "-" + DungChung.Bien.MaBV.Substring(2, 3);
                                rep.Ngaythang.Value = theoquy();
                                rep.HtTuyen = ckHienthiTuyen.Checked;
                                rep.Nguoilapbieu.Value = DungChung.Bien.NguoiLapBieu;
                                rep.Ketoantruong.Value = DungChung.Bien.KeToanTruong;
                                rep.Thutruong.Value = DungChung.Bien.GiamDoc;
                                rep.DataSource = q;
                                double stc = 0;
                                if (lupDoituong.Text == "BHYT" || lupDoituong.Text == "Tất cả")//&& ckTLBN.Checked == false)
                                {
                                    rep.Title.Value = ("Danh sách đề nghị thanh toán chi phí KCB ngoại trú").ToUpper();
                                    stc = Convert.ToDouble(q.Sum(p => p.T_bhtt));
                                    rep.TongtienString.Value = ("Tổng số tiền đề nghị thanh toán (Viết bằng chữ)");
                                    Title = ("Danh sách đề nghị thanh toán chi phí KCB ngoại trú").ToUpper();
                                }
                                else
                                {
                                    rep.Title.Value = ("Danh sách thanh toán chi phí KCB ngoại trú của bệnh nhân").ToUpper();
                                    rep.TongtienString.Value = ("Tổng số tiền thanh toán (Viết bằng chữ)");
                                    stc = Convert.ToDouble(q.Sum(p => p.T_bntt));
                                    Title = ("Danh sách thanh toán chi phí KCB ngoại trú của bệnh nhân").ToUpper();
                                }
                                if (_MaKPc != null && _MaKPc != 0)
                                {
                                    rep.paramKhoaPhong.Value = lupKhoaphong.Text.ToUpper();
                                }

                                #region xuat Excel

                                string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                int[] _arrWidth = new int[] { };
                                string[] _tieude = { "TT", "Khám chữa bệnh", "Số lượt", "Số ngày", "Tổng chi phí khám chưa bệnh BHYT", "Không áp dụng TLTT Xét nghiệm", "Không áp dụng TLTT CĐHA TDCN", "Không áp dụng TLTT Thuốc DT", "Không áp dụng TLTT Máu", "Không áp dụng TLTT TT PT", "Không áp dụng TLTT VTYT", "Thanh toán theo tỷ lệ DV KT", "Thanh toán theo tỷ lệ Thuốc", "Thanh toán theo tỷ lệ VTYT", "Tiền khám", "Tiền giường", "Vận chuyển", "Người bệnh cùng chi trả", "Chi phí đề nghị BHXH thanh toán Tổng", "Chi phí đề nghị BHXH thanh toán Trong đó CP ngoài quỹ ĐS" };
                                DungChung.Bien.MangHaiChieu = new Object[q.ToList().Count + 1, 27];
                                for (int i = 0; i < _tieude.Length; i++)
                                {
                                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                }

                                int num = 1;
                                foreach (var r in q)
                                {
                                    DungChung.Bien.MangHaiChieu[num, 0] = num;
                                    DungChung.Bien.MangHaiChieu[num, 1] = r.Tuyen;
                                    DungChung.Bien.MangHaiChieu[num, 2] = r.Soluot;
                                    DungChung.Bien.MangHaiChieu[num, 3] = r.So_ngay_dtri;
                                    DungChung.Bien.MangHaiChieu[num, 4] = r.T_tongchi;
                                    DungChung.Bien.MangHaiChieu[num, 5] = r.T_xn;
                                    DungChung.Bien.MangHaiChieu[num, 6] = r.T_cdha;
                                    DungChung.Bien.MangHaiChieu[num, 7] = r.T_thuoc;
                                    DungChung.Bien.MangHaiChieu[num, 8] = r.T_mau;
                                    DungChung.Bien.MangHaiChieu[num, 9] = r.T_pttt;
                                    DungChung.Bien.MangHaiChieu[num, 10] = r.T_vtyt;
                                    DungChung.Bien.MangHaiChieu[num, 11] = r.T_dvkt_tyle;
                                    DungChung.Bien.MangHaiChieu[num, 12] = r.T_thuoc_tyle;
                                    DungChung.Bien.MangHaiChieu[num, 13] = r.T_vtyt_tyle;
                                    DungChung.Bien.MangHaiChieu[num, 14] = r.T_kham;
                                    DungChung.Bien.MangHaiChieu[num, 15] = r.T_kham;
                                    DungChung.Bien.MangHaiChieu[num, 16] = r.T_vchuyen;
                                    DungChung.Bien.MangHaiChieu[num, 17] = r.T_bntt;
                                    DungChung.Bien.MangHaiChieu[num, 18] = r.T_bhtt;
                                    DungChung.Bien.MangHaiChieu[num, 19] = r.T_vchuyen;
                                    num++;
                                }

                                frmIn frmc = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, Title, "C:\\Biểu 79 tổng hợp.xls", true, this.Name);

                                #endregion xuat Excel

                                stc = Math.Round(stc, 0);
                                rep.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(stc, " đồng.");
                                rep.BindingData(SXTheoDT);
                                rep.CreateDocument();
                                frmc.prcIN.PrintingSystem = rep.PrintingSystem;
                                frmc.ShowDialog();
                            }
                            else
                            {
                                BaoCao.Rep_79aTH_1399 rep = new BaoCao.Rep_79aTH_1399(SXTheoDT);
                                if (DungChung.Bien.MaBV == "24012" && chkTitleICD.Checked == true)
                                {
                                    rep.txtDSMaBenhChon.Visible = true;
                                }
                                rep.DSMaBenhChon.Value = "Mã ICD: ";
                                if (_lDSMaDV.Count() == 0 || _lDSMaDV.Count() == _dataContext.ICD10.Count())
                                {
                                    rep.DSMaBenhChon.Value += "Tất cả";
                                }
                                else
                                    foreach (var ma in _lDSMaICD)
                                    {
                                        var ten = _dataContext.ICD10.Where(p => p.MaICD == ma).Select(p => p.TenICD).FirstOrDefault();
                                        if (ten != null)
                                            rep.DSMaBenhChon.Value += ma + ": " + ten + "; ";
                                    }
                                rep.ngaythanghientai.Value = DungChung.Ham.NgaySangChu(System.DateTime.Now);
                                rep.MaCS.Value = DungChung.Bien.MaBV.Substring(0, 2) + "-" + DungChung.Bien.MaBV.Substring(2, 3);
                                rep.Ngaythang.Value = theoquy();
                                rep.HtTuyen = ckHienthiTuyen.Checked;
                                rep.Nguoilapbieu.Value = DungChung.Bien.NguoiLapBieu;
                                rep.Ketoantruong.Value = DungChung.Bien.KeToanTruong;
                                rep.Thutruong.Value = DungChung.Bien.GiamDoc;
                                rep.DataSource = q;
                                double stc = 0;
                                if (lupDoituong.Text == "BHYT" || lupDoituong.Text == "Tất cả")//&& ckTLBN.Checked == false)
                                {
                                    rep.Title.Value = ("Danh sách đề nghị thanh toán chi phí KCB ngoại trú").ToUpper();
                                    stc = Convert.ToDouble(q.Sum(p => p.T_bhtt));
                                    rep.TongtienString.Value = ("Tổng số tiền đề nghị thanh toán (Viết bằng chữ)");
                                    Title = ("Danh sách đề nghị thanh toán chi phí KCB ngoại trú").ToUpper();
                                }
                                else
                                {
                                    rep.Title.Value = ("Danh sách thanh toán chi phí KCB ngoại trú của bệnh nhân").ToUpper();
                                    rep.TongtienString.Value = ("Tổng số tiền thanh toán (Viết bằng chữ)");
                                    stc = Convert.ToDouble(q.Sum(p => p.T_bntt));
                                    Title = ("Danh sách thanh toán chi phí KCB ngoại trú của bệnh nhân").ToUpper();
                                }
                                //chon nhieu kp
                                if (DungChung.Bien.MaBV == "24012" || Bien.MaBV == "24009")
                                {
                                    rep.paramKhoaPhong.Value = "  Khoa phòng: ";
                                    if (lstMaKP.Count() == _lKphong.Count())
                                    {
                                        rep.paramKhoaPhong.Value += "Tất cả";
                                    }
                                    else
                                        foreach (var i in lstTenKP)
                                        {
                                            rep.paramKhoaPhong.Value += i + "; ";
                                        }
                                }
                                else if (_MaKPc != null && _MaKPc != 0)
                                {
                                    rep.paramKhoaPhong.Value = lupKhoaphong.Text.ToUpper();
                                }

                                #region xuat Excel

                                string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                int[] _arrWidth = new int[] { };
                                string[] _tieude = { "TT", "Khám chữa bệnh", "Số lượt", "Số ngày", "Tổng chi phí khám chưa bệnh BHYT", "Không áp dụng TLTT Xét nghiệm", "Không áp dụng TLTT CĐHA TDCN", "Không áp dụng TLTT Thuốc DT", "Không áp dụng TLTT Máu", "Không áp dụng TLTT TT PT", "Không áp dụng TLTT VTYT", "Thanh toán theo tỷ lệ DV KT", "Thanh toán theo tỷ lệ Thuốc", "Thanh toán theo tỷ lệ VTYT", "Tiền khám", "Vận chuyển", "Người bệnh cùng chi trả", "Chi phí đề nghị BHXH thanh toán Tổng", "Chi phí đề nghị BHXH thanh toán Trong đó CP ngoài quỹ ĐS" };
                                DungChung.Bien.MangHaiChieu = new Object[q.ToList().Count + 1, 27];
                                for (int i = 0; i < _tieude.Length; i++)
                                {
                                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                }

                                int num = 1;
                                foreach (var r in q)
                                {
                                    DungChung.Bien.MangHaiChieu[num, 0] = num;
                                    DungChung.Bien.MangHaiChieu[num, 1] = r.Tuyen;
                                    DungChung.Bien.MangHaiChieu[num, 2] = r.Soluot;
                                    DungChung.Bien.MangHaiChieu[num, 3] = r.So_ngay_dtri;
                                    DungChung.Bien.MangHaiChieu[num, 4] = r.T_tongchi;
                                    DungChung.Bien.MangHaiChieu[num, 5] = r.T_xn;
                                    DungChung.Bien.MangHaiChieu[num, 6] = r.T_cdha;
                                    DungChung.Bien.MangHaiChieu[num, 7] = r.T_thuoc;
                                    DungChung.Bien.MangHaiChieu[num, 8] = r.T_mau;
                                    DungChung.Bien.MangHaiChieu[num, 9] = r.T_pttt;
                                    DungChung.Bien.MangHaiChieu[num, 10] = r.T_vtyt;
                                    DungChung.Bien.MangHaiChieu[num, 11] = r.T_dvkt_tyle;
                                    DungChung.Bien.MangHaiChieu[num, 12] = r.T_thuoc_tyle;
                                    DungChung.Bien.MangHaiChieu[num, 13] = r.T_vtyt_tyle;
                                    DungChung.Bien.MangHaiChieu[num, 14] = r.T_kham;
                                    DungChung.Bien.MangHaiChieu[num, 15] = r.T_vchuyen;
                                    DungChung.Bien.MangHaiChieu[num, 16] = r.T_bntt;
                                    DungChung.Bien.MangHaiChieu[num, 17] = r.T_bhtt;
                                    DungChung.Bien.MangHaiChieu[num, 18] = r.T_vchuyen;
                                    num++;
                                }

                                frmIn frmc = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, Title, "C:\\Biểu 79 tổng hợp.xls", true, this.Name);

                                #endregion xuat Excel

                                stc = Math.Round(stc, 0);
                                rep.SotienDNTT.Value = QLBV_Library.QLBV_Ham.DocTienBangChu(stc, " đồng.");
                                rep.BindingData(SXTheoDT);
                                rep.CreateDocument();
                                frmc.prcIN.PrintingSystem = rep.PrintingSystem;
                                frmc.ShowDialog();
                            }
                        }

                        #endregion in tổng hợp biểu 79
                    }

                    #endregion in tổng hợp
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu");
                }
            }
            _lDSMaDV = new List<int>();
            _lDSMaICD = new List<string>();
        }

        private string MaKPQD(int _MaKPc)
        {
            string rs = "";
            var q = _lKphong.Where(p => p.MaKP == _MaKPc).ToList();
            if (q.Count > 0)
                rs = q.First().MaQD == null ? "" : q.First().MaQD.ToString();
            return rs;
        }

        private void xuatExcel(List<Cls79_80.cl_79_80> _listVPBH)
        {
            throw new NotImplementedException();
        }

        private void lupKhoaphong_EditValueChanged(object sender, EventArgs e)
        {
        }

        private List<KPhong> _lKphong = new List<KPhong>();
        private List<DichVu> _ldv = new List<DichVu>();
        private int _hangbv = -1;
        private List<FormDanhMuc.usDichVu.KhoaPhong> _lKPsd = new List<FormDanhMuc.usDichVu.KhoaPhong>();

        private void frm_80ct_Load(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "27001" || DungChung.Bien.MaBV == "27023")
            {
                this.cklKP.Size = new System.Drawing.Size(239, 184);
                grcKP.Visible = true;
                labelControl3.Visible = false;
            }
            ckc_BNCCT.Checked = true;
            ckTLBH.Checked = true;
            ckTLBN.Checked = true;
            rgGuiBHXH.SelectedIndex = 2;
            rdHTThanhToan.SelectedIndex = 2;
            rdCKhoan.SelectedIndex = 2;
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lDSMaICD = _dataContext.ICD10.Select(p => p.MaICD).ToList();
            _ldv = _dataContext.DichVus.ToList();
            if (DungChung.Bien.MaBV == "27023")
            {
                _lKphong = _dataContext.KPhongs
                .Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám" || p.PLoai == DungChung.Bien.st_PhanLoaiKP.CanLamSang)
                .ToList();
            }
            else
            {
                _lKphong = _dataContext.KPhongs
                .Where(p => (p.PLoai == ("Lâm sàng") || p.PLoai == ("Phòng khám")))
                .ToList();
            }
            if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "27001" || DungChung.Bien.MaBV == "27023")
            {
                var kphong = (from kp in _dataContext.KPhongs
                              where (kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám")
                              select new { kp.TenKP, kp.MaKP }).ToList();
                if (kphong.Count > 0)
                {
                    KPhong_24009 themmoi1 = new KPhong_24009();
                    themmoi1.tenkp = "Chọn tất cả";
                    themmoi1.makp = 0;
                    themmoi1.chon = true;
                    _Kphong.Add(themmoi1);
                    foreach (var a in kphong)
                    {

                        KPhong_24009 themmoi = new KPhong_24009();
                        themmoi.tenkp = a.TenKP;
                        themmoi.makp = a.MaKP;
                        themmoi.chon = true;
                        _Kphong.Add(themmoi);
                    }
                    grcKP.DataSource = _Kphong.ToList();
                }
            }
            if (DungChung.Bien.MaBV == "24012")
            {
                btnChonMaICD.Visible = true; lupKhoaphong.Visible = false;
                chkTitleICD.Visible = true;
            }
            else
            {
                simpleButton1.Location = new System.Drawing.Point(479, 476);
                simpleButton1.Size = new System.Drawing.Size(176, 28);
            }
            //chon nhieu kp
            if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "27001" || DungChung.Bien.MaBV == "27023")
            {
                lupKhoaphong.Visible = false;
            }

            //grcKP.DataSource = _dataContext.KPhongs
            //    .Where(p => (p.PLoai == ("Lâm sàng") || p.PLoai == ("Phòng khám")))
            //    .ToList();
            _lKphong.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả", });
            lupKhoaphong.Properties.DataSource = _lKphong;
            //_lKPsd = (from kp in _dataContext.KPhongs.Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám")
            //          select new KhoaPhong()
            //          {
            //              Check = false,
            //              MaKP = kp.MaKP,
            //              TenKP = kp.TenKP
            //          }).Distinct().OrderBy(p => p.TenKP).ToList();
            cklKP.DataSource = _lKPsd;
            lupKhoaphong.EditValue = 0;
            lupNgaytu.EditValue = System.DateTime.Now.Date;
            lupngayden.EditValue = System.DateTime.Now.Date;
            lupNgaytu.Focus();
            List<DTBN> _lDTBN = new List<DTBN>();
            _lDTBN = _dataContext.DTBNs.Where(p => p.Status == 1).ToList();
            if (DungChung.Bien.MaBV == "27183")
            {
                _lDTBN.Add(new DTBN { IDDTBN = 99, DTBN1 = "Tất cả" });
            }
            lupDoituong.Properties.DataSource = _lDTBN;
            lupDoituong.Text = "BHYT";

            List<CanBo> _lcanbo = new List<CanBo>();
            _lcanbo = (from cb in _dataContext.CanBoes
                       join kp in _dataContext.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KeToan) on cb.MaKP equals kp.MaKP
                       select cb).ToList();
            _lcanbo.Insert(0, new CanBo { MaCB = "", TenCB = "Tất cả" });
            lupCanBoTT.Properties.DataSource = _lcanbo;
            lupCanBoTT.EditValue = lupCanBoTT.Properties.GetKeyValueByDisplayText("Tất cả");
            Xuatex_CheckedChanged(sender, e);
            ckXuatXML_CheckedChanged(sender, e);

            #region lấy ra hạng bệnh viện

            var qtuyenBV = (from bv in _dataContext.BenhViens where bv.MaBV == DungChung.Bien.MaBV select new { bv.TuyenBV }).Single();
            string strTuyenBv = "";
            if (qtuyenBV != null && qtuyenBV.TuyenBV != null)
                strTuyenBv = qtuyenBV.TuyenBV.ToString();
            if (strTuyenBv != "")
            {
                switch (strTuyenBv)
                {
                    case "A":
                        _hangbv = 1;
                        break;

                    case "B":
                        _hangbv = 2;
                        break;

                    case "C":
                        _hangbv = 3;
                        break;

                    case "D":
                        _hangbv = 4;
                        break;
                }
            }

            #endregion lấy ra hạng bệnh viện

            radio_DTNT.SelectedIndex = 2;
            radXP_SelectedIndexChanged(sender, e);

            List<MyObject> lMaDtuong = new List<MyObject>();
            lMaDtuong = _dataContext.DTuongs.Where(p => p.Status == 1).Select(p => new MyObject { value = p.MaDTuong, Text = p.MaDTuong }).OrderBy(p => p.Text).ToList();
            //lMaDtuong = data.BenhNhans.Select(p => new MyObject { value = p.MaDTuong == null ? "" : p.MaDTuong.Trim().ToUpper(), Text = p.MaDTuong == null ? "" : p.MaDTuong.Trim().ToUpper() }).Distinct().OrderBy(p => p.Text).ToList();
            lMaDtuong.Insert(0, new MyObject { value = "", Text = "Tất cả" });
            if (lMaDtuong.Count <= 0)
                MessageBox.Show("Danh mục Đối Tượng chưa được thiết lập!");
            cklMaDTuong.DataSource = lMaDtuong;
            cklMaDTuong.CheckAll();
            grvKP.SelectAll();
        }

        private class MyObject
        {
            public string value { set; get; }
            public string Text { set; get; }
        }

        private bool kt()
        {
            if (string.IsNullOrEmpty(lupNgaytu.Text))
            {
                MessageBox.Show("Bạn hãy chọn ngày tháng");
                lupNgaytu.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(lupngayden.Text))
            {
                MessageBox.Show("Bạn hãy chọn ngày tháng");
                lupngayden.Focus();
                return false;
            }
            else if ((lupngayden.DateTime - lupNgaytu.DateTime).Days < 0)
            {
                MessageBox.Show("Ngày đến phải lớn hơn hoặc bằng ngày từ");
                lupngayden.Focus();
                return false;
            }
            return true;
        }

        private string theoquy()
        {
            string quy = "";

            if (ckBC.Checked == true)
            {
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
            }
            else
            {
                quy = "Từ ngày " + lupNgaytu.DateTime.ToString().Substring(0, 10) + " đến ngày " + lupngayden.DateTime.ToString().Substring(0, 10);
            }
            return quy;
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

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private SaveFileDialog dialog = new SaveFileDialog();

        private void btnChonFilePath_Click(object sender, EventArgs e)
        {
            DateTime _ngay = System.DateTime.Now.Date;
            dialog.InitialDirectory = "C:\\";
            dialog.Filter = "Excel files (*.xls or *.xlsx)|*.xls;*.xlsx";
            dialog.FilterIndex = 1;
            dialog.FileName = "Bieu" + (rdBieu.SelectedIndex == 1 ? "80" : "79") + "_" + DungChung.Bien.MaBV + "_" + _ngay.Year + "_" + _ngay.Month + ".xls";
            dialog.RestoreDirectory = true;
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = dialog.FileName;
            }
        }

        private void Xuatex_CheckedChanged(object sender, EventArgs e)
        {
            DateTime _ngay = System.DateTime.Now.Date;
            // txtFilePath.Enabled = Xuatex.Checked;
            btnChonFilePath.Enabled = Xuatex.Checked;
            rdFont.Enabled = Xuatex.Checked;
            ck3360.Enabled = Xuatex.Checked;
            chk_QD917_excell.Enabled = Xuatex.Checked;
            ckXuatTheoBC.Enabled = Xuatex.Checked;
            if (Xuatex.Checked)
                txtFilePath.Text = "C:\\Bieu" + (rdBieu.SelectedIndex == 1 ? "80" : "79") + "_" + DungChung.Bien.MaBV + "_" + _ngay.Year + "_" + _ngay.Month + ".xls";
            else
                txtFilePath.Text = "";
        }

        private void ckTLBH_CheckedChanged(object sender, EventArgs e)
        {
            //if (ckTLBH.Checked)
            //    ckTLBN.Checked = false;
        }

        private void ckTLBN_CheckedChanged(object sender, EventArgs e)
        {
            //if (ckTLBN.Checked)
            //    ckTLBH.Checked = false;
        }

        private void rdTrongBH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdTrongBH.SelectedIndex == 0)
            {
                ckcDVTheoYC.Enabled = true;
            }
            else
            {
                ckcDVTheoYC.Enabled = false;
                ckcDVTheoYC.Checked = false;
            }
        }

        private void lupDoituong_EditValueChanged(object sender, EventArgs e)
        {
            if (lupDoituong.Text == "Dịch vụ")
            {
                rdTrongBH.SelectedIndex = 0;
                rdTrongBH.Properties.ReadOnly = true;
                cklMaDTuong.Enabled = false;
            }
            else
            {
                if (lupDoituong.Text == "BHYT")
                    cklMaDTuong.Enabled = true;
                else
                    cklMaDTuong.Enabled = false;
                rdTrongBH.SelectedIndex = 1;
                rdTrongBH.Properties.ReadOnly = false;
            }
            cklMaDTuong.CheckAll();
        }

        private void rdBieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            Xuatex.Checked = false;
            ckXuatXML.Checked = false;
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (DungChung.Bien.MaBV == "27023")
            {
                _lKphong = _dataContext.KPhongs
                                .Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám" || p.PLoai == DungChung.Bien.st_PhanLoaiKP.CanLamSang)
                                .ToList();
            }
            else
            {
                _lKphong = _dataContext.KPhongs
                    .Where(p => (rdBieu.SelectedIndex == 0 || DungChung.Bien.MaBV == "12122") ? (p.PLoai == ("Lâm sàng") || p.PLoai == ("Phòng khám")) : (((rdBieu.SelectedIndex == 1) && Bien.MaBV == "24012") ? (p.PLoai == ("Lâm sàng") || p.PLoai == ("Phòng khám")) : (p.PLoai == ("Lâm sàng"))))
                    .ToList();
            }

            lupKhoaphong.Properties.DataSource = _lKphong;
            lupKhoaphong.EditValue = 0;
            //chon nhieu kp
            //if (Bien.MaBV == "24012" || Bien.MaBV == "24009")
            //{
            //    grcKP.DataSource = _dataContext.KPhongs.Where(p => (rdBieu.SelectedIndex == 0) ? (p.PLoai == ("Lâm sàng") || p.PLoai == ("Phòng khám")) : (((rdBieu.SelectedIndex == 1) && Bien.MaBV == "24012") ? (p.PLoai == ("Lâm sàng") || p.PLoai == ("Phòng khám")) : (p.PLoai == ("Lâm sàng")))).ToList();
            //}
            if (rdBieu.SelectedIndex == 0)
            {
                radio_DTNT.Properties.ReadOnly = false;
                radio_DTNT.SelectedIndex = 2;
            }
            else if (rdBieu.SelectedIndex == 2)
            {
                radio_DTNT.Properties.ReadOnly = true;
                radio_DTNT.SelectedIndex = 2;
                rad_MauIn.SelectedIndex = 1;
            }
            else
                radio_DTNT.Properties.ReadOnly = true;
        }

        private void ckXuatXML_CheckedChanged(object sender, EventArgs e)
        {
            DateTime _ngay = System.DateTime.Now.Date;
            // txtXMLFilePath.Enabled = ckXuatXML.Checked;
            btnChonFilePath_XML.Enabled = ckXuatXML.Checked;
            if (ckXuatXML.Checked)
                txtXMLFilePath.Text = "C:\\Bieu" + (rdBieu.SelectedIndex == 1 ? "80" : "79") + "_" + DungChung.Bien.MaBV + "_" + _ngay.Year + "_" + _ngay.Month + ".xml";
            else
                txtXMLFilePath.Text = "";
        }

        private void btnChonFilePath_XML_Click(object sender, EventArgs e)
        {
            DateTime _ngay = System.DateTime.Now.Date;
            dialog.InitialDirectory = "C:\\";
            dialog.Filter = "XML files (*.xml)|*.xml";
            dialog.FilterIndex = 1;
            dialog.FileName = "Bieu" + (rdBieu.SelectedIndex == 1 ? "80" : "79") + "_" + DungChung.Bien.MaBV + "_" + _ngay.Year + "_" + _ngay.Month + ".xml";
            dialog.RestoreDirectory = true;
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtXMLFilePath.Text = dialog.FileName;
            }
        }

        private void radTimKiem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radTimKiem.SelectedIndex == 2)
            {
                rad_Duyet.Enabled = false;
                rad_Duyet.SelectedIndex = 1;
            }
            else
            {
                rad_Duyet.Enabled = true;
                rad_Duyet.SelectedIndex = 2;
            }
        }

        private void cklMaDTuong_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                if (cklMaDTuong.GetItemChecked(0) == true)
                    cklMaDTuong.CheckAll();
                else
                    cklMaDTuong.UnCheckAll();
            }
        }

        private void rad_Duyet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rad_Duyet.SelectedIndex == 1)
            {
                radioThuChi.Properties.ReadOnly = false;
                radioThuChi.SelectedIndex = 2;
            }
            else
            {
                radioThuChi.Properties.ReadOnly = true;
                radioThuChi.SelectedIndex = 2;
            }
        }

        private void btnChonMaICD_Click(object sender, EventArgs e)
        {
            ChucNang.frm_DsMaBenhChon frm = new ChucNang.frm_DsMaBenhChon(92);
            frm.passMaICD = new ChucNang.frm_DsMaBenhChon.PassMaICD(PassData);
            frm.ShowDialog();
        }

        private void PassData(List<string> lmaICD)
        {
            _lDSMaICD = lmaICD;
        }
        public class KPhong_24009
        {
            private string TenKP;
            private int MaKP;
            private bool Chon;
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
            public bool chon
            { set { Chon = value; } get { return Chon; } }
        }
        List<KPhong_24009> _Kphong = new List<KPhong_24009>();
        private void grvKP_CellValueChanging_1(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chọn")
            {
                if (grvKP.GetFocusedRowCellValue("tenkp") != null)
                {
                    string ten = grvKP.GetFocusedRowCellValue("tenkp").ToString();

                    if (ten == "Chọn tất cả")
                    {
                        if (_Kphong.First().chon == true)
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = true;
                            }
                        }
                        grcKP.DataSource = "";
                        grcKP.DataSource = _Kphong.ToList();
                    }
                }
            }
        }

        private void grvKP_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void popKP_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            _Kphong = grcKP.DataSource as List<KPhong_24009>;
        }

        private void popKP_BeforePopup(object sender, EventArgs e)
        {
            grcKP.DataSource = _Kphong;
        }



        private void rad_MauIn_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        public class KhoaPhong
        {
            public bool _check;
            public string _maKP;
            public string _kp;

            public string MaKP
            { get { return _maKP; } set { _maKP = value; } }

            public bool Check
            { get { return _check; } set { _check = value; } }

            public string TenKP
            { get { return _kp; } set { _kp = value; } }
        }
        private List<KhoaPhong> _lCSKCB = new List<KhoaPhong>();

        private void radXP_SelectedIndexChanged(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lCSKCB.Clear();
            var kphong = _dataContext.KPhongs.ToList();
            if (radXP.SelectedIndex == 0)
            {
                _lCSKCB.Add(new KhoaPhong { Check = true, MaKP = DungChung.Bien.MaBV, TenKP = DungChung.Bien.TenCQ });
            }
            if (radXP.SelectedIndex == 1)
            {
                _lCSKCB = (from kp in kphong
                           where kp.PLoai == DungChung.Bien.st_PhanLoaiKP.XaPhuong || (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham && kp.TrongBV == 0)
                           select new KhoaPhong()
                           {
                               Check = false,
                               MaKP = kp.MaBVsd,
                               TenKP = kp.TenKP
                           }).Distinct().OrderBy(p => p.TenKP).ToList();
                _lCSKCB.Insert(0, new KhoaPhong { MaKP = "0", TenKP = "Tất cả", });
            }
            if (radXP.SelectedIndex == 2)
            {
                _lCSKCB = (from kp in kphong
                           where kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PKKhuVuc
                           select new KhoaPhong()
                           {
                               Check = false,
                               MaKP = kp.MaBVsd,
                               TenKP = kp.TenKP
                           }).Distinct().OrderBy(p => p.TenKP).ToList();
            }
            if (radXP.SelectedIndex == 3)
            {
                _lCSKCB = (from kp in _dataContext.BenhViens.Where(p => p.Connect)
                           select new KhoaPhong()
                           {
                               Check = false,
                               MaKP = kp.MaBV,
                               TenKP = kp.TenBV
                           }).Distinct().OrderBy(p => p.TenKP).ToList();
                _lCSKCB.Insert(0, new KhoaPhong { MaKP = "0", TenKP = "Tất cả", });
            }
            cklKP.DataSource = null;
            cklKP.DataSource = _lCSKCB;
            cklKP.CheckAll();
        }

        private void chk_QD917_excell_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_QD917_excell.Checked)
            {
                ck3360.Checked = false;
                ckXuatTheoBC.Checked = false;
            }
        }

        private void ck3360_CheckedChanged(object sender, EventArgs e)
        {
            if (ck3360.Checked)
            {
                chk_QD917_excell.Checked = false;
                ckXuatTheoBC.Checked = false;
            }
        }

        private void ckXuatTheoBC_CheckedChanged(object sender, EventArgs e)
        {
            if (ckXuatTheoBC.Checked)
            {
                chk_QD917_excell.Checked = false;
                ck3360.Checked = false;
            }
        }

        private void cklKP_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cklKP_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                for (int i = 1; i < cklKP.ItemCount; i++)
                {
                    cklKP.SetItemCheckState(i, e.State);
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frm_DsDichVuChon frm = new frm_DsDichVuChon(92);
            frm.passMaDV = new frm_DsDichVuChon.PassMaDV(PassData);
            frm.ShowDialog();
        }

        private List<int> _lDSMaDV = new List<int>();

        private void PassData(List<int> lmaDV)
        {
            _lDSMaDV = lmaDV;
        }

        private void rdHTThanhToan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdHTThanhToan.SelectedIndex == 1)
                rdCKhoan.Enabled = true;
            else
                rdCKhoan.Enabled = false;
        }
    }
}