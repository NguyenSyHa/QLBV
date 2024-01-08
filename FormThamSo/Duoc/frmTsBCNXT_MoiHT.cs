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
    public partial class frmTsBCNXT_MoiHT : DevExpress.XtraEditors.XtraForm
    {
        public frmTsBCNXT_MoiHT()
        {
            InitializeComponent();
        }
        private bool KTtaoBcNXT()
        {
            if (dateTuNgay.EditValue ==null) 
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                dateTuNgay.Focus();
                return false;
            }
            if (dateDenNgay.EditValue ==null) 
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                dateDenNgay.Focus();
                return false;
            }
            //if (lupKho.EditValue == null)
            //{
            //    MessageBox.Show("Bạn chưa chọn khoa phòng để in báo cáo");
            //    lupKho.Focus();
            //    return false;
            //}
            else return true;
            
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            string DT = "";

            if (!string.IsNullOrEmpty(cboDTuong.Text))
            {
                DT = cboDTuong.Text;
                var q1 = (from nd in data.NhapDs.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                          join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                          select ndct).ToList();
                if (q1.Count > 0)
                {
                    foreach (var b in q1)
                    {
                       // string mbn=b.MaBNhan.ToString();
                        var tk = data.BenhNhans.Where(p => p.MaBNhan == 0).ToList();
                        if (tk.Count > 0 && tk.First().DTuong != null && tk.First().DTuong.ToString() != "")
                        {
                            if (tk.First().DTuong == "BHYT")
                            {
                               // b.DieuK = "BHYT";
                                data.SaveChanges();
                            }
                            else
                            {
                              //  b.DieuK = "Dịch vụ";
                                data.SaveChanges();
                            }
                        }
                    }
                }
            }
            if (KTtaoBcNXT())
            {
                int _kho = 0;
                if (lupKho.EditValue!=null)
                     _kho= Convert.ToInt32( lupKho.EditValue);
                string _nhacc = "";
                if (lupNhaCC.EditValue != null)
                     _nhacc = lupNhaCC.EditValue.ToString();
                tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
                denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;

                frmIn frm = new frmIn();
                BaoCao.RepBcNXT_MoiHT rep = new BaoCao.RepBcNXT_MoiHT();
                
                
                rep.TuNgay.Value = dateTuNgay.Text;
                rep.DenNgay.Value = dateDenNgay.Text;
                rep.Kho.Value = _kho;
                var qtenkho = (from kp in data.KPhongs
                               join nhapd in data.NhapDs on kp.MaKP equals nhapd.MaKP
                               where (nhapd.MaKP == _kho)
                               select new { kp.TenKP }).ToList();
                if (qtenkho.Count > 0)
                {
                    rep.Kho.Value = qtenkho.First().TenKP;
                }
                var qtenncc = (from nhapd in data.NhapDs 
                               join nhacc in data.NhaCCs on nhapd.MaCC equals nhacc.MaCC 
                               where (nhacc.MaCC==_nhacc)
                               select new { nhacc.TenCC }).ToList();
                if (qtenncc.Count > 0)
                {
                   rep.NhaCC.Value = qtenncc.First().TenCC;
                }
 
                //var qnxt = (from dv in data.DichVus 
                //            join nhapdct in data.NhapDcts on dv.MaDV equals nhapdct.MaDV 
                //            join nhapd in data.NhapDs on nhapdct.IDNhap equals nhapd.IDNhap
                //            join kp in data.KPhongs on nhapd.MaKP equals kp.MaKP
                //            join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom 
                //            join tieunhomdv in data.TieuNhomDVs on nhomdv.IDNhom equals tieunhomdv.IDNhom 
                 //where (from tt in _dataContext.VienPhis select tt.MaBNhan).Contains(kd.MaBNhan)
                if (_kho>0)
                {
                    if (!string.IsNullOrEmpty(_nhacc))
                    {
                        var qnxt = (from nhapd in data.NhapDs.Where(p => p.MaKP == _kho)
                                    join nhapdct in data.NhapDcts.Where(p=>p.MaCC==_nhacc) on nhapd.IDNhap equals nhapdct.IDNhap
                                    //where(nhapd.KieuDon==1 || nhapd.KieuDon==0)
                                    join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                                    join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                                    //join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                                    where (nhapd.PLoai == 1 || nhapd.PLoai == 2 || nhapd.PLoai == 3)
                                    group new { dv, nhapd, nhapdct } by new { nhapdct.MaDV, nhomdv.TenNhom, dv.TenDV, nhapdct.DonVi, nhapdct.DonGia } into kq
                                    select new
                                        {
                                            MaDV = kq.Key.MaDV,
                                            TenNhomDuoc = kq.Key.TenNhom,
                                            TenHamLuong = kq.Key.TenDV,
                                            DonVi = kq.Key.DonVi,
                                            DonGia = kq.Key.DonGia,
                                            //SoLo=kq.Key.SoLo,

                                            TonDKSL = kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.SoLuongN) - kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.SoLuongX),
                                            TonDKTT = kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.ThanhTienN) - kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.ThanhTienX),
                                            //TonDKTTTong = kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.ThanhTienN)-kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.ThanhTienX),

                                            NhapTKSL = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongN),
                                            NhapTKTT = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienN),
                                            //  NhapTKTTTong = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienN),

                                            XuatNoiTruSL = kq.Where(p => p.nhapd.KieuDon == 1).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongX),
                                            xuatNoiTruTT = kq.Where(p => p.nhapd.KieuDon == 1).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX),
                                            // xuatNoiTruTTTong = kq.Where(p => p.nhapd.KieuDon == 0).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX),

                                            XuatNgoaiTruSL = kq.Where(p => p.nhapd.KieuDon == 0).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongX),
                                            xuatNgoaiTruTT = kq.Where(p => p.nhapd.KieuDon == 0).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX),
                                            // xuatNgoaiTruTTTong = kq.Where(p => p.nhapd.KieuDon == 1).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX),

                                            XuatTKTongSL = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongX),
                                            xuatTKTongTT = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX),
                                            //  xuatTKTongTTTong = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX),

                                            TonCKSL = kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.SoLuongN) - kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.SoLuongX),
                                            TonCKTT = kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.ThanhTienN) - kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.ThanhTienX),
                                            // TonCKTTTong = kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.ThanhTienN) - kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.ThanhTienX)

                                        }).ToList();


                        rep.DataSource = qnxt.ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else {
                        var qnxt = (from nhapd in data.NhapDs.Where(p => p.MaKP == _kho)
                                    join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                    //where(nhapd.KieuDon==1 || nhapd.KieuDon==0)
                                    join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                                    join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                                    //join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                                    where (nhapd.PLoai == 1 || nhapd.PLoai == 2 || nhapd.PLoai == 3)
                                    group new { dv, nhapd, nhapdct } by new { nhapdct.MaDV, nhomdv.TenNhom, dv.TenDV, nhapdct.DonVi, nhapdct.DonGia } into kq
                                    select new
                                    {
                                        MaDV = kq.Key.MaDV,
                                        TenNhomDuoc = kq.Key.TenNhom,
                                        TenHamLuong = kq.Key.TenDV,
                                        DonVi = kq.Key.DonVi,
                                        DonGia = kq.Key.DonGia,
                                        //SoLo=kq.Key.SoLo,

                                        TonDKSL = kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.SoLuongN) - kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.SoLuongX),
                                        TonDKTT = kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.ThanhTienN) - kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.ThanhTienX),

                                        NhapTKSL = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongN),
                                        NhapTKTT = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienN),

                                       // XuatBHYT = kq.Where(p => p.nhapd.KieuDon == 1).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Where(p=>p.nhapdct.DieuK=="BHYT").Sum(p => p.nhapdct.SoLuongX),
                                      //  xuatBHYTTT = kq.Where(p => p.nhapd.KieuDon == 1).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Where(p => p.nhapdct.DieuK == "BHYT").Sum(p => p.nhapdct.ThanhTienX),

                                      //  XuatDichVuSL = kq.Where(p => p.nhapd.KieuDon == 0).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Where(p => p.nhapdct.DieuK == "Dịch vụ").Sum(p => p.nhapdct.SoLuongX),
                                        //xuatDichVuTT = kq.Where(p => p.nhapd.KieuDon == 0).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Where(p => p.nhapdct.DieuK == "Dịch vụ").Sum(p => p.nhapdct.ThanhTienX),
                                        XuatTKTongSL = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongX),
                                        xuatTKTongTT = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX),

                                        TonCKSL = kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.SoLuongN) - kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.SoLuongX),
                                        TonCKTT = kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.ThanhTienN) - kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.ThanhTienX),

                                    }).ToList();


                        rep.DataSource = qnxt.ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                }
                //else
                //{
                //    var qnxt = (from nhapd in data.NhapDs
                //                join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                //                //where(nhapd.KieuDon==1 || nhapd.KieuDon==0)
                //                join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                //                join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                //                //join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                //                where (nhapd.PLoai == 1 || nhapd.PLoai == 2 || nhapd.PLoai == 3)
                //                group new { dv, nhapd, nhapdct } by new {  nhapdct.MaDV, nhomdv.TenNhom, dv.TenDV, nhapdct.DonVi, nhapdct.DonGia } into kq
                //                select new
                //                {
                //                    MaDV = kq.Key.MaDV,
                //                    TenNhomDuoc = kq.Key.TenNhom,
                //                    TenHamLuong = kq.Key.TenDV,
                //                    DonVi = kq.Key.DonVi,
                //                    DonGia = kq.Key.DonGia,
                //                    //SoLo=kq.Key.SoLo,

                //                    TonDKSL = kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.SoLuongN) - kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.SoLuongX),
                //                    TonDKTT = kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.ThanhTienN) - kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.ThanhTienX),
                //                    //TonDKTTTong = kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.ThanhTienN)-kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.ThanhTienX),

                //                    NhapTKSL = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongN),
                //                    NhapTKTT = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienN),
                //                    //  NhapTKTTTong = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienN),

                //                    XuatNoiTruSL = kq.Where(p => p.nhapd.KieuDon == 1).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongX),
                //                    xuatNoiTruTT = kq.Where(p => p.nhapd.KieuDon == 1).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX),
                //                    // xuatNoiTruTTTong = kq.Where(p => p.nhapd.KieuDon == 0).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX),

                //                    XuatNgoaiTruSL = kq.Where(p => p.nhapd.KieuDon == 0).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongX),
                //                    xuatNgoaiTruTT = kq.Where(p => p.nhapd.KieuDon == 0).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX),
                //                    // xuatNgoaiTruTTTong = kq.Where(p => p.nhapd.KieuDon == 1).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX),

                //                    XuatTKTongSL = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongX),
                //                    xuatTKTongTT = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX),
                //                    //  xuatTKTongTTTong = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX),

                //                    TonCKSL = kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.SoLuongN) - kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.SoLuongX),
                //                    TonCKTT = kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.ThanhTienN) - kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.ThanhTienX),
                //                    // TonCKTTTong = kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.ThanhTienN) - kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.ThanhTienX)

                //                }).ToList();
                //    rep.DataSource = qnxt.ToList();
                //    //rep.DataSource = qnxt.ToList();
                //    rep.BindingData();
                //    rep.CreateDocument();
                //    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                //    frm.ShowDialog();
                //}
            }
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTsBCNXT_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var q = from TK in data.KPhongs.Where(p => p.PLoai== ("Khoa dược")) select new { TK.TenKP, TK.MaKP };
            lupKho.Properties.DataSource = q.ToList();

            var qcc = from CC in data.NhaCCs select new { CC.MaCC, CC.TenCC };
            lupNhaCC.Properties.DataSource = qcc.ToList();
        }

        private void lupKho_EditValueChanged(object sender, EventArgs e)
        {

        }

               
    }
}