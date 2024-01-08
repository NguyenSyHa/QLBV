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
    public partial class frm_VienPhiHangNgay_30005 : DevExpress.XtraEditors.XtraForm
    {
        public frm_VienPhiHangNgay_30005()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        class _canboTT
        {
            public string MaCB { get; set; }
            public string TenCB { get; set; }
        }

        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_VienPhiHangNgay_30005_Load(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
            {
                if (radBenhVien.SelectedIndex == 0)
                    rdDuyetPhieu.Visible = true;
            }
            else
            {
                rdDuyetPhieu.Visible = false;
            }
            rgXuatHD.SelectedIndex = 2;
            lupNgaytu.DateTime = DateTime.Now.Date;
            lupngayden.DateTime = DateTime.Now.Date.AddDays(1).AddMilliseconds(-1);
            if (DungChung.Bien.MaBV.Equals("30005") || DungChung.Bien.MaBV == "30002")
            {
                gct30005.Visible = true;
                radBenhVien.SelectedIndex = 2;
            }
            else if (DungChung.Bien.MaBV == "27023")
            {
                radBenhVien.SelectedIndex = 1;
            }
            else
            {
                gct30005.Visible = false;
                radBenhVien.SelectedIndex = 0;
            }


            List<CanBo> _lcb = new List<CanBo>();
            List<_canboTT> _listCBTT = new List<_canboTT>();
            _lcb = (from kp in data.KPhongs.Where(p => p.PLoai.Contains("Kế toán"))
                    join cb in data.CanBoes on kp.MaKP equals cb.MaKP
                    select cb).ToList();

            var q100 = (from vp in data.VienPhis
                        join cb in data.CanBoes on vp.MaCB equals cb.MaCB
                        select new _canboTT
                            {
                                MaCB = vp.MaCB,
                                TenCB = cb.TenCB,
                            }).Distinct().ToList();

            _listCBTT.AddRange(q100);

            _lcb.Insert(0, new CanBo { MaCB = "", TenCB = "Tất cả" });
            lupcbthu.Properties.DataSource = _lcb;

            _listCBTT.Insert(0, new _canboTT { MaCB = "", TenCB = "Tất cả" });
            lupCBTT.Properties.DataSource = _listCBTT;

            lupcbthu.EditValue = lupcbthu.Properties.GetKeyValueByDisplayText("Tất cả");

            lupNgaytu.Focus();
            List<DTBN> _lDTBN = new List<DTBN>();
            _lDTBN = data.DTBNs.Where(p => p.Status == 1).ToList();
            _lDTBN.Insert(0, new DTBN { IDDTBN = 100, DTBN1 = "Tất cả" });
            lupDoituong.Properties.DataSource = _lDTBN;
            lupDoituong.EditValue = lupDoituong.Properties.GetKeyValueByDisplayText("Tất cả");

            List<KPhong> _lkp = new List<KPhong>();
            _lkp = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).ToList();
            _lkp.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
            lupKhoaphong.Properties.DataSource = _lkp;
            lupKhoaphong.EditValue = lupKhoaphong.Properties.GetKeyValueByDisplayText("Tất cả");

            if (DungChung.Bien.MaBV == "30005" || DungChung.Bien.MaBV == "30002")
            {
                cklHThi.SetItemChecked(0, true);
                cklNoiNgoaiTru.SetItemChecked(0, true);
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    cklHThi.SetItemChecked(i, true);
                }
                for (int i = 0; i < 3; i++)
                {
                    cklNoiNgoaiTru.SetItemChecked(i, true);
                }
            }
            if (DungChung.Bien.MaBV == "30009")
                rdNgay.SelectedIndex = 1;
            else
                rdNgay.SelectedIndex = 0;

            if (radBenhVien.SelectedIndex == 0)
                ckInTongHop.Visible = true;
            if (DungChung.Bien.MaBV == "30372")
            {

                labelControl11.Visible = true;
                lupCBTT.Visible = true;
            }
            else
            {
                this.Size = new Size(659, 487);
                btnOK.Location = new Point(417, 388);
                btnThoat.Location = new Point(523, 388);
            }

        }

        #region Bệnh viện 30372 - lấy báo cáo viện phí theo cán bộ thanh toán
        public class canbothanhtoan
        {
            public int? MaBNhan { get; set; }
            public string MaCB { get; set; }
            public DateTime? NgayTT { get; set; }
            public int? NgoaiGio { get; set; }
            public int Mien { get; set; }
            public int? MaKP { get; set; }
            public int TrongBH { get; set; }
            public int? MaDV { get; set; }
            public double DonGia { get; set; }
            public double SoLuong { get; set; }
            public double ThanhTien { get; set; }
            public int ThanhToan { get; set; }
            public double TienBH { get; set; }
            public double TienBN { get; set; }
            public double TienNGDM { get; set; }
            public DateTime? NgayDuyet { get; set; }
        }
        #endregion
        List<canbothanhtoan> _listCBTT = new List<canbothanhtoan>();

        private void btnOK_Click(object sender, EventArgs e)
        {
            //đối tượng bệnh nhân
            int dtbn = -1;
            if (lupDoituong.EditValue != null)
                dtbn = Convert.ToInt32(lupDoituong.EditValue);

            //Thời gian
            DateTime tungay = lupNgaytu.DateTime;
            DateTime denngay = lupngayden.DateTime;
            int XuatHD = rgXuatHD.SelectedIndex;
            //Trong ngoài giờ hành chính
            int gioHC = rdTrongGioHC.SelectedIndex;

            //khoa phòng thanh toán
            int makp = 0;
            if (lupKhoaphong.EditValue != null)
                makp = Convert.ToInt32(lupKhoaphong.EditValue);
            string macbthu = "";
            if (lupcbthu.EditValue != null)
                macbthu = Convert.ToString(lupcbthu.EditValue);
            string macbTT = "";
            if (lupCBTT.EditValue != null)
                macbTT = Convert.ToString(lupCBTT.EditValue);
            //Chi phí trong ngoài danh mục
            int cp = rdTrongBH.SelectedIndex;
            //List<int> _lnoingoaitru = new List<int>();
            //for (int i = 0; i < 3; i++)
            //{
            //    if (cklNoiNgoaiTru.GetItemChecked(i) == true)
            //        _lnoingoaitru.Add(i);
            //}

            //List<int> _lHthi = new List<int>();
            //for (int i = 0; i < 3; i++)
            //{
            //    if (cklHThi.GetItemChecked(i) == true)
            //        _lHthi.Add(i);
            //}
            //bool HTSOHOADON = ckcHTHoaDon.Checked;
            //IdNhom và IDtieuNhom
            #region tạm bỏ để lấy tiền KSK (join 2 bảng)
            //var qtn = data.TieuNhomDVs.ToList();
            //var qn = data.NhomDVs.ToList();
            //int idNhomXN = 0, idThuThuat = 0, idCongkham = 0, idtienvc = 0, idKSK = 0; // id nhóm
            //int idSieuam = 0, idDientim = 0, idNoiSoi = 0, idXQ = 0; //id tiểu nhóm
            //idNhomXN = qn.Where(p => p.TenNhomCT == "Xét nghiệm").Select(p => p.IDNhom).FirstOrDefault();
            //idThuThuat = qn.Where(p => p.TenNhomCT == "Thủ thuật, phẫu thuật").Select(p => p.IDNhom).FirstOrDefault();
            //idCongkham = qn.Where(p => p.TenNhomCT == "Khám bệnh").Select(p => p.IDNhom).FirstOrDefault();
            //idtienvc = qn.Where(p => p.TenNhomCT == "Vận chuyển").Select(p => p.IDNhom).FirstOrDefault();
            //idKSK = qn.Where(p => p.TenNhomCT == "Chi phí KSK").Select(p => p.IDNhom).FirstOrDefault();
            //idSieuam = qtn.Where(p => p.TenRG == "Siêu âm").Select(p => p.IdTieuNhom).FirstOrDefault();
            //idDientim = qtn.Where(p => p.TenRG == "Điện tim").Select(p => p.IdTieuNhom).FirstOrDefault();
            //idNoiSoi = qtn.Where(p => p.TenRG == "Nội soi").Select(p => p.IdTieuNhom).FirstOrDefault();
            //idXQ = qtn.Where(p => p.TenRG == "X-Quang").Select(p => p.IdTieuNhom).FirstOrDefault();
            #endregion

            #region thay thế
            var qtn = (from tn in data.TieuNhomDVs
                       join n in data.NhomDVs on tn.IDNhom equals n.IDNhom
                       select new { tn.TenRG, tn.IdTieuNhom, n.IDNhom, n.TenNhomCT }).ToList();
            #region lấy TenRG theo TenNhomCT để in mẫu chi tiết
            var qtn30005 = (from tn in qtn
                            where (rdgNhomDV.SelectedIndex == 4 ? (tn.IDNhom == 1 || tn.IDNhom == 3 || tn.IDNhom == 2 || tn.IDNhom == 13) : (rdgNhomDV.SelectedIndex == 0 ? tn.IDNhom == 1 :
                                  (rdgNhomDV.SelectedIndex == 1 ? tn.IDNhom == 2 : (rdgNhomDV.SelectedIndex == 2 ? tn.IDNhom == 3 : tn.IDNhom == 13)//tn.TenNhomCT.ToLower().Contains("xét nghiệm")tn.TenNhomCT.ToLower().Contains("chẩn đoán hình ảnh")
                                   )))//tn.TenNhomCT.ToLower().Contains("thăm dò chức năng")
                            select new { tn.TenRG, tn.IDNhom }).OrderBy(p => p.IDNhom).ThenBy(p => p.TenRG).Distinct().ToList();
            BaoCao.rep_VienPhiHangNgay_MauCT_30005 rep30005 = new BaoCao.rep_VienPhiHangNgay_MauCT_30005();
            BaoCao.rep_VienPhiHangNgay_MauCT_30005_A3 rep30005A31 = new BaoCao.rep_VienPhiHangNgay_MauCT_30005_A3(1);
            BaoCao.rep_VienPhiHangNgay_MauCT_30005_A3 rep30005A32 = new BaoCao.rep_VienPhiHangNgay_MauCT_30005_A3(2);
            if (qtn30005.Count > 0)
            {
                #region gán TenRG lên từng cột
                //if (qtn30005.Count <= 9)
                //{
                for (int i = 0; i < qtn30005.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            rep30005.TieuNhom1.Value = qtn30005[0].TenRG == null ? "" : qtn30005[0].TenRG;
                            break;
                        case 1:
                            rep30005.TieuNhom2.Value = qtn30005[1].TenRG == null ? "" : qtn30005[1].TenRG;
                            break;
                        case 2:
                            rep30005.TieuNhom3.Value = qtn30005[2].TenRG == null ? "" : qtn30005[2].TenRG;
                            break;
                        case 3:
                            rep30005.TieuNhom4.Value = qtn30005[3].TenRG == null ? "" : qtn30005[3].TenRG;
                            break;
                        case 4:
                            rep30005.TieuNhom5.Value = qtn30005[4].TenRG == null ? "" : qtn30005[4].TenRG;
                            break;
                        case 5:
                            rep30005.TieuNhom6.Value = qtn30005[5].TenRG == null ? "" : qtn30005[5].TenRG;
                            break;
                        case 6:
                            rep30005.TieuNhom7.Value = qtn30005[6].TenRG == null ? "" : qtn30005[6].TenRG;
                            break;
                        case 7:
                            rep30005.TieuNhom8.Value = qtn30005[7].TenRG == null ? "" : qtn30005[7].TenRG;
                            break;
                        case 8:
                            rep30005.TieuNhom9.Value = qtn30005[8].TenRG == null ? "" : qtn30005[8].TenRG;
                            break;
                    }
                }
                //}
                //else
                //{

                for (int i = 0; i < qtn30005.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            rep30005A31.TieuNhom1.Value = qtn30005[0].TenRG == null ? "" : qtn30005[0].TenRG;
                            break;
                        case 1:
                            rep30005A31.TieuNhom2.Value = qtn30005[1].TenRG == null ? "" : qtn30005[1].TenRG;
                            break;
                        case 2:
                            rep30005A31.TieuNhom3.Value = qtn30005[2].TenRG == null ? "" : qtn30005[2].TenRG;
                            break;
                        case 3:
                            rep30005A31.TieuNhom4.Value = qtn30005[3].TenRG == null ? "" : qtn30005[3].TenRG;
                            break;
                        case 4:
                            rep30005A31.TieuNhom5.Value = qtn30005[4].TenRG == null ? "" : qtn30005[4].TenRG;
                            break;
                        case 5:
                            rep30005A31.TieuNhom6.Value = qtn30005[5].TenRG == null ? "" : qtn30005[5].TenRG;
                            break;
                        case 6:
                            rep30005A31.TieuNhom7.Value = qtn30005[6].TenRG == null ? "" : qtn30005[6].TenRG;
                            break;
                        case 7:
                            rep30005A31.TieuNhom8.Value = qtn30005[7].TenRG == null ? "" : qtn30005[7].TenRG;
                            break;
                        case 8:
                            rep30005A31.TieuNhom9.Value = qtn30005[8].TenRG == null ? "" : qtn30005[8].TenRG;
                            break;
                        case 9:
                            rep30005A31.TieuNhom10.Value = qtn30005[9].TenRG == null ? "" : qtn30005[9].TenRG;
                            break;
                        case 10:
                            rep30005A31.TieuNhom11.Value = qtn30005[10].TenRG == null ? "" : qtn30005[10].TenRG;
                            break;
                        case 11:
                            rep30005A31.TieuNhom12.Value = qtn30005[11].TenRG == null ? "" : qtn30005[11].TenRG;
                            break;
                        case 12:
                            rep30005A31.TieuNhom13.Value = qtn30005[12].TenRG == null ? "" : qtn30005[12].TenRG;
                            break;
                        case 13:
                            rep30005A31.TieuNhom14.Value = qtn30005[13].TenRG == null ? "" : qtn30005[13].TenRG;
                            break;
                        case 14:
                            rep30005A31.TieuNhom15.Value = qtn30005[14].TenRG == null ? "" : qtn30005[14].TenRG;
                            break;
                        case 15:
                            rep30005A31.TieuNhom16.Value = qtn30005[15].TenRG == null ? "" : qtn30005[15].TenRG;
                            break;
                    }
                }
                if (qtn30005.Count > 16)
                {

                    for (int i = 0; i < qtn30005.Count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                rep30005A32.TieuNhom1.Value = qtn30005[16].TenRG == null ? "" : qtn30005[16].TenRG;
                                break;
                            case 1:
                                rep30005A32.TieuNhom2.Value = qtn30005[17].TenRG == null ? "" : qtn30005[17].TenRG;
                                break;
                            case 2:
                                rep30005A32.TieuNhom3.Value = qtn30005[18].TenRG == null ? "" : qtn30005[18].TenRG;
                                break;
                            case 3:
                                rep30005A32.TieuNhom4.Value = qtn30005[19].TenRG == null ? "" : qtn30005[19].TenRG;
                                break;
                            case 4:
                                rep30005A32.TieuNhom5.Value = qtn30005[20].TenRG == null ? "" : qtn30005[20].TenRG;
                                break;
                            case 5:
                                rep30005A32.TieuNhom6.Value = qtn30005[21].TenRG == null ? "" : qtn30005[21].TenRG;
                                break;
                            case 6:
                                rep30005A32.TieuNhom7.Value = qtn30005[22].TenRG == null ? "" : qtn30005[22].TenRG;
                                break;
                            case 7:
                                rep30005A32.TieuNhom8.Value = qtn30005[23].TenRG == null ? "" : qtn30005[23].TenRG;
                                break;
                            case 8:
                                rep30005A32.TieuNhom9.Value = qtn30005[24].TenRG == null ? "" : qtn30005[24].TenRG;
                                break;
                            case 9:
                                rep30005A32.TieuNhom10.Value = qtn30005[25].TenRG == null ? "" : qtn30005[25].TenRG;
                                break;
                            case 10:
                                rep30005A32.TieuNhom11.Value = qtn30005[26].TenRG == null ? "" : qtn30005[26].TenRG;
                                break;
                            case 11:
                                rep30005A32.TieuNhom12.Value = qtn30005[27].TenRG == null ? "" : qtn30005[27].TenRG;
                                break;
                            case 12:
                                rep30005A32.TieuNhom13.Value = qtn30005[28].TenRG == null ? "" : qtn30005[28].TenRG;
                                break;
                            case 13:
                                rep30005A32.TieuNhom14.Value = qtn30005[29].TenRG == null ? "" : qtn30005[29].TenRG;
                                break;
                            case 14:
                                rep30005A32.TieuNhom15.Value = qtn30005[30].TenRG == null ? "" : qtn30005[30].TenRG;
                                break;
                            case 15:
                                rep30005A32.TieuNhom16.Value = qtn30005[31].TenRG == null ? "" : qtn30005[31].TenRG;
                                break;
                        }
                    }
                    //}
                    //}
                    //else if (qtn30005.Count <= 32)
                    //{

                    //}
                    //else
                    //{
                    //    MessageBox.Show("Quá nhiều tiểu nhóm");

                    //}

                }
                #endregion
            }
            #endregion
            #region biến
            bool _noitru = cklNoiNgoaiTru.GetItemChecked(2);
            bool _DTNT = cklNoiNgoaiTru.GetItemChecked(1);
            bool _ngoaitru = cklNoiNgoaiTru.GetItemChecked(0);
            //%bảo hiểm
            bool noitru = cklHThi.GetItemChecked(2);
            bool DTNT = cklHThi.GetItemChecked(1);
            bool ngoaitru = cklHThi.GetItemChecked(0);



            List<int> idNhomXN = new List<int>(); List<int> idThuThuat = new List<int>(); List<int> idtienvc = new List<int>(); List<int> idNhomTDCN = new List<int>(); List<int> idNhomCDHA = new List<int>();
            List<int> idSieuam = new List<int>(), idSieuamDL = new List<int>(), idDientim = new List<int>(), idNoiSoi = new List<int>(), idXQ = new List<int>(), idCongkham = new List<int>(), idKSK = new List<int>(), idChupCT = new List<int>(), idDoCNHH = new List<int>(); //id tiểu nhóm
            List<int> idDoLoangXuong = new List<int>(), idLuuHuyetNao = new List<int>(), idThuoc = new List<int>(), idVTYT = new List<int>(); //id tiểu nhóm
            List<int> idTienGiuong = new List<int>(), idDienNaoDo = new List<int>();

            idNhomXN = qtn.Where(p => p.TenNhomCT == "Xét nghiệm").Select(p => p.IdTieuNhom).ToList();
            idNhomTDCN = qtn.Where(p => p.IDNhom == 3).Where(p => p.TenRG != DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim && (radBenhVien.SelectedIndex == 2 || radBenhVien.SelectedIndex == 5 || p.TenRG != DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKDienNaoDo)).Select(p => p.IdTieuNhom).ToList();
            idNhomCDHA = qtn.Where(p => p.IDNhom == 2).Select(p => p.IdTieuNhom).ToList();
            idThuThuat = qtn.Where(p => p.TenNhomCT == "Thủ thuật, phẫu thuật").Select(p => p.IdTieuNhom).ToList();
            idtienvc = qtn.Where(p => p.TenNhomCT == "Vận chuyển").Select(p => p.IdTieuNhom).ToList();
            // idCongkham = qtn.Where(p => p.TenNhomCT == "Khám bệnh").Where(p => p.TenRG != "KSK").Select(p => p.IdTieuNhom).ToList();
            idCongkham = qtn.Where(p => p.TenNhomCT == "Khám bệnh").Select(p => p.IdTieuNhom).ToList();
            //  idKSK = qtn.Where(p => p.TenRG == "KSK").Select(p => p.IdTieuNhom).ToList();
            idSieuam = qtn.Where(p => p.TenRG == "Siêu âm").Select(p => p.IdTieuNhom).ToList();
            idSieuamDL = qtn.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Select(p => p.IdTieuNhom).ToList();
            idDientim = qtn.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Select(p => p.IdTieuNhom).ToList();
            idDienNaoDo = qtn.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKDienNaoDo).Select(p => p.IdTieuNhom).ToList();
            idNoiSoi = qtn.Where(p => p.TenRG.Contains("Nội soi")).Select(p => p.IdTieuNhom).ToList();
            idChupCT = qtn.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Select(p => p.IdTieuNhom).ToList();
            idDoCNHH = qtn.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Select(p => p.IdTieuNhom).ToList();
            idXQ = qtn.Where(p => p.TenRG == "X-Quang").Select(p => p.IdTieuNhom).ToList();
            idDoLoangXuong = qtn.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Select(p => p.IdTieuNhom).ToList();
            idLuuHuyetNao = qtn.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.LuuHuyetNao).Select(p => p.IdTieuNhom).ToList();
            idThuoc = qtn.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6).Select(p => p.IdTieuNhom).ToList();
            idVTYT = qtn.Where(p => p.IDNhom == 10 || p.IDNhom == 11).Select(p => p.IdTieuNhom).ToList();
            idTienGiuong = qtn.Where(p => p.IDNhom == 14 || p.IDNhom == 15).Select(p => p.IdTieuNhom).ToList();
            #endregion
            #endregion
            List<VPHangNgay> all = new List<VPHangNgay>();
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            var qdtbn = data.DTBNs.Where(p => dtbn == 100 || p.IDDTBN == dtbn).ToList();
            var qdv = (from dv in data.DichVus
                       join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                       join n in data.NhomDVs on tn.IDNhom equals n.IDNhom
                       select new
                       {
                           dv.MaDV,
                           tn.IdTieuNhom,
                           tn.TenRG,//tên tiểu nhóm
                           n.IDNhom,
                           n.TenNhomCT,
                           dv.PLoai
                       }).ToList();

            #region Mẫu 27183 - so sánh
            if (radBenhVien.SelectedIndex == 6)
            {
                var q0 = (from bn in data.BenhNhans.Where(p => p.TuyenDuoi == 0).Where(p => dtbn == 100 || p.IDDTBN == dtbn).Where(p => (_ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (_DTNT && p.NoiTru == 0 && p.DTNT == true) || (_noitru && p.NoiTru == 1))
                          join rv in data.RaViens
                          on bn.MaBNhan equals rv.MaBNhan
                          select new { bn.DTuong, bn.MaBNhan, bn.TenBNhan, bn.Tuoi, bn.DChi, bn.NoiTru, bn.DTNT, bn.IDDTBN }).ToList();
                var q1 = (from bn in q0
                          join dt in qdtbn on bn.IDDTBN equals dt.IDDTBN
                          select new { bn.DTuong, bn.MaBNhan, bn.TenBNhan, bn.Tuoi, bn.DChi, bn.NoiTru, dt.DTBN1, bn.DTNT }).ToList();
                var q2 = (from vp in data.VienPhis.Where(p => rdNgay.SelectedIndex == 1 ? (p.NgayDuyet >= tungay && p.NgayDuyet <= denngay) : (p.NgayTT >= tungay && p.NgayTT <= denngay)).Where(p => macbthu == "" ? true : p.MaCB == macbthu).Where(p => gioHC == 2 || (gioHC == 1 && p.NgoaiGio == 1) || (gioHC == 0 && (p.NgoaiGio == null || p.NgoaiGio != 1)))
                          join vpct in data.VienPhicts.Where(p => cp == 2 ? true : p.TrongBH == cp).Where(p => makp == 0 || p.MaKP == makp)
                          on vp.idVPhi equals vpct.idVPhi
                          select new
                          {
                              vp.MaBNhan,
                              vp.NgayTT,
                              vp.NgoaiGio,
                              vpct.Mien,
                              vpct.MaKP,
                              vpct.TrongBH,
                              vpct.MaDV,
                              vpct.DonGia,
                              vpct.SoLuong,
                              vpct.ThanhTien,
                              vpct.ThanhToan,
                              vpct.TienBH,
                              TienBN = vpct.TienBN,
                              vp.NgayDuyet
                          }).ToList();

                var q3 = (from bn in q1
                          join vp in q2 on bn.MaBNhan equals vp.MaBNhan
                          join dv in qdv on vp.MaDV equals dv.MaDV
                          select new
                          {
                              bn.MaBNhan,
                              bn.TenBNhan,
                              bn.Tuoi,
                              bn.DChi,
                              bn.NoiTru,
                              bn.DTNT,
                              bn.DTuong,
                              DTBN1 = bn.DTBN1.ToUpper().Trim(),
                              vp.TrongBH,
                              vp.MaDV,
                              vp.DonGia,
                              vp.SoLuong,
                              vp.ThanhTien,
                              vp.ThanhToan,
                              vp.TienBH,
                              TienBN = vp.TienBN,
                              dv.IdTieuNhom,
                              dv.TenRG,
                              dv.IDNhom,
                              dv.TenNhomCT,
                              NgayTT = (rdNgay.SelectedIndex == 1) ? vp.NgayDuyet.Value.Date : vp.NgayTT.Value.Date
                          }).ToList();

                var q4 = (from bn in q3
                          group bn by new { bn.TenBNhan, bn.Tuoi, bn.DChi, bn.MaBNhan, bn.NoiTru, bn.DTNT, bn.NgayTT, bn.DTBN1 } into kq
                          select new
                          {
                              kq.Key.TenBNhan,
                              kq.Key.Tuoi,
                              kq.Key.DChi,
                              kq.Key.MaBNhan,
                              kq.Key.NgayTT,
                              kq.Key.DTBN1,
                              XN = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idNhomXN.Contains(p.IdTieuNhom)).Sum(p => p.DTuong == "KSK" ? p.ThanhTien : p.TienBN),
                              TDCN = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idNhomTDCN.Contains(p.IdTieuNhom)).Where(p => !idDientim.Contains(p.IdTieuNhom)).Sum(p => p.DTuong == "KSK" ? p.ThanhTien : p.TienBN),
                              SieuAm = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idSieuam.Contains(p.IdTieuNhom)).Sum(p => p.DTuong == "KSK" ? p.ThanhTien : p.TienBN),
                              SieuAmDL = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idSieuamDL.Contains(p.IdTieuNhom)).Sum(p => p.DTuong == "KSK" ? p.ThanhTien : p.TienBN),
                              DienTim = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idDientim.Contains(p.IdTieuNhom)).Sum(p => p.DTuong == "KSK" ? p.ThanhTien : p.TienBN),
                              ChupCT = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idChupCT.Contains(p.IdTieuNhom)).Sum(p => p.DTuong == "KSK" ? p.ThanhTien : p.TienBN),
                              DoCNHH = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idDoCNHH.Contains(p.IdTieuNhom)).Sum(p => p.DTuong == "KSK" ? p.ThanhTien : p.TienBN),
                              ThuThuatPT = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idThuThuat.Contains(p.IdTieuNhom)).Where(p => !idNoiSoi.Contains(p.IdTieuNhom)).Sum(p => p.DTuong == "KSK" ? p.ThanhTien : p.TienBN),
                              NoiSoi = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idNoiSoi.Contains(p.IdTieuNhom)).Sum(p => p.DTuong == "KSK" ? p.ThanhTien : p.TienBN),
                              Congkham = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idCongkham.Contains(p.IdTieuNhom)).Sum(p => p.ThanhTien),
                              TienVC = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idtienvc.Contains(p.IdTieuNhom)).Sum(p => p.DTuong == "KSK" ? p.ThanhTien : p.TienBN),
                              XQ = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idXQ.Contains(p.IdTieuNhom)).Sum(p => p.DTuong == "KSK" ? p.ThanhTien : p.TienBN),
                              DoLoangXuong = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idDoLoangXuong.Contains(p.IdTieuNhom)).Sum(p => p.DTuong == "KSK" ? p.ThanhTien : p.TienBN),
                              LuuHuyetNao = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idLuuHuyetNao.Contains(p.IdTieuNhom)).Sum(p => p.DTuong == "KSK" ? p.ThanhTien : p.TienBN),
                              DienNaoDo = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idDienNaoDo.Contains(p.IdTieuNhom)).Sum(p => p.DTuong == "KSK" ? p.ThanhTien : p.TienBN),
                              TienThuoc = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7).Sum(p => p.DTuong == "KSK" ? p.ThanhTien : p.TienBN),
                              TienVTYT = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => p.IDNhom == 10 || p.IDNhom == 11).Sum(p => p.DTuong == "KSK" ? p.ThanhTien : p.TienBN),
                              TienBN = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Sum(p => p.DTuong == "KSK" ? p.ThanhTien : p.TienBN),
                              TienBH = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Sum(p => p.DTuong == "KSK" ? 0 : p.TienBH),
                              TienKTT = kq.Where(p => p.TrongBH == 2 && ((ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1))).Sum(p => p.ThanhTien),
                              ThanhTien = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Sum(p => p.ThanhTien),


                          }).ToList();

                List<VPHangNgay> q5 = (from bn in q4
                                       group bn by new { bn.TenBNhan, bn.Tuoi, bn.DChi, bn.MaBNhan, bn.NgayTT, bn.DTBN1 } into kq
                                       select new VPHangNgay
                                       {
                                           TenBNhan = kq.Key.TenBNhan,
                                           Tuoi = kq.Key.Tuoi ?? 0,
                                           DChi = kq.Key.DChi,
                                           DTuong = kq.Key.DTBN1,
                                           MaBNhan = kq.Key.MaBNhan,
                                           NgayTT = kq.Key.NgayTT,
                                           TienThuoc = kq.Sum(p => p.TienThuoc),
                                           TienVTYT = kq.Sum(p => p.TienVTYT),
                                           XN = kq.Sum(p => p.XN),
                                           TDCN = kq.Sum(p => p.TDCN),
                                           SieuAm = kq.Sum(p => p.SieuAm),
                                           SieuAmDL = kq.Sum(p => p.SieuAmDL),
                                           ChupCT = kq.Sum(p => p.ChupCT),
                                           DoCNHH = kq.Sum(p => p.DoCNHH),
                                           DienTim = kq.Sum(p => p.DienTim),
                                           ThuThuatPT = kq.Sum(p => p.ThuThuatPT),
                                           NoiSoi = kq.Sum(p => p.NoiSoi),
                                           DienNaoDo = kq.Sum(p => p.DienNaoDo),
                                           Congkham = (kq.Key.DTBN1 == "KSK") ? 0 : kq.Sum(p => p.Congkham),
                                           KSK = (kq.Key.DTBN1 == "KSK") ? kq.Sum(p => p.Congkham) : 0,
                                           TienVC = kq.Sum(p => p.TienVC),
                                           XQ = kq.Sum(p => p.XQ),
                                           DoLoangXuong = kq.Sum(p => p.DoLoangXuong),
                                           LuuHuyetNao = kq.Sum(p => p.LuuHuyetNao),
                                           TienBN = kq.Sum(p => p.TienBN),
                                           TienBH = kq.Sum(p => p.TienBH),
                                           TienKTT = kq.Sum(p => p.TienKTT),
                                           ThanhTien = kq.Sum(p => p.ThanhTien),
                                       }).OrderBy(p => p.MaBNhan).ToList();//.Where(p=>p.Tong != 0).ToList();
                string[] _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                string[] _tieude = { "stt", "Họ tên", "Tuổi", "Địa chỉ", "Số phiếu", "Nhóm xét nghiệm", "Siêu âm", "Điện tim", "Thủ thuật - Phẫu thuật", "Nội soi", "KSK", "Công khám", "Tiền vận chuyển", "Chụp XQ", "Nhóm thăm dò chức năng", "Đo loãng xương", "Lưu huyết não", "Điện não đồ", "Tiền thuốc", "Tiền VTYT", "Tiền BN", "Tiền BH", "Tiền VTYT không TT", "Cộng" };
                string _filePath = "D:\\BCVienPhiHangNgay_SoSanh.xls";
                int[] _arrWidth = new int[] { };
                DungChung.Bien.MangHaiChieu = new Object[q5.Count + 1, _arr.Length];
                for (int i = 0; i < _arr.Length; i++)
                {
                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                }
                int num = 1;
                foreach (var r in q5)
                {
                    DungChung.Bien.MangHaiChieu[num, 0] = num;
                    DungChung.Bien.MangHaiChieu[num, 1] = r.TenBNhan;
                    DungChung.Bien.MangHaiChieu[num, 2] = r.Tuoi;
                    DungChung.Bien.MangHaiChieu[num, 3] = r.DChi;
                    DungChung.Bien.MangHaiChieu[num, 4] = r.MaBNhan;
                    DungChung.Bien.MangHaiChieu[num, 5] = r.XN;
                    DungChung.Bien.MangHaiChieu[num, 6] = r.SieuAm;
                    DungChung.Bien.MangHaiChieu[num, 7] = r.DienTim;
                    DungChung.Bien.MangHaiChieu[num, 8] = r.ThuThuatPT;
                    DungChung.Bien.MangHaiChieu[num, 9] = r.NoiSoi;
                    DungChung.Bien.MangHaiChieu[num, 10] = r.KSK;
                    DungChung.Bien.MangHaiChieu[num, 11] = r.Congkham;
                    DungChung.Bien.MangHaiChieu[num, 12] = r.TienVC;
                    DungChung.Bien.MangHaiChieu[num, 13] = r.XQ;
                    DungChung.Bien.MangHaiChieu[num, 14] = r.TDCN;
                    DungChung.Bien.MangHaiChieu[num, 15] = r.DoLoangXuong;
                    DungChung.Bien.MangHaiChieu[num, 16] = r.LuuHuyetNao;
                    DungChung.Bien.MangHaiChieu[num, 17] = r.DienNaoDo;
                    DungChung.Bien.MangHaiChieu[num, 18] = r.TienThuoc;
                    DungChung.Bien.MangHaiChieu[num, 19] = r.TienVTYT;
                    DungChung.Bien.MangHaiChieu[num, 20] = r.TienBN;
                    DungChung.Bien.MangHaiChieu[num, 21] = r.TienBH;
                    DungChung.Bien.MangHaiChieu[num, 22] = r.TienKTT;
                    DungChung.Bien.MangHaiChieu[num, 23] = r.ThanhTien;
                    num++;
                }
                BaoCao.rep_VienPhiHangNgay_27183_SS rep = new BaoCao.rep_VienPhiHangNgay_27183_SS();
                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Sheet1", _filePath, true, this.Name);

                rep.DataSource = q5;
                rep.colCQ.Text = DungChung.Bien.TenCQ.ToUpper();
                rep.cel_diadanh.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                rep.celNgayThang.Text = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy");
                if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                    rep.celNgayThang.Text = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy HH:mm") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy HH:mm");
                if (String.IsNullOrEmpty(txtTieude.Text))
                {
                    rep.CelTieuDe.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN VIỆN PHÍ HẰNG NGÀY";
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
            #endregion
            #region bv khác
            else
            {
                var q0 = (from bn in data.BenhNhans.Where(p => p.TuyenDuoi == 0).Where(p => dtbn == 100 || p.IDDTBN == dtbn).Where(p => (_ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (_DTNT && p.NoiTru == 0 && p.DTNT == true) || (_noitru && p.NoiTru == 1))//.Where(p => noitru == 2 || p.NoiTru == noitru)
                          //join dt in data.DTBNs on bn.IDDTBN equals dt.IDDTBN
                          join rv in data.RaViens
                          on bn.MaBNhan equals rv.MaBNhan into kq
                          from kq1 in kq.DefaultIfEmpty()
                          select new { bn.DTuong, bn.MaBNhan, bn.TenBNhan, bn.Tuoi, bn.DChi, bn.NoiTru, kq1, bn.DTNT, bn.IDDTBN }).ToList();
                var q1 = (from bn in q0
                          join dt in qdtbn on bn.IDDTBN equals dt.IDDTBN
                          select new { bn.DTuong, bn.MaBNhan, bn.TenBNhan, bn.Tuoi, bn.DChi, bn.NoiTru, dt.DTBN1, bn.kq1, bn.DTNT }).ToList();
                #region radBenhVien==5
                if (radBenhVien.SelectedIndex == 5)
                {
                    List<VPHangNgay> q6 = new List<VPHangNgay>();
                    //chi phí thu khi thanh toán//.Where(p => XuatHD == 2 ? true : (XuatHD == 0 ? (p.FkeyVNPT == null && p.MaHD == null) : (p.FkeyVNPT != null || p.MaHD != null)))
                    var q2 = (from vp in data.VienPhis.Where(p => rdNgay.SelectedIndex == 1 ? (p.NgayDuyet >= tungay && p.NgayDuyet <= denngay) : (p.NgayTT >= tungay && p.NgayTT <= denngay)).Where(p => gioHC == 2 || (gioHC == 1 && p.NgoaiGio == 1) || (gioHC == 0 && (p.NgoaiGio == null || p.NgoaiGio != 1)))
                              join vpct in data.VienPhicts.Where(p => cp == 2 ? true : p.TrongBH == cp).Where(p => p.ThanhToan == 0).Where(p => makp == 0 || p.MaKP == makp) on vp.idVPhi equals vpct.idVPhi
                              join tu in data.TamUngs.Where(p => XuatHD == 2 ? true : (XuatHD == 0 ? (p.FkeyVNPT == null && p.MaHD == null) : (p.FkeyVNPT != null || p.MaHD != null))).Where(p => p.PhanLoai == 1 && (macbthu == "" ? true : p.MaCB == macbthu)) on vp.MaBNhan equals tu.MaBNhan
                              select new
                              {
                                  tu.IDTamUng,
                                  tu.SoHD,
                                  vp.MaBNhan,
                                  vp.NgayTT,
                                  vp.NgoaiGio,
                                  vpct.Mien,
                                  vpct.MaKP,
                                  vpct.TrongBH,
                                  vpct.MaDV,
                                  vpct.DonGia,
                                  vpct.SoLuong,
                                  vpct.ThanhTien,
                                  vpct.ThanhToan,
                                  vpct.TienBH,
                                  TienBN = vpct.TienBN,
                                  TienNGDM = vpct.TrongBH == 1 ? 0 : vpct.TienBN,
                                  vp.NgayDuyet
                              }).ToList();
                    var q3 = (from bn in q1.Where(p => p.kq1 != null)
                              join vp in q2 on bn.MaBNhan equals vp.MaBNhan
                              join dv in qdv on vp.MaDV equals dv.MaDV
                              select new
                              {
                                  bn.MaBNhan,
                                  bn.TenBNhan,
                                  bn.Tuoi,
                                  bn.DChi,
                                  bn.NoiTru,
                                  bn.DTNT,
                                  vp.IDTamUng,
                                  vp.SoHD,
                                  DTBN1 = bn.DTBN1.ToUpper().Trim(),
                                  vp.TrongBH,
                                  vp.MaDV,
                                  vp.DonGia,
                                  vp.SoLuong,
                                  vp.ThanhTien,
                                  vp.ThanhToan,
                                  vp.TienBH,
                                  dv.IdTieuNhom,
                                  vp.TienBN,
                                  dv.TenRG,
                                  dv.IDNhom,
                                  dv.TenNhomCT,
                                  NgayTT = (rdNgay.SelectedIndex == 1) ? vp.NgayDuyet.Value.Date : vp.NgayTT.Value.Date
                              }).ToList();
                    var q4 = (from bn in q3
                              group bn by new { bn.TenBNhan, bn.Tuoi, bn.DChi, bn.MaBNhan, bn.NoiTru, bn.DTNT, bn.NgayTT, bn.DTBN1, bn.IDTamUng, bn.SoHD } into kq
                              select new
                              {
                                  kq.Key.TenBNhan,
                                  kq.Key.Tuoi,
                                  kq.Key.DChi,
                                  kq.Key.MaBNhan,
                                  kq.Key.NgayTT,
                                  kq.Key.DTBN1,
                                  kq.Key.IDTamUng,
                                  kq.Key.SoHD,
                                  #region bv 27183
                                  Giuong = (kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idTienGiuong.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)),
                                  XN = (kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idNhomXN.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)),
                                  TDCN = (kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idNhomTDCN.Contains(p.IdTieuNhom)).Where(p => !idDientim.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)),
                                  SieuAm = (kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idSieuam.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)),
                                  SieuAmDL = (kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idSieuamDL.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)),
                                  DienTim = (kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idDientim.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)),
                                  ChupCT = (kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idChupCT.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)),
                                  DoCNHH = (kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idDoCNHH.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)),
                                  ThuThuatPT = (kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idThuThuat.Contains(p.IdTieuNhom)).Where(p => !idNoiSoi.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)),
                                  NoiSoi = (kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idNoiSoi.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)),
                                  Congkham = (kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idCongkham.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)),
                                  TienVC = (kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idtienvc.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)),
                                  XQ = (kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idXQ.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)),
                                  DoLoangXuong = (kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idDoLoangXuong.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)),
                                  LuuHuyetNao = (kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idLuuHuyetNao.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)),

                                  #endregion
                                  TienNGDM = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => p.TrongBH == 0 || p.TrongBH == 3).Sum(p => p.TienBN),
                                  TienBN = 0, // chỉ áp dụng đối với đối tượng dịch vụ _30002
                                  NgoaiTruBH = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => p.DTBN1 == "BHYT").Where(p => p.TrongBH == 1).Sum(p => p.TienBN),// -( DungChung.Bien.MaBV == "30005" ? (((ngoaitru && kq.Key.NoiTru == 0 &&kq.Key.DTNT == false) || (DTNT && kq.Key.NoiTru == 0 && kq.Key.DTNT == true) || (noitru && kq.Key.NoiTru == 1)) ? qtamungBNDV.Where(p => p.MaBNhan == kq.Key.MaBNhan).Sum(p => p.SoTien ?? 0) : 0) : 0), // phải trừ tiền đã tạm ứng
                                  TienThuoc = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7).Sum(p => p.TienBN),
                                  TienVTYT = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => p.IDNhom == 10 || p.IDNhom == 11).Sum(p => p.TienBN),
                                  TamUng = 0
                              }).ToList();

                    List<VPHangNgay> q5 = (from bn in q4
                                           group bn by new { bn.TenBNhan, bn.Tuoi, bn.DChi, bn.MaBNhan, bn.NgayTT, bn.DTBN1, bn.IDTamUng, bn.SoHD } into kq
                                           select new VPHangNgay
                                           {
                                               TenBNhan = kq.Key.TenBNhan,
                                               Tuoi = kq.Key.Tuoi ?? 0,
                                               DChi = kq.Key.DChi,
                                               DTuong = kq.Key.DTBN1,
                                               MaBNhan = kq.Key.MaBNhan,
                                               NgayTT = kq.Key.NgayTT,
                                               IDTamUng = kq.Key.IDTamUng,
                                               SoHD = kq.Key.SoHD,
                                               NgoaiTruBH = kq.Sum(p => p.NgoaiTruBH),// thu thêm bệnh nhân mới tính (bvì là bc thu vp)
                                               TienThuoc = kq.Sum(p => p.TienThuoc),
                                               TienVTYT = kq.Sum(p => p.TienVTYT),
                                               TienBN = kq.Sum(p => p.TienBN),
                                               TienNGDM = kq.Sum(p => p.TienNGDM),
                                               Giuong = kq.Sum(p => p.Giuong),
                                               XN = kq.Sum(p => p.XN),
                                               TDCN = kq.Sum(p => p.TDCN),
                                               SieuAm = kq.Sum(p => p.SieuAm),
                                               SieuAmDL = kq.Sum(p => p.SieuAmDL),
                                               ChupCT = kq.Sum(p => p.ChupCT),
                                               DoCNHH = kq.Sum(p => p.DoCNHH),
                                               DienTim = kq.Sum(p => p.DienTim),
                                               ThuThuatPT = kq.Sum(p => p.ThuThuatPT),
                                               NoiSoi = kq.Sum(p => p.NoiSoi),
                                               Congkham = kq.Key.DTBN1 == "KSK" ? 0 : kq.Sum(p => p.Congkham),
                                               KSK = kq.Key.DTBN1 == "KSK" ? kq.Sum(p => p.Congkham) : 0,
                                               TienVC = kq.Sum(p => p.TienVC),
                                               XQ = kq.Sum(p => p.XQ),
                                               DoLoangXuong = kq.Sum(p => p.DoLoangXuong),
                                               LuuHuyetNao = kq.Sum(p => p.LuuHuyetNao),
                                               Tong = kq.Sum(p => p.TienNGDM) + kq.Sum(p => p.NgoaiTruBH)
                                           }).OrderBy(p => p.NgayTT).ToList();
                    q6.AddRange(q5);
                    //List<int> _lMabn = q5.Select(p => p.MaBNhan).ToList();//.Where(p => _lMabn.Contains(p.MaBNhan))
                    //Chi phí thu trực tiếp
                    //join dt in qdtbn on bn.IDDTBN equals dt.IDDTBN
                    var qTamung0 = (from bn in data.BenhNhans.Where(p => p.TuyenDuoi == 0).Where(p => dtbn == 100 || p.IDDTBN == dtbn).Where(p => (_ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (_DTNT && p.NoiTru == 0 && p.DTNT == true) || (_noitru && p.NoiTru == 1))
                                    join tu in data.TamUngs.Where(p => XuatHD == 2 ? true : (XuatHD == 0 ? (p.FkeyVNPT == null && p.MaHD == null) : (p.FkeyVNPT != null || p.MaHD != null))).Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay).Where(p => p.IDGoiDV <= 0 || p.IDGoiDV == null).Where(p => p.PhanLoai == 3).Where(p => macbthu == "" || p.MaCB == macbthu).Where(p => makp == 0 || p.MaKP == makp).Where(p => gioHC == 2 || (gioHC == 1 && p.NgoaiGio == 1) || (gioHC == 0 && (p.NgoaiGio == null || p.NgoaiGio != 1))) on bn.MaBNhan equals tu.MaBNhan
                                    join tuct in data.TamUngcts.Where(p => cp == 2 ? true : p.TrongBH == cp).Where(p => p.Status == 0) on tu.IDTamUng equals tuct.IDTamUng
                                    select new { bn.MaBNhan, bn.IDDTBN, tu.IDTamUng, tu.SoHD, bn.TenBNhan, bn.Tuoi, bn.DChi, bn.NoiTru, bn.DTNT, tu.PhanLoai, tu.NgayThu, tuct.MaDV, tuct.Mien, tu.MaKP, tuct.SoLuong, tuct.TienBN, tuct.TrongBH, tuct.ThanhTien, tuct.DonGia, tuct.SoTien }).ToList();

                    var qTamung1 = (from bn in qTamung0
                                    join dt in qdtbn on bn.IDDTBN equals dt.IDDTBN
                                    select new { bn.MaBNhan, bn.IDTamUng, bn.SoHD, bn.TenBNhan, bn.Tuoi, bn.DChi, bn.NoiTru, bn.DTNT, dt.DTBN1, bn.PhanLoai, bn.NgayThu, bn.MaDV, bn.Mien, bn.MaKP, bn.SoLuong, bn.TienBN, bn.TrongBH, bn.ThanhTien, bn.DonGia, bn.SoTien }).ToList();
                    var qTamung2 = (from bn in qTamung1
                                    join dv in qdv on bn.MaDV equals dv.MaDV
                                    select new
                                    {
                                        bn.MaBNhan,
                                        bn.TenBNhan,
                                        bn.Tuoi,
                                        bn.DChi,
                                        bn.NoiTru,
                                        bn.DTNT,
                                        bn.IDTamUng,
                                        bn.SoHD,
                                        DTBN1 = bn.DTBN1.ToUpper().Trim(),
                                        bn.MaDV,
                                        bn.DonGia,
                                        bn.SoLuong,
                                        bn.ThanhTien,
                                        bn.SoTien,
                                        bn.TrongBH,
                                        TienBN = bn.SoTien,//hiển thị số tiền bệnh nhân thực trả
                                        dv.IdTieuNhom,
                                        dv.TenRG,
                                        dv.IDNhom,
                                        dv.TenNhomCT,
                                        NgayTT = bn.NgayThu.Value.Date
                                    }).ToList();

                    var qTamung3 = (from bn in qTamung2
                                    group bn by new { bn.TenBNhan, bn.Tuoi, bn.DChi, bn.MaBNhan, bn.NoiTru, bn.NgayTT, bn.DTBN1, bn.DTNT, bn.IDTamUng, bn.SoHD } into kq
                                    select new
                                    {
                                        kq.Key.TenBNhan,
                                        kq.Key.Tuoi,
                                        kq.Key.DChi,
                                        kq.Key.MaBNhan,
                                        kq.Key.NgayTT,
                                        kq.Key.DTBN1,
                                        kq.Key.SoHD,
                                        kq.Key.IDTamUng,
                                        #region mẫu chi tiết 30005
                                        CDHA = kq.Where(p => idNhomCDHA.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        TN1 = kq.Where(p => p.TenRG.Equals(rep30005.TieuNhom1.Value)).Sum(p => p.TienBN),
                                        TN2 = kq.Where(p => p.TenRG.Equals(rep30005.TieuNhom2.Value)).Sum(p => p.TienBN),
                                        TN3 = kq.Where(p => p.TenRG.Equals(rep30005.TieuNhom3.Value)).Sum(p => p.TienBN),
                                        TN4 = kq.Where(p => p.TenRG.Equals(rep30005.TieuNhom4.Value)).Sum(p => p.TienBN),
                                        TN5 = kq.Where(p => p.TenRG.Equals(rep30005.TieuNhom5.Value)).Sum(p => p.TienBN),
                                        TN6 = kq.Where(p => p.TenRG.Equals(rep30005.TieuNhom6.Value)).Sum(p => p.TienBN),
                                        TN7 = kq.Where(p => p.TenRG.Equals(rep30005.TieuNhom7.Value)).Sum(p => p.TienBN),
                                        TN8 = kq.Where(p => p.TenRG.Equals(rep30005.TieuNhom8.Value)).Sum(p => p.TienBN),
                                        TN9 = kq.Where(p => p.TenRG.Equals(rep30005.TieuNhom9.Value)).Sum(p => p.TienBN),
                                        #endregion
                                        Giuong = kq.Where(p => idTienGiuong.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        XN = kq.Where(p => idNhomXN.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        TDCN = kq.Where(p => idNhomTDCN.Contains(p.IdTieuNhom)).Where(p => !idDientim.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        SieuAm = kq.Where(p => idSieuam.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        SieuAmDL = kq.Where(p => idSieuamDL.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        DienTim = kq.Where(p => idDientim.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        ChupCT = kq.Where(p => idChupCT.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        DoCNHH = kq.Where(p => idDoCNHH.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        ThuThuatPT = kq.Where(p => idThuThuat.Contains(p.IdTieuNhom)).Where(p => !idNoiSoi.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        NoiSoi = kq.Where(p => idNoiSoi.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        Congkham = (radBenhVien.SelectedIndex == 4 && kq.Key.DTBN1 == "KSK") ? 0 : kq.Where(p => idCongkham.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        KSK = (radBenhVien.SelectedIndex == 4 && kq.Key.DTBN1 == "KSK") ? kq.Where(p => idCongkham.Contains(p.IdTieuNhom)).Sum(p => p.TienBN) : 0,
                                        TienVC = kq.Where(p => idtienvc.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        XQ = kq.Where(p => idXQ.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        DoLoangXuong = kq.Where(p => idDoLoangXuong.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        LuuHuyetNao = kq.Where(p => idLuuHuyetNao.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        TienBN = kq.Sum(p => p.TienBN)
                                    }).OrderBy(p => p.NgayTT).ToList();
                    List<VPHangNgay> qTamung4 = (from bn in qTamung3
                                                 group bn by new { bn.TenBNhan, bn.Tuoi, bn.DChi, bn.MaBNhan, bn.NgayTT, bn.DTBN1, bn.IDTamUng, bn.SoHD } into kq
                                                 select new VPHangNgay
                                                 {
                                                     TenBNhan = kq.Key.TenBNhan,
                                                     Tuoi = kq.Key.Tuoi ?? 0,
                                                     DChi = kq.Key.DChi,
                                                     MaBNhan = kq.Key.MaBNhan,
                                                     DTuong = kq.Key.DTBN1,
                                                     NgayTT = kq.Key.NgayTT,
                                                     XN = kq.Sum(p => p.XN),
                                                     TDCN = kq.Sum(p => p.TDCN),
                                                     IDTamUng = kq.Key.IDTamUng,
                                                     SoHD = kq.Key.SoHD,
                                                     #region dùng mẫu chi tiết
                                                     CDHA = kq.Sum(p => p.CDHA),
                                                     TienTN1 = kq.Sum(p => p.TN1),
                                                     TienTN2 = kq.Sum(p => p.TN2),
                                                     TienTN3 = kq.Sum(p => p.TN3),
                                                     TienTN4 = kq.Sum(p => p.TN4),
                                                     TienTN5 = kq.Sum(p => p.TN5),
                                                     TienTN6 = kq.Sum(p => p.TN6),
                                                     TienTN7 = kq.Sum(p => p.TN7),
                                                     TienTN8 = kq.Sum(p => p.TN8),
                                                     TienTN9 = kq.Sum(p => p.TN9),
                                                     #endregion
                                                     SieuAm = kq.Sum(p => p.SieuAm),
                                                     SieuAmDL = kq.Sum(p => p.SieuAmDL),
                                                     ChupCT = kq.Sum(p => p.ChupCT),
                                                     DoCNHH = kq.Sum(p => p.DoCNHH),
                                                     DienTim = kq.Sum(p => p.DienTim),
                                                     ThuThuatPT = kq.Sum(p => p.ThuThuatPT),
                                                     NoiSoi = kq.Sum(p => p.NoiSoi),
                                                     Congkham = kq.Sum(p => p.Congkham),
                                                     KSK = kq.Sum(p => p.KSK),
                                                     TienVC = kq.Sum(p => p.TienVC),
                                                     XQ = kq.Sum(p => p.XQ),
                                                     Giuong = kq.Sum(p => p.Giuong),
                                                     DoLoangXuong = kq.Sum(p => p.DoLoangXuong),
                                                     LuuHuyetNao = kq.Sum(p => p.LuuHuyetNao),
                                                     NgoaiTruBH = 0,
                                                     TienNGDM = 0,
                                                     TienBN = kq.Sum(p => p.TienBN),
                                                     Tong = kq.Sum(p => p.TienBN)
                                                 }).OrderBy(p => p.NgayTT).ToList();

                    q6.AddRange(qTamung4);

                    var qTamungksk = (from bn in data.BenhNhans.Where(p => p.DTuong == "KSK")//.Where(p => p.TuyenDuoi == 0).Where(p => dtbn == 100 || p.IDDTBN == dtbn).Where(p => (_ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (_DTNT && p.NoiTru == 0 && p.DTNT == true) || (_noitru && p.NoiTru == 1))
                                      join tu in data.TamUngs.Where(p => XuatHD == 2 ? true : (XuatHD == 0 ? (p.FkeyVNPT == null && p.MaHD == null) : (p.FkeyVNPT != null || p.MaHD != null))).Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay).Where(p => p.PhanLoai == 1).Where(p => macbthu == "" || p.MaCB == macbthu).Where(p => makp == 0 || p.MaKP == makp).Where(p => gioHC == 2 || (gioHC == 1 && p.NgoaiGio == 1) || (gioHC == 0 && (p.NgoaiGio == null || p.NgoaiGio != 1))) on bn.MaBNhan equals tu.MaBNhan
                                      select new { bn.MaBNhan, bn.IDDTBN, tu.SoTien, tu.IDTamUng, tu.SoHD, bn.TenBNhan, bn.Tuoi, bn.DChi, bn.NoiTru, bn.DTNT, tu.PhanLoai, tu.NgayThu, tu.MaKP, bn.DTuong }).ToList();
                    List<VPHangNgay> qksk = (from bn in qTamungksk
                                             group bn by new { bn.TenBNhan, bn.Tuoi, bn.DChi, bn.MaBNhan, bn.NgayThu, bn.DTuong, bn.IDTamUng, bn.SoHD } into kq
                                             select new VPHangNgay
                                             {
                                                 TenBNhan = kq.Key.TenBNhan,
                                                 Tuoi = kq.Key.Tuoi ?? 0,
                                                 DChi = kq.Key.DChi,
                                                 DTuong = kq.Key.DTuong,
                                                 MaBNhan = kq.Key.MaBNhan,
                                                 NgayTT = kq.Key.NgayThu.Value.Date,
                                                 IDTamUng = kq.Key.IDTamUng,
                                                 SoHD = kq.Key.SoHD,
                                                 KSK = kq.Sum(p => p.SoTien ?? 0),
                                                 Tong = kq.Sum(p => p.SoTien ?? 0)
                                             }).OrderBy(p => p.NgayTT).ToList();
                    q6.AddRange(qksk);
                    all.AddRange(q6);

                    all = (from a in all
                           group a by new { a.TenBNhan, a.MaBNhan, a.DChi, a.Tuoi, a.NgayTT, a.DTuong, a.IDTamUng, a.SoHD } into kq
                           select new VPHangNgay
                           {
                               TenBNhan = kq.Key.TenBNhan,
                               IDTamUng = kq.Key.IDTamUng,
                               SoHD = kq.Key.SoHD,
                               Tuoi = kq.Key.Tuoi,
                               DChi = kq.Key.DChi,
                               MaBNhan = kq.Key.MaBNhan,
                               DTuong = kq.Key.DTuong,
                               NgayTT = kq.Key.NgayTT,
                               Giuong = kq.Sum(p => p.Giuong),
                               XN = kq.Sum(p => p.XN),
                               TDCN = kq.Sum(p => p.TDCN),
                               SieuAm = kq.Sum(p => p.SieuAm),
                               SieuAmDL = kq.Sum(p => p.SieuAmDL),
                               DienTim = kq.Sum(p => p.DienTim),
                               ChupCT = kq.Sum(p => p.ChupCT),
                               DoCNHH = kq.Sum(p => p.DoCNHH),
                               ThuThuatPT = kq.Sum(p => p.ThuThuatPT),
                               NoiSoi = kq.Sum(p => p.NoiSoi),
                               Congkham = kq.Sum(p => p.Congkham),
                               TienVC = kq.Sum(p => p.TienVC),
                               XQ = kq.Sum(p => p.XQ),
                               NgoaiTruBH = kq.Sum(p => p.NgoaiTruBH),
                               KSK = kq.Sum(p => p.KSK),
                               DoLoangXuong = kq.Sum(p => p.DoLoangXuong),
                               LuuHuyetNao = kq.Sum(p => p.LuuHuyetNao),
                               TienThuoc = kq.Sum(p => p.TienThuoc),
                               TienVTYT = kq.Sum(p => p.TienVTYT),
                               TienBN = kq.Sum(p => p.TienBN),
                               TienNGDM = kq.Sum(p => p.TienNGDM),
                               Tong = kq.Sum(p => p.Tong) // kq.Sum(p => p.XN + p.TDCN + p.SieuAm + p.DienTim + p.ThuThuatPT + p.NoiSoi + p.Congkham + p.TienVC + p.XQ + p.NgoaiTruBH + p.KSK + p.DoLoangXuong + p.LuuHuyetNao + p.TienThuoc + p.TienVTYT)
                           }).Where(p => p.Tong != 0).OrderBy(p => p.SoHD).ThenBy(p => p.NgayTT).ToList();
                    string[] _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                    string[] _tieude = { "stt", "Họ tên", "Tuổi", "Địa chỉ", "Số phiếu", "Tiền giường", "Nhóm xét nghiệm", "Siêu âm", "Điện tim", "Thủ thuật - Phẫu thuật", "Nội soi", "KSK", "Công khám", "Tiền vận chuyển", "Chụp XQ", "Nhóm thăm dò chức năng", "siêu âm Doppler", "XQuang CT", "% BHYT ngoại trú", "Tiền BN DV", "Cộng" };
                    string _filePath = "D:\\BCVienPhiHangNgay.xls";
                    int[] _arrWidth = new int[] { };
                    DungChung.Bien.MangHaiChieu = new Object[all.Count + 1, 21];
                    for (int i = 0; i < 21; i++)
                    {
                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                    }
                    int num = 1;
                    foreach (var r in all)
                    {
                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.TenBNhan;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.Tuoi;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.DChi;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.SoHD;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.Giuong;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.XN;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.SieuAm;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.DienTim;
                        DungChung.Bien.MangHaiChieu[num, 9] = r.ThuThuatPT;
                        DungChung.Bien.MangHaiChieu[num, 10] = r.NoiSoi;
                        DungChung.Bien.MangHaiChieu[num, 11] = r.KSK;
                        DungChung.Bien.MangHaiChieu[num, 12] = r.Congkham;
                        DungChung.Bien.MangHaiChieu[num, 13] = r.TienVC;
                        DungChung.Bien.MangHaiChieu[num, 14] = r.XQ;
                        DungChung.Bien.MangHaiChieu[num, 15] = r.TDCN;
                        DungChung.Bien.MangHaiChieu[num, 16] = r.SieuAmDL;
                        DungChung.Bien.MangHaiChieu[num, 17] = r.ChupCT;
                        DungChung.Bien.MangHaiChieu[num, 18] = r.NgoaiTruBH;
                        DungChung.Bien.MangHaiChieu[num, 19] = r.TienBN;
                        DungChung.Bien.MangHaiChieu[num, 20] = r.Tong;
                        num++;
                    }

                    if (ckHienThiNgay.Checked)
                    {
                        BaoCao.rep_VienPhiHangNgay_30005_Ngay rep = new BaoCao.rep_VienPhiHangNgay_30005_Ngay(ckHienThiNgay.Checked);
                        frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Sheet1", _filePath, true, this.Name);
                        rep.celTitBH.Text = "% BHYT";
                        rep.DataSource = all;
                        rep.colCQ.Text = DungChung.Bien.TenCQ.ToUpper();
                        rep.cel_diadanh.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                        rep.celNgayThang.Text = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy");
                        if (String.IsNullOrEmpty(txtTieude.Text))
                        {
                            rep.CelTieuDe.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN VIỆN PHÍ HẰNG NGÀY";
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
                    {
                        BaoCao.rep_VienPhiHangNgay_30003_moi rep = new BaoCao.rep_VienPhiHangNgay_30003_moi();
                        frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Sheet1", _filePath, true, this.Name);
                        rep.celTitBH.Text = "% BHYT";
                        rep.DataSource = all;
                        rep.colCQ.Text = DungChung.Bien.TenCQ.ToUpper();
                        rep.cel_diadanh.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                        rep.celNgayThang.Text = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy");
                        if (String.IsNullOrEmpty(txtTieude.Text))
                        {
                            rep.CelTieuDe.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN VIỆN PHÍ HẰNG NGÀY";


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
                }
                #endregion
                else
                {

                    #region bv khác

                    #region Viện phí đối tượng bệnh nhân ko phải KSK // lấy cả đối tượng bn KSK đối với bv 01830 (ko lấy trong bảng tạm ứng)
                    var q2 = (from vp in data.VienPhis.Where(p => (macbthu == "" || radBenhVien.SelectedIndex == 2) ? true : p.MaCB == macbthu).Where(p => (macbTT == "" || radBenhVien.SelectedIndex == 0) ? true : p.MaCB == macbTT).Where(p => rdNgay.SelectedIndex == 1 ? (p.NgayDuyet >= tungay && p.NgayDuyet <= denngay) : (p.NgayTT >= tungay && p.NgayTT <= denngay)).Where(p => gioHC == 2 || (gioHC == 1 && p.NgoaiGio == 1) || (gioHC == 0 && (p.NgoaiGio == null || p.NgoaiGio != 1)))
                              join vpct in data.VienPhicts.Where(p => cp == 2 ? true : p.TrongBH == cp).Where(p => (radBenhVien.SelectedIndex == 4) ? (p.ThanhToan != 1) : p.ThanhToan == 0).Where(p => makp == 0 || p.MaKP == makp)
                              on vp.idVPhi equals vpct.idVPhi
                              select new
                              {
                                  vp.MaBNhan,
                                  vp.MaCB,
                                  vp.NgayTT,
                                  vp.NgoaiGio,
                                  vpct.Mien,
                                  vpct.MaKP,
                                  vpct.TrongBH,
                                  vpct.MaDV,
                                  vpct.DonGia,
                                  vpct.SoLuong,
                                  vpct.ThanhTien,
                                  vpct.ThanhToan,
                                  vpct.TienBH,
                                  TienBN = vpct.TienBN,
                                  TienNGDM = vpct.TrongBH == 1 ? 0 : vpct.TienBN,
                                  vp.NgayDuyet
                              }).ToList();

                    var q3 = (from bn in q1.Where(p => p.kq1 != null).Where(p => DungChung.Bien.MaBV == "27183" ? true : p.DTBN1 != "KSK")
                              join vp in q2 on bn.MaBNhan equals vp.MaBNhan
                              join dv in qdv on vp.MaDV equals dv.MaDV
                              select new
                              {
                                  bn.MaBNhan,
                                  bn.TenBNhan,
                                  bn.Tuoi,
                                  bn.DChi,
                                  bn.NoiTru,
                                  bn.DTNT,
                                  DTBN1 = bn.DTBN1.ToUpper().Trim(),
                                  vp.TrongBH,
                                  vp.MaDV,
                                  vp.DonGia,
                                  vp.SoLuong,
                                  vp.ThanhTien,
                                  vp.ThanhToan,
                                  vp.TienBH,
                                  TienBN = (radBenhVien.SelectedIndex == 4 && bn.DTBN1 == "KSK") ? vp.ThanhTien : vp.TienBN,
                                  TienNGDM = bn.DTuong == "BHYT" ? vp.TienNGDM : 0,
                                  dv.IdTieuNhom,
                                  dv.TenRG,
                                  dv.IDNhom,
                                  dv.TenNhomCT,
                                  NgayTT = (rdNgay.SelectedIndex == 1 && radBenhVien.SelectedIndex == 3) ? vp.NgayDuyet.Value.Date : vp.NgayTT.Value.Date
                              }).ToList();
                    #region bv kinh môn
                    if (radBenhVien.SelectedIndex == 2)
                    {
                        var qCBThu = data.TamUngs.Where(p => (p.PhanLoai == 1 || p.PhanLoai == 2)).ToList();//&& (macbthu == "" || p.MaCB == macbthu)
                        q3 = (from bn in q1.Where(p => p.kq1 != null)
                              join vp in q2 on bn.MaBNhan equals vp.MaBNhan
                              join tu in qCBThu on bn.MaBNhan equals tu.MaBNhan into kq
                              from kq1 in kq.DefaultIfEmpty()
                              join dv in qdv on vp.MaDV equals dv.MaDV
                              where (macbthu == "" || (kq1 != null && kq1.MaCB == macbthu))
                              select new
                              {
                                  bn.MaBNhan,
                                  bn.TenBNhan,
                                  bn.Tuoi,
                                  bn.DChi,
                                  bn.NoiTru,
                                  bn.DTNT,
                                  DTBN1 = bn.DTBN1.ToUpper().Trim(),
                                  vp.TrongBH,
                                  vp.MaDV,
                                  vp.DonGia,
                                  vp.SoLuong,
                                  vp.ThanhTien,
                                  vp.ThanhToan,
                                  vp.TienBH,
                                  vp.TienBN,
                                  TienNGDM = bn.DTuong == "BHYT" ? vp.TienNGDM : 0,
                                  dv.IdTieuNhom,
                                  dv.TenRG,
                                  dv.IDNhom,
                                  dv.TenNhomCT,
                                  NgayTT = (rdNgay.SelectedIndex == 1 && radBenhVien.SelectedIndex == 3) ? vp.NgayDuyet.Value.Date : vp.NgayTT.Value.Date
                              }).ToList();
                    }
                    #endregion
                    #region BV 30372
                    if (DungChung.Bien.MaBV == "30372")
                    {
                        if (radBenhVien.SelectedIndex == 0)
                        {
                            q2 = (from vp in data.VienPhis.Where(p => macbTT == "" ? true : p.MaCB == macbTT).Where(p => rdNgay.SelectedIndex == 1 ? (p.NgayDuyet >= tungay && p.NgayDuyet <= denngay) : (p.NgayTT >= tungay && p.NgayTT <= denngay)).Where(p => gioHC == 2 || (gioHC == 1 && p.NgoaiGio == 1) || (gioHC == 0 && (p.NgoaiGio == null || p.NgoaiGio != 1)))
                                  join vpct in data.VienPhicts.Where(p => cp == 2 ? true : p.TrongBH == cp).Where(p => (radBenhVien.SelectedIndex == 4) ? (p.ThanhToan != 1) : p.ThanhToan == 0).Where(p => makp == 0 || p.MaKP == makp)
                                  on vp.idVPhi equals vpct.idVPhi
                                  select new
                                  {
                                      vp.MaBNhan,
                                      vp.MaCB,
                                      vp.NgayTT,
                                      vp.NgoaiGio,
                                      vpct.Mien,
                                      vpct.MaKP,
                                      vpct.TrongBH,
                                      vpct.MaDV,
                                      vpct.DonGia,
                                      vpct.SoLuong,
                                      vpct.ThanhTien,
                                      vpct.ThanhToan,
                                      vpct.TienBH,
                                      TienBN = vpct.TienBN,
                                      TienNGDM = vpct.TrongBH == 1 ? 0 : vpct.TienBN,
                                      vp.NgayDuyet
                                  }).ToList();
                            #region bo
                            //var qcbTT = data.VienPhis.ToList();
                            q3 = (from bn in q1.Where(p => p.kq1 != null).Where(p => DungChung.Bien.MaBV == "27183" ? true : p.DTBN1 != "KSK")
                                  join vp in q2 on bn.MaBNhan equals vp.MaBNhan
                                  // join cbtt in qcbTT on bn.MaBNhan equals cbtt.MaBNhan into kq
                                  //from kq1 in kq.DefaultIfEmpty()
                                  join dv in qdv on vp.MaDV equals dv.MaDV
                                  //where(macbTT==""||kq1 !=null && kq1.MaCB==macbTT)
                                  select new
                                  {
                                      bn.MaBNhan,
                                      bn.TenBNhan,
                                      bn.Tuoi,
                                      bn.DChi,
                                      bn.NoiTru,
                                      bn.DTNT,
                                      DTBN1 = bn.DTBN1.ToUpper().Trim(),
                                      vp.TrongBH,
                                      vp.MaDV,
                                      vp.DonGia,
                                      vp.SoLuong,
                                      vp.ThanhTien,
                                      vp.ThanhToan,
                                      vp.TienBH,
                                      TienBN = (radBenhVien.SelectedIndex == 4 && bn.DTBN1 == "KSK") ? vp.ThanhTien : vp.TienBN,
                                      TienNGDM = bn.DTuong == "BHYT" ? vp.TienNGDM : 0,
                                      dv.IdTieuNhom,
                                      dv.TenRG,
                                      dv.IDNhom,
                                      dv.TenNhomCT,
                                      NgayTT = (rdNgay.SelectedIndex == 1 && radBenhVien.SelectedIndex == 3) ? vp.NgayDuyet.Value.Date : vp.NgayTT.Value.Date
                                  }).ToList();
                            #endregion
                        }
                    }
                    #endregion



                    //var qtamungBNDV = data.TamUngs.Where(p => p.PhanLoai == 0).Where(p => gioHC == 2 || (gioHC == 1 && p.NgoaiGio == 1) || (gioHC == 0 && (p.NgoaiGio == null || p.NgoaiGio != 1))).ToList();
                    var qtamungBNDV = (from a in data.TamUngs.Where(p => p.PhanLoai == 0).Where(p => macbthu == "" ? true : p.MaCB == macbthu)
                                                             .Where(p => gioHC == 2 ? true : ((gioHC == 1 && p.NgoaiGio == 1) ? true : (gioHC == 0 && (p.NgoaiGio == null || p.NgoaiGio != 1))))
                                       select a).ToList();
                    if (DungChung.Bien.MaBV == "30372")
                    {
                        if (radBenhVien.SelectedIndex == 0)
                        {
                            qtamungBNDV = (from a in data.TamUngs.Where(p => p.PhanLoai == 0).Where(p => macbTT == "" ? true : p.MaCB == macbTT)
                                                             .Where(p => gioHC == 2 ? true : ((gioHC == 1 && p.NgoaiGio == 1) ? true : (gioHC == 0 && (p.NgoaiGio == null || p.NgoaiGio != 1))))
                                           select a).ToList();
                        }
                    }

                    var q4 = (from bn in q3
                              group bn by new { bn.TenBNhan, bn.Tuoi, bn.DChi, bn.MaBNhan, bn.NoiTru, bn.DTNT, bn.NgayTT, bn.DTBN1 } into kq
                              select new
                              {
                                  kq.Key.TenBNhan,
                                  kq.Key.Tuoi,
                                  kq.Key.DChi,
                                  kq.Key.MaBNhan,
                                  kq.Key.NgayTT,
                                  kq.Key.DTBN1,
                                  #region bv 27183
                                  XN = (radBenhVien.SelectedIndex == 4) ? (kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idNhomXN.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)) : 0,
                                  TDCN = (radBenhVien.SelectedIndex == 4) ? (kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idNhomTDCN.Contains(p.IdTieuNhom)).Where(p => !idDientim.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)) : 0,
                                  SieuAm = (radBenhVien.SelectedIndex == 4) ? (kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idSieuam.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)) : 0,
                                  SieuAmDL = (radBenhVien.SelectedIndex == 4) ? (kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idSieuamDL.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)) : 0,
                                  DienTim = (radBenhVien.SelectedIndex == 4) ? (kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idDientim.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)) : 0,
                                  ChupCT = (radBenhVien.SelectedIndex == 4) ? (kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idChupCT.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)) : 0,
                                  DoCNHH = (radBenhVien.SelectedIndex == 4) ? (kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idDoCNHH.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)) : 0,
                                  ThuThuatPT = (radBenhVien.SelectedIndex == 4) ? (kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idThuThuat.Contains(p.IdTieuNhom)).Where(p => !idNoiSoi.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)) : 0,
                                  NoiSoi = (radBenhVien.SelectedIndex == 4) ? (kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idNoiSoi.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)) : 0,
                                  Congkham = (radBenhVien.SelectedIndex == 4) ? (kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idCongkham.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)) : 0,
                                  TienVC = (radBenhVien.SelectedIndex == 4) ? (kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idtienvc.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)) : 0,
                                  XQ = (radBenhVien.SelectedIndex == 4) ? (kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idXQ.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)) : 0,
                                  DoLoangXuong = (radBenhVien.SelectedIndex == 4) ? (kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idDoLoangXuong.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)) : 0,
                                  LuuHuyetNao = (radBenhVien.SelectedIndex == 4) ? (kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idLuuHuyetNao.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)) : 0,
                                  DienNaoDo = (radBenhVien.SelectedIndex == 4) ? (kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => idDienNaoDo.Contains(p.IdTieuNhom)).Sum(p => p.TienBN)) : 0,
                                  #endregion
                                  TienNGDM = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Sum(p => p.TienNGDM),
                                  TienBN = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => p.DTBN1 == "DỊCH VỤ" || (radBenhVien.SelectedIndex == 4 && p.DTBN1 == "KSK")).Sum(p => p.TienBN), // chỉ áp dụng đối với đối tượng dịch vụ _30002
                                  // TienBH = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Sum(p => p.TienBH),
                                  // ThanhTien = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Sum(p => p.ThanhTien),
                                  NgoaiTruBH = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => p.DTBN1 == "BHYT").Where(p => p.TrongBH == 1).Sum(p => p.TienBN),// -( DungChung.Bien.MaBV == "30005" ? (((ngoaitru && kq.Key.NoiTru == 0 &&kq.Key.DTNT == false) || (DTNT && kq.Key.NoiTru == 0 && kq.Key.DTNT == true) || (noitru && kq.Key.NoiTru == 1)) ? qtamungBNDV.Where(p => p.MaBNhan == kq.Key.MaBNhan).Sum(p => p.SoTien ?? 0) : 0) : 0), // phải trừ tiền đã tạm ứng
                                  TienThuoc = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => radBenhVien.SelectedIndex == 4 ? true : p.TrongBH == 0).Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7).Sum(p => p.TienBN),
                                  TienVTYT = kq.Where(p => (ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (DTNT && p.NoiTru == 0 && p.DTNT == true) || (noitru && p.NoiTru == 1)).Where(p => radBenhVien.SelectedIndex == 4 ? true : p.TrongBH == 0).Where(p => p.IDNhom == 10 || p.IDNhom == 11).Sum(p => p.TienBN),
                                  TamUng = ((ngoaitru && kq.Key.NoiTru == 0 && kq.Key.DTNT == false) || (DTNT && kq.Key.NoiTru == 0 && kq.Key.DTNT == true) || (noitru && kq.Key.NoiTru == 1)) ? (qtamungBNDV.Where(p => p.MaBNhan == kq.Key.MaBNhan).Sum(p => p.SoTien ?? 0)) : 0
                              }).ToList();

                    List<VPHangNgay> q5 = (from bn in q4
                                           group bn by new { bn.TenBNhan, bn.Tuoi, bn.DChi, bn.MaBNhan, bn.NgayTT, bn.DTBN1 } into kq
                                           select new VPHangNgay
                                           {
                                               TenBNhan = kq.Key.TenBNhan,
                                               Tuoi = kq.Key.Tuoi ?? 0,
                                               DChi = kq.Key.DChi,
                                               DTuong = kq.Key.DTBN1,
                                               MaBNhan = kq.Key.MaBNhan,
                                               NgayTT = kq.Key.NgayTT,
                                               NgoaiTruBH = (radBenhVien.SelectedIndex == 2) ? (kq.Sum(p => (p.NgoaiTruBH < 0) ? 0 : p.NgoaiTruBH)) : ((DungChung.Bien.MaBV == "30005") ? (kq.Sum(p => (p.NgoaiTruBH < 0) ? 0 : p.NgoaiTruBH) - kq.Sum(p => p.TamUng)) : (kq.Sum(p => (p.NgoaiTruBH < 0) ? 0 : p.NgoaiTruBH))),// thu thêm bệnh nhân mới tính (bvì là bc thu vp)
                                               TienThuoc = kq.Sum(p => p.TienThuoc),
                                               TienVTYT = kq.Sum(p => p.TienVTYT),
                                               TienBN = kq.Sum(p => p.TienBN),
                                               //TienBH = kq.Sum(p => p.TienBH),
                                               //ThanhTien = kq.Sum(p => p.ThanhTien),
                                               TienNGDM = kq.Sum(p => p.TienNGDM),
                                               XN = kq.Sum(p => p.XN),
                                               TDCN = kq.Sum(p => p.TDCN),
                                               XQ = radBenhVien.SelectedIndex == 0 && DungChung.Bien.MaBV != "30372" ? (kq.Sum(p => p.XQ) + kq.Sum(p => p.ChupCT)) : kq.Sum(p => p.XQ),
                                               SieuAm =  radBenhVien.SelectedIndex == 0 && DungChung.Bien.MaBV !="30372"? (kq.Sum(p => p.SieuAm) + kq.Sum(p => p.SieuAmDL)) : kq.Sum(p => p.SieuAm),
                                               SieuAmDL = kq.Sum(p => p.SieuAmDL),
                                               ChupCT = kq.Sum(p => p.ChupCT),
                                               DoCNHH = kq.Sum(p => p.DoCNHH),
                                               DienTim = kq.Sum(p => p.DienTim),
                                               ThuThuatPT = kq.Sum(p => p.ThuThuatPT),
                                               NoiSoi = kq.Sum(p => p.NoiSoi),
                                               DienNaoDo = kq.Sum(p => p.DienNaoDo),
                                               Congkham = (radBenhVien.SelectedIndex == 4 && kq.Key.DTBN1 == "KSK") ? 0 : kq.Sum(p => p.Congkham),
                                               KSK = (radBenhVien.SelectedIndex == 4 && kq.Key.DTBN1 == "KSK") ? kq.Sum(p => p.Congkham) : 0,
                                               TienVC = kq.Sum(p => p.TienVC),
                                               DoLoangXuong = kq.Sum(p => p.DoLoangXuong),
                                               LuuHuyetNao = kq.Sum(p => p.LuuHuyetNao),
                                               Tong = (radBenhVien.SelectedIndex == 0 || radBenhVien.SelectedIndex == 4) ? (kq.Sum(p => p.TienThuoc) + kq.Sum(p => p.TienVTYT) + kq.Sum(p => p.XN) + kq.Sum(p => p.TDCN) + kq.Sum(p => p.SieuAm) + kq.Sum(p => p.SieuAmDL) + kq.Sum(p => p.ChupCT) + kq.Sum(p => p.DoCNHH) + kq.Sum(p => p.DienTim) + kq.Sum(p => p.ThuThuatPT) + kq.Sum(p => p.NoiSoi) + kq.Sum(p => p.Congkham) + kq.Sum(p => p.TienVC) + kq.Sum(p => p.XQ) + kq.Sum(p => p.DoLoangXuong) + kq.Sum(p => p.LuuHuyetNao) + kq.Sum(p => p.DienNaoDo) + kq.Sum(p => p.NgoaiTruBH))
                                               : (kq.Sum(p => (p.NgoaiTruBH < 0) ? 0 : (radBenhVien.SelectedIndex == 2 ? (p.NgoaiTruBH + p.TienBN) : (radBenhVien.SelectedIndex == 3 ? (p.TienBN + p.NgoaiTruBH) : (radBenhVien.SelectedIndex == 1 ? 0 : p.NgoaiTruBH)))))
                                           }).OrderBy(p => p.NgayTT).ToList();//.Where(p=>p.Tong != 0).ToList();
                    #endregion
                    #region Thu thẳng bệnh nhân
                    bool ktraTachCongKhamKSK = false;
                    if ((DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && radBenhVien.SelectedIndex == 0)
                        ktraTachCongKhamKSK = true;//.Where(p => XuatHD == 2 ? true : (XuatHD == 0 ? (p.FkeyVNPT == null && p.MaHD == null) : (p.FkeyVNPT != null || p.MaHD != null)))
                    var qTamung00 = (from bn in data.BenhNhans.Where(p => p.TuyenDuoi == 0).Where(p => dtbn == 100 || p.IDDTBN == dtbn).Where(p => (_ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (_DTNT && p.NoiTru == 0 && p.DTNT == true) || (_noitru && p.NoiTru == 1))
                                     join tu in data.TamUngs.Where(p => p.IDGoiDV <= 0 || p.IDGoiDV == null).Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay).Where(p => p.PhanLoai == 3).Where(p => makp == 0 || p.MaKP == makp).Where(p => macbthu == "" ? true : p.MaCB == macbthu).Where(p => gioHC == 2 || (gioHC == 1 && p.NgoaiGio == 1) || (gioHC == 0 && (p.NgoaiGio == null || p.NgoaiGio != 1))) on bn.MaBNhan equals tu.MaBNhan
                                     join tuct in data.TamUngcts.Where(p => cp == 2 ? true : p.TrongBH == cp).Where(p => p.Status == 0) on tu.IDTamUng equals tuct.IDTamUng
                                     select new { bn.MaBNhan, bn.IDDTBN, bn.TenBNhan, bn.Tuoi, bn.DChi, bn.NoiTru, bn.DTNT, tu.PhanLoai, tu.NgayThu, tuct.MaDV, tuct.Mien, tu.MaKP, tuct.SoLuong, tuct.TienBN, tuct.TrongBH, tuct.ThanhTien, tuct.DonGia, tuct.SoTien, tu.FkeyVNPT, tu.MaHD, DuyetPhieuThu = tu.DuyetPhieuThu ?? false }).ToList();
                    var qTamung0 = qTamung00.Where(p => (((DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && radBenhVien.SelectedIndex == 0) ? (rdDuyetPhieu.SelectedIndex == 2 ? true : (rdDuyetPhieu.SelectedIndex == 0 ? p.DuyetPhieuThu : !p.DuyetPhieuThu)) : true)).ToList();
                    if (DungChung.Bien.MaBV == "30372")
                    {
                        if (radBenhVien.SelectedIndex == 0)
                        {
                            qTamung0 = (from bn in data.BenhNhans.Where(p => p.TuyenDuoi == 0).Where(p => dtbn == 100 || p.IDDTBN == dtbn).Where(p => (_ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (_DTNT && p.NoiTru == 0 && p.DTNT == true) || (_noitru && p.NoiTru == 1))
                                        join tu in data.TamUngs.Where(p => p.IDGoiDV <= 0 || p.IDGoiDV == null).Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay).Where(p => p.PhanLoai == 3).Where(p => makp == 0 || p.MaKP == makp).Where(p => macbTT == "" ? true : p.MaCB == macbTT).Where(p => gioHC == 2 || (gioHC == 1 && p.NgoaiGio == 1) || (gioHC == 0 && (p.NgoaiGio == null || p.NgoaiGio != 1))) on bn.MaBNhan equals tu.MaBNhan
                                        join tuct in data.TamUngcts.Where(p => cp == 2 ? true : p.TrongBH == cp).Where(p => p.Status == 0) on tu.IDTamUng equals tuct.IDTamUng
                                        select new { bn.MaBNhan, bn.IDDTBN, bn.TenBNhan, bn.Tuoi, bn.DChi, bn.NoiTru, bn.DTNT, tu.PhanLoai, tu.NgayThu, tuct.MaDV, tuct.Mien, tu.MaKP, tuct.SoLuong, tuct.TienBN, tuct.TrongBH, tuct.ThanhTien, tuct.DonGia, tuct.SoTien, tu.FkeyVNPT, tu.MaHD, DuyetPhieuThu = tu.DuyetPhieuThu ?? false }).ToList();
                        }
                    }
                    var qTamung1 = (from bn in qTamung0
                                    join dt in qdtbn on bn.IDDTBN equals dt.IDDTBN
                                    select new { bn.MaBNhan, bn.TenBNhan, bn.Tuoi, bn.DChi, bn.NoiTru, bn.DTNT, dt.DTBN1, bn.PhanLoai, bn.NgayThu, bn.MaDV, bn.Mien, bn.MaKP, bn.SoLuong, bn.TienBN, bn.TrongBH, bn.ThanhTien, bn.DonGia, bn.SoTien, bn.FkeyVNPT, bn.MaHD }).ToList();
                    //var qTamung1 = (from bn in data.BenhNhans.Where(p => p.TuyenDuoi == 0).Where(p => dtbn == 100 || p.IDDTBN == dtbn).Where(p => (_ngoaitru && p.NoiTru == 0 && p.DTNT == false) || (_DTNT && p.NoiTru == 0 && p.DTNT == true) || (_noitru && p.NoiTru == 1))
                    //                join dt in data.DTBNs on bn.IDDTBN equals dt.IDDTBN
                    //                join tu in data.TamUngs.Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay).Where(p => p.PhanLoai == 3).Where(p => makp == 0 || p.MaKP == makp).Where(p => gioHC == 2 || (gioHC == 1 && p.NgoaiGio == 1) || (gioHC == 0 && (p.NgoaiGio == null || p.NgoaiGio != 1))) on bn.MaBNhan equals tu.MaBNhan
                    //                join tuct in data.TamUngcts.Where(p => cp == 2 ? true : p.TrongBH == cp) on tu.IDTamUng equals tuct.IDTamUng
                    //                select new { bn.MaBNhan, bn.TenBNhan, bn.Tuoi, bn.DChi, bn.NoiTru, bn.DTNT, dt.DTBN1, tu.PhanLoai, tu.NgayThu, tuct.MaDV, tuct.Mien, tu.MaKP, tuct.SoLuong, tuct.TienBN, tuct.TrongBH, tuct.ThanhTien, tuct.DonGia }).ToList();

                    var qTamung2 = (from bn in qTamung1.Where(p => (radBenhVien.SelectedIndex == 4) ? true : ((ktraTachCongKhamKSK && p.DTBN1 == "KSK") ? true : p.DTBN1 != "KSK"))// dùng cho bnksk phòng khám bảo ngọc// || (radBenhVien.SelectedIndex == 1 && DungChung.Bien.MaBV == "12345" && p.DTBN1 == "KSK"
                                    join dv in qdv on bn.MaDV equals dv.MaDV
                                    select new
                                    {
                                        bn.MaBNhan,
                                        bn.TenBNhan,
                                        bn.Tuoi,
                                        bn.DChi,
                                        bn.NoiTru,
                                        bn.DTNT,
                                        DTBN1 = bn.DTBN1.ToUpper().Trim(),
                                        bn.MaDV,
                                        bn.DonGia,
                                        bn.SoLuong,
                                        bn.ThanhTien,
                                        bn.TrongBH,
                                        TienBN = bn.SoTien,
                                        dv.IdTieuNhom,
                                        dv.TenRG,
                                        dv.IDNhom,
                                        dv.TenNhomCT,
                                        dv.PLoai,
                                        NgayTT = bn.NgayThu.Value.Date,
                                        bn.FkeyVNPT,
                                        bn.MaHD

                                    }).ToList();

                    var qTamung3 = (from bn in qTamung2
                                    group bn by new { bn.TenBNhan, bn.Tuoi, bn.DChi, bn.MaBNhan, bn.NoiTru, bn.NgayTT, bn.DTBN1, bn.DTNT, bn.FkeyVNPT, bn.MaHD } into kq
                                    select new
                                    {
                                        kq.Key.TenBNhan,
                                        kq.Key.Tuoi,
                                        kq.Key.DChi,
                                        kq.Key.MaBNhan,
                                        kq.Key.NgayTT,
                                        kq.Key.DTBN1,
                                        #region mẫu chi tiết 30005
                                        CDHA = kq.Where(p => idNhomCDHA.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        TN1 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom1.Value.ToString()).Sum(p => p.TienBN),
                                        TN2 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom2.Value.ToString()).Sum(p => p.TienBN),
                                        TN3 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom3.Value.ToString()).Sum(p => p.TienBN),
                                        TN4 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom4.Value.ToString()).Sum(p => p.TienBN),
                                        TN5 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom5.Value.ToString()).Sum(p => p.TienBN),
                                        TN6 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom6.Value.ToString()).Sum(p => p.TienBN),
                                        TN7 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom7.Value.ToString()).Sum(p => p.TienBN),
                                        TN8 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom8.Value.ToString()).Sum(p => p.TienBN),
                                        TN9 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom9.Value.ToString()).Sum(p => p.TienBN),
                                        TN10 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom10.Value.ToString()).Sum(p => p.TienBN),
                                        TN11 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom11.Value.ToString()).Sum(p => p.TienBN),
                                        TN12 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom12.Value.ToString()).Sum(p => p.TienBN),
                                        TN13 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom13.Value.ToString()).Sum(p => p.TienBN),
                                        TN14 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom14.Value.ToString()).Sum(p => p.TienBN),
                                        TN15 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom15.Value.ToString()).Sum(p => p.TienBN),
                                        TN16 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom16.Value.ToString()).Sum(p => p.TienBN),

                                        TN17 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom1.Value.ToString()).Sum(p => p.TienBN),
                                        TN18 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom2.Value.ToString()).Sum(p => p.TienBN),
                                        TN19 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom3.Value.ToString()).Sum(p => p.TienBN),
                                        TN20 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom4.Value.ToString()).Sum(p => p.TienBN),
                                        TN21 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom5.Value.ToString()).Sum(p => p.TienBN),
                                        TN22 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom6.Value.ToString()).Sum(p => p.TienBN),
                                        TN23 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom7.Value.ToString()).Sum(p => p.TienBN),
                                        TN24 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom8.Value.ToString()).Sum(p => p.TienBN),
                                        TN25 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom9.Value.ToString()).Sum(p => p.TienBN),
                                        TN26 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom10.Value.ToString()).Sum(p => p.TienBN),
                                        TN27 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom11.Value.ToString()).Sum(p => p.TienBN),
                                        TN28 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom12.Value.ToString()).Sum(p => p.TienBN),
                                        TN29 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom13.Value.ToString()).Sum(p => p.TienBN),
                                        TN30 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom14.Value.ToString()).Sum(p => p.TienBN),
                                        TN31 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom15.Value.ToString()).Sum(p => p.TienBN),
                                        TN32 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom16.Value.ToString()).Sum(p => p.TienBN),
                                        #endregion
                                        XN = ktraTachCongKhamKSK && kq.Key.DTBN1 == "KSK" ? 0 : kq.Where(p => idNhomXN.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        TDCN = ktraTachCongKhamKSK && kq.Key.DTBN1 == "KSK" ? 0 : kq.Where(p => idNhomTDCN.Contains(p.IdTieuNhom)).Where(p => !idDientim.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        SieuAm = ktraTachCongKhamKSK && kq.Key.DTBN1 == "KSK" ? 0 : kq.Where(p => idSieuam.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        SieuAmDL = ktraTachCongKhamKSK && kq.Key.DTBN1 == "KSK" ? 0 : kq.Where(p => idSieuamDL.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        DienTim = ktraTachCongKhamKSK && kq.Key.DTBN1 == "KSK" ? 0 : kq.Where(p => idDientim.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        ChupCT = ktraTachCongKhamKSK && kq.Key.DTBN1 == "KSK" ? 0 : kq.Where(p => idChupCT.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        DoCNHH = ktraTachCongKhamKSK && kq.Key.DTBN1 == "KSK" ? 0 : kq.Where(p => idDoCNHH.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        ThuThuatPT = ktraTachCongKhamKSK && kq.Key.DTBN1 == "KSK" ? 0 : kq.Where(p => idThuThuat.Contains(p.IdTieuNhom)).Where(p => !idNoiSoi.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        NoiSoi = ktraTachCongKhamKSK && kq.Key.DTBN1 == "KSK" ? 0 : kq.Where(p => idNoiSoi.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        Congkham = (radBenhVien.SelectedIndex == 4 && kq.Key.DTBN1 == "KSK") ? 0 : kq.Where(p => idCongkham.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        KSK = ktraTachCongKhamKSK && kq.Key.DTBN1 == "KSK" ? 0 : (((radBenhVien.SelectedIndex == 4 && kq.Key.DTBN1 == "KSK") ? kq.Where(p => idCongkham.Contains(p.IdTieuNhom)).Sum(p => p.TienBN) : 0)),
                                        TienVC = ktraTachCongKhamKSK && kq.Key.DTBN1 == "KSK" ? 0 : kq.Where(p => idtienvc.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        XQ = ktraTachCongKhamKSK && kq.Key.DTBN1 == "KSK" ? 0 : kq.Where(p => idXQ.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        DoLoangXuong = ktraTachCongKhamKSK && kq.Key.DTBN1 == "KSK" ? 0 : kq.Where(p => idDoLoangXuong.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        LuuHuyetNao = ktraTachCongKhamKSK && kq.Key.DTBN1 == "KSK" ? 0 : kq.Where(p => idLuuHuyetNao.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        DienNaoDo = ktraTachCongKhamKSK && kq.Key.DTBN1 == "KSK" ? 0 : kq.Where(p => idDienNaoDo.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        TienThuoc = ktraTachCongKhamKSK && kq.Key.DTBN1 == "KSK" ? 0 : kq.Where(p => idThuoc.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        TienVTYT = ktraTachCongKhamKSK && kq.Key.DTBN1 == "KSK" ? 0 : kq.Where(p => idVTYT.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        TienBN = ktraTachCongKhamKSK && kq.Key.DTBN1 == "KSK" ? 0 : kq.Sum(p => p.TienBN),
                                        TienBNDV = kq.Where(p => p.DTBN1 == "DỊCH VỤ").Sum(p => p.TienBN),
                                        XuatHD = kq.Key.FkeyVNPT == null && kq.Key.MaHD == null ? 0 : 1
                                    }).OrderBy(p => p.NgayTT).ToList();//.Where(p=>p.Tong != 0).ToList();
                    #region bv 30005
                    if (radBenhVien.SelectedIndex == 2)
                    {
                        qTamung2 = (from bn in qTamung1
                                    join dv in qdv on bn.MaDV equals dv.MaDV
                                    select new
                                    {
                                        bn.MaBNhan,
                                        bn.TenBNhan,
                                        bn.Tuoi,
                                        bn.DChi,
                                        bn.NoiTru,
                                        bn.DTNT,
                                        DTBN1 = bn.DTBN1.ToUpper().Trim(),
                                        bn.MaDV,
                                        bn.DonGia,
                                        bn.SoLuong,
                                        bn.ThanhTien,
                                        bn.TrongBH,
                                        bn.TienBN,
                                        dv.IdTieuNhom,
                                        dv.TenRG,
                                        dv.IDNhom,
                                        dv.TenNhomCT,
                                        dv.PLoai,
                                        NgayTT = bn.NgayThu.Value.Date,
                                        bn.FkeyVNPT,
                                        bn.MaHD
                                    }).ToList();

                        qTamung3 = (from bn in qTamung2
                                    group bn by new { bn.TenBNhan, bn.Tuoi, bn.DChi, bn.MaBNhan, bn.NoiTru, bn.NgayTT, bn.DTBN1, bn.DTNT, bn.FkeyVNPT, bn.MaHD } into kq
                                    select new
                                    {
                                        kq.Key.TenBNhan,
                                        kq.Key.Tuoi,
                                        kq.Key.DChi,
                                        kq.Key.MaBNhan,
                                        kq.Key.NgayTT,
                                        kq.Key.DTBN1,
                                        #region mẫu chi tiết 30005
                                        CDHA = kq.Where(p => idNhomCDHA.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        TN1 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom1.Value.ToString()).Sum(p => p.TienBN),
                                        TN2 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom2.Value.ToString()).Sum(p => p.TienBN),
                                        TN3 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom3.Value.ToString()).Sum(p => p.TienBN),
                                        TN4 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom4.Value.ToString()).Sum(p => p.TienBN),
                                        TN5 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom5.Value.ToString()).Sum(p => p.TienBN),
                                        TN6 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom6.Value.ToString()).Sum(p => p.TienBN),
                                        TN7 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom7.Value.ToString().ToString()).Sum(p => p.TienBN),
                                        TN8 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom8.Value.ToString()).Sum(p => p.TienBN),
                                        TN9 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom9.Value.ToString()).Sum(p => p.TienBN),
                                        TN10 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom10.Value.ToString()).Sum(p => p.TienBN),
                                        TN11 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom11.Value.ToString()).Sum(p => p.TienBN),
                                        TN12 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom12.Value.ToString()).Sum(p => p.TienBN),
                                        TN13 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom13.Value.ToString()).Sum(p => p.TienBN),
                                        TN14 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom14.Value.ToString()).Sum(p => p.TienBN),
                                        TN15 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom15.Value.ToString()).Sum(p => p.TienBN),
                                        TN16 = kq.Where(p => p.TenRG == rep30005A31.TieuNhom16.Value.ToString()).Sum(p => p.TienBN),

                                        TN17 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom1.Value.ToString()).Sum(p => p.TienBN),
                                        TN18 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom2.Value.ToString()).Sum(p => p.TienBN),
                                        TN19 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom3.Value.ToString()).Sum(p => p.TienBN),
                                        TN20 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom4.Value.ToString()).Sum(p => p.TienBN),
                                        TN21 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom5.Value.ToString()).Sum(p => p.TienBN),
                                        TN22 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom6.Value.ToString()).Sum(p => p.TienBN),
                                        TN23 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom7.Value.ToString()).Sum(p => p.TienBN),
                                        TN24 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom8.Value.ToString()).Sum(p => p.TienBN),
                                        TN25 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom9.Value.ToString()).Sum(p => p.TienBN),
                                        TN26 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom10.Value.ToString()).Sum(p => p.TienBN),
                                        TN27 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom11.Value.ToString()).Sum(p => p.TienBN),
                                        TN28 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom12.Value.ToString()).Sum(p => p.TienBN),
                                        TN29 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom13.Value.ToString()).Sum(p => p.TienBN),
                                        TN30 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom14.Value.ToString()).Sum(p => p.TienBN),
                                        TN31 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom15.Value.ToString()).Sum(p => p.TienBN),
                                        TN32 = kq.Where(p => p.TenRG == rep30005A32.TieuNhom16.Value.ToString()).Sum(p => p.TienBN),
                                        #endregion
                                        XN = kq.Where(p => idNhomXN.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        TDCN = kq.Where(p => idNhomTDCN.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        SieuAm = kq.Where(p => idSieuam.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        SieuAmDL = kq.Where(p => idSieuamDL.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        DienTim = kq.Where(p => idDientim.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        ChupCT = kq.Where(p => idChupCT.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        DoCNHH = kq.Where(p => idDoCNHH.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        ThuThuatPT = kq.Where(p => idThuThuat.Contains(p.IdTieuNhom)).Where(p => !idNoiSoi.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        NoiSoi = kq.Where(p => idNoiSoi.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        Congkham = kq.Where(p => idCongkham.Contains(p.IdTieuNhom)).Where(p => p.DTBN1 != "KSK").Sum(p => p.TienBN),
                                        //KSK = (radBenhVien.SelectedIndex == 4 && kq.Key.DTBN1 == "KSK") ? kq.Where(p => idCongkham.Contains(p.IdTieuNhom)).Sum(p => p.TienBN) : 0,
                                        KSK = (kq.Key.DTBN1 == "KSK") ? kq.Where(p => idCongkham.Contains(p.IdTieuNhom)).Sum(p => p.TienBN) : 0,// dungtt sửa 07032019-his2950
                                        TienVC = kq.Where(p => idtienvc.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        XQ = kq.Where(p => idXQ.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        DoLoangXuong = kq.Where(p => idDoLoangXuong.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        LuuHuyetNao = kq.Where(p => idLuuHuyetNao.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        DienNaoDo = kq.Where(p => idDienNaoDo.Contains(p.IdTieuNhom)).Sum(p => p.TienBN),
                                        TienThuoc = (double)0,
                                        TienVTYT = (double)0,
                                        TienBN = kq.Sum(p => p.TienBN),
                                        TienBNDV = kq.Where(p => p.DTBN1 == "DỊCH VỤ").Sum(p => p.TienBN),
                                        XuatHD = kq.Key.FkeyVNPT == null && kq.Key.MaHD == null ? 0 : 1
                                    }).OrderBy(p => p.NgayTT).ToList();//.Where(p=>p.Tong != 0).ToList();
                    }
                    #endregion 30005

                    List<VPHangNgay> qTamung4 = (from bn in qTamung3
                                                 group bn by new { bn.TenBNhan, bn.Tuoi, bn.DChi, bn.MaBNhan, bn.NgayTT, bn.DTBN1, bn.XuatHD } into kq
                                                 select new VPHangNgay
                                                 {
                                                     TenBNhan = kq.Key.TenBNhan,
                                                     Tuoi = kq.Key.Tuoi ?? 0,
                                                     DChi = kq.Key.DChi,
                                                     MaBNhan = kq.Key.MaBNhan,
                                                     DTuong = kq.Key.DTBN1,
                                                     NgayTT = kq.Key.NgayTT,
                                                     XN = kq.Sum(p => p.XN),
                                                     TDCN = kq.Sum(p => p.TDCN),
                                                     #region dùng mẫu chi tiết
                                                     CDHA = kq.Sum(p => p.CDHA),
                                                     TienTN1 = kq.Sum(p => p.TN1),
                                                     TienTN2 = kq.Sum(p => p.TN2),
                                                     TienTN3 = kq.Sum(p => p.TN3),
                                                     TienTN4 = kq.Sum(p => p.TN4),
                                                     TienTN5 = kq.Sum(p => p.TN5),
                                                     TienTN6 = kq.Sum(p => p.TN6),
                                                     TienTN7 = kq.Sum(p => p.TN7),
                                                     TienTN8 = kq.Sum(p => p.TN8),
                                                     TienTN9 = kq.Sum(p => p.TN9),
                                                     TienTN10 = kq.Sum(p => p.TN10),
                                                     TienTN11 = kq.Sum(p => p.TN11),
                                                     TienTN12 = kq.Sum(p => p.TN12),
                                                     TienTN13 = kq.Sum(p => p.TN13),
                                                     TienTN14 = kq.Sum(p => p.TN14),
                                                     TienTN15 = kq.Sum(p => p.TN15),
                                                     TienTN16 = kq.Sum(p => p.TN16),

                                                     TienTN17 = kq.Sum(p => p.TN17),
                                                     TienTN18 = kq.Sum(p => p.TN18),
                                                     TienTN19 = kq.Sum(p => p.TN19),
                                                     TienTN20 = kq.Sum(p => p.TN20),
                                                     TienTN21 = kq.Sum(p => p.TN21),
                                                     TienTN22 = kq.Sum(p => p.TN22),
                                                     TienTN23 = kq.Sum(p => p.TN23),
                                                     TienTN24 = kq.Sum(p => p.TN24),
                                                     TienTN25 = kq.Sum(p => p.TN25),
                                                     TienTN26 = kq.Sum(p => p.TN26),
                                                     TienTN27 = kq.Sum(p => p.TN27),
                                                     TienTN28 = kq.Sum(p => p.TN28),
                                                     TienTN29 = kq.Sum(p => p.TN29),
                                                     TienTN30 = kq.Sum(p => p.TN30),
                                                     TienTN31 = kq.Sum(p => p.TN31),
                                                     TienTN32 = kq.Sum(p => p.TN32),
                                                     #endregion
                                                     XQ = radBenhVien.SelectedIndex == 0 && DungChung.Bien.MaBV != "30372" ? (kq.Sum(p => p.XQ) + kq.Sum(p => p.ChupCT)) : kq.Sum(p => p.XQ),
                                                     SieuAm = radBenhVien.SelectedIndex == 0 && DungChung.Bien.MaBV != "30372" ? (kq.Sum(p => p.SieuAm) + kq.Sum(p => p.SieuAmDL)) : kq.Sum(p => p.SieuAm),
                                                     SieuAmDL = kq.Sum(p => p.SieuAmDL),
                                                     ChupCT = kq.Sum(p => p.ChupCT),
                                                     DoCNHH = kq.Sum(p => p.DoCNHH),
                                                     DienTim = kq.Sum(p => p.DienTim),
                                                     ThuThuatPT = kq.Sum(p => p.ThuThuatPT),
                                                     NoiSoi = kq.Sum(p => p.NoiSoi),
                                                     Congkham = kq.Sum(p => p.Congkham),
                                                     KSK = kq.Sum(p => p.KSK),
                                                     TienVC = kq.Sum(p => p.TienVC),
                                                     DoLoangXuong = kq.Sum(p => p.DoLoangXuong),
                                                     LuuHuyetNao = kq.Sum(p => p.LuuHuyetNao),
                                                     DienNaoDo = kq.Sum(p => p.DienNaoDo),
                                                     TienBN = kq.Sum(p => p.TienBN),
                                                     TienBH = 0,
                                                     TienThuoc = kq.Sum(p => p.TienThuoc),
                                                     TienVTYT = kq.Sum(p => p.TienVTYT),
                                                     TienBNDV = kq.Sum(p => p.TienBNDV),
                                                     XuatHD = kq.Key.XuatHD,
                                                     //ThanhTien = radBenhVien.SelectedIndex == 6 ? 0 : kq.Sum(p => p.TienBN),
                                                     Tong = ktraTachCongKhamKSK && kq.Key.DTBN1 == "KSK" ? 0 : kq.Sum(p => p.XN + p.TDCN + p.SieuAm + p.SieuAmDL + p.DienTim + p.ThuThuatPT + p.NoiSoi + p.Congkham + p.TienVC + p.XQ + p.ChupCT + p.DoLoangXuong + p.LuuHuyetNao + (radBenhVien.SelectedIndex == 2 ? 0 : p.DienNaoDo) + ((radBenhVien.SelectedIndex == 2 && kq.Key.DTBN1 == "KSK") ? p.KSK : 0) + p.TienThuoc + p.TienVTYT),
                                                 }).OrderBy(p => p.NgayTT).ToList();//.Where(p=>p.Tong != 0).ToList();
                    var bnkhongtamung = (from a in q1.Where(p => (radBenhVien.SelectedIndex == 4) ? false : true)
                                         join b in q2 on a.MaBNhan equals b.MaBNhan
                                         join c in qdv on b.MaDV equals c.MaDV
                                         group new { a, b, c } by new { a.TenBNhan, a.DTuong, a.Tuoi, a.DChi, a.MaBNhan, NgayTT = (rdNgay.SelectedIndex == 1 && radBenhVien.SelectedIndex == 3) ? b.NgayDuyet : b.NgayTT } into kq
                                         select new VPHangNgay
                                         {
                                             TenBNhan = kq.Key.TenBNhan,
                                             Tuoi = kq.Key.Tuoi ?? 0,
                                             DChi = kq.Key.DChi,
                                             MaBNhan = kq.Key.MaBNhan,
                                             DTuong = kq.Key.DTuong,
                                             #region dùng mẫu chi tiết
                                             CDHA = kq.Where(p => idNhomCDHA.Contains(p.c.IdTieuNhom)).Sum(p => p.b.TienBN),
                                             //TienTN1 = kq.Where(p => p.c.TenRG.Equals(rep30005.TieuNhom1.Value)).Sum(p => p.b.TienBN),
                                             TienTN1 = kq.Where(p => p.c.TenRG == rep30005A31.TieuNhom1.Value.ToString()).Sum(p => p.b.TienBN),
                                             TienTN2 = kq.Where(p => p.c.TenRG == rep30005A31.TieuNhom2.Value.ToString()).Sum(p => p.b.TienBN),
                                             TienTN3 = kq.Where(p => p.c.TenRG == rep30005A31.TieuNhom3.Value.ToString()).Sum(p => p.b.TienBN),
                                             TienTN4 = kq.Where(p => p.c.TenRG == rep30005A31.TieuNhom4.Value.ToString()).Sum(p => p.b.TienBN),
                                             TienTN5 = kq.Where(p => p.c.TenRG == rep30005A31.TieuNhom5.Value.ToString()).Sum(p => p.b.TienBN),
                                             TienTN6 = kq.Where(p => p.c.TenRG == rep30005A31.TieuNhom6.Value.ToString()).Sum(p => p.b.TienBN),
                                             TienTN7 = kq.Where(p => p.c.TenRG == rep30005A31.TieuNhom7.Value.ToString()).Sum(p => p.b.TienBN),
                                             TienTN8 = kq.Where(p => p.c.TenRG == rep30005A31.TieuNhom8.Value.ToString()).Sum(p => p.b.TienBN),
                                             TienTN9 = kq.Where(p => p.c.TenRG == rep30005A31.TieuNhom9.Value.ToString()).Sum(p => p.b.TienBN),
                                             TienTN10 = kq.Where(p => p.c.TenRG == rep30005A31.TieuNhom10.Value.ToString()).Sum(p => p.b.TienBN),
                                             TienTN11 = kq.Where(p => p.c.TenRG == rep30005A31.TieuNhom11.Value.ToString()).Sum(p => p.b.TienBN),
                                             TienTN12 = kq.Where(p => p.c.TenRG == rep30005A31.TieuNhom12.Value.ToString()).Sum(p => p.b.TienBN),
                                             TienTN13 = kq.Where(p => p.c.TenRG == rep30005A31.TieuNhom13.Value.ToString()).Sum(p => p.b.TienBN),
                                             TienTN14 = kq.Where(p => p.c.TenRG == rep30005A31.TieuNhom14.Value.ToString()).Sum(p => p.b.TienBN),
                                             TienTN15 = kq.Where(p => p.c.TenRG == rep30005A31.TieuNhom15.Value.ToString()).Sum(p => p.b.TienBN),
                                             TienTN16 = kq.Where(p => p.c.TenRG == rep30005A31.TieuNhom16.Value.ToString()).Sum(p => p.b.TienBN),

                                             TienTN17 = kq.Where(p => p.c.TenRG == rep30005A32.TieuNhom1.Value.ToString()).Sum(p => p.b.TienBN),
                                             TienTN18 = kq.Where(p => p.c.TenRG == rep30005A32.TieuNhom2.Value.ToString()).Sum(p => p.b.TienBN),
                                             TienTN19 = kq.Where(p => p.c.TenRG == rep30005A32.TieuNhom3.Value.ToString()).Sum(p => p.b.TienBN),
                                             TienTN20 = kq.Where(p => p.c.TenRG == rep30005A32.TieuNhom4.Value.ToString()).Sum(p => p.b.TienBN),
                                             TienTN21 = kq.Where(p => p.c.TenRG == rep30005A32.TieuNhom5.Value.ToString()).Sum(p => p.b.TienBN),
                                             TienTN22 = kq.Where(p => p.c.TenRG == rep30005A32.TieuNhom6.Value.ToString()).Sum(p => p.b.TienBN),
                                             TienTN23 = kq.Where(p => p.c.TenRG == rep30005A32.TieuNhom7.Value.ToString()).Sum(p => p.b.TienBN),
                                             TienTN24 = kq.Where(p => p.c.TenRG == rep30005A32.TieuNhom8.Value.ToString()).Sum(p => p.b.TienBN),
                                             TienTN25 = kq.Where(p => p.c.TenRG == rep30005A32.TieuNhom9.Value.ToString()).Sum(p => p.b.TienBN),
                                             TienTN26 = kq.Where(p => p.c.TenRG == rep30005A32.TieuNhom10.Value.ToString()).Sum(p => p.b.TienBN),
                                             TienTN27 = kq.Where(p => p.c.TenRG == rep30005A32.TieuNhom11.Value.ToString()).Sum(p => p.b.TienBN),
                                             TienTN28 = kq.Where(p => p.c.TenRG == rep30005A32.TieuNhom12.Value.ToString()).Sum(p => p.b.TienBN),
                                             TienTN29 = kq.Where(p => p.c.TenRG == rep30005A32.TieuNhom13.Value.ToString()).Sum(p => p.b.TienBN),
                                             TienTN30 = kq.Where(p => p.c.TenRG == rep30005A32.TieuNhom14.Value.ToString()).Sum(p => p.b.TienBN),
                                             TienTN31 = kq.Where(p => p.c.TenRG == rep30005A32.TieuNhom15.Value.ToString()).Sum(p => p.b.TienBN),
                                             TienTN32 = kq.Where(p => p.c.TenRG == rep30005A32.TieuNhom16.Value.ToString()).Sum(p => p.b.TienBN),
                                             #endregion
                                             NgayTT = kq.Key.NgayTT.Value.Date,
                                             ChupCT = kq.Where(p => idChupCT.Contains(p.c.IdTieuNhom)).Where(p => (radBenhVien.SelectedIndex == 3) ? (p.a.DTuong == "BHYT" ? p.b.TrongBH == 0 : false) : true).Sum(p => p.b.TienBN),
                                             DoCNHH = kq.Where(p => idDoCNHH.Contains(p.c.IdTieuNhom)).Where(p => (radBenhVien.SelectedIndex == 3) ? (p.a.DTuong == "BHYT" ? p.b.TrongBH == 0 : false) : true).Sum(p => p.b.TienBN),
                                             XN = kq.Where(p => idNhomXN.Contains(p.c.IdTieuNhom)).Where(p => (radBenhVien.SelectedIndex == 3) ? (p.a.DTuong == "BHYT" ? p.b.TrongBH == 0 : false) : true).Sum(p => p.b.TienBN),
                                             TDCN = kq.Where(p => idNhomTDCN.Contains(p.c.IdTieuNhom)).Where(p => !idDientim.Contains(p.c.IdTieuNhom)).Where(p => (radBenhVien.SelectedIndex == 3) ? (p.a.DTuong == "BHYT" ? p.b.TrongBH == 0 : false) : true).Sum(p => p.b.TienBN),
                                             SieuAm = kq.Where(p => idSieuam.Contains(p.c.IdTieuNhom)).Where(p => (radBenhVien.SelectedIndex == 3) ? (p.a.DTuong == "BHYT" ? p.b.TrongBH == 0 : false) : true).Sum(p => p.b.TienBN),
                                             SieuAmDL = kq.Where(p => idSieuamDL.Contains(p.c.IdTieuNhom)).Where(p => (radBenhVien.SelectedIndex == 3) ? (p.a.DTuong == "BHYT" ? p.b.TrongBH == 0 : false) : true).Sum(p => p.b.TienBN),
                                             DienTim = kq.Where(p => idDientim.Contains(p.c.IdTieuNhom)).Where(p => (radBenhVien.SelectedIndex == 3) ? (p.a.DTuong == "BHYT" ? p.b.TrongBH == 0 : false) : true).Sum(p => p.b.TienBN),
                                             ThuThuatPT = kq.Where(p => idThuThuat.Contains(p.c.IdTieuNhom)).Where(p => !idNoiSoi.Contains(p.c.IdTieuNhom)).Where(p => (radBenhVien.SelectedIndex == 3) ? (p.a.DTuong == "BHYT" ? p.b.TrongBH == 0 : false) : true).Sum(p => p.b.TienBN),
                                             NoiSoi = kq.Where(p => idNoiSoi.Contains(p.c.IdTieuNhom)).Where(p => (radBenhVien.SelectedIndex == 3) ? (p.a.DTuong == "BHYT" ? p.b.TrongBH == 0 : false) : true).Sum(p => p.b.TienBN),
                                             Congkham = kq.Where(p => idCongkham.Contains(p.c.IdTieuNhom)).Where(p => (radBenhVien.SelectedIndex == 3) ? (p.a.DTuong == "BHYT" ? p.b.TrongBH == 0 : false) : true).Sum(p => p.b.TienBN),
                                             TienVC = kq.Where(p => idtienvc.Contains(p.c.IdTieuNhom)).Where(p => (radBenhVien.SelectedIndex == 3) ? (p.a.DTuong == "BHYT" ? p.b.TrongBH == 0 : false) : true).Sum(p => p.b.TienBN),
                                             XQ = kq.Where(p => idXQ.Contains(p.c.IdTieuNhom)).Where(p => (radBenhVien.SelectedIndex == 3) ? (p.a.DTuong == "BHYT" ? p.b.TrongBH == 0 : false) : true).Sum(p => p.b.TienBN),
                                             DoLoangXuong = kq.Where(p => idDoLoangXuong.Contains(p.c.IdTieuNhom)).Where(p => (radBenhVien.SelectedIndex == 3) ? (p.a.DTuong == "BHYT" ? p.b.TrongBH == 0 : false) : true).Sum(p => p.b.TienBN),
                                             LuuHuyetNao = kq.Where(p => idLuuHuyetNao.Contains(p.c.IdTieuNhom)).Where(p => (radBenhVien.SelectedIndex == 3) ? (p.a.DTuong == "BHYT" ? p.b.TrongBH == 0 : false) : true).Sum(p => p.b.TienBN),
                                             DienNaoDo = kq.Where(p => idDienNaoDo.Contains(p.c.IdTieuNhom)).Where(p => (radBenhVien.SelectedIndex == 3) ? (p.a.DTuong == "BHYT" ? p.b.TrongBH == 0 : false) : true).Sum(p => p.b.TienBN),
                                             TienBN = kq.Sum(p => p.b.TienBN),
                                             //TienBH = kq.Sum(p => p.b.TienBH),
                                             //ThanhTien = kq.Sum(p => p.b.ThanhTien)
                                         }).ToList();
                    List<VPHangNgay> qbnkhongtamung = (from n in bnkhongtamung
                                                       group n by new { n.TenBNhan, n.Tuoi, n.DChi, n.MaBNhan, n.NgayTT, n.DTuong } into kq
                                                       select new VPHangNgay
                                                       {
                                                           TenBNhan = kq.Key.TenBNhan,
                                                           Tuoi = kq.Key.Tuoi,
                                                           DChi = kq.Key.DChi,
                                                           MaBNhan = kq.Key.MaBNhan,
                                                           DTuong = kq.Key.DTuong,
                                                           NgayTT = kq.Key.NgayTT,
                                                           #region dùng mẫu chi tiết
                                                           CDHA = kq.Sum(p => p.CDHA),
                                                           TienTN1 = kq.Sum(p => p.TienTN1),
                                                           TienTN2 = kq.Sum(p => p.TienTN2),
                                                           TienTN3 = kq.Sum(p => p.TienTN3),
                                                           TienTN4 = kq.Sum(p => p.TienTN4),
                                                           TienTN5 = kq.Sum(p => p.TienTN5),
                                                           TienTN6 = kq.Sum(p => p.TienTN6),
                                                           TienTN7 = kq.Sum(p => p.TienTN7),
                                                           TienTN8 = kq.Sum(p => p.TienTN8),
                                                           TienTN9 = kq.Sum(p => p.TienTN9),
                                                           TienTN10 = kq.Sum(p => p.TienTN10),
                                                           TienTN11 = kq.Sum(p => p.TienTN11),
                                                           TienTN12 = kq.Sum(p => p.TienTN12),
                                                           TienTN13 = kq.Sum(p => p.TienTN13),
                                                           TienTN14 = kq.Sum(p => p.TienTN14),
                                                           TienTN15 = kq.Sum(p => p.TienTN15),
                                                           TienTN16 = kq.Sum(p => p.TienTN16),

                                                           TienTN17 = kq.Sum(p => p.TienTN17),
                                                           TienTN18 = kq.Sum(p => p.TienTN18),
                                                           TienTN19 = kq.Sum(p => p.TienTN19),
                                                           TienTN20 = kq.Sum(p => p.TienTN20),
                                                           TienTN21 = kq.Sum(p => p.TienTN21),
                                                           TienTN22 = kq.Sum(p => p.TienTN22),
                                                           TienTN23 = kq.Sum(p => p.TienTN23),
                                                           TienTN24 = kq.Sum(p => p.TienTN24),
                                                           TienTN25 = kq.Sum(p => p.TienTN25),
                                                           TienTN26 = kq.Sum(p => p.TienTN26),
                                                           TienTN27 = kq.Sum(p => p.TienTN27),
                                                           TienTN28 = kq.Sum(p => p.TienTN28),
                                                           TienTN29 = kq.Sum(p => p.TienTN29),
                                                           TienTN30 = kq.Sum(p => p.TienTN30),
                                                           TienTN31 = kq.Sum(p => p.TienTN31),
                                                           TienTN32 = kq.Sum(p => p.TienTN32),
                                                           #endregion
                                                           XN = kq.Sum(p => p.XN),
                                                           TDCN = kq.Sum(p => p.TDCN),
                                                           ChupCT = kq.Sum(p => p.ChupCT),
                                                           DoCNHH = kq.Sum(p => p.DoCNHH),
                                                           SieuAmDL = kq.Sum(p => p.SieuAmDL),
                                                           DienTim = kq.Sum(p => p.DienTim),
                                                           ThuThuatPT = kq.Sum(p => p.ThuThuatPT),
                                                           NoiSoi = kq.Sum(p => p.NoiSoi),
                                                           Congkham = kq.Sum(p => p.Congkham),
                                                           TienVC = kq.Sum(p => p.TienVC),
                                                           XQ = radBenhVien.SelectedIndex == 0 && DungChung.Bien.MaBV != "30372" ? (kq.Sum(p => p.XQ) + kq.Sum(p => p.ChupCT)) : kq.Sum(p => p.XQ),
                                                           SieuAm = radBenhVien.SelectedIndex == 0 && DungChung.Bien.MaBV != "30372" ? (kq.Sum(p => p.SieuAm) + kq.Sum(p => p.SieuAmDL)) : kq.Sum(p => p.SieuAm),
                                                           DoLoangXuong = kq.Sum(p => p.DoLoangXuong),
                                                           LuuHuyetNao = kq.Sum(p => p.LuuHuyetNao),
                                                           DienNaoDo = kq.Sum(p => p.DienNaoDo),
                                                           TienBN = kq.Sum(p => p.TienBN),
                                                           //TienBH = kq.Sum(p => p.TienBH),
                                                           //ThanhTien = kq.Sum(p => p.ThanhTien),
                                                           Tong = (radBenhVien.SelectedIndex == 2) ? 0 : kq.Sum(p => p.XN + p.TDCN + p.SieuAm + p.SieuAmDL + p.DienTim + p.ThuThuatPT + p.NoiSoi + p.Congkham + p.TienVC + p.XQ + p.ChupCT + p.DoLoangXuong + p.LuuHuyetNao + p.DienNaoDo)
                                                       }).ToList();
                    //viện phí (đã trừ thu thẳng và thu thẳng)
                    List<VPHangNgay> q6 = new List<VPHangNgay>();

                    q6.AddRange(qTamung4);
                    //if (DungChung.Bien.MaBV != "30002")
                    //{
                    q6.AddRange(q5);
                    //}
                    if (radBenhVien.SelectedIndex != 2)
                        q6.AddRange(qbnkhongtamung);

                    #endregion
                    #region Viện phí bệnh nhân KSK




                    var qksk1 = (from a in q1.Where(p => (radBenhVien.SelectedIndex == 4) ? false : p.DTBN1 == "KSK")
                                 join tu in data.TamUngs.Where(p => p.IDGoiDV <= 0 || p.IDGoiDV == null).Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay).Where(p => radBenhVien.SelectedIndex == 2 ? p.PhanLoai != 3 : true).Where(p => makp == 0 || p.MaKP == makp).Where(p => macbthu == "" ? true : p.MaCB == macbthu).Where(p => gioHC == 2 || (gioHC == 1 && p.NgoaiGio == 1) || (gioHC == 0 && (p.NgoaiGio == null || p.NgoaiGio != 1))) on a.MaBNhan equals tu.MaBNhan
                                 select new
                                 {
                                     NgayThu = tu.NgayThu.Value.Date,
                                     a.MaBNhan,
                                     a.TenBNhan,
                                     a.Tuoi,
                                     a.DChi,
                                     a.NoiTru,
                                     a.DTBN1,
                                     tu.PhanLoai,
                                     SoTien = tu.SoTien ?? 0.00,
                                     tu.TienChenh,
                                     ThanhTien = (tu.PhanLoai == 0 || tu.PhanLoai == 1 || tu.PhanLoai == 3) ? tu.SoTien : (tu.PhanLoai == 4 ? -tu.SoTien : (tu.PhanLoai == 2 ? tu.TienChenh : 0))
                                 }).ToList();
                    if (DungChung.Bien.MaBV == "30372")
                    {
                        if (radBenhVien.SelectedIndex == 0)
                        {
                            qksk1 = (from a in q1.Where(p => (radBenhVien.SelectedIndex == 4) ? false : p.DTBN1 == "KSK")
                                     join tu in data.TamUngs.Where(p => p.IDGoiDV <= 0 || p.IDGoiDV == null).Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay).Where(p => radBenhVien.SelectedIndex == 2 ? p.PhanLoai != 3 : true).Where(p => makp == 0 || p.MaKP == makp).Where(p => macbTT == "" ? true : p.MaCB == macbTT).Where(p => gioHC == 2 || (gioHC == 1 && p.NgoaiGio == 1) || (gioHC == 0 && (p.NgoaiGio == null || p.NgoaiGio != 1))) on a.MaBNhan equals tu.MaBNhan
                                     select new
                                     {
                                         NgayThu = tu.NgayThu.Value.Date,
                                         a.MaBNhan,
                                         a.TenBNhan,
                                         a.Tuoi,
                                         a.DChi,
                                         a.NoiTru,
                                         a.DTBN1,
                                         tu.PhanLoai,
                                         SoTien = tu.SoTien ?? 0.00,
                                         tu.TienChenh,
                                         ThanhTien = (tu.PhanLoai == 0 || tu.PhanLoai == 1 || tu.PhanLoai == 3) ? tu.SoTien : (tu.PhanLoai == 4 ? -tu.SoTien : (tu.PhanLoai == 2 ? tu.TienChenh : 0))
                                     }).ToList();
                        }
                    }
                    //if (radBenhVien.SelectedIndex == 2)
                    //{
                    //    qksk1 = (from a in q1.Where(p => p.DTBN1 == "KSK")
                    //             join tu in data.TamUngs.Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay).Where(p => makp == 0 || p.MaKP == makp).Where(p => gioHC == 2 || (gioHC == 1 && p.NgoaiGio == 1) || (gioHC == 0 && (p.NgoaiGio == null || p.NgoaiGio != 1))) on a.MaBNhan equals tu.MaBNhan
                    //             join tuct in data.TamUngcts on tu.IDTamUng equals tuct.IDTamUng
                    //             join b in qdv.Where(p => p.TenRG == "Tiền công khám") on tuct.MaDV equals b.MaDV
                    //             select new
                    //             {
                    //                 NgayThu = tu.NgayThu.Value.Date,
                    //                 a.MaBNhan,
                    //                 a.TenBNhan,
                    //                 a.Tuoi,
                    //                 a.DChi,
                    //                 a.NoiTru,
                    //                 a.DTBN1,
                    //                 tu.PhanLoai,
                    //                 SoTien = tuct.TienBN,
                    //                 tu.TienChenh,
                    //                 ThanhTien = (tu.PhanLoai == 0 || tu.PhanLoai == 1 || tu.PhanLoai == 3) ? tu.SoTien : (tu.PhanLoai == 4 ? -tu.SoTien : (tu.PhanLoai == 2 ? tu.TienChenh : 0))
                    //             }).ToList();
                    //}


                    List<VPHangNgay> qksk2 = (from a in qksk1
                                              group a by new
                                              {
                                                  a.MaBNhan,
                                                  a.TenBNhan,
                                                  a.Tuoi,
                                                  a.DChi,
                                                  a.NoiTru,
                                                  a.DTBN1,
                                                  a.NgayThu
                                              } into kq
                                              select new VPHangNgay
                                              {
                                                  MaBNhan = kq.Key.MaBNhan,
                                                  TenBNhan = kq.Key.TenBNhan,
                                                  DTuong = kq.Key.DTBN1,
                                                  Tuoi = kq.Key.Tuoi ?? 0,
                                                  DChi = kq.Key.DChi,
                                                  KSK = kq.Sum(p => p.SoTien),
                                                  NgayTT = kq.Key.NgayThu,
                                                  TienBN = kq.Sum(p => p.SoTien),
                                                  TienBH = 0, //ThanhTien = kq.Sum(p => p.SoTien), 
                                                  Tong = kq.Sum(p => p.SoTien)
                                              }).ToList();
                    if (ktraTachCongKhamKSK)
                    {
                        qksk2 = (from a in qksk2
                                 join b in qTamung4 on a.MaBNhan equals b.MaBNhan into kq
                                 from kq1 in kq.DefaultIfEmpty()
                                 select new VPHangNgay
                                          {
                                              MaBNhan = a.MaBNhan,
                                              TenBNhan = a.TenBNhan,
                                              DTuong = a.DTuong,
                                              Tuoi = a.Tuoi,
                                              DChi = a.DChi,
                                              KSK = a.KSK - (kq1 == null ? 0 : kq1.Congkham),
                                              NgayTT = a.NgayTT,
                                              TienBN = a.TienBN,
                                              TienBH = a.TienBH, //ThanhTien = kq.Sum(p => p.SoTien), 
                                              Tong = a.Tong
                                          }).ToList();
                    }

                    #endregion
                    if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                    {
                        var q123455 = (from tu in data.TamUngs.Where(p => p.IDGoiDV > 0 && p.NgayThu >= tungay && p.NgayThu <= denngay)//.Where(p => XuatHD == 2 ? true : (XuatHD == 0 ? (p.FkeyVNPT == null && p.MaHD == null) : (p.FkeyVNPT != null || p.MaHD != null)))

                                       join bn in data.BenhNhans on tu.MaBNhan equals bn.MaBNhan
                                       select new
                                       {
                                           TenBNhan = bn.TenBNhan,
                                           Tuoi = bn.Tuoi ?? 0,
                                           DChi = bn.DChi,
                                           MaBNhan = bn.MaBNhan,
                                           DTuong = bn.DTuong,
                                           NgayThu = tu.NgayThu,
                                           KSK = tu.SoTien,
                                           DuyetPhieuThu = tu.DuyetPhieuThu ?? false
                                       }).ToList();
                        var q12345 = q123455.Where(p => (((DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && radBenhVien.SelectedIndex == 0) ? (rdDuyetPhieu.SelectedIndex == 2 ? true : (rdDuyetPhieu.SelectedIndex == 0 ? p.DuyetPhieuThu : !p.DuyetPhieuThu)) : true)).ToList();

                        var q123456 = (from tu in q12345

                                       group tu by new { tu.TenBNhan, tu.Tuoi, tu.DChi, tu.MaBNhan, tu.DTuong, NgayThu = tu.NgayThu.Value.Date } into kq
                                       select new VPHangNgay
                                       {
                                           TenBNhan = kq.Key.TenBNhan,
                                           Tuoi = kq.Key.Tuoi,
                                           DChi = kq.Key.DChi,
                                           MaBNhan = kq.Key.MaBNhan,
                                           DTuong = kq.Key.DTuong.Trim().ToUpper(),
                                           NgayTT = kq.Key.NgayThu,
                                           KSK = kq.Sum(p => p.KSK ?? 0),
                                           Tong = kq.Sum(p => p.KSK ?? 0)
                                       }).ToList();
                        all.AddRange(q123456);
                    }

                    all.AddRange(q6);
                    all.AddRange(qksk2);

                    all = (from a in all
                           group a by new { a.TenBNhan, a.MaBNhan, a.DChi, a.Tuoi, a.NgayTT, a.DTuong } into kq
                           select new VPHangNgay
                           {
                               TenBNhan = kq.Key.TenBNhan,
                               Tuoi = kq.Key.Tuoi,
                               DChi = kq.Key.DChi,
                               MaBNhan = kq.Key.MaBNhan,
                               DTuong = kq.Key.DTuong,
                               NgayTT = kq.Key.NgayTT,
                               XN = kq.Sum(p => p.XN),
                               TDCN = kq.Sum(p => p.TDCN),
                               SieuAm = kq.Sum(p => p.SieuAm),
                               SieuAmDL = kq.Sum(p => p.SieuAmDL),
                               DienTim = kq.Sum(p => p.DienTim),
                               ChupCT = kq.Sum(p => p.ChupCT),
                               DoCNHH = kq.Sum(p => p.DoCNHH),
                               ThuThuatPT = kq.Sum(p => p.ThuThuatPT),
                               NoiSoi = kq.Sum(p => p.NoiSoi),
                               Congkham = kq.Sum(p => p.Congkham),
                               TienVC = kq.Sum(p => p.TienVC),
                               XQ = kq.Sum(p => p.XQ),
                               NgoaiTruBH = kq.Sum(p => p.NgoaiTruBH),
                               KSK = kq.Sum(p => p.KSK),
                               DoLoangXuong = kq.Sum(p => p.DoLoangXuong),
                               LuuHuyetNao = kq.Sum(p => p.LuuHuyetNao),
                               DienNaoDo = kq.Sum(p => p.DienNaoDo),
                               TienThuoc = kq.Sum(p => p.TienThuoc),
                               TienVTYT = kq.Sum(p => p.TienVTYT),
                               TienBN = kq.Sum(p => p.TienBN),
                               // TienBH = kq.Sum(p => p.TienBH),
                               // ThanhTien = kq.Sum(p => p.ThanhTien),
                               TienNGDM = kq.Sum(p => p.TienNGDM),
                               XuatHD = kq.Sum(p => p.XuatHD),
                               Tong = kq.Sum(p => p.Tong) // kq.Sum(p => p.XN + p.TDCN + p.SieuAm + p.DienTim + p.ThuThuatPT + p.NoiSoi + p.Congkham + p.TienVC + p.XQ + p.NgoaiTruBH + p.KSK + p.DoLoangXuong + p.LuuHuyetNao + p.TienThuoc + p.TienVTYT)
                           }).Where(p => p.Tong != 0).ToList();
                    //.Where(p => XuatHD == 2 ? true : (XuatHD == 0 ? (p.FkeyVNPT == null && p.MaHD == null) : (p.FkeyVNPT != null || p.MaHD != null)))
                    if (ckcBNTT.Checked)
                    {
                        var q12 = (from tu in data.TamUngs.Where(p => p.NgayThu >= tungay && p.NgayThu <= denngay).Where(p => p.PhanLoai == 1 && p.IDNhapD != null)
                                   join nd in data.NhapDs on tu.IDNhapD equals nd.IDNhap
                                   select new
                                   {
                                       TenBNhan = nd.TenNguoiCC,
                                       DChi = nd.DiaChi,
                                       NgayTT = tu.NgayThu.Value,
                                       TienThuoc = tu.SoTien ?? 0,
                                       TienBN = tu.SoTien ?? 0,
                                       // TienBH = kq.Sum(p => p.TienBH),
                                       Tong = tu.SoTien ?? 0,
                                       tu.FkeyVNPT,
                                       tu.MaHD
                                   }).ToList();
                        var q123 = (from tu in q12
                                    select new VPHangNgay
                                    {
                                        TenBNhan = tu.TenBNhan,
                                        DChi = tu.DChi,
                                        NgayTT = tu.NgayTT.Date,
                                        TienThuoc = tu.TienThuoc,
                                        TienBN = tu.TienBN,
                                        // TienBH = kq.Sum(p => p.TienBH),
                                        Tong = tu.Tong,
                                        XuatHD = tu.FkeyVNPT == null && tu.MaHD == null ? 0 : 1
                                    }).ToList();
                        all.AddRange(q123);
                        all = all.ToList();
                    }


                    if (all.Count > 0)
                    {
                        #region Thanh HÀ
                        if (radBenhVien.SelectedIndex == 0 || radBenhVien.SelectedIndex == 4)
                        {
                            string[] _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                            string[] _tieude = { "stt", "Họ tên", "Tuổi", "Địa chỉ", "Số phiếu", "Nhóm xét nghiệm", "Siêu âm", "Điện tim", "Thủ thuật - Phẫu thuật", "Nội soi", "KSK", "Công khám", "Tiền vận chuyển", "Chụp XQ", "Nhóm thăm dò chức năng", "Đo loãng xương", "Lưu huyết não", "Điện não đồ", "Tiền thuốc", "Tiền VTYT", "% BHYT ngoại trú", "Cộng" };
                            string _filePath = "D:\\BCVienPhiHangNgay.xls";
                            int[] _arrWidth = new int[] { };
                            DungChung.Bien.MangHaiChieu = new Object[all.Count + 1, 22];
                            for (int i = 0; i < 22; i++)
                            {
                                DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                            }
                            int num = 1;
                            foreach (var r in all)
                            {
                                DungChung.Bien.MangHaiChieu[num, 0] = num;
                                DungChung.Bien.MangHaiChieu[num, 1] = r.TenBNhan;
                                DungChung.Bien.MangHaiChieu[num, 2] = r.Tuoi;
                                DungChung.Bien.MangHaiChieu[num, 3] = r.DChi;
                                DungChung.Bien.MangHaiChieu[num, 4] = r.MaBNhan;
                                DungChung.Bien.MangHaiChieu[num, 5] = r.XN;
                                DungChung.Bien.MangHaiChieu[num, 6] = r.SieuAm;
                                DungChung.Bien.MangHaiChieu[num, 7] = r.DienTim;
                                DungChung.Bien.MangHaiChieu[num, 8] = r.ThuThuatPT;
                                DungChung.Bien.MangHaiChieu[num, 9] = r.NoiSoi;
                                DungChung.Bien.MangHaiChieu[num, 10] = r.KSK;
                                DungChung.Bien.MangHaiChieu[num, 11] = r.Congkham;
                                DungChung.Bien.MangHaiChieu[num, 12] = r.TienVC;
                                DungChung.Bien.MangHaiChieu[num, 13] = r.XQ;
                                DungChung.Bien.MangHaiChieu[num, 14] = r.TDCN;
                                DungChung.Bien.MangHaiChieu[num, 15] = r.DoLoangXuong;
                                DungChung.Bien.MangHaiChieu[num, 16] = r.LuuHuyetNao;
                                DungChung.Bien.MangHaiChieu[num, 17] = r.DienNaoDo;
                                DungChung.Bien.MangHaiChieu[num, 18] = r.TienThuoc;
                                DungChung.Bien.MangHaiChieu[num, 19] = r.TienVTYT;
                                DungChung.Bien.MangHaiChieu[num, 20] = r.NgoaiTruBH;
                                DungChung.Bien.MangHaiChieu[num, 21] = r.Tong;
                                num++;
                            }
                            if (DungChung.Bien.MaBV == "30372")
                            {
                                if (radBenhVien.SelectedIndex == 0)
                                {
                                    BaoCao.rep_VienPhiHangNgay_30372 rep = new BaoCao.rep_VienPhiHangNgay_30372(ckInTongHop.Checked);
                                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Sheet1", _filePath, true, this.Name);
                                    rep.celTitBH.Text = "% BHYT";
                                    rep.DataSource = all;
                                    rep.colCQ.Text = DungChung.Bien.TenCQ.ToUpper();
                                    rep.cel_diadanh.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                                    rep.celNgayThang.Text = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy");
                                    if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                                        rep.celNgayThang.Text = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy HH:mm") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy HH:mm");
                                    if (String.IsNullOrEmpty(txtTieude.Text))
                                    {
                                        rep.CelTieuDe.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN VIỆN PHÍ HẰNG NGÀY";
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
                            }
                            else
                            {
                                BaoCao.rep_VienPhiHangNgay_30009 rep = new BaoCao.rep_VienPhiHangNgay_30009(ckInTongHop.Checked);
                                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Sheet1", _filePath, true, this.Name);
                                rep.celTitBH.Text = "% BHYT";
                                rep.DataSource = all;
                                rep.colCQ.Text = DungChung.Bien.TenCQ.ToUpper();
                                rep.cel_diadanh.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                                rep.celNgayThang.Text = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy");
                                if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                                    rep.celNgayThang.Text = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy HH:mm") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy HH:mm");
                                if (String.IsNullOrEmpty(txtTieude.Text))
                                {
                                    rep.CelTieuDe.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN VIỆN PHÍ HẰNG NGÀY";
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
                        }
                        #endregion
                        else
                        {
                            #region mẫu chi tiết BVĐK Kinh Môn
                            if (radBenhVien.SelectedIndex == 2 & ckMauCT.Checked)
                            {
                                List<VPHangNgay> q30005 = new List<VPHangNgay>();
                                q30005.AddRange(q6);
                                q30005.AddRange(qksk2);
                                q30005 = (from a in q30005
                                          group a by new { a.TenBNhan, a.MaBNhan, a.DChi, a.Tuoi, a.NgayTT } into kq
                                          select new VPHangNgay
                                          {
                                              TenBNhan = kq.Key.TenBNhan,
                                              Tuoi = kq.Key.Tuoi,
                                              DChi = kq.Key.DChi,
                                              MaBNhan = kq.Key.MaBNhan,
                                              NgayTT = kq.Key.NgayTT,
                                              XN = kq.Sum(p => p.XN),
                                              TDCN = kq.Sum(p => p.TDCN),
                                              CDHA = kq.Sum(p => p.CDHA),
                                              TienTN1 = kq.Sum(p => p.TienTN1),
                                              TienTN2 = kq.Sum(p => p.TienTN2),
                                              TienTN3 = kq.Sum(p => p.TienTN3),
                                              TienTN4 = kq.Sum(p => p.TienTN4),
                                              TienTN5 = kq.Sum(p => p.TienTN5),
                                              TienTN6 = kq.Sum(p => p.TienTN6),
                                              TienTN7 = kq.Sum(p => p.TienTN7),
                                              TienTN8 = kq.Sum(p => p.TienTN8),
                                              TienTN9 = kq.Sum(p => p.TienTN9),
                                              TienTN10 = kq.Sum(p => p.TienTN10),
                                              TienTN11 = kq.Sum(p => p.TienTN11),
                                              TienTN12 = kq.Sum(p => p.TienTN12),
                                              TienTN13 = kq.Sum(p => p.TienTN13),
                                              TienTN14 = kq.Sum(p => p.TienTN14),
                                              TienTN15 = kq.Sum(p => p.TienTN15),
                                              TienTN16 = kq.Sum(p => p.TienTN16),

                                              TienTN17 = kq.Sum(p => p.TienTN17),
                                              TienTN18 = kq.Sum(p => p.TienTN18),
                                              TienTN19 = kq.Sum(p => p.TienTN19),
                                              TienTN20 = kq.Sum(p => p.TienTN20),
                                              TienTN21 = kq.Sum(p => p.TienTN21),
                                              TienTN22 = kq.Sum(p => p.TienTN22),
                                              TienTN23 = kq.Sum(p => p.TienTN23),
                                              TienTN24 = kq.Sum(p => p.TienTN24),
                                              TienTN25 = kq.Sum(p => p.TienTN25),
                                              TienTN26 = kq.Sum(p => p.TienTN26),
                                              TienTN27 = kq.Sum(p => p.TienTN27),
                                              TienTN28 = kq.Sum(p => p.TienTN28),
                                              TienTN29 = kq.Sum(p => p.TienTN29),
                                              TienTN30 = kq.Sum(p => p.TienTN30),
                                              TienTN31 = kq.Sum(p => p.TienTN31),
                                              TienTN32 = kq.Sum(p => p.TienTN32),
                                              Tong = kq.Sum(p => p.TienTN1 + p.TienTN2 + p.TienTN3 + p.TienTN4 + p.TienTN5 + p.TienTN6 + p.TienTN7 + p.TienTN8 + p.TienTN9 + p.TienTN10 + p.TienTN11 + p.TienTN12 + p.TienTN13 + p.TienTN14 + p.TienTN15
                                              + p.TienTN16 + p.TienTN17 + p.TienTN18 + p.TienTN19 + p.TienTN20 + p.TienTN21 + p.TienTN22 + p.TienTN23 + p.TienTN24 + p.TienTN25 + p.TienTN26 + p.TienTN27 + p.TienTN28 + p.TienTN29 + p.TienTN30 + p.TienTN31 + p.TienTN32)
                                          }).Where(p => p.Tong != 0).OrderBy(p => p.NgayTT).ThenBy(p => p.MaBNhan).ToList();
                                if (qtn30005.Count <= 9)
                                {
                                    #region Xuất Excel
                                    string[] _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                    string[] _tieude = { "Stt", "Họ tên", "Tuổi", "Địa chỉ", "Số phiếu", rep30005.TieuNhom1.Value.ToString(), rep30005.TieuNhom2.Value.ToString(), rep30005.TieuNhom3.Value.ToString(), rep30005.TieuNhom4.Value.ToString(), 
                                             rep30005.TieuNhom5.Value.ToString(), rep30005.TieuNhom6.Value.ToString(), rep30005.TieuNhom7.Value.ToString(), rep30005.TieuNhom8.Value.ToString(), rep30005.TieuNhom9.Value.ToString(), "Tổng" };
                                    string _filePath = "D:\\BCVienPhiHangNgay_MauCT.xls";
                                    int[] _arrWidth = new int[] { };
                                    DungChung.Bien.MangHaiChieu = new Object[q30005.Count + 1, 17];
                                    for (int i = 0; i < _tieude.Length; i++)
                                    {
                                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i].ToUpper();
                                    }
                                    int num = 1;
                                    foreach (var r in q30005)
                                    {
                                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                                        DungChung.Bien.MangHaiChieu[num, 1] = r.TenBNhan;
                                        DungChung.Bien.MangHaiChieu[num, 2] = r.Tuoi;
                                        DungChung.Bien.MangHaiChieu[num, 3] = r.DChi;
                                        DungChung.Bien.MangHaiChieu[num, 4] = r.MaBNhan;
                                        DungChung.Bien.MangHaiChieu[num, 5] = r.TienTN1;
                                        DungChung.Bien.MangHaiChieu[num, 6] = r.TienTN2;
                                        DungChung.Bien.MangHaiChieu[num, 7] = r.TienTN3;
                                        DungChung.Bien.MangHaiChieu[num, 8] = r.TienTN4;
                                        DungChung.Bien.MangHaiChieu[num, 9] = r.TienTN5;
                                        DungChung.Bien.MangHaiChieu[num, 10] = r.TienTN6;
                                        DungChung.Bien.MangHaiChieu[num, 11] = r.TienTN7;
                                        DungChung.Bien.MangHaiChieu[num, 12] = r.TienTN8;
                                        DungChung.Bien.MangHaiChieu[num, 13] = r.TienTN9;
                                        DungChung.Bien.MangHaiChieu[num, 14] = r.Tong;
                                        num++;
                                    }
                                    #endregion
                                    //QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "VienPhiHangNgay_MauCT_30005", "C:\\TsBCNXT_BVDKCL_30003.xls", true);

                                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Sheet1", "C:\\VienPhiHangNgay_MauCT.xls", true, this.Name);
                                    rep30005.DataSource = q30005;
                                    rep30005.cel_diadanh.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                                    rep30005.celNgayThang.Text = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy");
                                    rep30005.BindingData();
                                    rep30005.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep30005.PrintingSystem;
                                    frm.ShowDialog();
                                }
                                else if (qtn30005.Count <= 16)
                                {
                                    frmIn frm = new frmIn();
                                    rep30005A31.DataSource = q30005;
                                    rep30005A31.cel_diadanh.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                                    rep30005A31.celNgayThang.Text = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy");
                                    rep30005A31.BindingData();
                                    rep30005A31.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep30005A31.PrintingSystem;
                                    frm.ShowDialog();
                                }
                                else if (qtn30005.Count <= 32)
                                {
                                    frmIn frm = new frmIn();
                                    rep30005A31.DataSource = q30005;
                                    rep30005A31.cel_diadanh.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                                    rep30005A31.celNgayThang.Text = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy");
                                    rep30005A31.BindingData();
                                    rep30005A31.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep30005A31.PrintingSystem;
                                    frm.ShowDialog();

                                    rep30005A32.DataSource = q30005;
                                    rep30005A32.cel_diadanh.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                                    rep30005A32.celNgayThang.Text = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy");
                                    rep30005A32.BindingData();
                                    rep30005A32.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep30005A32.PrintingSystem;
                                    frm.ShowDialog();
                                }

                            }
                            #endregion
                            else
                            {
                                #region 27023
                                if (radBenhVien.SelectedIndex == 1)
                                {
                                    string[] _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                    string[] _tieude = { "stt", "Họ tên", "Tuổi", "Địa chỉ", "Số phiếu", "Nhóm xét nghiệm", "Siêu âm", "Điện tim", "Thủ thuật - Phẫu thuật", "Nội soi", "Chụp CT", "Đo thông khí phổi", "Tiền vận chuyển", "Chụp XQ", "% BHYT ngoại trú", "Cộng" };
                                    string _filePath = "D:\\BCVienPhiHangNgay.xls";
                                    int[] _arrWidth = new int[] { };
                                    DungChung.Bien.MangHaiChieu = new Object[all.Count + 1, 16];
                                    for (int i = 0; i < 16; i++)
                                    {
                                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                    }
                                    int num = 1;
                                    foreach (var r in all)
                                    {
                                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                                        DungChung.Bien.MangHaiChieu[num, 1] = r.TenBNhan;
                                        DungChung.Bien.MangHaiChieu[num, 2] = r.Tuoi;
                                        DungChung.Bien.MangHaiChieu[num, 3] = r.DChi;
                                        DungChung.Bien.MangHaiChieu[num, 4] = r.MaBNhan;
                                        DungChung.Bien.MangHaiChieu[num, 5] = r.XN;
                                        DungChung.Bien.MangHaiChieu[num, 6] = r.SieuAm;
                                        DungChung.Bien.MangHaiChieu[num, 7] = r.DienTim;
                                        DungChung.Bien.MangHaiChieu[num, 8] = r.ThuThuatPT;
                                        DungChung.Bien.MangHaiChieu[num, 9] = r.NoiSoi;
                                        DungChung.Bien.MangHaiChieu[num, 10] = r.ChupCT;
                                        DungChung.Bien.MangHaiChieu[num, 11] = r.Congkham;
                                        DungChung.Bien.MangHaiChieu[num, 12] = r.DoCNHH;
                                        DungChung.Bien.MangHaiChieu[num, 13] = r.XQ;
                                        DungChung.Bien.MangHaiChieu[num, 14] = r.NgoaiTruBH;
                                        DungChung.Bien.MangHaiChieu[num, 15] = r.Tong;
                                        num++;
                                    }
                                    BaoCao.rep_VienPhiHangNgay_27023 rep = new BaoCao.rep_VienPhiHangNgay_27023();
                                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Sheet1", _filePath, true, this.Name);
                                    rep.celTitBH.Text = "% BHYT";
                                    rep.DataSource = all;
                                    rep.colCQ.Text = DungChung.Bien.TenCQ.ToUpper();
                                    rep.cel_diadanh.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                                    rep.celNgayThang.Text = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy");
                                    if (String.IsNullOrEmpty(txtTieude.Text))
                                    {
                                        rep.CelTieuDe.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN VIỆN PHÍ HẰNG NGÀY";
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
                                #endregion
                                #region 30005
                                else if (radBenhVien.SelectedIndex == 2)
                                {

                                    string[] _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                    string[] _tieude = { "stt", "Họ tên", "Tuổi", "Địa chỉ", "Số phiếu", "Nhóm xét nghiệm", "Siêu âm", "Điện tim", "Thủ thuật - Phẫu thuật", "Nội soi", "KSK", "Công khám", "Tiền vận chuyển", "Chụp XQ", "Nhóm thăm dò chức năng", "% BHYT ngoại trú", "Tiền BN DV", "Cộng" };
                                    string _filePath = "D:\\BCVienPhiHangNgay.xls";
                                    int[] _arrWidth = new int[] { };
                                    DungChung.Bien.MangHaiChieu = new Object[all.Count + 1, 18];
                                    for (int i = 0; i < 18; i++)
                                    {
                                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                    }
                                    int num = 1;
                                    foreach (var r in all)
                                    {
                                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                                        DungChung.Bien.MangHaiChieu[num, 1] = r.TenBNhan;
                                        DungChung.Bien.MangHaiChieu[num, 2] = r.Tuoi;
                                        DungChung.Bien.MangHaiChieu[num, 3] = r.DChi;
                                        DungChung.Bien.MangHaiChieu[num, 4] = r.MaBNhan;
                                        DungChung.Bien.MangHaiChieu[num, 5] = r.XN;
                                        DungChung.Bien.MangHaiChieu[num, 6] = r.SieuAm;
                                        DungChung.Bien.MangHaiChieu[num, 7] = r.DienTim;
                                        DungChung.Bien.MangHaiChieu[num, 8] = r.ThuThuatPT;
                                        DungChung.Bien.MangHaiChieu[num, 9] = r.NoiSoi;
                                        DungChung.Bien.MangHaiChieu[num, 10] = r.KSK;
                                        DungChung.Bien.MangHaiChieu[num, 11] = r.Congkham;
                                        DungChung.Bien.MangHaiChieu[num, 12] = r.TienVC;
                                        DungChung.Bien.MangHaiChieu[num, 13] = r.XQ;
                                        DungChung.Bien.MangHaiChieu[num, 14] = r.TDCN;
                                        DungChung.Bien.MangHaiChieu[num, 15] = r.NgoaiTruBH;
                                        DungChung.Bien.MangHaiChieu[num, 16] = r.TienBN;
                                        DungChung.Bien.MangHaiChieu[num, 17] = r.Tong;
                                        num++;
                                    }
                                    BaoCao.rep_VienPhiHangNgay_30005 rep = new BaoCao.rep_VienPhiHangNgay_30005();
                                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Sheet1", _filePath, true, this.Name);
                                    rep.celTitBH.Text = "% BHYT";
                                    rep.DataSource = all;
                                    if (gioHC == 0)
                                    {
                                        rep.lblHanhChinh.Text = "(Trong giờ hành chính)";
                                    }
                                    else if (gioHC == 1)
                                    {
                                        rep.lblHanhChinh.Text = "(Ngoài giờ hành chính)";
                                    }
                                    rep.colCQ.Text = DungChung.Bien.TenCQ.ToUpper();
                                    rep.cel_diadanh.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                                    rep.celNgayThang.Text = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy");
                                    if (String.IsNullOrEmpty(txtTieude.Text))
                                    {
                                        rep.CelTieuDe.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN VIỆN PHÍ HẰNG NGÀY";
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
                                #endregion
                                #region mẫu PKHNBN _ so sánh
                                //else if (radBenhVien.SelectedIndex == 6)
                                //{
                                //    //tính viện phí
                                //    List<int> lmabn = all.Select(p => p.MaBNhan).Distinct().ToList();
                                //    var qdaTT = (from vp in data.VienPhis.Where(p => lmabn.Contains(p.MaBNhan ?? 0))
                                //                 join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                                //                 select new { vp.MaBNhan, vpct.TienBN, vpct.TienBH, vpct.ThanhTien }).ToList();
                                //    var qdaTT1 = (from vp in qdaTT group vp by new { vp.MaBNhan } into kq select new { kq.Key.MaBNhan, TienBN = kq.Sum(p => p.TienBN), TienBH = kq.Sum(p => p.TienBH), ThanhTien = kq.Sum(p => p.ThanhTien) }).ToList();
                                //    all = (from a in all
                                //           join b in qdaTT1 on a.MaBNhan equals b.MaBNhan into kq from kq1 in kq.DefaultIfEmpty()
                                //           select new VPHangNgay
                                //           {
                                //               TenBNhan = a.TenBNhan,
                                //               Tuoi = a.Tuoi,
                                //               DChi = a.DChi,
                                //               MaBNhan = a.MaBNhan,
                                //               DTuong = a.DTuong,
                                //               NgayTT = a.NgayTT,
                                //               XN = a.XN,
                                //               TDCN = a.TDCN,
                                //               SieuAm = a.SieuAm,
                                //               SieuAmDL = a.SieuAmDL,
                                //               DienTim = a.DienTim,
                                //               ChupCT = a.ChupCT,
                                //               DoCNHH = a.DoCNHH,
                                //               ThuThuatPT = a.ThuThuatPT,
                                //               NoiSoi = a.NoiSoi,
                                //               Congkham = a.Congkham,
                                //               TienVC = a.TienVC,
                                //               XQ = a.XQ,
                                //               NgoaiTruBH = a.NgoaiTruBH,
                                //               KSK = a.KSK,
                                //               DoLoangXuong = a.DoLoangXuong,
                                //               LuuHuyetNao = a.LuuHuyetNao,
                                //               DienNaoDo = a.DienNaoDo,
                                //               TienThuoc = a.TienThuoc,
                                //               TienVTYT = a.TienVTYT,
                                //               TienBN = kq1 == null ? 0 : kq1.TienBN,
                                //               TienBH = kq1 == null ? 0 : kq1.TienBH,
                                //               ThanhTien = kq1 == null ? 0 : kq1.ThanhTien,
                                //               TienNGDM = a.TienNGDM,
                                //               Tong = a.Tong
                                //           }).ToList();
                                //    //chỉ hiển thị những bn đã thanh toán

                                //    #region chỉ hiển thị thành tiền cho ngày cuối cùng bn thanh toán
                                //    var qngay = (from bn in all group bn by bn.MaBNhan into kq select new { MaBNhan = kq.Key, Ngay = kq.Max(p => p.NgayTT) }).ToList();
                                //    all = (from a in all
                                //           join ngay in qngay on a.MaBNhan equals ngay.MaBNhan
                                //           select new VPHangNgay
                                //           {
                                //               TenBNhan = a.TenBNhan,
                                //               Tuoi = a.Tuoi,
                                //               DChi = a.DChi,
                                //               MaBNhan = a.MaBNhan,
                                //               DTuong = a.DTuong,
                                //               NgayTT = a.NgayTT,
                                //               XN = a.XN,
                                //               TDCN = a.TDCN,
                                //               SieuAm = a.SieuAm,
                                //               SieuAmDL = a.SieuAmDL,
                                //               DienTim = a.DienTim,
                                //               ChupCT = a.ChupCT,
                                //               DoCNHH = a.DoCNHH,
                                //               ThuThuatPT = a.ThuThuatPT,
                                //               NoiSoi = a.NoiSoi,
                                //               Congkham = a.Congkham,
                                //               TienVC = a.TienVC,
                                //               XQ = a.XQ,
                                //               NgoaiTruBH = a.NgoaiTruBH,
                                //               KSK = a.KSK,
                                //               DoLoangXuong = a.DoLoangXuong,
                                //               LuuHuyetNao = a.LuuHuyetNao,
                                //               DienNaoDo = a.DienNaoDo,
                                //               TienThuoc = a.TienThuoc,
                                //               TienVTYT = a.TienVTYT,
                                //               TienBN = a.NgayTT == ngay.Ngay ? a.TienBN : 0,
                                //               TienBH = a.NgayTT == ngay.Ngay ? a.TienBH: 0,
                                //               ThanhTien = a.NgayTT == ngay.Ngay ? a.ThanhTien: 0,
                                //               TienNGDM = a.TienNGDM,
                                //               Tong = a.Tong
                                //           }).ToList();

                                //    #endregion

                                //    string[] _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" , "@", "@", "@"};
                                //    string[] _tieude = { "stt", "Họ tên", "Tuổi", "Địa chỉ", "Số phiếu", "Nhóm xét nghiệm", "Siêu âm", "Điện tim", "Thủ thuật - Phẫu thuật", "Nội soi", "KSK", "Công khám", "Tiền vận chuyển", "Chụp XQ", "Nhóm thăm dò chức năng", "Đo loãng xương", "Lưu huyết não", "Điện não đồ", "Tiền thuốc", "Tiền VTYT", "Tiền thu trực tiếp", "Tiền BN", "Tiền BH","Cộng" };
                                //    string _filePath = "D:\\BCVienPhiHangNgay.xls";
                                //    int[] _arrWidth = new int[] { };
                                //    DungChung.Bien.MangHaiChieu = new Object[all.Count + 1, _arr.Length];
                                //    for (int i = 0; i < _arr.Length; i++)
                                //    {
                                //        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                //    }
                                //    int num = 1;
                                //    foreach (var r in all)
                                //    {
                                //        DungChung.Bien.MangHaiChieu[num, 0] = num;
                                //        DungChung.Bien.MangHaiChieu[num, 1] = r.TenBNhan;
                                //        DungChung.Bien.MangHaiChieu[num, 2] = r.Tuoi;
                                //        DungChung.Bien.MangHaiChieu[num, 3] = r.DChi;
                                //        DungChung.Bien.MangHaiChieu[num, 4] = r.MaBNhan;
                                //        DungChung.Bien.MangHaiChieu[num, 5] = r.XN;
                                //        DungChung.Bien.MangHaiChieu[num, 6] = r.SieuAm;
                                //        DungChung.Bien.MangHaiChieu[num, 7] = r.DienTim;
                                //        DungChung.Bien.MangHaiChieu[num, 8] = r.ThuThuatPT;
                                //        DungChung.Bien.MangHaiChieu[num, 9] = r.NoiSoi;
                                //        DungChung.Bien.MangHaiChieu[num, 10] = r.KSK;
                                //        DungChung.Bien.MangHaiChieu[num, 11] = r.Congkham;
                                //        DungChung.Bien.MangHaiChieu[num, 12] = r.TienVC;
                                //        DungChung.Bien.MangHaiChieu[num, 13] = r.XQ;
                                //        DungChung.Bien.MangHaiChieu[num, 14] = r.TDCN;
                                //        DungChung.Bien.MangHaiChieu[num, 15] = r.DoLoangXuong;
                                //        DungChung.Bien.MangHaiChieu[num, 16] = r.LuuHuyetNao;
                                //        DungChung.Bien.MangHaiChieu[num, 17] = r.DienNaoDo;
                                //        DungChung.Bien.MangHaiChieu[num, 18] = r.TienThuoc;
                                //        DungChung.Bien.MangHaiChieu[num, 19] = r.TienVTYT;
                                //        DungChung.Bien.MangHaiChieu[num, 20] = r.NgoaiTruBH;
                                //        DungChung.Bien.MangHaiChieu[num, 21] = r.TienBN;
                                //        DungChung.Bien.MangHaiChieu[num, 22] = r.TienBH;
                                //        DungChung.Bien.MangHaiChieu[num, 23] = r.ThanhTien;
                                //        num++;
                                //    }
                                //    BaoCao.rep_VienPhiHangNgay_27183_SS rep = new BaoCao.rep_VienPhiHangNgay_27183_SS();
                                //    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Sheet1", _filePath, true, this.Name);

                                //    rep.DataSource = all;
                                //    rep.colCQ.Text = DungChung.Bien.TenCQ.ToUpper();
                                //    rep.cel_diadanh.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                                //    rep.celNgayThang.Text = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy");
                                //    if (DungChung.Bien.MaBV == "12345")
                                //        rep.celNgayThang.Text = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy HH:mm") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy HH:mm");
                                //    if (String.IsNullOrEmpty(txtTieude.Text))
                                //    {
                                //        rep.CelTieuDe.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN VIỆN PHÍ HẰNG NGÀY";
                                //    }
                                //    else
                                //    { rep.CelTieuDe.Text = txtTieude.Text.ToUpper(); }

                                //    if (!String.IsNullOrEmpty(txtNgayThang.Text))
                                //        rep.celNgayThang.Text = txtNgayThang.Text;
                                //    rep.cel_NguoiLap.Text = DungChung.Bien.NguoiLapBieu;
                                //    rep.BindingData();
                                //    rep.CreateDocument();
                                //    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                //    frm.ShowDialog();

                                //}
                                #endregion
                                #region bv khác
                                else
                                {
                                    string[] _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                                    string[] _tieude = { "stt", "Họ tên", "Tuổi", "Địa chỉ", "Số phiếu", "Nhóm xét nghiệm", "Siêu âm", "Điện tim", "Thủ thuật - Phẫu thuật", "Nội soi", "KSK", "Công khám", "Tiền vận chuyển", "Chụp XQ", "Nhóm thăm dò chức năng", "siêu âm Doppler", "XQuang CT", "% BHYT ngoại trú", "Tiền BN DV", "Cộng" };
                                    string _filePath = "D:\\BCVienPhiHangNgay.xls";
                                    int[] _arrWidth = new int[] { };
                                    DungChung.Bien.MangHaiChieu = new Object[all.Count + 1, 20];
                                    for (int i = 0; i < 20; i++)
                                    {
                                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                                    }
                                    int num = 1;
                                    foreach (var r in all)
                                    {
                                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                                        DungChung.Bien.MangHaiChieu[num, 1] = r.TenBNhan;
                                        DungChung.Bien.MangHaiChieu[num, 2] = r.Tuoi;
                                        DungChung.Bien.MangHaiChieu[num, 3] = r.DChi;
                                        DungChung.Bien.MangHaiChieu[num, 4] = r.MaBNhan;
                                        DungChung.Bien.MangHaiChieu[num, 5] = r.XN;
                                        DungChung.Bien.MangHaiChieu[num, 6] = r.SieuAm;
                                        DungChung.Bien.MangHaiChieu[num, 7] = r.DienTim;
                                        DungChung.Bien.MangHaiChieu[num, 8] = r.ThuThuatPT;
                                        DungChung.Bien.MangHaiChieu[num, 9] = r.NoiSoi;
                                        DungChung.Bien.MangHaiChieu[num, 10] = r.KSK;
                                        DungChung.Bien.MangHaiChieu[num, 11] = r.Congkham;
                                        DungChung.Bien.MangHaiChieu[num, 12] = r.TienVC;
                                        DungChung.Bien.MangHaiChieu[num, 13] = r.XQ;
                                        DungChung.Bien.MangHaiChieu[num, 14] = r.TDCN;
                                        DungChung.Bien.MangHaiChieu[num, 15] = r.SieuAmDL;
                                        DungChung.Bien.MangHaiChieu[num, 16] = r.ChupCT;
                                        DungChung.Bien.MangHaiChieu[num, 17] = r.NgoaiTruBH;
                                        DungChung.Bien.MangHaiChieu[num, 18] = r.TienBN;
                                        DungChung.Bien.MangHaiChieu[num, 19] = r.Tong;
                                        num++;
                                    }

                                    if (ckHienThiNgay.Checked)
                                    {
                                        BaoCao.rep_VienPhiHangNgay_30005_Ngay rep = new BaoCao.rep_VienPhiHangNgay_30005_Ngay(ckHienThiNgay.Checked);
                                        frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Sheet1", _filePath, true, this.Name);
                                        rep.celTitBH.Text = "% BHYT";
                                        rep.DataSource = all;
                                        rep.colCQ.Text = DungChung.Bien.TenCQ.ToUpper();
                                        rep.cel_diadanh.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                                        rep.celNgayThang.Text = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy");
                                        if (String.IsNullOrEmpty(txtTieude.Text))
                                        {
                                            rep.CelTieuDe.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN VIỆN PHÍ HẰNG NGÀY";
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
                                    {
                                        BaoCao.rep_VienPhiHangNgay_30005_moi rep = new BaoCao.rep_VienPhiHangNgay_30005_moi();
                                        frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Sheet1", _filePath, true, this.Name);
                                        rep.celTitBH.Text = "% BHYT";
                                        rep.DataSource = all;
                                        rep.colCQ.Text = DungChung.Bien.TenCQ.ToUpper();
                                        rep.cel_diadanh.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                                        rep.celNgayThang.Text = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy");
                                        if (String.IsNullOrEmpty(txtTieude.Text))
                                        {
                                            rep.CelTieuDe.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN VIỆN PHÍ HẰNG NGÀY";


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
                                }
                                #endregion
                            }
                        }

                    }
                    else
                        MessageBox.Show("Không có dữ liệu");
                    #endregion
                }
            }
            #endregion
        }

        #region class VPHangNgay
        public class VPHangNgay
        {
            public string TenBNhan { set; get; }
            public int Tuoi { set; get; }
            public string DChi { set; get; }
            public int MaBNhan { set; get; }
            public string DTuong { set; get; }
            public int TrongDM { set; get; }
            public DateTime NgayTT { set; get; }
            public double Giuong { set; get; }
            public double XN { set; get; }
            public double TDCN { set; get; }
            public double CDHA { set; get; }
            public double SieuAm { set; get; }
            public double DienTim { set; get; }
            public double ThuThuatPT { set; get; }
            public double NoiSoi { set; get; }
            public double KSK { set; get; }
            public double Congkham { set; get; }
            public double TienVC { set; get; }
            public double XQ { set; get; }
            public double DienNaoDo { get; set; }
            public double NgoaiTruBH { set; get; }
            public double NoiTruBH { set; get; }
            public double NgoaiTruDV { set; get; }
            public double NoiTruDV { set; get; }
            public double TienBN { set; get; }
            public double TienNGDM { set; get; }
            public double TienBNDV { set; get; }

            public string SoHD { set; get; }
            public int IDTamUng { set; get; }
            public int XuatHD { get; set; }

            #region dùng cho mẫu chi tiết
            public double TienTN1 { get; set; }
            public double TienTN2 { get; set; }
            public double TienTN3 { get; set; }
            public double TienTN4 { get; set; }
            public double TienTN5 { get; set; }
            public double TienTN6 { get; set; }
            public double TienTN7 { get; set; }
            public double TienTN8 { get; set; }
            public double TienTN9 { get; set; }
            public double TienTN10 { get; set; }
            public double TienTN11 { get; set; }
            public double TienTN12 { get; set; }
            public double TienTN13 { get; set; }
            public double TienTN14 { get; set; }
            public double TienTN15 { get; set; }
            public double TienTN16 { get; set; }
            public double TienTN17 { get; set; }
            public double TienTN18 { get; set; }
            public double TienTN19 { get; set; }
            public double TienTN20 { get; set; }
            public double TienTN21 { get; set; }
            public double TienTN22 { get; set; }
            public double TienTN23 { get; set; }
            public double TienTN24 { get; set; }
            public double TienTN25 { get; set; }
            public double TienTN26 { get; set; }
            public double TienTN27 { get; set; }
            public double TienTN28 { get; set; }
            public double TienTN29 { get; set; }
            public double TienTN30 { get; set; }
            public double TienTN31 { get; set; }
            public double TienTN32 { get; set; }
            #endregion

            public double Tong { set; get; }

            //XN = kq.Sum(p=>p.XN),
            //SieuAm= kq.Sum(p=>p.SieuAm),
            //DienTim = kq.Sum(p=>p.DienTim),
            //ThuThuatPT = kq.Sum(p => p.ThuThuatPT),
            //NoiSoi = kq.Sum(p => p.NoiSoi),
            //KSK = kq.Sum(p => p.KSK),
            //Congkham = kq.Sum(p => p.Congkham),
            //TienVC = kq.Sum(p => p.TienVC),
            //XQ = kq.Sum(p => p.XQ),
            //NgoaiTru = kq.Where(p=>p.NoiTru ==0).Sum(p => p.XN + p.SieuAm + p.DienTim + p.ThuThuatPT + p.NoiSoi + p.KSK + p.Congkham + p.TienVC + p.XQ),
            //NoiTru = kq.Where(p => p.NoiTru == 1).Sum(p => p.XN + p.SieuAm + p.DienTim + p.ThuThuatPT + p.NoiSoi + p.KSK + p.Congkham + p.TienVC + p.XQ),
            //Tong = kq.Sum(p =>

            public double DoLoangXuong { get; set; }

            public double LuuHuyetNao { get; set; }

            public double TienVTYT { get; set; }

            public double TienThuoc { get; set; }

            public double ChupCT { get; set; }

            public double DoCNHH { get; set; }

            public double SieuAmDL { get; set; }

            public double TienBH { get; set; }

            public double ThanhTien { get; set; }

            public double TienKTT { get; set; }
        }
        #endregion

        private void radBenhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radBenhVien.SelectedIndex == 3)
            {
                ckHienThiNgay.Visible = true;
                ckHienThiNgay.Checked = false;
                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "30005")
                {
                    lupcbthu.EditValue = lupcbthu.Properties.GetKeyValueByDisplayText("Tất cả");
                    lupcbthu.Visible = true;
                    labelControl9.Visible = true;
                }

                ckInTongHop.Visible = false;
                ckInTongHop.Checked = false;
            }
            else
            {
                if (radBenhVien.SelectedIndex == 2)
                {
                    lupcbthu.EditValue = lupcbthu.Properties.GetKeyValueByDisplayText("Tất cả");
                    lupcbthu.Visible = true;
                    labelControl9.Visible = true;
                }
                else
                {
                    lupcbthu.EditValue = lupcbthu.Properties.GetKeyValueByDisplayText("Tất cả");
                    lupcbthu.Visible = false;
                    labelControl9.Visible = false;
                }
                ckHienThiNgay.Visible = false;
                ckHienThiNgay.Checked = false;

                if (radBenhVien.SelectedIndex == 0)
                {
                    if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                        rdDuyetPhieu.Visible = true;
                    else
                        rdDuyetPhieu.Visible = false;
                    ckInTongHop.Visible = true;
                }
                else
                {
                    rdDuyetPhieu.Visible = false;
                    ckInTongHop.Visible = false;
                    ckInTongHop.Checked = false;
                }
                if (radBenhVien.SelectedIndex == 5)
                {
                    labelControl10.Visible = true;
                    rgXuatHD.Visible = true;
                    lupcbthu.Visible = true;
                    labelControl9.Visible = true;
                }
                else
                {
                    labelControl10.Visible = false;
                    rgXuatHD.Visible = false;
                }
                if (DungChung.Bien.MaBV == "30372")
                {
                    if (radBenhVien.SelectedIndex == 0)
                    {
                        labelControl11.Visible = true;
                        lupCBTT.Visible = true;
                    }

                    else
                    {
                        labelControl11.Visible = false;
                        lupCBTT.Visible = false;
                    }
                }
            }
        }

        private void rdNgay_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (rdNgay.SelectedIndex == 1)
            //    ckcHTHoaDon.Visible = true;
            //else
            //    ckcHTHoaDon.Visible = false;
        }

        private void ckMauCT_CheckedChanged(object sender, EventArgs e)
        {
            if (ckMauCT.Checked)
                rdgNhomDV.Enabled = true;
            else
                rdgNhomDV.Enabled = false;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void ckcHTHoaDon_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}