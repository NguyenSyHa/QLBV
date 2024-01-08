using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;

namespace QLBV.FormThamSo
{
    //Quy trình sử dụng kho xã:
    //Kho tổng --> kho xã --> xã phường --> sử dụng
    public partial class frm_TonDuoc_BìnhGiang : DevExpress.XtraEditors.XtraForm
    {
        public frm_TonDuoc_BìnhGiang()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        List<KPhong> _lkp;
        List<KPhong> _lkpNoiBo;
        List<KPhong> _lKhoXa;
        List<int> _lXP = new List<int>();
        private void frm_TonDuoc_BìnhGiang_Load(object sender, EventArgs e)
        {
            memoEdit1.Text += "Cách lấy dữ liệu\r\n";
            memoEdit1.Text += "\r\n";
            memoEdit1.Text += " - Tồn đầu kỳ: tồn của kho tổng cộng tồn của kho xã, phòng khám sặt\r\n";
            memoEdit1.Text += " - Nhập chuyển kho:  nhập của kho tổng từ kho ngoại trú \r\n";
            memoEdit1.Text += " - Xuất bệnh nhân: Xuất nội, ngoại trú của kho tổng - xuất trả dược nội ngoại trú của kho tổng\r\n";
            memoEdit1.Text += " - Xuất phòng khám, kho xã: Sử dụng (ploai = 5) của kho xã + tổng xuất (đã trừ xuất nội bộ, xuất trả dược của phòng khám Sặt)\r\n";
            memoEdit1.Text += " - Xuất kho ngoại trú: Xuất từ kho tổng cho kho ngoại trú đã trừ khoản nhập trả dược từ kho ngoại trú về kho tổng\r\n";
            memoEdit1.Text += " - Xuất khác: Hư hao, xuất kiểm nghiệm, lâm sàng ,xuất khác ... (đã trừ các loại xuất trên)\r\n";
            dateTuNgay.DateTime = DateTime.Now;
            dateDenNgay.DateTime = DateTime.Now;
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lkp = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc).Where(p => p.Status == 1).ToList();
            List<KPhong> lKhotong = _lkp.Where(p => p.MaBVsd == DungChung.Bien.MaBV).ToList();
            lupKhoTong.Properties.DataSource = lKhotong;
            _lXP = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.XaPhuong).Where(p => p.Status == 1).Select(p => p.MaKP).ToList();

