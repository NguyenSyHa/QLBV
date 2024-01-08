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
    public partial class Frm_BcChiPhiKCB : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcChiPhiKCB()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private bool KTtaoBc()
        {
            if (lupTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupTuNgay.Focus();
                return false;
            }
            if (lupDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
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
        private class NhomDV
        {
            private int IDNhom;
            private string TenNhomCT;
            public int idnhom
            {
                set { IDNhom = value; }
                get { return IDNhom; }
            }
            public string tennhom
            {
                set { TenNhomCT = value; }
                get { return TenNhomCT; }
            }
        }
        List<KPhong> _Kphong = new List<KPhong>();
        List<NhomDV> _lndv = new List<NhomDV>();
        int _nt = -1;
        private void Frm_BcChiPhiKCB_Load(object sender, EventArgs e)
        {
            _Kphong.Clear();
            _lndv.Clear();
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
            if (radBN.SelectedIndex == 0)
            {
                var kphong = (from kp in data.KPhongs
                              where (kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám")
                              select new { kp.TenKP, kp.MaKP }).ToList();

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
                }
            }

            grcKhoaphong.DataSource = _Kphong.ToList();


            var qndv = (from ndv in data.NhomDVs
                        select new {ndv.TenNhomCT }).ToList();
            if (qndv.Count() > 0)
            {
                NhomDV them1 = new NhomDV();
              //  them1.idnhom = 0;
                them1.tennhom = "Tất cả";
                _lndv.Add(them1);
                foreach (var a in qndv)
                {
                    NhomDV themmoi = new NhomDV();
                 //   themmoi.idnhom = a.IDNhom;
                    themmoi.tennhom = a.TenNhomCT;
                    _lndv.Add(themmoi);
                }
                lupNhomDV.Properties.DataSource = _lndv.ToList();
            }

        }
        private class BN
        {
            private string TenNhomCT;

            public string TenNhomCT1
            {
                get { return TenNhomCT; }
                set { TenNhomCT = value; }
            }
            private int MaBN;

            public int MaBN1
            {
                get { return MaBN; }
                set { MaBN = value; }
            }
            private int NNTru;

            public int NNTru1
            {
                get { return NNTru; }
                set { NNTru = value; }
            }
            private int MaDV;

            public int MaDV1
            {
                get { return MaDV; }
                set { MaDV = value; }
            }
            private string TenDV;

            public string TenDV1
            {
                get { return TenDV; }
                set { TenDV = value; }
            } private string STTNhom;

            public string STTNhom1
            {
                get { return STTNhom; }
                set { STTNhom = value; }
            } private string TenBN;

            public string TenBN1
            {
                get { return TenBN; }
                set { TenBN = value; }
            } private string SThe;

            public string SThe1
            {
                get { return SThe; }
                set { SThe = value; }
            } private string MaICD;

            public string MaICD1
            {
                get { return MaICD; }
                set { MaICD = value; }
            } private string NgayVao;

            public string NgayVao1
            {
                get { return NgayVao; }
                set { NgayVao = value; }
            } private string NgayRa;

            public string NgayRa1
            {
                get { return NgayRa; }
                set { NgayRa = value; }
            }
            private string KPhong;

            public string KPhong1
            {
                get { return KPhong; }
                set { KPhong = value; }
            } private string BacSy;

            public string BacSy1
            {
                get { return BacSy; }
                set { BacSy = value; }
            } private double DonGia;

            public double DonGia1
            {
                get { return DonGia; }
                set { DonGia = value; }
            } private double Sl;

            public double Sl1
            {
                get { return Sl; }
                set { Sl = value; }
            } private double ThanhTien;

            public double ThanhTien1
            {
                get { return ThanhTien; }
                set { ThanhTien = value; }
            } private string DiaChi;

            public string DiaChi1
            {
                get { return DiaChi; }
                set { DiaChi = value; }
            }
            public string ChanDoan;
            public string ChanDoan1
            {
                get { return ChanDoan; }
                set { ChanDoan = value; }
            }
            private int TrongBH;

            public int TrongBH1
            {
                get { return TrongBH; }
                set { TrongBH = value; }
            }

        }

        List<BN> _BN = new List<BN>();
        private void btnInBC_Click(object sender, EventArgs e)
        {
            _BN.Clear();
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            string _madv = "";// nhom dich vu
            if (lupNhomDV.EditValue != null && lupNhomDV.EditValue.ToString() != "")
                _madv = lupNhomDV.EditValue.ToString();
            frmIn frm = new frmIn();
            if (KTtaoBc())
            {

                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                List<KPhong> _lKP = new List<KPhong>();
                _lKP = _Kphong.Where(p => p.makp >0).Where(p => p.chon == true).ToList();
         
               
                BaoCao.Rep_BcChiPhiKCB rep = new BaoCao.Rep_BcChiPhiKCB();
                rep.TuNgayDenNgay.Value = "Từ ngày " + tungay.ToString().Substring(0, 10) + " đến ngày " + denngay.ToString().Substring(0, 10);
                var _ldv = (from dv in data.DichVus
                            join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                            join n in data.NhomDVs on tn.IDNhom equals n.IDNhom
                            select new
                            {
                                dv.MaDV,
                                dv.TenDV,
                                n.STT,
                                n.TenNhomCT,
                                dv.TrongDM
                            }).ToList();
                var _lbn = (from bn in data.BenhNhans.Where(p => p.DTuong == "BHYT")
                            join vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay) on bn.MaBNhan equals vp.MaBNhan
                            join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                            join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                            select new
                            {
                                rv.ChanDoan,
                                bn.MaBNhan,
                                bn.NoiTru,
                                bn.TenBNhan,
                                bn.SThe,
                                bn.DChi,
                                bn.MaKP,
                                rv.MaICD,
                                rv.NgayVao,
                                rv.NgayRa,
                                rv.MaCB,
                                vpct.SoLuong,
                                vpct.DonGia,
                                vpct.ThanhTien,
                                vpct.MaDV
                            }).ToList();

                var qbn = (from kp in _lKP
                           join bn in _lbn on kp.makp equals bn.MaKP
                           join dv in _ldv on bn.MaDV equals dv.MaDV
                           //    in data.BenhNhans.Where(p => p.DTuong == "BHYT") on kp.makp equals bn.MaKP
                           //join vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay) on bn.MaBNhan equals vp.MaBNhan
                           //join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                           //join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                           //join dv in data.DichVus on vpct.MaDV equals dv.MaDV
                           //join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                           select new { bn.ChanDoan, bn.MaBNhan, bn.NoiTru, dv.MaDV, dv.TenDV, dv.TrongDM, dv.STT, dv.TenNhomCT, bn.TenBNhan, bn.SThe, bn.DChi, MaKP = kp.tenkp, bn.MaICD, bn.NgayVao, bn.NgayRa, bn.MaCB, bn.SoLuong, bn.DonGia, bn.ThanhTien }).ToList();

                if (qbn.Count > 0)
                {
                    foreach (var a in qbn)
                    {
                        BN themmoi1 = new BN();
                        themmoi1.TenNhomCT1 = a.TenNhomCT;
                        themmoi1.MaBN1 = a.MaBNhan;
                        themmoi1.NNTru1 =Convert.ToInt32(a.NoiTru);
                        themmoi1.TenDV1 = a.TenDV;
                        themmoi1.TrongBH1 = Convert.ToInt32(a.TrongDM);
                        themmoi1.STTNhom1 = a.STT.ToString();
                        themmoi1.TenBN1 = a.TenBNhan;
                        themmoi1.SThe1 = a.SThe;
                        themmoi1.MaICD1 = DungChung.Ham.FreshString(a.MaICD);
                        if(a.NgayVao!=null)
                        themmoi1.NgayVao1 = a.NgayVao.ToString().Substring(0, 10);
                        
                        themmoi1.NgayRa1 = a.NgayRa.ToString().Substring(0, 10);
                        themmoi1.KPhong1 = a.MaKP;
                        themmoi1.BacSy1 = a.MaCB;
                        themmoi1.DonGia1 = Convert.ToDouble(a.DonGia);
                        themmoi1.Sl1 = Convert.ToDouble(a.SoLuong);
                        themmoi1.ThanhTien1 = Convert.ToDouble(a.ThanhTien);
                        themmoi1.DiaChi1 = a.DChi;
                        themmoi1.ChanDoan1 = a.ChanDoan;
                        _BN.Add(themmoi1);
                    }

                }
                if (ChkInDVBH.Checked == false)
                {
                    if (radBN.SelectedIndex == 0) //tất cả
                    {
                        rep.TenBC.Value = ("Báo cáo chi phí khám chữa bệnh BHYT").ToUpper();
                        if (lupNhomDV.EditValue == " " || lupNhomDV.EditValue == null || lupNhomDV.EditValue == "Tất cả")
                        {
                            rep.DataSource = _BN.OrderBy(p => p.STTNhom1).OrderBy(p => p.TenBN1).ToList();

                        }
                        else
                        {
                            string _tennhom = lupNhomDV.EditValue.ToString();
                            rep.DataSource = _BN.Where(p => p.TenNhomCT1 == _tennhom).OrderBy(p => p.STTNhom1).OrderBy(p => p.TenBN1).ToList();
                        }
                    }

                    if (radBN.SelectedIndex == 1) // nội trú
                    {
                        rep.TenBC.Value = ("Báo cáo chi phí khám chữa bệnh BHYT - bệnh nhân Nội trú").ToUpper();

                        if (lupNhomDV.EditValue == " " || lupNhomDV.EditValue == null || lupNhomDV.EditValue == "Tất cả")
                        {

                            rep.DataSource = _BN.Where(p => p.NNTru1 == 1).OrderBy(p => p.STTNhom1).OrderBy(p => p.TenBN1).ToList();

                        }
                        else
                        {
                            string _tennhom = lupNhomDV.EditValue.ToString();
                            rep.DataSource = _BN.Where(p => p.TenNhomCT1 == _tennhom).OrderBy(p => p.STTNhom1).OrderBy(p => p.TenBN1).ToList();
                        }
                    }
                    if (radBN.SelectedIndex == 2)// ngoại trú
                    {
                        rep.TenBC.Value = ("Báo cáo chi phí khám chữa bệnh BHYT - bệnh nhân ngoại trú").ToUpper();

                        if (lupNhomDV.EditValue == " " || lupNhomDV.EditValue == null || lupNhomDV.EditValue == "Tất cả")
                        {

                            rep.DataSource = _BN.Where(p => p.NNTru1 == 0).OrderBy(p => p.STTNhom1).OrderBy(p => p.TenBN1).ToList();

                        }
                        else
                        {
                            string _tennhom = lupNhomDV.EditValue.ToString();
                            rep.DataSource = _BN.Where(p => p.TenNhomCT1 == _tennhom).OrderBy(p => p.STTNhom1).OrderBy(p => p.TenBN1).ToList();
                        }


                    }
                }
                else
                {
                    if (radBN.SelectedIndex == 0)
                    {
                        rep.TenBC.Value = ("Báo cáo chi phí khám chữa bệnh BHYT").ToUpper();
                        if (lupNhomDV.EditValue == " " || lupNhomDV.EditValue == null || lupNhomDV.EditValue == "Tất cả")
                        {
                            rep.DataSource = _BN.Where(p=>p.TrongBH1==1).OrderBy(p => p.STTNhom1).OrderBy(p => p.TenBN1).ToList();

                        }
                        else
                        {
                            string _tennhom = lupNhomDV.EditValue.ToString();
                            rep.DataSource = _BN.Where(p => p.TrongBH1 == 1).Where(p => p.TenNhomCT1 == _tennhom).OrderBy(p => p.STTNhom1).OrderBy(p => p.TenBN1).ToList();
                        }
                    }

                    if (radBN.SelectedIndex == 1)
                    {
                        rep.TenBC.Value = ("Báo cáo chi phí khám chữa bệnh BHYT - bệnh nhân Nội trú").ToUpper();

                        if (lupNhomDV.EditValue == " " || lupNhomDV.EditValue == null || lupNhomDV.EditValue == "Tất cả")
                        {

                            rep.DataSource = _BN.Where(p => p.TrongBH1 == 1).Where(p => p.NNTru1 == 1).OrderBy(p => p.STTNhom1).OrderBy(p => p.TenBN1).ToList();

                        }
                        else
                        {
                            string _tennhom = lupNhomDV.EditValue.ToString();
                            rep.DataSource = _BN.Where(p => p.TenNhomCT1 == _tennhom).OrderBy(p => p.STTNhom1).OrderBy(p => p.TenBN1).ToList();
                        }
                    }
                    if (radBN.SelectedIndex == 2)
                    {
                        rep.TenBC.Value = ("Báo cáo chi phí khám chữa bệnh BHYT - bệnh nhân ngoại trú").ToUpper();

                        if (lupNhomDV.EditValue == " " || lupNhomDV.EditValue == null || lupNhomDV.EditValue == "Tất cả")
                        {

                            rep.DataSource = _BN.Where(p => p.TrongBH1 == 1).Where(p => p.NNTru1 == 0).OrderBy(p => p.STTNhom1).OrderBy(p => p.TenBN1).ToList();

                        }
                        else
                        {
                            string _tennhom = lupNhomDV.EditValue.ToString();
                            rep.DataSource = _BN.Where(p => p.TrongBH1 == 1).Where(p => p.TenNhomCT1 == _tennhom).OrderBy(p => p.STTNhom1).OrderBy(p => p.TenBN1).ToList();
                        }


                    }
                }
               
                #region xuat Excel
                if (Xuatex.Checked)
                {


                    COMExcel.Application exApp = new COMExcel.Application();
                    COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                              COMExcel.XlWBATemplate.xlWBATWorksheet);
                    COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                    //exSheet.Activate();
                    exSheet.Name = "Chi tiet(1)";// gán tên sheet
                    int i = 1;
                    string[] _arr = new string[16] { "STT", "MA_BN", "TEN_DVKT", "STT_NHOM", "HOTEN", "SOTHEBHYT", "MACHANDOAN", "NGAYVAO", "NGAYRA", "KHOAPHONGDT", "BACSY", "DONGIA", "SOLUONG", "THANHTIEN", "DIACHI","TENCHANDOAN" };
                    int k = 0;
                    foreach (var b in _arr)
                    {
                        k++;
                        COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                        r.Value2 = b.ToString();
                        r.Columns.AutoFit();
                    }
                    foreach (var a in _BN)
                    {
                        i++;
                        COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                        r1.Value2 = i - 1;
                        r1.Columns.AutoFit();
                        COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                        r2.NumberFormat = "@";
                        if (a.MaBN1 != null)
                            r2.Value2 = a.MaBN1;
                        r2.Columns.AutoFit();
                        COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                        r3.NumberFormat = "@";
                        r3.Value2 = a.TenDV1;
                        r3.Columns.AutoFit();
                        COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                        r4.NumberFormat = "@";
                        r4.Value2 = a.STTNhom1;
                        r4.Columns.AutoFit();
                        COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                        r5.NumberFormat = "@";
                        r5.Value2 = a.TenBN1;
                        r5.Columns.AutoFit();
                        COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                        r6.NumberFormat = "@";
                        r6.Value2 = a.SThe1;
                        r6.Columns.AutoFit();
                        COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                        r7.NumberFormat = "@";
                        r7.Value2 = a.MaICD1;
                        r7.Columns.AutoFit();
                        COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                        r8.NumberFormat = "@";
                        r8.Value2 = a.NgayVao1;
                        r8.Columns.AutoFit();
                        COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                        r9.NumberFormat = "@";
                        r9.Value2 = a.NgayRa1;
                        r9.Columns.AutoFit();
                        COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                        r10.NumberFormat = "@";
                        r10.Value2 = a.KPhong1;
                        r10.Columns.AutoFit();
                        COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                        r11.NumberFormat = "@";
                        r11.Value2 = a.BacSy1;
                        r11.Columns.AutoFit();
                        COMExcel.Range r12 = (COMExcel.Range)exSheet.Cells[i, 12];
                        r12.NumberFormat = "0";
                        r12.Value2 = a.DonGia1;
                        r12.Columns.AutoFit();
                        COMExcel.Range r13 = (COMExcel.Range)exSheet.Cells[i, 13];
                        r13.NumberFormat = "0";
                        r13.Value2 = a.Sl1;
                        r13.Columns.AutoFit();
                        COMExcel.Range r14 = (COMExcel.Range)exSheet.Cells[i, 14];
                        r14.NumberFormat = "0";
                        r14.Value2 = a.ThanhTien1;
                        r14.Columns.AutoFit();
                        COMExcel.Range r15 = (COMExcel.Range)exSheet.Cells[i, 15];
                        r15.NumberFormat = "@";
                        r15.Value2 = a.DiaChi1;
                        r15.Columns.AutoFit();
                        COMExcel.Range r16 = (COMExcel.Range)exSheet.Cells[i, 16];
                        r16.NumberFormat = "@";
                        r16.Value2 = a.ChanDoan1;
                        r16.Columns.AutoFit();
                       

                    }
                    exApp.Visible = true;//Ẩn hiện chương trình
                    exQLBV.SaveAs("C:\\BcChiPhiKCB.xls", COMExcel.XlFileFormat.xlWorkbookNormal,
                                    null, null, false, false,
                                    COMExcel.XlSaveAsAccessMode.xlExclusive,
                                    false, false, false, false, false);
                    //exQLBV.Close(false, false, false);
                    //exApp.Quit(); // thoát ứng dụng
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                }
                #endregion
                rep.BindingData();
                 rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radBN_SelectedIndexChanged(object sender, EventArgs e)
        {
            //_Kphong.Clear();

            //if (radBN.SelectedIndex == 0)
            //{
            //    var kphong = (from kp in data.KPhongs
            //                  where (kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám")
            //                  select new { kp.TenKP, kp.MaKP }).ToList();

            //    if (kphong.Count > 0)
            //    {
            //        KPhong themmoi1 = new KPhong();
            //        themmoi1.tenkp = "Chọn tất cả";
            //        themmoi1.makp = "";
            //        themmoi1.chon = true;
            //        _Kphong.Add(themmoi1);
            //        foreach (var a in kphong)
            //        {
            //            KPhong themmoi = new KPhong();
            //            themmoi.tenkp = a.TenKP;
            //            themmoi.makp = a.MaKP;
            //            themmoi.chon = true;
            //            _Kphong.Add(themmoi);
            //        }
            //    }
            //}
            //else
            //{
            //    string _plkp = "";
            //    if (radBN.SelectedIndex == 1) { _plkp = "Lâm sàng"; _nt = 1; }
            //    if (radBN.SelectedIndex == 2) { _plkp = "Phòng khám"; _nt = 0; }
            //    var kphong = (from kp in data.KPhongs.Where(p => p.PLoai == _plkp)
            //                  select new { kp.TenKP, kp.MaKP }).ToList();

            //    if (kphong.Count > 0)
            //    {
            //        KPhong themmoi1 = new KPhong();
            //        themmoi1.tenkp = "Chọn tất cả";
            //        themmoi1.makp = "";
            //        themmoi1.chon = true;
            //        _Kphong.Add(themmoi1);
            //        foreach (var a in kphong)
            //        {
            //            KPhong themmoi = new KPhong();
            //            themmoi.tenkp = a.TenKP;
            //            themmoi.makp = a.MaKP;
            //            themmoi.chon = true;
            //            _Kphong.Add(themmoi);
            //        }
            //    }
            //}
            //grcKhoaphong.DataSource = _Kphong.ToList();


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
    }
}