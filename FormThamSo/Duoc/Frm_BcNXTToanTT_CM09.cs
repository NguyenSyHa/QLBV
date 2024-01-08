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
    public partial class Frm_BcNXTToanTT_CM09 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcNXTToanTT_CM09()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private bool KTtaoBcNXT()
        {
            if (dateTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                dateTuNgay.Focus();
                return false;
            }
            if (dateDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                dateDenNgay.Focus();
                return false;
            }
            if (lupKho.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn kho để tổng hợp BC");
                lupKho.Focus();
                return false;
            }
            if (lupPL.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn phân loại dược");
                lupPL.Focus();
                return false;
            }
            return true;
        }
        private class KPhong
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
        //private class PL
        //{
        //    private string TenPL;
        //    public string tenpl
        //    { set { TenPL = value; } get { return TenPL; } }
        
        //}
        List<KPhong> _Kphong = new List<KPhong>();
       
        private void Frm_BcNXTToanTT_CM09_Load(object sender, EventArgs e)
        {
            dateTuNgay.DateTime = System.DateTime.Now;
            dateDenNgay.DateTime = System.DateTime.Now;
            var cc = data.NhaCCs.OrderBy(p => p.TenCC).ToList();
            lupNhaCC.Properties.DataSource = cc.ToList();
            _Kphong.Clear();
           
            var q = from TK in data.KPhongs.Where(p => p.PLoai== ("Khoa dược")) select new { TK.TenKP, TK.MaKP };
            lupKho.Properties.DataSource = q.ToList();
            //  lupPL.EditValue = "Thuốc";

            List<NhomDV> _Ploai = new List<NhomDV>();
            //var aa = (from pl in data.NhomDVs.Where(p => p.Status == 1) select new { pl.IDNhom, pl.TenNhomCT });
            _Ploai = (from pl in data.NhomDVs.Where(p => p.Status == 1) select pl).ToList();
            _Ploai.Add(new NhomDV { TenNhomCT = " ", Status = 1 }); //
            lupPL.Properties.DataSource = _Ploai.OrderBy(p => p.TenNhomCT).ToList();

            var kphong = (from kp in data.KPhongs.Where(p => p.PLoai== ("Khoa dược")) select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                KPhong themmoi1 = new KPhong();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = true;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhong themmoi = new KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }
     
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
      
        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;


            if (KTtaoBcNXT())
            {

                tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
                denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;
                string _macc = "";
                if (lupNhaCC.EditValue != null && lupNhaCC.EditValue.ToString() != "")
                    _macc = lupNhaCC.EditValue.ToString();
                frmIn frm = new frmIn();
                BaoCao.Rep_BcNXTToanTT_CM09 rep = new BaoCao.Rep_BcNXTToanTT_CM09(chkHienThi.Checked);

                rep.TuNgay.Value = dateTuNgay.Text;
                rep.DenNgay.Value = dateDenNgay.Text;
                rep.TuNgayDenNgay.Value = "Từ ngày " + dateTuNgay.Text + " Đến ngày " + dateDenNgay.Text;
                rep.PhanLoai.Value = ("Báo cáo xuất nhập - sử dụng " + lupPL.Text + " toàn trung tâm").ToUpper();

                int _kho = 0;
                if (lupKho.EditValue != null)
                    _kho = Convert.ToInt32( lupKho.EditValue);
                rep.MaKP.Value = _kho;
                int _MaKP1 = 0;
                int _MaKP2 = 0;
                int _MaKP3 = 0;
                int _MaKP4 = 0;
                int _MaKP5 = 0;
                int _MaKP6 = 0;
                int _MaKP7 = 0;
                int _MaKP8 = 0;
                int _MaKP9 = 0;
                int _MaKP10 = 0;
              
         
               
                for (int i = 0; i < _Kphong.Count; i++)
                {
                    if (_Kphong.Skip(i).First().chon == true)
                    {
                        switch (i)
                        {
                            case 0:
                                _MaKP1 = _Kphong.Skip(i).First().makp;
                                break;
                            case 1:
                                _MaKP2 = _Kphong.Skip(i).First().makp;
                                break;
                            case 2:
                                _MaKP3 = _Kphong.Skip(i).First().makp;
                                break;
                            case 3:
                                _MaKP4 = _Kphong.Skip(i).First().makp;
                                break;
                            case 4:
                                _MaKP5 = _Kphong.Skip(i).First().makp;
                                break;
                            case 5:
                                _MaKP6 = _Kphong.Skip(i).First().makp;
                                break;
                            case 6:
                                _MaKP7 = _Kphong.Skip(i).First().makp;
                                break;
                            case 7:
                                _MaKP8 = _Kphong.Skip(i).First().makp;
                                break;
                            case 8:
                                _MaKP9 = _Kphong.Skip(i).First().makp;
                                break;
                            case 9:
                                _MaKP10 = _Kphong.Skip(i).First().makp;
                                break;
                          
                        }
                    }
                }
                var qnxt = ((from nhapd in data.NhapDs.Where(p => (p.PLoai == 1 && p.MaKP == _kho) || (p.PLoai == 5 && p.MaKP == _kho) || (p.PLoai == 3 && p.MaKP == _kho) || ((p.PLoai == 2 && p.KieuDon != 2 && p.KieuDon != 3) && (p.MaKP == _MaKP1 || p.MaKP == _MaKP2 || p.MaKP == _MaKP3 || p.MaKP == _MaKP4 || p.MaKP == _MaKP5 || p.MaKP == _MaKP6 || p.MaKP == _MaKP7 || p.MaKP == _MaKP8 || p.MaKP == _MaKP9 || p.MaKP == _MaKP10)))
                             join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                             join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                             join nhomdv in data.NhomDVs.Where(p=>p.Status==1) on dv.IDNhom equals nhomdv.IDNhom
                             join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                             group new { nhomdv, dv, nhapd, nhapdct, tieunhomdv } by new { dv.MaCC, nhomdv.TenNhomCT, dv.MaDV, dv.TenDV, dv.DonVi, nhapdct.DonGia, tieunhomdv.TenTN } into kq
                             select new
                                 {
                                     //PLoai = kq.Key.PLoai,
                                     MaCC = kq.Key.MaCC,
                                     TenNhomDV = kq.Key.TenNhomCT,
                                     TieuNhomDV = kq.Key.TenTN,
                                     MaDV = kq.Key.MaDV,
                                     TenDV = kq.Key.TenDV,
                                     DVT = kq.Key.DonVi,
                                     DonGia = kq.Key.DonGia,
                                     //  NuocSX = kq.Key.NuocSX,

                                     TonDKSL = kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.SoLuongN)-kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.SoLuongX)-kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.SoLuongSD),
                                     TonDKTT = kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.ThanhTienN) - kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.ThanhTienX) - kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.ThanhTienSD),

                                     NhapTKSL = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongN) ,
                                     NhapTKTT = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienN),

                                     SDTKSL = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongX) + kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongSD),
                                     SDTKTT = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX) +kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienSD),

                                     TonCKSL = kq.Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongN) - kq.Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongX) - kq.Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongSD),
                                     TonCKTT = kq.Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienN) - kq.Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX) - kq.Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienSD),
                                 }).OrderBy(p => p.TenDV).ToList());
                        
                if (qnxt.Count > 0)
                {

                    double TT = 0;
                    string _pl = "";
                    if (lupPL.Text != null)
                    {
                        _pl = lupPL.EditValue.ToString();
                        if (_pl != " ")
                        {
                            if (!string.IsNullOrEmpty(_macc))
                            {
                                TT = qnxt.Where(p => p.TenNhomDV.Contains(_pl)).Where(p => p.MaCC== (_macc)).Sum(p => p.TonCKTT);
                                rep.TongTien.Value = TT;
                                rep.DataSource = qnxt.Where(p => p.TonDKSL > 0 || p.TonDKSL < 0 || p.TonCKSL < 0 || p.TonCKSL>0 || p.NhapTKSL>0).ToList().Where(p => p.MaCC== (_macc)).Where(p => p.TenNhomDV.Contains(_pl));

                            }
                            else
                            {
                                TT = qnxt.Where(p => p.TenNhomDV.Contains(_pl)).Sum(p => p.TonCKTT);
                                rep.TongTien.Value = TT;

                                rep.DataSource = qnxt.Where(p => p.TonDKSL > 0 || p.TonDKSL < 0 || p.TonCKSL < 0 || p.TonCKSL>0 || p.NhapTKSL>0).ToList().Where(p => p.TenNhomDV.Contains(_pl)).OrderBy(p => p.TenDV).ToList();
                            }
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(_macc))
                            {
                                TT = qnxt.Where(p => p.MaCC== (_macc)).Sum(p => p.TonCKTT);
                                rep.TongTien.Value = TT;
                                rep.DataSource = qnxt.Where(p => p.TonDKSL > 0 || p.TonDKSL < 0 || p.TonCKSL < 0 || p.TonCKSL>0 || p.NhapTKSL>0).ToList().Where(p => p.MaCC== (_macc)).OrderBy(p => p.TenDV).ToList();

                            }
                            else
                            {
                                TT = qnxt.Sum(p => p.TonCKTT);
                                rep.TongTien.Value = TT;

                                rep.DataSource = qnxt.Where(p => p.TonDKSL > 0 || p.TonDKSL < 0 || p.TonCKSL < 0 || p.TonCKSL>0 || p.NhapTKSL>0).OrderBy(p=>p.TenDV).ToList();
                            }
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                    }
                
                }
                else
                    MessageBox.Show("Không có dữ liệu để in báo cáo");
            }
        }

        private void grvKhoaphong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chọn")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("tenkp") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("tenkp").ToString();

                    if (Ten == "Chọn tất cả")
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
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _Kphong.ToList();
                    }
                }
            }
        }

        private void lupKho_EditValueChanged(object sender, EventArgs e)
        {
            
        }
    }
}