            List<NhomDV> _lnhom = new List<NhomDV>();
            _lnhom = data.NhomDVs.Where(p => p.Status == 1).ToList();
            _lnhom.Add(new NhomDV { IDNhom = -1, TenNhom = " Tất cả" });
            cboNhom.Properties.DataSource = _lnhom.OrderBy(p => p.TenNhom).ToList();

        }

        private void lupKhoTong_EditValueChanged(object sender, EventArgs e)
        {
            if (lupKhoTong.EditValue != null)
            {
                int MaKhoTong = Convert.ToInt32(lupKhoTong.EditValue);
                _lkpNoiBo = _lkp.Where(p => p.MaBVsd == DungChung.Bien.MaBV && p.MaKP != MaKhoTong).ToList();
                _lKhoXa = _lkp.Where(p => p.MaKP != MaKhoTong).ToList();
                cklKP.DataSource = _lkpNoiBo;
                ckKhoXa.DataSource = _lKhoXa;
                cklPhongKham.DataSource = _lKhoXa;
            }
        }

        private void ckKhoXa_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {

        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            //Bệnh viện có 4 kho: nội trú, ngoại trú, 2 kho xã
            //tính bcnxt là tính cho kho nội trú (kho tổng)
            // số lượng xuất chỉ tính xuất cho bệnh nhân + sl xuất của 2 kho xã kho bn (ko tính sl xuất của kho tổng cho 2 kho xã)
            // nhập được tính là số lượng nhập theo hóa đơn và nhập từ kho ngoại trú vào kho nội trú (ko tính số lượng nhập trả lại từ kho xã về kho tổng)
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);
            int makhotong = 0;
            List<int> lKPNoiBo = new List<int>();

            List<int> lKhoXa = new List<int>();
            List<int> lPKham = new List<int>();

            List<int> lKPNoiBoAll = new List<int>(); // danh sách tất cả khoa phòng nội bộ (được chọn và không được chọn)

            List<BC> list = new List<BC>();
            if (lupKhoTong.EditValue != null)
                makhotong = Convert.ToInt32(lupKhoTong.EditValue);
            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                lKPNoiBoAll.Add(Convert.ToInt32(cklKP.GetItemValue(i)));
                if (cklKP.GetItemCheckState(i) == CheckState.Checked)
                {
                    lKPNoiBo.Add(Convert.ToInt32(cklKP.GetItemValue(i)));

                }
            }

            for (int i = 1; i < ckKhoXa.ItemCount; i++)
            {
                if (ckKhoXa.GetItemCheckState(i) == CheckState.Checked)
                    lKhoXa.Add(Convert.ToInt32(ckKhoXa.GetItemValue(i)));
            }

            for (int i = 1; i < cklPhongKham.ItemCount; i++)
            {
                if (cklPhongKham.GetItemCheckState(i) == CheckState.Checked)
                {
                    lPKham.Add(Convert.ToInt32(cklPhongKham.GetItemValue(i)));

                }
            }

            List<int> allkpChon = new List<int>();

            allkpChon.Add(makhotong);
            allkpChon.AddRange(lKhoXa);
            allkpChon.AddRange(lPKham);
            allkpChon.AddRange(lKPNoiBo);
            List<int> qkpnoibokhac = new List<int>();

            var qkphong = data.KPhongs.ToList();
            foreach (var a in _lkp)
            {
                if (!allkpChon.Contains(a.MaKP))
                {
                    qkpnoibokhac.Add(a.MaKP);

                }
            }

            #region mẫu 170 cũ
            if (ckMauMoi.Checked == false)
            {
                var ltutruc = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc).ToList();



                var qnd = (from nd in data.NhapDs
                           join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                           select new
                           {
                               NgayNhap = nd.NgayNhap.Value,
                               nd.PLoai,
                               nd.KieuDon,
                               MaKP = nd.MaKP.Value,
                               MaKPnx = nd.MaKPnx,
                               nd.TraDuoc_KieuDon,
                               nd.MaCC,
                               ndct.MaDV,
                               ndct.DonGia,
                               ndct.DonVi,
                               SoLuongN = nd.PLoai == 1 ? ndct.SoLuongN : 0,
                               SoLuongX = (nd.PLoai == 2 || nd.PLoai == 3) ? ndct.SoLuongX : 0,
                               ThanhTienN = nd.PLoai == 1 ? ndct.ThanhTienN : 0,
                               ThanhTienX = (nd.PLoai == 2 || nd.PLoai == 3) ? ndct.ThanhTienX : 0,
                               ndct.SoLuongSD,
                               ndct.ThanhTienSD,
                               ndct.SoLuongDY,
                               ndct.ThanhTienDY
                           }).Where(p => p.SoLuongN != 0 || p.SoLuongX != 0 || p.SoLuongSD != 0 || p.SoLuongDY != 0).ToList();
                var qnd1 = (from nd in qnd
                            join kp in ltutruc on nd.MaKPnx equals kp.MaKP into kq
                            from kq1 in kq.DefaultIfEmpty()
                            select new
                            {
                                nd.PLoai,
                                nd.KieuDon,
                                nd.MaDV,
                                nd.DonVi,
                                nd.DonGia,
                                nd.NgayNhap,

                                //nhập theo hóa đơn - trả dược
                                NhapTheoHDSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon != 2 && nd.MaKP == makhotong && nd.KieuDon == 1) ? nd.SoLuongN : 0,
                                NhapTheoHDTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon != 2 && nd.MaKP == makhotong && nd.KieuDon == 1) ? nd.ThanhTienN : 0,
                                //nhập từ kho ngoại trú về kho tổng - nhập chuyển kho
                                NhapChuyenKhoSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 0 && nd.MaKP == makhotong && nd.MaKPnx != null && lKPNoiBo.Contains(nd.MaKPnx.Value)) ? nd.SoLuongN : 0,
                                NhapChuyenKhoTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 0 && nd.MaKP == makhotong && nd.MaKPnx != null && lKPNoiBo.Contains(nd.MaKPnx.Value)) ? nd.ThanhTienN : 0,
                                //nhập trả lại từ tủ trực -his 1956
                                NhapKhacSL = (kq1 != null && nd.PLoai == 1 && nd.KieuDon == 2 && nd.TraDuoc_KieuDon == 6 && nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.MaKP == makhotong) ? nd.SoLuongN : 0,
                                NhapKhacTT = (kq1 != null && nd.PLoai == 1 && nd.KieuDon == 2 && nd.TraDuoc_KieuDon == 6 && nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.MaKP == makhotong) ? nd.ThanhTienN : 0,
                                // xuất cho bệnh nhân của kho tổng
                                XuatBNSL = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.MaKP == makhotong && (nd.KieuDon == 0 || nd.KieuDon == 1 || nd.KieuDon == 4)) ? nd.SoLuongX : 0),
                                XuatBNTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.MaKP == makhotong && (nd.KieuDon == 0 || nd.KieuDon == 1 || nd.KieuDon == 4)) ? nd.ThanhTienX : 0,

                                // xuất cho bệnh nhân HIS-4638
                                XuatBNSL1 = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.MaKP == makhotong && (nd.KieuDon == 1 || nd.KieuDon == 5 || nd.KieuDon == 7 || nd.KieuDon == 11)) ? nd.SoLuongX : 0),
                                XuatBNTT1 = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.MaKP == makhotong && (nd.KieuDon == 1 || nd.KieuDon == 5 || nd.KieuDon == 7 || nd.KieuDon == 11)) ? nd.ThanhTienX : 0,

                                //xuất sử dụng của xã phường
                                XuatSuDungSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 5 && lKhoXa.Contains(nd.MaKP)) ? nd.SoLuongSD : 0,
                                XuatSuDungTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 5 && lKhoXa.Contains(nd.MaKP)) ? nd.ThanhTienSD : 0,
                                // xuất nội bộ phòng khám SẶt
                                XuatNoiBoSL = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.KieuDon != 2 && lPKham.Contains(nd.MaKP)) ? nd.SoLuongX : 0),
                                XuatNoiBoTT = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.KieuDon != 2 && lPKham.Contains(nd.MaKP)) ? nd.ThanhTienX : 0),

                                //Xuất cho kho ngoại trú 
                                XuatNgTSL = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.KieuDon == 2 && nd.MaKP == makhotong && nd.MaKPnx != null && lKPNoiBo.Contains(nd.MaKPnx.Value)) ? nd.SoLuongX : 0),
                                XuatNgTTT = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.KieuDon == 2 && nd.MaKP == makhotong && nd.MaKPnx != null && lKPNoiBo.Contains(nd.MaKPnx.Value)) ? nd.ThanhTienX : 0),


                                // xuất cho bệnh nhân của kho tổng (trả dược)
                                NhapBNSL_TD = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.MaKP == makhotong && nd.KieuDon == 2 && (nd.TraDuoc_KieuDon == 0 || nd.TraDuoc_KieuDon == 1 || nd.TraDuoc_KieuDon == 4)) ? nd.SoLuongN : 0),
                                NhapBNTT_TD = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.MaKP == makhotong && nd.KieuDon == 2 && (nd.TraDuoc_KieuDon == 0 || nd.TraDuoc_KieuDon == 1 || nd.TraDuoc_KieuDon == 4)) ? nd.ThanhTienN : 0),

                                // xuất nội bộ phòng khám SẶt (trả dược)
                                NhapNoiBoSL_TD = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.TraDuoc_KieuDon != 2 && nd.MaKPnx != null && lPKham.Contains(nd.MaKPnx.Value)) ? nd.SoLuongN : 0),
                                NhapNoiBoTT_TD = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.TraDuoc_KieuDon != 2 && nd.MaKPnx != null && lPKham.Contains(nd.MaKPnx.Value)) ? nd.ThanhTienN : 0),

                                //nhập từ kho ngoại trú về kho tổng (trả dược)
                                NhapChuyenKhoSL_TD = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.TraDuoc_KieuDon == 2 && nd.MaKP == makhotong && nd.MaKPnx != null && lKPNoiBo.Contains(nd.MaKPnx.Value)) ? nd.SoLuongN : 0,
                                NhapChuyenKhoTT_TD = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.TraDuoc_KieuDon == 2 && nd.MaKP == makhotong && nd.MaKPnx != null && lKPNoiBo.Contains(nd.MaKPnx.Value)) ? nd.ThanhTienN : 0,

                                //tổng nhập trả dược nội trú (bao gồm cả nhập trả dược từ kho ngoại trú về kho tổng) và nhập trả dược tủ trực (ko có nhập trả trược của bệnh nhân)
                                TongNhapTDSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.MaKP == makhotong && (nd.TraDuoc_KieuDon != 0 && nd.TraDuoc_KieuDon != 1 && nd.TraDuoc_KieuDon != 4)) ? nd.SoLuongN : 0,
                                TongNhapTDTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.MaKP == makhotong && (nd.TraDuoc_KieuDon != 0 && nd.TraDuoc_KieuDon != 1 && nd.TraDuoc_KieuDon != 4)) ? nd.ThanhTienN : 0,

                                //Xuất khác: = hư hao + xuất kiểm nghiệm, xuất tủ trực, xuất khác, xuất CLS, xuất phòng khám, xuất lâm sàng, xuất sản xuất (chưa có xuất chuyển kho cho kho khác trừ xuất trả dược )
                                XuatKhacSL = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.MaKP == makhotong && ((nd.PLoai == 2 && nd.KieuDon > 4) || nd.PLoai == 3)) ? nd.SoLuongX : 0),
                                XuatKhacTT = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.MaKP == makhotong && ((nd.PLoai == 2 && nd.KieuDon > 4) || nd.PLoai == 3)) ? nd.ThanhTienX : 0),

                                //Xuất cho kho  khác trong bv ngoại trừ kho ngoại trú, kho xã, kho thuốc pk sặt
                                XuatNgTKhacSL = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.KieuDon == 2 && nd.MaKP == makhotong && qkpnoibokhac.Count > 0 && nd.MaKPnx != null && qkpnoibokhac.Contains(nd.MaKPnx.Value)) ? nd.SoLuongX : 0),
                                XuatNgTKhacTT = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.KieuDon == 2 && nd.MaKP == makhotong && qkpnoibokhac.Count > 0 && nd.MaKPnx != null && qkpnoibokhac.Contains(nd.MaKPnx.Value)) ? nd.ThanhTienX : 0),

                                //Xuất khác HIS-4638
                                XuatKhacSL1 = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && (nd.KieuDon == 6 || nd.KieuDon == 8) && nd.MaKP == makhotong && qkpnoibokhac.Count > 0 && nd.MaKPnx != null && qkpnoibokhac.Contains(nd.MaKPnx.Value)) ? nd.SoLuongX : 0) + ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.MaKP == makhotong && qkpnoibokhac.Count > 0 && nd.MaKPnx != null && qkpnoibokhac.Contains(nd.MaKPnx.Value)) ? nd.SoLuongDY : 0),
                                XuatKhacTT1 = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && (nd.KieuDon == 6 || nd.KieuDon == 8) && nd.MaKP == makhotong && qkpnoibokhac.Count > 0 && nd.MaKPnx != null && qkpnoibokhac.Contains(nd.MaKPnx.Value)) ? nd.ThanhTienX : 0) + ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.MaKP == makhotong && qkpnoibokhac.Count > 0 && nd.MaKPnx != null && qkpnoibokhac.Contains(nd.MaKPnx.Value)) ? nd.ThanhTienDY : 0),

                                //nhập trả dược xã phường (xã phường trả về kho xã)
                                NhapTDXPDKSL = ((nd.NgayNhap < tungay && nd.PLoai == 1 && nd.KieuDon == 2 && lKhoXa.Contains(nd.MaKP) && nd.MaKPnx != null && _lXP.Contains(nd.MaKPnx.Value)) ? nd.SoLuongN : 0),
                                NhapTDXPDKTT = ((nd.NgayNhap < tungay && nd.PLoai == 1 && nd.KieuDon == 2 && lKhoXa.Contains(nd.MaKP) && nd.MaKPnx != null && _lXP.Contains(nd.MaKPnx.Value)) ? nd.ThanhTienN : 0),

                                NhapTDXPCKSL = ((nd.NgayNhap < denngay && nd.PLoai == 1 && nd.KieuDon == 2 && lKhoXa.Contains(nd.MaKP) && nd.MaKPnx != null && _lXP.Contains(nd.MaKPnx.Value)) ? nd.SoLuongN : 0),
                                NhapTDXPCKTT = ((nd.NgayNhap < denngay && nd.PLoai == 1 && nd.KieuDon == 2 && lKhoXa.Contains(nd.MaKP) && nd.MaKPnx != null && _lXP.Contains(nd.MaKPnx.Value)) ? nd.ThanhTienN : 0),

                                TonDKSL = ((nd.NgayNhap < tungay && nd.PLoai == 1 && (nd.MaKP == makhotong || lKhoXa.Contains(nd.MaKP) || lPKham.Contains(nd.MaKP))) ? nd.SoLuongN : 0) - ((nd.NgayNhap < tungay && (nd.PLoai == 2 || nd.PLoai == 3) && (nd.MaKP == makhotong || (lKhoXa.Contains(nd.MaKP) && nd.KieuDon != 3) || lPKham.Contains(nd.MaKP))) ? nd.SoLuongX : 0) - ((nd.NgayNhap < tungay && nd.PLoai == 5 && lKhoXa.Contains(nd.MaKP)) ? nd.SoLuongSD : 0),
                                TonDKTT = ((nd.NgayNhap < tungay && nd.PLoai == 1 && (nd.MaKP == makhotong || lKhoXa.Contains(nd.MaKP) || lPKham.Contains(nd.MaKP))) ? nd.ThanhTienN : 0) - ((nd.NgayNhap < tungay && (nd.PLoai == 2 || nd.PLoai == 3) && (nd.MaKP == makhotong || (lKhoXa.Contains(nd.MaKP) && nd.KieuDon != 3) || lPKham.Contains(nd.MaKP))) ? nd.ThanhTienX : 0) - ((nd.NgayNhap < tungay && nd.PLoai == 5 && lKhoXa.Contains(nd.MaKP)) ? nd.ThanhTienSD : 0),
                                TonCKSL = ((nd.NgayNhap < denngay && nd.PLoai == 1 && (nd.MaKP == makhotong || lKhoXa.Contains(nd.MaKP) || lPKham.Contains(nd.MaKP))) ? nd.SoLuongN : 0) - ((nd.NgayNhap < denngay && (nd.PLoai == 2 || nd.PLoai == 3) && (nd.MaKP == makhotong || (lKhoXa.Contains(nd.MaKP) && nd.KieuDon != 3) || lPKham.Contains(nd.MaKP))) ? nd.SoLuongX : 0) - ((nd.NgayNhap <= denngay && nd.PLoai == 5 && lKhoXa.Contains(nd.MaKP)) ? nd.SoLuongSD : 0),
                                TonCKTT = ((nd.NgayNhap < denngay && nd.PLoai == 1 && (nd.MaKP == makhotong || lKhoXa.Contains(nd.MaKP) || lPKham.Contains(nd.MaKP))) ? nd.ThanhTienN : 0) - ((nd.NgayNhap < denngay && (nd.PLoai == 2 || nd.PLoai == 3) && (nd.MaKP == makhotong || (lKhoXa.Contains(nd.MaKP) && nd.KieuDon != 3) || lPKham.Contains(nd.MaKP))) ? nd.ThanhTienX : 0) - ((nd.NgayNhap <= denngay && nd.PLoai == 5 && lKhoXa.Contains(nd.MaKP)) ? nd.ThanhTienSD : 0),




                            }).ToList();

                #region chậm
                //foreach (var a in qnd)
                //{
                //    BC moi = new BC();
                //    moi.MaDV = a.MaDV.Value;
                //    moi.DonVi = a.DonVi;
                //    moi.DonGia = a.DonGia;
                //    moi.NgayNhap = a.NgayNhap;
                //    if (a.NgayNhap >= tungay && a.NgayNhap <= denngay)// nhập xuất trong kỳ
                //    {
                //        if (a.PLoai == 1 && a.MaKP == makhotong)
                //        {
                //            if (a.KieuDon == 1)// nhập theo hóa đơn
                //            {
                //                moi.NhapTheoHDSL = a.SoLuongN;
                //                moi.NhapTheoHDTT = a.ThanhTienN;
                //                moi.NhapTKSL = a.SoLuongN;
                //                moi.NhapTKTT = a.ThanhTienN;
                //            }
                //            else if (a.MaKPnx != null && lKPNoiBo.Contains(a.MaKPnx.Value))// nhập từ kho nội bộ khác được chọn
                //            {
                //                moi.NhapChuyenKhoSL = a.SoLuongN;
                //                moi.NhapChuyenKhoTT = a.ThanhTienN;
                //                moi.NhapTKSL = a.SoLuongN;
                //                moi.NhapTKTT = a.ThanhTienN;
                //            }
                //        }
                //        if (a.PLoai == 2)
                //        {
                //            if (a.MaKP == makhotong && a.MaKPnx == null) // xuất cho bệnh nhân từ kho tổng
                //            {
                //                moi.XuatBNSL = a.SoLuongX;
                //                moi.XuatBNTT = a.ThanhTienX;
                //                moi.XuatTKSL = a.SoLuongX;
                //                moi.XuatTKTT = a.ThanhTienX;
                //            }
                //            else if (lKhoXa.Contains(a.MaKP) && a.KieuDon == 3) // kho xã xuất cho bệnh nhân
                //            {
                //                moi.XuatSuDungSL = a.SoLuongX;
                //                moi.XuatSuDungTT = a.ThanhTienX;
                //                moi.XuatTKSL = a.SoLuongX;
                //                moi.XuatTKTT = a.ThanhTienX;
                //            }
                //        }
                //    }
                //    //tồn
                //    if (a.NgayNhap < tungay)
                //    {
                //        if (a.MaKP == makhotong || (lKhoXa.Contains(a.MaKP)))
                //        {
                //            if (a.PLoai == 1)
                //            {
                //                moi.TonDKSL = a.SoLuongN;
                //                moi.TonDKTT = a.ThanhTienN;
                //            }
                //            else if (a.PLoai == 2)
                //            {
                //                moi.TonDKSL = -a.SoLuongX;
                //                moi.TonDKTT = -a.ThanhTienX;
                //            }
                //        }
                //    }

                //    if (a.NgayNhap <= denngay)
                //    {
                //        if (a.MaKP == makhotong || (lKhoXa.Contains(a.MaKP)))
                //        {
                //            if (a.PLoai == 1)
                //            {
                //                moi.TonCKSL = a.SoLuongN;
                //                moi.TonCKTT = a.ThanhTienN;
                //            }
                //            else if (a.PLoai == 2)
                //            {
                //                moi.TonCKSL = -a.SoLuongX;
                //                moi.TonCKTT = -a.ThanhTienX;
                //            }
                //        }
                //    }
                //    list.Add(moi);
                //}
                #endregion

                var qnxt = (from nd in qnd1
                            group nd by new { nd.MaDV, nd.DonGia, nd.DonVi } into kq
                            select new BC
                            {
                                MaDV = kq.Key.MaDV.Value,
                                DonVi = kq.Key.DonVi,
                                DonGia = kq.Key.DonGia,
                                TonDKSL = kq.Sum(p => p.TonDKSL) - kq.Sum(p => p.NhapTDXPDKSL),
                                TonDKTT = kq.Sum(p => p.TonDKTT) - kq.Sum(p => p.NhapTDXPDKTT),
                                NhapTheoHDSL = kq.Sum(p => p.NhapTheoHDSL),
                                NhapTheoHDTT = kq.Sum(p => p.NhapTheoHDTT),
                                NhapChuyenKhoSL = kq.Sum(p => p.NhapChuyenKhoSL),
                                NhapChuyenKhoTT = kq.Sum(p => p.NhapChuyenKhoTT),
                                NhapKhacSL = kq.Sum(p => p.NhapKhacSL),
                                NhapKhacTT = kq.Sum(p => p.NhapKhacTT),

                                //XuatBNSL = kq.Sum(p => p.XuatBNSL) - kq.Sum(p => p.NhapBNSL_TD),
                                //XuatBNTT = kq.Sum(p => p.XuatBNTT) - kq.Sum(p => p.NhapBNTT_TD),

                                //HIS-4638
                                XuatBNSL = kq.Sum(p => p.XuatBNSL1),//xuất nội trú + xuất phòng khám + xuất cLS + Xuất lâm sàng
                                XuatBNTT = kq.Sum(p => p.XuatBNTT1),

                                XuatSuDungSL = kq.Sum(p => p.XuatSuDungSL) + kq.Sum(p => p.XuatNoiBoSL) - kq.Sum(p => p.NhapNoiBoSL_TD),// xuất sử dụng kho xã + xuất nội bộ phòng khám sặt
                                XuatSuDungTT = kq.Sum(p => p.XuatSuDungTT) + kq.Sum(p => p.XuatNoiBoTT) - kq.Sum(p => p.NhapNoiBoTT_TD),

                                XuatNgTSL = kq.Sum(p => p.XuatNgTSL) - kq.Sum(p => p.NhapChuyenKhoSL_TD),
                                XuatNgTTT = kq.Sum(p => p.XuatNgTTT) - kq.Sum(p => p.NhapChuyenKhoTT_TD),

                                //XuatKhacSL = kq.Sum(p => p.XuatKhacSL) + kq.Sum(p => p.XuatNgTKhacSL) + kq.Sum(p => p.NhapKhacSL) - (kq.Sum(p => p.TongNhapTDSL) - kq.Sum(p => p.NhapChuyenKhoSL_TD)), // xuất hư hao, xuất kiểu đơn >4 + xuất nội trú (từ kho nội trú khác ngoài kho ngoại trú và kho xã, kho thuốc phòng khám sặt)- xuất trả dược của những kho nội trú trên (không tính nhập trả dươc j của tủ trực kho nội trú)
                                //XuatKhacTT = kq.Sum(p => p.XuatKhacTT) + kq.Sum(p => p.XuatNgTKhacTT) + kq.Sum(p => p.NhapKhacTT) - (kq.Sum(p => p.TongNhapTDTT) - kq.Sum(p => p.NhapChuyenKhoTT_TD)),

                                //HIS-4638
                                XuatKhacSL = kq.Sum(p => p.XuatKhacSL1), // Xuất tủ trực+hư hao+xuất kiểm nghiệm
                                XuatKhacTT = kq.Sum(p => p.XuatKhacTT1),

                                TonCKSL = kq.Sum(p => p.TonCKSL) - kq.Sum(p => p.NhapTDXPCKSL),
                                TonCKTT = kq.Sum(p => p.TonCKTT) - kq.Sum(p => p.NhapTDXPCKTT),
                            }).Where(p => ((p.TonDKSL != 0 || p.TonCKSL != 0) && !ckHthi.Checked) || p.NhapTheoHDSL != 0 || p.NhapChuyenKhoSL != 0 || p.XuatBNSL != 0 || p.XuatSuDungSL != 0 || p.XuatNgTSL != 0 || p.XuatKhacSL != 0 || p.NhapKhacSL != 0).ToList();

                List<DichVu> ldv = data.DichVus.Where(p => p.PLoai == 1).ToList();

                int idnhom = -1, idtieunhom = -1;
                if (cboNhom.EditValue != null)
                    idnhom = Convert.ToInt32(cboNhom.EditValue);
                if (cboTieuNhom.EditValue != null)
                    idtieunhom = Convert.ToInt32(cboTieuNhom.EditValue);

                var qnxt2 = (from nd in qnxt
                             join dv in ldv on nd.MaDV equals dv.MaDV
                             join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                             join ndv in data.NhomDVs on tn.IDNhom equals ndv.IDNhom
                             select new
                             {
                                 IdTieuNhom = tn.IdTieuNhom,
                                 IDNhom = ndv.IDNhom,
                                 MaDV = nd.MaDV,
                                 TenDV = dv.TenDV,
                                 DonGia = nd.DonGia,
                                 DonVi = nd.DonVi,
                                 TonDKSL = nd.TonDKSL,
                                 TonDKTT = nd.TonDKTT,
                                 NhapTheoHDSL = nd.NhapTheoHDSL,
                                 NhapTheoHDTT = nd.NhapTheoHDTT,
                                 NhapChuyenKhoSL = nd.NhapChuyenKhoSL,
                                 NhapChuyenKhoTT = nd.NhapChuyenKhoTT,
                                 NhapKhacSL = nd.NhapKhacSL,
                                 NhapKhacTT = nd.NhapKhacTT,
                                 XuatBNSL = nd.XuatBNSL,
                                 XuatBNTT = nd.XuatBNTT,
                                 XuatSuDungSL = nd.XuatSuDungSL,
                                 XuatSuDungTT = nd.XuatSuDungTT,
                                 XuatNgTSL = nd.XuatNgTSL,
                                 XuatNgTTT = nd.XuatNgTTT,
                                 XuatKhacSL = nd.XuatKhacSL,
                                 XuatKhacTT = nd.XuatKhacTT,
                                 TongXuatSL = nd.XuatBNSL + nd.XuatSuDungSL + nd.XuatNgTSL + nd.XuatKhacSL,
                                 TongXuatTT = nd.XuatBNTT + nd.XuatSuDungTT + nd.XuatNgTTT + nd.XuatKhacTT,
                                 TonCKSL = nd.TonCKSL,
                                 TonCKTT = nd.TonCKTT,
                                 TenTN = tn.TenTN,
                                 TenNhomDuoc = ndv.TenNhom
                             }).Where(p => (idnhom == -1 ? true : p.IDNhom == idnhom) && (idtieunhom == -1 ? true : p.IdTieuNhom == idtieunhom)).OrderBy(p => p.TenDV).ToList();


                frmIn frm = new frmIn();
                BaoCao.rep_TonDuoc_BinhGiang rep = new BaoCao.rep_TonDuoc_BinhGiang();

                rep.TENCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                rep.TENKHO.Value = lupKhoTong.Text.ToUpper();

                var tenkho = (from kp in lKhoXa
                              join p in _lkp on kp equals p.MaKP
                              select new { p.TenKP }).ToList();
                var tenpk = (from kp in lPKham //teen phongf khams
                             join p in _lkp on kp equals p.MaKP
                             select new { p.TenKP }).ToList();
                string tenkhonew = "";
                foreach (var item in tenkho)
                {
                    tenkhonew += item.TenKP + "; ";
                }
                foreach (var item in tenpk)
                {
                    tenkhonew += item.TenKP + "; ";
                }
                // rep.KHO.Value = tenkhonew;
                if (tenkhonew != "")
                    rep.TIEUDE.Value = "BÁO CÁO NHẬP - XUẤT - TỒN TỔNG " + lupKhoTong.Text.ToUpper() + "; " + tenkhonew.ToUpper();
                else
                    rep.TIEUDE.Value = "BÁO CÁO NHẬP - XUẤT - TỒN TỔNG " + lupKhoTong.Text.ToUpper();
                rep.NGAYTHANG.Value = "Từ ngày " + tungay.ToShortDateString() + " đến ngày " + denngay.ToShortDateString();

                rep.DataSource = qnxt2;
                rep.databinding();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            #endregion
            else
            {


                #region mẫu NXT mới -his 1894
                var ltutruc = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc).ToList();
                var qnd0 = (from nd in data.NhapDs
                            join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                            select new
                            {
                                NgayNhap = nd.NgayNhap.Value,
                                nd.PLoai,
                                nd.KieuDon,
                                ndct.IDDTBN,
                                MaKP = nd.MaKP.Value,
                                MaKPnx = nd.MaKPnx,
                                nd.TraDuoc_KieuDon,
                                nd.MaCC,
                                ndct.MaDV,
                                ndct.DonGia,
                                SoLuongN = nd.PLoai == 1 ? ndct.SoLuongN : 0,
                                SoLuongX = (nd.PLoai == 2 || nd.PLoai == 3) ? ndct.SoLuongX : 0,
                                ThanhTienN = nd.PLoai == 1 ? ndct.ThanhTienN : 0,
                                ThanhTienX = (nd.PLoai == 2 || nd.PLoai == 3) ? ndct.ThanhTienX : 0,
                                ndct.SoLuongSD,
                                ndct.ThanhTienSD
                            }).Where(p => p.SoLuongN != 0 || p.SoLuongX != 0 || p.SoLuongSD != 0).ToList();
                var qnd = (from nd in qnd0
                           join kp in ltutruc on nd.MaKPnx equals kp.MaKP into kq
                           from kq1 in kq.DefaultIfEmpty()
                           select new { NgayNhap = nd.NgayNhap, nd.PLoai, nd.KieuDon, nd.IDDTBN, MaKP = nd.MaKP, MaKPnx = nd.MaKPnx, nd.TraDuoc_KieuDon, nd.MaCC, nd.MaDV, nd.DonGia, nd.SoLuongN, nd.ThanhTienN, nd.SoLuongX, nd.ThanhTienX, nd.SoLuongSD, nd.ThanhTienSD, kq1 }).ToList();

                var qDTBN = data.DTBNs.ToList();
                int idDTBH = -2;
                int idDTDV = -2;
                var qDTBH = qDTBN.Where(p => p.DTBN1 != null && p.DTBN1.Trim() == "BHYT").FirstOrDefault();
                var qDTDV = qDTBN.Where(p => p.DTBN1 != null && p.DTBN1.Trim().ToLower() == "dịch vụ").FirstOrDefault();
                if (qDTBH != null)
                    idDTBH = qDTBH.IDDTBN;
                if (qDTDV != null)
                    idDTDV = qDTDV.IDDTBN;
                #region nxt kho Tồn đầu kỳ kho ngoại trú
                var qngtru = (from nd in qnd.Where(p => lKPNoiBo.Contains(p.MaKP))
                              select new BCMoi
                              {
                                  MaDV = nd.MaDV ?? 0,
                                  DonGia = nd.DonGia,

                                  TonDKSL = ((nd.NgayNhap < tungay && nd.PLoai == 1) ? nd.SoLuongN : 0) - ((nd.NgayNhap < tungay && (nd.PLoai == 2 || nd.PLoai == 3)) ? nd.SoLuongX : 0),
                                  TonDKTT = ((nd.NgayNhap < tungay && nd.PLoai == 1) ? nd.ThanhTienN : 0) - ((nd.NgayNhap < tungay && (nd.PLoai == 2 || nd.PLoai == 3)) ? nd.ThanhTienX : 0),
                                  NhapSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 1) ? nd.SoLuongN : 0,
                                  NhapTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 1) ? nd.ThanhTienN : 0,
                                  NhapTuTuTrucNgoaiTruSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.kq1 != null) ? nd.SoLuongN : 0,// nhập trả lại từ tủ trực
                                  NhapTuTuTrucNgoaiTruTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.kq1 != null) ? nd.ThanhTienN : 0,
                                  XuatBHSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && (nd.IDDTBN == idDTBH || nd.KieuDon == 1 || nd.KieuDon == 5 && nd.KieuDon == 7 || nd.KieuDon == 11)) ? nd.SoLuongX : 0,
                                  XuatBHTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && (nd.IDDTBN == idDTBH || nd.KieuDon == 1 || nd.KieuDon == 5 && nd.KieuDon == 7 || nd.KieuDon == 11)) ? nd.ThanhTienX : 0,
                                  NhapTraDuocBHSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && (nd.IDDTBN == idDTBH || nd.TraDuoc_KieuDon == 1 || nd.TraDuoc_KieuDon == 5 && nd.TraDuoc_KieuDon == 7 || nd.TraDuoc_KieuDon == 11)) ? nd.SoLuongN : 0,
                                  NhapTraDuocBHTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && (nd.IDDTBN == idDTBH || nd.TraDuoc_KieuDon == 1 || nd.TraDuoc_KieuDon == 5 && nd.TraDuoc_KieuDon == 7 || nd.TraDuoc_KieuDon == 11)) ? nd.ThanhTienN : 0,
                                  XuatDVSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.IDDTBN == idDTDV) ? nd.SoLuongX : 0,
                                  XuatDVTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.IDDTBN == idDTDV) ? nd.ThanhTienX : 0,
                                  NhapTraDuocDVSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.IDDTBN == idDTDV) ? nd.SoLuongN : 0,
                                  NhapTraDuocDVTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.IDDTBN == idDTDV) ? nd.ThanhTienN : 0,
                                  XuatKhacSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && (nd.PLoai == 3 || (nd.PLoai == 2 && nd.IDDTBN != idDTDV && nd.IDDTBN != idDTBH && nd.KieuDon != 2 && nd.KieuDon != 1 && nd.KieuDon != 5 && nd.KieuDon != 7 && nd.KieuDon != 11))) ? nd.SoLuongX : 0,
                                  XuatKhacTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && (nd.PLoai == 3 || (nd.PLoai == 2 && nd.IDDTBN != idDTDV && nd.IDDTBN != idDTBH && nd.KieuDon != 2 && nd.KieuDon != 1 && nd.KieuDon != 5 && nd.KieuDon != 7 && nd.KieuDon != 11))) ? nd.ThanhTienX : 0,
                                  NhapTraLaiKhacSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.kq1 == null && nd.IDDTBN != idDTDV && nd.IDDTBN != idDTBH && nd.TraDuoc_KieuDon != 2 && nd.TraDuoc_KieuDon != 1 && nd.TraDuoc_KieuDon != 5 && nd.TraDuoc_KieuDon != 7 && nd.TraDuoc_KieuDon != 11) ? nd.SoLuongN : 0,
                                  NhapTraLaiKhacTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.kq1 == null && nd.IDDTBN != idDTDV && nd.IDDTBN != idDTBH && nd.TraDuoc_KieuDon != 2 && nd.TraDuoc_KieuDon != 1 && nd.TraDuoc_KieuDon != 5 && nd.TraDuoc_KieuDon != 7 && nd.TraDuoc_KieuDon != 11) ? nd.ThanhTienN : 0,
                                  TongXuatSL = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && (nd.PLoai == 2 || nd.PLoai == 3) && nd.KieuDon != 2) ? nd.SoLuongX : 0), // tổng xuất - xuất chuyển kho
                                  TongXuatTT = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && (nd.PLoai == 2 || nd.PLoai == 3) && nd.KieuDon != 2) ? nd.ThanhTienX : 0),
                                  TongNhapTraLaiSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.kq1 == null && nd.TraDuoc_KieuDon != 2) ? nd.SoLuongN : 0,// không tính nhập trả lại của tủ trực
                                  TongNhapTraLaiTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.kq1 == null && nd.TraDuoc_KieuDon != 2) ? nd.ThanhTienN : 0,
                                  TonCKSL = ((nd.NgayNhap <= denngay && nd.PLoai == 1) ? nd.SoLuongN : 0) - ((nd.NgayNhap <= denngay && (nd.PLoai == 2 || nd.PLoai == 3)) ? nd.SoLuongX : 0),
                                  TonCKTT = ((nd.NgayNhap <= denngay && nd.PLoai == 1) ? nd.ThanhTienN : 0) - ((nd.NgayNhap <= denngay && (nd.PLoai == 2 || nd.PLoai == 3)) ? nd.ThanhTienX : 0),

                              }).ToList();

                #endregion
                #region nxt kho Tồn kho nội trú - all
                //var qntru = (from nd in qnd
                //             select new BCMoi
                //             {
                //                 MaDV = nd.MaDV ?? 0,
                //                 DonGia = nd.DonGia,
                //                 DonVi = nd.DonVi,
                //                 TonDKSL = ((nd.NgayNhap < tungay && nd.PLoai == 1 && (nd.MaKP == makhotong || lKhoXa.Contains(nd.MaKP) || lPKham.Contains(nd.MaKP))) ? nd.SoLuongN : 0) - ((nd.NgayNhap < tungay && (nd.PLoai == 2 || nd.PLoai == 3) && (nd.MaKP == makhotong || (lKhoXa.Contains(nd.MaKP) && nd.KieuDon != 3) || lPKham.Contains(nd.MaKP))) ? nd.SoLuongX : 0) - ((nd.NgayNhap < tungay && nd.PLoai == 5 && lKhoXa.Contains(nd.MaKP)) ? nd.SoLuongSD : 0),
                //                 TonDKTT = ((nd.NgayNhap < tungay && nd.PLoai == 1 && (nd.MaKP == makhotong || lKhoXa.Contains(nd.MaKP) || lPKham.Contains(nd.MaKP))) ? nd.ThanhTienN : 0) - ((nd.NgayNhap < tungay && (nd.PLoai == 2 || nd.PLoai == 3) && (nd.MaKP == makhotong || (lKhoXa.Contains(nd.MaKP) && nd.KieuDon != 3) || lPKham.Contains(nd.MaKP))) ? nd.ThanhTienX : 0) - ((nd.NgayNhap < tungay && nd.PLoai == 5 && lKhoXa.Contains(nd.MaKP)) ? nd.ThanhTienSD : 0),
                //                 NhapSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 1 && nd.MaKP == makhotong) ? nd.SoLuongN : 0,
                //                 NhapTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 1 && nd.MaKP == makhotong) ? nd.ThanhTienN : 0,
                //                 XuatBHSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.IDDTBN == idDTBH && (nd.KieuDon == 0 || nd.KieuDon == 1 || nd.KieuDon == 4) && (nd.MaKP == makhotong || lKhoXa.Contains(nd.MaKP) || lPKham.Contains(nd.MaKP))) ? nd.SoLuongX : 0,
                //                 XuatBHTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.IDDTBN == idDTBH && (nd.KieuDon == 0 || nd.KieuDon == 1 || nd.KieuDon == 4) && (nd.MaKP == makhotong || lKhoXa.Contains(nd.MaKP) || lPKham.Contains(nd.MaKP))) ? nd.ThanhTienX : 0,
                //                 XuatDVSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.IDDTBN == idDTDV && (nd.KieuDon == 0 || nd.KieuDon == 1 || nd.KieuDon == 4) && (nd.MaKP == makhotong || lKhoXa.Contains(nd.MaKP) || lPKham.Contains(nd.MaKP))) ? nd.SoLuongX : 0,
                //                 XuatDVTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.IDDTBN == idDTDV && (nd.KieuDon == 0 || nd.KieuDon == 1 || nd.KieuDon == 4) && (nd.MaKP == makhotong || lKhoXa.Contains(nd.MaKP) || lPKham.Contains(nd.MaKP))) ? nd.ThanhTienX : 0,
                //                 XuatKhacSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.IDDTBN != idDTDV && nd.IDDTBN != idDTBH && nd.KieuDon != 2 && nd.KieuDon != 3 && (nd.MaKP == makhotong || lKhoXa.Contains(nd.MaKP) || lPKham.Contains(nd.MaKP))) ? nd.SoLuongX : 0,
                //                 XuatKhacTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.IDDTBN != idDTDV && nd.IDDTBN != idDTBH && nd.KieuDon != 2 && nd.KieuDon != 3 && (nd.MaKP == makhotong || lKhoXa.Contains(nd.MaKP) || lPKham.Contains(nd.MaKP))) ? nd.ThanhTienX : 0,
                //                 TongXuatSL = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.KieuDon != 3 && nd.KieuDon != 2 && (nd.MaKP == makhotong || lKhoXa.Contains(nd.MaKP) || lPKham.Contains(nd.MaKP))) ? nd.SoLuongX : 0) + ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 5 && lKhoXa.Contains(nd.MaKP)) ? nd.SoLuongSD : 0),
                //                 TongXuatTT = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.KieuDon != 3 && nd.KieuDon != 2 && (nd.MaKP == makhotong || lKhoXa.Contains(nd.MaKP) || lPKham.Contains(nd.MaKP))) ? nd.ThanhTienX : 0) + ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 5 && lKhoXa.Contains(nd.MaKP)) ? nd.ThanhTienSD : 0),
                //                 TonCKSL = ((nd.NgayNhap < denngay && nd.PLoai == 1 && (nd.MaKP == makhotong || lKhoXa.Contains(nd.MaKP) || lPKham.Contains(nd.MaKP))) ? nd.SoLuongN : 0) - ((nd.NgayNhap < denngay && (nd.PLoai == 2 || nd.PLoai == 3) && (nd.MaKP == makhotong || (lKhoXa.Contains(nd.MaKP) && nd.KieuDon != 3) || lPKham.Contains(nd.MaKP))) ? nd.SoLuongX : 0) - ((nd.NgayNhap < denngay && nd.PLoai == 5 && lKhoXa.Contains(nd.MaKP)) ? nd.SoLuongSD : 0),
                //                 TonCKTT = ((nd.NgayNhap < denngay && nd.PLoai == 1 && (nd.MaKP == makhotong || lKhoXa.Contains(nd.MaKP) || lPKham.Contains(nd.MaKP))) ? nd.ThanhTienN : 0) - ((nd.NgayNhap < denngay && (nd.PLoai == 2 || nd.PLoai == 3) && (nd.MaKP == makhotong || (lKhoXa.Contains(nd.MaKP) && nd.KieuDon != 3) || lPKham.Contains(nd.MaKP))) ? nd.ThanhTienX : 0) - ((nd.NgayNhap < denngay && nd.PLoai == 5 && lKhoXa.Contains(nd.MaKP)) ? nd.ThanhTienSD : 0),

                //             }).ToList();
                #endregion
                #region nxt kho Tồn kho nội trú
                foreach (int i in lKhoXa)
                {
                    if (lKPNoiBoAll.Contains(i))
                        lKPNoiBoAll.Remove(i);
                }
                foreach (int i in lPKham)
                {
                    if (lKPNoiBoAll.Contains(i))
                        lKPNoiBoAll.Remove(i);
                }
                foreach (int i in lKPNoiBo)
                {
                    if (lKPNoiBoAll.Contains(i))
                        lKPNoiBoAll.Remove(i);
                }

                #region
                //var qntru0 = (from nd in qnd.Where(p => p.MaKP == makhotong)
                //              select new
                //              {
                //                  MaDV = nd.MaDV ?? 0,
                //                  DonGia = nd.DonGia,
                //                  TonDKSL = ((nd.NgayNhap < tungay && nd.PLoai == 1) ? nd.SoLuongN : 0) - ((nd.NgayNhap < tungay && (nd.PLoai == 2 || nd.PLoai == 3)) ? nd.SoLuongX : 0),
                //                  TonDKTT = ((nd.NgayNhap < tungay && nd.PLoai == 1) ? nd.ThanhTienN : 0) - ((nd.NgayNhap < tungay && (nd.PLoai == 2 || nd.PLoai == 3)) ? nd.ThanhTienX : 0),
                //                  NhapSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 1) ? nd.SoLuongN : 0,
                //                  NhapTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 1) ? nd.ThanhTienN : 0,
                //                  NhapTuTuTrucSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.kq1 != null) ? nd.SoLuongN : 0,// nhập trả lại từ tủ trực
                //                  NhapTuTuTrucTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.kq1 != null) ? nd.ThanhTienN : 0,// nhập trả lại từ tủ trực


                //                  XuatBHSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.IDDTBN == idDTBH && (nd.KieuDon == 0 || nd.KieuDon == 1 || nd.KieuDon == 4)) ? nd.SoLuongX : 0,
                //                  XuatBHTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.IDDTBN == idDTBH && (nd.KieuDon == 0 || nd.KieuDon == 1 || nd.KieuDon == 4)) ? nd.ThanhTienX : 0,
                //                  NhapTraDuocBHSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.IDDTBN == idDTBH && (nd.TraDuoc_KieuDon == 0 || nd.TraDuoc_KieuDon == 1 || nd.TraDuoc_KieuDon == 4)) ? nd.SoLuongN : 0,
                //                  NhapTraDuocBHTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.IDDTBN == idDTBH && (nd.TraDuoc_KieuDon == 0 || nd.TraDuoc_KieuDon == 1 || nd.TraDuoc_KieuDon == 4)) ? nd.ThanhTienN : 0,
                //                  XuatDVSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.IDDTBN == idDTDV && (nd.KieuDon == 0 || nd.KieuDon == 1 || nd.KieuDon == 4)) ? nd.SoLuongX : 0,
                //                  XuatDVTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.IDDTBN == idDTDV && (nd.KieuDon == 0 || nd.KieuDon == 1 || nd.KieuDon == 4)) ? nd.ThanhTienX : 0,
                //                  NhapTraDuocDVSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.IDDTBN == idDTDV && (nd.TraDuoc_KieuDon == 0 || nd.TraDuoc_KieuDon == 1 || nd.TraDuoc_KieuDon == 4)) ? nd.SoLuongN : 0,
                //                  NhapTraDuocDVTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.IDDTBN == idDTDV && (nd.TraDuoc_KieuDon == 0 || nd.TraDuoc_KieuDon == 1 || nd.TraDuoc_KieuDon == 4)) ? nd.ThanhTienN : 0,

                //                  XuatKhacSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.IDDTBN != idDTDV && nd.IDDTBN != idDTBH && nd.KieuDon != 2 && nd.KieuDon != 3) ? nd.SoLuongX : 0,
                //                  XuatKhacTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.IDDTBN != idDTDV && nd.IDDTBN != idDTBH && nd.KieuDon != 2 && nd.KieuDon != 3) ? nd.ThanhTienX : 0,

                //                  NhapTraLaiKhacSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.kq1 == null && nd.IDDTBN != idDTDV && nd.IDDTBN != idDTBH && nd.TraDuoc_KieuDon != 2 && nd.TraDuoc_KieuDon != 3) ? nd.SoLuongN : 0,
                //                  NhapTraLaiKhacTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.kq1 == null && nd.IDDTBN != idDTDV && nd.IDDTBN != idDTBH && nd.TraDuoc_KieuDon != 2 && nd.TraDuoc_KieuDon != 3) ? nd.ThanhTienN : 0,

                //                  //TongXuatSL = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && ((  nd.PLoai == 3 || (nd.PLoai == 2 && nd.KieuDon != 3 && nd.KieuDon != 2) || (nd.PLoai == 2 && nd.KieuDon == 2 && nd.MaKPnx != null && lKPNoiBoAll.Count > 0 && lKPNoiBoAll.Contains(nd.MaKPnx??0))))) ? nd.SoLuongX : 0),
                //                  //TongXuatTT = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && (( nd.PLoai == 3 ||(nd.PLoai == 2 && nd.KieuDon != 3 && nd.KieuDon != 2) || (nd.PLoai == 2 && nd.KieuDon == 2 && nd.MaKPnx != null && lKPNoiBoAll.Count > 0 && lKPNoiBoAll.Contains(nd.MaKPnx??0))))) ? nd.ThanhTienX : 0),

                //                  //TongNhapTraLaiSL = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.kq1 == null && nd.TraDuoc_KieuDon != 3 && (nd.TraDuoc_KieuDon != 2 || (nd.TraDuoc_KieuDon == 2 && lKPNoiBo.Count > 0 && lKPNoiBo.Contains(nd.MaKPnx ?? 0)))) ? nd.SoLuongN : 0),
                //                  //TongNhapTraLaiTT = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.kq1 == null && nd.TraDuoc_KieuDon != 3 && (nd.TraDuoc_KieuDon != 2 || (nd.TraDuoc_KieuDon == 2 && lKPNoiBo.Count > 0 && lKPNoiBo.Contains(nd.MaKPnx ?? 0)))) ? nd.ThanhTienN : 0),
                //                  #region tính tổng xuất
                //                  //Xuất cho kho ngoại trú  
                //                  XuatNgTSL = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.KieuDon == 2 && nd.MaKP == makhotong && nd.MaKPnx != null && lKPNoiBo.Contains(nd.MaKPnx.Value)) ? nd.SoLuongX : 0),
                //                  XuatNgTTT = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.KieuDon == 2 && nd.MaKP == makhotong && nd.MaKPnx != null && lKPNoiBo.Contains(nd.MaKPnx.Value)) ? nd.ThanhTienX : 0),

                //                  //nhập từ kho ngoại trú về kho tổng (trả dược)
                //                  NhapChuyenKhoSL_TD = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.TraDuoc_KieuDon == 2 && nd.MaKP == makhotong && nd.MaKPnx != null && lKPNoiBo.Contains(nd.MaKPnx.Value)) ? nd.SoLuongN : 0,
                //                  NhapChuyenKhoTT_TD = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.TraDuoc_KieuDon == 2 && nd.MaKP == makhotong && nd.MaKPnx != null && lKPNoiBo.Contains(nd.MaKPnx.Value)) ? nd.ThanhTienN : 0,

                //                  // xuất cho bệnh nhân của kho tổng
                //                  XuatBNSL = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.MaKP == makhotong && (nd.KieuDon == 0 || nd.KieuDon == 1 || nd.KieuDon == 4)) ? nd.SoLuongX : 0),
                //                  XuatBNTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.MaKP == makhotong && (nd.KieuDon == 0 || nd.KieuDon == 1 || nd.KieuDon == 4)) ? nd.ThanhTienX : 0,
                //                  // xuất cho bệnh nhân của kho tổng (trả dược)
                //                  NhapBNSL_TD = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.MaKP == makhotong && nd.KieuDon == 2 && (nd.TraDuoc_KieuDon == 0 || nd.TraDuoc_KieuDon == 1 || nd.TraDuoc_KieuDon == 4)) ? nd.SoLuongN : 0),
                //                  NhapBNTT_TD = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.MaKP == makhotong && nd.KieuDon == 2 && (nd.TraDuoc_KieuDon == 0 || nd.TraDuoc_KieuDon == 1 || nd.TraDuoc_KieuDon == 4)) ? nd.ThanhTienN : 0),


                //                  //tổng nhập trả dược nội trú (bao gồm cả nhập trả dược từ kho ngoại trú về kho tổng) và nhập trả dược tủ trực (ko có nhập trả trược của bệnh nhân)
                //                  TongNhapTDSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.MaKP == makhotong && (nd.TraDuoc_KieuDon != 0 && nd.TraDuoc_KieuDon != 1)) ? nd.SoLuongN : 0,
                //                  TongNhapTDTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.MaKP == makhotong && (nd.TraDuoc_KieuDon != 0 && nd.TraDuoc_KieuDon != 1)) ? nd.ThanhTienN : 0,

                //                  //Xuất khác: = hư hao + xuất kiểm nghiệm, xuất tủ trực, xuất khác, xuất CLS, xuất phòng khám, xuất lâm sàng, xuất sản xuất (chưa có xuất chuyển kho cho kho khác trừ xuất trả dược )
                //                  T_XuatKhacSL = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.MaKP == makhotong && ((nd.PLoai == 2 && nd.KieuDon > 4) || nd.PLoai == 3)) ? nd.SoLuongX : 0),
                //                  T_XuatKhacTT = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.MaKP == makhotong && ((nd.PLoai == 2 && nd.KieuDon > 4) || nd.PLoai == 3)) ? nd.ThanhTienX : 0),


                //                  //Xuất cho kho  khác trong bv ngoại trừ kho ngoại trú, kho xã, kho thuốc pk sặt
                //                  XuatNgTKhacSL = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.KieuDon == 2 && nd.MaKP == makhotong && qkpnoibokhac.Count > 0 && nd.MaKPnx != null && qkpnoibokhac.Contains(nd.MaKPnx.Value)) ? nd.SoLuongX : 0),
                //                  XuatNgTKhacTT = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.KieuDon == 2 && nd.MaKP == makhotong && qkpnoibokhac.Count > 0 && nd.MaKPnx != null && qkpnoibokhac.Contains(nd.MaKPnx.Value)) ? nd.ThanhTienX : 0),

                //                  #endregion
                //                  //TongNhapTraLaiSL = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.kq1 == null && nd.TraDuoc_KieuDon != 3 && (nd.TraDuoc_KieuDon != 2 || (nd.TraDuoc_KieuDon == 2 && lKPNoiBoAll.Count > 0 && lKPNoiBoAll.Contains(nd.MaKPnx??0)))) ? nd.SoLuongN : 0),
                //                  //TongNhapTraLaiTT = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.kq1 == null && nd.TraDuoc_KieuDon != 3 && (nd.TraDuoc_KieuDon != 2 || (nd.TraDuoc_KieuDon == 2 && lKPNoiBoAll.Count > 0 && lKPNoiBoAll.Contains(nd.MaKPnx??0)))) ? nd.ThanhTienN : 0),
                //                  TonCKSL = ((nd.NgayNhap <= denngay && nd.PLoai == 1) ? nd.SoLuongN : 0) - ((nd.NgayNhap <= denngay && (nd.PLoai == 2 || nd.PLoai == 3)) ? nd.SoLuongX : 0),
                //                  TonCKTT = ((nd.NgayNhap <= denngay && nd.PLoai == 1) ? nd.ThanhTienN : 0) - ((nd.NgayNhap <= denngay && (nd.PLoai == 2 || nd.PLoai == 3)) ? nd.ThanhTienX : 0),
                //              }).ToList();

                //var qntru1 = (from nd in qntru0
                //              select new
                //              {
                //                  MaDV = nd.MaDV,
                //                  DonGia = nd.DonGia,
                //                  TonDKSL = nd.TonDKSL,
                //                  TonDKTT = nd.TonDKTT,
                //                  NhapSL = nd.NhapSL,
                //                  NhapTT = nd.NhapTT,
                //                  NhapTuTuTrucSL = nd.NhapTuTuTrucSL,
                //                  NhapTuTuTrucTT = nd.NhapTuTuTrucTT,
                //                  XuatBHSL = nd.XuatBHSL,
                //                  XuatBHTT = nd.XuatBHTT,
                //                  NhapTraDuocBHSL = nd.NhapTraDuocBHSL,
                //                  NhapTraDuocBHTT = nd.NhapTraDuocBHTT,
                //                  XuatDVSL = nd.XuatDVSL,
                //                  XuatDVTT = nd.XuatDVTT,
                //                  NhapTraDuocDVSL = nd.NhapTraDuocDVSL,
                //                  NhapTraDuocDVTT = nd.NhapTraDuocDVTT,
                //                  XuatKhacSL = nd.XuatKhacSL,
                //                  XuatKhacTT = nd.XuatKhacTT,
                //                  NhapTraLaiKhacSL = nd.NhapTraLaiKhacSL,
                //                  NhapTraLaiKhacTT = nd.NhapTraLaiKhacTT,
                //                  #region tính tổng xuất

                //                  NhapKhacSL = nd.NhapTuTuTrucSL,
                //                  NhapKhacTT = nd.NhapTuTuTrucTT,
                //                  XuatBNSL = nd.XuatBNSL - nd.NhapBNSL_TD,
                //                  XuatBNTT = nd.XuatBNTT - nd.NhapBNTT_TD,

                //                  XuatNgTSL = nd.XuatNgTSL - nd.NhapChuyenKhoSL_TD,
                //                  XuatNgTTT = nd.XuatNgTTT - nd.NhapChuyenKhoTT_TD,

                //                  T_XuatKhacSL = nd.T_XuatKhacSL + nd.XuatNgTKhacSL + nd.NhapTuTuTrucSL - (nd.TongNhapTDSL - nd.NhapChuyenKhoSL_TD), // xuất hư hao, xuất kiểu đơn >4 + xuất nội trú (từ kho nội trú khác ngoài kho ngoại trú và kho xã, kho thuốc phòng khám sặt)- xuất trả dược của những kho nội trú trên (không tính nhập trả dươc j của tủ trực kho nội trú)
                //                  T_XuatKhacTT = nd.T_XuatKhacTT + nd.XuatNgTKhacTT + nd.NhapTuTuTrucTT - (nd.TongNhapTDTT - nd.NhapChuyenKhoTT_TD),
                //                  #endregion

                //                  //TongNhapTraLaiSL = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.kq1 == null && nd.TraDuoc_KieuDon != 3 && (nd.TraDuoc_KieuDon != 2 || (nd.TraDuoc_KieuDon == 2 && lKPNoiBoAll.Count > 0 && lKPNoiBoAll.Contains(nd.MaKPnx??0)))) ? nd.SoLuongN : 0),
                //                  //TongNhapTraLaiTT = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.kq1 == null && nd.TraDuoc_KieuDon != 3 && (nd.TraDuoc_KieuDon != 2 || (nd.TraDuoc_KieuDon == 2 && lKPNoiBoAll.Count > 0 && lKPNoiBoAll.Contains(nd.MaKPnx??0)))) ? nd.ThanhTienN : 0),
                //                  TonCKSL = nd.TonCKSL,
                //                  TonCKTT = nd.TonCKTT
                //              }).ToList();

                //var qntru = (from nd in qntru1
                //             select new BCMoi
                //             {
                //                 MaDV = nd.MaDV,
                //                 DonGia = nd.DonGia,
                //                 TonDKSL = nd.TonDKSL,
                //                 TonDKTT = nd.TonDKTT,
                //                 NhapSL = nd.NhapSL,
                //                 NhapTT = nd.NhapTT,
                //                 NhapTuTuTrucSL = nd.NhapTuTuTrucSL,
                //                 NhapTuTuTrucTT = nd.NhapTuTuTrucTT,
                //                 XuatBHSL = nd.XuatBHSL,
                //                 XuatBHTT = nd.XuatBHTT,
                //                 NhapTraDuocBHSL = nd.NhapTraDuocBHSL,
                //                 NhapTraDuocBHTT = nd.NhapTraDuocBHTT,
                //                 XuatDVSL = nd.XuatDVSL,
                //                 XuatDVTT = nd.XuatDVTT,
                //                 NhapTraDuocDVSL = nd.NhapTraDuocDVSL,
                //                 NhapTraDuocDVTT = nd.NhapTraDuocDVTT,
                //                 XuatKhacSL = nd.XuatKhacSL,
                //                 XuatKhacTT = nd.XuatKhacTT,
                //                 NhapTraLaiKhacSL = nd.NhapTraLaiKhacSL,
                //                 NhapTraLaiKhacTT = nd.NhapTraLaiKhacTT,
                //                 #region tính tổng xuất
                //                 TongXuatSL = nd.XuatBNSL + nd.XuatNgTSL + nd.XuatKhacSL,
                //                 TongXuatTT = nd.XuatBNTT + nd.XuatNgTTT + nd.XuatKhacTT,
                //                 #endregion

                //                 //TongNhapTraLaiSL = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.kq1 == null && nd.TraDuoc_KieuDon != 3 && (nd.TraDuoc_KieuDon != 2 || (nd.TraDuoc_KieuDon == 2 && lKPNoiBoAll.Count > 0 && lKPNoiBoAll.Contains(nd.MaKPnx??0)))) ? nd.SoLuongN : 0),
                //                 //TongNhapTraLaiTT = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.kq1 == null && nd.TraDuoc_KieuDon != 3 && (nd.TraDuoc_KieuDon != 2 || (nd.TraDuoc_KieuDon == 2 && lKPNoiBoAll.Count > 0 && lKPNoiBoAll.Contains(nd.MaKPnx??0)))) ? nd.ThanhTienN : 0),
                //                 TonCKSL = nd.TonCKSL,
                //                 TonCKTT = nd.TonCKTT
                //             }).ToList();
                #endregion

                #region
                var qntru = (from nd in qnd.Where(p => p.MaKP == makhotong)
                             select new BCMoi
                             {
                                 MaDV = nd.MaDV ?? 0,
                                 DonGia = nd.DonGia,
                                 TonDKSL = ((nd.NgayNhap < tungay && nd.PLoai == 1) ? nd.SoLuongN : 0) - ((nd.NgayNhap < tungay && (nd.PLoai == 2 || nd.PLoai == 3)) ? nd.SoLuongX : 0),
                                 TonDKTT = ((nd.NgayNhap < tungay && nd.PLoai == 1) ? nd.ThanhTienN : 0) - ((nd.NgayNhap < tungay && (nd.PLoai == 2 || nd.PLoai == 3)) ? nd.ThanhTienX : 0),
                                 NhapSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 1) ? nd.SoLuongN : 0,
                                 NhapTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 1) ? nd.ThanhTienN : 0,
                                 NhapTuTuTrucSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.kq1 != null) ? nd.SoLuongN : 0,// nhập trả lại từ tủ trực
                                 NhapTuTuTrucTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.kq1 != null) ? nd.ThanhTienN : 0,// nhập trả lại từ tủ trực

                                 XuatBHSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.IDDTBN != idDTDV && (nd.KieuDon == 0 || nd.KieuDon == 1 || nd.KieuDon == 4 || nd.KieuDon == 5 || nd.KieuDon == 7 || nd.KieuDon == 11)) ? nd.SoLuongX : 0,
                                 XuatBHTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.IDDTBN != idDTDV && (nd.KieuDon == 0 || nd.KieuDon == 1 || nd.KieuDon == 4 || nd.KieuDon == 5 || nd.KieuDon == 7 || nd.KieuDon == 11)) ? nd.ThanhTienX : 0,
                                 NhapTraDuocBHSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.IDDTBN != idDTDV && (nd.TraDuoc_KieuDon == 0 || nd.TraDuoc_KieuDon == 1 || nd.TraDuoc_KieuDon == 4 || nd.TraDuoc_KieuDon == 5 || nd.TraDuoc_KieuDon == 7 || nd.TraDuoc_KieuDon == 11)) ? nd.SoLuongN : 0,
                                 NhapTraDuocBHTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.IDDTBN != idDTDV && (nd.TraDuoc_KieuDon == 0 || nd.TraDuoc_KieuDon == 1 || nd.TraDuoc_KieuDon == 4 || nd.TraDuoc_KieuDon == 5 || nd.TraDuoc_KieuDon == 7 || nd.TraDuoc_KieuDon == 11)) ? nd.ThanhTienN : 0,
                                 XuatDVSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.IDDTBN == idDTDV && (nd.KieuDon == 0 || nd.KieuDon == 1 || nd.KieuDon == 4)) ? nd.SoLuongX : 0,
                                 XuatDVTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.IDDTBN == idDTDV && (nd.KieuDon == 0 || nd.KieuDon == 1 || nd.KieuDon == 4)) ? nd.ThanhTienX : 0,
                                 NhapTraDuocDVSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.IDDTBN == idDTDV && (nd.TraDuoc_KieuDon == 0 || nd.TraDuoc_KieuDon == 1 || nd.TraDuoc_KieuDon == 4)) ? nd.SoLuongN : 0,
                                 NhapTraDuocDVTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.IDDTBN == idDTDV && (nd.TraDuoc_KieuDon == 0 || nd.TraDuoc_KieuDon == 1 || nd.TraDuoc_KieuDon == 4)) ? nd.ThanhTienN : 0,

                                 XuatKhacSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && (nd.PLoai == 3 || (nd.PLoai == 2 && nd.IDDTBN != idDTDV && nd.IDDTBN != idDTBH && nd.KieuDon != 2 && nd.KieuDon != 3 && nd.KieuDon != 5 && nd.KieuDon != 1 && nd.KieuDon != 7 && nd.KieuDon != 11))) ? nd.SoLuongX : 0,
                                 XuatKhacTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && (nd.PLoai == 3 || (nd.PLoai == 2 && nd.IDDTBN != idDTDV && nd.IDDTBN != idDTBH && nd.KieuDon != 2 && nd.KieuDon != 3 && nd.KieuDon != 5 && nd.KieuDon != 1 && nd.KieuDon != 7 && nd.KieuDon != 11))) ? nd.ThanhTienX : 0,

                                 NhapTraLaiKhacSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.kq1 == null && nd.IDDTBN != idDTDV && nd.IDDTBN != idDTBH && nd.TraDuoc_KieuDon != 2 && nd.TraDuoc_KieuDon != 3 && nd.TraDuoc_KieuDon != 5 && nd.TraDuoc_KieuDon != 1 && nd.TraDuoc_KieuDon != 7 && nd.TraDuoc_KieuDon != 11) ? nd.SoLuongN : 0,
                                 NhapTraLaiKhacTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.kq1 == null && nd.IDDTBN != idDTDV && nd.IDDTBN != idDTBH && nd.TraDuoc_KieuDon != 2 && nd.TraDuoc_KieuDon != 3 && nd.TraDuoc_KieuDon != 5 && nd.TraDuoc_KieuDon != 1 && nd.TraDuoc_KieuDon != 7 && nd.TraDuoc_KieuDon != 11) ? nd.ThanhTienN : 0,

                                 TongXuatSL = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && ((nd.PLoai == 3 || (nd.PLoai == 2 && nd.KieuDon != 3 && nd.KieuDon != 2) || (nd.PLoai == 2 && nd.KieuDon == 2 && nd.MaKPnx != null && lKPNoiBoAll.Count > 0 && lKPNoiBoAll.Contains(nd.MaKPnx ?? 0))))) ? nd.SoLuongX : 0),
                                 TongXuatTT = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && ((nd.PLoai == 3 || (nd.PLoai == 2 && nd.KieuDon != 3 && nd.KieuDon != 2) || (nd.PLoai == 2 && nd.KieuDon == 2 && nd.MaKPnx != null && lKPNoiBoAll.Count > 0 && lKPNoiBoAll.Contains(nd.MaKPnx ?? 0))))) ? nd.ThanhTienX : 0),

                                 TongNhapTraLaiSL = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.kq1 == null && nd.TraDuoc_KieuDon != 3 && (nd.TraDuoc_KieuDon != 2)) ? nd.SoLuongN : 0),
                                 TongNhapTraLaiTT = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.kq1 == null && nd.TraDuoc_KieuDon != 3 && (nd.TraDuoc_KieuDon != 2)) ? nd.ThanhTienN : 0),

                                 //TongNhapTraLaiSL = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.kq1 == null && nd.TraDuoc_KieuDon != 3 && (nd.TraDuoc_KieuDon != 2 || (nd.TraDuoc_KieuDon == 2 && lKPNoiBo.Count > 0 && lKPNoiBo.Contains(nd.MaKPnx ?? 0)))) ? nd.SoLuongN : 0), // chưa trừ đi phần xuất từ kho nội trú sang kho ngoại trú
                                 //TongNhapTraLaiTT = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.kq1 == null && nd.TraDuoc_KieuDon != 3 && (nd.TraDuoc_KieuDon != 2 || (nd.TraDuoc_KieuDon == 2 && lKPNoiBo.Count > 0 && lKPNoiBo.Contains(nd.MaKPnx ?? 0)))) ? nd.ThanhTienN : 0),

                                 //TongNhapTraLaiSL = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.kq1 == null && nd.TraDuoc_KieuDon != 3 && (nd.TraDuoc_KieuDon != 2 || (nd.TraDuoc_KieuDon == 2 && lKPNoiBoAll.Count > 0 && lKPNoiBoAll.Contains(nd.MaKPnx??0)))) ? nd.SoLuongN : 0),
                                 //TongNhapTraLaiTT = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.kq1 == null && nd.TraDuoc_KieuDon != 3 && (nd.TraDuoc_KieuDon != 2 || (nd.TraDuoc_KieuDon == 2 && lKPNoiBoAll.Count > 0 && lKPNoiBoAll.Contains(nd.MaKPnx??0)))) ? nd.ThanhTienN : 0),
                                 TonCKSL = ((nd.NgayNhap <= denngay && nd.PLoai == 1) ? nd.SoLuongN : 0) - ((nd.NgayNhap <= denngay && (nd.PLoai == 2 || nd.PLoai == 3)) ? nd.SoLuongX : 0),
                                 TonCKTT = ((nd.NgayNhap <= denngay && nd.PLoai == 1) ? nd.ThanhTienN : 0) - ((nd.NgayNhap <= denngay && (nd.PLoai == 2 || nd.PLoai == 3)) ? nd.ThanhTienX : 0),
                             }).ToList();
                #endregion
                #endregion
                #region nxt kho Tồn  kho xã
                var qkhoxa = (from nd in qnd.Where(p => lKhoXa.Contains(p.MaKP))
                              select new BCMoi
                              {
                                  MaDV = nd.MaDV ?? 0,
                                  DonGia = nd.DonGia,
                                  TonDKSL = ((nd.NgayNhap < tungay && nd.PLoai == 1) ? nd.SoLuongN : 0) - ((nd.NgayNhap < tungay && (nd.PLoai == 2 || nd.PLoai == 3) && nd.KieuDon != 3) ? nd.SoLuongX : 0) - ((nd.NgayNhap < tungay && nd.PLoai == 5) ? nd.SoLuongSD : 0),
                                  TonDKTT = ((nd.NgayNhap < tungay && nd.PLoai == 1) ? nd.ThanhTienN : 0) - ((nd.NgayNhap < tungay && (nd.PLoai == 2 || nd.PLoai == 3) && nd.KieuDon != 3) ? nd.ThanhTienX : 0) - ((nd.NgayNhap < tungay && nd.PLoai == 5) ? nd.ThanhTienSD : 0),
                                  NhapSL = 0,
                                  NhapTT = 0,
                                  //XuatBHSL = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.IDDTBN == idDTBH && (nd.KieuDon == 0 || nd.KieuDon == 1 || nd.KieuDon == 4)) ? nd.SoLuongX : 0) + ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 5) ? nd.SoLuongSD : 0),
                                  //XuatBHTT = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.IDDTBN == idDTBH && (nd.KieuDon == 0 || nd.KieuDon == 1 || nd.KieuDon == 4)) ? nd.ThanhTienX : 0) + ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 5) ? nd.ThanhTienSD : 0),
                                  //NhapTraDuocBHSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.IDDTBN == idDTBH && (nd.TraDuoc_KieuDon == 0 || nd.TraDuoc_KieuDon == 1 || nd.TraDuoc_KieuDon == 4)) ? nd.SoLuongN : 0,
                                  //NhapTraDuocBHTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.IDDTBN == idDTBH && (nd.TraDuoc_KieuDon == 0 || nd.TraDuoc_KieuDon == 1 || nd.TraDuoc_KieuDon == 4)) ? nd.ThanhTienN : 0,
                                  //XuatDVSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.IDDTBN == idDTDV && (nd.KieuDon == 0 || nd.KieuDon == 1 || nd.KieuDon == 4)) ? nd.SoLuongX : 0,
                                  //XuatDVTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.IDDTBN == idDTDV && (nd.KieuDon == 0 || nd.KieuDon == 1 || nd.KieuDon == 4)) ? nd.ThanhTienX : 0,
                                  //NhapTraDuocDVSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.IDDTBN == idDTDV && (nd.TraDuoc_KieuDon == 0 || nd.TraDuoc_KieuDon == 1 || nd.TraDuoc_KieuDon == 4)) ? nd.SoLuongN : 0,
                                  //NhapTraDuocDVTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.IDDTBN == idDTDV && (nd.TraDuoc_KieuDon == 0 || nd.TraDuoc_KieuDon == 1 || nd.TraDuoc_KieuDon == 4)) ? nd.ThanhTienN : 0,
                                  //XuatKhacSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.IDDTBN != idDTDV && nd.IDDTBN != idDTBH && nd.KieuDon != 2 && nd.KieuDon != 3) ? nd.SoLuongX : 0,
                                  //XuatKhacTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.IDDTBN != idDTDV && nd.IDDTBN != idDTBH && nd.KieuDon != 2 && nd.KieuDon != 3) ? nd.ThanhTienX : 0,
                                  //NhapTraLaiKhacSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.IDDTBN != idDTDV && nd.IDDTBN != idDTBH && nd.TraDuoc_KieuDon != 2 && nd.TraDuoc_KieuDon != 3) ? nd.SoLuongN : 0,
                                  //NhapTraLaiKhacTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.IDDTBN != idDTDV && nd.IDDTBN != idDTBH && nd.TraDuoc_KieuDon != 2 && nd.TraDuoc_KieuDon != 3) ? nd.ThanhTienN : 0,

                                  XuatBHSL = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 5) ? nd.SoLuongSD : 0),
                                  XuatBHTT = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 5) ? nd.ThanhTienSD : 0),


                                  TongXuatSL = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 5) ? nd.SoLuongSD : 0),
                                  TongXuatTT = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 5) ? nd.ThanhTienSD : 0),

                                  TonCKSL = ((nd.NgayNhap <= denngay && nd.PLoai == 1) ? nd.SoLuongN : 0) - ((nd.NgayNhap <= denngay && (nd.PLoai == 2 || nd.PLoai == 3) && nd.KieuDon != 3) ? nd.SoLuongX : 0) - ((nd.NgayNhap <= denngay && nd.PLoai == 5) ? nd.SoLuongSD : 0),
                                  TonCKTT = ((nd.NgayNhap <= denngay && nd.PLoai == 1) ? nd.ThanhTienN : 0) - ((nd.NgayNhap <= denngay && (nd.PLoai == 2 || nd.PLoai == 3) && nd.KieuDon != 3) ? nd.ThanhTienX : 0) - ((nd.NgayNhap <= denngay && nd.PLoai == 5) ? nd.ThanhTienSD : 0),
                                  NhapTDXPDKSL = ((nd.NgayNhap < tungay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.MaKPnx != null && _lXP.Contains(nd.MaKPnx.Value)) ? nd.SoLuongN : 0),
                                  NhapTDXPDKTT = ((nd.NgayNhap < tungay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.MaKPnx != null && _lXP.Contains(nd.MaKPnx.Value)) ? nd.ThanhTienN : 0),

                                  NhapTDXPCKSL = ((nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.MaKPnx != null && _lXP.Contains(nd.MaKPnx.Value)) ? nd.SoLuongN : 0),
                                  NhapTDXPCKTT = ((nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.MaKPnx != null && _lXP.Contains(nd.MaKPnx.Value)) ? nd.ThanhTienN : 0),
                              }).ToList();

                qkhoxa = (from nd in qkhoxa
                          select new BCMoi
                          {
                              MaDV = nd.MaDV,
                              DonGia = nd.DonGia,
                              TonDKSL = nd.TonDKSL - nd.NhapTDXPDKSL,
                              TonDKTT = nd.TonDKTT - nd.NhapTDXPDKTT,
                              NhapSL = 0,
                              NhapTT = 0,
                              XuatBHSL = nd.XuatBHSL,
                              XuatBHTT = nd.XuatBHTT,
                              NhapTraDuocBHSL = nd.NhapTraDuocBHSL,
                              NhapTraDuocBHTT = nd.NhapTraDuocBHTT,
                              XuatDVSL = nd.XuatDVSL,
                              XuatDVTT = nd.XuatDVTT,
                              NhapTraDuocDVSL = nd.NhapTraDuocDVSL,
                              NhapTraDuocDVTT = nd.NhapTraDuocDVTT,
                              XuatKhacSL = nd.XuatKhacSL,
                              XuatKhacTT = nd.XuatKhacTT,
                              TongXuatSL = nd.TongXuatSL,
                              TongXuatTT = nd.TongXuatTT,
                              TonCKSL = nd.TonCKSL - nd.NhapTDXPCKSL,
                              TonCKTT = nd.TonCKTT - nd.NhapTDXPCKTT,
                          }).ToList();
                #endregion

                #region nxt kho Tồn phòng khám Sặt
                var qpkSat = (from nd in qnd.Where(p => lPKham.Contains(p.MaKP))
                              select new BCMoi
                              {
                                  MaDV = nd.MaDV ?? 0,
                                  DonGia = nd.DonGia,
                                  TonDKSL = ((nd.NgayNhap < tungay && nd.PLoai == 1) ? nd.SoLuongN : 0) - ((nd.NgayNhap < tungay && (nd.PLoai == 2 || nd.PLoai == 3)) ? nd.SoLuongX : 0),
                                  TonDKTT = ((nd.NgayNhap < tungay && nd.PLoai == 1) ? nd.ThanhTienN : 0) - ((nd.NgayNhap < tungay && (nd.PLoai == 2 || nd.PLoai == 3)) ? nd.ThanhTienX : 0),
                                  NhapSL = 0,
                                  NhapTT = 0,
                                  XuatBHSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.IDDTBN != idDTDV && nd.KieuDon != 2) ? nd.SoLuongX : 0,
                                  XuatBHTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.IDDTBN != idDTDV && nd.KieuDon != 2) ? nd.ThanhTienX : 0,
                                  NhapTraDuocBHSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.IDDTBN != idDTDV && nd.TraDuoc_KieuDon != 2) ? nd.SoLuongN : 0,
                                  NhapTraDuocBHTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.IDDTBN != idDTDV && nd.TraDuoc_KieuDon != 2) ? nd.ThanhTienN : 0,
                                  XuatDVSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.IDDTBN == idDTDV && (nd.KieuDon == 0 || nd.KieuDon == 1 || nd.KieuDon == 4)) ? nd.SoLuongX : 0,
                                  XuatDVTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.IDDTBN == idDTDV && (nd.KieuDon == 0 || nd.KieuDon == 1 || nd.KieuDon == 4)) ? nd.ThanhTienX : 0,
                                  NhapTraDuocDVSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.IDDTBN == idDTDV && (nd.TraDuoc_KieuDon == 0 || nd.TraDuoc_KieuDon == 1 || nd.TraDuoc_KieuDon == 4)) ? nd.SoLuongN : 0,
                                  NhapTraDuocDVTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.IDDTBN == idDTDV && (nd.TraDuoc_KieuDon == 0 || nd.TraDuoc_KieuDon == 1 || nd.TraDuoc_KieuDon == 4)) ? nd.ThanhTienN : 0,
                                  //XuatKhacSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.IDDTBN != idDTDV && nd.IDDTBN != idDTBH && nd.KieuDon != 2 && nd.KieuDon != 3) ? nd.SoLuongX : 0,
                                  //XuatKhacTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 2 && nd.IDDTBN != idDTDV && nd.IDDTBN != idDTBH && nd.KieuDon != 2 && nd.KieuDon != 3) ? nd.ThanhTienX : 0,
                                  //NhapTraLaiKhacSL = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.IDDTBN != idDTDV && nd.IDDTBN != idDTBH && nd.TraDuoc_KieuDon != 2 && nd.TraDuoc_KieuDon != 3) ? nd.SoLuongN : 0,
                                  //NhapTraLaiKhacTT = (nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.IDDTBN != idDTDV && nd.IDDTBN != idDTBH && nd.TraDuoc_KieuDon != 2 && nd.TraDuoc_KieuDon != 3) ? nd.ThanhTienN : 0,
                                  TongXuatSL = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && (nd.PLoai == 2 && nd.KieuDon != 2)) ? nd.SoLuongX : 0),
                                  TongXuatTT = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && (nd.PLoai == 2 && nd.KieuDon != 2)) ? nd.ThanhTienX : 0),
                                  TongNhapTraLaiSL = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.TraDuoc_KieuDon != 2) ? nd.SoLuongN : 0),
                                  TongNhapTraLaiTT = ((nd.NgayNhap >= tungay && nd.NgayNhap <= denngay && nd.PLoai == 1 && nd.KieuDon == 2 && nd.TraDuoc_KieuDon != 2) ? nd.ThanhTienN : 0),
                                  TonCKSL = ((nd.NgayNhap <= denngay && nd.PLoai == 1) ? nd.SoLuongN : 0) - ((nd.NgayNhap <= denngay && (nd.PLoai == 2 || nd.PLoai == 3)) ? nd.SoLuongX : 0),
                                  TonCKTT = ((nd.NgayNhap <= denngay && nd.PLoai == 1) ? nd.ThanhTienN : 0) - ((nd.NgayNhap <= denngay && (nd.PLoai == 2 || nd.PLoai == 3)) ? nd.ThanhTienX : 0),

                              }).ToList();
                #endregion

                List<BCMoi> listBC = new List<BCMoi>();
                listBC.AddRange(qngtru);
                listBC.AddRange(qntru);
                listBC.AddRange(qkhoxa);
                listBC.AddRange(qpkSat);
                List<DichVu> ldv = data.DichVus.Where(p => p.PLoai == 1).ToList();

                listBC = (from l in listBC
                          group l by new { l.MaDV, l.DonGia, l.DonVi } into kq
                          select new BCMoi
                          {
                              MaDV = kq.Key.MaDV,
                              DonGia = kq.Key.DonGia,
                              TonDKSL = kq.Sum(p => p.TonDKSL),
                              TonDKTT = kq.Sum(p => p.TonDKTT),
                              NhapSL = kq.Sum(p => p.NhapSL),
                              NhapTT = kq.Sum(p => p.NhapTT),
                              NhapTuTuTrucSL = kq.Sum(p => p.NhapTuTuTrucSL) + kq.Sum(p => p.NhapTuTuTrucNgoaiTruSL),
                              NhapTuTuTrucTT = kq.Sum(p => p.NhapTuTuTrucTT) + kq.Sum(p => p.NhapTuTuTrucNgoaiTruTT),
                              XuatBHSL = kq.Sum(p => p.XuatBHSL) - kq.Sum(p => p.NhapTraDuocBHSL),
                              XuatBHTT = kq.Sum(p => p.XuatBHTT) - kq.Sum(p => p.NhapTraDuocBHTT),
                              XuatDVSL = kq.Sum(p => p.XuatDVSL) - kq.Sum(p => p.NhapTraDuocDVSL),
                              XuatDVTT = kq.Sum(p => p.XuatDVTT) - kq.Sum(p => p.NhapTraDuocDVTT),
                              XuatKhacSL = kq.Sum(p => p.XuatKhacSL) - kq.Sum(p => p.NhapTraLaiKhacSL) - kq.Sum(p => p.NhapTuTuTrucNgoaiTruSL),
                              XuatKhacTT = kq.Sum(p => p.XuatKhacTT) - kq.Sum(p => p.NhapTraLaiKhacTT) - kq.Sum(p => p.NhapTuTuTrucNgoaiTruTT),
                              TongXuatSL = kq.Sum(p => p.TongXuatSL) - kq.Sum(p => p.TongNhapTraLaiSL) - kq.Sum(p => p.NhapTuTuTrucNgoaiTruSL),//+ kq.Sum(p => p.NhapTuTuTrucSL),
                              TongXuatTT = kq.Sum(p => p.TongXuatTT) - kq.Sum(p => p.TongNhapTraLaiTT) - kq.Sum(p => p.NhapTuTuTrucNgoaiTruTT),//+ kq.Sum(p => p.NhapTuTuTrucTT),
                              TonCKSL = kq.Sum(p => p.TonCKSL),
                              TonCKTT = kq.Sum(p => p.TonCKTT)
                          }).ToList();

                int idnhom = -1, idtieunhom = -1;
                if (cboNhom.EditValue != null)
                    idnhom = Convert.ToInt32(cboNhom.EditValue);
                if (cboTieuNhom.EditValue != null)
                    idtieunhom = Convert.ToInt32(cboTieuNhom.EditValue);

                listBC = (from l in listBC
                          join dv in ldv on l.MaDV equals dv.MaDV
                          join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                          join ndv in data.NhomDVs on tn.IDNhom equals ndv.IDNhom
                          select new BCMoi
                          {
                              IdTieuNhom = tn.IdTieuNhom,
                              IDNhom = ndv.IDNhom,
                              MaDV = l.MaDV,
                              DonVi = dv.DonVi,
                              DonGia = l.DonGia,
                              TenDV = dv.TenDV,
                              TonDKSL = l.TonDKSL,
                              TonDKTT = l.TonDKTT,
                              NhapSL = l.NhapSL,
                              NhapTT = l.NhapTT,
                              NhapTuTuTrucSL = l.NhapTuTuTrucSL,
                              NhapTuTuTrucTT = l.NhapTuTuTrucTT,
                              XuatBHSL = l.XuatBHSL,
                              XuatBHTT = l.XuatBHTT,
                              XuatDVSL = l.XuatDVSL,
                              XuatDVTT = l.XuatDVTT,
                              XuatKhacSL = l.XuatKhacSL,
                              XuatKhacTT = l.XuatKhacTT,
                              TongXuatSL = l.TongXuatSL,
                              TongXuatTT = l.TongXuatTT,
                              TonCKSL = l.TonCKSL,
                              TonCKTT = l.TonCKTT,
                              TenTN = tn.TenTN,
                              TenNhomDuoc = ndv.TenNhom
                          }).Where(p => ((p.TonDKSL != 0 || p.TonCKSL != 0) && !ckHthi.Checked) || p.NhapSL != 0 || p.XuatBHSL != 0 || p.XuatDVSL != 0 || p.XuatKhacSL != 0 || p.TongXuatSL != 0).Where(p => (idnhom == -1 ? true : p.IDNhom == idnhom) && (idtieunhom == -1 ? true : p.IdTieuNhom == idtieunhom)).ToList();
                frmIn frm = new frmIn();
                QLBV.BaoCao.rep_BaoCaoNXT_30002_Moi rep = new BaoCao.rep_BaoCaoNXT_30002_Moi();
                rep.celNgay.Text = "Từ ngày " + tungay.ToShortDateString() + " đến ngày " + denngay.ToShortDateString();
                rep.DataSource = listBC.OrderBy(p => p.TenDV);
                rep.databinding();
                rep.CreateDocument();
                //rep.SaveLayout(Path.Combine(Application.StartupPath + DungChung.Bien.ReportPath, rep.Name + ".repx"));
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
                #endregion
            }

        }
        private class BC
        {
            public DateTime NgayNhap { set; get; }
            public int MaDV { set; get; }
            public string TenDV { set; get; }
            public double TonDKSL { set; get; }
            public double TonDKTT { set; get; }
            public double NhapTKSL { set; get; }// tổng nhập trong kỳ về kho tổng
            public double NhapTKTT { set; get; }

            public double NhapTheoHDSL { set; get; }// nhập theo hóa đơn
            public double NhapTheoHDTT { set; get; }

            public double NhapChuyenKhoSL { set; get; }// nhập từ kho ngoại trú về kho nội trú (kho nội bộ khác về kho tổng)
            public double NhapChuyenKhoTT { set; get; }
            public double XuatBNSL { set; get; }//xuất từ kho nội trú cho bệnh nhân
            public double XuatBNTT { set; get; }

            public double XuatSuDungSL { set; get; }// xuất của kho xã cho bệnh nhân
            public double XuatSuDungTT { set; get; }

            public double XuatTKSL { set; get; }// tổng xuất trong kỳ từ kho tổng = tổng xuất từ kho tổng cho bệnh nhân và từ kho xã cho bệnh nhân
            public double XuatTKTT { set; get; }

            public double TonCKSL { set; get; }
            public double TonCKTT { set; get; }

            public string DonVi { get; set; }

            public double DonGia { get; set; }

            public double XuatNgTSL { get; set; }

            public double XuatNgTTT { get; set; }

            public double XuatKhacSL { get; set; }

            public double XuatKhacTT { get; set; }

            public double TongNhapSL { get; set; }

            public double TongNhapTT { get; set; }

            public double TongXuatSL { get; set; }

            public double TongXuatTT { get; set; }

            public double NhapKhacSL { get; set; }

            public double NhapKhacTT { get; set; }
        }

        private class BCMoi
        {
            public int? IDNhom { get; set; }
            public int? IdTieuNhom { get; set; }
            public double NhapTraDuocDVTT;
            public int MaDV { set; get; }
            public string TenDV { set; get; }
            public string TenNhomDuoc { get; set; }
            public string TenTN { get; set; }
            public string DonVi { set; get; }
            public double DonGia { set; get; }
            public double TonDKSL { set; get; }
            public double TonDKTT { set; get; }
            public double TonCKSL { set; get; }
            public double TonCKTT { set; get; }
            public double NhapSL { set; get; }
            public double NhapTT { set; get; }
            public double XuatBHSL { set; get; }
            public double XuatBHTT { set; get; }
            public double XuatDVSL { set; get; }
            public double XuatDVTT { set; get; }
            public double TongXuatSL { set; get; }
            public double TongXuatTT { set; get; }


            public byte IDDTBN { get; set; }

            public double XuatKhacSL { get; set; }

            public double XuatKhacTT { get; set; }

            public double NhapTraDuocBHSL { get; set; }

            public double NhapTraDuocBHTT { get; set; }

            public double NhapTraDuocDVSL { get; set; }

            public double NhapTraLaiKhacSL { get; set; }

            public double NhapTraLaiKhacTT { get; set; }

            public double TongNhapTraLaiSL { get; set; }

            public double TongNhapTraLaiTT { get; set; }

            public double NhapTuTuTrucSL { get; set; }

            public double NhapTuTuTrucTT { get; set; }

            public double NhapTDXPDKSL { get; set; }

            public double NhapTDXPDKTT { get; set; }

            public double NhapTDXPCKSL { get; set; }

            public double NhapTDXPCKTT { get; set; }

            public double NhapTuTuTrucNgoaiTruSL { get; set; }

            public double NhapTuTuTrucNgoaiTruTT { get; set; }
        }

        private void labelControl10_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void memoEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void cboNhom_EditValueChanged(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<TieuNhomDV> _ltnhom = new List<TieuNhomDV>();
            int id = -1;
            if (cboNhom.EditValue != null)
                id = Convert.ToInt32(cboNhom.EditValue);
            _ltnhom = data.TieuNhomDVs.Where(p => p.Status == 1).ToList();
            _ltnhom.Add(new TieuNhomDV { IdTieuNhom = -1, TenTN = " Tất cả" });
            if (id >= 0)
                _ltnhom = _ltnhom.Where(p => p.IDNhom == id).ToList();
            cboTieuNhom.Properties.DataSource = _ltnhom.OrderBy(p => p.TenTN).ToList();
        }
    }
}