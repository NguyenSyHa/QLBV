using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using COMExcel = Microsoft.Office.Interop.Excel;
namespace QLBV.FormThamSo
{
    public partial class frm_BcVTYTKoTT : DevExpress.XtraEditors.XtraForm
    {
        public frm_BcVTYTKoTT()
        {
            InitializeComponent();
        }

        private bool KTtaoBcMau20()
        {
            
           if (lupTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupTuNgay.Focus();
                return false;
            }
            if (lupDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
                return false;
            }
            else return true;
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

        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void btnInBC_Click(object sender, EventArgs e)
        {
            int _trongBH = -1;
            _trongBH = cboTrongBH.SelectedIndex;
            string _doituong="";
            //int macc = 0;
            DateTime ngaytu = System.DateTime.Now.Date;
            DateTime ngayden = System.DateTime.Now.Date;
            if (cboDoiTuong.SelectedIndex != 2)
                _doituong = cboDoiTuong.Text;
            int  _noitru=-1;
            int trongBH = 0;
            for (int i = 0; i < ckckphong.ItemCount; i++)
            {
                if (ckckphong.GetItemChecked(i))
                {
                    int makp = Convert.ToInt32(ckckphong.GetItemValue(i));
                    foreach (var item in _lkpc)
                    {
                        if (item.MakP == makp && item.MakP != 0)
                        {
                            item.CHon = true;
                            break;
                        }
                    }
                }
            }
           
            //if (lupNhaCC.EditValue != null && lupNhaCC.EditValue.ToString() != "")
            //    macc =Convert.ToInt32(lupNhaCC.EditValue);
            if (KTtaoBcMau20())
            {
                ngaytu = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                _noitru = radNoiTru.SelectedIndex;
                string _nhom = "";
                if (lupNhomDuoc.EditValue != null)
                    _nhom = lupNhomDuoc.EditValue.ToString();
                var _ldichvu = (from dv in data.DichVus.Where(p => p.PLoai == 1)
                                join nhomdv in data.NhomDVs.Where(p => p.TenNhomCT.Contains(_nhom)) on dv.IDNhom equals nhomdv.IDNhom
                                select new { dv.MaDV, dv.TenDV, nhomdv.IDNhom, dv.HamLuong, dv.SoDK, nhomdv.TenNhom, }).ToList();
                if (chkDSBN.Checked)
                {
                    frmIn frm = new frmIn();
                    BaoCao.rep_BcVTYTKoTT_DSBN rep = new BaoCao.rep_BcVTYTKoTT_DSBN();
                    rep.TuNgayDenNgay.Value = "Từ ngày: " + ngaytu.ToString().Substring(0, 10) + " đến ngày: " + ngayden.ToString().Substring(0, 10);

                    if (_lkpc.Where(p=>p.CHon==true).Count()> 0)
                    {
                        if (_lkpc.Where(p => p.CHon == true).Count()==1)
                            rep.NhaCC.Value = _lkpc.First().TenKP;
                        //else
                        //    rep.NhaCC.Value = "Tất cả";
                        var _lbn = (from bn in data.BenhNhans.Where(p => p.DTuong.Contains(_doituong))
                                    where (_noitru == 2 ? true : bn.NoiTru == _noitru)
                                    join dt in data.DThuocs.Where(p => p.NgayKe >= ngaytu && p.NgayKe <= ngayden) on bn.MaBNhan equals dt.MaBNhan
                                    join dtct in data.DThuoccts.Where(p => _trongBH == 3 ? (p.TrongBH != 3) : p.TrongBH == _trongBH) on dt.IDDon equals dtct.IDDon
                                    select new { bn.MaBNhan, bn.TenBNhan, dtct.DonVi, dt.MaKP, dtct.SoLuong, dtct.ThanhTien, dtct.MaDV, dtct.DonGia }).ToList();
                        if(radioGroup1.SelectedIndex == 0)
                        {
                             _lbn = (from bn in data.BenhNhans.Where(p => p.DTuong.Contains(_doituong))
                                        where (_noitru == 2 ? true : bn.NoiTru == _noitru)
                                     join dt in data.VienPhis.Where(p => p.NgayTT >= ngaytu && p.NgayTT <= ngayden) on bn.MaBNhan equals dt.MaBNhan
                                     join dtct in data.VienPhicts.Where(p => _trongBH == 3 ? (p.TrongBH != 3) : p.TrongBH == _trongBH) on dt.idVPhi equals dtct.idVPhi
                                     select new { bn.MaBNhan, bn.TenBNhan, dtct.DonVi, dtct.MaKP, dtct.SoLuong, dtct.ThanhTien, dtct.MaDV, dtct.DonGia }).ToList();
                        }
                        
                        var q = (from bn in _lbn
                                 join kp in _lkpc.Where(p => p.CHon == true) on bn.MaKP equals kp.MakP
                                 join dv in _ldichvu on bn.MaDV equals dv.MaDV
                                 //    in data.BenhNhans.Where(p => p.DTuong.Contains(_doituong))
                                 //where (_noitru == 2 ? true : bn.NoiTru == _noitru)
                                 //join dt in data.DThuocs.Where(p=>p.NgayKe >= ngaytu && p.NgayKe <= ngayden).Where(p => p.MaKP == (macc)) on bn.MaBNhan equals dt.MaBNhan
                                 //join dtct in data.DThuoccts.Where(p => _trongBH == 3 ? (p.TrongBH != 3) : p.TrongBH == _trongBH) on dt.IDDon equals dtct.IDDon
                                 //join dv in data.DichVus.Where(p => p.PLoai == 1) on dtct.MaDV equals dv.MaDV
                                 //join nhomdv in data.NhomDVs.Where(p => p.TenNhomCT.Contains(_nhom)) on dv.IDNhom equals nhomdv.IDNhom
                                 group new { bn, dv } by new { bn.MaBNhan, bn.TenBNhan, dv.MaDV, dv.TenDV, dv.HamLuong, bn.DonVi, bn.DonGia, dv.SoDK } into kq
                                 select new
                                 {
                                     kq.Key.MaBNhan,
                                     TenNhomThuoc = kq.Key.TenBNhan,
                                     TenThuoc = kq.Key.TenDV,
                                     HamLuong = kq.Key.HamLuong,
                                     DonVi = kq.Key.DonVi,
                                     DonGia = kq.Key.DonGia,
                                     SoDK = kq.Key.SoDK,
                                     MaDV = kq.Key.MaDV,
                                     SoLuong = kq.Sum(p => p.bn.SoLuong),
                                     ThanhTien = kq.Sum(p => p.bn.ThanhTien),
                                     ThanhTienTong = kq.Sum(p => p.bn.ThanhTien)
                                 }).OrderBy(p => p.TenThuoc).ToList();
                        //if (q.Count > 0)
                        //{
                        rep.DataSource = q.ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    //else
                    //{
                    //    rep.NhaCC.Value = "Tất cả";
                    //    var q = (from bn in data.BenhNhans.Where(p => p.DTuong.Contains(_doituong))
                    //             where (_noitru == 2 ? true : bn.NoiTru == _noitru)
                    //             join dt in data.DThuocs on bn.MaBNhan equals dt.MaBNhan
                    //             join dtct in data.DThuoccts.Where(p => _trongBH == 3 ? (p.TrongBH != 3) : p.TrongBH == _trongBH) on dt.IDDon equals dtct.IDDon
                    //             where (dt.NgayKe >= ngaytu && dt.NgayKe <= ngayden)
                    //             join dv in data.DichVus.Where(p => p.PLoai == 1) on dtct.MaDV equals dv.MaDV
                    //             join nhomdv in data.NhomDVs.Where(p => p.TenNhomCT.Contains(_nhom)) on dv.IDNhom equals nhomdv.IDNhom
                    //             group new { bn, dv, dtct } by new { bn.MaBNhan, bn.TenBNhan, dv.MaDV, dv.TenDV, dv.HamLuong, dtct.DonVi, dtct.DonGia, dv.SoDK } into kq
                    //             select new
                    //             {
                    //                 kq.Key.MaBNhan,
                    //                 TenNhomThuoc = kq.Key.TenBNhan,
                    //                 TenThuoc = kq.Key.TenDV,
                    //                 HamLuong = kq.Key.HamLuong,
                    //                 DonVi = kq.Key.DonVi,
                    //                 DonGia = kq.Key.DonGia,
                    //                 SoDK = kq.Key.SoDK,
                    //                 MaDV = kq.Key.MaDV,
                    //                 SoLuong = kq.Sum(p => p.dtct.SoLuong),
                    //                 ThanhTien = kq.Sum(p => p.dtct.ThanhTien),
                    //                 ThanhTienTong = kq.Sum(p => p.dtct.ThanhTien)

                    //             }).OrderBy(p => p.TenThuoc).ToList();
                    //    //if (q.Count > 0)
                    //    //{
                    //    rep.DataSource = q.ToList();
                    //    rep.BindingData();
                    //    rep.CreateDocument();
                    //    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    //    frm.ShowDialog();
                    //}
                }
                else
                {
                    frmIn frm = new frmIn();
                    BaoCao.rep_BcVTYTKoTT rep = new BaoCao.rep_BcVTYTKoTT();
                    rep.TuNgayDenNgay.Value = "Từ ngày: " + ngaytu.ToString().Substring(0, 10) + " đến ngày: " + ngayden.ToString().Substring(0, 10);
                 
                    //rep.Quy.Value = lupTuNgay.Text;
                    //rep.TuNgayDenNgay.Value = lupDenNgay.Text;
                    if (_lkpc.Where(p => p.CHon == true).Count() > 0)
                    {

                        if (_lkpc.Where(p => p.CHon == true).Count() == 1)
                            rep.NhaCC.Value = _lkpc.First().TenKP;
                            var _lbn = (from bn in data.BenhNhans.Where(p => p.DTuong.Contains(_doituong))
                                        where (_noitru == 2 ? true : bn.NoiTru == _noitru)
                                        join dt in data.DThuocs on bn.MaBNhan equals dt.MaBNhan
                                        join dtct in data.DThuoccts.Where(p => _trongBH == 3 ? (p.TrongBH != 3) : p.TrongBH == _trongBH) on dt.IDDon equals dtct.IDDon
                                        where (dt.NgayKe >= ngaytu && dt.NgayKe <= ngayden)
                                        select new { bn.MaBNhan, bn.TenBNhan, dtct.DonVi, dt.MaKP, dtct.SoLuong, dtct.ThanhTien, dtct.MaDV, dtct.DonGia }).ToList();
                        if(radioGroup1.SelectedIndex == 0)
                        {
                            _lbn = (from bn in data.BenhNhans.Where(p => p.DTuong.Contains(_doituong))
                                    where (_noitru == 2 ? true : bn.NoiTru == _noitru)
                                    join dt in data.VienPhis.Where(p => p.NgayTT >= ngaytu && p.NgayTT <= ngayden) on bn.MaBNhan equals dt.MaBNhan
                                    join dtct in data.VienPhicts.Where(p => _trongBH == 3 ? (p.TrongBH != 3) : p.TrongBH == _trongBH) on dt.idVPhi equals dtct.idVPhi
                                    select new { bn.MaBNhan, bn.TenBNhan, dtct.DonVi, dtct.MaKP, dtct.SoLuong, dtct.ThanhTien, dtct.MaDV, dtct.DonGia }).ToList();
                        }
                            var q = (from bn in _lbn
                                     join kp in _lkpc.Where(p=>p.CHon==true) on bn.MaKP equals kp.MakP
                                     join dv in _ldichvu on bn.MaDV equals dv.MaDV
                                     //    in data.BenhNhans.Where(p => p.DTuong.Contains(_doituong))
                                     //where (_noitru == 2 ? true : bn.NoiTru == _noitru)
                                     //join dt in data.DThuocs.Where(p => p.MaKP == (macc)) on bn.MaBNhan equals dt.MaBNhan
                                     //join dtct in data.DThuoccts.Where(p => p.TrongBH == 2) on dt.IDDon equals dtct.IDDon
                                     //where (dt.NgayKe >= ngaytu && dt.NgayKe <= ngayden)
                                     //join dv in data.DichVus.Where(p => p.PLoai == 1) on dtct.MaDV equals dv.MaDV
                                     //join nhomdv in data.NhomDVs.Where(p => p.TenNhomCT.Contains(_nhom)) on dv.IDNhom equals nhomdv.IDNhom
                                     group new { dv, bn } by new { dv.IDNhom, dv.TenNhom, bn.MaDV, dv.TenDV, dv.HamLuong, bn.DonVi, bn.DonGia, dv.SoDK } into kq
                                     select new
                                     {
                                         kq.Key.IDNhom,
                                         TenNhomThuoc = kq.Key.TenNhom,
                                         TenThuoc = kq.Key.TenDV,
                                         HamLuong = kq.Key.HamLuong,
                                         DonVi = kq.Key.DonVi,
                                         DonGia = kq.Key.DonGia,
                                         SoDK = kq.Key.SoDK,
                                         MaDV = kq.Key.MaDV,
                                         SoLuong = kq.Sum(p => p.bn.SoLuong),
                                         ThanhTien = kq.Sum(p => p.bn.ThanhTien),
                                         ThanhTienTong = kq.Sum(p => p.bn.ThanhTien)

                                     }).OrderBy(p => p.TenThuoc).ToList();
                            //if (q.Count > 0)
                            //{
                            rep.DataSource = q.ToList();
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                       
                       
                    }
                    //else
                    //{
                    //    rep.NhaCC.Value = lupNhaCC.Text;


                    //    var q = (from bn in data.BenhNhans.Where(p => p.DTuong.Contains(_doituong))
                    //             where (_noitru == 2 ? true : bn.NoiTru == _noitru)
                    //             join dt in data.DThuocs.Where(p => p.PLDV == 1) on bn.MaBNhan equals dt.MaBNhan
                    //             join dtct in data.DThuoccts.Where(p => p.TrongBH == 2) on dt.IDDon equals dtct.IDDon
                    //             where (dt.NgayKe >= ngaytu && dt.NgayKe <= ngayden)
                    //             join dv in data.DichVus.Where(p => p.PLoai == 1) on dtct.MaDV equals dv.MaDV
                    //             join nhomdv in data.NhomDVs.Where(p => p.TenNhomCT.Contains(_nhom)) on dv.IDNhom equals nhomdv.IDNhom
                    //             group new { dv, dtct } by new { nhomdv.IDNhom, nhomdv.TenNhom, dv.MaDV, dv.TenDV, dv.HamLuong, dtct.DonVi, dtct.DonGia, dv.SoDK } into kq
                    //             select new
                    //             {
                    //                 kq.Key.IDNhom,
                    //                 TenNhomThuoc = kq.Key.TenNhom,
                    //                 TenThuoc = kq.Key.TenDV,
                    //                 HamLuong = kq.Key.HamLuong,
                    //                 DonVi = kq.Key.DonVi,
                    //                 DonGia = kq.Key.DonGia,
                    //                 SoDK = kq.Key.SoDK,
                    //                 MaDV = kq.Key.MaDV,
                    //                 SoLuong = kq.Sum(p => p.dtct.SoLuong),
                    //                 ThanhTien = kq.Sum(p => p.dtct.ThanhTien),
                    //                 ThanhTienTong = kq.Sum(p => p.dtct.ThanhTien)

                    //             }).OrderBy(p => p.TenThuoc).ToList();
                    //        //if (q.Count > 0)
                    //        //{
                    //        rep.DataSource = q.ToList();
                    //        rep.BindingData();
                    //        rep.CreateDocument();
                    //        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    //        frm.ShowDialog();
                        
                        

                       
                    //}

                    //}
                    //else
                    //{
                    //    MessageBox.Show("Không có dữ liệu");
                    //}
                }
            }

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private class c_kphong
        {
            public string TenKP { get; set; }
            public int MakP { get; set; }
            public bool CHon { get; set; }
        }
        List<c_kphong> _lkpc = new List<c_kphong>();
        private void frmTsBcMau20_Load(object sender, EventArgs e)
        {
            var _lkp = (from kp in data.KPhongs where (kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám") select kp).ToList();
            //lupNhaCC.Properties.DataSource = nhacc;
            lupDenNgay.DateTime = System.DateTime.Now;
            lupTuNgay.DateTime = System.DateTime.Now;
            List<NhomDV> _lnhomdc=new List<NhomDV>();
            _lnhomdc=data.NhomDVs.Where(p=>p.Status==1).ToList();
            _lnhomdc.Add(new NhomDV{TenNhom="Tất cả",TenNhomCT=""});
            lupNhomDuoc.Properties.DataSource = _lnhomdc;
            //radNoiTru.SelectedIndex = 2;
            //var _lkp = q.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang).OrderBy(p => p.TenKP).ToList();
            if (_lkp.Count > 0)
            {
                c_kphong moi1 = new c_kphong();
                moi1.TenKP = "Chọn tất cả";
                moi1.MakP = 0;
                moi1.CHon = true;
                _lkpc.Add(moi1);
                foreach (var item in _lkp)
                {
                    c_kphong moi = new c_kphong();
                    moi.TenKP = item.TenKP;
                    moi.MakP = item.MaKP;
                    moi.CHon = true;
                    _lkpc.Add(moi);
                }
                ckckphong.DataSource = _lkpc;
            }
            ckckphong.CheckAll();
        }

        private void ckckphong_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (Convert.ToInt32(ckckphong.SelectedValue) == 0)
            {
                if (ckckphong.GetItemChecked(ckckphong.SelectedIndex))
                {
                    ckckphong.CheckAll();

                }
                else
                {
                    ckckphong.UnCheckAll();
                }
            }
            for (int i = 0; i < ckckphong.ItemCount; i++)
            {
                int makp = Convert.ToInt32(ckckphong.GetItemValue(i));
                if (ckckphong.GetItemChecked(i))
                {

                    foreach (var item in _lkpc)
                    {
                        if (item.MakP == makp && item.MakP != 0)
                        {
                            item.CHon = true;
                            //break;
                        }
                    }
                }
                else
                {
                    foreach (var item in _lkpc)
                    {
                        if (item.MakP == makp || item.MakP == 0)
                        {
                            item.CHon = false;
                            // break;
                        }
                    }
                }
            }
        }

               
    }
}