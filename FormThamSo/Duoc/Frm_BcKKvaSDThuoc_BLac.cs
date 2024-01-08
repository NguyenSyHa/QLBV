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
    public partial class Frm_BcKKvaSDThuoc_BLac : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcKKvaSDThuoc_BLac()
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
            if (lupKho.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn kho tổng hợp");
                lupKho.Focus();
                return false;
            }
            //if (lupPL.EditValue == null)
            //{
            //    MessageBox.Show("Bạn chưa chọn phân loại dược");
            //    lupPL.Focus();
            //    return false;
            //}
            return true;
        }
      
        private void Frm_BcKKvaSDThuoc_BLac_Load(object sender, EventArgs e)
        {
            dateTuNgay.DateTime = System.DateTime.Now;
            dateDenNgay.DateTime = System.DateTime.Now;
            List<NhaCC> _NCC = new List<NhaCC>();
            _NCC = (from cc in data.NhaCCs.Where(p => p.Status == 1) select cc).ToList();
            _NCC.Add(new NhaCC { MaCC = "",TenCC="" }); //
      
            lupNhaCC.Properties.DataSource = _NCC.OrderBy(p=>p.TenCC).ToList();
           
            var q = from TK in data.KPhongs.Where(p => p.PLoai== ("Khoa dược")) select new { TK.TenKP, TK.MaKP };
            lupKho.Properties.DataSource = q.ToList();
     
            List<NhomDV> _Ploai = new List<NhomDV>();
            _Ploai = (from pl in data.NhomDVs.Where(p => p.Status == 1) select pl).ToList();
            _Ploai.Add(new NhomDV { TenNhomCT = "", Status = 1 }); //
            lupPL.Properties.DataSource = _Ploai.OrderBy(p => p.TenNhomCT).ToList();
      }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private class ncc
        {
            private string MaNCC;
         
            public string MaNCC1
            {
                get { return MaNCC; }
                set { MaNCC = value; }
            }

        }
     
        List<ncc> _ncc = new List<ncc>();
   //     List<NhomDV> _nhacc = new List<NhaCC>();
        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            List<NhomDV> _Pl = new List<NhomDV>();
      

            if (KTtaoBcNXT())
            {

                tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);
            
                BaoCao.Rep_BcKKvaSDThuoc_BLac rep = new BaoCao.Rep_BcKKvaSDThuoc_BLac();

                #region Hiển thị thời gian
                int nam = Convert.ToInt32(denngay.Year);
                int thang = Convert.ToInt32(denngay.Month);
                if (radIn.SelectedIndex == 0)
                { rep.NgayThang.Value = "Từ ngày " + tungay.ToString().Substring(0,10) + " đến ngày " + denngay.ToString().Substring(0,10); }
                if (radIn.SelectedIndex == 1)
                { rep.NgayThang.Value = ("Tháng " + thang + " năm " + nam).ToUpper(); }
                if (radIn.SelectedIndex == 2)
                {
                    if (thang > 1 && thang <= 3) { rep.NgayThang.Value = ("Quý I năm " + nam).ToUpper(); }
                    if (thang > 3 && thang <= 6) { rep.NgayThang.Value = ("Quý II năm " + nam).ToUpper(); }
                    if (thang > 6 && thang <= 9) { rep.NgayThang.Value = ("Quý III năm " + nam).ToUpper(); }
                    if (thang > 9 && thang <= 12) { rep.NgayThang.Value = ("Quý IV năm " + nam).ToUpper(); }
                }
                if (radIn.SelectedIndex == 3)
                {
                    rep.NgayThang.Value = ("Báo cáo 6 tháng/ năm " + nam).ToUpper();
                }
                if (radIn.SelectedIndex == 4)
                {
                    rep.NgayThang.Value = ("Báo cáo 9 tháng/ năm " + nam).ToUpper();
                }
                if (radIn.SelectedIndex == 5)
                { rep.NgayThang.Value = ("Năm " + nam).ToUpper(); }
                #endregion
   
                //string _macc = "";
                //if (lupNhaCC.EditValue != null && lupNhaCC.EditValue.ToString() != "")
                //    _macc = lupNhaCC.EditValue.ToString();
                _ncc.Clear(); _Pl.Clear();
                int _kho = 0;
                if (lupKho.EditValue != null)
                    _kho = Convert.ToInt32( lupKho.EditValue);
                string _pl = "";
                //if (lupPL.EditValue != null)
                //    _pl = lupPL.EditValue.ToString();
                if (!string.IsNullOrEmpty(lupPL.Text))
                {
                    if (lupPL.EditValue != null && lupPL.EditValue.ToString() != "")
                    {
                        _pl = lupPL.EditValue.ToString();
                        NhomDV themmoi = new NhomDV();
                        themmoi.TenNhomCT = _pl;
                        _Pl.Add(themmoi);

                    }
                
                }
                else
                {
                    var nhomdv = data.NhomDVs.Select(p => new { p.TenNhomCT }).ToList();
                    if (nhomdv.Count > 0)
                    {
                        foreach (var a in nhomdv)
                        {
                            NhomDV themmoi = new NhomDV();
                            themmoi.TenNhomCT = a.TenNhomCT;
                            _Pl.Add(themmoi);
                        }
                    }
                    _Pl.Add(new NhomDV { TenNhomCT = "" });

                }
                string _nhacc = "";
                if (!string.IsNullOrEmpty(lupNhaCC.Text))
                {
                    if (lupNhaCC.EditValue != null && lupNhaCC.EditValue.ToString() != "")
                    {
                        _nhacc = lupNhaCC.EditValue.ToString();
                        ncc themmoi = new ncc();
                        themmoi.MaNCC1 = _nhacc;
                        _ncc.Add(themmoi);

                    }
                    var NCC = data.NhaCCs.Where(p=>p.MaCC ==_nhacc).Select(p=>new{p.TenCC}).ToList();
                    if(NCC.Count>0){rep.NCC.Value=NCC.First().TenCC;}
                }
                else
                {
                    var ncc =data.NhaCCs.Select(p => new { p.MaCC }).ToList();
                    if (ncc.Count > 0)
                    {
                        foreach (var a in ncc)
                        {
                            ncc themmoi = new ncc();
                            themmoi.MaNCC1 = a.MaCC;
                            _ncc.Add(themmoi);
                        }
                    }
                    _ncc.Add(new ncc { MaNCC1 = "" });

                }
             
                 var q1 = (from nhapd in data.NhapDs
                             join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                             join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                             join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                             join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                             join kp in data.KPhongs on nhapd.MaKP equals kp.MaKP
                             select new {nhapd.MaKP, nhapd.PLoai,nhapd.NgayNhap,nhapd.KieuDon,dv.MaCC, nhomdv.TenNhomCT,nhomdv.Status, dv.MaDV, dv.TenDV, dv.DonVi,dv.TrongDM, nhapdct.DonGia,nhapdct.SoLuongN,nhapdct.ThanhTienN,nhapdct.SoLuongX,nhapdct.ThanhTienX,nhapdct.SoLuongSD,nhapdct.ThanhTienSD, tieunhomdv.TenTN ,kp.TenKP,PL=kp.PLoai}).ToList();
                 var q2 = (from pl in _Pl
                          join q in q1 on pl.TenNhomCT equals q.TenNhomCT
                          select new { q.MaKP, q.PLoai, q.NgayNhap, q.KieuDon, q.MaCC, q.TenNhomCT, q.Status, q.MaDV, q.TenDV, q.DonVi, q.TrongDM, q.DonGia, q.SoLuongN, q.ThanhTienN, q.SoLuongX, q.ThanhTienX, q.SoLuongSD, q.ThanhTienSD, q.TenTN, q.TenKP, q.PL }).ToList();
                 var qnxt = ((from nc in _ncc
                              join n in q2.Where(p => (p.PLoai == 1|| p.PLoai == 5)).Where(p=>p.TrongDM==1) on nc.MaNCC1 equals n.MaCC
                             group n by new { n.MaCC, n.TenNhomCT, n.MaDV, n.TenDV, n.DonVi, n.DonGia, n.TenTN } into kq
                             select new
                                 {
                                     MaCC = kq.Key.MaCC,
                                     TenNhomDV = kq.Key.TenNhomCT,
                                     TieuNhomDV = kq.Key.TenTN,
                                     MaDV = kq.Key.MaDV,
                                     TenDV = kq.Key.TenDV,
                                     DVT = kq.Key.DonVi,
                                     DonGia = kq.Key.DonGia,

                                     TonDKSL =  kq.Where(p => p.NgayNhap < tungay).Where(p => p.MaKP == _kho).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap < tungay).Sum(p => p.SoLuongSD),
                                     TonDKTT =  kq.Where(p => p.NgayNhap < tungay).Where(p => p.MaKP == _kho).Sum(p => p.ThanhTienN) - kq.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienSD) ,

                                     NhapTKSL = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay&&p.MaKP==_kho).Sum(p => p.SoLuongN) ,
                                     NhapTKTT = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.MaKP == _kho).Sum(p => p.ThanhTienN),

                                     SL1 = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PL=="Khoa dược"&&p.TenKP.Contains("ã")).Sum(p => p.SoLuongSD) ,
                                     TT1 = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PL == "Khoa dược" && p.TenKP.Contains("ã")).Sum(p => p.ThanhTienSD),

                                     SLSDtongTK = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Sum(p => p.SoLuongSD),
                                     TTSDtongTK = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Sum(p => p.ThanhTienSD),

                                     SLNhapXaCK =kq.Where(p => p.NgayNhap <= denngay  && p.PL=="Khoa dược"&&p.TenKP.Contains("ã")).Sum(p => p.SoLuongN) ,//- kq.Where(p => p.NgayNhap <= denngay  && p.PL=="Khoa dược"&&p.TenKP.Contains("ã")).Sum(p => p.SoLuongSD),
                                     TTNhapXaCK = kq.Where(p => p.NgayNhap <= denngay && p.PL == "Khoa dược" && p.TenKP.Contains("ã")).Sum(p => p.ThanhTienN),// - kq.Where(p => p.NgayNhap <= denngay && p.PL == "Khoa dược" && p.TenKP.Contains("ã")).Sum(p => p.ThanhTienSD),
                                     
                                     SLNhaptongCK =kq.Where(p => p.NgayNhap <= denngay && p.MaKP == _kho).Sum(p => p.SoLuongN),
                                     TTNhaptongCK = kq.Where(p => p.NgayNhap <= denngay && p.MaKP == _kho).Sum(p => p.ThanhTienN),

                                     SLSDXaCK = kq.Where(p => p.NgayNhap <= denngay && p.PL == "Khoa dược" && p.TenKP.Contains("ã")).Sum(p => p.SoLuongSD),
                                     TTSDXaCK = kq.Where(p => p.NgayNhap <= denngay && p.PL == "Khoa dược" && p.TenKP.Contains("ã")).Sum(p => p.ThanhTienSD),
                                    
                                     SLSDtongCK = kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongSD),
                                     TTSDtongCK = kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienSD),
                                 
                                }).OrderBy(p => p.TenDV).ToList());

                var  nxt = qnxt.Select(p => new {p.TenNhomDV,p.TieuNhomDV,p.MaDV,p.TenDV,p.DVT,p.DonGia,
                                    TonDKSL=p.TonDKSL,
                                    TonDKTT=p.TonDKTT,
                                    NhapTKSL=p.NhapTKSL,
                                    NhapTKTT= p.NhapTKTT,

                                    SL1=p.SL1,
                                    SL2 =  p.SLSDtongTK - p.SL1,
                                    SL3 = p.SLNhapXaCK - p.SLSDXaCK,
                                    SL4 =  p.SLNhaptongCK - p.SLNhapXaCK - p.SLSDtongCK+p.SLSDXaCK,

                                    TT1 = p.TT1,
                                    TT2 =  p.TTSDtongTK - p.TT1,
                                    TT3 =  p.TTNhapXaCK - p.TTSDXaCK,
                                    TT4 = p.TTNhaptongCK - p.TTNhapXaCK - p.TTSDtongCK + p.TTSDXaCK,
                }).ToList();
                #region xuat Excel

                string[] _arr = new string[]  { "0", "@","@", "0", "0", "0", "0", "0", "0", "0", "0", "0","0","0","0","0" } ;
                int[] _arrWidth = new int[] { };
                DungChung.Bien.MangHaiChieu = new Object[nxt.Count+1, 16];
                string[] _tieude = { "STT", "Tên thuốc - Hàm lượng", "Đơn vị", "Đơn giá", "Tồn đầu kỳ - SL", "Tồn đầu kỳ - TT", "Nhập trong kỳ - SL", "Nhập trong kỳ - TT", "Xuất trong kỳ - SL Xã", "Xuất trong kỳ - TT Xã", "Xuất trong kỳ - SL BV", "Xuất trong kỳ - TT BV", "Tồn CK kỳ - SL Xã", "Tồn CK kỳ - TT Xã", "Tồn CK kỳ - SL BV", "Tồn CK kỳ - TT BV" };

                for (int i = 0; i < _tieude.Length; i++)
                {
                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                }
                int num = 1;
                foreach (var r in nxt)
                {
                    DungChung.Bien.MangHaiChieu[num, 0] = num;
                    DungChung.Bien.MangHaiChieu[num, 1] = r.TenDV;
                    DungChung.Bien.MangHaiChieu[num, 2] = r.DVT;
                    DungChung.Bien.MangHaiChieu[num, 3] = r.DonGia;
                    DungChung.Bien.MangHaiChieu[num, 4] = r.TonDKSL;
                    DungChung.Bien.MangHaiChieu[num, 5] = r.TonDKTT;
                    DungChung.Bien.MangHaiChieu[num, 6] = r.NhapTKSL;
                    DungChung.Bien.MangHaiChieu[num, 7] = r.NhapTKTT;
                    DungChung.Bien.MangHaiChieu[num, 8] = r.SL1;
                    DungChung.Bien.MangHaiChieu[num, 9] = r.TT1;
                    DungChung.Bien.MangHaiChieu[num, 10] = r.SL2;
                    DungChung.Bien.MangHaiChieu[num, 11] = r.TT2;
                    DungChung.Bien.MangHaiChieu[num, 12] = r.SL3;
                    DungChung.Bien.MangHaiChieu[num, 13] = r.TT3;
                    DungChung.Bien.MangHaiChieu[num, 14] = r.SL4;
                    DungChung.Bien.MangHaiChieu[num, 15] = r.TT4;
               
                    num++;

                }
                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "BC KK-SD thuốc BHYT", "C:\\BcKKvaSDThuoc.xls", true, this.Name);
            
                #endregion
          
                rep.DataSource = nxt.ToList();
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
                        
        
            }
        }

        private void grvKhoaphong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }

       
        private void lupKho_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }
    }
}